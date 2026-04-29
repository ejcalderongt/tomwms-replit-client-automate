---
id: pedido-extraccion-hh-cest
tipo: contrato_extraccion_consumidor
caso: CP-013
wave_origen: wave-13-10
wave_destino: wave-13-11
autor: "@agent"
fecha: 2026-04-29
estado: pendiente_de_emision_bundle
destinatario: erik_consumidor_via_VS_TOMHH2025
---

# Contrato de extracción — HH Android, path CEST

## Por qué este contrato

El productor (agente Replit) **no tiene clonado** el repo `TOMHH2025` por decisión arquitectónica documentada en `entregables_ajuste/AGENTS.md` del repo de intercambio. Para avanzar Wave 13-11 (confirmar hipótesis H1 y H4 del bug raíz V-DATAWAY-004), el consumidor (Erik desde su VS) tiene que extraer los snippets descritos abajo y empujarlos como bundle al repo de intercambio.

## Bug bajo investigación

`V-DATAWAY-004`: el path CEST (Cambio de Estado, `IdTipoTarea=3` en `sis_tipo_tarea_hh`) inserta una fila nueva en `stock` cuando debería consolidar contra una fila preexistente con la misma llave natural `(IdProductoBodega, IdUbicacion, IdProductoEstado, Lote, lic_plate)`.

Caso fundacional: CP-013 / Killios WMS164 / 23-abr-2026 / 919 filas afectadas (18.7% del stock activo de Killios).

## Hipótesis a confirmar/refutar con el bundle

| ID | Hipótesis | Qué buscar en el código |
|---|---|---|
| H1 | `lic_plate` NULL/vacío rompe el comparador | si el `WHERE` del lookup hace `lic_plate = @lp` directo (vulnerable) o `ISNULL(lic_plate,'') = ISNULL(@lp,'')` (defensivo) |
| H4 | Fallback INSERT cuando `UPDATE Cantidad=0` rechazado por check | si hay `try/catch` alrededor del UPDATE de stock origen + branch INSERT en el catch |
| H2 | Concurrencia inter-segundo, dos hilos CEST sin lock | si el `SELECT` del lookup tiene `WITH (UPDLOCK, HOLDLOCK)` o equivalente |
| H3 | CEST por lote partido HH | si el flujo HH permite confirmar el CEST en dos eventos separados sobre la misma posición destino |

## Qué archivos extraer

El método del CEST en HH Android probablemente vive en uno de estos lugares (nombres tentativos basados en convenciones que vimos en BOF):

### Nivel 1 — UI / pantalla del CEST en la HH

Buscar en `TOMHH2025/app/src/main/java/.../activity/` (o equivalente Kotlin):

- Cualquier `Activity` o `Fragment` con nombre que contenga: `CEST`, `CambioEstado`, `Cambio_Estado`, `ChangeState`, `EstadoChange`.
- En particular el handler que reacciona al barcode scan + confirmación de cantidad.

### Nivel 2 — Service / repository del stock

Buscar en `TOMHH2025/app/src/main/java/.../service/` o `.../repository/` o `.../dao/`:

- Cualquier clase con nombre que contenga: `Stock`, `StockRepository`, `StockDao`, `StockService`, `LnStock`, `clsLnStock` (si es port directo del BOF).
- Métodos con nombre tipo: `Insertar_CEST`, `InsertarCEST`, `UpsertStock`, `MoverStock`, `MoverEstado`, `CambiarEstado`, `ConsolidarStock`, `MergeStock`.

### Nivel 3 — Layer SQL / queries

Buscar:

- Strings SQL embebidos que mencionen `INSERT INTO stock` o `UPDATE stock`.
- Stored procedures invocados desde HH para hacer el CEST. Si existen y se invocan via `CallableStatement`/`Cursor`, extraer también el SP completo del lado SQL.
- Archivos `.sql`, `.xml` (si usan Room/iBatis), `.kt`/`.java` con SQL inline.

