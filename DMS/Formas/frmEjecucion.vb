Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports TOMWMS


Public Class frmEjecucion

    Dim fechasPorTabla As New Dictionary(Of String, DateTime)
    Private ListaInstancias As New List(Of clsCadenaConexion)

    Private Sub frmEjecucion_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Dim BeLogSincronizacion As New clsBeLog_sincronizacion_nube()

        Try


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

                        VerificarSincronizacionInicial()

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
        cmdPropietarios.Enabled = pVal
        cmdFechaBaseSync.Enabled = Not pVal
    End Sub


    Private Sub cmdProductos_ItemClickAsync(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdProductos.ItemClick
        Try

            cmdProductos.Enabled = False
            clsLnProductoDMS.Exportacion_ProductosAsync(lblprg)
            cmdProductos.Enabled = True

        Catch ex As Exception
            clsHelper.LogMensaje(lblprg, "Error en exportación de productos.", clsHelper.TipoMensaje.Error_)
            MessageBox.Show("Error al llamar a la API: " & ex.Message)
        Finally
            cmdIngresos.Enabled = True
        End Try
    End Sub

    Private Sub cmdIngresos_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdIngresos.ItemClick
        Try

            cmdIngresos.Enabled = False
            clsLnTrans_oc_encDMS.Exportacion_IngresosAsync(lblprg)
            cmdIngresos.Enabled = True

        Catch ex As Exception
            clsHelper.LogMensaje(lblprg, "Error en exportación de ingresos.", clsHelper.TipoMensaje.Error_)
            MessageBox.Show("Error al llamar a la API: " & ex.Message)
        Finally
            cmdIngresos.Enabled = True
        End Try
    End Sub

    Private Sub cmdSalidas_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalidas.ItemClick
        Try

            cmdIngresos.Enabled = False
            clsLnTrans_pe_encDMS.Exportacion_PedidosAsync(lblprg)
            cmdIngresos.Enabled = True

        Catch ex As Exception

            clsHelper.LogMensaje(lblprg, "Error en exportación de pedidos.", clsHelper.TipoMensaje.Error_)
            MessageBox.Show("Error al llamar a la API: " & ex.Message)
        Finally
            cmdIngresos.Enabled = True
        End Try
    End Sub

    Private Sub cmdFechaBaseSync_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdFechaBaseSync.ItemClick
        VerificarSincronizacionInicial()
    End Sub

    Private Sub VerificarSincronizacionInicial()
        Dim clsTransaccion As New clsTransaccion()
        Try

            clsTransaccion.Begin_Transaction()

            Dim fechaInicio As DateTime?
            fechasPorTabla = New Dictionary(Of String, DateTime)
            Dim pTablasNoSincronizadas = ObtenerFechasDeSincronizacionParaTablas(clsTransaccion.lConnection, clsTransaccion.lTransaction)

            If pTablasNoSincronizadas > 0 Then
                fechaInicio = PedirFechaBaseConDevExpress("")
                If Not fechaInicio.HasValue Then

                    clsHelper.LogMensaje(lblprg, "No se ingresó una fecha base. Se cancelará el proceso de sincronización.", clsHelper.TipoMensaje.Error_)
                    HabilitarOpciones(False)
                Else


                    Dim BeLogSincronizacion As New clsBeLog_sincronizacion_nube()
                    Dim MaxId = clsLnLog_sincronizacion_nube.MaxID(clsTransaccion.lConnection, clsTransaccion.lTransaction)

                    For Each tabla In fechasPorTabla
                        BeLogSincronizacion = New clsBeLog_sincronizacion_nube()
                        BeLogSincronizacion.IdLog = MaxId
                        BeLogSincronizacion.Fecha_sincronizacion = fechaInicio.Value
                        BeLogSincronizacion.Registros_enviados = 0
                        BeLogSincronizacion.Estado = "Ok"
                        BeLogSincronizacion.Mensaje_error = "Sincronización inicial"
                        BeLogSincronizacion.Tiempo_de_envio = 0
                        BeLogSincronizacion.User_agr = AP.UsuarioAp.IdUsuario
                        BeLogSincronizacion.Fec_agr = Now
                        BeLogSincronizacion.Entidad = tabla.Key
                        clsLnLog_sincronizacion_nube.Insertar(BeLogSincronizacion, clsTransaccion.lConnection, clsTransaccion.lTransaction)
                        clsHelper.LogMensaje(lblprg, "Se agregó fecha base para sincronizar la tabla " & tabla.Key, clsHelper.TipoMensaje.Exito)

                        MaxId += 1
                    Next

                    clsTransaccion.Commit_Transaction()

                    HabilitarOpciones(True)

                End If
            Else
                HabilitarOpciones(True)
            End If

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            clsTransaccion.Close_Conection()
        End Try
    End Sub


    Private Function ObtenerFechasDeSincronizacionParaTablas(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Dim tablas As List(Of String) = clsHelper.GetTablasSincronizables()
        Dim contador As Integer = 0

        For Each tabla In tablas
            Dim log = clsLnLog_sincronizacion_nube.GetLastSync(tabla, pConnection, pTransaction)

            If log Is Nothing Then
                fechasPorTabla.Add(tabla, Now.Date)
                clsHelper.LogMensaje(lblprg, "No se encontró sincronización previa para la tabla " & tabla & ".", True)
                contador += 1
            End If
        Next

        Return contador

    End Function


    ''' <summary>
    ''' Muestra una ventana DevExpress para seleccionar fecha y hora base.
    ''' Devuelve Nothing si el usuario cancela o cierra la ventana.
    ''' </summary>
    Private Function PedirFechaBaseConDevExpress(tabla As String) As DateTime?
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
        args.Prompt = $"No se encontró sincronización previa para la tabla '{tabla}'." & vbCrLf &
                  "Seleccione la fecha y hora base para iniciar la sincronización:"
        args.DefaultButtonIndex = 0
        args.Editor = dateEdit
        'args.Width = 450

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


End Class