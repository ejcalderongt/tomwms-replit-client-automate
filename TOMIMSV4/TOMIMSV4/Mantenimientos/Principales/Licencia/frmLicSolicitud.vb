Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmLicSolicitud

    Public sitem As clsBeLicencia_solic
    Public cLic As New clsBeLicencia_item
    Public Bandera As Integer
    Public Modo As pModo
    Public mac, sllave, lllave As String

    Public ForceLicenciaSinLicencia As Boolean = False

    Enum pModo
        LIC = 1
        SRV = 2
        CON = 3
    End Enum

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmLicSolicitud_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Genera_Llave()

        setAPP()

    End Sub

    Private Sub Genera_Llave()

        Try

            Dim solstr As String = "   "

            Select Case Modo
                Case 1 : solstr = "LIC"
                Case 2 : solstr = "SRV"
                Case 3 : solstr = "CON"
                Case Else
                    Exit Select
            End Select

            sllave = solstr & mac

            sllave = clsPublic.EncodeString(sllave)

            txtSol.Text = sllave

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Close()
    End Sub

    Private Sub btnAplicar_Click_1(sender As Object, e As EventArgs) Handles btnAplicar.Click

        Try

            Select Case Modo
                Case pModo.LIC
                    Text = "Activar Licencia "
                    Activa_Licencia()
                Case pModo.SRV
                    Text = "Activar bandera Servidor"
                    Activa_Servidor()
                Case pModo.CON
                    Text = "Limpiar bandera Conectado  "
                    Desconectar()
                Case Else
                    Exit Select
            End Select

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Try

            Genera_Llave()

            XtraMessageBox.Show("Solicitud generada",
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Activa_Licencia()

        Dim si, lmac As String
        Dim rslt As Integer

        Dim vIndicadorTipoLic As String = txtLic.Text
        If vIndicadorTipoLic = "" Then Return

        Try
            si = clsPublic.DecodeString(vIndicadorTipoLic)
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("Error al desencriptar la clave: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
            Return
        End Try

        Try

            vIndicadorTipoLic = Mid(si, 1, 5)
            lmac = Mid(si, 6)

            If vIndicadorTipoLic <> "LCENC" Then
                XtraMessageBox.Show("Licencia no válida, identificador de error: LCENC ",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
                Return
            End If

            If mac <> lmac Then
                XtraMessageBox.Show("Licencia no válida, error: HOSTMISTMATCH",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
                Return
            End If

            'MsgBox("Force_Licencia: " & ForceLicenciaSinLicencia & " Tipo: " & sitem.Tipo)

            If sitem.Tipo = clsBeLicencia_item.eTipoHost.BOF Then
                cLic.IdDisp = sitem.Identificacion
                cLic.Tipo = sitem.Tipo '#CKFK 20180526 No se actualizaba el Tipo y por eso se guardaba siempre 0
                rslt = clsLnLicencia_item.Aplica_Solicitud(lmac, cLic, Bandera, False, ForceLicenciaSinLicencia)
            Else
                cLic.IdDisp = sitem.IdDisp
                rslt = clsLnLicencia_item.Aplica_SolicitudHH(lmac, cLic, Bandera, False, ForceLicenciaSinLicencia)
            End If

            If rslt = 1 Then
                XtraMessageBox.Show("Solicitud procesada correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                DialogResult = DialogResult.Yes
            ElseIf rslt = 0 Then
                XtraMessageBox.Show("No hay licencia disponible. ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                XtraMessageBox.Show("No se pudo procesar la licencia. ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("Error al activar la licencia: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmLicSolicitud_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

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
                Dim ColView As New frmColumViewervb
                ColView.ShowDialog()
            ElseIf e.Control AndAlso e.KeyCode = Keys.G Then

                ForceLicenciaSinLicencia = True

                Dim si As String = ""
                Dim s As String = DecodeString(txtSol.Text.Trim())
                si = Mid(s, 1, 3) : mac = Mid(s, 4)
                If si <> "LIC" AndAlso si <> "SRV" Then
                    XtraMessageBox.Show("LLave de solicitud incorrecta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If
                si = "LCENC" & mac
                txtLic.Text = EncodeString(si)

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

    Private Shared Function DecodeString(ByVal encodedText As String) As String
        Dim stringBytes As Byte() = Convert.FromBase64String(encodedText)
        Return System.Text.Encoding.Unicode.GetString(stringBytes)
    End Function

    Private Shared Function EncodeString(ByVal origText As String) As String
        Dim stringBytes As Byte() = System.Text.Encoding.Unicode.GetBytes(origText)
        Return Convert.ToBase64String(stringBytes, 0, stringBytes.Length)
    End Function

    Private Sub Activa_Servidor()

        Dim vLicenciaDecodificada, lmac As String

        If txtLic.Text = "" Then
            Throw New Exception("Ingrese una clave válida")
        Else
            Try
                vLicenciaDecodificada = clsPublic.DecodeString(txtLic.Text)
            Catch ex As Exception
                Throw New Exception("Ingrese una clave válida")
            End Try
        End If

        Try

            Dim vIdentificadorLicencia As String = Mid(vLicenciaDecodificada, 1, 7)
            Dim vLlaveDecodificada As String = vLicenciaDecodificada.Remove(0, 7)
            Dim ValoresLLaveSeparados As String() = vLlaveDecodificada.Split(",")
            cLic.CantBackOffice = ValoresLLaveSeparados(1)
            cLic.CantHandHeld = ValoresLLaveSeparados(2)
            Dim vAño As Integer = ValoresLLaveSeparados(3) + 2000
            Dim vMes As Integer = ValoresLLaveSeparados(4)
            Dim vDia As Integer = ValoresLLaveSeparados(5)
            Dim vFechaVence As Date = New Date(vAño, vMes, vDia)
            cLic.Vence = vFechaVence

            If vIdentificadorLicencia.StartsWith("SERVLIC") Then
                lmac = Mid(vLlaveDecodificada, 1, vLlaveDecodificada.IndexOf("#"))
            Else
                lmac = Mid(vLicenciaDecodificada, 8)
            End If

            If vIdentificadorLicencia <> "SERVLIC" Then
                XtraMessageBox.Show("Licencia no válida, error: NOTFORSRV",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
                Exit Sub
            End If

            If mac <> lmac Then
                XtraMessageBox.Show("Licencia no válida, error: HOSTMISTMATCH",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
                Exit Sub
            End If

            If vFechaVence <= clsServidor.Get_Fecha_Servidor()
                XtraMessageBox.Show("La licencia vence hoy, no se puede aplicar",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
                Exit Sub
            End If

            '#EJC2018106: Agregado para actualizar servidor de licencia,
            'Creo que me falta una bandera de validación que agregue si HosEsNuevoServidor
            If XtraMessageBox.Show(String.Format("¿Aplicar licencia: {0}Backoffice: {1}  {0}HandHeld:{2} {0}Vence:{3}?", vbCrLf, cLic.CantBackOffice, cLic.CantHandHeld, cLic.Vence.ToShortDateString), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                clsLnLicencia_llave.EliminarTodos()

                Dim BeLicLlaveSol As New clsBeLicencia_llave() With {.IdLlave = clsLnLicencia_llave.MaxID(),
                    .Llave = txtLic.Text}

                BeLicLlaveSol.CantBackOffice = cLic.CantBackOffice
                BeLicLlaveSol.CantHandHeld = cLic.CantHandHeld

                '#EJC20171108_REF02_0605PM: Refactoring clsBeLicencia_llave                
                If Not clsLnLicencia_llave.Exist(txtLic.Text) Then
                    clsLnLicencia_llave.Insertar(BeLicLlaveSol)
                Else
                    clsLnLicencia_llave.Actualizar(BeLicLlaveSol)
                End If

                XtraMessageBox.Show("Licencia actualizada correctamente",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

                clsLnLicencia_item.Asigna_Servidor(AP.HostName, txtLic.Text)

                DialogResult = DialogResult.OK

            End If

        Catch ex As Exception
            XtraMessageBox.Show("LLave de licencia incompleta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try

    End Sub

    Private Sub Desconectar()

        Dim si, lmac As String

        Dim ss As String = txtLic.Text
        If ss = "" Then Return

        Try
            si = clsPublic.DecodeString(ss)
        Catch ex As Exception
            XtraMessageBox.Show("LLave de licencia incompleta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try

        Try

            ss = Mid(si, 1, 5) : lmac = Mid(si, 6)

            If ss <> "CONEX" Then
                XtraMessageBox.Show("LLave de licencia incorrecta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            If mac <> lmac Then
                XtraMessageBox.Show("LLave de licencia incorrecta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            '#EJC20171108_REF08_1210AM: Refactoring Cancela conexión
            'Cancela_Conexion(mac)
            clsLnLicencia_item.Registra_Conexion(mac)

            XtraMessageBox.Show("Solicitud procesada correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            DialogResult = DialogResult.Yes

        Catch ex As Exception
            XtraMessageBox.Show("LLave de licencia incompleta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End Try

    End Sub

    Private Sub setAPP()

        Try

            lblServerAPP.Text += " " & clsBD.Instancia.Server
            lblBDAPP.Text += " " & clsBD.Instancia.NombreBD

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

End Class