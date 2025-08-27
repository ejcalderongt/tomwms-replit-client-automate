Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnMenu_sistema_op
    Implements IDisposable

    Public Shared Sub Cargar(ByRef oBeMenu_sistema_op As clsBeMenu_sistema_op, ByRef dr As DataRow)
        Try
            With oBeMenu_sistema_op
                .IdMenuSistemaOP = IIf(IsDBNull(dr.Item("IdMenuSistemaOP")), "", dr.Item("IdMenuSistemaOP"))
                .IdTipoTarea = IIf(IsDBNull(dr.Item("IdTipoTarea")), 0, dr.Item("IdTipoTarea"))
                .Nombre = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                .Nivel = IIf(IsDBNull(dr.Item("Nivel")), 0, dr.Item("Nivel"))
                .Padre = IIf(IsDBNull(dr.Item("Padre")), "", dr.Item("Padre"))
                .Posicion = IIf(IsDBNull(dr.Item("Posicion")), 0, dr.Item("Posicion"))
            End With
        Catch ex As Exception
            Throw New Exception("Menu_sistema_op_Cargar: " & ex.Message)
        End Try
    End Sub

    Public Function Insertar(ByRef oBeMenu_sistema_op As clsBeMenu_sistema_op, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("menu_sistema_op")
            Ins.Add("idmenusistemaop", "@idmenusistemaop", DataType.Parametro)
            Ins.Add("idtipotarea", "@idtipotarea", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("nivel", "@nivel", DataType.Parametro)
            Ins.Add("padre", "@padre", DataType.Parametro)
            Ins.Add("posicion", "@posicion", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMENUSISTEMAOP", oBeMenu_sistema_op.IdMenuSistemaOP))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeMenu_sistema_op.IdTipoTarea))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeMenu_sistema_op.Nombre))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeMenu_sistema_op.Nivel))
            cmd.Parameters.Add(New SqlParameter("@PADRE", oBeMenu_sistema_op.Padre))
            cmd.Parameters.Add(New SqlParameter("@POSICION", oBeMenu_sistema_op.Posicion))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeMenu_sistema_op.IdMenuSistemaOP = CStr(cmd.Parameters("@IDMENUSISTEMAOP").Value)

        Catch ex As Exception
            Throw New Exception("Menu_sistema_op_Insertar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Actualizar(ByRef oBeMenu_sistema_op As clsBeMenu_sistema_op, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("menu_sistema_op")
            Upd.Add("idmenusistemaop", "@idmenusistemaop", DataType.Parametro)
            Upd.Add("idtipotarea", "@idtipotarea", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("nivel", "@nivel", DataType.Parametro)
            Upd.Add("padre", "@padre", DataType.Parametro)
            Upd.Add("posicion", "@posicion", DataType.Parametro)
            Upd.Where("IdMenuSistemaOP = @IdMenuSistemaOP")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDMENUSISTEMAOP", oBeMenu_sistema_op.IdMenuSistemaOP))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOTAREA", oBeMenu_sistema_op.IdTipoTarea))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeMenu_sistema_op.Nombre))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeMenu_sistema_op.Nivel))
            cmd.Parameters.Add(New SqlParameter("@PADRE", oBeMenu_sistema_op.Padre))
            cmd.Parameters.Add(New SqlParameter("@POSICION", oBeMenu_sistema_op.Posicion))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Menu_sistema_op_Actualizar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Eliminar(ByRef oBeMenu_sistema_op As clsBeMenu_sistema_op, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Menu_sistema_op" &
             "  Where(IdMenuSistemaOP = @IdMenuSistemaOP)"


            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDMENUSISTEMAOP", oBeMenu_sistema_op.IdMenuSistemaOP))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw New Exception("Menu_sistema_op_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Menu_sistema_op"

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
            Throw New Exception("Menu_sistema_op_Eliminar: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Menu_sistema_op"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw New Exception("Menu_sistema_op_Listar: " & ex.Message)
        End Try

    End Function

    Public Function Obtener(ByRef oBeMenu_sistema_op As clsBeMenu_sistema_op) As Boolean

        Try

            Const sp As String = "SELECT * FROM Menu_sistema_op" &
            " Where(IdMenuSistemaOP = @IdMenuSistemaOP)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)


            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMENUSISTEMAOP", oBeMenu_sistema_op.IdMenuSistemaOP))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeMenu_sistema_op, dt.Rows(0))
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

    Public Shared Function GetAll() As List(Of clsBeMenu_sistema_op)

        Try

            Dim lReturnList As New List(Of clsBeMenu_sistema_op)
            Const sp As String = "SELECT * FROM Menu_sistema_op"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeMenu_sistema_op As New clsBeMenu_sistema_op

            For Each dr As DataRow In dt.Rows

                vBeMenu_sistema_op = New clsBeMenu_sistema_op
                Cargar(vBeMenu_sistema_op, dr)
                lReturnList.Add(vBeMenu_sistema_op)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Menu_sistema_op_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeMenu_sistema_op As clsBeMenu_sistema_op)

        Try

            Const sp As String = "SELECT * FROM Menu_sistema_op" &
            " Where(IdMenuSistemaOP = @IdMenuSistemaOP)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMENUSISTEMAOP", pBeMenu_sistema_op.IdMenuSistemaOP))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeMenu_sistema_op, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
