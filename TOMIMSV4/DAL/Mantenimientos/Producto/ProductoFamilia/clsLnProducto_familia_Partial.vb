Imports System.Data.SqlClient

Partial Public Class clsLnProducto_familia
    Implements IDisposable

    Public Shared Function Listar(ByVal pActivo As Boolean) As DataTable

        Try

            Dim sp As String = "SELECT f.IdFamilia,p.IdPropietario,p.nombre_comercial AS Propietario, f.nombre AS Familia " _
                               & "FROM producto_familia AS f " _
                               & "INNER JOIN propietarios AS p ON f.IdPropietario = p.IdPropietario WHERE 1 > 0"

            If pActivo = True Then
                sp += " AND f.Activo=1"
            Else
                sp += " AND f.Activo=0"
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

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal pFiltro As String, ByVal pIdPropietario As Integer) As List(Of clsBeProducto_familia)

        Dim lReturnList As New List(Of clsBeProducto_familia)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = ""

                If pIdPropietario = 0 Then
                    vSQL = "SELECT * FROM VW_ProductoFamilia WHERE 1 > 0 "
                Else
                    vSQL = String.Format("SELECT * FROM VW_ProductoFamilia WHERE IdPropietario={0}", pIdPropietario)
                End If

                If pActivo = True Then
                    vSQL += " AND Activo=1"
                Else
                    vSQL += " AND Activo=0"
                End If

                If String.IsNullOrEmpty(pFiltro) = False Then
                    vSQL += String.Format(" AND (IdFamilia LIKE '%{0}%'", pFiltro)
                    vSQL += String.Format(" OR Familia LIKE '%{0}%'", pFiltro)
                    vSQL += String.Format(" OR Propietario LIKE '%{0}%')", pFiltro)
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_familia

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_familia
                            Cargar(Obj, lRow)

                            If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                Obj.Propietario = New clsBePropietarios
                                Obj.Propietario.IdPropietario = CType(lRow("IdPropietario"), Int32)
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

    Public Shared Function Get_All_By_IdPropietario(ByVal pActivo As Boolean,
                                                    ByVal pIdPropietario As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As DataTable
        Dim lDataTable As New DataTable("Familia")

        Try

            Dim vSQL As String = ""

            If pIdPropietario = 0 Then
                vSQL = "SELECT IdFamilia, Nombre  FROM VW_ProductoFamilia WHERE 1 > 0 "
            Else
                vSQL = String.Format("SELECT IdFamilia, Nombre FROM VW_ProductoFamilia WHERE IdPropietario={0}", pIdPropietario)
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

    Public Shared Function Get_All_By_IdPropietario(ByVal pActivo As Boolean,
                                                    ByVal pIdPropietario As Integer) As DataTable
        Dim lDataTable As New DataTable("Familia")

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = ""

                If pIdPropietario = 0 Then
                    vSQL = "SELECT IdFamilia, Nombre  FROM VW_ProductoFamilia WHERE 1 > 0 "
                Else
                    vSQL = String.Format("SELECT IdFamilia, Nombre FROM VW_ProductoFamilia WHERE IdPropietario={0}", pIdPropietario)
                End If

                If pActivo = True Then
                    vSQL += " AND Activo=1"
                Else
                    vSQL += " AND Activo=0"
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.Fill(lDataTable)
                End Using

            End Using

            Return lDataTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdFamilia As Integer) As clsBeProducto_familia

        GetSingle = Nothing

        Try

            Dim Obj As New clsBeProducto_familia()


            Dim vSQL As String = "SELECT * FROM producto_familia WHERE IdFamilia=@IdFamilia"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdFamilia", pIdFamilia)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDataTable.Rows(0)
                            Obj = New clsBeProducto_familia()
                            Cargar(Obj, lRow)
                            GetSingle = Obj

                        End If

                    End Using


                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdFamilia As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeProducto_familia

        GetSingle = Nothing

        Try

            Dim Obj As New clsBeProducto_familia()
            Obj = Nothing
            Dim IdxFamilia As Integer = -1

            Dim vSQL As String = "SELECT * FROM producto_familia WHERE IdFamilia=@IdFamilia"

            Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                lDataAdapter.SelectCommand.CommandType = CommandType.Text
                lDataAdapter.SelectCommand.Transaction = lTransaction
                lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdFamilia", pIdFamilia)

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)
                    Obj = New clsBeProducto_familia()
                    Cargar(Obj, lRow)
                    Return Obj

                End If

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdClasificacion As Integer,
                                     ByVal pIdPropietario As Integer) As clsBeProducto_familia

        GetSingle = Nothing

        Try

            Dim Obj As New clsBeProducto_familia()
            Dim IdxFamilia As Integer = -1

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_familia WHERE IdFamilia=@IdFamilia AND IdPropietario=@IdPropietario"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdFamilia", pIdClasificacion)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Dim lRow As DataRow = lDataTable.Rows(0)
                            Obj = New clsBeProducto_familia()
                            Cargar(Obj, lRow)
                            GetSingle = Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdFamilia As Integer) As Boolean

        Exists = False

        Try

            Dim vSQL As String = "SELECT COUNT(1) FROM producto_familia WHERE IdFamilia=@IdFamilia"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdFamilia", pIdFamilia)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        lConnection.Close()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Exists = CInt(lReturnValue) > 0
                        End If

                    End Using


                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists_By_Codigo(ByVal pCodigo As String) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto_familia WHERE codigo=@codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@codigo", pCodigo)

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

    Public Shared Function Exists(ByVal pIdFamilia As Integer,
                                  ByVal lConnection As SqlConnection,
                                  ByVal lTransaction As SqlTransaction) As Boolean

        Exists = False

        Try

            Dim vSQL As String = "SELECT COUNT(1) FROM producto_familia WHERE IdFamilia=@IdFamilia"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdFamilia", pIdFamilia)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Exists = CInt(lReturnValue) > 0
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdFamilia_By_Codigo(ByVal pCodigo As String,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As Integer

        Get_IdFamilia_By_Codigo = 0

        Try

            Dim vSQL As String = "SELECT IdFamilia FROM producto_familia WHERE Codigo=@Codigo"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_IdFamilia_By_Codigo = CInt(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ExisteProductoLigado(ByVal pIdFamilia As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto WHERE IdFamilia=@IdFamilia"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdFamilia", pIdFamilia)

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

    Public Shared Sub Delete(ByVal pIdFamilia As Integer)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("DELETE FROM producto_familia WHERE IdFamilia={0}", pIdFamilia), lConnection)

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

                Using lCommand As New SqlCommand(String.Format("DELETE FROM producto_familia WHERE IdPropietario={0}", pIdPropietario), lConnection)

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

    Public Shared Function Max_IdProducto_Familia() As Integer

        Try

            Max_IdProducto_Familia = 1

            Dim vSQL As String = "SELECT MAX(IdFamilia) + 1 as nuevo FROM producto_familia"

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
                Max_IdProducto_Familia = IIf(IsDBNull(dt.Rows(0).Item("nuevo")), "1", dt.Rows(0).Item("nuevo"))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function MaxId(ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try


            Dim vSQL As String = "SELECT  ISNULL(MAX(IdFamilia),0) + 1 as nuevo FROM producto_familia"
            Dim sp As String = vSQL
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count > 0 Then
                MaxId = IIf(IsDBNull(dt.Rows(0).Item("nuevo")), "1", dt.Rows(0).Item("nuevo"))
            Else
                MaxId = 1
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GuardarTransaccion(ByVal pListObjPF As List(Of clsBeProducto_familia))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            For Each Obj As clsBeProducto_familia In pListObjPF
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

    Public Shared Function Obtener(ByRef oBeProducto_familia As clsBeProducto_familia,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Dim sp As String = "SELECT * FROM Producto_familia
                                    Where(IdFamilia = @IdFamilia)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeProducto_familia.IdFamilia))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_familia, dt.Rows(0))
                Obtener = True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Single_By_Nombre_By_IdPropietario(ByVal pNombre As String, ByVal pIdPropietario As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As clsBeProducto_familia

        Get_Single_By_Nombre_By_IdPropietario = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_familia 
                                  WHERE IdPropietario=@IdProietario and Nombre like '%" + pNombre + "%'"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdProietario", pIdPropietario))
                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeProducto_familia()
                    Cargar(Obj, lRow)
                    Get_Single_By_Nombre_By_IdPropietario = Obj
                    Return Get_Single_By_Nombre_By_IdPropietario

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean,
                                          ByVal pIdPropietario As Integer,
                                          ByVal lConnection As SqlConnection,
                                          ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto_familia)

        Dim lReturnList As New List(Of clsBeProducto_familia)

        Try

            Dim vSQL As String = ""

            If pIdPropietario = 0 Then
                vSQL = "SELECT * FROM VW_ProductoFamilia WHERE 1 > 0 "
            Else
                vSQL = String.Format("SELECT * FROM VW_ProductoFamilia WHERE IdPropietario={0}", pIdPropietario)
            End If

            If pActivo = True Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_familia

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_familia
                        Cargar(Obj, lRow)

                        If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                            Obj.Propietario = New clsBePropietarios
                            Obj.Propietario.IdPropietario = CType(lRow("IdPropietario"), Int32)
                            Obj.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                        End If

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Nombre(ByVal pCodigo As String,
                                                ByVal pConnection As SqlConnection,
                                                ByVal pTransaction As SqlTransaction) As clsBeProducto_familia

        Get_Single_By_Nombre = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_familia WHERE Nombre like '%" + pCodigo + "%'"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeProductoFamilia As New clsBeProducto_familia()
                    Cargar(BeProductoFamilia, lRow)
                    Get_Single_By_Nombre = BeProductoFamilia
                    Return BeProductoFamilia

                End If

            End Using

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

    Public Shared Function Get_Single_By_Codigo(ByVal pCodigo As String,
                                                ByVal pConnection As SqlConnection,
                                                ByVal pTransaction As SqlTransaction) As clsBeProducto_familia

        Get_Single_By_Codigo = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_familia WHERE codigo = '" + pCodigo + "'"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeProductoFamilia As New clsBeProducto_familia()
                    Cargar(BeProductoFamilia, lRow)
                    Get_Single_By_Codigo = BeProductoFamilia
                    Return BeProductoFamilia

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class