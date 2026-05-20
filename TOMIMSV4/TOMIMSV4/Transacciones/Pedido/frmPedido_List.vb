Imports System.IO
Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core.ConditionalFormatting.Native
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraBars.ToastNotifications
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmPedido_List

    'Private pBePedidoEnc As clsBeTrans_pe_enc
    Public pBePedidoEnc As clsBeTrans_pe_enc

    Public Property Modo As pModo
    Public Property vNombreArchivoLayOutGrid As String = "frmPedido_List.vb"
    Public Property vNombreArchivoLayOutGridDetalle As String = ""
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Public Call_Bind_Listar_Pedidos As New MethodInvoker(AddressOf Listar_Pedidos)
    Public Property verificar_con_imagen = False
    Private Property verificar_bof = False

    Public Sub New()
        InitializeComponent()
        Inicializar_Menu_Contextual_Pedido()
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
        verificacion = 3
    End Enum

    Private Sub frmPedido_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.N AndAlso e.Control Then

            If clsLnMenu_rol.Permiso_Funcionalidad("3.2.1.1", AP.IdRol) Then
                Nuevo_Pedido()
            Else
                XtraMessageBox.Show("No tiene permisos para crear nuevos documentos de salida",
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
            End If

        End If

        If e.KeyCode = Keys.Escape Then Close()

        If e.KeyCode = Keys.F5 Then Listar_Pedidos()

    End Sub

    Private Sub frmPedido_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            mnuNuevo.Enabled = clsLnMenu_rol.Permiso_Funcionalidad("3.2.1.1", AP.IdRol)

            '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
            vNombreArchivoLayOutGrid = "grdPedidoListDespachos.xml"

            vNombreArchivoLayOutGridDetalle = "grdPedidoListDetalle.xml"

            If clsLnMenu_rol.Permiso_Funcionalidad("3.2.1.2", AP.IdRol) Then
                mnuEliminarPedido.Visibility = BarItemVisibility.Always
                ' mnuEliminarPedido.Enabled = True
            Else
                mnuEliminarPedido.Visibility = BarItemVisibility.Never
                ' mnuEliminarPedido.Enabled = False
            End If

            Select Case Modo
                Case pModo.verificacion
                    SetRibbonEnabled(RibbonControl, False)
                    verificar_bof = True
                Case pModo.Lista
                    SetRibbonEnabled(RibbonControl, True)
                    verificar_bof = False
            End Select

            Listar_Pedidos()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub AplicarEstiloScanner()

        With txtGuia.Properties.AppearanceFocused
            .BackColor = Color.LightYellow
            .Font = New Font(txtGuia.Font, FontStyle.Bold)
            .Options.UseBackColor = True
            .Options.UseFont = True
        End With

        ' opcional: también estilo normal cuando NO tiene foco
        With txtGuia.Properties.Appearance
            .BackColor = Color.White
            .Font = New Font(txtGuia.Font, FontStyle.Regular)
            .Options.UseBackColor = True
            .Options.UseFont = True
        End With

    End Sub

    Public Sub SetRibbonEnabled(ribbon As RibbonControl, enabled As Boolean)

        Try
            For Each item As BarItem In ribbon.Items
                ' Si quieres excluir algunos items especiales, lo puedes filtrar aquí
                item.Enabled = enabled
            Next

            If enabled Then
                mnuEliminarPedido.Visibility = BarItemVisibility.Always
            Else
                mnuEliminarPedido.Visibility = BarItemVisibility.Never
            End If

        Finally
        End Try
    End Sub


    Private Sub frmPedido_List_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        Try

            'RibbonControl.SaveLayoutToXml(fileNameLayout)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private IsLoading As Boolean = False
    Private RepositoryItemProgressOperacion As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Private MenuContextualPedido As PopupMenu
    Private mnuCopiarIdPedidoEnc As BarButtonItem
    Private mnuCopiarReferenciaPedido As BarButtonItem
    Private mnuVerIndicadoresPedido As BarButtonItem

    Public Sub Listar_Pedidos()

        IsLoading = True

        Try

            If gviewEncabezadoPedido Is Nothing Then
                Exit Sub
            End If

            gviewEncabezadoPedido.Columns.Clear()

            DgridPedido.DataSource = Nothing

            DgridPedido.BeginUpdate()

            Dim Dt As New DataTable
            Dim vMostrarIndicadores As Boolean = chkMostrarIndicadores IsNot Nothing AndAlso chkMostrarIndicadores.Checked

            If verificar_bof Then

                Dt = clsLnTrans_pe_enc.GetAll_By_VerificacionBOF(chkActivos.Checked,
                                                                 dtpFechaDel.Value,
                                                                 dtpFechaAl.Value,
                                                                 AP.UsuarioAp.IdUsuario,
                                                                 vMostrarIndicadores)

            Else

                If chkTemporales.Checked Then
                    Dt = clsLnTrans_pe_enc.GetAll_Tmp(AP.IdBodega,
                                                  dtpFechaDel.Value,
                                                  dtpFechaAl.Value,
                                                  vMostrarIndicadores)
                Else
                    Dt = clsLnTrans_pe_enc.GetAll(chkActivos.Checked,
                                              dtpFechaDel.Value,
                                              dtpFechaAl.Value,
                                              chkAnulados.Checked,
                                              AP.IdBodega,
                                              chkDespachados.Checked,
                                              chkSinExistencias.Checked,
                                              chkSinExistenciasERP.Checked,
                                              vMostrarIndicadores)
                End If

            End If

            DgridPedido.DataSource = Dt

            If gviewEncabezadoPedido.RowCount > 0 Then
                lblRegs.Caption = String.Format("Registros: {0}", gviewEncabezadoPedido.RowCount)
            End If

            Dim col = New Columns.GridColumn With
                        {.Name = "Enviado_A_ERP_Flag",
                        .Caption = "MI3_Estatus",
                        .FieldName = "Enviado_A_ERP_Flag",
                        .UnboundType = UnboundColumnType.Object,
                        .ColumnEdit = RepositoryItemPictureEdit1}

            If gviewEncabezadoPedido.Columns.ColumnByName("Enviado_A_ERP_Flag") Is Nothing Then
                gviewEncabezadoPedido.Columns.Add(col)
            End If

            col.Visible = True

            Try

                If gviewEncabezadoPedido.Columns.Count > 1 Then
                    gviewEncabezadoPedido.Columns("fec_agr").DisplayFormat.FormatType = FormatType.DateTime
                    gviewEncabezadoPedido.Columns("fec_agr").DisplayFormat.FormatString = "G"
                    gviewEncabezadoPedido.Columns("fec_agr").Caption = "Fecha_Agregado"
                    gviewEncabezadoPedido.Columns("anulado").Caption = "Anulado"
                    gviewEncabezadoPedido.Columns("activo").Caption = "Activo"
                    gviewEncabezadoPedido.Columns("no_documento").Caption = "No_Documento"
                    gviewEncabezadoPedido.Columns("referencia").Caption = "Referencia"
                    gviewEncabezadoPedido.Columns("IdBodega").Visible = False
                    gviewEncabezadoPedido.Columns("IdPrioridadPicking").Visible = False
                    gviewEncabezadoPedido.Columns("verificar_con_imagen").Visible = False
                End If

            Catch ex As Exception

            End Try

            Dim col1 = New Columns.GridColumn With
                        {.Name = "IdPrioridadPickingFlag",
                        .Caption = "Prioridad",
                        .FieldName = "IdPrioridadPickingFlag",
                        .UnboundType = UnboundColumnType.Object,
                        .ColumnEdit = RepositoryItemPictureEdit1}

            If gviewEncabezadoPedido.Columns.ColumnByName("IdPrioridadPickingFlag") Is Nothing Then
                gviewEncabezadoPedido.Columns.Add(col1)
            End If

            col1.Visible = True

            DgridPedido.EndUpdate()

            Set_LayOut_Grid()

            If vMostrarIndicadores Then
                Configurar_Columnas_Indicadores_Operacion()
            End If

            Try

                '#EJC20220427: Prevent column width lost.
                If Not ExisteLayOuotGridEnc Then

                    gviewEncabezadoPedido.OptionsView.ColumnAutoWidth = False

                    If gviewEncabezadoPedido.Columns.Count > 0 Then
                        If gviewEncabezadoPedido.RowCount > 0 Then
                            gviewEncabezadoPedido.BestFitColumns()
                        End If
                    End If

                End If

                'gviewEncabezadoPedido.BestFitColumns()

            Catch ex As Exception
            End Try

            DTPedidosIncompletosReserva = clsLnI_nav_ped_traslado_enc.Get_All_Pedidos_Incompletos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            IsLoading = False
        End Try

    End Sub

    Private Sub Inicializar_Menu_Contextual_Pedido()

        Try

            If RibbonControl Is Nothing OrElse RibbonControl.Manager Is Nothing Then
                Return
            End If

            MenuContextualPedido = New PopupMenu(RibbonControl.Manager)

            mnuCopiarIdPedidoEnc = New BarButtonItem(RibbonControl.Manager, "Copiar IdPedidoEnc")
            mnuCopiarReferenciaPedido = New BarButtonItem(RibbonControl.Manager, "Copiar referencia")
            mnuVerIndicadoresPedido = New BarButtonItem(RibbonControl.Manager, "Ver indicadores del pedido")

            MenuContextualPedido.AddItem(mnuCopiarIdPedidoEnc)
            MenuContextualPedido.AddItem(mnuCopiarReferenciaPedido)
            MenuContextualPedido.AddItem(mnuVerIndicadoresPedido).BeginGroup = True

            AddHandler mnuCopiarIdPedidoEnc.ItemClick, AddressOf mnuCopiarIdPedidoEnc_ItemClick
            AddHandler mnuCopiarReferenciaPedido.ItemClick, AddressOf mnuCopiarReferenciaPedido_ItemClick
            AddHandler mnuVerIndicadoresPedido.ItemClick, AddressOf mnuVerIndicadoresPedido_ItemClick

        Catch ex As Exception
        End Try

    End Sub

    Private Sub DgridPedido_MouseUp(sender As Object, e As MouseEventArgs) Handles DgridPedido.MouseUp

        Try

            If e.Button <> MouseButtons.Right OrElse MenuContextualPedido Is Nothing Then
                Return
            End If

            Dim vHitInfo = gviewEncabezadoPedido.CalcHitInfo(DgridPedido.PointToClient(Control.MousePosition))

            If Not vHitInfo.InRow AndAlso Not vHitInfo.InRowCell Then
                Return
            End If

            gviewEncabezadoPedido.FocusedRowHandle = vHitInfo.RowHandle
            MenuContextualPedido.ShowPopup(Control.MousePosition)

        Catch ex As Exception
        End Try

    End Sub

    Private Sub mnuCopiarIdPedidoEnc_ItemClick(sender As Object, e As ItemClickEventArgs)
        Copiar_Valor_Actual_Pedido("Correlativo")
    End Sub

    Private Sub mnuCopiarReferenciaPedido_ItemClick(sender As Object, e As ItemClickEventArgs)
        Copiar_Valor_Actual_Pedido("referencia", "Referencia")
    End Sub

    Private Sub mnuCopiarIndicadorPickingPedido_ItemClick(sender As Object, e As ItemClickEventArgs)

        Try
            Dim vTexto As String = Construir_Texto_Indicador_Pedido_Actual()

            If vTexto.Trim.Length > 0 Then
                Clipboard.SetText(vTexto)
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuVerIndicadoresPedido_ItemClick(sender As Object, e As ItemClickEventArgs)
        Mostrar_Indicadores_Pedido_Actual()
    End Sub

    Private Sub Copiar_Valor_Actual_Pedido(ParamArray pCampos() As String)

        Try
            Dim vValor As String = Get_Valor_Fila_Actual(pCampos)

            If vValor.Trim.Length > 0 Then
                Clipboard.SetText(vValor)
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Get_Valor_Fila_Actual(ParamArray pCampos() As String) As String

        Try
            Dim vRow As DataRowView = TryCast(gviewEncabezadoPedido.GetFocusedRow(), DataRowView)

            If vRow Is Nothing OrElse vRow.Row Is Nothing OrElse vRow.Row.Table Is Nothing Then
                Return ""
            End If

            For Each vCampo As String In pCampos
                If vRow.Row.Table.Columns.Contains(vCampo) AndAlso Not IsDBNull(vRow.Row(vCampo)) Then
                    Return vRow.Row(vCampo).ToString()
                End If
            Next

        Catch ex As Exception
        End Try

        Return ""

    End Function

    Private Function Get_IdPedidoEnc_Actual() As Integer

        Dim vIdPedidoEnc As Integer = 0
        Integer.TryParse(Get_Valor_Fila_Actual("Correlativo", "IdPedidoEnc"), vIdPedidoEnc)
        Return vIdPedidoEnc

    End Function

    Private Function Construir_Texto_Indicador_Pedido_Actual() As String

        Dim vIdPedidoEnc As Integer = Get_IdPedidoEnc_Actual()

        If vIdPedidoEnc = 0 Then
            Return ""
        End If

        Dim vReferencia As String = Get_Valor_Fila_Actual("referencia", "Referencia")
        Dim vDt As DataTable = clsLnTrans_pe_det.Get_VW_Progreso_Picking_By_IdPedidoEnc_And_IdBodega(vIdPedidoEnc, AP.Bodega.IdBodega)
        Dim vSolicitada As Decimal = 0D
        Dim vPicking As Decimal = 0D
        Dim vVerificada As Decimal = 0D
        Dim vDespachada As Decimal = 0D

        Calcular_Totales_Progreso(vDt, vSolicitada, vPicking, vVerificada, vDespachada)

        Return String.Format("Pedido={0} | Referencia={1} | Picking={2:n2}% | Verificado={3:n2}% | Despachado={4:n2}% | Solicitado={5:n2} | Pick={6:n2} | Verif={7:n2} | Desp={8:n2}",
                             vIdPedidoEnc,
                             vReferencia,
                             Calcular_Porcentaje(vPicking, vSolicitada),
                             Calcular_Porcentaje(vVerificada, vSolicitada),
                             Calcular_Porcentaje(vDespachada, vSolicitada),
                             vSolicitada,
                             vPicking,
                             vVerificada,
                             vDespachada)

    End Function

    Private Sub Mostrar_Indicadores_Pedido_Actual()

        Try
            Dim vIdPedidoEnc As Integer = Get_IdPedidoEnc_Actual()

            If vIdPedidoEnc = 0 Then
                Return
            End If

            Dim vReferencia As String = Get_Valor_Fila_Actual("referencia", "Referencia")
            Dim vDt As DataTable = clsLnTrans_pe_det.Get_VW_Progreso_Picking_By_IdPedidoEnc_And_IdBodega(vIdPedidoEnc, AP.Bodega.IdBodega)
            Dim vKpiPortal As DataTable = Get_Kpi_Picking_Portal_Seguro(vIdPedidoEnc)
            Dim vTiempos As DataTable = Get_Tiempos_Picking_Seguro(vIdPedidoEnc)

            Mostrar_Indicadores_Pedido(vIdPedidoEnc, vReferencia, vDt, vKpiPortal, vTiempos)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Mostrar_Indicadores_Pedido(ByVal pIdPedidoEnc As Integer,
                                           ByVal pReferencia As String,
                                           ByVal pDetalle As DataTable,
                                           ByVal pKpiPortal As DataTable,
                                           ByVal pTiempos As DataTable)

        Dim vSolicitada As Decimal = 0D
        Dim vPicking As Decimal = 0D
        Dim vVerificada As Decimal = 0D
        Dim vDespachada As Decimal = 0D
        Dim vPromedioMinutos As Decimal = Calcular_Totales_Progreso(pDetalle, vSolicitada, vPicking, vVerificada, vDespachada)
        Dim vPorcentajePicking As Decimal = Calcular_Porcentaje(vPicking, vSolicitada)
        Dim vPorcentajeVerificada As Decimal = Calcular_Porcentaje(vVerificada, vSolicitada)
        Dim vPorcentajeDespachada As Decimal = Calcular_Porcentaje(vDespachada, vSolicitada)
        Dim vReprocesos As Integer = Contar_Reprocesos(pDetalle, pKpiPortal)
        Dim vOperadores As DataTable = Crear_DataTable_Operadores(pDetalle, pKpiPortal)
        Dim vDetalleReprocesos As DataTable = Crear_DataTable_Reprocesos(pDetalle, pKpiPortal)

        Dim frmIndicadores As New XtraForm With {
            .Text = String.Format("Indicadores del pedido {0} - {1}", pIdPedidoEnc, pReferencia),
            .StartPosition = FormStartPosition.CenterParent,
            .Width = 1320,
            .Height = 820
        }
        frmIndicadores.Appearance.BackColor = Color.FromArgb(246, 248, 251)
        frmIndicadores.Appearance.Options.UseBackColor = True

        Dim vRoot As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 1,
            .RowCount = 3,
            .Padding = New Padding(14),
            .BackColor = Color.FromArgb(246, 248, 251)
        }
        vRoot.RowStyles.Add(New RowStyle(SizeType.Absolute, 58))
        vRoot.RowStyles.Add(New RowStyle(SizeType.Absolute, 166))
        vRoot.RowStyles.Add(New RowStyle(SizeType.Percent, 100))

        Dim vHeader As New PanelControl With {
            .Dock = DockStyle.Fill,
            .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder,
            .Padding = New Padding(4, 2, 4, 2)
        }
        vHeader.Appearance.BackColor = Color.FromArgb(246, 248, 251)
        vHeader.Appearance.Options.UseBackColor = True

        Dim vTitulo As New LabelControl With {
            .Text = String.Format("Pedido {0} | Ref. {1} | Lineas {2:n0}",
                                  pIdPedidoEnc,
                                  pReferencia,
                                  If(pDetalle Is Nothing, 0, pDetalle.Rows.Count)),
            .Dock = DockStyle.Top
        }
        vTitulo.Appearance.Font = New Font("Segoe UI", 16.0!, FontStyle.Bold)
        vTitulo.Appearance.ForeColor = Color.FromArgb(28, 36, 48)
        vTitulo.Appearance.Options.UseForeColor = True

        Dim vSubTitulo As New LabelControl With {
            .Text = String.Format("Solicitado {0:n2} | Picking {1:n2}% | Verificacion {2:n2}% | Despacho {3:n2}% | Pick-Verif prom. {4:n1} min | Reprocesos {5:n0}",
                                  vSolicitada,
                                  vPorcentajePicking,
                                  vPorcentajeVerificada,
                                  vPorcentajeDespachada,
                                  vPromedioMinutos,
                                  vReprocesos),
            .Dock = DockStyle.Bottom
        }
        vSubTitulo.Appearance.Font = New Font("Segoe UI", 9.5!, FontStyle.Regular)
        vSubTitulo.Appearance.ForeColor = Color.FromArgb(86, 98, 115)
        vSubTitulo.Appearance.Options.UseForeColor = True
        vHeader.Controls.Add(vSubTitulo)
        vHeader.Controls.Add(vTitulo)

        Dim vPanelProgreso As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 6,
            .RowCount = 1,
            .BackColor = Color.FromArgb(246, 248, 251),
            .Padding = New Padding(0, 4, 0, 4)
        }
        For i As Integer = 1 To 6
            vPanelProgreso.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.66!))
        Next

        vPanelProgreso.Controls.Add(Crear_Panel_Progreso("Picking", vPorcentajePicking, String.Format("{0:n2} / {1:n2}", vPicking, vSolicitada), Color.FromArgb(42, 111, 196)), 0, 0)
        vPanelProgreso.Controls.Add(Crear_Panel_Progreso("Verificacion", vPorcentajeVerificada, String.Format("{0:n2} / {1:n2}", vVerificada, vSolicitada), Color.FromArgb(38, 150, 120)), 1, 0)
        vPanelProgreso.Controls.Add(Crear_Panel_Progreso("Despacho", vPorcentajeDespachada, String.Format("{0:n2} / {1:n2}", vDespachada, vSolicitada), Color.FromArgb(88, 128, 76)), 2, 0)
        vPanelProgreso.Controls.Add(Crear_Panel_Kpi("Pick-Verif", String.Format("{0:n1} min", vPromedioMinutos), "Tiempo promedio por linea", Color.FromArgb(116, 83, 168)), 3, 0)
        vPanelProgreso.Controls.Add(Crear_Panel_Kpi("Operadores", String.Format("{0:n0}", If(vOperadores Is Nothing, 0, vOperadores.Rows.Count)), "Con actividad en el pedido", Color.FromArgb(198, 124, 38)), 4, 0)
        vPanelProgreso.Controls.Add(Crear_Panel_Kpi("Reprocesos", String.Format("{0:n0}", vReprocesos), "Reemplazos detectados", If(vReprocesos > 0, Color.FromArgb(190, 72, 68), Color.FromArgb(96, 128, 96))), 5, 0)

        Dim vTabs As New DevExpress.XtraTab.XtraTabControl With {.Dock = DockStyle.Fill}
        vTabs.TabPages.Add(Crear_Tab_Grid_Indicadores("Progreso pedido", pDetalle))

        If vOperadores IsNot Nothing AndAlso vOperadores.Rows.Count > 0 Then
            vTabs.TabPages.Add(Crear_Tab_Grid_Indicadores("Operadores", vOperadores))
        End If

        If vDetalleReprocesos IsNot Nothing AndAlso vDetalleReprocesos.Rows.Count > 0 Then
            vTabs.TabPages.Add(Crear_Tab_Grid_Indicadores("Reprocesos", vDetalleReprocesos))
        End If

        If pKpiPortal IsNot Nothing AndAlso pKpiPortal.Rows.Count > 0 Then
            vTabs.TabPages.Add(Crear_Tab_Grid_Indicadores("KPI portal", pKpiPortal))
        End If

        If pTiempos IsNot Nothing AndAlso pTiempos.Rows.Count > 0 Then
            vTabs.TabPages.Add(Crear_Tab_Grid_Indicadores("Tiempos", pTiempos))
        End If

        vRoot.Controls.Add(vHeader, 0, 0)
        vRoot.Controls.Add(vPanelProgreso, 0, 1)
        vRoot.Controls.Add(vTabs, 0, 2)

        frmIndicadores.Controls.Add(vRoot)
        frmIndicadores.ShowDialog(Me)

    End Sub

    Private Function Get_Kpi_Picking_Portal_Seguro(ByVal pIdPedidoEnc As Integer) As DataTable

        Try
            Return clsLnTrans_pe_det.Get_KPI_Picking_Portal_By_IdPedidoEnc_And_IdBodega(pIdPedidoEnc, AP.Bodega.IdBodega)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Function Get_Tiempos_Picking_Seguro(ByVal pIdPedidoEnc As Integer) As DataTable

        Try
            Return clsLnTrans_pe_det.Get_VW_Tiempos_Picking_Operador_By_IdPedidoEnc_And_IdBodega(pIdPedidoEnc, AP.Bodega.IdBodega)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Function Crear_Tab_Grid_Indicadores(ByVal pTitulo As String, ByVal pDataSource As DataTable) As DevExpress.XtraTab.XtraTabPage

        Dim vPage As New DevExpress.XtraTab.XtraTabPage With {.Text = pTitulo}
        Dim vGrid As New GridControl With {.Dock = DockStyle.Fill, .DataSource = pDataSource}
        Dim vView As New GridView(vGrid)

        vGrid.MainView = vView
        vGrid.ViewCollection.Add(vView)
        Configurar_Grid_Indicadores_Pedido(vView)
        AddHandler vView.RowCellStyle, AddressOf Indicadores_RowCellStyle
        vPage.Controls.Add(vGrid)

        Return vPage

    End Function

    Private Function Crear_Panel_Progreso(ByVal pTitulo As String,
                                          ByVal pPorcentaje As Decimal,
                                          ByVal pDetalle As String,
                                          ByVal pColor As Color) As PanelControl

        Dim vPanel As New PanelControl With {.Dock = DockStyle.Fill, .Padding = New Padding(10)}
        vPanel.Margin = New Padding(4)
        vPanel.Appearance.BackColor = Color.White
        vPanel.Appearance.BorderColor = Color.FromArgb(214, 220, 228)
        vPanel.Appearance.Options.UseBackColor = True
        vPanel.Appearance.Options.UseBorderColor = True
        Dim vLayout As New TableLayoutPanel With {.Dock = DockStyle.Fill, .ColumnCount = 1, .RowCount = 3}
        vLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, 28))
        vLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, 36))
        vLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 100))

        Dim vTitulo As New LabelControl With {.Text = pTitulo, .Dock = DockStyle.Fill}
        vTitulo.Appearance.Font = New Font("Segoe UI", 10.5!, FontStyle.Bold)
        vTitulo.Appearance.ForeColor = pColor
        vTitulo.Appearance.Options.UseForeColor = True

        Dim vProgreso As New ProgressBarControl With {.Dock = DockStyle.Fill}
        vProgreso.Properties.Minimum = 0
        vProgreso.Properties.Maximum = 100
        vProgreso.Properties.ShowTitle = True
        vProgreso.Properties.PercentView = True
        vProgreso.Properties.Appearance.BackColor = Color.FromArgb(232, 236, 242)
        vProgreso.Properties.Appearance.ForeColor = Color.White
        vProgreso.Properties.Appearance.Options.UseBackColor = True
        vProgreso.Properties.Appearance.Options.UseForeColor = True
        vProgreso.Position = CInt(Math.Max(0, Math.Min(100, pPorcentaje)))

        Dim vDetalle As New LabelControl With {.Text = pDetalle, .Dock = DockStyle.Fill}
        vDetalle.Appearance.Font = New Font("Segoe UI", 9.0!, FontStyle.Regular)
        vDetalle.Appearance.ForeColor = Color.FromArgb(76, 86, 102)
        vDetalle.Appearance.Options.UseForeColor = True

        vLayout.Controls.Add(vTitulo, 0, 0)
        vLayout.Controls.Add(vProgreso, 0, 1)
        vLayout.Controls.Add(vDetalle, 0, 2)
        vPanel.Controls.Add(vLayout)

        Return vPanel

    End Function

    Private Function Crear_Panel_Kpi(ByVal pTitulo As String,
                                     ByVal pValor As String,
                                     ByVal pDetalle As String,
                                     ByVal pColor As Color) As PanelControl

        Dim vPanel As New PanelControl With {.Dock = DockStyle.Fill, .Padding = New Padding(10)}
        vPanel.Margin = New Padding(4)
        vPanel.Appearance.BackColor = Color.White
        vPanel.Appearance.BorderColor = Color.FromArgb(214, 220, 228)
        vPanel.Appearance.Options.UseBackColor = True
        vPanel.Appearance.Options.UseBorderColor = True

        Dim vLayout As New TableLayoutPanel With {.Dock = DockStyle.Fill, .ColumnCount = 1, .RowCount = 3}
        vLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, 28))
        vLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, 44))
        vLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 100))

        Dim vTitulo As New LabelControl With {.Text = pTitulo, .Dock = DockStyle.Fill}
        vTitulo.Appearance.Font = New Font("Segoe UI", 10.5!, FontStyle.Bold)
        vTitulo.Appearance.ForeColor = Color.FromArgb(38, 45, 58)
        vTitulo.Appearance.Options.UseForeColor = True

        Dim vValor As New LabelControl With {.Text = pValor, .Dock = DockStyle.Fill}
        vValor.Appearance.Font = New Font("Segoe UI", 18.0!, FontStyle.Bold)
        vValor.Appearance.ForeColor = pColor
        vValor.Appearance.Options.UseForeColor = True

        Dim vDetalle As New LabelControl With {.Text = pDetalle, .Dock = DockStyle.Fill}
        vDetalle.Appearance.Font = New Font("Segoe UI", 9.0!, FontStyle.Regular)
        vDetalle.Appearance.ForeColor = Color.FromArgb(76, 86, 102)
        vDetalle.Appearance.Options.UseForeColor = True

        vLayout.Controls.Add(vTitulo, 0, 0)
        vLayout.Controls.Add(vValor, 0, 1)
        vLayout.Controls.Add(vDetalle, 0, 2)
        vPanel.Controls.Add(vLayout)

        Return vPanel

    End Function

    Private Sub Configurar_Grid_Indicadores_Pedido(ByVal pView As GridView)

        Try
            pView.OptionsBehavior.Editable = False
            pView.OptionsView.ShowAutoFilterRow = True
            pView.OptionsView.ShowFooter = True
            pView.OptionsView.ShowGroupPanel = True
            pView.OptionsView.EnableAppearanceEvenRow = True
            pView.OptionsView.EnableAppearanceOddRow = True
            pView.OptionsView.ColumnAutoWidth = False
            pView.Appearance.HeaderPanel.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold)
            pView.Appearance.FooterPanel.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold)
            pView.Appearance.EvenRow.BackColor = Color.FromArgb(250, 252, 255)
            pView.Appearance.OddRow.BackColor = Color.White
            pView.Appearance.FocusedRow.BackColor = Color.FromArgb(217, 231, 252)
            pView.Appearance.EvenRow.Options.UseBackColor = True
            pView.Appearance.OddRow.Options.UseBackColor = True
            pView.Appearance.FocusedRow.Options.UseBackColor = True

            Configurar_Summary_Indicador(pView, "Cantidad_Solicitada")
            Configurar_Summary_Indicador(pView, "Cantidad_Picking")
            Configurar_Summary_Indicador(pView, "Cantidad_Verificada")
            Configurar_Summary_Indicador(pView, "Cantidad_Despachada")
            Configurar_Summary_Indicador(pView, "Cantidad_Procesada")
            Configurar_Summary_Indicador(pView, "Cantidad_Pickeada_Cajas")
            Configurar_Summary_Indicador(pView, "Diferencia_Picking")
            Configurar_Summary_Indicador(pView, "Reprocesos")
            Configurar_Summary_Indicador(pView, "Minutos_Promedio", SummaryItemType.Average)

            If Not pView.Columns("Operador_Picking") Is Nothing Then
                pView.Columns("Operador_Picking").Caption = "Operador picking"
            End If

            If Not pView.Columns("Operador_Verifico") Is Nothing Then
                pView.Columns("Operador_Verifico").Caption = "Operador verificacion"
            End If

            If Not pView.Columns("Fecha_Picking") Is Nothing Then
                pView.Columns("Fecha_Picking").DisplayFormat.FormatType = FormatType.DateTime
                pView.Columns("Fecha_Picking").DisplayFormat.FormatString = "G"
            End If

            If Not pView.Columns("Fecha_Verificacion") Is Nothing Then
                pView.Columns("Fecha_Verificacion").DisplayFormat.FormatType = FormatType.DateTime
                pView.Columns("Fecha_Verificacion").DisplayFormat.FormatString = "G"
            End If

            Configurar_Columna_Fecha_Indicador(pView, "Fecha_Hora_Inicio")
            Configurar_Columna_Fecha_Indicador(pView, "Fecha_Hora_Fin")
            Configurar_Columna_Fecha_Indicador(pView, "Fecha_Por_Linea")
            Configurar_Columna_Fecha_Indicador(pView, "Primera_Actividad")
            Configurar_Columna_Fecha_Indicador(pView, "Ultima_Actividad")
            Configurar_Columna_Fecha_Indicador(pView, "Fecha_Vence")

            Configurar_Caption_Indicador(pView, "Codigo_Producto", "Codigo prod.")
            Configurar_Caption_Indicador(pView, "Nombre_Producto", "Producto")
            Configurar_Caption_Indicador(pView, "CodigoProducto", "Codigo prod.")
            Configurar_Caption_Indicador(pView, "NombreProducto", "Producto")
            Configurar_Caption_Indicador(pView, "Cantidad_Solicitada", "Solicitado")
            Configurar_Caption_Indicador(pView, "Cantidad_Picking", "Picking")
            Configurar_Caption_Indicador(pView, "Cantidad_Verificada", "Verificado")
            Configurar_Caption_Indicador(pView, "Cantidad_Despachada", "Despachado")
            Configurar_Caption_Indicador(pView, "Cantidad_Procesada", "Procesado")
            Configurar_Caption_Indicador(pView, "Diferencia_Picking", "Dif. picking")
            Configurar_Caption_Indicador(pView, "Minutos_Promedio", "Min. prom.")
            Configurar_Caption_Indicador(pView, "Porcentaje", "% avance")
            Configurar_Caption_Indicador(pView, "Lic_Plate_Reemplazo", "LP reemplazo")
            Configurar_Caption_Indicador(pView, "IdStock_Reemplazo", "Stock reemplazo")
            Configurar_Caption_Indicador(pView, "IdUbicacion_Reemplazo", "Ubic. reemplazo")

            If Not pView.Columns("Porcentaje") Is Nothing Then
                pView.Columns("Porcentaje").DisplayFormat.FormatType = FormatType.Numeric
                pView.Columns("Porcentaje").DisplayFormat.FormatString = "{0:n2}%"
            End If

            If Not pView.Columns("Minutos_Total") Is Nothing Then
                pView.Columns("Minutos_Total").Visible = False
            End If

            If Not pView.Columns("Minutos_Conteo") Is Nothing Then
                pView.Columns("Minutos_Conteo").Visible = False
            End If

            If Not pView.Columns("Operador") Is Nothing Then
                pView.Columns("Operador").GroupIndex = 0
            ElseIf Not pView.Columns("Operador_Picking") Is Nothing Then
                pView.Columns("Operador_Picking").GroupIndex = 0
            End If

            pView.BestFitColumns()
            pView.ExpandAllGroups()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub Configurar_Columna_Fecha_Indicador(ByVal pView As GridView, ByVal pFieldName As String)

        If pView.Columns(pFieldName) Is Nothing Then
            Return
        End If

        With pView.Columns(pFieldName)
            .DisplayFormat.FormatType = FormatType.DateTime
            .DisplayFormat.FormatString = "G"
        End With

    End Sub

    Private Sub Configurar_Caption_Indicador(ByVal pView As GridView,
                                             ByVal pFieldName As String,
                                             ByVal pCaption As String)

        If pView.Columns(pFieldName) Is Nothing Then
            Return
        End If

        pView.Columns(pFieldName).Caption = pCaption

    End Sub

    Private Sub Indicadores_RowCellStyle(ByVal sender As Object, ByVal e As RowCellStyleEventArgs)

        Try
            If e.Column Is Nothing Then
                Return
            End If

            Dim vField As String = e.Column.FieldName.ToLowerInvariant()
            Dim vValorDecimal As Decimal = 0D
            Decimal.TryParse(Convert.ToString(e.CellValue), vValorDecimal)

            If vField.Contains("diferencia") AndAlso vValorDecimal > 0D Then
                e.Appearance.BackColor = Color.FromArgb(255, 232, 232)
                e.Appearance.ForeColor = Color.FromArgb(158, 44, 44)
                e.Appearance.Options.UseBackColor = True
                e.Appearance.Options.UseForeColor = True
                Return
            End If

            If vField.Contains("reemplazo") OrElse vField.Contains("reproceso") Then
                Dim vTexto As String = Convert.ToString(e.CellValue).Trim()

                If vTexto <> "" AndAlso vTexto <> "0" AndAlso Not vTexto.Equals("False", StringComparison.OrdinalIgnoreCase) Then
                    e.Appearance.BackColor = Color.FromArgb(255, 242, 218)
                    e.Appearance.ForeColor = Color.FromArgb(148, 84, 22)
                    e.Appearance.Options.UseBackColor = True
                    e.Appearance.Options.UseForeColor = True
                    Return
                End If
            End If

            If vField.Contains("porcentaje") OrElse vField = "porcentaje" Then
                If vValorDecimal >= 100D Then
                    e.Appearance.BackColor = Color.FromArgb(223, 246, 229)
                    e.Appearance.ForeColor = Color.FromArgb(32, 113, 66)
                ElseIf vValorDecimal >= 80D Then
                    e.Appearance.BackColor = Color.FromArgb(226, 238, 255)
                    e.Appearance.ForeColor = Color.FromArgb(42, 94, 166)
                ElseIf vValorDecimal > 0D Then
                    e.Appearance.BackColor = Color.FromArgb(255, 244, 214)
                    e.Appearance.ForeColor = Color.FromArgb(142, 94, 22)
                Else
                    e.Appearance.BackColor = Color.FromArgb(255, 232, 232)
                    e.Appearance.ForeColor = Color.FromArgb(158, 44, 44)
                End If

                e.Appearance.Options.UseBackColor = True
                e.Appearance.Options.UseForeColor = True
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Function Contar_Reprocesos(ByVal pDetalle As DataTable, ByVal pKpiPortal As DataTable) As Integer

        Dim vReprocesos As DataTable = Crear_DataTable_Reprocesos(pDetalle, pKpiPortal)

        If vReprocesos Is Nothing Then
            Return 0
        End If

        Return vReprocesos.Rows.Count

    End Function

    Private Function Crear_DataTable_Reprocesos(ByVal pDetalle As DataTable, ByVal pKpiPortal As DataTable) As DataTable

        Dim vDt As New DataTable("Reprocesos")
        vDt.Columns.Add("Fuente", GetType(String))
        vDt.Columns.Add("Codigo_Producto", GetType(String))
        vDt.Columns.Add("Nombre_Producto", GetType(String))
        vDt.Columns.Add("Operador", GetType(String))
        vDt.Columns.Add("Lic_Plate", GetType(String))
        vDt.Columns.Add("Lic_Plate_Reemplazo", GetType(String))
        vDt.Columns.Add("IdStock_Reemplazo", GetType(Integer))
        vDt.Columns.Add("IdUbicacion_Reemplazo", GetType(Integer))
        vDt.Columns.Add("Cantidad_Procesada", GetType(Decimal))
        vDt.Columns.Add("Fecha", GetType(Date))

        Agregar_Reprocesos_Desde_DataTable(vDt, pDetalle, "Progreso pedido")
        Agregar_Reprocesos_Desde_DataTable(vDt, pKpiPortal, "KPI portal")

        Return vDt

    End Function

    Private Sub Agregar_Reprocesos_Desde_DataTable(ByVal pDestino As DataTable,
                                                   ByVal pOrigen As DataTable,
                                                   ByVal pFuente As String)

        If pDestino Is Nothing OrElse pOrigen Is Nothing Then
            Return
        End If

        For Each vRow As DataRow In pOrigen.Rows
            If Not Tiene_Reemplazo_Row(vRow) Then
                Continue For
            End If

            Dim vNew As DataRow = pDestino.NewRow()
            vNew("Fuente") = pFuente
            vNew("Codigo_Producto") = Get_String_Row(vRow, "Codigo_Producto", "CodigoProducto", "Codigo Prod.")
            vNew("Nombre_Producto") = Get_String_Row(vRow, "Nombre_Producto", "NombreProducto", "Nombre Prod.")
            vNew("Operador") = Get_String_Row(vRow, "Operador", "Operador_Picking", "Operador_Verifico")
            vNew("Lic_Plate") = Get_String_Row(vRow, "Lic_Plate", "LP", "LicensePlate")
            vNew("Lic_Plate_Reemplazo") = Get_String_Row(vRow, "Lic_Plate_Reemplazo", "LicPlate_Reemplazo")
            vNew("IdStock_Reemplazo") = CInt(Get_Decimal_Row(vRow, "IdStock_Reemplazo"))
            vNew("IdUbicacion_Reemplazo") = CInt(Get_Decimal_Row(vRow, "IdUbicacion_Reemplazo"))
            vNew("Cantidad_Procesada") = Get_Decimal_Row(vRow, "Cantidad_Procesada", "Cantidad_Picking", "cantidad_recibida", "Cantidad_Pickeada_Cajas")

            Dim vFecha As Date = Get_Date_Row(vRow, "Fecha_Picking", "Fecha_Por_Linea", "Fecha_Hora_Fin", "Fecha_Verificacion")
            If vFecha <= DateSerial(1900, 1, 1) Then
                vFecha = Date.Today
            End If
            vNew("Fecha") = vFecha

            pDestino.Rows.Add(vNew)
        Next

    End Sub

    Private Function Crear_DataTable_Operadores(ByVal pDetalle As DataTable, ByVal pKpiPortal As DataTable) As DataTable

        Dim vDt As New DataTable("Operadores")
        vDt.Columns.Add("Operador", GetType(String))
        vDt.Columns.Add("Rol", GetType(String))
        vDt.Columns.Add("Lineas", GetType(Integer))
        vDt.Columns.Add("Cantidad_Solicitada", GetType(Decimal))
        vDt.Columns.Add("Cantidad_Procesada", GetType(Decimal))
        vDt.Columns.Add("Porcentaje", GetType(Decimal))
        vDt.Columns.Add("Minutos_Promedio", GetType(Decimal))
        vDt.Columns.Add("Minutos_Total", GetType(Decimal))
        vDt.Columns.Add("Minutos_Conteo", GetType(Integer))
        vDt.Columns.Add("Primera_Actividad", GetType(Date))
        vDt.Columns.Add("Ultima_Actividad", GetType(Date))
        vDt.Columns.Add("Reprocesos", GetType(Integer))

        If pDetalle IsNot Nothing Then
            For Each vRow As DataRow In pDetalle.Rows
                Dim vSolicitada As Decimal = Get_Decimal_Row(vRow, "Cantidad_Solicitada")
                Dim vReproceso As Boolean = Tiene_Reemplazo_Row(vRow)
                Acumular_Operador(vDt,
                                  Get_String_Row(vRow, "Operador_Picking", "Operador"),
                                  "Picking",
                                  vSolicitada,
                                  Get_Decimal_Row(vRow, "Cantidad_Picking", "Cantidad_Procesada"),
                                  Get_Date_Row(vRow, "Fecha_Picking", "Fecha_Hora_Inicio"),
                                  Get_Date_Row(vRow, "Fecha_Verificacion", "Fecha_Hora_Fin"),
                                  vReproceso)
                Acumular_Operador(vDt,
                                  Get_String_Row(vRow, "Operador_Verifico"),
                                  "Verificacion",
                                  vSolicitada,
                                  Get_Decimal_Row(vRow, "Cantidad_Verificada"),
                                  Get_Date_Row(vRow, "Fecha_Verificacion"),
                                  Get_Date_Row(vRow, "Fecha_Verificacion"),
                                  vReproceso)
            Next
        End If

        If pKpiPortal IsNot Nothing Then
            For Each vRow As DataRow In pKpiPortal.Rows
                Acumular_Operador(vDt,
                                  Get_String_Row(vRow, "Operador"),
                                  "Picking portal",
                                  Get_Decimal_Row(vRow, "Cantidad_Solicitada"),
                                  Get_Decimal_Row(vRow, "Cantidad_Picking", "Cantidad_Procesada", "cantidad_recibida"),
                                  Get_Date_Row(vRow, "Fecha_Hora_Inicio", "Fecha_Por_Linea"),
                                  Get_Date_Row(vRow, "Fecha_Hora_Fin", "Fecha_Por_Linea"),
                                  Tiene_Reemplazo_Row(vRow))
            Next
        End If

        For Each vRow As DataRow In vDt.Rows
            vRow("Porcentaje") = Calcular_Porcentaje(CDec(vRow("Cantidad_Procesada")), CDec(vRow("Cantidad_Solicitada")))

            If CInt(vRow("Minutos_Conteo")) > 0 Then
                vRow("Minutos_Promedio") = Math.Round(CDec(vRow("Minutos_Total")) / CInt(vRow("Minutos_Conteo")), 2)
            Else
                vRow("Minutos_Promedio") = 0D
            End If
        Next

        Return vDt

    End Function

    Private Sub Acumular_Operador(ByVal pDt As DataTable,
                                  ByVal pOperador As String,
                                  ByVal pRol As String,
                                  ByVal pSolicitada As Decimal,
                                  ByVal pProcesada As Decimal,
                                  ByVal pFechaInicio As Date,
                                  ByVal pFechaFin As Date,
                                  ByVal pReproceso As Boolean)

        pOperador = If(pOperador, "").Trim()

        If pOperador = "" Then
            Return
        End If

        Dim vRow As DataRow = Buscar_Operador_Row(pDt, pOperador, pRol)

        If vRow Is Nothing Then
            vRow = pDt.NewRow()
            vRow("Operador") = pOperador
            vRow("Rol") = pRol
            vRow("Lineas") = 0
            vRow("Cantidad_Solicitada") = 0D
            vRow("Cantidad_Procesada") = 0D
            vRow("Porcentaje") = 0D
            vRow("Minutos_Promedio") = 0D
            vRow("Minutos_Total") = 0D
            vRow("Minutos_Conteo") = 0
            vRow("Primera_Actividad") = Date.MinValue
            vRow("Ultima_Actividad") = Date.MinValue
            vRow("Reprocesos") = 0
            pDt.Rows.Add(vRow)
        End If

        vRow("Lineas") = CInt(vRow("Lineas")) + 1
        vRow("Cantidad_Solicitada") = CDec(vRow("Cantidad_Solicitada")) + pSolicitada
        vRow("Cantidad_Procesada") = CDec(vRow("Cantidad_Procesada")) + pProcesada

        If pReproceso Then
            vRow("Reprocesos") = CInt(vRow("Reprocesos")) + 1
        End If

        Actualizar_Rango_Fecha_Operador(vRow, "Primera_Actividad", pFechaInicio, True)
        Actualizar_Rango_Fecha_Operador(vRow, "Ultima_Actividad", pFechaFin, False)

        If pFechaInicio > DateSerial(1900, 1, 1) AndAlso pFechaFin > pFechaInicio Then
            vRow("Minutos_Total") = CDec(vRow("Minutos_Total")) + CDec(DateDiff(DateInterval.Minute, pFechaInicio, pFechaFin))
            vRow("Minutos_Conteo") = CInt(vRow("Minutos_Conteo")) + 1
        End If

    End Sub

    Private Function Buscar_Operador_Row(ByVal pDt As DataTable,
                                         ByVal pOperador As String,
                                         ByVal pRol As String) As DataRow

        For Each vRow As DataRow In pDt.Rows
            If String.Equals(Convert.ToString(vRow("Operador")), pOperador, StringComparison.OrdinalIgnoreCase) AndAlso
               String.Equals(Convert.ToString(vRow("Rol")), pRol, StringComparison.OrdinalIgnoreCase) Then
                Return vRow
            End If
        Next

        Return Nothing

    End Function

    Private Sub Actualizar_Rango_Fecha_Operador(ByVal pRow As DataRow,
                                                ByVal pFieldName As String,
                                                ByVal pFecha As Date,
                                                ByVal pUsarMenor As Boolean)

        If pFecha <= DateSerial(1900, 1, 1) Then
            Return
        End If

        Dim vFechaActual As Date = Date.MinValue
        Date.TryParse(Convert.ToString(pRow(pFieldName)), vFechaActual)

        If vFechaActual <= DateSerial(1900, 1, 1) Then
            pRow(pFieldName) = pFecha
            Return
        End If

        If pUsarMenor AndAlso pFecha < vFechaActual Then
            pRow(pFieldName) = pFecha
        ElseIf Not pUsarMenor AndAlso pFecha > vFechaActual Then
            pRow(pFieldName) = pFecha
        End If

    End Sub

    Private Function Tiene_Reemplazo_Row(ByVal pRow As DataRow) As Boolean

        If pRow Is Nothing OrElse pRow.Table Is Nothing Then
            Return False
        End If

        For Each vColumn As DataColumn In pRow.Table.Columns
            Dim vNombre As String = vColumn.ColumnName.ToLowerInvariant()

            If Not vNombre.Contains("reemplaz") AndAlso Not vNombre.Contains("reproceso") Then
                Continue For
            End If

            If IsDBNull(pRow(vColumn)) Then
                Continue For
            End If

            Dim vValor As String = pRow(vColumn).ToString().Trim()

            If vValor <> "" AndAlso vValor <> "0" AndAlso Not vValor.Equals("False", StringComparison.OrdinalIgnoreCase) Then
                Return True
            End If
        Next

        Return False

    End Function

    Private Sub Configurar_Summary_Indicador(ByVal pView As GridView,
                                             ByVal pFieldName As String,
                                             Optional ByVal pSummaryType As SummaryItemType = SummaryItemType.Sum)

        If pView.Columns(pFieldName) Is Nothing Then
            Return
        End If

        With pView.Columns(pFieldName)
            .DisplayFormat.FormatType = FormatType.Numeric
            .DisplayFormat.FormatString = "{0:n2}"
            .SummaryItem.SummaryType = pSummaryType
            .SummaryItem.DisplayFormat = "{0:n2}"
        End With

    End Sub

    Private Function Calcular_Totales_Progreso(ByVal pDetalle As DataTable,
                                               ByRef pSolicitada As Decimal,
                                               ByRef pPicking As Decimal,
                                               ByRef pVerificada As Decimal,
                                               ByRef pDespachada As Decimal) As Decimal

        Dim vMinutosTotal As Decimal = 0D
        Dim vMinutosConteo As Integer = 0

        If pDetalle Is Nothing Then
            Return 0D
        End If

        For Each vRow As DataRow In pDetalle.Rows
            pSolicitada += Get_Decimal_Row(vRow, "Cantidad_Solicitada")
            pPicking += Get_Decimal_Row(vRow, "Cantidad_Picking")
            pVerificada += Get_Decimal_Row(vRow, "Cantidad_Verificada")
            pDespachada += Get_Decimal_Row(vRow, "Cantidad_Despachada")

            Dim vFechaPicking As Date = Get_Date_Row(vRow, "Fecha_Picking")
            Dim vFechaVerificacion As Date = Get_Date_Row(vRow, "Fecha_Verificacion")

            If vFechaPicking > DateSerial(1900, 1, 1) AndAlso vFechaVerificacion > vFechaPicking Then
                vMinutosTotal += CDec(DateDiff(DateInterval.Minute, vFechaPicking, vFechaVerificacion))
                vMinutosConteo += 1
            End If
        Next

        If vMinutosConteo = 0 Then
            Return 0D
        End If

        Return Math.Round(vMinutosTotal / vMinutosConteo, 2)

    End Function

    Private Function Get_Decimal_Row(ByVal pRow As DataRow, ParamArray pFieldNames As String()) As Decimal

        If pRow Is Nothing OrElse pRow.Table Is Nothing OrElse pFieldNames Is Nothing Then
            Return 0D
        End If

        For Each vFieldName As String In pFieldNames
            If pRow.Table.Columns.Contains(vFieldName) AndAlso Not IsDBNull(pRow(vFieldName)) Then
                Dim vValor As Decimal = 0D
                Decimal.TryParse(pRow(vFieldName).ToString(), vValor)
                Return vValor
            End If
        Next

        Return 0D

    End Function

    Private Function Get_Date_Row(ByVal pRow As DataRow, ParamArray pFieldNames As String()) As Date

        If pRow Is Nothing OrElse pRow.Table Is Nothing OrElse pFieldNames Is Nothing Then
            Return Date.MinValue
        End If

        For Each vFieldName As String In pFieldNames
            If pRow.Table.Columns.Contains(vFieldName) AndAlso Not IsDBNull(pRow(vFieldName)) Then
                Dim vFecha As Date

                If Date.TryParse(pRow(vFieldName).ToString(), vFecha) Then
                    Return vFecha
                End If
            End If
        Next

        Return Date.MinValue

    End Function

    Private Function Get_String_Row(ByVal pRow As DataRow, ParamArray pFieldNames As String()) As String

        If pRow Is Nothing OrElse pRow.Table Is Nothing OrElse pFieldNames Is Nothing Then
            Return ""
        End If

        For Each vFieldName As String In pFieldNames
            If pRow.Table.Columns.Contains(vFieldName) AndAlso Not IsDBNull(pRow(vFieldName)) Then
                Dim vValor As String = pRow(vFieldName).ToString().Trim()

                If vValor <> "" Then
                    Return vValor
                End If
            End If
        Next

        Return ""

    End Function

    Private Function Calcular_Porcentaje(ByVal pCantidad As Decimal, ByVal pTotal As Decimal) As Decimal

        If pTotal <= 0D Then
            Return 0D
        End If

        If pCantidad >= pTotal Then
            Return 100D
        End If

        Return Math.Round((pCantidad * 100D) / pTotal, 2)

    End Function

    Private Sub Configurar_Columnas_Indicadores_Operacion()

        Try

            If gviewEncabezadoPedido.Columns.Count = 0 Then
                Return
            End If

            Configurar_Columna_Numerica_Lista("Lineas_Pedido", "Lineas", "{0:n0}", SummaryItemType.Sum, 70, True)
            Configurar_Columna_Numerica_Lista("Lineas_Reservadas", "Lin. Res.", "{0:n0}", SummaryItemType.Sum, 80, True)
            Configurar_Columna_Numerica_Lista("Lineas_Pendientes_Reserva", "Pend. Res.", "{0:n0}", SummaryItemType.Sum, 90, True)

            Configurar_Columna_Progreso_Lista("Porcentaje_Reservado", "% Res.", 95)
            Configurar_Columna_Progreso_Lista("Porcentaje_Picking", "% Pick.", 95)
            Configurar_Columna_Progreso_Lista("Porcentaje_Verificado", "% Verif.", 95)

            If Not gviewEncabezadoPedido.Columns("Estado_Progreso") Is Nothing Then
                With gviewEncabezadoPedido.Columns("Estado_Progreso")
                    .Caption = "Progreso"
                    .Visible = True
                    .Width = 150
                    .OptionsColumn.AllowEdit = False
                End With
            End If

            Ocultar_Columna_Indicador("Cantidad_Picking_Solicitada_Total")
            Ocultar_Columna_Indicador("Cantidad_Picking_Total")
            Ocultar_Columna_Indicador("Cantidad_Verificada_Total")
            Ocultar_Columna_Indicador("Cantidad_Despachada_Total")

        Catch ex As Exception
        End Try

    End Sub

    Private Sub Configurar_Columna_Numerica_Lista(ByVal pFieldName As String,
                                                  ByVal pCaption As String,
                                                  ByVal pFormatString As String,
                                                  ByVal pSummaryType As SummaryItemType,
                                                  ByVal pWidth As Integer,
                                                  ByVal pVisible As Boolean)

        If gviewEncabezadoPedido.Columns(pFieldName) Is Nothing Then
            Return
        End If

        With gviewEncabezadoPedido.Columns(pFieldName)
            .Caption = pCaption
            .Visible = pVisible
            .Width = pWidth
            .DisplayFormat.FormatType = FormatType.Numeric
            .DisplayFormat.FormatString = pFormatString
            .AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far

            If pSummaryType <> SummaryItemType.None Then
                .SummaryItem.SummaryType = pSummaryType
                .SummaryItem.DisplayFormat = pFormatString
            End If
        End With

    End Sub

    Private Sub Configurar_Columna_Progreso_Lista(ByVal pFieldName As String,
                                                  ByVal pCaption As String,
                                                  ByVal pWidth As Integer)

        If gviewEncabezadoPedido.Columns(pFieldName) Is Nothing Then
            Return
        End If

        With gviewEncabezadoPedido.Columns(pFieldName)
            .Caption = pCaption
            .Visible = True
            .Width = pWidth
            .ColumnEdit = Get_Repository_Progreso_Operacion()
            .DisplayFormat.FormatType = FormatType.Numeric
            .DisplayFormat.FormatString = "{0:n2}%"
            .AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far
        End With

    End Sub

    Private Function Get_Repository_Progreso_Operacion() As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar

        If RepositoryItemProgressOperacion Is Nothing Then
            RepositoryItemProgressOperacion = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar With {
                .Minimum = 0,
                .Maximum = 100,
                .ShowTitle = True,
                .PercentView = True
            }

            DgridPedido.RepositoryItems.Add(RepositoryItemProgressOperacion)
        End If

        Return RepositoryItemProgressOperacion

    End Function

    Private Sub Ocultar_Columna_Indicador(ByVal pFieldName As String)

        If Not gviewEncabezadoPedido.Columns(pFieldName) Is Nothing Then
            gviewEncabezadoPedido.Columns(pFieldName).Visible = False
        End If

    End Sub

    Private Sub Nuevo_Pedido()

        Try

            Cierra_Instancia_Previa(frmPedido)

            With frmPedido

                .Modo = frmPedido.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .InvokeListarPedidos = AddressOf Listar_Pedidos
                .WindowState = FormWindowState.Normal

                If OpcionesMenu IsNot Nothing Then
                    .OpcionesMenu = OpcionesMenu
                    .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                    .cmdActualizar.Enabled = .OpcionesMenu.Modificar
                    .cmdEliminar.Enabled = .OpcionesMenu.Eliminar
                End If

                .Show()
                .Focus()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Cierra_Instancia_Previa(ByRef Myform As Form)

        Try

            For Each objForm In My.Application.OpenForms
                If (Trim(objForm.Name) = Trim(Myform.Name)) Then
                    Myform.Close()
                    Exit For
                End If
            Next

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuNuevo_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuNuevo.ItemClick
        mnuNuevo.Enabled = False
        Nuevo_Pedido()
        mnuNuevo.Enabled = True
    End Sub

    Private Sub Procesar_Registro()

        Try

            If (gviewEncabezadoPedido.RowCount > 0) Then

                Dim Dr As DataRowView = gviewEncabezadoPedido.GetFocusedRow

                If Not Dr Is Nothing Then

                    pBePedidoEnc = New clsBeTrans_pe_enc
                    pBePedidoEnc = clsLnTrans_pe_enc.GetSingle(Dr.Item("Correlativo"))

                    Dim lSelectionIndex As Integer = gviewEncabezadoPedido.FocusedRowHandle

                    If pBePedidoEnc Is Nothing Or pBePedidoEnc.IdPedidoEnc = 0 Then
                        clsLnTrans_pe_enc.Eliminar_Pedido(Dr.Item("Correlativo"))
                        Throw New Exception("El pedido con correlativo WMS: " & Dr.Item("Correlativo") & " fué modificado, eliminado o es un pedido inconsistente y no se puede recuperar")
                    ElseIf pBePedidoEnc.Estado = "NUEVO" AndAlso pBePedidoEnc.Ubicacion = "TMP" Then
                        XtraMessageBox.Show("El pedido seleccionado se creó de forma incorrecta por el usuario en el WMS y no se concluyó: Ubicación = TMP (Valide Cliente y Propietario)", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    '#GT09092024: si es fiscal cargar poliza.
                    If AP.Bodega.Es_Bodega_Fiscal Then
                        pBePedidoEnc.ObjPoliza = clsLnTrans_pe_pol.GetSingleId(pBePedidoEnc.IdPedidoEnc)
                    Else
                        pBePedidoEnc.ObjPoliza = Nothing
                    End If


                    Select Case Modo

                        Case pModo.Lista

                            Cierra_Instancia_Previa(frmPedido)

                            clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231702: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " abrió el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc)

                            With frmPedido
                                .Modo = frmPedido.TipoTrans.Editar
                                .pBePedidoEnc = pBePedidoEnc
                                .InvokeListarPedidos = AddressOf Listar_Pedidos
                                .MdiParent = MdiParent
                                .WindowState = FormWindowState.Normal
                                .Text = "Pedido " & pBePedidoEnc.IdPedidoEnc & " - " & pBePedidoEnc.Referencia

                                If OpcionesMenu IsNot Nothing Then
                                    .OpcionesMenu = OpcionesMenu
                                    .mnuGuardar.Enabled = .OpcionesMenu.Modificar
                                    .cmdActualizar.Enabled = .OpcionesMenu.Modificar
                                    .cmdEliminar.Enabled = .OpcionesMenu.Eliminar
                                End If

                                .Show()
                                .Focus()
                            End With

                            gviewEncabezadoPedido.FocusedRowHandle = lSelectionIndex

                        Case pModo.verificacion

                            '#GT10122025: aqui debo buscar la guia e iterar los pedidos, porque el evento fue por doble click en el grid
                            '#EJC20260105: Obtener todos los pedidos asociados a la guia.
                            pBeListPedidos = New List(Of clsBeTrans_pe_enc)

                            Dim pedidoActual As clsBeTrans_pe_enc = clsLnTrans_pe_enc.GetSingle(CInt(Dr.Item("Correlativo")))
                            If pedidoActual Is Nothing Then Exit Sub

                            Dim correlativosAgregados As New HashSet(Of Integer)

                            ' Función local para agregar solo si no está repetido
                            Dim AddPedido = Sub(pedido As clsBeTrans_pe_enc)
                                                If pedido Is Nothing Then Exit Sub
                                                If correlativosAgregados.Add(pedido.IdPedidoEnc) Then
                                                    pBeListPedidos.Add(pedido)
                                                End If
                                            End Sub

                            ' Agregar pedidos relacionados por guía (si tiene)
                            If Not String.IsNullOrWhiteSpace(pedidoActual.Guia_Transporte) Then

                                Dim dtPedidos As DataTable = clsLnTrans_pe_enc.GetAll_By_Guia_Transporte(pedidoActual.Guia_Transporte)

                                For Each drPedido As DataRow In dtPedidos.Rows
                                    Dim correlativo As Integer = CInt(drPedido("Correlativo"))
                                    Dim pedidoAux As clsBeTrans_pe_enc = clsLnTrans_pe_enc.GetSingle(correlativo)
                                    AddPedido(pedidoAux)
                                Next

                            End If

                            ' Asegurar que el pedido actual también esté incluido
                            AddPedido(pedidoActual)

                            Dim frmVerif As New frmVerificacionBOF

                            Cierra_Instancia_Previa(frmVerif)
                            clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_20251126: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " verifica bof con el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc)

                            With frmVerif

                                .pBeListaPedidos = pBeListPedidos
                                .InvokeListarPedidos = AddressOf Listar_Pedidos
                                ' Si está como hijo MDI, solo ocupará el contenedor MDI, no el monitor completo.
                                .MdiParent = Nothing
                                .StartPosition = FormStartPosition.Manual
                                Dim scr = Screen.FromControl(Me)
                                .FormBorderStyle = FormBorderStyle.None
                                .WindowState = FormWindowState.Normal
                                .Bounds = scr.WorkingArea
                                .Text = "Pedido " & pBePedidoEnc.IdPedidoEnc & " - " & pBePedidoEnc.Referencia
                                .ShowDialog(Me)
                                .Focus()
                            End With

                            gviewEncabezadoPedido.FocusedRowHandle = lSelectionIndex

                        Case pModo.Seleccion
                            DialogResult = DialogResult.OK

                    End Select

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles DgridPedido.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub Dgrid_Click(sender As Object, e As EventArgs) Handles DgridPedido.Click
        Mostrar_Detalle()
    End Sub
    Private Sub chkActivos_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkActivos.CheckedChanged
        Listar_Pedidos()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Pedidos()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub Imprimir_Vista()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = DgridPedido
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "Listado de Ordenes de Compra"

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Try

            Listar_Pedidos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub dtpFechaAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaAl.ValueChanged

        Try

            Listar_Pedidos()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gviewEncabezadoPedido.RowStyle

        Try

            gviewEncabezadoPedido.OptionsBehavior.Editable = False
            gviewEncabezadoPedido.OptionsSelection.EnableAppearanceFocusedCell = False
            gviewEncabezadoPedido.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            gviewEncabezadoPedido.OptionsSelection.EnableAppearanceFocusedRow = True
            gviewEncabezadoPedido.OptionsSelection.EnableAppearanceHideSelection = True
            gviewEncabezadoPedido.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gviewEncabezadoPedido.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            gviewEncabezadoPedido.Appearance.FocusedRow.ForeColor = Color.White
            gviewEncabezadoPedido.Appearance.SelectedRow.ForeColor = Color.White
            gviewEncabezadoPedido.Appearance.SelectedRow.Options.UseBackColor = True
            gviewEncabezadoPedido.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub gviewEncabezadoPedido_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles gviewEncabezadoPedido.RowCellStyle

        Try

            If e.RowHandle < 0 Then
                Return
            End If

            If e.Column Is Nothing Then
                Return
            End If

            If e.Column.FieldName <> "Estado_Progreso" AndAlso
               e.Column.FieldName <> "Porcentaje_Reservado" AndAlso
               e.Column.FieldName <> "Porcentaje_Picking" AndAlso
               e.Column.FieldName <> "Porcentaje_Verificado" AndAlso
               e.Column.FieldName <> "Lineas_Pendientes_Reserva" Then
                Return
            End If

            Dim View As GridView = TryCast(sender, GridView)

            If View Is Nothing OrElse View.Columns("Estado_Progreso") Is Nothing Then
                Return
            End If

            Dim vEstado As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Estado_Progreso"))

            Select Case vEstado
                Case "Listo", "Despachado"
                    e.Appearance.BackColor = Color.Honeydew
                    e.Appearance.ForeColor = Color.DarkGreen
                Case "Reserva pendiente"
                    e.Appearance.BackColor = Color.MistyRose
                    e.Appearance.ForeColor = Color.DarkRed
                Case "Sin picking", "Picking pendiente"
                    e.Appearance.BackColor = Color.LightYellow
                    e.Appearance.ForeColor = Color.FromArgb(120, 78, 0)
                Case "Verificacion pendiente"
                    e.Appearance.BackColor = Color.LightCyan
                    e.Appearance.ForeColor = Color.FromArgb(0, 82, 120)
                Case Else
                    e.Appearance.BackColor = Color.WhiteSmoke
                    e.Appearance.ForeColor = Color.DimGray
            End Select

            e.Appearance.Options.UseBackColor = True
            e.Appearance.Options.UseForeColor = True

        Catch ex As Exception
        End Try

    End Sub

    Private Sub mnuMI3Sync_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuMI3Sync.ItemClick

        Try

            If AP.IdConfiguracionInterface <> -1 Then

                If clsBD.Instancia.IdConfiguracionInterface = 1989 Then

                    lblPrg.Visible = True

                    Dim vCodigoBodega As String = clsLnBodega.Get_Codigo_By_IdBodega(AP.IdBodega)

                    lblPrg.Text = ""

                    If clsSyncPedidoRoad.Procesar_Pedidos_Road(1,
                                                               vCodigoBodega,
                                                               AP.IdEmpresa,
                                                               AP.IdBodega,
                                                               lblPrg,
                                                               prg) Then
                        Listar_Pedidos()
                        'lblPrg.Visible = False
                    End If

                ElseIf Ejecutar_Interface("4-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia, Me) Then
                    Listar_Pedidos()
                End If
            Else
                XtraMessageBox.Show("El archivo de configuración .ini no tiene un identificador de configuración para interface",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles DgridPedido.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub GridView1_CustomUnboundColumnData(sender As Object, e As CustomColumnDataEventArgs) Handles gviewEncabezadoPedido.CustomUnboundColumnData

        If IsLoading Then Exit Sub

        Try

            Dim iconGreen As Image = ImageCollection1.Images(1)
            Dim iconRed As Image = ImageCollection1.Images(0)
            Dim iconYellow As Image = ImageCollection1.Images(2)
            Dim view As GridView = TryCast(sender, GridView)
            Dim rowHandle As Integer = view.GetRowHandle(e.ListSourceRowIndex)

            If e.Column.FieldName = "Enviado_A_ERP_Flag" AndAlso e.IsGetData() Then

                Dim Enviado_A_Erp As Object = view.GetRowCellValue(rowHandle, view.Columns("Enviado_A_ERP"))
                Dim Estado As Object = view.GetRowCellValue(rowHandle, view.Columns("Estado"))
                Dim vEnviado_A_ERP As Boolean = False

                If Enviado_A_Erp IsNot Nothing Then

                    Boolean.TryParse(Enviado_A_Erp.ToString(), vEnviado_A_ERP)

                    If Not Estado Is Nothing Then

                        If Estado.ToString = "Nuevo" Then
                            e.Value = iconYellow 'No se le ha generado picking ni se ha procesado.
                        Else
                            If Not vEnviado_A_ERP Then
                                e.Value = iconRed 'Ya se procesó pero no se pudo enviar a ERP.
                            Else
                                e.Value = iconGreen 'Ya se procesó y se envió correctamente a ERP.
                            End If
                        End If

                    End If

                Else
                    e.Value = ImageCollection1.Images(1)
                End If

            End If

            If e.Column.FieldName = "IdPrioridadPickingFlag" AndAlso e.IsGetData() Then

                Dim IdPrioridadPicking As Object = view.GetRowCellValue(rowHandle, view.Columns("IdPrioridadPicking"))
                Dim vIdPrioridadPicking As Integer = 0

                If IdPrioridadPicking IsNot Nothing Then

                    Integer.TryParse(IdPrioridadPicking.ToString(), vIdPrioridadPicking)

                    Select Case vIdPrioridadPicking

                        Case 0
                            e.Value = iconGreen
                        Case 1
                            e.Value = iconYellow
                        Case 2
                            e.Value = iconRed
                    End Select

                End If

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub chkAnulados_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkAnulados.CheckedChanged
        Listar_Pedidos()
    End Sub

    Private Sub chkDespachados_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkDespachados.CheckedChanged
        Listar_Pedidos()
    End Sub

    Private DTPedidosIncompletosReserva As New DataTable

    Private Sub frmPedido_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        '#EJC20201208: Mostrar rich text si tiene inteface con Road.
        lblPrg.Visible = (clsBD.Instancia.IdConfiguracionInterface = 1989)

        '#EJC20210716:Restaurar LayoutGrid en LotesPorUbi.
        vNombreArchivoLayOutGrid = "grdPedidoListDespachos.xml"

        vNombreArchivoLayOutGridDetalle = "grdPedidoListDetalle.xml"

        If clsLnMenu_rol.Permiso_Funcionalidad("3.2.1.2", AP.IdRol) Then
            mnuEliminarPedido.Enabled = True
        Else
            mnuEliminarPedido.Enabled = False
        End If

        If AP.Bodega.Rango_Dias_Documentos > 0 Then
            dtpFechaDel.Value = Now.Date.AddDays(-(AP.Bodega.Rango_Dias_Documentos))
            dtpFechaAl.Value = Now.Date.AddDays(AP.Bodega.Rango_Dias_Documentos)
        End If

        Try

            DTPedidosIncompletosReserva = clsLnI_nav_ped_traslado_enc.Get_All_Pedidos_Incompletos()

        Catch ex As Exception
        End Try

        If verificar_bof Then
            txtGuia.Visible = True
            lbOk.Visible = True
            lbGuia.Visible = True
            txtGuia.SelectAll()
            txtGuia.Focus()
            AplicarEstiloScanner()
        Else
            txtGuia.Visible = False
            lbOk.Visible = False
            lbGuia.Visible = False
        End If

    End Sub

    Private ExisteLayOuotGridEnc As Boolean = False
    Private Sub Restore_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)


            If Not BeConfiguracionUsuarioDet Is Nothing Then
                gviewEncabezadoPedido.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

            Dim BeConfiguracionUsuarioDet1 As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet1 = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGridDetalle)


            If Not BeConfiguracionUsuarioDet1 Is Nothing Then
                gviewDetallePedido.RestoreLayoutFromStream(BeConfiguracionUsuarioDet1.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Mostrar_Detalle()

        Try

            If (gviewEncabezadoPedido.RowCount > 0) Then

                If chkMostrarGridDetalle.Checked Then

                    Dim Dr As DataRowView = gviewEncabezadoPedido.GetFocusedRow
                    Dim vIdPedidoEnc As Integer = Dr.Item("Correlativo")
                    Dim lSelectionIndex As Integer = gviewEncabezadoPedido.FocusedRowHandle

                    If Modo = pModo.Lista Then

                        Dim DT As New DataTable

                        DT = clsLnTrans_pe_det.Get_VW_Progreso_Picking_By_IdPedidoEnc_And_IdBodega(vIdPedidoEnc, AP.Bodega.IdBodega)

                        dgridDetalle.DataSource = DT

                        gviewDetallePedido.Columns("Cantidad_Solicitada").DisplayFormat.FormatType = FormatType.Numeric
                        gviewDetallePedido.Columns("Cantidad_Solicitada").DisplayFormat.FormatString = "{0:n6}"
                        gviewDetallePedido.Columns("Cantidad_Solicitada").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Solicitada").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Picking").DisplayFormat.FormatType = FormatType.Numeric
                        gviewDetallePedido.Columns("Cantidad_Picking").DisplayFormat.FormatString = "{0:n6}"
                        gviewDetallePedido.Columns("Cantidad_Picking").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Picking").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Verificada").DisplayFormat.FormatType = FormatType.Numeric
                        gviewDetallePedido.Columns("Cantidad_Verificada").DisplayFormat.FormatString = "{0:n6}"
                        gviewDetallePedido.Columns("Cantidad_Verificada").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Verificada").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Despachada").DisplayFormat.FormatType = FormatType.Numeric
                        gviewDetallePedido.Columns("Cantidad_Despachada").DisplayFormat.FormatString = "{0:n6}"
                        gviewDetallePedido.Columns("Cantidad_Despachada").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Despachada").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Solicitada").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Solicitada").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Picking").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Picking").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Verificada").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Verificada").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Cantidad_Despachada").SummaryItem.SummaryType = SummaryItemType.Sum
                        gviewDetallePedido.Columns("Cantidad_Despachada").SummaryItem.DisplayFormat = "{0:n6}"

                        gviewDetallePedido.Columns("Fecha_Picking").DisplayFormat.FormatType = FormatType.DateTime
                        gviewDetallePedido.Columns("Fecha_Picking").DisplayFormat.FormatString = "G"

                        gviewDetallePedido.Columns("Fecha_Verificacion").DisplayFormat.FormatType = FormatType.DateTime
                        gviewDetallePedido.Columns("Fecha_Verificacion").DisplayFormat.FormatString = "G"

                        Restore_LayOut_Grid_Detalle()

                        If Not ExisteLayOutDetealle Then
                            gviewDetallePedido.BestFitColumns()
                        End If

                        gviewEncabezadoPedido.FocusedRowHandle = lSelectionIndex

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub chkMostrarGridDetalle_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkMostrarGridDetalle.CheckedChanged

        dgridDetalle.Visible = chkMostrarGridDetalle.Checked
        SplitContainer1.Panel2Collapsed = Not chkMostrarGridDetalle.Checked

    End Sub

    Private ExisteLayOutDetealle As Boolean = False

    Private Sub Restore_LayOut_Grid_Detalle()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)


            If Not BeConfiguracionUsuarioDet Is Nothing Then
                gviewEncabezadoPedido.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
                ExisteLayOutDetealle = True
            Else
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
                ExisteLayOutDetealle = False
            End If


            Dim BeConfiguracionUsuarioDet1 As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet1 = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGridDetalle)


            If Not BeConfiguracionUsuarioDet1 Is Nothing Then
                gviewDetallePedido.RestoreLayoutFromStream(BeConfiguracionUsuarioDet1.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
                ExisteLayOutDetealle = True
            Else
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
                ExisteLayOutDetealle = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuGuardarLayoutGrid_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuGuardarLayoutGrid.ItemClick

        Try

            If IsLoading Then Exit Sub

            Dim Ms As New MemoryStream
            gviewEncabezadoPedido.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            Dim Ms1 As New MemoryStream
            gviewDetallePedido.SaveLayoutToStream(Ms1)
            Ms1.Seek(0, SeekOrigin.Begin)
            Dim MsReader1 As New StreamReader(Ms1)
            Dim LayoutToText1 As String = MsReader1.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGridDetalle,
                                                          LayoutToText1)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

            XtraMessageBox.Show("Diseño de grid guardado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GridView_Layout(sender As Object, e As EventArgs) Handles gviewEncabezadoPedido.Layout

        Try

            If IsLoading Then Exit Sub

            Dim Ms As New MemoryStream
            gviewEncabezadoPedido.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick

        Try

            clsLnConfiguracion_usuario_enc.Delete_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid)

            XtraMessageBox.Show("Diseño de grid eliminado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Set_LayOut_Grid()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                 vNombreArchivoLayOutGrid)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                gviewEncabezadoPedido.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Async Sub mnuEliminarPedido_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuEliminarPedido.ItemClick

        Try

            If Not permiteMenu("3.2.1.2") Then
                Return
            End If

            If (gviewEncabezadoPedido.RowCount > 0) Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Eliminando documento...")

                Dim Dr As DataRowView = gviewEncabezadoPedido.GetFocusedRow

                If Not Dr Is Nothing Then

                    pBePedidoEnc = New clsBeTrans_pe_enc
                    pBePedidoEnc = clsLnTrans_pe_enc.GetSingle(Dr.Item("Correlativo"))

                    Dim lSelectionIndex As Integer = gviewEncabezadoPedido.FocusedRowHandle

                    If pBePedidoEnc Is Nothing Or pBePedidoEnc.IdPedidoEnc = 0 Then
                        clsLnTrans_pe_enc.Eliminar_Pedido(Dr.Item("Correlativo"))
                        Throw New Exception("El pedido con correlativo WMS: " & Dr.Item("Correlativo") & " fué modificado, eliminado o es un pedido inconsistente y no se puede recuperar")
                    ElseIf pBePedidoEnc.Estado = "NUEVO" AndAlso pBePedidoEnc.Ubicacion = "TMP" Then
                        XtraMessageBox.Show("El pedido seleccionado se creó de forma incorrecta por el usuario en el WMS y no se concluyó: Ubicación = TMP (Valide Cliente y Propietario)", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    pBePedidoEnc.ObjPoliza = clsLnTrans_pe_pol.GetSingleId(pBePedidoEnc.IdPedidoEnc)

                    If Modo = pModo.Lista Then

                        If clsLnTrans_pe_enc.Tiene_Picking_Asociado(pBePedidoEnc.IdPedidoEnc) Then

                            SplashScreenManager.CloseForm(False)
                            XtraMessageBox.Show("No se puede anular, picking asociado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Else

                            If pBePedidoEnc.Estado = "Nuevo" _
                                OrElse pBePedidoEnc.Estado = "Incompleto" _
                                OrElse pBePedidoEnc.Estado = "Pendiente" _
                                OrElse pBePedidoEnc.Estado = "Verificado" _
                                OrElse pBePedidoEnc.Estado = "Anulado" Then

                                SplashScreenManager.CloseForm(False)

                                If XtraMessageBox.Show("¿Eliminar documento de salida:" & pBePedidoEnc.Referencia & "?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                    Using MA As New frmMotivo_AnulacionList()

                                        With MA

                                            .Modo = frmMotivo_AnulacionList.pModo.Seleccion
                                            .BeMotivoAnulacionBodega.IdBodega = pBePedidoEnc.IdBodega

                                            If .ShowDialog() = DialogResult.OK Then

                                                '#CKFK20220324 Modifiqué esto para que cuando esté habilitado el Sync_MI3 y se elimine el documento de salida
                                                'también se elimine en NAV
                                                If pBePedidoEnc.Sync_MI3 Then

                                                    SplashScreenManager.CloseForm(False)

                                                    If XtraMessageBox.Show("¿Eliminar documento de ERP?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                                        SplashScreenManager.Default.SetWaitFormDescription("Anulando...")

                                                        '#EJC20260306: Si la instancia tiene interface con SAP y la instancia de SAP es HANA SL, eliminar el documento desde SL.
                                                        If AP.Bodega.Interface_SAP AndAlso Not clsBD.Instancia.HANA_SL = "" Then

                                                            Dim vHanaService = New SapServiceLayerClient()
                                                            Dim loginResponse As LoginResponseDto = Await vHanaService.LoginAsync()

                                                            Select Case pBePedidoEnc.IdTipoPedido
                                                                Case clsDataContractDI.tTipoDocumentoSalida.Transferencia_Directa
                                                                    Await clsSyncSapTrasladosEnvio.Marcar_Traslado_Sincronizado_SLAsync(pBePedidoEnc.Referencia,
                                                                                                                            vHanaService.SessionCookie, SapServiceLayerClient.baseUrl, 2)
                                                                Case clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS
                                                                    Await clsSyncSapTrasladosEnvio.Marcar_Traslado_Sincronizado_SLAsync(pBePedidoEnc.Referencia,
                                                                                                                            vHanaService.SessionCookie, SapServiceLayerClient.baseUrl, 2)
                                                                Case clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor
                                                                    Await clsSyncSapDevolProveedor.Marcar_Devolucion_Proveedor_Sincronizada_SLAsync(pBePedidoEnc.Referencia,
                                                                                                                                        vHanaService.SessionCookie, SapServiceLayerClient.baseUrl, 2)
                                                                Case clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
                                                                    Await clsSyncSapFacturaReservaCliente.Marcar_Factura_Reserva_Cliente_Sincronizada_SLAsync(pBePedidoEnc.Referencia,
                                                                                                                                                  vHanaService.SessionCookie, SapServiceLayerClient.baseUrl, 2)

                                                            End Select

                                                        End If

                                                        '#EJC202211221049: Validar que la instancia no sea nothing para eliminar desde WS
                                                        If Not wsTOMHHInstance Is Nothing Then

                                                            Dim ArchHeader As New wsTOMHH.clsArchHeader()
                                                            ArchHeader.Tipo = "WM"

                                                            'Si no existe picking no debo borrar
                                                            Dim vResultBorraPicking As Boolean = wsTOMHHInstance.Borrar_Picking(ArchHeader,
                                                                                                                                pBePedidoEnc.Referencia)


                                                            If Not vResultBorraPicking Then
                                                                SplashScreenManager.CloseForm(False)
                                                                XtraMessageBox.Show("No se pudo eliminar el envío de almacén, ni el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Else

                                                                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                                                SplashScreenManager.Default.SetWaitFormDescription("Anulando...")

                                                                If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                                                   AP.Bodega.Eliminar_Documento_Salida,
                                                                                                                                   AP.UsuarioAp.IdUsuario,
                                                                                                                                   MA.BeMotivoAnulacionBodega.IdMotivoAnulacionBodega) Then

                                                                    SplashScreenManager.CloseForm(False)

                                                                    XtraMessageBox.Show("Se ha eliminado el pedido, el envío de almacén y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                                                    Listar_Pedidos()

                                                                Else
                                                                    SplashScreenManager.CloseForm(False)
                                                                    XtraMessageBox.Show("No se pudo eliminar el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                End If

                                                            End If

                                                        Else

                                                            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                                            SplashScreenManager.Default.SetWaitFormDescription("Eliminando...")

                                                            If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                                               AP.Bodega.Eliminar_Documento_Salida,
                                                                                                                               AP.UsuarioAp.IdUsuario) Then

                                                                SplashScreenManager.CloseForm(False)

                                                                Dim vInterfaceSAP As Boolean = clsLnI_nav_config_enc.Get_Interface_SAP(AP.IdConfiguracionInterface)

                                                                Listar_Pedidos()

                                                                Try

                                                                    If vInterfaceSAP Then
                                                                        'EJC202403271301: Actualizar el estado enviado a WMS a 2, para que se peuda volver a importar.

                                                                        Dim vArgumentosAEnviarAInterface As String = ""
                                                                        Dim tipoDocumento As New clsDataContractDI.tTipoDocumentoSalida

                                                                        tipoDocumento = pBePedidoEnc.IdTipoPedido

                                                                        Select Case tipoDocumento
                                                                            Case clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor
                                                                                vArgumentosAEnviarAInterface = "12-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2"
                                                                            Case clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
                                                                                vArgumentosAEnviarAInterface = "8-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2"
                                                                            Case clsDataContractDI.tTipoDocumentoSalida.Traslado_Por_Estados_SAP
                                                                                vArgumentosAEnviarAInterface = "7-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2"
                                                                        End Select

                                                                        'EJC202403271301: Actualizar el estado enviado a WMS a 2, para que se peuda volver a importar.
                                                                        If Ejecutar_Interface(vArgumentosAEnviarAInterface, Me) Then
                                                                            XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                                        End If

                                                                    End If

                                                                Catch ex As Exception
                                                                    XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado, sin embargo no se pudo actualizar el estado en SAP", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                    Exit Sub
                                                                End Try

                                                            Else
                                                                SplashScreenManager.CloseForm(False)
                                                                XtraMessageBox.Show("No se pudo eliminar el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            End If

                                                        End If

                                                    Else

                                                        If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                                           AP.Bodega.Eliminar_Documento_Salida,
                                                                                                                           AP.UsuarioAp.IdUsuario) Then

                                                            SplashScreenManager.CloseForm(False)

                                                            XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                                            Listar_Pedidos()

                                                        Else
                                                            XtraMessageBox.Show("No se pudo eliminar el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        End If

                                                    End If

                                                Else

                                                    Dim vInterfaceSAP As Boolean = clsLnI_nav_config_enc.Get_Interface_SAP(AP.IdConfiguracionInterface)

                                                    If clsLnTrans_pe_enc.Eliminar_Pedido_By_IdPedidoEnc_And_Referencia(pBePedidoEnc,
                                                                                                                       AP.Bodega.Eliminar_Documento_Salida,
                                                                                                                       AP.UsuarioAp.IdUsuario) Then

                                                        Listar_Pedidos()

                                                        SplashScreenManager.CloseForm(False)

                                                        Try

                                                            If vInterfaceSAP AndAlso clsBD.Instancia.HANA_SL = "" Then

                                                                Dim vArgumentosAEnviarAInterface As String = ""
                                                                Dim tipoDocumento As New clsDataContractDI.tTipoDocumentoSalida

                                                                tipoDocumento = pBePedidoEnc.IdTipoPedido

                                                                Select Case tipoDocumento
                                                                    Case clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor
                                                                        vArgumentosAEnviarAInterface = "12-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2-" & AP.HostName
                                                                    Case clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
                                                                        vArgumentosAEnviarAInterface = "8-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2-" & AP.HostName
                                                                    Case clsDataContractDI.tTipoDocumentoSalida.Traslado_Por_Estados_SAP
                                                                        vArgumentosAEnviarAInterface = "7-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & pBePedidoEnc.Referencia & "-2-" & AP.HostName
                                                                End Select

                                                                'EJC202403271301: Actualizar el estado enviado a WMS a 2, para que se peuda volver a importar.
                                                                If Ejecutar_Interface(vArgumentosAEnviarAInterface, Me) Then
                                                                    XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                                End If

                                                            End If

                                                        Catch ex As Exception
                                                            XtraMessageBox.Show("Se ha eliminado el pedido y se ha liberado el stock reservado, sin embargo no se pudo actualizar el estado en SAP", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                            Exit Sub
                                                        End Try

                                                    Else
                                                        SplashScreenManager.CloseForm(False)
                                                        XtraMessageBox.Show("No se pudo eliminar el pedido.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    End If

                                                End If

                                            End If

                                        End With

                                    End Using

                                End If

                            End If

                        End If

                    End If

                End If

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally

            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If

        End Try

    End Sub

    Private Sub mnuExportarExcel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mnuExportarExcel.ItemClick

        Try

            Exportar_Grid_A_Excel(DgridPedido, "WMS_ListaPedidos_" & DateTime.Now.ToString("yyyy_MM_dd") & ".xlsx")

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Exportar_Grid_A_Excel(ByRef dGrid As GridControl, ByVal NomArchivo As String)

        Try

            Try

                Dim myStream As Stream
                Dim saveFileDialog1 As New SaveFileDialog()

                saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
                saveFileDialog1.FilterIndex = 1
                saveFileDialog1.RestoreDirectory = True
                saveFileDialog1.FileName = NomArchivo

                If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                    myStream = saveFileDialog1.OpenFile()
                    If (myStream IsNot Nothing) Then
                        ' Code to write the stream goes here.
                        dGrid.ExportToXlsx(myStream)
                        myStream.Close()
                    End If
                End If

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
            End Try

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub gviewEncabezadoPedido_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gviewEncabezadoPedido.RowStyle

        Try

            If e.RowHandle >= 0 Then

                Dim View As GridView = sender

                '#EJC20210223: Formateo condicional de columnas por reabasto.
                If gviewEncabezadoPedido.RowCount > 0 Then

                    If View.Columns.ColumnByFieldName("referencia") IsNot Nothing Then

                        Dim vReferencia As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("referencia"))

                        Dim PedidoIncompleto As Boolean = DTPedidosIncompletosReserva.AsEnumerable().Any(Function(row) row.Field(Of String)("No") = vReferencia)

                        If (PedidoIncompleto) Then
                            e.Appearance.BackColor = Color.White
                            e.Appearance.BackColor2 = Color.LightSalmon
                        Else
                            e.Appearance.BackColor = Color.White
                            e.Appearance.BackColor2 = Color.White
                        End If

                    End If

                    If View.Columns.ColumnByFieldName("IdPrioridadPicking") IsNot Nothing Then

                        Dim vReferencia As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("IdPrioridadPicking"))

                        Select Case vReferencia

                            Case 0

                            Case 1

                            Case 2

                        End Select

                        'If (PedidoIncompleto) Then
                        '    e.Appearance.BackColor = Color.White
                        '    e.Appearance.BackColor2 = Color.LightSalmon
                        'Else
                        '    e.Appearance.BackColor = Color.White
                        '    e.Appearance.BackColor2 = Color.White
                        'End If

                    End If

                End If

            End If

        Catch ex As Exception

            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub gviewEncabezadoPedido_ColumnFilterChanged(sender As Object, e As EventArgs) Handles gviewEncabezadoPedido.ColumnFilterChanged
        lblRegs.Caption = String.Format("Registros: {0}", String.Format("{0:#,##0.00}", gviewEncabezadoPedido.DataRowCount.ToString()))
    End Sub

    Private Sub chkSinExistencias_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkSinExistencias.CheckedChanged
        Listar_Pedidos()
    End Sub

    Private Sub chkSinExistenciasERP_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkSinExistenciasERP.CheckedChanged
        Listar_Pedidos()
    End Sub

    Private Sub txtGuia_KeyDown(sender As Object, e As KeyEventArgs) Handles txtGuia.KeyDown

        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then
            e.SuppressKeyPress = True   ' evita beep / salto raro
            Buscar_Guia()
        End If
    End Sub


    '#GT08122025: lista global para enviar los pedidos por el escanner o un doble click que busca la guia en la lista y retorne mas de uno
    Dim pBeListPedidos As New List(Of clsBeTrans_pe_enc)()
    Private Sub Buscar_Guia()

        Dim listaGuias As List(Of Integer)
        pBeListPedidos = New List(Of clsBeTrans_pe_enc)()

        Try

            Dim Guia As String = txtGuia.Text.Trim()
            Dim Guia_Transporte As String = Guia.ToUpperInvariant()

            '#GT08122025: una guia puede estar asociada a más de un pedido
            'Dim pIdPedidoEnc = Procesar_Guia(Guia_Transporte)
            listaGuias = Procesar_Guia(txtGuia.Text)

            If listaGuias.Count > 0 Then
                pBePedidoEnc = New clsBeTrans_pe_enc
                For Each pIdPedidoEnc As Integer In listaGuias
                    pBePedidoEnc = clsLnTrans_pe_enc.GetSingle(pIdPedidoEnc)
                    If pBePedidoEnc IsNot Nothing Then
                        If pBePedidoEnc Is Nothing Or pBePedidoEnc.IdPedidoEnc = 0 Then
                            'clsLnTrans_pe_enc.Eliminar_Pedido(pIdPedidoEnc) '#EJC20260105: Peligroso, no utiilizar a menos que confirmemos que esta funcionalidad ocuurre.
                            Throw New Exception("El pedido con correlativo WMS: " & pIdPedidoEnc & " fué modificado, eliminado o es un pedido inconsistente y no se puede recuperar")
                        ElseIf pBePedidoEnc.Estado = "NUEVO" AndAlso pBePedidoEnc.Ubicacion = "TMP" Then
                            Throw New Exception("El pedido " & pBePedidoEnc.IdPedidoEnc & " seleccionado se creó de forma incorrecta por el usuario en el WMS y no se concluyó: Ubicación = TMP (Valide Cliente y Propietario)")
                        End If

                        '#GT09092024: si es fiscal cargar poliza.
                        If AP.Bodega.Es_Bodega_Fiscal Then
                            pBePedidoEnc.ObjPoliza = clsLnTrans_pe_pol.GetSingleId(pBePedidoEnc.IdPedidoEnc)
                        Else
                            pBePedidoEnc.ObjPoliza = Nothing
                        End If

                        pBeListPedidos.Add(pBePedidoEnc)
                    End If
                Next

                Select Case Modo

                    Case pModo.verificacion
                        Abrir_Ventana_Verificacion_BOF()
                    Case pModo.Seleccion
                        DialogResult = DialogResult.OK

                End Select
            Else

                pBePedidoEnc = clsLnTrans_pe_enc.Get_Single_By_NoGuia(Guia)

                If pBePedidoEnc IsNot Nothing Then
                    pBePedidoEnc = clsLnTrans_pe_enc.GetSingle(pBePedidoEnc.IdPedidoEnc)
                    pBeListPedidos.Add(pBePedidoEnc)
                    Abrir_Ventana_Verificacion_BOF()
                Else
                    'ToastError("No se encontró la guia: " & Guia)
                    txtGuia.SelectAll()
                    txtGuia.Focus()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Abrir_Ventana_Verificacion_BOF()

        txtGuia.Text = ""

        Dim frmVerif As New frmVerificacionBOF

        Cierra_Instancia_Previa(frmVerif)
        clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_20251126: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " verifica bof con el IdPedidoEnc: " & pBePedidoEnc.IdPedidoEnc)

        With frmVerif
            .pBeListaPedidos = pBeListPedidos
            .InvokeListarPedidos = AddressOf Listar_Pedidos
            .MdiParent = Nothing
            .StartPosition = FormStartPosition.Manual
            Dim scr = Screen.FromControl(Me)
            .FormBorderStyle = FormBorderStyle.None
            .WindowState = FormWindowState.Normal
            .Bounds = scr.WorkingArea
            .Text = "Pedido " & pBePedidoEnc.IdPedidoEnc & " - " & pBePedidoEnc.Referencia
            .ShowDialog(Me)
            .Focus()
        End With

    End Sub
    Private Function Procesar_Guia(guia_Transporte As String) As List(Of Integer)

        Dim listaIds As New List(Of Integer)

        Try
            Dim dt As DataTable = TryCast(DgridPedido.DataSource, DataTable)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                Return listaIds
            End If

            Dim filas() As DataRow = dt.Select("[Guia] = '" & guia_Transporte.Replace("'", "''") & "'")

            If filas.Length = 0 Then
                XtraMessageBox.Show("No se encontró la guía, por favor reintente: " & guia_Transporte,
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                Return listaIds
            End If

            ' Validar que exista la columna Correlativo
            If Not dt.Columns.Contains("Correlativo") Then
                Return listaIds
            End If

            ' Recorrer todas las filas encontradas y agregar los IdPedidoEnc (Correlativo)
            For Each row As DataRow In filas
                If Not IsDBNull(row("Correlativo")) Then
                    Dim id As Integer = CInt(row("Correlativo"))

                    ' Opcional: evitar duplicados en la lista
                    If Not listaIds.Contains(id) Then
                        listaIds.Add(id)
                    End If
                End If
            Next

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try

        Return listaIds

    End Function

    Private Sub ToastError(msg As String)
        Dim toast As New ToastNotification(Guid.NewGuid(), Nothing, "Error", msg, "TOMWMS", ToastNotificationTemplate.ImageAndText01)
        ToastNotificationsManager1.Notifications.Add(toast)
        ToastNotificationsManager1.ShowNotification(toast)
    End Sub

    Private Sub chkTemporales_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkTemporales.CheckedChanged
        Listar_Pedidos()
    End Sub

    Private Sub chkMostrarIndicadores_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles chkMostrarIndicadores.CheckedChanged
        Listar_Pedidos()
    End Sub
End Class
