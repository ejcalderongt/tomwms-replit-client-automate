# Scripts del bridge

## `azdo-*.cjs` — Lectura del repo Azure DevOps `ejcalderon0892`

Scripts CommonJS sin dependencias externas (sólo `https`). Requieren la variable
de entorno `AZURE_DEVOPS_PAT` con un Personal Access Token con scope
`Code (Read)` en la organización `ejcalderon0892`.

### Uso

```bash
export AZURE_DEVOPS_PAT="..."

# 1. Listar todos los proyectos de la organización
node azdo-discover.cjs

# 2. Listar repos y branches de un proyecto
node azdo-list-repos.cjs TOMWMS_BOF
node azdo-list-repos.cjs TOMHH2025

# 3. Listar el árbol completo de una rama (puede ser ~5 MB)
node azdo-list-tree.cjs   # configurar ORG/PROJECT/REPO/BRANCH dentro

# 4. Code-search en un proyecto (puede dar 0 si el índice no está habilitado)
node azdo-find-file.cjs   # configurar ORG/PROJECT/REPO/BRANCH dentro

# 5. Bajar un archivo concreto
node azdo-get-file.cjs TOMWMS_BOF <REPO_ID> dev_2028_merge \
  "/TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb" \
  /tmp/clsLnStock_res_Partial.vb
```

### Resultados verificados (abril 2026)

| Recurso | ID |
|---|---|
| Org Azure DevOps | `ejcalderon0892` |
| Proyecto TOMWMS_BOF | `04db89a7-f76f-4639-8e95-15088a794027` |
| Repo TOMWMS_BOF | `1a06eb98-9b7d-49e0-8086-bca97e309315` |
| Rama de trabajo | `dev_2028_merge` |
| Total items en la rama | 11317 archivos |
| Función legacy QA | `clsLnStock_res_Partial.vb` líneas 13119+ |

Las 11 proyectos descubiertos son:
TOMHH2025, TOMWMS5, RoadPOD, DMF, ROAD_FORTUNA, TOMWeb, ROAD_TOLEDANO,
CEALSA, mPos2025, TOMWMS_BOF, GenCodeSQL.

### Limitaciones

- **Code search devuelve 0** cuando el índice de búsqueda no está habilitado en
  la cuenta. Workaround: usar `azdo-list-tree.cjs` (lista el árbol completo) y
  filtrar por `path` en cliente.
- **`recursionLevel=Full`** descarga la lista de items pero NO el contenido.
  Para el contenido usar `azdo-get-file.cjs` por archivo.
- El PAT debe renovarse manualmente desde
  `https://dev.azure.com/ejcalderon0892/_usersSettings/tokens`.
