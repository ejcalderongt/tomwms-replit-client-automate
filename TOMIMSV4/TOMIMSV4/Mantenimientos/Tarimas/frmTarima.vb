Imports DevExpress.XtraEditors

Public Class frmTarima


    Private pListObjT As New List(Of clsTabla)
    Public pObjBEJ As New clsBeTarimas
    Public Delegate Sub ListarTarimas()
    Public Property InvokeListarTarimas As ListarTarimas

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmTarima_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("tarimas")

            IMS.Listar_Empresas(cmbEmpresa)
            IMS.Listar_TipoTarima(cmbTipoTarima)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnTarimas.MaxID
                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    clsLnTarimas.Obtener(pObjBEJ)

                    lblCodigo.Text = pObjBEJ.IdTarima

                    If pObjBEJ.IdEmpresa > 0 Then
                        cmbEmpresa.EditValue = pObjBEJ.IdEmpresa
                    End If

                    If pObjBEJ.IdTipoTarima > 0 Then
                        cmbTipoTarima.EditValue = pObjBEJ.IdTipoTarima
                    End If

                    txtCodigo.Text = pObjBEJ.Codigo

                    chkDisponible.Checked = pObjBEJ.Disponible
                    chkActivo.Checked = pObjBEJ.Activo

                    User_agrTextEdit.Text = pObjBEJ.User_agr
                    Fec_agrDateEdit.Text = pObjBEJ.Fec_agr
                    User_modTextEdit.Text = pObjBEJ.User_mod
                    Fec_modDateEdit.Text = pObjBEJ.Fec_mod

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

        Me.Focus()
        txtCodigo.Focus()

    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtCodigo.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Código", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCodigo.Focus()
            ElseIf txtCodigo.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO").Longitud Then
                DevExpress.XtraEditors.XtraMessageBox.Show("El Código debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigo.Focus()
            Else
                Datos_Correctos = True
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

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            pObjBEJ = New clsBeTarimas()

            pObjBEJ.IdTarima = clsLnTarimas.MaxID()

            If cmbEmpresa.ItemIndex > -1 Then
                pObjBEJ.IdEmpresa = cmbEmpresa.EditValue
            End If

            If cmbTipoTarima.ItemIndex > -1 Then
                pObjBEJ.IdTipoTarima = cmbTipoTarima.EditValue
            End If

            pObjBEJ.Codigo = txtCodigo.Text.Trim()

            pObjBEJ.User_agr = AP.UsuarioAp.IdUsuario
            pObjBEJ.Fec_agr = Now
            pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
            pObjBEJ.Fec_mod = Now
            pObjBEJ.Disponible = chkDisponible.Checked
            pObjBEJ.Activo = True

            Guardar = clsLnTarimas.Insertar(pObjBEJ) > 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                If cmbEmpresa.ItemIndex > -1 Then
                    pObjBEJ.IdEmpresa = cmbEmpresa.EditValue
                End If

                If cmbTipoTarima.ItemIndex > -1 Then
                    pObjBEJ.IdTipoTarima = cmbTipoTarima.EditValue
                End If

                pObjBEJ.Codigo = txtCodigo.Text.Trim()

                pObjBEJ.Disponible = chkDisponible.Checked
                pObjBEJ.Activo = chkActivo.Checked

                pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
                pObjBEJ.Fec_mod = Now

                Actualizar = clsLnTarimas.Actualizar(pObjBEJ) > 0

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

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        If Datos_Correctos() Then
            If XtraMessageBox.Show("¿Guardar la Tarima?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListarTarimas IsNot Nothing Then
                        InvokeListarTarimas.Invoke()
                    End If
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarTarimas IsNot Nothing Then
                InvokeListarTarimas.Invoke()
            End If
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Desactivar la Tarima?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjBEJ.Activo = False
                If clsLnTarimas.Actualizar(pObjBEJ) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListarTarimas IsNot Nothing Then
                        InvokeListarTarimas.Invoke()
                    End If
                    Close()
                    frmTarima_Lista.Dgrid.Refresh()
                End If
            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then TablasRelacionadas("tarima", pObjBEJ.IdTarima)
        End Try

    End Sub

    Private Sub cmbTipoTarima_SelectedValueChanged(sender As Object, e As EventArgs)

        Try

            If cmbTipoTarima.ItemIndex > -1 Then

                Dim ObjLn As New clsLnTipo_tarima
                Dim Obj As New clsBeTipo_tarima

                Obj.IdTipoTarima = cmbTipoTarima.EditValue

                ObjLn.Obtener(Obj)

                If Obj.IdTipoTarima > 0 Then

                    txtAlto.Value = Obj.Alto
                    txtLargo.Value = Obj.Largo
                    txtAncho.Value = Obj.Ancho
                    txtCargaDinamica.Value = Obj.CargaDinamica
                    txtCargaEstatica.Value = Obj.CargaEstatica
                    txtCargaEstanteria.Value = Obj.CargaEstanterias
                    txtEntradasPaleta.Value = Obj.EntradasTransPaleta
                    txtPesoPromedio.Value = Obj.PesoPromedio

                    GroupControl5.Text = "Valores de " & Obj.Nombre

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

    Private Sub frmTarima_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
End Class