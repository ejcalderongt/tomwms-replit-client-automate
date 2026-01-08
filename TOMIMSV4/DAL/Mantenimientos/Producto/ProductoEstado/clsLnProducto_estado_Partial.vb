Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProducto_estado
    Implements IDisposable

    Public Shared Function Listar(ByVal pActivo As Boolean) As DataTable

        Try

            Dim sp As String = "SELECT e.IdEstado,p.IdPropietario,p.nombre_comercial AS Propietario, e.nombre AS Estado, e.IdUbicacionDefecto AS 'Ubicación Defecto' " _
                               & "FROM producto_estado AS e " _
                               & "INNER JOIN propietarios AS p ON e.IdPropietario = p.IdPropietario WHERE 1 > 0 "

            If pActivo Then
                sp += " AND e.Activo=1"
            Else
                sp += " AND e.Activo=0"
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

    '#CM20172310_0434PM: Corrección de String.Format en Producto_estado
    Public Shared Function Get_All_For_Seleccion() As List(Of clsBeProducto_estado_selection)

        Dim lReturnList As New List(Of clsBeProducto_estado_selection)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM Producto_Estado"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_estado_selection

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_estado_selection
                                Obj.idEstado = lRow("IdEstado")
                                Obj.Seleccion = False
                                Obj.Propietario = New clsBePropietarios
                                Obj.Propietario.IdPropietario = lRow("IdPropietario")
                                Obj.Propietario = clsLnPropietarios.GetSingle(lRow("IdPropietario"), lConnection, lTransaction)
                                Obj.Nombre = IIf(IsDBNull(lRow("Nombre")), "", lRow("Nombre"))

                                lReturnList.Add(Obj)

                            Next

                        End If

                        lDataTable.Dispose()
                        lDTA.Dispose()

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()
                lConnection.Dispose()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean, Optional ByVal pIdPropietario As Integer = 0) As List(Of clsBeProducto_estado)

        Dim lReturnList As New List(Of clsBeProducto_estado)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM VW_ProductoEstado WHERE 1 > 0 "

                If pActivo Then
                    vSQL += " AND Activo=1"
                Else
                    vSQL += " AND Activo=0"
                End If

                If pIdPropietario > 0 Then
                    vSQL += " AND IdPropietario= @pIdPropietario"
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    If pIdPropietario > 0 Then
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdPropietario", pIdPropietario)
                    End If

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_estado

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_estado
                            Cargar(Obj, lRow)

                            Obj.IdEstado = CType(lRow("IdEstado"), Int32)

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

    Public Shared Function GetSingle(ByVal pIdEstado As Integer) As clsBeProducto_estado

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT p.*,bu.descripcion 
                    FROM producto_estado AS p LEFT OUTER JOIN  
                    bodega_ubicacion AS bu 
                    ON p.IdUbicacionDefecto = bu.IdUbicacion 
                    WHERE p.IdEstado=@IdEstado"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim ObjPE As New clsBeProducto_estado()

                        Cargar(ObjPE, lRow)

                        Return ObjPE

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#EJC20180113: Agregué transaccionalidad en GetSingle de Producto_Estado_Partial
    Public Shared Function GetSingle(ByVal pIdEstado As Integer,
                                    ByRef lConnection As SqlConnection,
                                    ByRef lTransaction As SqlTransaction) As clsBeProducto_estado

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 p.*,bu.descripcion FROM producto_estado AS p 
                    LEFT JOIN bodega_ubicacion AS bu 
                    ON p.IdUbicacionDefecto = bu.IdUbicacion 
                    WHERE p.IdEstado=@IdEstado"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjPE As New clsBeProducto_estado()

                    Cargar(ObjPE, lRow)
                    Return ObjPE

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CM20172310_0434PM: Corrección de String.Format en Producto_estado
    Public Shared Function GetAllByPropietario(ByVal pIdPropietario As Integer) As List(Of clsBeProducto_estado)

        Dim lReturnList As New List(Of clsBeProducto_estado)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM Producto_Estado WHERE IdPropietario=@IdPropietario"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_estado

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_estado
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

    '#CM20172310_0434PM: Corrección de String.Format en Producto_estado
    Public Shared Function Get_All_By_IdPropietarioBodega(ByVal pIdPropietario As Integer, ByVal pIdBodega As Integer) As List(Of clsBeProducto_estado)

        Dim lReturnList As New List(Of clsBeProducto_estado)

        Try


            Dim vSQL As String = "SELECT pe.IdEstado, 
                                      pe.IdPropietario, 
                                      pe.nombre, 
                                      pe.IdUbicacionDefecto, 
                                      pe.utilizable, 
                                      pe.activo, 
                                      pe.user_agr, 
                                      pe.fec_agr, 
                                      pe.user_mod, 
                                      pe.fec_mod, 
                                      pe.dañado, 
                                      producto_estado_ubic.IdBodega,
                                      pe.sistema
                                      FROM producto_estado AS pe INNER JOIN
                                      producto_estado_ubic ON pe.IdEstado = dbo.producto_estado_ubic.IdEstado
                                      WHERE pe.activo = 1 And pe.IdPropietario = @IdPropietario 
                                      And producto_estado_ubic.IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_estado

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeProducto_estado()
                                Cargar(Obj, lRow)
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

    Public Shared Function Get_All_Stock_Con_Estado_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_estado)

        Dim lReturnList As New List(Of clsBeProducto_estado)

        Try


            Dim vSQL As String = "SELECT distinct * from VW_StockEstadosProducto 
                                      WHERE IdProducto = @IdProducto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))


                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProducto_estado

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProducto_estado

                                Obj.IdEstado = CType(lRow("IdProductoEstado"), Int32)

                                If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                    Obj.IdPropietario = CType(lRow("IdPropietario"), Int32)
                                End If

                                If lRow("nombre") IsNot DBNull.Value AndAlso lRow("nombre") IsNot Nothing Then
                                    Obj.Nombre = CType(lRow("nombre"), String)
                                End If

                                If lRow("IdUbicacionDefecto") IsNot DBNull.Value AndAlso lRow("IdUbicacionDefecto") IsNot Nothing Then
                                    Obj.IdUbicacionDefecto = CType(lRow("IdUbicacionDefecto"), Int32)
                                End If

                                If lRow("Utilizable") IsNot DBNull.Value AndAlso lRow("Utilizable") IsNot Nothing Then
                                    Obj.Utilizable = CType(lRow("Utilizable"), Boolean)
                                End If

                                If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                    Obj.Activo = CType(lRow("activo"), Boolean)
                                End If

                                If lRow("Dañado") IsNot DBNull.Value AndAlso lRow("Dañado") IsNot Nothing Then
                                    Obj.Dañado = CType(lRow("Dañado"), Boolean)
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

    Public Shared Function Get_All_Stock_Con_Estado_By_IdProductoBodega(ByVal pIdProductoBodega As Integer, Optional ByVal pIdProductoEstado As Integer = 0) As List(Of clsBeProducto_estado)

        Dim lReturnList As New List(Of clsBeProducto_estado)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT distinct * from VW_StockEstadosProducto " &
                                                   "WHERE IdProductoBodega = @IdProductoBodega"

                If pIdProductoEstado > 0 Then
                    vSQL += " AND IdProductoEstado=@pIdProductoEstado"
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                    If pIdProductoEstado > 0 Then
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdProductoEstado", pIdProductoEstado)
                    End If

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_estado

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_estado

                            Obj.IdEstado = CType(lRow("IdProductoEstado"), Int32)

                            If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                Obj.IdPropietario = CType(lRow("IdPropietario"), Int32)
                            End If

                            If lRow("nombre") IsNot DBNull.Value AndAlso lRow("nombre") IsNot Nothing Then
                                Obj.Nombre = CType(lRow("nombre"), String)
                            End If

                            If lRow("IdUbicacionDefecto") IsNot DBNull.Value AndAlso lRow("IdUbicacionDefecto") IsNot Nothing Then
                                Obj.IdUbicacionDefecto = CType(lRow("IdUbicacionDefecto"), Int32)
                            End If

                            If lRow("Utilizable") IsNot DBNull.Value AndAlso lRow("Utilizable") IsNot Nothing Then
                                Obj.Utilizable = CType(lRow("Utilizable"), Boolean)
                            End If

                            If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                                Obj.Activo = CType(lRow("activo"), Boolean)
                            End If

                            If lRow("Dañado") IsNot DBNull.Value AndAlso lRow("Dañado") IsNot Nothing Then
                                Obj.Dañado = CType(lRow("Dañado"), Boolean)
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

    Public Shared Function Get_All_Stock_Con_Estado_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto_estado)

        Dim lReturnList As New List(Of clsBeProducto_estado)

        Try


            Dim vSQL As String = "SELECT distinct * from VW_StockEstadosProducto " &
                                   "WHERE IdProductoBodega = @IdProductoBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_estado

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_estado

                        Obj.IdEstado = CType(lRow("IdProductoEstado"), Int32)

                        If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                            Obj.IdPropietario = CType(lRow("IdPropietario"), Int32)
                        End If

                        If lRow("nombre") IsNot DBNull.Value AndAlso lRow("nombre") IsNot Nothing Then
                            Obj.Nombre = CType(lRow("nombre"), String)
                        End If

                        If lRow("IdUbicacionDefecto") IsNot DBNull.Value AndAlso lRow("IdUbicacionDefecto") IsNot Nothing Then
                            Obj.IdUbicacionDefecto = CType(lRow("IdUbicacionDefecto"), Int32)
                        End If

                        If lRow("Utilizable") IsNot DBNull.Value AndAlso lRow("Utilizable") IsNot Nothing Then
                            Obj.Utilizable = CType(lRow("Utilizable"), Boolean)
                        End If

                        If lRow("activo") IsNot DBNull.Value AndAlso lRow("activo") IsNot Nothing Then
                            Obj.Activo = CType(lRow("activo"), Boolean)
                        End If

                        If lRow("Dañado") IsNot DBNull.Value AndAlso lRow("Dañado") IsNot Nothing Then
                            Obj.Dañado = CType(lRow("Dañado"), Boolean)
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

    Public Shared Function Exists(ByVal pIdEstado As Integer) As Boolean

        Exists = False

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto_estado WHERE IdEstado=@IdEstado"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, ltransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                        Return lExists

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Eliminar_By_IdPropietario(ByVal pIdPropietario As Integer)

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = String.Format("DELETE FROM producto_estado WHERE IdPropietario={0}", pIdPropietario)

                    Using lCommand As New SqlCommand(vSQL, lConnection, ltransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.ExecuteNonQuery()

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdEstado),0) FROM Producto_estado"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdEstado(ByVal pIdEstado As Integer) As clsBeProducto_estado

        Get_Single_By_IdEstado = Nothing

        Try

            Dim vSQL As String = "SELECT * from VW_ProductoEstado Where IdEstado=@IdEstado "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using ltransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = ltransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim ObjPE As New clsBeProducto_estado()
                            Cargar(ObjPE, lRow)
                            Return ObjPE

                        End If

                    End Using

                    ltransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20180109 05:42 PM Sobrecargué la función GetSingleByIdEstado para poder usarla con una transacción y conexión externa
    Public Shared Function GetSingleByIdEstado(ByVal pIdEstado As Integer, ByRef pConnection As SqlConnection, ByRef pTransaction As SqlTransaction) As clsBeProducto_estado

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim TransaccionExterna As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)

        Dim lDTA As New SqlDataAdapter

        GetSingleByIdEstado = Nothing

        Try

            Dim vSQL As String = "SELECT * from VW_ProductoEstado Where  IdEstado=@IdEstado"

            If Not TransaccionExterna Then
                lDTA = New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
            Else
                lDTA = New SqlDataAdapter(vSQL, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)

            Dim lDT As New DataTable
            lDTA.Fill(lDT)

            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                Dim lRow As DataRow = lDT.Rows(0)
                Dim ObjPE As New clsBeProducto_estado()
                Cargar(ObjPE, lRow)

                Return ObjPE

            End If

            If Not TransaccionExterna Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    ''#EJC20171025_0100AM: Creada originalmente para obtener nombre de estado origen en cambio de estado.
    Public Shared Function Get_Nombre_By_IdEstado(ByVal pIdEstado As Integer) As String

        Get_Nombre_By_IdEstado = ""

        Try

            Dim vSQL As String = "SELECT * from VW_ProductoEstado Where  IdEstado=@IdEstado"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            Get_Nombre_By_IdEstado = IIf(IsDBNull(lRow("Nombre")), "", lRow("Nombre"))

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

    Public Shared Function GetNombreByIdEstado(ByVal pIdEstado As Integer,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction) As String

        GetNombreByIdEstado = ""

        Try

            Dim vSQL As String = "SELECT * from VW_ProductoEstado Where  IdEstado=@IdEstado"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    GetNombreByIdEstado = IIf(IsDBNull(lRow("Nombre")), "", lRow("Nombre"))

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetNombreByIdEstado(ByVal pIdEstado As Integer) As String

        GetNombreByIdEstado = ""

        Try

            Dim vSQL As String = "SELECT * from VW_ProductoEstado Where  IdEstado=@IdEstado"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)

                            GetNombreByIdEstado = IIf(IsDBNull(lRow("Nombre")), "", lRow("Nombre"))

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

    Public Shared Sub Eliminar_Producto_Estado_By_IdProductoEstadUbic(ByVal pIdProductoEstadUbic As Integer)

        Try

            Dim vSQL As String = "DELETE FROM producto_estado_ubic WHERE IdProductoEstadUbic=@pIdEstadoUbi"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@pIdEstadoUbi", pIdProductoEstadUbic)
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

    '''<summary>
    ''' Función creada por Bismarck
    ''' </summary>
    Public Shared Function BismarckFunction() As Boolean
        Return True
    End Function

    Public Shared Sub GuardarTransaccion(ByVal pListObjPE As List(Of clsBeProducto_estado))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            For Each Obj As clsBeProducto_estado In pListObjPE
                If Obj.IsNew Then
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Shared Function Insert_Producto_Estado_With_Ubic(ByVal pObjPE As clsBeProducto_estado, ByVal pListDet As List(Of clsBeProducto_estado_ubic)) As Boolean

        Insert_Producto_Estado_With_Ubic = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pObjPE.IsNew Then
                pObjPE.IdEstado = MaxID(lConnection, lTransaction) + 1
                Insertar(pObjPE, lConnection, lTransaction)
            Else
                Actualizar(pObjPE, lConnection, lTransaction)
            End If

            Dim lMax As Integer = clsLnProducto_estado_ubic.MaxID(lConnection, lTransaction)

            For Each Obj As clsBeProducto_estado_ubic In pListDet
                If Obj.IsNew Then
                    lMax += 1
                    Obj.IdProductoEstadUbic = lMax
                    Obj.IdEstado = pObjPE.IdEstado
                    clsLnProducto_estado_ubic.Insertar(Obj, lConnection, lTransaction)
                Else
                    clsLnProducto_estado_ubic.Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

            Insert_Producto_Estado_With_Ubic = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Get_Estados_Producto() As List(Of clsBeProducto_estado)

        Get_Estados_Producto = Nothing

        Try

            Dim lProductosEstado As New List(Of clsBeProducto_estado)
            Dim vBeProductoEstado As New clsBeProducto_estado

            Dim DT As New DataTable

            DT = Listar()

            For Each pe As DataRow In DT.Rows

                vBeProductoEstado = New clsBeProducto_estado
                Cargar(vBeProductoEstado, pe)
                lProductosEstado.Add(vBeProductoEstado)

            Next

            DT.Dispose()

            Return lProductosEstado

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function GetAllByForCombo() As DataTable

        Try

            Const sp As String = "Select IdEstado, nombre from producto_estado where activo = 1"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Combo_NE() As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_For_Combo_NE = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "Select IdEstado, nombre from producto_estado where activo = 1 AND sistema=1 AND utilizable=0 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Get_All_For_Combo_NE = dt

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Listar_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer, ByVal pIdBodega As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Listar_By_IdPropietario_And_IdBodega = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT * FROM VW_Producto_Estado_Ubic_Bodega 
                                WHERE activo = 1 
                                AND IdPropietario = @IdPropietario
                                AND (IdBodega = @IdBodega OR IdBodega IS NULL)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            Listar_By_IdPropietario_And_IdBodega = dt

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Listar_By_IdPropietario(ByVal pIdPropietario As Integer,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As DataTable

        Listar_By_IdPropietario = Nothing

        Try

            Dim sp As String = "SELECT * FROM producto_estado 
                                WHERE activo = 1 
                                AND IdPropietario = @IdPropietario "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Listar_By_IdPropietario = dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Listar_By_IdPropietario_And_IdBodegaHH(ByVal pIdPropietario As Integer,
                                                                  ByVal pIdBodega As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Listar_By_IdPropietario_And_IdBodegaHH = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT * FROM VW_Producto_Estado_Ubic_Bodega_HH 
                                WHERE activo = 1 
                                AND IdPropietario = @IdPropietario
                                AND ISNULL(IdBodega, 0) in (@IdBodega,0)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            Listar_By_IdPropietario_And_IdBodegaHH = dt

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#GT26112024: metodo para cargar combo en ajuste positivo
    Public Shared Function Get_All_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer,
                                                                  ByVal pIdBodega As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_By_IdPropietario_And_IdBodega = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT IdEstado,nombre as Nombre, utilizable as Utilizable FROM VW_Producto_Estado_Ubic_Bodega_HH 
                                WHERE activo = 1 
                                AND IdPropietario = @IdPropietario
                                AND ISNULL(IdBodega, 0) in (@IdBodega,0)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            Get_All_By_IdPropietario_And_IdBodega = dt

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Listar_By_IdPropietario_And_BodegaSAP(ByVal pIdPropietario As Integer,
                                                                  ByVal pIdBodega As Integer,
                                                                  ByVal pCodigoBodegaERP As String) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Listar_By_IdPropietario_And_BodegaSAP = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT * FROM VW_Producto_Estado_Ubic_Bodega_HH 
                                WHERE activo = 1 
                                AND IdPropietario = @IdPropietario
                                AND ISNULL(IdBodega, 0) in (@IdBodega,0)
                                AND (codigo_bodega_erp = @CodigoBodegaERP OR 
								codigo_bodega_erp= (SELECT codigo_bodega_erp_nc FROM i_nav_config_enc WHERE idbodega = @IdBodega))"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@CodigoBodegaERP", pCodigoBodegaERP)
            Dim dt As New DataTable
            dad.Fill(dt)

            Listar_By_IdPropietario_And_BodegaSAP = dt

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Buen_Estado_Porducto_By_IdPropietario_And_IdBodegaHH(ByVal pIdPropietario As Integer,
                                                                                    ByVal pIdBodega As Integer,
                                                                                    ByVal lConnection As SqlConnection,
                                                                                    ByVal lTransaction As SqlTransaction) As clsBeProducto_estado

        Get_Buen_Estado_Porducto_By_IdPropietario_And_IdBodegaHH = Nothing

        Try

            Dim sp As String = "SELECT * FROM VW_Producto_Estado_Ubic_Bodega_HH 
                                WHERE ACTIVO = 1 
                                AND IdPropietario = @IdPropietario
                                AND ISNULL(IdBodega, 0) in (@IdBodega,0)
                                AND DAÑADO = 0 AND UTILIZABLE = 1"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable

            dad.Fill(dt)
            cmd.Dispose()
            dad.Dispose()

            Dim vBeProductoEstado As New clsBeProducto_estado

            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                vBeProductoEstado = New clsBeProducto_estado
                Cargar(vBeProductoEstado, dr, True)
                Get_Buen_Estado_Porducto_By_IdPropietario_And_IdBodegaHH = vBeProductoEstado
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Listar_By_IdPropietario_For_HH(ByVal pIdPropietario As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Listar_By_IdPropietario_For_HH = Nothing

        Try
            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT * FROM Producto_estado WHERE activo = 1 and IdPropietario = @IdPropietario"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            Dim dt As New DataTable
            dad.Fill(dt)

            Listar_By_IdPropietario_For_HH = dt

            cmd.Dispose()
            dad.Dispose()

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Estados_By_IdPropietario_For_HH(ByVal pIdPropietario As Integer) As List(Of clsBeProducto_estado)

        Get_Estados_By_IdPropietario_For_HH = Nothing

        Try

            Dim lProductosEstado As New List(Of clsBeProducto_estado)
            Dim vBeProductoEstado As New clsBeProducto_estado

            Dim DT As New DataTable

            DT = Listar_By_IdPropietario_For_HH(pIdPropietario)

            For Each pe As DataRow In DT.Rows

                vBeProductoEstado = New clsBeProducto_estado
                Cargar(vBeProductoEstado, pe)
                lProductosEstado.Add(vBeProductoEstado)

            Next

            DT.Dispose()

            Return lProductosEstado

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Get_Estados_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer,
                                                                     ByVal pIdBodega As Integer) As List(Of clsBeProducto_estado)

        Get_Estados_By_IdPropietario_And_IdBodega = Nothing

        Try

            Dim lProductosEstado As New List(Of clsBeProducto_estado)
            Dim vBeProductoEstado As New clsBeProducto_estado

            Dim DT As New DataTable

            DT = Listar_By_IdPropietario_And_IdBodega(pIdPropietario, pIdBodega)

            For Each pe As DataRow In DT.Rows

                vBeProductoEstado = New clsBeProducto_estado
                Cargar(vBeProductoEstado, pe, True)
                lProductosEstado.Add(vBeProductoEstado)

            Next

            DT.Dispose()

            Return lProductosEstado

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Get_Estados_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer,
                                                                     ByVal pIdBodega As Integer,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto_estado)

        Get_Estados_By_IdPropietario_And_IdBodega = Nothing

        Try

            Dim lProductosEstado As New List(Of clsBeProducto_estado)
            Dim vBeProductoEstado As New clsBeProducto_estado

            Dim DT As New DataTable

            DT = Listar_By_IdPropietario_And_IdBodega(pIdPropietario, pIdBodega, lConnection, lTransaction)

            For Each pe As DataRow In DT.Rows

                vBeProductoEstado = New clsBeProducto_estado
                Cargar(vBeProductoEstado, pe, True)
                lProductosEstado.Add(vBeProductoEstado)

            Next

            DT.Dispose()

            Return lProductosEstado

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Get_Estados_By_IdPropietario_And_IdBodegaHH(ByVal pIdPropietario As Integer, ByVal pIdBodega As Integer) As List(Of clsBeProducto_estado)

        Get_Estados_By_IdPropietario_And_IdBodegaHH = Nothing

        Try

            Dim lProductosEstado As New List(Of clsBeProducto_estado)
            Dim vBeProductoEstado As New clsBeProducto_estado

            Dim DT As New DataTable

            DT = Listar_By_IdPropietario_And_IdBodegaHH(pIdPropietario, pIdBodega)

            For Each pe As DataRow In DT.Rows

                vBeProductoEstado = New clsBeProducto_estado
                Cargar(vBeProductoEstado, pe, True)
                lProductosEstado.Add(vBeProductoEstado)

            Next

            DT.Dispose()

            Return lProductosEstado

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeProducto_estado As clsBeProducto_estado,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Const sp As String = "SELECT * FROM Producto_estado" &
            " Where(IdEstado = @IdEstado)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDESTADO", oBeProducto_estado.IDESTADO))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_estado, dt.Rows(0))
                Obtener = True
            End If


        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Listar_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer,
                                                                ByVal pIdBodega As Integer,
                                                                ByVal lConnection As SqlConnection,
                                                                ByVal lTransaction As SqlTransaction) As DataTable

        Listar_By_IdPropietario_And_IdBodega = Nothing

        Try

            Dim sp As String = "SELECT * FROM VW_Producto_Estado_Ubic_Bodega 
                                WHERE activo = 1 
                                AND IdPropietario = @IdPropietario
                                AND IdBodega = @IdBodega "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            Listar_By_IdPropietario_And_IdBodega = dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_IdEstado_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer,
                                                                         ByVal pIdBodega As Integer,
                                                                         ByVal pIdEstado As Integer,
                                                                         ByVal lConnection As SqlConnection,
                                                                         ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto_estado)

        Existe_IdEstado_By_IdPropietario_And_IdBodega = Nothing

        Try

            Dim lProductosEstado As New List(Of clsBeProducto_estado)
            Dim vBeProductoEstado As New clsBeProducto_estado

            Dim DT As New DataTable


            Dim sp As String = "SELECT * FROM VW_Producto_Estado_Ubic_Bodega 
                                WHERE activo = 1 
                                AND IdPropietario = @IdPropietario
                                AND IdBodega = @IdBodega 
                                AND IdEstado = @IdEstado "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)
            dad.Fill(DT)

            For Each pe As DataRow In DT.Rows

                vBeProductoEstado = New clsBeProducto_estado
                Cargar(vBeProductoEstado, pe, True)
                lProductosEstado.Add(vBeProductoEstado)

            Next

            DT.Dispose()

            Return lProductosEstado

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Existe_IdEstado_By_IdPropietario(ByVal pIdPropietario As Integer,
                                                            ByVal pIdEstado As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto_estado)

        Existe_IdEstado_By_IdPropietario = Nothing

        Try

            Dim lProductosEstado As New List(Of clsBeProducto_estado)
            Dim vBeProductoEstado As New clsBeProducto_estado

            Dim DT As New DataTable


            Dim sp As String = "SELECT * FROM VW_Producto_Estado_Ubic_Bodega 
                                WHERE activo = 1 
                                AND IdPropietario = @IdPropietario                                
                                AND IdEstado = @IdEstado "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            dad.SelectCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)
            dad.Fill(DT)

            For Each pe As DataRow In DT.Rows

                vBeProductoEstado = New clsBeProducto_estado
                Cargar(vBeProductoEstado, pe, True)
                lProductosEstado.Add(vBeProductoEstado)

            Next

            DT.Dispose()

            Return lProductosEstado

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function


    Public Shared Function Get_ProductoEstado_By_IdPropietario_and_DescEstado(ByVal pIdPropietario As Integer,
                                                                         ByVal pEstado As String,
                                                                         ByVal lConnection As SqlConnection,
                                                                         ByVal lTransaction As SqlTransaction) As clsBeProducto_estado
        Get_ProductoEstado_By_IdPropietario_and_DescEstado = Nothing

        Try

            Dim lProductosEstado As New List(Of clsBeProducto_estado)
            Dim vBeProductoEstado As New clsBeProducto_estado

            Dim DT As New DataTable


            Dim sp As String = "SELECT * FROM producto_estado 
                                WHERE activo = 1 
                                AND IdPropietario = @IdPropietario
                                AND Nombre = @pNombre "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            dad.SelectCommand.Parameters.AddWithValue("@pNombre", pEstado)
            dad.Fill(DT)

            If DT.Rows.Count = 1 Then
                vBeProductoEstado = New clsBeProducto_estado
                Cargar(vBeProductoEstado, DT.Rows(0))
                Get_ProductoEstado_By_IdPropietario_and_DescEstado = vBeProductoEstado
            End If

            DT.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
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


    Public Shared Function Get_Single_By_IdEstado(ByVal pIdEstado As Integer,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As clsBeProducto_estado

        Get_Single_By_IdEstado = Nothing

        Try

            Dim vSQL As String = "SELECT * from VW_ProductoEstado Where  IdEstado=@IdEstado "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdEstado", pIdEstado)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeProductoEstado As New clsBeProducto_estado()
                    Cargar(BeProductoEstado, lRow)
                    Return BeProductoEstado

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Estados_By_IdPropietario_And_IdBodega_by_SAP(ByVal pIdPropietario As Integer,
                                                                            ByVal pIdBodega As Integer,
                                                                            ByVal pCodigoBodega As String) As List(Of clsBeProducto_estado)

        Get_Estados_By_IdPropietario_And_IdBodega_by_SAP = Nothing

        Try

            Dim lProductosEstado As New List(Of clsBeProducto_estado)
            Dim vBeProductoEstado As New clsBeProducto_estado

            Dim DT As New DataTable

            DT = Listar_By_IdPropietario_And_IdBodega_By_SAP(pIdPropietario, pIdBodega, pCodigoBodega)

            For Each pe As DataRow In DT.Rows

                vBeProductoEstado = New clsBeProducto_estado
                Cargar(vBeProductoEstado, pe, True)
                lProductosEstado.Add(vBeProductoEstado)

            Next

            DT.Dispose()

            Return lProductosEstado

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function Verificar_Fecha_Vencimiento(ByVal fechaVencimiento As DateTime) As Boolean

        Dim diasVencimiento As Integer
        Dim toleranciaDias As Integer

        Dim query As String = "SELECT dias_vencimiento_clasificacion, tolerancia_dias_vencimiento FROM producto_estado WHERE dias_vencimiento_clasificacion > 0 "

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using command As New SqlCommand(query, lConnection, lTransaction)
                        Using reader As SqlDataReader = command.ExecuteReader()
                            If reader.Read() Then
                                diasVencimiento = Convert.ToInt32(reader("dias_vencimiento_clasificacion"))
                                toleranciaDias = Convert.ToInt32(reader("tolerancia_dias_vencimiento"))
                            End If
                        End Using
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()
            End Using

            ' Verificar si la fecha de vencimiento está dentro del rango permitido.
            Dim fechaLimiteInferior As DateTime = fechaVencimiento.AddDays(-toleranciaDias)
            Dim fechaLimiteSuperior As DateTime = fechaVencimiento.AddDays(diasVencimiento + toleranciaDias)

            ' Comparar la fecha actual con los límites establecidos.
            Return DateTime.Now >= fechaLimiteInferior AndAlso DateTime.Now <= fechaLimiteSuperior

        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Public Shared Function Get_All_By_IdPropietarioBodega(ByVal pIdPropietario As Integer) As DataTable

        Try


            Dim vSQL As String = "SELECT 
                            bodega.codigo + ' - '  + bodega.nombre AS Bodega,
                            pe.IdEstado, pe.nombre, pe.IdUbicacionDefecto, 
                            pe.utilizable, pe.activo, 
                            pe.dañado, pe.sistema ,pe.codigo_bodega_erp
                            FROM producto_estado AS pe 
                            INNER JOIN producto_estado_ubic ON pe.IdEstado = producto_estado_ubic.IdEstado 
                            INNER JOIN bodega ON producto_estado_ubic.IdBodega = bodega.IdBodega
                            WHERE pe.activo = 1 And pe.IdPropietario = @IdPropietario "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Get_All_By_IdPropietarioBodega = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdEstado_By_Codigo_Area(ByVal pCodigo As String,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim rIdEstado As Integer = 0

            Const sp As String = "SELECT pe.IdEstado
                                  FROM producto_estado pe
                                  WHERE pe.codigo_bodega_erp = @codigo_bodega_erp AND pe.activo = 1 AND 
                                        pe.utilizable = 1 AND pe.dañado = 0 "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@codigo_bodega_erp", pCodigo)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    rIdEstado = CInt(lReturnValue)
                End If

            End Using

            Return rIdEstado

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Listar_By_IdPropietario_And_IdBodega_By_SAP(ByVal pIdPropietario As Integer,
                                                                       ByVal pIdBodega As Integer,
                                                                       ByVal pCodigoBodegaERP As String) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Listar_By_IdPropietario_And_IdBodega_By_SAP = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT * FROM VW_Producto_Estado_Ubic_Bodega_HH 
                                WHERE activo = 1 
                                AND IdPropietario = @IdPropietario
                                AND ISNULL(IdBodega, 0) in (@IdBodega,0)
                                AND (codigo_bodega_erp = @CodigoBodegaERP OR 
								codigo_bodega_erp= (SELECT codigo_bodega_erp_nc FROM i_nav_config_enc WHERE idbodega = @IdBodega))"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@CodigoBodegaERP", pCodigoBodegaERP)
            Dim dt As New DataTable
            dad.Fill(dt)

            Listar_By_IdPropietario_And_IdBodega_By_SAP = dt

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#GT22112024: obtener estado o estados para un producto sin stock
    Public Shared Function Get_ProductoEstado_By_IdPropietario_and_Actvio(ByVal pIdPropietario As Integer) As clsBeProducto_estado


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Get_ProductoEstado_By_IdPropietario_and_Actvio = Nothing

        Try

            Dim lProductosEstado As New List(Of clsBeProducto_estado)
            Dim vBeProductoEstado As New clsBeProducto_estado

            Dim DT As New DataTable


            Dim sp As String = "SELECT * FROM producto_estado 
                                WHERE activo = 1 
                                AND IdPropietario = @IdPropietario "


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
            dad.Fill(DT)

            If DT.Rows.Count = 1 Then
                vBeProductoEstado = New clsBeProducto_estado
                Cargar(vBeProductoEstado, DT.Rows(0))
                Get_ProductoEstado_By_IdPropietario_and_Actvio = vBeProductoEstado
            End If

            DT.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    '#GT16052025: obtener producto_estado activo para proceso importación a la nube.
    Public Shared Function GetAll_ByPropietario_And_Activo(ByVal pIdPropietario As Integer, ByVal pActivo As Boolean,
                                                           ByRef lConnection As SqlConnection,
                                                           ByRef lTransaction As SqlTransaction) As List(Of clsBeProducto_estado)

        Dim lReturnList As New List(Of clsBeProducto_estado)

        Try

            Dim vSQL As String = "SELECT * FROM Producto_Estado WHERE IdPropietario=@IdPropietario and activo=@pActivo "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                lDTA.SelectCommand.Parameters.AddWithValue("@pActivo", pActivo)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_estado

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_estado
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next
                Else
                    lReturnList = Nothing
                    End If

                End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Buen_Estado_Producto_By_IdPropietario(ByVal pIdPropietario As Integer,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As Integer

        Get_Buen_Estado_Producto_By_IdPropietario = 0

        Try

            Dim sp As String = "SELECT top(1) * FROM producto_estado 
                                WHERE ACTIVO = 1 AND DAÑADO = 0 AND UTILIZABLE = 1 and IdPropietario = @IdPropietario"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

            Dim dt As New DataTable

            dad.Fill(dt)
            cmd.Dispose()
            dad.Dispose()

            Dim vBeProductoEstado As New clsBeProducto_estado

            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                Get_Buen_Estado_Producto_By_IdPropietario = dr.Item("IdEstado")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class
