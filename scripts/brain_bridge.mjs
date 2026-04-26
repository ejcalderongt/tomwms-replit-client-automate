#!/usr/bin/env node
// brain_bridge.mjs — bridge entre eventos operativos y el brain del agente.
//
// Flujo:
//   1. CONSUMIDOR (openclaw): tras un apply OK, llama `brain_bridge notify ...`
//      que escribe un evento JSON en brain/_inbox/<id>.json del repo de
//      intercambio (rama wms-brain) y empuja.
//   2. PRODUCTOR (Replit): en la siguiente sesion lee `brain_bridge list`,
//      analiza con `brain_bridge analyze <id>` (heuristica: matchea modulos
//      tocados contra .md del brain), y genera una propuesta markdown.
//   3. El agente (humano o IA) revisa la propuesta, edita el brain segun
//      corresponda, y marca con `brain_bridge apply <id> --note "..."`.
//
// El bridge NO auto-edita el brain. Solo propone y trazabiliza.

import {
  readFileSync, writeFileSync, existsSync, readdirSync, mkdirSync,
  statSync, renameSync,
} from "node:fs";
import { spawnSync } from "node:child_process";
import { resolve, join, dirname, basename } from "node:path";
import { fileURLToPath } from "node:url";
import { hostname } from "node:os";

const __dirname = dirname(fileURLToPath(import.meta.url));
const SCHEMA_VERSION = "1";
const VALID_TYPES = [
  "apply_succeeded",
  "apply_failed",
  "skill_update",
  "directive",
  "merge_completed",
  "external_change",
];
const VALID_STATUSES = ["pending", "analyzed", "proposed", "applied", "skipped"];

// ---------- arg parsing -----------------------------------------------------

function parseArgs(argv) {
  const args = { _: [] };
  for (let i = 2; i < argv.length; i++) {
    const a = argv[i];
    if (a.startsWith("--")) {
      const key = a.slice(2);
      const next = argv[i + 1];
      if (next && !next.startsWith("--")) { args[key] = next; i++; }
      else args[key] = true;
    } else args._.push(a);
  }
  return args;
}

// ---------- git wrapper -----------------------------------------------------

function git(repo, args, opts = {}) {
  const r = spawnSync("git", args, { cwd: repo, encoding: "utf8", ...opts });
  if (r.status !== 0) {
    throw new Error(`git ${args.join(" ")} failed:\nSTDOUT: ${r.stdout}\nSTDERR: ${r.stderr}`);
  }
  return (r.stdout || "").trim();
}
function gitTry(repo, args) {
  const r = spawnSync("git", args, { cwd: repo, encoding: "utf8" });
  return { code: r.status, out: (r.stdout || "").trim(), err: (r.stderr || "").trim() };
}

// ---------- repo paths ------------------------------------------------------

function ensureBrainCheckout(exchangeRepo) {
  // Nota: este script asume que el caller ya hizo git fetch + checkout wms-brain.
  // Si la rama actual no es wms-brain, error.
  const branch = gitTry(exchangeRepo, ["symbolic-ref", "--short", "HEAD"]).out;
  if (branch !== "wms-brain") {
    throw new Error(`Repo en ${exchangeRepo} esta en rama '${branch}'. Necesario estar en 'wms-brain'.`);
  }
  for (const d of ["_inbox", "_proposals", "_processed"]) {
    const p = join(exchangeRepo, "brain", d);
    if (!existsSync(p)) mkdirSync(p, { recursive: true });
    const keep = join(p, ".gitkeep");
    if (!existsSync(keep)) writeFileSync(keep, "");
  }
}

// ---------- id ---------------------------------------------------------------

function newEventId(initials = "") {
  const now = new Date();
  const yyyy = now.getFullYear();
  const mm = String(now.getMonth() + 1).padStart(2, "0");
  const dd = String(now.getDate()).padStart(2, "0");
  const hh = String(now.getHours()).padStart(2, "0");
  const mi = String(now.getMinutes()).padStart(2, "0");
  const init = (initials || "").replace(/[^A-Za-z0-9]/g, "").toUpperCase().slice(0, 4);
  const suffix = init || Math.random().toString(36).slice(2, 6).toUpperCase();
  return `${yyyy}${mm}${dd}-${hh}${mi}-${suffix}`;
}

