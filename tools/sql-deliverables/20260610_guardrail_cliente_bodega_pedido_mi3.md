# Guardrail cliente_bodega - Pedido MI3 (MHS)

Fecha: 2026-06-10

## Resumen

Incidente detectado en MHS DEV: pedido importado por interfaz (`Referencia=CSFA02-36522`) con `IdCliente=1280` y `IdBodega=2`, pero sin fila en `cliente_bodega` para esa combinación. Resultado: BOF no cargaba correctamente Cliente en pantalla de pedido.

## Evidencia base (DEV)

- `trans_pe_enc`: pedido existe (`IdPedidoEnc=61`, `user_agr=1`, `fec_agr=2026-06-08 16:32:55.620`).
- `i_nav_ped_traslado_enc`: documento de interfaz con misma referencia y timestamp correlativo (`2026-06-08 16:32:55.587`).
- `cliente`: cliente maestro existe (`IdCliente=1280`).
- `cliente_bodega`: faltaba bodega 2 al momento del fallo.

## Fixes aplicados

1. Código (preventivo de creación):
   - Archivo: `TOMIMSV4/DAL/Interface/Pedido_Traslado/Pedido_Traslado_Enc/clsLnI_nav_ped_traslado_enc_Partial.vb`
   - Cambio: antes de `Inserta_Encabezado`, asegurar alta de `cliente_bodega` activa para `(IdCliente, IdBodega)`.
   - Cobertura: ambos caminos de importación de pedido MI3/NAV en el archivo.

2. Datos (correctivo puntual DEV):
   - Se insertó la relación faltante de `cliente_bodega` para `(1280,2)`.

3. DB guardrail (opcional por ambiente):
   - Script: `tools/sql-deliverables/20260610_prevent_delete_cliente_bodega_referenced.sql`
   - Acción: crea trigger `INSTEAD OF DELETE` para bloquear borrado de `cliente_bodega` si ya existen pedidos (`trans_pe_enc`) asociados.

## Riesgo/beneficio del trigger

- Beneficio: evita que mantenimiento deje pedidos huérfanos de relación cliente-bodega.
- Riesgo: puede bloquear procesos operativos de depuración masiva si antes no limpian dependencias.

## Recomendación

- Aplicar el script primero en QA y validar flujo de mantenimiento de clientes.
- Si el cliente requiere permitir bajas históricas, migrar a esquema de inactivación lógica (`activo=0`) en lugar de `DELETE` físico.
