Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmProducto_Estilo

    Private pListEstilo As New List(Of clsTabla)
    Public Delegate Sub Listar()
    Public Property InvokeListarEstilos As Listar
    Public objEstilo As New clsBeEstilo

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

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        Try

            cmdGuardar.Enabled = False

            If Datos_Correctos() Then

                If MessageBox.Show("¿Guardar el Estilo?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListarEstilos IsNot Nothing Then
                        InvokeListarEstilos.Invoke()
                    End If
                    Close()
                End If

            End If

            cmdGuardar.Enabled = True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
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
            ElseIf txtNombre.Text.Count > pListEstilo.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                DevExpress.XtraEditors.XtraMessageBox.Show("El Nombre debe de tener como máximo " & pListEstilo.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
            objEstilo = New clsBeEstilo

            objEstilo.IsNew = True
            objEstilo.IdEstilo = clsLnEstilo.MaxID() + 1
            objEstilo.Nombre = txtNombre.Text
            objEstilo.Descripcion = txtDescripcion.Text
            objEstilo.IdPropietario = cmbPropietario.EditValue
            objEstilo.Fec_agr = Now
            objEstilo.User_agr = AP.UsuarioAp.IdUsuario
            objEstilo.Fec_mod = Now
            objEstilo.User_mod = AP.UsuarioAp.IdUsuario
            objEstilo.Activo = True

            Guardar = clsLnEstilo.Insertar(objEstilo) > 0

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
                If InvokeListarEstilos IsNot Nothing Then
                    InvokeListarEstilos.Invoke()
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

                objEstilo.Nombre = txtNombre.Text
                objEstilo.Descripcion = txtDescripcion.Text
                objEstilo.Activo = chkActivo.Checked
                objEstilo.Fec_mod = Now
                objEstilo.User_mod = AP.UsuarioAp.IdUsuario
                '#GT18022025: user_agr, fec_agr no se deben alterar porque es un update
                Actualizar = clsLnEstilo.Actualizar(objEstilo) > 0

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

            If objEstilo.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf clsLnEstilo.ExisteProductoLigado(objEstilo.IdEstilo) Then
                If MessageBox.Show("¿La talla no puede eliminarse, desea desactivarla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Datos_Correctos() Then
                        '#GT18022025: coloca activo = false
                        clsLnEstilo.Desactivar(objEstilo)
                        If InvokeListarEstilos IsNot Nothing Then
                            InvokeListarEstilos.Invoke()
                        End If
                        Close()
                        frmProducto_ClasificacionList.Dgrid.Refresh()
                    End If
                End If
            Else
                If MessageBox.Show("¿Eliminar la Talla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    clsLnEstilo.Eliminar(objEstilo)
                    If InvokeListarEstilos IsNot Nothing Then
                        InvokeListarEstilos.Invoke()
                    End If
                    Close()
                    frmProducto_ClasificacionList.Dgrid.Refresh()
                End If
            End If

            cmdEliminar.Enabled = True
        Catch ex As Exception

            cmdEliminar.Enabled = True
            If ex.HResult = -2146233088 Then TablasRelacionadas("producto_estilo", objEstilo.IdEstilo)
        End Try
    End Sub

End Class