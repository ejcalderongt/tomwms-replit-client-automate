Imports System.Data.SqlClient

Public Class clsLnProducto_rellenado

    Public Shared Sub Cargar(ByRef oBeProducto_rellenado As clsBeProducto_rellenado, ByRef dr As DataRow)

        Try

            With oBeProducto_rellenado

                .IdRellenado = IIf(IsDBNull(dr.Item("IdRellenado")), 0, dr.Item("IdRellenado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdTipoAccion = IIf(IsDBNull(dr.Item("IdTipoAccion")), 0, dr.Item("IdTipoAccion"))
                .Minimo = IIf(IsDBNull(dr.Item("Minimo")), 0.0, dr.Item("Minimo"))
                .Maximo = IIf(IsDBNull(dr.Item("Maximo")), 0.0, dr.Item("Maximo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdUnidadMedidaBasica = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .Activo = IIf(IsDBNull(dr.Item("Activo")), False, dr.Item("Activo"))
                .IdPresentacionAbastercerCon = IIf(IsDBNull(dr.Item("IdPresentacionAbastercerCon")), 0, dr.Item("IdPresentacionAbastercerCon"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .NomPresentacionRellenarCon = IIf(IsDBNull(dr.Item("NomPresentacionRellenarCon")), "", dr.Item("NomPresentacionRellenarCon"))
                .IdOperadorDefecto = IIf(IsDBNull(dr.Item("IdOperadorDefecto")), 0, dr.Item("IdOperadorDefecto"))
                .NomOperador = IIf(IsDBNull(dr.Item("NomOperador")), "", dr.Item("NomOperador"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_rellenado As clsBeProducto_rellenado, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Ins.Init("producto_rellenado")
            Ins.Add("idrellenado", "@idrellenado", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Ins.Add("IdBodega", "@IdBodega", DataType.Parametro)
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idtipoaccion", "@idtipoaccion", DataType.Parametro)
            Ins.Add("minimo", "@minimo", DataType.Parametro)
            Ins.Add("maximo", "@maximo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("IdUnidadMedidaBasica", "@IdUnidadMedidaBasica", DataType.Parametro)
            Ins.Add("IdPropietario", "@IdPropietario", DataType.Parametro)
            Ins.Add("IdProductoBodega", "@IdProductoBodega", DataType.Parametro)
            'Ins.Add("IdUmBasAbastercerCon", "@IdUmBasAbastercerCon", DataType.Parametro)
            Ins.Add("IdPresentacionAbastercerCon", "@IdPresentacionAbastercerCon", DataType.Parametro)
            'Ins.Add("NomPresentacionRellenarCon", "@NomPresentacionRellenarCon", DataType.Parametro)
            'Ins.Add("NomUmBasAbastecerCon", "@NomUmBasAbastecerCon", DataType.Parametro)
            Ins.Add("IdOperadorDefecto", "@IdOperadorDefecto", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRELLENADO", oBeProducto_rellenado.IdRellenado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeProducto_rellenado.IdPresentacion = 0, DBNull.Value, oBeProducto_rellenado.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", IIf(oBeProducto_rellenado.IdProductoEstado = 0, DBNull.Value, oBeProducto_rellenado.IdProductoEstado)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", IIf(oBeProducto_rellenado.IdUbicacion = 0, DBNull.Value, oBeProducto_rellenado.IdUbicacion)))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOACCION", IIf(oBeProducto_rellenado.IdTipoAccion = 0, DBNull.Value, oBeProducto_rellenado.IdTipoAccion)))
            cmd.Parameters.Add(New SqlParameter("@MINIMO", oBeProducto_rellenado.Minimo))
            cmd.Parameters.Add(New SqlParameter("@MAXIMO", oBeProducto_rellenado.Maximo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_rellenado.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_rellenado.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_rellenado.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_rellenado.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_rellenado.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeProducto_rellenado.IdUnidadMedidaBasica))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeProducto_rellenado.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeProducto_rellenado.IdProductoBodega))
            'cmd.Parameters.Add(New SqlParameter("@IDUMBASABASTERCERCON", oBeProducto_rellenado.IdUmBasAbastercerCon))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONABASTERCERCON", oBeProducto_rellenado.IdPresentacionAbastercerCon))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto_rellenado.IdPropietario))
            'cmd.Parameters.Add(New SqlParameter("@NOMPRESENTACIONRELLENARCON", oBeProducto_rellenado.NomPresentacionRellenarCon))
            'cmd.Parameters.Add(New SqlParameter("@NOMUMBASABASTECERCON", oBeProducto_rellenado.NomUmBasAbastecerCon))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORDEFECTO", oBeProducto_rellenado.IdOperadorDefecto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

            oBeProducto_rellenado.IdRellenado = CInt(cmd.Parameters("@IDRELLENADO").Value)

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

    Public Shared Function Actualizar(ByRef oBeProducto_rellenado As clsBeProducto_rellenado, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("producto_rellenado")
            Upd.Add("idrellenado", "@idrellenado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idproductoestado", "@idproductoestado", DataType.Parametro)
            Upd.Add("IdBodega", "@IdBodega", DataType.Parametro)
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("idtipoaccion", "@idtipoaccion", DataType.Parametro)
            Upd.Add("minimo", "@minimo", DataType.Parametro)
            Upd.Add("maximo", "@maximo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("IdUnidadMedidaBasica", "@IdUnidadMedidaBasica", DataType.Parametro)
            Upd.Add("IdPropietario", "@IdPropietario", DataType.Parametro)
            Upd.Add("IdProductoBodega", "@IdProductoBodega", DataType.Parametro)
            'Upd.Add("IdUmBasAbastercerCon", "@IdUmBasAbastercerCon", DataType.Parametro)
            Upd.Add("IdPresentacionAbastercerCon", "@IdPresentacionAbastercerCon", DataType.Parametro)
            'Upd.Add("NomPresentacionRellenarCon", "@NomPresentacionRellenarCon", DataType.Parametro)
            'Upd.Add("NomUmBasAbastecerCon", "@NomUmBasAbastecerCon", DataType.Parametro)
            Upd.Add("IdOperadorDefecto", "@IdOperadorDefecto", DataType.Parametro)
            Upd.Where("IdRellenado = @IdRellenado")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRELLENADO", oBeProducto_rellenado.IdRellenado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeProducto_rellenado.IdPresentacion = 0, DBNull.Value, oBeProducto_rellenado.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADO", IIf(oBeProducto_rellenado.IdProductoEstado = 0, DBNull.Value, oBeProducto_rellenado.IdProductoEstado)))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", IIf(oBeProducto_rellenado.IdUbicacion = 0, DBNull.Value, oBeProducto_rellenado.IdUbicacion)))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOACCION", IIf(oBeProducto_rellenado.IdTipoAccion = 0, DBNull.Value, oBeProducto_rellenado.IdTipoAccion)))
            cmd.Parameters.Add(New SqlParameter("@MINIMO", oBeProducto_rellenado.Minimo))
            cmd.Parameters.Add(New SqlParameter("@MAXIMO", oBeProducto_rellenado.Maximo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_rellenado.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_rellenado.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_rellenado.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_rellenado.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_rellenado.Activo))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeProducto_rellenado.IdUnidadMedidaBasica))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeProducto_rellenado.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeProducto_rellenado.IdProductoBodega))
            'cmd.Parameters.Add(New SqlParameter("@IDUMBASABASTERCERCON", oBeProducto_rellenado.IdUmBasAbastercerCon))
            'cmd.Parameters.Add(New SqlParameter("@NOMUMBASABASTECERCON", oBeProducto_rellenado.NomUmBasAbastecerCon))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONABASTERCERCON", oBeProducto_rellenado.IdPresentacionAbastercerCon))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto_rellenado.IdPropietario))
            'cmd.Parameters.Add(New SqlParameter("@NOMPRESENTACIONRELLENARCON", oBeProducto_rellenado.NomPresentacionRellenarCon))
            cmd.Parameters.Add(New SqlParameter("@IDOPERADORDEFECTO", oBeProducto_rellenado.IdOperadorDefecto))

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


    Public Shared Function Eliminar(ByRef oBeProducto_rellenado As clsBeProducto_rellenado, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Producto_rellenado" &
             "  Where(IdRellenado = @IdRellenado)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRELLENADO", oBeProducto_rellenado.IdRellenado))

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

            Const sp As String = "SELECT * FROM Producto_rellenado"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Obtener(ByRef oBeProducto_rellenado As clsBeProducto_rellenado) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Producto_rellenado" &
            " Where(IdRellenado = @IdRellenado)"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRELLENADO", oBeProducto_rellenado.IdRellenado))

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_rellenado, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
