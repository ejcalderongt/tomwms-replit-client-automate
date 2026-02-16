Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_oc_det_lote

    Public Shared Function MaxID(ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdOrdenCompraDetLote),0) FROM Trans_oc_det_lote"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC20220413: Obtener los lotes activos de la oc_det_lote.
    ''' </summary>
    ''' <param name="IdOrdenCompraEnc"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Friend Shared Function Get_By_IdOrdenCompraEnc(ByVal IdOrdenCompraEnc As Integer,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_oc_det_lote)

        Try

            Dim lReturnList As New List(Of clsBeTrans_oc_det_lote)
            Const sp As String = "SELECT * FROM Trans_oc_det_lote 
                                  WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc AND Activo = 1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraEnc)
            dad.Fill(dt)

            Dim vBeTrans_oc_det_lote As New clsBeTrans_oc_det_lote

            For Each dr As DataRow In dt.Rows
                vBeTrans_oc_det_lote = New clsBeTrans_oc_det_lote
                Cargar(vBeTrans_oc_det_lote, dr)
                If Not vBeTrans_oc_det_lote.Presentacion.IdPresentacion = 0 Then
                    clsLnProducto_presentacion.GetSingle(vBeTrans_oc_det_lote.Presentacion, lConnection, lTransaction)
                End If
                vBeTrans_oc_det_lote.UnidadMedida = clsLnUnidad_medida.GetSingle(vBeTrans_oc_det_lote.UnidadMedida.IdUnidadMedida, lConnection, lTransaction)
                lReturnList.Add(vBeTrans_oc_det_lote)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Registrar_Lote_Documento_Ingreso(ByVal pNoDocumentoIngreso As String,
                                                            ByVal pNoLinea As Integer,
                                                            ByVal pCodigoProducto As String,
                                                            ByVal pCantidad As Double,
                                                            ByVal pFechaVence As Date,
                                                            ByVal pLote As String,
                                                            ByVal pLicencia As String,
                                                            ByVal pUbicacionNAV As String) As Boolean

        Registrar_Lote_Documento_Ingreso = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim vCantidadEnteraPres As Integer = 0
        Dim vCantidadDecimalUMBas As Double = 0
        Dim vCantidadSolicitadaPedido As Double
        Dim pBeConfigEnc As New clsBeI_nav_config_enc
        Dim BePres As New clsBeProducto_Presentacion

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim BeTransOcEnc As New clsBeTrans_oc_enc()
            BeTransOcEnc = clsLnTrans_oc_enc.Get_Single_By_NoDocumento(pNoDocumentoIngreso, lConnection, lTransaction)

            If BeTransOcEnc Is Nothing Then
                Throw New Exception("El número de documento de ingreso no existe en WMS.")
            Else

                Dim BeProducto As New clsBeProducto()
                BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pCodigoProducto, BeTransOcEnc.IdBodega, lConnection, lTransaction)

                If BeProducto Is Nothing Then
                    Throw New Exception("El código de material: " & pCodigoProducto & " no existe en WMS.")
                Else

                    Dim IdProductoBodega As Integer = clsLnProducto.Get_IdProductoBodega_By_IdProducto_And_IdBodega(BeProducto.IdProducto,
                                                                                                                    BeTransOcEnc.IdBodega,
                                                                                                                    lConnection, lTransaction)

                    If IdProductoBodega = 0 Then
                        Throw New Exception("El código de material: " & pCodigoProducto & " no está asociado a la bodega: " & BeTransOcEnc.IdBodega & " en WMS.")
                    Else

                        Dim BeTransOcDet As New clsBeTrans_oc_det()
                        BeTransOcDet = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_Linea(BeTransOcEnc.IdOrdenCompraEnc,
                                                                                                  pNoLinea,
                                                                                                  IdProductoBodega,
                                                                                                  lConnection,
                                                                                                  lTransaction)

                        Dim vMaxIdOrdenCompraDetLote As Integer = MaxID(lConnection, lTransaction) + 1
                        Dim BeINavBarraPallet As New clsBeI_nav_barras_pallet
                        Dim lMaxIdPallet As Integer = clsLnI_nav_barras_pallet.MaxID(lConnection, lTransaction) + 1
                        Dim vCodigoBodega As String = clsLnBodega.Get_Codigo_By_IdBodega(BeTransOcEnc.IdBodega, lConnection, lTransaction)

                        If Not BeTransOcDet Is Nothing Then

                            Dim BeTransOcDetLote As New clsBeTrans_oc_det_lote()
                            BeTransOcDetLote.IdOrdenCompraDetLote = vMaxIdOrdenCompraDetLote
                            BeTransOcDetLote.IdOrdenCompraEnc = BeTransOcEnc.IdOrdenCompraEnc
                            BeTransOcDetLote.IdOrdenCompraDet = BeTransOcDet.IdOrdenCompraDet
                            BeTransOcDetLote.IdProductoBodega = IdProductoBodega
                            BeTransOcDetLote.Lote = pLote
                            BeTransOcDetLote.No_linea = pNoLinea
                            BeTransOcDetLote.Fecha_vence = pFechaVence
                            BeTransOcDetLote.Codigo_producto = pCodigoProducto
                            BeTransOcDetLote.Lic_Plate = pLicencia
                            BeTransOcDetLote.Cantidad = pCantidad
                            BeTransOcDetLote.Ubicacion = pUbicacionNAV
                            BeTransOcDetLote.IdUnidadMedidaBasica = BeTransOcDet.IdPresentacion
                            BeTransOcDetLote.IdPresentacion = BeTransOcDet.IdPresentacion
                            BeTransOcDetLote.Presentacion = BeTransOcDet.Presentacion
                            BeTransOcDetLote.UnidadMedida = BeTransOcDet.UnidadMedida
                            BeTransOcDetLote.Activo = True
                            BeTransOcDetLote.No_Documento = pNoDocumentoIngreso

                            If BeTransOcDetLote.IdPresentacion <> 0 Then

                                BePres = New clsBeProducto_Presentacion()
                                BePres.IdPresentacion = BeTransOcDetLote.IdPresentacion

                                Dim vIdProducto As Integer = clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(BeTransOcDet.IdProductoBodega, lConnection, lTransaction)

                                BePres = clsLnProducto_presentacion.Get_IdPresentacion_By_IdPresentacion_And_IdProducto(BeTransOcDetLote.IdPresentacion,
                                                                                                                        vIdProducto,
                                                                                                                        lConnection,
                                                                                                                        lTransaction)

                                If BePres Is Nothing Then
                                    Throw New Exception("ERROR_202210202141: No se pudo obtener la presentación con identificador: " & BeTransOcDetLote.IdPresentacion & " para el producto al registrar el lote en el documento de ingreso.")
                                End If

                            Else
                                BePres = Nothing
                            End If

                            Dim pIdEmpresa As Integer
                            pIdEmpresa = clsLnPropietario_bodega.GetIdEmpresa_By_IdPropietarioBodega(BeTransOcEnc.IdPropietarioBodega,
                                                                                                     lConnection,
                                                                                                     lTransaction)


                            pBeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(BeTransOcEnc.IdBodega,
                                                                                                      pIdEmpresa,
                                                                                                      lConnection,
                                                                                                      lTransaction)

                            If Not pBeConfigEnc Is Nothing Then

                                If BePres IsNot Nothing Then

                                    If (pBeConfigEnc.Convertir_decimales_a_umbas = 1) Then

                                        If BePres.Factor > 0 Then
                                            Split_Decimal(BeTransOcDetLote.Cantidad / BePres.Factor, vCantidadEnteraPres, vCantidadDecimalUMBas)

                                            '#EJC20190602_0137AM: Multiplicar la parte decimal por el factor, para obtener la cantidad de unidades de medida básica.
                                            vCantidadDecimalUMBas = Math.Round(vCantidadDecimalUMBas * BePres.Factor)
                                            vCantidadEnteraPres = vCantidadEnteraPres * BePres.Factor

                                            If vCantidadEnteraPres > 0 Then
                                                vCantidadSolicitadaPedido = vCantidadEnteraPres
                                            Else
                                                vCantidadSolicitadaPedido = vCantidadDecimalUMBas
                                            End If

                                        Else
                                            Throw New Exception("ERROR_202210251745: El factor es 0 para la presentación con identificador: " & BeTransOcDetLote.IdPresentacion & " para el producto al registrar el lote en el documento de ingreso.")
                                        End If

                                    Else
                                        vCantidadSolicitadaPedido = BeTransOcDetLote.Cantidad
                                    End If

                                Else
                                    vCantidadEnteraPres = BeTransOcDetLote.Cantidad
                                    vCantidadSolicitadaPedido = BeTransOcDetLote.Cantidad
                                End If

                            Else
                                vCantidadSolicitadaPedido = BeTransOcDetLote.Cantidad
                            End If

                            If vCantidadEnteraPres > 0 Then

                                BeTransOcDetLote.Cantidad = vCantidadEnteraPres
                                Insertar(BeTransOcDetLote,
                                         lConnection,
                                         lTransaction)

                            End If

                            If Not pBeConfigEnc Is Nothing Then

                                If pBeConfigEnc.Convertir_decimales_a_umbas = 1 Then

                                    If vCantidadDecimalUMBas > 0 Then

                                        '#EJC20220314: Validar si ya existe la línea en umbas prior to insert.
                                        Dim BeTransOcDetUMBas As New clsBeTrans_oc_det()
                                        BeTransOcDetUMBas = clsLnTrans_oc_det.Get_Single_By_IdOrdenCompraEnc_And_IdProductoBodega(BeTransOcEnc.IdOrdenCompraEnc,
                                                                                                                                   IdProductoBodega,
                                                                                                                                   lConnection,
                                                                                                                                   lTransaction)

                                        Dim vMaxIdOrdenCompraDet As Integer = clsLnTrans_oc_det.MaxID(BeTransOcDet.IdOrdenCompraEnc,
                                                                                               lConnection,
                                                                                               lTransaction) + 1

                                        Dim vMaxIdLinea As Integer = clsLnTrans_oc_det.Max_No_Linea(BeTransOcDet.IdOrdenCompraEnc,
                                                                                                    lConnection,
                                                                                                    lTransaction) + 10000

                                        If BeTransOcDetUMBas Is Nothing Then

                                            '#CKFK20220131 Insertar el nuevo registro en la trans_oc_det sin presentación
                                            BeTransOcDet.Cantidad = vCantidadDecimalUMBas
                                            BeTransOcDet.Cantidad_recibida = 0
                                            BeTransOcDet.IdPresentacion = 0
                                            BeTransOcDet.Presentacion.IdPresentacion = 0
                                            BeTransOcDet.No_Linea = vMaxIdLinea
                                            BeTransOcDet.IdOrdenCompraDet = vMaxIdOrdenCompraDet
                                            BeTransOcDetLote.Activo = True
                                            BeTransOcDetLote.No_Documento = pNoDocumentoIngreso
                                            clsLnTrans_oc_det.Insertar(BeTransOcDet, lConnection, lTransaction)

                                        Else

                                            vMaxIdOrdenCompraDet = BeTransOcDetUMBas.IdOrdenCompraDet
                                            vMaxIdLinea = BeTransOcDetUMBas.No_Linea
                                            BeTransOcDetUMBas.Cantidad += vCantidadDecimalUMBas
                                            clsLnTrans_oc_det.Actualizar_Cantidad(BeTransOcDetUMBas,
                                                                                  lConnection,
                                                                                  lTransaction)
                                        End If

                                        '#CKFK20220131 Insertar el nuevo registro en la trans_oc_det_lote sin presentación
                                        BeTransOcDetLote.Cantidad = vCantidadDecimalUMBas
                                        BeTransOcDetLote.Presentacion.IdPresentacion = 0
                                        BeTransOcDetLote.IdPresentacion = 0

                                        If vCantidadEnteraPres = 0 Then
                                            BeTransOcDetLote.Lic_Plate = BeTransOcDetLote.Lic_Plate
                                        Else
                                            BeTransOcDetLote.Lic_Plate = "0"
                                        End If

                                        BeTransOcDetLote.IdOrdenCompraDetLote = vMaxIdOrdenCompraDetLote + 2
                                        BeTransOcDetLote.No_linea = vMaxIdLinea
                                        BeTransOcDetLote.IdOrdenCompraDet = vMaxIdOrdenCompraDet
                                        BeTransOcDetLote.Activo = True
                                        BeTransOcDetLote.No_Documento = pNoDocumentoIngreso

                                        Insertar(BeTransOcDetLote,
                                                 lConnection,
                                                 lTransaction)

                                    End If

                                End If

                            End If

                            'Es una barra existente en la tabla intermedia MI3 se borró. por eso.
                            BeINavBarraPallet = New clsBeI_nav_barras_pallet
                            BeINavBarraPallet.IdPallet = lMaxIdPallet
                            BeINavBarraPallet.Codigo = pCodigoProducto
                            BeINavBarraPallet.Nombre = BeProducto.Nombre
                            BeINavBarraPallet.Camas_Por_Tarima = 0
                            BeINavBarraPallet.Cajas_Por_Cama = 0
                            BeINavBarraPallet.Cantidad_Presentacion = vCantidadSolicitadaPedido
                            BeINavBarraPallet.UM_Producto = BeProducto.UnidadMedida.Codigo
                            BeINavBarraPallet.Lote = pLote
                            BeINavBarraPallet.Fecha_Agregado = Now
                            BeINavBarraPallet.Fecha_Ingreso = Now
                            BeINavBarraPallet.Fecha_Vence = pFechaVence
                            BeINavBarraPallet.Fecha_Produccion = Now
                            BeINavBarraPallet.Activo = True
                            BeINavBarraPallet.Recibido = 0
                            BeINavBarraPallet.IdRecepcion = 0
                            BeINavBarraPallet.Bodega_Origen = "PRD"
                            BeINavBarraPallet.Bodega_Destino = vCodigoBodega
                            BeINavBarraPallet.Codigo_barra = pLicencia
                            BeINavBarraPallet.Cantidad_UMP = pCantidad
                            BeINavBarraPallet.Lote_Numerico = 0
                            BeTransOcDetLote.Activo = True
                            BeTransOcDetLote.No_Documento = pNoDocumentoIngreso

                            clsLnI_nav_barras_pallet.Insertar(BeINavBarraPallet,
                                                              lConnection,
                                                              lTransaction)

                            Registrar_Lote_Documento_Ingreso = True

                        Else
                            Throw New Exception("No se encontró la línea del documento: " & pNoLinea & " con material: " & pCodigoProducto & " en documento de ingreso: " & pNoDocumentoIngreso)
                        End If

                    End If

                End If

            End If

            lTransaction.Commit()

        Catch ex1 As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Actualizar_Licencia_OP(ByVal pNoDocumentoIngreso As String,
                                                  ByVal pNoLinea As Integer,
                                                  ByVal pCodigoProducto As String,
                                                  ByVal pLicencia As String,
                                                  ByVal pFechaVence As Date,
                                                  ByVal pLote As String,
                                                  ByVal pUbicacionNAV As String) As Boolean

        Actualizar_Licencia_OP = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vUbicacionAnterior As String = ""

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim BeTransOcEnc As New clsBeTrans_oc_enc()
            BeTransOcEnc = clsLnTrans_oc_enc.Get_Single_By_NoDocumento(pNoDocumentoIngreso, lConnection, lTransaction)

            If BeTransOcEnc Is Nothing Then
                Throw New Exception("El número de documento de ingreso no existe en WMS.")
            Else

                Dim BeProducto As New clsBeProducto()
                BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pCodigoProducto,
                                                                    BeTransOcEnc.IdBodega,
                                                                    lConnection,
                                                                    lTransaction)

                If BeProducto Is Nothing Then
                    Throw New Exception("El código de material: " & pCodigoProducto & " no existe en WMS.")
                Else

                    Dim IdProductoBodega As Integer = clsLnProducto.Get_IdProductoBodega_By_IdProducto_And_IdBodega(BeProducto.IdProducto,
                                                                                                                    BeTransOcEnc.IdBodega,
                                                                                                                    lConnection, lTransaction)

                    If IdProductoBodega = 0 Then
                        Throw New Exception("El código de material: " & pCodigoProducto & " no está asociado a la bodega: " & BeTransOcEnc.IdBodega & " en WMS.")
                    Else

                        Dim BeTransOcDetLote As New clsBeTrans_oc_det_lote()
                        BeTransOcDetLote = Get_By_OP_Licencia_Linea_Prod(BeTransOcEnc.IdOrdenCompraEnc,
                                                                         pLicencia,
                                                                         pNoLinea,
                                                                         IdProductoBodega,
                                                                         lConnection,
                                                                         lTransaction)

                        If Not BeTransOcDetLote Is Nothing Then

                            vUbicacionAnterior = BeTransOcDetLote.Ubicacion

                            BeTransOcDetLote.Lote = pLote
                            BeTransOcDetLote.Fecha_vence = pFechaVence
                            BeTransOcDetLote.Ubicacion = pUbicacionNAV

                            Actualizar_Ubic_FechaV_Lote(BeTransOcDetLote, lConnection, lTransaction)

                            '#EJC202210261131:No entinendo para que haces esto, si esa tabla no tiene esos campos (ubicacion, licencia)
                            '#CKFK20221026 Corregido el error
                            clsLnI_nav_transacciones_push.Actualizar_Ubic_FechaV_Lote(BeTransOcDetLote,
                                                                                      vUbicacionAnterior,
                                                                                      lConnection,
                                                                                      lTransaction)

                            Actualizar_Licencia_OP = True

                        Else
                            Throw New Exception("No se encontró la línea del documento: " & pNoLinea & " con material: " & pCodigoProducto & " en documento de ingreso: " & pNoDocumentoIngreso)
                        End If

                    End If

                End If

            End If

            lTransaction.Commit()

        Catch ex1 As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Guarda_Trans_re_det_lote(ByVal pOcDetLote As clsBeTrans_oc_det_lote,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As Integer

        Guarda_Trans_re_det_lote = 0

        Dim vResult As Integer = 0

        Try

            If Not pOcDetLote Is Nothing Then

                If pOcDetLote.IsNew Then

                    Dim vOcDetLote As New clsBeTrans_oc_det_lote()

                    vOcDetLote = pOcDetLote.Clone()

                    If GetSingle(vOcDetLote,
                                 lConnection,
                                 lTransaction) Then

                        pOcDetLote.Cantidad_recibida += vOcDetLote.Cantidad_recibida
                        vResult = Actualizar_Parcial(pOcDetLote, lConnection, lTransaction)

                    End If

                End If

            End If

            Guarda_Trans_re_det_lote = vResult

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Parcial(ByRef oBeTrans_oc_det_lote As clsBeTrans_oc_det_lote,
                                              Optional ByVal pConection As SqlConnection = Nothing,
                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_det_lote")
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc" &
                      " AND IdOrdenCompraDet = @IdOrdenCompraDet" &
                      " AND IdOrdenCompraDetLote = @IdOrdenCompraDetLote")

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

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det_lote.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det_lote.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADETLOTE", oBeTrans_oc_det_lote.IdOrdenCompraDetLote))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_oc_det_lote.Cantidad_recibida))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Cantidad_Recibida(ByRef oBeTrans_oc_det_lote As clsBeTrans_oc_det_lote,
                                                         Optional ByVal pConection As SqlConnection = Nothing,
                                                         Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_det_lote")
            Upd.Add("cantidad_recibida", "@cantidad_recibida", DataType.Parametro)
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc AND
                       IdOrdenCompraDet = @IdOrdenCompraDet AND
                       Lote = @Lote AND
                       fecha_vence = @fecha_vence AND
                       (lic_plate = @lic_plate  or lic_plate is null)")

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

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det_lote.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det_lote.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_oc_det_lote.Lote))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD_RECIBIDA", oBeTrans_oc_det_lote.Cantidad_recibida))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_oc_det_lote.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", oBeTrans_oc_det_lote.Lic_Plate))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Ubic_FechaV_Lote(ByRef oBeTrans_oc_det_lote As clsBeTrans_oc_det_lote,
                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_det_lote")
            Upd.Add("Ubicacion", "@Ubicacion", DataType.Parametro)
            Upd.Add("Lote", "@Lote", DataType.Parametro)
            Upd.Add("Fecha_Vence", "@Fecha_Vence", DataType.Parametro)
            Upd.Where("IdOrdenCompraEnc = @IdOrdenCompraEnc" &
                      " AND IdOrdenCompraDet = @IdOrdenCompraDet" &
                      " AND IdOrdenCompraDetLote = @IdOrdenCompraDetLote" &
                      " AND lic_plate = @Licencia")

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

            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRAENC", oBeTrans_oc_det_lote.IdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADET", oBeTrans_oc_det_lote.IdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@IDORDENCOMPRADETLOTE", oBeTrans_oc_det_lote.IdOrdenCompraDetLote))
            cmd.Parameters.Add(New SqlParameter("@UBICACION", oBeTrans_oc_det_lote.Ubicacion))
            cmd.Parameters.Add(New SqlParameter("@LOTE", oBeTrans_oc_det_lote.Lote))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_oc_det_lote.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@LICENCIA", oBeTrans_oc_det_lote.Lic_Plate))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Sub Split_Decimal(ByVal number As Decimal,
                                     ByRef wholePart As Decimal,
                                     ByRef fractionalPart As Decimal)

        Try

            wholePart = Math.Truncate(number)
            fractionalPart = number - wholePart

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_Single_By_Licencia(ByVal pLicencia As String) As clsBeTrans_oc_det_lote

        Get_Single_By_Licencia = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Trans_oc_det_lote 
                                  Where(lic_plate = @lic_plate) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", pLicencia))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_oc_det_lote As New clsBeTrans_oc_det_lote
                Cargar(pBeTrans_oc_det_lote, dt.Rows(0))
                Get_Single_By_Licencia = pBeTrans_oc_det_lote
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Existe_Documento(ByVal pNoDocumentoIngreso As String) As Boolean

        Existe_Documento = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim BeTransOcEnc As New clsBeTrans_oc_enc()
            BeTransOcEnc = clsLnTrans_oc_enc.Get_Single_By_NoDocumento(pNoDocumentoIngreso,
                                                                       lConnection,
                                                                       lTransaction)

            If BeTransOcEnc IsNot Nothing Then
                Existe_Documento = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Desactivar_Ubicacion_Documento_Ingreso(ByVal pNoDocumento As String,
                                                                  ByVal pUbicacion As String,
                                                                  Optional ByVal pConection As SqlConnection = Nothing,
                                                                  Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_oc_det_lote")
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Where("no_documento = @no_documento " &
                      " AND ubicacion = @ubicacion")

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

            cmd.Parameters.Add(New SqlParameter("@NO_DOCUMENTO", pNoDocumento))
            cmd.Parameters.Add(New SqlParameter("@UBICACION", pUbicacion))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Exist(ByVal pOCEncabezado As Integer,
                                 ByVal pNoLinea As Integer,
                                 ByVal pOCDetalleLote As Integer,
                                 ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction) As clsBeTrans_oc_det_lote

        Exist = Nothing

        Try

            Const sp As String = "Select * from trans_oc_det_lote
                        Where IdOrdenCompraEnc = @pOCEncabezado  
                        And No_Linea = @No_Linea
                        And IdOrdenCompraDetLote = @pOCDetalleLote"

            Dim cmd As New SqlCommand(sp, pConnection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@pOCEncabezado", pOCEncabezado))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@No_Linea", pNoLinea))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@pOCDetalleLote", pOCDetalleLote))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then

                Dim lRow As DataRow = dt.Rows(0)
                Dim ObjOCDetLote As New clsBeTrans_oc_det_lote()

                Cargar(ObjOCDetLote, lRow)

                Return ObjOCDetLote

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function ExistLote(ByVal pOCEncabezado As Integer,
                                     ByVal pNoLinea As Integer,
                                     ByVal pLote As String,
                                     ByVal pConnection As SqlConnection,
                                     ByVal pTransaction As SqlTransaction) As clsBeTrans_oc_det_lote

        ExistLote = Nothing

        Try

            Const sp As String = "Select * from trans_oc_det_lote
                        Where IdOrdenCompraEnc = @pOCEncabezado  
                        And No_Linea = @No_Linea
                        And Lote = @Lote"

            Dim cmd As New SqlCommand(sp, pConnection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@pOCEncabezado", pOCEncabezado))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@No_Linea", pNoLinea))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Lote", pLote))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then

                Dim lRow As DataRow = dt.Rows(0)
                Dim ObjOCDetLote As New clsBeTrans_oc_det_lote()

                Cargar(ObjOCDetLote, lRow)

                Return ObjOCDetLote

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_By_OP_Licencia_Linea_Prod(ByVal pIdOrdenCompraEnc As Integer,
                                                         ByVal pLicencia As String,
                                                         ByVal pNoLinea As Integer,
                                                         ByVal pIdProductoBodega As Integer,
                                                         ByVal pConnection As SqlConnection,
                                                         ByVal pTransaction As SqlTransaction) As clsBeTrans_oc_det_lote

        Get_By_OP_Licencia_Linea_Prod = Nothing

        Try

            Const sp As String = "Select * from trans_oc_det_lote
                                  Where IdOrdenCompraEnc = @IdOrdenCompraEnc AND
                                        No_Linea = @No_Linea AND
                                        IdProductoBodega = @IdProductoBodega AND
                                        lic_plate = @Licencia"

            Dim cmd As New SqlCommand(sp, pConnection) With {.CommandType = CommandType.Text, .Transaction = pTransaction}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", pIdOrdenCompraEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@No_Linea", pNoLinea))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", pIdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Licencia", pLicencia))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count >= 1 Then

                Dim lRow As DataRow = dt.Rows(0)
                Dim ObjOCDetLote As New clsBeTrans_oc_det_lote()

                Cargar(ObjOCDetLote, lRow)

                Return ObjOCDetLote

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Detalle_Lotes_OC_By_RecepcionDet(ByVal BeTransReDet As clsBeTrans_re_det,
                                                                Optional ByVal pConnection As SqlConnection = Nothing,
                                                                Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of clsBeTrans_oc_det_lote)

        Dim lReturnList As New List(Of clsBeTrans_oc_det_lote)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

        Try

            Dim vSQL As String = "SELECT * 
                                  FROM trans_oc_det_lote
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc  
                                        And IdProductoBodega=@IdProductoBodega 
                                        And no_linea = @NoLinea 
                                        And IdOrdenCompraDet = @IdOrdenCompraDet    
                                        And (lic_plate = @Lic_Plate OR lic_plate = '')
                                        And lote = @Lote  
                                        And fecha_vence =@Fecha_Vence"

            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQL, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", BeTransReDet.IdOrdenCompraEnc)
            lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", BeTransReDet.IdProductoBodega)
            lDTA.SelectCommand.Parameters.AddWithValue("@NoLinea", BeTransReDet.No_Linea)
            lDTA.SelectCommand.Parameters.AddWithValue("@Lote", BeTransReDet.Lote)
            lDTA.SelectCommand.Parameters.AddWithValue("@Lic_Plate", BeTransReDet.Lic_plate)
            lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence", BeTransReDet.Fecha_vence)
            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraDet", BeTransReDet.IdOrdenCompraDet)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            Dim Obj As clsBeTrans_oc_det_lote

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                For Each lRow As DataRow In lDataTable.Rows

                    Obj = New clsBeTrans_oc_det_lote
                    Cargar(Obj, lRow)
                    Obj.IsNew = False
                    lReturnList.Add(Obj)

                Next

            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Detalle_Lotes_OC_By_IdOrdenCompraEnc(ByVal IdOrdenCompraEnc As Integer,
                                                                    Optional ByVal pConnection As SqlConnection = Nothing,
                                                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As List(Of clsBeTrans_oc_det_lote)

        Dim lReturnList As New List(Of clsBeTrans_oc_det_lote)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

        Try

            Dim vSQL As String = "SELECT * 
                                  FROM trans_oc_det_lote
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc"

            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQL, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraEnc)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            Dim Obj As clsBeTrans_oc_det_lote

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                For Each lRow As DataRow In lDataTable.Rows

                    Obj = New clsBeTrans_oc_det_lote
                    Cargar(Obj, lRow)
                    Obj.IsNew = False
                    lReturnList.Add(Obj)

                Next

            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Ubicacion_By_BeTransReDet(ByVal BeTransReDet As clsBeTrans_re_det,
                                                         Optional ByVal pConnection As SqlConnection = Nothing,
                                                         Optional ByVal pTransaction As SqlTransaction = Nothing) As String

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        Dim ubicacion As String = ""

        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

        Try

            Dim vSQL As String = "SELECT ubicacion
                                  FROM trans_oc_det_lote
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc  
                                        And IdProductoBodega=@IdProductoBodega 
                                        And no_linea = @NoLinea  
                                        And lic_plate = @Lic_Plate  
                                        And lote = @Lote  
                                        And fecha_vence =@Fecha_Vence"

            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQL, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQL, lConnection)
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", BeTransReDet.IdOrdenCompraEnc)
            lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", BeTransReDet.IdProductoBodega)
            lDTA.SelectCommand.Parameters.AddWithValue("@NoLinea", BeTransReDet.No_Linea)
            lDTA.SelectCommand.Parameters.AddWithValue("@Lote", BeTransReDet.Lote)
            lDTA.SelectCommand.Parameters.AddWithValue("@Lic_Plate", BeTransReDet.Lic_plate)
            lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence", BeTransReDet.Fecha_vence)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                ubicacion = lDataTable.Rows(0)("ubicacion")
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return ubicacion

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Lotes_By_IdOrdenCompraEnc_And_IdOrdenCompraDet(ByVal IdOrdenCompraEnc As Integer,
                                                                              ByVal IdOrdenCompraDet As Integer,
                                                                              Optional ByVal pConnection As SqlConnection = Nothing,
                                                                              Optional ByVal pTransaction As SqlTransaction = Nothing) As DataTable

        Dim lReturnList As New List(Of clsBeTrans_oc_det_lote)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

        Get_Lotes_By_IdOrdenCompraEnc_And_IdOrdenCompraDet = Nothing

        Try

            Dim vSQL As String = "SELECT IdOrdenCompraDetLote as IdLote,lote, fecha_vence,cantidad 
                                  FROM trans_oc_det_lote
                                  WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc AND IdOrdenCompraDet = @IdOrdenCompraDet "

            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQL, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQL, lConnection)
                lDTA.SelectCommand.Transaction = lTransaction
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraEnc)
            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraDet", IdOrdenCompraDet)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            Get_Lotes_By_IdOrdenCompraEnc_And_IdOrdenCompraDet = lDataTable

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

End Class
