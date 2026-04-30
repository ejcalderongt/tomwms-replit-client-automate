# PLAYBOOK-FIX CP-013 — Remediacion bug dañado_picking sin descuento

## Severidad: CRITICA (sistemica, multi-cliente potencial)

> **Actualizacion 2026-04-30 (agente Replit)**: agregadas secciones G (Guia
> de codigo paso a paso para el programador) y H (Estrategia de ramas: por
> que se aplica primero a `dev_2028_merge` y no a `dev_2023_estable`).
> Corregida la recomendacion previa que decia "aplicar a 2023". Ver §H.
> Material fuente: `brain/code-deep-flow/traza-002-danado-picking.md`.

---

## A. Patch funcional inmediato (BOF .NET — 1 semana)

### Cambio en modulo de gestion de pickings backoffice

Hoy: el backoffice permite marcar `dañado_picking = 1` sobre una linea `trans_picking_ubic` y guarda el cambio sin mas efectos.

Cambio requerido:

1. **Forzar seleccion de destino del dañado** mediante combo obligatorio:
   - "Mover a area MERMA" (default) → genera `trans_movimiento` IdTipoTarea=8 (PIK) origen=ubic_picking destino=ubic_merma con `cantidad = cantidad_solicitada`.
   - "Devolver a proveedor" → genera salida con motivo devolucion.
   - "Descarte / perdida" → genera `trans_movimiento` IdTipoTarea=17 (AJCANTN) con cantidad negativa.
2. **Pedir motivo + observacion obligatorios** y guardarlos en una tabla nueva `trans_picking_danado_log` (IdPickingUbic, motivo, destino, user_agr, fec_agr, observacion).
3. **Generar el descuento de `producto_bodega_stock`** segun el destino seleccionado.
4. **Bloquear el guardado si no hay ubicacion MERMA configurada** para la bodega.

### Validacion QA

Caso golden:
- Crear lote ficticio LOTE-TEST con 100 UM en ubicacion PICK01.
- Generar picking de 30 UM. Marcar dañado desde backoffice → debe pedir destino.
  - Si destino=MERMA → stock PICK01 debe quedar 70, stock MERMA debe quedar 30.
  - Si destino=AJCANTN → stock PICK01 debe quedar 70, sin stock nuevo.
- Verificar `trans_movimientos` registra el movimiento con tipo correcto.

## B. Reconciliacion retroactiva de las >320,000 UM fantasma (1-2 meses)

**No se puede aplicar AJCANTN a ciegas.** Pasos:

1. **Inventario fisico bodega completa** (Killios B1, ~10,800 lineas afectadas, ~500 productos).
   - Usar listado W19-G como prioridad: top 30 productos = ~150 mil UM (47% del problema).
   - Cuenta ciclica ABC, 30 dias.
2. **Recalcular delta esperado** por producto:
   - delta_esperado = saldo_BD − fisico_real
   - delta_dañados = SUM(cantidad_solicitada WHERE dañado_picking=1 AND activo=1)
3. **Aplicar AJCANTN** por producto:
   - Si fisico_real ≈ saldo_BD − delta_dañados → todos los dañados son reales: aplicar AJCANTN -delta_dañados.
   - Si fisico_real difiere → investigar mermas adicionales no reportadas.
4. **Marcar pickings reconciliados** con un nuevo flag `reconciliado_aj=1` para no contarlos otra vez.

## C. Inventarios formales periodicos (proceso, 1 mes)

Hoy: 6 movimientos INVE en historia, todos del 30-nov-2025. Cero inventarios desde entonces.

Implementar:
1. **Politica de conteo ciclico ABC**:
   - A (top 20% productos = 80% facturacion): conteo mensual.
   - B (siguiente 30%): conteo trimestral.
   - C (resto): conteo semestral.
2. **Reporte de discrepancia obligatorio** cada conteo: si delta > 5%, escalar a supervision.
3. **Inventario general anual** completo.

## D. Inventario WMS164 especifico (esta semana)

Para cerrar el caso reportado por Erik:

1. Contar fisico de TODOS los lotes WMS164 (no solo BG2512). Lotes activos segun W18-08:
   - BG2512 (lote alerta)
   - BM2601, BM2511, bm2508, bw2511, BM2510 (otros lotes con dañados o presencia activa)
