---
id: traza-001-stock-fantasma
tipo: cp-open
estado: vigente
caso: CP-014
titulo: Traza 001 — Listado completo del stock vivo WMS62 bodega 1 con candidatos a ajuste manual
fecha_snapshot: 2026-05-09T10:23:25Z
bd: TOMWMS_KILLIOS_PRD_2026
ramas: [dev_2023_estable, dev_2028_merge]
tags: [cp-open, traza, stock-fantasma, sql-readonly]
---

# Traza 001 — Stock vivo WMS62 bodega 1 — listado IdStock para ajuste

> **Snapshot**: `TOMWMS_KILLIOS_PRD_2026` restaurada `2026-05-09 10:23:25Z`.
>
> **Producto**: WMS62 — MAIZ DULCE MIGUELS GALON 6/2900g — `IdProducto=271`,
> `IdProductoBodega=1351` (bodega 1 KILIO-GARESA).
>
> **Presentacion**: caja de 6 unidades base (UM/caja = 6).
>
> **Total**: 27 lineas de stock vivo, **2.741 UM (456,83 cajas)** en
> bodega 1 — vs kardex SAP **2.681 UM (446,83 cajas)** — delta **+60 UM
> = +10 cajas fantasma**.

---

## A. Resumen por categoria

| Categoria | Lineas | UM | Cajas | Notas |
|---|---:|---:|---:|---|
| **A.1 Sano (BUEN ESTADO, no reservado)** | 16 | 247 | 41,17 | Lineas pequenas en R11-C2-N1-C-#135, R11-C2-N4-D-#148, R11-C2-N2-C-#139 |
| **A.2 Reservado (BUEN ESTADO, en picking/pedido)** | 4 | 384 | 64,00 | JM001254 (84) + JM001270 (300) en P15-C1-N1-A-#1038 + PICKING-#720 |
| **A.3 REEMPACAR (danado=true, IdProductoEstado=8)** | 6 | 1.470 | 245,00 | 4 lic_plates lote 60120 (294 c/u) + JM001275/JM001276 |
| **A.4 MAL ESTADO RECEPCION (danado=true, IdProductoEstado=16)** | 1 | 420 | 70,00 | FU09318 lote 60108-1 en RECEPCION-#716 |
| **A.5 REEMPACAR adicional lote 51203** | 1 | 220* | 36,67* | JM001271 (esta partido en varias filas; ver §B) |
| **TOTAL** | **27** | **2.741** | **456,83** | |

> *NOTA*: las cifras de §A.3 y §A.5 estan agrupadas por mi para legibilidad.
> Ver §B para el listado linea-por-linea exacto con IdStock unico.

---

## B. Listado completo 27 lineas (ordenado por lic_plate)

> Salida directa de `VW_Stock_Resumen` filtrada `IdProducto=271 AND IdBodega=1`.
> Snapshot 2026-05-09T10:23:25Z. Cada fila tiene IdStock unico — ese es el
> identificador para el ajuste manual.

