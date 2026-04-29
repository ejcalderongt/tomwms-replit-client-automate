# PATTERN.md — Regex de detección del comentario firmado

## Patrón de referencia

Captura inicial confirmada por Erik (screenshot `frmRecepcionBOF.vb` línea 15734):

```vbnet
'#EJC20220224_0326AM: Identificar si se debe registrar en NAV el documento para BYB.
```

Descomposición:

| Token | Valor | Obligatorio | Notas |
|---|---|---|---|
| Comment marker | `'` (VB) / `//` (Java/C#) / `--` (SQL) / `#` (Python) | sí | Por extensión de archivo |
| Hash separator | `#` | **no** | Decorador opcional. Aparece en ~80% pero no siempre |
| Iniciales autor | `EJC` | sí | 2-4 letras MAYÚSCULAS contiguas |
| Fecha | `20220224` | sí | `YYYYMMDD` 8 dígitos |
| Separador | `_` o `-` o nada | no | Tolerar las 3 variantes |
| Hora | `0326` | sí | `HHMM` 4 dígitos (también acepto 3 dígitos `326` como `03:26`) |
| Período | `AM` o `PM` | no | Aceptar minúsculas y ausencia |
| Separador final | `:` | no | A veces hay `;` o `-` o nada |
| Cuerpo | `Identificar si se debe registrar en NAV el documento para BYB.` | sí | Cualquier texto >= 1 char |

## Regex master (Python re)

```python
PATTERN = re.compile(
    r"""
    (?P<marker>['#/-]+)          # comment marker(s) y posible '#' decorador
    \s*
    (?:[#])?                      # '#' opcional adicional (caso '#EJC...)
    \s*
    (?P<initials>[A-Z]{2,4})     # iniciales del autor en MAYÚSCULAS
    \s*
    (?P<date>\d{8})              # YYYYMMDD
    [_\-]?                        # separador opcional
    (?P<time>\d{3,4})            # HHMM o HMM
    \s*
    (?P<period>[AaPp][Mm])?      # AM/PM opcional
    \s*
    [:;\-]?                       # separador antes del cuerpo
    \s*
    (?P<body>.+?)                # cuerpo del comentario
    \s*$
    """,
    re.VERBOSE | re.MULTILINE,
)
```

El script `scan.py` aplica este regex línea por línea sobre el contenido **ya recortado** a comentarios (es decir, antes ya identificó qué parte de la línea es comment según la sintaxis del lenguaje).

---

## Casos positivos (DEBEN matchear)

| Línea | Notas |
|---|---|
| `'#EJC20220224_0326AM: Identificar si se debe registrar en NAV el documento para BYB.` | Caso canónico (VB, marker `'`, hash decorador, AM, separador `_`) |
| `'EJC20220224_0326AM: Identificar...` | Sin `#` decorador |
| `'EJC20220224_326AM: Identificar...` | Hora 3 dígitos en vez de 4 |
| `'EJC202202240326AM: Identificar...` | Sin separador entre fecha y hora |
| `'EJC20220224-0326-AM: Identificar...` | Separador con `-` |
| `'EJC20220224_0326: Identificar...` | Sin AM/PM |
| `'EJC20220224_0326am: identificar...` | Período en minúsculas |
| `'EJC20220224_0326AM Identificar...` | Sin `:` separador final |
| `'#EJC20220224_0326AM; Identificar...` | Separador final con `;` |
| `// MA20231115_1430PM: Validar tope de cantidad antes de reservar.` | Java o C#, autor `MA` (2 letras), PM |
| `-- MECR20240301_0900AM: SP modificado para devolver lote_numerico correlativo.` | SQL, autor `MECR` (4 letras) |
| `// CF20250620_1645: cambia política reabasto cuando bodega es nivel 0.` | Java, sin AM/PM, sin `#` |

---

## Casos negativos (NO deben matchear)

| Línea | Razón |
|---|---|
| `'TODO: agregar validación` | Sin iniciales + fecha |
| `'EJC: arreglar después` | Sin fecha |
| `'EJC2022 ver con Erik` | Fecha incompleta (solo 4 dígitos) |
| `'AS200001011200AM: hardcode test` | Iniciales muy cortas (`AS`) más legítimo, **MATCHEA** — pero el body suele caer al filtro de scoring (`hardcode test` es ignorelist) |
| `' EJC20220224 // 0326 AM` | Separadores múltiples raros, fecha y hora separados por comment marker |
| `'comentario suelto sin firma` | No tiene patrón |

> **Política**: el regex es **permisivo en match, estricto en scoring**. Es preferible que matchee algo dudoso y el scoring lo descarte (queda en `noise-discarded.md` para inspección), antes que perder un comentario útil por falsa firmeza del regex.

---

## Variantes futuras a anticipar

A medida que se procesen más archivos pueden aparecer variantes no previstas. Tres que ya se contemplan:

### A. Comentario multilinea con firma en la primera línea

```vbnet
'#EJC20220224_0326AM: Identificar si se debe registrar en NAV el documento para BYB.
'   Caso especial: cuando BeTransOCEnc.Push_To_NAV = True y No_Documento_Recepcion_ERP
'   está vacío, llamar Registrar_Pedido_Compra_To_NAV_For_BYB con el correlativo.
```

→ El script junta las líneas siguientes que comienzan con marker + whitespace al cuerpo. Se controla con `--max-context-lines` (por defecto 5).

### B. Múltiples firmas en el mismo bloque (auditoría iterativa)

```vbnet
'#EJC20220224_0326AM: Identificar si se debe registrar en NAV el documento para BYB.
'#GT20240115_1100AM: refactor para K7 también, ahora flag por cliente.
```

→ El script crea **un match por línea firmada**. Cada uno con su autor + fecha. Quedan en el tree-map asociados al mismo archivo + ventana de 1-2 líneas, así un visualizador puede inferir la cadena evolutiva.

### C. Firma sin comment marker (en docstring de C# `///` o XML doc)

```csharp
/// <remarks>EJC20220224_0326AM: Identificar si se debe registrar en NAV.</remarks>
```

→ Se detecta porque la firma está en una línea que ya es comment según el lexer (`///` o dentro de un bloque `/** */`). El regex matchea sobre el cuerpo del comment ya extraído, así que esto entra naturalmente.

---

## Validación contínua

`scan.py --dry-run` corre el regex contra una batería de **15 casos positivos** y **8 casos negativos** definidos en el propio script. Si alguno falla, el script imprime el caso roto y sale con código 1.

```bash
python scan.py --dry-run
# Esperado:
# ✓ 15/15 positivos matchearon
# ✓ 8/8 negativos no matchearon
# Regex está sano. Listo para correr contra repo real.
```

Antes de cada cambio en el regex, **agregar el caso al `--dry-run`** primero. Tests-first incluso para regex.

---

## Cuándo modificar el regex

Modificar el regex SOLO cuando aparezca una variante real de comentario firmado que no matchee. **No modificar para cubrir casos hipotéticos**.

Cada modificación va con:
1. Nuevo caso de prueba en `--dry-run`
2. Bump de versión en README.md (`0.1.0` → `0.1.1`)
3. Comentario en este archivo explicando qué variante real se detectó

---

## Referencias

- AGENTS.md del agent-context lista al equipo: EJC, GT, AG, MA, AT, MECR, CF
- Convención formal de commits del repo (más estricta) está en `entities/decisions/dec-formato-commits.md` — útil para entender el espíritu pero no aplicable al scanner (los comentarios in-line tienen menos disciplina)
- Convenciones VB/SQL/JSON: `skills/wms-tomwms/conventions.md`
- Convenciones Java/Android: `skills/wms-tomhh2025/conventions.md`
