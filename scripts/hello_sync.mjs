#!/usr/bin/env node
import { readFileSync, existsSync, readdirSync, statSync } from "node:fs";
import { spawnSync } from "node:child_process";
import { resolve, join, dirname } from "node:path";
import { fileURLToPath } from "node:url";

const __dirname = dirname(fileURLToPath(import.meta.url));

function parseArgs(argv) {
  const args = { rol: "productor" };
  for (let i = 2; i < argv.length; i++) {
    const a = argv[i];
    if (a === "--rol") args.rol = argv[++i];
    else if (a === "--exchange-repo") args.exchangeRepo = argv[++i];
    else if (a === "--wms-repo") args.wmsRepo = argv[++i];
    else if (a === "--no-pull") args.noPull = true;
    else if (a === "--quiet") args.quiet = true;
    else if (a === "--help" || a === "-h") args.help = true;
    else throw new Error(`Argumento desconocido: ${a}`);
  }
  return args;
}

function printHelp() {
  console.log(`hello_sync.mjs — handshake productor (Replit) <-> consumidor (openclaw)

Uso:
  node scripts/hello_sync.mjs --rol productor   --exchange-repo <path> [--no-pull]
  node scripts/hello_sync.mjs --rol consumidor  --exchange-repo <path> --wms-repo <path>

Pasos del handshake (mismos para ambos roles):
  1. git fetch + pull del repo de intercambio (GitHub)
  2. Leer historial de apply_log.json en entregables_ajuste/
  3. Detectar ultimo bundle producido y ultimo aplicado
  4. (Consumidor) git fetch + pull del repo WMS (Azure DevOps)
  5. Verificar working tree limpio en exchange y WMS
  6. Si todo OK: imprimir 'Hello Erik' + figura ASCII

Salida:
  exit 0  -> handshake OK, listo para operar
  exit 1  -> handshake FAIL, motivo en stderr
`);
}

function git(repo, args, opts = {}) {
  const r = spawnSync("git", args, { cwd: repo, encoding: "utf8", ...opts });
  return { code: r.status, out: (r.stdout || "").trim(), err: (r.stderr || "").trim() };
}

function logStep(quiet, msg) {
  if (!quiet) console.log(msg);
}

function syncRepo(repo, label, quiet) {
  if (!existsSync(join(repo, ".git"))) {
    return { ok: false, msg: `${label}: '${repo}' no es un repo git` };
  }
  logStep(quiet, `  [${label}] git fetch...`);
  const f = git(repo, ["fetch", "--all", "--prune"]);
  if (f.code !== 0) return { ok: false, msg: `${label} fetch fallo: ${f.err}` };

  const branch = git(repo, ["rev-parse", "--abbrev-ref", "HEAD"]).out;
  logStep(quiet, `  [${label}] rama actual: ${branch}`);

  const status = git(repo, ["status", "--porcelain"]).out;
  if (status !== "") {
    return { ok: false, msg: `${label}: working tree sucio:\n${status}` };
  }

  logStep(quiet, `  [${label}] git pull --ff-only...`);
  const p = git(repo, ["pull", "--ff-only"]);
  if (p.code !== 0) {
    return { ok: false, msg: `${label} pull fallo (no fast-forward?): ${p.err}` };
  }
  const head = git(repo, ["log", "--oneline", "-1"]).out;
  return { ok: true, branch, head };
}

function scanHistory(exchangeRepo) {
  const root = join(exchangeRepo, "entregables_ajuste");
  if (!existsSync(root)) {
    return { bundles: [], note: "entregables_ajuste/ no existe (primera sesion)" };
  }
  const dateRe = /^\d{4}-\d{2}-\d{2}$/;
  const dates = readdirSync(root)
    .filter((d) => dateRe.test(d))
    .filter((d) => statSync(join(root, d)).isDirectory())
    .sort();
  const bundles = [];
  for (const date of dates) {
    const dateDir = join(root, date);
    const items = readdirSync(dateDir)
      .filter((b) => /^v\d+_bundle$/.test(b))
      .filter((b) => statSync(join(dateDir, b)).isDirectory())
      .sort((a, b) => parseInt(a.match(/^v(\d+)_/)[1], 10) - parseInt(b.match(/^v(\d+)_/)[1], 10));
    for (const item of items) {
      const bDir = join(dateDir, item);
      const manifestPath = join(bDir, "MANIFEST.json");
      const logPath = join(bDir, "apply_log.json");
      let version = item.replace(/_bundle$/, "");
      let manifest = null;
      if (existsSync(manifestPath)) {
        try {
          manifest = JSON.parse(readFileSync(manifestPath, "utf8"));
          version = manifest.version || manifest.bundle?.replace(/_bundle$/, "") || version;
        } catch {}
      }
      let log = null;
      if (existsSync(logPath)) {
        try { log = JSON.parse(readFileSync(logPath, "utf8")); } catch {}
      }
      bundles.push({ date, version, dir: bDir, manifest, log });
    }
  }
  return { bundles };
}

