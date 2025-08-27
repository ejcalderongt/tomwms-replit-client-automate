Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnImpresora
    Implements IDisposable

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(Idimpresora),0) FROM impresora"), lConnection)
                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If
                End Using
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal pActivo As Boolean, ByVal IdBodega As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_By_IdBodega = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT IdImpresora AS Código, empresa.nombre AS Empresa, 
                                impresora.nombre AS Impresora, direccion_Ip AS 'Dirección IP', 
                                impresora.mac_adress
                                FROM Impresora INNER JOIN empresa ON impresora.IdEmpresa = empresa.IdEmpresa 
                                WHERE 1 > 0  AND impresora.IdBodega=@IdBodega "

            If pActivo Then
                sp += " AND impresora.Activo=1"
            Else
                sp += " AND impresora.Activo=0"
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction)
            cmd.CommandType = CommandType.Text
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Get_All_By_IdBodega = dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_Impresora_By_IdEmpresa(ByVal IdEmpresa As Integer) As List(Of clsBeImpresora)

        Try

            Dim lReturnList As New List(Of clsBeImpresora)
            Const sp As String = "SELECT * FROM Impresora Where IdEmpresa= @IdEmpresa"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdEmpresa", IdEmpresa))

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

    Public Shared Function Get_All_Impresora_By_IdEmpresa_And_IdBodega(ByVal IdEmpresa As Integer,
                                                                       ByVal IdBodega As Integer) As List(Of clsBeImpresora)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeImpresora)
            Const sp As String = "SELECT * FROM Impresora Where IdEmpresa= @IdEmpresa AND IdBodega = @IdBodega AND Activo = 1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdEmpresa", IdEmpresa))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            dad.Fill(dt)

            Dim vBeImpresora As New clsBeImpresora

            For Each dr As DataRow In dt.Rows

                vBeImpresora = New clsBeImpresora
                Cargar(vBeImpresora, dr)
                lReturnList.Add(vBeImpresora)

            Next

            lTransaction.Commit()

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_Impresora_By_IdEmpresa_And_IdBodega_DT(ByVal IdEmpresa As Integer,
                                                                       ByVal IdBodega As Integer) As DataTable

        Get_All_Impresora_By_IdEmpresa_And_IdBodega_DT = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeQT_Impresora)
            Const sp As String = "SELECT * FROM Impresora Where IdEmpresa= @IdEmpresa AND IdBodega = @IdBodega AND Activo = 1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable("ListImpresoras")

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdEmpresa", IdEmpresa))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Impresora_BOF(ByVal IdEmpresa As Integer,
                                                 ByVal IdBodega As Integer) As DataTable

        Get_All_Impresora_BOF = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lReturnList As New List(Of clsBeImpresora)
            Const sp As String = "SELECT * FROM Impresora Where IdEmpresa= @IdEmpresa AND IdBodega = @IdBodega AND Activo = 1 AND Es_movil = 0 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable("ListImpresoras")

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdEmpresa", IdEmpresa))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
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
