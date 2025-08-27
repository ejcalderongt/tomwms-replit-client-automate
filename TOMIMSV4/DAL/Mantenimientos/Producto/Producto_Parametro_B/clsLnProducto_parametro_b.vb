Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnProducto_parametro_b

    Public Shared Sub Cargar(ByRef oBeProducto_parametro_b As clsBeProducto_parametro_b, ByRef dr As DataRow)
        Try
            With oBeProducto_parametro_b
                .IdProductoParametroB = IIf(IsDBNull(dr.Item("IdProductoParametroB")), 0, dr.Item("IdProductoParametroB"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_parametro_b As clsBeProducto_parametro_b, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("producto_parametro_b")
            Ins.Add("idproductoparametrob", "@idproductoparametrob", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETROB", oBeProducto_parametro_b.IdProductoParametroB))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto_parametro_b.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeProducto_parametro_b.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_parametro_b.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_parametro_b.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_parametro_b.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_parametro_b.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_parametro_b.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeProducto_parametro_b As clsBeProducto_parametro_b, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("producto_parametro_b")
            Upd.Add("idproductoparametrob", "@idproductoparametrob", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdProductoParametroB = @IdProductoParametroB")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETROB", oBeProducto_parametro_b.IdProductoParametroB))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto_parametro_b.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeProducto_parametro_b.Nombre))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_parametro_b.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_parametro_b.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_parametro_b.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_parametro_b.User_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_parametro_b.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeProducto_parametro_b As clsBeProducto_parametro_b, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Producto_parametro_b" & _
             "  Where(IdProductoParametroB = @IdProductoParametroB)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETROB", oBeProducto_parametro_b.IdProductoParametroB))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT IdProductoParametroB,Codigo,Nombre 
								  FROM Producto_parametro_b
								  where activo=1"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection is Nothing Then lConnection.Dispose()
            If Not lTransaction is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeProducto_parametro_b)

        Dim lReturnList As New List(Of clsBeProducto_parametro_b)

        Try

            Const sp As String = "SELECT * FROM Producto_parametro_b"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeProducto_parametro_b As New clsBeProducto_parametro_b

                        For Each dr As DataRow In lDataTable.Rows
                            vBeProducto_parametro_b = New clsBeProducto_parametro_b()
                            Cargar(vBeProducto_parametro_b, dr)
                            lReturnList.Add(vBeProducto_parametro_b)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef IdProductoParametroB As Integer) As clsBeProducto_parametro_b

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Producto_parametro_b" &
            " Where(IdProductoParametroB = @IdProductoParametroB)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IDPRODUCTOPARAMETROB", IdProductoParametroB)


                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeProducto_parametro_b As New clsBeProducto_parametro_b

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeProducto_parametro_b, lDataTable.Rows(0))
                            GetSingle = vBeProducto_parametro_b
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdProductoParametroB),0) FROM Producto_parametro_b"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdProductoParametroB),0) FROM Producto_parametro_b "

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

                lCommand.Dispose()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeProducto_parametro_b)

        Dim lReturnList As New List(Of clsBeProducto_parametro_b)

        Get_All_Filtro = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = ""

                vSQL = String.Format("SELECT * FROM producto_parametro_b WHERE 1=1 ")


                If pActivo = True Then
                    vSQL += " AND Activo=1"
                Else
                    vSQL += " AND Activo=0"
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_parametro_b

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_parametro_b
                            Cargar(Obj, lRow)

                            'If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                            '	Obj.Propietario = New clsBePropietarios
                            '	Obj.Propietario.IdPropietario = CType(lRow("IdPropietario"), Int32)
                            '	Obj.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                            'End If

                            lReturnList.Add(Obj)
                        Next

                        Get_All_Filtro = lReturnList

                    End If

                End Using

            End Using

            Return Get_All_Filtro

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Parametro_By_IdParametroB(ByVal IdProductoParametroB As Integer) As Boolean

        Existe_Parametro_By_IdParametroB = False

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto_parametro_b AS s 
                                                  INNER JOIN producto AS p ON s.IdProductoParametroB = p.IdProductoParametroB 
                                  WHERE p.IdProductoParametroB=@IdProductoParametroB"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdProductoParametroB", IdProductoParametroB)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0

                            Existe_Parametro_By_IdParametroB = lExists
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
