---
id: traza-005-cealsa-NEN025
tipo: sendero-producto
estado: vigente
titulo: Traza 005 — CEALSA NEN025 (AMOXICILINA 500MG)
clientes: [cealsa]
tags: [sendero-producto, cliente/cealsa]
---

# Traza 005 — CEALSA NEN025 (AMOXICILINA 500MG)

**Cliente**: CEALSA (IMS4MB_CEALSA_QAS) | **Bodegas**: 2  
**Producto**: IdProducto=4828 | 1798 movimientos historicos  
**Fecha de la traza**: 29-abr-2026 | **Correccion**: 29-abr-2026

> **CORRECCION (Erik 29-abr-2026)**: La hipotesis "CEALSA es ambiente
> QAS-truncado" era ERRONEA. CEALSA es cliente productivo con un flujo
> distinto al estandar de stock: el WMS NO envia transacciones de stock
> al ERP de CEALSA, envia **rubros de cobro (prefactura)**. Por eso
> `i_nav_transacciones_out` no se marca como enviado para los registros
> de salida.
>
> Esto explica todas las "anomalias":
> - `i_nav_transacciones_out` aparenta vacia/sin marcar = no es bug, es diseño.
> - 3707 pedidos en estado "Despachado" sin movimientos = la salida NO
>   se realiza por trans_movimientos sino via `trans_prefactura_enc`,
>   `trans_prefactura_det`, `trans_prefactura_mov`.
> - Vistas dedicadas: `cealsa_vwacuerdocomercialdet/enc`,
>   `cealsa_vwclientes`, `Polizas_CEALSA`.
>
> Patron: **`P-PREFACTURA-SIN-INTERFACE-STOCK`** (no P-QAS-TRUNCADO).

## Q1-Q4 (sin cambios — ver historial git anterior si interesa el detalle)

```
producto: NEN025 control_lote=True control_vencimiento=False
producto_bodega: 2 (B01 GENERAL, B02 FISCAL)
producto_presentacion: 0
stock_rec: todo en B12T01R00P00, IdProductoEstado=NULL, vence=1900-01-01
```

## Q5-Q6: solo 1:RECE (1798 / 1798 = 100%)

(re-interpretado): el sistema NO requiere put-away porque la prefactura
opera sobre stock disponible sin diferenciar ubicacion fisica.

## Tablas dedicadas a prefactura (descubiertas 29-abr-2026)

```
trans_prefactura_enc            ← encabezado prefactura
trans_prefactura_det            ← detalle (rubros)
trans_prefactura_mov            ← movimientos prefactura
cealsa_vwacuerdocomercialenc    ← vistas para acuerdos comerciales
cealsa_vwacuerdocomercialdet
cealsa_vwclientes               ← vista de clientes
Polizas_CEALSA                  ← polizas especificas
i_nav_transacciones_out         ← existe pero no se marca para salidas
                                  (intencionalmente — no se transmite stock)
```

## Patron P-PREFACTURA-SIN-INTERFACE-STOCK

```
=== INGRESO ===
ERP CEALSA -> CEALSASync.exe -> RECEPCION sintetica
   │
   --[ 1:RECE | stock_rec | sin estado, ubicacion fija ]-->
   ▼
[??? @ B12T01R00P00]   ← stock disponible para prefactura

=== SALIDA POR PREFACTURA (no via stock!) ===
ERP envia rubros de cobro
   │
   ▼
trans_prefactura_enc + trans_prefactura_det + trans_prefactura_mov
   │ procesa rubros
   ▼
trans_pe_enc en estado "Despachado" (sintetico, sin trans_movimientos)
   │
   ▼
i_nav_transacciones_out NO MARCADA (diseño, no bug)
```

## Pendientes refinados

- ~~Q-CEALSA-OUTBOX-VACIO~~ CERRADA (no es bug, es diseño).
- ~~Q-CEALSA-CEALSASYNC-ERP~~ CERRADA (sync envia rubros, no transacciones).
- NUEVO Q-CEALSA-PREFACTURA-MODELO: documentar esquema completo de
  `trans_prefactura_*` y como se relaciona con `trans_pe_enc/det`.
- Mover este patron al `heat-map-params/06-procesos-homologados/salida.md`
  como variante de salida.
