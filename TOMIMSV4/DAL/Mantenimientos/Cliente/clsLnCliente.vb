Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnCliente

    Public Shared lClientesInMemory As New List(Of clsBeCliente)

    Public Shared Sub Cargar(ByRef oBeCliente As clsBeCliente, ByRef dr As DataRow)

        Try

            With oBeCliente

                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                .IdTipoCliente = IIf(IsDBNull(dr.Item("IdTipoCliente")), 0, dr.Item("IdTipoCliente"))
                .IdUbicacionManufactura = IIf(IsDBNull(dr.Item("IdUbicacionManufactura")), 0, dr.Item("IdUbicacionManufactura"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Nombre_comercial = IIf(IsDBNull(dr.Item("nombre_comercial")), "", dr.Item("nombre_comercial"))
                .Nombre_contacto = IIf(IsDBNull(dr.Item("nombre_contacto")), "", dr.Item("nombre_contacto"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .Nit = IIf(IsDBNull(dr.Item("nit")), "", dr.Item("nit"))
                .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
                .Correo_electronico = IIf(IsDBNull(dr.Item("correo_electronico")), "", dr.Item("correo_electronico"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Realiza_manufactura = IIf(IsDBNull(dr.Item("realiza_manufactura")), False, dr.Item("realiza_manufactura"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Despachar_lotes_completos = IIf(IsDBNull(dr.Item("despachar_lotes_completos")), False, dr.Item("despachar_lotes_completos"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .Es_bodega_recepcion = IIf(IsDBNull(dr.Item("es_bodega_recepcion")), False, dr.Item("es_bodega_recepcion"))
                .Es_Bodega_Traslado = IIf(IsDBNull(dr.Item("es_bodega_traslado")), False, dr.Item("es_bodega_traslado"))
                .IdUbicacionVirtual = IIf(IsDBNull(dr.Item("idubicacionvirtual")), 0, dr.Item("idubicacionvirtual"))
                .Referencia = IIf(IsDBNull(dr.Item("Referencia")), "", dr.Item("Referencia"))
                .Control_Ultimo_Lote = IIf(IsDBNull(dr.Item("control_ultimo_lote")), False, dr.Item("control_ultimo_lote"))
                .Control_Calidad = IIf(IsDBNull(dr.Item("Control_Calidad")), False, dr.Item("Control_Calidad"))
                .IdUbicacionAbastecerCon = IIf(IsDBNull(dr.Item("IdUbicacionAbastecerCon")), 0, dr.Item("IdUbicacionAbastecerCon"))
                .IdBodegaAreaSAP = IIf(IsDBNull(dr.Item("IdBodegaAreaSAP")), 0, dr.Item("IdBodegaAreaSAP"))
                .Es_Proveedor = IIf(IsDBNull(dr.Item("Es_proveedor")), False, dr.Item("Es_proveedor"))
                .Codigo_Empresa_ERP = IIf(IsDBNull(dr.Item("Codigo_Empresa_ERP")), "", dr.Item("Codigo_Empresa_ERP"))
                .IdProductoEstadoDefecto = IIf(IsDBNull(dr.Item("IdProductoEstadoDefecto")), 0, dr.Item("IdProductoEstadoDefecto"))
            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Insertars the specified o be cliente.
    ''' </summary>
    ''' <param name="oBeCliente">The o be cliente.</param>
    ''' <param name="pConection">The p conection.</param>
    ''' <param name="pTransaction">The p transaction.</param>
    ''' <returns></returns>
    ''' <exception cref="Exception"></exception>
    Public Shared Function Insertar(ByRef oBeCliente As clsBeCliente, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("cliente")
            Ins.Add("idcliente", "@idcliente", DataType.Parametro)
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Ins.Add("idtipocliente", "@idtipocliente", DataType.Parametro)
            Ins.Add("idubicacionmanufactura", "@idubicacionmanufactura", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre_comercial", "@nombre_comercial", DataType.Parametro)
            Ins.Add("nombre_contacto", "@nombre_contacto", DataType.Parametro)
            Ins.Add("telefono", "@telefono", DataType.Parametro)
            Ins.Add("nit", "@nit", DataType.Parametro)
            Ins.Add("direccion", "@direccion", DataType.Parametro)
            Ins.Add("correo_electronico", "@correo_electronico", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("realiza_manufactura", "@realiza_manufactura", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("despachar_lotes_completos", "@despachar_lotes_completos", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("es_bodega_recepcion", "@es_bodega_recepcion", DataType.Parametro)
            Ins.Add("es_bodega_traslado", "@es_bodega_traslado", DataType.Parametro)
            Ins.Add("idubicacionvirtual", "@idubicacionvirtual", DataType.Parametro)
            Ins.Add("referencia", "@referencia", DataType.Parametro)
            Ins.Add("control_ultimo_lote", "@control_ultimo_lote", DataType.Parametro)
            Ins.Add("control_calidad", "@control_calidad", DataType.Parametro)
            Ins.Add("IdUbicacionAbastecerCon", "@IdUbicacionAbastecerCon", DataType.Parametro)
            Ins.Add("IdBodegaAreaSAP", "@IdBodegaAreaSAP", DataType.Parametro)
            Ins.Add("Es_proveedor", "@Es_proveedor", DataType.Parametro)
            Ins.Add("Codigo_Empresa_ERP", "@Codigo_Empresa_ERP", DataType.Parametro)
            Ins.Add("idproductoestadodefecto", "@idproductoestadodefecto", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeCliente.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeCliente.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOCLIENTE", oBeCliente.IdTipoCliente))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONMANUFACTURA", oBeCliente.IdUbicacionManufactura))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeCliente.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_COMERCIAL", clsPublic.Quitar_Caracteres_No_Permitidos(oBeCliente.Nombre_comercial)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_CONTACTO", oBeCliente.Nombre_contacto))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeCliente.Telefono))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeCliente.Nit))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeCliente.Direccion))
            cmd.Parameters.Add(New SqlParameter("@CORREO_ELECTRONICO", oBeCliente.Correo_electronico))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCliente.Activo))
            cmd.Parameters.Add(New SqlParameter("@REALIZA_MANUFACTURA", oBeCliente.Realiza_manufactura))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCliente.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCliente.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCliente.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCliente.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DESPACHAR_LOTES_COMPLETOS", oBeCliente.Despachar_lotes_completos))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeCliente.Sistema))
            cmd.Parameters.Add(New SqlParameter("@ES_BODEGA_RECEPCION", oBeCliente.Es_bodega_recepcion))
            cmd.Parameters.Add(New SqlParameter("@ES_BODEGA_TRASLADO", oBeCliente.Es_Bodega_Traslado))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONVIRTUAL", IIf(IsDBNull(oBeCliente.IdUbicacionVirtual), Nothing, oBeCliente.IdUbicacionVirtual)))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeCliente.Referencia))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_ULTIMO_LOTE", oBeCliente.Control_Ultimo_Lote))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_CALIDAD", oBeCliente.Control_Calidad))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONABASTECERCON", oBeCliente.IdUbicacionAbastecerCon))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAAREASAP", oBeCliente.IdBodegaAreaSAP))
            cmd.Parameters.Add(New SqlParameter("@ES_PROVEEDOR", oBeCliente.Es_Proveedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_EMPRESA_ERP", oBeCliente.Codigo_Empresa_ERP))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADODEFECTO", oBeCliente.IdProductoEstadoDefecto))

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

    ''' <summary>
    ''' Actualizars the specified o be cliente.
    ''' </summary>
    ''' <param name="oBeCliente">The o be cliente.</param>
    ''' <param name="pConection">The p conection.</param>
    ''' <param name="pTransaction">The p transaction.</param>
    ''' <returns></returns>
    ''' <exception cref="Exception"></exception>
    Public Shared Function Actualizar(ByRef oBeCliente As clsBeCliente, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("cliente")
            Upd.Add("idcliente", "@idcliente", DataType.Parametro)
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            Upd.Add("idtipocliente", "@idtipocliente", DataType.Parametro)
            Upd.Add("idubicacionmanufactura", "@idubicacionmanufactura", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre_comercial", "@nombre_comercial", DataType.Parametro)
            Upd.Add("nombre_contacto", "@nombre_contacto", DataType.Parametro)
            Upd.Add("telefono", "@telefono", DataType.Parametro)
            Upd.Add("nit", "@nit", DataType.Parametro)
            Upd.Add("direccion", "@direccion", DataType.Parametro)
            Upd.Add("correo_electronico", "@correo_electronico", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("realiza_manufactura", "@realiza_manufactura", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("despachar_lotes_completos", "@despachar_lotes_completos", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Add("es_bodega_recepcion", "@es_bodega_recepcion", DataType.Parametro)
            Upd.Add("es_bodega_traslado", "@es_bodega_traslado", DataType.Parametro)
            Upd.Add("idubicacionvirtual", "@idubicacionvirtual", DataType.Parametro)
            Upd.Add("referencia", "@referencia", DataType.Parametro)
            Upd.Add("control_ultimo_lote", "@control_ultimo_lote", DataType.Parametro)
            Upd.Add("control_calidad", "@control_calidad", DataType.Parametro)
            Upd.Add("IdUbicacionAbastecerCon", "@IdUbicacionAbastecerCon", DataType.Parametro)
            Upd.Add("IdBodegaAreaSAP", "@IdBodegaAreaSAP", DataType.Parametro)
            Upd.Add("es_proveedor", "@es_proveedor", DataType.Parametro)
            Upd.Add("codigo_empresa_erp", "@codigo_empresa_erp", DataType.Parametro)
            Upd.Add("idproductoestadodefecto", "@idproductoestadodefecto", DataType.Parametro)
            Upd.Where("IdCliente = @IdCliente")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeCliente.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeCliente.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeCliente.IdPropietario))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOCLIENTE", oBeCliente.IdTipoCliente))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONMANUFACTURA", oBeCliente.IdUbicacionManufactura))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeCliente.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_COMERCIAL", clsPublic.Quitar_Caracteres_No_Permitidos(oBeCliente.Nombre_comercial)))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_CONTACTO", oBeCliente.Nombre_contacto))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeCliente.Telefono))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeCliente.Nit))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeCliente.Direccion))
            cmd.Parameters.Add(New SqlParameter("@CORREO_ELECTRONICO", oBeCliente.Correo_electronico))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeCliente.Activo))
            cmd.Parameters.Add(New SqlParameter("@REALIZA_MANUFACTURA", oBeCliente.Realiza_manufactura))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeCliente.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeCliente.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeCliente.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeCliente.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DESPACHAR_LOTES_COMPLETOS", oBeCliente.Despachar_lotes_completos))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeCliente.Sistema))
            cmd.Parameters.Add(New SqlParameter("@ES_BODEGA_RECEPCION", oBeCliente.Es_bodega_recepcion))
            cmd.Parameters.Add(New SqlParameter("@ES_BODEGA_TRASLADO", oBeCliente.Es_Bodega_Traslado))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONVIRTUAL", oBeCliente.IdUbicacionVirtual))
            cmd.Parameters.Add(New SqlParameter("@REFERENCIA", oBeCliente.Referencia))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_ULTIMO_LOTE", oBeCliente.Control_Ultimo_Lote))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_CALIDAD", oBeCliente.Control_Calidad))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONABASTECERCON", oBeCliente.IdUbicacionAbastecerCon))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGAAREASAP", oBeCliente.IdBodegaAreaSAP))
            cmd.Parameters.Add(New SqlParameter("@ES_PROVEEDOR", oBeCliente.Es_Proveedor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_EMPRESA_ERP", oBeCliente.Codigo_Empresa_ERP))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOESTADODEFECTO", oBeCliente.IdProductoEstadoDefecto))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

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

    Public Shared Function GetSingle(ByVal pIdCliente As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeCliente

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Cliente 
                                  Where(IdCliente = @IdCliente) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCLIENTE", pIdCliente))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeCliente As New clsBeCliente
                pBeCliente.IdCliente = pIdCliente
                Cargar(pBeCliente, dt.Rows(0))
                GetSingle = pBeCliente
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Maximums the identifier for a new costumer.
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="Exception"></exception>
    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdCliente),0) FROM Cliente "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

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

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Single_By_IdEmpresa(ByVal IdEmpresa As Integer,
                                                   ByVal IdCliente As Integer) As clsBeCliente

        Get_Single_By_IdEmpresa = Nothing

        Try

            Dim IdxCliente As Integer = -1
            Dim BeCliente As New clsBeCliente()

            IdxCliente = lClientesInMemory.FindIndex(Function(x) x.IdCliente = IdCliente AndAlso x.IdEmpresa = IdEmpresa)

            If IdxCliente = -1 Then

                Dim vSQL As String = "SELECT * from Cliente WHERE IdEmpresa=@IdEmpresa AND IdCliente=@IdCliente AND Activo=1"

                Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", IdCliente)

                        Dim lDT As New DataTable
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then
                            BeCliente = New clsBeCliente()
                            Dim lRow As DataRow = lDT.Rows(0)
                            Cargar(BeCliente, lRow)
                            lClientesInMemory.Add(BeCliente.Clone())
                            'Return BePropietario
                            Get_Single_By_IdEmpresa = BeCliente

                        End If

                    End Using

                End Using

            Else
                BeCliente = New clsBeCliente()
                BeCliente = lClientesInMemory(IdxCliente).Clone()
                Get_Single_By_IdEmpresa = BeCliente
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
