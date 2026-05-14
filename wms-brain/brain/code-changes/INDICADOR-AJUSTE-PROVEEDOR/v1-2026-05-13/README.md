# frmIndicadorAjusteProveedor — Indicadores de ajustes por proveedor / contenedor

Forma nueva pedida por La Cumbre. Cruza ajustes de stock con la informacion de
proveedor / contenedor / orden de compra que entra por la cadena
`ajuste_det.IdRecepcionEnc → trans_re_enc / trans_re_oc → trans_oc_enc.IdProveedorBodega →
proveedor_bodega → proveedor`.

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

## Supuestos sobre el esquema

`ajuste_enc / ajuste_det` **no existen** en `TOMWMS_KILLIOS_PRD`, por lo que no
pude validar contra la BD productiva sus columnas exactas. Asumi nombres
estandar TOMWMS:

```sql
ajuste_enc(IdAjusteEnc, IdEmpresa, IdBodega, fec_agr, user_agr,
           estado, observacion)
ajuste_det(IdAjusteDet, IdAjusteEnc, IdProducto, codigo_producto,
           IdPresentacion, lote, fecha_vence,
           cantidad_original, cantidad_nueva,
           IdMotivoAjuste, IdRecepcionEnc, observacion)
```

Si en La Cumbre los nombres difieren (ej. `cantidad_real` en vez de
`cantidad_nueva`, o `IdPropietario` en `ajuste_enc`), ajustar **solo**
`SQL_MAESTRA` (la unica seccion que toca la BD). Toda la logica de
agregados / KPIs / charts trabaja sobre los nombres de columnas
**del SELECT final** (ej. `CantidadDiferencia`, `NombreProveedor`,
`NoContenedor`), que son alias y se mantienen estables.

Tablas validadas contra KILLIOS PRD (sus joins y columnas estan confirmados):
`trans_re_enc.no_contenedor`, `trans_re_oc.IdRecepcionEnc / IdOrdenCompraEnc`,
`trans_oc_enc.IdProveedorBodega`, `proveedor_bodega.IdAsignacion / IdProveedor`,
`proveedor.IdProveedor / nombre / codigo`, `producto.codigo / IdTipoProducto`,
`producto_tipo.IdTipoProducto / NombreTipoProducto`.

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
