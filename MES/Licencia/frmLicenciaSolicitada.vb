Imports System.Reflection
Imports DevExpress.XtraEditors
Imports TOMWMS

Public Class frmLicenciaSolicitada

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnAplicar_Click(sender As Object, e As EventArgs) Handles btnAplicar.Click

        If Licenciamiento_Activo() Then DialogResult = DialogResult.OK

    End Sub

    Private Function Licenciamiento_Activo() As Boolean

        Licenciamiento_Activo = False

        Try

            Dim BeLicItem As New clsBeLicencia_item

            If clsLnLicencia_item.Existe_Servidor_De_Licencias(BeLicItem) Then

                If clsLnLicencia_item.Licencia_Server_Activa(BeLicItem) Then

                    Dim StatusLicHost As clsBeLicencia_item.eEstatusLicencia = clsLnLicencia_item.Get_Estatus_Licencia_Host(AP.HostName, False)

                    Select Case StatusLicHost

                        Case clsBeLicencia_item.eEstatusLicencia.Activa
                            Licenciamiento_Activo = True

                        Case clsBeLicencia_item.eEstatusLicencia.Pendiente_Solicitud

                            XtraMessageBox.Show("El servidor de licencias, aún no ha aceptado la solicitud del host.",
                             Text,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error)

                        Case clsBeLicencia_item.eEstatusLicencia.No_Valida
                            Exit Select
                    End Select

                Else 'de Licencia_Activa(

                    XtraMessageBox.Show(String.Format("La licencia del server expiró el: {0}, se cerrará la aplicación", BeLicItem.Vence),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
                    Close()

                End If 'de Licencia_Activa

            Else 'de Existe_Servidor_De_Licencias(

                'No debería entrar aquí desde ésta pantalla
                If XtraMessageBox.Show("No existe un servidor de licencias activo, ¿Registrar éste ordenador como servidor de licencias?",
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim frmSol As New frmLicSolicitud
                    frmSol.Modo = frmLicSolicitud.pModo.SRV
                    frmSol.mac = clsLnLicencia_item.Get_Mac_Host(AP.HostName)

                    If frmSol.ShowDialog() = DialogResult.OK

                    Else
                        Application.Exit()
                    End If

                Else
                    Application.Exit()
                End If

            End If 'de Existe_Servidor_De_Licencias(

            clsLnLicencia_item.Registra_Ingreso(AP.HostName)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
             Text,
             MessageBoxButtons.OK,
             MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cmdLiberacionLicencia_Click(sender As Object, e As EventArgs) Handles cmdLiberacionLicencia.Click

    End Sub

    Private Sub frmLicenciaSolicitada_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        setAPP()

    End Sub

    Private Sub setAPP()

        Try

            lblServerAPP.Text += " " & clsBD.Instancia.Server
            lblBDAPP.Text += " " & clsBD.Instancia.NombreBD

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmLicenciaSolicitada_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Try

            If e.Control = True AndAlso e.KeyCode = Keys.I Then

                If XtraMessageBox.Show("¿Abrir archivo de configuración?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Process.Start(CurDir() & "\conn.ini", IO.FileMode.Open)
                End If

            ElseIf e.Control = True AndAlso e.KeyCode = Keys.U Then

                If XtraMessageBox.Show(String.Format("{0}{1}¿Abrir ruta de origen?", CurDir(), vbNewLine), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Process.Start(CurDir(), IO.FileMode.Open)
                End If

            ElseIf e.Control = True AndAlso e.KeyCode = Keys.T Then
                'Dim ColView As New frmColumViewervb
                'ColView.ShowDialog()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub GroupControl1_Paint(sender As Object, e As PaintEventArgs) Handles GroupControl1.Paint

    End Sub
End Class