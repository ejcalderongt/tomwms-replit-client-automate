Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnConfiguracion_barra_pallet

    Public Shared Sub Cargar(ByRef oBeConfiguracion_barra_pallet As clsBeConfiguracion_barra_pallet, ByRef dr As DataRow)
        Try
            With oBeConfiguracion_barra_pallet
                .IdConfiguracionPallet = IIf(IsDBNull(dr.Item("IdConfiguracionPallet")), 0, dr.Item("IdConfiguracionPallet"))
                .LongCodBodegaOrigen = IIf(IsDBNull(dr.Item("LongCodBodegaOrigen")), 0, dr.Item("LongCodBodegaOrigen"))
                .LongCodProducto = IIf(IsDBNull(dr.Item("LongCodProducto")), 0, dr.Item("LongCodProducto"))
                .LongLP = IIf(IsDBNull(dr.Item("LongLP")), 0, dr.Item("LongLP"))
                .CodigoNumerico = IIf(IsDBNull(dr.Item("CodigoNumerico")), False, dr.Item("CodigoNumerico"))
                .IdentificadorInicio = IIf(IsDBNull(dr.Item("IdentificadorInicio")), "", dr.Item("IdentificadorInicio"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeConfiguracion_barra_pallet As clsBeConfiguracion_barra_pallet, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("configuracion_barra_pallet")
            Ins.Add("idconfiguracionpallet", "@idconfiguracionpallet", DataType.Parametro)
            Ins.Add("longcodbodegaorigen", "@longcodbodegaorigen", DataType.Parametro)
            Ins.Add("longcodproducto", "@longcodproducto", DataType.Parametro)
            Ins.Add("longlp", "@longlp", DataType.Parametro)
            Ins.Add("codigonumerico", "@codigonumerico", DataType.Parametro)
            Ins.Add("identificadorinicio", "@identificadorinicio", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONPALLET", oBeConfiguracion_barra_pallet.IdConfiguracionPallet))
            cmd.Parameters.Add(New SqlParameter("@LONGCODBODEGAORIGEN", oBeConfiguracion_barra_pallet.LongCodBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@LONGCODPRODUCTO", oBeConfiguracion_barra_pallet.LongCodProducto))
            cmd.Parameters.Add(New SqlParameter("@LONGLP", oBeConfiguracion_barra_pallet.LongLP))
            cmd.Parameters.Add(New SqlParameter("@CODIGONUMERICO", oBeConfiguracion_barra_pallet.CodigoNumerico))
            cmd.Parameters.Add(New SqlParameter("@IDENTIFICADORINICIO", oBeConfiguracion_barra_pallet.IdentificadorInicio))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeConfiguracion_barra_pallet.IdConfiguracionPallet = CInt(cmd.Parameters("@IDCONFIGURACIONPALLET").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeConfiguracion_barra_pallet As clsBeConfiguracion_barra_pallet, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("configuracion_barra_pallet")
            Upd.Add("idconfiguracionpallet", "@idconfiguracionpallet", DataType.Parametro)
            Upd.Add("longcodbodegaorigen", "@longcodbodegaorigen", DataType.Parametro)
            Upd.Add("longcodproducto", "@longcodproducto", DataType.Parametro)
            Upd.Add("longlp", "@longlp", DataType.Parametro)
            Upd.Add("codigonumerico", "@codigonumerico", DataType.Parametro)
            Upd.Add("identificadorinicio", "@identificadorinicio", DataType.Parametro)
            Upd.Where("IdConfiguracionPallet = @IdConfiguracionPallet")

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

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONPALLET", oBeConfiguracion_barra_pallet.IdConfiguracionPallet))
            cmd.Parameters.Add(New SqlParameter("@LONGCODBODEGAORIGEN", oBeConfiguracion_barra_pallet.LongCodBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@LONGCODPRODUCTO", oBeConfiguracion_barra_pallet.LongCodProducto))
            cmd.Parameters.Add(New SqlParameter("@LONGLP", oBeConfiguracion_barra_pallet.LongLP))
            cmd.Parameters.Add(New SqlParameter("@CODIGONUMERICO", oBeConfiguracion_barra_pallet.CodigoNumerico))
            cmd.Parameters.Add(New SqlParameter("@IDENTIFICADORINICIO", oBeConfiguracion_barra_pallet.IdentificadorInicio))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeConfiguracion_barra_pallet As clsBeConfiguracion_barra_pallet, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Configuracion_barra_pallet" &
             "  Where(IdConfiguracionPallet = @IdConfiguracionPallet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONPALLET", oBeConfiguracion_barra_pallet.IdConfiguracionPallet))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Configuracion_barra_pallet"
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

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Configuracion_barra_pallet"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeConfiguracion_barra_pallet As clsBeConfiguracion_barra_pallet) As Boolean

        Try

            Const sp As String = "SELECT * FROM Configuracion_barra_pallet" &
            " Where(IdConfiguracionPallet = @IdConfiguracionPallet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCONFIGURACIONPALLET", oBeConfiguracion_barra_pallet.IdConfiguracionPallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeConfiguracion_barra_pallet, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeConfiguracion_barra_pallet)

        Try

            Dim lReturnList As New List(Of clsBeConfiguracion_barra_pallet)
            Const sp As String = "SELECT * FROM Configuracion_barra_pallet"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeConfiguracion_barra_pallet As New clsBeConfiguracion_barra_pallet

            For Each dr As DataRow In dt.Rows
                vBeConfiguracion_barra_pallet = New clsBeConfiguracion_barra_pallet
                Cargar(vBeConfiguracion_barra_pallet, dr)
                lReturnList.Add(vBeConfiguracion_barra_pallet)
            Next

            Return lReturnList

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeConfiguracion_barra_pallet As clsBeConfiguracion_barra_pallet)

        Try

            Const sp As String = "SELECT * FROM Configuracion_barra_pallet" &
            " Where(IdConfiguracionPallet = @IdConfiguracionPallet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCONFIGURACIONPALLET", pBeConfiguracion_barra_pallet.IDCONFIGURACIONPALLET))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeConfiguracion_barra_pallet, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdConfiguracionPallet),0) FROM Configuracion_barra_pallet"

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

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
