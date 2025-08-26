Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProducto_rellenado
    Implements IDisposable

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdRellenado),0) FROM producto_rellenado"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 07112017 Quité query dentro de SqlCommand.
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

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pConnection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdRellenado),0) FROM producto_rellenado"

            '#HS 07112017 Quité query dentro de SqlCommand.
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


    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pIdRellenado"></param>
    ''' <returns>Devuelve solamente un registro</returns>
    ''' <remarks></remarks>
    Public Shared Function GetSingle(ByVal pIdRellenado As Integer) As clsBeProducto_rellenado

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM VW_ProductoRellenado WHERE IdRellenado=@IdRellenado"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdRellenado", pIdRellenado)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDataTable.Rows(0)
                        Dim Obj As New clsBeProducto_rellenado()

                        Cargar(Obj, lRow)

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then

                            Obj.IdPresentacion = CType(lRow("IdPresentacion"), Int32)
                            Obj.Presentacion = CType(lRow("Presentación"), String)

                        End If

                        If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then

                            Obj.IdProductoEstado = CType(lRow("IdProductoEstado"), Int32)
                            Obj.Estado = CType(lRow("Estado"), String)

                        End If

                        If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then

                            Obj.IdUbicacion = CType(lRow("IdUbicacion"), Int32)
                            Obj.Ubicacion = CType(lRow("Ubicación"), String)

                        End If

                        Obj.IsNew = False

                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdRellenado As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeProducto_rellenado

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM VW_ProductoRellenado WHERE IdRellenado=@IdRellenado"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdRellenado", pIdRellenado)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Dim Obj As New clsBeProducto_rellenado()

                    Cargar(Obj, lRow)

                    If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then

                        Obj.IdPresentacion = CType(lRow("IdPresentacion"), Int32)
                        Obj.Presentacion = CType(lRow("Presentación"), String)

                    End If

                    If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then

                        Obj.IdProductoEstado = CType(lRow("IdProductoEstado"), Int32)
                        Obj.Estado = CType(lRow("Estado"), String)

                    End If

                    If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then

                        Obj.IdUbicacion = CType(lRow("IdUbicacion"), Int32)
                        Obj.Ubicacion = CType(lRow("Ubicación"), String)

                    End If

                    Obj.IsNew = False

                    GetSingle = Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pIdPresentacion"></param>
    ''' <param name="pActivo"></param>
    ''' <returns>Devuelve una lista de rellenados segun la presentacion</returns>
    ''' <remarks></remarks>
    ''' '#CM20172310_0522PM: Corrección de String.Format en Producto_rellenado
    Public Shared Function GetAllByPresentacion(ByVal pIdPresentacion As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_rellenado)

        GetAllByPresentacion = Nothing

        Dim lReturnList As New List(Of clsBeProducto_rellenado)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM VW_ProductoRellenado WHERE IdPresentacion=@IdPresentacion"

                If pActivo Then
                    vSQL += " AND activo=1 "
                Else
                    vSQL += " AND activo=0 "
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_rellenado

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_rellenado

                            Cargar(Obj, lRow)

                            If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                Obj.IdPresentacion = CType(lRow("IdPresentacion"), Int32)
                                Obj.Presentacion = CType(lRow("Presentación"), String)
                            End If

                            If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                                Obj.IdProductoEstado = CType(lRow("IdProductoEstado"), Int32)
                                Obj.Estado = CType(lRow("Estado"), String)
                            End If

                            If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                Obj.IdUbicacion = CType(lRow("IdUbicacion"), Int32)
                                Obj.Ubicacion = CType(lRow("Ubicación"), String)
                            End If

                            Obj.IsNew = False

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            If lReturnList.Count > 0 Then Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPresentacion(ByVal pIdPresentacion As Integer,
                                                     ByVal pActivo As Boolean,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeProducto_rellenado)

        Get_All_By_IdPresentacion = Nothing

        Dim lReturnList As New List(Of clsBeProducto_rellenado)

        Try

            Dim vSQL As String = "SELECT * FROM VW_ProductoRellenado WHERE IdPresentacion=@IdPresentacion"

            If pActivo Then
                vSQL += " AND activo=1 "
            Else
                vSQL += " AND activo=0 "
            End If


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_rellenado

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_rellenado

                        Cargar(Obj, lRow)

                        If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                            Obj.IdPresentacion = CType(lRow("IdPresentacion"), Int32)
                            Obj.Presentacion = CType(lRow("Presentación"), String)
                        End If

                        If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                            Obj.IdProductoEstado = CType(lRow("IdProductoEstado"), Int32)
                            Obj.Estado = CType(lRow("Estado"), String)
                        End If

                        If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                            Obj.IdUbicacion = CType(lRow("IdUbicacion"), Int32)
                            Obj.Ubicacion = CType(lRow("Ubicación"), String)
                        End If

                        Obj.IsNew = False

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            If lReturnList.Count > 0 Then Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CM20172310_0522PM: Corrección de String.Format en Producto_rellanado
    Public Shared Function Get_All_By_IdProducto(ByVal pIdProducto As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_rellenado)

        Dim lReturnList As New List(Of clsBeProducto_rellenado)

        Try

            Dim vSQL As String = "SELECT * FROM VW_ProductoRellenado WHERE IdProducto=@IdProducto"

            If pActivo Then
                vSQL += " AND activo=1 "
            Else
                vSQL += " AND activo=0 "
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_rellenado

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_rellenado

                                Cargar(Obj, lRow)

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.IdPresentacion = CType(lRow("IdPresentacion"), Int32)
                                    Obj.Presentacion = CType(lRow("Presentación"), String)
                                End If

                                If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                                    Obj.IdProductoEstado = CType(lRow("IdProductoEstado"), Int32)
                                    Obj.Estado = CType(lRow("Estado"), String)
                                End If

                                If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                    Obj.IdUbicacion = CType(lRow("IdUbicacion"), Int32)
                                    Obj.Ubicacion = CType(lRow("Ubicación"), String)
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

    Public Shared Function ExisteConfiguracion(ByVal pIdPresentacion As Integer,
                                               ByVal pIdUbicacion As Integer,
                                               ByVal pIdBodega As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto_rellenado 
                                  WHERE IdPresentacion=@IdPresentacion 
                                  AND IdUbicacion=@IdUbicacion 
                                  AND IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Transaction = lTransaction
                        lCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)
                        lCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Configuracion_Producto(ByVal pProdRellenado As clsBeProducto_rellenado,
                                                         ByRef pConnection As SqlConnection,
                                                         ByRef pTransaction As SqlTransaction) As Integer

        Try

            Dim lExists As Integer = 0

            Dim vSQL As String = "SELECT IdRellenado
                                  FROM producto_rellenado 
                                  WHERE IdPresentacion = @IdPresentacion  AND 
                                        IdUnidadMedidaBasica = @IdUnidadMedidaBasica AND 
	                                    IdPresentacionAbastercerCon = @IdPresentacionAbastercerCon AND 
	                                    IdProductoBodega = @IdProductoBodega AND 
	                                    IdUbicacion = @IdUbicacion AND
                                        IdBodega = @IdBodega "
            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdPresentacion", IIf(pProdRellenado.IdPresentacion = 0, DBNull.Value, pProdRellenado.IdPresentacion))
                lCommand.Parameters.AddWithValue("@IdUnidadMedidaBasica", pProdRellenado.IdUnidadMedidaBasica)
                lCommand.Parameters.AddWithValue("@IdPresentacionAbastercerCon", IIf(pProdRellenado.IdPresentacionAbastercerCon = 0, DBNull.Value, pProdRellenado.IdPresentacionAbastercerCon))
                lCommand.Parameters.AddWithValue("@IdProductoBodega", pProdRellenado.IdProductoBodega)
                lCommand.Parameters.AddWithValue("@IdUbicacion", pProdRellenado.IdUbicacion)
                lCommand.Parameters.AddWithValue("@IdBodega", pProdRellenado.IdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue)
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Ubicacion_Rellenado(ByVal pIdUbicacion As String,
                                                      ByVal pIdBodega As Integer,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lProducto As Integer = 0

            Dim sp As String = "select IdProductoBodega 
                                from producto_rellenado 
                                where IdBodega=@IdBodega and IdUbicacion=@IdUbicacion "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lProducto = CInt(lReturnValue)
                End If

            End Using

            Return lProducto

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Desactivar(ByVal pIdRellenado As Integer) As Boolean

        Dim rowsAffected As Integer = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                Using lCommand As New SqlCommand(String.Format("UPDATE producto_rellenado SET activo=0 WHERE IdRellenado={0}", pIdRellenado), lConnection)

                    lCommand.CommandType = CommandType.Text
                    lConnection.Open()
                    rowsAffected = lCommand.ExecuteNonQuery()
                    lConnection.Close()

                End Using

            End Using

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Insert_Multiple(ByVal pListObjPR As List(Of clsBeProducto_rellenado))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()

            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lMaxID As Integer = clsLnProducto_rellenado.MaxID()
            For Each Obj As clsBeProducto_rellenado In pListObjPR
                If Obj.IsNew Then
                    Obj.IdRellenado = lMaxID
                    lMaxID += 1
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
