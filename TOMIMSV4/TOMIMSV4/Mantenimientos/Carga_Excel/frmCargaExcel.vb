Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks
Imports ClosedXML.Excel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraSplashScreen
'Imports DevExpress.Export.Xl
'Imports Microsoft.Office.Interop

Public Class frmCargaExcel

    Public Delegate Sub Operar()
    Private Const vCantColumnas As Integer = 100
    Public Listar As Operar

    Public pNombreMantenimiento As String
    Public pTipoMantenimiento As String
    Public pPropietario As Boolean

    Private pListObj As List(Of clsExcel)
    Private pcmbP As New Windows.Forms.ComboBox
    Private pcmbProp As New Windows.Forms.ComboBox

    Public InsertaInv As Boolean = False
    Public IdOperador As Integer = 0
    Public NomOperador As String = ""

    Public Property IdInventarioEnc As Integer = 0
    Public Property lInvenarioTeorico As New DataTable

    Private DT As New DataTable("Carga")
    Private DT2 As New DataTable("Importacion")
    Private registro As Integer = 0
    Private pFilaVacia As Boolean = False
    Private ListObjImportBodega As New List(Of clsBeTrans_bodega_ubicaciones_excel)

    Private errores As Boolean = False
    Public tipo_archivo As String = ""

    '#GT15062022_1140: parametros para cambio de ubicaciones por importación Excel
    Dim pObjStock As clsBeVW_stock_res
    Dim lObjStock As New List(Of clsBeVW_stock_res)
    Public pListObjDet As New List(Of clsBeTrans_ubic_hh_det) '#EJC20170919
    Private pBeTransUbicHHDet As New clsBeTrans_ubic_hh_det
    Private selUbic As clsBeUbicacionSugeridaList
    Private pBeUbicacionDestino As New clsBeBodega_ubicacion
    Private lUbicSel As New List(Of clsBeUbicacionSugeridaList)
    Public pDetCorrel As Integer
    Private BeEstadoProd As New clsBeProducto_estado
    Public pListObjMov As New List(Of clsBeTrans_movimientos) '#EJC20170913
    Public pListStockMov As New List(Of clsBeStock) '#EJC20170913
    '#EJC20171006_0516AM: Agregado para validar si es cambio de estado.
    Public Property EsCambioEstado As Boolean = False
    'Public IdBodega_ubicacion As Integer = 0
    Public pBodegaOrigen As New clsBeBodega
    Private pBeEstadoDestino As New clsBeProducto_estado

    Private Sub SetDatataTable2()

        DT2.Columns.Add("registro", GetType(Integer))
        'DT2.Columns.Add("cod_bodega", GetType(Integer))
        '#GT26042022_1146: el tipo puede ser codigo o Id
        DT2.Columns.Add("cod_bodega", GetType(String))
        DT2.Columns.Add("cod_area", GetType(Integer))
        DT2.Columns.Add("cod_sector", GetType(Integer))
        DT2.Columns.Add("tipo_ubicacion", GetType(String))
        DT2.Columns.Add("tipo_rack", GetType(String))
        DT2.Columns.Add("numero", GetType(Integer))
        DT2.Columns.Add("ingreso_por", GetType(String))
        DT2.Columns.Add("orientacion", GetType(String))
        DT2.Columns.Add("orden", GetType(String))
        DT2.Columns.Add("x", GetType(Double))
        DT2.Columns.Add("y", GetType(Double))
        DT2.Columns.Add("nivel", GetType(String))
        DT2.Columns.Add("col1", GetType(String))
        DT2.Columns.Add("col2", GetType(String))
        DT2.Columns.Add("col3", GetType(String))
        DT2.Columns.Add("col4", GetType(String))
        DT2.Columns.Add("col5", GetType(String))
        DT2.Columns.Add("col6", GetType(String))
        DT2.Columns.Add("col7", GetType(String))
        DT2.Columns.Add("col8", GetType(String))
        DT2.Columns.Add("col9", GetType(String))
        DT2.Columns.Add("col10", GetType(String))
        DT2.Columns.Add("col11", GetType(String))
        DT2.Columns.Add("col12", GetType(String))
        DT2.Columns.Add("col13", GetType(String))
        DT2.Columns.Add("col14", GetType(String))
        DT2.Columns.Add("col15", GetType(String))
        DT2.Columns.Add("col16", GetType(String))
        DT2.Columns.Add("col17", GetType(String))
        DT2.Columns.Add("col18", GetType(String))
        DT2.Columns.Add("col19", GetType(String))
        DT2.Columns.Add("col20", GetType(String))
        DT2.Columns.Add("col21", GetType(String))
        DT2.Columns.Add("col22", GetType(String))
        DT2.Columns.Add("col23", GetType(String))
        DT2.Columns.Add("col24", GetType(String))
        DT2.Columns.Add("col25", GetType(String))
        DT2.Columns.Add("col26", GetType(String))
        DT2.Columns.Add("col27", GetType(String))
        DT2.Columns.Add("col28", GetType(String))
        DT2.Columns.Add("col29", GetType(String))
        DT2.Columns.Add("col30", GetType(String))
        DT2.Columns.Add("col31", GetType(String))
        DT2.Columns.Add("col32", GetType(String))
        DT2.Columns.Add("col33", GetType(String))
        DT2.Columns.Add("col34", GetType(String))
        DT2.Columns.Add("col35", GetType(String))
        DT2.Columns.Add("col36", GetType(String))
        DT2.Columns.Add("col37", GetType(String))
        DT2.Columns.Add("col38", GetType(String))
        DT2.Columns.Add("col39", GetType(String))
        DT2.Columns.Add("col40", GetType(String))
        DT2.Columns.Add("col41", GetType(String))
        DT2.Columns.Add("col42", GetType(String))
        DT2.Columns.Add("col43", GetType(String))
        DT2.Columns.Add("col44", GetType(String))
        DT2.Columns.Add("col45", GetType(String))
        DT2.Columns.Add("col46", GetType(String))
        DT2.Columns.Add("col47", GetType(String))
        DT2.Columns.Add("col48", GetType(String))
        DT2.Columns.Add("col49", GetType(String))
        DT2.Columns.Add("col50", GetType(String))
        DT2.Columns.Add("col51", GetType(String))
        DT2.Columns.Add("col52", GetType(String))
        DT2.Columns.Add("col53", GetType(String))
        DT2.Columns.Add("col54", GetType(String))
        DT2.Columns.Add("col55", GetType(String))
        DT2.Columns.Add("col56", GetType(String))
        DT2.Columns.Add("col57", GetType(String))
        DT2.Columns.Add("col58", GetType(String))
        DT2.Columns.Add("col59", GetType(String))
        DT2.Columns.Add("col60", GetType(String))
        DT2.Columns.Add("col61", GetType(String))
        DT2.Columns.Add("col62", GetType(String))
        DT2.Columns.Add("col63", GetType(String))
        DT2.Columns.Add("col64", GetType(String))
        DT2.Columns.Add("col65", GetType(String))
        DT2.Columns.Add("col66", GetType(String))
        DT2.Columns.Add("col67", GetType(String))
        DT2.Columns.Add("col68", GetType(String))
        DT2.Columns.Add("col69", GetType(String))
        DT2.Columns.Add("col70", GetType(String))

    End Sub

    Private Sub SetComboProducto()

        Try

            pcmbP.Items.Add("ID Producto")
            pcmbP.Items.Add("Código Producto")

            pcmbP = New Windows.Forms.ComboBox()

            With pcmbP
                .Location = New Point(110, 60)
                .Width = 207
                .DropDownStyle = ComboBoxStyle.DropDownList
                .Visible = True
            End With

            GrpBorraTabla.Controls.Add(pcmbProp)

            IMS.Listar_PropietariosByEmpresaExcel(pcmbProp, AP.IdEmpresa)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub CreaCombobox()

        Try

            pcmbP = New Windows.Forms.ComboBox()

            With pcmbP
                .Location = New Point(87, 65)
                .Width = 207
                .DropDownStyle = ComboBoxStyle.DropDownList
                .Visible = True
            End With

            GrpBorraTabla.Controls.Add(pcmbP)

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

    Private Function Cargar_Archivo_Excel() As Boolean

        Cargar_Archivo_Excel = False

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormCaption("Procesando archivo...")

        Try

            Dim Obj As clsExcel = pListObj.Find(Function(s) s.Checked = True)
            Dim fileName As String = txtArchivo.Text
            Dim Hash As String = clsPublic.Check_MD5(txtArchivo.Text)
            Dim documento As XLWorkbook = New XLWorkbook(fileName)
            Dim documento1 As IXLWorksheet = documento.Worksheet(Obj.Index + 1)

            beLogImportacion = New clsBeLog_importacion_excel()
            beLogImportacion.IdBodega = AP.IdBodega
            beLogImportacion.IdEmpresa = AP.IdEmpresa
            beLogImportacion.IdUsuario = AP.UsuarioAp.IdUsuario
            beLogImportacion.Hash_archivo = Hash

            If clsLnLog_importacion_excel.Existe(Hash) Then
                SplashScreenManager.CloseForm(False)
                If XtraMessageBox.Show("¿El archivo fue importado previamente, volver a importar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Return False
                End If
            End If

            If documento1.RowsUsed.Count < 2 Then
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("La hoja esta vacía. no contiene datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Procesando archivo...")

            DT = New DataTable("Carga")

            'Loop through the Worksheet rows.
            Dim firstRow As Boolean = True
            For Each row As IXLRow In documento1.RowsUsed
                'Use the first row to add columns to DataTable.
                If firstRow Then
                    For Each cell As IXLCell In row.Cells
                        DT.Columns.Add(cell.Value.ToString())
                    Next
                    firstRow = False
                Else
                    'Add rows to DataTable.
                    DT.Rows.Add()
                    Dim i As Integer = 0
                    For Each cell As IXLCell In row.Cells(False)
                        DT.Rows(DT.Rows.Count - 1)(i) = cell.Value.ToString()
                        i += 1
                    Next
                End If
            Next

            lblPrg.Text = ""

            If DT.Rows.Count > 0 Then

                Select Case pTipoMantenimiento

                    Case "MotivoAnulacion"
                        CargaMotivoAnulacion(DT)
                    Case "MotivoDevolucion"
                        CargaMotivoDevolucion(DT)
                    Case "Operador"
                        CargaOperador(DT)
                    Case "ProductoClasificacion"
                        CargaProductoClasificacion(DT)
                    Case "ProductoFamilia"
                        CargaProductoFamilia(DT)
                    Case "ProductoMarca"
                        CargaProductoMarca(DT)
                    Case "ProductoEstado"
                        CargaProductoEstado(DT)
                    Case "ProductoPresentacion"
                        CargaProductoPresentacion(DT)
                    Case "UnidadMedida"
                        Cargar_Archivo_Excel = CargaUnidadMedida(DT)
                    Case "Producto"
                        Cargar_Archivo_Excel = CargaProducto(DT)
                    Case "Tarima"
                        CargaTarima(DT)
                    Case "Inventario"
                        Cargar_Archivo_Excel = Carga_Inventario_Teorico(DT)
                    Case "Reubicación"
                        Cargar_Archivo_Excel = Carga_Cambio_Ubicacion(DT)
                    Case "CambioEstado"
                        Cargar_Archivo_Excel = Carga_Cambio_Estado(DT)
                    Case "Reabasto"
                        Cargar_Archivo_Excel = Cargar_Reabasto(DT)
                    Case "Indices_Rotacion"
                        Cargar_Archivo_Excel = Actualizar_Indices_Rotacion_Producto(DT)
                    Case "Indices_Rotacion_Bodega"
                        Cargar_Archivo_Excel = Actualizar_Indices_Rotacion_Bodega(DT)
                    Case Else
                        Exit Select
                End Select

            Else
                lblPrg.Text = "No hay registros para importar."
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub CargaMotivoAnulacion(ByVal pDT As DataTable)

        Dim pListObjMA As New List(Of clsBeMotivo_anulacion)

        Try

            Cursor = Cursors.WaitCursor

            For i As Integer = 0 To pDT.Rows.Count - 1

                Dim Indice As Integer = i

                Application.DoEvents()

                Dim ObjA As New clsBeMotivo_anulacion

                If pDT(i)(0) Is DBNull.Value AndAlso pDT(i)(0) Is Nothing Then
                    Throw New Exception("Falta código del motivo anulación en la fila " & i + 1)
                End If
                If IsNumeric(pDT(i)(0)) = False Then
                    Throw New Exception("El código del motivo anulación debe ser númerico entero. Fila " & i + 1)
                Else
                    Dim lIdExiste As Boolean = pListObjMA.Exists(Function(e) e.IdMotivoAnulacion = CInt(pDT(Indice)(0)))
                    If lIdExiste Then
                        Throw New Exception(String.Format("El código del motivo anulación de la fila {0} se encuentra repetido.", i + 1))
                    Else
                        If Not clsLnMotivo_anulacion.Exists(pDT(i)(0)) Then
                            ObjA.IsNew = True
                        Else
                            ObjA.IsNew = False
                        End If
                        ObjA.IdMotivoAnulacion = CInt(pDT(i)(0))
                    End If
                End If

                If pDT(i)(1) Is DBNull.Value AndAlso pDT(i)(1) Is Nothing Then
                    Throw New Exception("Falta nombre del motivo anulación en la fila " & i + 1)
                End If

                ObjA.IdEmpresa = AP.IdEmpresa
                ObjA.Nombre = CStr(pDT(i)(1))
                ObjA.Activo = True
                ObjA.User_agr = AP.UsuarioAp.IdUsuario
                ObjA.Fec_agr = Now
                ObjA.User_mod = AP.UsuarioAp.IdUsuario
                ObjA.Fec_mod = Now
                pListObjMA.Add(ObjA)

            Next

            If pListObjMA IsNot Nothing AndAlso pListObjMA.Count > 0 Then
                clsLnMotivo_anulacion.GuardarTransaccion(pListObjMA)
                XtraMessageBox.Show(String.Format("Se importaron {0} correctamente.", pListObjMA.Count - 1), Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Listar.Invoke()
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub CargaMotivoDevolucion(ByVal pDT As DataTable)

        Dim pListObjMD As New List(Of clsBeMotivo_devolucion)

        Try

            Cursor = Cursors.WaitCursor

            For i As Integer = 0 To pDT.Rows.Count - 1

                Application.DoEvents()

                Dim Indice As Integer = i

                Dim ObjD As New clsBeMotivo_devolucion

                If pDT(i)(0) Is DBNull.Value AndAlso pDT(i)(0) Is Nothing Then
                    Throw New Exception("Falta código del motivo devolución en la fila " & i + 1)
                End If
                If IsNumeric(pDT(i)(0)) = False Then
                    Throw New Exception("El código del motivo devolución debe ser númerico entero. Fila " & i + 1)
                Else
                    Dim lIdExiste As Boolean = pListObjMD.Exists(Function(d) d.IdMotivoDevolucion = CInt(pDT(Indice)(0)))
                    If lIdExiste Then
                        Throw New Exception(String.Format("El código del motivo devolución de la fila {0} se encuentra repetido.", i + 1))
                    Else
                        If Not clsLnMotivo_devolucion.Exists(pDT(i)(0)) Then
                            ObjD.IsNew = True
                        Else
                            ObjD.IsNew = False
                        End If
                        ObjD.IdMotivoDevolucion = CInt(pDT(i)(0))
                    End If
                End If

                If pDT(i)(0) Is DBNull.Value AndAlso pDT(i)(0) Is Nothing Then
                    Throw New Exception("Falta nombre del motivo devolución en la fila " & i + 1)
                End If

                ObjD.Empresa = New clsBeEmpresa
                ObjD.Empresa.IdEmpresa = AP.IdEmpresa
                ObjD.Propietario = New clsBePropietarios
                ObjD.Propietario.IdPropietario = CInt(pcmbP.SelectedValue)
                ObjD.Nombre = CStr(pDT(i)(1))
                ObjD.Activo = True
                ObjD.User_agr = AP.UsuarioAp.IdUsuario
                ObjD.Fec_agr = Now
                ObjD.User_mod = AP.UsuarioAp.IdUsuario
                ObjD.Fec_mod = Now
                pListObjMD.Add(ObjD)

            Next

            If pListObjMD IsNot Nothing AndAlso pListObjMD.Count > 0 Then
                clsLnMotivo_devolucion.GuardarTransaccion(pListObjMD)
                XtraMessageBox.Show("Importación realizada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Listar.Invoke()
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub CargaOperador(ByVal pDT As DataTable)

        Dim pListObjO As New List(Of clsBeOperador)

        Try

            Cursor = Cursors.WaitCursor

            For i As Integer = 0 To pDT.Rows.Count - 1

                Dim Indice As Integer = i

                Application.DoEvents()

                Dim ObjO As New clsBeOperador

                If pDT(i)(0) Is DBNull.Value AndAlso pDT(i)(0) Is Nothing Then
                    Throw New Exception("Falta código del operador en la fila " & i + 1)
                End If
                If IsNumeric(pDT(i)(0)) = False Then
                    Throw New Exception("El código del operador debe ser númerico entero. Fila " & i + 1)
                Else
                    Dim lIdExiste As Boolean = pListObjO.Exists(Function(d) d.IdOperador = CInt(pDT(Indice)(0)))
                    If lIdExiste Then
                        Throw New Exception(String.Format("El código del operador de la fila {0} se encuentra repetido.", i + 1))
                    Else
                        If Not clsLnOperador.Exists(pDT(i)(0)) Then
                            ObjO.IsNew = True
                        Else
                            ObjO.IsNew = False
                        End If
                        ObjO.IdOperador = CInt(pDT(i)(0))
                    End If
                End If

                If pDT(i)(0) Is DBNull.Value AndAlso pDT(i)(0) Is Nothing Then
                    Throw New Exception("Falta nombre del operador en la fila " & i + 1)
                End If

                ObjO.IdOperador = CInt(pDT(i)(0))
                ObjO.IdEmpresa = AP.IdEmpresa
                ObjO.Nombres = CStr(pDT(i)(1))

                If pDT(i)(2) IsNot DBNull.Value AndAlso pDT(i)(2) IsNot Nothing Then
                    ObjO.Apellidos = CStr(pDT(i)(2))
                End If

                If pDT(i)(3) IsNot DBNull.Value AndAlso pDT(i)(3) IsNot Nothing Then
                    ObjO.Direccion = CStr(pDT(i)(3))
                End If

                If pDT(i)(4) IsNot DBNull.Value AndAlso pDT(i)(4) IsNot Nothing Then
                    ObjO.Telefono = CStr(pDT(i)(4))
                End If

                If pDT(i)(5) IsNot DBNull.Value AndAlso pDT(i)(5) IsNot Nothing Then
                    ObjO.Codigo = CStr(pDT(i)(5))
                End If

                If pDT(i)(6) IsNot DBNull.Value AndAlso pDT(i)(6) IsNot Nothing Then
                    ObjO.Clave = CStr(pDT(i)(6))
                End If

                If pDT(i)(7) IsNot DBNull.Value AndAlso pDT(i)(7) IsNot Nothing Then
                    ObjO.Costo_hora = CDbl(pDT(i)(7))
                End If

                If pDT(i)(8) IsNot DBNull.Value AndAlso pDT(i)(8) IsNot Nothing Then
                    ObjO.Usa_hh = CBool(pDT(i)(8))
                End If

                ObjO.Activo = True
                ObjO.User_agr = AP.UsuarioAp.IdUsuario
                ObjO.Fec_agr = Now
                ObjO.User_mod = AP.UsuarioAp.IdUsuario
                ObjO.Fec_mod = Now
                pListObjO.Add(ObjO)

            Next

            If pListObjO IsNot Nothing AndAlso pListObjO.Count > 0 Then
                clsLnOperador.GuardarTransaccion(pListObjO)
                XtraMessageBox.Show("Importación realizada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Listar.Invoke()
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub CargaProductoClasificacion(ByVal pDT As DataTable)

        Dim pListObjPC As New List(Of clsBeProducto_clasificacion)

        Try

            Cursor = Cursors.WaitCursor

            For i As Integer = 0 To pDT.Rows.Count - 1

                Dim Indice As Integer = i

                Application.DoEvents()

                Dim ObjPC As New clsBeProducto_clasificacion() With {.Propietario = New clsBePropietarios()}

                If pDT(i)(0) Is DBNull.Value AndAlso pDT(i)(0) Is Nothing Then
                    Throw New Exception("Falta código de la clasificación en la fila " & i + 1)
                End If
                If IsNumeric(pDT(i)(0)) = False Then
                    Throw New Exception("El código de la clasificación debe ser númerico entero. Fila " & i + 1)
                Else
                    Dim lIdExiste As Boolean = pListObjPC.Exists(Function(d) d.IdClasificacion = CInt(pDT(Indice)(0)))
                    If lIdExiste Then
                        Throw New Exception(String.Format("El código de la clasificación de la fila {0} se encuentra repetido.", i + 1))
                    Else
                        If Not clsLnProducto_clasificacion.Exists(pDT(i)(0)) Then
                            ObjPC.IsNew = True
                        Else
                            ObjPC.IsNew = False
                        End If
                        ObjPC.IdClasificacion = CInt(pDT(i)(0))
                    End If
                End If

                If pDT(i)(1) Is DBNull.Value AndAlso pDT(i)(1) Is Nothing Then
                    Throw New Exception("Falta nombre de la clasificación en la fila " & i + 1)
                End If

                ObjPC.Propietario.IdPropietario = CInt(pcmbP.SelectedValue)
                ObjPC.Nombre = CStr(pDT(i)(1))
                ObjPC.Sistema = CBool(pDT(i)(2))
                ObjPC.Activo = True
                ObjPC.User_agr = AP.UsuarioAp.IdUsuario
                ObjPC.Fec_agr = Now
                ObjPC.User_mod = AP.UsuarioAp.IdUsuario
                ObjPC.Fec_mod = Now
                pListObjPC.Add(ObjPC)

            Next

            If pListObjPC IsNot Nothing AndAlso pListObjPC.Count > 0 Then
                clsLnProducto_clasificacion.GuardarTransaccion(pListObjPC)
                XtraMessageBox.Show("Importación realizada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Listar.Invoke()
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub CargaProductoFamilia(ByVal pDT As DataTable)

        'Dim wmpf As New WCFProductoFamilia.ServiceProductoFamiliaClient
        Dim pListObjPF As New List(Of clsBeProducto_familia)

        Try

            Cursor = Cursors.WaitCursor

            For i As Integer = 0 To pDT.Rows.Count - 1

                Application.DoEvents()

                Dim Indice As Integer = i

                Dim ObjPF As New clsBeProducto_familia() With {.Propietario = New clsBePropietarios()}

                If pDT(i)(0) Is DBNull.Value AndAlso pDT(i)(0) Is Nothing Then
                    Throw New Exception("Falta código de la familia en la fila " & i + 1)
                End If
                If IsNumeric(pDT(i)(0)) = False Then
                    Throw New Exception("El código de la familia debe ser númerico entero. Fila " & i + 1)
                Else
                    Dim lIdExiste As Boolean = pListObjPF.Exists(Function(d) d.IdFamilia = CInt(pDT(Indice)(0)))
                    If lIdExiste Then
                        Throw New Exception("El código de la familia de la fila " & i + 1 & " se encuentra repetido.")
                    Else
                        If Not clsLnProducto_familia.Exists(pDT(i)(0)) Then
                            ObjPF.IsNew = True
                        Else
                            ObjPF.IsNew = False
                        End If
                        ObjPF.IdFamilia = CInt(pDT(i)(0))
                    End If
                End If

                If pDT(i)(1) Is DBNull.Value AndAlso pDT(i)(1) Is Nothing Then
                    Throw New Exception("Falta nombre de la familia en la fila " & i + 1)
                End If

                ObjPF.Propietario.IdPropietario = CInt(pcmbP.SelectedValue)
                ObjPF.Nombre = CStr(pDT(i)(1))
                ObjPF.Activo = True
                ObjPF.User_agr = AP.UsuarioAp.IdUsuario
                ObjPF.Fec_agr = Now
                ObjPF.User_mod = AP.UsuarioAp.IdUsuario
                ObjPF.Fec_mod = Now
                pListObjPF.Add(ObjPF)

            Next

            If pListObjPF IsNot Nothing AndAlso pListObjPF.Count > 0 Then
                clsLnProducto_familia.GuardarTransaccion(pListObjPF)
                XtraMessageBox.Show("Importación realizada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Listar.Invoke()
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub CargaProductoMarca(ByVal pDT As DataTable)

        Dim pListObjPM As New List(Of clsBeProducto_marca)

        Try

            Cursor = Cursors.WaitCursor

            For i As Integer = 0 To pDT.Rows.Count - 1

                Dim Indice As Integer = i

                Application.DoEvents()

                Dim ObjPM As New clsBeProducto_marca

                If pDT(i)(0) Is DBNull.Value AndAlso pDT(i)(0) Is Nothing Then
                    Throw New Exception("Falta código de la marca en la fila " & i + 1)
                End If
                If IsNumeric(pDT(i)(0)) = False Then
                    Throw New Exception("El código de la marca debe ser númerico entero. Fila " & i + 1)
                Else
                    Dim lIdExiste As Boolean = pListObjPM.Exists(Function(d) d.IdMarca = CInt(pDT(Indice)(0)))
                    If lIdExiste Then
                        Throw New Exception("El código de la marca de la fila " & i + 1 & " se encuentra repetido.")
                    Else
                        If Not clsLnProducto_marca.Exists(pDT(i)(0)) Then
                            ObjPM.IsNew = True
                        Else
                            ObjPM.IsNew = False
                        End If
                        ObjPM.IdMarca = CInt(pDT(i)(0))
                    End If
                End If

                If pDT(i)(1) Is DBNull.Value AndAlso pDT(i)(1) Is Nothing Then
                    Throw New Exception("Falta nombre de la marca en la fila " & i + 1)
                End If

                ObjPM.IdPropietario = CInt(pcmbP.SelectedValue)
                ObjPM.Nombre = CStr(pDT(i)(1))
                ObjPM.Activo = True
                ObjPM.User_agr = AP.UsuarioAp.IdUsuario
                ObjPM.Fec_agr = Now
                ObjPM.User_mod = AP.UsuarioAp.IdUsuario
                ObjPM.Fec_mod = Now
                pListObjPM.Add(ObjPM)

            Next

            If pListObjPM IsNot Nothing AndAlso pListObjPM.Count > 0 Then
                clsLnProducto_marca.GuardarTransaccion(pListObjPM)
                XtraMessageBox.Show("Importación realizada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Listar.Invoke()
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub CargaProductoEstado(ByVal pDT As DataTable)

        Dim pListObjPE As New List(Of clsBeProducto_estado)

        Try

            Cursor = Cursors.WaitCursor

            For i As Integer = 0 To pDT.Rows.Count - 1

                Application.DoEvents()

                Dim Indice As Integer = i

                Dim ObjPE As New clsBeProducto_estado

                If pDT(i)(0) Is DBNull.Value AndAlso pDT(i)(0) Is Nothing Then
                    Throw New Exception("Falta código del estado en la fila " & i + 1)
                End If
                If IsNumeric(pDT(i)(0)) = False Then
                    Throw New Exception("El código del estado debe ser númerico entero. Fila " & i + 1)
                Else
                    Dim lIdExiste As Boolean = pListObjPE.Exists(Function(d) d.IdEstado = CInt(pDT(Indice)(0)))
                    If lIdExiste Then
                        Throw New Exception("El código del estado de la fila " & i + 1 & " se encuentra repetido.")
                    Else
                        If Not clsLnProducto_estado.Exists(pDT(i)(0)) Then
                            ObjPE.IsNew = True
                        Else
                            ObjPE.IsNew = False
                        End If
                        ObjPE.IdEstado = CInt(pDT(i)(0))
                    End If
                End If

                If pDT(i)(1) Is DBNull.Value AndAlso pDT(i)(1) Is Nothing Then
                    Throw New Exception("Falta nombre del estado en la fila " & i + 1)
                End If

                If pDT(i)(2) IsNot DBNull.Value AndAlso pDT(i)(2) IsNot Nothing Then
                    If IsNumeric(pDT(i)(2)) = False Then
                        Throw New Exception("La ubicación debe ser un valor númerico entero. Fila " & i + 1)
                    End If
                End If

                ObjPE.IdPropietario = CInt(pcmbP.SelectedValue)
                ObjPE.Nombre = CStr(pDT(i)(1))
                ObjPE.IdUbicacionDefecto = CInt(pDT(i)(2))
                ObjPE.Activo = True
                ObjPE.User_agr = AP.UsuarioAp.IdUsuario
                ObjPE.Fec_agr = Now
                ObjPE.User_mod = AP.UsuarioAp.IdUsuario
                ObjPE.Fec_mod = Now
                pListObjPE.Add(ObjPE)

            Next

            If pListObjPE IsNot Nothing AndAlso pListObjPE.Count > 0 Then
                clsLnProducto_estado.GuardarTransaccion(pListObjPE)
                XtraMessageBox.Show("Importación realizada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Listar.Invoke()
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub CargaProductoPresentacion(ByVal pDT As DataTable)

        If pcmbP.SelectedIndex = -1 Then
            XtraMessageBox.Show("Seleccione de que forma cargará las presentaciones. (Id o Código del Producto).", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim pListObjPP As New List(Of clsBeProducto_Presentacion)

        Try

            Cursor = Cursors.WaitCursor

            For i As Integer = 0 To pDT.Rows.Count - 1

                Dim Indice As Integer = i

                Application.DoEvents()

                Dim ObjPP As New clsBeProducto_Presentacion

                If pDT(i)(0) Is DBNull.Value AndAlso pDT(i)(0) Is Nothing Then
                    Throw New Exception("Falta código de la presentación en la fila " & i + 1)
                End If
                If IsNumeric(pDT(i)(0)) = False Then
                    Throw New Exception("El código de la presentación debe ser númerico entero. Fila " & i + 1)
                Else
                    Dim lIdExiste As Boolean = pListObjPP.Exists(Function(d) d.IdPresentacion = CInt(pDT(Indice)(0)))
                    If lIdExiste Then
                        Throw New Exception("El código de la presentación de la fila " & i + 1 & " se encuentra repetido.")
                    Else
                        If Not clsLnProducto_presentacion.Exists(pDT(i)(0)) Then
                            ObjPP.IsNew = True
                        Else
                            ObjPP.IsNew = False
                        End If
                        ObjPP.IdPresentacion = CInt(pDT(i)(0))
                    End If
                End If

                ' NOMBRE DE LA PRESENTACIÓN
                If pDT(i)(1) Is DBNull.Value AndAlso pDT(i)(1) Is Nothing Then
                    Throw New Exception("Falta nombre de la presentación en la fila " & i + 1)
                End If
                If pDT(i)(2) IsNot DBNull.Value AndAlso pDT(i)(2) IsNot Nothing Then
                    ObjPP.Imprime_barra = CBool(pDT(i)(2))
                End If
                If pDT(i)(3) IsNot DBNull.Value AndAlso pDT(i)(3) IsNot Nothing Then
                    If IsNumeric(pDT(i)(3)) = False Then
                        Throw New Exception("El peso debe ser un valor númerico. Fila " & i + 1)
                    Else
                        ObjPP.Peso = CDbl(pDT(i)(3))
                    End If
                End If
                If pDT(i)(4) IsNot DBNull.Value AndAlso pDT(i)(4) IsNot Nothing Then
                    If IsNumeric(pDT(i)(4)) = False Then
                        Throw New Exception("El alto debe ser un valor númerico. Fila " & i + 1)
                    Else
                        ObjPP.Alto = CDbl(pDT(i)(4))
                    End If
                End If
                If pDT(i)(5) IsNot DBNull.Value AndAlso pDT(i)(5) IsNot Nothing Then
                    If IsNumeric(pDT(i)(5)) = False Then
                        Throw New Exception("El largo debe ser un valor númerico. Fila " & i)
                    Else
                        ObjPP.Largo = CDbl(pDT(i)(5))
                    End If
                End If
                If pDT(i)(6) IsNot DBNull.Value AndAlso pDT(i)(6) IsNot Nothing Then
                    If IsNumeric(pDT(i)(6)) = False Then
                        Throw New Exception("El ancho debe ser un valor númerico. Fila " & i + 1)
                    Else
                        ObjPP.Ancho = CDbl(pDT(i)(6))
                    End If
                End If
                If pDT(i)(7) IsNot DBNull.Value AndAlso pDT(i)(7) IsNot Nothing Then
                    If IsNumeric(pDT(i)(7)) = False Then
                        Throw New Exception("El factor debe ser un valor númerico. Fila " & i + 1)
                    Else
                        ObjPP.Factor = CDbl(pDT(i)(7))
                    End If
                End If
                If pDT(i)(8) IsNot DBNull.Value AndAlso pDT(i)(8) IsNot Nothing Then
                    If IsNumeric(pDT(i)(8)) = False Then
                        Throw New Exception("El mínimo de existencia debe ser un valor númerico entero. Fila " & i + 1)
                    Else
                        ObjPP.MinimoExistencia = CInt(pDT(i)(8))
                    End If
                End If
                If pDT(i)(9) IsNot DBNull.Value AndAlso pDT(i)(9) IsNot Nothing Then
                    If IsNumeric(pDT(i)(9)) = False Then
                        Throw New Exception("El máximo de existencia debe ser un valor númerico entero. Fila " & i + 1)

                    Else
                        ObjPP.MaximoExistencia = CInt(pDT(i)(9))
                    End If
                End If

                ' si el índice es 0 es porque el id del producto viene en el excel
                If pcmbP.SelectedIndex = 0 Then
                    If IsNumeric(pDT(i)(10)) = False Then
                        Throw New Exception("El ID Producto debe ser un valor númerico entero. Fila " & i + 1)
                    Else
                        ObjPP.IdProducto = CInt(pDT(i)(10))
                    End If
                    ' de lo contrario si el índice es 1 es porque viene el código del producto en el excel
                ElseIf pcmbP.SelectedIndex = 1 Then

                    Dim lId As Integer = clsLnProducto.Get_IdProducto_By_Codigo(CStr(pDT(i)(10)))
                    If lId = 0 Then
                        Throw New Exception(String.Format("El producto con código {0} no existe.", pDT(i)(10)))
                    ElseIf lId > 0 Then
                        ObjPP.IdProducto = lId
                    End If

                End If

                ObjPP.Nombre = CStr(pDT(i)(1))

                ObjPP.Activo = True
                ObjPP.User_agr = AP.UsuarioAp.IdUsuario
                ObjPP.Fec_agr = Now
                ObjPP.User_mod = AP.UsuarioAp.IdUsuario
                ObjPP.Fec_mod = Now
                pListObjPP.Add(ObjPP)

            Next

            If pListObjPP IsNot Nothing AndAlso pListObjPP.Count > 0 Then
                clsLnProducto_presentacion.Insert_Multiple(pListObjPP)
                XtraMessageBox.Show("Importación realizada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Listar.Invoke()
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Function CargaUnidadMedida(ByVal pDT As DataTable) As Boolean

        Dim pListObjUM As New List(Of clsBeUnidad_medida)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim error_UMBAS As Boolean = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Cursor = Cursors.WaitCursor
            errores = False

            Dim IdPropietario = CInt(pcmbP.SelectedValue)

            For i As Integer = 0 To pDT.Rows.Count - 1

                Dim Indice As Integer = i

                Application.DoEvents()

                Dim BeUmbas As New clsBeUnidad_medida() With {.Propietario = New clsBePropietarios}

                '#GT28112023: se valida el nombre de UMBAS por valor numerico o alfanumerico
                If pDT(i)(2) IsNot DBNull.Value AndAlso pDT(i)(2) IsNot Nothing Then

                    Dim lIdExiste As Boolean = pListObjUM.Exists(Function(d) d.Nombre = pDT(Indice)(2))
                    If lIdExiste Then
                        error_UMBAS = True
                        clsPublic.Actualizar_Progreso(lblPrg, "ERROR: El nombre de la unidad de medida se encuentra duplicado, fila" & i + 1)
                    Else

                        Dim pNombreUMBAS = CStr(pDT(i)(2))

                        If pNombreUMBAS.Length > 75 Then
                            error_UMBAS = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: El Nombre excede el tamaño permitido. Fila " & i + 1)
                        Else

                            pNombreUMBAS = clsPublic.Quitar_Caracteres_No_Permitidos(pNombreUMBAS)
                            Dim pUmbas As clsBeUnidad_medida = clsLnUnidad_medida.Existe_By_Nombre_By_IdPropietario(pNombreUMBAS.Trim,
                                                                                                                    IdPropietario,
                                                                                                                    lConnection,
                                                                                                                    lTransaction)
                            If Not pUmbas Is Nothing Then
                                error_UMBAS = True
                                clsPublic.Actualizar_Progreso(lblPrg, "ERROR: El nombre de unidad de medida ya existe en WMS, fila: " & i + 1)
                            Else
                                BeUmbas.Nombre = pNombreUMBAS
                            End If

                        End If

                    End If

                Else
                    error_UMBAS = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Aviso: No existe un nombre para la unidad de medida, fila: " & i + 1)
                End If

                '#GT28112023: se valida el código de UMBAS por valor numerico o alfanumerico
                If pDT(i)(8) IsNot DBNull.Value AndAlso pDT(i)(8) IsNot Nothing Then

                    Dim lIdExiste As Boolean = pListObjUM.Exists(Function(d) d.Codigo = pDT(Indice)(8))
                    If lIdExiste Then
                        error_UMBAS = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Aviso: El código de la unidad de medida se encuentra duplicado, fila" & i + 1)
                    Else

                        Dim pCodigoUMBAS = CStr(pDT(i)(8))
                        pCodigoUMBAS = clsPublic.Quitar_Caracteres_No_Permitidos(pCodigoUMBAS)
                        Dim pUmbas As clsBeUnidad_medida = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(pCodigoUMBAS.Trim,
                                                                                                                 IdPropietario,
                                                                                                                 lConnection,
                                                                                                                 lTransaction)
                        If Not pUmbas Is Nothing Then
                            error_UMBAS = True
                            clsPublic.Actualizar_Progreso(lblPrg, "AVISO: El código de unidad de medida ya existe en WMS, fila: " & i + 1)
                        Else
                            BeUmbas.Codigo = pCodigoUMBAS
                        End If

                    End If
                Else
                    error_UMBAS = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Aviso: No existe un código para la unidad de medida, fila: " & i + 1)
                End If

                '#GT28112023: se valida es_um_cobro
                If pDT(i)(9) IsNot DBNull.Value AndAlso pDT(i)(9) IsNot Nothing Then
                    If IsNumeric(pDT(i)(9)) Then
                        BeUmbas.es_um_cobro = pDT(i)(9)
                    Else
                        error_UMBAS = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Aviso: el valor debe ser 1 o 0, fila: " & i + 1)
                    End If
                Else
                    error_UMBAS = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Aviso: ES_UM_COBRO debe ser un valor de 1 o 0, fila: " & i + 1)
                End If

                '#GT28112023: se valida el factor
                If pDT(i)(10) IsNot DBNull.Value AndAlso pDT(i)(10) IsNot Nothing Then

                    If IsNumeric(pDT(i)(10)) Then
                        BeUmbas.factor = pDT(i)(10)
                    Else
                        error_UMBAS = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Aviso: Factor debe ser un valor númerico entero o decimal, fila: " & i + 1)
                    End If
                Else
                    error_UMBAS = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Aviso: el valor debe ser númerico entero o decimal, fila: " & i + 1)
                End If

                BeUmbas.IsNew = True
                BeUmbas.Propietario.IdPropietario = CInt(pcmbP.SelectedValue)
                BeUmbas.IdPropietario = CInt(pcmbP.SelectedValue)
                BeUmbas.Activo = True
                BeUmbas.User_agr = AP.UsuarioAp.IdUsuario
                BeUmbas.Fec_agr = Now
                BeUmbas.User_mod = AP.UsuarioAp.IdUsuario
                BeUmbas.Fec_mod = Now
                pListObjUM.Add(BeUmbas)

            Next


            If Not error_UMBAS Then

                If pListObjUM IsNot Nothing AndAlso pListObjUM.Count > 0 Then
                    clsLnUnidad_medida.Guardar_Transaccion(pListObjUM, lConnection, lTransaction)
                    CargaUnidadMedida = True
                    errores = error_UMBAS
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Importación realizada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

                lTransaction.Commit()
            Else
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("Importación fallida, por favor revise el log.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                lTransaction.Rollback()
            End If

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            Cursor = Cursors.Default
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Dim Contador As Integer = 0
    Private Function CargaProducto(ByRef pDT As DataTable) As Boolean

        CargaProducto = False

        Dim pListOfBeProducto As New List(Of clsBeProducto)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lBodegas As New List(Of clsBeBodega)

        Dim BeProductoBodega As New clsBeProducto_bodega
        Dim Contador = 0
        Dim vIdProductoTran = 0

        Dim error_producto As Boolean = False

        Dim BeProducto As New clsBeProducto

        Try

            If pcmbP.SelectedValue > 0 Then

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                Cursor = Cursors.WaitCursor
                Contador = 0

                '#EJC20210901: llenar bodegas (activas) disponibles para asociar el producto_bodega en importación de excel. método tiene_errores_excel.
                lBodegas = clsLnBodega.GetAll(lConnection, lTransaction)

                'GT11082022_1016: obtener el max de producto y solo incrementar dentro del FOR
                vIdProductoTran = clsLnProducto.MaxID(lConnection, lTransaction) + 1

                For i As Integer = 0 To pDT.Rows.Count - 1

                    Dim Indice As Integer = i
                    Contador = Contador + 1

                    Application.DoEvents()

                    BeProducto = New clsBeProducto
                    BeProducto.IdProducto = vIdProductoTran

                    '#GT08032024: tomo el propietario seleccionado del combo
                    Dim IdPropietario As Integer = pcmbP.SelectedValue
                    If Not clsLnPropietarios.Exists(IdPropietario) Then
                        Throw New Exception("el ID del propietario no existe, revise en la fila " & i + 1)
                    Else
                        BeProducto.IdPropietario = CInt(pcmbP.SelectedValue)
                    End If


                    'GT11082022_1530: validar CLASIFICACION por Id o por descripcion
                    If pDT(i)(2) IsNot DBNull.Value AndAlso pDT(i)(2) IsNot Nothing Then
                        If IsNumeric(pDT(i)(2)) Then
                            If clsLnProducto_clasificacion.Exists_By_IdClasificacion_By_IdPropietario(pDT(i)(2), BeProducto.IdPropietario, lConnection, lTransaction) Then
                                BeProducto.IdClasificacion = CInt(pDT(Indice)(2))
                            Else
                                'error_producto = True
                                clsPublic.Actualizar_Progreso(lblPrg, "AVISO_20240724: " & " La clasificación por ID no existe en WMS, Fila: " & i + 1 & " Se intentará por descripción")

                                '#GT11102023: se intenta busqueda tomando el valor como cadena
                                Dim pNombreClasificacion = CStr(pDT(Indice)(2))

                                If pNombreClasificacion <> "" Then
                                    Dim pProdClasificacion As clsBeProducto_clasificacion = clsLnProducto_clasificacion.Get_Single_By_Nombre_By_Propietario(pNombreClasificacion.Trim,
                                                                                                                                                            BeProducto.IdPropietario,
                                                                                                                                                            lConnection,
                                                                                                                                                            lTransaction)
                                    If Not pProdClasificacion Is Nothing Then
                                        BeProducto.Clasificacion = New clsBeProducto_clasificacion
                                        BeProducto.Clasificacion.IdClasificacion = pProdClasificacion.IdClasificacion
                                        BeProducto.IdClasificacion = pProdClasificacion.IdClasificacion

                                    Else

                                        clsPublic.Actualizar_Progreso(lblPrg, "AVISO_20240724: " & " La clasificación no existe en WMS, se insertará desde la fila: " & i + 1)

                                        '#GT21062023: sino existe la clasificación por descripción la insertamos
                                        Dim pClasificacion As New clsBeProducto_clasificacion
                                        BeProducto.Clasificacion = New clsBeProducto_clasificacion
                                        pClasificacion.IdClasificacion = clsLnProducto_clasificacion.MaxId(lConnection, lTransaction)
                                        pClasificacion.Nombre = pNombreClasificacion.Trim
                                        pClasificacion.Propietario.IdPropietario = BeProducto.IdPropietario
                                        pClasificacion.Sistema = False
                                        pClasificacion.IsNew = True
                                        pClasificacion.Activo = True
                                        pClasificacion.Fec_agr = Now
                                        pClasificacion.User_agr = AP.UsuarioAp.IdUsuario
                                        pClasificacion.User_mod = AP.UsuarioAp.IdUsuario
                                        pClasificacion.Fec_mod = Now

                                        clsLnProducto_clasificacion.Insertar(pClasificacion, lConnection, lTransaction)
                                        BeProducto.Clasificacion = pClasificacion
                                        BeProducto.IdClasificacion = pClasificacion.IdClasificacion

                                    End If
                                Else
                                    BeProducto.IdClasificacion = 0
                                End If


                            End If
                        Else
                            Dim pNombreClasificacion = CStr(pDT(Indice)(2))

                            If pNombreClasificacion <> "" Then
                                Dim pProdClasificacion As clsBeProducto_clasificacion = clsLnProducto_clasificacion.Get_Single_By_Nombre_By_Propietario(pNombreClasificacion.Trim,
                                                                                                                                                        BeProducto.IdPropietario,
                                                                                                                                                        lConnection,
                                                                                                                                                        lTransaction)
                                If Not pProdClasificacion Is Nothing Then
                                    BeProducto.Clasificacion = New clsBeProducto_clasificacion
                                    BeProducto.Clasificacion.IdClasificacion = pProdClasificacion.IdClasificacion
                                    BeProducto.IdClasificacion = pProdClasificacion.IdClasificacion
                                Else

                                    clsPublic.Actualizar_Progreso(lblPrg, "AVISO_20240724: " & " La clasificación no existe en WMS, se insertará desde la fila: " & i + 1)

                                    '#GT21062023: sino existe la clasificación por descripción la insertamos
                                    Dim pClasificacion As New clsBeProducto_clasificacion
                                    BeProducto.Clasificacion = New clsBeProducto_clasificacion
                                    pClasificacion.IdClasificacion = clsLnProducto_clasificacion.MaxId(lConnection, lTransaction)
                                    pClasificacion.Nombre = pNombreClasificacion.Trim
                                    pClasificacion.Propietario.IdPropietario = BeProducto.IdPropietario
                                    pClasificacion.Sistema = False
                                    pClasificacion.IsNew = True
                                    pClasificacion.Activo = True
                                    pClasificacion.Fec_agr = Now
                                    pClasificacion.User_agr = AP.UsuarioAp.IdUsuario
                                    pClasificacion.User_mod = AP.UsuarioAp.IdUsuario
                                    pClasificacion.Fec_mod = Now

                                    clsLnProducto_clasificacion.Insertar(pClasificacion, lConnection, lTransaction)
                                    BeProducto.Clasificacion = pClasificacion
                                    BeProducto.IdClasificacion = pClasificacion.IdClasificacion

                                End If
                            Else
                                BeProducto.IdClasificacion = 0
                            End If


                        End If
                    End If


                    'GT10082022_1730: validar FAMILIA por Id, sino, por descripción
                    If pDT(i)(3) IsNot DBNull.Value Then
                        If IsNumeric(pDT(i)(3)) Then
                            If clsLnProducto_familia.Exists(pDT(i)(3), lConnection, lTransaction) Then
                                BeProducto.IdFamilia = CInt(pDT(i)(3))
                            Else
                                error_producto = True
                                clsPublic.Actualizar_Progreso(lblPrg, "Error: " & " La familia por ID, no existe en WMS. Fila" & i + 1)
                            End If
                        Else
                            Dim pNombreFamilia = CStr(pDT(i)(3))

                            If pNombreFamilia <> "" Then

                                Dim pProdFamilia As clsBeProducto_familia = clsLnProducto_familia.Get_Single_By_Nombre_By_IdPropietario(pNombreFamilia.Trim,
                                                                                                                                        BeProducto.IdPropietario,
                                                                                                                                        lConnection,
                                                                                                                                        lTransaction)
                                If Not pProdFamilia Is Nothing Then
                                    BeProducto.Familia = New clsBeProducto_familia
                                    BeProducto.Clasificacion.IdClasificacion = pProdFamilia.IdFamilia
                                    BeProducto.IdFamilia = pProdFamilia.IdFamilia
                                Else

                                    clsPublic.Actualizar_Progreso(lblPrg, "Aviso: " & " La Familia no existe en WMS, se insertará desde la fila: " & i + 1)

                                    Dim pFamilia As New clsBeProducto_familia
                                    BeProducto.Familia = New clsBeProducto_familia
                                    pFamilia.IdFamilia = clsLnProducto_familia.MaxId(lConnection, lTransaction)
                                    pFamilia.Propietario.IdPropietario = BeProducto.IdPropietario
                                    pFamilia.Nombre = pNombreFamilia.Trim
                                    pFamilia.Activo = True
                                    pFamilia.Fec_agr = Now
                                    pFamilia.User_agr = AP.UsuarioAp.IdUsuario
                                    pFamilia.User_mod = AP.UsuarioAp.IdUsuario
                                    pFamilia.Fec_mod = Now
                                    pFamilia.IsNew = True

                                    clsLnProducto_familia.Insertar(pFamilia, lConnection, lTransaction)
                                    BeProducto.Familia = pFamilia
                                    BeProducto.IdFamilia = pFamilia.IdFamilia

                                End If

                            Else
                                BeProducto.IdFamilia = 0
                            End If

                        End If

                    End If


                    'GT10082022_1730: validar MARCA por Id, sino, por descripción
                    If pDT(i)(4) IsNot DBNull.Value Then
                        If IsNumeric(pDT(i)(4)) Then
                            If clsLnProducto_marca.Exists(pDT(i)(4), lConnection, lTransaction) Then
                                BeProducto.IdMarca = CInt(pDT(i)(4))
                            Else
                                'Throw New Exception("La marca por ID, no existe en WMS. Fila " & i + 1)
                                error_producto = True
                                clsPublic.Actualizar_Progreso(lblPrg, "Error: " & " La Marca por ID, no existe en WMS. Fila" & i + 1)
                            End If
                        Else
                            Dim pNombreMarca = CStr(pDT(i)(4))

                            If pNombreMarca <> "" Then

                                Dim pProdMarca As clsBeProducto_marca = clsLnProducto_marca.Get_Single_By_Nombre_By_IdPropietario(pNombreMarca.Trim,
                                                                                                                                  BeProducto.IdPropietario,
                                                                                                                                  lConnection,
                                                                                                                                  lTransaction)
                                If Not pProdMarca Is Nothing Then
                                    BeProducto.Marca = New clsBeProducto_marca
                                    BeProducto.Marca.IdMarca = pProdMarca.IdMarca
                                    BeProducto.IdMarca = pProdMarca.IdMarca
                                Else

                                    clsPublic.Actualizar_Progreso(lblPrg, "Aviso: " & " La Marca no existe en WMS, se insertará desde la fila: " & i + 1)

                                    Dim pMarca As New clsBeProducto_marca()

                                    BeProducto.Marca = New clsBeProducto_marca
                                    pMarca.IdMarca = clsLnProducto_marca.MaxID(lConnection, lTransaction)
                                    pMarca.IdPropietario = BeProducto.IdPropietario
                                    pMarca.Nombre = pNombreMarca.Trim
                                    pMarca.Activo = True
                                    pMarca.Fec_agr = Now
                                    pMarca.User_agr = AP.UsuarioAp.IdUsuario
                                    pMarca.User_mod = AP.UsuarioAp.IdUsuario
                                    pMarca.Fec_mod = Now
                                    pMarca.IsNew = True
                                    clsLnProducto_marca.Insertar(pMarca, lConnection, lTransaction)
                                    BeProducto.Marca = pMarca
                                    BeProducto.IdMarca = pMarca.IdMarca

                                End If

                            Else
                                BeProducto.IdMarca = 0
                            End If

                        End If

                    End If


                    'GT10082022_1730: validar TIPO por Id, sino, por descripción
                    If pDT(i)(5) IsNot DBNull.Value Then
                        If IsNumeric(pDT(i)(5)) Then
                            If clsLnProducto_tipo.Exists(pDT(i)(5), lConnection, lTransaction) Then
                                BeProducto.IdTipoProducto = CInt(pDT(i)(5))
                            Else
                                'Throw New Exception("El Tipo Producto por ID, no existe en WMS. Fila " & i + 1)
                                error_producto = True
                                clsPublic.Actualizar_Progreso(lblPrg, "Error: " & " El Tipo Producto por ID, no existe en WMS. Fila" & i + 1)
                            End If
                        Else
                            Dim pNombreTipo = CStr(pDT(i)(5))

                            If pNombreTipo <> "" Then

                                Dim pProdTipo As clsBeProducto_tipo = clsLnProducto_tipo.Get_Single_By_Nombre_By_IdPropietario(pNombreTipo.Trim,
                                                                                                                               BeProducto.IdPropietario,
                                                                                                                               lConnection,
                                                                                                                               lTransaction)
                                If Not pProdTipo Is Nothing Then
                                    BeProducto.TipoProducto = New clsBeProducto_tipo
                                    BeProducto.TipoProducto.IdTipoProducto = pProdTipo.IdTipoProducto
                                    BeProducto.IdTipoProducto = pProdTipo.IdTipoProducto
                                Else

                                    clsPublic.Actualizar_Progreso(lblPrg, "Aviso: " & " El Tipo Producto no existe en WMS, se insertará desde la fila: " & i + 1)

                                    Dim pProducto_tipo = New clsBeProducto_tipo
                                    BeProducto.TipoProducto = New clsBeProducto_tipo
                                    pProducto_tipo.IdTipoProducto = clsLnProducto_tipo.MaxID(lConnection, lTransaction)
                                    pProducto_tipo.IdPropietario = BeProducto.IdPropietario
                                    pProducto_tipo.NombreTipoProducto = pNombreTipo.Trim
                                    pProducto_tipo.Codigo = BeProducto.TipoProducto.IdTipoProducto
                                    pProducto_tipo.Activo = True
                                    pProducto_tipo.Fec_agr = Now
                                    pProducto_tipo.User_agr = AP.UsuarioAp.IdUsuario
                                    pProducto_tipo.User_mod = AP.UsuarioAp.IdUsuario
                                    pProducto_tipo.Fec_mod = Now
                                    clsLnProducto_tipo.Insertar(pProducto_tipo, lConnection, lTransaction)
                                    BeProducto.TipoProducto = pProducto_tipo
                                    BeProducto.IdTipoProducto = pProducto_tipo.IdTipoProducto

                                End If
                            Else

                                BeProducto.IdTipoProducto = 0

                            End If

                        End If

                    End If


                    If pDT(i)(6) IsNot DBNull.Value AndAlso pDT(i)(6) IsNot Nothing Then
                        If IsNumeric(pDT(i)(6)) Then
                            If clsLnUnidad_medida.Exists(pDT(i)(6), lConnection, lTransaction) Then
                                BeProducto.IdUnidadMedidaBasica = CInt(pDT(Indice)(6))
                            Else
                                error_producto = True
                                clsPublic.Actualizar_Progreso(lblPrg, "Error: " & " La unidad de medida por ID, no existe en WMS. Fila" & i + 1)
                            End If
                        Else
                            Dim pNombreUmbas = CStr(pDT(i)(6))
                            Dim pUmbas As clsBeUnidad_medida = clsLnUnidad_medida.Existe_By_Nombre_By_IdPropietario(pNombreUmbas.Trim,
                                                                                                                    BeProducto.IdPropietario,
                                                                                                                    lConnection,
                                                                                                                    lTransaction)
                            If Not pUmbas Is Nothing Then
                                BeProducto.UnidadMedida = New clsBeUnidad_medida
                                BeProducto.UnidadMedida.IdUnidadMedida = pUmbas.IdUnidadMedida
                                BeProducto.IdUnidadMedidaBasica = pUmbas.IdUnidadMedida
                            Else
                                error_producto = True
                                clsPublic.Actualizar_Progreso(lblPrg, "Error: " & " La unidad de medida no existe en WMS. Fila" & i + 1)
                            End If
                        End If
                    End If


                    If pDT(i)(13) Is DBNull.Value AndAlso pDT(i)(13) Is Nothing Then
                        error_producto = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error: " & " Falta el código del producto en la fila: " & i + 1)
                    Else
                        Dim tam_codigo = CStr(pDT(i)(13))
                        If tam_codigo.Length > 50 Then
                            error_producto = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: " & " El codigo excede el tamaño permitido. Fila: " & i + 1)
                        Else

                            Dim pProducto As clsBeProducto = clsLnProducto.Existe(tam_codigo.Trim, lConnection, lTransaction)

                            If pProducto Is Nothing Then
                                Dim lIdExiste As Boolean = pListOfBeProducto.Exists(Function(d) d.Codigo = tam_codigo)
                                If lIdExiste Then
                                    error_producto = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El código del producto de la fila " & i + 1 & " se encuentra duplicado.")
                                Else
                                    BeProducto.Codigo = CStr(pDT(i)(13))
                                End If
                            Else
                                error_producto = True
                                clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El código del producto " & pProducto.Codigo & " ya esta registrado en la BD. Fila " & i + 1)
                            End If
                        End If
                    End If

                    If pDT(i)(14) Is DBNull.Value AndAlso pDT(i)(14) Is Nothing Then
                        error_producto = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error: Falta Nombre del producto. Fila " & i + 1)
                    Else

                        Dim tam_nombre = CStr(pDT(i)(14))
                        If tam_nombre.Length > 100 Then
                            error_producto = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: El Nombre excede el tamaño permitido. Fila " & i + 1)
                        Else
                            BeProducto.Nombre = CStr(pDT(i)(14))
                        End If
                    End If

                    If pDT(i)(15) Is DBNull.Value Then
                        error_producto = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El codigo barras de la fila " & i + 1 & " esta vacio.")
                    Else

                        Dim tam_barra = CStr(pDT(i)(15))
                        If tam_barra.Length > 35 Then
                            error_producto = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El código de barra excede el tamaño permitido.  Fila " & i + 1)
                        Else
                            BeProducto.Codigo_barra = CStr(pDT(i)(15))
                        End If
                    End If


                    If pDT(i)(16) IsNot DBNull.Value AndAlso pDT(i)(16) IsNot Nothing Then
                        If IsNumeric(pDT(i)(16)) = False Then
                            error_producto = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El precio debe ser un valor númerico. Fila " & i + 1)
                        Else
                            BeProducto.Precio = CDbl(pDT(i)(16))
                        End If
                    End If


                    If pDT(i)(17) IsNot DBNull.Value AndAlso pDT(i)(17) IsNot Nothing Then
                        If IsNumeric(pDT(i)(17)) = False Then
                            error_producto = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "La existencia mínima debe ser un valor númerico. Fila " & i + 1)
                        Else
                            BeProducto.Existencia_min = CDbl(pDT(i)(17))
                        End If
                    End If

                    If pDT(i)(18) IsNot DBNull.Value AndAlso pDT(i)(18) IsNot Nothing Then
                        If IsNumeric(pDT(i)(18)) = False Then
                            error_producto = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "La existencia máxima debe ser un valor númerico. Fila " & i + 1)
                        Else
                            BeProducto.Existencia_max = CDbl(pDT(i)(18))
                        End If
                    End If

                    If pDT(i)(19) IsNot DBNull.Value AndAlso pDT(i)(19) IsNot Nothing Then
                        If IsNumeric(pDT(i)(19)) = False Then
                            'Throw New Exception("El costo debe ser un valor númerico. Fila " & i + 1)
                            error_producto = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El costo debe ser un valor númerico. Fila " & i + 1)
                        Else
                            BeProducto.Costo = CDbl(pDT(i)(19))
                        End If
                    End If

                    'GT10082022_1745: falta validar con el control peso para agregar o no la referencia
                    If pDT(i)(20) IsNot DBNull.Value AndAlso pDT(i)(20) IsNot Nothing Then
                        If IsNumeric(pDT(i)(20)) Then
                            BeProducto.Peso_referencia = CDbl(pDT(i)(20))
                        End If
                    End If

                    'GT10082022_1745: falta validar con el control peso para agregar o no la referencia
                    If pDT(i)(21) IsNot DBNull.Value AndAlso pDT(i)(21) IsNot Nothing Then
                        If IsNumeric(pDT(i)(21)) Then
                            BeProducto.Peso_tolerancia = CDbl(pDT(i)(21))
                        End If
                    End If

                    'GT10082022_1745: falta validar con el control temperatura para agregar o no la referencia
                    If pDT(i)(22) IsNot DBNull.Value AndAlso pDT(i)(22) IsNot Nothing Then
                        If IsNumeric(pDT(i)(22)) Then
                            BeProducto.Temperatura_referencia = CDbl(pDT(i)(22))
                        End If
                    End If

                    'GT10082022_1745: falta validar con el control temperatura para agregar o no la referencia
                    If pDT(i)(23) IsNot DBNull.Value AndAlso pDT(i)(23) IsNot Nothing Then
                        If IsNumeric(pDT(i)(23)) Then
                            BeProducto.Temperatura_tolerancia = CDbl(pDT(i)(23))
                        End If
                    End If

                    'GT 01092021: el valor Activo del producto es por defecto true, aca solo se deja de referencia.
                    If pDT(i)(24) IsNot DBNull.Value AndAlso pDT(i)(24) IsNot Nothing Then

                    End If


                    If pDT(i)(25) IsNot DBNull.Value AndAlso pDT(i)(25) IsNot Nothing Then
                        BeProducto.Serializado = CBool(pDT(i)(25))
                    End If


                    If pDT(i)(26) IsNot DBNull.Value AndAlso pDT(i)(26) IsNot Nothing Then
                        BeProducto.Genera_lote = CBool(pDT(i)(26))
                    End If

                    If pDT(i)(27) IsNot DBNull.Value AndAlso pDT(i)(27) IsNot Nothing Then
                        BeProducto.Genera_lp = CBool(pDT(i)(27))
                    End If

                    If pDT(i)(28) IsNot DBNull.Value AndAlso pDT(i)(28) IsNot Nothing Then
                        BeProducto.Control_vencimiento = CBool(pDT(i)(28))
                    End If

                    If pDT(i)(29) IsNot DBNull.Value AndAlso pDT(i)(29) IsNot Nothing Then
                        BeProducto.Control_lote = CBool(pDT(i)(29))
                    End If

                    If pDT(i)(30) IsNot DBNull.Value AndAlso pDT(i)(30) IsNot Nothing Then
                        BeProducto.Peso_recepcion = CBool(pDT(i)(30))
                    End If


                    If pDT(i)(31) IsNot DBNull.Value AndAlso pDT(i)(31) IsNot Nothing Then
                        BeProducto.Peso_despacho = CBool(pDT(i)(31))
                    End If

                    If pDT(i)(32) IsNot DBNull.Value AndAlso pDT(i)(32) IsNot Nothing Then
                        BeProducto.Temperatura_recepcion = CBool(pDT(i)(32))
                    End If

                    If pDT(i)(33) IsNot DBNull.Value AndAlso pDT(i)(33) IsNot Nothing Then
                        BeProducto.Temperatura_despacho = CBool(pDT(i)(33))
                    End If

                    If pDT(i)(34) IsNot DBNull.Value AndAlso pDT(i)(34) IsNot Nothing Then
                        BeProducto.Materia_prima = CBool(pDT(i)(34))
                    End If

                    If pDT(i)(35) IsNot DBNull.Value AndAlso pDT(i)(35) IsNot Nothing Then
                        BeProducto.Kit = CBool(pDT(i)(35))
                    End If

                    If pDT(i)(36) IsNot DBNull.Value AndAlso pDT(i)(36) IsNot Nothing Then
                        If IsNumeric(pDT(i)(36)) = False Then
                            'Throw New Exception("La tolerancia debe ser un valor númerico entero. Fila " & i + 1)
                            error_producto = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "La tolerancia debe ser un valor númerico entero. Fila " & i + 1)
                        Else
                            BeProducto.Tolerancia = CInt(pDT(i)(36))
                        End If
                    End If

                    If pDT(i)(37) IsNot DBNull.Value AndAlso pDT(i)(37) IsNot Nothing Then
                        If IsNumeric(pDT(i)(37)) = False Then
                            'Throw New Exception("El ciclo de vida debe ser un valor númerico entero. Fila " & i + 1)
                            error_producto = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El ciclo de vida debe ser un valor númerico entero. Fila " & i + 1)
                        Else
                            BeProducto.Ciclo_vida = CInt(pDT(i)(37))
                        End If
                    End If

                    BeProducto.Propietario = New clsBePropietarios
                    BeProducto.Arancel = New clsBeArancel

                    If pDT(i)(7) IsNot DBNull.Value Then
                        If IsNumeric(pDT(i)(7)) Then
                            BeProducto.IdCamara = CInt(pDT(i)(7))
                        End If
                    End If

                    If pDT(i)(8) IsNot DBNull.Value AndAlso pDT(i)(8) IsNot Nothing Then

                        If String.IsNullOrEmpty(pDT(i)(8)) Then
                            error_producto = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El Id de Rotación no puede estar vacio. Fila " & i + 1)
                        Else

                            If IsNumeric(pDT(i)(8)) = True Then
                                Dim IdRotacion = pDT(i)(8)
                                If clsLnTipo_rotacion.Exists_rotacion_By_IdRotacion(IdRotacion, lConnection, lTransaction) Then
                                    BeProducto.IdTipoRotacion = IdRotacion
                                Else
                                    error_producto = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El Id de Rotación no es valido para el producto. Fila " & i + 1)
                                End If
                            Else
                                '#GT08032024: buscamos por descripcion si no ingresaron el IdRotación numerico
                                Dim Rotacion = pDT(i)(8).ToString()
                                Dim IdRotacion = clsLnTipo_rotacion.GetIdTipoRotacionByNombre(Rotacion, lConnection, lTransaction)
                                If IdRotacion > 0 Then
                                    BeProducto.IdTipoRotacion = IdRotacion
                                Else
                                    error_producto = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "La descripción de Rotación no es valida para el producto. Fila " & i + 1)
                                End If
                            End If
                        End If
                    Else
                        error_producto = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "Debe asignar un Id de tipo Rotación para el producto. Fila " & i + 1)
                    End If

                    If pDT(i)(9) IsNot DBNull.Value Then
                        BeProducto.IdPerfilSerializado = clsLnPerfil_serializado.GetIdPerfilSerializadoByNombre(pDT(i)(9).ToString, lConnection, lTransaction)
                    End If

                    If pDT(i)(10) IsNot DBNull.Value Then
                        BeProducto.IdIndiceRotacion = clsLnIndice_rotacion.GetIdIndiceRotacionByNombre(pDT(i)(10).ToString, lConnection, lTransaction)
                    End If

                    If pDT(i)(12) IsNot DBNull.Value Then
                        If IsNumeric(pDT(i)(12)) Then
                            BeProducto.Arancel.IdArancel = CInt(pDT(i)(12))
                        End If

                    End If

                    'GT10082022_1800: se valida por Id o por la descripcion del parámetroA
                    If pDT(i)(53) IsNot DBNull.Value AndAlso pDT(i)(53) IsNot Nothing Then
                        If IsNumeric(pDT(i)(53)) Then

                            If clsLnProducto_parametro_a.Existe_Parametro_By_Id(pDT(i)(53),
                                                                                lConnection,
                                                                                lTransaction) Then
                                BeProducto.IdProductoParametroA = CInt(pDT(i)(53))
                            Else
                                'Throw New Exception("El parámetro A: " & pDT(i)(53).ToString() & " no existe en WMS. Fila " & i + 1)
                                error_producto = True
                                clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El parámetro A: " & pDT(i)(53).ToString() & " no existe en WMS. Fila " & i + 1)
                            End If
                        Else
                            Dim pProdParametroA = CStr(pDT(i)(53))
                            Dim pParametroA As clsBeProducto_parametro_a = clsLnProducto_parametro_a.GetSingle_By_Name(pProdParametroA.Trim,
                                                                                                                       lConnection,
                                                                                                                       lTransaction)

                            If Not pParametroA Is Nothing Then
                                BeProducto.IdProductoParametroA = pParametroA.IdProductoParametroA
                            Else
                                'Throw New Exception("El parámetro A: " & pProdParametroA & " no existe en WMS. Fila " & i + 1)
                                error_producto = True
                                clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El parámetro A: " & pProdParametroA & " no existe en WMS. Fila " & i + 1)
                            End If

                        End If
                    End If

                    'GT10082022_1800: se valida por Id o por la descripcion del parámetroB
                    If pDT(i)(54) IsNot DBNull.Value AndAlso pDT(i)(54) IsNot Nothing Then
                        If IsNumeric(pDT(i)(54)) Then

                            If clsLnProducto_parametro_b.Existe_Parametro_By_Id(pDT(i)(54),
                                                                                lConnection,
                                                                                lTransaction) Then
                                BeProducto.IdProductoParametroB = CInt(pDT(i)(54))
                            Else
                                error_producto = True
                                clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El parámetro B: " & pDT(i)(54).ToString() & " no esta registrado en WMS. Fila " & i + 1)
                            End If
                        Else
                            Dim pProdParametroB = CStr(pDT(i)(54))
                            Dim pParametroB As clsBeProducto_parametro_b = clsLnProducto_parametro_b.GetSingle_By_Name(pProdParametroB.Trim,
                                                                                                                       lConnection,
                                                                                                                       lTransaction)

                            If Not pParametroB Is Nothing Then
                                BeProducto.IdProductoParametroB = pParametroB.IdProductoParametroB
                            Else
                                error_producto = True
                                clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "El parámetro B: " & pProdParametroB & " no esta registrado en WMS. Fila " & i + 1)
                            End If
                        End If
                    End If


                    'Parametrización de la bodega
                    Dim ObjConfigInt As New clsBeI_nav_config_enc
                    Dim pIdConfiguracion = clsLnI_nav_config_enc.Get_IdConfiguracion(AP.IdBodega, AP.IdEmpresa)

                    If pIdConfiguracion = 0 Then
                        clsPublic.Actualizar_Progreso(lblPrg, "Aviso: " & "La bodega: " & AP.IdBodega & " de la empresa " & AP.NomEmpresa & " no tiene definida interface para definir tipo de etiqueta. Fila " & i + 1)
                    Else
                        ObjConfigInt = clsLnI_nav_config_enc.GetSingle(pIdConfiguracion, lConnection, lTransaction)
                    End If



                    BeProducto.Activo = True
                    BeProducto.User_agr = AP.UsuarioAp.IdUsuario
                    BeProducto.Fec_agr = Now
                    BeProducto.User_mod = AP.UsuarioAp.IdUsuario
                    BeProducto.Fec_mod = Now
                    BeProducto.IdTipoEtiqueta = IIf(ObjConfigInt.IdTipoEtiqueta = 0, 0, ObjConfigInt.IdTipoEtiqueta)

                    pListOfBeProducto.Add(BeProducto)

                    vIdProductoTran += 1

                Next


                If Not error_producto Then

                    If pListOfBeProducto IsNot Nothing AndAlso pListOfBeProducto.Count > 0 Then

                        clsLnProducto.Guardar_Transaccion(pListOfBeProducto, lConnection, lTransaction)

                        '#EJC20210901_B: Insertar producto bodega por cada bodega importación excel. (Nuevo producto)
                        For Each pProducto In pListOfBeProducto

                            For Each Bodega In lBodegas

                                BeProductoBodega = New clsBeProducto_bodega
                                BeProductoBodega.IdProductoBodega = clsLnProducto_bodega.MaxID(lConnection, lTransaction) + Contador
                                BeProductoBodega.IdProducto = pProducto.IdProducto
                                BeProductoBodega.IdBodega = Bodega.IdBodega
                                BeProductoBodega.Activo = True
                                BeProductoBodega.Sistema = False
                                BeProductoBodega.User_agr = AP.UsuarioAp.IdUsuario
                                BeProductoBodega.Fec_agr = Now
                                BeProductoBodega.User_mod = AP.UsuarioAp.IdUsuario
                                BeProductoBodega.Fec_mod = Now
                                clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega, lConnection, lTransaction)

                            Next

                        Next

                        CargaProducto = error_producto

                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show("Importación realizada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Listar.Invoke()
                    End If

                    lTransaction.Commit()
                Else
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Importación fallida!.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    lTransaction.Rollback()
                End If

            Else
                XtraMessageBox.Show("Propietario no seleccionado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            Cursor = Cursors.Default
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Private Sub SetDatataTable()

        lInvenarioTeorico.Columns.Clear()
        lInvenarioTeorico.Columns.Add("Contador", GetType(Integer))
        lInvenarioTeorico.Columns.Add("EstadoProcesamiento", GetType(String))
        lInvenarioTeorico.Columns.Add("Codigo", GetType(String))
        lInvenarioTeorico.Columns.Add("Presentacion", GetType(String))
        lInvenarioTeorico.Columns.Add("Cantidad", GetType(Double))
        lInvenarioTeorico.Columns.Add("Peso", GetType(Double))
        lInvenarioTeorico.Columns.Add("UM", GetType(String))
        lInvenarioTeorico.Columns.Add("Lote", GetType(String))
        lInvenarioTeorico.Columns.Add("Vence", GetType(Date))
        lInvenarioTeorico.Columns.Add("Ubicacion", GetType(Integer))
        lInvenarioTeorico.Columns.Add("LP", GetType(String))
        lInvenarioTeorico.Columns.Add("CodVariante", GetType(String))
        '#GT24112022_1000: campos DyD
        lInvenarioTeorico.Columns.Add("Costo", GetType(Double))
        lInvenarioTeorico.Columns.Add("Precio", GetType(Double))
        lInvenarioTeorico.Columns.Add("Parametro_a", GetType(String))
        lInvenarioTeorico.Columns.Add("Parametro_b", GetType(String))
        lInvenarioTeorico.Columns.Add("Codigo_Area", GetType(String))

    End Sub

    Private Function Carga_Inventario_Teorico(ByVal pDT As DataTable) As Boolean

        Carga_Inventario_Teorico = False

        Dim pListObjP As New List(Of clsBeProducto)
        Dim sCantidad As String = ""
        Dim vCodigoProducto As String = ""
        Dim vCantidad, vPeso As Double
        Dim vNomPresentacion As String = ""
        Dim vNomUM As String = ""
        Dim sPeso As String = ""
        Dim vLote As String = ""
        Dim vFechaVence As Date = Now
        Dim sFechaVence As String = ""
        Dim vUbicacion As Integer
        Dim vLicensePlate As String = ""
        Dim vCod_Variante As String = ""
        '#GT24112022_0930: campos DyD
        Dim vPrecio As Double
        Dim vCosto As Double
        Dim vParametro_a As String = ""
        Dim vParametro_b As String = ""
        Dim Codigo_Area_SAP As String = ""

        Try

            Cursor = Cursors.WaitCursor

            Dim vContador As Integer = 1
            Dim vIndice As Integer = 0
            Dim vIndiceCodigoProducto As Integer = 1
            Dim vIndicadorFila As Integer = 1
            Dim Columna_leida As Integer = 1
            Dim errorCampos As Boolean = False
            Dim errorCargaInv As Boolean = False

            'pDT.Clear()

            SetDatataTable()

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Validando estructura...")
            SplashScreenManager.Default.SetWaitFormCaption("Procesando Archivo")

            Dim vCantRegistros As Integer = pDT.Rows.Count

            For i As Integer = 0 To pDT.Rows.Count - 1

                SplashScreenManager.Default.SetWaitFormDescription("Registros " & i + 1 & " de: " & vCantRegistros)

                Dim Indice As Integer = i
                Contador = Contador + 1

                Application.DoEvents()

                If Contador = 113 - 1 Then
                    Debug.WriteLine(" Espera " & Contador)
                End If

                Debug.WriteLine(" Espera " & Contador)

                Dim vCodigoProductoDebug As String = IIf(IsDBNull(pDT(i)(0)), "", pDT(i)(0))

                Debug.Write(" Código: " & vCodigoProducto)

                errorCampos = False

                'EFREN_17052021 valida codigo del producto
                If pDT(i)(0) Is DBNull.Value Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El Código: ( " & vCodigoProductoDebug & " ) producto en la fila " & i + 1 & " no es válido")
                Else

                    Dim Codigo_Producto As String = pDT(Indice)(0)

                    Dim lIdExiste As Boolean = pListObjP.Exists(Function(d) d.Codigo = Codigo_Producto)
                    If lIdExiste Then
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El Código del producto en la fila " & i + 1 & " se encuentra repetido.")
                    Else
                        If Not clsLnProducto.Exist_by_Codigo(pDT(i)(0)) Then
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El código del producto " & pDT(i)(0) & "en la fila " & i + 1 & " no existe en la bd.")
                        End If
                        vCodigoProducto = pDT(i)(0)
                    End If
                End If

                'GT no se valida Presentación, porque esta puede ir vacia, y se valida en el frmInventarioImport
                vNomPresentacion = IIf(pDT(i)(1) Is DBNull.Value, "", Convert.ToString(pDT(i)(1)))

                'GT 17052021 valida cantidad
                If pDT(i)(2) IsNot DBNull.Value AndAlso pDT(i)(2) IsNot Nothing Then
                    If IsNumeric(pDT(i)(2)) = False Then
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "La cantidad debe ser un valor númerico entero. Fila " & i + 1)
                    Else
                        If IsNumeric(pDT(i)(2)) = 0 Then
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "La cantidad de la fila " & i + 1 & " debe ser mayor a 0.")
                        Else
                            vCantidad = Math.Round(CDbl(pDT(i)(2)), 6)
                        End If
                    End If
                End If

                'GT 17052021 se valida que el peso sea al menos =0 o superior
                If pDT(i)(3) IsNot DBNull.Value AndAlso pDT(i)(3) IsNot Nothing Then
                    If IsNumeric(pDT(i)(3)) = False Then
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El Peso debe ser un valor númerico ó por defecto 0. Fila " & i + 1)
                    Else
                        vPeso = IIf(pDT(i)(3) = 0, 0, CDbl(pDT(i)(3)))
                    End If
                Else
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El Peso debe ser un valor númerico ó por defecto 0. Fila " & i + 1)
                End If

                'GT 17052021 se valida que exista al menos un string para la Unidad Medida
                If pDT(i)(4) Is DBNull.Value AndAlso pDT(i)(4) Is Nothing Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "Falta nombre de la unidad de medida (UM) para el producto. Fila " & i + 1)
                Else
                    vNomUM = pDT(i)(4)
                End If

                'EFREN 17052021 se asigna lote, este puede ir vacio.
                vLote = IIf(pDT(i)(5) Is DBNull.Value, "", Convert.ToString(pDT(i)(5)))

                sFechaVence = IIf(IsDBNull(pDT(i)(6)), New Date(1900, 1, 1), pDT(i)(6))

                'EFREN 17052021 valida que fecha vencimiento no este vacia, aunque el parametro como tal, se valida en frmInventarioImport
                If pDT(i)(6) Is DBNull.Value OrElse pDT(i)(6) = "" Then
                    '#CKFK Si la fecha de vencimiento es nula le coloco 01-01-1900
                    vFechaVence = New Date(1900, 1, 1)
                Else
                    Try
                        vFechaVence = Date.Parse(sFechaVence)
                    Catch ex As Exception
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "La fecha vencimiento: " & sFechaVence & " de la fila " & i + 1 & " para el código de producto: " & vCodigoProductoDebug & " no es válida. Si desconoce la fecha, asigne por defecto: 01/01/1900 ")
                    End Try

                End If

                'GT 10112021 valida que la ubicación sea númerica
                If pDT(i)(7) IsNot DBNull.Value AndAlso pDT(i)(7) IsNot Nothing AndAlso pDT(i)(7) <> "" Then
                    If IsNumeric(pDT(i)(7)) = False Then
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "La ubicación debe ser un valor númerico. Fila " & i + 1)
                    Else
                        vUbicacion = CInt(pDT(i)(7))
                    End If
                Else
                    '    lblPrg.AppendText("Error : " & "La fila " & i + 1 & " no tiene una ubicación.")
                    '#CKFK 20211207 Coloqué la ubicación de recepción por defecto
                    vUbicacion = AP.Bodega.Ubic_recepcion
                End If

                'GT0112021: Se obtiene valor para LP y de cod_variante 
                vLicensePlate = IIf(pDT(i)(8) Is DBNull.Value, "", Convert.ToString(pDT(i)(8)))

                vCod_Variante = IIf(pDT(i)(9) Is DBNull.Value, "", Convert.ToString(pDT(i)(9)))

                '#GT24112022_0930: campos DyD 
                'GT 17052021 se valida que el costo sea al menos =0 o superior
                If pDT(i)(10) IsNot DBNull.Value AndAlso pDT(i)(10) IsNot Nothing Then
                    If IsNumeric(pDT(i)(10)) = False Then
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El Costo debe ser un valor númerico ó por defecto 0. Fila " & i + 1)
                    Else
                        vCosto = IIf(pDT(i)(10) = 0, 0, CDbl(pDT(i)(10)))
                    End If
                Else
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El Costo debe ser un valor númerico ó por defecto 0. Fila " & i + 1)
                End If

                'GT 17052021 se valida que el precio sea al menos =0 o superior
                If pDT(i)(11) IsNot DBNull.Value AndAlso pDT(i)(11) IsNot Nothing Then
                    If IsNumeric(pDT(i)(11)) = False Then
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El Precio debe ser un valor númerico ó por defecto 0. Fila " & i + 1)
                    Else
                        vPrecio = IIf(pDT(i)(11) = 0, 0, CDbl(pDT(i)(11)))
                    End If
                Else
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El Precio debe ser un valor númerico ó por defecto 0. Fila " & i + 1)
                End If

                vParametro_a = IIf(pDT(i)(12) Is DBNull.Value, "", Convert.ToString(pDT(i)(12)))
                vParametro_b = IIf(pDT(i)(13) Is DBNull.Value, "", Convert.ToString(pDT(i)(13)))
                Codigo_Area_SAP = IIf(pDT(i)(14) Is DBNull.Value, "", Convert.ToString(pDT(i)(14)))

                If Not errorCampos Then

                    lInvenarioTeorico.Rows.Add(vContador,
                                               "Procesando",
                                               vCodigoProducto,
                                               vNomPresentacion,
                                               vCantidad,
                                               vPeso,
                                               vNomUM.Trim(),
                                               vLote,
                                               vFechaVence,
                                               vUbicacion,
                                               vLicensePlate,
                                               vCod_Variante,
                                               vCosto,
                                               vPrecio,
                                               vParametro_a,
                                               vParametro_b,
                                               Codigo_Area_SAP)

                End If

                If errorCampos Then
                    errorCargaInv = True
                End If

                Application.DoEvents()

            Next

            If lInvenarioTeorico.Rows.Count > 0 And Not errorCargaInv Then

                Carga_Inventario_Teorico = True

                SplashScreenManager.CloseForm(False)

                XtraMessageBox.Show("Se finalizó la lectura del archivo, se validarán los datos a continuación.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                If Not Listar Is Nothing Then
                    Listar.Invoke()
                End If

                DialogResult = DialogResult.OK

            Else

                Carga_Inventario_Teorico = False

                SplashScreenManager.CloseForm(False)

                XtraMessageBox.Show("Se finalizó la lectura del archivo, y tiene errores que deben corregir.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
            Cursor = Cursors.Default
        End Try

    End Function

    Private Sub Carga_Estructura_Bodega()

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            SetDatataTable2()

            Cursor = Cursors.WaitCursor

            Dim Obj As clsExcel = pListObj.Find(Function(s) s.Checked = True)
            Dim fileName As String = txtArchivo.Text
            Dim documento As XLWorkbook = New XLWorkbook(fileName)
            Dim documento1 As IXLWorksheet = documento.Worksheet(Obj.Index + 1)
            Dim DT As New DataTable
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim firstRow As Boolean = True
            Dim Range As IXLRange
            Range = documento1.RangeUsed(XLCellsUsedOptions.Contents)
            Dim firstCol As Integer
            Dim lastCol As Integer


            If documento1.RowsUsed.Count < 2 Then
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("La hoja esta vacía. no contiene datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Procesando Archivo")

            Dim pListBodegaArea As New List(Of clsBeBodega_area)
            Dim vIndicadorFila As Integer = 1
            Dim vFilaDetalle As Integer = 5  'Aqui comienza el detalle de las areas, bodegas
            Dim vIndicadorColumna As Integer = 1
            Dim vCodigoBarraScanPoliza As String = ""
            Dim vCodigoTicket As Integer = 0
            Dim vNombreOperador As String = ""
            Dim vValorCelda As String = ""
            Dim vNITConsEnc As String = ""
            Dim vValorCeldaNITCons As String = ""
            Dim vCantColumnasRack As Integer = 0

            DT2.Clear()


            For Each row As IXLRow In documento1.Rows

                Debug.WriteLine("Row its at: " & row.RowNumber & " and vIndicadorFila is at: " & vIndicadorFila)

                Try
                    If row.FirstCellUsed Is Nothing OrElse RowIsEmpty(row) Then
                        'GT22122021: por alguna razón, se salta filas, por eso si nothing falla, valido con RowIsEmpty
                    Else
                        If pFilaVacia Then
                            registro += 1
                        End If

                        firstCol = row.FirstCellUsed().Address.ColumnNumber
                        lastCol = row.LastCellUsed().Address.ColumnNumber
                        If firstCol = 1 Then
                            'La fila tiene datos de bodega al iniciar en col1
                            Valida_Data_Bodega(row, firstCol, lastCol)
                        ElseIf firstCol = 12 Then
                            'la fila tiene datos del rack al iniciar en col12
                            Valida_Data_Rack(row, firstCol, lastCol)
                        Else

                            errores = True

                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel tiene celdas con valores en Fila: " & row.RowNumber & " que no corresponden")

                        End If

                    End If
                Catch ex As Exception
                    clsPublic.Actualizar_Progreso(lblPrg, ex.Message)
                End Try

                vIndicadorFila += 1

            Next

            If Not errores Then

                If DT2.Rows.Count > 0 Then

                    SplashScreenManager.Default.SetWaitFormCaption("Preparando datos a importar...")

                    clsPublic.Actualizar_Progreso(lblPrg, "Verificando datos...")

                    If Importacion_Temp(DT2) Then
                        SplashScreenManager.CloseForm(False)
                        clsPublic.Actualizar_Progreso(lblPrg, "No se pudo realizar la importación del archivo, por favor revise e intente nuevamente.")
                    Else

                        SplashScreenManager.Default.SetWaitFormCaption("Guardando data temporal...")

                        lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                        If ListObjImportBodega IsNot Nothing AndAlso ListObjImportBodega.Count > 0 Then

                            clsLnTrans_bodega_ubicaciones_excel.GuardarTransaccion(ListObjImportBodega,
                                                                                   lConnection,
                                                                                   lTransaction)

                            clsPublic.Actualizar_Progreso(lblPrg, "Datos importados correctamente.")

                        End If

                        lTransaction.Commit()


                        SplashScreenManager.Default.SetWaitFormCaption("Registrando...")
                        If Not Guardar_Bodega() Then Return

                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show("Proceso completado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        'GT 03112021: Se limpian los inputs para evitar que se presione cargar nuevamente e intente sobreescribir
                        txtArchivo.Text = ""
                        Grid.DataSource = Nothing
                        cmdCargar.Enabled = True
                        cmdSalir.Enabled = True

                    End If

                End If
            Else

                SplashScreenManager.CloseForm(False)

                clsPublic.Actualizar_Progreso(lblPrg, "No se puedo realizar la importación del archivo, por favor revise e intente nuevamente.")

                txtArchivo.Text = ""

                ListObjImportBodega.Clear()

                DsExcel.Clear()

                Grid.BeginUpdate()
                Grid.EndUpdate()
                Grid.ForceInitialize()

            End If


        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        Finally
            SplashScreenManager.CloseForm(False)
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Sub

    Private Sub CargaTarima(ByVal pDT As DataTable)

        Dim lista As New List(Of clsBeTarimas)

        Try

            Cursor = Cursors.WaitCursor

            Dim i As Integer = 0

            For Each lRow As DataRow In pDT.Rows

                Application.DoEvents()

                Dim vBeTarima As New clsBeTarimas

                If lRow(0) Is DBNull.Value AndAlso lRow(0) Is Nothing Then
                    Throw New Exception("Falta correlativo de la tarima en la fila " & i + 1)
                End If

                If IsNumeric(lRow(0)) = False Then
                    Throw New Exception("El correlativo de la tarima debe ser númerico entero. Fila " & i + 1)
                Else
                    Dim lIdExiste As Boolean = lista.Exists(Function(d) d.IdTarima = CInt(lRow(0)))
                    If lIdExiste Then
                        Throw New Exception(String.Format("El correlativo de la tarima de la fila {0} se encuentra repetido.", i + 1))
                    Else
                        If Not clsLnTarimas.Exists(lRow(0)) Then
                            vBeTarima.IsNew = True
                        Else
                            vBeTarima.IsNew = False
                        End If
                        vBeTarima.IdTarima = CInt(lRow(0))
                    End If
                End If

                If lRow(1) Is DBNull.Value AndAlso lRow(1) Is Nothing Then
                    Throw New Exception("Falta código de la tarima en la fila " & i + 1)
                End If

                If lRow(2) IsNot DBNull.Value OrElse lRow(2) IsNot Nothing Then
                    vBeTarima.Disponible = CBool(lRow(2))
                End If

                vBeTarima.Codigo = CStr(lRow(1))
                vBeTarima.User_agr = AP.UsuarioAp.IdUsuario
                vBeTarima.Fec_agr = Now
                vBeTarima.User_mod = AP.UsuarioAp.IdUsuario
                vBeTarima.Fec_mod = Now
                vBeTarima.Activo = True
                lista.Add(vBeTarima)

                i += 1

            Next

            If lista.Count > 0 Then
                If clsLnTarimas.GuardarDatos(lista) Then
                    XtraMessageBox.Show("Importación realizada correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Listar.Invoke()
                End If
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cmdCargar_Click(sender As Object, e As EventArgs) Handles cmdCargar.Click

        cmdCargar.Enabled = False

        Try

            pBeproductoRellenadoList.Clear()

            errores = False

            lblPrg.Text = ""
            lblPrg.Refresh()

            If Archivo_Valido() Then

                '#GT14062022_1150: este proceso no itera desde la primera linea del archivo, por eso esta separado.
                If pTipoMantenimiento = "EstructuraBodega" Then

                    Carga_Estructura_Bodega()

                Else

                    If Cargar_Archivo_Excel() Then

                        'EFREN_21072021: Se limpian los inputs para evitar que se presione cargar nuevamente e intente sobreescribir
                        txtArchivo.Text = ""
                        pListObj.Clear()
                        DsExcel.Clear()
                        Grid.BeginUpdate()
                        Grid.EndUpdate()
                        Grid.ForceInitialize()
                    Else

                        'GT19012022: si no se importa, no debe cerrar la ventana para ver errores en el log.
                        'DialogResult = DialogResult.None
                        txtArchivo.Text = ""
                        pListObj.Clear()
                        DsExcel.Clear()
                        Grid.BeginUpdate()
                        Grid.EndUpdate()
                        Grid.ForceInitialize()

                        If errores Then
                            XtraMessageBox.Show("Errores en importación, revisar log.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            cmdCargar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally

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
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El archivo debe de contener por lo menos alguna hoja.")
                    Exit Function
                Else
                    If pListObj.All(Function(b) b.Checked = False) Then
                        ' Throw New Exception("Seleccione una hoja.")
                        errores = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "Seleccione una hoja.")
                        Exit Function
                    End If
                End If

                clsPublic.Actualizar_Progreso(lblPrg, "Archivo validado correctamente: " & Now)

                Archivo_Valido = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Validar_Seleccion_Archivo() As Boolean

        Validar_Seleccion_Archivo = False

        Try

            If String.IsNullOrEmpty(txtArchivo.Text.Trim()) Then
                XtraMessageBox.Show("Seleccione archivo.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Function
            End If

            If IO.File.Exists(txtArchivo.Text.Trim()) = False Then
                XtraMessageBox.Show("El archivo no existe.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Function
            End If

            If pListObj IsNot Nothing Then
                If pListObj.Count > 0 = False Then
                    XtraMessageBox.Show("El archivo debe de contener por lo menos alguna hoja.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Function
                Else
                    If pListObj.All(Function(b) b.Checked = False) Then
                        XtraMessageBox.Show("Seleccione una hoja.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Function
                    End If
                End If
            End If

            Validar_Seleccion_Archivo = True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Borrar_Tabla() As Boolean

        Borrar_Tabla = False

        Try

            Select Case pTipoMantenimiento

                Case "MotivoAnulacion"
                    clsLnMotivo_anulacion.Delete(AP.IdEmpresa)
                Case "MotivoDevolucion"
                    clsLnMotivo_devolucion.Delete(AP.IdEmpresa, pcmbP.SelectedValue)
                Case "Operador"
                    clsLnOperador.Delete(AP.IdEmpresa)
                Case "ProductoClasificacion"
                    clsLnProducto_clasificacion.DeleteByPropietario(pcmbP.SelectedValue)
                Case "ProductoFamilia"
                    clsLnProducto_familia.DeleteByPropietario(pcmbP.SelectedValue)
                Case "ProductoMarca"
                    clsLnProducto_marca.DeleteByPropietario(pcmbP.SelectedValue)
                Case "ProductoEstado"
                    clsLnProducto_estado.Eliminar_By_IdPropietario(pcmbP.SelectedValue)
                Case "ProductoPresentacion"
                    clsLnProducto_presentacion.Delete(1)
                Case "UnidadMedida"
                    clsLnUnidad_medida.DeleteByPropietario(pcmbP.SelectedValue)
                Case "Producto"
                    clsLnProducto.Delete(pcmbP.SelectedValue)
                Case "EstructuraBodega"
                    'clsLnBodega_area.Eliminar()
                Case Else
                    Exit Select
            End Select

            Borrar_Tabla = True

        Catch ex As Exception
            Throw ex
            'XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Private Sub cmbOperadores_EditValueChanged(sender As Object, e As EventArgs)
        IdOperador = cmbOperadores.EditValue
        NomOperador = cmbOperadores.Text
    End Sub

    Private Sub chkInsertInvt_CheckedChanged_1(sender As Object, e As EventArgs) Handles chkInsertInvt.CheckedChanged
        InsertaInv = chkInsertInvt.Checked

        If InsertaInv Then
            lblOperadores.Visible = InsertaInv
            cmbOperadores.Visible = InsertaInv

            IMS.Listar_Operadores(cmbOperadores)
            IdOperador = cmbOperadores.EditValue
            NomOperador = cmbOperadores.Text

        End If
    End Sub

    Private Sub frmCargaExcel_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Text = "Carga de Excel - " & pNombreMantenimiento

            If pPropietario Then

                If pTipoMantenimiento = "MotivoAnulacion" Or pTipoMantenimiento = "Operador" Then
                    lblTipo.Dispose()
                    GrpBorraTabla.Dispose()
                    grpInsertaInv.Dispose()
                    chkBorraTabla.Dispose()
                    pcmbP.Dispose()
                    GrpSeleccion.Top = 50
                    GrpSeleccion.Height += 67
                Else
                    CreaCombobox()
                    grpInsertaInv.Dispose()
                    lblTipo.Text = "Propietario:"
                    IMS.Listar_PropietariosByEmpresaExcel(pcmbP, AP.IdEmpresa)
                End If

            ElseIf pTipoMantenimiento = "Tarima" Then
                GrpBorraTabla.Dispose()
                grpInsertaInv.Dispose()
                GrpSeleccion.Top = 50
                GrpSeleccion.Height += 67
            ElseIf pTipoMantenimiento = "Inventario" Then
                GrpBorraTabla.Dispose()
                grpInsertaInv.Top = 50
                GrpSeleccion.Top = grpInsertaInv.Height + 55
                GrpSeleccion.Height += 67
            ElseIf pTipoMantenimiento = "EstructuraBodega" Then
                chkBorraTabla.Dispose()
                GrpBorraTabla.Dispose()
                grpInsertaInv.Dispose()
                GrpSeleccion.Top = 50
                GrpSeleccion.Height += 67
            ElseIf pTipoMantenimiento = "Reubicación" Then
                chkBorraTabla.Dispose()
                GrpBorraTabla.Dispose()
                grpInsertaInv.Dispose()
                GrpSeleccion.Top = 50
                GrpSeleccion.Height += 67
            ElseIf pTipoMantenimiento = "CambioEstado" Then
                chkBorraTabla.Dispose()
                GrpBorraTabla.Dispose()
                grpInsertaInv.Dispose()
                GrpSeleccion.Top = 50
                GrpSeleccion.Height += 67
            ElseIf pTipoMantenimiento = "Reabasto" Then
                GrpBorraTabla.Dispose()
                grpInsertaInv.Dispose()
                chkBorraTabla.Dispose()
                GrpSeleccion.Top = 50
                GrpSeleccion.Height += 67
            ElseIf pTipoMantenimiento = "UnidadMedida" Then
                grpInsertaInv.Dispose()
                CreaCombobox()
                lblTipo.Text = "Propietario:"
                IMS.Listar_PropietariosByEmpresaExcel(pcmbP, AP.IdEmpresa)
                GrpBorraTabla.Top = 70
                GrpSeleccion.Top = 180
            ElseIf pTipoMantenimiento = "Indices_Rotacion" OrElse pTipoMantenimiento = "Indices_Rotacion_Bodega" Then
                GrpBorraTabla.Visible = False
                grpInsertaInv.Visible = False
            Else

                CreaCombobox()

                grpInsertaInv.Dispose()
                chkBorraTabla.Dispose()

                lblTipo.Text = "Propietario:"

                IMS.Listar_PropietariosByEmpresaExcel(pcmbP, AP.IdEmpresa)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub

    Private Sub Valida_Data_Bodega(ByVal row As IXLRow, ByVal pFirstCol As Integer, ByVal pLastCol As Integer)

        Try

            Dim i As Integer

            If registro > 0 Then
                DT2.Rows.Add() : i = 1
                DT2.Rows(DT2.Rows.Count - 1).Item(0) = registro
            End If

            For Each cell As IXLCell In row.Cells(pFirstCol, pLastCol)

                If cell.Address.ColumnNumber > 0 AndAlso cell.Address.ColumnNumber < vCantColumnas Then

                    Dim vValorCelda = cell.Value.ToString().Replace("&", "")

                    'GT16122021: si es encabezado, nos retiramos, no hay que leer nada
                    If vValorCelda = "BODEGA" Then
                        pFilaVacia = True
                        Return
                    Else
                        pFilaVacia = False
                    End If

                    If cell.Address.ColumnNumber = 1 Then
                        vValorCelda = vValorCelda.ToString()
                    End If

                    If cell.Address.ColumnNumber = 2 _
                             OrElse cell.Address.ColumnNumber = 3 _
                             OrElse cell.Address.ColumnNumber = 6 Then
                        vValorCelda = Val(vValorCelda)
                    End If

                    If cell.Address.ColumnNumber = 4 _
                             OrElse cell.Address.ColumnNumber = 5 _
                             OrElse cell.Address.ColumnNumber = 7 _
                             OrElse cell.Address.ColumnNumber = 8 _
                             OrElse cell.Address.ColumnNumber = 9 _
                             OrElse cell.Address.ColumnNumber = 12 Then
                        vValorCelda = vValorCelda.ToString()
                    End If

                    If cell.Address.ColumnNumber = 10 _
                            OrElse cell.Address.ColumnNumber = 11 Then
                        vValorCelda = CDbl(vValorCelda)
                    End If

                    DT2.Rows(DT2.Rows.Count - 1).Item(i) = vValorCelda
                    i += 1

                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Valida_Data_Rack(ByVal row As IXLRow, ByVal pFirstCol As Integer, ByVal pLastCol As Integer)

        Dim i As Integer

        Try

            pFilaVacia = False

            DT2.Rows.Add() : i = 12

            DT2.Rows(DT2.Rows.Count - 1).Item(0) = registro

            For Each cell As IXLCell In row.Cells(pFirstCol, pLastCol)

                Dim vValorCelda = cell.Value.ToString().Replace("&", "")

                'GT16122021: se lee desde la col12 y maximo col33, porque esta estatico el limite
                If cell.Address.ColumnNumber > 11 AndAlso cell.Address.ColumnNumber < vCantColumnas Then

                    'If cell.Address.ColumnNumber > 12 Then
                    '    If IsNumeric(vValorCelda) Then
                    '        vValorCelda = Val(vValorCelda)
                    '    Else
                    '        vValorCelda = 0
                    '    End If
                    'End If

                    DT2.Rows(DT2.Rows.Count - 1).Item(i) = vValorCelda
                    i += 1

                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function RowIsEmpty(ByVal row As IXLRow) As Boolean
        RowIsEmpty = False

        Try
            Dim bandera As Integer = 0
            For Each cell As IXLCell In row.Cells

                If Not String.IsNullOrEmpty(cell.Value) Then
                    bandera += 1
                End If
            Next

            If bandera > 0 Then
                RowIsEmpty = False
            Else
                RowIsEmpty = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function Importacion_Temp(ByVal importacion As DataTable) As Boolean

        Importacion_Temp = False
        Try

            Dim listTmpImportbodega As New List(Of clsBeTrans_bodega_ubicaciones_excel)
            ListObjImportBodega = New List(Of clsBeTrans_bodega_ubicaciones_excel)


            For j As Integer = 0 To importacion.Rows.Count - 1

                Dim ObjImportBodega As New clsBeTrans_bodega_ubicaciones_excel()

                ObjImportBodega.Registro = importacion(j)(0)
                ObjImportBodega.Cod_bodega = IIf(IsDBNull(importacion(j)(1)), 0, importacion(j)(1))
                ObjImportBodega.Cod_area = IIf(IsDBNull(importacion(j)(2)), 0, importacion(j)(2))
                ObjImportBodega.Cod_sector = IIf(IsDBNull(importacion(j)(3)), 0, importacion(j)(3))
                ObjImportBodega.Tipo_ubicacion = IIf(IsDBNull(importacion(j)(4)), "", importacion(j)(4))
                ObjImportBodega.Tipo_rack = IIf(IsDBNull(importacion(j)(5)), "", importacion(j)(5))
                ObjImportBodega.Numero = IIf(IsDBNull(importacion(j)(6)), 0, importacion(j)(6))
                ObjImportBodega.Ingreso_por = IIf(IsDBNull(importacion(j)(7)), "", importacion(j)(7))
                ObjImportBodega.Orientacion = IIf(IsDBNull(importacion(j)(8)), "", importacion(j)(8))
                ObjImportBodega.Orden = IIf(IsDBNull(importacion(j)(9)), "", importacion(j)(9))
                ObjImportBodega.X = IIf(IsDBNull(importacion(j)(10)), 0, importacion(j)(10))
                ObjImportBodega.Y = IIf(IsDBNull(importacion(j)(11)), 0, importacion(j)(11))
                ObjImportBodega.Nivel = IIf(IsDBNull(importacion(j)(12)), "", importacion(j)(12))

                ObjImportBodega.Col1 = IIf(IsDBNull(importacion(j)(13)), "", importacion(j)(13))
                ObjImportBodega.Col2 = IIf(IsDBNull(importacion(j)(14)), "", importacion(j)(14))
                ObjImportBodega.Col3 = IIf(IsDBNull(importacion(j)(15)), "", importacion(j)(15))
                ObjImportBodega.Col4 = IIf(IsDBNull(importacion(j)(16)), "", importacion(j)(16))
                ObjImportBodega.Col5 = IIf(IsDBNull(importacion(j)(17)), "", importacion(j)(17))
                ObjImportBodega.Col6 = IIf(IsDBNull(importacion(j)(18)), "", importacion(j)(18))
                ObjImportBodega.Col7 = IIf(IsDBNull(importacion(j)(19)), "", importacion(j)(19))
                ObjImportBodega.Col8 = IIf(IsDBNull(importacion(j)(20)), "", importacion(j)(20))
                ObjImportBodega.Col9 = IIf(IsDBNull(importacion(j)(21)), "", importacion(j)(21))
                ObjImportBodega.Col10 = IIf(IsDBNull(importacion(j)(22)), "", importacion(j)(22))
                ObjImportBodega.Col11 = IIf(IsDBNull(importacion(j)(23)), "", importacion(j)(23))
                ObjImportBodega.Col12 = IIf(IsDBNull(importacion(j)(24)), "", importacion(j)(24))
                ObjImportBodega.Col13 = IIf(IsDBNull(importacion(j)(25)), "", importacion(j)(25))
                ObjImportBodega.Col14 = IIf(IsDBNull(importacion(j)(26)), "", importacion(j)(26))
                ObjImportBodega.Col15 = IIf(IsDBNull(importacion(j)(27)), "", importacion(j)(27))
                ObjImportBodega.Col16 = IIf(IsDBNull(importacion(j)(28)), "", importacion(j)(28))
                ObjImportBodega.Col17 = IIf(IsDBNull(importacion(j)(29)), "", importacion(j)(29))
                ObjImportBodega.Col18 = IIf(IsDBNull(importacion(j)(30)), "", importacion(j)(30))
                ObjImportBodega.Col19 = IIf(IsDBNull(importacion(j)(31)), "", importacion(j)(31))
                ObjImportBodega.Col20 = IIf(IsDBNull(importacion(j)(32)), "", importacion(j)(32))
                'GT19042022: se amplia a 30 columnas, para importar data ubicaciones
                ObjImportBodega.Col21 = IIf(IsDBNull(importacion(j)(33)), "", importacion(j)(33))
                ObjImportBodega.Col22 = IIf(IsDBNull(importacion(j)(34)), "", importacion(j)(34))
                ObjImportBodega.Col23 = IIf(IsDBNull(importacion(j)(35)), "", importacion(j)(35))
                ObjImportBodega.Col24 = IIf(IsDBNull(importacion(j)(36)), "", importacion(j)(36))
                ObjImportBodega.Col25 = IIf(IsDBNull(importacion(j)(37)), "", importacion(j)(37))
                ObjImportBodega.Col26 = IIf(IsDBNull(importacion(j)(38)), "", importacion(j)(38))
                ObjImportBodega.Col27 = IIf(IsDBNull(importacion(j)(39)), "", importacion(j)(39))
                ObjImportBodega.Col28 = IIf(IsDBNull(importacion(j)(40)), "", importacion(j)(40))
                ObjImportBodega.Col29 = IIf(IsDBNull(importacion(j)(41)), "", importacion(j)(41))
                ObjImportBodega.Col30 = IIf(IsDBNull(importacion(j)(42)), "", importacion(j)(42))

                ObjImportBodega.Col31 = IIf(IsDBNull(importacion(j)(43)), "", importacion(j)(43))
                ObjImportBodega.Col32 = IIf(IsDBNull(importacion(j)(44)), "", importacion(j)(44))
                ObjImportBodega.Col33 = IIf(IsDBNull(importacion(j)(45)), "", importacion(j)(45))
                ObjImportBodega.Col34 = IIf(IsDBNull(importacion(j)(46)), "", importacion(j)(46))
                ObjImportBodega.Col35 = IIf(IsDBNull(importacion(j)(47)), "", importacion(j)(47))
                ObjImportBodega.Col36 = IIf(IsDBNull(importacion(j)(48)), "", importacion(j)(48))
                ObjImportBodega.Col37 = IIf(IsDBNull(importacion(j)(49)), "", importacion(j)(49))
                ObjImportBodega.Col38 = IIf(IsDBNull(importacion(j)(50)), "", importacion(j)(50))
                ObjImportBodega.Col39 = IIf(IsDBNull(importacion(j)(51)), "", importacion(j)(51))
                ObjImportBodega.Col40 = IIf(IsDBNull(importacion(j)(52)), "", importacion(j)(52))

                ObjImportBodega.Col41 = IIf(IsDBNull(importacion(j)(53)), "", importacion(j)(53))
                ObjImportBodega.Col42 = IIf(IsDBNull(importacion(j)(54)), "", importacion(j)(54))
                ObjImportBodega.Col43 = IIf(IsDBNull(importacion(j)(55)), "", importacion(j)(55))
                ObjImportBodega.Col44 = IIf(IsDBNull(importacion(j)(56)), "", importacion(j)(56))
                ObjImportBodega.Col45 = IIf(IsDBNull(importacion(j)(57)), "", importacion(j)(57))
                ObjImportBodega.Col46 = IIf(IsDBNull(importacion(j)(58)), "", importacion(j)(58))
                ObjImportBodega.Col47 = IIf(IsDBNull(importacion(j)(59)), "", importacion(j)(59))
                ObjImportBodega.Col48 = IIf(IsDBNull(importacion(j)(60)), "", importacion(j)(60))
                ObjImportBodega.Col49 = IIf(IsDBNull(importacion(j)(61)), "", importacion(j)(61))
                ObjImportBodega.Col50 = IIf(IsDBNull(importacion(j)(62)), "", importacion(j)(62))

                ObjImportBodega.Col51 = IIf(IsDBNull(importacion(j)(63)), "", importacion(j)(63))
                ObjImportBodega.Col52 = IIf(IsDBNull(importacion(j)(64)), "", importacion(j)(64))
                ObjImportBodega.Col53 = IIf(IsDBNull(importacion(j)(65)), "", importacion(j)(65))
                ObjImportBodega.Col54 = IIf(IsDBNull(importacion(j)(66)), "", importacion(j)(66))
                ObjImportBodega.Col55 = IIf(IsDBNull(importacion(j)(67)), "", importacion(j)(67))
                ObjImportBodega.Col56 = IIf(IsDBNull(importacion(j)(68)), "", importacion(j)(68))
                ObjImportBodega.Col57 = IIf(IsDBNull(importacion(j)(69)), "", importacion(j)(69))
                ObjImportBodega.Col58 = IIf(IsDBNull(importacion(j)(70)), "", importacion(j)(70))
                ObjImportBodega.Col59 = IIf(IsDBNull(importacion(j)(71)), "", importacion(j)(71))
                ObjImportBodega.Col60 = IIf(IsDBNull(importacion(j)(72)), "", importacion(j)(72))

                ObjImportBodega.Col61 = IIf(IsDBNull(importacion(j)(73)), "", importacion(j)(73))
                ObjImportBodega.Col62 = IIf(IsDBNull(importacion(j)(74)), "", importacion(j)(74))
                ObjImportBodega.Col63 = IIf(IsDBNull(importacion(j)(75)), "", importacion(j)(75))
                ObjImportBodega.Col64 = IIf(IsDBNull(importacion(j)(76)), "", importacion(j)(76))
                ObjImportBodega.Col65 = IIf(IsDBNull(importacion(j)(77)), "", importacion(j)(77))
                ObjImportBodega.Col66 = IIf(IsDBNull(importacion(j)(78)), "", importacion(j)(78))
                ObjImportBodega.Col67 = IIf(IsDBNull(importacion(j)(79)), "", importacion(j)(79))
                ObjImportBodega.Col68 = IIf(IsDBNull(importacion(j)(80)), "", importacion(j)(80))
                ObjImportBodega.Col69 = IIf(IsDBNull(importacion(j)(81)), "", importacion(j)(81))
                ObjImportBodega.Col70 = IIf(IsDBNull(importacion(j)(82)), "", importacion(j)(82))

                listTmpImportbodega.Add(ObjImportBodega)
                'ListObjImportBodega.Add(ObjImportBodega)
            Next j

            For Each Obj As clsBeTrans_bodega_ubicaciones_excel In listTmpImportbodega
                If Not String.IsNullOrEmpty(Obj.Nivel) Then
                    ListObjImportBodega.Add(Obj)
                Else
                    Debug.Print("No lo inserte " & Obj.Numero)
                End If
            Next

            Importacion_Temp = False

        Catch ex As Exception
            Importacion_Temp = True
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try
    End Function


    Private Function Guardar_Bodega() As Boolean

        Guardar_Bodega = False
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            'Dim pBeBodegaArea As New clsBeBodega_area
            Dim BeBodega As New clsBeBodega
            Dim BeBodegaArea As New clsBeBodega_area
            Dim BeBodegaSector As New clsBeBodega_sector
            Dim BeEstructuraTramo As New clsBeEstructura_tramo
            Dim BeEstructuraGrupo As New clsBeEstructura_grupo
            Dim ListObjBodegas As New DataTable
            Dim ListObjLargoBodega As New DataTable
            Dim ListObjAnchoBodega As New DataTable
            Dim ListObjDataGrupo As New DataTable
            Dim vErrorLinea As Boolean = False
            Dim pRegistro As Integer
            'Dim pCodBodega As Integer
            '#GT26042022: puede que sea el codigo y no el id de la bodega
            Dim pCodBodega As String
            Dim pCodArea As Integer
            Dim pCodSector As Integer
            Dim pTipoUbicacion As String
            Dim pTipoRack As String
            Dim pNumero As Integer
            Dim pIngresoPor As String
            Dim pOrientacion As String
            Dim pOrden As String
            Dim pX As Double
            Dim pY As Double
            Dim pNivel As String
            Dim vLargo As Double = 0.00
            Dim vAlto As Double = 0.00
            Dim vAncho As Double = 0.00
            Dim vAlto_en_Celda As Double = 0
            Dim pInicia As String = ""

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'GT23122021: obtengo lista de las bodegas y # de niveles por cada una.
            ListObjBodegas.Clear()
            ListObjBodegas = clsLnTrans_bodega_ubicaciones_excel.Get_Filter_by_Bodega(lConnection, lTransaction)

            If ListObjBodegas.Rows.Count > 0 Then

                For fila As Integer = 0 To ListObjBodegas.Rows.Count - 1

                    pRegistro = CInt(ListObjBodegas(fila)(0))
                    'pCodBodega = CInt(ListObjBodegas(fila)(1))
                    pCodBodega = ListObjBodegas(fila)(1)
                    pCodArea = ListObjBodegas(fila)(2)
                    pCodSector = ListObjBodegas(fila)(3)
                    pTipoUbicacion = ListObjBodegas(fila)(4)
                    pTipoRack = ListObjBodegas(fila)(5)
                    pNumero = ListObjBodegas(fila)(6)
                    pIngresoPor = ListObjBodegas(fila)(7)
                    pOrientacion = ListObjBodegas(fila)(8)
                    pOrden = ListObjBodegas(fila)(9)
                    pX = ListObjBodegas(fila)(10)
                    pY = ListObjBodegas(fila)(11)
                    pNivel = ListObjBodegas(fila)(12)
                    vLargo = 0.00
                    vAlto = 0.00
                    vAncho = 0.00

                    Debug.WriteLine("Registro: " & pRegistro)

                    pInicia = pTipoUbicacion.Substring(0, 1) + pNumero.ToString 'concatenacion para validar si existe tramo

                    'Dim existe_bodega = clsLnBodega.Exists(pCodBodega,
                    '                                      lConnection,
                    '                                      lTransaction)

                    '#GT26042022_1043: Valida existencia de bodega por codigo o por el ID
                    BeBodega = New clsBeBodega
                    BeBodega = clsLnBodega.GetSingle_By_Codigo_Or_IdBodega(pCodBodega,
                                                                           lConnection,
                                                                           lTransaction)


                    If Not BeBodega Is Nothing Then

                        BeBodegaArea = New clsBeBodega_area
                        BeBodegaArea = clsLnBodega_area.GetSingle_By_IdArea_and_IdBodega(pCodArea,
                                                                                     BeBodega.IdBodega,
                                                                                     lConnection,
                                                                                     lTransaction)

                        BeBodegaSector = New clsBeBodega_sector
                        BeBodegaSector = clsLnBodega_sector.Get_Single_By_IdBodega_IdArea_IdSector(BeBodega.IdBodega, pCodArea, pCodSector,
                                                                                                                        lConnection,
                                                                                                                        lTransaction)

                        Dim existe_tramo = clsLnBodega_tramo.Existe_By_IdBodega_And_IdSector(BeBodega.IdBodega,
                                                                                             pCodSector,
                                                                                             pInicia,
                                                                                             lConnection,
                                                                                             lTransaction)

                        'GT21122021: sino existe area se crea
                        If BeBodegaArea Is Nothing Then

                            BeBodegaArea = New clsBeBodega_area
                            BeBodegaArea.IdBodega = BeBodega.IdBodega
                            BeBodegaArea.IdArea = clsLnBodega_area.MaxID(BeBodega.IdBodega,
                                                                         lConnection,
                                                                         lTransaction)
                            BeBodegaArea.Descripcion = "AREA " + pCodArea.ToString()
                            BeBodegaArea.Sistema = 0
                            BeBodegaArea.User_agr = AP.UsuarioAp.IdUsuario
                            BeBodegaArea.Fec_agr = Now
                            BeBodegaArea.User_mod = AP.UsuarioAp.IdUsuario
                            BeBodegaArea.Fec_mod = Now
                            BeBodegaArea.Codigo = "A" + pCodArea.ToString
                            BeBodegaArea.Activo = 1
                            BeBodegaArea.Alto = 0
                            BeBodegaArea.Largo = 0
                            BeBodegaArea.Ancho = 0
                            BeBodegaArea.Margen_izquierdo = 0
                            BeBodegaArea.Margen_derecho = 0
                            BeBodegaArea.Margen_superior = 0
                            BeBodegaArea.Margen_inferior = 0
                            BeBodegaArea.Grupo = ""

                            clsLnBodega_area.Insertar(BeBodegaArea,
                                                      lConnection,
                                                      lTransaction)

                            lblPrg.AppendText("Aviso : " & "Se registro Bodega-Area: " & BeBodegaArea.IdArea & " en WMS.")
                            lblPrg.AppendText(vbNewLine)
                            lblPrg.Refresh()
                            lblPrg.SelectionStart = lblPrg.TextLength
                            lblPrg.ScrollToCaret()

                        End If

                        'GT24122021: sino existe sector, se crea
                        'If Not existe_sector Then
                        If BeBodegaSector Is Nothing Then

                            'GT30122021: trae el alto del sector
                            vAlto = clsLnTrans_bodega_ubicaciones_excel.Bodega_Sector_Alto(pRegistro,
                                                                                           lConnection,
                                                                                           lTransaction)

                            'GT30122021: trae la fila para sumar el largo total
                            ListObjLargoBodega = clsLnTrans_bodega_ubicaciones_excel.Bodega_Sector_Largo(pRegistro,
                                                                                                         lConnection,
                                                                                                         lTransaction)
                            'suma las columnas que tengan valor
                            For Each column As DataColumn In ListObjLargoBodega.Columns
                                If column.ColumnName.StartsWith("col") Then
                                    If ListObjLargoBodega.Rows(0).Item(column).ToString() <> "" Then
                                        vLargo += CDbl(ListObjLargoBodega.Rows(0).Item(column).ToString())
                                    End If
                                End If
                            Next

                            'GT30122021:  trae la fila que indica profundidad para sumar y obtener el ancho
                            ListObjAnchoBodega = clsLnTrans_bodega_ubicaciones_excel.Bodega_Sector_Ancho(pRegistro,
                                                                                                        lConnection,
                                                                                                        lTransaction)

                            'GT08022022: obtiene el max de la fila para Ancho de bodega-sector y estructura-tramo
                            vAncho = FindMaxDataTableValue(ListObjAnchoBodega)

                            BeBodegaSector = New clsBeBodega_sector
                            BeBodegaSector.IdBodega = BeBodegaArea.IdBodega
                            BeBodegaSector.IdArea = BeBodegaArea.IdArea
                            BeBodegaSector.IdSector = clsLnBodega_sector.MaxID(BeBodegaArea.IdBodega,
                                                                               lConnection,
                                                                               lTransaction)

                            BeBodegaSector.Sistema = 0
                            BeBodegaSector.Descripcion = pTipoUbicacion.ToString() + " " + pNumero.ToString()
                            BeBodegaSector.User_agr = AP.UsuarioAp.IdUsuario
                            BeBodegaSector.Fec_agr = Now
                            BeBodegaSector.User_mod = AP.UsuarioAp.IdUsuario
                            BeBodegaSector.Fec_mod = Now
                            BeBodegaSector.Activo = 1
                            BeBodegaSector.Alto = Math.Round(vAlto, 2) 'suma de los niveles
                            BeBodegaSector.Largo = Math.Round(vLargo, 2) 'suma de anchos 
                            BeBodegaSector.Ancho = Math.Round(vAncho, 2)  'suma de las profundidades
                            BeBodegaSector.Margen_izquierdo = 0
                            BeBodegaSector.Margen_derecho = 0
                            BeBodegaSector.Margen_superior = 0
                            BeBodegaSector.Margen_inferior = 0
                            BeBodegaSector.Codigo = "S" + BeBodegaSector.IdSector.ToString()
                            BeBodegaSector.IdSectorIzquierda = 0
                            BeBodegaSector.IdSectorDerecha = 0
                            BeBodegaSector.Horizontal = IIf(pOrientacion = "Horizontal", False, True) 'el campo en la bd deberia ser vertical, se cambia los valores 
                            BeBodegaSector.Pos_x = pX
                            BeBodegaSector.Pos_y = pY


                            clsLnBodega_sector.Insertar(BeBodegaSector,
                                                        lConnection,
                                                        lTransaction)

                            clsPublic.Actualizar_Progreso(lblPrg, "Aviso : " & "Se registro Bodega-Sector: " & BeBodegaSector.IdSector & " en WMS.")

                        End If

                        If Not existe_tramo Then

                            Dim vIdTramo As Integer
                            BeEstructuraTramo = New clsBeEstructura_tramo
                            vIdTramo = clsLnEstructura_tramo.MaxID(lConnection,
                                                                   lTransaction)

                            BeEstructuraTramo.IdTramo = vIdTramo
                            BeEstructuraTramo.IdBodega = BeBodegaArea.IdBodega
                            BeEstructuraTramo.IdSector = BeBodegaSector.IdSector
                            BeEstructuraTramo.Sistema = False
                            BeEstructuraTramo.Descripcion = pInicia
                            BeEstructuraTramo.User_agr = AP.UsuarioAp.IdUsuario
                            BeEstructuraTramo.Fec_agr = Now
                            BeEstructuraTramo.User_mod = AP.UsuarioAp.IdUsuario
                            BeEstructuraTramo.Fec_mod = Now
                            BeEstructuraTramo.Activo = True
                            BeEstructuraTramo.Alto = Math.Round(vAlto, 2) 'suma de los niveles 
                            BeEstructuraTramo.Largo = Math.Round(vLargo, 2) 'suma de fila Largo 
                            BeEstructuraTramo.Ancho = Math.Round(vAncho, 2)  'No es suma de las profundidades  --validar max de la fila para obtener el valor maximo
                            BeEstructuraTramo.Margen_izquierdo = 0
                            BeEstructuraTramo.Margen_derecho = 0
                            BeEstructuraTramo.Margen_superior = 0
                            BeEstructuraTramo.Margen_inferior = 0
                            BeEstructuraTramo.Codigo = vIdTramo.ToString 'mismo valor que el ID del tramo
                            BeEstructuraTramo.Indice_x = 1
                            BeEstructuraTramo.Orientacion = IIf(pIngresoPor = "Izquierda", 0, 1)
                            BeEstructuraTramo.IdTipoProductoDefault = 0
                            BeEstructuraTramo.Horizontal = IIf(pOrientacion = "Horizontal", True, False) 'se usa el valor Orientación del excel
                            BeEstructuraTramo.IdArea = BeBodegaArea.IdArea
                            BeEstructuraTramo.Orden_Descendente = IIf(pOrden = "Descendente", True, False)

                            clsLnEstructura_tramo.Insertar(BeEstructuraTramo,
                                                           lConnection,
                                                           lTransaction)

                            clsPublic.Actualizar_Progreso(lblPrg, "Aviso : " & "Se registro Bodega-Tramo: " & BeEstructuraTramo.IdTramo & " en WMS.")

                            'GT31122021: se obtiene la data de las columnas
                            ListObjDataGrupo = clsLnTrans_bodega_ubicaciones_excel.Bodega_data_grupo(pRegistro,
                                                                                                     lConnection,
                                                                                                     lTransaction)

                            'GT05012021: trae la fila de largo, para saber el largo de la columna a setear en el grupo
                            Dim FilaLargoBodega = clsLnTrans_bodega_ubicaciones_excel.Bodega_Sector_Largo(pRegistro,
                                                                                                         lConnection,
                                                                                                         lTransaction)
                            'iteramos la lista para armar los grupos
                            Dim vCountOffset As Integer = 0
                            Dim vCountTamano As Integer = 0
                            Dim posicion As Integer = 1
                            Dim tmpLargo As Double = 0.00
                            Dim vIdGrupo = clsLnEstructura_grupo.MaxID(lConnection,
                                                                       lTransaction) + 1

                            For Each column As DataColumn In ListObjDataGrupo.Columns

                                If column.ColumnName.StartsWith("col") Then

                                    If ListObjDataGrupo.Rows(0).Item(column).ToString() <> "" Then
                                        Debug.WriteLine("Procesando columna " & column.ColumnName & " En la posición: " & pRegistro)

                                        If column.ColumnName = "col15" Then
                                            Debug.Print("Hola")
                                        End If

                                        tmpLargo = CDbl(IIf(FilaLargoBodega.Rows(0).Item(column.ColumnName).ToString() = "", "0", FilaLargoBodega.Rows(0).Item(column.ColumnName).ToString()))
                                        Dim vsAltoCelda As String = IIf(IsDBNull(ListObjDataGrupo.Rows(0).Item(column).ToString()), "", ListObjDataGrupo.Rows(0).Item(column).ToString())

                                        If vsAltoCelda = "" Then
                                            vAlto_en_Celda = ListObjDataGrupo.Rows.Count - 1
                                        Else
                                            vAlto_en_Celda = CDbl(vsAltoCelda)
                                        End If

                                        BeEstructuraGrupo = New clsBeEstructura_grupo
                                        BeEstructuraGrupo.IdGrupo = vIdGrupo
                                        BeEstructuraGrupo.IdTramo = BeEstructuraTramo.IdTramo
                                        BeEstructuraGrupo.IdBodega = BeBodegaArea.IdBodega
                                        BeEstructuraGrupo.Agrupacion = 1
                                        BeEstructuraGrupo.Pos = posicion
                                        If vAlto_en_Celda > 0 Then
                                            BeEstructuraGrupo.Tamano = pNivel
                                            BeEstructuraGrupo.Offset = 0 'si lectura tiene valor 0, es igual a offset, si tiene valor se deja en 0
                                        Else
                                            Try
                                                Dim vVacios = (From t In ListObjDataGrupo.Rows Where t(column.ColumnName) = "" Select t)
                                                Dim vOffset1 = (From t In ListObjDataGrupo.Rows Where Val(t(column.ColumnName)) = 0 Select t).AsEnumerable()
                                                Dim vtamano1 = (From t In ListObjDataGrupo.Rows Where Val(t(column.ColumnName)) > 0 Select t)

                                                Dim vCantOffSet As Integer = 0
                                                Dim vCantOtros As Integer = 0
                                                Dim vCantVacios As Integer = 0

                                                If Not vOffset1 Is Nothing Then
                                                    vCantOffSet = vOffset1.Count
                                                End If

                                                If Not vtamano1 Is Nothing Then
                                                    vCantOtros = vtamano1.Count
                                                End If

                                                If Not vVacios Is Nothing Then
                                                    vCantVacios = vVacios.Count
                                                End If

                                                BeEstructuraGrupo.Tamano = vCantOtros
                                                BeEstructuraGrupo.Offset = vCantOffSet - vCantVacios

                                            Catch ex As Exception
                                                Debug.WriteLine(ex.Message)
                                            End Try

                                        End If

                                        BeEstructuraGrupo.Cant = 1
                                        BeEstructuraGrupo.Ancho = tmpLargo 'vAncho
                                        BeEstructuraGrupo.Alto = vAlto_en_Celda
                                        BeEstructuraGrupo.Largo = vAncho 'tmpLargo 'no se usa el vLargo del tramo, sino el especifico de la columna
                                        BeEstructuraGrupo.Palet = 1
                                        'BeEstructuraGrupo.Orient = IIf(pOrientacion = "Horizontal", 1, 2)
                                        BeEstructuraGrupo.Orient = pTipoRack.Substring(0, 1) 'substring para obtener el id del tipoRack, no es Orientación!!
                                        clsLnEstructura_grupo.Insertar(BeEstructuraGrupo,
                                                                           lConnection,
                                                                           lTransaction)

                                        clsPublic.Actualizar_Progreso(lblPrg, "Aviso : " & "Se registro Estructura-Grupo: " & BeEstructuraGrupo.IdGrupo & " en WMS.")

                                        posicion += 1 'indica el grupo de columnas que esta guardando
                                        vIdGrupo += 1 'itera el id del grupo (pk)

                                    End If

                                End If

                            Next
                        Else
                            vErrorLinea = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "la configuración Bodega-Tramo: " & BeEstructuraTramo.IdTramo & " en el grupo" & pRegistro & " ya existe en WMS.")
                        End If

                    Else

                        vErrorLinea = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "La bodega: " & pCodBodega & " no existe en WMS.")

                    End If

                Next

            Else

                vErrorLinea = True
                clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No se puede leer la data importada desde trans_bodega_ubicaciones_excel!")

            End If

            clsLnTrans_bodega_ubicaciones_excel.Delete_All(lConnection, lTransaction)


            If vErrorLinea Then
                lTransaction.Rollback()
            Else
                lTransaction.Commit()
            End If

            Guardar_Bodega = vErrorLinea

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            clsLnTrans_bodega_ubicaciones_excel.Delete_All()
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        Finally
            SplashScreenManager.CloseForm(False)
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Private Function FindMaxDataTableValue(ByRef dt As DataTable) As Double

        Dim currentValue, maxValue As Double

        For Each column As DataColumn In dt.Columns
            If column.ColumnName.StartsWith("col") Then
                If dt.Rows(0).Item(column).ToString() <> "" Then

                    If maxValue = 0 Then
                        maxValue = CDbl(dt.Rows(0).Item(column).ToString())
                    Else
                        currentValue = CDbl(dt.Rows(0).Item(column).ToString())

                        If currentValue > maxValue Then
                            maxValue = currentValue
                        End If

                    End If

                End If
            End If
        Next

        Return maxValue
    End Function


#Region "Importar listado ubicaciones"

    Private Function Carga_Cambio_Ubicacion(ByVal pDT As DataTable) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Carga_Cambio_Ubicacion = False

        Dim IdStock As Integer = 0
        Dim pIdBodega As String = ""
        Dim pCodigoProducto As String = ""
        Dim pIdUbicacion As Integer = 0
        Dim pIdUbicacionDestino As Integer = 0
        Dim pCantidad As Integer = 0
        Dim pCantidadUmbas As Integer = 0
        Dim pPresentacion As String = ""
        Dim errorCampos As Boolean = False
        Dim pBodega As New clsBeBodega
        Dim pFactor As Double = 0.00


        Try

            'errorCampos = False
            errores = False
            Cursor = Cursors.WaitCursor
            lObjStock = New List(Of clsBeVW_stock_res)

            Dim vCantRegistros As Integer = pDT.Rows.Count
            Dim vIndicadorFila As Integer = 1

            '#GT21052022_0900: validamos los encabezados para que no falten columnas
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Procesando Encabezados del archivo...")

            '#GT21062022_0900: validamos la data de cada fila
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Procesando Archivo")

            For i As Integer = 0 To pDT.Rows.Count - 1

                Application.DoEvents()
                Dim Indice As Integer = i
                pObjStock = New clsBeVW_stock_res



#Region "Datos Ubicación Origen"

                '#GT14062022: valida IdStock.
                If pDT(i)(0) Is DBNull.Value Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: En la fila " & i + 1 & ", el valor no es válido")
                Else

                    IdStock = pDT(Indice)(0)
                    '#GT20062022_0840: consulto la bd para existencia del id
                    pObjStock = clsLnStock.Get_Single_By_IdStock(IdStock)

                End If

                '#GT20062022_1220: valida bodega excel
                If pDT(i)(1) Is DBNull.Value Or pDT(i)(1) = "" Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: En la fila " & i + 1 & ", el valor no es válido")
                Else

                    '#GT20062022_1210: validamos que la bodegaOrigen del excel coincida con la bodega del encabezado
                    pIdBodega = pDT(Indice)(1)
                    pBodega = New clsBeBodega

                    If IsNumeric(pIdBodega) Then
                        pBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega)
                    Else
                        pBodega = clsLnBodega.GetSingle_By_Codigo(pIdBodega)
                    End If

                    If pBodega Is Nothing Then

                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "no existe la bodega en la fila " & i + 1)

                    End If

                End If

                '#GT20062022_1220: valida codigo producto
                If pDT(i)(2) Is DBNull.Value Or pDT(i)(2) = "" Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: En la fila " & i + 1 & ", el codigo producto no es válido.")
                Else
                    pCodigoProducto = pDT(i)(2)
                End If

                '#GT20062022_1220: valida IdUbicación
                If pDT(i)(3) Is DBNull.Value Or pDT(i)(3) = "" Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: En la fila " & i + 1 & ", la úbicación no es válida.")
                Else
                    pIdUbicacion = pDT(i)(3)
                End If


                If Not pObjStock Is Nothing Then

                    '#GT14062022_1237: valida que no exista duplicados de idstock
                    Dim IdExiste As Boolean = lObjStock.Exists(Function(d) d.IdStock = IdStock)

                    If IdExiste Then
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El IdStock en la fila " & i + 1 & " se encuentra duplicado.")
                    Else

                        If pObjStock.Codigo_Producto <> pCodigoProducto Then
                            errores = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: En la fila " & i + 1 & ", el codigo producto no coincide con el registrado con el IdStock.")
                        End If


                        If pBodegaOrigen.IdBodega = pBodega.IdBodega Then

                            '#GT15062022_1114: conversión si tuviera presentación y un factor
                            Dim pReservadoUmbas = pObjStock.CantidadReservadaUMBas
                            pFactor = IIf(IsDBNull(pObjStock.Factor), 0, pObjStock.Factor)

                            '#GT16062022_0915: si hay presentación y reserva, CantidadUmbas es el nuevo "Disponible"
                            If pReservadoUmbas > 0 AndAlso pFactor > 0 Then
                                Dim pReservadoPresentacion = pReservadoUmbas / pFactor
                                pObjStock.CantidadReservada = pReservadoPresentacion  'cantidad reservada en presentación
                                pObjStock.CantidadUmBas = pObjStock.CantidadUmBas - pObjStock.CantidadReservadaUMBas 'cantidad reservada en UMBas
                            End If

                            '#GT15062022_1410: Se prepara obj de tipo hh_det
                            preparaObjDet(pObjStock)

                        Else
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "la bodega " & pIdBodega & ", no coincide con la del proceso actual de reubicación " & pBodegaOrigen.Nombre & ", en la fila " & i + 1)
                        End If

                    End If

                Else
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El IdStock en la fila " & i + 1 & " no existe en la BD.")
                End If

#End Region

#Region "Datos Ubicación Destino"

                '#GT14062022: ubicación destion (por id)
                If pDT(i)(4) Is DBNull.Value AndAlso pDT(i)(4) Is Nothing Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "Falta Ubicación destino en la fila " & i + 1)
                Else
                    pIdUbicacionDestino = pDT(i)(4)
                End If

                '#GT20062022_1402: valida que exista una cantidad numerica
                If pDT(i)(5) Is DBNull.Value AndAlso pDT(i)(5) Is Nothing Then
                    errores = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "Falta cantidad en la fila " & i + 1)
                Else

                    If IsNumeric(pDT(i)(5)) Then
                        pCantidad = pDT(i)(5)
                    Else
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "el valor debe ser númerico en la fila " & i + 1)
                    End If

                End If


                '#GT21062022_0930: La presentación no es obligatoria, pero se valida
                If pDT(i)(6) Is DBNull.Value Or pDT(i)(6) Is Nothing Then
                    pPresentacion = ""
                Else
                    pPresentacion = pDT(i)(6)
                End If


                If Not pObjStock Is Nothing Then

                    If pObjStock.Nombre_Presentacion <> pPresentacion.Trim Then
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "La presentación leida, no coincide con la registrada en stock, para la fila " & i + 1)
                    Else
                        '#GT21062022_1100: conversión de la cantidad si el idstock tuviera presentacion.
                        If pObjStock.IdPresentacion > 0 Then
                            pCantidadUmbas = pCantidad * pFactor
                        Else
                            pCantidadUmbas = pCantidad
                        End If

                    End If

                    '#GT15062022_1450: se obtiene la data de la ubicación destino
                    pBeUbicacionDestino = New clsBeBodega_ubicacion
                    pBeUbicacionDestino = clsLnBodega_ubicacion.GetSingle(pIdUbicacionDestino, AP.IdBodega)

                    If Not pBeUbicacionDestino Is Nothing Then

                        selUbic = New clsBeUbicacionSugeridaList()
                        selUbic.Descripcion = pBeUbicacionDestino.Descripcion
                        If pObjStock.CantidadUmBas < pCantidadUmbas Then
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "la cantidad a reubicar, es mayor a la disponible en la fila " & i + 1)
                        Else
                            'selUbic.Ubicar = pCantidadUmbas
                            selUbic.Ubicar = pCantidad

                            selUbic.IdUbicacionOrigen = pObjStock.IdUbicacion
                            selUbic.IdUbicacionDestino = pIdUbicacionDestino
                            selUbic.Tramo = pBeUbicacionDestino.Tramo.Descripcion
                            selUbic.Nivel = pBeUbicacionDestino.Nivel
                            selUbic.IsNew = True
                            lUbicSel.Add(selUbic)

                            '#GT16062022_0840: aqui esta la magia para llenar lista de objetos para reubicar de forma masiva.
                            Get_Info_Estado_Producto()
                            Agrega_Registros_Detalle()

                        End If

                    Else
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "La úbicación destino en la fila " & i + 1 & " no existe")
                    End If

                End If

