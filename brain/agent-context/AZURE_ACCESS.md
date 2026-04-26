# Azure DevOps Access — Productor Replit

**Estado**: validado 2026-04-26 desde container Replit.
**Alcance**: solo LECTURA. Cero permisos de write. Cero `git push` a Azure.

---

## 1. Repos accesibles

| URL clone HTTPS | Tamaño | Default branch | Branch trabajo |
|---|---|---|---|
| `https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_git/TOMWMS_BOF` | 376 MB | `master` | `dev_2028_merge` |
| `https://dev.azure.com/ejcalderon0892/TOMHH2025/_git/TOMHH2025` | 14 MB | `master` | `dev_2028_merge` |

**Default branch en Azure es `master`** pero la rama de trabajo del equipo es `dev_2028_merge`. Siempre pasar `versionDescriptor.version=dev_2028_merge` en queries de API si querés ver el estado actual del equipo.

`TOMWMS_DBA` **no existe en Azure DevOps**. Vive en GitHub `ejcalderongt/DBA`.

---

## 2. Auth (header HTTP)

```bash
AUTH=$(printf ":%s" "$AZURE_DEVOPS_PAT" | base64 -w0)
```

Después se usa como:

```bash
curl -H "Authorization: Basic $AUTH" ...
git -c http.extraHeader="Authorization: Basic $AUTH" ...
```

**NUNCA** loguear, imprimir ni embeber el PAT en código. Solo referencia por nombre `AZURE_DEVOPS_PAT`.

---

## 3. API REST (preferida — sin clonar)

Base por repo: `https://dev.azure.com/ejcalderon0892/<PROYECTO>/_apis/git/repositories/<REPO>`

### 3.1 Listar árbol

```bash
# Subárbol de Transacciones, recursivo
curl -s -H "Authorization: Basic $AUTH" \
  "https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_apis/git/repositories/TOMWMS_BOF/items?scopePath=/TOMIMSV4/Transacciones&recursionLevel=Full&versionDescriptor.version=dev_2028_merge&versionDescriptor.versionType=branch&api-version=7.0"
```

`recursionLevel` puede ser `OneLevel` (solo hijos directos) o `Full` (todo el subárbol).

### 3.2 Bajar contenido de un archivo

```bash
curl -s -H "Authorization: Basic $AUTH" \
  "https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_apis/git/repositories/TOMWMS_BOF/items?path=/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb&versionDescriptor.version=dev_2028_merge&versionDescriptor.versionType=branch&\$format=text&api-version=7.0"
```

`\$format=text` devuelve el contenido crudo. Sin él, devuelve JSON con metadata.

### 3.3 Diff entre commits

```bash
curl -s -H "Authorization: Basic $AUTH" \
  "https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_apis/git/repositories/TOMWMS_BOF/diffs/commits?baseVersion=<sha_base>&targetVersion=<sha_target>&api-version=7.0"
```

### 3.4 Listar commits recientes de la rama

```bash
curl -s -H "Authorization: Basic $AUTH" \
  "https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_apis/git/repositories/TOMWMS_BOF/commits?searchCriteria.itemVersion.version=dev_2028_merge&searchCriteria.\$top=20&api-version=7.0"
```

### 3.5 Listar repos del proyecto

```bash
curl -s -H "Authorization: Basic $AUTH" \
  "https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_apis/git/repositories?api-version=7.0"
```

### 3.6 Listar proyectos de la org

```bash
curl -s -H "Authorization: Basic $AUTH" \
  "https://dev.azure.com/ejcalderon0892/_apis/projects?api-version=7.0"
```

---

## 4. Git clone (alternativa, solo si se necesita full local)

```bash
AUTH=$(printf ":%s" "$AZURE_DEVOPS_PAT" | base64 -w0)
git -c http.extraHeader="Authorization: Basic $AUTH" \
    clone --branch dev_2028_merge --depth 1 \
    https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_git/TOMWMS_BOF /tmp/TOMWMS_BOF
```

