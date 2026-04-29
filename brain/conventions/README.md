# brain/conventions/

> Convenciones internas observadas en el código y la operatoria del equipo. Distintas de los **patterns** (que son sobre formas estructurales repetidas en el código): las conventions son **acuerdos implícitos o explícitos** sobre cómo se hacen ciertas cosas.

## Diferencia conceptual

| Categoría | Pregunta que responde | Carpeta |
|---|---|---|
| **case-pointer** | "¿qué bug histórico apunta este hardcode?" | `dataway-analysis/07-correlacion-codigo-data/case-pointers/` |
| **pattern** | "¿qué forma estructural se repite en el código?" | `dataway-analysis/07-correlacion-codigo-data/case-pointers/patterns/` |
| **convention** | "¿qué acuerdo (explícito o implícito) sigue el equipo?" | `brain/conventions/` |

Una convention puede manifestarse como un pattern, pero su valor está en lo **acordado**, no en lo **observado**.

## Inventario actual

| ID | Convención | Manifestación principal | Documento |
|---|---|---|---|
| `C-001` | Comments firmados `'#EJC<YYYYMMDD>[_REF<NN>_<HHMM><AM/PM>]: <body>` | 3270 ocurrencias en TOMWMS_BOF, hábito de Erik desde 2017 | [`comments-firmados-EJC.md`](./comments-firmados-EJC.md) |

## Conventions candidatas para próximas waves

- **C-002 (candidato)**: labels GoTo firmadas con timestamp `<INICIALES>_<YYYYMMDDHHMM>_<DESCRIPCION>:` (visto en CP-010, label `EJC_202308081248_RESERVAR_DESDE_ZONA_PICKING`). Si hay más instancias, formalizar.
- **C-003 (candidato)**: glosario rioplatense "operador / operación / cliente" en código y comments (en vez de inglés "user / operation"). Hay rastros en mensajes de UI y `lblPrg.Text`. Vale documentar para que futuros contribuidores mantengan el dialecto.

## Promoción

Para promover un candidato a convention formal:

1. Evidencia clara de uso intencional (no ruido).
2. Documento que explique: definición, manifestación, cobertura medida, valor para el equipo, futuro.
3. Cross-link con patterns y case-pointers relacionados.
