Public Class SapConnectionPool
    Private ReadOnly pool As List(Of SapConnectionWrapper)
    Private ReadOnly maxSize As Integer
    Private ReadOnly lockObj As New Object()

    Public Sub New(Optional size As Integer = 10)
        maxSize = size
        pool = New List(Of SapConnectionWrapper)()
    End Sub

    Public Function GetConnection(empresa As pEmpresa) As SapConnectionWrapper
        SyncLock lockObj
            ' Reutilizar conexión disponible
            For Each conn In pool
                If Not conn.InUse AndAlso conn.Empresa.Equals(empresa) Then
                    If conn.Company.Connected Then
                        conn.InUse = True
                        conn.LastUsed = Date.Now
                        Return conn
                    Else
                        Try
                            conn.Connect()
                            conn.InUse = True
                            conn.LastUsed = Date.Now
                            Return conn
                        Catch ex As Exception
                            pool.Remove(conn)
                        End Try
                    End If
                End If
            Next

            ' Crear nueva conexión si hay espacio
            If pool.Count < maxSize Then
                Dim newConn As New SapConnectionWrapper(empresa)
                newConn.Connect()
                newConn.InUse = True
                newConn.LastUsed = Date.Now
                pool.Add(newConn)
                Return newConn
            End If

            Throw New Exception("No hay conexiones disponibles en el pool para la empresa " & empresa.ToString())
        End SyncLock
    End Function

    Public Sub ReleaseConnection(conn As SapConnectionWrapper)
        SyncLock lockObj
            If conn IsNot Nothing Then
                If conn.Company.Connected Then conn.Disconnect()
                conn.InUse = False
                conn.LastUsed = Date.Now
            End If
        End SyncLock
    End Sub

    Public Sub CloseAll()
        SyncLock lockObj
            For Each conn In pool
                Try
                    conn.Disconnect()
                Catch ex As Exception
                End Try
            Next
            pool.Clear()
        End SyncLock
    End Sub
End Class