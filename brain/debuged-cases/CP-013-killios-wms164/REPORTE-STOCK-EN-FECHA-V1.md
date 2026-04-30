# Análisis del reporte "Stock en una fecha" — WMS164 caso BM2601

> Wave 15 · 2026-04-30 · Erik / PrograX24
> Status: **Bug del reporte confirmado en dos capas; SQL del reporte NO está en BD**

## TL;DR

1. **NO hay duplicación de devolución**. Las dos referencias que aparecen en `vw_stock_res` ubic 308 (`ReferenciaOCEnc=11350` y `=3335`) son dos OC completamente distintas de proveedores distintos:
   - **OC 1480 (Ref 11350)** = compra a `Industrias Treviso` (proveedor 47), `IdTipoIngresoOC=1`, multi-recepción del 3 al 17 dic.
   - **OC 2061 (Ref 3335)** = devolución del cliente `Imaginova` (proveedor 3672), `IdTipoIngresoOC=2`, observación textual *"DEVOLUCION DE IMAGINOVA QUE NO SE FACTURO SOLO SE REALIZA ENTREGA 10737"*, recepción única el 9-feb.
   - Producto/lotes distintos: la 1597 trae BM2511; la 2179 trae BG2511 / BG2512.

2. **El SQL del reporte "Stock en una fecha" NO existe en la base como vista ni stored procedure**. Se buscaron exhaustivamente las columnas exactas (`Inventario_Inicial`, `Ingresos`, `Ajustes_P`, `Ajustes_N`, `Salidas`, `Existencia_Al`, `Existencia_Actual`, `Diferencia`) y dieron **0 hits** en `sys.sql_modules`. Está hardcoded en el cliente VB.NET BOF (módulo `frmStockEnUnaFecha` o similar).

3. **Hay dos bugs aritméticos confirmados** en la columna `Existencia_Al` para BM2601 BUEN ESTADO. La fórmula visible (`Inv_Ini + Ingresos + Ajust_P − Ajust_N − Salidas`) NO se cumple ni con sus propios valores ni con los movs reales de la BD.

---

## Comparación de las 2 OC (responde la sospecha de duplicado)

| Campo | OC 1480 (Ref `11350`) | OC 2061 (Ref `3335`) |
|---|---|---|
| IdProveedorBodega | 47 — `INDUSTRIAS TREVISO, S.A.` | 3672 — `IMAGINOVA, S.A.` |
| IdTipoIngresoOC | 1 (compra normal) | 2 (devolución cliente) |
| No_Documento | 10524 | 3335 |
| Fecha_Creacion | 2025-12-03 10:47 | 2026-02-09 10:44 |
| Observación | (vacío) | DEVOLUCION DE IMAGINOVA QUE NO SE FACTURO SOLO SE REALIZA ENTREGA 10737 |
| no_documento_recepcion_erp | 7500 | 698 |
| Recepciones físicas (`trans_re_oc`) | 8 IdRecepcionEnc, multi-día | 1 IdRecepcionEnc (2179) |

**Conclusión negocio**: son dos eventos físicos independientes. El operador **no duplicó** la devolución.

## Lo único que de la OC 1480 ingresó WMS164

De las 8 recepciones de Treviso, **solo la 1597 (`ENV-WMS20251205` del 4-dic)** trae líneas WMS164:

| Rec | No_Linea | Estado recepción | Cant | LP |
|---|---|---|---|---|
| 1597 | 31 | **Reempacar** | 60 | FU04568 |
| 1597 | 31 | **Destruir** | 60 | FU04576 |
| 1597 | 31 | **Reempacar** | 60 | FU04578 |
| 1597 | 31 | **Reempacar** | 60 | FU04584 |
| 1597 | 31 | **Reempacar** | 60 | FU04594 |

Lote único: BM2511. Ninguna línea entró como `Buen Estado` directo. Esto explica por qué hay tantos cambios de estado posteriores en este lote.

## Lo único que de la OC 2061 ingresó WMS164

| Rec | No_Linea | Estado | Cant | LP | Lote |
|---|---|---|---|---|---|
| 2179 | 1 | Buen Estado | 62 | FU06688 | **BG2512** |
| 2179 | 1 | Buen Estado | 3 | FU06689 | **BG2511** |

⚠️ Lotes BG (no BM). Diferentes a la compra original. Probable lote de Imaginova devuelto.

---

## Reproducción de la fórmula del reporte para BM2601 BUEN ESTADO

**Datos visibles en el reporte (captura)**: `Inv_Ini=0  Ingresos=655  Ajust_P=0  Ajust_N=5  Salidas=668  Exist_Al=-14  Exist_Act=0  Diferencia=-14`

