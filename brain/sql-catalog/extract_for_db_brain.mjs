#!/usr/bin/env node
// Extractor full del catálogo SQL → markdown idempotente para db-brain.
// Versionado en brain/sql-catalog/ del branch wms-brain.
//
// Uso:
//   node extract_for_db_brain.mjs --out ./db-brain
//
// Variables de entorno requeridas:
//   WMS_KILLIOS_DB_PASSWORD
//
// Variables opcionales (con defaults):
//   WMS_KILLIOS_DB_HOST=52.41.114.122
//   WMS_KILLIOS_DB_PORT=1437
//   WMS_KILLIOS_DB_USER=sa
//   WMS_KILLIOS_DB_NAME=TOMWMS_KILLIOS_PRD
//
// Genera (idempotente, ordenado por nombre):
//   <out>/tables/<name>.md     · 1 por cada USER_TABLE
//   <out>/views/<name>.md      · 1 por cada VIEW
//   <out>/sps/<name>.md        · 1 por cada SQL_STORED_PROCEDURE
//   <out>/functions/<name>.md  · 1 por cada función (FN/IF/TF)
//   <out>/_meta/extracted_at.txt
//   <out>/_meta/stats.md
//   <out>/tables/_index.md, views/_index.md, sps/_index.md, functions/_index.md
//
// Preserva (NO toca) <out>/parametrizacion/*, <out>/dependencias/*, <out>/README.md.

import sql from "mssql";
import { writeFileSync, mkdirSync } from "node:fs";
import { join } from "node:path";

const args = process.argv.slice(2);
const outIdx = args.indexOf("--out");
const OUT = outIdx >= 0 ? args[outIdx + 1] : "./db-brain";

const HOST = process.env.WMS_KILLIOS_DB_HOST || "52.41.114.122";
const PORT = parseInt(process.env.WMS_KILLIOS_DB_PORT || "1437", 10);
const USER = process.env.WMS_KILLIOS_DB_USER || "sa";
const NAME = process.env.WMS_KILLIOS_DB_NAME || "TOMWMS_KILLIOS_PRD";
const PWD = process.env.WMS_KILLIOS_DB_PASSWORD;
if (!PWD) {
  console.error("ERROR: WMS_KILLIOS_DB_PASSWORD env var required");
  process.exit(2);
}

const NOW = new Date().toISOString();
const NOW_DATE = NOW.slice(0, 10);

console.log(`extract_for_db_brain v1.0`);
console.log(`Output: ${OUT}`);
console.log(`DB:     ${USER}@${HOST}:${PORT}/${NAME}`);

const pool = await sql.connect({
  server: HOST, port: PORT, user: USER, password: PWD, database: NAME,
  options: { encrypt: false, trustServerCertificate: true },
  connectionTimeout: 15000, requestTimeout: 180000,
});

async function q(s) { return (await pool.request().query(s)).recordset; }

console.log("\nExtracting...");

console.log("  objects...");
const objects = await q(`
  SELECT s.name AS schema_name, o.name, o.object_id,
    LTRIM(RTRIM(o.type)) AS type, o.type_desc,
    CONVERT(varchar(33), o.create_date, 126) AS create_date,
    CONVERT(varchar(33), o.modify_date, 126) AS modify_date,
    CASE WHEN o.type = 'U'
      THEN (SELECT TOP 1 SUM(p.rows) FROM sys.partitions p
        WHERE p.object_id = o.object_id AND p.index_id IN (0,1))
      ELSE NULL END AS row_count
  FROM sys.objects o JOIN sys.schemas s ON s.schema_id = o.schema_id
  WHERE o.is_ms_shipped = 0 AND o.type IN ('U','V','P','FN','IF','TF')
  ORDER BY s.name, o.name`);
console.log(`    ${objects.length} objects`);

console.log("  columns...");
const allCols = await q(`
  SELECT c.object_id, c.column_id AS ord, c.name, ty.name AS tipo,
    c.max_length AS len, c.is_nullable AS nullable,
    c.is_identity AS ident, c.is_computed AS computed,
    OBJECT_DEFINITION(c.default_object_id) AS default_def
  FROM sys.columns c JOIN sys.types ty ON ty.user_type_id = c.user_type_id
  ORDER BY c.object_id, c.column_id`);
console.log(`    ${allCols.length} columns`);

