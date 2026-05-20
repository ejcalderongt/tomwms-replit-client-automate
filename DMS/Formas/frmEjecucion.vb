Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Timers
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraSplashScreen
Imports TOMWMS
Imports TOMWMS.clsHelper


Public Class frmEjecucion

    Dim listaPropietarios As New List(Of Integer)()
    Dim listaPropietariosBodega As New List(Of Integer)

    Dim PropietariosList As New List(Of clsBePropietarios)()

    Dim fechasPorTabla As New Dictionary(Of String, DateTime)
    Private ListaInstancias As New List(Of clsCadenaConexion)
    Private Horario_DMS As New clsBeI_nav_config_det()
    Private ModoQA As Boolean = False
    Private ProcesoEjecutandose As Boolean = False


    Private Sub RefrescarHorarios()
        Horario_DMS = New clsBeI_nav_config_det()
        Dim diaHoy As Integer = CInt(DateTime.Now.DayOfWeek)
        If diaHoy = 0 Then diaHoy = 7 ' ajusta domingo si tu tabla usa 7
        Horario_DMS.Dia = diaHoy
        Horario_DMS.IdNavEnt = 1 'actualmente hay solo una entidad en la tabla DMS.Exe
        Horario_DMS = clsLnI_nav_config_det.GetSingle_By_Dia_And_Entidad(Horario_DMS)
    End Sub

    ' Control de última carga
    Private UltimaLecturaHorarios As DateTime = DateTime.MinValue
    Private ejecutandoProcesoDMS As Boolean = False
    Private minutoUltimaEjecucionDMS As Integer = -1

    Private Async Sub TimerElapsed(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs)
        Try

            ' Refrescar horarios desde BD cada 60 segundos
            If (DateTime.Now - UltimaLecturaHorarios).TotalSeconds >= 60 Then
                RefrescarHorarios()
            End If

            Dim ahora As TimeSpan = DateTime.Now.TimeOfDay

            '#GT25072025: valida la ejecución de DMS
            If Horario_DMS Is Nothing Then Exit Sub

            Dim diaHoy As Integer = CInt(DateTime.Now.DayOfWeek)
            If diaHoy = 0 Then diaHoy = 7 ' Ajustar domingo si es necesario

            If diaHoy = Horario_DMS.Dia Then

                If ahora >= Horario_DMS.HoraInicio.TimeOfDay AndAlso ahora <= Horario_DMS.HoraFin.TimeOfDay Then

                    Dim minutosDesdeInicio As Integer = CInt((ahora - Horario_DMS.HoraInicio.TimeOfDay).TotalMinutes)


                    '#GT29072025: bandera para saber si ejecutara el proceso o no
                    Debug.WriteLine($"Frecuencia: {minutosDesdeInicio}")

                    Dim cumpleFrecuencia As Boolean = (minutosDesdeInicio Mod Horario_DMS.Frecuencia = 0)

                    If cumpleFrecuencia AndAlso Not ejecutandoProcesoDMS AndAlso minutosDesdeInicio <> minutoUltimaEjecucionDMS Then
                        ejecutandoProcesoDMS = True
                        minutoUltimaEjecucionDMS = minutosDesdeInicio

                        Try

                            If Not ProcesoEjecutandose Then

                                If SplashScreenManager.Default IsNot Nothing Then
                                    SplashScreenManager.Default.SetWaitFormDescription("Generando Exportación a portal web...")
                                End If

                                Await EjecutarSecuenciaAutomaticaAsync()
                            End If

                        Catch ex As Exception
                            clsLnLog_error_wms.Agregar_Error(ex.Message)
                        Finally
                            ejecutandoProcesoDMS = False
                        End Try

                    End If

                End If

            End If

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error(ex.Message)
        End Try
    End Sub

    Private Sub setAPP()

        Try

            lblServerAPP.Caption += " " & clsBD.Instancia.Server
            lblBDAPP.Caption += " " & clsBD.Instancia.NombreBD

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmEjecucion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Dim BeLogSincronizacion As New clsBeDMS_Log_sincronizacion_nube()

        Try

            Dim args() As String = Environment.GetCommandLineArgs()
            Dim esInvocacionAutomatica As Boolean = args.Any(Function(a) a.ToLower() = "/auto")
            Dim ModoQA = args.Any(Function(a) a.TrimStart("-"c) = "1")

            '#GT13032023: controla la hora a ejecutar
            Dim timer As Timers.Timer = New Timers.Timer(1000)
            AddHandler timer.Elapsed, New ElapsedEventHandler(AddressOf TimerElapsed)
            timer.Start()


            If Not AP.Existe_Ini() Then
                XtraMessageBox.Show("No existe el archivo ini. se cerrará la aplicación",
                            Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
                Close()

            Else

                Dim IndiceInstanciaDefecto As Integer = -1
                ListaInstancias = clsPublic.Leer_Archivo_Configuracion_Ini(IndiceInstanciaDefecto)

                If ListaInstancias Is Nothing Then
                    '#CKFK 20180509 07:38 Corregí mensaje, le puse el No
                    XtraMessageBox.Show("No se encontró ninguna cadena de conexión válida en el archivo ini. se cerrará la aplicación",
                                        Text,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information)

                    Close()

                Else
                    Set_Parametros_Servidor(IndiceInstanciaDefecto, ListaInstancias)

                    If Abrio_Conexion(IndiceInstanciaDefecto, ListaInstancias) Then

                        Actualiza_APP_Config()

                        Cargar_Propietarios_Ux()

                        VerificarSincronizacionInicial()

                        setAPP()

                    Else
                        Throw New Exception("No se pudo establecer una conexión hacia la instancia")
                    End If
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        End Try

    End Sub


    Private Async Function EjecutarSecuenciaAutomaticaAsync() As Task
        Await EjecutarProductosAsync()
        Await EjecutarIngresosAsync()
        Await EjecutarSalidasAsync()
    End Function


    Private Async Sub cmdProductos_ItemClickAsync(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdProductos.ItemClick
        Try

            Await EjecutarProductosAsync()

        Catch ex As Exception
            clsHelper.LogMensaje(lblprg, "Error en exportación de productos.", clsHelper.TipoMensaje.Error_)
            MessageBox.Show("Error al llamar a la API: " & ex.Message)
        Finally
            'HabilitarOpciones(True)
        End Try
    End Sub

    Private Async Sub cmdIngresos_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdIngresos.ItemClick
        Try

            Await EjecutarIngresosAsync()

        Catch ex As Exception
            clsHelper.LogMensaje(lblprg, "Error en exportación de ingresos.", clsHelper.TipoMensaje.Error_)
            MessageBox.Show("Error al llamar a la API: " & ex.Message)
        Finally
            'cmdIngresos.Enabled = True
        End Try
    End Sub

    Private Async Sub cmdSalidas_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalidas.ItemClick
        Try

            Await EjecutarSalidasAsync()

        Catch ex As Exception

            clsHelper.LogMensaje(lblprg, "Error en exportación de pedidos.", clsHelper.TipoMensaje.Error_)
            MessageBox.Show("Error al llamar a la API: " & ex.Message)
        Finally
            'cmdIngresos.Enabled = True
        End Try
    End Sub

    Private Async Function EjecutarProductosAsync() As Task
        Try
            ProcesoEjecutandose = True
            HabilitarOpciones(False)
            Await clsLnProductoDMS.Exportacion_ProductosAsync(lblprg, listaPropietarios)
            HabilitarOpciones(True)
            ProcesoEjecutandose = False

        Catch ex As Exception
            clsHelper.LogMensaje(lblprg, "Error en exportación de productos.", clsHelper.TipoMensaje.Error_)
            MessageBox.Show("Error al llamar a la API: " & ex.Message)
        Finally
            HabilitarOpciones(True)
        End Try
    End Function


    Private Async Function EjecutarIngresosAsync() As Task
        Try
            ProcesoEjecutandose = True
            HabilitarOpciones(False)
            Await clsLnTrans_oc_encDMS.Exportacion_IngresosAsync(lblprg, listaPropietarios, listaPropietariosBodega)
            HabilitarOpciones(True)
            ProcesoEjecutandose = False
        Catch ex As Exception
            clsHelper.LogMensaje(lblprg, "Error en exportación de ingresos.", clsHelper.TipoMensaje.Error_)
            MessageBox.Show("Error al llamar a la API: " & ex.Message)
        Finally
            HabilitarOpciones(True)
        End Try
    End Function


    Private Async Function EjecutarSalidasAsync() As Task
        Try
            ProcesoEjecutandose = True
            HabilitarOpciones(False)
            Await clsLnTrans_pe_encDMS.Exportacion_PedidosAsync(lblprg, listaPropietarios, listaPropietariosBodega)
            HabilitarOpciones(True)
            ProcesoEjecutandose = False
        Catch ex As Exception
            clsHelper.LogMensaje(lblprg, "Error en exportación de pedidos.", clsHelper.TipoMensaje.Error_)
            MessageBox.Show("Error al llamar a la API: " & ex.Message)
        Finally
            HabilitarOpciones(True)
        End Try
    End Function

    Private Sub Set_Parametros_Servidor(ByVal IndiceInstanciaDefecto As Integer, ByVal ListaInstancias As List(Of clsCadenaConexion))

        Try

            If IndiceInstanciaDefecto <> -1 Then
                'Tomar por defecto la instancia que corresponde con el host que ejecuta.
                clsBD.Instancia = ListaInstancias(IndiceInstanciaDefecto)
            Else
                'Tomar por defecto la primera instancia.
                clsBD.Instancia = ListaInstancias(0)
            End If

            'mnuBD.Caption = String.Format("{0}/{1}", clsBD.Instancia.Server, clsBD.Instancia.NombreBD)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
             Text,
             MessageBoxButtons.OK,
             MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Function Abrio_Conexion(ByVal IndiceInstanciaDefecto As Integer,
                                   ByVal ListaInstancias As List(Of clsCadenaConexion)) As Boolean

        Abrio_Conexion = False

        '#EJC20171031_1259AM_REF: Cambiar timeOut de archivo de configuración para verificación de conexión a BD de forma temporal.
        Dim vTimeOutConfig As Double
        Dim CadenaConexion As String = ""

        If IndiceInstanciaDefecto = -1 Then
            vTimeOutConfig = ListaInstancias(0).TimeOutConBD
            ListaInstancias(0).TimeOutConBD = 10
            CadenaConexion = ListaInstancias(0).CadenaConexionSQLClient
        Else
            vTimeOutConfig = ListaInstancias(IndiceInstanciaDefecto).TimeOutConBD
            ListaInstancias(IndiceInstanciaDefecto).TimeOutConBD = 10
            CadenaConexion = ListaInstancias(IndiceInstanciaDefecto).CadenaConexionSQLClient
        End If

        Dim lConnection As New SqlClient.SqlConnection(CadenaConexion)

        Try

            lConnection.Open()
            lConnection.Close()

            Abrio_Conexion = True

        Catch ex1 As SqlClient.SqlException
            Throw ex1
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", "Error al intenar conectarse a la base de datos: ", ex.Message))
        Finally
            lConnection.Dispose()
            ListaInstancias(0).TimeOutConBD = vTimeOutConfig
        End Try

    End Function

    Private Sub Actualiza_APP_Config()

        Try

            Configuration.ConfigurationManager.AppSettings("CST") = clsBD.Instancia.CadenaConexionSQLClient

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub HabilitarOpciones(ByRef pVal As Boolean)
        cmdProductos.Enabled = pVal
        cmdIngresos.Enabled = pVal
        cmdSalidas.Enabled = pVal
    End Sub

    Private Sub cmdFechaBaseSync_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdFechaBaseSync.ItemClick
        cmdFechaBaseSync.Enabled = False
        VerificarSincronizacionInicial()
        cmdFechaBaseSync.Enabled = True
    End Sub

    Private Sub VerificarSincronizacionInicial()
        Dim clsTransaccion As New clsTransaccion()
        Dim listaPropietariosSinFechaSincro As New List(Of Integer)
        Try

            clsTransaccion.Begin_Transaction()

            Dim listaDuplasSinFecha As New List(Of Tuple(Of String, Integer))
            'Dim fechaInicio As DateTime?
            Dim tablas As List(Of String) = clsHelper.GetTablasSincronizables()

            '#GT16072025: PRIMERA ETAPA: Buscar nuevos propietarios sin fecha inicial de sincronización
            If listaPropietarios Is Nothing OrElse listaPropietarios.Count = 0 Then
                clsHelper.LogMensaje(lblprg, "No hay propietarios definidos para sincronización.", clsHelper.TipoMensaje.Advertencia)
                HabilitarOpciones(False)
                cmdFechaBaseSync.Enabled = True
                'clsTransaccion.RollBack_Transaction()
                Return
            End If

            For Each tabla In tablas
                For Each pIdPropietario In listaPropietarios
                    If Not clsLnDMS_Log_sincronizacion_nube.Existe_By_Tabla_and_IdPropietario(tabla, pIdPropietario, clsTransaccion.lConnection, clsTransaccion.lTransaction) Then
                        Dim dupla = Tuple.Create(tabla, pIdPropietario)
                        ' Validar si ya existe en la lista
                        If Not listaDuplasSinFecha.Any(Function(x) x.Item1 = tabla AndAlso x.Item2 = pIdPropietario) Then
                            listaDuplasSinFecha.Add(dupla)
                            clsHelper.LogMensaje(lblprg, $"Fecha de sincronización pendiente para propietario {pIdPropietario} y tabla {tabla}", clsHelper.TipoMensaje.Error_)
                        End If

                    End If
                Next
            Next

            '#GT16072025: SEGUNDA ETAPA: ¿hay duplas sin fecha?
            If listaDuplasSinFecha.Count > 0 Then

                '#GT07082025: pedir fechas en una lista y no en un ciclo.
                ' Una nueva clase auxiliar para gestionar cada fila
                Dim listaDuplas As New List(Of DuplaSinFecha)

                For Each dupla In listaDuplasSinFecha
                    listaDuplas.Add(New DuplaSinFecha With {
                    .Tabla = dupla.Item1,
                    .IdPropietario = dupla.Item2,
                    .Nombre = PropietariosList.Find(Function(x) x.IdPropietario = dupla.Item2).Nombre_comercial,
                    .FechaSincronizacion = Nothing
                    })
                Next

                clsTransaccion.Commit_Transaction()

                If listaDuplas.Count > 0 Then
                    Dim frm As New frmRegistraFechaExpotacion(listaDuplas)

                    If frm.ShowDialog() <> DialogResult.OK Then
                        clsHelper.LogMensaje(lblprg, "No se ingresaron fechas individuales. Se cancelará el proceso de sincronización.", clsHelper.TipoMensaje.Error_)
                        HabilitarOpciones(False)
                        Return
                    End If

                    Dim listaResultado As List(Of DuplaSinFecha) = frm.Resultado
                    ' Aquí puedes usar listaResultado con las fechas ingresadas
                End If

            Else
                ' No hay propietarios sin fecha inicial
                HabilitarOpciones(True)
                cmdFechaBaseSync.Enabled = False
            End If

            'clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            'clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            'clsTransaccion.Close_Conection()
        End Try
    End Sub

    ''' <summary>
    ''' Muestra una ventana DevExpress para seleccionar fecha y hora base.
    ''' Devuelve Nothing si el usuario cancela o cierra la ventana.
    ''' </summary>
    Private Function PedirFechaBaseConDevExpress(tabla As String) As DateTime?

        Dim dateEdit As New DateEdit()
        dateEdit.EditValue = Date.Now
        dateEdit.Dock = DockStyle.Fill

        With dateEdit.Properties
            .CalendarView = CalendarView.Classic
            .VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True
            .Mask.EditMask = "yyyy-MM-dd HH:mm"
            .Mask.UseMaskAsDisplayFormat = True
            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .DisplayFormat.FormatString = "yyyy-MM-dd HH:mm"
            .EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .EditFormat.FormatString = "yyyy-MM-dd HH:mm"
            .VistaTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .VistaTimeProperties.DisplayFormat.FormatString = "HH:mm"
            .VistaTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .VistaTimeProperties.EditFormat.FormatString = "HH:mm"
        End With


        Dim args As New XtraInputBoxArgs()
        args.Caption = "Fecha y hora de sincronización"
        args.Prompt = $"Se han encontrado '{tabla}'." & vbCrLf &
                  "Seleccione la fecha y hora base para iniciar la sincronización:"
        args.DefaultButtonIndex = 0
        args.Editor = dateEdit

        Dim result = XtraInputBox.Show(args)

        If result Is Nothing OrElse String.IsNullOrWhiteSpace(result.ToString()) Then
            Return Nothing
        End If

        Dim fecha As DateTime
        If DateTime.TryParse(result.ToString(), fecha) Then
            Return fecha
        Else
            Return Nothing
        End If
    End Function

    Private Function PedirFechaBaseConDevExpress(tabla As String, propietarioId As Integer) As DateTime?
        ' Crear editor de fecha con hora
        Dim dateEdit As New DateEdit()
        dateEdit.EditValue = Date.Now
        dateEdit.Dock = DockStyle.Fill

        With dateEdit.Properties
            .CalendarView = CalendarView.Classic
            .VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True

            ' Mostrar y editar con fecha y hora
            .Mask.EditMask = "yyyy-MM-dd HH:mm"
            .Mask.UseMaskAsDisplayFormat = True

            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .DisplayFormat.FormatString = "yyyy-MM-dd HH:mm"

            .EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .EditFormat.FormatString = "yyyy-MM-dd HH:mm"

            ' Habilitar selector de hora en el calendario
            .VistaTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .VistaTimeProperties.DisplayFormat.FormatString = "HH:mm"
            .VistaTimeProperties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .VistaTimeProperties.EditFormat.FormatString = "HH:mm"
        End With

        ' Preparar ventana modal
        Dim args As New XtraInputBoxArgs()
        args.Caption = "Fecha y hora de sincronización"
        args.Prompt = $"No se encontró sincronización previa para la tabla '{tabla}' y el propietario ID {propietarioId}." & vbCrLf &
                  "Seleccione la fecha y hora base para iniciar la sincronización:"
        args.DefaultButtonIndex = 0
        args.Editor = dateEdit

        ' Mostrar cuadro
        Dim result = XtraInputBox.Show(args)

        If result Is Nothing OrElse String.IsNullOrWhiteSpace(result.ToString()) Then
            Return Nothing
        End If

        ' Convertir resultado
        Dim fecha As DateTime
        If DateTime.TryParse(result.ToString(), fecha) Then
            Return fecha
        Else
            Return Nothing
        End If
    End Function


    Private Sub cmdLogErrores_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdLogErrores.ItemClick
        Try

            Dim frmErrores As New frmLogEventos
            frmErrores.ShowDialog()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cargar_Propietarios_Ux()
        Try
            If clsLnPropietarioDMS.Get_All_UX().Count < 1 Then
                Throw New Exception("No se encontraron propietarios para el proceso de exportación.")
            Else

                PropietariosList = clsLnPropietarios.Get_Propietarios_By_UX()


                If PropietariosList IsNot Nothing AndAlso PropietariosList.Count > 0 Then
                    For Each p In PropietariosList
                        listaPropietarios.Add(p.IdPropietario)
                    Next
                End If

                'listaPropietarios = clsLnPropietarioDMS.Get_All_UX()

                If listaPropietarios IsNot Nothing AndAlso listaPropietarios.Count > 0 Then
                    listaPropietariosBodega = clsLnPropietarioDMS.Get_Propietarios_Bodega_By_IdPropietario(listaPropietarios)
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
        Text,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdParametrizacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdParametrizacion.ItemClick
        Try

            Dim frm As New frmConfiguracionHorarios()
            frm.Show(Me) ' Pasas el formulario actual como dueño
            frm.BringToFront()


        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
       Text,
       MessageBoxButtons.OK,
       MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdReiniciar_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdReiniciar.ItemClick
        Dim clsTransaccion As New clsTransaccion()

        Try
            clsTransaccion.Begin_Transaction()

            clsLnDMS_Log_sincronizacion_fallos.Eliminar_Todo(clsTransaccion.lConnection, clsTransaccion.lTransaction)
            clsLnDMS_Log_sincronizacion_nube.Eliminar(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
        End Try
    End Sub

    'Private Sub cmdLogErrores_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdLogErrores.ItemClick
    '    Dim clsTransaccion As New clsTransaccion()

    '    Try
    '        clsTransaccion.Begin_Transaction()

    '        clsLnDMS_Log_sincronizacion_fallos.Eliminar_Todo(clsTransaccion.lConnection, clsTransaccion.lTransaction)
    '        clsLnDMS_Log_sincronizacion_nube.Eliminar(clsTransaccion.lConnection, clsTransaccion.lTransaction)

    '        clsTransaccion.Commit_Transaction()

    '    Catch ex As Exception
    '        clsTransaccion.RollBack_Transaction()
    '    End Try
    'End Sub
End Class