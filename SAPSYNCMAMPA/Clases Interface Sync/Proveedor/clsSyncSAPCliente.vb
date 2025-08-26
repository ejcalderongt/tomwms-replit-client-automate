Imports System.Data.SqlClient
Imports System.Reflection
Public Class clsSyncSAPCliente

    Private Shared BeNavEjecucionEnc As New clsBeI_nav_ejecucion_enc()
    Private Shared BeNavEjecucionRes As New clsBeI_nav_ejecucion_res()
    Private Shared BeConfigDet As New clsBeI_nav_config_det()
    Private Shared BeConfigEnc As New clsBeI_nav_config_enc()
    Public Shared ListaDetalleConfigDet As New List(Of clsBeI_nav_config_det)()
    Private Shared VContadorBitacoraTOMWMS As Integer = 0
    Private Shared VContadorBitacoraIntermedia As Integer = 0

    Public Shared Sub Iniciar_Ejecucion(lbl As RichTextBox, cnnLog As SqlConnection)
        Try
            BeNavEjecucionEnc = New clsBeI_nav_ejecucion_enc With {
                .IdEjecucionEnc = clsLnI_nav_ejecucion_enc.MaxID(cnnLog),
                .IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface,
                .Fecha = Now
            }

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, cnnLog)

            BeNavEjecucionRes = New clsBeI_nav_ejecucion_res With {
                .IdEjecucionRes = clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(cnnLog) + 1,
                .IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc,
                .IdNavConfigDet = BeConfigDet.Idnavconfigdet,
                .Registros_ws = 0,
                .Registros_ti = 0,
                .Registros_WMS = 0,
                .Exitosa = False
            }

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, cnnLog)
            clsPublic.Actualizar_Progreso(lbl, $"Inicio de ejecución {BeNavEjecucionEnc.IdEjecucionEnc}")
        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lbl, $"Error al iniciar ejecución: {ex.Message}")
            Throw
        End Try
    End Sub
    Public Shared Function Importar_Clientes_Desde_SAP_Hana_A_TablaIntermedia(lbl As RichTextBox,
                                                                              ByRef prg As ProgressBar,
                                                                              ByRef cnnLog As SqlConnection) As Boolean
        Dim cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim tran As SqlTransaction = Nothing
        Importar_Clientes_Desde_SAP_Hana_A_TablaIntermedia = False

        Try
            clsPublic.Actualizar_Progreso(lbl, "Consultando clientes nuevos en SAP...")
            Dim lista = clsSyncSAPProveedor.Get_Clientes_SAP_Hana()
            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)
            Application.DoEvents()

            prg.Maximum = lista.Count
            prg.Value = 0
            VContadorBitacoraIntermedia = 0

            cnn.Open() : tran = cnn.BeginTransaction(IsolationLevel.ReadUncommitted)
            clsLnI_nav_cliente.Eliminar_Todos(cnn, tran)
            If cnnLog.State = ConnectionState.Closed Then cnnLog.Open()

            For Each cli In lista
                Try
                    cli.IdCliente = clsLnI_nav_cliente.MaxID(cnn, tran) + 1
                    clsPublic.Actualizar_Progreso(lbl, $"Insertando cliente: {cli.No}")
                    clsLnI_nav_cliente.Insertar(cli, cnn, tran)
                    VContadorBitacoraIntermedia += 1
                    prg.Value += 1
                    Application.DoEvents()
                Catch ex As Exception
                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, cli.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
                    clsPublic.Actualizar_Progreso(lbl, $"Error al insertar {cli.No}: {ex.Message}")
                End Try
            Next

            tran.Commit()
            clsPublic.Actualizar_Progreso(lbl, "Importación de clientes a tabla intermedia finalizada.")
            Importar_Clientes_Desde_SAP_Hana_A_TablaIntermedia = True

        Catch ex As Exception
            If tran IsNot Nothing Then tran.Rollback()
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
            clsPublic.Actualizar_Progreso(lbl, $"Error general en importación de clientes: {ex.Message}")
            Throw
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
        End Try
    End Function
    Public Shared Sub ProcesarClientes(clientes As List(Of clsBeI_nav_cliente),
                                        cnn As SqlConnection,
                                        tran As SqlTransaction,
                                        cnnLog As SqlConnection,
                                        lbl As RichTextBox,
                                        prg As ProgressBar,
                                        ByRef actualizados As List(Of String))

        Cargar_Config_Desde_DB(cnn, tran)

        prg.Maximum = clientes.Count
        prg.Value = 0
        VContadorBitacoraTOMWMS = 0

        For Each navCli In clientes

            prg.Value += 1

            clsPublic.Actualizar_Progreso(lbl, $"Procesando Cliente: {navCli.No}")

            Dim existente = clsLnCliente.Existe(navCli.No, cnn, tran)

            If existente IsNot Nothing Then
                Try
                    Dim cliente As New clsBeCliente With {
                        .IdEmpresa = BeConfigEnc.Idempresa,
                        .IdPropietario = BeConfigEnc.IdPropietario,
                        .IdCliente = existente.IdCliente,
                        .Codigo = navCli.No,
                        .Nombre_comercial = navCli.Name,
                        .Telefono = navCli.Phone_No,
                        .Nit = navCli.VAT_Registratrion_No,
                        .Direccion = navCli.Adress,
                        .Nombre_contacto = navCli.ContactName,
                        .Activo = True
                    }

                    clsLnCliente.Actualizar(cliente, cnn, tran)
                    actualizados.Add(cliente.Codigo)
                    VContadorBitacoraTOMWMS += 1

                Catch ex As Exception
                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, navCli.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
                    clsPublic.Actualizar_Progreso(lbl, $"Error al actualizar cliente: {navCli.No} -> {ex.Message}")
                End Try
            Else
                Try
                    Dim nuevoId = clsLnCliente.MaxID(cnn, tran) + 1
                    Dim cliente As New clsBeCliente With {
                        .IdEmpresa = BeConfigEnc.Idempresa,
                        .IdPropietario = BeConfigEnc.IdPropietario,
                        .IdCliente = nuevoId,
                        .Codigo = navCli.No,
                        .Nombre_comercial = navCli.Name,
                        .Telefono = navCli.Phone_No,
                        .Nit = navCli.VAT_Registratrion_No,
                        .Direccion = navCli.Adress,
                        .Nombre_contacto = navCli.ContactName,
                        .Activo = True,
                        .User_agr = BeConfigEnc.IdUsuario,
                        .Fec_agr = Date.UtcNow,
                        .User_mod = BeConfigEnc.IdUsuario,
                        .Fec_mod = Date.UtcNow
                    }

                    clsLnCliente.Insertar(cliente, cnn, tran)

                    Dim BeClienteBodega As New clsBeCliente_bodega
                    BeClienteBodega = clsLnCliente_bodega.GetSingle(cliente.IdCliente, BeConfigEnc.Idbodega, cnn, tran)

                    If BeClienteBodega Is Nothing Then

                        BeClienteBodega = New clsBeCliente_bodega
                        BeClienteBodega.IdClienteBodega = clsLnCliente_bodega.MaxID(cnn, tran) + 1
                        BeClienteBodega.IdCliente = cliente.IdCliente
                        BeClienteBodega.IdBodega = BeConfigEnc.Idbodega
                        BeClienteBodega.Activo = True
                        BeClienteBodega.User_agr = BeConfigEnc.IdUsuario
                        BeClienteBodega.User_mod = BeConfigEnc.IdUsuario
                        BeClienteBodega.Fec_agr = Now
                        BeClienteBodega.Fec_mod = Now

                        clsLnCliente_bodega.Insertar(BeClienteBodega, cnn, tran)

                    End If

                    actualizados.Add(cliente.Codigo)
                    VContadorBitacoraTOMWMS += 1

                Catch ex As Exception
                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, navCli.No, BeNavEjecucionEnc.IdEjecucionEnc, BeConfigDet.Idnavconfigdet, cnnLog)
                    clsPublic.Actualizar_Progreso(lbl, $"Error al insertar cliente: {navCli.No} -> {ex.Message}")
                End Try
            End If

            Application.DoEvents()
        Next
    End Sub

    Public Shared Function Insertar_Clientes_Desde_TablaIntermedia_A_Tabla_TOMWMS(lbl As RichTextBox,
                                                                                   prg As ProgressBar,
                                                                                   Optional ForzarEjecucion As Boolean = False,
                                                                                   Optional Preguntar As Boolean = False) As Boolean
        Insertar_Clientes_Desde_TablaIntermedia_A_Tabla_TOMWMS = False

        Dim cnn As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim cnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim tran As SqlTransaction = Nothing
        Dim clientesActualizados As New List(Of String)

        Try
            clsPublic.Actualizar_Progreso(lbl, $"Force_Ejecución: {ForzarEjecucion}")
            If Not ForzarEjecucion AndAlso Not clsSyncSAPProveedor.Ejecutar_Interfaz("Cliente") Then
                clsPublic.Actualizar_Progreso(lbl, "La configuración de la interfaz indica que no debe ejecutarse ahora.")
                Exit Function
            End If

            cnnLog.Open()
            clsSyncSAPProveedor.Iniciar_Ejecucion(lbl, cnnLog)

            cnn.Open()
            tran = cnn.BeginTransaction()
            Cargar_Config_Desde_DB(cnn, tran)

            If Not clsSyncSAPProveedor.Continuar_Importacion(Preguntar,
                                                             "¿Deseas llenar tabla intermedia de clientes desde SAP?",
                                                             Function() Importar_Clientes_Desde_SAP_Hana_A_TablaIntermedia(lbl, prg, cnnLog),
                                                             lbl) Then
                Exit Function
            End If

            Dim lista = clsLnI_nav_cliente.Get_All(cnn, tran)
            If lista.Count = 0 Then
                clsPublic.Actualizar_Progreso(lbl, "No se encontraron clientes en tabla intermedia.")
                Exit Function
            End If

            ProcesarClientes(lista, cnn, tran, cnnLog, lbl, prg, clientesActualizados)

            tran.Commit()
            clsSyncSAPProveedor.Finalizar_Ejecucion(lbl, cnnLog, "Clientes procesados correctamente")
            Insertar_Clientes_Desde_TablaIntermedia_A_Tabla_TOMWMS = True

        Catch ex As Exception
            If tran IsNot Nothing Then tran.Rollback()
            clsPublic.Actualizar_Progreso(lbl, $"Error general: {ex.Message}")
            Throw New Exception($" (M) {MethodBase.GetCurrentMethod.Name} {ex.Message}")
        Finally
            If cnn.State = ConnectionState.Open Then cnn.Close()
            If cnnLog.State = ConnectionState.Open Then cnnLog.Close()
            prg.Value = 0 : prg.Visible = False
        End Try
    End Function
    Public Shared Sub Finalizar_Ejecucion(lbl As RichTextBox, cnnLog As SqlConnection, resumen As String)
        Try
            clsPublic.Actualizar_Progreso(lbl, "Fin de inserción en TOMWMS.")
            clsPublic.Actualizar_Progreso(lbl, resumen & $": {VContadorBitacoraTOMWMS}")
            clsPublic.Actualizar_Progreso(lbl, $"Tiempo transcurrido: {DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)} segundo(s)")

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS
            BeNavEjecucionRes.Exitosa = (VContadorBitacoraTOMWMS = VContadorBitacoraIntermedia)

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)
        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lbl, $"Error al finalizar ejecución: {ex.Message}")
        End Try
    End Sub

    Public Shared Function Continuar_Importacion(preguntar As Boolean,
                                                 mensajeConfirmacion As String,
                                                 importacionHandler As Func(Of Boolean),
                                                 lbl As RichTextBox) As Boolean
        Try
            If Not preguntar Then
                Return importacionHandler()
            End If

            Dim respuesta = MessageBox.Show(mensajeConfirmacion, "Confirmar importación", MessageBoxButtons.YesNo)
            If respuesta = DialogResult.Yes Then
                Return importacionHandler()
            Else
                clsPublic.Actualizar_Progreso(lbl, "Importación desde SAP cancelada por el usuario.")
                Return False
            End If
        Catch ex As Exception
            clsPublic.Actualizar_Progreso(lbl, $"Error en la decisión de importación: {ex.Message}")
            Throw
        End Try
    End Function

    Public Shared Sub Cargar_Config_Desde_DB(cnn As SqlConnection, tran As SqlTransaction)
        BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, cnn, tran)
        If BeConfigEnc Is Nothing Then
            Throw New Exception("No se pudo cargar la configuración de interface (BeConfigEnc).")
        End If
    End Sub

End Class