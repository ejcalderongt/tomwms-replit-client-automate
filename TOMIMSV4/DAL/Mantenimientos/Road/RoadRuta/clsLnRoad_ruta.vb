Imports System.Data.SqlClient

Public Class clsLnRoad_ruta

    Public Shared Sub Cargar(ByRef oBeRoad_ruta As clsBeRoad_ruta, ByRef dr As DataRow)

        Try

            With oBeRoad_ruta

                .IdRuta = IIf(IsDBNull(dr.Item("IdRuta")), 0, dr.Item("IdRuta"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdUbicacionTransito = IIf(IsDBNull(dr.Item("IdUbicacionTransito")), 0, dr.Item("IdUbicacionTransito"))
                .CODIGO = IIf(IsDBNull(dr.Item("CODIGO")), "", dr.Item("CODIGO"))
                .NOMBRE = IIf(IsDBNull(dr.Item("NOMBRE")), "", dr.Item("NOMBRE"))
                .ACTIVO = IIf(IsDBNull(dr.Item("ACTIVO")), "", dr.Item("ACTIVO"))
                .VENDEDOR = IIf(IsDBNull(dr.Item("VENDEDOR")), "", dr.Item("VENDEDOR"))
                .VENTA = IIf(IsDBNull(dr.Item("VENTA")), "", dr.Item("VENTA"))
                .FORANIA = IIf(IsDBNull(dr.Item("FORANIA")), "", dr.Item("FORANIA"))
                .SUCURSAL = IIf(IsDBNull(dr.Item("SUCURSAL")), "", dr.Item("SUCURSAL"))
                .TIPO = IIf(IsDBNull(dr.Item("TIPO")), "", dr.Item("TIPO"))
                .SUBTIPO = IIf(IsDBNull(dr.Item("SUBTIPO")), "", dr.Item("SUBTIPO"))
                .BODEGA = IIf(IsDBNull(dr.Item("BODEGA")), "", dr.Item("BODEGA"))
                .SUBBODEGA = IIf(IsDBNull(dr.Item("SUBBODEGA")), "", dr.Item("SUBBODEGA"))
                .DESCUENTO = IIf(IsDBNull(dr.Item("DESCUENTO")), "", dr.Item("DESCUENTO"))
                .BONIF = IIf(IsDBNull(dr.Item("BONIF")), "", dr.Item("BONIF"))
                .KILOMETRAJE = IIf(IsDBNull(dr.Item("KILOMETRAJE")), "", dr.Item("KILOMETRAJE"))
                .IMPRESION = IIf(IsDBNull(dr.Item("IMPRESION")), "", dr.Item("IMPRESION"))
                .RECIBOPROPIO = IIf(IsDBNull(dr.Item("RECIBOPROPIO")), "", dr.Item("RECIBOPROPIO"))
                .CELULAR = IIf(IsDBNull(dr.Item("CELULAR")), "", dr.Item("CELULAR"))
                .RENTABIL = IIf(IsDBNull(dr.Item("RENTABIL")), "", dr.Item("RENTABIL"))
                .OFERTA = IIf(IsDBNull(dr.Item("OFERTA")), "", dr.Item("OFERTA"))
                .PERCRENT = IIf(IsDBNull(dr.Item("PERCRENT")), 0.0, dr.Item("PERCRENT"))
                .PASARCREDITO = IIf(IsDBNull(dr.Item("PASARCREDITO")), "", dr.Item("PASARCREDITO"))
                .TECLADO = IIf(IsDBNull(dr.Item("TECLADO")), "", dr.Item("TECLADO"))
                .EDITDEVPREC = IIf(IsDBNull(dr.Item("EDITDEVPREC")), "", dr.Item("EDITDEVPREC"))
                .EDITDESC = IIf(IsDBNull(dr.Item("EDITDESC")), "", dr.Item("EDITDESC"))
                .PARAMS = IIf(IsDBNull(dr.Item("PARAMS")), "", dr.Item("PARAMS"))
                .SEMANA = IIf(IsDBNull(dr.Item("SEMANA")), 0, dr.Item("SEMANA"))
                .OBJANO = IIf(IsDBNull(dr.Item("OBJANO")), 0, dr.Item("OBJANO"))
                .OBJMES = IIf(IsDBNull(dr.Item("OBJMES")), 0, dr.Item("OBJMES"))
                .SYNCFOLD = IIf(IsDBNull(dr.Item("SYNCFOLD")), "", dr.Item("SYNCFOLD"))
                .WLFOLD = IIf(IsDBNull(dr.Item("WLFOLD")), "", dr.Item("WLFOLD"))
                .FTPFOLD = IIf(IsDBNull(dr.Item("FTPFOLD")), "", dr.Item("FTPFOLD"))
                .EMAIL = IIf(IsDBNull(dr.Item("EMAIL")), "", dr.Item("EMAIL"))
                .LASTIMP = IIf(IsDBNull(dr.Item("LASTIMP")), 0, dr.Item("LASTIMP"))
                .LASTCOM = IIf(IsDBNull(dr.Item("LASTCOM")), 0, dr.Item("LASTCOM"))
                .LASTEXP = IIf(IsDBNull(dr.Item("LASTEXP")), 0, dr.Item("LASTEXP"))
                .IMPSTAT = IIf(IsDBNull(dr.Item("IMPSTAT")), "", dr.Item("IMPSTAT"))
                .EXPSTAT = IIf(IsDBNull(dr.Item("EXPSTAT")), "", dr.Item("EXPSTAT"))
                .COMSTAT = IIf(IsDBNull(dr.Item("COMSTAT")), "", dr.Item("COMSTAT"))
                .PARAM1 = IIf(IsDBNull(dr.Item("PARAM1")), "", dr.Item("PARAM1"))
                .PARAM2 = IIf(IsDBNull(dr.Item("PARAM2")), "", dr.Item("PARAM2"))
                .PESOLIM = IIf(IsDBNull(dr.Item("PESOLIM")), 0.0, dr.Item("PESOLIM"))
                .INTERVALO_MAX = IIf(IsDBNull(dr.Item("INTERVALO_MAX")), 0, dr.Item("INTERVALO_MAX"))
                .LECTURAS_VALID = IIf(IsDBNull(dr.Item("LECTURAS_VALID")), 0, dr.Item("LECTURAS_VALID"))
                .INTENTOS_LECT = IIf(IsDBNull(dr.Item("INTENTOS_LECT")), 0, dr.Item("INTENTOS_LECT"))
                .HORA_INI = IIf(IsDBNull(dr.Item("HORA_INI")), 0, dr.Item("HORA_INI"))
                .HORA_FIN = IIf(IsDBNull(dr.Item("HORA_FIN")), 0, dr.Item("HORA_FIN"))
                .APLICACION_USA = IIf(IsDBNull(dr.Item("APLICACION_USA")), 0, dr.Item("APLICACION_USA"))
                .PUERTO_GPS = IIf(IsDBNull(dr.Item("PUERTO_GPS")), 0, dr.Item("PUERTO_GPS"))
                .ES_RUTA_OFICINA = IIf(IsDBNull(dr.Item("ES_RUTA_OFICINA")), False, dr.Item("ES_RUTA_OFICINA"))
                .DILUIR_BON = IIf(IsDBNull(dr.Item("DILUIR_BON")), False, dr.Item("DILUIR_BON"))
                .PREIMPRESION_FACTURA = IIf(IsDBNull(dr.Item("PREIMPRESION_FACTURA")), False, dr.Item("PREIMPRESION_FACTURA"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public shared Function Insertar(ByRef oBeRoad_ruta As clsBeRoad_ruta, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("road_ruta")
            Ins.Add("idruta", "@idruta", DataType.Parametro)
            Ins.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Ins.Add("idubicaciontransito", "@idubicaciontransito", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("vendedor", "@vendedor", DataType.Parametro)
            Ins.Add("venta", "@venta", DataType.Parametro)
            Ins.Add("forania", "@forania", DataType.Parametro)
            Ins.Add("sucursal", "@sucursal", DataType.Parametro)
            Ins.Add("tipo", "@tipo", DataType.Parametro)
            Ins.Add("subtipo", "@subtipo", DataType.Parametro)
            Ins.Add("bodega", "@bodega", DataType.Parametro)
            Ins.Add("subbodega", "@subbodega", DataType.Parametro)
            Ins.Add("descuento", "@descuento", DataType.Parametro)
            Ins.Add("bonif", "@bonif", DataType.Parametro)
            Ins.Add("kilometraje", "@kilometraje", DataType.Parametro)
            Ins.Add("impresion", "@impresion", DataType.Parametro)
            Ins.Add("recibopropio", "@recibopropio", DataType.Parametro)
            Ins.Add("celular", "@celular", DataType.Parametro)
            Ins.Add("rentabil", "@rentabil", DataType.Parametro)
            Ins.Add("oferta", "@oferta", DataType.Parametro)
            Ins.Add("percrent", "@percrent", DataType.Parametro)
            Ins.Add("pasarcredito", "@pasarcredito", DataType.Parametro)
            Ins.Add("teclado", "@teclado", DataType.Parametro)
            Ins.Add("editdevprec", "@editdevprec", DataType.Parametro)
            Ins.Add("editdesc", "@editdesc", DataType.Parametro)
            Ins.Add("params", "@params", DataType.Parametro)
            Ins.Add("semana", "@semana", DataType.Parametro)
            Ins.Add("objano", "@objano", DataType.Parametro)
            Ins.Add("objmes", "@objmes", DataType.Parametro)
            Ins.Add("syncfold", "@syncfold", DataType.Parametro)
            Ins.Add("wlfold", "@wlfold", DataType.Parametro)
            Ins.Add("ftpfold", "@ftpfold", DataType.Parametro)
            Ins.Add("email", "@email", DataType.Parametro)
            Ins.Add("lastimp", "@lastimp", DataType.Parametro)
            Ins.Add("lastcom", "@lastcom", DataType.Parametro)
            Ins.Add("lastexp", "@lastexp", DataType.Parametro)
            Ins.Add("impstat", "@impstat", DataType.Parametro)
            Ins.Add("expstat", "@expstat", DataType.Parametro)
            Ins.Add("comstat", "@comstat", DataType.Parametro)
            Ins.Add("param1", "@param1", DataType.Parametro)
            Ins.Add("param2", "@param2", DataType.Parametro)
            Ins.Add("pesolim", "@pesolim", DataType.Parametro)
            Ins.Add("intervalo_max", "@intervalo_max", DataType.Parametro)
            Ins.Add("lecturas_valid", "@lecturas_valid", DataType.Parametro)
            Ins.Add("intentos_lect", "@intentos_lect", DataType.Parametro)
            Ins.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Ins.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Ins.Add("aplicacion_usa", "@aplicacion_usa", DataType.Parametro)
            Ins.Add("puerto_gps", "@puerto_gps", DataType.Parametro)
            Ins.Add("es_ruta_oficina", "@es_ruta_oficina", DataType.Parametro)
            Ins.Add("diluir_bon", "@diluir_bon", DataType.Parametro)
            Ins.Add("preimpresion_factura", "@preimpresion_factura", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)


            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
                oBeRoad_ruta.IdRuta = MaxID(pConection, pTransaction) + 1
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
                oBeRoad_ruta.IdRuta = MaxID(lConnection, lTransaction) + 1
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRUTA", oBeRoad_ruta.IdRuta))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeRoad_ruta.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONTRANSITO", oBeRoad_ruta.IdUbicacionTransito))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeRoad_ruta.CODIGO))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeRoad_ruta.NOMBRE))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeRoad_ruta.ACTIVO))
            cmd.Parameters.Add(New SqlParameter("@VENDEDOR", oBeRoad_ruta.VENDEDOR))
            cmd.Parameters.Add(New SqlParameter("@VENTA", oBeRoad_ruta.VENTA))
            cmd.Parameters.Add(New SqlParameter("@FORANIA", oBeRoad_ruta.FORANIA))
            cmd.Parameters.Add(New SqlParameter("@SUCURSAL", oBeRoad_ruta.SUCURSAL))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeRoad_ruta.TIPO))
            cmd.Parameters.Add(New SqlParameter("@SUBTIPO", oBeRoad_ruta.SUBTIPO))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeRoad_ruta.BODEGA))
            cmd.Parameters.Add(New SqlParameter("@SUBBODEGA", oBeRoad_ruta.SUBBODEGA))
            cmd.Parameters.Add(New SqlParameter("@DESCUENTO", oBeRoad_ruta.DESCUENTO))
            cmd.Parameters.Add(New SqlParameter("@BONIF", oBeRoad_ruta.BONIF))
            cmd.Parameters.Add(New SqlParameter("@KILOMETRAJE", oBeRoad_ruta.KILOMETRAJE))
            cmd.Parameters.Add(New SqlParameter("@IMPRESION", oBeRoad_ruta.IMPRESION))
            cmd.Parameters.Add(New SqlParameter("@RECIBOPROPIO", oBeRoad_ruta.RECIBOPROPIO))
            cmd.Parameters.Add(New SqlParameter("@CELULAR", oBeRoad_ruta.CELULAR))
            cmd.Parameters.Add(New SqlParameter("@RENTABIL", oBeRoad_ruta.RENTABIL))
            cmd.Parameters.Add(New SqlParameter("@OFERTA", oBeRoad_ruta.OFERTA))
            cmd.Parameters.Add(New SqlParameter("@PERCRENT", oBeRoad_ruta.PERCRENT))
            cmd.Parameters.Add(New SqlParameter("@PASARCREDITO", oBeRoad_ruta.PASARCREDITO))
            cmd.Parameters.Add(New SqlParameter("@TECLADO", oBeRoad_ruta.TECLADO))
            cmd.Parameters.Add(New SqlParameter("@EDITDEVPREC", oBeRoad_ruta.EDITDEVPREC))
            cmd.Parameters.Add(New SqlParameter("@EDITDESC", oBeRoad_ruta.EDITDESC))
            cmd.Parameters.Add(New SqlParameter("@PARAMS", oBeRoad_ruta.PARAMS))
            cmd.Parameters.Add(New SqlParameter("@SEMANA", oBeRoad_ruta.SEMANA))
            cmd.Parameters.Add(New SqlParameter("@OBJANO", oBeRoad_ruta.OBJANO))
            cmd.Parameters.Add(New SqlParameter("@OBJMES", oBeRoad_ruta.OBJMES))
            cmd.Parameters.Add(New SqlParameter("@SYNCFOLD", oBeRoad_ruta.SYNCFOLD))
            cmd.Parameters.Add(New SqlParameter("@WLFOLD", oBeRoad_ruta.WLFOLD))
            cmd.Parameters.Add(New SqlParameter("@FTPFOLD", oBeRoad_ruta.FTPFOLD))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBeRoad_ruta.EMAIL))
            cmd.Parameters.Add(New SqlParameter("@LASTIMP", oBeRoad_ruta.LASTIMP))
            cmd.Parameters.Add(New SqlParameter("@LASTCOM", oBeRoad_ruta.LASTCOM))
            cmd.Parameters.Add(New SqlParameter("@LASTEXP", oBeRoad_ruta.LASTEXP))
            cmd.Parameters.Add(New SqlParameter("@IMPSTAT", oBeRoad_ruta.IMPSTAT))
            cmd.Parameters.Add(New SqlParameter("@EXPSTAT", oBeRoad_ruta.EXPSTAT))
            cmd.Parameters.Add(New SqlParameter("@COMSTAT", oBeRoad_ruta.COMSTAT))
            cmd.Parameters.Add(New SqlParameter("@PARAM1", oBeRoad_ruta.PARAM1))
            cmd.Parameters.Add(New SqlParameter("@PARAM2", oBeRoad_ruta.PARAM2))
            cmd.Parameters.Add(New SqlParameter("@PESOLIM", oBeRoad_ruta.PESOLIM))
            cmd.Parameters.Add(New SqlParameter("@INTERVALO_MAX", oBeRoad_ruta.INTERVALO_MAX))
            cmd.Parameters.Add(New SqlParameter("@LECTURAS_VALID", oBeRoad_ruta.LECTURAS_VALID))
            cmd.Parameters.Add(New SqlParameter("@INTENTOS_LECT", oBeRoad_ruta.INTENTOS_LECT))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeRoad_ruta.HORA_INI))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeRoad_ruta.HORA_FIN))
            cmd.Parameters.Add(New SqlParameter("@APLICACION_USA", oBeRoad_ruta.APLICACION_USA))
            cmd.Parameters.Add(New SqlParameter("@PUERTO_GPS", oBeRoad_ruta.PUERTO_GPS))
            cmd.Parameters.Add(New SqlParameter("@ES_RUTA_OFICINA", oBeRoad_ruta.ES_RUTA_OFICINA))
            cmd.Parameters.Add(New SqlParameter("@DILUIR_BON", oBeRoad_ruta.DILUIR_BON))
            cmd.Parameters.Add(New SqlParameter("@PREIMPRESION_FACTURA", oBeRoad_ruta.PREIMPRESION_FACTURA))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            Return rowsAffected

            'oBeRoad_ruta.IdRuta = CInt(cmd.Parameters("@IDRUTA").Value)


        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            'cmd.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeRoad_ruta As clsBeRoad_ruta, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand

        Try

            Upd.Init("road_ruta")
            Upd.Add("idruta", "@idruta", DataType.Parametro)
            Upd.Add("idpropietariobodega", "@idpropietariobodega", DataType.Parametro)
            Upd.Add("idubicaciontransito", "@idubicaciontransito", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("vendedor", "@vendedor", DataType.Parametro)
            Upd.Add("venta", "@venta", DataType.Parametro)
            Upd.Add("forania", "@forania", DataType.Parametro)
            Upd.Add("sucursal", "@sucursal", DataType.Parametro)
            Upd.Add("tipo", "@tipo", DataType.Parametro)
            Upd.Add("subtipo", "@subtipo", DataType.Parametro)
            Upd.Add("bodega", "@bodega", DataType.Parametro)
            Upd.Add("subbodega", "@subbodega", DataType.Parametro)
            Upd.Add("descuento", "@descuento", DataType.Parametro)
            Upd.Add("bonif", "@bonif", DataType.Parametro)
            Upd.Add("kilometraje", "@kilometraje", DataType.Parametro)
            Upd.Add("impresion", "@impresion", DataType.Parametro)
            Upd.Add("recibopropio", "@recibopropio", DataType.Parametro)
            Upd.Add("celular", "@celular", DataType.Parametro)
            Upd.Add("rentabil", "@rentabil", DataType.Parametro)
            Upd.Add("oferta", "@oferta", DataType.Parametro)
            Upd.Add("percrent", "@percrent", DataType.Parametro)
            Upd.Add("pasarcredito", "@pasarcredito", DataType.Parametro)
            Upd.Add("teclado", "@teclado", DataType.Parametro)
            Upd.Add("editdevprec", "@editdevprec", DataType.Parametro)
            Upd.Add("editdesc", "@editdesc", DataType.Parametro)
            Upd.Add("params", "@params", DataType.Parametro)
            Upd.Add("semana", "@semana", DataType.Parametro)
            Upd.Add("objano", "@objano", DataType.Parametro)
            Upd.Add("objmes", "@objmes", DataType.Parametro)
            Upd.Add("syncfold", "@syncfold", DataType.Parametro)
            Upd.Add("wlfold", "@wlfold", DataType.Parametro)
            Upd.Add("ftpfold", "@ftpfold", DataType.Parametro)
            Upd.Add("email", "@email", DataType.Parametro)
            Upd.Add("lastimp", "@lastimp", DataType.Parametro)
            Upd.Add("lastcom", "@lastcom", DataType.Parametro)
            Upd.Add("lastexp", "@lastexp", DataType.Parametro)
            Upd.Add("impstat", "@impstat", DataType.Parametro)
            Upd.Add("expstat", "@expstat", DataType.Parametro)
            Upd.Add("comstat", "@comstat", DataType.Parametro)
            Upd.Add("param1", "@param1", DataType.Parametro)
            Upd.Add("param2", "@param2", DataType.Parametro)
            Upd.Add("pesolim", "@pesolim", DataType.Parametro)
            Upd.Add("intervalo_max", "@intervalo_max", DataType.Parametro)
            Upd.Add("lecturas_valid", "@lecturas_valid", DataType.Parametro)
            Upd.Add("intentos_lect", "@intentos_lect", DataType.Parametro)
            Upd.Add("hora_ini", "@hora_ini", DataType.Parametro)
            Upd.Add("hora_fin", "@hora_fin", DataType.Parametro)
            Upd.Add("aplicacion_usa", "@aplicacion_usa", DataType.Parametro)
            Upd.Add("puerto_gps", "@puerto_gps", DataType.Parametro)
            Upd.Add("es_ruta_oficina", "@es_ruta_oficina", DataType.Parametro)
            Upd.Add("diluir_bon", "@diluir_bon", DataType.Parametro)
            Upd.Add("preimpresion_factura", "@preimpresion_factura", DataType.Parametro)
            Upd.Where("IdRuta = @IdRuta")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            cmd.CommandType = CommandType.Text

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRUTA", oBeRoad_ruta.IdRuta))
            cmd.Parameters.Add(New SqlParameter("@IDPROPIETARIOBODEGA", oBeRoad_ruta.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IDUBICACIONTRANSITO", oBeRoad_ruta.IdUbicacionTransito))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeRoad_ruta.CODIGO))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeRoad_ruta.NOMBRE))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeRoad_ruta.ACTIVO))
            cmd.Parameters.Add(New SqlParameter("@VENDEDOR", oBeRoad_ruta.VENDEDOR))
            cmd.Parameters.Add(New SqlParameter("@VENTA", oBeRoad_ruta.VENTA))
            cmd.Parameters.Add(New SqlParameter("@FORANIA", oBeRoad_ruta.FORANIA))
            cmd.Parameters.Add(New SqlParameter("@SUCURSAL", oBeRoad_ruta.SUCURSAL))
            cmd.Parameters.Add(New SqlParameter("@TIPO", oBeRoad_ruta.TIPO))
            cmd.Parameters.Add(New SqlParameter("@SUBTIPO", oBeRoad_ruta.SUBTIPO))
            cmd.Parameters.Add(New SqlParameter("@BODEGA", oBeRoad_ruta.BODEGA))
            cmd.Parameters.Add(New SqlParameter("@SUBBODEGA", oBeRoad_ruta.SUBBODEGA))
            cmd.Parameters.Add(New SqlParameter("@DESCUENTO", oBeRoad_ruta.DESCUENTO))
            cmd.Parameters.Add(New SqlParameter("@BONIF", oBeRoad_ruta.BONIF))
            cmd.Parameters.Add(New SqlParameter("@KILOMETRAJE", oBeRoad_ruta.KILOMETRAJE))
            cmd.Parameters.Add(New SqlParameter("@IMPRESION", oBeRoad_ruta.IMPRESION))
            cmd.Parameters.Add(New SqlParameter("@RECIBOPROPIO", oBeRoad_ruta.RECIBOPROPIO))
            cmd.Parameters.Add(New SqlParameter("@CELULAR", oBeRoad_ruta.CELULAR))
            cmd.Parameters.Add(New SqlParameter("@RENTABIL", oBeRoad_ruta.RENTABIL))
            cmd.Parameters.Add(New SqlParameter("@OFERTA", oBeRoad_ruta.OFERTA))
            cmd.Parameters.Add(New SqlParameter("@PERCRENT", oBeRoad_ruta.PERCRENT))
            cmd.Parameters.Add(New SqlParameter("@PASARCREDITO", oBeRoad_ruta.PASARCREDITO))
            cmd.Parameters.Add(New SqlParameter("@TECLADO", oBeRoad_ruta.TECLADO))
            cmd.Parameters.Add(New SqlParameter("@EDITDEVPREC", oBeRoad_ruta.EDITDEVPREC))
            cmd.Parameters.Add(New SqlParameter("@EDITDESC", oBeRoad_ruta.EDITDESC))
            cmd.Parameters.Add(New SqlParameter("@PARAMS", oBeRoad_ruta.PARAMS))
            cmd.Parameters.Add(New SqlParameter("@SEMANA", oBeRoad_ruta.SEMANA))
            cmd.Parameters.Add(New SqlParameter("@OBJANO", oBeRoad_ruta.OBJANO))
            cmd.Parameters.Add(New SqlParameter("@OBJMES", oBeRoad_ruta.OBJMES))
            cmd.Parameters.Add(New SqlParameter("@SYNCFOLD", oBeRoad_ruta.SYNCFOLD))
            cmd.Parameters.Add(New SqlParameter("@WLFOLD", oBeRoad_ruta.WLFOLD))
            cmd.Parameters.Add(New SqlParameter("@FTPFOLD", oBeRoad_ruta.FTPFOLD))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBeRoad_ruta.EMAIL))
            cmd.Parameters.Add(New SqlParameter("@LASTIMP", oBeRoad_ruta.LASTIMP))
            cmd.Parameters.Add(New SqlParameter("@LASTCOM", oBeRoad_ruta.LASTCOM))
            cmd.Parameters.Add(New SqlParameter("@LASTEXP", oBeRoad_ruta.LASTEXP))
            cmd.Parameters.Add(New SqlParameter("@IMPSTAT", oBeRoad_ruta.IMPSTAT))
            cmd.Parameters.Add(New SqlParameter("@EXPSTAT", oBeRoad_ruta.EXPSTAT))
            cmd.Parameters.Add(New SqlParameter("@COMSTAT", oBeRoad_ruta.COMSTAT))
            cmd.Parameters.Add(New SqlParameter("@PARAM1", oBeRoad_ruta.PARAM1))
            cmd.Parameters.Add(New SqlParameter("@PARAM2", oBeRoad_ruta.PARAM2))
            cmd.Parameters.Add(New SqlParameter("@PESOLIM", oBeRoad_ruta.PESOLIM))
            cmd.Parameters.Add(New SqlParameter("@INTERVALO_MAX", oBeRoad_ruta.INTERVALO_MAX))
            cmd.Parameters.Add(New SqlParameter("@LECTURAS_VALID", oBeRoad_ruta.LECTURAS_VALID))
            cmd.Parameters.Add(New SqlParameter("@INTENTOS_LECT", oBeRoad_ruta.INTENTOS_LECT))
            cmd.Parameters.Add(New SqlParameter("@HORA_INI", oBeRoad_ruta.HORA_INI))
            cmd.Parameters.Add(New SqlParameter("@HORA_FIN", oBeRoad_ruta.HORA_FIN))
            cmd.Parameters.Add(New SqlParameter("@APLICACION_USA", oBeRoad_ruta.APLICACION_USA))
            cmd.Parameters.Add(New SqlParameter("@PUERTO_GPS", oBeRoad_ruta.PUERTO_GPS))
            cmd.Parameters.Add(New SqlParameter("@ES_RUTA_OFICINA", oBeRoad_ruta.ES_RUTA_OFICINA))
            cmd.Parameters.Add(New SqlParameter("@DILUIR_BON", oBeRoad_ruta.DILUIR_BON))
            cmd.Parameters.Add(New SqlParameter("@PREIMPRESION_FACTURA", oBeRoad_ruta.PREIMPRESION_FACTURA))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

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

    Public Shared Function Eliminar(ByRef oBeRoad_ruta As clsBeRoad_ruta, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand()

        Try

            cmd.CommandType = CommandType.Text

            Dim sp As String = " Delete from Road_ruta" &
             "  Where(IdRuta = @IdRuta)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRUTA", oBeRoad_ruta.IdRuta))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

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

    Public Function Obtener(ByRef oBeRoad_ruta As clsBeRoad_ruta) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Obtener = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)


            Dim sp As String = "SELECT * FROM Road_ruta" &
            " Where(IdRuta = @IdRuta)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDRUTA", oBeRoad_ruta.IdRuta))

            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count = 1 Then
                Cargar(oBeRoad_ruta, dt.Rows(0))
                Obtener = True
            End If

            lTransaction.Commit()

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
