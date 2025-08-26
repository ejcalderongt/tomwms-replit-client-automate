Imports DevExpress.XtraEditors

Public Class frmMontacarga

    Private pListObjT As New List(Of clsTabla)
    Public BeMontacarga As New clsBeMontacarga
    Public Delegate Sub cargarListadeMontacargaxEmpresa()
    Public Property InvokeListarMontacarga As cargarListadeMontacargaxEmpresa

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

    Private Sub frmMontacarga_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        limpiarFormulario()

        Try

            pListObjT = clsBD.GetLongitudByTabla("montacarga")

            cbxEmpresa.EditValue = AP.IdEmpresa

            If Not IMS.Listar_Empresas(cbxEmpresa) Then
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Select Case Modo

                Case TipoTrans.Nuevo

                    txtID.Text = clsLnMontacarga.MaxID()

                    If OpcionesMenu IsNot Nothing Then
                        btnGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    btnActualizar.Enabled = False
                    btnEliminar.Enabled = False
                    btnImprimeCodigoBarra.Enabled = False
                    Listar_Bodegas(TipoTrans.Nuevo)

                Case TipoTrans.Editar

                    txtID.Text = BeMontacarga.IdMontacarga

                    If (clsLnMontacarga.Obtener(BeMontacarga)) Then
                        txtNombre.Text = BeMontacarga.Nombre
                        txtModelo.Text = BeMontacarga.Modelo
                        txtSerie.Text = BeMontacarga.Serie
                        nudCapacidadBasica.Value = BeMontacarga.Capacidad_basica
                        nudDesplazamientoMotor.Value = BeMontacarga.Desplazamiento_motor
                        nudcostohora.Value = BeMontacarga.Costo_Hora
                        txtTipoCombustible.Text = BeMontacarga.Tipo_combustible
                        txtTipoMontacarga.Text = BeMontacarga.Tipo_montacarga
                        dcFechaCompra.Text = BeMontacarga.Fecha_compra
                        dcFechaInicioOperaciones.Text = BeMontacarga.Fecha_inicio_operaciones
                        dcProximoMantenimiento.Text = BeMontacarga.Proximo_mantenimiento
                        txtNivelDesde.Value = BeMontacarga.Nivel_Desde
                        txtNivelHasta.Value = BeMontacarga.Nivel_Hasta
                        Listar_Bodegas(TipoTrans.Editar)
                    Else
                        XtraMessageBox.Show("No se puedo cargar la informacion de la montacarga", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    btnGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        btnActualizar.Enabled = OpcionesMenu.Modificar
                        btnEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    btnImprimeCodigoBarra.Enabled = False

            End Select

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        txtNombre.Focus()

    End Sub

    Private Sub btnGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnGuardar.ItemClick

        If Datos_Correctos() Then

            If XtraMessageBox.Show("¿Guardar montacarga?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarMontacarga.Invoke
                    Close()
                Else
                    XtraMessageBox.Show("No se pudo guardar el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End If

        End If

    End Sub

    Private Sub btnActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnActualizar.ItemClick

        If Datos_Correctos() Then

            If XtraMessageBox.Show("¿Actualizar Montacarga?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Actualizar() Then
                    XtraMessageBox.Show("Se Actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarMontacarga.Invoke
                    Close()
                Else
                    XtraMessageBox.Show("No se pudo Actualizar el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End If

        End If

    End Sub

    Private Sub Dgrid_DoubleClick(sender As Object, e As EventArgs) Handles Dgrid.DoubleClick

        Dim objetoDetalle As New clsBeMontacarga_bodega
        Dim tabla As DataTable = Dgrid.DataSource
        Dim filaseleccionada As Integer = GridView1.GetSelectedRows()(0)

        tabla.Rows(filaseleccionada).BeginEdit()
        Dim valor As Boolean = tabla.Rows(filaseleccionada)("ASIGNAR")
        tabla.Rows(filaseleccionada)("ASIGNAR") = IIf(valor, False, True)
        tabla.Rows(filaseleccionada).EndEdit()
        tabla.Rows(filaseleccionada).AcceptChanges()

        objetoDetalle.IdMontacargaBodega = tabla.Rows(filaseleccionada)("ID")
        objetoDetalle.IdMontacarga = Integer.Parse(txtID.Text)
        objetoDetalle.IdBodega = tabla.Rows(filaseleccionada)("IDBODEGA")
        objetoDetalle.User_agr = tabla.Rows(filaseleccionada)("USUARIO AGREGO")
        objetoDetalle.Fec_agr = tabla.Rows(filaseleccionada)("FECHA DE REGISTRO")
        objetoDetalle.User_mod = AP.UsuarioAp.Nombres
        objetoDetalle.Fec_mod = Date.Now
        objetoDetalle.Activo = tabla.Rows(filaseleccionada)("ASIGNAR")

        Try
            clsLnMontacarga_bodega.Actualizar(objetoDetalle)
        Catch ex As Exception
            XtraMessageBox.Show("No se pudo cambiar el estado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            tabla.Rows(filaseleccionada)("ASIGNAR") = IIf(valor, False, True)
        End Try

    End Sub


#Region "Metodos propios"

    Private Function Guardar() As Boolean

        Try

            SetObjMontacarga()

            BeMontacarga.IdMontacarga = clsLnMontacarga.MaxID()

            If (clsLnMontacarga.Insertar(BeMontacarga) > 0) Then
                Return Guardar_Asignacion_Bodega()
            End If

            Return False ' No se pudo guardar en la tabla Montacarga

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try

    End Function


    Private Function Actualizar() As Boolean

        SetObjMontacarga()

        BeMontacarga.IdMontacarga = Integer.Parse(txtID.Text)

        Try
            If clsLnMontacarga.Actualizar(BeMontacarga) > 0 Then
                Return Guardar_Asignacion_Bodega()
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Function


    Private Function Guardar_Asignacion_Bodega() As Boolean

        Guardar_Asignacion_Bodega = False

        Try

            Dim detalle As clsBeMontacarga_bodega
            Dim DT As DataTable = Dgrid.DataSource

            For it As Integer = 0 To (DT.Rows.Count - 1)

                detalle = New clsBeMontacarga_bodega()
                detalle.IdMontacargaBodega = clsLnMontacarga_bodega.MaxID()
                detalle.IdMontacarga = Integer.Parse(txtID.Text)
                detalle.IdBodega = DT.Rows(it)("IDBODEGA")
                detalle.User_agr = AP.UsuarioAp.IdUsuario
                detalle.Fec_agr = Date.Now   'Cambiar por fecha de Servidor
                detalle.User_mod = AP.UsuarioAp.IdUsuario
                detalle.Fec_mod = Date.Now
                detalle.Activo = DT.Rows(it)("ASIGNAR")

                If Not clsLnMontacarga_bodega.Existe_By_IdBodega(detalle.IdMontacarga, detalle.IdBodega) Then
                    clsLnMontacarga_bodega.Insertar(detalle)
                Else
                    If Not detalle.Activo Then
                        clsLnMontacarga_bodega.Eliminar(detalle)
                    Else
                        'clsLnMontacarga_bodega.Eliminar(detalle)
                    End If

                End If


            Next

            Guardar_Asignacion_Bodega = True

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Private Sub Listar_Bodegas(tipoTrans As TipoTrans)

        Dim DT As New DataTable("Asignacion")
        DT.Columns.Add("ID", GetType(Integer))
        DT.Columns("ID").ColumnMapping = MappingType.Hidden
        DT.Columns.Add("IDBODEGA", GetType(Integer))
        DT.Columns("IDBODEGA").ColumnMapping = MappingType.Hidden

        DT.Columns.Add("ASIGNAR", GetType(Boolean))
        DT.Columns.Add("BODEGA", GetType(String))
        DT.Columns.Add("USUARIO AGREGO", GetType(String))
        DT.Columns("USUARIO AGREGO").ColumnMapping = MappingType.Hidden
        DT.Columns.Add("FECHA DE REGISTRO", GetType(DateTime))
        DT.Columns("FECHA DE REGISTRO").ColumnMapping = MappingType.Hidden
        DT.Columns.Add("USUARIO MODIFICO", GetType(String))
        DT.Columns("USUARIO MODIFICO").ColumnMapping = MappingType.Hidden
        DT.Columns.Add("FECHA DE MODIFICACION", GetType(DateTime))
        DT.Columns("FECHA DE MODIFICACION").ColumnMapping = MappingType.Hidden

        Try

            Dim lBodegas As New List(Of clsBeBodega)
            Dim lMontaCargaBodegas As New List(Of clsBeMontacarga_bodega)

            lBodegas = clsLnBodega.Get_All_By_IdEmpresa(AP.IdEmpresa)
            lMontaCargaBodegas = clsLnMontacarga_bodega.Get_All_By_IdMontaCarga(txtID.Text)
            Dim IdxMCB As Integer = -1
            Dim vSeleccionar As Boolean = False

            For Each Bod As clsBeBodega In lBodegas

                If Not lMontaCargaBodegas Is Nothing Then

                    If lMontaCargaBodegas.Count > 0 Then

                        IdxMCB = lMontaCargaBodegas.FindIndex(Function(X) X.IdBodega = Bod.IdBodega)

                        If IdxMCB >= 0 Then
                            vSeleccionar = lMontaCargaBodegas(IdxMCB).Activo
                        End If

                    End If

                End If

                DT.Rows.Add(0, Bod.IdBodega, vSeleccionar, Bod.Nombre, "", Now, "", Now)

            Next

            Dgrid.DataSource = DT

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    'Este objeto se crea tanto para actualizar como para crear un nuevo Montacarga
    Private Sub SetObjMontacarga()

        BeMontacarga.IdEmpresa = AP.IdEmpresa
        BeMontacarga.Nombre = txtNombre.Text
        BeMontacarga.Modelo = txtModelo.Text
        BeMontacarga.Serie = txtSerie.Text
        BeMontacarga.Capacidad_basica = Double.Parse(nudCapacidadBasica.Value)
        BeMontacarga.Desplazamiento_motor = Double.Parse(nudDesplazamientoMotor.Value)
        BeMontacarga.Costo_Hora = Double.Parse(nudcostohora.Value)
        BeMontacarga.Tipo_combustible = txtTipoCombustible.Text
        BeMontacarga.Tipo_montacarga = txtTipoMontacarga.Text
        BeMontacarga.Fecha_compra = dcFechaCompra.DateTime
        BeMontacarga.Fecha_inicio_operaciones = dcFechaInicioOperaciones.DateTime
        BeMontacarga.Proximo_mantenimiento = dcProximoMantenimiento.DateTime
        BeMontacarga.Nivel_Desde = txtNivelDesde.Value
        BeMontacarga.Nivel_Hasta = txtNivelDesde.Value

    End Sub

    Public Sub limpiarFormulario()
        txtID.Text = ""
        txtNombre.Text = ""
        txtModelo.Text = ""
        txtSerie.Text = ""
        nudCapacidadBasica.Value = 0.0
        nudDesplazamientoMotor.Value = 0.0
        txtTipoCombustible.Text = ""
        txtTipoMontacarga.Text = ""
        dcFechaCompra.Text = Date.Now()
        dcFechaInicioOperaciones.Text = dcFechaCompra.Text
        dcProximoMantenimiento.Text = dcFechaCompra.Text
    End Sub


    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf String.IsNullOrEmpty(txtModelo.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Modelo.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtModelo.Focus()
            ElseIf txtModelo.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "MODELO").Longitud Then
                XtraMessageBox.Show(String.Format("El Modelo debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "MODELO").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtModelo.Focus()
            ElseIf String.IsNullOrEmpty(txtSerie.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Serie.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSerie.Focus()
            ElseIf txtSerie.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "SERIE").Longitud Then
                XtraMessageBox.Show(String.Format("La Serie debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "SERIE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSerie.Focus()

            ElseIf txtTipoCombustible.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "TIPO_COMBUSTIBLE").Longitud Then
                XtraMessageBox.Show(String.Format("El Tipo Combustible debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "TIPO_COMBUSTIBLE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtTipoCombustible.Focus()

            ElseIf txtTipoMontacarga.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "TIPO_MONTACARGA").Longitud Then
                XtraMessageBox.Show(String.Format("La Serie debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "TIPO_MONTACARGA").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtTipoMontacarga.Focus()

            Else

                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

#End Region


    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = False
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView1.OptionsSelection.EnableAppearanceHideSelection = True
            GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.FocusedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.Options.UseBackColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmMontacarga_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub btnImprimeCodigoBarra_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnImprimeCodigoBarra.ItemClick

    End Sub

End Class