# CP-013 KILLIOS WMS164 — Reporte Conclusión V3 (vigente)

> Reemplaza V2. V2 quedó obsoleto por dos razones: (1) factor caja era pregunta abierta, ahora confirmado = 5 UM/caja (presentación única "Caja5"); (2) faltaba el cruce histórico con AJUSTES manuales que confirma cronicidad >5 años. V2 también arrastraba contaminación textual al final del archivo, removida acá.

## 1. Caso reportado

- Cliente: KILLIOS, Bodega 1
- Producto: **WMS164** (IdProducto=77, IdProductoBodega=381)
- Lote en alerta: **BG2512** (FU06688, recepción IdRecepcionEnc=2179, 10-feb-2026)
- Síntoma original (Erik): "el stock entre fisico y wms no es el mismo y el wms tiene mas de lo que en realidad hay"
- Cuantificación original (Erik): WMS sobra **~14 cajas** vs físico ~13 cajas

## 2. Cuadre matemático del lote BG2512

**Factor caja confirmado: 5 UM/caja** (presentación única "Caja5", IdPresentacion=90, factor=5.0; W20-01)

**Stock activo BG2512 actual (W20-02, sin joins duplicantes):**

| IdStock | IdUbicacion | Ubic.   | LP       | UM   | Cajas |
|--------:|------------:|---------|----------|-----:|------:|
| 134176  | 22          | 3B1 (picking) | FU06688 | 40 | 8 |
| 134177  | 22          | 3B1 (picking) | FU06688 | 30 | 6 |
| 134178  | 308         | 2B2 (picking) | FU06688 | 65 | 13 |
| **Total BD** |        |               |          | **135** | **27** |

**Cuadre con observación física:**
- BD reporta: **27 cajas**
- Físico Erik: **~13 cajas**
- **Delta: BD sobra exactamente 14 cajas (70 UM)** ✓ coincide al 100% con la denuncia original

## 3. Causa raíz identificada

**BUG funcional sistémico TOMWMS:** cuando un picking se marca como "dañado" desde el backoffice (BOF VB.NET), el flujo:

- Pone `dañado_picking=1`, `cantidad_verificada=0`, `cantidad_despachada=0`, `encontrado=True`, `activo=1`
- **NO genera movimiento `AJCANTN` (IdTipoTarea=17)**
- **NO mueve la mercadería a una ubicación MERMA**
- **NO descuenta de `stock`** (tabla viva)

Resultado: el operador retira físicamente la mercadería de la ubicación picking, pero la BD sigue contándola en stock activo.

En BG2512 puntual:
- 12 líneas dañadas activas (W19-A) totalizan **148 UM = 29.6 cajas** marcadas como daño
- Recepciones 310 − Despachos 175 = saldo BD oficial 135 UM = 27 cajas
- 148 (dañadas) > 135 (saldo) — matemáticamente solo posible si dañado_picking no descuenta
- 12 líneas todas con `IdOperadorBodega_Pickeo=0` y `user_agr=20` (Heidi López, BOF) → procesadas desde backoffice, ningún operador HH involucrado

## 4. Conciliación de los 14 cajas con los 148 UM dañados

| Concepto                                      | UM    | Cajas |
|-----------------------------------------------|------:|------:|
| Marcados dañado_picking=1 activos (W19-A)     | 148   | 29.6  |
| Sobre-estimación BD vs físico medido por Erik | **70** | **14** |
| Implícito: dañados retirados físicamente sin descuento BD | 70 | 14 |
| Implícito: dañados que quedaron en sitio o falsos positivos | 78 | 15.6 |

Lectura: aproximadamente **47 % de los pickings dañados sí se retiraron físicamente** (operador llevó la mercadería a daños/descarte) → esos son los 14 cajas que sobran en BD. El resto (53 %) puede haberse marcado dañado pero quedado en sitio, o el operador la recuperó después sin reversar el flag.

## 5. Esto NO es un caso aislado — es endémico de toda la BD Killios

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

Productos top afectados (W19-G):