#End Region

            Next

            If Not errorCampos Then

                If pListObjDet.Count > 0 Then

                    '#CKFK20220620 Agregué esto porque me daba error de duplicación
                    Dim MaxId As Integer = clsLnLog_importacion_excel.MaxID() + 1
                    beLogImportacion.IdImportacion = MaxId

                    '#GT20062022_1640: log del proceso completado.
                    clsLnLog_importacion_excel.Insertar(beLogImportacion)
                    Carga_Cambio_Ubicacion = True

                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Se finalizó la lectura del archivo, se procesaran los datos a continuación.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    DialogResult = DialogResult.OK
                    'Close()

                End If

            Else

                Carga_Cambio_Ubicacion = False

                SplashScreenManager.CloseForm(False)

                XtraMessageBox.Show("Se finalizó la lectura del archivo, y tiene errores que deben corregir.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

                '#GT15062022_1010: Se limpian los inputs para evitar que se presione cargar nuevamente e intente sobreescribir
                txtArchivo.Text = ""
                lObjStock.Clear()
                DsExcel.Clear()
                Grid.BeginUpdate()
                Grid.EndUpdate()
                Grid.ForceInitialize()

                cmdCargar.Enabled = True

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
            Cursor = Cursors.Default
        End Try


    End Function

    Private Function Carga_Cambio_Estado(ByVal pDT As DataTable) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Carga_Cambio_Estado = False

        Dim IdStock As Integer = 0
        Dim pIdBodega As String = ""
        Dim pCodigoProducto As String = ""
        Dim pIdUbicacion As Integer = 0
        Dim pIdUbicacionDestino As Integer = 0
        Dim pCantidad As Integer = 0
        Dim pCantidadUmbas As Integer = 0
        Dim pPresentacion As String = ""
        Dim errorCampos As Boolean = False
        Dim pBodega As New clsBeBodega
        Dim pFactor As Double = 0.00
        Dim pIdEstadoDestino As Integer = 0

        Try

            'errorCampos = False
            errores = False
            Cursor = Cursors.WaitCursor
            lObjStock = New List(Of clsBeVW_stock_res)

            'SetDatataTable()



            Dim vCantRegistros As Integer = pDT.Rows.Count
            Dim vIndicadorFila As Integer = 1

            '#GT21052022_0900: validamos los encabezados para que no falten columnas
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Procesando Encabezados del archivo...")

            '#GT21062022_0900: validamos la data de cada fila
            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Procesando Archivo")

            For i As Integer = 0 To pDT.Rows.Count - 1

                Application.DoEvents()
                Dim Indice As Integer = i
                pObjStock = New clsBeVW_stock_res

#Region "Datos Ubicación Origen"

                '#GT14062022: valida IdStock.
                If pDT(i)(0) Is DBNull.Value Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: En la fila " & i + 1 & ", el valor no es válido")
                Else

                    IdStock = pDT(Indice)(0)
                    '#GT20062022_0840: consulto la bd para existencia del id
                    pObjStock = clsLnStock.Get_Single_By_IdStock(IdStock)

                End If

                '#GT20062022_1220: valida bodega excel
                If pDT(i)(1) Is DBNull.Value Or pDT(i)(1) = "" Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: En la fila " & i + 1 & ", el valor no es válido")
                Else

                    '#GT20062022_1210: validamos que la bodegaOrigen del excel coincida con la bodega del encabezado
                    pIdBodega = pDT(Indice)(1)
                    pBodega = New clsBeBodega

                    If IsNumeric(pIdBodega) Then
                        pBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega)
                    Else
                        pBodega = clsLnBodega.GetSingle_By_Codigo(pIdBodega)
                    End If

                    If pBodega Is Nothing Then
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "no existe la bodega en la fila " & i + 1)
                    End If

                End If

                '#GT20062022_1220: valida codigo producto
                If pDT(i)(2) Is DBNull.Value Or pDT(i)(2) = "" Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: En la fila " & i + 1 & ", el codigo producto no es válido.")
                Else
                    pCodigoProducto = pDT(i)(2)
                End If

                '#GT20062022_1220: valida IdUbicación
                If pDT(i)(3) Is DBNull.Value Or pDT(i)(3) = "" Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: En la fila " & i + 1 & ", la úbicación no es válida.")
                Else
                    pIdUbicacion = pDT(i)(3)
                End If

                If Not pObjStock Is Nothing Then

                    '#GT14062022_1237: valida que no exista duplicados de idstock
                    Dim IdExiste As Boolean = lObjStock.Exists(Function(d) d.IdStock = IdStock)

                    If IdExiste Then
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El IdStock en la fila " & i + 1 & " se encuentra duplicado.")
                    Else

                        If pObjStock.Codigo_Producto <> pCodigoProducto Then
                            errores = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: En la fila " & i + 1 & ", el codigo producto no coincide con el registrado con el IdStock.")
                        End If

                        If pBodegaOrigen.IdBodega = pBodega.IdBodega Then

                            '#GT15062022_1114: conversión si tuviera presentación y un factor
                            Dim pReservadoUmbas = pObjStock.CantidadReservadaUMBas
                            pFactor = IIf(IsDBNull(pObjStock.Factor), 0, pObjStock.Factor)

                            '#GT16062022_0915: si hay presentación y reserva, CantidadUmbas es el nuevo "Disponible"
                            If pReservadoUmbas > 0 AndAlso pFactor > 0 Then
                                Dim pReservadoPresentacion = pReservadoUmbas / pFactor
                                pObjStock.CantidadReservada = pReservadoPresentacion  'cantidad reservada en presentación
                                pObjStock.CantidadUmBas = pObjStock.CantidadUmBas - pObjStock.CantidadReservadaUMBas 'cantidad reservada en UMBas
                            End If

                            '#GT15062022_1410: Se prepara obj de tipo hh_det
                            preparaObjDet(pObjStock)

                        Else
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "la bodega " & pIdBodega & ", no coincide con la del proceso actual de reubicación " & pBodegaOrigen.Nombre & ", en la fila " & i + 1)
                        End If

                    End If

                Else
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El IdStock en la fila " & i + 1 & " no existe en la BD.")
                End If


