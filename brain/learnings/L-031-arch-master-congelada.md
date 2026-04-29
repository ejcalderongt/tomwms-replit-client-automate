# L-031 — ARCH: rama `master` es release oficial congelada, no recibe trabajo activo

> Etiqueta: `L-031_ARCH_MASTER-CONGELADA_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 10, respuesta Carolina a Q-MASTER-PROPOSITO

## Hallazgo

La rama `master` (default en ambos repos `TOMWMS_BOF` y `TOMHH2025`)
NO es la rama de trabajo activo. Es una **version congelada** de
referencia que NO recibe las actualizaciones de las ramas de
desarrollo.

Cita literal Carolina:

> "Si es una version congelada, no tiene las actualizaciones de las
> ramas."

## Convencion del equipo

| Rama | Rol |
|---|---|
| `master` | **Release oficial congelada** — version "publica" / fallback. No se commitea. |
| `dev_2023_estable` | Trabajo activo de la version 2023 (4 clientes en PROD: BECO/K7/BYB/CEALSA al 2026-04-30) |
| `dev_2025` | Historica, no se toca (ver L-030) |
| `dev_2028_merge` | Trabajo activo de la version 2028 (MAMPA PROD; BYB/IDEALSA/INELAC QAS) |
| `dev_2028_Cumbre` | Rama dedicada para fix de implosion + cambio de ubicacion (ver L-029) |

## Contradiccion aparente con git default

Que `master` sea la rama default en GitHub / Azure DevOps puede
confundir a contributors externos que esperen que `master` sea el
"trunk" de desarrollo. En este repo NO lo es. Lo correcto es:

- Para trabajo en 2023: branch off `dev_2023_estable`, PR a
  `dev_2023_estable`.
- Para trabajo en 2028: branch off `dev_2028_merge`, PR a
  `dev_2028_merge` (o `dev_2028_Cumbre` si es la feature de Cumbre).
- **Nunca** branch off `master` ni PR a `master`.

## Por que existe `master` si no se usa

Hipotesis (a confirmar con Erik):

1. **Default historico de Visual Studio + Azure DevOps**. Cuando se
   creo el repo, automaticamente se hizo `master`. Quedo.
2. **Backup pasivo**. Si todas las ramas de desarrollo se rompen
   catastroficamente, `master` mantiene una version "ultima conocida
   buena" que se puede deployar de emergencia.
3. **Snapshot legal / contractual**. Algunas implementaciones para
   clientes pueden requerir mostrar la version "oficial" del software,
   y `master` cumple ese rol.

→ Q-MASTER-RAZON-DE-EXISTIR (a abrir si se quiere clarificar):
¿`master` se actualiza periodicamente con un snapshot de
`dev_2023_estable` o `dev_2028_merge`, o esta congelada en un commit
muy antiguo?

## Verificacion de "congelada"

Wave 6.2 quick-wins detecto:
- `master` en BOF apunta al mismo commit que `dev_2023_estable` y
  `dev_2025`: `1f5cc2c4`.

Si `master` no tuviera updates, deberia apuntar a un commit FIJO en
el tiempo (digamos 2022 o 2023). Que coincida con `dev_2023_estable`
hoy puede significar dos cosas:

- (a) Carolina actualiza `master` periodicamente con snapshot de
  `dev_2023_estable` para mantenerlo "fresco como release oficial".
- (b) En algun punto `master` recibio el HEAD de
  `dev_2023_estable` y de ahi quedo congelado.

→ Q-MASTER-SYNC-DESDE-ESTABLE (a abrir): ¿como se mantiene en sync
`master` con `dev_2023_estable`?

## Implicaciones

### 1. Para el brain
- Cualquier referencia a "master del WMS" en el brain debe entenderse
  como "version oficial de referencia, no trabajo activo".
- Cualquier hallazgo en `master` que difiera de `dev_2023_estable`
  indica que `master` NO esta sincronizado y vive en una version
  anterior.

### 2. Para nuevos contributors
Documentar en `agent-context/RAMAS_Y_CLIENTES.md`:
> "NO trabajes en `master`. La rama de trabajo es
> `dev_2023_estable` (version 2023) o `dev_2028_merge` (version 2028).
> `master` es solo release oficial congelada."

### 3. Para git operations
Los `git clone` por defecto traen `master`. Eso es OK para inspeccion
rapida, pero **siempre hay que checkout a la rama de trabajo correcta**
antes de tocar nada.

### 4. Para la migracion 2028 → 2028+1 futura
Cuando 2028 se estabilice y se "release", probablemente Carolina /
Erik haran: `git push origin dev_2028_merge:master --force` o
similar. Documentar el procedimiento cuando ocurra.

## Cierra Q-*

- `Q-MASTER-PROPOSITO` (Bloque 1, media) — RESUELTA.

## Q-* nuevas derivadas

- Q-MASTER-RAZON-DE-EXISTIR (¿por que existe si no se trabaja?)
- Q-MASTER-SYNC-DESDE-ESTABLE (¿como se mantiene sincronizada?)
- Q-RELEASE-PROCESO (¿hay procedimiento documentado de "promover
  dev_2028_merge a release oficial"?)
