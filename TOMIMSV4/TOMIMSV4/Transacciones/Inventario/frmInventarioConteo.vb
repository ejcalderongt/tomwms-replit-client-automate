Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmInventarioConteo

    Public gBeTransInvConteo As New clsBeTrans_inv_detalle

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Private CargandoDatos As Boolean = False
    Private PresentacionBorrada As Boolean = False

    Private Sub frmInventarioConteo_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            IMS.Listar_TramosInventario(cmbTramo)
            IMS.Listar_Operadores(cmbOperador)
            IMS.Listar_Unidad_Medida(cmbUM)
            IMS.Listar_ProductoEstado(cmbPEstado)
            IMS.Listar_Productos(cmbProducto, AP.IdBodega)

            Select Case Modo

                Case TipoTrans.Nuevo

                Case TipoTrans.Editar
                    cargarDatos()
            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cargarDatos()

        Try
            CargandoDatos = True
            Dim Ubicacion As New clsBeBodega_ubicacion
            Ubicacion = clsLnBodega_ubicacion.GetSingle(gBeTransInvConteo.IdUbicacion, AP.IdBodega)
            PresentacionBorrada = (gBeTransInvConteo.IdPresentacion = 0)

            IMS.Listar_Presentaciones(cmbPresentacion, gBeTransInvConteo.Idproducto)

            If gBeTransInvConteo.IdPresentacion > 0 Then
                cmbPresentacion.EditValue = gBeTransInvConteo.IdPresentacion
            Else
                cmbPresentacion.EditValue = Nothing
                cmbPresentacion.Text = ""
            End If

            lblCod.Text = gBeTransInvConteo.Idinventariodet
            lblCodInv.Text = gBeTransInvConteo.Idinventarioenc
            cmbTramo.EditValue = gBeTransInvConteo.Idtramo
            txtUbicacion.Text = Ubicacion.NombreCompleto
            cmbOperador.EditValue = gBeTransInvConteo.Idoperador
            ' txtProducto.Text = gBeTransInvConteo.Nom_producto
            cmbUM.EditValue = gBeTransInvConteo.Idunidadmedida
            cmbPEstado.EditValue = gBeTransInvConteo.Idproductoestado
            txtLote.Text = gBeTransInvConteo.Lote
            dtFechaVence.EditValue = gBeTransInvConteo.Fecha_vence
            txtCantidad.Value = gBeTransInvConteo.Cantidad
            cmbProducto.EditValue = gBeTransInvConteo.Idproducto
            cmbProducto.Tag = gBeTransInvConteo.Idproducto
            txtIdUbicacion.Text = gBeTransInvConteo.IdUbicacion

            If gBeTransInvConteo.Idproducto > 0 Then
                Dim producto As clsBeProducto =
                clsLnProducto.Get_Single_By_IdProducto(gBeTransInvConteo.Idproducto)

                If producto IsNot Nothing Then
                    txtProducto.Text = producto.Nombre
                End If
            End If
            Ubicacion_Es_Valida()


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            CargandoDatos = False
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show("Se actualizó el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        Else
            XtraMessageBox.Show("No se pudo actualizar el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try
            '#MA20251229 
            If cmbPresentacion.EditValue Is Nothing Then
                gBeTransInvConteo.IdPresentacion = 0
            Else
                gBeTransInvConteo.IdPresentacion = CInt(cmbPresentacion.EditValue)
            End If

            gBeTransInvConteo.IdPresentacion = cmbPresentacion.EditValue
            gBeTransInvConteo.Idunidadmedida = cmbUM.EditValue
            gBeTransInvConteo.Lote = txtLote.Text.Trim
            gBeTransInvConteo.Fecha_vence = dtFechaVence.EditValue
            gBeTransInvConteo.Idproductoestado = cmbPEstado.EditValue
            gBeTransInvConteo.Cantidad = Math.Round(txtCantidad.Value, 6)
            gBeTransInvConteo.Fecha_captura = Now
            gBeTransInvConteo.Nom_producto = txtProducto.Text
            gBeTransInvConteo.Idproducto = cmbProducto.EditValue
            gBeTransInvConteo.IdUbicacion = txtIdUbicacion.Text
            clsLnTrans_inv_detalle.Actualizar(gBeTransInvConteo)

            Actualizar = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Eliminar Inventario?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Eliminar() Then
                    XtraMessageBox.Show("Se eliminó el Inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                Else
                    XtraMessageBox.Show("No se logró eliminar el Inventario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

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
        clsLnTrans_inv_detalle.Eliminar(gBeTransInvConteo)
        Eliminar = True
    End Function

    Private Sub cmdVerificar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdVerificar.ItemClick

        Dim gBeTransInvVer As New clsBeTrans_inv_resumen

        Try

            gBeTransInvVer.Idinventariores = clsLnTrans_inv_resumen.MaxID()
            gBeTransInvVer.Idinventarioenct = gBeTransInvConteo.Idinventarioenc
            gBeTransInvVer.Idtramo = gBeTransInvConteo.Idtramo
            gBeTransInvVer.Idproducto = gBeTransInvConteo.Idproducto
            gBeTransInvVer.Host = AP.HostName
            gBeTransInvVer.Idoperador = gBeTransInvConteo.Idoperador
            gBeTransInvVer.Nom_operador = gBeTransInvConteo.Nom_operador
            gBeTransInvVer.IdUbicacion = gBeTransInvConteo.IdUbicacion
            gBeTransInvVer.IdBodega = gBeTransInvConteo.IdBodega
            gBeTransInvConteo.IdBodega = AP.IdBodega
            gBeTransInvVer.Idpresentacion = cmbPresentacion.EditValue
            gBeTransInvVer.IdUnidadMedida = cmbUM.EditValue
            gBeTransInvVer.Idproductoestado = cmbPEstado.EditValue
            gBeTransInvVer.Cantidad = Math.Round(txtCantidad.Value, 6)
            gBeTransInvVer.Fecha_captura = Now
            gBeTransInvVer.Nom_producto = txtProducto.Text

            clsLnTrans_inv_resumen.Insertar(gBeTransInvVer)

            '#MA20260102
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show("Inventario verificado correctamente",
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
            Close()

        Catch ex As Exception
            MsgBox("Error " & ex.Message)
        End Try
    End Sub

    Private Sub txtIdUbicacion_TextChanged(sender As Object, e As EventArgs) Handles txtIdUbicacion.TextChanged

    End Sub

    Private Sub txtIdUbicacion_Validating(sender As Object, e As CancelEventArgs) Handles txtIdUbicacion.Validating
        Ubicacion_Es_Valida()
    End Sub

    Private Function Ubicacion_Es_Valida() As Boolean
        Try

            Dim BeBodegaUbicacion = clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(txtIdUbicacion.Text, gBeTransInvConteo.IdBodega)
            If Not BeBodegaUbicacion Is Nothing Then
                txtNomUbicacion.Text = BeBodegaUbicacion.Descripcion
            End If
        Catch ex As Exception

        End Try
    End Function

    Private Sub grpConteo_Paint(sender As Object, e As PaintEventArgs) Handles grpConteo.Paint

    End Sub

    '#MA20251229 
    Private Sub cmbPresentacion_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbPresentacion.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then
            cmbPresentacion.EditValue = Nothing
            gBeTransInvConteo.IdPresentacion = 0
            PresentacionBorrada = True
            e.Handled = True
        End If
    End Sub

    '#MA20251230 Manejo de direccionales y enter al seleccionar
    Private Sub cmbProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProducto.KeyDown

        Dim cmb As DevExpress.XtraEditors.GridLookUpEdit = CType(sender, DevExpress.XtraEditors.GridLookUpEdit)

        If cmb Is Nothing Then Return

        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = cmb.Properties.View

        If e.KeyCode = Keys.Enter Then
            If view.FocusedRowHandle >= 0 Then
                cmb.EditValue = view.GetRowCellValue(view.FocusedRowHandle, "IdProducto")
                cmb.ClosePopup()
            End If

            e.Handled = True
        End If
    End Sub

    '#MA20251230 Actualizar al cambiar producto
    Private Sub cmbProducto_EditValueChanged(sender As Object, e As EventArgs) Handles cmbProducto.EditValueChanged
        Try
            If CargandoDatos Then Return

            Dim cmb As DevExpress.XtraEditors.GridLookUpEdit = CType(sender, DevExpress.XtraEditors.GridLookUpEdit)

            If cmb.EditValue Is Nothing Then
                txtProducto.Text = ""
                cmbPresentacion.EditValue = Nothing
                cmbUM.EditValue = Nothing
                Return
            End If

            Dim IdProducto As Integer = CInt(cmb.EditValue)
            Dim producto As clsBeProducto = clsLnProducto.Get_Single_By_IdProducto(IdProducto)

            If producto IsNot Nothing Then
                txtProducto.Text = producto.Nombre

                If producto.UnidadMedida IsNot Nothing Then
                    cmbUM.EditValue = producto.UnidadMedida.IdUnidadMedida
                Else
                    cmbUM.EditValue = Nothing
                End If

                IMS.Listar_Presentaciones(cmbPresentacion, producto.IdProducto)

                If Not PresentacionBorrada Then
                    If cmbPresentacion.EditValue Is Nothing Then
                        If producto.Presentacion IsNot Nothing AndAlso producto.Presentacion.IdPresentacion > 0 Then
                            cmbPresentacion.EditValue = producto.Presentacion.IdPresentacion
                        ElseIf producto.Presentaciones.Count > 0 Then
                            cmbPresentacion.EditValue = producto.Presentaciones(0).IdPresentacion
                        Else
                            cmbPresentacion.EditValue = Nothing
                        End If
                    End If
                End If

            Else
                txtProducto.Text = ""
                cmbPresentacion.EditValue = Nothing
                cmbUM.EditValue = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error al cambiar producto: " & ex.Message)
        End Try
    End Sub
End Class