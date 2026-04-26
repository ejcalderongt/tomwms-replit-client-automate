#!/usr/bin/env node
import { readFileSync, writeFileSync, existsSync, readdirSync, statSync } from "node:fs";
import { createHash } from "node:crypto";
import { execSync, spawnSync } from "node:child_process";
import { resolve, join, basename, dirname } from "node:path";
import { createInterface } from "node:readline";
import { fileURLToPath } from "node:url";

const __dirname = dirname(fileURLToPath(import.meta.url));
const REPO_ROOT_DEFAULT = resolve(__dirname, "..");

function parseArgs(argv) {
  const args = { dryRun: false, yes: false };
  for (let i = 2; i < argv.length; i++) {
    const a = argv[i];
    if (a === "--latest") args.latest = true;
    else if (a === "--bundle") args.bundle = argv[++i];
    else if (a === "--repo") args.repo = argv[++i];
    else if (a === "--rama-destino") args.ramaDestino = argv[++i];
    else if (a === "--dry-run") args.dryRun = true;
    else if (a === "--yes" || a === "-y") args.yes = true;
    else if (a === "--help" || a === "-h") args.help = true;
    else if (a === "--bundles-root") args.bundlesRoot = argv[++i];
    else throw new Error(`Argumento desconocido: ${a}`);
  }
  return args;
}

function printHelp() {
  console.log(`apply_bundle.mjs — aplicador de bundles vNN al repo TOMWMS

Uso:
  node scripts/apply_bundle.mjs [--latest | --bundle <path>] --repo <repo>
                                [--rama-destino <rama>] [--dry-run] [--yes]
                                [--bundles-root <dir>]

Args:
  --latest             Tomar el ultimo vNN_bundle de --bundles-root.
  --bundle <path>      Path explicito al directorio vNN_bundle.
  --repo <path>        Path al checkout local del repo VS (TOMIMSV4).
  --rama-destino <s>   Rama esperada y a la que se merge eventualmente.
                       Default: la del MANIFEST (rama_destino).
  --dry-run            Correr todas las validaciones, NO aplicar.
  --yes / -y           Confirmar automaticamente (uso CI).
  --bundles-root <d>   Default: <dir del script>/../entregables_ajuste

Comportamiento:
  1. Resuelve el bundle (--latest ordena por fecha+vNN descendente).
  2. Valida precondiciones: working tree limpio, rama, md5_orig, marcador
     ausente, git apply --check.
  3. Crea rama efimera: agent/vNN-<YYYYMMDD>-<md5short>.
  4. Aplica con git apply, valida md5_mod y conteo de marcador.
  5. Imprime diff --stat, ofrece comando para diff completo.
  6. Pide confirmacion (Y/N) salvo --yes.
  7. Escribe apply_log.json en el bundle (OK o FAIL).
  8. Si OK: imprime comandos de merge sugeridos. NO auto-mergea.
  9. Si FAIL: deja la rama efimera intacta para inspeccion.

Ver entregables_ajuste/AGENTS.md para el contrato completo.
`);
}

function md5File(path) {
  const buf = readFileSync(path);
  return createHash("md5").update(buf).digest("hex");
}

function git(repo, args, opts = {}) {
  const r = spawnSync("git", args, { cwd: repo, encoding: "utf8", ...opts });
  if (r.status !== 0 && !opts.allowFail) {
    const cmd = "git " + args.join(" ");
    throw new Error(`Comando fallo (exit ${r.status}): ${cmd}\nstderr: ${r.stderr}`);
  }
  return r;
}

function gitOut(repo, args) {
  return git(repo, args).stdout.trim();
}

function findLatestBundle(bundlesRoot) {
  if (!existsSync(bundlesRoot)) {
    throw new Error(`bundles-root no existe: ${bundlesRoot}`);
  }
  const dateRe = /^\d{4}-\d{2}-\d{2}$/;
  const dates = readdirSync(bundlesRoot)
    .filter((d) => dateRe.test(d))
    .filter((d) => statSync(join(bundlesRoot, d)).isDirectory())
    .sort()
    .reverse();
  for (const d of dates) {
    const dateDir = join(bundlesRoot, d);
    const bundles = readdirSync(dateDir)
      .filter((b) => /^v\d+_bundle$/.test(b))
      .filter((b) => statSync(join(dateDir, b)).isDirectory())
      .sort((a, b) => {
        const na = parseInt(a.match(/^v(\d+)_/)[1], 10);
        const nb = parseInt(b.match(/^v(\d+)_/)[1], 10);
        return nb - na;
      });
    if (bundles.length > 0) return join(dateDir, bundles[0]);
  }
  throw new Error(`No se encontro ningun vNN_bundle en ${bundlesRoot}`);
}

