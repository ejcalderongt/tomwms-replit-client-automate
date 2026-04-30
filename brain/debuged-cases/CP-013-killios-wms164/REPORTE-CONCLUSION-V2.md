# CP-013 KILLIOS WMS164 — Reporte Conclusión V2 (final)

> Reemplaza V1. V1 contenía la inferencia "48 unidades físicas" que NO tenía respaldo documental. Retirada.

## 1. Caso reportado

- Cliente: KILLIOS, Bodega 1
- Producto: **WMS164** (IdProducto=77, IdProductoBodega=381)
- Lote en alerta: **BG2512** (FU06688, recepción IdRecepcionEnc=2179, 10-feb-2026)
- Síntoma original (Erik): "el stock entre fisico y wms no es el mismo y el wms tiene mas de lo que en realidad hay"
- Cuantificación original (Erik): WMS sobra **~14 cajas** vs físico ~13 cajas

## 2. Resultado del cuadre matemático en BD

| Concepto                                | UM      | Origen                                  |
|-----------------------------------------|---------|-----------------------------------------|
| Recepciones (IdTipoTarea=1, RECE)       | 310     | trans_movimientos / W18-03              |
| Despachos    (IdTipoTarea=5, DESP)      | 175     | trans_movimientos / W18-03              |
| Ajustes formales (IdTipoTarea=17, AJCANTN) | 0    | trans_movimientos / W18-03 (BG2512 no recibió ajustes formales) |
| **Saldo BD oficial**                    | **135** | recepciones − despachos                 |
| Pickings BG2512 con `dañado_picking=1`  | **148** | trans_picking_ubic / W19-A              |

**Comparativa:** la cantidad marcada como dañada (148 UM) es **MAYOR** que el saldo BD actual (135 UM).
Esto solo es matemáticamente posible si el flag `dañado_picking` **no descuenta stock**. El sistema sigue contando esas unidades como existencia, aunque ya no estén físicamente en la ubicación.

## 3. Causa raíz identificada

**BUG funcional sistémico TOMWMS:** cuando un picking se marca como "dañado" desde el backoffice (BOF VB.NET, BG2512: 12 líneas, todas con `IdOperadorBodega_Pickeo=0` y `user_agr=20 = Heidi López`), el flujo:

- Pone `dañado_picking=1`, `cantidad_verificada=0`, `cantidad_despachada=0`, `encontrado=True`, `activo=1`
- **NO genera movimiento `AJCANTN` (IdTipoTarea=17)**
- **NO mueve la mercadería a una ubicación MERMA (IdTipoTarea no aparece)**
- **NO actualiza `producto_bodega_stock`**

Resultado: el operador retira físicamente la mercadería de la ubicación (o la separa en daños), pero la BD sigue contándola en la ubicación picking.

## 4. Esto NO es un caso aislado — es endémico de toda la BD Killios

W19-F y W19-G prueban que el problema es transversal a TODA la operación Killios desde junio 2025:

| user_agr | n_líneas | UM dañadas no descontadas | productos | lotes  | desde       | hasta      |
|----------|---------:|---------------------------:|----------:|-------:|-------------|------------|
| 10       | 3,426    | **142,509**               | 111       | 348    | 2025-06-09  | 2026-04-28 |
| 20 (Heidi López) | 4,036 | 88,848               | 210       | 415    | 2025-11-30  | 2026-04-28 |
| 13 (Mario Santizo) | 1,332 | 41,618             | 148       | 293    | 2025-09-05  | 2026-04-13 |
| 12       | 1,153    | 31,709                    | 133       | 226    | 2025-07-04  | 2026-02-13 |
| 28       | 217      | 5,682                     | 40        | 81     | 2025-06-19  | 2026-01-12 |
| (otros 20 users) | … | …                         | …         | …      | …           | …          |
| **TOTAL** | **~10,800** | **>320,000 UM**         | **>500**  | **>1,500** | jun-2025 | abr-2026 |

**320 mil unidades fantasma** acumuladas en la BD Killios. Es una sobre-estimación crónica del stock que ningún reporte refleja. Productos top afectados (W19-G):

| IdProductoBodega | UM dañadas | Lotes afectados |
|-----------------:|-----------:|----------------:|
| 395              | 24,580     | 19              |
| 1515             | 21,421     | 24              |
| 1350             | 18,978     | 11              |
| 730              | 16,068     | 5               |
| 1315             | 15,622     | 9               |
| 1320             | 13,535     | 12              |
| 1375             | 11,353     | 7               |
| 1495             | 11,186     | 8               |

(WMS164/381 tiene 322 UM en total entre 4 lotes — caso pequeño en el contexto general)

## 5. Causa raíz secundaria (proceso)

Confirmado en W18-05 (resumen movimientos por tipo de tarea WMS164):

