Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnProveedor
    Public Shared Sub Cargar(ByRef oBeProveedor As clsBeProveedor, ByRef dr As DataRow)

        Try

            With oBeProveedor

                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdProveedor = IIf(IsDBNull(dr.Item("IdProveedor")), 0, dr.Item("IdProveedor"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
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
                .IdUbicacionVirtual = IIf(IsDBNull(dr.Item("idubicacionvirtual")), 0, dr.Item("idubicacionvirtual"))
                .Es_Bodega_Recepcion = IIf(IsDBNull(dr.Item("es_bodega_recepcion")), False, dr.Item("es_bodega_recepcion"))
                .Es_Bodega_Traslado = IIf(IsDBNull(dr.Item("es_bodega_traslado")), False, dr.Item("es_bodega_traslado"))
                .Referencia = IIf(IsDBNull(dr.Item("referencia")), "", dr.Item("referencia"))
                .Sistema = IIf(IsDBNull(dr.Item("Sistema")), False, dr.Item("Sistema"))
                .IdConfiguracionBarraPallet = IIf(IsDBNull(dr.Item("IdConfiguracionBarraPallet")), 0, dr.Item("IdConfiguracionBarraPallet"))
                .Es_Proveedor_Servicio = IIf(IsDBNull(dr.Item("Es_Proveedor_Servicio")), False, dr.Item("Es_Proveedor_Servicio"))
                .IdBodegaAreaSAP = IIf(IsDBNull(dr.Item("IdBodegaAreaSAP")), 0, dr.Item("IdBodegaAreaSAP"))
                If dr.Table.Columns.Contains("idPais") Then .IdPais = IIf(IsDBNull(dr.Item("idPais")), 0, dr.Item("idPais"))
                .Codigo_Empresa_ERP = IIf(IsDBNull(dr.Item("Codigo_Empresa_ERP")), "", dr.Item("Codigo_Empresa_ERP"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeProveedor As clsBeProveedor, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("proveedor")
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("telefono", "@telefono", DataType.Parametro)
            Ins.Add("nit", "@nit", DataType.Parametro)
            Ins.Add("direccion", "@direccion", DataType.Parametro)
            Ins.Add("email", "@email", DataType.Parametro)
            Ins.Add("contacto", "@contacto", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("muestra_precio", "@muestra_precio", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("actualiza_costo_oc", "@actualiza_costo_oc", DataType.Parametro)
            If Not oBeProveedor.IdUbicacionVirtual = 0 Then Ins.Add("idubicacionvirtual", "@idubicacionvirtual", DataType.Parametro)
            Ins.Add("es_bodega_recepcion", "@es_bodega_recepcion", DataType.Parametro)
            Ins.Add("es_bodega_traslado", "@es_bodega_traslado", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("idconfiguracionbarrapallet", "@idconfiguracionbarrapallet", DataType.Parametro)
            Ins.Add("es_proveedor_servicio", "@es_proveedor_servicio", DataType.Parametro)
            If Not oBeProveedor.IdBodegaAreaSAP = 0 Then Ins.Add("idbodegaareasap", "@idbodegaareasap", DataType.Parametro)
            If Not oBeProveedor.IdPais = 0 Then Ins.Add("idpais", "@idpais", DataType.Parametro)
            Ins.Add("codigo_empresa_erp", "@codigo_empresa_erp", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeProveedor.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProveedor.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProveedor.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProveedor.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProveedor.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeProveedor.Telefono))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeProveedor.Nit))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProveedor.Direccion)))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBeProveedor.Email))
            cmd.Parameters.Add(New SqlParameter("@CONTACTO", oBeProveedor.Contacto))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProveedor.Activo))
            cmd.Parameters.Add(New SqlParameter("@MUESTRA_PRECIO", oBeProveedor.Muestra_precio))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProveedor.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProveedor.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProveedor.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProveedor.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTUALIZA_COSTO_OC", oBeProveedor.Actualiza_costo_oc))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONVIRTUAL", oBeProveedor.IdUbicacionVirtual))
            cmd.Parameters.Add(New SqlParameter("@ES_BODEGA_RECEPCION", oBeProveedor.Es_Bodega_Recepcion))
            cmd.Parameters.Add(New SqlParameter("@ES_BODEGA_TRASLADO", oBeProveedor.Es_Bodega_Traslado))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeProveedor.Referencia))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeProveedor.Sistema))
            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONBARRAPALLET", oBeProveedor.IdConfiguracionBarraPallet))
            cmd.Parameters.Add(New SqlParameter("@ES_PROVEEDOR_SERVICIO", oBeProveedor.Es_Proveedor_Servicio))
            If Not oBeProveedor.IdBodegaAreaSAP = 0 Then cmd.Parameters.Add(New SqlParameter("@IDBODEGAAREASAP", oBeProveedor.IdBodegaAreaSAP))
            If Not oBeProveedor.IdPais = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPAIS", oBeProveedor.IdPais))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_EMPRESA_ERP", oBeProveedor.Codigo_Empresa_ERP))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

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

    Public Shared Function Actualizar(ByRef oBeProveedor As clsBeProveedor, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("proveedor")
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("idproveedor", "@idproveedor", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("telefono", "@telefono", DataType.Parametro)
            Upd.Add("nit", "@nit", DataType.Parametro)
            Upd.Add("direccion", "@direccion", DataType.Parametro)
            Upd.Add("email", "@email", DataType.Parametro)
            Upd.Add("contacto", "@contacto", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("muestra_precio", "@muestra_precio", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("actualiza_costo_oc", "@actualiza_costo_oc", DataType.Parametro)
            If Not oBeProveedor.IdUbicacionVirtual = 0 Then Upd.Add("idubicacionvirtual", "@idubicacionvirtual", DataType.Parametro)
            Upd.Add("es_bodega_recepcion", "@es_bodega_recepcion", DataType.Parametro)
            Upd.Add("es_bodega_traslado", "@es_bodega_traslado", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Add("idconfiguracionbarrapallet", "@IdConfiguracionBarraPallet", DataType.Parametro)
            Upd.Add("es_proveedor_servicio", "@es_proveedor_servicio", DataType.Parametro)
            If Not oBeProveedor.IdBodegaAreaSAP = 0 Then Upd.Add("idbodegaareasap", "@idbodegaareasap", DataType.Parametro)
            If Not oBeProveedor.IdPais = 0 Then Upd.Add("idpais", "@idpais", DataType.Parametro)
            Upd.Add("codigo_empresa_erp", "@codigo_empresa_erp", DataType.Parametro)
            Upd.Where("IdProveedor = @IdProveedor")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeProveedor.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProveedor.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProveedor.IdProveedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProveedor.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProveedor.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeProveedor.Telefono))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeProveedor.Nit))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProveedor.Direccion)))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBeProveedor.Email))
            cmd.Parameters.Add(New SqlParameter("@CONTACTO", oBeProveedor.Contacto))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProveedor.Activo))
            cmd.Parameters.Add(New SqlParameter("@MUESTRA_PRECIO", oBeProveedor.Muestra_precio))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProveedor.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProveedor.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProveedor.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProveedor.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTUALIZA_COSTO_OC", oBeProveedor.Actualiza_costo_oc))
            If Not oBeProveedor.IdUbicacionVirtual = 0 Then cmd.Parameters.Add(New SqlParameter("@IDUBICACIONVIRTUAL", oBeProveedor.IdUbicacionVirtual))
            cmd.Parameters.Add(New SqlParameter("@ES_BODEGA_RECEPCION", oBeProveedor.Es_Bodega_Recepcion))
            cmd.Parameters.Add(New SqlParameter("@ES_BODEGA_TRASLADO", oBeProveedor.Es_Bodega_Traslado))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeProveedor.Referencia))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeProveedor.Sistema))
            cmd.Parameters.Add(New SqlParameter("@IDCONFIGURACIONBARRAPALLET", oBeProveedor.IdConfiguracionBarraPallet))
            cmd.Parameters.Add(New SqlParameter("@ES_PROVEEDOR_SERVICIO", oBeProveedor.Es_Proveedor_Servicio))
            If Not oBeProveedor.IdBodegaAreaSAP = 0 Then cmd.Parameters.Add(New SqlParameter("@IDBODEGAAREASAP", oBeProveedor.IdBodegaAreaSAP))
            If Not oBeProveedor.IdPais = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPAIS", oBeProveedor.IdPais))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_EMPRESA_ERP", oBeProveedor.Codigo_Empresa_ERP))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

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

    Public Shared Function Eliminar(ByRef oBeProveedor As clsBeProveedor, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " DELETE FROM Proveedor 
                                   WHERE(IdProveedor = @IdProveedor)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPROVEEDOR", oBeProveedor.IdProveedor))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

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

    Public Shared Function GetAll() As List(Of clsBeProveedor)

        Try

            Dim lReturnList As New List(Of clsBeProveedor)
            Const sp As String = "SELECT * FROM Proveedor"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeProveedor As New clsBeProveedor

            For Each dr As DataRow In dt.Rows

                vBeProveedor = New clsBeProveedor
                Cargar(vBeProveedor, dr)
                lReturnList.Add(vBeProveedor)

            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdProveedor(ByVal IdProveedor As Integer) As clsBeProveedor

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Single_By_IdProveedor = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Proveedor 
                                  WHERE(IdProveedor = @IdProveedor)"

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPROVEEDOR", IdProveedor))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeProveedor As New clsBeProveedor
                Cargar(pBeProveedor, dt.Rows(0))
                Get_Single_By_IdProveedor = pBeProveedor
            End If

            lTransaction.Commit()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Codigo_By_IdProveedorBodega(ByVal IdProveedorBodega As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As String

        Get_Codigo_By_IdProveedorBodega = String.Empty

        Try

            Const sp As String = "SELECT Codigo FROM Proveedor p JOIN proveedor_bodega pb ON p.IdProveedor = pb.IdProveedor
                                  WHERE(pb.IdAsignacion= @IdProveedorBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProveedorBodega", IdProveedorBodega))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Get_Codigo_By_IdProveedorBodega = IIf(IsDBNull(dt.Rows(0).Item("Codigo")), "", dt.Rows(0).Item("Codigo"))
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
End Class
