Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnUnidad_medida

    Private Shared lUnidadMedidaInMemory As New List(Of clsBeUnidad_medida)

    Public Shared Function Listar(ByVal pActivo As Boolean, ByVal pFiltro As String) As DataTable

        Try

            Dim sp As String = "SELECT e.IdUnidadMedida,p.IdPropietario,p.nombre_comercial AS Propietario, e.Nombre AS 'Unidad de Medida' " _
                               & "FROM unidad_medida AS e " _
                               & "INNER JOIN propietarios AS p ON e.IdPropietario = p.IdPropietario WHERE 1 > 0 "

            If pActivo Then
                sp += " AND e.Activo=1"
            Else
                sp += " AND e.Activo=0"
            End If

            If String.IsNullOrEmpty(pFiltro) = False Then

                sp += " AND (e.IdUnidadMedida LIKE '%@IdUnidadMedida%'"
                sp += " OR e.Nombre LIKE '%@Nombre%'"
                sp += " OR p.nombre_comercial LIKE '%@nombre_comercial%')"
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection)

            'cmd.CommandType = CommandType.StoredProcedure

            Dim dad As New SqlDataAdapter(cmd)
            If String.IsNullOrEmpty(pFiltro) = False Then
                dad.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pFiltro)
                dad.SelectCommand.Parameters.AddWithValue("@Nombre", pFiltro)
                dad.SelectCommand.Parameters.AddWithValue("@nombre_comercial", pFiltro)
            End If
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

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeUnidad_medida)

        Dim lReturnList As New List(Of clsBeUnidad_medida)

        Try

            Dim vSQL As String = String.Empty

            If pIdPropietario = 0 Then
                vSQL = "SELECT * FROM VW_UnidadMedida WHERE 1 > 0 "
            Else
                vSQL = "SELECT * FROM VW_UnidadMedida WHERE IdPropietario=@IdPropietario "
            End If

            If pActivo Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        If pIdPropietario <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeUnidad_medida

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeUnidad_medida

                                Cargar(Obj, lRow)

                                If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                                    Obj.Propietario = New clsBePropietarios
                                    Obj.Propietario.IdPropietario = CType(lRow("IdPropietario"), Integer)
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

    Public Shared Function Get_All_Filtro_DT(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As DataTable

        Get_All_Filtro_DT = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = String.Empty

                    If pIdPropietario = 0 Then

                        vSQL = "SELECT IdUnidadMedida as Correlativo, Propietario, Codigo, Nombre, es_um_cobro, factor
                            FROM VW_UnidadMedida 
                            WHERE 1 > 0 "
                    Else

                        vSQL = "SELECT IdUnidadMedida as Correlativo, Propietario, Codigo, Nombre, es_um_cobro, factor
                            FROM VW_UnidadMedida 
                            WHERE IdPropietario=@IdPropietario "

                    End If

                    If pActivo Then
                        vSQL += " AND Activo=1"
                    Else
                        vSQL += " AND Activo=0"
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        If pIdPropietario <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_Filtro_DT = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdUnidadMedida As Integer) As clsBeUnidad_medida

        GetSingle = Nothing

        Try

            Dim ObjUM As New clsBeUnidad_medida()
            Dim IdxUnidadMedida As Integer = -1
            Dim vIdUM As Integer = pIdUnidadMedida
            IdxUnidadMedida = lUnidadMedidaInMemory.FindIndex(Function(x) x.IdUnidadMedida = vIdUM)

            If IdxUnidadMedida = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM unidad_medida WHERE IdUnidadMedida=@IdUnidadMedida"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    lConnection.Open()

                    Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                        Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                            lDTA.SelectCommand.CommandType = CommandType.Text
                            lDTA.SelectCommand.Transaction = lTransaction
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pIdUnidadMedida)

                            Dim lDT As New DataTable
                            lDTA.Fill(lDT)

                            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                                Dim lRow As DataRow = lDT.Rows(0)
                                Cargar(ObjUM, lRow)
                                lUnidadMedidaInMemory.Add(ObjUM.Clone())
                                Return ObjUM

                            End If

                        End Using

                        lTransaction.Commit()

                    End Using

                    lConnection.Close()

                End Using

            Else
                ObjUM = New clsBeUnidad_medida()
                ObjUM = lUnidadMedidaInMemory(IdxUnidadMedida).Clone()
                Return ObjUM
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdUnidadMedida As Integer,
                                    ByRef lConnection As SqlConnection,
                                    ByRef lTransaction As SqlTransaction) As clsBeUnidad_medida

        GetSingle = Nothing

        Try

            Dim ObjUM As New clsBeUnidad_medida()
            Dim IdxUnidadMedida As Integer = -1
            Dim vIdUM As Integer = pIdUnidadMedida
            IdxUnidadMedida = lUnidadMedidaInMemory.FindIndex(Function(x) x.IdUnidadMedida = vIdUM)

            If IdxUnidadMedida = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM unidad_medida WHERE IdUnidadMedida=@IdUnidadMedida"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Transaction = lTransaction
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pIdUnidadMedida)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                        Dim lRow As DataRow = lDT.Rows(0)
                        Cargar(ObjUM, lRow)
                        lUnidadMedidaInMemory.Add(ObjUM.Clone())
                        Return ObjUM
                    End If

                End Using

            Else
                ObjUM = New clsBeUnidad_medida()
                ObjUM = lUnidadMedidaInMemory(IdxUnidadMedida).Clone()
                Return ObjUM
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdUnidadMedida As Integer,
                                     ByVal pIdPropietario As Integer) As clsBeUnidad_medida

        GetSingle = Nothing

        Try

            Dim ObjUM As New clsBeUnidad_medida()
            Dim IdxUnidadMedida As Integer = -1
            Dim vIdUM As Integer = IdUnidadMedida
            IdxUnidadMedida = lUnidadMedidaInMemory.FindIndex(Function(x) x.IdUnidadMedida = vIdUM AndAlso x.IdPropietario = pIdPropietario)

            If IdxUnidadMedida = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM unidad_medida WHERE IdUnidadMedida=@IdUnidadMedida AND IdPropietario=@IdPropietario"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    lConnection.Open()

                    Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                        Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                            lDTA.SelectCommand.CommandType = CommandType.Text
                            lDTA.SelectCommand.Transaction = lTransaction
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", IdUnidadMedida)
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                            Dim lDT As New DataTable
                            lDTA.Fill(lDT)

                            If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                                Dim lRow As DataRow = lDT.Rows(0)
                                Cargar(ObjUM, lRow)
                                lUnidadMedidaInMemory.Add(ObjUM.Clone())
                                Return ObjUM

                            End If

                        End Using

                        lTransaction.Commit()

                    End Using

                    lConnection.Close()

                End Using

            Else
                ObjUM = New clsBeUnidad_medida()
                ObjUM = lUnidadMedidaInMemory(IdxUnidadMedida).Clone()
                Return ObjUM
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdUnidadMedida As Integer) As Boolean

        Try


            Dim IdxUnidadMedida As Integer = -1
            Dim vIdUM As Integer = pIdUnidadMedida
            IdxUnidadMedida = lUnidadMedidaInMemory.FindIndex(Function(x) x.IdUnidadMedida = vIdUM)

            If IdxUnidadMedida = -1 Then

                Dim vSQL As String = "SELECT COUNT(1) FROM unidad_medida WHERE IdUnidadMedida=@IdUnidadMedida"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    lConnection.Open()

                    Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                        Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                            lCommand.CommandType = CommandType.Text
                            lCommand.Parameters.AddWithValue("@IdUnidadMedida", pIdUnidadMedida)

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

    Public Shared Function Exists(ByVal pIdUnidadMedida As Integer,
                                  ByVal lConection As SqlConnection,
                                  ByVal lTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM unidad_medida WHERE IdUnidadMedida=@IdUnidadMedida"

            Using lCommand As New SqlCommand(vSQL, lConection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pIdUnidadMedida)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ExisteProductoLigado(ByVal pIdUnidadMedida As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM producto WHERE IdUnidadMedidaBasica=@IdUnidadMedidaBasica"

            'Validacion y estandarizacion de los datos
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdUnidadMedidaBasica", pIdUnidadMedida)

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

    Public Shared Sub Delete(ByVal pIdUnidadMedida As Integer)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim sp As String = String.Format("DELETE FROM unidad_medida WHERE IdUnidadMedida={0}", pIdUnidadMedida)

                    Using lCommand As New SqlCommand(sp, lConnection)
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

    Public Shared Sub DeleteByPropietario(ByVal pIdPropietario As Integer)

        Try

            Dim Sp As String = String.Format("DELETE FROM unidad_medida WHERE IdPropietario={0}", pIdPropietario)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(Sp, lConnection, lTransaction)
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

    Public Shared Function MaxIdUnidadMedida() As Integer

        Try

            MaxIdUnidadMedida = 1

            Dim vSQL As String = "SELECT MAX(IdUnidadMedida) + 1 as nuevo FROM unidad_medida"
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
                MaxIdUnidadMedida = IIf(IsDBNull(dt.Rows(0).Item("nuevo")), "1", dt.Rows(0).Item("nuevo"))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub GuardarTransaccion(ByVal pListObjUM As List(Of clsBeUnidad_medida))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()
            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Dim IdUnidadMedida = MaxID(lConnection, lTransaction) + 1

            For Each beUnidadMedida As clsBeUnidad_medida In pListObjUM
                beUnidadMedida.IdUnidadMedida = IdUnidadMedida
                Insertar(beUnidadMedida, lConnection, lTransaction)
                IdUnidadMedida = IdUnidadMedida + 1
            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pListObjUM"></param>
    ''' <remarks></remarks>
    Public Shared Sub Guardar_Transaccion(ByRef pListObjUM As List(Of clsBeUnidad_medida),
                                          ByRef lConnection As SqlConnection,
                                          ByRef lTransaction As SqlTransaction)

        Try
            Dim IdUnidadMedida = MaxID(lConnection, lTransaction) + 1

            For Each objUmbas As clsBeUnidad_medida In pListObjUM
                objUmbas.IdUnidadMedida = IdUnidadMedida
                Insertar(objUmbas, lConnection, lTransaction)
                IdUnidadMedida = IdUnidadMedida + 1
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub


    'Verificar si existe la unidad de medida
    Public Shared Function Existe_Unidad_Medida(ByVal pNombre As String) As clsBeUnidad_medida

        Existe_Unidad_Medida = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM unidad_medida " &
                   " Where(Nombre = @Nombre)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pNombre)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)

                        Dim ObjUM As New clsBeUnidad_medida()

                        Cargar(ObjUM, lRow)

                        Return ObjUM

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdUnidadMedida),0) FROM Unidad_medida"

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

    Public Shared Function Existe_By_Nombre(ByVal pNomUnidadMedida As String,
                                            ByRef Cnn As SqlConnection,
                                            ByRef pTransaction As SqlTransaction) As clsBeUnidad_medida

        Existe_By_Nombre = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM unidad_medida WHERE Nombre=@Nombre"

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pNomUnidadMedida)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeUnidad_medida()
                    Cargar(ObjUM, lRow)
                    Existe_By_Nombre = ObjUM

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_Nombre_By_IdPropietario(ByVal pNomUnidadMedida As String,
                                                             ByVal pIdPropietario As Integer,
                                                             ByRef Cnn As SqlConnection,
                                                             ByRef pTransaction As SqlTransaction) As clsBeUnidad_medida

        Existe_By_Nombre_By_IdPropietario = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM unidad_medida WHERE Nombre=@Nombre AND IdPropietario = @IdPropietario"

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pNomUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeUnidad_medida()
                    Cargar(ObjUM, lRow)
                    Existe_By_Nombre_By_IdPropietario = ObjUM
                    Return Existe_By_Nombre_By_IdPropietario
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_Codigo(ByVal pCodUnidadMedida As String,
                                            ByRef Cnn As SqlConnection,
                                            ByRef pTransaction As SqlTransaction) As clsBeUnidad_medida

        Existe_By_Codigo = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM unidad_medida WHERE Codigo=@Codigo "

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodUnidadMedida)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeUnidad_medida()

                    Cargar(ObjUM, lRow)

                    Existe_By_Codigo = ObjUM

                End If

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_Codigo(ByVal pCodUnidadMedida As String) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Existe_By_Codigo = False

        Try

            Dim vSQL As String = "SELECT * FROM unidad_medida WHERE Codigo=@Codigo"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodUnidadMedida)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeUnidad_medida()
                    Cargar(ObjUM, lRow)
                    Existe_By_Codigo = True

                End If

            End Using

            lTransaction.Commit()

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Existe_By_Codigo_And_IdPropietario(ByVal pCodUnidadMedida As String,
                                                              ByVal pIdPropietario As Integer,
                                                              ByRef Cnn As SqlConnection,
                                                              ByRef pTransaction As SqlTransaction) As clsBeUnidad_medida

        Existe_By_Codigo_And_IdPropietario = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM unidad_medida WHERE Codigo=@Codigo and IdPropietario=@IdPropietario"

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeUnidad_medida()

                    Cargar(ObjUM, lRow)

                    Existe_By_Codigo_And_IdPropietario = ObjUM

                End If

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function InsertarFromInterface(ByRef oBeUnidad_medida As clsBeUnidad_medida,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTrans As SqlTransaction) As Integer

        Try

            Ins.Init("unidad_medida")
            Ins.Add("idunidadmedida", "@idunidadmedida", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)

            Dim sp As String = Ins.SQL()

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text, .Transaction = lTrans}

            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeUnidad_medida.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeUnidad_medida.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeUnidad_medida.Nombre))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeUnidad_medida.Codigo))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeUnidad_medida.Activo))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeUnidad_medida.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeUnidad_medida.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeUnidad_medida.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeUnidad_medida.User_agr))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub Insert_Multiple(ByVal pListObjUM As List(Of clsBeUnidad_medida))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            For Each Obj As clsBeUnidad_medida In pListObjUM
                Insertar(Obj, lConnection, lTransaction)
            Next

            lTransaction.Commit()

        Catch ex As Exception
            lTransaction.Rollback()
            Throw ex
        Finally
            lConnection.Close()
        End Try

    End Sub

    Public Shared Function Obtener(ByRef oBeUnidad_medida As clsBeUnidad_medida,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Dim IdxUnidadMedida As Integer = -1
            Dim vIdUM As Integer = oBeUnidad_medida.IdUnidadMedida
            IdxUnidadMedida = lUnidadMedidaInMemory.FindIndex(Function(x) x.IdUnidadMedida = vIdUM)

            If IdxUnidadMedida = -1 Then

                Const sp As String = "SELECT * FROM Unidad_medida 
                                      Where(IdUnidadMedida = @IdUnidadMedida)"

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDA", oBeUnidad_medida.IdUnidadMedida))
                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    oBeUnidad_medida = New clsBeUnidad_medida()
                    Cargar(oBeUnidad_medida, dt.Rows(0))
                    lUnidadMedidaInMemory.Add(oBeUnidad_medida.Clone())
                End If

            Else
                oBeUnidad_medida = New clsBeUnidad_medida()
                oBeUnidad_medida = lUnidadMedidaInMemory(IdxUnidadMedida).Clone()
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Unidad_Medida_By_Nombre(ByVal pNombreUmbas As String,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As clsBeUnidad_medida

        Get_Unidad_Medida_By_Nombre = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM unidad_medida WHERE Nombre=@Nombre"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pNombreUmbas)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeUnidad_medida()
                    Cargar(ObjUM, lRow)
                    Return ObjUM
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Nombre_By_IdUnidadMedida(ByVal pIdUnidadMedida As Integer) As String

        Get_Nombre_By_IdUnidadMedida = ""

        Try

            Dim vSQL As String = "SELECT Nombre FROM unidad_medida WHERE IdUnidadMedida=@IdUnidadMedida"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pIdUnidadMedida)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)

                        Return IIf(IsDBNull(lRow("Nombre")), "", lRow("Nombre"))

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Nombre_By_IdUnidadMedida(ByVal pIdUnidadMedida As Integer,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As String

        Get_Nombre_By_IdUnidadMedida = ""

        Try

            Dim vSQL As String = "SELECT Nombre FROM unidad_medida WHERE IdUnidadMedida=@IdUnidadMedida"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pIdUnidadMedida)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Return IIf(IsDBNull(lRow("Nombre")), "", lRow("Nombre"))

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Codigo_By_IdUnidadMedida(ByVal pIdUnidadMedida As Integer,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As String

        Get_Codigo_By_IdUnidadMedida = ""

        Try

            Dim vSQL As String = "SELECT Codigo FROM unidad_medida WHERE IdUnidadMedida=@IdUnidadMedida"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pIdUnidadMedida)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Return IIf(IsDBNull(lRow("Codigo")), "", lRow("Codigo"))

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetUnidadMedidaByNombre(ByVal pNombre As String) As clsBeUnidad_medida

        GetUnidadMedidaByNombre = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM unidad_medida WHERE Nombre=@Nombre"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@Nombre", pNombre)

                    Dim lDT As New DataTable
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim ObjUM As New clsBeUnidad_medida()

                        Cargar(ObjUM, lRow)

                        Return ObjUM

                    End If

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Listar_By_IdPropietario_Bodega(pIdPropietarioBodega As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try
            Dim vSQL As String = "SELECT unidad_medida.IdUnidadMedida, unidad_medida.Nombre 
                    FROM  unidad_medida inner join 
                    propietario_bodega on propietario_bodega.IdPropietario = unidad_medida.IdPropietario
                    WHERE  (propietario_bodega.IdPropietarioBodega = @IdPropietarioBodega)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'GT 10052021 todas las unidades de medida para inv. multiempresa
    Public Shared Function Listar_By_IdPropietario_Bodega() As DataTable

        Dim lTable As New DataTable("Result")

        Try
            Dim vSQL As String = "SELECT unidad_medida.IdUnidadMedida, unidad_medida.Nombre 
                    FROM  dbo.unidad_medida inner join 
                    propietario_bodega on propietario_bodega.IdPropietario = unidad_medida.IdPropietario"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.Fill(lTable)
                End Using
            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_By_IdPropietario(ByVal pActivo As Boolean,
                                                    ByVal pIdPropietario As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As DataTable
        Dim lDataTable As New DataTable("UMBas")

        Try

            Dim vSQL As String = ""

            If pIdPropietario = 0 Then
                vSQL = "SELECT IdUnidadMedida, Nombre  FROM VW_UnidadMedida WHERE 1 > 0 "
            Else
                vSQL = String.Format("SELECT IdUnidadMedida, Nombre FROM VW_UnidadMedida WHERE IdPropietario={0}", pIdPropietario)
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

    Public Shared Function Get_UMBas_ByIdUMBas(ByVal IdUnidadMedida As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As DataTable

        Get_UMBas_ByIdUMBas = Nothing

        Try

            Dim vSQL As String = "SELECT IdUnidadMedida,Nombre FROM unidad_medida WHERE IdUnidadMedida=@IdUnidadMedida"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", IdUnidadMedida)

                Dim lDT As New DataTable("UMBas")
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Return lDT

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_UMBas_Default_By_IdPropietario(ByVal IdPropietario As Integer) As Integer

        Get_UMBas_Default_By_IdPropietario = 0

        Try

            Dim vSQL As String = "SELECT IdUnidadMedida,Nombre FROM unidad_medida WHERE IdPropietario=@IdPropietario AND Codigo = 'UN'"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)

                        Dim lDT As New DataTable("UMBas")
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Get_UMBas_Default_By_IdPropietario = lDT.Rows(0).Item("IdUnidadMedida")
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

    Public Shared Function Get_Unidad_Medida_By_Codigo(ByVal pCodigo As String,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As clsBeUnidad_medida

        Get_Unidad_Medida_By_Codigo = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM unidad_medida WHERE Codigo=@Codigo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeUnidadMedida As New clsBeUnidad_medida()
                    Cargar(BeUnidadMedida, lRow)
                    Return BeUnidadMedida
                End If

            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function


    Public Shared Function Get_Unidad_Medida_By_IdUnidadMedida(ByVal pIdUnidadMedida As Integer) As clsBeUnidad_medida


        Get_Unidad_Medida_By_IdUnidadMedida = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM unidad_medida WHERE IdUnidadMedida=@pIdUnidadMedida"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdUnidadMedida", pIdUnidadMedida)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim BeUnidadMedida As New clsBeUnidad_medida()
                            Cargar(BeUnidadMedida, lRow)

                            Get_Unidad_Medida_By_IdUnidadMedida = BeUnidadMedida
                        End If

                    End Using

                End Using


            End Using


        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario(ByVal pIdPropietario As Integer) As DataTable

        Dim lDataTable As New DataTable("UMBas")

        Get_All_By_IdPropietario = Nothing

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT IdUnidadMedida, Codigo, Nombre  FROM VW_UnidadMedida WHERE 1 > 0 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.Fill(lDataTable)
                        Get_All_By_IdPropietario = lDataTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_IdUnidadMedida_By_Codigo(ByVal pCodigo As String) As Integer


        Get_IdUnidadMedida_By_Codigo = Nothing

        Try

            Dim vSQL As String = "SELECT IdUnidadMedida FROM unidad_medida WHERE codigo=@codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigo)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Get_IdUnidadMedida_By_Codigo = lDT.Rows(0).Item("IdUnidadMedida")
                        End If

                    End Using

                End Using

            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function Get_IdUnidadMedida_By_Codigo(ByVal pCodigo As String, lConnection As SqlConnection, lTransaction As SqlTransaction) As Integer


        Get_IdUnidadMedida_By_Codigo = Nothing

        Try

            Dim vSQL As String = "SELECT IdUnidadMedida FROM unidad_medida WHERE codigo=@codigo "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigo)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Get_IdUnidadMedida_By_Codigo = lDT.Rows(0).Item("IdUnidadMedida")
                End If

            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function


    Public Shared Function Existe_By_Nombre(ByVal pNombreUmbas As String, ByVal pIdPropietario As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Existe_By_Nombre = False

        Try

            Dim vSQL As String = "SELECT top 1 * FROM unidad_medida WHERE (Nombre=@Nombre and IdPropietario=@IdPropietario)"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@NOMBRE", pNombreUmbas)
                lDTA.SelectCommand.Parameters.AddWithValue("@IDPROPIETARIO", pIdPropietario)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeUnidad_medida()
                    Cargar(ObjUM, lRow)
                    Existe_By_Nombre = True

                End If

            End Using

            lTransaction.Commit()

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    '#GT11062025: umbas para enviar a la nube con transaccion
    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer, ByRef lConnection As SqlConnection,
                                                                                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeUnidad_medida)

        Dim lReturnList As New List(Of clsBeUnidad_medida)

        Try

            Dim vSQL As String = String.Empty

            If pIdPropietario = 0 Then
                vSQL = "SELECT * FROM VW_UnidadMedida WHERE 1 > 0 "
            Else
                vSQL = "SELECT * FROM VW_UnidadMedida WHERE IdPropietario=@IdPropietario "
            End If

            If pActivo Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                If pIdPropietario <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeUnidad_medida

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeUnidad_medida

                        Cargar(Obj, lRow)

                        If lRow("IdPropietario") IsNot DBNull.Value AndAlso lRow("IdPropietario") IsNot Nothing Then
                            Obj.Propietario = New clsBePropietarios
                            Obj.Propietario.IdPropietario = CType(lRow("IdPropietario"), Integer)
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

    Public Shared Function Get_All_By_IdPropietario_And_Activo(ByVal pIdPropietario As Integer) As DataTable

        Dim lDataTable As New DataTable("UMBas")

        Get_All_By_IdPropietario_And_Activo = Nothing

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT IdUnidadMedida, Codigo, Nombre  FROM VW_UnidadMedida WHERE es_um_cobro=0 and activo=1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.Fill(lDataTable)
                        Get_All_By_IdPropietario_And_Activo = lDataTable
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
