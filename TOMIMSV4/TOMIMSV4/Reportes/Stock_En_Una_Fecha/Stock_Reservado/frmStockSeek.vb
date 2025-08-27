Imports DevExpress.XtraEditors

Public Class frmStockSeek

    Public pBeStock As New clsBeVW_stock_res
    Public Property CantidadDiferencia As Double = 0

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

            lblIdStockRes.Text = pBeStock.IdStockRes
            lblIdStock.Text = pBeStock.IdStock
            lblPicking.Text = pBeStock.IdPicking
            lblPedido.Text = pBeStock.IdPedido
            txtCodigo.Text = pBeStock.Codigo_Producto
            txtProducto.Text = pBeStock.Nombre_Producto
            txtUMbas.Text = pBeStock.UMBas
            txtPresentacion.Text = pBeStock.Nombre_Presentacion
            txtEstadoProd.Text = pBeStock.NomEstado
            txtCantFisica.Text = pBeStock.CantidadUmBas
            txtCantidad.Text = pBeStock.CantidadUmBas
            txtPropietario.Text = pBeStock.Propietario
            txtUbicaAct.Text = pBeStock.IdUbicacion
            txtUbicAnt.Text = pBeStock.IdUbicacion_Anterior
            txtLote.Text = pBeStock.Lote
            txtLic_plate.Text = pBeStock.Lic_plate
            txtFechaIng.EditValue = pBeStock.Fecha_ingreso
            txtFechaVence.EditValue = pBeStock.Fecha_Vence
            txtRef.Text = pBeStock.Ubicacion_Nombre

            lblDiferencia.Text = "Diferencia: " & CantidadDiferencia

            If Val(CantidadDiferencia) <> 0 Then
                txtCantidad.EditValue = pBeStock.CantidadUmBas - CantidadDiferencia
            End If

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

            If XtraMessageBox.Show(String.Format("¿Está seguro de eliminar el stock {0} ?", pBeStock.IdStock), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Eliminar = (clsLnStock.Eliminar_By_IdStock(pBeStock.IdStock) > 0)

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            Dim Stock As New clsBeStock_res

            If XtraMessageBox.Show(String.Format("¿Está seguro de actualizar el stock {0} ?", pBeStock.IdStock), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim BeStock As New clsBeStock
                BeStock.IdStock = pBeStock.IdStock
                BeStock.Cantidad = txtCantidad.EditValue

                Actualizar = (clsLnStock.Actualiza_Cantidad_Y_Peso(BeStock) > 0)

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminar.ItemClick

        Try

            If XtraMessageBox.Show(String.Format("¿Está seguro de eliminar el Id Stock {0}?", pBeStock.IdStock), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Eliminar() Then
                    XtraMessageBox.Show("Se eliminó el stock", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    XtraMessageBox.Show("No se eliminó el stock", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub frmStockSeek_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cargar_Datos_StockReservado()
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            If XtraMessageBox.Show(String.Format("¿Está seguro de actualizar el Id Stock {0}?", pBeStock.IdStock), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Actualizar() Then
                    XtraMessageBox.Show("Se actualizó el stock", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    XtraMessageBox.Show("No se eliminó el stock", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

End Class