# AGENTS.md — Manual del consumidor de bundles

Documento agnóstico de cliente para aplicar bundles `vNN_bundle/` generados desde el lado productor (Replit) al repo local de TOMWMS VS.NET en `dev_2028_merge`. Cualquier agente CLI o script que respete este contrato puede automatizar el flujo.

## Topología de repos

Tres repos distintos, roles distintos. **No mezclar.**

| Repo | Rol | URL / ubicación | Quién escribe |
|---|---|---|---|
| **Intercambio** | Bundles, logs e historial | `https://github.com/ejcalderongt/tomwms-replit-client-automate.git` | Productor empuja bundles. Consumidor empuja `apply_log.json`. |
| **Código WMS** | Fuente del WMS (VB.NET / VS 2026) | Azure DevOps (rama `dev_2028_merge`) | Solo el consumidor sincroniza y aplica patches localmente. |
| **Workspace Replit** | Productor: scripts + skill + builders | Sesión Replit del autor | Productor. |

Reglas duras:
- El repo de **intercambio en GitHub no contiene código WMS**. Solo `entregables_ajuste/`, scripts del consumidor, contratos.
- El repo del **WMS en Azure DevOps no recibe push automático del productor ni del consumidor**. Los patches viajan por el repo de intercambio; el merge a `dev_2028_merge` lo hace el humano desde VS.
- El productor (Replit) **no tiene clonado el WMS completo**. No aplica patches; solo los construye y los publica al repo de intercambio.

## Hello sync — handshake pre-sesión

Antes de cualquier análisis, propuesta o aplicación, **ambos lados** ejecutan el handshake:

```
1. git pull del repo de intercambio (GitHub)
2. Leer historial: todos los apply_log.json en entregables_ajuste/<fecha>/vNN_bundle/
3. Detectar el último bundle producido y el último aplicado OK
4. (Solo consumidor) git pull del repo WMS desde Azure DevOps
5. Verificar working tree limpio en ambos repos
6. Si todo OK -> imprimir "Hello Erik" + figura ASCII corta
```

Implementación de referencia: `scripts/hello_sync.mjs`. Mismo script para ambos roles, distinto flag:

```bash
# Productor (Replit)
node scripts/hello_sync.mjs --rol productor --exchange-repo /path/exchange

# Consumidor (openclaw / Windows)
node scripts/hello_sync.mjs --rol consumidor \
  --exchange-repo C:\path\exchange \
  --wms-repo C:\path\TOMIMSV4
```

Salidas esperadas:
- `exit 0` + figura ASCII -> handshake OK, autorizado a operar.
- `exit 1` + motivo en stderr -> handshake FAIL, **no proceder**.

Motivos de fallo posibles: working tree sucio, fetch falla (sin red / sin token), pull no es fast-forward (rama divergente), rama actual no coincide con la esperada.

El handshake **es obligatorio** antes de cada sesión de trabajo. No es decorativo — es el mecanismo anti-double-apply y anti-divergencia.

## Ubicación de los bundles

```
entregables_ajuste/
├── AGENTS.md                     <- este documento
├── <YYYY-MM-DD>/
│   ├── vNN_bundle/
│   │   ├── MANIFEST.json
│   │   ├── README.md
│   │   ├── patches/
│   │   │   └── 0001-*.patch       <- formato git mailbox
│   │   └── apply_log.json         <- escrito post-aplicación (ver abajo)
│   └── vNN_bundle_<YYYY-MM-DD>.zip
```

El bundle viaja **versionado en este repo** como fuente de verdad. El ZIP es artefacto adicional para canales sueltos. Ambos contienen los mismos bytes.

## Contrato del bundle (lo que el productor garantiza)

Todo bundle entregado cumple:

1. `git apply --check` ya pasó del lado productor antes de empaquetar.
2. Replay byte-identical entre `mod` esperado y resultado del patch.
3. ZIP con CRC verificado.
4. `MANIFEST.json` con campos estables (claves en español, schema canónico del productor):
   - `bundle` (ej `v23_bundle`), `fecha`, `rama_destino`, `base_commit`, `marcador_global`, `descripcion`.
   - `compatibilidad`: objeto `{ vXX: "explicación" }`.
   - `archivos[]`: cada uno con `{ path, encoding, patch, md5_orig, md5_mod, cambios }`.
   - `author`, `mailbox_date`.

   El consumidor de referencia (`apply_bundle.mjs`) también acepta los alias en inglés
   (`version`, `date`, `marker`, `description`, `files`, `patch_name`, `summary`,
   `compat`) por compatibilidad, pero el productor sólo garantiza el schema español.
