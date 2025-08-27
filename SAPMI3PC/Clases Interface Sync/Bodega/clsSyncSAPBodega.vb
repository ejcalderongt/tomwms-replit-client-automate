Imports System.Data.SqlClient
Imports System.Reflection
Imports SAPbobsCOM

Public Class clsSyncSAPBodega : Inherits clsInterfaceBase
    Implements IDisposable

    Private fichaBodegas As List(Of clsBeI_nav_bodega)
    Dim VContadorBitacoraTOMWMS As Integer = 0
    Dim VContadorBitacoraIntermedia As Integer = 0

    Private oCompany As Company
    Dim lRetCode, lErrCode As Long
    Dim sErrMsg As String = ""

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing

    Private Function Importar_Bodegas_Desde_SAP_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                  ByRef prg As ProgressBar,
                                                                  ByRef cnnLog As SqlConnection) As Boolean

        Importar_Bodegas_Desde_SAP_A_TablaIntermedia = False

        Dim Cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTrans As SqlTransaction = Nothing

        Try

            If Conectar_A_SAP(oCompany, False, lErrCode, sErrMsg) Then
                clsPublic.Actualizar_Progreso(lblprg, "Conexión a SAP exitosa.")
            Else
                clsPublic.Actualizar_Progreso(lblprg, "No se pudo conectar a SAPBO: " & sErrMsg)
                Exit Function
            End If
            '
            fichaBodegas = Get_Bodegas_SAP()

            Application.DoEvents()

            clsPublic.Actualizar_Progreso(lblprg, "Consultando bodegas en SAP (OWHS).")
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Bodegas encontradas en WS: {0} ", fichaBodegas.Count))

            prg.Maximum = fichaBodegas.Count

            Dim vContador As Integer = 0

            Cnn.Open() : lTrans = Cnn.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnI_nav_bodega.EliminarTodos(Cnn, lTrans)

            BeNavEjecucionRes.Registros_ws = fichaBodegas.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Dim vCodigoBodega As String = ""

            For Each bodega In fichaBodegas

                vCodigoBodega = bodega.Bodega_code

                Try

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Bodega: {0} ", bodega.Bodega_code, vbNewLine))
                    clsLnI_nav_bodega.Insertar(bodega, Cnn, lTrans)

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               vCodigoBodega,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet,
                                                               cnnLog)

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: {0} ", ex.Message, vbNewLine))

                End Try

            Next

            lTrans.Commit()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso: " & Now)

            Importar_Bodegas_Desde_SAP_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTrans Is Nothing Then lTrans.Rollback()

            '#EJC20171107_REF03_0237AM: Insertar en log, excepción general
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet,
                                                       cnnLog)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error: {0} ", ex.Message, vbNewLine))

            Throw ex

        Finally
            If Cnn.State = ConnectionState.Open Then Cnn.Close()
            prg.Value = 0
            Desconectar_SAP(oCompany)
        End Try

    End Function

    Private Function Get_Bodegas_SAP() As List(Of clsBeI_nav_bodega)

        Get_Bodegas_SAP = Nothing

        Dim lBodegasWMS As New List(Of clsBeI_nav_bodega)
        Dim BeBodega As New clsBeI_nav_bodega

        Try

            Dim query_sap As String = "SELECT WhsCode,WhsName, TransferAc
                                       FROM OWHS WHERE ISNULL(U_Enviado_WMS,2)=2 AND INACTIVE = 'N' "

            If oCompany.Connected Then

                Dim rs As Recordset = CType(oCompany.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
                rs.DoQuery(query_sap)

                While rs.EoF = False

                    BeBodega = New clsBeI_nav_bodega()
                    BeBodega.Bodega_code = IIf(IsDBNull(rs.Fields.Item("WhsCode").Value.ToString()), "0", rs.Fields.Item("WhsCode").Value.ToString())
                    BeBodega.Bodega_name = IIf(IsDBNull(rs.Fields.Item("WhsName").Value.ToString()), "", rs.Fields.Item("WhsName").Value.ToString())
                    lBodegasWMS.Add(BeBodega)
                    rs.MoveNext()

                End While

            End If

            Return lBodegasWMS

        Catch ex As Exception
            Throw ex
        Finally
            If oCompany.Connect Then
                oCompany.Disconnect()
            End If
        End Try

    End Function

    Public Function Insertar_Bodegas_Desde_Tabla_Intermedia_A_Tabla_TOMWMS(ByVal lblprg As RichTextBox,
                                                                           ByRef prg As ProgressBar,
                                                                           Optional ByVal ForzarEjecucion As Boolean = False,
                                                                           Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Bodegas_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTrans As SqlTransaction = Nothing

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Force_Ejecución: " & ForzarEjecucion)

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Bodega") Then
                    clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    Exit Function
                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            CnnInterface.Open() : lTrans = CnnInterface.BeginTransaction(IsolationLevel.ReadUncommitted)

            BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Bodegas_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde SAP?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Not Importar_Bodegas_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If

                End If

            End If

            Dim lBodegas As New List(Of clsBeI_nav_bodega)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando bodegas en tabla intermedia ")

            lBodegas = clsLnI_nav_bodega.GetAll(CnnInterface, lTrans)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Bodegas en tabla intermedia: {0}", lBodegas.Count))

            If lBodegas.Count > 0 Then

                Dim BeCliente As clsBeCliente = Nothing
                Dim BeClienteBodega As clsBeCliente_bodega = Nothing
                Dim BeClienteExistente As clsBeCliente = Nothing

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnInterface, lTrans)

                prg.Maximum = lBodegas.Count

                Dim vContador As Integer = 0

                prg.Value = 0

                clsPublic.Actualizar_Progreso(lblprg, "Trasladando bodegas como clientes en TOMWMS.")

                For Each navBodega As clsBeI_nav_bodega In lBodegas

                    BeCliente = New clsBeCliente
                    BeClienteExistente = New clsBeCliente
                    BeClienteExistente = clsLnCliente.Existe(navBodega.Bodega_code, CnnInterface, lTrans)

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Bodega: {0}", navBodega.Bodega_code))

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

                            VContadorBitacoraTOMWMS += 1

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                     BeCliente.Codigo,
                                                                     BeNavEjecucionEnc.IdEjecucionEnc,
                                                                     BeConfigDet.Idnavconfigdet,
                                                                     CnnLog)

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar bodega: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))

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

                            VContadorBitacoraTOMWMS += 1

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

                            clsPublic.Actualizar_Progreso(lblprg, "Fin de inserción para: " & BeCliente.Codigo)

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeCliente.Codigo,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet,
                                                                       CnnLog)

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar bodega: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))

                        End Try

                        Application.DoEvents()

                    End If

                Next

            End If

            lTrans.Commit()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso.")
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Bodegas procesadas correctamente: {0}", VContadorBitacoraTOMWMS))
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS

            If VContadorBitacoraIntermedia = VContadorBitacoraTOMWMS Then
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
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Bodega-Cliente a tabla de TOMWMS: {0}", ex.Message))
                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
            End Try

            Insertar_Bodegas_Desde_Tabla_Intermedia_A_Tabla_TOMWMS = True

        Catch ex As Exception
            If Not lTrans Is Nothing Then lTrans.Rollback()
            prg.Value = 0
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Bodega-Cliente a tabla de TOMWMS: {0}", ex.Message))
            '#EJC20171107_REF02_0237AM: Insertar en log, excepción general
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                            MethodBase.GetCurrentMethod.Name(),
                            BeNavEjecucionEnc.IdEjecucionEnc,
                            BeConfigDet.Idnavconfigdet,
                            CnnLog)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            prg.Value = 0
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
        End Try

    End Function

    Public Function Insertar_Bodegas_Desde_SAP(ByVal lblprg As RichTextBox,
                                              ByRef prg As ProgressBar,
                                              Optional ByVal ForzarEjecucion As Boolean = False,
                                              Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Bodegas_Desde_SAP = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTrans As SqlTransaction = Nothing

        Try

            clsPublic.Actualizar_Progreso(lblprg, "Force_Ejecución: " & ForzarEjecucion)

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Bodega") Then
                    clsPublic.Actualizar_Progreso(lblprg, "La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    Exit Function
                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            CnnInterface.Open() : lTrans = CnnInterface.BeginTransaction(IsolationLevel.ReadCommitted)

            BeNavEjecucionRes.IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            clsPublic.Actualizar_Progreso(lblprg, vbNewLine)

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Bodegas_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde SAP?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Not Importar_Bodegas_Desde_SAP_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If

                End If

            End If

            Dim lBodegas As New List(Of clsBeI_nav_bodega)

            clsPublic.Actualizar_Progreso(lblprg, "Consultando bodegas en tabla intermedia ")

            lBodegas = clsLnI_nav_bodega.GetAll(CnnInterface, lTrans)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Bodegas en tabla intermedia: {0}", lBodegas.Count))

            If lBodegas.Count > 0 Then

                Dim BeCliente As clsBeCliente = Nothing
                Dim BeClienteBodega As clsBeCliente_bodega = Nothing
                Dim BeClienteExistente As clsBeCliente = Nothing

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnInterface, lTrans)

                prg.Maximum = lBodegas.Count

                Dim vContador As Integer = 0

                prg.Value = 0

                clsPublic.Actualizar_Progreso(lblprg, "Trasladando bodegas de SAP a clientes de TOMWMS.")

                For Each navBodega As clsBeI_nav_bodega In lBodegas

                    BeCliente = New clsBeCliente
                    BeClienteExistente = New clsBeCliente
                    BeClienteExistente = clsLnCliente.Existe(navBodega.Bodega_code, CnnInterface, lTrans)

                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Bodega: {0}", navBodega.Bodega_code))

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

                            VContadorBitacoraTOMWMS += 1

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                     BeCliente.Codigo,
                                                                     BeNavEjecucionEnc.IdEjecucionEnc,
                                                                     BeConfigDet.Idnavconfigdet, CnnLog)

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar bodega: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))

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

                            VContadorBitacoraTOMWMS += 1

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

                            clsPublic.Actualizar_Progreso(lblprg, "Fin de inserción para: " & BeCliente.Codigo)

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeCliente.Codigo,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar bodega: {0}{1}{2}", BeCliente.Codigo, vbNewLine, ex.Message))

                        End Try

                        Application.DoEvents()

                    End If

                Next

            End If

            lTrans.Commit()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de proceso.")
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Clientes procesados correctamente: {0}", VContadorBitacoraTOMWMS))
            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS

            If VContadorBitacoraIntermedia = VContadorBitacoraTOMWMS Then
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
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Bodega-Cliente a tabla de TOMWMS: {0}", ex.Message))
                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
            End Try

        Catch ex As Exception

            If Not lTrans Is Nothing Then lTrans.Rollback()
            prg.Value = 0
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Bodega-Cliente a tabla de TOMWMS: {0}", ex.Message))
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

End Class