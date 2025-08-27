Imports System.Data.SqlClient

Public Class clsLnProducto_tipo

    Public Shared Sub Cargar(ByRef oBeProducto_tipo As clsBeProducto_tipo, ByRef dr As DataRow)

        Try

            With oBeProducto_tipo
                .IdTipoProducto = IIf(IsDBNull(dr.Item("IdTipoProducto")), 0, dr.Item("IdTipoProducto"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .NombreTipoProducto = IIf(IsDBNull(dr.Item("NombreTipoProducto")), "", dr.Item("NombreTipoProducto"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_tipo As clsBeProducto_tipo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("producto_tipo")
            Ins.Add("idtipoproducto", "@idtipoproducto", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombretipoproducto", "@nombretipoproducto", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTO", oBeProducto_tipo.IdTipoProducto))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto_tipo.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto_tipo.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRETIPOPRODUCTO", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto_tipo.NombreTipoProducto)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_tipo.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_tipo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_tipo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_tipo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_tipo.Fec_mod))

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

    Public Shared Function Actualizar(ByRef oBeProducto_tipo As clsBeProducto_tipo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("producto_tipo")
            Upd.Add("idtipoproducto", "@idtipoproducto", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombretipoproducto", "@nombretipoproducto", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdTipoProducto = @IdTipoProducto")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If
            cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTO", oBeProducto_tipo.IdTipoProducto))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto_tipo.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto_tipo.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRETIPOPRODUCTO", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto_tipo.NombreTipoProducto)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_tipo.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_tipo.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_tipo.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_tipo.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_tipo.Fec_mod))

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

    Public Shared Function Eliminar(ByRef oBeProducto_tipo As clsBeProducto_tipo, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Producto_tipo" &
             "  Where(IdTipoProducto = @IdTipoProducto)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTO", oBeProducto_tipo.IdTipoProducto))

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

            Const sp As String = "SELECT * FROM Producto_tipo"
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

    Public Shared Function Obtener(ByRef oBeProducto_tipo As clsBeProducto_tipo) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Producto_tipo" &
            " Where(IdTipoProducto = @IdTipoProducto)"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTO", oBeProducto_tipo.IdTipoProducto))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_tipo, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Nombre_By_IdPropietario(ByVal pNombre As String, ByVal pIdPropietario As Integer, pConnection As SqlConnection, pTransaction As SqlTransaction) As clsBeProducto_tipo

        Get_Single_By_Nombre_By_IdPropietario = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_tipo 
                                  WHERE IdPropietario=@IdPropietario NombreTipoProducto like '%" & pNombre & "%'"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPropietario", pIdPropietario))
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeProducto_tipo()
                    Cargar(Obj, lRow)
                    Get_Single_By_Nombre_By_IdPropietario = Obj
                    Return Get_Single_By_Nombre_By_IdPropietario

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdTipoProducto(ByVal pIdTipoProducto As String, pConnection As SqlConnection, pTransaction As SqlTransaction) As clsBeProducto_tipo

        Get_Single_By_IdTipoProducto = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_tipo WHERE IdTipoProducto = @IdTipoProducto "

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", pIdTipoProducto)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeProducto_tipo()
                    Cargar(Obj, lRow)
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Codigo(ByVal pNombre As String, pConnection As SqlConnection, pTransaction As SqlTransaction) As clsBeProducto_tipo

        Get_Single_By_Codigo = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_tipo WHERE Codigo = '" & pNombre & "'"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeTipoProducto As New clsBeProducto_tipo()
                    Cargar(BeTipoProducto, lRow)
                    Return BeTipoProducto

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
