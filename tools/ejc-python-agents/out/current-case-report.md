# EJC Python Agent - Case Report

- generated_at: 2026-05-26T15:40:54-06:00
- agent: ejc-python-agent v0.1 (draft)
- case: Documento 17946 en back-order sin banderita no enviado

## 1) Data Lane
- IDs detectados: 17946, 66967, 301202600008320
- Top coincidencias de codigo relacionadas:
  - Sin coincidencias directas en codebase para los terminos.

### SQL sugerido (solo lectura)
```sql
-- Encabezado OC
SELECT e.IdOrdenCompraEnc,e.No_Documento,e.Referencia,e.IdEstadoOC,es.Nombre AS Estado,
       e.IdTipoIngresoOC,ti.Nombre AS TipoIngreso,e.Enviado_A_ERP
FROM trans_oc_enc e
LEFT JOIN trans_oc_estado es ON es.IdEstadoOC=e.IdEstadoOC
LEFT JOIN trans_oc_ti ti ON ti.IdTipoIngresoOC=e.IdTipoIngresoOC
WHERE e.IdOrdenCompraEnc=17946 OR e.No_Documento='66967';
```
```sql
-- Detalle OC
SELECT d.IdOrdenCompraEnc,d.No_Linea,d.codigo_producto,d.cantidad,d.cantidad_recibida,
       (d.cantidad-d.cantidad_recibida) AS pendiente
FROM trans_oc_det d
WHERE d.IdOrdenCompraEnc=17946
ORDER BY d.No_Linea;
```
```sql
-- Recepciones asociadas
SELECT TOP 50 *
FROM trans_re_enc
WHERE IdOrdenCompraEnc=17946
ORDER BY IdRecepcionEnc DESC;
```
```sql
-- Tablas seed data lane: trans_oc_enc, trans_oc_det, trans_oc_estado, trans_oc_ti, trans_re_enc
```

## 2) Operativity Lane
- Top coincidencias de flujo/UI:
  - Sin coincidencias directas en codebase para los terminos.
- Objetivos seed operativos:
  - frmOrdenCompra.vb
  - frmPedido.vb
  - frmAjusteStock.vb

## 3) Cruce datos vs operatividad
- Validar si el estado en BD habilita/deshabilita visualmente accion esperada (ej: bandera No Enviado).
- Confirmar si la accion existe en codigo y si esta enlazada en el ribbon/menu activo del formulario.
- Confirmar precondiciones para cerrar flujo (enviado ERP, pendientes=0, estado actual).

## 4) Guardrails
