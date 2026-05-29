Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports DevExpress.Utils
Imports DevExpress.XtraBars
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

' =============================================================================
' frmIndicadorAjusteProveedor
' -----------------------------------------------------------------------------
' Indicadores de ajustes de stock cruzados con la informacion del proveedor
' obtenida via la cadena: ajuste_det.IdRecepcionEnc → trans_re_enc /
' trans_re_oc → trans_oc_enc.IdProveedorBodega → proveedor_bodega →
' proveedor.
'
' Aporta:
'   - 6 KPI tiles (totales, positivos, negativos, proveedores, contenedores,
'     puntaje neto).
'   - Tab "Por Proveedor / Contenedor": grid agrupable.
'   - Tab "Por Tipo Producto": grafico de barras + grid resumen.
'   - Tab "Tendencia Diaria": grafico de linea cantidad neta por dia.
'   - Tab "Por Lote": grid lote x proveedor con foco en cantidades ajustadas.
'   - Tab "Por Vencimiento": grid con dias para vencer y coloreado semaforico.
'   - Exportable a XLSX nativo (DevExpress) y CSV UTF-8 BOM como fallback.
'
' Patron de carga: una sola query SQL maestra trae todo el detalle, y los
' agregados se calculan en memoria (LINQ to DataTable) para que el filtrado
' interactivo sea inmediato sin recargar.
'
' Cliente original: La Cumbre. La forma es agnostica del cliente siempre que
' el esquema ajuste_enc/ajuste_det (con IdRecepcionEnc en det) este presente.
'
' Convenciones del repo respetadas:
'   - Hereda RibbonForm (igual que frmReporteAjustesDet).
'   - Logging via clsLnLog_error_wms.Agregar_Error(ex.Message).
'   - Bodegas via AP.Listar_Bodegas_By_Usuario.
'   - Conexion via Configuration.ConfigurationManager.AppSettings("CST")
'     (mismo patron que clsLnLog_error_wms.Insertar).
'   - Layout grid persistente via clsLnConfiguracion_usuario_enc.Guardar_Layout
'     / Get_Layout (omitido por simplicidad; se puede sumar si la forma estabiliza).
'
' Si el equipo decide migrar la query maestra a la DAL, basta con extraer
' SQL_MAESTRA y crear clsLnIndicador_ajuste_proveedor.Get_Indicadores(...)
' que devuelva el DataTable maestro; el resto de la forma queda igual.
' =============================================================================

