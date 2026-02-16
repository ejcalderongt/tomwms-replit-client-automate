Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProducto_clasificacion
    Implements IDisposable

    Public Shared Function GetSingle(ByVal pIdClasificacion As Integer) As clsBeProducto_clasificacion

        GetSingle = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            IdxProductoClasificacion = lProductoClasificacionInMemory.FindIndex(Function(x) x.IdClasificacion = pIdClasificacion)

            If IdxProductoClasificacion = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM producto_clasificacion WHERE IdClasificacion=@IdClasificacion"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    lConnection.Open()

                    Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                        Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                            lDTA.SelectCommand.CommandType = CommandType.Text
                            lDTA.SelectCommand.Transaction = lTransaction
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdClasificacion", pIdClasificacion)

                            Dim lDT As New DataTable()
                            lDTA.Fill(lDT)

                            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                                Dim lRow As DataRow = lDT.Rows(0)
                                Dim Obj As New clsBeProducto_clasificacion()
                                Cargar(Obj, lRow)
                                lProductoClasificacionInMemory.Add(Obj.Clone())
                                Return Obj

                            End If

                        End Using

                        lTransaction.Commit()

                    End Using

                    lConnection.Close()

                End Using

            Else

                Return lProductoClasificacionInMemory(IdxProductoClasificacion)

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdClasificacion As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeProducto_clasificacion

        GetSingle = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            IdxProductoClasificacion = lProductoClasificacionInMemory.FindIndex(Function(x) x.IdClasificacion = pIdClasificacion)

            If IdxProductoClasificacion = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM producto_clasificacion WHERE IdClasificacion=@IdClasificacion"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Transaction = lTransaction
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdClasificacion", pIdClasificacion)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeProducto_clasificacion()
                        Cargar(Obj, lRow)
                        lProductoClasificacionInMemory.Add(Obj.Clone())
                        Return Obj

                    End If

                End Using

            Else

                Return lProductoClasificacionInMemory(IdxProductoClasificacion)

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdClasificacion As Integer,
                                     ByVal pIdPropietario As Integer) As clsBeProducto_clasificacion

        GetSingle = Nothing

        Try

            Dim Obj As New clsBeProducto_clasificacion()
            Dim IdxProductoClasificacion As Integer = 0

            IdxProductoClasificacion = lProductoClasificacionInMemory.FindIndex(Function(x) x.IdClasificacion = pIdClasificacion AndAlso x.Propietario.IdPropietario = pIdPropietario)

            If IdxProductoClasificacion = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM producto_clasificacion WHERE IdClasificacion=@IdClasificacion AND IdPropietario=@IdPropietario"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    lConnection.Open()

                    Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                        Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                            lDTA.SelectCommand.CommandType = CommandType.Text
                            lDTA.SelectCommand.Transaction = lTransaction
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdClasificacion", pIdClasificacion)
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                            Dim lDT As New DataTable()
                            lDTA.Fill(lDT)

                            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                                Dim lRow As DataRow = lDT.Rows(0)
                                Obj = New clsBeProducto_clasificacion()
                                Cargar(Obj, lRow)
                                lProductoClasificacionInMemory.Add(Obj.Clone())
                                Return Obj

                            End If

                        End Using

                        lTransaction.Commit()

                    End Using

                    lConnection.Close()

                End Using

            Else
                Obj = New clsBeProducto_clasificacion()
                Obj = lProductoClasificacionInMemory(IdxProductoClasificacion).Clone()
                Return Obj
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProducto_clasificacion)

        Dim lReturnList As New List(Of clsBeProducto_clasificacion)
        Dim vSQL As String = String.Empty

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    If pIdPropietario = 0 Then
                        vSQL = "SELECT * FROM VW_ProductoClasificacion WHERE 1 > 0 "
                    Else
                        vSQL = "SELECT * FROM VW_ProductoClasificacion WHERE IdPropietario=@IdPropietario"
                    End If

                    If pActivo = True Then
                        vSQL += " AND Activo=1"
                    Else
                        vSQL += " AND Activo=0"
                    End If


                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_clasificacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_clasificacion
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

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdClasificacion As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto_clasificacion WHERE IdClasificacion=@IdClasificacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)
                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@IdClasificacion", pIdClasificacion)
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

    Public Shared Function Exist_By_Codigo(ByVal pCodigo As String) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto_clasificacion WHERE Codigo=@Codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Transaction = lTransaction
                        lCommand.Parameters.AddWithValue("@Codigo", pCodigo)

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

    Public Shared Function Exists(ByVal pIdIdClasificacion As Integer,
                                  ByVal pConection As SqlConnection,
                                  ByVal pTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto_clasificacion WHERE IdClasificacion=@IdClasificacion"

            Using lCommand As New SqlCommand(vSQL, pConection, pTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdClasificacion", pIdIdClasificacion)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw New Exception(String.Format("{0}_TipoProducto {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function Exists_By_Nombre(ByVal pNombre As String,
                                           ByVal pConection As SqlConnection,
                                           ByVal pTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM 
                    producto_clasificacion 
                    WHERE nombre=@nombre"

            Using lCommand As New SqlCommand(vSQL, pConection, pTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@nombre", pNombre)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw New Exception(String.Format("{0}_TipoProducto {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function Get_IdClasificacion_By_Codigo(ByVal pCodigo As String,
                                                         ByVal pConection As SqlConnection,
                                                         ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lClasificacion As Integer = 0

            Dim vSQL As String = "SELECT IdClasificacion FROM producto_clasificacion WHERE codigo=@codigo"

            Using lCommand As New SqlCommand(vSQL, pConection, pTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@codigo", pCodigo)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lClasificacion = CInt(lReturnValue)
                End If

            End Using

            Return lClasificacion

        Catch ex As Exception
            Throw New Exception(String.Format("{0}_TipoProducto {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function ExisteProductoLigado(ByVal pIdClasificacion As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto WHERE IdClasificacion=@IdClasificacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection)

                        lCommand.Transaction = lTransaction
                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdClasificacion", pIdClasificacion)

                        lConnection.Open()

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        lConnection.Close()

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
    Public Shared Sub Delete(ByVal pIdClasificacion As Integer)

        Try

            Dim sp As String = String.Format("DELETE FROM producto_clasificacion WHERE IdClasificacion={0}", pIdClasificacion)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Transaction = lTransaction
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
    Public Shared Sub DeleteByPropietario(ByVal pIdPropietario As Integer)

        Try

            Dim sp As String = String.Format("DELETE FROM producto_clasificacion WHERE IdPropietario={0}", pIdPropietario)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Transaction = lTransaction
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
    Public Shared Function Max_IdProducto_Clasificacion() As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim vSQL As String = "SELECT  MAX(IdClasificacion) + 1 as nuevo FROM producto_clasificacion"
            Dim sp As String = vSQL

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count > 0 Then
                Max_IdProducto_Clasificacion = IIf(IsDBNull(dt.Rows(0).Item("nuevo")), "1", dt.Rows(0).Item("nuevo"))
            Else
                Max_IdProducto_Clasificacion = 1
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function
    Public Shared Function MaxId(ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Dim lMax As Integer = 0

        Try

            Dim vSQL As String = "SELECT ISNULL(MAX(IdClasificacion),0) + 1 as nuevo FROM producto_clasificacion"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)
                lCommand.CommandType = CommandType.Text
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
    Public Shared Sub GuardarTransaccion(ByVal pListObjPC As List(Of clsBeProducto_clasificacion))


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            For Each Obj As clsBeProducto_clasificacion In pListObjPC
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
    Public Shared Function Obtener(ByRef oBeProducto_clasificacion As clsBeProducto_clasificacion,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Dim Obj As New clsBeProducto_clasificacion()
            Dim IdxProductoClasificacion As Integer = oBeProducto_clasificacion.IdClasificacion

            IdxProductoClasificacion = lProductoClasificacionInMemory.FindIndex(Function(x) x.IdClasificacion = IdxProductoClasificacion)

            If IdxProductoClasificacion = -1 Then

                Dim sp As String = "SELECT * FROM Producto_clasificacion
                                    Where(IdClasificacion = @IdClasificacion)"

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeProducto_clasificacion.IdClasificacion))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    Obj = New clsBeProducto_clasificacion()
                    Cargar(Obj, dt.Rows(0))
                    lProductoClasificacionInMemory.Add(Obj.Clone())
                    oBeProducto_clasificacion = Obj.Clone()
                End If

            Else
                Obj = New clsBeProducto_clasificacion()
                Obj = lProductoClasificacionInMemory(IdxProductoClasificacion).Clone()
                oBeProducto_clasificacion = Obj
                Obtener = True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario(ByVal pActivo As Boolean,
                                                    ByVal pIdPropietario As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As DataTable

        Dim lDataTable As New DataTable("Clasificacion")

        Try

            Dim vSQL As String = ""

            If pIdPropietario = 0 Then
                vSQL = "SELECT IdClasificacion, Nombre  FROM VW_ProductoClasificacion WHERE 1 > 0 "
            Else
                vSQL = String.Format("SELECT IdClasificacion, Nombre FROM VW_ProductoClasificacion WHERE IdPropietario={0}", pIdPropietario)
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

        Dim lDataTable As New DataTable("Clasificacion")
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = ""

            If pIdPropietario = 0 Then
                vSQL = "SELECT IdClasificacion, Nombre  FROM VW_ProductoClasificacion WHERE 1 > 0 AND ACTIVO=1"
            Else
                vSQL = String.Format("SELECT IdClasificacion, Nombre FROM VW_ProductoClasificacion WHERE ACTIVO=1 AND IdPropietario={0}", pIdPropietario)
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
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function
    Public Shared Function Get_Single_By_Nombre(ByVal pNombre As String) As clsBeProducto_clasificacion

        Get_Single_By_Nombre = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_clasificacion WHERE Nombre like '% " + pNombre + " %'"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeProducto_clasificacion()
                            Cargar(Obj, lRow)
                            Return Obj

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

    Public Shared Function Get_Single_By_Nombre(ByVal pNombre As String, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As clsBeProducto_clasificacion

        Get_Single_By_Nombre = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_clasificacion WHERE Nombre like '%" + pNombre + "%'"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeProducto_clasificacion()
                    Cargar(Obj, lRow)
                    Get_Single_By_Nombre = Obj
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_Nombre_By_Propietario(ByVal pNombre As String, IdPropietario As Integer, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As clsBeProducto_clasificacion

        Get_Single_By_Nombre_By_Propietario = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_clasificacion WHERE Nombre = '" + pNombre + "' AND IdPropietario = " & IdPropietario & ""

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeProducto_clasificacion()
                    Cargar(Obj, lRow)
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean,
                                          ByVal pIdPropietario As Integer,
                                          ByVal lConnection As SqlConnection,
                                          ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto_clasificacion)

        Dim lReturnList As New List(Of clsBeProducto_clasificacion)
        Dim vSQL As String = String.Empty

        Try

            If pIdPropietario = 0 Then
                vSQL = "SELECT * FROM VW_ProductoClasificacion WHERE 1 > 0 "
            Else
                vSQL = "SELECT * FROM VW_ProductoClasificacion WHERE IdPropietario=@IdPropietario"
            End If

            If pActivo = True Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_clasificacion

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_clasificacion
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

    Public Shared Function Get_Single_By_Codigo(ByVal pCodigo As String, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As clsBeProducto_clasificacion

        Get_Single_By_Codigo = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_clasificacion WHERE Codigo =  '" + pCodigo + "'"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeProductoClasificacion As New clsBeProducto_clasificacion()
                    Cargar(BeProductoClasificacion, lRow)
                    Get_Single_By_Codigo = BeProductoClasificacion
                    Return BeProductoClasificacion

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Exists_By_IdClasificacion_By_IdPropietario(ByVal pIdIdClasificacion As Integer,
                                                                      ByVal pIdPropietario As Integer,
                                                                      ByVal pConection As SqlConnection,
                                                                      ByVal pTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto_clasificacion 
                                  WHERE IdClasificacion=@IdClasificacion and IdPropietario=@IdPropietario"

            Using lCommand As New SqlCommand(vSQL, pConection, pTransaction)
                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdClasificacion", pIdIdClasificacion)
                lCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw New Exception(String.Format("{0}_TipoProducto {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    ' Get_Single_By_Codigo sin recibir conexión/tx como parámetro.
    ' - Abre y cierra su propia conexión.
    ' - NO usa transacción explícita.
    ' - Corrige SQL Injection usando parámetro (tu versión concatenaba el código).
    Public Shared Function Get_Single_By_Codigo(ByVal pCodigo As String) As clsBeProducto_clasificacion
        Dim cn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

        Try
            cn.Open()

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_clasificacion WHERE Codigo = @Codigo;"

            Using cmd As New SqlCommand(vSQL, cn)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 60
                cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = pCodigo

                Using dad As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    dad.Fill(dt)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim row As DataRow = dt.Rows(0)
                        Dim be As New clsBeProducto_clasificacion()
                        Cargar(be, row)
                        Return be
                    End If
                End Using
            End Using

            Return Nothing

        Catch ex As Exception
            Throw
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Function

    ' MaxId sin recibir conexión ni transacción como parámetro.
    ' - Abre y cierra su propia conexión.
    ' - NO usa transacción explícita (autocommit).
    ' - Usa la cadena desde Configuration.ConfigurationManager.AppSettings("CST")
    Public Shared Function MaxId() As Integer
        Dim lMax As Integer = 0
        Dim cn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

        Try
            cn.Open()

            Dim vSQL As String = "SELECT ISNULL(MAX(IdClasificacion), 0) + 1 FROM producto_clasificacion;"

            Using cmd As New SqlCommand(vSQL, cn)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 60

                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                    lMax = CInt(result)
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Throw
        Finally
            If cn IsNot Nothing AndAlso cn.State = ConnectionState.Open Then cn.Close()
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