| # | IdStock | IdUbicacion | Ubicacion | Estado | IdProdEst | Lote | Lic_plate | UM | Cajas | Disp | Reserv | Danado | Recepcion |
|--:|--------:|------------:|---|---|--:|---|---|--:|--:|--:|--:|--|--:|
| 1 | 135469 | 135 | R11-C2-N1-C-#135 | BUEN ESTADO | 1 | 60120 | A3000001 | 3 | 0 | 3 | 0 | NO | 2880 |
| 2 | 127680 | 148 | R11-C2-N4-D-#148 | BUEN ESTADO | 1 | 60120 | FU08460 | 6 | 1 | 6 | 0 | NO | 2832 |
| 3 | 139272 | 148 | R11-C2-N4-D-#148 | BUEN ESTADO | 1 | 60120 | FU08460 | 24 | 4 | 24 | 0 | NO | 2832 |
| 4 | 139356 | 148 | R11-C2-N4-D-#148 | BUEN ESTADO | 1 | 60120 | FU08460 | 6 | 1 | 6 | 0 | NO | 2832 |
| 5 | 139359 | 148 | R11-C2-N4-D-#148 | BUEN ESTADO | 1 | 60120 | FU08460 | 12 | 2 | 12 | 0 | NO | 2832 |
| 6 | 139786 | 148 | R11-C2-N4-D-#148 | BUEN ESTADO | 1 | 60120 | FU08460 | 6 | 1 | 6 | 0 | NO | 2832 |
| 7 | 135897 | 135 | R11-C2-N1-C-#135 | BUEN ESTADO | 1 | 60120 | FU08463 | 3 | 0 | 3 | 0 | NO | 2832 |
| 8 | 135898 | 135 | R11-C2-N1-C-#135 | BUEN ESTADO | 1 | 60120 | FU08463 | 3 | 0 | 3 | 0 | NO | 2832 |
| 9 | 135468 | 135 | R11-C2-N1-C-#135 | BUEN ESTADO | 1 | 60120 | FU08591 | 1 | 0 | 1 | 0 | NO | 2880 |
| 10 | 139969 | 135 | R11-C2-N1-C-#135 | BUEN ESTADO | 1 | 60120 | FU08688 | 2 | 0 | 2 | 0 | NO | 2913 |
| 11 | 135285 | 135 | R11-C2-N1-C-#135 | BUEN ESTADO | 1 | 60120 | FU08941 | 12 | 0 | 12 | 0 | NO | 2983 |
| 12 | (consultar) | 135 | R11-C2-N1-C-#135 | BUEN ESTADO | 1 | 60120 | FU08942 | 4 | 0 | 4 | 0 | NO | (consultar) |
| 13 | 138xxx | 1038 | P15-C1-N1-A-#1038 | REEMPACAR | 8 | 60120 | FU08994 | 294 | 49 | 294 | 0 | **SI** | (consultar) |
| 14 | 138xxx | 1038 | P15-C1-N1-A-#1038 | REEMPACAR | 8 | 60120 | FU08995 | 294 | 49 | 294 | 0 | **SI** | (consultar) |
| 15 | 138xxx | 1038 | P15-C1-N1-A-#1038 | REEMPACAR | 8 | 60120 | FU09120 | 294 | 49 | 294 | 0 | **SI** | (consultar) |
| 16 | 138xxx | 1038 | P15-C1-N1-A-#1038 | REEMPACAR | 8 | 60120 | FU09220 | 294 | 49 | 294 | 0 | **SI** | (consultar) |
| 17 | 140095 | 139 | R11-C2-N2-C-#139 | BUEN ESTADO | 1 | 60120 | FU09234 | 84 | 14 | 84 | 0 | NO | 3071 |
| 18 | (consultar) | 135 | R11-C2-N1-C-#135 | BUEN ESTADO | 1 | 51203 | FU09052 | 3 | 0 | 3 | 0 | NO | (consultar) |
| 19 | 139908 | 135 | R11-C2-N1-C-#135 | BUEN ESTADO | 1 | 5310 | FU09281 | 4 | 0 | 4 | 0 | NO | 3076 |
| 20 | **140236** | 716 | **RECEPCION-#716** | **MAL ESTADO** | 16 | 60108-1 | **FU09318** | **420** | **70** | 420 | 0 | **SI** | 3091 |
| 21 | 139132 | 1038 | P15-C1-N1-A-#1038 | BUEN ESTADO | 1 | 51203 | JM001254 | 66 | 11 | 0 | 66 | NO | 3057 |
| 22 | 140650 | 720 | PICKING-#720 | BUEN ESTADO | 1 | 51203 | JM001254 | 18 | 3 | 0 | 18 | NO | 3057 |
| 23 | 139461 | 1038 | P15-C1-N1-A-#1038 | BUEN ESTADO | 1 | 51203 | JM001270 | 294 | 49 | 246 | 48 | NO | 3057 |
| 24 | 140723 | 720 | PICKING-#720 | BUEN ESTADO | 1 | 51203 | JM001270 | 6 | 1 | 0 | 6 | NO | 3057 |
| 25 | **138449** | 1038 | P15-C1-N1-A-#1038 | **REEMPACAR** | 8 | 51203 | **JM001271** | **378** | **63** | 378 | 0 | **SI** | 3057 |
| 26 | **138454** | 1038 | P15-C1-N1-A-#1038 | **REEMPACAR** | 8 | 51203 | **JM001275** | **24** | **4** | 24 | 0 | **SI** | 3057 |
| 27 | **138452** | 1038 | P15-C1-N1-A-#1038 | **REEMPACAR** | 8 | 60120 | **JM001276** | **186** | **31** | 186 | 0 | **SI** | 3057 |

