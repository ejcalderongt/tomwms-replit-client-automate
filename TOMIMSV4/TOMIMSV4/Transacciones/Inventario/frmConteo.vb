Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmConteo
    Public Regulariza As Boolean = False
    Public Cargando As Boolean = False
    Public gBeCiclico As clsBeTrans_inv_ciclico
    Public ListCiclico As New List(Of clsBeTrans_inv_ciclico)
    Private gBeReconteoEnc As New clsBeTrans_inv_enc_reconteo

    Private FechaVenceOriginal As DateTime
    Private IdProductoEstadoOriginal As Integer
    Private IdProductoTallaColorOriginal As Integer
    Private LoteOriginal As String = ""
    Private IdUbicacionOriginal As Integer = 0
    Private EsInvOriginal As Boolean = False
    Private IdUbicacionSeleccionada As Integer = 0

    '#EJC20180801_1118PM:Puntero hacia la fila del del DT asociado al datasource del grid.
    Public DrDetalleInv As DataRow
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

            Cargando = True

            If Regulariza Then
                cmdGuardar.Enabled = False
                cmdEliminar.Enabled = False
            End If

            Dim Producto As New clsBeProducto
            Dim Presentacion As New clsBeProducto_Presentacion
            Dim Ubicacion As New clsBeBodega_ubicacion
            Dim vCantidad As Double = 0.0
            Dim vPeso As Double = 0.0
            Dim Conver As Double = 0.0

            lblCodReconteo.Text = gBeCiclico.IdStock
            lblCodigo.Text = gBeCiclico.Idinventarioenc

            Producto = clsLnProducto.Get_Single_BeProducto_By_IdProductoBodega(gBeCiclico.IdProductoBodega)
            txtProducto.Text = Producto.Nombre

            cmbEstadoProducto.Text = clsLnProducto_estado.Get_Nombre_By_IdEstado(gBeCiclico.IdProductoEstado)

            IMS.Listar_ProductoEstado(cmbEstadoProducto)

            If gBeCiclico.IdProductoEst_nuevo <> 0 Then
                cmbEstadoProducto.EditValue = gBeCiclico.IdProductoEst_nuevo
            Else
                cmbEstadoProducto.EditValue = gBeCiclico.IdProductoEstado
            End If

            IMS.Listar_OperadoresByStock(cmbOperador, gBeCiclico.IdUbicacion, gBeCiclico.Idinventarioenc, gBeCiclico.IdStock)
            cmbOperador.EditValue = gBeCiclico.Idoperador

            If gBeCiclico.IdPresentacion > 0 Then

                Presentacion.IdPresentacion = gBeCiclico.IdPresentacion
                clsLnProducto_presentacion.GetSingle(Presentacion)
                IMS.Listar_Presentaciones(cmbPresentacion, Producto.IdProducto)
                cmbPresentacion.EditValue = gBeCiclico.IdPresentacion

                If Presentacion.EsPallet Then
                    Conver = (Presentacion.CajasPorCama * Presentacion.CamasPorTarima) * Presentacion.Factor
                    vCantidad = Math.Round((gBeCiclico.Cantidad / Conver), 6)
                Else
                    vCantidad = Math.Round((gBeCiclico.Cantidad / Presentacion.Factor), 6)
                    txtCantidadStock.Value = Math.Round((gBeCiclico.Cant_stock / Presentacion.Factor), 6)
                End If
            Else
                vCantidad = gBeCiclico.Cantidad
                txtCantidadStock.Value = gBeCiclico.Cant_stock
            End If

            If gBeCiclico.IdUbicacion_nuevo <> 0 Then

                Ubicacion.IdUbicacion = gBeCiclico.IdUbicacion_nuevo
                clsLnBodega_ubicacion.Obtener(Ubicacion)

                txtUbicacion.Text = Ubicacion.NombreCompleto
                IdUbicacionSeleccionada = gBeCiclico.IdUbicacion_nuevo

            ElseIf gBeCiclico.IdUbicacion > 0 Then

                Ubicacion.IdUbicacion = gBeCiclico.IdUbicacion
                clsLnBodega_ubicacion.Obtener(Ubicacion)

                txtUbicacion.Text = Ubicacion.NombreCompleto
                IdUbicacionSeleccionada = gBeCiclico.IdUbicacion

            End If

            dtpFechaVence.Enabled = Producto.Control_vencimiento
            txtLote.Enabled = Producto.Control_lote

            If Producto.Control_lote Then
                If gBeCiclico.Lote <> gBeCiclico.Lote_stock Then
                    txtLote.Text = gBeCiclico.Lote
                Else
                    txtLote.Text = gBeCiclico.Lote_stock
                End If
            End If

            If Producto.Control_vencimiento Then
                If gBeCiclico.Fecha_vence <> gBeCiclico.Fecha_vence_stock Then
                    dtpFechaVence.EditValue = gBeCiclico.Fecha_vence
                Else
                    dtpFechaVence.EditValue = gBeCiclico.Fecha_vence_stock
                End If
            Else
                dtpFechaVence.EditValue = New Date(1900, 1, 1)
            End If

            If AP.Bodega.Control_Talla_Color Then
                IMS.Listar_Color(cmbColor)
                IMS.Listar_Tallas(cmbTalla)

                If gBeCiclico.IdProductoTallaColor <> 0 Then
                    Dim BeTallaColor = clsLnProducto_talla_color.GetSingle(gBeCiclico.IdProductoTallaColor)

                    If BeTallaColor IsNot Nothing Then
                        Dim Color = clsLnColor.GetSingle_By_IdColor(BeTallaColor.IdColor)
                        Dim Talla = clsLnTalla.GetSingle_By_IdTalla(BeTallaColor.IdTalla)

                        cmbColor.EditValue = Color.IdColor
                        cmbTalla.EditValue = Talla.IdTalla
                    End If
                End If
            End If

            txtCantidadAnterior.Value = vCantidad
            txtPesoAnterior.Value = gBeCiclico.Peso
            txtLicencia.Text = gBeCiclico.lic_plate

            txtGondola.Text = ""

            If DrDetalleInv IsNot Nothing Then

                If DrDetalleInv.Table.Columns.Contains("Góndola") Then
                    txtGondola.Text = DrDetalleInv.Item("Góndola").ToString()
                ElseIf DrDetalleInv.Table.Columns.Contains("Gondola") Then
                    txtGondola.Text = DrDetalleInv.Item("Gondola").ToString()
                End If

            End If

            ' Licencia y ubicación siempre habilitadas
            txtLicencia.Enabled = True
            txtLicencia.Properties.ReadOnly = False

            txtUbicacion.Enabled = True
            txtUbicacion.Properties.ReadOnly = False

            ' Talla / Color
            If AP.Bodega.Control_Talla_Color Then

                Label2.Visible = True
                cmbColor.Visible = True

                Label3.Visible = True
                cmbTalla.Visible = True

            Else

                Label2.Visible = False
                cmbColor.Visible = False

                Label3.Visible = False
                cmbTalla.Visible = False

            End If

            ' Góndola
            If AP.Bodega.Control_Gondola Then

                lblGondola.Visible = True
                txtGondola.Visible = True
                txtGondola.Enabled = True
                txtGondola.Properties.ReadOnly = False

            Else

                lblGondola.Visible = False
                txtGondola.Visible = False
                txtGondola.Text = ""

            End If

            txtLote.Focus()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally

            Cargando = False

        End Try

    End Sub

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick

        Try
            cmdGuardar.Enabled = False

            If Actualizar() Then
                XtraMessageBox.Show("Conteo Actualizado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            Else
                cmdGuardar.Enabled = True
            End If

        Catch ex As Exception
            cmdGuardar.Enabled = True

            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Dim Presentacion As New clsBeProducto_Presentacion
        Dim IdProductoTallaColorDestino As Integer = gBeCiclico.IdProductoTallaColor
        Dim vCantidadUmBas As Double = 0.0
        Dim vCantidadPres As Double = 0.0
        Dim vPeso As Double = 0.0
        Dim Conver As Double = 0.0
        Dim vDiferencia As Double = 0.0

        Dim cTrans As New clsTransaccion

        Try

            If txtCantidadAnterior.Value = 0 Then
                XtraMessageBox.Show("La cantidad debe ser mayor que 0", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If

            cTrans.Begin_Transaction()


            If AP.Bodega.Control_Talla_Color Then

                Dim IdProducto = clsLnProducto.Get_IdProducto_By_IdProductoBodega(gBeCiclico.IdProductoBodega)

                Dim IdProductoTallaColorTemp As Integer =
        clsLnProducto_talla_color.Get_IdProductoTallaColor_By_IdTalla_and_IdColor(
            cmbTalla.EditValue,
            cmbColor.EditValue,
            IdProducto
        )

                If IdProductoTallaColorTemp <> 0 Then

                    IdProductoTallaColorDestino = IdProductoTallaColorTemp

                    If gBeCiclico.IdProductoTallaColor <> IdProductoTallaColorDestino Then
                        gBeCiclico.IdProductoTallaColor_nuevo = IdProductoTallaColorDestino
                    Else
                        gBeCiclico.IdProductoTallaColor_nuevo = 0
                    End If

                End If

            End If

            gBeCiclico.IdProductoEst_nuevo = cmbEstadoProducto.EditValue
            gBeCiclico.IdPresentacion = cmbPresentacion.EditValue
            gBeCiclico.Fecha_vence = dtpFechaVence.EditValue
            gBeCiclico.Lote = txtLote.Text.Trim()
            gBeCiclico.Cantidad = txtCantidadAnterior.Value
            gBeCiclico.Peso = txtPesoAnterior.Value
            gBeCiclico.User_agr = AP.UsuarioAp.IdUsuario
            gBeCiclico.Fec_Mod = Now
            gBeCiclico.Idoperador = cmbOperador.EditValue
            gBeCiclico.lic_plate = txtLicencia.Text.Trim()

            If AP.Bodega.Control_Gondola Then
                gBeCiclico.Gondola = txtGondola.Text.Trim()
            End If

            If txtUbicacion.Text.Trim() <> "" AndAlso IsNumeric(txtUbicacion.Text.Trim()) Then
                IdUbicacionSeleccionada = CInt(txtUbicacion.Text.Trim())
            End If

            IdUbicacionSeleccionada = CInt(txtUbicacion.Text.Trim())

            If IdUbicacionSeleccionada <> 0 AndAlso IdUbicacionSeleccionada <> gBeCiclico.IdUbicacion Then
                gBeCiclico.IdUbicacion_nuevo = IdUbicacionSeleccionada
            Else
                gBeCiclico.IdUbicacion_nuevo = 0
            End If

            If gBeCiclico.IdPresentacion > 0 Then

                Presentacion.IdPresentacion = gBeCiclico.IdPresentacion
                clsLnProducto_presentacion.GetSingle(Presentacion, cTrans.lConnection, cTrans.lTransaction)

                If Presentacion.EsPallet Then
                    Conver = (Presentacion.CajasPorCama * Presentacion.CamasPorTarima) * Presentacion.Factor
                    vCantidadUmBas = Conver * gBeCiclico.Cantidad
                Else
                    vCantidadUmBas = Presentacion.Factor * gBeCiclico.Cantidad
                End If

                gBeCiclico.Cantidad = vCantidadUmBas
                vCantidadPres = txtCantidadAnterior.Value

            Else
                vCantidadPres = 0
                vCantidadUmBas = Math.Round(txtCantidadAnterior.Value, 6)
            End If

            '#CKFK20241119 Agregué una validación para que si cambia el lote o la fecha o el estado o la ubicación genere una nueva línea
            '#EJC202412041632: Comparar si hay cambios para actualizar.

            Dim cambioFecha As Boolean = FechaVenceOriginal <> CDate(dtpFechaVence.EditValue)
            Dim cambioEstado As Boolean = IdProductoEstadoOriginal <> CInt(cmbEstadoProducto.EditValue)
            Dim cambioLote As Boolean = LoteOriginal.Trim() <> txtLote.Text.Trim()
            Dim cambioUbicacion As Boolean = IdUbicacionOriginal <> IdUbicacionSeleccionada
            Dim cambioTallaColor As Boolean = IdProductoTallaColorOriginal <> IdProductoTallaColorDestino

            Dim debeCrearNuevoRegistro As Boolean =
                                        cambioEstado OrElse
                                        cambioUbicacion OrElse
                                        cambioTallaColor

            If EsInvOriginal AndAlso debeCrearNuevoRegistro Then

                gBeCiclico.Cant_stock = 0
                gBeCiclico.IdInvCiclico = clsLnTrans_inv_ciclico.MaxID(cTrans.lConnection, cTrans.lTransaction) + 1

                clsLnTrans_inv_ciclico.Insertar(gBeCiclico, cTrans.lConnection, cTrans.lTransaction)

            Else

                clsLnTrans_inv_ciclico.Actualizar(gBeCiclico, cTrans.lConnection, cTrans.lTransaction)

            End If


            '#EJC20180801:Actualizar la fila del del DT asociado al datasource del grid.
            If gBeCiclico.IdPresentacion > 0 Then
                Dim stockPres As Double = gBeCiclico.Cant_stock / Presentacion.Factor
                vDiferencia = Math.Round(stockPres - vCantidadPres, 6)
            Else
                vDiferencia = Math.Round(gBeCiclico.Cant_stock - vCantidadUmBas, 6)
            End If

            DrDetalleInv.Item("Vence") = dtpFechaVence.EditValue
            DrDetalleInv.Item("Lote") = txtLote.Text.Trim
            DrDetalleInv.Item("Estado") = cmbEstadoProducto.Text
            DrDetalleInv.Item("Cant.Conteo.Pres") = vCantidadPres
            DrDetalleInv.Item("PesoConteo") = txtPesoAnterior.Value
            DrDetalleInv.Item("Dif.Cant.UMBas") = vDiferencia * -1



            If AP.Bodega.Control_Gondola Then

                If DrDetalleInv.Table.Columns.Contains("Góndola") Then
                    DrDetalleInv.Item("Góndola") = txtGondola.Text.Trim()
                ElseIf DrDetalleInv.Table.Columns.Contains("Gondola") Then
                    DrDetalleInv.Item("Gondola") = txtGondola.Text.Trim()
                End If

            End If

            cTrans.Commit_Transaction()
            Actualizar = True

        Catch ex As Exception
            Actualizar = False
            cTrans.RollBack_Transaction()

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally

            cTrans.Close_Conection()

        End Try

    End Function

    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Eliminar el registro de inventario?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Elimina_Conteo_Ciclico() Then
                    XtraMessageBox.Show("Registro eliminado de la lista de conteos",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
                    Close()
                Else
                    XtraMessageBox.Show("No se pudo eliminar el registro",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Elimina_Conteo_Ciclico() As Boolean

        Elimina_Conteo_Ciclico = False

        Try

            clsLnTrans_inv_ciclico.EliminarConteoByIdStock(gBeCiclico)

            Elimina_Conteo_Ciclico = True

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Function

    Private Sub cmbOperador_EditValueChanged(sender As Object, e As EventArgs) Handles cmbOperador.EditValueChanged
        Try

            If Not Cargando Then
                gBeCiclico.Idoperador = cmbOperador.EditValue
                clsLnTrans_inv_ciclico.Get_All_By_IdOperador(gBeCiclico)
                Cargar_Datos()
            End If

        Catch ex As Exception
            MessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub frmConteo_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        If gBeCiclico.IdProductoEstado = gBeCiclico.IdProductoEst_nuevo AndAlso
           (gBeCiclico.IdUbicacion = gBeCiclico.IdUbicacion_nuevo OrElse gBeCiclico.IdUbicacion_nuevo = 0) AndAlso
           gBeCiclico.Lote_stock = gBeCiclico.Lote AndAlso
           gBeCiclico.Fecha_vence_stock = gBeCiclico.Fecha_vence Then
            EsInvOriginal = True
        End If

        FechaVenceOriginal = gBeCiclico.Fecha_vence_stock
        IdProductoEstadoOriginal = gBeCiclico.IdProductoEstado
        LoteOriginal = gBeCiclico.Lote
        IdUbicacionOriginal = gBeCiclico.IdUbicacion
        IdProductoTallaColorOriginal = gBeCiclico.IdProductoTallaColor

    End Sub

End Class