Public Class frmIndicadorAjusteProveedor

    ' --- Estado ---------------------------------------------------------------
    Private DT_Maestro As DataTable
    Private DT_Proveedor As DataTable
    Private DT_TipoProducto As DataTable
    Private DT_Tendencia As DataTable
    Private DT_Lote As DataTable
    Private DT_Vencimiento As DataTable

    ' Controles dinamicos por tab (creados en BuildUI)
    Private grdProveedor As GridControl
    Private gvProveedor As GridView
    Private grdTipoProducto As GridControl
    Private gvTipoProducto As GridView
    Private chartTipoProducto As ChartControl
    Private grdLote As GridControl
    Private gvLote As GridView
    Private grdVencimiento As GridControl
    Private gvVencimiento As GridView
    Private chartTendencia As ChartControl
    Private grdTendencia As GridControl
    Private gvTendencia As GridView

    ' KPI tiles
    Private kpiTotalLineas As LabelControl
    Private kpiCantNeta As LabelControl
    Private kpiPositivos As LabelControl
    Private kpiNegativos As LabelControl
    Private kpiProveedores As LabelControl
    Private kpiContenedores As LabelControl

    Public Sub New()
        InitializeComponent()
    End Sub

    ' =========================================================================
    ' Eventos del Ribbon / Form
    ' =========================================================================

    Private Sub frmIndicadorAjusteProveedor_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            BuildUI()

            ' Filtros default: ultimos 30 dias, bodega del usuario.
            dtpFechaDel.EditValue = Date.Today.AddDays(-30)
            dtpFechaAl.EditValue = Date.Today

            Try
                AP.Listar_Bodegas_By_Usuario(cmbBodega)
                cmbBodega.EditValue = Integer.Parse(AP.IdBodega)
            Catch
                ' Tolerar entornos donde AP no este aun listo en design time.
            End Try

            Cargar()
        Catch ex As Exception
            Mostrar_Error(ex, MethodBase.GetCurrentMethod().Name)
        End Try
    End Sub

    Private Sub cmdAplicar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdAplicar.ItemClick
        Cargar()
    End Sub

    Private Sub btnAplicar_Click(sender As Object, e As EventArgs) Handles btnAplicar.Click
        Cargar()
    End Sub

    Private Sub cmdLimpiar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdLimpiar.ItemClick
        Limpiar_Filtros()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        Limpiar_Filtros()
    End Sub

    Private Sub cmdExportar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdExportar.ItemClick
        Exportar_Tab_Activo()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Tab_Activo()
    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    ' =========================================================================
    ' BuildUI: crea controles dinamicos dentro de cada tab y panel KPI.
    ' Mantener el Designer corto a proposito; aqui esta el cableado real.
    ' =========================================================================

    Private Sub BuildUI()

        ' ---- KPI tiles ------------------------------------------------------
        kpiTotalLineas = Crear_KPI("Total Lineas", "0", Color.FromArgb(48, 84, 150))
        kpiCantNeta = Crear_KPI("Cantidad Neta", "0.00", Color.FromArgb(112, 48, 160))
        kpiPositivos = Crear_KPI("Positivos", "0  (+0.00)", Color.FromArgb(56, 142, 60))
        kpiNegativos = Crear_KPI("Negativos", "0  (-0.00)", Color.FromArgb(198, 40, 40))
        kpiProveedores = Crear_KPI("Proveedores", "0", Color.FromArgb(0, 121, 107))
        kpiContenedores = Crear_KPI("Contenedores", "0", Color.FromArgb(245, 124, 0))

        Dim tiles = {kpiTotalLineas, kpiCantNeta, kpiPositivos, kpiNegativos, kpiProveedores, kpiContenedores}
        Dim margenX As Integer = 6
        Dim ancho As Integer = (PanelKPI.ClientSize.Width - margenX * (tiles.Length + 1)) \ tiles.Length
        If ancho < 140 Then ancho = 140
        For i As Integer = 0 To tiles.Length - 1
            Dim panel = New PanelControl()
            panel.BorderStyle = BorderStyles.NoBorder
            panel.Appearance.BackColor = ColorTranslator.FromHtml("#F4F6FA")
            panel.Appearance.Options.UseBackColor = True
            panel.Size = New Size(ancho, 96)
            panel.Location = New Point(margenX + i * (ancho + margenX), 7)
            panel.Anchor = AnchorStyles.Top Or AnchorStyles.Left
            panel.Tag = tiles(i)
            panel.Controls.Add(tiles(i))
            tiles(i).Dock = DockStyle.Fill
            PanelKPI.Controls.Add(panel)
        Next

        ' ---- Tab Proveedor --------------------------------------------------
        grdProveedor = New GridControl() With {.Dock = DockStyle.Fill}
        gvProveedor = New GridView() With {.GridControl = grdProveedor, .Name = "gvProveedor"}
        Configurar_GridView_Comun(gvProveedor)
        grdProveedor.MainView = gvProveedor
        tabProveedor.Controls.Add(grdProveedor)
        AddHandler gvProveedor.RowStyle, AddressOf gvProveedor_RowStyle

        ' ---- Tab Tipo Producto ----------------------------------------------
        Dim splitTipo = New SplitContainerControl() With {.Dock = DockStyle.Fill, .Horizontal = False}
        splitTipo.Panel1.Text = "Grafico"
        splitTipo.Panel2.Text = "Detalle"
        splitTipo.SplitterPosition = 320
        tabTipoProducto.Controls.Add(splitTipo)

        chartTipoProducto = New ChartControl() With {.Dock = DockStyle.Fill}
        Configurar_Chart_Vacio(chartTipoProducto, "Cantidad Neta por Tipo de Producto")
        splitTipo.Panel1.Controls.Add(chartTipoProducto)

        grdTipoProducto = New GridControl() With {.Dock = DockStyle.Fill}
        gvTipoProducto = New GridView() With {.GridControl = grdTipoProducto, .Name = "gvTipoProducto"}
        Configurar_GridView_Comun(gvTipoProducto)
        grdTipoProducto.MainView = gvTipoProducto
        splitTipo.Panel2.Controls.Add(grdTipoProducto)

        ' ---- Tab Tendencia --------------------------------------------------
        Dim splitTen = New SplitContainerControl() With {.Dock = DockStyle.Fill, .Horizontal = False}
        splitTen.Panel1.Text = "Grafico"
        splitTen.Panel2.Text = "Detalle"
        splitTen.SplitterPosition = 320
        tabTendencia.Controls.Add(splitTen)

        chartTendencia = New ChartControl() With {.Dock = DockStyle.Fill}
        Configurar_Chart_Vacio(chartTendencia, "Tendencia Diaria de Cantidad Neta Ajustada")
        splitTen.Panel1.Controls.Add(chartTendencia)

        grdTendencia = New GridControl() With {.Dock = DockStyle.Fill}
        gvTendencia = New GridView() With {.GridControl = grdTendencia, .Name = "gvTendencia"}
        Configurar_GridView_Comun(gvTendencia)
        grdTendencia.MainView = gvTendencia
        splitTen.Panel2.Controls.Add(grdTendencia)

        ' ---- Tab Lote -------------------------------------------------------
        grdLote = New GridControl() With {.Dock = DockStyle.Fill}
        gvLote = New GridView() With {.GridControl = grdLote, .Name = "gvLote"}
        Configurar_GridView_Comun(gvLote)
        grdLote.MainView = gvLote
        tabLote.Controls.Add(grdLote)

        ' ---- Tab Vencimiento ------------------------------------------------
        grdVencimiento = New GridControl() With {.Dock = DockStyle.Fill}
        gvVencimiento = New GridView() With {.GridControl = grdVencimiento, .Name = "gvVencimiento"}
        Configurar_GridView_Comun(gvVencimiento)
        grdVencimiento.MainView = gvVencimiento
        tabVencimiento.Controls.Add(grdVencimiento)
        AddHandler gvVencimiento.RowStyle, AddressOf gvVencimiento_RowStyle
    End Sub

    Private Function Crear_KPI(titulo As String, valor As String, accent As Color) As LabelControl
        Dim lbl = New LabelControl()
        lbl.AutoSizeMode = LabelAutoSizeMode.None
        lbl.AllowHtmlString = True
        lbl.Appearance.TextOptions.HAlignment = HorzAlignment.Center
        lbl.Appearance.TextOptions.VAlignment = VertAlignment.Center
        lbl.Appearance.Font = New Font("Segoe UI", 9.0!, FontStyle.Regular)
        lbl.Text = String.Format(
            "<size=8><color=gray>{0}</color></size><br><size=18><color={1}><b>{2}</b></color></size>",
            titulo,
            ColorToHtml(accent),
            valor)
        lbl.Tag = titulo
        Return lbl
    End Function

    Private Function ColorToHtml(c As Color) As String
        Return String.Format("#{0:X2}{1:X2}{2:X2}", c.R, c.G, c.B)
    End Function

    Private Sub Configurar_GridView_Comun(gv As GridView)
        gv.OptionsBehavior.Editable = False
        gv.OptionsView.ShowAutoFilterRow = True
        gv.OptionsView.ShowFooter = True
        gv.OptionsView.ColumnAutoWidth = False
        gv.OptionsFind.AlwaysVisible = True
        gv.OptionsSelection.EnableAppearanceFocusedRow = True
        gv.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
        gv.Appearance.FocusedRow.ForeColor = Color.White
        gv.OptionsCustomization.AllowGroup = True
        gv.OptionsCustomization.AllowSort = True
        gv.OptionsView.ShowGroupPanel = True
    End Sub

    Private Sub Configurar_Chart_Vacio(chart As ChartControl, titulo As String)
        chart.Titles.Clear()
        Dim t = New ChartTitle()
        t.Text = titulo
        t.Font = New Font("Segoe UI", 11.0!, FontStyle.Bold)
        t.Alignment = StringAlignment.Center
        chart.Titles.Add(t)
        chart.Legend.Visibility = DefaultBoolean.[True]
    End Sub

    ' =========================================================================
    ' Cargar: ejecuta SQL maestra y refresca todas las vistas.
    ' =========================================================================

    Private Sub Cargar()
        Try
            Cursor = Cursors.WaitCursor

            DT_Maestro = Ejecutar_Query_Maestra()

            If DT_Maestro Is Nothing OrElse DT_Maestro.Rows.Count = 0 Then
                Vaciar_Vistas()
                Refrescar_KPIs()
                Actualizar_Status_Filtros()
                lblRegs.Caption = "Registros: 0"
                XtraMessageBox.Show("No se encontraron ajustes para los filtros aplicados.",
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Construir_Vistas()

            grdProveedor.DataSource = DT_Proveedor
            grdTipoProducto.DataSource = DT_TipoProducto
            grdTendencia.DataSource = DT_Tendencia
            grdLote.DataSource = DT_Lote
            grdVencimiento.DataSource = DT_Vencimiento

            Aplicar_Formatos_Grids()
            Refrescar_Charts()
            Refrescar_KPIs()
            Refrescar_Combos_Dependientes()
            Actualizar_Status_Filtros()

            lblRegs.Caption = String.Format("Registros: {0:N0}", DT_Maestro.Rows.Count)

            gvProveedor.BestFitColumns(True)
            gvTipoProducto.BestFitColumns(True)
            gvTendencia.BestFitColumns(True)
            gvLote.BestFitColumns(True)
            gvVencimiento.BestFitColumns(True)

        Catch ex As Exception
            Mostrar_Error(ex, MethodBase.GetCurrentMethod().Name)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Vaciar_Vistas()
        For Each g In New GridControl() {grdProveedor, grdTipoProducto, grdTendencia, grdLote, grdVencimiento}
            g.DataSource = Nothing
        Next
        chartTipoProducto.Series.Clear()
        chartTendencia.Series.Clear()
    End Sub

    Private Sub Limpiar_Filtros()
        dtpFechaDel.EditValue = Date.Today.AddDays(-30)
        dtpFechaAl.EditValue = Date.Today
        cmbProveedor.EditValue = Nothing
        cmbTipoProducto.EditValue = Nothing
        txtNoContenedor.Text = ""
        Try
            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)
        Catch
        End Try
        Cargar()
    End Sub

    Private Sub Actualizar_Status_Filtros()
        Dim sb = New StringBuilder()
        sb.AppendFormat("Periodo: {0:dd/MM/yyyy} a {1:dd/MM/yyyy}", FechaDel(), FechaAl())
        If cmbBodega.EditValue IsNot Nothing AndAlso Not IsDBNull(cmbBodega.EditValue) Then
            sb.AppendFormat(" | Bodega: {0}", cmbBodega.Text)
        End If
        If cmbProveedor.EditValue IsNot Nothing AndAlso Not IsDBNull(cmbProveedor.EditValue) Then
            sb.AppendFormat(" | Proveedor: {0}", cmbProveedor.Text)
        End If
        If cmbTipoProducto.EditValue IsNot Nothing AndAlso Not IsDBNull(cmbTipoProducto.EditValue) Then
            sb.AppendFormat(" | Tipo: {0}", cmbTipoProducto.Text)
        End If
        If Not String.IsNullOrWhiteSpace(txtNoContenedor.Text) Then
            sb.AppendFormat(" | Contenedor: {0}", txtNoContenedor.Text.Trim())
        End If
        lblFiltrosActivos.Caption = sb.ToString()
    End Sub

    ' =========================================================================
    ' Helpers de filtros tipados
    ' =========================================================================

    Private Function FechaDel() As Date
        If dtpFechaDel.EditValue Is Nothing OrElse IsDBNull(dtpFechaDel.EditValue) Then Return Date.Today.AddDays(-30)
        Return CDate(dtpFechaDel.EditValue).Date
    End Function

    Private Function FechaAl() As Date
        If dtpFechaAl.EditValue Is Nothing OrElse IsDBNull(dtpFechaAl.EditValue) Then Return Date.Today
        Return CDate(dtpFechaAl.EditValue).Date.AddDays(1).AddSeconds(-1)
    End Function

    Private Function IdBodegaSel() As Integer
        If cmbBodega.EditValue Is Nothing OrElse IsDBNull(cmbBodega.EditValue) Then Return 0
        Dim r As Integer = 0
        Integer.TryParse(cmbBodega.EditValue.ToString(), r)
        Return r
    End Function

    Private Function IdProveedorSel() As Integer
        If cmbProveedor.EditValue Is Nothing OrElse IsDBNull(cmbProveedor.EditValue) Then Return 0
        Dim r As Integer = 0
        Integer.TryParse(cmbProveedor.EditValue.ToString(), r)
        Return r
    End Function

    Private Function IdTipoProductoSel() As Integer
        If cmbTipoProducto.EditValue Is Nothing OrElse IsDBNull(cmbTipoProducto.EditValue) Then Return 0
        Dim r As Integer = 0
        Integer.TryParse(cmbTipoProducto.EditValue.ToString(), r)
        Return r
    End Function

    Private Function NoContenedorSel() As String
        If txtNoContenedor.Text Is Nothing Then Return ""
        Return txtNoContenedor.Text.Trim()
    End Function

    ' =========================================================================
    ' Query maestra
    ' Devuelve detalle ajuste-linea con todos los joins de proveedor / contenedor
    ' / tipo producto / presentacion.
    '
    ' Esquema VALIDADO contra TOMWMS_KILLIOS_PRD (sa @ 52.41.114.122,1437,
    ' 2026-05-13, 825 enc / 2113 det):
    '
    '   trans_ajuste_enc(idajusteenc PK, fecha date, idusuario, referencia,
    '                    fec_agr, user_agr, fec_mod, user_mod, idbodega,
    '                    Enviado_A_ERP, IdProductoFamilia, IdPropietarioBodega,
    '                    ajuste_por_inventario, IdCentroCosto, auditado)
    '
    '   trans_ajuste_det(idajustedet PK, idajusteenc, IdStock,
    '                    IdPropietarioBodega, IdProductoBodega,
    '                    IdProductoEstado, IdPresentacion, IdUnidadMedida,
    '                    IdUbicacion, lote_original, lote_nuevo,
    '                    fecha_vence_original, fecha_vence_nueva,
    '                    peso_original, peso_nuevo, cantidad_original,
    '                    cantidad_nueva, codigo_producto, nombre_producto,
    '                    idtipoajuste, idmotivoajuste, observacion,
    '                    codigo_ajuste, enviado, IdBodegaERP, lic_plate,
    '                    referencia_ajuste_erp, estado_ajuste_erp)
    '
    ' Puente a proveedor (cross-cliente, NO requiere IdRecepcionEnc directo):
    '   trans_ajuste_det.lic_plate -> trans_re_det.lic_plate
    '   -> trans_re_det.IdOrdenCompraEnc -> trans_oc_enc.IdProveedorBodega
    '   -> proveedor_bodega.IdProveedor -> proveedor.
    '
    ' En La Cumbre, segun migracion en curso (commit 5c81c536), trans_ajuste_det
    ' tendria una columna adicional IdRecepcionEnc directo. Para mantener la
    ' forma cross-cliente NO se usa esa columna; se infiere por lic_plate, que
    ' funciona en KILLIOS y se asume tambien en La Cumbre (validado: lic_plate
    ' 'FU00087' en idajustedet 2111 enlaza correctamente a IdRecepcionEnc 51).
    ' Cobertura de match esperada: ~25% en KILLIOS (resto son ajustes legacy /
    ' inventario sin licencia), >50% esperado en La Cumbre por flujo mas
    ' estricto. Las lineas sin lic_plate aparecen igual en el indicador con
    ' Proveedor/Contenedor en blanco.
    '
    ' Filtro implicito: solo se incluyen ajustes cuyo tipo tiene
    ' modifica_cantidad=1 o modifica_peso=1 (ajustes puros de lote o vencimiento
    ' sin cambio de cantidad/peso no son relevantes para indicador de cantidades
    ' ajustadas; se pueden incluir invirtiendo el filtro si se requiere despues).
    ' =========================================================================
    Private Const SQL_MAESTRA As String = "