2. Confirmar **factor caja real** del producto (UM por presentacion). Probable 6 o 12, validar con `producto_presentacion`.
3. Aplicar AJCANTN por lote segun diferencia fisico vs BD.
4. Validar que el stock total queda consistente.

## E. Hardening del modulo BOF (mediano plazo, 3 meses)

1. **Audit log inmutable** para todas las acciones de backoffice sobre pickings (no solo dañados).
2. **Reverso supervisado**: permitir anular un picking dañado mal marcado, con doble autorizacion.
3. **Dashboard de daños**: panel con tendencia mensual de daños por producto/operador para detectar abusos.
4. **Validacion cruzada**: cada vez que se marque dañado_picking=1, alertar si el producto+lote tiene >5% de daños historicos.

## F. Riesgo si NO se ejecuta

- La sobre-estimacion seguira creciendo (~32 mil UM/mes al ritmo actual).
- En 1 año mas, el inventario contable sera irreal en >40% para varios SKUs.
- Perdidas por venta no cumplida + ajustes contables masivos.
- Posibles desvios no detectados (mermas reales mezcladas con falsos dañados).

---

# G. Guia de codigo paso a paso para el programador

Esta seccion es el "manual de orientacion" para que el dev que tome el ticket
sepa exactamente que abrir, en que orden, y que verificar antes de tocar
cualquier linea. **Todas las referencias usan rama `dev_2028_merge`** (ver §H
para la justificacion). Las lineas estan tomadas del snapshot del 2026-04-30
en Azure DevOps; pueden moverse 1-2 lineas si hay cambios posteriores, pero
el contexto del bloque sigue siendo unico.

Repos involucrados:
- `https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_git/TOMWMS_BOF` (rama `dev_2028_merge`)
- `https://dev.azure.com/ejcalderon0892/TOMHH2025/_git/TOMHH2025` (rama equivalente, ver §H.3)

## G.0 Tabla "que leer primero" — orden recomendado

| # | Si queres entender... | Abri este archivo | Lineas | Por que |
|:-:|---|---|---|---|
| 1 | El sintoma observable en BD | `traza-002-danado-picking.md` §0 + §3 | todo §3 | numeros crudos Killios, anomalia default NULL, cero triggers |
| 2 | El intento previo de fix | `clsLnStock_res_Partial.vb` (rama 2028) | **1998-2008** | bloque COMENTADO con `'`, alguien empezo y no termino |
| 3 | Donde se prende el flag desde el HH | `clsLnTrans_ubic_hh_enc_Partial.vb` (rama 2028) | **1373-1396** | comentario historico `'#AT 20220110` — origen del bug en CEST |
| 4 | Donde se prende desde el backoffice | `clsLnStock_res_Partial.vb` (rama 2028) | **2306-2354** | 2 setters reales (mas otros 2 comentados) |
| 5 | Como el sistema "ignora" dañados en totales | `clsLnTrans_pe_det_Partial.vb` (rama 2028) | **822-823, 985-986** | SELECTs con `WHERE ISNULL(dañado_picking, 0) = 0` |
| 6 | Que NO hace la BD por su cuenta | query SQL del paso G.1.1 abajo | n/a | confirmar 0 triggers y 0 SPs sobre la columna |
| 7 | El contrato SOAP que viaja entre HH y BOF | `WSHHRN/WMS.xml` + `WSHHRN/Update_BD_WMS.sql` | grep `Danado_picking` | confirma que el flag cruza por SOAP serializado sin Ñ |

## G.1 Verificacion previa (no toques nada todavia)

### G.1.1 Confirmar el estado en BD del cliente target

Conectarse a la BD del cliente que se va a usar para QA (sugerido: MAMPA QA
si el fix se prueba en `dev_2028_merge`, o crear una BD copia de Killios
PRD para QA si va a hotfix de 2023).

