Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnStock_jornada

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdStockJornada),0) FROM Stock_jornada"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

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

    Public Shared Function Insert_Multiple(ByVal lStockJornada As List(Of clsBeStock_jornada)) As Boolean

        Insert_Multiple = False

        Try


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vIdMax = MaxID(lConnection, lTransaction) + 1

                    'For Each S In lStockJornada
                    '	S.IdStockJornada = vIdMax
                    '	Insertar(S, lConnection, lTransaction)
                    '	vIdMax += 1
                    'Next

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Insert_Multiple = True

        Catch ex As Exception
            Throw ex
        End Try


    End Function

    '#EJC20260607_STOCK_JORNADA_BATCH: insercion por lotes reutilizando comando parametrizado.
    Public Shared Function Insert_Multiple_Fast(ByVal pItems As List(Of clsBeStock_jornada),
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As Integer

        Insert_Multiple_Fast = 0

        Try

            If pItems Is Nothing OrElse pItems.Count = 0 Then Return 0

            Const vSql As String = "INSERT INTO stock_jornada (" &
                                   "idstockjornada,idjornadasistema,fecha,idbodega,idstock,idpropietariobodega,idproductobodega,idproductoestado,idpresentacion,idunidadmedida,idubicacion,idubicacion_anterior," &
                                   "idrecepcionenc,idrecepciondet,idpedidoenc,idpickingenc,iddespachoenc,lote,lic_plate,serial,cantidad,fecha_ingreso,fecha_vence,uds_lic_plate,no_bulto,fecha_manufactura," &
                                   "añada,user_agr,fec_agr,user_mod,fec_mod,activo,peso,temperatura,atributo_variante_1,pallet_no_estandar,propietario,proveedor,bodega,idordencompraenc,no_documentooc,no_documentorec," &
                                   "referenciaoc,fecha_recepcion,tipotrans,fecha_agrego,codigo_producto,codigo_barra_producto,nombre_producto,existencia,nom_umbas,nom_estado_producto,nom_presentacion_producto,ubicacion_origen,no_poliza," &
                                   "valor_aduana,valor_fob,valor_iva,valor_dai,valor_seguro,valor_flete,peso_neto,numero_orden,codigo_regimen,nombre_regimen,dias_vencimiento_regimen,fecha_ingreso_ticket_tms,es_retroactivo,factor,camasportarima,cajasporcama," &
                                   "cantidad_ingreso_afecta_a_salida,stock_jornada_hash,idtickettms,idpropietario,idclasificacion,clasificacion,regimen,posiciones,costo_unitario,no_documento_procesado_erp,procesado_erp,fecha_procesado_stock_jornada,IdProductoTallaColor,Talla,Color" &
                                   ") VALUES (" &
                                   "@idstockjornada,@idjornadasistema,@fecha,@idbodega,@idstock,@idpropietariobodega,@idproductobodega,@idproductoestado,@idpresentacion,@idunidadmedida,@idubicacion,@idubicacion_anterior," &
                                   "@idrecepcionenc,@idrecepciondet,@idpedidoenc,@idpickingenc,@iddespachoenc,@lote,@lic_plate,@serial,@cantidad,@fecha_ingreso,@fecha_vence,@uds_lic_plate,@no_bulto,@fecha_manufactura," &
                                   "@añada,@user_agr,@fec_agr,@user_mod,@fec_mod,@activo,@peso,@temperatura,@atributo_variante_1,@pallet_no_estandar,@propietario,@proveedor,@bodega,@idordencompraenc,@no_documentooc,@no_documentorec," &
                                   "@referenciaoc,@fecha_recepcion,@tipotrans,@fecha_agrego,@codigo_producto,@codigo_barra_producto,@nombre_producto,@existencia,@nom_umbas,@nom_estado_producto,@nom_presentacion_producto,@ubicacion_origen,@no_poliza," &
                                   "@valor_aduana,@valor_fob,@valor_iva,@valor_dai,@valor_seguro,@valor_flete,@peso_neto,@numero_orden,@codigo_regimen,@nombre_regimen,@dias_vencimiento_regimen,@fecha_ingreso_ticket_tms,@es_retroactivo,@factor,@camasportarima,@cajasporcama," &
                                   "@cantidad_ingreso_afecta_a_salida,@stock_jornada_hash,@idtickettms,@idpropietario,@idclasificacion,@clasificacion,@regimen,@posiciones,@costo_unitario,@no_documento_procesado_erp,@procesado_erp,@fecha_procesado_stock_jornada,@IdProductoTallaColor,@Talla,@Color)"

            Using cmd As New SqlCommand(vSql, lConnection, lTransaction)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 120

                cmd.Parameters.Add("@idstockjornada", SqlDbType.Int)
                cmd.Parameters.Add("@idjornadasistema", SqlDbType.Int)
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime)
                cmd.Parameters.Add("@idbodega", SqlDbType.Int)
                cmd.Parameters.Add("@idstock", SqlDbType.Int)
                cmd.Parameters.Add("@idpropietariobodega", SqlDbType.Int)
                cmd.Parameters.Add("@idproductobodega", SqlDbType.Int)
                cmd.Parameters.Add("@idproductoestado", SqlDbType.Int)
                cmd.Parameters.Add("@idpresentacion", SqlDbType.Int)
                cmd.Parameters.Add("@idunidadmedida", SqlDbType.Int)
                cmd.Parameters.Add("@idubicacion", SqlDbType.Int)
                cmd.Parameters.Add("@idubicacion_anterior", SqlDbType.Int)
                cmd.Parameters.Add("@idrecepcionenc", SqlDbType.Int)
                cmd.Parameters.Add("@idrecepciondet", SqlDbType.Int)
                cmd.Parameters.Add("@idpedidoenc", SqlDbType.Int)
                cmd.Parameters.Add("@idpickingenc", SqlDbType.Int)
                cmd.Parameters.Add("@iddespachoenc", SqlDbType.Int)
                cmd.Parameters.Add("@lote", SqlDbType.VarChar)
                cmd.Parameters.Add("@lic_plate", SqlDbType.VarChar)
                cmd.Parameters.Add("@serial", SqlDbType.VarChar)
                cmd.Parameters.Add("@cantidad", SqlDbType.Float)
                cmd.Parameters.Add("@fecha_ingreso", SqlDbType.DateTime)
                cmd.Parameters.Add("@fecha_vence", SqlDbType.DateTime)
                cmd.Parameters.Add("@uds_lic_plate", SqlDbType.Float)
                cmd.Parameters.Add("@no_bulto", SqlDbType.Int)
                cmd.Parameters.Add("@fecha_manufactura", SqlDbType.DateTime)
                cmd.Parameters.Add("@añada", SqlDbType.Int)
                cmd.Parameters.Add("@user_agr", SqlDbType.VarChar)
                cmd.Parameters.Add("@fec_agr", SqlDbType.DateTime)
                cmd.Parameters.Add("@user_mod", SqlDbType.VarChar)
                cmd.Parameters.Add("@fec_mod", SqlDbType.DateTime)
                cmd.Parameters.Add("@activo", SqlDbType.Bit)
                cmd.Parameters.Add("@peso", SqlDbType.Float)
                cmd.Parameters.Add("@temperatura", SqlDbType.Float)
                cmd.Parameters.Add("@atributo_variante_1", SqlDbType.VarChar)
                cmd.Parameters.Add("@pallet_no_estandar", SqlDbType.Bit)
                cmd.Parameters.Add("@propietario", SqlDbType.VarChar)
                cmd.Parameters.Add("@proveedor", SqlDbType.VarChar)
                cmd.Parameters.Add("@bodega", SqlDbType.VarChar)
                cmd.Parameters.Add("@idordencompraenc", SqlDbType.Int)
                cmd.Parameters.Add("@no_documentooc", SqlDbType.VarChar)
                cmd.Parameters.Add("@no_documentorec", SqlDbType.VarChar)
                cmd.Parameters.Add("@referenciaoc", SqlDbType.VarChar)
                cmd.Parameters.Add("@fecha_recepcion", SqlDbType.DateTime)
                cmd.Parameters.Add("@tipotrans", SqlDbType.VarChar)
                cmd.Parameters.Add("@fecha_agrego", SqlDbType.DateTime)
                cmd.Parameters.Add("@codigo_producto", SqlDbType.VarChar)
                cmd.Parameters.Add("@codigo_barra_producto", SqlDbType.VarChar)
                cmd.Parameters.Add("@nombre_producto", SqlDbType.VarChar)
                cmd.Parameters.Add("@existencia", SqlDbType.Float)
                cmd.Parameters.Add("@nom_umbas", SqlDbType.VarChar)
                cmd.Parameters.Add("@nom_estado_producto", SqlDbType.VarChar)
                cmd.Parameters.Add("@nom_presentacion_producto", SqlDbType.VarChar)
                cmd.Parameters.Add("@ubicacion_origen", SqlDbType.VarChar)
                cmd.Parameters.Add("@no_poliza", SqlDbType.VarChar)
                cmd.Parameters.Add("@valor_aduana", SqlDbType.Float)
                cmd.Parameters.Add("@valor_fob", SqlDbType.Float)
                cmd.Parameters.Add("@valor_iva", SqlDbType.Float)
                cmd.Parameters.Add("@valor_dai", SqlDbType.Float)
                cmd.Parameters.Add("@valor_seguro", SqlDbType.Float)
                cmd.Parameters.Add("@valor_flete", SqlDbType.Float)
                cmd.Parameters.Add("@peso_neto", SqlDbType.Float)
                cmd.Parameters.Add("@numero_orden", SqlDbType.VarChar)
                cmd.Parameters.Add("@codigo_regimen", SqlDbType.VarChar)
                cmd.Parameters.Add("@nombre_regimen", SqlDbType.VarChar)
                cmd.Parameters.Add("@dias_vencimiento_regimen", SqlDbType.Int)
                cmd.Parameters.Add("@fecha_ingreso_ticket_tms", SqlDbType.DateTime)
                cmd.Parameters.Add("@es_retroactivo", SqlDbType.Bit)
                cmd.Parameters.Add("@factor", SqlDbType.Float)
                cmd.Parameters.Add("@camasportarima", SqlDbType.Float)
                cmd.Parameters.Add("@cajasporcama", SqlDbType.Float)
                cmd.Parameters.Add("@cantidad_ingreso_afecta_a_salida", SqlDbType.Float)
                cmd.Parameters.Add("@stock_jornada_hash", SqlDbType.VarChar)
                cmd.Parameters.Add("@idtickettms", SqlDbType.Int)
                cmd.Parameters.Add("@idpropietario", SqlDbType.Int)
                cmd.Parameters.Add("@idclasificacion", SqlDbType.Int)
                cmd.Parameters.Add("@clasificacion", SqlDbType.VarChar)
                cmd.Parameters.Add("@regimen", SqlDbType.VarChar)
                cmd.Parameters.Add("@posiciones", SqlDbType.Int)
                cmd.Parameters.Add("@costo_unitario", SqlDbType.Float)
                cmd.Parameters.Add("@no_documento_procesado_erp", SqlDbType.VarChar)
                cmd.Parameters.Add("@procesado_erp", SqlDbType.Bit)
                cmd.Parameters.Add("@fecha_procesado_stock_jornada", SqlDbType.DateTime)
                cmd.Parameters.Add("@IdProductoTallaColor", SqlDbType.Int)
                cmd.Parameters.Add("@Talla", SqlDbType.VarChar)
                cmd.Parameters.Add("@Color", SqlDbType.VarChar)

                For Each vItem As clsBeStock_jornada In pItems
                    cmd.Parameters("@idstockjornada").Value = vItem.IdStockJornada
                    cmd.Parameters("@idjornadasistema").Value = vItem.IdJornadaSistema
                    cmd.Parameters("@fecha").Value = vItem.Fecha
                    cmd.Parameters("@idbodega").Value = vItem.IdBodega
                    cmd.Parameters("@idstock").Value = vItem.IdStock
                    cmd.Parameters("@idpropietariobodega").Value = vItem.IdPropietarioBodega
                    cmd.Parameters("@idproductobodega").Value = vItem.IdProductoBodega
                    cmd.Parameters("@idproductoestado").Value = vItem.IdProductoEstado
                    cmd.Parameters("@idpresentacion").Value = If(vItem.IdPresentacion = 0, CType(DBNull.Value, Object), vItem.IdPresentacion)
                    cmd.Parameters("@idunidadmedida").Value = vItem.IdUnidadMedida
                    cmd.Parameters("@idubicacion").Value = vItem.IdUbicacion
                    cmd.Parameters("@idubicacion_anterior").Value = vItem.IdUbicacion_anterior
                    cmd.Parameters("@idrecepcionenc").Value = If(vItem.IdRecepcionEnc = 0, CType(DBNull.Value, Object), vItem.IdRecepcionEnc)
                    cmd.Parameters("@idrecepciondet").Value = If(vItem.IdRecepcionDet = 0, CType(DBNull.Value, Object), vItem.IdRecepcionDet)
                    cmd.Parameters("@idpedidoenc").Value = If(vItem.IdPedidoEnc = 0, CType(DBNull.Value, Object), vItem.IdPedidoEnc)
                    cmd.Parameters("@idpickingenc").Value = vItem.IdPickingEnc
                    cmd.Parameters("@iddespachoenc").Value = vItem.IdDespachoEnc
                    cmd.Parameters("@lote").Value = If(vItem.Lote, "")
                    cmd.Parameters("@lic_plate").Value = If(vItem.Lic_plate, "")
                    cmd.Parameters("@serial").Value = If(vItem.Serial, "")
                    cmd.Parameters("@cantidad").Value = vItem.Cantidad
                    cmd.Parameters("@fecha_ingreso").Value = vItem.Fecha_ingreso
                    cmd.Parameters("@fecha_vence").Value = vItem.Fecha_vence
                    cmd.Parameters("@uds_lic_plate").Value = vItem.Uds_lic_plate
                    cmd.Parameters("@no_bulto").Value = vItem.No_bulto
                    cmd.Parameters("@fecha_manufactura").Value = vItem.Fecha_manufactura
                    cmd.Parameters("@añada").Value = vItem.Añada
                    cmd.Parameters("@user_agr").Value = If(vItem.User_agr, "")
                    cmd.Parameters("@fec_agr").Value = vItem.Fec_agr
                    cmd.Parameters("@user_mod").Value = If(vItem.User_mod, "")
                    cmd.Parameters("@fec_mod").Value = vItem.Fec_mod
                    cmd.Parameters("@activo").Value = vItem.Activo
                    cmd.Parameters("@peso").Value = vItem.Peso
                    cmd.Parameters("@temperatura").Value = vItem.Temperatura
                    cmd.Parameters("@atributo_variante_1").Value = If(String.IsNullOrEmpty(vItem.Atributo_variante_1), CType(DBNull.Value, Object), vItem.Atributo_variante_1)
                    cmd.Parameters("@pallet_no_estandar").Value = vItem.Pallet_no_estandar
                    cmd.Parameters("@propietario").Value = If(vItem.Propietario, "")
                    cmd.Parameters("@proveedor").Value = If(vItem.Proveedor, "")
                    cmd.Parameters("@bodega").Value = If(vItem.Bodega, "")
                    cmd.Parameters("@idordencompraenc").Value = vItem.IdOrdenCompraEnc
                    cmd.Parameters("@no_documentooc").Value = If(vItem.No_DocumentoOC, "")
                    cmd.Parameters("@no_documentorec").Value = If(vItem.No_DocumentoRec, "")
                    cmd.Parameters("@referenciaoc").Value = If(vItem.ReferenciaOC, "")
                    cmd.Parameters("@fecha_recepcion").Value = vItem.Fecha_Recepcion
                    cmd.Parameters("@tipotrans").Value = If(vItem.TipoTrans, "")
                    cmd.Parameters("@fecha_agrego").Value = vItem.Fecha_Agrego
                    cmd.Parameters("@codigo_producto").Value = If(vItem.Codigo_producto, "")
                    cmd.Parameters("@codigo_barra_producto").Value = If(vItem.Codigo_barra_producto, "")
                    cmd.Parameters("@nombre_producto").Value = If(vItem.Nombre_producto, "")
                    cmd.Parameters("@existencia").Value = vItem.Existencia
                    cmd.Parameters("@nom_umbas").Value = If(vItem.Nom_umBas, "")
                    cmd.Parameters("@nom_estado_producto").Value = If(vItem.Nom_estado_producto, "")
                    cmd.Parameters("@nom_presentacion_producto").Value = If(vItem.Nom_presentacion_producto, "")
                    cmd.Parameters("@ubicacion_origen").Value = If(vItem.Ubicacion_origen, "")
                    cmd.Parameters("@no_poliza").Value = If(vItem.No_poliza, "")
                    cmd.Parameters("@valor_aduana").Value = vItem.Valor_aduana
                    cmd.Parameters("@valor_fob").Value = vItem.Valor_fob
                    cmd.Parameters("@valor_iva").Value = vItem.Valor_iva
                    cmd.Parameters("@valor_dai").Value = vItem.Valor_dai
                    cmd.Parameters("@valor_seguro").Value = vItem.Valor_seguro
                    cmd.Parameters("@valor_flete").Value = vItem.Valor_flete
                    cmd.Parameters("@peso_neto").Value = vItem.Peso_neto
                    cmd.Parameters("@numero_orden").Value = If(vItem.Numero_orden, "")
                    cmd.Parameters("@codigo_regimen").Value = If(vItem.Codigo_regimen, "")
                    cmd.Parameters("@nombre_regimen").Value = If(vItem.Nombre_regimen, "")
                    cmd.Parameters("@dias_vencimiento_regimen").Value = vItem.Dias_vencimiento_regimen
                    cmd.Parameters("@fecha_ingreso_ticket_tms").Value = vItem.Fecha_Ingreso_Ticket_TMS
                    cmd.Parameters("@es_retroactivo").Value = vItem.Es_Retroactivo
                    cmd.Parameters("@factor").Value = vItem.Factor
                    cmd.Parameters("@camasportarima").Value = vItem.CamasPorTarima
                    cmd.Parameters("@cajasporcama").Value = vItem.CajasPorCama
                    cmd.Parameters("@cantidad_ingreso_afecta_a_salida").Value = vItem.Cantidad_Ingreso_Afecta_A_salida
                    cmd.Parameters("@stock_jornada_hash").Value = If(vItem.Stock_Jornada_Hash, "")
                    cmd.Parameters("@idtickettms").Value = vItem.IdTicketTMS
                    cmd.Parameters("@idpropietario").Value = vItem.IdPropietario
                    cmd.Parameters("@idclasificacion").Value = vItem.IdClasificacion
                    cmd.Parameters("@clasificacion").Value = If(vItem.Clasificacion, "")
                    cmd.Parameters("@regimen").Value = If(vItem.Regimen, "")
                    cmd.Parameters("@posiciones").Value = vItem.Posiciones
                    cmd.Parameters("@costo_unitario").Value = vItem.Costo_Unitario
                    cmd.Parameters("@no_documento_procesado_erp").Value = If(vItem.No_Documento_Procesado_ERP, "")
                    cmd.Parameters("@procesado_erp").Value = vItem.Procesado_ERP
                    cmd.Parameters("@fecha_procesado_stock_jornada").Value = vItem.Fecha_Procesado_Stock_Jornada
                    cmd.Parameters("@IdProductoTallaColor").Value = If(vItem.IdProductoTallaColor > 0, CType(vItem.IdProductoTallaColor, Object), DBNull.Value)
                    cmd.Parameters("@Talla").Value = If(String.IsNullOrEmpty(vItem.Talla), CType(DBNull.Value, Object), vItem.Talla)
                    cmd.Parameters("@Color").Value = If(String.IsNullOrEmpty(vItem.Color), CType(DBNull.Value, Object), vItem.Color)

                    Insert_Multiple_Fast += cmd.ExecuteNonQuery()
                Next

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function Insert_Multiple(ByVal lStockJornada As List(Of clsBeStock_jornada),
    '									   ByVal lConnection As SqlConnection,
    '									   ByVal lTransaction As SqlTransaction,
    '									   ByRef prg As ProgressBar,
    '									   ByRef lblprg As RichTextBox) As Boolean

    '	Insert_Multiple = False

    '	Try

    '		Dim vIdMax = MaxID(lConnection, lTransaction) + 1
    '		Dim j As Integer = 0

    '		prg.Maximum = lStockJornada.Count

    '		For Each S In lStockJornada

    '			S.IdStockJornada = vIdMax

    '			Insertar(S, lConnection, lTransaction)

    '			vIdMax += 1

    '			lblprg.AppendText("Insertando stock_jornada " & S.IdStockJornada & " " & S.Fecha & " " & j & " de " & lStockJornada.Count)
    '			lblprg.AppendText(vbNewLine)
    '			lblprg.Refresh()
    '			lblprg.SelectionStart = lblprg.TextLength
    '			lblprg.ScrollToCaret()

    '			prg.Value = j
    '			j += 1

    '			Application.DoEvents()

    '		Next

    '		Insert_Multiple = True

    '	Catch ex As Exception
    '		Throw ex
    '	End Try

    'End Function

    'Public Shared Function Insert_Multiple(ByVal lStockJornada As List(Of clsBeStock_jornada),
    '									   ByVal pFechaJornada As Date,
    '									   ByVal lConnection As SqlConnection,
    '									   ByVal lTransaction As SqlTransaction,
    '									   ByRef prg As ProgressBar,
    '									   ByRef lblprg As RichTextBox) As Boolean

    '	Insert_Multiple = False

    '	Try

    '		Dim vIdMax = MaxID(lConnection, lTransaction) + 1
    '		Dim j As Integer = 0

    '		prg.Maximum = lStockJornada.Count

    '		For Each S In lStockJornada

    '			S.IdStockJornada = vIdMax
    '			S.Fecha = pFechaJornada

    '			Insertar(S, lConnection, lTransaction)

    '			vIdMax += 1

    '			lblprg.AppendText("Insertando stock_jornada " & S.IdStockJornada & " " & S.Fecha & " " & j & " de " & lStockJornada.Count)
    '			lblprg.AppendText(vbNewLine)
    '			lblprg.Refresh()
    '			lblprg.SelectionStart = lblprg.TextLength
    '			lblprg.ScrollToCaret()

    '			prg.Value = j
    '			j += 1

    '			Application.DoEvents()

    '		Next

    '		Insert_Multiple = True

    '	Catch ex As Exception
    '		Throw ex
    '	End Try

    'End Function

    Public Shared Function Get_Existencia_By_IdProducto_IdPropietario(ByVal pIdPropietarioBodega As Integer, ByVal pFecha As Date, ByVal pCodigo_producto As Integer) As Integer

        Try

            Try

                Dim lMax As Integer = 0

                Dim vSQL As String = "select existencia from stock_jornada where IdPropietarioBodega = @pIdPropietarioBodega and codigo_producto=@pCodigo_producto"

                vSQL += String.Format(" AND cast(Fecha AS DATE) = {0} ", FormatoFechas.fFecha(pFecha))

                Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                    lConnection.Open()

                    Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                        Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                            lCommand.Parameters.AddWithValue("@pIdPropietarioBodega", pIdPropietarioBodega)
                            lCommand.Parameters.AddWithValue("@pCodigo_producto", pCodigo_producto)


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
                Throw ex
            End Try

        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Public Shared Function Get_Movimientos_Inv_Inicial(ByVal pIdPropietarioBodega As Integer, ByVal pFecha As Date) As List(Of clsBeVW_MovimientosRetroactivo)


        'GT 12042021 clase de prueba clsBeVW_MovimientosRetroactivo es clon de clsBeVW_Movimientos
        Dim lReturnList As New List(Of clsBeVW_MovimientosRetroactivo)

        Try

            Dim vSQL As String = "SELECT * FROM VW_Movimientos_Inv_Inicial" &
            " Where  IdPropietarioBodega= @pIdPropietarioBodega "

            vSQL += String.Format(" AND cast(Fecha AS DATE) = {0} ", FormatoFechas.fFecha(pFecha))


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.CommandType = CommandType.Text
                    dad.SelectCommand.Parameters.AddWithValue("@pIdPropietarioBodega", pIdPropietarioBodega)

                    Dim lTable As New DataTable
                    dad.Fill(lTable)

                    Dim Obj As clsBeVW_MovimientosRetroactivo

                    If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lTable.Rows
                            Obj = New clsBeVW_MovimientosRetroactivo
                            clsLnVW_MovimientosRetroactivo.Cargar(Obj, lRow)
                            lReturnList.Add(Obj)
                        Next

                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Hash_Retroactivo_By_Fecha(ByVal pHash As String, ByVal pFecha As Date) As Boolean

        Existe_Hash_Retroactivo_By_Fecha = False

        Try

            Const sp As String = "SELECT * FROM Stock_jornada " &
                                 " Where(stock_jornada_hash = @stock_jornada_hash and fecha = @fecha and es_retroactivo =1)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", FormatoFechas.tFecha(pFecha))

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeStock_jornada As New clsBeStock_jornada

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Existe_Hash_Retroactivo_By_Fecha = True
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

    ''' <summary>
    ''' #EJC20210519: Determinar si ya se generó el retroactivo para un día específico. :)
    ''' </summary>
    ''' <param name="pHash"></param>
    ''' <param name="pFecha"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Existe_Hash_Retroactivo_By_Fecha(ByVal pHash As String,
                                                            ByVal pFecha As Date,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As Boolean

        Existe_Hash_Retroactivo_By_Fecha = False

        Try

            Const sp As String = " SELECT * FROM Stock_jornada 
								   WHERE(stock_jornada_hash = @stock_jornada_hash 
								   AND fecha = @fecha and es_retroactivo =1) "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@stock_jornada_hash", pHash)
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", FormatoFechas.tFecha(pFecha))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_Hash_Retroactivo_By_Fecha = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario_And_Rango_Fechas(ByVal pIdPropietario As Integer,
                                                                     ByVal FechaDesde As Date,
                                                                     ByVal FechaHasta As Date) As List(Of clsBeStock_jornada)

        Dim lReturnList As New List(Of clsBeStock_jornada)

        Try

            Const sp As String = "SELECT * FROM Stock_jornada WHERE IdPropietario = @IdPropietario AND Fecha BETWEN @Desde AND @Hasta"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", FechaDesde)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", FechaHasta)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeStock_jornada As New clsBeStock_jornada

                        For Each dr As DataRow In lDataTable.Rows
                            vBeStock_jornada = New clsBeStock_jornada()
                            Cargar(vBeStock_jornada, dr)
                            lReturnList.Add(vBeStock_jornada)
                        Next

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

    Public Shared Function Get_All_By_IdBodega_And_Rango_Fechas(ByVal pIdBodega As Integer,
                                                                ByVal FechaDesde As Date,
                                                                ByVal FechaHasta As Date) As DataTable

        Dim lReturn As New DataTable

        Try

            Const sp As String = "SELECT * FROM Stock_jornada 
                                  WHERE IdBodega = @IdBodega AND Fecha BETWEEN CONVERT(DATE, @Desde)  AND CONVERT(DATE, @Hasta)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Desde", FechaDesde)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Hasta", FechaDesde)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        lReturn = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturn

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20211102 Función modificada por la Interface de CEALSA
    Public Shared Function Get_All_By_IdPropietario_And_Rango_Fechas(ByVal pIdCliente As Integer,
                                                                     ByVal pNoOrden As String,
                                                                     ByVal FechaDesde As Date,
                                                                     ByVal FechaHasta As Date,
                                                                     ByVal pAlmacen As String,
                                                                     ByVal pRubro As clsDataContractDI.tRubroERP,
                                                                     ByVal pClasificacion As String,
                                                                     ByVal pProducto As String,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeStock_jornada)

        Dim lReturnList As New List(Of clsBeStock_jornada)
        Dim sp As String = ""

        Try

            Select Case pRubro

                Case clsDataContractDI.tRubroERP.CantidadUMBas

                    sp = "SELECT Fecha,
							   IdPropietario,
							   Codigo_Producto,
							   Numero_Orden,
							   No_DocumentoOC,
							   sum(existencia) AS Cantidad,
							   Nom_UMBas,
							   nombre_producto,
                               0 AS Bultos_Por_Tarima,
                               '' AS UMBultos
                           FROM Stock_jornada 
						   WHERE Fecha BETWEEN @Desde AND @Hasta
							  AND IdPropietario = @IdPropietario 
							  AND (numero_orden = @Numero_Orden OR No_DocumentoOC = @Numero_Orden)
							  AND Regimen =  @Regimen"

                    If (pProducto <> "") Then
                        sp += " AND codigo_producto = @Codigo_Producto "
                    End If

                    sp += " GROUP BY Fecha,
							IdPropietario,
							Numero_Orden,
							No_DocumentoOC,
							codigo_producto,
							nombre_producto,
							Nom_UMBas
                          Order By IdPropietario, codigo_producto, numero_orden, Fecha"

                Case clsDataContractDI.tRubroERP.Posicion

                    sp = "SELECT Fecha,
							   IdPropietario,
							   Codigo_Producto,
							   Numero_Orden,
							   No_DocumentoOC,
							   sum(posiciones) AS Cantidad,
							   Nom_UMBas,
							   nombre_producto,
                               0 AS Bultos_Por_Tarima,
                               '' AS UMBultos
						FROM Stock_jornada 
						WHERE Fecha BETWEEN @Desde AND @Hasta
							  AND IdPropietario = @IdPropietario 
							  AND (numero_orden = @Numero_Orden OR No_DocumentoOC = @Numero_Orden)
							  AND Regimen = @Regimen"

                    If (pProducto <> "") Then
                        sp += " AND codigo_producto = @Codigo_Producto "
                    End If

                    sp += " GROUP BY Fecha,
								 IdPropietario,
								 Numero_Orden,
								 No_DocumentoOC,
								 codigo_producto,
								 nombre_producto,
							     Nom_UMBas 
                          Order By IdPropietario, codigo_producto, numero_orden, Fecha"

                Case clsDataContractDI.tRubroERP.Presentacion

                    sp = "Select Fecha,
								 IdPropietario,
								 Codigo_Producto,
								 Numero_Orden,
								 No_DocumentoOC,
								 sum(existencia)/CASE WHEN factor>0 THEN factor ELSE 1 END AS Cantidad,
								 Nom_UMBas,
								 nombre_producto,
                                 0 AS Bultos_Por_Tarima,
                                 '' AS UMBultos
						FROM Stock_jornada 
						WHERE Fecha BETWEEN @Desde AND @Hasta
							  AND IdPropietario = @IdPropietario 
                              AND (numero_orden = @Numero_Orden OR No_DocumentoOC = @Numero_Orden) 
							  AND Regimen = @Regimen"

                    If (pProducto <> "") Then
                        sp += " And codigo_producto = @Codigo_Producto "
                    End If

                    sp += " GROUP BY Fecha,
								IdPropietario,
								Numero_Orden,
								No_DocumentoOC,
								codigo_producto,
								nombre_producto,
								Nom_UMBas,
								Factor
                          Order By IdPropietario, codigo_producto, numero_orden, Fecha"

                Case clsDataContractDI.tRubroERP.Tarimas

                    sp = "SELECT Fecha,
							   IdPropietario,
							   Codigo_Producto,
							   Numero_Orden,
							   No_DocumentoOC,
							   sum(CASE WHEN Posiciones>1 THEN POSICIONES ELSE 1 END) AS Cantidad,
							   Nom_UMBas,
							   nombre_producto,
                               SUM(cantidad) AS Bultos_Por_Tarima,
                               Nom_presentacion_producto AS UMBultos
						FROM Stock_jornada 
						WHERE Fecha BETWEEN @Desde AND @Hasta
									AND IdPropietario = @IdPropietario 
									AND (numero_orden = @Numero_Orden OR No_DocumentoOC = @Numero_Orden)
                                    AND Regimen = @Regimen"

                    If (pProducto <> "") Then
                        sp += " AND codigo_producto = @Codigo_Producto "
                    End If

                    sp += " GROUP BY Fecha,
								IdPropietario,
								Numero_Orden,
								No_DocumentoOC,
								codigo_producto,
								nombre_producto,
                                Nom_presentacion_producto,
								Nom_UMBas
                          Order By IdPropietario, codigo_producto, numero_orden, Fecha"

                Case clsDataContractDI.tRubroERP.CIF_DAI_IVA

                    sp = "SELECT Fecha,
							     IdPropietario,
							     Codigo_Producto,
							     Numero_Orden,
							     No_DocumentoOC,
							     SUM(Valor_aduana + Valor_dai + Valor_iva) AS Cantidad,
							     Nom_UMBas,
							     nombre_producto,
                                 0 AS Bultos_Por_Tarima,
                               '' AS UMBultos
						FROM Stock_jornada 
						WHERE Fecha BETWEEN @Desde AND @Hasta
									AND IdPropietario = @IdPropietario 
									AND (numero_orden = @Numero_Orden OR No_DocumentoOC = @Numero_Orden)
                                    AND Regimen = @Regimen"

                    If (pProducto <> "") Then
                        sp += " AND codigo_producto = @Codigo_Producto "
                    End If

                    sp += " GROUP BY Fecha,
								IdPropietario,
								Numero_Orden,
								No_DocumentoOC,
								codigo_producto,
								nombre_producto,
								Nom_UMBas
                          Order By IdPropietario, codigo_producto, numero_orden, Fecha"


                Case clsDataContractDI.tRubroERP.Clasificacion

                    sp = "SELECT Fecha,
							     IdPropietario,
							     CONVERT(NVARCHAR(100),IdClasificacion) as codigo_producto,
							     '' AS Numero_Orden,
							     '' AS No_DocumentoOC,
							     SUM(existencia) AS Cantidad,
							     '' AS Nom_UMBas,
							     Clasificacion,
                                 0 AS Bultos_Por_Tarima,
                                 '' AS UMBultos
						FROM Stock_jornada 
						WHERE Fecha BETWEEN @Desde AND @Hasta
									AND IdPropietario = @IdPropietario 
                                    AND Regimen = @Regimen"

                    If (pClasificacion <> "") Then
                        sp += " AND IdClasificacion = @Clasificacion "
                    End If

                    sp += " GROUP BY Fecha,
								IdPropietario,
								IdClasificacion,
								Clasificacion
						    Order By IdPropietario, IdClasificacion, Fecha"


            End Select

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdCliente)
                lDTA.SelectCommand.Parameters.AddWithValue("@Desde", FechaDesde)
                lDTA.SelectCommand.Parameters.AddWithValue("@Hasta", FechaHasta)
                If pRubro <> clsDataContractDI.tRubroERP.Clasificacion Then lDTA.SelectCommand.Parameters.AddWithValue("@Numero_Orden", pNoOrden)
                lDTA.SelectCommand.Parameters.AddWithValue("@Regimen", IIf(pAlmacen = 1, "General", "Fiscal"))
                If pClasificacion <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@Clasificacion", pClasificacion)
                If pProducto <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@Codigo_Producto", pProducto)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeStock_jornada As New clsBeStock_jornada

                For Each dr As DataRow In lDataTable.Rows
                    vBeStock_jornada = New clsBeStock_jornada()
                    Cargar(vBeStock_jornada, dr)
                    lReturnList.Add(vBeStock_jornada)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20210619 Agregué esta funcion con los parametros y la conexion
    Public Shared Function Get_All_By_Id_Propietario_And_No_Orden_And_Rango_Fechas(ByVal pIdCliente As String,
                                                                                   ByVal pNoOrden As String,
                                                                                   ByVal FechaDesde As Date,
                                                                                   ByVal FechaHasta As Date,
                                                                                   ByVal lConnection As SqlConnection,
                                                                                   ByVal lTransaction As SqlTransaction) As List(Of clsBeStock_jornada)

        Dim lReturnList As New List(Of clsBeStock_jornada)

        Try

            Const sp As String = "SELECT * FROM Stock_jornada 
								  WHERE Fecha BETWEEN @Desde AND @Hasta
                                  AND IdPropietario = @IdPropietario
								  AND (numero_orden = @Numero_Orden OR numero_orden = 0)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Desde", FechaDesde)
                lDTA.SelectCommand.Parameters.AddWithValue("@Hasta", FechaHasta)
                lDTA.SelectCommand.Parameters.AddWithValue("@Numero_Orden", pNoOrden)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdCliente)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeStock_jornada As New clsBeStock_jornada

                For Each dr As DataRow In lDataTable.Rows
                    vBeStock_jornada = New clsBeStock_jornada()
                    Cargar(vBeStock_jornada, dr)
                    lReturnList.Add(vBeStock_jornada)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_No_Orden_And_Rango_Fechas(ByVal pNoOrden As String,
                                                                ByVal FechaDesde As Date,
                                                                ByVal FechaHasta As Date,
                                                                ByVal lConnection As SqlConnection,
                                                                ByVal lTransaction As SqlTransaction) As List(Of clsBeStock_jornada)

        Dim lReturnList As New List(Of clsBeStock_jornada)

        Try

            Const sp As String = "SELECT * FROM Stock_jornada 
								  WHERE Fecha BETWEEN @Desde AND @Hasta										
								  AND (numero_orden = @Numero_Orden OR numero_orden = 0)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Desde", FechaDesde)
                lDTA.SelectCommand.Parameters.AddWithValue("@Hasta", FechaHasta)
                lDTA.SelectCommand.Parameters.AddWithValue("@Numero_Orden", pNoOrden)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeStock_jornada As New clsBeStock_jornada

                For Each dr As DataRow In lDataTable.Rows
                    vBeStock_jornada = New clsBeStock_jornada()
                    Cargar(vBeStock_jornada, dr)
                    lReturnList.Add(vBeStock_jornada)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_No_Orden_And_Rango_Fechas(ByVal pNoOrden As String,
                                                                ByVal FechaDesde As Date,
                                                                ByVal FechaHasta As Date) As List(Of clsBeStock_jornada)

        Dim lReturnList As New List(Of clsBeStock_jornada)

        Try

            Const sp As String = "SELECT * FROM Stock_jornada 
								  WHERE Fecha BETWEEN @Desde AND @Hasta										
								  AND (numero_orden = @Numero_Orden OR numero_orden = 0)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()


                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Desde", FechaDesde)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Hasta", FechaHasta)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Numero_Orden", pNoOrden)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeStock_jornada As New clsBeStock_jornada

                        For Each dr As DataRow In lDataTable.Rows
                            vBeStock_jornada = New clsBeStock_jornada()
                            Cargar(vBeStock_jornada, dr)
                            lReturnList.Add(vBeStock_jornada)
                        Next

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

    Public Shared Function Get_All_By_Rango_Fechas(ByVal FechaDesde As Date,
                                                   ByVal FechaHasta As Date,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As List(Of clsBeStock_jornada)

        Dim lReturnList As New List(Of clsBeStock_jornada)

        Try

            Const sp As String = "SELECT * FROM Stock_jornada 
								  WHERE Fecha BETWEEN @Desde AND @Hasta"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Desde", FechaDesde)
                lDTA.SelectCommand.Parameters.AddWithValue("@Hasta", FechaHasta)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeStock_jornada As New clsBeStock_jornada

                For Each dr As DataRow In lDataTable.Rows
                    vBeStock_jornada = New clsBeStock_jornada()
                    Cargar(vBeStock_jornada, dr)
                    lReturnList.Add(vBeStock_jornada)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPropietario_And_Rango_Fechas(ByVal pIdPropietario As Integer,
                                                                     ByVal pNoOrden As String,
                                                                     ByVal FechaDesde As Date,
                                                                     ByVal FechaHasta As Date) As List(Of clsBeStock_jornada)

        Dim lReturnList As New List(Of clsBeStock_jornada)

        Try

            Const sp As String = "SELECT * FROM Stock_jornada 
								  WHERE IdPropietario = @IdPropietario 
								  AND Fecha BETWEEN @Desde AND @Hasta
								  AND (numero_orden = @Numero_Orden OR numero_orden = 0)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Desde", FechaDesde)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Hasta", FechaHasta)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Numero_Orden", pNoOrden)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeStock_jornada As New clsBeStock_jornada

                        For Each dr As DataRow In lDataTable.Rows
                            vBeStock_jornada = New clsBeStock_jornada()
                            Cargar(vBeStock_jornada, dr)
                            lReturnList.Add(vBeStock_jornada)
                        Next

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

    Public Shared Function Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas(ByVal pIdCliente As Integer,
                                                                                      ByVal pNoOrden As String,
                                                                                      ByVal FechaDesde As Date,
                                                                                      ByVal FechaHasta As Date) As List(Of clsBeStock_jornada_logistico)
        Dim lStockJornadaCealsa As New List(Of clsBeStock_jornada_logistico)
        Dim vTipoDocIngreso As New clsDataContractDI.tTipoDocumentoIngreso

        Try

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim lStockJornada As List(Of clsBeStock_jornada) = Get_All_By_Id_Propietario_And_No_Orden_And_Rango_Fechas(pIdCliente,
                                                                                                                               pNoOrden,
                                                                                                                               FechaDesde,
                                                                                                                               FechaHasta,
                                                                                                                               lConnection,
                                                                                                                               lTransaction)

                    Dim BeStockCealsa As New clsBeStock_jornada_logistico

                    For Each S In lStockJornada

                        BeStockCealsa = New clsBeStock_jornada_logistico

                        '#EJC20210603: Task 53 cealsa - Simplificar en bultos por pallet..
                        'https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/53
                        'BeStockCealsa.BultosPorPallet = S.CajasPorCama * S.CamasPorTarima

                        BeStockCealsa.Cantidad = S.Cantidad

                        ''#EJC20210603: Task 51 cealsa, enviar conversión de cantidad UMBas y presentación..
                        ''https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/51
                        'If S.IdPresentacion = 0 Then
                        '	BeStockCealsa.CantidadUMBas = S.Cantidad
                        '	BeStockCealsa.CantidadPresentacion = 0
                        'Else
                        '	BeStockCealsa.CantidadUMBas = Math.Round(S.Cantidad * S.Factor, 6)
                        '	BeStockCealsa.CantidadPresentacion = S.Cantidad
                        'End If

                        BeStockCealsa.Codigo_Producto = S.Codigo_producto
                        'BeStockCealsa.Codigo_Regimen = S.Codigo_regimen

                        BeStockCealsa.Fecha = S.Fecha
                        BeStockCealsa.IdCliente = S.IdPropietario
                        BeStockCealsa.Nombre_Producto = S.Nombre_producto
                        'BeStockCealsa.NoPoliza = S.No_poliza

                        '#EJC20210603: Task 55 cealsa - Enviar el IdCliente (Propietario) cuando el ingreso es de consolidado..
                        'https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/55
                        vTipoDocIngreso = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(S.IdOrdenCompraEnc, lConnection, lTransaction)
                        If vTipoDocIngreso = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then
                            BeStockCealsa.IdConsolidador = clsLnTrans_oc_enc.Get_IdPropietario_By_IdOrdenCompraEnc(S.IdOrdenCompraEnc, lConnection, lTransaction)
                            'BeStockCealsa.Clasificacion = ""
                        Else
                            BeStockCealsa.IdConsolidador = 0
                        End If

                        ''#CKFK20210607: Enviar la Clasificación en el Propietario si es consolidado, en caso contrario se envía en la Clasificación
                        'If vTipoDocIngreso = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then
                        '	BeStockCealsa.Clasificacion = ""
                        'Else
                        '	BeStockCealsa.Clasificacion = S.Clasificacion

                        'End If

                        '#EJC20210603: Task 52 cealsa - Enviar el numero de documento de OC si no se tiene poliza con número de orden.
                        'https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/52
                        If S.Numero_orden.Trim = "" Or S.Numero_orden.Trim = "0" Then
                            BeStockCealsa.Numero_Orden = S.No_DocumentoOC
                        Else
                            BeStockCealsa.Numero_Orden = S.Numero_orden
                        End If

                        'BeStockCealsa.Posiciones = S.Posiciones
                        BeStockCealsa.UMBas = S.Nom_umBas
                        'BeStockCealsa.CIF_DAI_IVA = S.Valor_aduana + S.Valor_dai + S.Valor_iva

                        lStockJornadaCealsa.Add(BeStockCealsa)

                        'BeStockCealsa.Peso_Neto = S.Peso_neto
                        'BeStockCealsa.Propietario = ""
                        'BeStockCealsa.Valor_Aduana = S.Valor_aduana
                        'BeStockCealsa.Valor_DAI = S.Valor_dai
                        'BeStockCealsa.Valor_Flete = S.Valor_flete
                        'BeStockCealsa.Valor_FOB = S.Valor_fob
                        'BeStockCealsa.Valor_IVA = S.Valor_iva
                        'BeStockCealsa.Valor_Seguro = S.Valor_seguro
                        'BeStockCealsa.Presentacion = S.Nom_presentacion_producto
                        'BeStockCealsa.Cliente = S.Propietario
                        'BeStockCealsa.Regimen = S.Regimen
                        'BeStockCealsa.Ubicacion_Origen = S.Ubicacion_origen
                        'BeStockCealsa.Dias_Vencimiento_Regimen = S.Dias_vencimiento_regimen
                        'BeStockCealsa.Regimen = S.Regimen
                        'BeStockCealsa.Presentacion = S.Nom_presentacion_producto
                        'BeStockCealsa.Fecha_Ingreso = S.Fecha_ingreso
                        'BeStockCealsa.Nom_Estado_Producto = S.Nom_estado_producto
                        'BeStockCealsa.Bodega = S.Bodega

                    Next

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lStockJornadaCealsa

        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Shared Function Get_Almacenaje_Historico_By_No_Orden_And_Rango_Fechas(ByVal pNoOrden As String,
                                                                                 ByVal FechaDesde As Date,
                                                                                 ByVal FechaHasta As Date) As List(Of clsBeStock_jornada_logistico)
        Dim lStockJornadaCealsa As New List(Of clsBeStock_jornada_logistico)
        Dim vTipoDocIngreso As New clsDataContractDI.tTipoDocumentoIngreso

        Try

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim lStockJornada As List(Of clsBeStock_jornada) = Get_All_By_No_Orden_And_Rango_Fechas(pNoOrden,
                                                                                                            FechaDesde,
                                                                                                            FechaHasta,
                                                                                                            lConnection,
                                                                                                            lTransaction)

                    Dim BeStockCealsa As New clsBeStock_jornada_logistico

                    For Each S In lStockJornada

                        BeStockCealsa = New clsBeStock_jornada_logistico

                        ''#EJC20210603: Task 53 cealsa - Simplificar en bultos por pallet..
                        ''https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/53
                        'BeStockCealsa.BultosPorPallet = S.CajasPorCama * S.CamasPorTarima

                        BeStockCealsa.Cantidad = S.Cantidad

                        '#EJC20210603: Task 51 cealsa, enviar conversión de cantidad UMBas y presentación..
                        ''https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/51
                        'If S.IdPresentacion = 0 Then
                        '	BeStockCealsa.CantidadUMBas = S.Cantidad
                        '	BeStockCealsa.CantidadPresentacion = 0
                        'Else
                        '	BeStockCealsa.CantidadUMBas = Math.Round(S.Cantidad * S.Factor, 6)
                        '	BeStockCealsa.CantidadPresentacion = S.Cantidad
                        'End If

                        BeStockCealsa.Codigo_Producto = S.Codigo_producto
                        'BeStockCealsa.Codigo_Regimen = S.Codigo_regimen
                        BeStockCealsa.Fecha = S.Fecha
                        BeStockCealsa.IdCliente = S.IdPropietario
                        BeStockCealsa.Nombre_Producto = S.Nombre_producto
                        'BeStockCealsa.NoPoliza = S.No_poliza

                        '#EJC20210603: Task 55 cealsa - Enviar el IdCliente (Propietario) cuando el ingreso es de consolidado..
                        'https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/55
                        vTipoDocIngreso = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(S.IdOrdenCompraEnc, lConnection, lTransaction)
                        If vTipoDocIngreso = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then
                            BeStockCealsa.IdConsolidador = clsLnTrans_oc_enc.Get_IdPropietario_By_IdOrdenCompraEnc(S.IdOrdenCompraEnc, lConnection, lTransaction)
                            'BeStockCealsa.Clasificacion = ""
                        Else
                            BeStockCealsa.IdConsolidador = 0
                        End If

                        ''#CKFK20210607: Enviar la Clasificación en el Propietario si es consolidado, en caso contrario se envía en la Clasificación
                        'If vTipoDocIngreso = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then
                        '	BeStockCealsa.Clasificacion = ""
                        'Else
                        '	BeStockCealsa.Clasificacion = S.IdClasificacion
                        'End If

                        '#EJC20210603: Task 52 cealsa - Enviar el numero de documento de OC si no se tiene poliza con número de orden.
                        'https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/52
                        If S.Numero_orden.Trim = "" Or S.Numero_orden.Trim = "0" Then
                            BeStockCealsa.Numero_Orden = S.No_DocumentoOC
                        Else
                            BeStockCealsa.Numero_Orden = S.Numero_orden
                        End If

                        'BeStockCealsa.Posiciones = S.Posiciones
                        BeStockCealsa.UMBas = S.Nom_umBas
                        'BeStockCealsa.CIF_DAI_IVA = S.Valor_aduana + S.Valor_dai + S.Valor_iva

                        lStockJornadaCealsa.Add(BeStockCealsa)

                        'BeStockCealsa.Peso_Neto = S.Peso_neto
                        'BeStockCealsa.Propietario = ""
                        'BeStockCealsa.Valor_Aduana = S.Valor_aduana
                        'BeStockCealsa.Valor_DAI = S.Valor_dai
                        'BeStockCealsa.Valor_Flete = S.Valor_flete
                        'BeStockCealsa.Valor_FOB = S.Valor_fob
                        'BeStockCealsa.Valor_IVA = S.Valor_iva
                        'BeStockCealsa.Valor_Seguro = S.Valor_seguro
                        'BeStockCealsa.Presentacion = S.Nom_presentacion_producto
                        'BeStockCealsa.Cliente = S.Propietario
                        'BeStockCealsa.Regimen = S.Regimen
                        'BeStockCealsa.Ubicacion_Origen = S.Ubicacion_origen
                        'BeStockCealsa.Dias_Vencimiento_Regimen = S.Dias_vencimiento_regimen
                        'BeStockCealsa.Regimen = S.Regimen
                        'BeStockCealsa.Presentacion = S.Nom_presentacion_producto
                        'BeStockCealsa.Fecha_Ingreso = S.Fecha_ingreso
                        'BeStockCealsa.Nom_Estado_Producto = S.Nom_estado_producto
                        'BeStockCealsa.Bodega = S.Bodega

                    Next

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lStockJornadaCealsa

        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Shared Function Get_Almacenaje_Historico_By_Rango_Fechas(ByVal FechaDesde As Date,
                                                                    ByVal FechaHasta As Date) As List(Of clsBeStock_jornada_logistico)

        Dim lStockJornadaCealsa As New List(Of clsBeStock_jornada_logistico)
        Dim vTipoDocIngreso As New clsDataContractDI.tTipoDocumentoIngreso
        Dim tipoRubro As Integer = 1

        Try

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim lStockJornada As List(Of clsBeStock_jornada) = Get_All_By_Rango_Fechas(FechaDesde,
                                                                                               FechaHasta,
                                                                                               lConnection,
                                                                                               lTransaction)


                    '#EJC20230512: Esto es para el reporte de facturación de CEALSA, no borrar, no tocar, no quitar.
                    'Dim lStockJornada As List(Of clsBeStock_jornada) = Get_All_By_IdPropietario_And_Rango_Fechas(1,
                    '																							 "0",
                    '																							 FechaDesde,
                    '																							 FechaHasta,
                    '																							 1,
                    '																							 tipoRubro,
                    '																							 "",
                    '																							 "",
                    '																							 lConnection,
                    '																							 lTransaction) '

                    Dim BeStockCealsa As New clsBeStock_jornada_logistico

                    For Each S In lStockJornada

                        BeStockCealsa = New clsBeStock_jornada_logistico

                        ''#EJC20210603: Task 53 cealsa - Simplificar en bultos por pallet..
                        ''https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/53
                        'BeStockCealsa.BultosPorPallet = S.CajasPorCama * S.CamasPorTarima

                        BeStockCealsa.Cantidad = S.Cantidad

                        ''#EJC20210603: Task 51 cealsa, enviar conversión de cantidad UMBas y presentación..
                        ''https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/51
                        'If S.IdPresentacion = 0 Then
                        '	BeStockCealsa.CantidadUMBas = S.Cantidad
                        '	BeStockCealsa.CantidadPresentacion = 0
                        'Else
                        '	BeStockCealsa.CantidadUMBas = Math.Round(S.Cantidad * S.Factor, 6)
                        '	BeStockCealsa.CantidadPresentacion = S.Cantidad
                        'End If

                        BeStockCealsa.Codigo_Producto = S.Codigo_producto
                        'BeStockCealsa.Codigo_Regimen = S.Codigo_regimen
                        BeStockCealsa.Fecha = S.Fecha
                        BeStockCealsa.IdCliente = S.IdPropietario
                        BeStockCealsa.Nombre_Producto = S.Nombre_producto
                        'BeStockCealsa.NoPoliza = S.No_poliza
                        BeStockCealsa.Almacen = IIf(S.Regimen = "General", clsDataContractDI.tTipoAlmacen.General, clsDataContractDI.tTipoAlmacen.Fiscal)

                        '#EJC20210603: Task 55 cealsa - Enviar el IdCliente (Propietario) cuando el ingreso es de consolidado..
                        'https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/55
                        vTipoDocIngreso = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(S.IdOrdenCompraEnc, lConnection, lTransaction)
                        If vTipoDocIngreso = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then
                            BeStockCealsa.IdConsolidador = clsLnTrans_oc_enc.Get_IdPropietario_By_IdOrdenCompraEnc(S.IdOrdenCompraEnc, lConnection, lTransaction)
                        Else
                            BeStockCealsa.IdConsolidador = S.IdPropietario
                        End If

                        ''#CKFK20210607: Enviar la Clasificación en el Propietario si es consolidado, en caso contrario se envía en la Clasificación
                        'If vTipoDocIngreso = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then
                        '	BeStockCealsa.Clasificacion = ""
                        'Else
                        '	BeStockCealsa.Clasificacion = S.IdClasificacion
                        'End If

                        '#EJC20210603: Task 52 cealsa - Enviar el numero de documento de OC si no se tiene poliza con número de orden.
                        'https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/52
                        If S.Numero_orden.Trim = "" Or S.Numero_orden.Trim = "0" Then
                            BeStockCealsa.Numero_Orden = S.No_DocumentoOC
                        Else
                            BeStockCealsa.Numero_Orden = S.Numero_orden
                        End If

                        'BeStockCealsa.Posiciones = S.Posiciones
                        BeStockCealsa.UMBas = S.Nom_umBas
                        'BeStockCealsa.CIF_DAI_IVA = S.Valor_aduana + S.Valor_dai + S.Valor_iva

                        lStockJornadaCealsa.Add(BeStockCealsa)

                        BeStockCealsa.TipoRubro = S.TipoRubro

                        'BeStockCealsa.Peso_Neto = S.Peso_neto
                        'BeStockCealsa.Propietario = ""
                        'BeStockCealsa.Valor_Aduana = S.Valor_aduana
                        'BeStockCealsa.Valor_DAI = S.Valor_dai
                        'BeStockCealsa.Valor_Flete = S.Valor_flete
                        'BeStockCealsa.Valor_FOB = S.Valor_fob
                        'BeStockCealsa.Valor_IVA = S.Valor_iva
                        'BeStockCealsa.Valor_Seguro = S.Valor_seguro
                        'BeStockCealsa.Presentacion = S.Nom_presentacion_producto
                        'BeStockCealsa.Cliente = S.Propietario
                        'BeStockCealsa.Regimen = S.Regimen
                        'BeStockCealsa.Ubicacion_Origen = S.Ubicacion_origen
                        'BeStockCealsa.Dias_Vencimiento_Regimen = S.Dias_vencimiento_regimen
                        'BeStockCealsa.Regimen = S.Regimen
                        'BeStockCealsa.Presentacion = S.Nom_presentacion_producto
                        'BeStockCealsa.Fecha_Ingreso = S.Fecha_ingreso
                        'BeStockCealsa.Nom_Estado_Producto = S.Nom_estado_producto
                        'BeStockCealsa.Bodega = S.Bodega

                    Next

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lStockJornadaCealsa

        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    '#CKFK 20210619 Modifiqué esta función con los cambios solicitados por Claudia
    Public Shared Function Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas(ByVal pIdCliente As Integer,
                                                                                      ByVal pIdConsolidador As Integer,
                                                                                      ByVal pNoOrden As String,
                                                                                      ByVal FechaDesde As Date,
                                                                                      ByVal FechaHasta As Date,
                                                                                      ByVal Almacen As clsDataContractDI.tTipoAlmacen,
                                                                                      ByVal Clasificacion As String,
                                                                                      ByRef pTipoRubro As clsDataContractDI.tRubroERP,
                                                                                      ByVal pProducto As String) As List(Of clsBeStock_jornada_logistico)
        Dim lStockJornadaCealsa As New List(Of clsBeStock_jornada_logistico)
        Dim vTipoDocIngreso As New clsDataContractDI.tTipoDocumentoIngreso

        Try

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim lStockJornada As List(Of clsBeStock_jornada) = Get_All_By_IdPropietario_And_Rango_Fechas(pIdCliente,
                                                                                                                 pNoOrden,
                                                                                                                 FechaDesde,
                                                                                                                 FechaHasta,
                                                                                                                 Almacen,
                                                                                                                 pTipoRubro,
                                                                                                                 Clasificacion,
                                                                                                                 pProducto,
                                                                                                                 lConnection,
                                                                                                                 lTransaction)

                    Dim BeStockCealsa As New clsBeStock_jornada_logistico

                    For Each S In lStockJornada

                        BeStockCealsa = New clsBeStock_jornada_logistico

                        '#EJC20210603: Task 55 cealsa - Enviar el IdCliente (Propietario) cuando el ingreso es de consolidado..
                        'https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/55
                        vTipoDocIngreso = clsLnTrans_oc_enc.Get_IdTipoDocumento_By_IdOrdenCompraEnc(S.IdOrdenCompraEnc, lConnection, lTransaction)
                        If vTipoDocIngreso = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Consolidado Then
                            BeStockCealsa.IdConsolidador = clsLnTrans_oc_enc.Get_IdPropietario_By_IdOrdenCompraEnc(S.IdOrdenCompraEnc, lConnection, lTransaction)
                        Else
                            BeStockCealsa.IdConsolidador = 0
                        End If

                        BeStockCealsa.Cantidad = S.Cantidad

                        BeStockCealsa.Codigo_Producto = S.Codigo_producto
                        BeStockCealsa.Fecha = S.Fecha
                        BeStockCealsa.IdCliente = S.IdPropietario
                        BeStockCealsa.Nombre_Producto = S.Nombre_producto
                        BeStockCealsa.Almacen = IIf(S.Regimen = "General", clsDataContractDI.tTipoAlmacen.General, clsDataContractDI.tTipoAlmacen.Fiscal)

                        '#EJC20210603: Task 52 cealsa - Enviar el numero de documento de OC si no se tiene poliza con número de orden.
                        'https://dev.azure.com/ejcalderon0892/CEALSA/_workitems/edit/52
                        If S.Numero_orden.Trim = "" Or S.Numero_orden.Trim = "0" Then
                            BeStockCealsa.Numero_Orden = S.No_DocumentoOC
                        Else
                            BeStockCealsa.Numero_Orden = S.Numero_orden
                        End If

                        BeStockCealsa.UMBas = S.Nom_umBas

                        BeStockCealsa.Bultos_Por_Tarima = S.Bultos_Por_Tarima
                        BeStockCealsa.UMBultos = S.Nom_presentacion_producto

                        lStockJornadaCealsa.Add(BeStockCealsa)

                    Next

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lStockJornadaCealsa

        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_IdJornada_And_IdTicket(ByVal pIdJornadaSistema As Integer,
                                                             ByVal pIdTicketTMS As Integer,
                                                             ByVal pIdBodega As Integer) As DataTable

        Dim lReturn As New DataTable

        Try

            Const sp As String = "SELECT * FROM Stock_jornada 
                                  WHERE IdBodega = @IdBodega 
								  AND IdJornadaSistema = @IdJornadaSistema AND IdTicketTMS = @IdTicketTMS "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdTicketTMS)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdJornadaSistema", pIdJornadaSistema)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTicketTMS", pIdTicketTMS)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        lReturn = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturn

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdJornada_And_IdTicket(ByVal pIdJornadaSistema As Integer,
                                                             ByVal pIdTicketTMS As Integer,
                                                             ByVal pIdBodega As Integer,
                                                             ByVal lConnection As SqlConnection,
                                                             ByVal lTransaction As SqlTransaction) As DataTable

        Dim lReturn As New DataTable

        Try

            lReturn.Clear()

            Const sp As String = "SELECT * FROM Stock_jornada 
                                  WHERE IdBodega = @IdBodega 
								  AND IdJornadaSistema = @IdJornadaSistema AND IdTicketTMS = @IdTicketTMS "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdJornadaSistema", pIdJornadaSistema)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTicketTMS", pIdTicketTMS)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then
                    lReturn = lDataTable
                End If

            End Using

            Return lReturn

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_IdStock_Retroactivo_By_Fecha(ByVal pIdStock As String,
                                                               ByVal pFecha As Date,
                                                               ByVal pIdBodega As Integer) As Boolean

        Existe_IdStock_Retroactivo_By_Fecha = False

        Try

            Const sp As String = " SELECT * FROM Stock_jornada " &
                                 " Where(IdStock = @IdStock and fecha = @fecha and es_retroactivo =1 and IdBodega = @IdBodega)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", pIdStock)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", FormatoFechas.tFecha(pFecha))
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeStock_jornada As New clsBeStock_jornada

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Existe_IdStock_Retroactivo_By_Fecha = True
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

    Public Shared Function Existe_IdStock_Retroactivo_By_Fecha(ByVal pIdStock As String,
                                                               ByVal pFecha As Date,
                                                               ByVal pIdBodega As Integer,
                                                               ByVal lConnection As SqlConnection,
                                                               ByVal lTransaction As SqlTransaction) As Boolean

        Existe_IdStock_Retroactivo_By_Fecha = False

        Try

            Const sp As String = " SELECT * FROM Stock_jornada " &
                                 " Where(IdStock = @IdStock and fecha = @fecha and es_retroactivo =1 and IdBodega = @IdBodega)"


            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", pIdStock)
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", FormatoFechas.tFecha(pFecha))
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeStock_jornada As New clsBeStock_jornada

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_IdStock_Retroactivo_By_Fecha = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdJornada_And_IdTicket(ByVal pIdJornadaSistema As Integer,
                                                             ByVal pIdTicketTMS As Integer,
                                                             ByVal pIdBodega As Integer,
                                                             ByVal pLicPlate As String,
                                                             ByVal pIdStock As Integer,
                                                             ByVal pFecha As Date,
                                                             ByVal lConnection As SqlConnection,
                                                             ByVal lTransaction As SqlTransaction) As DataTable

        Dim lReturn As New DataTable

        Try

            lReturn.Clear()

            Const sp As String = "SELECT * FROM Stock_jornada 
                                  WHERE IdBodega = @IdBodega 
								  AND IdJornadaSistema = @IdJornadaSistema 
								  AND IdTicketTMS = @IdTicketTMS 
								  AND Lic_Plate = @pLicPlate 
								  AND IdStock = @pIdStock 	
								  AND Fecha = @pFecha "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdJornadaSistema", pIdJornadaSistema)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTicketTMS", pIdTicketTMS)
                lDTA.SelectCommand.Parameters.AddWithValue("@pLicPlate", pLicPlate)
                lDTA.SelectCommand.Parameters.AddWithValue("@pIdStock", pIdStock)
                lDTA.SelectCommand.Parameters.AddWithValue("@pFecha", pFecha)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then
                    lReturn = lDataTable
                End If

            End Using

            Return lReturn

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdStock(ByVal lic_plate As String,
                                                 ByVal Fecha As Date,
                                                 ByVal IdPropietarioBodega As Integer,
                                                 ByVal IdProductoBodega As Integer,
                                                 ByVal IdStock As Integer,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction) As clsBeStock_jornada


        Get_Single_By_IdStock = Nothing

        Try

            Dim vSQL As String = "Select * from stock_jornada 
							      WHERE lic_plate=@lic_plate 
								  AND fecha= @Fecha and IdPropietarioBodega = @IdPropietarioBodega 
								  AND IdProductoBodega=@IdProductoBodega
								  AND IdStock = @IdStock "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", lic_plate)
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", Fecha.Date)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim Obj As New clsBeStock_jornada
                    Cargar(Obj, lDataTable.Rows(0))
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT29082024: traer solo producto unico del historico, según OrdenCompra, para no incluir duplicados en la consulta original de historico.
    Public Shared Function Get_Productos_By_IdOrdenCompra_And_Rango_Fechas(ByVal pIdOrdenCompraEnc As Integer,
                                                                           ByVal FechaDesde As Date,
                                                                           ByVal pTimeOut As Integer) As DataTable

        Get_Productos_By_IdOrdenCompra_And_Rango_Fechas = Nothing

        Try

            'Const sp As String = "SELECT Distinct codigo_producto FROM Stock_jornada 
            '					  WHERE Fecha BETWEEN @Desde AND @Hasta										
            '					  AND IdOrdenCompraEnc = @pIdOrdenCompraEnc "

            Const sp As String = "SELECT Distinct codigo_producto FROM Stock_jornada 
								  WHERE Fecha = @Desde AND IdOrdenCompraEnc = @pIdOrdenCompraEnc "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()


                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@Desde", FechaDesde)
                        'lDTA.SelectCommand.Parameters.AddWithValue("@Hasta", FechaHasta)
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)

                        '#GT05062023: se agrega el timer para que el reporte no se cuelge por los filtros
                        If pTimeOut > 0 Then
                            lDTA.SelectCommand.CommandTimeout = pTimeOut
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Productos_By_IdOrdenCompra_And_Rango_Fechas = lDataTable
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

    '#GT23102025: retornar licencias unicas que representan una posición por cada una de ellas.
    Public Shared Function Get_Posiciones_By_IdOrdenCompra_And_Fecha_And_IdProducto(ByVal pIdOrdenCompraEnc As Integer,
                                                                                    ByVal FechaDesde As Date,
                                                                                    ByVal pCodigoProducto As String,
                                                                                    ByVal pTimeOut As Integer) As Integer

        Get_Posiciones_By_IdOrdenCompra_And_Fecha_And_IdProducto = 0

        Try

            Const sp As String = "SELECT COUNT(lic_plate) FROM stock_jornada
                                  WHERE Fecha = @Desde AND IdOrdenCompraEnc = @pIdOrdenCompraEnc AND codigo_producto = @pCodigoProducto"

            Dim resultado As Integer = 0

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction)
                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@Desde", FechaDesde)
                        lCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)
                        lCommand.Parameters.AddWithValue("@pCodigoProducto", pCodigoProducto)

                        ' Ajustar timeout si aplica
                        If pTimeOut > 0 Then
                            lCommand.CommandTimeout = pTimeOut
                        End If

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            resultado = Convert.ToInt32(lReturnValue)
                        End If
                    End Using

                    lTransaction.Commit()
                End Using
            End Using

            Get_Posiciones_By_IdOrdenCompra_And_Fecha_And_IdProducto = resultado


        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class
