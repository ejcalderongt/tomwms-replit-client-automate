Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnMarcaje

    Public Shared Function Agregar_Marcaje(ByVal pIdEmpresa As Integer,
                                           ByVal pIdBodega As Integer,
                                           ByVal pIdOperador As Integer,
                                           ByVal pIdDispositivo As String,
                                           ByVal pEsSalida As Boolean) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim rowsafected As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'Función para insertar el marcaje tomando en consideración todos los parámetros...

            Dim lHorariosOP As New List(Of clsBeVW_operador_horario)
            lHorariosOP = clsLnVW_operador_horario.Get_All_Horarios_By_IdOperador_And_IdBodega(pIdOperador,
                                                                                               pIdBodega,
                                                                                               lConnection,
                                                                                               lTransaction)

            If Not lHorariosOP Is Nothing Then

                Dim DiaActual As Integer = Now.DayOfWeek

                '#CKFK20221127 Si día actual de la semana es igual a 0 lo vamos a cambiar por 7 para que no de error.
                If DiaActual = 0 Then
                    DiaActual = 7
                End If

                Dim BeHorarioDia As clsBeVW_operador_horario = lHorariosOP.Find(Function(x) x.Dia = DiaActual _
                                                                                AndAlso x.HorarioActivo = True _
                                                                                AndAlso x.HorarioLaboralDetActivo = True _
                                                                                AndAlso x.JornadaLaboralActivo = True _
                                                                                AndAlso x.OperadorActivo = True _
                                                                                AndAlso x.OperadorBodegaActivo = True)

                If Not BeHorarioDia Is Nothing Then

                    Dim BeNuevoMarcaje As New clsBeMarcaje
                    BeNuevoMarcaje.IdMarcaje = MaxID(lConnection, lTransaction) + 1
                    BeNuevoMarcaje.IdEmpresa = pIdEmpresa
                    BeNuevoMarcaje.IdBodega = pIdBodega
                    BeNuevoMarcaje.IdDispositivo = pIdDispositivo
                    BeNuevoMarcaje.IdHorarioLaboral = BeHorarioDia.IdHorarioLaboralEnc
                    BeNuevoMarcaje.IdOperador = pIdOperador

                    Dim BeParametrosMarcajeAnterior As New clsBeMarcaje
                    BeParametrosMarcajeAnterior.IdEmpresa = pIdEmpresa
                    BeParametrosMarcajeAnterior.IdBodega = pIdBodega
                    BeParametrosMarcajeAnterior.IdOperador = pIdOperador
                    BeParametrosMarcajeAnterior.Fec_lectura = Now

                    BeNuevoMarcaje.Hora_inicio_horario = BeHorarioDia.Hora_inicio
                    BeNuevoMarcaje.Hora_fin_horario = BeHorarioDia.Hora_fin
                    BeNuevoMarcaje.Hora_lectura = Now
                    BeNuevoMarcaje.IdDispositivo = pIdDispositivo
                    BeNuevoMarcaje.Fec_lectura = Now

                    Dim BeMarcajeAnterior As New clsBeMarcaje

                    BeMarcajeAnterior = Get_Marcaje_By_Operador_And_FechaActual(BeParametrosMarcajeAnterior,
                                                                                lConnection,
                                                                                lTransaction)

                    If Not BeMarcajeAnterior Is Nothing Then

                        If BeMarcajeAnterior.Entro AndAlso Not BeMarcajeAnterior.Salio Then

                            BeMarcajeAnterior.Salio = BeMarcajeAnterior.Entro
                            BeMarcajeAnterior.Hora_salio = Now
                            rowsafected = Actualizar(BeMarcajeAnterior, lConnection, lTransaction)
                            lTransaction.Commit()
                            Exit Function

                        End If

                    Else
                        BeNuevoMarcaje.Primer_marcaje = 1
                    End If

                    BeNuevoMarcaje.Entro = True
                    BeNuevoMarcaje.Hora_entro = Now
                    BeNuevoMarcaje.Ingreso_anticipado = False
                    BeNuevoMarcaje.Marcaje_manual = False
                    BeNuevoMarcaje.Marcaje_contabilizado = False
                    BeNuevoMarcaje.Salida_tardia = False

                    If Not pEsSalida Then
                        rowsafected = Insertar(BeNuevoMarcaje, lConnection, lTransaction)
                    Else
                        '#EJC20200407: Se salió de la HH de forma abrupta y existe un registro de entrada sin salida.
                        BeMarcajeAnterior.Salio = BeMarcajeAnterior.Entro
                        BeMarcajeAnterior.Hora_salio = Now
                        rowsafected = Actualizar(BeMarcajeAnterior, lConnection, lTransaction)
                    End If
                Else

                    Dim vMsgError As String = String.Format("Error_08012025_Marcaje B: MISS_CONFIG_DAY No se encontró horarios asociados al Operador: " & pIdOperador & " Bodega: " & pIdBodega & "para el día:" & Now.DayOfWeek.ToString())
                    clsLnLog_error_wms.Agregar_Error(vMsgError)
                    Throw New Exception("#20200403_MISS_CONFIG_DAY: No se encontró horarios asociados al Operador: " & pIdOperador & " Bodega: " & pIdBodega &
                                   " para el día:" & Now.DayOfWeek.ToString())
                End If


            Else

                Dim vMsgError As String = String.Format("Error_08012025_Marcaje C: MISS_CONFIG_DAY No se encontró horarios asociados al Operador: " & pIdOperador & " Bodega: " & pIdBodega & ". Configure: Turno/Jornada/Horario en BOF y asocie con bodega.")
                clsLnLog_error_wms.Agregar_Error(vMsgError)

                Throw New Exception("#20200403_MISS_CONFIG: No se encontraron horarios asociados al operador: " & pIdOperador & " bodega: " & pIdBodega &
                                    " Configure Turno/Jornada/Horario en BOF y asocie con bodega.")
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("Error_08012025_Marcaje A: {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Function Tiene_Marcaje_Previo() As Boolean

        Try

            Return False

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
