---
id: 04-caso-explosion-cajas-a-unidades
tipo: wms-test-natural-case
estado: vigente
titulo: CASO 04 — Explosión automática (cajas → unidades)
tags: [wms-test-natural-case]
---

# CASO 04 — Explosión automática (cajas → unidades)

## Trigger

Pedido en UMB (Unidad de Medida Básica) cuya conversión a presentación da un **número fraccionario**.
Ejemplo:
- Pedido: 124 unidades de "Aceite 1L"
- Presentación: caja con `factor = 12` (12 unidades por caja)
- Conversión: `124 / 12 = 10.333...` cajas
- WMS NO puede tomar 10.333 cajas. Decisión: tomar **10 cajas completas** + remanente de **(0.333 × 12) = 4 unidades sueltas**

## Parámetros activos

| Parámetro | Tabla | Tipo | Rol |
|-----------|-------|------|-----|
| `explosion_automatica` | `i_nav_config_enc` | bit | gate global |
| `explosion_automatica_desde_ubicacion_picking` | `i_nav_config_enc` | bit | permite explotar desde picking |
| `explosion_automatica_nivel_max` | `i_nav_config_enc` | int | **límite de nivel para explotar** |
| `convertir_decimales_a_umbas` | `i_nav_config_enc` | bit | convierte cantidades decimales |
| `reservar_umbas_primero` | `i_nav_config_enc` | bit | prioriza UMB antes que pallet |
| `factor` | `producto_presentacion` | float | conversión |

## Tablas involucradas

```
i_nav_config_enc            ← gate y nivel max
producto_presentacion       ← factor para split
producto                    ← Tiene_Control_Por_Peso? (decimales OK?)
bodega_ubicacion            ← nivel para validar explosion_automatica_nivel_max
stock                       ← candidatos por presentación
stock_res                   ← reserva fragmentada (1 fila por presentación)
```

## Pseudo-código del flujo

```python
def reservar_con_explosion(pedido_det, config_enc, bodega):
    presentacion = get_presentacion(pedido_det.IdPresentacion)
    factor = presentacion.factor

    # 1. Calcular en UMB
    cantidad_umb_total = pedido_det.cantidad * factor

    # 2. Split entero / decimal
    cantidad_entera_pres, cantidad_decimal_pres = split_decimal(pedido_det.cantidad)
    cantidad_umb_decimal = math.ceil(cantidad_decimal_pres * factor)

    # 3. Reservar la parte ENTERA (10 cajas completas) usando ubicaciones que aceptan pallet/caja
    if cantidad_entera_pres > 0:
        Reservar_Stock(
            cantidad=cantidad_entera_pres,
            presentacion=presentacion.IdPresentacion,
            buscar_en="ubicaciones que aceptan caja"  # típicamente cualquier nivel
        )

    # 4. Reservar el REMANENTE (4 unidades sueltas) con explosión
    if cantidad_umb_decimal > 0:
        if not config_enc.explosion_automatica:
            raise Exception("Pedido tiene fracción pero explosión está apagada")

        # ¿Desde dónde se permite explotar?
        if config_enc.explosion_automatica_desde_ubicacion_picking:
            # Buscar primero en picking (nivel 1) — caja abierta lista para unidades
            buscar_en_picking_primero = True

        # Validar nivel_max
        nivel_max = config_enc.explosion_automatica_nivel_max
        # Solo se puede explotar caja en ubicaciones con nivel <= nivel_max
        # Ej: si nivel_max = 1, solo se puede romper caja en picking (nivel 1)
        # Si nivel_max = 0, no se puede romper caja en ninguna parte → falla
        # Si nivel_max = 6, se puede romper caja en cualquier nivel (mala idea operativamente)

        Reservar_Stock(
            cantidad=cantidad_umb_decimal,
            presentacion=0,  # UMB suelta
            filtro_ubicacion=f"nivel <= {nivel_max}",
            buscar_en_picking_primero=buscar_en_picking_primero
        )

    # 5. La reserva queda fragmentada en N filas de stock_res
    return reservas
```

### Manejo del Tiene_Control_Por_Peso

```python
if not Tiene_Control_Por_Peso_By_IdProductoBodega(IdProductoBodega):
    # Producto NO va por peso variable: forzar a entero
    if Integer.TryParse(vCantidadSolicitadaPedido, out vEntero):
        vCantidadSolicitadaPedido = vEntero
    else:
        vCantidadSolicitadaPedido = Math.Truncate(vCantidadSolicitadaPedido)
else:
    # Producto va por peso: permitir 6 decimales
    vCantidadSolicitadaPedido = Math.Round(vCantidadSolicitadaPedido, 6)
```

