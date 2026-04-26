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

| Quién | Path | Notas |
|---|---|---|
| Erik (yejc2) | `C:\Users\yejc2\source\repos\TOMWMS` | Confirmado 2026-04-26 |
| openclaw | (por confirmar) | ¿Misma máquina que Erik? ¿Mismo path? |

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
