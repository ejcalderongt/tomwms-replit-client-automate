Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors

Partial Public Class clsLnTrans_ubic_hh_enc

    Public Shared Function Get_All_Filtro(ByVal pActivo As Boolean,
                                          ByVal pFechaInicio As Date,
                                          ByVal pFechaFin As Date,
                                          ByVal pEsCambioEstado As Boolean) As List(Of clsBeTrans_ubic_hh_enc)

        Dim lReturnList As New List(Of clsBeTrans_ubic_hh_enc)

        Try

            Dim vSQL As String = String.Format("SELECT * from VW_TransUbicacionHhEnc 
                                                WHERE cambio_estado = @cambio_estado And cast(FechaInicio as Date) >={0} and cast(FechaFin as Date) <={1} ",
                                                FormatoFechas.fFecha(pFechaInicio), FormatoFechas.fFecha(pFechaFin))

            If pActivo Then
                vSQL += " AND Activo=1"
            Else
                vSQL += " AND Activo=0"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@cambio_estado", IIf(pEsCambioEstado, 1, 0))

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_ubic_hh_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_ubic_hh_enc
                                Cargar(Obj, lRow)

                                If lRow("DescripcionMotivo") IsNot DBNull.Value AndAlso lRow("DescripcionMotivo") IsNot Nothing Then
                                    Obj.DescripcionMotivo = CType(lRow("DescripcionMotivo"), String)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

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

    Public Shared Function GetAll() As List(Of clsBeTrans_ubic_hh_enc)

        Dim lReturnList As New List(Of clsBeTrans_ubic_hh_enc)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT * from trans_ubic_hh_enc where 1 > 0 AND activo=1 "

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeTrans_ubic_hh_enc

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            Obj = New clsBeTrans_ubic_hh_enc
                            Cargar(Obj, lRow)
                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Pendientes_By_IdBodega_And_IdOperador(pIdBodega As Integer,
                                                                         pIdOperador As Integer,
                                                                         pIdTarea As Integer,
                                                                         ByVal Es_Cambio_Estado As Boolean) As List(Of clsBeTrans_ubic_hh_enc)

        Dim lReturnList As New List(Of clsBeTrans_ubic_hh_enc)
        Dim cambioest As Integer = IIf(Es_Cambio_Estado, 1, 0)

        Try

            '#CKFK 20180213_09:38pm: Cambié el formato de la consulta GetAllPendientesByOperador en clsLnTrans_ubic_hh_enc
            'GT17122021: Agregue hh_op para filtrar las tareas por operador
            'GT29122021: Agregue el left join a hh_op porque no siempre tiene registros
            Dim vSQL As String = "SELECT DISTINCT trans_ubic_hh_enc.IdTareaUbicacionEnc, trans_ubic_hh_enc.IdPropietarioBodega, trans_ubic_hh_enc.IdMotivoUbicacion, 
                                    motivo_ubicacion.Nombre AS DescripcionMotivo,trans_ubic_hh_enc.FechaInicio, trans_ubic_hh_enc.HoraInicio, 
                                    trans_ubic_hh_enc.FechaFin, trans_ubic_hh_enc.HoraFin, trans_ubic_hh_enc.user_agr, trans_ubic_hh_enc.fec_agr, 
                                    trans_ubic_hh_enc.user_mod, trans_ubic_hh_enc.fec_mod, trans_ubic_hh_enc.Observacion, trans_ubic_hh_enc.activo, 
                                    trans_ubic_hh_enc.operador_por_linea, trans_ubic_hh_enc.ubicacion_con_hh, 
                                    trans_ubic_hh_enc.estado, trans_ubic_hh_enc.IdReabastecimientoLog
                                    FROM  trans_ubic_hh_enc INNER JOIN
                                    motivo_ubicacion ON trans_ubic_hh_enc.IdMotivoUbicacion = motivo_ubicacion.IdMotivoUbicacion INNER JOIN
                                    trans_ubic_hh_det ON trans_ubic_hh_enc.IdTareaUbicacionEnc = trans_ubic_hh_det.IdTareaUbicacionEnc 
                                    --INNER JOIN
                                    LEFT OUTER JOIN
                                    trans_ubic_hh_op AS hh_op ON trans_ubic_hh_enc.IdTareaUbicacionEnc = hh_op.IdTareaUbicacionEnc
                                    Where (trans_ubic_hh_enc.activo = 1) And (trans_ubic_hh_enc.estado = 'NUEVO' OR trans_ubic_hh_enc.estado = 'PENDIENTE')  
                                    And (trans_ubic_hh_enc.cambio_estado = @cambio_estado)  "

            If pIdOperador > 0 Then
                'GT17122021: se agrega hh_op para validar tareas por operadorbodega, en hh_det no existe el operador
                'vSQL &= " AND (trans_ubic_hh_det.IdOperador = @IdOperador )"
                vSQL &= " AND (trans_ubic_hh_det.IdOperadorBodega = @IdOperador OR hh_op.IdOperadorBodega = @IdOperador) "

            End If

            If pIdTarea > 0 Then
                vSQL &= "And (trans_ubic_hh_enc.IdTareaUbicacionEnc=@IdTareaUbicacionEnc )"
            End If

            vSQL &= String.Format("And (trans_ubic_hh_enc.IdPropietarioBodega  IN (SELECT IdPropietarioBodega  FROM dbo.propietario_bodega WHERE (IdBodega ={0}))) ", pIdBodega)

            vSQL &= "Order By trans_ubic_hh_enc.IdTareaUbicacionEnc "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@cambio_estado", cambioest)
                        If pIdOperador > 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdOperador", pIdOperador)
                        If pIdTarea > 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", pIdTarea)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTrans_ubic_hh_enc

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTrans_ubic_hh_enc
                                CargarMot(Obj, lRow)
                                lReturnList.Add(Obj)

                            Next

                        End If

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

    Public Shared Sub CargarMot(ByRef oBeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc, ByRef dr As DataRow)

        Try

            With oBeTrans_ubic_hh_enc

                .IdTareaUbicacionEnc = IIf(IsDBNull(dr.Item("IdTareaUbicacionEnc")), 0, dr.Item("IdTareaUbicacionEnc"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdMotivoUbicacion = IIf(IsDBNull(dr.Item("IdMotivoUbicacion")), 0, dr.Item("IdMotivoUbicacion"))
                .FechaInicio = IIf(IsDBNull(dr.Item("FechaInicio")), Date.Now, dr.Item("FechaInicio"))
                .HoraInicio = IIf(IsDBNull(dr.Item("HoraInicio")), Date.Now, dr.Item("HoraInicio"))
                .FechaFin = IIf(IsDBNull(dr.Item("FechaFin")), Date.Now, dr.Item("FechaFin"))
                .HoraFin = IIf(IsDBNull(dr.Item("HoraFin")), Date.Now, dr.Item("HoraFin"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Observacion = IIf(IsDBNull(dr.Item("Observacion")), "", dr.Item("Observacion"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Operador_por_linea = IIf(IsDBNull(dr.Item("operador_por_linea")), False, dr.Item("operador_por_linea"))
                .Ubicacion_con_hh = IIf(IsDBNull(dr.Item("ubicacion_con_hh")), False, dr.Item("ubicacion_con_hh"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .DescripcionMotivo = IIf(IsDBNull(dr.Item("DescripcionMotivo")), "", dr.Item("DescripcionMotivo"))
                .IdReabastecimientoLog = IIf(IsDBNull(dr.Item("IdReabastecimientoLog")), 0, dr.Item("IdReabastecimientoLog"))

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function GetSingle(ByVal IdTareaUbicacionEnc As Integer) As clsBeTrans_ubic_hh_enc

        GetSingle = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM trans_ubic_hh_enc WHERE IdTareaUbicacionEnc=@IdTareaUbicacionEnc"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", IdTareaUbicacionEnc)

                        Dim lDT As New DataTable()
                        lDTA.Fill(lDT)

                        If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDT.Rows(0)
                            Dim Obj As New clsBeTrans_ubic_hh_enc()

                            Cargar(Obj, lRow)

                            Return Obj

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

    Public Shared Function Exists(ByVal pIdTareaUbicacionEnc As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "SELECT COUNT(1) FROM trans_ubic_hh_enc WHERE IdTareaUbicacionEnc=@IdTareaUbicacionEnc"

                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}

                    lCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", pIdTareaUbicacionEnc)

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Trae las dimensiones de cada producto, los productos se filtran por la ubicacion
    ''' </summary>
    ''' <param name="pIdUbicacion">IdUbicacion</param>
    ''' <returns></returns>
    ''' <remarks>Bcuscul06072016</remarks>
    Public Shared Function Get_Dimensiones_Productos(ByRef pIdUbicacion As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "SELECT * from VW_ProductoDimension where idUbicacion=@idUbicacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@idUbicacion", pIdUbicacion)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Dim Obj As New clsBeVW_stock_res()
                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
                                lReturnList.Add(Obj)

                            Next

                        End If

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

    Private Shared Sub Guarda_Trans_Ubic_HH_Enc(ByRef BeTransUbicHHEnc As clsBeTrans_ubic_hh_enc,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)

        Try

            If BeTransUbicHHEnc.IdTareaUbicacionEnc = 0 Then
                BeTransUbicHHEnc.IdTareaUbicacionEnc = MaxID(lConnection, lTransaction) + 1
                Insertar(BeTransUbicHHEnc, lConnection, lTransaction)
            Else
                Actualizar(BeTransUbicHHEnc, lConnection, lTransaction)
            End If

        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdBodega:=BeTransUbicHHEnc.IdBodega,
                                                  pIdTareaUbicacionEnc:=BeTransUbicHHEnc.IdTareaUbicacionEnc,
                                                  pIdMotivoUbicacion:=BeTransUbicHHEnc.IdMotivoUbicacion)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Guardar_Transaccion(ByRef BeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc,
                                          ByVal pListObjDet As List(Of clsBeTrans_ubic_hh_det),
                                          ByVal pListObjOp As List(Of clsBeTrans_ubic_hh_op),
                                          ByVal pListObjMov As List(Of clsBeTrans_movimientos),
                                          ByVal Con_HH As Boolean,
                                          ByVal pIdPropietario As Integer,
                                          ByVal pListObjStock As List(Of clsBeStock),
                                          ByVal pListObjTransUbicTarimaDisponibles As List(Of clsBeTrans_ubic_tarima),
                                          ByVal pListObjTransUbicTarimasUsadas As List(Of clsBeTrans_ubic_tarima),
                                          ByVal pIdTareaHH As Integer,
                                          ByVal pHostSolicita As String)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Guarda_Trans_Ubic_HH_Enc(BeTrans_ubic_hh_enc, lConnection, lTransaction)

            If BeTrans_ubic_hh_enc.IdReabastecimientoLog > 0 Then

                '#EJC20240624: Marcar reabasto como procesado si se ejecuta por BOF.
                Dim BeTransReabastecimientoLog As New clsBeTrans_reabastecimiento_log
                BeTransReabastecimientoLog = clsLnTrans_reabastecimiento_log.GetSingle(BeTrans_ubic_hh_enc.IdReabastecimientoLog, lConnection, lTransaction)

                If Not BeTransReabastecimientoLog Is Nothing Then

                    BeTransReabastecimientoLog.Procesado_HH = True
                    BeTransReabastecimientoLog.Fecha_Procesamiento_BOF = Now
                    BeTransReabastecimientoLog.Hora_Procesamiento_BOF = Now
                    BeTransReabastecimientoLog.IdTareaUbicacionEnc = BeTrans_ubic_hh_enc.IdTareaUbicacionEnc

                    If clsLnTrans_reabastecimiento_log.Actualizar_Procesamiento_BOF(BeTransReabastecimientoLog,
                                                                                    lConnection,
                                                                                    lTransaction) Then


                    End If

                End If

            End If

            clsLnTrans_ubic_hh_det.Guardar_Trans_Ubic_HH_Det(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pListObjDet, lConnection, lTransaction)
            clsLnTrans_ubic_hh_stock.Guardar_Trans_Ubic_HH_Stock(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pListObjStock, pListObjDet, lConnection, lTransaction)

            If Con_HH Then
                clsLnTarea_hh.Guardar_Tarea_Ubicacion_HH(BeTrans_ubic_hh_enc, pIdPropietario, lConnection, lTransaction)
                clsLnStock_res.Guardar_Stock_Res(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pListObjStock, pHostSolicita, lConnection, lTransaction)
            Else
                '#EJC20171016_0228PM: Cuando se cambia la transacción a BOF, eliminar tarea de la HH.
                clsLnTarea_hh.Eliminar_By_IdTareaHH(pIdTareaHH, lConnection, lTransaction)
                '#EJC20171018_0636PM: Si la transacción era antes con HH y se cambio a BOF eliminar el stock reservado.
                '#CKFK20240624 Si la tarea es de tipo reabasto el tipo de transaccion es diferente
                If BeTrans_ubic_hh_enc.IdReabastecimientoLog > 0 Then
                    clsLnStock_res.Eliminar_Stock_Res_Reabasto(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, lConnection, lTransaction)
                Else
                    clsLnStock_res.Eliminar_Stock_Res_Ubic(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, lConnection, lTransaction)
                End If
                clsLnStock.Actualizar_Stock_Por_Cambio_de_Ubicacion(pListObjStock, pListObjDet, (BeTrans_ubic_hh_enc.IdReabastecimientoLog <> 0), lConnection, lTransaction)
            End If

            'GT20122021: envio el pObjEnc, con IsNew para insert o update
            'clsLnTrans_ubic_hh_op.Guarda_Operadores(pObjEnc.IdTareaUbicacionEnc, pListObjOp, lConnection, lTransaction)
            clsLnTrans_ubic_hh_op.Guarda_Operadores_By_Enc(BeTrans_ubic_hh_enc, pListObjOp, lConnection, lTransaction)
            If Not Con_HH Then clsLnTrans_movimientos.Guardar_Movimientos(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pListObjMov, lConnection, lTransaction)
            clsLnTrans_ubic_tarima.Guardar_Tarimas_Usadas(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pListObjTransUbicTarimasUsadas, lConnection, lTransaction)
            clsLnTrans_ubic_tarima.Guardar_Tarimas_Disponibles(pListObjTransUbicTarimaDisponibles, lConnection, lTransaction)

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Sub Guardar_Transaccion_Por_Picking_Process(ByVal TransUbicHHEnc As clsBeTrans_ubic_hh_enc,
                                                              ByVal pListObjDet As List(Of clsBeTrans_ubic_hh_det),
                                                              ByVal pListObjOp As List(Of clsBeTrans_ubic_hh_op),
                                                              ByVal pListObjMov As List(Of clsBeTrans_movimientos),
                                                              ByVal Con_HH As Boolean,
                                                              ByVal pIdPropietario As Integer,
                                                              ByVal pListObjStock As List(Of clsBeStock),
                                                              ByVal pListObjTransUbicTarimaDisponibles As List(Of clsBeTrans_ubic_tarima),
                                                              ByVal pListObjTransUbicTarimasUsadas As List(Of clsBeTrans_ubic_tarima),
                                                              ByVal pIdTareaHH As Integer,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction,
                                                              ByVal pHostSolicita As String)

        Try

            Guarda_Trans_Ubic_HH_Enc(TransUbicHHEnc,
                                     lConnection,
                                     lTransaction)

            clsLnTrans_ubic_hh_det.Guardar_Trans_Ubic_HH_Det(TransUbicHHEnc.IdTareaUbicacionEnc,
                                                             pListObjDet,
                                                             lConnection,
                                                             lTransaction)

            Dim Origen, Anterior As Integer

            Origen = pListObjStock(0).IdUbicacion
            Anterior = pListObjStock(0).IdUbicacion_anterior

            pListObjStock(0).IdUbicacion = pListObjDet(0).IdUbicacionDestino
            pListObjStock(0).IdUbicacion_anterior = pListObjDet(0).IdUbicacionOrigen

            clsLnTrans_ubic_hh_stock.Guardar_Trans_Ubic_HH_Stock(TransUbicHHEnc.IdTareaUbicacionEnc,
                                                                 pListObjStock,
                                                                 pListObjDet,
                                                                 lConnection,
                                                                 lTransaction)

            pListObjStock(0).IdUbicacion = Origen
            pListObjStock(0).IdUbicacion_anterior = Anterior

            If Con_HH Then

                clsLnTarea_hh.Guardar_Tarea_Ubicacion_HH(TransUbicHHEnc, pIdPropietario, lConnection, lTransaction)
                clsLnStock_res.Guardar_Stock_Res(TransUbicHHEnc.IdTareaUbicacionEnc, pListObjStock, pHostSolicita, lConnection, lTransaction)

            Else
                '#EJC20171016_0228PM: Cuando se cambia la transacción a BOF, eliminar tarea de la HH.
                clsLnTarea_hh.Eliminar_By_IdTareaHH(pIdTareaHH, lConnection, lTransaction)
                '#EJC20171018_0636PM: Si la transacción era antes con HH y se cambio a BOF eliminar el stock reservado.
                clsLnStock_res.Eliminar_Stock_Res_Ubic(TransUbicHHEnc.IdTareaUbicacionEnc, lConnection, lTransaction)
                '#EJC20181211: Insertar el stock_res cuando el IdStock origen y destino es el mismo ;)!
                clsLnStock_res.Guardar_Stock_Res(TransUbicHHEnc.IdTareaUbicacionEnc, pListObjStock, pHostSolicita, lConnection, lTransaction)
                clsLnStock.Actualizar_Stock_Por_Cambio_de_Ubicacion(pListObjStock, pListObjDet, (TransUbicHHEnc.IdReabastecimientoLog <> 0), lConnection, lTransaction)
            End If

            clsLnTrans_ubic_hh_op.Guarda_Operadores(TransUbicHHEnc.IdTareaUbicacionEnc, pListObjOp, lConnection, lTransaction)
            '#CKFK 20180422: El movimiento solo se guarda si el cambio de ubicación no es con HH
            If Not Con_HH Then clsLnTrans_movimientos.Guardar_Movimientos(TransUbicHHEnc.IdTareaUbicacionEnc, pListObjMov, lConnection, lTransaction)
            clsLnTrans_ubic_tarima.Guardar_Tarimas_Usadas(TransUbicHHEnc.IdTareaUbicacionEnc, pListObjTransUbicTarimasUsadas, lConnection, lTransaction)
            clsLnTrans_ubic_tarima.Guardar_Tarimas_Disponibles(pListObjTransUbicTarimaDisponibles, lConnection, lTransaction)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    '#CKFK20220208 Recuperada
    Public Shared Sub Guardar_Transaccion_Por_Picking_Process(ByVal pObjEnc As clsBeTrans_ubic_hh_enc,
                                                              ByVal pListObjDet As List(Of clsBeTrans_ubic_hh_det),
                                                              ByVal pListObjOp As List(Of clsBeTrans_ubic_hh_op),
                                                              ByVal pListObjMov As List(Of clsBeTrans_movimientos),
                                                              ByVal Con_HH As Boolean,
                                                              ByVal pIdPropietario As Integer,
                                                              ByVal pListObjStock As List(Of clsBeStock),
                                                              ByVal pListObjTransUbicTarimaDisponibles As List(Of clsBeTrans_ubic_tarima),
                                                              ByVal pListObjTransUbicTarimasUsadas As List(Of clsBeTrans_ubic_tarima),
                                                              ByVal pIdTareaHH As Integer,
                                                              ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction)

        Try

            Guarda_Trans_Ubic_HH_Enc(pObjEnc, lConnection, lTransaction)

            clsLnTrans_ubic_hh_det.Guardar_Trans_Ubic_HH_Det(pObjEnc.IdTareaUbicacionEnc, pListObjDet, lConnection, lTransaction)

            Dim Origen, Anterior As Integer

            Origen = pListObjStock(0).IdUbicacion
            Anterior = pListObjStock(0).IdUbicacion_anterior

            pListObjStock(0).IdUbicacion = pListObjDet(0).IdUbicacionDestino
            pListObjStock(0).IdUbicacion_anterior = pListObjDet(0).IdUbicacionOrigen

            clsLnTrans_ubic_hh_stock.Guardar_Trans_Ubic_HH_Stock(pObjEnc.IdTareaUbicacionEnc, pListObjStock, pListObjDet, lConnection, lTransaction)


            pListObjStock(0).IdUbicacion = Origen
            pListObjStock(0).IdUbicacion_anterior = Anterior

            If Con_HH Then

                clsLnTarea_hh.Guardar_Tarea_Ubicacion_HH(pObjEnc, pIdPropietario, lConnection, lTransaction)
                clsLnStock_res.Guardar_Stock_Res(pObjEnc.IdTareaUbicacionEnc, pListObjStock, "", lConnection, lTransaction)

            Else
                '#EJC20171016_0228PM: Cuando se cambia la transacción a BOF, eliminar tarea de la HH.
                clsLnTarea_hh.Eliminar_By_IdTareaHH(pIdTareaHH, lConnection, lTransaction)
                '#EJC20171018_0636PM: Si la transacción era antes con HH y se cambio a BOF eliminar el stock reservado.
                clsLnStock_res.Eliminar_Stock_Res_Ubic(pObjEnc.IdTareaUbicacionEnc, lConnection, lTransaction)
                '#EJC20181211: Insertar el stock_res cuando el IdStock origen y destino es el mismo ;)!
                clsLnStock_res.Guardar_Stock_Res(pObjEnc.IdTareaUbicacionEnc, pListObjStock, "", lConnection, lTransaction)
                clsLnStock.Actualizar_Stock_Por_Cambio_de_Ubicacion(pListObjStock, pListObjDet, False, lConnection, lTransaction)
            End If

            clsLnTrans_ubic_hh_op.Guarda_Operadores(pObjEnc.IdTareaUbicacionEnc, pListObjOp, lConnection, lTransaction)
            '#CKFK 20180422: El movimiento solo se guarda si el cambio de ubicación no es con HH
            If Not Con_HH Then clsLnTrans_movimientos.Guardar_Movimientos(pObjEnc.IdTareaUbicacionEnc, pListObjMov, lConnection, lTransaction)
            clsLnTrans_ubic_tarima.Guardar_Tarimas_Usadas(pObjEnc.IdTareaUbicacionEnc, pListObjTransUbicTarimasUsadas, lConnection, lTransaction)
            clsLnTrans_ubic_tarima.Guardar_Tarimas_Disponibles(pListObjTransUbicTarimaDisponibles, lConnection, lTransaction)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    '#CKFK 20180130 04:24 PM 
    'Procedimiento creado para generar tarea de cambio de estado por producto de reemplazo en picking o verificación
    Public Shared Sub Genera_Tarea_Cambio_Estado_Por_Producto_Dañado(ByVal IdBodega As Integer,
                                                                     ByVal IdEmpresa As Integer,
                                                                     ByVal IdStock As Integer,
                                                                     ByVal IdStockRes As Integer,
                                                                     ByVal UsuarioHH As Integer,
                                                                     ByVal CantDañada As Double,
                                                                     ByVal IdUbicDest As Integer,
                                                                     ByVal IdEstadoDest As Integer,
                                                                     ByVal IdPropietarioBodega As Integer,
                                                                     ByVal IdPickingUbic As Integer,
                                                                     ByVal EsPicking As Boolean,
                                                                     ByVal pHostSolicita As String,
                                                                     ByRef pConnection As SqlConnection,
                                                                     ByRef pTransaction As SqlTransaction)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim Factor As Double = 0
            Dim CantidadDañadaPresentacion As Double = 0

            Dim UbicaAuto As Boolean = False
            UbicaAuto = clsLnBodega.Get_Parametro_Cambio_Ubicacion_Auto(IdBodega, lConnection, lTransaction)

            Dim pBePropietarioBodega As New clsBePropietario_bodega
            pBePropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(IdPropietarioBodega, lConnection, lTransaction)

            'Quita la cantidad dañada del stock reservado y marca como dañado en picking_ubic
            Dim bePickingUbicExistente As New clsBeTrans_picking_ubic() With {.IdPickingUbic = IdPickingUbic}
            clsLnTrans_picking_ubic.GetSingle(bePickingUbicExistente, lConnection, lTransaction)

            Dim bePickingUbicNuevo As New clsBeTrans_picking_ubic() With {.IdPickingUbic = IdPickingUbic}
            Dim bePickingUbicExistenteDañado As New clsBeTrans_picking_ubic() With {.IdPickingUbic = IdPickingUbic}

            bePickingUbicNuevo = bePickingUbicExistente.Clone()
            bePickingUbicExistenteDañado = bePickingUbicExistente.Clone()

            Dim beStockRes As New clsBeStock_res() With {.IdStockRes = IdStockRes,
                                                         .IdProductoBodega = bePickingUbicExistente.IdProductoBodega}
            clsLnStock_res.GetSingle(beStockRes, lConnection, lTransaction)

            If beStockRes.IdPresentacion <> 0 Then
                Factor = 0
                Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(beStockRes.IdProductoBodega, beStockRes.IdPresentacion, lConnection, lTransaction)
                CantidadDañadaPresentacion = Math.Round(CantDañada * Factor, 6)
            End If

            If beStockRes.Cantidad = IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaPresentacion, CantDañada) Then

                bePickingUbicExistenteDañado.Encontrado = True
                bePickingUbicExistenteDañado.Acepto = False

                If Not EsPicking Then
                    bePickingUbicExistenteDañado.Cantidad_Recibida = CantDañada
                End If

                If EsPicking Then
                    bePickingUbicExistenteDañado.Dañado_picking = True
                Else
                    bePickingUbicExistenteDañado.Dañado_verificacion = True
                End If

                bePickingUbicExistenteDañado.IdPickingUbic = clsLnTrans_picking_ubic.MaxID(lConnection, lTransaction) + 1

                clsLnTrans_picking_ubic.Insertar(bePickingUbicExistenteDañado)

                'clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(IdStockRes, lConnection, lTransaction)

            ElseIf beStockRes.Cantidad > IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaPresentacion, CantDañada) Then

                '#EJC20181205 11:56 AM Se modificó comentario y renombró funciones.                
                bePickingUbicExistente.Cantidad_Solicitada -= CantDañada
                bePickingUbicNuevo.Cantidad_Solicitada = CantDañada

                If bePickingUbicExistente.Cantidad_Solicitada <> 0 Then

                    bePickingUbicExistenteDañado.Cantidad_Solicitada = CantDañada

                    bePickingUbicExistenteDañado.Encontrado = True
                    bePickingUbicExistenteDañado.Acepto = False

                    If Not EsPicking Then
                        bePickingUbicExistenteDañado.Cantidad_Recibida = CantDañada
                    End If

                    If EsPicking Then
                        bePickingUbicExistenteDañado.Dañado_picking = True
                    Else
                        bePickingUbicExistenteDañado.Dañado_verificacion = True
                    End If

                    bePickingUbicExistenteDañado.IdPickingUbic = clsLnTrans_picking_ubic.MaxID(lConnection, lTransaction) + 1

                    clsLnTrans_picking_ubic.Insertar(bePickingUbicExistenteDañado)

                ElseIf bePickingUbicExistente.Cantidad_Solicitada = 0 Then
                    bePickingUbicExistente.Cantidad_Solicitada = CantDañada
                End If

                If Not EsPicking Then

                    bePickingUbicNuevo.Cantidad_Recibida = 0

                    Dim BePickingEnc As New clsBeTrans_picking_enc
                    BePickingEnc.IdPickingEnc = clsLnTrans_picking_ubic.ObtienIdPickingEnc_Y_Verifica_Estado_PickingEnc_ByPickingUbic(IdPickingUbic, lConnection, lTransaction)

                    If BePickingEnc.IdPickingEnc <> 0 Then
                        BePickingEnc.Estado = "Pendiente"
                        clsLnTrans_picking_enc.Actualizar_Estado(BePickingEnc, lConnection, lTransaction)
                    End If

                End If

                If Not EsPicking Then
                    bePickingUbicExistente.Cantidad_Recibida -= CantDañada
                End If

                'clsLnTrans_picking_ubic.Actualizar(bePickingUbicExistente, lConnection, lTransaction)

                '#CKFK 20180220 11:03 PM 
                'Si la cantidad del picking_ubic es mayor a la cantidad dañada se crea un nuevo picking_ubic 
                'con esa cantidad y con el dañado picking en true
                bePickingUbicNuevo.IdPickingUbic = clsLnTrans_picking_ubic.MaxID(lConnection, lTransaction) + 1
                bePickingUbicNuevo.IsNew = True

                bePickingUbicNuevo.Encontrado = True

                If EsPicking Then
                    bePickingUbicNuevo.Dañado_picking = True
                Else
                    bePickingUbicNuevo.Dañado_verificacion = True
                End If


                '#EJC20181211: 1201: Copiar el Id antes de asignar el nuevo MaxId para actualizar el stockres.
                Dim vIdStockResOriginal As Integer = bePickingUbicNuevo.IdStockRes

                '#CKFK 20180501 Agregué el IdStockRes porque estaba guardando el anterior
                bePickingUbicExistente.IdStockRes = clsLnStock_res.MaxID(lConnection, lTransaction)
                clsLnTrans_picking_ubic.Insertar(bePickingUbicNuevo)

            Else
                Throw New Exception(String.Format("La cantidad disponible: {0} es menor a la cantidad dañada:{1} ", beStockRes.Cantidad, CantDañada))
            End If

            'Encabezado de ubicación con HH por cambio de estado
            Dim pIdMotivoUbicacion As Integer = clsLnBodega.Get_IdMotivoUbicacion_Dañado_Picking(IdBodega, lConnection, lTransaction)

            If pIdMotivoUbicacion = 0 Then Throw New Exception("No está definido IdMotivoUbicacion, no puede realizarse el reemplazo")

            Dim beUbicHHEnc As New clsBeTrans_ubic_hh_enc()
            Dim beTareaHH As New clsBeTarea_hh()

            If Not UbicaAuto Then

                beUbicHHEnc.IdPropietarioBodega = pBePropietarioBodega.IdPropietarioBodega
                beUbicHHEnc.IdMotivoUbicacion = pIdMotivoUbicacion
                beUbicHHEnc.FechaInicio = Now
                beUbicHHEnc.HoraInicio = Now
                beUbicHHEnc.FechaFin = Now
                beUbicHHEnc.HoraFin = Now
                beUbicHHEnc.Activo = True
                beUbicHHEnc.Observacion = "Cambio de estado por reemplazo de producto"
                beUbicHHEnc.User_agr = UsuarioHH
                beUbicHHEnc.Fec_agr = Now
                beUbicHHEnc.User_mod = UsuarioHH
                beUbicHHEnc.Fec_mod = Now
                beUbicHHEnc.Operador_por_linea = UsuarioHH
                beUbicHHEnc.Ubicacion_con_hh = True
                beUbicHHEnc.Cambio_estado = True
                beUbicHHEnc.Asunto = String.Format("{0}  {1}", pBePropietarioBodega.IdPropietario, "Reemplazo")
                beUbicHHEnc.IdPrioridad = 1 'Aquí es correcto que lo coloque con prioridad 3?
                beUbicHHEnc.IdTipoTarea = 3
                beUbicHHEnc.IdBodega = IdBodega
                beUbicHHEnc.Estado = "Nuevo"
                beUbicHHEnc.IsNew = True

                'Tarea de cambio de estado
                beTareaHH.IdTareahh = clsLnTarea_hh.MaxID(lConnection, lTransaction) + 1
                beTareaHH.IdPropietario = pBePropietarioBodega.IdPropietario
                beTareaHH.IdBodega = IdBodega
                beTareaHH.IdEstado = 1
                beTareaHH.Tipo = 0
                beTareaHH.FechaInicio = Now
                beTareaHH.FechaFin = Now
                beTareaHH.DiaCompleto = False
                beTareaHH.Ubicacion = ""
                beTareaHH.Descripcion = "Cambio de estado por reemplazo de producto"
                beTareaHH.Recordatorio = ""
                beTareaHH.Asunto = ""
                beTareaHH.IdPrioridad = 0
                beTareaHH.IdTipoTarea = 3
                beTareaHH.IdMuelle = Nothing

            End If

            'Stock que cambió de estado
            Dim pStock As New clsBeStock
            Dim pListStock As New List(Of clsBeStock)

            pStock = clsLnStock.GetSingle(IdStock, lConnection, lTransaction)

            '#CKFK 20190208 Guardé la presentación original del Stock porque se necesita para hacer el cambio de ubicación cuando es automático
            Dim vPresOrigStock As Integer = pStock.IdPresentacion

            pStock.IdPresentacion = bePickingUbicExistente.IdPresentacion

            If pStock.IdPresentacion <> 0 Then
                Factor = 0
                Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pStock.IdProductoBodega, pStock.IdPresentacion, lConnection, lTransaction)
                CantidadDañadaPresentacion = Math.Round(CantDañada * Factor, 6)
            End If

            'Detalle de ubicación con HH por cambio de estado
            Dim beListUbicHHDet As New List(Of clsBeTrans_ubic_hh_det)
            Dim beUbicHHDet As New clsBeTrans_ubic_hh_det()

            If Not UbicaAuto Then

                clsPublic.CopyObject(pStock, beUbicHHDet.Stock)

                beUbicHHDet.IdStock = IdStock
                beUbicHHDet.Producto = New clsBeProducto
                beUbicHHDet.Stock = New clsBeStock

                beUbicHHDet.IdTareaUbicacionDet = 0

                beUbicHHDet.ProductoEstado = New clsBeProducto_estado
                beUbicHHDet.ProductoPresentacion = New clsBeProducto_Presentacion
                beUbicHHDet.UnidadMedida = New clsBeUnidad_medida
                beUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion
                beUbicHHDet.Producto.Nombre = pStock.Producto.Nombre
                beUbicHHDet.Producto.Codigo = pStock.Producto.Codigo
                beUbicHHDet.Stock.IdUbicacion_anterior = pStock.IdUbicacion
                beUbicHHDet.IdUbicacionOrigen = pStock.IdUbicacion 'IdUbicDest
                beUbicHHDet.IdUbicacionDestino = IdUbicDest
                beUbicHHDet.Stock.Fecha_vence = pStock.Fecha_vence
                beUbicHHDet.Stock.Serial = pStock.Serial
                beUbicHHDet.IdEstadoOrigen = pStock.IdProductoEstado
                beUbicHHDet.IdEstadoDestino = IdEstadoDest
                beUbicHHDet.Cantidad = IIf(pStock.IdPresentacion <> 0, CantidadDañadaPresentacion, CantDañada)
                beUbicHHDet.Recibido = 0
                beUbicHHDet.Estado = "Pendiente"
                beUbicHHDet.Realizado = False
                beUbicHHDet.Operador = New clsBeOperador
                beUbicHHDet.IdOperadorBodega = UsuarioHH

                beUbicHHDet.Activo = True

                beListUbicHHDet.Add(beUbicHHDet)

            End If

            'Inicio de movimiento de cambio de ubicacion
            Dim pListMov As New List(Of clsBeTrans_movimientos)

            Dim pMov As New clsBeTrans_movimientos()
            pMov.IdEmpresa = IdEmpresa
            pMov.IdBodegaOrigen = IdBodega
            pMov.IdTransaccion = IIf(Not UbicaAuto, beUbicHHDet.IdTareaUbicacionEnc, 1)
            pMov.IdPropietarioBodega = pBePropietarioBodega.IdPropietarioBodega
            pMov.IdProductoBodega = pStock.IdProductoBodega
            pMov.IdUbicacionOrigen = pStock.IdUbicacion
            pMov.IdUbicacionDestino = IIf(Not UbicaAuto, beUbicHHDet.IdUbicacionDestino, IdUbicDest)
            pMov.IdPresentacion = pStock.IdPresentacion
            pMov.IdEstadoOrigen = pStock.IdProductoEstado
            pMov.IdEstadoDestino = IdEstadoDest
            pMov.IdUnidadMedida = pStock.IdUnidadMedida
            pMov.IdTipoTarea = 3
            pMov.IdBodegaDestino = IdBodega
            pMov.IdRecepcion = pStock.IdRecepcionEnc
            pMov.IdRecepcionDet = pStock.IdRecepcionDet
            pMov.Cantidad = IIf(Not UbicaAuto, beUbicHHDet.Cantidad, IIf(pStock.IdPresentacion <> 0, CantidadDañadaPresentacion, CantDañada))
            pMov.Serie = pStock.Serial
            pMov.Peso = pStock.Peso
            pMov.Lote = pStock.Lote
            pMov.Fecha_vence = pStock.Fecha_vence
            pMov.Fecha = Now
            pMov.Barra_pallet = pStock.Lic_plate
            pMov.Hora_ini = Now
            pMov.Hora_fin = Now
            pMov.Fecha_agr = Now
            pMov.Usuario_agr = UsuarioHH
            pMov.Cantidad_hist = pStock.Cantidad
            pMov.Peso_hist = pStock.Peso

            'Modificar campos en el stock
            pStock.Cantidad = CantDañada
            pStock.IdProductoEstado = IdEstadoDest
            pStock.ProductoEstado.IdEstado = IdEstadoDest
            'pStock.IdUbicacion = IdUbicDest '#CKFK 20180219 Agregué al stock que la ubicación origen es la ubicación destino cuando genero la tarea de cambio de estado

            pListMov.Add(pMov)

            pListStock.Add(pStock)

            If Not UbicaAuto Then

                Guardar_Transaccion_Por_Picking_Process(beUbicHHEnc,
                                                       beListUbicHHDet,
                                                       Nothing,
                                                       pListMov,
                                                       True,
                                                       pBePropietarioBodega.IdPropietario,
                                                       pListStock,
                                                       Nothing,
                                                       Nothing,
                                                       beTareaHH.IdTareahh,
                                                       lConnection,
                                                       lTransaction,
                                                       pHostSolicita)
            Else

                Dim vStockRes As New clsBeVW_stock_res

                vStockRes.IdProductoBodega = pStock.IdProductoBodega
                vStockRes.IdUbicacion = pMov.IdUbicacionOrigen
                vStockRes.Lote = pStock.Lote
                vStockRes.Fecha_Vence = pStock.Fecha_vence
                vStockRes.CantidadUmBas = IIf(pStock.IdPresentacion <> 0, CantidadDañadaPresentacion, pStock.Cantidad)
                vStockRes.Peso = pStock.Peso
                vStockRes.IdPresentacion = vPresOrigStock ' pStock.IdPresentacion #CKFK 20190208 Aquí voy a mandar la presentación original del stock y no la del picking ubic
                vStockRes.IdProductoEstado = pMov.IdEstadoOrigen
                vStockRes.Fecha_ingreso = Now
                vStockRes.ValorFecha = Now

                clsLnTrans_ubic_hh_det.Aplica_Cambio_Estado_Ubic_En_Picking(pMov, vStockRes, True, lConnection, lTransaction)

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Sub

    '#CKFK 20180130 04:24 PM -- Recuperada
    'Procedimiento creado para generar tarea de cambio de estado por producto de reemplazo en picking o verificación
    Public Shared Sub Genera_Tarea_Cambio_Estado_Por_Producto_Dañado(ByVal IdBodega As Integer,
                                                                      ByVal IdEmpresa As Integer,
                                                                      ByVal IdStock As Integer,
                                                                      ByVal IdStockRes As Integer,
                                                                      ByVal UsuarioHH As Integer,
                                                                      ByVal CantDañada As Double,
                                                                      ByVal IdUbicDest As Integer,
                                                                      ByVal IdEstadoDest As Integer,
                                                                      ByVal IdPropietarioBodega As Integer,
                                                                      ByVal IdPickingUbic As Integer,
                                                                      ByVal EsPicking As Boolean)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction()

            Dim Factor As Double = 0
            Dim CantidadDañadaPresentacion As Double = 0

            Dim UbicaAuto As Boolean = False
            UbicaAuto = clsLnBodega.Get_Parametro_Cambio_Ubicacion_Auto(IdBodega, lConnection, lTransaction)

            Dim pBePropietarioBodega As New clsBePropietario_bodega
            pBePropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(IdPropietarioBodega, lConnection, lTransaction)

            'Quita la cantidad dañada del stock reservado y marca como dañado en picking_ubic
            Dim bePickingUbicExistente As New clsBeTrans_picking_ubic() With {.IdPickingUbic = IdPickingUbic}
            clsLnTrans_picking_ubic.GetSingle(bePickingUbicExistente, lConnection, lTransaction)

            Dim bePickingUbicNuevo As New clsBeTrans_picking_ubic() With {.IdPickingUbic = IdPickingUbic}
            Dim bePickingUbicExistenteDañado As New clsBeTrans_picking_ubic() With {.IdPickingUbic = IdPickingUbic}

            bePickingUbicNuevo = bePickingUbicExistente.Clone()
            bePickingUbicExistenteDañado = bePickingUbicExistente.Clone()

            Dim beStockRes As New clsBeStock_res() With {.IdStockRes = IdStockRes}
            clsLnStock_res.GetSingle(beStockRes, lConnection, lTransaction)

            If beStockRes.IdPresentacion <> 0 Then
                Factor = 0
                Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(beStockRes.IdProductoBodega, beStockRes.IdPresentacion, lConnection, lTransaction)
                CantidadDañadaPresentacion = Math.Round(CantDañada * Factor, 6)
            End If

            If beStockRes.Cantidad = IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaPresentacion, CantDañada) Then

                bePickingUbicExistenteDañado.Encontrado = True
                bePickingUbicExistenteDañado.Acepto = False

                If Not EsPicking Then
                    bePickingUbicExistenteDañado.Cantidad_Recibida = CantDañada
                End If

                If EsPicking Then
                    bePickingUbicExistenteDañado.Dañado_picking = True
                Else
                    bePickingUbicExistenteDañado.Dañado_verificacion = True
                End If

                bePickingUbicExistenteDañado.IdPickingUbic = clsLnTrans_picking_ubic.MaxID(lConnection, lTransaction) + 1

                clsLnTrans_picking_ubic.Insertar(bePickingUbicExistenteDañado)

                'clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(IdStockRes, lConnection, lTransaction)

            ElseIf beStockRes.Cantidad > IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaPresentacion, CantDañada) Then

                '#EJC20181205 11:56 AM Se modificó comentario y renombró funciones.                
                bePickingUbicExistente.Cantidad_Solicitada -= CantDañada
                bePickingUbicNuevo.Cantidad_Solicitada = CantDañada

                If bePickingUbicExistente.Cantidad_Solicitada <> 0 Then

                    bePickingUbicExistenteDañado.Cantidad_Solicitada = CantDañada

                    bePickingUbicExistenteDañado.Encontrado = True
                    bePickingUbicExistenteDañado.Acepto = False

                    If Not EsPicking Then
                        bePickingUbicExistenteDañado.Cantidad_Recibida = CantDañada
                    End If

                    If EsPicking Then
                        bePickingUbicExistenteDañado.Dañado_picking = True
                    Else
                        bePickingUbicExistenteDañado.Dañado_verificacion = True
                    End If

                    bePickingUbicExistenteDañado.IdPickingUbic = clsLnTrans_picking_ubic.MaxID(lConnection, lTransaction) + 1

                    clsLnTrans_picking_ubic.Insertar(bePickingUbicExistenteDañado)

                ElseIf bePickingUbicExistente.Cantidad_Solicitada = 0 Then
                    bePickingUbicExistente.Cantidad_Solicitada = CantDañada
                End If

                If Not EsPicking Then

                    bePickingUbicNuevo.Cantidad_Recibida = 0

                    Dim BePickingEnc As New clsBeTrans_picking_enc
                    BePickingEnc.IdPickingEnc = clsLnTrans_picking_ubic.ObtienIdPickingEnc_Y_Verifica_Estado_PickingEnc_ByPickingUbic(IdPickingUbic, lConnection, lTransaction)

                    If BePickingEnc.IdPickingEnc <> 0 Then
                        BePickingEnc.Estado = "Pendiente"
                        clsLnTrans_picking_enc.Actualizar_Estado(BePickingEnc, lConnection, lTransaction)
                    End If

                End If

                If Not EsPicking Then
                    bePickingUbicExistente.Cantidad_Recibida -= CantDañada
                End If

                clsLnTrans_picking_ubic.Actualizar(bePickingUbicExistente, lConnection, lTransaction)

                '#CKFK 20180220 11:03 PM 
                'Si la cantidad del picking_ubic es mayor a la cantidad dañada se crea un nuevo picking_ubic 
                'con esa cantidad y con el dañado picking en true
                bePickingUbicNuevo.IdPickingUbic = clsLnTrans_picking_ubic.MaxID(lConnection, lTransaction) + 1
                bePickingUbicNuevo.IsNew = True

                If EsPicking Then
                    bePickingUbicNuevo.Dañado_picking = False
                Else
                    bePickingUbicNuevo.Dañado_verificacion = False
                End If


                '#EJC20181211: 1201: Copiar el Id antes de asignar el nuevo MaxId para actualizar el stockres.
                Dim vIdStockResOriginal As Integer = bePickingUbicNuevo.IdStockRes

                '#CKFK 20180501 Agregué el IdStockRes porque estaba guardando el anterior
                bePickingUbicExistente.IdStockRes = clsLnStock_res.MaxID(lConnection, lTransaction)
                clsLnTrans_picking_ubic.Insertar(bePickingUbicNuevo)

                'CM_10012019: Puse en comentario esto porque al actualizar el IdStockres se le estaba quitando la cantidad dañada y no se si sea
                'correcto porque es el mismo IdStockRes. (Consultar con Erik)
                'clsLnStock_res.Actualizar_Stock_Reservado_By_IdStockRes(vIdStockResOriginal,
                '                                                        IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaPresentacion, CantDañada),
                '                                                        lConnection,
                '                                                        lTransaction)

            Else
                Throw New Exception(String.Format("La cantidad disponible: {0} es menor a la cantidad dañada:{1} ", beStockRes.Cantidad, CantDañada))
            End If

            'Encabezado de ubicación con HH por cambio de estado
            Dim pIdMotivoUbicacion As Integer = clsLnBodega.Get_IdMotivoUbicacion_Dañado_Picking(IdBodega, lConnection, lTransaction)

            If pIdMotivoUbicacion = 0 Then Throw New Exception("No está definido IdMotivoUbicacion, no puede realizarse el reemplazo")

            Dim beUbicHHEnc As New clsBeTrans_ubic_hh_enc()
            Dim beTareaHH As New clsBeTarea_hh()

            If Not UbicaAuto Then

                beUbicHHEnc.IdPropietarioBodega = pBePropietarioBodega.IdPropietarioBodega
                beUbicHHEnc.IdMotivoUbicacion = pIdMotivoUbicacion
                beUbicHHEnc.FechaInicio = Now
                beUbicHHEnc.HoraInicio = Now
                beUbicHHEnc.FechaFin = Now
                beUbicHHEnc.HoraFin = Now
                beUbicHHEnc.Activo = True
                beUbicHHEnc.Observacion = "Cambio de estado por reemplazo de producto"
                beUbicHHEnc.User_agr = UsuarioHH
                beUbicHHEnc.Fec_agr = Now
                beUbicHHEnc.User_mod = UsuarioHH
                beUbicHHEnc.Fec_mod = Now
                beUbicHHEnc.Operador_por_linea = UsuarioHH
                beUbicHHEnc.Ubicacion_con_hh = True
                beUbicHHEnc.Cambio_estado = True
                beUbicHHEnc.Asunto = String.Format("{0}  {1}", pBePropietarioBodega.IdPropietario, "Reemplazo")
                beUbicHHEnc.IdPrioridad = 1 'Aquí es correcto que lo coloque con prioridad 3?
                beUbicHHEnc.IdTipoTarea = 3
                beUbicHHEnc.IdBodega = IdBodega
                beUbicHHEnc.Estado = "Nuevo"
                beUbicHHEnc.IsNew = True

                'Tarea de cambio de estado
                beTareaHH.IdTareahh = clsLnTarea_hh.MaxID(lConnection, lTransaction) + 1
                beTareaHH.IdPropietario = pBePropietarioBodega.IdPropietario
                beTareaHH.IdBodega = IdBodega
                beTareaHH.IdEstado = 1
                beTareaHH.Tipo = 0
                beTareaHH.FechaInicio = Now
                beTareaHH.FechaFin = Now
                beTareaHH.DiaCompleto = False
                beTareaHH.Ubicacion = ""
                beTareaHH.Descripcion = "Cambio de estado por reemplazo de producto"
                beTareaHH.Recordatorio = ""
                beTareaHH.Asunto = ""
                beTareaHH.IdPrioridad = 0
                beTareaHH.IdTipoTarea = 3
                beTareaHH.IdMuelle = Nothing

            End If

            'Stock que cambió de estado
            Dim pStock As New clsBeStock
            Dim pListStock As New List(Of clsBeStock)

            pStock = clsLnStock.GetSingle(IdStock, lConnection, lTransaction)

            '#CKFK 20190208 Guardé la presentación original del Stock porque se necesita para hacer el cambio de ubicación cuando es automático
            Dim vPresOrigStock As Integer = pStock.IdPresentacion

            pStock.IdPresentacion = bePickingUbicExistente.IdPresentacion

            If pStock.IdPresentacion <> 0 Then
                Factor = 0
                Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pStock.IdProductoBodega, pStock.IdPresentacion, lConnection, lTransaction)
                CantidadDañadaPresentacion = Math.Round(CantDañada * Factor, 6)
            End If

            'Detalle de ubicación con HH por cambio de estado
            Dim beListUbicHHDet As New List(Of clsBeTrans_ubic_hh_det)
            Dim beUbicHHDet As New clsBeTrans_ubic_hh_det()

            If Not UbicaAuto Then

                clsPublic.CopyObject(pStock, beUbicHHDet.Stock)

                beUbicHHDet.IdStock = IdStock
                beUbicHHDet.Producto = New clsBeProducto
                beUbicHHDet.Stock = New clsBeStock

                beUbicHHDet.IdTareaUbicacionDet = 0

                beUbicHHDet.ProductoEstado = New clsBeProducto_estado
                beUbicHHDet.ProductoPresentacion = New clsBeProducto_Presentacion
                beUbicHHDet.UnidadMedida = New clsBeUnidad_medida
                beUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion
                beUbicHHDet.Producto.Nombre = pStock.Producto.Nombre
                beUbicHHDet.Producto.Codigo = pStock.Producto.Codigo
                beUbicHHDet.Stock.IdUbicacion_anterior = pStock.IdUbicacion
                beUbicHHDet.IdUbicacionOrigen = pStock.IdUbicacion 'IdUbicDest
                beUbicHHDet.IdUbicacionDestino = IdUbicDest
                beUbicHHDet.Stock.Fecha_vence = pStock.Fecha_vence
                beUbicHHDet.Stock.Serial = pStock.Serial
                beUbicHHDet.IdEstadoOrigen = pStock.IdProductoEstado
                beUbicHHDet.IdEstadoDestino = IdEstadoDest
                beUbicHHDet.Cantidad = IIf(pStock.IdPresentacion <> 0, CantidadDañadaPresentacion, CantDañada)
                beUbicHHDet.Recibido = 0
                beUbicHHDet.Estado = "Pendiente"
                beUbicHHDet.Realizado = False
                beUbicHHDet.Operador = New clsBeOperador
                'beUbicHHDet.IdOperador = UsuarioHH

                beUbicHHDet.Activo = True

                beListUbicHHDet.Add(beUbicHHDet)

            End If

            'Inicio de movimiento de cambio de ubicacion
            Dim pListMov As New List(Of clsBeTrans_movimientos)

            Dim pMov As New clsBeTrans_movimientos()
            pMov.IdEmpresa = IdEmpresa
            pMov.IdBodegaOrigen = IdBodega
            pMov.IdTransaccion = IIf(Not UbicaAuto, beUbicHHDet.IdTareaUbicacionEnc, 1)
            pMov.IdPropietarioBodega = pBePropietarioBodega.IdPropietarioBodega
            pMov.IdProductoBodega = pStock.IdProductoBodega
            pMov.IdUbicacionOrigen = pStock.IdUbicacion
            pMov.IdUbicacionDestino = IIf(Not UbicaAuto, beUbicHHDet.IdUbicacionDestino, IdUbicDest)
            pMov.IdPresentacion = pStock.IdPresentacion
            pMov.IdEstadoOrigen = pStock.IdProductoEstado
            pMov.IdEstadoDestino = IdEstadoDest
            pMov.IdUnidadMedida = pStock.IdUnidadMedida
            pMov.IdTipoTarea = 3
            pMov.IdBodegaDestino = IdBodega
            pMov.IdRecepcion = pStock.IdRecepcionEnc
            pMov.IdRecepcionDet = pStock.IdRecepcionDet
            pMov.Cantidad = IIf(Not UbicaAuto, beUbicHHDet.Cantidad, IIf(pStock.IdPresentacion <> 0, CantidadDañadaPresentacion, CantDañada))
            pMov.Serie = pStock.Serial
            pMov.Peso = pStock.Peso
            pMov.Lote = pStock.Lote
            pMov.Fecha_vence = pStock.Fecha_vence
            pMov.Fecha = Now
            pMov.Barra_pallet = pStock.Lic_plate
            pMov.Hora_ini = Now
            pMov.Hora_fin = Now
            pMov.Fecha_agr = Now
            pMov.Usuario_agr = UsuarioHH
            pMov.Cantidad_hist = pStock.Cantidad
            pMov.Peso_hist = pStock.Peso

            'Modificar campos en el stock
            pStock.Cantidad = CantDañada
            pStock.IdProductoEstado = IdEstadoDest
            pStock.ProductoEstado.IdEstado = IdEstadoDest
            'pStock.IdUbicacion = IdUbicDest '#CKFK 20180219 Agregué al stock que la ubicación origen es la ubicación destino cuando genero la tarea de cambio de estado

            pListMov.Add(pMov)

            pListStock.Add(pStock)

            If Not UbicaAuto Then

                Guardar_Transaccion_Por_Picking_Process(beUbicHHEnc,
                                                       beListUbicHHDet,
                                                       Nothing,
                                                       pListMov,
                                                       True,
                                                       pBePropietarioBodega.IdPropietario,
                                                       pListStock,
                                                       Nothing,
                                                       Nothing,
                                                       beTareaHH.IdTareahh,
                                                       lConnection,
                                                       lTransaction)
            Else

                Dim vStockRes As New clsBeVW_stock_res

                vStockRes.IdProductoBodega = pStock.IdProductoBodega
                vStockRes.IdUbicacion = pMov.IdUbicacionOrigen
                vStockRes.Lote = pStock.Lote
                vStockRes.Fecha_Vence = pStock.Fecha_vence
                vStockRes.CantidadUmBas = IIf(pStock.IdPresentacion <> 0, CantidadDañadaPresentacion, pStock.Cantidad)
                vStockRes.Peso = pStock.Peso
                vStockRes.IdPresentacion = vPresOrigStock ' pStock.IdPresentacion #CKFK 20190208 Aquí voy a mandar la presentación original del stock y no la del picking ubic
                vStockRes.IdProductoEstado = pMov.IdEstadoOrigen
                vStockRes.Fecha_ingreso = Now
                vStockRes.ValorFecha = Now

                clsLnTrans_ubic_hh_det.Aplica_Cambio_Estado_Ubic_En_Picking(pMov, vStockRes, True, lConnection, lTransaction)

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Sub

    '#CKFK 20180202 04:17 PM Sobrecarga del procedimiento GeneraTareaCambioEstado con transaccionalidad
    Public Shared Sub Generar_Tarea_Cambio_Estado(ByVal IdBodega As Integer,
                                                  ByVal IdEmpresa As Integer,
                                                  ByVal IdStock As Integer,
                                                  ByVal IdStockRes As Integer,
                                                  ByVal UsuarioHH As Integer,
                                                  ByVal CantDañada As Double,
                                                  ByVal IdUbicDest As Integer,
                                                  ByVal IdEstadoDest As Integer,
                                                  ByVal IdPropietarioBodega As Integer,
                                                  ByVal IdPickingUbic As Integer,
                                                  ByRef pConnection As SqlConnection,
                                                  ByRef pTransaction As SqlTransaction,
                                                  ByVal pHostSolicita As String,
                                                  ByVal EsPicking As Boolean)

        Dim pBePropietarioBodega As New clsBePropietario_bodega
        Dim beStockRes As New clsBeStock_res
        Dim BeProductoEstado As New clsBeProducto_estado
        Dim BuenEstado As Boolean = False

        Try

            Dim Factor As Double = 0
            Dim CantidadDañadaUmBas As Double = 0

            Dim UbicaAuto As Boolean = False
            UbicaAuto = clsLnBodega.Get_Parametro_Cambio_Ubicacion_Auto(IdBodega, pConnection, pTransaction)

            pBePropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(IdPropietarioBodega, pConnection, pTransaction)

            'Quita la cantidad dañada del stock reservado y marca como dañado en picking_ubic
            '#AT20230104 Analizando los datos, esta consulta devuelve la nueva linea insertada en trans_picking_ubic
            'disponible para ser pickeada aca pude obtener la licencia para la bitacora de movimientos.
            Dim bePickingUbic As New clsBeTrans_picking_ubic() With {.IdPickingUbic = IdPickingUbic}
            clsLnTrans_picking_ubic.GetSingle(bePickingUbic, pConnection, pTransaction)

            Dim pStockActual As New clsBeStock
            pStockActual = clsLnStock.GetSingle(IdStock, pConnection, pTransaction)

            If pStockActual Is Nothing Then
                Throw New Exception("No se pudo obtener el stock seleccionado para cambio de estado.")
            End If

            beStockRes.IdStockRes = IdStockRes
            beStockRes.IdProductoBodega = bePickingUbic.IdProductoBodega
            clsLnStock_res.GetSingle(beStockRes,
                                     pConnection,
                                     pTransaction)

            If beStockRes.IdPresentacion <> 0 Then
                Factor = 0
                Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(beStockRes.IdProductoBodega, beStockRes.IdPresentacion, pConnection, pTransaction)
                CantidadDañadaUmBas = Math.Round(CantDañada * Factor, 6)
            End If

            If beStockRes.Cantidad = IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaUmBas, CantDañada) Then

                'CM_21012019: Se elimina el IdStock_res porque ya se insertó uno nuevo con los valores correctos. 
                clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(IdStockRes, pConnection, pTransaction)

            ElseIf beStockRes.Cantidad > IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaUmBas, CantDañada) Then '#CKFK 20180219 Cambié la condición porque es cuando la cantidad dañada es menor que se modifica la cantidad en el Stock

                bePickingUbic.IdPickingUbic = clsLnTrans_picking_ubic.MaxID(pConnection, pTransaction) + 1 '#CKFK 20180329 12:34 PM Corregí el IdPickingUbic, estaba tomando el Max de la tabla y debía adicionarle 1.
                bePickingUbic.IsNew = True
                '#AT 20220110 Se cambio del valor a true 
                bePickingUbic.Dañado_picking = True
                bePickingUbic.Cantidad_Solicitada = CantDañada

                clsLnStock_res.Actualizar_Stock_Reservado_By_IdStockRes(IdStockRes, IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaUmBas, CantDañada), pConnection, pTransaction)

            End If

            Dim beUbicHHEnc As New clsBeTrans_ubic_hh_enc
            Dim beTareaHH As New clsBeTarea_hh()


            If Not UbicaAuto Then

                'Encabezado de ubicación con HH por cambio de estado
                Dim pIdMotivoUbicacion As Integer = clsLnBodega.Get_IdMotivoUbicacion_Dañado_Picking(IdBodega, pConnection, pTransaction)
                If pIdMotivoUbicacion = 0 Then Throw New Exception("No está definido IdMotivoUbicacion por defecto para ubicación automática, no puede realizarse el reemplazo")

                beUbicHHEnc.IdPropietarioBodega = pBePropietarioBodega.IdPropietarioBodega
                beUbicHHEnc.IdMotivoUbicacion = pIdMotivoUbicacion
                beUbicHHEnc.FechaInicio = Now
                beUbicHHEnc.HoraInicio = Now
                beUbicHHEnc.FechaFin = Now
                beUbicHHEnc.HoraFin = Now
                beUbicHHEnc.Activo = True
                beUbicHHEnc.Observacion = "Cambio de estado por reemplazo de producto"
                beUbicHHEnc.User_agr = UsuarioHH
                beUbicHHEnc.Fec_agr = Now
                beUbicHHEnc.User_mod = UsuarioHH
                beUbicHHEnc.Fec_mod = Now
                beUbicHHEnc.Operador_por_linea = UsuarioHH
                beUbicHHEnc.Ubicacion_con_hh = True
                beUbicHHEnc.Cambio_estado = True
                beUbicHHEnc.Asunto = String.Format("{0}  {1}", pBePropietarioBodega.IdPropietario, "Reemplazo")
                beUbicHHEnc.IdPrioridad = 1 'Aquí es correcto que lo coloque con prioridad 3?
                beUbicHHEnc.IdTipoTarea = 3
                beUbicHHEnc.IdBodega = IdBodega
                beUbicHHEnc.Estado = "Nuevo"
                beUbicHHEnc.IsNew = True

                'Tarea de cambio de estado
                beTareaHH.IdTareahh = clsLnTarea_hh.MaxID(pConnection, pTransaction) + 1
                beTareaHH.IdPropietario = pBePropietarioBodega.IdPropietario
                beTareaHH.IdBodega = IdBodega
                beTareaHH.IdEstado = 1
                beTareaHH.Tipo = 0
                beTareaHH.FechaInicio = Now
                beTareaHH.FechaFin = Now
                beTareaHH.DiaCompleto = False
                beTareaHH.Ubicacion = ""
                beTareaHH.Descripcion = "Cambio de estado por reemplazo de producto"
                beTareaHH.Recordatorio = ""
                beTareaHH.Asunto = ""
                beTareaHH.IdPrioridad = 0
                beTareaHH.IdTipoTarea = 3
                beTareaHH.IdMuelle = Nothing

            End If

            'Stock que cambió de estado
            Dim pStock As New clsBeStock
            Dim pListStock As New List(Of clsBeStock)

            pStock = clsLnStock.GetSingle(IdStock, pConnection, pTransaction)

            If pStock Is Nothing Then
                Throw New Exception("No se pudo obtener el registro de stock.")
            End If

            '#CKFK 20190208 Guardé la presentación original del Stock porque se necesita para hacer el cambio de ubicación cuando es automático
            Dim vPresOrigStock As Integer = pStock.IdPresentacion

            pStock.IdPresentacion = bePickingUbic.IdPresentacion

            If pStock.IdPresentacion <> 0 Then
                Factor = 0
                Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pStock.IdProductoBodega, pStock.IdPresentacion, pConnection, pTransaction)
                CantidadDañadaUmBas = Math.Round(CantDañada * Factor, 6)
            End If

            'Detalle de ubicación con HH por cambio de estado
            Dim beListUbicHHDet As New List(Of clsBeTrans_ubic_hh_det)
            Dim beUbicHHDet As New clsBeTrans_ubic_hh_det()

            If Not UbicaAuto Then

                clsPublic.CopyObject(pStock, beUbicHHDet.Stock)

                beUbicHHDet.IdStock = IdStock
                beUbicHHDet.Producto = New clsBeProducto
                beUbicHHDet.Stock = New clsBeStock
                beUbicHHDet.IdTareaUbicacionDet = 0
                beUbicHHDet.ProductoEstado = New clsBeProducto_estado
                beUbicHHDet.ProductoPresentacion = New clsBeProducto_Presentacion
                beUbicHHDet.UnidadMedida = New clsBeUnidad_medida
                beUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion
                beUbicHHDet.Producto.Nombre = pStock.Producto.Nombre
                beUbicHHDet.Producto.Codigo = pStock.Producto.Codigo
                beUbicHHDet.Stock.IdUbicacion_anterior = pStock.IdUbicacion
                beUbicHHDet.IdUbicacionOrigen = pStock.IdUbicacion 'IdUbicDest
                beUbicHHDet.IdUbicacionDestino = IdUbicDest
                beUbicHHDet.Stock.Fecha_vence = pStock.Fecha_vence
                beUbicHHDet.Stock.Serial = pStock.Serial
                beUbicHHDet.IdEstadoOrigen = pStock.IdProductoEstado
                beUbicHHDet.IdEstadoDestino = IdEstadoDest
                '#AT20260105 Guardar la cantidad sin aplicar la conversión 
                beUbicHHDet.Cantidad = CantDañada
                beUbicHHDet.Recibido = 0
                beUbicHHDet.Estado = "Pendiente"
                beUbicHHDet.Operador = New clsBeOperador
                beUbicHHDet.IdOperadorBodega = UsuarioHH
                '#CKFK20251229 Agregamos el campo IdBodega que no se estaba enviando
                beUbicHHDet.IdBodega = IdBodega
                beUbicHHDet.Activo = True
                beListUbicHHDet.Add(beUbicHHDet)

            End If

            'Inicio de movimiento de cambio de ubicacion
            Dim pListMov As New List(Of clsBeTrans_movimientos)
            Dim pMov As New clsBeTrans_movimientos()

            pMov.IdEmpresa = IdEmpresa
            pMov.IdBodegaOrigen = IdBodega
            pMov.IdTransaccion = IIf(Not UbicaAuto, beUbicHHDet.IdTareaUbicacionEnc, 1)
            pMov.IdPropietarioBodega = pBePropietarioBodega.IdPropietarioBodega
            pMov.IdProductoBodega = pStock.IdProductoBodega
            pMov.IdUbicacionOrigen = pStock.IdUbicacion
            pMov.IdUbicacionDestino = IIf(Not UbicaAuto, beUbicHHDet.IdUbicacionDestino, IdUbicDest)
            pMov.IdPresentacion = pStock.IdPresentacion
            pMov.IdEstadoOrigen = pStock.IdProductoEstado
            pMov.IdEstadoDestino = IdEstadoDest
            pMov.IdUnidadMedida = pStock.IdUnidadMedida

            '#AT20230104 Se valida el estado, para asigiar el TipoTarea correspondiente para la bitacora de movimientos en reemplazos
            BeProductoEstado = clsLnProducto_estado.GetSingle(IdEstadoDest, pConnection, pTransaction)

            If Not BeProductoEstado Is Nothing Then
                If BeProductoEstado.Utilizable AndAlso Not BeProductoEstado.Dañado Then
                    BuenEstado = True
                End If
            Else
                Throw New Exception("Error en la validación de estado destino en reemplazo.")
            End If

            If EsPicking Then
                If BuenEstado Then
                    'REEMP_BE_PICK
                    pMov.IdTipoTarea = 25
                Else
                    'REEMP_ME_PICK
                    pMov.IdTipoTarea = 26
                End If
            Else
                If BuenEstado Then
                    'REEMP_BE_VERI
                    pMov.IdTipoTarea = 28
                Else
                    'REEMP_ME_VERI
                    pMov.IdTipoTarea = 29
                End If
            End If

            pMov.IdBodegaDestino = IdBodega
            pMov.IdRecepcion = pStock.IdRecepcionEnc
            pMov.IdRecepcionDet = pStock.IdRecepcionDet
            pMov.Cantidad = IIf(Not UbicaAuto, beUbicHHDet.Cantidad, IIf(pStock.IdPresentacion <> 0, CantidadDañadaUmBas, CantDañada))
            pMov.Serie = pStock.Serial
            pMov.Peso = pStock.Peso
            pMov.Lote = pStock.Lote
            pMov.Fecha_vence = pStock.Fecha_vence
            pMov.Fecha = Now
            pMov.Barra_pallet = pStock.Lic_plate
            pMov.Hora_ini = Now
            pMov.Hora_fin = Now
            pMov.Fecha_agr = Now
            pMov.Usuario_agr = UsuarioHH
            pMov.Cantidad_hist = pStock.Cantidad
            pMov.Peso_hist = pStock.Peso
            pMov.IdOperadorBodega = UsuarioHH
            pMov.Lic_plate = bePickingUbic.Lic_plate

            'Modificar campos en el stock
            '#AT20260105 pStock se utiliza para guardar stock res, siempre se debe guardar en umbas (cantidad)
            'pStock.Cantidad = CantDañada
            pStock.Cantidad = IIf(pStock.IdPresentacion <> 0, CantidadDañadaUmBas, CantDañada)
            pStock.IdProductoEstado = IdEstadoDest
            pStock.ProductoEstado.IdEstado = IdEstadoDest
            'pStock.IdUbicacion = IdUbicDest '#CKFK 20180219 Agregué al stock que la ubicación origen es la ubicación destino cuando genero la tarea de cambio de estado


            pListMov.Add(pMov)
            pListStock.Add(pStock)

            If Not UbicaAuto Then

                Guardar_Transaccion_Por_Picking_Process(beUbicHHEnc,
                                                       beListUbicHHDet,
                                                       Nothing,
                                                       pListMov,
                                                       True,
                                                       pBePropietarioBodega.IdPropietario,
                                                       pListStock,
                                                       Nothing,
                                                       Nothing,
                                                       beTareaHH.IdTareahh,
                                                       pConnection,
                                                       pTransaction,
                                                       pHostSolicita)

            Else

                Dim vStockRes As New clsBeVW_stock_res

                vStockRes.IdProductoBodega = pStock.IdProductoBodega
                vStockRes.IdUbicacion = pMov.IdUbicacionOrigen
                vStockRes.Lote = pStock.Lote
                vStockRes.Fecha_Vence = pStock.Fecha_vence
                vStockRes.CantidadUmBas = IIf(pStock.IdPresentacion <> 0, CantidadDañadaUmBas, pStock.Cantidad)
                vStockRes.Peso = pStock.Peso
                vStockRes.IdPresentacion = vPresOrigStock 'pStock.IdPresentacion '#CKFK 20190208 Aquí estoy enviando la presentación del stock original
                vStockRes.IdProductoEstado = pMov.IdEstadoOrigen
                vStockRes.Fecha_ingreso = Now
                vStockRes.ValorFecha = Now

                clsLnTrans_ubic_hh_det.Aplica_Cambio_Estado_Ubic_En_Picking(pMov,
                                                                            vStockRes,
                                                                            False,
                                                                            pConnection,
                                                                            pTransaction)


            End If

        Catch ex As Exception

            If beStockRes IsNot Nothing Then
                Libera_Inventario_Reservado(beStockRes.IdPedidoDet, beStockRes.IdPropietarioBodega, beStockRes.IdPicking, beStockRes.IdPedido, pConnection, pTransaction)
            End If

            Throw ex

        End Try

    End Sub

    Public Shared Function Generar_Tarea_Cambio_Estado_Sin_Exist(ByVal IdBodega As Integer,
                                                                  ByVal IdEmpresa As Integer,
                                                                  ByVal IdStock As Integer,
                                                                  ByVal IdStockRes As Integer,
                                                                  ByVal UsuarioHH As Integer,
                                                                  ByVal CantDañada As Double,
                                                                  ByVal IdUbicDest As Integer,
                                                                  ByVal IdEstadoDest As Integer,
                                                                  ByVal IdPropietarioBodega As Integer,
                                                                  ByVal IdPickingUbic As Integer,
                                                                  ByRef pConnection As SqlConnection,
                                                                  ByRef pTransaction As SqlTransaction,
                                                                  ByVal pHostSolicita As String) As Boolean

        Generar_Tarea_Cambio_Estado_Sin_Exist = False
        Dim pBePropietarioBodega As New clsBePropietario_bodega
        Dim beStockRes As New clsBeStock_res

        Try

            Dim Factor As Double = 0
            Dim CantidadDañadaUmBas As Double = 0

            Dim UbicaAuto As Boolean = False
            UbicaAuto = clsLnBodega.Get_Parametro_Cambio_Ubicacion_Auto(IdBodega, pConnection, pTransaction)

            pBePropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(IdPropietarioBodega, pConnection, pTransaction)

            'Quita la cantidad dañada del stock reservado y marca como dañado en picking_ubic
            Dim bePickingUbic As New clsBeTrans_picking_ubic() With {.IdPickingUbic = IdPickingUbic}
            clsLnTrans_picking_ubic.GetSingle(bePickingUbic, pConnection, pTransaction)

            Dim pStockActual As New clsBeStock
            pStockActual = clsLnStock.GetSingle(IdStock, pConnection, pTransaction)

            beStockRes.IdStockRes = IdStockRes
            beStockRes.IdProductoBodega = bePickingUbic.IdProductoBodega
            clsLnStock_res.GetSingle(beStockRes,
                                     pConnection,
                                     pTransaction)

            If beStockRes.IdPresentacion <> 0 Then
                Factor = 0
                Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(beStockRes.IdProductoBodega, beStockRes.IdPresentacion, pConnection, pTransaction)
                CantidadDañadaUmBas = Math.Round(CantDañada * Factor, 6)
            End If

            If beStockRes.Cantidad = IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaUmBas, CantDañada) Then

                'CM_21012019: Se elimina el IdStock_res porque ya se insertó uno nuevo con los valores correctos. 
                clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(IdStockRes, pConnection, pTransaction)

            ElseIf beStockRes.Cantidad > IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaUmBas, CantDañada) Then '#CKFK 20180219 Cambié la condición porque es cuando la cantidad dañada es menor que se modifica la cantidad en el Stock

                bePickingUbic.IdPickingUbic = clsLnTrans_picking_ubic.MaxID(pConnection, pTransaction) + 1 '#CKFK 20180329 12:34 PM Corregí el IdPickingUbic, estaba tomando el Max de la tabla y debía adicionarle 1.
                bePickingUbic.IsNew = True
                '#AT 20220110 Se cambio del valor a true 
                bePickingUbic.Dañado_picking = True
                bePickingUbic.Cantidad_Solicitada = CantDañada

                '#CM_21012019: Lo puse en comentario porque esto hace referencia al picking nuevo y tengo que hacer las
                'modificaciones sobre el picking original.
                'clsLnTrans_picking_ubic.Insertar(bePickingUbic)

                clsLnStock_res.Actualizar_Stock_Reservado_By_IdStockRes(IdStockRes, IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaUmBas, CantDañada), pConnection, pTransaction)

            End If

            Dim beUbicHHEnc As New clsBeTrans_ubic_hh_enc
            Dim beTareaHH As New clsBeTarea_hh()


            If Not UbicaAuto Then

                'Encabezado de ubicación con HH por cambio de estado
                Dim pIdMotivoUbicacion As Integer = clsLnBodega.Get_IdMotivoUbicacion_Dañado_Picking(IdBodega, pConnection, pTransaction)
                If pIdMotivoUbicacion = 0 Then Throw New Exception("No está definido IdMotivoUbicacion por defecto para ubicación automática, no puede realizarse el reemplazo")

                beUbicHHEnc.IdPropietarioBodega = pBePropietarioBodega.IdPropietarioBodega
                beUbicHHEnc.IdMotivoUbicacion = pIdMotivoUbicacion
                beUbicHHEnc.FechaInicio = Now
                beUbicHHEnc.HoraInicio = Now
                beUbicHHEnc.FechaFin = Now
                beUbicHHEnc.HoraFin = Now
                beUbicHHEnc.Activo = True
                beUbicHHEnc.Observacion = "Cambio de estado por reemplazo de producto"
                beUbicHHEnc.User_agr = UsuarioHH
                beUbicHHEnc.Fec_agr = Now
                beUbicHHEnc.User_mod = UsuarioHH
                beUbicHHEnc.Fec_mod = Now
                beUbicHHEnc.Operador_por_linea = UsuarioHH
                beUbicHHEnc.Ubicacion_con_hh = True
                beUbicHHEnc.Cambio_estado = True
                beUbicHHEnc.Asunto = String.Format("{0}  {1}", pBePropietarioBodega.IdPropietario, "Reemplazo")
                beUbicHHEnc.IdPrioridad = 1 'Aquí es correcto que lo coloque con prioridad 3?
                beUbicHHEnc.IdTipoTarea = 3
                beUbicHHEnc.IdBodega = IdBodega
                beUbicHHEnc.Estado = "Nuevo"
                beUbicHHEnc.IsNew = True

                'Tarea de cambio de estado
                beTareaHH.IdTareahh = clsLnTarea_hh.MaxID(pConnection, pTransaction) + 1
                beTareaHH.IdPropietario = pBePropietarioBodega.IdPropietario
                beTareaHH.IdBodega = IdBodega
                beTareaHH.IdEstado = 1
                beTareaHH.Tipo = 0
                beTareaHH.FechaInicio = Now
                beTareaHH.FechaFin = Now
                beTareaHH.DiaCompleto = False
                beTareaHH.Ubicacion = ""
                beTareaHH.Descripcion = "Cambio de estado por reemplazo de producto"
                beTareaHH.Recordatorio = ""
                beTareaHH.Asunto = ""
                beTareaHH.IdPrioridad = 0
                beTareaHH.IdTipoTarea = 3
                beTareaHH.IdMuelle = Nothing

            End If

            'Stock que cambió de estado
            Dim pStock As New clsBeStock
            Dim pListStock As New List(Of clsBeStock)

            pStock = clsLnStock.GetSingle(IdStock, pConnection, pTransaction)

            '#CKFK 20190208 Guardé la presentación original del Stock porque se necesita para hacer el cambio de ubicación cuando es automático
            Dim vPresOrigStock As Integer = pStock.IdPresentacion

            pStock.IdPresentacion = bePickingUbic.IdPresentacion

            If pStock.IdPresentacion <> 0 Then
                Factor = 0
                Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pStock.IdProductoBodega, pStock.IdPresentacion, pConnection, pTransaction)
                CantidadDañadaUmBas = Math.Round(CantDañada * Factor, 6)
            End If

            'Detalle de ubicación con HH por cambio de estado
            Dim beListUbicHHDet As New List(Of clsBeTrans_ubic_hh_det)
            Dim beUbicHHDet As New clsBeTrans_ubic_hh_det()

            If Not UbicaAuto Then

                clsPublic.CopyObject(pStock, beUbicHHDet.Stock)

                beUbicHHDet.IdStock = IdStock
                beUbicHHDet.Producto = New clsBeProducto
                beUbicHHDet.Stock = New clsBeStock
                beUbicHHDet.IdTareaUbicacionDet = 0
                beUbicHHDet.ProductoEstado = New clsBeProducto_estado
                beUbicHHDet.ProductoPresentacion = New clsBeProducto_Presentacion
                beUbicHHDet.UnidadMedida = New clsBeUnidad_medida
                beUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion
                beUbicHHDet.Producto.Nombre = pStock.Producto.Nombre
                beUbicHHDet.Producto.Codigo = pStock.Producto.Codigo
                beUbicHHDet.Stock.IdUbicacion_anterior = pStock.IdUbicacion
                beUbicHHDet.IdUbicacionOrigen = pStock.IdUbicacion 'IdUbicDest
                beUbicHHDet.IdUbicacionDestino = IdUbicDest
                beUbicHHDet.Stock.Fecha_vence = pStock.Fecha_vence
                beUbicHHDet.Stock.Serial = pStock.Serial
                beUbicHHDet.IdEstadoOrigen = pStock.IdProductoEstado
                beUbicHHDet.IdEstadoDestino = IdEstadoDest
                beUbicHHDet.Cantidad = IIf(pStock.IdPresentacion <> 0, CantidadDañadaUmBas, CantDañada)
                beUbicHHDet.Recibido = 0
                beUbicHHDet.Estado = "Pendiente"
                beUbicHHDet.Operador = New clsBeOperador
                beUbicHHDet.IdOperadorBodega = UsuarioHH
                beUbicHHDet.Activo = True
                beListUbicHHDet.Add(beUbicHHDet)

            End If

            'Inicio de movimiento de cambio de ubicacion
            Dim pListMov As New List(Of clsBeTrans_movimientos)
            Dim pMov As New clsBeTrans_movimientos()

            pMov.IdEmpresa = IdEmpresa
            pMov.IdBodegaOrigen = IdBodega
            pMov.IdTransaccion = IIf(Not UbicaAuto, beUbicHHDet.IdTareaUbicacionEnc, 1)
            pMov.IdPropietarioBodega = pBePropietarioBodega.IdPropietarioBodega
            pMov.IdProductoBodega = pStock.IdProductoBodega
            pMov.IdUbicacionOrigen = pStock.IdUbicacion
            pMov.IdUbicacionDestino = IIf(Not UbicaAuto, beUbicHHDet.IdUbicacionDestino, IdUbicDest)
            pMov.IdPresentacion = pStock.IdPresentacion
            pMov.IdEstadoOrigen = pStock.IdProductoEstado
            pMov.IdEstadoDestino = IdEstadoDest
            pMov.IdUnidadMedida = pStock.IdUnidadMedida
            pMov.IdTipoTarea = 3
            pMov.IdBodegaDestino = IdBodega
            pMov.IdRecepcion = pStock.IdRecepcionEnc
            pMov.IdRecepcionDet = pStock.IdRecepcionDet
            pMov.Cantidad = IIf(Not UbicaAuto, beUbicHHDet.Cantidad, IIf(pStock.IdPresentacion <> 0, CantidadDañadaUmBas, CantDañada))
            pMov.Serie = pStock.Serial
            pMov.Peso = pStock.Peso
            pMov.Lote = pStock.Lote
            pMov.Fecha_vence = pStock.Fecha_vence
            pMov.Fecha = Now
            pMov.Barra_pallet = pStock.Lic_plate
            pMov.Hora_ini = Now
            pMov.Hora_fin = Now
            pMov.Fecha_agr = Now
            pMov.Usuario_agr = UsuarioHH
            pMov.Cantidad_hist = pStock.Cantidad
            pMov.Peso_hist = pStock.Peso

            'Modificar campos en el stock
            pStock.Cantidad = CantDañada
            pStock.IdProductoEstado = IdEstadoDest
            pStock.ProductoEstado.IdEstado = IdEstadoDest
            'pStock.IdUbicacion = IdUbicDest '#CKFK 20180219 Agregué al stock que la ubicación origen es la ubicación destino cuando genero la tarea de cambio de estado


            pListMov.Add(pMov)
            pListStock.Add(pStock)

            If Not UbicaAuto Then

                Guardar_Transaccion_Por_Picking_Process(beUbicHHEnc,
                                                       beListUbicHHDet,
                                                       Nothing,
                                                       pListMov,
                                                       True,
                                                       pBePropietarioBodega.IdPropietario,
                                                       pListStock,
                                                       Nothing,
                                                       Nothing,
                                                       beTareaHH.IdTareahh,
                                                       pConnection,
                                                       pTransaction,
                                                       pHostSolicita)

            Else

                Dim vStockRes As New clsBeVW_stock_res

                vStockRes.IdProductoBodega = pStock.IdProductoBodega
                vStockRes.IdUbicacion = pMov.IdUbicacionOrigen
                vStockRes.Lote = pStock.Lote
                vStockRes.Fecha_Vence = pStock.Fecha_vence
                vStockRes.CantidadUmBas = IIf(pStock.IdPresentacion <> 0, CantidadDañadaUmBas, pStock.Cantidad)
                vStockRes.Peso = pStock.Peso
                vStockRes.IdPresentacion = vPresOrigStock 'pStock.IdPresentacion '#CKFK 20190208 Aquí estoy enviando la presentación del stock original
                vStockRes.IdProductoEstado = pMov.IdEstadoOrigen
                vStockRes.Fecha_ingreso = Now
                vStockRes.ValorFecha = Now

                clsLnTrans_ubic_hh_det.Aplica_Cambio_Estado_Ubic_En_Picking(pMov,
                                                                            vStockRes,
                                                                            False,
                                                                            pConnection,
                                                                            pTransaction)

            End If

            Return True

        Catch ex As Exception

            If beStockRes IsNot Nothing Then
                Libera_Inventario_Reservado(beStockRes.IdPedidoDet, beStockRes.IdPropietarioBodega, beStockRes.IdPicking, beStockRes.IdPedido, pConnection, pTransaction)
            End If

            Throw ex

        End Try

    End Function

    Public Shared Sub Generar_Tarea_Cambio_Estado(ByVal IdBodega As Integer,
                                              ByVal IdEmpresa As Integer,
                                              ByVal IdStock As Integer,
                                              ByVal IdStockRes As Integer,
                                              ByVal UsuarioHH As Integer,
                                              ByVal CantDañada As Double,
                                              ByVal IdUbicDest As Integer,
                                              ByVal IdProductoEstadoDest As Integer,
                                              ByVal IdPropietarioBodega As Integer,
                                              ByVal IdPickingUbic As Integer,
                                              ByVal MarcarComoNE As Boolean,
                                              ByRef pConnection As SqlConnection,
                                              ByRef pTransaction As SqlTransaction,
                                              ByVal pHostSolicita As String)

        Dim pBePropietarioBodega As New clsBePropietario_bodega
        Dim beStockRes As New clsBeStock_res

        Try

            Dim Factor As Double = 0
            Dim CantidadDañadaUMBas As Double = 0
            Dim CantidadPresentacion As Double = 0

            Dim UbicaAuto As Boolean = False
            UbicaAuto = clsLnBodega.Get_Parametro_Cambio_Ubicacion_Auto(IdBodega, pConnection, pTransaction)

            pBePropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario(IdPropietarioBodega, pConnection, pTransaction)

            'Quita la cantidad dañada del stock reservado y marca como dañado en picking_ubic
            '#23122022: Agrega la nueva linea de reemplazo en el picking_ubic
            Dim bePickingUbic As New clsBeTrans_picking_ubic() With {.IdPickingUbic = IdPickingUbic}
            clsLnTrans_picking_ubic.GetSingle(bePickingUbic, pConnection, pTransaction)

            beStockRes.IdStockRes = IdStockRes
            clsLnStock_res.GetSingle(beStockRes, pConnection, pTransaction)

            If beStockRes.IdPresentacion <> 0 Then
                CantidadPresentacion = CantDañada
                Factor = 0
                Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(beStockRes.IdProductoBodega, beStockRes.IdPresentacion, pConnection, pTransaction)
                CantidadDañadaUMBas = Math.Round(CantDañada * Factor, 6)
            Else
                CantDañada = 0
                CantidadDañadaUMBas = CantDañada
            End If

            If IdUbicDest = 0 Then
                IdUbicDest = clsLnBodega.Get_IdUbicNE_By_IdBodega(IdBodega, pConnection, pTransaction)
                If IdUbicDest = 0 Then
                    Throw New Exception("No está definida la ubicación por defecto para producto no encontrado")
                End If
            End If

            If IdProductoEstadoDest = 0 Then
                IdProductoEstadoDest = clsLnBodega.Get_IdProductoEstadoNE_By_IdBodega(IdBodega, pConnection, pTransaction)
                If IdProductoEstadoDest = 0 Then
                    Throw New Exception("No está definido el estado por defecto para producto no encontrado")
                End If
            End If

            If beStockRes.Cantidad = IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaUMBas, CantDañada) Then

                'CM_21012019: Se elimina el IdStock_res porque ya se insertó uno nuevo con los valores correctos. 
                clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(IdStockRes, pConnection, pTransaction)

            ElseIf beStockRes.Cantidad > IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaUMBas, CantDañada) Then '#CKFK 20180219 Cambié la condición porque es cuando la cantidad dañada es menor que se modifica la cantidad en el Stock

                bePickingUbic.IdPickingUbic = clsLnTrans_picking_ubic.MaxID(pConnection, pTransaction) + 1 '#CKFK 20180329 12:34 PM Corregí el IdPickingUbic, estaba tomando el Max de la tabla y debía adicionarle 1.
                bePickingUbic.IsNew = True
                bePickingUbic.Dañado_picking = False
                bePickingUbic.Cantidad_Solicitada = CantDañada
                bePickingUbic.No_packing = 0

                '#CM_21012019: Lo puse en comentario porque esto hace referencia al picking nuevo y tengo que hacer las
                'modificaciones sobre el picking original.
                'clsLnTrans_picking_ubic.Insertar(bePickingUbic)

                clsLnStock_res.Actualizar_Stock_Reservado_By_IdStockRes(IdStockRes,
                                                                        IIf(beStockRes.IdPresentacion <> 0, CantidadDañadaUMBas, CantDañada),
                                                                        pConnection,
                                                                        pTransaction)

            End If

            Dim beUbicHHEnc As New clsBeTrans_ubic_hh_enc
            Dim beTareaHH As New clsBeTarea_hh()

            If MarcarComoNE Then

                If Not UbicaAuto Then

                    'Encabezado de ubicación con HH por cambio de estado
                    Dim pIdMotivoUbicacion As Integer = clsLnBodega.Get_IdMotivoUbicacion_Dañado_Picking(IdBodega, pConnection, pTransaction)
                    If pIdMotivoUbicacion = 0 Then Throw New Exception("No está definido IdMotivoUbicacion por defecto para ubicación automática, no puede realizarse el reemplazo")

                    beUbicHHEnc.IdPropietarioBodega = pBePropietarioBodega.IdPropietarioBodega
                    beUbicHHEnc.IdMotivoUbicacion = pIdMotivoUbicacion
                    beUbicHHEnc.FechaInicio = Now
                    beUbicHHEnc.HoraInicio = Now
                    beUbicHHEnc.FechaFin = Now
                    beUbicHHEnc.HoraFin = Now
                    beUbicHHEnc.Activo = True
                    beUbicHHEnc.Observacion = "Cambio de estado por reemplazo de producto"
                    beUbicHHEnc.User_agr = UsuarioHH
                    beUbicHHEnc.Fec_agr = Now
                    beUbicHHEnc.User_mod = UsuarioHH
                    beUbicHHEnc.Fec_mod = Now
                    beUbicHHEnc.Operador_por_linea = UsuarioHH
                    beUbicHHEnc.Ubicacion_con_hh = True
                    beUbicHHEnc.Cambio_estado = True
                    beUbicHHEnc.Asunto = String.Format("{0}  {1}", pBePropietarioBodega.IdPropietario, "Reemplazo")
                    beUbicHHEnc.IdPrioridad = 1 'Aquí es correcto que lo coloque con prioridad 3?
                    beUbicHHEnc.IdTipoTarea = 3
                    beUbicHHEnc.IdBodega = IdBodega
                    beUbicHHEnc.Estado = "Nuevo"
                    beUbicHHEnc.IsNew = True

                    'Tarea de cambio de estado
                    beTareaHH.IdTareahh = clsLnTarea_hh.MaxID(pConnection, pTransaction) + 1
                    beTareaHH.IdPropietario = pBePropietarioBodega.IdPropietario
                    beTareaHH.IdBodega = IdBodega
                    beTareaHH.IdEstado = 1
                    beTareaHH.Tipo = 0
                    beTareaHH.FechaInicio = Now
                    beTareaHH.FechaFin = Now
                    beTareaHH.DiaCompleto = False
                    beTareaHH.Ubicacion = ""
                    beTareaHH.Descripcion = "Cambio de estado por reemplazo de producto"
                    beTareaHH.Recordatorio = ""
                    beTareaHH.Asunto = ""
                    beTareaHH.IdPrioridad = 0
                    beTareaHH.IdTipoTarea = 3
                    beTareaHH.IdMuelle = Nothing

                End If

                'Stock que cambió de estado
                Dim pStock As New clsBeStock
                Dim pListStock As New List(Of clsBeStock)

                pStock = clsLnStock.GetSingle(IdStock, pConnection, pTransaction)

                '#CKFK 20190208 Guardé la presentación original del Stock porque se necesita para hacer el cambio de ubicación cuando es automático
                Dim vPresOrigStock As Integer = pStock.IdPresentacion

                pStock.IdPresentacion = bePickingUbic.IdPresentacion

                'If pStock.IdPresentacion <> 0 Then
                '    Factor = 0
                '    Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pStock.IdProductoBodega, pStock.IdPresentacion, pConnection, pTransaction)
                '    CantidadDañadaUMBas = Math.Round(CantDañada / Factor, 6)
                'End If

                'Detalle de ubicación con HH por cambio de estado
                Dim beListUbicHHDet As New List(Of clsBeTrans_ubic_hh_det)
                Dim beUbicHHDet As New clsBeTrans_ubic_hh_det()

                If Not UbicaAuto Then

                    clsPublic.CopyObject(pStock, beUbicHHDet.Stock)

                    beUbicHHDet.IdStock = IdStock
                    beUbicHHDet.Producto = New clsBeProducto
                    beUbicHHDet.Stock = New clsBeStock
                    beUbicHHDet.IdTareaUbicacionDet = 0
                    beUbicHHDet.ProductoEstado = New clsBeProducto_estado
                    beUbicHHDet.ProductoPresentacion = New clsBeProducto_Presentacion
                    beUbicHHDet.UnidadMedida = New clsBeUnidad_medida
                    beUbicHHDet.UbicacionDestino = New clsBeBodega_ubicacion
                    beUbicHHDet.Producto.Nombre = pStock.Producto.Nombre
                    beUbicHHDet.Producto.Codigo = pStock.Producto.Codigo
                    beUbicHHDet.Stock.IdUbicacion_anterior = pStock.IdUbicacion
                    beUbicHHDet.IdUbicacionOrigen = pStock.IdUbicacion 'IdUbicDest
                    beUbicHHDet.IdUbicacionDestino = IdUbicDest
                    beUbicHHDet.Stock.Fecha_vence = pStock.Fecha_vence
                    beUbicHHDet.Stock.Serial = pStock.Serial
                    beUbicHHDet.IdEstadoOrigen = pStock.IdProductoEstado
                    beUbicHHDet.IdEstadoDestino = IdProductoEstadoDest
                    beUbicHHDet.Cantidad = IIf(pStock.IdPresentacion <> 0, CantidadDañadaUMBas, CantDañada)
                    beUbicHHDet.Recibido = 0
                    beUbicHHDet.Estado = "Pendiente"
                    beUbicHHDet.Operador = New clsBeOperador
                    beUbicHHDet.IdOperadorBodega = UsuarioHH
                    beUbicHHDet.Activo = True
                    beListUbicHHDet.Add(beUbicHHDet)

                End If

                'Inicio de movimiento de cambio de ubicacion
                Dim pListMov As New List(Of clsBeTrans_movimientos)
                Dim pMov As New clsBeTrans_movimientos()

                pMov.IdEmpresa = IdEmpresa
                pMov.IdBodegaOrigen = IdBodega
                pMov.IdTransaccion = IIf(Not UbicaAuto, beUbicHHDet.IdTareaUbicacionEnc, 1)
                pMov.IdPropietarioBodega = pBePropietarioBodega.IdPropietarioBodega
                pMov.IdProductoBodega = pStock.IdProductoBodega
                pMov.IdUbicacionOrigen = pStock.IdUbicacion
                pMov.IdUbicacionDestino = IIf(Not UbicaAuto, beUbicHHDet.IdUbicacionDestino, IdUbicDest)
                pMov.IdPresentacion = pStock.IdPresentacion
                pMov.IdEstadoOrigen = pStock.IdProductoEstado
                pMov.IdEstadoDestino = IdProductoEstadoDest
                pMov.IdUnidadMedida = pStock.IdUnidadMedida
                pMov.IdTipoTarea = 3
                pMov.IdBodegaDestino = IdBodega
                pMov.IdRecepcion = pStock.IdRecepcionEnc
                pMov.IdRecepcionDet = pStock.IdRecepcionDet
                pMov.Cantidad = CantidadDañadaUMBas
                pMov.Serie = pStock.Serial
                pMov.Peso = pStock.Peso
                pMov.Lote = pStock.Lote
                pMov.Fecha_vence = pStock.Fecha_vence
                pMov.Fecha = Now
                pMov.Barra_pallet = pStock.Lic_plate
                pMov.Hora_ini = Now
                pMov.Hora_fin = Now
                pMov.Fecha_agr = Now
                pMov.Usuario_agr = UsuarioHH
                pMov.Cantidad_hist = pStock.Cantidad
                pMov.Peso_hist = pStock.Peso

                'Modificar campos en el stock
                pStock.Cantidad = CantidadDañadaUMBas
                pStock.IdProductoEstado = IdProductoEstadoDest
                pStock.ProductoEstado.IdEstado = IdProductoEstadoDest
                'pStock.IdUbicacion = IdUbicDest '#CKFK 20180219 Agregué al stock que la ubicación origen es la ubicación destino cuando genero la tarea de cambio de estado


                pListMov.Add(pMov)
                pListStock.Add(pStock)

                If Not UbicaAuto Then

                    Guardar_Transaccion_Por_Picking_Process(beUbicHHEnc,
                                                           beListUbicHHDet,
                                                           Nothing,
                                                           pListMov,
                                                           True,
                                                           pBePropietarioBodega.IdPropietario,
                                                           pListStock,
                                                           Nothing,
                                                           Nothing,
                                                           beTareaHH.IdTareahh,
                                                           pConnection,
                                                           pTransaction,
                                                           pHostSolicita)

                Else

                    Dim vStockRes As New clsBeVW_stock_res
                    vStockRes.IdProductoBodega = pStock.IdProductoBodega
                    vStockRes.IdUbicacion = pMov.IdUbicacionOrigen
                    vStockRes.Lote = pStock.Lote
                    vStockRes.Fecha_Vence = pStock.Fecha_vence
                    'vStockRes.CantidadUmBas = IIf(pStock.IdPresentacion <> 0, CantidadPresentacion, CantidadDañadaUMBas)
                    vStockRes.CantidadUmBas = CantidadDañadaUMBas
                    vStockRes.Peso = pStock.Peso
                    vStockRes.IdPresentacion = vPresOrigStock 'pStock.IdPresentacion '#CKFK 20190208 Aquí estoy enviando la presentación del stock original
                    vStockRes.IdProductoEstado = pMov.IdEstadoOrigen
                    vStockRes.Fecha_ingreso = Now
                    vStockRes.ValorFecha = Now

                    clsLnTrans_ubic_hh_det.Aplica_Cambio_Estado_Ubic_En_Picking(pMov,
                                                                                vStockRes,
                                                                                False,
                                                                                pConnection,
                                                                                pTransaction)

                End If 'Fin de Ubicación Automática

            End If 'Fin de MarcarComoNE

        Catch ex As Exception

            If beStockRes IsNot Nothing Then
                Libera_Inventario_Reservado(beStockRes.IdPedidoDet,
                                            beStockRes.IdPropietarioBodega,
                                            beStockRes.IdPicking,
                                            beStockRes.IdPedido,
                                            pConnection,
                                            pTransaction)
            End If

            Throw ex

        End Try

    End Sub

    '#CKFK 20180131 04:24 PM Función creada para obtener el MaxID con transaccionalidad
    Public Shared Function MaxID(ByRef pConnection As SqlConnection,
                                  ByRef pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0
            Const sp As String = "SELECT ISNULL(Max(IdTareaUbicacionEnc),0) FROM Trans_ubic_hh_enc"

            Dim lCommand As New SqlCommand(sp, pConnection) With {.CommandType = CommandType.Text}
            lCommand.Transaction = pTransaction

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                lMax = CInt(lReturnValue)
            End If

            Return lMax

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            '#MECR03112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            ' clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)
            Throw ex
        End Try

    End Function

    Private Shared Sub Libera_Inventario_Reservado(ByVal pIdPedidoDet As Integer,
                                            ByVal pIdPropietarioBodega As Integer,
                                            ByVal pIdPickingEnc As Integer,
                                            ByVal pIdPedidoEnc As Integer,
                                            Optional ByRef pConnection As SqlConnection = Nothing,
                                            Optional ByRef pTransaction As SqlTransaction = Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Not Es_Transaccion_Remota Then
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction
            End If

            Dim lBeStockRes As New List(Of clsBeStock_res)

            If Es_Transaccion_Remota Then
                lBeStockRes = clsLnStock_res.Get_All_Reemplazo_By_IdPedidoDet(pIdPedidoDet, pIdPropietarioBodega, pIdPickingEnc, pIdPedidoEnc, pConnection, pTransaction)
            Else
                lBeStockRes = clsLnStock_res.Get_All_Reemplazo_By_IdPedidoDet(pIdPedidoDet, pIdPropietarioBodega, pIdPickingEnc, pIdPedidoEnc, lConnection, lTransaction)
            End If

            For Each stockres In lBeStockRes

                If Es_Transaccion_Remota Then
                    clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(stockres.IdStockRes, pConnection, pTransaction)
                Else
                    clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(stockres.IdStockRes, lConnection, lTransaction)
                End If

            Next

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Sub

    '#CKFK 20180412 12:04 PM Creé esta función para poder actualizar la tarea de cambio de ubicación y la tarea de la HH a estado finalizado
    Public Shared Function Actualiza_Cambio_Estado(ByRef oBeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc, Optional ByRef pConnection As SqlConnection = Nothing, Optional ByRef pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim IdTipoTarea As Integer = 0

        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Not Es_Transaccion_Remota Then

                lConnection.Open() : lTransaction = lConnection.BeginTransaction

                Actualizar(oBeTrans_ubic_hh_enc, lConnection, lTransaction)

                IdTipoTarea = IIf(oBeTrans_ubic_hh_enc.Cambio_estado, 3, 2) '#CKFK 20180501 Agregué la variable IdTipoTarea porque puede ser un cambio de ubicación o de estado

                clsLnTarea_hh.Actualiza_Estado_Tarea(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, IdTipoTarea, 4, lConnection, lTransaction) 'El IdEstado 4 es Finalizado

                lTransaction.Commit()

            Else

                Actualizar(oBeTrans_ubic_hh_enc, pConnection, pTransaction)
                clsLnTarea_hh.Actualiza_Estado_Tarea(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, 2, 4, pConnection, pTransaction) 'El IdEstado 4 es Finalizado

            End If

        Catch ex1 As SqlException
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Anular_Tarea_Cambio_Ubic_O_Estado(ByRef oBeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc,
                                                             Optional ByRef pConnection As SqlConnection = Nothing,
                                                             Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vResultadoEliminoStockReservado As Boolean = False

        Anular_Tarea_Cambio_Ubic_O_Estado = False

        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Not Es_Transaccion_Remota Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            Dim lBeStockRes As New List(Of clsBeStock_res)

            If Es_Transaccion_Remota Then

                oBeTrans_ubic_hh_enc.Estado = "Anulado"

                Actualizar(oBeTrans_ubic_hh_enc, pConnection, pTransaction)

                clsLnTarea_hh.Actualiza_Estado_Tarea(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                     2,
                                                     3,
                                                     pConnection,
                                                     pTransaction) 'El IdEstado 3 es Anulado

                If oBeTrans_ubic_hh_enc.IdReabastecimientoLog <> 0 Then

                    clsLnTrans_reabastecimiento_log.Quitar_Tarea_HH(oBeTrans_ubic_hh_enc.IdReabastecimientoLog,
                                                                    pConnection,
                                                                    pTransaction)

                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Reabasto(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                                                        pConnection,
                                                                                        pTransaction)
                Else
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Ubicacion(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                                                         pConnection,
                                                                                         pTransaction)
                End If

            Else

                oBeTrans_ubic_hh_enc.Estado = "Anulado"
                Actualizar(oBeTrans_ubic_hh_enc,
                           lConnection,
                           lTransaction)

                clsLnTarea_hh.Actualiza_Estado_Tarea(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                     2,
                                                     3,
                                                     lConnection,
                                                     lTransaction) 'El IdEstado 3 es Anulado

                If oBeTrans_ubic_hh_enc.IdReabastecimientoLog <> 0 Then
                    clsLnTrans_reabastecimiento_log.Quitar_Tarea_HH(oBeTrans_ubic_hh_enc.IdReabastecimientoLog,
                                                                    lConnection,
                                                                    lTransaction)
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Reabasto(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                                                        lConnection,
                                                                                        lTransaction)
                Else
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Ubicacion(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                                                         lConnection,
                                                                                         lTransaction)
                End If

            End If

            If Not lBeStockRes Is Nothing Then

                If Not lBeStockRes.Count = 0 Then

                    For Each stockres In lBeStockRes

                        If Es_Transaccion_Remota Then
                            vResultadoEliminoStockReservado = clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(stockres.IdStockRes,
                                                                                                                    pConnection,
                                                                                                                    pTransaction)
                        Else
                            vResultadoEliminoStockReservado = clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(stockres.IdStockRes,
                                                                                                                    lConnection,
                                                                                                                    lTransaction)
                        End If

                    Next

                End If

            Else
                Throw New Exception("#EJC20211216_0959: No se pudo obtener el stock reservado de la transacción.")
            End If

            If Not vResultadoEliminoStockReservado Then

                If XtraMessageBox.Show("¿No se pudo eliminar el stock reservado, anular el cambio de ubicación de todas formas?",
                    "Stock_Reservado_Cambio_Ubicacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    Throw New Exception("#EJC20211216_0959: No se pudo eliminar el stock reservado de la transacción.")
                End If

            End If

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            Anular_Tarea_Cambio_Ubic_O_Estado = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Liberar_Stock_Reservado_Sin_Procesar(ByRef oBeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc,
                                                                Optional ByRef pConnection As SqlConnection = Nothing,
                                                                Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vResultadoEliminoStockReservado As Integer = 0

        Liberar_Stock_Reservado_Sin_Procesar = False

        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Not Es_Transaccion_Remota Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            Dim lBeStockRes As New List(Of clsBeStock_res)
            Dim lBeStockResByDet As New List(Of clsBeStock_res)

            If Es_Transaccion_Remota Then

                If oBeTrans_ubic_hh_enc.IdReabastecimientoLog <> 0 Then
                    clsLnTrans_reabastecimiento_log.Quitar_Tarea_HH(oBeTrans_ubic_hh_enc.IdReabastecimientoLog, pConnection, pTransaction)
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Reabasto(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pConnection, pTransaction)
                Else
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Ubicacion(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pConnection, pTransaction)
                End If

            Else

                If oBeTrans_ubic_hh_enc.IdReabastecimientoLog <> 0 Then
                    clsLnTrans_reabastecimiento_log.Quitar_Tarea_HH(oBeTrans_ubic_hh_enc.IdReabastecimientoLog, lConnection, lTransaction)
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Reabasto(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, lConnection, lTransaction)
                Else
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Ubicacion(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, lConnection, lTransaction)
                End If

            End If

            Dim pListObjDet As New List(Of clsBeTrans_ubic_hh_det)

            If Not lBeStockRes Is Nothing Then

                If Not lBeStockRes.Count = 0 Then

                    pListObjDet = clsLnTrans_ubic_hh_det.Get_All_By_Id_Trans_Ubic_Enc(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, lConnection, lTransaction)

                    For Each det In pListObjDet

                        If Not det.Realizado Then

                            lBeStockResByDet = lBeStockRes.FindAll(Function(x) x.IdStock = det.IdStock)

                            For Each stockres In lBeStockResByDet

                                If Es_Transaccion_Remota Then
                                    vResultadoEliminoStockReservado += clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(stockres.IdStockRes, pConnection, pTransaction)
                                Else
                                    vResultadoEliminoStockReservado += clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(stockres.IdStockRes, lConnection, lTransaction)
                                End If

                            Next

                        End If

                    Next

                    If vResultadoEliminoStockReservado > 0 Then

                        oBeTrans_ubic_hh_enc.Estado = "Incompleta"

                        Actualizar(oBeTrans_ubic_hh_enc, pConnection, pTransaction)

                        If Es_Transaccion_Remota Then
                            clsLnTarea_hh.Actualiza_Estado_Tarea(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, 2, 4, pConnection, pTransaction) 'El IdEstado 3 es Anulado                            
                        Else
                            clsLnTarea_hh.Actualiza_Estado_Tarea(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, 2, 4, lConnection, lTransaction) 'El IdEstado 3 es Anulado
                        End If

                        Liberar_Stock_Reservado_Sin_Procesar = True

                    End If

                End If

            Else
                Throw New Exception("#EJC20211216_0959: No se pudo obtener el stock reservado de la transacción.")
            End If

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#CKFK20220127 Creé esta función para eliminar la tarea sin registros
    Public Shared Function Anular_Tarea_Cambio_Ubic_O_Estado_Sin_Registros(ByRef oBeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc,
                                                                           Optional ByRef pConnection As SqlConnection = Nothing,
                                                                           Optional ByRef pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vResultadoEliminoStockReservado As Boolean = False

        Anular_Tarea_Cambio_Ubic_O_Estado_Sin_Registros = False

        Try

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Not Es_Transaccion_Remota Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            Dim lBeStockRes As New List(Of clsBeStock_res)

            If Es_Transaccion_Remota Then

                oBeTrans_ubic_hh_enc.Estado = "Anulado"

                Actualizar(oBeTrans_ubic_hh_enc, pConnection, pTransaction)

                clsLnTarea_hh.Actualiza_Estado_Tarea(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, 2, 3, pConnection, pTransaction) 'El IdEstado 3 es Anulado

                If oBeTrans_ubic_hh_enc.IdReabastecimientoLog <> 0 Then
                    clsLnTrans_reabastecimiento_log.Quitar_Tarea_HH(oBeTrans_ubic_hh_enc.IdReabastecimientoLog, pConnection, pTransaction)
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Reabasto(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pConnection, pTransaction)
                Else
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Ubicacion(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pConnection, pTransaction)
                End If

            Else

                oBeTrans_ubic_hh_enc.Estado = "Anulado"
                Actualizar(oBeTrans_ubic_hh_enc, lConnection, lTransaction)

                clsLnTarea_hh.Actualiza_Estado_Tarea(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, 2, 3, lConnection, lTransaction) 'El IdEstado 3 es Anulado

                If oBeTrans_ubic_hh_enc.IdReabastecimientoLog <> 0 Then
                    clsLnTrans_reabastecimiento_log.Quitar_Tarea_HH(oBeTrans_ubic_hh_enc.IdReabastecimientoLog, lConnection, lTransaction)
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Reabasto(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, lConnection, lTransaction)
                Else
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Ubicacion(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc, lConnection, lTransaction)
                End If

            End If

            If Not lBeStockRes Is Nothing Then

                If Not lBeStockRes.Count = 0 Then

                    For Each stockres In lBeStockRes

                        If Es_Transaccion_Remota Then
                            vResultadoEliminoStockReservado = clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(stockres.IdStockRes, pConnection, pTransaction)
                        Else
                            vResultadoEliminoStockReservado = clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(stockres.IdStockRes, lConnection, lTransaction)
                        End If

                    Next
                Else
                    vResultadoEliminoStockReservado = True
                End If
            Else
                vResultadoEliminoStockReservado = True
            End If

            If Not vResultadoEliminoStockReservado Then
                Throw New Exception("#EJC20211216_0959: No se pudo eliminar el stock reservado de la transacción.")
            End If

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

            Anular_Tarea_Cambio_Ubic_O_Estado_Sin_Registros = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Anular_Tarea_Reabasto(ByVal pIdTareaReabasto As Integer,
                                                 ByRef pConnection As SqlConnection,
                                                 ByRef pTransaction As SqlTransaction) As Boolean

        Dim vResultadoEliminoStockReservado As Boolean = False
        Dim oBeTrans_ubic_hh_enc As New clsBeTrans_ubic_hh_enc
        Dim lBeStockRes As New List(Of clsBeStock_res)

        Anular_Tarea_Reabasto = False

        Try

            oBeTrans_ubic_hh_enc = GetSingle(pIdTareaReabasto)

            If Not oBeTrans_ubic_hh_enc Is Nothing Then

                oBeTrans_ubic_hh_enc.Estado = "Anulado"
                Actualizar(oBeTrans_ubic_hh_enc,
                           pConnection,
                           pTransaction)

                clsLnTarea_hh.Actualiza_Estado_Tarea(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                 2, 3,
                                                 pConnection,
                                                 pTransaction) 'El IdEstado 3 es Anulado

                If oBeTrans_ubic_hh_enc.IdReabastecimientoLog <> 0 Then
                    clsLnTrans_reabastecimiento_log.Quitar_Tarea_HH(oBeTrans_ubic_hh_enc.IdReabastecimientoLog,
                                                                pConnection,
                                                                pTransaction)
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Reabasto(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                                                    pConnection,
                                                                                    pTransaction)
                Else
                    lBeStockRes = clsLnStock_res.Get_All_By_IdTransaccion_Para_Ubicacion(oBeTrans_ubic_hh_enc.IdTareaUbicacionEnc,
                                                                                     pConnection,
                                                                                     pTransaction)
                End If

                If Not lBeStockRes Is Nothing Then

                    If Not lBeStockRes.Count = 0 Then

                        For Each stockres In lBeStockRes

                            vResultadoEliminoStockReservado = clsLnStock_res.Eliminar_Stock_Reservado_By_IdStockRes(stockres.IdStockRes,
                                                                                                                pConnection,
                                                                                                                pTransaction)


                        Next

                    End If

                Else
                    Throw New Exception("#EJC20211216_0959: No se pudo obtener el stock reservado de la transacción.")
                End If

                If Not vResultadoEliminoStockReservado Then

                    If XtraMessageBox.Show("¿No se pudo eliminar el stock reservado, anular el cambio de ubicación de todas formas?",
                    "Stock_Reservado_Cambio_Ubicacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        Throw New Exception("#EJC20211216_0959: No se pudo eliminar el stock reservado de la transacción.")
                    End If

                End If

                Anular_Tarea_Reabasto = True

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_All_Reabastecimientos_No_Procesados_HH(ByVal IdBodega As Integer,
                                                                      ByVal lConnection As SqlConnection,
                                                                      ByVal lTransaction As SqlTransaction) As List(Of clsBeStockResByReabastecimientoLog)

        Get_All_Reabastecimientos_No_Procesados_HH = Nothing

        Dim lReturnList As New List(Of clsBeStockResByReabastecimientoLog)

        Try

            Dim vSQL As String = "SELECT IdTransaccion, IdUbicacion, trans_ubic_hh_enc.IdReabastecimientoLog
                                  FROM stock_res 
                                  INNER JOIN trans_ubic_hh_enc ON stock_res.IdTransaccion = trans_ubic_hh_enc.IdTareaUbicacionEnc
                                  WHERE 
                                  IdTransaccion IN (SELECT IdTareaUbicacionEnc
				                                      FROM VW_TransUbicacionHhEnc 
				                                      WHERE IdReabastecimientoLog <> 0
				                  AND estado IN ('Nuevo', 'Finalizado Parcial')) AND Indicador = 'REAB'
                                  AND cambio_estado = 0 AND Activo=1 "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_reabastecimiento_log As New clsBeStockResByReabastecimientoLog

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_reabastecimiento_log = New clsBeStockResByReabastecimientoLog()
                    clsLnStockResByReabastecimientoLog.Cargar(vBeTrans_reabastecimiento_log, dr)
                    lReturnList.Add(vBeTrans_reabastecimiento_log)
                Next

                Get_All_Reabastecimientos_No_Procesados_HH = lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdTareaUbicacionEnc As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal ltransaction As SqlTransaction) As clsBeTrans_ubic_hh_enc

        GetSingle = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM trans_ubic_hh_enc WHERE IdTareaUbicacionEnc=@IdTareaUbicacionEnc"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = ltransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", IdTareaUbicacionEnc)

                Dim lDT As New DataTable()
                lDTA.Fill(lDT)

                If lDT IsNot Nothing AndAlso lDT.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDT.Rows(0)
                    Dim Obj As New clsBeTrans_ubic_hh_enc()

                    Cargar(Obj, lRow)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub Crear_Tarea_Cambio_Ubicacion(ByVal IdBodega As Integer,
                                             ByVal pTipoOperacion As Integer)

        Try

            '#GT20062022_1520: enviamos la bodega para comparar contra la bodega leida x linea.
            Dim pBodega As New clsBeBodega
            pBodega = clsLnBodega.GetSingle_By_Idbodega(IdBodega)

            Dim vTipoMantenimiento As String = "Reubicación"
            Dim vEsCambioEstado As Boolean = pTipoOperacion = 3

            'Cargar_Detalle(True)

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Sub

    '#CKFK20240303 Agregué esta función para determinar si ese traslado ya existe como cambio de ubicación
    Public Shared Function Existe_Cambio_Ubicacion_Traslado(ByVal pNoPedido As String,
                                                            ByRef lConnection As SqlConnection,
                                                            ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM trans_ubic_hh_enc WHERE no_documento=@pNoPedido"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@pNoPedido", pNoPedido)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Guardar_Traslado_SAP(ByVal BePedidoCliente As clsBeI_nav_ped_traslado_enc,
                                           ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                           ByVal pIdPropietarioBodega As Integer,
                                           ByVal pHostSolicita As String,
                                           ByVal lblprg As RichTextBox,
                                           ByVal lConnection As SqlConnection,
                                           ByVal lTransaction As SqlTransaction)

        Dim vIdTareaUbicHHEnc As Integer = 0
        Dim listaBeTransUbicHHDet As New List(Of clsBeTrans_ubic_hh_det)
        Dim listaBeTransUbicHHStock As New List(Of clsBeTrans_ubic_hh_stock)
        Dim listaBeTransMovimientos As New List(Of clsBeTrans_movimientos)
        Dim lStock As New List(Of clsBeStock)
        Dim lStockDestino As New List(Of clsBeStock)

        Dim BeStock As New clsBeStock
        Dim pMov As New clsBeTrans_movimientos()
        Dim BeTransUbicHHD As New clsBeTrans_ubic_hh_det

        Try

            If Not Existe_Cambio_Ubicacion_Traslado(BePedidoCliente.No, lConnection, lTransaction) Then

                vIdTareaUbicHHEnc = Guardar_Encabezado_Traslado_SAP(BePedidoCliente,
                                                                    BeConfigEnc,
                                                                    pIdPropietarioBodega,
                                                                    lConnection,
                                                                    lTransaction)

                If clsLnTrans_ubic_hh_det.Guardar_Detalle_Traslado_SAP(vIdTareaUbicHHEnc,
                                                                       BePedidoCliente,
                                                                       pIdPropietarioBodega,
                                                                       BeConfigEnc,
                                                                       pHostSolicita,
                                                                       listaBeTransUbicHHDet,
                                                                       listaBeTransUbicHHStock,
                                                                       lConnection,
                                                                       lTransaction,
                                                                       lblprg) Then

                    For Each stock In listaBeTransUbicHHStock

                        BeStock = New clsBeStock

                        With BeStock
                            .IdBodega = BeConfigEnc.Idbodega
                            .IdStock = stock.IdStock
                            .IdPropietarioBodega = stock.IdPropietarioBodega
                            .IdProductoBodega = stock.IdProductoBodega
                            .IdProductoEstado = stock.IdProductoEstado
                            .IdPresentacion = stock.IdPresentacion
                            .IdUnidadMedida = stock.IdUnidadMedida
                            .IdUbicacion = stock.IdUbicacion
                            .IdUbicacion_anterior = stock.IdUbicacion_anterior
                            .IdRecepcionEnc = stock.IdRecepcionEnc
                            .IdRecepcionDet = stock.IdRecepcionDet
                            .IdPedidoEnc = stock.IdPedidoEnc
                            .IdPickingEnc = stock.IdPickingEnc
                            .IdDespachoEnc = stock.IdDespachoEnc
                            .Lote = stock.Lote
                            .Lic_plate = stock.Lic_plate
                            .Serial = stock.Serial
                            .Cantidad = stock.Cantidad
                            .Fecha_Ingreso = stock.Fecha_ingreso
                            .Fecha_vence = stock.Fecha_vence
                            .Uds_lic_plate = stock.Uds_lic_plate
                            .No_bulto = stock.No_bulto
                            .Fecha_Manufactura = stock.Fecha_manufactura
                            .Añada = stock.añada
                            .User_agr = stock.User_agr
                            .Fec_agr = stock.Fec_agr
                            .User_mod = stock.User_mod
                            .Fec_mod = stock.Fec_mod
                            .Activo = stock.Activo
                            .Peso = stock.Peso
                            .Temperatura = stock.Temperatura
                            .Atributo_Variante_1 = stock.Atributo_variante_1
                            .Pallet_No_Estandar = False

                        End With

                        lStock.Add(BeStock)

                    Next

                    For Each stock In listaBeTransUbicHHStock

                        pMov = New clsBeTrans_movimientos()

                        BeTransUbicHHD = New clsBeTrans_ubic_hh_det
                        BeTransUbicHHD.IdTareaUbicacionEnc = stock.IdTareaUbicacionEnc
                        BeTransUbicHHD.IdTareaUbicacionDet = stock.IdTareaUbicacionDet
                        clsLnTrans_ubic_hh_det.GetSingle(BeTransUbicHHD)

                        pMov.IdEmpresa = BeConfigEnc.Idempresa
                        pMov.IdBodegaOrigen = BeConfigEnc.Idbodega
                        pMov.IdTransaccion = stock.IdTareaUbicacionEnc
                        pMov.IdPropietarioBodega = stock.IdPropietarioBodega
                        pMov.IdProductoBodega = stock.IdProductoBodega
                        pMov.IdUbicacionOrigen = BeTransUbicHHD.IdUbicacionOrigen
                        pMov.IdUbicacionDestino = BeTransUbicHHD.IdUbicacionDestino
                        pMov.IdPresentacion = stock.IdPresentacion
                        pMov.IdEstadoOrigen = BeTransUbicHHD.IdEstadoOrigen
                        pMov.IdEstadoDestino = BeTransUbicHHD.IdEstadoDestino
                        pMov.IdUnidadMedida = stock.IdUnidadMedida
                        pMov.IdTipoTarea = 3
                        pMov.IdBodegaDestino = BeConfigEnc.Idbodega
                        pMov.IdRecepcion = stock.IdRecepcionEnc
                        pMov.IdRecepcionDet = stock.IdRecepcionDet
                        pMov.Cantidad = BeTransUbicHHD.Cantidad
                        pMov.Serie = stock.Serial
                        pMov.Peso = stock.Peso
                        pMov.Lote = stock.Lote
                        pMov.Fecha_vence = stock.Fecha_vence
                        pMov.Fecha = Now
                        pMov.Barra_pallet = stock.Lic_plate
                        pMov.Hora_ini = Now
                        pMov.Hora_fin = Now
                        pMov.Fecha_agr = Now
                        pMov.Usuario_agr = BeConfigEnc.IdUsuario
                        pMov.Cantidad_hist = stock.Cantidad
                        pMov.Peso_hist = stock.Peso

                        listaBeTransMovimientos.Add(pMov)

                    Next

                    clsLnStock.Actualizar_Stock_Por_Cambio_de_Ubicacion_Traslado(lStock, listaBeTransUbicHHDet, lStockDestino, lConnection, lTransaction)
                    clsLnTrans_movimientos.Guardar_Movimientos(vIdTareaUbicHHEnc, listaBeTransMovimientos, lConnection, lTransaction)

                    clsLnStock_res.Eliminar_Stock_Res_UbicEst(vIdTareaUbicHHEnc, "CEST", lConnection, lTransaction)

                    '#CKFK20240427 Aquí vamos a generar la nueva tarea de la HH hacia la ubicación
                    'donde el operador debe confirmar de recibido y ubicar el producto 
                    Dim vTareaUbicHHEnc As New clsBeTrans_ubic_hh_enc

                    vIdTareaUbicHHEnc = Guardar_Encabezado_Confirmacion_Traslado_SAP(BePedidoCliente,
                                                                                     BeConfigEnc,
                                                                                     pIdPropietarioBodega,
                                                                                     lConnection,
                                                                                     lTransaction)

                    vTareaUbicHHEnc = GetSingle(vIdTareaUbicHHEnc,
                                                lConnection,
                                                lTransaction)

                    If lStockDestino IsNot Nothing Then

                        If lStockDestino.Count > 0 Then

                            Dim BeTransUbicHHDet As New clsBeTrans_ubic_hh_det
                            Dim BeTransUbicHHStock As New clsBeTrans_ubic_hh_stock
                            Dim vArea As New clsBeBodega_area
                            Dim vUbicacion As New clsBeBodega_ubicacion

                            For Each StockResForTras In lStockDestino

                                BeTransUbicHHDet = New clsBeTrans_ubic_hh_det

                                vArea = New clsBeBodega_area
                                vUbicacion = New clsBeBodega_ubicacion
                                vUbicacion.IdUbicacion = StockResForTras.IdUbicacion
                                vUbicacion.IdBodega = StockResForTras.IdBodega

                                clsLnBodega_ubicacion.GetSingle(vUbicacion,
                                                               lConnection,
                                                               lTransaction)

                                vArea = clsLnBodega_area.GetSingle_By_IdArea_and_IdBodega(vUbicacion.IdArea,
                                                                                          vUbicacion.IdBodega,
                                                                                          lConnection,
                                                                                          lTransaction)

                                With BeTransUbicHHDet

                                    .IdTareaUbicacionEnc = vIdTareaUbicHHEnc
                                    .IdTareaUbicacionDet = MaxID(lConnection, lTransaction) + 1
                                    .IdStock = StockResForTras.IdStock
                                    .IdUbicacionOrigen = StockResForTras.IdUbicacion
                                    .IdUbicacionDestino = clsLnBodega_area.Get_IdUbicacion_Confirmacion_By_IdArea(vArea.IdArea, lConnection, lTransaction)
                                    .IdEstadoOrigen = StockResForTras.IdProductoEstado
                                    .IdEstadoDestino = StockResForTras.IdProductoEstado
                                    .IdOperadorBodega = 0
                                    .HoraInicio = Now
                                    .HoraFin = Now
                                    .Realizado = False
                                    .Cantidad = StockResForTras.Cantidad
                                    .Activo = True
                                    .Recibido = False
                                    .Estado = "Nuevo"
                                    .Atributo_variante_1 = ""
                                    .IdBodega = StockResForTras.IdBodega
                                    .No_Linea = 0

                                End With

                                clsLnTrans_ubic_hh_det.Insertar(BeTransUbicHHDet, lConnection, lTransaction)

                                clsPublic.Actualizar_Progreso(lblprg, "Reserva generada para IdStock: " & StockResForTras.IdStock)

                                BeTransUbicHHStock = New clsBeTrans_ubic_hh_stock

                                With BeTransUbicHHStock

                                    .IdStockTransUbicHHDet = clsLnTrans_ubic_hh_stock.MaxID(lConnection, lTransaction) + 1
                                    .IdTareaUbicacionEnc = BeTransUbicHHDet.IdTareaUbicacionEnc
                                    .IdTareaUbicacionDet = BeTransUbicHHDet.IdTareaUbicacionDet
                                    .IdStock = StockResForTras.IdStock
                                    .IdPropietarioBodega = StockResForTras.IdPropietarioBodega
                                    .IdProductoBodega = StockResForTras.IdProductoBodega
                                    .IdProductoEstado = BeTransUbicHHDet.IdEstadoDestino
                                    .IdPresentacion = StockResForTras.IdPresentacion
                                    .IdUnidadMedida = StockResForTras.IdUnidadMedida
                                    .IdUbicacion = BeTransUbicHHDet.IdUbicacionDestino
                                    .IdUbicacion_anterior = BeTransUbicHHDet.IdUbicacionOrigen
                                    .IdRecepcionEnc = StockResForTras.IdRecepcionEnc
                                    .IdRecepcionDet = StockResForTras.IdRecepcionDet
                                    .IdPedidoEnc = 0
                                    .IdPickingEnc = 0
                                    .IdDespachoEnc = 0
                                    .Lote = StockResForTras.Lote
                                    .Lic_plate = StockResForTras.Lic_plate
                                    .Serial = StockResForTras.Serial
                                    .Cantidad = StockResForTras.Cantidad
                                    .Fecha_ingreso = StockResForTras.Fecha_Ingreso
                                    .Fecha_vence = StockResForTras.Fecha_vence
                                    .Uds_lic_plate = StockResForTras.Uds_lic_plate
                                    .No_bulto = StockResForTras.No_bulto
                                    .Fecha_manufactura = StockResForTras.Fecha_Manufactura
                                    .añada = StockResForTras.Añada
                                    .User_agr = 1
                                    .Fec_agr = Now
                                    .User_mod = 1
                                    .Fec_mod = Now
                                    .Activo = True
                                    .Peso = StockResForTras.Peso
                                    .Temperatura = 0
                                    .Fecha_mov_hist = Now
                                    .Atributo_variante_1 = StockResForTras.Atributo_Variante_1

                                End With

                                clsLnTrans_ubic_hh_stock.Insertar(BeTransUbicHHStock, lConnection, lTransaction)

                                clsPublic.Actualizar_Progreso(lblprg, "Reserva generada para IdStock: " & StockResForTras.IdStock)

                            Next

                            Dim vIdPropietario As Integer = clsLnPropietarios.Get_IdPropietario(vUbicacion.IdBodega,
                                                                                                vTareaUbicHHEnc.IdPropietarioBodega,
                                                                                                lConnection,
                                                                                                lTransaction)

                            clsLnTarea_hh.Guardar_Tarea_Ubicacion_HH(vTareaUbicHHEnc, vIdPropietario, lConnection, lTransaction)
                            clsLnStock_res.Guardar_Stock_Res(vTareaUbicHHEnc.IdTareaUbicacionEnc, lStockDestino, pHostSolicita, lConnection, lTransaction)

                        End If

                    End If

                Else

                    Dim UbicHHEnc As New clsBeTrans_ubic_hh_enc
                    UbicHHEnc.IdTareaUbicacionEnc = vIdTareaUbicHHEnc
                    Eliminar(UbicHHEnc,
                             lConnection,
                             lTransaction)

                End If


            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Shared Function Guardar_Encabezado_Traslado_SAP(ByVal BePedidoCliente As clsBeI_nav_ped_traslado_enc,
                                                            ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                            ByVal IdPropietarioBodega As Integer,
                                                            ByRef lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As Integer

        Guardar_Encabezado_Traslado_SAP = 0

        Dim IdPropietario As Integer = 0
        Dim gBeTransubicacionHHEnc As New clsBeTrans_ubic_hh_enc

        Try

            gBeTransubicacionHHEnc.IdPropietarioBodega = IdPropietarioBodega
            gBeTransubicacionHHEnc.IdMotivoUbicacion = 1
            gBeTransubicacionHHEnc.FechaInicio = Now
            gBeTransubicacionHHEnc.HoraInicio = Now
            gBeTransubicacionHHEnc.FechaFin = Now.AddHours(1)
            gBeTransubicacionHHEnc.HoraFin = Now.AddHours(1)
            gBeTransubicacionHHEnc.Activo = True
            gBeTransubicacionHHEnc.Observacion = BePedidoCliente.Transfer_from_Contact + " - " + BePedidoCliente.Transfer_to_Name
            gBeTransubicacionHHEnc.User_agr = BeConfigEnc.User_agr
            gBeTransubicacionHHEnc.Fec_agr = Now
            gBeTransubicacionHHEnc.User_mod = BeConfigEnc.User_agr
            gBeTransubicacionHHEnc.Fec_mod = Now
            gBeTransubicacionHHEnc.Operador_por_linea = False
            '#CKFK20240402 Agregamos que la tarea se guarde automáticamente en la ubicación destino
            gBeTransubicacionHHEnc.Ubicacion_con_hh = False
            gBeTransubicacionHHEnc.Cambio_estado = True
            gBeTransubicacionHHEnc.IdPrioridad = 1
            gBeTransubicacionHHEnc.IdTipoTarea = clsDataContractDI.tTipoTarea.CEST
            gBeTransubicacionHHEnc.IdBodega = BeConfigEnc.Idbodega
            gBeTransubicacionHHEnc.Es_Traslado_SAP = True
            gBeTransubicacionHHEnc.No_Documento = BePedidoCliente.No
            gBeTransubicacionHHEnc.Estado = "Finalizado"
            gBeTransubicacionHHEnc.IsNew = False
            gBeTransubicacionHHEnc.IdTareaUbicacionEnc = MaxID(lConnection, lTransaction) + 1

            Insertar(gBeTransubicacionHHEnc, lConnection, lTransaction)

            Return gBeTransubicacionHHEnc.IdTareaUbicacionEnc

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Function Guardar_Encabezado_Confirmacion_Traslado_SAP(ByVal BePedidoCliente As clsBeI_nav_ped_traslado_enc,
                                                                         ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                                         ByVal IdPropietarioBodega As Integer,
                                                                         ByRef lConnection As SqlConnection,
                                                                         ByVal lTransaction As SqlTransaction) As Integer

        Guardar_Encabezado_Confirmacion_Traslado_SAP = 0

        Dim IdPropietario As Integer = 0
        Dim gBeTransubicacionHHEnc As New clsBeTrans_ubic_hh_enc

        Try

            gBeTransubicacionHHEnc.IdPropietarioBodega = IdPropietarioBodega
            gBeTransubicacionHHEnc.IdMotivoUbicacion = 1
            gBeTransubicacionHHEnc.FechaInicio = Now
            gBeTransubicacionHHEnc.HoraInicio = Now
            gBeTransubicacionHHEnc.FechaFin = Now.AddHours(1)
            gBeTransubicacionHHEnc.HoraFin = Now.AddHours(1)
            gBeTransubicacionHHEnc.Activo = True
            gBeTransubicacionHHEnc.Observacion = BePedidoCliente.Transfer_from_Contact + " - " + BePedidoCliente.Transfer_to_Name
            gBeTransubicacionHHEnc.User_agr = BeConfigEnc.User_agr
            gBeTransubicacionHHEnc.Fec_agr = Now
            gBeTransubicacionHHEnc.User_mod = BeConfigEnc.User_agr
            gBeTransubicacionHHEnc.Fec_mod = Now
            gBeTransubicacionHHEnc.Operador_por_linea = False
            gBeTransubicacionHHEnc.Ubicacion_con_hh = True
            gBeTransubicacionHHEnc.Cambio_estado = False
            gBeTransubicacionHHEnc.IdPrioridad = 1
            gBeTransubicacionHHEnc.IdTipoTarea = clsDataContractDI.tTipoTarea.UBIC
            gBeTransubicacionHHEnc.IdBodega = BeConfigEnc.Idbodega
            gBeTransubicacionHHEnc.Es_Traslado_SAP = True
            gBeTransubicacionHHEnc.No_Documento = BePedidoCliente.No
            gBeTransubicacionHHEnc.Estado = "Nuevo"
            gBeTransubicacionHHEnc.IsNew = False
            gBeTransubicacionHHEnc.IdTareaUbicacionEnc = MaxID(lConnection, lTransaction) + 1

            Insertar(gBeTransubicacionHHEnc, lConnection, lTransaction)

            Return gBeTransubicacionHHEnc.IdTareaUbicacionEnc

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Guardar_Transaccion(ByVal BeTrans_ubic_hh_enc As clsBeTrans_ubic_hh_enc,
                                          ByVal pListObjDet As List(Of clsBeTrans_ubic_hh_det),
                                          ByVal pListObjOp As List(Of clsBeTrans_ubic_hh_op),
                                          ByVal pListObjMov As List(Of clsBeTrans_movimientos),
                                          ByVal Con_HH As Boolean,
                                          ByVal pIdPropietario As Integer,
                                          ByVal pListObjStock As List(Of clsBeStock),
                                          ByVal pListObjTransUbicTarimaDisponibles As List(Of clsBeTrans_ubic_tarima),
                                          ByVal pListObjTransUbicTarimasUsadas As List(Of clsBeTrans_ubic_tarima),
                                          ByVal pIdTareaHH As Integer,
                                          ByVal pHostSolicita As String,
                                          ByVal lConnection As SqlConnection,
                                          ByVal lTransaction As SqlTransaction)

        Try

            Guarda_Trans_Ubic_HH_Enc(BeTrans_ubic_hh_enc, lConnection, lTransaction)

            If BeTrans_ubic_hh_enc.IdReabastecimientoLog > 0 Then

                '#EJC20240624: Marcar reabasto como procesado si se ejecuta por BOF.
                Dim BeTransReabastecimientoLog As New clsBeTrans_reabastecimiento_log
                BeTransReabastecimientoLog = clsLnTrans_reabastecimiento_log.GetSingle(BeTrans_ubic_hh_enc.IdReabastecimientoLog, lConnection, lTransaction)

                If Not BeTransReabastecimientoLog Is Nothing Then

                    BeTransReabastecimientoLog.Procesado_HH = True
                    BeTransReabastecimientoLog.Fecha_Procesamiento_BOF = Now
                    BeTransReabastecimientoLog.Hora_Procesamiento_BOF = Now
                    BeTransReabastecimientoLog.IdTareaUbicacionEnc = BeTrans_ubic_hh_enc.IdTareaUbicacionEnc

                    If clsLnTrans_reabastecimiento_log.Actualizar_Procesamiento_BOF(BeTransReabastecimientoLog,
                                                                                    lConnection,
                                                                                    lTransaction) Then


                    End If

                End If

            End If

            clsLnTrans_ubic_hh_det.Guardar_Trans_Ubic_HH_Det(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pListObjDet, lConnection, lTransaction)
            clsLnTrans_ubic_hh_stock.Guardar_Trans_Ubic_HH_Stock(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pListObjStock, pListObjDet, lConnection, lTransaction)

            If Con_HH Then
                clsLnTarea_hh.Guardar_Tarea_Ubicacion_HH(BeTrans_ubic_hh_enc, pIdPropietario, lConnection, lTransaction)
                clsLnStock_res.Guardar_Stock_Res(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pListObjStock, pHostSolicita, lConnection, lTransaction)
            Else
                '#EJC20171016_0228PM: Cuando se cambia la transacción a BOF, eliminar tarea de la HH.
                clsLnTarea_hh.Eliminar_By_IdTareaHH(pIdTareaHH, lConnection, lTransaction)
                '#EJC20171018_0636PM: Si la transacción era antes con HH y se cambio a BOF eliminar el stock reservado.
                '#CKFK20240624 Si la tarea es de tipo reabasto el tipo de transaccion es diferente
                If BeTrans_ubic_hh_enc.IdReabastecimientoLog > 0 Then
                    clsLnStock_res.Eliminar_Stock_Res_Reabasto(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, lConnection, lTransaction)
                Else
                    clsLnStock_res.Eliminar_Stock_Res_Ubic(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, lConnection, lTransaction)
                End If
                clsLnStock.Actualizar_Stock_Por_Cambio_de_Ubicacion(pListObjStock, pListObjDet, (BeTrans_ubic_hh_enc.IdReabastecimientoLog <> 0), lConnection, lTransaction)
            End If

            'GT20122021: envio el pObjEnc, con IsNew para insert o update
            'clsLnTrans_ubic_hh_op.Guarda_Operadores(pObjEnc.IdTareaUbicacionEnc, pListObjOp, lConnection, lTransaction)
            clsLnTrans_ubic_hh_op.Guarda_Operadores_By_Enc(BeTrans_ubic_hh_enc, pListObjOp, lConnection, lTransaction)
            If Not Con_HH Then clsLnTrans_movimientos.Guardar_Movimientos(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pListObjMov, lConnection, lTransaction)
            clsLnTrans_ubic_tarima.Guardar_Tarimas_Usadas(BeTrans_ubic_hh_enc.IdTareaUbicacionEnc, pListObjTransUbicTarimasUsadas, lConnection, lTransaction)
            clsLnTrans_ubic_tarima.Guardar_Tarimas_Disponibles(pListObjTransUbicTarimaDisponibles, lConnection, lTransaction)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Shared Function Get_rpt_Control_Calidad_Cambio_Estado(ByVal IdTareaUbicacionEnc As Integer) As DataTable

        Get_rpt_Control_Calidad_Cambio_Estado = Nothing

        Try

            Dim vSQL As String = "SELECT * from VW_CONTROLCALIDAD_CAMBIOESTADO 
                                  WHERE IdTareaUbicacionEnc = @IdTareaUbicacionEnc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", IdTareaUbicacionEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_rpt_Control_Calidad_Cambio_Estado = lDataTable

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

End Class