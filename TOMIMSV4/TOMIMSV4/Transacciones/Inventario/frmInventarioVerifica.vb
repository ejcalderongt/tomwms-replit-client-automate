Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmInventarioVerifica

    Public gBeTransInvVer As New clsBeTrans_inv_resumen

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Private Sub frmInventarioVerifica_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            IMS.Listar_TramosInventario(cmbTramo)
            IMS.Listar_Operadores(cmbOperador)
            IMS.Listar_Unidad_Medida(cmbUM)
            IMS.Listar_ProductoEstado(cmbPEstado)

            Select Case Modo

                Case TipoTrans.Nuevo

                Case TipoTrans.Editar
                    cargarDatos()

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub mnuSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

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

    Private Sub cargarDatos()

        Try

            IMS.Listar_Presentaciones(cmbPresentacion, gBeTransInvVer.Idproducto)
            lblCod.Text = gBeTransInvVer.Idinventariores
            lblCodInv.Text = gBeTransInvVer.Idinventarioenct
            cmbUM.EditValue = gBeTransInvVer.IdUnidadMedida
            cmbOperador.EditValue = gBeTransInvVer.Idoperador
            txtProducto.Text = gBeTransInvVer.Nom_producto
            cmbTramo.EditValue = gBeTransInvVer.Idtramo
            cmbPEstado.EditValue = gBeTransInvVer.Idproductoestado
            cmbPresentacion.EditValue = gBeTransInvVer.Idpresentacion
            txtCantidad.Value = gBeTransInvVer.Cantidad

            Dim Ubicacion As New clsBeBodega_ubicacion
            Ubicacion = clsLnBodega_ubicacion.GetSingle(gBeTransInvVer.IdUbicacion, AP.IdBodega)

            If Not Ubicacion Is Nothing Then
                txtUbicacion.Text = Ubicacion.NombreCompleto
            End If

            txtIdUbicacion.Text = gBeTransInvVer.IdUbicacion
            Ubicacion_Es_Valida()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try
            '#MA20251229 
            If cmbPresentacion.EditValue Is Nothing Then
                gBeTransInvVer.Idpresentacion = 0
            Else
                gBeTransInvVer.Idpresentacion = CInt(cmbPresentacion.EditValue)
            End If

            If cmbOperador.EditValue IsNot Nothing Then
                gBeTransInvVer.Idoperador = CInt(cmbOperador.EditValue)
            End If

            gBeTransInvVer.Idpresentacion = cmbPresentacion.EditValue
            gBeTransInvVer.Fecha_captura = Now
            gBeTransInvVer.Cantidad = txtCantidad.Value
            gBeTransInvVer.IdUbicacion = txtIdUbicacion.Text

            clsLnTrans_inv_resumen.Actualizar(gBeTransInvVer)

            Actualizar = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Eliminar() As Boolean

        Eliminar = False

        Try

            clsLnTrans_inv_resumen.Eliminar(gBeTransInvVer)

            Eliminar = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub txtIdUbicacion_Validating(sender As Object, e As CancelEventArgs) Handles txtIdUbicacion.Validating
        Ubicacion_Es_Valida()
    End Sub

    Private Function Ubicacion_Es_Valida() As Boolean
        Try

            Dim BeBodegaUbicacion = clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(txtIdUbicacion.Text, gBeTransInvVer.IdBodega)
            If Not BeBodegaUbicacion Is Nothing Then
                txtNomUbicacion.Text = BeBodegaUbicacion.Descripcion
            End If
        Catch ex As Exception

        End Try
    End Function

    '#MA20251230 
    Private Sub cmbPresentacion_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbPresentacion.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then
            cmbPresentacion.EditValue = Nothing
            e.Handled = True
        End If
    End Sub
End Class