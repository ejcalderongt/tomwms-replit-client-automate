Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnP_CLIENTE

    Public Shared Sub Cargar(ByRef oBeP_CLIENTE As clsBeP_CLIENTE, ByRef dr As DataRow)
        Try
            With oBeP_CLIENTE
                .CODIGO = IIf(IsDBNull(dr.Item("CODIGO")), "", dr.Item("CODIGO"))
                .NOMBRE = IIf(IsDBNull(dr.Item("NOMBRE")), "", dr.Item("NOMBRE"))
                .BLOQUEADO = IIf(IsDBNull(dr.Item("BLOQUEADO")), "", dr.Item("BLOQUEADO"))
                .TIPONEG = IIf(IsDBNull(dr.Item("TIPONEG")), "", dr.Item("TIPONEG"))
                .TIPO = IIf(IsDBNull(dr.Item("TIPO")), "", dr.Item("TIPO"))
                .SUBTIPO = IIf(IsDBNull(dr.Item("SUBTIPO")), "", dr.Item("SUBTIPO"))
                .CANAL = IIf(IsDBNull(dr.Item("CANAL")), "", dr.Item("CANAL"))
                .SUBCANAL = IIf(IsDBNull(dr.Item("SUBCANAL")), "", dr.Item("SUBCANAL"))
                .NIVELPRECIO = IIf(IsDBNull(dr.Item("NIVELPRECIO")), 0, dr.Item("NIVELPRECIO"))
                .MEDIAPAGO = IIf(IsDBNull(dr.Item("MEDIAPAGO")), "", dr.Item("MEDIAPAGO"))
                .LIMITECREDITO = IIf(IsDBNull(dr.Item("LIMITECREDITO")), 0.0, dr.Item("LIMITECREDITO"))
                .DIACREDITO = IIf(IsDBNull(dr.Item("DIACREDITO")), 0, dr.Item("DIACREDITO"))
                .DESCUENTO = IIf(IsDBNull(dr.Item("DESCUENTO")), "", dr.Item("DESCUENTO"))
                .BONIFICACION = IIf(IsDBNull(dr.Item("BONIFICACION")), "", dr.Item("BONIFICACION"))
                .ULTVISITA = IIf(IsDBNull(dr.Item("ULTVISITA")), Date.Now, dr.Item("ULTVISITA"))
                .IMPSPEC = IIf(IsDBNull(dr.Item("IMPSPEC")), 0.0, dr.Item("IMPSPEC"))
                .INVTIPO = IIf(IsDBNull(dr.Item("INVTIPO")), "", dr.Item("INVTIPO"))
                .INVEQUIPO = IIf(IsDBNull(dr.Item("INVEQUIPO")), "", dr.Item("INVEQUIPO"))
                .INV1 = IIf(IsDBNull(dr.Item("INV1")), "", dr.Item("INV1"))
                .INV2 = IIf(IsDBNull(dr.Item("INV2")), "", dr.Item("INV2"))
                .INV3 = IIf(IsDBNull(dr.Item("INV3")), "", dr.Item("INV3"))
                .NIT = IIf(IsDBNull(dr.Item("NIT")), "", dr.Item("NIT"))
                .MENSAJE = IIf(IsDBNull(dr.Item("MENSAJE")), "", dr.Item("MENSAJE"))
                .EMAIL = IIf(IsDBNull(dr.Item("EMAIL")), "", dr.Item("EMAIL"))
                .ESERVICE = IIf(IsDBNull(dr.Item("ESERVICE")), "", dr.Item("ESERVICE"))
                .TELEFONO = IIf(IsDBNull(dr.Item("TELEFONO")), "", dr.Item("TELEFONO"))
                .DIRTIPO = IIf(IsDBNull(dr.Item("DIRTIPO")), "", dr.Item("DIRTIPO"))
                .DIRECCION = IIf(IsDBNull(dr.Item("DIRECCION")), "", dr.Item("DIRECCION"))
                .REGION = IIf(IsDBNull(dr.Item("REGION")), "", dr.Item("REGION"))
                .SUCURSAL = IIf(IsDBNull(dr.Item("SUCURSAL")), "", dr.Item("SUCURSAL"))
                .MUNICIPIO = IIf(IsDBNull(dr.Item("MUNICIPIO")), "", dr.Item("MUNICIPIO"))
                .CIUDAD = IIf(IsDBNull(dr.Item("CIUDAD")), "", dr.Item("CIUDAD"))
                .ZONA = IIf(IsDBNull(dr.Item("ZONA")), 0, dr.Item("ZONA"))
                .COLONIA = IIf(IsDBNull(dr.Item("COLONIA")), "", dr.Item("COLONIA"))
                .AVENIDA = IIf(IsDBNull(dr.Item("AVENIDA")), "", dr.Item("AVENIDA"))
                .CALLE = IIf(IsDBNull(dr.Item("CALLE")), "", dr.Item("CALLE"))
                .NUMERO = IIf(IsDBNull(dr.Item("NUMERO")), "", dr.Item("NUMERO"))
                .CARTOGRAFICO = IIf(IsDBNull(dr.Item("CARTOGRAFICO")), "", dr.Item("CARTOGRAFICO"))
                .COORX = IIf(IsDBNull(dr.Item("COORX")), 0.0, dr.Item("COORX"))
                .COORY = IIf(IsDBNull(dr.Item("COORY")), 0.0, dr.Item("COORY"))
                .BODEGA = IIf(IsDBNull(dr.Item("BODEGA")), "", dr.Item("BODEGA"))
                .COD_PAIS = IIf(IsDBNull(dr.Item("COD_PAIS")), "", dr.Item("COD_PAIS"))
                .FIRMADIG = IIf(IsDBNull(dr.Item("FIRMADIG")), "", dr.Item("FIRMADIG"))
                .CODBARRA = IIf(IsDBNull(dr.Item("CODBARRA")), "", dr.Item("CODBARRA"))
                .VALIDACREDITO = IIf(IsDBNull(dr.Item("VALIDACREDITO")), "", dr.Item("VALIDACREDITO"))
                .FACT_VS_FACT = IIf(IsDBNull(dr.Item("FACT_VS_FACT")), "", dr.Item("FACT_VS_FACT"))
                .CHEQUEPOST = IIf(IsDBNull(dr.Item("CHEQUEPOST")), "", dr.Item("CHEQUEPOST"))
                .PRECIO_ESTRATEGICO = IIf(IsDBNull(dr.Item("PRECIO_ESTRATEGICO")), "", dr.Item("PRECIO_ESTRATEGICO"))
                .NOMBRE_PROPIETARIO = IIf(IsDBNull(dr.Item("NOMBRE_PROPIETARIO")), "", dr.Item("NOMBRE_PROPIETARIO"))
                .NOMBRE_REPRESENTANTE = IIf(IsDBNull(dr.Item("NOMBRE_REPRESENTANTE")), "", dr.Item("NOMBRE_REPRESENTANTE"))
                .PERCEPCION = IIf(IsDBNull(dr.Item("PERCEPCION")), 0.0, dr.Item("PERCEPCION"))
                .TIPO_CONTRIBUYENTE = IIf(IsDBNull(dr.Item("TIPO_CONTRIBUYENTE")), "", dr.Item("TIPO_CONTRIBUYENTE"))
                .ID_DESPACHO = IIf(IsDBNull(dr.Item("ID_DESPACHO")), 0, dr.Item("ID_DESPACHO"))
                .ID_FACTURACION = IIf(IsDBNull(dr.Item("ID_FACTURACION")), 0, dr.Item("ID_FACTURACION"))
                .MODIF_PRECIO = IIf(IsDBNull(dr.Item("MODIF_PRECIO")), False, dr.Item("MODIF_PRECIO"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeP_CLIENTE As clsBeP_CLIENTE, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("p_cliente")
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("bloqueado", "@bloqueado", DataType.Parametro)
            Ins.Add("tiponeg", "@tiponeg", DataType.Parametro)
            Ins.Add("tipo", "@tipo", DataType.Parametro)
            Ins.Add("subtipo", "@subtipo", DataType.Parametro)
            Ins.Add("canal", "@canal", DataType.Parametro)
            Ins.Add("subcanal", "@subcanal", DataType.Parametro)
            Ins.Add("nivelprecio", "@nivelprecio", DataType.Parametro)
            Ins.Add("mediapago", "@mediapago", DataType.Parametro)
            Ins.Add("limitecredito", "@limitecredito", DataType.Parametro)
            Ins.Add("diacredito", "@diacredito", DataType.Parametro)
            Ins.Add("descuento", "@descuento", DataType.Parametro)
            Ins.Add("bonificacion", "@bonificacion", DataType.Parametro)
            Ins.Add("ultvisita", "@ultvisita", DataType.Parametro)
            Ins.Add("impspec", "@impspec", DataType.Parametro)
            Ins.Add("invtipo", "@invtipo", DataType.Parametro)
            Ins.Add("invequipo", "@invequipo", DataType.Parametro)
            Ins.Add("inv1", "@inv1", DataType.Parametro)
            Ins.Add("inv2", "@inv2", DataType.Parametro)
            Ins.Add("inv3", "@inv3", DataType.Parametro)
            Ins.Add("nit", "@nit", DataType.Parametro)
            Ins.Add("mensaje", "@mensaje", DataType.Parametro)
            Ins.Add("email", "@email", DataType.Parametro)
            Ins.Add("eservice", "@eservice", DataType.Parametro)
            Ins.Add("telefono", "@telefono", DataType.Parametro)
            Ins.Add("dirtipo", "@dirtipo", DataType.Parametro)
            Ins.Add("direccion", "@direccion", DataType.Parametro)
            Ins.Add("region", "@region", DataType.Parametro)
            Ins.Add("sucursal", "@sucursal", DataType.Parametro)
            Ins.Add("municipio", "@municipio", DataType.Parametro)
            Ins.Add("ciudad", "@ciudad", DataType.Parametro)
            Ins.Add("zona", "@zona", DataType.Parametro)
            Ins.Add("colonia", "@colonia", DataType.Parametro)
            Ins.Add("avenida", "@avenida", DataType.Parametro)
            Ins.Add("calle", "@calle", DataType.Parametro)
            Ins.Add("numero", "@numero", DataType.Parametro)
            Ins.Add("cartografico", "@cartografico", DataType.Parametro)
            Ins.Add("coorx", "@coorx", DataType.Parametro)
            Ins.Add("coory", "@coory", DataType.Parametro)
            Ins.Add("bodega", "@bodega", DataType.Parametro)
            Ins.Add("cod_pais", "@cod_pais", DataType.Parametro)
            Ins.Add("firmadig", "@firmadig", DataType.Parametro)
            Ins.Add("codbarra", "@codbarra", DataType.Parametro)
            Ins.Add("validacredito", "@validacredito", DataType.Parametro)
            Ins.Add("fact_vs_fact", "@fact_vs_fact", DataType.Parametro)
            Ins.Add("chequepost", "@chequepost", DataType.Parametro)
            Ins.Add("precio_estrategico", "@precio_estrategico", DataType.Parametro)
            Ins.Add("nombre_propietario", "@nombre_propietario", DataType.Parametro)
            Ins.Add("nombre_representante", "@nombre_representante", DataType.Parametro)
            Ins.Add("percepcion", "@percepcion", DataType.Parametro)
            Ins.Add("tipo_contribuyente", "@tipo_contribuyente", DataType.Parametro)
            Ins.Add("id_despacho", "@id_despacho", DataType.Parametro)
            Ins.Add("id_facturacion", "@id_facturacion", DataType.Parametro)
            Ins.Add("modif_precio", "@modif_precio", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeP_CLIENTE.CODIGO))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeP_CLIENTE.NOMBRE))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEADO", oBeP_CLIENTE.BLOQUEADO))
            cmd.Parameters.Add(New SqlParameter("@TIPONEG", oBeP_CLIENTE.TIPONEG))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeP_CLIENTE.TIPO))
            cmd.Parameters.Add(New SqlParameter("@SUBTIPO", oBeP_CLIENTE.SUBTIPO))
            cmd.Parameters.Add(New SqlParameter("@CANAL", oBeP_CLIENTE.CANAL))
            cmd.Parameters.Add(New SqlParameter("@SUBCANAL", oBeP_CLIENTE.SUBCANAL))
            cmd.Parameters.Add(New SqlParameter("@NIVELPRECIO", oBeP_CLIENTE.NIVELPRECIO))
            cmd.Parameters.Add(New SqlParameter("@MEDIAPAGO", oBeP_CLIENTE.MEDIAPAGO))
            cmd.Parameters.Add(New SqlParameter("@LIMITECREDITO", oBeP_CLIENTE.LIMITECREDITO))
            cmd.Parameters.Add(New SqlParameter("@DIACREDITO", oBeP_CLIENTE.DIACREDITO))
            cmd.Parameters.Add(New SqlParameter("@DESCUENTO", oBeP_CLIENTE.DESCUENTO))
            cmd.Parameters.Add(New SqlParameter("@BONIFICACION", oBeP_CLIENTE.BONIFICACION))
            cmd.Parameters.Add(New SqlParameter("@ULTVISITA", oBeP_CLIENTE.ULTVISITA))
            cmd.Parameters.Add(New SqlParameter("@IMPSPEC", oBeP_CLIENTE.IMPSPEC))
            cmd.Parameters.Add(New SqlParameter("@INVTIPO", oBeP_CLIENTE.INVTIPO))
            cmd.Parameters.Add(New SqlParameter("@INVEQUIPO", oBeP_CLIENTE.INVEQUIPO))
            cmd.Parameters.Add(New SqlParameter("@INV1", oBeP_CLIENTE.INV1))
            cmd.Parameters.Add(New SqlParameter("@INV2", oBeP_CLIENTE.INV2))
            cmd.Parameters.Add(New SqlParameter("@INV3", oBeP_CLIENTE.INV3))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeP_CLIENTE.NIT))
            cmd.Parameters.Add(New SqlParameter("@MENSAJE", oBeP_CLIENTE.MENSAJE))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBeP_CLIENTE.EMAIL))
            cmd.Parameters.Add(New SqlParameter("@ESERVICE", oBeP_CLIENTE.ESERVICE))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeP_CLIENTE.TELEFONO))
            cmd.Parameters.Add(New SqlParameter("@DIRTIPO", oBeP_CLIENTE.DIRTIPO))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeP_CLIENTE.DIRECCION))
            cmd.Parameters.Add(New SqlParameter("@REGION", oBeP_CLIENTE.REGION))
            cmd.Parameters.Add(New SqlParameter("@SUCURSAL", oBeP_CLIENTE.SUCURSAL))
            cmd.Parameters.Add(New SqlParameter("@MUNICIPIO", oBeP_CLIENTE.MUNICIPIO))
            cmd.Parameters.Add(New SqlParameter("@CIUDAD", oBeP_CLIENTE.CIUDAD))
            cmd.Parameters.Add(New SqlParameter("@ZONA", oBeP_CLIENTE.ZONA))
            cmd.Parameters.Add(New SqlParameter("@COLONIA", oBeP_CLIENTE.COLONIA))
            cmd.Parameters.Add(New SqlParameter("@AVENIDA", oBeP_CLIENTE.AVENIDA))
            cmd.Parameters.Add(New SqlParameter("@CALLE", oBeP_CLIENTE.CALLE))
            cmd.Parameters.Add(New SqlParameter("@NUMERO", oBeP_CLIENTE.NUMERO))
            cmd.Parameters.Add(New SqlParameter("@CARTOGRAFICO", oBeP_CLIENTE.CARTOGRAFICO))
            cmd.Parameters.Add(New SqlParameter("@COORX", oBeP_CLIENTE.COORX))
            cmd.Parameters.Add(New SqlParameter("@COORY", oBeP_CLIENTE.COORY))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeP_CLIENTE.BODEGA))
            cmd.Parameters.Add(New SqlParameter("@COD_PAIS", oBeP_CLIENTE.COD_PAIS))
            cmd.Parameters.Add(New SqlParameter("@FIRMADIG", oBeP_CLIENTE.FIRMADIG))
            cmd.Parameters.Add(New SqlParameter("@CODBARRA", oBeP_CLIENTE.CODBARRA))
            cmd.Parameters.Add(New SqlParameter("@VALIDACREDITO", oBeP_CLIENTE.VALIDACREDITO))
            cmd.Parameters.Add(New SqlParameter("@FACT_VS_FACT", oBeP_CLIENTE.FACT_VS_FACT))
            cmd.Parameters.Add(New SqlParameter("@CHEQUEPOST", oBeP_CLIENTE.CHEQUEPOST))
            cmd.Parameters.Add(New SqlParameter("@PRECIO_ESTRATEGICO", oBeP_CLIENTE.PRECIO_ESTRATEGICO))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PROPIETARIO", oBeP_CLIENTE.NOMBRE_PROPIETARIO))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_REPRESENTANTE", oBeP_CLIENTE.NOMBRE_REPRESENTANTE))
            cmd.Parameters.Add(New SqlParameter("@PERCEPCION", oBeP_CLIENTE.PERCEPCION))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CONTRIBUYENTE", oBeP_CLIENTE.TIPO_CONTRIBUYENTE))
            cmd.Parameters.Add(New SqlParameter("@ID_DESPACHO", oBeP_CLIENTE.ID_DESPACHO))
            cmd.Parameters.Add(New SqlParameter("@ID_FACTURACION", oBeP_CLIENTE.ID_FACTURACION))
            cmd.Parameters.Add(New SqlParameter("@MODIF_PRECIO", oBeP_CLIENTE.MODIF_PRECIO))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeP_CLIENTE As clsBeP_CLIENTE, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("p_cliente")
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("bloqueado", "@bloqueado", DataType.Parametro)
            Upd.Add("tiponeg", "@tiponeg", DataType.Parametro)
            Upd.Add("tipo", "@tipo", DataType.Parametro)
            Upd.Add("subtipo", "@subtipo", DataType.Parametro)
            Upd.Add("canal", "@canal", DataType.Parametro)
            Upd.Add("subcanal", "@subcanal", DataType.Parametro)
            Upd.Add("nivelprecio", "@nivelprecio", DataType.Parametro)
            Upd.Add("mediapago", "@mediapago", DataType.Parametro)
            Upd.Add("limitecredito", "@limitecredito", DataType.Parametro)
            Upd.Add("diacredito", "@diacredito", DataType.Parametro)
            Upd.Add("descuento", "@descuento", DataType.Parametro)
            Upd.Add("bonificacion", "@bonificacion", DataType.Parametro)
            Upd.Add("ultvisita", "@ultvisita", DataType.Parametro)
            Upd.Add("impspec", "@impspec", DataType.Parametro)
            Upd.Add("invtipo", "@invtipo", DataType.Parametro)
            Upd.Add("invequipo", "@invequipo", DataType.Parametro)
            Upd.Add("inv1", "@inv1", DataType.Parametro)
            Upd.Add("inv2", "@inv2", DataType.Parametro)
            Upd.Add("inv3", "@inv3", DataType.Parametro)
            Upd.Add("nit", "@nit", DataType.Parametro)
            Upd.Add("mensaje", "@mensaje", DataType.Parametro)
            Upd.Add("email", "@email", DataType.Parametro)
            Upd.Add("eservice", "@eservice", DataType.Parametro)
            Upd.Add("telefono", "@telefono", DataType.Parametro)
            Upd.Add("dirtipo", "@dirtipo", DataType.Parametro)
            Upd.Add("direccion", "@direccion", DataType.Parametro)
            Upd.Add("region", "@region", DataType.Parametro)
            Upd.Add("sucursal", "@sucursal", DataType.Parametro)
            Upd.Add("municipio", "@municipio", DataType.Parametro)
            Upd.Add("ciudad", "@ciudad", DataType.Parametro)
            Upd.Add("zona", "@zona", DataType.Parametro)
            Upd.Add("colonia", "@colonia", DataType.Parametro)
            Upd.Add("avenida", "@avenida", DataType.Parametro)
            Upd.Add("calle", "@calle", DataType.Parametro)
            Upd.Add("numero", "@numero", DataType.Parametro)
            Upd.Add("cartografico", "@cartografico", DataType.Parametro)
            Upd.Add("coorx", "@coorx", DataType.Parametro)
            Upd.Add("coory", "@coory", DataType.Parametro)
            Upd.Add("bodega", "@bodega", DataType.Parametro)
            Upd.Add("cod_pais", "@cod_pais", DataType.Parametro)
            Upd.Add("firmadig", "@firmadig", DataType.Parametro)
            Upd.Add("codbarra", "@codbarra", DataType.Parametro)
            Upd.Add("validacredito", "@validacredito", DataType.Parametro)
            Upd.Add("fact_vs_fact", "@fact_vs_fact", DataType.Parametro)
            Upd.Add("chequepost", "@chequepost", DataType.Parametro)
            Upd.Add("precio_estrategico", "@precio_estrategico", DataType.Parametro)
            Upd.Add("nombre_propietario", "@nombre_propietario", DataType.Parametro)
            Upd.Add("nombre_representante", "@nombre_representante", DataType.Parametro)
            Upd.Add("percepcion", "@percepcion", DataType.Parametro)
            Upd.Add("tipo_contribuyente", "@tipo_contribuyente", DataType.Parametro)
            Upd.Add("id_despacho", "@id_despacho", DataType.Parametro)
            Upd.Add("id_facturacion", "@id_facturacion", DataType.Parametro)
            Upd.Add("modif_precio", "@modif_precio", DataType.Parametro)
            Upd.Where("CODIGO = @CODIGO")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeP_CLIENTE.CODIGO))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeP_CLIENTE.NOMBRE))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEADO", oBeP_CLIENTE.BLOQUEADO))
            cmd.Parameters.Add(New SqlParameter("@TIPONEG", oBeP_CLIENTE.TIPONEG))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeP_CLIENTE.TIPO))
            cmd.Parameters.Add(New SqlParameter("@SUBTIPO", oBeP_CLIENTE.SUBTIPO))
            cmd.Parameters.Add(New SqlParameter("@CANAL", oBeP_CLIENTE.CANAL))
            cmd.Parameters.Add(New SqlParameter("@SUBCANAL", oBeP_CLIENTE.SUBCANAL))
            cmd.Parameters.Add(New SqlParameter("@NIVELPRECIO", oBeP_CLIENTE.NIVELPRECIO))
            cmd.Parameters.Add(New SqlParameter("@MEDIAPAGO", oBeP_CLIENTE.MEDIAPAGO))
            cmd.Parameters.Add(New SqlParameter("@LIMITECREDITO", oBeP_CLIENTE.LIMITECREDITO))
            cmd.Parameters.Add(New SqlParameter("@DIACREDITO", oBeP_CLIENTE.DIACREDITO))
            cmd.Parameters.Add(New SqlParameter("@DESCUENTO", oBeP_CLIENTE.DESCUENTO))
            cmd.Parameters.Add(New SqlParameter("@BONIFICACION", oBeP_CLIENTE.BONIFICACION))
            cmd.Parameters.Add(New SqlParameter("@ULTVISITA", oBeP_CLIENTE.ULTVISITA))
            cmd.Parameters.Add(New SqlParameter("@IMPSPEC", oBeP_CLIENTE.IMPSPEC))
            cmd.Parameters.Add(New SqlParameter("@INVTIPO", oBeP_CLIENTE.INVTIPO))
            cmd.Parameters.Add(New SqlParameter("@INVEQUIPO", oBeP_CLIENTE.INVEQUIPO))
            cmd.Parameters.Add(New SqlParameter("@INV1", oBeP_CLIENTE.INV1))
            cmd.Parameters.Add(New SqlParameter("@INV2", oBeP_CLIENTE.INV2))
            cmd.Parameters.Add(New SqlParameter("@INV3", oBeP_CLIENTE.INV3))
            cmd.Parameters.Add(New SqlParameter("@NIT", oBeP_CLIENTE.NIT))
            cmd.Parameters.Add(New SqlParameter("@MENSAJE", oBeP_CLIENTE.MENSAJE))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBeP_CLIENTE.EMAIL))
            cmd.Parameters.Add(New SqlParameter("@ESERVICE", oBeP_CLIENTE.ESERVICE))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeP_CLIENTE.TELEFONO))
            cmd.Parameters.Add(New SqlParameter("@DIRTIPO", oBeP_CLIENTE.DIRTIPO))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeP_CLIENTE.DIRECCION))
            cmd.Parameters.Add(New SqlParameter("@REGION", oBeP_CLIENTE.REGION))
            cmd.Parameters.Add(New SqlParameter("@SUCURSAL", oBeP_CLIENTE.SUCURSAL))
            cmd.Parameters.Add(New SqlParameter("@MUNICIPIO", oBeP_CLIENTE.MUNICIPIO))
            cmd.Parameters.Add(New SqlParameter("@CIUDAD", oBeP_CLIENTE.CIUDAD))
            cmd.Parameters.Add(New SqlParameter("@ZONA", oBeP_CLIENTE.ZONA))
            cmd.Parameters.Add(New SqlParameter("@COLONIA", oBeP_CLIENTE.COLONIA))
            cmd.Parameters.Add(New SqlParameter("@AVENIDA", oBeP_CLIENTE.AVENIDA))
            cmd.Parameters.Add(New SqlParameter("@CALLE", oBeP_CLIENTE.CALLE))
            cmd.Parameters.Add(New SqlParameter("@NUMERO", oBeP_CLIENTE.NUMERO))
            cmd.Parameters.Add(New SqlParameter("@CARTOGRAFICO", oBeP_CLIENTE.CARTOGRAFICO))
            cmd.Parameters.Add(New SqlParameter("@COORX", oBeP_CLIENTE.COORX))
            cmd.Parameters.Add(New SqlParameter("@COORY", oBeP_CLIENTE.COORY))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeP_CLIENTE.BODEGA))
            cmd.Parameters.Add(New SqlParameter("@COD_PAIS", oBeP_CLIENTE.COD_PAIS))
            cmd.Parameters.Add(New SqlParameter("@FIRMADIG", oBeP_CLIENTE.FIRMADIG))
            cmd.Parameters.Add(New SqlParameter("@CODBARRA", oBeP_CLIENTE.CODBARRA))
            cmd.Parameters.Add(New SqlParameter("@VALIDACREDITO", oBeP_CLIENTE.VALIDACREDITO))
            cmd.Parameters.Add(New SqlParameter("@FACT_VS_FACT", oBeP_CLIENTE.FACT_VS_FACT))
            cmd.Parameters.Add(New SqlParameter("@CHEQUEPOST", oBeP_CLIENTE.CHEQUEPOST))
            cmd.Parameters.Add(New SqlParameter("@PRECIO_ESTRATEGICO", oBeP_CLIENTE.PRECIO_ESTRATEGICO))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_PROPIETARIO", oBeP_CLIENTE.NOMBRE_PROPIETARIO))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_REPRESENTANTE", oBeP_CLIENTE.NOMBRE_REPRESENTANTE))
            cmd.Parameters.Add(New SqlParameter("@PERCEPCION", oBeP_CLIENTE.PERCEPCION))
            cmd.Parameters.Add(New SqlParameter("@TIPO_CONTRIBUYENTE", oBeP_CLIENTE.TIPO_CONTRIBUYENTE))
            cmd.Parameters.Add(New SqlParameter("@ID_DESPACHO", oBeP_CLIENTE.ID_DESPACHO))
            cmd.Parameters.Add(New SqlParameter("@ID_FACTURACION", oBeP_CLIENTE.ID_FACTURACION))
            cmd.Parameters.Add(New SqlParameter("@MODIF_PRECIO", oBeP_CLIENTE.MODIF_PRECIO))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function Eliminar(ByRef oBeP_CLIENTE As clsBeP_CLIENTE, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from P_CLIENTE" &
             "  Where(CODIGO = @CODIGO)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeP_CLIENTE.CODIGO))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM P_CLIENTE"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction()
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeP_CLIENTE)

        Dim lReturnList As New List(Of clsBeP_CLIENTE)

        Try

            Const sp As String = "SELECT * FROM P_CLIENTE"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeP_CLIENTE As New clsBeP_CLIENTE

                        For Each dr As DataRow In lDataTable.Rows
                            vBeP_CLIENTE = New clsBeP_CLIENTE()
                            Cargar(vBeP_CLIENTE, dr)
                            lReturnList.Add(vBeP_CLIENTE)
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

    Public Shared Sub GetSingle(ByRef pBeP_CLIENTE As clsBeP_CLIENTE)

        Try

            Const sp As String = "SELECT * FROM P_CLIENTE" &
            " Where(CODIGO = @CODIGO)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeP_CLIENTE As New clsBeP_CLIENTE

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeP_CLIENTE, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_Single_By_Codigo(ByVal pCodigo As String) As clsBeP_CLIENTE

        Get_Single_By_Codigo = Nothing

        Try

            Const sp As String = "SELECT * FROM P_CLIENTE" &
            " Where(CODIGO = @CODIGO)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@CODIGO", pCodigo)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeP_CLIENTE As New clsBeP_CLIENTE

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeP_CLIENTE, lDataTable.Rows(0))
                            Return vBeP_CLIENTE
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

            Const sp As String = "SELECT ISNULL(Max(CODIGO),0) FROM P_CLIENTE"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
