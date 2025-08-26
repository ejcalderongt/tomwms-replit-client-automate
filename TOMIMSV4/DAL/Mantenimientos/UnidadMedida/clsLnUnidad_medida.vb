Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnUnidad_medida

    Public Shared Sub Cargar(ByRef oBeUnidad_medida As clsBeUnidad_medida, ByRef dr As DataRow)

        Try

            With oBeUnidad_medida

                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .es_um_cobro = IIf(IsDBNull(dr.Item("es_um_cobro")), False, dr.Item("es_um_cobro"))
                .factor = IIf(IsDBNull(dr.Item("factor")), 1, dr.Item("factor"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeUnidad_medida As clsBeUnidad_medida, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("unidad_medida")
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("factor", "@factor", DataType.Parametro)
            Ins.Add("es_um_cobro", "@es_um_cobro", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeUnidad_medida.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeUnidad_medida.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeUnidad_medida.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeUnidad_medida.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeUnidad_medida.Activo))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeUnidad_medida.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeUnidad_medida.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeUnidad_medida.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeUnidad_medida.User_agr))
            cmd.Parameters.Add(New SqlParameter("@ES_UM_COBRO", oBeUnidad_medida.es_um_cobro))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeUnidad_medida.factor))


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

    Public Shared Function Actualizar(ByRef oBeUnidad_medida As clsBeUnidad_medida, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("unidad_medida")
            Upd.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("factor", "@factor", DataType.Parametro)
            Upd.Add("es_um_cobro", "@es_um_cobro", DataType.Parametro)
            Upd.Where("IdUnidadMedida = @IdUnidadMedida")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeUnidad_medida.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeUnidad_medida.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeUnidad_medida.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeUnidad_medida.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeUnidad_medida.Activo))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeUnidad_medida.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeUnidad_medida.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeUnidad_medida.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeUnidad_medida.User_agr))
            cmd.Parameters.Add(New SqlParameter("@ES_UM_COBRO", oBeUnidad_medida.es_um_cobro))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeUnidad_medida.factor))

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

    Public Shared Function Eliminar(ByRef oBeUnidad_medida As clsBeUnidad_medida, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Unidad_medida" &
             "  Where(IdUnidadMedida = @IdUnidadMedida)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeUnidad_medida.IdUnidadMedida))


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

            Const sp As String = " Delete from Unidad_medida"
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
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeUnidad_medida As clsBeUnidad_medida) As Boolean

        Try

            Const sp As String = "SELECT * FROM Unidad_medida" &
            " Where(IdUnidadMedida = @IdUnidadMedida)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeUnidad_medida.IdUnidadMedida))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeUnidad_medida, dt.Rows(0))
            Else
                Throw New Exception(String.Format("El IdUnidad de medida: {0} no existe. ", oBeUnidad_medida))
            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeUnidad_medida)

        Try

            Dim lReturnList As New List(Of clsBeUnidad_medida)
            Const sp As String = "SELECT * FROM Unidad_medida"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeUnidad_medida As New clsBeUnidad_medida

            For Each dr As DataRow In dt.Rows

                vBeUnidad_medida = New clsBeUnidad_medida
                Cargar(vBeUnidad_medida, dr)
                lReturnList.Add(vBeUnidad_medida)

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

    Public Shared Function GetAllForCombo(Optional ByVal pIdPropietario As Integer = 0) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "Select IdUnidadMedida, Nombre from unidad_medida where Activo = 1 "

            If pIdPropietario > 0 Then
                sp += " and IdPropietario=@pIdPropietario"
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            If pIdPropietario > 0 Then
                dad.SelectCommand.Parameters.Add(New SqlParameter("@PIDPROPIETARIO", pIdPropietario))
            End If
            Dim dt As New DataTable

            dad.Fill(dt)

            lTransaction.Commit()

            cmd.Dispose()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#GT25072024: cargar umbas que se utiliza como unidad de cobro.
    Public Shared Function GetAllForCombo_Es_Um_Cobro(ByVal pIdPropietario As Integer, ByVal pEsUmCobro As Boolean) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "Select IdUnidadMedida, Nombre from unidad_medida where Activo = 1 and es_um_cobro=@pEsUmCobro "

            If pIdPropietario > 0 Then
                sp += " and IdPropietario=@pIdPropietario "
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@pEsUmCobro", pEsUmCobro))

            If pIdPropietario > 0 Then
                dad.SelectCommand.Parameters.Add(New SqlParameter("@pIdPropietario", pIdPropietario))
            End If

            Dim dt As New DataTable

            dad.Fill(dt)

            lTransaction.Commit()

            cmd.Dispose()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeUnidad_medida As clsBeUnidad_medida)

        Try

            Const sp As String = " SELECT * FROM Unidad_medida 
			                       Where(IdUnidadMedida = @IdUnidadMedida)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", pBeUnidad_medida.IDUNIDADMEDIDA))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeUnidad_medida, dt.Rows(0))
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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdUnidadMedida),0) FROM Unidad_medida"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue) + 1
                        End If
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

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

    Public Shared Function Get_All_For_Cobro() As List(Of clsBeUnidad_medida)

        Get_All_For_Cobro = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT *
                                  FROM unidad_medida 
                                  WHERE Activo = 1 AND Es_Um_Cobro = 1"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeUnidad_medida As New clsBeUnidad_medida
            Dim lReturnList As New List(Of clsBeUnidad_medida)

            For Each dr As DataRow In dt.Rows

                vBeUnidad_medida = New clsBeUnidad_medida()
                Cargar(vBeUnidad_medida, dr)
                lReturnList.Add(vBeUnidad_medida)

            Next

            Get_All_For_Cobro = lReturnList

            lTransaction.Commit()

            cmd.Dispose()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_For_Cobro(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As List(Of clsBeUnidad_medida)

        Get_All_For_Cobro = Nothing

        Try

            Const sp As String = "SELECT *
                                  FROM unidad_medida 
                                  WHERE Activo = 1 AND Es_Um_Cobro = 1"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeUnidad_medida As New clsBeUnidad_medida
            Dim lReturnList As New List(Of clsBeUnidad_medida)

            For Each dr As DataRow In dt.Rows

                vBeUnidad_medida = New clsBeUnidad_medida()
                Cargar(vBeUnidad_medida, dr)
                lReturnList.Add(vBeUnidad_medida)

            Next

            Get_All_For_Cobro = lReturnList

            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
