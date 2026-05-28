---
tipo: other
ramas_afectadas: [dev_2028_merge]
autores: [erik]
---
# Ajuste de Stock — Export Excel limpio (v2 sobre base 2026-05-13)

## Contexto

La v1 (commits `c59ae5b6` y `f2cb44d3` del 12-may) quedó **obsoleta** porque
el 13-may a las 23:06 UTC entró el commit Azure DevOps `5c81c536be65`
("campo IdRecepcionEnc en ajuste_det") que tocó tanto `frmAjusteStock.vb`
como `frmAjusteStock.Designer.vb`, cambiando la base de 7398 → 7407 líneas
en el .vb y de 1500 → 1502 líneas en el Designer.

Adicionalmente, en ese mismo commit Erik **agregó él mismo el botón nuevo
`mnuExportar`** (Caption "Exportar", Id=20, con SVG ya cargado, MaxItemId=21
correctamente actualizado) y dejó el handler `mnuExportar_ItemClick` **vacío**
con la intención explícita de que esta v2 inyecte ahí la lógica de export
Excel limpio que el agent armó en la v1.

Por esa razón, **esta v2 ya NO necesita modificar `frmAjusteStock.Designer.vb`**.
Solo se reemplaza `frmAjusteStock.vb`.

## Cambios respecto a la base actual de Azure DevOps

Base: `dev_2028_merge` commit `5c81c536be65` (2026-05-13 23:06 UTC).
Archivo base: 7407 L / sha256 `b877a98a24adffa49d5f24c09aa96b7afb87ff840fc4dc6919a4a5fd7edb63b9`.

### En `frmAjusteStock.vb`

1. **Imports nuevos** (después de `Imports TOMWMS.wsTOMHH`):
   - `Imports System.IO`
   - `Imports System.Text`
   - `Imports System.Globalization`

2. **Handler vacío reemplazado**:
   - Antes: `Private Sub mnuExportar_ItemClick(...) Handles mnuExportar.ItemClick` con cuerpo vacío.
   - Después: implementación completa que valida grilla, construye DataTable
     limpio, abre `SaveFileDialog` (.xlsx default / .csv) y delega al exporter.
   - El log de error referencia el nombre real del handler (`mnuExportar_ItemClick`).

3. **Region nueva `#Region "Exportar Excel limpio"`** insertada antes del último
   `End Class`. Contiene 8 helpers privados:

   | Helper | Propósito |
   |---|---|
   | `Construir_DataTable_Limpio_AjusteStock(dGrid)` | Arma DataTable "AjusteStockDet" de 20 columnas tipadas. Calcula `Diferencia = Cant.Física − Cant.Sistema`. |
   | `SafeStr_AS(row, colName)` | Lectura defensiva de `cell.Value` (guards: existencia col, Nothing, DBNull). |
   | `SafeFmt_AS(row, colName)` | Devuelve `cell.FormattedValue` (texto del combo, no Id). Para Bodega/Talla/Color/TipoAjuste/MotivoAjuste. |
   | `SafeDec_AS(row, colName)` | Parsea con `NumberStyles.Any` probando `CurrentCulture` y `InvariantCulture`. |
   | `SafeLong_AS(row, colName)` | `Long.TryParse` con fallback `0L`. |
   | `SafeBool_AS(row, colName)` | Acepta Boolean nativo o strings `"1"/"true"/"verdadero"/"si"/"sí"`. |
   | `Exportar_DataTable_A_Xlsx(dt, filePath)` | XLSX nativo via `GridControl` in-memory de DevExpress (mismo patrón que `frmPicking_List.Exportar_Grid_A_Excel`). Sin estilos. |
   | `Exportar_DataTable_A_Csv(dt, filePath)` | UTF-8 BOM, separador `;`, decimales con `"0.####"` cultura local, booleans `"Si"/"No"`. |
   | `EscaparCsv_AS(s, sep)` | Helper de #8: comillas dobles si contiene separador, comillas, CR o LF. Escapa `"` → `""`. |

   Sufijo `_AS` (Ajuste Stock) en los helpers para garantizar que no colisionen
   con `SafeStr/SafeFmt/etc.` que pudieran existir en otros forms o módulos.

### En `frmAjusteStock.Designer.vb`

**Sin cambios.** El botón `mnuExportar` ya está declarado, instanciado, agregado
al `RibbonControl.Items.AddRange` y al `RibbonPageGroup1.ItemLinks` por el
commit `5c81c536be65`. `MaxItemId = 21` ya cubre el Id=20 del botón.

## Resultado

Archivo final: 7688 L / 345,018 bytes / sha256 `401ab6b1b2dc12e84cd3ebd9a80f1ddf16e4a014dc4c1bb4bc7fc1d2c999c93e`.

## Cómo bajarlo

```bash
curl -L -o TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb \
  "https://raw.githubusercontent.com/ejcalderongt/tomwms-replit-client-automate/wms-brain/wms-brain/brain/code-changes/AJUSTE-STOCK-EXPORT-EXCEL/v2-2026-05-13/full-files/frmAjusteStock.vb"
```

Verificar:

```bash
sha256sum TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/frmAjusteStock.vb
# debe dar: 401ab6b1b2dc12e84cd3ebd9a80f1ddf16e4a014dc4c1bb4bc7fc1d2c999c93e
```

> **Nota:** si tu carpeta `TOMIMSV4/TOMIMSV4/Transacciones/Ajustes/` no existe
> aún en el repo local donde estás corriendo curl (caso reportado por EJC en el
> chat el 13-may), curl falla con `Failed to open the file`. Asegurate de
> ejecutar el curl **desde la raíz del clone de TOMWMS_BOF**, no desde una
> carpeta auxiliar.