function nowIso() {
  return new Date().toISOString();
}

// ---------- subcommands -----------------------------------------------------

function cmdHelp() {
  console.log(`brain_bridge.mjs — bridge entre eventos operativos y el brain

Subcomandos:

  notify    Escribir un evento al inbox.
            --exchange-repo <path>      ruta al clon (debe estar en rama wms-brain)
            --from-event-file <path>    cargar draft generado por apply_bundle
                                        (mutuamente exclusivo con flags individuales).
            --type <tipo>               ${VALID_TYPES.join(" | ")}
            --source <openclaw|replit|manual|apply_bundle>
            --bundle <vNN>              opcional
            --commit <sha>              opcional
            --rama-destino <rama>       opcional (default dev_2028_merge)
            --files <a,b,c>             opcional, csv de paths
            --marker <#FIX_...>         opcional
            --modules <a,b,c>           opcional, csv de modulos tocados
            --tags <a,b,c>              opcional, csv de tags
            --message <texto>           recomendado, contexto libre
            --initials <XYZ>            opcional, para el id
            --no-push                   no hacer git commit+push (solo escribir local)

  list      Lista eventos.
            --exchange-repo <path>
            --status <pending|analyzed|proposed|applied|skipped|all>   default pending

  show      Muestra un evento.
            --exchange-repo <path>
            --id <event_id>

  analyze   Heuristica: busca menciones de modulos/tags/marker en .md del brain
            y genera /brain/_proposals/<id>.md con candidate_files + extractos.
            Marca el evento como status=proposed.
            --exchange-repo <path>
            --id <event_id>
            --no-push

  apply     Marca el evento como applied y lo mueve a _processed/.
            Es responsabilidad del agente haber hecho los edits al brain antes.
            --exchange-repo <path>
            --id <event_id>
            --note <texto>              razon o resumen del cambio aplicado
            --by <iniciales>            quien aplico
            --no-push

  skip      Marca el evento como skipped (no requiere accion en el brain).
            --exchange-repo <path>
            --id <event_id>
            --reason <texto>
            --no-push

  help      Esta ayuda.

Ejemplos:

  # Lado consumidor (openclaw), tras un apply OK:
  node scripts/brain_bridge.mjs notify \\
    --exchange-repo C:\\tomwms-exchange \\
    --type apply_succeeded --source openclaw \\
    --bundle v23 --commit abc1234 \\
    --files "TOMIMSV4/.../frmAjusteStock.vb" \\
    --marker "#FIX_v23_ELIMINAR_AJUSTE_RULES_2026-04-25" \\
    --modules "frmAjusteStock,ajuste_rules" \\
    --message "Eliminada validacion de borrador. Revisar regla en SKILL §6."

  # Lado productor (Replit):
  node scripts/brain_bridge.mjs list --exchange-repo /tmp/exchange-rw
  node scripts/brain_bridge.mjs analyze --exchange-repo /tmp/exchange-rw --id 20260427-1845-EJC
  # ...editar manualmente brain/skills/wms-tomwms/SKILL.md...
  node scripts/brain_bridge.mjs apply --exchange-repo /tmp/exchange-rw --id 20260427-1845-EJC \\
    --note "Actualizada SKILL §6: la regla de ajuste_rules ya no aplica"
`);
}

