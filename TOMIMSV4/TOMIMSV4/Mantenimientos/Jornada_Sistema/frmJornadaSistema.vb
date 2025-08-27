Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmJornadaSistema

    Private ListaInstancias As New List(Of clsCadenaConexion)
    Public Property Empresa As New clsBeEmpresa()

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        CheckForIllegalCrossThreadCalls = False

    End Sub

    Private Sub frmJornadaSistema_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

            Get_IP_Address()

            lblUsuario.Caption = AP.HostName

        Catch ex1 As SqlClient.SqlException

            If ex1.HResult = -2146233088 OrElse ex1.HResult = -2146232060 Then

                If Not ListaInstancias Is Nothing Then
                    XtraMessageBox.Show("No se pudo abrir la conexión a la base de datos con los parámetros de conexión para la instancia: " & ListaInstancias(0).NombreInstancia,
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
                Else
                    XtraMessageBox.Show("No se pudo abrir la conexión a la base de datos los parámetros de conexión están vacíos",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
                End If

            Else
                XtraMessageBox.Show("Error: " & ex1.Message,
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If

        Catch ex As Exception

            XtraMessageBox.Show("Error: " & ex.Message,
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

            Close()

        Finally
            'IsLoading = False
        End Try

    End Sub

    Private Sub Set_Parametros_Servidor()

        Try

            lblBD.Caption = clsBD.Instancia.NombreBD
            lblServer.Caption = clsBD.Instancia.Server
            lblEmpresa.Caption = "All"

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
             Text,
             MessageBoxButtons.OK,
             MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Get_IP_Address()

        Try

            AP.HostName = Net.Dns.GetHostName()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Function Ejecutar_Realizacion_De_Jornada() As Boolean

        Ejecutar_Realizacion_De_Jornada = False

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

            Dim fecha_server As Date = clsServidor.Get_Fecha_Servidor()

            'lblprg.AppendText("Inicio de verificación - " & Now)
            lblprg.AppendText("Inicio de verificación - " & fecha_server)

            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lblprg.AppendText("Procesando jornada para empresa: " & AP.Empresa.Nombre)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim vStopwatch As Stopwatch = Stopwatch.StartNew() '//creates And start the instance Of Stopwatch

            Dim vResultado As Boolean = clsLnJornada_sistema.Inicio_De_Jornada_Correcto(AP.IdEmpresa,
                                                                                        AP.IdBodega,
                                                                                        1965,
                                                                                        lblprg,
                                                                                        prg,
                                                                                        Me)
            Ejecutar_Realizacion_De_Jornada = vResultado

            vStopwatch.Stop()

            Dim vTiempoSegundos As Double = vStopwatch.Elapsed.TotalSeconds

            lblprg.AppendText("Fin de proceso - " & Now & " Tiempo transcurrido en segundos: " & vTiempoSegundos)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            SplashScreenManager.CloseForm(False)

            If vResultado Then Close()

        Catch ex As Exception

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            lblprg.AppendText("ERROR: " & ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        End Try

    End Function

    Private Sub frmJornadaSistema_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            If Not BW.IsBusy Then
                BW.RunWorkerAsync()
            End If

        Catch ex As Exception

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            lblprg.AppendText("ERROR: " & ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        End Try


    End Sub

    Private Sub BW_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork

        Try

            Set_Parametros_Servidor()

            If Ejecutar_Realizacion_De_Jornada() Then
                DialogResult = DialogResult.OK
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            If Not BW.IsBusy Then
                BW.RunWorkerAsync()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmJornadaSistema_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Try

            If BW.IsBusy Then
                BW.CancelAsync()
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            lblprg.AppendText("ERROR: " & ex.Message)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
        End Try

    End Sub

End Class