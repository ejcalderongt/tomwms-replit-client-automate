Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_pe_det

    Public Shared Sub Cargar(ByRef oBeTrans_pe_det As clsBeTrans_pe_det, ByRef dr As DataRow)

        Try

            With oBeTrans_pe_det

                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .ProductoBodega.IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                clsLnProducto_bodega.Obtener(.ProductoBodega)
                .IdEstado = IIf(IsDBNull(dr.Item("IdEstado")), 0, dr.Item("IdEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedidaBasica = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0.0, dr.Item("Cantidad"))
                .Peso = IIf(IsDBNull(dr.Item("Peso")), 0.0, dr.Item("Peso"))
                .Precio = IIf(IsDBNull(dr.Item("Precio")), 0.0, dr.Item("Precio"))
                .No_recepcion = IIf(IsDBNull(dr.Item("no_recepcion")), 0, dr.Item("no_recepcion"))
                .Ndias = IIf(IsDBNull(dr.Item("ndias")), 0, dr.Item("ndias"))
                .Cant_despachada = IIf(IsDBNull(dr.Item("cant_despachada")), 0.0, dr.Item("cant_despachada"))
                .Peso_despachado = IIf(IsDBNull(dr.Item("peso_despachado")), 0.0, dr.Item("peso_despachado"))
                .Codigo_Producto = IIf(IsDBNull(dr.Item("codigo_producto")), 0.0, dr.Item("codigo_producto"))
                .Nombre_producto = IIf(IsDBNull(dr.Item("nombre_producto")), "", dr.Item("nombre_producto"))
                .Nom_presentacion = IIf(IsDBNull(dr.Item("nom_presentacion")), "", dr.Item("nom_presentacion"))
                .Nom_unid_med = IIf(IsDBNull(dr.Item("nom_unid_med")), "", dr.Item("nom_unid_med"))
                .Nom_estado = IIf(IsDBNull(dr.Item("nom_estado")), "", dr.Item("nom_estado"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .Fecha_especifica = IIf(IsDBNull(dr.Item("fecha_especifica")), False, dr.Item("fecha_especifica"))
                .RoadDes = IIf(IsDBNull(dr.Item("RoadDes")), 0.0, dr.Item("RoadDes"))
                .RoadDesMon = IIf(IsDBNull(dr.Item("RoadDesMon")), 0.0, dr.Item("RoadDesMon"))
                .RoadTotal = IIf(IsDBNull(dr.Item("RoadTotal")), 0.0, dr.Item("RoadTotal"))
                .RoadPrecioDoc = IIf(IsDBNull(dr.Item("RoadPrecioDoc")), 0.0, dr.Item("RoadPrecioDoc"))
                .RoadVAL1 = IIf(IsDBNull(dr.Item("RoadVAL1")), 0.0, dr.Item("RoadVAL1"))
                .RoadVAL2 = IIf(IsDBNull(dr.Item("RoadVAL2")), "", dr.Item("RoadVAL2"))
                .RoadCantProc = IIf(IsDBNull(dr.Item("RoadCantProc")), 0.0, dr.Item("RoadCantProc"))
                .Atributo_Variante_1 = IIf(IsDBNull(dr.Item("Atributo_Variante_1")), "", dr.Item("Atributo_Variante_1"))
                .IdStockEspecifico = IIf(IsDBNull(dr.Item("IdStockEspecifico")), 0, dr.Item("IdStockEspecifico"))

                '#EJC20210708: Cealsa fiscal campos.
                .Peso_Bruto = IIf(IsDBNull(dr.Item("Peso_Bruto")), 0, dr.Item("Peso_Bruto"))
                .Peso_Neto = IIf(IsDBNull(dr.Item("Peso_Neto")), 0, dr.Item("Peso_Neto"))
                .Costo = IIf(IsDBNull(dr.Item("Costo")), 0, dr.Item("Costo"))
                .valor_aduana = IIf(IsDBNull(dr.Item("valor_aduana")), 0, dr.Item("valor_aduana"))
                .valor_fob = IIf(IsDBNull(dr.Item("valor_fob")), 0, dr.Item("valor_fob"))
                .valor_iva = IIf(IsDBNull(dr.Item("valor_iva")), 0, dr.Item("valor_iva"))
                .valor_dai = IIf(IsDBNull(dr.Item("valor_dai")), 0, dr.Item("valor_dai"))
                .valor_seguro = IIf(IsDBNull(dr.Item("valor_seguro")), 0, dr.Item("valor_seguro"))
                .valor_flete = IIf(IsDBNull(dr.Item("valor_flete")), 0, dr.Item("valor_flete"))
                .Total_linea = IIf(IsDBNull(dr.Item("Total_linea")), 0, dr.Item("Total_linea"))
                .No_linea = IIf(IsDBNull(dr.Item("No_linea")), 0, dr.Item("No_linea"))

                '#EJC20220307: IdCliente detalle para BYB.
                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .Talla = IIf(IsDBNull(dr.Item("Talla")), "", dr.Item("Talla"))
                .Color = IIf(IsDBNull(dr.Item("Color")), "", dr.Item("Color"))
                .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub Cargar(ByRef oBeTrans_pe_det As clsBeTrans_pe_det,
                             ByRef dr As DataRow,
                             ByRef lConnection As SqlConnection,
                             ByRef lTransaction As SqlTransaction)

        Try

            With oBeTrans_pe_det

                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .ProductoBodega.IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                clsLnProducto_bodega.Obtener(.ProductoBodega, lConnection, lTransaction)
                .IdEstado = IIf(IsDBNull(dr.Item("IdEstado")), 0, dr.Item("IdEstado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedidaBasica = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0.0, dr.Item("Cantidad"))
                .Peso = IIf(IsDBNull(dr.Item("Peso")), 0.0, dr.Item("Peso"))
                .Precio = IIf(IsDBNull(dr.Item("Precio")), 0.0, dr.Item("Precio"))
                .No_recepcion = IIf(IsDBNull(dr.Item("no_recepcion")), 0, dr.Item("no_recepcion"))
                .Ndias = IIf(IsDBNull(dr.Item("ndias")), 0, dr.Item("ndias"))
                .Cant_despachada = IIf(IsDBNull(dr.Item("cant_despachada")), 0.0, dr.Item("cant_despachada"))
                .Peso_despachado = IIf(IsDBNull(dr.Item("peso_despachado")), 0.0, dr.Item("peso_despachado"))
                .Codigo_Producto = IIf(IsDBNull(dr.Item("codigo_producto")), 0.0, dr.Item("codigo_producto"))
                .Nombre_producto = IIf(IsDBNull(dr.Item("nombre_producto")), "", dr.Item("nombre_producto"))
                .Nom_presentacion = IIf(IsDBNull(dr.Item("nom_presentacion")), "", dr.Item("nom_presentacion"))
                .Nom_unid_med = IIf(IsDBNull(dr.Item("nom_unid_med")), "", dr.Item("nom_unid_med"))
                .Nom_estado = IIf(IsDBNull(dr.Item("nom_estado")), "", dr.Item("nom_estado"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .Fecha_especifica = IIf(IsDBNull(dr.Item("fecha_especifica")), False, dr.Item("fecha_especifica"))
                .RoadDes = IIf(IsDBNull(dr.Item("RoadDes")), 0.0, dr.Item("RoadDes"))
                .RoadDesMon = IIf(IsDBNull(dr.Item("RoadDesMon")), 0.0, dr.Item("RoadDesMon"))
                .RoadTotal = IIf(IsDBNull(dr.Item("RoadTotal")), 0.0, dr.Item("RoadTotal"))
                .RoadPrecioDoc = IIf(IsDBNull(dr.Item("RoadPrecioDoc")), 0.0, dr.Item("RoadPrecioDoc"))
                .RoadVAL1 = IIf(IsDBNull(dr.Item("RoadVAL1")), 0.0, dr.Item("RoadVAL1"))
                .RoadVAL2 = IIf(IsDBNull(dr.Item("RoadVAL2")), "", dr.Item("RoadVAL2"))
                .RoadCantProc = IIf(IsDBNull(dr.Item("RoadCantProc")), 0.0, dr.Item("RoadCantProc"))
                .Atributo_Variante_1 = IIf(IsDBNull(dr.Item("Atributo_Variante_1")), "", dr.Item("Atributo_Variante_1"))
                .IdStockEspecifico = IIf(IsDBNull(dr.Item("IdStockEspecifico")), 0, dr.Item("IdStockEspecifico"))

                '#EJC20210708: Cealsa fiscal campos.
                .Peso_Bruto = IIf(IsDBNull(dr.Item("Peso_Bruto")), 0, dr.Item("Peso_Bruto"))
                .Peso_Neto = IIf(IsDBNull(dr.Item("Peso_Neto")), 0, dr.Item("Peso_Neto"))
                .Costo = IIf(IsDBNull(dr.Item("Costo")), 0, dr.Item("Costo"))
                .valor_aduana = IIf(IsDBNull(dr.Item("valor_aduana")), 0, dr.Item("valor_aduana"))
                .valor_fob = IIf(IsDBNull(dr.Item("valor_fob")), 0, dr.Item("valor_fob"))
                .valor_iva = IIf(IsDBNull(dr.Item("valor_iva")), 0, dr.Item("valor_iva"))
                .valor_dai = IIf(IsDBNull(dr.Item("valor_dai")), 0, dr.Item("valor_dai"))
                .valor_seguro = IIf(IsDBNull(dr.Item("valor_seguro")), 0, dr.Item("valor_seguro"))
                .valor_flete = IIf(IsDBNull(dr.Item("valor_flete")), 0, dr.Item("valor_flete"))
                .Total_linea = IIf(IsDBNull(dr.Item("Total_linea")), 0, dr.Item("Total_linea"))

                '#EJC20220307: IdCliente detalle para BYB.
                .IdCliente = IIf(IsDBNull(dr.Item("IdCliente")), 0, dr.Item("IdCliente"))
                .No_linea = IIf(IsDBNull(dr.Item("No_linea")), 0, dr.Item("No_linea"))
                .Talla = IIf(IsDBNull(dr.Item("Talla")), "", dr.Item("Talla"))
                .Color = IIf(IsDBNull(dr.Item("Color")), "", dr.Item("Color"))
                .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeTrans_pe_det As clsBeTrans_pe_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("trans_pe_det")
            Ins.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Ins.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Ins.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Ins.Add("idestado", "@idestado", DataType.Parametro)
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Ins.Add("cantidad", "@cantidad", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("precio", "@precio", DataType.Parametro)
            Ins.Add("no_recepcion", "@no_recepcion", DataType.Parametro)
            Ins.Add("ndias", "@ndias", DataType.Parametro)
            Ins.Add("cant_despachada", "@cant_despachada", DataType.Parametro)
            Ins.Add("peso_despachado", "@peso_despachado", DataType.Parametro)
            Ins.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Ins.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Ins.Add("nom_presentacion", "@nom_presentacion", DataType.Parametro)
            Ins.Add("nom_unid_med", "@nom_unid_med", DataType.Parametro)
            Ins.Add("nom_estado", "@nom_estado", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("fecha_especifica", "@fecha_especifica", DataType.Parametro)
            Ins.Add("roaddes", "@roaddes", DataType.Parametro)
            Ins.Add("roaddesmon", "@roaddesmon", DataType.Parametro)
            Ins.Add("roadtotal", "@roadtotal", DataType.Parametro)
            Ins.Add("roadpreciodoc", "@roadpreciodoc", DataType.Parametro)
            Ins.Add("roadval1", "@roadval1", DataType.Parametro)
            Ins.Add("roadval2", "@roadval2", DataType.Parametro)
            Ins.Add("roadcantproc", "@roadcantproc", DataType.Parametro)
            Ins.Add("no_linea", "@no_linea", DataType.Parametro)
            If Not oBeTrans_pe_det.Atributo_Variante_1 Is Nothing Then Ins.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            Ins.Add("idstockespecifico", "@idstockespecifico", DataType.Parametro)
            Ins.Add("EsPadre", "@EsPadre", DataType.Parametro)
            Ins.Add("IdPedidoDetPadre", "@IdPedidoDetPadre", DataType.Parametro)
            '#EJC20210708:Cealsa fiscal Ins.
            Ins.Add("Peso_Bruto", "@Peso_Bruto", DataType.Parametro)
            Ins.Add("Peso_Neto", "@Peso_Neto", DataType.Parametro)
            Ins.Add("Costo", "@Costo", DataType.Parametro)
            Ins.Add("valor_aduana", "@valor_aduana", DataType.Parametro)
            Ins.Add("valor_fob", "@valor_fob", DataType.Parametro)
            Ins.Add("valor_iva", "@valor_iva", DataType.Parametro)
            Ins.Add("valor_dai", "@valor_dai", DataType.Parametro)
            Ins.Add("valor_seguro", "@valor_seguro", DataType.Parametro)
            Ins.Add("valor_flete", "@valor_flete", DataType.Parametro)
            Ins.Add("Total_linea", "@Total_linea", DataType.Parametro)
            Ins.Add("IdCliente", "@IdCliente", DataType.Parametro)
            Ins.Add("Talla", "@Talla", DataType.Parametro)
            Ins.Add("Color", "@Color", DataType.Parametro)
            Ins.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
                cmd.CommandTimeout = 60
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_pe_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_det.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_pe_det.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeTrans_pe_det.IdEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_pe_det.IdPresentacion = 0, DBNull.Value, oBeTrans_pe_det.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeTrans_pe_det.IdUnidadMedidaBasica))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_pe_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_pe_det.Peso))
            cmd.Parameters.Add(New SqlParameter("@PRECIO", oBeTrans_pe_det.Precio))
            cmd.Parameters.Add(New SqlParameter("@NO_RECEPCION", oBeTrans_pe_det.No_recepcion))
            cmd.Parameters.Add(New SqlParameter("@NDIAS", oBeTrans_pe_det.Ndias))
            cmd.Parameters.Add(New SqlParameter("@CANT_DESPACHADA", oBeTrans_pe_det.Cant_despachada))
            cmd.Parameters.Add(New SqlParameter("@PESO_DESPACHADO", oBeTrans_pe_det.Peso_despachado))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_pe_det.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", clsPublic.Quitar_Caracteres_No_Permitidos(oBeTrans_pe_det.Nombre_producto)))
            cmd.Parameters.Add(New SqlParameter("@NOM_PRESENTACION", oBeTrans_pe_det.Nom_presentacion))
            cmd.Parameters.Add(New SqlParameter("@NOM_UNID_MED", oBeTrans_pe_det.Nom_unid_med))
            cmd.Parameters.Add(New SqlParameter("@NOM_ESTADO", oBeTrans_pe_det.Nom_estado))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_pe_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_pe_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ESPECIFICA", oBeTrans_pe_det.Fecha_especifica))
            cmd.Parameters.Add(New SqlParameter("@ROADDES", oBeTrans_pe_det.RoadDes))
            cmd.Parameters.Add(New SqlParameter("@ROADDESMON", oBeTrans_pe_det.RoadDesMon))
            cmd.Parameters.Add(New SqlParameter("@ROADTOTAL", oBeTrans_pe_det.RoadTotal))
            cmd.Parameters.Add(New SqlParameter("@ROADPRECIODOC", oBeTrans_pe_det.RoadPrecioDoc))
            cmd.Parameters.Add(New SqlParameter("@ROADVAL1", oBeTrans_pe_det.RoadVAL1))
            cmd.Parameters.Add(New SqlParameter("@ROADVAL2", oBeTrans_pe_det.RoadVAL2))
            cmd.Parameters.Add(New SqlParameter("@ROADCANTPROC", oBeTrans_pe_det.RoadCantProc))
            If Not oBeTrans_pe_det.Atributo_Variante_1 Is Nothing Then cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeTrans_pe_det.Atributo_Variante_1))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_pe_det.No_linea))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKESPECIFICO", oBeTrans_pe_det.IdStockEspecifico))
            cmd.Parameters.Add(New SqlParameter("@ESPADRE", oBeTrans_pe_det.EsPadre))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODETPADRE", oBeTrans_pe_det.IdPedidoDetPadre))

            '#EJC20210708:Cealsa fiscal Ins params.
            cmd.Parameters.Add(New SqlParameter("@PESO_BRUTO", oBeTrans_pe_det.Peso_Bruto))
            cmd.Parameters.Add(New SqlParameter("@PESO_NETO", oBeTrans_pe_det.Peso_Neto))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeTrans_pe_det.Costo))
            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeTrans_pe_det.valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FOB", oBeTrans_pe_det.valor_fob))
            cmd.Parameters.Add(New SqlParameter("@VALOR_IVA", oBeTrans_pe_det.valor_iva))
            cmd.Parameters.Add(New SqlParameter("@VALOR_DAI", oBeTrans_pe_det.valor_dai))
            cmd.Parameters.Add(New SqlParameter("@VALOR_SEGURO", oBeTrans_pe_det.valor_seguro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FLETE", oBeTrans_pe_det.valor_flete))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LINEA", oBeTrans_pe_det.Total_linea))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeTrans_pe_det.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@TALLA", oBeTrans_pe_det.Talla))
            cmd.Parameters.Add(New SqlParameter("@COLOR", oBeTrans_pe_det.Color))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_pe_det.IdProductoTallaColor))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex1.Message))
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeTrans_pe_det As clsBeTrans_pe_det,
                                      Optional ByVal pConection As SqlConnection = Nothing,
                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_pe_det")
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idestado", "@idestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("precio", "@precio", DataType.Parametro)
            Upd.Add("no_recepcion", "@no_recepcion", DataType.Parametro)
            Upd.Add("ndias", "@ndias", DataType.Parametro)
            Upd.Add("cant_despachada", "@cant_despachada", DataType.Parametro)
            Upd.Add("peso_despachado", "@peso_despachado", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Upd.Add("nom_presentacion", "@nom_presentacion", DataType.Parametro)
            Upd.Add("nom_unid_med", "@nom_unid_med", DataType.Parametro)
            Upd.Add("nom_estado", "@nom_estado", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("fecha_especifica", "@fecha_especifica", DataType.Parametro)
            Upd.Add("roaddes", "@roaddes", DataType.Parametro)
            Upd.Add("roaddesmon", "@roaddesmon", DataType.Parametro)
            Upd.Add("roadtotal", "@roadtotal", DataType.Parametro)
            Upd.Add("roadpreciodoc", "@roadpreciodoc", DataType.Parametro)
            Upd.Add("roadval1", "@roadval1", DataType.Parametro)
            Upd.Add("roadval2", "@roadval2", DataType.Parametro)
            Upd.Add("roadcantproc", "@roadcantproc", DataType.Parametro)
            Upd.Add("no_linea", "@no_linea", DataType.Parametro)
            Upd.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            Upd.Add("idstockespecifico", "@idstockespecifico", DataType.Parametro)
            Upd.Add("EsPadre", "@EsPadre", DataType.Parametro)
            Upd.Add("IdPedidoDetPadre", "@IdPedidoDetPadre", DataType.Parametro)

            '#EJC20210708:Cealsa fiscal Upd.
            Upd.Add("Peso_Bruto", "@Peso_Bruto", DataType.Parametro)
            Upd.Add("Peso_Neto", "@Peso_Neto", DataType.Parametro)
            Upd.Add("Costo", "@Costo", DataType.Parametro)
            Upd.Add("valor_aduana", "@valor_aduana", DataType.Parametro)
            Upd.Add("valor_fob", "@valor_fob", DataType.Parametro)
            Upd.Add("valor_iva", "@valor_iva", DataType.Parametro)
            Upd.Add("valor_dai", "@valor_dai", DataType.Parametro)
            Upd.Add("valor_seguro", "@valor_seguro", DataType.Parametro)
            Upd.Add("valor_flete", "@valor_flete", DataType.Parametro)
            Upd.Add("Total_linea", "@Total_linea", DataType.Parametro)
            Upd.Add("IdCliente", "@IdCliente", DataType.Parametro)
            Upd.Add("Talla", "@Talla", DataType.Parametro)
            Upd.Add("Color", "@Color", DataType.Parametro)
            Upd.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
            Upd.Where(" IdPedidoDet = @IdPedidoDet")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As SqlCommand = Nothing

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_pe_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_det.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_pe_det.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeTrans_pe_det.IdEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_pe_det.IdPresentacion = 0, DBNull.Value, oBeTrans_pe_det.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeTrans_pe_det.IdUnidadMedidaBasica))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_pe_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_pe_det.Peso))
            cmd.Parameters.Add(New SqlParameter("@PRECIO", oBeTrans_pe_det.Precio))
            cmd.Parameters.Add(New SqlParameter("@NO_RECEPCION", oBeTrans_pe_det.No_recepcion))
            cmd.Parameters.Add(New SqlParameter("@NDIAS", oBeTrans_pe_det.Ndias))
            cmd.Parameters.Add(New SqlParameter("@CANT_DESPACHADA", oBeTrans_pe_det.Cant_despachada))
            cmd.Parameters.Add(New SqlParameter("@PESO_DESPACHADO", oBeTrans_pe_det.Peso_despachado))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_pe_det.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", clsPublic.Quitar_Caracteres_No_Permitidos(oBeTrans_pe_det.Nombre_producto)))
            cmd.Parameters.Add(New SqlParameter("@NOM_PRESENTACION", oBeTrans_pe_det.Nom_presentacion))
            cmd.Parameters.Add(New SqlParameter("@NOM_UNID_MED", oBeTrans_pe_det.Nom_unid_med))
            cmd.Parameters.Add(New SqlParameter("@NOM_ESTADO", oBeTrans_pe_det.Nom_estado))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_pe_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_pe_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ESPECIFICA", oBeTrans_pe_det.Fecha_especifica))
            cmd.Parameters.Add(New SqlParameter("@ROADDES", oBeTrans_pe_det.RoadDes))
            cmd.Parameters.Add(New SqlParameter("@ROADDESMON", oBeTrans_pe_det.RoadDesMon))
            cmd.Parameters.Add(New SqlParameter("@ROADTOTAL", oBeTrans_pe_det.RoadTotal))
            cmd.Parameters.Add(New SqlParameter("@ROADPRECIODOC", oBeTrans_pe_det.RoadPrecioDoc))
            cmd.Parameters.Add(New SqlParameter("@ROADVAL1", oBeTrans_pe_det.RoadVAL1))
            cmd.Parameters.Add(New SqlParameter("@ROADVAL2", oBeTrans_pe_det.RoadVAL2))
            cmd.Parameters.Add(New SqlParameter("@ROADCANTPROC", oBeTrans_pe_det.RoadCantProc))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_pe_det.No_linea))
            cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeTrans_pe_det.Atributo_Variante_1))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKESPECIFICO", oBeTrans_pe_det.IdStockEspecifico))
            cmd.Parameters.Add(New SqlParameter("@EsPadre", oBeTrans_pe_det.EsPadre))
            cmd.Parameters.Add(New SqlParameter("@IdPedidoDetPadre", oBeTrans_pe_det.IdPedidoDetPadre))

            '#EJC20210708:Cealsa fiscal Upd params.
            cmd.Parameters.Add(New SqlParameter("@PESO_BRUTO", oBeTrans_pe_det.Peso_Bruto))
            cmd.Parameters.Add(New SqlParameter("@PESO_NETO", oBeTrans_pe_det.Peso_Neto))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeTrans_pe_det.Costo))
            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeTrans_pe_det.valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FOB", oBeTrans_pe_det.valor_fob))
            cmd.Parameters.Add(New SqlParameter("@VALOR_IVA", oBeTrans_pe_det.valor_iva))
            cmd.Parameters.Add(New SqlParameter("@VALOR_DAI", oBeTrans_pe_det.valor_dai))
            cmd.Parameters.Add(New SqlParameter("@VALOR_SEGURO", oBeTrans_pe_det.valor_seguro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FLETE", oBeTrans_pe_det.valor_flete))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LINEA", oBeTrans_pe_det.Total_linea))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeTrans_pe_det.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@TALLA", oBeTrans_pe_det.Talla))
            cmd.Parameters.Add(New SqlParameter("@COLOR", oBeTrans_pe_det.Color))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_pe_det.IdProductoTallaColor))

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

    Public Shared Function ActualizarRemoto(ByRef oBeTrans_pe_det As clsBeTrans_pe_det,
                                            ByRef pConection As SqlConnection,
                                            ByRef pTransaction As SqlTransaction) As Integer

        Try

            Upd.Init("trans_pe_det")
            Upd.Add("idpedidodet", "@idpedidodet", DataType.Parametro)
            Upd.Add("idpedidoenc", "@idpedidoenc", DataType.Parametro)
            Upd.Add("idproductobodega", "@idproductobodega", DataType.Parametro)
            Upd.Add("idestado", "@idestado", DataType.Parametro)
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            Upd.Add("cantidad", "@cantidad", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("precio", "@precio", DataType.Parametro)
            Upd.Add("no_recepcion", "@no_recepcion", DataType.Parametro)
            Upd.Add("ndias", "@ndias", DataType.Parametro)
            Upd.Add("cant_despachada", "@cant_despachada", DataType.Parametro)
            Upd.Add("peso_despachado", "@peso_despachado", DataType.Parametro)
            Upd.Add("codigo_producto", "@codigo_producto", DataType.Parametro)
            Upd.Add("nombre_producto", "@nombre_producto", DataType.Parametro)
            Upd.Add("nom_presentacion", "@nom_presentacion", DataType.Parametro)
            Upd.Add("nom_unid_med", "@nom_unid_med", DataType.Parametro)
            Upd.Add("nom_estado", "@nom_estado", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("fecha_especifica", "@fecha_especifica", DataType.Parametro)
            Upd.Add("roaddes", "@roaddes", DataType.Parametro)
            Upd.Add("roaddesmon", "@roaddesmon", DataType.Parametro)
            Upd.Add("roadtotal", "@roadtotal", DataType.Parametro)
            Upd.Add("roadpreciodoc", "@roadpreciodoc", DataType.Parametro)
            Upd.Add("roadval1", "@roadval1", DataType.Parametro)
            Upd.Add("roadval2", "@roadval2", DataType.Parametro)
            Upd.Add("roadcantproc", "@roadcantproc", DataType.Parametro)
            Upd.Add("no_linea", "@no_linea", DataType.Parametro)
            Upd.Add("atributo_variante_1", "@atributo_variante_1", DataType.Parametro)
            Upd.Add("idstockespecifico", "@idstockespecifico", DataType.Parametro)
            Upd.Add("EsPadre", "@EsPadre", DataType.Parametro)
            Upd.Add("IdPedidoDetPadre", "@IdPedidoDetPadre", DataType.Parametro)

            '#EJC20210708:Cealsa fiscal Upd.
            Upd.Add("Peso_Bruto", "@Peso_Bruto", DataType.Parametro)
            Upd.Add("Peso_Neto", "@Peso_Neto", DataType.Parametro)
            Upd.Add("Costo", "@Costo", DataType.Parametro)
            Upd.Add("valor_aduana", "@valor_aduana", DataType.Parametro)
            Upd.Add("valor_fob", "@valor_fob", DataType.Parametro)
            Upd.Add("valor_iva", "@valor_iva", DataType.Parametro)
            Upd.Add("valor_dai", "@valor_dai", DataType.Parametro)
            Upd.Add("valor_seguro", "@valor_seguro", DataType.Parametro)
            Upd.Add("valor_flete", "@valor_flete", DataType.Parametro)
            Upd.Add("Total_linea", "@Total_linea", DataType.Parametro)
            Upd.Add("IdCliente", "@IdCliente", DataType.Parametro)
            Upd.Add("Talla", "@Talla", DataType.Parametro)
            Upd.Add("Color", "@Color", DataType.Parametro)
            Upd.Add("IdProductoTallaColor", "@IdProductoTallaColor", DataType.Parametro)
            Upd.Where("IdPedidoDet = @IdPedidoDet")

            Dim sp As String = Upd.SQL()
            Dim cmd As SqlCommand = Nothing

            cmd = New SqlCommand(sp, pConection, pTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_pe_det.IdPedidoDet))
            cmd.Parameters.Add(New SqlParameter("@IDPEDIDOENC", oBeTrans_pe_det.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOBODEGA", oBeTrans_pe_det.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IDESTADO", oBeTrans_pe_det.IdEstado))
            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", IIf(oBeTrans_pe_det.IdPresentacion = 0, DBNull.Value, oBeTrans_pe_det.IdPresentacion)))
            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeTrans_pe_det.IdUnidadMedidaBasica))
            cmd.Parameters.Add(New SqlParameter("@CANTIDAD", oBeTrans_pe_det.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeTrans_pe_det.Peso))
            cmd.Parameters.Add(New SqlParameter("@PRECIO", oBeTrans_pe_det.Precio))
            cmd.Parameters.Add(New SqlParameter("@NO_RECEPCION", oBeTrans_pe_det.No_recepcion))
            cmd.Parameters.Add(New SqlParameter("@NDIAS", oBeTrans_pe_det.Ndias))
            cmd.Parameters.Add(New SqlParameter("@CANT_DESPACHADA", oBeTrans_pe_det.Cant_despachada))
            cmd.Parameters.Add(New SqlParameter("@PESO_DESPACHADO", oBeTrans_pe_det.Peso_despachado))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_PRODUCTO", oBeTrans_pe_det.Codigo_Producto))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PRODUCTO", clsPublic.Quitar_Caracteres_No_Permitidos(oBeTrans_pe_det.Nombre_producto)))
            cmd.Parameters.Add(New SqlParameter("@NOM_PRESENTACION", oBeTrans_pe_det.Nom_presentacion))
            cmd.Parameters.Add(New SqlParameter("@NOM_UNID_MED", oBeTrans_pe_det.Nom_unid_med))
            cmd.Parameters.Add(New SqlParameter("@NOM_ESTADO", oBeTrans_pe_det.Nom_estado))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeTrans_pe_det.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeTrans_pe_det.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@FECHA_ESPECIFICA", oBeTrans_pe_det.Fecha_especifica))
            cmd.Parameters.Add(New SqlParameter("@ROADDES", oBeTrans_pe_det.RoadDes))
            cmd.Parameters.Add(New SqlParameter("@ROADDESMON", oBeTrans_pe_det.RoadDesMon))
            cmd.Parameters.Add(New SqlParameter("@ROADTOTAL", oBeTrans_pe_det.RoadTotal))
            cmd.Parameters.Add(New SqlParameter("@ROADPRECIODOC", oBeTrans_pe_det.RoadPrecioDoc))
            cmd.Parameters.Add(New SqlParameter("@ROADVAL1", oBeTrans_pe_det.RoadVAL1))
            cmd.Parameters.Add(New SqlParameter("@ROADVAL2", oBeTrans_pe_det.RoadVAL2))
            cmd.Parameters.Add(New SqlParameter("@ROADCANTPROC", oBeTrans_pe_det.RoadCantProc))
            cmd.Parameters.Add(New SqlParameter("@NO_LINEA", oBeTrans_pe_det.No_linea))
            cmd.Parameters.Add(New SqlParameter("@ATRIBUTO_VARIANTE_1", oBeTrans_pe_det.Atributo_Variante_1))
            cmd.Parameters.Add(New SqlParameter("@IDSTOCKESPECIFICO", oBeTrans_pe_det.IdStockEspecifico))
            cmd.Parameters.Add(New SqlParameter("@EsPadre", oBeTrans_pe_det.EsPadre))
            cmd.Parameters.Add(New SqlParameter("@IdPedidoDetPadre", oBeTrans_pe_det.IdPedidoDetPadre))

            '#EJC20210708:Cealsa fiscal Upd params.
            cmd.Parameters.Add(New SqlParameter("@PESO_BRUTO", oBeTrans_pe_det.Peso_Bruto))
            cmd.Parameters.Add(New SqlParameter("@PESO_NETO", oBeTrans_pe_det.Peso_Neto))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeTrans_pe_det.Costo))
            cmd.Parameters.Add(New SqlParameter("@VALOR_ADUANA", oBeTrans_pe_det.valor_aduana))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FOB", oBeTrans_pe_det.valor_fob))
            cmd.Parameters.Add(New SqlParameter("@VALOR_IVA", oBeTrans_pe_det.valor_iva))
            cmd.Parameters.Add(New SqlParameter("@VALOR_DAI", oBeTrans_pe_det.valor_dai))
            cmd.Parameters.Add(New SqlParameter("@VALOR_SEGURO", oBeTrans_pe_det.valor_seguro))
            cmd.Parameters.Add(New SqlParameter("@VALOR_FLETE", oBeTrans_pe_det.valor_flete))
            cmd.Parameters.Add(New SqlParameter("@TOTAL_LINEA", oBeTrans_pe_det.Total_linea))
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeTrans_pe_det.IdCliente))
            cmd.Parameters.Add(New SqlParameter("@TALLA", oBeTrans_pe_det.Talla))
            cmd.Parameters.Add(New SqlParameter("@COLOR", oBeTrans_pe_det.Color))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOTALLACOLOR", oBeTrans_pe_det.IdProductoTallaColor))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeTrans_pe_det As clsBeTrans_pe_det, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_pe_det" &
             "  Where(IdPedidoDet = @IdPedidoDet)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", oBeTrans_pe_det.IdPedidoDet))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try
    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_pe_det As clsBeTrans_pe_det)

        Try

            Const sp As String = "SELECT * FROM Trans_pe_det" &
            " Where(IdPedidoDet = @IdPedidoDet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPEDIDODET", pBeTrans_pe_det.IdPedidoDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_pe_det, dt.Rows(0))
            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPedidoDet:=pBeTrans_pe_det.IdPedidoDet)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdPedidoDet(ByVal pIdPedidoDet As Integer) As clsBeTrans_pe_det

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Single_By_IdPedidoDet = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Trans_pe_det Where(IdPedidoDet = @IdPedidoDet)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPEDIDODET", pIdPedidoDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_pe_det As New clsBeTrans_pe_det()
                Cargar(pBeTrans_pe_det, dt.Rows(0))
                Get_Single_By_IdPedidoDet = pBeTrans_pe_det
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPedidoDet:=pIdPedidoDet)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                                 ByRef pConection As SqlConnection,
                                                 ByRef pTransaction As SqlTransaction) As clsBeTrans_pe_det

        If pConection Is Nothing OrElse pTransaction Is Nothing Then
            Throw New ArgumentNullException("La conexión y la transacción no pueden ser nulas.")
        End If

        Dim resultado As clsBeTrans_pe_det = Nothing

        Try
            Const sp As String = "SELECT * FROM Trans_pe_det WHERE IdPedidoDet = @IdPedidoDet"

            Using cmd As New SqlCommand(sp, pConection, pTransaction)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add(New SqlParameter("@IDPEDIDODET", pIdPedidoDet))

                Using dad As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count = 1 Then
                        resultado = New clsBeTrans_pe_det()
                        Cargar(resultado, dt.Rows(0))
                    End If
                End Using
            End Using

        Catch ex As SqlException
            Throw New Exception($"Error SQL en {MethodBase.GetCurrentMethod.Name}: {ex.Message}", ex)
        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            'clsLnLog_error_wms.Agregar_Error($"{MethodBase.GetCurrentMethod.Name} - {ex.Message}")
            Dim vMsg As String = $"{MethodBase.GetCurrentMethod.Name}: {ex.Message}"
            clsLnLog_error_wms_pe.Agregar_Error(vMsg, pStackTrace:=ex.StackTrace, pIdPedidoDet:=pIdPedidoDet)

            Throw New Exception($"Error en {MethodBase.GetCurrentMethod.Name}: {ex.Message}", ex)
        End Try

        Return resultado
    End Function


    Public Shared Function Get_Single_By_IdPedidoEnc_And_IdPedidoDet(ByVal pIdPedidoEnc As Integer,
                                                                     ByVal pIdPedidoDet As Integer,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As clsBeTrans_pe_det

        Get_Single_By_IdPedidoEnc_And_IdPedidoDet = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_pe_det " &
            " Where(IdPedidoDet = @IdPedidoDet AND IdPedidoEnc = @IdPedidoEnc)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPEDIDODET", pIdPedidoDet))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPEDIDOENC", pIdPedidoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_pe_det As New clsBeTrans_pe_det()
                Cargar(pBeTrans_pe_det, dt.Rows(0))
                Get_Single_By_IdPedidoEnc_And_IdPedidoDet = pBeTrans_pe_det
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPedidoEnc:=pIdPedidoEnc, pIdPedidoDet:=pIdPedidoDet)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTrans_pe_det As clsBeTrans_pe_det,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        GetSingle = False

        Try

            Const sp As String = "SELECT * FROM Trans_pe_det 
			 Where(IdPedidoDet = @IdPedidoDet)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPEDIDODET", pBeTrans_pe_det.IdPedidoDet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_pe_det, dt.Rows(0), lConnection, lTransaction)
                GetSingle = True
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError,
                                                pStackTrace:=ex.StackTrace,
                                                pIdPedidoDet:=pBeTrans_pe_det.IdPedidoDet,
                                                pIdPedidoEnc:=pBeTrans_pe_det?.IdPedidoEnc)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdPedidoDet),0) FROM Trans_pe_det"

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

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            '#MECR15102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

End Class
