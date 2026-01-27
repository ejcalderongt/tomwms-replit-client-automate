Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmInventarioDet
    Public gBeStockProd As New clsBeTrans_inv_stock_prod

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub


    Public Property IdInventario As Integer
    Public Property IdBodega As Integer
    Public Property IdInventarioStockProd As Integer

    Private CargandoDatos As Boolean = False

    Private Sub frmInventarioDet_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            IMS.Listar_Productos(cmbProducto, IdBodega)
            IMS.Listar_Unidad_Medida(cmbUM)
            IMS.Listar_ProductoEstado(cmbEstado)

            If Modo = TipoTrans.Nuevo Then
                gBeStockProd = New clsBeTrans_inv_stock_prod()

                lblCodInv.Text = IdInventario.ToString()

                cmbProducto.EditValue = Nothing
                cmbPresentacion.EditValue = Nothing
                cmbUM.EditValue = Nothing

                txtCantidad.Value = 0
                txtLicencia.Text = ""
                txtLote.Text = ""

                txtIdUbicacion.Text = ""
                txtNomUbicacion.Text = ""
            Else
                cargarDatos()
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cargarDatos()
        Try
            CargandoDatos = True

            gBeStockProd.Idinvstockprod = IdInventarioStockProd

            Dim exito As Boolean = clsLnTrans_inv_stock_prod.GetSingle(gBeStockProd)
            If Not exito Then
                XtraMessageBox.Show("No se encontró el registro a editar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            lblCodInv.Text = gBeStockProd.Idinventario
            cmbProducto.EditValue = gBeStockProd.IdProducto
            IMS.Listar_Presentaciones(cmbPresentacion, gBeStockProd.IdProducto)
            cmbPresentacion.EditValue = If(gBeStockProd.IdPresentacion = 0, Nothing, gBeStockProd.IdPresentacion)
            cmbUM.EditValue = gBeStockProd.IdUnidadMedida
            txtLote.Text = gBeStockProd.Lote
            dtFechaVence.EditValue = gBeStockProd.Fecha_vence
            txtCantidad.Value = gBeStockProd.Cant
            txtIdUbicacion.Text = gBeStockProd.IdUbicacion
            Ubicacion_Es_Valida()
            txtLicencia.Text = gBeStockProd.License_plate

            Dim producto = clsLnProducto.Get_Single_By_IdProducto(gBeStockProd.IdProducto)
            If producto IsNot Nothing Then
                ConfigurarControlesProducto(producto)
                If producto.Control_vencimiento Then
                    If gBeStockProd.Fecha_vence <> Date.MinValue AndAlso gBeStockProd.Fecha_vence <> New Date(1900, 1, 1) Then
                        dtFechaVence.EditValue = gBeStockProd.Fecha_vence
                    Else
                        dtFechaVence.EditValue = Date.Today
                    End If
                Else
                    dtFechaVence.EditValue = New Date(1900, 1, 1)
                End If
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            CargandoDatos = False
        End Try
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Guardar() Then
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show("Se actualizó el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Close()
        Else
            XtraMessageBox.Show("No se pudo actualizar el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Function Guardar() As Boolean
        Try

            If cmbProducto.EditValue Is Nothing Then
                XtraMessageBox.Show("Seleccione un producto")
                Return False
            End If

            If cmbUM.EditValue Is Nothing Then
                XtraMessageBox.Show("Seleccione unidad de medida")
                Return False
            End If

            Dim producto = clsLnProducto.Get_Single_By_IdProducto(CInt(cmbProducto.EditValue))

            If Not ValidarFormulario(producto) Then
                Return False
            End If

            With gBeStockProd
                .Idinvstockprod = clsLnTrans_inv_stock_prod.SiguienteId(IdInventario)
                .Idinventario = IdInventario
                .IdProducto = producto.IdProducto
                .Codigo = producto.Codigo
                .IdUnidadMedida = cmbUM.EditValue
                .IdPresentacion = If(cmbPresentacion.EditValue Is Nothing, 0, cmbPresentacion.EditValue)
                .Cant = txtCantidad.Value
                .Lote = txtLote.Text.Trim
                .Fecha_vence = If(producto.Control_vencimiento,
                  dtFechaVence.EditValue, New Date(1900, 1, 1))
                .IdUbicacion = txtIdUbicacion.Text.Trim
                .License_plate = txtLicencia.Text.Trim
                .IdBodega = IdBodega
                .TipoTeoricoImportacion = 0
            End With

            If Modo = TipoTrans.Nuevo Then
                clsLnTrans_inv_stock_prod.Insertar(gBeStockProd)
            Else
                clsLnTrans_inv_stock_prod.Actualizar(gBeStockProd)
            End If

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message)
            Return False
        End Try
    End Function

    Private Sub cmbPresentacion_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbPresentacion.KeyDown
        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then
            cmbPresentacion.EditValue = Nothing
            gBeStockProd.IdPresentacion = 0
            e.Handled = True
        End If
    End Sub

    Private Sub Control_KeyDown(sender As Object, e As KeyEventArgs) _
        Handles cmbProducto.KeyDown, cmbPresentacion.KeyDown, cmbUM.KeyDown,
            txtCantidad.KeyDown, txtLicencia.KeyDown, txtLote.KeyDown,
            dtFechaVence.KeyDown, txtIdUbicacion.KeyDown, cmbEstado.KeyDown

        If e.KeyCode = Keys.Enter Then
            MoverFocoSiguiente(CType(sender, Control))
            e.Handled = True
        End If
    End Sub

    Private Sub cmbProducto_EditValueChanged(sender As Object, e As EventArgs) _
        Handles cmbProducto.EditValueChanged

        If CargandoDatos OrElse cmbProducto.EditValue Is Nothing Then Return

        Try
            Dim idProd As Integer = CInt(cmbProducto.EditValue)
            Dim producto = clsLnProducto.Get_Single_By_IdProducto(idProd)

            If producto Is Nothing Then Return

            cmbUM.EditValue = producto.UnidadMedida.IdUnidadMedida

            IMS.Listar_Presentaciones(cmbPresentacion, producto.IdProducto)
            cmbPresentacion.EditValue = Nothing

            txtCantidad.Value = 0
            txtLicencia.Text = ""
            txtLote.Text = ""

            ConfigurarControlesProducto(producto)

        Catch ex As Exception
            XtraMessageBox.Show("Error al cambiar producto: " & ex.Message)
        End Try
    End Sub

    Private Function Ubicacion_Es_Valida() As Boolean
        Try
            If String.IsNullOrWhiteSpace(txtIdUbicacion.Text) Then
                txtNomUbicacion.Text = ""
                Return False
            End If

            Dim BeBodegaUbicacion = clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(txtIdUbicacion.Text, IdBodega)
            If BeBodegaUbicacion IsNot Nothing Then
                txtNomUbicacion.Text = BeBodegaUbicacion.Descripcion
                Return True
            Else
                txtNomUbicacion.Text = ""
                Return False
            End If
        Catch ex As Exception
            txtNomUbicacion.Text = ""
            Return False
        End Try
    End Function

    Private Sub txtIdUbicacion_Validating(sender As Object, e As CancelEventArgs) Handles txtIdUbicacion.Validating
        Ubicacion_Es_Valida()
    End Sub

    Private Sub txtLicencia_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLicencia.KeyDown
        If e.KeyCode = Keys.Enter Then
            If String.IsNullOrWhiteSpace(txtLicencia.Text) Then
                Dim r = XtraMessageBox.Show("La licencia está vacía.", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If r = DialogResult.No Then
                    txtLicencia.Focus()
                    e.Handled = True
                    Return
                End If
            End If

            MoverFocoSiguiente(txtLicencia)
            e.Handled = True
        End If
    End Sub

    Private Function ValidarFormulario(ByVal producto As clsBeProducto) As Boolean

        If txtCantidad.Value <= 0 Then
            XtraMessageBox.Show("La cantidad no puede ser 0", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCantidad.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtIdUbicacion.Text) Then
            XtraMessageBox.Show("Debe ingresar una ubicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtIdUbicacion.Focus()
            Return False
        End If

        If Not Ubicacion_Es_Valida() Then
            XtraMessageBox.Show("La ubicación ingresada no es válida", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtIdUbicacion.Focus()
            Return False
        End If

        If producto.Control_lote Then
            If String.IsNullOrWhiteSpace(txtLote.Text) Then
                XtraMessageBox.Show("El producto requiere lote", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtLote.Focus()
                Return False
            End If
        Else
            If Not String.IsNullOrWhiteSpace(txtLote.Text) Then
                XtraMessageBox.Show("Este producto no maneja lote", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtLote.Focus()
                Return False
            End If
        End If

        If cmbEstado.EditValue Is Nothing Then
            XtraMessageBox.Show("Debe seleccionar un estado", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbEstado.Focus()
            Return False
        End If

        If producto.Control_vencimiento Then
            If dtFechaVence.EditValue Is Nothing Then
                XtraMessageBox.Show("Debe ingresar fecha de vencimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                dtFechaVence.Focus()
                Return False
            End If
        End If

        Return True
    End Function
    Private Sub ConfigurarControlesProducto(ByVal producto As clsBeProducto)

        If producto.Control_vencimiento Then
            dtFechaVence.Enabled = True
            If Modo = TipoTrans.Nuevo Then
                dtFechaVence.EditValue = Date.Today
            End If
        Else
            dtFechaVence.Enabled = False
            dtFechaVence.EditValue = New Date(1900, 1, 1)
        End If

        If producto.Control_lote Then
            txtLote.Enabled = True
            If Modo = TipoTrans.Nuevo Then txtLote.Text = ""
        Else
            txtLote.Enabled = False
            txtLote.Text = ""
        End If
    End Sub
    Private Sub MoverFocoSiguiente(ByVal controlActual As Control)

        Select Case controlActual.Name

            Case "cmbProducto"
                cmbPresentacion.Focus()

            Case "cmbPresentacion"
                cmbUM.Focus()

            Case "cmbUM"
                txtLicencia.Focus()

            Case "txtLicencia"
                If txtLote.Enabled Then
                    txtLote.Focus()
                Else
                    txtIdUbicacion.Focus()
                End If

            Case "txtLote"
                txtIdUbicacion.Focus()

            Case "txtIdUbicacion"
                If dtFechaVence.Enabled Then
                    dtFechaVence.Focus()
                Else
                    cmbEstado.Focus()
                End If

            Case "dtFechaVence"
                cmbEstado.Focus()

            Case "cmbEstado"
                txtCantidad.Focus()

            Case "txtCantidad"
                If cmbProducto.EditValue Is Nothing Then
                    XtraMessageBox.Show("Seleccione un producto antes de guardar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbProducto.Focus()
                    Return
                End If

                Dim producto = clsLnProducto.Get_Single_By_IdProducto(CInt(cmbProducto.EditValue))
                If Not ValidarFormulario(producto) Then
                    Return
                End If

                If Guardar() Then
                    XtraMessageBox.Show("Inventario guardado correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    XtraMessageBox.Show("No se pudo guardar el inventario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
        End Select
    End Sub
    Private Sub frmInventarioDet_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        cmbProducto.Focus()
    End Sub
End Class