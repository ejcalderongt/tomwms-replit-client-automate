Imports System.Data.SqlClient

Public Class clsLnProducto_marca

    Public Shared Sub Cargar(ByRef oBeProducto_marca As clsBeProducto_marca, ByRef dr As DataRow)
        Try
            With oBeProducto_marca
                .IdMarca = IIf(IsDBNull(dr.Item("IdMarca")), 0, dr.Item("IdMarca"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), 0, dr.Item("Codigo"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_marca As clsBeProducto_marca, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("producto_marca")
            Ins.Add("idmarca", "@idmarca", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMARCA", oBeProducto_marca.IdMarca))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto_marca.Codigo))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto_marca.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto_marca.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_marca.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_marca.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_marca.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_marca.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_marca.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeProducto_marca As clsBeProducto_marca, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("producto_marca")
            Upd.Add("idmarca", "@idmarca", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdMarca = @IdMarca")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If
            cmd.Parameters.Add(New SqlParameter("@IDMARCA", oBeProducto_marca.IdMarca))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto_marca.Codigo))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto_marca.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto_marca.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_marca.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_marca.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_marca.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_marca.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_marca.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeProducto_marca As clsBeProducto_marca, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Producto_marca" &
             "  Where(IdMarca = @IdMarca)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMARCA", oBeProducto_marca.IdMarca))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Producto_marca"
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

    Public Shared Function Get_Single_By_Codigo(ByVal pCodigo As String, pConnection As SqlConnection, pTransaction As SqlTransaction) As clsBeProducto_marca


        Get_Single_By_Codigo = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_marca WHERE Codigo = '" + pCodigo + "'"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeProductoMarca As New clsBeProducto_marca()
                    Cargar(BeProductoMarca, lRow)
                    Return BeProductoMarca

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try



    End Function

    Public Shared Function Get_Single_By_Nombre_By_IdPropietario(ByVal pNombre As String,
                                                                 ByVal pIdPropietario As Integer,
                                                                 pConnection As SqlConnection,
                                                                 pTransaction As SqlTransaction) As clsBeProducto_marca


        Get_Single_By_Nombre_By_IdPropietario = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_marca 
                                  WHERE IdPropietario=@IdPropietario Nombre like '%" + pNombre + "%'"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPropietario", pIdPropietario))
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeProducto_marca()
                    Cargar(Obj, lRow)

                    Get_Single_By_Nombre_By_IdPropietario = Obj

                    Return Get_Single_By_Nombre_By_IdPropietario

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try



    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(MAX(IdMarca),0) + 1 as nuevo FROM producto_marca"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction)
                lCommand.CommandType = CommandType.Text
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

    Public Shared Function Get_Single_By_Nombre(ByVal pCodigo As String, pConnection As SqlConnection, pTransaction As SqlTransaction) As clsBeProducto_marca


        Get_Single_By_Nombre = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_marca WHERE Nombre like '" + pCodigo + "%'"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeProducto_marca()
                    Cargar(Obj, lRow)
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try



    End Function

End Class
