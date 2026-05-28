Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsTransaccion
    Implements IDisposable

    Public Property lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    Public Property lTransaction As SqlTransaction

    '#EJC20260528: WmsTrace v2 — tracking del span de TX para correlación OTel-inspired
    Private _traceSpanId As String = ""
    Private _txStartMs As Long = 0

    Sub New()
        lConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        lTransaction = Nothing
    End Sub

    Public Async Function Begin_Transaction_Async() As Threading.Tasks.Task(Of Boolean)

        Try

            Await lConnection.OpenAsync() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            _traceSpanId = WmsTrace.TxBegin("Async") : _txStartMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() '#EJC20260528

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Function Begin_Transaction() As Boolean

        Try

            lConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            _traceSpanId = WmsTrace.TxBegin() : _txStartMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() '#EJC20260528

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Function Open_Connection() As Boolean

        Try

            lConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            lConnection.Open()

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Sub Close_Conection()

        Try

            If lConnection.State = ConnectionState.Open Then
                lConnection.Close()
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Sub Commit_Transaction()

        Try

            If Not lTransaction Is Nothing Then
                lTransaction.Commit()
                WmsTrace.TxCommit(_traceSpanId, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _txStartMs) '#EJC20260528
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Sub RollBack_Transaction()

        Try

            If lTransaction IsNot Nothing Then
                lTransaction.Rollback()
                WmsTrace.TxRollback(_traceSpanId, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _txStartMs) '#EJC20260528
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        If lConnection IsNot Nothing Then
            lConnection.Dispose()
        End If
        If lTransaction IsNot Nothing Then
            lTransaction.Dispose()
            lTransaction = Nothing
        End If
    End Sub

End Class