function safeRepoPath(repo, relPath) {
  // Resuelve relPath dentro de repo y valida que NO escape (path traversal).
  // Tolera separadores Windows ('\') aunque el manifest deberia traer '/'.
  const cleaned = String(relPath || "").replace(/\\/g, "/");
  if (cleaned === "" || cleaned.startsWith("/") || /^[a-zA-Z]:/.test(cleaned)) {
    throw new Error(`MANIFEST: path no relativo: ${relPath}`);
  }
  const repoRoot = resolve(repo);
  const abs = resolve(repoRoot, cleaned);
  const relCheck = abs.slice(repoRoot.length);
  if (abs !== repoRoot && !relCheck.startsWith("/") && !relCheck.startsWith("\\")) {
    throw new Error(`MANIFEST: path escapa del repo: ${relPath}`);
  }
  if (abs.indexOf(repoRoot) !== 0) {
    throw new Error(`MANIFEST: path escapa del repo: ${relPath}`);
  }
  return abs;
}

function loadBundle(bundleDir) {
  const manifestPath = join(bundleDir, "MANIFEST.json");
  if (!existsSync(manifestPath)) {
    throw new Error(`MANIFEST.json no encontrado en ${bundleDir}`);
  }
  const raw = JSON.parse(readFileSync(manifestPath, "utf8"));
  // Normalizar: el productor emite campos en espanol, mapeamos a un objeto
  // canonico para uso interno. Aceptamos tambien el nombre en ingles por
  // compatibilidad futura.
  const manifest = {
    version: raw.version || (raw.bundle ? raw.bundle.replace(/_bundle$/, "") : undefined),
    date: raw.date || raw.fecha,
    rama_destino: raw.rama_destino,
    base_commit: raw.base_commit,
    marker: raw.marker || raw.marcador_global,
    description: raw.description || raw.descripcion,
    files: (raw.files || raw.archivos || []).map((f) => ({
      path: f.path,
      encoding: (f.encoding || "utf8").toLowerCase().replace(/^utf-?8$/i, "utf8"),
      patch_name: f.patch_name || f.patch,
      md5_orig: f.md5_orig,
      md5_mod: f.md5_mod,
      summary: f.summary || f.cambios || "",
    })),
    compat: raw.compat || raw.compatibilidad || {},
    raw,
  };
  if (!manifest.version) throw new Error("MANIFEST sin 'version' / 'bundle'");
  if (!manifest.files.length) throw new Error("MANIFEST sin archivos");
  if (!manifest.marker) throw new Error("MANIFEST sin 'marker' / 'marcador_global'");

  const patchesDir = join(bundleDir, "patches");
  if (!existsSync(patchesDir)) {
    throw new Error(`patches/ no encontrado en ${bundleDir}`);
  }
  const patches = readdirSync(patchesDir)
    .filter((f) => f.endsWith(".patch"))
    .sort()
    .map((f) => join(patchesDir, f));
  if (patches.length === 0) {
    throw new Error(`No hay .patch en ${patchesDir}`);
  }
  return { dir: bundleDir, manifestPath, manifest, patches };
}