```sql
-- 1. Estructura de la columna y default
SELECT c.name, t.name AS tipo, c.is_nullable,
       OBJECT_DEFINITION(c.default_object_id) AS default_value, c.column_id AS pos
FROM sys.columns c
JOIN sys.types t ON c.user_type_id = t.user_type_id
WHERE object_id = OBJECT_ID('trans_picking_ubic')
  AND c.name IN ('dañado_picking', 'dañado_verificacion');
-- Esperado en Killios PRD: dañado_picking pos=41 default=NULL,
--                          dañado_verificacion pos=27 default=((0))
-- Si default es ((0)) en QA: la BD QA ya esta migrada con default 0,
--   confirmar antes de aplicar el ALTER de C-011.

-- 2. Triggers y SPs sobre la tabla (debe dar 0)
SELECT 'TRIGGERS' AS tipo, COUNT(*) AS cnt
FROM sys.triggers WHERE parent_id = OBJECT_ID('trans_picking_ubic')
UNION ALL
SELECT 'SPs que tocan la columna' AS tipo, COUNT(DISTINCT object_id) AS cnt
FROM sys.sql_modules
WHERE definition LIKE '%dañado_picking%' COLLATE Modern_Spanish_CI_AS;

-- 3. Constraint hipotesis A INFORME-EJECUTIVO (cola C-008)
SELECT name, OBJECT_NAME(parent_object_id) AS tabla, definition
FROM sys.check_constraints
WHERE parent_object_id IN (OBJECT_ID('producto_bodega_stock'), OBJECT_ID('stock'))
  AND definition LIKE '%antidad%';
-- Si aparece CHECK (Cantidad > 0), confirma hipotesis: el UPDATE de stock
-- a 0 falla, y debe haber un try/catch que termina insertando fila nueva.
-- Esa logica de catch hay que neutralizar antes de aplicar el fix.

-- 4. Conteo de filas afectadas en este cliente
SELECT
  SUM(CASE WHEN ISNULL(dañado_picking,0)=1 THEN 1 ELSE 0 END) AS marcadas,
  COUNT(*) AS total,
  CAST(100.0*SUM(CASE WHEN ISNULL(dañado_picking,0)=1 THEN 1 ELSE 0 END)/COUNT(*) AS DECIMAL(5,2)) AS pct
FROM trans_picking_ubic;
-- Killios: 6500/26567 = 24%
-- MAMPA QA: 0/X = 0% (si da 0, hay que generar casos sinteticos para QA)
```

### G.1.2 Descargar y leer los 5 archivos cruciales

Bajada de cada archivo via API REST Azure DevOps (rama 2028):

```bash
PAT_B64=$(printf ":%s" "$AZURE_DEVOPS_PAT" | base64 -w0)
BR=dev_2028_merge

for path in \
  "/TOMIMSV4/DAL/Transacciones/Stock_Reservado/clsLnStock_res_Partial.vb" \
  "/TOMIMSV4/DAL/Transacciones/Transaccion_Ubicacion_HH/clsLnTrans_ubic_hh_enc_Partial.vb" \
  "/TOMIMSV4/DAL/Transacciones/Pedido/clsLnTrans_pe_det_Partial.vb" \
  "/TOMIMSV4/DAL/Transacciones/Picking/clsLnTrans_picking_ubic_Partial.vb" \
  "/TOMIMSV4/Entity/Transacciones/Picking/Trans_Picking_Ubic/clsBeTrans_picking_ubic.vb"
do
  enc=$(python3 -c "import urllib.parse,sys; print(urllib.parse.quote(sys.argv[1]))" "$path")
  out="/tmp/$(basename $path)"
  curl -s -H "Authorization: Basic $PAT_B64" \
    "https://dev.azure.com/ejcalderon0892/TOMWMS_BOF/_apis/git/repositories/TOMWMS_BOF/items?path=${enc}&versionDescriptor.version=${BR}&versionDescriptor.versionType=branch&api-version=7.1-preview.1" \
    -o "$out"
  echo "$(wc -c < $out) bytes -> $out"
done
```

Leer en este orden, marcando con un comentario `' TODO-FIX-DANADO` los puntos a tocar:

1. `clsBeTrans_picking_ubic.vb` (2.4 KB) — entendimiento del modelo, no se toca.
2. `clsLnStock_res_Partial.vb` lineas 1998-2008 (bloque comentado del intento previo).
3. `clsLnTrans_ubic_hh_enc_Partial.vb` lineas 690-700, 719-731, 760-768, 1032-1042, 1059-1071, 1373-1396 (los 6 setters).
4. `clsLnStock_res_Partial.vb` lineas 2306-2362 (los 2 setters reales rama 2028).
5. `clsLnTrans_pe_det_Partial.vb` lineas 822, 985 (las 2 SELECTs principales que filtran "no dañado").

## G.2 Cambios concretos por archivo