function cmdNotify(args) {
  const exch = resolve(args["exchange-repo"] || "");
  if (!exch) throw new Error("--exchange-repo requerido");
  ensureBrainCheckout(exch);

  let event;
  // Modo A: cargar desde archivo (draft generado por apply_bundle).
  if (args["from-event-file"]) {
    const src = resolve(args["from-event-file"]);
    if (!existsSync(src)) throw new Error(`from-event-file no existe: ${src}`);
    event = JSON.parse(readFileSync(src, "utf8"));
    if (!event.type || !VALID_TYPES.includes(event.type)) {
      throw new Error(`brain_event.json: type invalido (${event.type}). Validos: ${VALID_TYPES.join(", ")}`);
    }
    event.id = newEventId(args.initials || "");
    event.schema_version = SCHEMA_VERSION;
    event.created_at = event.created_at || nowIso();
    event.status = "pending";
    event.history = event.history || [];
    event.history.push({ at: nowIso(), action: "notify", by: event.source || "from-file" });
  } else {
    // Modo B: construir desde flags.
    if (!args.type) throw new Error("--type requerido (o usar --from-event-file)");
    if (!VALID_TYPES.includes(args.type)) {
      throw new Error(`--type invalido. Validos: ${VALID_TYPES.join(", ")}`);
    }
    const id = newEventId(args.initials || "");
    const filesCsv = args.files || "";
    const modulesCsv = args.modules || "";
    const tagsCsv = args.tags || "";
    event = {
      id,
      schema_version: SCHEMA_VERSION,
      created_at: nowIso(),
      type: args.type,
      source: args.source || "manual",
      host: hostname(),
      ref: {
        bundle: args.bundle || null,
        commit_sha: args.commit || null,
        rama_destino: args["rama-destino"] || null,
        files_changed: filesCsv ? filesCsv.split(",").map((s) => s.trim()).filter(Boolean) : [],
        marker: args.marker || null,
      },
      context: {
        message: args.message || "",
        modules_touched: modulesCsv ? modulesCsv.split(",").map((s) => s.trim()).filter(Boolean) : [],
        tags: tagsCsv ? tagsCsv.split(",").map((s) => s.trim()).filter(Boolean) : [],
      },
      analysis: null,
      proposal: null,
      status: "pending",
      decision: null,
      history: [{ at: nowIso(), action: "notify", by: args.source || "manual" }],
    };
  }
  const id = event.id;

  const dest = join(exch, "brain", "_inbox", `${id}.json`);
  writeFileSync(dest, JSON.stringify(event, null, 2) + "\n");
  console.log(`evento creado: brain/_inbox/${id}.json`);

  if (args["no-push"]) {
    console.log("--no-push: skip git commit/push");
    return;
  }
  git(exch, ["add", `brain/_inbox/${id}.json`]);
  git(exch, ["-c", "user.email=brain-bridge@prograx24", "-c", "user.name=brain_bridge",
             "commit", "-m", `wms-brain: evento ${args.type} ${id}`]);
  git(exch, ["push", "origin", "wms-brain"]);
  console.log(`commit + push OK`);
}

function loadEvents(exch, status) {
  const inbox = join(exch, "brain", "_inbox");
  const proc = join(exch, "brain", "_processed");
  const all = [];
  for (const dir of [inbox, proc]) {
    if (!existsSync(dir)) continue;
    for (const f of readdirSync(dir).filter((n) => n.endsWith(".json"))) {
      try {
        const ev = JSON.parse(readFileSync(join(dir, f), "utf8"));
        ev._dir = dir === inbox ? "_inbox" : "_processed";
        ev._path = join(dir, f);
        all.push(ev);
      } catch (e) {
        console.warn(`WARN: no pude leer ${f}: ${e.message}`);
      }
    }
  }
  if (status && status !== "all") {
    return all.filter((e) => e.status === status);
  }
  return all;
}

function cmdList(args) {
  const exch = resolve(args["exchange-repo"] || "");
  if (!exch) throw new Error("--exchange-repo requerido");
  const status = args.status || "pending";
  if (status !== "all" && !VALID_STATUSES.includes(status)) {
    throw new Error(`--status invalido. Validos: all, ${VALID_STATUSES.join(", ")}`);
  }
  const events = loadEvents(exch, status);
  if (events.length === 0) {
    console.log(`(no hay eventos con status=${status})`);
    return;
  }
  events.sort((a, b) => a.created_at.localeCompare(b.created_at));
  console.log(`${events.length} evento(s) status=${status}:\n`);
  for (const e of events) {
    const ref = e.ref?.bundle ? `bundle=${e.ref.bundle}` : "";
    console.log(`  ${e.id}  type=${e.type}  status=${e.status}  ${ref}  source=${e.source}`);
    if (e.context?.message) console.log(`    "${e.context.message.slice(0, 100)}"`);
  }
}

