---
id: 03-caso-clavaud-conservar-picking
tipo: wms-test-natural-case
estado: vigente
titulo: CASO 03 — Estrategia Clavaud (conservar zona picking)
tags: [wms-test-natural-case]
---

# CASO 03 — Estrategia Clavaud (conservar zona picking)

## Trigger (cuándo se dispara)

1. La empresa/bodega tiene `i_nav_config_enc.conservar_zona_picking_clavaud = True`
2. Llega un pedido cuya cantidad solicitada **equivale a 1+ pallet completo** del producto, calculado con `producto_presentacion.factor` o `CamasPorTarima * CajasPorCama`
3. Hay stock disponible TANTO en `bodega_ubicacion.ubicacion_picking = True` (nivel 1) COMO en niveles superiores (rack)

## Parámetros activos

| Parámetro | Tabla | Tipo | Rol |
|-----------|-------|------|-----|
| `conservar_zona_picking_clavaud` | `i_nav_config_enc` | bit | gate ON/OFF |
| `factor` | `producto_presentacion` | float | conversión presentación → UMB |
| `EsPallet` | `producto_presentacion` | bit | identifica presentación pallet |
| `CamasPorTarima`, `CajasPorCama` | `producto_presentacion` | float | geometría del pallet |
| `ubicacion_picking` | `bodega_ubicacion` | bit | flag picking |
| `nivel` | `bodega_ubicacion` | int | nivel 0-6 |

## Tablas involucradas

```
i_nav_config_enc            ← lee flag Clavaud
producto_presentacion       ← calcula Z pallets
bodega_ubicacion            ← filtra picking sí/no
stock                       ← candidatos
stock_res                   ← donde se inserta la reserva
```

## Función llamada

```
clsLnStock_res_Partial.Reserva_Stock(
    pStockRes, pIdPropietario, DiasVencimiento, MaquinaQueSolicita,
    lConnection, lTransaction,
    pExcluirUbicacionPicking := True,   ← AQUÍ se materializa Clavaud
    BeConfigEnc := <ya cargado>
)
```

## Pseudo-código del flujo

```python
def reservar_pedido_con_clavaud(pedido_det, config_enc, bodega):
    # 1. Convertir cantidad pedida a UMB
    presentacion = get_presentacion(pedido_det.IdPresentacion)
    cantidad_umb = pedido_det.cantidad * presentacion.factor

    # 2. Calcular pallets equivalentes
    if presentacion.CamasPorTarima > 0 and presentacion.CajasPorCama > 0:
        cajas_por_pallet = presentacion.CamasPorTarima * presentacion.CajasPorCama
        pallets_equivalentes = cantidad_pedida_cajas / cajas_por_pallet
    elif presentacion.IdPresentacionPallet:
        presentacion_pallet = get_presentacion(presentacion.IdPresentacionPallet)
        pallets_equivalentes = cantidad_umb / presentacion_pallet.factor
    else:
        pallets_equivalentes = 0  # producto no paletizado, no aplica Clavaud

    # 3. Decidir Clavaud
    aplicar_clavaud = (
        config_enc.conservar_zona_picking_clavaud
        and pallets_equivalentes >= 1.0  # Q-CLAVAUD-THRESHOLD: ¿es exacto o tiene margen?
    )

    # 4. Llamar Reserva_Stock con flag
    resultado = Reserva_Stock(
        pStockRes=pedido_det.to_stock_res(),
        pExcluirUbicacionPicking=aplicar_clavaud,
        BeConfigEnc=config_enc,
        ...
    )

    # 5. Fallback: si rack no tiene suficiente, reintentar SIN Clavaud
    # Q-CLAVAUD-FALLBACK: ¿hay fallback o falla la reserva?
    return resultado
```

### Dentro de `Reserva_Stock` (cuando `pExcluirUbicacionPicking = True`)

```python
def Reserva_Stock(..., pExcluirUbicacionPicking, ...):
    # El query interno de lStock agrega filtro
    if pExcluirUbicacionPicking:
        sql_filter += " AND bu.ubicacion_picking = 0 "  # solo rack alto

    candidatos = clsLnStock.lStock(...)  # devuelve solo niveles > 1
    candidatos_ordenados = ordenar_por_rotacion(candidatos, IdTipoRotacion)
    # FIFO: OrderBy(Fecha_Ingreso)
    # FEFO: OrderBy(Fecha_vence)

    # Iterar y reservar
    for stock in candidatos_ordenados:
        if cantidad_pendiente <= 0: break
        reservar(stock, min(stock.cantidad, cantidad_pendiente))
        cantidad_pendiente -= ...
```

