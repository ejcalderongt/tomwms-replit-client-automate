Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnImpresora

    Public Shared Sub Cargar(ByRef oBeImpresora As clsBeImpresora, ByRef dr As DataRow)

        Try

            With oBeImpresora

                .IdImpresora = IIf(IsDBNull(dr.Item("IdImpresora")), 0, dr.Item("IdImpresora"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Direccion_Ip = IIf(IsDBNull(dr.Item("direccion_Ip")), "", dr.Item("direccion_Ip"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .mac_adress = IIf(IsDBNull(dr.Item("mac_adress")), "", dr.Item("mac_adress"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Numero_Serie = IIf(IsDBNull(dr.Item("Numero_Serie")), 0, dr.Item("Numero_Serie"))
                .IdImpresoraMarca = IIf(IsDBNull(dr.Item("IdImpresoraMarca")), 0, dr.Item("IdImpresoraMarca"))
                .IdLenguaje = IIf(IsDBNull(dr.Item("IdLenguaje")), 0, dr.Item("IdLenguaje"))
                .IdTipoConexion = IIf(IsDBNull(dr.Item("IdTipoConexion")), 0, dr.Item("IdTipoConexion"))
                .Puerto = IIf(IsDBNull(dr.Item("puerto")), 0, dr.Item("puerto"))
                .Velocidad = IIf(IsDBNull(dr.Item("velocidad")), 0, dr.Item("velocidad"))
                .Es_Movil = IIf(IsDBNull(dr.Item("es_movil")), False, dr.Item("es_movil"))

            End With

        Catch ex As Exception
            Throw New Exception("Impresora_Cargar: " & ex.Message)
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeImpresora As clsBeImpresora, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("impresora")
            Ins.Add("idimpresora", "@idimpresora", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("direccion_ip", "@direccion_ip", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("mac_adress", "@mac_adress", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("Numero_Serie", "@Numero_Serie", DataType.Parametro)
            Ins.Add("IdImpresoraMarca", "@IdImpresoraMarca", DataType.Parametro)
            Ins.Add("IdLenguaje", "@IdLenguaje", DataType.Parametro)
            Ins.Add("IdTipoConexion", "@IdTipoConexion", DataType.Parametro)
            Ins.Add("puerto", "@Puerto", DataType.Parametro)
            Ins.Add("es_movil", "@EsMovil", DataType.Parametro)
            Ins.Add("velocidad", "@Velocidad", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORA", oBeImpresora.IdImpresora))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeImpresora.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeImpresora.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION_IP", oBeImpresora.Direccion_Ip))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresora.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeImpresora.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeImpresora.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeImpresora.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeImpresora.Activo))
            cmd.Parameters.Add(New SqlParameter("@MAC_ADRESS", oBeImpresora.mac_adress))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeImpresora.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_SERIE", oBeImpresora.Numero_Serie))
            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORAMARCA", oBeImpresora.IdImpresoraMarca))
            cmd.Parameters.Add(New SqlParameter("@IDLENGUAJE", oBeImpresora.IdLenguaje))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOCONEXION", oBeImpresora.IdTipoConexion))
            cmd.Parameters.Add(New SqlParameter("@PUERTO", oBeImpresora.Puerto))
            cmd.Parameters.Add(New SqlParameter("@ESMOVIL", oBeImpresora.Es_Movil))
            cmd.Parameters.Add(New SqlParameter("@VELOCIDAD", oBeImpresora.Velocidad))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeImpresora.IdImpresora = CInt(cmd.Parameters("@IDIMPRESORA").Value)

        Catch ex As Exception
            Throw New Exception("Impresora_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeImpresora As clsBeImpresora, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("impresora")
            Upd.Add("idimpresora", "@idimpresora", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("direccion_ip", "@direccion_ip", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("mac_adress", "@mac_adress", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("Numero_Serie", "@Numero_Serie", DataType.Parametro)
            Upd.Add("IdImpresoraMarca", "@IdImpresoraMarca", DataType.Parametro)
            Upd.Add("IdLenguaje", "@IdLenguaje", DataType.Parametro)
            Upd.Add("IdTipoConexion", "@IdTipoConexion", DataType.Parametro)
            Upd.Add("puerto", "@Puerto", DataType.Parametro)
            Upd.Add("es_movil", "@EsMovil", DataType.Parametro)
            Upd.Add("velocidad", "@Velocidad", DataType.Parametro)
            Upd.Where("IdImpresora = @IdImpresora")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORA", oBeImpresora.IdImpresora))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeImpresora.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeImpresora.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION_IP", oBeImpresora.Direccion_Ip))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeImpresora.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeImpresora.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeImpresora.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeImpresora.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeImpresora.Activo))
            cmd.Parameters.Add(New SqlParameter("@MAC_ADRESS", oBeImpresora.mac_adress))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeImpresora.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@NUMERO_SERIE", oBeImpresora.Numero_Serie))
            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORAMARCA", oBeImpresora.IdImpresoraMarca))
            cmd.Parameters.Add(New SqlParameter("@IDLENGUAJE", oBeImpresora.IdLenguaje))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOCONEXION", oBeImpresora.IdTipoConexion))
            cmd.Parameters.Add(New SqlParameter("@PUERTO", oBeImpresora.Puerto))
            cmd.Parameters.Add(New SqlParameter("@ESMOVIL", oBeImpresora.Es_Movil))
            cmd.Parameters.Add(New SqlParameter("@VELOCIDAD", oBeImpresora.Velocidad))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Impresora_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBeImpresora As clsBeImpresora, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            Const sp As String = " DELETE FROM Impresora
                                   WHERE(IdImpresora = @IdImpresora)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDIMPRESORA", oBeImpresora.IdImpresora))
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Impresora_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try
    End Function

    Public Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Impresora"

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Impresora_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Obtener(ByVal pIdImpresora As Integer) As clsBeImpresora

        Obtener = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Impresora 
                                  Where(IdImpresora = @IdImpresora) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDIMPRESORA", pIdImpresora))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim oBeImpresora As New clsBeImpresora
                Cargar(oBeImpresora, dt.Rows(0))
                Obtener = oBeImpresora
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeImpresora)

        Try

            Dim lReturnList As New List(Of clsBeImpresora)
            Const sp As String = "SELECT * FROM Impresora"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeImpresora As New clsBeImpresora

            For Each dr As DataRow In dt.Rows

                vBeImpresora = New clsBeImpresora
                Cargar(vBeImpresora, dr)
                lReturnList.Add(vBeImpresora)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Impresora_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_Single(ByRef pBeImpresora As clsBeImpresora)

        Try

            Const sp As String = "SELECT * FROM Impresora" &
            " Where(IdImpresora = @IdImpresora)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDIMPRESORA", pBeImpresora.IdImpresora))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeImpresora, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdEmpresora(ByVal IdImpresora As Integer) As clsBeImpresora

        Get_Single_By_IdEmpresora = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Impresora" &
            " Where(IdImpresora = @IdImpresora)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDIMPRESORA", IdImpresora))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeImpresora As New clsBeImpresora
                Cargar(pBeImpresora, dt.Rows(0))
                Get_Single_By_IdEmpresora = pBeImpresora
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_PermiteImprimir(ByVal IdImpresora As Integer) As Boolean

        Get_PermiteImprimir = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT permite_imprimir FROM Impresora" &
            " Where(IdImpresora = @IdImpresora)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDIMPRESORA", IdImpresora))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_PermiteImprimir = IIf(IsDBNull(dt.Rows(0).Item("permite_imprimir")), False, dt.Rows(0).Item("permite_imprimir"))
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Nombre_By_IdImpresora(ByRef IdImpresora As Integer) As String

        Get_Nombre_By_IdImpresora = ""

        Try

            Const sp As String = "SELECT nombre FROM Impresora" &
            " Where(IdImpresora = @IdImpresora)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDIMPRESORA", IdImpresora))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_Nombre_By_IdImpresora = IIf(IsDBNull(dt.Rows(0).Item("nombre")), "", dt.Rows(0).Item("nombre"))
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Velocidad(ByVal Velocidad As Double,
                                                ByVal IdImpresora As Integer,
                                                Optional ByVal pConection As SqlConnection = Nothing,
                                                Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("impresora")
            Upd.Add("velocidad", "@velocidad", DataType.Parametro)
            Upd.Where("IdImpresora = @IdImpresora")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdImpresora", IdImpresora))
            cmd.Parameters.Add(New SqlParameter("@VELOCIDAD", Velocidad))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Impresora_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function
End Class
