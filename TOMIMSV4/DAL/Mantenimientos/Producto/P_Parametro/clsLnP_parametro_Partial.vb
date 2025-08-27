Imports System.Data.SqlClient

Partial Public Class clsLnP_parametro
    Implements IDisposable

    Public Shared Function MAXIdParametro() As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim sp As String = "SELECT  MAX(IdParametro) + 1 as nuevo FROM p_parametro"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                MAXIdParametro = IIf(IsDBNull(dt.Rows(0).Item("nuevo")), "1", dt.Rows(0).Item("nuevo"))
            Else
                MAXIdParametro = 1
            End If

            lTransaction.Commit()

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

    Public Shared Function GetAll(ByVal pActivo As Boolean) As List(Of clsBeP_parametro)

        Try

            Dim lReturnList As New List(Of clsBeP_parametro)
            Const sp As String = "SELECT * FROM P_parametro WHERE Activo = @Activo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", pActivo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeP_parametro As New clsBeP_parametro

            For Each dr As DataRow In dt.Rows

                vBeP_parametro = New clsBeP_parametro
                Cargar(vBeP_parametro, dr)
                lReturnList.Add(vBeP_parametro)

            Next

            lConnection.Dispose()

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("P_parametro_GetAll: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetAllForCombo(ByVal pActivo As Boolean) As DataTable

        Try

            Const sp As String = "SELECT IdParametro,descripcion FROM P_parametro WHERE Activo = @Activo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activo", pActivo)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Throw New Exception("P_parametro_GetAll: " & ex.Message)
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

