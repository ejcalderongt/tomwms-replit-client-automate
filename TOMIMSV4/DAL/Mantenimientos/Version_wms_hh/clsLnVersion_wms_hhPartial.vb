Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnVersion_wms_hh

    Implements IDisposable

    Public Shared Function Android_Get_All() As List(Of clsBeVersion_wms_hh_and)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeVersion_wms_hh_and)

            Const sp As String = "Select TOP(1) IdEmpresaVersion, IdEmpresa, version, notas, fecha From version_wms_hh ORDER BY FECHA DESC "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            For Each dr As DataRow In dt.Rows

                Dim vBeVersion As New clsBeVersion_wms_hh_and
                Android_Cargar(vBeVersion, dr)
                lReturnList.Add(vBeVersion)

            Next

            lTransaction.Commit()

            cmd.Dispose()

            Return lReturnList

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


    Public Shared Sub Android_Cargar(ByRef oBeVersion_wms_hh As clsBeVersion_wms_hh_and, ByRef dr As DataRow)
        Try
            With oBeVersion_wms_hh
                .IdEmpresaVersion = IIf(IsDBNull(dr.Item("IdEmpresaVersion")), 0, dr.Item("IdEmpresaVersion"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Version = IIf(IsDBNull(dr.Item("version")), "", dr.Item("version"))
                .Notas = IIf(IsDBNull(dr.Item("notas")), "", dr.Item("notas"))
                .Fecha = 0
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub



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
