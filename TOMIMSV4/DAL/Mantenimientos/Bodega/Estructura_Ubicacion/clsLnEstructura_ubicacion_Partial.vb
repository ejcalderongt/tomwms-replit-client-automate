Imports System.Data.SqlClient
Imports System.Threading.Tasks

Partial Public Class clsLnEstructura_ubicacion

    Public Shared Async Sub Insertar_Estructura_Ubicacion(ByVal listBeEstructuraUbicacion As List(Of clsBeEstructura_ubicacion),
                                                          ByVal lblprg As Label)


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lTaskOfInserts As New List(Of Task)

        Try

            Await lConnection.OpenAsync() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            If listBeEstructuraUbicacion.Count > 0 Then

                Dim vMaxIdUbicacion As Integer = MaxID(lConnection, lTransaction) + 1

                For Each BeEstructuraUbicacion As clsBeEstructura_ubicacion In listBeEstructuraUbicacion
                    BeEstructuraUbicacion.IdUbicacion = vMaxIdUbicacion
                    Debug.WriteLine("Ubic: " & BeEstructuraUbicacion.IdUbicacion)
                    lTaskOfInserts.Add(Insertar_Async(BeEstructuraUbicacion, lConnection, lTransaction))
                    lblprg.Text = "Insertando ubicación: " & BeEstructuraUbicacion.IdUbicacion
                    lblprg.Refresh()
                    vMaxIdUbicacion += 1
                    Application.DoEvents()
                Next

            End If

            Await Task.WhenAll(lTaskOfInserts)

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Async Function Insertar_Estructura_Ubicacion(ByVal listBeEstructuraUbicacion As List(Of clsBeEstructura_ubicacion),
                                                          ByVal lblprg As Label,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As Task

        Dim lTaskOfInserts As New List(Of Task)

        Try

            If listBeEstructuraUbicacion.Count > 0 Then

                Dim vMaxIdUbicacion As Integer = MaxID(lConnection, lTransaction) + 1

                For Each BeEstructuraUbicacion As clsBeEstructura_ubicacion In listBeEstructuraUbicacion
                    BeEstructuraUbicacion.IdUbicacion = vMaxIdUbicacion
                    Debug.WriteLine("Ubic: " & BeEstructuraUbicacion.IdUbicacion)
                    lTaskOfInserts.Add(Insertar_Async(BeEstructuraUbicacion, lConnection, lTransaction))
                    lblprg.Text = "Insertando ubicación: " & BeEstructuraUbicacion.IdUbicacion
                    lblprg.Refresh()
                    vMaxIdUbicacion += 1
                    Application.DoEvents()
                Next

            End If

            Await Task.WhenAll(lTaskOfInserts)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Eliminar_Tramo(ByRef IdTramo As Integer,
                                          Optional ByVal pConection As SqlConnection = Nothing,
                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete From Estructura_ubicacion Where(idtramo = @idtramo)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idtramo", IdTramo))


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

End Class