console.log("  indexes...");
const allIdx = await q(`
  SELECT i.object_id, i.index_id, i.name AS idx, i.type_desc,
    i.is_primary_key AS is_pk, i.is_unique AS is_uniq,
    STRING_AGG(c.name, ', ') WITHIN GROUP (ORDER BY ic.key_ordinal) AS cols
  FROM sys.indexes i
  JOIN sys.index_columns ic ON ic.object_id=i.object_id AND ic.index_id=i.index_id
  JOIN sys.columns c ON c.object_id=ic.object_id AND c.column_id=ic.column_id
  WHERE i.type > 0
  GROUP BY i.object_id, i.index_id, i.name, i.type_desc, i.is_primary_key, i.is_unique`);
console.log(`    ${allIdx.length} indexes`);

console.log("  foreign keys...");
const allFks = await q(`
  SELECT name, parent_object_id, referenced_object_id,
    OBJECT_NAME(parent_object_id) AS parent_name,
    OBJECT_NAME(referenced_object_id) AS ref_name
  FROM sys.foreign_keys`);
console.log(`    ${allFks.length} FKs`);

console.log("  dependencies...");
const allDeps = await q(`
  SELECT d.referencing_id, d.referenced_entity_name AS ref_name,
    OBJECT_NAME(d.referencing_id) AS from_name,
    o_from.type_desc AS from_type
  FROM sys.sql_expression_dependencies d
  JOIN sys.objects o_from ON o_from.object_id = d.referencing_id
  WHERE o_from.is_ms_shipped = 0
    AND d.referenced_entity_name IS NOT NULL`);
console.log(`    ${allDeps.length} dependencies`);

console.log("  modules (definitions)...");
const allModules = await q(`
  SELECT m.object_id, m.definition
  FROM sys.sql_modules m
  JOIN sys.objects o ON o.object_id = m.object_id
  WHERE o.is_ms_shipped = 0 AND o.type IN ('V','P','FN','IF','TF')`);
console.log(`    ${allModules.length} modules`);

console.log("  parameters...");
const allParams = await q(`
  SELECT p.object_id, p.parameter_id AS ord, p.name,
    ty.name AS tipo, p.max_length AS len,
    p.is_output AS is_out, p.has_default_value AS has_default
  FROM sys.parameters p JOIN sys.types ty ON ty.user_type_id = p.user_type_id
  WHERE p.parameter_id > 0`);
console.log(`    ${allParams.length} parameters`);

console.log("  check constraints...");
const allChecks = await q(`
  SELECT parent_object_id, name, definition
  FROM sys.check_constraints`);
console.log(`    ${allChecks.length} check constraints`);

await pool.close();

// === Indexar ===
const colsByObj = new Map();
for (const c of allCols) {
  if (!colsByObj.has(c.object_id)) colsByObj.set(c.object_id, []);
  colsByObj.get(c.object_id).push(c);
}
const idxByObj = new Map();
for (const i of allIdx) {
  if (!idxByObj.has(i.object_id)) idxByObj.set(i.object_id, []);
  idxByObj.get(i.object_id).push(i);
}
const moduleByObj = new Map();
for (const m of allModules) moduleByObj.set(m.object_id, m.definition);
const paramsByObj = new Map();
for (const p of allParams) {
  if (!paramsByObj.has(p.object_id)) paramsByObj.set(p.object_id, []);
  paramsByObj.get(p.object_id).push(p);
}
const fkOut = new Map(), fkIn = new Map();
for (const f of allFks) {
  if (!fkOut.has(f.parent_object_id)) fkOut.set(f.parent_object_id, []);
  fkOut.get(f.parent_object_id).push(f);
  if (!fkIn.has(f.referenced_object_id)) fkIn.set(f.referenced_object_id, []);
  fkIn.get(f.referenced_object_id).push(f);
}
const checksByObj = new Map();
for (const c of allChecks) {
  if (!checksByObj.has(c.parent_object_id)) checksByObj.set(c.parent_object_id, []);
  checksByObj.get(c.parent_object_id).push(c);
}
const depsBySource = new Map();
const depsByTarget = new Map();
for (const d of allDeps) {
  if (!depsBySource.has(d.referencing_id)) depsBySource.set(d.referencing_id, []);
  depsBySource.get(d.referencing_id).push(d);
  const key = (d.ref_name || "").toLowerCase();
  if (!key) continue;
  if (!depsByTarget.has(key)) depsByTarget.set(key, []);
  depsByTarget.get(key).push({ name: d.from_name, type: d.from_type });
}

