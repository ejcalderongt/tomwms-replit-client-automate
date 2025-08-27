Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProveedor_bodega
    Implements IDisposable

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdAsignacion),0) FROM proveedor_bodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

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
            Throw New Exception("LnProveedorBodega_MaxId: " & ex.Message)
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdAsignacion),0) FROM proveedor_bodega"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

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

    Public Shared Function Get_All_By_IdProveedor(ByVal pIdProveedor As Integer) As List(Of clsBeProveedor_bodega)

        Dim lReturnList As New List(Of clsBeProveedor_bodega)

        Try

            Dim vSQL As String = "SELECT * FROM proveedor_bodega WHERE IdProveedor=@IdProveedor "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProveedor", pIdProveedor)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeProveedor_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeProveedor_bodega
                                Cargar(Obj, lRow, lConnection, lTransaction)
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
            Throw New Exception("LnProveedorBodega_GetAllByProveedor: " & ex.Message)
        End Try

    End Function

    'Public Shared Function Get_All_By_IdBodega_And_IdPropietario(ByVal IdBodega As Integer, ByVal IdPropietario As Integer) As List(Of clsBeProveedor_bodega)

    '    Dim lReturnList As New List(Of clsBeProveedor_bodega)
    '    Dim lTransaction As SqlTransaction = Nothing

    '    Try

    '        Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

    '            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

    '            Dim vSQL As String = "SELECT * FROM VW_ProveedorBodega WHERE IdBodega=@IdBodega And activo_proveedor_bodega=1 And Activo=1 And IdPropietario = @IdPropietario"

    '            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

    '                lDTA.SelectCommand.CommandType = CommandType.Text
    '                lDTA.SelectCommand.Transaction = lTransaction
    '                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
    '                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)

    '                Dim lDataTable As New DataTable
    '                lDTA.Fill(lDataTable)

    '                Dim BeProveedorBodega As clsBeProveedor_bodega

    '                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

    '                    For Each lRow As DataRow In lDataTable.Rows

    '                        BeProveedorBodega = New clsBeProveedor_bodega
    '                        BeProveedorBodega.Proveedor = New clsBeProveedor

    '                        '#EJC20171106_0402PM: Se pasa la conexión y la transacción porque dentro se hace el cargar del prvoeedor
    '                        'Y se debe(ría) utilizar la misma transacción y conexión abierta.
    '                        Cargar_With_Proveedor(BeProveedorBodega, lRow)

    '                        '#EJC20171106_0358PM: Se hace dentro del cargar de proveedor_bodega
    '                        'BeProveedorBodega.Proveedor.IdProveedor = BeProveedorBodega.IdProveedor
    '                        'clsLnProveedor.Obtener(BeProveedorBodega.Proveedor,lConnection,lTransaction)
    '                        If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
    '                            BeProveedorBodega.Proveedor.Propietario = New clsBePropietarios
    '                            BeProveedorBodega.Proveedor.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
    '                        End If

    '                        If lRow("Empresa") IsNot DBNull.Value AndAlso lRow("Empresa") IsNot Nothing Then
    '                            BeProveedorBodega.Proveedor.Empresa = New clsBeEmpresa
    '                            BeProveedorBodega.Proveedor.Empresa.Nombre = CType(lRow("Empresa"), String)
    '                        End If

    '                        lReturnList.Add(BeProveedorBodega)

    '                    Next

    '                End If

    '            End Using

    '            lTransaction.Commit()

    '            lConnection.Close()

    '        End Using

    '        Return lReturnList

    '    Catch ex As Exception
    '        Throw New Exception("LnProveedorBodega_GetAllByBodegaHH: " & ex.Message)
    '    End Try

    'End Function

    Public Shared Function Get_All_By_IdBodega_And_IdPropietario(ByVal IdBodega As Integer,
                                                                 ByVal IdPropietario As Integer,
                                                                 ByVal Requerir_Proveedor_Es_Bodega_WMS As Boolean) As List(Of clsBeProveedor_bodega)

        Dim lReturnList As New List(Of clsBeProveedor_bodega)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                Dim vSQL As String = "SELECT * FROM VW_ProveedorBodega 
                                      WHERE IdBodega=@IdBodega 
                                      AND IdPropietario = @IdPropietario 
                                      AND activo_proveedor_bodega=1 
                                      AND Activo=1 "

                If Requerir_Proveedor_Es_Bodega_WMS Then
                    vSQL += " AND es_bodega_recepcion = 1 AND es_bodega_traslado = 1 AND idubicacionvirtual <> @IdBodega "
                Else
                    vSQL += " AND es_bodega_recepcion = 0 AND es_bodega_traslado = 0 "
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Transaction = lTransaction
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim BeProveedorBodega As clsBeProveedor_bodega

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            BeProveedorBodega = New clsBeProveedor_bodega
                            BeProveedorBodega.Proveedor = New clsBeProveedor

                            '#EJC20171106_0402PM: Se pasa la conexión y la transacción porque dentro se hace el cargar del prvoeedor
                            'Y se debe(ría) utilizar la misma transacción y conexión abierta.
                            Cargar_With_Proveedor(BeProveedorBodega, lRow)

                            '#EJC20171106_0358PM: Se hace dentro del cargar de proveedor_bodega
                            'BeProveedorBodega.Proveedor.IdProveedor = BeProveedorBodega.IdProveedor
                            'clsLnProveedor.Obtener(BeProveedorBodega.Proveedor,lConnection,lTransaction)
                            If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                BeProveedorBodega.Proveedor.Propietario = New clsBePropietarios
                                BeProveedorBodega.Proveedor.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                            End If

                            If lRow("Empresa") IsNot DBNull.Value AndAlso lRow("Empresa") IsNot Nothing Then
                                BeProveedorBodega.Proveedor.Empresa = New clsBeEmpresa
                                BeProveedorBodega.Proveedor.Empresa.Nombre = CType(lRow("Empresa"), String)
                            End If

                            lReturnList.Add(BeProveedorBodega)

                        Next

                    End If

                End Using

                lTransaction.Commit()

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("LnProveedorBodega_GetAllByBodegaHH: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_HH(ByVal IdBodega As Integer) As List(Of clsBeProveedor_bodega)

        Dim lReturnList As New List(Of clsBeProveedor_bodega)
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                Dim vSQL As String = "SELECT * FROM VW_ProveedorBodega WHERE IdBodega=@IdBodega And activo_proveedor_bodega=1 And Activo=1 "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Transaction = lTransaction

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim BeProveedorBodega As clsBeProveedor_bodega

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            BeProveedorBodega = New clsBeProveedor_bodega
                            BeProveedorBodega.Proveedor = New clsBeProveedor

                            '#EJC20171106_0402PM: Se pasa la conexión y la transacción porque dentro se hace el cargar del prvoeedor
                            'Y se debe(ría) utilizar la misma transacción y conexión abierta.
                            Cargar_With_Proveedor(BeProveedorBodega, lRow)

                            '#EJC20171106_0358PM: Se hace dentro del cargar de proveedor_bodega
                            'BeProveedorBodega.Proveedor.IdProveedor = BeProveedorBodega.IdProveedor
                            'clsLnProveedor.Obtener(BeProveedorBodega.Proveedor,lConnection,lTransaction)
                            If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                BeProveedorBodega.Proveedor.Propietario = New clsBePropietarios
                                BeProveedorBodega.Proveedor.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                            End If

                            If lRow("Empresa") IsNot DBNull.Value AndAlso lRow("Empresa") IsNot Nothing Then
                                BeProveedorBodega.Proveedor.Empresa = New clsBeEmpresa
                                BeProveedorBodega.Proveedor.Empresa.Nombre = CType(lRow("Empresa"), String)
                            End If

                            lReturnList.Add(BeProveedorBodega)

                        Next

                    End If

                End Using

                lTransaction.Commit()

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("LnProveedorBodega_GetAllByBodegaHH: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_HH(ByVal IdBodega As Integer, ByVal IdProveedorBodega As Integer) As List(Of clsBeProveedor_bodega)

        Dim lReturnList As New List(Of clsBeProveedor_bodega)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProveedorBodega WHERE IdBodega=@IdBodega And activo_proveedor_bodega=1 And IdAsignacion = @IdAsignacion "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdAsignacion", IdProveedorBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeProveedorBodega As clsBeProveedor_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeProveedorBodega = New clsBeProveedor_bodega
                                BeProveedorBodega.Proveedor = New clsBeProveedor

                                Cargar(BeProveedorBodega, lRow)

                                BeProveedorBodega.Proveedor.IdProveedor = BeProveedorBodega.IdProveedor
                                clsLnProveedor.Obtener(BeProveedorBodega.Proveedor, lConnection, lTransaction)

                                If lRow("Empresa") IsNot DBNull.Value AndAlso lRow("Empresa") IsNot Nothing Then
                                    BeProveedorBodega.Proveedor.Empresa = New clsBeEmpresa
                                    BeProveedorBodega.Proveedor.Empresa.Nombre = CType(lRow("Empresa"), String)
                                End If

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    BeProveedorBodega.Proveedor.Propietario = New clsBePropietarios
                                    BeProveedorBodega.Proveedor.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                                End If

                                lReturnList.Add(BeProveedorBodega)

                            Next

                        End If

                    End Using

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("LnProveedorBodega_GetAllByBodegaHH: " & ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_HH_Filtro(ByVal IdBodega As Integer,
                                                  ByVal Filtro As String) As List(Of clsBeProveedor_bodega)

        Dim lReturnList As New List(Of clsBeProveedor_bodega)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_ProveedorBodega WHERE IdBodega=@IdBodega And activo_proveedor_bodega=1  " &
                       " And (IdProveedor = @Filtro or Nombre like '% @Filtro %' ) "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Filtro", Filtro)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeProveedorBodega As clsBeProveedor_bodega

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeProveedorBodega = New clsBeProveedor_bodega
                                BeProveedorBodega.Proveedor = New clsBeProveedor

                                Cargar(BeProveedorBodega, lRow)

                                BeProveedorBodega.Proveedor.IdProveedor = BeProveedorBodega.IdProveedor
                                clsLnProveedor.Obtener(BeProveedorBodega.Proveedor, lConnection, lTransaction)

                                If lRow("Empresa") IsNot DBNull.Value AndAlso lRow("Empresa") IsNot Nothing Then
                                    BeProveedorBodega.Proveedor.Empresa = New clsBeEmpresa
                                    BeProveedorBodega.Proveedor.Empresa.Nombre = CType(lRow("Empresa"), String)
                                End If

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    BeProveedorBodega.Proveedor.Propietario = New clsBePropietarios
                                    BeProveedorBodega.Proveedor.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                                End If

                                lReturnList.Add(BeProveedorBodega)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using


            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception("LnProveedorBodega_GetAllByBodegaHH: " & ex.Message)
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeProveedor_bodega As clsBeProveedor_bodega)

        Try

            Const sp As String = "SELECT * FROM Proveedor_bodega" &
            " Where(IdAsignacion = @IdAsignacion) and Activo=1"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDASIGNACION", pBeProveedor_bodega.IdAsignacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeProveedor_bodega, dt.Rows(0))
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function ActualizarDatos(ByVal pObjMD As clsBeProveedor, ByVal pListObjMDB As List(Of clsBeProveedor_bodega)) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnProveedor.Actualizar(pObjMD, lConnection, lTransaction)

            Dim lMax As Integer = MaxID()

            For Each Obj As clsBeProveedor_bodega In pListObjMDB
                If Obj.IdAsignacion = 0 Then
                    lMax += 1
                    Obj.IdAsignacion = lMax
                    Insertar(Obj, lConnection, lTransaction)
                Else
                    Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

            lConnection.Close()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Sub GetSingle_By_IdBodega_And_IdProveedor(ByRef pBeProveedor_bodega As clsBeProveedor_bodega)

        Try

            Const sp As String = "SELECT * FROM Proveedor_bodega" &
            " Where(IdBodega = @IdBodega) " &
            " And (IdProveedor = @Idproveedor) " &
            " And Activo = 1 "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROVEEDOR", pBeProveedor_bodega.IdProveedor))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeProveedor_bodega.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeProveedor_bodega, dt.Rows(0))
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_Single_By_IdBodega_And_IdProveedor(ByRef pBeProveedor_bodega As clsBeProveedor_bodega,
                                                                  ByRef lConnection As SqlConnection,
                                                                  ByRef lTransaction As SqlTransaction) As Boolean

        Get_Single_By_IdBodega_And_IdProveedor = False

        Try

            Const sp As String = "SELECT * FROM Proveedor_bodega" &
            " Where(IdBodega = @IdBodega) " &
            " And (IdProveedor = @Idproveedor) " &
            " And Activo = 1 "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROVEEDOR", pBeProveedor_bodega.IdProveedor))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeProveedor_bodega.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Cargar(pBeProveedor_bodega, dt.Rows(0), lConnection, lTransaction)
                Get_Single_By_IdBodega_And_IdProveedor = True
            Else
                pBeProveedor_bodega = Nothing
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Exist_By_IdBodega_And_IdProveedor(ByVal pIdBodega As Integer, ByVal pIdProveedor As Integer,
                                                                                        ByRef lConnection As SqlConnection,
                                                                                        ByRef lTransaction As SqlTransaction) As Boolean

        Exist_By_IdBodega_And_IdProveedor = False

        Try

            Const sp As String = "SELECT * FROM Proveedor_bodega" &
            " Where(IdBodega = @IdBodega) " &
            " And (IdProveedor = @Idproveedor) " &
            " And Activo = 1 "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROVEEDOR", pIdProveedor))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

            Dim dt As New DataTable
            Dim pBeProveedor_bodega As New clsBeProveedor_bodega
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeProveedor_bodega, dt.Rows(0), lConnection, lTransaction)
                Exist_By_IdBodega_And_IdProveedor = True
            Else
                pBeProveedor_bodega = Nothing
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdBodega_And_IdProveedor(ByRef pBeProveedor_bodega As clsBeProveedor_bodega) As Boolean

        Get_Single_By_IdBodega_And_IdProveedor = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM VW_ProveedorBodega 
                                 Where IdBodega = @IdBodega 
                                 And IdProveedor = @Idproveedor 
                                 And Activo = 1 
                                 And activo_proveedor_bodega=1 "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROVEEDOR", pBeProveedor_bodega.IdProveedor))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeProveedor_bodega.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Cargar(pBeProveedor_bodega, dt.Rows(0), lConnection, lTransaction)
                Get_Single_By_IdBodega_And_IdProveedor = True
            Else
                pBeProveedor_bodega = Nothing
            End If

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

    '#CM191020171017AM
    'Comprobación para txtIdProveedor en Orden de Compra, filtra los proveedores a los cuales se les asignó una bodega.
    Public Shared Sub GetSingleByIdBodegaAndIdProveedorOC(ByRef pBeProveedor_bodega As clsBeProveedor_bodega,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction)

        Try

            Const sp As String = "SELECT * FROM VW_ProveedorBodega" &
            " Where(IdBodega = @IdBodega) " &
            " And (IdProveedor = @Idproveedor " &
            " And Activo = 1 " &
            "And activo_proveedor_bodega=1)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROVEEDOR", pBeProveedor_bodega.IdProveedor))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pBeProveedor_bodega.IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeProveedor_bodega, dt.Rows(0))
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub
    Public Shared Function InsertarFromInterface(ByRef oBeProveedor_bodega As clsBeProveedor_bodega, ByVal pConection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try
            Ins.Init("proveedor_bodega")
            Ins.Add("idasignacion", "@idasignacion", DataType.Parametro)
            Ins.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, pConection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}

            cmd.Parameters.Add(New SqlParameter("@IDASIGNACION", oBeProveedor_bodega.IdAsignacion))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProveedor_bodega.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeProveedor_bodega.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProveedor_bodega.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProveedor_bodega.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProveedor_bodega.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProveedor_bodega.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProveedor_bodega.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeProveedor_bodega As clsBeProveedor_bodega,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Dim sp As String = "SELECT * FROM Proveedor_bodega
                                Where(IdAsignacion = @IdAsignacion) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDASIGNACION", oBeProveedor_bodega.IdAsignacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProveedor_bodega, dt.Rows(0), lConnection, lTransaction)
                Obtener = True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Cargar_With_Proveedor(ByRef oBeProveedor_bodega As clsBeProveedor_bodega, ByRef dr As DataRow)

        Try

            '#EJC20171106_0416PM:Se realizó este cambio para optimizar la carga de proveedores 5k+ registros.
            With oBeProveedor_bodega

                .IdAsignacion = IIf(IsDBNull(dr.Item("IdAsignacion")), 0, dr.Item("IdAsignacion"))
                .IdProveedor = IIf(IsDBNull(dr.Item("IdProveedor")), 0, dr.Item("IdProveedor"))
                .Proveedor = New clsBeProveedor
                .Proveedor.IdProveedor = .IdProveedor

                With .Proveedor
                    .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                    .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                    .IdProveedor = IIf(IsDBNull(dr.Item("IdProveedor")), 0, dr.Item("IdProveedor"))
                    .Codigo = IIf(IsDBNull(dr.Item("Código")), "", dr.Item("Código"))
                    .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                    .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                    .Nit = IIf(IsDBNull(dr.Item("nit")), "", dr.Item("nit"))
                    .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
                    .Email = IIf(IsDBNull(dr.Item("email")), "", dr.Item("email"))
                    .Contacto = IIf(IsDBNull(dr.Item("contacto")), "", dr.Item("contacto"))
                    .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                    .Muestra_precio = IIf(IsDBNull(dr.Item("muestra_precio")), False, dr.Item("muestra_precio"))
                    .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                    .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                    .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                    .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                    .Actualiza_costo_oc = IIf(IsDBNull(dr.Item("actualiza_costo_oc")), False, dr.Item("actualiza_costo_oc"))
                End With

                '.Proveedor = clsLnProveedor.GetSingle(.IdProveedor,lConnection,lTransaction)
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Tiene_Proveedor_Bodega(ByVal IdProveedor As Integer, ByVal IdBodega As Integer) As Boolean

        Try

            Tiene_Proveedor_Bodega = False

            Dim vSQL As String = "SELECT * from proveedor_bodega inner join 
            bodega on bodega.IdBodega = proveedor_bodega.IdBodega 
            where proveedor_bodega.IdProveedor = @IdProveedor and proveedor_bodega.IdBodega = @IdBodega"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProveedor", IdProveedor))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Tiene_Proveedor_Bodega = True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Elimina_ProveedorBodega(ByVal IdBodega As Integer, ByVal IdProveedor As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim vSQL As String = " Delete from Proveedor_bodega" &
             "  Where(IdProveedor = @IdProveedor and IdBodega=@IdBodega)"

            cmd = New SqlCommand(vSQL, lConnection)
            lConnection.Open()

            cmd.Parameters.Add(New SqlParameter("@IdProveedor", IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Existe_Id(ByRef pIdAsignacion As Integer,
                                     ByVal pConnection As SqlConnection,
                                     ByVal pTransaction As SqlTransaction) As Boolean

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT IdAsignacion FROM proveedor_bodega WHERE IdAsignacion = @IdAsignacion"

            Using lCommand As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdAsignacion", pIdAsignacion)

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

    Public Shared Function Get_Single_By_NIT_AND_IdBodega(ByVal pNIT As String,
                                                          ByVal pIdBodega As Integer,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction) As clsBeProveedor_bodega

        Get_Single_By_NIT_AND_IdBodega = Nothing

        Try

            Const sp As String = "SELECT * FROM VW_ProveedorBodega 
                                 Where(IdBodega = @IdBodega) 
                                 And (NIT = @NIT
                                 And Activo = 1 
                                 And activo_proveedor_bodega=1)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@NIT", pNIT))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then

                Dim pBeProveedor_bodega As New clsBeProveedor_bodega
                Cargar(pBeProveedor_bodega, dt.Rows(0))
                Get_Single_By_NIT_AND_IdBodega = pBeProveedor_bodega
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Single_By_NIT_AND_IdBodega(ByVal pNIT As String,
                                                          ByVal pIdBodega As Integer) As clsBeProveedor_bodega

        Get_Single_By_NIT_AND_IdBodega = Nothing

        Try
            '#GT29092022_1400: se agrega idubicacionvirtual con valor 0 porque se asocia al propietario base y no a los virtuales.
            Const sp As String = "SELECT * FROM VW_ProveedorBodega 
                                 Where(IdBodega = @IdBodega) 
                                 And (NIT = @NIT
                                 And Activo = 1 
                                 And activo_proveedor_bodega=1
                                 And idubicacionvirtual=0 )"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@NIT", pNIT))
                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))

                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count = 1 Then
                        Dim pBeProveedor_bodega As New clsBeProveedor_bodega
                        Cargar(pBeProveedor_bodega, dt.Rows(0), lConnection, lTransaction)
                        Get_Single_By_NIT_AND_IdBodega = pBeProveedor_bodega
                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    '#GT19072023: Devuelve proveedor-bodega por idbodega y el idpropietario para importación inventario inicial excel
    Public Shared Function Get_Single_By_IdBodega_AND_IdProveedor(ByVal pIdBodega As Integer,
                                                                    ByVal pIdProveedor As Integer,
                                                                    ByRef lConnection As SqlConnection,
                                                                    ByRef lTransaction As SqlTransaction) As clsBeProveedor_bodega

        Get_Single_By_IdBodega_AND_IdProveedor = Nothing

        Try

            Const sp As String = "SELECT * FROM VW_ProveedorBodega 
                                 Where(IdBodega = @IdBodega) 
                                 And (IdProveedor = @IdProveedor)
                                 And Activo = 1 "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDBODEGA", pIdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROVEEDOR", pIdProveedor))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then

                Dim pBeProveedor_bodega As New clsBeProveedor_bodega
                Cargar(pBeProveedor_bodega, dt.Rows(0))
                Get_Single_By_IdBodega_AND_IdProveedor = pBeProveedor_bodega
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#GT25062025: lista de proveedores_bodega por proveedor para exportacion a la nube.
    Public Shared Function Get_All_By_IdProveedor(ByVal pIdProveedor As Integer, ByRef lConnection As SqlConnection,
                                                                                 ByRef lTransaction As SqlTransaction) As List(Of clsBeProveedor_bodega)

        Get_All_By_IdProveedor = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM proveedor_bodega WHERE IdProveedor=@IdProveedor "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProveedor", pIdProveedor)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeProveedor_bodega

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Get_All_By_IdProveedor = New List(Of clsBeProveedor_bodega)()

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeProveedor_bodega
                        Cargar(Obj, lRow, lConnection, lTransaction)
                        Get_All_By_IdProveedor.Add(Obj)

                    Next

                End If

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
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
