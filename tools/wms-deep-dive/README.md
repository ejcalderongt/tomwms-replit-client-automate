# wms-deep-dive

Herramientas reproducibles del **Atlas BOF/HH 2023↔2028 cliente-aware**.

Generadas el 2026-04-30 como parte del deep dive Opción A.

## Qué hace

1. `parse_inventory.py` — clona los 4 checkouts de Azure DevOps (BOF dev_2023_estable,
   BOF dev_2028_merge, HH dev_2023_estable, HH dev_2028_merge) y produce un CSV
   por checkout con: hash, líneas, funciones públicas, tablas tocadas, SPs invocados,
   flags `i_nav_config_enc` referenciados.

2. `diff_and_flags.py` — consume los CSVs y emite:
   - `DIFF-BOF-2023-VS-2028.md` (added/removed/modified + TOP 30 archivos modificados)
   - `DIFF-HH-2023-VS-2028.md` (idem para mobile)
   - `flags_callsites.md` (mapa flag → archivos por checkout, con conteos y deltas)

## Cómo correrlo

Requisitos:
- `AZURE_DEVOPS_PAT` exportado (acceso a `ejcalderon0892`).
- Python 3 con stdlib (no requiere paquetes externos).
- ~3 GB libres en `/tmp` para los 4 clones.

```bash
# 1. Clonar los 4 checkouts (ajustar el ORG si difiere)
PAT="$AZURE_DEVOPS_PAT"
ORG="ejcalderon0892"
mkdir -p /tmp/wms-deep-dive/repos
cd /tmp/wms-deep-dive/repos
for combo in "TOMWMS_BOF dev_2023_estable BOF-2023" \
             "TOMWMS_BOF dev_2028_merge BOF-2028" \
             "TOMHH2025 dev_2023_estable HH-2023" \
             "TOMHH2025 dev_2028_merge HH-2028"; do
  read repo branch dest <<< "$combo"
  git clone --no-tags --depth=1 --branch "$branch" \
    "https://${PAT}@dev.azure.com/${ORG}/${repo}/_git/${repo}" "$dest"
done

# 2. Inventario (ver _summary.csv para totales)
python3 parse_inventory.py

# 3. Diffs + flags
python3 diff_and_flags.py
```

## Outputs históricos (snapshot 2026-04-30)

`_summary.csv` (este directorio) tiene los totales. Resumen rápido:

| Checkout | Archivos | Líneas | Funciones | Tablas únicas | Flag hits |
|---|---:|---:|---:|---:|---:|
| BOF-2023 | 3.731 | 1.948.569 | 47.014 | 903 | 567 |
| BOF-2028 | 4.387 | 2.115.371 | 50.715 | 948 | 694 |
| HH-2023 | 373 | 101.964 | 7.171 | 3 | 23 |
| HH-2028 | 406 | 121.670 | 7.317 | 4 | 27 |

Tiempo total de ejecución (parse + diff): ~19 segundos en sandbox de Replit
(8 cores, ProcessPoolExecutor).

## Limitaciones conocidas

- El parser de funciones VB/C#/Java es heurístico (regex). Captura la mayoría de
  funciones públicas pero puede perder algunas con signatures complejas
  (genéricos anidados, atributos multilínea).
- El detector de tablas SQL solo mira strings literales en el código. SQL
  construido dinámicamente con concatenación puede no detectarse.
- Los SPs detectados son los invocados con `EXEC` — no incluye `sp_executesql`
  con SQL inline.
- El detector de flags es substring-literal contra una lista canónica de 49
  flags. Falsos positivos posibles si un flag aparece en un comentario o
  string no relacionado. Falsos negativos si el código accede al flag por
  índice numérico de columna.

## Para extender

- Para agregar más flags: editar la lista `FLAGS` en `parse_inventory.py`.
- Para agregar más checkouts (ej. dev_2025): agregar a la lista `checkouts`
  en `parse_inventory.py:main()`.
- Para granularidad por línea: el CSV ya incluye `funcs` con `Class:Nombre`,
  `method:Nombre`, etc. Suficiente para diff funcional sin necesidad de AST.
