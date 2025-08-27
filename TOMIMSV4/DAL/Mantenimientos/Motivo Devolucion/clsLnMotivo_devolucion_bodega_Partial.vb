Imports System.Data.SqlClient

Partial Public Class clsLnMotivo_devolucion_bodega
    Implements IDisposable

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdMotivoDevolucionBodega),0) FROM motivo_Devolucion_bodega"

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

    Public Shared Function GetAllByMotivoDevolucion(ByVal pIdMotivoDevolucion As Integer) As List(Of clsBeMotivo_devolucion_bodega)

        Dim lReturnList As New List(Of clsBeMotivo_devolucion_bodega)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM motivo_devolucion_bodega WHERE IdMotivoDevolucion=@IdMotivoDevolucion"


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdMotivoDevolucion", pIdMotivoDevolucion)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeMotivo_devolucion_bodega

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeMotivo_devolucion_bodega
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

    Public Shared Function Get_Motivos_Devolucion_Bodega_By_IdBodega(ByVal IdBodega As Integer) As List(Of clsBeMotivo_devolucion_bodega)

        Get_Motivos_Devolucion_Bodega_By_IdBodega = Nothing

        Try

            Const sp As String = "SELECT * FROM Motivo_devolucion_bodega WHERE IdBodega = @IdBodega "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.CommandType = CommandType.Text
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim lMotivoDevol As New List(Of clsBeMotivo_devolucion_bodega)
            Dim vMotivoDevol As New clsBeMotivo_devolucion_bodega

            For Each drow In dt.Rows

                vMotivoDevol = New clsBeMotivo_devolucion_bodega
                clsLnMotivo_devolucion_bodega.Cargar(vMotivoDevol, drow)
                lMotivoDevol.Add(vMotivoDevol)

            Next

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            Return lMotivoDevol

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GuardarTransaccion(ByVal pListObjMD As List(Of clsBeMotivo_devolucion))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            For Each Obj As clsBeMotivo_devolucion In pListObjMD
                If Obj.IsNew Then
                    clsLnMotivo_devolucion.Insertar(Obj, lConnection, lTransaction)
                Else
                    clsLnMotivo_devolucion.Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

        Catch ex As Exception
            lTransaction.Rollback()
            Throw ex
        Finally
            lConnection.Close()
        End Try

    End Sub

    Public Shared Function ActualizarDatos(ByVal pObjMD As clsBeMotivo_devolucion, ByVal pListObjMDB As List(Of clsBeMotivo_devolucion_bodega)) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing


        Dim ObjMDB As New clsLnMotivo_devolucion_bodega()

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            clsLnMotivo_devolucion.Actualizar(pObjMD, lConnection, lTransaction)

            Dim lMax As Integer = clsLnMotivo_devolucion_bodega.MaxID()
            For Each Obj As clsBeMotivo_devolucion_bodega In pListObjMDB
                If Obj.IdMotivoDevolucionBodega = 0 Then
                    lMax += 1
                    Obj.IdMotivoDevolucionBodega = lMax
                    ObjMDB.Insertar(Obj, lConnection, lTransaction)
                Else
                    If Not Obj.Activo Then
                        Try
                            ObjMDB.Eliminar(Obj, lConnection, lTransaction)
                        Catch ex As Exception
                            ObjMDB.Actualizar(Obj, lConnection, lTransaction)
                        End Try
                    End If
                End If
            Next

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
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