;WITH AjusteBase AS (
    SELECT
        ad.idajustedet                                   AS IdAjusteDet,
        ae.idajusteenc                                   AS IdAjusteEnc,
        ISNULL(ae.fec_agr, CAST(ae.fecha AS datetime))   AS FechaAjuste,
        ae.idbodega                                      AS IdBodega,
        ae.user_agr                                      AS UsuarioAjuste,
        ae.referencia                                    AS ReferenciaEnc,
        ae.Enviado_A_ERP                                 AS EnviadoERP,
        ae.IdPropietarioBodega,
        ad.IdProductoBodega,
        ad.IdProductoEstado,
        ad.IdUbicacion,
        ad.codigo_producto,
        ad.nombre_producto                               AS NombreProductoDenorm,
        ad.lote_original,
        ad.lote_nuevo,
        COALESCE(NULLIF(ad.lote_nuevo, ''), ad.lote_original) AS LoteEfectivo,
        ad.fecha_vence_original,
        ad.fecha_vence_nueva,
        COALESCE(ad.fecha_vence_nueva, ad.fecha_vence_original) AS FechaVenceEfectiva,
        ad.cantidad_original,
        ad.cantidad_nueva,
        (ISNULL(ad.cantidad_nueva, 0) - ISNULL(ad.cantidad_original, 0)) AS CantidadDiferencia,
        ad.peso_original,
        ad.peso_nuevo,
        (ISNULL(ad.peso_nuevo, 0) - ISNULL(ad.peso_original, 0)) AS PesoDiferencia,
        ad.idtipoajuste,
        ad.idmotivoajuste,
        ad.observacion,
        ad.codigo_ajuste,
        ad.lic_plate,
        ad.referencia_ajuste_erp,
        ad.estado_ajuste_erp
    FROM trans_ajuste_det ad
    INNER JOIN trans_ajuste_enc ae ON ae.idajusteenc = ad.idajusteenc
    INNER JOIN ajuste_tipo at_f    ON at_f.idtipoajuste = ad.idtipoajuste
    WHERE ISNULL(ae.fec_agr, CAST(ae.fecha AS datetime)) BETWEEN @FechaDel AND @FechaAl
      AND (@IdBodega = 0 OR ae.idbodega = @IdBodega)
      AND (at_f.modifica_cantidad = 1 OR at_f.modifica_peso = 1)
)
SELECT
    ab.IdAjusteEnc,
    ab.IdAjusteDet,
    ab.FechaAjuste,
    CONVERT(date, ab.FechaAjuste)                        AS FechaAjusteDia,
    ab.IdBodega,
    ab.UsuarioAjuste,
    ab.ReferenciaEnc                                     AS ObservacionEnc,
    ab.EnviadoERP,
    ab.codigo_producto                                   AS CodigoProducto,
    COALESCE(p.nombre, ab.NombreProductoDenorm)          AS NombreProducto,
    p.IdTipoProducto,
    pt.NombreTipoProducto,
    ab.LoteEfectivo                                      AS Lote,
    ab.lote_original                                     AS LoteOriginal,
    ab.lote_nuevo                                        AS LoteNuevo,
    ab.FechaVenceEfectiva                                AS FechaVence,
    ab.fecha_vence_original                              AS FechaVenceOriginal,
    ab.fecha_vence_nueva                                 AS FechaVenceNueva,
    ab.cantidad_original                                 AS CantidadOriginal,
    ab.cantidad_nueva                                    AS CantidadNueva,
    ab.CantidadDiferencia,
    ab.peso_original                                     AS PesoOriginal,
    ab.peso_nuevo                                        AS PesoNuevo,
    ab.PesoDiferencia,
    CASE WHEN ab.CantidadDiferencia > 0 THEN 1 ELSE 0 END AS EsPositivo,
    CASE WHEN ab.CantidadDiferencia < 0 THEN 1 ELSE 0 END AS EsNegativo,
    ab.idtipoajuste                                      AS IdTipoAjuste,
    at2.nombre                                           AS NombreTipoAjuste,
    ab.idmotivoajuste                                    AS IdMotivoAjuste,
    am.nombre                                            AS NombreMotivoAjuste,
    ab.observacion                                       AS ObservacionDet,
    ab.codigo_ajuste                                     AS CodigoAjuste,
    ab.lic_plate                                         AS LicPlate,
    ab.referencia_ajuste_erp                             AS ReferenciaAjusteERP,
    rd.IdRecepcionEnc                                    AS IdRecepcionEnc,
    re.no_contenedor                                     AS NoContenedor,
    re.fecha_recepcion                                   AS FechaRecepcion,
    re.NoGuia,
    pr.IdProveedor,
    pr.codigo                                            AS CodigoProveedor,
    pr.nombre                                            AS NombreProveedor
