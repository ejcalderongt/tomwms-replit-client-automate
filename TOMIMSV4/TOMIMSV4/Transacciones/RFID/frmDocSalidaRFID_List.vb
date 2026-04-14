Imports DevExpress.XtraEditors

Public Class frmDocSalidaRFID_List

    Private listaIngresoRFID As New List(Of clsBeI_nav_barras_rfid_enc)
    Private BeIngresoRFID As New clsBeI_nav_barras_rfid_enc
    Public Property Modo As pModo

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmDocSalidaRFID_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            Cargar_Lista_Pedidos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cargar_Lista_Pedidos()

        'listaIngresoRFID = New List(Of clsBeI_nav_barras_rfid_enc)

        Dim listaIngresoRFID As DataTable

        Try

            listaIngresoRFID = clsLnI_nav_barras_rfid_enc.Get_All_Salidas()

            If listaIngresoRFID IsNot Nothing AndAlso listaIngresoRFID.Rows.Count > 0 Then

                Dgrid.DataSource = listaIngresoRFID

                If GridView1.Columns.Count > 0 Then
                    GridView1.Columns("IdRFIDEnc").Caption = "Correlativo"
                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.BestFitColumns()

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                End If
            Else
                lblRegs.Caption = String.Format("Registros: {0}", 0)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
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

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick
        Procesar_Registro()
    End Sub

    Private Sub Procesar_Registro()

        Try

            If GridView1.RowCount > 0 AndAlso GridView1.FocusedRowHandle >= 0 Then

                Dim idRFIDEncObj = GridView1.GetFocusedRowCellValue("IdRFIDEnc")
                'Dim idPedidoEncObj = GridView1.GetFocusedRowCellValue("IdPedidoEnc")

                If idRFIDEncObj IsNot Nothing AndAlso idRFIDEncObj IsNot DBNull.Value Then

                    BeIngresoRFID = New clsBeI_nav_barras_rfid_enc
                    BeIngresoRFID.IdRFIDEnc = Convert.ToInt32(idRFIDEncObj)

                    'If idPedidoEncObj IsNot Nothing AndAlso idPedidoEncObj IsNot DBNull.Value Then
                    '    BeIngresoRFID.IdPedidoEnc = Convert.ToInt32(idPedidoEncObj)
                    'End If

                    clsLnI_nav_barras_rfid_enc.GetSingle(BeIngresoRFID)

                    Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                    If Modo = pModo.Lista Then

                        Cierra_Instancia_Previa(frmDocSalidaRFID)

                        clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231652: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " abrió el IdRFIDEnc: " & BeIngresoRFID.IdRFIDEnc)

                        With frmDocSalidaRFID
                            .Modo = frmDocSalidaRFID.ModoTrans.Editar
                            .gBeRFIDEnc = BeIngresoRFID
                            .InvokeListarSalidasRFID = AddressOf Cargar_Lista_Pedidos
                            .MdiParent = MdiParent
                            .WindowState = FormWindowState.Normal
                            .Show()
                            .Focus()
                        End With

                    ElseIf Modo = pModo.Seleccion Then
                        DialogResult = DialogResult.OK
                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class