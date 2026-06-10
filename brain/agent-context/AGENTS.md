---
id: AGENTS
tipo: agent-context
estado: vigente
titulo: AGENTS.md — Contexto del proyecto WMS para agentes locales
ramas: [dev_2028_merge]
tags: [agent-context]
---

# AGENTS.md — Contexto del proyecto WMS para agentes locales

> **Cómo usar este archivo**: copialo (o symlinkealo) a la raíz del repo donde
> corre tu agente local. Los nombres aceptados según herramienta:
>
> | Herramienta            | Nombre del archivo en la raíz |
> |------------------------|-------------------------------|
> | Claude Code            | `CLAUDE.md`                   |
> | OpenCode               | `AGENTS.md`                   |
> | Cursor                 | `.cursorrules`                |
> | Aider                  | `CONVENTIONS.md`              |
> | Cualquier otro         | `AGENTS.md`                   |
>
> El contenido es el mismo. Si trabajás con varios repos, dejá uno en cada raíz.

---

## Identidad y entorno

Sos un agente técnico que trabaja en el proyecto **WMS de Erik Calderón
(PrograX24)**. Stack: **Visual Studio 2026, DevExpress 24, SQL Server**.

### Repos

| Repo            | Lenguaje  | Rol                                                              |
|-----------------|-----------|------------------------------------------------------------------|
| `TOMWMS_BOF`    | VB.NET    | Backend core: DAL, Entity, WebMethods `.asmx`. **El cerebro.**   |
| `TOMHH2025`     | Java      | Frontend Android para terminales handheld (HH).                  |
| `TOMWMS_DBA`   | T-SQL     | Schema y SPs SQL Server productivos.                             |

Branch activa: `dev_2028_merge`.

### Equipo

EJC (Erik), GT, AG, MA, AT, MECR, CF.

---

## Reglas duras (no negociables)

1. **No commits automáticos.** Erik los revisa y los hace él.
2. **No mezclar HH con VB** en un mismo cambio / commit / PR.
3. **No tocar `Reference.vb`** — son proxies SOAP autogenerados; la
   implementación real vive en `*.asmx.vb`.
4. **No reescribir desde cero.** Debuggear primero. Siempre.
5. **UTF-8 con BOM en VB.** Mantener `ñ` y acentos. Nunca guardar como ANSI.
6. **Nunca loguear ni imprimir** `WMS_EC2_DB_PASSWORD` ni
   `BRAIN_IMPORT_TOKEN`. Pasarlos por referencia (`$env:VAR`), nunca el valor.
7. **La conexión a KILLIOS prod es solo lectura** desde este agente.
   Cualquier write lo hace Erik con el procedimiento normal.
8. **El módulo `definition` del catálogo SQL es sensible.** No lo expongas a
   logs ni respuestas que vayan al cliente final.

---

## Herramienta principal — TOMWMS Brain

API privada que indexa los 3 repos + el catálogo SQL productivo. Devuelve
relaciones cross-layer (Java HH → VB WebMethod → SP/tabla SQL).

```
BRAIN_BASE_URL=https://a5b1339e-a6ee-4eb8-bed1-c2bf8103da79-00-2i7pdom0xaba6.janeway.replit.dev/api/brain
```

### Endpoints (GET, sin token)

| Endpoint           | Para qué                                           |
|--------------------|----------------------------------------------------|
| `/health`          | Sanity check. Devuelve totales de símbolos/refs.   |
| `/search`          | Encontrar el símbolo exacto por nombre + filtros.  |
| `/impact`          | Quién depende de mí (callers, blast radius).       |
| `/dependencies`    | Qué necesito yo (callees, upstream).               |
| `/writers`         | Quién escribe esta tabla/SP (insert/update/delete/merge/exec). |

### Endpoints de escritura (POST, requieren `X-Brain-Token`)

| Endpoint                    | Para qué                                  |
|-----------------------------|-------------------------------------------|
| `/import/sql-catalog`       | Subir el catálogo extraído por el script local. |
| `/index/vb`                 | Re-indexar VB tras cambios.               |
| `/index/java`               | Re-indexar Java HH tras cambios.          |
| `/repos/sync`               | Pull de los repos en el server Brain.     |

