Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProveedor
    Implements IDisposable

    Public Shared Function GetSingle(ByVal pIdProveedor As Integer) As clsBeProveedor

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM proveedor WHERE IdProveedor=@IdProveedor"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProveedor", pIdProveedor)

                        Dim lDT As New DataTable()

                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeProveedor()
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

    Public Shared Function Get_Single_By_IdProveedor_And_IdPropietario(ByVal pIdProveedor As Integer,
                                                                       ByVal pIdPropietario As Integer) As clsBeProveedor

        Try

            Dim vSQL As String = "SELECT * FROM proveedor WHERE IdProveedor=@IdProveedor AND IdPropietario=@IdPropietario"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProveedor", pIdProveedor)
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                    Dim lDT As New DataTable()
                    lDTA.Fill(lDT)

                    If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                        Dim lRow As DataRow = lDT.Rows(0)
                        Dim Obj As New clsBeProveedor()

                        Cargar(Obj, lRow)

                        Return Obj

                    End If

                End Using

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdProveedorBodega(ByVal pIdProveedorBodega As Integer) As clsBeProveedor

        Get_Single_By_IdProveedorBodega = Nothing

        Try

            Dim vSQL As String = "SELECT proveedor_bodega.IdAsignacion, dbo.proveedor.* 
                                  FROM proveedor INNER JOIN
                                  proveedor_bodega ON dbo.proveedor.IdProveedor = proveedor_bodega.IdProveedor
                                  WHERE IdAsignacion=@IdAsignacion "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdAsignacion", pIdProveedorBodega)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeProveedor()
                            Cargar(Obj, lRow)
                            Get_Single_By_IdProveedorBodega = Obj

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

    Public Shared Function Get_IdProveedorBodega_By_IdBodegaWMS(ByVal pIdBodegaWMS As Integer) As Integer

        Get_IdProveedorBodega_By_IdBodegaWMS = 0

        Try

            Dim vSQL As String = "SELECT proveedor_bodega.IdAsignacion as IdProveedorBodega, proveedor.* 
                                  FROM proveedor INNER JOIN
                                  proveedor_bodega ON dbo.proveedor.IdProveedor = proveedor_bodega.IdProveedor
                                  WHERE proveedor.IdUbicacionVirtual=@IdUbicacionVirtual AND es_bodega_recepcion = 1 "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacionVirtual", pIdBodegaWMS)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            Dim lRow As DataRow = lDT.Rows(0)
                            Get_IdProveedorBodega_By_IdBodegaWMS = lRow.Item("IdProveedorBodega")
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

    Public Shared Function Get_IdProveedorBodega_By_IdBodegaWMS(ByVal pIdBodegaWMS As Integer,
                                                                ByVal pIdPropietario As Integer,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction) As Integer

        Get_IdProveedorBodega_By_IdBodegaWMS = 0

        Try

            Dim vSQL As String = "SELECT proveedor_bodega.IdAsignacion as IdProveedorBodega, proveedor.* 
                                  FROM proveedor INNER JOIN
                                  proveedor_bodega ON proveedor.IdProveedor = proveedor_bodega.IdProveedor
                                  WHERE proveedor.IdUbicacionVirtual=@IdUbicacionVirtual 
                                  AND es_bodega_recepcion = 1 
                                  AND proveedor.IdPropietario = @IdPropietario"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacionVirtual", pIdBodegaWMS)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)

                    Return lRow.Item("IdProveedorBodega")

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal IdBodega As Integer) As List(Of clsBeProveedor)

        Dim lReturnList As New List(Of clsBeProveedor)

        Try

            Dim vSQL As String = "SELECT * FROM VW_Proveedor WHERE 1 > 0 and IdBodega=@IdBodega "

            If pActivo = True Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeProveedor As clsBeProveedor

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeProveedor = New clsBeProveedor
                                BeProveedor.Empresa = New clsBeEmpresa
                                BeProveedor.Propietario = New clsBePropietarios
                                Cargar(BeProveedor, lRow)

                                If lRow("Empresa") IsNot DBNull.Value AndAlso lRow("Empresa") IsNot Nothing Then
                                    BeProveedor.Empresa.Nombre = CType(lRow("Empresa"), String)
                                End If

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    BeProveedor.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                                End If

                                lReturnList.Add(BeProveedor)

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


    Public Shared Function Get_All_Lista_Mantenimiento(ByVal pActivo As Boolean, ByVal IdBodega As Integer) As List(Of clsBeProveedor)

        Dim lReturnList As New List(Of clsBeProveedor)

        Try

            '#CKFK 20210812 Agregué el campo es_proveedor_servicio porque se necesita en la clase
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = " SELECT [Empresa], [Propietario], [IdEmpresa], [IdPropietario], [IdProveedor], [codigo], [nombre], 
                                    [telefono], [nit], [direccion], [email], [contacto], [activo], [muestra_precio], [user_agr], 
                                    [fec_agr], [user_mod], [fec_mod], [actualiza_costo_oc], [activo_proveedor_bodega], 
                                    [idubicacionvirtual], [es_bodega_recepcion], [es_bodega_traslado], [referencia], 
                                    [sistema], [IdConfiguracionBarraPallet], [es_proveedor_servicio],IdBodegaAreaSAP, Codigo_Empresa_ERP 
                                    FROM VW_Proveedor
                                    WHERE IdBodega =@IdBodega  "

                If pActivo = True Then
                    vSQL += " AND Activo=1"
                Else
                    vSQL += " AND Activo=0"
                End If

                vSQL += "group by [Empresa], [Propietario], [IdEmpresa], [IdPropietario], [IdProveedor], [codigo], [nombre], 
                         [telefono], [nit], [direccion], [email], [contacto], [activo], [muestra_precio], [user_agr], 
                         [fec_agr], [user_mod], [fec_mod], [actualiza_costo_oc], [activo_proveedor_bodega], 
                         [idubicacionvirtual], [es_bodega_recepcion], [es_bodega_traslado], [referencia], 
                         [sistema], [IdConfiguracionBarraPallet], [es_proveedor_servicio], IdBodegaAreaSAP, Codigo_Empresa_ERP "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim BeProveedor As clsBeProveedor

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            BeProveedor = New clsBeProveedor
                            BeProveedor.Empresa = New clsBeEmpresa
                            BeProveedor.Propietario = New clsBePropietarios
                            Cargar(BeProveedor, lRow)

                            If lRow("Empresa") IsNot DBNull.Value AndAlso lRow("Empresa") IsNot Nothing Then
                                BeProveedor.Empresa.Nombre = CType(lRow("Empresa"), String)
                            End If

                            If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                BeProveedor.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                            End If

                            lReturnList.Add(BeProveedor)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByPropietario(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProveedor)

        Dim lReturnList As New List(Of clsBeProveedor)

        Try

            Dim vSQL As String = "SELECT * FROM VW_Proveedor WHERE 1 > 0 "

            If pActivo = True Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            If pIdPropietario <> 0 Then
                vSQL += " AND IdPropietario=@IdPropietario"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        If pIdPropietario <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeProveedor As clsBeProveedor

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeProveedor = New clsBeProveedor
                                BeProveedor.Empresa = New clsBeEmpresa
                                BeProveedor.Propietario = New clsBePropietarios
                                Cargar(BeProveedor, lRow)

                                If lRow("Empresa") IsNot DBNull.Value AndAlso lRow("Empresa") IsNot Nothing Then
                                    BeProveedor.Empresa.Nombre = CType(lRow("Empresa"), String)
                                End If

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    BeProveedor.Propietario.Nombre_comercial = CType(lRow("Propietario"), String)
                                End If

                                lReturnList.Add(BeProveedor)

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

    Public Shared Function Existe(ByVal pCodigo As String,
                                  ByRef Cnn As SqlConnection,
                                  ByRef pTransaction As SqlTransaction) As clsBeProveedor

        Existe = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM proveedor WHERE Codigo=@Codigo"

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim ObjUM As New clsBeProveedor()

                    Cargar(ObjUM, lRow)
                    Return ObjUM

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe(ByVal pCodigo As String) As clsBeProveedor

        Existe = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM proveedor WHERE Codigo=@Codigo"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                        lDTA.SelectCommand.Transaction = lTransaction

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim BeProveedor As New clsBeProveedor()
                            Cargar(BeProveedor, lRow)
                            Existe = BeProveedor

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

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const SP As String = "SELECT ISNULL(Max(IdProveedor),0) FROM proveedor"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(SP, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue) + 1
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdProveedor),0) FROM proveedor"

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

    Public Shared Function Get_ProveedorBodega_By_Codigo_Proveedor(ByVal pCodigo As String,
                                                                  ByVal pIdBodega As Integer,
                                                                  ByRef lConection As SqlConnection,
                                                                  ByRef lTransaction As SqlTransaction) As clsBeProveedor_bodega

        Get_ProveedorBodega_By_Codigo_Proveedor = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM proveedor WHERE Codigo=@Codigo "

            Using lDTA As New SqlDataAdapter(vSQL, lConection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeProveedor As New clsBeProveedor()

                    Cargar(BeProveedor, lRow)

                    Dim BeProveedorBodega As New clsBeProveedor_bodega()
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConection, lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = pIdBodega
                    BeProveedorBodega.Proveedor = BeProveedor

                    If Not clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdProveedor(BeProveedorBodega, lConection, lTransaction) Then

                        Dim vMensaje As String = String.Format("El proveedor: {0 } existe con identificador: {1}, pero no fue posible obtener la asociación del proveedor con el IdBodega: {2} ", pCodigo, BeProveedorBodega.IdProveedor, pIdBodega)
                        Throw New Exception(vMensaje)

                    Else
                        Return BeProveedorBodega
                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Proveedor(ByVal pCodigo As String) As Boolean

        Existe_Proveedor = False

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT TOP 1 * FROM proveedor WHERE Codigo=@Codigo "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Existe_Proveedor = True

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

    Public Shared Function Get_All_For_Combo() As DataTable

        Try

            Const sp As String = "SELECT IdProveedor,nombre AS Nombre FROM Proveedor"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Grid() As DataTable

        Try

            Const sp As String = "SELECT IdProveedor, Codigo as Codigo, Nombre AS Nombre FROM Proveedor "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    'GT13072022_1830: listar proveedor acorde al propietario en mantto producto para codigos de barra
    Public Shared Function Get_All_For_Combo_By_IdProveedor_and_Bodega(ByVal pIdBodega, pIdProveedor) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_For_Combo_By_IdProveedor_and_Bodega = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT IdProveedor,Nombre FROM VW_ProveedorBodega 
                                 Where IdBodega = @IdBodega 
                                 And IdProveedor = @Idproveedor "
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProveedor", pIdProveedor))
            Dim dt As New DataTable
            dad.Fill(dt)
            Get_All_For_Combo_By_IdProveedor_and_Bodega = dt

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

    Public Shared Function Obtener(ByRef oBeProveedor As clsBeProveedor,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Const sp As String = "SELECT * FROM Proveedor 
                                  Where(IdProveedor = @IdProveedor)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProveedor.IdProveedor))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProveedor, dt.Rows(0))
                Obtener = True
            Else
                Obtener = False
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pIdProveedor As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeProveedor

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Proveedor 
                                  WHERE(IdProveedor = @IdProveedor)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProveedor", pIdProveedor))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim BeProveedor As New clsBeProveedor

            If dt.Rows.Count = 1 Then
                Cargar(BeProveedor, dt.Rows(0))
                Return BeProveedor
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_IdBodegaVirtual(ByRef oBeProveedor As clsBeProveedor) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Upd.Init("proveedor")
            Upd.Add("idubicacionvirtual", "@idubicacionvirtual", DataType.Parametro)
            Upd.Where("IdProveedor = @IdProveedor")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProveedor.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONVIRTUAL", oBeProveedor.IdUbicacionVirtual))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualizar_Activo(ByRef oBeProveedor As clsBeProveedor) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Upd.Init("proveedor")
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("IdProveedor = @IdProveedor")

            Dim sp As String = Upd.SQL()
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProveedor.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProveedor.Activo))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_NIT(ByVal pNIT As String, ByVal pIdBodega As Integer) As clsBeProveedor

        Get_Single_By_NIT = Nothing

        Try

            Dim vSQL As String = "SELECT proveedor_bodega.IdAsignacion, dbo.proveedor.* 
                                    FROM proveedor INNER JOIN
                                    proveedor_bodega ON dbo.proveedor.IdProveedor = proveedor_bodega.IdProveedor
                                    WHERE proveedor.NIT=@NIT AND proveedor_bodega.IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@NIT", pNIT)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeProveedor()
                            Cargar(Obj, lRow)
                            Get_Single_By_NIT = Obj

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

    '#CKFK 20220902 Función creada para obtener los propietarios que no han sido creados como Proveedores
    Public Shared Function Get_Propietarios_No_Existentes_En_Proveedores(ByRef lConnection As SqlConnection,
                                                                         ByRef lTransaction As SqlTransaction) As List(Of clsBePropietarios)

        Get_Propietarios_No_Existentes_En_Proveedores = Nothing

        Dim lReturnList As New List(Of clsBePropietarios)

        Try

            Const lSQl As String = "SELECT * 
                                    FROM propietarios 
                                    WHERE nit COLLATE SQL_LATIN1_GENERAL_CP1_CI_AS  NOT IN (SELECT nit FROM proveedor) "

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBePropietarios

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBePropietarios
                        clsLnPropietarios.Cargar(Obj, lRow)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20220902 Función creada para obtener clientes que no han sido creados como Proveedores
    Public Shared Function Get_Clientes_No_Existentes_En_Proveedores(ByRef lConnection As SqlConnection,
                                                                     ByRef lTransaction As SqlTransaction) As List(Of clsBeCliente)

        Get_Clientes_No_Existentes_En_Proveedores = Nothing

        Dim lReturnList As New List(Of clsBeCliente)

        Try

            Const lSQl As String = "SELECT * 
                                    FROM cliente 
                                    WHERE nit COLLATE SQL_LATIN1_GENERAL_CP1_CI_AS  NOT IN (SELECT nit FROM proveedor)"

            Using lDTA As New SqlDataAdapter(lSQl, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeCliente As clsBeCliente

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        BeCliente = New clsBeCliente
                        clsLnCliente.Cargar(BeCliente, lRow)
                        lReturnList.Add(BeCliente)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    '#GT19072023: función para crear proveedor de un propietario importado por inventario inicial excel
    Public Shared Function Existe_by_IdPropietario(ByVal IdPropietario As Integer, ByRef Cnn As SqlConnection, ByRef pTransaction As SqlTransaction) As clsBeProveedor

        Existe_by_IdPropietario = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM proveedor WHERE IdPropietario=@IdPropietario"

            Using lDTA As New SqlDataAdapter(vSQL, Cnn)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", IdPropietario)
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeProveedor As New clsBeProveedor()
                    Cargar(BeProveedor, lRow)
                    Return BeProveedor

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Sub Guardar_Transaccion(ByVal pBeProveedor As clsBeProveedor,
                                          ByVal pProveedorTiemposList As List(Of clsBeProveedor_tiempos),
                                          ByVal pProveedorBodegaList As List(Of clsBeProveedor_bodega))


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If pBeProveedor.IsNew Then
                pBeProveedor.IdProveedor = MaxID(lConnection, lTransaction) + 1
                Insertar(pBeProveedor, lConnection, lTransaction)
            Else
                Actualizar(pBeProveedor, lConnection, lTransaction)
            End If

            If pProveedorBodegaList IsNot Nothing AndAlso pProveedorBodegaList.Count > 0 Then

                For Each BeProveedorBodega As clsBeProveedor_bodega In pProveedorBodegaList
                    If BeProveedorBodega.IdAsignacion = 0 Then
                        BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                        clsLnProveedor_bodega.Insertar(BeProveedorBodega, lConnection, lTransaction)
                    Else
                        clsLnProveedor_bodega.Actualizar(BeProveedorBodega, lConnection, lTransaction)
                    End If
                Next

            End If

            If pProveedorTiemposList IsNot Nothing AndAlso pProveedorTiemposList.Count > 0 Then

                For Each BeProveedorTiempo As clsBeProveedor_tiempos In pProveedorTiemposList

                    If BeProveedorTiempo.IdProveedor = 0 Then
                        BeProveedorTiempo.IdTiempoProveedor = clsLnProveedor_tiempos.MaxID(lConnection, lTransaction) + 1
                        BeProveedorTiempo.IdProveedor = pBeProveedor.IdProveedor
                        clsLnProveedor_tiempos.Insertar(BeProveedorTiempo, lConnection, lTransaction)
                    Else
                        clsLnProveedor_tiempos.Actualizar(BeProveedorTiempo, lConnection, lTransaction)
                    End If

                Next

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Get_ProveedorBodega_By_Codigo_Proveedor_Defecto_Inv_Inicial(ByRef lConnection As SqlConnection,
                                                                                       ByRef lTransaction As SqlTransaction,
                                                                                       ByVal BeConfigEnc As clsBeI_nav_config_enc) As clsBeProveedor_bodega

        Get_ProveedorBodega_By_Codigo_Proveedor_Defecto_Inv_Inicial = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM proveedor WHERE Codigo=@Codigo "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", "INVINI_MI3")

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeProveedor As New clsBeProveedor()

                    Cargar(BeProveedor, lRow)

                    Dim BeProveedorBodega As New clsBeProveedor_bodega()
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.Proveedor = BeProveedor
                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega

                    If Not clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdProveedor(BeProveedorBodega, lConnection, lTransaction) Then
                        Dim vMensaje As String = String.Format("El proveedor: {0 } existe con identificador: {1}, pero no fue posible obtener la asociación del proveedor con la Bodega")
                        Throw New Exception(vMensaje)
                    Else
                        BeProveedorBodega.IdBodega = BeProveedorBodega.IdBodega
                    End If

                    Get_ProveedorBodega_By_Codigo_Proveedor_Defecto_Inv_Inicial = BeProveedorBodega

                Else

                    Dim BeProveedor As New clsBeProveedor
                    Dim BeProveedorBodega As New clsBeProveedor_bodega

                    BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                    BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                    BeProveedor.IdProveedor = MaxID(lConnection, lTransaction) + 1
                    BeProveedor.Codigo = "INVINI_MI3"
                    BeProveedor.Nombre = "PROVEEDOR DE SISTEMA PARA INVENTARIO INICIAL"
                    BeProveedor.Telefono = 0
                    BeProveedor.Nit = 0
                    BeProveedor.Direccion = ""
                    BeProveedor.Contacto = ""
                    BeProveedor.Activo = True
                    BeProveedor.User_agr = BeConfigEnc.IdUsuario
                    BeProveedor.Fec_agr = Date.UtcNow
                    BeProveedor.User_mod = BeConfigEnc.IdUsuario
                    BeProveedor.Fec_mod = Date.Now

                    Try

                        Insertar(BeProveedor, lConnection, lTransaction)

                        BeProveedorBodega = New clsBeProveedor_bodega
                        BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                        BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                        BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                        BeProveedorBodega.Activo = True
                        BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario
                        BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario
                        BeProveedorBodega.Fec_agr = Now
                        BeProveedorBodega.Fec_mod = Now
                        clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, lConnection, lTransaction)

                        Get_ProveedorBodega_By_Codigo_Proveedor_Defecto_Inv_Inicial = BeProveedorBodega

                    Catch ex As Exception
                        clsLnLog_error_wms.Agregar_Error(ex.Message)
                    End Try

                End If

            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function Get_ProveedorBodega_By_Codigo_Proveedor(ByVal pCodigo As String,
                                                                   ByVal pIdBodega As Integer,
                                                                   ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                                   ByRef lConection As SqlConnection,
                                                                   ByRef lTransaction As SqlTransaction) As clsBeProveedor_bodega

        Get_ProveedorBodega_By_Codigo_Proveedor = Nothing

        Try

            Dim vSQL As String = "SELECT TOP 1 * FROM proveedor WHERE Codigo=@Codigo "

            Using lDTA As New SqlDataAdapter(vSQL, lConection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim BeProveedor As New clsBeProveedor()

                    Cargar(BeProveedor, lRow)

                    Dim BeProveedorBodega As New clsBeProveedor_bodega()
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConection, lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = pIdBodega
                    BeProveedorBodega.Proveedor = BeProveedor

                    Dim BeProveedorBodegaNuevo As New clsBeProveedor_bodega()

                    clsPublic.CopyObject(BeProveedorBodega, BeProveedorBodegaNuevo)

                    If Not clsLnProveedor_bodega.Get_Single_By_IdBodega_And_IdProveedor(BeProveedorBodega, lConection, lTransaction) Then

                        BeProveedorBodega = BeProveedorBodegaNuevo
                        BeProveedorBodega.User_agr = BeConfigEnc.User_agr
                        BeProveedorBodega.User_mod = BeConfigEnc.User_mod
                        BeProveedorBodega.Fec_agr = Now
                        BeProveedorBodega.Fec_mod = Now
                        BeProveedorBodega.Activo = True
                        clsLnProveedor_bodega.Insertar(BeProveedorBodega)

                    End If

                    Get_ProveedorBodega_By_Codigo_Proveedor = BeProveedorBodega

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_By_Codigo_And_Company(ByVal pCodigo As String, ByVal pEmpresa As String) As Boolean

        Existe_By_Codigo_And_Company = False

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT TOP 1 * FROM proveedor WHERE Codigo=@Codigo AND Codigo_Empresa_ERP = @Codigo_Empresa_ERP "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Codigo_Empresa_ERP", pEmpresa)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Existe_By_Codigo_And_Company = True

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

    Public Shared Function Existe_Proveedor(ByVal pCodigo As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean
        Try
            Dim vSQL As String = "SELECT TOP 1 * FROM proveedor WHERE Codigo=@Codigo"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                Return (lDT IsNot Nothing AndAlso lDT.Rows.Count > 0)
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Insert_Proveedor_Interface(INavBeProveedor As clsBeI_nav_bodega, BeConfigEnc As clsBeI_nav_config_enc, ByVal User As String) As Boolean

        Insert_Proveedor_Interface = False

        Try

            Dim BeProveedor As New clsBeProveedor
            BeProveedor.IdProveedor = MaxID() + 1
            BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
            BeProveedor.Codigo = INavBeProveedor.Bodega_code
            BeProveedor.Nombre = INavBeProveedor.Bodega_name
            BeProveedor.Activo = True
            BeProveedor.Fec_agr = Now
            BeProveedor.Fec_mod = Now
            BeProveedor.User_agr = User
            BeProveedor.User_mod = User
            BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
            Insertar(BeProveedor)

            Dim lBodegas = clsLnBodega.GetAll()

            For Each Bod In lBodegas
                Dim BeProvBod As New clsBeProveedor_bodega
                BeProvBod.IdAsignacion = clsLnProveedor_bodega.MaxID() + 1
                BeProvBod.IdProveedor = BeProveedor.IdProveedor
                BeProvBod.IdBodega = Bod.IdBodega
                BeProvBod.User_agr = Now
                BeProvBod.User_mod = Now
                BeProvBod.Activo = True
                clsLnProveedor_bodega.Insertar(BeProvBod)
            Next

            Insert_Proveedor_Interface = True

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