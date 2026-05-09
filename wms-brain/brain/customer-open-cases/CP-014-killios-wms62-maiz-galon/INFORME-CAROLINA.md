---
id: INFORME-CAROLINA-CP014
tipo: cp-open
estado: vigente
titulo: Informe tecnico para Carolina Fuentes — Caso WMS62 (donde buscar el bug en codigo)
cliente: killios
producto: WMS62
materializa_bug: BUG-001
fecha: 2026-05-09
audiencia: Carolina Fuentes (PrograX24 — analisis codigo)
ramas: [dev_2023_estable, dev_2028_merge]
---

# Informe tecnico para Carolina — CP-014 WMS62 Killios

**Para**: Carolina Fuentes — PrograX24
**De**: Erik Calderon — PrograX24
**Asunto**: Caso WMS62 Killios — Variante BUG-001, donde buscar el bug en codigo
**Fecha**: 09-may-2026

> Este informe asume conocimiento de **CP-013/REPORTE-CONCLUSION-V3.md**
> (ola 1 del bug, descubrimiento WMS164) y de
> **wms-known-issues/BUG-001-danado-picking-no-resta-inventario/INDEX.md**.

---

## 1. Resumen ejecutivo del hallazgo

Stock fantasma de **+10 cajas (60 UM)** en producto WMS62 bodega 1
KILIO-GARESA. La evidencia forense confirma que es **la misma raiz que
BUG-001 pero en una variante distinta**: aqui NO es solo el flag
`dañado_picking`, sino la **escritura desincronizada entre `stock` y
`trans_movimientos`** durante el ciclo recepcion-verificacion-picking.

Smoking gun: **17 de 17 matriculas (lic_plate) vivas en stock NO tienen
ningun registro en `trans_movimientos` para WMS62 bodega 1**. Sin
embargo el saldo SAP (reconstruido desde `trans_movimientos`
contabilizable) calza al kardex que entrego el cliente. Esto significa
que el WMS escribio en `stock` un universo de matriculas que jamas
fueron registradas en el log de movimientos contables.

---

## 2. Datos forenses (BD TOMWMS_KILLIOS_PRD_2026 snapshot 2026-05-09 10:23 UTC)

### 2.1. Match exacto del reporte

| Vista | UM | Cajas |
|---|---:|---:|
| Stock vivo bodega 1 (`VW_Stock_Resumen` filtrado IdProducto=271, IdBodega=1) | 2.741 | 456,83 |
| Kardex SAP del cliente (Excel, 908 movimientos historicos) | 2.681 | 446,83 |
| **Delta** | **+60** | **+10** |

### 2.2. Reconstruccion del kardex desde `trans_movimientos`

Sumando solo `IdTipoTarea` con `Contabilizar=true` en `sis_tipo_tarea`:

| TipoTarea | Nombre | Movs | UM neto |
|--:|---|--:|--:|
| 1 | RECE | 129 | +33.355 |
| 6 | INVE | 6 | +2.267 |
| 5 | DESP | 769 | -32.941 |
| **Saldo SAP reconstruido** | | | **+2.681** |

Coincide exacto al kardex del cliente. **El kardex SAP esta bien.**

### 2.3. El smoking gun

Los movs `trans_movimientos` de WMS62 bodega 1 estan dominados por
matriculas de la serie antigua **FU04xxx** (operadores Francisco
Us / Anderly Teleguario fechas 2025), pero el stock vivo actual usa
matriculas FU08xxx-FU09xxx, JM001xxx, A3000001 (recepciones 2026-04 y
2026-05). El cruce join lic_plate-a-lic_plate da **0 matches**.

```sql
-- 17 matriculas vivas, 0 en trans_movimientos
SELECT v.lic_plate, COUNT(tm.IdMovimiento) AS movs
FROM VW_Stock_Resumen v
INNER JOIN producto p ON p.IdProducto=v.IdProducto AND p.codigo='WMS62' AND v.IdBodega=1
LEFT JOIN producto_bodega pb ON pb.IdProducto=p.IdProducto AND pb.IdBodega=1
LEFT JOIN trans_movimientos tm ON tm.IdProductoBodega=pb.IdProductoBodega
                              AND tm.lic_plate COLLATE Modern_Spanish_CI_AS
                                = v.lic_plate COLLATE Modern_Spanish_CI_AS
GROUP BY v.lic_plate;
-- => todas devuelven movs=0
```

