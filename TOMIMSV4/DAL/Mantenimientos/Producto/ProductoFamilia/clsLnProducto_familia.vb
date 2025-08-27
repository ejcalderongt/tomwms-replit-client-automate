Imports System.Data.SqlClient

Public Class clsLnProducto_familia

    'Public Shared lProductoFamiliaInMemory As New List(Of clsBeProducto_familia)

    Public Shared Sub Cargar(ByRef oBeProducto_familia As clsBeProducto_familia, ByRef dr As DataRow)
        Try
            With oBeProducto_familia
                .IdFamilia = IIf(IsDBNull(dr.Item("IdFamilia")), 0, dr.Item("IdFamilia"))
                If .Propietario Is Nothing Then
                    .Propietario = New clsBePropietarios()
                End If
                .Propietario.IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
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

    Public Shared Function Insertar(ByRef oBeProducto_familia As clsBeProducto_familia, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("producto_familia")
            Ins.Add("idfamilia", "@idfamilia", DataType.Parametro)
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

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeProducto_familia.IdFamilia))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto_familia.Propietario.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto_familia.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto_familia.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_familia.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_familia.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_familia.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_familia.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_familia.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

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

    Public Shared Function Actualizar(ByRef oBeProducto_familia As clsBeProducto_familia, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("producto_familia")
            Upd.Add("idfamilia", "@idfamilia", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdFamilia = @IdFamilia")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If
            cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeProducto_familia.IdFamilia))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto_familia.Propietario.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto_familia.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto_familia.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_familia.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_familia.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_familia.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_familia.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_familia.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

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

    Public Shared Function Eliminar(ByRef oBeProducto_familia As clsBeProducto_familia, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Producto_familia" &
             "  Where(IdFamilia = @IdFamilia)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then

                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)


            End If

            cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeProducto_familia.IdFamilia))

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

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Listar = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Producto_familia "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Listar = dt

        Catch ex As Exception
            lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Listar_For_Grid() As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Listar_For_Grid = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT IdFamilia, Codigo, Nombre from producto_familia where activo = 1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Listar_For_Grid = dt

        Catch ex As Exception
            lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeProducto_familia As clsBeProducto_familia) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = "SELECT * FROM Producto_familia
                                WHERE(IdFamilia = @IdFamilia)"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeProducto_familia.IdFamilia))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_familia, dt.Rows(0))
                Obtener = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

End Class
