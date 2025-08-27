Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnProducto_estado_ubic

    Public Shared Sub Cargar(ByRef oBeProducto_estado_ubic As clsBeProducto_estado_ubic, ByRef dr As DataRow)
        Try
            With oBeProducto_estado_ubic
                .IdProductoEstadUbic = IIf(IsDBNull(dr.Item("IdProductoEstadUbic")), 0, dr.Item("IdProductoEstadUbic"))
                .IdEstado = IIf(IsDBNull(dr.Item("IdEstado")), 0, dr.Item("IdEstado"))
                .IdUbicacionDefecto = IIf(IsDBNull(dr.Item("IdUbicacionDefecto")), 0, dr.Item("IdUbicacionDefecto"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), False, dr.Item("IdBodega"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_estado_ubic As clsBeProducto_estado_ubic, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("producto_estado_ubic")
            Ins.Add("idproductoestadubic", "@idproductoestadubic", DataType.Parametro)
            Ins.Add("idestado", "@idestado", DataType.Parametro)
            Ins.Add("idubicaciondefecto", "@idubicaciondefecto", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("IdBodega", "@IdBodega", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADUBIC", oBeProducto_estado_ubic.IdProductoEstadUbic))
            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeProducto_estado_ubic.IdEstado))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONDEFECTO", oBeProducto_estado_ubic.IdUbicacionDefecto))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_estado_ubic.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_estado_ubic.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_estado_ubic.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_estado_ubic.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_estado_ubic.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeProducto_estado_ubic.IdBodega))

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
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeProducto_estado_ubic As clsBeProducto_estado_ubic, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("producto_estado_ubic")
            Upd.Add("idproductoestadubic", "@idproductoestadubic", DataType.Parametro)
            Upd.Add("idestado", "@idestado", DataType.Parametro)
            Upd.Add("idubicaciondefecto", "@idubicaciondefecto", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("IdBodega", "@IdBodega", DataType.Parametro)
            Upd.Where("IdProductoEstadUbic = @IdProductoEstadUbic")

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

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADUBIC", oBeProducto_estado_ubic.IdProductoEstadUbic))
            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeProducto_estado_ubic.IdEstado))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONDEFECTO", oBeProducto_estado_ubic.IdUbicacionDefecto))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_estado_ubic.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_estado_ubic.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_estado_ubic.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_estado_ubic.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_estado_ubic.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeProducto_estado_ubic.IdBodega))

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
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeProducto_estado_ubic As clsBeProducto_estado_ubic, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Producto_estado_ubic" &
             "  Where(IdProductoEstadUbic = @IdProductoEstadUbic)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADUBIC", oBeProducto_estado_ubic.IdProductoEstadUbic))

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

            Const sp As String = " Delete from Producto_estado_ubic"
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
            If lConnection.State = ConnectionState.Open Then lConnection.Close
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Producto_estado_ubic"
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

    Public Shared Function Obtener(ByRef oBeProducto_estado_ubic As clsBeProducto_estado_ubic) As Boolean

        Try

            Const sp As String = "SELECT * FROM Producto_estado_ubic" & _
            " Where(IdProductoEstadUbic = @IdProductoEstadUbic)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADUBIC", oBeProducto_estado_ubic.IDPRODUCTOESTADUBIC))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_estado_ubic, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeProducto_estado_ubic)

        Try

            Dim lReturnList As New List(Of clsBeProducto_estado_ubic)
            Const sp As String = "SELECT * FROM Producto_estado_ubic"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeProducto_estado_ubic As New clsBeProducto_estado_ubic

            For Each dr As DataRow In dt.Rows

                vBeProducto_estado_ubic = New clsBeProducto_estado_ubic
                Cargar(vBeProducto_estado_ubic, dr)
                lReturnList.Add(vBeProducto_estado_ubic)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeProducto_estado_ubic As clsBeProducto_estado_ubic)

        Try

            Const sp As String = "SELECT * FROM Producto_estado_ubic" & _
            " Where(IdProductoEstadUbic = @IdProductoEstadUbic)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADUBIC", pBeProducto_estado_ubic.IDPRODUCTOESTADUBIC))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeProducto_estado_ubic, dt.Rows(0))
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

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdProductoEstadUbic),0) FROM Producto_estado_ubic"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
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

End Class