## Caso operativo real

**Cliente**: MERCOPAN (IDEALSA Panamá), bodega CDPan-1.
**Anécdota** (Erik 2026-04-29):
> "Marcelo Clavaud, gerente de logística de IDEALSA Panamá, planteó: si reservás 100 cajas y vaciás picking, el operario de piso se queda sin nada que despachar para los pedidos pequeños. Reabastecer 20 pallets toma 30+ minutos con el montacargas. Cuello de botella operativo. Si el pedido equivale a pallets completos, **es más eficiente cargar pallets enteros directo del rack** y dejar picking intacto."

**Fecha de incorporación**: 2022-11-07 (commit `EJC202211071706`)
**Migration**: `alter table i_nav_config_enc add conservar_zona_picking_clavaud bit default 0`

## Variantes y combinaciones

### Combina con FIFO/FEFO
La selección dentro del rack sigue rotación. Si hay 3 pallets del mismo producto en rack con distinta `Fecha_vence`, FEFO toma primero el más próximo a vencer.

### Combina con Operador_logistico (3PL)
- Si la empresa es 3PL (`Operador_logistico=True`), el flag Clavaud se busca por (IdBodega, IdEmpresa). Cada empresa cliente puede tener Clavaud distinto.
- Si NO es 3PL, se busca por (IdBodega, IdPropietario).

### NO combina con `IdUbicacionAbastecerCon`
Si el cliente tiene restricción de ubicación específica (Caso 05), Clavaud queda overridden — el WMS busca solo en esa ubicación, sea picking o rack.

### Si el rack está vacío
- **Hipótesis A**: la función falla y devuelve `False` → el pedido queda sin reservar y debe intervenir el operador.
- **Hipótesis B**: hay fallback automático que reintenta sin Clavaud y permite vaciar picking.
- **Q-CLAVAUD-FALLBACK abierta**.

## Q-* asociadas

- ✅ **Q-CLAVAUD-MEANING** (resuelta) — apellido del gerente
- 🟡 **Q-CLAVAUD-THRESHOLD** — ¿corte exacto en 1.0 pallet o tiene margen?
- 🟡 **Q-CLAVAUD-FALLBACK** — ¿qué pasa si rack no tiene suficiente?
- 🟡 **Q-CLAVAUD-CLIENTES-ACTIVOS** — ¿qué clientes tienen el flag ON hoy?

## Validación SQL

```sql
-- Bodegas/empresas con Clavaud activo
SELECT IdBodega, IdEmpresa, IdPropietario, conservar_zona_picking_clavaud
FROM IMS4MB_MERCOPAN_PRD.dbo.i_nav_config_enc
WHERE conservar_zona_picking_clavaud = 1;

-- Detectar pedidos donde Clavaud aplicó (heurística: stock_res sin filas en nivel 1)
SELECT pe.IdPedidoEnc, COUNT(*) lineas_solo_rack
FROM IMS4MB_MERCOPAN_PRD.dbo.trans_pe_enc pe
JOIN IMS4MB_MERCOPAN_PRD.dbo.stock_res sr ON sr.IdPedidoEnc = pe.IdPedidoEnc
JOIN IMS4MB_MERCOPAN_PRD.dbo.stock s ON s.IdStock = sr.IdStock
JOIN IMS4MB_MERCOPAN_PRD.dbo.bodega_ubicacion bu ON bu.IdUbicacion = s.IdUbicacion
WHERE bu.ubicacion_picking = 0
GROUP BY pe.IdPedidoEnc
HAVING NOT EXISTS (
    SELECT 1 FROM stock_res sr2
    JOIN stock s2 ON s2.IdStock = sr2.IdStock
    JOIN bodega_ubicacion bu2 ON bu2.IdUbicacion = s2.IdUbicacion
    WHERE sr2.IdPedidoEnc = pe.IdPedidoEnc AND bu2.ubicacion_picking = 1
);
```