### 2.4. Distribucion por TipoTarea (todos los movs WMS62 bodega 1)

| TipoTarea | Nombre `sis_tipo_tarea` | Contabilizar | Movs | UM |
|--:|---|---|--:|--:|
| 8 | PIK (pickeo pre-verificacion) | true | 785 | 33.814 |
| 11 | VERI (verificacion despacho) | true | 779 | 10.044 |
| 5 | DESP (despacho final) | true | 769 | 32.941 |
| 2 | UBIC (ubicacion post-recepcion) | **false** | 310 | 41.542 |
| 1 | RECE (recepcion HH) | true | 129 | 33.355 |
| 3 | CEST (cambio estado) | **false** | 120 | 25.439 |
| 25 | REEMP_BE_PICK (reemplazo BE picking) | **false** | 51 | 1.704 |
| 20 | EXPLOSION | true | 9 | 54 |
| 12 | PACK (packing) | true | 8 | 894 |
| 6 | INVE (inventario) | true | 6 | 2.267 |
| 26 | REEMP_ME_PICK (reemplazo ME picking) | **false** | 4 | 30 |

> **Observacion**: 4 de las 11 categorias tienen `Contabilizar=false`
> (UBIC, CEST, REEMP_BE_PICK, REEMP_ME_PICK). Suman **485 movs / 68.715 UM**
> que NO se reflejan en kardex SAP. Aqui es donde puede estar inyectandose
> el desbalance.

---

## 3. Donde buscar el bug (analisis de codigo)

### 3.1. Hipotesis A — Recepcion HH escribe stock con lic_plate distinto al de trans_movimientos

**Sintoma observado**: las matriculas vivas en `stock` no aparecen en
`trans_movimientos` del producto. Pero `trans_movimientos.IdRecepcion`
si referencia a las recepciones (2880, 2832, 3057, 3071, 3076, 3091…).

**Lugar a auditar (BOF .NET)**:
- `TOMWMS_BOF/WSHHRN/RecepcionHH.asmx.vb` — endpoint `IngresarItemPallet`
  o equivalente. Verificar si:
  - el `lic_plate` que se escribe en `stock` proviene del mismo parametro
    que el que se escribe en `trans_movimientos`, o si hay dos sources
    diferentes (uno del payload SOAP, otro generado en el SP).
  - existe algun split/pad/replace sobre el lic_plate antes de uno de
    los dos INSERT (por ejemplo `.Replace(" ", "")` en stock pero no en
    trans_movimientos, o viceversa).
- SP llamado por la recepcion: buscar `sp_RecepcionAlta`,
  `sp_StockIngreso`, `sp_RecepcionConfirma` o similares.
  - Inspeccionar si el INSERT a `stock` y el INSERT a `trans_movimientos`
    estan en el mismo SP o en SPs distintos.
  - Si estan en SPs distintos: verificar si hay ROLLBACK que aplica solo
    a uno de los dos.

**Lugar a auditar (HH Java)**:
- `TOMHH2025/app/.../activities/RecepcionActivity.java` y
  `WebService.java` — metodo que llama `IngresarItemPallet`.
  - Verificar si el campo `barra_pallet` del payload SOAP/JSON es el
    mismo que se muestra al operador y el mismo que se persiste.
  - Recordar la regla del `replit.md` §4: la `ñ` NO debe reemplazarse;
    si hay `replace("ñ","n")` aplicado al lic_plate eso podria explicar
    desalineamientos para matriculas con caracteres especiales.

### 3.2. Hipotesis B — UPDATE-cae-INSERT (heredada de CP-013)

Es la misma hipotesis A de CP-013. **Probabilidad alta**: el patron
"matricula nueva sin historial" es exactamente lo que CP-013 hipotetizo
para WMS164 y la evidencia es analoga.

Lugar a auditar: revisar todos los `UPDATE stock SET cantidad = cantidad
- @x` que tienen un try/catch que ante `Cantidad > 0` violado hace
INSERT compensatorio. Ver `code-deep-flow/traza-002-danado-picking.md`.

