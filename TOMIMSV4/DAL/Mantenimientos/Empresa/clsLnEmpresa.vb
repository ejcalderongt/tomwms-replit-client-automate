Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnEmpresa

    Public Shared Sub Cargar(ByRef oBeEmpresa As clsBeEmpresa, ByRef dr As DataRow)

        Try

            With oBeEmpresa

                .IdEmpresa = IIf(IsDBNull(dr.Item("IdEmpresa")), 0, dr.Item("IdEmpresa"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Direccion = IIf(IsDBNull(dr.Item("direccion")), "", dr.Item("direccion"))
                .Telefono = IIf(IsDBNull(dr.Item("telefono")), "", dr.Item("telefono"))
                .Email = IIf(IsDBNull(dr.Item("email")), "", dr.Item("email"))
                .Razon_social = IIf(IsDBNull(dr.Item("razon_social")), "", dr.Item("razon_social"))
                .Representante = IIf(IsDBNull(dr.Item("representante")), "", dr.Item("representante"))
                .Corr_cod_barra = IIf(IsDBNull(dr.Item("corr_cod_barra")), 0, dr.Item("corr_cod_barra"))
                .Path_printer = IIf(IsDBNull(dr.Item("path_printer")), "", dr.Item("path_printer"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .ClienteRapido = IIf(IsDBNull(dr.Item("clienteRapido")), False, dr.Item("clienteRapido"))
                .Imagen = IIf(IsDBNull(dr.Item("imagen")), Nothing, dr.Item("imagen"))
                .Operador_logistico = IIf(IsDBNull(dr.Item("operador_logistico")), False, dr.Item("operador_logistico"))
                .Puerto_escaner = IIf(IsDBNull(dr.Item("puerto_escaner")), 0, dr.Item("puerto_escaner"))
                .Control_presentaciones = IIf(IsDBNull(dr.Item("control_presentaciones")), False, dr.Item("control_presentaciones"))
                .Anulaciones_por_supervisor = IIf(IsDBNull(dr.Item("anulaciones_por_supervisor")), False, dr.Item("anulaciones_por_supervisor"))
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Clave = IIf(IsDBNull(dr.Item("clave")), "", dr.Item("clave"))
                .Intento = IIf(IsDBNull(dr.Item("intento")), 0, dr.Item("intento"))
                .Duracionclave = IIf(IsDBNull(dr.Item("duracionclave")), 0, dr.Item("duracionclave"))
                .Duracionclavetemporal = IIf(IsDBNull(dr.Item("duracionclavetemporal")), 0, dr.Item("duracionclavetemporal"))
                .codigo_automatico = IIf(IsDBNull(dr.Item("codigo_automatico")), False, dr.Item("codigo_automatico"))
                .codigo_automatico = IIf(IsDBNull(dr.Item("politica_contraseñas")), False, dr.Item("politica_contraseñas"))
                .IdMotivoAjusteInventario = IIf(IsDBNull(dr.Item("IdMotivoAjusteInventario")), False, dr.Item("IdMotivoAjusteInventario"))
                .Hora_Corte_Jornada_Sistema = IIf(IsDBNull(dr.Item("hora_corte_jornada_sistema")), "23:59:59", dr.Item("hora_corte_jornada_sistema"))
                .Generar_Stock_Jornada = IIf(IsDBNull(dr.Item("generar_stock_jornada")), False, dr.Item("generar_stock_jornada"))
                '#GT12052022: si la app busca o no actualizaciones (cealsa)
                .buscar_actualizacion_hh = IIf(IsDBNull(dr.Item("buscar_actualizacion_hh")), False, dr.Item("buscar_actualizacion_hh"))
                .Version_BD = IIf(IsDBNull(dr.Item("Version_BD")), "0", dr.Item("Version_BD"))
                .AWS_Token = IIf(IsDBNull(dr.Item("AWS_Token")), "", dr.Item("AWS_Token"))

            End With

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeEmpresa As clsBeEmpresa, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("empresa")
            Ins.Add("idempresa", "@idempresa", DataType.Parametro)
            Ins.Add("nombre", "@nombre", DataType.Parametro)
            Ins.Add("direccion", "@direccion", DataType.Parametro)
            Ins.Add("telefono", "@telefono", DataType.Parametro)
            Ins.Add("email", "@email", DataType.Parametro)
            Ins.Add("razon_social", "@razon_social", DataType.Parametro)
            Ins.Add("representante", "@representante", DataType.Parametro)
            Ins.Add("corr_cod_barra", "@corr_cod_barra", DataType.Parametro)
            Ins.Add("path_printer", "@path_printer", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("clienterapido", "@clienterapido", DataType.Parametro)
            If Not oBeEmpresa.Imagen Is Nothing Then Ins.Add("imagen", "@imagen", DataType.Parametro)
            Ins.Add("operador_logistico", "@operador_logistico", DataType.Parametro)
            Ins.Add("puerto_escaner", "@puerto_escaner", DataType.Parametro)
            Ins.Add("control_presentaciones", "@control_presentaciones", DataType.Parametro)
            Ins.Add("anulaciones_por_supervisor", "@anulaciones_por_supervisor", DataType.Parametro)
            Ins.Add("codigo", "@codigo", DataType.Parametro)
            Ins.Add("clave", "@clave", DataType.Parametro)
            Ins.Add("intento", "@intento", DataType.Parametro)
            Ins.Add("duracionclave", "@duracionclave", DataType.Parametro)
            Ins.Add("duracionclavetemporal", "@duracionclavetemporal", DataType.Parametro)
            Ins.Add("politica_contraseñas", "@politica_contraseñas", DataType.Parametro)
            Ins.Add("codigo_automatico", "@codigo_automatico", DataType.Parametro)
            Ins.Add("IdMotivoAjusteInventario", "@IdMotivoAjusteInventario", DataType.Parametro)
            Ins.Add("hora_corte_jornada_sistema", "@hora_corte_jornada_sistema", DataType.Parametro)
            Ins.Add("generar_stock_jornada", "@generar_stock_jornada", DataType.Parametro)
            '#GT12052022: si la app busca o no actualizaciones (cealsa)
            Ins.Add("buscar_actualizacion_hh", "@buscar_actualizacion_hh", DataType.Parametro)
            '#EJC202208041102: 
            Ins.Add("version_bd", "@version_bd", DataType.Parametro)
            Ins.Add("aws_token", "@aws_token", DataType.Parametro)


            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeEmpresa.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeEmpresa.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeEmpresa.Direccion))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeEmpresa.Telefono))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBeEmpresa.Email))
            cmd.Parameters.Add(New SqlParameter("@RAZON_SOCIAL", oBeEmpresa.Razon_social))
            cmd.Parameters.Add(New SqlParameter("@REPRESENTANTE", oBeEmpresa.Representante))
            cmd.Parameters.Add(New SqlParameter("@CORR_COD_BARRA", oBeEmpresa.Corr_cod_barra))
            cmd.Parameters.Add(New SqlParameter("@PATH_PRINTER", oBeEmpresa.Path_printer))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeEmpresa.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeEmpresa.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeEmpresa.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeEmpresa.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeEmpresa.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@CLIENTERAPIDO", oBeEmpresa.ClienteRapido))
            If Not oBeEmpresa.Imagen Is Nothing Then cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeEmpresa.Imagen))
            cmd.Parameters.Add(New SqlParameter("@OPERADOR_LOGISTICO", oBeEmpresa.Operador_logistico))
            cmd.Parameters.Add(New SqlParameter("@PUERTO_ESCANER", oBeEmpresa.Puerto_escaner))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_PRESENTACIONES", oBeEmpresa.Control_presentaciones))
            cmd.Parameters.Add(New SqlParameter("@ANULACIONES_POR_SUPERVISOR", oBeEmpresa.Anulaciones_por_supervisor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeEmpresa.Codigo))
            cmd.Parameters.Add(New SqlParameter("@CLAVE", oBeEmpresa.Clave))
            cmd.Parameters.Add(New SqlParameter("@INTENTO", oBeEmpresa.Intento))
            cmd.Parameters.Add(New SqlParameter("@DURACIONCLAVE", oBeEmpresa.Duracionclave))
            cmd.Parameters.Add(New SqlParameter("@DURACIONCLAVETEMPORAL", oBeEmpresa.Duracionclavetemporal))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_AUTOMATICO", oBeEmpresa.codigo_automatico))
            cmd.Parameters.Add(New SqlParameter("@POLITICA_CONTRASEÑAS", oBeEmpresa.politica_contraseñas))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOAJUSTEINVENTARIO", oBeEmpresa.IdMotivoAjusteInventario))
            cmd.Parameters.Add(New SqlParameter("@HORA_CORTE_JORNADA_SISTEMA", oBeEmpresa.Hora_Corte_Jornada_Sistema))
            cmd.Parameters.Add(New SqlParameter("@GENERAR_STOCK_JORNADA", oBeEmpresa.Generar_Stock_Jornada))
            '#GT12052022: si la app busca o no actualizaciones (cealsa)
            cmd.Parameters.Add(New SqlParameter("@BUSCAR_ACTUALIZACION_HH", oBeEmpresa.buscar_actualizacion_hh))
            '#EJC202208041103
            cmd.Parameters.Add(New SqlParameter("@VERSION_BD", oBeEmpresa.Version_BD))
            cmd.Parameters.Add(New SqlParameter("@AWS_TOKEN", oBeEmpresa.AWS_Token))


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

    Public Shared Function Actualizar(ByRef oBeEmpresa As clsBeEmpresa, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("empresa")
            Upd.Add("idempresa", "@idempresa", DataType.Parametro)
            Upd.Add("nombre", "@nombre", DataType.Parametro)
            Upd.Add("direccion", "@direccion", DataType.Parametro)
            Upd.Add("telefono", "@telefono", DataType.Parametro)
            Upd.Add("email", "@email", DataType.Parametro)
            Upd.Add("razon_social", "@razon_social", DataType.Parametro)
            Upd.Add("representante", "@representante", DataType.Parametro)
            Upd.Add("corr_cod_barra", "@corr_cod_barra", DataType.Parametro)
            Upd.Add("path_printer", "@path_printer", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("clienterapido", "@clienterapido", DataType.Parametro)
            If Not oBeEmpresa.Imagen Is Nothing Then Upd.Add("imagen", "@imagen", DataType.Parametro)
            Upd.Add("operador_logistico", "@operador_logistico", DataType.Parametro)
            Upd.Add("puerto_escaner", "@puerto_escaner", DataType.Parametro)
            Upd.Add("control_presentaciones", "@control_presentaciones", DataType.Parametro)
            Upd.Add("anulaciones_por_supervisor", "@anulaciones_por_supervisor", DataType.Parametro)
            Upd.Add("codigo", "@codigo", DataType.Parametro)
            Upd.Add("clave", "@clave", DataType.Parametro)
            Upd.Add("intento", "@intento", DataType.Parametro)
            Upd.Add("duracionclave", "@duracionclave", DataType.Parametro)
            Upd.Add("duracionclavetemporal", "@duracionclavetemporal", DataType.Parametro)
            Upd.Add("codigo_automatico", "@codigo_automatico", DataType.Parametro)
            Upd.Add("politica_contraseñas", "@politica_contraseñas", DataType.Parametro)
            Upd.Add("IdMotivoAjusteInventario", "@IdMotivoAjusteInventario", DataType.Parametro)
            Upd.Add("hora_corte_jornada_sistema", "@hora_corte_jornada_sistema", DataType.Parametro)
            Upd.Add("generar_stock_jornada", "@generar_stock_jornada", DataType.Parametro)
            '#GT12052022: si la app busca o no actualizacion (cealsa)
            Upd.Add("buscar_actualizacion_hh", "@buscar_actualizacion_hh", DataType.Parametro)
            '#EJC202208041102: 
            Upd.Add("version_bd", "@version_bd", DataType.Parametro)
            Upd.Add("aws_token", "@aws_token", DataType.Parametro)
            Upd.Where("IdEmpresa = @IdEmpresa")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeEmpresa.IdEmpresa))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE", oBeEmpresa.Nombre))
            cmd.Parameters.Add(New SqlParameter("@DIRECCION", oBeEmpresa.Direccion))
            cmd.Parameters.Add(New SqlParameter("@TELEFONO", oBeEmpresa.Telefono))
            cmd.Parameters.Add(New SqlParameter("@EMAIL", oBeEmpresa.Email))
            cmd.Parameters.Add(New SqlParameter("@RAZON_SOCIAL", oBeEmpresa.Razon_social))
            cmd.Parameters.Add(New SqlParameter("@REPRESENTANTE", oBeEmpresa.Representante))
            cmd.Parameters.Add(New SqlParameter("@CORR_COD_BARRA", oBeEmpresa.Corr_cod_barra))
            cmd.Parameters.Add(New SqlParameter("@PATH_PRINTER", oBeEmpresa.Path_printer))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeEmpresa.Activo))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeEmpresa.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeEmpresa.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeEmpresa.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeEmpresa.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@CLIENTERAPIDO", oBeEmpresa.ClienteRapido))
            If Not oBeEmpresa.Imagen Is Nothing Then cmd.Parameters.Add(New SqlParameter("@IMAGEN", oBeEmpresa.Imagen))
            cmd.Parameters.Add(New SqlParameter("@OPERADOR_LOGISTICO", oBeEmpresa.Operador_logistico))
            cmd.Parameters.Add(New SqlParameter("@PUERTO_ESCANER", oBeEmpresa.Puerto_escaner))
            cmd.Parameters.Add(New SqlParameter("@CONTROL_PRESENTACIONES", oBeEmpresa.Control_presentaciones))
            cmd.Parameters.Add(New SqlParameter("@ANULACIONES_POR_SUPERVISOR", oBeEmpresa.Anulaciones_por_supervisor))
            cmd.Parameters.Add(New SqlParameter("@CODIGO", oBeEmpresa.Codigo))
            cmd.Parameters.Add(New SqlParameter("@CLAVE", oBeEmpresa.Clave))
            cmd.Parameters.Add(New SqlParameter("@INTENTO", oBeEmpresa.Intento))
            cmd.Parameters.Add(New SqlParameter("@DURACIONCLAVE", oBeEmpresa.Duracionclave))
            cmd.Parameters.Add(New SqlParameter("@DURACIONCLAVETEMPORAL", oBeEmpresa.Duracionclavetemporal))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_AUTOMATICO", oBeEmpresa.codigo_automatico))
            cmd.Parameters.Add(New SqlParameter("@POLITICA_CONTRASEÑAS", oBeEmpresa.politica_contraseñas))
            cmd.Parameters.Add(New SqlParameter("@IDMOTIVOAJUSTEINVENTARIO", oBeEmpresa.IdMotivoAjusteInventario))
            If (oBeEmpresa.Hora_Corte_Jornada_Sistema.Year < 1700) Then
                oBeEmpresa.Hora_Corte_Jornada_Sistema = New Date(1900, 1, 1, 0, 0, 0)
            End If
            cmd.Parameters.Add(New SqlParameter("@HORA_CORTE_JORNADA_SISTEMA", oBeEmpresa.Hora_Corte_Jornada_Sistema))
            cmd.Parameters.Add(New SqlParameter("@GENERAR_STOCK_JORNADA", oBeEmpresa.Generar_Stock_Jornada))
            cmd.Parameters.Add(New SqlParameter("@BUSCAR_ACTUALIZACION_HH", oBeEmpresa.buscar_actualizacion_hh))
            '#EJC202208041103
            cmd.Parameters.Add(New SqlParameter("@VERSION_BD", oBeEmpresa.Version_BD))
            cmd.Parameters.Add(New SqlParameter("@AWS_TOKEN", oBeEmpresa.AWS_Token))

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

    Public Shared Function Eliminar(ByRef oBeEmpresa As clsBeEmpresa, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Empresa" &
             "  Where(IdEmpresa = @IdEmpresa)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeEmpresa.IdEmpresa))

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

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Empresa"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            '#EJC20191205: Trans_Ref02
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

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

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Empresa"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeEmpresa As clsBeEmpresa) As Boolean

        Try

            Const sp As String = "SELECT * FROM Empresa" &
            " Where(IdEmpresa = @IdEmpresa)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeEmpresa.IdEmpresa))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEmpresa, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
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

    Public Shared Function ObtenerWithLocked(ByRef oBeEmpresa As clsBeEmpresa, ByVal HostName As String) As Boolean

        Try

            Const sp As String = "SELECT * FROM Empresa" &
            " WITH (ROWLOCK, UPDLOCK) Where(IdEmpresa = @IdEmpresa) "

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim lTransaction As SqlTransaction = Nothing

            lConnection.Open()

            lTransaction = lConnection.BeginTransaction(IsolationLevel.Serializable, HostName)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeEmpresa.IdEmpresa))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEmpresa, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            'lTransaction.Commit()

            Return True


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeEmpresa)

        Try

            Dim lReturnList As New List(Of clsBeEmpresa)
            Const sp As String = "SELECT * FROM Empresa"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeEmpresa As New clsBeEmpresa

            For Each dr As DataRow In dt.Rows

                vBeEmpresa = New clsBeEmpresa
                Cargar(vBeEmpresa, dr)
                lReturnList.Add(vBeEmpresa)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll(ByVal Activos As Boolean) As List(Of clsBeEmpresa)

        Try

            Dim lReturnList As New List(Of clsBeEmpresa)
            Const sp As String = "SELECT * FROM Empresa WHERE activo = @Activos"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@Activos", Activos)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeEmpresa As New clsBeEmpresa

            For Each dr As DataRow In dt.Rows

                vBeEmpresa = New clsBeEmpresa
                Cargar(vBeEmpresa, dr)
                lReturnList.Add(vBeEmpresa)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeEmpresa As clsBeEmpresa)

        Try

            Const sp As String = "SELECT * FROM Empresa" &
            " Where(IdEmpresa = @IdEmpresa)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", pBeEmpresa.IdEmpresa))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeEmpresa, dt.Rows(0))
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

    Public Shared Function GetSingle(ByRef pBeEmpresa As clsBeEmpresa,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction)

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Empresa" &
                                 " Where(IdEmpresa = @IdEmpresa)"


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdEmpresa", pBeEmpresa.IdEmpresa))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim BeEmpresa As New clsBeEmpresa
                Cargar(BeEmpresa, dt.Rows(0))
                Return BeEmpresa
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdEmpresa),0) FROM Empresa"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()
                Using lCommand As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()
                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
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

    Public Shared Function GetNombreEmpresa(ByVal IdEmpresa As Integer) As String

        GetNombreEmpresa = ""

        Try

            Const sp As String = "SELECT Nombre FROM Empresa" &
            " Where(IdEmpresa = @IdEmpresa)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", IdEmpresa))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                GetNombreEmpresa = IIf(IsDBNull(dt.Rows(0).Item("Nombre")), "", dt.Rows(0).Item("Nombre"))
            End If


        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdEmpresa(ByVal pIdEmpresa As Integer) As clsBeEmpresa

        Get_Single_By_IdEmpresa = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Empresa" &
            " Where(IdEmpresa = @IdEmpresa)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDEMPRESA", pIdEmpresa))

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim pBeEmpresa As New clsBeEmpresa()

            If dt.Rows.Count = 1 Then
                Cargar(pBeEmpresa, dt.Rows(0))
                Get_Single_By_IdEmpresa = pBeEmpresa
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_AWS(ByRef oBeEmpresa As clsBeEmpresa, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("empresa")
            '#EJC202208041102: 
            Upd.Add("version_bd", "@version_bd", DataType.Parametro)
            Upd.Add("aws_token", "@aws_token", DataType.Parametro)
            Upd.Where("IdEmpresa = @IdEmpresa")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            '#EJC20191205: Trans_Ref03
            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDEMPRESA", oBeEmpresa.IdEmpresa))
            '#EJC202208041103
            cmd.Parameters.Add(New SqlParameter("@VERSION_BD", oBeEmpresa.Version_BD))
            cmd.Parameters.Add(New SqlParameter("@AWS_TOKEN", oBeEmpresa.AWS_Token))

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