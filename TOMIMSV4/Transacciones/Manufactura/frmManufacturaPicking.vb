Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmManufacturaPicking
    Public Property pBeManufacturaDet As New clsBeTrans_manufactura_det()
    Public Property pBeManufacturaEnc As New clsBeTrans_manufactura_enc
    Public Property pBePedidoEnc As New clsBeTrans_pe_enc()
    Public pPedidoDet As New clsBeTrans_pe_det()

    Private contador As Integer = 0

    Private Sub frmManufacturaPicking_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Cargando...")

            Cargar_Datos()

            SplashScreenManager.CloseForm(False)
        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Cargar_Datos()

        Try

            pPedidoDet = New clsBeTrans_pe_det
            pPedidoDet.IdPedidoDet = pBeManufacturaDet.IdPedidoDet
            clsLnTrans_pe_det.GetSingle(pPedidoDet)

            txtPedido.Text = pBePedidoEnc.IdPedidoEnc
            txtReferenciaSAP.Text = pBePedidoEnc.Referencia

            Dim pCliente As New clsBeCliente
            pCliente = clsLnCliente.GetSingle(pBePedidoEnc.IdCliente)
            If pCliente IsNot Nothing Then
                txtCliente.Text = pCliente.Nombre_comercial
            End If

            txtCodigoProducto.Text = pBeManufacturaDet.Codigo_producto
            txtDescripcionProducto.Text = pBeManufacturaDet.Nombre_producto
            txtBarraProducto.Text = ""
            txtCantidadEsperada.Text = pBeManufacturaDet.Cantidad_esperada
            txtCantidadRegistrada.Text = pBeManufacturaDet.Cantidad_recibida
            contador = pBeManufacturaDet.Cantidad_recibida

            Mostrar_Manufactura_Picking()

            txtCantidad.Enabled = IIf(pBeManufacturaEnc.Escaneo, 0, 1)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Mostrar_Manufactura_Picking()

        Try

            Dim DT As New DataTable
            DT = clsLnTrans_manufactura_picking.Listar(pBeManufacturaEnc.IdManufacturaEnc, pBeManufacturaDet.IdManufacturaDet)

            If DT IsNot Nothing Then

                If DT.Rows.Count > 0 Then

                    GridControl1.DataSource = DT
                    GridView1.Columns("IdManufacturaEnc").Visible = False
                    GridView1.Columns("IdManufacturaDet").Visible = False
                    GridView1.Columns("IdPedidoDet").Visible = False
                    GridView1.Columns("IdProductoBodega").Visible = False
                    GridView1.Columns("licencia_manufactura").Visible = False
                    GridView1.Columns("user_agr").Visible = False
                    GridView1.Columns("user_mod").Visible = False
                    GridView1.Columns("fec_agr").Visible = False
                    GridView1.Columns("fec_mod").Visible = False
                    GridView1.Columns("IdManufacturaPicking").Caption = "Correlativo"
                    GridView1.Columns("codigo_barra").Caption = "Código Barra"
                    GridView1.Columns("licencia").Caption = "Licencia"

                    GridView1.OptionsView.ShowFooter = True

                    lblRegistros.Caption = "Registros:" & DT.Rows.Count

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                      Text,
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error)
        End Try
    End Sub

End Class