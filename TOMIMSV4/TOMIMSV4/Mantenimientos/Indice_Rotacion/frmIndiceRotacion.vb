Imports DevExpress.XtraEditors

Public Class frmIndiceRotacion

    Private pListObjT As New List(Of clsTabla)
    Public pObjBEJ As New clsBeIndice_rotacion
    Public Delegate Sub ListarIndices()
    Public Property InvokeListarIndices As ListarIndices

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmTipoTarima_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("indice_rotacion")

            Select Case Modo
                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnIndice_rotacion.MaxID
                    txtPrior.Value = 1
                    txtGrupo.Value = 1
                    chkActivo.Checked = True

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    'mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    clsLnIndice_rotacion.Obtener(pObjBEJ)

                    lblCodigo.Text = pObjBEJ.IdIndiceRotacion
                    txtNombre.Text = pObjBEJ.Descripcion
                    txtPrior.Value = pObjBEJ.IndicePrioridad
                    txtGrupo.Value = pObjBEJ.Grupo
                    chkActivo.Checked = pObjBEJ.Activo

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                    End If

                    'mnuEliminar.Enabled = True

            End Select

            mnuEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

        Me.Focus()
        txtNombre.Focus()

    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "DESCRIPCION").Longitud Then
                DevExpress.XtraEditors.XtraMessageBox.Show("El Nombre debe de tener como máximo " & pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "DESCRIPCION").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
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

            pObjBEJ = New clsBeIndice_rotacion

            pObjBEJ.IdIndiceRotacion = clsLnIndice_rotacion.MaxID()
            pObjBEJ.Descripcion = txtNombre.Text.Trim()
            pObjBEJ.IndicePrioridad = txtPrior.Value
            pObjBEJ.Grupo = txtGrupo.Value
            pObjBEJ.Activo = True

            Guardar = clsLnIndice_rotacion.Insertar(pObjBEJ) > 0

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

                pObjBEJ.Descripcion = txtNombre.Text.Trim()
                pObjBEJ.IndicePrioridad = txtPrior.Value
                pObjBEJ.Grupo = txtGrupo.Value
                pObjBEJ.Activo = chkActivo.Checked

                Actualizar = clsLnIndice_rotacion.Actualizar(pObjBEJ) > 0
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
            If XtraMessageBox.Show("¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListarIndices IsNot Nothing Then
                        InvokeListarIndices.Invoke()
                    End If
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarIndices IsNot Nothing Then
                InvokeListarIndices.Invoke()
            End If
            Close()
        End If
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Desactivar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjBEJ.Activo = False
                If clsLnIndice_rotacion.Actualizar(pObjBEJ) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListarIndices IsNot Nothing Then
                        InvokeListarIndices.Invoke()
                    End If
                    Close()
                    frmTipoTarima_List.Dgrid.Refresh()
                End If
            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then TablasRelacionadas("indice_rotacion", pObjBEJ.IdIndiceRotacion)
        End Try

    End Sub

    Private Sub frmIndiceRotacion_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
End Class