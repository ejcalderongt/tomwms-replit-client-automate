# Matriz de compatibilidad escenario × cliente

> Auto-generada por `compute-matrix.cjs` a partir de `clients/*.yaml` y los
> `requires_config` de cada `reservation/RES-*.yaml`.
> Estado: **todos los clientes están `learned: false`** → todas las celdas son
> `unknown`. Una vez ejecutado `learn-config` contra Killios PRD (rama
> dev_2023_merge), las celdas se resuelven a `OK` / `N/A` / `INVALID`.

## Leyenda

- `OK`: el cliente cumple los `requires_config` del escenario; debe correrse y validarse.
- `N/A`: el cliente NO cumple los `requires_config`; el escenario no aplica.
- `unknown`: faltan datos del cliente o del escenario; no se puede decidir.
- `INVALID`: el escenario tiene `requires_config` contradictorios; revisar YAML.

## Hipótesis previas (a confirmar con learn-config)

| Cliente | Hipótesis dominante |
|---|---|
| IDEALSA | Control_vencimiento + FEFO + Explosion_Automatica desde pick |
| BYB | Documentos de traslado, explosión desconocida |
| KILLIOS | Por confirmar (this is the source of truth) |
| LA_CUMBRE | Similar a IDEALSA, sin Conservar_Zona_Picking_Clavaud |

## Matriz

| Escenario | IDEALSA | BYB | KILLIOS | LA_CUMBRE |
|---|---|---|---|---|
| RES-001 | unknown | unknown | unknown | unknown |
| RES-006 | unknown | unknown | unknown | unknown |
| RES-007 | unknown | unknown | unknown | unknown |
| RES-008 | unknown | unknown | unknown | unknown |
| RES-009 | unknown | unknown | unknown | unknown |
| RES-010 | unknown | unknown | unknown | unknown |
| RES-011 | unknown | unknown | unknown | unknown |
| RES-012 | unknown | unknown | unknown | unknown |
| RES-013 | unknown | unknown | unknown | unknown |
| RES-014 | unknown | unknown | unknown | unknown |
| RES-015 | unknown | unknown | unknown | unknown |
| RES-016 | unknown | unknown | unknown | unknown |
| RES-017 | unknown | unknown | unknown | unknown |
| RES-018 | unknown | unknown | unknown | unknown |
| RES-019 | unknown | unknown | unknown | unknown |
| RES-020 | unknown | unknown | unknown | unknown |
| RES-021 | unknown | unknown | unknown | unknown |
| RES-022 | unknown | unknown | unknown | unknown |
| RES-023 | unknown | unknown | unknown | unknown |
| RES-024 | unknown | unknown | unknown | unknown |
| RES-DIN | unknown | unknown | unknown | unknown |

## Próximo paso para hacer la matriz útil

1. Ejecutar `brain/skills/wms-test-bridge/sql/learn-config.sql` contra Killios PRD
   con cada bodega relevante.
2. Volcar el resultado en `clients/killios.yaml` bajo `flags:` y poner
   `learned: true`, `learned_from.branch: dev_2023_merge`.
3. Re-correr `compute-matrix.cjs` para regenerar este archivo.
4. Repetir para los otros clientes cuando se tenga acceso o se obtengan dumps.