5. Marcador único `#FIX_vNN_<NOMBRE>_<FECHA>` con count documentado en README.
6. Mailbox formato git apto para `git apply` o `git am`.

El consumidor confía en estas garantías y se concentra en **validar precondiciones locales** antes de aplicar.

## Flujo end-to-end

```
1. Pull del repo (o descargar ZIP) → bundle disponible en disco
2. Descubrimiento: localizar último vNN por fecha+versión descendente
   (las precondiciones locales detectan si ya está aplicado y abortan)
3. Validaciones precondición (abortar si fallan):
   - Repo destino con working tree limpio
   - Rama actual = rama_destino del MANIFEST (configurable)
   - md5 de cada archivo local = md5_orig del MANIFEST
   - Marcadores del bundle ausentes en código (anti doble-apply)
   - git apply --check pasa
4. Crear rama efímera: agent/vNN-<YYYYMMDD>-<corto>
5. Aplicar: git apply (atómico)
6. Validaciones postcondición:
   - md5 de cada archivo local = md5_mod del MANIFEST
   - Marcadores presentes con count esperado
7. Mostrar diff resumido (git diff --stat) + comando para diff completo
8. Pedir confirmación interactiva (Y/N)
9. Si Y: dejar rama efímera lista, escribir apply_log.json OK,
        imprimir comandos de merge sugeridos (no auto-merge)
   Si N: dejar rama efímera intacta para inspección, escribir apply_log.json FAIL
```

## Validaciones obligatorias

### Precondición (antes de aplicar)

| Check | Acción si falla |
|---|---|
| Working tree limpio (`git status --porcelain` vacío) | Abortar. No tocar nada. |
| Rama actual coincide con `rama_destino` (default `dev_2028_merge`) | Advertir. Permitir override con flag explícito. |
| md5(archivo_local) == files[].md5_orig | Abortar. La base local no coincide; aplicar bundles previos primero. |
| Marcador del bundle ausente en código fuente | Si presente, abortar con "ya aplicado". Idempotencia. |
| git apply --check pasa | Abortar. Reportar la salida del check. |

### Postcondición (después de aplicar, antes de confirmar)

| Check | Acción si falla |
|---|---|
| md5(archivo_local) == files[].md5_mod | Abortar y `git checkout -- <archivo>` en la rama efímera. Reportar discrepancia. |
| Marcador presente con count esperado (parsear del README o pasarlo via flag) | Advertir pero permitir continuar (puede haber count derivado, no siempre estricto). |

## Política de rama efímera

- **Nombre**: `agent/vNN-<YYYYMMDD>-<short>` donde `<short>` son los primeros 4 caracteres del hash md5_mod del primer archivo, para garantizar unicidad si se aplica más de una vez.
- **Base**: HEAD actual de `rama_destino` al momento de aplicar (no `base_commit` del MANIFEST, que es informativo).
- **Vida**: persiste hasta merge manual o cleanup explícito. Nunca auto-borrar; si la aplicación falla, la rama queda para inspección.

## Confirmación final

El agente local debe esperar input humano explícito antes de declarar éxito. Mecanismos válidos:

- Prompt interactivo en stdin (`Y/N`).
- Archivo `.confirm` o flag `--yes` para CI.
- Cualquier mecanismo equivalente que requiera acción humana o aprobación previa.

**Auto-merge a `dev_2028_merge` no está permitido por este contrato.** El agente local sólo deja la rama efímera lista e imprime los comandos de merge sugeridos. El humano decide cuándo mergear desde VS o desde consola.

## apply_log.json — formato

Escrito en `entregables_ajuste/<fecha>/vNN_bundle/apply_log.json` después de cada intento de aplicación (éxito o fracaso). Lo leen los productores e iteraciones siguientes para saber el estado.

```json
{
  "version": "v23",
  "date": "2026-04-25",
  "applied_at": "2026-04-27T18:42:13-03:00",
  "result": "OK",
  "branch": "agent/v23-20260427-14f1",
  "commit_sha": "abc1234567890...",
  "rama_destino": "dev_2028_merge",
  "files": [
    {
      "path": "TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb",
      "md5_orig_expected": "32cb29f30d22406df3e633e52e71a5f9",
      "md5_orig_actual":   "32cb29f30d22406df3e633e52e71a5f9",
      "md5_mod_expected":  "14f1a78771eb3dd7d9ace6c2bc2440fc",
      "md5_mod_actual":    "14f1a78771eb3dd7d9ace6c2bc2440fc"
    }
  ],
  "marker": "#FIX_v23_ELIMINAR_AJUSTE_RULES_2026-04-25",
  "marker_hits_actual": 3,
  "host": "ERIK-DESK",
  "agent": "<nombre del cliente CLI>"
}
```

