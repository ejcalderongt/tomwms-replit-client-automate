Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading
Imports System.IO
Imports DevExpress.XtraSplashScreen

Partial Public Class clsLnJornada_sistema

    Private Shared BeJornada As New clsBeJornada_sistema()
    Private Class InvJornadaPreloadCtx
        Public Property Activo As Boolean = False
        Public Property TicketsProcesados As New HashSet(Of Integer)()
        Public Property LicenciasProcesadas As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Public Property StockExistente As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Public Property HashRetroExistente As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Public Property JornadasExistentes As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
    End Class

    Private Shared Function InvJornadaKeyStock(ByVal pLic As String,
                                               ByVal pFecha As Date,
                                               ByVal pIdPropBod As Integer,
                                               ByVal pIdProdBod As Integer,
                                               ByVal pIdStock As Integer) As String
        Return String.Format("{0}|{1}|{2}|{3}|{4}",
                             If(pLic, "").Trim().ToUpperInvariant(),
                             pFecha.ToString("yyyyMMdd"),
                             pIdPropBod,
                             pIdProdBod,
                             pIdStock)
    End Function

    Private Shared Function InvJornadaKeyLic(ByVal pLic As String,
                                             ByVal pIdRecepcionEnc As Integer,
                                             ByVal pIdRecepcionDet As Integer) As String
        Return String.Format("{0}|{1}|{2}",
                             If(pLic, "").Trim().ToUpperInvariant(),
                             pIdRecepcionEnc,
                             pIdRecepcionDet)
    End Function

    Private Shared Function InvJornadaKeyHashFecha(ByVal pHash As String, ByVal pFecha As Date) As String
        Return String.Format("{0}|{1}",
                             If(pHash, "").Trim().ToUpperInvariant(),
                             pFecha.ToString("yyyyMMdd"))
    End Function

    Private Shared Function InvJornadaFechaKey(ByVal pFecha As Date) As String
        Return pFecha.ToString("yyyyMMdd")
    End Function

    Private Shared Function InvJornadaBuildClavesTVP(ByVal pRows As DataTable) As DataTable
        Dim vTabla As New DataTable()
        vTabla.Columns.Add("LicPlate", GetType(String))
        vTabla.Columns.Add("IdPropietarioBodega", GetType(Integer))
        vTabla.Columns.Add("IdProductoBodega", GetType(Integer))
        vTabla.Columns.Add("IdStock", GetType(Integer))
        vTabla.Columns.Add("StockJornadaHash", GetType(String))
        vTabla.Columns.Add("IdTicketTMS", GetType(Integer))
        vTabla.Columns.Add("IdRecepcionEnc", GetType(Integer))
        vTabla.Columns.Add("IdRecepcionDet", GetType(Integer))

        If pRows Is Nothing OrElse pRows.Rows.Count = 0 Then Return vTabla

        For Each r As DataRow In pRows.Rows
            Dim vLicObj As Object = InvJornadaGetRowValue(r, "Lic_Plate", "lic_plate")
            Dim vIdPropObj As Object = InvJornadaGetRowValue(r, "IdPropietarioBodega", "idpropietariobodega")
            Dim vIdProdObj As Object = InvJornadaGetRowValue(r, "IdProductoBodega", "idproductobodega")
            Dim vIdStockObj As Object = InvJornadaGetRowValue(r, "IdStock", "idstock")
            Dim vIdTicketObj As Object = InvJornadaGetRowValue(r, "IdTicketTMS", "idtickettms")
            Dim vIdRecepEncObj As Object = InvJornadaGetRowValue(r, "IdRecepcionEnc", "idrecepcionenc")
            Dim vIdRecepDetObj As Object = InvJornadaGetRowValue(r, "IdRecepcionDet", "idrecepciondet")

            Dim vLic As String = If(vLicObj Is Nothing OrElse IsDBNull(vLicObj), "", CStr(vLicObj))
            Dim vIdProp As Integer = If(vIdPropObj Is Nothing OrElse IsDBNull(vIdPropObj), 0, CInt(vIdPropObj))
            Dim vIdProd As Integer = If(vIdProdObj Is Nothing OrElse IsDBNull(vIdProdObj), 0, CInt(vIdProdObj))
            Dim vIdStock As Integer = If(vIdStockObj Is Nothing OrElse IsDBNull(vIdStockObj), 0, CInt(vIdStockObj))
            Dim vIdTicket As Integer = If(vIdTicketObj Is Nothing OrElse IsDBNull(vIdTicketObj), 0, CInt(Val(vIdTicketObj.ToString())))
            Dim vIdRecepEnc As Integer = If(vIdRecepEncObj Is Nothing OrElse IsDBNull(vIdRecepEncObj), 0, CInt(vIdRecepEncObj))
            Dim vIdRecepDet As Integer = If(vIdRecepDetObj Is Nothing OrElse IsDBNull(vIdRecepDetObj), 0, CInt(vIdRecepDetObj))

            Dim vHash As String = ""
            Try
                Dim vTmp As New clsBeStock_jornada()
                vTmp.IdStock = vIdStock
                vTmp.IdPropietarioBodega = vIdProp
                vTmp.IdProductoBodega = vIdProd
                vTmp.Lic_plate = vLic
                Dim vFechaTicketObj As Object = InvJornadaGetRowValue(r, "Fecha_Ingreso_Ticket_TMS", "fecha_ingreso_ticket_tms")
                vTmp.Fecha_Ingreso_Ticket_TMS = If(vFechaTicketObj Is Nothing OrElse IsDBNull(vFechaTicketObj), New Date(1900, 1, 1), CDate(vFechaTicketObj))
                vTmp.IdRecepcionEnc = vIdRecepEnc
                vTmp.IdRecepcionDet = vIdRecepDet
                vTmp.IdTicketTMS = vIdTicket
                vHash = clsBeStock_jornada.GetRecordHash(vTmp)
            Catch
                vHash = ""
            End Try

            vTabla.Rows.Add(vLic, vIdProp, vIdProd, vIdStock, vHash, vIdTicket, vIdRecepEnc, vIdRecepDet)
        Next

        Return vTabla
    End Function

    Private Shared Function InvJornadaGetRowValue(ByVal pRow As DataRow, ParamArray pNombres() As String) As Object
        If pRow Is Nothing OrElse pRow.Table Is Nothing Then Return Nothing
        For Each vNombre As String In pNombres
            If pRow.Table.Columns.Contains(vNombre) Then
                Return pRow(vNombre)
            End If
        Next
        Return Nothing
    End Function

    Private Shared Function InvJornadaTryPreloadCtx(ByVal pRows As DataTable,
                                                     ByVal pFechaDesde As Date,
                                                     ByVal pFechaHasta As Date,
                                                     ByVal pConnection As SqlConnection,
                                                     ByVal pTransaction As SqlTransaction,
                                                     ByVal pSesionTrace As String,
                                                     ByVal pInicioTrace As DateTime) As InvJornadaPreloadCtx
        Dim vCtx As New InvJornadaPreloadCtx()

        Try
            Dim vClaves As DataTable = InvJornadaBuildClavesTVP(pRows)
            If vClaves.Rows.Count = 0 Then Return vCtx

            Using vCmd As New SqlCommand("dbo.usp_wms_stock_jornada_preload_ctx_tvp_v1", pConnection, pTransaction)
                vCmd.CommandType = CommandType.StoredProcedure
                vCmd.CommandTimeout = 120
                vCmd.Parameters.Add("@FechaDesde", SqlDbType.Date).Value = pFechaDesde.Date
                vCmd.Parameters.Add("@FechaHasta", SqlDbType.Date).Value = pFechaHasta.Date

                Dim vParamClaves As SqlParameter = vCmd.Parameters.Add("@Claves", SqlDbType.Structured)
                vParamClaves.TypeName = "dbo.tvp_wms_stock_jornada_claves_v1"
                vParamClaves.Value = vClaves

                Using vReader As SqlDataReader = vCmd.ExecuteReader()
                    While vReader.Read()
                        If Not IsDBNull(vReader("IdTicketTMS")) Then
                            vCtx.TicketsProcesados.Add(CInt(vReader("IdTicketTMS")))
                        End If
                    End While

                    If vReader.NextResult() Then
                        While vReader.Read()
                            vCtx.LicenciasProcesadas.Add(InvJornadaKeyLic(
                                If(IsDBNull(vReader("LicPlate")), "", CStr(vReader("LicPlate"))),
                                If(IsDBNull(vReader("IdRecepcionEnc")), 0, CInt(vReader("IdRecepcionEnc"))),
                                If(IsDBNull(vReader("IdRecepcionDet")), 0, CInt(vReader("IdRecepcionDet")))))
                        End While
                    End If

                    If vReader.NextResult() Then
                        While vReader.Read()
                            vCtx.StockExistente.Add(InvJornadaKeyStock(
                                If(IsDBNull(vReader("LicPlate")), "", CStr(vReader("LicPlate"))),
                                If(IsDBNull(vReader("Fecha")), New Date(1900, 1, 1), CDate(vReader("Fecha"))),
                                If(IsDBNull(vReader("IdPropietarioBodega")), 0, CInt(vReader("IdPropietarioBodega"))),
                                If(IsDBNull(vReader("IdProductoBodega")), 0, CInt(vReader("IdProductoBodega"))),
                                If(IsDBNull(vReader("IdStock")), 0, CInt(vReader("IdStock")))))
                        End While
                    End If

                    If vReader.NextResult() Then
                        While vReader.Read()
                            vCtx.HashRetroExistente.Add(InvJornadaKeyHashFecha(
                                If(IsDBNull(vReader("Stock_Jornada_Hash")), "", CStr(vReader("Stock_Jornada_Hash"))),
                                If(IsDBNull(vReader("Fecha")), New Date(1900, 1, 1), CDate(vReader("Fecha")))))
                        End While
                    End If

                    If vReader.NextResult() Then
                        While vReader.Read()
                            vCtx.JornadasExistentes.Add(InvJornadaFechaKey(If(IsDBNull(vReader("Fecha")), New Date(1900, 1, 1), CDate(vReader("Fecha")))))
                        End While
                    End If
                End Using
            End Using

            vCtx.Activo = True
            JornadaStockTrace(pSesionTrace, "INSERTAR_STOCK_JORNADA_PRELOAD_OK", pInicioTrace,
                              "Tickets=" & vCtx.TicketsProcesados.Count &
                              ";Licencias=" & vCtx.LicenciasProcesadas.Count &
                              ";Stock=" & vCtx.StockExistente.Count &
                              ";HashRetro=" & vCtx.HashRetroExistente.Count &
                              ";Jornadas=" & vCtx.JornadasExistentes.Count)
        Catch exSql As SqlException When exSql.Number = 2812 OrElse exSql.Number = 208 OrElse exSql.Number = 207
            JornadaStockTrace(pSesionTrace, "INSERTAR_STOCK_JORNADA_PRELOAD_FALLBACK", pInicioTrace,
                              "SqlNumber=" & exSql.Number & ";Msg=" & exSql.Message)
        Catch ex As Exception
            JornadaStockTrace(pSesionTrace, "INSERTAR_STOCK_JORNADA_PRELOAD_ERROR", pInicioTrace, ex.Message)
        End Try

        Return vCtx
    End Function

    Private Shared Sub JornadaStockTrace(ByVal pSesion As String,
                                         ByVal pPaso As String,
                                         ByVal pInicio As DateTime,
                                         Optional ByVal pExtra As String = "")
        Try
            Dim vDir As String = Path.Combine(Path.GetTempPath(), "TOMWMS")
            If Not Directory.Exists(vDir) Then Directory.CreateDirectory(vDir)

            Dim vLinea As String = String.Join("|", New String() {
                Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                "#EJC20260607_STOCK_JORNADA_TRACE",
                "clsLnJornada_sistema",
                pSesion,
                pPaso,
                "DeltaMs=" & CLng((Date.Now - pInicio).TotalMilliseconds),
                pExtra
            })

            File.AppendAllText(Path.Combine(vDir, "stock-jornada-trace.log"), vLinea & Environment.NewLine, System.Text.Encoding.UTF8)
        Catch
        End Try
    End Sub

    Public Shared Function Inicio_De_Jornada_Correcto(ByVal pIdEmpresa As Integer,
                                                      ByVal pIdBodega As Integer,
                                                      ByVal pIdUsuario As Integer,
                                                      ByVal lblprg As RichTextBox,
                                                      ByRef prg As ProgressBar,
                                                      ByRef pParenForm As Form) As Boolean

        Inicio_De_Jornada_Correcto = False

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim fecha_ahora As Date = clsServidor.Get_Fecha_Servidor(lConnection, lTransaction)

            If Existe_Jornada(lConnection, lTransaction) Then

                Inicio_De_Jornada_Correcto = True
                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("La jornada existe para la fecha: " & fecha_ahora.Date)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            Else

                '#EJC202303291522: Esperar dos segundos y volver a consultar si la jornada existe, en caso de concurrencia.
                Thread.Sleep(2000)

                If Existe_Jornada(lConnection, lTransaction) Then
                    Exit Function
                End If

                Dim BeUltimaJornada As New clsBeJornada_sistema
                BeUltimaJornada = Get_Ultima_Jornada(lConnection, lTransaction)

                If Not BeUltimaJornada Is Nothing Then
                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText("La última jornada se registró con fecha -> " & BeUltimaJornada.Fecha)
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()
                End If

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("Iniciando copia de stock... -> " & fecha_ahora)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Application.DoEvents()

                Dim pJornadaActual As New clsBeJornada_sistema

                pJornadaActual = Inserta_Jornada_Actual(pIdEmpresa,
                                                        pIdUsuario,
                                                        lConnection,
                                                        lTransaction)

                If Not pJornadaActual Is Nothing Then

                    If Copiar_Stock_A_Stock_Jornada(BeUltimaJornada,
                                                    pIdEmpresa,
                                                    pIdBodega,
                                                    pIdUsuario,
                                                    lConnection,
                                                    lTransaction,
                                                    lblprg,
                                                    prg,
                                                    pParenForm,
                                                    pJornadaActual) Then

                        Inicio_De_Jornada_Correcto = True

                    End If

                End If

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Existe_Jornada(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        Existe_Jornada = False

        Try

            Dim Fecha_Ahora = clsServidor.Get_Fecha_Servidor(lConnection, lTransaction)

            Dim vSQL As String = "SELECT * FROM jornada_sistema WHERE fecha =@Fecha "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", FormatoFechas.tFecha(Fecha_Ahora))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_Jornada = True
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#GT18012023: Retorna la jornada del día en ejecución
    Public Shared Function Get_Jornada_Actual(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As clsBeJornada_sistema

        Get_Jornada_Actual = Nothing

        Try

            Dim hoy = clsServidor.Get_Fecha_Servidor(lConnection, lTransaction)

            Dim vSQL As String = "SELECT * FROM jornada_sistema WHERE fecha =@Fecha "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", FormatoFechas.tFecha(hoy))
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)


                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vBeJornada_sistema As New clsBeJornada_sistema
                    Cargar(vBeJornada_sistema, lDataTable.Rows(0))
                    Get_Jornada_Actual = vBeJornada_sistema

                End If


            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Ultima_Jornada(ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction) As clsBeJornada_sistema

        Get_Ultima_Jornada = Nothing

        Try

            Dim vSQL As String = "SELECT top(1) * FROM jornada_sistema order by Fecha desc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vBeJornada_sistema As New clsBeJornada_sistema
                    Cargar(vBeJornada_sistema, lDataTable.Rows(0))
                    Get_Ultima_Jornada = vBeJornada_sistema

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Inserta_Jornada(ByVal pIdEmpresa As Integer,
                                            ByVal pIdUsuario As Integer,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction) As Boolean

        Inserta_Jornada = False

        Try


            BeJornada = New clsBeJornada_sistema()
            BeJornada.IdJornada = MaxID(lConnection, lTransaction) + 1
            BeJornada.IdEmpresa = pIdEmpresa
            BeJornada.IdBodega = 0
            BeJornada.Fecha = New Date(Now.Year, Now.Month, Now.Day)
            BeJornada.FechaAgregado = Now
            BeJornada.IdUsuario = pIdUsuario
            Insertar(BeJornada, lConnection, lTransaction)

            Inserta_Jornada = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function MaxID(ByVal lConnection As SqlConnection,
                                 ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdJornada),0) FROM Jornada_sistema"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared vFechaJornadaDiaAnterior As Date = New Date(Now.Year, Now.Month, Now.Day)

    Private Shared Function Copiar_Stock_A_Stock_Jornada(ByVal pUltimaJornada As clsBeJornada_sistema,
                                                         ByVal pIdEmpresa As Integer,
                                                         ByVal pIdBodega As Integer,
                                                         ByVal pIdUsuario As Integer,
                                                         ByVal lConnection As SqlConnection,
                                                         ByVal lTransaction As SqlTransaction,
                                                         ByVal lblprg As RichTextBox,
                                                         ByRef prg As ProgressBar,
                                                         ByRef pParentForm As Form,
                                                         Optional pJornadaActual As clsBeJornada_sistema = Nothing) As Boolean


        Copiar_Stock_A_Stock_Jornada = False

        Try

            Dim fecha_server = clsServidor.Get_Fecha_Servidor()
            Dim vElDiaDeAyer As Date = fecha_server.AddDays(-1)
            Dim lIngresosYSalidasDelDia As New List(Of clsBeStockEnUnaFecha)
            Dim DTVWStockJornada As New DataTable

            DTVWStockJornada = Get_VW_Stock_Jornada(lConnection,
                                                    lTransaction)

            If pUltimaJornada Is Nothing Then


                lIngresosYSalidasDelDia = Get_Ingresos_Y_Salidas_El_Mismo_Dia(vElDiaDeAyer,
                                                                              lConnection,
                                                                              lTransaction,
                                                                              lblprg,
                                                                              prg)

                Insertar_Stock_Jornada(DTVWStockJornada,
                                       vElDiaDeAyer,
                                       lIngresosYSalidasDelDia,
                                       pUltimaJornada,
                                       pIdEmpresa,
                                       pIdBodega,
                                       pIdUsuario,
                                       lConnection,
                                       lTransaction,
                                       lblprg,
                                       prg,
                                       pParentForm)

                Copiar_Stock_A_Stock_Jornada = True

                lblprg.AppendText("La jornada del día 0 en el sistema se registró correctamente :) " & fecha_server)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            Else

                'Dim days As Long = DateDiff(DateInterval.Day, pUltimaJornada.Fecha, Now)
                Dim days As Long = DateDiff(DateInterval.Day, pUltimaJornada.Fecha.Date, fecha_server.Date)
                Dim vFechaJornada As Date

                lblprg.AppendText("Se encontraron " & days & " días de diferencia a partir de la última jornada generada el: " & pUltimaJornada.Fecha)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                For i = 0 To days - 1

                    '#EJC20210520: Utilizar para debug de escenarios específicos.
                    'vFechaJornada = New Date(2021, 4, 23)

                    If i = 0 Then
                        vFechaJornada = pUltimaJornada.Fecha.AddDays(1)
                    Else

                        vFechaJornada = vFechaJornada.AddDays(1)

                        lblprg.AppendText("Procesando stock para jornada del día: " & vFechaJornada)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If

                    lIngresosYSalidasDelDia = Get_Ingresos_Y_Salidas_El_Mismo_Dia(vFechaJornada.Date,
                                                                                  lConnection,
                                                                                  lTransaction,
                                                                                  lblprg,
                                                                                  prg)

                    '#GT27122022_1900: aca si debe validar retroactivo=1 porque es el proceso normal diario.  La otra llamada es una copia
                    Insertar_Stock_Jornada(DTVWStockJornada,
                                           vFechaJornada,
                                           lIngresosYSalidasDelDia,
                                           pUltimaJornada,
                                           pIdEmpresa,
                                           pIdBodega,
                                           pIdUsuario,
                                           lConnection,
                                           lTransaction,
                                           lblprg,
                                           prg,
                                           pParentForm,
                                           1,
                                           pJornadaActual)

                    Application.DoEvents()

                Next

                Copiar_Stock_A_Stock_Jornada = True

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Tiene_Tickets_TMS_Sin_Retroactivo(ByVal lblprg As RichTextBox,
                                                             ByRef prg As ProgressBar,
                                                             ByRef pParenForm As Form,
                                                             ByVal pIdEmpresa As Integer,
                                                             ByVal pIdUsuario As Integer) As Boolean

        Tiene_Tickets_TMS_Sin_Retroactivo = False

        Dim lBeTicketTMSSinRetroactivo As New List(Of clsBeTicketTMSSinRetroactivo)
        Dim BeTicketTMSSinRetroactivo As New clsBeTicketTMSSinRetroactivo
        Dim BeJornadaFechaTicket As New clsBeJornada_sistema
        Dim BeUltimaJornada As New clsBeJornada_sistema


        Try

            Dim vSQL As String = "SELECT distinct IdTicketTMS, 
                                  Fecha_Creacion_Documento, 
                                  Fecha_Ingreso_Ticket 
                                  FROM VW_TMSTickets_Sin_Retroactivo  "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            lblprg.AppendText("Se encontraron: " & lDataTable.Rows.Count & " registros sin retroactivo.")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            For Each R As DataRow In lDataTable.Rows

                                BeTicketTMSSinRetroactivo = New clsBeTicketTMSSinRetroactivo()

                                BeTicketTMSSinRetroactivo.IdTicketTMS = IIf(IsDBNull(R.Item("IdTicketTMS")), 0, R.Item("IdTicketTMS"))
                                BeTicketTMSSinRetroactivo.Fecha_Creacion_Documento = IIf(IsDBNull(R.Item("Fecha_Creacion_Documento")), New Date(1900, 1, 1), R.Item("Fecha_Creacion_Documento"))
                                BeTicketTMSSinRetroactivo.Fecha_Ingreso_Ticket = IIf(IsDBNull(R.Item("Fecha_Ingreso_Ticket")), New Date(1900, 1, 1), R.Item("Fecha_Ingreso_Ticket"))
                                lBeTicketTMSSinRetroactivo.Add(BeTicketTMSSinRetroactivo)

                                lblprg.AppendText("Preparando Ticket: " & BeTicketTMSSinRetroactivo.IdTicketTMS)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            Next

                            For Each Ticket In lBeTicketTMSSinRetroactivo

                                BeJornadaFechaTicket = Get_Jornada_By_Fecha(Ticket.Fecha_Ingreso_Ticket,
                                                                            lConnection,
                                                                            lTransaction)


                                '#GT17102022_1500: si existe la jornada con fecha ticket se imprimre
                                If BeJornadaFechaTicket IsNot Nothing Then
                                    Debug.WriteLine(Ticket.Fecha_Ingreso_Ticket)
                                End If

                                If BeJornadaFechaTicket Is Nothing Then

                                    Dim BeJornadaSistemaDiaFaltante As New clsBeJornada_sistema()
                                    BeJornadaSistemaDiaFaltante = Inserta_Jornada(pIdEmpresa,
                                                                                  pIdUsuario,
                                                                                  Ticket.Fecha_Ingreso_Ticket,
                                                                                  lConnection,
                                                                                  lTransaction)

                                    If Not BeJornadaSistemaDiaFaltante Is Nothing Then
                                        BeJornadaFechaTicket = BeJornadaSistemaDiaFaltante
                                    End If

                                    lblprg.AppendText("Insertando ticket  " & Ticket.IdTicketTMS & " en la jornada nueva: " & BeJornadaSistemaDiaFaltante.IdJornada)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()

                                End If

                                BeUltimaJornada = Get_Ultima_Jornada(lConnection, lTransaction)

                                If Not BeJornadaFechaTicket Is Nothing Then

                                    '#GT27122022_1900: en esta copia de método, existe Insertar_Stock_Jornada que no debe ejecutar retroactivo=1 como parametro enviado.
                                    If Copiar_Stock_A_Stock_Jornada_Parcial(BeJornadaFechaTicket,
                                                                            BeUltimaJornada,
                                                                            Ticket.IdTicketTMS,
                                                                            pIdEmpresa,
                                                                            BeJornadaFechaTicket.IdBodega,
                                                                            pIdUsuario,
                                                                            lConnection,
                                                                            lTransaction,
                                                                            lblprg,
                                                                            prg,
                                                                            pParenForm) Then

                                        '#EJC20220808: El retroactivo de un ticket específico se insertó correctamente en una jornada intercalada.
                                        'lblprg.AppendText(vbNewLine)
                                        lblprg.AppendText("Insertado ticket " & Ticket.IdTicketTMS & " a stock jornada: " & BeJornadaFechaTicket.IdJornada)
                                        lblprg.AppendText(vbNewLine)
                                        lblprg.Refresh()
                                        lblprg.SelectionStart = lblprg.TextLength
                                        lblprg.ScrollToCaret()

                                    Else
                                        Throw New Exception("ERROR_202208081550: No se pudo insertar la jornada del ticket: " & Ticket.IdTicketTMS & " En la fecha de jornada: " & BeJornadaFechaTicket.Fecha)
                                    End If

                                Else
                                    Throw New Exception("ERROR_202208081550: La jornada debería existir para fecha: " & Ticket.Fecha_Ingreso_Ticket)
                                End If

                                lblprg.AppendText("ticket procesado: " & Ticket.IdTicketTMS)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            Next

                            Tiene_Tickets_TMS_Sin_Retroactivo = True

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Tiene_Ingresos_sin_Ticket(ByVal lblprg As RichTextBox,
                                                     ByRef prg As ProgressBar,
                                                     ByRef pParenForm As Form,
                                                     ByVal pIdEmpresa As Integer,
                                                     ByVal pIdUsuario As Integer,
                                                     ByVal pHoy As Date) As Boolean

        Tiene_Ingresos_sin_Ticket = False
        '#GT03012023_1830: solo necesito algunos campos para el desfase de ingresos sin ticket
        Dim lBeIngresoSinTicket As New List(Of clsBeStock_Jornada_Desface)
        Dim BeIngresoSinTicket As New clsBeStock_Jornada_Desface
        Dim BeJornadaHoy As New clsBeJornada_sistema

        Try

            Dim vSQL As String = "Select IdStock,licencia,Fecha_Ingreso 
                                  FROM VW_Ingresos_Sin_Ticket where 1=1 "

            vSQL += " and Fecha_Ingreso = @pHoy "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@pHoy", FormatoFechas.tFecha(pHoy))

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            lblprg.AppendText("Se encontraron: " & lDataTable.Rows.Count & " registros sin ticket.")
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            BeJornadaHoy = Get_Jornada_By_Fecha(pHoy,
                                                                lConnection,
                                                                lTransaction)


                            '#GT17102022_1500: se imprime la fecha de la jornada de "hoy"
                            If BeJornadaHoy IsNot Nothing Then
                                Debug.WriteLine(BeJornadaHoy.Fecha)
                            End If

                            For Each R As DataRow In lDataTable.Rows

                                BeIngresoSinTicket = New clsBeStock_Jornada_Desface()

                                BeIngresoSinTicket.IDSTOCK = IIf(IsDBNull(R.Item("IdStock")), 0, R.Item("IdStock"))
                                BeIngresoSinTicket.LIC_PLATE = IIf(IsDBNull(R.Item("licencia")), "", R.Item("licencia"))
                                BeIngresoSinTicket.FECHA = IIf(IsDBNull(R.Item("Fecha_Ingreso")), New Date(1900, 1, 1), R.Item("Fecha_Ingreso"))
                                lBeIngresoSinTicket.Add(BeIngresoSinTicket)

                                lblprg.AppendText("Preparando IdStock: " & BeIngresoSinTicket.IDSTOCK)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            Next

                            For Each Ingreso In lBeIngresoSinTicket

                                If BeJornadaHoy IsNot Nothing Then

                                    '#GT27122022_1900: en esta copia de método, existe Insertar_Stock_Jornada que no debe ejecutar retroactivo=1 como parametro enviado.
                                    '#GT03012023_2000: en esta copia se valida ingresos sin ticket, y se envia el idstock con fecha hoy
                                    If Copiar_Stock_A_Stock_Jornada_Parcial(BeJornadaHoy,
                                                                            BeJornadaHoy,
                                                                            0,
                                                                            pIdEmpresa,
                                                                            Ingreso.IDBODEGA,
                                                                            pIdUsuario,
                                                                            lConnection,
                                                                            lTransaction,
                                                                            lblprg,
                                                                            prg,
                                                                            pParenForm,
                                                                            Ingreso.IDSTOCK) Then

                                    Else
                                        Throw New Exception("ERROR_202301031800: No se pudo insertar la jornada sin ticket para el stock: " & Ingreso.IDSTOCK & " En la fecha de jornada: " & Ingreso.FECHA)
                                    End If
                                Else
                                    Throw New Exception("ERROR_202301031830: La jornada sin ticket debería existir para fecha: " & BeJornadaHoy.Fecha)
                                End If

                            Next

                            Tiene_Ingresos_sin_Ticket = True

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Private Shared vIteracion As Integer = 1

    Public Shared Function Actualiza_Registros_Con_Desfase(ByVal lblprg As RichTextBox,
                                                           ByRef prg As ProgressBar,
                                                           ByRef pParentForm As Form,
                                                           ByVal lConnection As SqlConnection,
                                                           ByVal lTransaction As SqlTransaction) As Boolean

        Actualiza_Registros_Con_Desfase = False

        Dim lRegistrosAInsertar As New List(Of clsBeStock_Jornada_Desface)
        Dim vRegistrosInsertadosConDesfase As Integer = 0

        Try

            Dim fecha_server = clsServidor.Get_Fecha_Servidor()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Ejecutando SP para búsqueda de desfases...")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Dim vSQL As String = "SP_STOCK_JORNADA_DESFASE "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.StoredProcedure}

                Dim returnValue As Integer = 0
                lCommand.Parameters.Add("@RegistrosARevisar", SqlDbType.Int).Value = returnValue
                lCommand.Parameters.Add("@Fecha_Inicial", SqlDbType.Date).Value = Now.Date.AddMonths(-2)
                'lCommand.Parameters.Add("@Fecha_Final", SqlDbType.Date).Value = Now.Date
                '#GT26122022_1720: uso la fecha del server, en lugar de la fecha del PC.
                lCommand.Parameters.Add("@Fecha_Final", SqlDbType.Date).Value = fecha_server

                lCommand.Parameters("@RegistrosARevisar").Direction = ParameterDirection.Output
                lCommand.Parameters("@Fecha_Inicial").Direction = ParameterDirection.Input
                lCommand.Parameters("@Fecha_Final").Direction = ParameterDirection.Input
                lCommand.ExecuteNonQuery()

                returnValue = lCommand.Parameters("@RegistrosARevisar").Value

                If (returnValue > 0) Then

                    lblprg.AppendText("Se encontraron: " & returnValue & " registros con desfase de fechas, corrigiendo registros automáticamente.")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Dim vSQL1 As String = "select df.* from stock_jornada_desfase df
                                                   where not exists (SELECT * FROM STOCK_JORNADA D WHERE D.LIC_PLATE = DF.LIC_PLATE AND D.FECHA = DF.FECHA_CONSECUTIVA)
                                                   and df.max_fecha >=df.fecha_consecutiva
                                                   order by lic_plate, fecha_consecutiva"

                    Using lDTA As New SqlDataAdapter(vSQL1, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim BeStockJornadaDesfase As New clsBeStock_Jornada_Desface

                            For Each R As DataRow In lDataTable.Rows

                                BeStockJornadaDesfase = New clsBeStock_Jornada_Desface()
                                clsLnSock_Jornada_Desface.Cargar(BeStockJornadaDesfase, R)
                                lRegistrosAInsertar.Add(BeStockJornadaDesfase)

                                lblprg.AppendText("Obteniendo registro base con desfase, Ticket: " & BeStockJornadaDesfase.IDTICKETTMS & " fecha desfase: " & BeStockJornadaDesfase.FECHA_CONSECUTIVA & " Licencia: " & BeStockJornadaDesfase.LIC_PLATE)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            Next

                            Dim DTBeStockJornada As New DataTable
                            Dim BeStockJornadaACopiar As New clsBeStock_jornada
                            Dim BeStockJornadaAInsertar As New clsBeStock_jornada
                            Dim vIdMaxIdStockJornada As Integer = clsLnStock_jornada.MaxID(lConnection, lTransaction) + 1
                            Dim vMensaje As String = ""

                            If lRegistrosAInsertar.Count > 0 Then

                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText("Registros con desfase a insertar: " & lRegistrosAInsertar.Count - 1)
                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                                For Each BeStockJornadaDesfase In lRegistrosAInsertar

                                    DTBeStockJornada = clsLnStock_jornada.Get_All_By_IdJornada_And_IdTicket(BeStockJornadaDesfase.IDJORNADASISTEMA,
                                                                                                            BeStockJornadaDesfase.IDTICKETTMS,
                                                                                                            BeStockJornadaDesfase.IDBODEGA,
                                                                                                            BeStockJornadaDesfase.LIC_PLATE,
                                                                                                            BeStockJornadaDesfase.IDSTOCK,
                                                                                                            BeStockJornadaDesfase.FECHA,
                                                                                                            lConnection,
                                                                                                            lTransaction)


                                    If BeStockJornadaDesfase.LIC_PLATE = "TF000000242" Then
                                        Debug.WriteLine("espera")
                                    End If

                                    If Not DTBeStockJornada Is Nothing Then

                                        If DTBeStockJornada.Rows.Count > 0 Then

                                            Dim vDifDias As Long = DateDiff(DateInterval.Day, BeStockJornadaDesfase.MIN_FECHA, BeStockJornadaDesfase.MAX_FECHA)

                                            clsLnStock_jornada.Cargar(BeStockJornadaACopiar, DTBeStockJornada.Rows(0))
                                            clsPublic.CopyObject(BeStockJornadaACopiar, BeStockJornadaAInsertar)
                                            BeStockJornadaAInsertar.IdStockJornada = vIdMaxIdStockJornada
                                            BeStockJornadaAInsertar.Fecha = BeStockJornadaDesfase.FECHA_CONSECUTIVA
                                            BeStockJornadaAInsertar.Temperatura = 1989
                                            clsLnStock_jornada.Insertar(BeStockJornadaAInsertar, lConnection, lTransaction)
                                            vRegistrosInsertadosConDesfase += 1
                                            vIdMaxIdStockJornada += 1
                                            vMensaje = "Insertando desfase: " & BeStockJornadaAInsertar.IdTicketTMS & " Licencia: " & BeStockJornadaAInsertar.Lic_plate & " Fecha: " & BeStockJornadaDesfase.FECHA_CONSECUTIVA

                                            lblprg.AppendText(vMensaje)
                                            lblprg.AppendText(vbNewLine)
                                            lblprg.Refresh()
                                            lblprg.SelectionStart = lblprg.TextLength
                                            lblprg.ScrollToCaret()

                                        Else
                                            Debug.WriteLine("Error_202209070932A: El Objeto primario.count =0, no puedo copiar el registro base")
                                        End If

                                    Else
                                        Debug.WriteLine("Error_202209070932B: El Objeto primario es Nothing, no puedo copiar el registro base")
                                    End If

                                Next

                                lblprg.AppendText(vRegistrosInsertadosConDesfase & " desfases insertados correctamente.")
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            End If

                        End If

                        vIteracion += 1

                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText(vbNewLine)
                        lblprg.AppendText("Verificando iteración: " & vIteracion)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                        '#EJC20220907: LLamada recursiva bastante peligrosa.
                        Actualiza_Registros_Con_Desfase(lblprg,
                                                        prg,
                                                        pParentForm,
                                                        lConnection,
                                                        lTransaction)

                    End Using

                Else

                    lblprg.AppendText(vbNewLine)
                    lblprg.AppendText("No se encontraron registros de retroactivo con desfase, todo bien :)")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            vIteracion = 0
        End Try

    End Function

    Private Shared Function Get_Ingresos_Y_Salidas_El_Mismo_Dia(ByVal pFechaJornada As Date,
                                                                ByVal lConnection As SqlConnection,
                                                                ByVal lTransaction As SqlTransaction,
                                                                ByVal lblprg As RichTextBox,
                                                                ByRef prg As ProgressBar) As List(Of clsBeStockEnUnaFecha)


        Get_Ingresos_Y_Salidas_El_Mismo_Dia = Nothing

        Try

            Dim lMovimientosMismoDia As New List(Of clsBeVW_Movimientos)

            lMovimientosMismoDia = clsLnVW_Movimientos.Get_All_Ingresos_Y_Salidas_By_Fecha(pFechaJornada,
                                                                        lConnection,
                                                                        lTransaction)

            Dim lStockEnFecha As New List(Of clsBeStockEnUnaFecha)

            lStockEnFecha = Normalizar_Movimientos(lMovimientosMismoDia,
                                                   pFechaJornada,
                                                   lblprg,
                                                   prg)

            Return lStockEnFecha

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Function Normalizar_Movimientos(ByVal ListaMovimientos As List(Of clsBeVW_Movimientos),
                                                   ByVal pFechaJornada As Date,
                                                   ByVal lblprg As RichTextBox,
                                                   ByRef prg As ProgressBar) As List(Of clsBeStockEnUnaFecha)
        Normalizar_Movimientos = Nothing

        Try

            Dim BeStockEnFecha As New clsBeStockEnUnaFecha
            Dim RepMovEnUnaFecha As New List(Of clsBeStockEnUnaFecha)

            Dim Idx As Integer = -1
            Dim Idx1 As Integer = -1

            RepMovEnUnaFecha.Clear()

            lblprg.Visible = True

            If Not ListaMovimientos Is Nothing Then

                For Each ObjM In ListaMovimientos.OrderBy(Function(x) x.EstadoOrigen)

                    lblprg.AppendText("Procesando movimiento para producto: " & ObjM.Codigo & " Para fecha: " & pFechaJornada & vbNewLine)
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    BeStockEnFecha = New clsBeStockEnUnaFecha()
                    BeStockEnFecha.Codigo = ObjM.Codigo
                    BeStockEnFecha.Producto = ObjM.Producto
                    BeStockEnFecha.IdEstadoOrigen = ObjM.IdEstadoOrigen
                    BeStockEnFecha.Fecha_Vence = ObjM.Fecha_Vence
                    BeStockEnFecha.IdUnidadMedida = ObjM.IdUnidadMedida
                    'GT13072022_1426: se agrega la poliza porque no estaba incluida
                    BeStockEnFecha.Lic_Plate = ObjM.Lic_Plate

                    'If ObjM.Lic_Plate = "SG00013" Then
                    '    Debug.Print("Es esta")
                    'End If

                    clsPublic.CopyObject(ObjM, BeStockEnFecha)

                    Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                     AndAlso x.IdEstadoOrigen = BeStockEnFecha.IdEstadoOrigen _
                                                     AndAlso x.Fecha_Vence.Date = BeStockEnFecha.Fecha_Vence.Date _
                                                     AndAlso x.IdPropietarioBodega = BeStockEnFecha.IdPropietarioBodega _
                                                     AndAlso x.IdBodegaOrigen = BeStockEnFecha.IdBodegaOrigen)

                    If Idx <> -1 Then 'Lo encontró por lote.

                        Idx1 = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                          AndAlso x.Lote = BeStockEnFecha.Lote _
                                                          AndAlso x.Fecha_Vence.Date = BeStockEnFecha.Fecha_Vence.Date)
                        'Si no tiene contro por lote...
                        If BeStockEnFecha.Lote = "" Then

                            Idx1 = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                              AndAlso x.Fecha_Vence.Date = BeStockEnFecha.Fecha_Vence.Date)
                            If Idx1 = -1 Then 'No coincide la fecha de vencimiento.. no pasa nada
                                Idx = Idx1
                            Else
                                Idx1 = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                                  AndAlso x.Fecha_Vence.Date = BeStockEnFecha.Fecha_Vence.Date _
                                                                  AndAlso x.IdEstadoOrigen = BeStockEnFecha.IdEstadoOrigen)
                                If Idx1 = -1 Then 'No coincide el estado
                                    Idx = Idx1
                                Else
                                    Idx = Idx1
                                End If

                            End If

                        End If

                    Else

                        Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                         AndAlso x.Fecha_Vence = BeStockEnFecha.Fecha_Vence _
                                                         AndAlso x.IdPropietarioBodega = BeStockEnFecha.IdPropietarioBodega _
                                                         AndAlso x.IdBodegaOrigen = BeStockEnFecha.IdBodegaOrigen)
                    End If

                    '#EJC20210518: Sinceramente no me recuerdo para que se hace lo de arriba.
                    'pero se que tiene sentido, como tambión que se busque por LP
                    If Idx = -1 Then

                        If BeStockEnFecha.Lic_Plate <> "" Then
                            Idx = RepMovEnUnaFecha.FindIndex(Function(x) x.Codigo = BeStockEnFecha.Codigo _
                                                             AndAlso x.Lic_Plate = BeStockEnFecha.Lic_Plate _
                                                             AndAlso x.IdPropietarioBodega = BeStockEnFecha.IdPropietarioBodega _
                                                             AndAlso x.IdBodegaOrigen = BeStockEnFecha.IdBodegaOrigen)
                        End If

                    End If

                    If Idx = -1 Then
                        RepMovEnUnaFecha.Add(BeStockEnFecha)
                    Else
                        BeStockEnFecha = RepMovEnUnaFecha(Idx) 'Puntero =>
                    End If

                    If ObjM.TipoTarea = clsDataContractDI.tTipoTarea.INVE Then
                        BeStockEnFecha.Inventario_Inicial += ObjM.Cantidad
                    ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.RECE Then
                        BeStockEnFecha.Ingresos += ObjM.Cantidad
                    ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTP OrElse ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTPI Then
                        BeStockEnFecha.Ajuste_Positivo += ObjM.Cantidad
                    ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTN OrElse ObjM.TipoTarea = clsDataContractDI.tTipoTarea.AJCANTNI Then
                        BeStockEnFecha.Ajuste_Negativo += ObjM.Cantidad
                    ElseIf ObjM.TipoTarea = clsDataContractDI.tTipoTarea.DESP Then
                        BeStockEnFecha.Salidas += ObjM.Cantidad
                    Else
                        Debug.Print(ObjM.TipoTarea)
                    End If

                    Debug.Print(ObjM.TipoTarea)

                    Application.DoEvents()

                Next

                Normalizar_Movimientos = RepMovEnUnaFecha

            End If

        Catch ex As Exception
            Throw ex
        Finally
            prg.Visible = False
        End Try

    End Function

    Public Shared Function Get_VW_Stock_Jornada(ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As DataTable

        Get_VW_Stock_Jornada = Nothing

        Try

            Dim vSQL As String = " select * 
								   FROM vw_stock_jornada "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandTimeout = 120

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_VW_Stock_Jornada = lDataTable
                End If

            End Using

        Catch ex As Exception
            Throw ex
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function


    Public Shared Function Get_VW_Stock_Jornada_By_IdStock(ByVal pIdStock As Integer,
                                                           ByVal lConnection As SqlConnection,
                                                           ByVal lTransaction As SqlTransaction) As DataTable

        Get_VW_Stock_Jornada_By_IdStock = Nothing

        Try

            Dim vSQL As String = " select * 
								   FROM vw_stock_jornada where IdStock = @pIdStock "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@pIdStock", pIdStock)
                'lDTA.SelectCommand.CommandTimeout = 120

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_VW_Stock_Jornada_By_IdStock = lDataTable
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#GT28122022_1020: se llena pero nunca se usa la lista...
    'Public Shared Property lJornada As New List(Of clsBeStock_jornada)

    Private Shared Function Insertar_Stock_Jornada(ByVal DTVWStockJornada As DataTable,
                                                   ByVal pFechaJornada As Date,
                                                   ByRef pListaIngresosYSalidasDelDia As List(Of clsBeStockEnUnaFecha),
                                                   ByVal pUltimaJornada As clsBeJornada_sistema,
                                                   ByVal pIdEmpresa As Integer,
                                                   ByVal pIdBodega As Integer,
                                                   ByVal pIdUsuario As Integer,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction,
                                                   ByVal lblprg As RichTextBox,
                                                   ByRef prg As ProgressBar,
                                                   ByVal pParentForm As Form,
                                                   Optional ByVal ValidarRetroactivo As Boolean = True,
                                                   Optional ByVal pJornadaActual As clsBeJornada_sistema = Nothing) As Boolean

        Insertar_Stock_Jornada = False


        Dim vContador As Integer = 0
        Dim vSingularidadStock As New List(Of clsBeStockEnUnaFecha)
        Dim lTicketsProcesados As New Dictionary(Of Integer, Boolean)
        Dim lLicenciasProcesadas As New List(Of clsBeLicenciaJornada)
        Dim lBeStockDet As New List(Of clsBeStock_det)
        Dim lBeStockDetUltimaJornada As New DataTable
        Dim lTicketProcesados As New List(Of String)
        Dim lCambiosUbicacionYEstado As New List(Of clsBeVW_Movimientos)
        Dim vSesionTrace As String = Date.Now.ToString("yyyyMMddHHmmssfff") & "-" & Guid.NewGuid().ToString("N").Substring(0, 8)
        Dim vInicioTrace As DateTime = Date.Now
        Dim vPasoTrace As DateTime = vInicioTrace
        Dim vMsServidor As Long = 0
        Dim vMsMaxId As Long = 0
        Dim vMsStockDet As Long = 0
        Dim vMsBodega As Long = 0
        Dim vMsMapeo As Long = 0
        Dim vMsSingularidad As Long = 0
        Dim vMsTicket As Long = 0
        Dim vMsLicencia As Long = 0
        Dim vMsHash As Long = 0
        Dim vMsRetroactivo As Long = 0
        Dim vMsDedupe As Long = 0
        Dim vMsInsertNormal As Long = 0
        Dim vMsInsertRetro As Long = 0
        Dim vMsUi As Long = 0
        Dim vMsCierreTickets As Long = 0
        Dim vMsCierreLicencias As Long = 0
        Dim vCntInsertNormal As Integer = 0
        Dim vCntInsertRetro As Integer = 0
        Dim vCntOmitidosDuplicado As Integer = 0
        Dim vCntRetroIntentos As Integer = 0
        Dim vCntTicketQuery As Integer = 0
        Dim vCntLicenciaQuery As Integer = 0
        Dim vLoteInsert As New List(Of clsBeStock_jornada)
        Dim vLoteTam As Integer = 250
        Dim vPreload As New InvJornadaPreloadCtx()

        Try

            JornadaStockTrace(vSesionTrace, "INSERTAR_STOCK_JORNADA_START", vInicioTrace,
                              "Rows=" & If(DTVWStockJornada Is Nothing, 0, DTVWStockJornada.Rows.Count) &
                              ";FechaJornada=" & pFechaJornada.ToString("yyyy-MM-dd") &
                              ";IdBodega=" & pIdBodega &
                              ";ValidarRetroactivo=" & ValidarRetroactivo.ToString())

            vPasoTrace = Date.Now
            Dim fecha_hoy = clsServidor.Get_Fecha_Servidor(lConnection, lTransaction)
            vMsServidor = CLng((Date.Now - vPasoTrace).TotalMilliseconds)

            If DTVWStockJornada IsNot Nothing AndAlso DTVWStockJornada.Rows.Count > 0 Then

                Dim BeStockJornadaExistente As New clsBeStock_jornada
                Dim BeStockJornadaDesfase As New clsBeStock_jornada
                Dim BeStockDet As New clsBeStock_det()
                Dim vIdStockJornada As Integer = clsLnStock_jornada.MaxID(lConnection, lTransaction) + 1
                Dim vFechaDiaAnterior As Date = fecha_hoy.AddDays(-1)
                Dim vDifDiasIngreso As Integer = 0
                Dim vDiasPrevios As Integer = 0
                Dim vExisteRetroactivoDia As Boolean = False
                Dim vTicketTMSProcesado As Boolean = False
                Dim vLicenciaProcesada As Boolean = False
                Dim vIndiceDocumentoIngreso As Integer = -1
                Dim vCostoUnitarioLinea As Double = 0
                Dim vIdOrdenCompraEnc As Integer = 0
                Dim vIdRecepcionDet As Integer = 0
                Dim vCostoUnitario As Double = 0
                Dim vCostoTotal As Double = 0
                Dim vCantidadInicial As Double = 0
                Dim BeTMSTicket As New clsBeTms_ticket
                Dim vVerificarExistenciaStock As Boolean = False
                Dim pBeBodega As New clsBeBodega

                vPasoTrace = Date.Now
                lblprg.AppendText("Se encontraron " & DTVWStockJornada.Rows.Count & " registros de stock para jornada")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                prg.Visible = True : prg.Value = 0 : prg.Maximum = DTVWStockJornada.Rows.Count
                vMsUi += CLng((Date.Now - vPasoTrace).TotalMilliseconds)

                vPasoTrace = Date.Now
                lBeStockDet = New List(Of clsBeStock_det)
                lBeStockDet = clsLnStock_det.Get_All(lConnection,
                                                     lTransaction)
                vMsStockDet = CLng((Date.Now - vPasoTrace).TotalMilliseconds)

                Dim vIdJornada As Integer = 0

                If pJornadaActual Is Nothing Then

                    If pUltimaJornada Is Nothing Then
                        pUltimaJornada = Get_Ultima_Jornada(lConnection, lTransaction)
                        vIdJornada = pUltimaJornada.IdJornada
                        'vVerificarExistenciaStock = True
                    Else
                        vIdJornada = pUltimaJornada.IdJornada
                    End If

                Else
                    vIdJornada = pJornadaActual.IdJornada
                End If

                '//#GT20122022_1555: validar la existencia previa del registro, porque el proceo de stock jornada
                'se ejecuta desde varias llamadas, sea por desfase, retroactivo o por error en tiempo de espera.
                vVerificarExistenciaStock = True
                'lJornada.Clear()

                Dim BeStockJornadaExistenteFechaAnt As New clsBeStock_jornada
                Dim BeStockJornadaExistentePosteriorRetroactivo As New clsBeStock_jornada
                Dim vMensaje As String = ""

                Dim registro_ As Integer = 0


                vPasoTrace = Date.Now
                pBeBodega = clsLnBodega.GetSingle_By_Idbodega(pIdBodega, lConnection, lTransaction)
                vMsBodega = CLng((Date.Now - vPasoTrace).TotalMilliseconds)

                Dim vFechaDesdePreload As Date = pFechaJornada.Date
                Dim vFechaHastaPreload As Date = fecha_hoy.Date
                For Each rPre As DataRow In DTVWStockJornada.Rows
                    Dim vFechaTicketObj As Object = InvJornadaGetRowValue(rPre, "Fecha_Ingreso_Ticket_TMS", "fecha_ingreso_ticket_tms")
                    Dim vFechaIngresoObj As Object = InvJornadaGetRowValue(rPre, "Fecha_ingreso", "fecha_ingreso")
                    Dim vFechaTicket As Date = If(vFechaTicketObj Is Nothing OrElse IsDBNull(vFechaTicketObj), pFechaJornada.Date, CDate(vFechaTicketObj).Date)
                    Dim vFechaIngreso As Date = If(vFechaIngresoObj Is Nothing OrElse IsDBNull(vFechaIngresoObj), pFechaJornada.Date, CDate(vFechaIngresoObj).Date)
                    If vFechaTicket < vFechaDesdePreload Then vFechaDesdePreload = vFechaTicket
                    If vFechaIngreso < vFechaDesdePreload Then vFechaDesdePreload = vFechaIngreso
                Next

                vPreload = InvJornadaTryPreloadCtx(DTVWStockJornada,
                                                   vFechaDesdePreload,
                                                   vFechaHastaPreload,
                                                   lConnection,
                                                   lTransaction,
                                                   vSesionTrace,
                                                   vInicioTrace)


                For Each R As DataRow In DTVWStockJornada.Rows


                    'Dim vStopwatch2 As Stopwatch = Stopwatch.StartNew()

                    vPasoTrace = Date.Now
                    vIdOrdenCompraEnc = IIf(IsDBNull(R.Item("IdOrdenCompraEnc")), 0, R.Item("IdOrdenCompraEnc"))

                    If vIndiceDocumentoIngreso = -1 Then

                        Dim vCalcularCostoUnitario As Boolean = False


                        If vCalcularCostoUnitario Then

                            vCostoUnitarioLinea = clsLnTrans_oc_det.Get_Costo_Unitario_By_IdOrdenCompraEnc_And_IdRecepcionDet(vIdOrdenCompraEnc,
                                                                                                                              vIdRecepcionDet,
                                                                                                                              lConnection,
                                                                                                                              lTransaction)

                        End If

                        vCostoUnitario = vCostoUnitarioLinea

                    End If

                    BeStockJornadaExistente = New clsBeStock_jornada()
                    BeStockJornadaExistente.IdStockJornada = vIdStockJornada
                    BeStockJornadaExistente.IdJornadaSistema = vIdJornada
                    BeStockJornadaExistente.Fecha = pFechaJornada
                    BeStockJornadaExistente.IdBodega = IIf(IsDBNull(R.Item("IdBodega")), 0, R.Item("IdBodega"))
                    BeStockJornadaExistente.IdStock = IIf(IsDBNull(R.Item("IdStock")), 0, R.Item("IdStock"))
                    BeStockJornadaExistente.IdPropietarioBodega = IIf(IsDBNull(R.Item("IdPropietarioBodega")), 0, R.Item("IdPropietarioBodega"))
                    BeStockJornadaExistente.IdProductoBodega = IIf(IsDBNull(R.Item("IdProductoBodega")), 0, R.Item("IdProductoBodega"))
                    BeStockJornadaExistente.IdProductoEstado = IIf(IsDBNull(R.Item("IdProductoEstado")), 0, R.Item("IdProductoEstado"))
                    BeStockJornadaExistente.IdPresentacion = IIf(IsDBNull(R.Item("IdPresentacion")), Nothing, R.Item("IdPresentacion"))
                    BeStockJornadaExistente.IdUnidadMedida = IIf(IsDBNull(R.Item("IdUnidadMedida")), 0, R.Item("IdUnidadMedida"))
                    BeStockJornadaExistente.IdUbicacion = IIf(IsDBNull(R.Item("IdUbicacion")), 0, R.Item("IdUbicacion"))
                    BeStockJornadaExistente.IdUbicacion_anterior = IIf(IsDBNull(R.Item("IdUbicacion_anterior")), 0, R.Item("IdUbicacion_anterior"))
                    BeStockJornadaExistente.IdRecepcionEnc = IIf(IsDBNull(R.Item("IdRecepcionEnc")), 0, R.Item("IdRecepcionEnc"))
                    BeStockJornadaExistente.IdRecepcionDet = IIf(IsDBNull(R.Item("IdRecepcionDet")), 0, R.Item("IdRecepcionDet"))
                    BeStockJornadaExistente.IdPedidoEnc = IIf(IsDBNull(R.Item("IdPedidoEnc")), 0, R.Item("IdPedidoEnc"))
                    BeStockJornadaExistente.IdPickingEnc = IIf(IsDBNull(R.Item("IdPickingEnc")), 0, R.Item("IdPickingEnc"))
                    BeStockJornadaExistente.IdDespachoEnc = IIf(IsDBNull(R.Item("IdDespachoEnc")), 0, R.Item("IdDespachoEnc"))
                    BeStockJornadaExistente.Lote = IIf(IsDBNull(R.Item("lote")), "", R.Item("lote"))
                    BeStockJornadaExistente.Lic_plate = IIf(IsDBNull(R.Item("lic_plate")), "", R.Item("lic_plate"))
                    BeStockJornadaExistente.Serial = IIf(IsDBNull(R.Item("serial")), "", R.Item("serial"))
                    BeStockJornadaExistente.Cantidad = IIf(IsDBNull(R.Item("cantidad")), 0, R.Item("cantidad"))
                    BeStockJornadaExistente.Fecha_ingreso = IIf(IsDBNull(R.Item("fecha_ingreso")), "01/01/1900", R.Item("fecha_ingreso"))
                    BeStockJornadaExistente.Fecha_vence = IIf(IsDBNull(R.Item("fecha_vence")), "01/01/1900", R.Item("fecha_vence"))
                    BeStockJornadaExistente.Uds_lic_plate = IIf(IsDBNull(R.Item("uds_lic_plate")), 0, R.Item("uds_lic_plate"))
                    BeStockJornadaExistente.No_bulto = IIf(IsDBNull(R.Item("no_bulto")), 0, R.Item("no_bulto"))
                    BeStockJornadaExistente.Fecha_manufactura = IIf(IsDBNull(R.Item("fecha_manufactura")), "01/01/1900", R.Item("fecha_manufactura"))
                    BeStockJornadaExistente.Añada = IIf(IsDBNull(R.Item("añada")), 0, R.Item("añada"))
                    BeStockJornadaExistente.User_agr = IIf(IsDBNull(R.Item("user_agr")), "", R.Item("user_agr"))
                    BeStockJornadaExistente.Fec_agr = IIf(IsDBNull(R.Item("fec_agr")), "01/01/1900", R.Item("fec_agr"))
                    BeStockJornadaExistente.User_mod = IIf(IsDBNull(R.Item("user_mod")), "", R.Item("user_mod"))
                    BeStockJornadaExistente.Fec_mod = IIf(IsDBNull(R.Item("fec_mod")), "01/01/1900", R.Item("fec_mod"))
                    BeStockJornadaExistente.Activo = IIf(IsDBNull(R.Item("activo")), False, R.Item("activo"))
                    BeStockJornadaExistente.Peso = IIf(IsDBNull(R.Item("peso")), 0, R.Item("peso"))
                    BeStockJornadaExistente.Temperatura = IIf(IsDBNull(R.Item("temperatura")), 0, R.Item("temperatura"))
                    BeStockJornadaExistente.Atributo_variante_1 = IIf(IsDBNull(R.Item("atributo_variante_1")), Nothing, R.Item("atributo_variante_1"))
                    BeStockJornadaExistente.Pallet_no_estandar = IIf(IsDBNull(R.Item("pallet_no_estandar")), False, R.Item("pallet_no_estandar"))
                    BeStockJornadaExistente.Propietario = IIf(IsDBNull(R.Item("Propietario")), "", R.Item("Propietario"))
                    BeStockJornadaExistente.Proveedor = IIf(IsDBNull(R.Item("Proveedor")), "", R.Item("Proveedor"))
                    BeStockJornadaExistente.Bodega = IIf(IsDBNull(R.Item("Bodega")), "", R.Item("Bodega"))
                    BeStockJornadaExistente.IdOrdenCompraEnc = IIf(IsDBNull(R.Item("IdOrdenCompraEnc")), 0, R.Item("IdOrdenCompraEnc"))
                    BeStockJornadaExistente.No_DocumentoOC = IIf(IsDBNull(R.Item("No_DocumentoOC")), "", R.Item("No_DocumentoOC"))
                    BeStockJornadaExistente.No_DocumentoRec = IIf(IsDBNull(R.Item("No_DocumentoRec")), "", R.Item("No_DocumentoRec"))
                    BeStockJornadaExistente.ReferenciaOC = IIf(IsDBNull(R.Item("ReferenciaOC")), "", R.Item("ReferenciaOC"))
                    BeStockJornadaExistente.Fecha_Recepcion = IIf(IsDBNull(R.Item("Fecha_Recepcion")), New Date(1900, 1, 1), R.Item("Fecha_Recepcion"))
                    BeStockJornadaExistente.TipoTrans = IIf(IsDBNull(R.Item("TipoTrans")), "", R.Item("TipoTrans"))
                    BeStockJornadaExistente.Fecha_Agrego = IIf(IsDBNull(R.Item("Fecha_Agrego")), New Date(1900, 1, 1), R.Item("Fecha_Agrego"))
                    BeStockJornadaExistente.Codigo_producto = IIf(IsDBNull(R.Item("codigo_producto")), "", R.Item("codigo_producto"))
                    BeStockJornadaExistente.Codigo_barra_producto = IIf(IsDBNull(R.Item("codigo_barra_producto")), "", R.Item("codigo_barra_producto"))
                    BeStockJornadaExistente.Nombre_producto = IIf(IsDBNull(R.Item("nombre_producto")), "", R.Item("nombre_producto"))
                    BeStockJornadaExistente.Existencia = IIf(IsDBNull(R.Item("existencia")), 0, R.Item("existencia"))
                    BeStockJornadaExistente.Nom_umBas = IIf(IsDBNull(R.Item("nom_umBas")), "", R.Item("nom_umBas"))
                    BeStockJornadaExistente.Nom_estado_producto = IIf(IsDBNull(R.Item("nom_estado_producto")), "", R.Item("nom_estado_producto"))
                    BeStockJornadaExistente.Ubicacion_origen = IIf(IsDBNull(R.Item("ubicacion_origen")), "", R.Item("ubicacion_origen"))
                    BeStockJornadaExistente.No_poliza = IIf(IsDBNull(R.Item("no_poliza")), "", R.Item("no_poliza"))

                    '#EJC20210604: Valores unitarios en stock_jornada
                    'https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/54/
                    BeStockJornadaExistente.Valor_aduana = IIf(IsDBNull(R.Item("valor_aduana")), 0, R.Item("valor_aduana"))
                    BeStockJornadaExistente.Valor_fob = IIf(IsDBNull(R.Item("valor_fob")), 0, R.Item("valor_fob"))
                    BeStockJornadaExistente.Valor_iva = IIf(IsDBNull(R.Item("valor_iva")), 0, R.Item("valor_iva"))
                    BeStockJornadaExistente.Valor_dai = IIf(IsDBNull(R.Item("valor_dai")), 0, R.Item("valor_dai"))
                    BeStockJornadaExistente.Valor_seguro = IIf(IsDBNull(R.Item("valor_seguro")), 0, R.Item("valor_seguro"))
                    BeStockJornadaExistente.Costo_Unitario = vCostoUnitario
                    BeStockJornadaExistente.Peso_neto = IIf(IsDBNull(R.Item("peso_neto")), 0, R.Item("peso_neto"))
                    BeStockJornadaExistente.Numero_orden = IIf(IsDBNull(R.Item("numero_orden")), "", R.Item("numero_orden"))
                    BeStockJornadaExistente.Codigo_regimen = IIf(IsDBNull(R.Item("codigo_regimen")), 0, R.Item("codigo_regimen"))
                    BeStockJornadaExistente.Nombre_regimen = IIf(IsDBNull(R.Item("nombre_regimen")), "", R.Item("nombre_regimen"))
                    BeStockJornadaExistente.Dias_vencimiento_regimen = IIf(IsDBNull(R.Item("dias_vencimiento_regimen")), 0, R.Item("dias_vencimiento_regimen"))
                    BeStockJornadaExistente.Factor = IIf(IsDBNull(R.Item("Factor")), 0, R.Item("Factor"))
                    BeStockJornadaExistente.CamasPorTarima = IIf(IsDBNull(R.Item("CamasPorTarima")), 0, R.Item("CamasPorTarima"))
                    BeStockJornadaExistente.CajasPorCama = IIf(IsDBNull(R.Item("CajasPorCama")), 0, R.Item("CajasPorCama"))
                    BeStockJornadaExistente.Fecha_Ingreso_Ticket_TMS = IIf(IsDBNull(R.Item("Fecha_Ingreso_Ticket_TMS")), BeStockJornadaExistente.Fecha_Recepcion, R.Item("Fecha_Ingreso_Ticket_TMS"))
                    BeStockJornadaExistente.IdTicketTMS = Val(IIf(IsDBNull(R.Item("IdTicketTMS")), 0, R.Item("IdTicketTMS")))
                    BeStockJornadaExistente.IdPropietario = Val(IIf(IsDBNull(R.Item("IdPropietario")), 0, R.Item("IdPropietario")))
                    BeStockJornadaExistente.IdClasificacion = Val(IIf(IsDBNull(R.Item("IdClasificacion")), 0, R.Item("IdClasificacion")))
                    BeStockJornadaExistente.Clasificacion = IIf(IsDBNull(R.Item("Clasificacion")), "", R.Item("Clasificacion"))
                    BeStockJornadaExistente.Regimen = IIf(IsDBNull(R.Item("Regimen")), "", R.Item("Regimen"))
                    BeStockJornadaExistente.Nom_presentacion_producto = IIf(IsDBNull(R.Item("Presentacion_Producto")), "", R.Item("Presentacion_Producto"))

                    '#GT10092025: campos de talla color
                    BeStockJornadaExistente.IdProductoTallaColor = IIf(IsDBNull(R.Item("IdProductoTallaColor")), 0, R.Item("IdProductoTallaColor"))
                    BeStockJornadaExistente.Talla = IIf(IsDBNull(R.Item("Talla")), "", R.Item("Talla"))
                    BeStockJornadaExistente.Color = IIf(IsDBNull(R.Item("Color")), "", R.Item("Color"))
                    vMsMapeo += CLng((Date.Now - vPasoTrace).TotalMilliseconds)

                    '#EJC20210618: Optimizado, se obtiene la lista completo primero.-
                    'GT 020820211028: En la lista completa, se valida que el id stock coincida con el detalle, para traer las posiciones ocupadas.
                    vPasoTrace = Date.Now
                    BeStockDet = lBeStockDet.Find(Function(x) x.IdStock = BeStockJornadaExistente.IdStock)
                    '#EJC20210603:Posiciones cealsa.
                    'BeStockDet = clsLnStock_det.GetSingle(BeStockJornada.IdStock, lConnection, lTransaction)
                    If Not BeStockDet Is Nothing Then
                        BeStockJornadaExistente.Posiciones = BeStockDet.Posiciones
                    Else
                        BeStockJornadaExistente.Posiciones = 1
                    End If
                    vMsStockDet += CLng((Date.Now - vPasoTrace).TotalMilliseconds)

                    Dim tickettms As New clsBeTms_ticket()

                    If Not pListaIngresosYSalidasDelDia Is Nothing Then

                        vPasoTrace = Date.Now
                        '#EJC20210519: Buscar en la lista si este producto existe con estos criterios
                        vSingularidadStock = pListaIngresosYSalidasDelDia.FindAll(Function(x) x.IdProductoBodega = BeStockJornadaExistente.IdProductoBodega _
                                                                              AndAlso x.IdPropietarioBodega = BeStockJornadaExistente.IdPropietarioBodega _
                                                                              AndAlso x.Lote = BeStockJornadaExistente.Lote _
                                                                              AndAlso x.Lic_Plate = BeStockJornadaExistente.Lic_plate _
                                                                              AndAlso x.IdUbicacionOrigen = BeStockJornadaExistente.IdUnidadMedida _
                                                                              AndAlso x.IdPresentacion = BeStockJornadaExistente.IdPresentacion)

                        'Si encuentra registros que coincidan sumarón al stock de la jornada el total que ingresí.
                        For Each vs In vSingularidadStock
                            BeStockJornadaExistente.Cantidad_Ingreso_Afecta_A_salida += vs.Ingresos
                        Next
                        vMsSingularidad += CLng((Date.Now - vPasoTrace).TotalMilliseconds)

                    End If

                    '#GT03012023_2100: solo se asigna cuando es retroactivo
                    vPasoTrace = Date.Now
                    Dim Hash As String = clsBeStock_jornada.GetRecordHash(BeStockJornadaExistente)
                    BeStockJornadaExistente.Stock_Jornada_Hash = ""
                    vMsHash += CLng((Date.Now - vPasoTrace).TotalMilliseconds)