FROM AjusteBase ab
LEFT JOIN ajuste_tipo at2     ON at2.idtipoajuste = ab.idtipoajuste
LEFT JOIN ajuste_motivo am    ON am.idmotivoajuste = ab.idmotivoajuste
LEFT JOIN producto_bodega pb0 ON pb0.IdProductoBodega = ab.IdProductoBodega
LEFT JOIN producto p          ON p.IdProducto = pb0.IdProducto
LEFT JOIN producto_tipo pt    ON pt.IdTipoProducto = p.IdTipoProducto
OUTER APPLY (
    SELECT TOP 1 rd2.IdRecepcionEnc, rd2.IdOrdenCompraEnc
    FROM trans_re_det rd2
    WHERE rd2.lic_plate = ab.lic_plate
      AND ab.lic_plate IS NOT NULL AND ab.lic_plate NOT IN ('', '0')
    ORDER BY rd2.IdRecepcionDet DESC
) rd
LEFT JOIN trans_re_enc re     ON re.IdRecepcionEnc = rd.IdRecepcionEnc
LEFT JOIN trans_oc_enc tone   ON tone.IdOrdenCompraEnc = rd.IdOrdenCompraEnc
LEFT JOIN proveedor_bodega pb ON pb.IdAsignacion = tone.IdProveedorBodega
LEFT JOIN proveedor pr        ON pr.IdProveedor = pb.IdProveedor
WHERE (@IdProveedor = 0 OR pr.IdProveedor = @IdProveedor)
  AND (@IdTipoProducto = 0 OR p.IdTipoProducto = @IdTipoProducto)
  AND (@NoContenedor = '' OR re.no_contenedor LIKE '%' + @NoContenedor + '%')
