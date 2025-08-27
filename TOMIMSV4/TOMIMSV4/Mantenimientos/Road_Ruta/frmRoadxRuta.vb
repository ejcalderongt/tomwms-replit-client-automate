Imports DevExpress.XtraEditors

Public Class frmRoadxRuta
    Private pListObjT As New List(Of clsTabla)
    'Private wcfmd As New WCFRoadRuta.RoadRutaClient
    'Public pObj As New clsBeRoad_ruta
    Public Delegate Sub Listar_Rutas()
    Public Property InvokeListarRutas As Listar_Rutas

    Public pListObjDB As List(Of clsBeRoad_ruta)
    Public pIdRuta As Integer

    Public gBeRoadRuta As New clsBeRoad_ruta

    '''Bodega a la que pertenece la ruta
    Private rrBodega As New clsBeBodega

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Sub limpiarFormulario()

        txtID.Text = ""
        txtCodigo.Text = ""
        txtNombre.Text = ""

    End Sub

    Private Sub frmRoadxRuta_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("road_ruta")

            Me.Top = 50
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2

            Listar_Vendedores_ROAD(cbxVendedor)
            AP.Listar_Bodegas_By_Usuario(cmbBodega) 'Filtar las bodega de las empresa con la que actualmente se ha ingrsado
            'AP.Listar_PropietarioBodega(cmbPropietario)

            cbxTipoVenta.Properties.DataSource = getTipoVenta()
            cbxTipoVenta.Properties.DisplayMember = "Nombre"
            cbxTipoVenta.Properties.ValueMember = "ID"
            cbxTipoVenta.ItemIndex = 0

            cbxAplicacion.DataSource = getAplicacion()
            cbxAplicacion.DisplayMember = "nombre"
            cbxAplicacion.ValueMember = "ID"

            Select Case Modo

                Case TipoTrans.Nuevo

                    limpiarFormulario()

                    txtID.Text = clsLnRoad_ruta.MaxID()

                    Carga_Ubicacion_Transito()

                    mnuGuardar.Enabled = True
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    txtCodigo.Enabled = True

                Case TipoTrans.Editar

                    BuscarRegistro()

            End Select

            Me.Focus()
            txtCodigo.Focus()


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub BuscarRegistro()

        Dim objtemporal As New clsBeBodega_ubicacion

        Try

            'pObj = wcfmd.GetSingle(pObj.IdRuta)

            'gBeRoadRuta = clsLnRoad_ruta.GetSingle(gBeRoadRuta.IdRuta)


            txtID.Text = gBeRoadRuta.IdRuta
            cmbPropietario.EditValue = gBeRoadRuta.IdPropietarioBodega

            If gBeRoadRuta.IdUbicacionTransito = 0 Then
                Carga_Ubicacion_Transito()
            Else
                txtIdUbicacionDestino.Text = gBeRoadRuta.IdUbicacionTransito
                cargarUbicacion()
            End If

            txtCodigo.Text = gBeRoadRuta.CODIGO
            txtNombre.Text = gBeRoadRuta.NOMBRE

            Try
                cbxVendedor.EditValue = gBeRoadRuta.VENDEDOR

            Catch exx As Exception
                XtraMessageBox.Show(exx.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

            cbxActivo.CheckState = IIf(gBeRoadRuta.ACTIVO = "S", CheckState.Checked, CheckState.Unchecked)

            cbxTipoVenta.EditValue = gBeRoadRuta.VENTA
            cbxRutaForanea.CheckState = IIf(gBeRoadRuta.FORANIA = "S", CheckState.Checked, CheckState.Unchecked)
            txtSucursal.Text = gBeRoadRuta.SUCURSAL
            txtTipo.Text = gBeRoadRuta.TIPO
            txtSubTipo.Text = gBeRoadRuta.SUBTIPO

            cmbBodega.Text = gBeRoadRuta.BODEGA '*********************************************************************************
            txtSubbodega.Text = gBeRoadRuta.SUBBODEGA
            cbxManejarDescuento.CheckState = IIf(gBeRoadRuta.EDITDEVPREC = "S", CheckState.Checked, CheckState.Unchecked)
            cbxDescuento.CheckState = IIf(gBeRoadRuta.DESCUENTO = "S", CheckState.Checked, CheckState.Unchecked)
            cbxBonificar.CheckState = IIf(gBeRoadRuta.BONIF = "S", CheckState.Checked, CheckState.Unchecked)
            cbxKilometraje.CheckState = IIf(gBeRoadRuta.KILOMETRAJE = "S", CheckState.Checked, CheckState.Unchecked)
            cbxImpresion.CheckState = IIf(gBeRoadRuta.IMPRESION = "S", CheckState.Checked, CheckState.Unchecked)
            cbxReciboPropio.CheckState = IIf(gBeRoadRuta.RECIBOPROPIO = "S", CheckState.Checked, CheckState.Unchecked)
            cbxCelular.CheckState = IIf(gBeRoadRuta.CELULAR = "S", CheckState.Checked, CheckState.Unchecked)
            cbxRentabilidad.CheckState = IIf(gBeRoadRuta.RENTABIL = "S", CheckState.Checked, CheckState.Unchecked)
            cbxOferta.CheckState = IIf(gBeRoadRuta.OFERTA = "S", CheckState.Checked, CheckState.Unchecked)
            txtPorcentajeRentabilidad.Text = gBeRoadRuta.PERCRENT
            cbxPasarCredito.CheckState = IIf(gBeRoadRuta.PASARCREDITO = "S", CheckState.Checked, CheckState.Unchecked)

            cbxVerTeclado.CheckState = IIf(gBeRoadRuta.TECLADO = "S", CheckState.Checked, CheckState.Unchecked)
            cbxPrecioDevolucion.CheckState = IIf(gBeRoadRuta.EDITDEVPREC = "S", CheckState.Checked, CheckState.Unchecked)
            cbxDescuento.CheckState = IIf(gBeRoadRuta.EDITDESC = "S", CheckState.Checked, CheckState.Unchecked)
            txtParametro.Text = gBeRoadRuta.PARAMS
            SpinSemana.Value = Decimal.Parse(gBeRoadRuta.SEMANA)
            SpinAno.Value = Decimal.Parse(gBeRoadRuta.OBJANO)
            SpinMes.Value = Decimal.Parse(gBeRoadRuta.OBJMES)
            txtFolderSincronizacion.Text = gBeRoadRuta.SYNCFOLD
            txtServicioLocal.Text = gBeRoadRuta.WLFOLD
            txtServicioRemoto.Text = gBeRoadRuta.FTPFOLD
            txtCorreo.Text = gBeRoadRuta.EMAIL
            SpinUltimaImportacion.Value = Decimal.Parse(gBeRoadRuta.LASTIMP)
            SpinUltimaComunicacion.Value = Decimal.Parse(gBeRoadRuta.LASTCOM)
            SpinUltimaExportacion.Value = Decimal.Parse(gBeRoadRuta.LASTEXP)
            spinEstadoImportacion.Value = Decimal.Parse(IIf(gBeRoadRuta.IMPSTAT = "", 0, gBeRoadRuta.IMPSTAT))
            spinEstadoExportacion.Value = Decimal.Parse(IIf(gBeRoadRuta.EXPSTAT = "", 0, gBeRoadRuta.EXPSTAT))
            SpinEstadoComunicacion.Value = Decimal.Parse(IIf(gBeRoadRuta.COMSTAT = "", 0, gBeRoadRuta.COMSTAT))
            txtParametro1.Text = gBeRoadRuta.PARAM1
            txtParametro2.Text = gBeRoadRuta.PARAM2
            txtPesoLimite.Text = gBeRoadRuta.PESOLIM
            SpinIntervaloMaximo.Value = Decimal.Parse(gBeRoadRuta.INTERVALO_MAX)
            SpinLecturasValidas.Value = Decimal.Parse(gBeRoadRuta.LECTURAS_VALID)
            SpinIntentosLectura.Value = Decimal.Parse(gBeRoadRuta.INTENTOS_LECT)
            SpinHoraEncendido.Value = Decimal.Parse(gBeRoadRuta.HORA_INI)
            SpinHoraApagado.Value = Decimal.Parse(gBeRoadRuta.HORA_FIN)
            cbxAplicacion.SelectedValue = gBeRoadRuta.APLICACION_USA

            'MsgBox(cbxAplicacion.SelectedValue & " -- " & gBeRoadRuta.APLICACION_USA)
            SpinCom.Value = gBeRoadRuta.PUERTO_GPS
            cbxRutaOficina.CheckState = IIf(gBeRoadRuta.ES_RUTA_OFICINA, CheckState.Checked, CheckState.Unchecked)
            cbxDeluirBonificacion.CheckState = IIf(gBeRoadRuta.DILUIR_BON, CheckState.Checked, CheckState.Unchecked)
            cbxPreimpresionFactura.CheckState = IIf(gBeRoadRuta.PREIMPRESION_FACTURA, CheckState.Checked, CheckState.Unchecked)

            objtemporal.IdUbicacion = gBeRoadRuta.IdUbicacionTransito
            If (clsLnBodega_ubicacion.Obtener(objtemporal)) Then

                txtIdUbicacionDestino.Text = objtemporal.IdUbicacion
                txtUbicacionDestino.Text = objtemporal.Descripcion
                'MsgBox("Si lo encontro pero no lo puso" + pObj.IdUbicacionTransito)
                'txtIdUbicacionDestino.Text = pObj.IdUbicacionTransito
            Else
                '#CKFK20220324 Preguntar si puedo poner esto en comentario porque es un mensaje sin información que aclare la importancia
                XtraMessageBox.Show("No pude obtener la ubicación de tránsito, se continuará con la carga de la ruta", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            mnuGuardar.Enabled = False
            mnuActualizar.Enabled = True
            mnuEliminar.Enabled = True
            txtCodigo.Enabled = False

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Function Listar_Vendedores_ROAD(ByRef Cmb As LookUpEdit) As Boolean

        Dim DT As New DataTable

        Try

            DT = clsLnRoad_p_vendedor.Get_All_For_Combo()

            If Not DT Is Nothing Then
                Cmb.Properties.DisplayMember = "Nombre"
                Cmb.Properties.ValueMember = "IdVendedor"
                Cmb.Properties.DataSource = DT
                Cmb.ItemIndex = 0
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function getTipoVenta() As DataTable

        Dim TipoVenta As New DataTable("Tipo_Venta")

        TipoVenta.Columns.Add("ID")
        TipoVenta.Columns.Add("Nombre")

        TipoVenta.Rows.Add({"V", "Venta"})
        TipoVenta.Rows.Add({"P", "Preventa"})
        TipoVenta.Rows.Add({"D", "Despacho"})
        TipoVenta.Rows.Add({"T", "Todas"})

        Return TipoVenta

    End Function

    Public Function getAplicacion() As DataTable
        Dim TipoVenta As New DataTable("Aplicacion")

        TipoVenta.Columns.Add("ID")
        TipoVenta.Columns.Add("Nombre")

        TipoVenta.Rows.Add({1, "Road"})
        TipoVenta.Rows.Add({2, "Encuentas"})
        TipoVenta.Rows.Add({3, "Ambos"})
        Return TipoVenta
    End Function

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        If Datos_Correctos() Then

            If MessageBox.Show("¿Guardar Ruta?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarRutas.Invoke
                    Close()
                End If

            End If

        End If

    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtCodigo.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Código.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigo.Focus()

            ElseIf txtCodigo.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO").Longitud Then
                XtraMessageBox.Show("El Código debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigo.Focus()

            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()

            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show("El Nombre debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf cbxVendedor.Text = "" Then
                XtraMessageBox.Show("No existe vendedor, Favor registre un vendedor.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cbxVendedor.Focus()
            ElseIf String.IsNullOrEmpty(cmbBodega.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbBodega.Focus()
            ElseIf cmbBodega.Text.Split("-")(0).Trim.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "BODEGA").Longitud Then
                XtraMessageBox.Show("La Bodega debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "BODEGA").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbBodega.Focus()
            ElseIf String.IsNullOrEmpty(cmbPropietario.Text.Trim) Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbPropietario.Focus()
            ElseIf String.IsNullOrEmpty(txtSubbodega.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Sub bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSubbodega.Focus()
            ElseIf txtSucursal.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "SUCURSAL").Longitud Then
                XtraMessageBox.Show("La Sucursal debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "SUCURSAL").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSucursal.Focus()
            ElseIf String.IsNullOrEmpty(txtIdUbicacionDestino.Text.Trim) And String.IsNullOrEmpty(txtUbicacionDestino.Text.Trim) Then
                XtraMessageBox.Show("La Ubicación es Obligatoria.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtIdUbicacionDestino.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Guardar() As Boolean

        Guardar = False

        Dim objetoRuta As New clsBeRoad_ruta

        Try

            objetoRuta.IdRuta = txtID.Text
            objetoRuta.IdPropietarioBodega = cmbPropietario.EditValue
            objetoRuta.IdUbicacionTransito = txtIdUbicacionDestino.Text 'SpinIdUbicacion.Value 'txtIdUbicacionDestino.Text*********************************
            objetoRuta.CODIGO = txtCodigo.Text.Trim
            objetoRuta.NOMBRE = txtNombre.Text.Trim
            objetoRuta.ACTIVO = IIf(cbxActivo.CheckState = CheckState.Checked, "S", "N")
            objetoRuta.VENDEDOR = cbxVendedor.EditValue
            objetoRuta.VENTA = cbxTipoVenta.EditValue
            objetoRuta.FORANIA = IIf(cbxRutaForanea.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
            objetoRuta.SUCURSAL = txtSucursal.Text.Trim
            objetoRuta.TIPO = txtTipo.Text.Trim
            objetoRuta.SUBTIPO = txtSubTipo.Text.Trim
            objetoRuta.BODEGA = cmbBodega.Text.Split("-")(0).Trim 'cmbBodega.DisplayMember ' cmbBodega.Text.Trim *************************
            objetoRuta.SUBBODEGA = txtSubbodega.Text.Trim
            objetoRuta.DESCUENTO = IIf(cbxDescuento.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
            objetoRuta.BONIF = IIf(cbxBonificar.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
            objetoRuta.KILOMETRAJE = IIf(cbxKilometraje.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
            objetoRuta.IMPRESION = IIf(cbxImpresion.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
            objetoRuta.RECIBOPROPIO = IIf(cbxReciboPropio.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
            objetoRuta.CELULAR = IIf(cbxCelular.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
            objetoRuta.RENTABIL = IIf(cbxRentabilidad.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
            objetoRuta.OFERTA = IIf(cbxOferta.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
            objetoRuta.PERCRENT = txtPorcentajeRentabilidad.Text.Trim
            objetoRuta.PASARCREDITO = IIf(cbxPasarCredito.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1

            objetoRuta.TECLADO = IIf(cbxVerTeclado.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
            objetoRuta.EDITDEVPREC = IIf(cbxPrecioDevolucion.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
            objetoRuta.EDITDESC = IIf(cbxDescuento.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
            objetoRuta.PARAMS = txtParametro.Text.Trim
            objetoRuta.SEMANA = SpinSemana.Value
            objetoRuta.OBJANO = SpinAno.Value
            objetoRuta.OBJMES = SpinMes.Value
            objetoRuta.SYNCFOLD = txtFolderSincronizacion.Text.Trim
            objetoRuta.WLFOLD = txtServicioLocal.Text.Trim
            objetoRuta.FTPFOLD = txtServicioRemoto.Text.Trim
            objetoRuta.EMAIL = txtCorreo.Text.Trim
            objetoRuta.LASTIMP = SpinUltimaImportacion.Value
            objetoRuta.LASTCOM = SpinUltimaComunicacion.Value
            objetoRuta.LASTEXP = SpinUltimaExportacion.Value
            objetoRuta.IMPSTAT = spinEstadoImportacion.Value
            objetoRuta.EXPSTAT = spinEstadoExportacion.Value
            objetoRuta.COMSTAT = SpinEstadoComunicacion.Value
            objetoRuta.PARAM1 = txtParametro1.Text.Trim
            objetoRuta.PARAM2 = txtParametro2.Text.Trim
            objetoRuta.PESOLIM = txtPesoLimite.Text.Trim
            objetoRuta.INTERVALO_MAX = SpinIntervaloMaximo.Value
            objetoRuta.LECTURAS_VALID = SpinLecturasValidas.Value
            objetoRuta.INTENTOS_LECT = SpinIntentosLectura.Value
            objetoRuta.HORA_INI = SpinHoraEncendido.Value
            objetoRuta.HORA_FIN = SpinHoraApagado.Value
            objetoRuta.APLICACION_USA = cbxAplicacion.SelectedValue
            objetoRuta.PUERTO_GPS = SpinCom.Value
            objetoRuta.ES_RUTA_OFICINA = IIf(cbxRutaOficina.CheckState = CheckState.Checked, True, False) 'Numero 0 o 1
            objetoRuta.DILUIR_BON = IIf(cbxDeluirBonificacion.CheckState = CheckState.Checked, True, False) 'Numero 0 o 1
            objetoRuta.PREIMPRESION_FACTURA = IIf(cbxPreimpresionFactura.CheckState = CheckState.Checked, True, False) 'Numero 0 o 1

            Guardar = clsLnRoad_ruta.Insertar(objetoRuta) > 0


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                gBeRoadRuta.IdPropietarioBodega = Integer.Parse(cmbPropietario.EditValue)
                gBeRoadRuta.IdUbicacionTransito = Integer.Parse(txtIdUbicacionDestino.Text.Trim) 'Integer.Parse(SpinIdUbicacion.Value)***********************************
                gBeRoadRuta.CODIGO = txtCodigo.Text
                gBeRoadRuta.NOMBRE = txtNombre.Text
                gBeRoadRuta.ACTIVO = IIf(cbxActivo.CheckState = CheckState.Checked, "S", "N")
                gBeRoadRuta.VENDEDOR = cbxVendedor.EditValue.ToString()
                gBeRoadRuta.VENTA = cbxTipoVenta.EditValue.ToString()
                gBeRoadRuta.FORANIA = IIf(cbxRutaForanea.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
                gBeRoadRuta.SUCURSAL = txtSucursal.Text.Trim
                gBeRoadRuta.TIPO = txtTipo.Text.Trim
                gBeRoadRuta.SUBTIPO = txtSubTipo.Text.Trim
                gBeRoadRuta.BODEGA = cmbBodega.Properties.DisplayMember.Trim  ' cmbBodega.Text.Trim**********************************************
                gBeRoadRuta.SUBBODEGA = txtSubbodega.Text.Trim
                gBeRoadRuta.DESCUENTO = IIf(cbxDescuento.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
                gBeRoadRuta.BONIF = IIf(cbxBonificar.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
                gBeRoadRuta.KILOMETRAJE = IIf(cbxKilometraje.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
                gBeRoadRuta.IMPRESION = IIf(cbxImpresion.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
                gBeRoadRuta.RECIBOPROPIO = IIf(cbxReciboPropio.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
                gBeRoadRuta.CELULAR = IIf(cbxCelular.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
                gBeRoadRuta.RENTABIL = IIf(cbxRentabilidad.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
                gBeRoadRuta.OFERTA = IIf(cbxOferta.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
                gBeRoadRuta.PERCRENT = Double.Parse(txtPorcentajeRentabilidad.Text.Trim)
                gBeRoadRuta.PASARCREDITO = IIf(cbxPasarCredito.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1

                gBeRoadRuta.TECLADO = IIf(cbxVerTeclado.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
                gBeRoadRuta.EDITDEVPREC = IIf(cbxPrecioDevolucion.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
                gBeRoadRuta.EDITDESC = IIf(cbxDescuento.CheckState = CheckState.Checked, "S", "N") 'Numero 0 o 1
                gBeRoadRuta.PARAMS = txtParametro.Text.Trim
                gBeRoadRuta.SEMANA = SpinSemana.Value
                gBeRoadRuta.OBJANO = SpinAno.Value
                gBeRoadRuta.OBJMES = SpinMes.Value
                gBeRoadRuta.SYNCFOLD = txtFolderSincronizacion.Text.Trim
                gBeRoadRuta.WLFOLD = txtServicioLocal.Text.Trim
                gBeRoadRuta.FTPFOLD = txtServicioRemoto.Text.Trim
                gBeRoadRuta.EMAIL = txtCorreo.Text.Trim
                gBeRoadRuta.LASTIMP = Integer.Parse(SpinUltimaImportacion.Value)
                gBeRoadRuta.LASTCOM = Integer.Parse(SpinUltimaComunicacion.Value)
                gBeRoadRuta.LASTEXP = Integer.Parse(SpinUltimaExportacion.Value)
                gBeRoadRuta.IMPSTAT = spinEstadoImportacion.Value.ToString()
                gBeRoadRuta.EXPSTAT = spinEstadoExportacion.Value.ToString()
                gBeRoadRuta.COMSTAT = SpinEstadoComunicacion.Value.ToString()
                gBeRoadRuta.PARAM1 = txtParametro1.Text.Trim
                gBeRoadRuta.PARAM2 = txtParametro2.Text.Trim
                gBeRoadRuta.PESOLIM = Double.Parse(txtPesoLimite.Text)
                gBeRoadRuta.INTERVALO_MAX = Integer.Parse(SpinIntervaloMaximo.Value)
                gBeRoadRuta.LECTURAS_VALID = Integer.Parse(SpinLecturasValidas.Value)
                gBeRoadRuta.INTENTOS_LECT = Integer.Parse(SpinIntentosLectura.Value)
                gBeRoadRuta.HORA_INI = Integer.Parse(SpinHoraEncendido.Value)
                gBeRoadRuta.HORA_FIN = Integer.Parse(SpinHoraApagado.Value)
                gBeRoadRuta.APLICACION_USA = IIf(IsNothing(cbxAplicacion.SelectedValue), "0", cbxAplicacion.SelectedValue)
                gBeRoadRuta.PUERTO_GPS = Integer.Parse(SpinCom.Value)
                gBeRoadRuta.ES_RUTA_OFICINA = IIf(cbxRutaOficina.CheckState = CheckState.Checked, True, False) 'Numero 0 o 1
                gBeRoadRuta.DILUIR_BON = IIf(cbxDeluirBonificacion.CheckState = CheckState.Checked, True, False) 'Numero 0 o 1
                gBeRoadRuta.PREIMPRESION_FACTURA = IIf(cbxPreimpresionFactura.CheckState = CheckState.Checked, True, False) 'Numero 0 o 1

                Return clsLnRoad_ruta.Actualizar(gBeRoadRuta)

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            If MessageBox.Show("¿Actualizar Ruta?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Actualizar() Then
                    XtraMessageBox.Show("Se ha actualizado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarRutas.Invoke
                    Close()
                    'Hacer esto con un sub delegado.
                    frmListaRoadVendedor.Dgrid.Refresh()
                Else
                    XtraMessageBox.Show("No se puedo guardar el cambio", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If MessageBox.Show("¿Desactivar Ruta?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If clsLnRoad_ruta.Eliminar(gBeRoadRuta) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarRutas.Invoke
                    Close()
                    frmListaRoadRuta.Dgrid.Refresh()
                End If

            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then
                TablasRelacionadas("Road_ruta", txtID.Text)
            Else
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Try

    End Sub

    Private Sub cmbPropietario_SelectedIndexChanged(sender As Object, e As EventArgs)

        Try

            rrBodega = clsLnPropietario_bodega.GetBodegaByIdPropietario(cmbPropietario.EditValue)

            If Not rrBodega Is Nothing Then
                cmbBodega.Text = String.Format("{0} - {1}", rrBodega.Codigo, rrBodega.Nombre)
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked

        Try
            Dim frmbodegaUbicacionLink As New frmBodegaUbicacion_List()
            frmbodegaUbicacionLink.pIdBodega = cmbBodega.EditValue
            frmbodegaUbicacionLink.Modo = frmBodegaUbicacion_List.pModo.Seleccion
            frmbodegaUbicacionLink.ShowDialog()
            If frmbodegaUbicacionLink.pObj IsNot Nothing AndAlso frmbodegaUbicacionLink.pObj.IdUbicacion <> 0 Then
                txtIdUbicacionDestino.Text = frmbodegaUbicacionLink.pObj.IdUbicacion
                txtUbicacionDestino.Text = frmbodegaUbicacionLink.pObj.Descripcion
            End If
            frmbodegaUbicacionLink.Close()
            frmbodegaUbicacionLink.Dispose()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdUbicacionDestino_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIdUbicacionDestino.KeyPress
        Try
            If Not (Char.IsControl(e.KeyChar) And e.KeyChar <> ".") Then
                e.Handled = True
            End If

            If e.KeyChar = "." Then
                e.Handled = True
            End If

            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            End If

            If e.KeyChar = Convert.ToChar(8) AndAlso txtIdUbicacionDestino.Text.Length = 1 Then
                txtIdUbicacionDestino.Text = String.Empty
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtIdUbicacionDestino_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdUbicacionDestino.KeyDown
        Try
            If (e.KeyData = Keys.Tab Or e.KeyData = Keys.Enter) Then
                cargarUbicacion()
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Carga_Ubicacion_Transito()

        Try

            If AP.Bodega.Ubic_despacho <> "" Then
                txtIdUbicacionDestino.Text = AP.Bodega.Ubic_despacho
                cargarUbicacion()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cargarUbicacion()

        Dim pObj As New clsBeBodega_ubicacion

        Dim resultado As Boolean = False

        If String.IsNullOrEmpty(txtIdUbicacionDestino.Text.Trim()) = False Then
            pObj.IdUbicacion = CInt(txtIdUbicacionDestino.Text.Trim())
            If (pObj.IdUbicacion > 0) Then

                Dim lista As List(Of clsBeBodega_ubicacion) = clsLnBodega_ubicacion.Get_All_By_IdBodega(True, cmbBodega.EditValue, "ubicacion_despacho")
                clsLnBodega_ubicacion.Obtener(pObj)

                For it = 0 To lista.Count - 1
                    If (pObj.IdUbicacion = lista.ElementAt(it).IdUbicacion) Then
                        resultado = True
                    End If
                Next

                If (resultado) Then
                    txtIdUbicacionDestino.Text = pObj.IdUbicacion
                    txtUbicacionDestino.Text = pObj.Descripcion()
                Else
                    txtUbicacionDestino.Text = ""
                    txtIdUbicacionDestino.Text = ""
                    txtIdUbicacionDestino.Focus() ': txtIdUbicacionDestino.SelectAll()
                End If
            Else
                txtIdUbicacionDestino.Text = ""
                txtUbicacionDestino.Text = ""
                txtIdUbicacionDestino.Focus()

            End If
        Else
            txtIdUbicacionDestino.Text = ""
            txtUbicacionDestino.Text = ""
        End If

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        If cmbBodega.ItemIndex > -1 Then
            Dim idtempral As Integer = AP.IdBodega
            AP.IdBodega = cmbBodega.EditValue
            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)
            AP.IdBodega = idtempral
        End If

        txtIdUbicacionDestino.Text = ""
        txtUbicacionDestino.Text = ""
    End Sub

End Class