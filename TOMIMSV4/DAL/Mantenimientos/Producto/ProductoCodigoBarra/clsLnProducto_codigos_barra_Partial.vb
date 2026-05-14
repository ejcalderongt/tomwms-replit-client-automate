Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProducto_codigos_barra
    Implements IDisposable

    Private Shared lCodigosBarraInMemory As New List(Of clsBeProducto_codigos_barra)

    Public Shared Function GetSingle(ByVal pIdProducto As Integer,
                                     ByVal pIdProveedor As Integer,
                                     ByVal pCodigoBarra As String) As clsBeProducto_codigos_barra

        GetSingle = Nothing

        Try

            Dim IdxCodigoBarra As Integer = lCodigosBarraInMemory.FindIndex(Function(x) x.IdProducto = pIdProducto AndAlso x.IdProveedor = pIdProducto AndAlso x.Codigo_barra = pCodigoBarra)

            If IdxCodigoBarra = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM producto_codigos_barra WHERE IdProducto=@IdProducto AND IdProveedor=@IdProveedor AND codigo_barra=@codigo_barra"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    lConnection.Open()

                    Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                        Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                            lDTA.SelectCommand.CommandType = CommandType.Text
                            lDTA.SelectCommand.Transaction = lTransaction
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdProveedor", pIdProveedor)
                            lDTA.SelectCommand.Parameters.AddWithValue("@codigo_barra", pCodigoBarra)

                            Dim lDT As New DataTable()
                            lDTA.Fill(lDT)

                            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                                Dim lRow As DataRow = lDT.Rows(0)
                                Dim Obj As New clsBeProducto_codigos_barra()
                                Cargar(Obj, lRow)
                                Return Obj

                            End If

                        End Using

                        lTransaction.Commit()

                    End Using

                    lConnection.Close()

                End Using

            Else
                Return lCodigosBarraInMemory(IdxCodigoBarra)
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeProducto_codigos_barra)

        Dim lReturnList As New List(Of clsBeProducto_codigos_barra)

        Try

            Dim vSQL As String = "SELECT pcb.IdProducto,pcb.IdProveedor,pr.nombre AS Producto,p.nombre AS Proveedor,pcb.codigo_barra AS 'Código de Barra',pcb.activo,pcb.user_agr,pcb.fec_agr,pcb.user_mod,pcb.fec_mod " _
                                  & "FROM producto_codigos_barra AS pcb " _
                                  & "INNER JOIN proveedor AS p ON pcb.IdProveedor = p.IdProveedor " _
                                  & "INNER JOIN producto AS pr ON pcb.IdProducto = pr.IdProducto WHERE 1 > 0 "

            If pActivo Then
                vSQL += " AND pcb.Activo=1"
            Else
                vSQL += " AND pcb.Activo=0"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_codigos_barra

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_codigos_barra()

                                Obj.IdProveedor = CType(lRow("IdProveedor"), Int32)
                                Obj.Codigo_barra = CType(lRow("Código de Barra"), String)

                                If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                    Obj.IdProducto = CType(lRow("IdProducto"), Int32)
                                End If

                                If lRow("IdProveedor") IsNot DBNull.Value AndAlso lRow("IdProveedor") IsNot Nothing Then
                                    Obj.Proveedor = New clsBeProveedor
                                    Obj.Proveedor.Nombre = CType(lRow("Proveedor"), String)
                                End If

                                If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                    Obj.Activo = CType(lRow("activo"), Boolean)
                                End If

                                If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                                    Obj.User_agr = CType(lRow("user_agr"), String)
                                End If

                                If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                                    Obj.Fec_agr = CType(lRow("fec_agr"), DateTime)
                                End If

                                If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
                                    Obj.User_mod = CType(lRow("user_mod"), String)
                                End If

                                If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
                                    Obj.Fec_mod = CType(lRow("fec_mod"), DateTime)
                                End If

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

    Public Shared Function Get_All_By_IdProducto(ByVal pIdProducto As Integer,
                                                 ByVal pActivo As Boolean) As List(Of clsBeProducto_codigos_barra)

        Get_All_By_IdProducto = Nothing

        Dim lReturnList As New List(Of clsBeProducto_codigos_barra)

        Try

            Dim vSQL As String = "SELECT pcb.IdProducto,pcb.IdProveedor,pr.nombre AS Producto,p.nombre AS Proveedor,pcb.codigo_barra AS 'Código de Barra',pcb.activo,pcb.user_agr,pcb.fec_agr,pcb.user_mod,pcb.fec_mod FROM producto_codigos_barra AS pcb INNER JOIN proveedor AS p ON pcb.IdProveedor = p.IdProveedor INNER JOIN producto AS pr ON pcb.IdProducto = pr.IdProducto WHERE pcb.IdProducto=@IdProducto"

            If pActivo Then
                vSQL += " AND pcb.activo=1 "
            Else
                vSQL += " AND pcb.activo=0 "
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_codigos_barra

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_codigos_barra()
                                Obj.IdProducto = CType(lRow("IdProducto"), Int32)
                                Obj.IdProveedor = CType(lRow("IdProveedor"), Int32)
                                Obj.Codigo_barra = CType(lRow("Código de Barra"), String)

                                If lRow("IdProveedor") IsNot DBNull.Value AndAlso lRow("IdProveedor") IsNot Nothing Then
                                    Obj.Proveedor = New clsBeProveedor
                                    Obj.Proveedor.Nombre = CType(lRow("Proveedor"), String)
                                End If

                                If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                    Obj.Activo = CType(lRow("activo"), Boolean)
                                End If

                                If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                                    Obj.User_agr = CType(lRow("user_agr"), String)
                                End If

                                If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                                    Obj.Fec_agr = CType(lRow("fec_agr"), DateTime)
                                End If

                                If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
                                    Obj.User_mod = CType(lRow("user_mod"), String)
                                End If

                                If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
                                    Obj.Fec_mod = CType(lRow("fec_mod"), DateTime)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            If lReturnList.Count > 0 Then Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#EJC20171107_REF17_0658PM: Función GetAllByProducto con transacción
    Public Shared Function Get_All_By_IdProducto(ByVal pIdProducto As Integer,
                                                 ByVal pActivo As Boolean,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction) As List(Of clsBeProducto_codigos_barra)

        Get_All_By_IdProducto = Nothing

        Dim lReturnList As New List(Of clsBeProducto_codigos_barra)

        Try

            If lCodigosBarraInMemory Is Nothing Then
                lCodigosBarraInMemory = New List(Of clsBeProducto_codigos_barra)
            End If

            Dim lCodigos As New List(Of clsBeProducto_codigos_barra)

            lCodigos = lCodigosBarraInMemory.FindAll(Function(x) x IsNot Nothing AndAlso x.IdProducto = pIdProducto)

            'Dim lCodigos As New List(Of clsBeProducto_codigos_barra)
            'lCodigos = lCodigosBarraInMemory.FindAll(Function(x) x.IdProducto = pIdProducto)

            If Not lCodigos Is Nothing AndAlso lCodigos.Count > 0 Then
                Return lCodigos
            Else

                Dim vSQL As String = "SELECT 
                                        pcb.IdProducto,
                                        pcb.IdProveedor,
                                        pr.nombre AS Producto,
                                        p.nombre AS Proveedor,
                                        pcb.codigo_barra AS 'Código de Barra',
                                        pcb.activo,
                                        pcb.user_agr,
                                        pcb.fec_agr,
                                        pcb.user_mod,
                                        pcb.fec_mod,
                                        pcb.IdTalla,
                                        pcb.IdColor
                                    FROM producto_codigos_barra AS pcb 
                                    INNER JOIN proveedor AS p ON pcb.IdProveedor = p.IdProveedor 
                                    INNER JOIN producto AS pr ON pcb.IdProducto = pr.IdProducto 
                                    WHERE pcb.IdProducto=@IdProducto"

                If pActivo Then
                    vSQL += " AND pcb.activo=1 "
                Else
                    vSQL += " AND pcb.activo=0 "
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Transaction = lTransaction
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim BeProductoCodigosBarra As clsBeProducto_codigos_barra

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            BeProductoCodigosBarra = New clsBeProducto_codigos_barra()
                            BeProductoCodigosBarra.IdProducto = CType(lRow("IdProducto"), Int32)
                            BeProductoCodigosBarra.IdProveedor = CType(lRow("IdProveedor"), Int32)
                            BeProductoCodigosBarra.Codigo_barra = CType(lRow("Código de Barra"), String)

                            If lRow("IdProveedor") IsNot DBNull.Value AndAlso lRow("IdProveedor") IsNot Nothing Then
                                BeProductoCodigosBarra.Proveedor = New clsBeProveedor
                                BeProductoCodigosBarra.Proveedor.Nombre = CType(lRow("Proveedor"), String)
                            End If

                            If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                BeProductoCodigosBarra.Activo = CType(lRow("activo"), Boolean)
                            End If

                            If lRow("user_agr") IsNot DBNull.Value AndAlso lRow("user_agr") IsNot Nothing Then
                                BeProductoCodigosBarra.User_agr = CType(lRow("user_agr"), String)
                            End If

                            If lRow("fec_agr") IsNot DBNull.Value AndAlso lRow("fec_agr") IsNot Nothing Then
                                BeProductoCodigosBarra.Fec_agr = CType(lRow("fec_agr"), DateTime)
                            End If

                            If lRow("user_mod") IsNot DBNull.Value AndAlso lRow("user_mod") IsNot Nothing Then
                                BeProductoCodigosBarra.User_mod = CType(lRow("user_mod"), String)
                            End If

                            If lRow("fec_mod") IsNot DBNull.Value AndAlso lRow("fec_mod") IsNot Nothing Then
                                BeProductoCodigosBarra.Fec_mod = CType(lRow("fec_mod"), DateTime)
                            End If

                            If lRow("IdTalla") IsNot DBNull.Value AndAlso lRow("IdTalla") IsNot Nothing Then
                                BeProductoCodigosBarra.IdTalla = CType(lRow("IdTalla"), Int32)
                            End If

                            If lRow("IdColor") IsNot DBNull.Value AndAlso lRow("IdColor") IsNot Nothing Then
                                BeProductoCodigosBarra.IdColor = CType(lRow("IdColor"), Int32)
                            End If

                            lReturnList.Add(BeProductoCodigosBarra)

                        Next

                        lCodigosBarraInMemory.AddRange(lReturnList)

                        Return lReturnList

                    End If

                End Using

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function ExisteCodigoBarra(ByVal pIdProducto As Integer,
                                             ByVal pIdProveedor As Integer,
                                             ByVal pCodigoBarra As String) As Boolean

        Try

            Dim sp As String = String.Format("SELECT TOP 1 * FROM producto_codigos_barra WHERE IdProducto={0} AND IdProveedor={1} AND codigo_barra='{2}'", pIdProducto, pIdProveedor, pCodigoBarra)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ExisteCodigoBarra(ByVal pCodigoBarra As String) As Boolean

        Try

            Dim sp As String = String.Format("SELECT TOP 1 * FROM producto_codigos_barra WHERE codigo_barra='{0}'", pCodigoBarra)
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection)
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeProducto_codigos_barra As clsBeProducto_codigos_barra, ByVal pCodigoBarraAnterior As String) As Integer

        Try

            Upd.Init("producto_codigos_barra")
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Where(String.Format("IdProducto = @IdProducto AND IdProveedor = @IdProveedor AND codigo_barra ='{0}'", pCodigoBarraAnterior))

            Dim sp As String = Upd.SQL()

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection)

            Dim rowsAffected As Integer = 0
            lConnection.Open()

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_codigos_barra.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProducto_codigos_barra.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeProducto_codigos_barra.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_codigos_barra.Activo))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_codigos_barra.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_codigos_barra.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_codigos_barra.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_codigos_barra.User_agr))

            rowsAffected = cmd.ExecuteNonQuery()

            lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()
            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Desactivar el codigo de barra
    ''' </summary>
    ''' <param name="pIdProducto"></param>
    ''' <param name="pIdProveedor"></param>
    ''' <param name="pCodigoBarra"></param>
    ''' <remarks>Erik Calderón</remarks>
    Public Shared Sub Desactivar(ByVal pIdProducto As Integer, ByVal pIdProveedor As Integer, ByVal pCodigoBarra As String)

        Try

            Dim sp As String = String.Format("UPDATE producto_codigos_barra SET Activo=0 WHERE IdProducto={0} AND IdProveedor={1} AND codigo_barra='{2}'", pIdProducto, pIdProveedor, pCodigoBarra)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.ExecuteNonQuery()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdProductoCodigoBarra),0) FROM Producto_codigos_barra"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdProductoCodigoBarra),0) FROM producto_codigos_barra"

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

    Public Shared Function Existe_Codigo_Barra(ByVal pIdProducto As Integer,
                                               ByVal pIdProveedor As Integer,
                                               ByVal pCodigoBarra As String,
                                               ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction) As Boolean

        Existe_Codigo_Barra = False

        Try

            Dim sp As String = String.Format("SELECT TOP 1 * FROM producto_codigos_barra 
                                              WHERE IdProducto={0} AND IdProveedor={1} AND 
                                              codigo_barra='{2}'", pIdProducto, pIdProveedor, pCodigoBarra)
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Existe_Codigo_Barra = (dt.Rows.Count > 0)

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function Delete_All() As Integer

        Dim cmd As New SqlCommand()
        Dim rowsAffected As Integer = 0

        Try

            Dim vSQL As String = "DELETE FROM producto_codigos_barra  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    cmd.CommandType = CommandType.Text
                    cmd = New SqlCommand(vSQL, lConnection, lTransaction)

                    rowsAffected = cmd.ExecuteNonQuery()

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

        Return rowsAffected

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
