Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors

Partial Public Class clsLnTrans_reabastecimiento_log

    Public Shared Function Get_All_Generados(ByVal IdBodega As Integer,
                                             ByVal pFechaDel As Date,
                                             ByVal pFechaAl As Date,
                                             ByVal IdReabasto As Integer) As List(Of clsBeTrans_reabastecimiento_log)

        Get_All_Generados = Nothing

        Dim lReturnList As New List(Of clsBeTrans_reabastecimiento_log)

        Try

            '#EJC20210303:  Buscar los reabastecimientos por bodega/fecha/IdRellenado | 
            'Si la HH no lo ha procesado entonces no debería mostrar nuevamente la alerta, por eso se consulta si la HH ya lo procesí o no.
            Dim vSQL As String = "SELECT * FROM Trans_reabastecimiento_log WHERE IdBodega = @IdBodega AND IdRellenado = @IdReabasto AND Procesado_HH =0"

            vSQL += " AND cast(Fecha_Generacion_Inexistencia AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdReabasto", IdReabasto)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_reabastecimiento_log = New clsBeTrans_reabastecimiento_log()
                            Cargar(vBeTrans_reabastecimiento_log, dr)
                            lReturnList.Add(vBeTrans_reabastecimiento_log)
                        Next

                        Get_All_Generados = lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Generados(ByVal IdBodega As Integer,
                                             ByVal pFechaDel As Date,
                                             ByVal pFechaAl As Date,
                                             ByVal IdReabasto As Integer,
                                             ByVal IdProductoBodega As Integer,
                                             ByVal IdUbicacion As Integer,
                                             ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_reabastecimiento_log)

        Get_All_Generados = Nothing

        Dim lReturnList As New List(Of clsBeTrans_reabastecimiento_log)

        Try

            '#EJC20210303:  Buscar los reabastecimientos por bodega/fecha/IdRellenado | 
            'Si la HH no lo ha procesado entonces no debería mostrar nuevamente la alerta, por eso se consulta si la HH ya lo procesí o no.
            Dim vSQL As String = "SELECT * FROM Trans_reabastecimiento_log 
								  WHERE IdBodega = @IdBodega 
								  AND IdRellenado = @IdReabasto 
								  AND IdProductoBodega = @IdProductoBodega 
								  AND IdUbicacion = @IdUbicacion 
							      AND Procesado_HH =0"

            vSQL += " AND cast(Fecha_Generacion_Inexistencia AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdReabasto", IdReabasto)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", IdUbicacion)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_reabastecimiento_log = New clsBeTrans_reabastecimiento_log()
                    Cargar(vBeTrans_reabastecimiento_log, dr)
                    lReturnList.Add(vBeTrans_reabastecimiento_log)
                Next

                Get_All_Generados = lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Generados(ByVal IdBodega As Integer,
                                             ByVal pFechaDel As Date,
                                             ByVal pFechaAl As Date,
                                             ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_reabastecimiento_log)

        Get_All_Generados = Nothing

        Dim lReturnList As New List(Of clsBeTrans_reabastecimiento_log)

        Try

            '#EJC20210303:  Buscar los reabastecimientos por bodega/fecha/IdRellenado | 
            'Si la HH no lo ha procesado entonces no debería mostrar nuevamente la alerta, por eso se consulta si la HH ya lo procesí o no.
            Dim vSQL As String = "SELECT * FROM Trans_reabastecimiento_log 
								  WHERE IdBodega = @IdBodega 
							      AND Procesado_HH =0"

            vSQL += " AND cast(Fecha_Generacion_Inexistencia AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_reabastecimiento_log = New clsBeTrans_reabastecimiento_log()
                    Cargar(vBeTrans_reabastecimiento_log, dr)
                    lReturnList.Add(vBeTrans_reabastecimiento_log)
                Next

                Get_All_Generados = lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Pendientes_De_Procesar_Trans(ByVal IdBodega As Integer,
                                                                ByVal pFechaDel As Date,
                                                                ByVal pFechaAl As Date,
                                                                ByVal IdReabasto As Integer,
                                                                ByVal IdProductoBodega As Integer,
                                                                ByVal IdUbicacion As Integer,
                                                                ByVal lConnection As SqlConnection,
                                                                ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_reabastecimiento_log)

        Get_All_Pendientes_De_Procesar_Trans = Nothing

        Dim lReturnList As New List(Of clsBeTrans_reabastecimiento_log)

        Try

            '#EJC20210303:  Buscar los reabastecimientos por bodega/fecha/IdRellenado | 
            'Si la HH no lo ha procesado entonces no debería mostrar nuevamente la alerta, por eso se consulta si la HH ya lo procesí o no.
            Dim vSQL As String = "SELECT * FROM Trans_reabastecimiento_log 
								  WHERE IdBodega = @IdBodega 
								  AND IdRellenado = @IdReabasto 
								  AND IdProductoBodega = @IdProductoBodega 
								  AND IdUbicacion = @IdUbicacion 
							      AND Procesado_HH =0"

            vSQL += " AND cast(Fecha_Procesamiento_BOF AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdReabasto", IdReabasto)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", IdUbicacion)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_reabastecimiento_log = New clsBeTrans_reabastecimiento_log()
                    Cargar(vBeTrans_reabastecimiento_log, dr)
                    lReturnList.Add(vBeTrans_reabastecimiento_log)
                Next

                Get_All_Pendientes_De_Procesar_Trans = lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Pendientes_De_Procesar_Trans(ByVal IdBodega As Integer,
                                                                ByVal pFechaDel As Date,
                                                                ByVal pFechaAl As Date,
                                                                ByVal lConnection As SqlConnection,
                                                                ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_reabastecimiento_log)

        Get_All_Pendientes_De_Procesar_Trans = Nothing

        Dim lReturnList As New List(Of clsBeTrans_reabastecimiento_log)

        Try

            '#EJC20210303:  Buscar los reabastecimientos por bodega/fecha/IdRellenado | 
            'Si la HH no lo ha procesado entonces no debería mostrar nuevamente la alerta, por eso se consulta si la HH ya lo procesí o no.
            'Dim vSQL As String = "SELECT * FROM Trans_reabastecimiento_log 
            '					  WHERE IdBodega = @IdBodega 
            '				      AND Procesado_HH =0
            '					  AND IdTareaUbicacionEnc = 0	"

            'vSQL += " AND cast(Fecha_Generacion_Inexistencia AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
            '	   " AND " & FormatoFechas.fFecha(pFechaAl)

            Dim vSQL As String = "SELECT * FROM Trans_reabastecimiento_log 
									WHERE IdTareaUbicacionEnc IN (
									SELECT IdTareaUbicacionEnc FROM trans_reabastecimiento_log 
										WHERE IdReabastecimientoLog IN (
											SELECT trans_ubic_hh_enc.IdReabastecimientoLog
											FROM stock_res 
											INNER JOIN trans_ubic_hh_enc ON stock_res.IdTransaccion = trans_ubic_hh_enc.IdTareaUbicacionEnc
											WHERE 
											IdTransaccion IN (SELECT IdTareaUbicacionEnc
														  FROM VW_TransUbicacionHhEnc 
														  WHERE IdReabastecimientoLog <> 0
														  AND estado IN ('Nuevo', 'Finalizado Parcial')) AND Indicador = 'REAB'AND cambio_estado = 0 AND Activo=1 
									))"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_reabastecimiento_log = New clsBeTrans_reabastecimiento_log()
                    Cargar(vBeTrans_reabastecimiento_log, dr)
                    lReturnList.Add(vBeTrans_reabastecimiento_log)
                Next

                Get_All_Pendientes_De_Procesar_Trans = lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Pendientes_De_Procesar(ByVal IdBodega As Integer,
                                                          ByVal pFechaDel As Date,
                                                          ByVal pFechaAl As Date,
                                                          ByVal IdReabasto As Integer) As List(Of clsBeTrans_reabastecimiento_log)

        Get_All_Pendientes_De_Procesar = Nothing

        Dim lReturnList As New List(Of clsBeTrans_reabastecimiento_log)

        Try

            '#EJC20210303:  Buscar los reabastecimientos por bodega/fecha/IdRellenado | 
            'Si la HH no lo ha procesado entonces no debería mostrar nuevamente la alerta, por eso se consulta si la HH ya lo procesí o no.
            Dim vSQL As String = "SELECT * FROM Trans_reabastecimiento_log 
								  WHERE IdBodega = @IdBodega 
								  AND IdRellenado = @IdReabasto 
							      AND Procesado_HH =0"

            vSQL += " AND cast(Fecha_Procesamiento_BOF AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdReabasto", IdReabasto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_reabastecimiento_log = New clsBeTrans_reabastecimiento_log()
                            Cargar(vBeTrans_reabastecimiento_log, dr)
                            lReturnList.Add(vBeTrans_reabastecimiento_log)
                        Next

                        Get_All_Pendientes_De_Procesar = lReturnList

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Pendientes_De_Procesar(ByVal IdBodega As Integer,
                                                          ByVal pFechaDel As Date,
                                                          ByVal pFechaAl As Date,
                                                          ByVal IdReabasto As Integer,
                                                          ByVal IdProductoBodega As Integer,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_reabastecimiento_log)

        Get_All_Pendientes_De_Procesar = Nothing

        Dim lReturnList As New List(Of clsBeTrans_reabastecimiento_log)

        Try

            '#EJC20210303:  Buscar los reabastecimientos por bodega/fecha/IdRellenado | 
            'Si la HH no lo ha procesado entonces no debería mostrar nuevamente la alerta, por eso se consulta si la HH ya lo procesí o no.
            Dim vSQL As String = "SELECT * FROM Trans_reabastecimiento_log 
								  WHERE IdBodega = @IdBodega 
								  AND IdRellenado = @IdReabasto
								  AND IdProductoBodega = @IdProductoBodega
								  AND Procesado_HH =0"

            vSQL += " AND cast(Fecha_Procesamiento_BOF AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdReabasto", IdReabasto)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_reabastecimiento_log = New clsBeTrans_reabastecimiento_log()
                    Cargar(vBeTrans_reabastecimiento_log, dr)
                    lReturnList.Add(vBeTrans_reabastecimiento_log)
                Next

                Get_All_Pendientes_De_Procesar = lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK20230217 Cree esta función para saber si está pendiente el reabasto
    Public Shared Function Get_Pendiente_De_Procesar(ByVal IdBodega As Integer,
                                                     ByVal pFechaDel As Date,
                                                     ByVal pFechaAl As Date,
                                                     ByVal IdReabasto As Integer,
                                                     ByVal IdProductoBodega As Integer,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_reabastecimiento_log)

        Get_Pendiente_De_Procesar = Nothing

        Dim lReturnList As New List(Of clsBeTrans_reabastecimiento_log)

        Try

            '#EJC20210303:  Buscar los reabastecimientos por bodega/fecha/IdRellenado | 
            'Si la HH no lo ha procesado entonces no debería mostrar nuevamente la alerta, por eso se consulta si la HH ya lo procesí o no.
            Dim vSQL As String = "SELECT * FROM Trans_reabastecimiento_log 
								  WHERE IdBodega = @IdBodega 
								  AND IdRellenado = @IdReabasto
								  AND IdProductoBodega = @IdProductoBodega
								  AND Procesado_HH =0"

            vSQL += " AND cast(Fecha_Procesamiento_BOF AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdReabasto", IdReabasto)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", IdProductoBodega)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_reabastecimiento_log = New clsBeTrans_reabastecimiento_log()
                    Cargar(vBeTrans_reabastecimiento_log, dr)
                    lReturnList.Add(vBeTrans_reabastecimiento_log)
                Next

                Get_Pendiente_De_Procesar = lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reabastecimientos_Productos(ByVal pIdBodega As Integer) As List(Of clsBeTrans_reabastecimiento_log)

        Dim lReturnList As New List(Of clsBeTrans_reabastecimiento_log)

        Try

            Dim vSQL As String = "SELECT * FROM VW_Revision_Producto WHERE IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeReabastecimientoLog As New clsBeTrans_reabastecimiento_log()

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim vIdentificadorVirtual As Integer = 1

                            For Each lRow As DataRow In lDataTable.Rows

                                BeReabastecimientoLog = New clsBeTrans_reabastecimiento_log()
                                Cargar(BeReabastecimientoLog, lRow)
                                BeReabastecimientoLog.IdReabastecimientoLog = vIdentificadorVirtual
                                BeReabastecimientoLog.IsNew = True
                                lReturnList.Add(BeReabastecimientoLog)
                                vIdentificadorVirtual += 1

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

    Public Shared Function Get_Reabastecimientos_Productos(ByVal pIdBodega As Integer,
                                                           ByVal lConnection As SqlConnection,
                                                           ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_reabastecimiento_log)

        Dim lReturnList As New List(Of clsBeTrans_reabastecimiento_log)

        Get_Reabastecimientos_Productos = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_Revision_Producto 
								  WHERE IdBodega = @IdBodega ORDER BY Codigo_Producto"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeReabastecimientoLog As New clsBeTrans_reabastecimiento_log()

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vIdentificadorVirtual As Integer = 1

                    For Each lRow As DataRow In lDataTable.Rows

                        BeReabastecimientoLog = New clsBeTrans_reabastecimiento_log()
                        Cargar(BeReabastecimientoLog, lRow)
                        BeReabastecimientoLog.IdReabastecimientoLog = vIdentificadorVirtual
                        BeReabastecimientoLog.IsNew = True
                        lReturnList.Add(BeReabastecimientoLog)
                        vIdentificadorVirtual += 1

                    Next

                    Get_Reabastecimientos_Productos = lReturnList

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Enviar_Tareas(ByVal lBeReabastoLog As List(Of clsBeTrans_reabastecimiento_log),
                                         ByVal pHost As String,
                                         ByVal pIdBodega As Integer,
                                         ByVal pUser As String,
                                         ByVal pIdEmpresa As Integer,
                                         ByVal pIdOperadorBodegaModificado As Integer,
                                         ByRef pLblPrg As RichTextBox) As Boolean

        Dim BeTransaccionesLog As New clsLnTransacciones_log()
        Dim pStockResList As New List(Of clsBeStock_res)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim BeTransReabastecimientoLog As New clsBeTrans_reabastecimiento_log()
        Dim BeStockUbicDestino As New clsBeStock

        Enviar_Tareas = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Dim lMaxTarea As Integer = clsLnTarea_hh.MaxID(lConnection, lTransaction)
            'Dim lMaxTareaHHEnc As Integer = clsLnTrans_ubic_hh_enc.MaxID(lConnection, lTransaction)

            Dim vResult As Integer = 0

            For Each BeStockReabasto In lBeReabastoLog


                If BeStockReabasto.Stock_Inferior AndAlso BeStockReabasto.Stock_Disponible = 0 Then

                    If BeStockReabasto.Stock_Disponible = 0 Then
                        BeStockReabasto.Cancelado = True
                    End If

                ElseIf BeStockReabasto.IsNew Then

                    If BeStockReabasto.Cantidad_A_Ubicar > BeStockReabasto.Stock_Disponible AndAlso BeStockReabasto.Stock_Inferior Then

                        BeStockReabasto.Cantidad_A_Ubicar = BeStockReabasto.Cantidad_A_Ubicar - BeStockReabasto.Stock_Disponible
                        BeStockReabasto.Cancelado = True
                        BeStockReabasto.Cantidad_A_Ubicar = BeStockReabasto.Stock_Disponible

                    Else

                        BeStockReabasto.Enviado = True
                        BeStockReabasto.Fecha_Procesamiento_BOF = New Date(Now.Year, Now.Month, Now.Day)
                        BeStockReabasto.Hora_Procesamiento_BOF = Now
                        BeStockReabasto.Cancelado = False
                        BeStockReabasto.IdTareaUbicacionEnc = clsLnTarea_hh.MaxID(lConnection, lTransaction) + 1

                    End If

                    Dim BeStockRes As New clsBeStock_res
                    BeStockRes.IdStockRes = 0
                    '#CKFK 20211216 En el IdTransacción de stock_res debe ser el lMaxTareaHHEnc en IdReabastecimientoLog
                    BeStockRes.IdTransaccion = clsLnTrans_ubic_hh_enc.MaxID(lConnection, lTransaction) + 1 'BeStockReabasto.IdReabastecimientoLog
                    BeStockRes.Indicador = "REAB"

                    '#EJC20210304: Carol dice que en el stock_res debo enviar la cantidad en UMBAS pero la HH
                    'Realiza el procedimiento incorrecto al procesarlo de esa forma
                    'Enviaré temporalmente la cantidad en la presentación.
                    If BeStockReabasto.IdPresentacionAbastercerCon <> 0 Then
                        'r.Cantidad = BeStockReabasto.Cantidad_A_Ubicar * BeStockReabasto.Factor
                        '#CKFK 20211216 Enviaremos la cantidad en UMBAS cuando la cantidad a ubicar está en presentación
                        If BeStockReabasto.IdPresentacion = 0 Then
                            BeStockRes.Cantidad = Math.Round(BeStockReabasto.Cantidad_A_Ubicar * BeStockReabasto.FactorAbastecerCon, 6)
                        Else
                            BeStockRes.Cantidad = BeStockReabasto.Cantidad_A_Ubicar '* BeStockReabasto.FactorAbastecerCon
                        End If

                        'BeStockRes.Cantidad = BeStockReabasto.Cantidad_A_Ubicar
                    Else
                        BeStockRes.Cantidad = BeStockReabasto.Cantidad_A_Ubicar
                    End If

                    If BeStockRes.Cantidad = 0 Then
                        clsPublic.Actualizar_Progreso(pLblPrg, "Error: 20211123_0708: No pudo determinar la cantidad a ubicar con precisión para la ubicación: " & BeStockReabasto.Ubicacion & " se omitirá la tarea de rebasto. ")
                        Continue For
                    End If

                    BeStockRes.User_agr = pUser
                    BeStockRes.Fec_agr = Now
                    BeStockRes.User_mod = pUser
                    BeStockRes.Fec_mod = Now
                    BeStockRes.Host = pHost
                    BeStockRes.IdProductoEstado = 0
                    BeStockRes.IdUnidadMedida = BeStockReabasto.IdUnidadMedidaBasica
                    BeStockRes.IdPresentacion = BeStockReabasto.IdPresentacionAbastercerCon
                    BeStockRes.IdProductoEstado = BeStockReabasto.IdProductoEstado
                    BeStockRes.IdProductoBodega = BeStockReabasto.IdProductoBodega
                    BeStockRes.IdPropietarioBodega = BeStockReabasto.IdPropietarioBodega
                    BeStockRes.IdUbicacion = BeStockReabasto.IdUbicacion
                    BeStockRes.IdBodega = BeStockReabasto.IdBodega

                    Dim vCantidadDisponible As Double = 0
                    Dim BeConfigEnc As New clsBeI_nav_config_enc
                    Dim pListStockRes As New List(Of clsBeStock_res)
                    BeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(pIdBodega,
                                                                                             pIdEmpresa,
                                                                                             lConnection,
                                                                                             lTransaction)


                    Dim vEspacioOcupadoUbicacionDestino As Double = 0

                    If BeConfigEnc.Considerar_Disponibilidad_Ubicacion_Reabasto Then

                        clsPublic.CopyObject(BeStockRes, BeStockUbicDestino)

                        Dim vCantidadSFUbicDestino As Double = 0

                        If clsLnStock.Get_Existencia_Disp_By_IdProducto_And_IdUbicacion(BeStockUbicDestino,
                                                                                        pIdBodega,
                                                                                        BeStockReabasto.IdUbicacion,
                                                                                        True,
                                                                                        False,
                                                                                        0,
                                                                                        True,
                                                                                        lConnection,
                                                                                        lTransaction) Then

                            If BeStockUbicDestino.IdPresentacion = 0 Then
                                vEspacioOcupadoUbicacionDestino = BeStockUbicDestino.Cantidad
                            Else
                                vEspacioOcupadoUbicacionDestino = Math.Round(BeStockUbicDestino.Cantidad * BeStockReabasto.FactorAbastecerCon, 6)
                            End If

                        End If

                    End If

                    '#EJC202302130117:Considerar_Disponibilidad_Ubicacion_Reabasto 
                    If BeConfigEnc.Considerar_Disponibilidad_Ubicacion_Reabasto AndAlso Not (vEspacioOcupadoUbicacionDestino = 0) Then


                        Dim vMensajeNoRellenado As String = "La tarea de reabasto para: " & BeStockReabasto.Codigo_Producto & " no se generará porque la ubicación de destino no está vacía."
                        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), vMensajeNoRellenado),
                                            "Reabasto",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)

                        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), vMensajeNoRellenado)
                        clsLnLog_error_wms.Agregar_Error(vMsgError)

                        Continue For

                    End If

                    If clsLnStock_res.Reserva_Stock_From_Reabasto(BeStockRes,
                                                                  DiasVencimiento:=0,
                                                                  pHost,
                                                                  BeConfigEnc,
                                                                  vCantidadDisponible,
                                                                  BeStockReabasto.IdPropietarioBodega,
                                                                  pListStockResOUT:=pListStockRes,
                                                                  lConnection,
                                                                  lTransaction,
                                                                  0,
                                                                  True) Then

                        Dim BeTareaHH As New clsBeTarea_hh() With {.IdTareahh = BeStockReabasto.IdTareaUbicacionEnc,
                                                                 .IdPropietario = BeStockReabasto.IdPropietario,
                                                                 .IdEstado = 1,
                                                                 .IdTransaccion = BeStockRes.IdTransaccion,
                                                                 .Tipo = 0,
                                                                 .FechaInicio = Now,
                                                                 .FechaFin = Now.AddDays(1),
                                                                 .DiaCompleto = False,
                                                                 .Ubicacion = BeStockReabasto.Ubicacion,
                                                                 .Descripcion = "Reabastecimiento " & BeStockReabasto.Codigo_Producto,
                                                                 .Asunto = "Reabastecimiento",
                                                                 .IdPrioridad = 3,
                                                                 .IdTipoTarea = 2,
                                                                 .IdBodega = BeStockReabasto.IdBodega,
                                                                 .IdMuelle = 0}

                        pStockResList = clsLnStock_res.Get_All_Para_Reabasto_By_IdTransaccion_And_IdProductoBodega(BeStockRes.IdTransaccion,
                                                                                                                   BeStockReabasto.IdProductoBodega,
                                                                                                                   lConnection,
                                                                                                                   lTransaction)

                        If Not pStockResList Is Nothing Then

                            If Guardar_Cambio_Ubic(BeStockReabasto.IdPropietarioBodega,
                                                   pIdBodega,
                                                   pHost,
                                                   pUser,
                                                   pStockResList,
                                                   BeStockRes.IdTransaccion,
                                                   BeStockReabasto.IdUbicacion,
                                                   BeStockReabasto.IdReabastecimientoLog,
                                                   BeStockReabasto.Codigo_Producto + "-" + BeStockReabasto.Nombre_Producto,
                                                   lConnection,
                                                   lTransaction,
                                                   pIdOperadorBodegaModificado) Then

                                clsLnTarea_hh.Insertar(BeTareaHH,
                                                       lConnection,
                                                       lTransaction)

                            End If

                        Else
                            'Throw New Exception("No se pudo realizar la reserva de stock. código de error: 20201123_1105.")

                            pLblPrg.AppendText(vbNewLine)
                            pLblPrg.AppendText("Error: 20201123_1105: No se pudo realizar la reserva de stock se omitirá la tarea de rebasto. ")
                            pLblPrg.AppendText(vbNewLine)
                            pLblPrg.Refresh()
                            pLblPrg.SelectionStart = pLblPrg.TextLength
                            pLblPrg.ScrollToCaret()

                            Continue For

                        End If

                    Else

                        pLblPrg.AppendText(vbNewLine)
                        pLblPrg.AppendText("Error: 20201123_1105: No se pudo realizar la reserva de stock se omitirá la tarea de rebasto. ")
                        pLblPrg.AppendText(vbNewLine)
                        pLblPrg.Refresh()
                        pLblPrg.SelectionStart = pLblPrg.TextLength
                        pLblPrg.ScrollToCaret()

                        Continue For

                    End If

                End If

            Next

            lTransaction.Commit()

            Enviar_Tareas = True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdReabastecimientoLog),0) FROM Trans_reabastecimiento_log"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

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

    Private Shared Function Guardar_Cambio_Ubic(ByVal pIdPropietarioBodega As Integer,
                                                ByVal pIdBodega As Integer,
                                                ByVal pHost As String,
                                                ByVal pUser As String,
                                                ByVal pListStockRes As List(Of clsBeStock_res),
                                                ByVal IdTareaHHEnc As Integer,
                                                ByVal pIdUbicDestino As Integer,
                                                ByVal IdReabastecimientoLog As Integer,
                                                ByVal pProducto As String,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction,
                                                Optional IdOperadorBodegaModificado As Integer = 0) As Boolean

        Dim BeTransUbicHHEnc As New clsBeTrans_ubic_hh_enc
        Dim BeUbicHHDet As New clsBeTrans_ubic_hh_det
        Dim pListBeTransUbicHHDet As New List(Of clsBeTrans_ubic_hh_det)
        Dim pListBeStock As New List(Of clsBeStock)
        Dim pBeStock As New clsBeStock
        Dim pListTransUbicHHOp As New List(Of clsBeTrans_ubic_hh_op)
        Dim pBeTransUbicHHOp As New clsBeTrans_ubic_hh_op
        Dim BeTransReabastecimientoLog As New clsBeTrans_reabastecimiento_log
        Dim BeReglaRellenadoProducto As New clsBeProducto_rellenado

        Guardar_Cambio_Ubic = False

        Try

            Dim vIdMotivoUbicacionPorDefectoReabasto As Integer = clsLnBodega.Get_Id_Motivo_Ubic_Reabasto(pIdBodega,
                                                                                                          lConnection,
                                                                                                          lTransaction)

            '#CKFK 20211120 Voy a llamar a cargar el objeto para poder obtener el Operador Bodega
            BeTransReabastecimientoLog = GetSingle(IdReabastecimientoLog, lConnection, lTransaction)

            If vIdMotivoUbicacionPorDefectoReabasto = 0 Then
                Throw New Exception("Error de configuración: No está definido el motivo de ubicación por defecto para el reabasto, debe configurarlo en el mantenimiento de bodega.")
            End If

            If BeTransReabastecimientoLog Is Nothing Then
                Throw New Exception("No se obtuvo la configuración de rellenado. #20211123_0718")
            End If

            BeReglaRellenadoProducto = clsLnProducto_rellenado.GetSingle(BeTransReabastecimientoLog.IdRellenado,
                                                                         lConnection,
                                                                         lTransaction)

            If BeReglaRellenadoProducto Is Nothing Then
                Throw New Exception("No se encontró la configuración de reabastecimiento con identificador:" & BeTransReabastecimientoLog.IdRellenado)
            End If

            BeTransUbicHHEnc.IdPropietarioBodega = pIdPropietarioBodega
            BeTransUbicHHEnc.IdMotivoUbicacion = vIdMotivoUbicacionPorDefectoReabasto
            BeTransUbicHHEnc.FechaInicio = Now
            BeTransUbicHHEnc.HoraInicio = Now
            BeTransUbicHHEnc.FechaFin = Now
            BeTransUbicHHEnc.HoraFin = Now
            BeTransUbicHHEnc.Activo = True
            BeTransUbicHHEnc.Observacion = "Reabasto " + pProducto.Substring(0, IIf(pProducto.Length < 141, pProducto.Length, 141))
            BeTransUbicHHEnc.User_agr = pUser
            BeTransUbicHHEnc.Fec_agr = Now
            BeTransUbicHHEnc.User_mod = pUser
            BeTransUbicHHEnc.Fec_mod = Now
            BeTransUbicHHEnc.Operador_por_linea = False
            BeTransUbicHHEnc.Ubicacion_con_hh = True
            BeTransUbicHHEnc.Cambio_estado = False
            BeTransUbicHHEnc.Asunto = "Reabasto " + pProducto
            BeTransUbicHHEnc.IdPrioridad = 1
            BeTransUbicHHEnc.IdTipoTarea = 2
            BeTransUbicHHEnc.IdBodega = pIdBodega
            BeTransUbicHHEnc.Estado = "Nuevo"
            BeTransUbicHHEnc.IsNew = True
            BeTransUbicHHEnc.IdReabastecimientoLog = IdReabastecimientoLog
            BeTransUbicHHEnc.IdTareaUbicacionEnc = IdTareaHHEnc
            BeTransUbicHHEnc.Operador_por_linea = (IdReabastecimientoLog <> 0)

            clsLnTrans_ubic_hh_enc.Insertar(BeTransUbicHHEnc, lConnection, lTransaction)

            Dim lMaxTareaHH As Integer
            Dim Factor As Double

            If Not pListStockRes Is Nothing Then

                If pListStockRes.Count > 0 Then

                    Dim vIdOperadorBodega As Integer

                    If IdOperadorBodegaModificado <> 0 Then
                        vIdOperadorBodega = IdOperadorBodegaModificado
                    Else
                        vIdOperadorBodega = clsLnOperador_bodega.Get_IdOperadorBodega_By_IdOperador_Activo(BeReglaRellenadoProducto.IdOperadorDefecto,
                                                                                                                      BeReglaRellenadoProducto.IdBodega,
                                                                                                                      lConnection,
                                                                                                                      lTransaction)
                    End If


                    If vIdOperadorBodega = 0 Then
                        Throw New Exception("El operador por defecto para la tareas de reabastecimiento no está creado o no está activo (Mantenimiento -> Producto -> Reabast -> Operador por Defecto:" & BeTransReabastecimientoLog.IdRellenado)
                    End If

                    For Each st In pListStockRes

                        lMaxTareaHH += 1

                        BeUbicHHDet = New clsBeTrans_ubic_hh_det()
                        BeUbicHHDet.IdTareaUbicacionEnc = 0
                        BeUbicHHDet.IdTareaUbicacionDet = lMaxTareaHH
                        BeUbicHHDet.IdStock = st.IdStock
                        '#EJC20210303:Por qué la ubicación anterior ?
                        'BeUbicHHDet.IdUbicacionOrigen = st.Ubicacion_ant
                        BeUbicHHDet.IdUbicacionOrigen = st.IdUbicacion
                        BeUbicHHDet.IdUbicacionDestino = pIdUbicDestino
                        BeUbicHHDet.IdEstadoOrigen = st.IdProductoEstado
                        BeUbicHHDet.IdEstadoDestino = st.IdProductoEstado
                        '#CKFK 20211120 Estoy colocando el operador por defecto definido en el producto rellenado
                        BeUbicHHDet.IdOperadorBodega = vIdOperadorBodega
                        BeUbicHHDet.HoraInicio = Now
                        BeUbicHHDet.HoraFin = Now
                        BeUbicHHDet.Realizado = False

                        If st.IdPresentacion <> 0 Then
                            Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(st.IdProductoBodega,
                                                                                               st.IdPresentacion,
                                                                                               lConnection,
                                                                                               lTransaction)
                            BeUbicHHDet.Cantidad = Math.Round(st.Cantidad / Factor, 6)
                        Else
                            BeUbicHHDet.Cantidad = st.Cantidad
                        End If

                        BeUbicHHDet.Activo = True
                        BeUbicHHDet.Recibido = 0
                        BeUbicHHDet.Estado = st.Estado
                        BeUbicHHDet.Atributo_variante_1 = st.Atributo_Variante_1
                        BeUbicHHDet.IdBodega = pIdBodega

                        pListBeTransUbicHHDet.Add(BeUbicHHDet)

                        pBeStock = New clsBeStock()
                        pBeStock.IdBodega = pIdBodega
                        pBeStock.IdStock = st.IdStock
                        pBeStock.IdPropietarioBodega = st.IdPropietarioBodega
                        pBeStock.IdProductoBodega = st.IdProductoBodega
                        pBeStock.IdProductoEstado = st.IdProductoEstado
                        pBeStock.IdPresentacion = st.IdPresentacion
                        pBeStock.IdUnidadMedida = st.IdUnidadMedida
                        pBeStock.IdUbicacion = pIdUbicDestino ' st.IdUbicacion
                        pBeStock.IdUbicacion_anterior = st.Ubicacion_ant
                        pBeStock.IdRecepcionEnc = st.IdRecepcion
                        pBeStock.IdRecepcionDet = 0
                        pBeStock.IdPedidoEnc = st.IdPedido
                        pBeStock.IdPickingEnc = st.IdPicking
                        pBeStock.IdDespachoEnc = st.IdDespacho
                        pBeStock.Lote = st.Lote
                        pBeStock.Lic_plate = st.Lic_plate
                        pBeStock.Serial = st.Serial
                        pBeStock.Cantidad = st.Cantidad
                        pBeStock.Fecha_Ingreso = st.Fecha_ingreso
                        pBeStock.Fecha_vence = st.Fecha_vence
                        pBeStock.Uds_lic_plate = st.Uds_lic_plate
                        pBeStock.No_bulto = st.No_bulto
                        pBeStock.Fecha_Manufactura = st.Fecha_manufactura
                        pBeStock.añada = st.añada
                        pBeStock.User_agr = st.User_agr
                        pBeStock.Fec_agr = Date.Now
                        pBeStock.User_mod = st.User_mod
                        pBeStock.Fec_mod = Date.Now
                        pBeStock.Activo = True
                        pBeStock.Peso = st.Peso
                        pBeStock.Temperatura = 0
                        pBeStock.Atributo_Variante_1 = st.Atributo_Variante_1
                        pBeStock.Pallet_No_Estandar = False
                        pListBeStock.Add(pBeStock)

                    Next

                    '#CKFK 20211120 Ahora que ya tengo operador bodega ya puedo llenar esta tabla
                    'aún no sé a qué operadores se les asigna
                    If vIdOperadorBodega <> 0 Then

                        pBeTransUbicHHOp.IdTransUbicHhOp = clsLnTrans_ubic_hh_op.MaxID(lConnection,
                                                                             lTransaction)
                        pBeTransUbicHHOp.IdTareaUbicacionEnc = BeTransUbicHHEnc.IdTareaUbicacionEnc
                        pBeTransUbicHHOp.IdOperadorBodega = vIdOperadorBodega
                        pBeTransUbicHHOp.User_agr = pUser
                        pBeTransUbicHHOp.Fec_agr = Now
                        pBeTransUbicHHOp.User_mod = pUser
                        pBeTransUbicHHOp.Fec_mod = Now
                        pListTransUbicHHOp.Add(pBeTransUbicHHOp)

                    Else

                        Dim lOperadores As New List(Of clsBeTrans_ubic_hh_op)
                        Dim lOperadoresBodega As New List(Of clsBeOperador_bodega)

                        lOperadoresBodega = clsLnOperador_bodega.Get_All_By_IdBodega(pIdBodega,
                                                                                     lConnection,
                                                                                     lTransaction)

                        If Not lOperadoresBodega Is Nothing Then

                            If lOperadoresBodega.Count > 0 Then

                                Dim vMaxIdOperador As Integer = clsLnTrans_ubic_hh_op.MaxID(lConnection,
                                                                                            lTransaction)

                                For Each OpBod In lOperadoresBodega

                                    pBeTransUbicHHOp = New clsBeTrans_ubic_hh_op()
                                    pBeTransUbicHHOp.IdTransUbicHhOp = vMaxIdOperador
                                    pBeTransUbicHHOp.IdTareaUbicacionEnc = BeTransUbicHHEnc.IdTareaUbicacionEnc
                                    pBeTransUbicHHOp.IdOperadorBodega = BeTransReabastecimientoLog.IdOperadorBodega
                                    pBeTransUbicHHOp.User_agr = pUser
                                    pBeTransUbicHHOp.Fec_agr = Now
                                    pBeTransUbicHHOp.User_mod = pUser
                                    pBeTransUbicHHOp.Fec_mod = Now
                                    pListTransUbicHHOp.Add(pBeTransUbicHHOp)
                                    vMaxIdOperador += 1

                                Next

                            End If

                        End If

                    End If

                    If clsLnTrans_ubic_hh_det.Guardar_Trans_Ubic_HH_Det(BeTransUbicHHEnc.IdTareaUbicacionEnc,
                                                                        pListBeTransUbicHHDet,
                                                                        lConnection,
                                                                        lTransaction) Then

                        If clsLnTrans_ubic_hh_stock.Guardar_Trans_Ubic_HH_Stock(BeTransUbicHHEnc.IdTareaUbicacionEnc,
                                                                                pListBeStock,
                                                                                pListBeTransUbicHHDet,
                                                                                lConnection,
                                                                                lTransaction) Then

                            If clsLnTrans_ubic_hh_op.Guarda_Operadores(BeTransUbicHHEnc.IdTareaUbicacionEnc,
                                                                       pListTransUbicHHOp,
                                                                       lConnection,
                                                                       lTransaction) Then

                                BeTransReabastecimientoLog.IdTareaUbicacionEnc = BeTransUbicHHEnc.IdTareaUbicacionEnc

                                If Actualizar_Procesamiento_BOF(BeTransReabastecimientoLog,
                                                                lConnection,
                                                                lTransaction) Then

                                    Guardar_Cambio_Ubic = True

                                End If

                            End If


                        End If

                    End If

                End If

            End If


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_IdRellenado_By_IdReabastecimientoLog(ByRef oBeTrans_reabastecimiento_log As clsBeTrans_reabastecimiento_log,
                                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_reabastecimiento_log")
            Upd.Add("IdRellenado", "@IdRellenado", DataType.Parametro)
            Upd.Where("IdReabastecimientoLog = @IdReabastecimientoLog and IdBodega = @IdBodega")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDRELLENADO", oBeTrans_reabastecimiento_log.IdRellenado))
            cmd.Parameters.Add(New SqlParameter("@IDREABASTECIMIENTOLOG", oBeTrans_reabastecimiento_log.IdReabastecimientoLog))
            cmd.Parameters.Add(New SqlParameter("@IDBODEGA", oBeTrans_reabastecimiento_log.IdBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Procesamiento_BOF(ByRef oBeTrans_reabastecimiento_log As clsBeTrans_reabastecimiento_log, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_reabastecimiento_log")
            Upd.Add("fecha_procesamiento_bof", "@fecha_procesamiento_bof", DataType.Parametro)
            Upd.Add("hora_procesamiento_bof", "@hora_procesamiento_bof", DataType.Parametro)
            Upd.Add("IdTareaUbicacionEnc", "@IdTareaUbicacionEnc", DataType.Parametro)
            Upd.Add("procesado_hh", "@procesado_hh", DataType.Parametro)
            Upd.Where("IdReabastecimientoLog = @IdReabastecimientoLog AND IdRellenado = @IdRellenado")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREABASTECIMIENTOLOG", oBeTrans_reabastecimiento_log.IdReabastecimientoLog))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESAMIENTO_BOF", oBeTrans_reabastecimiento_log.Fecha_Procesamiento_BOF))
            cmd.Parameters.Add(New SqlParameter("@HORA_PROCESAMIENTO_BOF", oBeTrans_reabastecimiento_log.Hora_Procesamiento_BOF))
            cmd.Parameters.Add(New SqlParameter("@IDTAREAUBICACIONENC", oBeTrans_reabastecimiento_log.IdTareaUbicacionEnc))
            cmd.Parameters.Add(New SqlParameter("@IDRELLENADO", oBeTrans_reabastecimiento_log.IdRellenado))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_HH", oBeTrans_reabastecimiento_log.Procesado_HH))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Procesamiento_HH(ByRef oBeTrans_reabastecimiento_log As clsBeTrans_reabastecimiento_log,
                                                       Optional ByVal pConection As SqlConnection = Nothing,
                                                       Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_reabastecimiento_log")
            Upd.Add("fecha_procesamiento_hh", "@fecha_procesamiento_hh", DataType.Parametro)
            Upd.Add("procesado_hh", "@procesado_hh", DataType.Parametro)
            Upd.Where("IdReabastecimientoLog = @IdReabastecimientoLog")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREABASTECIMIENTOLOG", oBeTrans_reabastecimiento_log.IdReabastecimientoLog))
            cmd.Parameters.Add(New SqlParameter("@FECHA_PROCESAMIENTO_HH", oBeTrans_reabastecimiento_log.Fecha_Procesamiento_HH))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_HH", oBeTrans_reabastecimiento_log.Procesado_HH))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Quitar_Tarea_HH(ByVal pIdReabastecimientoLog As Integer,
                                           Optional ByVal pConection As SqlConnection = Nothing,
                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_reabastecimiento_log")
            Upd.Add("IdTareaUbicacionEnc", "@IdTareaUbicacionEnc", DataType.Parametro)
            Upd.Where("IdReabastecimientoLog = @IdReabastecimientoLog")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDREABASTECIMIENTOLOG", pIdReabastecimientoLog))
            cmd.Parameters.Add(New SqlParameter("@IdTareaUbicacionEnc", 0))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


    Public Shared Function GetSingle_By_IdTareaUbicacionEnc(ByVal pIdTareaUbicacionEnc As Integer) As clsBeTrans_reabastecimiento_log

        GetSingle_By_IdTareaUbicacionEnc = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_reabastecimiento_log " &
            " Where(IdTareaUbicacionEnc = @IdTareaUbicacionEnc)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", pIdTareaUbicacionEnc)
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeTrans_reabastecimiento_log, lDataTable.Rows(0))
                            GetSingle_By_IdTareaUbicacionEnc = vBeTrans_reabastecimiento_log
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

    Public Shared Function GetSingle_By_IdTareaUbicacionEnc(ByVal pIdTareaUbicacionEnc As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As clsBeTrans_reabastecimiento_log

        GetSingle_By_IdTareaUbicacionEnc = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_reabastecimiento_log Where (IdTareaUbicacionEnc = @IdTareaUbicacionEnc)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTareaUbicacionEnc", pIdTareaUbicacionEnc)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeTrans_reabastecimiento_log, lDataTable.Rows(0))
                    GetSingle_By_IdTareaUbicacionEnc = vBeTrans_reabastecimiento_log
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal pIdReabastecimientoLog As Integer) As clsBeTrans_reabastecimiento_log

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_reabastecimiento_log" &
            " Where(IdReabastecimientoLog = @IdReabastecimientoLog)"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Dim pBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log()
                            Cargar(vBeTrans_reabastecimiento_log, lDataTable.Rows(0))
                            GetSingle = vBeTrans_reabastecimiento_log
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

    Public Shared Function GetSingle(ByVal pIdReabastecimientoLog As Integer,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction) As clsBeTrans_reabastecimiento_log

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_reabastecimiento_log Where(IdReabastecimientoLog = @IdReabastecimientoLog)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdReabastecimientoLog", pIdReabastecimientoLog)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Dim pBeTrans_reabastecimiento_log As New clsBeTrans_reabastecimiento_log()
                    Cargar(vBeTrans_reabastecimiento_log, lDataTable.Rows(0))
                    GetSingle = vBeTrans_reabastecimiento_log
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Creado por Carolina Fuentes
    ''' </summary>
    ''' <param name="pIdRellenado"></param>
    ''' <param name="pConnection"></param>
    ''' <param name="pTransaction"></param>
    ''' <remarks></remarks>
    ''' 
    Public Shared Sub Eliminar_By_IdRellenado(ByVal pIdRellenado As Integer,
                                              ByVal pConnection As SqlConnection,
                                              ByVal pTransaction As SqlTransaction)
        Try

            Dim sp As String = " Delete from trans_reabastecimiento_log 
                                 Where(IdRellenado = @IdRellenado)"

            Dim cmd As New SqlCommand(sp, pConnection, pTransaction) With {.CommandType = CommandType.Text}

            cmd.Parameters.Add(New SqlParameter("@IdRellenado", pIdRellenado))

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub


    Public Shared Function Eliminar_By_IdReabastecimientoLog(ByVal pIdReabastecimientoLog As Integer) As Integer

        Try


            Dim sp As String = " Delete from trans_reabastecimiento_log 
                                 Where(IdReabastecimientoLog = @IdReabastecimientoLog)"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                    cmd.Parameters.Add(New SqlParameter("@IdReabastecimientoLog", pIdReabastecimientoLog))

                    Eliminar_By_IdReabastecimientoLog = cmd.ExecuteNonQuery()

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

End Class