function checkPreconditions(repo, bundle, args) {
  const { manifest } = bundle;
  const errors = [];

  // Working tree limpio
  const status = gitOut(repo, ["status", "--porcelain"]);
  if (status !== "") {
    errors.push({
      check: "working_tree_clean",
      msg: `Working tree con cambios sin commitear:\n${status}`,
    });
  }

  // Rama actual
  const currentBranch = gitOut(repo, ["rev-parse", "--abbrev-ref", "HEAD"]);
  const expectedBranch = args.ramaDestino || manifest.rama_destino;
  if (expectedBranch && currentBranch !== expectedBranch) {
    errors.push({
      check: "branch_match",
      msg: `Rama actual '${currentBranch}' != esperada '${expectedBranch}'. Use --rama-destino para overridear.`,
    });
  }

  // md5_orig por archivo (con sandbox de path)
  for (const f of manifest.files) {
    let filePath;
    try {
      filePath = safeRepoPath(repo, f.path);
    } catch (e) {
      errors.push({ check: "path_safe", msg: e.message });
      continue;
    }
    if (!existsSync(filePath)) {
      errors.push({
        check: "file_exists",
        msg: `Archivo del MANIFEST no existe en repo: ${f.path}`,
      });
      continue;
    }
    const actual = md5File(filePath);
    if (actual !== f.md5_orig) {
      errors.push({
        check: "md5_orig_match",
        msg: `md5 mismatch en ${f.path}\n  esperado: ${f.md5_orig}\n  actual:   ${actual}\nProbablemente falte aplicar bundles previos.`,
      });
    }
  }

  // Marcador ausente (anti doble-apply)
  if (manifest.marker) {
    for (const f of manifest.files) {
      let filePath;
      try { filePath = safeRepoPath(repo, f.path); } catch { continue; }
      if (!existsSync(filePath)) continue;
      const content = readFileSync(filePath, "utf8");
      if (content.includes(manifest.marker)) {
        errors.push({
          check: "marker_absent",
          msg: `Marcador '${manifest.marker}' ya presente en ${f.path}. Bundle ya aplicado.`,
        });
        break;
      }
    }
  }

  // git apply --check con TODOS los patches en una sola invocacion (atomico)
  if (bundle.patches.length > 0) {
    const r = git(repo, ["apply", "--check", ...bundle.patches], { allowFail: true });
    if (r.status !== 0) {
      errors.push({
        check: "git_apply_check",
        msg: `git apply --check fallo:\n${r.stderr}`,
      });
    }
  }

  return errors;
}

function ensureEphemeralBranch(repo, bundle) {
  const { manifest } = bundle;
  const yyyymmdd = new Date().toISOString().slice(0, 10).replace(/-/g, "");
  const shortHash = (manifest.files[0]?.md5_mod || "00000000").slice(0, 4);
  const name = `agent/${manifest.version}-${yyyymmdd}-${shortHash}`;
  const exists = git(repo, ["rev-parse", "--verify", name], { allowFail: true });
  if (exists.status === 0) {
    throw new Error(`Rama efimera '${name}' ya existe. Borrar manualmente con: git branch -D ${name}`);
  }
  git(repo, ["checkout", "-b", name]);
  return name;
}

function applyPatches(repo, bundle) {
  // Atomico: una sola invocacion con todos los patches. Si falla a la mitad,
  // git apply hace rollback de los hunks aplicados antes de fallar.
  if (bundle.patches.length === 0) return;
  git(repo, ["apply", ...bundle.patches]);
}

function rollbackEphemeral(repo, branch, ramaDestino) {
  // Tras fallo de postcondicion: limpiar working tree, volver a la rama
  // destino, borrar la rama efimera. Idempotente y best-effort.
  try { git(repo, ["reset", "--hard", "HEAD"], { allowFail: true }); } catch {}
  try { git(repo, ["checkout", ramaDestino], { allowFail: true }); } catch {}
  try { git(repo, ["branch", "-D", branch], { allowFail: true }); } catch {}
}

function checkPostconditions(repo, bundle) {
  const { manifest } = bundle;
  const errors = [];

  for (const f of manifest.files) {
    const filePath = safeRepoPath(repo, f.path);
    const actual = md5File(filePath);
    if (actual !== f.md5_mod) {
      errors.push({
        check: "md5_mod_match",
        msg: `md5_mod mismatch en ${f.path}\n  esperado: ${f.md5_mod}\n  actual:   ${actual}`,
      });
    }
  }

  let markerHits = 0;
  if (manifest.marker) {
    for (const f of manifest.files) {
      const filePath = safeRepoPath(repo, f.path);
      const content = readFileSync(filePath, "utf8");
      const re = new RegExp(manifest.marker.replace(/[.*+?^${}()|[\]\\]/g, "\\$&"), "g");
      const hits = (content.match(re) || []).length;
      markerHits += hits;
    }
    if (markerHits === 0) {
      errors.push({
        check: "marker_present",
        msg: `Marcador '${manifest.marker}' NO presente despues de aplicar.`,
      });
    }
  }

  return { errors, markerHits };
}

