Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmAjustePositivo

    '#GT26112024: parametros compartidos con ajuste stock
    Public Property pStockIdPropietario As Integer
    Public Property pStockIdPropietarioBodega As Integer
    Public Property pStockBodegaFiltro As Integer
    Public Property vProducto As New clsBeProducto

    Private pIdProductoBodega As Integer
    Public Property pStockTemporal As clsBeStock
    Public Property pUbicacion As clsBeBodega_ubicacion

    Private BeBodega As New clsBeBodega
    Private DT As New DataTable("Producto")
    Private DT_Estado As New DataTable("Producto_Estado")
    Private DT_Presentacion As New DataTable("Producto_Presentacion")
    Private DT_Umbas As New DataTable("Umbas")

    Private listaTalla As New List(Of clsBeTalla)
    Private listaColor As New List(Of clsBeColor)

    Dim altoHueco As Integer


    Private Sub frmAjustePositivo_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Producto sin Stock")
            SplashScreenManager.Default.SetWaitFormDescription("cargando productos...")

            BeBodega = AP.Bodega
            '#GT26112024: datos del encabezado
            lbIdStock.Text = 0
            pUbicacion = New clsBeBodega_ubicacion
            pUbicacion = clsLnBodega_ubicacion.GetSingle(AP.Bodega.Ubic_recepcion, AP.IdBodega)
            txtUbicacion.Text = pUbicacion.NombreCompleto
            '#GT06012025: se permite cambiar la ubicación mediante la busqueda, no digitado en el input
            txtUbicacion.Enabled = False

            Cargar_Productos_Sin_Stock()
            cmbProductos.Focus()

            '#GT15122025: se utiliza para reducir el espacio dejado por inputs que estan invisibles pero ocupan
            altoHueco = txtCantidad.Height + 6 ' separación (ajusta)

            If BeBodega.Control_Talla_Color Then

                listaTalla = clsLnTalla.Get_All()
                listaColor = clsLnColor.Get_All()

                Cargar_Talla()
                Caregar_Color()

                lbTalla.Visible = True
                lbColor.Visible = True
                cmbTalla.Visible = True
                cmbColor.Visible = True

                lbTalla.Top = lbTalla.Top - altoHueco
                lbColor.Top = lbColor.Top - altoHueco
                cmbTalla.Top = cmbTalla.Top - altoHueco
                cmbColor.Top = cmbColor.Top - altoHueco

                '#GT15122025: campos por defecto
                dtpFechaVence.Visible = False
                txtLote.Visible = False
                txtPeso.Visible = False
                lblFechaVence.Visible = False
                lblLote.Visible = False
                lblPesoAnterior.Visible = False

            Else

                lbTalla.Visible = False
                lbColor.Visible = False
                cmbTalla.Visible = False
                cmbColor.Visible = False

                lbTalla.Top = lbTalla.Top + altoHueco
                lbColor.Top = lbColor.Top + altoHueco
                cmbTalla.Top = cmbTalla.Top + altoHueco
                cmbColor.Top = cmbColor.Top + altoHueco

                '#GT15122025: campos por defecto
                dtpFechaVence.Visible = True
                txtLote.Visible = True
                txtPeso.Visible = True
                lblFechaVence.Visible = True
                lblLote.Visible = True
                lblPesoAnterior.Visible = True

            End If

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text,
          MessageBoxButtons.OK,
          MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub Caregar_Color()
        Try

            With cmbColor.Properties
                .DataSource = listaColor
                .ValueMember = "IdColor"
                .DisplayMember = "Codigo"   ' el campo requerido

                .Columns.Clear()
                .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Codigo", "Código"))
                .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nombre", "Nombre"))
                .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdColor", "Id") With {
                    .Visible = False
                })

                .ShowHeader = True          ' para que se vean los títulos de columnas
                .NullText = ""
                .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
            End With

            cmbColor.EditValue = Nothing


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Cargar_Talla()
        Try

            With cmbTalla.Properties
                .DataSource = listaTalla
                .ValueMember = "IdTalla"
                .DisplayMember = "Codigo"   ' el campo requerido

                .Columns.Clear()
                .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Codigo", "Código"))
                .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nombre", "Nombre"))
                .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdTalla", "Id") With {
                    .Visible = False
                })

                .ShowHeader = True          ' para que se vean los títulos de columnas
                .NullText = ""
                .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
            End With

            cmbTalla.EditValue = Nothing

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Cargar_Productos_Sin_Stock()
        Try

            If AP.Bodega.Control_Talla_Color Then
                DT = clsLnProducto.Get_All_Lista_Productos_Todos(pStockIdPropietario, pStockIdPropietarioBodega, pStockBodegaFiltro, 1)
            Else
                DT = clsLnProducto.Get_All_Lista_Producto_SinStock(pStockIdPropietario, pStockIdPropietarioBodega, pStockBodegaFiltro, 1)
            End If

            If DT.Rows.Count > 0 Then
                With cmbProductos.Properties
                    .DataSource = DT
                    .ValueMember = "IdProductoBodega"
                    .DisplayMember = "Nombre"   ' el campo requerido
                    .NullText = ""
                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                End With

            End If

            cmbProductos.EditValue = Nothing

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub cmbProductos_EditValueChanged(sender As Object, e As EventArgs) Handles cmbProductos.EditValueChanged
        Try

            If cmbProductos.EditValue > 0 Then
                pIdProductoBodega = cmbProductos.EditValue
                vProducto = New clsBeProducto
                vProducto = clsLnProducto.Get_BeProducto_By_IdProductoBodega(pIdProductoBodega, pStockBodegaFiltro)

                If vProducto IsNot Nothing Then

                    '#GT27112024: nuevo objeto cuando se cambia de producto.
                    pStockTemporal = New clsBeStock

                    '#GT26112024: cargar propiedades de un producto valido, para no hacerlo sobre uno vacio.
                    '#GT15122025: ajustar para no genera lp auto si maneja control talla/color
                    Set_Propiedades_Producto()

                Else
                    Throw New Exception("No se cargaron los atributos del producto seleccionado.")
                End If

            Else

                '#GT26112024: deshabilitar los campos cuando no hay producto seleccionado en el combo
                txtLicencia.Enabled = False
                txtLote.Enabled = False
                txtPeso.Enabled = False
                dtpFechaVence.Enabled = False

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_Propiedades_Producto()
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando propiedades...")

            Cargar_Producto_Estado()
            Cargar_Producto_Presentacion()
            Cargar_Umbas()

            '#GT15122025: control talla color excluye si genera lp en producto
            If BeBodega.Control_Talla_Color Then

                lblLote.Visible = False
                txtLote.Visible = False
                lblFechaVence.Visible = False
                dtpFechaVence.Visible = False

                lblLote.Visible = False
                txtLote.Visible = False
                lblFechaVence.Visible = False
                dtpFechaVence.Visible = False
                lblPesoAnterior.Visible = False
                txtPeso.Visible = False

            Else

                If vProducto.Genera_lp Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Cálculando LP...")

                    txtLicencia.EditValue = Genera_Licencia_BOF(pStockBodegaFiltro, AP.UsuarioAp.IdUsuario)

                Else
                    txtLicencia.Enabled = False
                End If

                If vProducto.Genera_lote Then

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormDescription("Cálculando Lote...")

                    txtLote.Enabled = True
                    txtLote.EditValue = ""
                Else
                    txtLote.Enabled = False
                End If

                If vProducto.Control_peso Then
                    txtPeso.Enabled = True
                    txtPeso.Value = 0
                Else
                    txtPeso.Enabled = False
                End If

                If vProducto.Control_vencimiento Then
                    dtpFechaVence.Enabled = True
                    dtpFechaVence.EditValue = Now
                Else
                    dtpFechaVence.Enabled = False
                    dtpFechaVence.EditValue = Nothing
                End If



            End If

            cmbUmbas.EditValue = vProducto.IdUnidadMedidaBasica
            cmbUmbas.Enabled = False

            pStockTemporal.IsNew = True
            pStockTemporal.IdUnidadMedida = vProducto.IdUnidadMedidaBasica
            pStockTemporal.IdPresentacion = cmbProductoPresentacion.EditValue
            pStockTemporal.IdProductoEstado = cmbProductoEstado.EditValue
            pStockTemporal.IdPropietarioBodega = pStockIdPropietarioBodega
            pStockTemporal.IdBodega = pStockBodegaFiltro
            pStockTemporal.IdProductoBodega = pIdProductoBodega
            'pStockTemporal.Cantidad = txtCantidad.Value
            pStockTemporal.Cantidad = 0
            pStockTemporal.IdUbicacion = pUbicacion.IdUbicacion
            '#GT: parametros del producto
            pStockTemporal.Lic_plate = IIf(vProducto.Genera_lp, txtLicencia.EditValue, DBNull.Value)
            pStockTemporal.Fecha_vence = IIf(vProducto.Control_vencimiento, dtpFechaVence.DateTime, Now.ToString("1900-01-01"))
            pStockTemporal.Peso = IIf(vProducto.Control_peso, txtPeso.Value, 0)
            pStockTemporal.Lote = IIf(vProducto.Genera_lote, txtLote.EditValue, "")

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cargar_Umbas()
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando Umbas...")

            DT_Umbas = clsLnUnidad_medida.Get_All_By_IdPropietario_And_Activo(pStockIdPropietario)

            'DT_Estado = clsLnProducto_estado.Get_All_By_IdPropietario_And_IdBodega(pStockIdPropietario, pStockBodegaFiltro)

            If DT_Umbas IsNot Nothing AndAlso DT_Umbas.Rows.Count > 0 Then
                cmbUmbas.Properties.DisplayMember = "Nombre"
                cmbUmbas.Properties.ValueMember = "IdUnidadMedida"
                cmbUmbas.Properties.DataSource = DT_Umbas
                cmbUmbas.Properties.PopupWidth = 700
                cmbUmbas.Properties.PopulateColumns()
                cmbUmbas.Properties.BestFit()
                cmbUmbas.Properties.NullText = ""
                cmbUmbas.EditValue = 0
            End If

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cargar_Producto_Estado()
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando Estados...")

            DT_Estado = clsLnProducto_estado.Get_All_By_IdPropietario_And_IdBodega(pStockIdPropietario, pStockBodegaFiltro)

            If DT_Estado IsNot Nothing AndAlso DT_Estado.Rows.Count > 0 Then
                cmbProductoEstado.Properties.DisplayMember = "Nombre"
                cmbProductoEstado.Properties.ValueMember = "IdEstado"
                cmbProductoEstado.Properties.DataSource = DT_Estado
                cmbProductoEstado.Properties.PopupWidth = 700
                cmbProductoEstado.Properties.PopulateColumns()
                cmbProductoEstado.Properties.BestFit()
                cmbProductoEstado.Properties.NullText = ""
                cmbProductoEstado.EditValue = 0
            End If

        Catch ex As Exception

            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cargar_Producto_Presentacion()
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Cargando Presentaciones...")

            DT_Presentacion = clsLnProducto_presentacion.Get_All_By_IdProductoBodega(pIdProductoBodega)

            If DT_Presentacion IsNot Nothing AndAlso DT_Presentacion.Rows.Count > 0 Then
                cmbProductoPresentacion.Properties.DisplayMember = "Nombre"
                cmbProductoPresentacion.Properties.ValueMember = "IdPresentacion"
                cmbProductoPresentacion.Properties.DataSource = DT_Presentacion
                cmbProductoPresentacion.Properties.PopupWidth = 700
                cmbProductoPresentacion.Properties.PopulateColumns()
                cmbProductoPresentacion.Properties.BestFit()
                cmbProductoPresentacion.Properties.NullText = ""
                cmbProductoPresentacion.EditValue = 0
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        Try

            If Validar_Datos() Then

                DialogResult = DialogResult.OK
                Close()

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbProductoEstado_EditValueChanged(sender As Object, e As EventArgs) Handles cmbProductoEstado.EditValueChanged
        Try
            If cmbProductoEstado.EditValue > 0 Then
                pStockTemporal.IdProductoEstado = cmbProductoEstado.EditValue
            End If
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbProductoPresentacion_EditValueChanged(sender As Object, e As EventArgs) Handles cmbProductoPresentacion.EditValueChanged
        Try

            If cmbProductoPresentacion.EditValue > 0 Then
                pStockTemporal.IdPresentacion = cmbProductoPresentacion.EditValue
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbUmbas_EditValueChanged(sender As Object, e As EventArgs) Handles cmbUmbas.EditValueChanged
        Try

            If cmbUmbas.EditValue > 0 Then
                pStockTemporal.Producto.IdUnidadMedidaBasica = cmbUmbas.EditValue
                pStockTemporal.IdUnidadMedida = cmbUmbas.EditValue
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbTalla_EditValueChanged(sender As Object, e As EventArgs) Handles cmbTalla.EditValueChanged
        Try

            If cmbTalla.EditValue > 0 Then
                pStockTemporal.Talla = cmbTalla.Text
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbColor_EditValueChanged(sender As Object, e As EventArgs) Handles cmbColor.EditValueChanged
        Try

            If cmbColor.EditValue > 0 Then
                pStockTemporal.Color = cmbColor.Text
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtLicencia_EditValueChanged(sender As Object, e As EventArgs) Handles txtLicencia.EditValueChanged
        If Not String.IsNullOrEmpty(txtLicencia.EditValue) Then
            '#GT 05012025: talla_color es excluyente de si genera lp_auto
            If BeBodega.Control_Talla_Color Then
                pStockTemporal.Lic_plate = txtLicencia.EditValue
            End If
        End If
    End Sub

    Private Sub lnkUbicaciones_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicaciones.LinkClicked

        '#GT28112024: cargar producto sin stock asociado
        Dim frmUbicaciones As New frmBodegaUbicacion_List() With {
                  .Modo = 2,
                 .pIdBodega = AP.IdBodega,
                 .pUbicacionesTodas = True,
                 .StartPosition = FormStartPosition.CenterParent,
                 .WindowState = FormWindowState.Normal
            }
        frmUbicaciones.ShowDialog()

        '#GT06012025: el showdialog no retorna Ok por eso solo validamos si el objeto no es nothing
        If frmUbicaciones.pObj IsNot Nothing Then
            txtUbicacion.Text = frmUbicaciones.pObj.NombreCompleto
            pStockTemporal.IdUbicacion = frmUbicaciones.pObj.IdUbicacion
        End If

    End Sub

    Private Function Validar_Datos() As Boolean
        Try

            If AP.Bodega.Control_Talla_Color Then
                If String.IsNullOrEmpty(cmbProductoEstado.Text) Then
                    XtraMessageBox.Show("Debe seleccionar un estado para el producto.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbProductoEstado.Focus()
                    Return False
                End If
                If String.IsNullOrEmpty(cmbTalla.Text) Then
                    XtraMessageBox.Show("Debe seleccionar una talla.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbTalla.Focus()
                    Return False
                End If
                If String.IsNullOrEmpty(cmbColor.Text) Then
                    XtraMessageBox.Show("Debe seleccionar un color.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbColor.Focus()
                    Return False
                End If
                If String.IsNullOrEmpty(txtLicencia.Text) Then
                    XtraMessageBox.Show("Debe seleccionar una licencia para el producto.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtLicencia.Focus()
                    Return False
                End If

            End If

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

End Class