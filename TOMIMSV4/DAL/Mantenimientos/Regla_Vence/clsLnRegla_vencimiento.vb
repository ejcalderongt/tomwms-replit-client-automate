Imports System.Data.SqlClient

Public Class clsLnRegla_vencimiento

    Public Shared Sub Cargar(ByRef oBeRegla_vencimiento As clsBeRegla_vencimiento, ByRef dr As DataRow)
        Try
            With oBeRegla_vencimiento
                .IdReglaVencimiento = IIf(IsDBNull(dr.Item("IdReglaVencimiento")), 0, dr.Item("IdReglaVencimiento"))
                .NombreRegla = IIf(IsDBNull(dr.Item("NombreRegla")), "", dr.Item("NombreRegla"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdProductoFamilia = IIf(IsDBNull(dr.Item("IdProductoFamilia")), 0, dr.Item("IdProductoFamilia"))
                .IdProductoClasificacion = IIf(IsDBNull(dr.Item("IdProductoClasificacion")), 0, dr.Item("IdProductoClasificacion"))
                .TiempoVencimientoDias = IIf(IsDBNull(dr.Item("TiempoVencimientoDias")), 0, dr.Item("TiempoVencimientoDias"))
                .TipoNotificacion = IIf(IsDBNull(dr.Item("TipoNotificacion")), "", dr.Item("TipoNotificacion"))
                .IdPropietarioMercancia = IIf(IsDBNull(dr.Item("IdPropietarioMercancia")), 0, dr.Item("IdPropietarioMercancia"))
                .IdProveedor = IIf(IsDBNull(dr.Item("IdProveedor")), 0, dr.Item("IdProveedor"))
                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .Activa = IIf(IsDBNull(dr.Item("Activa")), False, dr.Item("Activa"))
                .FechaCreacion = IIf(IsDBNull(dr.Item("FechaCreacion")), Date.Now, dr.Item("FechaCreacion"))
                .UsuarioCreacion = IIf(IsDBNull(dr.Item("UsuarioCreacion")), "", dr.Item("UsuarioCreacion"))
                .FechaModificacion = IIf(IsDBNull(dr.Item("FechaModificacion")), Date.Now, dr.Item("FechaModificacion"))
                .UsuarioModificacion = IIf(IsDBNull(dr.Item("UsuarioModificacion")), "", dr.Item("UsuarioModificacion"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeRegla_vencimiento As clsBeRegla_vencimiento, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("regla_vencimiento")
            Ins.Add("idreglavencimiento", "@idreglavencimiento", DataType.Parametro)
            Ins.Add("nombreregla", "@nombreregla", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            If Not oBeRegla_vencimiento.IdProductoFamilia = 0 Then Ins.Add("idproductofamilia", "@idproductofamilia", DataType.Parametro)
            If Not oBeRegla_vencimiento.IdProductoClasificacion = 0 Then Ins.Add("idproductoclasificacion", "@idproductoclasificacion", DataType.Parametro)
            Ins.Add("tiempovencimientodias", "@tiempovencimientodias", DataType.Parametro)
            Ins.Add("tiponotificacion", "@tiponotificacion", DataType.Parametro)
            Ins.Add("idpropietariomercancia", "@idpropietariomercancia", DataType.Parametro)
            Ins.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Ins.Add("idcliente", "@idcliente", DataType.Parametro)
            Ins.Add("activa", "@activa", DataType.Parametro)
            Ins.Add("fechacreacion", "@fechacreacion", DataType.Parametro)
            Ins.Add("usuariocreacion", "@usuariocreacion", DataType.Parametro)
            Ins.Add("fechamodificacion", "@fechamodificacion", DataType.Parametro)
            Ins.Add("usuariomodificacion", "@usuariomodificacion", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGLAVENCIMIENTO", oBeRegla_vencimiento.IdReglaVencimiento))
            cmd.Parameters.Add(New SqlParameter("@NOMBREREGLA", oBeRegla_vencimiento.NombreRegla))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeRegla_vencimiento.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOFAMILIA", IIf(oBeRegla_vencimiento.IdProductoFamilia = 0, DBNull.Value, oBeRegla_vencimiento.IdProductoFamilia)))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOCLASIFICACION", IIf(oBeRegla_vencimiento.IdProductoClasificacion = 0, DBNull.Value, oBeRegla_vencimiento.IdProductoClasificacion)))
            cmd.Parameters.Add(New SqlParameter("@TIEMPOVENCIMIENTODIAS", oBeRegla_vencimiento.TiempoVencimientoDias))
            cmd.Parameters.Add(New SqlParameter("@TIPONOTIFICACION", oBeRegla_vencimiento.TipoNotificacion))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOMERCANCIA", oBeRegla_vencimiento.IdPropietarioMercancia))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", IIf(oBeRegla_vencimiento.IdProveedor = 0, DBNull.Value, oBeRegla_vencimiento.IdProveedor)))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", IIf(oBeRegla_vencimiento.IdCliente = 0, DBNull.Value, oBeRegla_vencimiento.IdCliente)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVA", oBeRegla_vencimiento.Activa))
            cmd.Parameters.Add(New SqlParameter("@FECHACREACION", oBeRegla_vencimiento.FechaCreacion))
            cmd.Parameters.Add(New SqlParameter("@USUARIOCREACION", oBeRegla_vencimiento.UsuarioCreacion))
            cmd.Parameters.Add(New SqlParameter("@FECHAMODIFICACION", oBeRegla_vencimiento.FechaModificacion))
            cmd.Parameters.Add(New SqlParameter("@USUARIOMODIFICACION", oBeRegla_vencimiento.UsuarioModificacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeRegla_vencimiento As clsBeRegla_vencimiento, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("regla_vencimiento")
            Upd.Add("idreglavencimiento", "@idreglavencimiento", DataType.Parametro)
            Upd.Add("nombreregla", "@nombreregla", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            If Not oBeRegla_vencimiento.IdProductoFamilia = 0 Then Upd.Add("idproductofamilia", "@idproductofamilia", DataType.Parametro)
            If Not oBeRegla_vencimiento.IdProductoClasificacion = 0 Then Upd.Add("idproductoclasificacion", "@idproductoclasificacion", DataType.Parametro)
            Upd.Add("tiempovencimientodias", "@tiempovencimientodias", DataType.Parametro)
            Upd.Add("tiponotificacion", "@tiponotificacion", DataType.Parametro)
            Upd.Add("idpropietariomercancia", "@idpropietariomercancia", DataType.Parametro)
            Upd.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Upd.Add("idcliente", "@idcliente", DataType.Parametro)
            Upd.Add("activa", "@activa", DataType.Parametro)
            Upd.Add("fechacreacion", "@fechacreacion", DataType.Parametro)
            Upd.Add("usuariocreacion", "@usuariocreacion", DataType.Parametro)
            Upd.Add("fechamodificacion", "@fechamodificacion", DataType.Parametro)
            Upd.Add("usuariomodificacion", "@usuariomodificacion", DataType.Parametro)
            Upd.Where("IdReglaVencimiento = @IdReglaVencimiento")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGLAVENCIMIENTO", oBeRegla_vencimiento.IdReglaVencimiento))
            cmd.Parameters.Add(New SqlParameter("@NOMBREREGLA", oBeRegla_vencimiento.NombreRegla))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeRegla_vencimiento.IdBodega))
            If Not oBeRegla_vencimiento.IdProductoFamilia = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOFAMILIA", oBeRegla_vencimiento.IdProductoFamilia))
            If Not oBeRegla_vencimiento.IdProductoClasificacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOCLASIFICACION", oBeRegla_vencimiento.IdProductoClasificacion))
            cmd.Parameters.Add(New SqlParameter("@TIEMPOVENCIMIENTODIAS", oBeRegla_vencimiento.TiempoVencimientoDias))
            cmd.Parameters.Add(New SqlParameter("@TIPONOTIFICACION", oBeRegla_vencimiento.TipoNotificacion))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOMERCANCIA", oBeRegla_vencimiento.IdPropietarioMercancia))

            Dim vProveedor As Object = IIf(oBeRegla_vencimiento.IdProveedor = 0, DBNull.Value, oBeRegla_vencimiento.IdProveedor)
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", vProveedor))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", IIf(oBeRegla_vencimiento.IdCliente = 0, DBNull.Value, oBeRegla_vencimiento.IdCliente)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVA", oBeRegla_vencimiento.Activa))
            cmd.Parameters.Add(New SqlParameter("@FECHACREACION", oBeRegla_vencimiento.FechaCreacion))
            cmd.Parameters.Add(New SqlParameter("@USUARIOCREACION", oBeRegla_vencimiento.UsuarioCreacion))
            cmd.Parameters.Add(New SqlParameter("@FECHAMODIFICACION", oBeRegla_vencimiento.FechaModificacion))
            cmd.Parameters.Add(New SqlParameter("@USUARIOMODIFICACION", oBeRegla_vencimiento.UsuarioModificacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeRegla_vencimiento As clsBeRegla_vencimiento, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Regla_vencimiento" &
             "  Where(IdReglaVencimiento = @IdReglaVencimiento)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREGLAVENCIMIENTO", oBeRegla_vencimiento.IdReglaVencimiento))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Regla_vencimiento"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
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
            If lConnection.State = ConnectionState.Open Then lConnection.Close
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All() As List(Of clsBeRegla_vencimiento)

        Dim lReturnList As New List(Of clsBeRegla_vencimiento)

        Try

            Const sp As String = "SELECT * FROM Regla_vencimiento"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeRegla_vencimiento As New clsBeRegla_vencimiento

                        For Each dr As DataRow In lDataTable.Rows
                            vBeRegla_vencimiento = New clsBeRegla_vencimiento()
                            Cargar(vBeRegla_vencimiento, dr)
                            lReturnList.Add(vBeRegla_vencimiento)
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

    Public Shared Sub GetSingle(ByRef pBeRegla_vencimiento As clsBeRegla_vencimiento)

        Try

            Const sp As String = "SELECT * FROM Regla_vencimiento" &
            " Where(IdReglaVencimiento = @IdReglaVencimiento)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeRegla_vencimiento As New clsBeRegla_vencimiento

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeRegla_vencimiento, lDataTable.Rows(0))
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

    Public Shared Function Get_By_IdReglaVencimiento(ByRef pRegla_vencimiento As Integer) As clsBeRegla_vencimiento

        Get_By_IdReglaVencimiento = Nothing

        Try

            Const sp As String = "SELECT * FROM Regla_vencimiento" &
            " Where(IdReglaVencimiento = @IdReglaVencimiento)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IDREGLAVENCIMIENTO", pRegla_vencimiento))
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeRegla_vencimiento As New clsBeRegla_vencimiento

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeRegla_vencimiento, lDataTable.Rows(0))
                            Get_By_IdReglaVencimiento = vBeRegla_vencimiento
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

            Const sp As String = "SELECT ISNULL(Max(IdReglaVencimiento),0) FROM Regla_vencimiento"

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

    Public Shared Function MaxID(ByVal lconection As SqlConnection, ByVal ltransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdReglaVencimiento),0) FROM Regla_vencimiento "

            Using lCommand As New SqlCommand(sp, lconection, ltransaction)

                lCommand.CommandType = CommandType.Text

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

    '#GT11122023: guardar la lista iterando, similar a servicios.
    Public Shared Sub Guardar_Lista(lReglaVencimientos As List(Of clsBeRegla_vencimiento))

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vMaxId As Integer = MaxID(lConnection, lTransaction) + 1

            For Each pRegla In lReglaVencimientos

                If pRegla.IsNew Then
                    pRegla.IdReglaVencimiento = vMaxId
                    Insertar(pRegla, lConnection, lTransaction)
                    vMaxId += 1
                Else
                    Actualizar(pRegla, lConnection, lTransaction)
                End If

            Next

            lTransaction.Commit()


        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try
    End Sub

    Public Shared Function Existe_Regla(ByVal pReglaVencimiento As clsBeRegla_vencimiento) As Boolean

        Existe_Regla = False

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM Regla_vencimiento 
												  WHERE IdBodega=@IdBodega"


            vSQL += " and Activa=1 
					  and IdPropietarioMercancia=@IdPropietarioMercancia 
                      and IdProveedor=@IdProveedor"

            If pReglaVencimiento.IdProductoFamilia > 0 Then
                vSQL += " and IdProductoFamilia=@IdProductoFamilia "
            End If
            If pReglaVencimiento.IdProductoClasificacion > 0 Then
                vSQL += " and IdProductoClasificacion=@IdProductoClasificacion "
            End If
            If pReglaVencimiento.IdCliente > 0 Then
                vSQL += " and IdCliente=@IdCliente "
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdBodega", pReglaVencimiento.IdBodega)
                        lCommand.Parameters.AddWithValue("@IdPropietarioMercancia", pReglaVencimiento.IdPropietarioMercancia)
                        If pReglaVencimiento.IdProductoFamilia > 0 Then
                            lCommand.Parameters.AddWithValue("@IdProductoFamilia", pReglaVencimiento.IdProductoFamilia)
                        End If
                        If pReglaVencimiento.IdProductoClasificacion > 0 Then
                            lCommand.Parameters.AddWithValue("@IdProductoClasificacion", pReglaVencimiento.IdProductoClasificacion)
                        End If
                        lCommand.Parameters.AddWithValue("@IdProveedor", pReglaVencimiento.IdProveedor)
                        If pReglaVencimiento.IdCliente > 0 Then
                            lCommand.Parameters.AddWithValue("@IdCliente", pReglaVencimiento.IdCliente)
                        End If


                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                        Existe_Regla = lExists

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Regla_Vencimiento(ByVal pReglaVencimiento As clsBeRegla_vencimiento) As clsBeRegla_vencimiento

        Existe_Regla_Vencimiento = Nothing

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT * FROM Regla_vencimiento 
												  WHERE IdBodega=@IdBodega"


            vSQL += " and Activa=1 
					  and IdPropietarioMercancia=@IdPropietarioMercancia 
                      and IdProveedor=@IdProveedor"

            If pReglaVencimiento.IdProductoFamilia > 0 Then
                vSQL += " and IdProductoFamilia=@IdProductoFamilia "
            End If
            If pReglaVencimiento.IdProductoClasificacion > 0 Then
                vSQL += " and IdProductoClasificacion=@IdProductoClasificacion "
            End If
            If pReglaVencimiento.IdCliente > 0 Then
                vSQL += " and IdCliente=@IdCliente "
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    'Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)
                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pReglaVencimiento.IdBodega))
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPropietarioMercancia", pReglaVencimiento.IdPropietarioMercancia))
                        If pReglaVencimiento.IdProductoFamilia > 0 Then
                            lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoFamilia", pReglaVencimiento.IdProductoFamilia))
                        End If
                        If pReglaVencimiento.IdProductoClasificacion > 0 Then
                            lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoClasificacion", pReglaVencimiento.IdProductoClasificacion))
                        End If
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdProveedor", pReglaVencimiento.IdProveedor))
                        If pReglaVencimiento.IdCliente > 0 Then
                            lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdCliente", pReglaVencimiento.IdCliente))
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeRegla_vencimiento As New clsBeRegla_vencimiento

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeRegla_vencimiento, lDataTable.Rows(0))
                            Existe_Regla_Vencimiento = vBeRegla_vencimiento
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

    Public Shared Function Listar_For_Combo() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "select IdReglaVencimiento, cast(IdReglaVencimiento as nvarchar(50)) +' - '+NombreRegla as NombreRegla from regla_vencimiento where Activa=1"
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

End Class