## Formato del bundle

Seguir la convención existente del repo de intercambio (`entregables_ajuste/AGENTS.md` apartado "Ubicación de los bundles"):

```
entregables_ajuste/2026-04-XX/v24_bundle/
├── MANIFEST.json                      # con campo "tipo": "extraccion-readonly"
├── README.md                          # qué se extrajo y por qué
├── snippets/
│   ├── 01-activity-cest.kt            # archivo completo, NO patch
│   ├── 02-stock-repository.kt
│   ├── 03-stock-dao.kt
│   ├── 04-sp-insertar-cest.sql        # si existe SP
│   └── 05-otros-relevantes.kt
└── notas-erik.md                      # observaciones del consumidor
```

**Importante**: este bundle es de tipo **extracción read-only** — NO genera patches contra `TOMHH2025`, NO se aplica a ningún lado. Es sólo entrega de archivos para análisis. El campo `MANIFEST.json` debe declarar `"tipo": "extraccion-readonly"` para que el flujo `apply_bundle.mjs` lo skipee.

## Snippets críticos a marcar

Una vez los archivos están en el bundle, marcar (con comment al inicio del bloque) los snippets críticos:

```kotlin
// >>> WAVE-13-11 H1 H4 INSERT-VS-UPDATE BLOCK START
fun insertarCest(...) {
    val existente = dao.buscarStockExistente(
        idProductoBodega, idUbicacion, idProductoEstado, lote, licPlate
    )
    if (existente == null) {
        dao.insertarStock(...)        // ← branch INSERT
    } else {
        dao.actualizarCantidad(...)   // ← branch UPDATE
    }
}
// <<< WAVE-13-11 H1 H4 INSERT-VS-UPDATE BLOCK END
```

Y el método del DAO:

```kotlin
// >>> WAVE-13-11 H1 LOOKUP COMPARATOR START
fun buscarStockExistente(...): Stock? {
    return db.rawQuery("""
        SELECT * FROM stock
        WHERE IdProductoBodega = ?
          AND IdUbicacion = ?
          AND IdProductoEstado = ?
          AND lote = ?
          AND lic_plate = ?         -- <-- AQUI: H1 dice que esto es vulnerable a NULL
    """, ...)
}
// <<< WAVE-13-11 H1 LOOKUP COMPARATOR END
```

## Sensibilidad

El código de TOMHH2025 puede contener credenciales/connection strings. **Antes de empujar el bundle**, redactar (reemplazar con `<REDACTED>`) cualquier:

- Connection string a SQL Server.
- API key o token.
- Path absoluto del entorno de Erik (Windows usernames, GUIDs de máquina).
- Usuarios/passwords hardcodeados.

## Estimación de esfuerzo

Para un consumidor familiarizado con el repo: 30 minutos a 2 horas, dependiendo de cuán dispersa esté la lógica del CEST. Si la lógica está en un solo archivo (`StockRepository.kt` o equivalente), 30 minutos. Si está dispersa entre Activity + Service + DAO + SP, 2 horas.

## Una vez emitido el bundle

El productor (yo) en Wave 13-11:

1. `git pull` del repo de intercambio.
2. Lee el bundle `v24_bundle/snippets/`.
3. Confronta con H1, H2, H3, H4 — confirma o refuta cada una con cita archivo + línea.
4. Si confirma: abre `V-DATAWAY-004.md` formal en `brain/dataway-analysis/04-ecuacion-de-balance/`.
5. Si refuta todo: replantea hipótesis y emite Wave 13-12 con nuevo contrato de extracción.

## Cross-refs

- `brain/debuged-cases/CP-013-killios-wms164/REPORTE-wave-13-10.md` — análisis estructural que motiva este pedido
- `dataway-analysis/04-ecuacion-de-balance/anti-patron-insert-stock-sin-merge.md` — V-DATAWAY-004
- `entregables_ajuste/AGENTS.md` (rama `main` del repo de intercambio) — contrato general de bundles
