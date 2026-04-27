# MIGRATION-NOTE.md — formato de las question cards

> Las cards `Q-001..Q-008` de este directorio usan `protocolVersion: 1`,
> que es el **formato propio del cliente PowerShell** (no el formato del
> brain_bridge).

## Por qué

Cuando se redactaron (pasada-7 a pasada-9b), el bridge real
(`scripts/brain_bridge.mjs` en rama `main`, `SCHEMA_VERSION="1"`) ya
existía pero **no tenía un type apropiado** para investigación SQL al
brain de la BD. La pasada-10 reconcilió:

1. **El bridge** sigue con `SCHEMA_VERSION="1"` y 6 tipos `apply_*` /
   `skill_update` / `directive` / `merge_completed` / `external_change`.
2. **El cliente** propone formalmente bump a `SCHEMA_VERSION="2"` con
   3 tipos nuevos: `question_request` / `question_answer` /
   `learning_proposed`. Ver `../EXTENSION-V2-PROPOSAL.md`.
3. **Mientras tanto**, las cards se procesan vía **workaround**: el
   cmdlet `New-WmsBrainQuestionEvent` lee la card local y emite un
   evento `directive` al bridge con `tags=["question", "Q-NNN", ...]`,
   conservando la trazabilidad.

## Mapping de campos legacy → bridge

| Campo card (legacy)        | Campo bridge (workaround `directive`)                 |
|----------------------------|-------------------------------------------------------|
| `id` (Q-NNN)               | `context.tags[]` incluye `Q-NNN`                      |
| `title`                    | `context.message` (prefijado con `Q-NNN: <title>`)    |
| `priority`                 | `context.tags[]` incluye `priority-<level>`           |
| `tags[]`                   | `context.tags[]` (concatenado con `question`+`Q-NNN`) |
| `targets[].codename`       | `context.modules_touched[]` (con prefijo `db-`)       |
| `suggestedQueries[].sql`   | NO se sube al bridge; vive solo local en la card.     |

Cuando se acepte v2, el mapping pasa a ser nativo:

| Campo card                 | Campo bridge v2                                       |
|----------------------------|-------------------------------------------------------|
| `id`                       | `ref.question_id`                                     |
| (path al .md)              | `ref.question_card_path`                              |
| `title`                    | `context.message`                                     |
| `tags`                     | `context.tags`                                        |
| `targets`                  | `context.targets[]`                                   |
| (descripcion de queries)   | `context.expected_outputs[]`                          |

## Qué NO migrar todavía

- **No reescribir** las cards Q-001..Q-008 al formato v2 hasta que el
  bridge acepte `SCHEMA_VERSION="2"`. Si las reescribimos antes y el
  bridge rechaza, perdemos trazabilidad.
- **No borrar** `protocolVersion: 1` de las cards: es la marca que el
  cliente lee para decidir si emite `directive` (workaround) o
  `question_request` (nativo).

## Cuándo migrar

Cuando:

1. Erik aprobó `EXTENSION-V2-PROPOSAL.md`.
2. `scripts/brain_bridge.mjs` tiene `SCHEMA_VERSION="2"` mergeado en `main`.
3. `Test-WmsBrainEnvironment` reporta schema match con v2.

Entonces:

```powershell
# Bumpear todas las cards a v2 con sed-like helper PS
Get-ChildItem questions/Q-*.md | ForEach-Object {
  (Get-Content $_) -replace '^protocolVersion: 1$', 'protocolVersion: 2' |
    Set-Content $_
}
git add questions/
git commit -m "chore(questions): bump protocolVersion 1 -> 2 (bridge schema_version 2 aceptado)"
```

Y borrar la sección §5 "Workaround" del `PROTOCOL.md`.
