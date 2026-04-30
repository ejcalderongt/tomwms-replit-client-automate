Imports DevExpress.XtraEditors
Imports TOMWMS.frmOrdenCompra

Public Class frmDocIngresoRFID_List

    Private listaIngresoRFID As New List(Of clsBeI_nav_barras_rfid_enc)
    Private BeIngresoRFID As New clsBeI_nav_barras_rfid_enc
    Public Property Modo As pModo

    Public Sub New()
        InitializeComponent()
        'IsLoading = False
    End Sub

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmDocIngresoRFID_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            Cargar_Lista_Ingresos()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cargar_Lista_Ingresos()

        Dim listaIngresoRFID As DataTable

        Try

            listaIngresoRFID = clsLnI_nav_barras_rfid_enc.Get_All_Ingresos()

            If listaIngresoRFID IsNot Nothing AndAlso listaIngresoRFID.Rows.Count > 0 Then

                Dgrid.DataSource = listaIngresoRFID

                If GridView1.Columns.Count > 0 Then
                    GridView1.Columns("IdRFIDEnc").Caption = "Correlativo"
                    GridView1.Columns("IdOrdenCompraEnc").Caption = "IdIngreso"

                    If GridView1.Columns("fec_agr") IsNot Nothing Then
                        GridView1.Columns("fec_agr").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        GridView1.Columns("fec_agr").DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
                    End If

                    If GridView1.Columns("fec_mod") IsNot Nothing Then
                        GridView1.Columns("fec_mod").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        GridView1.Columns("fec_mod").DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
                    End If

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

    Private Sub cmdCargar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdCargar.ItemClick
        Try
            Cargar_Lista_Ingresos()
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
                'Dim idOrdenCompraEncObj = GridView1.GetFocusedRowCellValue("IdOrdenCompraEnc")

                If idRFIDEncObj IsNot Nothing AndAlso idRFIDEncObj IsNot DBNull.Value Then

                    BeIngresoRFID = New clsBeI_nav_barras_rfid_enc
                    BeIngresoRFID.IdRFIDEnc = Convert.ToInt32(idRFIDEncObj)

                    'If idOrdenCompraEncObj IsNot Nothing AndAlso idOrdenCompraEncObj IsNot DBNull.Value Then
                    '    BeIngresoRFID.IdOrdenCompraEnc = Convert.ToInt32(idOrdenCompraEncObj)
                    'End If

                    clsLnI_nav_barras_rfid_enc.GetSingle(BeIngresoRFID)

                    Dim lSelectionIndex As Integer = GridView1.FocusedRowHandle

                    If Modo = pModo.Lista Then

                        Cierra_Instancia_Previa(frmDocIngresoRFID)

                        clsLnLog_error_wms.Agregar_Error("ADVERTENCIA_202302231652: El IdUsuario: " & AP.UsuarioAp.IdUsuario & " abrió el IdRFIDEnc: " & BeIngresoRFID.IdRFIDEnc)

                        With frmDocIngresoRFID
                            .Modo = frmDocIngresoRFID.ModoTrans.Editar
                            .gBeRFIDEnc = BeIngresoRFID
                            .InvokeListarIngresosRFID = AddressOf Cargar_Lista_Ingresos
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