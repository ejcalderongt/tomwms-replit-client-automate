Imports DevExpress.XtraEditors

Public Class frmIniWCF

    Private pUsuario As String
    Private pClave As String
    Private pURLWCF As String

    Public Property InstanciaActual As New clsCadenaConexion

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        txtServidorAPP.Text = String.Empty
        txtBDAPP.Text = String.Empty
        txtServerWCF.Text = String.Empty
        txtBDWCF.Text = String.Empty

    End Sub

    Private Sub Set_Info_Ini()

        Try

            txtServidorAPP.Text = InstanciaActual.Server
            txtBDAPP.Text = InstanciaActual.NombreBD
            pUsuario = InstanciaActual.Usuario
            pClave = InstanciaActual.Clave
            pURLWCF = InstanciaActual.IpWCF

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub frmIniWCF_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Set_Info_Ini()

            txtServidorAPP.Focus()
            txtServidorAPP.SelectAll()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Function ValidaAPP() As Boolean

        Try

            If String.IsNullOrEmpty(txtServidorAPP.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Servidor APP.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtServidorAPP.Focus()
                Return False
            ElseIf String.IsNullOrEmpty(txtBDAPP.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Base Datos APP.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtBDAPP.Focus()
                Return False
            Else
                Return True
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

    Private Function ValidaWCF() As Boolean

        Try

            If String.IsNullOrEmpty(txtServerWCF.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Servidor WCF.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtServerWCF.Focus()
                Return False
            ElseIf String.IsNullOrEmpty(txtBDWCF.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Base Datos WCF.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtBDWCF.Focus()
                Return False
            Else
                Return True
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

    Private Sub cmdAplicarAPP_Click(sender As Object, e As EventArgs) Handles cmdAplicarAPP.Click

        Try

            If ValidaAPP() Then

                If XtraMessageBox.Show("¿Guardar la configuración APP?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    'pObjC = New WCFConfiguracion.Configuracion() With {.Servidor = clsPublic.Encriptar(txtServidorAPP.Text.Trim), .BaseDatos = clsPublic.Encriptar(txtBDAPP.Text.Trim)}

                    'If gWCFConfiguracion.ModificarCSTParcial(pObjC) Then

                    '    XtraMessageBox.Show("Configuración actualizada", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    '    Me.DialogResult = DialogResult.OK

                    'Else
                    '    XtraMessageBox.Show("Configuración actualizada", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'End If

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

    Private Sub cmdAplicarWCF_Click(sender As Object, e As EventArgs) Handles cmdAplicarWCF.Click

        Try

            If ValidaWCF() Then

                If XtraMessageBox.Show("¿Guardar la configuración WCF?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    'pObjC = New WCFConfiguracion.Configuracion() With {.Servidor = clsPublic.Encriptar(txtServerWCF.Text.Trim), .BaseDatos = clsPublic.Encriptar(txtBDWCF.Text.Trim)}

                    If InfoConnIni(True, InstanciaActual.NombreInstancia, txtServerWCF.Text.Trim, txtBDWCF.Text,
                                    InstanciaActual.Usuario, InstanciaActual.Clave, "No", InstanciaActual.IpWCF) Then

                        XtraMessageBox.Show("Configuración actualizada", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        DialogResult = DialogResult.OK

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

    Public Function InfoConnIni(ByVal borrar As Boolean,
                           ByVal instancia As String,
                           ByVal serv As String,
                           ByVal bd As String,
                           ByVal usr As String,
                           ByVal clave As String,
                           ByVal vsegura As String,
                           ByVal IpWCF As String) As Boolean

        InfoConnIni = False

        Dim oWriteD As IO.StreamWriter = Nothing

        Try

            If borrar Then
                If IO.File.Exists(CurDir() & "Conn.ini") Then
                    IO.File.Delete(CurDir() & "Conn.ini")
                End If
                oWriteD = IO.File.CreateText(CurDir() & "Conn.ini")
            Else
                If Not IO.File.Exists(CurDir() & "Conn.ini") Then
                    oWriteD = IO.File.CreateText(CurDir() & "Conn.ini")
                    oWriteD.Close()
                End If
                oWriteD = IO.File.AppendText(CurDir() & "Conn.ini")
            End If

            oWriteD.WriteLine("#" & instancia)
            oWriteD.WriteLine("SERVIDOR = " & serv)
            oWriteD.WriteLine("BD = " & bd)
            oWriteD.WriteLine("USUARIO = " & usr)
            oWriteD.WriteLine("CLAVE = " & clave)
            oWriteD.WriteLine("SEGURIDAD_INTEGRADA = " & vsegura)
            oWriteD.WriteLine("IPWCF = " & IpWCF)
            oWriteD.WriteLine("#FIN")

            InfoConnIni = True

        Catch ex As Exception
            Throw ex
        Finally
            If Not oWriteD Is Nothing Then oWriteD.Close()
        End Try

    End Function

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick

    End Sub
End Class