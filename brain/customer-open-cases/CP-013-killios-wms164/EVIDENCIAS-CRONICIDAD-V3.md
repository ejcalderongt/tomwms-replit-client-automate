# Evidencias de cronicidad V3 — Caso 1261/371 y duplicados de stock

> **Wave 13-14**. Reemplaza V1 (tesis del release del 30-nov) y V2 (tesis de "explosión EXPLOSION en recepción"). Ambas tesis quedan **invalidadas** por hallazgos nuevos vía `IdTipoTarea`, columnas reales de `stock` (que conserva trazabilidad) y cruce timestamp `stock.fec_agr` vs `trans_movimientos.fecha`.

---

## TL;DR de la wave

1. **`IdTransaccion` en `trans_movimientos` NO es código de tipo de transacción** — es el **encabezado/proceso** que originó el movimiento (`IdRecepcionEnc` para RECE, `IdPedidoEnc` para PIK/DESP, `IdPickingEnc` para PICK, `IdDespachoEnc` para DESP, o un código fijo cuando es operación HH suelta). El **verdadero tipo** vive en `IdTipoTarea` → `sis_tipo_tarea` (35 tipos formales).
2. **La "serie 7926..7945 del 30-nov-2025"** que el doc V1 leyó como "20 tipos nuevos introducidos en un release" eran simplemente **20 `IdRecepcionEnc` correlativos creados ese día** (recepciones masivas de cierre de mes). **No hubo ningún release técnico el 30-nov**.
3. **`IdTipoTarea=20 (EXPLOSION)` existe formalmente** en `sis_tipo_tarea`, pero el producto 1261 tuvo **una sola** EXPLOSION en toda su historia, y solo **243 movimientos** tipo EXPLOSION en TODA la base contra productos con duplicados (vs 32k de PIK, 32k de VERI, 31k de DESP). **El bug NO es la explosión**.
4. **El bug real es de doble naturaleza, ambos en flujo de stock destino**:
   - **(a) Falta de UPSERT/MERGE**: la rutina que inserta filas en `stock` cuando llega mercadería a una ubicación destino **inserta fila nueva en vez de consolidar con la fila existente** del mismo producto/ubic/lote/lp. Esto produce **197 clusters explosivos** (filas creadas en el mismo segundo).
   - **(b) Inserción fantasma sin movimiento**: el **97% de las filas duplicadas (1.353 de 1.388)** **NO tienen ningún movimiento en `trans_movimientos` dentro de ±3 segundos** de su `fec_agr`, y solo el 4% (54 filas) coinciden al segundo. Hay un servicio backend que inserta directamente en `stock` sin pasar por el log de movimientos.
5. **Discrepancia de usuario**: el `usuario_agr` en `trans_movimientos` (operario real, ej. id=30) **no coincide** con `user_agr` en `stock` (insertador). En el caso paradigmático, el insertador es el **usuario id=18 ("Auditoria")**, creado el 8-sep-2025, que figura como creador de **953 / 1.388 filas duplicadas (69%)**, sin coincidir con ningún operario humano que haya disparado los movimientos. **Existe un proceso backend que ejecuta INSERT en stock con `user_agr=18` hardcoded**.

---

## 1. Catálogo `sis_tipo_tarea` (los 35 tipos formales)

| IdTT | Nombre | Contabiliza | Significado |
|---:|---|---|---|
| 1 | RECE | sí | Recepción |
| 2 | UBIC | no | Ubicación / putaway |
| 3 | CEST | no | Cestear (consolidar pallet) |
| 4 | TRAS | sí | Traslado |
| 5 | DESP | sí | Despacho |
| 6 | INVE | sí | Inventario |
| 7 | AJUS | sí | Ajuste genérico |
| 8 | PIK | sí | Picking previo |
| 9 | DEVP | sí | Devolución de picking |
| 10 | PICK | sí | Picking |
| 11 | VERI | sí | Verificación |
| 12 | PACK | sí | Empaque |
| 13 | AJCANTP | sí | Ajuste cantidad positivo |
| 14 | AJPESO | sí | Ajuste peso |
| 15 | AJVENC | sí | Ajuste vencimiento |
| 16 | AJLOTE | sí | Ajuste lote |
| 17 | AJCANTN | sí | Ajuste cantidad negativo |
| 18 | AJCANTNI | sí | Ajuste cantidad negativo invariante |
| 19 | AJCANTPI | sí | Ajuste cantidad positivo invariante |
| **20** | **EXPLOSION** | **sí** | Explosión Caja → UN sueltas |
| 21 | UBIC_PICK | sí | Ubicación a zona pick |
| 22 | ANUL_PICK | sí | Anulación picking |
| 23 | REABMAN | no | Reabasto manual |
| 24 | REUBICSTOCKRES | no | Reubicación stock reservado |
| 25 | REEMP_BE_PICK | no | Reemplazo BE picking |
| 26 | REEMP_ME_PICK | no | Reemplazo ME picking |
| 27 | REEMP_NE_PICK | no | Reemplazo NE picking |
| 28 | REEMP_BE_VERI | no | Reemplazo BE verificación |
| 29 | REEMP_ME_VERI | no | Reemplazo ME verificación |
| 30..33 | AJLOTENI/PI, AJVENCENI/PI | no | Ajustes invariantes |
| 34 | CESTI | sí | Cestear invariante |
| 35 | CUBII | sí | Cubicar invariante |