**Cuándo usar clone vs API**:
- API REST → indexación incremental, lectura selectiva, diffs, conteos.
- `git clone --depth 1` → análisis completo puntual (ej. extractor inicial del code-index sobre 9609 archivos).

---

## 5. Reglas duras

1. **NUNCA push a Azure DevOps**. Sin excepción. Cambios viajan por bundle vía GitHub `tomwms-replit-client-automate` rama `main`.
2. **NUNCA log/print del PAT**. Solo referencia por nombre `AZURE_DEVOPS_PAT`.
3. **Default branch en Azure es `master`** pero el equipo trabaja en `dev_2028_merge`. Siempre especificar la branch en queries.
4. **Preferir API REST sobre `git clone`** para operaciones puntuales (376 MB es pesado).
5. **Antes de tocar un archivo**: verificar si existe duplicado entre `/TOMIMSV4/...` y `/TOMIMSV4/TOMIMSV4/...` (legacy nested). Confirmar con Erik cuál es el activo.

---

## 6. Snapshot inventario (`dev_2028_merge`, 2026-04-26)

- **9609 archivos** · **1708 directorios**
- Top extensiones: `.vb` = 3218, `.svg` = 1624, `.cs` = 1083, `.resx` = 657, `.datasource` = 420, `.png` = 358, `.js` = 229, `.xml` = 199, `.xsd` = 162, `.dat` = 134, `.config` = 119, `.svc` = 94, `.dll` = 88
- **Solution**: `TOMWMS.sln`
- **Subraíces relevantes**: `TOMIMSV4/`, `WSHHRN/`, `WSSAPSYNC/`, `WMSWebAPI/`, `WMS.AppGlobalCore/`, `WMS.DALCore/`, `WMS.EntityCore/`, `WMSPortal/`, `DMS/`, `MES/`, `MI3/`, `IAService/`, `SAPSYNCCUMBRE/`, `SAPSYNCMAMPA/`, `SAPSYNC_Killios/`, `WMSBDUpdater/`, `WMS.StockReservation2/`, `WMS.StockReservation3/`, `AWS_WMSBD_Updater/`, `CEALSAMI3/`, `DashBorlin/`, `DynamicsNavInterface/`, `Quick_Tag/`, `PrintService/`, `ServicioImpresion/`, `TomLic/`
- **Topología `TOMIMSV4/`**: `DAL`, `DataSets`, `Entity`, `Images`, `Resources`, `Service References`, `TOMIMSV4` (subraíz anidada legacy), `TOMIMS_WCF`, `Transacciones`
- ⚠ **Duplicación detectada**: `frmAjusteStock.vb` aparece en dos rutas con hashes distintos:
  - `/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb` (290f7925, **probable activo según SKILL.md**)
  - `/TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb` (790bef10, probable legacy)
- Patrón de duplicación esperado en otros archivos del subárbol `/TOMIMSV4/TOMIMSV4/`. **Validar siempre antes de tocar**.

---

## 7. Paths locales conocidos

| Quién | Repo | Path | Notas |
|---|---|---|---|
| Erik (yejc2) | TOMWMS_BOF | `C:\Users\yejc2\source\repos\TOMWMS` | Confirmado 2026-04-26 |
| Erik (yejc2) | TOMHH2025 | `C:\Users\yejc2\StudioProjects\TOMHH2025` | ✓ Confirmado 2026-04-26 (Android Studio default workspace) |
| openclaw | (ambos) | misma máquina, mismos paths que Erik | **Confirmado 2026-04-26**: openclaw corre en la PC de Erik (yejc2). Mismo working set local. Implicación: si Erik tiene algo unstaged, openclaw también lo ve. |

## 7.bis Snapshot inventario TOMHH2025 (`dev_2028_merge`, 2026-04-26)

