Imports System.Data.SqlClient
Imports System.Threading.Tasks

Partial Public Class clsLnArancel
    Implements IDisposable

    Private Shared lArancelInMemory As New List(Of clsBeArancel)

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeArancel)

        Dim lReturnList As New List(Of clsBeArancel)

        Try


            Dim vSQL As String = "SELECT * FROM Arancel WHERE 1 > 0 "

            If pActivo Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeArancel As clsBeArancel

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Parallel.ForEach(lDataTable.AsEnumerable, Sub(ByVal lrow)
                                                                          SyncLock lReturnList
                                                                              BeArancel = New clsBeArancel
                                                                              Cargar(BeArancel, lrow)
                                                                              lReturnList.Add(BeArancel)
                                                                          End SyncLock
                                                                      End Sub)
                        End If

                    End Using


                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeArancel)

        Dim lReturnList As New List(Of clsBeArancel)

        Try

            Dim vSQL As String = "SELECT * FROM Arancel WHERE activo=1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeArancel

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Parallel.ForEach(lDataTable.AsEnumerable, Sub(ByVal lrow)

                                                                          SyncLock lDataTable

                                                                              Obj = New clsBeArancel
                                                                              Cargar(Obj, lrow)
                                                                              lReturnList.Add(Obj)

                                                                          End SyncLock

                                                                      End Sub)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllForCombo() As DataTable

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT IdArancel,nombre As Nombre FROM Arancel WHERE activo=1 "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Return lDataTable

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdArancel As Integer, Optional pConnection As SqlConnection = Nothing, Optional pTransaction As SqlTransaction = Nothing) As clsBeArancel

        GetSingle = Nothing
        Dim EsTransaccionExterna As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            Dim ObjUM As New clsBeArancel()
            Dim IdxArancel As Integer = -1
            Dim vIdArancel As Integer = pIdArancel
            IdxArancel = lArancelInMemory.FindIndex(Function(x) x.IdArancel = vIdArancel)

            If Not EsTransaccionExterna Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Else
                lConnection = pConnection : lTransaction = pTransaction
            End If


            If IdxArancel = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM Arancel WHERE IdArancel=@IdArancel"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Transaction = lTransaction
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdArancel", pIdArancel)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                        Dim lRow As DataRow = lDT.Rows(0)
                        ObjUM = New clsBeArancel()
                        Cargar(ObjUM, lRow)
                        lArancelInMemory.Add(ObjUM.Clone())
                        Return ObjUM
                    End If

                End Using

                If Not EsTransaccionExterna Then lTransaction.Commit()

                'lConnection.Close()

            Else
                ObjUM = New clsBeArancel()
                ObjUM = lArancelInMemory(IdxArancel).Clone()
                Return ObjUM
            End If

        Catch ex As Exception
            If Not EsTransaccionExterna Then If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not EsTransaccionExterna Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
            End If
        End Try

    End Function
    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdArancel),0) FROM Arancel"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

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
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeArancel As clsBeArancel,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Dim IdxArancel As Integer = -1
            Dim vIdArancel As Integer = oBeArancel.IdArancel
            IdxArancel = lArancelInMemory.FindIndex(Function(x) x.IdArancel = vIdArancel)

            If IdxArancel = -1 Then
                '#GT07082023: se agrega la tran
                oBeArancel = GetSingle(vIdArancel, lConnection, lTransaction)
            Else
                oBeArancel = New clsBeArancel()
                oBeArancel = lArancelInMemory(IdxArancel).Clone()
            End If

        Catch ex As Exception
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