function fmtType(c) {
  if (c.tipo === "nvarchar" || c.tipo === "nchar") {
    return `${c.tipo}(${c.len === -1 ? "max" : c.len / 2})`;
  }
  if (c.tipo === "varchar" || c.tipo === "char" ||
      c.tipo === "varbinary" || c.tipo === "binary") {
    return `${c.tipo}(${c.len === -1 ? "max" : c.len})`;
  }
  return c.tipo;
}

function slugify(name) {
  return name.toLowerCase().replace(/[^a-z0-9]+/g, "-").replace(/^-|-$/g, "");
}

function dedupSorted(arr, keyFn) {
  const seen = new Set();
  const out = [];
  for (const x of arr) {
    const k = keyFn(x);
    if (seen.has(k)) continue;
    seen.add(k);
    out.push(x);
  }
  return out.sort((a, b) => keyFn(a).localeCompare(keyFn(b)));
}

// === Generar ===
console.log("\nGenerating markdown...");
let counts = { tables: 0, views: 0, sps: 0, functions: 0 };

for (const o of objects) {
  let dir, kind;
  if (o.type === "U") { dir = "tables"; kind = "table"; counts.tables++; }
  else if (o.type === "V") { dir = "views"; kind = "view"; counts.views++; }
  else if (o.type === "P") { dir = "sps"; kind = "sp"; counts.sps++; }
  else {
    dir = "functions";
    kind = o.type === "FN" ? "function-scalar"
      : (o.type === "IF" ? "function-inline" : "function-tvf");
    counts.functions++;
  }

  const cols = (colsByObj.get(o.object_id) || []).slice().sort((a, b) => a.ord - b.ord);
  const idx = idxByObj.get(o.object_id) || [];
  const fks_out = dedupSorted(fkOut.get(o.object_id) || [], f => f.name);
  const fks_in = dedupSorted(fkIn.get(o.object_id) || [], f => `${f.parent_name}.${f.name}`);
  const deps_out = dedupSorted(depsBySource.get(o.object_id) || [], d => d.ref_name || "");
  const deps_in = dedupSorted(depsByTarget.get(o.name.toLowerCase()) || [], d => d.name);
  const def = moduleByObj.get(o.object_id);
  const params = (paramsByObj.get(o.object_id) || []).slice().sort((a, b) => a.ord - b.ord);
  const checks = checksByObj.get(o.object_id) || [];

  const fm = [
    "---",
    `id: db-brain-${kind}-${slugify(o.name)}`,
    `type: db-${kind}`,
    `title: ${o.schema_name}.${o.name}`,
    `schema: ${o.schema_name}`,
    `name: ${o.name}`,
    `kind: ${kind}`,
  ];
  if (o.row_count !== null && o.row_count !== undefined) {
    fm.push(`rows: ${o.row_count}`);
  }
  fm.push(`modify_date: ${(o.modify_date || "").slice(0, 10)}`);
  fm.push(`extracted_at: ${NOW}`);
  fm.push(`extracted_from: ${NAME}`);
  fm.push("---", "");

  let md = fm.join("\n");
  md += `# \`${o.schema_name}.${o.name}\`\n\n`;

  // Header
  md += `| Atributo | Valor |\n|---|---|\n`;
  md += `| Tipo | ${o.type_desc} |\n`;
  if (o.row_count !== null && o.row_count !== undefined) {
    md += `| Filas | ${Number(o.row_count).toLocaleString("es-AR")} |\n`;
  }
  md += `| Schema modify_date | ${(o.modify_date || "—").slice(0, 10)} |\n`;
  if (cols.length > 0) md += `| Columnas | ${cols.length} |\n`;
  if (idx.length > 0) md += `| Índices | ${idx.length} |\n`;
  if (params.length > 0) md += `| Parámetros | ${params.length} |\n`;
  if (fks_out.length + fks_in.length > 0) {
    md += `| FKs | out:${fks_out.length} in:${fks_in.length} |\n`;
  }
  md += `\n`;

  // Columns
  if (cols.length > 0) {
    md += `## Columnas\n\n`;
    md += `| # | Nombre | Tipo | Null | Ident |\n|---:|---|---|:-:|:-:|\n`;
    for (const c of cols) {
      md += `| ${c.ord} | \`${c.name}\` | \`${fmtType(c)}\` | ${c.nullable ? "✓" : ""} | ${c.ident ? "✓" : ""} |\n`;
    }
    md += `\n`;
  }

  // Params
  if (params.length > 0) {
    md += `## Parámetros\n\n`;
    md += `| # | Nombre | Tipo | Out |\n|---:|---|---|:-:|\n`;
    for (const p of params) {
      md += `| ${p.ord} | \`${p.name}\` | \`${fmtType(p)}\` | ${p.is_out ? "✓" : ""} |\n`;
    }
    md += `\n`;
  }

  // Indexes
  if (idx.length > 0) {
    md += `## Índices\n\n`;
    md += `| Nombre | Tipo | Columnas |\n|---|---|---|\n`;
    for (const i of idx) {
      md += `| \`${i.idx}\` | ${i.type_desc}${i.is_pk ? " · **PK**" : ""}${i.is_uniq && !i.is_pk ? " · UNIQUE" : ""} | ${i.cols} |\n`;
    }
    md += `\n`;
  }

  // Check constraints
  if (checks.length > 0) {
    md += `## Check constraints\n\n`;
    for (const c of checks) {
      md += `- \`${c.name}\`: \`${c.definition}\`\n`;
    }
    md += `\n`;
  }

  // FKs (only tables)
  if (o.type === "U") {
    if (fks_out.length === 0 && fks_in.length === 0) {
      md += `## Foreign Keys\n\n**Sin FKs declaradas** (ni entrantes ni salientes).\n\n`;
    } else {
      md += `## Foreign Keys\n\n`;
      if (fks_out.length > 0) {
        md += `### Salientes (esta tabla → otra)\n\n`;
        for (const f of fks_out) md += `- \`${f.name}\` → \`${f.ref_name}\`\n`;
        md += `\n`;
      }
      if (fks_in.length > 0) {
        md += `### Entrantes (otra tabla → esta)\n\n`;
        for (const f of fks_in) md += `- \`${f.parent_name}\` (\`${f.name}\`)\n`;
        md += `\n`;
      }
    }
  }

  // Deps OUT (lo que consume)
  if (deps_out.length > 0) {
    md += `## Consume\n\n`;
    for (const d of deps_out.slice(0, 100)) {
      md += `- \`${d.ref_name}\`\n`;
    }
    if (deps_out.length > 100) md += `\n_... +${deps_out.length - 100} más_\n`;
    md += `\n`;
  }

  // Deps IN (quién la consume)
  if (deps_in.length > 0) {
    md += `## Quién la referencia\n\n**${deps_in.length}** objetos:\n\n`;
    for (const d of deps_in.slice(0, 100)) {
      md += `- \`${d.name}\` (${(d.type || "").replace("SQL_", "").toLowerCase()})\n`;
    }
    if (deps_in.length > 100) md += `\n_... +${deps_in.length - 100} más_\n`;
    md += `\n`;
  } else if (o.type === "U") {
    md += `## Quién la referencia\n\n_Ninguna referencia desde SPs/vistas/funciones._\n\n`;
  }

  // Definition
  if (def) {
    md += `## Definition\n\n> Sensible — no exponer fuera del brain ni a clientes externos.\n\n`;
    md += "```sql\n" + def.trim() + "\n```\n";
  }

  const file = join(OUT, dir, `${o.name}.md`);
  mkdirSync(join(OUT, dir), { recursive: true });
  writeFileSync(file, md, "utf8");
}

