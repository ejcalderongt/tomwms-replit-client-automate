# Parametrización por cliente

Modelo de cómo cambia el comportamiento del WMS según los flags configurados en cada BD cliente.

## Filosofía

El WMS es **multi-cliente con código único**. Las diferencias de comportamiento entre Killios, Becofarma, BYB, Cealsa, Mampa NO viven en el código sino en **flags bit** de los maestros (`cliente`, `producto`, `bodega`, `ajuste_tipo`).

> **Hallazgo clave (2026-04-27)**: NO existen tablas `config_*` ni `parametros` en Killios. La parametrización está **embebida en los maestros como columnas bit**.

## Densidad de configuración

| Maestro | # flags bit | Detalle |
|---|---:|---|
| `bodega` | ~57 | El maestro más configurable. Ver `flags-bodega`. |
| `producto` | 17 | Ver `flags-producto`. |
| `cliente` | 9 | Ver `flags-cliente`. |
| `ajuste_tipo` | 5 | Ver `flags-ajuste-tipo`. Incluye typo `momdifica_vencimiento`. |

## Matrices por cliente

| Cliente | BD | Estado | Matriz |
|---|---|---|---|
| Killios | `TOMWMS_KILLIOS_PRD` | ✅ Validado 2026-04-27 | [matriz-killios.md](./matriz-killios.md) |
| Becofarma | (TBD) | ⏳ Sin acceso aún | (placeholder) |
| BYB | `IMS4MB_BYB_PRD` | ⏳ Tiene acceso, sin extracción | (placeholder) |
| Cealsa | `IMS4MB_CEALSA_QAS` | ⏳ QA solamente | (placeholder) |
| La Cumbre | (TBD) | ⏳ — | (placeholder) |
| Mampa (MHS) | (TBD) | ⏳ — | (placeholder) |

Cuando se completen ≥2 matrices, se publica `diff-cross-cliente.md` con qué cambia.

## Cómo se usa esto desde `wms-brain`

Cuando una entity de `wms-brain` (caso, módulo, regla) depende de un comportamiento **configurable**, debe linkear:

```yaml
db_brain_refs:
  - db-brain://parametrizacion/flags-producto#control_lote
```

Y la matriz por cliente le dice si el flag está prendido o apagado en cada cliente productivo.