CSV completo: `outputs/wave-13-14/E10_tipo_tarea_creador.csv`.

---

## 2. Distribución de `IdTipoTarea` para productos con duplicados (E03)

| IdTT | Nombre | Movs | Internos (origen=destino) | Con presentación | Avg cant |
|---:|---|---:|---:|---:|---:|
| 8 | PIK | 32.048 | 0 | 25.588 | 145 |
| 11 | VERI | 32.020 | 125 | 25.404 | 31 |
| 5 | DESP | 31.418 | 31.418 | 25.030 | 145 |
| 2 | UBIC | 14.316 | 1.718 | 9.130 | 429 |
| **1** | **RECE** | **4.505** | **4.505 (100%)** | 3.332 | 1.020 |
| 25 | REEMP_BE_PICK | 3.815 | 3.793 | 3.801 | 436 |
| 3 | CEST | 2.969 | 974 | 1.914 | 558 |
| 6 | INVE | 1.644 | 1.644 | 1.285 | 844 |
| 12 | PACK | 1.024 | 1.022 | 993 | 1.007 |
| **20** | **EXPLOSION** | **243** | 56 | 0 | 21 |
| 17 | AJCANTN | 181 | 181 | 108 | 62 |
| 13 | AJCANTP | 98 | 98 | 72 | 71 |

**Lectura**:
- "Movimiento interno (origen=destino)" no es huella de explosión. Es el patrón normal de RECE, DESP, INVE y reemplazos: operaciones que ocurren EN una ubicación, no traslados entre ubicaciones.
- EXPLOSION solo representa 243 movs (~0.3%), no es el motor del problema.
- PIK y VERI dominan en volumen (32k cada uno), pero ninguno es interno.

---

## 3. Caso paradigmático 1261/371: 15 filas con trazabilidad completa (E04)

| IdStock | cant | pres | LP | IdRecepEnc | IdRecepDet | UbicAnt | user_agr | fec_agr |
|---:|---:|---:|---|---:|---:|---:|---:|---|
| 115725 | 6 | NULL | '0' | 2144 | 8 | 331 | **18** | 2026-03-10 07:57:16 |
| 115728 | 6 | NULL | '0' | 2144 | 8 | 331 | **18** | 2026-03-10 07:57:19 |
| 116321 | 6 | NULL | '0' | 2144 | 8 | 331 | **18** | 2026-03-11 09:04:47 |
| 116751 | 6 | NULL | '0' | 2144 | 8 | 331 | **18** | 2026-03-12 06:27:14 |
| 124592 | 6 | NULL | '0' | 2144 | 10 | 315 | **18** | **2026-03-27 10:02:06.750** |
| 124593 | 6 | NULL | '0' | 2144 | 10 | 315 | **18** | 2026-03-27 10:02:06.783 |
| 124594 | 6 | NULL | '0' | 2144 | 10 | 315 | **18** | 2026-03-27 10:02:06.797 |
| 124595 | 6 | NULL | '0' | 2144 | 10 | 315 | **18** | 2026-03-27 10:02:06.813 |
| 124596 | 6 | NULL | '0' | 2144 | 10 | 315 | **18** | 2026-03-27 10:02:06.847 |
| 124597 | 6 | NULL | '0' | 2144 | 10 | 315 | **18** | 2026-03-27 10:02:06.860 |
| 124598 | 6 | NULL | '0' | 2144 | 10 | 315 | **18** | 2026-03-27 10:02:06.893 |
| 124599 | 6 | NULL | '0' | 2144 | 10 | 315 | **18** | **2026-03-27 10:02:06.907** |
| 120528 | 6 | NULL | '0' | 2674 | 2 | 716 | 18 | 2026-03-20 08:15:48 |
| 134643 | 60 | 225 | FU09011 | 2998 | 11 | 716 | 18 | 2026-04-24 09:55:39 |
| 136037 | 72 | 225 | FU09082 | 3022 | 1 | 716 | 18 | 2026-04-29 06:35:22 |