function findEvent(exch, id) {
  const events = loadEvents(exch, "all");
  const ev = events.find((e) => e.id === id);
  if (!ev) throw new Error(`evento '${id}' no encontrado`);
  return ev;
}

function cmdShow(args) {
  const exch = resolve(args["exchange-repo"] || "");
  const id = args.id;
  if (!exch || !id) throw new Error("--exchange-repo y --id requeridos");
  const ev = findEvent(exch, id);
  console.log(JSON.stringify(ev, null, 2));
}

// ---------- analyze: heuristica ---------------------------------------------

function listBrainMarkdown(exch) {
  const root = join(exch, "brain");
  const out = [];
  function walk(dir) {
    for (const e of readdirSync(dir)) {
      if (e.startsWith("_")) continue; // skip _inbox, _proposals, _processed
      const p = join(dir, e);
      const st = statSync(p);
      if (st.isDirectory()) walk(p);
      else if (e.endsWith(".md")) out.push(p);
    }
  }
  walk(root);
  return out;
}

function escapeRegex(s) { return s.replace(/[.*+?^${}()|[\]\\]/g, "\\$&"); }

function findMatches(text, needles) {
  // Devuelve { needle -> [linea de match, ...] } (max 5 lineas por needle)
  const lines = text.split("\n");
  const result = {};
  for (const n of needles) {
    if (!n) continue;
    const re = new RegExp(escapeRegex(n), "i");
    const hits = [];
    for (let i = 0; i < lines.length; i++) {
      if (re.test(lines[i])) {
        hits.push({ lineno: i + 1, text: lines[i].slice(0, 200) });
        if (hits.length >= 5) break;
      }
    }
    if (hits.length) result[n] = hits;
  }
  return result;
}

