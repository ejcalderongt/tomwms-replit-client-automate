Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Reflection
Imports ClosedXML.Excel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraSplashScreen

Public Class frmCargaExcel_Inv_Ini_Op_Log

    Public Delegate Sub Operar()
    Public Listar As Operar

    Public pNombreMantenimiento As String
    Public pTipoMantenimiento As String
    Public pPropietario As Boolean
    Public pTipoDocumento As clsDataContractDI.tTipoDocumentoIngreso

    Private pListObj As List(Of clsExcel)

    Public InsertaInv As Boolean = False
    Public IdOperador As Integer = 0
    Public NomOperador As String = ""
    Public IdPropietarioBodega As Integer = 0
    'Public Property IdOrdenCompraEnc As Integer = 0
    Public Property lDetalle_OC As New DataTable
    Public Property IdBodega As Integer = 0
    Public Property IdUsuario As Integer = 0

    Public gRefBeOrdenCompra As New clsBeTrans_oc_enc()
    Public gScanCodigoBarraPoliza As String = ""
    'Public gNombreOperador As String = ""
    'Public gScanTicket As Integer = 0

    Private pIdConfiguracion As Integer
    Private errores As Boolean = False

    Private ListOC_to_ReEnc As New List(Of clsBeTrans_oc_enc)
    Private pListBeStockRec As New List(Of clsBeStock_rec)
    Private gBeRecepcionEnc As New clsBeTrans_re_enc
    Private BeTareaHH As clsBeTarea_hh
    Private lBeProdPallet As New List(Of clsBeProducto_pallet)

    Private pListOpe As New List(Of clsBeTrans_re_op)
    Private lBeTransRecDet As New List(Of clsBeTrans_re_det)
    Private BeTransReDet As New clsBeTrans_re_det
    Private plistBeReDetParametros As New List(Of clsBeTrans_re_det_parametros)
    Private pListRecFact As New List(Of clsBeTrans_re_fact)
    Private pListRecImgs As New List(Of clsBeTrans_re_img)
    Private pListBeStockSeRec As New List(Of clsBeStock_se_rec)

    Private lResolucionesLP As New clsBeResolucion_lp_operador()

    'GT17012022: obtiene la bodega y si es fiscal/general
    Private BeBodega As New clsBeBodega()
    Private BeBodegaArea As New clsBeBodega_area()
    Private Fecha_llegada As DateTime

    Private Sub frmCargaExcel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Text = "Carga de Excel - " & pNombreMantenimiento

            'GT 21102021: set de la bodega en la que se esta importando el inventario
            IdBodega = AP.Bodega.IdBodega
            'la importación es nueva
            gRefBeOrdenCompra.IsNew = True

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

    Public Shared Function ImportExceltoDatatable(filepath As String) As DataTable

        Dim dt As New DataTable

        Try

            Dim ds As New DataSet()
            Dim constring As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filepath & ";Extended Properties=""Excel 12.0;HDR=YES;"""
            Dim con As New OleDbConnection(constring & "")
            con.Open()

            Dim myTableName = con.GetSchema("Tables").Rows(0)("TABLE_NAME")
            Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName)
            Dim da As New OleDbDataAdapter(sqlquery, con)
            da.Fill(ds)
            dt = ds.Tables(0)
            Return dt

        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Critical)
            Return dt
        End Try

    End Function

    Private beLogImportacion As New clsBeLog_importacion_excel()

    Private Function Importar_Archivo() As Boolean

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Procesando archivo...")

        Importar_Archivo = False
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

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
                    Exit Function
                End If
            Else
                'GT27012022: se añade en la tran, si falla, no se guarda el hash, porque no es necesario
                beLogImportacion.IdImportacion = clsLnLog_importacion_excel.MaxID() + 1
                clsLnLog_importacion_excel.Insertar(beLogImportacion, lConnection, lTransaction)
            End If

            If documento1.RowsUsed.Count < 2 Then
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("La hoja esta vacía. no contiene datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Importar_Archivo = False
                Exit Function
            End If

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Procesando Encabezados del archivo...")

            Dim vIndicadorFila As Integer = 1
            Dim vFilaDetalle As Integer = 7
            Dim vIndicadorColumna As Integer = 1
            Dim vCodigoBarraScanPoliza As String = ""
            Dim vCodigoTicket As Integer = 0
            Dim vNombreOperador As String = ""
            Dim vValorCelda As String = ""
            Dim vNITConsEnc As String = ""
            Dim vValorCeldaNITCons As String = ""

            Dim pListObjInv As New List(Of clsBeTrans_inv_inicial_excel_op_log)
            DT.Clear()

            SplashScreenManager.Default.SetWaitFormCaption("Procesando columnas...")

            For Each row As IXLRow In documento1.Rows

                Debug.WriteLine("Row its at: " & row.RowNumber & " and vIndicadorFila is at: " & vIndicadorFila)
                '#EJC20210406:Corresponde a la segunda fila del excel, pero es la primera en ser utilizada.
                'Aquí se valida el scan de la poliza.
                If vIndicadorFila = 1 Then

                    If Not row.FirstCellUsed Is Nothing Then

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
                                If Not cell.Value.ToString.Trim = "CODIGO_PRODUCTO" Then
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
                                If Not cell.Value.ToString.Trim = "DESCRIPCION" Then
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
                                If Not cell.Value.ToString.Trim = "NIT_FACTURAR_A" AndAlso Not cell.Value.ToString.Trim = "NIT-FACTURAR-A" Then
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
                                    DT.Columns.Add("PIECES", GetType(String))
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
                                    'GT 21102021: el valor es alternativo, a veces esta vacio, no puede ser double
                                    'DT.Columns.Add("PESO-KGS", GetType(Double))
                                    DT.Columns.Add("PESO-KGS", GetType(String))
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
                                    'GT 21102021: el valor es alternativo, a veces esta vacio, no puede ser double
                                    'DT.Columns.Add("CBMS", GetType(String))
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

                            'If cell.Address.ColumnLetter = "R" AndAlso cell.Address.ColumnNumber = "18" Then
                            '    If Not cell.Value.ToString.ToUpper = "#POLIZA" Then
                            '        errores = True
                            '        lblprg.AppendText("Error : " & "El formato en excel No contiene el campo #POLIZA en la celda R6")
                            '        lblprg.AppendText(vbNewLine)
                            '        lblprg.Refresh()
                            '        lblprg.SelectionStart = lblprg.TextLength
                            '        lblprg.ScrollToCaret()
                            '    Else
                            '        DT.Columns.Add("#POLIZA", GetType(String))
                            '    End If
                            'End If

                            If cell.Address.ColumnLetter = "R" AndAlso cell.Address.ColumnNumber = "18" Then
                                If Not cell.Value.ToString.Trim = "TASA_CAMBIO" AndAlso Not cell.Value.ToString = "TIPO DE CAMBIO" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo TASA_CAMBIO en la celda R")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("TIPO_CAMBIO", GetType(String))
                                End If
                            End If



                            If cell.Address.ColumnLetter = "S" AndAlso cell.Address.ColumnNumber = "19" Then
                                If Not cell.Value.ToString = "UMBAS" AndAlso Not cell.Value.ToString = "UM_BAS" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo UMBAS en la celda S")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("UMBAS", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "T" AndAlso cell.Address.ColumnNumber = "20" Then
                                If Not cell.Value.ToString = "PRESENTACION" AndAlso Not cell.Value.ToString = "PRESENTACION" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo PRESENTACION en la celda T")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("PRESENTACION", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "U" AndAlso cell.Address.ColumnNumber = "21" Then
                                If Not cell.Value.ToString = "FACTOR_PRESENTACION" AndAlso Not cell.Value.ToString = "FACTOR_PRESENTACION" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo FACTOR_PRESENTACION en la celda U")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("FACTOR_PRESENTACION", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "V" AndAlso cell.Address.ColumnNumber = "22" Then
                                If Not cell.Value.ToString = "BULTOS_POR_PALLET" AndAlso Not cell.Value.ToString = "BULTOS POR PALLET" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo BULTOS_POR_PALLET en la celda V")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("BULTOS_POR_PALLET", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "W" AndAlso cell.Address.ColumnNumber = "23" Then
                                If Not cell.Value.ToString = "CLASIFICACION" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo CLASIFICACION en la celda W")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("CLASIFICACION", GetType(String))
                                End If
                            End If


                            If cell.Address.ColumnLetter = "X" AndAlso cell.Address.ColumnNumber = "24" Then
                                If Not cell.Value.ToString.Trim = "SCAN_POLIZA" AndAlso Not cell.Value.ToString = "SCAN POLIZA" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo SCAN POLIZA en la celda X")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("SCAN_POLIZA", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "Y" AndAlso cell.Address.ColumnNumber = "25" Then
                                If Not cell.Value.ToString.Trim = "SCAN_TICKET" AndAlso Not cell.Value.ToString = "SCAN TICKET" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo SCAN TICKET en la celda Y")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("SCAN_TICKET", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "Z" AndAlso cell.Address.ColumnNumber = "26" Then
                                If Not cell.Value.ToString.Trim = "NOMBRE_OPERADOR" AndAlso Not cell.Value.ToString = "NOMBRE OPERADOR" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo NOMBRE OPERADOR en la celda Z")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("NOMBRE_OPERADOR", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AA" AndAlso cell.Address.ColumnNumber = "27" Then
                                If Not cell.Value.ToString.Trim = "NIT_CONSOLIDADOR" AndAlso Not cell.Value.ToString = "NIT CONSOLIDADOR" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo NIT-CONSOLIDADOR en la celda AA")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("NIT_CONSOLIDADOR", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AB" AndAlso cell.Address.ColumnNumber = "28" Then
                                If Not cell.Value.ToString.Trim = "Numero_Orden" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Numero_Orden en la celda AB")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Numero_Orden", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AC" AndAlso cell.Address.ColumnNumber = "29" Then
                                If Not cell.Value.ToString.Trim = "Numero_DUCA" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Numero_DUCA en la celda AC")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Numero_DUCA", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AD" AndAlso cell.Address.ColumnNumber = "30" Then
                                If Not cell.Value.ToString.Trim = "Clave_aduana" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Clave_aduana en la celda AD")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Clave_aduana", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AE" AndAlso cell.Address.ColumnNumber = "31" Then
                                If Not cell.Value.ToString.Trim = "NIT_Importador" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo NIT_Importador en la celda AE")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("NIT_Importador", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AF" AndAlso cell.Address.ColumnNumber = "32" Then
                                If Not cell.Value.ToString.Trim = "Regimen" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Regimen en la celda AF")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Regimen", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AG" AndAlso cell.Address.ColumnNumber = "33" Then
                                If Not cell.Value.ToString.Trim = "Clase" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Clase en la celda AG")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Clase", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AH" AndAlso cell.Address.ColumnNumber = "34" Then
                                If Not cell.Value.ToString.Trim = "Pais_procedencia" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Pais_procedencia en la celda AH")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Pais_procedencia", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AI" AndAlso cell.Address.ColumnNumber = "35" Then
                                If Not cell.Value.ToString.Trim = "Modo_transporte" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Modo_transporte en la celda AI")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Modo_transporte", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AJ" AndAlso cell.Address.ColumnNumber = "36" Then
                                If Not cell.Value.ToString.Trim = "Tipo_cambio" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo pol_Tipo_cambio en la celda AJ")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("pol_Tipo_cambio", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AK" AndAlso cell.Address.ColumnNumber = "37" Then
                                If Not cell.Value.ToString.Trim = "Total_valor_aduana" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Total_valor_aduana  en la celda AK")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Total_valor_aduana", GetType(Double))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AL" AndAlso cell.Address.ColumnNumber = "38" Then
                                If Not cell.Value.ToString.Trim = "Total_bultos_Peso_Bruto" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Total_bultos_Peso_Bruto  en la celda AL")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Total_bultos_Peso_Bruto", GetType(Double))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AM" AndAlso cell.Address.ColumnNumber = "39" Then
                                If Not cell.Value.ToString.Trim = "TotalFOBUSD" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo TotalFOBUSD en la celda AM")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("TotalFOBUSD", GetType(Double))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AN" AndAlso cell.Address.ColumnNumber = "40" Then
                                If Not cell.Value.ToString.Trim = "Total_Flete_USD" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Total_Flete_USD en la celda AN")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Total_Flete_USD", GetType(Double))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AO" AndAlso cell.Address.ColumnNumber = "41" Then
                                If Not cell.Value.ToString.Trim = "Total_Seguro_USD" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Total_Seguro_USD en la celda AO")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Total_Seguro_USD", GetType(Double))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AP" AndAlso cell.Address.ColumnNumber = "42" Then
                                If Not cell.Value.ToString.Trim = "TotalOtrosgastosUSD" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo TotalOtrosgastosUSD en la celda AP")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("TotalOtrosgastosUSD", GetType(Double))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AQ" AndAlso cell.Address.ColumnNumber = "43" Then
                                If Not cell.Value.ToString.Trim = "Total_Liquidar" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Total_Liquidar  en la celda AQ")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Total_Liquidar ", GetType(Double))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AR" AndAlso cell.Address.ColumnNumber = "44" Then
                                If Not cell.Value.ToString.Trim = "Total_General" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Total_General en la celda X6")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Total_General", GetType(Double))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AS" AndAlso cell.Address.ColumnNumber = "45" Then
                                If Not cell.Value.ToString.Trim = "NO_POLIZA" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo NO_POLIZA en la celda X6")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("NO_POLIZA", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AT" AndAlso cell.Address.ColumnNumber = "46" Then
                                If Not cell.Value.ToString.Trim = "Fecha_Llegada" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Fecha_Llegada en la celda X6")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Fecha_Llegada", GetType(Date))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AU" AndAlso cell.Address.ColumnNumber = "47" Then
                                If Not cell.Value.ToString.Trim = "Codigo_Ubicacion" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Codigo_Ubicacion en la celda X6")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Codigo_Ubicacion", GetType(String))
                                End If
                            End If

                            If cell.Address.ColumnLetter = "AV" AndAlso cell.Address.ColumnNumber = "48" Then
                                If Not cell.Value.ToString.Trim = "Codigo_Bodega" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Codigo_Bodega en la celda X6")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Codigo_Bodega", GetType(String))
                                End If
                            End If


                            If cell.Address.ColumnLetter = "AW" AndAlso cell.Address.ColumnNumber = "49" Then
                                If Not cell.Value.ToString.Trim = "Referencia" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Referencia en la celda X6")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Referencia", GetType(String))
                                End If
                            End If


                            If cell.Address.ColumnLetter = "AX" AndAlso cell.Address.ColumnNumber = "50" Then
                                If Not cell.Value.ToString.Trim = "No_Documento_WMS" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo No_Documento_WMS en la celda X6")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("No_Documento_WMS", GetType(String))
                                End If
                            End If


                            If cell.Address.ColumnLetter = "AY" AndAlso cell.Address.ColumnNumber = "51" Then
                                If Not cell.Value.ToString.Trim = "Licencia" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel No contiene el campo Licencia en la celda AY")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Licencia", GetType(String))
                                End If
                            End If

                            '#GT04052022_0830: campo para mapear ubicaciones ocupadas
                            If cell.Address.ColumnLetter = "AZ" AndAlso cell.Address.ColumnNumber = "52" Then
                                If Not cell.Value.ToString.Trim = "Posiciones" Then
                                    errores = True
                                    lblprg.AppendText("Error : " & "El formato en excel no contiene el campo Posiciones en la celda AZ")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                Else
                                    DT.Columns.Add("Posiciones", GetType(Integer))
                                End If
                            End If

                        Next cell

                    End If

                    vIndicadorFila += 1

                ElseIf (vIndicadorFila = 2) Then

                    firstCol = row.FirstCellUsed().Address.ColumnNumber
                    lastCol = row.LastCellUsed().Address.ColumnNumber

                    DT.Rows.Add() : i = 0

                    For Each cell As IXLCell In row.Cells(firstCol, lastCol)

                        vValorCelda = cell.Value.ToString().Replace("&", "")

                        'GT04052022: se infiere que son valores númericos
                        If cell.Address.ColumnNumber = 52 _
                            OrElse cell.Address.ColumnNumber = 50 _
                            OrElse cell.Address.ColumnNumber = 36 _
                            OrElse cell.Address.ColumnNumber = 22 _
                            OrElse cell.Address.ColumnNumber = 21 _
                            OrElse cell.Address.ColumnNumber = 18 _
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

                        ElseIf cell.Address.ColumnNumber = 46 Then
                            vValorCelda = CType(vValorCelda, Date).ToShortDateString()
                        End If

                        '#GT25082023: removemos carácteres especiales antes de guardar en temporal
                        Dim vDescripcion = clsPublic.Quitar_Caracteres_No_Permitidos(vValorCelda)
                        DT.Rows(DT.Rows.Count - 1).Item(i) = vDescripcion
                        'DT.Rows(DT.Rows.Count - 1).Item(i) = vValorCelda.Trim

                        i += 1

                    Next

                    vFilaDetalle += 1

                Else
                    vIndicadorFila += 1
                End If

            Next row

            lblprg.AppendText("Validación de columnas correcta...")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If DT.Rows.Count > 0 Then

                lblprg.AppendText("Verificando lineas de detalle...")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                'GT 22102021: valida existencia de nit, presentacion, clasificación y LP duplicadas
                '#GT19072023: valida si interface genera lp auto, o no para pasar por alto la validación de LP
                If Tiene_Errores_Excel(DT, lConnection, lTransaction) Then

                    SplashScreenManager.CloseForm(False)

                    If lTransaction IsNot Nothing Then lTransaction.Rollback()

                    lblprg.AppendText("No se pudo realizar la importación del archivo, por favor revise log, e intente nuevamente.")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                Else

                    lblprg.AppendText("Creando detalle de inventario...")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    For j As Integer = 0 To DT.Rows.Count - 1

                        Dim ObjInv As New clsBeTrans_inv_inicial_excel_op_log()
                        ObjInv.No_linea = DT(j)(0)
                        ObjInv.Barra = DT(j)(1)
                        ObjInv.Codigo_producto = DT(j)(2)
                        ObjInv.Descripcion = DT(j)(3)
                        ObjInv.Nit_facturar_a = DT(j)(4)
                        ObjInv.Nit_propietario = DT(j)(5)
                        ObjInv.Propietario = DT(j)(6)
                        ObjInv.Shipper = DT(j)(7)
                        ObjInv.Pieces = DT(j)(8)
                        ObjInv.Peso_kgs = DT(j)(9)
                        ObjInv.Cbms = DT(j)(10)
                        ObjInv.Valor_aduana = DT(j)(11)
                        ObjInv.Fob = DT(j)(12)
                        ObjInv.Flete = DT(j)(13)
                        ObjInv.Seguro = DT(j)(14)
                        ObjInv.Dai = DT(j)(15)
                        ObjInv.Iva = DT(j)(16)
                        ObjInv.Tipo_cambio = DT(j)(17)
                        ObjInv.Umbas = DT(j)(18)
                        ObjInv.Presentacion = DT(j)(19)
                        ObjInv.Factor_presentacion = DT(j)(20)
                        ObjInv.Bultos_por_pallet = DT(j)(21)
                        ObjInv.Clasificacion = DT(j)(22)
                        ObjInv.Pol_scan_poliza = DT(j)(23)
                        ObjInv.Scan_ticket = DT(j)(24)
                        ObjInv.Nombre_operador = DT(j)(25)
                        ObjInv.Nit_consolidador = DT(j)(26)
                        ObjInv.Pol_numero_orden = DT(j)(27)
                        ObjInv.Pol_numero_duca = DT(j)(28)
                        ObjInv.Pol_clave_aduana = DT(j)(29)
                        ObjInv.Pol_nit_importador = DT(j)(30)
                        ObjInv.Pol_regimen = DT(j)(31)
                        ObjInv.Pol_clase = DT(j)(32)
                        ObjInv.Pol_pais_procedencia = DT(j)(33)
                        ObjInv.Pol_modo_transporte = DT(j)(34)
                        ObjInv.Pol_tipo_cambio = DT(j)(35)
                        ObjInv.Pol_total_valor_aduana = DT(j)(36)
                        ObjInv.Pol_total_peso_bruto = DT(j)(37)
                        ObjInv.Pol_totalfobusd = DT(j)(38)
                        ObjInv.Pol_total_flete_usd = DT(j)(39)
                        ObjInv.Pol_total_seguro_usd = DT(j)(40)
                        ObjInv.Pol_totalotrosgastosusd = DT(j)(41)
                        ObjInv.Pol_total_liquidar = DT(j)(42)
                        ObjInv.Pol_total_general = DT(j)(43)
                        ObjInv.Pol_codigo_poliza = DT(j)(44)
                        ObjInv.Pol_fecha_llegada = DT(j)(45)
                        ObjInv.Codigo_ubicacion = DT(j)(46)
                        ObjInv.Codigo_bodega = DT(j)(47)
                        ObjInv.Referencia = DT(j)(48)
                        '#GT25082023: si el documento de ingreso viene vacio, se deja 0 por defecto
                        'ObjInv.Id_documento_ingreso = DT(j)(49)
                        ObjInv.Id_documento_ingreso = IIf(DT(j)(48) <> "", DT(j)(48), 0)
                        ObjInv.Licencia = IIf(IsDBNull(DT(j)(50)), "", DT(j)(50))
                        'ObjInv.Consolidado = IIf(IsDBNull(DT(j)(50)), 0, DT(j)(50))
                        ObjInv.Consolidado = IIf(ObjInv.Nit_consolidador <> "" And BeBodega.Es_Bodega_Fiscal, 1, 0)
                        '#GT04052022_0918: se guardan las posiciones que ocupa la merca
                        ObjInv.Posiciones = IIf(IsDBNull(DT(j)(51)), 0, DT(j)(51))
                        '#GT25082023: agregó al log, que linea da error, para facilitar debug
                        Dim errorp As String = "Alerta_25082023: carga inventario inicial fiscal en linea: " & ObjInv.No_linea
                        clsLnLog_error_wms.Agregar_Error(errorp)
                        pListObjInv.Add(ObjInv)

                    Next j

                    SplashScreenManager.Default.SetWaitFormCaption("Guardando data...")

                    If pListObjInv IsNot Nothing AndAlso pListObjInv.Count > 0 Then

                        clsLnTrans_inv_inicial_excel_op_log.Guardar_Transaccion(pListObjInv,
                                                                                lConnection,
                                                                                lTransaction)

                        lblprg.AppendText("Data importada correctamente...")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                    lTransaction.Commit()

                    SplashScreenManager.Default.SetWaitFormCaption("Creando OC y Stock...")

                    If Not Guardar_OC() Then
                        Importar_Archivo = False
                        Exit Function
                    End If


                    SplashScreenManager.CloseForm(False)

                    XtraMessageBox.Show("Proceso completado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    'GT 03112021: Se limpian los inputs para evitar que se presione cargar nuevamente e intente sobreescribir
                    txtArchivo.Text = ""
                    Grid.DataSource = Nothing
                    cmdCargar.Enabled = True
                    cmdSalir.Enabled = True

                    Importar_Archivo = True

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            SplashScreenManager.CloseForm(False)
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()

        End Try

    End Function

    Private Function Guardar_OC() As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Guardar_OC = False

        Try

            Dim pObjEnc As New clsBeTrans_inv_inicial_excel_op_log
            Dim pObjDetalle As New List(Of clsBeTrans_inv_inicial_excel_op_log)
            Dim pBeProducto As New clsBeProducto
            Dim pBePropietario As New clsBePropietarios
            Dim IdPropietarioBodega As Integer
            'Dim pListObjOC As New List(Of clsBeTrans_oc_det)
            Dim BeTransOcDet As New clsBeTrans_oc_det
            Dim ObjInv As New DataTable
            Dim control = 0
            Dim IdOrdenCompraEnc = 0
            Dim IdPropietario As Integer = 0
            Dim vCodigoProducto As String = ""
            Dim vDescripcionProducto As String = ""
            Dim vNoLinea As Integer = 1
            Dim vNitPropietario As String = ""
            Dim vNitConsolidador As String = ""
            Dim vNombrePropietario As String = ""
            Dim BePropietario As New clsBePropietarios()
            Dim vCantidadBultos As Double = 0
            Dim vPesoKgs As Double = 0
            Dim vCBMS As Double = 0
            Dim vValorAduanaExcelQuetzales As Double = 0
            Dim vValorAduana As Double = 0
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
            Dim documento As String = ""
            Dim CodigoBodega As String = ""
            Dim consolidado As Boolean = False
            Dim No_Poliza As String = ""
            Dim No_Documento_WMS As String = ""
            Dim referencia_importacion As String = ""
            Dim BeEmbarcador As New clsBeTrans_oc_embarcador
            'GT#29042022: se volvio global, para setear en OC, RE, POL
            'Dim Fecha_llegada As DateTime

            'Parametrización de la bodega
            pIdConfiguracion = clsLnI_nav_config_enc.Get_IdConfiguracion(IdBodega, AP.IdEmpresa)

            If pIdConfiguracion = 0 Then
                Throw New Exception(String.Format("La Bodega {0} de la Empresa {1} no  tiene definida configuración para interface", AP.NomBodega, AP.NomEmpresa))
            End If

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim ObjConfigInt As New clsBeI_nav_config_enc
            ObjConfigInt = clsLnI_nav_config_enc.GetSingle(pIdConfiguracion, lConnection, lTransaction)

            ObjInv.Clear()

            ObjInv = clsLnTrans_inv_inicial_excel_op_log.Get_Filter_by_DocIngreso(lConnection,
                                                                                  lTransaction)

            Dim vContadorPrg As Integer = 0

            prg.Visible = True
            prg.Maximum = ObjInv.Rows.Count

            For fila As Integer = 0 To ObjInv.Rows.Count - 1

                CodigoBodega = IIf(IsDBNull((ObjInv(fila)(0))), "", (ObjInv(fila)(0)))
                consolidado = ObjInv(fila)(1)
                No_Poliza = ObjInv(fila)(2)
                No_Documento_WMS = ObjInv(fila)(3)
                referencia_importacion = ObjInv(fila)(4)
                Fecha_llegada = ObjInv(fila)(5)

                documento = ""
                pObjEnc = New clsBeTrans_inv_inicial_excel_op_log
                BeBodega = clsLnBodega.GetSingle_By_Codigo(CodigoBodega,
                                                           lConnection,
                                                           lTransaction)

                If Not BeBodega Is Nothing Then

                    If Not BeBodega.Es_Bodega_Fiscal Then
                        documento = No_Documento_WMS
                    Else
                        documento = No_Poliza
                    End If

                Else
                    '#EJC202201171138: Buscar en bodega área, si existe una equivalencia de bodega, por código.
                    BeBodegaArea = clsLnBodega_area.Get_Single_By_Codigo_Bodega(CodigoBodega, lConnection, lTransaction)

                    If Not BeBodegaArea Is Nothing Then

                        BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeBodegaArea.IdBodega,
                                                                     lConnection,
                                                                     lTransaction)
                        If Not BeBodega.Es_Bodega_Fiscal Then
                            documento = No_Documento_WMS
                        Else
                            documento = No_Poliza
                        End If

                    Else
                        Throw New Exception("#20220117_1127A: No se obtuvo la bodega: " & CodigoBodega & " en bodega_area.")
                    End If

                End If

                'GT 27102021: trae el encabezado
                pObjEnc = clsLnTrans_inv_inicial_excel_op_log.Get_Single_By_DocIngreso(BeBodega.Es_Bodega_Fiscal,
                                                                                       documento,
                                                                                       lConnection,
                                                                                       lTransaction)

                If Not pObjEnc Is Nothing Then

                    gRefBeOrdenCompra = New clsBeTrans_oc_enc() With {.IsNew = True}
                    gRefBeOrdenCompra.IdBodega = BeBodega.IdBodega
                    gRefBeOrdenCompra.PropietarioBodega = New clsBePropietario_bodega
                    gRefBeOrdenCompra.IdEstadoOC = clsDataContractDI.tEstadoOC.NUEVA

                    Select Case BeBodega.Es_Bodega_Fiscal

                        Case True

                            If consolidado Then
                                gRefBeOrdenCompra.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado
                            Else
                                gRefBeOrdenCompra.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Poliza_DUCA
                            End If

                        Case False

                            If No_Poliza <> "" Then
                                gRefBeOrdenCompra.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Almacén_General_Con_Póliza
                            Else
                                gRefBeOrdenCompra.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso
                            End If

                    End Select

                    'gRefBeOrdenCompra.Fecha_Creacion = Now
                    'gRefBeOrdenCompra.Fec_Agr = Now
                    gRefBeOrdenCompra.Fec_Agr = Fecha_llegada
                    gRefBeOrdenCompra.Fecha_Creacion = Fecha_llegada
                    gRefBeOrdenCompra.Hora_Creacion = Now
                    gRefBeOrdenCompra.User_Agr = AP.UsuarioAp.IdUsuario
                    gRefBeOrdenCompra.Activo = True
                    gRefBeOrdenCompra.Observacion = "Generado por Inv Inicial WMS"
                    gRefBeOrdenCompra.Procedencia = "INV_INI: " + No_Documento_WMS
                    gRefBeOrdenCompra.Referencia = referencia_importacion
                    gRefBeOrdenCompra.No_Documento_Recepcion_ERP = No_Poliza
                    gRefBeOrdenCompra.No_Marchamo = No_Documento_WMS


                    'GT 26102021: si el operador no es valido, se asigna uno por defecto valido
                    Dim gOperador As New clsBeOperador
                    gOperador.Nombres = pObjEnc.Nombre_operador
                    gOperador.IdEmpresa = AP.IdEmpresa
                    clsLnOperador.Get_Operador_By_Nombre_And_Apellido(gOperador, lConnection, lTransaction)

                    If gOperador.IdOperador = 0 Then
                        gRefBeOrdenCompra.IdOperadorBodegaDefecto = clsLnOperador_bodega.Get_IdOperadorBodega_Default(BeBodega.IdBodega, lConnection, lTransaction)
                    Else
                        gRefBeOrdenCompra.IdOperadorBodegaDefecto = clsLnOperador_bodega.Get_IdOperadorBodega_By_IdOperador(gOperador.IdOperador, Me.IdBodega, lConnection, lTransaction)
                    End If

                    pBePropietario = New clsBePropietarios()
                    pBePropietario = clsLnPropietarios.Get_Single_By_NIT(IIf(consolidado, pObjEnc.Nit_consolidador,
                                                                             pObjEnc.Nit_propietario),
                                                                             lConnection,
                                                                             lTransaction)

                    IdPropietarioBodega = 0
                    IdPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(pBePropietario.IdPropietario,
                                                                                                                        BeBodega.IdBodega,
                                                                                                                        lConnection,
                                                                                                                        lTransaction)
                    'GT24012022: si no encuentra propietario-bodega aun despues del proceso: tiene_errores
                    If IdPropietarioBodega = 0 Then
                        Guardar_OC = False
                        lblprg.AppendText("Error: No existe propietario-bodega para: " & pBePropietario.IdPropietario)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        Exit Function
                    End If

                    gRefBeOrdenCompra.PropietarioBodega.IdPropietarioBodega = IdPropietarioBodega
                    gRefBeOrdenCompra.IdPropietarioBodega = IdPropietarioBodega

                    Dim pProveedorBodega As New clsBeProveedor_bodega
                    Dim proveedor = clsLnProveedor.Existe_by_IdPropietario(pBePropietario.IdPropietario, lConnection, lTransaction)

                    pProveedorBodega = clsLnProveedor_bodega.Get_Single_By_IdBodega_AND_IdProveedor(BeBodega.IdBodega,
                                                                                                proveedor.IdProveedor,
                                                                                                          lConnection,
                                                                                                         lTransaction)

                    gRefBeOrdenCompra.ProveedorBodega = pProveedorBodega
                    gRefBeOrdenCompra.IdProveedorBodega = pProveedorBodega.IdAsignacion

                    'GT20012022: si es bodega fiscal si lleva control póliza
                    gRefBeOrdenCompra.Control_Poliza = IIf(BeBodega.Es_Bodega_Fiscal, 1, 0)

                    If clsLnTrans_oc_enc.Guarda_Trans_OC_Enc(gRefBeOrdenCompra, lConnection, lTransaction) Then

                        IdOrdenCompraEnc = gRefBeOrdenCompra.IdOrdenCompraEnc

                        'GT 29102021: guardamos cada encabezado confirmado para crear las recepciones
                        Dim pOC_enc As New clsBeTrans_oc_enc()
                        pOC_enc.IdOrdenCompraEnc = IdOrdenCompraEnc
                        pOC_enc.IdBodega = Me.IdBodega
                        pOC_enc.IdOperadorBodegaDefecto = gRefBeOrdenCompra.IdOperadorBodegaDefecto
                        ListOC_to_ReEnc.Add(pOC_enc)

                        'GT 27102021: se llena el detalle de la OC
                        gRefBeOrdenCompra.DetalleOC = New List(Of clsBeTrans_oc_det)
                        pObjDetalle = New List(Of clsBeTrans_inv_inicial_excel_op_log)
                        pObjDetalle = clsLnTrans_inv_inicial_excel_op_log.Get_All_By_DocIngreso(BeBodega.Es_Bodega_Fiscal,
                                                                                                documento,
                                                                                                lConnection,
                                                                                                lTransaction)

                        'reset al contador para iniciar nuevo IdOrdenCompraDet para otro encabezado
                        Dim vIdOrdenCompraDet As Integer = clsLnTrans_oc_det.MaxID(IdOrdenCompraEnc, lConnection, lTransaction) + 1

                        vNoLinea = 1

                        For Each linea In pObjDetalle

                            vCodigoProducto = linea.Codigo_producto
                            vDescripcionProducto = linea.Descripcion
                            vNitConsolidador = linea.Nit_consolidador
                            vNitPropietario = linea.Nit_propietario
                            vNombrePropietario = linea.Propietario
                            vCantidadBultos = linea.Pieces
                            vPesoKgs = linea.Peso_kgs
                            vCBMS = linea.Cbms
                            vValorAduanaExcelQuetzales = linea.Valor_aduana
                            vValorAduana = linea.Valor_aduana
                            vFOBDolares = linea.Fob
                            vFleteDolares = linea.Flete
                            vSeguroDolares = linea.Seguro
                            vDAIQuetzales = linea.Dai
                            vIVAQuetzalesExcel = linea.Iva
                            vNoPoliza = linea.Pol_numero_orden
                            vTasaCambio = linea.Tipo_cambio
                            vUnidadMedidaBasica = linea.Umbas
                            vNombrePresentacion = linea.Presentacion
                            vFactorPresentacion = linea.Factor_presentacion
                            vBultosPorTarima = linea.Bultos_por_pallet
                            vClasificacion = linea.Clasificacion

                            pBeProducto = New clsBeProducto()
                            pBeProducto = clsLnProducto.Get_Producto_By_Codigo(vCodigoProducto,
                                                                               BeBodega.IdBodega,
                                                                               pBePropietario.IdPropietario,
                                                                               lConnection,
                                                                               lTransaction)

                            If pBeProducto Is Nothing Then
                                Guardar_OC = False
                                lblprg.AppendText("Error: No se encontró el producto: " & vCodigoProducto)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                                Exit Function
                            End If

                            BeTransOcDet = New clsBeTrans_oc_det()
                            BeTransOcDet.IdOrdenCompraEnc = IdOrdenCompraEnc
                            BeTransOcDet.IdOrdenCompraDet = vIdOrdenCompraDet
                            BeTransOcDet.IdProductoBodega = pBeProducto.IdProductoBodega
                            BeTransOcDet.IdArancel = pBeProducto.IdArancel
                            BeTransOcDet.IdPresentacion = vIdPresentacion
                            BeTransOcDet.Presentacion.IdPresentacion = vIdPresentacion
                            BeTransOcDet.IdUnidadMedidaBasica = pBeProducto.IdUnidadMedidaBasica
                            BeTransOcDet.IdMotivoDevolucion = 0
                            BeTransOcDet.No_Linea = vNoLinea : linea.No_linea = vNoLinea
                            BeTransOcDet.Nombre_producto = pBeProducto.Nombre
                            BeTransOcDet.Nombre_presentacion = vNombrePresentacion
                            BeTransOcDet.Nombre_arancel = pBeProducto.Arancel.Nombre
                            BeTransOcDet.Porcentaje_arancel = 0
                            BeTransOcDet.Nombre_unidad_medida_basica = pBeProducto.UnidadMedida.Nombre
                            BeTransOcDet.UnidadMedida.IdUnidadMedida = pBeProducto.UnidadMedida.IdUnidadMedida

                            BeTransOcDet.Producto = pBeProducto

                            'GT 271020211143: valida que pieces/cantidad sea mayor a 0
                            If vCantidadBultos = 0 Then
                                errores = True
                                lblprg.AppendText("Error : " & "Cantidad no válida para código: " & vCodigoProducto)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                BeTransOcDet.Cantidad = vCantidadBultos
                                'BeTransOcDet.Cantidad_recibida = vCantidadBultos
                            End If

                            vFOBQuetzales = Math.Round(vFOBDolares * vTasaCambio, 6)
                            vFleteQuetzales = Math.Round(vFleteDolares * vTasaCambio, 6)
                            vSeguroQuetzales = Math.Round(vSeguroDolares * vTasaCambio, 6)
                            vValorAduanaExcelQuetzales = Math.Round(vFOBQuetzales + vFleteQuetzales + vSeguroQuetzales, 6)

                            'GT si la suma de fob, flete y seguro es 0, tomar el monto de valor_aduana original
                            If vValorAduanaExcelQuetzales = 0 Then
                                vValorAduanaExcelQuetzales = vValorAduana
                            End If

                            vCIFCalculadoDolares = Math.Round(vFOBDolares + vFleteDolares + vSeguroDolares, 6)
                            vConversionCIFDolaresAQuetzales = Math.Round((vCIFCalculadoDolares * vTasaCambio), 6)

                            'GT 28102021: con el encabezado registrado, se identifica que tipo de Doc tiene
                            If gRefBeOrdenCompra.IdTipoIngresoOC = 1 OrElse gRefBeOrdenCompra.IdTipoIngresoOC = 10 Then
                                If vConversionCIFDolaresAQuetzales > 0 Then
                                    vDiferenciaCIF = Math.Round(vValorAduanaExcelQuetzales - vConversionCIFDolaresAQuetzales, 6)
                                Else
                                    vDiferenciaCIF = 0
                                End If
                            End If


                            If vDiferenciaCIF > 0.001 Then
                                XtraMessageBox.Show("Existe una diferencia en la conversión de US$ a Q. " & vDiferenciaCIF, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                BeTransOcDet.valor_aduana = vValorAduanaExcelQuetzales
                            End If

                            'GT 28102021: con el encabezado registrado, se identifica que tipo de Doc tiene
                            If gRefBeOrdenCompra.IdTipoIngresoOC = 1 Or gRefBeOrdenCompra.IdTipoIngresoOC = 10 Then
                                Dim valores_poliza = Math.Round(vFOBQuetzales + vFleteQuetzales + vSeguroQuetzales, 6)
                                If valores_poliza = 0 Then
                                    BeTransOcDet.Costo = Math.Round((vValorAduanaExcelQuetzales) / IIf(vCantidadBultos = 0, 1, vCantidadBultos), 2)
                                Else
                                    BeTransOcDet.Costo = Math.Round((vValorAduanaExcelQuetzales + vDAIQuetzales + vIVAQuetzalesExcel) / IIf(vCantidadBultos = 0, 1, vCantidadBultos), 2)
                                End If
                            Else
                                BeTransOcDet.Costo = Math.Round((vValorAduanaExcelQuetzales + vDAIQuetzales + vIVAQuetzalesExcel) / IIf(vCantidadBultos = 0, 1, vCantidadBultos), 2)
                            End If

                            'GT si es general con o sin poliza, el iva no se suma al CIF-Q porque este, ya es el total de todo.
                            If gRefBeOrdenCompra.IdTipoIngresoOC = 1 Or gRefBeOrdenCompra.IdTipoIngresoOC = 10 Then
                                BeTransOcDet.Total_linea = Math.Round(vValorAduanaExcelQuetzales, 2)
                            Else
                                BeTransOcDet.Total_linea = Math.Round(vValorAduanaExcelQuetzales + vDAIQuetzales + vIVAQuetzalesExcel, 2)
                            End If

                            '#GT01052022_2128: se guarda campo fecha_llegada incluida en Excel, no se debe guardar la fecha del sistema 
                            BeTransOcDet.User_agr = AP.UsuarioAp.IdUsuario
                            'BeTransOcDet.Fec_agr = Now
                            BeTransOcDet.Fec_agr = Fecha_llegada
                            BeTransOcDet.User_mod = AP.UsuarioAp.IdUsuario
                            'BeTransOcDet.Fec_mod = Now
                            BeTransOcDet.Fec_mod = Fecha_llegada
                            BeTransOcDet.Activo = True

                            If vPesoKgs = 0 AndAlso ObjConfigInt.Control_peso Then
                                errores = True : vErrorLinea = True
                                lblprg.AppendText("Error : " & "Peso no definido para código: " & vCodigoProducto)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else

                                If IsNumeric(vPesoKgs) Then
                                    BeTransOcDet.Peso = Math.Round(vPesoKgs, 2)
                                    BeTransOcDet.Peso_Recibido = Math.Round(vPesoKgs, 2)
                                Else
                                    errores = True : vErrorLinea = True
                                    lblprg.AppendText("Error : " & "el valor del peso no es numérico, para código: " & vCodigoProducto)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                End If

                            End If

                            BeTransOcDet.Atributo_variante_1 = 0
                            BeTransOcDet.Codigo_Producto = pBeProducto.Codigo

                            If vFOBDolares = 0 AndAlso consolidado Then
                                errores = True : vErrorLinea = True
                                lblprg.AppendText("Error : " & "FOB no definido para código: " & vCodigoProducto)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                vErrorLinea = False
                            End If

                            If vFleteDolares = 0 AndAlso consolidado Then
                                errores = True : vErrorLinea = True
                                lblprg.AppendText("Error : " & "Flete no definido para código: " & vCodigoProducto)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                vErrorLinea = False
                            End If

                            If vSeguroDolares = 0 AndAlso consolidado Then
                                errores = True : vErrorLinea = True
                                lblprg.AppendText("Error : " & "Seguro no definido para código: " & vCodigoProducto)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                vErrorLinea = False
                            End If

                            If vIVAQuetzalesExcel = 0 AndAlso consolidado Then
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

                            If vTasaCambio = 0 AndAlso consolidado Then
                                errores = True : vErrorLinea = True
                                lblprg.AppendText("Error : " & "Tasa de cambio no definida para código: " & vCodigoProducto)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                            Else
                                vErrorLinea = False
                            End If

                            BeTransOcDet.valor_fob = Math.Round(vFOBQuetzales, 2)
                            BeTransOcDet.valor_iva = Math.Round(vIVAQuetzalesExcel, 2)
                            BeTransOcDet.valor_dai = Math.Round(vDAIQuetzales, 2)
                            BeTransOcDet.valor_seguro = Math.Round(vSeguroQuetzales, 2)
                            BeTransOcDet.valor_flete = Math.Round(vFleteQuetzales, 2)
                            BeTransOcDet.Peso_Neto = Math.Round(vPesoKgs, 2)
                            BeTransOcDet.Peso_Bruto = Math.Round(vPesoKgs, 2)

                            BeTransOcDet.IdPropietarioBodega = IdPropietarioBodega
                            BeTransOcDet.Nombre_Propietario = pBePropietario.Nombre_comercial

                            '#EJC20220224: Guardar IdEmbarcador en detalle de OC.
                            BeEmbarcador = clsLnTrans_oc_embarcador.Get_Single_By_Nombre(linea.Shipper, lConnection, lTransaction)

                            If BeEmbarcador Is Nothing Then
                                BeEmbarcador = New clsBeTrans_oc_embarcador()
                                BeEmbarcador.IdEmbarcador = clsLnTrans_oc_embarcador.MaxID(lConnection, lTransaction) + 1
                                BeEmbarcador.Codigo = BeEmbarcador.IdEmbarcador
                                BeEmbarcador.Nombre = linea.Shipper
                                clsLnTrans_oc_embarcador.Insertar(BeEmbarcador, lConnection, lTransaction)
                                BeTransOcDet.IdEmbarcador = BeEmbarcador.IdEmbarcador
                            Else
                                BeEmbarcador.IdEmbarcador = BeEmbarcador.IdEmbarcador
                            End If

                            'GT 27120211211: sino hay errores en el detalle del encabezado se crea la lista
                            If Not vErrorLinea Then
                                gRefBeOrdenCompra.DetalleOC.Add(BeTransOcDet)
                            End If

                            vNoLinea += 1 : vIdOrdenCompraDet += 1

                        Next

                        If Not errores AndAlso Not vErrorLinea Then

                            If clsLnTrans_oc_det.Guardar_Transaccion(gRefBeOrdenCompra.DetalleOC, lConnection, lTransaction) Then


                                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                                SplashScreenManager.Default.SetWaitFormCaption("Registrando Recepción...")

                                Guardar_Recepcion(gRefBeOrdenCompra,
                                                  pObjDetalle,
                                                  BeBodega.Es_Bodega_Fiscal,
                                                  lConnection,
                                                  lTransaction)

                                lblprg.AppendText("Aviso: Doc. Ingreso: " & IdOrdenCompraEnc & " creado correctamente.")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            End If

                        Else
                            Guardar_OC = False
                            lblprg.AppendText("Error: No se registró detalle de ingreso en doc ref: " & documento)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            Exit Function
                        End If

                    Else

                        errores = True
                        lblprg.AppendText("Error: No se registró doc. Ingreso para: " & No_Documento_WMS)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                    End If

                    SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                    SplashScreenManager.Default.SetWaitFormCaption("Registrando Póliza/doc...")

                    If Not errores AndAlso Not vErrorLinea Then

                        If Not String.IsNullOrEmpty(pObjEnc.Pol_codigo_poliza) Then

                            Dim BeRegimen As New clsBeRegimen_fiscal

                            'GT 271020211622: guardamos la poliza
                            Dim BePoliza As New clsBeCEALSA_DUCA_ENC()
                            BePoliza = Scan_Poliza(pObjEnc)

                            Dim BeTransOCPol As New clsBeTrans_oc_pol()
                            BeTransOCPol.IsNew = True
                            BeTransOCPol.User_agr = AP.UsuarioAp.IdUsuario
                            'BeTransOCPol.Fec_agr = Now
                            'BeTransOCPol.Fec_mod = Now
                            '#GT01052022_2134: se guarda fecha_llegada del Excel, no la fecha del sistema
                            BeTransOCPol.Fec_agr = Fecha_llegada
                            BeTransOCPol.Fec_mod = Fecha_llegada
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
                            BeTransOCPol.NoPoliza = ""
                            BeTransOCPol.Pais_procede = BePoliza.Pais_procedencia
                            BeTransOCPol.Total_valoraduana = BePoliza.Total_valor_aduana
                            BeTransOCPol.Total_bultos_Peso_Bruto = BePoliza.Total_bultos_Peso_Bruto
                            BeTransOCPol.Total_bultos_Peso_Neto = 0
                            BeTransOCPol.Total_flete = BePoliza.Total_Flete_USD
                            BeTransOCPol.Total_usd = BePoliza.TotalFOBUSD
                            BeTransOCPol.Dua = BePoliza.Numero_DUCA
                            BeTransOCPol.Tipo_cambio = BePoliza.Tipo_cambio
                            BeTransOCPol.Total_lineas = 0
                            BeTransOCPol.Total_bultos = 0
                            BeTransOCPol.Total_seguro = BePoliza.Total_Seguro_USD
                            BeTransOCPol.User_mod = AP.UsuarioAp.IdUsuario
                            BeTransOCPol.codigo_poliza = BePoliza.Codigo_Poliza
                            BeTransOCPol.ticket = 0
                            BeTransOCPol.numero_orden = BePoliza.Numero_Orden
                            'BeTransOCPol.Fecha_poliza = Now
                            BeTransOCPol.Fecha_poliza = BePoliza.Fecha_Llegada
                            BeTransOCPol.fecha_llegada = BePoliza.Fecha_Llegada
                            'BeTransOCPol.fecha_aceptacion = BePoliza.Fecha_Aceptacion
                            'BeTransOCPol.fecha_llegada = New Date(1900, 1, 1)
                            'BeTransOCPol.fecha_aceptacion = Now
                            BeTransOCPol.fecha_aceptacion = BePoliza.Fecha_Llegada
                            BeTransOCPol.total_otros = BePoliza.TotalOtrosgastosUSD
                            BeTransOCPol.clave_aduana = BePoliza.Clave_aduana_despacho_destino.Trim
                            BeTransOCPol.nit_imp_exp = BePoliza.NIT_Importador
                            BeTransOCPol.clase = BePoliza.Clase
                            'GT03022022: Se agrega el regimen.
                            BeRegimen = clsLnRegimen_fiscal.GetSingle_By_Codigo_Regimen(IIf(BePoliza.Regimen = "", "0", BePoliza.Regimen),
                                                                                                                        lConnection,
                                                                                                                        lTransaction)
                            If BeRegimen Is Nothing Then
                                '#GT04052022_1429: si no tiene regimen podria ser de general, se deja importacion definitiva (#3) 
                                'BeTransOCPol.IdRegimen = 3
                                '#GT19072023: si el regimen fiscal de la poliza no existe en WMS lanzar excepción, no inferir que es importación definitiva
                                Guardar_OC = False
                                lblprg.AppendText("Error: no esta registrado en WMS el régimen fiscal: " & BePoliza.Regimen)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                                Exit Function
                            Else
                                BeTransOCPol.IdRegimen = BeRegimen.IdRegimen
                            End If
                            BeTransOCPol.nit_imp_exp = BePoliza.NIT_Importador
                            BeTransOCPol.clave_aduana = BePoliza.Clave_aduana_despacho_destino
                            BeTransOCPol.mod_transporte = BePoliza.Modo_transporte
                            BeTransOCPol.total_liquidar = BePoliza.Total_Liquidar
                            BeTransOCPol.total_general = BePoliza.Total_General
                            clsLnTrans_oc_enc.Guarda_Trans_oc_pol(gRefBeOrdenCompra.IdOrdenCompraEnc, BeTransOCPol, lConnection, lTransaction)

                            lblprg.AppendText("Aviso: se guardo Póliza " & BeTransOCPol.codigo_poliza)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        ElseIf Not String.IsNullOrEmpty(pObjEnc.Id_documento_ingreso) Then

                            lblprg.AppendText("Aviso: Se guardó doc. ingreso " & pObjEnc.Id_documento_ingreso)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End If
                    Else

                        Guardar_OC = False
                        lblprg.AppendText("Error: No se registro ninguna Poliza/Doc de ingreso")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        Exit Function
                    End If
                Else

                    Debug.Write("Algo bien raro pasó")

                End If

                vContadorPrg += 1
                prg.Value = vContadorPrg

            Next

            'GT14122021: Eliminamos la data temporal utilizada
            clsLnTrans_inv_inicial_excel_op_log.Eliminar_Temporal(lConnection, lTransaction)

            lTransaction.Commit()

            Guardar_OC = True

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)

            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    'Private Function Guardar_Recepcion(ByVal lTransOcEnc As List(Of clsBeTrans_oc_enc),
    '                                   ByVal lBeTransInvIniExcel As List(Of clsBeTrans_inv_inicial_excel_op_log)) As Boolean

    '    Guardar_Recepcion = False
    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing

    '    Dim contador_tran_enc As Integer = 0

    '    Try


    '        lblprg.AppendText("Aviso: Inicia registro tareas recepción ")
    '        lblprg.AppendText(vbNewLine)
    '        lblprg.Refresh()
    '        lblprg.SelectionStart = lblprg.TextLength
    '        lblprg.ScrollToCaret()

    '        lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

    '        For Each pOC In lTransOcEnc

    '            contador_tran_enc += 1

    '            pObjOC = New clsBeTrans_oc_enc()
    '            pObjOC.IsNew = True
    '            pObjOC = clsLnTrans_oc_enc.Get_Orden_Compra(pOC.IdOrdenCompraEnc, lConnection, lTransaction)

    '            Llena_Rec_Enc(pObjOC, lConnection, lTransaction)
    '            Llena_Re_Det(pObjOC, lBeTransInvIniExcel, pEsBodegaFiscal, lConnection, lTransaction)
    '            Llena_Re_OC(pObjOC)
    '            Llena_Operador(pObjOC)
    '            Crea_Tarea_HH(pObjOC)
    '            Llena_Stock_REC()

    '            clsLnTrans_re_enc.Guardar_By_Import_Excel(BeTareaHH,
    '                                                       gBeRecepcionEnc,
    '                                                       gBeRecepcionEnc.OrdenCompraRec,
    '                                                       lBeTransRecDet,
    '                                                       plistBeReDetParametros,
    '                                                       pListOpe,
    '                                                       pListRecFact,
    '                                                       pListRecImgs,
    '                                                       pListBeStockSeRec,
    '                                                       pListBeStockRec,
    '                                                       lBeProdPallet,
    '                                                       gRefBeOrdenCompra.IdBodega,
    '                                                       AP.IdEmpresa,
    '                                                       AP.UsuarioAp.IdUsuario,
    '                                                       pObjOC.IdOrdenCompraEnc,
    '                                                       lResolucionesLP.IdResolucionlp,
    '                                                       pObjOC.No_Ticket_TMS,
    '                                                       lConnection, lTransaction)

    '            lblprg.AppendText("Aviso: Registrando stock... ")
    '            lblprg.AppendText(vbNewLine)
    '            lblprg.Refresh()
    '            lblprg.SelectionStart = lblprg.TextLength
    '            lblprg.ScrollToCaret()


    '        Next

    '        lTransaction.Commit()

    '        Guardar_Recepcion = True

    '        lblprg.AppendText("Aviso: Finaliza registro tareas de recepción ")
    '        lblprg.AppendText(vbNewLine)
    '        lblprg.Refresh()
    '        lblprg.SelectionStart = lblprg.TextLength
    '        lblprg.ScrollToCaret()

    '    Catch ex As Exception
    '        SplashScreenManager.CloseForm(False)
    '        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
    '    Finally
    '        SplashScreenManager.CloseForm(False)
    '        If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '        lTransaction.Dispose()
    '        lConnection.Dispose()
    '    End Try

    'End Function


    Private Function Guardar_Recepcion(ByVal pObjOC As clsBeTrans_oc_enc,
                                       ByVal pObjDetalleInvExcel As List(Of clsBeTrans_inv_inicial_excel_op_log),
                                       ByVal pEsBodegaFiscal As Boolean,
                                       ByVal lConnection As SqlConnection,
                                       ByVal lTransaction As SqlTransaction) As Boolean

        Guardar_Recepcion = False

        Dim contador_tran_enc As Integer = 0

        Try


            lblprg.AppendText("Aviso: Inicia registro tareas recepción ")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()


            contador_tran_enc += 1

            pObjOC.IsNew = True

            Llena_Rec_Enc(pObjOC, lConnection, lTransaction)
            Llena_Re_Det(pObjOC, pObjDetalleInvExcel, pEsBodegaFiscal, lConnection, lTransaction)
            Llena_Re_OC(pObjOC)
            Llena_Operador(pObjOC)
            Crea_Tarea_HH(pObjOC)
            Llena_Stock_REC(pObjOC, pObjDetalleInvExcel, gRefBeOrdenCompra.IdBodega, pEsBodegaFiscal, lConnection, lTransaction)

            clsLnTrans_re_enc.Guardar_By_Import_Excel(BeTareaHH,
                                                      gBeRecepcionEnc,
                                                      gBeRecepcionEnc.OrdenCompraRec,
                                                      lBeTransRecDet,
                                                      plistBeReDetParametros,
                                                      pListOpe,
                                                      pListRecFact,
                                                      pListRecImgs,
                                                      pListBeStockSeRec,
                                                      pListBeStockRec,
                                                      lBeProdPallet,
                                                      gRefBeOrdenCompra.IdBodega,
                                                      AP.IdEmpresa,
                                                      AP.UsuarioAp.IdUsuario,
                                                      pObjOC.IdOrdenCompraEnc,
                                                      lResolucionesLP.IdResolucionlp,
                                                      pObjOC.No_Ticket_TMS,
                                                      pObjOC,
                                                      pObjDetalleInvExcel,
                                                      lConnection,
                                                      lTransaction)

            Guardar_Recepcion = True

            lblprg.AppendText("Aviso: Finaliza registro tareas de recepción ")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
            'Finally
            '    SplashScreenManager.CloseForm(False)
        End Try

    End Function

    Private Sub Llena_Rec_Enc(ByRef pObjOC_Enc As clsBeTrans_oc_enc,
                              ByRef lConnection As SqlConnection,
                              ByRef lTransaction As SqlTransaction)

        Try

            Dim vIdUbicacionRecepcion As Integer = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(pObjOC_Enc.IdBodega,
                                                                                                             lConnection,
                                                                                                             lTransaction)

            '#GT20072023: validar que hay una ubicación de recepción o el resto del proceso estara inconsistente
            If vIdUbicacionRecepcion = 0 Then
                Throw New Exception("Error_20072023: No existe una ubicación definida para recepción en la bodega " & pObjOC_Enc.IdBodega)
            Else
                Dim pBodega_Ubicacion As New clsBeBodega_ubicacion
                pBodega_Ubicacion = clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(vIdUbicacionRecepcion,
                                                                                                   pObjOC_Enc.IdBodega,
                                                                                                           lConnection,
                                                                                                          lTransaction)

                If pBodega_Ubicacion Is Nothing Then
                    Throw New Exception("Error_20072023_A: La ubicación asignada " & vIdUbicacionRecepcion & " existe, pero no esta registrada para la bodega " & pObjOC_Enc.IdBodega)
                End If

            End If

            Dim vIdMuellePorDefectoBodega As Integer = clsLnBodega_muelles.Get_IdMuelle_Default_By_IdBodega(pObjOC_Enc.IdBodega,
                                                                                                                    lConnection,
                                                                                                                   lTransaction)

            '#GT20072023: validar que exista un muelle por defecto para la bodega
            If vIdMuellePorDefectoBodega = 0 Then
                Throw New Exception("Error_20072023: No existe un muelle definido para recepción en la bodega " & pObjOC_Enc.IdBodega)
            End If

            ' Encabezado de Recepción
            gBeRecepcionEnc = New clsBeTrans_re_enc() With {.IsNew = True}
            gBeRecepcionEnc.IdRecepcionEnc = clsLnTrans_re_enc.MaxID(lConnection, lTransaction) + 1
            gBeRecepcionEnc.PropietarioBodega = New clsBePropietario_bodega
            gBeRecepcionEnc.PropietarioBodega.IdBodega = pObjOC_Enc.IdBodega
            gBeRecepcionEnc.PropietarioBodega.IdPropietarioBodega = pObjOC_Enc.IdPropietarioBodega
            gBeRecepcionEnc.User_agr = AP.UsuarioAp.IdUsuario
            'gBeRecepcionEnc.Fec_agr = Now
            'gBeRecepcionEnc.Fec_mod = Now
            '#GT01052022_2203: guardar fecha_llegada del excel
            gBeRecepcionEnc.Fec_agr = Fecha_llegada
            gBeRecepcionEnc.Fec_mod = Fecha_llegada
            'gBeRecepcionEnc.Fecha_recepcion = Now
            gBeRecepcionEnc.Fecha_recepcion = Fecha_llegada
            gBeRecepcionEnc.Hora_ini_pc = Now
            gBeRecepcionEnc.Hora_fin_pc = Now
            gBeRecepcionEnc.Fecha_tarea = Now
            gBeRecepcionEnc.Activo = True
            gBeRecepcionEnc.Estado = "Nuevo"
            gBeRecepcionEnc.No_Marchamo = "N/A"
            gBeRecepcionEnc.IdTipoTransaccion = "HCOC00"
            gBeRecepcionEnc.IdMuelle = vIdMuellePorDefectoBodega
            gBeRecepcionEnc.IdUbicacionRecepcion = vIdUbicacionRecepcion
            gBeRecepcionEnc.Muestra_precio = True
            gBeRecepcionEnc.User_mod = AP.UsuarioAp.IdUsuario
            gBeRecepcionEnc.Tomar_fotos = False
            gBeRecepcionEnc.Escanear_rec_ubic = False
            gBeRecepcionEnc.Para_por_codigo = False
            gBeRecepcionEnc.Observacion = "Generado por Inv Inicial WMS"
            gBeRecepcionEnc.IdPiloto = 0
            gBeRecepcionEnc.IdVehiculo = 0
            gBeRecepcionEnc.Habilitar_Stock = True
            gBeRecepcionEnc.Mostrar_Cantidad_Esperada = True
            gBeRecepcionEnc.IdBodega = pObjOC_Enc.IdBodega

            lblprg.AppendText("Aviso: Se registra cabeceras de la recepción " & gBeRecepcionEnc.IdRecepcionEnc)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Llena_Re_OC(pObjOC_Enc As clsBeTrans_oc_enc)

        Try

            ' Encabezado de Recepción OC
            gBeRecepcionEnc.IsNew = True
            gBeRecepcionEnc.OrdenCompraRec.IsNew = True
            gBeRecepcionEnc.OrdenCompraRec.User_agr = AP.UsuarioAp.IdUsuario
            'gBeRecepcionEnc.OrdenCompraRec.Fec_agr = Now
            '#GT01052022_2211: guardar fecha_llegada del Excel
            gBeRecepcionEnc.OrdenCompraRec.Fec_agr = Fecha_llegada
            gBeRecepcionEnc.OrdenCompraRec.IdOrdenCompraEnc = pObjOC_Enc.IdOrdenCompraEnc
            gBeRecepcionEnc.OrdenCompraRec.Recepcion_ciega = False
            gBeRecepcionEnc.OrdenCompraRec.Recepcion_manual = False
            gBeRecepcionEnc.OrdenCompraRec.No_docto = pObjOC_Enc.No_Documento
            ' gBeRecepcionEnc.OrdenCompraRec.Hora_ini_hh = dtmHoraIhh.Value
            ' gBeRecepcionEnc.OrdenCompraRec.Hora_fin_hh = dtmHoraFhh.Value
            gBeRecepcionEnc.OrdenCompraRec.IdRecepcionEnc = gBeRecepcionEnc.IdRecepcionEnc
            gBeRecepcionEnc.OrdenCompraRec.Recepcion_ciega = 0
            gBeRecepcionEnc.OrdenCompraRec.Recepcion_manual = 0
            gBeRecepcionEnc.OrdenCompraRec.No_docto = pObjOC_Enc.No_Documento

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Crea_Tarea_HH(pObjOC_Enc As clsBeTrans_oc_enc)

        Try

            BeTareaHH = New clsBeTarea_hh

            'If gBeRecepcionEnc IsNot Nothing AndAlso gBeRecepcionEnc.IsNew Then
            'BeTareaHH.IsNew = True
            BeTareaHH.IdPropietario = pObjOC_Enc.PropietarioBodega.IdPropietario
            BeTareaHH.IdBodega = pObjOC_Enc.IdBodega
            BeTareaHH.IdMuelle = gBeRecepcionEnc.IdMuelle
            'GT 03112021: probando estados
            BeTareaHH.IdEstado = 1
            BeTareaHH.IdPrioridad = 1
            BeTareaHH.IdTipoTarea = 1
            BeTareaHH.IdTransaccion = gBeRecepcionEnc.IdRecepcionEnc
            BeTareaHH.Tipo = 0
            'BeTareaHH.FechaInicio = dtmHoraI.Value
            'BeTareaHH.FechaFin = dtmHoraF.Value
            BeTareaHH.DiaCompleto = False
            BeTareaHH.Descripcion = "Recepcion generada por Inv Inicial WMS"
            BeTareaHH.CreaTarea = True
            BeTareaHH.IsNew = True
            BeTareaHH.Asunto = "Ingreso con Orden de Compra"

            'Select Case gBeRecepcionEnc.IdTipoTransaccion.ToString()
            '    Case "HSOC00"
            '        BeTareaHH.Asunto = "Ingreso sin Orden de Compra "
            '    Case "HSOD00"
            '        BeTareaHH.Asunto = "Ingreso de Devolución sin referencia"
            '    Case "HCOC00"
            '        BeTareaHH.Asunto = "Ingreso con Orden de Compra"
            '    Case "HCOD00"
            '        BeTareaHH.Asunto = "Devolución de Pedido"
            '    Case "HHSR00"
            '        BeTareaHH.Asunto = "Ingreso sin referencia"
            '    Case "PICH000"
            '        BeTareaHH.Asunto = "Pre-ingreso con HH"
            '    Case Else
            '        Exit Select
            'End Select

            'End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Llena_Re_Det(ByVal pObjOC_Enc As clsBeTrans_oc_enc,
                             ByVal lBeTransInvIniExcel As List(Of clsBeTrans_inv_inicial_excel_op_log),
                             ByVal pEsBodegaFiscal As Boolean,
                             ByRef lConnection As SqlConnection,
                             ByRef lTransaction As SqlTransaction)

        Try

            Dim lPesoPresentacion As Double = 0.0
            lBeTransRecDet = New List(Of clsBeTrans_re_det)
            lBeProdPallet = New List(Of clsBeProducto_pallet)
            Dim vIndice As Integer = 0
            Dim vlBeStockRec As New List(Of clsBeStock_rec)
            Dim contador_transacciones As Integer = 0
            Dim BeTransOcDet As New clsBeTrans_oc_det
            Dim BeDetalleInvExcelIni As New clsBeTrans_inv_inicial_excel_op_log
            Dim BeProductoEstado As New clsBeProducto_estado
            Dim lLicenciasProcesadas As New List(Of String)
            Dim vLicencia As String = ""
            Dim vIdPropietario As Integer = 0
            Dim vIdMaxRecepcionDet As Integer = 0

            '#GT19072023: requiero validar si genera LP o no para el proceso de guardar detalle en recepción
            Dim objInterface As New clsBeI_nav_config_enc
            objInterface = clsLnI_nav_config_enc.GetSingle(pIdConfiguracion, lConnection, lTransaction)

            For Each OcDet In pObjOC_Enc.DetalleOC

                If Not pEsBodegaFiscal Then

                    BeDetalleInvExcelIni = lBeTransInvIniExcel.Find(Function(x) x.Id_documento_ingreso = CInt(pObjOC_Enc.No_Marchamo) _
                                                                           AndAlso x.Codigo_producto = OcDet.Codigo_Producto _
                                                                           AndAlso x.No_linea = OcDet.No_Linea)
                Else

                    BeDetalleInvExcelIni = lBeTransInvIniExcel.FindAll(Function(x) x.Pol_codigo_poliza = pObjOC_Enc.No_Documento_Recepcion_ERP _
                                                                AndAlso x.Codigo_producto = OcDet.Codigo_Producto _
                                                                AndAlso x.No_linea = OcDet.No_Linea).FirstOrDefault
                End If

                vIdPropietario = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(pObjOC_Enc.IdBodega,
                                                                                                           OcDet.IdPropietarioBodega,
                                                                                                           lConnection,
                                                                                                           lTransaction)

                BeProductoEstado = clsLnProducto_estado.Get_Buen_Estado_Porducto_By_IdPropietario_And_IdBodegaHH(vIdPropietario,
                                                                                                                 pObjOC_Enc.IdBodega,
                                                                                                                 lConnection,
                                                                                                                 lTransaction)

                If BeProductoEstado Is Nothing Then
                    Throw New Exception("Error_EJC20211105: No se encontró el estado de producto para la línea de detalle.")
                End If


                If Not AP.Bodega.industria_motriz Then

                    If Not objInterface.Genera_lp Then

                        If Not BeDetalleInvExcelIni Is Nothing Then
                            vLicencia = BeDetalleInvExcelIni.Licencia
                        End If

                        If lLicenciasProcesadas.Contains(vLicencia) Then
                            Continue For
                        End If

                    End If

                End If

                BeTransReDet = New clsBeTrans_re_det()
                BeTransReDet.IsNew = True
                BeTransReDet.IdRecepcionEnc = gBeRecepcionEnc.IdRecepcionEnc
                BeTransReDet.IdRecepcionDet = vIdMaxRecepcionDet
                'GT07042022: Encontrar el IdOrdenCompraEnc y Det 
                BeTransReDet.IdOrdenCompraEnc = OcDet.IdOrdenCompraEnc
                BeTransReDet.IdOrdenCompraDet = OcDet.IdOrdenCompraDet
                BeTransReDet.Codigo_Producto = OcDet.Codigo_Producto
                BeTransReDet.Producto.IdProducto = OcDet.Producto.IdProducto
                BeTransReDet.Producto.Codigo = OcDet.Codigo_Producto
                BeTransReDet.IdProductoBodega = OcDet.IdProductoBodega
                BeTransReDet.IdPresentacion = OcDet.IdPresentacion
                BeTransReDet.UnidadMedida = New clsBeUnidad_medida
                BeTransReDet.UnidadMedida.IdUnidadMedida = OcDet.IdUnidadMedidaBasica
                BeTransReDet.IdUnidadMedida = OcDet.IdUnidadMedidaBasica
                BeTransReDet.Lic_plate = vLicencia
                BeTransReDet.ProductoEstado = New clsBeProducto_estado
                BeTransReDet.ProductoEstado.IdEstado = BeProductoEstado.IdEstado
                BeTransReDet.IdProductoEstado = BeProductoEstado.IdEstado
                BeTransReDet.IdOperadorBodega = pObjOC_Enc.IdOperadorBodegaDefecto
                BeTransReDet.MotivoDevolucion = New clsBeMotivo_devolucion
                BeTransReDet.No_Linea = OcDet.No_Linea
                BeTransReDet.cantidad_recibida = OcDet.Cantidad
                BeTransReDet.Nombre_producto = OcDet.Nombre_producto
                BeTransReDet.Nombre_presentacion = OcDet.Nombre_presentacion
                BeTransReDet.Nombre_unidad_medida = OcDet.Nombre_unidad_medida_basica
                BeTransReDet.Nombre_producto_estado = BeProductoEstado.Nombre
                BeTransReDet.IdPropietarioBodega = OcDet.IdPropietarioBodega
                'BeTransReDet.Fecha_ingreso = Now
                '#GT01052022_2205: guardar fecha_llegada del excel
                BeTransReDet.Fecha_ingreso = Fecha_llegada
                BeTransReDet.Peso = OcDet.Peso
                BeTransReDet.Peso_Estadistico = 0
                BeTransReDet.Peso_Minimo = 0
                BeTransReDet.Peso_Maximo = 0
                BeTransReDet.Aniada = 0
                BeTransReDet.Costo_Oc = OcDet.Costo
                BeTransReDet.Costo = OcDet.Costo
                BeTransReDet.User_agr = AP.UsuarioAp.IdUsuario
                BeTransReDet.Fec_agr = Now
                BeTransReDet.Observacion = "Importación inv. inicial WMS"
                BeTransReDet.Atributo_Variante_1 = ""
                BeTransReDet.Pallet_No_Estandar = 0
                lBeTransRecDet.Add(BeTransReDet) : If vLicencia <> "" Then lLicenciasProcesadas.Add(vLicencia)

                lblprg.AppendText("Aviso: Se prepara detalle de la recepción " & BeTransReDet.IdRecepcionDet)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                If lBeTransRecDet.Count = 0 Then
                    Throw New Exception("La recepción no tiene detalle")
                End If

                vIdMaxRecepcionDet += 1

                '#EJC20220502:Si el producto se importa con peso, pero no tiene control por peso, actualizar la bandera a control peso = true.
                If Not OcDet.Producto.Control_peso AndAlso OcDet.Peso > 0 Then
                    OcDet.Producto.Control_peso = True
                    clsLnProducto.Actualizar_Control_Peso(OcDet.Producto, lConnection, lTransaction)
                End If

            Next

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Llena_Operador(pObjOC_Enc As clsBeTrans_oc_enc)
        Try

            Dim Operador As New clsBeTrans_re_op()
            pListOpe = New List(Of clsBeTrans_re_op)

            Operador.IsNew = True
            'Operador.IdOperadorRec = pObjOC_Enc.IdOperadorBodegaDefecto
            Operador.IdOperadorRec = 0
            Operador.IdRecepcionEnc = gBeRecepcionEnc.IdRecepcionEnc
            Operador.IdOperadorBodega = pObjOC_Enc.IdOperadorBodegaDefecto
            Operador.User_agr = AP.UsuarioAp.IdUsuario
            'Operador.Fec_agr = Now
            'Operador.Fec_mod = Now
            '#GT01052022_2212: guardar fecha_llegada del excel
            Operador.Fec_agr = Fecha_llegada
            Operador.Fec_mod = Fecha_llegada
            Operador.User_mod = AP.UsuarioAp.IdUsuario
            pListOpe.Add(Operador)

            lblprg.AppendText("Aviso: Se prepara operador de la recepción " & Operador.IdOperadorBodega)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Llena_Stock_REC(ByVal pObjOC_Enc As clsBeTrans_oc_enc, ByVal lBeTransInvIniExcel As List(Of clsBeTrans_inv_inicial_excel_op_log),
                                ByVal IdBodega As Integer, ByVal pEsBodegaFiscal As Boolean,
                                                                                                ByRef lConnection As SqlConnection,
                                                                                                ByRef lTransaction As SqlTransaction)
        Try

            pListBeStockRec = New List(Of clsBeStock_rec)
            Dim pBeInvExcelIni As New clsBeTrans_inv_inicial_excel_op_log

            For Each detalle In lBeTransRecDet

                'GT18012022: se obtiene la ubicación indicada en la data importada del Excel
                If Not pEsBodegaFiscal Then

                    pBeInvExcelIni = lBeTransInvIniExcel.Find(Function(x) x.Id_documento_ingreso = CInt(pObjOC_Enc.No_Marchamo) _
                                                                            AndAlso x.Codigo_producto = detalle.Codigo_Producto _
                                                                            AndAlso x.Licencia = detalle.Lic_plate)
                Else

                    pBeInvExcelIni = lBeTransInvIniExcel.FindAll(Function(x) x.Pol_codigo_poliza = pObjOC_Enc.No_Documento_Recepcion_ERP _
                                                                                AndAlso x.Codigo_producto = detalle.Codigo_Producto _
                                                                                AndAlso x.Licencia = detalle.Lic_plate).FirstOrDefault
                End If


                If Not pBeInvExcelIni Is Nothing Then
                    'GT18012022: Se valida que exista la ubicación (sea por idubicación o por código)
                    If (pBeInvExcelIni.Codigo_ubicacion = "PREDIOABANDONO") Then
                        Debug.WriteLine(pBeInvExcelIni.Codigo_ubicacion)
                    End If
                    Dim bodega_ubic = clsLnBodega_ubicacion.Exists(pBeInvExcelIni.Codigo_ubicacion, IdBodega, lConnection, lTransaction)
                    Dim pStockRec As New clsBeStock_rec()
                    pStockRec.IsNew = True
                    pStockRec.IdPropietarioBodega = gRefBeOrdenCompra.IdPropietarioBodega
                    pStockRec.IdProductoBodega = detalle.IdProductoBodega
                    pStockRec.IdProductoEstado = detalle.IdProductoEstado
                    pStockRec.ProductoEstado.IdEstado = detalle.IdProductoEstado
                    pStockRec.IdPresentacion = detalle.IdPresentacion
                    pStockRec.IdUnidadMedida = detalle.IdUnidadMedida
                    pStockRec.IdUbicacion = IIf(bodega_ubic = 0, gBeRecepcionEnc.IdUbicacionRecepcion, bodega_ubic)  'Esto hay que ubicarlo, en la localidad del excel (segun comparativo de la bodega de wms con la bodega actual)
                    pStockRec.IdUbicacion_anterior = IIf(bodega_ubic = 0, gBeRecepcionEnc.IdUbicacionRecepcion, bodega_ubic)
                    pStockRec.IdRecepcionEnc = detalle.IdRecepcionEnc
                    pStockRec.IdRecepcionDet = detalle.IdRecepcionDet
                    pStockRec.Lote = ""
                    pStockRec.Lic_plate = detalle.Lic_plate
                    pStockRec.Serial = ""
                    pStockRec.Cantidad = detalle.cantidad_recibida
                    'pStockRec.Fecha_Ingreso = Now
                    'pStockRec.Fec_agr = Now
                    'pStockRec.Fec_mod = Now
                    '#GT01052022_2218: guardar fecha_llegada del excel
                    pStockRec.Fecha_regularizacion = detalle.Fecha_ingreso
                    pStockRec.Fecha_Ingreso = detalle.Fecha_ingreso
                    pStockRec.Fec_agr = detalle.Fecha_ingreso
                    pStockRec.Fec_mod = detalle.Fecha_ingreso
                    pStockRec.User_agr = AP.UsuarioAp.IdUsuario
                    pStockRec.User_mod = AP.UsuarioAp.IdUsuario
                    pStockRec.Activo = True
                    pStockRec.Peso = detalle.Peso
                    pStockRec.Temperatura = 0
                    pStockRec.No_linea = detalle.No_Linea
                    pStockRec.IdBodega = AP.IdBodega
                    pStockRec.Pallet_No_Estandar = IIf(detalle.Posiciones > 0, True, False)
                    pListBeStockRec.Add(pStockRec)

                Else
                    lblprg.AppendText("Error: Dato no valido para buscar úbicación para Stock " & detalle.IdRecepcionEnc)
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()
                    Return
                End If

            Next

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Function Tiene_Errores_Excel(ByRef pDT As DataTable,
                                         Optional ByVal pConection As SqlConnection = Nothing,
                                         Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Tiene_Errores_Excel = False
        Dim max As Integer
        Dim ObjPBod As New clsBeProducto_bodega
        Dim lBodegas As New List(Of clsBeBodega)
        Dim Contador As Integer = 0
        Dim ObjConfigInt As New clsBeI_nav_config_enc

        Try


            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Validando catálogos...")

            Cursor = Cursors.WaitCursor

            Dim fila = 2

            errores = False

            pIdConfiguracion = clsLnI_nav_config_enc.Get_IdConfiguracion(IdBodega, AP.IdEmpresa)

            If pIdConfiguracion = 0 Then

                XtraMessageBox.Show(String.Format("La Bodega {0} de la Empresa {1} no  tiene definida configuración para interface", AP.NomBodega, AP.NomEmpresa),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
                Tiene_Errores_Excel = True
                Return True

            Else
                ObjConfigInt = New clsBeI_nav_config_enc
                ObjConfigInt = clsLnI_nav_config_enc.GetSingle(pIdConfiguracion, pConection, pTransaction)
            End If

            Dim vCodigoProducto As String = ""
            Dim vCodigoBarra As String = ""
            Dim vDescripcionProducto As String = ""
            Dim vNoLinea As Integer = 0
            Dim vNitFacturarA As String = ""
            Dim vNitPropietario As String = ""
            Dim vNombrePropietario As String = ""
            Dim BePropietario As New clsBePropietarios()
            Dim BeProducto As New clsBeProducto
            Dim lObjP As New List(Of clsBeProducto)
            Dim BeBodega As New clsBeBodega
            Dim vCosto As Double = 0.00
            Dim vUnidadMedidaBasica As String = ""
            Dim vNombrePresentacion As String = ""
            Dim vFactorPresentacion As Double = 0.00
            Dim vBultosPorTarima As Double = 0.00
            Dim vClasificacion As String = ""
            Dim vNitConsolidador As String = ""
            Dim vConsolidado As Integer = 0
            Dim vNombreOperador As String = ""
            Dim vBodega As String = ""
            Dim vPeso_ref As Double = 0.00
            Dim vProductoBodega As New clsBeProducto_bodega
            Dim vLP As String = ""
            Dim lLicenciasProcesadas As New List(Of String)
            Dim vPosiciones As Integer = 0
            Dim objP As New clsBePropietarios

            '#EJC20210901: llenar bodegas (activas) disponibles para asociar el producto_bodega en importación de excel. método tiene_errores_excel.
            lBodegas = clsLnBodega.GetAll(pConection, pTransaction)

            Dim vContadorPrg As Integer = 0

            prg.Visible = True
            prg.Maximum = pDT.Rows.Count

            For Each r As DataRow In pDT.Rows

                vNoLinea = IIf(IsDBNull(r.Item("NO_LINEA")), 0, r.Item("NO_LINEA"))
                vCodigoProducto = IIf(IsDBNull(r.Item("CODIGO_PRODUCTO")), "", r.Item("CODIGO_PRODUCTO"))
                vCodigoBarra = IIf(IsDBNull(r.Item("CODIGO_BARRA")), "", r.Item("CODIGO_BARRA"))
                vDescripcionProducto = IIf(IsDBNull(r.Item("DESCRIPCION")), "", r.Item("DESCRIPCION"))
                vUnidadMedidaBasica = IIf(IsDBNull(r.Item("UMBAS")), "", r.Item("UMBAS"))
                vNombrePresentacion = IIf(IsDBNull(r.Item("PRESENTACION")), "", r.Item("PRESENTACION"))
                vFactorPresentacion = IIf(IsDBNull(r.Item("FACTOR_PRESENTACION")), 0, r.Item("FACTOR_PRESENTACION"))
                vBultosPorTarima = IIf(IsDBNull(r.Item("BULTOS_POR_PALLET")), 0, r.Item("BULTOS_POR_PALLET"))
                vNombreOperador = IIf(IsDBNull(r.Item("NOMBRE_OPERADOR")), 0, r.Item("NOMBRE_OPERADOR"))
                vClasificacion = IIf(IsDBNull(r.Item("CLASIFICACION")), "", r.Item("CLASIFICACION"))
                vNitFacturarA = IIf(IsDBNull(r.Item("NIT_FACTURAR_A")), "", r.Item("NIT_FACTURAR_A"))
                vNitFacturarA = vNitFacturarA.Replace("-", "")
                vNitPropietario = IIf(IsDBNull(r.Item("NIT_PROPIETARIO")), "", r.Item("NIT_PROPIETARIO"))
                vNitPropietario = vNitPropietario.Replace("-", "")
                vNombrePropietario = IIf(IsDBNull(r.Item("PROPIETARIO")), "", r.Item("PROPIETARIO"))
                vNitConsolidador = IIf(IsDBNull(r.Item("NIT_CONSOLIDADOR")), "", r.Item("NIT_CONSOLIDADOR"))
                vNitConsolidador = vNitConsolidador.Replace("-", "")
                'GT 26102021: identifica si la mercaderia de la poliza o doc de ingreso es consolidada para validar nit_consolidador y nit_propietario
                vBodega = r.Item("Codigo_Bodega")
                vPeso_ref = r.Item("PESO-KGS")
                'vLP = r.Item("Licencia")
                vLP = IIf(IsDBNull(r.Item("Licencia")), "", r.Item("Licencia"))
                vPosiciones = IIf(IsDBNull(r.Item("Posiciones")), 0, r.Item("Posiciones"))

                SplashScreenManager.Default.SetWaitFormCaption("Procesando fila: " & fila)
                Application.DoEvents()


                BeBodega = New clsBeBodega
                BeBodega = clsLnBodega.GetSingle_By_Codigo(vBodega,
                                                           pConection,
                                                           pTransaction)

                If Not BeBodega Is Nothing Then
                    vConsolidado = IIf(vNitConsolidador <> "" And BeBodega.Es_Bodega_Fiscal, 1, 0)
                Else

                    Dim BeBodegaArea = clsLnBodega_area.Get_Single_By_Codigo_Bodega(vBodega,
                                                                             pConection,
                                                                             pTransaction)

                    If Not BeBodegaArea Is Nothing Then

                        BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeBodegaArea.IdBodega,
                                                                     pConection,
                                                                     pTransaction)

                        vConsolidado = IIf(vNitConsolidador <> "" And BeBodega.Es_Bodega_Fiscal, 1, 0)

                    Else
                        Throw New Exception("#20220117_1127A: No se obtuvo la bodega por código: " & vBodega & " en WMS.")
                    End If

                End If

                'If Not Val(vNoLinea) = 0 AndAlso Not vCodigoProducto = "" AndAlso Not vDescripcionProducto = "" AndAlso Not vNitPropietario = "" Then
                If Not Val(vNoLinea) = 0 AndAlso Not vCodigoProducto = "" Then

                    If (vConsolidado) Then

                        If (vNitConsolidador <> "") Then

                            Dim vBePropietario As clsBePropietarios = clsLnPropietarios.Get_Single_By_NIT(vNitConsolidador,
                                                                                                          pConection,
                                                                                                          pTransaction)

                            If vBePropietario Is Nothing Then
                                errores = True
                                Tiene_Errores_Excel = True
                                lblprg.AppendText("Error : En fila " & fila & ", no se puede importar al consolidador con nit " & vNitConsolidador & " porque no existe en WMS.")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                                Exit Function
                            End If

                        Else
                            Tiene_Errores_Excel = True
                            errores = True
                            Tiene_Errores_Excel = True
                            lblprg.AppendText("Error : En fila " & fila & ", el consolidador no tiene un nit definido.")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, " el consolidador no tiene un nit definido."))
                            Return True
                        End If

                    End If

                    If (vNitFacturarA <> "") Then

                        Dim vBePropietario As clsBePropietarios = clsLnPropietarios.Get_Single_By_NIT(vNitFacturarA.Trim, pConection, pTransaction)

                        If vBePropietario Is Nothing Then

                            'errores = True
                            'Tiene_Errores_Excel = True
                            'lblprg.AppendText("Error : En fila " & fila & ", no se puede importar el NIT_FACTURAR_A: " & vNitFacturarA & " porque no existe en WMS.")
                            'lblprg.AppendText(vbNewLine)
                            'lblprg.Refresh()
                            'lblprg.SelectionStart = lblprg.TextLength
                            'lblprg.ScrollToCaret()
                            'Exit Function


                            '#GT09082023: mejora, se inserta el propietario en el proceso de importar inv inicial para bodega fiscal
                            Dim pIdPropietario As Integer = 0
                            'Dim objP As New clsBePropietarios
                            objP = New clsBePropietarios
                            pIdPropietario = clsLnPropietarios.MaxID(pConection, pTransaction)
                            objP.IdPropietario = pIdPropietario
                            objP.IdEmpresa = AP.IdEmpresa
                            objP.IdTipoActualizacionCosto = 1
                            objP.Contacto = clsPublic.Quitar_Caracteres_No_Permitidos(vNombrePropietario)
                            objP.Nombre_comercial = clsPublic.Quitar_Caracteres_No_Permitidos(vNombrePropietario)
                            objP.Telefono = ""
                            objP.Direccion = "CIUDAD"
                            objP.Activo = 1
                            objP.User_agr = AP.UsuarioAp.IdUsuario
                            objP.Fec_agr = Now
                            objP.User_mod = AP.UsuarioAp.IdUsuario
                            objP.Fec_mod = Now
                            objP.Email = ""
                            objP.Actualiza_costo_oc = 1
                            objP.Color = 0
                            objP.Codigo = pIdPropietario
                            objP.Sistema = 0
                            objP.NIT = vNitFacturarA.Trim

                            Try
                                clsLnPropietarios.Insertar(objP, pConection, pTransaction)
                            Catch ex As Exception
                                errores = True
                                Throw New Exception("Error al insertar nuevo propietario: " & objP.Nombre_comercial & " " & ex.Message)
                            End Try

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("Aviso: Se insertó nuevo propietario: " & objP.Nombre_comercial)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            '#GT09082023: luego de insertar al propietario, se agrega un producto-estado del propietario.
                            Dim pProducto_estado As New clsBeProducto_estado
                            Dim IdProductoEstado As Integer = 0

                            IdProductoEstado = clsLnProducto_estado.MaxID(pConection, pTransaction) + 1
                            pProducto_estado.IdEstado = IdProductoEstado
                            pProducto_estado.IdPropietario = objP.IdPropietario
                            pProducto_estado.Nombre = "Buen Estado"
                            pProducto_estado.IdUbicacionDefecto = 0
                            pProducto_estado.Utilizable = 1
                            pProducto_estado.Activo = 1
                            pProducto_estado.User_agr = AP.UsuarioAp.IdUsuario
                            pProducto_estado.Fec_agr = Now
                            pProducto_estado.User_mod = AP.UsuarioAp.IdUsuario
                            pProducto_estado.Fec_mod = Now
                            pProducto_estado.Dañado = 0
                            pProducto_estado.Sistema = 0
                            pProducto_estado.IdUbicacionBodegaDefecto = 0

                            Try
                                clsLnProducto_estado.Insertar(pProducto_estado, pConection, pTransaction)
                            Catch ex As Exception
                                errores = True
                                Throw New Exception("Error al insertar estado-producto para: " & pProducto_estado.Nombre & " " & ex.Message)
                            End Try


                            '#GT10082023: valida la inserción cliente para pedidos, sino existe interface de importación
                            Dim objCliente As New clsBeCliente
                            Dim pCliente As New clsBeCliente
                            Dim vContadorCliente As Integer = 0

                            pCliente = clsLnCliente.Get_Cliente_By_IdPropietario(objP.IdPropietario, pConection, pTransaction)

                            If pCliente Is Nothing Then
                                vContadorCliente = clsLnCliente.MaxID(pConection, pTransaction) + 1
                                objCliente.IdCliente = vContadorCliente
                                objCliente.IdEmpresa = AP.IdEmpresa
                                objCliente.IdPropietario = objP.IdPropietario
                                objCliente.IdTipoCliente = 18 'GT13082023: este valor aplica solo para CLC, para otra importación tipo fiscal, validar primero.
                                objCliente.IdUbicacionManufactura = 0
                                objCliente.Codigo = vContadorCliente
                                objCliente.Nombre_comercial = clsPublic.Quitar_Caracteres_No_Permitidos(objP.Nombre_comercial.Trim)
                                objCliente.Nombre_contacto = ""
                                objCliente.Telefono = ""
                                objCliente.Nit = objP.NIT.Trim
                                objCliente.Direccion = "Ciudad"
                                objCliente.Correo_electronico = ""
                                objCliente.Activo = 1
                                objCliente.Realiza_manufactura = 0
                                objCliente.User_agr = AP.UsuarioAp.IdUsuario
                                objCliente.Fec_agr = Now
                                objCliente.User_mod = AP.UsuarioAp.IdUsuario
                                objCliente.Fec_mod = Now
                                objCliente.Despachar_lotes_completos = 0
                                objCliente.Sistema = 0
                                objCliente.Es_bodega_recepcion = 0
                                objCliente.Es_Bodega_Traslado = 0
                                objCliente.IdUbicacionVirtual = 0
                                objCliente.Referencia = ""
                                objCliente.Control_Ultimo_Lote = 0
                                objCliente.Control_Calidad = 0
                                objCliente.IdUbicacionAbastecerCon = 0

                                Try
                                    clsLnCliente.Insertar(objCliente, pConection, pTransaction)
                                Catch ex As Exception
                                    errores = True
                                    Throw New Exception("Error al insertar cliente para: " & objCliente.Nombre_comercial & " " & ex.Message)
                                End Try

                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText("Aviso: Se insertó nuevo cliente: " & objCliente.Nombre_comercial)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            End If


                        End If

                    Else
                        errores = True
                        Tiene_Errores_Excel = True
                        lblprg.AppendText("Error : En fila " & fila & " NIT FACTURAR A esta vacio.")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        Exit Function
                    End If

                    If (vNitPropietario <> "") Then

                        BePropietario = clsLnPropietarios.Get_Single_By_NIT(vNitPropietario, pConection, pTransaction)

                        If BePropietario Is Nothing Then
                            errores = True
                            Tiene_Errores_Excel = True
                            lblprg.AppendText("Error : En fila " & fila & ", no se puede importar el NIT PROPIETARIO: " & vNitPropietario & " porque no existe en WMS.")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            Exit Function

                        End If

                    Else
                        errores = True
                        Tiene_Errores_Excel = True
                        lblprg.AppendText("Error : En fila " & fila & ", NIT PROPIETARIO esta vacio.")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        Exit Function
                    End If

                    If vCodigoProducto <> "" Then

                        If Not BePropietario Is Nothing Then

                            Contador += 1
                            BeProducto = New clsBeProducto
                            lObjP = New List(Of clsBeProducto)

                            lObjP = clsLnProducto.Exist_By_Codigo(vCodigoProducto.Trim,
                                                                  pConection,
                                                                  pTransaction)

                            If Not lObjP Is Nothing Then

                                '#GT24062022_1150: FirstOrDefault retorna nothing sino encuentra match, y no se usa idpropietario
                                '#GT27062022_0920: si producto existe no se guarda, solo se actualizan producto-bodega
                                'BeProducto = lObjP.First(Function(x) x.IdPropietario = BePropietario.IdPropietario
                                BeProducto = lObjP.FirstOrDefault(Function(x) x.Codigo = vCodigoProducto.Trim)

                                If Not BeProducto Is Nothing Then

                                    If Not BeProducto.Activo Then
                                        errores = True
                                        lblprg.AppendText("Error : " & " El producto " & vCodigoProducto & " ya está registrado pero se encuentra inactivo.")
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                    Else

                                        '#GT24062022_09098: si se guarda producto nuevo se valida la relación producto-bodega
                                        Dim ObjProdBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(BeProducto.Codigo, IdBodega,
                                                                                        pConection,
                                                                                        pTransaction)
                                        If ObjProdBodega Is Nothing Then

                                            vProductoBodega = New clsBeProducto_bodega
                                            vProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(pConection, pTransaction) + 1
                                            vProductoBodega.IdProducto = BeProducto.IdProducto
                                            vProductoBodega.IdBodega = IdBodega
                                            vProductoBodega.Activo = 1
                                            vProductoBodega.Sistema = 0
                                            vProductoBodega.User_agr = AP.UsuarioAp.IdUsuario
                                            vProductoBodega.Fec_agr = Now
                                            vProductoBodega.User_mod = AP.UsuarioAp.IdUsuario
                                            vProductoBodega.Fec_mod = Now
                                            clsLnProducto_bodega.Insertar(vProductoBodega, pConection, pTransaction)

                                            lblprg.AppendText("Aviso: Se insertó la relación de producto-bodega para el código: " & vCodigoProducto)
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        End If

                                    End If

                                End If

                            End If

                            'GT24012022: si lista vacia o, el producto vacio, se inserta
                            If lObjP Is Nothing OrElse BeProducto Is Nothing Then

                                '#GT02052022_1009: si el producto no existe, antes de crear, valida que tenga descripción
                                If vDescripcionProducto <> "" Then
                                    BeProducto = New clsBeProducto
                                    'max = clsLnProducto.MaxID(pConection, pTransaction) + Contador
                                    max = clsLnProducto.MaxID(pConection, pTransaction) + 1
                                    BeProducto.IsNew = True
                                    BeProducto.IdProducto = max
                                    BeProducto.Codigo = vCodigoProducto

                                    'GT 25102021: si hay umbas se valida, o se crea
                                    If vUnidadMedidaBasica <> "" Then

                                        Dim ObjUM = clsLnUnidad_medida.Existe_By_Nombre_By_IdPropietario(vUnidadMedidaBasica,
                                                                                                         BePropietario.IdPropietario,
                                                                                                         pConection,
                                                                                                         pTransaction)

                                        If Not ObjUM Is Nothing Then
                                            BeProducto.UnidadMedida.IdUnidadMedida = ObjUM.IdUnidadMedida
                                            BeProducto.IdUnidadMedidaBasica = ObjUM.IdUnidadMedida
                                        Else

                                            ObjUM = New clsBeUnidad_medida
                                            'ObjUM.IdUnidadMedida = clsLnUnidad_medida.MaxID(pConection, pTransaction) + Contador
                                            ObjUM.IdUnidadMedida = clsLnUnidad_medida.MaxID(pConection, pTransaction) + 1
                                            ObjUM.Nombre = clsPublic.Quitar_Caracteres_No_Permitidos(vUnidadMedidaBasica)
                                            ObjUM.Codigo = clsPublic.Quitar_Caracteres_No_Permitidos(vUnidadMedidaBasica)
                                            ObjUM.Activo = 1
                                            ObjUM.IdPropietario = BePropietario.IdPropietario
                                            ObjUM.IsNew = True
                                            ObjUM.User_agr = AP.UsuarioAp.IdUsuario
                                            ObjUM.User_mod = AP.UsuarioAp.IdUsuario
                                            ObjUM.Fec_agr = Now
                                            ObjUM.Fec_mod = Now

                                            clsLnUnidad_medida.InsertarFromInterface(ObjUM, pConection, pTransaction)
                                            BeProducto.UnidadMedida.IdUnidadMedida = ObjUM.IdUnidadMedida
                                            BeProducto.IdUnidadMedidaBasica = ObjUM.IdUnidadMedida

                                            lblprg.AppendText("Aviso : Se agregó UMBas " & vUnidadMedidaBasica & " porque no existe en WMS.")
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        End If

                                    Else

                                        errores = True
                                        Tiene_Errores_Excel = True
                                        lblprg.AppendText("Error : En fila " & fila & ", la UMBas no esta definida.")
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, "la UMBas no esta definida."))
                                        Return True

                                    End If

                                    If vClasificacion <> "" Then

                                        BeProducto.Clasificacion = clsLnProducto_clasificacion.Get_Single_By_Nombre_By_Propietario(vClasificacion,
                                                                                                                                   BePropietario.IdPropietario,
                                                                                                                                   pConection,
                                                                                                                                   pTransaction)

                                        If Not BeProducto.Clasificacion Is Nothing Then
                                            BeProducto.IdClasificacion = BeProducto.Clasificacion.IdClasificacion
                                        Else 'Crea la clasificacion

                                            BeProducto.Clasificacion = New clsBeProducto_clasificacion
                                            BeProducto.Clasificacion.IdClasificacion = clsLnProducto_clasificacion.MaxId(pConection, pTransaction)
                                            BeProducto.Clasificacion.Nombre = clsPublic.Quitar_Caracteres_No_Permitidos(vClasificacion)
                                            BeProducto.Clasificacion.Propietario = BePropietario
                                            BeProducto.Clasificacion.Sistema = False
                                            BeProducto.Clasificacion.IsNew = True
                                            BeProducto.Clasificacion.Activo = True
                                            BeProducto.Clasificacion.Fec_agr = Now
                                            BeProducto.Clasificacion.User_agr = AP.UsuarioAp.IdUsuario
                                            BeProducto.Clasificacion.User_mod = AP.UsuarioAp.IdUsuario
                                            BeProducto.Clasificacion.Fec_mod = Now
                                            clsLnProducto_clasificacion.Insertar(BeProducto.Clasificacion, pConection, pTransaction)
                                            BeProducto.IdClasificacion = BeProducto.Clasificacion.IdClasificacion

                                            lblprg.AppendText("Aviso : Se agregó Clasificación " & vClasificacion & " porque no existe en WMS.")
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        End If
                                    End If

                                    'GT 25102021: Hasta aqui todo bien, se guarda el producto
                                    BeProducto.Codigo_barra = vCodigoBarra
                                    BeProducto.Precio = 0
                                    BeProducto.Existencia_min = 0
                                    BeProducto.Existencia_max = 0
                                    BeProducto.Costo = vCosto
                                    BeProducto.Peso_referencia = vPeso_ref
                                    BeProducto.Peso_tolerancia = 0
                                    BeProducto.Temperatura_referencia = 0
                                    BeProducto.Temperatura_tolerancia = 0
                                    BeProducto.Serializado = 0
                                    BeProducto.Genera_lote = 0
                                    BeProducto.Genera_lp = ObjConfigInt.Genera_lp '1
                                    BeProducto.Control_vencimiento = ObjConfigInt.Control_vencimiento  '0
                                    BeProducto.Control_lote = ObjConfigInt.Control_lote '0
                                    BeProducto.Control_peso = IIf(vPeso_ref > 0, True, ObjConfigInt.Control_peso)
                                    BeProducto.Peso_recepcion = 0
                                    BeProducto.Peso_despacho = 0
                                    BeProducto.Temperatura_recepcion = 0
                                    BeProducto.Temperatura_despacho = 0
                                    BeProducto.Materia_prima = 0
                                    BeProducto.Tolerancia = 0
                                    BeProducto.Ciclo_vida = 0
                                    BeProducto.Propietario = New clsBePropietarios()
                                    BeProducto.Clasificacion = New clsBeProducto_clasificacion()
                                    BeProducto.Familia = New clsBeProducto_familia()
                                    BeProducto.Marca = New clsBeProducto_marca()
                                    BeProducto.TipoProducto = New clsBeProducto_tipo()
                                    BeProducto.UnidadMedida = New clsBeUnidad_medida()
                                    BeProducto.Arancel = New clsBeArancel()
                                    BeProducto.IdPropietario = BePropietario.IdPropietario
                                    BeProducto.IdCamara = 0
                                    BeProducto.IdTipoRotacion = 0
                                    BeProducto.IdPerfilSerializado = 0
                                    BeProducto.IdIndiceRotacion = 0
                                    BeProducto.Arancel.IdArancel = 0
                                    BeProducto.Nombre = clsPublic.Quitar_Caracteres_No_Permitidos(vDescripcionProducto.Trim)
                                    BeProducto.Activo = True
                                    BeProducto.User_agr = AP.UsuarioAp.IdUsuario
                                    BeProducto.Fec_agr = Now
                                    BeProducto.User_mod = AP.UsuarioAp.IdUsuario
                                    BeProducto.Fec_mod = Now
                                    BeProducto.Dias_Inventario_Promedio = 0
                                    BeProducto.IdTipoEtiqueta = ObjConfigInt.IdTipoEtiqueta

                                    clsLnProducto.Insertar(BeProducto,
                                                           pConection,
                                                           pTransaction)

                                    lblprg.AppendText("Aviso: Se insertó producto " & vCodigoProducto & " porque no existe en WMS.")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                Else

                                    errores = True
                                    Tiene_Errores_Excel = True
                                    lblprg.AppendText("Error :  En fila " & fila & ", falta descripción del producto " & vCodigoProducto)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                    Exit Function

                                End If

                            Else
                                'GT08042022: si existe el producto, se hace set del tipo etiqueta default cealsa
                                BeProducto.Fec_mod = Now
                                BeProducto.User_mod = AP.UsuarioAp.IdUsuario
                                BeProducto.IdTipoEtiqueta = ObjConfigInt.IdTipoEtiqueta
                                clsLnProducto.Actualizar(BeProducto,
                                                         pConection,
                                                         pTransaction)

                            End If

                        Else
                            errores = True
                            Tiene_Errores_Excel = True
                            lblprg.AppendText("Error : En fila " & fila & ", el propietario no existe en wms, no se puede crear el producto.")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            Exit Function
                        End If

                    Else
                        errores = True
                        Tiene_Errores_Excel = True
                        lblprg.AppendText("Error :  En fila " & fila & ", falta el código del producto " & vCodigoProducto)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        Exit Function
                    End If

                    If vDescripcionProducto = "" Then
                        errores = True
                        Tiene_Errores_Excel = True
                        lblprg.AppendText("Error :  En fila " & fila & ", falta descripción del producto " & vCodigoProducto)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        Exit Function
                    End If

                    'GT 25102021: Si presentación no existe, se inserta
                    If vNombrePresentacion <> "" Then

                        Dim pvNombrePresentacion = clsPublic.Quitar_Caracteres_No_Permitidos(vNombrePresentacion)

                        Dim ObjPPres = clsLnProducto_presentacion.Get_Presentacion_By_Nombre(pvNombrePresentacion, pConection, pTransaction)

                        If ObjPPres Is Nothing And vFactorPresentacion <> "" Then

                            Dim objIdProducto = clsLnProducto.Exists(vCodigoProducto, pConection, pTransaction)

                            If objIdProducto > 0 Then

                                Dim pObjPresentacion = New clsBeProducto_Presentacion
                                pObjPresentacion.IdPresentacion = clsLnProducto_presentacion.MaxID(pConection, pTransaction) + 1
                                pObjPresentacion.IdProducto = objIdProducto
                                pObjPresentacion.Codigo_barra = vCodigoBarra
                                pObjPresentacion.Nombre = pvNombrePresentacion
                                pObjPresentacion.Factor = CInt(vFactorPresentacion)
                                pObjPresentacion.Imprime_barra = 1
                                pObjPresentacion.User_agr = AP.UsuarioAp.IdUsuario
                                pObjPresentacion.Fec_agr = Date.Now
                                pObjPresentacion.User_mod = AP.UsuarioAp.IdUsuario
                                pObjPresentacion.Fec_mod = Date.Now
                                pObjPresentacion.Activo = 1
                                pObjPresentacion.Genera_lp_auto = 1
                                pObjPresentacion.Sistema = 0
                                pObjPresentacion.Codigo = vCodigoProducto

                                clsLnProducto_presentacion.Insertar(pObjPresentacion, pConection, pTransaction)
                                lblprg.AppendText("Aviso : Se agregó Presentación porque no existe en WMS: " & pObjPresentacion.Nombre)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            End If

                        Else

                            errores = True
                            Tiene_Errores_Excel = True
                            lblprg.AppendText("Error : En fila " & fila & ", no se puede importar la presentacion " & vNombrePresentacion & " porque no tiene factor de conversión.")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                            Exit Function

                        End If

                    End If

                    'GT 28102021: valida un operador
                    If vNombreOperador = "" Then
                        Tiene_Errores_Excel = True
                        errores = True
                        lblprg.AppendText("Error : En fila " & fila & ", no hay un operador asignado")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        Exit Function

                    End If

                    'GT21022022: valida duplicidad de LP
                    '#GT07122022: valida si es motriz para no validar LP

                    If Not AP.Bodega.industria_motriz Then

                        '#GT19072023: sino es motriz, pero la interface dice no lp auto, se salta la validación de LP
                        If ObjConfigInt.Genera_lp Then

                            If Not vLP = "" Then

                                Dim vExisteLp = clsLnStock.Existe_Lp_In_Stock(vLP, pConection, pTransaction)

                                If vExisteLp Then
                                    Tiene_Errores_Excel = True
                                    errores = True
                                    lblprg.AppendText("Error : En fila " & fila & " del archivo, la LP ya existe en stock")
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                    Exit Function
                                Else

                                    If lLicenciasProcesadas.Contains(vLP) Then
                                        Tiene_Errores_Excel = True
                                        errores = True
                                        lblprg.AppendText("Error : En fila " & fila & " del archivo, la licencia está duplicada en otro registro.")
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()
                                        Exit Function
                                    Else
                                        lLicenciasProcesadas.Add(vLP)
                                    End If

                                End If
                            Else
                                Tiene_Errores_Excel = True
                                errores = True
                                lblprg.AppendText("Error : En fila " & fila & ", no hay una LP asignada")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()
                                Exit Function
                            End If
                        Else
                            Debug.Write("registro sin LP")
                        End If

                    End If


                    '#GT04052022_0915: validar que el valor sea mayor a 0 para posiciones
                    If vPosiciones < 0 Then
                        Tiene_Errores_Excel = True
                        errores = True
                        lblprg.AppendText("Error : En fila " & fila & ", el valor de posiciones no puede ser negativo.")
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        Exit Function

                    End If

                    'GT24012022: valida propietario_bodega, en caso no existir data de interfaz propietarios
                    Dim OjbPropetarioBodega As New clsBePropietario_bodega
                    Dim vContadorBod As Integer = 0
                    vContadorBod = clsLnPropietario_bodega.MaxID(pConection, pTransaction) + 1

                    For Each B In lBodegas

                        Dim existe = clsLnPropietario_bodega.Existe_IdPropietario_And_IdBodega(BePropietario.IdPropietario,
                                                                                               B.IdBodega,
                                                                                               pConection,
                                                                                               pTransaction)
                        If Not existe Then

                            OjbPropetarioBodega = New clsBePropietario_bodega()
                            OjbPropetarioBodega.IdPropietarioBodega = vContadorBod 'clsLnPropietario_bodega.MaxID(pConection, pTransaction) + vContadorBod
                            OjbPropetarioBodega.IdPropietario = BePropietario.IdPropietario
                            OjbPropetarioBodega.IdBodega = B.IdBodega
                            OjbPropetarioBodega.Activo = True
                            OjbPropetarioBodega.User_agr = AP.UsuarioAp.IdUsuario
                            OjbPropetarioBodega.Fec_agr = Now
                            OjbPropetarioBodega.User_mod = AP.UsuarioAp.IdUsuario
                            OjbPropetarioBodega.Fec_mod = Now

                            clsLnPropietario_bodega.Insertar(OjbPropetarioBodega,
                                                             pConection,
                                                             pTransaction)
                            vContadorBod += 1

                            lblprg.AppendText("Aviso: Se agrega propietario-bodega al catálogo: " & OjbPropetarioBodega.IdPropietarioBodega)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                        End If
                    Next


                    '#EJC20210901_B: Insertar producto bodega por cada bodega importación excel. (Nuevo producto)
                    'GT05022022: Se traslada para aca la iteración, porque dentro de validacion producto, solo revisa para una bodega
                    Dim vContador As Integer = 0
                    vContador = clsLnProducto_bodega.MaxID(pConection, pTransaction) + 1

                    For Each B In lBodegas

                        Dim ObjProdBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(BeProducto.Codigo,
                                                                                           B.IdBodega,
                                                                                           pConection,
                                                                                           pTransaction)

                        If ObjProdBodega Is Nothing Then

                            ObjPBod = New clsBeProducto_bodega
                            ObjPBod.IdProductoBodega = vContador
                            ObjPBod.IdProducto = BeProducto.IdProducto
                            ObjPBod.IdBodega = B.IdBodega
                            ObjPBod.Activo = True
                            ObjPBod.Sistema = False
                            ObjPBod.User_agr = AP.UsuarioAp.IdUsuario
                            ObjPBod.Fec_agr = Now
                            ObjPBod.User_mod = AP.UsuarioAp.IdUsuario
                            ObjPBod.Fec_mod = Now

                            clsLnProducto_bodega.InsertarFromInterface(ObjPBod,
                                                                       pConection,
                                                                       pTransaction)
                            vContador += 1

                            lblprg.AppendText("Aviso: Se insertó el producto-bodega " & BeProducto.Codigo & " porque no existía en WMS.")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End If

                    Next

                    '#GT19072023: insertar proveedor sino existe
                    Dim objProveedor As New clsBeProveedor
                    Dim pProveedor As New clsBeProveedor
                    Dim vContadorProveedor As Integer = 0

                    pProveedor = clsLnProveedor.Existe_by_IdPropietario(BePropietario.IdPropietario, pConection, pTransaction)

                    If pProveedor Is Nothing Then
                        vContadorProveedor = clsLnProveedor.MaxID(pConection, pTransaction) + 1
                        objProveedor.IdProveedor = vContadorProveedor
                        objProveedor.Codigo = vContadorProveedor
                        objProveedor.IdPropietario = BePropietario.IdPropietario
                        objProveedor.IdEmpresa = AP.IdEmpresa
                        objProveedor.Nombre = clsPublic.Quitar_Caracteres_No_Permitidos(BePropietario.Nombre_comercial)
                        objProveedor.Telefono = BePropietario.Telefono
                        objProveedor.Nit = BePropietario.NIT
                        objProveedor.Email = BePropietario.Email
                        objProveedor.Contacto = BePropietario.Nombre_comercial
                        objProveedor.Activo = 1
                        objProveedor.Muestra_precio = 0
                        objProveedor.User_agr = AP.UsuarioAp.IdUsuario
                        objProveedor.Fec_agr = Now
                        objProveedor.User_mod = AP.UsuarioAp.IdUsuario
                        objProveedor.Fec_mod = Now
                        objProveedor.Actualiza_costo_oc = 0
                        objProveedor.IdUbicacionVirtual = 0
                        objProveedor.Es_Bodega_Recepcion = 0
                        objProveedor.Es_Bodega_Traslado = 0
                        objProveedor.Referencia = "Importación inv inicial"
                        objProveedor.Sistema = 0
                        objProveedor.IdConfiguracionBarraPallet = 0
                        objProveedor.Es_Proveedor_Servicio = 0

                        clsLnProveedor.Insertar(objProveedor,
                                                  pConection,
                                                  pTransaction)

                        lblprg.AppendText("Aviso: Se agrega proveedor al catálogo: " & objProveedor.Nombre)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If


                    '#GT19072023: insertar proveedor-bodega sino existe
                    Dim objProveedorBodega As New clsBeProveedor_bodega
                    Dim vIdAsignacion As Integer = 0
                    vIdAsignacion = clsLnProveedor_bodega.MaxID(pConection, pTransaction) + 1

                    For Each B In lBodegas

                        Dim Proveedor = clsLnProveedor.Existe_by_IdPropietario(BePropietario.IdPropietario, pConection, pTransaction)

                        If Proveedor IsNot Nothing Then

                            Dim existeProveedorBodega = clsLnProveedor_bodega.Exist_By_IdBodega_And_IdProveedor(B.IdBodega, Proveedor.IdProveedor,
                                                                                                             pConection,
                                                                                                             pTransaction)

                            If Not existeProveedorBodega Then

                                objProveedorBodega.IdAsignacion = vIdAsignacion
                                objProveedorBodega.IdProveedor = Proveedor.IdProveedor
                                objProveedorBodega.IdBodega = B.IdBodega
                                objProveedorBodega.Activo = True
                                objProveedorBodega.User_agr = AP.UsuarioAp.IdUsuario
                                objProveedorBodega.Fec_agr = Now
                                objProveedorBodega.User_mod = AP.UsuarioAp.IdUsuario
                                objProveedorBodega.Fec_mod = Now

                                clsLnProveedor_bodega.Insertar(objProveedorBodega, pConection, pTransaction)

                                lblprg.AppendText("Aviso: Se agrega proveedor-bodega al catálogo: " & objProveedorBodega.IdAsignacion)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                                vIdAsignacion += 1

                            End If

                        End If

                    Next



                Else

                    If vCodigoProducto = "" Then
                        errores = True
                        Tiene_Errores_Excel = True
                        lblprg.AppendText("Error :  En fila " & fila & ", falta el código del producto " & vCodigoProducto)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        Exit Function
                    Else
                        errores = True
                        Tiene_Errores_Excel = True
                        lblprg.AppendText("Error :  En fila " & fila & ", falta # de linea, para identificar correlativo del producto: " & vCodigoProducto)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()
                        Exit Function
                    End If

                End If

                fila += 1

                vContadorPrg += 1
                prg.Value = vContadorPrg

            Next

            Tiene_Errores_Excel = errores

        Catch ex As Exception
            Tiene_Errores_Excel = True
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            Cursor = Cursors.Default
            prg.Value = 0
            prg.Visible = False
        End Try

    End Function

    Private Function Scan_Poliza(ByVal barra_poliza As clsBeTrans_inv_inicial_excel_op_log) As clsBeCEALSA_DUCA_ENC
        Try

            Dim poliza As New clsBeCEALSA_DUCA_ENC()

            poliza.Numero_Orden = barra_poliza.Pol_numero_orden
            poliza.Numero_DUCA = barra_poliza.Pol_numero_duca
            poliza.Clave_aduana_despacho_destino = barra_poliza.Pol_clave_aduana
            poliza.NIT_Importador = barra_poliza.Pol_nit_importador
            poliza.Regimen = barra_poliza.Pol_regimen 'el valor del excel esta mal asignado, debe ser alfanumerico tipo 24TO pero viene como fiscal/general
            poliza.Clase = barra_poliza.Pol_clase
            poliza.Pais_procedencia = barra_poliza.Pol_pais_procedencia
            poliza.Modo_transporte = barra_poliza.Pol_modo_transporte
            poliza.Tipo_cambio = Convert.ToDouble(barra_poliza.Pol_tipo_cambio)
            poliza.Total_valor_aduana = Convert.ToDouble(barra_poliza.Pol_total_valor_aduana)
            poliza.Total_bultos_Peso_Bruto = Convert.ToDouble(barra_poliza.Pol_total_peso_bruto)
            poliza.TotalFOBUSD = Convert.ToDouble(barra_poliza.Pol_totalfobusd)
            poliza.Total_Flete_USD = Convert.ToDouble(barra_poliza.Pol_total_flete_usd)
            poliza.Total_Seguro_USD = Convert.ToDouble(barra_poliza.Pol_total_seguro_usd)
            poliza.TotalOtrosgastosUSD = Convert.ToDouble(barra_poliza.Pol_totalotrosgastosusd)
            poliza.Total_Liquidar = Convert.ToDouble(barra_poliza.Pol_total_liquidar)
            poliza.Total_General = Convert.ToDouble(barra_poliza.Pol_total_general)
            poliza.Codigo_Poliza = barra_poliza.Pol_codigo_poliza
            'poliza.Fecha_Aceptacion = CType(barra_poliza.Pol_fecha_llegada, Date).ToShortDateString()
            poliza.Fecha_Llegada = barra_poliza.Pol_fecha_llegada.ToShortDateString()

            Scan_Poliza = poliza

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub cmdCargar_Click(sender As Object, e As EventArgs) Handles cmdCargar.Click

        Try

            errores = False

            'GT14012021: limpiamos tabla temporal previo a la nueva carga
            clsLnTrans_inv_inicial_excel_op_log.Eliminar_Temporal()

            If Archivo_Valido() Then

                'GT 03112021: deshabilitamos botones
                cmdCargar.Enabled = False
                cmdSalir.Enabled = False

                If Importar_Archivo() Then
                    DialogResult = DialogResult.OK
                Else
                    'GT19012022: si no se importa, no debe cerrar la ventana para ver errores en el log.
                    DialogResult = DialogResult.None

                    If errores Then
                        XtraMessageBox.Show("Errores en importación, revisar log.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If
            End If
        Catch ex As Exception
            cmdCargar.Enabled = True
            cmdSalir.Enabled = True
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

            GridView1.Appearance.FocusedRow.ForeColor = ColorTranslator.FromHtml("#FFFFFF")
            GridView1.Appearance.SelectedRow.ForeColor = ColorTranslator.FromHtml("#FFFFFF")

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

End Class