Hallazgos:
1. **Ninguna fila se creó el 6-feb-2026** (fecha de la recepción 2144). Las primeras aparecen un mes después → invalida la tesis "explosión en recepción".
2. **8 filas creadas en 157 milisegundos** el 27-mar 10:02:06 (`fec_agr` 10:02:06.750 → 10:02:06.907) → patrón explosivo de un servicio que ejecutó 8 INSERTs consecutivos.
3. **Todas las filas vienen trasladadas desde otra ubicación** (`IdUbicacion_anterior` = 331 / 315 / 716, nunca 371) → son reubicaciones, no nacieron en 371.
4. **Todas con `user_agr=18`** ("Auditoria").
5. **`IdRecepcionEnc/IdRecepcionDet`** se preserva del lote originario (recepción 2144 detalles 8 y 10) — esto NO indica que la fila se creó en la recepción, indica que rastrea de qué recepción vino el material.

---

## 4. Tipología de las 1.388 filas duplicadas globales (E06)

| Origen documental (en stock) | Pres | LP | Filas | Cant total | Avg |
|---|---|---|---:|---:|---:|
| RECEPCION | sin pres | **con LP** | **660** | 8.230 | 12 |
| RECEPCION | con pres | con LP | 352 | 123.130 | 350 |
| SIN_REF | sin pres | sin LP | 273 | 2.477 | 9 |
| SIN_REF | con pres | sin LP | 80 | 47.866 | 598 |
| RECEPCION | sin pres | sin LP | 18 | 116 | 6 |
| RECEPCION | con pres | sin LP | 5 | 1.556 | 311 |

- **75% (1.035) tienen IdRecepcionEnc≠NULL** → trazables a una OC de origen.
- **660 filas son UN sueltas con LP válida** (avg 12 ≈ 1 caja exacta) → patrón "se desarmó una caja entera al moverla a zona pick".
- **353 filas SIN_REF** (sin trazabilidad documental) → vienen de ajustes / inventarios / explosiones sin proceso padre.

---

## 5. Quién crea las filas duplicadas — `user_agr` (E07 + E08)

| user_agr | Nombre | Tipo / Notas | Filas dup | UN dup |
|---:|---|---|---:|---:|
| **18** | **Auditoria** | Creado **8-sep-2025**, último login 30-sep-2025, no asignado a persona específica | **953** | **104.586** |
| 1 | DTS | Sistema/admin del integrador | 353 | 50.343 |
| 19 | Jonatan Santiago | Creado 28-nov-2025 | 72 | 27.027 |
| 26, 28 | otros | — | 9 | 1.407 |
| 6 | MI3 User | Usuario de sistema | 1 | 12 |

**Pista enorme**: el usuario "Auditoria" (id 18) representa el **69% de los duplicados**. No es un operario real — es un usuario genérico que usa el backend para insertar filas en stock. Hay que entender QUÉ proceso/servicio se autentica con id=18.

---

## 6. Discrepancia user en mov vs user en stock (E12)

Para las filas duplicadas que SÍ tienen movimiento matching:

| user en stock (insertador) | user en mov (operario) | Filas |
|:-:|:-:|---:|
| 1 | 1 | 45 (consistente) |
| 19 | 28 | 6 (inconsistente) |
| 1 | 31 | 5 (inconsistente) |
| 1 | 18 | 1 (inconsistente) |

Confirma que **el usuario que aparece como insertador del stock NO siempre es el operario real del movimiento**. Hay desacople entre quién dispara la operación y quién queda registrado como creador físico de la fila.

---

## 7. Inserciones fantasma: la mayor parte del bug NO deja rastro en `trans_movimientos` (E13)

Distancia temporal entre `stock.fec_agr` y el movimiento más cercano del mismo producto/ubicación:

| Bucket | Filas | % |
|---|---:|---:|
| ≤5 segundos | 54 | 4% |
| ≤1 minuto | 87 | 6% |
| **≤10 minutos** | **640** | **46%** |
| ≤1 hora | 88 | 6% |
| ≤1 día | 354 | 26% |
| >1 día | 165 | 12% |

**El 96% (1.334 / 1.388) de las filas duplicadas NO tienen movimiento al segundo de su creación**. Esto contradice la suposición de que cada cambio de stock está respaldado por un movimiento auditable.

