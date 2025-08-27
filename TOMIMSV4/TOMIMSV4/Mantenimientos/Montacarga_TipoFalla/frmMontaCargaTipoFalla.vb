Imports DevExpress.XtraEditors

Public Class frmMontaCargaTipoFalla

    Private pListObjT As New List(Of clsTabla)
    Public ObjetoMontaCargaTipoFalla As New clsBeMontacarga_tipoFalla
    Public Delegate Sub cargarListadeFalladeMontacargaxEmpresa()
    Public Property InvokeListarFallaMontaCarga As cargarListadeFalladeMontacargaxEmpresa

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

    Private Sub frmMontaCargaTipoFalla_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("montacarga_tipoFalla")

            cbxEmpresa.EditValue = AP.IdEmpresa
            If Not IMS.Listar_Empresas(cbxEmpresa) Then
                XtraMessageBox.Show("No hay empresas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Select Case Modo

                Case TipoTrans.Nuevo

                    txtID.Text = clsLnMontacarga_tipoFalla.MaxID
                    txtNombre.Text = String.Empty
                    chkActivo.CheckState = CheckState.Checked
                    mnuActualizar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                Case TipoTrans.Editar

                    txtID.Text = ObjetoMontaCargaTipoFalla.IdTipoFalla
                    If (clsLnMontacarga_tipoFalla.Obtener(ObjetoMontaCargaTipoFalla)) Then
                        txtNombre.Text = ObjetoMontaCargaTipoFalla.Nombre
                        chkActivo.CheckState = IIf(ObjetoMontaCargaTipoFalla.Activo = 1, CheckState.Checked, CheckState.Unchecked)

                        If OpcionesMenu IsNot Nothing Then
                            mnuActualizar.Enabled = OpcionesMenu.Modificar
                        End If

                        mnuGuardar.Enabled = False
                    Else
                        XtraMessageBox.Show("No se puedo cargar la información de la montacarga", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        Me.txtNombre.Focus()

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        Try

            If Datos_Correctos() Then
                If MessageBox.Show("¿Guardar Tipo de Falla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Guardar() Then
                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        InvokeListarFallaMontaCarga.Invoke
                        Close()
                    Else
                        XtraMessageBox.Show("Error al guardar el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function Datos_Correctos() As Boolean
        Datos_Correctos = False
        Try
            If String.IsNullOrEmpty(txtNombre.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try
            If Datos_Correctos() Then
                If Actualizar() Then
                    XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarFallaMontaCarga.Invoke
                    Close()
                Else
                    XtraMessageBox.Show("No se logró actualizar el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Try

            ObjetoMontaCargaTipoFalla.IdTipoFalla = clsLnMontacarga_tipoFalla.MaxID
            ObjetoMontaCargaTipoFalla.IdEmpresa = cbxEmpresa.EditValue
            ObjetoMontaCargaTipoFalla.Nombre = txtNombre.Text
            ObjetoMontaCargaTipoFalla.Activo = IIf(chkActivo.CheckState = CheckState.Checked, 1, 0)
            'MsgBox(String.Format("{0}{1} {2} {3}", ObjetoMontaCargaTipoFalla.IdTipoFalla, ObjetoMontaCargaTipoFalla.IdEmpresa, ObjetoMontaCargaTipoFalla.Nombre, ObjetoMontaCargaTipoFalla.Activo))
            Return clsLnMontacarga_tipoFalla.Insertar(ObjetoMontaCargaTipoFalla) > 0
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Function Actualizar() As Boolean

        Try

            ObjetoMontaCargaTipoFalla.IdEmpresa = Integer.Parse(cbxEmpresa.EditValue)
            ObjetoMontaCargaTipoFalla.Nombre = txtNombre.Text
            ObjetoMontaCargaTipoFalla.Activo = IIf(chkActivo.CheckState = CheckState.Checked, 1, 0)
            Return clsLnMontacarga_tipoFalla.Actualizar(ObjetoMontaCargaTipoFalla) > 0

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

    End Sub

    Private Sub frmMontaCargaTipoFalla_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class