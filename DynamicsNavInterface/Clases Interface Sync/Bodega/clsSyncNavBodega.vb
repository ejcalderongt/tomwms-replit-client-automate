Imports System.Data.SqlClient
Imports System.Reflection
Imports TOMWMS.WSFichaBodegas

Public Class clsSyncNavBodega : Inherits clsInterfaceBase
    Implements IDisposable

    Private Shared fichaBodegas() As Ficha_Bodegas
    Shared VContadorBitacoraTomims As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0

    Private Shared wsBodegaService As New Ficha_Bodegas_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

    Public Sub Dispose() Implements IDisposable.Dispose
        If wsBodegaService IsNot Nothing Then
            wsBodegaService.Dispose()
            wsBodegaService = Nothing
        End If
    End Sub

    Private Shared Function Importar_Bodega_Single_DesdeWSNav_A_TablaIntermedia(ByVal pCodigoBodega As String,
                                                                                ByVal lblprg As RichTextBox,
                                                                                ByRef cnnLog As SqlConnection,
                                                                                ByVal lConnection As SqlConnection,
                                                                                ByVal lTransaction As SqlTransaction) As Boolean

        Importar_Bodega_Single_DesdeWSNav_A_TablaIntermedia = False

        Try

            Dim vFiltro1 As New Ficha_Bodegas_Filter() With {.Field = Ficha_Bodegas_Fields.Code, .Criteria = pCodigoBodega}
            Dim vFiltros As Ficha_Bodegas_Filter() = New Ficha_Bodegas_Filter() {vFiltro1}

            wsBodegaService.Url = My.Settings.DynamicsNavInterface_WSFichaBodegas_Ficha_Bodegas_Service

            fichaBodegas = wsBodegaService.ReadMultiple(vFiltros, "", 0)

            Application.DoEvents()

            Dim BeI_nav_Bodega As clsBeI_nav_bodega

            lblprg.AppendText(String.Format("Bodegas encontradas en WS: {0} ", fichaBodegas.Count))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim vContador As Integer = 0

            clsLnI_nav_bodega.EliminarTodos(lConnection, lTransaction)

            BeNavEjecucionRes.Registros_ws = fichaBodegas.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Dim beBodega As Ficha_Bodegas

            For Each bodega As Ficha_Bodegas In fichaBodegas

                beBodega = bodega

                Try

                    BeI_nav_Bodega = New clsBeI_nav_bodega
                    BeI_nav_Bodega.Bodega_code = bodega.Code
                    BeI_nav_Bodega.Bodega_name = bodega.Name

                    lblprg.AppendText(String.Format("Procesando Bodega: {0} ", BeI_nav_Bodega.Bodega_code, vbNewLine))
                    lblprg.AppendText(vbNewLine)
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    clsLnI_nav_bodega.Insertar(BeI_nav_Bodega, lConnection, lTransaction)

                    VContadorBitacoraIntermedia += 1

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    Throw ex

                End Try

            Next

            Importar_Bodega_Single_DesdeWSNav_A_TablaIntermedia = True

        Catch ex As Exception


            '#EJC20171107_REF03_0237AM: Insertar en log, excepción general
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            lblprg.AppendText(String.Format("Error: {0} ", ex.Message, vbNewLine))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        End Try

    End Function

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Private Function Importar_Bodegas_DesdeWSNav_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                   ByRef prg As ProgressBar,
                                                                   ByRef cnnLog As SqlConnection) As Boolean

        Importar_Bodegas_DesdeWSNav_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTrans As SqlTransaction = Nothing

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** PROCESANDO DOCUMENTO EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            wsBodegaService.Url = My.Settings.DynamicsNavInterface_WSFichaBodegas_Ficha_Bodegas_Service

            fichaBodegas = wsBodegaService.ReadMultiple(Nothing, "", 0)

            Application.DoEvents()

            Dim BeI_nav_Bodega As clsBeI_nav_bodega

            lblprg.AppendText(String.Format("Bodegas encontradas en WS: {0} ", fichaBodegas.Count))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            prg.Maximum = fichaBodegas.Count

            Dim vContador As Integer = 0

            lConnection.Open() : lTrans = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnI_nav_bodega.EliminarTodos(lConnection, lTrans)

            BeNavEjecucionRes.Registros_ws = fichaBodegas.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Dim beBodega As Ficha_Bodegas

            For Each bodega As Ficha_Bodegas In fichaBodegas

                beBodega = bodega

                Try

                    BeI_nav_Bodega = New clsBeI_nav_bodega
                    BeI_nav_Bodega.Bodega_code = bodega.Code
                    BeI_nav_Bodega.Bodega_name = bodega.Name

                    lblprg.AppendText(String.Format("Procesando Bodega: {0} ", BeI_nav_Bodega.Bodega_code, vbNewLine))
                    lblprg.AppendText(vbNewLine)
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    clsLnI_nav_bodega.Insertar(BeI_nav_Bodega, lConnection, lTrans)

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               beBodega.Code,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, cnnLog)

                    lblprg.AppendText(String.Format("Error: {0} ", ex.Message, vbNewLine))
                    lblprg.AppendText(vbNewLine)
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            Next

            lTrans.Commit()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Importar_Bodegas_DesdeWSNav_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTrans Is Nothing Then lTrans.Rollback()

            '#EJC20171107_REF03_0237AM: Insertar en log, excepción general
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            lblprg.AppendText(String.Format("Error: {0} ", ex.Message, vbNewLine))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            prg.Value = 0
        End Try

    End Function

    Public Function Insertar_Bodegas_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(ByVal lblprg As RichTextBox,
                                                                           ByRef prg As ProgressBar,
                                                                           Optional ByVal ForzarEjecucion As Boolean = False,
                                                                           Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Bodegas_Desde_Tabla_Intermedia_A_Tabla_TOMIMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTrans As SqlTransaction = Nothing

        Try


            lblprg.AppendText("Force_Ejecución: " & ForzarEjecucion)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Bodega") Then

                    lblprg.AppendText("La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Exit Function

                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = 0 '0'0' 0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            'lblprg.AppendText(String.Format("Conectando a BD: {0} Sever: {1}", BD.Instancia.NombreBD, BD.Instancia.Server))
            'lblprg.AppendText(vbNewLine)
            'lblprg.Refresh()

            CnnInterface.Open() : lTrans = CnnInterface.BeginTransaction(IsolationLevel.ReadCommitted)

            BeNavEjecucionRes.IdEjecucionRes = 0 '0' clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            'lblprg.AppendText("Iniciando transacción a BD: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lblprg.AppendText("Consultando WebService de bodega en: " & My.MySettings.Default.DynamicsNavInterface_WSFichaBodegas_Ficha_Bodegas_Service)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Bodegas_DesdeWSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde WS?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Not Importar_Bodegas_DesdeWSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If

                End If

            End If

            Dim lBodegas As New List(Of clsBeI_nav_bodega)

            lblprg.AppendText("Consultando bodegas en tabla intermedia ")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lBodegas = clsLnI_nav_bodega.GetAll(CnnInterface, lTrans)

            lblprg.AppendText(String.Format("Bodegas en tabla intermedia: {0}", lBodegas.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If lBodegas.Count > 0 Then

                Dim BeCliente As clsBeCliente = Nothing
                Dim BeClienteBodega As clsBeCliente_bodega = Nothing
                Dim BeClienteExistente As clsBeCliente = Nothing

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnInterface, lTrans)

                If BeConfigEnc Is Nothing Then
                    If BD.Instancia.IdConfiguracionInterface = 0 Then
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                    Else
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                    End If
                End If

                prg.Maximum = lBodegas.Count

                Dim vContador As Integer = 0

                prg.Value = 0


                lblprg.AppendText("********** TRASLADANDO DOCUMENTO A TOMWMS ********** ")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                For Each navBodega As clsBeI_nav_bodega In lBodegas

                    BeCliente = New clsBeCliente
                    BeClienteExistente = New clsBeCliente
                    BeClienteExistente = clsLnCliente.Existe(navBodega.Bodega_code, CnnInterface, lTrans)

                    lblprg.AppendText(String.Format("Procesando Bodega: {0}", navBodega.Bodega_code))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    vContador += 1

                    prg.Value = vContador

                    If Not BeClienteExistente Is Nothing Then

                        Try

                            BeCliente.IdCliente = BeClienteExistente.IdCliente
                            BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                            BeCliente.Codigo = navBodega.Bodega_code
                            BeCliente.Nombre_comercial = navBodega.Bodega_name
                            BeCliente.Sistema = True
                            BeCliente.Activo = True
                            BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                            BeCliente.Nit = navBodega.Bodega_code
                            BeCliente.IdTipoCliente = 1


                            '#EJC20180110: Mantener bandera que indica si la bodega, 
                            'es una bodega válida para recepción 
                            'a partir de un cliente/bodega existente 
                            BeCliente.Es_bodega_recepcion = BeClienteExistente.Es_bodega_recepcion

                            '#EJC20180110: Mantener bandera que indica si la bodega, 
                            'es una bodega válida para un pedido de traslado/transferencia. 
                            'a partir de un cliente/bodega existente 
                            BeCliente.Es_Bodega_Traslado = BeClienteExistente.Es_Bodega_Traslado

                            clsLnCliente.ActualizarFromInterface(BeCliente, CnnInterface, lTrans)

                            VContadorBitacoraTomims += 1

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                     BeCliente.Codigo,
                                                                     BeNavEjecucionEnc.IdEjecucionEnc,
                                                                     BeConfigDet.Idnavconfigdet, CnnLog)

                            lblprg.AppendText(String.Format("Error al insertar bodega: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))

                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End Try

                    Else


                        BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                        BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                        BeCliente.Codigo = navBodega.Bodega_code
                        BeCliente.Nombre_comercial = navBodega.Bodega_name
                        BeCliente.IdCliente = clsLnCliente.MaxID(CnnInterface, lTrans) + 1
                        BeCliente.Nit = navBodega.Bodega_code
                        BeCliente.IdTipoCliente = 1
                        BeCliente.Activo = True
                        BeCliente.User_agr = BeConfigEnc.IdUsuario
                        BeCliente.Fec_agr = Now
                        BeCliente.User_mod = BeConfigEnc.IdUsuario
                        BeCliente.Fec_mod = Now
                        BeCliente.Sistema = True

                        Try

                            '#EJC20171105_1259AM: Se llamaba actualizarfrominterface y no insertaba el cliente -> error de FK despues en cliente_bodega
                            clsLnCliente.Insertar(BeCliente, CnnInterface, lTrans)

                            VContadorBitacoraTomims += 1

                            BeClienteBodega = New clsBeCliente_bodega

                            BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(CnnInterface, lTrans) + 1
                            BeClienteBodega.IdCliente = BeCliente.IdCliente
                            BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                            BeClienteBodega.Activo = True
                            BeClienteBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                            BeClienteBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                            BeClienteBodega.Fec_agr = Now
                            BeClienteBodega.Fec_mod = Now

                            clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega, CnnInterface, lTrans)

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("Fin de inserción para: " & BeCliente.Codigo)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeCliente.Codigo,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                            lblprg.AppendText(String.Format("Error al insertar bodega: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))

                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End Try

                        Application.DoEvents()

                    End If

                Next

            End If

            lTrans.Commit()

            lblprg.AppendText("********** FIN DE INSERCIÓN EN TOMWMS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Clientes procesados correctamente: {0}", VContadorBitacoraTomims))
            lblprg.AppendText(vbNewLine)
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            lblprg.AppendText(String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTomims

            If VContadorBitacoraIntermedia = VContadorBitacoraTomims Then
                BeNavEjecucionRes.Exitosa = True
            Else
                BeNavEjecucionRes.Exitosa = False
            End If

            '#EJC20171107_REF01_0237AM: Agregué esto en el finally 
            'para garantizar que siempre se actualicen los contadores en la bitácora
            'incluso cuando hay errores antes de la finalización del procedimiento.
            'anteriormente estaba al fin del procedimiento, pero si había una excepción
            'los contadores quedaban sin ser actualizados.
            Try
                clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)
            Catch ex As Exception
                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                            "clsLnI_nav_ejecucion_res.Actualizar",
                                            BeNavEjecucionEnc.IdEjecucionEnc,
                                            BeConfigDet.Idnavconfigdet, CnnLog)
                lblprg.AppendText(String.Format("Error al insertar Bodega-Cliente a tabla DE TOMWMS: {0}", ex.Message))
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()
                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
            End Try

        Catch ex As Exception
            If Not lTrans Is Nothing Then lTrans.Rollback()
            prg.Value = 0
            lblprg.AppendText(String.Format("Error al insertar Bodega-Cliente a tabla DE TOMWMS: {0}", ex.Message))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            '#EJC20171107_REF02_0237AM: Insertar en log, excepción general
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                            MethodBase.GetCurrentMethod.Name(),
                            BeNavEjecucionEnc.IdEjecucionEnc,
                            BeConfigDet.Idnavconfigdet, CnnLog)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Public Shared Function Insertar_Bodega_Single_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(ByVal pCodigoBodega As String,
                                                                                        ByVal pEsBodegaRecepcion As Boolean,
                                                                                        ByVal lConnectionLog As SqlConnection,
                                                                                        ByVal lConnectionInterface As SqlConnection,
                                                                                        ByVal lTransactionInterface As SqlTransaction,
                                                                                        ByVal lblprg As RichTextBox) As Boolean

        Insertar_Bodega_Single_Desde_Tabla_Intermedia_A_Tabla_TOMIMS = False

        Try

            If Not Importar_Bodega_Single_DesdeWSNav_A_TablaIntermedia(pCodigoBodega,
                                                                       lblprg,
                                                                       lConnectionLog,
                                                                       lConnectionInterface,
                                                                       lTransactionInterface) Then
                Exit Function
            End If

            Dim lBodegas As New List(Of clsBeI_nav_bodega)

            lblprg.AppendText("Consultando bodegas en tabla intermedia ")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lBodegas = clsLnI_nav_bodega.GetAll(lConnectionInterface, lTransactionInterface)

            lblprg.AppendText(String.Format("Bodegas en tabla intermedia: {0}", lBodegas.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If lBodegas.Count > 0 Then

                Dim BeCliente As clsBeCliente = Nothing
                Dim BeClienteBodega As clsBeCliente_bodega = Nothing
                Dim BeClienteExistente As clsBeCliente = Nothing

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnectionInterface, lTransactionInterface)

                If BeConfigEnc Is Nothing Then
                    If BD.Instancia.IdConfiguracionInterface = 0 Then
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                    Else
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                    End If
                End If

                Dim vContador As Integer = 0

                For Each navBodega As clsBeI_nav_bodega In lBodegas

                    BeCliente = New clsBeCliente
                    BeClienteExistente = New clsBeCliente
                    BeClienteExistente = clsLnCliente.Existe(navBodega.Bodega_code, lConnectionInterface, lTransactionInterface)

                    lblprg.AppendText(String.Format("Procesando Bodega: {0}", navBodega.Bodega_code))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    vContador += 1

                    If Not BeClienteExistente Is Nothing Then

                        Try

                            BeCliente.IdCliente = BeClienteExistente.IdCliente
                            BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                            BeCliente.Codigo = navBodega.Bodega_code
                            BeCliente.Nombre_comercial = navBodega.Bodega_name
                            BeCliente.Sistema = True
                            BeCliente.Activo = True
                            BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                            BeCliente.Nit = navBodega.Bodega_code
                            BeCliente.IdTipoCliente = 1
                            BeCliente.Es_bodega_recepcion = IIf(Not BeClienteExistente.Es_bodega_recepcion, pEsBodegaRecepcion, BeClienteExistente.Es_bodega_recepcion)
                            BeCliente.Es_Bodega_Traslado = BeClienteExistente.Es_Bodega_Traslado
                            clsLnCliente.ActualizarFromInterface(BeCliente, lConnectionInterface, lTransactionInterface)

                            VContadorBitacoraTomims += 1

                        Catch ex As Exception
                            Throw ex
                        End Try

                    Else


                        BeCliente.IdEmpresa = BeConfigEnc.Idempresa
                        BeCliente.IdPropietario = BeConfigEnc.IdPropietario
                        BeCliente.Codigo = navBodega.Bodega_code
                        BeCliente.Nombre_comercial = navBodega.Bodega_name
                        BeCliente.IdCliente = clsLnCliente.MaxID(lConnectionInterface, lTransactionInterface) + 1
                        BeCliente.Nit = navBodega.Bodega_code
                        BeCliente.IdTipoCliente = 1
                        BeCliente.Activo = True
                        BeCliente.User_agr = BeConfigEnc.IdUsuario
                        BeCliente.Fec_agr = Now
                        BeCliente.User_mod = BeConfigEnc.IdUsuario
                        BeCliente.Fec_mod = Now
                        BeCliente.Sistema = True
                        BeCliente.Es_bodega_recepcion = True
                        BeCliente.Es_Bodega_Traslado = True

                        Try

                            clsLnCliente.Insertar(BeCliente, lConnectionInterface, lTransactionInterface)

                            VContadorBitacoraTomims += 1

                            BeClienteBodega = New clsBeCliente_bodega
                            BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(lConnectionInterface, lTransactionInterface) + 1
                            BeClienteBodega.IdCliente = BeCliente.IdCliente
                            BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                            BeClienteBodega.Activo = True
                            BeClienteBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                            BeClienteBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                            BeClienteBodega.Fec_agr = Now
                            BeClienteBodega.Fec_mod = Now

                            clsLnCliente_bodega.Insertar_From_Interface(BeClienteBodega, lConnectionInterface, lTransactionInterface)

                            lblprg.AppendText(vbNewLine)
                            lblprg.AppendText("Bodega insertada como cliente: " & BeCliente.Codigo & " " & Now)
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        Catch ex As Exception
                            Throw ex
                        End Try

                        Application.DoEvents()

                    End If

                Next

            End If

            Insertar_Bodega_Single_Desde_Tabla_Intermedia_A_Tabla_TOMIMS = True

        Catch ex As Exception
            lblprg.AppendText(String.Format("Error al insertar Bodega-Cliente a tabla DE TOMWMS: {0}", ex.Message))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            '#EJC20171107_REF02_0237AM: Insertar en log, excepción general
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                            MethodBase.GetCurrentMethod.Name(),
                            BeNavEjecucionEnc.IdEjecucionEnc,
                            BeConfigDet.Idnavconfigdet, lConnectionLog)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
