Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTipo_etiqueta

    Public Shared Sub Cargar(ByRef oBeTipo_etiqueta As clsBeTipo_etiqueta, ByRef dr As DataRow)

        Try

            With oBeTipo_etiqueta

                .IdTipoEtiqueta = IIf(IsDBNull(dr.Item("IdTipoEtiqueta")), 0, dr.Item("IdTipoEtiqueta"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Alto = IIf(IsDBNull(dr.Item("Alto")), 0.0, dr.Item("Alto"))
                .Ancho = IIf(IsDBNull(dr.Item("Ancho")), 0.0, dr.Item("Ancho"))
                .MargenIzq = IIf(IsDBNull(dr.Item("MargenIzq")), 0.0, dr.Item("MargenIzq"))
                .MagenDer = IIf(IsDBNull(dr.Item("MagenDer")), 0.0, dr.Item("MagenDer"))
                .MargenSup = IIf(IsDBNull(dr.Item("MargenSup")), 0.0, dr.Item("MargenSup"))
                .MargenInf = IIf(IsDBNull(dr.Item("MargenInf")), 0.0, dr.Item("MargenInf"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Nothing, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Nothing, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .dpi = IIf(IsDBNull(dr.Item("dpi")), 0, dr.Item("dpi"))
                .codigo_zpl = IIf(IsDBNull(dr.Item("codigo_zpl")), "", dr.Item("codigo_zpl"))
                .Idclasificacion_etiqueta = IIf(IsDBNull(dr.Item("Idclasificacion_etiqueta")), 0, dr.Item("Idclasificacion_etiqueta"))
                .Es_Inkjet = IIf(IsDBNull(dr.Item("es_inkjet")), False, dr.Item("es_inkjet"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTipo_etiqueta As clsBeTipo_etiqueta, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("tipo_etiqueta")
            Ins.Add("idtipoetiqueta", "@idtipoetiqueta", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("margenizq", "@margenizq", DataType.Parametro)
            Ins.Add("magender", "@magender", DataType.Parametro)
            Ins.Add("margensup", "@margensup", DataType.Parametro)
            Ins.Add("margeninf", "@margeninf", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("dpi", "@dpi", DataType.Parametro)
            Ins.Add("codigo_zpl", "@codigo_zpl", DataType.Parametro)
            Ins.Add("Idclasificacion_etiqueta", "@Idclasificacion_etiqueta", DataType.Parametro)
            Ins.Add("es_inkjet", "@es_inkjet", DataType.Parametro)


            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", oBeTipo_etiqueta.IdTipoEtiqueta))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTipo_etiqueta.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeTipo_etiqueta.Alto))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeTipo_etiqueta.Ancho))
            cmd.Parameters.Add(New SqlParameter("@MARGENIZQ", oBeTipo_etiqueta.MargenIzq))
            cmd.Parameters.Add(New SqlParameter("@MAGENDER", oBeTipo_etiqueta.MagenDer))
            cmd.Parameters.Add(New SqlParameter("@MARGENSUP", oBeTipo_etiqueta.MargenSup))
            cmd.Parameters.Add(New SqlParameter("@MARGENINF", oBeTipo_etiqueta.MargenInf))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTipo_etiqueta.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTipo_etiqueta.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTipo_etiqueta.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTipo_etiqueta.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTipo_etiqueta.Activo))
            cmd.Parameters.Add(New SqlParameter("@DPI", oBeTipo_etiqueta.dpi))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ZPL", oBeTipo_etiqueta.codigo_zpl))
            cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION_ETIQUETA", oBeTipo_etiqueta.Idclasificacion_etiqueta))
            cmd.Parameters.Add(New SqlParameter("@ES_INKJET", oBeTipo_etiqueta.Es_Inkjet))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTipo_etiqueta As clsBeTipo_etiqueta, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("tipo_etiqueta")
            Upd.Add("idtipoetiqueta", "@idtipoetiqueta", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("margenizq", "@margenizq", DataType.Parametro)
            Upd.Add("magender", "@magender", DataType.Parametro)
            Upd.Add("margensup", "@margensup", DataType.Parametro)
            Upd.Add("margeninf", "@margeninf", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("dpi", "@dpi", DataType.Parametro)
            Upd.Add("codigo_zpl", "@codigo_zpl", DataType.Parametro)
            Upd.Add("Idclasificacion_etiqueta", "@Idclasificacion_etiqueta", DataType.Parametro)
            Upd.Add("es_inkjet", "@es_inkjet", DataType.Parametro)
            Upd.Where("IdTipoEtiqueta = @IdTipoEtiqueta")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", oBeTipo_etiqueta.IdTipoEtiqueta))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeTipo_etiqueta.Nombre))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeTipo_etiqueta.Alto))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeTipo_etiqueta.Ancho))
            cmd.Parameters.Add(New SqlParameter("@MARGENIZQ", oBeTipo_etiqueta.MargenIzq))
            cmd.Parameters.Add(New SqlParameter("@MAGENDER", oBeTipo_etiqueta.MagenDer))
            cmd.Parameters.Add(New SqlParameter("@MARGENSUP", oBeTipo_etiqueta.MargenSup))
            cmd.Parameters.Add(New SqlParameter("@MARGENINF", oBeTipo_etiqueta.MargenInf))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTipo_etiqueta.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTipo_etiqueta.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeTipo_etiqueta.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeTipo_etiqueta.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeTipo_etiqueta.Activo))
            cmd.Parameters.Add(New SqlParameter("@DPI", oBeTipo_etiqueta.dpi))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ZPL", oBeTipo_etiqueta.codigo_zpl))
            cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION_ETIQUETA", oBeTipo_etiqueta.Idclasificacion_etiqueta))
            cmd.Parameters.Add(New SqlParameter("@ES_INKJET", oBeTipo_etiqueta.Es_Inkjet))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTipo_etiqueta As clsBeTipo_etiqueta, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tipo_etiqueta" &
             "  Where(IdTipoEtiqueta = @IdTipoEtiqueta)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", oBeTipo_etiqueta.IdTipoEtiqueta))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Tipo_etiqueta"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Listar(ByVal pActivo As Boolean) As DataTable

        Try

            Dim sp As String = "SELECT te.IdTipoEtiqueta AS código,ce.descripcion,te.Nombre,te.Alto,te.Ancho,
                                       te.MargenIzq,te.MagenDer,te.MargenInf,te.MargenSup 
                                FROM Tipo_etiqueta te 
                                       LEFT OUTER JOIN producto_clasificacion_etiqueta ce
                                       ON te.idclasificacion_etiqueta=ce.idclasificacion_etiqueta
                                       WHERE 1 > 0"

            If pActivo Then
                sp += " AND activo=1"
            Else
                sp += " AND activo=0"
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTipo_etiqueta As clsBeTipo_etiqueta) As Boolean

        Try

            Const sp As String = "SELECT te.* FROM Tipo_etiqueta te 
                                         left outer join producto_clasificacion_etiqueta ce on
				                         te.Idclasificacion_etiqueta=ce.Idclasificacion_etiqueta" &
                                 " Where(IdTipoEtiqueta = @IdTipoEtiqueta) "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", oBeTipo_etiqueta.IdTipoEtiqueta))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTipo_etiqueta, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeTipo_etiqueta)

        Try

            Dim lReturnList As New List(Of clsBeTipo_etiqueta)
            Const sp As String = "SELECT * FROM Tipo_etiqueta"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTipo_etiqueta As New clsBeTipo_etiqueta

            For Each dr As DataRow In dt.Rows

                vBeTipo_etiqueta = New clsBeTipo_etiqueta
                Cargar(vBeTipo_etiqueta, dr)
                lReturnList.Add(vBeTipo_etiqueta)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForCombo() As DataTable

        Try

            Const sp As String = "SELECT IdTipoEtiqueta,Nombre FROM Tipo_etiqueta WHERE activo=1"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTipo_etiqueta As clsBeTipo_etiqueta)

        Try

            Const sp As String = "SELECT te.* FROM Tipo_etiqueta te 
                                         left outer join producto_clasificacion_etiqueta ce on
				                         te.Idclasificacion_etiqueta=ce.Idclasificacion_etiqueta 
                                         Where(IdTipoEtiqueta = @IdTipoEtiqueta) "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", pBeTipo_etiqueta.IdTipoEtiqueta))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTipo_etiqueta, dt.Rows(0))
                pBeTipo_etiqueta.codigo_zpl = clsPublic.Conversion_ZPL_Codabar_to_QR(pBeTipo_etiqueta.codigo_zpl)
            End If

            Return True


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Single_By_IdTipoEtiqueta(ByRef pBeTipo_etiqueta As clsBeTipo_etiqueta,
                                                        ByVal IdSimbologia As Integer) As clsBeTipo_etiqueta


        Get_Single_By_IdTipoEtiqueta = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT TE.* FROM TIPO_ETIQUETA TE 
                                  LEFT OUTER JOIN PRODUCTO_CLASIFICACION_ETIQUETA CE ON
				                  TE.IDCLASIFICACION_ETIQUETA=CE.IDCLASIFICACION_ETIQUETA 
                                  WHERE(IDTIPOETIQUETA = @IDTIPOETIQUETA) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", pBeTipo_etiqueta.IdTipoEtiqueta))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then

                Dim pTipoSimbologia As clsDataContractDI.tSimbologiaEtiqueta
                pTipoSimbologia = IdSimbologia

                '#GT27112023: se obtiene el zpl por defecto
                Cargar(pBeTipo_etiqueta, dt.Rows(0))

                '#GT27112023: a futuro, validar otras simbologias, para adecuar el código ZPL
                Select Case pTipoSimbologia
                    Case clsDataContractDI.tSimbologiaEtiqueta.QRCode
                        pBeTipo_etiqueta.codigo_zpl = clsPublic.Conversion_ZPL_Codabar_to_QR(pBeTipo_etiqueta.codigo_zpl)
                End Select

                Get_Single_By_IdTipoEtiqueta = pBeTipo_etiqueta

            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdTipoEtiqueta),0) FROM Tipo_etiqueta"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If
                End Using
            End Using

            Return lMax

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdTipoEtiqueta(ByVal IdTipoEtiqueta As Integer,
                                                        ByVal IdSimbologia As Integer,
                                                        ByVal IdClasificacionEtiqueta As Integer) As clsBeTipo_etiqueta

        Get_Single_By_IdTipoEtiqueta = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT TE.* FROM TIPO_ETIQUETA TE 
                                  LEFT OUTER JOIN PRODUCTO_CLASIFICACION_ETIQUETA CE ON
				                  TE.IDCLASIFICACION_ETIQUETA=CE.IDCLASIFICACION_ETIQUETA 
                                  WHERE(IDTIPOETIQUETA = @IDTIPOETIQUETA AND CE.Idclasificacion_etiqueta = @IDCLASIDICACIONETIQUETA) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", IdTipoEtiqueta))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCLASIDICACIONETIQUETA", IdClasificacionEtiqueta))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then

                Dim pTipoSimbologia As clsDataContractDI.tSimbologiaEtiqueta
                pTipoSimbologia = IdSimbologia

                Dim pBeTipo_etiqueta As New clsBeTipo_etiqueta

                '#GT27112023: se obtiene el zpl por defecto
                Cargar(pBeTipo_etiqueta, dt.Rows(0))

                '#GT27112023: a futuro, validar otras simbologias, para adecuar el código ZPL
                Select Case pTipoSimbologia
                    Case clsDataContractDI.tSimbologiaEtiqueta.QRCode
                        pBeTipo_etiqueta.codigo_zpl = clsPublic.Conversion_ZPL_Codabar_to_QR(pBeTipo_etiqueta.codigo_zpl)
                    Case clsDataContractDI.tSimbologiaEtiqueta.Codabar
                        pBeTipo_etiqueta.codigo_zpl = clsPublic.Conversion_ZPL_Codabar_to_Codabar(pBeTipo_etiqueta.codigo_zpl)
                End Select

                Get_Single_By_IdTipoEtiqueta = pBeTipo_etiqueta

            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_IdTipoEtiqueta(ByVal IdTipoEtiqueta As Integer) As clsBeTipo_etiqueta

        Get_Single_By_IdTipoEtiqueta = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT TE.* FROM TIPO_ETIQUETA TE 
                                  LEFT OUTER JOIN PRODUCTO_CLASIFICACION_ETIQUETA CE ON
				                  TE.IDCLASIFICACION_ETIQUETA=CE.IDCLASIFICACION_ETIQUETA 
                                  WHERE(IDTIPOETIQUETA = @IDTIPOETIQUETA) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", IdTipoEtiqueta))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTipo_etiqueta As New clsBeTipo_etiqueta
                Cargar(pBeTipo_etiqueta, dt.Rows(0))
                Get_Single_By_IdTipoEtiqueta = pBeTipo_etiqueta
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function


    Public Shared Function Get_Single_By_IdTipoEtiqueta(
    ByVal IdTipoEtiqueta As Integer,
    ByVal IdSimbologia As Integer,
    ByVal IdClasificacionEtiqueta As Integer,
    ByVal lConnection As SqlConnection,
    ByVal lTransaction As SqlTransaction) As clsBeTipo_etiqueta

        Get_Single_By_IdTipoEtiqueta = Nothing

        Try
            Const sp As String =
            "SELECT TE.* FROM TIPO_ETIQUETA TE " &
            "LEFT OUTER JOIN PRODUCTO_CLASIFICACION_ETIQUETA CE ON " &
            "TE.IDCLASIFICACION_ETIQUETA=CE.IDCLASIFICACION_ETIQUETA " &
            "WHERE(IDTIPOETIQUETA = @IDTIPOETIQUETA AND CE.Idclasificacion_etiqueta = @IDCLASIDICACIONETIQUETA) "

            Using cmd As New SqlCommand(sp, lConnection, lTransaction)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", IdTipoEtiqueta))
                cmd.Parameters.Add(New SqlParameter("@IDCLASIDICACIONETIQUETA", IdClasificacionEtiqueta))

                Using dad As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count = 1 Then
                        Dim pTipoSimbologia As clsDataContractDI.tSimbologiaEtiqueta =
                        CType(IdSimbologia, clsDataContractDI.tSimbologiaEtiqueta)

                        Dim pBeTipo_etiqueta As New clsBeTipo_etiqueta

                        Cargar(pBeTipo_etiqueta, dt.Rows(0))

                        Select Case pTipoSimbologia
                            Case clsDataContractDI.tSimbologiaEtiqueta.QRCode
                                pBeTipo_etiqueta.codigo_zpl =
                                clsPublic.Conversion_ZPL_Codabar_to_QR(pBeTipo_etiqueta.codigo_zpl)
                            Case clsDataContractDI.tSimbologiaEtiqueta.Codabar
                                pBeTipo_etiqueta.codigo_zpl =
                                clsPublic.Conversion_ZPL_Codabar_to_Codabar(pBeTipo_etiqueta.codigo_zpl)
                        End Select

                        Get_Single_By_IdTipoEtiqueta = pBeTipo_etiqueta
                    End If
                End Using
            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw
        End Try

    End Function


End Class