async function confirm(question) {
  if (!process.stdin.isTTY) return false;
  const rl = createInterface({ input: process.stdin, output: process.stdout });
  const answer = await new Promise((res) => rl.question(question, (a) => { rl.close(); res(a); }));
  return /^(s|y|si|yes)$/i.test(answer.trim());
}

function writeApplyLog(bundle, payload) {
  const logPath = join(bundle.dir, "apply_log.json");
  writeFileSync(logPath, JSON.stringify(payload, null, 2) + "\n");
  return logPath;
}

function isoLocal() {
  const d = new Date();
  const pad = (n) => String(n).padStart(2, "0");
  const tz = -d.getTimezoneOffset();
  const sign = tz >= 0 ? "+" : "-";
  const tzh = pad(Math.floor(Math.abs(tz) / 60));
  const tzm = pad(Math.abs(tz) % 60);
  return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}T${pad(d.getHours())}:${pad(d.getMinutes())}:${pad(d.getSeconds())}${sign}${tzh}:${tzm}`;
}

async function main() {
  let args;
  try {
    args = parseArgs(process.argv);
  } catch (e) {
    console.error(`ERROR: ${e.message}`);
    printHelp();
    process.exit(2);
  }
  if (args.help) { printHelp(); return; }

  if (!args.repo) {
    console.error("ERROR: --repo es obligatorio");
    process.exit(2);
  }
  const repo = resolve(args.repo);
  if (!existsSync(join(repo, ".git"))) {
    console.error(`ERROR: ${repo} no es un repo git (falta .git/)`);
    process.exit(2);
  }

  const bundlesRoot = args.bundlesRoot
    ? resolve(args.bundlesRoot)
    : resolve(REPO_ROOT_DEFAULT, "entregables_ajuste");

  let bundleDir;
  if (args.bundle) bundleDir = resolve(args.bundle);
  else if (args.latest) bundleDir = findLatestBundle(bundlesRoot);
  else {
    console.error("ERROR: especificar --latest o --bundle <path>");
    process.exit(2);
  }
  console.log(`Bundle: ${bundleDir}`);
  console.log(`Repo:   ${repo}`);

  const bundle = loadBundle(bundleDir);
  console.log(`Version: ${bundle.manifest.version}  Fecha: ${bundle.manifest.date}  Marcador: ${bundle.manifest.marker}`);

  console.log("\n[1/5] Precondiciones...");
  const preErrors = checkPreconditions(repo, bundle, args);
  if (preErrors.length > 0) {
    console.error(`  FAIL: ${preErrors.length} error(es)`);
    for (const e of preErrors) console.error(`  - [${e.check}] ${e.msg}`);
    writeApplyLog(bundle, {
      version: bundle.manifest.version,
      date: bundle.manifest.date,
      applied_at: isoLocal(),
      result: "FAIL",
      failed_at_check: preErrors[0].check,
      error: preErrors[0].msg,
      rama_destino: bundle.manifest.rama_destino,
    });
    process.exit(1);
  }
  console.log("  OK");

  if (args.dryRun) {
    console.log("\n--dry-run: validaciones precondicion OK, no aplica. Saliendo.");
    return;
  }

  console.log("\n[2/5] Crear rama efimera...");
  const branch = ensureEphemeralBranch(repo, bundle);
  console.log(`  rama: ${branch}`);

  console.log("\n[3/5] Aplicar patches...");
  try {
    applyPatches(repo, bundle);
    console.log(`  OK (${bundle.patches.length} patch(es) aplicado(s))`);
  } catch (e) {
    console.error(`  FAIL: ${e.message}`);
    writeApplyLog(bundle, {
      version: bundle.manifest.version,
      date: bundle.manifest.date,
      applied_at: isoLocal(),
      result: "FAIL",
      failed_at_check: "git_apply",
      error: e.message,
      branch,
      rama_destino: bundle.manifest.rama_destino,
    });
    process.exit(1);
  }

  console.log("\n[4/5] Postcondiciones...");
  const post = checkPostconditions(repo, bundle);
  if (post.errors.length > 0) {
    console.error(`  FAIL: ${post.errors.length} error(es)`);
    for (const e of post.errors) console.error(`  - [${e.check}] ${e.msg}`);
    const target = args.ramaDestino || bundle.manifest.rama_destino;
    console.error(`  Rollback: descartando rama efimera y volviendo a '${target}'.`);
    rollbackEphemeral(repo, branch, target);
    writeApplyLog(bundle, {
      version: bundle.manifest.version,
      date: bundle.manifest.date,
      applied_at: isoLocal(),
      result: "FAIL",
      failed_at_check: post.errors[0].check,
      error: post.errors[0].msg,
      branch,
      branch_rolled_back: true,
      rama_destino: bundle.manifest.rama_destino,
    });
    process.exit(1);
  }
  console.log(`  OK  (marker hits: ${post.markerHits})`);

  console.log("\n[5/5] Diff resumido:");
  const stat = git(repo, ["diff", "--stat", "HEAD"]).stdout;
  console.log(stat || "  (sin cambios visibles?)");
  console.log(`  Diff completo: cd ${repo} && git diff HEAD`);

  let confirmed = args.yes;
  if (!confirmed) {
    console.log("");
    confirmed = await confirm("Confirmar aplicacion? [Y/n]: ");
  }

  if (!confirmed) {
    console.log("\nNO confirmado. La rama efimera queda intacta.");
    console.log(`Para descartar:  cd ${repo} && git checkout ${args.ramaDestino || bundle.manifest.rama_destino} && git branch -D ${branch}`);
    writeApplyLog(bundle, {
      version: bundle.manifest.version,
      date: bundle.manifest.date,
      applied_at: isoLocal(),
      result: "FAIL",
      failed_at_check: "user_confirmation",
      error: "Usuario respondio N en confirmacion final",
      branch,
      rama_destino: bundle.manifest.rama_destino,
      files: bundle.manifest.files.map((f) => ({
        path: f.path,
        md5_orig_expected: f.md5_orig,
        md5_mod_expected: f.md5_mod,
      })),
    });
    process.exit(0);
  }

  // Commit en la rama efimera
  git(repo, ["add", "-A"]);
  const subject = `[${bundle.manifest.version}] ${bundle.manifest.description || "patch incremental"}`;
  git(repo, ["commit", "-m", subject]);
  const commitSha = gitOut(repo, ["rev-parse", "HEAD"]);

  const logPayload = {
    version: bundle.manifest.version,
    date: bundle.manifest.date,
    applied_at: isoLocal(),
    result: "OK",
    branch,
    commit_sha: commitSha,
    rama_destino: bundle.manifest.rama_destino,
    files: bundle.manifest.files.map((f) => ({
      path: f.path,
      md5_orig_expected: f.md5_orig,
      md5_orig_actual: f.md5_orig,
      md5_mod_expected: f.md5_mod,
      md5_mod_actual: md5File(safeRepoPath(repo, f.path)),
    })),
    marker: bundle.manifest.marker,
    marker_hits_actual: post.markerHits,
    host: process.env.COMPUTERNAME || process.env.HOSTNAME || "unknown",
    agent: process.env.AGENT_NAME || "apply_bundle.mjs",
  };
  const logPath = writeApplyLog(bundle, logPayload);

  console.log("\nOK. Aplicado en rama efimera.");
  console.log(`  rama:    ${branch}`);
  console.log(`  commit:  ${commitSha}`);
  console.log(`  log:     ${logPath}`);
  console.log("");
  console.log("Comandos sugeridos para mergear (revisa antes en VS):");
  const target = args.ramaDestino || bundle.manifest.rama_destino;
  console.log(`  cd ${repo}`);
  console.log(`  git checkout ${target}`);
  console.log(`  git merge --no-ff ${branch} -m "Merge ${bundle.manifest.version} (${bundle.manifest.description || ""})"`);
  console.log(`  # opcional: git branch -d ${branch}`);
}

main().catch((e) => {
  console.error(`\nERROR FATAL: ${e.message}`);
  if (process.env.DEBUG) console.error(e.stack);
  process.exit(1);
});