function asciiOk() {
  return [
    "    .---------------------.",
    "    |  TOMWMS x OpenClaw  |",
    "    |    sync handshake   |",
    "    |       [ OK ]        |",
    "    '---------------------'",
    "             | |",
    "           __|_|__",
    "          |       |",
    "          |  v23  |",
    "          '-------'",
  ].join("\n");
}

async function main() {
  let args;
  try { args = parseArgs(process.argv); } catch (e) {
    console.error(`ERROR: ${e.message}`);
    printHelp();
    process.exit(2);
  }
  if (args.help) { printHelp(); return; }
  if (!args.exchangeRepo) {
    console.error("ERROR: --exchange-repo es obligatorio");
    process.exit(2);
  }
  if (args.rol === "consumidor" && !args.wmsRepo) {
    console.error("ERROR: --wms-repo es obligatorio en rol consumidor");
    process.exit(2);
  }

  const exchangeRepo = resolve(args.exchangeRepo);
  const quiet = !!args.quiet;

  console.log(`hello_sync — rol: ${args.rol}`);
  console.log(`exchange:    ${exchangeRepo}`);
  if (args.wmsRepo) console.log(`wms:         ${resolve(args.wmsRepo)}`);
  console.log("");

  // Paso 1+5: sync del repo de intercambio
  console.log("[1/4] Sync repo de intercambio (GitHub)...");
  if (args.noPull) {
    logStep(quiet, "  --no-pull: skip fetch/pull");
  } else {
    const r = syncRepo(exchangeRepo, "exchange", quiet);
    if (!r.ok) {
      console.error(`  FAIL: ${r.msg}`);
      process.exit(1);
    }
    console.log(`  OK  rama=${r.branch} head=${r.head}`);
  }

  // Paso 2+3: historial
  console.log("\n[2/4] Historial de bundles...");
  const { bundles, note } = scanHistory(exchangeRepo);
  if (note) console.log(`  ${note}`);
  console.log(`  bundles encontrados: ${bundles.length}`);
  let lastProduced = null;
  let lastApplied = null;
  let pending = null;
  for (const b of bundles) {
    const status = b.log ? b.log.result : "PENDING";
    console.log(`  - ${b.date}/${b.version}  ${status}` + (b.log?.commit_sha ? `  commit=${b.log.commit_sha.slice(0, 8)}` : ""));
    lastProduced = b;
    if (b.log?.result === "OK") lastApplied = b;
    if (!b.log) pending = b;
  }
  console.log("");
  console.log(`  ultimo producido: ${lastProduced ? `${lastProduced.date}/${lastProduced.version}` : "(ninguno)"}`);
  console.log(`  ultimo aplicado:  ${lastApplied ? `${lastApplied.date}/${lastApplied.version}` : "(ninguno)"}`);
  console.log(`  pendiente:        ${pending ? `${pending.date}/${pending.version}` : "(ninguno)"}`);

  // Paso 4: WMS sync (solo consumidor)
  if (args.rol === "consumidor") {
    console.log("\n[3/4] Sync repo WMS (Azure DevOps)...");
    const r = syncRepo(resolve(args.wmsRepo), "wms", quiet);
    if (!r.ok) {
      console.error(`  FAIL: ${r.msg}`);
      process.exit(1);
    }
    console.log(`  OK  rama=${r.branch} head=${r.head}`);
  } else {
    console.log("\n[3/4] Sync WMS: skip (rol productor; el WMS vive del lado consumidor)");
  }

  // Paso final
  console.log("\n[4/4] Handshake OK");
  console.log("");
  console.log("Hello Erik");
  console.log(asciiOk());
  console.log("");
  if (pending && args.rol === "consumidor") {
    console.log(`Proximo paso: aplicar ${pending.date}/${pending.version}`);
    console.log(`  node scripts/apply_bundle.mjs --latest --repo ${args.wmsRepo}`);
  } else if (args.rol === "productor") {
    if (lastProduced && !lastProduced.log) {
      console.log(`Aviso: ${lastProduced.date}/${lastProduced.version} aun sin apply_log. Esperar a openclaw antes de generar el siguiente.`);
    } else {
      console.log("Listo para producir el proximo bundle.");
    }
  }
}

main().catch((e) => {
  console.error(`\nERROR FATAL: ${e.message}`);
  process.exit(1);
});
