# L-032 — PROC: ramas por cliente en TOMHH2025 son historicas, modelo no sostenible

> Etiqueta: `L-032_PROC_RAMAS-CLIENTE-HH-OBSOLETAS_APPLIED`
> Fecha: 30-abr-2026
> Origen: Wave 10, respuesta Carolina a Q-HH-RAMAS-25 + Q-MERCOPAN-MERCOSAL

## Hallazgo

El repo `TOMHH2025` tiene 25+ ramas, varias de las cuales tienen
nombre de cliente: `byb`, `cealsa`, `mercopan`, `mercosal`, `240byb`,
`240Cealsa`. **Estas ramas son HISTORICAS y no se mantienen activas**.

Cita literal Carolina:

> "Esas ramas se crearon en algun momento para que fueran las ramas
> de cada cliente, pero esto no fue sostenible y se quedaron como
> historicas, pero en realidad deberiamos eliminarlas."

## Por que el modelo no fue sostenible

El patron "una rama de codigo por cliente" tiene problemas conocidos:

1. **Drift acumulado**. Cada cliente recibe fixes y features
   independientes; con el tiempo las ramas divergen y un fix critico
   tiene que aplicarse 5-7 veces (una por rama).
2. **Doble trabajo de QA**. Cada rama tiene su propio set de bugs
   conocidos y tests. Multiplicar el ciclo por N clientes es
   insostenible.
3. **No hay forma simple de "release unificado"**. Si 5 clientes
   reciben actualizacion al mismo tiempo, ¿cual rama se usa de base?
4. **Cherry-pick infierno**. Llevar un fix de `byb` a `cealsa` requiere
   cherry-picks delicados con merge conflicts.

## Modelo correcto adoptado

A partir de algun punto post-2025, el equipo adopto el modelo de **una
rama por version mayor (no por cliente)**:

- Una sola rama por version (`dev_2023_estable`, `dev_2028_merge`).
- **Las diferencias por cliente viven en parametros** (tabla
  `i_nav_config_enc` con 50+ flags, ver L-019), NO en codigo.
- Ramas dedicadas SOLO para features grandes (ej:
  `dev_2028_Cumbre` para fix de implosion, L-029).

Este es el patron actual y correcto. Las ramas viejas por cliente son
fosil de la transicion.

## Inventario de ramas a eliminar (sugerencia)

Lista observada en `TOMHH2025`:

| Rama | Cliente | Probable accion |
|---|---|---|
| `byb` | BYB | eliminar |
| `cealsa` | CEALSA | eliminar |
| `mercopan` | MERCOPAN (holding IDEALSA) | eliminar |
| `mercosal` | MERCOSAL (holding IDEALSA — Wave 10 L-034) | eliminar |
| `240byb` | BYB version "2.4.0" | eliminar |
| `240Cealsa` | CEALSA version "2.4.0" | eliminar |

(Pendiente listar todas las 25 ramas para inventario completo.)

## Pre-condiciones para eliminar

Antes de borrar:
1. **Verificar que NO hay PR abierto contra esa rama**.
2. **Verificar que no hay tag apuntando a un commit unico de la rama**
   (si lo hay, los SHAs sobreviven al delete pero ya no son alcanzables
   sin reflog).
3. **Considerar archivar antes de eliminar**: hacer un tag de
   `archive/<rama>` apuntando al ultimo commit, y entonces borrar la
   rama. Asi se conserva el historial sin contaminar el listado de
   ramas activas.
4. **Revisar `scan-comments-tree-map` aplicado a esas ramas**: si
   tienen comentarios firmados unicos con valor (decisiones cliente-
   especificas), promoverlos al brain como L-* antes de borrar.

## Que NO eliminar

Ramas que NO son por cliente y siguen vigentes:
- `dev_2023_estable` — trabajo activo
- `dev_2028_merge` — trabajo activo
- `dev_2028_Cumbre` — feature activa
- `master` — release oficial congelada (L-031)

Ramas potencialmente vigentes (a confirmar):
- `dev_2025` — historica pero util como referencia (L-030)

## Implicaciones

### 1. Para el equipo
La accion recomendada es hacer **cleanup de ramas en una sesion
aparte**, con backup previo. Carolina o Erik tienen autoridad para
hacerlo. Documentar el proceso en `tasks-historicas/` post-cleanup.

### 2. Para el brain
Cuando se documente una funcionalidad cliente-especifica de HH, NO
referenciar la rama por cliente. Referenciar el flag de configuracion
(`i_nav_config_enc.PTiene_*`) o el codigo en `dev_2028_merge`.

### 3. Para `scan-comments-tree-map`
Antes de eliminar una rama por cliente, correr el scanner sobre ella
con threshold bajo (1-2) para asegurarse de no perder un comentario
firmado unico. Esos shortlist deben revisarse manualmente y promoverse
al brain antes del delete.

### 4. Para `migration-2028`
Que CEALSA tenga rama `cealsa` en HH historica NO significa que
CEALSA tenga codigo HH custom hoy. Significa que hubo un intento.
Hoy CEALSA HH usa `dev_2023_estable` (probable) parametrizada via
`i_nav_config_enc`.

## Cierra Q-*

- `Q-HH-RAMAS-25` (Bloque 1, media) — RESUELTA: ramas por cliente son
  historicas, deberian eliminarse.
- `Q-MERCOPAN-MERCOSAL` (Bloque 1, media) — RESUELTA parcial:
  MERCOPAN y MERCOSAL existen como BDs (Carolina ya paso a Erik), las
  ramas HH son fosil. Para fingerprint completo de MERCOSAL ver L-034.

## Q-* nuevas derivadas

- Q-RAMAS-HH-CLEANUP (Q87 cuestionario bloque 14): hacer cleanup de
  ramas con backup previo.
- Q-RAMAS-INVENTARIO-COMPLETO: listar las 25+ ramas reales para
  documentar cuales borrar y cuales mantener.
