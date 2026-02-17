Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnBodega_muelles

    Public Shared Sub Cargar(ByRef oBeBodega_muelles As clsBeBodega_muelles, ByRef dr As DataRow)
        Try
            With oBeBodega_muelles
                .IdMuelle = IIf(IsDBNull(dr.Item("IdMuelle")), 0, dr.Item("IdMuelle"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Color = IIf(IsDBNull(dr.Item("color")), 0, dr.Item("color"))
                .Imagen = IIf(IsDBNull(dr.Item("imagen")), Nothing, dr.Item("imagen"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Entrada = IIf(IsDBNull(dr.Item("Entrada")), False, dr.Item("Entrada"))
                .Salida = IIf(IsDBNull(dr.Item("Salida")), False, dr.Item("Salida"))
                .IdUbicacionDefecto = IIf(IsDBNull(dr.Item("IdUbicacionDefecto")), 0, dr.Item("IdUbicacionDefecto"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeBodega_muelles As clsBeBodega_muelles, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("bodega_muelles")
            Ins.Add("idmuelle", "@idmuelle", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("color", "@color", DataType.Parametro)
            If Not oBeBodega_muelles.Imagen Is Nothing Then Ins.Add("imagen", "@imagen", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("entrada", "@entrada", DataType.Parametro)
            Ins.Add("salida", "@salida", DataType.Parametro)
            If Not oBeBodega_muelles.IdUbicacionDefecto = 0 Then Ins.Add("idubicaciondefecto", "@idubicaciondefecto", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDMUELLE", oBeBodega_muelles.IdMuelle))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_muelles.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeBodega_muelles.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeBodega_muelles.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeBodega_muelles.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeBodega_muelles.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeBodega_muelles.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeBodega_muelles.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@COLOR", oBeBodega_muelles.Color))
            If Not oBeBodega_muelles.Imagen Is Nothing Then cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeBodega_muelles.Imagen))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodega_muelles.Activo))
            cmd.Parameters.Add(New SqlParameter("@ENTRADA", oBeBodega_muelles.Entrada))
            cmd.Parameters.Add(New SqlParameter("@SALIDA", oBeBodega_muelles.Salida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONDEFECTO", oBeBodega_muelles.IdUbicacionDefecto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeBodega_muelles.IdMuelle = CInt(cmd.Parameters("@IDMUELLE").Value)

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

    Public Shared Function Actualizar(ByRef oBeBodega_muelles As clsBeBodega_muelles, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("bodega_muelles")
            Upd.Add("idmuelle", "@idmuelle", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("color", "@color", DataType.Parametro)
            If Not oBeBodega_muelles.Imagen Is Nothing Then Upd.Add("imagen", "@imagen", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("entrada", "@entrada", DataType.Parametro)
            Upd.Add("salida", "@salida", DataType.Parametro)
            If Not oBeBodega_muelles.IdUbicacionDefecto = 0 Then Upd.Add("idubicaciondefecto", "@idubicaciondefecto", DataType.Parametro)
            Upd.Where("IdMuelle = @IdMuelle")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDMUELLE", oBeBodega_muelles.IdMuelle))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeBodega_muelles.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeBodega_muelles.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeBodega_muelles.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeBodega_muelles.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeBodega_muelles.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeBodega_muelles.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeBodega_muelles.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@COLOR", oBeBodega_muelles.Color))
            If Not oBeBodega_muelles.Imagen Is Nothing Then cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeBodega_muelles.Imagen))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeBodega_muelles.Activo))
            cmd.Parameters.Add(New SqlParameter("@ENTRADA", oBeBodega_muelles.Entrada))
            cmd.Parameters.Add(New SqlParameter("@SALIDA", oBeBodega_muelles.Salida))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONDEFECTO", oBeBodega_muelles.IdUbicacionDefecto))


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


    Public Shared Function Eliminar(ByRef oBeBodega_muelles As clsBeBodega_muelles, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Bodega_muelles" &
             "  Where(IdMuelle = @IdMuelle)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDMUELLE", oBeBodega_muelles.IdMuelle))

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

            Const sp As String = " Delete from Bodega_muelles"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            Return rowsAffected

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Bodega_muelles"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeBodega_muelles As clsBeBodega_muelles) As Boolean

        Try

            Const sp As String = "SELECT * FROM Bodega_muelles" &
            " Where(IdMuelle = @IdMuelle)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMUELLE", oBeBodega_muelles.IDMUELLE))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeBodega_muelles, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeBodega_muelles)

        Try

            Dim lReturnList As New List(Of clsBeBodega_muelles)
            Const sp As String = "SELECT * FROM Bodega_muelles"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeBodega_muelles As New clsBeBodega_muelles

            For Each dr As DataRow In dt.Rows

                vBeBodega_muelles = New clsBeBodega_muelles
                Cargar(vBeBodega_muelles, dr)
                lReturnList.Add(vBeBodega_muelles)

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

    Public Shared Function GetSingle(ByRef pBeBodega_muelles As clsBeBodega_muelles)

        Try

            Const sp As String = "SELECT * FROM Bodega_muelles 
			 Where(IdMuelle = @IdMuelle) "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMUELLE", pBeBodega_muelles.IDMUELLE))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeBodega_muelles, dt.Rows(0))
            End If

            Return True


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeBodega_muelles As clsBeBodega_muelles,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Bodega_muelles 
			 Where(IdMuelle = @IdMuelle) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMUELLE", pBeBodega_muelles.IdMuelle))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeBodega_muelles, dt.Rows(0))
                Return True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdMuelle_Default_By_IdBodega(ByVal IdBodega As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As Integer


        Get_IdMuelle_Default_By_IdBodega = 0

        Try

            Const sp As String = "SELECT top(1) IdMuelle FROM Bodega_muelles 
                                  Where(IdBodega= @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Return dt.Rows(0).Item("IdMuelle")
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdMuelle),0) FROM Bodega_muelles"

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


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdMuelle As Integer) As clsBeBodega_muelles

        GetSingle = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Bodega_muelles 
			                       Where(IdMuelle = @IdMuelle) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMUELLE", IdMuelle))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega_muelles As New clsBeBodega_muelles
                Cargar(pBeBodega_muelles, dt.Rows(0))
                GetSingle = pBeBodega_muelles
            End If

            lTransaction.Commit()

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

    Public Shared Function Get_All_By_IdBodega(ByVal IdBodega) As List(Of clsBeBodega_muelles)

        Try

            Dim lReturnList As New List(Of clsBeBodega_muelles)
            Const sp As String = "SELECT * FROM Bodega_muelles WHERE IdBodega = @IdBodega "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeBodega_muelles As New clsBeBodega_muelles

            For Each dr As DataRow In dt.Rows

                vBeBodega_muelles = New clsBeBodega_muelles
                Cargar(vBeBodega_muelles, dr)
                lReturnList.Add(vBeBodega_muelles)

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

    Public Shared Function Get_IdUbicacion_By_IdMuelle(ByVal IdBodega As Integer,
                                                       ByVal IdMuelle As Integer,
                                                       ByVal lConnection As SqlConnection,
                                                       ByVal lTransaction As SqlTransaction) As Integer


        Get_IdUbicacion_By_IdMuelle = 0

        Try

            Const sp As String = "SELECT IdUbicacionDefecto FROM Bodega_muelles 
                                  Where(IdBodega= @IdBodega AND IdMuelle = @IdMuelle)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMUELLE", IdMuelle))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Return IIf(IsDBNull(dt.Rows(0).Item("IdUbicacionDefecto")), 0, dt.Rows(0).Item("IdUbicacionDefecto"))
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdBodegaMuelle(IdBodegaMuelle As Integer,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As clsBeBodega_muelles

        Get_Single_By_IdBodegaMuelle = Nothing

        Try

            Const sp As String = "SELECT * FROM Bodega_muelles 
			 Where(IdMuelle = @IdMuelle) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMUELLE", IdBodegaMuelle))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeBodega_muelles As New clsBeBodega_muelles
                Cargar(pBeBodega_muelles, dt.Rows(0))
                Return pBeBodega_muelles
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdMuelle_By_IdBodega(ByVal IdBodega As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As Integer


        Get_IdMuelle_By_IdBodega = 0

        Try

            Const sp As String = "SELECT IdMuelle FROM Bodega_muelles 
                                  Where(IdBodega= @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", IdBodega))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Return IIf(IsDBNull(dt.Rows(0).Item("IdMuelle")), 0, dt.Rows(0).Item("IdMuelle"))
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
