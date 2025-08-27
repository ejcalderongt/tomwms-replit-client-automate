Imports DevExpress.XtraEditors

Public Class frmBaseDatos

    'Private pObjC As WCFConfiguracion.Configuracion


    Private Sub Validar()

        If String.IsNullOrEmpty(txtServer.Text.Trim) Then
            Throw New Exception("Debe Ingresar el Nombre del Servidor.")
        ElseIf String.IsNullOrEmpty(txtBaseDatos.Text.Trim) Then
            Throw New Exception("Debe Ingresar el Nombre de la Base de Datos.")
        ElseIf String.IsNullOrEmpty(txtUsuario.Text.Trim) Then
            Throw New Exception("Debe Ingresar el Usuario.")
        ElseIf String.IsNullOrEmpty(txtClave.Text.Trim) Then
            Throw New Exception("Debe Ingresar la Clave.")
        ElseIf String.IsNullOrEmpty(txtWCFURL.Text.Trim) Then
            Throw New Exception("Debe Ingrese la Dirección del WCF")
        End If

    End Sub


    Private Sub cmdGuardar_Click(sender As Object, e As EventArgs) Handles cmdGuardar.Click

        Try

            Call Me.Validar()

            'pObjC = New WCFConfiguracion.Configuracion() With {.Servidor = txtServer.Text.Trim, .BaseDatos = txtBaseDatos.Text.Trim, .Usuario = txtUsuario.Text.Trim, .Clave = txtClave.Text.Trim}

            'If Not gWCFConfiguracion.ModificarCST(pObjC) Then
            '    MsgBox("Cambios no aplicados.", MsgBoxStyle.Exclamation, "TOMIMS4")
            'Else

            '    If IO.File.Exists(BD.AppPath & "Conn.ini") Then
            '        IO.File.Delete(BD.AppPath & "Conn.ini")
            '    End If

            '    IO.File.AppendAllText(BD.AppPath & "Conn.ini", pObjC.Servidor & vbNewLine & pObjC.BaseDatos & vbNewLine & pObjC.Usuario & vbNewLine & pObjC.Clave & vbNewLine & txtWCFURL.Text.Trim)

            '    MsgBox("Los cambios fueron aplicados correctamente. La aplicación se reiniciará.", MsgBoxStyle.Information, "BIELLA")
            '    Application.Restart()

            'End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Sub frmBaseDatos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtServer.Text = String.Empty
        txtBaseDatos.Text = String.Empty
        txtUsuario.Text = String.Empty
        txtClave.Text = String.Empty
        txtWCFURL.Text = String.Empty

        Try

            'Dim lLista As List(Of String) = gWCFConfiguracion.GetCST().ToList

            'If lLista IsNot Nothing AndAlso lLista.Count > 0 Then

            '    txtServer.Text = lLista(0)
            '    txtBaseDatos.Text = lLista(1)
            '    txtUsuario.Text = lLista(2)
            '    txtClave.Text = lLista(3)

            '    If IO.File.Exists(BD.AppPath & "Conn.ini") Then

            '        Dim oRead As IO.StreamReader = IO.File.OpenText(BD.AppPath & "Conn.ini")

            '        Dim lArray() As String = Split(oRead.ReadToEnd, vbNewLine)

            '        If lArray.Count > 4 Then
            '            txtWCFURL.Text = lArray(4).ToString
            '        End If

            '        oRead.Close()
            '        oRead.Dispose()

            '    End If

            'End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

        txtServer.Focus()
        txtServer.SelectAll()

    End Sub

End Class