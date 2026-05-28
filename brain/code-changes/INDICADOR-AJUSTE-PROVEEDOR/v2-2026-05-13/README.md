---
tipo: other
clientes: [killios]
---
# frmIndicadorAjusteProveedor — Indicadores de ajustes por proveedor / contenedor

> **v2 (2026-05-13)** — Esquema VALIDADO contra `TOMWMS_KILLIOS_PRD` (825 enc /
> 2113 det). Tablas correctas: `trans_ajuste_enc` / `trans_ajuste_det` (con
> prefijo `trans_`, igual que `trans_re_*`, `trans_oc_*`, `trans_picking_*`).
> El puente a proveedor se hace por `lic_plate` (cross-cliente, NO requiere la
> columna `IdRecepcionEnc` que se esta migrando en La Cumbre). Ver seccion
> "Esquema validado" mas abajo.

Forma nueva pedida por La Cumbre. Cruza ajustes de stock con la informacion de
proveedor / contenedor / orden de compra que entra por la cadena
`trans_ajuste_det.lic_plate → trans_re_det.lic_plate → trans_re_det.IdOrdenCompraEnc →
trans_oc_enc.IdProveedorBodega → proveedor_bodega → proveedor`.

## Archivos

| Archivo | Lineas | Proposito |
|---|---|---|
| `frmIndicadorAjusteProveedor.Designer.vb` | ~330 | Chasis: Ribbon + Panel filtros + Panel KPI + XtraTabControl con 5 pages vacias. |
| `frmIndicadorAjusteProveedor.vb` | ~720 | Logica completa: query maestra, agregaciones, KPIs, charts, export. |

## Donde colocarlos

```
TOMIMSV4/TOMIMSV4/Reportes/Ajustes/
  ├── frmIndicadorAjusteProveedor.vb
  └── frmIndicadorAjusteProveedor.Designer.vb
```

(Mismo folder que `frmReporteAjustesDet.vb`, su forma vecina y template directo.)

## Decisiones de diseño

### 1. Chasis Designer + BuildUI dinamico
El Designer queda corto a proposito (solo el esqueleto: ribbon, panel filtros y
las 5 tab pages vacias). Todo el contenido de cada pestana (KPIs, GridControls,
ChartControls) se crea por codigo en `BuildUI()` del .vb. Beneficios:

- Designer manejable (~330L vs ~1500L si todo estuviera cableado).
- Cambiar la cantidad o disposicion de KPIs / charts es solo editar VB.
- Si se quiere "promover" un control a Designer para edicion visual, basta
  con cortarlo de `BuildUI()` y declararlo en el Designer como friend field.

### 2. Una sola query maestra + agregados en memoria
`SQL_MAESTRA` trae todo el detalle linea-por-linea con todos los joins. Despues,
en VB, se construyen **5 DataTables agregados** (Por Proveedor, Por Tipo
Producto, Tendencia Diaria, Por Lote, Por Vencimiento) usando LINQ to DataTable.
Beneficio: el filtrado / busqueda en cualquier grid es instantaneo sin volver a
golpear la BD.

### 3. Conexion via `ConfigurationManager.AppSettings("CST")`
Mismo patron que `clsLnLog_error_wms.Insertar` (visto en linea 38 del archivo).
Si La Cumbre usa otra clave en App.config (ej. `CADENA_TOMWMS`), reemplazar la
constante en `Ejecutar_Query_Maestra()`.

### 4. Migracion opcional a la DAL
Si quieren mover la query a la DAL, basta extraer `SQL_MAESTRA` y crear:

```vb
Public Class clsLnIndicador_ajuste_proveedor
    Public Shared Function Get_Indicadores(FechaDel, FechaAl, IdBodega,
                                           IdProveedor, IdTipoProducto,
                                           NoContenedor) As DataTable
        ' ejecuta SQL_MAESTRA
    End Function
End Class
```

…y reemplazar `Ejecutar_Query_Maestra()` por la llamada al DAL. El resto de la
forma (BuildUI + agregados + charts) queda intacto.

## Esquema validado (KILLIOS PRD 2026-05-13)