| IdProductoBodega | UM dañadas | Lotes |
|-----------------:|-----------:|------:|
| 395              | 24,580     | 19    |
| 1515             | 21,421     | 24    |
| 1350             | 18,978     | 11    |
| 730              | 16,068     | 5     |
| 1315             | 15,622     | 9     |
| 1320             | 13,535     | 12    |
| 1375             | 11,353     | 7     |
| 1495             | 11,186     | 8     |

WMS164/381 tiene 322 UM en total entre 4 lotes (BG2512:148, BM2601:110, BM2511:59, bm2508:5) — caso pequeño en el contexto general.

## 6. Causa raíz secundaria (proceso)

W18-05 (resumen movimientos por tipo de tarea WMS164):

| IdTipoTarea | Tipo  | n_movs | suma UM | Período                       |
|------------:|:------|-------:|--------:|:------------------------------|
| 1           | RECE  | 28     | 5,500   | 2025-12-04 → 2026-04-23       |
| 5           | DESP  | 106    | 6,502   | 2025-12-01 → 2026-04-28       |
| 8           | PIK   | 108    | 6,552   | 2025-12-01 → 2026-04-28       |
| 11          | VERI  | 107    | 2,814   | 2025-12-01 → 2026-04-28       |
| 2           | UBIC  | 91     | 11,506  | 2025-12-04 → 2026-04-24       |
| 3           | CEST  | 16     | 1,539   | 2025-12-20 → 2026-04-23       |
| **6**       | **INVE** | **6** | **1,095** | **2025-11-30 (un solo día)** |
| 17          | AJCANTN | 3    | 15      | 2026-04-10 (Aj1020 Mario Santizo, contra físico) |
| 20          | EXPLOSION | 3  | 15      | 2026-01-05 → 2026-02-25       |
| 25          | REEMP_BE_PICK | 1 | 75    | 2025-12-27                    |

**Cero inventarios formales** desde la carga inicial (30-nov-2025). En 5 meses de operación nadie auditó el stock de WMS164 contra el físico, por eso la sobre-estimación se acumula sin punto de corrección.

Adicional: las 12 líneas dañadas BG2512 NO generaron CESTs. Los 8 CESTs BUEN→MAL del producto (cubiertos en EVIDENCIAS-CRONICIDAD) son síntoma colateral: el operador de cesta detecta la diferencia visual al rotar mercadería, pero como no se corrige con AJCANTN el problema persiste.

## 7. Cruce histórico — el bug existe desde 2019, observación de Erik se confirma

Tabla `trans_ajuste_enc` + `trans_ajuste_det` (encabezado y detalle de ajustes manuales con motivo y observación). Total histórico Killios: **1,653 ajustes** entre 2019-09-12 y 2026-04-22.

**Distribución temporal en 3 fases (W21-06):**

| Fase                  | Ajustes positivos | Ajustes negativos | Lectura                                   |
|-----------------------|------------------:|------------------:|-------------------------------------------|
| 2019-09 → 2023-04     | 596               | 925               | Período activo: equipo corrigiendo permanentemente |
| 2023-04 → 2025-11     | **0**             | **0**             | **Gap de 2.5 años — sin auditoría aparente** |
| 2025-11 → 2026-04     | 5                 | 122               | Reactivación: **96 % negativos**          |

**Top motivos de ajustes (W21-05) — síntomas directos del mismo bug que vemos hoy:**

| Tipo + Motivo                              | n     | Lectura                                      |
|--------------------------------------------|------:|----------------------------------------------|
| Ajuste Positivo · Falla de sistema         | 297   | "El sistema perdió stock y lo agregamos manual" |
| Ajuste Negativo · Despacho Licencia        | 105   | "Se despachó pero no se descontó" ← idéntico a tu observación histórica |
| Ajuste Negativo · Ajuste contra físico     | 105   | Inventario encontró menos que BD             |
| Ajuste Positivo · Ajuste contra físico     |  34   | Inventario encontró más que BD               |
| Ajuste Negativo · Falla de sistema         |  20   | Falla de sistema en sentido inverso          |

**Observación de Erik validada:** "stock que no se restaba" + "elimino X cajas y el reporte cuadra" coincide exactamente con el patrón "Despacho Licencia + Ajuste contra físico, signo Negativo" que el equipo Killios viene aplicando manualmente desde 2019.