function cmdAnalyze(args) {
  const exch = resolve(args["exchange-repo"] || "");
  const id = args.id;
  if (!exch || !id) throw new Error("--exchange-repo y --id requeridos");
  ensureBrainCheckout(exch);
  const ev = findEvent(exch, id);
  if (ev._dir === "_processed") {
    throw new Error(`evento '${id}' ya esta en _processed`);
  }

  const needles = [...new Set([
    ...(ev.context?.modules_touched || []),
    ...(ev.context?.tags || []),
    ev.ref?.marker || "",
    ...(ev.ref?.files_changed || []).map((f) => basename(f).replace(/\.[a-z]+$/i, "")),
  ].filter(Boolean))];

  const mds = listBrainMarkdown(exch);
  const candidates = [];
  for (const md of mds) {
    const text = readFileSync(md, "utf8");
    const matches = findMatches(text, needles);
    if (Object.keys(matches).length > 0) {
      candidates.push({ path: md.replace(exch + "/", ""), matches });
    }
  }

  const proposalLines = [
    `# Propuesta de actualizacion del brain`,
    ``,
    `**Evento**: \`${ev.id}\``,
    `**Tipo**: \`${ev.type}\``,
    `**Origen**: ${ev.source} (host ${ev.host})`,
    `**Fecha**: ${ev.created_at}`,
    ``,
    `## Contexto`,
    ``,
    ev.context?.message || "_(sin mensaje)_",
    ``,
    `## Referencias`,
    ``,
    ev.ref?.bundle ? `- Bundle: \`${ev.ref.bundle}\``: "",
    ev.ref?.commit_sha ? `- Commit WMS: \`${ev.ref.commit_sha}\`` : "",
    ev.ref?.marker ? `- Marker: \`${ev.ref.marker}\`` : "",
    (ev.ref?.files_changed || []).length
      ? `- Archivos cambiados:\n${ev.ref.files_changed.map((f) => `  - \`${f}\``).join("\n")}`
      : "",
    (ev.context?.modules_touched || []).length
      ? `- Modulos tocados: ${ev.context.modules_touched.map((m) => `\`${m}\``).join(", ")}`
      : "",
    (ev.context?.tags || []).length
      ? `- Tags: ${ev.context.tags.map((t) => `\`${t}\``).join(", ")}`
      : "",
    ``,
    `## Analisis heuristico`,
    ``,
    `Busque las siguientes claves en los .md del brain:`,
    ``,
    needles.map((n) => `- \`${n}\``).join("\n"),
    ``,
  ].filter((l) => l !== "");

  if (candidates.length === 0) {
    proposalLines.push(`### Resultado`);
    proposalLines.push(``);
    proposalLines.push(`**Sin matches** en los .md del brain. Posibles cursos:`);
    proposalLines.push(``);
    proposalLines.push(`1. El cambio no impacta el brain -> \`brain_bridge skip --id ${ev.id} --reason "no impacta brain"\``);
    proposalLines.push(`2. El brain no documentaba esa area todavia -> agregar seccion nueva manualmente y \`brain_bridge apply --id ${ev.id}\``);
  } else {
    proposalLines.push(`### Archivos candidatos a actualizar (${candidates.length})`);
    proposalLines.push(``);
    for (const c of candidates) {
      proposalLines.push(`#### \`${c.path}\``);
      proposalLines.push(``);
      for (const [needle, hits] of Object.entries(c.matches)) {
        proposalLines.push(`- match \`${needle}\`:`);
        for (const h of hits) {
          proposalLines.push(`  - L${h.lineno}: \`${h.text.trim()}\``);
        }
      }
      proposalLines.push(``);
    }
    proposalLines.push(`### Sugerencia`);
    proposalLines.push(``);
    proposalLines.push(`Revisar cada archivo candidato. Para cada match decidir:`);
    proposalLines.push(``);
    proposalLines.push(`- **Mantener** la regla/menciona como esta (el cambio del WMS no contradice el brain).`);
    proposalLines.push(`- **Actualizar** el texto (la regla cambio).`);
    proposalLines.push(`- **Eliminar** la mencion (la funcionalidad fue removida).`);
    proposalLines.push(``);
    proposalLines.push(`Despues de editar, ejecutar:`);
    proposalLines.push(``);
    proposalLines.push(`\`\`\``);
    proposalLines.push(`brain_bridge apply --id ${ev.id} --note "<resumen del cambio>" --by <iniciales>`);
    proposalLines.push(`\`\`\``);
  }

  const propPath = join(exch, "brain", "_proposals", `${ev.id}.md`);
  writeFileSync(propPath, proposalLines.join("\n") + "\n");

  // actualizar evento
  ev.analysis = {
    candidate_files: candidates.map((c) => c.path),
    needles_used: needles,
    auto: true,
    at: nowIso(),
  };
  ev.proposal = { markdown_path: `brain/_proposals/${ev.id}.md` };
  ev.status = "proposed";
  ev.history.push({ at: nowIso(), action: "analyze" });
  writeFileSync(ev._path, JSON.stringify(ev, null, 2) + "\n");

  console.log(`propuesta escrita: brain/_proposals/${ev.id}.md`);
  console.log(`candidatos: ${candidates.length}`);
  console.log(`evento ${ev.id} ahora en status=proposed`);

  if (args["no-push"]) {
    console.log("--no-push: skip git commit/push");
    return;
  }
  git(exch, ["add", `brain/_inbox/${ev.id}.json`, `brain/_proposals/${ev.id}.md`]);
  git(exch, ["-c", "user.email=brain-bridge@prograx24", "-c", "user.name=brain_bridge",
             "commit", "-m", `wms-brain: analyze ${ev.id} (${candidates.length} candidatos)`]);
  git(exch, ["push", "origin", "wms-brain"]);
  console.log(`commit + push OK`);
}

