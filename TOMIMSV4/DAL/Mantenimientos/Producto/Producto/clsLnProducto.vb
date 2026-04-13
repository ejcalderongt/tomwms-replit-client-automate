Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnProducto

    Private Shared Sub ValidarLongitudTexto(nombrePropiedad As String,
                                        valor As String,
                                        maximo As Integer)

        If valor IsNot Nothing AndAlso valor.Length > maximo Then
            Throw New Exception("La propiedad [" & nombrePropiedad & "] excede el tamaño permitido. " &
                            "Longitud actual: " & valor.Length & ", máximo permitido: " & maximo &
                            ". Valor: " & valor)
        End If

    End Sub

    '#GT30032026: se añade validación truncate en codigo, barra o el nombre del producto.
    Public Shared Function Insertar(ByRef oBeProducto As clsBeProducto,
                                Optional ByVal pConection As SqlConnection = Nothing,
                                Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("producto")
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            If Not oBeProducto.IdPropietario = 0 Then Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
            If Not oBeProducto.IdClasificacion = 0 Then Ins.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
            If Not oBeProducto.IdFamilia = 0 Then Ins.Add("idfamilia", "@idfamilia", DataType.Parametro)
            If Not oBeProducto.IdMarca = 0 Then Ins.Add("idmarca", "@idmarca", DataType.Parametro)
            If Not oBeProducto.IdTipoProducto = 0 Then Ins.Add("idtipoproducto", "@idtipoproducto", DataType.Parametro)
            If Not oBeProducto.IdUnidadMedidaBasica = 0 Then Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            If Not oBeProducto.IdCamara = 0 Then Ins.Add("idcamara", "@idcamara", DataType.Parametro)
            If Not oBeProducto.IdTipoRotacion = 0 Then Ins.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
            If Not oBeProducto.IdPerfilSerializado = 0 Then Ins.Add("idperfilserializado", "@idperfilserializado", DataType.Parametro)
            If Not oBeProducto.IdIndiceRotacion = 0 Then Ins.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
            If Not oBeProducto.IdSimbologia = 0 Then Ins.Add("idsimbologia", "@idsimbologia", DataType.Parametro)
            If Not oBeProducto.IdArancel = 0 Then Ins.Add("idarancel", "@idarancel", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("precio", "@precio", DataType.Parametro)
            Ins.Add("existencia_min", "@existencia_min", DataType.Parametro)
            Ins.Add("existencia_max", "@existencia_max", DataType.Parametro)
            Ins.Add("costo", "@costo", DataType.Parametro)
            Ins.Add("peso_referencia", "@peso_referencia", DataType.Parametro)
            Ins.Add("peso_tolerancia", "@peso_tolerancia", DataType.Parametro)
            Ins.Add("temperatura_referencia", "@temperatura_referencia", DataType.Parametro)
            Ins.Add("temperatura_tolerancia", "@temperatura_tolerancia", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("serializado", "@serializado", DataType.Parametro)
            Ins.Add("genera_lote", "@genera_lote", DataType.Parametro)
            Ins.Add("genera_lp_old", "@genera_lp_old", DataType.Parametro)
            Ins.Add("control_vencimiento", "@control_vencimiento", DataType.Parametro)
            Ins.Add("control_lote", "@control_lote", DataType.Parametro)
            Ins.Add("peso_recepcion", "@peso_recepcion", DataType.Parametro)
            Ins.Add("peso_despacho", "@peso_despacho", DataType.Parametro)
            Ins.Add("temperatura_recepcion", "@temperatura_recepcion", DataType.Parametro)
            Ins.Add("temperatura_despacho", "@temperatura_despacho", DataType.Parametro)
            Ins.Add("materia_prima", "@materia_prima", DataType.Parametro)
            Ins.Add("kit", "@kit", DataType.Parametro)
            Ins.Add("tolerancia", "@tolerancia", DataType.Parametro)
            Ins.Add("ciclo_vida", "@ciclo_vida", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            If Not oBeProducto.Imagen Is Nothing Then Ins.Add("imagen", "@imagen", DataType.Parametro)
            Ins.Add("noserie", "@noserie", DataType.Parametro)
            Ins.Add("noparte", "@noparte", DataType.Parametro)
            Ins.Add("fechamanufactura", "@fechamanufactura", DataType.Parametro)
            Ins.Add("capturar_aniada", "@capturar_aniada", DataType.Parametro)
            Ins.Add("control_peso", "@control_peso", DataType.Parametro)
            Ins.Add("captura_arancel", "@captura_arancel", DataType.Parametro)
            Ins.Add("es_hardware", "@es_hardware", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("margen_impresion", "@margen_impresion", DataType.Parametro)
            Ins.Add("idunidadmedidacobro", "@idunidadmedidacobro", DataType.Parametro)
            Ins.Add("IdTipoEtiqueta", "@IdTipoEtiqueta", DataType.Parametro)
            Ins.Add("IdProductoParametroA", "@IdProductoParametroA", DataType.Parametro)
            Ins.Add("IdProductoParametroB", "@IdProductoParametroB", DataType.Parametro)
            Ins.Add("IdTipoManufactura", "@IdTipoManufactura", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            ValidarLongitudTexto("Codigo", oBeProducto.Codigo, 50)
            ValidarLongitudTexto("Codigo_barra", oBeProducto.Codigo_barra, 35)
            ValidarLongitudTexto("Nombre", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto.Nombre), 200)

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto.IdProducto))
            If Not oBeProducto.IdPropietario = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto.IdPropietario))
            If Not oBeProducto.IdClasificacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeProducto.IdClasificacion))
            If Not oBeProducto.IdFamilia = 0 Then cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeProducto.IdFamilia))
            If Not oBeProducto.IdMarca = 0 Then cmd.Parameters.Add(New SqlParameter("@IDMARCA", oBeProducto.IdMarca))
            If Not oBeProducto.IdTipoProducto = 0 Then cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTO", oBeProducto.IdTipoProducto))
            If Not oBeProducto.IdUnidadMedidaBasica = 0 Then cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeProducto.IdUnidadMedidaBasica))
            If Not oBeProducto.IdCamara = 0 Then cmd.Parameters.Add(New SqlParameter("@IDCAMARA", oBeProducto.IdCamara))
            If Not oBeProducto.IdTipoRotacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeProducto.IdTipoRotacion))
            If Not oBeProducto.IdPerfilSerializado = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPERFILSERIALIZADO", oBeProducto.IdPerfilSerializado))
            If Not oBeProducto.IdIndiceRotacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeProducto.IdIndiceRotacion))
            If Not oBeProducto.IdSimbologia = 0 Then cmd.Parameters.Add(New SqlParameter("@IDSIMBOLOGIA", oBeProducto.IdSimbologia))
            If Not oBeProducto.IdArancel = 0 Then cmd.Parameters.Add(New SqlParameter("@IDARANCEL", oBeProducto.IdArancel))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeProducto.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@PRECIO", oBeProducto.Precio))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIA_MIN", oBeProducto.Existencia_min))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIA_MAX", oBeProducto.Existencia_max))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeProducto.Costo))
            cmd.Parameters.Add(New SqlParameter("@PESO_REFERENCIA", oBeProducto.Peso_referencia))
            cmd.Parameters.Add(New SqlParameter("@PESO_TOLERANCIA", oBeProducto.Peso_tolerancia))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_REFERENCIA", oBeProducto.Temperatura_referencia))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_TOLERANCIA", oBeProducto.Temperatura_tolerancia))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto.Activo))
            cmd.Parameters.Add(New SqlParameter("@SERIALIZADO", oBeProducto.Serializado))
            cmd.Parameters.Add(New SqlParameter("@GENERA_LOTE", oBeProducto.Genera_lote))
            cmd.Parameters.Add(New SqlParameter("@GENERA_LP_OLD", oBeProducto.Genera_lp))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_VENCIMIENTO", oBeProducto.Control_vencimiento))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_LOTE", oBeProducto.Control_lote))
            cmd.Parameters.Add(New SqlParameter("@PESO_RECEPCION", oBeProducto.Peso_recepcion))
            cmd.Parameters.Add(New SqlParameter("@PESO_DESPACHO", oBeProducto.Peso_despacho))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_RECEPCION", oBeProducto.Temperatura_recepcion))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_DESPACHO", oBeProducto.Temperatura_despacho))
            cmd.Parameters.Add(New SqlParameter("@MATERIA_PRIMA", oBeProducto.Materia_prima))
            cmd.Parameters.Add(New SqlParameter("@KIT", oBeProducto.Kit))
            cmd.Parameters.Add(New SqlParameter("@TOLERANCIA", oBeProducto.Tolerancia))
            cmd.Parameters.Add(New SqlParameter("@CICLO_VIDA", oBeProducto.Ciclo_vida))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto.Fec_mod))
            If Not oBeProducto.Imagen Is Nothing Then cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeProducto.Imagen))
            cmd.Parameters.Add(New SqlParameter("@NOSERIE", oBeProducto.Noserie))
            cmd.Parameters.Add(New SqlParameter("@NOPARTE", oBeProducto.Noparte))
            cmd.Parameters.Add(New SqlParameter("@FECHAMANUFACTURA", oBeProducto.Fechamanufactura))
            cmd.Parameters.Add(New SqlParameter("@CAPTURAR_ANIADA", oBeProducto.Capturar_aniada))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_PESO", oBeProducto.Control_peso))
            cmd.Parameters.Add(New SqlParameter("@CAPTURA_ARANCEL", oBeProducto.Captura_arancel))
            cmd.Parameters.Add(New SqlParameter("@ES_HARDWARE", oBeProducto.Es_hardware))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeProducto.Largo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeProducto.Alto))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeProducto.Ancho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IMPRESION", oBeProducto.Margen_Impresion))

            If oBeProducto.IdUnidadMedidaCobro = 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDACOBRO", DBNull.Value))
            Else
                cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDACOBRO", oBeProducto.IdUnidadMedidaCobro))
            End If

            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", oBeProducto.IdTipoEtiqueta))
            cmd.Parameters.Add(New SqlParameter("@DIAS_INVENTARIO_PROMEDIO", oBeProducto.Dias_Inventario_Promedio))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETROA", oBeProducto.IdProductoParametroA))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETROB", oBeProducto.IdProductoParametroB))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOMANUFACTURA", oBeProducto.IdTipoManufactura))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            oBeProducto.IdProducto = CInt(cmd.Parameters("@IDPRODUCTO").Value)

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then
                Try
                    lTransaction.Rollback()
                Catch
                End Try
            End If
            Throw
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    'Public Shared Function Insertar(ByRef oBeProducto As clsBeProducto, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing

    '    Try

    '        Ins.Init("producto")
    '        Ins.Add("idproducto", "@idproducto", DataType.Parametro)
    '        If Not oBeProducto.IdPropietario = 0 Then Ins.Add("idpropietario", "@idpropietario", DataType.Parametro)
    '        If Not oBeProducto.IdClasificacion = 0 Then Ins.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
    '        If Not oBeProducto.IdFamilia = 0 Then Ins.Add("idfamilia", "@idfamilia", DataType.Parametro)
    '        If Not oBeProducto.IdMarca = 0 Then Ins.Add("idmarca", "@idmarca", DataType.Parametro)
    '        If Not oBeProducto.IdTipoProducto = 0 Then Ins.Add("idtipoproducto", "@idtipoproducto", DataType.Parametro)
    '        If Not oBeProducto.IdUnidadMedidaBasica = 0 Then Ins.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
    '        If Not oBeProducto.IdCamara = 0 Then Ins.Add("idcamara", "@idcamara", DataType.Parametro)
    '        If Not oBeProducto.IdTipoRotacion = 0 Then Ins.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
    '        If Not oBeProducto.IdPerfilSerializado = 0 Then Ins.Add("idperfilserializado", "@idperfilserializado", DataType.Parametro)
    '        If Not oBeProducto.IdIndiceRotacion = 0 Then Ins.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
    '        If Not oBeProducto.IdSimbologia = 0 Then Ins.Add("idsimbologia", "@idsimbologia", DataType.Parametro)
    '        If Not oBeProducto.IdArancel = 0 Then Ins.Add("idarancel", "@idarancel", DataType.Parametro)
    '        Ins.Add("codigo", "@codigo", DataType.Parametro)
    '        Ins.Add("nombre", "@nombre", DataType.Parametro)
    '        Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
    '        Ins.Add("precio", "@precio", DataType.Parametro)
    '        Ins.Add("existencia_min", "@existencia_min", DataType.Parametro)
    '        Ins.Add("existencia_max", "@existencia_max", DataType.Parametro)
    '        Ins.Add("costo", "@costo", DataType.Parametro)
    '        Ins.Add("peso_referencia", "@peso_referencia", DataType.Parametro)
    '        Ins.Add("peso_tolerancia", "@peso_tolerancia", DataType.Parametro)
    '        Ins.Add("temperatura_referencia", "@temperatura_referencia", DataType.Parametro)
    '        Ins.Add("temperatura_tolerancia", "@temperatura_tolerancia", DataType.Parametro)
    '        Ins.Add("activo", "@activo", DataType.Parametro)
    '        Ins.Add("serializado", "@serializado", DataType.Parametro)
    '        Ins.Add("genera_lote", "@genera_lote", DataType.Parametro)
    '        Ins.Add("genera_lp_old", "@genera_lp_old", DataType.Parametro)
    '        Ins.Add("control_vencimiento", "@control_vencimiento", DataType.Parametro)
    '        Ins.Add("control_lote", "@control_lote", DataType.Parametro)
    '        Ins.Add("peso_recepcion", "@peso_recepcion", DataType.Parametro)
    '        Ins.Add("peso_despacho", "@peso_despacho", DataType.Parametro)
    '        Ins.Add("temperatura_recepcion", "@temperatura_recepcion", DataType.Parametro)
    '        Ins.Add("temperatura_despacho", "@temperatura_despacho", DataType.Parametro)
    '        Ins.Add("materia_prima", "@materia_prima", DataType.Parametro)
    '        Ins.Add("kit", "@kit", DataType.Parametro)
    '        Ins.Add("tolerancia", "@tolerancia", DataType.Parametro)
    '        Ins.Add("ciclo_vida", "@ciclo_vida", DataType.Parametro)
    '        Ins.Add("user_agr", "@user_agr", DataType.Parametro)
    '        Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
    '        Ins.Add("user_mod", "@user_mod", DataType.Parametro)
    '        Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
    '        If Not oBeProducto.Imagen Is Nothing Then Ins.Add("imagen", "@imagen", DataType.Parametro)
    '        Ins.Add("noserie", "@noserie", DataType.Parametro)
    '        Ins.Add("noparte", "@noparte", DataType.Parametro)
    '        Ins.Add("fechamanufactura", "@fechamanufactura", DataType.Parametro)
    '        Ins.Add("capturar_aniada", "@capturar_aniada", DataType.Parametro)
    '        Ins.Add("control_peso", "@control_peso", DataType.Parametro)
    '        Ins.Add("captura_arancel", "@captura_arancel", DataType.Parametro)
    '        Ins.Add("es_hardware", "@es_hardware", DataType.Parametro)
    '        Ins.Add("largo", "@largo", DataType.Parametro)
    '        Ins.Add("alto", "@alto", DataType.Parametro)
    '        Ins.Add("ancho", "@ancho", DataType.Parametro)
    '        Ins.Add("margen_impresion", "@margen_impresion", DataType.Parametro)
    '        'If Not oBeProducto.IdUnidadMedidaCobro = 0 Then Ins.Add("idunidadmedidacobro", "@idunidadmedidacobro", DataType.Parametro)
    '        Ins.Add("idunidadmedidacobro", "@idunidadmedidacobro", DataType.Parametro)
    '        Ins.Add("IdTipoEtiqueta", "@IdTipoEtiqueta", DataType.Parametro)
    '        Ins.Add("IdProductoParametroA", "@IdProductoParametroA", DataType.Parametro)
    '        Ins.Add("IdProductoParametroB", "@IdProductoParametroB", DataType.Parametro)
    '        Ins.Add("IdTipoManufactura", "@IdTipoManufactura", DataType.Parametro)

    '        Dim sp As String = Ins.SQL()
    '        Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

    '        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

    '        If Es_Transaccion_Remota Then
    '            cmd = New SqlCommand(sp, pConection, pTransaction)
    '        Else
    '            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
    '            cmd = New SqlCommand(sp, lConnection, lTransaction)
    '        End If

    '        cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto.IdProducto))
    '        If Not oBeProducto.IdPropietario = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto.IdPropietario))
    '        If Not oBeProducto.IdClasificacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeProducto.IdClasificacion))
    '        If Not oBeProducto.IdFamilia = 0 Then cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeProducto.IdFamilia))
    '        If Not oBeProducto.IdMarca = 0 Then cmd.Parameters.Add(New SqlParameter("@IDMARCA", oBeProducto.IdMarca))
    '        If Not oBeProducto.IdTipoProducto = 0 Then cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTO", oBeProducto.IdTipoProducto))
    '        If Not oBeProducto.IdUnidadMedidaBasica = 0 Then cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeProducto.IdUnidadMedidaBasica))
    '        If Not oBeProducto.IdCamara = 0 Then cmd.Parameters.Add(New SqlParameter("@IDCAMARA", oBeProducto.IdCamara))
    '        If Not oBeProducto.IdTipoRotacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeProducto.IdTipoRotacion))
    '        If Not oBeProducto.IdPerfilSerializado = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPERFILSERIALIZADO", oBeProducto.IdPerfilSerializado))
    '        If Not oBeProducto.IdIndiceRotacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeProducto.IdIndiceRotacion))
    '        If Not oBeProducto.IdSimbologia = 0 Then cmd.Parameters.Add(New SqlParameter("@IDSIMBOLOGIA", oBeProducto.IdSimbologia))
    '        If Not oBeProducto.IdArancel = 0 Then cmd.Parameters.Add(New SqlParameter("@IDARANCEL", oBeProducto.IdArancel))
    '        cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto.Codigo))
    '        cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto.Nombre)))
    '        cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeProducto.Codigo_barra))
    '        cmd.Parameters.Add(New SqlParameter("@PRECIO", oBeProducto.Precio))
    '        cmd.Parameters.Add(New SqlParameter("@EXISTENCIA_MIN", oBeProducto.Existencia_min))
    '        cmd.Parameters.Add(New SqlParameter("@EXISTENCIA_MAX", oBeProducto.Existencia_max))
    '        cmd.Parameters.Add(New SqlParameter("@COSTO", oBeProducto.Costo))
    '        cmd.Parameters.Add(New SqlParameter("@PESO_REFERENCIA", oBeProducto.Peso_referencia))
    '        cmd.Parameters.Add(New SqlParameter("@PESO_TOLERANCIA", oBeProducto.Peso_tolerancia))
    '        cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_REFERENCIA", oBeProducto.Temperatura_referencia))
    '        cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_TOLERANCIA", oBeProducto.Temperatura_tolerancia))
    '        cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto.Activo))
    '        cmd.Parameters.Add(New SqlParameter("@SERIALIZADO", oBeProducto.Serializado))
    '        cmd.Parameters.Add(New SqlParameter("@GENERA_LOTE", oBeProducto.Genera_lote))
    '        cmd.Parameters.Add(New SqlParameter("@GENERA_LP_OLD", oBeProducto.Genera_lp))
    '        cmd.Parameters.Add(New SqlParameter("@CONTROL_VENCIMIENTO", oBeProducto.Control_vencimiento))
    '        cmd.Parameters.Add(New SqlParameter("@CONTROL_LOTE", oBeProducto.Control_lote))
    '        cmd.Parameters.Add(New SqlParameter("@PESO_RECEPCION", oBeProducto.Peso_recepcion))
    '        cmd.Parameters.Add(New SqlParameter("@PESO_DESPACHO", oBeProducto.Peso_despacho))
    '        cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_RECEPCION", oBeProducto.Temperatura_recepcion))
    '        cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_DESPACHO", oBeProducto.Temperatura_despacho))
    '        cmd.Parameters.Add(New SqlParameter("@MATERIA_PRIMA", oBeProducto.Materia_prima))
    '        cmd.Parameters.Add(New SqlParameter("@KIT", oBeProducto.Kit))
    '        cmd.Parameters.Add(New SqlParameter("@TOLERANCIA", oBeProducto.Tolerancia))
    '        cmd.Parameters.Add(New SqlParameter("@CICLO_VIDA", oBeProducto.Ciclo_vida))
    '        cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto.User_agr))
    '        cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto.Fec_agr))
    '        cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto.User_mod))
    '        cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto.Fec_mod))
    '        If Not oBeProducto.Imagen Is Nothing Then cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeProducto.Imagen))
    '        cmd.Parameters.Add(New SqlParameter("@NOSERIE", oBeProducto.Noserie))
    '        cmd.Parameters.Add(New SqlParameter("@NOPARTE", oBeProducto.Noparte))
    '        cmd.Parameters.Add(New SqlParameter("@FECHAMANUFACTURA", oBeProducto.Fechamanufactura))
    '        cmd.Parameters.Add(New SqlParameter("@CAPTURAR_ANIADA", oBeProducto.Capturar_aniada))
    '        cmd.Parameters.Add(New SqlParameter("@CONTROL_PESO", oBeProducto.Control_peso))
    '        cmd.Parameters.Add(New SqlParameter("@CAPTURA_ARANCEL", oBeProducto.Captura_arancel))
    '        cmd.Parameters.Add(New SqlParameter("@ES_HARDWARE", oBeProducto.Es_hardware))
    '        cmd.Parameters.Add(New SqlParameter("@LARGO", oBeProducto.Largo))
    '        cmd.Parameters.Add(New SqlParameter("@ALTO", oBeProducto.Alto))
    '        cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeProducto.Ancho))
    '        cmd.Parameters.Add(New SqlParameter("@MARGEN_IMPRESION", oBeProducto.Margen_Impresion))
    '        'If Not oBeProducto.IdUnidadMedidaCobro = 0 Then cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDACOBRO", oBeProducto.IdUnidadMedidaCobro))
    '        If oBeProducto.IdUnidadMedidaCobro = 0 Then
    '            cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDACOBRO", DBNull.Value))
    '        End If
    '        cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", oBeProducto.IdTipoEtiqueta))
    '        cmd.Parameters.Add(New SqlParameter("@DIAS_INVENTARIO_PROMEDIO", oBeProducto.Dias_Inventario_Promedio))
    '        cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETROA", oBeProducto.IdProductoParametroA))
    '        cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETROB", oBeProducto.IdProductoParametroB))
    '        cmd.Parameters.Add(New SqlParameter("@IDTIPOMANUFACTURA", oBeProducto.IdTipoManufactura))

    '        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

    '        cmd.Dispose()

    '        If Not Es_Transaccion_Remota Then lTransaction.Commit()

    '        oBeProducto.IdProducto = CInt(cmd.Parameters("@IDPRODUCTO").Value)

    '        Return rowsAffected

    '    Catch ex As Exception
    '        If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex
    '    Finally
    '        If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '        If lTransaction IsNot Nothing Then lTransaction.Dispose()
    '    End Try

    'End Function

    Public Shared Function Actualizar(ByRef oBeProducto As clsBeProducto, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("producto")
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            If Not oBeProducto.IdPropietario = 0 Then Upd.Add("idpropietario", "@idpropietario", DataType.Parametro)
            If Not oBeProducto.IdClasificacion = 0 Then Upd.Add("idclasificacion", "@idclasificacion", DataType.Parametro)
            If Not oBeProducto.IdFamilia = 0 Then Upd.Add("idfamilia", "@idfamilia", DataType.Parametro)
            If Not oBeProducto.IdMarca = 0 Then Upd.Add("idmarca", "@idmarca", DataType.Parametro)
            If Not oBeProducto.IdTipoProducto = 0 Then Upd.Add("idtipoproducto", "@idtipoproducto", DataType.Parametro)
            If Not oBeProducto.IdUnidadMedidaBasica = 0 Then Upd.Add("idunidadmedidabasica", "@idunidadmedidabasica", DataType.Parametro)
            If Not oBeProducto.IdCamara = 0 Then Upd.Add("idcamara", "@idcamara", DataType.Parametro)
            If Not oBeProducto.IdTipoRotacion = 0 Then Upd.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
            If Not oBeProducto.IdPerfilSerializado = 0 Then Upd.Add("idperfilserializado", "@idperfilserializado", DataType.Parametro)
            If Not oBeProducto.IdIndiceRotacion = 0 Then Upd.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
            If Not oBeProducto.IdSimbologia = 0 Then Upd.Add("idsimbologia", "@idsimbologia", DataType.Parametro)
            If Not oBeProducto.IdArancel = 0 Then Upd.Add("idarancel", "@idarancel", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("precio", "@precio", DataType.Parametro)
            Upd.Add("existencia_min", "@existencia_min", DataType.Parametro)
            Upd.Add("existencia_max", "@existencia_max", DataType.Parametro)
            Upd.Add("costo", "@costo", DataType.Parametro)
            Upd.Add("peso_referencia", "@peso_referencia", DataType.Parametro)
            Upd.Add("peso_tolerancia", "@peso_tolerancia", DataType.Parametro)
            Upd.Add("temperatura_referencia", "@temperatura_referencia", DataType.Parametro)
            Upd.Add("temperatura_tolerancia", "@temperatura_tolerancia", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("serializado", "@serializado", DataType.Parametro)
            Upd.Add("genera_lote", "@genera_lote", DataType.Parametro)
            Upd.Add("genera_lp_old", "@genera_lp_old", DataType.Parametro)
            Upd.Add("control_vencimiento", "@control_vencimiento", DataType.Parametro)
            Upd.Add("control_lote", "@control_lote", DataType.Parametro)
            Upd.Add("peso_recepcion", "@peso_recepcion", DataType.Parametro)
            Upd.Add("peso_despacho", "@peso_despacho", DataType.Parametro)
            Upd.Add("temperatura_recepcion", "@temperatura_recepcion", DataType.Parametro)
            Upd.Add("temperatura_despacho", "@temperatura_despacho", DataType.Parametro)
            Upd.Add("materia_prima", "@materia_prima", DataType.Parametro)
            Upd.Add("kit", "@kit", DataType.Parametro)
            Upd.Add("tolerancia", "@tolerancia", DataType.Parametro)
            Upd.Add("ciclo_vida", "@ciclo_vida", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            If Not oBeProducto.Imagen Is Nothing Then Upd.Add("imagen", "@imagen", DataType.Parametro)
            Upd.Add("noserie", "@noserie", DataType.Parametro)
            Upd.Add("noparte", "@noparte", DataType.Parametro)
            Upd.Add("fechamanufactura", "@fechamanufactura", DataType.Parametro)
            Upd.Add("capturar_aniada", "@capturar_aniada", DataType.Parametro)
            Upd.Add("control_peso", "@control_peso", DataType.Parametro)
            Upd.Add("captura_arancel", "@captura_arancel", DataType.Parametro)
            Upd.Add("es_hardware", "@es_hardware", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("margen_impresion", "@margen_impresion", DataType.Parametro)
            'If Not oBeProducto.IdUnidadMedidaCobro = 0 Then Upd.Add("idunidadmedidacobro", "@idunidadmedidacobro", DataType.Parametro)
            Upd.Add("idunidadmedidacobro", "@idunidadmedidacobro", DataType.Parametro)
            Upd.Add("IdTipoEtiqueta", "@IdTipoEtiqueta", DataType.Parametro)
            Upd.Add("dias_inventario_promedio", "@dias_inventario_promedio", DataType.Parametro)
            Upd.Add("IdProductoParametroA", "@IdProductoParametroA", DataType.Parametro)
            Upd.Add("IdProductoParametroB", "@IdProductoParametroB", DataType.Parametro)
            Upd.Add("IdTipoManufactura", "@IdTipoManufactura", DataType.Parametro)
            Upd.Where("IdProducto = @IdProducto")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto.IdProducto))
            If Not oBeProducto.IdPropietario = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIO", oBeProducto.IdPropietario))
            If Not oBeProducto.IdClasificacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDCLASIFICACION", oBeProducto.IdClasificacion))
            If Not oBeProducto.IdFamilia = 0 Then cmd.Parameters.Add(New SqlParameter("@IDFAMILIA", oBeProducto.IdFamilia))
            If Not oBeProducto.IdMarca = 0 Then cmd.Parameters.Add(New SqlParameter("@IDMARCA", oBeProducto.IdMarca))
            If Not oBeProducto.IdTipoProducto = 0 Then cmd.Parameters.Add(New SqlParameter("@IDTIPOPRODUCTO", oBeProducto.IdTipoProducto))
            If Not oBeProducto.IdUnidadMedidaBasica = 0 Then cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDABASICA", oBeProducto.IdUnidadMedidaBasica))
            If Not oBeProducto.IdCamara = 0 Then cmd.Parameters.Add(New SqlParameter("@IDCAMARA", oBeProducto.IdCamara))
            If Not oBeProducto.IdTipoRotacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeProducto.IdTipoRotacion))
            If Not oBeProducto.IdPerfilSerializado = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPERFILSERIALIZADO", oBeProducto.IdPerfilSerializado))
            If Not oBeProducto.IdIndiceRotacion = 0 Then cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeProducto.IdIndiceRotacion))
            If Not oBeProducto.IdSimbologia = 0 Then cmd.Parameters.Add(New SqlParameter("@IDSIMBOLOGIA", oBeProducto.IdSimbologia))
            If Not oBeProducto.IdArancel = 0 Then cmd.Parameters.Add(New SqlParameter("@IDARANCEL", oBeProducto.IdArancel))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto.Codigo))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeProducto.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@PRECIO", oBeProducto.Precio))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIA_MIN", oBeProducto.Existencia_min))
            cmd.Parameters.Add(New SqlParameter("@EXISTENCIA_MAX", oBeProducto.Existencia_max))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeProducto.Costo))
            cmd.Parameters.Add(New SqlParameter("@PESO_REFERENCIA", oBeProducto.Peso_referencia))
            cmd.Parameters.Add(New SqlParameter("@PESO_TOLERANCIA", oBeProducto.Peso_tolerancia))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_REFERENCIA", oBeProducto.Temperatura_referencia))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_TOLERANCIA", oBeProducto.Temperatura_tolerancia))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto.Activo))
            cmd.Parameters.Add(New SqlParameter("@SERIALIZADO", oBeProducto.Serializado))
            cmd.Parameters.Add(New SqlParameter("@GENERA_LOTE", oBeProducto.Genera_lote))
            cmd.Parameters.Add(New SqlParameter("@GENERA_LP_OLD", oBeProducto.Genera_lp))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_VENCIMIENTO", oBeProducto.Control_vencimiento))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_LOTE", oBeProducto.Control_lote))
            cmd.Parameters.Add(New SqlParameter("@PESO_RECEPCION", oBeProducto.Peso_recepcion))
            cmd.Parameters.Add(New SqlParameter("@PESO_DESPACHO", oBeProducto.Peso_despacho))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_RECEPCION", oBeProducto.Temperatura_recepcion))
            cmd.Parameters.Add(New SqlParameter("@TEMPERATURA_DESPACHO", oBeProducto.Temperatura_despacho))
            cmd.Parameters.Add(New SqlParameter("@MATERIA_PRIMA", oBeProducto.Materia_prima))
            cmd.Parameters.Add(New SqlParameter("@KIT", oBeProducto.Kit))
            cmd.Parameters.Add(New SqlParameter("@TOLERANCIA", oBeProducto.Tolerancia))
            cmd.Parameters.Add(New SqlParameter("@CICLO_VIDA", oBeProducto.Ciclo_vida))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto.Fec_mod))
            If Not oBeProducto.Imagen Is Nothing Then cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeProducto.Imagen))
            cmd.Parameters.Add(New SqlParameter("@NOSERIE", oBeProducto.Noserie))
            cmd.Parameters.Add(New SqlParameter("@NOPARTE", oBeProducto.Noparte))
            cmd.Parameters.Add(New SqlParameter("@FECHAMANUFACTURA", oBeProducto.Fechamanufactura))
            cmd.Parameters.Add(New SqlParameter("@CAPTURAR_ANIADA", oBeProducto.Capturar_aniada))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_PESO", oBeProducto.Control_peso))
            cmd.Parameters.Add(New SqlParameter("@CAPTURA_ARANCEL", oBeProducto.Captura_arancel))
            cmd.Parameters.Add(New SqlParameter("@ES_HARDWARE", oBeProducto.Es_hardware))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeProducto.Largo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeProducto.Alto))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeProducto.Ancho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IMPRESION", oBeProducto.Margen_Impresion))
            'If Not oBeProducto.IdUnidadMedidaCobro = 0 Then cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDACOBRO", oBeProducto.IdUnidadMedidaCobro))
            If oBeProducto.IdUnidadMedidaCobro = 0 Then
                cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDACOBRO", DBNull.Value))
            Else
                cmd.Parameters.Add(New SqlParameter("@IDUNIDADMEDIDACOBRO", oBeProducto.IdUnidadMedidaCobro))
            End If
            cmd.Parameters.Add(New SqlParameter("@IDTIPOETIQUETA", oBeProducto.IdTipoEtiqueta))
            cmd.Parameters.Add(New SqlParameter("@DIAS_INVENTARIO_PROMEDIO", oBeProducto.Dias_Inventario_Promedio))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETROA", oBeProducto.IdProductoParametroA))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTOPARAMETROB", oBeProducto.IdProductoParametroB))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOMANUFACTURA", oBeProducto.IdTipoManufactura))

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
            If lConnection IsNot Nothing Then lConnection.Dispose()

        End Try

    End Function

    Public Shared Function Eliminar(ByRef oBeProducto As clsBeProducto,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Producto" &
             "  Where(IdProducto = @IdProducto)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto.IdProducto))


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

    Public Shared Function Obtener(ByRef oBeProducto As clsBeProducto) As Boolean

        Obtener = False

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Const sp As String = " SELECT * FROM Producto" &
                                 " Where(IdProducto = @IdProducto)"


                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto.IdProducto))

                    Dim dt As New DataTable
                    dad.Fill(dt)

                    If dt.Rows.Count = 1 Then
                        oBeProducto = New clsBeProducto()
                        Cargar(oBeProducto, dt.Rows(0), lConnection, lTransaction)
                        Obtener = True
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

    Public Shared Function GetAll_Tiene_Cliclo_Vida() As List(Of clsBeProducto)

        Try

            Dim lReturnList As New List(Of clsBeProducto)

            Dim sp As String = "SELECT * FROM Producto where ciclo_vida > 0 "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            lConnection.Open()

            Dim lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Transaction = lTransaction

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeProducto As New clsBeProducto

            For Each dr As DataRow In dt.Rows

                vBeProducto = New clsBeProducto
                Cargar(vBeProducto, dr, lConnection, lTransaction)
                lReturnList.Add(vBeProducto)

            Next

            lTransaction.Commit()

            lConnection.Dispose()

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeProducto As clsBeProducto)

        GetSingle = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            Const sp As String = "SELECT * FROM Producto
                                  Where(IdProducto = @IdProducto)"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRODUCTO", pBeProducto.IdProducto))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                pBeProducto = New clsBeProducto()
                Cargar(pBeProducto, dt.Rows(0), lConnection, lTransaction)
                GetSingle = True
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    ''' <summary>
    ''' #EJC2020502: Actualizar el control peso de un producto.
    ''' </summary>
    ''' <param name="oBeProducto"></param>
    ''' <param name="pConection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Actualizar_Control_Peso(ByRef oBeProducto As clsBeProducto,
                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("producto")
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("control_peso", "@control_peso", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdProducto = @IdProducto")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_PESO", oBeProducto.Control_peso))

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
            If lConnection IsNot Nothing Then lConnection.Dispose()

        End Try

    End Function

    Public Shared Function Actualizar_CodigoBarra_By_IdProducto(ByRef oBeProducto As clsBeProducto,
                                                                Optional ByVal pConection As SqlConnection = Nothing,
                                                                Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Actualizar_CodigoBarra_By_IdProducto = 0

        Try

            Upd.Init("producto")
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdProducto = @IdProducto")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeProducto.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto.Fec_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Actualizar_CodigoBarra_By_IdProducto = rowsAffected

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
    ''' #EJC20240512: Actualizar el control lote de un producto.
    ''' </summary>
    ''' <param name="oBeProducto"></param>
    ''' <param name="pConection"></param>
    ''' <param name="pTransaction"></param>
    ''' <returns></returns>
    Public Shared Function Actualizar_Control_Vencimiento(ByRef oBeProducto As clsBeProducto,
                                                   Optional ByVal pConection As SqlConnection = Nothing,
                                                   Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("producto")
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("control_vencimiento", "@control_vencimiento", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("IdProducto = @IdProducto")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_VENCIMIENTO", oBeProducto.Control_vencimiento))

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
            If lConnection IsNot Nothing Then lConnection.Dispose()

        End Try

    End Function

    Public Shared Function Actualizar_Indice_Rotacion(ByVal CodigoProducto As String,
                                                      ByVal IdIndiceRotacion As Integer,
                                                      ByVal IdUserMod As Integer,
                                                      Optional ByVal pConection As SqlConnection = Nothing,
                                                      Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("producto")
            Upd.Add("IdIndiceRotacion", "@IdIndiceRotacion", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Where("codigo = @codigo")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@CODIGO", CodigoProducto))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", IdUserMod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", Now))
            cmd.Parameters.Add(New SqlParameter("@IdIndiceRotacion", IdIndiceRotacion))

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
            If lConnection IsNot Nothing Then lConnection.Dispose()

        End Try

    End Function

End Class