"

    Private Function Ejecutar_Query_Maestra() As DataTable
        Dim dt As New DataTable("Ajustes")
        Dim cs As String = ConfigurationManager.AppSettings("CST")
        If String.IsNullOrEmpty(cs) Then
            Throw New Exception("No se encontro la cadena de conexion 'CST' en la configuracion.")
        End If
        Using cn As New SqlConnection(cs)
            cn.Open()
            Using cmd As New SqlCommand(SQL_MAESTRA, cn)
                cmd.CommandTimeout = 120
                cmd.Parameters.Add("@FechaDel", SqlDbType.DateTime).Value = FechaDel()
                cmd.Parameters.Add("@FechaAl", SqlDbType.DateTime).Value = FechaAl()
                cmd.Parameters.Add("@IdBodega", SqlDbType.Int).Value = IdBodegaSel()
                cmd.Parameters.Add("@IdProveedor", SqlDbType.Int).Value = IdProveedorSel()
                cmd.Parameters.Add("@IdTipoProducto", SqlDbType.Int).Value = IdTipoProductoSel()
                cmd.Parameters.Add("@NoContenedor", SqlDbType.NVarChar, 100).Value = NoContenedorSel()
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' =========================================================================
    ' Construccion de DataTables agregados (LINQ to DataTable)
    ' =========================================================================

    Private Sub Construir_Vistas()
        DT_Proveedor = Construir_DT_Proveedor(DT_Maestro)
        DT_TipoProducto = Construir_DT_TipoProducto(DT_Maestro)
        DT_Tendencia = Construir_DT_Tendencia(DT_Maestro)
        DT_Lote = Construir_DT_Lote(DT_Maestro)
        DT_Vencimiento = Construir_DT_Vencimiento(DT_Maestro)
    End Sub

    Private Function Construir_DT_Proveedor(dtFuente As DataTable) As DataTable
        Dim dt As New DataTable("PorProveedor")
        With dt.Columns
            .Add("Proveedor", GetType(String))
            .Add("CodigoProveedor", GetType(String))
            .Add("NoContenedor", GetType(String))
            .Add("Lineas", GetType(Integer))
            .Add("Positivos", GetType(Integer))
            .Add("Negativos", GetType(Integer))
            .Add("CantPos", GetType(Decimal))
            .Add("CantNeg", GetType(Decimal))
            .Add("CantNeta", GetType(Decimal))
            .Add("PctPositivo", GetType(Decimal))
            .Add("PctNegativo", GetType(Decimal))
            .Add("Tendencia", GetType(String))
        End With

        Dim grupos = From r In dtFuente.AsEnumerable()
                     Group By prov = SafeStr_Row(r, "NombreProveedor"),
                              cod = SafeStr_Row(r, "CodigoProveedor"),
                              cont = SafeStr_Row(r, "NoContenedor")
                     Into g = Group
                     Select New With {
                         .Proveedor = prov,
                         .CodigoProveedor = cod,
                         .NoContenedor = cont,
                         .Grupo = g
                     }

        For Each gr In grupos
            Dim cantPos = (From x In gr.Grupo Where SafeDec_Row(x, "CantidadDiferencia") > 0 Select SafeDec_Row(x, "CantidadDiferencia")).DefaultIfEmpty(0D).Sum()
            Dim cantNeg = (From x In gr.Grupo Where SafeDec_Row(x, "CantidadDiferencia") < 0 Select SafeDec_Row(x, "CantidadDiferencia")).DefaultIfEmpty(0D).Sum()
            Dim cntPos = (From x In gr.Grupo Where SafeDec_Row(x, "CantidadDiferencia") > 0).Count()
            Dim cntNeg = (From x In gr.Grupo Where SafeDec_Row(x, "CantidadDiferencia") < 0).Count()
            Dim total = gr.Grupo.Count()
            Dim pctP As Decimal = If(total = 0, 0D, Math.Round(CDec(cntPos) * 100D / CDec(total), 2))
            Dim pctN As Decimal = If(total = 0, 0D, Math.Round(CDec(cntNeg) * 100D / CDec(total), 2))
            Dim neta As Decimal = cantPos + cantNeg
            Dim tendencia As String = Calcular_Tendencia(neta, cntPos, cntNeg)

            Dim row = dt.NewRow()
            row("Proveedor") = If(String.IsNullOrEmpty(gr.Proveedor), "(Sin proveedor)", gr.Proveedor)
            row("CodigoProveedor") = gr.CodigoProveedor
            row("NoContenedor") = gr.NoContenedor
            row("Lineas") = total
            row("Positivos") = cntPos
            row("Negativos") = cntNeg
            row("CantPos") = cantPos
            row("CantNeg") = cantNeg
            row("CantNeta") = neta
            row("PctPositivo") = pctP
            row("PctNegativo") = pctN
            row("Tendencia") = tendencia
            dt.Rows.Add(row)
        Next

        Dim view = dt.DefaultView
        view.Sort = "CantNeta DESC"
        Return view.ToTable()
    End Function

    Private Function Construir_DT_TipoProducto(dtFuente As DataTable) As DataTable
        Dim dt As New DataTable("PorTipoProducto")
        With dt.Columns
            .Add("TipoProducto", GetType(String))
            .Add("Lineas", GetType(Integer))
            .Add("Productos", GetType(Integer))
            .Add("CantPos", GetType(Decimal))
            .Add("CantNeg", GetType(Decimal))
            .Add("CantNeta", GetType(Decimal))
            .Add("Proveedores", GetType(Integer))
        End With

        Dim grupos = From r In dtFuente.AsEnumerable()
                     Group By tipo = SafeStr_Row(r, "NombreTipoProducto") Into g = Group
                     Select New With {.Tipo = tipo, .Grupo = g}

        For Each gr In grupos
            Dim cantPos = (From x In gr.Grupo Where SafeDec_Row(x, "CantidadDiferencia") > 0 Select SafeDec_Row(x, "CantidadDiferencia")).DefaultIfEmpty(0D).Sum()
            Dim cantNeg = (From x In gr.Grupo Where SafeDec_Row(x, "CantidadDiferencia") < 0 Select SafeDec_Row(x, "CantidadDiferencia")).DefaultIfEmpty(0D).Sum()
            Dim productos = (From x In gr.Grupo Select SafeStr_Row(x, "CodigoProducto")).Distinct().Count()
            Dim provs = (From x In gr.Grupo Select SafeStr_Row(x, "NombreProveedor")).Where(Function(s) Not String.IsNullOrEmpty(s)).Distinct().Count()

            Dim row = dt.NewRow()
            row("TipoProducto") = If(String.IsNullOrEmpty(gr.Tipo), "(Sin clasificar)", gr.Tipo)
            row("Lineas") = gr.Grupo.Count()
            row("Productos") = productos
            row("CantPos") = cantPos
            row("CantNeg") = cantNeg
            row("CantNeta") = cantPos + cantNeg
            row("Proveedores") = provs
            dt.Rows.Add(row)
        Next

        Dim view = dt.DefaultView
        view.Sort = "Lineas DESC"
        Return view.ToTable()
    End Function

    Private Function Construir_DT_Tendencia(dtFuente As DataTable) As DataTable
        Dim dt As New DataTable("Tendencia")
        With dt.Columns
            .Add("Fecha", GetType(Date))
            .Add("Lineas", GetType(Integer))
            .Add("CantPos", GetType(Decimal))
            .Add("CantNeg", GetType(Decimal))
            .Add("CantNeta", GetType(Decimal))
            .Add("Proveedores", GetType(Integer))
        End With

        Dim grupos = From r In dtFuente.AsEnumerable()
                     Where Not r.IsNull("FechaAjusteDia")
                     Group By fecha = CDate(r("FechaAjusteDia")).Date Into g = Group
                     Order By fecha

        For Each gr In grupos
            Dim cantPos = (From x In gr.g Where SafeDec_Row(x, "CantidadDiferencia") > 0 Select SafeDec_Row(x, "CantidadDiferencia")).DefaultIfEmpty(0D).Sum()
            Dim cantNeg = (From x In gr.g Where SafeDec_Row(x, "CantidadDiferencia") < 0 Select SafeDec_Row(x, "CantidadDiferencia")).DefaultIfEmpty(0D).Sum()
            Dim provs = (From x In gr.g Select SafeStr_Row(x, "NombreProveedor")).Where(Function(s) Not String.IsNullOrEmpty(s)).Distinct().Count()

            Dim row = dt.NewRow()
            row("Fecha") = gr.fecha
            row("Lineas") = gr.g.Count()
            row("CantPos") = cantPos
            row("CantNeg") = cantNeg
            row("CantNeta") = cantPos + cantNeg
            row("Proveedores") = provs
            dt.Rows.Add(row)
        Next

        Return dt
    End Function

    Private Function Construir_DT_Lote(dtFuente As DataTable) As DataTable
        Dim dt As New DataTable("PorLote")
        With dt.Columns
            .Add("Proveedor", GetType(String))
            .Add("CodigoProducto", GetType(String))
            .Add("Producto", GetType(String))
            .Add("Lote", GetType(String))
            .Add("FechaVence", GetType(Date))
            .Add("Lineas", GetType(Integer))
            .Add("CantPos", GetType(Decimal))
            .Add("CantNeg", GetType(Decimal))
            .Add("CantNeta", GetType(Decimal))
            .Add("NoContenedor", GetType(String))
        End With

        Dim grupos = From r In dtFuente.AsEnumerable()
                     Where Not String.IsNullOrEmpty(SafeStr_Row(r, "Lote"))
                     Group By prov = SafeStr_Row(r, "NombreProveedor"),
                              cod = SafeStr_Row(r, "CodigoProducto"),
                              prod = SafeStr_Row(r, "NombreProducto"),
                              lote = SafeStr_Row(r, "Lote"),
                              fv = SafeDate_Row(r, "FechaVence"),
                              cont = SafeStr_Row(r, "NoContenedor")
                     Into g = Group
                     Select New With {.Proveedor = prov, .Codigo = cod, .Producto = prod,
                                      .Lote = lote, .FechaVence = fv, .NoContenedor = cont, .Grupo = g}

        For Each gr In grupos
            Dim cantPos = (From x In gr.Grupo Where SafeDec_Row(x, "CantidadDiferencia") > 0 Select SafeDec_Row(x, "CantidadDiferencia")).DefaultIfEmpty(0D).Sum()
            Dim cantNeg = (From x In gr.Grupo Where SafeDec_Row(x, "CantidadDiferencia") < 0 Select SafeDec_Row(x, "CantidadDiferencia")).DefaultIfEmpty(0D).Sum()

            Dim row = dt.NewRow()
            row("Proveedor") = If(String.IsNullOrEmpty(gr.Proveedor), "(Sin proveedor)", gr.Proveedor)
            row("CodigoProducto") = gr.Codigo
            row("Producto") = gr.Producto
            row("Lote") = gr.Lote
            If gr.FechaVence.HasValue Then row("FechaVence") = gr.FechaVence.Value Else row("FechaVence") = DBNull.Value
            row("Lineas") = gr.Grupo.Count()
            row("CantPos") = cantPos
            row("CantNeg") = cantNeg
            row("CantNeta") = cantPos + cantNeg
            row("NoContenedor") = gr.NoContenedor
            dt.Rows.Add(row)
        Next

        Dim view = dt.DefaultView
        view.Sort = "CantNeg ASC, CantPos DESC"
        Return view.ToTable()
    End Function

    Private Function Construir_DT_Vencimiento(dtFuente As DataTable) As DataTable
        Dim dt As New DataTable("PorVencimiento")
        With dt.Columns
            .Add("Proveedor", GetType(String))
            .Add("CodigoProducto", GetType(String))
            .Add("Producto", GetType(String))
            .Add("Lote", GetType(String))
            .Add("FechaVence", GetType(Date))
            .Add("DiasParaVencer", GetType(Integer))
            .Add("Lineas", GetType(Integer))
            .Add("CantNeta", GetType(Decimal))
            .Add("Estado", GetType(String))
        End With

        Dim hoy = Date.Today
        Dim grupos = From r In dtFuente.AsEnumerable()
                     Where Not r.IsNull("FechaVence") AndAlso Not String.IsNullOrEmpty(SafeStr_Row(r, "Lote"))
                     Group By prov = SafeStr_Row(r, "NombreProveedor"),
                              cod = SafeStr_Row(r, "CodigoProducto"),
                              prod = SafeStr_Row(r, "NombreProducto"),
                              lote = SafeStr_Row(r, "Lote"),
                              fv = CDate(r("FechaVence")).Date
                     Into g = Group
                     Select New With {.Proveedor = prov, .Codigo = cod, .Producto = prod,
                                      .Lote = lote, .FechaVence = fv, .Grupo = g}

        For Each gr In grupos
            Dim neta = (From x In gr.Grupo Select SafeDec_Row(x, "CantidadDiferencia")).DefaultIfEmpty(0D).Sum()
            Dim dias = CInt((gr.FechaVence - hoy).TotalDays)
            Dim estado As String
            If dias < 0 Then
                estado = "Vencido"
            ElseIf dias <= 30 Then
                estado = "Critico (<=30d)"
            ElseIf dias <= 90 Then
                estado = "Alerta (<=90d)"
            Else
                estado = "Normal"
            End If

            Dim row = dt.NewRow()
            row("Proveedor") = If(String.IsNullOrEmpty(gr.Proveedor), "(Sin proveedor)", gr.Proveedor)
            row("CodigoProducto") = gr.Codigo
            row("Producto") = gr.Producto
            row("Lote") = gr.Lote
            row("FechaVence") = gr.FechaVence
            row("DiasParaVencer") = dias
            row("Lineas") = gr.Grupo.Count()
            row("CantNeta") = neta
            row("Estado") = estado
            dt.Rows.Add(row)
        Next

        Dim view = dt.DefaultView
        view.Sort = "DiasParaVencer ASC"
        Return view.ToTable()
    End Function

    Private Function Calcular_Tendencia(neta As Decimal, pos As Integer, neg As Integer) As String
        If pos + neg = 0 Then Return "Sin actividad"
        If neta > 0 AndAlso pos > neg Then Return "Sobrante predominante"
        If neta < 0 AndAlso neg > pos Then Return "Faltante predominante"
        If Math.Abs(neta) < 0.001D Then Return "Compensado"
        Return "Mixto"
    End Function

    ' =========================================================================
    ' KPIs / Charts / Combos
    ' =========================================================================

    Private Sub Refrescar_KPIs()
        If DT_Maestro Is Nothing OrElse DT_Maestro.Rows.Count = 0 Then
            Setear_KPI(kpiTotalLineas, "Total Lineas", "0", Color.FromArgb(48, 84, 150))
            Setear_KPI(kpiCantNeta, "Cantidad Neta", "0.00", Color.FromArgb(112, 48, 160))
            Setear_KPI(kpiPositivos, "Positivos", "0", Color.FromArgb(56, 142, 60))
            Setear_KPI(kpiNegativos, "Negativos", "0", Color.FromArgb(198, 40, 40))
            Setear_KPI(kpiProveedores, "Proveedores", "0", Color.FromArgb(0, 121, 107))
            Setear_KPI(kpiContenedores, "Contenedores", "0", Color.FromArgb(245, 124, 0))
            Return
        End If

        Dim total = DT_Maestro.Rows.Count
        Dim cantPos = (From r In DT_Maestro.AsEnumerable() Where SafeDec_Row(r, "CantidadDiferencia") > 0 Select SafeDec_Row(r, "CantidadDiferencia")).DefaultIfEmpty(0D).Sum()
        Dim cantNeg = (From r In DT_Maestro.AsEnumerable() Where SafeDec_Row(r, "CantidadDiferencia") < 0 Select SafeDec_Row(r, "CantidadDiferencia")).DefaultIfEmpty(0D).Sum()
        Dim cntPos = (From r In DT_Maestro.AsEnumerable() Where SafeDec_Row(r, "CantidadDiferencia") > 0).Count()
        Dim cntNeg = (From r In DT_Maestro.AsEnumerable() Where SafeDec_Row(r, "CantidadDiferencia") < 0).Count()
        Dim provs = (From r In DT_Maestro.AsEnumerable() Select SafeStr_Row(r, "NombreProveedor")).Where(Function(s) Not String.IsNullOrEmpty(s)).Distinct().Count()
        Dim conts = (From r In DT_Maestro.AsEnumerable() Select SafeStr_Row(r, "NoContenedor")).Where(Function(s) Not String.IsNullOrEmpty(s)).Distinct().Count()
        Dim neta = cantPos + cantNeg

        Dim accentNeta As Color = If(neta >= 0, Color.FromArgb(56, 142, 60), Color.FromArgb(198, 40, 40))

        Setear_KPI(kpiTotalLineas, "Total Lineas", total.ToString("N0"), Color.FromArgb(48, 84, 150))
        Setear_KPI(kpiCantNeta, "Cantidad Neta", neta.ToString("N2"), accentNeta)
        Setear_KPI(kpiPositivos, "Positivos", String.Format("{0:N0}  (+{1:N2})", cntPos, cantPos), Color.FromArgb(56, 142, 60))
        Setear_KPI(kpiNegativos, "Negativos", String.Format("{0:N0}  ({1:N2})", cntNeg, cantNeg), Color.FromArgb(198, 40, 40))
        Setear_KPI(kpiProveedores, "Proveedores", provs.ToString("N0"), Color.FromArgb(0, 121, 107))
        Setear_KPI(kpiContenedores, "Contenedores", conts.ToString("N0"), Color.FromArgb(245, 124, 0))
    End Sub

    Private Sub Setear_KPI(lbl As LabelControl, titulo As String, valor As String, accent As Color)
        If lbl Is Nothing Then Return
        lbl.Text = String.Format(
            "<size=8><color=gray>{0}</color></size><br><size=18><color={1}><b>{2}</b></color></size>",
            titulo, ColorToHtml(accent), valor)
    End Sub

    Private Sub Refrescar_Charts()
        ' Tipo Producto: barras
        chartTipoProducto.Series.Clear()
        If DT_TipoProducto IsNot Nothing AndAlso DT_TipoProducto.Rows.Count > 0 Then
            Dim sPos = New Series("Positivos", ViewType.Bar)
            Dim sNeg = New Series("Negativos", ViewType.Bar)
            For Each row As DataRow In DT_TipoProducto.Rows
                sPos.Points.Add(New SeriesPoint(SafeStr_Row(row, "TipoProducto"), CDbl(SafeDec_Row(row, "CantPos"))))
                sNeg.Points.Add(New SeriesPoint(SafeStr_Row(row, "TipoProducto"), Math.Abs(CDbl(SafeDec_Row(row, "CantNeg")))))
            Next
            chartTipoProducto.Series.Add(sPos)
            chartTipoProducto.Series.Add(sNeg)
        End If

        ' Tendencia: linea cantidad neta.
        ' Importante: usar argumentos STRING (Qualitative) en vez de DateTime
        ' continuo. DevExpress XtraCharts dispara el error
        '   "The MeasureUnit property can't be modified in both the automatic
        '    and continuous date-time scale modes"
        ' cuando el eje X queda en ScaleType.DateTime con ScaleMode automatico
        ' / continuous y el rango es chico o irregular. Pasar las fechas como
        ' string ordenado cronologicamente evita el conflicto sin perder
        ' legibilidad. El mismo patron se usa en el resto de TOMWMS
        ' (frmReporteAjustesDet, etc.).
        chartTendencia.Series.Clear()
        If DT_Tendencia IsNot Nothing AndAlso DT_Tendencia.Rows.Count > 0 Then
            Dim sNeta = New Series("Cant. Neta", ViewType.Line)
            Dim sPos  = New Series("Positivos", ViewType.Line)
            Dim sNeg  = New Series("Negativos", ViewType.Line)
            sNeta.ArgumentScaleType = ScaleType.Qualitative
            sPos.ArgumentScaleType  = ScaleType.Qualitative
            sNeg.ArgumentScaleType  = ScaleType.Qualitative
            Dim dv As New DataView(DT_Tendencia)
            Try
                dv.Sort = "Fecha ASC"
            Catch
                ' columna podria no existir en algun caso edge; ignorar.
            End Try
            For Each rv As DataRowView In dv
                Dim arg As String
                Try
                    arg = CDate(rv("Fecha")).ToString("dd/MM/yyyy")
                Catch
                    arg = SafeStr_Row(rv.Row, "Fecha")
                End Try
                sNeta.Points.Add(New SeriesPoint(arg, CDbl(SafeDec_Row(rv.Row, "CantNeta"))))
                sPos.Points.Add(New SeriesPoint(arg, CDbl(SafeDec_Row(rv.Row, "CantPos"))))
                sNeg.Points.Add(New SeriesPoint(arg, Math.Abs(CDbl(SafeDec_Row(rv.Row, "CantNeg")))))
            Next
            chartTendencia.Series.Add(sNeta)
            chartTendencia.Series.Add(sPos)
            chartTendencia.Series.Add(sNeg)
        End If
    End Sub

    Private Sub Refrescar_Combos_Dependientes()
        ' Cargar proveedores presentes en el dataset (sin re-query a BD).
        Try
            Dim dtProvs = New DataTable("Proveedores")
            dtProvs.Columns.Add("IdProveedor", GetType(Integer))
            dtProvs.Columns.Add("NombreProveedor", GetType(String))
            Dim distintos = (From r In DT_Maestro.AsEnumerable()
                             Where Not r.IsNull("IdProveedor") AndAlso Not String.IsNullOrEmpty(SafeStr_Row(r, "NombreProveedor"))
                             Select id = CInt(r("IdProveedor")), nom = SafeStr_Row(r, "NombreProveedor")).Distinct()
            For Each x In distintos
                dtProvs.Rows.Add(x.id, x.nom)
            Next
            cmbProveedor.Properties.DataSource = dtProvs
            cmbProveedor.Properties.DisplayMember = "NombreProveedor"
            cmbProveedor.Properties.ValueMember = "IdProveedor"
            cmbProveedor.Properties.PopulateColumns()

            Dim dtTipos = New DataTable("Tipos")
            dtTipos.Columns.Add("IdTipoProducto", GetType(Integer))
            dtTipos.Columns.Add("NombreTipoProducto", GetType(String))
            Dim tipos = (From r In DT_Maestro.AsEnumerable()
                         Where Not r.IsNull("IdTipoProducto") AndAlso Not String.IsNullOrEmpty(SafeStr_Row(r, "NombreTipoProducto"))
                         Select id = CInt(r("IdTipoProducto")), nom = SafeStr_Row(r, "NombreTipoProducto")).Distinct()
            For Each x In tipos
                dtTipos.Rows.Add(x.id, x.nom)
            Next
            cmbTipoProducto.Properties.DataSource = dtTipos
            cmbTipoProducto.Properties.DisplayMember = "NombreTipoProducto"
            cmbTipoProducto.Properties.ValueMember = "IdTipoProducto"
            cmbTipoProducto.Properties.PopulateColumns()
        Catch
            ' Combos opcionales; no abortar carga si fallan.
        End Try
    End Sub

    ' =========================================================================
    ' Formatos / colores
    ' =========================================================================

    Private Sub Aplicar_Formatos_Grids()
        Aplicar_Formatos_Comunes(gvProveedor, {"Lineas", "Positivos", "Negativos", "CantPos", "CantNeg", "CantNeta", "PctPositivo", "PctNegativo"})
        Aplicar_Formatos_Comunes(gvTipoProducto, {"Lineas", "Productos", "CantPos", "CantNeg", "CantNeta", "Proveedores"})
        Aplicar_Formatos_Comunes(gvTendencia, {"Lineas", "CantPos", "CantNeg", "CantNeta", "Proveedores"})
        Aplicar_Formatos_Comunes(gvLote, {"Lineas", "CantPos", "CantNeg", "CantNeta"})
        Aplicar_Formatos_Comunes(gvVencimiento, {"Lineas", "CantNeta", "DiasParaVencer"})

        ' Formato fecha
        Setear_Formato_Fecha(gvTendencia, "Fecha", "dd/MM/yyyy")
        Setear_Formato_Fecha(gvLote, "FechaVence", "dd/MM/yyyy")
        Setear_Formato_Fecha(gvVencimiento, "FechaVence", "dd/MM/yyyy")
    End Sub

    Private Sub Aplicar_Formatos_Comunes(gv As GridView, columnasNumericas As String())
        For Each cn In columnasNumericas
            If gv.Columns(cn) IsNot Nothing Then
                gv.Columns(cn).DisplayFormat.FormatType = FormatType.Numeric
                gv.Columns(cn).DisplayFormat.FormatString = "n2"
                gv.Columns(cn).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                gv.Columns(cn).SummaryItem.DisplayFormat = "{0:n2}"
            End If
        Next
    End Sub

    Private Sub Setear_Formato_Fecha(gv As GridView, col As String, fmt As String)
        If gv.Columns(col) IsNot Nothing Then
            gv.Columns(col).DisplayFormat.FormatType = FormatType.DateTime
            gv.Columns(col).DisplayFormat.FormatString = fmt
        End If
    End Sub

    Private Sub gvProveedor_RowStyle(sender As Object, e As RowStyleEventArgs)
        Try
            If e.RowHandle < 0 Then Return
            Dim view = TryCast(sender, GridView)
            If view Is Nothing Then Return
            Dim valTendencia = view.GetRowCellValue(e.RowHandle, "Tendencia")
            If valTendencia Is Nothing OrElse IsDBNull(valTendencia) Then Return
            Dim t As String = valTendencia.ToString()
            Select Case t
                Case "Sobrante predominante"
                    e.Appearance.BackColor = ColorTranslator.FromHtml("#E8F5E9")
                Case "Faltante predominante"
                    e.Appearance.BackColor = ColorTranslator.FromHtml("#FFEBEE")
                Case "Mixto"
                    e.Appearance.BackColor = ColorTranslator.FromHtml("#FFF8E1")
            End Select
        Catch
        End Try
    End Sub

    Private Sub gvVencimiento_RowStyle(sender As Object, e As RowStyleEventArgs)
        Try
            If e.RowHandle < 0 Then Return
            Dim view = TryCast(sender, GridView)
            If view Is Nothing Then Return
            Dim valEstado = view.GetRowCellValue(e.RowHandle, "Estado")
            If valEstado Is Nothing OrElse IsDBNull(valEstado) Then Return
            Dim s As String = valEstado.ToString()
            Select Case s
                Case "Vencido"
                    e.Appearance.BackColor = ColorTranslator.FromHtml("#EF5350")
                    e.Appearance.ForeColor = Color.White
                Case "Critico (<=30d)"
                    e.Appearance.BackColor = ColorTranslator.FromHtml("#FFCDD2")
                Case "Alerta (<=90d)"
                    e.Appearance.BackColor = ColorTranslator.FromHtml("#FFF59D")
            End Select
        Catch
        End Try
    End Sub

    ' =========================================================================
    ' Exportar / Imprimir
    ' =========================================================================

    Private Sub Exportar_Tab_Activo()
        Try
            Dim grdActivo As GridControl = Get_Grid_Activo()
            If grdActivo Is Nothing OrElse grdActivo.DataSource Is Nothing Then
                XtraMessageBox.Show("No hay datos para exportar en la pestana actual.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim sufijo As String = TabIndicadores.SelectedTabPage.Text.Replace(" ", "_").Replace("/", "_")
            Dim nombreSugerido As String = String.Format("IndicadorAjusteProveedor_{0}_{1:yyyyMMdd_HHmmss}.xlsx", sufijo, Date.Now)

            Using sfd As New SaveFileDialog()
                sfd.Filter = "Excel xlsx (*.xlsx)|*.xlsx|CSV UTF-8 (*.csv)|*.csv"
                sfd.FilterIndex = 1
                sfd.FileName = nombreSugerido
                sfd.RestoreDirectory = True
                If sfd.ShowDialog() <> DialogResult.OK Then Return
                Dim ext As String = Path.GetExtension(sfd.FileName).ToLowerInvariant()
                If ext = ".csv" Then
                    Exportar_Grid_A_Csv(grdActivo, sfd.FileName)
                Else
                    Exportar_Grid_A_Xlsx(grdActivo, sfd.FileName)
                End If
                XtraMessageBox.Show("Exportacion completada: " & sfd.FileName, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            Mostrar_Error(ex, MethodBase.GetCurrentMethod().Name)
        End Try
    End Sub

    Private Function Get_Grid_Activo() As GridControl
        Select Case TabIndicadores.SelectedTabPage.Name
            Case "tabProveedor" : Return grdProveedor
            Case "tabTipoProducto" : Return grdTipoProducto
            Case "tabTendencia" : Return grdTendencia
            Case "tabLote" : Return grdLote
            Case "tabVencimiento" : Return grdVencimiento
            Case Else : Return Nothing
        End Select
    End Function

    Private Sub Exportar_Grid_A_Xlsx(grd As GridControl, ruta As String)
        Using fs As New FileStream(ruta, FileMode.Create)
            grd.ExportToXlsx(fs)
        End Using
    End Sub

    Private Sub Exportar_Grid_A_Csv(grd As GridControl, ruta As String)
        Dim view As GridView = TryCast(grd.MainView, GridView)
        If view Is Nothing Then Return
        Dim sep As String = ";"
        Using sw As New StreamWriter(ruta, False, New UTF8Encoding(True))
            ' Encabezados (columnas visibles)
            Dim cols = view.Columns.Cast(Of DevExpress.XtraGrid.Columns.GridColumn).Where(Function(c) c.Visible).OrderBy(Function(c) c.VisibleIndex).ToList()
            sw.WriteLine(String.Join(sep, cols.Select(Function(c) EscaparCsv(c.Caption, sep))))
            ' Filas visibles
            For i As Integer = 0 To view.RowCount - 1
                Dim handle = view.GetVisibleRowHandle(i)
                If handle < 0 Then Continue For
                Dim valores = cols.Select(Function(c)
                                              Dim v = view.GetRowCellValue(handle, c)
                                              If v Is Nothing OrElse IsDBNull(v) Then Return ""
                                              If TypeOf v Is Decimal OrElse TypeOf v Is Double OrElse TypeOf v Is Single Then
                                                  Return Convert.ToDecimal(v).ToString("0.####", CultureInfo.CurrentCulture)
                                              End If
                                              If TypeOf v Is Date Then Return CDate(v).ToString("yyyy-MM-dd HH:mm:ss")
                                              Return EscaparCsv(v.ToString(), sep)
                                          End Function)
                sw.WriteLine(String.Join(sep, valores))
            Next
        End Using
    End Sub

    Private Function EscaparCsv(s As String, sep As String) As String
        If s Is Nothing Then Return ""
        If s.Contains(sep) OrElse s.Contains("""") OrElse s.Contains(vbCr) OrElse s.Contains(vbLf) Then
            Return """" & s.Replace("""", """""") & """"
        End If
        Return s
    End Function

    Private Sub Imprimir_Tab_Activo()
        Try
            Dim grd As GridControl = Get_Grid_Activo()
            If grd Is Nothing OrElse grd.DataSource Is Nothing Then Return
            Dim ps As New DevExpress.XtraPrinting.PrintingSystem()
            Dim link As New DevExpress.XtraPrinting.PrintableComponentLink()
            link.Component = grd
            link.Landscape = True
            link.CreateDocument(ps)
            ps.PreviewFormEx.ShowDialog()
            ps.Dispose()
        Catch ex As Exception
            Mostrar_Error(ex, MethodBase.GetCurrentMethod().Name)
        End Try
    End Sub

    ' =========================================================================
    ' Helpers genericos
    ' =========================================================================

    Private Function SafeStr_Row(r As DataRow, col As String) As String
        Try
            If Not r.Table.Columns.Contains(col) Then Return ""
            If r.IsNull(col) Then Return ""
            Return Convert.ToString(r(col))
        Catch
            Return ""
        End Try
    End Function

    Private Function SafeDec_Row(r As DataRow, col As String) As Decimal
        Try
            If Not r.Table.Columns.Contains(col) Then Return 0D
            If r.IsNull(col) Then Return 0D
            Return Convert.ToDecimal(r(col))
        Catch
            Return 0D
        End Try
    End Function

    Private Function SafeDate_Row(r As DataRow, col As String) As Date?
        Try
            If Not r.Table.Columns.Contains(col) Then Return Nothing
            If r.IsNull(col) Then Return Nothing
            Return Convert.ToDateTime(r(col))
        Catch
            Return Nothing
        End Try
    End Function

    Private Sub Mostrar_Error(ex As Exception, contexto As String)
        Try
            XtraMessageBox.Show(String.Format("{0}: {1}", contexto, ex.Message),
                                Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Try
                clsLnLog_error_wms.Agregar_Error(String.Format("frmIndicadorAjusteProveedor.{0}: {1}", contexto, ex.Message))
            Catch
                ' Si la DAL no esta accesible (caso test en designer), no abortar.
            End Try
        Catch
        End Try
    End Sub

End Class
