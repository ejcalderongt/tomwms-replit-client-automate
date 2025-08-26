Imports System.Data.SqlClient

Partial Public Class clsLnProducto_sustituto
    Implements IDisposable

    Public Shared Function GetAllByProducto(ByVal pIdProductoOriginal As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_sustituto)

        Dim lReturnList As New List(Of clsBeProducto_sustituto)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM VW_ProductoSustituto WHERE IdProductoOriginal=@IdProductoOriginal"

                If pActivo Then
                    vSQL += " AND activo=1 "
                Else
                    vSQL += " AND activo=0 "
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoOriginal", pIdProductoOriginal)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_sustituto

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_sustituto

                            Obj.IdProductoSustituto = CType(lRow("Código"), Int32)

                            If lRow("IdProductoOriginal") IsNot DBNull.Value AndAlso lRow("IdProductoOriginal") IsNot Nothing Then
                                Obj.IdProductoOriginal = CType(lRow("IdProductoOriginal"), Int32)
                            End If

                            If lRow("IdProductoPresentacionOriginal") IsNot DBNull.Value AndAlso lRow("IdProductoPresentacionOriginal") IsNot Nothing Then
                                Obj.IdProductoPresentacionOriginal = CType(lRow("IdProductoPresentacionOriginal"), Int32)
                                Obj.ProductoPresentacionOriginal = New clsBeProducto_Presentacion
                                Obj.ProductoPresentacionOriginal.Nombre = CType(lRow("Presentación Original"), String)
                            End If

                            If lRow("IdProductoReemplazo") IsNot DBNull.Value AndAlso lRow("IdProductoReemplazo") IsNot Nothing Then
                                Obj.IdProductoReemplazo = CType(lRow("IdProductoReemplazo"), Int32)
                                Obj.ProductoReemplazo = New clsBeProducto
                                Obj.ProductoReemplazo.Nombre = CType(lRow("Producto Reemplazo"), String)
                            End If

                            If lRow("IdProductoPresentacionReemplazo") IsNot DBNull.Value AndAlso lRow("IdProductoPresentacionReemplazo") IsNot Nothing Then
                                Obj.IdProductoPresentacionReemplazo = CType(lRow("IdProductoPresentacionReemplazo"), Int32)
                                Obj.ProductoPresentacionReemplazo = New clsBeProducto_Presentacion
                                Obj.ProductoPresentacionReemplazo.Nombre = CType(lRow("Presentación Reemplazo"), String)
                            End If

                            If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                Obj.Activo = CType(lRow("activo"), Boolean)
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

    Public Shared Function GetSingle(ByVal pIdProductoSustituto As Integer) As clsBeProducto_sustituto

        Try

            Dim vSQL As String = "SELECT ps.*,p.nombre FROM producto_sustituto AS ps " _
                   & "INNER JOIN producto AS p ON ps.IdProductoReemplazo = p.IdProducto " _
                   & "WHERE IdProductoSustituto=@IdProductoSustituto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoSustituto", pIdProductoSustituto)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDataTable.Rows(0)
                        Dim Obj As New clsBeProducto_sustituto()

                        Cargar(Obj, lRow)

                        If lRow("IdProductoReemplazo") IsNot DBNull.Value AndAlso lRow("IdProductoReemplazo") IsNot Nothing Then

                            Obj.ProductoReemplazo = New clsBeProducto
                            Obj.ProductoReemplazo.Nombre = CType(lRow("nombre"), String)

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

    Public Shared Function Exists(ByVal pidProductoOriginal As Integer, ByVal pIdProductoPresentacionOriginal As Integer) As Boolean

        Dim lExists As Boolean = False

        Dim vSQL As String = "SELECT COUNT(1) FROM producto_sustituto WHERE IdProductoOriginal=@IdProductoOriginal AND IdProductoPresentacionOriginal=@IdProductoPresentacionOriginal"

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@idProductoOriginal", pidProductoOriginal)
                    lCommand.Parameters.AddWithValue("@IdProductoPresentacionOriginal", pIdProductoPresentacionOriginal)

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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdProductoSustituto),0) FROM producto_sustituto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

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

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdProductoSustituto),0) FROM producto_sustituto"

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

                lCommand.Dispose()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Delete(ByVal pIdProductoSustituto As Integer)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                Using lCommand As New SqlCommand(String.Format("UPDATE producto_sustituto SET activo=0 WHERE IdProductoSustituto={0}", pIdProductoSustituto), lConnection)

                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()
                    lCommand.ExecuteNonQuery()
                    lConnection.Close()

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub GuardarTransaccion(ByVal pListObjPS As List(Of clsBeProducto_sustituto))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            For Each Obj As clsBeProducto_sustituto In pListObjPS
                If Obj.IsNew Then
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
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
