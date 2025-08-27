Imports System.Data.SqlClient
Public Class clsLnProducto_talla_color

    Public Shared Sub Cargar(ByRef oBeProducto_talla_color As clsBeProducto_talla_color, ByRef dr As DataRow)
        Try
            With oBeProducto_talla_color
                .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .IdTalla = IIf(IsDBNull(dr.Item("IdTalla")), 0, dr.Item("IdTalla"))
                .IdColor = IIf(IsDBNull(dr.Item("IdColor")), 0, dr.Item("IdColor"))
                .CodigoSKU = IIf(IsDBNull(dr.Item("CodigoSKU")), "", dr.Item("CodigoSKU"))
                .IdCampaña = IIf(IsDBNull(dr.Item("IdCampaña")), 0, dr.Item("IdCampaña"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), New Date(1900, 1, 1), dr.Item("fec_agr"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), New Date(1900, 1, 1), dr.Item("fec_mod"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_talla_color As clsBeProducto_talla_color, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("producto_talla_color")
            Ins.Add("idproductotallacolor", "@idproductotallacolor", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idproducto", "@idproducto", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idtalla", "@idtalla", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idcolor", "@idcolor", WMSTipoDato.Tipo.Parametro)
            Ins.Add("codigosku", "@codigosku", WMSTipoDato.Tipo.Parametro)
            Ins.Add("idcampaña", "@idcampaña", WMSTipoDato.Tipo.Parametro)
            Ins.Add("fec_agr", "@fec_agr", WMSTipoDato.Tipo.Parametro)
            Ins.Add("user_agr", "@user_agr", WMSTipoDato.Tipo.Parametro)
            Ins.Add("fec_mod", "@fec_mod", WMSTipoDato.Tipo.Parametro)
            Ins.Add("user_mod", "@user_mod", WMSTipoDato.Tipo.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeProducto_talla_color.IdProductoTallaColor))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_talla_color.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDTALLA", oBeProducto_talla_color.IdTalla))
            cmd.Parameters.Add(New SqlParameter("@IDCOLOR", oBeProducto_talla_color.IdColor))
            cmd.Parameters.Add(New SqlParameter("@CODIGOSKU", oBeProducto_talla_color.CodigoSKU))
            cmd.Parameters.Add(New SqlParameter("@IDCAMPAÑA", oBeProducto_talla_color.IdCampaña))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_talla_color.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_talla_color.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_talla_color.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_talla_color.User_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Actualizar(ByRef oBeProducto_talla_color As clsBeProducto_talla_color, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("producto_talla_color")
            Upd.Add("idproductotallacolor", "@idproductotallacolor", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idproducto", "@idproducto", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idtalla", "@idtalla", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idcolor", "@idcolor", WMSTipoDato.Tipo.Parametro)
            Upd.Add("codigosku", "@codigosku", WMSTipoDato.Tipo.Parametro)
            Upd.Add("idcampaña", "@idcampaña", WMSTipoDato.Tipo.Parametro)
            Upd.Add("fec_agr", "@fec_agr", WMSTipoDato.Tipo.Parametro)
            Upd.Add("user_agr", "@user_agr", WMSTipoDato.Tipo.Parametro)
            Upd.Add("fec_mod", "@fec_mod", WMSTipoDato.Tipo.Parametro)
            Upd.Add("user_mod", "@user_mod", WMSTipoDato.Tipo.Parametro)
            Upd.Where("IdProductoTallaColor = @IdProductoTallaColor")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeProducto_talla_color.IdProductoTallaColor))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_talla_color.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDTALLA", oBeProducto_talla_color.IdTalla))
            cmd.Parameters.Add(New SqlParameter("@IDCOLOR", oBeProducto_talla_color.IdColor))
            cmd.Parameters.Add(New SqlParameter("@CODIGOSKU", oBeProducto_talla_color.CodigoSKU))
            cmd.Parameters.Add(New SqlParameter("@IDCAMPAÑA", oBeProducto_talla_color.IdCampaña))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_talla_color.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_talla_color.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_talla_color.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_talla_color.User_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeProducto_talla_color As clsBeProducto_talla_color, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Producto_talla_color" &
             "  Where(IdProductoTallaColor = @IdProductoTallaColor)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeProducto_talla_color.IdProductoTallaColor))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Producto_talla_color"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeProducto_talla_color)

        Dim lReturnList As New List(Of clsBeProducto_talla_color)

        Try

            Const sp As String = "SELECT * FROM Producto_talla_color"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeProducto_talla_color As New clsBeProducto_talla_color

                        For Each dr As DataRow In lDataTable.Rows
                            vBeProducto_talla_color = New clsBeProducto_talla_color()
                            Cargar(vBeProducto_talla_color, dr)
                            lReturnList.Add(vBeProducto_talla_color)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeProducto_talla_color As clsBeProducto_talla_color)

        Try

            Const sp As String = "SELECT * FROM Producto_talla_color" &
            " Where(IdProductoTallaColor = @IdProductoTallaColor)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeProducto_talla_color As New clsBeProducto_talla_color

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeProducto_talla_color, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdProductoTallaColor),0) FROM Producto_talla_color"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

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

    Public Shared Function Existe(ByVal idProductoTallaColor As Integer, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim cmd As New SqlCommand()
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sql As String = "SELECT COUNT(idproductotallacolor) FROM producto_talla_color WHERE idproductotallacolor = @idproductotallacolor"

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sql, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sql, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idproductotallacolor", idProductoTallaColor))

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return (count > 0)

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Sub InsertOrUpdate(ByRef oBeProducto_talla_color As clsBeProducto_talla_color, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing)
        Try
            If ExisteBySKU(oBeProducto_talla_color.CodigoSKU, pConection, pTransaction) Then
                Actualizar(oBeProducto_talla_color, pConection, pTransaction)
            Else
                oBeProducto_talla_color.IdProductoTallaColor = MaxID(pConection, pTransaction) + 1
                Insertar(oBeProducto_talla_color, pConection, pTransaction)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function ExisteBySKU(ByVal CodigoSku As String, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim cmd As New SqlCommand()
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sql As String = "SELECT COUNT(idproductotallacolor) FROM producto_talla_color WHERE CodigoSKU = @CodigoSKU"

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sql, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sql, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@CodigoSKU", CodigoSku))

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return (count > 0)

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function Get_All_Dt_By_IdCampaña(IdCampaña As Integer) As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "select ptc.IdProductoTallaColor as Codigo, 
									t.Descripcion as Color,
									c.Nombre as Talla,
									ca.Nombre as Campaña,
									ptc.CodigoSKU
									from producto_talla_color ptc
									inner join talla t on ptc.IdTalla = t.IdTalla
									inner join color c on ptc.IdColor = c.IdColor
									inner join campaña ca on ptc.IdCampaña = ca.IdCampaña 
									WHERE ptc.IdCampaña= @IdCampaña "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdCampaña", IdCampaña)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Dt_By_IdProducto(IdProducto As Integer) As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "select IdProductoTallaColor as Codigo, 
									t.Descripcion as Color,
									c.Nombre as Talla,
									ca.Nombre as Campaña,
									ptc.CodigoSKU 
									from producto_talla_color ptc
									inner join talla t on ptc.IdTalla = t.IdTalla
									inner join color c on ptc.IdColor = c.IdColor
									inner join campaña ca on ptc.IdCampaña = ca.IdCampaña 
									WHERE ptc.IdProducto = @IdProducto "

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function MaxID(lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdProductoTallaColor),0) FROM Producto_talla_color"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function
End Class