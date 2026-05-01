---
id: 07-caso-tolerancia-vencimiento
tipo: wms-test-natural-case
estado: vigente
titulo: CASO 07 — Tolerancia de vencimiento por familia/clasificación
tags: [wms-test-natural-case]
---

# CASO 07 — Tolerancia de vencimiento por familia/clasificación

## Trigger

Cliente acuerda con proveedor que recibirá producto con **al menos N días de vida útil restante**. Lo que esté por debajo se discrimina automáticamente — no se le despacha aunque haya stock.

Ejemplo: cereales con 90 días o más. Si en stock hay un lote a 60 días de vencer, NO entra en candidatos para ese cliente.

## Parámetros activos (cascada de 3 niveles)

| Parámetro | Tabla | Tipo | Rol |
|-----------|-------|------|-----|
| `tolerancia` | `producto` | int | tolerancia general por producto |
| `peso_tolerancia` | `producto` | float | productos con peso variable |
| `temperatura_tolerancia` | `producto` | float | productos refrigerados |
| `dias_vida_defecto_perecederos` | `i_nav_config_enc` | int | default por bodega |
| `tolerancia_dias_vencimiento` | `producto_estado` | int | **por estado del producto** |
| `dias_vencimiento_clasificacion` | `producto_estado` | int | margen por clasificación |
| `Dias_Local`, `Dias_Exterior` | `cliente_tiempos` | int | por cliente + familia + clasificación |

## Tablas involucradas

```
producto                     ← tolerancia general
producto_estado              ← tolerancia por estado (Conforme, Cuarentena, Avería, etc)
i_nav_config_enc             ← default bodega
cliente                      ← cliente del pedido
cliente_tiempos              ← tabla N-a-N: cliente x familia x clasificacion → días tolerados
familia, clasificacion       ← jerarquías de producto
stock                        ← candidatos con Fecha_vence
```

## Pseudo-código del flujo

```python
def reservar_con_tolerancia(pedido_det, pedido_enc, config_enc):
    cliente = get_cliente(pedido_enc.IdCliente)
    producto = get_producto(pedido_det.IdProducto)

    # 1. Calcular días de tolerancia aplicables (CASCADA de precedencia)
    dias_tolerancia = calcular_tolerancia(cliente, producto, config_enc)

    # 2. DiasVencimiento se pasa como parámetro a Reserva_Stock
    # El query interno filtra: WHERE Fecha_vence > GETDATE() + dias_tolerancia
    resultado = Reserva_Stock(
        pStockRes=pedido_det.to_stock_res(),
        DiasVencimiento=dias_tolerancia,  # ← AQUÍ se filtra
        ...
    )
    return resultado


def calcular_tolerancia(cliente, producto, config_enc):
    # Precedencia (hipótesis a confirmar — Q-TOLERANCIA-PRECEDENCIA):
    # 1. cliente_tiempos × producto.familia × producto.clasificacion (más específico)
    # 2. producto_estado.tolerancia_dias_vencimiento (estado actual)
    # 3. producto.tolerancia (general producto)
    # 4. i_nav_config_enc.dias_vida_defecto_perecederos (default bodega)

    # Buscar combinacion mas especifica
    tiempo_cliente = lookup(
        cliente_tiempos,
        IdCliente=cliente.IdCliente,
        IdFamilia=producto.IdFamilia,
        IdClasificacion=producto.IdClasificacion
    )
    if tiempo_cliente:
        return tiempo_cliente.Dias_Local  # o Dias_Exterior si destino exterior

    if producto.tolerancia and producto.tolerancia > 0:
        return producto.tolerancia

    estado_actual = get_estado_default()
    if estado_actual.tolerancia_dias_vencimiento > 0:
        return estado_actual.tolerancia_dias_vencimiento

    return config_enc.dias_vida_defecto_perecederos
```

### Filtro SQL en `clsLnStock.lStock`

```sql
-- Pseudo-SQL del filtro interno
SELECT s.*
FROM stock s
WHERE s.IdProductoBodega = @IdProductoBodega
  AND s.IdBodega = @IdBodega
  AND (
       s.Fecha_vence IS NULL  -- producto no perecedero
    OR DATEDIFF(day, GETDATE(), s.Fecha_vence) >= @DiasVencimiento
  )
  AND s.IdEstado IN (SELECT IdEstado FROM producto_estado WHERE utilizable = 1)
```