### G.2.1 `clsLnStock_res_Partial.vb` — terminar el fix parcial

**Contexto del bloque comentado en rama 2028** (lineas 1998-2008):

```vbnet
' Lo que hay HOY (mal):
'    If EsPicking Then
'        bePickingUbicExistente.Dañado_picking = True
'    Else
'        bePickingUbicExistente.Dañado_verificacion = True
'    End If
'
'    bePickingUbicExistente.Dañado_picking = False
'    bePickingUbicExistente.Dañado_verificacion = False
```

Alguien lo comento sin explicar por que ni que reemplazar. **Cola C-010**
(git blame en Azure DevOps) debe identificar al autor antes de cambiar
nada — quizas su contexto cambia el plan.

**Lo que debe quedar despues del fix** (descomentar + agregar la
generacion de movimiento compensatorio):

```vbnet
If EsPicking Then
    bePickingUbicExistente.Dañado_picking = True

    ' --- FIX CP-013/CP-015: generar trans_movimiento compensatorio ---
    Dim pIdMotivoUbicacion As Integer = clsLnBodega.Get_IdMotivoUbicacion_Dañado_Picking(
        bePickingUbicExistente.IdBodega, lConnection, lTransaction)
    If pIdMotivoUbicacion = 0 Then
        Throw New Exception(
            "No esta definido IdMotivoUbicacion para daños en la bodega " &
            bePickingUbicExistente.IdBodega.ToString() &
            ". No se puede registrar el daño sin destino fisico definido.")
    End If

    Dim cantDañada As Decimal = bePickingUbicExistente.Cantidad_Solicitada -
                                bePickingUbicExistente.Cantidad_Recibida
    If cantDañada <= 0 Then cantDañada = bePickingUbicExistente.Cantidad_Solicitada

    Dim beMov As New clsBeTrans_movimientos With {
        .IdTipoTarea = 8,                            ' PIK
        .IdProducto = bePickingUbicExistente.IdProducto,
        .Lic_plate = bePickingUbicExistente.Lic_plate,
        .IdUbicacion_origen = bePickingUbicExistente.IdUbicacion,
        .IdUbicacion_destino = pIdMotivoUbicacion,
        .Cantidad = cantDañada,
        .IdBodega = bePickingUbicExistente.IdBodega,
        .User_agr = bePickingUbicExistente.User_mod,
        .Fec_agr = DateTime.Now,
        .Observacion = "Auto-generado por marca dañado_picking"
    }
    clsLnTrans_movimientos.Insert(beMov, lConnection, lTransaction)

    ' Tambien insertar en log dedicado (tabla nueva propuesta en §A)
    clsLnTrans_picking_danado_log.Insert_FromUbic(
        bePickingUbicExistente, "AUTO_BOF_STOCKRES", lConnection, lTransaction)
    ' --- FIN FIX ---
Else
    bePickingUbicExistente.Dañado_verificacion = True
End If
```

**Aplicar el mismo patron en los 2 setters reales restantes** del archivo
en rama 2028: lineas 2307 y 2349 (y los hermanos `False` de 2311, 2353
quedan como estan).

### G.2.2 `clsLnTrans_ubic_hh_enc_Partial.vb` — flujo CEST (cambio de estado HH)

**Este es el flujo principal del bug** (origen historico, comentario
`'#AT 20220110` linea 1373). 6 puntos a tocar:

| Linea (rama 2028) | Contexto | Accion |
|:-:|---|---|
| 690-696 | `If EsPicking Then bePickingUbicExistenteDañado.Dañado_picking = True` | aplicar bloque G.2.1 |
| 719-725 | mismo patron, dentro de un If anidado | aplicar bloque G.2.1 |
| 760-767 | mismo, sobre `bePickingUbicNuevo` (caso linea nueva) | aplicar bloque G.2.1 con `IdUbicacion_origen = bePickingUbicNuevo.IdUbicacion` |
| 1032-1040 | igual a 690 pero en el segundo metodo del archivo | aplicar bloque G.2.1 |
| 1059-1067 | igual a 719 en segundo metodo | aplicar bloque G.2.1 |
| **1373-1396** | bloque historico `'#AT 20220110`, ya tiene el `Get_IdMotivoUbicacion_Dañado_Picking` resuelto en linea 1387 | **no agregar el Get_, ya esta. Solo agregar `clsLnTrans_movimientos.Insert(...)` despues de la asignacion del flag** |

