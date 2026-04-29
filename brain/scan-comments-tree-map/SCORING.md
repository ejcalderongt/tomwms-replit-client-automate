# SCORING.md — Rúbrica de utilidad de comentarios firmados

## Premisa

No todos los comentarios firmados son útiles para el brain. La firma garantiza autoría y trazabilidad, pero **no garantiza valor informativo**. Un `'EJC20240501_1500PM: arreglar` es firmado y trazable, pero no aporta contexto.

La rúbrica clasifica cada match en uno de 3 buckets:

| Bucket | Score | Destino |
|---|---|---|
| **Señal alta** | >= 5 | `useful-shortlist.md` con marca `★` para promoción inmediata |
| **Señal media** | 3-4 | `useful-shortlist.md` para revisión humana |
| **Ruido** | < 3 | `noise-discarded.md` con razón |

El threshold se ajusta con `--useful-threshold N` (default 3).

---

## Tabla de señales (suman positivo)

| Señal | Puntos | Detección |
|---|---|---|
| Menciona cliente conocido | +3 cada uno (max +6) | Match con lista en `config/boost-keywords.yml` sección `clients`: BYB, BECO, BECOFARMA, MAMPA, K7, KILLIOS, CEALSA, CUMBRE, IDEALSA, MERHONSA, MERCOPAN, INELAC, MHS |
| Menciona ERP / canal de integración | +2 cada uno (max +4) | Lista `erps`: NAV, SAP, MI3, CEALSAMI3, CEALSASync, NavSync, SAPBOSync |
| Palabra de regla de negocio o invariante | +2 cada una (max +4) | Lista `rule_keywords`: porque, regla, siempre, nunca, no eliminar, no tocar, no quitar, cuidado, ojo, obligatorio, debe, requiere, importante, crítico, mantener |
| Palabra de fix/bug con justificación | +1 cada una (max +2) | Lista `fix_keywords`: corrección, fix, bug, issue, ticket, hotfix, patch, error de, falla de, falla en |
| Palabra de condición específica | +1 cada una (max +2) | Lista `condition_keywords`: si, cuando, solo, excepto, salvo, en caso de, mientras, hasta que |
| Menciona tabla específica del WMS | +2 cada una (max +4) | Lista `wms_tables`: stock, stock_res, trans_pe_enc, trans_pe_det, i_nav_config_enc, producto, producto_estado, bodega, etc. |
| Menciona función/SP específico del WMS | +2 cada una (max +4) | Lista `wms_functions`: Reserva_Stock, Insertar_Stock_Res_MI3, Push_To_NAV, Get_Nuevo_Correlativo_LicensePlate, etc. |
| Cuerpo >= 60 chars | +1 | `len(body) >= 60` |
| Cuerpo >= 120 chars | +1 (acumula con anterior) | `len(body) >= 120` |
| Múltiples líneas de body (continuación) | +1 | Detectado si el match tiene ≥ 2 líneas |
| Antigüedad documentada (fecha < 2023) | +1 | Comentario viejo que sobrevivió → probable contexto histórico valioso |

---

## Tabla de señales (restan)

| Señal | Puntos | Detección |
|---|---|---|
| Cuerpo <= 20 chars | -2 | `len(body) <= 20` |
| Cuerpo <= 10 chars | -3 (acumula con anterior) | `len(body) <= 10` |
| Match con ignorelist | -5 | Lista `noise_keywords`: TODO solo, FIX solo, prueba, test, debug, console, print, borrar, descomentar, comentar, ver con, hablar con, preguntar, ojo (sin contexto), revisar (sin objeto) |
| Comentario es duplicado exacto en el repo | -1 | Hash del cuerpo aparece >= 2 veces |
| Cuerpo es solo signos/números | -5 | `re.fullmatch(r'[\W\d\s]+', body)` |
| Body es código pegado (parece línea de código comentada, no comentario humano) | -3 | Heurística: contiene `=`, `(`, `)` y termina en `;` o `_` |

---

## Reglas de combinación

1. **Score final = max(0, suma_positivos - suma_negativos + bonificaciones)**
2. **Si match con ignorelist → score = 0 forzado**, va directo a noise. La idea: si el comentario tiene `prueba`, no importa cuántos clientes mencione, es ruido.
3. **Bonificación cross-señal**: si en el mismo comentario aparecen **cliente + ERP + regla**, suma +2 extra. Es un comentario "de oro" (ej: el caso del screenshot suma 3 + 2 + 2 + 1 + 2 = 10 → bonificación → 12).
4. **Score máximo capeado en 15** para que el ranking sea legible.

---

## Ejemplo trabajado

**Comentario**: `'#EJC20220224_0326AM: Identificar si se debe registrar en NAV el documento para BYB.`

