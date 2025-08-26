Imports System.Drawing.Printing
Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmImpresiones

    Public ListaInstancias As New List(Of clsCadenaConexion)
    Private BeListImpresion As List(Of clsBeImpresion_productos_barras)

    Public Sub New()

        InitializeComponent()

        Dim IndiceConexion As Integer
        Dim con As SqlClient.SqlConnection = Nothing

        If BD.Instancia.Indice <> 0 Then
            IndiceConexion = BD.Instancia.Indice
        Else
            IndiceConexion = 1
        End If

        ListaInstancias = clsPublic.Leer_Archivo_Configuracion_Ini(IndiceConexion)

        If ListaInstancias Is Nothing Then
            Me.Close()
            Exit Sub
        End If

        Try

            con = New SqlClient.SqlConnection(ListaInstancias(IndiceConexion).CadenaConexionSQLClient)
            con.Open()

        Catch ex As Exception
            XtraMessageBox.Show("No se pudo conectar con la instacia: " & ListaInstancias(IndiceConexion).CadenaConexionSQLClient, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End Try

        Try

            BD.Instancia = ListaInstancias(IndiceConexion)
            My.Settings.IMS4MB_ConnectionStringConfigurable = BD.Instancia.CadenaConexionSQLClient
            Configuration.ConfigurationManager.AppSettings("CST") = BD.Instancia.CadenaConexionSQLClient

        Catch ex As Exception
            Application.Exit()
        Finally
            If Not con Is Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
        End Try

    End Sub

    Private Sub frmImpresiones_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Set_Info_Conexion()

            'If (FormWindowState.Minimized = WindowState) Then

            '    NotifyIcon1.Visible = True
            '    NotifyIcon1.ShowBalloonTip(100, "Servicio de Impresión", "Presione doble click para maximizar", ToolTipIcon.Info)
            '    Hide()

            'End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Set_Info_Conexion()

        Try

            ' MsgBox(String.Format("Instancia: {0}, BD: {1}, Server,{2}", BD.Instancia.NombreInstancia, BD.Instancia.NombreBD, BD.Instancia.Server))

            lblServerAPP.Caption += " " & BD.Instancia.Server
            lblBDAPP.Caption += " " & BD.Instancia.NombreBD
            lblNombrePC.Caption = Net.Dns.GetHostName()
            lblEmpresa.Caption += " " & clsLnEmpresa.GetNombreEmpresa(m_Global.IdEmpresa)
            lblBodega.Caption += " " & clsLnBodega.Get_Nombre_Bodega_By_IdBodega(m_Global.IdBodega)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Imprime_productos_barras()

        Try

            BeListImpresion = clsLnImpresion_productos_barras.GetAll()

            If BeListImpresion.Count > 0 AndAlso Not BeListImpresion Is Nothing Then

                Timer1.Stop()

                For Each Obj In BeListImpresion

                    For i = 0 To Obj.Cantidad_impresiones - 1
                        If Imprimir_Etiqueta(Obj.Codigo, Obj.Nombre, Obj.Codigo_barra, Obj.IdImpresora) Then
                            Obj.Activo = 0
                            Obj.Impreso = 1
                            clsLnImpresion_productos_barras.Actualizar(Obj)
                        End If
                    Next

                Next

                Timer1.Start()

            Else
                txtLog.AppendText("Nothing on Queue... at: " & Now)
                txtLog.AppendText(vbNewLine)
                txtLog.Refresh()
                txtLog.SelectionStart = txtLog.TextLength
                txtLog.ScrollToCaret()
            End If

        Catch ex As Exception
            'XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtLog.AppendText("Error: " & ex.Message)
            txtLog.AppendText(vbNewLine)
            txtLog.Refresh()
            txtLog.SelectionStart = txtLog.TextLength
            txtLog.ScrollToCaret()
        End Try

    End Sub

    Private Function Imprimir_Etiqueta(ByVal pCodigo As String, ByVal pProducto As String, ByVal pBarra As String, ByVal IdImpresora As Integer) As Boolean

        Imprimir_Etiqueta = False

        Try

            Dim pd As New PrintDocument
            Dim vProducto As String = ""
            Dim cProducto As String = ""
            Dim pMax As Integer = 0
            Dim pLength As Integer = 0
            Dim instance As New Printing.PrinterSettings
            Dim impresosaPredt As String = ""
            ' Default printer      
            Dim s_Default_Printer As String = clsLnImpresora.Get_Nombre_By_IdImpresora(IdImpresora) 'pd.PrinterSettings.PrinterName
            instance.PrinterName = s_Default_Printer

            If instance.IsValid Then
                impresosaPredt = instance.PrinterName
            Else
                Return False
            End If


            txtLog.AppendText("Print request to: " & s_Default_Printer & " Código: " & pCodigo)
            txtLog.AppendText(vbNewLine)
            txtLog.Refresh()
            txtLog.SelectionStart = txtLog.TextLength
            txtLog.ScrollToCaret()

            If s_Default_Printer = "" Then
                Throw New Exception("No se ha definido el nombre de la impresora para el identificador: " & IdImpresora)
            Else

                pLength = pProducto.Trim.Length

                If pLength > 29 Then
                    cProducto = pProducto.Trim.Substring(0, 27)
                    vProducto = pProducto.Trim.Substring(28)
                Else
                    cProducto = pProducto
                End If

                Dim wt As Integer = 2
                If pBarra.Length > 11 Then
                    wt = 1
                End If

                Dim ZPLString As String = String.Format("
                                                        ^XA
                                                        ^MMT
                                                        ^PW406
                                                        ^LL0203
                                                        ^LS0
                                                        ^BY{3},3,78^FT365,73^BCI,,Y,N
                                                        ^FD>:{2}^FS
                                                        ^FT370,188^A0I,10,16^FH\^FD{0}^FS
                                                        ^FT370,172^A0I,10,16^FH\^FD{4}^FS
                                                        ^FT370,31^A0I,17,16^FH\^FD{1}^FS
                                                        ^FT370,54^A0R,14,14^FH\^FDTOM, WMS.^FS
                                                        ^PQ1,0,1,Y
                                                        ^XZ", cProducto,
                                                                  pCodigo,
                                                                  pBarra,
                                                                  wt,
                                                                  vProducto)

                RawPrinterHelper.SendStringToPrinter(impresosaPredt, ZPLString)

                Imprimir_Etiqueta = True

            End If

        Catch ex As Exception
            txtLog.AppendText("Error: " & ex.Message)
            txtLog.AppendText(vbNewLine)
            txtLog.Refresh()
            txtLog.SelectionStart = txtLog.TextLength
            txtLog.ScrollToCaret()
        End Try

    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Try

            Timer1.Enabled = False
            Imprime_productos_barras()
            Timer1.Enabled = True

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon1.DoubleClick

        Try

            Show()
            WindowState = FormWindowState.Maximized
            NotifyIcon1.Visible = False

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Close()
    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Impresion()
    End Sub

    Private Sub Impresion()

        Try

            BeListImpresion = clsLnImpresion_productos_barras.GetAll()

            If BeListImpresion.Count > 0 AndAlso Not BeListImpresion Is Nothing Then

                For Each Obj In BeListImpresion

                    If Imprimir_Etiqueta(Obj.Codigo, Obj.Nombre, Obj.Codigo_barra, Obj.IdImpresora) Then
                        Obj.Activo = 0
                        Obj.Impreso = 1
                        clsLnImpresion_productos_barras.Actualizar(Obj)
                    End If

                Next

            End If

        Catch ex As Exception
            txtLog.AppendText("Error: " & ex.Message)
            txtLog.AppendText(vbNewLine)
            txtLog.Refresh()
            txtLog.SelectionStart = txtLog.TextLength
            txtLog.ScrollToCaret()
        End Try

    End Sub

    Private Sub frmImpresiones_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        If (FormWindowState.Maximized = WindowState) Then

            If MsgBox("¿Cerrar el monitor de impresión?", MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then

                Me.WindowState = FormWindowState.Minimized
                NotifyIcon1.Visible = True
                NotifyIcon1.ShowBalloonTip(100, "Servicio de Impresión", "Presione doble click para maximizar", ToolTipIcon.Info)
                Hide()

            End If

        Else
            e.Cancel = True
        End If

    End Sub

    Private Sub rbMain_Click(sender As Object, e As EventArgs) Handles rbMain.Click

    End Sub
End Class