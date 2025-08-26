Imports System.Net.Sockets
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports TOMWMS

Public Class frmPrinterConfig

    Public pImpresora As New clsBeQT_Impresora()
    Public Sub New()
        InitializeComponent()
    End Sub

    Private printerName As String
    Private puertoIP As String

    Public Sub New(selectedPrinter As String, selectedPort As String)
        InitializeComponent()
        Me.printerName = selectedPrinter
        Me.puertoIP = selectedPort
    End Sub

    Private Class TipoImpresora
        Public Property ID As Integer = 0
        Public Property Nombre As String = ""

    End Class

    Private Class FormatoImpresion
        Public Property ID As Integer = 0
        Public Property Nombre As String = ""
    End Class

    Dim listTipoImpresora As New List(Of TipoImpresora) From {
                New TipoImpresora With {.ID = 1, .Nombre = "IP"},
                New TipoImpresora With {.ID = 2, .Nombre = "USB"}
            }

    Dim listFormatoImpresion As New List(Of TipoImpresora) From {
                New TipoImpresora With {.ID = 1, .Nombre = "ZPL"},
                New TipoImpresora With {.ID = 2, .Nombre = "Texto"},
                New TipoImpresora With {.ID = 2, .Nombre = "PDF"},
                New TipoImpresora With {.ID = 2, .Nombre = "ESC/POS"},
                New TipoImpresora With {.ID = 2, .Nombre = "Imagen"},
                New TipoImpresora With {.ID = 2, .Nombre = "PCL"},
                New TipoImpresora With {.ID = 2, .Nombre = "PostScript"}
            }

    Private Sub FrmPrinterConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '#GT04032025: se invoca el form desde otro lugar que no es la lista
        If printerName IsNot Nothing Then
            txtNombreImpresora.Text = printerName
            If puertoIP.StartsWith("USB") Then
                txtIp.Enabled = False
                txtIp.Text = ""
            ElseIf puertoIP.StartsWith("IP") Then
                txtIp.Enabled = True
                txtIp.Text = puertoIP
            Else
                Cargar_TipoImpresora()
                cmbConexionImpresora.Focus()
            End If
        Else
            Cargar_TipoImpresora()
        End If

        Cargar_FormatoImpresion()

        cmbConexionImpresora.Focus()

    End Sub

    Private Sub Cargar_FormatoImpresion()
        Try

            cmbFormatoImpresion.Properties.DataSource = listFormatoImpresion
            cmbFormatoImpresion.Properties.DisplayMember = "ID"  ' Campo visible en la lista
            cmbFormatoImpresion.Properties.ValueMember = "Nombre"        ' Valor interno seleccionado
            cmbFormatoImpresion.Properties.NullText = "Seleccione un tipo..."

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Cargar_TipoImpresora()
        Try
            cmbConexionImpresora.Properties.DataSource = listTipoImpresora
            cmbConexionImpresora.Properties.DisplayMember = "ID"  ' Campo visible en la lista
            cmbConexionImpresora.Properties.ValueMember = "Nombre"        ' Valor interno seleccionado
            cmbConexionImpresora.Properties.NullText = "Seleccione un tipo..."

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles cmdTest.ItemClick
        If cmbConexionImpresora.EditValue = "IP" AndAlso Not String.IsNullOrWhiteSpace(txtIp.Text) Then
            If TestConnection(txtIp.Text, 9100) Then
                XtraMessageBox.Show("Conexión exitosa.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                XtraMessageBox.Show("No se pudo conectar a la impresora.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            XtraMessageBox.Show("Seleccione una configuración válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Function TestConnection(ip As String, port As Integer) As Boolean
        Try
            Using client As New TcpClient()
                Dim result As IAsyncResult = client.BeginConnect(ip, port, Nothing, Nothing)
                Dim success As Boolean = result.AsyncWaitHandle.WaitOne(2000, True)
                If Not success Then Return False
                client.EndConnect(result)
                Return True
            End Using
        Catch
            Return False
        End Try
    End Function

    'Dim newConfig As String = ""
    Private Sub cmdGuardar_Click(sender As Object, e As EventArgs) Handles cmdGuardar.ItemClick

        cmdGuardar.Enabled = False
        Guardar()
        cmdGuardar.Enabled = True
        XtraMessageBox.Show("Configuración guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
    End Sub

    Private Sub Guardar()
        Try
            pImpresora = New clsBeQT_Impresora
            pImpresora.IsNew = True
            pImpresora.Descripcion = txtNombreImpresora.Text
            pImpresora.Predeterminada = chkPredeterminada.Checked
            pImpresora.Activo = chkActivo.Checked
            pImpresora.IP = txtIp.Text
            pImpresora.Conexion = cmbConexionImpresora.EditValue
            pImpresora.user_agr = 1
            pImpresora.fec_agr = Now
            pImpresora.user_mod = 1
            pImpresora.fec_mod = Now

            '#GT10032025: parametrización de la printer
            pImpresora.Formato_Impresion = cmbFormatoImpresion.Text
            pImpresora.Velocidad_Impresion = cmbVelocidad.EditValue
            pImpresora.Reintentos_Impresion = cmbReintentos.EditValue
            pImpresora.Delay_Impresion = cmbDelay.EditValue

            clsLnQT_Impresora.Insertar(pImpresora)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmbTipoImpresora_EditValueChanged(sender As Object, e As EventArgs) Handles cmbConexionImpresora.EditValueChanged
        Try
            If cmbConexionImpresora.EditValue = "USB" Then
                txtIp.Enabled = False
            Else
                txtIp.Enabled = True
            End If
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
End Class