## Caso operativo real

**Anécdota Erik (2026-04-29)**:
> "El cliente le pide al WMS la mercancía con cierta condición. Por ejemplo: cereales recibimos con 90 días de vida o más. Entonces el WMS despacha solo aquello que cumpla con esa condición. Lo que está por debajo de esa fecha se discrimina."

### Catálogo `producto_estado` (MERCOPAN, 14 estados)
| IdEstado | Nombre | utilizable | dañado | tolerancia_dias_vencimiento |
|----------|--------|------------|--------|------------------------------|
| 10 | No Encontrado | False | True | 0 |
| 11 | Cuarentena | False | False | 0 |
| 12 | Avería de Importación | True | True | 0 |
| 13 | Conforme | True | False | 0 |
| 14 | No conforme | True | True | 0 |

→ Los estados **utilizable=False** (Cuarentena, No Encontrado) son automáticamente excluidos del candidato pool. **utilizable=True + dañado=True** (Avería, No conforme) probablemente solo entran si el cliente acepta producto dañado.

### Tabla `cliente_tiempos` (N-a-N por familia/clasificación)
```
IdTiempoCliente | IdCliente | IdFamilia | IdClasificacion | Dias_Local | Dias_Exterior
```
- `Dias_Local` para clientes locales (Honduras local, Panamá local, etc)
- `Dias_Exterior` para exportación (vida útil más larga necesaria por logística internacional)

→ **Diseño granular**: cliente A pide cereales con 90 días local pero 120 días exterior. Cliente B pide los mismos cereales con 60 días local. Cada combo independiente.

## Variantes y combinaciones

### Combina con FIFO/FEFO
Sí. La tolerancia filtra el candidato pool, después se aplica rotación. FEFO igual prioriza el más próximo a vencer **dentro del set válido** (ej: si la tolerancia es 90 días, FEFO toma el lote más cercano a 90 días).

### Combina con Clavaud
Sí. La tolerancia se evalúa **antes** del filtro Clavaud (picking sí/no).

### Combina con `IdUbicacionAbastecerCon` (Caso 05)
Sí. Tolerancia primero, después restricción de ubicación.

### Combina con lote numérico (Caso 06)
Sí. Tolerancia primero, después correlativo.

### Producto NO perecedero
`Fecha_vence IS NULL` → no aplica filtro, todo el stock es válido.

## Q-* asociadas

- ✅ **Q-TOLERANCIA-EXISTE** — confirmado: 4 niveles de configuración
- 🟡 **Q-TOLERANCIA-PRECEDENCIA** — orden exacto (cliente_tiempos > producto > producto_estado > config_enc?)
- 🟡 **Q-TOLERANCIA-DIAS-LOCAL-VS-EXTERIOR** — qué disparara el switch entre Dias_Local y Dias_Exterior (¿cliente.tipo? ¿región?)
- 🟡 **Q-DAÑADO-SE-DESPACHA** — ¿`utilizable=True + dañado=True` se despacha o solo se reporta?

## Validación SQL

```sql
-- Productos con tolerancia configurada
SELECT IdProducto, codigo, nombre, tolerancia, peso_tolerancia, temperatura_tolerancia
FROM IMS4MB_MERCOPAN_PRD.dbo.producto
WHERE tolerancia > 0 OR peso_tolerancia > 0 OR temperatura_tolerancia > 0;

-- Estados con tolerancia
SELECT IdEstado, nombre, utilizable, dañado, tolerancia_dias_vencimiento
FROM IMS4MB_MERCOPAN_PRD.dbo.producto_estado
WHERE tolerancia_dias_vencimiento > 0;

-- Cliente_tiempos: matriz cliente × familia × clasificación
SELECT TOP 50 ct.IdCliente, c.codigo, ct.IdFamilia, ct.IdClasificacion,
       ct.Dias_Local, ct.Dias_Exterior
FROM IMS4MB_MERCOPAN_PRD.dbo.cliente_tiempos ct
JOIN IMS4MB_MERCOPAN_PRD.dbo.cliente c ON c.IdCliente = ct.IdCliente
WHERE ct.activo = 1
ORDER BY ct.IdCliente;

-- Default por bodega
SELECT IdBodega, IdEmpresa, IdPropietario, dias_vida_defecto_perecederos
FROM IMS4MB_MERCOPAN_PRD.dbo.i_nav_config_enc;
```