console.log(`  tables:    ${counts.tables}`);
console.log(`  views:     ${counts.views}`);
console.log(`  sps:       ${counts.sps}`);
console.log(`  functions: ${counts.functions}`);
console.log(`  TOTAL:     ${counts.tables + counts.views + counts.sps + counts.functions}`);

// === _index.md por carpeta ===
function writeIndex(dir, items, label) {
  mkdirSync(join(OUT, dir), { recursive: true });
  const sorted = items.slice().sort((a, b) => a.name.localeCompare(b.name));
  let md = `# Index — ${label}\n\n`;
  md += `> Snapshot: ${NOW_DATE}\n> Total: **${sorted.length}**\n\n`;
  md += `## Listado completo\n\n`;
  md += `| Nombre | ${dir === "tables" ? "Filas | " : ""}Modify date |\n`;
  md += `|---|${dir === "tables" ? "---:|" : ""}---|\n`;
  for (const o of sorted) {
    const rows = dir === "tables"
      ? ` ${o.row_count !== null && o.row_count !== undefined ? Number(o.row_count).toLocaleString("es-AR") : "—"} |`
      : "";
    md += `| [\`${o.name}\`](./${o.name}.md) |${rows} ${(o.modify_date || "—").slice(0, 10)} |\n`;
  }
  md += `\n`;
  writeFileSync(join(OUT, dir, "_index.md"), md, "utf8");
}

