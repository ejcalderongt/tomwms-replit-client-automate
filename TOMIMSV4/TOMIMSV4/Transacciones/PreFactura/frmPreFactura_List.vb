Imports System.Reflection
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraSplashScreen

Public Class frmPreFactura_List

    Private DT As New DataTable

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub mnuNuevo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNuevo.ItemClick
        Nuevo_Registro()
    End Sub

    Private Sub Nuevo_Registro()

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Nuevo Documento de Prefacturación...")

            Cierra_Instancia_Previa(frmPreFactura)

            With frmPreFactura
                .Modo = frmPreFactura.TipoTrans.Nuevo
                .InvokeListarPrefacturas = AddressOf Listar_Registros
                .MdiParent = MdiParent

                If OpcionesMenu IsNot Nothing Then
                    .OpcionesMenu = OpcionesMenu
                    .cmdGuardar.Enabled = .OpcionesMenu.Modificar
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

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Registros()
    End Sub

    Private Sub Listar_Registros()
        Try
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Listando Registros...")


            DT = clsLnTrans_prefactura_enc.Listar_Movimientos(dtpFechaDel.Value, dtpFechaAl.Value)


            Dgrid.DataSource = Nothing
            Dgrid.DataSource = DT

            If GridView1.Columns.Count > 0 Then

                GridView1.OptionsView.ColumnAutoWidth = False
                GridView1.BestFitColumns()

            End If

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub frmPreFactura_List_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando Registros...")

            Listar_Registros()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                If Dr Is Nothing Then Exit Sub

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Cargando Registro...")

                Dim IdTransPrefacturaEnc As Integer = Integer.Parse(Dr.Item("Codigo"))

                Procesar_Registro(IdTransPrefacturaEnc)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub Procesar_Registro(IdTransPrefacturaEnc As Integer)
        Try
            Dim BeTransPrefacturaEnc As New clsBeTrans_prefactura_enc

            If (IdTransPrefacturaEnc > 0) Then

                BeTransPrefacturaEnc.IdTransPrefacturaEnc = IdTransPrefacturaEnc
                clsLnTrans_prefactura_enc.GetSingle_And_Details_By_IdPrefacturaEnc(BeTransPrefacturaEnc)

                Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                If Modo = pModo.Lista Then

                    Cierra_Instancia_Previa(frmPreFactura)

                    clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231702: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " abrió prefactura: " & BeTransPrefacturaEnc.IdTransPrefacturaEnc)

                    With frmPreFactura
                        .Modo = frmPreFactura.TipoTrans.Editar
                        .BePrefacturaEnc = BeTransPrefacturaEnc
                        .MdiParent = MdiParent
                        .InvokeListarPrefacturas = AddressOf Listar_Registros

                        If OpcionesMenu IsNot Nothing Then
                            .OpcionesMenu = OpcionesMenu
                            .cmdGuardar.Enabled = .OpcionesMenu.Modificar
                        End If

                        .Show()
                        .Focus()
                    End With

                    GridView1.FocusedRowHandle = lSelectionIndex

                ElseIf Modo = pModo.Seleccion Then
                    DialogResult = DialogResult.OK
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub dtpFechaDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDel.ValueChanged

        Listar_Registros()

    End Sub

    Private Sub ToolTipController1_GetActiveObjectInfo(ByVal sender As Object, ByVal e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs) Handles ToolTipController1.GetActiveObjectInfo
        Try
            If e.SelectedControl IsNot Dgrid Then Return

            Dim info As ToolTipControlInfo = Nothing
            Dim view As GridView = TryCast(Dgrid.GetViewAt(e.ControlMousePosition), GridView)
            If view Is Nothing Then Return
            Dim hi As GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)
            Dim lAreasAsociadas As New List(Of Integer)
            Dim lUbicaciones As New List(Of Integer)
            Dim lBodegas As New List(Of String)
            Dim pIdPedidoEnc As New clsBeTrans_prefactura_enc
            Dim pListaUbicaciones As New List(Of clsBeTrans_picking_ubic)

            If hi.HitTest = GridHitTest.RowCell Then

                Dim o As Object = hi.HitTest.ToString() + hi.RowHandle.ToString()
                Dim IdPrefactura As String = view.GetRowCellDisplayText(hi.RowHandle, view.Columns("Codigo"))
                Dim TipoConsolidador As Boolean = view.GetRowCellValue(hi.RowHandle, view.Columns("Consolidador"))

                If Not IdPrefactura.Trim = "" Then

                    pIdPedidoEnc.IdTransPrefacturaEnc = CInt(IdPrefactura)
                    clsLnTrans_prefactura_enc.GetSingle(pIdPedidoEnc)


                    lAreasAsociadas = New List(Of Integer)
                    lUbicaciones = New List(Of Integer)
                    lBodegas = New List(Of String)

                    If TipoConsolidador Then

                        If pIdPedidoEnc IsNot Nothing Then
                            pListaUbicaciones = clsLnTrans_picking_ubic.Get_Picking_Ubic_By_IdPedidoEnc(pIdPedidoEnc.IdOrdenPedidoEnc, pIdPedidoEnc.IdBodega)

                            If pListaUbicaciones IsNot Nothing Then

                                For Each ubicacion In pListaUbicaciones

                                    If Not lUbicaciones.Contains(ubicacion.IdUbicacion) Then
                                        lUbicaciones.Add(ubicacion.IdUbicacion)
                                    End If

                                Next

                                If lUbicaciones.Count > 0 Then

                                    For Each ubicacion In lUbicaciones

                                        Dim Area As New clsBeBodega_ubicacion
                                        Area = clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(ubicacion, pIdPedidoEnc.IdBodega)

                                        If Not lAreasAsociadas.Contains(Area.IdArea) Then
                                            lAreasAsociadas.Add(Area.IdArea)
                                        End If

                                    Next

                                End If

                                If lAreasAsociadas.Count > 0 Then

                                    For Each area In lAreasAsociadas

                                        Dim bodegaArea As New clsBeBodega_area
                                        bodegaArea = clsLnBodega_area.GetSingle_By_IdArea_and_IdBodega(area, pIdPedidoEnc.IdBodega)

                                        lBodegas.Add(bodegaArea.Descripcion)

                                    Next

                                End If


                            End If

                        End If

                    Else

                    End If


                    Dim vCadenaAreas As String = ""

                    If Not lBodegas Is Nothing Then

                        If lBodegas.Count > 0 Then

                            For Each OpInTrans In lBodegas
                                vCadenaAreas += OpInTrans & vbNewLine
                            Next

                        End If

                    End If

                    If Not String.IsNullOrEmpty(vCadenaAreas) Then
                        info = New ToolTipControlInfo(o, vCadenaAreas)
                    End If

                End If

            End If

            If info IsNot Nothing Then e.Info = info

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

End Class