Si `result` es `FAIL` agregar `"error": "<mensaje>"` y `"failed_at_check": "<nombre del check>"`.

## Implementación de referencia

El repo trae dos implementaciones del flujo:

- **`scripts/apply_bundle.mjs`** — Node.js puro, sin dependencias externas. Portable. Recomendado.
- **`scripts/Apply-Bundle.ps1`** — Wrapper PowerShell que invoca el script Node y simplifica args en Windows.

Cualquier otro agente CLI que respete este contrato (validaciones + rama efímera + apply_log.json) es válido. El formato `apply_log.json` es la API entre el lado consumidor y el lado productor.

## Comandos típicos

### Aplicar el último bundle disponible

```powershell
# Windows
node scripts/apply_bundle.mjs --latest --repo C:\path\al\TOMIMSV4
# o
.\scripts\Apply-Bundle.ps1 -Latest -RepoPath C:\path\al\TOMIMSV4
```

### Aplicar un bundle específico

```powershell
node scripts/apply_bundle.mjs --bundle entregables_ajuste/2026-04-25/v23_bundle --repo C:\path\al\TOMIMSV4
```

### Verificar estado sin aplicar (dry-run)

```powershell
node scripts/apply_bundle.mjs --latest --repo C:\path\al\TOMIMSV4 --dry-run
```

## Fallos comunes y resolución

| Síntoma | Causa probable | Resolución |
|---|---|---|
| `md5_orig mismatch` | Falta aplicar un bundle previo, o el archivo local fue editado a mano | Aplicar bundles faltantes en orden, o revertir edición manual |
| `Marker already present` | El bundle ya fue aplicado en una sesión anterior | Verificar `apply_log.json`. Si ya está en `dev_2028_merge`, ignorar |
| `git apply --check failed` | Conflicto inesperado por edición concurrente del archivo | Inspeccionar diff manual, eventualmente pedir bundle re-baseado al productor |
| `Working tree not clean` | Hay cambios sin commitear | Stash, commit, o descartar antes de aplicar |
| `Wrong branch` | No estás en `dev_2028_merge` | `git checkout dev_2028_merge && git pull` |

## Reglas operativas (ambos lados)

Aplicables al productor y al consumidor. Son contrato, no sugerencia.

1. **Analizar antes de modificar.** Leer el código existente, identificar dependencias, mapear impacto. Nada de cambios a ciegas.
2. **Identificar archivos relacionados.** Todo cambio que toca un archivo debe declarar explícitamente qué otros archivos consume o asume.
3. **Plan breve antes de aplicar.** Frase corta que describa qué se hace, dónde y por qué. No editar primero y explicar después.
4. **Cambios pequeños e incrementales.** Un bundle = una intención. No mezclar refactors con bug fixes con features.
5. **No romper compatibilidad.** Nombres de funciones, firmas, contratos JSON, orden de columnas, claves de configuración: se preservan salvo instrucción explícita y documentada.
6. **No asumir refactors masivos.** Si la tarea pide arreglar X, no se "aprovecha" para reescribir Y.
7. **Pedir confirmación antes de cambios grandes.** Cambios estructurales (mover archivos, renombrar módulos, cambiar schemas) requieren OK explícito antes de empaquetar.
8. **Trazabilidad obligatoria.** Cada bundle deja `MANIFEST.json` y cada intento deja `apply_log.json`. Sin excepción.
9. **Anti double-apply.** Antes de generar el bundle vNN+1, leer `apply_log.json` de vNN. Si está FAIL o ausente, primero resolver eso.
10. **Hello sync antes de operar.** Sin handshake exitoso (`exit 0` + figura ASCII), no hay autorización para análisis ni propuestas ni aplicación.

## Versionado de este documento

Cualquier cambio al contrato (hashes, formato apply_log, política de rama, hello sync, etc.) debe coordinarse con la skill productor `.local/skills/bundle-builder/SKILL.md` y los scripts en `scripts/`. Las cuatro partes son una sola unidad coherente: AGENTS.md, SKILL.md, `apply_bundle.mjs` y `hello_sync.mjs`.
