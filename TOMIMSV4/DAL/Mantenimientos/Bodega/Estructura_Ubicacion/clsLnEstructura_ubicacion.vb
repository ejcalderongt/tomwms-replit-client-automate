Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks

Public Class clsLnEstructura_ubicacion

    Public Shared Sub Cargar(ByRef oBeEstructura_ubicacion As clsBeEstructura_ubicacion, ByRef dr As DataRow)

        Try

            With oBeEstructura_ubicacion

                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                .IdSector = IIf(IsDBNull(dr.Item("IdSector")), 0, dr.Item("IdSector"))
                .IdArea = IIf(IsDBNull(dr.Item("IdArea")), 0, dr.Item("IdArea"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .Descripcion = IIf(IsDBNull(dr.Item("descripcion")), "", dr.Item("descripcion"))
                .Ancho = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                .Largo = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                .Alto = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                .Nivel = IIf(IsDBNull(dr.Item("nivel")), 0, dr.Item("nivel"))
                .Indice_x = IIf(IsDBNull(dr.Item("indice_x")), 0, dr.Item("indice_x"))
                .IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                .IdTipoRotacion = IIf(IsDBNull(dr.Item("IdTipoRotacion")), 0, dr.Item("IdTipoRotacion"))
                .Sistema = IIf(IsDBNull(dr.Item("sistema")), False, dr.Item("sistema"))
                .Codigo_barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                .Codigo_barra2 = IIf(IsDBNull(dr.Item("codigo_barra2")), "", dr.Item("codigo_barra2"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Dañado = IIf(IsDBNull(dr.Item("dañado")), False, dr.Item("dañado"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Bloqueada = IIf(IsDBNull(dr.Item("bloqueada")), False, dr.Item("bloqueada"))
                .Acepta_pallet = IIf(IsDBNull(dr.Item("acepta_pallet")), False, dr.Item("acepta_pallet"))
                .Ubicacion_picking = IIf(IsDBNull(dr.Item("ubicacion_picking")), False, dr.Item("ubicacion_picking"))
                .Ubicacion_recepcion = IIf(IsDBNull(dr.Item("ubicacion_recepcion")), False, dr.Item("ubicacion_recepcion"))
                .Ubicacion_despacho = IIf(IsDBNull(dr.Item("ubicacion_despacho")), False, dr.Item("ubicacion_despacho"))
                .Ubicacion_merma = IIf(IsDBNull(dr.Item("ubicacion_merma")), False, dr.Item("ubicacion_merma"))
                .Margen_izquierdo = IIf(IsDBNull(dr.Item("margen_izquierdo")), 0.0, dr.Item("margen_izquierdo"))
                .Margen_derecho = IIf(IsDBNull(dr.Item("margen_derecho")), 0.0, dr.Item("margen_derecho"))
                .Margen_superior = IIf(IsDBNull(dr.Item("margen_superior")), 0.0, dr.Item("margen_superior"))
                .Margen_inferior = IIf(IsDBNull(dr.Item("margen_inferior")), 0.0, dr.Item("margen_inferior"))
                .Orientacion_pos = IIf(IsDBNull(dr.Item("orientacion_pos")), "", dr.Item("orientacion_pos"))


            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar(ByRef oBeEstructura_ubicacion As clsBeEstructura_ubicacion,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("estructura_ubicacion")
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("idsector", "@idsector", DataType.Parametro)
            Ins.Add("idarea", "@idarea", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("nivel", "@nivel", DataType.Parametro)
            Ins.Add("indice_x", "@indice_x", DataType.Parametro)
            Ins.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
            Ins.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("codigo_barra2", "@codigo_barra2", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("dañado", "@dañado", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("bloqueada", "@bloqueada", DataType.Parametro)
            Ins.Add("acepta_pallet", "@acepta_pallet", DataType.Parametro)
            Ins.Add("ubicacion_picking", "@ubicacion_picking", DataType.Parametro)
            Ins.Add("ubicacion_recepcion", "@ubicacion_recepcion", DataType.Parametro)
            Ins.Add("ubicacion_despacho", "@ubicacion_despacho", DataType.Parametro)
            Ins.Add("ubicacion_merma", "@ubicacion_merma", DataType.Parametro)
            Ins.Add("margen_izquierdo", "@margen_izquierdo", DataType.Parametro)
            Ins.Add("margen_derecho", "@margen_derecho", DataType.Parametro)
            Ins.Add("margen_superior", "@margen_superior", DataType.Parametro)
            Ins.Add("margen_inferior", "@margen_inferior", DataType.Parametro)
            Ins.Add("orientacion_pos", "@orientacion_pos", DataType.Parametro)

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

            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeEstructura_ubicacion.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeEstructura_ubicacion.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeEstructura_ubicacion.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeEstructura_ubicacion.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeEstructura_ubicacion.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeEstructura_ubicacion.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeEstructura_ubicacion.Ancho))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeEstructura_ubicacion.Largo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeEstructura_ubicacion.Alto))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeEstructura_ubicacion.Nivel))
            cmd.Parameters.Add(New SqlParameter("@INDICE_X", oBeEstructura_ubicacion.Indice_x))
            cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeEstructura_ubicacion.IdIndiceRotacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeEstructura_ubicacion.IdTipoRotacion))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeEstructura_ubicacion.Sistema))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeEstructura_ubicacion.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA2", oBeEstructura_ubicacion.Codigo_barra2))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeEstructura_ubicacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeEstructura_ubicacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeEstructura_ubicacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeEstructura_ubicacion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO", oBeEstructura_ubicacion.Dañado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeEstructura_ubicacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEADA", oBeEstructura_ubicacion.Bloqueada))
            cmd.Parameters.Add(New SqlParameter("@ACEPTA_PALLET", oBeEstructura_ubicacion.Acepta_pallet))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_PICKING", oBeEstructura_ubicacion.Ubicacion_picking))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_RECEPCION", oBeEstructura_ubicacion.Ubicacion_recepcion))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_DESPACHO", oBeEstructura_ubicacion.Ubicacion_despacho))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_MERMA", oBeEstructura_ubicacion.Ubicacion_merma))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeEstructura_ubicacion.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeEstructura_ubicacion.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeEstructura_ubicacion.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeEstructura_ubicacion.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION_POS", oBeEstructura_ubicacion.Orientacion_pos))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

            oBeEstructura_ubicacion.IdUbicacion = CInt(cmd.Parameters("@IDUBICACION").Value)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Async Function Insertar_Async(ByVal oBeEstructura_ubicacion As clsBeEstructura_ubicacion,
                                          Optional ByVal pConection As SqlConnection = Nothing,
                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Threading.Tasks.Task(Of Integer)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("estructura_ubicacion")
            Ins.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Ins.Add("idtramo", "@idtramo", DataType.Parametro)
            Ins.Add("idsector", "@idsector", DataType.Parametro)
            Ins.Add("idarea", "@idarea", DataType.Parametro)
            Ins.Add("idbodega", "@idbodega", DataType.Parametro)
            Ins.Add("descripcion", "@descripcion", DataType.Parametro)
            Ins.Add("ancho", "@ancho", DataType.Parametro)
            Ins.Add("largo", "@largo", DataType.Parametro)
            Ins.Add("alto", "@alto", DataType.Parametro)
            Ins.Add("nivel", "@nivel", DataType.Parametro)
            Ins.Add("indice_x", "@indice_x", DataType.Parametro)
            Ins.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
            Ins.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
            Ins.Add("sistema", "@sistema", DataType.Parametro)
            Ins.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Ins.Add("codigo_barra2", "@codigo_barra2", DataType.Parametro)
            Ins.Add("user_agr", "@user_agr", DataType.Parametro)
            Ins.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Ins.Add("user_mod", "@user_mod", DataType.Parametro)
            Ins.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Ins.Add("dañado", "@dañado", DataType.Parametro)
            Ins.Add("activo", "@activo", DataType.Parametro)
            Ins.Add("bloqueada", "@bloqueada", DataType.Parametro)
            Ins.Add("acepta_pallet", "@acepta_pallet", DataType.Parametro)
            Ins.Add("ubicacion_picking", "@ubicacion_picking", DataType.Parametro)
            Ins.Add("ubicacion_recepcion", "@ubicacion_recepcion", DataType.Parametro)
            Ins.Add("ubicacion_despacho", "@ubicacion_despacho", DataType.Parametro)
            Ins.Add("ubicacion_merma", "@ubicacion_merma", DataType.Parametro)
            Ins.Add("margen_izquierdo", "@margen_izquierdo", DataType.Parametro)
            Ins.Add("margen_derecho", "@margen_derecho", DataType.Parametro)
            Ins.Add("margen_superior", "@margen_superior", DataType.Parametro)
            Ins.Add("margen_inferior", "@margen_inferior", DataType.Parametro)
            Ins.Add("orientacion_pos", "@orientacion_pos", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                Await lConnection.OpenAsync() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeEstructura_ubicacion.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeEstructura_ubicacion.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeEstructura_ubicacion.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeEstructura_ubicacion.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeEstructura_ubicacion.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeEstructura_ubicacion.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeEstructura_ubicacion.Ancho))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeEstructura_ubicacion.Largo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeEstructura_ubicacion.Alto))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeEstructura_ubicacion.Nivel))
            cmd.Parameters.Add(New SqlParameter("@INDICE_X", oBeEstructura_ubicacion.Indice_x))
            cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeEstructura_ubicacion.IdIndiceRotacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeEstructura_ubicacion.IdTipoRotacion))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeEstructura_ubicacion.Sistema))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeEstructura_ubicacion.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA2", oBeEstructura_ubicacion.Codigo_barra2))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeEstructura_ubicacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeEstructura_ubicacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeEstructura_ubicacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeEstructura_ubicacion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO", oBeEstructura_ubicacion.Dañado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeEstructura_ubicacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEADA", oBeEstructura_ubicacion.Bloqueada))
            cmd.Parameters.Add(New SqlParameter("@ACEPTA_PALLET", oBeEstructura_ubicacion.Acepta_pallet))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_PICKING", oBeEstructura_ubicacion.Ubicacion_picking))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_RECEPCION", oBeEstructura_ubicacion.Ubicacion_recepcion))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_DESPACHO", oBeEstructura_ubicacion.Ubicacion_despacho))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_MERMA", oBeEstructura_ubicacion.Ubicacion_merma))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeEstructura_ubicacion.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeEstructura_ubicacion.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeEstructura_ubicacion.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeEstructura_ubicacion.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION_POS", oBeEstructura_ubicacion.Orientacion_pos))

            Dim rowsAffected As Task(Of Integer)

            rowsAffected = cmd.ExecuteNonQueryAsync()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected.Result

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar(ByRef oBeEstructura_ubicacion As clsBeEstructura_ubicacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("estructura_ubicacion")
            Upd.Add("idubicacion", "@idubicacion", DataType.Parametro)
            Upd.Add("idtramo", "@idtramo", DataType.Parametro)
            Upd.Add("idsector", "@idsector", DataType.Parametro)
            Upd.Add("idarea", "@idarea", DataType.Parametro)
            Upd.Add("idbodega", "@idbodega", DataType.Parametro)
            Upd.Add("descripcion", "@descripcion", DataType.Parametro)
            Upd.Add("ancho", "@ancho", DataType.Parametro)
            Upd.Add("largo", "@largo", DataType.Parametro)
            Upd.Add("alto", "@alto", DataType.Parametro)
            Upd.Add("nivel", "@nivel", DataType.Parametro)
            Upd.Add("indice_x", "@indice_x", DataType.Parametro)
            Upd.Add("idindicerotacion", "@idindicerotacion", DataType.Parametro)
            Upd.Add("idtiporotacion", "@idtiporotacion", DataType.Parametro)
            Upd.Add("sistema", "@sistema", DataType.Parametro)
            Upd.Add("codigo_barra", "@codigo_barra", DataType.Parametro)
            Upd.Add("codigo_barra2", "@codigo_barra2", DataType.Parametro)
            Upd.Add("user_agr", "@user_agr", DataType.Parametro)
            Upd.Add("fec_agr", "@fec_agr", DataType.Parametro)
            Upd.Add("user_mod", "@user_mod", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("dañado", "@dañado", DataType.Parametro)
            Upd.Add("activo", "@activo", DataType.Parametro)
            Upd.Add("bloqueada", "@bloqueada", DataType.Parametro)
            Upd.Add("acepta_pallet", "@acepta_pallet", DataType.Parametro)
            Upd.Add("ubicacion_picking", "@ubicacion_picking", DataType.Parametro)
            Upd.Add("ubicacion_recepcion", "@ubicacion_recepcion", DataType.Parametro)
            Upd.Add("ubicacion_despacho", "@ubicacion_despacho", DataType.Parametro)
            Upd.Add("ubicacion_merma", "@ubicacion_merma", DataType.Parametro)
            Upd.Add("margen_izquierdo", "@margen_izquierdo", DataType.Parametro)
            Upd.Add("margen_derecho", "@margen_derecho", DataType.Parametro)
            Upd.Add("margen_superior", "@margen_superior", DataType.Parametro)
            Upd.Add("margen_inferior", "@margen_inferior", DataType.Parametro)
            Upd.Add("orientacion_pos", "@orientacion_pos", DataType.Parametro)
            Upd.Where("IdUbicacion = @IdUbicacion")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeEstructura_ubicacion.IdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IDTRAMO", oBeEstructura_ubicacion.IdTramo))
            cmd.Parameters.Add(New SqlParameter("@IDSECTOR", oBeEstructura_ubicacion.IdSector))
            cmd.Parameters.Add(New SqlParameter("@IDAREA", oBeEstructura_ubicacion.IdArea))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeEstructura_ubicacion.IdBodega))
            cmd.Parameters.Add(New SqlParameter("@DESCRIPCION", oBeEstructura_ubicacion.Descripcion))
            cmd.Parameters.Add(New SqlParameter("@ANCHO", oBeEstructura_ubicacion.Ancho))
            cmd.Parameters.Add(New SqlParameter("@LARGO", oBeEstructura_ubicacion.Largo))
            cmd.Parameters.Add(New SqlParameter("@ALTO", oBeEstructura_ubicacion.Alto))
            cmd.Parameters.Add(New SqlParameter("@NIVEL", oBeEstructura_ubicacion.Nivel))
            cmd.Parameters.Add(New SqlParameter("@INDICE_X", oBeEstructura_ubicacion.Indice_x))
            cmd.Parameters.Add(New SqlParameter("@IDINDICEROTACION", oBeEstructura_ubicacion.IdIndiceRotacion))
            cmd.Parameters.Add(New SqlParameter("@IDTIPOROTACION", oBeEstructura_ubicacion.IdTipoRotacion))
            cmd.Parameters.Add(New SqlParameter("@SISTEMA", oBeEstructura_ubicacion.Sistema))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeEstructura_ubicacion.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA2", oBeEstructura_ubicacion.Codigo_barra2))
            cmd.Parameters.Add(New SqlParameter("@USER_AGR", oBeEstructura_ubicacion.User_agr))
            cmd.Parameters.Add(New SqlParameter("@FEC_AGR", oBeEstructura_ubicacion.Fec_agr))
            cmd.Parameters.Add(New SqlParameter("@USER_MOD", oBeEstructura_ubicacion.User_mod))
            cmd.Parameters.Add(New SqlParameter("@FEC_MOD", oBeEstructura_ubicacion.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@DAÑADO", oBeEstructura_ubicacion.Dañado))
            cmd.Parameters.Add(New SqlParameter("@ACTIVO", oBeEstructura_ubicacion.Activo))
            cmd.Parameters.Add(New SqlParameter("@BLOQUEADA", oBeEstructura_ubicacion.Bloqueada))
            cmd.Parameters.Add(New SqlParameter("@ACEPTA_PALLET", oBeEstructura_ubicacion.Acepta_pallet))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_PICKING", oBeEstructura_ubicacion.Ubicacion_picking))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_RECEPCION", oBeEstructura_ubicacion.Ubicacion_recepcion))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_DESPACHO", oBeEstructura_ubicacion.Ubicacion_despacho))
            cmd.Parameters.Add(New SqlParameter("@UBICACION_MERMA", oBeEstructura_ubicacion.Ubicacion_merma))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_IZQUIERDO", oBeEstructura_ubicacion.Margen_izquierdo))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_DERECHO", oBeEstructura_ubicacion.Margen_derecho))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_SUPERIOR", oBeEstructura_ubicacion.Margen_superior))
            cmd.Parameters.Add(New SqlParameter("@MARGEN_INFERIOR", oBeEstructura_ubicacion.Margen_inferior))
            cmd.Parameters.Add(New SqlParameter("@ORIENTACION_POS", oBeEstructura_ubicacion.Orientacion_pos))

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

    Public Shared Function Eliminar(ByRef oBeEstructura_ubicacion As clsBeEstructura_ubicacion, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Estructura_ubicacion" &
             "  Where(IdUbicacion = @IdUbicacion)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDUBICACION", oBeEstructura_ubicacion.IdUbicacion))

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

    Public Shared Function EliminarTodos(Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Estructura_ubicacion "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

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

    Public Shared Function Listar() As DataTable

        Try

            Const sp As String = "SELECT * FROM Estructura_ubicacion"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeEstructura_ubicacion As clsBeEstructura_ubicacion) As Boolean

        Try

            Const sp As String = "SELECT * FROM Estructura_ubicacion" &
            " Where(IdUbicacion = @IdUbicacion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", oBeEstructura_ubicacion.IdUbicacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeEstructura_ubicacion, dt.Rows(0))
            Else
                Throw New Exception("No se pudo obtener el registro")
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeEstructura_ubicacion)

        Try

            Dim lReturnList As New List(Of clsBeEstructura_ubicacion)
            Const sp As String = "SELECT * FROM Estructura_ubicacion "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeEstructura_ubicacion As New clsBeEstructura_ubicacion

            For Each dr As DataRow In dt.Rows
                vBeEstructura_ubicacion = New clsBeEstructura_ubicacion
                Cargar(vBeEstructura_ubicacion, dr)
                lReturnList.Add(vBeEstructura_ubicacion)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function Get_All_By_IdTramo(ByVal pIdTramo As Integer) As List(Of clsBeEstructura_ubicacion)

        Try

            Dim lReturnList As New List(Of clsBeEstructura_ubicacion)
            Const sp As String = "SELECT * FROM Estructura_ubicacion WHERE IdTramo = @IdTramo "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeEstructura_ubicacion As New clsBeEstructura_ubicacion

            For Each dr As DataRow In dt.Rows
                vBeEstructura_ubicacion = New clsBeEstructura_ubicacion
                Cargar(vBeEstructura_ubicacion, dr)
                lReturnList.Add(vBeEstructura_ubicacion)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdTramo(ByVal pIdTramo As Integer,
                                              ByVal lConnection As SqlConnection,
                                              ByVal lTransaction As SqlTransaction) As List(Of clsBeEstructura_ubicacion)

        Try

            Dim lReturnList As New List(Of clsBeEstructura_ubicacion)
            Const sp As String = "SELECT * FROM Estructura_ubicacion WHERE IdTramo = @IdTramo "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeEstructura_ubicacion As New clsBeEstructura_ubicacion

            For Each dr As DataRow In dt.Rows
                vBeEstructura_ubicacion = New clsBeEstructura_ubicacion
                Cargar(vBeEstructura_ubicacion, dr)
                lReturnList.Add(vBeEstructura_ubicacion)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeEstructura_ubicacion As clsBeEstructura_ubicacion)

        Try

            Const sp As String = "SELECT * FROM Estructura_ubicacion" &
            " Where(IdUbicacion = @IdUbicacion)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDUBICACION", pBeEstructura_ubicacion.IdUbicacion))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeEstructura_ubicacion, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdUbicacion),0) FROM Estructura_ubicacion"

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

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdUbicacion),0) FROM Estructura_ubicacion"

            Using lCommand As New SqlCommand(sp, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

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

End Class
