Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTransacciones_log

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdTransaccionLog),0) FROM transacciones_log"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

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

    Public Shared Function GetAll() As List(Of clsBeTransacciones_log)

        Dim lReturnList As New List(Of clsBeTransacciones_log)

        Try

            Dim vSQL As String = "SELECT DISTINCT IdProductoBodega,IdPresentacion,IdUbicacion FROM transacciones_log WHERE CONVERT(VARCHAR(8),fec_agr,112)= CONVERT(VARCHAR(8),GETDATE(),112)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeTransacciones_log

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeTransacciones_log

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdPresentacion") IsNot DBNull.Value AndAlso lRow("IdPresentacion") IsNot Nothing Then
                                    Obj.IdPresentacion = CType(lRow("IdPresentacion"), Integer)
                                End If

                                If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                    Obj.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
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

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="oBeTransacciones_log"></param>
    ''' <param name="pConnection"></param>
    ''' <param name="pTransaction"></param>
    ''' <remarks></remarks>
    ''' 
    Public Shared Sub Delete_By_Producto(ByVal oBeTransacciones_log As clsBeTransacciones_log,
                                         ByVal pConnection As SqlConnection,
                                         ByVal pTransaction As SqlTransaction)
        Try

            Dim sp As String = " Delete from Transacciones_log" &
             "  Where(IdProductoBodega = @IdProductoBodega)" &
             "  AND (IdPresentacion = @IdPresentacion)" &
             "  AND (IdProductoEstado = @IdProductoEstado)" &
             "  AND (IdUnidadMedida = @IdUnidadMedida) " &
             "  AND (IdUbicacion = @IdUbicacion)"


            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IdProductoBodega", oBeTransacciones_log.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IdPresentacion", oBeTransacciones_log.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IdProductoEstado", oBeTransacciones_log.IdProductoEstado))
            cmd.Parameters.Add(New SqlParameter("@IdUnidadMedida", oBeTransacciones_log.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IdUbicacion", oBeTransacciones_log.IdUbicacion))

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pObjListT"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Posponer(ByVal pObjListT As List(Of clsBeTransacciones_log)) As Boolean

        Dim ObjT As New clsLnTransacciones_log()

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim lMax As Integer = MaxID(lConnection, lTransaction)
            For Each Obj As clsBeTransacciones_log In pObjListT
                If Obj.IsNew Then
                    lMax += 1
                    Obj.IdTransaccionLog = lMax
                    ' El Id 3 corresponde a que Se pospusieron todos los productos de la lista
                    Obj.IdObservacion = 3
                    ObjT.Insertar(Obj, lConnection, lTransaction)
                Else
                    ObjT.Actualizar(Obj, lConnection, lTransaction)
                End If
            Next

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    'Public Shared Function Enviar_Tareas(ByVal pObjListT As List(Of clsBeTrans_reabastecimiento_log),
    '                                     ByVal pHost As String,
    '                                     ByVal pIdBodega As Integer,
    '                                     ByVal pUser As String) As Boolean

    '    Dim ObjT As New clsLnTransacciones_log()
    '    Dim pStockResList As New List(Of clsBeStock_res)
    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing
    '    Dim BeTransReabastecimientoLog As New clsBeTrans_reabastecimiento_log()

    '    Try

    '        lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

    '        Dim lMax As Integer = MaxID(lConnection, lTransaction)
    '        Dim lMaxTarea As Integer = clsLnTarea_hh.MaxID(lConnection, lTransaction)
    '        Dim lMaxTareaHHEnc As Integer = clsLnTrans_ubic_hh_enc.MaxID(lConnection, lTransaction)

    '        For Each BeStockReabasto In pObjListT

    '            lMax += 1
    '            lMaxTarea += 1
    '            lMaxTareaHHEnc += 1

    '            BeStockReabasto.IdReabastecimientoLog = lMax

    '            If BeStockReabasto.StockInferior AndAlso BeStockReabasto.Disponible = 0 Then

    '                If BeStockReabasto.Disponible = 0 Then
    '                    '' El Id 2 corresponde a que No se pudo abastecer debido a que el stock era insuficiente
    '                    BeStockReabasto.IdObservacion = 2
    '                    ObjT.Insertar(BeStockReabasto, lConnection, lTransaction)
    '                End If

    '            ElseIf BeStockReabasto.IsNew Then

    '                If BeStockReabasto.Cantidad_reabasto > BeStockReabasto.CantBodega AndAlso BeStockReabasto.StockInferior Then
    '                    BeStockReabasto.Cantidad_reabasto = BeStockReabasto.Cantidad_reabasto - BeStockReabasto.CantBodega
    '                    BeStockReabasto.IdObservacion = 2
    '                    ObjT.Insertar(BeStockReabasto, lConnection, lTransaction)
    '                    BeStockReabasto.Cantidad_reabasto = BeStockReabasto.CantBodega
    '                Else
    '                    BeStockReabasto.Cantidad_reabasto = BeStockReabasto.Cantidad_reabasto
    '                    BeStockReabasto.IdObservacion = 3
    '                    ObjT.Insertar(BeStockReabasto, lConnection, lTransaction)
    '                    BeStockReabasto.Cantidad_reabasto = BeStockReabasto.CantBodega
    '                End If

    '                Dim r As New clsBeStock_res
    '                r.IdStockRes = 0
    '                r.IdTransaccion = lMax
    '                r.Indicador = "UBI"
    '                r.Cantidad = BeStockReabasto.CantUbicar
    '                r.User_agr = pUser
    '                r.Fec_agr = Now
    '                r.User_mod = pUser
    '                r.Fec_mod = Now
    '                r.Host = pHost
    '                r.IdProductoEstado = 0
    '                r.IdUnidadMedida = BeStockReabasto.IdUnidadMedidaBasica
    '                r.IdPresentacion = BeStockReabasto.IdPresentacionAbastercerCon
    '                r.IdProductoEstado = BeStockReabasto.IdProductoEstado
    '                r.IdProductoBodega = BeStockReabasto.IdProductoBodega
    '                r.IdPropietarioBodega = BeStockReabasto.IdPropietarioBodega
    '                r.IdUbicacion = BeStockReabasto.IdUbicacion

    '                '#EJC20171004: Comentariado por agregado de transacción
    '                clsLnStock_res.Reserva_Stock(r, 0, pHost, lConnection, lTransaction)

    '                '''' ACA DEBERIA DE MANDARSE EL IdPropietario NO el IdPropietarioBodega
    '                Dim BeTareaHH As New clsBeTarea_hh() With {.IdTareahh = lMaxTarea,
    '                                                     .IdPropietario = BeStockReabasto.IdPropietario,
    '                                                     .IdEstado = 1,
    '                                                     .IdTransaccion = r.IdTransaccion,
    '                                                     .Tipo = 0,
    '                                                     .FechaInicio = Now,
    '                                                     .FechaFin = Now.AddDays(1),
    '                                                     .DiaCompleto = False,
    '                                                     .Ubicacion = "",
    '                                                     .Descripcion = "Cambio de Ubicación por Reabastecimiento",
    '                                                     .Asunto = "Cambio de Ubicación",
    '                                                     .IdPrioridad = 3,
    '                                                     .IdTipoTarea = 2,
    '                                                     .IdBodega = BeStockReabasto.IdBodega,
    '                                                     .IdMuelle = 0}

    '                pStockResList = clsLnStock_res.Get_All_By_IdTransaccion_Para_Ubicacion(lMax, lConnection, lTransaction)

    '                Guardar_Cambio_Ubic(BeStockReabasto.IdPropietarioBodega,
    '                                    pIdBodega,
    '                                    pHost,
    '                                    pUser,
    '                                    pStockResList,
    '                                    lMaxTareaHHEnc,
    '                                    BeStockReabasto.IdUbicacion,
    '                                    lConnection, lTransaction)

    '                clsLnTarea_hh.Insertar(BeTareaHH,
    '                                       lConnection,
    '                                       lTransaction)

    '                Delete_By_Producto(BeStockReabasto,
    '                                   lConnection,
    '                                   lTransaction)

    '            End If

    '        Next

    '        lTransaction.Commit()

    '        Return True

    '    Catch ex As Exception
    '        If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex
    '    Finally
    '        If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '        If lTransaction IsNot Nothing Then lTransaction.Dispose()
    '        If lConnection IsNot Nothing Then lConnection.Dispose()
    '    End Try

    'End Function

    Public pObjEnc As New clsBeTrans_ubic_hh_enc

    Private Shared Function Guardar_Cambio_Ubic(ByVal pIdPropietarioBodega As Integer,
                                                ByVal pIdBodega As Integer,
                                                ByVal pHost As String,
                                                ByVal pUser As String,
                                                ByVal pListStockRes As List(Of clsBeStock_res),
                                                ByVal IdTareaHHEnc As Integer,
                                                ByVal pIdUbicDestino As Integer,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction) As Boolean

        Dim pObjEnc As New clsBeTrans_ubic_hh_enc
        Dim BeUbicHHDet As New clsBeTrans_ubic_hh_det
        Dim pListObjDet As New List(Of clsBeTrans_ubic_hh_det)
        Dim pListObjStock As New List(Of clsBeStock)
        Dim pObjStock As New clsBeStock
        Dim pListObjOp As New List(Of clsBeTrans_ubic_hh_op)
        Dim pObjOp As New clsBeTrans_ubic_hh_op

        Try

            pObjEnc.IdPropietarioBodega = pIdPropietarioBodega
            pObjEnc.IdMotivoUbicacion = clsLnBodega.Get_Id_Motivo_Ubic_Reabasto(pIdBodega, lConnection, lTransaction)
            pObjEnc.FechaInicio = Now
            pObjEnc.HoraInicio = Now
            pObjEnc.FechaFin = Now
            pObjEnc.HoraFin = Now
            pObjEnc.Activo = True
            pObjEnc.Observacion = "Reabasto de producto"
            pObjEnc.User_agr = pUser
            pObjEnc.Fec_agr = Now
            pObjEnc.User_mod = pUser
            pObjEnc.Fec_mod = Now
            pObjEnc.Operador_por_linea = False
            pObjEnc.Ubicacion_con_hh = True
            pObjEnc.Cambio_estado = False
            pObjEnc.Asunto = "Reabasto de producto"
            pObjEnc.IdPrioridad = 1
            pObjEnc.IdTipoTarea = 2
            pObjEnc.IdBodega = pIdBodega
            pObjEnc.Estado = "Nuevo"
            pObjEnc.IsNew = True

            pObjEnc.IdTareaUbicacionEnc = IdTareaHHEnc
            clsLnTrans_ubic_hh_enc.Insertar(pObjEnc, lConnection, lTransaction)

            Dim lMaxTareaHH As Integer

            For Each st In pListStockRes

                lMaxTareaHH += 1

                BeUbicHHDet.IdTareaUbicacionEnc = 0
                BeUbicHHDet.IdTareaUbicacionDet = lMaxTareaHH
                BeUbicHHDet.IdStock = st.IdStock

                '#EJC20210303:Porqué la ubicación anteiror ?
                'BeUbicHHDet.IdUbicacionOrigen = st.Ubicacion_ant
                BeUbicHHDet.IdUbicacionOrigen = st.IdUbicacion
                BeUbicHHDet.IdUbicacionDestino = pIdUbicDestino

                BeUbicHHDet.IdEstadoOrigen = st.IdProductoEstado
                BeUbicHHDet.IdEstadoDestino = st.IdProductoEstado
                BeUbicHHDet.IdOperadorBodega = 0
                BeUbicHHDet.HoraInicio = Now
                BeUbicHHDet.HoraFin = Now
                BeUbicHHDet.Realizado = False
                BeUbicHHDet.Cantidad = st.Cantidad
                BeUbicHHDet.Activo = True
                BeUbicHHDet.Recibido = 0
                BeUbicHHDet.Estado = st.Estado
                BeUbicHHDet.Atributo_variante_1 = st.Atributo_Variante_1
                BeUbicHHDet.IdBodega = pIdBodega

                pListObjDet.Add(BeUbicHHDet)

                pObjStock.IdBodega = pIdBodega
                pObjStock.IdStock = st.IdStock
                pObjStock.IdPropietarioBodega = st.IdPropietarioBodega
                pObjStock.IdProductoBodega = st.IdProductoBodega
                pObjStock.IdProductoEstado = st.IdProductoEstado
                pObjStock.IdPresentacion = st.IdPresentacion
                pObjStock.IdUnidadMedida = st.IdUnidadMedida
                pObjStock.IdUbicacion = pIdUbicDestino ' st.IdUbicacion
                pObjStock.IdUbicacion_anterior = st.Ubicacion_ant
                pObjStock.IdRecepcionEnc = st.IdRecepcion
                pObjStock.IdRecepcionDet = 0
                pObjStock.IdPedidoEnc = st.IdPedido
                pObjStock.IdPickingEnc = st.IdPicking
                pObjStock.IdDespachoEnc = st.IdDespacho
                pObjStock.Lote = st.Lote
                pObjStock.Lic_plate = st.Lic_plate
                pObjStock.Serial = st.Serial
                pObjStock.Cantidad = st.Cantidad
                pObjStock.Fecha_Ingreso = st.Fecha_ingreso
                pObjStock.Fecha_vence = st.Fecha_vence
                pObjStock.Uds_lic_plate = st.Uds_lic_plate
                pObjStock.No_bulto = st.No_bulto
                pObjStock.Fecha_Manufactura = st.Fecha_manufactura
                pObjStock.Añada = st.añada
                pObjStock.User_agr = st.User_agr
                pObjStock.Fec_agr = Date.Now
                pObjStock.User_mod = st.User_mod
                pObjStock.Fec_mod = Date.Now
                pObjStock.Activo = True
                pObjStock.Peso = st.Peso
                pObjStock.Temperatura = 0
                pObjStock.Atributo_Variante_1 = st.Atributo_Variante_1
                pObjStock.Pallet_No_Estandar = False

                pListObjStock.Add(pObjStock)

            Next

            'Aún no sé a qué operadores se les asigna

            'pObjOp.IdTransUbicHhOp = clsLnTrans_ubic_hh_op.MaxID(lConnection, lTransaction)
            'pObjOp.IdTareaUbicacionEnc = pObjEnc.IdTareaUbicacionEnc
            'pObjOp.IdOperadorBodega = IdOperadorBodega
            'pObjOp.User_agr = pUser
            'pObjOp.Fec_agr = Now
            'pObjOp.User_mod = pUser
            'pObjOp.Fec_mod = Now

            'pListObjOp.Add(pObjOp)

            clsLnTrans_ubic_hh_det.Guardar_Trans_Ubic_HH_Det(pObjEnc.IdTareaUbicacionEnc, pListObjDet, lConnection, lTransaction)
            clsLnTrans_ubic_hh_stock.Guardar_Trans_Ubic_HH_Stock(pObjEnc.IdTareaUbicacionEnc, pListObjStock, pListObjDet, lConnection, lTransaction)

            'clsLnTrans_ubic_hh_op.Guarda_Operadores(pObjEnc.IdTareaUbicacionEnc, pListObjOp, lConnection, lTransaction)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Sub Guarda_Trans_Ubic_HH_Enc(ByRef pObjEnc As clsBeTrans_ubic_hh_enc,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)

        Try

            If pObjEnc.IdTareaUbicacionEnc = 0 Then
                pObjEnc.IdTareaUbicacionEnc = MaxID(lConnection, lTransaction) + 1
                clsLnTrans_ubic_hh_enc.Insertar(pObjEnc, lConnection, lTransaction)
            Else
                clsLnTrans_ubic_hh_enc.Actualizar(pObjEnc, lConnection, lTransaction)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub
End Class
