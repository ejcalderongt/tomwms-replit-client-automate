Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraSplashScreen
Imports Microsoft.Office.Interop

Public Class frmCargaExcel_Anterior

    Public Delegate Sub Operar()
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

    Private errores As Boolean = False

    Public tipo_archivo As String = ""

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
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ButtonEdit1_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtArchivo.Properties.ButtonClick

        Try

            Dim ObjO As New OpenFileDialog() With {.Filter = "All Files|*.*|Excel Files(.xls)|*.xls|Excel Files(.xlsx)|*.xlsx|Excel Files(*.xlsm)|*.xlsm"}
            If ObjO.ShowDialog() = DialogResult.OK AndAlso ObjO.FileName.Length <> 0 Then
                txtArchivo.Text = ObjO.FileName
                If IO.Path.GetExtension(txtArchivo.Text).ToUpper = ".XLS" Then
                    tipo_archivo = "xls"
                Else
                    tipo_archivo = "xlsx"
                End If
                CargaHojas(ObjO.FileName)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub CargaHojas(ByVal pFileName As String)

        DsExcel.Clear()
        Grid.BeginUpdate()
        Dim xlApp As New Excel.Application

        Try

            xlApp.Workbooks.Open(pFileName)
            pListObj = New List(Of clsExcel)

            Dim i As Integer = -1
            For Each sheet As Excel.Worksheet In xlApp.Worksheets
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
            RemoveHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged

            Application.DoEvents()

        Catch ex As Exception
            xlApp.Workbooks.Close()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Try
                xlApp.Workbooks.Close()
            Catch ex As Exception
            End Try
            If Not xlApp Is Nothing Then xlApp.Quit()
            Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)
            xlApp = Nothing
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
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Private Function Cargar_Archivo_Excel() As Boolean

        Cargar_Archivo_Excel = False

        Try

            Dim Obj As clsExcel = pListObj.Find(Function(s) s.Checked = True)
            Dim vCadenaConexionExcel As String = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1;""", txtArchivo.Text.Trim)
            Dim vCadenaConexionExcel2 As String = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""", txtArchivo.Text.Trim)
            Dim lConnection As New OleDbConnection(vCadenaConexionExcel)
            Dim lConnection2 As New OleDbConnection(vCadenaConexionExcel2)

            Try

                DT = New DataTable("Carga")

                lConnection.Open()

                Using lDataAdapter As New OleDbDataAdapter(String.Format("SELECT * FROM [{0}$]", Obj.NombreHoja.Trim()), lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.Fill(DT)
                End Using

            Catch ex As Exception

                XtraMessageBox.Show(String.Format("Error al intentar abrir excel con Microsoft.ACE.OLEDB.12 " & ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

                If lConnection.State = ConnectionState.Open Then lConnection.Close()

                Try

                    lConnection2.Open()

                    Using lDataAdapter As New OleDbDataAdapter(String.Format("SELECT * FROM [{0}$]", Obj.NombreHoja.Trim()), lConnection2)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.Fill(DT)
                    End Using

                Catch ex1 As Exception
                    XtraMessageBox.Show(String.Format("Error al intentar abrir excel con Microsoft.Jet.OLEDB.4.0 " & ex1.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
                    'Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex1.Message))
                Finally
                    If lConnection2.State = ConnectionState.Open Then lConnection2.Close()
                End Try

            Finally
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
            End Try

            lblprg.Text = ""

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
                        CargaUnidadMedida(DT)
                    Case "Producto"
                        CargaProducto(DT)
                    Case "Tarima"
                        CargaTarima(DT)
                    Case "Inventario"
                        Cargar_Archivo_Excel = Carga_Inventario_Teorico(DT)
                    Case Else
                        Exit Select
                End Select

            Else
                lblprg.Text = "No hay registros para importar."
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

                ObjA.IdEmpresa = CInt(AP.IdEmpresa)
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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
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
                ObjD.Empresa.IdEmpresa = CInt(AP.IdEmpresa)
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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
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
                ObjO.IdEmpresa = CInt(AP.IdEmpresa)
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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
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

    Private Sub CargaUnidadMedida(ByVal pDT As DataTable)

        Dim pListObjUM As New List(Of clsBeUnidad_medida)

        Try

            Cursor = Cursors.WaitCursor

            For i As Integer = 0 To pDT.Rows.Count - 1

                Dim Indice As Integer = i

                Application.DoEvents()

                Dim ObjUM As New clsBeUnidad_medida() With {.Propietario = New clsBePropietarios}

                If pDT(i)(0) Is DBNull.Value AndAlso pDT(i)(0) Is Nothing Then
                    Throw New Exception("Falta código de la unidad de medida en la fila " & i + 1)
                End If
                If IsNumeric(pDT(i)(0)) = False Then
                    Throw New Exception("El código de la unidad de medida debe ser númerico entero. Fila " & i + 1)
                Else
                    Dim lIdExiste As Boolean = pListObjUM.Exists(Function(d) d.IdUnidadMedida = CInt(pDT(Indice)(0)))
                    If lIdExiste Then
                        Throw New Exception(String.Format("El código de la unidad de medida de la fila {0} se encuentra repetido.", i + 1))
                    Else
                        If Not clsLnUnidad_medida.Exists(pDT(i)(0)) Then
                            ObjUM.IsNew = True
                        Else
                            ObjUM.IsNew = False
                        End If
                        ObjUM.IdUnidadMedida = CInt(pDT(i)(0))
                    End If
                End If

                If pDT(i)(1) Is DBNull.Value AndAlso pDT(i)(1) Is Nothing Then
                    Throw New Exception("Falta nombre de la unidad de medida en la fila " & i + 1)
                End If

                ObjUM.Propietario.IdPropietario = CInt(pcmbP.SelectedValue)
                ObjUM.Nombre = CStr(pDT(i)(1))
                ObjUM.Activo = True
                ObjUM.User_agr = AP.UsuarioAp.IdUsuario
                ObjUM.Fec_agr = Now
                ObjUM.User_mod = AP.UsuarioAp.IdUsuario
                ObjUM.Fec_mod = Now
                pListObjUM.Add(ObjUM)

            Next

            If pListObjUM IsNot Nothing AndAlso pListObjUM.Count > 0 Then
                clsLnUnidad_medida.GuardarTransaccion(pListObjUM)
                XtraMessageBox.Show("Importación realizada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Listar.Invoke()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Dim Contador As Integer = 0
    Private Sub CargaProducto(ByRef pDT As DataTable)

        Dim pListObjP As New List(Of clsBeProducto)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lBodegas As New List(Of clsBeBodega)

        Dim ObjPBod As New clsBeProducto_bodega
        Dim Contador = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Cursor = Cursors.WaitCursor
            Contador = 0

            '#EJC20210901: llenar bodegas (activas) disponibles para asociar el producto_bodega en importación de excel. método tiene_errores_excel.
            lBodegas = clsLnBodega.GetAll(lConnection, lTransaction)

            For i As Integer = 0 To pDT.Rows.Count - 1

                Dim Indice As Integer = i
                Contador = Contador + 1

                Application.DoEvents()

                If Contador = 736 Then
                    Debug.Write("Espera")
                End If

                Dim ObjP As New clsBeProducto

                If pDT(i)(0) Is DBNull.Value AndAlso pDT(i)(0) Is Nothing Then
                    Throw New Exception("Falta ID del producto en la fila " & i + 1)
                End If
                If IsNumeric(pDT(i)(0)) = False Then
                    Throw New Exception("El ID del producto debe ser númerico entero. Fila " & i + 1)
                Else

                    Dim IdProducto As Integer = CInt(pDT(Indice)(0))

                    Dim lIdExiste As Boolean = pListObjP.Exists(Function(d) d.IdProducto = IdProducto)
                    If lIdExiste Then
                        Throw New Exception("El ID del prouducto de la fila " & i + 1 & " se encuentra repetido.")
                    Else
                        If Not clsLnProducto.Exists(CInt(pDT(i)(0)), lConnection, lTransaction) Then
                            ObjP.IsNew = True
                        Else
                            ObjP.IsNew = False
                        End If
                        ObjP.IdProducto = CInt(pDT(i)(0))
                    End If
                End If


                If pDT(i)(1) Is DBNull.Value AndAlso pDT(i)(1) Is Nothing Then
                    Throw New Exception("Falta ID del propietario en la fila " & i + 1)
                End If
                If IsNumeric(pDT(i)(1)) = False Then
                    Throw New Exception("El ID del propietario debe ser númerico entero. Fila " & i + 1)
                Else

                    Dim IdPropietario As Integer = CInt(pDT(Indice)(1))
                    If Not clsLnPropietarios.Exists(IdPropietario) Then

                        Throw New Exception("el ID del propietario no existe, revise en la fila " & i + 1)
                    Else
                        ObjP.IdPropietario = CInt(pDT(i)(1))
                    End If
                End If

                If pDT(i)(2) IsNot DBNull.Value AndAlso pDT(i)(2) IsNot Nothing Then
                    If IsNumeric(pDT(i)(2)) = False Then
                        Throw New Exception("La clasificación debe ser un valor númerico entero. Fila " & i + 1)
                    Else
                        If Not clsLnProducto_clasificacion.Exists(pDT(i)(2), lConnection, lTransaction) Then
                            Throw New Exception("La clasificación de la fila " & i + 1 & " no existe.")
                        Else
                            'GT 190720212033: aunque la llave es null para importar producto, no ignorar el valor del excel, por eso se valida en la linea 1067
                            ObjP.IdClasificacion = CInt(pDT(Indice)(2))
                        End If
                    End If
                End If


                If pDT(i)(3) IsNot DBNull.Value Then
                    ObjP.IdFamilia = CInt(pDT(i)(3))
                End If

                If pDT(i)(6) IsNot DBNull.Value AndAlso pDT(i)(6) IsNot Nothing Then
                    If IsNumeric(pDT(i)(6)) = False Then
                        Throw New Exception("La unidad de medida debe ser un valor númerico entero. Fila " & i + 1)
                    Else
                        If pDT(i)(6) <> 0 Then
                            If Not clsLnUnidad_medida.Exists(pDT(i)(6), lConnection, lTransaction) Then
                                Throw New Exception("La unidad de medida de la fila " & i + 1 & " no existe.")
                            Else
                                'GT 190720212033: aunque la llave es null para importar producto, no ignorar el valor del excel, por eso se valida en la linea 1080
                                ObjP.IdUnidadMedidaBasica = CInt(pDT(Indice)(6))
                            End If
                        End If
                    End If
                End If

                If pDT(i)(13) Is DBNull.Value AndAlso pDT(i)(13) Is Nothing Then
                    Throw New Exception("Falta el código del producto en la fila " & i + 1)
                Else
                    Dim tam_codigo = CStr(pDT(i)(13))
                    If tam_codigo.Length > 50 Then
                        Throw New Exception("El codigo excede el tamaño en la fila " & i + 1)
                    Else
                        ObjP.Codigo = CStr(pDT(i)(13))
                    End If
                End If


                If pDT(i)(14) Is DBNull.Value AndAlso pDT(i)(14) Is Nothing Then
                    Throw New Exception("Falta Nombre del producto en la fila " & i + 1)
                Else

                    Dim tam_nombre = CStr(pDT(i)(14))
                    If tam_nombre.Length > 100 Then
                        Throw New Exception("El Nombre excede el tamaño en la fila " & i + 1)
                    Else
                        ObjP.Nombre = CStr(pDT(i)(14))
                    End If
                End If

                If pDT(i)(15) Is DBNull.Value Then
                    Throw New Exception("El codigo barras de la fila " & i + 1 & " esta vacio.")
                Else

                    Dim tam_barra = CStr(pDT(i)(15))
                    If tam_barra.Length > 35 Then
                        Throw New Exception("La barra excede el tamaño en la fila " & i + 1)
                    Else
                        ObjP.Codigo_barra = CStr(pDT(i)(15))
                    End If
                End If


                If pDT(i)(16) IsNot DBNull.Value AndAlso pDT(i)(16) IsNot Nothing Then
                    If IsNumeric(pDT(i)(16)) = False Then
                        Throw New Exception("La existencia mínima debe ser un valor númerico. Fila " & i + 1)
                    Else
                        ObjP.Existencia_min = CDbl(pDT(i)(16))
                    End If
                End If


                If pDT(i)(17) IsNot DBNull.Value AndAlso pDT(i)(17) IsNot Nothing Then
                    If IsNumeric(pDT(i)(17)) = False Then
                        Throw New Exception("La existencia mínima debe ser un valor númerico. Fila " & i + 1)
                    Else
                        ObjP.Existencia_min = CDbl(pDT(i)(17))
                    End If
                End If

                If pDT(i)(18) IsNot DBNull.Value AndAlso pDT(i)(18) IsNot Nothing Then
                    If IsNumeric(pDT(i)(18)) = False Then
                        Throw New Exception("La existencia máxima debe ser un valor númerico. Fila " & i + 1)
                    Else
                        ObjP.Existencia_max = CDbl(pDT(i)(18))
                    End If
                End If

                If pDT(i)(19) IsNot DBNull.Value AndAlso pDT(i)(19) IsNot Nothing Then
                    If IsNumeric(pDT(i)(19)) = False Then
                        Throw New Exception("El costo debe ser un valor númerico. Fila " & i + 1)
                    Else
                        ObjP.Costo = CDbl(pDT(i)(19))
                    End If
                End If

                If pDT(i)(20) IsNot DBNull.Value AndAlso pDT(i)(20) IsNot Nothing Then
                    If IsNumeric(pDT(i)(20)) = False Then
                        Throw New Exception("El peso referencia debe ser un valor númerico. Fila " & i + 1)
                    Else
                        ObjP.Peso_referencia = CDbl(pDT(i)(20))
                    End If
                End If

                If pDT(i)(21) IsNot DBNull.Value AndAlso pDT(i)(21) IsNot Nothing Then
                    If IsNumeric(pDT(i)(21)) = False Then
                        Throw New Exception("El peso tolerancia debe ser un valor númerico. Fila " & i + 1)
                    Else
                        ObjP.Peso_tolerancia = CDbl(pDT(i)(21))
                    End If
                End If

                If pDT(i)(22) IsNot DBNull.Value AndAlso pDT(i)(22) IsNot Nothing Then
                    If IsNumeric(pDT(i)(22)) = False Then
                        Throw New Exception("La temperatura referencia debe ser un valor númerico. Fila " & i + 1)
                    Else
                        ObjP.Temperatura_referencia = CDbl(pDT(i)(22))
                    End If
                End If

                If pDT(i)(23) IsNot DBNull.Value AndAlso pDT(i)(23) IsNot Nothing Then
                    If IsNumeric(pDT(i)(23)) = False Then
                        Throw New Exception("La temperatura tolerancia debe ser un valor númerico. Fila " & i + 1)
                    Else
                        ObjP.Temperatura_tolerancia = CDbl(pDT(i)(23))
                    End If
                End If

                'GT 01092021: el valor Activo del producto es por defecto true, aca solo se deja de referencia.
                If pDT(i)(24) IsNot DBNull.Value AndAlso pDT(i)(24) IsNot Nothing Then

                End If


                If pDT(i)(25) IsNot DBNull.Value AndAlso pDT(i)(25) IsNot Nothing Then
                    ObjP.Serializado = CBool(pDT(i)(25))
                End If


                If pDT(i)(26) IsNot DBNull.Value AndAlso pDT(i)(26) IsNot Nothing Then
                    ObjP.Genera_lote = CBool(pDT(i)(26))
                End If

                If pDT(i)(27) IsNot DBNull.Value AndAlso pDT(i)(27) IsNot Nothing Then
                    ObjP.Genera_lp = CBool(pDT(i)(27))
                End If

                If pDT(i)(28) IsNot DBNull.Value AndAlso pDT(i)(28) IsNot Nothing Then
                    ObjP.Control_vencimiento = CBool(pDT(i)(28))
                End If

                If pDT(i)(29) IsNot DBNull.Value AndAlso pDT(i)(29) IsNot Nothing Then
                    ObjP.Control_lote = CBool(pDT(i)(29))
                End If

                If pDT(i)(30) IsNot DBNull.Value AndAlso pDT(i)(30) IsNot Nothing Then
                    ObjP.Peso_recepcion = CBool(pDT(i)(30))
                End If


                If pDT(i)(31) IsNot DBNull.Value AndAlso pDT(i)(31) IsNot Nothing Then
                    ObjP.Peso_despacho = CBool(pDT(i)(31))
                End If

                If pDT(i)(32) IsNot DBNull.Value AndAlso pDT(i)(32) IsNot Nothing Then
                    ObjP.Temperatura_recepcion = CBool(pDT(i)(32))
                End If

                If pDT(i)(33) IsNot DBNull.Value AndAlso pDT(i)(33) IsNot Nothing Then
                    ObjP.Temperatura_despacho = CBool(pDT(i)(33))
                End If

                If pDT(i)(34) IsNot DBNull.Value AndAlso pDT(i)(34) IsNot Nothing Then
                    ObjP.Materia_prima = CBool(pDT(i)(34))
                End If

                If pDT(i)(35) IsNot DBNull.Value AndAlso pDT(i)(35) IsNot Nothing Then
                    ObjP.Kit = CBool(pDT(i)(35))
                End If

                If pDT(i)(36) IsNot DBNull.Value AndAlso pDT(i)(36) IsNot Nothing Then
                    If IsNumeric(pDT(i)(36)) = False Then
                        Throw New Exception("La tolerancia debe ser un valor númerico entero. Fila " & i + 1)
                    Else
                        ObjP.Tolerancia = CInt(pDT(i)(36))
                    End If
                End If

                If pDT(i)(37) IsNot DBNull.Value AndAlso pDT(i)(37) IsNot Nothing Then
                    If IsNumeric(pDT(i)(37)) = False Then
                        Throw New Exception("El ciclo de vida debe ser un valor númerico entero. Fila " & i + 1)
                    Else
                        ObjP.Ciclo_vida = CInt(pDT(i)(37))
                    End If
                End If

                ObjP.Propietario = New clsBePropietarios
                ObjP.Clasificacion = New clsBeProducto_clasificacion
                ObjP.Familia = New clsBeProducto_familia
                ObjP.Marca = New clsBeProducto_marca
                ObjP.TipoProducto = New clsBeProducto_tipo
                ObjP.UnidadMedida = New clsBeUnidad_medida
                ObjP.Arancel = New clsBeArancel

                If pDT(i)(2) IsNot DBNull.Value Then
                    ObjP.Clasificacion.IdClasificacion = CInt(pDT(i)(2))
                End If

                If pDT(i)(3) IsNot DBNull.Value Then
                    ObjP.Familia.IdFamilia = CInt(pDT(i)(3))
                End If

                If pDT(i)(4) IsNot DBNull.Value Then
                    ObjP.Marca.IdMarca = CInt(pDT(i)(4))
                End If

                If pDT(i)(5) IsNot DBNull.Value Then
                    ObjP.TipoProducto.IdTipoProducto = CInt(pDT(i)(5))
                End If

                'ObjP.UnidadMedida.IdUnidadMedida = CInt(pDT(i)(5))
                ObjP.UnidadMedida.IdUnidadMedida = CInt(pDT(i)(6))

                If pDT(i)(7) IsNot DBNull.Value Then
                    ObjP.IdCamara = CInt(pDT(i)(7))
                End If

                If pDT(i)(8) IsNot DBNull.Value Then
                    ObjP.IdTipoRotacion = clsLnTipo_rotacion.GetIdTipoRotacionByNombre(pDT(i)(8).ToString, lConnection, lTransaction)
                End If


                If pDT(i)(9) IsNot DBNull.Value Then
                    ObjP.IdPerfilSerializado = clsLnPerfil_serializado.GetIdPerfilSerializadoByNombre(pDT(i)(9).ToString, lConnection, lTransaction)
                End If

                If pDT(i)(10) IsNot DBNull.Value Then
                    ObjP.IdIndiceRotacion = clsLnIndice_rotacion.GetIdIndiceRotacionByNombre(pDT(i)(10).ToString, lConnection, lTransaction)
                End If

                If pDT(i)(12) IsNot DBNull.Value Then
                    ObjP.Arancel.IdArancel = CInt(pDT(i)(12))
                End If

                ObjP.Activo = True
                ObjP.User_agr = AP.UsuarioAp.IdUsuario
                ObjP.Fec_agr = Now
                ObjP.User_mod = AP.UsuarioAp.IdUsuario
                ObjP.Fec_mod = Now
                pListObjP.Add(ObjP)

            Next


            If pListObjP IsNot Nothing AndAlso pListObjP.Count > 0 Then

                clsLnProducto.Guardar_Transaccion(pListObjP, lConnection, lTransaction)

                '#EJC20210901_B: Insertar producto bodega por cada bodega importación excel. (Nuevo producto)
                For Each pProducto In pListObjP

                    For Each B In lBodegas

                        ObjPBod = New clsBeProducto_bodega
                        ObjPBod.IdProductoBodega = clsLnProducto_bodega.MaxID(lConnection, lTransaction) + Contador
                        ObjPBod.IdProducto = pProducto.IdProducto
                        ObjPBod.IdBodega = B.IdBodega
                        ObjPBod.Activo = True
                        ObjPBod.Sistema = False
                        ObjPBod.User_agr = AP.UsuarioAp.IdUsuario
                        ObjPBod.Fec_agr = Now
                        ObjPBod.User_mod = AP.UsuarioAp.IdUsuario
                        ObjPBod.Fec_mod = Now
                        clsLnProducto_bodega.InsertarFromInterface(ObjPBod, lConnection, lTransaction)
                    Next

                Next

                XtraMessageBox.Show("Importación realizada correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Listar.Invoke()
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            XtraMessageBox.Show(ex.Message & Contador, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally

            Cursor = Cursors.Default
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

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

        Try

            Cursor = Cursors.WaitCursor

            Dim vContador As Integer = 1
            Dim vIndice As Integer = 0
            Dim vIndiceCodigoProducto As Integer = 1
            Dim vIndicadorFila As Integer = 1
            Dim Columna_leida As Integer = 1

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

                'EFREN_17052021 valida codigo del producto
                If pDT(i)(0) Is DBNull.Value Then
                    Throw New Exception("El Código: ( " & vCodigoProductoDebug & " ) producto en la fila " & i + 1 & " no es válido")
                Else

                    Dim Codigo_Producto As String = pDT(Indice)(0)

                    Dim lIdExiste As Boolean = pListObjP.Exists(Function(d) d.Codigo = Codigo_Producto)
                    If lIdExiste Then
                        Throw New Exception("El Código del producto en la fila " & i + 1 & " se encuentra repetido.")
                    Else
                        If Not clsLnProducto.Exist_by_Codigo(pDT(i)(0)) Then
                            Throw New Exception("El código del producto " & pDT(i)(0) & "en la fila " & i + 1 & " no existe en la bd.")
                        End If
                        vCodigoProducto = pDT(i)(0)
                    End If
                End If

                'GT no se valida Presentación, porque esta puede ir vacia, y se valida en el frmInventarioImport
                vNomPresentacion = IIf(pDT(i)(1) Is DBNull.Value, "", Convert.ToString(pDT(i)(1)))


                'GT 17052021 valida cantidad
                If pDT(i)(2) IsNot DBNull.Value AndAlso pDT(i)(2) IsNot Nothing Then
                    If IsNumeric(pDT(i)(2)) = False Then
                        Throw New Exception("La cantidad debe ser un valor númerico entero. Fila " & i + 1)
                    Else
                        If IsNumeric(pDT(i)(2)) = 0 Then
                            Throw New Exception("La cantidad de la fila " & i + 1 & " debe ser mayor a 0.")
                        Else
                            vCantidad = Math.Round(CDbl(pDT(i)(2)), 6)
                        End If
                    End If
                End If

                'GT 17052021 se valida que el peso sea al menos =0 o superior
                If pDT(i)(3) IsNot DBNull.Value AndAlso pDT(i)(3) IsNot Nothing Then
                    If IsNumeric(pDT(i)(3)) = False Then
                        Throw New Exception("El Peso debe ser un valor númerico ó por defecto 0. Fila " & i + 1)
                    Else
                        vPeso = IIf(pDT(i)(3) = 0, 0, CDbl(pDT(i)(3)))
                    End If
                Else
                    Throw New Exception("El Peso debe ser un valor númerico ó por defecto 0. Fila " & i + 1)
                End If

                'GT 17052021 se valida que exista al menos un string para la Unidad Medida
                If pDT(i)(4) Is DBNull.Value AndAlso pDT(i)(4) Is Nothing Then
                    Throw New Exception("Falta nombre de la unidad de medida (UM) para el producto. Fila " & i + 1)
                Else
                    vNomUM = pDT(i)(4)
                End If

                'EFREN 17052021 se asigna lote, este puede ir vacio.
                vLote = IIf(pDT(i)(5) Is DBNull.Value, "", Convert.ToString(pDT(i)(5)))

                sFechaVence = IIf(IsDBNull(pDT(i)(6)), New Date(1900, 1, 1), pDT(i)(6))

                'EFREN 17052021 valida que fecha vencimiento no este vacia, aunque el parametro como tal, se valida en frmInventarioImport
                If pDT(i)(6) Is DBNull.Value Then
                    Throw New Exception("La fecha vencimiento: " & sFechaVence & " de la fila " & i + 1 & " para el código de producto: " & vCodigoProductoDebug & " no es válida. Si desconoce la fecha, asigne por defecto: 01/01/1900 ")
                Else
                    Try
                        vFechaVence = Date.Parse(sFechaVence)
                    Catch ex As Exception
                        Throw New Exception("La fecha vencimiento: " & sFechaVence & " de la fila " & i + 1 & " para el código de producto: " & vCodigoProductoDebug & " no es válida. Si desconoce la fecha, asigne por defecto: 01/01/1900 ")
                    End Try

                End If

                'GT 10112021 valida que la ubicación sea númerica
                If pDT(i)(7) IsNot DBNull.Value AndAlso pDT(i)(7) IsNot Nothing Then
                    If IsNumeric(pDT(i)(7)) = False Then
                        Throw New Exception("La ubicación debe ser un valor númerico. Fila " & i + 1)
                    Else
                        vUbicacion = CInt(pDT(i)(7))
                    End If
                Else
                    '    Throw New Exception("La fila " & i + 1 & " no tiene una ubicación.")
                    vUbicacion = 0
                End If

                'GT0112021: Se obtiene valor para LP y de cod_variante 
                vLicensePlate = IIf(pDT(i)(8) Is DBNull.Value, "", Convert.ToString(pDT(i)(8)))

                vCod_Variante = IIf(pDT(i)(9) Is DBNull.Value, "", Convert.ToString(pDT(i)(9)))

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
                                           vCod_Variante)

                Application.DoEvents()

            Next


            If lInvenarioTeorico.Rows.Count > 0 Then

                Carga_Inventario_Teorico = True

                SplashScreenManager.CloseForm(False)

                XtraMessageBox.Show("Se finalizó la lectura del archivo, se validarán los datos a continuación.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                If Not Listar Is Nothing Then
                    Listar.Invoke()
                End If

                DialogResult = DialogResult.OK

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

            If Validar_Seleccion_Archivo() Then

                If chkBorraTabla.Checked Then
                    If MessageBox.Show("¿Desea borrar la tabla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Borrar_Tabla()
                    End If
                End If

                If Cargar_Archivo_Excel() Then

                    'EFREN_21072021: Se limpian los inputs para evitar que se presione cargar nuevamente e intente sobreescribir
                    txtArchivo.Text = ""
                    pListObj.Clear()
                    DsExcel.Clear()
                    Grid.BeginUpdate()
                    Grid.EndUpdate()
                    Grid.ForceInitialize()

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            cmdCargar.Enabled = True
        End Try

    End Sub

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
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                Case Else
                    Exit Select
            End Select

            Borrar_Tabla = True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
            'XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
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
            Else
                grpInsertaInv.Dispose()
                lblTipo.Text = "Producto:"
                CreaCombobox()
                SetComboProducto()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub

End Class