writeIndex("tables", objects.filter(o => o.type === "U"), "Tablas");
writeIndex("views", objects.filter(o => o.type === "V"), "Vistas");
writeIndex("sps", objects.filter(o => o.type === "P"), "Stored Procedures");
writeIndex("functions", objects.filter(o => ["FN", "IF", "TF"].includes(o.type)), "Funciones");

// === _meta/extracted_at.txt + stats.md ===
mkdirSync(join(OUT, "_meta"), { recursive: true });
writeFileSync(join(OUT, "_meta/extracted_at.txt"),
  `${NOW}\nDatabase: ${NAME}\nServer: ${HOST}:${PORT}\nUser: ${USER}\nExtractor: brain/sql-catalog/extract_for_db_brain.mjs v1.0\n`,
  "utf8");

const objsByType = {
  USER_TABLE: objects.filter(o => o.type === "U"),
  VIEW: objects.filter(o => o.type === "V"),
  SQL_STORED_PROCEDURE: objects.filter(o => o.type === "P"),
  SQL_SCALAR_FUNCTION: objects.filter(o => o.type === "FN"),
  SQL_INLINE_TABLE_VALUED_FUNCTION: objects.filter(o => o.type === "IF"),
  SQL_TABLE_VALUED_FUNCTION: objects.filter(o => o.type === "TF"),
};
const total = objects.length;
const topTables = objects
  .filter(o => o.type === "U" && o.row_count !== null && o.row_count !== undefined)
  .sort((a, b) => Number(b.row_count) - Number(a.row_count))
  .slice(0, 15);

let statsMd = `# Stats globales del catálogo ${NAME}\n\n`;
statsMd += `> Snapshot: ${NOW_DATE}\n`;
statsMd += `> Source: \`${NAME}\` @ \`${HOST},${PORT}\`\n`;
statsMd += `> Extractor: \`brain/sql-catalog/extract_for_db_brain.mjs\`\n\n`;
statsMd += `## Conteos por tipo\n\n| Tipo SQL | Cantidad |\n|---|---:|\n`;
for (const [k, v] of Object.entries(objsByType)) {
  if (v.length > 0) statsMd += `| \`${k}\` | ${v.length} |\n`;
}
statsMd += `| **TOTAL** | **${total}** |\n\n`;
statsMd += `## Top 15 tablas por filas\n\n| Tabla | Filas | Modify date |\n|---|---:|---|\n`;
for (const t of topTables) {
  statsMd += `| \`${t.name}\` | ${Number(t.row_count).toLocaleString("es-AR")} | ${(t.modify_date || "—").slice(0, 10)} |\n`;
}
statsMd += `\n## Hallazgos persistentes\n\n`;
statsMd += `- **Naming mixto**: convive \`t_*\` (heredado, ej. \`t_producto_bodega\`) con sin-prefijo (\`stock\`, \`cliente\`, \`trans_*\`).\n`;
statsMd += `- **Backups en BD**: tablas con sufijo \`_bk\` están en producción.\n`;
statsMd += `- **Snapshots puntuales**: \`stock_YYYYMMDD\` — convención de respaldo manual.\n`;
statsMd += `- **Typo en producción**: \`ajuste_tipo.momdifica_vencimiento\`. Ver \`parametrizacion/flags-ajuste-tipo\`.\n`;
statsMd += `- Total dependencias \`sys.sql_expression_dependencies\`: ${allDeps.length}.\n`;
statsMd += `- Total FKs declaradas: ${allFks.length} (la mayoría de tablas operacionales no tiene FKs).\n`;
statsMd += `- Total check constraints: ${allChecks.length}.\n`;

writeFileSync(join(OUT, "_meta/stats.md"), statsMd, "utf8");

console.log(`\nDone. Output in ${OUT}`);
