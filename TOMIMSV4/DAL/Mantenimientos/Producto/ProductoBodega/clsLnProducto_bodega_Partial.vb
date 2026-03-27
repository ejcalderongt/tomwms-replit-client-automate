Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProducto_bodega
    Implements IDisposable

    Public Shared Function MaxID(ByRef pConnection As SqlConnection, ByRef pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdProductoBodega),0) FROM producto_bodega"

            Using lCommand As New SqlCommand(sp, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

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

    Public Shared Function Get_All_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_bodega)

        Dim lReturnList As New List(Of clsBeProducto_bodega)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM producto_bodega WHERE IdProducto=@IdProducto"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeProducto_bodega

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeProducto_bodega

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

    Public Shared Function Get_IdProducto_By_IdProductoBodega(ByVal pIdProductoBodega As Integer) As Integer

        Get_IdProducto_By_IdProductoBodega = 0

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT IdProducto FROM producto_bodega WHERE IdProductoBodega=@IdProductoBodega"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_IdProducto_By_IdProductoBodega = lDataTable.Rows(0).Item("IdProducto")
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

    Public Shared Function Get_IdProducto_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                              ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction) As Integer

        Get_IdProducto_By_IdProductoBodega = 0

        Try

            Dim vSQL As String = "SELECT IdProducto FROM producto_bodega WHERE IdProductoBodega=@IdProductoBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_IdProducto_By_IdProductoBodega = lDataTable.Rows(0).Item("IdProducto")
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'EJC20190410: 12:00 AN,
    Public Shared Function Get_Producto_By_IdProductoBodega(ByVal pIdProductoBodega As Integer) As clsBeProducto

        Get_Producto_By_IdProductoBodega = Nothing

        Dim BeProducto As New clsBeProducto

        Try


            Dim vSQL As String = "SELECT IdProducto FROM producto_bodega WHERE IdProductoBodega=@IdProductoBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            BeProducto.IdProducto = lDataTable.Rows(0).Item("IdProducto")
                            BeProducto = clsLnProducto.GetSingle(BeProducto.IdProducto, lConnection, lTransaction)
                            BeProducto.IdProductoBodega = pIdProductoBodega
                            Return BeProducto
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

    Public Shared Function Get_Producto_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As clsBeProducto

        Get_Producto_By_IdProductoBodega = Nothing

        Dim BeProducto As New clsBeProducto

        Try

            Dim vSQL As String = "SELECT IdProducto FROM producto_bodega WHERE IdProductoBodega=@IdProductoBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    BeProducto.IdProducto = lDataTable.Rows(0).Item("IdProducto")
                    BeProducto = clsLnProducto.GetSingle(BeProducto.IdProducto, lConnection, lTransaction)
                    BeProducto.IdProductoBodega = pIdProductoBodega
                    Return BeProducto
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20180109 05:42 PM Sobrecargué la función GetProductoByIdProductoBodega para poder usarla con una transacción y conexión externa
    Public Shared Function Get_BeProducto_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                              Optional ByVal pConnection As SqlConnection = Nothing,
                                                              Optional pTransaction As SqlTransaction = Nothing) As clsBeProducto

        Dim BeProducto As New clsBeProducto
        Dim lDTA As New SqlDataAdapter

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim TransaccionExterna As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)

        Get_BeProducto_By_IdProductoBodega = Nothing

        Try

            Dim vSQL As String = "SELECT IdProducto FROM producto_bodega WHERE IdProductoBodega=@IdProductoBodega"

            If Not TransaccionExterna Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            If Not TransaccionExterna Then
                lDTA = New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
            Else
                lDTA = New SqlDataAdapter(vSQL, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                BeProducto.IdProducto = lDataTable.Rows(0).Item("IdProducto")

                If Not TransaccionExterna Then
                    BeProducto = clsLnProducto.GetSingle(BeProducto.IdProducto, lConnection, lTransaction)
                Else
                    BeProducto = clsLnProducto.GetSingle(BeProducto.IdProducto, pConnection, pTransaction)
                End If

                BeProducto.IdProductoBodega = pIdProductoBodega

                Return BeProducto

            End If

            If Not TransaccionExterna Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If Not TransaccionExterna Then If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not TransaccionExterna Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
                If lTransaction IsNot Nothing Then lTransaction.Dispose()
            End If
        End Try

    End Function

    '#CKFK 20180109 05:42 PM Sobrecargué la función GetProductoByIdProductoBodega para poder usarla con una transacción y conexión externa
    Public Shared Function Get_BeProducto_By_Nombre(ByVal pNombreProducto As String,
                                                    ByVal pIdBodega As String,
                                                    ByRef pConnection As SqlConnection,
                                                    ByRef pTransaction As SqlTransaction) As clsBeProducto

        Dim BeProducto As New clsBeProducto
        Dim lDTA As New SqlDataAdapter

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim TransaccionExterna As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)

        Get_BeProducto_By_Nombre = Nothing

        Try

            Dim vSQL As String = "SELECT DISTINCT p.IdProducto, pb.IdProductoBodega  
                                  FROM producto_bodega pb INNER JOIN producto p ON pb.IdProducto = p.IdProducto
                                  WHERE p.nombre =@pNombreProducto AND IdBodega = @pIdBodega "

            If Not TransaccionExterna Then
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction
            End If

            If Not TransaccionExterna Then
                lDTA = New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
            Else
                lDTA = New SqlDataAdapter(vSQL, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@pNombreProducto", pNombreProducto)
            lDTA.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                BeProducto.IdProducto = lDataTable.Rows(0).Item("IdProducto")
                If Not TransaccionExterna Then
                    BeProducto = clsLnProducto.GetSingle(BeProducto.IdProducto, lConnection, lTransaction)
                Else
                    BeProducto = clsLnProducto.GetSingle(BeProducto.IdProducto, pConnection, pTransaction)
                End If

                BeProducto.IdProductoBodega = lDataTable.Rows(0).Item("IdProductoBodega")

                Return BeProducto

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

    Public Shared Function Get_All_By_IdBodega(ByVal IdBodega As Integer) As List(Of clsBeProducto_bodega)

        Dim lReturnList As New List(Of clsBeProducto_bodega)

        Try

            Dim vSQL As String = "SELECT * FROM producto_bodega WHERE IdBodega=@IdBodega adn Activo =1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim vBeProductoBodega As clsBeProducto_bodega

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            vBeProductoBodega = New clsBeProducto_bodega
                            Cargar(vBeProductoBodega, lRow)
                            lReturnList.Add(vBeProductoBodega)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal IdBodega As Integer,
                                               ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto_bodega)

        Get_All_By_IdBodega = Nothing

        Dim lReturnList As New List(Of clsBeProducto_bodega)

        Try

            Dim vSQL As String = "SELECT * FROM producto_bodega WHERE IdBodega=@IdBodega AND Activo =1 "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeProductoBodega As clsBeProducto_bodega

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            vBeProductoBodega = New clsBeProducto_bodega
                            CargarDetProducto(vBeProductoBodega, lRow, lConnection, lTransaction)
                            lReturnList.Add(vBeProductoBodega)

                        Next

                    End If

                    Get_All_By_IdBodega = lReturnList

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_HH(ByVal IdBodega As Integer) As List(Of clsBeProducto_bodega)

        Dim lReturnList As New List(Of clsBeProducto_bodega)

        Try

            Dim vSQL As String = "SELECT * FROM producto_bodega WHERE IdBodega=@IdBodega and Activo =1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeProductoBodega As clsBeProducto_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                vBeProductoBodega = New clsBeProducto_bodega
                                CargarDetProducto(vBeProductoBodega, lRow, lConnection, lTransaction)
                                lReturnList.Add(vBeProductoBodega)

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

    Public Shared Sub Cargar(ByRef oBeProducto_bodega As clsBeProducto_bodega, ByRef dr As DataRow)

        Try

            With oBeProducto_bodega

                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .Producto.IdProducto = .IdProducto
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub CargarDetProducto(ByRef oBeProducto_bodega As clsBeProducto_bodega,
                                        ByRef dr As DataRow,
                                        ByRef lConnection As SqlConnection,
                                        ByRef lTransaction As SqlTransaction)

        Try

            With oBeProducto_bodega

                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .Producto.IdProducto = .IdProducto
                clsLnProducto.Obtener(.Producto, lConnection, lTransaction)
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function InsertarFromInterface(ByRef oBeProducto_bodega As clsBeProducto_bodega,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTrans As SqlTransaction) As Integer

        Try

            Ins.Init("producto_bodega")
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text, .Transaction = lTrans}


            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeProducto_bodega.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_bodega.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeProducto_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_bodega.Activo))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeProducto_bodega.Sistema))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_bodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_bodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_bodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_bodega.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe(ByVal pCodigo As String,
                                  ByVal pIdBodega As Integer,
                                  ByRef lConnection As SqlConnection,
                                  ByRef lTransaction As SqlTransaction) As clsBeProducto_bodega

        Existe = Nothing

        Try

            Dim vSQL As String = "SELECT * from producto_bodega pb Inner Join producto p 
                                  ON pb.IdProducto = p.IdProducto 
                                  WHERE p.codigo =  @Codigo AND pb.IdBodega = @IdBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeProductoBodega As New clsBeProducto_bodega()
                    Dim ObjP As New clsBeProducto()
                    Cargar(BeProductoBodega, lRow)
                    ObjP.IdProducto = BeProductoBodega.IdProducto
                    ObjP = clsLnProducto.GetSingle(ObjP.IdProducto, lConnection, lTransaction)
                    BeProductoBodega.Producto = ObjP
                    Existe = BeProductoBodega

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Codigo_By_IdBodega(ByVal pCodigo As String,
                                                     ByVal pIdBodega As Integer,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction) As clsBeProducto_bodega

        Existe_Codigo_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * from producto_bodega pb Inner Join producto p 
                                  ON pb.IdProducto = p.IdProducto 
                                  WHERE (p.codigo =  @Codigo AND pb.IdBodega =@IdBodega)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjPB As New clsBeProducto_bodega()
                    Dim ObjP As New clsBeProducto()

                    Cargar(ObjPB, lRow)

                    ObjP.IdProducto = ObjPB.IdProducto
                    ObjP = clsLnProducto.GetSingle(ObjP.IdProducto, lConnection, lTransaction)
                    ObjPB.Producto = ObjP
                    Return ObjPB

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exist(ByVal pCodigo As String,
                                 ByRef Cnn As SqlConnection,
                                 ByRef ltrans As SqlTransaction) As Boolean

        Exist = False

        Try

            Dim vSQL As String = "SELECT * from producto_bodega pb Inner Join producto p 
                    ON pb.IdProducto = p.IdProducto Where p.codigo =  @Codigo "

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Transaction = ltrans

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Exist = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdProductoBodega_By_IdProducto_And_IdBodega(ByVal pIdProducto As Integer,
                                                                           ByVal pIdBodega As Integer) As Integer

        Get_IdProductoBodega_By_IdProducto_And_IdBodega = 0

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdProductoBodega FROM producto_bodega WHERE IdProducto=@IdProducto AND IdBodega=@IdBodega"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_IdProductoBodega_By_IdProducto_And_IdBodega = lDataTable.Rows(0).Item("IdProductoBodega")
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

    Public Shared Function Exist_By_IdProducto_And_IdBodega(ByVal pIdProducto As Integer,
                                                            ByVal pIdBodega As Integer) As Boolean

        Exist_By_IdProducto_And_IdBodega = False

        Try


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT IdProductoBodega 
                                          FROM producto_bodega 
                                          WHERE IdProducto=@IdProducto 
                                          AND IdBodega=@IdBodega"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Exist_By_IdProducto_And_IdBodega = False
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

    Public Shared Function Get_IdProductoBodega_By_IdProducto_And_IdBodega(ByVal pIdProducto As Integer,
                                                                           pIdBodega As Integer,
                                                                           ByRef pConnection As SqlConnection,
                                                                           ByRef pTransaction As SqlTransaction) As Integer

        Get_IdProductoBodega_By_IdProducto_And_IdBodega = 0

        Try

            Dim lDTA As New SqlDataAdapter

            Dim vSQL As String = "SELECT IdProductoBodega FROM producto_bodega WHERE IdProducto=@IdProducto AND IdBodega=@IdBodega"

            lDTA = New SqlDataAdapter(vSQL, pConnection)
            lDTA.SelectCommand.Transaction = pTransaction

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
            lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                Get_IdProductoBodega_By_IdProducto_And_IdBodega = lDataTable.Rows(0).Item("IdProductoBodega")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdBodega_By_IdProductoBodega(ByVal pIdProductoBodega As Integer, ByRef pConnection As SqlConnection, ByRef pTransaction As SqlTransaction) As Integer

        Get_IdBodega_By_IdProductoBodega = 0

        Try

            Dim lDTA As New SqlDataAdapter

            Dim vSQL As String = "SELECT IdBodega FROM producto_bodega WHERE IdProductoBodega=@IdProductoBodega"

            lDTA = New SqlDataAdapter(vSQL, pConnection)
            lDTA.SelectCommand.Transaction = pTransaction

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                Get_IdBodega_By_IdProductoBodega = lDataTable.Rows(0).Item("IdBodega")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Actualizar(ByVal ListBeProductoBodega As List(Of clsBeProducto_bodega)) As Boolean

        Actualizar = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lMax As Integer = MaxID()

            For Each Obj As clsBeProducto_bodega In ListBeProductoBodega
                If Obj.IdProductoBodega = 0 Then
                    lMax += 1
                    Obj.IdProductoBodega = lMax
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()
            lConnection.Close()

            Return True

        Catch ex As Exception
            lTransaction.Rollback()
            lConnection.Close()
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdProductoBodega As Integer) As clsBeProducto_bodega

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Producto_bodega" &
            " Where(IdProductoBodega = @IdProductoBodega)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", IdProductoBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim pBeProducto_bodega As New clsBeProducto_bodega

            If dt.Rows.Count = 1 Then
                Cargar(pBeProducto_bodega, dt.Rows(0))
                Return pBeProducto_bodega
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Guardar_Transaccion(ByVal pListObjPBod As List(Of clsBeProducto_bodega),
                                         ByRef lConnection As SqlConnection,
                                         ByRef lTransaction As SqlTransaction) As Integer
        Dim total_insertados As Integer = 0
        Try


            For Each Obj As clsBeProducto_bodega In pListObjPBod
                total_insertados += InsertarFromInterface(Obj, lConnection, lTransaction)
            Next

            Return total_insertados

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return total_insertados
        End Try

    End Function

    Public Shared Function Get_Producto_By_IdProductoBodega_For_HH(ByVal pIdProductoBodega As Integer) As clsBeProducto

        Get_Producto_By_IdProductoBodega_For_HH = Nothing

        Dim BeProducto As New clsBeProducto

        Try


            Dim vSQL As String = "SELECT IdProducto FROM producto_bodega WHERE IdProductoBodega=@IdProductoBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            BeProducto.IdProducto = lDataTable.Rows(0).Item("IdProducto")
                            BeProducto = clsLnProducto.GetSingle(BeProducto.IdProducto, lConnection, lTransaction)
                            BeProducto.IdProductoBodega = pIdProductoBodega
                            Get_Producto_By_IdProductoBodega_For_HH = BeProducto
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

    Public Shared Function Get_All_By_IdTipoManufactura(ByVal pIdTipoManufactura As clsDataContractDI.Manufacturing_Process,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As List(Of clsBeProducto_bodega)

        Dim lReturnList As New List(Of clsBeProducto_bodega)

        Try

            Dim vSQL As String = "SELECT * 
                                  FROM producto_bodega 
                                  WHERE IdProducto IN (SELECT IdProducto 
                                                       FROM producto  
                                                       WHERE IdTipoManufactura = @IdTipoManufactura)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTipoManufactura", pIdTipoManufactura)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_bodega

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_bodega

                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Parte_By_IdBodega(ByVal pCodigo As String,
                                                    ByVal pIdBodega As Integer,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As clsBeProducto_bodega

        Existe_Parte_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * from producto_bodega pb Inner Join producto p 
                                  ON pb.IdProducto = p.IdProducto 
                                  WHERE (p.noparte =  @Codigo AND pb.IdBodega =@IdBodega)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjPB As New clsBeProducto_bodega()
                    Dim ObjP As New clsBeProducto()

                    Cargar(ObjPB, lRow)

                    ObjP.IdProducto = ObjPB.IdProducto
                    ObjP = clsLnProducto.GetSingle(ObjP.IdProducto, lConnection, lTransaction)
                    ObjPB.Producto = ObjP
                    Return ObjPB

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_NoSerie_By_IdBodega(ByVal pCodigo As String,
                                                      ByVal pIdBodega As Integer,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As clsBeProducto_bodega

        Existe_NoSerie_By_IdBodega = Nothing

        Try

            Dim vSQL As String = "SELECT * 
                                  FROM producto_bodega pb Inner Join 
                                       producto p ON pb.IdProducto = p.IdProducto 
                                  WHERE (p.noserie =  @Codigo AND pb.IdBodega =@IdBodega)"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjPB As New clsBeProducto_bodega()
                    Dim ObjP As New clsBeProducto()

                    Cargar(ObjPB, lRow)

                    ObjP.IdProducto = ObjPB.IdProducto
                    ObjP = clsLnProducto.GetSingle(ObjP.IdProducto, lConnection, lTransaction)
                    ObjPB.Producto = ObjP
                    Return ObjPB

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT11062025: metodo para enviar datos a la nube con transaccion
    Public Shared Function Get_All_By_IdProducto(ByVal pIdProducto As Integer, ByRef lConnection As SqlConnection,
                                                                               ByRef lTransaction As SqlTransaction) As List(Of clsBeProducto_bodega)

        Dim lReturnList As New List(Of clsBeProducto_bodega)

        Try



            Dim vSQL As String = "SELECT * FROM producto_bodega WHERE IdProducto=@IdProducto"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProducto_bodega

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProducto_bodega
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As List(Of clsBeProducto_bodega)

        Dim lReturnList As New List(Of clsBeProducto_bodega)

        Try

            Dim vSQL As String = "
                                SELECT DISTINCT pb.*
                FROM producto_bodega pb
                INNER JOIN trans_pe_det pd ON pb.IdProductoBodega = pd.IdProductoBodega
                WHERE pd.IdPedidoEnc = @IdPedidoEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPedidoEnc", pIdPedidoEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    For Each lRow As DataRow In lDataTable.Rows
                        Dim Obj As New clsBeProducto_bodega
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)
                    Next
                End If
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

    Public Shared Function Existe_Codigo_By_IdBodega(ByVal pCodigo As String, ByVal pIdBodega As Integer) As Boolean
        Dim cn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

        Try
            cn.Open()

            Dim sql As String =
            "SELECT TOP 1 1 " &
            "FROM producto_bodega pb " &
            "INNER JOIN producto p ON pb.IdProducto = p.IdProducto " &
            "WHERE p.codigo = @Codigo AND pb.IdBodega = @IdBodega;"

            Using cmd As New SqlCommand(sql, cn)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 60
                cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = pCodigo
                cmd.Parameters.Add("@IdBodega", SqlDbType.Int).Value = pIdBodega

                Dim result As Object = cmd.ExecuteScalar()
                Return (result IsNot Nothing AndAlso result IsNot DBNull.Value)
            End Using

        Catch ex As Exception
            Throw
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Function

    Public Shared Function InsertarFromInterface(ByRef oBeProducto_bodega As clsBeProducto_bodega) As Integer
        Dim cn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

        Try
            cn.Open()

            Ins.Init("producto_bodega")
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Using cmd As New SqlCommand(sp, cn)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 60

                cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeProducto_bodega.IdProductoBodega))
                cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_bodega.IdProducto))
                cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeProducto_bodega.IdBodega))
                cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_bodega.Activo))
                cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeProducto_bodega.Sistema))
                cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_bodega.User_agr))
                cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_bodega.Fec_agr))
                cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_bodega.User_mod))
                cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_bodega.Fec_mod))

                Return cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Throw
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Function

End Class