Imports DevExpress.XtraEditors

Public Class frmCantidadUbicacion

    Public pListObjDet As New clsBeTrans_ubic_hh_det
    Public objStockRef As New clsBeVW_stock_res

    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub frmCantidadUbicacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            If Not IMS.Listar_Operadores(cmbOperador) Then
                XtraMessageBox.Show("No hay operadores definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If Not objStockRef Is Nothing Then


                txtIdStock.Text = objStockRef.Codigo_Producto
                txtIdStock.Tag = objStockRef.IdStock
                txtIdOrigen.Text = objStockRef.IdUbicacion
                txtProducto.Text = objStockRef.Nombre_Producto
                txtVence.Text = objStockRef.Fecha_Vence
                txtEstado.Text = objStockRef.NomEstado
                txtSerie.Text = objStockRef.Serial
                txtUnidadMedida.Text = objStockRef.UMBas
                txtAñada.Text = objStockRef.Añada
                txtPresentacion.Text = objStockRef.Nombre_Presentacion
                txtIngreso.Text = objStockRef.Fecha_ingreso
                txtLote.Text = objStockRef.Lote
                Dim vCantidadReservada As Double = clsLnStock.Get_Cantidad_Reservada(objStockRef.IdStock)
                txtCantidad.Value = objStockRef.CantidadPresentacion - vCantidadReservada
                txtCantidad.Maximum = objStockRef.CantidadPresentacion - vCantidadReservada
                txtCantidad.Minimum = 1
                lblCantRef.Text = txtCantidad.Value
                lblFactor.Text = objStockRef.Factor
                txtIdEstado.Text = objStockRef.IdProductoEstado
                txtIdEstado_LostFocus(Nothing, Nothing)

                Dim pDimensionProducoSeleccionado As Double = txtCantidad.Value * (objStockRef.AltoUbicacion * objStockRef.LargoUbicacion * objStockRef.AnchoUbicacion)

                lblVolumen.Text = String.Format("{0} * ({1} x {2} x {3}) = {4}", Val(lblCantRef.Text), objStockRef.AltoUbicacion, objStockRef.LargoUbicacion, objStockRef.AnchoUbicacion, pDimensionProducoSeleccionado) & " m3"
                txtCantidad.Select(0, txtCantidad.Text.Length)
                txtCantidad.Focus()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub lnkCambioDeEstado_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCambioDeEstado.LinkClicked

        Try

            Dim CambioEstado As New frmProducto_EstadoList() With {.Modo = frmProducto_EstadoList.pModo.Seleccion}

            If CambioEstado.ShowDialog() = DialogResult.OK Then

                If CambioEstado.pObj IsNot Nothing AndAlso CambioEstado.pObj.IdEstado <> 0 Then
                    txtIdEstado.Text = CambioEstado.pObj.IdEstado
                    txtNombreEstado.Text = CambioEstado.pObj.Nombre
                End If

            End If

            CambioEstado.Close()
            CambioEstado.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdEstado_EditValueChanged(sender As Object, e As EventArgs) Handles txtIdEstado.EditValueChanged
        txtNombreEstado.Text = ""
    End Sub

    Private Sub txtIdEstado_LostFocus(sender As Object, e As EventArgs) Handles txtIdEstado.LostFocus

        Try

            If txtIdEstado.Text.Trim <> "" Then

                Dim lGWCFProdEstado As New clsBeProducto_estado()
                lGWCFProdEstado = clsLnProducto_Estado.Get_Single_By_IdEstado(txtIdEstado.Text)
                txtNombreEstado.Text = lGWCFProdEstado.Nombre

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

End Class