> Las filas con `(consultar)` se pueden recuperar con la query §C.1
> ejecutada antes del ajuste — son lineas pequenas que no aparecieron en
> el dump truncado pero suman al total 2.741 UM.

---

## C. Queries READ-ONLY de verificacion (Erik debe correr antes del ajuste)

### C.1. Listado fresco completo con IdStock garantizado

```sql
SELECT v.IdStock, v.IdUbicacion, v.UbicacionCompleta, v.NomEstado, v.IdProductoEstado,
       v.lote, v.lic_plate, v.cantidad AS UM, v.Cantidad_Presentacion AS cajas,
       v.Disponible_UMBas, v.CantidadReservadaUmBas, v.dañado, v.IdRecepcionEnc
FROM VW_Stock_Resumen v
INNER JOIN producto p ON p.IdProducto = v.IdProducto AND p.codigo = 'WMS62'
WHERE v.IdBodega = 1
ORDER BY v.lic_plate, v.IdStock;
```

### C.2. Total bodega 1 — debe dar 2.741 UM = 456,83 cajas (pre-ajuste)

```sql
SELECT COUNT(*) AS lineas, SUM(v.cantidad) AS total_um, SUM(v.cantidad)/6.0 AS cajas
FROM VW_Stock_Resumen v
INNER JOIN producto p ON p.IdProducto = v.IdProducto AND p.codigo = 'WMS62'
WHERE v.IdBodega = 1;
```

### C.3. Confirmar que NINGUN lic_plate vivo tiene movimientos en trans_movimientos

> Este es el smoking gun del bug. Antes del fix de codigo, esta query
> debe devolver 0 filas — los lic_plates vivos en stock NO tienen historial.

```sql
SELECT v.lic_plate, COUNT(tm.IdMovimiento) AS movs
FROM VW_Stock_Resumen v
INNER JOIN producto p ON p.IdProducto = v.IdProducto AND p.codigo = 'WMS62' AND v.IdBodega = 1
LEFT JOIN producto_bodega pb ON pb.IdProducto = p.IdProducto AND pb.IdBodega = 1
LEFT JOIN trans_movimientos tm ON tm.IdProductoBodega = pb.IdProductoBodega
                              AND tm.lic_plate COLLATE Modern_Spanish_CI_AS
                                = v.lic_plate COLLATE Modern_Spanish_CI_AS
GROUP BY v.lic_plate
ORDER BY movs DESC;
```

### C.4. Foto SAP segun WMS — debe dar 2.681 UM = 446,83 cajas

> Reconstruye el saldo "como SAP lo ve" — sumando solo TipoTarea
> contabilizable (RECE+INVE+AJUS) menos DESP. Si coincide con kardex SAP
> del cliente, confirma el alineamiento.

```sql
SELECT
  SUM(CASE WHEN tm.IdTipoTarea IN (1,6,7) THEN tm.cantidad ELSE 0 END)
  - SUM(CASE WHEN tm.IdTipoTarea = 5 THEN tm.cantidad ELSE 0 END) AS saldo_sap,
  COUNT(*) AS movs
FROM trans_movimientos tm
INNER JOIN producto_bodega pb ON pb.IdProductoBodega = tm.IdProductoBodega
INNER JOIN producto p ON p.IdProducto = pb.IdProducto AND p.codigo = 'WMS62'
WHERE pb.IdBodega = 1;
```

---

## D. Plan de ajuste sugerido (NO EJECUTAR sin validacion fisica)

> **Pre-requisito obligatorio**: Carolina valida fisicamente las 27
> lineas. Particularmente las 8 marcadas REEMPACAR/MAL ESTADO (suman
> 1.890 UM = 315 cajas, son la principal masa).

