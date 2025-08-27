Imports DevExpress.XtraEditors

Public Class frmReconteo
    Public Regulariza As Boolean = False
    Public gBeReconteoDet As New clsBeTrans_inv_reconteo
    Private ListReconteoDet As New List(Of clsBeTrans_inv_reconteo)

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

    Private Sub frmReconteo_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Select Case Modo

                Case TipoTrans.Nuevo

                    Cargar_Datos()

                Case TipoTrans.Editar

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Cargar_Datos()

        Try

            If Regulariza Then
                cmdActualizar.Enabled = False
            End If

            Dim Producto As New clsBeProducto
            Dim Presentacion As New clsBeProducto_Presentacion
            Dim Ubicacion As New clsBeBodega_ubicacion
            Dim vCantidad As Double = 0.0
            Dim vPeso As Double = 0.0
            Dim Conver As Double = 0.0

            lblIdStock.Text = gBeReconteoDet.IdStock
            lblcodenc.Text = gBeReconteoDet.Idreconteo

            Producto = clsLnProducto.Get_Single_BeProducto_By_IdProductoBodega(gBeReconteoDet.IdProductoBodega)
            txtProducto.Text = Producto.Nombre

            cmbEstadoProd.Text = clsLnProducto_estado.Get_Nombre_By_IdEstado(gBeReconteoDet.IdProductoEstado)

            IMS.Listar_ProductoEstado(cmbEstadoProd)
            cmbEstadoProd.EditValue = gBeReconteoDet.IdProductoEstado

            If gBeReconteoDet.IdPresentacion > 0 Then

                Presentacion.IdPresentacion = gBeReconteoDet.IdPresentacion
                clsLnProducto_presentacion.GetSingle(Presentacion)
                IMS.Listar_Presentaciones(cmbPresentacion, Producto.IdProducto)
                cmbPresentacion.EditValue = gBeReconteoDet.IdPresentacion

                If Presentacion.EsPallet Then
                    Conver = (Presentacion.CajasPorCama * Presentacion.CamasPorTarima) * Presentacion.Factor
                    vCantidad = (gBeReconteoDet.Cantidad / Conver)
                Else
                    vCantidad = (gBeReconteoDet.Cantidad / Presentacion.Factor)
                End If
            Else
                vCantidad = gBeReconteoDet.Cantidad
            End If

            If gBeReconteoDet.IdUbicacion > 0 Then
                Ubicacion.IdUbicacion = gBeReconteoDet.IdUbicacion
                clsLnBodega_ubicacion.Obtener(Ubicacion)
                txtUbicacion.Text = Ubicacion.NombreCompleto
            End If

            dtFecha_Vence.EditValue = gBeReconteoDet.Fecha_vence
            txtLote.Text = gBeReconteoDet.Lote
            txtCantidad.Value = vCantidad
            txtPeso.Value = gBeReconteoDet.Peso

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Dim Presentacion As New clsBeProducto_Presentacion
        Dim vCantidad As Double = 0.0
        Dim Conver As Double = 0.0

        Try

            ListReconteoDet = clsLnTrans_inv_reconteo.GetAllByStockUbic(gBeReconteoDet.Idinventarioenc, gBeReconteoDet.IdStock, gBeReconteoDet.IdUbicacion, gBeReconteoDet.Idreconteo)

            If ListReconteoDet.Count > 0 Then

                For Each Obj As clsBeTrans_inv_reconteo In ListReconteoDet

                    Obj.IdProductoEstado = cmbEstadoProd.EditValue
                    Obj.IdPresentacion = cmbPresentacion.EditValue
                    Obj.Fecha_vence = dtFecha_Vence.EditValue
                    Obj.Lote = txtLote.Text
                    Obj.Cantidad = txtCantidad.Value
                    Obj.Peso = txtPeso.Value

                    If Obj.IdPresentacion > 0 Then

                        Presentacion.IdPresentacion = Obj.IdPresentacion
                        clsLnProducto_presentacion.GetSingle(Presentacion)

                        If Presentacion.EsPallet Then
                            Conver = (Presentacion.CajasPorCama * Presentacion.CamasPorTarima) * Presentacion.Factor
                            vCantidad = Conver * Obj.Cantidad
                        Else
                            vCantidad = Presentacion.Factor * Obj.Cantidad
                        End If

                        Obj.Cantidad = vCantidad

                    End If


                    clsLnTrans_inv_reconteo.Actualizar(Obj)

                Next

                Actualizar = True

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Reconteo Actualizado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If
    End Sub
End Class