Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnP_PRODUCTO

    Public Shared Sub Cargar(ByRef oBeP_PRODUCTO As clsBeP_PRODUCTO, ByRef dr As DataRow)
        Try
            With oBeP_PRODUCTO
                .CODIGO = IIf(IsDBNull(dr.Item("CODIGO")), "", dr.Item("CODIGO"))
                .TIPO = IIf(IsDBNull(dr.Item("TIPO")), "", dr.Item("TIPO"))
                .LINEA = IIf(IsDBNull(dr.Item("LINEA")), "", dr.Item("LINEA"))
                .SUBLINEA = IIf(IsDBNull(dr.Item("SUBLINEA")), "", dr.Item("SUBLINEA"))
                .EMPRESA = IIf(IsDBNull(dr.Item("EMPRESA")), "", dr.Item("EMPRESA"))
                .MARCA = IIf(IsDBNull(dr.Item("MARCA")), "", dr.Item("MARCA"))
                .CODBARRA = IIf(IsDBNull(dr.Item("CODBARRA")), "", dr.Item("CODBARRA"))
                .DESCCORTA = IIf(IsDBNull(dr.Item("DESCCORTA")), "", dr.Item("DESCCORTA"))
                .DESCLARGA = IIf(IsDBNull(dr.Item("DESCLARGA")), "", dr.Item("DESCLARGA"))
                .COSTO = IIf(IsDBNull(dr.Item("COSTO")), 0.0, dr.Item("COSTO"))
                .FACTORCONV = IIf(IsDBNull(dr.Item("FACTORCONV")), 0.0, dr.Item("FACTORCONV"))
                .UNIDBAS = IIf(IsDBNull(dr.Item("UNIDBAS")), "", dr.Item("UNIDBAS"))
                .UNIDMED = IIf(IsDBNull(dr.Item("UNIDMED")), "", dr.Item("UNIDMED"))
                .UNIMEDFACT = IIf(IsDBNull(dr.Item("UNIMEDFACT")), 0.0, dr.Item("UNIMEDFACT"))
                .UNIGRA = IIf(IsDBNull(dr.Item("UNIGRA")), "", dr.Item("UNIGRA"))
                .UNIGRAFACT = IIf(IsDBNull(dr.Item("UNIGRAFACT")), 0.0, dr.Item("UNIGRAFACT"))
                .DESCUENTO = IIf(IsDBNull(dr.Item("DESCUENTO")), "", dr.Item("DESCUENTO"))
                .BONIFICACION = IIf(IsDBNull(dr.Item("BONIFICACION")), "", dr.Item("BONIFICACION"))
                .IMP1 = IIf(IsDBNull(dr.Item("IMP1")), 0.0, dr.Item("IMP1"))
                .IMP2 = IIf(IsDBNull(dr.Item("IMP2")), 0.0, dr.Item("IMP2"))
                .IMP3 = IIf(IsDBNull(dr.Item("IMP3")), 0.0, dr.Item("IMP3"))
                .VENCOMP = IIf(IsDBNull(dr.Item("VENCOMP")), "", dr.Item("VENCOMP"))
                .DEVOL = IIf(IsDBNull(dr.Item("DEVOL")), "", dr.Item("DEVOL"))
                .OFRECER = IIf(IsDBNull(dr.Item("OFRECER")), "", dr.Item("OFRECER"))
                .RENTAB = IIf(IsDBNull(dr.Item("RENTAB")), "", dr.Item("RENTAB"))
                .DESCMAX = IIf(IsDBNull(dr.Item("DESCMAX")), "", dr.Item("DESCMAX"))
                .IVA = IIf(IsDBNull(dr.Item("IVA")), "", dr.Item("IVA"))
                .CODBARRA2 = IIf(IsDBNull(dr.Item("CODBARRA2")), "", dr.Item("CODBARRA2"))
                .CBCONV = IIf(IsDBNull(dr.Item("CBCONV")), 0, dr.Item("CBCONV"))
                .BODEGA = IIf(IsDBNull(dr.Item("BODEGA")), "", dr.Item("BODEGA"))
                .SUBBODEGA = IIf(IsDBNull(dr.Item("SUBBODEGA")), "", dr.Item("SUBBODEGA"))
                .PESO_PROMEDIO = IIf(IsDBNull(dr.Item("PESO_PROMEDIO")), 0.0, dr.Item("PESO_PROMEDIO"))
                .MODIF_PRECIO = IIf(IsDBNull(dr.Item("MODIF_PRECIO")), False, dr.Item("MODIF_PRECIO"))
                .IMAGEN = IIf(IsDBNull(dr.Item("IMAGEN")), "", dr.Item("IMAGEN"))
                .VIDEO = IIf(IsDBNull(dr.Item("VIDEO")), "", dr.Item("VIDEO"))
                .VENTA_POR_PESO = IIf(IsDBNull(dr.Item("VENTA_POR_PESO")), False, dr.Item("VENTA_POR_PESO"))
                .ES_PROD_BARRA = IIf(IsDBNull(dr.Item("ES_PROD_BARRA")), False, dr.Item("ES_PROD_BARRA"))
                .UNID_INV = IIf(IsDBNull(dr.Item("UNID_INV")), "", dr.Item("UNID_INV"))
                .VENTA_POR_PAQUETE = IIf(IsDBNull(dr.Item("VENTA_POR_PAQUETE")), False, dr.Item("VENTA_POR_PAQUETE"))
                .VENTA_POR_FACTOR_CONV = IIf(IsDBNull(dr.Item("VENTA_POR_FACTOR_CONV")), False, dr.Item("VENTA_POR_FACTOR_CONV"))
                .ES_SERIALIZADO = IIf(IsDBNull(dr.Item("ES_SERIALIZADO")), False, dr.Item("ES_SERIALIZADO"))
                .PARAM_CADUCIDAD = IIf(IsDBNull(dr.Item("PARAM_CADUCIDAD")), 0, dr.Item("PARAM_CADUCIDAD"))
                .PRODUCTO_PADRE = IIf(IsDBNull(dr.Item("PRODUCTO_PADRE")), "", dr.Item("PRODUCTO_PADRE"))
                .FACTOR_PADRE = IIf(IsDBNull(dr.Item("FACTOR_PADRE")), 0.0, dr.Item("FACTOR_PADRE"))
                .TIENE_INV = IIf(IsDBNull(dr.Item("TIENE_INV")), False, dr.Item("TIENE_INV"))
                .TIENE_VINETA_O_TUBO = IIf(IsDBNull(dr.Item("TIENE_VINETA_O_TUBO")), False, dr.Item("TIENE_VINETA_O_TUBO"))
                .PRECIO_VINETA_O_TUBO = IIf(IsDBNull(dr.Item("PRECIO_VINETA_O_TUBO")), 0.0, dr.Item("PRECIO_VINETA_O_TUBO"))
                .ES_VENDIBLE = IIf(IsDBNull(dr.Item("ES_VENDIBLE")), False, dr.Item("ES_VENDIBLE"))
                .UNIGRASAP = IIf(IsDBNull(dr.Item("UNIGRASAP")), 0.0, dr.Item("UNIGRASAP"))
                .UM_SALIDA = IIf(IsDBNull(dr.Item("UM_SALIDA")), "", dr.Item("UM_SALIDA"))
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    Public Shared Function Insertar(ByRef oBeP_PRODUCTO As clsBeP_PRODUCTO, Optional ByVal pConection as SqlConnection = Nothing, Optional Byval pTransaction as SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("p_producto")
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("tipo", "@tipo", DataType.Parametro)
            Ins.Add("linea", "@linea", DataType.Parametro)
            Ins.Add("sublinea", "@sublinea", DataType.Parametro)
            Ins.Add("empresa", "@empresa", DataType.Parametro)
            Ins.Add("marca", "@marca", DataType.Parametro)
            Ins.Add("codbarra", "@codbarra", DataType.Parametro)
            Ins.Add("desccorta", "@desccorta", DataType.Parametro)
            Ins.Add("desclarga", "@desclarga", DataType.Parametro)
            Ins.Add("costo", "@costo", DataType.Parametro)
            Ins.Add("factorconv", "@factorconv", DataType.Parametro)
            Ins.Add("unidbas", "@unidbas", DataType.Parametro)
            Ins.Add("unidmed", "@unidmed", DataType.Parametro)
            Ins.Add("unimedfact", "@unimedfact", DataType.Parametro)
            Ins.Add("unigra", "@unigra", DataType.Parametro)
            Ins.Add("unigrafact", "@unigrafact", DataType.Parametro)
            Ins.Add("descuento", "@descuento", DataType.Parametro)
            Ins.Add("bonificacion", "@bonificacion", DataType.Parametro)
            Ins.Add("imp1", "@imp1", DataType.Parametro)
            Ins.Add("imp2", "@imp2", DataType.Parametro)
            Ins.Add("imp3", "@imp3", DataType.Parametro)
            Ins.Add("vencomp", "@vencomp", DataType.Parametro)
            Ins.Add("devol", "@devol", DataType.Parametro)
            Ins.Add("ofrecer", "@ofrecer", DataType.Parametro)
            Ins.Add("rentab", "@rentab", DataType.Parametro)
            Ins.Add("descmax", "@descmax", DataType.Parametro)
            Ins.Add("iva", "@iva", DataType.Parametro)
            Ins.Add("codbarra2", "@codbarra2", DataType.Parametro)
            Ins.Add("cbconv", "@cbconv", DataType.Parametro)
            Ins.Add("bodega", "@bodega", DataType.Parametro)
            Ins.Add("subbodega", "@subbodega", DataType.Parametro)
            Ins.Add("peso_promedio", "@peso_promedio", DataType.Parametro)
            Ins.Add("modif_precio", "@modif_precio", DataType.Parametro)
            Ins.Add("imagen", "@imagen", DataType.Parametro)
            Ins.Add("video", "@video", DataType.Parametro)
            Ins.Add("venta_por_peso", "@venta_por_peso", DataType.Parametro)
            Ins.Add("es_prod_barra", "@es_prod_barra", DataType.Parametro)
            Ins.Add("unid_inv", "@unid_inv", DataType.Parametro)
            Ins.Add("venta_por_paquete", "@venta_por_paquete", DataType.Parametro)
            Ins.Add("venta_por_factor_conv", "@venta_por_factor_conv", DataType.Parametro)
            Ins.Add("es_serializado", "@es_serializado", DataType.Parametro)
            Ins.Add("param_caducidad", "@param_caducidad", DataType.Parametro)
            Ins.Add("producto_padre", "@producto_padre", DataType.Parametro)
            Ins.Add("factor_padre", "@factor_padre", DataType.Parametro)
            Ins.Add("tiene_inv", "@tiene_inv", DataType.Parametro)
            Ins.Add("tiene_vineta_o_tubo", "@tiene_vineta_o_tubo", DataType.Parametro)
            Ins.Add("precio_vineta_o_tubo", "@precio_vineta_o_tubo", DataType.Parametro)
            Ins.Add("es_vendible", "@es_vendible", DataType.Parametro)
            Ins.Add("unigrasap", "@unigrasap", DataType.Parametro)
            Ins.Add("um_salida", "@um_salida", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeP_PRODUCTO.CODIGO))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeP_PRODUCTO.TIPO))
            cmd.Parameters.Add(New SqlParameter("@LINEA", oBeP_PRODUCTO.LINEA))
            cmd.Parameters.Add(New SqlParameter("@SUBLINEA", oBeP_PRODUCTO.SUBLINEA))
            cmd.Parameters.Add(New SqlParameter("@EMPRESA", oBeP_PRODUCTO.EMPRESA))
            cmd.Parameters.Add(New SqlParameter("@MARCA", oBeP_PRODUCTO.MARCA))
            cmd.Parameters.Add(New SqlParameter("@CODBARRA", oBeP_PRODUCTO.CODBARRA))
            cmd.Parameters.Add(New SqlParameter("@DESCCORTA", oBeP_PRODUCTO.DESCCORTA))
            cmd.Parameters.Add(New SqlParameter("@DESCLARGA", oBeP_PRODUCTO.DESCLARGA))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeP_PRODUCTO.COSTO))
            cmd.Parameters.Add(New SqlParameter("@FACTORCONV", oBeP_PRODUCTO.FACTORCONV))
            cmd.Parameters.Add(New SqlParameter("@UNIDBAS", oBeP_PRODUCTO.UNIDBAS))
            cmd.Parameters.Add(New SqlParameter("@UNIDMED", oBeP_PRODUCTO.UNIDMED))
            cmd.Parameters.Add(New SqlParameter("@UNIMEDFACT", oBeP_PRODUCTO.UNIMEDFACT))
            cmd.Parameters.Add(New SqlParameter("@UNIGRA", oBeP_PRODUCTO.UNIGRA))
            cmd.Parameters.Add(New SqlParameter("@UNIGRAFACT", oBeP_PRODUCTO.UNIGRAFACT))
            cmd.Parameters.Add(New SqlParameter("@DESCUENTO", oBeP_PRODUCTO.DESCUENTO))
            cmd.Parameters.Add(New SqlParameter("@BONIFICACION", oBeP_PRODUCTO.BONIFICACION))
            cmd.Parameters.Add(New SqlParameter("@IMP1", oBeP_PRODUCTO.IMP1))
            cmd.Parameters.Add(New SqlParameter("@IMP2", oBeP_PRODUCTO.IMP2))
            cmd.Parameters.Add(New SqlParameter("@IMP3", oBeP_PRODUCTO.IMP3))
            cmd.Parameters.Add(New SqlParameter("@VENCOMP", oBeP_PRODUCTO.VENCOMP))
            cmd.Parameters.Add(New SqlParameter("@DEVOL", oBeP_PRODUCTO.DEVOL))
            cmd.Parameters.Add(New SqlParameter("@OFRECER", oBeP_PRODUCTO.OFRECER))
            cmd.Parameters.Add(New SqlParameter("@RENTAB", oBeP_PRODUCTO.RENTAB))
            cmd.Parameters.Add(New SqlParameter("@DESCMAX", oBeP_PRODUCTO.DESCMAX))
            cmd.Parameters.Add(New SqlParameter("@IVA", oBeP_PRODUCTO.IVA))
            cmd.Parameters.Add(New SqlParameter("@CODBARRA2", oBeP_PRODUCTO.CODBARRA2))
            cmd.Parameters.Add(New SqlParameter("@CBCONV", oBeP_PRODUCTO.CBCONV))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeP_PRODUCTO.BODEGA))
            cmd.Parameters.Add(New SqlParameter("@SUBBODEGA", oBeP_PRODUCTO.SUBBODEGA))
            cmd.Parameters.Add(New SqlParameter("@PESO_PROMEDIO", oBeP_PRODUCTO.PESO_PROMEDIO))
            cmd.Parameters.Add(New SqlParameter("@MODIF_PRECIO", oBeP_PRODUCTO.MODIF_PRECIO))
            cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeP_PRODUCTO.IMAGEN))
            cmd.Parameters.Add(New SqlParameter("@VIDEO", oBeP_PRODUCTO.VIDEO))
            cmd.Parameters.Add(New SqlParameter("@VENTA_POR_PESO", oBeP_PRODUCTO.VENTA_POR_PESO))
            cmd.Parameters.Add(New SqlParameter("@ES_PROD_BARRA", oBeP_PRODUCTO.ES_PROD_BARRA))
            cmd.Parameters.Add(New SqlParameter("@UNID_INV", oBeP_PRODUCTO.UNID_INV))
            cmd.Parameters.Add(New SqlParameter("@VENTA_POR_PAQUETE", oBeP_PRODUCTO.VENTA_POR_PAQUETE))
            cmd.Parameters.Add(New SqlParameter("@VENTA_POR_FACTOR_CONV", oBeP_PRODUCTO.VENTA_POR_FACTOR_CONV))
            cmd.Parameters.Add(New SqlParameter("@ES_SERIALIZADO", oBeP_PRODUCTO.ES_SERIALIZADO))
            cmd.Parameters.Add(New SqlParameter("@PARAM_CADUCIDAD", oBeP_PRODUCTO.PARAM_CADUCIDAD))
            cmd.Parameters.Add(New SqlParameter("@PRODUCTO_PADRE", oBeP_PRODUCTO.PRODUCTO_PADRE))
            cmd.Parameters.Add(New SqlParameter("@FACTOR_PADRE", oBeP_PRODUCTO.FACTOR_PADRE))
            cmd.Parameters.Add(New SqlParameter("@TIENE_INV", oBeP_PRODUCTO.TIENE_INV))
            cmd.Parameters.Add(New SqlParameter("@TIENE_VINETA_O_TUBO", oBeP_PRODUCTO.TIENE_VINETA_O_TUBO))
            cmd.Parameters.Add(New SqlParameter("@PRECIO_VINETA_O_TUBO", oBeP_PRODUCTO.PRECIO_VINETA_O_TUBO))
            cmd.Parameters.Add(New SqlParameter("@ES_VENDIBLE", oBeP_PRODUCTO.ES_VENDIBLE))
            cmd.Parameters.Add(New SqlParameter("@UNIGRASAP", oBeP_PRODUCTO.UNIGRASAP))
            cmd.Parameters.Add(New SqlParameter("@UM_SALIDA", oBeP_PRODUCTO.UM_SALIDA))

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

    Public Shared Function Actualizar(ByRef oBeP_PRODUCTO As clsBeP_PRODUCTO, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("p_producto")
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("tipo", "@tipo", DataType.Parametro)
            Upd.Add("linea", "@linea", DataType.Parametro)
            Upd.Add("sublinea", "@sublinea", DataType.Parametro)
            Upd.Add("empresa", "@empresa", DataType.Parametro)
            Upd.Add("marca", "@marca", DataType.Parametro)
            Upd.Add("codbarra", "@codbarra", DataType.Parametro)
            Upd.Add("desccorta", "@desccorta", DataType.Parametro)
            Upd.Add("desclarga", "@desclarga", DataType.Parametro)
            Upd.Add("costo", "@costo", DataType.Parametro)
            Upd.Add("factorconv", "@factorconv", DataType.Parametro)
            Upd.Add("unidbas", "@unidbas", DataType.Parametro)
            Upd.Add("unidmed", "@unidmed", DataType.Parametro)
            Upd.Add("unimedfact", "@unimedfact", DataType.Parametro)
            Upd.Add("unigra", "@unigra", DataType.Parametro)
            Upd.Add("unigrafact", "@unigrafact", DataType.Parametro)
            Upd.Add("descuento", "@descuento", DataType.Parametro)
            Upd.Add("bonificacion", "@bonificacion", DataType.Parametro)
            Upd.Add("imp1", "@imp1", DataType.Parametro)
            Upd.Add("imp2", "@imp2", DataType.Parametro)
            Upd.Add("imp3", "@imp3", DataType.Parametro)
            Upd.Add("vencomp", "@vencomp", DataType.Parametro)
            Upd.Add("devol", "@devol", DataType.Parametro)
            Upd.Add("ofrecer", "@ofrecer", DataType.Parametro)
            Upd.Add("rentab", "@rentab", DataType.Parametro)
            Upd.Add("descmax", "@descmax", DataType.Parametro)
            Upd.Add("iva", "@iva", DataType.Parametro)
            Upd.Add("codbarra2", "@codbarra2", DataType.Parametro)
            Upd.Add("cbconv", "@cbconv", DataType.Parametro)
            Upd.Add("bodega", "@bodega", DataType.Parametro)
            Upd.Add("subbodega", "@subbodega", DataType.Parametro)
            Upd.Add("peso_promedio", "@peso_promedio", DataType.Parametro)
            Upd.Add("modif_precio", "@modif_precio", DataType.Parametro)
            Upd.Add("imagen", "@imagen", DataType.Parametro)
            Upd.Add("video", "@video", DataType.Parametro)
            Upd.Add("venta_por_peso", "@venta_por_peso", DataType.Parametro)
            Upd.Add("es_prod_barra", "@es_prod_barra", DataType.Parametro)
            Upd.Add("unid_inv", "@unid_inv", DataType.Parametro)
            Upd.Add("venta_por_paquete", "@venta_por_paquete", DataType.Parametro)
            Upd.Add("venta_por_factor_conv", "@venta_por_factor_conv", DataType.Parametro)
            Upd.Add("es_serializado", "@es_serializado", DataType.Parametro)
            Upd.Add("param_caducidad", "@param_caducidad", DataType.Parametro)
            Upd.Add("producto_padre", "@producto_padre", DataType.Parametro)
            Upd.Add("factor_padre", "@factor_padre", DataType.Parametro)
            Upd.Add("tiene_inv", "@tiene_inv", DataType.Parametro)
            Upd.Add("tiene_vineta_o_tubo", "@tiene_vineta_o_tubo", DataType.Parametro)
            Upd.Add("precio_vineta_o_tubo", "@precio_vineta_o_tubo", DataType.Parametro)
            Upd.Add("es_vendible", "@es_vendible", DataType.Parametro)
            Upd.Add("unigrasap", "@unigrasap", DataType.Parametro)
            Upd.Add("um_salida", "@um_salida", DataType.Parametro)
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

            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeP_PRODUCTO.CODIGO))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeP_PRODUCTO.TIPO))
            cmd.Parameters.Add(New SqlParameter("@LINEA", oBeP_PRODUCTO.LINEA))
            cmd.Parameters.Add(New SqlParameter("@SUBLINEA", oBeP_PRODUCTO.SUBLINEA))
            cmd.Parameters.Add(New SqlParameter("@EMPRESA", oBeP_PRODUCTO.EMPRESA))
            cmd.Parameters.Add(New SqlParameter("@MARCA", oBeP_PRODUCTO.MARCA))
            cmd.Parameters.Add(New SqlParameter("@CODBARRA", oBeP_PRODUCTO.CODBARRA))
            cmd.Parameters.Add(New SqlParameter("@DESCCORTA", oBeP_PRODUCTO.DESCCORTA))
            cmd.Parameters.Add(New SqlParameter("@DESCLARGA", oBeP_PRODUCTO.DESCLARGA))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeP_PRODUCTO.COSTO))
            cmd.Parameters.Add(New SqlParameter("@FACTORCONV", oBeP_PRODUCTO.FACTORCONV))
            cmd.Parameters.Add(New SqlParameter("@UNIDBAS", oBeP_PRODUCTO.UNIDBAS))
            cmd.Parameters.Add(New SqlParameter("@UNIDMED", oBeP_PRODUCTO.UNIDMED))
            cmd.Parameters.Add(New SqlParameter("@UNIMEDFACT", oBeP_PRODUCTO.UNIMEDFACT))
            cmd.Parameters.Add(New SqlParameter("@UNIGRA", oBeP_PRODUCTO.UNIGRA))
            cmd.Parameters.Add(New SqlParameter("@UNIGRAFACT", oBeP_PRODUCTO.UNIGRAFACT))
            cmd.Parameters.Add(New SqlParameter("@DESCUENTO", oBeP_PRODUCTO.DESCUENTO))
            cmd.Parameters.Add(New SqlParameter("@BONIFICACION", oBeP_PRODUCTO.BONIFICACION))
            cmd.Parameters.Add(New SqlParameter("@IMP1", oBeP_PRODUCTO.IMP1))
            cmd.Parameters.Add(New SqlParameter("@IMP2", oBeP_PRODUCTO.IMP2))
            cmd.Parameters.Add(New SqlParameter("@IMP3", oBeP_PRODUCTO.IMP3))
            cmd.Parameters.Add(New SqlParameter("@VENCOMP", oBeP_PRODUCTO.VENCOMP))
            cmd.Parameters.Add(New SqlParameter("@DEVOL", oBeP_PRODUCTO.DEVOL))
            cmd.Parameters.Add(New SqlParameter("@OFRECER", oBeP_PRODUCTO.OFRECER))
            cmd.Parameters.Add(New SqlParameter("@RENTAB", oBeP_PRODUCTO.RENTAB))
            cmd.Parameters.Add(New SqlParameter("@DESCMAX", oBeP_PRODUCTO.DESCMAX))
            cmd.Parameters.Add(New SqlParameter("@IVA", oBeP_PRODUCTO.IVA))
            cmd.Parameters.Add(New SqlParameter("@CODBARRA2", oBeP_PRODUCTO.CODBARRA2))
            cmd.Parameters.Add(New SqlParameter("@CBCONV", oBeP_PRODUCTO.CBCONV))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeP_PRODUCTO.BODEGA))
            cmd.Parameters.Add(New SqlParameter("@SUBBODEGA", oBeP_PRODUCTO.SUBBODEGA))
            cmd.Parameters.Add(New SqlParameter("@PESO_PROMEDIO", oBeP_PRODUCTO.PESO_PROMEDIO))
            cmd.Parameters.Add(New SqlParameter("@MODIF_PRECIO", oBeP_PRODUCTO.MODIF_PRECIO))
            cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeP_PRODUCTO.IMAGEN))
            cmd.Parameters.Add(New SqlParameter("@VIDEO", oBeP_PRODUCTO.VIDEO))
            cmd.Parameters.Add(New SqlParameter("@VENTA_POR_PESO", oBeP_PRODUCTO.VENTA_POR_PESO))
            cmd.Parameters.Add(New SqlParameter("@ES_PROD_BARRA", oBeP_PRODUCTO.ES_PROD_BARRA))
            cmd.Parameters.Add(New SqlParameter("@UNID_INV", oBeP_PRODUCTO.UNID_INV))
            cmd.Parameters.Add(New SqlParameter("@VENTA_POR_PAQUETE", oBeP_PRODUCTO.VENTA_POR_PAQUETE))
            cmd.Parameters.Add(New SqlParameter("@VENTA_POR_FACTOR_CONV", oBeP_PRODUCTO.VENTA_POR_FACTOR_CONV))
            cmd.Parameters.Add(New SqlParameter("@ES_SERIALIZADO", oBeP_PRODUCTO.ES_SERIALIZADO))
            cmd.Parameters.Add(New SqlParameter("@PARAM_CADUCIDAD", oBeP_PRODUCTO.PARAM_CADUCIDAD))
            cmd.Parameters.Add(New SqlParameter("@PRODUCTO_PADRE", oBeP_PRODUCTO.PRODUCTO_PADRE))
            cmd.Parameters.Add(New SqlParameter("@FACTOR_PADRE", oBeP_PRODUCTO.FACTOR_PADRE))
            cmd.Parameters.Add(New SqlParameter("@TIENE_INV", oBeP_PRODUCTO.TIENE_INV))
            cmd.Parameters.Add(New SqlParameter("@TIENE_VINETA_O_TUBO", oBeP_PRODUCTO.TIENE_VINETA_O_TUBO))
            cmd.Parameters.Add(New SqlParameter("@PRECIO_VINETA_O_TUBO", oBeP_PRODUCTO.PRECIO_VINETA_O_TUBO))
            cmd.Parameters.Add(New SqlParameter("@ES_VENDIBLE", oBeP_PRODUCTO.ES_VENDIBLE))
            cmd.Parameters.Add(New SqlParameter("@UNIGRASAP", oBeP_PRODUCTO.UNIGRASAP))
            cmd.Parameters.Add(New SqlParameter("@UM_SALIDA", oBeP_PRODUCTO.UM_SALIDA))

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


    Public Shared Function Eliminar(ByRef oBeP_PRODUCTO As clsBeP_PRODUCTO, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from P_PRODUCTO" &
             "  Where(CODIGO = @CODIGO)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeP_PRODUCTO.CODIGO))

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

            Const sp As String = "SELECT * FROM P_PRODUCTO"
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

    Public Shared Function GetAll() As List(Of clsBeP_PRODUCTO)

        Dim lReturnList As New List(Of clsBeP_PRODUCTO)

        Try

            Const sp As String = "SELECT * FROM P_PRODUCTO"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeP_PRODUCTO As New clsBeP_PRODUCTO

                        For Each dr As DataRow In lDataTable.Rows
                            vBeP_PRODUCTO = New clsBeP_PRODUCTO()
                            Cargar(vBeP_PRODUCTO, dr)
                            lReturnList.Add(vBeP_PRODUCTO)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub GetSingle(ByRef pBeP_PRODUCTO As clsBeP_PRODUCTO)

        Try

            Const sp As String = "SELECT * FROM P_PRODUCTO" & _
            " Where(CODIGO = @CODIGO)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ROAD"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeP_PRODUCTO As New clsBeP_PRODUCTO

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeP_PRODUCTO, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function Get_Producto_By_Codigo(ByVal pCodigo As String) As clsBeP_PRODUCTO

        Get_Producto_By_Codigo = Nothing

        Try

            Const sp As String = "SELECT * FROM P_PRODUCTO" &
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

                        Dim vBeP_PRODUCTO As New clsBeP_PRODUCTO

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeP_PRODUCTO, lDataTable.Rows(0))
                            Return vBeP_PRODUCTO
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

    Public Shared Function MaxID() as Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(CODIGO),0) FROM P_PRODUCTO"

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
