# EJC20260602_STOCK_FECHA_FASE2 — Trace regularización vs stock en fecha

Contexto: `frmRegularizarInventario` regulariza stock después de conteo cíclico.  
Objetivo: evitar desfases en `Stock en una fecha` cuando hay ajustes intermedios por lote, vencimiento o estado.

## 1) Regla de reclasificación aplicada

En `usp_Reporte_StockEnFecha_Consolidado` se amplió el set de `TipoTarea` para sumar/restar correctamente ajustes de regularización:

- Positivos: `AJCANTP`, `AJCANTPI`, `AJLOTE`, `AJLOTEPI`, `AJVENC`, `AJVENCEPI`, `CEST`, `CESTI`
- Negativos: `AJCANTN`, `AJCANTNI`, `AJLOTENI`, `AJVENCENI`
- Salidas: `DESP`, `TRAS`
- Base: `INVE`, `RECE`

Esto permite que los pares de reclasificación (ej. `AJLOTEPI` + `AJLOTENI`) queden reflejados en historia y no solo en snapshot actual.

## 2) Ajuste al SP de movimientos

En `usp_Reporte_StockEnFecha_Movimientos` se agregó:

- `MAX([Presentación]) AS [Presentación]`

Motivo: `Cargar_StockEnFecha` usa esta columna para mapear `Presentacion` del objeto de movimientos.

## 3) QA mínimo recomendado

1. Ejecutar inventario cíclico con regularización que cambie lote.
2. Ejecutar inventario cíclico con cambio de vencimiento.
3. Ejecutar inventario cíclico con cambio de estado.
4. Correr `Stock en una fecha` en rango que incluya esos ajustes.
5. Validar por SKU/lote/vence:
   - `Existencia_Al` consistente con movimientos.
   - `Existencia_Actual` consistente con `VW_Stock_Res`.
   - `Diferencia` sin desfasajes artificiales por reclasificación.

## Auditoría de mapeo `Cargar_StockEnFecha`

Fuente: `usp_Reporte_StockEnFecha_Movimientos`  
Destino: `clsBeVW_Movimientos`

Mapeo revisado:

- `Codigo` -> `Codigo`
- `Producto` -> `Producto`
- `Cantidad` -> `Cantidad`
- `EstadoOrigen` -> `EstadoOrigen`
- `EstadoDestino` -> `EstadoDestino`
- `TipoTarea` -> `TTarea`
- `IdTipoTarea` -> `TipoTarea` + `IdTipoTarea`
- `lote` -> `Lote`
- `Fecha_Vence` -> `Fecha_Vence`
- `IdPresentacion` -> `IdPresentacion`
- `IdUnidadMedida` -> `IdUnidadMedida`
- `IdEstadoOrigen` -> `IdEstadoOrigen`
- `IdProductoBodega` -> `IdProductoBodega`
- `Fecha` -> `Fecha`
- `UMBas` -> `UMBas`
- `[Presentación]`/`Presentacion` -> `Presentacion`
- `Operador` -> `Operador`

Campos no críticos para este flujo (opcionales): `barra_pallet`, `Clasificacion`, `Area_Origen`.

