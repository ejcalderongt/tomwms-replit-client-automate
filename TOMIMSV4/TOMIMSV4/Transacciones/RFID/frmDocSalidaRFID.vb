Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports DocumentFormat.OpenXml.Drawing
Imports Microsoft.Office.Interop.Excel

Public Class frmDocSalidaRFID

    Public Enum ModoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As ModoTrans
    Public gBeRFIDEnc As New clsBeI_nav_barras_rfid_enc
    Public Delegate Sub Listar_Salidas_RFID()
    Public Property InvokeListarSalidasRFID As Listar_Salidas_RFID

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmDocSalidaRFID_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Documento de Salida RFID...")

            IMS.Listar_Clientes(cmbCliente)

            Select Case Modo

                Case ModoTrans.Nuevo

                Case ModoTrans.Editar
                    Cargar_Datos()
            End Select

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
            Focus()
        End Try
    End Sub

    Private Sub Cargar_Datos()
        Dim BeCliente As New clsBeCliente
        Try

            If gBeRFIDEnc IsNot Nothing Then

                If gBeRFIDEnc.IdCliente > 0 Then
                    BeCliente = clsLnCliente.GetSingle(gBeRFIDEnc.IdCliente)
                End If

                If BeCliente IsNot Nothing Then
                    cmbCliente.EditValue = BeCliente.IdCliente
                End If

                txtIdRFIDEnc.Text = gBeRFIDEnc.IdRFIDEnc
                txtIdPedido.Text = gBeRFIDEnc.IdPedidoEnc
                txtEstado.Text = gBeRFIDEnc.Estado
                txtTipo.Text = gBeRFIDEnc.Tipo
                txtFechaAgr.Text = gBeRFIDEnc.Fec_agr

                If gBeRFIDEnc.Detalle.Count > 0 Then
                    grdDetalle.DataSource = gBeRFIDEnc.Detalle

                    If GridView1.Columns.Count > 0 Then
                        GridView1.OptionsView.ColumnAutoWidth = False
                        GridView1.BestFitColumns()
                        lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Try
            If Actualizar() Then
                XtraMessageBox.Show("Salida actualizada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                If Not InvokeListarSalidasRFID Is Nothing Then InvokeListarSalidasRFID.Invoke()
                Close()

            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Actualizar() As Boolean
        Try

            If cmbCliente.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione un Proveedor.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            gBeRFIDEnc.IdCliente = cmbCliente.EditValue
            gBeRFIDEnc.Fec_mod = Now

            Return clsLnI_nav_barras_rfid_enc.Actualizar_Encabezado(gBeRFIDEnc) = 1

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
            Return False
        End Try
    End Function
End Class