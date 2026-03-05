Imports System.Drawing.Printing
Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Threading.Tasks
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

'Zebra SDK v2.x (Zebra.Printer.SDK 2.15.x)
Imports Zebra.Sdk.Comm
Imports Zebra.Sdk.Printer.Discovery

Public Class frmImpresion_OC_RFID

    Private listBarrasPallet As New List(Of clsBeI_nav_barras_pallet)
    Private BeEmpresa As New clsBeEmpresa

    Private Sub frmImpresionRFID_OC_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        BeEmpresa = New clsBeEmpresa
        Try

            BeEmpresa = AP.Empresa
            Cargar_Impresora_Zebra()
            cargar_barras_pallet()


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


    Private Sub Cargar_Impresora_Zebra()
        Try
            Dim dt As New DataTable()
            dt.Columns.Add("Nombre", GetType(String))
            dt.Columns.Add("DriverName", GetType(String))
            dt.Columns.Add("PortName", GetType(String))
            dt.Columns.Add("IsDefault", GetType(Boolean))

            Using searcher As New ManagementObjectSearcher("SELECT Name, DriverName, PortName, Default, WorkOffline FROM Win32_Printer")
                Using results = searcher.Get()
                    For Each mo As ManagementObject In results

                        Dim name As String = If(TryCast(mo("Name"), String), "")
                        Dim driverName As String = If(TryCast(mo("DriverName"), String), "")
                        Dim portName As String = If(TryCast(mo("PortName"), String), "")
                        Dim isDefault As Boolean = False

                        Try
                            isDefault = If(mo("Default") IsNot Nothing, Convert.ToBoolean(mo("Default")), False)
                        Catch
                            isDefault = False
                        End Try

                        ' Filtro Zebra (cola/driver)
                        Dim isZebra As Boolean =
                        driverName.IndexOf("zebra", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                        driverName.IndexOf("zdesigner", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                        name.IndexOf("zebra", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                        name.IndexOf("zt", StringComparison.OrdinalIgnoreCase) >= 0 ' ej: ZT411

                        If isZebra Then
                            dt.Rows.Add(name, driverName, portName, isDefault)
                        End If
                    Next
                End Using
            End Using

            cmbImpresora.Properties.DataSource = dt
            cmbImpresora.Properties.DisplayMember = "Nombre"
            cmbImpresora.Properties.ValueMember = "Nombre"
            cmbImpresora.Properties.PopulateColumns()

            ' Ocultar columnas auxiliares si no las quieres visibles en el dropdown
            If cmbImpresora.Properties.Columns("DriverName") IsNot Nothing Then cmbImpresora.Properties.Columns("DriverName").Visible = False
            If cmbImpresora.Properties.Columns("PortName") IsNot Nothing Then cmbImpresora.Properties.Columns("PortName").Visible = False
            If cmbImpresora.Properties.Columns("IsDefault") IsNot Nothing Then cmbImpresora.Properties.Columns("IsDefault").Visible = False

            ' Selección inicial: si hay una Zebra default, úsala; si no, la primera Zebra.
            Dim defaultRow = dt.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of Boolean)("IsDefault"))
            If defaultRow IsNot Nothing Then
                cmbImpresora.EditValue = defaultRow.Field(Of String)("Nombre")
            ElseIf dt.Rows.Count > 0 Then
                cmbImpresora.EditValue = dt.Rows(0)("Nombre").ToString()
            Else
                cmbImpresora.EditValue = Nothing
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub


    Private Sub cargar_barras_pallet()
        Try

            listBarrasPallet = New List(Of clsBeI_nav_barras_pallet)
            listBarrasPallet = clsLnI_nav_barras_pallet.GetAll()

            If listBarrasPallet IsNot Nothing AndAlso listBarrasPallet.Count > 0 Then

                grdListaBarraPallets.DataSource = listBarrasPallet

                If GridView1.Columns.Count = 0 Then
                    GridView1.PopulateColumns()
                End If


                If GridView1.Columns.ColumnByFieldName("IdPallet") IsNot Nothing Then GridView1.Columns("IdPallet").Visible = False
                If GridView1.Columns.ColumnByFieldName("Codigo") IsNot Nothing Then GridView1.Columns("Codigo").Visible = True
                If GridView1.Columns.ColumnByFieldName("Nombre") IsNot Nothing Then GridView1.Columns("Nombre").Visible = True
                If GridView1.Columns.ColumnByFieldName("Camas_Por_Tarima") IsNot Nothing Then GridView1.Columns("Camas_Por_Tarima").Visible = True
                If GridView1.Columns.ColumnByFieldName("Cajas_Por_Cama") IsNot Nothing Then GridView1.Columns("Cajas_Por_Cama").Visible = True
                If GridView1.Columns.ColumnByFieldName("Cantidad_Presentacion") IsNot Nothing Then GridView1.Columns("Cantidad_Presentacion").Visible = True
                If GridView1.Columns.ColumnByFieldName("UM_Producto") IsNot Nothing Then GridView1.Columns("UM_Producto").Visible = True
                If GridView1.Columns.ColumnByFieldName("Lote") IsNot Nothing Then GridView1.Columns("Lote").Visible = True
                If GridView1.Columns.ColumnByFieldName("Fecha_Agregado") IsNot Nothing Then GridView1.Columns("Fecha_Agregado").Visible = False
                If GridView1.Columns.ColumnByFieldName("Fecha_Ingreso") IsNot Nothing Then GridView1.Columns("Fecha_Ingreso").Visible = False
                If GridView1.Columns.ColumnByFieldName("Fecha_Vence") IsNot Nothing Then GridView1.Columns("Fecha_Vence").Visible = True
                If GridView1.Columns.ColumnByFieldName("Fecha_Produccion") IsNot Nothing Then GridView1.Columns("Fecha_Produccion").Visible = False
                If GridView1.Columns.ColumnByFieldName("Activo") IsNot Nothing Then GridView1.Columns("Activo").Visible = False
                If GridView1.Columns.ColumnByFieldName("Recibido") IsNot Nothing Then GridView1.Columns("Recibido").Visible = False
                If GridView1.Columns.ColumnByFieldName("IdRecepcion") IsNot Nothing Then GridView1.Columns("IdRecepcion").Visible = False
                If GridView1.Columns.ColumnByFieldName("Bodega_Origen") IsNot Nothing Then GridView1.Columns("Bodega_Origen").Visible = False
                If GridView1.Columns.ColumnByFieldName("Bodega_Destino") IsNot Nothing Then GridView1.Columns("Bodega_Destino").Visible = True
                If GridView1.Columns.ColumnByFieldName("Codigo_Barra") IsNot Nothing Then GridView1.Columns("Codigo_Barra").Visible = True
                If GridView1.Columns.ColumnByFieldName("Cantidad_UMP") IsNot Nothing Then GridView1.Columns("Cantidad_UMP").Visible = False
                If GridView1.Columns.ColumnByFieldName("Lote_Numerico") IsNot Nothing Then GridView1.Columns("Lote_Numerico").Visible = False
                If GridView1.Columns.ColumnByFieldName("fecha_procesado_erp") IsNot Nothing Then GridView1.Columns("fecha_procesado_erp").Visible = False

                GridView1.OptionsView.ColumnAutoWidth = False
                GridView1.OptionsView.ShowFooter = True

            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Public Shared ReadOnly Lis_Licencias As New List(Of String) From {
      "LP0000000001",
      "LP0000000002",
      "LP0000000003",
      "LP0000000004",
      "LP0000000005",
      "LP0000000006",
      "LP0000000007",
      "LP0000000008",
      "LP0000000009",
      "LP0000000010"
    }


    ' Guarda los códigos ya impresos (para colorear filas)
    Private ReadOnly _codigosImpresos As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle
        If e.RowHandle < 0 Then Return

        Dim row = TryCast(GridView1.GetRow(e.RowHandle), Object)
        If row Is Nothing Then Return

        ' Asumiendo que el objeto tipificado expone Codigo_barra
        Dim codigo As String = TryCast(CallByName(row, "Codigo_barra", CallType.Get), String)
        If String.IsNullOrWhiteSpace(codigo) Then Return

        If _codigosImpresos.Contains(codigo) Then
            e.Appearance.BackColor = Color.LightGreen
            e.HighPriority = True
        End If
    End Sub



    Private Sub cmdImpresion_Click(sender As Object, e As EventArgs) Handles cmdImpresion.Click
        Try
            cmdImpresion.Enabled = False

            ' === Valores fijos (misma impresión física) ===
            'Dim licencia As String = "TEA4406620022563"
            'Dim empresa As String = "TEXCONSA"
            'Dim bodega As String = "CENTRAL"
            'Dim vigencia As String = "01-01-2028"
            'Dim presentacion As String = "PAR"
            'Dim cantidad As String = "2"
            'Dim fecha As String = "23-02-2026"
            'Dim producto As String = "COBERTOR"

            ' Impresora USB (cola instalada en Windows)
            Dim ps As New PrinterSettings()
            Dim printerName As String = Convert.ToString(cmbImpresora.EditValue)

            Dim st = GetPrinterStatus(printerName)
            If Not st.found Then
                XtraMessageBox.Show("Impresora no encontrada: " & printerName, "Impresión RFID", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            If Not st.ok Then
                XtraMessageBox.Show("Impresora no lista: " & printerName & vbCrLf & st.message, "Impresión RFID", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            Dim maxToProcess As Integer = CInt(txtCantidadImpresiones.Value)

            '#iteración real para impresión
            For i As Integer = 0 To listBarrasPallet.Count - 1
                Dim barraPallet = listBarrasPallet(i)

                Dim BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(barraPallet.Codigo)
                Dim Bepresentacion = clsLnProducto_presentacion.Get_Presentacion_Defecto_By_IdProducto(BeProducto.IdProducto)
                Dim BeBodega = clsLnBodega.GetSingle_By_Idbodega(barraPallet.Bodega_Origen)

                '074061710038175183
                'Dim epc96Hex As String = BuildEpc96_FromCodigoBarra(barraPallet.Codigo_barra)

                Dim epc96Hex As String = EncodeSsccToEpc96_WithPrefix7406171(barraPallet.Codigo_barra, filterValue:=0)
                Dim FechaActual As String = Date.Now.ToString("dd-MM-yyyy")
                Dim FechaVence As String = Convert.ToDateTime(barraPallet.Fecha_Vence).ToString("dd-MM-yyyy")

                Dim zpl As String = BuildZpl_RfidEncode_And_Print(epc96Hex, barraPallet.Codigo_barra, BeEmpresa.Nombre, BeBodega.Nombre, FechaVence, Bepresentacion.Nombre, barraPallet.Cantidad_Presentacion, FechaActual)

                For j As Integer = 0 To maxToProcess - 1
                    RawPrinterHelper.SendStringToPrinter(printerName, zpl)
                Next

                ' Marcar como impreso y refrescar UI
                _codigosImpresos.Add(barraPallet.Codigo_barra)

                Dim rowHandle As Integer = GridView1.GetRowHandle(i)
                If rowHandle >= 0 Then
                    GridView1.RefreshRow(rowHandle)
                Else
                    GridView1.RefreshData()
                End If
            Next

            ' Tiraje 3 etiquetas: EPC 96-bit completo, incluye 199004 y consecutivo al final
            'For i As Integer = 1 To 3
            '    Dim epc96Hex As String = BuildEpc96_199004_Consecutivo16(i) ' 24 hex (96 bits)
            '    Dim zpl As String = BuildZpl_RfidEncode_And_Print(epc96Hex, licencia, empresa, bodega, vigencia, presentacion, cantidad, fecha)
            '    RawPrinterHelper.SendStringToPrinter(printerName, zpl)
            'Next


            ' === NUEVO: DEMO con Lis_Licencias (NO ALTERA LOS CICLOS EXISTENTES) ===
            'For Each lic In Lis_Licencias

            '    Dim data As String = If(lic, "").Trim()

            '    If data.Length <> 12 Then
            '        Throw New Exception("Lis_Licencias debe contener valores de 12 caracteres para EPC literal. Valor: " & data)
            '    End If

            '    ' EPC literal 12 chars => 12 bytes => 96 bits => 24 hex
            '    Dim epcBytes As Byte() = Encoding.ASCII.GetBytes(data)
            '    Dim epc96Hex As String = BitConverter.ToString(epcBytes).Replace("-", "").ToUpperInvariant()

            '    ' ZPL FÍSICO en 300 dpi (layout basado en tu plantilla 203, escalado ~1.48)
            '    ' Mapeo:
            '    ' {0}=bodega, {1}=empresa, {2}=producto, {3}=data (DataMatrix), {4}=fecha,
            '    ' {5}=data (L:), {6}=vigencia, {7}=presentacion, {8}=cantidad
            '    Dim zplFisico300 As String =
            '                            "^MMT^PW1200^LL0600^LS0" &
            '                            "^FT665,31^A0I,30,21^FH^FD{4}^FS" &
            '                            "^FO3,59^GB1200,7,7^FS" &
            '                            "^FT444,90^A0I,44,36^FH^FD{0}^FS" &
            '                            "^FT532,90^A0I,44,36^FH^FDBod:^FS" &
            '                            "^FT930,90^A0I,44,36^FH^FD{1}^FS" &
            '                            "^FT1034,90^A0I,44,36^FH^FDEmp:^FS" &
            '                            "^FT1034,177^BXI,18,200^FH^FD{3}^FS" &
            '                            "^FT1034,452^A0I,44,36^FH^FD{2}^FS" &
            '                            "^FT429,318^A0I,74,65^FH^FDV:{6}^FS" &
            '                            "^FT429,392^A0I,74,58^FH^FDL:{5}^FS" &
            '                            "^FT429,244^A0I,74,58^FH^FDPRES:{7}^FS" &
            '                            "^FT429,177^A0I,74,58^FH^FDCANT:{8}^FS" &
            '                            "^FO3,503^GB1200,21,21^FS" &
            '                            "^FT1034,543^A0I,37,36^FH^FDTOMWMS Licencia.^FS" &
            '                            "^PQ1,0,1,Y"

            '    Dim zplFisicoFinal As String = String.Format(
            '                                zplFisico300,
            '                                bodega,       ' {0}
            '                                empresa,      ' {1}
            '                                producto,     ' {2}
            '                                data,         ' {3}
            '                                fecha,        ' {4}
            '                                data,         ' {5}
            '                                vigencia,     ' {6}
            '                                presentacion, ' {7}
            '                                cantidad      ' {8}
            '                    )

            '    ' Unificar RFID + físico en un solo job (300 dpi)
            '    Dim zplFinal As String =
            '                        "^XA" &
            '                        "^CI27" &
            '                        "^RS4" &
            '                        "^RFW,H^FD" & epc96Hex & "^FS" &
            '                        zplFisicoFinal &
            '                        "^XZ"

            '    RawPrinterHelper.SendStringToPrinter(printerName, zplFinal)
            'Next


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Impresión RFID", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw
        Finally
            cmdImpresion.Enabled = True
        End Try
    End Sub


    ' Conversión estándar: EPC96 = primeros 12 bytes de SHA1(codigo_barra como NVARCHAR/UTF-16LE)
    ' Equivalente a SQL Server: SUBSTRING(HASHBYTES('SHA1', @data), 1, 12) donde @data es NVARCHAR
    Private Function BuildEpc96_FromCodigoBarra(codigo_barra As String) As String
        Try
            If String.IsNullOrWhiteSpace(codigo_barra) Then
                Throw New Exception("Codigo_barra vacío.")
            End If

            Dim data As String = codigo_barra.Trim()

            ' 1) Bytes NVARCHAR (UTF-16LE en SQL Server) => Encoding.Unicode en .NET
            Dim dataBytes As Byte() = Encoding.Unicode.GetBytes(data)

            ' 2) SHA1 completo (20 bytes)
            Dim sha1Full As Byte()
            Using sha As System.Security.Cryptography.SHA1 = System.Security.Cryptography.SHA1.Create()
                sha1Full = sha.ComputeHash(dataBytes)
            End Using

            ' 3) Primeros 12 bytes => 96 bits
            Dim epc96Bytes(11) As Byte
            Array.Copy(sha1Full, 0, epc96Bytes, 0, 12)

            ' 4) 24 hex (96 bits)
            Dim epc96Hex As String = BitConverter.ToString(epc96Bytes).Replace("-", "").ToUpperInvariant()

            If epc96Hex.Length <> 24 Then
                Throw New Exception("EPC inválido: debe ser 24 hex (96 bits). EPC=" & epc96Hex)
            End If

            Return epc96Hex

        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function StringToHexWith96BitFlag(ByVal input As String) As (Hex As String, Is96Bits As Boolean)
        If input Is Nothing Then Return (String.Empty, False)

        Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(input)

        Dim sb As New System.Text.StringBuilder(bytes.Length * 2)
        For Each b As Byte In bytes
            sb.Append(b.ToString("X2"))
        Next

        Dim hex As String = sb.ToString()

        ' 96 bits = 12 bytes = 24 chars HEX
        Dim is96 As Boolean = (bytes.Length = 12) AndAlso (hex.Length = 24)

        Return (hex, is96)
    End Function


    Private Function BuildEpc96_199004_Consecutivo16(consecutivo As Integer) As String
        If consecutivo < 0 OrElse consecutivo > &HFFFF Then
            Throw New ArgumentOutOfRangeException(NameOf(consecutivo), "Consecutivo fuera de rango para 16 bits.")
        End If

        Dim prefijo As String = "03098C" ' 199004 decimal
        Dim relleno As String = New String("0"c, 14) ' 56 bits = 14 hex
        Dim ultimos16 As String = consecutivo.ToString("X4") ' 16 bits

        Dim epc As String = (prefijo & relleno & ultimos16).ToUpperInvariant()

        If epc.Length <> 24 Then
            Throw New Exception("EPC inválido: debe ser 24 hex (96 bits). EPC=" & epc)
        End If

        Return epc
    End Function

    Private Function BuildZpl_RfidEncode_And_Print(epc96Hex As String,
                                              licencia As String,
                                              empresa As String,
                                              bodega As String,
                                              vigencia As String,
                                              presentacion As String,
                                              cantidad As String,
                                              fecha As String) As String

        Dim sb As New StringBuilder()

        sb.AppendLine("^XA")
        sb.AppendLine("^CI27")
        sb.AppendLine("^PW1200")
        sb.AppendLine("^LL600")
        sb.AppendLine("^LH0,0")
        sb.AppendLine("^PR6")

        ' === RFID: escribir EPC completo (96 bits) ===
        sb.AppendLine("^RS4")
        sb.AppendLine("^RFW,H^FD" & epc96Hex & "^FS")

        ' === Impresión física (escalada 203 -> 300 dpi; factor aprox 1.48) ===
        sb.AppendLine("^FO30,22^A0N,44,44^FDTOMWMS Licencia.^FS")
        sb.AppendLine("^FO30,74^GB1140,4,4^FS")
        sb.AppendLine("^FO30,103^A0N,62,62^FD" & licencia & "^FS")
        sb.AppendLine("^FO52,185^BXN,12,200^FD" & licencia & "^FS")

        sb.AppendLine("^FO620,140^A0N,62,62^FDL:" & licencia & "^FS")
        sb.AppendLine("^FO620,214^A0N,62,62^FDV:" & vigencia & "^FS")
        sb.AppendLine("^FO620,303^A0N,62,62^FDPRES:" & presentacion & "^FS")
        sb.AppendLine("^FO620,369^A0N,62,62^FDCANT:" & cantidad & "^FS")

        sb.AppendLine("^FO44,480^A0N,41,41^FDEmp:    " & empresa & "^FS")
        sb.AppendLine("^FO635,480^A0N,41,41^FDBod:  " & bodega & "^FS")
        sb.AppendLine("^FO30,524^GB1140,4,4^FS")
        sb.AppendLine("^FO487,543^A0N,33,33^FD" & fecha & "^FS")

        sb.AppendLine("^PQ1,0,1,N")
        sb.AppendLine("^XZ")

        Return sb.ToString()
    End Function


    '#VERSION PARA 203 DPI
    'Private Function BuildZpl_RfidEncode_And_Print(epc96Hex As String,
    '                                              licencia As String,
    '                                              empresa As String,
    '                                              bodega As String,
    '                                              vigencia As String,
    '                                              presentacion As String,
    '                                              cantidad As String,
    '                                              fecha As String) As String

    '    Dim sb As New StringBuilder()

    '    sb.AppendLine("^XA")
    '    sb.AppendLine("^CI27")
    '    sb.AppendLine("^PW812")
    '    sb.AppendLine("^LL406")
    '    sb.AppendLine("^LH0,0")
    '    sb.AppendLine("^PR6")

    '    ' === RFID: escribir EPC completo (96 bits) ===
    '    ' ^RS4 => longitud EPC 96 bits
    '    ' ^RFW,H => Write RFID (hex data)
    '    ' Nota: NO se lee EPC; solo se escribe el valor completo que enviamos.
    '    sb.AppendLine("^RS4")
    '    sb.AppendLine("^RFW,H^FD" & epc96Hex & "^FS")

    '    ' === Impresión física (igual a la referencia) ===
    '    sb.AppendLine("^FO20,15^A0N,30,30^FDTOMWMS Licencia.^FS")
    '    sb.AppendLine("^FO20,50^GB772,3,3^FS")
    '    sb.AppendLine("^FO20,70^A0N,42,42^FD" & licencia & "^FS")
    '    sb.AppendLine("^FO35,125^BXN,8,200^FD" & licencia & "^FS")

    '    sb.AppendLine("^FO420,95^A0N,42,42^FDL:" & licencia & "^FS")
    '    sb.AppendLine("^FO420,145^A0N,42,42^FDV:" & vigencia & "^FS")
    '    sb.AppendLine("^FO420,205^A0N,42,42^FDPRES:" & presentacion & "^FS")
    '    sb.AppendLine("^FO420,250^A0N,42,42^FDCANT:" & cantidad & "^FS")

    '    sb.AppendLine("^FO30,325^A0N,28,28^FDEmp:    " & empresa & "^FS")
    '    sb.AppendLine("^FO430,325^A0N,28,28^FDBod:  " & bodega & "^FS")
    '    sb.AppendLine("^FO20,355^GB772,3,3^FS")
    '    sb.AppendLine("^FO330,368^A0N,22,22^FD" & fecha & "^FS")

    '    ' 1 etiqueta por job (el loop manda 3 jobs)
    '    sb.AppendLine("^PQ1,0,1,N")
    '    sb.AppendLine("^XZ")

    '    Return sb.ToString()
    'End Function

    Private Function BuildZpl_Tiraje3(licencia As String,
                                      empresa As String,
                                      bodega As String,
                                      vigencia As String,
                                      presentacion As String,
                                      cantidad As String,
                                      fecha As String) As String

        ' Referencia: etiqueta 4x2 aprox, 203 dpi (ancho 812 dots, alto 406 dots)
        ' SOLO impresión física: NO incluye comandos RFID (^RF / ^RFW / ^H* etc.)
        Dim sb As New StringBuilder()

        sb.AppendLine("^XA")
        sb.AppendLine("^CI27") ' ASCII/Latin básico
        sb.AppendLine("^PW812")
        sb.AppendLine("^LL406")
        sb.AppendLine("^LH0,0")
        sb.AppendLine("^PR6")  ' velocidad moderada (ajustable)

        ' Título superior
        sb.AppendLine("^FO20,15^A0N,30,30^FDTOMWMS Licencia.^FS")

        ' Línea superior
        sb.AppendLine("^FO20,50^GB772,3,3^FS")

        ' Licencia grande izquierda
        sb.AppendLine("^FO20,70^A0N,42,42^FD" & licencia & "^FS")

        ' DataMatrix (izquierda)
        sb.AppendLine("^FO35,125^BXN,8,200^FD" & licencia & "^FS")

        ' Bloque derecho (L / V / PRES / CANT)
        sb.AppendLine("^FO420,95^A0N,42,42^FDL:" & licencia & "^FS")
        sb.AppendLine("^FO420,145^A0N,42,42^FDV:" & vigencia & "^FS")
        sb.AppendLine("^FO420,205^A0N,42,42^FDPRES:" & presentacion & "^FS")
        sb.AppendLine("^FO420,250^A0N,42,42^FDCANT:" & cantidad & "^FS")

        ' Pie: Emp / Bod
        sb.AppendLine("^FO30,325^A0N,28,28^FDEmp:    " & empresa & "^FS")
        sb.AppendLine("^FO430,325^A0N,28,28^FDBod:  " & bodega & "^FS")

        ' Línea inferior y fecha centrada
        sb.AppendLine("^FO20,355^GB772,3,3^FS")
        sb.AppendLine("^FO330,368^A0N,22,22^FD" & fecha & "^FS")

        ' Tiraje 3 etiquetas
        sb.AppendLine("^PQ3,0,1,N")
        sb.AppendLine("^XZ")

        Return sb.ToString()
    End Function

    ' ========= VALIDACIÓN DE ESTADO (WMI) =========
    Private Function GetPrinterStatus(printerName As String) As (found As Boolean, ok As Boolean, message As String)
        Try
            Dim target As String = If(printerName, "").Trim()
            If target = "" Then Return (False, False, "Nombre de impresora vacío.")

            ' 1) Validación REAL: si WinSpool puede abrir la cola, existe (aunque WMI falle)
            If Not RawPrinterHelper.CanOpenPrinter(target) Then
                Return (False, False, "No se pudo abrir la cola (OpenPrinter). ¿apagada/desconectada o sin driver?")
            End If

            ' 2) Intento WMI solo para detectar estados (offline/paused/paperout). Si WMI no la encuentra, no bloqueamos.
            Try
                Using searcher As New ManagementObjectSearcher(
                "SELECT Name, WorkOffline, Offline, Paused, ErrorState, PaperOut, OutputBinFull, DoorOpen FROM Win32_Printer")
                    Using results = searcher.Get()
                        For Each mo As ManagementObject In results
                            Dim name As String = If(TryCast(mo("Name"), String), "").Trim()
                            If String.Equals(name, target, StringComparison.OrdinalIgnoreCase) Then

                                Dim issues As New List(Of String)()
                                If SafeBool(mo("WorkOffline")) OrElse SafeBool(mo("Offline")) Then issues.Add("Offline/No disponible")
                                If SafeBool(mo("Paused")) Then issues.Add("Pausada")
                                If SafeBool(mo("PaperOut")) Then issues.Add("Sin papel")
                                If SafeBool(mo("DoorOpen")) Then issues.Add("Puerta abierta")
                                If SafeBool(mo("OutputBinFull")) Then issues.Add("Bandeja llena")
                                If SafeBool(mo("ErrorState")) Then issues.Add("Error de impresora")

                                If issues.Count = 0 Then Return (True, True, "OK")
                                Return (True, False, String.Join(", ", issues))
                            End If
                        Next
                    End Using
                End Using
            Catch
                ' WMI falló: no bloqueamos
            End Try

            ' Si WMI no la listó, pero OpenPrinter sí abrió, dejamos pasar
            Return (True, True, "OK (sin WMI)")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function SafeBool(value As Object) As Boolean
        If value Is Nothing Then Return False
        Try
            Return Convert.ToBoolean(value)
        Catch
            Return False
        End Try
    End Function

    Private Function SafeInt(value As Object) As Integer
        If value Is Nothing Then Return 0
        Try
            Return Convert.ToInt32(value)
        Catch
            Return 0
        End Try
    End Function

    ' ========= RAW printer helper (envía ZPL directo a la cola USB) =========
    Public NotInheritable Class RawPrinterHelper
        Private Sub New()
        End Sub


        Public Shared Function CanOpenPrinter(printerName As String) As Boolean
            Dim hPrinter As IntPtr = IntPtr.Zero
            Try
                Return OpenPrinter(printerName, hPrinter, IntPtr.Zero)
            Finally
                If hPrinter <> IntPtr.Zero Then
                    ClosePrinter(hPrinter)
                End If
            End Try
        End Function

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
        Private Shared Function WritePrinter(hPrinter As IntPtr, pBytes As IntPtr, dwCount As Integer, ByRef dwWritten As Integer) As Boolean
        End Function

        Public Shared Sub SendStringToPrinter(printerName As String, zpl As String)
            Dim hPrinter As IntPtr = IntPtr.Zero
            Dim di As New DOCINFOA() With {
                .pDocName = "ZPL RAW",
                .pDataType = "RAW"
            }

            If Not OpenPrinter(printerName, hPrinter, IntPtr.Zero) Then
                Throw New System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "OpenPrinter falló.")
            End If

            Try
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
                        Marshal.Copy(bytes, 0, pUnmanaged, bytes.Length)

                        Try
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
            Finally
                ClosePrinter(hPrinter)
            End Try
        End Sub
    End Class


End Class