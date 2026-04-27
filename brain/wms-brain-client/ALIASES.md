# wms-brain — Alias de clientes

> **Politica de seguridad**: en TODA la documentacion publica del repo
> `wms-brain` y en TODOS los outputs del cliente local, los nombres reales
> de los clientes se reemplazan por codenames de 2 caracteres. Esto
> protege al cliente y a PrograX24 ante cualquier exposicion accidental
> del repositorio.

## Tabla de alias (ronda inicial)

| Cliente real | Codename | ERP | Modalidad outbox |
|---|:---:|---|---|
| Killios | **K7** | SAP B1 | SAPSYNCKILLIOS.vbproj |
| BYB | **BB** | NAV | NavSync.vbproj |
| CEALSA | **C9** | (3PL — esquema propio) | n/a |
| Idealsa | **ID** | Aurora (homemade) | MI3.vbproj (WCF) |
| Merhonsa | **MH** | Aurora | MI3.vbproj (WCF) |
| Mercosal | **MC** | Aurora | MI3.vbproj (WCF) |
| Mercopan | **MP** | Aurora | MI3.vbproj (WCF) |
| Inelac | **IN** | Aurora | MI3.vbproj (WCF) |
| MHS (Molinos Harineros Sula) | **MS** | (no especificado) | WebAPI moderno |
| Becofarma | **BF** | SAP B1 | SAPSYNC.vbproj |
| Mampa | **MM** | SAP B1 | SAPSYNCMAMPA.vbproj |
| La Cumbre | **LC** | SAP B1 | SAPSYNCCUMBRE.vbproj |
| (placeholder cliente nuevo) | **XX** | — | — |

## Criterio de generacion de codenames

1. Dos caracteres (alfanumericos mayusculas).
2. Preferencia: iniciales del nombre real (Killios=K?, BYB=BB).
3. Si colisionan, agregar digito (Killios=K7, no K1 que ya esta tomado por otro).
4. **Nunca** usar codename que sugiera el cliente real a un outsider
   (no usar "KI" para Killios — usar K7).
5. Mantener mapping centralizado **solo en este archivo**. Resto del
   repo usa el codename.

## Mapping reverso (privado, NO commitear a repo publico)

El client local mantiene una copia del mapping en
`%APPDATA%\WmsBrainClient\aliases.local.json` para resolver codenames a
nombres reales SOLO en outputs locales del usuario autenticado. Nunca se
sube al repo.

```json
{
  "K7": "Killios",
  "BB": "BYB",
  "...": "..."
}
```

## Convencion de uso en docs y queries

- Nombre de carpetas: `learnings/answered/ejc/2026-04-27/K7-Q-001-...md`
- Nombre de snapshots: `schema-snapshots/K7-PRD/2026-04-27.sql`
- Mensajes de commit: "K7: outbox tiene 110k INGRESOS pendientes"
- Logs locales: el cliente PUEDE mostrar nombre real al operador
  autenticado, pero los archivos commiteados al repo siempre llevan
  codename.

## Identificacion de developers (no es alias, es identidad)

Los developers NO se enmascaran (estamos en confianza de equipo). Cada
uno usa su slug:

| Developer | Slug |
|---|:---:|
| Erik Calderon | `ejc` |
| (futuros) | `<inicial>+<apellido>` |

Slug aparece en paths (`learnings/answered/ejc/...`) y en commits
(`Co-authored-by: ejc <...>`).
