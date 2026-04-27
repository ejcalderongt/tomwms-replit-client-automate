# ADR-007: Integracion Killios ↔ SAP Business One via DI-API

## Contexto

Killios (TOMWMS_KILLIOS_PRD) tiene `interface_sap=true` en las 6 bodegas. La integracion con SAP B1 se hace via DI-API (no DI Server, no Service Layer). Este ADR documenta los flags relacionados y el comportamiento observado.

## Datos aprendidos

```
interface_sap                      = true
sap_control_draft_ajustes          = false   # postea directo, no draft
sap_control_draft_traslados        = false   # postea directo
inferir_bonificacion_pedido_sap    = false   # NO infiere lineas con UnitPrice=0
rechazar_bonificacion_incompleta   = false
convertir_decimales_a_umbas        = 1       # CRITICO: maneja cantidades fraccionarias
```

## Reto principal

SAP B1 envia cantidades en presentacion (cajas) con **decimales** (ej: 3.5 cajas). El WMS maneja inventario por:
- Presentacion (CJ) — cantidad entera o fraccionaria que SAP entiende.
- UM base (UDS) — unidades enteras internas para reservar/picking.

`convertir_decimales_a_umbas=1` aplica el factor de equivalencia (`i_nav_producto_presentacion.equivalencia`) y traduce 3.5 CJ → N UDS para reservar internamente. Al despachar, el documento de salida hacia SAP tiene que devolver el resultado en cajas (con decimales si correspondio).

## Decisiones para el bridge

1. Los escenarios de reserva contra Killios deben **simular cantidades fraccionarias** del lado del input.
2. El bridge debe poder **compilar el resultado de vuelta** a cajas-decimales para el documento SAP.
3. Si `convertir_decimales_a_umbas=0` y llega un decimal, el comportamiento esperado del legacy es **rechazar el documento**. Documentar como escenario CONV-001.
4. NO se postean drafts: el bridge no debe asumir un workflow de aprobacion intermedio.

## Pendiente

- Documentar el schema completo de `i_nav_producto_presentacion` y como se calcula `equivalencia`.
- Capturar pedidos SAP reales (sample) para usar en escenarios.
- Modelar el caso `inferir_bonificacion_pedido_sap=true` (no usado en Killios pero relevante para otros clientes).