---

## Workflow obligatorio según el caso

### Caso 1 — Cambio en SP, vista, tabla o columna SQL
```bash
# 1. Localizar el símbolo
curl "$BRAIN_BASE_URL/search?q=VW_Stock_Res&kind=sql-view"

# 2. Blast radius — qué se rompe si lo toco?
curl "$BRAIN_BASE_URL/impact?symbol=VW_Stock_Res&depth=2"

# 3. Si hay writers, listar quién escribe (para entender contratos)
curl "$BRAIN_BASE_URL/writers?symbol=stock&op=update,delete,merge,insert&kind=sql-table"

# 4. Hacer el cambio. Re-correr el extractor SQL local (ver más abajo).
```

### Caso 2 — Refactor de un método VB con muchos llamadores
```bash
curl "$BRAIN_BASE_URL/search?q=Actualizar_Stock&kind=vb-method"
curl "$BRAIN_BASE_URL/impact?symbol=Actualizar_Stock&depth=3"
# Tras el cambio:
curl -X POST "$BRAIN_BASE_URL/index/vb" -H "X-Brain-Token: $BRAIN_IMPORT_TOKEN" \
  -H "Content-Type: application/json" -d '{"repos":["TOMWMS_BOF"]}'
```

### Caso 3 — Rename de un WebMethod (impacto Java HH)
```bash
# Antes del rename, listar Java callers
curl "$BRAIN_BASE_URL/impact?symbol=Get_Ubicacion_By_Codigo_Barra_And_IdBodega&depth=2"
# Renombrar en VB *.asmx.vb (NO en Reference.vb), luego renombrar también
# en TODOS los Java callers que aparecieron, luego re-indexar ambos.
```

### Caso 4 — Discrepancia de datos en producción ("este SKU está mal")
```bash
# Quiénes pueden haber tocado la tabla involucrada?
curl "$BRAIN_BASE_URL/writers?symbol=stock&op=update,delete,merge&kind=sql-table"
# Cruzar con la fecha del Kardex y el método que se ejecutó esa fecha.
# Validar la hipótesis con queries de solo lectura sobre KILLIOS prod.
```

---

## Cuándo NO usar el Brain

- Cambios cosméticos de UI (form layout, labels, colores).
- Edits aislados que no tocan datos ni firmas públicas.
- Bugfixes de lógica interna de un único método sin callers externos.
- Tocar `Reference.vb` (no se debe tocar nunca, ya está dicho).

---

## Conexión a la BD productiva KILLIOS (solo lectura)

> Nota operativa: **Amazon** y **EC2** se refieren al **mismo servidor SQL**
> (`52.41.114.122,1437`). Ahí se cuelgan las BDs y la contraseña canónica es
> `$env:WMS_EC2_DB_PASSWORD`.

```
Server   = 52.41.114.122,1437
Database = TOMWMS_KILLIOS_PRD
User     = wmsuser
Password = $env:WMS_EC2_DB_PASSWORD
```

ConnectionString:
```
Server=52.41.114.122,1437;Database=TOMWMS_KILLIOS_PRD;User Id=wmsuser;Password=$env:WMS_EC2_DB_PASSWORD;Encrypt=no;
```

Desde sqlcmd:
```bash
sqlcmd -S "52.41.114.122,1437" -d TOMWMS_KILLIOS_PRD -U wmsuser \
       -P $env:WMS_EC2_DB_PASSWORD -Q "SELECT TOP 10 * FROM stock"
```

> **Importante**: cualquier query que ejecutes contra esta BD debe ser
> `SELECT`. Si necesitás escribir, parar y avisarle a Erik.

---

## Variables de entorno requeridas

