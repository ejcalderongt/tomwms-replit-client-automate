Imports System.Reflection
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmCambioUbicacion_List

    Public pObjTranUbicHhEnc As clsBeTrans_ubic_hh_enc
    Public gBeTransubicacionHHEnc As New clsBeTrans_ubic_hh_enc

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Property tipoOperacion As pTipoOperacion
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pTipoOperacion
        CambioUbic = 2
        CambioEst = 3
    End Enum

    Private Sub frmCambioUbicacion_List_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ListarTransaccionUbicHhEnc()

        If tipoOperacion = 2 Then

            Text = "Cambio de ubicación"

            RibbonPage2.Text = "Lista Cambio Ubicación"

            If clsLnMenu_rol.Permiso_Funcionalidad("3.2.1.2", AP.IdRol) Then
                cmdEliminarDocumento.Visibility = BarItemVisibility.Always
            Else
                cmdEliminarDocumento.Visibility = BarItemVisibility.Never
            End If

        ElseIf tipoOperacion = 3 Then

            Text = "Cambio de estado"
            RibbonPage2.Text = "Lista Cambio de Estado"

            cmdEliminarDocumento.Visibility = BarItemVisibility.Never

        End If
    End Sub

    Public Sub ListarTransaccionUbicHhEnc()

        Try

            Dgrid.DataSource = Nothing

            Dim lista As New List(Of clsBeTrans_ubic_hh_enc)

            lista = clsLnTrans_ubic_hh_enc.Get_All_Filtro(chkActivos.Checked,
                                                          dtpFechaInicio.Value.Date,
                                                          dtpFechaFin.Value.Date,
                                                          tipoOperacion = 3).ToList()

            If lista IsNot Nothing AndAlso lista.Count > 0 Then

                Dim DT As New DataTable("TransaccionUbicacionHhEnc")
                DT.Columns.Add(("Código"), GetType(Integer))
                DT.Columns.Add(("UbicacionConHh"), GetType(Boolean))
                DT.Columns.Add(("OperadorPorLinea"), GetType(Boolean))
                DT.Columns.Add(("MotivoUbicacion"), GetType(String))
                DT.Columns.Add(("Observacion"), GetType(String))
                DT.Columns.Add(("FechaInicio"), GetType(Date))
                DT.Columns.Add(("HoraInicio"), GetType(TimeSpan))
                DT.Columns.Add(("FechaFin"), GetType(Date))
                DT.Columns.Add(("HoraFin"), GetType(TimeSpan))
                DT.Columns.Add(("Estado"), GetType(String))
                DT.Columns.Add(("Nombre_Operador"), GetType(String))
                DT.Columns.Add(("Usuario"), GetType(String))
                DT.Columns.Add(("Rol"), GetType(String))

                For Each Obj As clsBeTrans_ubic_hh_enc In lista

                    DT.Rows.Add(Obj.IdTareaUbicacionEnc,
                                Obj.Ubicacion_con_hh,
                                Obj.Operador_por_linea,
                                Obj.DescripcionMotivo,
                                Obj.Observacion,
                                Obj.FechaInicio,
                                Obj.HoraInicio.TimeOfDay,
                                Obj.FechaFin,
                                Obj.HoraFin.TimeOfDay,
                                Obj.Estado,
                                Obj.Nombre_Operador,
                                Obj.Usuario,
                                Obj.Rol)

                Next

                Dgrid.DataSource = DT

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub chkActivos_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkActivos.CheckedChanged
        ListarTransaccionUbicHhEnc()
    End Sub

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSalir.ItemClick
        Close()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        ListarTransaccionUbicHhEnc()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
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
            printLink.Component = Dgrid
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)
        Dim reportHeader As String = ""
        If tipoOperacion = pTipoOperacion.CambioEst Then
            reportHeader = vbNewLine & "Listado de cambios de estado"
        Else
            reportHeader = vbNewLine & "Listado de cambios de ubicación"
        End If

        e.Graph.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center)
        e.Graph.Font = New Font("Tahoma", 12, FontStyle.Bold)

        Dim rec As RectangleF = New RectangleF(0, 0, e.Graph.ClientPageSize.Width, 70)
        e.Graph.DrawString(reportHeader, Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None)

    End Sub

    Private Sub Nuevo_CambioUbic()

        Try

            Cierra_Instancia_Previa(frmCambioUbicacion)

            With frmCambioUbicacion

                .tipoOperacion = tipoOperacion
                .Modo = frmCambioUbicacion.TipoTrans.Nuevo
                .MdiParent = MdiParent
                .InvokeListarUbicHH = AddressOf ListarTransaccionUbicHhEnc

                If OpcionesMenu IsNot Nothing Then
                    .OpcionesMenu = OpcionesMenu
                    .mnuGuardar.Enabled = OpcionesMenu.Modificar
                    .mnuActualizar.Enabled = OpcionesMenu.Modificar
                    .mnuEliminar.Enabled = OpcionesMenu.Eliminar
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

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        Nuevo_CambioUbic()
    End Sub

    Private Sub Procesar_Registro()

        Try
            If (GridView1.RowCount > 0) Then
                Dim Dr As DataRowView = GridView1.GetFocusedRow

                gBeTransubicacionHHEnc = clsLnTrans_ubic_hh_enc.GetSingle(Dr.Item("Código"))

                If Modo = frmCambioUbicacion_List.pModo.Lista Then

                    Cierra_Instancia_Previa(frmCambioUbicacion)

                    With frmCambioUbicacion
                        .tipoOperacion = tipoOperacion
                        .Modo = frmCambioUbicacion.TipoTrans.Editar
                        .InvokeListarTareasUbicacion = AddressOf ListarTransaccionUbicHhEnc
                        .gBeTransubicacionHHEnc = gBeTransubicacionHHEnc
                        .InvokeListarUbicHH = AddressOf ListarTransaccionUbicHhEnc
                        .MdiParent = MdiParent

                        If OpcionesMenu IsNot Nothing Then
                            .OpcionesMenu = OpcionesMenu
                            .mnuGuardar.Enabled = OpcionesMenu.Modificar
                            .mnuActualizar.Enabled = OpcionesMenu.Modificar
                            .mnuEliminar.Enabled = OpcionesMenu.Eliminar
                        End If

                        .Show()
                        .Focus()
                    End With

                ElseIf Modo = pModo.Seleccion Then
                    pObjTranUbicHhEnc = gBeTransubicacionHHEnc
                    Hide()
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub dtpFechaFin_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaFin.ValueChanged

        Try

            If dtpFechaFin.Value < dtpFechaInicio.Value Then
                XtraMessageBox.Show("La fecha fin debe de ser mayor a fecha inicio", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtpFechaFin.Value = Now
            End If

            If Me.dtpFechaInicio.Value > Me.dtpFechaFin.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            End If

            ListarTransaccionUbicHhEnc()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub dtpFechaInicio_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaInicio.ValueChanged
        Try
            ListarTransaccionUbicHhEnc()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub Dgrid_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgrid.KeyDown
        If e.KeyCode = Keys.Enter Then Procesar_Registro()
    End Sub

    Private Sub frmCambioUbicacion_List_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub cmdEliminarDocumento_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdEliminarDocumento.ItemClick

        Try

            If Not permiteMenu("3.2.1.2") Then
                Return
            End If

            If GridView1.RowCount > 0 Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                If Not Dr Is Nothing Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Eliminando documento...")

                    gBeTransubicacionHHEnc = New clsBeTrans_ubic_hh_enc
                    gBeTransubicacionHHEnc = clsLnTrans_ubic_hh_enc.GetSingle(Dr.Item("Código"))

                    If Not gBeTransubicacionHHEnc Is Nothing Then

                        If gBeTransubicacionHHEnc.Estado = "NUEVO" Then

                            If clsLnTrans_ubic_hh_enc.Eliminar(gBeTransubicacionHHEnc) Then

                                If gBeTransubicacionHHEnc.No_Documento <> "" Then

                                    Dim vInterfaceSAP As Boolean = clsLnI_nav_config_enc.Get_Interface_SAP(AP.IdConfiguracionInterface)

                                    Try

                                        If vInterfaceSAP Then

                                            If XtraMessageBox.Show("¿Eliminar documento de ERP?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                                'EJC202403271301: Actualizar el estado enviado a WMS a 2, para que se peuda volver a importar.
                                                If Ejecutar_Interface("8-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & gBeTransubicacionHHEnc.No_Documento & "-2" & "-" & clsBD.Instancia.NombreInstancia, Me) Then
                                                    XtraMessageBox.Show("Se ha eliminado el cambio de ubicación asociado al traslado " & gBeTransubicacionHHEnc.No_Documento & " y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                End If

                                            Else

                                                XtraMessageBox.Show("Se ha eliminado el cambio de ubicación y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                            End If

                                        Else

                                            XtraMessageBox.Show("Se ha eliminado el cambio de ubicación y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                        End If

                                    Catch ex As Exception
                                        XtraMessageBox.Show("Se ha eliminado el cambio de ubicación asociado al traslado " & gBeTransubicacionHHEnc.No_Documento, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    End Try

                                Else

                                    XtraMessageBox.Show("Se ha eliminado el cambio de ubicación y se ha liberado el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                End If

                                Dgrid.Refresh()

                            End If

                        Else

                            XtraMessageBox.Show("No se puede eliminar el cambio de ubicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Dgrid_Click(sender As Object, e As EventArgs) Handles Dgrid.Click

    End Sub
End Class