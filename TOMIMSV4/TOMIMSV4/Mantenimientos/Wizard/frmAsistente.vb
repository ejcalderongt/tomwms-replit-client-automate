Imports DevExpress.XtraEditors
Public Class frmAsistente

    Public EmpresaConfigurada As Boolean
    Public PaisConfigurado As Boolean
    Public BodegaConfigurada As Boolean
    Public UsuariosConfigurados As Boolean

    Private Sub cmdCancelar_Click(sender As Object, e As EventArgs) Handles cmdCancelar.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Function ConfiguraEmpresa() As Boolean

        ConfiguraEmpresa = False

        Try

            Dim Res As New DialogResult
            Dim Emp As New frmEmpresa(frmEmpresa.TipoTrans.Nuevo)

            Res = Emp.ShowDialog()

            If Res = DialogResult.OK Then
                PicEmpresa.Image = My.Resources.cheked
                ConfiguraEmpresa = True
            End If

            Application.DoEvents()
            Me.Refresh()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function


    Private Function ConfiguraPais() As Boolean

        ConfiguraPais = False

        Try

            Dim Res As New DialogResult
            Dim Emp As New frmPais(frmPais.TipoTrans.Nuevo)

            Res = Emp.ShowDialog()

            If Res = DialogResult.OK Then
                ConfiguraPais = True
            End If

            Application.DoEvents()
            Me.Refresh()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function


    Private Function ConfiguraBodega() As Boolean

        ConfiguraBodega = False

        Try

            Dim Res As New DialogResult
            Dim Emp As New frmBodega(frmBodega.TipoTrans.Nuevo)

            Res = Emp.ShowDialog()

            If Res = DialogResult.OK Then
                picBodega.Image = My.Resources.cheked
                ConfiguraBodega = True
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


    'Private Function ConfiguraUsuario() As Boolean

    '    'ConfiguraUsuario = False

    '    'Try

    '    '    Dim Res As New DialogResult
    '    '    Dim Emp As New frmUsu(frmBodega.TipoTrans.Nuevo)

    '    '    Res = Emp.ShowDialog()

    '    '    If Res = DialogResult.OK Then
    '    '        picBodega.Image = My.Resources.cheked
    '    '        ConfiguraUsuario = True
    '    '    End If

    '    'Catch ex As Exception
    '    '    MsgBox(ex.Message)
    '    'End Try

    'End Function


    Private Sub cmdSiguiente_Click(sender As Object, e As EventArgs) Handles cmdSiguiente.Click

        Try

            If Not EmpresaConfigurada Then

                If ConfiguraEmpresa() Then

                    If ConfiguraPais() Then

                        If ConfiguraBodega() Then
                            XtraMessageBox.Show("Configuración aplicada correctamente. La aplicación se reiniciarà, vuelva a iniciar sesión.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Application.Restart()
                        End If

                    End If

                End If

            ElseIf Not BodegaConfigurada Then

                If ConfiguraPais() Then

                    If ConfiguraBodega() Then
                        XtraMessageBox.Show("Configuración aplicada correctamente. La aplicación se reiniciarà, vuelva a iniciar sesión.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Application.Restart()
                    End If

                End If

            ElseIf Not UsuariosConfigurados Then

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


    Private Sub frmAsistente_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            If Not EmpresaConfigurada Then
                PicEmpresa.Image = My.Resources.unchecked
            Else
                PicEmpresa.Image = My.Resources.cheked
            End If

            If Not BodegaConfigurada Then
                picBodega.Image = My.Resources.unchecked
            Else
                picBodega.Image = My.Resources.cheked
            End If

            If Not UsuariosConfigurados Then
                picUsuario.Image = My.Resources.unchecked
            Else
                picUsuario.Image = My.Resources.cheked
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


    Dim lContadorE As Integer = 0
    Dim lContadorB As Integer = 0
    Dim lContadorU As Integer = 0


    Private Sub PicEmpresa_Click(sender As Object, e As EventArgs) Handles PicEmpresa.Click

        lContadorE += 1
        lContadorB = 0
        lContadorU = 0

        picBodega.Image = My.Resources.unchecked
        picUsuario.Image = My.Resources.unchecked

        If (lContadorE Mod 2) = 0 Then
            PicEmpresa.Image = My.Resources.unchecked
        Else
            PicEmpresa.Image = My.Resources.cheked
        End If

    End Sub


    Private Sub picBodega_Click(sender As Object, e As EventArgs) Handles picBodega.Click

        lContadorB += 1
        lContadorE = 0
        lContadorU = 0

        PicEmpresa.Image = My.Resources.unchecked
        picUsuario.Image = My.Resources.unchecked

        If (lContadorB Mod 2) = 0 Then
            picBodega.Image = My.Resources.unchecked
        Else
            picBodega.Image = My.Resources.cheked
        End If

    End Sub


    Private Sub picUsuario_Click(sender As Object, e As EventArgs) Handles picUsuario.Click

        lContadorU += 1
        lContadorB = 0
        lContadorE = 0

        picBodega.Image = My.Resources.unchecked
        PicEmpresa.Image = My.Resources.unchecked

        If (lContadorU Mod 2) = 0 Then
            picUsuario.Image = My.Resources.unchecked
        Else
            picUsuario.Image = My.Resources.cheked
        End If

    End Sub

End Class