| Señal | Detectada | Puntos |
|---|---|---|
| Cliente: BYB | sí | +3 |
| ERP: NAV | sí | +2 |
| Regla keyword: "Identificar" (no en lista) | no | 0 |
| Condición: "si" | sí | +1 |
| Tabla mencionada: ninguna | no | 0 |
| Body length: 60 chars | sí | +1 |
| Body length: >= 120 chars | no | 0 |
| Antigüedad < 2023 (es 2022-02-24) | sí | +1 |
| Bonificación cross-señal (cliente + ERP + condición → 2 de 3 categorías mayores) | parcial | 0 (necesita 3) |
| **Total** | | **8** |

→ Score 8 = **señal alta** → `useful-shortlist.md` con marca `★`

---

## Ejemplos de noise

**`'#EJC20240501_1500PM: arreglar`**

| Señal | Puntos |
|---|---|
| Body length 8 chars | -2 |
| Body length <= 10 | -3 |
| Match noise keyword (sin objeto) | 0 (no matchea exacto) |
| **Total** | **-5 → 0** |

→ noise-discarded con razón "body too short, no informative content".

**`// MA20240301_1000AM: TODO`**

| Señal | Puntos |
|---|---|
| Body length 4 chars | -2 |
| Match ignorelist "TODO solo" | -5 |
| **Total** | **-7 → 0** |

→ noise.

**`'#GT20240115_1100AM: prueba para Carolina`**

| Señal | Puntos |
|---|---|
| Body length 20 chars | 0 |
| Match ignorelist "prueba" | -5 |
| **Total** | **forzado a 0** |

→ noise.

---

## Cómo ajustar la rúbrica

La rúbrica vive en 3 lugares:

1. **`config/boost-keywords.yml`** — listas de clientes, ERPs, palabras de regla, etc. **Editable sin tocar código.**
2. **`config/ignorelist.yml`** — patrones que mandan a noise. **Editable sin tocar código.**
3. **`scan.py`** — lógica de combinación, multiplicadores, cap. **Editar solo si la rúbrica cambia conceptualmente.**

Workflow recomendado para tunear:

```
1. correr una vez
2. abrir noise-discarded.md y revisar 20 al azar
3. encontrar 1-3 falsos negativos (algo útil que cayó a noise)
   ┌─ si la causa es boost-keyword faltante → agregarla a boost-keywords.yml
   ├─ si la causa es ignorelist demasiado agresiva → quitarla de ignorelist.yml
   └─ si la causa es lógica de combinación → editar scan.py (raro)
4. correr de nuevo, comparar diff de noise → useful
5. iterar hasta que el spot-check de 20 dé < 1 falso negativo
```

---

## Métricas de salud de la rúbrica

Cada corrida emite (en `scan_metadata` del JSON y al stdout):

| Métrica | Saludable | Acción si fuera de rango |
|---|---|---|
| `useful_count / total_matches` | 15-30% | Si > 50%: rúbrica laxa, subir threshold. Si < 5%: rúbrica estricta, revisar boost-keywords |
| `noise_count / total_matches` | 70-85% | Espejo del anterior |
| `unknown_authors / total_matches` | < 2% | Si > 5%: actualizar `authors.yml` con las siglas nuevas |
| `clients_mentioned (distinct)` | >= 4 | Si solo 1-2: el repo escaneado es muy específico (esperable para `WSHHRN/` solo) |
| `score_distribution` | Cola larga hacia 0, pico medio en 1-2, decay suave hasta 12+ | Si bimodal extremo (todo 0 o todo 10): la rúbrica no discrimina |

---

## Por qué este score y no otro

**Por qué no usar embeddings / LLM**: la rúbrica es transparente, auditable y reproducible. Un score alto se puede explicar línea por línea. Embeddings serían más finos pero opacos. Para un MVP que va a ser revisado por humanos, transparencia > precisión.

**Por qué multiplicadores y no pesos calibrados**: porque no hay dataset etiquetado de "comentario útil". Calibrar pesos requiere ground truth. La rúbrica actual es una **opinión razonada** que se ajusta empíricamente. Cuando haya 200+ comentarios marcados manualmente, se puede pasar a regresión logística.

**Por qué cap a 15**: visualmente, un score de 15 ya es "comentario excepcional". Score 80 vs 150 no aporta más al ranking. El cap evita que un comentario muy largo y muy específico bloquee al resto del ranking.

---

## Versionado de la rúbrica

Cualquier cambio en SCORING.md o en los YAML de config implica:
1. Bump de versión beta del scanner (`0.1.0 → 0.1.1`)
2. Nota en el commit explicando qué se ajustó y por qué
3. Re-corrida sobre el último escaneo para regenerar outputs (los anteriores quedan en `outputs/<timestamp>/` como histórico)