**Diff esperado para el bloque 1373-1396 (el mas critico)**:

```diff
  '#AT 20220110 Se cambio del valor a true
  bePickingUbic.Dañado_picking = True
  bePickingUbic.Cantidad_Solicitada = CantDañada
  ...
  ' Encabezado de ubicacion con HH por cambio de estado
  Dim pIdMotivoUbicacion As Integer = clsLnBodega.Get_IdMotivoUbicacion_Dañado_Picking(IdBodega, pConnection, pTransaction)
  If pIdMotivoUbicacion = 0 Then Throw New Exception("No esta definido IdMotivoUbicacion por defecto para ubicacion automatica, no puede realizarse el reemplazo")

+ ' --- FIX CP-013/CP-015: trans_movimiento compensatorio ---
+ Dim beMovCEST As New clsBeTrans_movimientos With {
+     .IdTipoTarea = 8,
+     .IdProducto = bePickingUbic.IdProducto,
+     .Lic_plate = bePickingUbic.Lic_plate,
+     .IdUbicacion_origen = bePickingUbic.IdUbicacion,
+     .IdUbicacion_destino = pIdMotivoUbicacion,
+     .Cantidad = CantDañada,
+     .IdBodega = IdBodega,
+     .User_agr = bePickingUbic.User_mod,
+     .Fec_agr = DateTime.Now,
+     .Observacion = "Auto CEST HH (FIX CP-013)"
+ }
+ clsLnTrans_movimientos.Insert(beMovCEST, pConnection, pTransaction)
+ clsLnTrans_picking_danado_log.Insert_FromUbic(bePickingUbic, "AUTO_HH_CEST", pConnection, pTransaction)
+ ' --- FIN FIX ---
```

### G.2.3 `clsLnTrans_pe_det_Partial.vb` — NO TOCAR los SELECTs

Las 3 queries con `WHERE ISNULL(trans_picking_ubic.dañado_picking, 0) = 0`
(lineas 822-823, 985, ~1000) **se quedan como estan**. Su semantica
("no contar dañados para totales del pedido") es correcta una vez que el
descuento de stock se hace en otro lado (en G.2.1 y G.2.2).

Verificar que despues del fix:
- `Total_Entregado` del pedido sigue mostrando solo lo no dañado (correcto para el cliente final).
- `Stock_Disponible` ya no incluye lo marcado como dañado (correcto para reservas futuras).

### G.2.4 `clsLnTrans_picking_ubic_Partial.vb` — refactor pendiente del 2028

En rama 2028 se ELIMINARON 4 queries del tipo `SELECT ... WHERE dañado_picking=0`
que estaban en rama 2023. **Antes de aplicar G.2.1 / G.2.2 hay que entender
por que se eliminaron** — pueden haber sido reemplazadas por una vista o
por logica nueva no descargada todavia. Cola Q-FIX-PARCIAL-2028 (C-010)
debe ayudar.

### G.2.5 Tabla nueva `trans_picking_danado_log`

DDL propuesto (agregar a `WSHHRN/Update_BD_WMS.sql`):

```sql
CREATE TABLE trans_picking_danado_log (
    Id              BIGINT IDENTITY(1,1) PRIMARY KEY,
    IdPickingUbic   BIGINT NOT NULL,
    IdProducto      INT NOT NULL,
    Lic_plate       NVARCHAR(50) NULL,
    Cantidad        DECIMAL(18,4) NOT NULL,
    Origen          NVARCHAR(50) NOT NULL, -- AUTO_BOF_STOCKRES | AUTO_HH_CEST | MANUAL_BOF | etc
    IdMovimiento_compensatorio BIGINT NULL, -- FK a trans_movimientos generado
    Motivo          NVARCHAR(200) NULL,
    Observacion     NVARCHAR(500) NULL,
    User_agr        NVARCHAR(50) NOT NULL,
    Fec_agr         DATETIME NOT NULL DEFAULT GETDATE(),
    Reconciliado_aj BIT NOT NULL DEFAULT 0, -- §B.4
    Fec_reconciliacion DATETIME NULL,
    CONSTRAINT FK_picking_danado_log_ubic FOREIGN KEY (IdPickingUbic)
        REFERENCES trans_picking_ubic(IdPickingUbic)
);
CREATE INDEX IX_picking_danado_log_producto ON trans_picking_danado_log(IdProducto, Lic_plate);
CREATE INDEX IX_picking_danado_log_reconc ON trans_picking_danado_log(Reconciliado_aj, Fec_agr);
```