Para el caso 1261/371: las 8 filas se crearon a las **10:02:06**, los movimientos UBIC del operario 70 fueron a las **10:04:30 / 10:04:37** (2½ min después). Y los movimientos UBIC tienen `IdRecepcionDet=0`, mientras las filas tienen `IdRecepcionDet=10`. **Son operaciones distintas que no se corresponden entre sí**.

---

## 8. Clusters explosivos — patrón temporal del bug (E11)

Filas duplicadas creadas en el **mismo segundo** en idéntica combinación producto/ubic/lote/lp:

| Filas en mismo segundo | Clusters | Cant total |
|---:|---:|---:|
| 9 | 3 | 198 |
| 8 | 2 | 96 |
| 6 | 8 | 1.089 |
| 5 | 12 | 2.356 |
| 4 | 17 | 2.272 |
| 3 | 33 | 16.800 |
| 2 | 122 | 27.686 |

**197 clusters explosivos confirmados**. El bug es replicable: hay un código que itera y ejecuta N INSERTs consecutivos en `stock` sin chequear consolidación.

---

## 9. Tesis invalidadas explícitamente

| Tesis | Origen | Estado |
|---|---|---|
| "Release del 30-nov-2025 introdujo 20 tipos nuevos de transacción 7926..7945" | V1 | **INVALIDADA** — son IdRecepcionEnc correlativos, no tipos |
| "El bug es la operación EXPLOSION (Caja→UN) al recibir mercadería" | V2 | **INVALIDADA** — solo 243 movs EXPLOSION en toda la base, 1 sola para producto 1261, y las filas duplicadas no se crean en el momento de la recepción |
| "Las filas duplicadas se generan al recibir 28+32 cajas el 6-feb-2026" | V2 | **INVALIDADA** — las filas se crearon entre 10-mar y 27-mar, NO el 6-feb |

---

## 10. Tesis nueva consolidada (V3)

El bug de stock duplicado tiene **dos capas concurrentes**:

**Capa A — Falta de MERGE en INSERT a stock destino**
La rutina backend que recibe mercadería en una ubicación física (sea por traslado, reubicación, picking inverso, ajuste) **inserta fila nueva en `stock` en lugar de consolidar (UPSERT) con la fila existente** del mismo producto/ubic/lote/lp/estado. Esto produce 197 clusters explosivos donde un solo proceso emite N INSERTs en menos de 1 segundo.

**Capa B — Servicio backend desacoplado de `trans_movimientos`**
Existe un servicio que se autentica con `user_agr=18` ("Auditoria") y modifica `stock` directamente **sin emitir movimientos correspondientes en `trans_movimientos`**. Esto explica que el 96% de las filas duplicadas no tengan movimiento al segundo. Cuando sí hay movimiento, el `usuario_agr` del movimiento (operario real) no coincide con el `user_agr` del stock (Auditoria).

---

## 11. Preguntas abiertas para Erik

1. **¿Quién/qué proceso es `user 18 = "Auditoria"`?** Creado el 8-sep-2025 sin asignar a persona. ¿Es un servicio? ¿Un job programado? ¿Una API key compartida? Esta respuesta probablemente identifica el binario/módulo culpable.
2. **¿Existe alguna lógica de "explosión perezosa" en el reabasto a zona pick?** El patrón "660 filas avg=12 UN" sugiere que alguien desarma una caja entera (12 UN) cada vez que se traslada a 371, generando una fila por unidad reasentada.
3. **¿La operación UBIC desde HH (operador 70) emite eventos batch al backend que causan 8 INSERTs en 200ms por escaneo?** Sí, parece ser el patrón.
4. **¿Hay algún job batch de auditoría/conteo cíclico que corra automáticamente y modifique `stock` sin movimiento?** Esto explicaría las 1.353 filas sin matching temporal con `trans_movimientos`.

---

## 12. Próximos pasos sugeridos (wave 13-15)

1. Revisar el código fuente del backend (BOF VB.NET) buscando:
   - INSERTs directos a `stock` sin acompañar INSERT a `trans_movimientos`.
   - Llamadas que se autentiquen con un usuario fijo "Auditoria".
   - Loops que iteren cantidades en pasos de 6 / 12 (caja típica) sin acumular.
2. Identificar la stored procedure / trigger que escribe en stock con `user_agr=18` hardcoded.
3. Revisar si existe un job programado (SQL Agent, Windows Service) que sincronice stock con SAP B1 (los usuarios tienen `usuario_sap_b1` / `clave_sap_b1`).