### 3.3. Hipotesis C — TipoTarea no contabilizable inyecta stock sin contraparte

Las 4 categorias `Contabilizar=false` (TipoTarea 2 UBIC, 3 CEST,
25 REEMP_BE_PICK, 26 REEMP_ME_PICK) suman 485 movs / 68.715 UM.

**Especial atencion a TipoTarea=25 REEMP_BE_PICK** — el reemplazo de
matricula durante picking. Es el flujo donde el operador HH dice
"este pallet no esta o esta danado, dame otro" y el sistema le entrega
una nueva matricula. Si ese flujo:
1. Escribe la nueva linea de stock con lic_plate nuevo.
2. NO escribe el descuento de la lic_plate vieja en `trans_movimientos`
   contabilizable (queda solo el REEMP_BE_PICK no-contable).

**Resultado**: stock duplicado entre vieja matricula (que sigue viva)
y nueva matricula (recien creada). Coincide exactamente con el patron
de las 4 lineas FU08994/FU08995/FU09120/FU09220 lote 60120, todas en
ubicacion P15-C1-N1-A-#1038, todas marcadas REEMPACAR (294 UM cada una
= 49 cajas). Suman 196 cajas, 4 versiones del mismo pallet "fisico".

**Lugar a auditar (BOF + HH)**:
- WSHHRN endpoint `ReemplazoPallet` / `CambioPallet` o equivalente.
- Activity HH `ReemplazoPickingActivity.java`.
- SP asociado: buscar `sp_ReemplazoPallet*`.

### 3.4. Hipotesis D — Marcar REEMPACAR desde backoffice no descuenta

Patron analogo al fix §A propuesto en CP-013/PLAYBOOK-FIX.md (caso del
flag `dañado_picking`). El backoffice permite cambiar `IdProductoEstado`
de 1 (BUEN) a 8 (REEMPACAR) sin generar movimiento contable. Mismo
sintoma, distinto trigger.

**Lugar a auditar (BOF .NET)**:
- TOMIMSV4 forms / modulos: `frmGestionStock`, `frmStockReempacar`,
  o el grid editable de stock. Endpoint backoffice que ejecuta
  `UPDATE stock SET IdProductoEstado = 8 WHERE IdStock IN (…)`.

---

## 4. Cross-cliente — escala del problema

| Cliente | Lineas con `dañado=true` o estado problematico | AJCANTN compensatorios | Ratio |
|---|--:|--:|---|
| MERCOPAN | 19.607 | 0 | 0% |
| KILLIOS (2025) | 6.500 | 0 | 0% |
| KILLIOS_2026 | 10.565 | 0 | 0% |

(Fuente: `data-deep-dive/CROSS-COMPARATIVA.md` §F4 del session-plan
Atlas. Killios_2026 es la BD de este caso.)

WMS62 + WMS164 son solo los dos productos investigados a fondo. La
exposicion total es **>36.000 lineas** sin AJCANTN compensatorio.

---

## 5. Que necesito de vos

1. **Validar la hipotesis C primero** (REEMP_BE_PICK). Es la que mejor
   explica las 4 matriculas FU08994/95/9120/9220 lote 60120 con 49 cajas
   identicas en la misma ubicacion. Si ese flujo es el culpable, ataja
   muchos casos a futuro.
2. Identificar en codigo (BOF + HH) las funciones de:
   - Reemplazo de pallet en picking (TipoTarea 25/26).
   - Cambio manual de estado a REEMPACAR (IdProductoEstado 1->8) desde
     backoffice.
3. Cross-ref con flags `i_nav_config_enc` (si hay flag tipo
   `genera_ajcantn_reemplazo` o similar). Ver
   `reference/flags-callsites.md` cuando este disponible.
4. Si confirmas la hipotesis C, podemos extender el patch del PLAYBOOK
   CP-013 para cubrir tambien REEMP_BE_PICK / REEMP_ME_PICK.

Cuando tengas resultado, lo cargo a `code-deep-flow/traza-003-reemp-picking.md`
y actualizo el INDEX de BUG-001.

---

**Erik Calderon**
PrograX24 — Direccion de Desarrollo TOMWMS
