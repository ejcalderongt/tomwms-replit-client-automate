
Imports System.Reflection
Imports DevExpress.Utils
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmAcuerdoComercial


    Private IdPropietario As Integer
    Private vCorrelativoAcuerdo As Integer


    Private Sub frmAcuerdoComercial_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, AP.IdBodega)

            If cmbPropietario.EditValue > 0 Then

                Dim fila As Object = cmbPropietario.GetSelectedDataRow
                If fila IsNot Nothing Then
                    IdPropietario = fila.Item("IdPropietario")
                End If

                Cargar_Acuerdos_Comerciales()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub Cargar_Acuerdos_Comerciales()
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Listando Acuerdos...")

            Dim DTAcuerdosComerciales As New DataTable
            DTAcuerdosComerciales.Clear()
            DGridAcuerdos.DataSource = Nothing
            DTAcuerdosComerciales = clsLnTrans_acuerdoscomerciales_enc.Get_Acuerdos_Comerciales_By_IdCliente(IdPropietario, chkActivos.Checked)

            If DTAcuerdosComerciales IsNot Nothing Then

                If DTAcuerdosComerciales.Rows.Count > 0 Then

                    DGridAcuerdos.DataSource = DTAcuerdosComerciales

                    GrdAcuerdosComerciales.Columns("estado").Caption = "Activo"
                    GrdAcuerdosComerciales.Columns("estado_enc").Caption = "Activo Encabezado"
                    GrdAcuerdosComerciales.Columns("codigo").OptionsColumn.AllowEdit = False
                    GrdAcuerdosComerciales.Columns("acuerdo").OptionsColumn.AllowEdit = False
                    GrdAcuerdosComerciales.Columns("tipo_cobro").OptionsColumn.AllowEdit = False
                    GrdAcuerdosComerciales.Columns("moneda").OptionsColumn.AllowEdit = False
                    GrdAcuerdosComerciales.Columns("estado_enc").OptionsColumn.AllowEdit = False
                    GrdAcuerdosComerciales.Columns("codigo_producto").OptionsColumn.AllowEdit = False
                    GrdAcuerdosComerciales.Columns("bodega").OptionsColumn.AllowEdit = False
                    GrdAcuerdosComerciales.Columns("tipo cobro").OptionsColumn.AllowEdit = False
                    GrdAcuerdosComerciales.Columns("servicio").OptionsColumn.AllowEdit = False
                    GrdAcuerdosComerciales.Columns("correlativo").OptionsColumn.AllowEdit = False
                    GrdAcuerdosComerciales.Columns("descripcion").OptionsColumn.AllowEdit = False
                    GrdAcuerdosComerciales.Columns("monto").OptionsColumn.AllowEdit = False
                    GrdAcuerdosComerciales.Columns("porcentaje").OptionsColumn.AllowEdit = False

                    Dim spinEdit As New RepositoryItemSpinEdit
                    GrdAcuerdosComerciales.Columns("prioridad").ColumnEdit = spinEdit
                    GrdAcuerdosComerciales.Columns("prioridad").DisplayFormat.FormatType = FormatType.Numeric
                    GrdAcuerdosComerciales.Columns("prioridad").DisplayFormat.FormatString = "D"

                    GrdAcuerdosComerciales.BestFitColumns()
                    lblRegs.Caption = "Registros: " & GrdAcuerdosComerciales.RowCount

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged
        Try

            Dim fila As Object = cmbPropietario.GetSelectedDataRow
            If fila IsNot Nothing Then
                IdPropietario = fila.Item("IdPropietario")
            End If

            Cargar_Acuerdos_Comerciales()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub cmdAplicar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdAplicar.ItemClick
        Try
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Actualizando...")

            Try

                If Actualizar() Then
                    SplashScreenManager.CloseForm(False)
                    Close()
                End If

            Catch ex As Exception
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Finally
                SplashScreenManager.CloseForm(False)
            End Try
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then Actualizar = Actualizar_Acuerdos()

            If Actualizar Then

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Actualizacion completa.")
                SplashScreenManager.CloseForm(False)

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Function

    Private Function Actualizar_Acuerdos() As Boolean
        Try

            If GrdAcuerdosComerciales.RowCount > 0 Then

                Dim pAcuerdoComercial As New clsBeTrans_acuerdoscomerciales_det
                Dim listAcuerdosComerciales As New List(Of clsBeTrans_acuerdoscomerciales_det)
                Dim vCorrelativoAcuerdo As Integer = 0
                Dim vCodigoProducto As String = ""
                Dim vCodigoAcuerdo As Integer = 0
                Dim vEstado As Boolean
                Dim vPrioridad As Integer = 0

                For i As Integer = 0 To GrdAcuerdosComerciales.DataRowCount - 1

                    vCodigoAcuerdo = GrdAcuerdosComerciales.GetRowCellValue(i, "codigo")
                    vCodigoProducto = GrdAcuerdosComerciales.GetRowCellValue(i, "codigo_producto")
                    vCorrelativoAcuerdo = GrdAcuerdosComerciales.GetRowCellValue(i, "correlativo")
                    vEstado = GrdAcuerdosComerciales.GetRowCellValue(i, "estado")
                    vPrioridad = GrdAcuerdosComerciales.GetRowCellValue(i, "prioridad")

                    pAcuerdoComercial = New clsBeTrans_acuerdoscomerciales_det
                    pAcuerdoComercial.Correlativo_detalleacuerdo = vCorrelativoAcuerdo
                    pAcuerdoComercial.Codigo_producto = vCodigoProducto
                    pAcuerdoComercial.Codigo_acuerdo = vCodigoAcuerdo
                    pAcuerdoComercial.Estado = vEstado
                    pAcuerdoComercial.Prioridad = vPrioridad
                    pAcuerdoComercial.fec_mod = Now
                    pAcuerdoComercial.user_mod = AP.UsuarioAp.IdUsuario

                    listAcuerdosComerciales.Add(pAcuerdoComercial)

                Next


                clsLnTrans_acuerdoscomerciales_det.Actualizar_Lista(listAcuerdosComerciales)

                Cargar_Acuerdos_Comerciales()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Function

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbPropietario.EditValue Is Nothing Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbPropietario.Focus()
            ElseIf Grid_Tiene_Error Then
                XtraMessageBox.Show("El detalle del acuerdo contiene errores, debe corregirlos antes de guardar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf GrdAcuerdosComerciales.RowCount < 0 Then
                XtraMessageBox.Show("El detalle del acuerdo esta vacio.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function

    Private Grid_Tiene_Error As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub GrdAcuerdosComerciales_ValidateRow(sender As Object, e As ValidateRowEventArgs) Handles GrdAcuerdosComerciales.ValidateRow
        Try
            Dim View As GridView = CType(sender, GridView)

            Dim Etapa_Uno_Correcta As Boolean = False
            Dim Etapa_Dos_Correcta As Boolean = False
            Dim Etapa_Tres_Correcta As Boolean = False

            Dim isValidPrioridad As Boolean = True
            Dim isValidCodacuerdo As Boolean = True
            Dim isValidCodigoproducto As Boolean = True
            Dim isValidCorrelativo As Boolean = True

            Dim ColEstado As GridColumn = View.Columns("estado")
            Dim ColPrioridad As GridColumn = View.Columns("prioridad")
            Dim ColCodAcuerdo As GridColumn = View.Columns("codacuerdo")
            Dim ColCodigoProducto As GridColumn = View.Columns("codigoproducto")
            Dim ColCorrelativo As GridColumn = View.Columns("correlativo")

            Dim vEstado As Boolean = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "prioridad")), False, View.GetRowCellValue(e.RowHandle, "prioridad"))
            Dim vPrioridad As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "prioridad")), 0, View.GetRowCellValue(e.RowHandle, "prioridad"))
            Dim vCodacuerdo As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "codigo")), 0, View.GetRowCellValue(e.RowHandle, "codigo"))
            Dim vCodigoproducto As String = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "codigo_producto")), "", View.GetRowCellValue(e.RowHandle, "codigo_producto"))
            Dim vCorrelativo As Integer = IIf(IsDBNull(View.GetRowCellValue(e.RowHandle, "correlativo")), 0, View.GetRowCellValue(e.RowHandle, "correlativo"))

            If vPrioridad < 0 Then
                isValidPrioridad = False
                View.SetColumnError(ColPrioridad, "Prioridad debe ser >= 0")
            End If

            If vCodacuerdo = 0 Then
                isValidCodacuerdo = False
                View.SetColumnError(ColCodAcuerdo, "El acuerdo no puede estar vacio!.")
            End If

            If vCodigoproducto = "" Then
                isValidCodigoproducto = False
                View.SetColumnError(ColCodigoProducto, "El codigo del rubro no puede estar vacio!.")
            End If

            If vCorrelativo = 0 Then
                isValidCorrelativo = False
                View.SetColumnError(ColCorrelativo, "El correlativo del rubro no puede estar vacio!.")
            End If


            e.Valid = isValidPrioridad AndAlso isValidCodacuerdo AndAlso isValidCodigoproducto AndAlso
                          isValidCorrelativo

            Etapa_Uno_Correcta = e.Valid

            If Etapa_Uno_Correcta Then
                Grid_Tiene_Error = False
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmbPropietario_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbPropietario.KeyDown
        Try

            If e.KeyCode = Keys.Back Then
                cmbPropietario.EditValue = 0
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub cmdRecargar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdRecargar.ItemClick
        Try
            Cargar_Acuerdos_Comerciales()
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GrdAcuerdosComerciales_InvalidRowException(sender As Object, e As InvalidRowExceptionEventArgs) Handles GrdAcuerdosComerciales.InvalidRowException
        Try

            '#EJC20210307: Evita que salte mensaje indicando si se quiere corregir la fila.
            e.ExceptionMode = ExceptionMode.NoAction

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdImportarAcuerdosERP_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImportarAcuerdosERP.ItemClick

        Try

            If AP.IdConfiguracionInterface <> 0 Then

                Ejecutar_Interface(" -" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-0-0" & "-" & clsBD.Instancia.NombreInstancia, Me)

            Else

                XtraMessageBox.Show(String.Format("La Bodega {0} de la Empresa {1} no  tiene definida configuración para interface", AP.NomBodega, AP.NomEmpresa),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub DGridAcuerdos_DoubleClick(sender As Object, e As EventArgs) Handles DGridAcuerdos.DoubleClick
        Try

            If (GrdAcuerdosComerciales.RowCount > 0) Then

                Dim Dr As DataRowView = GrdAcuerdosComerciales.GetFocusedRow
                If Dr Is Nothing Then Exit Sub

                Dim pCorrelativo As Integer = Integer.Parse(Dr.Item("correlativo"))
                Dim pBeAcuerdoDet As New clsBeTrans_acuerdoscomerciales_det
                pBeAcuerdoDet.Correlativo_detalleacuerdo = pCorrelativo

                Dim lSelectionIndex As Integer = GrdAcuerdosComerciales.FocusedRowHandle
                GrdAcuerdosComerciales.FocusedRowHandle = lSelectionIndex

                clsLnTrans_acuerdoscomerciales_det.GetSingle_By_Correlativo(pBeAcuerdoDet)

                Dim TipoAcuerdo As New frmTipoAcuerdoComercial
                TipoAcuerdo.InvokeListarAcuerdosComerciales = AddressOf Cargar_Acuerdos_Comerciales
                TipoAcuerdo.BeAcuerdoComercialDet = pBeAcuerdoDet
                TipoAcuerdo.OpcionesMenu = OpcionesMenu
                TipoAcuerdo.ShowDialog()
                TipoAcuerdo.Dispose()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GrdAcuerdosComerciales_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GrdAcuerdosComerciales.RowStyle
        Try

            Dim ViewForIncorporated As GridView = GrdAcuerdosComerciales
            Dim isIncorporated As String = ViewForIncorporated.GetRowCellDisplayText(e.RowHandle, ViewForIncorporated.Columns("bodega"))

            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim pTieneBodega As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("bodega"))
                Dim pTieneCobro As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("tipo cobro"))


                'If IsDBNull(StatusDRNADCNCellValue) Then
                If (pTieneBodega <> "" AndAlso pTieneCobro <> "") Then
                    e.Appearance.BackColor = Color.White
                ElseIf pTieneBodega <> "" OrElse pTieneCobro <> "" Then
                    e.Appearance.BackColor = Color.LightGoldenrodYellow
                Else
                    e.Appearance.BackColor = Color.Coral
                End If

            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdImpresion_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImpresion.ItemClick
        Try
            Imprimir_AcuerdosComerciales()
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub Imprimir_AcuerdosComerciales()

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
            printLink.Component = DGridAcuerdos
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As System.Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        Dim reportHeader As String = vbNewLine & "TOM, WMS" &
                              vbNewLine & "Acuerdos Comerciales " &
                              vbNewLine & "BODEGA: " & AP.NomBodega

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

End Class