#Region "Retroactivo"

                    '#GT03012023_1430: retroactivo para stock con ticket ****
                    vTicketTMSProcesado = False
                    '#GT21112022_1200: valido que ticket tenga un valor, sino, llenara la lista lTicketsProcesados con valores default que no sirven
                    If BeStockJornadaExistente.IdTicketTMS <> 0 Then
                        If lTicketsProcesados.Count > 0 Then
                            If Not lTicketsProcesados.ContainsKey(BeStockJornadaExistente.IdTicketTMS) Then
                                '#CKFK 20210523 Puse esto en comentario porque daba error ya que el ticket no existe en la lista lTicketsProcesados y daba error
                                ' Dim num As Boolean = lTicketsProcesados.Item(BeStockJornada.IdTicketTMS)
                                If vPreload.Activo Then
                                    vTicketTMSProcesado = vPreload.TicketsProcesados.Contains(BeStockJornadaExistente.IdTicketTMS)
                                Else
                                    vPasoTrace = Date.Now
                                    vTicketTMSProcesado = clsLnTms_ticket.Ticket_Procesado_Stock_Jornada(BeStockJornadaExistente.IdTicketTMS,
                                                                                                         lConnection,
                                                                                                         lTransaction)
                                    vMsTicket += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                                    vCntTicketQuery += 1
                                End If
                                '#GT26122022_1600: no se requieren ticket ya cerrados.
                                If Not vTicketTMSProcesado Then
                                    lTicketsProcesados.Add(BeStockJornadaExistente.IdTicketTMS, vTicketTMSProcesado)
                                End If
                            Else
                                Dim num As Boolean = lTicketsProcesados.Item(BeStockJornadaExistente.IdTicketTMS)
                                vTicketTMSProcesado = lTicketsProcesados.Item(BeStockJornadaExistente.IdTicketTMS)
                            End If
                        Else
                            If vPreload.Activo Then
                                vTicketTMSProcesado = vPreload.TicketsProcesados.Contains(BeStockJornadaExistente.IdTicketTMS)
                            Else
                                vPasoTrace = Date.Now
                                vTicketTMSProcesado = clsLnTms_ticket.Ticket_Procesado_Stock_Jornada(BeStockJornadaExistente.IdTicketTMS,
                                                                                                     lConnection,
                                                                                                     lTransaction)
                                vMsTicket += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                                vCntTicketQuery += 1
                            End If
                            '#GT26122022_1600: no se requieren ticket ya cerrados.
                            If Not vTicketTMSProcesado Then
                                lTicketsProcesados.Add(BeStockJornadaExistente.IdTicketTMS, vTicketTMSProcesado)
                            End If
                        End If

                    End If


                    '#GT16022023: llenamos lista con las LP de las recepciones que aplican a retroactivo para cerrarlas y evitar que otro dia
                    'ejecuten retroactivo nuevamente, similar al proceso de cerrar ticket
                    vLicenciaProcesada = False

                    Dim vIndiceLicenciaExistente As Integer = -1

                    If lLicenciasProcesadas.Count > 0 Then

                        vIndiceLicenciaExistente = lLicenciasProcesadas.FindIndex(Function(x) x.Licencia = BeStockJornadaExistente.Lic_plate _
                                                                                  AndAlso x.IdRecepcionEnc = BeStockJornadaExistente.IdRecepcionEnc _
                                                                                  AndAlso x.IdRecepcionDet = BeStockJornadaExistente.IdRecepcionDet)

                        If vIndiceLicenciaExistente = -1 Then

                            If vPreload.Activo Then
                                vLicenciaProcesada = vPreload.LicenciasProcesadas.Contains(InvJornadaKeyLic(BeStockJornadaExistente.Lic_plate,
                                                                                                                BeStockJornadaExistente.IdRecepcionEnc,
                                                                                                                BeStockJornadaExistente.IdRecepcionDet))
                            Else
                                vPasoTrace = Date.Now
                                vLicenciaProcesada = clsLnTrans_re_det.Licencia_Procesada_Stock_Jornada(BeStockJornadaExistente.Lic_plate,
                                                                                                        BeStockJornadaExistente.IdRecepcionEnc,
                                                                                                        BeStockJornadaExistente.IdRecepcionDet,
                                                                                                        lConnection,
                                                                                                        lTransaction)
                                vMsLicencia += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                                vCntLicenciaQuery += 1
                            End If

                            '#GT26122022_1600: no se requieren ticket ya cerrados.
                            If Not vLicenciaProcesada Then

                                Dim BeLicenciaJornada As New clsBeLicenciaJornada
                                BeLicenciaJornada.Licencia = BeStockJornadaExistente.Lic_plate
                                BeLicenciaJornada.IdRecepcionEnc = BeStockJornadaExistente.IdRecepcionEnc
                                BeLicenciaJornada.IdRecepcionDet = BeStockJornadaExistente.IdRecepcionDet
                                BeLicenciaJornada.IdJornadaSistema = BeStockJornadaExistente.IdJornadaSistema
                                lLicenciasProcesadas.Add(BeLicenciaJornada)

                            End If

                        Else
                            vLicenciaProcesada = True
                        End If

                    Else

                        If vPreload.Activo Then
                            vLicenciaProcesada = vPreload.LicenciasProcesadas.Contains(InvJornadaKeyLic(BeStockJornadaExistente.Lic_plate,
                                                                                                            BeStockJornadaExistente.IdRecepcionEnc,
                                                                                                            BeStockJornadaExistente.IdRecepcionDet))
                        Else
                            vPasoTrace = Date.Now
                            vLicenciaProcesada = clsLnTrans_re_det.Licencia_Procesada_Stock_Jornada(BeStockJornadaExistente.Lic_plate,
                                                                                                    BeStockJornadaExistente.IdRecepcionEnc,
                                                                                                    BeStockJornadaExistente.IdRecepcionDet,
                                                                                                    lConnection,
                                                                                                    lTransaction)
                            vMsLicencia += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                            vCntLicenciaQuery += 1
                        End If
                        '#GT26122022_1600: no se requieren licencias ya cerradas.
                        If Not vLicenciaProcesada Then
                            Dim BeLicenciaJornada As New clsBeLicenciaJornada
                            BeLicenciaJornada.Licencia = BeStockJornadaExistente.Lic_plate
                            BeLicenciaJornada.IdRecepcionEnc = BeStockJornadaExistente.IdRecepcionEnc
                            BeLicenciaJornada.IdRecepcionDet = BeStockJornadaExistente.IdRecepcionDet
                            BeLicenciaJornada.IdJornadaSistema = BeStockJornadaExistente.IdJornadaSistema
                            lLicenciasProcesadas.Add(BeLicenciaJornada)
                        End If

                    End If


                    Debug.Print("IdStock: " & BeStockJornadaExistente.IdStock)
                    Debug.Print("LP: " & BeStockJornadaExistente.Lic_plate)

                    If ValidarRetroactivo AndAlso BeStockJornadaExistente.IdTicketTMS <> 0 Then
                        vPasoTrace = Date.Now
                        '#EJC20210521: Si el ticket ya fue procesado, no volver a escribir los días en el retroactivo.
                        '#GT12012023: Aunque el ticket ya este procesado validamos, con maximo 10 dias atras, para no cargar todo.
                        ''If Not vTicketTMSProcesado Then

                        '#GT16022023: como el ticket ya no es bandera para evitar volver a escribir los dias en retroactivo
                        'se pasa la validación a la LP de la recepción, porque esta se puede mantener abierta varios días
                        If Not vLicenciaProcesada Then

                            vDifDiasIngreso = 0
                            vDifDiasIngreso = DateDiff(DateInterval.Day,
                                                       BeStockJornadaExistente.Fecha_Ingreso_Ticket_TMS.Date,
                                                       fecha_hoy.Date)

                            If vDifDiasIngreso > 0 AndAlso vDifDiasIngreso <= pBeBodega.Dias_Limite_Retroactivo Then

                                Dim vDiaRetroaActivo As Date = BeStockJornadaExistente.Fecha_Ingreso_Ticket_TMS.Date

                                If vPreload.Activo Then
                                    vExisteRetroactivoDia = vPreload.HashRetroExistente.Contains(InvJornadaKeyHashFecha(Hash, vDiaRetroaActivo))
                                Else
                                    vExisteRetroactivoDia = clsLnStock_jornada.Existe_Hash_Retroactivo_By_Fecha(Hash,
                                                                                                                vDiaRetroaActivo,
                                                                                                                lConnection,
                                                                                                                lTransaction)
                                End If

                                If Not vExisteRetroactivoDia Then

                                    Dim BeStockJornadaRetroActiva As New clsBeStock_jornada()

                                    For i = 0 To vDifDiasIngreso - 1

                                        Dim vExisteJornadaDia As Boolean = False
                                        If vPreload.Activo Then
                                            vExisteJornadaDia = vPreload.JornadasExistentes.Contains(InvJornadaFechaKey(vDiaRetroaActivo))
                                        Else
                                            vExisteJornadaDia = Existe_Jornada(vDiaRetroaActivo, lConnection, lTransaction)
                                        End If

                                        If Not vExisteJornadaDia Then

                                            Dim BeJornadaSistemaDiaFaltante As New clsBeJornada_sistema()
                                            BeJornadaSistemaDiaFaltante = Inserta_Jornada(pIdEmpresa,
                                                                                          pIdUsuario,
                                                                                          vDiaRetroaActivo,
                                                                                          lConnection,
                                                                                          lTransaction)

                                            If Not BeJornadaSistemaDiaFaltante Is Nothing Then
                                                BeStockJornadaRetroActiva = New clsBeStock_jornada()
                                                clsPublic.CopyObject(BeJornadaSistemaDiaFaltante, BeStockJornadaRetroActiva)
                                                If vPreload.Activo Then vPreload.JornadasExistentes.Add(InvJornadaFechaKey(vDiaRetroaActivo))
                                            End If

                                        Else

                                            BeStockJornadaRetroActiva = New clsBeStock_jornada()
                                            clsPublic.CopyObject(BeStockJornadaExistente, BeStockJornadaRetroActiva)
                                            BeStockJornadaRetroActiva.Fecha = vDiaRetroaActivo
                                            BeStockJornadaRetroActiva.Es_Retroactivo = True
                                            BeStockJornadaRetroActiva.Stock_Jornada_Hash = Hash
                                            vIdStockJornada += 1
                                            BeStockJornadaRetroActiva.IdStockJornada = vIdStockJornada
                                            Debug.Print("Iteracion_retroactivo: " & i)

                                            '#GT28122022_0800: no le encontró uso a la lista.
                                            'lJornada.Add(BeStockJornadaRetroActiva)

                                            'lCambiosUbicacionYEstado = clsLnVW_Movimientos.Get_All_Ubic_Y_Cest_By_Fecha_And_LicPlate(BeStockJornadaExistente.Fecha_Ingreso_Ticket_TMS.Date,
                                            '                                                                                         fecha_hoy.Date,
                                            '                                                                                         BeStockJornadaRetroActiva.Lic_plate,
                                            '                                                                                         lConnection,
                                            '                                                                                         lTransaction)


                                            'Dim vSingleCambiosUbicacionYEstado1 As New List(Of clsBeVW_Movimientos)
                                            'vSingleCambiosUbicacionYEstado1 = lCambiosUbicacionYEstado.FindAll(Function(x) x.Fecha.Date = vDiaRetroaActivo.Date).ToList()

                                            'Dim vBeStockRec As New clsBeStock_rec
                                            'vBeStockRec = clsLnStock_rec.Get_Single_By_IdRecepcionEnc_And_Licencia(BeStockJornadaRetroActiva.IdRecepcionEnc,
                                            '                                                                       BeStockJornadaRetroActiva.Lic_plate,
                                            '                                                                       lConnection,
                                            '                                                                       lTransaction)

                                            'If pListaIngresosYSalidasDelDia Is Nothing Then
                                            '    If Not vBeStockRec Is Nothing Then
                                            '        BeStockJornadaRetroActiva.Cantidad = vBeStockRec.Cantidad
                                            '    End If
                                            'End If

                                            '#GT28122022_1500: validar que no exista el stock con la fecha del retroactivo
                                            Dim BeStockJornadaExistente_ As New clsBeStock_jornada

                                            Dim vExisteStockRetro As Boolean = False
                                            Dim vKeyStockRetro As String = InvJornadaKeyStock(BeStockJornadaRetroActiva.Lic_plate,
                                                                                              vDiaRetroaActivo,
                                                                                              BeStockJornadaRetroActiva.IdPropietarioBodega,
                                                                                              BeStockJornadaRetroActiva.IdProductoBodega,
                                                                                              BeStockJornadaRetroActiva.IdStock)
                                            If vPreload.Activo Then
                                                vExisteStockRetro = vPreload.StockExistente.Contains(vKeyStockRetro)
                                            Else
                                                BeStockJornadaExistente_ = clsLnStock_jornada.Get_Single_By_IdStock(BeStockJornadaRetroActiva.Lic_plate,
                                                                                                                    vDiaRetroaActivo,
                                                                                                                    BeStockJornadaRetroActiva.IdPropietarioBodega,
                                                                                                                    BeStockJornadaRetroActiva.IdProductoBodega,
                                                                                                                    BeStockJornadaRetroActiva.IdStock,
                                                                                                                    lConnection,
                                                                                                                    lTransaction)
                                                vExisteStockRetro = Not BeStockJornadaExistente_ Is Nothing
                                            End If
                                            If vExisteStockRetro Then

                                                vDiaRetroaActivo = vDiaRetroaActivo.AddDays(1)
                                                vCntOmitidosDuplicado += 1
                                                Continue For

                                            End If

                                            lTicketProcesados.Add(BeStockJornadaRetroActiva.IdJornadaSistema)

                                            Dim vPasoInsertRetro As DateTime = Date.Now
                                            clsLnStock_jornada.Insertar(BeStockJornadaRetroActiva,
                                                                        lConnection,
                                                                        lTransaction)
                                            vMsInsertRetro += CLng((Date.Now - vPasoInsertRetro).TotalMilliseconds)
                                            vCntInsertRetro += 1
                                            If vPreload.Activo Then
                                                vPreload.StockExistente.Add(vKeyStockRetro)
                                                vPreload.HashRetroExistente.Add(InvJornadaKeyHashFecha(Hash, vDiaRetroaActivo))
                                            End If


                                            '#EJC202302160922: Revisar si aplica.
                                            vDiaRetroaActivo = vDiaRetroaActivo.AddDays(1)

                                        End If

                                    Next

                                End If

                            End If

                        End If
                        vMsRetroactivo += CLng((Date.Now - vPasoTrace).TotalMilliseconds)



                        'End If

                    End If

                    '#GT03022023_1440: valida retroactivo desde un día atras a la fecha actual, para no sobrecargar en ingresos desde 2017
                    If ValidarRetroactivo AndAlso BeStockJornadaExistente.IdTicketTMS = 0 AndAlso BeStockJornadaExistente.Fecha_ingreso.Date = vFechaDiaAnterior.Date Then

                        vPasoTrace = Date.Now
                        vDiasPrevios = 0
                        vDiasPrevios = DateDiff(DateInterval.Day,
                                                BeStockJornadaExistente.Fecha_ingreso.Date,
                                                fecha_hoy.Date)

                        If vDiasPrevios <> 0 Then

                            Dim vDiaRetroaActivo As Date = BeStockJornadaExistente.Fecha_ingreso.Date
                            Dim BeStockJornadaRetroActiva As New clsBeStock_jornada()


                            lblprg.AppendText("Se encontraron " & vDiasPrevios & " días sin retroactivo y ticket, para el IdStock: " & BeStockJornadaExistente.IdStock)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            For i = 0 To vDiasPrevios - 1

                                Dim vExisteJornadaDia As Boolean = False
                                If vPreload.Activo Then
                                    vExisteJornadaDia = vPreload.JornadasExistentes.Contains(InvJornadaFechaKey(vDiaRetroaActivo))
                                Else
                                    vExisteJornadaDia = Existe_Jornada(vDiaRetroaActivo, lConnection, lTransaction)
                                End If

                                If Not vExisteJornadaDia Then

                                    Dim BeJornadaSistemaDiaFaltante As New clsBeJornada_sistema()
                                    BeJornadaSistemaDiaFaltante = Inserta_Jornada(pIdEmpresa,
                                                                                      pIdUsuario,
                                                                                      vDiaRetroaActivo,
                                                                                      lConnection,
                                                                                      lTransaction)

                                    If Not BeJornadaSistemaDiaFaltante Is Nothing Then
                                        BeStockJornadaRetroActiva = New clsBeStock_jornada()
                                        clsPublic.CopyObject(BeJornadaSistemaDiaFaltante, BeStockJornadaRetroActiva)
                                        If vPreload.Activo Then vPreload.JornadasExistentes.Add(InvJornadaFechaKey(vDiaRetroaActivo))
                                    End If

                                Else

                                    BeStockJornadaRetroActiva = New clsBeStock_jornada()
                                    clsPublic.CopyObject(BeStockJornadaExistente, BeStockJornadaRetroActiva)
                                    BeStockJornadaRetroActiva.Fecha = vDiaRetroaActivo
                                    BeStockJornadaRetroActiva.Es_Retroactivo = True
                                    BeStockJornadaRetroActiva.Stock_Jornada_Hash = ""
                                    vIdStockJornada += 1
                                    BeStockJornadaRetroActiva.IdStockJornada = vIdStockJornada
                                    Debug.Print("Iteracion sin ticket: " & i)

                                    '#GT28122022_1500: validar que no exista el stock con la fecha del retroactivo
                                    Dim BeStockJornadaExistente_ As New clsBeStock_jornada
                                    Dim vExisteStockRetro As Boolean = False
                                    Dim vKeyStockRetro As String = InvJornadaKeyStock(BeStockJornadaRetroActiva.Lic_plate,
                                                                                      vDiaRetroaActivo,
                                                                                      BeStockJornadaRetroActiva.IdPropietarioBodega,
                                                                                      BeStockJornadaRetroActiva.IdProductoBodega,
                                                                                      BeStockJornadaRetroActiva.IdStock)
                                    If vPreload.Activo Then
                                        vExisteStockRetro = vPreload.StockExistente.Contains(vKeyStockRetro)
                                    Else
                                        BeStockJornadaExistente_ = clsLnStock_jornada.Get_Single_By_IdStock(BeStockJornadaRetroActiva.Lic_plate,
                                                                                                            vDiaRetroaActivo,
                                                                                                            BeStockJornadaRetroActiva.IdPropietarioBodega,
                                                                                                            BeStockJornadaRetroActiva.IdProductoBodega,
                                                                                                            BeStockJornadaRetroActiva.IdStock,
                                                                                                            lConnection,
                                                                                                            lTransaction)
                                        vExisteStockRetro = Not BeStockJornadaExistente_ Is Nothing
                                    End If
                                    If vExisteStockRetro Then
                                        vDiaRetroaActivo = vDiaRetroaActivo.AddDays(1)
                                        vCntOmitidosDuplicado += 1
                                        Continue For
                                    End If

                                    Dim vPasoInsertRetro As DateTime = Date.Now
                                    clsLnStock_jornada.Insertar(BeStockJornadaRetroActiva,
                                                                                lConnection,
                                                                                lTransaction)
                                    vMsInsertRetro += CLng((Date.Now - vPasoInsertRetro).TotalMilliseconds)
                                    vCntInsertRetro += 1
                                    If vPreload.Activo Then vPreload.StockExistente.Add(vKeyStockRetro)

                                    vDiaRetroaActivo = vDiaRetroaActivo.AddDays(1)

                                End If


                            Next
                        End If
                        vMsRetroactivo += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                    End If

