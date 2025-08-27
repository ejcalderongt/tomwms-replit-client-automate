Imports System.Data.SqlClient

Partial Public Class clsLnProducto_presentacion_tarima
    Implements IDisposable

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("SELECT ISNULL(Max(IdPresentacionTarima),0) FROM producto_presentacion_tarima"), lConnection)
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

            Dim vSQL As String = "SELECT ISNULL(Max(IdPresentacionTarima),0) FROM producto_presentacion_tarima"

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
    ''' <param name="pIdPresentacionTarima"></param>
    ''' <returns>Devuelve solamente un registro</returns>
    ''' <remarks></remarks>
    Public Shared Function GetSingle(ByVal pIdPresentacionTarima As Integer) As clsBeProducto_presentacion_tarima

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_presentacion_tarima WHERE IdPresentacionTarima=@IdPresentacionTarima"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                '#HS 08112017 Quité query dentro de SqlDataAdapter.
                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacionTarima", pIdPresentacionTarima)

                    Dim lDataTable As New DataTable()
                    lDataAdapter.Fill(lDataTable)

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDataTable.Rows(0)
                        Dim Obj As New clsBeProducto_presentacion_tarima()

                        Cargar(Obj, lRow)
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

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pActivo"></param>
    ''' <returns>Devuelve una lista de rellenados segun la presentacion</returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllByPresentacion(ByVal pActivo As Boolean) As List(Of clsBeProducto_presentacion_tarima)

        Dim lReturnList As New List(Of clsBeProducto_presentacion_tarima)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM VW_Presentacion_Tarima WHERE 1 > 0  "

                If pActivo Then
                    vSQL += " AND activo=1 "
                Else
                    vSQL += " AND activo=0 "
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_presentacion_tarima

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_presentacion_tarima

                            Obj.IdPresentacionTarima = CType(lRow("IdPresentacionTarima"), Integer)

                            If lRow("nombre") IsNot DBNull.Value AndAlso lRow("nombre") IsNot Nothing Then
                                Obj.Presentacion = CType(lRow("nombre"), String)
                            End If

                            If lRow("TipoTarima") IsNot DBNull.Value AndAlso lRow("TipoTarima") IsNot Nothing Then
                                Obj.TipoTarima = CType(lRow("TipoTarima"), String)
                            End If

                            If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                                Obj.Cantidad = CType(lRow("Cantidad"), String)
                            End If

                            If lRow("CantidadPorCama") IsNot DBNull.Value AndAlso lRow("CantidadPorCama") IsNot Nothing Then
                                Obj.CantidadPorCama = CType(lRow("CantidadPorCama"), String)
                            End If

                            If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                Obj.Activo = CType(lRow("activo"), System.Boolean)
                            End If

                            Obj.IsNew = False

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

    '#CM20172410_0145PM: Quité String.Format en Producto_presentacion_tarima.
    Public Shared Function GetAllByIdPresentacion(ByVal IdPresentacion As Integer) As List(Of clsBeProducto_presentacion_tarima)

        GetAllByIdPresentacion = Nothing

        Dim lReturnList As New List(Of clsBeProducto_presentacion_tarima)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM producto_presentacion_tarima WHERE IdPresentacion= @IdPresentacion AND activo = 1"


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", IdPresentacion)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_presentacion_tarima

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_presentacion_tarima

                            Cargar(Obj, lRow)
                            Obj.IsNew = False

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            If lReturnList.Count > 0 Then Return lReturnList

        Catch ex As Exception
            Throw New Exception("Producto_Presentacion_Tarima_GetAllByIdPresentacion: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdPresentacion(ByVal IdPresentacion As Integer,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction) As List(Of clsBeProducto_presentacion_tarima)

        Get_All_By_IdPresentacion = Nothing

        Dim lReturnList As New List(Of clsBeProducto_presentacion_tarima)

        Try

            Dim vSQL As String = "SELECT * FROM producto_presentacion_tarima WHERE IdPresentacion= @IdPresentacion AND activo = 1"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", IdPresentacion)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_presentacion_tarima

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        Obj = New clsBeProducto_presentacion_tarima
                        Cargar(Obj, lRow)
                        Obj.IsNew = False
                        lReturnList.Add(Obj)
                    Next

                End If

            End Using

            If lReturnList.Count > 0 Then Return lReturnList

        Catch ex As Exception
            Throw New Exception("Producto_Presentacion_Tarima_GetAllByIdPresentacion: " & ex.Message)
        End Try

    End Function

    '#CM20172410_0145PM: Quité String.Format en Producto_presentacion_tarima.
    Public Shared Function Get_All_By_IdProducto(ByVal IdProducto As Integer) As List(Of clsBeProducto_presentacion_tarima)


        Dim lReturnList As New List(Of clsBeProducto_presentacion_tarima)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM VW_Presentacion_Tarima WHERE IdProducto = @IdProducto AND activo = 1"


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_presentacion_tarima

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_presentacion_tarima

                            Cargar(Obj, lRow)
                            Obj.IsNew = False

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("Producto_Presentacion_Tarima_GetAllByIdPresentacion: " & ex.Message)
        End Try

    End Function


    Public Shared Sub Desactivar(ByVal pIdPresentacionTarima As Integer)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                Using lCommand As New SqlCommand(String.Format("UPDATE producto_presentacion_tarima SET activo=0 WHERE IdPresentacionTarima={0}", pIdPresentacionTarima), lConnection)

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
