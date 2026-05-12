# Cambio: frmAjusteStock — Exportar Excel limpio

**Fecha**: 2026-05-09
**Autor**: Erik Jose Calderon (#EJCRP)
**Repo**: TOMWMS_BOF rama `dev_2028_merge`
**Form**: `TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock`
**Estado**: aplicado en clone local `/tmp/wms-deep-dive/repos/TOMWMS_BOF`,
NO commiteado a Azure DevOps (regla EJC: no push automatico sin permiso).

## Que hace

Agrega un boton "Exportar Excel" al ribbon del formulario de ajuste de
stock, al lado del boton "Importar". Al cliquear, abre un SaveFileDialog
con dos opciones:

- **Excel (.xlsx)** — formato nativo, sin estilos decorativos. Solo
  encabezados + datos. Generado via `DevExpress.XtraGrid.GridControl`
  in-memory + `ExportToXlsx()` (mismo patron usado en
  `frmPicking_List.Exportar_Grid_A_Excel`).
- **CSV UTF-8 (.csv)** — separador `;`, con BOM, escapeo de comillas y
  saltos de linea. Compatible Excel ES nativo.

Nombre por defecto: `AjusteStock_<IdAjusteEnc>_<yyyyMMdd_HHmm>`.

## Columnas exportadas (20)

Orden pensado para reporte limpio, no replica el orden visual del
DataGridView (que tiene combos y columna imagen de por medio).

| # | Columna | Origen DataGridViewColumn | Tipo | Notas |
|--:|---|---|---|---|
| 1 | Codigo Producto | ColCodigoProducto | String | |
| 2 | Nombre Producto | colNombreProducto | String | |
| 3 | Bodega | ColBodega | String | FormattedValue (combo) |
| 4 | Ubicacion | colUbicacion | String | |
| 5 | Lic Plate | ColLicPlate | String | |
| 6 | Lote | colLote | String | |
| 7 | Lote Original | LoteOrig | String | |
| 8 | Talla | colTalla | String | FormattedValue (combo) |
| 9 | Color | colColor | String | FormattedValue (combo) |
| 10 | Presentacion | colPresentacion | String | |
| 11 | UM Base | UmBas | String | |
| 12 | Tipo Ajuste | tipoajuste | String | FormattedValue (combo) |
| 13 | Motivo Ajuste | motivoajuste | String | FormattedValue (combo) |
| 14 | Cant. Sistema | CantidadP | Decimal | |
| 15 | Cant. Fisica | ColCantidad | Decimal | |
| 16 | Diferencia | calculado | Decimal | Cant.Fisica - Cant.Sistema |
| 17 | Proveedor | colProveedor | String | |
| 18 | Observacion | ColObservacion | String | |
| 19 | Enviado a ERP | ColEnviadoAErp | Boolean | |
| 20 | Id Ajuste Det | ColIdAjusteDEt | Long | trazabilidad |

## Columnas omitidas a proposito

- `ColDiferencia` — es `DataGridViewImageColumn` (icono visual), no
  tiene valor util. La diferencia numerica va en columna #16 calculada.
- `colIdProductoTallaColor` — id interno sin valor para usuario.

## Archivos modificados

| Archivo | Cambios | Notas |
|---|---|---|
| `frmAjusteStock.Designer.vb` | +13 / -2 lineas | declaracion del boton, addRange items, bloque config, ItemLink al RibbonPageGroup1, MaxItemId 20→21, friend WithEvents |
| `frmAjusteStock.vb` | +285 lineas | 4 imports nuevos (`System.IO`, `System.Text`, `System.Globalization` — `System.Reflection` ya estaba), region completo "Exportar Excel limpio" antes de `End Class` |

## Como probar

1. Compilar la solucion `TOMWMS.sln` (proyecto WMS).
2. Abrir cualquier Ajuste de Stock con detalle cargado (modo Editar) o
   con filas en pantalla (modo Nuevo).
3. Click en boton "Exportar Excel" del ribbon.
4. Elegir destino y formato. Validar:
   - .xlsx abre limpio en Excel, sin colores ni estilos
   - encabezados en fila 1, datos desde fila 2
   - combos (Tipo Ajuste, Motivo Ajuste, Bodega, Talla, Color) muestran
     el TEXTO no el ID
   - Cant. Sistema / Cant. Fisica / Diferencia son numericas (alineadas
     a la derecha y suma/resta funciona)
   - Enviado a ERP es Boolean (TRUE/FALSE en xlsx, "Si"/"No" en csv)

## Como aplicar el cambio sobre tu clone limpio

```bash
cd /tu/clone/TOMWMS_BOF
git checkout dev_2028_merge
git apply --check wms-brain/brain/code-changes/AJUSTE-STOCK-EXPORT-EXCEL/frmAjusteStock-export-excel-limpio.patch
git apply wms-brain/brain/code-changes/AJUSTE-STOCK-EXPORT-EXCEL/frmAjusteStock-export-excel-limpio.patch
```

## Reglas vinculantes respetadas

- (#1) NO migracion XML→JSON forzada, no aplica.
- (#2) Patron JSON Forma A, no aplica (es UI puro).
- (#3) `ñ` preservada (no se toca `WebService.java`).
- (#4) **NO commit/push automatico a Azure DevOps**. El patch queda en
  wms-brain como referencia. Erik decide si commitear.
- (#5) **NO mezclar HH y backend**. Solo TOMIMSV4 (BOF/.NET 4.8).

## Notas tecnicas

- **Por que GridControl in-memory y no Interop.Excel**: la solucion ya
  referencia `DevExpress.XtraGrid.v24.2`, por lo que el costo es cero.
  Interop.Excel arrastra dependencia COM y bloqueos de licencia Office
  en el cliente. ExportToXlsx genera el xlsx via OpenXML internamente.
- **Por que duplicar helpers `_AS`**: los nombres `SafeStr`, `SafeFmt`,
  etc. son comunes y posiblemente colisionan con codigo legacy en otros
  forms o modulos. El sufijo `_AS` (Ajuste Stock) garantiza no romper
  compilacion.
- **CultureInfo.CurrentCulture vs Invariant**: las celdas del DataGrid
  vienen formateadas segun cultura del usuario (es-GT). El parser
  intenta CurrentCulture primero, fallback Invariant.
- **NumberStyles.Any**: tolera espacios, signos, separadores de miles.
- **DocumentFormat.OpenXml** (referencia 5689 del vbproj) NO se usa
  directamente — DevExpress lo usa internamente.

## Plan de release sugerido

- Verificar compilacion local.
- Probar con ajuste pequeno (5-10 lineas) y luego con uno grande
  (200+ lineas, paginacion del DataGridView).
- Si OK, commit a `dev_2028_merge`:
  ```
  #EJCRP feat(TOMIMSV4/Ajustes): exportar Excel limpio en frmAjusteStock
  ```
- Mencionar en release notes 8.4.3+ como mejora UX para soporte y
  conciliacion contra SAP (especialmente util para casos como CP-014).
