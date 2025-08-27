Imports System.Data.SqlClient

Public Class clsLnProducto_clasificacion

    Public Shared lProductoClasificacionInMemory As New List(Of clsBeProducto_clasificacion)

    Public Shared Sub Cargar(ByRef oBeProducto_clasificacion As clsBeProducto_clasificacion, ByRef dr As DataRow)

        Try

            With oBeProducto_clasificacion

                .IdClasificacion = IIf(IsDBNull(dr.Item("IdClasificacion")), 0, dr.Item("IdClasificacion"))

                If .Propietario Is Nothing Then
                    .Propietario = New clsBePropietarios()
                End If

                .Propietario.IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_clasificacion As clsBeProducto_clasificacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Ins.Init("producto_clasificacion")
            Ins.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeProducto_clasificacion.IdClasificacion))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto_clasificacion.Propietario.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto_clasificacion.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto_clasificacion.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_clasificacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeProducto_clasificacion.Sistema))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_clasificacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_clasificacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_clasificacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_clasificacion.Fec_mod))

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

    Public Shared Function Actualizar(ByRef oBeProducto_clasificacion As clsBeProducto_clasificacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            Upd.Init("producto_clasificacion")
            Upd.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdClasificacion = @IdClasificacion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeProducto_clasificacion.IdClasificacion))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto_clasificacion.Propietario.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto_clasificacion.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto_clasificacion.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_clasificacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeProducto_clasificacion.Sistema))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_clasificacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_clasificacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_clasificacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_clasificacion.Fec_mod))

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

    Public Shared Function Eliminar(ByRef oBeProducto_clasificacion As clsBeProducto_clasificacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Producto_clasificacion " &
                               "  Where(IdClasificacion = @IdClasificacion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeProducto_clasificacion.IdClasificacion))

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

            Const sp As String = "SELECT * FROM Producto_clasificacion"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Listar = dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
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

            Const sp As String = "SELECT IdClasificacion, Codigo, Nombre from producto_clasificacion where activo =1 "
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

End Class
