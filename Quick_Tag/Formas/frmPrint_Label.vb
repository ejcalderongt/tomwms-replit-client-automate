Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.IO
Imports System.Management
Imports System.Reflection
Imports System.Resources
Imports System.Text.RegularExpressions
Imports System.Threading
Imports DevExpress.XtraEditors
Imports TOMWMS

Public Class frmPrint_Label

    Public Id_StandAlone As Boolean

    Private ImpresionUnica As Boolean
    Dim pPack_Date As String = ""
    Dim pDate As Date = Now
    Dim producto_L1 As String = ""
    Dim producto_L2 As String = ""
    Dim producto_L3 As String = ""
    Dim producto_L4 As String = ""
    Dim pLote As String = ""

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

    'Private productoBanana As String = "Banana/Cavendish 40/Lbs US #1 Product of Guatemala One Banana Inc. Miami/FL"
    'Private productoPlantains As String = "Plantains 50/Lbs US #1 Product of Guatemala One Banana Inc. Guatemala"


    Private NombreFincaSeleccionada As String = ""
    Private IdFincaSeleccionada As String = ""
    Private IdFinca As Integer = 0
    Public printerName As String = ""


    Dim ListaFinca_Y_Producto As New List(Of Finca) From {
                New Finca With {.ID = "FLZ", .Nombre = "LA ZARCA", .descripcion_producto = "Banana/Cavendish 40/Lbs US #1 Product of Guatemala One Banana Inc. Miami/FL", .Codigo_Producto = "17404003900174"},
                New Finca With {.ID = "FLZV", .Nombre = "LA VIRGEN", .descripcion_producto = "Plantains 50/Lbs US #1 Product of Guatemala One Banana Inc. Guatemala", .Codigo_Producto = "17404003900297"},
                New Finca With {.ID = "FLF", .Nombre = "LA FE I/II", .descripcion_producto = "Banana/Cavendish 40/Lbs US #1 Product of Guatemala One Banana Inc. Miami/FL", .Codigo_Producto = "17404003900174"},
                New Finca With {.ID = "FLV", .Nombre = "LAS VEGAS I/II", .descripcion_producto = "Banana/Cavendish 40/Lbs US #1 Product of Guatemala One Banana Inc. Miami/FL", .Codigo_Producto = "17404003900174"},
                New Finca With {.ID = "FTZ", .Nombre = "TAZMANIA", .descripcion_producto = "Banana/Cavendish 40/Lbs US #1 Product of Guatemala One Banana Inc. Miami/FL", .Codigo_Producto = "17404003900174"}
            }

    Private Sub frmPrint_Label_Shown(sender As Object, e As EventArgs)
        Try

            If Id_StandAlone Then
                Llenar_fincas_locales()
            Else
                Llenar_fincas()
            End If


            LlenarLookupImpresoras()
            dtPackDate.EditValue = Now
            cmbFinca.Focus()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        End Try
    End Sub

    Dim pFincaSeleccionada As New Finca
    Private Sub cmbFinca_EditValueChanged(sender As Object, e As EventArgs) Handles cmbFinca.EditValueChanged

    End Sub

    Private Sub Crear_Etiqueta(ByVal ImpresionUnica As Boolean)
        Try

            If Not String.IsNullOrEmpty(pFincaSeleccionada.Nombre) Then

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
                Dim matchDigit As Match = Regex.Match(pFincaSeleccionada.descripcion_producto, "^.*?(?=\d)")
                producto_L1 = If(matchDigit.Success, matchDigit.Value, "").ToUpper()
                Dim remainingString As String = If(matchDigit.Success, pFincaSeleccionada.descripcion_producto.Substring(matchDigit.Length).Trim(), pFincaSeleccionada.descripcion_producto)
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
                pPack_Date = Convert.ToDateTime(dtPackDate.EditValue).ToString("MMM dd", New Globalization.CultureInfo("en-US")) 'equivale a {5}
                pDate = dtPackDate.DateTime.Date

                ' 2 # de caja proviene de la iteración de 1 hasta 48 cajas
                'Dim pCaja_No As Integer = 15 'equivale a {7}
                ' 3 código de la finca desde IdFincaSeleccionada {6}
                ' 4 codigo interno walmart corresponde a la iteración de 1 hasta 20 PALLETS {8}
                'Dim pPallet_No As Integer = 85
                ' 5 codigo interno walmar que proviene de la iteración de 1 hasta 48 cajas {9} pCaja_No

                pLote = txtLote.EditValue

                printerName = cmbImpresoras.EditValue?.ToString()

                If String.IsNullOrEmpty(printerName) Then
                    MessageBox.Show("Por favor, seleccione una impresora.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If Not Imprimir_Etiqueta(printerName, pPalletaDesde, pPalletaHasta, pCajaDesde, pCajaHasta) Then
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


                    Dim pEmblemaBanasa As String = "^FO470,230^GFA,1700,1700,10,,::::::K07496ABEBFA4,I0SFC,001OF8301F,007OF810038,007OFC1800C,J03KFA7E03C1E,K0KF07E00E0F,K07JF07FJ03,K03JF27F8I01,010401JF27FEI01,013E00JF33FF8003,01E01C1IF33IF00F,01801F8FFE31LF,01I03CFFC30LF,01K0FFC00LF,01001F9FFI07KF,010E7IFEI07KF,013EJFE8007FFE01,0177KF8107FF8,01LFB0107FF001,01IFEFF20107FF001,01BF801FE0307FF,017FI07C0207FFE07,01FCI0380E07FFE07,01F8I01C1C0IFC01,01F00100C3B9IFC,01F03FC07F7KF,01E07FE07FC0FF81C,01E0607K03C0078,01E0603K038003F,01E0E03K030181F,01E0C038007030381F,01C0C03I0F8103C0F,01C0C03001F8303C0F,01C0C03800F81J0F,01E0C03I0F83J0F,01E0E03001F81I01F,01E0603I0F81I01F,01E0607I0F8103IF,01E030E040F8103IF,01F03FC0C0F8103IF,01F01F80C0F8181F3F,01F8I01C0F818081F,01FCI03C0F81C001F,01FFI07C0F83E003F,01FF801FC1FC3F80FF8,01IF8PFE7,01TFE38,01UF3,01VF8,01FD7E6D7B3IFBIF8,01F926497337FBBCFF8,01VF,:01VF8,::01VF,01VF8,:::::::::::01IFE7LFEJF8,01IFE7LFE7IF8,01IFEJFCIF7IF8,01FF9E73FF878F79FF8,01FE0670E1872660FF8,01FE467CDCCFE644FF8,01FCE67CDCCFE664FF8,01FCE646DCCEI6CFF8,01FCE64E48CE626C7F8,01FE4E48DCEE6366FF8,01FC7SF8,01FETF8,:01FCTF8,01VF,:00VF,00UFE,:007TFC,003TF8,001TF,I07RFC,N06CEE,N07CEE,N0E4CE,N0ICE,N0CCEE,O0IC,O0C6C4,I03JFDC7E18C18,I03KFE08,I07LF8,I03NFA38,K0A054MF8,I03RF8,I07LFDKF8,I03LFDJ528,I03FEJF8015,I037KFD0151,I03MFJ528,I07EQF8,O03LF8,O018,::O01C,O03JF7A8,I03086133LFC,N0237KFC,L09037LF8,I03FF9IFDD77ED8,I03IF57FD75E8F8,L02067F7JF8,O01LF8,O03LF8,O018,O038,J0J987E497708,O01493761,I01KFC7A7FEF,I03KFC7KFC,P03KF8,,L06K01,L040383A52,L0DDBBFBF3,L07FDB2BF7,L022492A8A,I0400624826I204,I0664734E6E66766,I082C95592F979F9,J04FF77B379523F,I0602626E3677226,N02006I2,J0B130E0062,I0199300C0726248,I03180131C73E248,N04002,,:::::::::::::^FS"

                    Dim barra As New codigo_barra
                    Dim codigo_barra As String = barra.separador + "" + pFincaSeleccionada.Codigo_Producto + "" + barra.separador_2 + "" + pDate.ToString("yyMMdd") + "" + barra.separador_3 + "" + tmpPallet + "" + pLote

                    Dim tmpBarra = codigo_barra.Replace("(", "").Replace(")", "")

                    'vZPL = String.Format("^XA
                    '                ^FX Código de barras GS1-128 aún más pequeño para etiqueta de 4 pulgadas
                    '                ^BY1.4,2,60
                    '                ^FO25,10
                    '                ^BCN,120,N,N,N
                    '                ^FD>8{10}^FS
                    '                ^FX Texto debajo del código de barras para mostrar los paréntesis
                    '                ^FO25,135
                    '                ^A0N,25,25
                    '                ^FD{9}^FS
                    '                ^FX Second section with recipient address and permit information.
                    '                ^FO25,175
                    '                ^A0N,50,85
                    '                ^FD{0}^FS
                    '                ^FO25,240
                    '                ^A0N,40,65
                    '                ^FD{1}^FS
                    '                ^FO30,283
                    '                ^A0N,30,37
                    '                ^FD{2}^FS
                    '                ^FO600,230
                    '                ^A0N,20,35
                    '                ^FDPack Date^FS
                    '                ^FO620,270
                    '                ^A0N,25,40
                    '                ^FD{4}^FS
                    '                ^FO660,365
                    '                ^A0N,30,40
                    '                ^FD{7}^FS
                    '                ^FO715,345
                    '                ^A0N,55,60
                    '                ^FD{8}^FS
                    '                ^FO30,320
                    '                ^A0N,30,30
                    '                ^FD{3}^FS
                    '                ^FO30,370
                    '                ^A0N,25,25
                    '                ^FDITEM#{6}^FS
                    '                ^FO180,370
                    '                ^A0N,25,25
                    '                ^FD{5}^FS
                    '                ^FX cuadro para el emblema.
                    '                ^FO480,250^GB80,150,3^FS
                    '                ^FX cuadro pack date.
                    '                ^FO600,250^GB175,60,3^FS
                    '                ^FX cuadro codigo interno.
                    '                ^FO630,320^GB170,80,3^FS
                    '                ^XZ", producto_L1, producto_L2, producto_L3, producto_L4,
                    '                 pPack_Date, IdFincaSeleccionada, tmpCaja, tmpPallet, tmpCaja, codigo_barra, tmpBarra)

                    vZPL = String.Format("^XA
                                    ^FX Código de barras GS1-128 aún más pequeño para etiqueta de 4 pulgadas
                                    ^BY1.4,2,120
                                    ^FO30,10
                                    ^BCN,120,N,N,N
                                    ^FD>8{10}^FS
                                    ^FX Texto debajo del código de barras para mostrar los paréntesis
                                    ^FO25,135
                                    ^A0N,25,22
                                    ^FD{9}^FS
                                    ^FX Second section with recipient address and permit information.
                                    ^FO25,175
                                    ^A0N,50,85
                                    ^FD{0}^FS
                                    ^FO25,240
                                    ^A0N,40,65
                                    ^FD{1}^FS
                                    ^FO30,283
                                    ^A0N,30,37
                                    ^FD{2}^FS
                                    ^FO600,230
                                    ^A0N,20,35
                                    ^FDPack Date^FS
                                    ^FO620,270
                                    ^A0N,35,55
                                    ^FD{4}^FS
                                    ^FX Fondo negro para 85
                                    ^FO630,320
                                    ^GB80,80,80^FS  
                                    ^FO650,355
                                    ^A0N,40,60
                                    ^FR  
                                    ^FD{7}^FS                         
                                    ^FX Fondo negro para 15
                                    ^FO710,320
                                    ^GB90,80,80^FS 
                                    ^FO715,335
                                    ^A0N,70,70
                                    ^FR  
                                    ^FD{8}^FS
                                    ^FO30,320
                                    ^A0N,30,30
                                    ^FD{3}^FS
                                    ^FO30,370
                                    ^A0N,25,25
                                    ^FDITEM#{6}^FS
                                    ^FO180,370
                                    ^A0N,25,25
                                    ^FD{5}^FS
                                    ^FX cuadro para el emblema.
                                    ^FD{11}^FS
                                    ^FX cuadro pack date.
                                    ^FO600,250^GB175,60,3^FS
                                    ^XZ", producto_L1, producto_L2, producto_L3, producto_L4,
                                     pPack_Date, IdFincaSeleccionada, tmpCaja, tmpPallet, tmpCaja, codigo_barra, tmpBarra, pEmblemaBanasa)


                    If RawPrinterHelper.PrintUSB(printerName, vZPL) Then

                        lblPrg.AppendText(vbNewLine)
                        Dim msg As String = String.Format("Impresion Pallet {0} y caja {1}", pPallet, pCaja)
                        lblPrg.AppendText(msg)
                        lblPrg.AppendText(vbNewLine)
                        lblPrg.Refresh()
                        lblPrg.SelectionStart = lblPrg.TextLength
                        lblPrg.ScrollToCaret()
                        Thread.Sleep(100) 'micropausa para no saturar
                    Else

                        lblPrg.AppendText(vbNewLine)
                        Dim msg As String = String.Format("Error en Impresion: Revise suministro y conexión, Palleta {0} y caja {1}", pPallet, pCaja)
                        lblPrg.AppendText(msg)
                        lblPrg.AppendText(vbNewLine)
                        lblPrg.Refresh()
                        lblPrg.SelectionStart = lblPrg.TextLength
                        lblPrg.ScrollToCaret()
                        Return False
                        Exit For

                    End If

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

    End Sub

    Private Sub btPrintIndividual_Click(sender As Object, e As EventArgs) Handles btPrintIndividual.Click

    End Sub

    Private Function Validar_Datos() As Boolean

        Validar_Datos = False

        Try
            If String.IsNullOrEmpty(cmbFinca.EditValue) Then
                cmbFinca.Focus()
                XtraMessageBox.Show("Debe seleccionar una finca ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtLote.EditValue) OrElse txtLote.EditValue = 0 Then
                XtraMessageBox.Show("Debe ingresar el # de lote ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtPalletDesde.EditValue) OrElse txtPalletDesde.EditValue = 0 Then
                txtPalletDesde.Focus()
                XtraMessageBox.Show("Debe ingresar el # de Pallet inicial ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtPalletHasta.EditValue) OrElse txtPalletHasta.EditValue = 0 Then
                txtPalletHasta.Focus()
                XtraMessageBox.Show("Debe ingresar el # de Pallet final ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtCajaDesde.EditValue) OrElse txtCajaDesde.EditValue = 0 Then
                txtCajaDesde.Focus()
                XtraMessageBox.Show("Debe ingresar el # de caja inicial ", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtCajaHasta.EditValue) OrElse txtCajaHasta.EditValue = 0 Then
                txtCajaHasta.Focus()
                XtraMessageBox.Show("Debe ingresar el # de caja final", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf txtPalletDesde.EditValue > txtPalletHasta.EditValue Then
                txtPalletDesde.Focus()
                XtraMessageBox.Show("La Pallet inicial debe ser menor que la Pallet final", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf txtCajaDesde.EditValue > txtCajaHasta.EditValue Then
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
            pFinca = clsLnFinca.Listar()

            With cmbFinca.Properties
                .DataSource = pFinca
                .NullText = "Seleccione una finca..."
                .ShowHeader = False
                .ShowFooter = False
                .PopulateColumns()
                .DisplayMember = "codigo"
                .ValueMember = "descripcion"
                .Columns("IdFinca").Visible = False
            End With

        Catch ex As Exception

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

        End Try
    End Sub

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
        Catch ex As Exception
            MessageBox.Show($"Error al obtener impresoras USB: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region



End Class