Imports DevExpress.XtraEditors

Public Class frmProducto_Talla

    Private pListTalla As New List(Of clsTabla)
    Public Delegate Sub Listar()
    Public Property InvokeListarTallas As Listar
    Public objTalla As New clsBeTalla

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

    Private Sub frmProducto_Talla_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            pListTalla = clsBD.GetLongitudByTabla("talla")

            IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario, AP.IdEmpresa)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblIdTalla.Text = clsLnTalla.MaxID() + 1
                    If OpcionesMenu IsNot Nothing Then
                        cmdGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    cmdActualizar.Enabled = False
                    cmdEliminar.Enabled = False
                    cmbPropietario.Enabled = True

                Case TipoTrans.Editar

                    objTalla.IsNew = False
                    lblIdTalla.Text = objTalla.IdTalla
                    'cmbPropietario.EditValue = objTalla.Propietario.IdPropietario
                    cmbPropietario.Enabled = False
                    txtNombre.Text = objTalla.Nombre
                    txtDescripcion.Text = objTalla.Descripcion
                    chkActivo.Checked = objTalla.Activo

                    cmdGuardar.Enabled = False
                    cmdActualizar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    cmdEliminar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Eliminar, True)

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        Try



            cmdGuardar.Enabled = False

            If Datos_Correctos() Then

                If MessageBox.Show("¿Guardar la Talla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListarTallas IsNot Nothing Then
                        InvokeListarTallas.Invoke()
                    End If
                    Close()
                End If

            End If

            cmdGuardar.Enabled = True


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbPropietario.Focus()
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListTalla.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                DevExpress.XtraEditors.XtraMessageBox.Show("El Nombre debe de tener como máximo " & pListTalla.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
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
            objTalla = New clsBeTalla

            objTalla.IsNew = True
            objTalla.IdTalla = clsLnTalla.MaxID() + 1
            objTalla.Nombre = txtNombre.Text
            objTalla.Descripcion = txtDescripcion.Text
            objTalla.IdPropietario = cmbPropietario.EditValue
            objTalla.Fec_agr = Now
            objTalla.User_agr = AP.UsuarioAp.IdUsuario
            objTalla.Fec_mod = Now
            objTalla.User_mod = AP.UsuarioAp.IdUsuario
            objTalla.Activo = True

            Guardar = clsLnTalla.Insertar(objTalla) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Try

            cmdActualizar.Enabled = False

            If Actualizar() Then
                XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If InvokeListarTallas IsNot Nothing Then
                    InvokeListarTallas.Invoke()
                End If
                Close()
            End If

            cmdActualizar.Enabled = True


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub


    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                objTalla.Nombre = txtNombre.Text
                objTalla.Descripcion = txtDescripcion.Text
                objTalla.Activo = chkActivo.Checked
                objTalla.Fec_mod = Now
                objTalla.User_mod = AP.UsuarioAp.IdUsuario
                '#GT18022025: user_agr, fec_agr no se deben alterar porque es un update
                Actualizar = clsLnTalla.Actualizar(objTalla) > 0

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try


    End Function

    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminar.ItemClick
        Try

            cmdEliminar.Enabled = False

            If objTalla.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf clsLnTalla.ExisteProductoLigado(objTalla.IdTalla) Then
                If MessageBox.Show("¿La talla no puede eliminarse, desea desactivarla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Datos_Correctos() Then
                        '#GT18022025: coloca activo = false
                        clsLnTalla.Desactivar(objTalla)
                        If InvokeListarTallas IsNot Nothing Then
                            InvokeListarTallas.Invoke()
                        End If
                        Close()
                        frmProducto_ClasificacionList.Dgrid.Refresh()
                    End If
                End If
            Else
                If MessageBox.Show("¿Eliminar la Talla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    clsLnTalla.Eliminar(objTalla)
                    If InvokeListarTallas IsNot Nothing Then
                        InvokeListarTallas.Invoke()
                    End If
                    Close()
                    frmProducto_ClasificacionList.Dgrid.Refresh()
                End If
            End If

            cmdEliminar.Enabled = True
        Catch ex As Exception

            cmdEliminar.Enabled = True
            If ex.HResult = -2146233088 Then TablasRelacionadas("producto_talla", objTalla.IdTalla)
        End Try
    End Sub
End Class