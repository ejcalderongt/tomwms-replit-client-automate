Imports System.Threading
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports Zebra.Sdk.Comm
Imports Zebra.Sdk.Printer
Imports Zebra.Sdk.Printer.Discovery

Public Class Form1

    Private splash As SplashScreenManager

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim impresoraSeleccionada As ImpresoraZebra = TryCast(GridView1.GetRow(GridView1.FocusedRowHandle), ImpresoraZebra)

        If impresoraSeleccionada Is Nothing Then Return

        Imprimir_Etiqueta_Sin_Tag(impresoraSeleccionada)

    End Sub

    Private Function ObtenerImpresorasZebraUSB() As List(Of ImpresoraZebra)

        Dim lista As New List(Of ImpresoraZebra)

        Try

            WaitFormHelper.Show("Buscando", "Buscando impresoras USB...")

            '"Buscando", "Buscando impresoras USB Zebra..."
            ' Driver
            For Each printer As DiscoveredPrinterDriver In UsbDiscoverer.GetZebraDriverPrinters()
                lista.Add(New ImpresoraZebra With {
                .Nombre = printer.PrinterName,
                .Direccion = printer.Address,
                .TipoConexion = "Driver"
            })
            Next

            ' USB
            For Each usbPrinter As DiscoveredUsbPrinter In UsbDiscoverer.GetZebraUsbPrinters(New ZebraPrinterFilter())
                lista.Add(New ImpresoraZebra With {
                .Nombre = "Zebra USB (" & usbPrinter.Address & ")",
                .Direccion = usbPrinter.Address,
                .TipoConexion = "USB",
                .PuertoUsb = usbPrinter.Address ' Aquí guardamos el puerto real
            })
            Next

        Catch ex As ConnectionException
            XtraMessageBox.Show("Error al descubrir impresoras USB: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            WaitFormHelper.Close()
        End Try

        Return lista

    End Function

    Private Sub CargarImpresorasEnGrid()
        Dim impresoras As List(Of ImpresoraZebra) = ObtenerTodasLasImpresoras()
        GridControl1.DataSource = impresoras
        GridView1.BestFitColumns()
    End Sub

    Private Function ObtenerImpresorasRed() As List(Of ImpresoraZebra)

        Dim listaRed As New List(Of ImpresoraZebra)
        Dim handler As New ZebraDiscoveryHandler(listaRed)

        Try

            WaitFormHelper.Show("Buscando", "Buscando impresoras en red...")

            NetworkDiscoverer.FindPrinters(handler)

            ' Esperar hasta 5 segundos
            Dim timeout = 5000
            Dim espera = 0
            While Not handler.Completado AndAlso espera < timeout
                Thread.Sleep(100)
                espera += 100
            End While

        Catch ex As DiscoveryException
            XtraMessageBox.Show("Error al descubrir impresoras de red: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            WaitFormHelper.Close()
        End Try

        Return listaRed

    End Function

    Public Function ObtenerTodasLasImpresoras() As List(Of ImpresoraZebra)
        Dim lista As New List(Of ImpresoraZebra)

        lista.AddRange(ObtenerImpresorasZebraUSB())
        lista.AddRange(ObtenerImpresorasRed())

        ' Eliminar duplicados por dirección
        Return lista.GroupBy(Function(p) p.Direccion).Select(Function(g) g.First()).ToList()
    End Function

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick

        Dim impresoraSeleccionada As ImpresoraZebra = TryCast(GridView1.GetRow(GridView1.FocusedRowHandle), ImpresoraZebra)

        If impresoraSeleccionada Is Nothing Then Return

        Imprimir_Etiqueta_Con_Tag(impresoraSeleccionada)

    End Sub

    Public Sub ImprimirEtiquetaRFID(impresora As ImpresoraZebra, licenciaRFID As String)

        Dim conexion As Connection = Nothing
        Dim printer As ZebraPrinter = Nothing

        ' Convertir licencia a hexadecimal para grabarla en RFID
        Dim hexRFID As String = String.Concat(licenciaRFID.Select(Function(c) Asc(c).ToString("X2")))

        ' ZPL con RFID + diseño visual
        Dim zpl As String = "^XA" & vbCrLf &
                            "^RS8" & vbCrLf &
                            $"^RFW,H,1,10,1^FD{hexRFID}^FS" & vbCrLf &
                            "^FO630,45^GFA,1530,1530,15,gNFC,::::::::::JFEK0E1F80F1FF8J03IFC,::JFE1IF0FE07F01E387FFC3IFC,JFE1IF0FE07F01C387FFC3IFC," & vbCrLf &
                            ":JFE1C0F0FFC780IF8781C3IFC,JFE1C070FFC780IF8701C3IFC,::JFE1C070JF80IF8701C3IFC,::JFE1IF0FE38701E387FFC3IFC," & vbCrLf &
                            "JFE1IF0FE38701C387FFC3IFC,:JFEK0E1C7801FF8J03IFC,::KFJ01E1C7801FFCJ07IFC,PFE1C78FFC3OFC,:PFE3C78FFC3OFC," & vbCrLf &
                            "JFE0380F1FC07FE0387FFC3IFC,JFE038T03C3IFC,:KFE07U03JFC,:KFE06U03JFC,KFE04T03KFC,KFEV03KFC," & vbCrLf &
                            "::KFE38T0203IFC,::KFE003E7CE30C66C33C3C3IFC,KFEI08C66304E4E7603C3IFC,KFEI08826706E4E7703C3IFC," & vbCrLf &
                            "KF03808825506A4BB1C33JFC,JFE03808C65903989B0623JFC,JFE038086CC90318936C23JFC,KF0380838410118823821JFC," & vbCrLf &
                            "LFCU0203IFC,::KFE38T03C3IFC,::KFEV063JFC,KFEV043JFC,KFEV083JFC,KFEU01E3JFC," & vbCrLf &
                            "MF8S03E3JFC,MF8R01DE3JFC,MFCOFE7FF9E3JFC,QFE00F9FE7CF1FC3IFC,QFE0071FE3871FC3IFC," & vbCrLf &
                            "QFE0070FE3870FC3IFC,JFEK0FE3870E03FF0E3JFC,::JFE1IF0F3F80FEI0F0LFC," & vbCrLf &
                            "JFE1IF0E1F80FEI070LFC,::JFE1C070E1C7FK070E03IFC,:JFE1C070E3C7FK070E03IFC," & vbCrLf &
                            "JFE1C070FFC7F0E3C780E03IFC,::JFE1IF0E3C78IFC780E03IFC,JFE1IF0E1C78IFC780E03IFC," & vbCrLf &
                            ":JFE1IF0E1C70IF8380E03IFC,JFEK0E0380FFC0070FC3IFC,:JFEK0E03C0FFE0071FE3IFC," & vbCrLf &
                            "gNFC,:::::::::::^FS" & vbCrLf &
                            "^FO40,50^A0N,30,25^FD TOMWMS RFID-INDUSTRIA LA POPULAR^FS" & vbCrLf &
                            "^FO40,150^A0N,30,25^FD Producto DEMO^FS" & vbCrLf &
                            "^FO50,215^GB720,3,3^FS" & vbCrLf &
                            "^FO50,260^GB420,120,3^FS" & vbCrLf &
                            "^BY2,2,40^FO60,270^BC^FD" & licenciaRFID & "^FS" & vbCrLf &
                            "^FO560,265^A0N,25,25^FD Vence: 01/08/2025^FS" & vbCrLf &
                            "^FO560,300^A0N,25,25^FD Lote:    01082025^FS" & vbCrLf &
                            "^FO560,335^A0N,25,25^FD Peso:   100kg^FS" & vbCrLf &
                            "^XZ"

        Try

            conexion = ZebraConnectionHelper.ObtenerConexion(impresora)
            conexion.Open()

            Try

                printer = ZebraPrinterFactory.GetInstance(conexion)
                printer.SendCommand(zpl)

            Catch ex As Exception

                If Not TypeOf conexion Is UsbConnection Then
                    Try
                        conexion.Close()

                        Dim puertoUsb As String = impresora.PuertoUsb
                        If String.IsNullOrWhiteSpace(puertoUsb) Then
                            Throw New ApplicationException("La impresora USB no tiene puerto asignado. Por favor vuelva a escanear.")
                        End If

                        conexion = New UsbConnection(puertoUsb)
                        conexion.Open()

                        printer = ZebraPrinterFactory.GetInstance(conexion)
                        printer.SendCommand(zpl)

                    Catch usbEx As Exception
                        Throw New ApplicationException("Fallo al intentar fallback por USB: " & usbEx.Message, usbEx)
                    End Try
                Else
                    Throw New ApplicationException("Error con conexión USB: " & ex.Message, ex)
                End If
            End Try

        Catch exFinal As Exception
            Throw New ApplicationException("No se pudo imprimir: " & exFinal.Message, exFinal)

        Finally
            If conexion IsNot Nothing AndAlso conexion.Connected Then
                conexion.Close()
            End If
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        CargarImpresorasEnGrid()
    End Sub

    Private Sub Imprimir_Etiqueta_Con_Tag(ByVal impresoraSeleccionada As ImpresoraZebra)

        ' Mensaje de confirmación con ícono de impresora (pregunta)
        Dim mensaje As String = $"¿Imprimir etiqueta en impresora: {impresoraSeleccionada.Nombre} con conexión: {impresoraSeleccionada.TipoConexion.ToUpper()}?"
        Dim respuesta As DialogResult = XtraMessageBox.Show(mensaje, "Confirmar impresión",
                                                                                MessageBoxButtons.YesNo,
                                                                                MessageBoxIcon.Question)

        If respuesta = System.Windows.Forms.DialogResult.Yes Then

            Dim tagRfid As String = "WE000000042"

            WaitFormHelper.Show("Imprimiendo", "Enviando a impresora...")

            Try
                ImprimirEtiquetaRFID(impresoraSeleccionada, tagRfid)
                XtraMessageBox.Show("Etiqueta enviada correctamente a " & impresoraSeleccionada.Nombre,
                                                       "Impresión exitosa",
                                                       MessageBoxButtons.OK,
                                                       MessageBoxIcon.Information)

            Catch ex As Exception
                XtraMessageBox.Show("Error al imprimir: " & ex.Message,
                                                       "Error",
                                                       MessageBoxButtons.OK,
                                                       MessageBoxIcon.Error)
            Finally
                WaitFormHelper.Close()
            End Try
        End If

    End Sub

    Private Sub Imprimir_Etiqueta_Sin_Tag(ByVal impresoraSeleccionada As ImpresoraZebra)

        ' Mensaje de confirmación con ícono de impresora (pregunta)
        Dim mensaje As String = $"¿Imprimir etiqueta en impresora: {impresoraSeleccionada.Nombre} con conexión: {impresoraSeleccionada.TipoConexion.ToUpper()}?"
        Dim respuesta As DialogResult = XtraMessageBox.Show(mensaje, "Confirmar impresión",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question)

        If respuesta = System.Windows.Forms.DialogResult.Yes Then

            Dim tagRfid As String = "WE000000042"

            WaitFormHelper.Show("Imprimiendo", "Enviando a impresora...")

            Try
                ImprimirEtiquetaSinTag(impresoraSeleccionada, tagRfid)
                XtraMessageBox.Show("Etiqueta enviada correctamente a " & impresoraSeleccionada.Nombre,
                                     "Impresión exitosa",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information)

            Catch ex As Exception
                XtraMessageBox.Show("Error al imprimir: " & ex.Message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
            Finally
                WaitFormHelper.Close()
            End Try
        End If

    End Sub

    Public Sub ImprimirEtiquetaSinTag(impresora As ImpresoraZebra, licenciaRFID As String)

        Dim conexion As Connection = Nothing
        Dim printer As ZebraPrinter = Nothing

        ' Convertir licencia a hexadecimal para grabarla en RFID
        Dim hexRFID As String = String.Concat(licenciaRFID.Select(Function(c) Asc(c).ToString("X2")))

        ' ZPL con RFID + diseño visual
        Dim zpl As String = "^XA" & vbCrLf &
                            "^FO630,45^GFA,1530,1530,15,gNFC,::::::::::JFEK0E1F80F1FF8J03IFC,::JFE1IF0FE07F01E387FFC3IFC,JFE1IF0FE07F01C387FFC3IFC," & vbCrLf &
                            ":JFE1C0F0FFC780IF8781C3IFC,JFE1C070FFC780IF8701C3IFC,::JFE1C070JF80IF8701C3IFC,::JFE1IF0FE38701E387FFC3IFC," & vbCrLf &
                            "JFE1IF0FE38701C387FFC3IFC,:JFEK0E1C7801FF8J03IFC,::KFJ01E1C7801FFCJ07IFC,PFE1C78FFC3OFC,:PFE3C78FFC3OFC," & vbCrLf &
                            "JFE0380F1FC07FE0387FFC3IFC,JFE038T03C3IFC,:KFE07U03JFC,:KFE06U03JFC,KFE04T03KFC,KFEV03KFC," & vbCrLf &
                            "::KFE38T0203IFC,::KFE003E7CE30C66C33C3C3IFC,KFEI08C66304E4E7603C3IFC,KFEI08826706E4E7703C3IFC," & vbCrLf &
                            "KF03808825506A4BB1C33JFC,JFE03808C65903989B0623JFC,JFE038086CC90318936C23JFC,KF0380838410118823821JFC," & vbCrLf &
                            "LFCU0203IFC,::KFE38T03C3IFC,::KFEV063JFC,KFEV043JFC,KFEV083JFC,KFEU01E3JFC," & vbCrLf &
                            "MF8S03E3JFC,MF8R01DE3JFC,MFCOFE7FF9E3JFC,QFE00F9FE7CF1FC3IFC,QFE0071FE3871FC3IFC," & vbCrLf &
                            "QFE0070FE3870FC3IFC,JFEK0FE3870E03FF0E3JFC,::JFE1IF0F3F80FEI0F0LFC," & vbCrLf &
                            "JFE1IF0E1F80FEI070LFC,::JFE1C070E1C7FK070E03IFC,:JFE1C070E3C7FK070E03IFC," & vbCrLf &
                            "JFE1C070FFC7F0E3C780E03IFC,::JFE1IF0E3C78IFC780E03IFC,JFE1IF0E1C78IFC780E03IFC," & vbCrLf &
                            ":JFE1IF0E1C70IF8380E03IFC,JFEK0E0380FFC0070FC3IFC,:JFEK0E03C0FFE0071FE3IFC," & vbCrLf &
                            "gNFC,:::::::::::^FS" & vbCrLf &
                            "^FO40,50^A0N,30,25^FD TOMWMS RFID-INDUSTRIA LA POPULAR^FS" & vbCrLf &
                            "^FO40,150^A0N,30,25^FD Producto DEMO^FS" & vbCrLf &
                            "^FO50,215^GB720,3,3^FS" & vbCrLf &
                            "^FO50,260^GB420,120,3^FS" & vbCrLf &
                            "^BY2,2,40^FO60,270^BC^FD" & licenciaRFID & "^FS" & vbCrLf &
                            "^FO560,265^A0N,25,25^FD Vence: 01/08/2025^FS" & vbCrLf &
                            "^FO560,300^A0N,25,25^FD Lote:    01082025^FS" & vbCrLf &
                            "^FO560,335^A0N,25,25^FD Peso:   100kg^FS" & vbCrLf &
                            "^XZ"

        Try

            conexion = ZebraConnectionHelper.ObtenerConexion(impresora)
            conexion.Open()

            Try

                printer = ZebraPrinterFactory.GetInstance(conexion)
                printer.SendCommand(zpl)

            Catch ex As Exception

                If Not TypeOf conexion Is UsbConnection Then
                    Try
                        conexion.Close()

                        Dim puertoUsb As String = impresora.PuertoUsb
                        If String.IsNullOrWhiteSpace(puertoUsb) Then
                            Throw New ApplicationException("La impresora USB no tiene puerto asignado. Por favor vuelva a escanear.")
                        End If

                        conexion = New UsbConnection(puertoUsb)
                        conexion.Open()

                        printer = ZebraPrinterFactory.GetInstance(conexion)
                        printer.SendCommand(zpl)

                    Catch usbEx As Exception
                        Throw New ApplicationException("Fallo al intentar fallback por USB: " & usbEx.Message, usbEx)
                    End Try
                Else
                    Throw New ApplicationException("Error con conexión USB: " & ex.Message, ex)
                End If

            End Try

        Catch exFinal As Exception
            Throw New ApplicationException("No se pudo imprimir: " & exFinal.Message, exFinal)

        Finally
            If conexion IsNot Nothing AndAlso conexion.Connected Then
                conexion.Close()
            End If
        End Try

    End Sub

End Class