`trans_ajuste_enc` / `trans_ajuste_det` SI existen en `TOMWMS_KILLIOS_PRD` con
**825 enc** y **2113 det**. Esquema confirmado via `sys.columns`:

```sql
trans_ajuste_enc(
  idajusteenc       int PK,
  fecha             date,            -- fecha logica (sin hora)
  idusuario         int,
  referencia        nvarchar,        -- texto libre
  fec_agr           datetime,        -- timestamp creacion
  user_agr          nvarchar,
  fec_mod, user_mod,
  idbodega          int,
  Enviado_A_ERP     bit,
  IdProductoFamilia int,
  IdPropietarioBodega int,
  ajuste_por_inventario int,
  IdCentroCosto     int,
  auditado          bit
)
-- NOTA: NO tiene observacion, ni IdEmpresa, ni estado.
--       La "observacion" del header se guarda en 'referencia'.

trans_ajuste_det(
  idajustedet        int PK,
  idajusteenc        int FK,
  IdStock            int,
  IdPropietarioBodega int,
  IdProductoBodega   int,            -- pivote: producto_bodega.IdProducto
  IdProductoEstado   int,
  IdPresentacion     int,
  IdUnidadMedida     int,
  IdUbicacion        int,
  lote_original      nvarchar,       -- DOS lotes: ajuste puede cambiar lote
  lote_nuevo         nvarchar,
  fecha_vence_original datetime,     -- DOS fechas: idem cambio de vencimiento
  fecha_vence_nueva    datetime,
  peso_original      float,          -- DOS pesos: ajuste por peso
  peso_nuevo         float,
  cantidad_original  float,          -- DOS cantidades: ajuste por cantidad
  cantidad_nueva     float,
  codigo_producto    nvarchar,       -- denormalizado
  nombre_producto    nvarchar,       -- denormalizado
  idtipoajuste       int,            -- ajuste_tipo (lote / vencimiento / cant / peso)
  idmotivoajuste     int,            -- ajuste_motivo
  observacion        nvarchar,
  codigo_ajuste      nvarchar,
  enviado            bit,
  IdBodegaERP        int,
  lic_plate          nvarchar,       -- PUENTE A RECEPCION
  referencia_ajuste_erp nvarchar,
  estado_ajuste_erp  bit
)
-- IMPORTANTE: KILLIOS NO tiene IdRecepcionEnc directo en det.
-- En La Cumbre la migracion en curso (commit 5c81c536) agregaria esa columna,
-- pero esta forma NO la usa: infiere la recepcion via lic_plate, asi corre
-- igual en KILLIOS, La Cumbre, Becofarma, Cealsa, Mampa, etc.
```

### Puente a proveedor — validado

```
trans_ajuste_det.lic_plate
    -> trans_re_det.lic_plate (OUTER APPLY TOP 1 por IdRecepcionDet DESC)
    -> trans_re_det.IdOrdenCompraEnc
    -> trans_oc_enc.IdProveedorBodega
    -> proveedor_bodega.IdAsignacion / IdProveedor
    -> proveedor
```

Validado en KILLIOS:

```
idajustedet=2111, lic_plate='FU00087', codigo='WMS87'
   -> trans_re_det match: IdRecepcionEnc=51, codigo='WMS87'  OK
```

**Cobertura `lic_plate` en KILLIOS:** 533/2113 = **25%** (el resto son ajustes
por inventario / carga inicial sin licencia origen). En La Cumbre se espera
mayor cobertura por flujo de recepcion mas estricto. Las lineas sin `lic_plate`
aparecen igual en el indicador, pero con Proveedor / Contenedor en blanco — son
visibles en pestana "Por Tipo Producto" / "Por Lote" que no requieren proveedor.

### Filtro implicito sobre `idtipoajuste`

