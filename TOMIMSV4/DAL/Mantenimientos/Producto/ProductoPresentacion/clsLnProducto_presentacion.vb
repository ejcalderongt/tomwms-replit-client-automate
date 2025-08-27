Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnProducto_presentacion

    Public Shared Sub Cargar(ByRef oBeProducto_presentacion As clsBeProducto_Presentacion, ByRef dr As DataRow)

        Try

            With oBeProducto_presentacion

                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Imprime_barra = IIf(IsDBNull(dr.Item("imprime_barra")), False, dr.Item("imprime_barra"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Factor = IIf(IsDBNull(dr.Item("factor")), 0.0, dr.Item("factor"))
                .MinimoExistencia = IIf(IsDBNull(dr.Item("MinimoExistencia")), 0.0, dr.Item("MinimoExistencia"))
                .MaximoExistencia = IIf(IsDBNull(dr.Item("MaximoExistencia")), 0.0, dr.Item("MaximoExistencia"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .EsPallet = IIf(IsDBNull(dr.Item("EsPallet")), False, dr.Item("EsPallet"))
                .Precio = IIf(IsDBNull(dr.Item("Precio")), 0.0, dr.Item("Precio"))
                .MinimoPeso = IIf(IsDBNull(dr.Item("MinimoPeso")), 0.0, dr.Item("MinimoPeso"))
                .MaximoPeso = IIf(IsDBNull(dr.Item("MaximoPeso")), 0.0, dr.Item("MaximoPeso"))
                .Costo = IIf(IsDBNull(dr.Item("Costo")), 0.0, dr.Item("Costo"))
                .CamasPorTarima = IIf(IsDBNull(dr.Item("CamasPorTarima")), 0.0, dr.Item("CamasPorTarima"))
                .CajasPorCama = IIf(IsDBNull(dr.Item("CajasPorCama")), 0.0, dr.Item("CajasPorCama"))
                .Genera_lp_auto = IIf(IsDBNull(dr.Item("genera_lp_auto")), False, dr.Item("genera_lp_auto"))
                .Permitir_paletizar = IIf(IsDBNull(dr.Item("permitir_paletizar")), False, dr.Item("permitir_paletizar"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .IdPresentacionPallet = IIf(IsDBNull(dr.Item("IdPresentacionPallet")), 0, dr.Item("IdPresentacionPallet"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
            End With


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeProducto_presentacion As clsBeProducto_Presentacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("producto_presentacion")
            Ins.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Ins.Add("idproducto", "@idproducto", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("imprime_barra", "@imprime_barra", DataType.Parametro)
            Ins.Add("peso", "@peso", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("factor", "@factor", DataType.Parametro)
            Ins.Add("minimoexistencia", "@minimoexistencia", DataType.Parametro)
            Ins.Add("maximoexistencia", "@maximoexistencia", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("espallet", "@espallet", DataType.Parametro)
            Ins.Add("precio", "@precio", DataType.Parametro)
            Ins.Add("minimopeso", "@minimopeso", DataType.Parametro)
            Ins.Add("maximopeso", "@maximopeso", DataType.Parametro)
            Ins.Add("costo", "@costo", DataType.Parametro)
            Ins.Add("camasportarima", "@camasportarima", DataType.Parametro)
            Ins.Add("cajasporcama", "@cajasporcama", DataType.Parametro)
            Ins.Add("genera_lp_auto", "@genera_lp_auto", DataType.Parametro)
            Ins.Add("permitir_paletizar", "@permitir_paletizar", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            If Not oBeProducto_presentacion.IdPresentacionPallet = 0 Then Ins.Add("idpresentacionpallet", "@idpresentacionpallet", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeProducto_presentacion.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_presentacion.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeProducto_presentacion.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto_presentacion.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@IMPRIME_BARRA", oBeProducto_presentacion.Imprime_barra))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeProducto_presentacion.Peso))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeProducto_presentacion.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeProducto_presentacion.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeProducto_presentacion.Ancho))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeProducto_presentacion.Factor))
            cmd.Parameters.Add(New SqlParameter("@MINIMOEXISTENCIA", oBeProducto_presentacion.MinimoExistencia))
            cmd.Parameters.Add(New SqlParameter("@MAXIMOEXISTENCIA", oBeProducto_presentacion.MaximoExistencia))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_presentacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_presentacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_presentacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_presentacion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_presentacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@ESPALLET", oBeProducto_presentacion.EsPallet))
            cmd.Parameters.Add(New SqlParameter("@PRECIO", oBeProducto_presentacion.Precio))
            cmd.Parameters.Add(New SqlParameter("@MINIMOPESO", oBeProducto_presentacion.MinimoPeso))
            cmd.Parameters.Add(New SqlParameter("@MAXIMOPESO", oBeProducto_presentacion.MaximoPeso))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeProducto_presentacion.Costo))
            cmd.Parameters.Add(New SqlParameter("@CAMASPORTARIMA", oBeProducto_presentacion.CamasPorTarima))
            cmd.Parameters.Add(New SqlParameter("@CAJASPORCAMA", oBeProducto_presentacion.CajasPorCama))
            cmd.Parameters.Add(New SqlParameter("@GENERA_LP_AUTO", oBeProducto_presentacion.Genera_lp_auto))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_PALETIZAR", oBeProducto_presentacion.Permitir_paletizar))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeProducto_presentacion.Sistema))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto_presentacion.Codigo))
            If Not oBeProducto_presentacion.IdPresentacionPallet = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONPALLET", oBeProducto_presentacion.IdPresentacionPallet))


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

    Public Shared Function Actualizar(ByRef oBeProducto_presentacion As clsBeProducto_Presentacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("producto_presentacion")
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("idproducto", "@idproducto", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("imprime_barra", "@imprime_barra", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("factor", "@factor", DataType.Parametro)
            Upd.Add("minimoexistencia", "@minimoexistencia", DataType.Parametro)
            Upd.Add("maximoexistencia", "@maximoexistencia", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("EsPallet", "@EsPallet", DataType.Parametro)
            Upd.Add("precio", "@precio", DataType.Parametro)
            Upd.Add("minimopeso", "@minimopeso", DataType.Parametro)
            Upd.Add("maximopeso", "@maximopeso", DataType.Parametro)
            Upd.Add("costo", "@costo", DataType.Parametro)
            Upd.Add("camasportarima", "@camasportarima", DataType.Parametro)
            Upd.Add("cajasporcama", "@cajasporcama", DataType.Parametro)
            Upd.Add("genera_lp_auto", "@genera_lp_auto", DataType.Parametro)
            Upd.Add("permitir_paletizar", "@permitir_paletizar", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            If Not oBeProducto_presentacion.IdPresentacionPallet = 0 Then Upd.Add("idpresentacionpallet", "@idpresentacionpallet", DataType.Parametro)
            Upd.Where("IdPresentacion = @IdPresentacion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeProducto_presentacion.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IDPRODUCTO", oBeProducto_presentacion.IdProducto))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeProducto_presentacion.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", clsPublic.Quitar_Caracteres_No_Permitidos(oBeProducto_presentacion.Nombre)))
            cmd.Parameters.Add(New SqlParameter("@IMPRIME_BARRA", oBeProducto_presentacion.Imprime_barra))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeProducto_presentacion.Peso))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeProducto_presentacion.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeProducto_presentacion.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeProducto_presentacion.Ancho))
            cmd.Parameters.Add(New SqlParameter("@FACTOR", oBeProducto_presentacion.Factor))
            cmd.Parameters.Add(New SqlParameter("@MINIMOEXISTENCIA", oBeProducto_presentacion.MinimoExistencia))
            cmd.Parameters.Add(New SqlParameter("@MAXIMOEXISTENCIA", oBeProducto_presentacion.MaximoExistencia))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeProducto_presentacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeProducto_presentacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_presentacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_presentacion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_presentacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@ESPALLET", oBeProducto_presentacion.EsPallet))
            cmd.Parameters.Add(New SqlParameter("@PRECIO", oBeProducto_presentacion.Precio))
            cmd.Parameters.Add(New SqlParameter("@MINIMOPESO", oBeProducto_presentacion.MinimoPeso))
            cmd.Parameters.Add(New SqlParameter("@MAXIMOPESO", oBeProducto_presentacion.MaximoPeso))
            cmd.Parameters.Add(New SqlParameter("@COSTO", oBeProducto_presentacion.Costo))
            cmd.Parameters.Add(New SqlParameter("@CAMASPORTARIMA", oBeProducto_presentacion.CamasPorTarima))
            cmd.Parameters.Add(New SqlParameter("@CAJASPORCAMA", oBeProducto_presentacion.CajasPorCama))
            cmd.Parameters.Add(New SqlParameter("@GENERA_LP_AUTO", oBeProducto_presentacion.Genera_lp_auto))
            cmd.Parameters.Add(New SqlParameter("@PERMITIR_PALETIZAR", oBeProducto_presentacion.Permitir_paletizar))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeProducto_presentacion.Sistema))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeProducto_presentacion.Codigo))
            If Not oBeProducto_presentacion.IdPresentacionPallet = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONPALLET", oBeProducto_presentacion.IdPresentacionPallet))


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

    Public Shared Function Eliminar(ByRef oBeProducto_presentacion As clsBeProducto_Presentacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Producto_presentacion" &
             "  Where(IdPresentacion = @IdPresentacion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If


            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeProducto_presentacion.IdPresentacion))

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

    Public Shared Function Eliminar_Todos_By_IdProducto(ByVal pIdProducto As Integer,
                                                        Optional ByVal pConection As SqlConnection = Nothing,
                                                        Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Producto_presentacion" &
                                 "  Where(IdProducto= @IdProducto)"

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.AddWithValue("@IdProducto", pIdProducto)


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

    Public Shared Function Obtener(ByRef oBeProducto_presentacion As clsBeProducto_Presentacion) As Boolean

        Try

            Const sp As String = "SELECT * FROM Producto_presentacion" &
            " Where(IdPresentacion = @IdPresentacion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeProducto_presentacion.IdPresentacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeProducto_presentacion, dt.Rows(0))
            End If

            Return True

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeProducto_presentacion As clsBeProducto_Presentacion) As Boolean

        GetSingle = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM Producto_presentacion 
                                  Where(IdPresentacion = @IdPresentacion)"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDPRESENTACION", pBeProducto_presentacion.IdPresentacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                pBeProducto_presentacion = New clsBeProducto_Presentacion
                Cargar(pBeProducto_presentacion, dt.Rows(0))
                GetSingle = True
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdPresentacion),0) FROM Producto_presentacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue)
                    End If
                End Using
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

    Public Shared Function Actualizar_Valores_Relativos(ByRef oBeProducto_presentacion As clsBeProducto_Presentacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("producto_presentacion")
            Upd.Add("idpresentacion", "@idpresentacion", DataType.Parametro)
            Upd.Add("peso", "@peso", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            If Not oBeProducto_presentacion.IdPresentacionPallet = 0 Then Upd.Add("idpresentacionpallet", "@idpresentacionpallet", DataType.Parametro)
            Upd.Where("IdPresentacion = @IdPresentacion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPRESENTACION", oBeProducto_presentacion.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@PESO", oBeProducto_presentacion.Peso))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeProducto_presentacion.Alto))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeProducto_presentacion.Largo))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeProducto_presentacion.Ancho))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeProducto_presentacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeProducto_presentacion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeProducto_presentacion.Activo))
            If Not oBeProducto_presentacion.IdPresentacionPallet = 0 Then cmd.Parameters.Add(New SqlParameter("@IDPRESENTACIONPALLET", oBeProducto_presentacion.IdPresentacionPallet))

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

End Class
