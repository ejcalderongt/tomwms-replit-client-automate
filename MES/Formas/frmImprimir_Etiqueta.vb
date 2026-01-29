Imports System.Drawing.Printing
Imports System.Globalization
Imports System.IO
Imports System.Management
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraEditors
Imports TOMWMS

Public Class frmImprimir_Etiqueta

    Public Id_StandAlone As Boolean
    Public pImpresoraDefault As String = ""

    Private ImpresionIndividual As Boolean
    Private AbrioIni As Boolean = False
    Private NombreFincaSeleccionada As String = ""
    Private IdFincaSeleccionada As String = ""
    Private IdFinca As Integer = 0
    Private pImpresoraSeleccionada As String = ""

    Dim pPack_Date As String = ""
    Dim pDate As Date = Now
    Dim producto_L1 As String = ""
    Dim producto_L2 As String = ""
    Dim producto_L3 As String = ""
    Dim producto_L4 As String = ""
    Dim pLote As String = ""
    Dim pVoicePickLarge As String = ""
    Dim pVoicePickSmall As String = ""

    Dim rutaCarpeta As String = ""
    Dim rutaArchivo As String = ""

    Dim listaImpresorasDefault As New List(Of clsBeQT_Impresora)
    Dim pImpresion As New clsBeImpresion_log

    Dim pSucursalSeleccionada As New clsBeQT_Sucursal
    Dim pProductoSeleccionado As New clsBeQTProducto_Sucursal

    Private Class Finca
        Public Property ID As String
        Public Property Nombre As String
        Public Property descripcion_producto As String
        Public Property Codigo_Producto As String
    End Class
    Private Class codigo_barra
        Public Property separador As String = "(01)"
        Public Property codigo_producto As String = ""
        Public Property separador_2 As String = "(13)"
        Public Property fecha As String = "250210" 'yyMMdd
        Public Property separador_3 As String = "(10)"
        Public Property paleta As String = "00" 'usar siempre 2 digitos
        Public Property lote As String = "000000" 'usar siempre 6 digitos
    End Class

    Dim ListaFinca_Y_Producto As New List(Of Finca) From {
                New Finca With {.ID = "FLZ", .Nombre = "LA ZARCA", .descripcion_producto = "Banana/Cavendish 40/Lbs US #1 Product of Guatemala One Banana Inc. Miami/FL", .Codigo_Producto = "17404003900174"},
                New Finca With {.ID = "FLZV", .Nombre = "LA VIRGEN", .descripcion_producto = "Plantains 50/Lbs US #1 Product of Guatemala One Banana Inc. Guatemala", .Codigo_Producto = "17404003900297"},
                New Finca With {.ID = "FLF", .Nombre = "LA FE I/II", .descripcion_producto = "Banana/Cavendish 40/Lbs US #1 Product of Guatemala One Banana Inc. Miami/FL", .Codigo_Producto = "17404003900174"},
                New Finca With {.ID = "FLV", .Nombre = "LAS VEGAS I/II", .descripcion_producto = "Banana/Cavendish 40/Lbs US #1 Product of Guatemala One Banana Inc. Miami/FL", .Codigo_Producto = "17404003900174"},
                New Finca With {.ID = "FTZ", .Nombre = "TAZMANIA", .descripcion_producto = "Banana/Cavendish 40/Lbs US #1 Product of Guatemala One Banana Inc. Miami/FL", .Codigo_Producto = "17404003900174"}
            }

    Private Sub frmPrint_Label_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            Me.KeyPreview = True ' Permite que el formulario detecte eventos de teclado antes de otros controles

            RibbonControl.Minimized = True
            RibbonControl.ShowExpandCollapseButton = False

            If Id_StandAlone Then
                Llenar_fincas_locales()
            Else
                Llenar_fincas()
            End If

            LlenarLookupImpresoras()
            dtPackDate.EditValue = Now
            cmbFinca.Focus()

            '#GT26032025: validar carpeta y archivo lote.txt para guardar el correlativo.
            rutaCarpeta = "C:\DTS_QuickTag_Lote"
            rutaArchivo = Path.Combine(rutaCarpeta, "lote.txt")

            ' Verificar existencia de carpeta "C:\DTS_QuickTag_Lote"
            If Not Directory.Exists(rutaCarpeta) Then
                Directory.CreateDirectory(rutaCarpeta)
            End If

            ' Verificar existencia de archivo "lote.txt"
            If File.Exists(rutaArchivo) Then
                Dim contenido As String = File.ReadAllText(rutaArchivo).Trim()
                Dim valorLeido As Integer

                If Integer.TryParse(contenido, valorLeido) Then
                    txtLote.Text = valorLeido + 1
                Else
                    txtLote.Text = 0 ' En caso que el contenido no sea numérico válido
                End If

            Else
                ' Crear archivo con valor inicial 0
                File.WriteAllText(rutaArchivo, "0")
                txtLote.Text = 1
            End If

            txtLoteTxt.Text = txtLote.Text

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        End Try
    End Sub

    Dim pFincaSeleccionada As New Finca
    Private Sub cmbFinca_EditValueChanged(sender As Object, e As EventArgs) Handles cmbFinca.EditValueChanged
        Try

            '#GT04032025: utiliza datos hardcodeados!
            If Id_StandAlone Then

                Dim fila As Object = cmbFinca.GetSelectedDataRow

                'GT08022022
                If fila IsNot Nothing Then
                    IdFincaSeleccionada = fila.ID
                    NombreFincaSeleccionada = fila.Nombre
                    pFincaSeleccionada = ListaFinca_Y_Producto.Find(Function(x) x.ID = IdFincaSeleccionada)
                End If

                lblNombreFinca.Text = cmbFinca.EditValue

            Else
                '#GT04032025: utiliza datos de la bd, por eso se maneja fila.item
                Dim fila As Object = cmbFinca.GetSelectedDataRow
                If fila IsNot Nothing Then

                    pSucursalSeleccionada = New clsBeQT_Sucursal()
                    pProductoSeleccionado = New clsBeQTProducto_Sucursal()

                    IdFincaSeleccionada = fila.item("codigo")
                    NombreFincaSeleccionada = fila.item("descripcion")
                    IdFinca = fila.item("IdFinca")

                    pSucursalSeleccionada.IdFinca = IdFinca
                    pProductoSeleccionado.IdFinca = IdFinca

                    clsLnQT_Sucursal.GetSingle(pSucursalSeleccionada)
                    clsLnQT_Producto_Sucursal.GetSingle_By_Finca(pProductoSeleccionado)
                    lblProducto.Text = pProductoSeleccionado.Codigo + " - " + pProductoSeleccionado.Descripcion
                End If

                lblNombreFinca.Text = cmbFinca.EditValue

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Crear_Etiqueta(ByVal ImpresionUnica As Boolean)
        Try

            If Not String.IsNullOrEmpty(pProductoSeleccionado.Descripcion) Then

                Dim pPalletaDesde As Integer = 0
                Dim pPalletaHasta As Integer = 0
                Dim pCajaDesde As Integer = 0
                Dim pCajaHasta As Integer = 0

                If ImpresionUnica Then
                    '#GT25022025: aqui tomamos los rangos de impresion para el rollo o solo para una etiqueta
                    pPalletaDesde = txtPalletDesde.EditValue
                    pPalletaHasta = txtPalletHasta.EditValue
                    pCajaDesde = txtCajaDesde.EditValue
                    pCajaHasta = txtCajaHasta.EditValue
                Else
                    pPalletaDesde = 1
                    pPalletaHasta = 20
                    pCajaDesde = 1
                    pCajaHasta = 48
                End If

                '**************************************************************************************************
                '#GT24022025: aqui se descompone la descripcion del producto y se divide en las lineas requeridas
                ' 1. Hasta el primer dígito
                'Dim matchDigit As Match = Regex.Match(pFincaSeleccionada.descripcion_producto, "^.*?(?=\d)")
                Dim matchDigit As Match = Regex.Match(pProductoSeleccionado.Descripcion, "^.*?(?=\d)")
                producto_L1 = If(matchDigit.Success, matchDigit.Value, "").ToUpper()
                Dim remainingString As String = If(matchDigit.Success, pProductoSeleccionado.Descripcion.Substring(matchDigit.Length).Trim(), pProductoSeleccionado.Descripcion)
                ' 2. Hasta donde termina con #1
                Dim matchHash As Match = Regex.Match(remainingString, ".*?#1")
                producto_L2 = If(matchHash.Success, matchHash.Value, "").ToUpper()
                remainingString = If(matchHash.Success, remainingString.Substring(matchHash.Length).Trim(), remainingString)
                ' 3. Hasta donde aparece la palabra Guatemala
                Dim matchGuatemala As Match = Regex.Match(remainingString, ".*?Guatemala")
                producto_L3 = If(matchGuatemala.Success, matchGuatemala.Value, "")
                remainingString = If(matchGuatemala.Success, remainingString.Substring(matchGuatemala.Length).Trim(), remainingString)
                ' 4. El resto de la cadena después de la palabra Guatemala
                producto_L4 = remainingString

                '**************************************************************************************************
                '#GT24022025: obtener los valores de los inputs
                ' 1 fecha del packing
                Dim cultura As New CultureInfo("es-ES")
                Dim fecha As Date = Convert.ToDateTime(dtPackDate.EditValue)
                Dim mes As String = cultura.DateTimeFormat.GetAbbreviatedMonthName(fecha.Month)
                mes = Char.ToUpper(mes(0)) & mes.Substring(1)
                pPack_Date = mes & " " & fecha.ToString("dd")
                pDate = dtPackDate.DateTime.Date
                pLote = txtLote.EditValue
                pImpresoraSeleccionada = cmbImpresoras.EditValue?.ToString()

                If String.IsNullOrEmpty(pImpresoraSeleccionada) Then
                    MessageBox.Show("Por favor, seleccione una impresora.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If Not Imprimir_Etiqueta(pImpresoraSeleccionada,
                                         pPalletaDesde,
                                         pPalletaHasta,
                                         pCajaDesde,
                                         pCajaHasta) Then
                    MessageBox.Show("Error al enviar el mensaje ZPL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Imprimir_Etiqueta(ByVal printerName As String, ByVal pPalletDesde As Integer,
                                       ByVal pCantidadPaletas As Integer,
                                       ByVal pCajaDesde As Integer,
                                       ByVal pCantidadCajas As Integer) As Boolean
        Try

            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = printerName
            Dim vZPL As String = ""

            '#EJC20250424: aqui se obtiene el lote del archivo de texto
            If Not ImpresionIndividual Then Get_Lote_Archivo()

            For pPallet As Integer = pPalletDesde To pCantidadPaletas

                For pCaja As Integer = pCajaDesde To pCantidadCajas

                    '#GT26022025: convertir el valor numerico de la palleta a string para mostrar en ciertas partes 01,02...
                    Dim valor As String = pPallet
                    Dim tmpPallet As String = ""
                    If IsNumeric(valor) Then
                        tmpPallet = valor.PadLeft(2, "0"c) ' Rellenar con ceros hasta 1 digito para la palleta
                    End If

                    '#GT26022025: convertir el valor numerico de la caja a string para mostrar en ciertas partes 01,02...
                    Dim valor_caja As String = pCaja
                    Dim tmpCaja As String = ""
                    If IsNumeric(valor_caja) Then
                        tmpCaja = valor_caja.PadLeft(2, "0"c) ' Rellenar con ceros hasta 1 digito para la palleta
                    End If

                    Dim pEmblemaBanasa As String = pProductoSeleccionado.Img_Producto

                    Dim barra As New codigo_barra
                    Dim codigo_barra As String = barra.separador + "" +
                                                 pProductoSeleccionado.Codigo +
                                                 "" + barra.separador_2 + "" +
                                                 pDate.ToString("yyMMdd") + "" +
                                                 barra.separador_3 + "" +
                                                 tmpPallet + "" +
                                                 pLote

                    '#GT10032025: barra sin los marcadores ()
                    Dim tmpBarra = codigo_barra.Replace("(", "").Replace(")", "")

                    '****************************AQUI CALCULAMOS VOICE PICK ***********************************************************************
                    '******************************************************************************************************************************
                    Dim EntradaVoicePick As String = String.Format("{0}{1}{2}", pProductoSeleccionado.Codigo, tmpPallet + "" + pLote, pDate.ToString("yyMMdd"))
                    Dim bytes As Byte() = Encoding.ASCII.GetBytes(EntradaVoicePick)
                    Dim crc As UShort = ComputeChecksum(bytes)
                    Dim voiceCode As String = (crc Mod 10000).ToString("D4")
                    pVoicePickLarge = voiceCode.Substring(0, 2)
                    pVoicePickSmall = voiceCode.Substring(2, 2)

                    If String.IsNullOrEmpty(pEmblemaBanasa) Then
                        Throw New Exception("No se ha configurado el logo del producto, revisar")
                    End If

                    If pVoicePickLarge.Length = 0 Or pVoicePickSmall.Length = 0 Then
                        Throw New Exception("No se generó correctamente el voice pick, revisar")
                    End If

                    vZPL = String.Format("^XA" & vbCrLf &
                                        "^FX Código de barras GS1-128 ajustado sin texto debajo" & vbCrLf &
                                        "^BY3,3,92" & vbCrLf &
                                        "^FO40,10" & vbCrLf &
                                        "^BCN,120,N,N,N" & vbCrLf &
                                        "^FD>;{10}^FS" & vbCrLf &
                                        "^FX Texto representativo de AIs con paréntesis (no visible en código)" & vbCrLf &
                                        "^FO40,135" & vbCrLf &
                                        "^A0N,25,22" & vbCrLf &
                                        "^FD{9}^FS" & vbCrLf &
                                        "^FX Segunda sección con destinatario y datos adicionales" & vbCrLf &
                                        "^FO40,175" & vbCrLf &
                                        "^A0N,50,85" & vbCrLf &
                                        "^FD{0}^FS" & vbCrLf &
                                        "^FO40,240" & vbCrLf &
                                        "^A0N,40,65" & vbCrLf &
                                        "^FD{1}^FS" & vbCrLf &
                                        "^FO40,283" & vbCrLf &
                                        "^A0N,30,37" & vbCrLf &
                                        "^FD{2}^FS" & vbCrLf &
                                        "^FO600,230" & vbCrLf &
                                        "^A0N,20,35" & vbCrLf &
                                        "^FDPack Date^FS" & vbCrLf &
                                        "^FO620,270" & vbCrLf &
                                        "^A0N,35,55" & vbCrLf &
                                        "^FD{4}^FS" & vbCrLf &
                                        "^FX Fondo negro para 85" & vbCrLf &
                                        "^FO630,320" & vbCrLf &
                                        "^GB80,80,80^FS" & vbCrLf &
                                        "^FO650,355" & vbCrLf &
                                        "^A0N,40,60" & vbCrLf &
                                        "^FR" & vbCrLf &
                                        "^FD{7}^FS" & vbCrLf &
                                        "^FX Fondo negro para 15" & vbCrLf &
                                        "^FO710,320" & vbCrLf &
                                        "^GB90,80,80^FS" & vbCrLf &
                                        "^FO715,335" & vbCrLf &
                                        "^A0N,70,70" & vbCrLf &
                                        "^FR" & vbCrLf &
                                        "^FD{8}^FS" & vbCrLf &
                                        "^FO40,320" & vbCrLf &
                                        "^A0N,30,30" & vbCrLf &
                                        "^FD{3}^FS" & vbCrLf &
                                        "^FO40,370" & vbCrLf &
                                        "^A0N,25,25" & vbCrLf &
                                        "^FDITEM#{6}^FS" & vbCrLf &
                                        "^FO180,370" & vbCrLf &
                                        "^A0N,25,25" & vbCrLf &
                                        "^FD{5}^FS" & vbCrLf &
                                        "^FX Cuadro para el emblema." & vbCrLf &
                                        "^FD{11}^FS" & vbCrLf &
                                        "^FX Cuadro pack date." & vbCrLf &
                                        "^FO600,250^GB200,60,3^FS" & vbCrLf &
                                        "^XZ",
                                        producto_L1,   ' {0}
                                        producto_L2,   ' {1}
                                        producto_L3,   ' {2}
                                        producto_L4,   ' {3}
                                        pPack_Date,    ' {4}
                                        IdFincaSeleccionada, ' {5}
                                        tmpCaja,       ' {6}
                                        pVoicePickLarge, ' {7}
                                        pVoicePickSmall, ' {8}
                                        codigo_barra,  ' {9} con paréntesis
                                        tmpBarra,      ' {10} sin paréntesis
                                        pEmblemaBanasa ' {11}
                                    )



                    If RawPrinterHelper.PrintUSB(printerName, vZPL) Then

                        '#EJC20250424: aqui se genera el progreso con lote y voicecode (solicitud de Axel.)
                        clsPublic.Actualizar_Progreso(lblPrg, String.Format("Pallet {0} caja {1} lote {2} VC {3}", pPallet, pCaja, pLote, pVoicePickLarge & "/" & pVoicePickSmall))
                        Thread.Sleep(100) 'micropausa para no saturar

                    Else

                        Dim msg As String = String.Format("Error en Impresion: Revise suministro y conexión, Palleta {0} y caja {1}", pPallet, pCaja)
                        clsPublic.Actualizar_Progreso(lblPrg, msg)
                        Return False
                        Exit For

                    End If

                    '#GT28032025: si es reimpresion no se guarda el lote
                    Dim pValorLote = CInt(pLote)
                    If Not ImpresionIndividual Then
                        File.WriteAllText(rutaArchivo, pValorLote)
                    End If

                    '#GT06032025: log de impresiones para estimar consumo de etiquetas y rendimiento del toner
                    pImpresion = New clsBeImpresion_log()
                    pImpresion.IdImpresionLog = clsLnImpresion_log.MaxID() + 1
                    pImpresion.User_agr = 1
                    pImpresion.Fec_agr = Now
                    pImpresion.Impresora = printerName
                    pImpresion.Estado = "Completado"
                    pImpresion.Accion = "Control"
                    pImpresion.IdColaImpresion = 1

                    If ImpresionIndividual Then
                        Dim msg As String = String.Format("Impresion_Parcial lote: " & pValorLote & "  pallet: " & pPallet & " caja: " & pCaja)
                        pImpresion.Descripcion = msg
                    Else
                        pImpresion.Descripcion = "Impresion_Rollo lote: " & pValorLote & " Pallet: " & pPallet & " caja: " & pCaja
                    End If

                    clsLnImpresion_log.Insertar(pImpresion)

                Next

            Next

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                               Text,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error)
        End Try

        Return False

    End Function

    Private Sub btnPrintRollo_Click(sender As Object, e As EventArgs) Handles btnPrintRollo.Click
        Try
            ImpresionIndividual = False

            If Validar_Datos() Then
                Crear_Etiqueta(ImpresionIndividual)
            End If
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btPrintIndividual_Click(sender As Object, e As EventArgs) Handles btPrintIndividual.Click
        Try
            ImpresionIndividual = True
            If Validar_Datos() Then
                Crear_Etiqueta(ImpresionIndividual)
            End If
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function Validar_Datos() As Boolean

        Validar_Datos = False

        Try
            If String.IsNullOrEmpty(cmbFinca.EditValue) Then
                cmbFinca.Focus()
                XtraMessageBox.Show("Debe seleccionar una finca ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            ElseIf ImpresionIndividual AndAlso (String.IsNullOrEmpty(txtLote.EditValue) OrElse Val(txtLote.EditValue) = 0) Then
                txtLote.Focus()
                XtraMessageBox.Show("Debe ingresar el # de lote ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                ' En impresión individual, permitir 0 en pallet/caja: solo validar si vienen vacíos (si querés permitir vacío también, quitá estas líneas)
            ElseIf ImpresionIndividual AndAlso String.IsNullOrEmpty(txtPalletDesde.EditValue) Then
                txtPalletDesde.Focus()
                XtraMessageBox.Show("Debe ingresar el # de Pallet inicial ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            ElseIf ImpresionIndividual AndAlso String.IsNullOrEmpty(txtPalletHasta.EditValue) Then
                txtPalletHasta.Focus()
                XtraMessageBox.Show("Debe ingresar el # de Pallet final ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            ElseIf ImpresionIndividual AndAlso String.IsNullOrEmpty(txtCajaDesde.EditValue) Then
                txtCajaDesde.Focus()
                XtraMessageBox.Show("Debe ingresar el # de caja inicial ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            ElseIf ImpresionIndividual AndAlso String.IsNullOrEmpty(txtCajaHasta.EditValue) Then
                txtCajaHasta.Focus()
                XtraMessageBox.Show("Debe ingresar el # de caja final", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                ' Validar orden SOLO si ambos son > 0 (si alguno es 0, no se compara)
            ElseIf ImpresionIndividual AndAlso Val(txtPalletDesde.EditValue) > 0 AndAlso Val(txtPalletHasta.EditValue) > 0 _
               AndAlso Val(txtPalletDesde.EditValue) > Val(txtPalletHasta.EditValue) Then
                txtPalletDesde.Focus()
                XtraMessageBox.Show("La Pallet inicial debe ser menor que la Pallet final", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            ElseIf ImpresionIndividual AndAlso Val(txtCajaDesde.EditValue) > 0 AndAlso Val(txtCajaHasta.EditValue) > 0 _
               AndAlso Val(txtCajaDesde.EditValue) > Val(txtCajaHasta.EditValue) Then
                txtCajaDesde.Focus()
                XtraMessageBox.Show("La caja inicial debe ser menor que la caja final", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else
                Validar_Datos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub txtLote_EditValueChanged(sender As Object, e As EventArgs) Handles txtLote.EditValueChanged
        Dim txt As TextEdit = CType(sender, TextEdit)
        Dim valor As String = txt.Text.Trim()
        If IsNumeric(valor) Then
            If valor = 0 Then
                txtLote.EditValue = 0
            Else
                txt.Text = valor.PadLeft(6, "0"c) ' Rellenar con ceros hasta 6 dígitos
            End If
        End If
    End Sub

    Private Sub spPalletDesde_EditValueChanged(sender As Object, e As EventArgs) Handles txtPalletDesde.EditValueChanged
        If txtPalletDesde.EditValue < 0 Then
            txtPalletDesde.EditValue = 0
        End If
    End Sub

    Private Sub spPalletHasta_EditValueChanged(sender As Object, e As EventArgs) Handles txtPalletHasta.EditValueChanged
        If txtPalletHasta.EditValue < 0 Then
            txtPalletHasta.EditValue = 0
        End If
    End Sub

    Private Sub spCajaDesde_EditValueChanged(sender As Object, e As EventArgs) Handles txtCajaDesde.EditValueChanged
        If txtCajaDesde.EditValue < 0 Then
            txtCajaDesde.EditValue = 0
        End If
    End Sub

    Private Sub spCajaHasta_EditValueChanged(sender As Object, e As EventArgs) Handles txtCajaHasta.EditValueChanged
        If txtCajaHasta.EditValue < 0 Then
            txtCajaHasta.EditValue = 0
        End If
    End Sub

    Private Sub Llenar_fincas()
        Try

            Dim pFinca As DataTable
            pFinca = clsLnQT_Sucursal.Listar_Activos()

            With cmbFinca.Properties
                .DataSource = pFinca
                .NullText = "Seleccione una finca..."
                .ShowHeader = False
                .ShowFooter = False
                .PopulateColumns()
                .DisplayMember = "codigo"
                .ValueMember = "IdFinca"
                .Columns("IdFinca").Visible = False
                .Columns("predeterminada").Visible = False
            End With

            Dim filaPredeterminada = pFinca.AsEnumerable().FirstOrDefault(Function(row) row.Field(Of Boolean)("predeterminada"))

            If filaPredeterminada IsNot Nothing Then
                cmbFinca.EditValue = filaPredeterminada.Field(Of String)("descripcion")
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Llenar_fincas_locales()
        Try
            cmbFinca.Properties.DataSource = ListaFinca_Y_Producto
            cmbFinca.Properties.DisplayMember = "ID"  ' Campo visible en la lista
            cmbFinca.Properties.ValueMember = "Nombre"        ' Valor interno seleccionado
            cmbFinca.Properties.NullText = "Seleccione una finca..."
            cmbFinca.Properties.PopulateColumns()
            cmbFinca.Properties.Columns("descripcion_producto").Visible = False
            cmbFinca.Properties.Columns("Codigo_Producto").Visible = False

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub frmPrint_Label_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try

            If Control.ModifierKeys = Keys.Control AndAlso e.KeyCode = Keys.I Then
                If XtraMessageBox.Show("¿Abrir archivo de configuración?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim a As New frmEditorIni
                    a.WindowState = FormWindowState.Maximized
                    a.ShowDialog()
                    AbrioIni = True
                End If
            ElseIf Control.ModifierKeys = Keys.Control AndAlso e.KeyCode = Keys.M Then

                Dim menuPrincipal As frmmenu = TryCast(Me.MdiParent, frmmenu)
                If menuPrincipal IsNot Nothing Then
                    menuPrincipal.AlternarBotonesRibbon() ' Llamar método del menú principal
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


    ' Constante para el polinomio usado en el cálculo CRC16.
    Private Const polynomial As UShort = &HA001

    ' Tabla para almacenar los valores precalculados del CRC16.
    Private Shared table() As UShort = Nothing

    Private Shared Sub InitializeTable()
        ReDim table(255)
        Dim value As UShort
        Dim temp As UShort
        Dim v As Integer, t As Integer

        For i As UShort = 0 To 255
            value = 0
            temp = i
            ' Usamos variables de tipo Integer para las operaciones intermedias y evitar desbordamiento.
            v = value
            t = temp

            For j As Byte = 0 To 7
                If ((v Xor t) And 1) <> 0 Then
                    v = (v >> 1) Xor polynomial
                Else
                    v = v >> 1
                End If
                t = t >> 1
            Next

            table(i) = CUShort(v)
        Next
    End Sub


    ''' <summary>
    ''' Computa el checksum CRC16 de un arreglo de bytes.
    ''' </summary>
    ''' <param name="bytes">Arreglo de bytes a procesar.</param>
    ''' <returns>Valor CRC16 calculado.</returns>
    Public Shared Function ComputeChecksum(ByVal bytes As Byte()) As UShort
        If table Is Nothing Then
            InitializeTable()
        End If

        Dim crc As UShort = 0
        Dim idx As Byte, shifted As Integer
        For Each b As Byte In bytes
            ' Se opera con Integer para evitar desbordamientos
            idx = CByte((CInt(crc) Xor b) And &HFF)
            shifted = CInt(crc) >> 8
            crc = CUShort(shifted Xor table(idx))
        Next

        Return crc
    End Function

#Region "tab por Enter"

    Private Sub cmbFinca_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbFinca.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrEmpty(cmbFinca.EditValue) Then
                If cmbFinca.ItemIndex > -1 Then
                    txtLote.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub txtLote_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLote.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtLote.EditValue > "0" Then
                txtPalletDesde.Focus()
            Else
                txtLote.Focus()
            End If
        End If
    End Sub

    Private Sub txtPalletDesde_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPalletDesde.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtPalletDesde.EditValue > 0 Then
                txtPalletHasta.Focus()
            Else
                txtPalletDesde.Focus()
            End If
        End If
    End Sub

    Private Sub txtPalletHasta_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPalletHasta.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtPalletHasta.EditValue > 0 Then
                txtCajaDesde.Focus()
            Else
                txtPalletHasta.Focus()
            End If
        End If
    End Sub

    Private Sub txtCajaDesde_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCajaDesde.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCajaDesde.EditValue > 0 Then
                txtCajaHasta.Focus()
            Else
                txtCajaDesde.Focus()
            End If
        End If
    End Sub


    Private Sub txtCajaHasta_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCajaHasta.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCajaHasta.EditValue > 0 Then
                dtPackDate.Focus()
            Else
                txtCajaHasta.Focus()
            End If
        End If
    End Sub


    Private Sub dtPackDate_KeyDown(sender As Object, e As KeyEventArgs) Handles dtPackDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbImpresoras.Focus()
        End If
    End Sub


    Private Sub cmbImpresoras_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbImpresoras.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrEmpty(cmbImpresoras.EditValue) Then
                If cmbImpresoras.ItemIndex > -1 Then
                    btPrintIndividual.Focus()
                End If
            End If
        End If
    End Sub


    Private Sub txtPalletDesde_MouseClick(sender As Object, e As MouseEventArgs) Handles txtPalletDesde.Click
        txtPalletDesde.SelectAll()
    End Sub

    Private Sub txtPalletHasta_MouseClick(sender As Object, e As MouseEventArgs) Handles txtPalletHasta.Click
        txtPalletHasta.SelectAll()
    End Sub

    Private Sub txtCajaDesde_MouseClick(sender As Object, e As MouseEventArgs) Handles txtCajaDesde.Click
        txtCajaDesde.SelectAll()
    End Sub

    Private Sub txtCajaHasta_MouseClick(sender As Object, e As MouseEventArgs) Handles txtCajaHasta.Click
        txtCajaHasta.SelectAll()
    End Sub

    Private Sub txtLote_MouseClick(sender As Object, e As MouseEventArgs) Handles txtLote.Click
        txtLote.SelectAll()
    End Sub


#End Region

#Region "Configuracion de la impresora USB"

    Private Function EsImpresoraUSB(printerName As String) As Boolean
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Printer")
            For Each printer As ManagementObject In searcher.[Get]()
                Dim name As String = printer("Name")?.ToString()
                Dim portName As String = printer("PortName")?.ToString()

                If Not String.IsNullOrEmpty(name) AndAlso Not String.IsNullOrEmpty(portName) AndAlso
                   name.Equals(printerName, StringComparison.OrdinalIgnoreCase) AndAlso
                   portName.StartsWith("USB", StringComparison.OrdinalIgnoreCase) Then
                    Return True
                End If
            Next
        Catch ex As Exception
            MessageBox.Show($"Error al verificar la impresora: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return False
    End Function

    Private Sub LlenarLookupImpresoras()
        Try
            Dim impresorasUSB As New List(Of String)()

            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Printer")
            For Each printer As ManagementObject In searcher.[Get]()
                Dim name As String = printer("Name")?.ToString()
                Dim portName As String = printer("PortName")?.ToString()

                If Not String.IsNullOrEmpty(name) AndAlso Not String.IsNullOrEmpty(portName) AndAlso
                   portName.StartsWith("USB", StringComparison.OrdinalIgnoreCase) Then
                    impresorasUSB.Add(name)
                End If
            Next

            ' Configurar LookupEdit
            With cmbImpresoras.Properties
                .DataSource = impresorasUSB
                .NullText = "Seleccione una impresora..."
                .ShowHeader = False
                .ShowFooter = False
            End With

            '#GT04032025: aqui validamos si existe una impresora default desde una tabla a futuro...
            'Dim pImpresoraDefault As clsBeQT_Impresora
            'listaImpresorasDefault = clsLnQT_Impresora.GetList_By_Predeterminada()
            'pImpresoraDefault = listaImpresorasDefault.FirstOrDefault(Function(x) x.Predeterminada)

            If impresorasUSB.Contains(pImpresoraDefault) Then
                cmbImpresoras.EditValue = pImpresoraDefault
            End If

        Catch ex As Exception
            MessageBox.Show($"Error al obtener impresoras USB: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



#End Region

    Private Function Get_Lote_Archivo() As String
        Dim rutaCarpeta As String = "C:\DTS_QuickTag_Lote"
        Dim rutaArchivo As String = Path.Combine(rutaCarpeta, "lote.txt")
        Dim nuevoLote As String = "000001"

        Get_Lote_Archivo = "0"

        rutaCarpeta = "C:\DTS_QuickTag_Lote"
        rutaArchivo = Path.Combine(rutaCarpeta, "lote.txt")

        If Directory.Exists(rutaCarpeta) Then

            If File.Exists(rutaArchivo) Then

                Dim contenido As String = File.ReadAllText(rutaArchivo).Trim()
                Dim longitudOriginal As Integer = contenido.Length
                Dim valorNumerico As Integer

                If Integer.TryParse(contenido, valorNumerico) Then
                    valorNumerico += 1
                    nuevoLote = valorNumerico.ToString().PadLeft(longitudOriginal, "0"c)
                End If

            End If

        End If

        txtLoteTxt.Text = nuevoLote
        Return nuevoLote
    End Function

End Class