Imports System.Data.SqlClient

Partial Class clsLnBodega_monitor_parametro
    Implements IDisposable

    Public Shared Function GetAllByIdBodega(ByVal pIdBodega As Integer) As List(Of clsBeBodega_monitor_parametro)

        Try

            Dim lReturnList As New List(Of clsBeBodega_monitor_parametro)


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS20171023_1625pm: Quité String.Format.
                Dim vSQL As String = "SELECT * FROM bodega_monitor_parametro WHERE IdBodega=@IdBodega"


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("IdBodega", pIdBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeBodega_monitor_parametro


                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeBodega_monitor_parametro()

                            Obj.IdMonitor = CType(lRow("IdMonitor"), System.Int32)

                            If lRow("IdBodega") IsNot DBNull.Value AndAlso lRow("IdBodega") IsNot Nothing Then
                                Obj.IdBodega = CType(lRow("IdBodega"), System.Int32)
                            End If

                            If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                Obj.Nombre = CType(lRow("Nombre"), System.String)
                            End If

                            If lRow("TiempoActualizacion") IsNot DBNull.Value AndAlso lRow("TiempoActualizacion") IsNot Nothing Then
                                Obj.TiempoActualizacion = CType(lRow("TiempoActualizacion"), System.Int32)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdBodega As Integer, pIdMonitor As Integer) As clsBeBodega_monitor_parametro


        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS20171023_1625pm: Quité String.Format.
                Dim vSQL As String = "SELECT * FROM bodega_monitor_parametro WHERE IdBodega=@IdBodega And IdMonitor=@IdMonitor"


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdMonitor", pIdMonitor)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    Dim Obj As clsBeBodega_monitor_parametro

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Obj = New clsBeBodega_monitor_parametro()

                        Obj.IdMonitor = CType(lRow("IdMonitor"), System.Int32)

                        If lRow("IdBodega") IsNot DBNull.Value AndAlso lRow("IdBodega") IsNot Nothing Then
                            Obj.IdBodega = CType(lRow("IdBodega"), System.Int32)
                        End If

                        If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                            Obj.Nombre = CType(lRow("Nombre"), System.String)
                        End If
                        If lRow("TiempoActualizacion") IsNot DBNull.Value AndAlso lRow("TiempoActualizacion") IsNot Nothing Then
                            Obj.TiempoActualizacion = CType(lRow("TiempoActualizacion"), System.Int32)
                        End If

                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Exists(ByVal pIdBodega As Integer, ByVal pIdMonitor As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM bodega_monitor_parametro WHERE IdBodega=@IdBodega And IdMonitor=@IdMonitor "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    lCommand.Parameters.AddWithValue("@IdMonitor", pIdMonitor)

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetTiempoActualizacionByIdMonitor(ByVal pIdMonitor As Integer,
                                                             ByVal pIdBodega As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        GetTiempoActualizacionByIdMonitor = 0

        Try

            Dim vTiempoActualizacion As Double = 0

            Dim vSQL As String = "SELECT TiempoActualizacion " &
                    " FROM bodega_monitor_parametro " &
                    " WHERE IdBodega=@IdBodega And IdMonitor=@IdMonitor "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lCommand.Parameters.AddWithValue("@IdMonitor", pIdMonitor)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vTiempoActualizacion = lReturnValue
                End If

            End Using

            Return vTiempoActualizacion

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Tiempos_Actualizacion(ByVal pIdBodega As Integer,
                                                     ByRef TiempoListaDeTarea As Integer,
                                                     ByRef TiempoRellenadoZonaPicking As Integer,
                                                     ByRef TiempoCalendarioMuelle As Integer,
                                                     ByRef TiempoCalendarioPorPropietario As Integer,
                                                     ByRef TiempoMaxMin As Integer) As Boolean

        Get_Tiempos_Actualizacion = 0

        Dim lconection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim ltransaction As SqlTransaction = Nothing

        Try

            lconection.Open() : ltransaction = lconection.BeginTransaction()

            TiempoListaDeTarea = GetTiempoActualizacionByIdMonitor(1, pIdBodega, lconection, ltransaction)
            TiempoCalendarioPorPropietario = GetTiempoActualizacionByIdMonitor(2, pIdBodega, lconection, ltransaction)
            TiempoCalendarioMuelle = GetTiempoActualizacionByIdMonitor(3, pIdBodega, lconection, ltransaction)
            TiempoRellenadoZonaPicking = GetTiempoActualizacionByIdMonitor(4, pIdBodega, lconection, ltransaction)
            TiempoMaxMin = GetTiempoActualizacionByIdMonitor(5, pIdBodega, lconection, ltransaction)

            ltransaction.Commit()

        Catch ex As Exception
            ltransaction.Rollback()
            Throw ex
        Finally
            If Not lconection Is Nothing AndAlso lconection.State = ConnectionState.Open Then lconection.Close() : lconection.Dispose()
            If ltransaction IsNot Nothing Then ltransaction.Dispose()
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