`ajuste_tipo` tiene flags `modifica_lote / modifica_vencimiento /
modifica_cantidad / modifica_peso`. La forma incluye SOLO los tipos donde
`modifica_cantidad=1 OR modifica_peso=1`, ya que es un indicador de **cantidades
ajustadas**. Los ajustes puros de lote o vencimiento (sin cambio de cant/peso)
generan delta=0 y solo agregan ruido. Si se quiere incluirlos despues, basta
quitar la clausula `AND (at_f.modifica_cantidad = 1 OR at_f.modifica_peso = 1)`
del CTE `AjusteBase`.

### Joins externos (sin cambios desde v1)

Tablas confirmadas en KILLIOS PRD: `trans_re_enc.no_contenedor`,
`trans_re_det.lic_plate / IdRecepcionEnc / IdOrdenCompraEnc`,
`trans_oc_enc.IdProveedorBodega`, `proveedor_bodega.IdAsignacion / IdProveedor`,
`proveedor.IdProveedor / nombre / codigo`, `producto_bodega.IdProductoBodega /
IdProducto`, `producto.IdProducto / nombre / IdTipoProducto`,
`producto_tipo.IdTipoProducto / NombreTipoProducto`,
`ajuste_motivo.idmotivoajuste / nombre`, `ajuste_tipo.idtipoajuste / nombre /
modifica_cantidad / modifica_peso`.

### Cambios v1 -> v2

- Tablas `ajuste_enc / ajuste_det` -> `trans_ajuste_enc / trans_ajuste_det`.
- PK lowercase (`idajusteenc`, `idajustedet`).
- `IdBodega` -> `idbodega` en enc.
- `observacion` (enc) no existe -> usar `referencia`.
- `lote / fecha_vence / cantidad / peso` -> versiones `_original` y `_nuevo`,
  con `LoteEfectivo` y `FechaVenceEfectiva` calculados via `COALESCE`.
- Producto resuelto via `IdProductoBodega -> producto_bodega -> producto` (en
  vez de `producto.codigo = codigo_producto`, porque el join por PK es mas
  robusto y no depende de unicidad).
- `IdRecepcionEnc` se infiere via `OUTER APPLY` por `lic_plate` (no se asume
  columna directa que solo La Cumbre tendria).
- Se agregan `NombreTipoAjuste` y `NombreMotivoAjuste` al SELECT.
- Filtro implicito por `modifica_cantidad / modifica_peso`.

Toda la logica de agregados / KPIs / charts queda intacta — los alias del
SELECT final (`CantidadDiferencia`, `NombreProveedor`, `NoContenedor`, etc.)
son los mismos que en v1.

## Funcionalidad

### Filtros (panel izquierdo)
- Rango de fechas (`Desde / Hasta`, default ultimos 30 dias)
- Bodega (LookUpEdit poblada via `AP.Listar_Bodegas_By_Usuario`, default = `AP.IdBodega`)
- Proveedor (LookUpEdit poblada con los proveedores presentes en el dataset, despues de la primera carga)
- Tipo de producto (idem)
- No. Contenedor (TextEdit con LIKE `%texto%`)
- Botones `Aplicar` / `Limpiar`

### KPI tiles (top, 6 tarjetas)
1. Total Lineas
2. Cantidad Neta (verde si ≥0, rojo si <0)
3. Positivos (cantidad de lineas + suma)
4. Negativos (cantidad de lineas + suma)
5. Proveedores distintos
6. Contenedores distintos

### Pestañas

| Pestaña | Contenido |
|---|---|
| **Por Proveedor / Contenedor** | Grid agrupable: Proveedor, NoContenedor, Lineas, Positivos, Negativos, CantPos, CantNeg, CantNeta, %Pos, %Neg, Tendencia (Sobrante/Faltante/Mixto). Coloreado por fila segun tendencia. |
| **Por Tipo Producto** | Grafico de barras (Pos vs Neg por tipo) + grid resumen (Lineas, Productos distintos, Proveedores distintos, CantNeta). |
| **Tendencia Diaria** | Grafico de linea (CantNeta + Pos + Neg por dia) + grid detalle. |
| **Por Lote** | Grid: Proveedor, CodigoProducto, Producto, Lote, FechaVence, Lineas, CantPos, CantNeg, CantNeta, NoContenedor. Ordenado por mas afectados. |
| **Por Vencimiento** | Grid: Proveedor, CodigoProducto, Producto, Lote, FechaVence, DiasParaVencer, Lineas, CantNeta, Estado (Vencido / Critico ≤30d / Alerta ≤90d / Normal). Coloreado semaforico. |

