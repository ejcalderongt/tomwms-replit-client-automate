Imports System.Data.SqlClient

Partial Public Class clsLnProducto_estado_ubic
    Implements IDisposable

    Public Shared Function Get_All_By_IdEstado_And_IdBodega(ByVal pIdEstado As Integer,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal Activo As Boolean) As List(Of clsBeProducto_estado_ubic)

        Get_All_By_IdEstado_And_IdBodega = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto_estado_ubic)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoEstadoUbicacion 
                                WHERE IdBodega = @IdBodega AND IdEstado=@IdEstado "

                    If Activo Then
                        vSQL += " AND activo=1"
                    Else
                        vSQL += " AND activo=0"
                    End If


                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_estado_ubic

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_estado_ubic()
                                Cargar(Obj, lRow)

                                If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                                    Obj.Estado = CType(lRow("Estado"), String)
                                End If

                                If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                                    Obj.Ubicacion = CType(lRow("Ubicacion"), String)
                                End If

                                If lRow("IdBodega") IsNot DBNull.Value AndAlso lRow("IdBodega") IsNot Nothing Then
                                    Obj.IdBodega = CType(lRow("IdBodega"), Integer)
                                End If

                                If lRow("Bodega") IsNot DBNull.Value AndAlso lRow("Bodega") IsNot Nothing Then
                                    Obj.Bodega = CType(lRow("Bodega"), String)
                                End If

                                Obj.IsNew = False

                                lReturnList.Add(Obj)

                            Next

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

    Public Shared Function Get_All_By_IdEstado(ByVal pIdEstado As Integer,
                                               ByVal Activo As Boolean) As List(Of clsBeProducto_estado_ubic)

        Get_All_By_IdEstado = Nothing

        Try

            Dim lReturnList As New List(Of clsBeProducto_estado_ubic)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProductoEstadoUbicacion WHERE IdEstado=@IdEstado"

                    If Activo Then
                        vSQL += " AND activo=1"
                    Else
                        vSQL += " AND activo=0"
                    End If


                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeProductoEstadoUbic As clsBeProducto_estado_ubic

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeProductoEstadoUbic = New clsBeProducto_estado_ubic()
                                Cargar(BeProductoEstadoUbic, lRow)

                                If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                                    BeProductoEstadoUbic.Estado = CType(lRow("Estado"), String)
                                End If

                                If lRow("Ubicacion") IsNot DBNull.Value AndAlso lRow("Ubicacion") IsNot Nothing Then
                                    BeProductoEstadoUbic.Ubicacion = CType(lRow("Ubicacion"), String)
                                End If

                                If lRow("IdBodega") IsNot DBNull.Value AndAlso lRow("IdBodega") IsNot Nothing Then
                                    BeProductoEstadoUbic.IdBodega = CType(lRow("IdBodega"), Integer)
                                End If

                                If lRow("Bodega") IsNot DBNull.Value AndAlso lRow("Bodega") IsNot Nothing Then
                                    BeProductoEstadoUbic.Bodega = CType(lRow("Bodega"), String)
                                End If

                                BeProductoEstadoUbic.IsNew = False

                                lReturnList.Add(BeProductoEstadoUbic)

                            Next

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

    Public Shared Function AceptaEstadoMalo(pIdUbicacion As Integer, pEstMalo As Integer) As Boolean

        Dim items As New List(Of clsBeProducto_estado_ubic)

        Try

            items = Get_All_By_IdEstado(pEstMalo, True)

            For Each item As clsBeProducto_estado_ubic In items
                If item.IdUbicacionDefecto = pIdUbicacion Then Return True
            Next

            Return False

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function MaxID(ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdProductoEstadUbic),0) FROM producto_estado_ubic"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

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