#End Region

                    '#GT26122022_1630: valido nuevamente sino existe con la Fecha, porque previamente se pudo ejecutar iteración de Retroactivo
                    If vVerificarExistenciaStock Then
                        vPasoTrace = Date.Now
                        Dim vKeyStockActual As String = InvJornadaKeyStock(BeStockJornadaExistente.Lic_plate,
                                                                           pFechaJornada,
                                                                           BeStockJornadaExistente.IdPropietarioBodega,
                                                                           BeStockJornadaExistente.IdProductoBodega,
                                                                           BeStockJornadaExistente.IdStock)
                        Dim vExisteStockActual As Boolean = False
                        If vPreload.Activo Then
                            vExisteStockActual = vPreload.StockExistente.Contains(vKeyStockActual)
                        Else
                            BeStockJornadaExistentePosteriorRetroactivo = clsLnStock_jornada.Get_Single_By_IdStock(BeStockJornadaExistente.Lic_plate,
                                                                                                                   pFechaJornada,
                                                                                                                   BeStockJornadaExistente.IdPropietarioBodega,
                                                                                                                   BeStockJornadaExistente.IdProductoBodega,
                                                                                                                   BeStockJornadaExistente.IdStock,
                                                                                                                   lConnection,
                                                                                                                   lTransaction)
                            vExisteStockActual = Not BeStockJornadaExistentePosteriorRetroactivo Is Nothing
                        End If
                        If vExisteStockActual Then
                            vIdStockJornada += 1
                            vCntOmitidosDuplicado += 1
                            vMsDedupe += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                            Continue For
                        End If
                        vMsDedupe += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                    End If

                    '#EJC202302211036: No actualizar cantidad base.
                    'Dim vBeStockRec1 As New clsBeStock_rec
                    'vBeStockRec1 = clsLnStock_rec.Get_Single_By_IdRecepcionEnc_And_Licencia(BeStockJornadaExistente.IdRecepcionEnc,
                    '                                                                       BeStockJornadaExistente.Lic_plate,
                    '                                                                       lConnection,
                    '                                                                       lTransaction)

                    'If pListaIngresosYSalidasDelDia Is Nothing Then
                    '    If Not vBeStockRec1 Is Nothing Then
                    '        BeStockJornadaExistente.Cantidad = vBeStockRec1.Cantidad
                    '    End If
                    'End If

                    vLoteInsert.Add(BeStockJornadaExistente)
                    If vPreload.Activo Then
                        vPreload.StockExistente.Add(InvJornadaKeyStock(BeStockJornadaExistente.Lic_plate,
                                                                        pFechaJornada,
                                                                        BeStockJornadaExistente.IdPropietarioBodega,
                                                                        BeStockJornadaExistente.IdProductoBodega,
                                                                        BeStockJornadaExistente.IdStock))
                    End If
                    If vLoteInsert.Count >= vLoteTam Then
                        vPasoTrace = Date.Now
                        vCntInsertNormal += clsLnStock_jornada.Insert_Multiple_Fast(vLoteInsert,
                                                                                     lConnection,
                                                                                     lTransaction)
                        vMsInsertNormal += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                        vLoteInsert.Clear()
                    End If

                    vIdStockJornada += 1

                    vPasoTrace = Date.Now
                    Application.DoEvents()

                    prg.Value = vContador
                    vContador += 1
                    vMsUi += CLng((Date.Now - vPasoTrace).TotalMilliseconds)

                    If vContador Mod 500 = 0 Then
                        JornadaStockTrace(vSesionTrace, "INSERTAR_STOCK_JORNADA_PROGRESS", vInicioTrace,
                                          "Procesados=" & vContador &
                                          ";InsertNormal=" & vCntInsertNormal &
                                          ";InsertRetro=" & vCntInsertRetro &
                                          ";Duplicados=" & vCntOmitidosDuplicado &
                                          ";MsInsertNormal=" & vMsInsertNormal &
                                          ";MsInsertRetro=" & vMsInsertRetro &
                                          ";MsDedupe=" & vMsDedupe &
                                          ";MsRetro=" & vMsRetroactivo &
                                          ";MsUi=" & vMsUi)
                    End If


                Next

                If vLoteInsert.Count > 0 Then
                    vPasoTrace = Date.Now
                    vCntInsertNormal += clsLnStock_jornada.Insert_Multiple_Fast(vLoteInsert,
                                                                                 lConnection,
                                                                                 lTransaction)
                    vMsInsertNormal += CLng((Date.Now - vPasoTrace).TotalMilliseconds)
                    vLoteInsert.Clear()
                End If

                lblprg.AppendText("Fin de inserción para jornada: - " & pFechaJornada)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            End If

            '#GT26122022_1700: sígun proceso que llena la lista, toma solo ticket con procesado_stock_jornada=0, para aca setearlos a 1
            If lTicketsProcesados.Count > 0 Then
                vPasoTrace = Date.Now
                For Each t In lTicketsProcesados
                    clsLnTms_ticket.Actualizar_Tms_Ticket_Procesado_Por_Stock_Jornada(t.Key,
                                                                                      lConnection,
                                                                                      lTransaction)
                Next
                vMsCierreTickets = CLng((Date.Now - vPasoTrace).TotalMilliseconds)
            End If

            '#GT26122022_1700: sígun proceso que llena la lista, toma solo licencia en recepcion sin procesar jornada, para aca setearlos a 1
            If lLicenciasProcesadas.Count > 0 Then
                vPasoTrace = Date.Now
                For Each tBeLicenciaJornada In lLicenciasProcesadas
                    clsLnTrans_re_det.Actualizar_Licencia_Procesada_Por_Stock_Jornada(tBeLicenciaJornada,
                                                                                      lConnection,
                                                                                      lTransaction)
                Next
                vMsCierreLicencias = CLng((Date.Now - vPasoTrace).TotalMilliseconds)
            End If


            Insertar_Stock_Jornada = True
            JornadaStockTrace(vSesionTrace, "INSERTAR_STOCK_JORNADA_FIN", vInicioTrace,
                              "Rows=" & If(DTVWStockJornada Is Nothing, 0, DTVWStockJornada.Rows.Count) &
                              ";InsertNormal=" & vCntInsertNormal &
                              ";InsertRetro=" & vCntInsertRetro &
                              ";Duplicados=" & vCntOmitidosDuplicado &
                              ";TicketQueries=" & vCntTicketQuery &
                              ";LicenciaQueries=" & vCntLicenciaQuery &
                              ";MsServidor=" & vMsServidor &
                              ";MsStockDet=" & vMsStockDet &
                              ";MsBodega=" & vMsBodega &
                              ";MsMapeo=" & vMsMapeo &
                              ";MsSingularidad=" & vMsSingularidad &
                              ";MsTicket=" & vMsTicket &
                              ";MsLicencia=" & vMsLicencia &
                              ";MsHash=" & vMsHash &
                              ";MsRetro=" & vMsRetroactivo &
                              ";MsDedupe=" & vMsDedupe &
                              ";MsInsertNormal=" & vMsInsertNormal &
                              ";MsInsertRetro=" & vMsInsertRetro &
                              ";MsUi=" & vMsUi &
                              ";MsCierreTickets=" & vMsCierreTickets &
                              ";MsCierreLicencias=" & vMsCierreLicencias)

        Catch ex As Exception

            JornadaStockTrace(vSesionTrace, "INSERTAR_STOCK_JORNADA_ERROR", vInicioTrace, ex.Message)
            lblprg.AppendText("ERROR: " & String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw ex

        Finally
            prg.Value = 0 : prg.Visible = False
        End Try

    End Function


    Public Shared Function Insertar_Stock_Jornada_Desde_HH(ByVal DTVWStockJornada As DataTable,
                                                           ByVal pFechaJornada As Date,
                                                           ByVal pJornadaActual As clsBeJornada_sistema,
                                                           ByRef pListaIngresosYSalidasDelDia As List(Of clsBeStockEnUnaFecha),
                                                           ByVal pUltimaJornada As clsBeJornada_sistema,
                                                           ByVal pIdEmpresa As Integer,
                                                           ByVal pIdUsuario As Integer,
                                                           ByVal lConnection As SqlConnection,
                                                           ByVal lTransaction As SqlTransaction,
                                                           Optional ByVal ValidarRetroactivo As Boolean = True) As Boolean

        Insertar_Stock_Jornada_Desde_HH = False
        Dim vContador As Integer = 0
        Dim vSingularidadStock As New List(Of clsBeStockEnUnaFecha)
        Dim lTicketsProcesados As New Dictionary(Of Integer, Boolean)
        Dim lBeStockDet As New List(Of clsBeStock_det)
        Dim lBeStockDetUltimaJornada As New DataTable
        Dim lTicketProcesados As New List(Of String)
        Dim genera_retroactivo As Boolean = False

        Try

            If DTVWStockJornada IsNot Nothing AndAlso DTVWStockJornada.Rows.Count > 0 Then

                Dim fecha_hoy = clsServidor.Get_Fecha_Servidor(lConnection, lTransaction)
                Dim BeStockJornadaExistente As New clsBeStock_jornada
                Dim BeStockJornadaDesfase As New clsBeStock_jornada
                Dim BeStockJornadaRegistrado As New clsBeStock_jornada
                Dim BeStockDet As New clsBeStock_det()
                Dim vFechaDiaAnterior As Date = fecha_hoy.AddDays(-1)
                Dim vDifDiasIngreso As Integer = 0
                Dim vDiasPrevios As Integer = 0
                Dim vExisteRetroactivoDia As Boolean = False
                Dim vTicketTMSProcesado As Boolean = False
                Dim vIndiceDocumentoIngreso As Integer = -1
                Dim vCostoUnitarioLinea As Double = 0
                Dim vIdOrdenCompraEnc As Integer = 0
                Dim vIdRecepcionDet As Integer = 0
                Dim vCostoUnitario As Double = 0
                Dim vCostoTotal As Double = 0
                Dim vCantidadInicial As Double = 0
                Dim BeTMSTicket As New clsBeTms_ticket
                Dim vVerificarExistenciaStock As Boolean = False
                Dim vIdStockJornada As Integer = clsLnStock_jornada.MaxID(lConnection, lTransaction) + 1

                '#GT18012023: traer el detalle del stock para validar posiciones ocupadas.
                lBeStockDet = New List(Of clsBeStock_det)
                lBeStockDet = clsLnStock_det.Get_All(lConnection,
                                                     lTransaction)

                '#GT18012023: para iterar el correlativo en stock_jornada
                Dim vIdJornada As Integer = 0
                vIdJornada = pJornadaActual.IdJornada

                'Dim registro_ As Integer = 0

                '#GT18012023: iteramos un registro con stock disponible, para no disponible, viene una lista
                For Each R As DataRow In DTVWStockJornada.Rows

                    vVerificarExistenciaStock = True

                    BeStockJornadaExistente = New clsBeStock_jornada()
                    BeStockJornadaExistente.IdStockJornada = vIdStockJornada
                    BeStockJornadaExistente.IdJornadaSistema = vIdJornada
                    BeStockJornadaExistente.Fecha = pFechaJornada
                    BeStockJornadaExistente.IdBodega = IIf(IsDBNull(R.Item("IdBodega")), 0, R.Item("IdBodega"))
                    BeStockJornadaExistente.IdStock = IIf(IsDBNull(R.Item("IdStock")), 0, R.Item("IdStock"))
                    BeStockJornadaExistente.IdPropietarioBodega = IIf(IsDBNull(R.Item("IdPropietarioBodega")), 0, R.Item("IdPropietarioBodega"))
                    BeStockJornadaExistente.IdProductoBodega = IIf(IsDBNull(R.Item("IdProductoBodega")), 0, R.Item("IdProductoBodega"))
                    BeStockJornadaExistente.IdProductoEstado = IIf(IsDBNull(R.Item("IdProductoEstado")), 0, R.Item("IdProductoEstado"))
                    BeStockJornadaExistente.IdPresentacion = IIf(IsDBNull(R.Item("IdPresentacion")), Nothing, R.Item("IdPresentacion"))
                    BeStockJornadaExistente.IdUnidadMedida = IIf(IsDBNull(R.Item("IdUnidadMedida")), 0, R.Item("IdUnidadMedida"))
                    BeStockJornadaExistente.IdUbicacion = IIf(IsDBNull(R.Item("IdUbicacion")), 0, R.Item("IdUbicacion"))
                    BeStockJornadaExistente.IdUbicacion_anterior = IIf(IsDBNull(R.Item("IdUbicacion_anterior")), 0, R.Item("IdUbicacion_anterior"))
                    BeStockJornadaExistente.IdRecepcionEnc = IIf(IsDBNull(R.Item("IdRecepcionEnc")), 0, R.Item("IdRecepcionEnc"))
                    BeStockJornadaExistente.IdRecepcionDet = IIf(IsDBNull(R.Item("IdRecepcionDet")), 0, R.Item("IdRecepcionDet"))
                    BeStockJornadaExistente.IdPedidoEnc = IIf(IsDBNull(R.Item("IdPedidoEnc")), 0, R.Item("IdPedidoEnc"))
                    BeStockJornadaExistente.IdPickingEnc = IIf(IsDBNull(R.Item("IdPickingEnc")), 0, R.Item("IdPickingEnc"))
                    BeStockJornadaExistente.IdDespachoEnc = IIf(IsDBNull(R.Item("IdDespachoEnc")), 0, R.Item("IdDespachoEnc"))
                    BeStockJornadaExistente.Lote = IIf(IsDBNull(R.Item("lote")), "", R.Item("lote"))
                    BeStockJornadaExistente.Lic_plate = IIf(IsDBNull(R.Item("lic_plate")), "", R.Item("lic_plate"))
                    BeStockJornadaExistente.Serial = IIf(IsDBNull(R.Item("serial")), "", R.Item("serial"))
                    BeStockJornadaExistente.Cantidad = IIf(IsDBNull(R.Item("cantidad")), 0, R.Item("cantidad"))
                    BeStockJornadaExistente.Fecha_ingreso = IIf(IsDBNull(R.Item("fecha_ingreso")), "01/01/1900", R.Item("fecha_ingreso"))
                    BeStockJornadaExistente.Fecha_vence = IIf(IsDBNull(R.Item("fecha_vence")), "01/01/1900", R.Item("fecha_vence"))
                    BeStockJornadaExistente.Uds_lic_plate = IIf(IsDBNull(R.Item("uds_lic_plate")), 0, R.Item("uds_lic_plate"))
                    BeStockJornadaExistente.No_bulto = IIf(IsDBNull(R.Item("no_bulto")), 0, R.Item("no_bulto"))
                    BeStockJornadaExistente.Fecha_manufactura = IIf(IsDBNull(R.Item("fecha_manufactura")), "01/01/1900", R.Item("fecha_manufactura"))
                    BeStockJornadaExistente.Añada = IIf(IsDBNull(R.Item("añada")), 0, R.Item("añada"))
                    BeStockJornadaExistente.User_agr = IIf(IsDBNull(R.Item("user_agr")), "", R.Item("user_agr"))
                    BeStockJornadaExistente.Fec_agr = IIf(IsDBNull(R.Item("fec_agr")), "01/01/1900", R.Item("fec_agr"))
                    BeStockJornadaExistente.User_mod = IIf(IsDBNull(R.Item("user_mod")), "", R.Item("user_mod"))
                    BeStockJornadaExistente.Fec_mod = IIf(IsDBNull(R.Item("fec_mod")), "01/01/1900", R.Item("fec_mod"))
                    BeStockJornadaExistente.Activo = IIf(IsDBNull(R.Item("activo")), False, R.Item("activo"))
                    BeStockJornadaExistente.Peso = IIf(IsDBNull(R.Item("peso")), 0, R.Item("peso"))
                    BeStockJornadaExistente.Temperatura = IIf(IsDBNull(R.Item("temperatura")), 0, R.Item("temperatura"))
                    BeStockJornadaExistente.Atributo_variante_1 = IIf(IsDBNull(R.Item("atributo_variante_1")), Nothing, R.Item("atributo_variante_1"))
                    BeStockJornadaExistente.Pallet_no_estandar = IIf(IsDBNull(R.Item("pallet_no_estandar")), False, R.Item("pallet_no_estandar"))
                    BeStockJornadaExistente.Propietario = IIf(IsDBNull(R.Item("Propietario")), "", R.Item("Propietario"))
                    BeStockJornadaExistente.Proveedor = IIf(IsDBNull(R.Item("Proveedor")), "", R.Item("Proveedor"))
                    BeStockJornadaExistente.Bodega = IIf(IsDBNull(R.Item("Bodega")), "", R.Item("Bodega"))
                    BeStockJornadaExistente.IdOrdenCompraEnc = IIf(IsDBNull(R.Item("IdOrdenCompraEnc")), 0, R.Item("IdOrdenCompraEnc"))
                    BeStockJornadaExistente.No_DocumentoOC = IIf(IsDBNull(R.Item("No_DocumentoOC")), "", R.Item("No_DocumentoOC"))
                    BeStockJornadaExistente.No_DocumentoRec = IIf(IsDBNull(R.Item("No_DocumentoRec")), "", R.Item("No_DocumentoRec"))
                    BeStockJornadaExistente.ReferenciaOC = IIf(IsDBNull(R.Item("ReferenciaOC")), "", R.Item("ReferenciaOC"))
                    BeStockJornadaExistente.Fecha_Recepcion = IIf(IsDBNull(R.Item("Fecha_Recepcion")), New Date(1900, 1, 1), R.Item("Fecha_Recepcion"))
                    BeStockJornadaExistente.TipoTrans = IIf(IsDBNull(R.Item("TipoTrans")), "", R.Item("TipoTrans"))
                    BeStockJornadaExistente.Fecha_Agrego = IIf(IsDBNull(R.Item("Fecha_Agrego")), New Date(1900, 1, 1), R.Item("Fecha_Agrego"))
                    BeStockJornadaExistente.Codigo_producto = IIf(IsDBNull(R.Item("codigo_producto")), "", R.Item("codigo_producto"))
                    BeStockJornadaExistente.Codigo_barra_producto = IIf(IsDBNull(R.Item("codigo_barra_producto")), "", R.Item("codigo_barra_producto"))
                    BeStockJornadaExistente.Nombre_producto = IIf(IsDBNull(R.Item("nombre_producto")), "", R.Item("nombre_producto"))
                    BeStockJornadaExistente.Existencia = IIf(IsDBNull(R.Item("existencia")), 0, R.Item("existencia"))
                    BeStockJornadaExistente.Nom_umBas = IIf(IsDBNull(R.Item("nom_umBas")), "", R.Item("nom_umBas"))
                    BeStockJornadaExistente.Nom_estado_producto = IIf(IsDBNull(R.Item("nom_estado_producto")), "", R.Item("nom_estado_producto"))
                    BeStockJornadaExistente.Ubicacion_origen = IIf(IsDBNull(R.Item("ubicacion_origen")), "", R.Item("ubicacion_origen"))
                    BeStockJornadaExistente.No_poliza = IIf(IsDBNull(R.Item("no_poliza")), "", R.Item("no_poliza"))
                    BeStockJornadaExistente.Valor_aduana = IIf(IsDBNull(R.Item("valor_aduana")), 0, R.Item("valor_aduana"))
                    BeStockJornadaExistente.Valor_fob = IIf(IsDBNull(R.Item("valor_fob")), 0, R.Item("valor_fob"))
                    BeStockJornadaExistente.Valor_iva = IIf(IsDBNull(R.Item("valor_iva")), 0, R.Item("valor_iva"))
                    BeStockJornadaExistente.Valor_dai = IIf(IsDBNull(R.Item("valor_dai")), 0, R.Item("valor_dai"))
                    BeStockJornadaExistente.Valor_seguro = IIf(IsDBNull(R.Item("valor_seguro")), 0, R.Item("valor_seguro"))
                    BeStockJornadaExistente.Peso_neto = IIf(IsDBNull(R.Item("peso_neto")), 0, R.Item("peso_neto"))
                    BeStockJornadaExistente.Numero_orden = IIf(IsDBNull(R.Item("numero_orden")), 0, R.Item("numero_orden"))
                    BeStockJornadaExistente.Codigo_regimen = IIf(IsDBNull(R.Item("codigo_regimen")), 0, R.Item("codigo_regimen"))
                    BeStockJornadaExistente.Nombre_regimen = IIf(IsDBNull(R.Item("nombre_regimen")), "", R.Item("nombre_regimen"))
                    BeStockJornadaExistente.Dias_vencimiento_regimen = IIf(IsDBNull(R.Item("dias_vencimiento_regimen")), 0, R.Item("dias_vencimiento_regimen"))
                    BeStockJornadaExistente.Factor = IIf(IsDBNull(R.Item("Factor")), 0, R.Item("Factor"))
                    BeStockJornadaExistente.CamasPorTarima = IIf(IsDBNull(R.Item("CamasPorTarima")), 0, R.Item("CamasPorTarima"))
                    BeStockJornadaExistente.CajasPorCama = IIf(IsDBNull(R.Item("CajasPorCama")), 0, R.Item("CajasPorCama"))
                    BeStockJornadaExistente.Fecha_Ingreso_Ticket_TMS = IIf(IsDBNull(R.Item("Fecha_Ingreso_Ticket_TMS")), BeStockJornadaExistente.Fecha_Recepcion, R.Item("Fecha_Ingreso_Ticket_TMS"))
                    BeStockJornadaExistente.IdTicketTMS = Val(IIf(IsDBNull(R.Item("IdTicketTMS")), 0, R.Item("IdTicketTMS")))
                    BeStockJornadaExistente.IdPropietario = Val(IIf(IsDBNull(R.Item("IdPropietario")), 0, R.Item("IdPropietario")))
                    BeStockJornadaExistente.IdClasificacion = Val(IIf(IsDBNull(R.Item("IdClasificacion")), 0, R.Item("IdClasificacion")))
                    BeStockJornadaExistente.Clasificacion = IIf(IsDBNull(R.Item("Clasificacion")), "", R.Item("Clasificacion"))
                    BeStockJornadaExistente.Regimen = IIf(IsDBNull(R.Item("Regimen")), "", R.Item("Regimen"))
                    BeStockJornadaExistente.Nom_presentacion_producto = IIf(IsDBNull(R.Item("Presentacion_Producto")), "", R.Item("Presentacion_Producto"))
                    '#EJC20210603:Posiciones cealsa.
                    BeStockDet = lBeStockDet.Find(Function(x) x.IdStock = BeStockJornadaExistente.IdStock)
                    If Not BeStockDet Is Nothing Then
                        BeStockJornadaExistente.Posiciones = BeStockDet.Posiciones
                    Else
                        BeStockJornadaExistente.Posiciones = 1
                    End If
                    '#GT18012023: Calculo del costo unitario
                    vIdOrdenCompraEnc = IIf(IsDBNull(R.Item("IdOrdenCompraEnc")), 0, R.Item("IdOrdenCompraEnc"))
                    If vIndiceDocumentoIngreso = -1 Then
                        Dim vCalcularCostoUnitario As Boolean = False
                        If vCalcularCostoUnitario Then
                            vCostoUnitarioLinea = clsLnTrans_oc_det.Get_Costo_Unitario_By_IdOrdenCompraEnc_And_IdRecepcionDet(vIdOrdenCompraEnc,
                                                                                                                              vIdRecepcionDet,
                                                                                                                              lConnection,
                                                                                                                              lTransaction)
                        End If
                        vCostoUnitario = vCostoUnitarioLinea
                    End If
                    BeStockJornadaExistente.Costo_Unitario = vCostoUnitario
                    '#GT18012023: valida si existen movimientos relacionados a la LP y producto.
                    If Not pListaIngresosYSalidasDelDia Is Nothing Then
                        '#EJC20210519: Buscar en la lista si este producto existe con estos criterios
                        vSingularidadStock = pListaIngresosYSalidasDelDia.FindAll(Function(x) x.IdProductoBodega = BeStockJornadaExistente.IdProductoBodega _
                                                                              AndAlso x.IdPropietarioBodega = BeStockJornadaExistente.IdPropietarioBodega _
                                                                              AndAlso x.Lote = BeStockJornadaExistente.Lote _
                                                                              AndAlso x.Lic_Plate = BeStockJornadaExistente.Lic_plate _
                                                                              AndAlso x.IdUbicacionOrigen = BeStockJornadaExistente.IdUnidadMedida _
                                                                              AndAlso x.IdPresentacion = BeStockJornadaExistente.IdPresentacion)
                        'Si encuentra registros que coincidan sumarón al stock de la jornada el total que ingresí.
                        For Each vs In vSingularidadStock
                            BeStockJornadaExistente.Cantidad_Ingreso_Afecta_A_salida += vs.Ingresos
                        Next
                    End If

                    '#GT03012023_2100: solo se asigna hash cuando es registro de un retroactivo
                    Dim Hash As String = clsBeStock_jornada.GetRecordHash(BeStockJornadaExistente)
                    BeStockJornadaExistente.Stock_Jornada_Hash = ""

#Region "Retroactivo"

                    '#GT18012023: procesa ticket para cerrarlo
                    vTicketTMSProcesado = False

                    If BeStockJornadaExistente.IdTicketTMS <> 0 Then
                        If lTicketsProcesados.Count > 0 Then
                            If Not lTicketsProcesados.ContainsKey(BeStockJornadaExistente.IdTicketTMS) Then
                                vTicketTMSProcesado = clsLnTms_ticket.Ticket_Procesado_Stock_Jornada(BeStockJornadaExistente.IdTicketTMS,
                                                                                                     lConnection,
                                                                                                     lTransaction)
                                If Not vTicketTMSProcesado Then
                                    lTicketsProcesados.Add(BeStockJornadaExistente.IdTicketTMS, vTicketTMSProcesado)
                                End If
                            Else
                                vTicketTMSProcesado = lTicketsProcesados.Item(BeStockJornadaExistente.IdTicketTMS)
                            End If
                        Else
                            vTicketTMSProcesado = clsLnTms_ticket.Ticket_Procesado_Stock_Jornada(BeStockJornadaExistente.IdTicketTMS,
                                                                                                 lConnection,
                                                                                                 lTransaction)
                            If Not vTicketTMSProcesado Then
                                lTicketsProcesados.Add(BeStockJornadaExistente.IdTicketTMS, vTicketTMSProcesado)
                            End If
                        End If

                    End If

                    '#GT18012023: proceso retroactivo segón fecha ingreso del ticket
                    If ValidarRetroactivo AndAlso BeStockJornadaExistente.IdTicketTMS <> 0 Then

                        vDifDiasIngreso = 0
                        vDifDiasIngreso = DateDiff(DateInterval.Day,
                                                      BeStockJornadaExistente.Fecha_Ingreso_Ticket_TMS.Date,
                                                      fecha_hoy.Date)

                        '#GT18012023: se estima que el ticket tenga no mas de 7 días de antigüedad para retroactivo
                        If vDifDiasIngreso > 0 AndAlso vDifDiasIngreso < 7 Then
                            Dim vFechaIniciaRetroactivo As Date = BeStockJornadaExistente.Fecha_Ingreso_Ticket_TMS.Date
                            Dim BeStockJornadaRetroActiva As New clsBeStock_jornada()
                            For i = 0 To vDifDiasIngreso - 1

                                If Not Existe_Jornada(vFechaIniciaRetroactivo,
                                                        lConnection,
                                                        lTransaction) Then

                                    Dim BeJornadaSistemaDiaFaltante As New clsBeJornada_sistema()
                                    BeJornadaSistemaDiaFaltante = Inserta_Jornada(pIdEmpresa,
                                                                                      pIdUsuario,
                                                                                      vFechaIniciaRetroactivo,
                                                                                      lConnection,
                                                                                      lTransaction)

                                    If Not BeJornadaSistemaDiaFaltante Is Nothing Then
                                        BeStockJornadaRetroActiva = New clsBeStock_jornada()
                                        clsPublic.CopyObject(BeJornadaSistemaDiaFaltante, BeStockJornadaRetroActiva)
                                    End If

                                Else

                                    BeStockJornadaRetroActiva = New clsBeStock_jornada()
                                    clsPublic.CopyObject(BeStockJornadaExistente, BeStockJornadaRetroActiva)
                                    BeStockJornadaRetroActiva.Fecha = vFechaIniciaRetroactivo
                                    BeStockJornadaRetroActiva.Es_Retroactivo = True
                                    BeStockJornadaRetroActiva.Stock_Jornada_Hash = Hash
                                    vIdStockJornada += 1
                                    BeStockJornadaRetroActiva.IdStockJornada = vIdStockJornada
                                    '#GT28122022_1500: validar que no exista el stock con la fecha del retroactivo
                                    Dim BeStockJornadaExistente_ As New clsBeStock_jornada
                                    BeStockJornadaExistente_ = clsLnStock_jornada.Get_Single_By_IdStock(BeStockJornadaRetroActiva.Lic_plate,
                                                                                                        vFechaIniciaRetroactivo,
                                                                                                        BeStockJornadaRetroActiva.IdPropietarioBodega,
                                                                                                        BeStockJornadaRetroActiva.IdProductoBodega,
                                                                                                        BeStockJornadaRetroActiva.IdStock,
                                                                                                        lConnection,
                                                                                                        lTransaction)

                                    If Not BeStockJornadaExistente_ Is Nothing Then
                                        vFechaIniciaRetroactivo = vFechaIniciaRetroactivo.AddDays(1)
                                        Continue For
                                    End If

                                    Debug.Write("fecha" & BeStockJornadaRetroActiva.Fecha)
                                    Debug.Write("LP" & BeStockJornadaRetroActiva.Lic_plate)

                                    clsLnStock_jornada.Insertar_desde_HH(BeStockJornadaRetroActiva,
                                                                                lConnection,
                                                                                lTransaction)

                                    vFechaIniciaRetroactivo = vFechaIniciaRetroactivo.AddDays(1)

                                End If

                            Next

                        End If

                        '#GT18012023 Si tiene retroactivo sin ticket se valida
                    Else

                        If ValidarRetroactivo AndAlso BeStockJornadaExistente.IdTicketTMS = 0 AndAlso BeStockJornadaExistente.Fecha_ingreso.Date = vFechaDiaAnterior.Date Then

                            vDiasPrevios = 0
                            vDiasPrevios = DateDiff(DateInterval.Day,
                                                           BeStockJornadaExistente.Fecha_ingreso.Date,
                                                           fecha_hoy.Date)

                            If vDiasPrevios <> 0 Then
                                Dim vDiaRetroaActivo As Date = BeStockJornadaExistente.Fecha_ingreso.Date
                                Dim BeStockJornadaRetroActiva As New clsBeStock_jornada()

                                For i = 0 To vDiasPrevios - 1

                                    If Not Existe_Jornada(vDiaRetroaActivo,
                                                              lConnection,
                                                              lTransaction) Then

                                        Dim BeJornadaSistemaDiaFaltante As New clsBeJornada_sistema()
                                        BeJornadaSistemaDiaFaltante = Inserta_Jornada(pIdEmpresa,
                                                                                          pIdUsuario,
                                                                                          vDiaRetroaActivo,
                                                                                          lConnection,
                                                                                          lTransaction)

                                        If Not BeJornadaSistemaDiaFaltante Is Nothing Then
                                            BeStockJornadaRetroActiva = New clsBeStock_jornada()
                                            clsPublic.CopyObject(BeJornadaSistemaDiaFaltante, BeStockJornadaRetroActiva)
                                        End If

                                    Else

                                        BeStockJornadaRetroActiva = New clsBeStock_jornada()
                                        clsPublic.CopyObject(BeStockJornadaExistente, BeStockJornadaRetroActiva)
                                        BeStockJornadaRetroActiva.Fecha = vDiaRetroaActivo
                                        BeStockJornadaRetroActiva.Es_Retroactivo = True
                                        BeStockJornadaRetroActiva.Stock_Jornada_Hash = ""
                                        vIdStockJornada += 1
                                        BeStockJornadaRetroActiva.IdStockJornada = vIdStockJornada
                                        '#GT28122022_1500: validar que no exista el stock con la fecha del retroactivo
                                        Dim BeStockJornadaExistente_ As New clsBeStock_jornada
                                        BeStockJornadaExistente_ = clsLnStock_jornada.Get_Single_By_IdStock(BeStockJornadaRetroActiva.Lic_plate,
                                                                                                            vDiaRetroaActivo,
                                                                                                            BeStockJornadaRetroActiva.IdPropietarioBodega,
                                                                                                            BeStockJornadaRetroActiva.IdProductoBodega,
                                                                                                            BeStockJornadaRetroActiva.IdStock,
                                                                                                            lConnection,
                                                                                                            lTransaction)

                                        If Not BeStockJornadaExistente_ Is Nothing Then
                                            vDiaRetroaActivo = vDiaRetroaActivo.AddDays(1)
                                            Continue For
                                        End If

                                        clsLnStock_jornada.Insertar_desde_HH(BeStockJornadaRetroActiva,
                                                                                    lConnection,
                                                                                    lTransaction)

                                        Debug.Print("Insert_retroactivo_sinTicket: " & BeStockJornadaRetroActiva.IdStock)

                                    End If

                                    vDiaRetroaActivo = vDiaRetroaActivo.AddDays(1)
                                Next
                            End If
                        End If


                    End If

#End Region



                    '#GT18012023: evita duplicar el registro si es que en retroactivo ya se insertó con la fecha en proceso.
                    If vVerificarExistenciaStock Then
                        BeStockJornadaRegistrado = New clsBeStock_jornada
                        BeStockJornadaRegistrado = clsLnStock_jornada.Get_Single_By_IdStock(BeStockJornadaExistente.Lic_plate,
                                                                                            pFechaJornada,
                                                                                            BeStockJornadaExistente.IdPropietarioBodega,
                                                                                            BeStockJornadaExistente.IdProductoBodega,
                                                                                            BeStockJornadaExistente.IdStock,
                                                                                            lConnection,
                                                                                            lTransaction)

                        If Not BeStockJornadaRegistrado Is Nothing Then
                            vIdStockJornada += 1
                            Continue For
                        End If
                    End If

                    clsLnStock_jornada.Insertar_desde_HH(BeStockJornadaExistente,
                                             lConnection,
                                             lTransaction)

                    vIdStockJornada += 1

                    Dim Resultado = " inserte_historico_idstock:: " & BeStockJornadaExistente.IdStock & " y LP: " & BeStockJornadaExistente.Lic_plate
                    Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), Resultado)
                    clsLnLog_error_wms.Agregar_Error(vMsgError)

                Next


            End If

            Insertar_Stock_Jornada_Desde_HH = True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Jornada() As Boolean

        Existe_Jornada = False

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT * FROM jornada_sistema WHERE fecha =@Fecha "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", FormatoFechas.tFecha(Now))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_Jornada = True
                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Private Shared Function Copiar_Stock_A_Stock_Jornada_Parcial(ByVal pUltimaJornadaTicket As clsBeJornada_sistema,
                                                                 ByVal pJornadaActual As clsBeJornada_sistema,
                                                                 ByVal pIdTicketTMS As Integer,
                                                                 ByVal pIdEmpresa As Integer,
                                                                 ByVal pIdBodega As Integer,
                                                                 ByVal pIdUsuario As Integer,
                                                                 ByVal lConnection As SqlConnection,
                                                                 ByVal lTransaction As SqlTransaction,
                                                                 ByVal lblprg As RichTextBox,
                                                                 ByRef prg As ProgressBar,
                                                                 ByRef pParentForm As Form,
                                                                 Optional ByVal pIdStock As Integer = 0) As Boolean

        Copiar_Stock_A_Stock_Jornada_Parcial = False

        Try

            Dim vElDiaDeAyer As Date = Now.AddDays(-1)
            Dim lIngresosYSalidasDelDia As New List(Of clsBeStockEnUnaFecha)

            Dim DTVWStockJornada As New DataTable

            '#GT03012023_2025: Se valida si llena por ticket o no.
            If pIdTicketTMS = 0 Then
                DTVWStockJornada = Get_VW_Stock_Jornada_By_IdStock_and_Fecha(pIdStock,
                                                                             pJornadaActual.Fecha,
                                                                             lConnection,
                                                                             lTransaction)
            Else
                DTVWStockJornada = Get_VW_Stock_Jornada_By_IdTicketTMS(pIdTicketTMS,
                                                                       lConnection,
                                                                       lTransaction)
            End If

            If pUltimaJornadaTicket Is Nothing Then

                lIngresosYSalidasDelDia = Get_Ingresos_Y_Salidas_El_Mismo_Dia(vElDiaDeAyer,
                                                                              lConnection,
                                                                              lTransaction,
                                                                              lblprg,
                                                                              prg)

                Insertar_Stock_Jornada(DTVWStockJornada,
                                       vElDiaDeAyer,
                                       lIngresosYSalidasDelDia,
                                       pUltimaJornadaTicket,
                                       pIdEmpresa,
                                       pIdBodega,
                                       pIdUsuario,
                                       lConnection,
                                       lTransaction,
                                       lblprg,
                                       prg,
                                       pParentForm,
                                       False,
                                       pJornadaActual)

                Copiar_Stock_A_Stock_Jornada_Parcial = True

                lblprg.AppendText("La jornada del día 0 en el sistema se registró correctamente :) " & Now)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

            Else

                Dim days As Long = DateDiff(DateInterval.Day, pUltimaJornadaTicket.Fecha, Now) + 1
                Dim vFechaJornada As Date

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText("Se encontraron " & days & " días de diferencia a partir de la última jornada generada el: " & pUltimaJornadaTicket.Fecha)
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                For i = 0 To days - 1

                    '#EJC20210520: Utilizar para debug de escenarios específicos.
                    'vFechaJornada = New Date(2021, 4, 23)

                    If i = 0 Then
                        vFechaJornada = pUltimaJornadaTicket.Fecha
                    Else

                        vFechaJornada = vFechaJornada.AddDays(1)

                        lblprg.AppendText("Procesando stock para jornada del día: " & vFechaJornada)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    End If


                    If pIdTicketTMS > 0 Then

                        lIngresosYSalidasDelDia = Get_Ingresos_Y_Salidas_El_Mismo_Dia_By_IdTicketTMS(vFechaJornada,
                                                                                               pIdTicketTMS,
                                                                                               lConnection,
                                                                                               lTransaction,
                                                                                               lblprg,
                                                                                               prg)

                    End If

                    Insertar_Stock_Jornada(DTVWStockJornada,
                                           vFechaJornada,
                                           lIngresosYSalidasDelDia,
                                           pUltimaJornadaTicket,
                                           pIdEmpresa,
                                           pIdBodega,
                                           pIdUsuario,
                                           lConnection,
                                           lTransaction,
                                           lblprg,
                                           prg,
                                           pParentForm,
                                           False,
                                           pJornadaActual)

                    Application.DoEvents()

                Next

                Copiar_Stock_A_Stock_Jornada_Parcial = True

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Get_Jornada_By_Fecha(ByVal pFechaJornada As Date,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As clsBeJornada_sistema

        Get_Jornada_By_Fecha = Nothing

        Try

            Dim vSQL As String = "SELECT top(1) * FROM jornada_sistema WHERE Fecha = @Fecha"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", FormatoFechas.tFecha(pFechaJornada))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vBeJornada_sistema As New clsBeJornada_sistema
                    Cargar(vBeJornada_sistema, lDataTable.Rows(0))
                    Get_Jornada_By_Fecha = vBeJornada_sistema

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_VW_Stock_Jornada_By_IdTicketTMS(ByVal pIdTicketTMS As Integer,
                                                               ByVal lConnection As SqlConnection,
                                                               ByVal lTransaction As SqlTransaction) As DataTable

        Get_VW_Stock_Jornada_By_IdTicketTMS = Nothing

        Try

            Dim vSQL As String = " select * 
								   FROM vw_stock_jornada WHERE IdTicketTMS = @IdTicketTMS "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandTimeout = 120
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTicketTMS", pIdTicketTMS)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_VW_Stock_Jornada_By_IdTicketTMS = lDataTable
                End If

            End Using

        Catch ex As Exception
            Throw ex
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function


    '#GT03012023_2030: trae stock jornada sin ticket
    Public Shared Function Get_VW_Stock_Jornada_By_IdStock_and_Fecha(ByVal pIdStock As Integer,
                                                                     ByVal pFecha As Date,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As DataTable

        Get_VW_Stock_Jornada_By_IdStock_and_Fecha = Nothing

        Try

            Dim vSQL As String = " select * 
								   FROM vw_stock_jornada 
                                   WHERE IdTicketTMS =0 and IdStock= @pIdStock and CAST(fecha_ingreso AS date)= @pFecha "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandTimeout = 120
                lDTA.SelectCommand.Parameters.AddWithValue("@pIdStock", pIdStock)
                lDTA.SelectCommand.Parameters.AddWithValue("@pFecha", FormatoFechas.tFecha(pFecha))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_VW_Stock_Jornada_By_IdStock_and_Fecha = lDataTable
                End If

            End Using

        Catch ex As Exception
            Throw ex
        Finally
            SplashScreenManager.CloseForm(False)
        End Try

    End Function

    Private Shared Function Existe_Jornada(ByVal pFecha As Date,
                                           ByVal lConnection As SqlConnection,
                                           ByVal lTransaction As SqlTransaction) As Boolean

        Existe_Jornada = False

        Try

            Dim vSQL As String = "SELECT * FROM jornada_sistema WHERE fecha =@Fecha "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", FormatoFechas.tFecha(pFecha))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_Jornada = True
                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Inserta_Jornada(ByVal pIdEmpresa As Integer,
                                            ByVal pIdUsuario As Integer,
                                            ByVal pFecha As Date,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction) As clsBeJornada_sistema

        Inserta_Jornada = Nothing

        Try

            Dim vBeJornada As New clsBeJornada_sistema()
            vBeJornada = New clsBeJornada_sistema()
            vBeJornada.IdJornada = MaxID(lConnection, lTransaction) + 1
            vBeJornada.IdEmpresa = pIdEmpresa
            vBeJornada.IdBodega = 0
            vBeJornada.Fecha = New Date(pFecha.Year, pFecha.Month, pFecha.Day)
            vBeJornada.FechaAgregado = Now
            vBeJornada.IdUsuario = pIdUsuario
            Insertar(vBeJornada, lConnection, lTransaction)

            Inserta_Jornada = vBeJornada

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Private Shared Function Inserta_Jornada_Actual(ByVal pIdEmpresa As Integer,
                                                   ByVal pIdUsuario As Integer,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As clsBeJornada_sistema

        Inserta_Jornada_Actual = Nothing

        Try

            Dim hoy = clsServidor.Get_Fecha_Servidor(lConnection, lTransaction)
            Dim vBeJornada As New clsBeJornada_sistema()
            vBeJornada = New clsBeJornada_sistema()
            vBeJornada.IdJornada = MaxID(lConnection, lTransaction) + 1
            vBeJornada.IdEmpresa = pIdEmpresa
            vBeJornada.IdBodega = 0
            vBeJornada.Fecha = New Date(hoy.Year, hoy.Month, hoy.Day)
            vBeJornada.IdUsuario = pIdUsuario
            vBeJornada.FechaAgregado = hoy
            Insertar(vBeJornada, lConnection, lTransaction)

            Inserta_Jornada_Actual = vBeJornada

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Tickets_Tienen_Saltos_En_Retroactivo(ByVal lblprg As RichTextBox,
                                                                ByRef prg As ProgressBar,
                                                                ByRef pParenForm As Form,
                                                                ByVal pIdEmpresa As Integer,
                                                                ByVal pIdUsuario As Integer) As Boolean

        Tickets_Tienen_Saltos_En_Retroactivo = False

        Dim lBeTicketTMSSinRetroactivo As New List(Of clsBeStock_jornada)
        Dim BeTicketTMSSinRetroactivo As New clsBeStock_jornada
        Dim BeJornadaFechaTicket As New clsBeJornada_sistema
        Dim BeUltimaJornada As New clsBeJornada_sistema


        Try

            Dim vSQL As String = "select IdStock, 
                                  IdJornadaSistema, 
                                  IdBodega, 
                                  IdStock, 
                                  IdTicketTMS, 
                                  Fecha,
                                  Lic_Plate,
                                  YEAR(FECHA) AS Año, 
                                  Month(fecha) AS Mes, 
                                  DAY(FECHA) AS Dia 
                                  from stock_jornada  
                                  WHERE IdTicketTMS <> 0 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            lblprg.AppendText("Se encontrarón:  " & lDataTable.Rows.Count & " registros para revisión. Hora: " & Now)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                            For Each R As DataRow In lDataTable.Rows

                                BeTicketTMSSinRetroactivo = New clsBeStock_jornada
                                BeTicketTMSSinRetroactivo.IdJornadaSistema = IIf(IsDBNull(R.Item("IdJornadaSistema")), 0, R.Item("IdJornadaSistema"))
                                BeTicketTMSSinRetroactivo.IdBodega = IIf(IsDBNull(R.Item("IdBodega")), 0, R.Item("IdBodega"))
                                BeTicketTMSSinRetroactivo.IdTicketTMS = IIf(IsDBNull(R.Item("IdTicketTMS")), 0, R.Item("IdTicketTMS"))
                                BeTicketTMSSinRetroactivo.IdStock = IIf(IsDBNull(R.Item("IdStock")), 0, R.Item("IdStock"))
                                BeTicketTMSSinRetroactivo.Fecha = IIf(IsDBNull(R.Item("Fecha")), 0, R.Item("Fecha"))
                                BeTicketTMSSinRetroactivo.Año = IIf(IsDBNull(R.Item("Año")), 0, R.Item("Año"))
                                BeTicketTMSSinRetroactivo.Mes = IIf(IsDBNull(R.Item("Mes")), 0, R.Item("Mes"))
                                BeTicketTMSSinRetroactivo.Dia = IIf(IsDBNull(R.Item("Dia")), 0, R.Item("Dia"))
                                BeTicketTMSSinRetroactivo.Lic_plate = IIf(IsDBNull(R.Item("Lic_plate")), 0, R.Item("Lic_plate"))
                                lBeTicketTMSSinRetroactivo.Add(BeTicketTMSSinRetroactivo)

                                lblprg.AppendText("Llenando lista para IdTicketTMS: " & BeTicketTMSSinRetroactivo.IdTicketTMS & " Licencia: " & BeTicketTMSSinRetroactivo.Lic_plate & " Fecha: " & BeTicketTMSSinRetroactivo.Fecha)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                                Application.DoEvents()

                            Next

                            Dim BeTicketAnt As clsBeStock_jornada = Nothing

                            For Each Ticket In lBeTicketTMSSinRetroactivo

                                If BeTicketAnt Is Nothing Then
                                    BeTicketAnt = Ticket
                                Else
                                    If Not BeTicketAnt.IdStock = Ticket.IdStock Then
                                        BeTicketAnt = Ticket
                                    End If
                                End If

                                If Not (Ticket.Año = BeTicketAnt.Año AndAlso Not Ticket.Mes = BeTicketAnt.Mes AndAlso Not BeTicketAnt.Dia = Ticket.Dia) Then
                                    Dim vMensaje As String = "Falta la siguiente fecha: " & New Date(Ticket.Año, Ticket.Mes, Ticket.Dia) & " para el ticket: " & Ticket.IdTicketTMS
                                    lblprg.AppendText(vMensaje)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength

                                    lblprg.ScrollToCaret()
                                Else
                                    lblprg.AppendText("Todo bien en la secuencia para el IdTicketTMS: " & BeTicketTMSSinRetroactivo.IdTicketTMS & " Fecha anterior: " & BeTicketAnt.Fecha & " Siguiente fecha: " & Ticket.Fecha & " Licencia: " & BeTicketAnt.Lic_plate)
                                    lblprg.AppendText(vbNewLine)
                                    lblprg.Refresh()
                                    lblprg.SelectionStart = lblprg.TextLength
                                    lblprg.ScrollToCaret()
                                End If

                            Next

                            Tickets_Tienen_Saltos_En_Retroactivo = True

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Get_Ingresos_Y_Salidas_El_Mismo_Dia_By_IdTicketTMS(ByVal pFechaJornada As Date,
                                                                               ByVal pIdTicketTMS As Integer,
                                                                               ByVal lConnection As SqlConnection,
                                                                               ByVal lTransaction As SqlTransaction,
                                                                               ByVal lblprg As RichTextBox,
                                                                               ByRef prg As ProgressBar) As List(Of clsBeStockEnUnaFecha)


        Get_Ingresos_Y_Salidas_El_Mismo_Dia_By_IdTicketTMS = Nothing

        Try

            Dim lMovimientosMismoDia As New List(Of clsBeVW_Movimientos)

            lMovimientosMismoDia = clsLnVW_Movimientos.Get_All_By_Fecha(pFechaJornada,
                                                                        pIdTicketTMS,
                                                                        lConnection,
                                                                        lTransaction)

            Dim lStockEnFecha As New List(Of clsBeStockEnUnaFecha)

            lStockEnFecha = Normalizar_Movimientos(lMovimientosMismoDia,
                                                   pFechaJornada,
                                                   lblprg,
                                                   prg)

            Return lStockEnFecha

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Tiene_Tickets_TMS_Sin_Retroactivo(ByVal lblprg As RichTextBox,
                                                             ByRef prg As ProgressBar,
                                                             ByRef pParenForm As Form,
                                                             ByVal pIdEmpresa As Integer,
                                                             ByVal pIdUsuario As Integer,
                                                             ByVal lConnection As SqlConnection,
                                                             ByVal lTransaction As SqlTransaction) As Boolean

        Tiene_Tickets_TMS_Sin_Retroactivo = False

        Dim lBeTicketTMSSinRetroactivo As New List(Of clsBeTicketTMSSinRetroactivo)
        Dim BeTicketTMSSinRetroactivo As New clsBeTicketTMSSinRetroactivo
        Dim BeJornadaFechaTicket As New clsBeJornada_sistema
        Dim BeUltimaJornada As New clsBeJornada_sistema


        Try

            Dim vSQL As String = "SELECT distinct IdTicketTMS, 
                                  Fecha_Creacion_Documento, 
                                  Fecha_Ingreso_Ticket 
                                  FROM VW_TMSTickets_Sin_Retroactivo  "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    lblprg.AppendText("Se encontraron: " & lDataTable.Rows.Count & " registros sin retroactivo.")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    For Each R As DataRow In lDataTable.Rows

                        BeTicketTMSSinRetroactivo = New clsBeTicketTMSSinRetroactivo()
                        BeTicketTMSSinRetroactivo.IdTicketTMS = IIf(IsDBNull(R.Item("IdTicketTMS")), 0, R.Item("IdTicketTMS"))
                        BeTicketTMSSinRetroactivo.Fecha_Creacion_Documento = IIf(IsDBNull(R.Item("Fecha_Creacion_Documento")), New Date(1900, 1, 1), R.Item("Fecha_Creacion_Documento"))
                        BeTicketTMSSinRetroactivo.Fecha_Ingreso_Ticket = IIf(IsDBNull(R.Item("Fecha_Ingreso_Ticket")), New Date(1900, 1, 1), R.Item("Fecha_Ingreso_Ticket"))
                        lBeTicketTMSSinRetroactivo.Add(BeTicketTMSSinRetroactivo)

                        lblprg.AppendText("Preparando Ticket: " & BeTicketTMSSinRetroactivo.IdTicketTMS)
                        lblprg.AppendText(vbNewLine)
                        lblprg.Refresh()
                        lblprg.SelectionStart = lblprg.TextLength
                        lblprg.ScrollToCaret()

                    Next

                    For Each Ticket In lBeTicketTMSSinRetroactivo

                        BeJornadaFechaTicket = Get_Jornada_By_Fecha(Ticket.Fecha_Ingreso_Ticket,
                                                                            lConnection,
                                                                            lTransaction)

                        If Not BeJornadaFechaTicket Is Nothing Then

                            Debug.Write("jornada procesada" & BeJornadaFechaTicket.IdJornada)
                            Debug.Write("ticket procesado" & Ticket.IdTicketTMS)
                        End If


                        If BeJornadaFechaTicket Is Nothing Then



                            Dim BeJornadaSistemaDiaFaltante As New clsBeJornada_sistema()
                            BeJornadaSistemaDiaFaltante = Inserta_Jornada(pIdEmpresa,
                                                                                  pIdUsuario,
                                                                                  Ticket.Fecha_Ingreso_Ticket,
                                                                                  lConnection,
                                                                                  lTransaction)

                            If Not BeJornadaSistemaDiaFaltante Is Nothing Then
                                BeJornadaFechaTicket = BeJornadaSistemaDiaFaltante
                            End If

                            lblprg.AppendText("Insertando Ticket: " & BeTicketTMSSinRetroactivo.IdTicketTMS)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End If

                        BeUltimaJornada = Get_Ultima_Jornada(lConnection, lTransaction)

                        If Not BeJornadaFechaTicket Is Nothing Then

                            If Copiar_Stock_A_Stock_Jornada_Parcial(BeJornadaFechaTicket,
                                                                            BeUltimaJornada,
                                                                            Ticket.IdTicketTMS,
                                                                            pIdEmpresa,
                                                                            BeJornadaFechaTicket.IdBodega,
                                                                            pIdUsuario,
                                                                            lConnection,
                                                                            lTransaction,
                                                                            lblprg,
                                                                            prg,
                                                                            pParenForm) Then

                                '#EJC20220808: El retroactivo de un ticket específico se insertó correctamente en una jornada intercalada.
                                lblprg.AppendText(vbNewLine)
                                lblprg.AppendText("Copiando a stock jornada: " & BeTicketTMSSinRetroactivo.IdTicketTMS)
                                lblprg.AppendText(vbNewLine)
                                lblprg.Refresh()
                                lblprg.SelectionStart = lblprg.TextLength
                                lblprg.ScrollToCaret()

                            Else
                                Throw New Exception("ERROR_202208081550: No se pudo insertar la jornada del ticket: " & Ticket.IdTicketTMS & " En la fecha de jornada: " & BeJornadaFechaTicket.Fecha)
                            End If

                        Else
                            Throw New Exception("ERROR_202208081550: La jornada debería existir para fecha: " & Ticket.Fecha_Ingreso_Ticket)
                        End If

                    Next

                    Tiene_Tickets_TMS_Sin_Retroactivo = True

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
