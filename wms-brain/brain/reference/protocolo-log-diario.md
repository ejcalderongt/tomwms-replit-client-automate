# Protocolo: log diario por handoff

> Origen: handoff `2026-05-19-codex-learning-bof-veri-movimientos-duplicados/LOG_DIARIO.md`
> (Codex local 2026-05-19). Promovido por Brain Keeper como convencion del
> proyecto al ver que el formato funciona para sintetizar sesiones largas.

## Proposito

Cada handoff o sesion de trabajo significativa debe dejar una bitacora diaria
ordenada que sobreviva al chat. Sirve a tres consumidores:

1. **Erik** (revision al dia siguiente, contexto sin releer chat).
2. **Codex local / Replit Brain Keeper** (sincronizacion entre agentes).
3. **Brain estable** (extraer learnings reusables a `PATTERNS-*.md` y
   `LEARNINGS-*.md`).

No reemplaza `PROPOSAL.md` / `RESULT.md` / `LEARNINGS.md`; es bitacora
**complementaria**.

## Ubicacion

- Dentro del directorio del handoff:
  `wms-brain/brain/handoffs/<slug>/LOG_DIARIO.md`
- No crear un `LOG_DIARIO.md` global por dia; los handoffs ya estan fechados
  en su slug y la bitacora vive con su trabajo.

## Estructura de cada entrada

```md
## YYYY-MM-DD - <tema corto>

### Resumen
1-3 parrafos. Que se intento y que resultado se obtuvo.

### Cambios / analisis por area
Subseccion por area: BOF, BD, API, HH, Brain. Solo las que aplican.

### Validaciones
Comandos exactos ejecutados + resultado observado.
Ej: `MSBuild DAL.vbproj` → `DAL.dll compilado correctamente`.
Ej: `EXEC dbo.usp_WMS_VERI_PostCheck @IdPickingEnc = 1465`
    → `DUPLICADOS_EXACTOS_RESTANTES = 0, MISMATCH_PRESENTACION_RESTANTES = 5`.

### Riesgos / what-if pendientes
Bullets cortos. Cosas que pueden romperse si se aplica el fix sin cuidado,
o efectos colaterales detectados.

### Proximos pasos
Bullets accionables, en orden. "Revisar X con Erik", "Aplicar Y solo en QA",
"Documentar Z en Brain Keeper", etc.

### Links / artefactos
Bullets con rutas relativas a:
- otros archivos del mismo handoff;
- patrones promovidos en brain estable;
- documentos del cliente;
- commits Azure DevOps (solo SHA, no credenciales).
```

## Reglas

- Entradas breves pero accionables. Si necesita >2 paginas, probablemente
  hay material para promover a `PATTERNS-*.md` o `LEARNINGS-*.md`.
- Separar por area (BOF / BD / API / HH / Brain) cuando aplique.
- Registrar comandos exactos de validacion, no parafrasear.
- Registrar si hubo commit/push y a que branch (Azure DevOps y/o wms-brain).
- **Nunca** incluir credenciales, contraseñas, tokens, ni datos personales
  del cliente. Si se necesita referenciar un dump, dar la ruta y omitir
  contenido sensible.
- No reemplazar `RESULT.md`; el log es bitacora del proceso, no del resultado.
- Si la sesion involucra varios dias, agregar entradas adicionales al mismo
  `LOG_DIARIO.md` en orden cronologico, no crear un archivo por dia.

## Marca de protocolo

Cuando se siga este protocolo, agregar al inicio del LOG:

```md
> Sigue protocolo `wms-brain/brain/reference/protocolo-log-diario.md`.
```

Asi un lector futuro reconoce el formato sin tener que inferirlo.

## Ejemplo en vivo

Ver `wms-brain/brain/handoffs/2026-05-19-codex-learning-bof-veri-movimientos-duplicados/LOG_DIARIO.md`
como referencia del formato aplicado correctamente.

## Relacion con LEARNINGS.md

| Pieza | Vida | Granularidad |
|---|---|---|
| `LOG_DIARIO.md` | Bitacora cronologica por handoff | Por sesion/dia |
| `LEARNINGS.md` (frontmatter) | Reglas/gotchas/sugerencias para el brain | Por handoff (sintesis final) |
| `PATTERNS-*.md` (estable) | Patrones canonicos cross-handoff | Por dominio |

El flujo natural es:
LOG_DIARIO (proceso) → LEARNINGS (sintesis) → PATTERNS (promocion Brain Keeper).
