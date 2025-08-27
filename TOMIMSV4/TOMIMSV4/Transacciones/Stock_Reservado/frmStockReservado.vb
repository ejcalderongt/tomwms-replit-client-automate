Imports DevExpress.XtraEditors

Public Class frmStockReservado

    Public pBeStockRes As New clsBeVW_Stock_Res_Pedido

    Public Modo As TipoTrans
    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Cargar_Datos_StockReservado()

        Try

            lblIdStockRes.Text = pBeStockRes.IdStockRes
            lblIdStock.Text = pBeStockRes.IdStock
            lblIdRece.Text = pBeStockRes.IdRecepcion
            lblPicking.Text = pBeStockRes.IdPicking
            lblPedido.Text = pBeStockRes.IdPedido
            lblDespacho.Text = pBeStockRes.IdDespacho
            lblEstadoRes.Text = pBeStockRes.Estado
            txtIdTransaccion.Text = pBeStockRes.IdTransaccion
            txtCodigo.Text = pBeStockRes.Codigo
            txtProducto.Text = pBeStockRes.Nombre
            txtUMbas.Text = pBeStockRes.Unidadmedida
            txtPresentacion.Text = pBeStockRes.Presentacion
            txtEstadoProd.Text = pBeStockRes.NomEstado
            txtCantFisica.Text = pBeStockRes.Cantidadfisica
            txtCantidad.Text = pBeStockRes.Cantidad
            txtPropietario.Text = pBeStockRes.Propietario
            txtUbicaAct.Text = pBeStockRes.Bodegaubicacion
            txtUbicAnt.Text = pBeStockRes.Ubicacion_ant
            txtLote.Text = pBeStockRes.Lote
            txtLic_plate.Text = pBeStockRes.Lic_plate
            txtFechaIng.EditValue = pBeStockRes.Fecha_ingreso
            txtFechaVence.EditValue = pBeStockRes.Fecha_vence
            txtHost.Text = pBeStockRes.Host
            txtRef.Text = pBeStockRes.Referencia

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Function Eliminar() As Boolean

        Eliminar = False

        Try

            Dim Stock As New clsBeStock_res

            If Not pBeStockRes.Estado.ToUpper = "UNCOMMITED" Then

                If XtraMessageBox.Show(String.Format("¿Está seguro de eliminar el stock reservado {0} aunque su estado sea pickeado o verificado?", pBeStockRes.IdStockRes), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Stock.IdStockRes = pBeStockRes.IdStockRes

                    Dim vIdPedidoDet As Integer = 0
                    Dim vIdPedidoEnc As Integer = 0
                    Dim vIdPickingEnc As Integer = 0

                    '#EJC20210830: Validación de Picking asociado al stock reservado antes de liberarlo y también liberarlo de los procesos de picking.
                    If clsLnStock_res.GetSingle(Stock) Then

                        vIdPedidoDet = Stock.IdPedidoDet
                        vIdPedidoEnc = Stock.IdPedido
                        vIdPickingEnc = Stock.IdPicking

                        If vIdPickingEnc <> 0 Then

                            If Not (clsLnTrans_picking_det.Liberar_Producto_No_Pickeado(vIdPedidoDet,
                                                                                        vIdPedidoEnc,
                                                                                        vIdPickingEnc,
                                                                                        AP.UsuarioAp.IdUsuario,
                                                                                        txtRef.Text,
                                                                                        "frmstockreservado_eliminar",
                                                                                        pBeStockRes.IdBodega,
                                                                                        Stock.IdStockRes)) Then

                                XtraMessageBox.Show("No se pudo liberar el producto del picking", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                            Else
                                Eliminar = True
                                'XtraMessageBox.Show("Se ha liberado la reserva y el producto de la tarea de picking", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If

                        Else
                            Eliminar = (clsLnStock_res.Eliminar(Stock) > 0)
                        End If

                    Else
                        XtraMessageBox.Show("No se obtuvo el registro de reserva, no se puede liberar el stock", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                End If

            Else

                Stock.IdStockRes = pBeStockRes.IdStockRes
                Eliminar = (clsLnStock_res.Eliminar(Stock) > 0)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminar.ItemClick

        Try

            If XtraMessageBox.Show(String.Format("¿Está seguro de eliminar el Id Stock Reservado {0}?", pBeStockRes.IdStockRes), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Eliminar() Then
                    XtraMessageBox.Show("Se eliminó el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    XtraMessageBox.Show("No se eliminó el stock reservado", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmStockReservado_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cargar_Datos_StockReservado()
    End Sub

End Class