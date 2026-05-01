---
id: 05-caso-restriccion-ubicacion-por-cliente
tipo: wms-test-natural-case
estado: vigente
titulo: "CASO 05 — Restricción de ubicación por cliente (`IdUbicacionAbastecerCon`)"
tags: [wms-test-natural-case]
---

# CASO 05 — Restricción de ubicación por cliente (`IdUbicacionAbastecerCon`)

## Trigger

Cliente final (no propietario, no empresa) tiene asignada una **ubicación específica** desde la cual SIEMPRE se le abastece. El WMS solo busca stock candidato dentro de esa ubicación, ignorando cualquier otro stock disponible.

## Parámetros activos

| Parámetro | Tabla | Tipo | Rol |
|-----------|-------|------|-----|
| `IdUbicacionAbastecerCon` | `cliente` | int (FK a `bodega_ubicacion`) | restricción |
| `despachar_lotes_completos` | `cliente` | bit | "el cliente quiere lotes enteros, no parciales" |
| `realiza_manufactura` + `IdUbicacionManufactura` | `cliente` | bit + int | restricción análoga para manufactura |

## Tablas involucradas

```
cliente                     ← lee IdUbicacionAbastecerCon
bodega_ubicacion            ← target ubicación específica
stock                       ← solo candidatos en esa ubicación
trans_pe_enc                ← header pedido (lleva IdCliente)
```

## Pseudo-código del flujo

```python
def reservar_para_cliente_restringido(pedido_det, pedido_enc):
    cliente = get_cliente(pedido_enc.IdCliente)

    # 1. ¿Tiene restricción de ubicación?
    if cliente.IdUbicacionAbastecerCon and cliente.IdUbicacionAbastecerCon > 0:
        # Filtro forzado: solo stock en esa ubicación
        candidatos = stock_query(
            IdProducto=pedido_det.IdProducto,
            IdUbicacion=cliente.IdUbicacionAbastecerCon,  # FORZADO
            IdBodega=pedido_enc.IdBodega
        )

        if candidatos.empty():
            raise Exception(
                f"Cliente {cliente.codigo} tiene ubicación restringida "
                f"{cliente.IdUbicacionAbastecerCon} sin stock disponible. "
                f"NO se puede tomar de otras ubicaciones."
            )

        # Aplica rotación FIFO/FEFO dentro del set restringido
        ordenar_y_reservar(candidatos)

    else:
        # Flujo normal: candidatos en toda la bodega
        candidatos = stock_query(IdProducto=..., IdBodega=...)
        ordenar_y_reservar(candidatos)

    # 2. ¿Despacha solo lotes completos?
    if cliente.despachar_lotes_completos:
        # Filtra candidatos cuya cantidad coincide con el lote completo
        # (rechaza picking parcial)
        candidatos = [c for c in candidatos if c.cantidad == c.lote_total]
```

## Caso operativo real

**Anécdota Erik (2026-04-29)**:
> "Hay clientes que tienen políticas muy específicas. Por ejemplo: 'a este cliente le abastesco solo lo que muevo a esta ubicación'. Es útil para aplicar restricciones muy generalizadas — el WMS no toca el resto del stock para ese cliente, ni siquiera si está disponible."

### Caso de uso típico
- **Ubicación dedicada por cliente VIP**: el cliente compró un acuerdo de exclusividad de stock. El proveedor reserva físicamente una zona del almacén para él.
- **Cliente con manufactura interna**: `IdUbicacionManufactura` apunta a la ubicación donde se hace el ensamblado/empaque para ese cliente.
- **Cliente con SLA estricto**: la ubicación está pre-seleccionada con la rotación correcta para ese cliente, sin riesgo de FIFO global.

## Variantes y combinaciones

### Combina con Clavaud → ANULA Clavaud
Si el cliente tiene `IdUbicacionAbastecerCon`, no importa si la ubicación es picking o rack — el WMS busca SOLO ahí.

### Combina con Lote numérico (Caso 06)
Sí. Si el cliente tiene `IdUbicacionAbastecerCon` Y `control_ultimo_lote`, el WMS busca lotes correlativos N+1 dentro de la ubicación restringida.

### Combina con tolerancia (Caso 07)
Sí. La tolerancia de vencimiento se aplica dentro del set restringido.

### Sin stock disponible
La función falla. **Q-RESTRICCION-FALLBACK**: ¿hay alguna política de fallback (ej: alertar al supervisor para mover stock)? ¿O queda en el operador?

## Q-* asociadas

- ✅ **Q-RESTRICCION-EXISTE** — confirmado: `cliente.IdUbicacionAbastecerCon`
- 🟡 **Q-RESTRICCION-CLIENTES-ACTIVOS** — qué clientes hoy tienen este parámetro setteado
- 🟡 **Q-RESTRICCION-FALLBACK** — política cuando ubicación restringida queda sin stock
- 🟡 **Q-MANUFACTURA-FLOW** — cómo se diferencia el flujo cuando `realiza_manufactura=True` + `IdUbicacionManufactura`

## Validación SQL

```sql
-- Clientes con restricción de ubicación
SELECT c.IdCliente, c.codigo, c.nombre_comercial,
       c.IdUbicacionAbastecerCon, bu.descripcion as ubic_restringida,
       bu.nivel, bu.ubicacion_picking
FROM IMS4MB_MERCOPAN_PRD.dbo.cliente c
LEFT JOIN IMS4MB_MERCOPAN_PRD.dbo.bodega_ubicacion bu
    ON bu.IdUbicacion = c.IdUbicacionAbastecerCon
WHERE c.IdUbicacionAbastecerCon IS NOT NULL
  AND c.IdUbicacionAbastecerCon > 0
ORDER BY c.codigo;

-- Clientes con manufactura
SELECT c.IdCliente, c.codigo, c.nombre_comercial,
       c.realiza_manufactura, c.IdUbicacionManufactura
FROM IMS4MB_MERCOPAN_PRD.dbo.cliente c
WHERE c.realiza_manufactura = 1;

-- Clientes con despacho de lotes completos
SELECT c.IdCliente, c.codigo, c.despachar_lotes_completos
FROM IMS4MB_MERCOPAN_PRD.dbo.cliente c
WHERE c.despachar_lotes_completos = 1;
```
