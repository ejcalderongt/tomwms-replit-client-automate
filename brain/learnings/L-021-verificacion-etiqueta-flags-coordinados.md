# L-021 — Verificacion etiqueta NO es un flag, es un sistema coordinado

> Etiqueta: `L-021_FEAT_VERIF-FLAGS-COORDINADOS_APPLIED`
> Fecha: 29-abr-2026
> Origen: cierre parcial Q-CAPABILITY-FLAG-VERIF + caso K7 (donde
> `tipo_pantalla_verificacion=0` pero verificacion SI esta activa)

## Hallazgo

La activacion del modulo de verificacion etiqueta NO se controla con
un solo flag tipo `verificacion_activa`. Son al menos 9 columnas
coordinadas en 3 tablas distintas.

## Las 9 columnas

### En `bodega`

| Columna | Tipo | Significado |
|---|---|---|
| `tipo_pantalla_verificacion` | int | **Tipo** de pantalla (0 = legacy / 1 = nueva). NO ON/OFF |
| `IdTipoEtiquetaVerificacion` | int | Tipo de etiqueta usado en verificacion |
| `impresion_verificacion` | bit | Si imprime etiqueta al verificar |
| `operador_picking_realiza_verificacion` | bit | Si el mismo operador del pick verifica |
| `permitir_reemplazo_verificacion` | bit | Si permite reemplazar etiquetas en verificacion |
| `Permitir_Verificacion_Consolidada` | bit | Habilita modo consolidado |
| `verificacion_consolidada` | bit | Modo consolidado activo |

### En `operador`

| Columna | Tipo | Significado |
|---|---|---|
| `verifica` | bit | El operador X tiene permiso para verificar |

### En `cuadrilla_tipo` (cuando aplica)

| Columna | Tipo | Significado |
|---|---|---|
| `es_verificacion` | bit | El tipo de cuadrilla esta dedicado a verificacion |

## Implicacion clave

`bodega.tipo_pantalla_verificacion = 0` **NO significa "verificacion off"**.
Significa "pantalla legacy". K7 tiene este flag en 0 en TODAS sus 6
bodegas y aun asi tiene 7 pedidos en estado `Verificado` (screenshot
Erik 29-abr corroboro la operacion ON).

La verificacion REAL se activa cuando:
1. Hay al menos 1 `operador.verifica = True` para esa bodega.
2. (Opcional) Hay tipo de cuadrilla con `es_verificacion = True`.
3. El estado del pedido pasa por `Pickeado` → `Verificado` → `Despachado`.

## Patron de lectura para WebAPI

```csharp
bool BodegaTieneVerificacion(int idBodega)
{
    return _db.Operador
        .Where(o => o.idBodega == idBodega && o.verifica == true)
        .Any()
    || _db.CuadrillaTipo
        .Where(ct => ct.es_verificacion == true && ct.idBodega == idBodega)
        .Any();
}
```

## Cierre

- Q-CAPABILITY-FLAG-VERIF: cerrada con respuesta NO TRIVIAL.
- Q-VERIF-K7-PERIODOS: parcialmente respondida — la intermitencia
  probablemente depende de cuantos `operador.verifica=True` hay en
  cada periodo (si retiraron el permiso, el modulo queda dormido).
  PROXIMO PASO: medir en EC2 K7 cuantos operadores tienen `verifica=True`
  hoy.

## Pendientes

- Medir distribucion de `operador.verifica` por cliente.
- Documentar transicion de estados de pedido segun `verifica`.
- Reclasificar fingerprints de cada cliente para usar este modelo
  coordinado en vez del flag crudo `tipo_pantalla_verificacion`.