| IdTipoTarea | Tipo  | n_movimientos | suma_cantidad | Período                       |
|------------:|:------|--------------:|--------------:|:------------------------------|
| 1           | RECE  | 28            | 5,500         | 2025-12-04 → 2026-04-23       |
| 5           | DESP  | 106           | 6,502         | 2025-12-01 → 2026-04-28       |
| 8           | PIK   | 108           | 6,552         | 2025-12-01 → 2026-04-28       |
| 11          | VERI  | 107           | 2,814         | 2025-12-01 → 2026-04-28       |
| 2           | UBIC  | 91            | 11,506        | 2025-12-04 → 2026-04-24       |
| 3           | CEST  | 16            | 1,539         | 2025-12-20 → 2026-04-23       |
| **6**       | **INVE** | **6**      | **1,095**     | **2025-11-30 (un solo día)**  |
| 17          | AJCANTN | 3           | 15            | 2026-04-10 (Aj1020 user13)    |
| 20          | EXPLOSION | 3         | 15            | 2026-01-05 → 2026-02-25       |
| 25          | REEMP_BE_PICK | 1     | 75            | 2025-12-27                    |

**Cero inventarios formales** desde la carga inicial (30-nov-2025). En 5 meses de operación nadie auditó el stock de WMS164 contra el físico. La sobre-estimación se acumula sin punto de corrección.

Adicional: las 12 líneas dañadas BG2512 NO generaron CESTs. Los 8 CESTs BUEN→MAL del producto (cubiertos en EVIDENCIAS-CRONICIDAD-V4) son síntoma colateral: el operador de cesta detecta la diferencia visual al rotar mercadería, pero como no se corrige con AJCANTN, el problema persiste.

## 6. Por qué Erik veía "WMS sobra ~14 cajas"

Si el factor caja es 12 UM/caja:
- BD: 135 UM = 11.25 cajas
- Físico Erik: 13 cajas = 156 UM
- Diferencia esperada: BD subreporta 21 UM (incongruente con "WMS sobra")

Si el factor caja es 6 UM/caja:
- BD: 135 UM = 22.5 cajas
- Físico Erik: 13 cajas = 78 UM
- BD sobra: 57 UM ≈ 9.5 cajas. Cerca de las "14 cajas".

**Pregunta abierta a Erik:** confirmar el factor caja real de WMS164 (cuántas UM por caja según presentación). Probable que sean 6 UM/caja (pares de 6-pack), pero hay que validar contra `producto_presentacion`.

Si son 6 UM/caja: la diferencia ~57 UM = ~9.5 cajas se explica con una FRACCIÓN de los 148 UM marcados como dañados. Es decir, no todos los daños se llevaron de la ubicación; algunos quedaron en sitio o el flag se marcó por error y luego el operador no recreó la línea de picking. El resto del delta (148 − 57 = 91 UM) debe estar:
- En área de daños/merma sin registrar movimiento
- Devuelto a otro lp sin trazabilidad
- O efectivamente perdido

## 7. Evidencias archivos (outputs/wave-18/ y outputs/wave-19/)

- W18-01 picking_ubic WMS164 toda la historia (219 KB)
- W18-02 picking_ubic BG2512 multi-producto (162 filas)
- W18-03 trans_movimientos WMS164 (13,644 movs, 3 MB)
- W18-04 movs con cantidad_hist != cantidad (12,996 — confirma cant_hist es snapshot post-mov)
- W18-05 resumen tipos de tarea (10 tipos, ningún INVE post 30-nov-2025)
- W18-06 lp multi-lote (solo lp="0" default; sin swap real)
- W18-07 stock_hist BG2512 (360 cambios, todos por user 18 = Auditoría)
- W18-08 stock_hist WMS164 toda la historia (3,888 cambios)
- W18-09 pickings con anomalía WMS164 (27 líneas)
- W18-10 pickings anulados activo=0 (0 — nunca se anuló nada)
- W19-A 12 líneas dañadas BG2512 activas
- W19-B suma por operador (todas user 20 Heidi López)
- W19-C cuadre cuantitativo (135 BD vs 148 dañados)
- W19-E dañados WMS164 todos lotes (BG2512:148, BM2601:110, BM2511:59, bm2508:5 = 322 UM total)
- W19-F dañados Killios completo por usuario (25 usuarios, >320 mil UM)
- W19-G top 30 productos más afectados

## 8. Acciones recomendadas — ver PLAYBOOK-FIX.md

1. **Patch funcional inmediato** (BOF .NET): cuando se marca `dañado_picking=1` desde backoffice, debe pedir "destino del dañado" (área MERMA, devolución a proveedor, descarte) y generar el `trans_movimiento` correspondiente.
2. **Reconciliación retroactiva**: aplicar AJCANTN para descontar las >320,000 UM fantasma. Necesita validación con conteo físico previo.
3. **Inventarios formales periódicos**: implementar política de conteo cíclico (ABC). Cero inventarios en 5 meses es inaceptable.
4. **Inventario físico WMS164**: contar todos los lotes (no solo BG2512) y aplicar AJCANTN específico. Calcular factor caja correcto.

