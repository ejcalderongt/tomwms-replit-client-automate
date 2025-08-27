Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks

Public Class clsLnBodega_tramo

    Public Shared Sub Cargar(ByRef oBeBodega_tramo As clsBeBodega_tramo, ByRef dr As DataRow)

        Try

            With oBeBodega_tramo

                .IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .IdSector = IIf(IsDBNull(dr.Item("IdSector")), 0, dr.Item("IdSector"))
                .IdArea = IIf(IsDBNull(dr.Item("IdArea")), 0, dr.Item("IdArea"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Margen_izquierdo = IIf(IsDBNull(dr.Item("margen_izquierdo")), 0.0, dr.Item("margen_izquierdo"))
                .Margen_derecho = IIf(IsDBNull(dr.Item("margen_derecho")), 0.0, dr.Item("margen_derecho"))
                .Margen_superior = IIf(IsDBNull(dr.Item("margen_superior")), 0.0, dr.Item("margen_superior"))
                .Margen_inferior = IIf(IsDBNull(dr.Item("margen_inferior")), 0.0, dr.Item("margen_inferior"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Indice_x = IIf(IsDBNull(dr.Item("Indice_x")), 0, dr.Item("Indice_x"))
                .Orientacion = IIf(IsDBNull(dr.Item("Orientacion")), 0, dr.Item("Orientacion"))
                .IdTipoProductoDefault = IIf(IsDBNull(dr.Item("IdTipoProductoDefault")), 0, dr.Item("IdTipoProductoDefault"))
                .IdFontEnc = IIf(IsDBNull(dr.Item("IdFontEnc")), 0, dr.Item("IdFontEnc"))
                .IdTipoRack = IIf(IsDBNull(dr.Item("IdTipoRack")), 0, dr.Item("IdTipoRack"))
                .Es_Rack = IIf(IsDBNull(dr.Item("Es_Rack")), True, dr.Item("Es_Rack"))
                .Horizontal = IIf(IsDBNull(dr.Item("Horizontal")), False, dr.Item("Horizontal"))
                .Orden_Descendente = IIf(IsDBNull(dr.Item("Orden_Descendente")), False, dr.Item("Orden_Descendente"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeBodega_tramo As clsBeBodega_tramo,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("bodega_tramo")
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("idsector", "@idsector", DataType.Parametro)
            Ins.Add("idarea", "@idarea", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("margen_izquierdo", "@margen_izquierdo", DataType.Parametro)
            Ins.Add("margen_derecho", "@margen_derecho", DataType.Parametro)
            Ins.Add("margen_superior", "@margen_superior", DataType.Parametro)
            Ins.Add("margen_inferior", "@margen_inferior", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("indice_x", "@indice_x", DataType.Parametro)
            Ins.Add("orientacion", "@orientacion", DataType.Parametro)
            Ins.Add("idtipoproductodefault", "@idtipoproductodefault", DataType.Parametro)
            Ins.Add("idtiporack", "@idtiporack", DataType.Parametro)
            Ins.Add("es_rack", "@es_rack", DataType.Parametro)
            Ins.Add("idfontenc", "@idfontenc", DataType.Parametro)
            Ins.Add("horizontal", "@horizontal", DataType.Parametro)
            Ins.Add("orden_descendente", "@orden_descendente", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeBodega_tramo.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeBodega_tramo.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeBodega_tramo.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_tramo.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeBodega_tramo.Sistema))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeBodega_tramo.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeBodega_tramo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeBodega_tramo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeBodega_tramo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeBodega_tramo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodega_tramo.Activo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeBodega_tramo.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeBodega_tramo.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeBodega_tramo.Ancho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeBodega_tramo.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeBodega_tramo.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeBodega_tramo.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeBodega_tramo.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeBodega_tramo.Codigo))
            cmd.Parameters.Add(New SqlParameter("@INDICE_X", oBeBodega_tramo.Indice_x))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION", oBeBodega_tramo.Orientacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTODEFAULT", oBeBodega_tramo.IdTipoProductoDefault))
            cmd.Parameters.Add(New SqlParameter("@IDTIPORACK", oBeBodega_tramo.IdTipoRack))
            cmd.Parameters.Add(New SqlParameter("@ES_RACK", oBeBodega_tramo.Es_Rack))
            cmd.Parameters.Add(New SqlParameter("@IDFONTENC", oBeBodega_tramo.IdFontEnc))
            cmd.Parameters.Add(New SqlParameter("@HORIZONTAL", oBeBodega_tramo.Horizontal))
            cmd.Parameters.Add(New SqlParameter("@ORDEN_DESCENDENTE", oBeBodega_tramo.Orden_Descendente))

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

    Public Shared Async Function Insertar_Async(ByVal oBeBodega_tramo As clsBeBodega_tramo,
                                                Optional ByVal pConection As SqlConnection = Nothing,
                                                Optional ByVal pTransaction As SqlTransaction = Nothing) As Task(Of Integer)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("bodega_tramo")
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("idsector", "@idsector", DataType.Parametro)
            Ins.Add("idarea", "@idarea", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("margen_izquierdo", "@margen_izquierdo", DataType.Parametro)
            Ins.Add("margen_derecho", "@margen_derecho", DataType.Parametro)
            Ins.Add("margen_superior", "@margen_superior", DataType.Parametro)
            Ins.Add("margen_inferior", "@margen_inferior", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("indice_x", "@indice_x", DataType.Parametro)
            Ins.Add("orientacion", "@orientacion", DataType.Parametro)
            Ins.Add("idtipoproductodefault", "@idtipoproductodefault", DataType.Parametro)
            Ins.Add("idtiporack", "@idtiporack", DataType.Parametro)
            Ins.Add("es_rack", "@es_rack", DataType.Parametro)
            Ins.Add("idfontenc", "@idfontenc", DataType.Parametro)
            Ins.Add("horizontal", "@horizontal", DataType.Parametro)
            Ins.Add("orden_descendente", "@orden_descendente", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                Await lConnection.OpenAsync() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeBodega_tramo.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeBodega_tramo.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeBodega_tramo.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_tramo.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeBodega_tramo.Sistema))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeBodega_tramo.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeBodega_tramo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeBodega_tramo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeBodega_tramo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeBodega_tramo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodega_tramo.Activo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeBodega_tramo.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeBodega_tramo.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeBodega_tramo.Ancho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeBodega_tramo.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeBodega_tramo.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeBodega_tramo.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeBodega_tramo.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeBodega_tramo.Codigo))
            cmd.Parameters.Add(New SqlParameter("@INDICE_X", oBeBodega_tramo.Indice_x))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION", oBeBodega_tramo.Orientacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTODEFAULT", oBeBodega_tramo.IdTipoProductoDefault))
            cmd.Parameters.Add(New SqlParameter("@IDTIPORACK", oBeBodega_tramo.IdTipoRack))
            cmd.Parameters.Add(New SqlParameter("@ES_RACK", oBeBodega_tramo.Es_Rack))
            cmd.Parameters.Add(New SqlParameter("@IDFONTENC", oBeBodega_tramo.IdFontEnc))
            cmd.Parameters.Add(New SqlParameter("@HORIZONTAL", oBeBodega_tramo.Horizontal))
            cmd.Parameters.Add(New SqlParameter("@ORDEN_DESCENDENTE", oBeBodega_tramo.Orden_Descendente))

            Dim rowsAffected As Task(Of Integer) = cmd.ExecuteNonQueryAsync()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return Await rowsAffected

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

    Public Shared Function Actualizar(ByRef oBeBodega_tramo As clsBeBodega_tramo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("bodega_tramo")
            Upd.Add("idtramo", "@idtramo", DataType.Parametro)
            Upd.Add("idsector", "@idsector", DataType.Parametro)
            Upd.Add("idarea", "@idarea", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("margen_izquierdo", "@margen_izquierdo", DataType.Parametro)
            Upd.Add("margen_derecho", "@margen_derecho", DataType.Parametro)
            Upd.Add("margen_superior", "@margen_superior", DataType.Parametro)
            Upd.Add("margen_inferior", "@margen_inferior", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("indice_x", "@indice_x", DataType.Parametro)
            Upd.Add("orientacion", "@orientacion", DataType.Parametro)
            Upd.Add("idtipoproductodefault", "@idtipoproductodefault", DataType.Parametro)
            Upd.Add("idtiporack", "@idtiporack", DataType.Parametro)
            Upd.Add("idfontenc", "@idfontenc", DataType.Parametro)
            Upd.Add("es_rack", "@es_rack", DataType.Parametro)
            Upd.Add("horizontal", "@horizontal", DataType.Parametro)
            Upd.Add("orden_descendente", "@orden_descendente", DataType.Parametro)
            Upd.Where("IdTramo = @IdTramo AND IdBodega=@IdBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeBodega_tramo.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeBodega_tramo.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeBodega_tramo.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_tramo.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeBodega_tramo.Sistema))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeBodega_tramo.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeBodega_tramo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeBodega_tramo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeBodega_tramo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeBodega_tramo.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodega_tramo.Activo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeBodega_tramo.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeBodega_tramo.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeBodega_tramo.Ancho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeBodega_tramo.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeBodega_tramo.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeBodega_tramo.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeBodega_tramo.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeBodega_tramo.Codigo))
            cmd.Parameters.Add(New SqlParameter("@INDICE_X", oBeBodega_tramo.Indice_x))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION", oBeBodega_tramo.Orientacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTODEFAULT", oBeBodega_tramo.IdTipoProductoDefault))
            cmd.Parameters.Add(New SqlParameter("@IDTIPORACK", oBeBodega_tramo.IdTipoRack))
            cmd.Parameters.Add(New SqlParameter("@ES_RACK", oBeBodega_tramo.Es_Rack))
            cmd.Parameters.Add(New SqlParameter("@IDFONTENC", oBeBodega_tramo.IdFontEnc))
            cmd.Parameters.Add(New SqlParameter("@HORIZONTAL", oBeBodega_tramo.Horizontal))
            cmd.Parameters.Add(New SqlParameter("@ORDEN_DESCENDENTE", oBeBodega_tramo.Orden_Descendente))

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

    Public Shared Function Eliminar(ByRef oBeBodega_tramo As clsBeBodega_tramo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Bodega_tramo" &
             "  Where(IdTramo = @IdTramo)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeBodega_tramo.IdTramo))

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

            Const sp As String = " Delete from Bodega_tramo"
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

    Public Shared Function Listar(ByVal pIdBodega As Integer) As DataTable

        Try

            Const sp As String = "SELECT * FROM Bodega_tramo WHERE IdBodega=@IdBodega "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeBodega_tramo As clsBeBodega_tramo) As Boolean

        Try

            Const sp As String = " SELECT * FROM Bodega_tramo" &
                                 " Where(IdTramo = @IdTramo AND IdBodega=@IdBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", oBeBodega_tramo.IdTramo))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", oBeBodega_tramo.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega_tramo, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeBodega_tramo)

        Try

            Dim lReturnList As New List(Of clsBeBodega_tramo)
            Const sp As String = "SELECT * FROM Bodega_tramo"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeBodega_tramo As New clsBeBodega_tramo

            For Each dr As DataRow In dt.Rows

                vBeBodega_tramo = New clsBeBodega_tramo
                Cargar(vBeBodega_tramo, dr)
                lReturnList.Add(vBeBodega_tramo)

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

    Public Shared Function GetSingle(ByRef pBeBodega_tramo As clsBeBodega_tramo) As Boolean

        GetSingle = False

        Try

            If pBeBodega_tramo Is Nothing Then Exit Function

            Const sp As String = "SELECT * FROM Bodega_tramo
                                  WHERE (IdTramo = @IDTRAMO AND IdBodega = @IDBODEGA)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Debug.Print("Tramo: " & pBeBodega_tramo.IdTramo)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", pBeBodega_tramo.IdTramo))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeBodega_tramo.IdBodega))


            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeBodega_tramo, dt.Rows(0))
                GetSingle = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdTramo_And_IdBodega(ByVal IdTramo As Integer, ByVal IdBodega As Integer) As clsBeBodega_tramo

        Get_Single_By_IdTramo_And_IdBodega = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Bodega_tramo
                                  WHERE (IdTramo = @IDTRAMO AND IdBodega = @IDBODEGA)"


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Debug.Print("Tramo: " & IdTramo)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTRAMO", IdTramo))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega_tramo As New clsBeBodega_tramo()
                Cargar(pBeBodega_tramo, dt.Rows(0))
                Get_Single_By_IdTramo_And_IdBodega = pBeBodega_tramo
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function MaxID(ByVal pIdBodega As Integer) As Integer

        Try

            Dim lMax As Integer = 1

            Dim sp As String = "SELECT ISNULL(Max(IdTramo),0) FROM Bodega_tramo 
                                WHERE IdBodega =@IdBodega "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue) + 1
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

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

    Public Shared Function Get_All_VW_By_IdBodega(ByVal pIdBodega As Integer) As DataTable

        Get_All_VW_By_IdBodega = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM VW_BodegaTramo WHERE IdBodega=@IdBodega "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Get_All_VW_By_IdBodega = dt

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function


End Class
