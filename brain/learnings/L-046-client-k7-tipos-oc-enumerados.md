# L-046 — CLIENT: K7 maneja 18 tipos de Orden de Compra (OC), enumerados oficialmente por Carolina

> Etiqueta: `L-046_CLIENT_K7-TIPOS-OC_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 11, respuesta Carolina a Q-K7-OC-TIPOS

## Hallazgo

K7 tiene **18 tipos de OC** activos (codigos 1-18, faltan 6, 9, 11, 12 — ¿deprecated o reservados? Q nueva).

## Lista oficial (al 30-abr-2026)

| Codigo | Nombre |
|---|---|
| 1 | Ingreso Almacen General |
| 2 | Devolucion |
| 3 | Transferencia |
| 4 | Transferencia WMS |
| 5 | Ingreso Almacen Fiscal |
| 7 | Ingreso consolidado |
| 8 | Transferencia de Ingreso |
| 10 | Ingreso Almacen General con Poliza |
| 13 | Factura_Reserva_Proveedor Imp. |
| 14 | Ingreso_Inventario_Inicial |
| 15 | Ingreso por nota credito cancelada |
| 16 | Factura_Reserva_Proveedor |
| 17 | Ingreso por nota de credito |
| 18 | Ingreso por factura cancelada |

## Cita literal Carolina (Wave 11)

> "Actualmente tenemos estos tipos de OC: [lista arriba]"

## Observaciones del agente

### Codigos faltantes (6, 9, 11, 12)

Posibilidades:
- **Deprecated**: tipos que existian y se desactivaron sin renumerar.
- **Reservados**: huecos intencionales para futuros tipos (mala practica pero comun en ERPs).
- **Especificos de otro cliente**: codigos que solo aplican a otros 3PL y K7 nunca los uso.

→ Q nueva.

### Diferencia entre Transferencia (3) y Transferencia WMS (4)

Probable:
- **Transferencia (3)**: movimiento ERP-driven (NAV o Almacen Fiscal genera).
- **Transferencia WMS (4)**: movimiento interno del WMS sin reflejo ERP (o que el WMS empuja a ERP).

→ Confirmar con Erik o documento operativo K7.

### Patron "Ingreso/Devolucion/Transferencia" + variantes financieras

Los 18 tipos cubren:
- **Recepcion fisica**: 1, 5, 7, 10 (general / fiscal / consolidado / con poliza)
- **Movimientos**: 3, 4, 8 (transferencias)
- **Devoluciones y rectificaciones**: 2, 15, 17, 18 (devoluciones, notas credito, facturas canceladas)
- **Provisional / contable**: 13, 16 (factura reserva proveedor, con/sin importacion)
- **Setup**: 14 (inventario inicial — solo va-vivo)

→ K7 tiene **el catalogo de tipos de OC mas rico** del brain hasta ahora. Otros clientes (BYB, MAMPA, BECO) probablemente tienen subset.

## Implicaciones

1. **Cualquier reporte/UI que muestre tipos de OC para K7 debe usar esta tabla como fuente de verdad** (probable tabla `tipo_oc` o `cat_tipo_oc` filtrada por empresa K7).
2. **Migracion 2028 K7 (L-027)**: si en algun cliente se simplifica el catalogo, K7 debe quedar tal cual — su negocio tiene matices fiscales (almacen fiscal, polizas, NC) que no son negociables.
3. **Validacion en codigo nuevo**: cualquier `if tipoOC = 1 Or tipoOC = 5 Or ...` debe migrar a tabla parametrica (L-035 — no codificar por cliente, agregar parametro a la tabla `tipo_oc`).

## Q-* abiertas

- Q-K7-TIPOS-OC-FALTANTES: ¿que paso con los codigos 6, 9, 11, 12? Deprecated, reservados, o de otro cliente?
- Q-K7-TIPO-OC-TABLA: nombre exacto de la tabla que persiste estos tipos.
- Q-OTROS-CLIENTES-TIPOS-OC: ¿que tipos de OC manejan BYB, MAMPA, BECO, IDEALSA, INELAC, MERCOPAN, MHS? Construir matriz cliente × tipo.
- Q-K7-TRANSFERENCIA-VS-TRANSFERENCIAWMS: diferencia operativa concreta entre tipo 3 y tipo 4.
- Q-K7-FACTURA-RESERVA-PROV: que es exactamente "Factura_Reserva_Proveedor" — flujo end-to-end.

## Vinculos

- L-035: tabla parametrica `tipo_oc` es el patron correcto, no IF por cliente.
- L-027: K7 no aparece en la primera ola de migracion 2028 (BYB+IDEALSA+INELAC). Pendiente confirmar cuando entra K7 al plan.
