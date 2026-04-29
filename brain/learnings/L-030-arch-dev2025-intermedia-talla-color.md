# L-030 — ARCH: rama `dev_2025` fue intermedia para talla x color MAMPA, hoy historica

> Etiqueta: `L-030_ARCH_DEV2025-INTERMEDIA-TALLA-COLOR_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 10, respuesta Carolina a Q-DEV2025-PROPOSITO

## Hallazgo

La rama `dev_2025` en los repos `TOMWMS_BOF` y `TOMHH2025` **fue una
rama intermedia** entre `dev_2023_estable` y `dev_2028_merge`. Su
proposito original fue desarrollar el cambio de talla x color para
MAMPA, que era un cambio drastico en el manejo de inventario.

Cita literal Carolina:

> "Si es correcto, fue una rama Intermedia entre 2023 y 2028, la 2025
> fue donde se comenzaron a trabajar los cambios para MAMPA de talla
> color, porque este cambio era bastante drastico en cuanto al manejo
> de inventario y de todas las transacciones."

## Cronologia inferida

```
Tiempo →
─────────────────────────────────────────────────
dev_2023_estable     ●─────────────────────────────────
                            \
dev_2025 (intermedia)        ●─── feature talla×color MAMPA ───●
                                                                 \  (squash + merge)
dev_2028_merge               ←──────────────────────────────●─────●─── ... ─→ MAMPA prod
```

## Aparente contradiccion con Wave 6.2

En Wave 6.2 quick-wins se documento que `dev_2023_estable`,
`dev_2025` y `master` apuntan al **mismo commit** (`1f5cc2c4`), lo
que se interpreto como "alias del mismo HEAD".

Esa interpretacion era **incompleta**. Lo correcto es:

- `dev_2023_estable` = rama estable de la version 2023 (HEAD activo).
- `dev_2025` = rama que **tuvo trabajo unico de talla x color**. Cuando
  el feature termino y se mergeo (probable squash) a
  `dev_2028_merge`, el HEAD de `dev_2025` quedo congelado en el
  ultimo commit ANTES del squash. Ese commit puede coincidir con
  `dev_2023_estable` si la base original fue alli y los cambios
  unicos del feature ya viven en otra rama.
- `master` = release oficial congelada (L-031).

→ La coincidencia de SHA no implica que `dev_2025` "no tuvo vida
propia". Tuvo vida propia, vive ahora en `dev_2028_merge`. El SHA
visible es post-cleanup.

→ Q-DEV2025-COMMIT-DRIFT (cuestionario bloque 14): para confirmar este
modelo habria que mirar reflog de `dev_2025` o tags intermedios.

## Lo que cambio talla x color en MAMPA

Carolina describe el cambio como "drastico en cuanto al manejo de
inventario y de todas las transacciones". Hipotesis especificas:

1. **Cardinality del producto**: antes un SKU era atomico. Despues, un
   SKU "remera" se descompone en (talla, color) → matriz N×M de
   variantes inventariables.
2. **Tabla `producto_presentacion`** probablemente recibio columnas
   `IdTalla`, `IdColor` (o equivalente). Ver heat-map Wave 5.
3. **Tablas de stock + reservas + transacciones** tuvieron que aceptar
   filtros por talla x color en todas las queries de busqueda.
4. **HH** tuvo que mostrar selectores adicionales en formularios de
   recepcion / picking / despacho cuando el producto tiene talla x
   color (`PTiene_TallaColor`).
5. **Sincronizacion ERP**: SAPBOSyncMampa.exe tuvo que mapear las
   variantes a la representacion de SAP B1 (probable que SAP use
   "matrix items" o codigos compuestos).

→ Q-TALLA-COLOR-IMPACTO-TABLAS (a abrir si se quiere mapear el alcance
exacto del cambio).

## Estado actual de `dev_2025`

- **No tiene commits nuevos desde el cierre del feature talla x color**.
- **No se elimina** porque mantiene historial de la rama feature.
- **No se documenta como activa** en RAMAS_Y_CLIENTES.md a partir de
  Wave 10.

Recomendacion de Erik / Carolina: dejar como historica, no
mantener. Si se necesita volver a esa epoca, usar el SHA o crear
tag `v2025-talla-color-base`.

## Implicaciones para el brain

### 1. RAMAS_Y_CLIENTES.md actualizado
La seccion de ramas activas debe diferenciar:
- **Activas**: `dev_2023_estable`, `dev_2028_merge`, `dev_2028_Cumbre`
- **Historicas (no se tocan)**: `dev_2025`, `master`
- **Historicas por cliente HH (a eliminar)**: `byb`, `cealsa`,
  `mercopan`, `mercosal`, `240byb`, `240Cealsa`, etc. (ver L-032)

### 2. Para inferir cuando empezo el desarrollo 2028
Antes de Wave 10, se asumia que el desarrollo 2028 nacio "fresh".
Ahora se sabe que hubo un trampolin via `dev_2025` (feature
talla x color). El primer commit de `dev_2028_merge` probablemente es
el merge del feature.

### 3. Para el scanner `scan-comments-tree-map`
Los comentarios firmados con fechas 2024-2025 que mencionan
"talla", "color", "matriz", "variante" probablemente vienen del
feature `dev_2025` y son ejemplos de comentarios de ALTO valor para
el shortlist.

## Cierra Q-*

- `Q-DEV2025-PROPOSITO` (Bloque 1, media) — RESUELTA.

## Refuerza / matiza

- Wave 6.2 hallazgo "rama master + dev_2023_estable + dev_2025 mismo
  commit" — sigue siendo cierto en cuanto al SHA visible, pero la
  interpretacion debe matizarse: `dev_2025` SI tuvo vida propia, su
  contenido se promovio a `dev_2028_merge`.

## Q-* nueva derivada

- Q-DEV2025-COMMIT-DRIFT (cuestionario Q81): explicar por que el HEAD
  visible de `dev_2025` apunta al mismo commit que `dev_2023_estable`
  si tuvo trabajo de talla x color.