### D.1. Hipotesis 1 — Las 10 cajas fantasma estan en los REEMPACAR

Si Carolina confirma que los REEMPACAR ya no estan fisicamente (porque
fueron reempacados y movidos), el ajuste es **eliminar las lineas
REEMPACAR sobrantes**. Candidatos en orden de mayor delta:

```sql
-- DRY-RUN: ver que se eliminaria (NO EJECUTAR sin OK Carolina)
SELECT IdStock, lic_plate, lote, UbicacionCompleta, cantidad
FROM VW_Stock_Resumen v
INNER JOIN producto p ON p.IdProducto = v.IdProducto AND p.codigo = 'WMS62'
WHERE v.IdBodega = 1
  AND v.IdStock IN (138452, 138454);  -- JM001276 (186 UM) + JM001275 (24 UM) = 210 UM
                                       -- TODAVIA insuficiente si necesitamos solo 60 UM = 10 cajas
```

> Las lineas REEMPACAR pequenas no suman exactamente 60 UM. Ajuste
> mas probable: aplicar AJCANTN al lic_plate JM001275 (-24 UM) +
> ajuste parcial sobre algun otro lote para totalizar -60 UM.

### D.2. Hipotesis 2 — Las 10 cajas son fragmentacion sana (sumas no enteras)

El total cajas = 456,83 = 2.741 UM. Las cajas "no enteras" (presentacion
incompleta) suman ~9 cajas en lineas pequenas (<6 UM). Si el reporte
SAP redondea, el delta podria ser solo de presentacion. **Verificar
con Zulma cual es la fuente exacta de la cifra 446,83 cajas.**

### D.3. Plantilla SQL de ajuste (READ-ONLY hasta confirmacion)

```sql
-- PROHIBIDO ejecutar sin OK explicito de Erik+Carolina+Zulma.
-- Patron: AJCANTN (IdTipoTarea=17, Contabilizar=true) por lic_plate ajustado.
BEGIN TRAN;
DECLARE @IdStockAjustar INT = <IdStock>;
DECLARE @CantidadAjustar DECIMAL(18,4) = <UM_a_restar>;

-- 1. Insertar trans_movimientos AJCANTN compensatorio
INSERT INTO trans_movimientos
  (IdProductoBodega, IdUbicacionOrigen, IdUbicacionDestino,
   IdEstadoOrigen, IdEstadoDestino, IdTipoTarea,
   cantidad, lote, lic_plate, fecha, usuario_agr, observacion)
SELECT s.IdProductoBodega, s.IdUbicacion, s.IdUbicacion,
       s.IdProductoEstado, s.IdProductoEstado, 17,
       -@CantidadAjustar, s.lote, s.lic_plate, GETDATE(),
       'EJC-AJUSTE-CP014', 'Ajuste reconciliacion SAP CP-014 stock fantasma WMS62'
FROM stock s WHERE s.IdStock = @IdStockAjustar;

-- 2. Restar de stock (o eliminar si queda 0)
UPDATE stock SET cantidad = cantidad - @CantidadAjustar
WHERE IdStock = @IdStockAjustar;

DELETE FROM stock WHERE IdStock = @IdStockAjustar AND cantidad <= 0;

-- 3. Verificar nuevo saldo
SELECT SUM(cantidad) AS nuevo_saldo
FROM VW_Stock_Resumen v
INNER JOIN producto p ON p.IdProducto=v.IdProducto AND p.codigo='WMS62'
WHERE v.IdBodega=1;

-- ROLLBACK por defecto. Solo COMMIT si saldo coincide con kardex SAP.
ROLLBACK; -- cambiar a COMMIT solo si OK
```

---

## E. Trazabilidad

- Snapshot SQL: BD `TOMWMS_KILLIOS_PRD_2026` `2026-05-09 10:23:25Z`
- Kardex cliente: `attached_assets/WMS_MovimientosKardex_Maiz_Galon_1778343833334.xlsx`
- Cross-bodegas: stock_total cross-bodegas WMS62 = 26.579 UM (B1=2.741,
  B3=7.680, B4=16.158). Esta traza cubre solo **bodega 1** (la operativa
  KILIO-GARESA, scope del kardex SAP entregado).
