Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports TOMWMS.frmOrdenCompra

Public Class frmDocIngresoRFID

    Public Enum ModoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As ModoTrans
    Public gBeRFIDEnc As New clsBeI_nav_barras_rfid_enc
    Public Delegate Sub Listar_Ingresos_RFID()
    Public Property InvokeListarIngresosRFID As Listar_Ingresos_RFID

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmDocIngresoRFID_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            'IsLoading = True

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Documento de Ingreso RFID...")

            '#GT15042026: carga de lookupedit, no combobox de forms
            IMS.Listar_Proveedor(cmbProveedor)


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

        Dim BeProveedor As New clsBeProveedor
        Dim dtDetalle As DataTable

        Try

            If gBeRFIDEnc IsNot Nothing Then

                If gBeRFIDEnc.IdProveedor > 0 Then
                    BeProveedor = clsLnProveedor.GetSingle(gBeRFIDEnc.IdProveedor)
                End If

                If BeProveedor IsNot Nothing Then
                    cmbProveedor.EditValue = BeProveedor.IdProveedor
                End If

                txtIdRFIDEnc.Text = gBeRFIDEnc.IdRFIDEnc
                txtIdOrdenCompraEnc.Text = gBeRFIDEnc.IdOrdenCompraEnc
                txtEstado.Text = gBeRFIDEnc.Estado
                txtTipo.Text = gBeRFIDEnc.Tipo
                txtFechaAgr.Text = Format(gBeRFIDEnc.Fec_agr, "dd/MM/yyyy HH:mm:ss")

                If gBeRFIDEnc.Detalle IsNot Nothing AndAlso gBeRFIDEnc.Detalle.Count > 0 Then

                    dtDetalle = New DataTable
                    dtDetalle.Columns.Add("IdRFIDEnc", GetType(Integer))
                    dtDetalle.Columns.Add("Barra_epc", GetType(String))
                    dtDetalle.Columns.Add("Tagid", GetType(String))
                    dtDetalle.Columns.Add("IdDispositivo", GetType(String))
                    dtDetalle.Columns.Add("IdOperador", GetType(Integer))
                    dtDetalle.Columns.Add("Operador", GetType(String))
                    dtDetalle.Columns.Add("Producto", GetType(String))

                    For Each det As clsBeI_nav_barras_rfid_det In gBeRFIDEnc.Detalle

                        Dim nombreOperador As String = ""
                        Dim nombreProducto As String = ""

                        If det.Operador IsNot Nothing Then
                            nombreOperador = Trim(det.Operador.Nombres & " " & det.Operador.Apellidos)
                        End If

                        If det.Producto IsNot Nothing Then
                            nombreProducto = Trim(det.Producto.Codigo & " " & det.Producto.Nombre)
                        End If

                        dtDetalle.Rows.Add(
                            det.IdRFIDEnc,
                            det.Barra_epc,
                            det.Tagid,
                            det.IdDispositivo,
                            det.IdOperador,
                            nombreOperador,
                            nombreProducto
                        )
                    Next

                    grdDetalle.DataSource = dtDetalle

                    If GridView1.Columns.Count > 0 Then
                        GridView1.Columns("IdRFIDEnc").Caption = "Id RFID Enc"
                        GridView1.Columns("Barra_epc").Caption = "Barra EPC"
                        GridView1.Columns("Tagid").Caption = "Tag ID"
                        GridView1.Columns("IdDispositivo").Caption = "Id Dispositivo"
                        GridView1.Columns("IdOperador").Caption = "Id Operador"
                        GridView1.Columns("Operador").Caption = "Operador"
                        GridView1.Columns("Producto").Caption = "Producto"

                        GridView1.OptionsView.ColumnAutoWidth = False
                        GridView1.BestFitColumns()
                        lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    End If

                Else
                    grdDetalle.DataSource = Nothing
                    lblRegs.Caption = String.Format("Registros: {0}", 0)
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
                XtraMessageBox.Show("Ingreso actualizado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                If Not InvokeListarIngresosRFID Is Nothing Then InvokeListarIngresosRFID.Invoke()
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

            If cmbProveedor.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione un Proveedor.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            gBeRFIDEnc.IdProveedor = cmbProveedor.EditValue
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