#End Region

#Region "Datos Ubicación Destino"

                '#GT14062022: ubicación destion (por id)
                If pDT(i)(4) Is DBNull.Value AndAlso pDT(i)(4) Is Nothing Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "Falta Ubicación destino en la fila " & i + 1)
                Else
                    pIdUbicacionDestino = pDT(i)(4)
                End If

                '#GT20062022_1402: valida que exista una cantidad numerica
                If pDT(i)(5) Is DBNull.Value AndAlso pDT(i)(5) Is Nothing Then
                    errores = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "Falta cantidad en la fila " & i + 1)
                Else

                    If IsNumeric(pDT(i)(5)) Then
                        pCantidad = pDT(i)(5)
                    Else
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "el valor debe ser númerico en la fila " & i + 1)
                    End If

                End If

                '#GT21062022_0930: La presentación no es obligatoria, pero se valida
                If pDT(i)(6) Is DBNull.Value Or pDT(i)(6) Is Nothing Then
                    pPresentacion = ""
                Else
                    pPresentacion = pDT(i)(6)
                End If

                '#CKFK20230110: valida IdEstado destino por Id
                If pDT(i)(7) Is DBNull.Value AndAlso pDT(i)(7) Is Nothing Then
                    errorCampos = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "Falta estado destino en la fila " & i + 1)
                Else
                    pIdEstadoDestino = pDT(i)(7)
                End If

                If Not pObjStock Is Nothing Then

                    If pObjStock.Nombre_Presentacion <> pPresentacion.Trim Then
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "La presentación leida, no coincide con la registrada en stock, para la fila " & i + 1)
                    Else
                        '#GT21062022_1100: conversión de la cantidad si el idstock tuviera presentacion.
                        If pObjStock.IdPresentacion > 0 Then
                            pCantidadUmbas = pCantidad * pFactor
                        Else
                            pCantidadUmbas = pCantidad
                        End If

                    End If

                    '#GT15062022_1450: se obtiene la data de la ubicación destino
                    pBeUbicacionDestino = New clsBeBodega_ubicacion
                    pBeUbicacionDestino = clsLnBodega_ubicacion.GetSingle(pIdUbicacionDestino, AP.IdBodega)

                    '#CKFK20240110: se obtiene la data del estado destino
                    pBeEstadoDestino = New clsBeProducto_estado
                    pBeEstadoDestino = clsLnProducto_estado.GetSingle(pIdEstadoDestino)

                    If Not pBeUbicacionDestino Is Nothing Then

                        If Not pBeEstadoDestino Is Nothing Then

                            selUbic = New clsBeUbicacionSugeridaList()
                            selUbic.Descripcion = pBeUbicacionDestino.Descripcion
                            If pObjStock.CantidadUmBas < pCantidadUmbas Then
                                errorCampos = True
                                clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "la cantidad a reubicar, es mayor a la disponible en la fila " & i + 1)
                            Else
                                'selUbic.Ubicar = pCantidadUmbas
                                selUbic.Ubicar = pCantidad

                                selUbic.IdUbicacionOrigen = pObjStock.IdUbicacion
                                selUbic.IdUbicacionDestino = pIdUbicacionDestino
                                selUbic.Tramo = pBeUbicacionDestino.Tramo.Descripcion
                                selUbic.Nivel = pBeUbicacionDestino.Nivel
                                selUbic.IsNew = True
                                lUbicSel.Add(selUbic)

                                Get_Info_Estado_Producto()

                                '#GT16062022_0840: aqui esta la magia para llenar lista de objetos para reubicar de forma masiva.
                                Agrega_Registros_Detalle()

                            End If

                        Else
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "Estado destino en la fila " & i + 1 & " no existe")
                        End If

                    Else
                        errorCampos = True
                        clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "La ubicación destino en la fila " & i + 1 & " no existe")
                    End If

                End If

