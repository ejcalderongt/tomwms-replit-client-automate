# Modelo del sendero — Puntos de salida

## Concepto

El producto sale del WMS cuando se ejecuta un despacho que satisface
un pedido. El flujo de salida tiene tres etapas claras:

```
trans_pe_enc/det (PEDIDO desde ERP)
        │
        │ se asigna a operador, se planifica
        ▼
trans_pi_enc/det (PICKING — recolectar de las ubicaciones)
        │ trans_movimientos: IdTipoTarea=8 (PIK)
        │ IdUbicacionOrigen = ubicacion del stock
        │ IdUbicacionDestino = NULL (sale del WMS) o "ESTACION_PICKING"
        │
        ▼
trans_(verif?) (VERIFICACION — opcional, depende del cliente)
        │ trans_movimientos: IdTipoTarea=11 (VERI)
        │ chequeo de etiquetas/cantidades antes de despachar
        │
        ▼
trans_des_enc/det (DESPACHO — salida fisica del producto)
        │ trans_movimientos: IdTipoTarea=5 (DESP)
        │ stock_rec.IdDespachoEnc se setea
        │ stock_rec.activo = 0 (o cantidad descuenta)
        │
        ▼
ERP (notificacion de despacho via outbox / SAPBOSync*)
```

## Estados del pedido cross-cliente

| Estado | BECOFARMA | K7 | MAMPA | BYB | CEALSA |
|---|---|---|---|---|---|
| Pendiente | ✓ | ✓ | ✓ | 4 | ✗ |
| Pickeado | 3802 | ? | ✓ | ✗ | ✗ |
| Verificado | 4 | ? | ? | 8 | ✗ |
| Despachado | (mayoria) | (mayoria) | (mayoria) | 9597 | 3707 |
| Anulado | ? | ? | ? | 27 | ✗ |
| Nuevo | ? | ? | ? | 27 | ✗ |

> CEALSA tiene SOLO `Despachado` → no hay flujo de pedido real, los
> 3707 fueron seteados directos.

## Variantes del flujo de salida cross-cliente

### Caso BECOFARMA (cuello de botella H29)

```
PEDIDO Pendiente → PIK (picking) → 3802 quedan en estado "Pickeado"
                                   ↓
                          (NO avanza a Verificado/Despachado)
                                   ↓
                       outbox=31263 sin enviar (CRITICO)
```

> Hipotesis H29: el cuello esta entre Pickeado y Verificado/Despachado.
> Operadores no completan la verificacion → pedidos quedan en limbo.

### Caso K7 (flujo sano con reabasto intensivo)

```
PEDIDO → PIK desde ubicacion picking
         │ si stock < demanda → REEMP_BE_PICK (reabasto)
         │ luego retomar PIK
         ▼
        VERI (869 movs)
         │
         ▼
        DESP (812 movs)

outbox=3 sin enviar (sano)
```

### Caso MAMPA (flujo con traslados)

```
PEDIDO desde TIENDA CENTRAL → PIK
                              │
                              ▼
                              VERI (289 movs)
                              │
                              ▼
                              DESP (136 movs)

PARALELO: TRAS (90 movs) entre tiendas para reabasto inter-tienda
```

### Caso BYB (flujo masivo distribucion, parado 2024)

```
PEDIDO → PIK (251 movs) → VERI (4631) → DESP (12873)
              ↑
              │ EXPLOSION (57) cuando picking pide < pallet
              │ REABMAN (106) reabasto manual desde almacen
              │ PACK (14) empaque final
              │ REEMP_*_PICK reabasto automatico
              │
         (intensa actividad de soporte)

Pero: ULTIMA actividad dic-2023, despues silencio.
```

### Caso CEALSA (no hay salida)

```
PEDIDO Despachado (3707) ← seteados directo en BD, sin movimientos
NO HAY PIK, VERI, DESP en trans_movimientos.
```

## Outbox: el reflejo de la salida hacia el ERP

Cada DESP genera (debe generar) una fila en `i_nav_transacciones_out`
con `tipo=SALIDA`. Esa fila se envia al ERP via SAPBOSync*/NavSync/
CEALSASync.

### Backlog de outbox por cliente

| Cliente | enviado=1 | enviado=0 | % backlog |
|---|---|---|---|
| BECOFARMA | (a medir, ~mucho) | 31263 | CRITICO |
| K7 | (sano) | 3 | sano |
| MAMPA | (a medir) | (a medir) | (a medir) |
| BYB | 277417 | 255912 | 48% (causa: corte 2024) |
| CEALSA | 0 | 0 | n/a (no hay outbox) |

## Implicaciones para WebAPI

1. La salida es la zona mas critica del WMS: cuello de botella H29 en
   BECOFARMA, backlog 48% en BYB. La WebAPI debe tener observabilidad
   detallada del flujo de salida (metricas por etapa, alertas cuando
   un pedido se queda en una etapa > X tiempo).
2. La maquina de estados del pedido debe estar explicita y validada.
   La WebAPI define las transiciones permitidas (Pendiente → Pickeado
   → Verificado → Despachado) y rechaza saltos invalidos.
3. El outbox debe ser observable y purgable. Cada cliente debe tener
   su politica (ej: BYB con 256K filas requiere o purga o reproceso).