PowerShell (sesión actual):
```powershell
$env:BRAIN_BASE_URL          = "https://a5b1339e-a6ee-4eb8-bed1-c2bf8103da79-00-2i7pdom0xaba6.janeway.replit.dev/api/brain"
$env:BRAIN_IMPORT_TOKEN      = "<pegar valor del panel de Secrets de Replit>"
$env:WMS_KILLIOS_DB_HOST     = "52.41.114.122,1437"
$env:WMS_KILLIOS_DB_NAME     = "TOMWMS_KILLIOS_PRD"
$env:WMS_KILLIOS_DB_USER     = "wmsuser"
$env:WMS_EC2_DB_PASSWORD = "<el mismo que usás en SSMS>"
$env:AZURE_DEVOPS_PAT        = "<solo si el agente clona/pulls los repos>"
```

Para que persistan entre sesiones (abre nueva terminal después):
```powershell
setx BRAIN_BASE_URL          "https://a5b1339e-a6ee-4eb8-bed1-c2bf8103da79-00-2i7pdom0xaba6.janeway.replit.dev/api/brain"
setx BRAIN_IMPORT_TOKEN      "<valor>"
setx WMS_KILLIOS_DB_HOST     "52.41.114.122,1437"
setx WMS_KILLIOS_DB_NAME     "TOMWMS_KILLIOS_PRD"
setx WMS_KILLIOS_DB_USER     "wmsuser"
setx WMS_EC2_DB_PASSWORD "<valor>"
```

---

## Re-extraer y subir el catálogo SQL al Brain

Ejecutar tras cambios de schema en producción (nuevo SP, vista modificada,
columna nueva). El extractor corre **local** porque la BD prod está detrás del
firewall de Erik.

```powershell
cd <repo>\tools\sql-catalog
pip install pyodbc requests          # una sola vez

python extract_sql_catalog.py `
    --server   $env:WMS_KILLIOS_DB_HOST `
    --database $env:WMS_KILLIOS_DB_NAME `
    --user     $env:WMS_KILLIOS_DB_USER `
    --upload   "$env:BRAIN_BASE_URL/import/sql-catalog"
```

El script lee la password de `$env:WMS_EC2_DB_PASSWORD` y el token de
`$env:BRAIN_IMPORT_TOKEN`. Idempotente: cada run reemplaza el catálogo del
repo virtual `TOMWMS_KILLIOS_PRD`.

---

## Reglas Transversales de Calidad y Operación (TOMWMS Brain)

### Regla A — Trazas Finas Operativas
Antes de debuggear o proponer cambios en procesos clave, **se DEBE consultar el Grafo de Traza Fina** correspondiente en `brain/handoffs/2026-05-22-codex-performance-bof-hh/`.
- Procesos con traza: Recepción, Picking, Packing, Verificación, Reemplazo, Existencias, Inventario Cíclico.
- **Why:** Permite identificar el flujo exacto HH -> WS -> DAL -> BD y localizar el nodo de falla sin adivinar.

### Regla B — Inventario y Movimientos (UMBAS e Idempotencia)
1. **UMBAS Siempre:** Toda cantidad en `trans_movimientos.cantidad`, `stock` y `stock_res` debe estar en Unidad de Medida Básica. Conversión: `Cantidad_Presentacion * Factor`.
2. **Idempotencia:** Antes de insertar en `trans_movimientos` (especialmente `VERI`), validar existencia previa por llave lógica (Pedido, Producto, Lote, FechaVence, Ubicación, Licencia) y cantidad.
3. **Dañado_picking (BUG-001):** Si marcas `Dañado_picking = True` en `trans_picking_ubic`, **DEBES** generar el descuento de stock (`AJCANTN`). No hacerlo genera stock fantasma.

### Regla C — Robustez en Java HH (Quality Sweeper)
1. **Validación de Strings:** Preferir `txt.getText().toString().isEmpty()` o `.equals("")` sobre comparaciones `== null` (que suelen ser falsos negativos en Android).
2. **Blindaje de Listas:** Siempre verificar `if (lista != null && lista.items != null && !lista.items.isEmpty())` antes de acceder a `items.get(0)`. REGLA 7 de `domain-hh-android.yml`.

