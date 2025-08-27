Imports System.Reflection
Imports System.Data.SqlClient
Imports DevExpress.DataAccess.Native
Imports System.Data.Common
Imports DevExpress.Xpo.Helpers

Partial Public Class clsLnPropietarios
    Implements IDisposable

    Public Shared Function GetSingle(ByVal pIdPropietario As Integer) As clsBePropietarios

        GetSingle = Nothing

        Try

            Dim Obj As New clsBePropietarios()

            Dim vSQL As String = "SELECT TOP 1 * FROM propietarios WHERE IdPropietario=@IdPropietario"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Obj = New clsBePropietarios()
                            Cargar(Obj, lRow)
                            GetSingle = Obj

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdPropietario As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBePropietarios

        GetSingle = Nothing

        Try
            Dim Obj As New clsBePropietarios()
            Dim IdxPropietario As Integer = -1
            Dim vIdPropietario As Integer = pIdPropietario

            IdxPropietario = lPropietariosInMemory.FindIndex(Function(x) x.IdPropietario = vIdPropietario)

            If IdxPropietario = -1 Then

                Dim vSQL As String = "SELECT TOP 1 * FROM propietarios WHERE IdPropietario=@IdPropietario"

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Transaction = lTransaction

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Obj = New clsBePropietarios()
                        Cargar(Obj, lRow)
                        lPropietariosInMemory.Add(Obj.Clone())
                        Return Obj

                    End If

                End Using
            Else
                Obj = New clsBePropietarios()
                Obj = lPropietariosInMemory(IdxPropietario).Clone()
                Return Obj
            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBePropietarios)

        Dim lReturnList As New List(Of clsBePropietarios)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * FROM VW_Propietario WHERE 1 > 0 "

                If pActivo = True Then
                    vSQL += " AND Activo=1"
                Else
                    vSQL += " AND Activo=0"
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBePropietarios

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBePropietarios
                            Obj.Empresa = New clsBeEmpresa
                            Cargar(Obj, lRow)

                            If lRow("IdEmpresa") IsNot DBNull.Value AndAlso lRow("IdEmpresa") IsNot Nothing Then
                                Obj.Empresa.Nombre = CType(lRow("Empresa"), String)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Exists(ByVal pIdPropietario As Integer) As Boolean

        Try

            Dim IdxPropietario As Integer = -1
            Dim vIdPropietario As Integer = pIdPropietario

            IdxPropietario = lPropietariosInMemory.FindIndex(Function(x) x.IdPropietario = vIdPropietario)

            If IdxPropietario = -1 Then

                Dim lExists As Boolean = False

                Dim vSQL As String = "SELECT COUNT(1) FROM propietarios WHERE IdPropietario=@IdPropietario"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    lConnection.Open()

                    Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                        Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                            lCommand.CommandType = CommandType.Text
                            lCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

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

            Else
                Return True
            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function ActualizarDatos(ByVal pObjP As clsBePropietarios,
                                           ByVal pListObjP As List(Of clsBePropietario_bodega),
                                           ByVal pListDestinatarios As List(Of clsBePropietario_destinatario)) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim ObjLnDestinatario As New clsLnPropietario_destinatario()

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Actualizar(pObjP, lConnection, lTransaction)

            Dim lMax As Integer = clsLnPropietario_bodega.MaxID()
            For Each Obj As clsBePropietario_bodega In pListObjP
                If Obj.IdPropietarioBodega = 0 Then
                    lMax += 1
                    Obj.IdPropietarioBodega = lMax
                    clsLnPropietario_bodega.Insertar(Obj, lConnection, lTransaction)
                Else
                    clsLnPropietario_bodega.Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            Dim lMaxDest As Integer = clsLnPropietario_destinatario.MaxID()
            For Each OBj As clsBePropietario_destinatario In pListDestinatarios
                If OBj.IdPropietario = 0 Then
                    lMaxDest += 1
                    OBj.IdDestinatarioPropietario = lMaxDest
                    OBj.IdPropietario = pObjP.IdPropietario
                    ObjLnDestinatario.Insertar(OBj, lConnection, lTransaction)
                Else
                    ObjLnDestinatario.Actualizar(OBj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(ByVal pIdBodega As Integer, ByVal pIdPropietario As Integer) As Integer

        Try

            Dim vSQL As String = "SELECT IdPropietarioBodega FROM propietario_bodega 
                                     WHERE IdPropietario=@IdPropietario 
                                     AND IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Return lDT.Rows(0).Item("IdPropietarioBodega")
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Nothing

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(ByVal pIdBodega As Integer,
                                                                                 ByVal pIdPropietario As Integer,
                                                                                 ByRef lConnection As SqlConnection,
                                                                                 ByRef lTrans As SqlTransaction) As Integer

        Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario = 0

        Try

            Dim vSQL As String = "SELECT IdPropietarioBodega FROM propietario_bodega " &
            " WHERE IdPropietario=@IdPropietario " &
            " AND IdBodega = @IdBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTrans
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Return lDT.Rows(0).Item("IdPropietarioBodega")
                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_IdPropietario(ByVal pIdBodega As Integer,
                                             ByVal IdPropietarioBodega As Integer) As Integer

        Get_IdPropietario = Nothing

        Try

            Dim vSQL As String = "SELECT IdPropietario FROM propietario_bodega 
                                  WHERE IdPropietarioBodega=@IdPropietarioBodega 
                                  AND IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Return lDT.Rows(0).Item("IdPropietario")
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_IdPropietario_By_IdBodega_By_IdProducto(ByVal pIdBodega As Integer,
                                                                       ByVal pIdProducto As Integer) As Integer

        Get_IdPropietario_By_IdBodega_By_IdProducto = Nothing

        Try

            Dim vSQL As String = "SELECT propietario_bodega.IdPropietario, propietario_bodega.IdBodega
                                  FROM propietario_bodega inner join producto ON propietario_bodega.IdPropietario = producto.IdPropietario
                                  WHERE IdProducto=@IdProducto
                                       AND IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Return lDT.Rows(0).Item("IdPropietario")
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_IdPropietario(ByVal pIdBodega As Integer,
                                             ByVal IdPropietarioBodega As Integer,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction) As Integer

        Get_IdPropietario = Nothing

        Try

            Dim vSQL As String = "SELECT IdPropietario FROM propietario_bodega 
                    WHERE IdPropietarioBodega=@IdPropietarioBodega 
                    AND IdBodega = @IdBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                    Return lDT.Rows(0).Item("IdPropietario")
                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub CargarHH(ByRef oBePropietarios As clsBePropietarios, ByRef dr As DataRow)

        Try

            With oBePropietarios

                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdTipoActualizacionCosto = IIf(IsDBNull(dr.Item("IdTipoActualizacionCosto")), 0, dr.Item("IdTipoActualizacionCosto"))
                .Contacto = IIf(IsDBNull(dr.Item("contacto")), "", dr.Item("contacto"))
                .Nombre_comercial = IIf(IsDBNull(dr.Item("nombre_comercial")), "", dr.Item("nombre_comercial"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Email = IIf(IsDBNull(dr.Item("email")), "", dr.Item("email"))
                .Actualiza_costo_oc = IIf(IsDBNull(dr.Item("actualiza_costo_oc")), False, dr.Item("actualiza_costo_oc"))
                .Color = IIf(IsDBNull(dr.Item("Color")), 0, dr.Item("Color"))
                .Empresa = Nothing
                .Imagen = Nothing
            End With

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function Obtener(ByRef oBePropietarios As clsBePropietarios,
                                   Optional ByVal EsParaHH As Boolean = False) As Boolean

        Try

            Dim sp As String = "SELECT * FROM Propietarios" &
            " Where(IdPropietario = @IdPropietario)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBePropietarios.IdPropietario))

            Dim dt As New DataTable
            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then

                If EsParaHH Then
                    CargarHH(oBePropietarios, dt.Rows(0))
                Else
                    Cargar(oBePropietarios, dt.Rows(0))
                End If

            End If

            Return True

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBePropietarios As clsBePropietarios,
                                   ByVal EsParaHH As Boolean,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Dim sp As String = "SELECT * FROM Propietarios 
                                Where(IdPropietario = @IdPropietario)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBePropietarios.IdPropietario))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then

                If EsParaHH Then
                    CargarHH(oBePropietarios, dt.Rows(0))
                Else
                    Cargar(oBePropietarios, dt.Rows(0))
                End If

                Obtener = True

            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_Class(ByVal pIdEmpresa As Integer) As List(Of clsBePropietarios)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBePropietarios)
            Const sp As String = "SELECT * FROM Propietarios WHERE IdEmpresa = @IdEmpresa AND Activo = 1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBePropietarios As New clsBePropietarios

            For Each dr As DataRow In dt.Rows

                vBePropietarios = New clsBePropietarios
                Cargar(vBePropietarios, dr)
                lReturnList.Add(vBePropietarios)

            Next

            cmd.Dispose()

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa_For_Combo(ByVal pIdEmpresa As Integer) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT IdPropietario, codigo as Código, nombre_comercial as Nombre FROM Propietarios WHERE IdEmpresa = @IdEmpresa AND Activo = 1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
            Dim dt As New DataTable

            dad.Fill(dt)

            cmd.Dispose()

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBePropietarios)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBePropietarios)

            Dim vSQL As String = "SELECT propietarios.*, 
                         propietario_bodega.IdBodega 
                         FROM propietarios INNER JOIN
                         propietario_bodega ON 
                         propietarios.IdPropietario = propietario_bodega.IdPropietario 
                         WHERE propietario_bodega.IdBodega = @IdBodega 
                         AND propietario_bodega.Activo = 1 
                         AND propietarios.Activo= 1"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBePropietarios As New clsBePropietarios

            For Each dr As DataRow In dt.Rows

                vBePropietarios = New clsBePropietarios
                Cargar(vBePropietarios, dr)
                lReturnList.Add(vBePropietarios)

            Next

            cmd.Dispose()

            lTransaction.Commit()

            Return lReturnList


        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBePropietarios As clsBePropietarios,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Dim IdxPropietario As Integer = -1
            Dim vIdPropietario As Integer = oBePropietarios.IdPropietario

            IdxPropietario = lPropietariosInMemory.FindIndex(Function(x) x.IdPropietario = vIdPropietario)

            If IdxPropietario = -1 Then

                Const sp As String = "SELECT * FROM Propietarios 
                                      Where(IdPropietario = @IdPropietario)"

                'Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                'Dim dad As New SqlDataAdapter(cmd)

                'dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBePropietarios.IdPropietario))

                'Dim dt As New DataTable
                'dad.Fill(dt)

                'If dt.Rows.Count = 1 Then
                '    oBePropietarios = New clsBePropietarios()
                '    CargarSinImagen(oBePropietarios, dt.Rows(0))
                '    lPropietariosInMemory.Add(oBePropietarios.Clone())
                '    Obtener = True
                'End If


                Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                    lCommand.Parameters.AddWithValue("@IDPROPIETARIO", oBePropietarios.IdPropietario)
                    Dim dt As New DataTable
                    Dim dad As New SqlDataAdapter(lCommand)
                    dad.Fill(dt)

                    If dt.Rows.Count = 1 Then
                        oBePropietarios = New clsBePropietarios()
                        CargarSinImagen(oBePropietarios, dt.Rows(0))
                        lPropietariosInMemory.Add(oBePropietarios.Clone())
                        Obtener = True
                    End If

                End Using

            Else
                oBePropietarios = lPropietariosInMemory(IdxPropietario).Clone()
            End If

        Catch ex1 As SqlException
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex1.Message))
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Existe(pCodigoPropietario As String,
                                  lConnection As SqlConnection,
                                  lTransaction As SqlTransaction) As clsBePropietarios

        Existe = Nothing

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT * FROM propietarios WHERE codigo=@codigo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigoPropietario)

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim BePropietario As New clsBePropietarios()
                    Dim lRow As DataRow = lDT.Rows(0)
                    Cargar(BePropietario, lRow)
                    Return BePropietario

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function ObtenerSinImagen(ByRef oBePropietarios As clsBePropietarios,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As Boolean

        ObtenerSinImagen = False

        Try

            Dim IdxPropietario As Integer = -1
            Dim vIdPropietario As Integer = oBePropietarios.IdPropietario

            IdxPropietario = lPropietariosInMemory.FindIndex(Function(x) x.IdPropietario = vIdPropietario)

            If IdxPropietario = -1 Then

                Const sp As String = "SELECT * FROM Propietarios 
                                      Where(IdPropietario = @IdPropietario)"

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)

                dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBePropietarios.IdPropietario))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    oBePropietarios = New clsBePropietarios()
                    CargarSinImagen(oBePropietarios, dt.Rows(0))
                    lPropietariosInMemory.Add(oBePropietarios.Clone())
                    ObtenerSinImagen = True
                End If

            Else
                oBePropietarios = lPropietariosInMemory(IdxPropietario).Clone()
            End If

        Catch ex1 As SqlException
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex1.Message))
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Single_By_IdEmpresa(ByVal IdEmpresa As Integer,
                                                   ByVal IdPropietario As Integer) As clsBePropietarios

        Get_Single_By_IdEmpresa = Nothing

        Try

            Dim IdxPropietario As Integer = -1
            Dim BePropietario As New clsBePropietarios()

            IdxPropietario = lPropietariosInMemory.FindIndex(Function(x) x.IdPropietario = IdPropietario AndAlso x.IdEmpresa = IdEmpresa)

            If IdxPropietario = -1 Then

                Dim vSQL As String = "SELECT * from propietarios WHERE IdEmpresa=@IdEmpresa AND IdPropietario=@IdPropietario AND Activo=1"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            BePropietario = New clsBePropietarios()
                            Dim lRow As DataRow = lDT.Rows(0)
                            Cargar(BePropietario, lRow)
                            lPropietariosInMemory.Add(BePropietario.Clone())
                            'Return BePropietario
                            Get_Single_By_IdEmpresa = BePropietario

                        End If

                    End Using

                End Using

            Else
                BePropietario = New clsBePropietarios()
                BePropietario = lPropietariosInMemory(IdxPropietario).Clone()
                Get_Single_By_IdEmpresa = BePropietario
            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_IdPropietario_By_Codigo(ByVal Codigo_Propietario As String) As Integer

        Get_IdPropietario_By_Codigo = 0

        Try

            Dim IdxPropietario As Integer = -1

            IdxPropietario = lPropietariosInMemory.FindIndex(Function(x) x.Codigo = Codigo_Propietario)

            If IdxPropietario = -1 Then

                Const sp As String = "SELECT IdPropietario FROM propietarios 
            Where (Codigo = @Codigo) "

                Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", Codigo_Propietario))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    Get_IdPropietario_By_Codigo = IIf(IsDBNull(dt.Rows(0).Item("IdPropietario")), 0, dt.Rows(0).Item("IdPropietario"))
                End If

            Else
                Return lPropietariosInMemory(IdxPropietario).IdPropietario
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_IdPropietario_By_Codigo(ByVal Codigo_Propietario As String,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As Integer

        Get_IdPropietario_By_Codigo = 0

        Try

            Dim IdxPropietario As Integer = -1

            IdxPropietario = lPropietariosInMemory.FindIndex(Function(x) x.Codigo = Codigo_Propietario)

            If IdxPropietario = -1 Then

                Const sp As String = "SELECT IdPropietario FROM propietarios 
                                      Where (Codigo = @Codigo) "

                Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dad As New SqlDataAdapter(cmd)
                dad.SelectCommand.Parameters.Add(New SqlParameter("@CODIGO", Codigo_Propietario))

                Dim dt As New DataTable
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    Get_IdPropietario_By_Codigo = IIf(IsDBNull(dt.Rows(0).Item("IdPropietario")), 0, dt.Rows(0).Item("IdPropietario"))
                End If

            Else
                Return lPropietariosInMemory(IdxPropietario).IdPropietario
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa(ByVal pIdEmpresa As Integer,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As DataTable

        Dim lDataTable As New DataTable("Clasificacion")

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT IdPropietario, nombre_comercial as Nombre  FROM Propietarios WHERE IdEmpresa=@IdEmpresa AND ACTIVO=1"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                lDTA.Fill(lDataTable)
            End Using

            Return lDataTable

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdEmpresa(ByVal pIdEmpresa As Integer) As DataTable

        Dim lDataTable As New DataTable("Clasificacion")

        Try

            Dim vSQL As String = "SELECT IdPropietario, nombre_comercial as Nombre  
            FROM Propietarios WHERE IdEmpresa=@IdEmpresa AND ACTIVO=1"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                        lDTA.Fill(lDataTable)
                    End Using

                End Using

                lConnection.Close()

            End Using

            Return lDataTable

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Guardar_Nuevo_Propietario(ByRef oBePropietarios As clsBePropietarios, ByVal pIdBodegaPorDefecto As Integer) As Integer

        Guardar_Nuevo_Propietario = 0

        Dim vResult As Integer = 0
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            vResult = Insertar_Nuevo_Propietario(oBePropietarios, pIdBodegaPorDefecto, False, lConnection, lTransaction)

            lTransaction.Commit()

            Guardar_Nuevo_Propietario = vResult

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Insertar_Nuevo_Propietario(ByRef oBePropietarios As clsBePropietarios,
                                                      ByVal pIdBodegaPorDefecto As Integer,
                                                      ByVal Es_Ejecucion_Interface As Boolean,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As Integer

        Insertar_Nuevo_Propietario = 0

        Dim vResult As Integer = 0
        Dim CodigoPropietarioOriginal As Integer = 0

        Try

            Try

                vResult = Insertar(oBePropietarios, lConnection, lTransaction)

            Catch ex As Exception
                Throw New Exception("Error al insertar propietario: " & oBePropietarios.Nombre_comercial & " " & ex.Message)
            End Try


            Dim BeUnidadMedidaDefecto As New clsBeUnidad_medida
            BeUnidadMedidaDefecto.IdUnidadMedida = clsLnUnidad_medida.MaxID(lConnection, lTransaction) + 1
            BeUnidadMedidaDefecto.IdPropietario = oBePropietarios.IdPropietario
            BeUnidadMedidaDefecto.Codigo = "UN"
            BeUnidadMedidaDefecto.Nombre = "UN"
            BeUnidadMedidaDefecto.Activo = True
            BeUnidadMedidaDefecto.Fec_agr = Now
            BeUnidadMedidaDefecto.Fec_mod = Now
            BeUnidadMedidaDefecto.User_agr = oBePropietarios.User_agr
            BeUnidadMedidaDefecto.User_mod = oBePropietarios.User_mod
            vResult += clsLnUnidad_medida.Insertar(BeUnidadMedidaDefecto, lConnection, lTransaction)

            Dim BeProductoClas As New clsBeProducto_clasificacion
            BeProductoClas.IdClasificacion = clsLnProducto_clasificacion.MaxId(lConnection, lTransaction) + 1
            BeProductoClas.Nombre = "N/D"
            BeProductoClas.Propietario.IdPropietario = oBePropietarios.IdPropietario
            BeProductoClas.Fec_agr = Now
            BeProductoClas.Fec_mod = Now
            BeProductoClas.User_agr = oBePropietarios.User_agr
            BeProductoClas.User_mod = oBePropietarios.User_mod
            BeProductoClas.Sistema = True
            vResult += clsLnProducto_clasificacion.Insertar(BeProductoClas, lConnection, lTransaction)

            Dim BeProductoTipo As New clsBeProducto_tipo
            BeProductoTipo.IdTipoProducto = clsLnProducto_tipo.MaxID(lConnection, lTransaction) + 1
            BeProductoTipo.NombreTipoProducto = "N/D"
            BeProductoTipo.Propietario.IdPropietario = oBePropietarios.IdPropietario
            BeProductoTipo.IdPropietario = oBePropietarios.IdPropietario
            BeProductoTipo.Fec_agr = Now
            BeProductoTipo.Fec_mod = Now
            BeProductoTipo.User_agr = oBePropietarios.User_agr
            BeProductoTipo.User_mod = oBePropietarios.User_mod
            vResult += clsLnProducto_tipo.Insertar(BeProductoTipo, lConnection, lTransaction)

            Dim BeProductoEstado As New clsBeProducto_estado
            BeProductoEstado.IdEstado = clsLnProducto_estado.MaxID(lConnection, lTransaction) + 1
            BeProductoEstado.IdPropietario = oBePropietarios.IdPropietario
            BeProductoEstado.Nombre = "Buen Estado"
            BeProductoEstado.IdUbicacionDefecto = 0
            BeProductoEstado.Utilizable = True
            BeProductoEstado.Activo = True
            BeProductoEstado.Fec_agr = Now
            BeProductoEstado.Fec_mod = Now
            BeProductoEstado.User_agr = oBePropietarios.User_agr
            BeProductoEstado.User_mod = oBePropietarios.User_mod
            BeProductoEstado.Dañado = False
            BeProductoEstado.Sistema = False
            clsLnProducto_estado.Insertar(BeProductoEstado, lConnection, lTransaction)

            Dim BeProveedor As New clsBeProveedor()
            BeProveedor.IdEmpresa = oBePropietarios.IdEmpresa
            BeProveedor.IdPropietario = oBePropietarios.IdPropietario

            '#CKFK 20210312 Modifiqué esto para que el IdProveedor sea el mismo IdPropietario
            If Es_Ejecucion_Interface Then
                BeProveedor.IdProveedor = oBePropietarios.Codigo ' clsLnProveedor.MaxID(lConnection, lTransaction) + 1
                CodigoPropietarioOriginal = oBePropietarios.Codigo
            Else
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1
            End If

            'BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1
            BeProveedor.Codigo = oBePropietarios.Codigo
            BeProveedor.Nombre = oBePropietarios.Nombre_comercial
            BeProveedor.Telefono = oBePropietarios.Telefono
            BeProveedor.Nit = oBePropietarios.NIT
            BeProveedor.Direccion = oBePropietarios.Direccion
            BeProveedor.Email = oBePropietarios.Email
            BeProveedor.Contacto = oBePropietarios.Nombre_comercial
            BeProveedor.Activo = True
            BeProveedor.Muestra_precio = False
            BeProveedor.Fec_agr = Now
            BeProveedor.Fec_mod = Now
            BeProveedor.User_agr = oBePropietarios.User_agr
            BeProveedor.User_mod = oBePropietarios.User_mod
            BeProveedor.Actualiza_costo_oc = False
            BeProveedor.IdUbicacionVirtual = 0
            BeProveedor.Es_Bodega_Recepcion = False
            BeProveedor.Es_Bodega_Traslado = False
            BeProveedor.Referencia = "MI3_" & oBePropietarios.Codigo
            BeProveedor.Sistema = True
            BeProveedor.IdConfiguracionBarraPallet = 0

            Try
                '#GT22062022_1421: si se usa codigoPropietario como idProveedor y existe por usar interface, cambiamos a MaxId
                'Carol ya lo valido en la linea 1105, pero no aplica a cealsa porque el idpropietario no siempre hace match
                If Es_Ejecucion_Interface Then

                    Dim existeProveedor = clsLnProveedor.Obtener(BeProveedor, lConnection, lTransaction)

                    If existeProveedor Then
                        'GT22082022_1127: si proveedor ya existe con el idProveedor original, le asignamos el correlativo
                        BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1
                        vResult += clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)
                    Else
                        vResult += clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)
                    End If

                Else

                    Dim existeProveedor = clsLnProveedor.Obtener(BeProveedor, lConnection, lTransaction)

                    If existeProveedor Then
                        'GT22082022_1127: si proveedor ya existe con el idProveedor original, le asignamos el correlativo
                        BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1
                        vResult += clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)
                    Else
                        vResult += clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)
                    End If

                End If

            Catch ex As Exception
                Throw New Exception("Error al insertar al proveedor: " & oBePropietarios.Nombre_comercial & " " & ex.Message)
            End Try

            Dim BeProveedorBodega As New clsBeProveedor_bodega()
            Dim BeProveedorBodegaExistente As New clsBeProveedor_bodega()
            BeProveedorBodega = New clsBeProveedor_bodega

            '#CKFK 20210312 Modifiqué esto para que el IdAsignacion que es el IdProveedorBodega sea el mismo IdProveedor
            If Es_Ejecucion_Interface Then
                BeProveedorBodega.IdAsignacion = BeProveedor.IdProveedor 'clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
            Else
                BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
            End If
            BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
            BeProveedorBodega.IdBodega = pIdBodegaPorDefecto
            BeProveedorBodega.Activo = True
            BeProveedorBodega.User_agr = oBePropietarios.User_agr
            BeProveedorBodega.User_mod = oBePropietarios.User_agr
            BeProveedorBodega.Fec_agr = Now
            BeProveedorBodega.Fec_mod = Now
            BeProveedorBodegaExistente = BeProveedorBodega
            clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdProveedor(BeProveedorBodegaExistente, lConnection, lTransaction)

            If BeProveedorBodegaExistente Is Nothing Then

                If clsLnProveedor_bodega.Existe_Id(BeProveedorBodega.IdAsignacion, lConnection, lTransaction) Then
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                End If

                clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, lConnection, lTransaction)

            End If

            Dim BeClienteTipo As New clsBeCliente_tipo
            BeClienteTipo.IdTipoCliente = clsLnCliente_tipo.MaxID(lConnection, lTransaction) + 1
            BeClienteTipo.NombreTipoCliente = "N/D"
            BeClienteTipo.IdPropietario = oBePropietarios.IdPropietario
            BeClienteTipo.Fec_agr = Now
            BeClienteTipo.Fec_mod = Now
            BeClienteTipo.User_agr = oBePropietarios.User_agr
            BeClienteTipo.User_mod = oBePropietarios.User_mod
            vResult += clsLnCliente_tipo.Insertar(BeClienteTipo, lConnection, lTransaction)

            '#CKFK 20210311 Se inserta el cliente cuando se llama la Interface de Cealsa
            If Es_Ejecucion_Interface Then

                Dim BeCliente As New clsBeCliente
                BeCliente.IdEmpresa = oBePropietarios.IdEmpresa
                BeCliente.IdPropietario = oBePropietarios.IdPropietario
                '#CKFK 20210312 Modifiqué esto para que el IdCliente sea el IdPropietario
                BeCliente.IdCliente = oBePropietarios.IdPropietario 'clsLnCliente.MaxID(lConnection, lTransaction) + 1
                BeCliente.Codigo = oBePropietarios.Codigo
                BeCliente.Nombre_comercial = oBePropietarios.Nombre_comercial.Trim()
                BeCliente.Telefono = ""
                BeCliente.Nit = oBePropietarios.NIT
                BeCliente.Direccion = ""
                BeCliente.Control_Ultimo_Lote = False
                BeCliente.Es_bodega_recepcion = False
                BeCliente.Es_Bodega_Traslado = False
                BeCliente.Realiza_manufactura = False
                BeCliente.Sistema = False
                BeCliente.IdTipoCliente = BeClienteTipo.IdTipoCliente
                BeCliente.Activo = True
                BeCliente.User_agr = oBePropietarios.User_agr
                BeCliente.Fec_agr = Date.UtcNow
                BeCliente.User_mod = oBePropietarios.User_agr
                BeCliente.Fec_mod = Date.UtcNow

                '#EJC20210405: Bla, al bad lajdklji sjkldfjaijsd iwe 
                'Osea, se debe intentar mantener el mismo idcliente, pero van a existir en determinado momento desfases.
                'Garantizar que si se creó un cliente con ese Id se inserte un cliente con el código correspondiente 
                'aunque el Id <> codigo...
                If Not clsLnCliente.Existe_Cliente_By_Codigo(BeCliente.Codigo, lConnection, lTransaction) Then

                    '#GT22062022_1510: le asignamos el codigo_propietario original, para ver si existe, sino tomar MaxId
                    BeCliente.IdCliente = CodigoPropietarioOriginal

                    If clsLnCliente.Existe_Cliente_By_IdCliente(BeCliente.IdCliente, lConnection, lTransaction) Then
                        BeCliente.IdCliente = clsLnCliente.MaxID(lConnection, lTransaction) + 1
                    End If

                    clsLnCliente.Insertar(BeCliente, lConnection, lTransaction)

                    Dim BeClienteBodega As New clsBeCliente_bodega()
                    '#CKFK 20210312 Modifiqué esto para que el IdClienteBodega sea el mismo IdCliente
                    'BeClienteBodega.IdClienteBodega = BeCliente.IdCliente 'clsLnCliente_bodega.MaxID(lConnection, lTransaction) + 1
                    '#GT22062022_1536: se utiliza MaxId porque el Idcliente de origen no hace match
                    BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(lConnection, lTransaction) + 1
                    BeClienteBodega.IdCliente = BeCliente.IdCliente
                    BeClienteBodega.IdBodega = pIdBodegaPorDefecto
                    BeClienteBodega.Activo = True
                    BeClienteBodega.User_agr = oBePropietarios.User_agr '1 Esto debería ser parametrizable?
                    BeClienteBodega.User_mod = oBePropietarios.User_agr  '1 Esto debería ser parametrizable?
                    BeClienteBodega.Fec_agr = Now
                    BeClienteBodega.Fec_mod = Now

                    clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega, lConnection, lTransaction)

                End If


            End If

            Insertar_Nuevo_Propietario = vResult

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function


    '#CKFK 20220902 Función creada para obtener los Proveedores que no han sido creados como propietarios
    Public Shared Function Get_Proveedores_No_Existentes_En_Propietarios(ByRef lConnection As SqlConnection,
                                                                         ByRef lTransaction As SqlTransaction) As List(Of clsBeProveedor)

        Get_Proveedores_No_Existentes_En_Propietarios = Nothing

        Dim lReturnList As New List(Of clsBeProveedor)

        Try

            Const lSQl As String = "SELECT *
                                    FROM proveedor 
                                    WHERE nit COLLATE SQL_LATIN1_GENERAL_CP1_CI_AS NOT IN (SELECT nit FROM propietarios)"


            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProveedor

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProveedor
                        clsLnProveedor.Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#CKFK 20220902 Función creada para obtener clientes que no han sido creados como Propietarios
    Public Shared Function Get_Clientes_No_Existentes_En_Propietarios(ByRef lConnection As SqlConnection,
                                                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeCliente)

        Get_Clientes_No_Existentes_En_Propietarios = Nothing

        Dim lReturnList As New List(Of clsBeCliente)

        Try

            Const lSQl As String = "SELECT * 
                                    FROM cliente 
                                    WHERE nit COLLATE SQL_LATIN1_GENERAL_CP1_CI_AS  NOT IN (SELECT nit FROM propietarios) AND CODIGO NOT LIKE 'BG%' "

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeCliente

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeCliente
                        clsLnCliente.Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function


    Public Shared Function Existe_By_IdPropietario(ByRef pIdPropietario As Integer,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Existe_By_IdPropietario = False

        Try


            Const sp As String = "SELECT * FROM Propietarios Where(IdPropietario = @IdPropietario)"


            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IDPROPIETARIO", pIdPropietario)
                Dim dt As New DataTable
                Dim dad As New SqlDataAdapter(lCommand)
                dad.Fill(dt)

                If dt.Rows.Count = 1 Then
                    Dim oBePropietarios = New clsBePropietarios()
                    CargarSinImagen(oBePropietarios, dt.Rows(0))
                    Existe_By_IdPropietario = True
                End If

            End Using


        Catch ex1 As SqlException
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex1.Message))
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function


    Public Shared Function Listar_Propietarios_By_Facturacion_For_Combo(ByVal IdBodega As Integer) As DataTable

        Listar_Propietarios_By_Facturacion_For_Combo = Nothing

        Try

            Const sp As String = "select propietarios.IdPropietario,
                                         propietarios.NIT,propietarios.codigo AS Codigo,
                                         propietarios.nombre_comercial AS Nombre 
                                         FROM propietarios INNER JOIN
                                         propietario_bodega ON propietarios.IdPropietario = propietario_bodega.IdPropietario
                                         where IdBodega = @IdBodega "


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Listar_Propietarios_By_Facturacion_For_Combo = lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Listar_Propietarios_By_Facturacion_For_Combo

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#GT24062025: Obtener lista de propietarios que son afectos a exportación  a la nube, mediante parametro ux
    'Public Shared Function Get_Propietarios_By_UX(ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As List(Of clsBePropietarios)
    '    Get_Propietarios_By_UX = Nothing

    '    Try

    '        Const sp As String = "SELECT * FROM Propietarios Where (controlux = 1) "

    '        Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}


    '            Dim dt As New DataTable
    '            Dim dad As New SqlDataAdapter(lCommand)
    '            dad.Fill(dt)

    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                Get_Propietarios_By_UX = New List(Of clsBePropietarios)()

    '                For Each lRow As DataRow In dt.Rows
    '                    Dim oBePropietarios = New clsBePropietarios()
    '                    Cargar(oBePropietarios, lRow)
    '                    Get_Propietarios_By_UX.Add(oBePropietarios)
    '                Next

    '            End If

    '        End Using

    '    Catch ex1 As SqlException
    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex1.Message))
    '    Catch ex As Exception
    '        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)
    '        Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
    '    End Try

    'End Function

    Public Shared Function Get_Propietarios_By_UX(
    Optional ByRef lConnection As SqlConnection = Nothing,
    Optional ByRef lTransaction As SqlTransaction = Nothing
) As List(Of clsBePropietarios)

        Get_Propietarios_By_UX = Nothing

        Dim localConnection As Boolean = False
        Dim localTransaction As Boolean = False

        Try
            ' Si no se pasó conexión externa, crear una local con la cadena de conexión configurada
            If lConnection Is Nothing Then
                lConnection = New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                localConnection = True
            End If

            ' Si no se pasó transacción externa, crear una local
            If lTransaction Is Nothing Then
                lTransaction = lConnection.BeginTransaction()
                localTransaction = True
            End If

            Const sp As String = "SELECT * FROM Propietarios WHERE (controlux = 1)"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim dt As New DataTable
                Dim dad As New SqlDataAdapter(lCommand)
                dad.Fill(dt)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Get_Propietarios_By_UX = New List(Of clsBePropietarios)()

                    For Each lRow As DataRow In dt.Rows
                        Dim oBePropietarios As New clsBePropietarios()
                        Cargar(oBePropietarios, lRow)
                        Get_Propietarios_By_UX.Add(oBePropietarios)
                    Next
                End If
            End Using

            ' Si la transacción es local, confirmarla
            If localTransaction AndAlso lTransaction IsNot Nothing Then
                lTransaction.Commit()
            End If

        Catch ex1 As SqlException
            ' Rollback si la transacción es local
            If localTransaction AndAlso lTransaction IsNot Nothing Then
                Try
                    lTransaction.Rollback()
                Catch
                    ' Ignorar fallo en rollback
                End Try
            End If
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))

        Catch ex As Exception
            ' Rollback si la transacción es local
            If localTransaction AndAlso lTransaction IsNot Nothing Then
                Try
                    lTransaction.Rollback()
                Catch
                    ' Ignorar fallo en rollback
                End Try
            End If

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))

        Finally
            ' Cerrar conexión solo si es local
            If localConnection AndAlso lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then
                lConnection.Close()
            End If
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
