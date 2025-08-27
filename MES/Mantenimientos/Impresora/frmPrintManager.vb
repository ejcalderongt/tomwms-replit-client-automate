Imports System.ComponentModel
Imports System.Drawing.Printing
Imports System.IO
Imports System.Management
Imports System.Net.Sockets
Imports System.Reflection
Imports System.Text
Imports DevExpress.XtraEditors
Imports Microsoft.Win32
Imports TOMWMS

Public Class frmPrintManager

    Public Sub New()
        InitializeComponent()
    End Sub

    Public printerName
    Public puertoIP

    Private WithEvents bgWorker As New BackgroundWorker()

    Dim objColaImpresion As New clsBeQT_Impresion_cola()
    Dim pImpresora As New clsBeQT_Impresora()


    Private Sub FrmPrintManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            bgWorker.WorkerReportsProgress = False
            bgWorker.WorkerSupportsCancellation = False
            bgWorker.RunWorkerAsync()

            CargarColaImpresion()
            CargarColaCerradaImpresion()
            CargarLog(False)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub bgWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgWorker.DoWork
        CargarImpresoras()
    End Sub

    Private Sub CargarImpresoras()
        If InvokeRequired Then
            Invoke(New MethodInvoker(AddressOf CargarImpresoras))
        Else
            Dim printerList As New DataTable()
            printerList.Columns.Add("Nombre")
            printerList.Columns.Add("Estado")
            printerList.Columns.Add("Tipo")
            printerList.Columns.Add("Puerto/IP")

            For Each printer As String In PrinterSettings.InstalledPrinters
                Dim estado As String = GetPrinterStatus(printer)
                Dim puerto As String = GetPrinterPort(printer)
                Dim tipo As String = If(puerto.StartsWith("USB"), "USB", "IP")
                printerList.Rows.Add(printer, estado, tipo, puerto)
            Next

            grdPrinters.DataSource = printerList
            gridViewPrinters.BestFitColumns()
            gridViewPrinters.OptionsBehavior.Editable = False

        End If
    End Sub

    Private Function GetPrinterStatus(printerName As String) As String
        Try
            Dim query As String = "SELECT * FROM Win32_Printer WHERE Name = '" & printerName.Replace("'", "''") & "'"
            Dim searcher As New ManagementObjectSearcher(query)
            Dim printers As ManagementObjectCollection = searcher.Get()

            For Each printer As ManagementObject In printers
                Dim status As Integer = Convert.ToInt32(printer("PrinterStatus"))
                Select Case status
                    Case 3
                        Return "🟢 Activa"
                    Case 4
                        Return "🟡 En cola"
                    Case 5
                        Return "🔴 Error"
                    Case 7
                        Return "⚠️ Sin Papel"
                    Case Else
                        Return "❓ Desconocido"
                End Select
            Next
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
        Return "❓ Desconocido"
    End Function

    Private Function GetPrinterPort(printerName As String) As String
        Try
            Using key As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Print\Printers\" & printerName & "\Port")
                If key IsNot Nothing Then
                    Return key.GetValue("").ToString()
                End If
            End Using
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
        Return ""
    End Function

    Private Sub gridPrinters_DoubleClick(sender As Object, e As EventArgs) Handles grdPrinters.DoubleClick
        Dim selectedRow As DataRowView = TryCast(gridViewPrinters.GetFocusedRow(), DataRowView)
        If selectedRow IsNot Nothing Then
            Dim printerName As String = selectedRow("Nombre").ToString()
            Dim puertoIP As String = selectedRow("Puerto/IP").ToString()

            Dim frmPrinter As New frmPrinterConfig(printerName, puertoIP)

            With frmPrinter
                .MdiParent = MdiParent
                .WindowState = FormWindowState.Normal
                .Show()
                .Focus()
            End With

            bgWorker.RunWorkerAsync()

            'Dim configForm As New frmPrinterConfig(printerName, puertoIP)
            'If configForm.ShowDialog() = DialogResult.OK Then
            '    bgWorker.RunWorkerAsync()
            'End If

        End If
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        bgWorker.RunWorkerAsync()

        CargarColaImpresion()
        CargarColaCerradaImpresion()

    End Sub

    Private Sub cmdTestImpresion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdTestImpresion.ItemClick
        Try
            Dim selectedRow As DataRowView = TryCast(gridViewPrinters.GetFocusedRow(), DataRowView)
            If selectedRow IsNot Nothing Then
                Dim printerName As String = selectedRow("Nombre").ToString()
                Dim tipoConexion As String = selectedRow("Tipo").ToString()
                Dim puertoIP As String = selectedRow("Puerto/IP").ToString()

                If tipoConexion = "USB" Then
                    ImprimirPorUSB(printerName, "^XA^FO50,50^ADN,36,20^FDPrueba de impresión^FS^XZ", "ZPL")
                ElseIf tipoConexion = "IP" Then
                    ImprimirPorIP(puertoIP, 9100, "^XA^FO50,50^ADN,36,20^FDPrueba de impresión^FS^XZ", "ZPL")
                Else
                    XtraMessageBox.Show("Tipo de conexión no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function ImprimirPorUSB(printerName As String, zpl As String, formato As String) As Boolean
        ImprimirPorUSB = False
        Try

            Select Case formato
                Case "ZPL"
                    If RawPrinterHelper.PrintUSB(printerName, zpl) Then
                        ImprimirPorUSB = True
                    End If
                Case "Texto"
                    Dim p As New PrintDocument()
                    p.PrinterSettings.PrinterName = printerName
                    AddHandler p.PrintPage, Sub(sender, e)
                                                e.Graphics.DrawString(zpl, New Font("Arial", 10), Brushes.Black, 10, 10)
                                            End Sub
                    p.Print()
                    ImprimirPorUSB = True
                Case "PDF"
                    Process.Start("AcroRd32.exe", $"/t {zpl} {printerName}")
                    ImprimirPorUSB = True
                Case "ESC/POS"
                    Using writer As New StreamWriter(New FileStream(printerName, FileMode.OpenOrCreate))
                        writer.Write(zpl)
                    End Using
                    ImprimirPorUSB = True
                Case Else
                    XtraMessageBox.Show("Formato no soportado para impresión USB.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Select

            If ImprimirPorUSB Then
                XtraMessageBox.Show("Impresión enviada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Error al imprimir: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Function ImprimirPorIP(ip As String, port As Integer, zpl As String, formato As String) As Boolean

        ImprimirPorIP = False

        Try

            Using client As New TcpClient(ip, port)
                Using stream As NetworkStream = client.GetStream()
                    Dim buffer As Byte()
                    Select Case formato
                        Case "ZPL", "ESC/POS"
                            buffer = Encoding.ASCII.GetBytes(zpl)
                        Case "Texto"
                            buffer = Encoding.UTF8.GetBytes(zpl & vbCrLf)
                        Case Else
                            XtraMessageBox.Show("Formato no soportado para impresión por IP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return False
                    End Select
                    stream.Write(buffer, 0, buffer.Length)
                    ImprimirPorIP = True
                End Using
            End Using
            XtraMessageBox.Show("Impresión enviada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            XtraMessageBox.Show("Error al imprimir: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function


    Private Sub tlbCrearTarea_Click(sender As Object, e As EventArgs) Handles tlbCrearTarea.Click
        Try
            Dim txtZPL As String = "PRUEBA DE IMPRESION"

            Dim selectedRow As DataRowView = TryCast(gridViewPrinters.GetFocusedRow(), DataRowView)
            If selectedRow Is Nothing Then
                XtraMessageBox.Show("Debe seleccionar una impresora antes de imprimir.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim printerName As String = selectedRow("Nombre").ToString()
            Dim tipoConexion As String = selectedRow("Tipo").ToString()
            Dim puertoIP As String = selectedRow("Puerto/IP").ToString()
            Dim Estado As String = selectedRow("Estado").ToString()
            Dim IdImpresora As Integer = selectedRow("Id")

            If Estado <> "Activa" Then
                XtraMessageBox.Show("La impresora debe estar disponible para impresión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If String.IsNullOrEmpty(puertoIP) Then
                XtraMessageBox.Show("La configuracion Puerto/IP no puede estar vacia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If


            '#GT05032025: definir el source para impresion (importar un archivo y guardarlo en la bd? o tenerlo previamente guardado desde otro mantto?)
            Dim datosImpresion As String = txtZPL.Trim()
            If String.IsNullOrEmpty(datosImpresion) Then
                XtraMessageBox.Show("El contenido de impresión no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If


            objColaImpresion = New clsBeQT_Impresion_cola
            objColaImpresion.Impresora = printerName
            objColaImpresion.Tipoconexion = tipoConexion
            objColaImpresion.Detalleconexion = puertoIP
            objColaImpresion.DataImpresion = datosImpresion
            objColaImpresion.TipoImpresion = "ZPL"

            If AgregarACola(objColaImpresion) Then
                XtraMessageBox.Show("Trabajo agregado a la cola de impresión.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CargarColaImpresion()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
       Text,
       MessageBoxButtons.OK,
       MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub tsbIniciarTarea_Click(sender As Object, e As EventArgs) Handles tsbIniciarTarea.Click

        Try

            ProcesarColaImpresion()
            CargarColaImpresion()
            CargarColaCerradaImpresion()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
       Text,
       MessageBoxButtons.OK,
       MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub tlbReintento_Click(sender As Object, e As EventArgs) Handles tlbReintentarTarea.Click

        Try

            Dim selectedRow As DataRowView = TryCast(gridviewCola.GetFocusedRow(), DataRowView)
            If selectedRow Is Nothing Then
                XtraMessageBox.Show("Seleccione un trabajo fallido para reintentar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim jobId As Integer = Convert.ToInt32(selectedRow("IdCola"))
            Dim estado As String = selectedRow("estado").ToString()

            If estado <> "Error" Then
                XtraMessageBox.Show("Solo se pueden reintentar trabajos fallidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim pTareaConError As New clsBeQT_Impresion_cola
            pTareaConError.IdColaImpresion = jobId
            clsLnQT_Impresion_cola.GetSingle(pTareaConError)

            If ActualizarEstadoCola(pTareaConError, "Pendiente") Then
                XtraMessageBox.Show("Trabajo reprogramado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            CargarColaImpresion()


        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub tsbCancelarTarea_Click(sender As Object, e As EventArgs) Handles tsbCancelarTarea.Click
        Try

            Dim selectedRow As DataRowView = TryCast(gridviewCola.GetFocusedRow(), DataRowView)
            If selectedRow Is Nothing Then
                XtraMessageBox.Show("Seleccione un trabajo para cancelar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim jobId As Integer = Convert.ToInt32(selectedRow("IdCola"))
            Dim estado As String = selectedRow("estado").ToString()

            If estado <> "Pendiente" AndAlso estado <> "Error" Then
                XtraMessageBox.Show("Solo se pueden cancelar trabajos en espera o con error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim pTareaConError As New clsBeQT_Impresion_cola
            pTareaConError.IdColaImpresion = jobId
            clsLnQT_Impresion_cola.GetSingle(pTareaConError)

            If ActualizarEstadoCola(pTareaConError, "Cancelado") Then
                XtraMessageBox.Show("Trabajo cancelado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                XtraMessageBox.Show("Trabajo no se canceló.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            CargarColaImpresion()
            CargarColaCerradaImpresion()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub


    Private Function Validar(ByRef pImpresionPendiente As clsBeQT_Impresion_cola) As Boolean

        Validar = False

        Try
            If String.IsNullOrEmpty(pImpresionPendiente.Tipoconexion) Then
                XtraMessageBox.Show("La impresora no tiene un tipo de conexion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ElseIf String.IsNullOrEmpty(pImpresionPendiente.Detalleconexion) Then
                XtraMessageBox.Show("La impresora no tiene definido un puerto/IP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ElseIf String.IsNullOrEmpty(pImpresionPendiente.DataImpresion) Then
                XtraMessageBox.Show("La cola de impresión no tiene definida una etiqueta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Validar = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Exclamation)
        End Try

    End Function


    Private Function ObtenerConfiguracionImpresora(printerName As String) As clsBeQT_Impresora

        ObtenerConfiguracionImpresora = Nothing

        Try
            pImpresora = New clsBeQT_Impresora()
            pImpresora.Descripcion = printerName

            clsLnQT_Impresora.GetSingle_By_Name(pImpresora)
            ObtenerConfiguracionImpresora = pImpresora

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Exclamation)
        End Try

    End Function


#Region "procesos de enviar, procesar, cola y log"

    Private Function AgregarACola(ByRef pColaImpresion As clsBeQT_Impresion_cola) As Boolean
        AgregarACola = False

        Try

            Dim pIdColaIMpresion = clsLnQT_Impresion_cola.MaxID() + 1

            pColaImpresion.User_agr = 1
            pColaImpresion.Fec_agr = Now
            pColaImpresion.User_mod = 1
            pColaImpresion.Fec_mod = Now
            pColaImpresion.Estado = "Pendiente"

            '#GT06032025: prueba para enviar N trabajos
            For i = 1 To 3
                pIdColaIMpresion += 1

                pColaImpresion.IdColaImpresion = pIdColaIMpresion

                clsLnQT_Impresion_cola.Insertar(pColaImpresion)
            Next

            AgregarACola = True


        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub ProcesarColaImpresion()

        Try
            Dim pImpresionPendiente = clsLnQT_Impresion_cola.GetSingle()

            '#GT05032025: hay un trabajo pendiente
            If pImpresionPendiente IsNot Nothing Then

                Dim config As clsBeQT_Impresora = ObtenerConfiguracionImpresora(pImpresionPendiente.Impresora)


                If config IsNot Nothing Then

                    If Validar(pImpresionPendiente) Then
                        ' Marcar en proceso el trabajo pendiente
                        If ActualizarEstadoCola(pImpresionPendiente, "Procesando") Then

                            '#GT05032025: enviar impresion y marcar como Completado
                            Dim exito As Boolean = False

                            For i As Integer = 1 To config.Reintentos_Impresion

                                If pImpresionPendiente.Tipoconexion = "USB" Then
                                    exito = ImprimirPorUSB(pImpresionPendiente.Impresora, pImpresionPendiente.DataImpresion, config.Formato_Impresion)
                                ElseIf pImpresionPendiente.Tipoconexion = "IP" Then
                                    exito = ImprimirPorIP(puertoIP, 9100, pImpresionPendiente.DataImpresion, config.Formato_Impresion)
                                End If

                                If exito Then Exit For
                                Threading.Thread.Sleep(config.Delay_Impresion)

                            Next

                            'If pImpresionPendiente.Tipoconexion = "USB" Then
                            '    exito = ImprimirPorUSB(pImpresionPendiente.Impresora, pImpresionPendiente.DataImpresion)
                            'ElseIf pImpresionPendiente.Tipoconexion = "IP" Then
                            '    exito = ImprimirPorIP(puertoIP, 9100, pImpresionPendiente.DataImpresion)
                            'End If

                            ' Actualizar estado según el resultado
                            ActualizarEstadoCola(pImpresionPendiente, If(exito, "Completado", "Error"))

                            CargarColaImpresion()
                            CargarColaCerradaImpresion()

                        End If
                    End If

                End If


            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Function ImprimirTexto(printerName As String, texto As String) As Boolean
        Try
            Dim p As New PrintDocument()
            p.PrinterSettings.PrinterName = printerName
            AddHandler p.PrintPage, Sub(sender, e)
                                        e.Graphics.DrawString(texto, New Font("Arial", 10), Brushes.Black, 10, 10)
                                    End Sub
            p.Print()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function



    Private Function ImprimirPDF(printerName As String, filePath As String) As Boolean
        Try
            Process.Start("AcroRd32.exe", $"/t {filePath} {printerName}")
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function ImprimirESCPOS(printerName As String, comando As String) As Boolean
        Try
            Using writer As New IO.StreamWriter(New IO.FileStream(printerName, IO.FileMode.OpenOrCreate))
                writer.Write(comando)
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function




    '#GT05032025: actualizar la cola según el proceso: procesando, completado, error.
    Private Function ActualizarEstadoCola(pImpresionEnCola As clsBeQT_Impresion_cola, estado As String) As Boolean
        ActualizarEstadoCola = False
        Try
            pImpresionEnCola.Estado = estado
            ActualizarEstadoCola = clsLnQT_Impresion_cola.Actualizar(pImpresionEnCola) > 0
        Catch ex As Exception

        End Try
    End Function

    Private Sub CargarColaImpresion()
        Try

            If InvokeRequired Then
                Invoke(New MethodInvoker(AddressOf CargarColaImpresion))
            Else
                Dim tableTrabajosEnCola As New DataTable

                tableTrabajosEnCola = clsLnQT_Impresion_cola.CargarColaImpresion()

                If tableTrabajosEnCola IsNot Nothing AndAlso tableTrabajosEnCola.Rows.Count > 0 Then

                    grdCola.DataSource = tableTrabajosEnCola

                    gridviewCola.BestFitColumns()

                    lblEstadoCola.Caption = $"📋 Trabajos en cola: {tableTrabajosEnCola.Rows.Count}"

                    gridviewCola.OptionsBehavior.Editable = False

                Else
                    lblEstadoCola.Caption = "Trabajos en cola: 0 "
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CargarColaCerradaImpresion()
        Try

            If InvokeRequired Then
                Invoke(New MethodInvoker(AddressOf CargarColaCerradaImpresion))
            Else
                Dim tableTrabajosProcesados As New DataTable

                tableTrabajosProcesados = clsLnQT_Impresion_cola.CargarColaCerradaImpresion()

                If tableTrabajosProcesados IsNot Nothing AndAlso tableTrabajosProcesados.Rows.Count > 0 Then

                    grdProcesado.DataSource = tableTrabajosProcesados

                    gridviewProcesado.BestFitColumns()

                    lblEstadoProcesado.Caption = $"📋 Trabajos procesados: {tableTrabajosProcesados.Rows.Count}"

                    gridviewProcesado.OptionsBehavior.Editable = False

                Else
                    lblEstadoProcesado.Caption = $"📋 Trabajos procesados: 0 "
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub


    Dim logTable As New DataTable
    Private Sub CargarLog(filtrarCompletados As Boolean)
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() CargarLog(filtrarCompletados)))
        Else

            Dim logText As New StringBuilder()

            logTable = clsLnImpresion_log.Listar_By_Filtro(filtrarCompletados)

            If logTable IsNot Nothing AndAlso logTable.Rows.Count > 0 Then
                For Each row As DataRow In logTable.Rows
                    logText.AppendLine($"[{row("Fecha")}] Trabajo {row("Trabajo")} en {row("Impresora")} - Acción: {row("Acción")}, Estado: {row("Estado")}")
                Next

                lblLog.Text = logText.ToString()
            End If

            '#GT10032025: mostrar o no el grid de historial cuando son trabajos completados
            'gridHistorial.DataSource = historialTable

        End If
    End Sub


#End Region





End Class
