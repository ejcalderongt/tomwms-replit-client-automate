---
id: CEALSA
tipo: sendero-producto
estado: vigente
titulo: Graph-EQL — CEALSA (CORREGIDO 29-abr-2026)
clientes: [cealsa]
tags: [sendero-producto, cliente/cealsa]
---

# Graph-EQL — CEALSA (CORREGIDO 29-abr-2026)

> **CORRECCION**: La hipotesis "QAS-TRUNCADO" era erronea. CEALSA es
> cliente productivo con flujo de prefactura (rubros de cobro) en vez
> de transacciones de stock al ERP.

**Caracter**: pharma con flujo PREFACTURA. WMS gestiona stock
internamente pero al ERP de CEALSA envia rubros, no transacciones.
Tablas dedicadas: `trans_prefactura_enc/det/mov`, `cealsa_vw*`, `Polizas_CEALSA`.

## Sub-grafo

```
=== INGRESO (sintetico desde ERP via CEALSASync.exe) ===
RECEPCION
   │
   --[ 1:RECE | stock_rec | bodega 2 = BODEGA FISCAL ]-->
   ▼
[??? @ B12T01R00P00]   ← IdProductoEstado=NULL, no requiere put-away

=== "SALIDA" POR PREFACTURA ===
ERP CEALSA solicita rubros
   │
   ▼
trans_prefactura_enc + trans_prefactura_det + trans_prefactura_mov
   │ procesamiento de rubros
   ▼
trans_pe_enc estado "Despachado" (sintetico)
   │
   ▼
i_nav_transacciones_out NO se marca (sin transmision de stock al ERP)
```

## Aristas dominantes

| Tipo | n | % |
|---|---|---|
| 1 RECE | 1798 | 100% |

NO hay 2:UBIC, 8:PIK, 11:VERI, 5:DESP — la salida no genera mov de stock.

## Caracteristicas del sendero

- **Patron P-PREFACTURA-SIN-INTERFACE-STOCK**.
- No requiere put-away (prefactura no diferencia ubicacion fisica).
- No requiere picking/verificacion/despacho en `trans_movimientos`.
- Pedidos "Despachado" son sinteticos: confirman cobertura de rubro.
- `i_nav_transacciones_out` vacia/sin marcar = comportamiento esperado.
- CEALSASync.exe sincroniza rubros, no transacciones.

## Implicaciones para WebAPI .NET 10

CEALSA requiere capability dedicada: `salida-via-prefactura`. La
WebAPI debe distinguir:
- Salida por transaccion de stock (BECOFARMA, K7, MAMPA, BYB).
- Salida por prefactura (CEALSA + posibles otros futuros).

## Pendientes

- Capturar esquema `trans_prefactura_enc/det/mov`.
- Mapear relacion rubros ↔ productos/bodegas/lotes.
- Documentar flujo bidireccional con ERP CEALSA.
