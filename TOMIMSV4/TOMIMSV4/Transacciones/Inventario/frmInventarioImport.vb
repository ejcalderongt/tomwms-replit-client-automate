Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Text
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmInventarioImport

    Public IdInventario As Integer
    Private dtc, dtp, dtu, dtall As New DataTable
    Private dr() As DataRow

    Public InsertaInv As Boolean = False
    Public IdOperador As Integer = 0
    Public NomOperador As String = ""
    Public DobleVerificacion As Boolean

    Private errc As Integer

    'EFREN07052021 valida si se cargan los productos x propietario especifico o carga todos.
    Public InvTeorico_Multi_Propietario As Boolean
    Public vBodega As Integer = 0
    'EFREN10052021 el propietario bodega se obtiene desde frminventario, si es multipropietario se deja con valor 0
    Public IdPropietarioBodega As Integer = 0
    Public TipoInventario As String = ""
    ''' <summary>
    ''' #EJC20240723: Con cariño para Carolina, por su paciencia en explicarme el requerimiento de lo que habíamos perdido, pero no notado.
    ''' </summary>
    ''' <returns></returns>
    Public Property TipoTeoricoImportacion As pTipoImportacion = pTipoImportacion.WMS

    Public Enum pTipoImportacion
        WMS = 0
        ERP = 1
    End Enum

    Private Const TAG_INV_IMPORT_TRACE As String = "#EJC20260522_INV_IMPORT_TRACE"
    Private mInvImportTraceSesion As String = ""
    Private mInvImportTraceTotal As Stopwatch
    Private mInvImportTracePaso As Stopwatch

    Private Function InvImportTrace_Activo() As Boolean
        Try
            Dim vValor As String = If(Environment.GetEnvironmentVariable("TOMWMS_INV_IMPORT_TRACE"), "").ToUpperInvariant()
            Return Not (vValor = "0" OrElse vValor = "NO" OrElse vValor = "FALSE")
        Catch
            Return True
        End Try
    End Function

    Private Function InvImportTrace_Path() As String
        Return System.IO.Path.Combine(System.IO.Path.GetTempPath(), "TOMWMS", "inventario-import-trace.log")
    End Function

    Private Function InvImportTrace_Limpiar(ByVal pTexto As String) As String
        If pTexto Is Nothing Then Return ""
        Return pTexto.Replace(vbCr, " ").Replace(vbLf, " ").Replace("|", "/")
    End Function

    Private Sub InvImportTrace_Iniciar(ByVal pPaso As String, Optional ByVal pExtra As String = "")
        If Not InvImportTrace_Activo() Then Return
        mInvImportTraceSesion = Date.Now.ToString("yyyyMMddHHmmssfff") & "-" & Guid.NewGuid().ToString("N").Substring(0, 8)
        mInvImportTraceTotal = Stopwatch.StartNew()
        mInvImportTracePaso = Stopwatch.StartNew()
        InvImportTrace_Escribir(pPaso, pExtra)
    End Sub

    Private Sub InvImportTrace_Marca(ByVal pPaso As String, Optional ByVal pExtra As String = "")
        If Not InvImportTrace_Activo() Then Return
        If mInvImportTraceTotal Is Nothing Then
            InvImportTrace_Iniciar(pPaso, pExtra)
        Else
            InvImportTrace_Escribir(pPaso, pExtra)
        End If
    End Sub

    Private Sub InvImportTrace_Escribir(ByVal pPaso As String, Optional ByVal pExtra As String = "")
        Try
            Dim vPath As String = InvImportTrace_Path()
            Dim vDir As String = System.IO.Path.GetDirectoryName(vPath)
            If Not System.IO.Directory.Exists(vDir) Then System.IO.Directory.CreateDirectory(vDir)

            If System.IO.File.Exists(vPath) AndAlso New System.IO.FileInfo(vPath).Length > 5242880 Then
                System.IO.File.Delete(vPath)
            End If

            Dim vTotalMs As Long = If(mInvImportTraceTotal Is Nothing, 0, mInvImportTraceTotal.ElapsedMilliseconds)
            Dim vDeltaMs As Long = If(mInvImportTracePaso Is Nothing, 0, mInvImportTracePaso.ElapsedMilliseconds)
            If mInvImportTracePaso IsNot Nothing Then mInvImportTracePaso.Restart()

            Dim vLinea As String = String.Join("|", New String() {
                Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                TAG_INV_IMPORT_TRACE,
                "frmInventarioImport",
                InvImportTrace_Limpiar(mInvImportTraceSesion),
                InvImportTrace_Limpiar(pPaso),
                "IdInventario=" & IdInventario,
                "IdBodega=" & AP.IdBodega,
                "RowsGrid=" & If(grdData Is Nothing, 0, grdData.Rows.Count),
                "TotalMs=" & vTotalMs,
                "DeltaMs=" & vDeltaMs,
                InvImportTrace_Limpiar(pExtra)
            })

            System.IO.File.AppendAllText(vPath, vLinea & Environment.NewLine, System.Text.Encoding.UTF8)
        Catch
        End Try
    End Sub

    '#EJC20260522_INV_IMPORT_VALIDACION_CACHE: soporte liviano para evitar consultas por fila durante Validar_Datos.
    Private Function InvImportValorCelda(ByVal pFila As Integer, ByVal pColumna As String) As String
        Try
            Dim vValor As Object = grdData.Rows(pFila).Cells(pColumna).Value
            If vValor Is Nothing OrElse IsDBNull(vValor) Then Return ""
            Return vValor.ToString().Trim()
        Catch
            Return ""
        End Try
    End Function

    Private Function InvImportEnteroCelda(ByVal pFila As Integer, ByVal pColumna As String) As Integer
        Try
            Dim vValor As Object = grdData.Rows(pFila).Cells(pColumna).Value
            If vValor Is Nothing OrElse IsDBNull(vValor) OrElse vValor.ToString().Trim() = "" Then Return 0
            Return CInt(vValor)
        Catch
            Return 0
        End Try
    End Function

    Private Sub InvImportAgregarClave(Of T)(ByRef pDiccionario As Dictionary(Of String, T),
                                           ByVal pClave As String,
                                           ByVal pValor As T)
        If pClave Is Nothing Then Return
        pClave = pClave.Trim()
        If pClave = "" Then Return
        If Not pDiccionario.ContainsKey(pClave) Then pDiccionario.Add(pClave, pValor)
    End Sub

    Private Function InvImportXmlValores(ByVal pValores As IEnumerable(Of String)) As String
        Dim vXml As New StringBuilder()
        vXml.Append("<root>")
        For Each vValor As String In pValores
            If vValor IsNot Nothing AndAlso vValor.Trim() <> "" Then
                vXml.Append("<i v=""")
                vXml.Append(System.Security.SecurityElement.Escape(vValor.Trim()))
                vXml.Append(""" />")
            End If
        Next
        vXml.Append("</root>")
        Return vXml.ToString()
    End Function

    Private Function InvImportInt(ByVal pRow As DataRow, ByVal pColumn As String) As Integer
        If Not pRow.Table.Columns.Contains(pColumn) OrElse IsDBNull(pRow.Item(pColumn)) Then Return 0
        Return CInt(pRow.Item(pColumn))
    End Function

    Private Function InvImportDbl(ByVal pRow As DataRow, ByVal pColumn As String) As Double
        If Not pRow.Table.Columns.Contains(pColumn) OrElse IsDBNull(pRow.Item(pColumn)) Then Return 0.0
        Return CDbl(pRow.Item(pColumn))
    End Function

    Private Function InvImportStr(ByVal pRow As DataRow, ByVal pColumn As String) As String
        If Not pRow.Table.Columns.Contains(pColumn) OrElse IsDBNull(pRow.Item(pColumn)) Then Return ""
        Return pRow.Item(pColumn).ToString()
    End Function

    Private Function InvImportBool(ByVal pRow As DataRow, ByVal pColumn As String) As Boolean
        If Not pRow.Table.Columns.Contains(pColumn) OrElse IsDBNull(pRow.Item(pColumn)) Then Return False
        Return CBool(pRow.Item(pColumn))
    End Function

    Private Function InvImportProductoDesdeRow(ByVal pRow As DataRow) As clsBeProducto
        Dim vProducto As New clsBeProducto()

        vProducto.IdProducto = InvImportInt(pRow, "IdProducto")
        vProducto.IdPropietario = InvImportInt(pRow, "IdPropietario")
        vProducto.Propietario = New clsBePropietarios()
        vProducto.Propietario.IdPropietario = vProducto.IdPropietario
        vProducto.Propietario.Nombre_comercial = InvImportStr(pRow, "PropietarioNombre")
        vProducto.IdUnidadMedidaBasica = InvImportInt(pRow, "IdUnidadMedidaBasica")
        vProducto.UnidadMedida = New clsBeUnidad_medida()
        vProducto.UnidadMedida.IdUnidadMedida = vProducto.IdUnidadMedidaBasica
        vProducto.UnidadMedida.Nombre = InvImportStr(pRow, "UnidadMedidaNombre")
        vProducto.IdProductoParametroA = InvImportInt(pRow, "IdProductoParametroA")
        If vProducto.IdProductoParametroA <> 0 Then
            vProducto.ParametroA = New clsBeProducto_parametro_a()
            vProducto.ParametroA.IdProductoParametroA = vProducto.IdProductoParametroA
            vProducto.ParametroA.Nombre = InvImportStr(pRow, "ParametroANombre")
        End If
        vProducto.IdProductoParametroB = InvImportInt(pRow, "IdProductoParametroB")
        If vProducto.IdProductoParametroB <> 0 Then
            vProducto.ParametroB = New clsBeProducto_parametro_b()
            vProducto.ParametroB.IdProductoParametroB = vProducto.IdProductoParametroB
            vProducto.ParametroB.Nombre = InvImportStr(pRow, "ParametroBNombre")
        End If
        vProducto.Codigo = InvImportStr(pRow, "codigo")
        vProducto.Codigo_barra = InvImportStr(pRow, "codigo_barra")
        vProducto.Nombre = InvImportStr(pRow, "nombre")
        vProducto.Control_lote = InvImportBool(pRow, "control_lote")
        vProducto.Control_vencimiento = InvImportBool(pRow, "control_vencimiento")
        vProducto.Costo = InvImportDbl(pRow, "costo")
        vProducto.Precio = InvImportDbl(pRow, "precio")
        vProducto.IsNew = False

        Return vProducto
    End Function

    Private Sub InvImportCrearDiccionariosCatalogo(ByRef pProductoIdPorCodigo As Dictionary(Of String, Integer),
                                                   ByRef pPresentacionPorClave As Dictionary(Of String, Integer),
                                                   ByRef pUnidadPorNombre As Dictionary(Of String, Integer))
        pProductoIdPorCodigo = New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        pPresentacionPorClave = New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        pUnidadPorNombre = New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

        Dim vTablaProductos As DataTable = If(InvTeorico_Multi_Propietario, dtall, dtc)
        If vTablaProductos IsNot Nothing Then
            For Each vRow As DataRow In vTablaProductos.Rows
                Dim vCodigo As String = InvImportStr(vRow, "Codigo")
                If vCodigo <> "" AndAlso Not pProductoIdPorCodigo.ContainsKey(vCodigo) Then
                    pProductoIdPorCodigo.Add(vCodigo, InvImportInt(vRow, "IdProducto"))
                End If
            Next
        End If

        If dtp IsNot Nothing Then
            For Each vRow As DataRow In dtp.Rows
                Dim vClave As String = InvImportInt(vRow, "IdProducto") & "|" & InvImportStr(vRow, "Nombre")
                If Not pPresentacionPorClave.ContainsKey(vClave) Then
                    pPresentacionPorClave.Add(vClave, InvImportInt(vRow, "IdPresentacion"))
                End If
            Next
        End If

        If dtu IsNot Nothing Then
            For Each vRow As DataRow In dtu.Rows
                Dim vNombre As String = InvImportStr(vRow, "Nombre")
                If vNombre <> "" AndAlso Not pUnidadPorNombre.ContainsKey(vNombre) Then
                    pUnidadPorNombre.Add(vNombre, InvImportInt(vRow, "IdUnidadMedida"))
                End If
            Next
        End If
    End Sub

    Private Function InvImportCargarValidacionReadModel(ByVal pConnection As SqlConnection,
                                                        ByVal pTransaction As SqlTransaction,
                                                        ByRef pProductoPorCodigo As Dictionary(Of String, clsBeProducto),
                                                        ByRef pColorPorCodigo As Dictionary(Of String, clsBeColor),
                                                        ByRef pTallaPorCodigo As Dictionary(Of String, clsBeTalla),
                                                        ByRef pUbicacionesValidas As HashSet(Of Integer)) As Boolean
        pProductoPorCodigo = New Dictionary(Of String, clsBeProducto)(StringComparer.OrdinalIgnoreCase)
        pColorPorCodigo = New Dictionary(Of String, clsBeColor)(StringComparer.OrdinalIgnoreCase)
        pTallaPorCodigo = New Dictionary(Of String, clsBeTalla)(StringComparer.OrdinalIgnoreCase)
        pUbicacionesValidas = New HashSet(Of Integer)()

        Dim vCodigos As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim vTallas As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim vColores As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim vUbicaciones As New HashSet(Of Integer)()

        For ii As Integer = 0 To grdData.Rows.Count - 1
            Dim vCodigo As String = InvImportValorCelda(ii, "ColCodigo")
            If vCodigo <> "" Then vCodigos.Add(vCodigo)
            Dim vTalla As String = InvImportValorCelda(ii, "ColTalla")
            If vTalla <> "" Then vTallas.Add(vTalla)
            Dim vColor As String = InvImportValorCelda(ii, "ColColor")
            If vColor <> "" Then vColores.Add(vColor)
            Dim vUbicacion As Integer = InvImportEnteroCelda(ii, "ColUbicacion")
            If vUbicacion > 0 Then vUbicaciones.Add(vUbicacion)
        Next

        If vCodigos.Count = 0 Then Return True

        Dim vDS As New DataSet()
        Using vCmd As New SqlCommand("dbo.usp_wms_inventario_import_preload_readmodel_v1", pConnection, pTransaction)
            vCmd.CommandType = CommandType.StoredProcedure
            vCmd.CommandTimeout = 0
            vCmd.Parameters.Add("@CodigosXml", SqlDbType.Xml).Value = InvImportXmlValores(vCodigos)
            vCmd.Parameters.Add("@TallasXml", SqlDbType.Xml).Value = If(vTallas.Count > 0, CType(InvImportXmlValores(vTallas), Object), DBNull.Value)
            vCmd.Parameters.Add("@ColoresXml", SqlDbType.Xml).Value = If(vColores.Count > 0, CType(InvImportXmlValores(vColores), Object), DBNull.Value)
            vCmd.Parameters.Add("@IdBodega", SqlDbType.Int).Value = AP.IdBodega
            vCmd.Parameters.Add("@IdPropietarioBodega", SqlDbType.Int).Value = IdPropietarioBodega

            Using vDTA As New SqlDataAdapter(vCmd)
                vDTA.Fill(vDS)
            End Using
        End Using

        If vDS.Tables.Count > 0 Then
            For Each vRow As DataRow In vDS.Tables(0).Rows
                Dim vProducto As clsBeProducto = InvImportProductoDesdeRow(vRow)
                InvImportAgregarClave(pProductoPorCodigo, vProducto.Codigo, vProducto)
                InvImportAgregarClave(pProductoPorCodigo, vProducto.Codigo_barra, vProducto)
            Next
        End If

        If vDS.Tables.Count > 1 Then
            For Each vRow As DataRow In vDS.Tables(1).Rows
                Dim vTalla As New clsBeTalla()
                vTalla.IdTalla = InvImportInt(vRow, "IdTalla")
                vTalla.Codigo = InvImportStr(vRow, "Codigo")
                vTalla.Nombre = InvImportStr(vRow, "Nombre")
                vTalla.IsNew = False
                InvImportAgregarClave(pTallaPorCodigo, vTalla.Codigo, vTalla)
            Next
        End If

        If vDS.Tables.Count > 2 Then
            For Each vRow As DataRow In vDS.Tables(2).Rows
                Dim vColor As New clsBeColor()
                vColor.IdColor = InvImportInt(vRow, "IdColor")
                vColor.Codigo = InvImportStr(vRow, "Codigo")
                vColor.Nombre = InvImportStr(vRow, "Nombre")
                vColor.IsNew = False
                InvImportAgregarClave(pColorPorCodigo, vColor.Codigo, vColor)
            Next
        End If

        If vUbicaciones.Count > 0 Then
            Dim vSql As String = "SELECT IdUbicacion FROM dbo.bodega_ubicacion WHERE IdBodega=@IdBodega AND IdUbicacion IN (" &
                                 String.Join(",", vUbicaciones) & ")"
            Using vCmd As New SqlCommand(vSql, pConnection, pTransaction)
                vCmd.CommandType = CommandType.Text
                vCmd.Parameters.Add("@IdBodega", SqlDbType.Int).Value = AP.IdBodega
                Using vReader As SqlDataReader = vCmd.ExecuteReader()
                    While vReader.Read()
                        pUbicacionesValidas.Add(CInt(vReader.Item("IdUbicacion")))
                    End While
                End Using
            End Using
        End If

        InvImportTrace_Marca("VALIDAR_DATOS_READMODEL_OK",
                             "Codigos=" & vCodigos.Count &
                             ";Productos=" & pProductoPorCodigo.Count &
                             ";Tallas=" & pTallaPorCodigo.Count &
                             ";Colores=" & pColorPorCodigo.Count &
                             ";Ubicaciones=" & pUbicacionesValidas.Count)
        Return True
    End Function

#Region " Metodos principales "

    Private Sub Paste()

        Dim vArr() As String
        Dim vRow() As String
        Dim vIdxFilaExcel, vIdxColExcel, vIdxColGrid, vR, vRL, vRows As Integer

        lblPrg.Text = ""

        grdData.SuspendLayout()
        grdData.Rows.Clear()

        Try

            vArr = Clipboard.GetText().Split(Environment.NewLine)

            vR = grdData.Rows.Count

            vRows = vArr.Length
            vRow = vArr(0).Split(vbTab)
            grdData.Rows.Add(vRows)

            prg.Maximum = vArr.Length - 1
            prg.Visible = True

            lblPrg.Text = "Pegando datos desde portapapeles"
            lblPrg.Refresh()
            lblPrg.Visible = True

            For vIdxFilaExcel = 0 To vArr.Length - 1

                If vArr(vIdxFilaExcel) <> "" Then

                    lblPrg.Text = "Procesando fila: " & vIdxFilaExcel & " de: " & vArr.Length - 1
                    lblPrg.Refresh()

                    grdData.Item(0, vR).Value = "ERR"

                    Try

                        vRow = vArr(vIdxFilaExcel).Split(vbTab)
                        vIdxColGrid = 1 'Iniciar en columna Estado. 
                        vRL = vRow.Length  'If vRL > 5 Then vRL = 5

                        '#EJC20180528: Utilizar nombres de columnas no índices!!!
                        If vRow(vIdxColExcel).TrimStart <> "" Then
                            grdData.Item("ColId", vR).Value = vIdxFilaExcel + 1
                            grdData.Item("ColEstado", vR).Value = "PROCESANDO"
                            If vRow.Length > 1 Then grdData.Item("ColCodigo", vR).Value = vRow(0).TrimStart
                            If vRow.Length > 2 Then grdData.Item("ColPresentacion", vR).Value = vRow(1).TrimStart
                            If vRow.Length > 3 Then grdData.Item("ColCantidad", vR).Value = vRow(2).TrimStart
                            If vRow.Length > 4 Then grdData.Item("ColPeso", vR).Value = vRow(3).TrimStart
                            If vRow.Length > 5 Then grdData.Item("ColUnidadMedida", vR).Value = vRow(4).TrimStart
                            If vRow.Length > 6 Then grdData.Item("ColLote", vR).Value = vRow(5).TrimStart
                            If vRow.Length > 7 Then grdData.Item("colFechaVence", vR).Value = vRow(6).TrimStart
                            If vRow.Length >= 8 Then grdData.Item("ColUbicacion", vR).Value = vRow(7).TrimStart
                            vR = vR + 1
                        End If

                    Catch ex As Exception
                        grdData.Item(0, vR).Value = ex.Message
                    End Try

                End If

                prg.Value = vIdxFilaExcel

                Application.DoEvents()

            Next

            grdData.Rows.RemoveAt(grdData.Rows.Count - 1)

            prg.Value = 0
            prg.Visible = False

            lblRegs.Caption = "Registros: " & grdData.Rows.Count()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

        grdData.ResumeLayout()

    End Sub

    Private Sub Validar_Datos()

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim rc, ii, cc, vprod, vpres, vuni, vIdPropietarioBodega As Integer
        Dim cod, pres, UM, vPeso, vLote, vEstado As String
        Dim vCantidad As String = ""
        Dim Cantidad, Peso As Double
        Dim cantval, pesoval, costo, precio As Boolean
        Dim vFecha_Vence As DateTime = Now
        Dim vIdUnidadMedida As Integer = 0
        Dim BeProducto As New clsBeProducto
        Dim vUbicacion As Integer
        'GT01122021: campos para LP y cod_variante
        Dim vLicensePlate As String = ""
        Dim vCodVariante As String = ""
        Dim vErrorDescription As String = ""
        '#GT24112022_0800: campos DyD
        Dim vCosto As Double
        Dim vPrecio As Double
        Dim vParametro_a As String = ""
        Dim vParametro_b As String = ""
        Dim vColor As String = ""
        Dim vTalla As String = ""

        Dim correlativo_a As Integer
        Dim correlativo_b As Integer
        Dim vTraceReloj As System.Diagnostics.Stopwatch
        Dim vTraceMsCatalogos As Long = 0
        Dim vTraceMsProducto As Long = 0
        Dim vTraceMsUnidadDefault As Long = 0
        Dim vTraceMsUbicacion As Long = 0
        Dim vTraceMsParametroA As Long = 0
        Dim vTraceMsParametroB As Long = 0
        Dim vTraceMsProductoUpdate As Long = 0
        Dim vTraceMsColor As Long = 0
        Dim vTraceMsTalla As Long = 0
        Dim vTraceMsReadModel As Long = 0
        Dim vTraceMsUi As Long = 0
        Dim vProductoPorCodigo As New Dictionary(Of String, clsBeProducto)(StringComparer.OrdinalIgnoreCase)
        Dim vColorPorCodigo As New Dictionary(Of String, clsBeColor)(StringComparer.OrdinalIgnoreCase)
        Dim vTallaPorCodigo As New Dictionary(Of String, clsBeTalla)(StringComparer.OrdinalIgnoreCase)
        Dim vUbicacionesValidas As New HashSet(Of Integer)()
        Dim vProductoIdPorCodigo As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        Dim vPresentacionPorClave As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        Dim vUnidadPorNombre As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        Dim vReadModelDisponible As Boolean = False

        rc = grdData.Rows.Count  'If rc > 3 Then rc = 3

        lblPrg.Text = ""
        grdData.EndEdit()

        InvImportTrace_Marca("VALIDAR_DATOS_START", "Rows=" & rc & ";ControlTallaColor=" & AP.Bodega.Control_Talla_Color)
        vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
        Llena_Catalogos()
        vTraceMsCatalogos = vTraceReloj.ElapsedMilliseconds
        InvImportTrace_Marca("VALIDAR_DATOS_CATALOGOS",
                             "MsCatalogos=" & vTraceMsCatalogos &
                             ";dtc=" & If(dtc Is Nothing, 0, dtc.Rows.Count) &
                             ";dtall=" & If(dtall Is Nothing, 0, dtall.Rows.Count) &
                             ";dtp=" & If(dtp Is Nothing, 0, dtp.Rows.Count) &
                             ";dtu=" & If(dtu Is Nothing, 0, dtu.Rows.Count))

        errc = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            InvImportTrace_Marca("VALIDAR_DATOS_TX_OPEN")

            InvImportCrearDiccionariosCatalogo(vProductoIdPorCodigo,
                                               vPresentacionPorClave,
                                               vUnidadPorNombre)

            Try
                vTraceReloj = Stopwatch.StartNew()
                vReadModelDisponible = InvImportCargarValidacionReadModel(lConnection,
                                                                          lTransaction,
                                                                          vProductoPorCodigo,
                                                                          vColorPorCodigo,
                                                                          vTallaPorCodigo,
                                                                          vUbicacionesValidas)
                vTraceMsReadModel = vTraceReloj.ElapsedMilliseconds
            Catch exReadModel As Exception
                vReadModelDisponible = False
                InvImportTrace_Marca("VALIDAR_DATOS_READMODEL_FALLBACK", exReadModel.Message)
            End Try

            prg.Maximum = rc
            prg.Visible = True

            lblPrg.Text = "Validando datos..."
            lblPrg.Refresh()

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Procesando archivo...")

            For ii = 0 To rc - 1

                correlativo_a = 0
                correlativo_b = 0

                If ii = 0 OrElse (ii + 1) Mod 25 = 0 OrElse ii = rc - 1 Then
                    vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
                    SplashScreenManager.Default.SetWaitFormDescription("Validando fila: " & ii + 1 & " de: " & rc - 1)
                    lblPrg.Text = "Validando fila: " & ii + 1 & " de: " & rc - 1
                    lblPrg.Refresh()
                    vTraceMsUi += vTraceReloj.ElapsedMilliseconds
                End If

                If ii = 118 Then
                    '
                End If

                Debug.WriteLine("procesando: " & ii)

                If ii = 0 OrElse (ii + 1) Mod 25 = 0 OrElse ii = rc - 1 Then
                    vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
                    prg.Value = ii : prg.Refresh() : Application.DoEvents()
                    vTraceMsUi += vTraceReloj.ElapsedMilliseconds
                Else
                    prg.Value = ii
                End If

                For cc = 0 To grdData.ColumnCount - 4
                    grdData.Rows(ii).Cells(cc).Style.BackColor = Color.White
                Next

                vprod = 0 : vpres = 0 : vuni = 0 : cantval = False : pesoval = False : costo = False : precio = False

                vEstado = IIf(IsDBNull(grdData.Rows(ii).Cells("ColEstado").Value), "", grdData.Rows(ii).Cells("ColEstado").Value)
                cod = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCodigo").Value), "", grdData.Rows(ii).Cells("ColCodigo").Value)
                pres = IIf(IsDBNull(grdData.Rows(ii).Cells("ColPresentacion").Value), "", grdData.Rows(ii).Cells("ColPresentacion").Value)

                vLicensePlate = IIf(IsDBNull(grdData.Rows(ii).Cells("ColLp").Value), "", grdData.Rows(ii).Cells("ColLp").Value)
                vCodVariante = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCodVariante").Value), "", grdData.Rows(ii).Cells("ColCodVariante").Value)

                '#EJC20260522_INV_IMPORT_PRODUCTO_LITE: evita carga completa y Obtener(...) anidados por fila.
                vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
                If vReadModelDisponible AndAlso vProductoPorCodigo.ContainsKey(cod) Then
                    BeProducto = vProductoPorCodigo(cod)
                Else
                    BeProducto = clsLnProducto.Get_Single_By_Codigo_For_InventarioImport(cod,
                                                                                        lConnection,
                                                                                        lTransaction)
                End If
                vTraceMsProducto += vTraceReloj.ElapsedMilliseconds

                If Not BeProducto Is Nothing Then

                    'EFREN10052021 Se obtiene el nombre comercial y el id propietario
                    'Dim BePropietario As clsBePropietarios = clsLnPropietarios.GetSingle(BeProducto.Propietario.IdPropietario)
                    vIdPropietarioBodega = cmbPropietario.EditValue

                    If vIdPropietarioBodega <> 0 Then

                        'grdData.Rows(ii).Cells("colIdPropietarioBodega").Value = vIdPropietarioBodega
                        'grdData.Rows(ii).Cells("colnombre_propietario").Value = BeProducto.Propietario.Nombre_comercial

                    Else
                        Marcar_Error(ii, "colIdPropietarioBodega", "No se encontró bodega del propietario")
                        Marcar_Error(ii, "colnombre_propietario", "No se encontró nombre del propietario")
                    End If

                    Try
                        vCantidad = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCantidad").Value), "0", grdData.Rows(ii).Cells("ColCantidad").Value)
                    Catch ex As Exception
                        Marcar_Error(ii, "ColCantidad", ex.Message)
                    End Try

                    vPeso = IIf(IsDBNull(grdData.Rows(ii).Cells("ColPeso").Value), "0", grdData.Rows(ii).Cells("ColPeso").Value)
                    UM = IIf(IsDBNull(grdData.Rows(ii).Cells("ColUnidadMedida").Value), "", grdData.Rows(ii).Cells("ColUnidadMedida").Value)
                    vUbicacion = IIf(IsDBNull(grdData.Rows(ii).Cells("ColUbicacion").Value), 0, grdData.Rows(ii).Cells("ColUbicacion").Value)
                    '#GT24112022_0900: campos DyD
                    vCosto = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCosto").Value), 0, grdData.Rows(ii).Cells("ColCosto").Value)
                    vPrecio = IIf(IsDBNull(grdData.Rows(ii).Cells("ColPrecio").Value), 0, grdData.Rows(ii).Cells("ColPrecio").Value)
                    vParametro_a = IIf(IsDBNull(grdData.Rows(ii).Cells("ColParametro_a").Value), "", grdData.Rows(ii).Cells("ColParametro_a").Value)
                    vParametro_b = IIf(IsDBNull(grdData.Rows(ii).Cells("ColParametro_b").Value), "", grdData.Rows(ii).Cells("ColParametro_b").Value)
                    vTalla = IIf(IsDBNull(grdData.Rows(ii).Cells("ColTalla").Value), "", grdData.Rows(ii).Cells("ColTalla").Value)
                    vColor = IIf(IsDBNull(grdData.Rows(ii).Cells("ColColor").Value), "", grdData.Rows(ii).Cells("ColColor").Value)

                    If BeProducto.Control_lote Then
                        vLote = IIf(IsDBNull(grdData.Rows(ii).Cells("ColLote").Value), "", grdData.Rows(ii).Cells("ColLote").Value)
                    Else
                        vLote = ""
                    End If

                    grdData.Rows(ii).Cells("ColLote").Value = vLote

                    If BeProducto.Control_vencimiento Then

                        Try
                            vFecha_Vence = IIf(IsDBNull(grdData.Rows(ii).Cells("colFechaVence").Value), "01/01/1900", grdData.Rows(ii).Cells("colFechaVence").Value)
                        Catch ex As Exception
                            Marcar_Error(ii, "ColFechaVence", ex.Message)
                        End Try

                    Else
                        vFecha_Vence = New Date(1900, 1, 1)
                    End If

                    grdData.Rows(ii).Cells("colFechaVence").Value = vFecha_Vence

                    If cod <> "" Then

                        grdData.Rows(ii).Cells("ColEstado").Value = "VALIDADO"

                        If pres <> "" Then vpres = True
                        If vCantidad <> "" Then cantval = True
                        If vPeso <> "" Then pesoval = True
                        If vCosto <> 0 Then costo = True
                        If vPrecio <> 0 Then precio = True

                        If (vCantidad = "") Then vCantidad = "0" : If (vPeso = "") Then vPeso = "0"

                        'EFREN07052021 si es multi propietario, el producto se busca en toda la lista
                        If vProductoIdPorCodigo.ContainsKey(cod) Then
                            vprod = vProductoIdPorCodigo(cod)
                            grdData.Rows(ii).Cells("colIdProducto").Value = vprod
                        ElseIf Not BeProducto Is Nothing AndAlso BeProducto.IdProducto <> 0 Then
                            vprod = BeProducto.IdProducto
                            grdData.Rows(ii).Cells("colIdProducto").Value = vprod
                        Else
                            MsgBox("El código de producto: " & cod & " No existe.", MsgBoxStyle.Exclamation, Text)
                            Marcar_Error(ii, "ColCodigo", "El código no existe en maestro")
                            'cod = ""
                        End If

                        'EFREN 200720211214: La llave es null, pero en procesos, para el inv. se requiere idpresentación
                        ' Presentacion
                        If pres <> "" Then

                            If vPresentacionPorClave.ContainsKey(vprod & "|" & pres) Then
                                vpres = vPresentacionPorClave(vprod & "|" & pres)
                                grdData.Rows(ii).Cells("ColIdPresentacion").Value = vpres
                            Else

                                Dim vNomPresSinCNP As String = clsPublic.Quitar_Caracteres_No_Permitidos(pres)

                                If vPresentacionPorClave.ContainsKey(vprod & "|" & vNomPresSinCNP) Then
                                    vpres = vPresentacionPorClave(vprod & "|" & vNomPresSinCNP)
                                    grdData.Rows(ii).Cells("ColIdPresentacion").Value = vpres
                                Else
                                    '#EJC20180528: No se obtuvo la presentación con el nombre.
                                    Marcar_Error(ii, "ColPresentacion", " Nombre de presentación incorrecto")
                                End If

                            End If
                        Else
                            '#EJC20211116: Se va a recibir en UMBAS
                            grdData.Rows(ii).Cells("ColIdPresentacion").Value = 0
                        End If

                        ' Cantidad
                        '#EJC20180528: Quité try de Jaros para validar la cantidad.
                        Cantidad = IIf(Val(vCantidad) <= 0, 0, Val(vCantidad))

                        ' Unidad medida
                        If UM <> "" Then
                            If vUnidadPorNombre.ContainsKey(UM) Then
                                vuni = vUnidadPorNombre(UM)
                                grdData.Rows(ii).Cells("ColIdUnidadMedida").Value = vuni
                            Else
                                If UM = "UN" OrElse UM = "UNI" OrElse UM = "UNIDAD" Then
                                    If Not BeProducto.UnidadMedida Is Nothing Then
                                        If BeProducto.UnidadMedida.Nombre.StartsWith(UM) Then
                                            vuni = BeProducto.UnidadMedida.IdUnidadMedida
                                            grdData.Rows(ii).Cells("ColIdUnidadMedida").Value = vuni
                                        Else
                                            Marcar_Error(ii, "ColUnidadMedida", "Unidad de medida incorrecta")
                                        End If
                                    Else
                                        Marcar_Error(ii, "ColUnidadMedida", "Unidad de medida incorrecta")
                                    End If
                                End If
                            End If
                        Else
                            '#EJC20180528: Si la UM es vacía se busca por defecto la UMBas del producto ;)
                            vTraceReloj.Restart()
                            '#EJC20260522_INV_IMPORT_PRODUCTO_LITE: la UMBas ya viene en BeProducto.
                            vIdUnidadMedida = BeProducto.IdUnidadMedidaBasica
                            vTraceMsUnidadDefault += vTraceReloj.ElapsedMilliseconds
                            If vIdUnidadMedida = 0 Then
                                '#EJC20180528: La unidad de medida básica no puede ser vacía coño!!
                                Marcar_Error(ii, "ColUnidadMedida", "El producto requiere una unidad de medida")
                            Else
                                vuni = vIdUnidadMedida
                                grdData.Rows(ii).Cells("ColIdUnidadMedida").Value = vuni
                                If Not BeProducto.UnidadMedida Is Nothing Then
                                    grdData.Rows(ii).Cells("ColUnidadMedida").Value = BeProducto.UnidadMedida.Nombre
                                End If
                            End If
                        End If

                        'EFREN10112021: Se valida existencia de la idubicación
                        If vUbicacion > 0 Then

                            vTraceReloj.Restart()
                            Dim vUbicacionExiste As Boolean = False
                            If vReadModelDisponible Then
                                vUbicacionExiste = vUbicacionesValidas.Contains(vUbicacion)
                            Else
                                Dim BeBodegaUbicacion = New clsBeBodega_ubicacion()
                                BeBodegaUbicacion.IdUbicacion = vUbicacion
                                BeBodegaUbicacion.IdBodega = AP.IdBodega
                                clsLnBodega_ubicacion.GetSingle(BeBodegaUbicacion)
                                vUbicacionExiste = BeBodegaUbicacion IsNot Nothing
                            End If
                            vTraceMsUbicacion += vTraceReloj.ElapsedMilliseconds

                            If vUbicacionExiste Then
                                grdData.Rows(ii).Cells("ColUbicacion").Value = vUbicacion
                            Else
                                Marcar_Error(ii, "ColUbicacion", "Ubicación no existe en WMS")
                            End If

                        End If

                        '#EJC20180528: Quité try de Jaros para validar el peso.
                        Peso = IIf(Val(vPeso) <= 0, 0, Val(vPeso))

                        '#GT24112022_1730: se valida que parametro a y b correspondan con el registrado, y se envia el ID

                        If vParametro_a <> "" Then

                            If Not BeProducto.ParametroA Is Nothing Then

                                If IsNumeric(vParametro_a) Then
                                    If BeProducto.ParametroA.IdProductoParametroA <> vParametro_a Then
                                        Marcar_Error(ii, "ColParametro_a", "El valor en la columna ParametroA no corresponde con el registrado para el producto")
                                    Else
                                        grdData.Rows(ii).Cells("ColParametro_a").Value = BeProducto.ParametroA.IdProductoParametroA
                                    End If
                                Else
                                    If BeProducto.ParametroA.Nombre <> vParametro_a Then
                                        Marcar_Error(ii, "ColParametro_a", "El valor en la columna ParametroA no corresponde con el registrado para el producto")
                                    Else
                                        grdData.Rows(ii).Cells("ColParametro_a").Value = BeProducto.ParametroA.IdProductoParametroA
                                    End If
                                End If

                            Else
                                '#GT30112022_0900: si es texto, se usa como nombre del parametro, el codigo sera igual que el id
                                If IsNumeric(vParametro_a) = False Then
                                    Dim parametro_a As New clsBeProducto_parametro_a()
                                    vTraceReloj.Restart()
                                    correlativo_a = clsLnProducto_parametro_a.MaxID(lConnection, lTransaction) + 1
                                    parametro_a.IdProductoParametroA = correlativo_a
                                    parametro_a.Codigo = correlativo_a.ToString
                                    parametro_a.Nombre = vParametro_a.Trim
                                    parametro_a.Fec_agr = Now
                                    parametro_a.User_agr = AP.UsuarioAp.IdUsuario
                                    parametro_a.Fec_mod = Now
                                    parametro_a.User_mod = AP.UsuarioAp.IdUsuario
                                    parametro_a.Activo = 1
                                    clsLnProducto_parametro_a.Insertar(parametro_a, lConnection, lTransaction)
                                    vTraceMsParametroA += vTraceReloj.ElapsedMilliseconds
                                    grdData.Rows(ii).Cells("ColParametro_a").Value = correlativo_a
                                    'BeProducto.IdProductoParametroA = correlativo_a
                                    'clsLnProducto.Actualizar(BeProducto, lConnection, lTransaction)

                                End If

                            End If

                        End If

                        If vParametro_b <> "" Then

                            If Not BeProducto.ParametroB Is Nothing Then

                                If IsNumeric(vParametro_b) Then
                                    If BeProducto.ParametroB.IdProductoParametroB <> vParametro_b Then
                                        Marcar_Error(ii, "ColParametro_b", "El valor en la columna ParametroB no corresponde con el registrado para el producto")
                                    Else
                                        grdData.Rows(ii).Cells("ColParametro_b").Value = BeProducto.ParametroB.IdProductoParametroB
                                    End If
                                Else
                                    If BeProducto.ParametroB.Nombre <> vParametro_b Then
                                        Marcar_Error(ii, "ColParametro_b", "El valor en la columna ParametroB no corresponde con el registrado para el producto")
                                    Else
                                        grdData.Rows(ii).Cells("ColParametro_b").Value = BeProducto.ParametroB.IdProductoParametroB
                                    End If
                                End If

                            Else
                                '#GT30112022_0900: si es texto, se usa como nombre del parametro, el codigo sera igual que el id
                                If IsNumeric(vParametro_b) = False Then
                                    Dim parametro_b As New clsBeProducto_parametro_b()
                                    vTraceReloj.Restart()
                                    correlativo_b = clsLnProducto_parametro_b.MaxID(lConnection, lTransaction) + 1
                                    parametro_b.IdProductoParametroB = correlativo_b
                                    parametro_b.Codigo = correlativo_b.ToString
                                    parametro_b.Nombre = vParametro_b.Trim
                                    parametro_b.Fec_agr = Now
                                    parametro_b.User_agr = AP.UsuarioAp.IdUsuario
                                    parametro_b.Fec_mod = Now
                                    parametro_b.User_mod = AP.UsuarioAp.IdUsuario
                                    parametro_b.Activo = 1
                                    clsLnProducto_parametro_b.Insertar(parametro_b, lConnection, lTransaction)
                                    vTraceMsParametroB += vTraceReloj.ElapsedMilliseconds
                                    grdData.Rows(ii).Cells("ColParametro_a").Value = correlativo_b

                                    'BeProducto.IdProductoParametroB = correlativo_b
                                    'clsLnProducto.Actualizar(BeProducto, lConnection, lTransaction)

                                End If

                            End If

                        End If

                        If correlativo_a <> 0 OrElse correlativo_b <> 0 Then
                            vTraceReloj.Restart()
                            BeProducto = clsLnProducto.Get_Single_By_Codigo(BeProducto.Codigo, lConnection, lTransaction)
                            BeProducto.IdProductoParametroA = correlativo_a
                            BeProducto.IdProductoParametroB = correlativo_b
                            clsLnProducto.Actualizar(BeProducto, lConnection, lTransaction)
                            vTraceMsProductoUpdate += vTraceReloj.ElapsedMilliseconds
                        End If

                        If AP.Bodega.Control_Talla_Color Then

                            If vColor = "" Then
                                Marcar_Error(ii, "ColColor", "Debe ingresar el color del producto")
                            End If

                            If vTalla = "" Then
                                Marcar_Error(ii, "ColTalla", "Debe ingresar la talla del producto")
                            End If

                            If vColor <> "" Then
                                vTraceReloj.Restart()
                                Dim BeColor As clsBeColor = Nothing
                                If vReadModelDisponible AndAlso vColorPorCodigo.ContainsKey(vColor) Then
                                    BeColor = vColorPorCodigo(vColor)
                                Else
                                    BeColor = clsLnColor.Get_Single_By_Codigo(vColor, lConnection, lTransaction)
                                End If
                                vTraceMsColor += vTraceReloj.ElapsedMilliseconds
                                If BeColor Is Nothing Then
                                    Marcar_Error(ii, "ColColor", "El valor en la columna color no existe")
                                End If
                            End If

                            If vTalla <> "" Then
                                vTraceReloj.Restart()
                                Dim BeTalla As clsBeTalla = Nothing
                                If vReadModelDisponible AndAlso vTallaPorCodigo.ContainsKey(vTalla) Then
                                    BeTalla = vTallaPorCodigo(vTalla)
                                Else
                                    BeTalla = clsLnTalla.Get_Single_By_Codigo(vTalla, lConnection, lTransaction)
                                End If
                                vTraceMsTalla += vTraceReloj.ElapsedMilliseconds
                                If BeTalla Is Nothing Then
                                    Marcar_Error(ii, "ColTalla", "El valor en la columna talla no existe")
                                End If
                            End If

                        End If

                    End If

                Else
                    MsgBox("El código de producto: " & cod & " No existe.", MsgBoxStyle.Exclamation, Text)
                    Marcar_Error(ii, "ColCodigo", "El código no existe en maestro")
                End If

                grdData.CurrentCell = grdData.Rows(ii).Cells(0)

                If ii = 0 OrElse (ii + 1) Mod 25 = 0 OrElse ii = rc - 1 Then
                    vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
                    Application.DoEvents()
                    vTraceMsUi += vTraceReloj.ElapsedMilliseconds
                End If

                If (ii + 1) Mod 500 = 0 Then
                    InvImportTrace_Marca("VALIDAR_DATOS_PROGRESS",
                                         "Fila=" & ii + 1 &
                                         ";Err=" & errc &
                                         ";MsProducto=" & vTraceMsProducto &
                                         ";MsUnidadDefault=" & vTraceMsUnidadDefault &
                                         ";MsUbicacion=" & vTraceMsUbicacion &
                                         ";MsParametroA=" & vTraceMsParametroA &
                                         ";MsParametroB=" & vTraceMsParametroB &
                                         ";MsProductoUpdate=" & vTraceMsProductoUpdate &
                                         ";MsColor=" & vTraceMsColor &
                                         ";MsTalla=" & vTraceMsTalla &
                                         ";MsReadModel=" & vTraceMsReadModel &
                                         ";MsUI=" & vTraceMsUi)
                End If

            Next

            InvImportTrace_Marca("VALIDAR_DATOS_COMMIT_START")
            lTransaction.Commit()
            InvImportTrace_Marca("VALIDAR_DATOS_COMMIT_END")

            lblPrg.Text = "Validación finalizada"
            lblPrg.Refresh()

        Catch ex As Exception
            errc = errc + 1
            vErrorDescription = ex.Message
            InvImportTrace_Marca("VALIDAR_DATOS_ERROR", ex.Message)
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
        Finally
            InvImportTrace_Marca("VALIDAR_DATOS_FIN",
                                 "Rows=" & rc &
                                 ";Err=" & errc &
                                 ";MsCatalogos=" & vTraceMsCatalogos &
                                 ";MsProducto=" & vTraceMsProducto &
                                 ";MsUnidadDefault=" & vTraceMsUnidadDefault &
                                 ";MsUbicacion=" & vTraceMsUbicacion &
                                 ";MsParametroA=" & vTraceMsParametroA &
                                 ";MsParametroB=" & vTraceMsParametroB &
                                 ";MsProductoUpdate=" & vTraceMsProductoUpdate &
                                 ";MsColor=" & vTraceMsColor &
                                 ";MsTalla=" & vTraceMsTalla &
                                 ";MsReadModel=" & vTraceMsReadModel &
                                 ";MsUI=" & vTraceMsUi &
                                 ";ReadModel=" & vReadModelDisponible)
            prg.Value = 0
            prg.Visible = False
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

        lblPrg.Text = ""

        grdData.ClearSelection()

        If errc > 0 Then
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("Error al procesar fila {0}: ", ii + 1) & " " & vErrorDescription, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

    Private Sub Importar_Datos()

        Dim lInventarioTeorico As New List(Of clsBeTrans_inv_stock_prod)
        Dim BeTrans_inv_stock_prod As clsBeTrans_inv_stock_prod
        Dim rc, ii, vIdProducto, vIdPresentacion, vIdUnidadMedida As Integer
        Dim Cantidad, Peso As Double
        Dim vLote As String = ""
        Dim vNombre_comercial As String = ""
        Dim vCodigoProducto As String = ""
        Dim vFechaVence As DateTime = Now
        Dim sFechaVence As String = ""
        Dim vUbicacion As Integer
        Dim vLicense_plate As String = ""
        Dim vCodigo_Variante As String = ""
        Dim ExisteInventarioTeorico As Boolean = False
        '#GT24112022_1500: campos DyD
        Dim vCosto As Double
        Dim vPrecio As Double
        Dim vParametro_a As String = ""
        Dim vParametro_b As String = ""
        Dim vCodigo_Area_SAP As String = ""
        Dim Color As String = ""
        Dim Talla As String = ""
        Dim IdProductoTallaColor As Integer = 0
        Dim vTraceReloj As System.Diagnostics.Stopwatch
        Dim vTraceMsUnidad As Long = 0
        Dim vTraceMsExist As Long = 0
        Dim vTraceMsImportar As Long = 0
        '#EJC20260522_INV_APLICAR_TEORICO_CACHE: evita abrir conexion por cada fila para obtener la unidad basica.
        Dim vUnidadMedidaPorCodigo As New System.Collections.Generic.Dictionary(Of String, Integer)(System.StringComparer.OrdinalIgnoreCase)

        Cursor.Current = Cursors.WaitCursor

        'EFREN190720212030: Se limpia la lista, porque al reintentar importar, mantiene la data anterior.
        lInventarioTeorico.Clear()

        Try

            rc = grdData.Rows.Count
            InvImportTrace_Marca("IMPORTAR_DATOS_START", "Rows=" & rc & ";InsertaInv=" & InsertaInv & ";TipoTeorico=" & TipoTeoricoImportacion)

            prg.Maximum = rc

            lblPrg.Text = "Preparando lista"
            lblPrg.Refresh()

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Aplicando inventario...")

            For ii = 0 To rc - 1

                If ii = 0 OrElse (ii + 1) Mod 100 = 0 OrElse ii = rc - 1 Then
                    lblPrg.Text = "Preparando fila: " & ii + 1 & " de: " & rc
                    lblPrg.Refresh()
                    SplashScreenManager.Default.SetWaitFormDescription("Preparando fila: " & ii + 1 & " de: " & rc)
                End If

                Try
                    Cantidad = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCantidad").Value), 0, CDbl(grdData.Rows(ii).Cells("ColCantidad").Value))
                Catch ex As Exception
                    Cantidad = 0
                End Try

                Try
                    Peso = IIf(IsDBNull(grdData.Rows(ii).Cells("ColPeso").Value), 0, Val(grdData.Rows(ii).Cells("ColPeso").Value))
                Catch ex As Exception
                    Peso = 0
                End Try

                vIdProducto = grdData.Rows(ii).Cells("ColIdProducto").Value
                vIdPresentacion = grdData.Rows(ii).Cells("ColIdPresentacion").Value
                vCodigoProducto = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCodigo").Value), "", grdData.Rows(ii).Cells("ColCodigo").Value)
                vIdUnidadMedida = 0
                Try
                    If Not IsDBNull(grdData.Rows(ii).Cells("ColIdUnidadMedida").Value) AndAlso
                       grdData.Rows(ii).Cells("ColIdUnidadMedida").Value.ToString().Trim() <> "" Then
                        vIdUnidadMedida = CInt(grdData.Rows(ii).Cells("ColIdUnidadMedida").Value)
                    End If
                Catch ex As Exception
                    vIdUnidadMedida = 0
                End Try

                If vIdUnidadMedida <= 0 Then
                    Dim vIdUnidadMedidaCache As Integer = 0
                    If Not vUnidadMedidaPorCodigo.TryGetValue(vCodigoProducto, vIdUnidadMedidaCache) Then
                        vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
                        vIdUnidadMedidaCache = clsLnProducto.Get_Id_Unidad_Medida_By_Codigo(vCodigoProducto) '#CM_20190807: para idealsa se cargaba el idunidadmedida del maestros de productos, no del excel.
                        vTraceMsUnidad += vTraceReloj.ElapsedMilliseconds
                        vUnidadMedidaPorCodigo(vCodigoProducto) = vIdUnidadMedidaCache
                    End If
                    vIdUnidadMedida = vIdUnidadMedidaCache
                End If
                vLote = IIf(IsDBNull(grdData.Rows(ii).Cells("ColLote").Value), "", grdData.Rows(ii).Cells("ColLote").Value)
                sFechaVence = IIf(IsDBNull(grdData.Rows(ii).Cells("ColFechaVence").Value), "01/01/1900", grdData.Rows(ii).Cells("ColFechaVence").Value)
                vUbicacion = grdData.Rows(ii).Cells("ColUbicacion").Value
                vLicense_plate = IIf(IsDBNull(grdData.Rows(ii).Cells("ColLp").Value), "", grdData.Rows(ii).Cells("ColLp").Value)
                vCodigo_Variante = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCodVariante").Value), "", grdData.Rows(ii).Cells("ColCodVariante").Value)
                '#GT24112022_1500: campos DyD
                vCosto = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCosto").Value), 0, grdData.Rows(ii).Cells("ColCosto").Value)
                vPrecio = IIf(IsDBNull(grdData.Rows(ii).Cells("ColPrecio").Value), 0, grdData.Rows(ii).Cells("ColPrecio").Value)
                vParametro_a = IIf(IsDBNull(grdData.Rows(ii).Cells("ColParametro_a").Value), "", grdData.Rows(ii).Cells("ColParametro_a").Value)
                vParametro_b = IIf(IsDBNull(grdData.Rows(ii).Cells("ColParametro_b").Value), "", grdData.Rows(ii).Cells("ColParametro_b").Value)
                vCodigo_Area_SAP = IIf(IsDBNull(grdData.Rows(ii).Cells("ColCodigo_Area").Value), "", grdData.Rows(ii).Cells("ColCodigo_Area").Value)
                Color = IIf(IsDBNull(grdData.Rows(ii).Cells("ColColor").Value), "", grdData.Rows(ii).Cells("ColColor").Value)
                Talla = IIf(IsDBNull(grdData.Rows(ii).Cells("ColTalla").Value), "", grdData.Rows(ii).Cells("ColTalla").Value)
                IdProductoTallaColor = IIf(IsDBNull(grdData.Rows(ii).Cells("ColIdProductoTallaColor").Value), "", grdData.Rows(ii).Cells("ColIdProductoTallaColor").Value)

                If sFechaVence <> "" Then
                    vFechaVence = CDate(sFechaVence)
                Else
                    vFechaVence = New Date(1900, 1, 1)
                End If

                If vCodigoProducto = "11000504" Then
                    'Debug.Print("Espera")
                End If

                'EFREN17112021: Se omiten campos, porque no son funcionales en la clase.
                'vIdPropietarioBodega = grdData.Rows(ii).Cells("colIdPropietarioBodega").Value
                'vNombre_comercial = grdData.Rows(ii).Cells("colnombre_propietario").Value
                'item.IdPropietarioBodega = vIdPropietarioBodega
                'item.nombre_propietario = pBeProducto.Propietario.Nombre_comercial

                BeTrans_inv_stock_prod = New clsBeTrans_inv_stock_prod
                BeTrans_inv_stock_prod.Idinventario = IdInventario
                BeTrans_inv_stock_prod.IdProducto = vIdProducto
                BeTrans_inv_stock_prod.IdPresentacion = vIdPresentacion
                BeTrans_inv_stock_prod.Cant = Cantidad
                BeTrans_inv_stock_prod.Peso = Peso
                BeTrans_inv_stock_prod.IdUnidadMedida = vIdUnidadMedida
                BeTrans_inv_stock_prod.Lote = vLote
                BeTrans_inv_stock_prod.Fecha_vence = vFechaVence
                BeTrans_inv_stock_prod.Codigo = vCodigoProducto
                BeTrans_inv_stock_prod.IdBodega = AP.IdBodega
                BeTrans_inv_stock_prod.IdUbicacion = vUbicacion
                BeTrans_inv_stock_prod.License_plate = vLicense_plate
                BeTrans_inv_stock_prod.Codigo_variante = vCodigo_Variante
                '#GT25112022_1200: campos DyD
                BeTrans_inv_stock_prod.Costo = vCosto
                BeTrans_inv_stock_prod.Precio = vPrecio
                BeTrans_inv_stock_prod.Parametro_a = vParametro_a
                BeTrans_inv_stock_prod.Parametro_b = vParametro_b
                BeTrans_inv_stock_prod.TipoTeoricoImportacion = TipoTeoricoImportacion
                BeTrans_inv_stock_prod.Codigo_Area = vCodigo_Area_SAP
                BeTrans_inv_stock_prod.Codigo_Talla = Talla
                BeTrans_inv_stock_prod.Codigo_Color = Color
                BeTrans_inv_stock_prod.IdProductoTallaColor = IdProductoTallaColor

                '#AT20251015 Campos para MAMPA
                lInventarioTeorico.Add(BeTrans_inv_stock_prod)

                If ii = 0 OrElse (ii + 1) Mod 100 = 0 OrElse ii = rc - 1 Then
                    prg.Value = Math.Min(ii + 1, prg.Maximum)
                    Application.DoEvents()
                End If

                If (ii + 1) Mod 500 = 0 Then
                    InvImportTrace_Marca("IMPORTAR_DATOS_LISTA_PROGRESS",
                                         "Fila=" & ii + 1 &
                                         ";Lista=" & lInventarioTeorico.Count &
                                         ";MsUnidad=" & vTraceMsUnidad &
                                         ";UnidadCache=" & vUnidadMedidaPorCodigo.Count)
                End If

            Next

            lblPrg.Text = "Insertando registros..."
            lblPrg.Refresh()

            InvImportTrace_Marca("IMPORTAR_DATOS_LISTA_END", "Lista=" & lInventarioTeorico.Count & ";MsUnidad=" & vTraceMsUnidad & ";UnidadCache=" & vUnidadMedidaPorCodigo.Count)
            vTraceReloj = System.Diagnostics.Stopwatch.StartNew()
            ExisteInventarioTeorico = clsLnTrans_inv_stock_prod.Exist(IdInventario,
                                                                      TipoTeoricoImportacion)
            vTraceMsExist = vTraceReloj.ElapsedMilliseconds
            InvImportTrace_Marca("IMPORTAR_DATOS_EXIST_END", "Existe=" & ExisteInventarioTeorico & ";MsExist=" & vTraceMsExist)

            If ExisteInventarioTeorico Then

                If MsgBox("Eliminar inventario teórico existente?",
                                    MessageBoxButtons.YesNo,
                                    "Inventario") = vbYes Then

                    vTraceReloj.Restart()
                    clsLnTrans_inv_stock_prod.Importar_Productos(lInventarioTeorico,
                                                                InsertaInv,
                                                                AP.IdBodega,
                                                                AP.IdEmpresa,
                                                                IdOperador,
                                                                NomOperador,
                                                                DobleVerificacion,
                                                                prg,
                                                                True,
                                                                ExisteInventarioTeorico)
                    vTraceMsImportar = vTraceReloj.ElapsedMilliseconds
                Else
                    vTraceReloj.Restart()
                    clsLnTrans_inv_stock_prod.Importar_Productos(lInventarioTeorico,
                                                                InsertaInv,
                                                                AP.IdBodega,
                                                                AP.IdEmpresa,
                                                                IdOperador,
                                                                NomOperador,
                                                                DobleVerificacion,
                                                                prg,
                                                                False,
                                                                ExisteInventarioTeorico)
                    vTraceMsImportar = vTraceReloj.ElapsedMilliseconds
                End If

            Else

                vTraceReloj.Restart()
                clsLnTrans_inv_stock_prod.Importar_Productos(lInventarioTeorico,
                                                             InsertaInv,
                                                             AP.IdBodega,
                                                             AP.IdEmpresa,
                                                             IdOperador,
                                                             NomOperador,
                                                             DobleVerificacion,
                                                             prg,
                                                             False,
                                                             ExisteInventarioTeorico)
                vTraceMsImportar = vTraceReloj.ElapsedMilliseconds
            End If

            InvImportTrace_Marca("IMPORTAR_DATOS_IMPORTAR_PRODUCTOS_END", "MsImportar=" & vTraceMsImportar)

            SplashScreenManager.CloseForm(False)

            XtraMessageBox.Show("Se aplicó el inventario inicial correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            DialogResult = DialogResult.OK

        Catch ex As Exception
            InvImportTrace_Marca("IMPORTAR_DATOS_ERROR", ex.Message)
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                    Text,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
            Cursor.Current = Cursors.Default
        Finally
            InvImportTrace_Marca("IMPORTAR_DATOS_FIN",
                                 "Rows=" & rc &
                                 ";Lista=" & lInventarioTeorico.Count &
                                 ";MsUnidad=" & vTraceMsUnidad &
                                 ";UnidadCache=" & vUnidadMedidaPorCodigo.Count &
                                 ";MsExist=" & vTraceMsExist &
                                 ";MsImportar=" & vTraceMsImportar)
            SplashScreenManager.CloseForm(False)
        End Try

        Cursor.Current = Cursors.Default

    End Sub

#End Region

#Region " Eventos "

    Private Sub mnuPaste_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdPegar.ItemClick
        Paste()
        Validar_Datos()
    End Sub

    Private Sub mnuValidar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuValidar.ItemClick
        If grdData.Rows.Count > 0 Then Validar_Datos()
    End Sub

    Private Sub mnuAplicar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAplicar.ItemClick
        Aplicar_Teorico()
    End Sub

    Private Sub Aplicar_Teorico(Optional ByVal ConfirmarImportacion As Boolean = True,
                               Optional ByVal PreguntaValidarDatos As Boolean = True)

        Try

            If grdData.Rows.Count = 0 Then
                MsgBox("El inventario esta vacío.") : Return
            End If

            If ConfirmarImportacion Then
                If MessageBox.Show("¿Importar datos?", "", MessageBoxButtons.YesNo) = DialogResult.No Then Return
            End If

            If PreguntaValidarDatos Then
                If MessageBox.Show("¿Validar datos?", "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Validar_Datos()
                End If
            Else
                Validar_Datos()
            End If

            If errc > 0 Then
                MsgBox("No se puede completar la importacion, primero debe corregir los errores.") : Return
            End If

            lblPrg.Text = "Importando datos ...  "

            Importar_Datos()

            lblPrg.Text = ""

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmdAdd_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdAdd.ItemClick
        grdData.Rows.Add(1)
        grdData.FirstDisplayedScrollingRowIndex = grdData.Rows.Count - 1
        grdData.Rows(grdData.Rows.Count - 1).Cells(0).Selected = True
    End Sub

    Private Sub cmdDel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdDel.ItemClick
        Try
            grdData.Rows.Remove(grdData.CurrentRow)
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Try
            grdData.Rows.Clear()
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

#End Region

#Region " Aux "

    Private Sub Marcar_Error(ri As Integer, NombreColumna As String, ByVal Mensaje As String)
        errc += 1
        grdData.Rows(ri).Cells(0).Style.BackColor = Color.Salmon
        grdData.Rows(ri).Cells(0).Value = "ERROR"
        grdData.Rows(ri).Cells("ColError").Value = Mensaje
        grdData.Rows(ri).Cells(NombreColumna).Style.BackColor = Color.Salmon
    End Sub

    Private Sub Importar_Excel()

        Try
            InvImportTrace_Iniciar("IMPORTAR_EXCEL_START", "TipoInventario=" & TipoInventario & ";TipoTeorico=" & TipoTeoricoImportacion)

            Dim Carga As New frmCargaExcel() With {.pNombreMantenimiento = "Inventario " + TipoInventario,
                .pTipoMantenimiento = "Inventario",
                .Listar = Nothing,
                .IdInventarioEnc = IdInventario}

            If Carga.ShowDialog() = DialogResult.OK Then
                InvImportTrace_Marca("IMPORTAR_EXCEL_DIALOG_OK", "RowsCarga=" & Carga.lInvenarioTeorico.Rows.Count)

                Dim i As Integer = 0
                Dim vContador As Integer = 1

                prg.Visible = True
                lblPrg.Visible = True

                prg.Maximum = Carga.lInvenarioTeorico.Rows.Count

                RibbonControl.Enabled = False

                grdData.SuspendLayout()
                grdData.Rows.Clear()

                lblRegs.Caption = "Registros: " & Carga.lInvenarioTeorico.Rows.Count()

                InsertaInv = Carga.InsertaInv
                IdOperador = Carga.IdOperador
                NomOperador = Carga.NomOperador

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormDescription("Procesando archivo...")

                Application.DoEvents()

                Dim vCantRegistros As Integer = Carga.lInvenarioTeorico.Rows().Count()

                For Each ProdInv As DataRow In Carga.lInvenarioTeorico.Rows()

                    SplashScreenManager.Default.SetWaitFormDescription("Código: " & ProdInv("Codigo") & " " & i & " de " & vCantRegistros)

                    lblPrg.Text = "Llenando datos para: " & ProdInv("Codigo")
                    lblPrg.Refresh()

                    i = grdData.Rows.Add(ProdInv("EstadoProcesamiento"),
                               ProdInv("Contador"))

                    grdData.Item("ColCodigo", i).Value = ProdInv("Codigo")
                    grdData.Item("ColPresentacion", i).Value = ProdInv("Presentacion")
                    grdData.Item("ColCantidad", i).Value = ProdInv("Cantidad")
                    grdData.Item("ColPeso", i).Value = ProdInv("Peso")
                    grdData.Item("ColUnidadMedida", i).Value = ProdInv("UM")
                    grdData.Item("ColLote", i).Value = ProdInv("Lote")
                    grdData.Item("colFechaVence", i).Value = ProdInv("Vence")
                    grdData.Item("ColUbicacion", i).Value = ProdInv("Ubicacion")
                    'GT01122021: se agrega LP y cod_variante
                    grdData.Item("ColLp", i).Value = ProdInv("LP")
                    grdData.Item("ColCodVariante", i).Value = ProdInv("CodVariante")
                    '#GT24112022_0800: campos DyD
                    grdData.Item("ColCosto", i).Value = ProdInv("Costo")
                    grdData.Item("ColPrecio", i).Value = ProdInv("Precio")
                    grdData.Item("ColParametro_a", i).Value = ProdInv("Parametro_a")
                    grdData.Item("ColParametro_b", i).Value = ProdInv("Parametro_b")
                    grdData.Item("ColCodigo_Area", i).Value = ProdInv("Codigo_Area")
                    grdData.Item("ColTalla", i).Value = ProdInv("Talla")
                    grdData.Item("ColColor", i).Value = ProdInv("Color")
                    grdData.Item("ColIdProductoTallaColor", i).Value = ProdInv("IdProductoTallaColor")

                    prg.Value = i

                    vContador += 1

                    Application.DoEvents()

                    If vContador Mod 500 = 0 Then
                        InvImportTrace_Marca("IMPORTAR_EXCEL_GRID_PROGRESS", "Fila=" & vContador & ";RowsGrid=" & grdData.Rows.Count)
                    End If

                Next

                grdData.ResumeLayout()
                InvImportTrace_Marca("IMPORTAR_EXCEL_GRID_END", "RowsGrid=" & grdData.Rows.Count)

                InvImportTrace_Marca("IMPORTAR_EXCEL_APLICAR_TEORICO_START")
                Aplicar_Teorico(False, False)
                InvImportTrace_Marca("IMPORTAR_EXCEL_APLICAR_TEORICO_END", "Err=" & errc)

            End If

            Carga.Dispose()

        Catch ex As Exception
            InvImportTrace_Marca("IMPORTAR_EXCEL_ERROR", ex.Message)
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Finally
            InvImportTrace_Marca("IMPORTAR_EXCEL_FIN", "RowsGrid=" & If(grdData Is Nothing, 0, grdData.Rows.Count) & ";Err=" & errc)
            SplashScreenManager.CloseForm(False)
            RibbonControl.Enabled = True
        End Try

    End Sub

    Private Sub frmInventarioImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub mnuImportarExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImportarExcel.ItemClick
        Importar_Excel()
    End Sub

    Private Sub Llena_Catalogos()

        Try
            InvImportTrace_Marca("LLENA_CATALOGOS_START", "MultiPropietario=" & InvTeorico_Multi_Propietario & ";IdPropietarioBodega=" & IdPropietarioBodega)

            'EFREN09052021 si es multi propietario, no se filtra por propietario especifico.
            If InvTeorico_Multi_Propietario Then

                dtall = clsLnProducto.GetCodigosProd_By_Multi_Propietario()
                InvImportTrace_Marca("LLENA_CATALOGOS_PRODUCTOS_MULTI", "Rows=" & If(dtall Is Nothing, 0, dtall.Rows.Count))

                dtp = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_Idbodega(AP.IdBodega)
                InvImportTrace_Marca("LLENA_CATALOGOS_PRESENTACIONES", "Rows=" & If(dtp Is Nothing, 0, dtp.Rows.Count))
                dtu = clsLnUnidad_medida.Listar_By_IdPropietario_Bodega()
                InvImportTrace_Marca("LLENA_CATALOGOS_UM", "Rows=" & If(dtu Is Nothing, 0, dtu.Rows.Count))

            Else
                'EFREN10052021 el propietario especifico se obtiene desde frm inventario, no es necesario obtenerlo del combobox
                dtc = clsLnProducto.GetCodigosProd_By_IdPropietarioBodega(AP.IdBodega, IdPropietarioBodega)
                InvImportTrace_Marca("LLENA_CATALOGOS_PRODUCTOS", "Rows=" & If(dtc Is Nothing, 0, dtc.Rows.Count))
                dtp = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_Idbodega_And_IdPropietarioBodega(AP.IdBodega, IdPropietarioBodega)
                InvImportTrace_Marca("LLENA_CATALOGOS_PRESENTACIONES", "Rows=" & If(dtp Is Nothing, 0, dtp.Rows.Count))
                dtu = clsLnUnidad_medida.Listar_By_IdPropietario_Bodega(IdPropietarioBodega)
                InvImportTrace_Marca("LLENA_CATALOGOS_UM", "Rows=" & If(dtu Is Nothing, 0, dtu.Rows.Count))

            End If

            'dtp = clsLnProducto_presentacion.Get_Nombre_Presentacion_By_Idbodega_And_IdPropietarioBodega(AP.IdBodega, cmbPropietario.EditValue)
            'dtu = clsLnUnidad_medida.Listar_By_IdPropietario_Bodega(cmbPropietario.EditValue)

        Catch ex As Exception
            InvImportTrace_Marca("LLENA_CATALOGOS_ERROR", ex.Message)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmInventarioImport_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, AP.IdBodega)

            lblPrg.Text = ""

            vBodega = AP.IdBodega

            If InvTeorico_Multi_Propietario Then
                cmbPropietario.Visible = False
                lblPropietario.Visible = False
            Else
                cmbPropietario.EditValue = IdPropietarioBodega
                cmbPropietario.Enabled = False
            End If

            Importar_Excel()

            Set_Tipo_Importacion()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub Set_Tipo_Importacion()

        Try

            Select Case TipoTeoricoImportacion
                Case pTipoImportacion.ERP
                    lblTipoImportacion.Caption = "Importación de teórico ERP"
                Case pTipoImportacion.WMS
                    lblTipoImportacion.Caption = "Importación de teórico WMS"
            End Select


        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error)
        End Try

    End Sub

#End Region


End Class
