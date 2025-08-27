Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProducto_tipo
    Implements IDisposable

    Public Shared lTipoProductoInMemory As New List(Of clsBeProducto_tipo)

    Public Shared Function Listar(ByVal pActivo As Boolean) As DataTable

        Try

            Dim sp As String = "SELECT e.IdTipoProducto,p.IdPropietario,p.nombre_comercial AS Propietario, e.NombreTipoProducto AS Tipo " _
                               & "FROM producto_tipo AS e " _
                               & "INNER JOIN propietarios AS p ON e.IdPropietario = p.IdPropietario WHERE 1 > 0 "

            If pActivo Then
                sp += " AND e.Activo=1"
            Else
                sp += " AND e.Activo=0"
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
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

    Public Shared Function GetAllFiltro(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProducto_tipo)

        Dim lReturnList As New List(Of clsBeProducto_tipo)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = ""

                If pIdPropietario = 0 Then
                    vSQL = "SELECT * FROM VW_ProductoTipo WHERE 1 > 0 "
                Else
                    vSQL = String.Format("SELECT * FROM VW_ProductoTipo WHERE IdPropietario={0} ", pIdPropietario)
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

                    Dim Obj As clsBeProducto_tipo

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_tipo

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

    Public Shared Function Get_All_For_Seleccion() As List(Of clsBeProducto_tipo_seleccion)

        Dim lReturnList As New List(Of clsBeProducto_tipo_seleccion)

        Try

            Dim vSQL As String = "SELECT producto_tipo.IdTipoProducto AS id, 
                                  producto_tipo.NombreTipoProducto AS nombre, 
                                  propietarios.nombre_comercial AS propietario 
                                  From producto_tipo INNER Join propietarios ON 
                                  producto_tipo.IdPropietario = propietarios.IdPropietario  
                                  ORDER BY nombre "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_tipo_seleccion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_tipo_seleccion
                                Obj.Seleccion = False
                                Obj.idTipoProducto = lRow("id")
                                Obj.Nombre = lRow("Nombre")
                                Obj.Propietario = lRow("propietario")
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

    Public Shared Function GetSingle(ByVal pIdTipoProducto As Integer) As clsBeProducto_tipo

        GetSingle = Nothing

        Try

            Dim ObjPT As New clsBeProducto_tipo()
            Dim IdxTipoProducto As Integer = -1
            Dim vIdTipo As Integer = pIdTipoProducto
            IdxTipoProducto = lTipoProductoInMemory.FindIndex(Function(x) x.IdTipoProducto = vIdTipo)

            If IdxTipoProducto = -1 Then


                Dim vSQL As String = "SELECT TOP 1 * FROM producto_tipo WHERE IdTipoProducto=@IdTipoProducto"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", pIdTipoProducto)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Cargar(ObjPT, lRow)
                            lTipoProductoInMemory.Add(ObjPT.Clone())
                            Return ObjPT

                        End If

                    End Using

                End Using

            Else
                ObjPT = New clsBeProducto_tipo()
                ObjPT = lTipoProductoInMemory(IdxTipoProducto).Clone()
                Return ObjPT
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdTipoProducto As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeProducto_tipo

        GetSingle = Nothing

        Try

            Dim ObjPT As New clsBeProducto_tipo()
            Dim IdxTipoProducto As Integer = -1
            Dim vIdTipo As Integer = pIdTipoProducto
            IdxTipoProducto = lTipoProductoInMemory.FindIndex(Function(x) x.IdTipoProducto = vIdTipo)

            If IdxTipoProducto = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM producto_tipo WHERE IdTipoProducto=@IdTipoProducto"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.Transaction = lTransaction

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", pIdTipoProducto)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Cargar(ObjPT, lRow)
                        lTipoProductoInMemory.Add(ObjPT.Clone())
                        Return ObjPT

                    End If

                End Using

            Else
                ObjPT = New clsBeProducto_tipo()
                ObjPT = lTipoProductoInMemory(IdxTipoProducto).Clone()
                Return ObjPT
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdTipoProducto As Integer, ByVal pIdPropietario As Integer) As clsBeProducto_tipo

        GetSingle = Nothing

        Try

            Dim ObjPT As New clsBeProducto_tipo()
            Dim IdxTipoProducto As Integer = -1
            Dim vIdTipo As Integer = pIdTipoProducto
            IdxTipoProducto = lTipoProductoInMemory.FindIndex(Function(x) x.IdTipoProducto = vIdTipo AndAlso x.IdPropietario = pIdPropietario)

            If IdxTipoProducto = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM producto_tipo WHERE IdTipoProducto=@IdTipoProducto AND IdPropietario=@IdPropietario"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", pIdTipoProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            ObjPT = New clsBeProducto_tipo()
                            Cargar(ObjPT, lRow)
                            lTipoProductoInMemory.Add(ObjPT.Clone())
                            Return ObjPT

                        End If

                    End Using

                End Using

            Else
                ObjPT = New clsBeProducto_tipo()
                ObjPT = lTipoProductoInMemory(IdxTipoProducto).Clone()
                Return ObjPT
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdTipoProducto As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto_tipo WHERE IdTipoProducto=@IdTipoProducto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdTipoProducto", pIdTipoProducto)

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

    Public Shared Function Exists_By_Codigo(ByVal pCodigo As String) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto_tipo WHERE codigo=@codigo"

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

    Public Shared Function Exists(ByVal pIdTipoProducto As Integer, ByVal pConection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Const Sp As String = "SELECT COUNT(1) FROM producto_tipo WHERE IdTipoProducto=@IdTipoProducto"

            Using lCommand As New SqlCommand(Sp, pConection, pTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdTipoProducto", pIdTipoProducto)

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

            Const Sp As String = "SELECT COUNT(1) 
                                  FROM producto_tipo 
                                  WHERE NombreTipoProducto=@Nombre"

            Using lCommand As New SqlCommand(Sp, pConection, pTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@Nombre", pNombre)

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
                                            ByVal pTransaction As SqlTransaction,
                                            ByRef IdTipoProducto As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Const Sp As String = "SELECT IdTipoProducto
                                  FROM producto_tipo 
                                  WHERE NombreTipoProducto=@Nombre"

            Using lCommand As New SqlCommand(Sp, pConection, pTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@Nombre", pNombre)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                    IdTipoProducto = lReturnValue
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw New Exception(String.Format("{0}_TipoProducto {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function Exists_By_IdTipoProducto(ByVal pIdTipoProducto As String,
                                                    ByVal pConection As SqlConnection,
                                                    ByVal pTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Const Sp As String = "SELECT COUNT(1) 
                                  FROM producto_tipo 
                                  WHERE IdTipoProducto=@IdTipoProducto"

            Using lCommand As New SqlCommand(Sp, pConection, pTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdTipoProducto", pIdTipoProducto)

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

    Public Shared Sub Delete(ByVal pIdTipoProducto As Integer)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(String.Format("DELETE FROM producto_tipo WHERE IdTipoProducto={0}", pIdTipoProducto), lConnection)

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

    Public Shared Function ExisteProductoLigado(ByVal pIdTipoProducto As Integer) As Boolean

        ExisteProductoLigado = False

        Try

            Dim vSQL As String = "SELECT COUNT(1) FROM producto WHERE IdTipoProducto=@IdTipoProducto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdTipoProducto", pIdTipoProducto)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            ExisteProductoLigado = CInt(lReturnValue) > 0
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

    Public Shared Function MAXIdTipoProducto() As Integer

        Try

            Dim vSQL As String = "SELECT MAX(IdTipoProducto) + 1 as nuevo FROM producto_tipo"

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
                MAXIdTipoProducto = IIf(IsDBNull(dt.Rows(0).Item("nuevo")), "1", dt.Rows(0).Item("nuevo"))
            Else
                MAXIdTipoProducto = 1
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function TipoProductoByIdProductoBodega(ByVal IdProductoBodega As Integer) As String

        TipoProductoByIdProductoBodega = ""

        Try

            Dim vSQL As String = "SELECT producto_tipo.IdTipoProducto, producto_tipo.NombreTipoProducto
                       FROM producto_bodega INNER JOIN
                            producto ON producto_bodega.IdProducto = producto.IdProducto INNER JOIN
                            producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto
                      WHERE producto_bodega.IdProductoBodega=@IdProductoBodega"

            Dim sp As String = vSQL
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count > 0 Then
                TipoProductoByIdProductoBodega = dt.Rows(0).Item("NombreTipoProducto")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_TipoProducto_By_IdProductoBodega(ByVal IdProductoBodega As Integer,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As String

        Get_TipoProducto_By_IdProductoBodega = ""

        Try

            Dim vSQL As String = "SELECT producto_tipo.IdTipoProducto, producto_tipo.NombreTipoProducto
                       FROM producto_bodega INNER JOIN
                            producto ON producto_bodega.IdProducto = producto.IdProducto INNER JOIN
                            producto_tipo ON producto.IdTipoProducto = producto_tipo.IdTipoProducto
                      WHERE producto_bodega.IdProductoBodega=@IdProductoBodega"

            Dim sp As String = vSQL
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Get_TipoProducto_By_IdProductoBodega = dt.Rows(0).Item("NombreTipoProducto")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(MAX(IdTipoProducto),0) + 1 as nuevo FROM producto_tipo"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction)
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

    Public Shared Function Obtener(ByRef oBeProducto_tipo As clsBeProducto_tipo,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try



            Dim IdxTipoProducto As Integer = -1
            Dim vIdTipo As Integer = oBeProducto_tipo.IdTipoProducto
            IdxTipoProducto = lTipoProductoInMemory.FindIndex(Function(x) x.IdTipoProducto = vIdTipo)

            If IdxTipoProducto = -1 Then

                Dim sp As String = "SELECT * FROM Producto_tipo 
                                     Where(IdTipoProducto = @IdTipoProducto)"

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTO", oBeProducto_tipo.IdTipoProducto))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    oBeProducto_tipo = New clsBeProducto_tipo()
                    Cargar(oBeProducto_tipo, dt.Rows(0))
                    lTipoProductoInMemory.Add(oBeProducto_tipo.Clone())
                    Obtener = True
                End If

            Else
                oBeProducto_tipo = New clsBeProducto_tipo()
                oBeProducto_tipo = lTipoProductoInMemory(IdxTipoProducto).Clone()
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario(ByVal pActivo As Boolean,
                                                    ByVal pIdPropietario As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As DataTable
        Dim lDataTable As New DataTable("Tipo")

        Try

            Dim vSQL As String = ""

            If pIdPropietario = 0 Then
                vSQL = "SELECT IdTipoProducto, NombreTipoProducto  FROM VW_ProductoTipo WHERE 1 > 0 "
            Else
                vSQL = String.Format("SELECT IdTipoProducto, NombreTipoProducto FROM VW_ProductoTipo WHERE IdPropietario={0}", pIdPropietario)
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

        Dim lDataTable As New DataTable("Tipo")
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = ""

            If pIdPropietario = 0 Then
                vSQL = "SELECT IdTipoProducto, NombreTipoProducto  FROM VW_ProductoTipo WHERE 1 > 0 AND ACTIVO=1"
            Else
                vSQL = String.Format("SELECT IdTipoProducto, NombreTipoProducto  FROM VW_ProductoTipo WHERE ACTIVO=1 AND IdPropietario={0}", pIdPropietario)
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

    Public Shared Function Get_Single_By_Nombre(ByVal pNombre As String, pConnection As SqlConnection, pTransaction As SqlTransaction) As clsBeProducto_tipo

        Get_Single_By_Nombre = Nothing

        Try

            Dim IdxProductoClasificacion As Integer = 0

            Dim vSQL As String = "SELECT TOP 1 * FROM producto_tipo WHERE NombreTipoProducto like '%" & pNombre & "%'"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeProducto_tipo()
                    Cargar(Obj, lRow)
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Seleccion_By_Filtros(ByVal lPropietarios As List(Of Integer),
                                                            ByVal lTipoRotacion As List(Of Integer),
                                                            ByVal lIndiceRotacion As List(Of Integer)) As List(Of clsBeProducto_tipo_seleccion)

        Dim lReturnList As New List(Of clsBeProducto_tipo_seleccion)

        Try

            ' Crear los filtros de la consulta en base a las listas recibidas
            Dim propietarioFilter As String = IIf(lPropietarios IsNot Nothing, String.Join(",", lPropietarios), "")
            Dim tipoRotacionFilter As String = IIf(lTipoRotacion IsNot Nothing, String.Join(",", lTipoRotacion), "")
            Dim indiceRotacionFilter As String = IIf(lIndiceRotacion IsNot Nothing, String.Join(",", lIndiceRotacion), "")

            ' Construir la consulta SQL con los filtros
            Dim vSQL As String = $"SELECT DISTINCT producto_tipo.IdTipoProducto AS id, producto_tipo.NombreTipoProducto AS nombre, 
                              propietarios.nombre_comercial AS propietario, producto.IdTipoRotacion, producto.IdIndiceRotacion, 
                              producto_tipo.IdPropietario
                              FROM producto_tipo
                              INNER JOIN propietarios ON producto_tipo.IdPropietario = propietarios.IdPropietario
                              INNER JOIN propietario_bodega ON propietarios.IdPropietario = propietario_bodega.IdPropietario
                              INNER JOIN producto ON producto_tipo.IdTipoProducto = producto.IdTipoProducto
                              WHERE 0 = 0 "

            If propietarioFilter <> "" Then
                vSQL += $" AND propietario_bodega.IdPropietarioBodega IN ({propietarioFilter})"
            End If

            If tipoRotacionFilter <> "" Then
                vSQL += $" AND producto.IdTipoRotacion IN ({tipoRotacionFilter})"
            End If

            If indiceRotacionFilter <> "" Then
                vSQL += $" AND producto.IdIndiceRotacion IN ({indiceRotacionFilter})"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_tipo_seleccion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_tipo_seleccion
                                Obj.Seleccion = False
                                Obj.idTipoProducto = lRow("id")
                                Obj.Nombre = lRow("Nombre")
                                Obj.Propietario = lRow("propietario")
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