#End Region

            Next

            If Not errorCampos Then

                If pListObjDet.Count > 0 Then

                    '#CKFK20220620 Agregué esto porque me daba error de duplicación
                    Dim MaxId As Integer = clsLnLog_importacion_excel.MaxID() + 1
                    beLogImportacion.IdImportacion = MaxId

                    '#GT20062022_1640: log del proceso completado.
                    clsLnLog_importacion_excel.Insertar(beLogImportacion)
                    Carga_Cambio_Estado = True

                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Se finalizó la lectura del archivo, se procesaran los datos a continuación.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    DialogResult = DialogResult.OK
                    'Close()

                End If

            Else

                Carga_Cambio_Estado = False

                SplashScreenManager.CloseForm(False)

                XtraMessageBox.Show("Se finalizó la lectura del archivo, y tiene errores que deben corregir.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

                '#GT15062022_1010: Se limpian los inputs para evitar que se presione cargar nuevamente e intente sobreescribir
                txtArchivo.Text = ""
                lObjStock.Clear()
                DsExcel.Clear()
                Grid.BeginUpdate()
                Grid.EndUpdate()
                Grid.ForceInitialize()

                cmdCargar.Enabled = True

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
            Cursor = Cursors.Default
        End Try


    End Function

    Private Sub preparaObjDet(pObjStock As clsBeVW_stock_res)

        pBeTransUbicHHDet = New clsBeTrans_ubic_hh_det()

        pBeTransUbicHHDet.IdTareaUbicacionDet = 0
        pBeTransUbicHHDet.IdStock = pObjStock.IdStock
        pBeTransUbicHHDet.Producto = New clsBeProducto
        pBeTransUbicHHDet.Stock = New clsBeStock
        pBeTransUbicHHDet.ProductoEstado = New clsBeProducto_estado
        pBeTransUbicHHDet.ProductoPresentacion = New clsBeProducto_Presentacion
        pBeTransUbicHHDet.UnidadMedida = New clsBeUnidad_medida
        pBeTransUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion
        pBeTransUbicHHDet.Producto.Nombre = pObjStock.Nombre_Producto
        pBeTransUbicHHDet.Producto.Codigo = pObjStock.Codigo_Producto
        pBeTransUbicHHDet.Stock.IdUbicacion_anterior = pObjStock.IdUbicacion
        pBeTransUbicHHDet.IdUbicacionOrigen = pObjStock.IdUbicacion
        pBeTransUbicHHDet.Stock.Fecha_vence = pObjStock.Fecha_Vence
        pBeTransUbicHHDet.Stock.Serial = pObjStock.Serial

        pBeTransUbicHHDet.IdBodega = AP.IdBodega

        'If (chkOperadorPorlinea.Checked) Then
        '    pObjDet.Operador = New clsBeOperador
        '    pObjDet.IdOperadorBodega = cmbOperadores.EditValue
        '    pObjDet.Operador.Nombres = cmbOperadores.Text.Trim()
        'End If

        pBeTransUbicHHDet.ProductoEstado.Nombre = pObjStock.NomEstado
        pBeTransUbicHHDet.Stock.Añada = pObjStock.Añada
        pBeTransUbicHHDet.Stock.Lote = pObjStock.Lote
        pBeTransUbicHHDet.Stock.Fecha_Ingreso = pObjStock.Fecha_ingreso
        pBeTransUbicHHDet.ProductoPresentacion.Nombre = pObjStock.Nombre_Presentacion
        '#GT16062022_1355: faltaban estos campos en el grid del frmCambioUbicación detalle.
        pBeTransUbicHHDet.ProductoPresentacion.IdPresentacion = pObjStock.IdPresentacion
        pBeTransUbicHHDet.ProductoPresentacion.Factor = pObjStock.Factor
        pBeTransUbicHHDet.UnidadMedida.Nombre = pObjStock.UMBas
        pBeTransUbicHHDet.Activo = True

    End Sub

    Private Sub Agrega_Registros_Detalle()

        Dim pObjUbicHHDet As clsBeTrans_ubic_hh_det

        Try

            Parallel.ForEach(lUbicSel, Sub(ByVal Ubic)
                                           If Ubic.IsNew Then
                                               pDetCorrel += 1
                                               pObjUbicHHDet = New clsBeTrans_ubic_hh_det()
                                               pObjUbicHHDet = pBeTransUbicHHDet.Clone
                                               pObjUbicHHDet.IdTareaUbicacionDet = pDetCorrel
                                               pObjUbicHHDet.IdUbicacionOrigen = pBeTransUbicHHDet.IdUbicacionOrigen
                                               pObjUbicHHDet.IdUbicacionDestino = Ubic.IdUbicacionDestino
                                               pObjUbicHHDet.Cantidad = Ubic.Ubicar
                                               pObjUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion()
                                               pObjUbicHHDet.UbicacionDestino.Descripcion = Ubic.Descripcion
                                               '#EJC20171025_0947AM: Si es cambio de ubicación estado origen y destino es el mismo.
                                               If Not EsCambioEstado Then
                                                   pObjUbicHHDet.IdEstadoDestino = BeEstadoProd.IdEstado
                                               Else
                                                   pObjUbicHHDet.IdEstadoDestino = pBeEstadoDestino.IdEstado
                                                   '#CKFK20240110 Puse esto en comentario porque en BeEstadoProd voy a enviar el estado destino
                                                   'pObjUbicHHDet.IdEstadoDestino = pObjDet.IdEstadoDestino
                                               End If
                                               pObjUbicHHDet.IdEstadoOrigen = BeEstadoProd.IdEstado
                                               '#EJC20171023_0158PM: Se utilizaba instancia de la forma de ubicación en vez de lista. Ver -> '#EJC20171023_0356PM_REF
                                               pListObjDet.Add(pObjUbicHHDet)
                                               Dim pObjStockMov As New clsBeStock() With {.IdStockOrigen = pBeTransUbicHHDet.IdStock, .IdStock = pBeTransUbicHHDet.IdStock}
                                               clsLnStock.GetSingle(pObjStockMov)
                                               '#EJC20171014_1016PM: Se agregó esto porque si se cambia de ubicación, la ubicación anterior del stock, no será su úlitma ubicación (Confusio)
                                               pObjStockMov.IdUbicacion_anterior = pBeTransUbicHHDet.IdUbicacionOrigen
                                               pObjUbicHHDet.Stock = pObjStockMov
                                               If EsCambioEstado Then
                                                   pObjStockMov.ProductoEstado.IdEstado = pBeTransUbicHHDet.IdEstadoDestino
                                               Else
                                                   pObjStockMov.ProductoEstado.IdEstado = pObjStockMov.IdProductoEstado
                                               End If
                                               pObjStockMov.Cantidad = Ubic.Ubicar
                                               pObjStockMov.Producto = New clsBeProducto()
                                               Dim Producto = clsLnProducto.Get_Single_BeProducto_By_IdProductoBodega(pObjStockMov.IdProductoBodega)
                                               '#GT29052024: validar que no sea nothing
                                               If Producto Is Nothing Then
                                                   SplashScreenManager.CloseForm(False)
                                                   Throw New Exception("No se encontro el producto.")
                                               Else
                                                   pObjStockMov.Producto = Producto
                                               End If
                                               clsLnProducto.GetSingle(pObjStockMov.Producto)
                                               'clsLnProducto.GetSingle(clsLnProducto.Get_Single_BeProducto_By_IdProductoBodega(pObjStockMov.IdProductoBodega))
                                               If EsCambioEstado Then
                                                   pObjUbicHHDet.ProductoEstado.IdEstado = pBeTransUbicHHDet.IdEstadoDestino
                                               Else
                                                   pObjUbicHHDet.ProductoEstado.IdEstado = pObjStockMov.IdProductoEstado
                                               End If
                                               clsLnProducto_presentacion.GetSingle(pObjUbicHHDet.ProductoPresentacion)
                                               pObjUbicHHDet.ProductoPresentacion.IdPresentacion = pObjStockMov.IdPresentacion
                                               Cargar_Movimiento(pObjUbicHHDet, pObjStockMov)
                                               pObjStockMov.IdUbicacion = Ubic.IdUbicacionDestino
                                               pListStockMov.Add(pObjStockMov)
                                               Ubic.IsNew = False
                                           End If
                                       End Sub)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Get_Info_Estado_Producto()

        Try

            BeEstadoProd.IdEstado = pObjStock.IdProductoEstado
            BeEstadoProd = clsLnProducto_estado.Get_Single_By_IdEstado(BeEstadoProd.IdEstado)

            If BeEstadoProd Is Nothing Then
                Throw New Exception("No se pudo obtener el estado del producto")
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Cargar_Movimiento(ByVal det As clsBeTrans_ubic_hh_det, ByVal pObjStock As clsBeStock)

        Try

            Dim mov As New clsBeTrans_movimientos() _
                With {.IdEmpresa = AP.IdEmpresa,
                .IdBodegaOrigen = AP.IdBodega,
                .IdTransaccion = det.IdTareaUbicacionEnc,
                .IdPropietarioBodega = pObjStock.IdPropietarioBodega,
                .IdProductoBodega = pObjStock.IdProductoBodega,
                .IdUbicacionOrigen = pObjStock.IdUbicacion,
                .IdUbicacionDestino = det.IdUbicacionDestino,
                .IdPresentacion = pObjStock.IdPresentacion,
                .IdEstadoOrigen = pObjStock.IdProductoEstado,
                .IdEstadoDestino = pObjStock.IdProductoEstado}

            If EsCambioEstado Then
                mov.IdEstadoDestino = pBeTransUbicHHDet.IdEstadoDestino
            End If

            '#EJC20170913 - Tomar en cuenta para cambio de estado a futuro!!!!!!!!!!!!!!
            'If Not String.IsNullOrEmpty(txtIdEstado.Text.Trim()) Then
            '    mov.IdEstadoDestino = det.IdEstadoDestino
            'Else
            '    mov.IdEstadoDestino = pObjStock.IdProductoEstado
            'End If
            mov.IdUnidadMedida = pObjStock.IdUnidadMedida
            mov.IdTipoTarea = 2 'TAREA UBI'
            mov.IdBodegaDestino = AP.IdBodega
            mov.IdRecepcion = pObjStock.IdRecepcionEnc
            mov.IdRecepcionDet = pObjStock.IdRecepcionDet
            mov.Cantidad = det.Cantidad
            mov.Serie = det.Stock.Serial
            mov.Peso = det.Stock.Peso
            mov.Lote = det.Stock.Lote
            mov.Fecha_vence = pObjStock.Fecha_vence
            mov.Fecha = pObjStock.Fecha_Ingreso
            mov.Barra_pallet = pObjStock.Lic_plate
            mov.Hora_ini = Now
            mov.Hora_fin = Now
            mov.Fecha_agr = Now
            mov.Usuario_agr = AP.IdRol
            mov.Cantidad_hist = det.Stock.Cantidad
            mov.Peso_hist = det.Stock.Peso

            If mov.IdPresentacion <> 0 Then

                Dim BePresentacion As New clsBeProducto_Presentacion
                BePresentacion = clsLnProducto_presentacion.GetSingle(mov.IdPresentacion)

                If Not BePresentacion Is Nothing Then
                    If BePresentacion.Factor = 0 Then
                        Throw New Exception("ERR20220202_1458: El factor de la presentación es 0. esto crearía un movimiento no válido para el sistema, valide el factor de la presentación. Identificador de presentación: " & mov.IdPresentacion)
                    Else
                        mov.Cantidad = Math.Round(mov.Cantidad * BePresentacion.Factor, 6)
                        mov.Cantidad_hist = Math.Round(mov.Cantidad_hist * BePresentacion.Factor, 6)
                    End If
                Else
                    Throw New Exception("ERR20220202_1458: No se encontró el objeto de presentación para el identificador: " & mov.IdPresentacion)
                End If

            End If

            pListObjMov.Add(mov)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

#End Region


#Region "Importar reabasto"

    Dim pRack As String = ""
    Dim pColIni As Integer = 0
    Dim pColFin As Integer = 0
    Dim pNivelIni As Integer = 0
    Dim pNIvelFin As Integer = 0
    Dim pCodProducto As String = ""
    Dim pCantidadMin As Integer = 0
    Dim pCantidadMax As Integer = 0
    Dim pAbastoCon As String = ""
    Dim pAbastoA As String = ""
    Dim pObservacion As String = ""
    Dim pEstado As String = ""
    Dim pOperador As String = ""
    Dim pBodega As Integer = 0
    Dim pIdUbicacion As Integer = 0
    Dim pDescripcionProducto As String = ""
    Private pBeproductoRellenadoList As New List(Of clsBeProducto_rellenado)
    Private pBodegaUbicacionList As List(Of clsBeBodega_ubicacion)
    Private Function Cargar_Reabasto(ByVal pDT As DataTable) As Boolean

        Cargar_Reabasto = False
        Dim vValorCelda As String = ""
        Dim pProducto As New clsBeProducto
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim firstCol As Integer = 0
        Dim lastCol As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Procesando Encabezados del archivo...")


            Dim Obj As clsExcel = pListObj.Find(Function(s) s.Checked = True)
            Dim fileName As String = txtArchivo.Text
            Dim documento As XLWorkbook = New XLWorkbook(fileName)
            Dim documento1 As IXLWorksheet = documento.Worksheet(Obj.Index + 1)

            Dim vCantRegistros As Integer = pDT.Rows.Count
            Dim vIndicadorFila As Integer = 1
            Dim errorCampos As Boolean = False
            Dim errorFila As Boolean = False

            Cursor = Cursors.WaitCursor

            'For Each row As IXLRow In pDT.Rows
            For Each row As IXLRow In documento1.Rows

                Debug.WriteLine("Row its at: " & row.RowNumber & " and vIndicadorFila is at: " & vIndicadorFila)

                '#GT23062022_1050: iteramos columnas de la primera fila para confirmar campos
                If vIndicadorFila = 1 Then

                    If Not row.FirstCellUsed Is Nothing Then

                        firstCol = row.FirstCellUsed().Address.ColumnNumber
                        lastCol = row.LastCellUsed().Address.ColumnNumber

                        For Each cell As IXLCell In row.Cells(firstCol, lastCol)

                            If cell.Address.ColumnLetter = "A" AndAlso cell.Address.ColumnNumber = "1" Then
                                If Not cell.Value.ToString.ToUpper = "NumeroRack" AndAlso Not cell.Value.ToString.ToUpper = "Numero de Rack" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Numero de Rack en la celda A1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "B" AndAlso cell.Address.ColumnNumber = "2" Then
                                If Not cell.Value.ToString.ToUpper = "Codigo Ubicación" AndAlso Not cell.Value.ToString.ToUpper = "Ubicación" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo de Ubicación en la celda B1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "C" AndAlso cell.Address.ColumnNumber = "3" Then
                                If Not cell.Value.ToString = "Columna Inicial" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Columna Inicial en la celda C1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "D" AndAlso cell.Address.ColumnNumber = "4" Then
                                If Not cell.Value.ToString.Trim = "Columna Final" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Columna Final en la celda D1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "E" AndAlso cell.Address.ColumnNumber = "5" Then
                                If Not cell.Value.ToString.Trim = "Nivel Inicial" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Nivel Inicial en la celda E1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "F" AndAlso cell.Address.ColumnNumber = "6" Then
                                If Not cell.Value.ToString.Trim = "Nivel Final" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Nivel Final en la celda F1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "G" AndAlso cell.Address.ColumnNumber = "7" Then
                                If Not cell.Value.ToString = "Código Producto" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Código Producto en la celda G1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "H" AndAlso cell.Address.ColumnNumber = "8" Then
                                If Not cell.Value.ToString = "Cantidad Mínima" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Cantidad Mínima en la celda H1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "I" AndAlso cell.Address.ColumnNumber = "9" Then
                                If Not cell.Value.ToString = "Cantidad Máxima" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Cantidad Máxima en la celda I1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "J" AndAlso cell.Address.ColumnNumber = "10" Then
                                If Not cell.Value.ToString = "Abastecer Con" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Abastecer Con en la celda J1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "K" AndAlso cell.Address.ColumnNumber = "11" Then
                                If Not cell.Value.ToString = "Abastecer A" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Abastecer A en la celda K1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "L" AndAlso cell.Address.ColumnNumber = "12" Then
                                If Not cell.Value.ToString = "OBSERVACION" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo OBSERVACION en la celda L1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "M" AndAlso cell.Address.ColumnNumber = "13" Then
                                If Not cell.Value.ToString = "Estado" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Estado en la celda M1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "N" AndAlso cell.Address.ColumnNumber = "14" Then
                                If Not cell.Value.ToString = "Operador" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Operador en la celda N1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "O" AndAlso cell.Address.ColumnNumber = "15" Then
                                If Not cell.Value.ToString = "Bodega" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel No contiene el campo Bodega en la celda O1")
                                End If
                            End If

                            If cell.Address.ColumnLetter = "P" AndAlso cell.Address.ColumnNumber = "16" Then
                                If Not cell.Value.ToString = "Descripción" Then
                                    errores = True
                                    clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "El formato en excel no contiene el campo Descripción en la celda P1")
                                End If
                            End If

                        Next

                    End If

                    vIndicadorFila += 1

                ElseIf (vIndicadorFila = 2) Then

                    For i As Integer = 0 To pDT.Rows.Count - 1

                        SplashScreenManager.Default.SetWaitFormCaption("Procesando linea: " & i + 1)

                        'GT29062022: valor del rack
                        If String.IsNullOrEmpty(pDT(i)(0).ToString) Then
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: " & "No hay un valor de Rack en la fila " & i + 1)
                        Else
                            pRack = Trim(pDT(i)(0))
                        End If

                        'GT29062022: valor de la úbicación
                        If IsNumeric(pDT(i)(1)) Then
                            pIdUbicacion = pDT(i)(1)
                        ElseIf pDT(i)(1) = "" Then
                            pIdUbicacion = 0
                        Else
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un valor númerico para la Ubicación en la fila " & i + 1)
                        End If

                        'GT29062022: valor de col inicial
                        If IsNumeric(pDT(i)(2)) Then
                            pColIni = pDT(i)(2)
                        Else
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un valor númerico para Columna Inicial en la fila " & i + 1)
                        End If

                        'GT29062022: valor de col final
                        If IsNumeric(pDT(i)(3)) Then
                            pColFin = pDT(i)(3)
                        Else
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un valor númerico para Columna Final en la fila " & i + 1)
                        End If

                        'GT29062022: valor de nivel inicial
                        If IsNumeric(pDT(i)(4)) Then
                            pNivelIni = pDT(i)(4)
                        Else
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un valor númerico para Nivel Inicial en la fila " & i + 1)
                        End If

                        'GT29062022: valor de nivel final
                        If IsNumeric(pDT(i)(5)) Then
                            pNIvelFin = pDT(i)(5)
                        Else
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un valor númerico para Nivel Final en la fila " & i + 1)
                        End If

                        'GT29062022: valor de codigo producto
                        If String.IsNullOrEmpty(pDT(i)(6).ToString) Then
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un código de producto en la fila " & i + 1)
                        Else
                            pCodProducto = Trim(pDT(i)(6))
                        End If

                        'GT29062022: valor de cantidad minima
                        If IsNumeric(pDT(i)(7)) Then
                            pCantidadMin = pDT(i)(7)
                        Else
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un valor númerico para Cantidad Minima en la fila " & i + 1)
                        End If

                        'GT29062022: valor de cantidad maxima
                        If IsNumeric(pDT(i)(8)) Then
                            pCantidadMax = pDT(i)(8)
                        Else
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un valor númerico para Cantidad Maxima en la fila " & i + 1)
                        End If

                        If String.IsNullOrEmpty(pDT(i)(9).ToString) Then
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un ABASTO CON en la fila " & i + 1)
                        Else
                            pAbastoCon = Trim(pDT(i)(9))
                        End If

                        If String.IsNullOrEmpty(pDT(i)(10).ToString) Then
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un ABASTO A en la fila " & i + 1)
                        Else
                            pAbastoA = Trim(pDT(i)(10))
                        End If

                        If pDT(i)(11) Is DBNull.Value Or pDT(i)(11) Is Nothing Then
                            pObservacion = ""
                        Else
                            pObservacion = pDT(i)(11)
                        End If

                        If String.IsNullOrEmpty(pDT(i)(12).ToString) Then
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un Estado en la fila " & i + 1)
                        Else
                            pEstado = Trim(pDT(i)(12))
                        End If

                        If String.IsNullOrEmpty(pDT(i)(13).ToString) Then
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un Operador en la fila " & i + 1)
                        Else
                            pOperador = Trim(pDT(i)(13))
                        End If

                        If IsNumeric(pDT(i)(14)) Then
                            pBodega = pDT(i)(14)
                        Else
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay un valor de Bodega en la fila " & i + 1)
                        End If

                        If String.IsNullOrEmpty(pDT(i)(15).ToString) Then
                            errorCampos = True
                            clsPublic.Actualizar_Progreso(lblPrg, "Error : " & "No hay una Descripción de producto en la fila " & i + 1)
                        Else
                            pDescripcionProducto = pDT(i)(15)
                        End If

                        'GT27062022_1140: las columnas y la data de la fila es legible
                        If Not errorCampos Then

                            pBodegaUbicacionList = Procesar_Ubicacion_Reabasto(lConnection, lTransaction)

                            If Not Procesar_Producto_Reabasto(pBodegaUbicacionList, lConnection, lTransaction) Then
                                errorFila = True
                            End If

                        Else
                            errorFila = True
                        End If
                    Next

                    vIndicadorFila += 1

                End If

            Next row

            If errorFila Then

                SplashScreenManager.CloseForm(False)
                errores = True
                If lTransaction IsNot Nothing Then lTransaction.Rollback()

            Else

                lTransaction.Commit()

                SplashScreenManager.Default.SetWaitFormCaption("Guardando Reabastos...")

                If Not Guardar_Rebasto() Then
                    Cargar_Reabasto = False
                    SplashScreenManager.CloseForm(False)
                    Exit Function
                End If

                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("Proceso completado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Cargar_Reabasto = True

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        Finally
            SplashScreenManager.CloseForm(False)
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            Cursor = Cursors.Arrow
            cmdCargar.Enabled = True
        End Try

    End Function

    Private Function Guardar_Rebasto() As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Guardar_Rebasto = False

        Try
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lMaxID = 0
            lMaxID = clsLnProducto_rellenado.MaxID(lConnection, lTransaction)

            For Each ObjPRE As clsBeProducto_rellenado In pBeproductoRellenadoList
                If ObjPRE.IsNew Then
                    lMaxID += 1
                    ObjPRE.IdRellenado = lMaxID
                    clsLnProducto_rellenado.Insertar(ObjPRE, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

            Guardar_Rebasto = True

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        Finally
            SplashScreenManager.CloseForm(False)
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Private Function Procesar_Ubicacion_Reabasto(Optional ByVal pConection As SqlConnection = Nothing,
                                                 Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of clsBeBodega_ubicacion)

        Procesar_Ubicacion_Reabasto = Nothing

        Dim pBodegaTramo As New clsBeBodega_tramo
        Dim pBodegaUbicacionList As New List(Of clsBeBodega_ubicacion)

        SplashScreenManager.Default.SetWaitFormCaption("Validando Ubicaciones para Reabasto...")

        Try
            'GT27062022_1040: obtengo el Tramo usando el valor del archivo excel
            pBodegaTramo = clsLnBodega_tramo.GetSingle_by_Descripcion(pRack, pBodega,
                                                                            pConection,
                                                                            pTransaction)
            If Not pBodegaTramo Is Nothing Then
                pBodegaUbicacionList = clsLnBodega_ubicacion.Get_List_By_Tramo_And_IdBodega(pBodegaTramo.IdTramo, pBodega,
                                                                                                pColIni, pColFin, pNivelIni,
                                                                                                                  pNIvelFin,
                                                                                                                  pConection,
                                                                                                                  pTransaction)

                'GT27062022_1140: itero las ubicaciones del Tramo para setearlas como reabasto.
                If Not pBodegaUbicacionList Is Nothing Then

                    clsLnBodega_ubicacion.Update_To_Reabasto(pBodegaTramo.IdTramo, pBodega, pColIni, pColFin, pNivelIni,
                                                                                                pNIvelFin,
                                                                                                pConection,
                                                                                                pTransaction)
                    Procesar_Ubicacion_Reabasto = pBodegaUbicacionList
                Else
                    clsPublic.Actualizar_Progreso(lblPrg, String.Format("Error: No hay ubicaciones para asignar como reabasto, en el Rack {0} 
                                                    Columna inicial {1} Columna final {2} 
                                                    Nivel inicial {3} Nivel final {4} ", pRack, pColIni, pColFin, pNivelIni, pNIvelFin))
                End If
            Else
                clsPublic.Actualizar_Progreso(lblPrg, "Error: No existe en la bodega, el Rack " & pRack)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)

        End Try

    End Function

    Private Function Procesar_Producto_Reabasto(pListBeBodegaUbicacion As List(Of clsBeBodega_ubicacion),
                                                Optional ByVal pConection As SqlConnection = Nothing,
                                                Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Procesar_Producto_Reabasto = False
        Dim BeProductoRellenado As New clsBeProducto_rellenado
        Dim pProdPresentacion As New clsBeProducto_Presentacion
        Dim pProdPresentacionCon As New clsBeProducto_Presentacion
        Dim pProdEstado As New clsBeProducto_estado
        Dim pProducto As New clsBeProducto
        Dim pOperador_ As New clsBeOperador
        Dim pError As Boolean = False
        Dim existe_ubicacion As Integer = 0
        Dim existe_rellenado As Integer = 0
        Dim pIdProductoBodega As Integer
        Dim separator As String = " "
        Dim separatorIndex As Integer
        Dim nombre As String = ""
        Dim apellido As String = ""
        Dim pIdUnidadMedidaBasica As Integer = 0
        Dim pIdPresentacion As Integer = 0
        Dim pIdProdEstado As Integer = 0
        Dim pIdPropietario As Integer = 0
        Dim pIdPresentacionAbastercerCon As Integer = 0
        Dim pIdOperador As Integer = 0
        Dim pPresentacionNombre As String = ""
        Dim pProdEstadoNombre As String = ""
        Dim vProdRellenado As Integer = 0
        Dim BeProducto As New clsBeProducto

        SplashScreenManager.Default.SetWaitFormCaption("Validando Producto para Reabasto...")

        Try

            pProducto = clsLnProducto.Get_BeProducto_By_Codigo(pCodProducto, pBodega, pConection, pTransaction)

            If Not pProducto Is Nothing Then

                pIdUnidadMedidaBasica = pProducto.IdUnidadMedidaBasica
                pIdPropietario = pProducto.Propietario.IdPropietario

                pProdPresentacion = clsLnProducto_presentacion.Get_By_Codigo_Producto_And_Presentacion(pProducto.Codigo, pAbastoA,
                                                                                                           pConection,
                                                                                                           pTransaction)

                If pProdPresentacion Is Nothing Then
                    pError = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: La Presentación REABASTO_CON del producto" & pCodProducto & ", no es valida.")
                Else
                    pIdPresentacion = pProdPresentacion.IdPresentacion
                    pPresentacionNombre = pProdPresentacion.Nombre
                End If

                pProdEstado = clsLnProducto_estado.Get_ProductoEstado_By_IdPropietario_and_DescEstado(pProducto.Propietario.IdPropietario,
                                                                                                                    pEstado,
                                                                                                                    pConection,
                                                                                                                    pTransaction)

                If pProdEstado Is Nothing Then
                    pError = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: El Estado para reabasto del producto" & pCodProducto & ", no es valida.")
                Else
                    pIdProdEstado = pProdEstado.IdEstado
                    pProdEstadoNombre = pProdEstado.Nombre
                End If

                pIdProductoBodega = clsLnProducto.Get_IdProductoBodega_By_Codigo(pProducto.Codigo,
                                                                                        pConection,
                                                                                        pTransaction)

                pProdPresentacionCon = clsLnProducto_presentacion.Get_By_Codigo_Producto_And_Presentacion(pProducto.Codigo, pAbastoCon,
                                                                                                              pConection,
                                                                                                              pTransaction)
                If pProdPresentacionCon Is Nothing Then
                    pError = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: La Presentación de REABASTO_A del producto" & pCodProducto & ", no es valida.")
                Else
                    pIdPresentacionAbastercerCon = pProdPresentacionCon.IdPresentacion
                End If

            Else
                pError = True
                clsPublic.Actualizar_Progreso(lblPrg, "Error: El producto no se pudo obtener para el código " & pCodProducto & ".")
            End If

            'GT27062022_1930: Datos del operador en el archivo importado
            separator = " "

            If pOperador <> "" Then

                separatorIndex = pOperador.IndexOf(separator)
                nombre = pOperador.Substring(0, separatorIndex)
                apellido = pOperador.Substring(separatorIndex + separator.Length)

                pOperador_ = New clsBeOperador
                pOperador_ = clsLnOperador.Get_Operador_By_Name(pOperador_,
                                                                pOperador,
                                                                pConection,
                                                                pTransaction)

                If pOperador_ Is Nothing Then
                    pError = True
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: El operador  " & pOperador & ", no es valido.")
                Else
                    pIdOperador = pOperador_.IdOperador
                End If
            Else
                pError = True
                clsPublic.Actualizar_Progreso(lblPrg, "Error: El operador  " & pOperador & ", no es valido.")
            End If

            If pIdUbicacion <> 0 Then

                existe_ubicacion = clsLnBodega_ubicacion.Exists(pIdUbicacion, pBodega, pConection, pTransaction)
                existe_rellenado = clsLnProducto_rellenado.Existe_Ubicacion_Rellenado(pIdUbicacion, pBodega, pConection, pTransaction)

                If existe_ubicacion > 0 Then

                    If existe_rellenado = 0 Then

                        BeProductoRellenado = New clsBeProducto_rellenado

                        BeProductoRellenado.IdPresentacion = pIdPresentacion
                        BeProductoRellenado.Presentacion = pPresentacionNombre
                        BeProductoRellenado.IdProductoEstado = pProdEstado.IdEstado
                        BeProductoRellenado.Estado = pProdEstado.Nombre

                        '#GT28062022_0900: por defecto notificar
                        BeProductoRellenado.IdTipoAccion = 1
                        BeProductoRellenado.Minimo = pCantidadMin
                        BeProductoRellenado.Maximo = pCantidadMax
                        BeProductoRellenado.User_agr = AP.UsuarioAp.IdUsuario
                        BeProductoRellenado.Fec_agr = Now
                        BeProductoRellenado.User_mod = AP.UsuarioAp.IdUsuario
                        BeProductoRellenado.Fec_mod = Now
                        BeProductoRellenado.Activo = True
                        BeProductoRellenado.IsNew = True
                        BeProductoRellenado.IdBodega = pBodega
                        BeProductoRellenado.IdUnidadMedidaBasica = pIdUnidadMedidaBasica
                        BeProductoRellenado.IdProductoBodega = pIdProductoBodega
                        BeProductoRellenado.IdPropietario = pIdPropietario
                        BeProductoRellenado.IdUbicacion = pIdUbicacion
                        BeProductoRellenado.IdPresentacionAbastercerCon = pIdPresentacionAbastercerCon
                        BeProductoRellenado.NomPresentacionRellenarCon = pAbastoCon
                        BeProductoRellenado.IdOperadorDefecto = pIdOperador
                        BeProductoRellenado.NomOperador = pOperador

                        vProdRellenado = clsLnProducto_rellenado.Existe_Configuracion_Producto(BeProductoRellenado, pConection, pTransaction)

                        If vProdRellenado <> 0 Then
                            BeProductoRellenado.IdRellenado = vProdRellenado
                            clsLnProducto_rellenado.Eliminar(BeProductoRellenado, pConection, pTransaction)
                        End If

                        pBeproductoRellenadoList.Add(BeProductoRellenado)

                    Else

                        BeProducto = New clsBeProducto

                        BeProducto.IdProductoBodega = existe_rellenado
                        clsLnProducto_bodega.Get_BeProducto_By_IdProductoBodega(BeProducto.IdProductoBodega, pConection, pTransaction)

                        If BeProducto IsNot Nothing Then
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: La ubicación para reabasto " & pIdUbicacion & ", ya está asociada al producto " & BeProducto.Codigo)
                        Else
                            clsPublic.Actualizar_Progreso(lblPrg, "Error: La ubicación para reabasto " & pIdUbicacion & ", ya está asociada a otro producto ")
                        End If

                    End If

                Else
                    clsPublic.Actualizar_Progreso(lblPrg, "Error: La ubicación para reabasto " & pIdUbicacion & ", no es válida.")
                End If

            Else

                If pListBeBodegaUbicacion IsNot Nothing Then

                    For Each ubi In pListBeBodegaUbicacion

                        pIdUbicacion = ubi.IdUbicacion

                        existe_rellenado = clsLnProducto_rellenado.Existe_Ubicacion_Rellenado(pIdUbicacion, pBodega, pConection, pTransaction)

                        If existe_rellenado = 0 OrElse existe_rellenado = pIdProductoBodega Then

                            BeProductoRellenado = New clsBeProducto_rellenado

                            BeProductoRellenado.IdPresentacion = pIdPresentacion
                            BeProductoRellenado.Presentacion = pPresentacionNombre

                            BeProductoRellenado.IdProductoEstado = pIdProdEstado
                            BeProductoRellenado.Estado = pProdEstadoNombre

                            '#GT28062022_0900: por defecto notificar
                            BeProductoRellenado.IdTipoAccion = 1
                            BeProductoRellenado.Minimo = pCantidadMin
                            BeProductoRellenado.Maximo = pCantidadMax
                            BeProductoRellenado.User_agr = AP.UsuarioAp.IdUsuario
                            BeProductoRellenado.Fec_agr = Now
                            BeProductoRellenado.User_mod = AP.UsuarioAp.IdUsuario
                            BeProductoRellenado.Fec_mod = Now
                            BeProductoRellenado.Activo = True
                            BeProductoRellenado.IsNew = True
                            BeProductoRellenado.IdBodega = pBodega
                            BeProductoRellenado.IdUnidadMedidaBasica = pIdUnidadMedidaBasica
                            BeProductoRellenado.IdProductoBodega = pIdProductoBodega
                            BeProductoRellenado.IdPropietario = pIdPropietario
                            BeProductoRellenado.IdUbicacion = pIdUbicacion
                            BeProductoRellenado.IdPresentacionAbastercerCon = pIdPresentacionAbastercerCon
                            BeProductoRellenado.NomPresentacionRellenarCon = pAbastoCon
                            BeProductoRellenado.IdOperadorDefecto = pIdOperador
                            BeProductoRellenado.NomOperador = pOperador

                            vProdRellenado = clsLnProducto_rellenado.Existe_Configuracion_Producto(BeProductoRellenado, pConection, pTransaction)

                            If vProdRellenado <> 0 Then
                                BeProductoRellenado.IdRellenado = vProdRellenado
                                clsLnProducto_rellenado.Eliminar(BeProductoRellenado, pConection, pTransaction)
                            End If

                            pBeproductoRellenadoList.Add(BeProductoRellenado)

                        Else

                            BeProducto = New clsBeProducto

                            BeProducto.IdProductoBodega = existe_rellenado
                            BeProducto = clsLnProducto_bodega.Get_BeProducto_By_IdProductoBodega(BeProducto.IdProductoBodega, pConection, pTransaction)

                            If BeProducto IsNot Nothing Then
                                clsPublic.Actualizar_Progreso(lblPrg, "Error: La ubicación para reabasto " & pIdUbicacion & ", ya está asociada al producto " & BeProducto.Codigo)
                            Else
                                clsPublic.Actualizar_Progreso(lblPrg, "Error: La ubicación para reabasto " & pIdUbicacion & ", ya está asociada a otro producto ")
                            End If

                        End If

                    Next
                Else
                    clsPublic.Actualizar_Progreso(lblPrg, String.Format("Error: Las ubicaciones para reabasto
                                                    en el Rack {0} con la columna inicial {1} 
                                                    y final {2} y Nivel inicial {3} y final {4} , no son válidas.", pRack, pColIni, pColFin, pNivelIni, pNIvelFin))
                End If

            End If

            If Not pError Then
                Procesar_Producto_Reabasto = True
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Function

#End Region

    Private Function Actualizar_Indices_Rotacion_Producto(ByVal pDT As DataTable) As Boolean

        Dim pListObjUM As New List(Of clsBeUnidad_medida)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim error_UMBAS As Boolean = False
        Dim vCodigoProducto As String = ""
        Dim vIndiceRotacion As String = ""


        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Cursor = Cursors.WaitCursor
            errores = False

            Dim IdPropietario = CInt(pcmbP.SelectedValue)

            For i As Integer = 0 To pDT.Rows.Count - 1

                Application.DoEvents()

                vCodigoProducto = pDT(i)(0)
                vIndiceRotacion = pDT(i)(1)

                vIndiceRotacion = clsLnIndice_rotacion.GetIdIndiceRotacionByNombre(vIndiceRotacion, lConnection, lTransaction)

                If vIndiceRotacion = 0 Then
                    Dim BeIndiceRotacion As New clsBeIndice_rotacion
                    BeIndiceRotacion.IdIndiceRotacion = clsLnIndice_rotacion.MaxID(lConnection, lTransaction)
                    BeIndiceRotacion.Descripcion = vIndiceRotacion
                    BeIndiceRotacion.Activo = True
                    BeIndiceRotacion.Grupo = 0
                    BeIndiceRotacion.IndicePrioridad = 0
                    clsLnIndice_rotacion.Insertar(BeIndiceRotacion, lConnection, lTransaction)
                    vIndiceRotacion = BeIndiceRotacion.IdIndiceRotacion
                    clsPublic.Actualizar_Progreso(lblPrg, "El índice: " & vIndiceRotacion & " no existía y se insertó automaticamente.")
                End If

                clsLnProducto.Actualizar_Indice_Rotacion(vCodigoProducto,
                                                         vIndiceRotacion,
                                                         AP.UsuarioAp.IdUsuario,
                                                         lConnection,
                                                         lTransaction)

                clsPublic.Actualizar_Progreso(lblPrg, "Actualizando código: " & vCodigoProducto & " índice: " & vIndiceRotacion)

            Next

            lTransaction.Commit()

            clsPublic.Actualizar_Progreso(lblPrg, "Fin de proceso")

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            Cursor = Cursors.Default
            SplashScreenManager.CloseForm(False)
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Private Function Actualizar_Indices_Rotacion_Bodega(ByVal pDT As DataTable) As Boolean

        Dim pListObjUM As New List(Of clsBeUnidad_medida)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim error_UMBAS As Boolean = False
        Dim vIdUbicacion As String = ""
        Dim vIndiceRotacion As String = ""
        Dim vNombreIndiceRotacion As String = ""

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Procesando archivo...")

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Cursor = Cursors.WaitCursor
            errores = False

            Dim IdPropietario = CInt(pcmbP.SelectedValue)

            For i As Integer = 0 To pDT.Rows.Count - 1

                Application.DoEvents()

                vIdUbicacion = pDT(i)(0)
                vNombreIndiceRotacion = pDT(i)(1)

                vIndiceRotacion = clsLnIndice_rotacion.GetIdIndiceRotacionByNombre(vNombreIndiceRotacion, lConnection, lTransaction)

                If vIndiceRotacion = 0 Then
                    Dim BeIndiceRotacion As New clsBeIndice_rotacion
                    BeIndiceRotacion.IdIndiceRotacion = clsLnIndice_rotacion.MaxID(lConnection, lTransaction)
                    BeIndiceRotacion.Descripcion = vNombreIndiceRotacion
                    BeIndiceRotacion.Activo = True
                    BeIndiceRotacion.Grupo = 0
                    BeIndiceRotacion.IndicePrioridad = 0
                    clsLnIndice_rotacion.Insertar(BeIndiceRotacion, lConnection, lTransaction)
                    vIndiceRotacion = BeIndiceRotacion.IdIndiceRotacion
                    clsPublic.Actualizar_Progreso(lblPrg, "El índice: " & vIndiceRotacion & " no existía y se insertó automaticamente.")
                End If

                clsLnBodega_ubicacion.Actualizar_Indice_Rotacion_Bodega(vIdUbicacion,
                                                                        vIndiceRotacion,
                                                                        AP.UsuarioAp.IdUsuario,
                                                                        lConnection,
                                                                        lTransaction)

                clsPublic.Actualizar_Progreso(lblPrg, "Actualizando Ubicación: " & vIdUbicacion & " índice: " & vIndiceRotacion)

            Next

            lTransaction.Commit()

            clsPublic.Actualizar_Progreso(lblPrg, "Fin de proceso")

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            Cursor = Cursors.Default
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

End Class
