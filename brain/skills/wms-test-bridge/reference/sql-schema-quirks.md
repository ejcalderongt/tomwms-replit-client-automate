# Quirks del schema SQL Server (deuda historica)

## 1. Typo en columna de explosion automatica

La tabla `i_nav_config_enc` tiene **dos columnas casi-iguales**:

| Columna | Tipo | Nombre con typo? |
|---|---|---|
| `explosio_automatica_nivel_max` | int | SI (falta la "n") |
| `explosion_automatica_nivel_max` | int | NO |

En Killios prod tienen valores **divergentes**: typo=`1`, correcto=`-1`.

**Pendiente**: definir cual es la fuente de verdad y deprecar la otra. Hasta ahora no esta documentado en el codigo VB cual lee. Riesgo: cambios en una columna no afectan el comportamiento si el motor lee la otra.

**Accion sugerida**: mini-ADR para fijar la columna correcta y migrar valores.

## 2. VW_Configuracioninv NO contiene los flags

La vista `VW_Configuracioninv` solo expone 8 columnas (metadatos). Los 69 flags reales viven en la tabla base `i_nav_config_enc`. Cualquier herramienta de aprendizaje **debe** apuntar a la tabla, no a la vista.

## 3. Bodega de facturacion virtual

Killios tiene `bodega_facturacion = "BOD7"` pero `BOD7` no aparece como bodega operativa en `bodega` (solo BOD1, PRTOK, PRTO, BOD5, PRTK17, PRT17). `BOD7` es contable, no fisica.

## 4. Pares de bodega de prorrateo

`bodega_prorrateo` y `bodega_prorrateo1` se usan **cruzados** entre Amatitlan y Z17:

| Bodega | bodega_prorrateo | bodega_prorrateo1 |
|---|---|---|
| BOD1 (principal Amatitlan) | PRT17 | PRTK17 |
| PRTOK (prorrateo Kilio) | PRTO | PRTOK |
| PRTO (prorrateo Garesa) | PRT17 (asumido) | ... |
| BOD5 (Amatitlan sucursal) | ... | ... |
| PRTK17 (prorrateo Kilio Z17) | ... | ... |
| PRT17 (prorrateo Garesa Z17) | ... | ... |

Esto modela el flujo: **compra → prorrateo (costeo SAP) → traslado a operativa**, con dos entidades legales (Kilio/Garesa) compartiendo bodegas fisicas.

## 5. Whitelist de prefijos SQL en assert_read_only

`brain/wms-agent/wmsa/killios.py` permite: `SELECT, WITH, EXEC, EXECUTE, SET, DECLARE, PRINT`. Bloquea patrones internos como `EXEC sp_executesql` con texto mutante. Documentar la lista exacta de SPs permitidos via EXEC para evitar ambiguedad.