### Regla D — SQL y Collation
En cruces de columnas de texto (`Lic_plate`, `Lote`, `Barra`, `CodigoProducto`), usar siempre `COLLATE DATABASE_DEFAULT` para evitar conflictos entre bases de datos de clientes con diferentes collations.

---

## Recepcion HH — Reglas BUG-003

### Regla 1 — pLineaOC vs pIdRecepcionDet (NO confundirlas nunca)
En `frm_recepcion_datos.java`, dentro de `Carga_Datos_Producto*`:
- `pIdRecepcionDet = max(pListTransRecDet.items.IdRecepcionDet) + 1` // ID local del det, OK
- `pLineaOC = BeOcDet.No_Linea` // No_Linea REAL de la OC

**Antipatron a detectar (grep periodico — es BUG-003 reincidente):**
`pLineaOC = stream(pListTransRecDet.items).max(c->c.IdRecepcionDet>0).IdRecepcionDet+1`

**Why:** El BOF (`Asignar_IdRecepcionDet_StockRec`) matchea det<->stock_rec por `stk.No_linea = det.No_Linea`. Si `pLineaOC` recibe un ID local del det en vez del `No_Linea` real, el match falla y depende del fallback Intento 2, que tambien falla si la OC tiene legitimamente `No_Linea=0` (confirmado en MAMPA_QA OC 156).

### Regla 2 — Guardar_Caja_Master_enMemoria filtra por producto, no copia toda la OC
Al construir `AuxListStockRec` dentro del loop de `Guardar_Caja_Master_enMemoria`, copiar SOLO el `stock_rec` cuyo (`IdProductoBodega`, `Talla`, `Color`) coincide con `Auxredet`. NUNCA copiar `pListBeStockRec` completa.

**Patron correcto:**
```java
for (clsBeStock_rec s : pListBeStockRec.items) {
    if (s.IdProductoBodega == Auxredet.IdProductoBodega
            && s.Talla.equals(Auxredet.Talla.Codigo)
            && s.Color.equals(Auxredet.Color.Codigo)) {
        s.IdRecepcionDet = Auxredet.IdRecepcionDet; // Mas robusto que index + 1
        AuxListStockRec.items.add(s);
        break;
    }
}
```

**Why:** Evita arrastrar excedentes de otros productos de la misma OC al procesar una caja master específica.

### Regla 3 — Cuando salga un metodo nuevo de carga de producto
Si se agrega un nuevo metodo en `frm_recepcion_datos.java` que asigne `pLineaOC`, verificar que use `BeOcDet.No_Linea` (field de clase, siempre disponible tras `gl.gselitem`) o equivalente del scope. Patron referencia ya correcto: L3332.

---

## Commits de Referencia (BUG-003)

- **2513cddd**: Guardar_Caja_Master_enMemoria copia solo stock_rec del producto actual.
- **eb739365**: pLineaOC = BeOcDet.No_Linea en TipoOpcion=2.
- **4fa4c2bf (BOF)**: Match det<->stock_rec por No_Linea en Asignar_IdRecepcionDet_StockRec.

---

## Limitaciones conocidas del Brain

- **SP refs por variable** en VB: solo se capturan los SP cuyo nombre aparece
  como string literal. Los que se construyen en variable no salen como
  referencia.
- **`Reference.vb`** aparece en los resultados (es código indexable); ignoralo.
- **127 dependencias ambiguas** en el catálogo SQL (refs cross-DB o dinámicas).
  Es esperable.
- **OUTPUT clauses, CTEs que escriben, y SQL dinámico** no están cubiertos por
  el Movement Tracer; si encontrás un caso real, avisar para extender el parser.

---

## Antes de cerrar una tarea

1. ¿Re-indexaste el repo afectado? (`POST /index/vb` o `/index/java`).
2. ¿Verificaste el `/impact` post-cambio para confirmar que no quedaron
   referencias colgantes?
3. ¿Dejaste el código en UTF-8 con BOM?
4. ¿Documentaste el cambio en el ticket / nota interna?
5. **No commits automáticos.** Pasale el patch a Erik.

