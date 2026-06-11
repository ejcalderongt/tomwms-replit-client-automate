# EJC20260605 - OC Lotes: trazabilidad de impresiones MHS (modelo)

## Objetivo
En la pestaña **Lotes** de la OC, poder responder de forma inmediata:
- si para un lote ya se imprimieron etiquetas,
- cuántas se imprimieron,
- y qué licencias participaron.

Sin romper el flujo actual de impresión/licencia.

---

## Traza actual (código)

### 1) De dónde sale el grid de lotes (OC)
- Form: `Transacciones/Orden_Compra/frmOrdenCompra.vb`
- Método: `Cargar_Detalle_Lotes_OC()`
- Data source: `gBeOrdenCompra.DetalleLotes`
- Campos mostrados: cantidad, cantidad recibida, presentación, lote, licencia, vence, etc.

### 2) De dónde sale el combo de lotes en impresión OC
- Form: `Mantenimientos/Impresion_OC/frmImpresion_OC.vb`
- Método: `Cargar_oc_lotes(...)`
- DAL: `Get_Lotes_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(...)`
- Query actual (DAL): solo `trans_oc_det_lote` (`IdLote, lote, fecha_vence, cantidad, peso_licencia`).

### 3) Dónde está la trazabilidad de impresión hoy
- Tabla: `i_nav_barras_pallet`
- DAL ya expone:
  - `cant_etiquetas_presentacion_impresas`
  - `Impreso`
  - `Cantidad_Presentacion`, `Cantidad_UMP`, `Lote`, `Fecha_Agregado`, etc.
- Form de impresión ya lista licencias con esos campos:
  - `Get_Barras_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(...)`
  - Grid de licencias en `frmImpresion_OC.vb` (con formateo/row style).

## Hallazgo clave
El “estado de impresión por lote” existe de forma **derivable**, pero no está “proyectado” en el grid de lotes de OC.  
Hoy lotes y licencias se ven en pantallas/queries separadas.

---

## Modelo recomendado (sin rediseño pesado)

## Opción recomendada: mismo grid de lotes + columnas de estado derivado
No crear otro tab inicialmente. Mantener UX simple y operativa.

Agregar al grid de lotes columnas calculadas por lote:
- `Licencias_Generadas`
- `Licencias_Impresas`
- `Etiquetas_Presentacion_Impresas`
- `Cant_Presentacion_Impresa`
- `Pendiente_Imprimir` (cantidad lote - cantidad presentación impresa)
- `Estado_Impresion` (`Sin imprimir`, `Parcial`, `Completo`)

### Ventajas
- Cero cambio de navegación.
- Diagnóstico inmediato por fila.
- Menor costo de mantenimiento.

---

## SQL de proyección sugerida (resumen por lote)

Base: `trans_oc_det_lote l`  
Cruce: `i_nav_barras_pallet p` por:
- `IdOrdenCompraEnc`
- `IdOrdenCompraDet`
- normalización de lote (`ISNULL(LTRIM(RTRIM(...)),'')`)

Notas:
- usar `LEFT JOIN` para no perder lotes sin impresión.
- agregar `ISNULL` en agregaciones.

Agregaciones por lote (ejemplo conceptual):
- `COUNT(DISTINCT p.Codigo_Barra)` como `Licencias_Generadas`
- `SUM(CASE WHEN ISNULL(p.Impreso,0)=1 THEN 1 ELSE 0 END)` como `Licencias_Impresas`
- `SUM(ISNULL(p.cant_etiquetas_presentacion_impresas,0))` como `Etiquetas_Presentacion_Impresas`
- `SUM(ISNULL(p.Cantidad_Presentacion,0))` como `Cant_Presentacion_Impresa`

`Estado_Impresion`:
- `Sin imprimir`: sin licencias o 0 etiquetas.
- `Parcial`: >0 y < cantidad objetivo del lote.
- `Completo`: cantidad impresa >= cantidad lote (con tolerancia decimal configurable).

---

## ¿Grid nuevo o detail expand?

### Recomendación en 2 fases
1. **Fase 1 (rápida y segura):** mismo grid de lotes con columnas de estado.
2. **Fase 2 (opcional):** detail expand por lote (master-detail DevExpress) mostrando licencias hijas:
   - `Codigo_Barra`
   - `Fecha_Agregado`
   - `Impreso`
   - `cant_etiquetas_presentacion_impresas`
   - `Cantidad_Presentacion`
   - usuario/equipo si se agrega bitácora futura.

Así logramos visibilidad inmediata primero, y drill-down cuando sea necesario.

---

## Reglas MHS recomendadas

- Aplicar esta proyección cuando:
  - exista `trans_oc_det_lote` para la línea,
  - y el documento opere con flujo de licencias/pallet (`i_nav_barras_pallet`).
- No bloquear otros clientes: habilitar por bandera de configuración o condición de tipo de documento/interfaz.

---

## Riesgos y mitigación

- Riesgo: lotes con diferencias de formato (espacios/case).
  - Mitigar con normalización `LTRIM/RTRIM` y collation case-insensitive.
- Riesgo: sobreconteo por join no controlado.
  - Mitigar agregando por llaves correctas + `COUNT DISTINCT` de licencia.
- Riesgo: performance en OC grandes.
  - Mitigar con índice compuesto en `i_nav_barras_pallet(IdOrdenCompraEnc, IdOrdenCompraDet, Lote)`.

---

## Decisión propuesta

Implementar **Fase 1** en el mismo grid de lotes (sin pantalla nueva).  
Si el equipo operativo necesita trazabilidad por licencia dentro de la OC, agregar **detail expand** en Fase 2.

