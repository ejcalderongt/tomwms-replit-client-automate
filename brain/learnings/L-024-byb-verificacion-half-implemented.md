# L-024 — Verificacion etiqueta half-implemented (caso BYB)

> Etiqueta: `L-024_FEAT_VERIF-HALF-IMPLEMENTED-BYB_APPLIED`
> Fecha: 29-abr-2026
> Origen: anomalia detectada durante fingerprint BYB

## Hallazgo

BYB tiene **8 pedidos en estado `Verificado`** en `trans_pe_enc` PERO
**NINGUNA de las 3 tablas de soporte de verificacion existe** en su
BD:

- `trans_verificacion_etiqueta` → no existe
- `verificacion_estado` → no existe
- `log_verificacion_bof` → no existe

Es decir, el state machine del pedido reconoce el estado `Verificado`
pero no hay tablas donde escribir el detalle de la verificacion.

## Hipotesis

1. **Modulo desarrollado a medias**: el codigo del HH/BOF puede
   transicionar el estado a `Verificado` pero las tablas nunca se
   crearon en BYB. Un INSERT a `trans_verificacion_etiqueta` fallaria
   silenciosamente o por logging.
2. **Tablas borradas**: existieron pero alguien las dropeo. Improbable
   en PRD.
3. **Estados puestos por SP/manual**: alguien hizo `UPDATE trans_pe_enc
   SET estado='Verificado'` directamente sin pasar por modulo. Consistent
   con el caso BYB donde la operacion esta parada (los 8 pedidos
   `Verificado` pueden ser de pruebas residuales 2024-2025).

## Comparacion cross-cliente

| Cliente | `trans_verificacion_etiqueta` | `verificacion_estado` | `log_verificacion_bof` | Pedidos `Verificado` |
|---|---|---|---|---|
| K7 | (no consultado) | (no consultado) | (no consultado) | 7 |
| BECOFARMA | existe (0 filas) | existe (2 filas) | existe (84 filas) | 4 |
| MAMPA | (existe) | (existe) | existe (4 filas) | 3 |
| BYB | **NO existe** | **NO existe** | **NO existe** | **8** ← anomalia |
| CEALSA | NO existe | NO existe | NO existe | 0 |

→ CEALSA tampoco tiene las tablas, pero coherentemente tampoco tiene
pedidos `Verificado`. BYB es el unico que tiene pedidos pero sin
tablas → **anomalia datos vs esquema**.

## Por que importa

1. **Validar integridad de modulo**: cuando se hace fingerprint, hay
   que cruzar `estados de pedido usados` vs `tablas presentes`. Si hay
   estados sin tablas → bug latente o operacion irregular.
2. **Predicado nuevo `predicado_modulo_completo(c, modulo)`**: chequea
   coherencia entre estados+tablas+config. Si `Verificado` se usa,
   entonces deben existir las 3 tablas Y `bodega.tipo_pantalla_verificacion`
   debe ser >0 Y debe haber al menos un `operador.verifica=True`.
3. **Para WebAPI**: validar al startup que el cliente activo tenga
   las tablas necesarias para los modulos que va a usar. Health check
   de coherencia.

## Pendientes

- Investigar como se setearon los 8 pedidos `Verificado` de BYB. Query:
  `SELECT * FROM trans_pe_enc WHERE estado='Verificado'` para ver
  user_mod, fec_mod y rastrear quien/cuando.
- Confirmar K7 las 3 tablas (no consultadas en sesion de fingerprint).
- Crear test de integridad cross-cliente: para cada estado usado en
  trans_pe_enc, verificar que las tablas de soporte existan.