Y ALTER opcional para corregir el default NULL anomalo (cola C-011):

```sql
-- Solo si la BD del cliente lo amerita y no hay mas dependencias del NULL
ALTER TABLE trans_picking_ubic ADD CONSTRAINT
    DF_trans_picking_ubic_dañado_picking DEFAULT 0 FOR dañado_picking;
UPDATE trans_picking_ubic SET dañado_picking = 0 WHERE dañado_picking IS NULL;
```

## G.3 Hilo HH (TOMHH2025) — que verificar

Cola C-007 abierta. Hipotesis: cuando el operador HH marca dañado en un
`frm_picking_*.java`, se llama a `setDanado_picking(true)` y despues
`webService.Insertar_Trans_Picking_Ubic_Activa(...)` (o similar). El BOF
recibe la llamada y termina en `clsLnTrans_picking_ubic.Update_*` o
`clsLnStock_res_Partial.Update_*`, donde se aplican los fixes de G.2.

Verificacion:
1. Bajar `TOMHH2025/app/src/main/java/com/dts/activities/frm_picking_*.java`
   y grep por `setDanado_picking`.
2. Identificar el `webService.execws` o `webService.Insertar*` que se llama
   inmediatamente despues.
3. Confirmar en `WSHHRN` que ese metodo desemboca en uno de los archivos
   parchados en G.2 (sino, hay un cuarto camino no mapeado).

**No hace falta tocar codigo del HH si el fix se hace todo del lado server**:
el HH manda `Danado_picking=true`, el BOF parchado ahora lo respeta y ademas
descuenta el stock. Caso ideal.

## G.4 Validacion del fix con casos golden (antes de mergear)

Crear los siguientes casos en MAMPA QA (BD `TOMWMS_MAMPA_QA`) o en una BD
copia de Killios PRD:

```
Caso GOLD-01: BOF, sin lic_plate
  Dado: lote LOTE-G01 / 100 UM en ubicacion PICK-G01
  Cuando: backoffice marca dañado_picking=1 con destino MERMA
  Entonces:
    - PICK-G01 queda con 70 UM (asume picking previo de 30 UM dañadas)
    - MERMA queda con +30 UM
    - trans_movimientos tiene 1 fila nueva con IdTipoTarea=8
    - trans_picking_danado_log tiene 1 fila con Origen=AUTO_BOF_STOCKRES

Caso GOLD-02: HH CEST, con lic_plate
  Dado: lote LOTE-G02 con LP=LP002 / 50 UM en ubicacion PICK-G02
  Cuando: operador HH cambia estado a "dañado picking" sobre 20 UM
  Entonces:
    - LP002 en PICK-G02 queda con 30 UM
    - trans_movimientos tiene 1 fila con IdTipoTarea=8 origen=PICK-G02 destino=MERMA
    - trans_picking_danado_log Origen=AUTO_HH_CEST

Caso GOLD-03: Bodega SIN ubicacion MERMA configurada (caso negativo)
  Dado: bodega B-NOMERMA sin Get_IdMotivoUbicacion_Dañado_Picking
  Cuando: se intenta marcar dañado_picking=1 desde BOF
  Entonces:
    - Se lanza Exception con mensaje claro
    - NO se modifica trans_picking_ubic
    - NO se inserta en trans_movimientos ni log

Caso GOLD-04: AJCANTN (descarte total)
  Dado: lote LOTE-G04 / 40 UM en PICK-G04
  Cuando: backoffice marca dañado con destino DESCARTE
  Entonces:
    - PICK-G04 queda con 0 (o no aparece)
    - trans_movimientos tiene IdTipoTarea=17 con Cantidad=-40
    - sin contraparte en MERMA

Caso GOLD-05: Reconciliacion historica
  Dado: 100 filas previas con dañado_picking=1 y trans_picking_danado_log vacio (porque son pre-fix)
  Cuando: corre el script de reconciliacion (§B)
  Entonces:
    - Se generan AJCANTN segun el delta
    - Se marca Reconciliado_aj=1 en log
    - Re-correr el script no genera nuevos AJCANTN

Caso GOLD-06: Idempotencia
  Dado: linea ya marcada dañado_picking=1
  Cuando: se vuelve a llamar al setter desde otro flujo
  Entonces:
    - No genera segundo trans_movimientos
    - El log NO duplica filas
    (validar con UNIQUE KEY o check al Insert)
```

