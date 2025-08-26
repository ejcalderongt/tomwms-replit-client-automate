Imports DevExpress.XtraEditors

Public Class frmRegistroInter

    Public pBeINavTransOut As New clsBeI_nav_transacciones_out

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Carga_Datos()

        Dim Presentacion As New clsBeProducto_Presentacion

        Try

            lblCodigo.Text = pBeINavTransOut.Idtransaccion
            txtLinea.Text = pBeINavTransOut.No_linea
            txtCodigo.Text = pBeINavTransOut.Codigo_producto
            txtProducto.Text = pBeINavTransOut.Nombre_producto
            txtUm.Text = pBeINavTransOut.Unidad_medida
            If pBeINavTransOut.Idpresentacion > 0 Then
                Presentacion.IdPresentacion = pBeINavTransOut.Idpresentacion
                clsLnProducto_presentacion.GetSingle(Presentacion)
                txtPresentacion.Text = Presentacion.Nombre
            End If
            txtCantidad.Value = pBeINavTransOut.Cantidad
            txtPeso.Value = pBeINavTransOut.Peso
            dtVence.EditValue = pBeINavTransOut.Fecha_vence
            lblTipoTrans.Text = pBeINavTransOut.Tipo_transaccion
            lblOC.Text = pBeINavTransOut.Idordencompra
            lblRec.Text = pBeINavTransOut.Idrecepcionenc
            lblPed.Text = pBeINavTransOut.Idpedidoenc
            lblDes.Text = pBeINavTransOut.Iddespachoenc
            txtNoPedido.Text = pBeINavTransOut.No_pedido
            txtLote.Text = pBeINavTransOut.Lote
            dtRece.EditValue = pBeINavTransOut.Fecha_recepcion
            chkEnviado.Checked = pBeINavTransOut.Enviado

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        Else
            XtraMessageBox.Show("No fue posible actualizar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            pBeINavTransOut.Cantidad = txtCantidad.Value
            pBeINavTransOut.Lote = txtLote.Text
            pBeINavTransOut.Enviado = chkEnviado.Checked
            pBeINavTransOut.No_linea = txtLinea.Text

            clsLnI_nav_transacciones_out.Actualizar(pBeINavTransOut)

            Actualizar = True

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Private Sub frmRegistroInter_Load(sender As Object, e As EventArgs) Handles Me.Load
        Carga_Datos()
    End Sub
End Class