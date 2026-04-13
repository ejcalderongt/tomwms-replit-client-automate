Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports ClosedXML.Excel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraSplashScreen

Public Class frmCargaExcel_DI

    Public Delegate Sub Operar()
    Public Listar As Operar

    Public pNombreMantenimiento As String
    Public pTipoMantenimiento As String
    Public pPropietario As Boolean
    Public pTipoDocumento As clsDataContractDI.tTipoDocumentoIngreso
    Public Property IdMotivoDevolucion As Integer = 0

    Private pListObj As List(Of clsExcel)

    Public InsertaInv As Boolean = False
    Public IdOperador As Integer = 0
    Public NomOperador As String = ""
    Public IdPropietarioBodega As Integer = 0

    Public Property IdOrdenCompraEnc As Integer = 0
    Public Property lDetalle_OC As New DataTable

    Public Property IdBodega As Integer = 0
    Public Property IdUsuario As Integer = 0

    Public gRefBeOrdenCompra As New clsBeTrans_oc_enc()

    Public gScanCodigoBarraPoliza As String = ""
    Public gNombreOperador As String = ""
    Public gScanTicket As Integer = 0

    Private pIdConfiguracion As Integer

    Private errores As Boolean = False

    Private Sub frmCargaExcel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Text = "Carga de Excel - " & pNombreMantenimiento
            '#GT25082022: obtener la bodega para el resto del proceso.
            IdBodega = AP.IdBodega
            pIdConfiguracion = clsLnI_nav_config_enc.Get_IdConfiguracion(IdBodega, AP.IdEmpresa)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ButtonEdit1_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtArchivo.Properties.ButtonClick

        Try

            Dim ObjO As New OpenFileDialog() With {.Filter = "Excel Files(.xlsx)|*.xlsx"}

            If ObjO.ShowDialog() = DialogResult.OK AndAlso ObjO.FileName.Length <> 0 Then

                txtArchivo.Text = ObjO.FileName
                CargaHojas(txtArchivo.Text)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub CargaHojas(ByVal pFileName As String)

        Try

            Dim fileName As String = txtArchivo.Text
            Dim documento As XLWorkbook = New XLWorkbook(fileName)

            pListObj = New List(Of clsExcel)

            DsExcel.Data.Clear()

            Dim i As Integer = -1
            For Each sheet As IXLWorksheet In documento.Worksheets
                i += 1
                Dim ObjE As New clsExcel() With {.Index = i, .Checked = False, .NombreHoja = sheet.Name}
                pListObj.Add(ObjE)
            Next

            If pListObj.Count > 0 Then
                For Each Obj As clsExcel In pListObj
                    Dim lRow As DataRow = DsExcel.Data.NewRow
                    lRow.Item("Hoja") = Obj.NombreHoja
                    lRow.Item("Seleccionar") = False
                    DsExcel.Data.AddDataRow(lRow)
                Next
            End If

            Grid.EndUpdate()

            Grid.ForceInitialize()

            Dim ritem As RepositoryItemCheckEdit = TryCast(GridView1.Columns("Seleccionar").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtArchivo.Text = ""
        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As CheckEdit = TryCast(sender, CheckEdit)

            If ritem IsNot Nothing Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow
                Dim lIndex As Integer = -1
                lIndex = pListObj.FindIndex(Function(b) b.NombreHoja = CStr(Dr.Item("Hoja")))

                If ritem.Checked Then
                    For Each Obj As clsExcel In pListObj
                        If Obj.Index = lIndex Then
                            Obj.Checked = True
                        Else
                            Obj.Checked = False
                        End If
                    Next
                    If pListObj.Count > 0 Then
                        DsExcel.Clear()
                        Grid.BeginUpdate()
                        For Each Obj As clsExcel In pListObj
                            Dim lRow As DataRow = DsExcel.Data.NewRow
                            lRow.Item("Hoja") = Obj.NombreHoja
                            lRow.Item("Seleccionar") = Obj.Checked
                            DsExcel.Data.AddDataRow(lRow)
                        Next
                        Grid.EndUpdate()
                        Grid.ForceInitialize()
                    End If
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private beLogImportacion As New clsBeLog_importacion_excel()

    Private Sub Importar_Archivo()

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Procesando archivo...")

        Try

            Dim Obj As clsExcel = pListObj.Find(Function(s) s.Checked = True)
            Dim fileName As String = txtArchivo.Text
            Dim Hash As String = clsPublic.Check_MD5(txtArchivo.Text)
            Dim documento As XLWorkbook = New XLWorkbook(fileName)
            Dim documento1 As IXLWorksheet = documento.Worksheet(Obj.Index + 1)
            Dim DT As New DataTable
            Dim i As Integer = 0
            Dim firstRow As Boolean = True
            Dim Range As IXLRange
            Range = documento1.RangeUsed(XLCellsUsedOptions.Contents)
            Dim firstCol As Integer
            Dim lastCol As Integer

            beLogImportacion = New clsBeLog_importacion_excel()
            beLogImportacion.IdBodega = IdBodega
            beLogImportacion.IdEmpresa = AP.IdEmpresa
            beLogImportacion.IdUsuario = IdUsuario
            beLogImportacion.Hash_archivo = Hash

            If clsLnLog_importacion_excel.Existe(Hash) Then
                SplashScreenManager.CloseForm(False)
                If XtraMessageBox.Show("¿El archivo fue importado previamente, volver a importar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            If documento1.RowsUsed.Count < 2 Then
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("La hoja esta vacía. no contiene datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Procesando archivo...")

            Dim vIndicadorFila As Integer = 1
            Dim vFilaDetalle As Integer = 7
            Dim vIndicadorColumna As Integer = 1
            Dim vCodigoBarraScanPoliza As String = ""
            Dim vCodigoTicket As Integer = 0
            Dim vNombreOperador As String = ""
            Dim vValorCelda As String = ""
            Dim vNITConsEnc As String = ""
            Dim vValorCeldaNITCons As String = ""

            DT.Clear()

            For Each row As IXLRow In documento1.Rows

                Debug.WriteLine("Row its at: " & row.RowNumber & " and vIndicadorFila is at: " & vIndicadorFila)
                '#EJC20210406:Corresponde a la segunda fila del excel, pero es la primera en ser utilizada.
                'Aquí se valida el scan de la poliza.
                If vIndicadorFila = 1 Then

                    SplashScreenManager.Default.SetWaitFormCaption("Procesando el encabezado...")

                    If Not row.FirstCellUsed Is Nothing Then

                        firstCol = row.FirstCellUsed().Address.ColumnNumber
                        lastCol = row.LastCellUsed().Address.ColumnNumber

                        For Each cell As IXLCell In row.Cells(firstCol, lastCol)

                            If cell.Address.ColumnLetter = "B" AndAlso cell.Address.ColumnNumber = "2" Then
                                If Not cell.Value.ToString = "SCAN POLIZA" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo scan poliza en la celda B2")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                End If
                            End If

                            If cell.Address.ColumnLetter = "C" AndAlso cell.Address.ColumnNumber = "3" Then
                                If Not cell.Value.ToString = "" Then
                                    vCodigoBarraScanPoliza = cell.Value.ToString.Trim()
                                    gScanCodigoBarraPoliza = vCodigoBarraScanPoliza
                                End If

                            End If

                        Next cell

                        vIndicadorFila += 1

                    End If

                ElseIf vIndicadorFila = 2 Then

                    i = 0

                    firstCol = row.FirstCellUsed().Address.ColumnNumber
                    lastCol = row.LastCellUsed().Address.ColumnNumber

                    For Each cell As IXLCell In row.Cells(firstCol, lastCol)

                        If cell.Address.ColumnLetter = "B" AndAlso cell.Address.ColumnNumber = "3" Then
                            If Not cell.Value.ToString = "SCAN TICKET" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo scan ticket en la celda B3")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            End If
                        End If

                        If cell.Address.ColumnLetter = "C" AndAlso cell.Address.ColumnNumber = "3" Then
                            If Not cell.Value.ToString = "" Then
                                vCodigoTicket = cell.Value.ToString.Trim()
                                gScanTicket = vCodigoTicket
                            End If
                        End If

                    Next cell

                    vIndicadorFila += 1

                ElseIf vIndicadorFila = 3 Then

                    firstCol = row.FirstCellUsed().Address.ColumnNumber
                    lastCol = row.LastCellUsed().Address.ColumnNumber

                    For Each cell As IXLCell In row.Cells(firstCol, lastCol)

                        If cell.Address.ColumnLetter = "B" AndAlso cell.Address.ColumnNumber = "2" Then
                            If Not cell.Value.ToString = "NOMBRE OPERADOR" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo NOMBRE OPERADOR en la celda B4")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            End If
                        End If

                        If cell.Address.ColumnLetter = "C" AndAlso cell.Address.ColumnNumber = "3" Then
                            If Not cell.Value.ToString = "" Then
                                vNombreOperador = cell.Value.ToString.Trim()
                                gNombreOperador = vNombreOperador
                            End If
                        End If

                    Next cell

                    vIndicadorFila += 1

                ElseIf vIndicadorFila = 4 Then

                    firstCol = row.FirstCellUsed().Address.ColumnNumber
                    lastCol = row.LastCellUsed().Address.ColumnNumber + 1

                    For Each cell As IXLCell In row.Cells(firstCol, lastCol)

                        If cell.Address.ColumnLetter = "B" AndAlso cell.Address.ColumnNumber = "2" Then
                            If Not cell.Value.ToString = "NIT CONSOLIDADOR" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo NIT CONSOLIDADOR en la celda B5")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            End If
                        End If

                        If cell.Address.ColumnLetter = "C" AndAlso cell.Address.ColumnNumber = "3" Then

                            If Not cell.Value.ToString = "" Then

                                vNITConsEnc = cell.Value.ToString.Trim().Replace("-", "")

                                If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then 'Es un ingreso consolidado, el NIT debe ser igual al del documento de ingreso

                                    Dim vIdPropietario As Integer = clsLnPropietarios.Get_IdPropietario(IdBodega, IdPropietarioBodega)
                                    Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(vIdPropietario)

                                    'El NIT del consolidador debe ser igual al NIT del propietario del documento, si no lo es debe darse un mensaje de error
                                    If vNITConsEnc <> BePropietario.NIT Then
                                        errores = True
                                        lblprg.AppendText("Error con el NIT del Consolidador del documento a importar " & vFilaDetalle & ": " & "El NIT encontrado es " & vNITConsEnc & " y debe ser igual al NIT registrado " & BePropietario.NIT)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                    End If
                                Else

                                    '#CKFK 20210827 Se cambio la validación para el documento
                                    If vNITConsEnc <> "" Then
                                        errores = True
                                        Throw New Exception(String.Format("Se está importando un documento consolidado en un tipo de documento {0} que no es consolidado", pTipoDocumento))
                                    End If

                                End If

                            Else
                                vNITConsEnc = ""
                            End If

                        End If

                    Next cell

                    vIndicadorFila += 1

                ElseIf (vIndicadorFila = 6) Then

                    firstCol = row.FirstCellUsed().Address.ColumnNumber
                    lastCol = row.LastCellUsed().Address.ColumnNumber

                    For Each cell As IXLCell In row.Cells(firstCol, lastCol)

                        If cell.Address.ColumnLetter = "A" AndAlso cell.Address.ColumnNumber = "1" Then
                            If Not cell.Value.ToString.ToUpper = "NO_LINEA" AndAlso Not cell.Value.ToString.ToUpper = "NO. DE LINEA" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo NO. DE LINEA en la celda A6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("NO_LINEA", GetType(Integer))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "B" AndAlso cell.Address.ColumnNumber = "2" Then
                            If Not cell.Value.ToString = "CODIGO_BARRA" AndAlso Not cell.Value.ToString = "HBL/OTS/SKU - CÓDIGO DE BARRA" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo CODIGO_BARRA en la celda B6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("CODIGO_BARRA", GetType(String))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "C" AndAlso cell.Address.ColumnNumber = "3" Then
                            If Not cell.Value.ToString = "CODIGO_PRODUCTO" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo CODIGO_PRODUCTO en la celda C6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("CODIGO_PRODUCTO", GetType(String))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "D" AndAlso cell.Address.ColumnNumber = "4" Then
                            If Not cell.Value.ToString = "DESCRIPCION" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo DESCRIPCION en la celda D6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("DESCRIPCION", GetType(String))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "E" AndAlso cell.Address.ColumnNumber = "5" Then
                            If Not cell.Value.ToString = "NIT_FACTURAR_A" AndAlso Not cell.Value.ToString = "NIT-FACTURAR-A" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo NIT FACTURAR A en la celda E6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("NIT_FACTURAR_A", GetType(String))
                            End If

                        End If

                        If cell.Address.ColumnLetter = "F" AndAlso cell.Address.ColumnNumber = "6" Then
                            If Not cell.Value.ToString = "NIT_PROPIETARIO" AndAlso Not cell.Value.ToString = "NIT-PROPIETARIO" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo NIT PROPIETARIO en la celda F6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("NIT_PROPIETARIO", GetType(String))
                            End If

                        End If

                        If cell.Address.ColumnLetter = "G" AndAlso cell.Address.ColumnNumber = "7" Then
                            If Not cell.Value.ToString = "PROPIETARIO" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo PROPIETARIO en la celda G6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("PROPIETARIO", GetType(String))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "H" AndAlso cell.Address.ColumnNumber = "8" Then
                            If Not cell.Value.ToString = "EMBARCADOR" AndAlso Not cell.Value.ToString = "SHIPPER" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo EMBARCADOR en la celda H6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("EMBARCADOR", GetType(String))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "I" AndAlso cell.Address.ColumnNumber = "9" Then
                            If Not cell.Value.ToString = "PIECES" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo BULTOS en la celda I6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("BULTOS", GetType(Double))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "J" AndAlso cell.Address.ColumnNumber = "10" Then
                            If Not cell.Value.ToString = "PESO/KGS" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo PESO-KGS en la celda J6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("PESO-KGS", GetType(Double))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "K" AndAlso cell.Address.ColumnNumber = "11" Then
                            If Not cell.Value.ToString = "CBMS" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo CBMS en la celda K6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("CBMS", GetType(Double))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "L" AndAlso cell.Address.ColumnNumber = "12" Then

                            If Not cell.Value.ToString = "VALOR-ADUANA" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo VALOR-ADUANA en la celda L6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("VALOR-ADUANA", GetType(Double))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "M" AndAlso cell.Address.ColumnNumber = "13" Then
                            If Not cell.Value.ToString = "FOB-$" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo FOB-$ en la celda M6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("FOB$", GetType(Double))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "N" AndAlso cell.Address.ColumnNumber = "14" Then
                            If Not cell.Value.ToString = "FLETE-$" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo FLETE-$ en la celda N6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("FLETE$", GetType(Double))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "O" AndAlso cell.Address.ColumnNumber = "15" Then
                            If Not cell.Value.ToString = "SEGURO-$" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo SEGURO-$ en la celda O6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("SEGURO$", GetType(Double))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "P" AndAlso cell.Address.ColumnNumber = "16" Then
                            If Not cell.Value.ToString = "DAI-Q" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo DAI-Q en la celda P6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("DAI-Q", GetType(Double))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "Q" AndAlso cell.Address.ColumnNumber = "17" Then
                            If Not cell.Value.ToString = "IVA-Q" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo IVA-Q en la celda Q6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("IVA-Q", GetType(Double))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "R" AndAlso cell.Address.ColumnNumber = "18" Then
                            If Not cell.Value.ToString.ToUpper = "#POLIZA" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo #POLIZA en la celda R6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("#POLIZA", GetType(String))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "S" AndAlso cell.Address.ColumnNumber = "19" Then
                            If Not cell.Value.ToString = "TASA_CAMBIO" AndAlso Not cell.Value.ToString = "TIPO DE CAMBIO" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo TASA_CAMBIO en la celda S6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("TASA_CAMBIO", GetType(Double))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "T" AndAlso cell.Address.ColumnNumber = "20" Then
                            If Not cell.Value.ToString = "UMBAS" AndAlso Not cell.Value.ToString = "UM_BAS" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo UMBAS en la celda T6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("UMBAS", GetType(String))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "U" AndAlso cell.Address.ColumnNumber = "21" Then
                            If Not cell.Value.ToString = "PRESENTACION" AndAlso Not cell.Value.ToString = "PRESENTACION" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo PRESENTACION en la celda U6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("PRESENTACION", GetType(String))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "V" AndAlso cell.Address.ColumnNumber = "22" Then
                            If Not cell.Value.ToString = "FACTOR_PRESENTACION" AndAlso Not cell.Value.ToString = "FACTOR_PRESENTACION" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo FACTOR_PRESENTACION en la celda V6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("FACTOR_PRESENTACION", GetType(Double))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "W" AndAlso cell.Address.ColumnNumber = "23" Then
                            If Not cell.Value.ToString = "BULTOS_POR_PALLET" AndAlso Not cell.Value.ToString = "BULTOS POR PALLET" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo BULTOS_POR_PALLET en la celda W6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("BULTOS_POR_PALLET", GetType(Double))
                            End If
                        End If

                        If cell.Address.ColumnLetter = "X" AndAlso cell.Address.ColumnNumber = "24" Then
                            If Not cell.Value.ToString = "CLASIFICACION" Then
                                errores = True
                                lblprg.AppendText("Error : " & "El formato en excel No contiene el campo CLASIFICACION en la celda X6")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                DT.Columns.Add("CLASIFICACION", GetType(String))
                            End If
                        End If

                    Next cell

                    vIndicadorFila += 1

                ElseIf (vIndicadorFila = 7) Then

                    SplashScreenManager.Default.SetWaitFormCaption("Procesando el detalle...")

                    firstCol = row.FirstCellUsed().Address.ColumnNumber
                    lastCol = row.LastCellUsed().Address.ColumnNumber

                    DT.Rows.Add() : i = 0

                    For Each cell As IXLCell In row.Cells(firstCol, lastCol)

                        vValorCelda = cell.Value.ToString().Replace("&", "")

                        'Quité la columna 18 porque es la del Póliza que no es un número y agregué la 10 que si 
                        '                            OrElse cell.Address.ColumnNumber = 18 _

                        If cell.Address.ColumnNumber = 23 _
                            OrElse cell.Address.ColumnNumber = 22 _
                            OrElse cell.Address.ColumnNumber = 19 _
                            OrElse cell.Address.ColumnNumber = 17 _
                            OrElse cell.Address.ColumnNumber = 16 _
                            OrElse cell.Address.ColumnNumber = 15 _
                            OrElse cell.Address.ColumnNumber = 14 _
                            OrElse cell.Address.ColumnNumber = 13 _
                            OrElse cell.Address.ColumnNumber = 12 _
                            OrElse cell.Address.ColumnNumber = 11 _
                            OrElse cell.Address.ColumnNumber = 10 _
                            OrElse cell.Address.ColumnNumber = 9 _
                            OrElse cell.Address.ColumnNumber = 1 Then

                            vValorCelda = Val(vValorCelda)

                        End If

                        DT.Rows(DT.Rows.Count - 1).Item(i) = vValorCelda
                        i += 1

                    Next

                    SplashScreenManager.Default.SetWaitFormCaption("Validando los propietarios definidos en el documento...")

                    i = 0

                    For Each cell As IXLCell In row.Cells(firstCol, lastCol)

                        vValorCelda = cell.Value.ToString().Replace("&", "")

                        If cell.Address.ColumnNumber = 5 Then

                            vValorCeldaNITCons = vValorCelda

                            If pTipoDocumento <> 7 Then 'No es un ingreso consolidado, el NIT debe ser igual al del documento de ingreso

                                Dim vIdPropietario As Integer = clsLnPropietarios.Get_IdPropietario(IdBodega, IdPropietarioBodega)
                                Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(vIdPropietario)

                                '#CKFK 20210825 Agregué validacion para cuando el tipo de documento no es consolidado
                                'El NIT del consolidador debe ser igual al NIT del propietario del documento, si no lo es debe darse un mensaje de error
                                If (vValorCelda <> "") Then
                                    If vValorCelda.Replace("-", "") <> BePropietario.NIT Then
                                        errores = True
                                        lblprg.AppendText("Error en fila " & vFilaDetalle & ": " & "El NIT del propietario encontrado es " & vValorCelda & " y debe ser igual al NIT del propietario " & BePropietario.NIT)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                    End If

                                Else
                                    errores = True
                                    lblprg.AppendText("Error en fila " & vFilaDetalle & ": " & "El NIT del propietario esta vacio.")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                End If
                            End If

                        End If

                        If cell.Address.ColumnNumber = 6 Then

                            Dim vIdPropietario As Integer = clsLnPropietarios.Get_IdPropietario(IdBodega, IdPropietarioBodega)
                            Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(vIdPropietario)

                            If pTipoDocumento <> clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then

                                'El NIT del propietario debe ser igual al propietario del documento, si no lo es debe darse un mensaje de error
                                If (vValorCelda <> "") Then
                                    If vValorCelda <> vValorCeldaNITCons Then
                                        errores = True
                                        lblprg.AppendText("Error en fila " & vFilaDetalle & ": " & "El NIT del propietario en el documento encontrado es " & vValorCelda & " y debe ser igual al NIT del propietario registrado en WMS " & vValorCeldaNITCons)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                    End If
                                Else
                                    errores = True
                                    lblprg.AppendText("Error en fila " & vFilaDetalle & ": " & "El NIT del propietario esta vacio.")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                End If

                            End If

                        End If

                        i += 1

                    Next

                    vFilaDetalle += 1

                    If vFilaDetalle = documento1.RowsUsed.Count + 1 Then
                        vIndicadorFila += 1
                    End If

                Else
                    vIndicadorFila += 1
                End If

            Next row

            If Not errores Then

                SplashScreenManager.Default.SetWaitFormCaption("Validación de estructura...")

                lblprg.AppendText("Validación de estructura correcta...")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                If DT.Rows.Count > 0 Then

                    SplashScreenManager.Default.SetWaitFormCaption("Verificando productos...")

                    lblprg.AppendText("Verificando propietarios y productos...")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    If Tiene_Errores_Excel(DT) Then

                        SplashScreenManager.CloseForm(False)

                        lblprg.AppendText("No se pudo realizar la importación del archivo, por favor revise e intente nuevamente.")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    Else

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Procesando líneas detalle...")

                        If Llena_Detalle_DI_Excel(DT) Then

                            beLogImportacion.IdImportacion = clsLnLog_importacion_excel.MaxID() + 1

                            clsLnLog_importacion_excel.Insertar(beLogImportacion)

                            If SplashScreenManager.Default IsNot Nothing Then

                                SplashScreenManager.Default.SetWaitFormCaption("Finalizando...")

                                SplashScreenManager.CloseForm(False)

                            End If

                            XtraMessageBox.Show("Importación realizada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            Me.DialogResult = DialogResult.OK

                        Else

                            SplashScreenManager.CloseForm(False)

                            lblprg.AppendText("No se pudo realizar la importación del archivo, por favor revise e intente nuevamente.")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            XtraMessageBox.Show("No se pudo realizar la importación del Excel.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        End If

                    End If

                End If

            Else

                SplashScreenManager.CloseForm(False)

                lblprg.AppendText("No se puedo realizar la importación del archivo, por favor revise e intente nuevamente.")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.DialogResult = DialogResult.Cancel
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Sub

    Dim Contador As Integer = 0

    Private Function Tiene_Errores_Excel(ByRef pDT As DataTable) As Boolean


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim pListObjP As New List(Of clsBeProducto)
        Dim ObjListPBod As New List(Of clsBeProducto_bodega)
        Dim ObjProductoNuevo As New clsBeProducto
        Dim ObjPBod As New clsBeProducto_bodega
        Dim ObjPPres As New clsBeProducto_Presentacion
        Dim max As Integer
        Dim maxPres As Integer
        Dim NombreProducto As String
        Dim lIdExiste As Boolean
        Dim insertados As Integer = 0
        Dim ObjUM As New clsBeUnidad_medida
        Dim ObjConfigInt As New clsBeI_nav_config_enc
        Dim lBodegas As New List(Of clsBeBodega)

        Tiene_Errores_Excel = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Cursor = Cursors.WaitCursor

            Contador = 0
            errores = False

            If pIdConfiguracion = 0 Then
                XtraMessageBox.Show(String.Format("La Bodega {0} de la Empresa {1} no  tiene definida configuración para interface", AP.NomBodega, AP.NomEmpresa),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
                Tiene_Errores_Excel = True
                Return True
            End If

            ObjConfigInt = clsLnI_nav_config_enc.GetSingle(pIdConfiguracion, lConnection, lTransaction)

            '#GT31082023: Valida que la póliza del excel no exista o que se confirme registrarla nuevamente.
            '#GT21092023: confirmar que sea fiscal para validar si hay póliza en el excel importado.
            If AP.Bodega.Es_Bodega_Fiscal Then

                Dim pNumero_Orden As String = ""

                If gScanCodigoBarraPoliza.Length > 10 Then
                    pNumero_Orden = gScanCodigoBarraPoliza.Substring(0, 10)
                Else
                    pNumero_Orden = "0"

                    clsLnLog_error_wms.Agregar_Error("Advertencia_21062024: Importación por Excel no contiene el scanner de la póliza. Operado por: " & AP.UsuarioAp.IdUsuario)

                End If

                '#GT31082023: Se valida si exite ingreso con misma póliza, según el numero_orden
                Dim pPolizaExiste = New clsBeTrans_oc_pol
                pPolizaExiste = clsLnTrans_oc_pol.GetSingle_By_Numero_Orden(pNumero_Orden)

                If Not pPolizaExiste Is Nothing Then

                    SplashScreenManager.CloseForm(False)

                    If XtraMessageBox.Show("¿La póliza ingresada " & pPolizaExiste.numero_orden & " ya existe, asociar el ingreso a póliza existente?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Dim pLog = "ADVERTENCIA_IMPORTACION_31082023_A: El usuario" & AP.UsuarioAp.IdUsuario & " esta asociando la poliza existente " & pPolizaExiste.numero_orden & " a mas de un ingreso"
                        clsLnLog_error_wms.Agregar_Error(pLog)

                        lblprg.AppendText("Advertencia : " & " se esta asociando la poliza existente a mas de un ingreso.")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Procesando líneas de detalle...")

                    Else

                    End If
                End If

            End If


            Dim vCodigoProducto As String = ""
            Dim vCodigoBarra As String = ""
            Dim vDescripcionProducto As String = ""
            Dim vNoLinea As Integer = 0
            Dim vNitPropietario As String = ""
            Dim vNombrePropietario As String = ""
            Dim BePropietario As New clsBePropietarios()
            Dim vCosto As Double = 0
            Dim vUnidadMedidaBasica As String = ""
            Dim vNombrePresentacion As String = ""
            Dim vFactorPresentacion As Double = 0
            Dim vBultosPorTarima As Double = 0
            Dim vClasificacion As String = ""
            Dim vNitFacturarA As String = ""
            '#GT26042022_1522: set de la descripcion del producto sin returns del ENTER en Excel
            Dim vDescripcion_limpia As String = ""

            '#EJC20210901: llenar bodegas (activas) disponibles para asociar el producto_bodega en importación de excel. método tiene_errores_excel.
            lBodegas = clsLnBodega.GetAll(lConnection, lTransaction)

            For Each r As DataRow In pDT.Rows

                vNoLinea = IIf(IsDBNull(r.Item("NO_LINEA")), 0, r.Item("NO_LINEA"))
                vCodigoProducto = IIf(IsDBNull(r.Item("CODIGO_PRODUCTO")), "", r.Item("CODIGO_PRODUCTO"))
                vCodigoBarra = IIf(IsDBNull(r.Item("CODIGO_BARRA")), "", r.Item("CODIGO_BARRA"))
                vDescripcionProducto = IIf(IsDBNull(r.Item("DESCRIPCION")), "", r.Item("DESCRIPCION"))
                vUnidadMedidaBasica = IIf(IsDBNull(r.Item("UMBAS")), "", r.Item("UMBAS"))
                vNombrePresentacion = IIf(IsDBNull(r.Item("PRESENTACION")), "", r.Item("PRESENTACION"))
                vFactorPresentacion = IIf(IsDBNull(r.Item("FACTOR_PRESENTACION")), 0, r.Item("FACTOR_PRESENTACION"))
                vBultosPorTarima = IIf(IsDBNull(r.Item("BULTOS_POR_PALLET")), 0, r.Item("BULTOS_POR_PALLET"))
                vClasificacion = IIf(IsDBNull(r.Item("CLASIFICACION")), "", r.Item("CLASIFICACION"))
                vClasificacion = clsPublic.Quitar_Caracteres_No_Permitidos(vClasificacion.Trim())

                If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then

                    vNitFacturarA = IIf(IsDBNull(r.Item("NIT_FACTURAR_A")), "", r.Item("NIT_FACTURAR_A"))
                    vNitFacturarA = vNitPropietario.Replace("-", "")
                    vNitPropietario = IIf(IsDBNull(r.Item("NIT_PROPIETARIO")), "", r.Item("NIT_PROPIETARIO"))
                    vNitPropietario = vNitPropietario.Replace("-", "")

                    If vNitPropietario <> vNitFacturarA Then

                        If vClasificacion <> "" Then

                            lblprg.AppendText("Advertencia : " & "La Clasificación que está enviando no se va a poder guardar porque allí vamos a guardar el propietario")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End If

                    End If

                Else
                    vNitPropietario = IIf(IsDBNull(r.Item("NIT_PROPIETARIO")), "", r.Item("NIT_PROPIETARIO"))
                    vNitPropietario = vNitPropietario.Replace("-", "")
                End If

                '#GT13082023: se remueven caracteres previamente, no esperar hasta guardar.
                vNombrePropietario = IIf(IsDBNull(r.Item("PROPIETARIO")), "", r.Item("PROPIETARIO"))
                vNombrePropietario = clsPublic.Quitar_Caracteres_No_Permitidos(vNombrePropietario)

                If Not Val(vNoLinea) = 0 AndAlso Not vCodigoProducto = "" AndAlso Not vDescripcionProducto = "" AndAlso Not vNitPropietario = "" Then

                    BePropietario = clsLnPropietarios.Get_Single_By_NIT(vNitPropietario, lConnection, lTransaction)

                    If BePropietario Is Nothing Then

                        BePropietario = clsLnPropietarios.Get_Single_By_Nombre(vNombrePropietario, lConnection, lTransaction)

                        If BePropietario Is Nothing Then
                            'Throw New Exception("El propietario: " & vNombrePropietario & " con NIT: " & vNitPropietario & " no existe en WMS.")
                            Tiene_Errores_Excel = True
                            errores = True
                            lblprg.AppendText("Error : " & "El propietario: " & vNombrePropietario & " con NIT: " & vNitPropietario & " no existe en WMS.")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            Exit Function

                            'lblprg.AppendText("Error : No se puede importar el producto " & vCodigoProducto & " porque el propietario no existe en WMS.")
                            'lblprg.AppendText(vbNewLine)
                            'lblprg.Refresh()
                            'lblprg.SelectionStart = lblprg.TextLength
                            'lblprg.ScrollToCaret()

                        End If

                    Else

                        If vDescripcionProducto = "" Then
                            Tiene_Errores_Excel = True
                            errores = True
                            lblprg.AppendText("Error : " & "Falta el nombre del producto " & vCodigoProducto)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            Exit Function
                        Else

                            '#GT26042022_1501: se valida que, no tenga Retorno de linea, y que no sobrepase el tamaño predefinido
                            '#GT13082023: se remueven caracteres antes de validar una existencia previa
                            vDescripcion_limpia = clsPublic.Quitar_Caracteres_No_Permitidos(vDescripcionProducto.Replace(vbLf, ""))

                            If vDescripcion_limpia.Trim.Length() > 100 Then
                                Tiene_Errores_Excel = True
                                errores = True
                                lblprg.AppendText("Error : " & "La descripción del producto sobrepasa el tamaño definido, en la linea " & vNoLinea & " del archivo. Cierre esta ventana y valide.")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                                Exit Function
                            End If

                        End If

                        NombreProducto = vDescripcion_limpia.Trim

                        lIdExiste = pListObjP.Exists(Function(d) d.Nombre = NombreProducto And d.Codigo = vCodigoProducto)

                        If Not lIdExiste Then

                            Contador += 1

                            ObjProductoNuevo = clsLnProducto.Get_Single_By_Codigo(vCodigoProducto, lConnection, lTransaction)

                            If ObjProductoNuevo Is Nothing Then

                                ObjProductoNuevo = New clsBeProducto

                                max = clsLnProducto.MaxID(lConnection, lTransaction) + Contador

                                ObjProductoNuevo.IsNew = True
                                ObjProductoNuevo.IdProducto = max
                                ObjProductoNuevo.Codigo = vCodigoProducto

                                If BePropietario Is Nothing Then
                                    BePropietario = New clsBePropietarios
                                    BePropietario.IdPropietario = clsLnPropietarios.Get_IdPropietario(IdBodega, IdPropietarioBodega, lConnection, lTransaction)
                                End If

                                'Crear unidad de medida
                                ObjProductoNuevo.UnidadMedida = New clsBeUnidad_medida()
                                vUnidadMedidaBasica = clsPublic.Quitar_Caracteres_No_Permitidos(vUnidadMedidaBasica.Trim)
                                ObjUM = clsLnUnidad_medida.Existe_By_Nombre_By_IdPropietario(vUnidadMedidaBasica, BePropietario.IdPropietario, lConnection, lTransaction)

                                If Not ObjUM Is Nothing Then
                                    ObjProductoNuevo.UnidadMedida.IdUnidadMedida = ObjUM.IdUnidadMedida
                                    ObjProductoNuevo.IdUnidadMedidaBasica = ObjUM.IdUnidadMedida
                                Else

                                    ObjUM = New clsBeUnidad_medida
                                    ObjUM.IdUnidadMedida = clsLnUnidad_medida.MaxID(lConnection, lTransaction) + 1
                                    ObjUM.Nombre = vUnidadMedidaBasica
                                    ObjUM.Codigo = vUnidadMedidaBasica
                                    ObjUM.Activo = 1

                                    If BePropietario Is Nothing Then
                                        BePropietario = New clsBePropietarios
                                        BePropietario.IdPropietario = clsLnPropietarios.Get_IdPropietario(IdBodega, IdPropietarioBodega, lConnection, lTransaction)
                                    End If

                                    ObjUM.IdPropietario = BePropietario.IdPropietario 'clsLnPropietarios.Get_IdPropietario(IdBodega, IdPropietarioBodega, lConnection, lTransaction)
                                    ObjUM.IsNew = True
                                    ObjUM.User_agr = AP.UsuarioAp.IdUsuario
                                    ObjUM.User_mod = AP.UsuarioAp.IdUsuario
                                    ObjUM.Fec_agr = Now
                                    ObjUM.Fec_mod = Now

                                    clsLnUnidad_medida.InsertarFromInterface(ObjUM, lConnection, lTransaction)

                                    ObjProductoNuevo.UnidadMedida.IdUnidadMedida = ObjUM.IdUnidadMedida
                                    ObjProductoNuevo.IdUnidadMedidaBasica = ObjUM.IdUnidadMedida

                                End If

                                ObjProductoNuevo.Codigo_barra = vCodigoBarra
                                ObjProductoNuevo.Precio = 0
                                ObjProductoNuevo.Existencia_min = 0
                                ObjProductoNuevo.Existencia_max = 0
                                ObjProductoNuevo.Costo = vCosto
                                ObjProductoNuevo.Peso_referencia = 0
                                ObjProductoNuevo.Peso_tolerancia = 0
                                ObjProductoNuevo.Temperatura_referencia = 0
                                ObjProductoNuevo.Temperatura_tolerancia = 0
                                ObjProductoNuevo.Serializado = 0
                                ObjProductoNuevo.Genera_lote = 0
                                ObjProductoNuevo.Genera_lp = ObjConfigInt.Genera_lp
                                ObjProductoNuevo.Control_vencimiento = ObjConfigInt.Control_vencimiento
                                ObjProductoNuevo.Control_lote = ObjConfigInt.Control_lote
                                ObjProductoNuevo.Control_peso = ObjConfigInt.Control_peso
                                ObjProductoNuevo.Peso_recepcion = 0
                                ObjProductoNuevo.Peso_despacho = 0
                                ObjProductoNuevo.Temperatura_recepcion = 0
                                ObjProductoNuevo.Temperatura_despacho = 0
                                ObjProductoNuevo.Materia_prima = 0
                                ObjProductoNuevo.Tolerancia = 0
                                ObjProductoNuevo.Ciclo_vida = 0
                                ObjProductoNuevo.Propietario = New clsBePropietarios()
                                ObjProductoNuevo.Clasificacion = New clsBeProducto_clasificacion()
                                ObjProductoNuevo.Familia = New clsBeProducto_familia()
                                ObjProductoNuevo.Marca = New clsBeProducto_marca()
                                ObjProductoNuevo.TipoProducto = New clsBeProducto_tipo()
                                ObjProductoNuevo.Arancel = New clsBeArancel()
                                ObjProductoNuevo.IdPropietario = BePropietario.IdPropietario

                                'Si el documento es un consolidado entonces en la clasificación se va a guardar el propietario
                                If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then

                                    ObjProductoNuevo.Clasificacion = clsLnProducto_clasificacion.Get_Single_By_Nombre_By_Propietario(vNombrePropietario, BePropietario.IdPropietario, lConnection, lTransaction)

                                    'Si ya existe la clasificación se la asigna
                                    If Not ObjProductoNuevo.Clasificacion Is Nothing Then

                                        '#GT27042022_0748: se valida que, clasificación guardada sea la misma del excel, sino alertara
                                        If ObjProductoNuevo.Clasificacion.Nombre <> vClasificacion.Trim Then

                                            Tiene_Errores_Excel = True
                                            errores = True
                                            lblprg.AppendText("Error : " & "El producto tiene una clasificación guardada distinta a la del archivo en la linea " & vNoLinea)
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()
                                            Exit Function

                                        End If

                                        ObjProductoNuevo.IdClasificacion = ObjProductoNuevo.Clasificacion.IdClasificacion
                                    Else 'Crea la clasificacion

                                        ObjProductoNuevo.Clasificacion = New clsBeProducto_clasificacion
                                        ObjProductoNuevo.Clasificacion.IdClasificacion = clsLnProducto_clasificacion.MaxId(lConnection, lTransaction)
                                        ObjProductoNuevo.Clasificacion.Nombre = vNombrePropietario
                                        ObjProductoNuevo.Clasificacion.Propietario = BePropietario
                                        ObjProductoNuevo.Clasificacion.Sistema = False
                                        ObjProductoNuevo.Clasificacion.IsNew = True
                                        ObjProductoNuevo.Clasificacion.Activo = True
                                        ObjProductoNuevo.Clasificacion.Fec_agr = Now
                                        ObjProductoNuevo.Clasificacion.User_agr = AP.UsuarioAp.IdUsuario
                                        ObjProductoNuevo.Clasificacion.User_mod = AP.UsuarioAp.IdUsuario
                                        ObjProductoNuevo.Clasificacion.Fec_mod = Now

                                        clsLnProducto_clasificacion.Insertar(ObjProductoNuevo.Clasificacion, lConnection, lTransaction)

                                        ObjProductoNuevo.IdClasificacion = ObjProductoNuevo.Clasificacion.IdClasificacion
                                    End If

                                Else

                                    If vClasificacion <> "" Then

                                        '#GT06102022_1100: se busca clasificación igual a , en lugar de like porque top1 no siempre devuelve el match
                                        ObjProductoNuevo.Clasificacion = clsLnProducto_clasificacion.Get_Single_By_Nombre_By_Propietario(vClasificacion.Trim, BePropietario.IdPropietario, lConnection, lTransaction)

                                        'Si ya existe la clasificación se la asigna
                                        If Not ObjProductoNuevo.Clasificacion Is Nothing Then


                                            '#GT27042022_0748: se valida que, clasificación guardada sea la misma del excel, sino alertara
                                            If ObjProductoNuevo.Clasificacion.Nombre.Trim <> vClasificacion.Trim Then

                                                Tiene_Errores_Excel = True
                                                errores = True
                                                lblprg.AppendText("Error : " & "El producto tiene una clasificación guardada distinta a la del archivo en la linea " & vNoLinea)
                                                lblprg.AppendText(vbNewLine)
                                                lblprg.Refresh()
                                                lblprg.SelectionStart = lblprg.TextLength
                                                lblprg.ScrollToCaret()
                                                Exit Function

                                            End If


                                            ObjProductoNuevo.IdClasificacion = ObjProductoNuevo.Clasificacion.IdClasificacion
                                        Else 'Crea la clasificacion

                                            ObjProductoNuevo.Clasificacion = New clsBeProducto_clasificacion
                                            ObjProductoNuevo.Clasificacion.IdClasificacion = clsLnProducto_clasificacion.MaxId(lConnection, lTransaction)
                                            ObjProductoNuevo.Clasificacion.Nombre = vClasificacion
                                            ObjProductoNuevo.Clasificacion.Propietario = BePropietario
                                            ObjProductoNuevo.Clasificacion.Sistema = False
                                            ObjProductoNuevo.Clasificacion.IsNew = True
                                            ObjProductoNuevo.Clasificacion.Activo = True
                                            ObjProductoNuevo.Clasificacion.Fec_agr = Now
                                            ObjProductoNuevo.Clasificacion.User_agr = AP.UsuarioAp.IdUsuario
                                            ObjProductoNuevo.Clasificacion.User_mod = AP.UsuarioAp.IdUsuario
                                            ObjProductoNuevo.Clasificacion.Fec_mod = Now

                                            clsLnProducto_clasificacion.Insertar(ObjProductoNuevo.Clasificacion, lConnection, lTransaction)

                                            ObjProductoNuevo.IdClasificacion = ObjProductoNuevo.Clasificacion.IdClasificacion

                                        End If

                                    Else

                                        ObjProductoNuevo.IdClasificacion = ObjConfigInt.Idclasificacion
                                        ObjProductoNuevo.Clasificacion = clsLnProducto_clasificacion.GetSingle(ObjConfigInt.Idclasificacion, lConnection, lTransaction)
                                        If Not ObjProductoNuevo.Clasificacion Is Nothing Then
                                            ObjProductoNuevo.Clasificacion.IdClasificacion = ObjProductoNuevo.IdClasificacion
                                        Else
                                            ObjProductoNuevo.IdClasificacion = Nothing
                                        End If

                                    End If

                                End If

                                ObjProductoNuevo.IdFamilia = ObjConfigInt.IdFamilia
                                ObjProductoNuevo.Familia = clsLnProducto_familia.GetSingle(ObjConfigInt.IdFamilia, lConnection, lTransaction)
                                If Not ObjProductoNuevo.Familia Is Nothing Then
                                    ObjProductoNuevo.Familia.IdFamilia = ObjProductoNuevo.IdFamilia
                                Else
                                    ObjProductoNuevo.IdFamilia = Nothing
                                End If

                                ObjProductoNuevo.IdMarca = ObjConfigInt.IdMarca
                                ObjProductoNuevo.Marca = clsLnProducto_marca.GetSingle(ObjConfigInt.IdMarca, lConnection, lTransaction)
                                If Not ObjProductoNuevo.Marca Is Nothing Then
                                    ObjProductoNuevo.Marca.IdMarca = ObjProductoNuevo.IdClasificacion
                                Else
                                    ObjProductoNuevo.IdMarca = Nothing
                                End If

                                ObjProductoNuevo.IdTipoProducto = ObjConfigInt.IdTipoProducto
                                ObjProductoNuevo.TipoProducto = clsLnProducto_tipo.GetSingle(ObjConfigInt.IdTipoProducto, lConnection, lTransaction)
                                If Not ObjProductoNuevo.TipoProducto Is Nothing Then
                                    ObjProductoNuevo.TipoProducto.IdTipoProducto = ObjProductoNuevo.IdTipoProducto
                                Else
                                    ObjProductoNuevo.IdTipoProducto = Nothing
                                End If

                                ObjProductoNuevo.IdCamara = 0
                                ObjProductoNuevo.IdTipoRotacion = 0
                                ObjProductoNuevo.IdPerfilSerializado = 0
                                ObjProductoNuevo.IdIndiceRotacion = 0
                                ObjProductoNuevo.Arancel.IdArancel = 0
                                ObjProductoNuevo.Nombre = NombreProducto
                                ObjProductoNuevo.Activo = True
                                ObjProductoNuevo.User_agr = AP.UsuarioAp.IdUsuario
                                ObjProductoNuevo.Fec_agr = Now
                                ObjProductoNuevo.User_mod = AP.UsuarioAp.IdUsuario
                                ObjProductoNuevo.Fec_mod = Now
                                ObjProductoNuevo.IdTipoEtiqueta = ObjConfigInt.IdTipoEtiqueta

                                lblprg.AppendText("Aviso: " & "Etiqueta para impresión asignada: " & ObjConfigInt.IdTipoEtiqueta & " al producto: " & ObjProductoNuevo.Nombre)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                                clsLnProducto.Insertar(ObjProductoNuevo, lConnection, lTransaction)

                                '#EJC20210901_B: Insertar producto bodega por cada bodega importación excel. (Nuevo producto)
                                For Each B In lBodegas

                                    ObjPBod = New clsBeProducto_bodega
                                    ObjPBod.IdProductoBodega = clsLnProducto_bodega.MaxID(lConnection, lTransaction) + Contador
                                    ObjPBod.IdProducto = max
                                    ObjPBod.IdBodega = B.IdBodega
                                    ObjPBod.Activo = True
                                    ObjPBod.Sistema = False
                                    ObjPBod.User_agr = AP.UsuarioAp.IdUsuario
                                    ObjPBod.Fec_agr = Now
                                    ObjPBod.User_mod = AP.UsuarioAp.IdUsuario
                                    ObjPBod.Fec_mod = Now
                                    clsLnProducto_bodega.InsertarFromInterface(ObjPBod, lConnection, lTransaction)

                                Next

                                'Crear presentacion
                                If vNombrePresentacion <> "" AndAlso vFactorPresentacion > 0 Then

                                    vNombrePresentacion = clsPublic.Quitar_Caracteres_No_Permitidos(vNombrePresentacion)

                                    ObjPPres = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(ObjProductoNuevo.IdProducto, vNombrePresentacion, lConnection, lTransaction)

                                    If ObjPPres Is Nothing Then

                                        ObjPPres = New clsBeProducto_Presentacion

                                        maxPres = clsLnProducto_presentacion.MaxID(lConnection, lTransaction) + 1

                                        ObjPPres.IdPresentacion = maxPres
                                        ObjPPres.IdProducto = max
                                        ObjPPres.Codigo_barra = ObjProductoNuevo.Codigo & "_" & maxPres
                                        ObjPPres.Nombre = vNombrePresentacion
                                        ObjPPres.Imprime_barra = 1
                                        ObjPPres.Peso = 0
                                        ObjPPres.Alto = 0
                                        ObjPPres.Largo = 0
                                        ObjPPres.Ancho = 0
                                        ObjPPres.Factor = vFactorPresentacion
                                        ObjPPres.MinimoExistencia = 0
                                        ObjPPres.MaximoExistencia = 0
                                        ObjPPres.User_agr = AP.UsuarioAp.IdUsuario
                                        ObjPPres.Fec_agr = Now
                                        ObjPPres.User_mod = AP.UsuarioAp.IdUsuario
                                        ObjPPres.Fec_mod = Now
                                        ObjPPres.Activo = 1
                                        ObjPPres.EsPallet = 0
                                        ObjPPres.Precio = 0
                                        ObjPPres.MinimoPeso = 0
                                        ObjPPres.MaximoPeso = 0
                                        ObjPPres.Costo = 0
                                        ObjPPres.CamasPorTarima = vBultosPorTarima
                                        ObjPPres.CajasPorCama = 1
                                        ObjPPres.Genera_lp_auto = 1
                                        ObjPPres.Permitir_paletizar = 0
                                        ObjPPres.Sistema = 0
                                        ObjPPres.Codigo = ObjProductoNuevo.Codigo

                                        clsLnProducto_presentacion.Insertar(ObjPPres, lConnection, lTransaction)

                                    Else

                                        If ObjPPres.Factor <> vFactorPresentacion Then
                                            Tiene_Errores_Excel = True
                                            errores = True
                                            lblprg.AppendText("Error: " & "El factor de la presentacion " & ObjPPres.Nombre & " del producto " & ObjProductoNuevo.Codigo &
                                                              " es distinto al configurado actualmente que es de " & ObjPPres.Factor & " y no de " & vFactorPresentacion)
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()
                                            Exit Function
                                        End If

                                        If ObjPPres.CamasPorTarima <> vBultosPorTarima Then
                                            Tiene_Errores_Excel = True
                                            errores = True
                                            lblprg.AppendText("Error: " & "Las camas por tarima de la presentacion " & ObjPPres.Nombre & " del producto " & ObjProductoNuevo.Codigo &
                                                              " es distinta a la configuración actual que es de " & ObjPPres.CamasPorTarima & " y no de " & vBultosPorTarima)
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()
                                            Exit Function
                                        End If

                                        If ObjPPres.CajasPorCama <> 1 Then
                                            Tiene_Errores_Excel = True
                                            errores = True
                                            lblprg.AppendText("Error: " & "Las cajas por cama de la presentacion " & ObjPPres.Nombre & " del producto " & ObjProductoNuevo.Codigo &
                                                              " es distinta a la configuración actual que es de " & ObjPPres.CajasPorCama & " y no de 1")
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()
                                            Exit Function
                                        End If

                                    End If

                                ElseIf vNombrePresentacion <> "" AndAlso vFactorPresentacion = 0 Then

                                    Tiene_Errores_Excel = True
                                    errores = True
                                    lblprg.AppendText("Error : " & "El producto tiene una Presentacion, pero no se ha definido un factor de conversión en linea " & vNoLinea)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                    Exit Function

                                End If


                                '#GT:12072021 se avisa si el producto existe pero esta inactivo porque aun no se ha eliminado o no se puede eliminar por tener transacciones.
                            ElseIf ObjProductoNuevo.Activo = False Then
                                Tiene_Errores_Excel = True
                                errores = True
                                lblprg.AppendText("Error : " & " El producto " & vCodigoProducto & " ya esta registrado pero inactivo.")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                                Exit Function
                            Else

                                Dim BeProducto As New clsBeProducto
                                BeProducto.IdProducto = ObjProductoNuevo.IdProducto
                                clsLnProducto.Obtener(BeProducto, lConnection, lTransaction)

                                '#EJC20210901_A: Insertar producto bodega por cada bodega importación excel. (Producto Existe o Edición)
                                For Each B In lBodegas

                                    If clsLnProducto_bodega.Existe_Codigo_By_IdBodega(BeProducto.Codigo, B.IdBodega, lConnection, lTransaction) Is Nothing Then

                                        ObjPBod = New clsBeProducto_bodega
                                        ObjPBod.IdProductoBodega = clsLnProducto_bodega.MaxID(lConnection, lTransaction) + Contador
                                        ObjPBod.IdProducto = BeProducto.IdProducto
                                        ObjPBod.IdBodega = B.IdBodega
                                        ObjPBod.Activo = True
                                        ObjPBod.Sistema = False
                                        ObjPBod.User_agr = AP.UsuarioAp.IdUsuario
                                        ObjPBod.Fec_agr = Now
                                        ObjPBod.User_mod = AP.UsuarioAp.IdUsuario
                                        ObjPBod.Fec_mod = Now
                                        clsLnProducto_bodega.InsertarFromInterface(ObjPBod, lConnection, lTransaction)

                                    End If

                                Next

                                'Crear unidad de medida
                                vUnidadMedidaBasica = clsPublic.Quitar_Caracteres_No_Permitidos(vUnidadMedidaBasica.Trim)
                                ObjUM = clsLnUnidad_medida.Existe_By_Nombre_By_IdPropietario(vUnidadMedidaBasica, BePropietario.IdPropietario, lConnection, lTransaction)

                                If Not ObjUM Is Nothing Then

                                    If ObjUM.Activo Then
                                        ObjProductoNuevo.UnidadMedida.IdUnidadMedida = ObjUM.IdUnidadMedida
                                        ObjProductoNuevo.IdUnidadMedidaBasica = ObjUM.IdUnidadMedida
                                    Else
                                        Tiene_Errores_Excel = True
                                        errores = True
                                        lblprg.AppendText("Error : " & "La unidad de medida esta inactiva, no se puede asignar al producto. " & vNoLinea)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                        Exit Function
                                    End If

                                Else

                                    ObjUM = New clsBeUnidad_medida
                                    ObjUM.IdUnidadMedida = clsLnUnidad_medida.MaxID(lConnection, lTransaction) + Contador
                                    ObjUM.Nombre = vUnidadMedidaBasica
                                    ObjUM.Codigo = vUnidadMedidaBasica
                                    ObjUM.Activo = 1

                                    If BePropietario Is Nothing Then
                                        BePropietario = New clsBePropietarios
                                        BePropietario.IdPropietario = clsLnPropietarios.Get_IdPropietario(IdBodega, IdPropietarioBodega, lConnection, lTransaction)
                                    End If

                                    ObjUM.IdPropietario = BePropietario.IdPropietario 'clsLnPropietarios.Get_IdPropietario(IdBodega, IdPropietarioBodega, lConnection, lTransaction)
                                    ObjUM.IsNew = True
                                    ObjUM.User_agr = AP.UsuarioAp.IdUsuario
                                    ObjUM.User_mod = AP.UsuarioAp.IdUsuario
                                    ObjUM.Fec_agr = Now
                                    ObjUM.Fec_mod = Now

                                    clsLnUnidad_medida.InsertarFromInterface(ObjUM, lConnection, lTransaction)

                                    ObjProductoNuevo.UnidadMedida.IdUnidadMedida = ObjUM.IdUnidadMedida
                                    ObjProductoNuevo.IdUnidadMedidaBasica = ObjUM.IdUnidadMedida

                                End If

                                'Crear presentacion
                                If vNombrePresentacion <> "" AndAlso vFactorPresentacion > 0 Then

                                    ObjPPres = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(ObjProductoNuevo.IdProducto, vNombrePresentacion, lConnection, lTransaction)

                                    If ObjPPres Is Nothing Then

                                        ObjPPres = New clsBeProducto_Presentacion

                                        maxPres = clsLnProducto_presentacion.MaxID(lConnection, lTransaction) + 1

                                        ObjPPres.IdPresentacion = maxPres
                                        ObjPPres.IdProducto = ObjProductoNuevo.IdProducto
                                        ObjPPres.Codigo_barra = ObjProductoNuevo.Codigo & "_" & maxPres
                                        ObjPPres.Nombre = vNombrePresentacion
                                        ObjPPres.Imprime_barra = 1
                                        ObjPPres.Peso = 0
                                        ObjPPres.Alto = 0
                                        ObjPPres.Largo = 0
                                        ObjPPres.Ancho = 0
                                        ObjPPres.Factor = vFactorPresentacion
                                        ObjPPres.MinimoExistencia = 0
                                        ObjPPres.MaximoExistencia = 0
                                        ObjPPres.User_agr = AP.UsuarioAp.IdUsuario
                                        ObjPPres.Fec_agr = Now
                                        ObjPPres.User_mod = AP.UsuarioAp.IdUsuario
                                        ObjPPres.Fec_mod = Now
                                        ObjPPres.Activo = 1
                                        ObjPPres.EsPallet = 0
                                        ObjPPres.Precio = 0
                                        ObjPPres.MinimoPeso = 0
                                        ObjPPres.MaximoPeso = 0
                                        ObjPPres.Costo = 0
                                        ObjPPres.CamasPorTarima = vBultosPorTarima
                                        ObjPPres.CajasPorCama = 1
                                        ObjPPres.Genera_lp_auto = 1
                                        ObjPPres.Permitir_paletizar = 0
                                        ObjPPres.Sistema = 0
                                        ObjPPres.Codigo = ObjProductoNuevo.Codigo

                                        clsLnProducto_presentacion.Insertar(ObjPPres, lConnection, lTransaction)

                                    End If

                                ElseIf vNombrePresentacion <> "" AndAlso vFactorPresentacion = 0 Then

                                    Tiene_Errores_Excel = True
                                    errores = True
                                    lblprg.AppendText("Error : " & "El producto tiene una Presentacion, pero no se ha definido un factor de conversión en linea " & vNoLinea)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                    Exit Function

                                End If

                                '**********************
                                'Crear Clasificacion
                                If vClasificacion <> "" Then

                                    ObjProductoNuevo.Clasificacion.Nombre = ObjProductoNuevo.Clasificacion.Nombre.Trim()

                                    If ObjProductoNuevo.Clasificacion.Nombre = "" Then

                                        ObjProductoNuevo.Clasificacion = clsLnProducto_clasificacion.Get_Single_By_Nombre_By_Propietario(vClasificacion,
                                                                                                                             BePropietario.IdPropietario,
                                                                                                                             lConnection,
                                                                                                                             lTransaction)

                                        'Si ya existe la clasificación se la asigna
                                        If Not ObjProductoNuevo.Clasificacion Is Nothing Then
                                            ObjProductoNuevo.IdClasificacion = ObjProductoNuevo.Clasificacion.IdClasificacion
                                        Else 'Crea la clasificacion

                                            ObjProductoNuevo.Clasificacion = New clsBeProducto_clasificacion
                                            ObjProductoNuevo.Clasificacion.IdClasificacion = clsLnProducto_clasificacion.MaxId(lConnection, lTransaction)
                                            ObjProductoNuevo.Clasificacion.Nombre = vClasificacion
                                            ObjProductoNuevo.Clasificacion.Propietario = BePropietario
                                            ObjProductoNuevo.Clasificacion.Sistema = False
                                            ObjProductoNuevo.Clasificacion.IsNew = True
                                            ObjProductoNuevo.Clasificacion.Activo = True
                                            ObjProductoNuevo.Clasificacion.Fec_agr = Now
                                            ObjProductoNuevo.Clasificacion.User_agr = AP.UsuarioAp.IdUsuario
                                            ObjProductoNuevo.Clasificacion.User_mod = AP.UsuarioAp.IdUsuario
                                            ObjProductoNuevo.Clasificacion.Fec_mod = Now

                                            clsLnProducto_clasificacion.Insertar(ObjProductoNuevo.Clasificacion, lConnection, lTransaction)

                                            ObjProductoNuevo.IdClasificacion = ObjProductoNuevo.Clasificacion.IdClasificacion

                                        End If

                                        clsLnProducto.Actualizar(ObjProductoNuevo, lConnection, lTransaction)

                                    ElseIf vClasificacion.Trim <> ObjProductoNuevo.Clasificacion.Nombre.Trim Then


                                        Throw New Exception("El producto {" & vCodigoProducto & "} ya existe con clasificación: {" & ObjProductoNuevo.Clasificacion.Nombre.Trim & "}" & " que es diferente de la clasificación en el excel: {" & vClasificacion.Trim & " }, debe crear un nuevo código de producto si la clasificación es diferente")

                                    End If

                                End If

                                '************************

                            End If

                        End If

                    End If

                Else
                    'una línea vacía en el excel.
                End If

            Next

            lTransaction.Commit()

            Tiene_Errores_Excel = errores


        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Tiene_Errores_Excel = True
            Throw ex
        Finally
            Cursor = Cursors.Default
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try



    End Function

    Private Function Llena_Detalle_DI_Excel(ByRef pDT As DataTable) As Boolean

        Dim pListObjOC As New List(Of clsBeTrans_oc_det)
        Dim BeTransOcDet As New clsBeTrans_oc_det()
        Dim pBeProducto As New clsBeProducto()

        Llena_Detalle_DI_Excel = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Contador = 0

            If pIdConfiguracion = 0 Then
                Throw New Exception(String.Format("La Bodega {0} de la Empresa {1} no  tiene definida configuración para interface", AP.NomBodega, AP.NomEmpresa))
            End If

            Dim ObjConfigInt As New clsBeI_nav_config_enc

            ObjConfigInt = clsLnI_nav_config_enc.GetSingle(pIdConfiguracion, lConnection, lTransaction)

            '#EJC20210407_2313PM: En un día de vacaciones.. 
            'Si se guarda el encabezado continuamos con el proceso.
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            gRefBeOrdenCompra.IdBodega = IdBodega

            If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then
                If gRefBeOrdenCompra.IdPropietarioBodega = 0 Then
                    gRefBeOrdenCompra.IdPropietarioBodega = IdPropietarioBodega

                End If
            End If

            If clsLnTrans_oc_enc.Guarda_Trans_OC_Enc(gRefBeOrdenCompra, lConnection, lTransaction) Then

                IdOrdenCompraEnc = gRefBeOrdenCompra.IdOrdenCompraEnc

                Dim vIdOrdenCompraDet As Integer = clsLnTrans_oc_det.MaxID(IdOrdenCompraEnc, lConnection, lTransaction) + 1
                Dim vIdNoLInea As Integer = clsLnTrans_oc_det.Max_No_Linea(IdOrdenCompraEnc, lConnection, lTransaction) + 1
                Dim vCodigoProducto As String = ""
                Dim vDescripcionProducto As String = ""
                Dim vNoLinea As Integer = 0
                Dim vNitPropietario As String = ""
                Dim vNitConsolidador As String = ""
                Dim vNombrePropietario As String = ""
                Dim BePropietario As New clsBePropietarios()
                Dim vCantidadBultos As Double = 0
                Dim vPesoKgs As Double = 0
                Dim vCBMS As Double = 0
                Dim vValorAduanaExcelQuetzales As Double = 0
                Dim VALOR_ADUANA As Double = 0
                Dim vCIFCalculadoDolares As Double = 0
                Dim vDiferenciaCIF As Double = 0
                Dim vFOBDolares As Double = 0
                Dim vFleteDolares As Double = 0
                Dim vSeguroDolares As Double = 0
                Dim vDAIQuetzales As Double = 0
                Dim vIVAQuetzalesExcel As Double = 0
                Dim vNoPoliza As String = ""
                Dim vTasaCambio As Double = 0
                Dim vFOBQuetzales As Double = 0
                Dim vFleteQuetzales As Double = 0
                Dim vSeguroQuetzales As Double = 0
                Dim vIdPropietarioBodega As Integer = 0
                Dim vConversionCIFDolaresAQuetzales As Double = 0
                Dim vUnidadMedidaBasica As String = ""
                Dim vNombrePresentacion As String = ""
                Dim vFactorPresentacion As Double = 0
                Dim vBultosPorTarima As Double = 0
                Dim ObjPPres As New clsBeProducto_Presentacion
                Dim vIdPresentacion As Integer = 0
                Dim ObjPUM As New clsBeUnidad_medida
                Dim vIdUM As Integer = 0
                Dim vClasificacion As String = ""
                Dim vIdClasificacion As Integer = 0
                Dim vErrorLinea As Boolean = False
                Dim vShipper As String = ""
                Dim BeEmbarcador As New clsBeTrans_oc_embarcador

                For Each r In pDT.Rows

                    vNoLinea = IIf(IsDBNull(r.Item("NO_LINEA")), 0, r.Item("NO_LINEA"))
                    vCodigoProducto = IIf(IsDBNull(r.Item("CODIGO_PRODUCTO")), "", r.Item("CODIGO_PRODUCTO"))
                    vDescripcionProducto = IIf(IsDBNull(r.Item("DESCRIPCION")), "", r.Item("DESCRIPCION"))
                    vNitPropietario = IIf(IsDBNull(r.Item("NIT_PROPIETARIO")), "", r.Item("NIT_PROPIETARIO"))
                    vNitPropietario = vNitPropietario.Replace("-", "")
                    vNitConsolidador = IIf(IsDBNull(r.Item("NIT_FACTURAR_A")), "", r.Item("NIT_FACTURAR_A"))
                    vNitConsolidador = vNitConsolidador.Replace("-", "")
                    vNombrePropietario = IIf(IsDBNull(r.Item("PROPIETARIO")), "", r.Item("PROPIETARIO"))
                    vCantidadBultos = IIf(IsDBNull(r.Item("BULTOS")), 0, r.Item("BULTOS"))
                    vPesoKgs = IIf(IsDBNull(r.Item("PESO-KGS")), 0, r.Item("PESO-KGS"))
                    vCBMS = IIf(IsDBNull(r.Item("CBMS")), 0, r.Item("CBMS"))
                    vValorAduanaExcelQuetzales = IIf(IsDBNull(r.Item("VALOR-ADUANA")), 0, r.Item("VALOR-ADUANA"))
                    VALOR_ADUANA = IIf(IsDBNull(r.Item("VALOR-ADUANA")), 0, r.Item("VALOR-ADUANA"))
                    vFOBDolares = IIf(IsDBNull(r.Item("FOB$")), 0, r.Item("FOB$"))
                    vFleteDolares = IIf(IsDBNull(r.Item("FLETE$")), 0, r.Item("FLETE$"))
                    vSeguroDolares = IIf(IsDBNull(r.Item("SEGURO$")), 0, r.Item("SEGURO$"))
                    vDAIQuetzales = IIf(IsDBNull(r.Item("DAI-Q")), 0, r.Item("DAI-Q"))
                    vIVAQuetzalesExcel = IIf(IsDBNull(r.Item("IVA-Q")), 0, r.Item("IVA-Q"))
                    vNoPoliza = IIf(IsDBNull(r.Item("#POLIZA")), 0, r.Item("#POLIZA"))
                    vTasaCambio = IIf(IsDBNull(r.Item("TASA_CAMBIO")), 0, r.Item("TASA_CAMBIO"))
                    vUnidadMedidaBasica = IIf(IsDBNull(r.Item("UMBAS")), "", r.Item("UMBAS"))
                    vNombrePresentacion = IIf(IsDBNull(r.Item("PRESENTACION")), "", r.Item("PRESENTACION"))
                    vFactorPresentacion = IIf(IsDBNull(r.Item("FACTOR_PRESENTACION")), 0, r.Item("FACTOR_PRESENTACION"))
                    vBultosPorTarima = IIf(IsDBNull(r.Item("BULTOS_POR_PALLET")), 0, r.Item("BULTOS_POR_PALLET"))
                    vClasificacion = IIf(IsDBNull(r.Item("CLASIFICACION")), "", r.Item("CLASIFICACION"))
                    vClasificacion = vClasificacion.Trim()
                    vShipper = IIf(IsDBNull(r.Item("EMBARCADOR")), "", r.Item("EMBARCADOR"))
                    vIdPresentacion = 0

                    If Not vCodigoProducto = "" Then

                        If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then
                            BePropietario = clsLnPropietarios.Get_Single_By_NIT(vNitConsolidador, lConnection, lTransaction)
                        Else
                            BePropietario = clsLnPropietarios.Get_Single_By_NIT(vNitPropietario, lConnection, lTransaction)
                        End If

                        If Not BePropietario Is Nothing Then

                            vIdPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(BePropietario.IdPropietario, IdBodega, lConnection, lTransaction)

                            If vIdPropietarioBodega = 0 Then
                                errores = True : vErrorLinea = True
                                lblprg.AppendText("Error : " & "El propietario con NIT: " & vNitPropietario & " no tiene asociación con la bodega: " & IdBodega)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                vErrorLinea = False
                            End If

                        Else
                            errores = True
                            lblprg.AppendText("Error : " & "No se encontró el propietario con NIT: " & vNitPropietario)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                        End If

                        BeTransOcDet = New clsBeTrans_oc_det()
                        pBeProducto = New clsBeProducto()




                        pBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(vCodigoProducto,
                                                                             IdBodega,
                                                                             lConnection,
                                                                             lTransaction)

                        If Not pBeProducto Is Nothing Then

                            If pBeProducto.IdPropietario = BePropietario.IdPropietario Then

                                If Not String.IsNullOrEmpty(vNombrePresentacion) Then
                                    ObjPPres = clsLnProducto_presentacion.Existe_By_IdProducto_And_NombrePresentacion(pBeProducto.IdProducto, vNombrePresentacion, lConnection, lTransaction)
                                    If ObjPPres IsNot Nothing Then
                                        vIdPresentacion = ObjPPres.IdPresentacion
                                    Else
                                        errores = True
                                        lblprg.AppendText("Error: " & "No esta registrada la presentación: " & vNombrePresentacion)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                    End If

                                End If

                                If Not String.IsNullOrEmpty(vUnidadMedidaBasica) Then
                                    ObjPUM = clsLnUnidad_medida.Existe_By_Nombre_By_IdPropietario(vUnidadMedidaBasica, pBeProducto.IdPropietario, lConnection, lTransaction)

                                    If ObjPUM IsNot Nothing Then
                                        vIdUM = ObjPUM.IdUnidadMedida
                                    Else
                                        errores = True
                                        lblprg.AppendText("Error: " & "No esta registrada la unidad de medida: " & vUnidadMedidaBasica)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                    End If
                                Else
                                    errores = True
                                    lblprg.AppendText("Error: " & "La unidad de medida no puede estar vacia! " & vUnidadMedidaBasica)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                End If

                                BeTransOcDet.IdOrdenCompraEnc = IdOrdenCompraEnc
                                BeTransOcDet.IdOrdenCompraDet = vIdOrdenCompraDet
                                BeTransOcDet.IdPropietarioBodega = vIdPropietarioBodega
                                BeTransOcDet.Nombre_Propietario = vNombrePropietario
                                BeTransOcDet.IdProductoBodega = pBeProducto.IdProductoBodega
                                BeTransOcDet.IdArancel = pBeProducto.IdArancel
                                BeTransOcDet.IdPresentacion = vIdPresentacion 'pBeProducto.IdPresentacionOrigen
                                BeTransOcDet.Presentacion.IdPresentacion = vIdPresentacion
                                BeTransOcDet.IdUnidadMedidaBasica = vIdUM
                                BeTransOcDet.UnidadMedida.IdUnidadMedida = vIdUM
                                BeTransOcDet.IdMotivoDevolucion = IdMotivoDevolucion
                                BeTransOcDet.No_Linea = vIdNoLInea
                                BeTransOcDet.Nombre_producto = pBeProducto.Nombre
                                BeTransOcDet.Nombre_presentacion = vNombrePresentacion
                                BeTransOcDet.Nombre_arancel = pBeProducto.Arancel.Nombre
                                BeTransOcDet.Porcentaje_arancel = 0
                                BeTransOcDet.Nombre_unidad_medida_basica = pBeProducto.UnidadMedida.Nombre

                                If vCantidadBultos = 0 Then
                                    errores = True
                                    lblprg.AppendText("Error: " & "Cantidad no válida para código: " & vCodigoProducto)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                End If

                                If vPesoKgs = 0 AndAlso ObjConfigInt.Control_peso Then
                                    errores = True
                                    lblprg.AppendText("Error: " & "Peso no definido para código: " & vCodigoProducto)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                End If

                                '#GT09092022_0930: Estos campos pueden tener valor 0, ya no se validan
                                'If vFOBDolares = 0 AndAlso (pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado OrElse pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Poliza_DUCA) Then
                                '    errores = True : vErrorLinea = True
                                '    lblprg.AppendText("Error : " & "FOB no definido para código: " & vCodigoProducto)
                                '    lblprg.AppendText(vbNewLine)
                                '    lblprg.Refresh()
                                '    lblprg.SelectionStart = lblprg.TextLength
                                '    lblprg.ScrollToCaret()
                                'Else
                                '    vErrorLinea = False
                                'End If

                                'If vSeguroDolares = 0 AndAlso (pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado OrElse pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Poliza_DUCA) Then
                                '    errores = True : vErrorLinea = True
                                '    lblprg.AppendText("Error : " & "Seguro no definido para código: " & vCodigoProducto)
                                '    lblprg.AppendText(vbNewLine)
                                '    lblprg.Refresh()
                                '    lblprg.SelectionStart = lblprg.TextLength
                                '    lblprg.ScrollToCaret()
                                'Else
                                '    vErrorLinea = False
                                'End If

                                'If vFleteDolares = 0 AndAlso (pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado OrElse pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Poliza_DUCA) Then
                                '    errores = True : vErrorLinea = True
                                '    lblprg.AppendText("Error : " & "Flete no definido para código: " & vCodigoProducto)
                                '    lblprg.AppendText(vbNewLine)
                                '    lblprg.Refresh()
                                '    lblprg.SelectionStart = lblprg.TextLength
                                '    lblprg.ScrollToCaret()
                                'Else
                                '    vErrorLinea = False
                                'End If

                                'If vDAIQuetzales = 0 AndAlso (pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado OrElse pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Poliza_DUCA) Then
                                '    errores = True : vErrorLinea = True
                                '    lblprg.AppendText("Error : " & "DAI no definido para código: " & vCodigoProducto)
                                '    lblprg.AppendText(vbNewLine)
                                '    lblprg.Refresh()
                                '    lblprg.SelectionStart = lblprg.TextLength
                                '    lblprg.ScrollToCaret()
                                'Else
                                '    vErrorLinea = False
                                'End If

                                If vIVAQuetzalesExcel = 0 AndAlso (pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado OrElse pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Poliza_DUCA) Then
                                    errores = True : vErrorLinea = True
                                    lblprg.AppendText("Error : " & "IVA no definido para código: " & vCodigoProducto)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    vIVAQuetzalesExcel = Math.Round(vIVAQuetzalesExcel, 6)
                                    vErrorLinea = False
                                End If

                                If vTasaCambio = 0 AndAlso (pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado OrElse pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Poliza_DUCA) Then
                                    errores = True : vErrorLinea = True
                                    lblprg.AppendText("Error : " & "Tasa de cambio no definida para código: " & vCodigoProducto)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    vErrorLinea = False
                                End If

                                vFOBQuetzales = Math.Round(vFOBDolares * vTasaCambio, 6)
                                vFleteQuetzales = Math.Round(vFleteDolares * vTasaCambio, 6)
                                vSeguroQuetzales = Math.Round(vSeguroDolares * vTasaCambio, 6)

                                '#GT02112022_1600: Acorde a lo hablado con Otto, tomar valor aduana tal cual del excel, sin operaciones, sea fiscal o general
                                'por eso, las validaciones anteriores de pTipoDocumento se dejaron en comentario, se infiere que vienen en Q.
                                vValorAduanaExcelQuetzales = Math.Round(VALOR_ADUANA, 6)


                                '#GT02112022_1630: Se valida que valor aduana tenga siempre valor.
                                If vValorAduanaExcelQuetzales = 0 Then
                                    errores = True : vErrorLinea = True
                                    lblprg.AppendText("Error : " & "El valor Aduana en Quetzales es 0 para el producto " & vCodigoProducto)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    BeTransOcDet.valor_aduana = vValorAduanaExcelQuetzales
                                    vErrorLinea = False
                                End If

                                BeTransOcDet.Cantidad = vCantidadBultos
                                BeTransOcDet.Cantidad_recibida = 0

                                '#EJC20210609: Corrección de calculos por tipo de documento.
                                If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso OrElse pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Almacén_General_Con_Póliza Then
                                    BeTransOcDet.Costo = Math.Round((vValorAduanaExcelQuetzales) / IIf(vCantidadBultos = 0, 1, vCantidadBultos), 2)
                                    'ObjDet.Costo = Math.Round((CIF_Q) / IIf(vCantidadBultos = 0, 1, vCantidadBultos), 2)
                                ElseIf pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Poliza_DUCA OrElse clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then
                                    BeTransOcDet.Costo = Math.Round((vValorAduanaExcelQuetzales + vDAIQuetzales + vIVAQuetzalesExcel) / IIf(vCantidadBultos = 0, 1, vCantidadBultos), 2)
                                Else
                                    BeTransOcDet.Costo = Math.Round((vValorAduanaExcelQuetzales + vDAIQuetzales + vIVAQuetzalesExcel) / IIf(vCantidadBultos = 0, 1, vCantidadBultos), 2)
                                End If

                                'GT si es general con o sin poliza, el iva no se suma al CIF-Q porque este, ya es el total de todo.
                                If pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso OrElse pTipoDocumento = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Almacén_General_Con_Póliza Then
                                    BeTransOcDet.Total_linea = Math.Round(vValorAduanaExcelQuetzales, 2)
                                Else
                                    BeTransOcDet.Total_linea = Math.Round(vValorAduanaExcelQuetzales + vDAIQuetzales + vIVAQuetzalesExcel, 2)
                                End If

                                BeTransOcDet.User_agr = AP.UsuarioAp.IdUsuario
                                BeTransOcDet.Fec_agr = Now
                                BeTransOcDet.User_mod = AP.UsuarioAp.IdUsuario
                                BeTransOcDet.Fec_mod = Now
                                BeTransOcDet.Activo = 1
                                BeTransOcDet.Peso = vPesoKgs
                                BeTransOcDet.Peso_Recibido = 0
                                BeTransOcDet.Atributo_variante_1 = 0
                                BeTransOcDet.Codigo_Producto = pBeProducto.Codigo
                                BeTransOcDet.valor_fob = Math.Round(vFOBQuetzales, 2)
                                'GT11072022: asegurar que el iva para bodega general es 0, aunque tenga valores el excel
                                BeTransOcDet.valor_iva = IIf(gRefBeOrdenCompra.IdBodega = 1, 0, Math.Round(vIVAQuetzalesExcel, 2))
                                BeTransOcDet.valor_dai = Math.Round(vDAIQuetzales, 2)
                                BeTransOcDet.valor_seguro = Math.Round(vSeguroQuetzales, 2)
                                BeTransOcDet.valor_flete = Math.Round(vFleteQuetzales, 2)
                                BeTransOcDet.Peso_Neto = Math.Round(vPesoKgs, 2)
                                BeTransOcDet.Peso_Bruto = Math.Round(vPesoKgs, 2)

                                '#EJC20220224: Guardar IdEmbarcador en detalle de OC.
                                BeEmbarcador = clsLnTrans_oc_embarcador.Get_Single_By_Nombre(vShipper, lConnection, lTransaction)

                                If BeEmbarcador Is Nothing Then
                                    BeEmbarcador = New clsBeTrans_oc_embarcador()
                                    BeEmbarcador.IdEmbarcador = clsLnTrans_oc_embarcador.MaxID(lConnection, lTransaction) + 1
                                    BeEmbarcador.Codigo = BeEmbarcador.IdEmbarcador
                                    BeEmbarcador.Nombre = vShipper
                                    clsLnTrans_oc_embarcador.Insertar(BeEmbarcador, lConnection, lTransaction)
                                    BeTransOcDet.IdEmbarcador = BeEmbarcador.IdEmbarcador
                                Else
                                    BeTransOcDet.IdEmbarcador = BeEmbarcador.IdEmbarcador
                                End If

                                If Not vErrorLinea Then
                                    pListObjOC.Add(BeTransOcDet)
                                End If

                                vIdOrdenCompraDet += 1 : vIdNoLInea += 1

                            Else
                                errores = True
                                lblprg.AppendText("Error : " & "El producto " & pBeProducto.Codigo & " ya existe asociado al propietario: " & pBeProducto.IdPropietario &
                                                           " para poder asociar este código con el propietario " & BePropietario.IdPropietario &
                                                           " debe crear un nuevo código de producto")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            End If

                        Else
                            ' Throw New Exception("No se pudo obtener el producto: " & vCodigoProducto)
                            errores = True
                            lblprg.AppendText("Error : " & "No se pudo obtener el producto: " & vCodigoProducto & " en la linea " & vNoLinea)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                        End If

                    End If

                Next

                If Not errores Then

                    If pListObjOC IsNot Nothing AndAlso pListObjOC.Count > 0 Then

                        If gNombreOperador <> "" Then

                            Dim gOperador As New clsBeOperador
                            Dim IdOperadorBodega As Integer

                            gOperador.Nombres = gNombreOperador
                            gOperador.IdEmpresa = AP.IdEmpresa

                            clsLnOperador.Get_Operador_By_Nombre_And_Apellido(gOperador, lConnection, lTransaction)

                            IdOperadorBodega = clsLnOperador_bodega.Get_IdOperadorBodega_By_IdOperador(gOperador.IdOperador, IdBodega, lConnection, lTransaction)

                            gRefBeOrdenCompra = clsLnTrans_oc_enc.GetSingle(IdOrdenCompraEnc, lConnection, lTransaction)
                            gRefBeOrdenCompra.IdOperadorBodegaDefecto = IdOperadorBodega
                            gRefBeOrdenCompra.IdBodega = IdBodega

                            clsLnTrans_oc_enc.Guarda_Trans_OC_Enc(gRefBeOrdenCompra, lConnection, lTransaction)

                        End If

                        clsLnTrans_oc_det.Guardar_Transaccion(pListObjOC, lConnection, lTransaction)

                        If gScanCodigoBarraPoliza.Trim() <> "" Then

                            Dim BePoliza As New clsBeCEALSA_DUCA_ENC()
                            BePoliza = Scan_Poliza(gScanCodigoBarraPoliza)

                            If Not BePoliza Is Nothing Then

                                '#CKFK Agregué el trim al codigo del régimen
                                Dim BeRegimen As New clsBeRegimen_fiscal()
                                BeRegimen = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(BePoliza.Regimen.Trim, lConnection, lTransaction)

                                Dim BeTransOCPol As New clsBeTrans_oc_pol()
                                BeTransOCPol.User_agr = AP.UsuarioAp.IdUsuario
                                BeTransOCPol.Fec_agr = Now
                                BeTransOCPol.Codigo_Barra = gScanCodigoBarraPoliza
                                BeTransOCPol.Bl_No = ""
                                BeTransOCPol.Pto_Descarga = ""
                                BeTransOCPol.Remitente = ""
                                BeTransOCPol.Fecha_abordaje = Now
                                BeTransOCPol.Descripcion = "BY EXCEL"
                                BeTransOCPol.Cantidad = 0
                                BeTransOCPol.Total_kgs = 0
                                BeTransOCPol.Viaje_no = 0
                                BeTransOCPol.Buque_no = 0
                                BeTransOCPol.Destino = ""
                                BeTransOCPol.Dir_destino = ""
                                BeTransOCPol.Po_number = ""
                                BeTransOCPol.Piezas = 0
                                BeTransOCPol.Cbm = 0
                                'GT18022022: si el regimen de la cadena no es legible puede afectar el tamaño de los demas valores de la póliza
                                If Not BeRegimen Is Nothing Then
                                    BeTransOCPol.IdRegimen = BeRegimen.IdRegimen
                                Else
                                    'Throw New Exception("El régimen: " & BePoliza.Regimen & " no esta registrado o no es legible desde el archivo de importación")
                                    Llena_Detalle_DI_Excel = False
                                    errores = True
                                    lblprg.AppendText("Error: " & "El régimen: " & BePoliza.Regimen & " de la póliza no esta registrado, o no es legible.")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                    Return False

                                End If
                                BeTransOCPol.NoPoliza = ""
                                BeTransOCPol.Pais_procede = BePoliza.Pais_procedencia
                                BeTransOCPol.Total_valoraduana = BePoliza.Total_valor_aduana
                                BeTransOCPol.Total_bultos_Peso_Bruto = BePoliza.Total_bultos_Peso_Bruto
                                BeTransOCPol.Total_bultos_Peso_Neto = 0
                                BeTransOCPol.Total_flete = BePoliza.Total_Flete_USD
                                BeTransOCPol.Total_usd = BePoliza.TotalFOBUSD
                                BeTransOCPol.Dua = BePoliza.Numero_DUCA
                                BeTransOCPol.Fecha_poliza = BePoliza.Fecha_Aceptacion
                                'GT07042022: otra validación en poliza, porque manualmente alteran la cadena y luego es culpa del sistema :(
                                If Valida_Tipo_Cambio_Poliza(BePoliza.Tipo_cambio) Then
                                    BeTransOCPol.Tipo_cambio = BePoliza.Tipo_cambio
                                Else
                                    Llena_Detalle_DI_Excel = False
                                    errores = True
                                    lblprg.AppendText("Error: " & "El tipo cambio: " & BePoliza.Tipo_cambio & " de la póliza es ilegible, o no tiene el formato  00.0000")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                    Return False

                                End If
                                BeTransOCPol.Total_lineas = 0
                                BeTransOCPol.Total_bultos = 0
                                BeTransOCPol.Total_seguro = BePoliza.Total_Seguro_USD
                                BeTransOCPol.User_mod = AP.UsuarioAp.IdUsuario
                                BeTransOCPol.Fec_mod = Now
                                BeTransOCPol.codigo_poliza = BePoliza.Codigo_Poliza
                                BeTransOCPol.ticket = gScanTicket
                                BeTransOCPol.numero_orden = BePoliza.Numero_Orden
                                BeTransOCPol.fecha_aceptacion = BePoliza.Fecha_Aceptacion
                                BeTransOCPol.fecha_llegada = Now 'New Date(1900, 1, 1)
                                BeTransOCPol.total_otros = BePoliza.TotalOtrosgastosUSD
                                BeTransOCPol.clave_aduana = BePoliza.Clave_aduana_despacho_destino.Trim
                                BeTransOCPol.nit_imp_exp = BePoliza.NIT_Importador
                                BeTransOCPol.clase = BePoliza.Clase
                                BeTransOCPol.mod_transporte = BePoliza.Modo_transporte
                                BeTransOCPol.total_liquidar = BePoliza.Total_Liquidar
                                BeTransOCPol.total_general = BePoliza.Total_General
                                BeTransOCPol.Cbm = vCBMS
                                BeTransOCPol.activo = True
                                BeTransOCPol.IdBodega = AP.Bodega.IdBodega

                                clsLnTrans_oc_enc.Guarda_Trans_oc_pol(gRefBeOrdenCompra.IdOrdenCompraEnc,
                                                                      BeTransOCPol,
                                                                      lConnection,
                                                                      lTransaction)

                            End If


                        End If


                    End If

                    Llena_Detalle_DI_Excel = True

                End If


            End If

            lTransaction.Commit()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            gRefBeOrdenCompra.IsNew = True
            Throw ex
        Finally
            SplashScreenManager.CloseForm(False)
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
            errores = False
        End Try

    End Function

    Private Function Scan_Poliza(ByVal barra_poliza As String) As clsBeCEALSA_DUCA_ENC

        Scan_Poliza = Nothing

        Try

            Dim poliza As New clsBeCEALSA_DUCA_ENC()

            If Not String.IsNullOrEmpty(barra_poliza) Then

                Dim Fecha_string = barra_poliza.Substring(30, 8)
                poliza.Numero_Orden = barra_poliza.Substring(0, 10)
                poliza.Numero_DUCA = barra_poliza.Substring(10, 20)
                poliza.Clave_aduana_despacho_destino = barra_poliza.Substring(38, 7)
                poliza.NIT_Importador = barra_poliza.Substring(45, 25).Trim()
                poliza.Regimen = barra_poliza.Substring(70, 5).ToUpper()
                poliza.Clase = barra_poliza.Substring(75, 3).Trim()
                poliza.Pais_procedencia = barra_poliza.Substring(78, 2)
                poliza.Modo_transporte = barra_poliza.Substring(80, 1)
                poliza.Tipo_cambio = Convert.ToDouble(barra_poliza.Substring(81, 7))
                poliza.Total_valor_aduana = Convert.ToDouble(barra_poliza.Substring(88, 16))
                poliza.Total_bultos_Peso_Bruto = Convert.ToDouble(barra_poliza.Substring(104, 15))
                poliza.TotalFOBUSD = Convert.ToDouble(barra_poliza.Substring(119, 16))
                poliza.Total_Flete_USD = Convert.ToDouble(barra_poliza.Substring(135, 15))
                poliza.Total_Seguro_USD = Convert.ToDouble(barra_poliza.Substring(150, 15))
                poliza.TotalOtrosgastosUSD = Convert.ToDouble(barra_poliza.Substring(165, 15))
                poliza.Total_Liquidar = Convert.ToDouble(barra_poliza.Substring(180, 15))
                poliza.Total_General = Convert.ToDouble(barra_poliza.Substring(195, 15))
                poliza.Codigo_Poliza = barra_poliza.Substring(210, 9)

                'concatenación para fecha dd/mm/yyyy
                Dim comodin As String = "/"
                Dim dd As String = ""
                Dim mm As String = ""
                Dim anio As String = ""

                dd = Fecha_string.ToString.Substring(0, 2)
                mm = Fecha_string.ToString.Substring(2, 2)
                anio = Fecha_string.ToString.Substring(4, 4)
                poliza.Fecha_Aceptacion = New Date(anio, mm, dd)

                Scan_Poliza = poliza

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub cmdCargar_Click(sender As Object, e As EventArgs) Handles cmdCargar.Click

        Try

            errores = False

            If Archivo_Valido() Then
                Importar_Archivo()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Archivo_Valido() As Boolean

        Archivo_Valido = False

        Try

            If String.IsNullOrEmpty(txtArchivo.Text.Trim()) Then Throw New Exception("Seleccione archivo.")

            If IO.File.Exists(txtArchivo.Text.Trim()) = False Then Throw New Exception("El archivo no existe.")

            If pListObj IsNot Nothing Then

                If pListObj.Count > 0 = False Then
                    ' Throw New Exception("El archivo debe de contener por lo menos alguna hoja.")
                    errores = True
                    lblprg.AppendText("Error : " & "El archivo debe de contener por lo menos alguna hoja.")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()
                    Exit Function
                Else
                    If pListObj.All(Function(b) b.Checked = False) Then
                        ' Throw New Exception("Seleccione una hoja.")
                        errores = True
                        lblprg.AppendText("Error : " & "Seleccione una hoja.")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        Exit Function
                    End If
                End If

                lblprg.Text = "Archivo validado correctamente: " & Now
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Archivo_Valido = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cmdSalir_Click(sender As Object, e As EventArgs) Handles cmdSalir.Click
        Close()
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles GridView1.RowStyle

        Try

            GridView1.OptionsBehavior.Editable = True
            GridView1.OptionsSelection.EnableAppearanceFocusedCell = True

            GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
            GridView1.OptionsSelection.EnableAppearanceHideSelection = True
            GridView1.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GridView1.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GridView1.Appearance.FocusedRow.ForeColor = Color.White
            GridView1.Appearance.SelectedRow.ForeColor = Color.White

            GridView1.Appearance.SelectedRow.Options.UseBackColor = True
            GridView1.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Public Shared Function Valida_Tipo_Cambio_Poliza(ByVal input As String) As Boolean

        If input.Trim <> "" Then
            'GT04022022: esta expresión valida 2 numeros y 4 decimales de acuerdo al formato SAT
            'Dim valPlaca As Regex = New Regex("^[0-9]*\.[0-9]{4}$ or ^[0-9]*\.[0-9][0-9][0-9][0-9]$")
            'Dim vPlacaValida As Match = valPlaca.Match(input)

            Dim valPlaca2 As Regex = New Regex("^[0-9]*(\.[0-9]{0,4})?$")
            Dim validado As Match = valPlaca2.Match(input)

            If validado.Success Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

End Class