## G.5 Roll-out gradual sugerido

| Semana | Cliente | BD | Rama destino | Validacion |
|:-:|---|---|---|---|
| 1 | MAMPA | TOMWMS_MAMPA_QA | dev_2028_merge | golden 1-6 |
| 2 | MAMPA | TOMWMS_MAMPA_PRD | dev_2028_merge | smoke real con operacion |
| 3 | Killios | clon de PRD a QA_FIX | cherry-pick a `dev_2023_estable_hotfix_danado` | golden 1-6 + replay de 100 casos historicos |
| 4 | Killios | TOMWMS_KILLIOS_PRD | hotfix 2023 | con ventana de mantenimiento + script reconciliacion §B |
| 5+ | BYB, MERCOPAN | sus PRDs | hotfix 2023 si ya cerro Killios | mismo proceso |

---

# H. Estrategia de ramas — por que 2028 primero (no 2023)

## H.1 Mi recomendacion previa estaba equivocada

En el `traza-002-danado-picking.md` §10 y en el INDEX del CP-015 escribi
inicialmente "aplicar a `dev_2023_estable`, no a `dev_2028_merge`". Esa
recomendacion nacio de un razonamiento corto: "Killios productivo corre 2023,
ahi esta sangrando, ahi hay que parchar". Es valida solo como hotfix de
emergencia, no como estrategia.

## H.2 Por que `dev_2028_merge` es el target principal

1. **Es la rama que Erik esta estabilizando** (project goal: MAMPA + scan
   Wave 1-6). Todo el esfuerzo de QA, refactor, observabilidad va ahi.
2. **Ya tiene un fix parcial empezado** en `clsLnStock_res_Partial.vb`
   lineas 1998-2008 (comentado). Terminarlo es continuar trabajo
   existente, no abrir un frente paralelo.
3. **Ya elimino queries problematicas** que estaban en 2023 (`clsLnTrans_picking_ubic_Partial.vb`
   perdio 73 KB y 4 queries de `WHERE dañado_picking=0`). El refactor
   esta vivo.
4. **Cuando 2028 entre a produccion** (proximo release general), todos los
   clientes la reciben — incluido Killios. El fix se propaga "gratis".
5. **Reduce el riesgo de divergencia**: si parchamos solo 2023, despues
   hay que portear el patch a 2028 sin perder nada. Si parchamos 2028 y
   despues hacemos cherry-pick a 2023, el conflicto vive en un solo lugar.

## H.3 Cuando si conviene tocar `dev_2023_estable`

**Solo como hotfix de emergencia, despues de validar en 2028.** Criterios
para que justifique el hotfix:

- Killios reporta perdida economica concreta atribuible al bug en proximas
  4-8 semanas (el horizonte normal para que 2028 llegue a su PRD).
- El fix en 2028 ya paso golden 1-6 + smoke en MAMPA PRD.
- Hay ventana de mantenimiento disponible en Killios.

En ese caso: cherry-pick del commit del fix de 2028 a una rama
`dev_2023_estable_hotfix_danado`, validacion completa en QA, despues
deploy. **No mergear a 2023 features adicionales** — solo el patch
puntual.

## H.4 Resumen de decision

```
┌─────────────────────────────────────────────────────────────────┐
│ Target principal:  dev_2028_merge   (terminar fix parcial)      │
│ Target secundario: dev_2023_estable_hotfix_danado (cherry-pick) │
│                                                                 │
│ NO target:         dev_2023_estable directo                     │
│                    (riesgo de divergencia y deuda doble)        │
└─────────────────────────────────────────────────────────────────┘
```

## H.5 Implicancia para el WMSWebAPI .NET 10 nuevo

El servicio nuevo (que reemplaza a WSHHRN) **debe nacer con el invariante
ya implementado**: si un endpoint REST pone `dañado_picking=true`, el
service layer del WMSWebAPI tiene que generar el `trans_movimientos`
compensatorio por defecto. No puede heredar el bug por copia ciega del
codigo de TOMWMS_BOF. Documentar esto como ADR cuando arranque ese
trabajo (probable ADR-006 o posterior).