**Caso WMS164 ya tiene un ajuste de este patrón (W21-08, W21-09):**
- `idajusteenc=1020`, fecha 2026-04-10, usuario Mario Santizo (IdUsuario=13)
- Motivo "Ajuste contra físico", `ajuste_por_inventario=0` (manual, no parte de un INVE formal)
- 3 detalles, todos restando: JG000006/BM2511 −6 UM, FU06505/BM2601 −5 UM, JG000013/BM2601 −4 UM
- Total restado: 15 UM = 3 cajas
- **No tocó BG2512** → por eso el lote sigue con 14 cajas fantasma a la fecha del reporte

**Outlier detectado y descartado del análisis:** `idajustedet=638` del 13-may-2021 tiene `cantidad_original=4,804,442,747,532` UM (4.8 billones) ajustado a cero, observación "sa". Es dato corrupto/test mal hecho; distorsiona los agregados pero no representa el bug.

## 8. Saldo neto computado actual (W21-04)

`SUM(stock.cantidad) − SUM(stock_res.cantidad)` por producto: **0 productos con neto < 0** en este momento. La vista `VW_Stock_Resumen` actualmente no mostraría negativos, pero el patrón histórico explica que sí ocurría puntualmente cuando reservas vivas superaban stock real disponible (combinación de pedidos contra fantasmas + despachos posteriores que tampoco descontaban). El que Erik viera reportes con negativo en versiones viejas es consistente con el mismo bug operando con menor volumen de compensación manual.

## 9. Evidencias archivos

**outputs/wave-18/** — trazabilidad full WMS164
- W18-01 picking_ubic WMS164 toda la historia (219 KB)
- W18-02 picking_ubic BG2512 multi-producto
- W18-03 trans_movimientos WMS164 (13,644 movs, 3 MB)
- W18-04 movs con cantidad_hist != cantidad
- W18-05 resumen tipos de tarea
- W18-07 stock_hist BG2512
- W18-08 stock_hist WMS164 toda la historia
- W18-09/10 pickings con anomalía / anulados

**outputs/wave-19/** — cuantificación dañados Killios completo
- W19-A 12 líneas dañadas BG2512 activas
- W19-B suma por operador
- W19-E dañados WMS164 todos lotes (322 UM, 4 lotes)
- W19-F dañados Killios completo por usuario (25 usuarios, >320 mil UM)
- W19-G top 30 productos más afectados

**outputs/wave-20/** — desglose stock real BG2512
- W20-01 presentaciones WMS164 (factor caja = 5 UM)
- W20-02 stock activo BG2512 detalle por lp/ubic
- W20-03 stock activo WMS164 todos lotes
- W20-04 resumen ubicación BG2512

**outputs/wave-21/** — cruce histórico ajustes manuales
- W21-01/02 AJCANTN trans_movimientos (250 movs todos positivos, ya 30-nov en adelante)
- W21-04 saldo neto actual (cero negativos)
- W21-05 ajustes por tipo + motivo
- W21-06 ajustes por mes y signo (full historia 2019→2026)
- W21-07 top 30 productos ajustados históricamente
- W21-08 ajustes WMS164 (3 líneas, abril 2026)
- W21-09 detalle del ajuste enc=1020
- W21-10 ajustes recientes 2025-11 a hoy (220 encabezados)

## 10. Acciones recomendadas — ver PLAYBOOK-FIX.md

1. **Patch funcional inmediato** (BOF .NET): cuando se marca `dañado_picking=1` desde backoffice, debe pedir destino del dañado (área MERMA, devolución a proveedor, descarte) y generar `trans_movimiento` correspondiente que descuente stock.
2. **Reconciliación retroactiva** masiva: aplicar AJCANTN para descontar las >320,000 UM fantasma. Necesita conteo físico previo bodega completa.
3. **Inventarios formales periódicos** (política conteo cíclico ABC): cero inventarios en 5 meses es inaceptable.
4. **Inventario físico WMS164**: contar todos los lotes (no solo BG2512) y aplicar AJCANTN específico.