**Datos reales en `trans_movimientos` para BM2601 IdEstado=1 (UM Bas)**:

| Tipo | IdTipoTarea | UM Bas | Cajas (÷5) | Notas |
|---|---|---|---|---|
| RECE | 1 | 3299 | **659.8** | recepciones acumuladas |
| DESP+PIK | 5+8 | 6704 (interno) | 1340.8 | mov internos del proceso de despacho |
| VERI | 11 | 1801 (interno) | 360.2 | mov internos de verificación |
| UBIC | 2 | 4040 (interno) | 808 | mov internos de reubicación |
| CEST sale BUEN | 3 | **17 (sale)** | 3.4 | 4 cambios BUEN→MAL: 4+4+4+5 |
| AJCANTN | 17 | 0 BUEN, 9 MAL | 0 / 1.8 | ajustes en MAL ESTADO |
| EXPLOSION | 20 | 10 (interno) | 2 | desarmado de pallet |

**Lo que NO cuadra con la captura del reporte**:

| Reporte | Calculado de movs | Diferencia | Hipótesis |
|---|---|---|---|
| Ingresos = 655 cajas | RECE_buen / 5 = **659.8** | -4.8 cajas | El reporte resta CEST en Ingresos en vez de sumarlo en Salidas |
| Salidas = 668 cajas | DESP/PIK/VERI/UBIC son **internos** del mismo estado, no debería contarlos como salida | El despacho real no está separado en BD | El reporte cuenta TODOS los movs de tipo 5/8/11 como salida sin filtrar por `IdUbicacionDestino` real |
| Ajust_N = 5 cajas | AJCANTN_buen=0, EXPLOSION_buen=10 (interno) | El "5" NO viene de AJCANTN ni EXPLOSION en BUEN | Probablemente cuenta el último CEST (M367884 del 27-mar de 5 cajas) como ajuste negativo |
| Existencia_Al = -14 | 0+655+0-5-668 = **-18**, no -14 | -4 | Aritmética interna del reporte ya inconsistente con sus propios valores |

**Existencia teórica vs real**:
- Fórmula del reporte usando sus propios valores: 0+655+0-5-668 = **-18**
- Reporte muestra: -14
- Real en BD MAL ESTADO de BM2601: **8 cajas** (17 entradas CEST − 9 AJCANTN)
- Real en BD MAL ESTADO mostrado en `Resumen Existencias`: **14 cajas**
- Diferencia adicional 14-8 = **6 cajas en MAL sin trazabilidad en `trans_movimientos`** (segundo bug, posiblemente del mismo patrón de filas fantasma de la wave 13-14).

## Tipología de movimientos del WMS164 BM2601

Importante: cada paso del proceso de despacho (DESP → PIK → VERI → UBIC) genera **un mov separado** en `trans_movimientos`, todos con `IdUbicacionOrigen = IdUbicacionDestino` y `IdEstadoOrigen = IdEstadoDestino`. Estos son **movimientos de proceso** (auditoría de qué hizo el operador), NO salidas reales del lote.

Si el reporte cuenta esos movs como "Salida", **infla artificialmente las salidas y obliga al `Existencia_Al` a dar negativo**. Esa parece ser la causa raíz del `-43.60` global del reporte WMS164.

## Próximos pasos propuestos

1. **Necesitamos el código fuente del cliente VB.NET BOF** del formulario "Stock en una fecha" (`frmStockEnUnaFecha` o similar) para ver el SQL exacto que arma. La búsqueda en `sys.sql_modules` confirmó que NO está en BD.
2. Una vez que tengamos el código, podremos:
   - Identificar el filtro `WHERE` que separa "ingresos" vs "salidas" vs "ajustes".
   - Confirmar si filtra por `IdTipoTarea` o por `IdUbicacionOrigen<>IdUbicacionDestino` o por algún otro criterio.
   - Proponer la fix puntual.
3. Pregunta abierta para el operador: el lote BM2601 tiene 4 cambios de estado BUEN→MAL hechos por usuario 31 entre 13-feb y 27-mar. ¿Sabe quién es ese operador y qué motivo se asentó?

## Anexo — IDs verificados

- `IdProductoBodega` WMS164 en bodega 1: **381**
- Ubicación con stock duplicado: **308** (`R02 - C2 - N2 - B - #308`)
- IdEstado: 1=BUEN ESTADO, 16=MAL ESTADO
- IdTipoTarea: 1=RECE, 2=UBIC, 3=CEST (cambio estado), 5=DESP, 8=PIK, 11=VERI, 17=AJCANTN, 20=EXPLOSION
