Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnBodega_sector

    Public Shared Sub Cargar(ByRef oBeBodega_sector As clsBeBodega_sector, ByRef dr As DataRow)
        Try
            With oBeBodega_sector
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
                .IdSectorIzquierda = IIf(IsDBNull(dr.Item("IdSectorIzquierda")), 0, dr.Item("IdSectorIzquierda"))
                .IdSectorDerecha = IIf(IsDBNull(dr.Item("IdSectorDerecha")), 0, dr.Item("IdSectorDerecha"))
                .Horizontal = IIf(IsDBNull(dr.Item("horizontal")), False, dr.Item("horizontal"))
                .Pos_x = IIf(IsDBNull(dr.Item("pos_x")), 0.0, dr.Item("pos_x"))
                .Pos_y = IIf(IsDBNull(dr.Item("pos_y")), 0.0, dr.Item("pos_y"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeBodega_sector As clsBeBodega_sector, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("bodega_sector")
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
            Ins.Add("idsectorizquierda", "@idsectorizquierda", DataType.Parametro)
            Ins.Add("idsectorderecha", "@idsectorderecha", DataType.Parametro)
            Ins.Add("horizontal", "@horizontal", DataType.Parametro)
            Ins.Add("pos_x", "@pos_x", DataType.Parametro)
            Ins.Add("pos_y", "@pos_y", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeBodega_sector.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeBodega_sector.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_sector.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeBodega_sector.Sistema))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeBodega_sector.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeBodega_sector.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeBodega_sector.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeBodega_sector.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeBodega_sector.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodega_sector.Activo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeBodega_sector.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeBodega_sector.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeBodega_sector.Ancho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeBodega_sector.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeBodega_sector.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeBodega_sector.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeBodega_sector.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeBodega_sector.Codigo))
            cmd.Parameters.Add(New SqlParameter("@IDSECTORIZQUIERDA", oBeBodega_sector.IdSectorIzquierda))
            cmd.Parameters.Add(New SqlParameter("@IDSECTORDERECHA", oBeBodega_sector.IdSectorDerecha))
            cmd.Parameters.Add(New SqlParameter("@HORIZONTAL", oBeBodega_sector.Horizontal))
            cmd.Parameters.Add(New SqlParameter("@POS_X", oBeBodega_sector.Pos_x))
            cmd.Parameters.Add(New SqlParameter("@POS_Y", oBeBodega_sector.Pos_y))


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

    Public Shared Function Actualizar(ByRef oBeBodega_sector As clsBeBodega_sector, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("bodega_sector")
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
            Upd.Add("idsectorizquierda", "@idsectorizquierda", DataType.Parametro)
            Upd.Add("idsectorderecha", "@idsectorderecha", DataType.Parametro)
            Upd.Add("horizontal", "@horizontal", DataType.Parametro)
            Upd.Add("pos_x", "@pos_x", DataType.Parametro)
            Upd.Add("pos_y", "@pos_y", DataType.Parametro)
            Upd.Where("IdSector = @IdSector AND IdBodega=@IdBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeBodega_sector.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeBodega_sector.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_sector.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeBodega_sector.Sistema))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeBodega_sector.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeBodega_sector.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeBodega_sector.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeBodega_sector.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeBodega_sector.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodega_sector.Activo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeBodega_sector.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeBodega_sector.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeBodega_sector.Ancho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeBodega_sector.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeBodega_sector.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeBodega_sector.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeBodega_sector.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeBodega_sector.Codigo))
            cmd.Parameters.Add(New SqlParameter("@IDSECTORIZQUIERDA", oBeBodega_sector.IdSectorIzquierda))
            cmd.Parameters.Add(New SqlParameter("@IDSECTORDERECHA", oBeBodega_sector.IdSectorDerecha))
            cmd.Parameters.Add(New SqlParameter("@HORIZONTAL", oBeBodega_sector.Horizontal))
            cmd.Parameters.Add(New SqlParameter("@POS_X", oBeBodega_sector.Pos_x))
            cmd.Parameters.Add(New SqlParameter("@POS_Y", oBeBodega_sector.Pos_y))


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


    Public Shared Function Eliminar(ByRef oBeBodega_sector As clsBeBodega_sector, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Bodega_sector" &
             "  Where(IdSector = @IdSector)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeBodega_sector.IdSector))

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

            Const sp As String = " Delete from Bodega_sector"
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

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Bodega_sector "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
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

    Public Shared Function Obtener(ByRef oBeBodega_sector As clsBeBodega_sector) As Boolean

        Try

            Const sp As String = "SELECT * FROM Bodega_sector" &
            " Where(IdSector = @IdSector AND IdBodega=@IdBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSECTOR", oBeBodega_sector.IdSector))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", oBeBodega_sector.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega_sector, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeBodega_sector)

        Try

            Dim lReturnList As New List(Of clsBeBodega_sector)
            Const sp As String = "SELECT * FROM Bodega_sector"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeBodega_sector As New clsBeBodega_sector

            For Each dr As DataRow In dt.Rows

                vBeBodega_sector = New clsBeBodega_sector
                Cargar(vBeBodega_sector, dr)
                lReturnList.Add(vBeBodega_sector)

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

    Public Shared Function GetSingle(ByRef pBeBodega_sector As clsBeBodega_sector)

        Try

            Const sp As String = "SELECT * FROM Bodega_sector" &
            " Where(IdSector = @IdSector and IdBodega=@IdBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSECTOR", pBeBodega_sector.IdSector))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeBodega_sector.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeBodega_sector, dt.Rows(0))
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

    Public Shared Function MaxID(ByVal pIdBodega As Integer) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdSector),0) FROM Bodega_sector WHERE IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue) + 1
                        End If

                        lTransaction.Commit()

                    End Using

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


    Public Shared Function MaxID(ByVal pIdBodega As Integer,
                                 ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdSector),0) FROM Bodega_sector WHERE IdBodega = @IdBodega "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

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

End Class
