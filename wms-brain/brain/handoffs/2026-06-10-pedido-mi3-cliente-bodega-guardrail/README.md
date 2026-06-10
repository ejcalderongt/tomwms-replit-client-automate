# Pedido MI3 sin cliente visible por cliente_bodega faltante (MHS) — 2026-06-10

## Síntoma

En BOF (pantalla Pedido), un documento importado desde interfaz mostraba `Cliente` vacío/no resolvible en UI aunque `trans_pe_enc.IdCliente` sí venía poblado.

Caso base:

- BD: `TOMWMS_MHS_DEV`
- Pedido: `Referencia = CSFA02-36522`
- `trans_pe_enc`: `IdCliente=1280`, `IdBodega=2`, `user_agr=1`

## Causa raíz

Al momento de generar el pedido desde interfaz, el cliente maestro existía, pero faltaba la relación activa en `cliente_bodega` para la bodega del pedido (`IdCliente=1280`, `IdBodega=2`).

Esto no está protegido por FK directa desde `trans_pe_enc` hacia `cliente_bodega`; `trans_pe_enc` referencia `cliente` y `bodega` por separado.

## Evidencia SQL mínima

```sql
SELECT TOP 1 IdPedidoEnc, Referencia, IdBodega, IdCliente, user_agr, fec_agr
FROM trans_pe_enc
WHERE Referencia = 'CSFA02-36522';

SELECT IdClienteBodega, IdBodega, IdCliente, Activo
FROM cliente_bodega
WHERE IdCliente = 1280;
```

Resultado observado en incidente:

- Pedido existente con `IdBodega=2`.
- `cliente_bodega` sin fila activa para bodega 2 (solo otras bodegas).

## Fix de código (preventivo)

Archivo:

- `TOMWMS/TOMIMSV4/DAL/Interface/Pedido_Traslado/Pedido_Traslado_Enc/clsLnI_nav_ped_traslado_enc_Partial.vb`

Cambio:

- Antes de `Inserta_Encabezado`, asegurar alta de `cliente_bodega` para `(IdCliente, IdBodega)` si no existe.
- Aplicado en ambos caminos de importación MI3/NAV.

## Guardrail DB opcional

Script recomendado:

- `TOMWMS/tools/sql-deliverables/20260610_prevent_delete_cliente_bodega_referenced.sql`

Comportamiento:

- Trigger `INSTEAD OF DELETE` en `cliente_bodega`.
- Bloquea borrado físico si existe al menos un `trans_pe_enc` para la misma combinación `(IdCliente, IdBodega)`.

## Decisión operativa

- Recomendado aplicar primero en QA.
- Si se requiere baja operativa, preferir `activo=0` sobre `DELETE` físico.
- Mantener trazabilidad de usuario que elimina en mantenimiento de cliente/bodega.

## Clasificación

- Tipo: integración + data integrity
- Dominios: `domain-integration-services`, `domain-database`, `domain-bof`
- Riesgo: medio (inconsistencia silenciosa de datos maestros por bodega)
