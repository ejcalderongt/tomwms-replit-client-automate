#!/usr/bin/env node
// Genera brain/test-scenarios/_matrix/compatibility.md cruzando:
//   - brain/test-scenarios/**/<scenario>.yaml  (campo requires_config:)
//   - brain/test-scenarios/_matrix/clients/*.yaml  (campo flags:)
//
// Uso:
//   node brain/test-scenarios/_matrix/compute-matrix.cjs > brain/test-scenarios/_matrix/compatibility.md
//
// Reglas:
//   - Si todos los flags requeridos por el escenario coinciden con los del cliente: OK
//   - Si al menos uno NO coincide y el valor del cliente está declarado: N/A
//   - Si al menos uno requerido NO está declarado en el cliente o el cliente tiene learned: false: unknown
//   - Si el escenario tiene flags requeridos contradictorios entre sí: INVALID
//   - Si el escenario no tiene requires_config: OK para todos

const fs = require('fs');
const path = require('path');

function walk(dir, out = []) {
  for (const e of fs.readdirSync(dir, { withFileTypes: true })) {
    const p = path.join(dir, e.name);
    if (e.isDirectory()) walk(p, out);
    else if (e.isFile() && /\.ya?ml$/i.test(e.name)) out.push(p);
  }
  return out;
}

// Parser YAML mínimo (solo lo que necesitamos: id, requires_config[], flags{})
// Para no agregar dependencias. Soporta:
//   id: VAL
//   requires_config:
//     - flag: NAME
//       value: VAL
//   flags:
//     NAME: VAL
function parseYaml(text) {
  const lines = text.split('\n');
  const out = {};
  let i = 0;
  while (i < lines.length) {
    const l = lines[i];
    if (/^\s*#/.test(l) || /^\s*$/.test(l)) { i++; continue; }
    let m = /^([A-Za-z_][A-Za-z0-9_]*)\s*:\s*(.*)$/.exec(l);
    if (m && !m[2].trim().startsWith('|')) {
      const key = m[1]; const val = m[2].trim();
      if (val === '' || val === '|' || val === '>') {
        // Bloque hijo: detectar si es lista (- ...) o map
        const kids = []; const map = {};
        let j = i + 1; let isList = false;
        while (j < lines.length && /^\s+/.test(lines[j])) {
          if (/^\s*-\s/.test(lines[j])) isList = true;
          j++;
        }
        if (isList) {
          // recolectar items que empiezan con "- "
          let cur = null;
          for (let k = i + 1; k < j; k++) {
            const ln = lines[k];
            const it = /^\s*-\s+([A-Za-z_][A-Za-z0-9_]*)\s*:\s*(.*)$/.exec(ln);
            if (it) { cur = {}; cur[it[1]] = parseScalar(it[2]); kids.push(cur); }
            else {
              const sub = /^\s+([A-Za-z_][A-Za-z0-9_]*)\s*:\s*(.*)$/.exec(ln);
              if (sub && cur) cur[sub[1]] = parseScalar(sub[2]);
            }
          }
          out[key] = kids;
        } else {
          for (let k = i + 1; k < j; k++) {
            const sub = /^\s+([A-Za-z_][A-Za-z0-9_]*)\s*:\s*(.*)$/.exec(lines[k]);
            if (sub) map[sub[1]] = parseScalar(sub[2]);
          }
          out[key] = map;
        }
        i = j; continue;
      } else {
        out[key] = parseScalar(val);
      }
    }
    i++;
  }
  return out;
}
function parseScalar(s) {
  s = s.trim();
  if (s === '') return null;
  if (s === 'null' || s === '~') return null;
  if (s === 'true') return true;
  if (s === 'false') return false;
  if (/^-?\d+$/.test(s)) return parseInt(s, 10);
  // string entre comillas
  if (/^".*"$/.test(s) || /^'.*'$/.test(s)) return s.slice(1, -1);
  return s;
}

function normaliseValue(v) {
  if (typeof v === 'string' && /^\(inferido\)/.test(v)) return null; // valores inferidos son unknown
  if (v === null || v === undefined) return null;
  return v;
}

function evaluate(scenario, client) {
  const reqs = scenario.requires_config;
  if (!reqs || !Array.isArray(reqs) || reqs.length === 0) return 'OK';
  if (client.learned === false) return 'unknown';
  // Detectar contradicciones internas
  const seen = {};
  for (const r of reqs) {
    if (r.flag in seen && seen[r.flag] !== r.value) return 'INVALID';
    seen[r.flag] = r.value;
  }
  let allOk = true;
  for (const r of reqs) {
    const cv = normaliseValue(client.flags ? client.flags[r.flag] : null);
    if (cv === null) return 'unknown';
    if (cv !== r.value) allOk = false;
  }
  return allOk ? 'OK' : 'N/A';
}

const ROOT = path.resolve(__dirname, '..');
const allYamls = walk(ROOT);
const scenarios = [];
const clients = [];
for (const f of allYamls) {
  const rel = path.relative(ROOT, f);
  const parsed = parseYaml(fs.readFileSync(f, 'utf8'));
  if (rel.startsWith('_matrix/clients/')) {
    parsed.__file = rel;
    clients.push(parsed);
  } else if (parsed.id) {
    parsed.__file = rel;
    scenarios.push(parsed);
  }
}

scenarios.sort((a, b) => a.id.localeCompare(b.id));
clients.sort((a, b) => (a.slug || '').localeCompare(b.slug || ''));

console.log('# Matriz de compatibilidad cliente × escenario');
console.log('');
console.log('> Generada por `compute-matrix.cjs`. NO editar a mano.');
console.log('');
console.log('| Escenario | Origen | ' + clients.map(c => c.display_name || c.slug).join(' | ') + ' |');
console.log('|---' + '|---'.repeat(clients.length + 1) + '|');
for (const s of scenarios) {
  const row = [`\`${s.id}\``, s.legacy_origin || '—'];
  for (const c of clients) row.push(evaluate(s, c));
  console.log('| ' + row.join(' | ') + ' |');
}

console.log('\n## Diagnóstico\n');
const learnedCount = clients.filter(c => c.learned === true).length;
console.log(`- Clientes aprendidos: **${learnedCount}/${clients.length}**`);
console.log(`- Escenarios totales: **${scenarios.length}**`);
const totalCells = scenarios.length * clients.length;
const okCells = scenarios.reduce((s, sc) => s + clients.reduce((sx, c) => sx + (evaluate(sc, c) === 'OK' ? 1 : 0), 0), 0);
const naCells = scenarios.reduce((s, sc) => s + clients.reduce((sx, c) => sx + (evaluate(sc, c) === 'N/A' ? 1 : 0), 0), 0);
const unkCells = totalCells - okCells - naCells;
console.log(`- Celdas OK: ${okCells} / N/A: ${naCells} / unknown: ${unkCells} / total: ${totalCells}`);