### Exportacion
Boton `Exportar Excel` exporta el grid del **tab activo** a:
- `.xlsx` nativo via `GridControl.ExportToXlsx` (recomendado).
- `.csv` UTF-8 BOM con separador `;` y decimales en cultura local (fallback compatible Excel ES).

Boton `Imprimir` abre el preview de DevExpress del grid activo.

## Dependencias DevExpress
- `DevExpress.XtraBars` (RibbonControl, BarButtonItem, BarStaticItem)
- `DevExpress.XtraEditors` (PanelControl, GroupControl, LookUpEdit, DateEdit, TextEdit, SimpleButton, LabelControl, SplitContainerControl, XtraMessageBox)
- `DevExpress.XtraGrid` (GridControl, GridView, RowStyleEventArgs)
- `DevExpress.XtraTab` (XtraTabControl, XtraTabPage)
- `DevExpress.XtraCharts` (ChartControl, Series, ViewType)
- `DevExpress.XtraPrinting` (preview)

Todas estas estan ya referenciadas en el proyecto TOMIMSV4 (ver `frmReporteAjustesDet.vb` y otras formas de Reportes).

## Que falta / siguientes pasos

1. **Validar nombres de columnas en `ajuste_enc / ajuste_det`** contra el esquema productivo de La Cumbre. Es lo unico bloqueante.
2. **Resx**: el proyecto suele incluir un `.resx` por forma; aqui no lo genere porque el Designer no usa `resources.GetObject` (no hay imagenes/SVG embebidos). Visual Studio creara uno vacio al primer build sin problema.
3. **Iconos del ribbon**: los 5 BarButtonItem del ribbon no llevan SVG. Erik puede asignar iconos desde el Designer (mismos que usa `frmReporteAjustesDet.cmdImprimir / cmdExToExcel / cmdSalir`).
4. **Layout grid persistente**: omitido a proposito (vendria via `clsLnConfiguracion_usuario_enc.Guardar_Layout / Get_Layout`). Se puede sumar despues con el mismo patron de `frmReporteAjustesDet.vb` lineas 241-301.
5. **Drill-down**: opcional, doble-click en una fila del grid Proveedor podria abrir un sub-form con las lineas detalle de ese proveedor / contenedor. No esta implementado pero el `DT_Maestro` ya tiene toda la info necesaria.

## Como bajarlos

```bash
# Desde la raiz del clone de TOMWMS_BOF
curl -L --create-dirs \
  -o TOMIMSV4/TOMIMSV4/Reportes/Ajustes/frmIndicadorAjusteProveedor.vb \
  "https://raw.githubusercontent.com/ejcalderongt/tomwms-replit-client-automate/wms-brain/wms-brain/brain/code-changes/INDICADOR-AJUSTE-PROVEEDOR/v1-2026-05-13/full-files/frmIndicadorAjusteProveedor.vb"

curl -L --create-dirs \
  -o TOMIMSV4/TOMIMSV4/Reportes/Ajustes/frmIndicadorAjusteProveedor.Designer.vb \
  "https://raw.githubusercontent.com/ejcalderongt/tomwms-replit-client-automate/wms-brain/wms-brain/brain/code-changes/INDICADOR-AJUSTE-PROVEEDOR/v1-2026-05-13/full-files/frmIndicadorAjusteProveedor.Designer.vb"

# Verificar checksums
sha256sum -c <(curl -s "https://raw.githubusercontent.com/ejcalderongt/tomwms-replit-client-automate/wms-brain/wms-brain/brain/code-changes/INDICADOR-AJUSTE-PROVEEDOR/v1-2026-05-13/SHA256SUMS.txt")
```

Despues, desde Visual Studio: `Add → Existing Item…` ambos archivos al proyecto
TOMIMSV4. El Designer va a quedar como partial class de la forma .vb (la
relacion la maneja MSBuild solo).