## Caso operativo real

**Anécdota Erik (2026-04-29)**:
> "Cuando no teníamos el parámetro `nivel_max_explosion`, el WMS mandaba a nivel 2 a sacar unidades. Imaginate: el operador va con el montacarga al rack, baja el pallet entero, lo abre arriba, saca 4 unidades sueltas, deja el pallet abierto en la nada... un lío. Pero el flujo pesado ya existía. Tuvimos que modelar un parámetro encima de lo que ya teníamos como proceso de reserva para 'desviar el río'. Le dijimos: si estás en `nivel_max_explosion = 1`, entonces a partir de ese nivel ya no se puede explosionar. No era la mejor solución, lo admito. Pero comprendeme, éramos pocos y el trabajo mucho, como siempre."

### Estado del parámetro en producción
- **MERCOPAN**: `explosion_automatica_nivel_max = ?` (consultar)
- **CEALSA**: `explosion_automatica_nivel_max = ?` (consultar)
- **MERHONSA**: tiene **DOS columnas** por typo histórico — `explosion_automatica_nivel_max` Y `explosio_automatica_nivel_max` (sin la 'n' final). Ambas conviven en el schema. Q-* abierta sobre cuál se lee.

## Variantes y combinaciones

### Con `explosion_automatica = False`
Si el flag está OFF, el WMS rechaza el pedido fraccionario o pide al usuario que ajuste la cantidad. **Q-EXPLOSION-OFF-COMPORTAMIENTO**: ¿cuál de los dos?

### Con `reservar_umbas_primero = True`
Invierte el orden: primero busca UMB sueltas (típicamente en picking), después completa con cajas.

### Con `convertir_decimales_a_umbas = True`
Forzar siempre conversión a UMB, ignorando `factor` para reservar.

### Con Clavaud
La parte ENTERA (10 cajas → 1 pallet completo) puede ser sujeta a Clavaud si el setup lo activa. La parte REMANENTE (4 unidades) sale igual de picking.

## Q-* asociadas

- ✅ **Q-EXPLOSION-EXISTE** — confirmado: 4 columnas en `i_nav_config_enc` lo gobiernan
- 🟡 **Q-EXPLOSION-NIVEL-MAX-DEFAULT** — ¿qué valor recomienda Erik por cliente?
- 🟡 **Q-MERHONSA-DOBLE-COLUMNA-EXPLOSION** — ¿cuál de las dos columnas se lee en MERHONSA (typo)?
- 🟡 **Q-EXPLOSION-OFF-COMPORTAMIENTO** — qué pasa si flag OFF y pedido es fraccionario
- 🟡 **Q-EXPLOSION-VS-CLAVAUD** — interacción entre Clavaud y explosión cuando aplican ambos

## Validación SQL

```sql
-- Configuración actual de explosión por bodega
SELECT IdBodega, IdEmpresa, IdPropietario,
       explosion_automatica,
       explosion_automatica_desde_ubicacion_picking,
       explosion_automatica_nivel_max,
       convertir_decimales_a_umbas,
       reservar_umbas_primero
FROM IMS4MB_MERCOPAN_PRD.dbo.i_nav_config_enc;

-- Detectar pedidos con explosión real (heurística: misma línea de pedido con stock_res
-- en múltiples presentaciones)
SELECT sr.IdPedidoEnc, sr.IdPedidoDet, COUNT(DISTINCT sr.IdPresentacion) n_presentaciones
FROM IMS4MB_MERCOPAN_PRD.dbo.stock_res sr
GROUP BY sr.IdPedidoEnc, sr.IdPedidoDet
HAVING COUNT(DISTINCT sr.IdPresentacion) > 1;
```

## Referencias en código

```vb
' DMS/Formas/Mantenimientos/Configuracion_Interface/frmConfiguracionHorarios.vb:177-180
BeConfigEnc.Explosion_Automatica = chkExplosionAutomaticaInterface.Checked
BeConfigEnc.Explosion_Automatica_Desde_Ubicacion_Picking = chkExplosionAutomaticaUbicacionPicking.Checked
BeConfigEnc.Explosion_Automatica_Nivel_Max = txtNivelMaximoExplosionAuto.EditValue

' DynamicsNavInterface/.../clsSyncNavEnvioAlm.vb:1194 y 2750
If BeConfigEnc.Explosion_Automatica Then
    ' ... aplica explosion
End If
```