function cmdApply(args) {
  const exch = resolve(args["exchange-repo"] || "");
  const id = args.id;
  if (!exch || !id) throw new Error("--exchange-repo y --id requeridos");
  ensureBrainCheckout(exch);
  const ev = findEvent(exch, id);
  if (ev._dir === "_processed") {
    throw new Error(`evento '${id}' ya esta en _processed`);
  }

  ev.status = "applied";
  ev.decision = {
    by: args.by || "agent",
    at: nowIso(),
    verdict: "approved",
    note: args.note || "",
  };
  ev.history.push({ at: nowIso(), action: "apply", by: args.by || "agent" });

  const newPath = join(exch, "brain", "_processed", `${ev.id}.json`);
  writeFileSync(newPath, JSON.stringify(ev, null, 2) + "\n");
  // Borrar del inbox
  if (ev._dir === "_inbox") {
    const inboxPath = ev._path;
    const r = spawnSync("git", ["rm", "-q", inboxPath], { cwd: exch, encoding: "utf8" });
    if (r.status !== 0) {
      // fallback fs
      try { renameSync(inboxPath, newPath); } catch {}
    }
  }
  console.log(`evento ${ev.id} -> _processed/`);

  if (args["no-push"]) return;

  // git add captura el nuevo file y el rm del inbox
  git(exch, ["add", "-A", "brain/_inbox/", "brain/_processed/"]);
  // tambien stagear cualquier cambio en brain/skills, brain/agent-context, etc.
  git(exch, ["add", "brain/"]);
  const status = git(exch, ["status", "--porcelain"]);
  if (status === "") {
    console.log("(nada para commitear)");
    return;
  }
  git(exch, ["-c", "user.email=brain-bridge@prograx24", "-c", "user.name=brain_bridge",
             "commit", "-m", `wms-brain: apply ${ev.id} — ${args.note || "applied"}`]);
  git(exch, ["push", "origin", "wms-brain"]);
  console.log(`commit + push OK`);
}

function cmdSkip(args) {
  const exch = resolve(args["exchange-repo"] || "");
  const id = args.id;
  if (!exch || !id) throw new Error("--exchange-repo y --id requeridos");
  ensureBrainCheckout(exch);
  const ev = findEvent(exch, id);
  if (ev._dir === "_processed") {
    throw new Error(`evento '${id}' ya esta en _processed`);
  }

  ev.status = "skipped";
  ev.decision = {
    by: "agent",
    at: nowIso(),
    verdict: "skipped",
    reason: args.reason || "",
  };
  ev.history.push({ at: nowIso(), action: "skip" });

  const newPath = join(exch, "brain", "_processed", `${ev.id}.json`);
  writeFileSync(newPath, JSON.stringify(ev, null, 2) + "\n");
  if (ev._dir === "_inbox") {
    spawnSync("git", ["rm", "-q", ev._path], { cwd: exch, encoding: "utf8" });
  }
  console.log(`evento ${ev.id} -> _processed/ (skipped)`);

  if (args["no-push"]) return;
  git(exch, ["add", "-A", "brain/_inbox/", "brain/_processed/"]);
  git(exch, ["-c", "user.email=brain-bridge@prograx24", "-c", "user.name=brain_bridge",
             "commit", "-m", `wms-brain: skip ${ev.id} — ${args.reason || "no aplica"}`]);
  git(exch, ["push", "origin", "wms-brain"]);
  console.log(`commit + push OK`);
}

// ---------- main ------------------------------------------------------------

const args = parseArgs(process.argv);
const sub = args._[0] || "help";
try {
  switch (sub) {
    case "notify":  cmdNotify(args); break;
    case "list":    cmdList(args); break;
    case "show":    cmdShow(args); break;
    case "analyze": cmdAnalyze(args); break;
    case "apply":   cmdApply(args); break;
    case "skip":    cmdSkip(args); break;
    case "help":
    case "-h":
    case "--help":  cmdHelp(); break;
    default:
      console.error(`subcomando desconocido: ${sub}`);
      cmdHelp();
      process.exit(2);
  }
} catch (e) {
  console.error(`ERROR: ${e.message}`);
  process.exit(1);
}