- **694 archivos** · **163 directorios**
- Top extensiones: `.java` = 405, `.xml` = 167, `.png` = 94, `.jar` = 14
- **Estructura**: proyecto Android Gradle estándar (`app/`, `gradle/`, `build.gradle`, `settings.gradle`, `gradlew`).
- **Package raíz Java**: `com.dts.*`. Subpaquetes confirmados: `com.dts.tom.*` (UI/activities), `com.dts.base.*` (clases base, WebService), `com.dts.classes.<Mantenimientos|Transacciones>.<Modulo>` (POJOs de dominio), `com.dts.ladapt.*` (adapters, 30 archivos), `com.dts.servicios.*` (services, 5 archivos), `com.dts.rfid.*` (RFID, 11 archivos).
- **Activities declaradas en `AndroidManifest.xml`**: **64** (corregido — el conteo previo de 58 era con un grep simple que se perdía las que tienen newline después de `<activity`). De las 64: `MainActivity`, `Mainmenu`, `PBase` y 61 son `frm_*` distribuidas en 13 módulos de negocio. Solo `MainActivity` y `PrintReceiverActivity` usan sufijo `Activity` en filename — el resto NO. Para mapear activities reales, parsear el manifest, NO buscar por filename.
- **Sufijos comunes en filenames** (top): `Manager` (5), `Base` (4), `Adapter` (4), `Service` (2), `Listener` (2), `Holder` (2), `Activity` (2), `Util` (2), `Dialog` (1), `Fragment` (1).
- **Archivos clave conocidos**:
  - `/app/src/main/java/com/dts/base/WebService.java` (759 líneas, 27 KB) — capa HTTP/SOAP de la HH al backend. Contiene la regla legacy de la `ñ` en línea 352 (método `normalize`).
  - `/app/src/main/java/com/dts/tom/MainActivity.java` — entrypoint.
  - `/app/src/main/java/com/dts/tom/PrintReceiverActivity.java` — receiver de impresión.
  - `/app/src/main/AndroidManifest.xml` — declaraciones de activities, permisos.
- **Observación**: el manifest tiene 58 activities pero solo 2 archivos terminan en `Activity.java`. Para mapear el grafo de activities reales hay que parsear el manifest, no listar filenames.

---

## 8. Otros proyectos visibles en la org `ejcalderon0892`

Para contexto (no necesariamente accesibles ni relevantes hoy):
- `TOMHH2025` — App Android handheld (relevante)
- `TOMWMS5` — TOMWMS V 5.0 - 2024
- `TOMWMS_BOF` — backend core (el que nos interesa)
- `TOMWeb`
- `mPos2025`
- `CEALSA` — Proyecto WMS para CEALSA
- `ROAD_FORTUNA`, `ROAD_TOLEDANO`, `RoadPOD`
- `DMF` — AudioFingerPrinting solutions
- `GenCodeSQL`


## 8. Nota sobre BD Killios (AWS) — VOLATILIDAD

La BD operacional Killios (SQL Server en AWS, secret `WMS_KILLIOS_DB_PASSWORD`) **no es un servicio gestionado por nosotros**. Puede:

- **Dejar de existir** (si Erik no renueva la instancia AWS).
- **Restaurarse en otro lugar** para casos de análisis o funcionalidad.
- **Migrar de proveedor** sin previo aviso.

**Reglas para sesiones que dependan de Killios**:
1. Antes de usarla, validar conectividad con un `SELECT 1`.
2. Si el host/credenciales cambiaron, actualizar este archivo + `brain/replit.md` antes de continuar.
3. **Nunca asumir** que el modelo de Killios = modelo de producción del cliente. El cliente tiene su propia instancia.
4. La BD es para **analizar el modelo y validar estados de prueba**, no para operación real.
5. Si Killios no responde y la sesión la necesita, **detenerse y reportar** — no inventar el shape de las tablas.
