Imports System.Data.SqlClient

Partial Public Class clsLnProducto_marca
    Implements IDisposable

    Public Shared lMarcasInMemory As New List(Of clsBeProducto_marca)

    Public Shared Function Listar(ByVal pActivo As Boolean) As DataTable

        Try

            Dim sp As String = "SELECT m.IdMarca,p.IdPropietario,p.nombre_comercial AS Propietario, m.nombre AS Marca " _
                               & "FROM producto_marca AS m " _
                               & "INNER JOIN propietarios AS p ON m.IdPropietario = p.IdPropietario WHERE 1 > 0 "

            If pActivo Then
                sp += " AND m.Activo=1"
            Else
                sp += " AND m.Activo=0"
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection)
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllFiltro(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProducto_marca)

        Dim lReturnList As New List(Of clsBeProducto_marca)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = ""

                If pIdPropietario = 0 Then
                    vSQL = "SELECT * FROM VW_ProductoMarca WHERE 1 > 0 "
                Else
                    vSQL = String.Format("SELECT * FROM VW_ProductoMarca WHERE IdPropietario={0} ", pIdPropietario)
                End If

                If pActivo Then
                    vSQL += " AND Activo=1"
                Else
                    vSQL += " AND Activo=0"
                End If


                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_marca

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_marca

                            Cargar(Obj, lRow)

                            Obj.Propietario = New clsBePropietarios
                            If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                Obj.Propietario.IdPropietario = CType(lRow("IdPropietario"), Integer)
                                Obj.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
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

    Public Shared Function GetSingle(ByVal pIdMarca As Integer) As clsBeProducto_marca

        GetSingle = Nothing

        Try

            Dim ObjMarca As New clsBeProducto_marca()
            Dim IdxMarca As Integer = -1
            Dim vIdMarca As Integer = pIdMarca
            IdxMarca = lMarcasInMemory.FindIndex(Function(x) x.IdMarca = vIdMarca)

            If IdxMarca = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM producto_marca WHERE IdMarca=@IdMarca"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    lConnection.Open()

                    Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                        Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                            lDTA.SelectCommand.Transaction = ltransaction
                            lDTA.SelectCommand.CommandType = CommandType.Text
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdMarca", pIdMarca)

                            Dim lDT As New DataTable
                            lDTA.Fill(lDT)

                            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                                ObjMarca = New clsBeProducto_marca()
                                Dim lRow As DataRow = lDT.Rows(0)
                                Cargar(ObjMarca, lRow)
                                lMarcasInMemory.Add(ObjMarca.Clone())
                                Return ObjMarca

                            End If

                        End Using

                        ltransaction.Commit()

                    End Using

                    lConnection.Close()

                End Using

            Else
                ObjMarca = New clsBeProducto_marca()
                ObjMarca = lMarcasInMemory(IdxMarca).Clone()
                Return ObjMarca
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdMarca As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeProducto_marca

        GetSingle = Nothing

        Try

            Dim ObjMarca As New clsBeProducto_marca()
            Dim IdxMarca As Integer = -1
            Dim vIdMarca As Integer = pIdMarca

            IdxMarca = lMarcasInMemory.FindIndex(Function(x) x.IdMarca = vIdMarca)

            If IdxMarca = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM producto_marca WHERE IdMarca=@IdMarca"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.Transaction = lTransaction
                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdMarca", pIdMarca)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        ObjMarca = New clsBeProducto_marca()

                        Dim lRow As DataRow = lDT.Rows(0)

                        Cargar(ObjMarca, lRow)

                        lMarcasInMemory.Add(ObjMarca.Clone())

                        Return ObjMarca

                    End If

                End Using

            Else

                ObjMarca = New clsBeProducto_marca()
                ObjMarca = lMarcasInMemory(IdxMarca).Clone()
                Return ObjMarca

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdMarca As Integer, ByVal pIdPropietario As Integer) As clsBeProducto_marca

        GetSingle = Nothing

        Try

            Dim ObjMarca As New clsBeProducto_marca()
            Dim IdxMarca As Integer = -1
            Dim vIdMarca As Integer = pIdMarca
            IdxMarca = lMarcasInMemory.FindIndex(Function(x) x.IdMarca = vIdMarca AndAlso x.IdPropietario = pIdPropietario)

            If IdxMarca = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM producto_marca WHERE IdMarca=@IdMarca AND IdPropietario=@IdPropietario"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdMarca", pIdMarca)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            ObjMarca = New clsBeProducto_marca()
                            Dim lRow As DataRow = lDT.Rows(0)
                            Cargar(ObjMarca, lRow)
                            lMarcasInMemory.Add(ObjMarca.Clone())
                            Return ObjMarca

                        End If

                    End Using

                End Using

            Else
                ObjMarca = New clsBeProducto_marca()
                ObjMarca = lMarcasInMemory(IdxMarca).Clone()
                Return ObjMarca
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdMarca As Integer) As Boolean

        Exists = False

        Try

            Dim IdxMarca As Integer = -1
            Dim vIdMarca As Integer = pIdMarca
            IdxMarca = lMarcasInMemory.FindIndex(Function(x) x.IdMarca = vIdMarca)

            If IdxMarca = -1 Then

                Dim vSQL As String = "SELECT COUNT(1) FROM producto_marca WHERE IdMarca=@IdMarca"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    lConnection.Open()

                    Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                        Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                            lCommand.CommandType = CommandType.Text
                            lCommand.Parameters.AddWithValue("@IdMarca", pIdMarca)

                            Dim lReturnValue As Object = lCommand.ExecuteScalar()

                            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                                Exists = CInt(lReturnValue) > 0
                            End If

                        End Using

                        lTransaction.Commit()

                    End Using

                    lConnection.Close()

                End Using

            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdMarca As Integer,
                                  ByVal lConection As SqlConnection,
                                  ByVal lTransaction As SqlTransaction) As Boolean

        Try

            Dim IdxMarca As Integer = -1
            Dim vIdMarca As Integer = pIdMarca
            IdxMarca = lMarcasInMemory.FindIndex(Function(x) x.IdMarca = vIdMarca)

            If IdxMarca = -1 Then

                Dim vSQL As String = "SELECT COUNT(1) FROM producto_marca WHERE IdMarca=@IdMarca"

                Using lCommand As New SqlCommand(vSQL, lConection, lTransaction)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdMarca", pIdMarca)

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        Exists = CInt(lReturnValue) > 0
                    End If

                End Using

            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ExisteProductoLigado(ByVal pIdMarca As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto WHERE IdMarca=@IdMarca"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdMarca", pIdMarca)

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

    Public Shared Sub Delete(ByVal pIdMarca As Integer)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                Using lCommand As New SqlCommand(String.Format("DELETE FROM producto_marca WHERE IdMarca={0}", pIdMarca), lConnection)

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

    Public Shared Sub DeleteByPropietario(ByVal pIdPropietario As Integer)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                Using lCommand As New SqlCommand(String.Format("DELETE FROM producto_marca WHERE IdPropietario={0}", pIdPropietario), lConnection)

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

    Public Shared Function Max_IdMarca() As Integer

        Try

            Max_IdMarca = 1

            Dim vSQL As String = "SELECT  MAX(IdMarca) + 1 as nuevo FROM producto_marca"
            Dim sp As String = vSQL
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count > 0 Then
                Max_IdMarca = IIf(IsDBNull(dt.Rows(0).Item("nuevo")), "1", dt.Rows(0).Item("nuevo"))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GuardarTransaccion(ByVal pListObjPM As List(Of clsBeProducto_marca))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            For Each Obj As clsBeProducto_marca In pListObjPM
                If Obj.IsNew Then
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Obtener(ByRef oBeProducto_marca As clsBeProducto_marca,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Dim IdxMarca As Integer = -1
            Dim vIdMarca As Integer = oBeProducto_marca.IdMarca
            IdxMarca = lMarcasInMemory.FindIndex(Function(x) x.IdMarca = vIdMarca)

            If IdxMarca = -1 Then

                Dim sp As String = "SELECT * FROM Producto_marca
                                     Where(IdMarca = @IdMarca)"

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDMARCA", oBeProducto_marca.IdMarca))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    oBeProducto_marca = New clsBeProducto_marca()
                    Cargar(oBeProducto_marca, dt.Rows(0))
                    lMarcasInMemory.Add(oBeProducto_marca.Clone())
                    Obtener = True
                End If

            Else
                oBeProducto_marca = New clsBeProducto_marca()
                oBeProducto_marca = lMarcasInMemory(IdxMarca).Clone()
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario(ByVal pActivo As Boolean,
                                                    ByVal pIdPropietario As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As DataTable
        Dim lDataTable As New DataTable("Marca")

        Try

            Dim vSQL As String = ""

            If pIdPropietario = 0 Then
                vSQL = "SELECT IdMarca, Nombre  FROM VW_ProductoMarca WHERE 1 > 0 "
            Else
                vSQL = String.Format("SELECT IdMarca, Nombre FROM VW_ProductoMarca WHERE IdPropietario={0}", pIdPropietario)
            End If

            If pActivo = True Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.Fill(lDataTable)
            End Using

            Return lDataTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Propietario(ByVal pIdPropietario As Integer) As DataTable

        Dim lDataTable As New DataTable("Marca")
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = ""

            If pIdPropietario = 0 Then
                vSQL = "SELECT IdMarca, Nombre  FROM VW_ProductoMarca WHERE 1 > 0 AND ACTIVO=1"
            Else
                vSQL = String.Format("SELECT IdMarca, Nombre FROM VW_ProductoMarca WHERE ACTIVO=1 AND IdPropietario={0}", pIdPropietario)
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.Fill(lDataTable)
            End Using

            lTransaction.Commit()

            Return lDataTable

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
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
