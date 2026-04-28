# Graph-EQL — CEALSA

**Caracter**: ambiente QAS (no productivo), 19503 ubicaciones (estructura
sintetica), 2 estados (catalogo pobre), 0 presentaciones, 0 outbox,
**SOLO RECEPCION** (NUNCA llega a salir del WMS).

## Sub-grafo (truncado)

```
=== UNICO FLUJO QUE EJECUTA: INGRESO ===
RECEPCION (CEALSASync.exe sintetica)
   │
   --[ 1:RECE | stock_rec | bodega 2 = BODEGA FISCAL ]-->
   ▼
[??? @ B12T01R00P00]   ← IdProductoEstado=NULL (no setea estado!)
                          ubicacion siempre la misma
                          fecha_vence = 1900-01-01 (control_vencimiento=False)

=== NO HAY MAS TRANSICIONES ===
NO 2:UBIC, NO 8:PIK, NO 11:VERI, NO 5:DESP, NO 4:TRAS, NO ajustes.

=== SIMULACRO DE SALIDA (sin movimientos reales) ===
trans_pe_enc: 3707 pedidos en estado "Despachado"
   │
   │ pero NO hay trans_movimientos correspondientes
   │ (los pedidos se setean directo en BD via script)
   ▼
(no hay outbox, no hay i_nav_transacciones_out)
```

## Aristas dominantes (datos historicos producto NEN025 AMOXICILINA)

| Tipo | n | % |
|---|---|---|
| 1 RECE | 1798 | **100%** |

## Caracteristicas del sendero CEALSA

- **Sendero TRUNCADO**: NUNCA se completa. El producto entra y se queda.
  Esto es coherente con que CEALSA es un ambiente QAS (Quality Assurance
  System) usado para pruebas, NO para operacion real.
- **IdProductoEstado=NULL**: no se setea estado en stock_rec. Coherente
  con catalogo pobre (solo 2 estados en producto_estado).
- **Ubicacion unica**: TODOS los productos en B12T01R00P00 (a pesar de
  tener 19503 ubicaciones disponibles). Es la "ubicacion de recepcion
  por defecto" y nadie hace put-away.
- **Sin vencimiento real**: fecha_vence = 1900-01-01 (constante). Confirma
  que no se setean datos reales.
- **3707 pedidos en "Despachado"**: pero sin movimientos. Indican que
  alguien usa la BD para setear pedidos como despachados sin pasar por
  el flujo del WMS (probable script de sincronizacion del ERP que crea
  pedidos directamente).
- **0 outbox**: no hay tabla `i_nav_transacciones_out` con datos, o la
  tabla esta vacia. CEALSASync.exe nunca recibio nada que enviar.

## Tesis: CEALSA no es un cliente, es un ambiente de prueba

Caracteristicas que lo confirman:
- Solo RECE (no salida).
- Catalogo pobre (2 estados).
- Datos sinteticos (vencimiento 1900-01-01).
- Outbox vacia.
- Pedidos directos en BD sin flujo.

## Pendientes (Q-CEALSA-OUTBOX-VACIO + Q-CEALSA-CEALSASYNC-ERP)

- Confirmar con Erik si CEALSA es realmente un QAS o si tiene algun
  uso productivo escondido.
- Identificar el script que setea los 3707 pedidos como "Despachado".
- Si es QAS, evaluar si vale la pena mantenerlo o si se puede eliminar
  del set (5 → 4 clientes).

## Para WebAPI

CEALSA NO debe usarse como cliente de validacion del comportamiento de
la WebAPI porque su sendero es no representativo. Debe documentarse
explicitamente como ambiente de QA y excluirse del catalogo de "patrones
de cliente".
