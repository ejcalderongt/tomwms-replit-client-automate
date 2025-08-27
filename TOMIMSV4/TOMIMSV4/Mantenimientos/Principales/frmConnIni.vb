Imports DevExpress.XtraEditors

Public Class frmConnIni

    'Private pObjC As WCFConfiguracion.Configuracion

    Private vServidor As String = ""
    Private vBaseDatos As String = ""
    Private vUsuario As String = ""
    Private vContraseña As String = ""
    Private vUrlWCF As String = ""
    Private _lblMod As String

    Public Property lblMod() As String
        Get
            Return _lblMod
        End Get
        Set(ByVal value As String)
            _lblMod = value
        End Set
    End Property

    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Function BuscarRegistro() As Boolean
        BuscarRegistro = True
        Try
            If Not LLenaLv() Then BuscarRegistro = False
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Function

    Private Sub RegistroABC(ByVal pMod$)

        Try

            'Dim Resultado$

            Select Case pMod$

                Case "A"

                    If InfOK("ENC") Then

                        Try

                            'pObjC = New WCFConfiguracion.Configuracion() With {.Servidor = clsPublic.Encriptar(vServidor$), .BaseDatos = clsPublic.Encriptar(vBaseDatos$), .Usuario = clsPublic.Encriptar(vUsuario$), .Clave = clsPublic.Encriptar(vContraseña$)}

                            'If Not gWCFConfiguracion.ModificarCST(pObjC) Then
                            '    MsgBox("Cambios no aplicados.", MsgBoxStyle.Exclamation, Me.Text)
                            'Else

                            '    Resultado$ = GrabaIni(BD.AppPath & "Conn.ini", vServidor$, vBaseDatos$, vUsuario$, vContraseña$, IIf(lblMod = "NUEVO", True, False), vUrlWCF)

                            '    If String.IsNullOrEmpty(Resultado$) Then
                            '        LLenaLv()
                            '        MsgBox("Se han guardado los Parámetros.", MsgBoxStyle.Information)
                            '        Close()
                            '    Else
                            '        MsgBox("Ocurrió el siguiente error " & Resultado, MsgBoxStyle.Critical)
                            '        LimpiarCampos()
                            '    End If

                            'End If

                        Catch ex As Exception
                            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Try

                    End If

                Case "C"

                    If InfOK("ENC") Then

                        If XtraMessageBox.Show("¿Desea modificar los Parámetros?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            Try

                                'pObjC = New WCFConfiguracion.Configuracion() With {.Servidor = clsPublic.Encriptar(vServidor$), .BaseDatos = clsPublic.Encriptar(vBaseDatos$), .Usuario = clsPublic.Encriptar(vUsuario$), .Clave = clsPublic.Encriptar(vContraseña$)}

                                'If Not gWCFConfiguracion.ModificarCST(pObjC) Then
                                '    MsgBox("Cambios no aplicados.", MsgBoxStyle.Exclamation, Me.Text)
                                'Else

                                '    Resultado$ = GrabaIni(BD.AppPath & "Conn.ini", vServidor$, vBaseDatos$, vUsuario$, vContraseña$, IIf(lblMod = "NUEVO", True, False), vUrlWCF)

                                '    If String.IsNullOrEmpty(Resultado$) Then
                                '        LLenaLv()
                                '        MsgBox("Se han modificado los parametros. La aplicación se reiniciará.", MsgBoxStyle.Information)
                                '        Application.Restart()
                                '    Else
                                '        MsgBox("Ocurrió el siguiente error " & Resultado, MsgBoxStyle.Critical)
                                '        LimpiarCampos()
                                '    End If

                                'End If

                            Catch ex As Exception
                                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End Try

                        End If

                    End If

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Err.Clear()
        End Try

    End Sub

    Private Function InfOK(ByVal pMod$) As Boolean
        ' Este procedimiento valida que la información obligatoria sea completada.

        InfOK = True

        If pMod$ = "ENC" Then

            If String.IsNullOrEmpty(txtServidor.Text.Trim) Then
                XtraMessageBox.Show("Ingrese IP o nombre del Servidor SQL para Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : txtServidor.Focus()
                Return False : Exit Function
            Else
                vServidor$ = Trim(txtServidor.Text.ToUpper)
            End If

            If String.IsNullOrEmpty(txtBD.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Base de Datos para Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtBD.Focus()
                Return False : Exit Function
            Else
                'If gWCFConfiguracion.ExisteBaseDatos(txtBD.Text.Trim) = False Then
                '    XtraMessageBox.Show(String.Format("La Base de Datos {0} no existe.", txtBD.Text.Trim), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    txtServidor.Focus()
                '    Return False : Exit Function
                'Else
                '    vBaseDatos$ = Trim(txtBD.Text.ToUpper)
                'End If
            End If

            If String.IsNullOrEmpty(txtUsuarioBD.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Usuario de la Base de Datos.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtUsuarioBD.Focus()
                Return False : Exit Function
            Else
                vUsuario$ = txtUsuarioBD.Text.Trim
            End If

            If String.IsNullOrEmpty(txtContraseñaBD.Text.Trim) Then

                If XtraMessageBox.Show("¿Guardar la contraseña de la base de datos vacia?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    txtContraseñaBD.Focus() : Return False : Exit Function
                Else
                    vContraseña$ = ""
                End If
            Else
                vContraseña$ = txtContraseñaBD.Text.Trim
            End If

            If String.IsNullOrEmpty(txtUrlWCF.Text.Trim) Then

                If XtraMessageBox.Show("¿Guardar la URL para WCF por defecto?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    txtContraseñaBD.Focus() : Return False : Exit Function
                Else
                    vUrlWCF = "http://localhost:4180/"
                End If
            Else
                vUrlWCF = txtUrlWCF.Text.Trim
            End If

        End If

    End Function

    Private Sub LimpiarCampos()
        txtServidor.Text = ""
        txtBD.Text = ""
        txtUsuarioBD.Text = ""
        txtContraseñaBD.Text = ""
        txtServidor.Focus()
    End Sub

    Private Function LLenaLv() As Boolean

        LLenaLv = True

        Dim Idx As Integer

        Try

            Dim AppPath As String = CurDir() & "\"
            Dim oRead As IO.StreamReader = IO.File.OpenText(AppPath & "Conn.ini")

            vServidor$ = oRead.ReadLine()
            vBaseDatos$ = oRead.ReadLine()
            vUsuario$ = oRead.ReadLine()
            vContraseña$ = oRead.ReadLine()
            'vContraseña$ = oRead.ReadLine()
            vUrlWCF = oRead.ReadLine()

            oRead.Close()

            txtServidor.Text = vServidor$
            txtBD.Text = vBaseDatos$
            txtUsuarioBD.Text = (vUsuario)
            txtContraseñaBD.Text = (vContraseña)
            txtUrlWCF.Text = vUrlWCF

            lvwDetalle.Items.Clear()
            lvwDetalle.Items.Add(New ListViewItem(vServidor$))
            Idx = lvwDetalle.Items.Count - 1
            lvwDetalle.Items(Idx).SubItems.Add(vBaseDatos$)
            lvwDetalle.Items(Idx).SubItems.Add((vUsuario$))
            lvwDetalle.Items(Idx).SubItems.Add((vContraseña$))
            lvwDetalle.Items(Idx).SubItems.Add((vUrlWCF))

            Application.DoEvents()

        Catch ex As Exception
            If Err.Number = 53 Then
                XtraMessageBox.Show("No existe el archivo de configuración", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Err.Clear()
            Else
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Err.Clear()
            End If
            LLenaLv = False
        End Try

    End Function

    Public Function GrabaIni(ByVal Ubicacion$, ByVal Servidor$, ByVal BaseDatos$, ByVal UsuarioBD$, ByVal ContraseñaBD$, ByVal EsNuevo As Boolean, ByVal UrlWCF As String) As String
        Try
            If EsNuevo Then
                If Not ContraseñaBD$ = "" Then
                    IO.File.AppendAllText(Ubicacion$, Servidor$ & vbNewLine & BaseDatos$ & vbNewLine & UsuarioBD$ & vbNewLine & ContraseñaBD$ & vbNewLine & UrlWCF)
                Else
                    IO.File.AppendAllText(Ubicacion$, Servidor$ & vbNewLine & BaseDatos$ & vbNewLine & UsuarioBD$ & vbNewLine & vbNewLine & UrlWCF)
                End If
            Else
                IO.File.Delete(Ubicacion$)
                If Not ContraseñaBD$ = "" Then
                    IO.File.AppendAllText(Ubicacion$, Servidor$ & vbNewLine & BaseDatos$ & vbNewLine & UsuarioBD$ & vbNewLine & ContraseñaBD$ & vbNewLine & UrlWCF)
                Else
                    IO.File.AppendAllText(Ubicacion$, Servidor$ & vbNewLine & BaseDatos$ & vbNewLine & UsuarioBD$ & vbNewLine & vbNewLine & UrlWCF)
                End If
            End If
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Sub frmParamGlobales_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If lblMod = "NUEVO" Then

            mnuGuardar.Enabled = True
            mnuActualizar.Enabled = False
            'LimpiarCampos()

        ElseIf lblMod = "EDITAR" Then

            mnuGuardar.Enabled = False
            mnuActualizar.Enabled = True
            LimpiarCampos()

            If Not BuscarRegistro() Then Exit Sub

        End If

        'lvwDetalle.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

    End Sub

    Private Sub Guardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuGuardar.ItemClick
        RegistroABC("A")
    End Sub

    Private Sub Actualizar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuActualizar.ItemClick
        RegistroABC("C")
    End Sub

    Private Sub Salir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuSalir.ItemClick
        IMS.Cerrar_Ventana(Me)
    End Sub

End Class