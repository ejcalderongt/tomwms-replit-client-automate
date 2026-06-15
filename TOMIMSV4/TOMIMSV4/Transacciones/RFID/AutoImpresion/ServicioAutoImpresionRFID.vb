Imports System.Data.SqlClient
Imports System.Management
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text
Imports DevExpress.XtraEditors

Public Class ServicioAutoImpresionRFID

    Private BeEmpresa As clsBeEmpresa
    Public Es_Demo As Boolean
    Public Property NotificarMensaje As Action(Of String)

    Public Sub New(pEmpresa As clsBeEmpresa)
        Try
            BeEmpresa = pEmpresa
        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error("AUTO_RFID: New ServicioAutoImpresionRFID " & ex.Message)
        End Try
    End Sub

    Private Sub NotificarRFID(ByVal mensaje As String, Optional ByVal registrarLog As Boolean = False)

        Try
            If registrarLog Then
                clsLnLog_error_wms.Agregar_Error(mensaje)
            End If

            If NotificarMensaje IsNot Nothing Then
                NotificarMensaje.Invoke(mensaje)
            End If

        Catch ex As Exception
            'Log silencioso
        End Try

    End Sub

    Private Sub RollbackSeguro(ByVal pTransaccion As clsTransaccion)

        Try
            If pTransaccion IsNot Nothing AndAlso pTransaccion.lTransaction IsNot Nothing Then
                pTransaccion.lTransaction.Rollback()
            End If
        Catch
        End Try

    End Sub

    Public Shared Function Cargar_Impresora_Zebra() As String
        Try
            Dim impresoraDefault As String = String.Empty
            Dim primeraImpresoraZebra As String = String.Empty

            Using searcher As New ManagementObjectSearcher("SELECT Name, DriverName, PortName, Default, WorkOffline FROM Win32_Printer")
                Using results = searcher.Get()
                    For Each mo As ManagementObject In results

                        Dim name As String = If(TryCast(mo("Name"), String), "")
                        Dim driverName As String = If(TryCast(mo("DriverName"), String), "")
                        Dim isDefault As Boolean = False

                        Try
                            isDefault = If(mo("Default") IsNot Nothing, Convert.ToBoolean(mo("Default")), False)
                        Catch
                            isDefault = False
                        End Try

                        '#GT08062026: validar que existe una printer zebra, de lo contrario no imprimira los tag
                        Dim isZebra As Boolean =
                            driverName.IndexOf("zebra", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                            driverName.IndexOf("zdesigner", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                            name.IndexOf("zebra", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                            name.IndexOf("zdesigner", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                            name.IndexOf("zt", StringComparison.OrdinalIgnoreCase) >= 0

                        If isZebra Then
                            If String.IsNullOrWhiteSpace(primeraImpresoraZebra) Then
                                primeraImpresoraZebra = name
                            End If

                            If isDefault Then
                                impresoraDefault = name
                                Exit For
                            End If
                        End If

                    Next
                End Using
            End Using

            If Not String.IsNullOrWhiteSpace(impresoraDefault) Then
                Return impresoraDefault
            End If

            Return primeraImpresoraZebra

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error("AUTO_RFID: Cargar_Impresora_Zebra " & ex.Message)
            Return String.Empty
        End Try
    End Function

    Public Sub ProcesarAutoImpresion(printerName As String, cantidadCopias As Integer)

        Dim clsTransaccion As New clsTransaccion

        Try

            NotificarRFID("RFID: Iniciando proceso de autoimpresión...")

            clsTransaccion.Begin_Transaction()

            If Not Es_Demo Then

                If String.IsNullOrWhiteSpace(printerName) Then
                    NotificarRFID("AUTO_RFID: Nombre de impresora vacío.", True)
                    RollbackSeguro(clsTransaccion)
                    Exit Sub
                End If

                If cantidadCopias <= 0 Then
                    cantidadCopias = 1
                End If

                Dim st = GetPrinterStatus(printerName)

                If Not st.found Then
                    NotificarRFID("AUTO_RFID: Impresora no encontrada: " & printerName, True)
                    RollbackSeguro(clsTransaccion)
                    Exit Sub
                End If

                If Not st.ok Then
                    NotificarRFID("AUTO_RFID: Impresora no lista: " & printerName & " - " & st.message, True)
                    RollbackSeguro(clsTransaccion)
                    Exit Sub
                End If

            Else

                If cantidadCopias <= 0 Then
                    cantidadCopias = 1
                End If

                NotificarRFID("RFID DEMO: Modo simulación activo.")

            End If

            '#GT05062026: leer las barras, asignarlas e imprimirlas dentro de la tran.
            Dim listaBarrasPallet As List(Of clsBeI_nav_barras_pallet) = ObtenerBarrasPendientesImpresion(clsTransaccion.lConnection,
                                                                                                          clsTransaccion.lTransaction)

            If listaBarrasPallet Is Nothing OrElse listaBarrasPallet.Count = 0 Then
                NotificarRFID("RFID: No hay barras pendientes para autoimpresión.")
                clsTransaccion.Commit_Transaction()
                Exit Sub
            End If

            NotificarRFID("RFID: Barras pendientes encontradas: " & listaBarrasPallet.Count.ToString())

            If Not ProcesarIngresoMovimientoStock(listaBarrasPallet, clsTransaccion.lConnection, clsTransaccion.lTransaction) Then
                NotificarRFID("AUTO_RFID: No se pudo completar ingreso/movimiento/stock. Se revierte la operación.", True)
                RollbackSeguro(clsTransaccion)
                Exit Sub
            End If

            If Not ImprimirLista(printerName, listaBarrasPallet, cantidadCopias, clsTransaccion.lConnection, clsTransaccion.lTransaction) Then
                NotificarRFID("AUTO_RFID: No se pudo completar la impresión. Se revierte la operación.", True)
                RollbackSeguro(clsTransaccion)
                Exit Sub
            End If

            clsTransaccion.Commit_Transaction()

            NotificarRFID("RFID: Autoimpresión finalizada correctamente.")

        Catch ex As Exception

            RollbackSeguro(clsTransaccion)
            NotificarRFID("AUTO_RFID: " & MethodBase.GetCurrentMethod.Name() & " " & ex.Message, True)

        Finally

            Try
                If clsTransaccion IsNot Nothing Then

                    If clsTransaccion.lConnection IsNot Nothing AndAlso clsTransaccion.lConnection.State = ConnectionState.Open Then
                        clsTransaccion.lConnection.Close()
                    End If

                    If clsTransaccion.lTransaction IsNot Nothing Then
                        clsTransaccion.lTransaction.Dispose()
                    End If

                    If clsTransaccion.lConnection IsNot Nothing Then
                        clsTransaccion.lConnection.Dispose()
                    End If

                End If
            Catch
            End Try

        End Try

    End Sub

    Private Function ProcesarIngresoMovimientoStock(listaBarrasPallet As List(Of clsBeI_nav_barras_pallet),
                                                    lConnection As SqlConnection,
                                                    ltransaction As SqlTransaction) As Boolean

        ProcesarIngresoMovimientoStock = False

        Try

            If listaBarrasPallet Is Nothing OrElse listaBarrasPallet.Count = 0 Then
                NotificarRFID("AUTO_RFID: No existen barras para registrar ingreso/movimiento/stock.", True)
                Return False
            End If

            Dim pEncabezado As New clsBeI_nav_barras_rfid_enc
            pEncabezado.IdBodega = AP.IdBodega
            pEncabezado.Estado = "Cerrado"
            pEncabezado.Tipo = 1 'Ingreso
            pEncabezado.Fec_agr = Now
            pEncabezado.Fec_mod = Now

            For Each barraPallet As clsBeI_nav_barras_pallet In listaBarrasPallet

                If barraPallet Is Nothing Then
                    NotificarRFID("AUTO_RFID: Existe una barra pallet nula.", True)
                    Return False
                End If

                If String.IsNullOrWhiteSpace(barraPallet.SSCC) Then
                    NotificarRFID("AUTO_RFID: Existe una barra pallet sin SSCC.", True)
                    Return False
                End If

                Dim pBarra_Detalle As New clsBeI_nav_barras_rfid_det
                pBarra_Detalle.IdOperador = AP.UsuarioAp.IdUsuario
                pBarra_Detalle.Barra_epc = barraPallet.SSCC

                pEncabezado.Detalle.Add(pBarra_Detalle)

            Next

            ProcesarIngresoMovimientoStock = clsLnI_nav_barras_rfid_enc.Barra_AutoGuardado_RFID(pEncabezado,
                                                                                                lConnection,
                                                                                                ltransaction)

            If Not ProcesarIngresoMovimientoStock Then
                NotificarRFID("AUTO_RFID: Barra_AutoGuardado_RFID retornó False.", True)
                Return False
            End If

            Return True

        Catch ex As Exception
            NotificarRFID("AUTO_RFID: " & MethodBase.GetCurrentMethod.Name() & " " & ex.Message, True)
            Return False
        End Try

    End Function

    Private Function ObtenerBarrasPendientesImpresion(lConnection As SqlConnection,
                                                      ltransaction As SqlTransaction) As List(Of clsBeI_nav_barras_pallet)
        Try

            Return clsLnI_nav_barras_pallet.Get_All_By_EstadoImpresion(False, lConnection, ltransaction)

        Catch ex As Exception
            NotificarRFID("AUTO_RFID: " & MethodBase.GetCurrentMethod.Name() & " " & ex.Message, True)
            Return Nothing
        End Try

    End Function

    Public Function ImprimirLista(printerName As String,
                                  listaBarrasPallet As List(Of clsBeI_nav_barras_pallet),
                                  cantidadCopias As Integer,
                                  lConnection As SqlConnection,
                                  ltransaction As SqlTransaction) As Boolean

        ImprimirLista = False

        Try

            If listaBarrasPallet Is Nothing OrElse listaBarrasPallet.Count = 0 Then
                NotificarRFID("AUTO_RFID: No existen barras para imprimir.", True)
                Return False
            End If

            For Each barraPallet As clsBeI_nav_barras_pallet In listaBarrasPallet

                If barraPallet Is Nothing Then
                    NotificarRFID("AUTO_RFID: Existe una barra pallet nula en impresión.", True)
                    Return False
                End If

                If Not ImprimirBarraPallet(printerName, barraPallet, cantidadCopias, lConnection, ltransaction) Then
                    Return False
                End If

            Next

            Return True

        Catch ex As Exception
            NotificarRFID("AUTO_RFID: " & MethodBase.GetCurrentMethod.Name() & " " & ex.Message, True)
            Return False
        End Try

    End Function

    Private Function ImprimirBarraPallet(printerName As String,
                                         barraPallet As clsBeI_nav_barras_pallet,
                                         cantidadCopias As Integer,
                                         lConnection As SqlConnection,
                                         ltransaction As SqlTransaction) As Boolean

        ImprimirBarraPallet = False

        Try

            If barraPallet Is Nothing Then
                NotificarRFID("AUTO_RFID: Barra pallet nula.", True)
                Return False
            End If

            Dim beProducto = clsLnProducto.Get_BeProducto_By_Codigo(barraPallet.Codigo, lConnection, ltransaction)

            If beProducto Is Nothing Then
                NotificarRFID("AUTO_RFID: No se encontró producto para código: " & barraPallet.Codigo, True)
                Return False
            End If

            Dim bePresentacion As clsBeProducto_Presentacion =
                clsLnProducto_presentacion.Get_Presentacion_Defecto_By_IdProducto(beProducto.IdProducto, lConnection, ltransaction)

            If bePresentacion Is Nothing Then
                bePresentacion = New clsBeProducto_Presentacion
                bePresentacion.Nombre = "ND"
            End If

            Dim idBodegaOrigen As Integer = 0

            If Not Integer.TryParse(Convert.ToString(barraPallet.Bodega_Origen), idBodegaOrigen) Then
                NotificarRFID("AUTO_RFID: La bodega origen no es numérica para la barra EPC: " & barraPallet.Codigo_barra, True)
                Return False
            End If

            Dim beBodega = clsLnBodega.GetSingle_By_Idbodega(idBodegaOrigen, lConnection, ltransaction)

            If beBodega Is Nothing Then
                NotificarRFID("AUTO_RFID: No se encontró bodega origen: " &
                              idBodegaOrigen.ToString() &
                              " para la barra EPC: " &
                              barraPallet.Codigo_barra, True)
                Return False
            End If

            Dim loteGs1 As String = barraPallet.Lote.ToString().PadLeft(6, "0"c).Substring(0, 6)

            Dim pGS1 As String = "01" & barraPallet.GTIN &
                                  "10" & loteGs1 &
                                  "11" & barraPallet.Fecha_Produccion.ToString("yyMMdd")

            Dim epc96Hex As String = EncodeSsccToEpc96_WithPrefix7406171(barraPallet.Codigo_barra, filterValue:=0)

            Dim zpl As String = BuildZpl_RfidEncode_And_Print(epc96Hex,
                                                              barraPallet.Codigo_barra,
                                                              BeEmpresa.Nombre,
                                                              beBodega.Nombre,
                                                              pGS1,
                                                              barraPallet.SSCC,
                                                              barraPallet.Lote,
                                                              barraPallet.Fecha_Produccion,
                                                              barraPallet.Codigo,
                                                              barraPallet.Cantidad_UMP,
                                                              barraPallet.Nombre,
                                                              barraPallet.GTIN)

            If Not Es_Demo Then

                For i As Integer = 1 To cantidadCopias
                    RawPrinterHelper.SendStringToPrinter(printerName, zpl)
                Next

                NotificarRFID("RFID: Impresión enviada para SSCC: " & barraPallet.SSCC)

            Else

                NotificarRFID("RFID DEMO: Simulación de impresión enviada para " & barraPallet.Codigo_barra)

            End If

            barraPallet.Impreso = True

            If Not clsLnI_nav_barras_pallet.Actualiza_Estado_Barras_Pallet_By_Impresion(barraPallet, lConnection, ltransaction) Then
                NotificarRFID("AUTO_RFID: No se pudo actualizar estado Impreso para SSCC: " & barraPallet.SSCC, True)
                Return False
            End If

            Return True

        Catch ex As Exception
            NotificarRFID("AUTO_RFID: " & MethodBase.GetCurrentMethod.Name() & " " & ex.Message, True)
            Return False
        End Try

    End Function

    Private Function BuildZpl_RfidEncode_And_Print(epc96Hex As String,
                                                   licencia As String,
                                                   empresa As String,
                                                   bodega As String,
                                                   gs1 As String,
                                                   sscc As String,
                                                   lote As String,
                                                   fechaProd As String,
                                                   skuSavona As String,
                                                   cantidad As String,
                                                   descripcionProducto As String,
                                                   GTIN As String) As String
        Try
            Dim sb As New StringBuilder()

            sb.AppendLine("^XA")
            sb.AppendLine("^MMT")
            sb.AppendLine("^PW1035")
            sb.AppendLine("^LL0600")
            sb.AppendLine("^LS0")
            sb.AppendLine("^CI27")

            sb.AppendLine("^RS4")
            sb.AppendLine("^RFW,H^FD" & epc96Hex & "^FS")

            sb.AppendLine("^FT40,36^A0N,20,20^FH^FDTOMWMS^FS")
            sb.AppendLine("^FT350,36^A0N,20,20^FH^FDEmp:^FS")
            sb.AppendLine("^FT404,36^A0N,20,20^FH^FD" & empresa & "^FS")

            sb.AppendLine("^FO0,48^GB1035,4,4^FS")

            sb.AppendLine("^FT40,115^A0N,49,52^FH^FDSAVONA:^FS")
            sb.AppendLine("^FT253,115^A0N,49,52^FH^FD" & skuSavona & "^FS")

            sb.AppendLine("^FT462,108^A0N,29,36^FH^FDGTIN:^FS")
            sb.AppendLine("^FT563,108^A0N,29,36^FH^FD" & GTIN & "^FS")

            sb.AppendLine("^FO40,133^A0N,38,42^FB952,2,6,L,0^FH^FD" & descripcionProducto & "^FS")

            sb.AppendLine("^FT40,242^A0N,26,25^FH^FDCANT:^FS")
            sb.AppendLine("^FT123,242^A0N,26,25^FH^FD" & cantidad & "^FS")

            sb.AppendLine("^FT231,242^A0N,26,25^FH^FDFECHA PROD:^FS")
            sb.AppendLine("^FT397,242^A0N,26,25^FH^FD" & fechaProd & "^FS")

            sb.AppendLine("^FT563,242^A0N,26,25^FH^FDLOTE:^FS")
            sb.AppendLine("^FT642,242^A0N,26,25^FH^FD" & lote & "^FS")

            sb.AppendLine("^FO0,261^GB1035,4,4^FS")

            sb.AppendLine("^BY2,2,120")
            sb.AppendLine("^FT50,391^BCN,111,Y,N,N")
            sb.AppendLine("^FD" & gs1 & "^FS")

            sb.AppendLine("^BY3,2,105")
            sb.AppendLine("^FT50,537^BCN,110,Y,N,N")
            sb.AppendLine("^FD" & sscc & "^FS")

            sb.AppendLine("^PQ1,0,1,Y")
            sb.AppendLine("^XZ")

            Return sb.ToString()

        Catch ex As Exception
            NotificarRFID("AUTO_RFID: " & MethodBase.GetCurrentMethod.Name() & " " & ex.Message, True)
            Return String.Empty
        End Try
    End Function

    Private Function BuildZpl_RfidEncode_And_Print_208dpi(epc96Hex As String,
                                                           licencia As String,
                                                           empresa As String,
                                                           bodega As String,
                                                           gs1 As String,
                                                           sscc As String,
                                                           lote As String,
                                                           fechaProd As String,
                                                           skuSavona As String,
                                                           cantidad As String,
                                                           descripcionProducto As String,
                                                           GTIN As String) As String

        Try
            Dim sb As New StringBuilder()
            Dim fechaActual As String = DateTime.Now.ToString("dd/MM/yyyy")

            sb.AppendLine("^XA")
            sb.AppendLine("^MMT")
            sb.AppendLine("^PW718")
            sb.AppendLine("^LL416")
            sb.AppendLine("^LS0")
            sb.AppendLine("^CI27")

            sb.AppendLine("^RS4")
            sb.AppendLine("^RFW,H^FD" & epc96Hex & "^FS")

            sb.AppendLine("^FT28,25^A0N,14,14^FH^FDTOMWMS^FS")
            sb.AppendLine("^FT243,25^A0N,14,14^FH^FDEmp:^FS")
            sb.AppendLine("^FT280,25^A0N,14,14^FH^FD" & empresa & "^FS")

            sb.AppendLine("^FO0,33^GB718,3,3^FS")

            sb.AppendLine("^FT28,80^A0N,34,36^FH^FDSAVONA:^FS")
            sb.AppendLine("^FT175,80^A0N,34,36^FH^FD" & skuSavona & "^FS")

            sb.AppendLine("^FT320,75^A0N,20,25^FH^FDGTIN:^FS")
            sb.AppendLine("^FT390,75^A0N,20,25^FH^FD" & GTIN & "^FS")

            sb.AppendLine("^FO28,92^A0N,26,29^FB660,2,4,L,0^FH^FD" & descripcionProducto & "^FS")

            sb.AppendLine("^FT28,168^A0N,18,17^FH^FDCANT:^FS")
            sb.AppendLine("^FT85,168^A0N,18,17^FH^FD" & cantidad & "^FS")

            sb.AppendLine("^FT160,168^A0N,18,17^FH^FDFECHA PROD:^FS")
            sb.AppendLine("^FT275,168^A0N,18,17^FH^FD" & fechaProd & "^FS")

            sb.AppendLine("^FT390,168^A0N,18,17^FH^FDLOTE:^FS")
            sb.AppendLine("^FT445,168^A0N,18,17^FH^FD" & lote & "^FS")

            sb.AppendLine("^FO0,181^GB718,3,3^FS")

            sb.AppendLine("^BY1,2,83")
            sb.AppendLine("^FT35,271^BCN,77,Y,N,N")
            sb.AppendLine("^FD" & gs1 & "^FS")

            sb.AppendLine("^BY2,2,73")
            sb.AppendLine("^FT35,372^BCN,76,Y,N,N")
            sb.AppendLine("^FD" & sscc & "^FS")

            sb.AppendLine("^PQ1,0,1,Y")
            sb.AppendLine("^XZ")

            Return sb.ToString()

        Catch ex As Exception
            NotificarRFID("AUTO_RFID: " & MethodBase.GetCurrentMethod.Name() & " " & ex.Message, True)
            Return String.Empty
        End Try

    End Function

    Private Function GetPrinterStatus(printerName As String) As (found As Boolean, ok As Boolean, message As String)
        Try
            Dim target As String = If(printerName, "").Trim()

            If target = "" Then
                Return (False, False, "Nombre de impresora vacío.")
            End If

            If Not RawPrinterHelper.CanOpenPrinter(target) Then
                Return (False, False, "No se pudo abrir la cola de impresión.")
            End If

            Try
                Using searcher As New ManagementObjectSearcher(
                    "SELECT Name, WorkOffline, Offline, Paused, ErrorState, PaperOut, OutputBinFull, DoorOpen FROM Win32_Printer")

                    Using results = searcher.Get()

                        For Each mo As ManagementObject In results

                            Dim name As String = If(TryCast(mo("Name"), String), "").Trim()

                            If String.Equals(name, target, StringComparison.OrdinalIgnoreCase) Then

                                Dim issues As New List(Of String)

                                If SafeBool(mo("WorkOffline")) OrElse SafeBool(mo("Offline")) Then issues.Add("Offline/No disponible")
                                If SafeBool(mo("Paused")) Then issues.Add("Pausada")
                                If SafeBool(mo("PaperOut")) Then issues.Add("Sin papel")
                                If SafeBool(mo("DoorOpen")) Then issues.Add("Puerta abierta")
                                If SafeBool(mo("OutputBinFull")) Then issues.Add("Bandeja llena")
                                If SafeBool(mo("ErrorState")) Then issues.Add("Error de impresora")

                                If issues.Count = 0 Then
                                    Return (True, True, "OK")
                                End If

                                Return (True, False, String.Join(", ", issues))

                            End If

                        Next

                    End Using
                End Using

            Catch ex As Exception
                'Si WMI falla, no se bloquea si OpenPrinter abrió correctamente.
            End Try

            Return (True, True, "OK")

        Catch ex As Exception
            Return (False, False, ex.Message)
        End Try
    End Function

    Private Function SafeBool(value As Object) As Boolean
        Try
            If value Is Nothing Then Return False
            Return Convert.ToBoolean(value)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public NotInheritable Class RawPrinterHelper

        Private Sub New()
        End Sub

        Public Shared Function CanOpenPrinter(printerName As String) As Boolean
            Dim hPrinter As IntPtr = IntPtr.Zero

            Try
                Return OpenPrinter(printerName, hPrinter, IntPtr.Zero)
            Catch ex As Exception
                Return False
            Finally
                If hPrinter <> IntPtr.Zero Then
                    ClosePrinter(hPrinter)
                End If
            End Try
        End Function

        Public Shared Sub SendStringToPrinter(printerName As String, zpl As String)
            Dim hPrinter As IntPtr = IntPtr.Zero

            Dim di As New DOCINFOA() With {
                .pDocName = "ZPL RAW",
                .pDataType = "RAW"
            }

            Try
                If Not OpenPrinter(printerName, hPrinter, IntPtr.Zero) Then
                    Throw New System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "OpenPrinter falló.")
                End If

                If Not StartDocPrinter(hPrinter, 1, di) Then
                    Throw New System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "StartDocPrinter falló.")
                End If

                Try
                    If Not StartPagePrinter(hPrinter) Then
                        Throw New System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "StartPagePrinter falló.")
                    End If

                    Try
                        Dim bytes As Byte() = Encoding.ASCII.GetBytes(zpl)
                        Dim pUnmanaged As IntPtr = Marshal.AllocCoTaskMem(bytes.Length)

                        Try
                            Marshal.Copy(bytes, 0, pUnmanaged, bytes.Length)

                            Dim written As Integer = 0

                            If Not WritePrinter(hPrinter, pUnmanaged, bytes.Length, written) OrElse written <> bytes.Length Then
                                Throw New System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "WritePrinter falló.")
                            End If

                        Finally
                            Marshal.FreeCoTaskMem(pUnmanaged)
                        End Try

                    Finally
                        EndPagePrinter(hPrinter)
                    End Try

                Finally
                    EndDocPrinter(hPrinter)
                End Try

            Catch ex As Exception
                Throw
            Finally
                If hPrinter <> IntPtr.Zero Then
                    ClosePrinter(hPrinter)
                End If
            End Try
        End Sub

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
        Private Structure DOCINFOA
            <MarshalAs(UnmanagedType.LPStr)>
            Public pDocName As String

            <MarshalAs(UnmanagedType.LPStr)>
            Public pOutputFile As String

            <MarshalAs(UnmanagedType.LPStr)>
            Public pDataType As String
        End Structure

        <DllImport("winspool.Drv", EntryPoint:="OpenPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function OpenPrinter(szPrinter As String, ByRef hPrinter As IntPtr, pd As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="ClosePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function ClosePrinter(hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function StartDocPrinter(hPrinter As IntPtr, level As Integer, ByRef di As DOCINFOA) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EndDocPrinter(hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function StartPagePrinter(hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EndPagePrinter(hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", EntryPoint:="WritePrinter", SetLastError:=True, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function WritePrinter(hPrinter As IntPtr,
                                             pBytes As IntPtr,
                                             dwCount As Integer,
                                             ByRef dwWritten As Integer) As Boolean
        End Function

    End Class

End Class