Imports DevExpress.XtraEditors

Public Class frmRoadxVendedor
    Private pListObjT As New List(Of clsTabla)
    Public pObj As New clsBeRoad_p_vendedor
    Public pListObjDB As List(Of clsBeRoad_p_vendedor)
    Public pIdVendedor As Integer
    Public Delegate Sub listar_Vendedores()
    Public Property InvokeListarVendedores As listar_Vendedores

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

    Private Sub frmRoadxVendedor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("road_p_vendedor")
            IMS.Listar_RoadRutas(cmbRuta)
            'clsLnRoad_ruta.Listar_RoadRutas(cmbRuta, clsLnRoad_ruta.TipoRuta.Todas)

            Select Case Modo

                Case TipoTrans.Nuevo

                    limpiarFormulario()
                    txtIDVendedor.Text = clsLnRoad_p_vendedor.MaxID()

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    txtCodigo.Enabled = True

                Case TipoTrans.Editar

                    pObj = clsLnRoad_p_vendedor.GetSingle(pObj.IdVendedor)

                    txtIDVendedor.Text = pObj.IdVendedor
                    txtCodigo.Text = pObj.Codigo
                    txtNombre.Text = pObj.Nombre
                    txtClave.Text = pObj.Clave

                    Try
                        cmbRuta.EditValue = pObj.IdRuta
                    Catch ex As Exception
                    End Try

                    cmbRuta.EditValue = pObj.IdRuta
                    txtSpinNivel.Text = pObj.Nivel
                    txtSpinPrecio.Text = pObj.Nivelprecio
                    txtBodega.Text = pObj.Bodega
                    txtSubbodega.Text = pObj.Subbodega
                    txtCodVehiculo.Text = pObj.Cod_vehiculo
                    txtLiquidado.Text = pObj.Liquidando
                    txtDatapickerFechaUltimaLiquidacion.DateTime = pObj.Ultima_fecha_liq
                    cbxBloquear.CheckState = IIf(pObj.Bloqueado, CheckState.Checked, CheckState.Unchecked)
                    txtSpinDevolucionSAP.Text = pObj.Devolucion_sap

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

                    txtCodigo.Enabled = False

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        Focus()
        txtNombre.Focus()

    End Sub

    Public Sub limpiarFormulario()

        txtIDVendedor.Text = clsLnRoad_p_vendedor.MaxID().ToString()
        txtCodigo.Text = ""
        txtNombre.Text = ""
        txtClave.Text = ""
        txtBodega.Text = ""
        txtSubbodega.Text = ""
        txtCodVehiculo.Text = ""
        txtLiquidado.Text = ""
        txtDatapickerFechaUltimaLiquidacion.DateTime = Date.Now()
        cbxBloquear.CheckState = CheckState.Unchecked
        txtCodigo.Focus()

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        Try

            If Datos_Correctos() Then

                If MessageBox.Show("¿Guardar Vendedor?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Guardar() Then
                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        InvokeListarVendedores.Invoke
                        Close()

                    End If

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

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        txtSpinNivel.Text = txtSpinNivel.Text.Replace(".", "")
        txtSpinPrecio.Text = txtSpinPrecio.Text.Replace(".", "")

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

            ElseIf String.IsNullOrEmpty(txtClave.Text.Trim) Then
                XtraMessageBox.Show("Ingrese clave.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtClave.Focus()

            ElseIf txtClave.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "CLAVE").Longitud Then
                XtraMessageBox.Show("La Clave debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "CLAVE").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtClave.Focus()

            ElseIf txtRuta.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "RUTA").Longitud Then
                XtraMessageBox.Show("La Clave debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "RUTA").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtRuta.Focus()

            ElseIf txtBodega.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "BODEGA").Longitud Then
                XtraMessageBox.Show("La Bodega debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "BODEGA").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtBodega.Focus()

            ElseIf txtSubbodega.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "SUBBODEGA").Longitud Then
                XtraMessageBox.Show("La Sub Bodega debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "SUBBODEGA").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSubbodega.Focus()

            ElseIf txtCodVehiculo.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "COD_VEHICULO").Longitud Then
                XtraMessageBox.Show("El Código del Vehículo debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "COD_VEHICULO").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodVehiculo.Focus()

            ElseIf txtLiquidado.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "LIQUIDANDO").Longitud Then
                XtraMessageBox.Show("El Liquidando debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "LIQUIDANDO").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtLiquidado.Focus()

            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Function

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            pObj.IdVendedor = clsLnRoad_p_vendedor.MaxID
            pObj.Codigo = clsLnRoad_p_vendedor.MaxID() 'txtCodigo.Text
            pObj.Nombre = txtNombre.Text
            pObj.Clave = txtClave.Text
            pObj.IdRuta = cmbRuta.EditValue
            pObj.Ruta = txtRuta.Text
            pObj.Nivel = Integer.Parse(txtSpinNivel.Text)
            pObj.Nivelprecio = Integer.Parse(txtSpinPrecio.Text)
            pObj.Bodega = txtBodega.Text
            pObj.Subbodega = txtSubbodega.Text
            pObj.Cod_vehiculo = txtCodVehiculo.Text
            pObj.Liquidando = txtLiquidado.Text
            pObj.Ultima_fecha_liq = Date.Parse(txtDatapickerFechaUltimaLiquidacion.Text)
            pObj.Bloqueado = IIf(cbxBloquear.CheckState = CheckState.Checked, 1, 0)
            pObj.Devolucion_sap = txtSpinDevolucionSAP.Text

            Guardar = clsLnRoad_p_vendedor.Insertar(pObj) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObj.Codigo = txtCodigo.Text.Trim
                pObj.Nombre = txtNombre.Text.Trim
                pObj.Clave = txtClave.Text.Trim
                pObj.Ruta = txtRuta.Text
                pObj.IdRuta = cmbRuta.EditValue
                pObj.Nivel = Integer.Parse(txtSpinNivel.Text.Trim)
                pObj.Nivelprecio = Integer.Parse(txtSpinPrecio.Text.Trim)
                pObj.Bodega = txtBodega.Text.Trim
                pObj.Subbodega = txtSubbodega.Text.Trim
                pObj.Cod_vehiculo = txtCodVehiculo.Text.Trim
                pObj.Liquidando = txtLiquidado.Text.Trim
                pObj.Bloqueado = IIf(cbxBloquear.CheckState = CheckState.Checked, True, False)
                pObj.Devolucion_sap = Integer.Parse(txtSpinDevolucionSAP.Text.Trim)

                Return clsLnRoad_p_vendedor.Actualizar(pObj)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If MessageBox.Show("¿Desactivar Vendedor?" & pObj.IdVendedor, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If clsLnRoad_p_vendedor.Eliminar(pObj) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarVendedores.Invoke
                    Close()
                    'delegate sub
                    frmListaRoadVendedor.Dgrid.Refresh()
                End If

            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then
                TablasRelacionadas("Road_p_vendedor", txtIDVendedor.Text)
            Else
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            If MessageBox.Show("¿Actualizar Vendedor?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Actualizar() Then
                    XtraMessageBox.Show("Se ha actualizado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarVendedores.Invoke
                    Close()
                    'delegate sub
                    frmListaRoadVendedor.Dgrid.Refresh()
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

    Private Sub frmRoadxVendedor_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class