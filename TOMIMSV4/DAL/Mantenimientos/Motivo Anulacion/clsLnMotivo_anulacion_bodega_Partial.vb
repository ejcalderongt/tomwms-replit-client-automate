Imports System.Data.SqlClient

Partial Public Class clsLnMotivo_anulacion_bodega
    Implements IDisposable

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdMotivoAnulacionBodega),0) FROM motivo_anulacion_bodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                'HS 07112017 Quité query dentro de SqlCommand.
                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()

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

    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdMotivoAnulacionBodega),0) FROM motivo_anulacion_bodega"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lConnection.Open()

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                lConnection.Close()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByMotivoAnulacion(ByVal pIdMotivoAnulacion As Integer) As List(Of clsBeMotivo_anulacion_bodega)

        Dim lReturnList As New List(Of clsBeMotivo_anulacion_bodega)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM motivo_anulacion_bodega WHERE IdMotivoAnulacion=@IdMotivoAnulacion"


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdMotivoAnulacion", pIdMotivoAnulacion)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeMotivo_anulacion_bodega

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeMotivo_anulacion_bodega
                            Cargar(Obj, lRow)
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

    Public Shared Function Get_All_By_Bodega(ByVal IdBodega As Integer) As List(Of clsBeMotivo_anulacion_bodega)

        Get_All_By_Bodega = Nothing

        Dim lMa As New List(Of clsBeMotivo_anulacion_bodega)
        Dim Ma As New clsBeMotivo_anulacion_bodega

        Try

            Const sp As String = "SELECT * FROM Motivo_anulacion_bodega WHERE IdBodega = @IdBodega "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.CommandType = CommandType.Text
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            For Each vma As DataRow In dt.Rows
                Ma = New clsBeMotivo_anulacion_bodega
                Cargar(Ma, vma)
                lMa.Add(Ma)
            Next

            Return lMa

        Catch ex As Exception
            Throw New Exception("ListarMotivoAnulacionBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Sub GetIdMotivoAnulacionBodega(ByRef BeMotivoAnulacionBodega As clsBeMotivo_anulacion_bodega)

        Try

            Const sp As String = "SELECT * FROM Motivo_anulacion_bodega " &
                                    " WHERE IdBodega = @IdBodega " &
                                    " AND IdMotivoAnulacion = @IdMotivoAnulacion"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Using lCommand As New SqlCommand(sp, lConnection)

                lCommand.CommandType = CommandType.Text

                lCommand.Parameters.AddWithValue("@IdBodega", BeMotivoAnulacionBodega.IdBodega)
                lCommand.Parameters.AddWithValue("@IdMotivoAnulacion", BeMotivoAnulacionBodega.IdMotivoAnulacion)
                lConnection.Open()

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                lConnection.Close()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    BeMotivoAnulacionBodega.IdMotivoAnulacionBodega = CInt(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Throw New Exception("GetIdMotivoAnulacionBodega: " & ex.Message)
        End Try

    End Sub

    Public Shared Function GetIdMotivoAnulacionBodega(ByVal IdBodega As Integer, ByVal IdMotivoAnulacion As Integer) As Integer

        GetIdMotivoAnulacionBodega = 0

        Try

            Const sp As String = "SELECT * FROM Motivo_anulacion_bodega " &
            " WHERE IdBodega = @IdBodega " &
            " AND IdMotivoAnulacion = @IdMotivoAnulacion"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Using lCommand As New SqlCommand(sp, lConnection)

                lCommand.CommandType = CommandType.Text

                lCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lCommand.Parameters.AddWithValue("@IdMotivoAnulacion", IdMotivoAnulacion)
                lConnection.Open()

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                lConnection.Close()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    GetIdMotivoAnulacionBodega = CInt(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Throw New Exception("GetIdMotivoAnulacionBodega: " & ex.Message)
        End Try

    End Function

    Public Shared Function ActualizarDatos(ByVal pObjMA As clsBeMotivo_anulacion, ByVal pListObjMAB As List(Of clsBeMotivo_anulacion_bodega)) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim ObjMA As New clsLnMotivo_anulacion()
        Dim ObjMAB As New clsLnMotivo_anulacion_bodega()

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            ObjMA.Actualizar(pObjMA, lConnection, lTransaction)

            Dim lMax As Integer = MaxID()

            For Each Obj As clsBeMotivo_anulacion_bodega In pListObjMAB

                If Obj.IdMotivoAnulacionBodega = 0 Then
                    lMax += 1
                    Obj.IdMotivoAnulacionBodega = lMax
                    ObjMAB.Insertar(Obj, lConnection, lTransaction)
                Else
                    If Not Obj.Activo Then
                        Try
                            clsLnMotivo_anulacion_bodega.Eliminar(Obj, lConnection, lTransaction)
                        Catch ex As Exception
                            ObjMAB.Actualizar(Obj, lConnection, lTransaction)
                        End Try
                    End If
                End If

            Next

            lTransaction.Commit()
            lConnection.Close()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
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