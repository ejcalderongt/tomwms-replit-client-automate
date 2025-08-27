Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnCliente_direccion

    Public Shared Sub Cargar(ByRef oBeCliente_direccion As clsBeCliente_direccion, ByRef dr As DataRow)
        Try
            With oBeCliente_direccion
                .IdDireccion = IIf(IsDBNull(dr.Item("IdDireccion")), 0, dr.Item("IdDireccion"))
                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .IdRegion = IIf(IsDBNull(dr.Item("IdRegion")), 0, dr.Item("IdRegion"))
                .IdMunicipio = IIf(IsDBNull(dr.Item("IdMunicipio")), 0, dr.Item("IdMunicipio"))
                .Avenida = IIf(IsDBNull(dr.Item("Avenida")), "", dr.Item("Avenida"))
                .Calle = IIf(IsDBNull(dr.Item("Calle")), "", dr.Item("Calle"))
                .No_Casa = IIf(IsDBNull(dr.Item("No_Casa")), "", dr.Item("No_Casa"))
                .Zona = IIf(IsDBNull(dr.Item("Zona")), "", dr.Item("Zona"))
                .Direccion = IIf(IsDBNull(dr.Item("Direccion")), "", dr.Item("Direccion"))
                .Referencia = IIf(IsDBNull(dr.Item("Referencia")), "", dr.Item("Referencia"))
                .Coordenada_x = IIf(IsDBNull(dr.Item("coordenada_x")), "", dr.Item("coordenada_x"))
                .Coordenada_y = IIf(IsDBNull(dr.Item("coordenada_y")), "", dr.Item("coordenada_y"))
                .Local = IIf(IsDBNull(dr.Item("Local")), False, dr.Item("Local"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
            End With

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeCliente_direccion As clsBeCliente_direccion, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("cliente_direccion")
            Ins.Add("iddireccion", "@iddireccion", DataType.Parametro)
            Ins.Add("idcliente", "@idcliente", DataType.Parametro)
            Ins.Add("idregion", "@idregion", DataType.Parametro)
            Ins.Add("idmunicipio", "@idmunicipio", DataType.Parametro)
            Ins.Add("avenida", "@avenida", DataType.Parametro)
            Ins.Add("calle", "@calle", DataType.Parametro)
            Ins.Add("no_casa", "@no_casa", DataType.Parametro)
            Ins.Add("zona", "@zona", DataType.Parametro)
            Ins.Add("direccion", "@direccion", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("coordenada_x", "@coordenada_x", DataType.Parametro)
            Ins.Add("coordenada_y", "@coordenada_y", DataType.Parametro)
            Ins.Add("local", "@local", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDDIRECCION", oBeCliente_direccion.IdDireccion))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente_direccion.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@IDREGION", oBeCliente_direccion.IdRegion))
            cmd.Parameters.Add(New SqlParameter("@IDMUNICIPIO", oBeCliente_direccion.IdMunicipio))
            cmd.Parameters.Add(New SqlParameter("@AVENIDA", oBeCliente_direccion.Avenida))
            cmd.Parameters.Add(New SqlParameter("@CALLE", oBeCliente_direccion.Calle))
            cmd.Parameters.Add(New SqlParameter("@NO_CASA", oBeCliente_direccion.No_Casa))
            cmd.Parameters.Add(New SqlParameter("@ZONA", oBeCliente_direccion.Zona))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeCliente_direccion.Direccion))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeCliente_direccion.Referencia))
            cmd.Parameters.Add(New SqlParameter("@COORDENADA_X", oBeCliente_direccion.Coordenada_x))
            cmd.Parameters.Add(New SqlParameter("@COORDENADA_Y", oBeCliente_direccion.Coordenada_y))
            cmd.Parameters.Add(New SqlParameter("@LOCAL", oBeCliente_direccion.Local))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCliente_direccion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCliente_direccion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCliente_direccion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCliente_direccion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCliente_direccion.Activo))

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

    Public Shared Function Actualizar(ByRef oBeCliente_direccion As clsBeCliente_direccion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("cliente_direccion")
            Upd.Add("iddireccion", "@iddireccion", DataType.Parametro)
            Upd.Add("idcliente", "@idcliente", DataType.Parametro)
            Upd.Add("idregion", "@idregion", DataType.Parametro)
            Upd.Add("idmunicipio", "@idmunicipio", DataType.Parametro)
            Upd.Add("avenida", "@avenida", DataType.Parametro)
            Upd.Add("calle", "@calle", DataType.Parametro)
            Upd.Add("no_casa", "@no_casa", DataType.Parametro)
            Upd.Add("zona", "@zona", DataType.Parametro)
            Upd.Add("direccion", "@direccion", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("coordenada_x", "@coordenada_x", DataType.Parametro)
            Upd.Add("coordenada_y", "@coordenada_y", DataType.Parametro)
            Upd.Add("local", "@local", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdDireccion = @IdDireccion" &
                " AND IdCliente = @IdCliente")

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

            cmd.Parameters.Add(New SqlParameter("@IDDIRECCION", oBeCliente_direccion.IdDireccion))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente_direccion.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@IDREGION", oBeCliente_direccion.IdRegion))
            cmd.Parameters.Add(New SqlParameter("@IDMUNICIPIO", oBeCliente_direccion.IdMunicipio))
            cmd.Parameters.Add(New SqlParameter("@AVENIDA", oBeCliente_direccion.Avenida))
            cmd.Parameters.Add(New SqlParameter("@CALLE", oBeCliente_direccion.Calle))
            cmd.Parameters.Add(New SqlParameter("@NO_CASA", oBeCliente_direccion.No_Casa))
            cmd.Parameters.Add(New SqlParameter("@ZONA", oBeCliente_direccion.Zona))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeCliente_direccion.Direccion))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeCliente_direccion.Referencia))
            cmd.Parameters.Add(New SqlParameter("@COORDENADA_X", oBeCliente_direccion.Coordenada_x))
            cmd.Parameters.Add(New SqlParameter("@COORDENADA_Y", oBeCliente_direccion.Coordenada_y))
            cmd.Parameters.Add(New SqlParameter("@LOCAL", oBeCliente_direccion.Local))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCliente_direccion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCliente_direccion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCliente_direccion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCliente_direccion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCliente_direccion.Activo))

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


    Public Shared Function Eliminar(ByRef oBeCliente_direccion As clsBeCliente_direccion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Cliente_direccion" &
             "  Where(IdDireccion = @IdDireccion)" &
             "  AND (IdCliente = @IdCliente)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDIRECCION", oBeCliente_direccion.IdDireccion))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente_direccion.IdCliente))

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

            Const sp As String = " Delete from Cliente_direccion"
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

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Cliente_direccion"
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

    Public Shared Function Obtener(ByRef oBeCliente_direccion As clsBeCliente_direccion) As Boolean

        Try

            Const sp As String = "SELECT * FROM Cliente_direccion" & _
            " Where(IdDireccion = @IdDireccion)" & _
            " AND (IdCliente = @IdCliente)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDIRECCION", oBeCliente_direccion.IDDIRECCION))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente_direccion.IDCLIENTE))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeCliente_direccion, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeCliente_direccion)

        Try

            Dim lReturnList As New List(Of clsBeCliente_direccion)
            Const sp As String = "SELECT * FROM Cliente_direccion"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeCliente_direccion As New clsBeCliente_direccion

            For Each dr As DataRow In dt.Rows

                vBeCliente_direccion = New clsBeCliente_direccion
                Cargar(vBeCliente_direccion, dr)
                lReturnList.Add(vBeCliente_direccion)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeCliente_direccion As clsBeCliente_direccion)

        Try

            Const sp As String = "SELECT * FROM Cliente_direccion" & _
            " Where(IdDireccion = @IdDireccion)" & _
            " AND (IdCliente = @IdCliente)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDIRECCION", pBeCliente_direccion.IDDIRECCION))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCLIENTE", pBeCliente_direccion.IDCLIENTE))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeCliente_direccion, dt.Rows(0))
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

            Const sp As String = "SELECT ISNULL(Max(IdDireccion),0) FROM Cliente_direccion"

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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="IdCliente"></param>
    ''' <param name="IdDireccion"></param>
    ''' <returns></returns>
    Public Shared Function GetSingle(ByVal IdCliente As Integer,
                                     ByVal IdDireccion As Integer) As clsBeCliente_direccion

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Cliente_direccion" & _
            " Where(IdDireccion = @IdDireccion)" & _
            " AND (IdCliente = @IdCliente)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDIRECCION", IDDIRECCION))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCLIENTE", IDCLIENTE))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim pBeCliente_direccion As New clsBeCliente_direccion

            If dt.Rows.Count = 1 Then
                Cargar(pBeCliente_direccion, dt.Rows(0))
                Return pBeCliente_direccion
            End If

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
