Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnLicencia_item

    Public Shared Sub Cargar(ByRef oBeLicencia_item As clsBeLicencia_item, ByRef dr As DataRow)
        Try
            With oBeLicencia_item
                .IdDisp = IIf(IsDBNull(dr.Item("idDisp")), "", dr.Item("idDisp"))
                .Identificacion = IIf(IsDBNull(dr.Item("identificacion")), "", dr.Item("identificacion"))
                .Tipo = IIf(IsDBNull(dr.Item("tipo")), 0, dr.Item("tipo"))
                .Bandera = IIf(IsDBNull(dr.Item("bandera")), 0, dr.Item("bandera"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .Fecha_Sistema = IIf(IsDBNull(dr.Item("Fecha_Sistema")), Now, dr.Item("Fecha_Sistema"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub Cargar(ByRef pLicencia As clsBeLicencia_item, llave As String)

        Dim cLic As New clsBeLicencia_item
        Dim sp() As String
        Dim ss As String
        Dim vence As Date
        Dim licbo, lichh, yy, mm, dd As Integer

        pLicencia = Nothing

        Try
            ss = clsPublic.DecodeString(llave)

            If ss.Length < 10 Then
                Throw New Exception("La licencia decodificada no es válida")
            End If

            sp = ss.Split(",")

            licbo = sp(0)
            lichh = sp(1)
            yy = sp(2) + 2000
            mm = sp(3)
            dd = sp(4)

            vence = New DateTime(yy, mm, dd)

            cLic.CantBackOffice = licbo
            cLic.CantHandHeld = lichh
            cLic.Vence = vence

            pLicencia = cLic

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeLicencia_item As clsBeLicencia_item, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("licencia_item")
            Ins.Add("iddisp", "@iddisp", DataType.Parametro)
            Ins.Add("identificacion", "@identificacion", DataType.Parametro)
            Ins.Add("tipo", "@tipo", DataType.Parametro)
            Ins.Add("bandera", "@bandera", DataType.Parametro)
            Ins.Add("estado", "@estado", DataType.Parametro)
            Ins.Add("fecha_sistema", "@fecha_sistema", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDDISP", oBeLicencia_item.IdDisp))
            cmd.Parameters.Add(New SqlParameter("@IDENTIFICACION", oBeLicencia_item.Identificacion))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeLicencia_item.Tipo))
            cmd.Parameters.Add(New SqlParameter("@BANDERA", oBeLicencia_item.Bandera))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeLicencia_item.Estado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SISTEMA", oBeLicencia_item.Fecha_Sistema))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeLicencia_item.IdDisp = CStr(cmd.Parameters("@IDDISP").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeLicencia_item As clsBeLicencia_item, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("licencia_item")
            Upd.Add("iddisp", "@iddisp", DataType.Parametro)
            Upd.Add("identificacion", "@identificacion", DataType.Parametro)
            Upd.Add("tipo", "@tipo", DataType.Parametro)
            Upd.Add("bandera", "@bandera", DataType.Parametro)
            Upd.Add("estado", "@estado", DataType.Parametro)
            Upd.Add("fecha_sistema", "@fecha_sistema", DataType.Parametro)
            Upd.Where("idDisp = @idDisp")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDISP", oBeLicencia_item.IdDisp))
            cmd.Parameters.Add(New SqlParameter("@IDENTIFICACION", oBeLicencia_item.Identificacion))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeLicencia_item.Tipo))
            cmd.Parameters.Add(New SqlParameter("@BANDERA", oBeLicencia_item.Bandera))
            cmd.Parameters.Add(New SqlParameter("@ESTADO", oBeLicencia_item.Estado))
            cmd.Parameters.Add(New SqlParameter("@FECHA_SISTEMA", oBeLicencia_item.Fecha_Sistema))

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

    Public Shared Function Eliminar(ByRef oBeLicencia_item As clsBeLicencia_item, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Licencia_item" &
                 "  Where(idDisp = @idDisp)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDDISP", oBeLicencia_item.IdDisp))

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

            Const sp As String = " Delete from Licencia_item"
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

            Const sp As String = "SELECT * FROM Licencia_item"
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

    Public Shared Function Obtener(ByRef oBeLicencia_item As clsBeLicencia_item) As Boolean

        Try

            Const sp As String = "SELECT * FROM Licencia_item" &
                " Where(idDisp = @idDisp)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDISP", oBeLicencia_item.IdDisp))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeLicencia_item, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeLicencia_item)

        Try

            Dim lReturnList As New List(Of clsBeLicencia_item)
            Const sp As String = "SELECT * FROM Licencia_item"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeLicencia_item As New clsBeLicencia_item

            For Each dr As DataRow In dt.Rows

                vBeLicencia_item = New clsBeLicencia_item
                Cargar(vBeLicencia_item, dr)
                lReturnList.Add(vBeLicencia_item)

            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeLicencia_item As clsBeLicencia_item) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Licencia_item" &
                " Where(idDisp = @idDisp Or Identificacion =@idDisp)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDISP", pBeLicencia_item.IdDisp))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeLicencia_item, dt.Rows(0))
                GetSingle = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeLicencia_item As clsBeLicencia_item,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Licencia_item" &
                " Where(idDisp = @idDisp Or Identificacion =@idDisp)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDDISP", pBeLicencia_item.IdDisp))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeLicencia_item, dt.Rows(0))
                GetSingle = True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idDisp),0) FROM Licencia_item"

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
            Throw ex
        End Try

    End Function

End Class

