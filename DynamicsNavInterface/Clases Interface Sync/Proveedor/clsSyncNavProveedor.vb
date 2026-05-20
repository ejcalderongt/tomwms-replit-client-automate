Imports System.Data.SqlClient
Imports System.Reflection
Imports TOMWMS.WSListaCliente
Imports TOMWMS.WSProveedores

Public Class clsSyncNavProveedor : Inherits clsInterfaceBase
    Implements IDisposable

    Private fichaProveedores() As Proveedores
    Shared VContadorBitacoraTomims As Integer = 0
    Shared VContadorBitacoraIntermedia As Integer = 0

    Private Shared wsProveedorService As New Proveedores_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

    Public Sub Dispose() Implements IDisposable.Dispose
        If wsProveedorService IsNot Nothing Then
            wsProveedorService.Dispose()
            wsProveedorService = Nothing
        End If
    End Sub

    Dim BeNavEjecRes As clsBeI_nav_ejecucion_res = Nothing
    Private Function Importar_Proveedores_DesdeWSNav_A_TablaIntermedia(ByVal lblprg As RichTextBox,
                                                                       ByRef prg As ProgressBar,
                                                                       ByRef cnnLog As SqlConnection) As Boolean
        Importar_Proveedores_DesdeWSNav_A_TablaIntermedia = False

        Dim lConnection As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransaction As SqlTransaction = Nothing
        Dim vContador As Integer = 0
        Dim TieneFiltros As Boolean = False

        Try

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** PROCESANDO DOCUMENTO EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Consultando proveedores desde webservice.")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            Dim BeNavEnt As New clsBeI_nav_ent
            BeNavEnt = clsLnI_nav_ent.Get_Single_By_Nombre("proveedor")
            Dim vCriteria As String = ""

            If Not BeNavEnt Is Nothing Then


                If Not BeNavEnt.lDetalleFiltros Is Nothing Then

                    If BeNavEnt.lDetalleFiltros.Count > 0 Then

                        TieneFiltros = True

                        Dim lFiltros As New List(Of clsBeI_nav_ent_filtros)
                        lFiltros = BeNavEnt.lDetalleFiltros

                        For Each FiltroCategoria In lFiltros

                            If vContador = 0 Then
                                vCriteria = FiltroCategoria.Valor
                            Else
                                vCriteria += "|" & FiltroCategoria.Valor
                            End If

                            vContador += 1

                        Next

                    End If

                End If

            End If

            wsProveedorService.Url = My.Settings.DynamicsNavInterface_WSProveedores_Proveedores_Service

            If TieneFiltros Then
                Dim vFiltro1 As New Proveedores_Filter() With {.Field = Proveedores_Fields.Location_Code, .Criteria = vCriteria}
                Dim vFiltros As Proveedores_Filter() = New Proveedores_Filter() {vFiltro1}
                fichaProveedores = wsProveedorService.ReadMultiple(vFiltros, Nothing, 1000)
            Else
                fichaProveedores = wsProveedorService.ReadMultiple(Nothing, "", 0)
            End If

            BeNavEjecucionRes.Registros_ws = fichaProveedores.Count()

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, cnnLog)

            Application.DoEvents()

            Dim BeI_nav_Proveedor As clsBeI_nav_proveedor

            lblprg.AppendText(String.Format("prvoeedores encontrados en WS: {0} ", fichaProveedores.Count))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            lblprg.Refresh()

            prg.Maximum = fichaProveedores.Count

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            clsLnI_nav_proveedor.EliminarTodos(lConnection, lTransaction)

            If cnnLog.State = ConnectionState.Closed Then cnnLog.Open()

            Dim beProveedor As Proveedores

            vContador = 0

            For Each Prov As Proveedores In fichaProveedores

                beProveedor = Prov

                Try

                    BeI_nav_Proveedor = New clsBeI_nav_proveedor
                    BeI_nav_Proveedor.No = Prov.No
                    BeI_nav_Proveedor.Name = Prov.Name
                    BeI_nav_Proveedor.Adress = Prov.Address
                    BeI_nav_Proveedor.City = Prov.City
                    BeI_nav_Proveedor.Country = Prov.County
                    BeI_nav_Proveedor.Phone_No = Prov.Phone_No
                    BeI_nav_Proveedor.Contact = Prov.Contact
                    BeI_nav_Proveedor.Search_Name = Prov.Search_Name
                    BeI_nav_Proveedor.VAT_Registratrion_No = Prov.VAT_Registration_No
                    BeI_nav_Proveedor.Location_Code = Prov.Location_Code

                    lblprg.AppendText(String.Format("Procesando Proveedor: {0} ", BeI_nav_Proveedor.No, vbNewLine))
                    lblprg.AppendText(vbNewLine)
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    clsLnI_nav_proveedor.Insertar(BeI_nav_Proveedor, lConnection, lTransaction)

                    VContadorBitacoraIntermedia += 1

                    prg.Value = vContador

                    vContador += 1

                    Application.DoEvents()

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                          beProveedor.No,
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          BeConfigDet.Idnavconfigdet, cnnLog)

                    lblprg.AppendText("Error al insertar desde ws a intermedia: " & beProveedor.No & vbNewLine &
                                                   ex.Message)

                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            Next

            lTransaction.Commit()

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TABLA INTERMEDIA ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Importar_Proveedores_DesdeWSNav_A_TablaIntermedia = True

        Catch ex As Exception

            If Not lTransaction Is Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet, cnnLog)

            lblprg.AppendText(String.Format("Error al insertar proveedores desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function
    'fichaClientes = wsClienteService.ReadMultiple(vFiltros, Nothing, 0)

    Public Shared Function Insertar_Cliente_As_Proveedor_Single(ByVal NoProveedor As String,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction,
                                                                ByRef lConnectionLog As SqlConnection,
                                                                ByRef lblprg As RichTextBox,
                                                                ByRef prg As ProgressBar) As clsBeProveedor_bodega

        Insertar_Cliente_As_Proveedor_Single = Nothing

        Try

            Dim navCliente As New Lista_clientes

            Dim wsClienteService As New Lista_clientes_Service() With
            {
            .UseDefaultCredentials = UsarCredencialesPorDefecto,
            .Credentials = CredencialesConexion
            }

            wsClienteService.Url = My.MySettings.Default.NavSync_WSListaClientes_Lista_clientes_Service
            navCliente = wsClienteService.Read(NoProveedor)

            Dim BeProveedor As New clsBeProveedor
            Dim BeProveedorBodega As New clsBeProveedor_bodega

            If Not navCliente Is Nothing Then

                lblprg.AppendText(String.Format("Procesando Proveedor: {0} ", navCliente.No, vbNewLine))
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1
                BeProveedor.Codigo = navCliente.No
                BeProveedor.Nombre = navCliente.Name
                BeProveedor.Telefono = IIf(navCliente.Phone_No Is Nothing, "", navCliente.Phone_No)
                BeProveedor.Nit = IIf(navCliente.VAT_Registration_No Is Nothing, "", navCliente.VAT_Registration_No)
                BeProveedor.Direccion = IIf(navCliente.Address Is Nothing, "", navCliente.Address)
                BeProveedor.Contacto = IIf(navCliente.Contact Is Nothing, "", navCliente.Contact)
                BeProveedor.Activo = True
                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                BeProveedor.Fec_agr = Date.UtcNow
                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                BeProveedor.Fec_mod = Date.UtcNow

                Try

                    clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)

                    VContadorBitacoraTomims += 1

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, lConnection, lTransaction)

                    Return BeProveedorBodega

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeProveedor.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, lConnectionLog)

                    lblprg.AppendText(String.Format("Error al insertar proveedor: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, ex.Message))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            ElseIf NoProveedor = "PROD" Then

                lblprg.AppendText(String.Format("Procesando Proveedor: {0} ", NoProveedor, vbNewLine))
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1
                BeProveedor.Codigo = NoProveedor
                BeProveedor.Nombre = "PRODUCCIÓN (AUTO) MI3"
                BeProveedor.Telefono = "0"
                BeProveedor.Nit = "0"
                BeProveedor.Direccion = "N/A"
                BeProveedor.Contacto = "N/A"
                BeProveedor.Activo = True
                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                BeProveedor.Fec_agr = Date.UtcNow
                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                BeProveedor.Fec_mod = Date.UtcNow

                Try

                    clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)

                    VContadorBitacoraTomims += 1

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, lConnection, lTransaction)

                    Return BeProveedorBodega

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeProveedor.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, lConnectionLog)

                    lblprg.AppendText(String.Format("Error al insertar proveedor: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, ex.Message))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            End If

        Catch ex As Exception
            lblprg.AppendText(String.Format("Error al insertar Proveedor a tabla DE TOMWMS: {0}", ex.Message))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Insertar_Proveedor_Single(ByVal NoProveedor As String,
                                              ByRef lConnection As SqlConnection,
                                              ByRef lTransaction As SqlTransaction,
                                              ByRef lConnectionLog As SqlConnection,
                                              ByRef lblprg As RichTextBox,
                                              ByRef prg As ProgressBar) As clsBeProveedor_bodega

        Insertar_Proveedor_Single = Nothing

        Try

            Dim navProveedor As New Proveedores

            wsProveedorService.Url = My.Settings.DynamicsNavInterface_WSProveedores_Proveedores_Service
            navProveedor = wsProveedorService.Read(NoProveedor)

            Dim BeProveedor As New clsBeProveedor
            Dim BeProveedorBodega As New clsBeProveedor_bodega

            If Not navProveedor Is Nothing Then

                lblprg.AppendText(String.Format("Procesando Proveedor: {0} ", navProveedor.No, vbNewLine))
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1
                BeProveedor.Codigo = navProveedor.No
                BeProveedor.Nombre = navProveedor.Name
                BeProveedor.Telefono = IIf(navProveedor.Phone_No Is Nothing, "", navProveedor.Phone_No)
                BeProveedor.Nit = IIf(navProveedor.VAT_Registration_No Is Nothing, "", navProveedor.VAT_Registration_No)
                BeProveedor.Direccion = IIf(navProveedor.Address Is Nothing, "", navProveedor.Address)
                BeProveedor.Contacto = IIf(navProveedor.Contact Is Nothing, "", navProveedor.Contact)
                BeProveedor.Activo = True
                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                BeProveedor.Fec_agr = Date.UtcNow
                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                BeProveedor.Fec_mod = Date.UtcNow

                Try

                    clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)

                    VContadorBitacoraTomims += 1

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, lConnection, lTransaction)

                    Return BeProveedorBodega

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeProveedor.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, lConnectionLog)

                    lblprg.AppendText(String.Format("Error al insertar proveedor: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, ex.Message))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            ElseIf NoProveedor = "PROD" Then

                lblprg.AppendText(String.Format("Procesando Proveedor: {0} ", NoProveedor, vbNewLine))
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1
                BeProveedor.Codigo = NoProveedor
                BeProveedor.Nombre = "PRODUCCIÓN (AUTO) MI3"
                BeProveedor.Telefono = "0"
                BeProveedor.Nit = "0"
                BeProveedor.Direccion = "N/A"
                BeProveedor.Contacto = "N/A"
                BeProveedor.Activo = True
                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                BeProveedor.Fec_agr = Date.UtcNow
                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                BeProveedor.Fec_mod = Date.UtcNow

                Try

                    clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)

                    VContadorBitacoraTomims += 1

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, lConnection, lTransaction)

                    Return BeProveedorBodega

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeProveedor.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, lConnectionLog)

                    lblprg.AppendText(String.Format("Error al insertar proveedor: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, ex.Message))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            End If

        Catch ex As Exception
            lblprg.AppendText(String.Format("Error al insertar Proveedor a tabla DE TOMWMS: {0}", ex.Message))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Insertar_Bodega_Origen_Como_Proveedor(ByVal NoProveedor As String,
                                                                 ByRef lConnection As SqlConnection,
                                                                 ByRef lTransaction As SqlTransaction,
                                                                 ByRef lConnectionLog As SqlConnection,
                                                                 ByRef lblprg As RichTextBox,
                                                                 ByRef prg As ProgressBar) As clsBeProveedor_bodega

        Insertar_Bodega_Origen_Como_Proveedor = Nothing

        Try

            Dim BodSrv As New WSFichaBodegas.Ficha_Bodegas_Service With {
                                                                        .UseDefaultCredentials = UsarCredencialesPorDefecto,
                                                                        .Credentials = CredencialesConexion}
            Dim NavBodegaOrigen As New WSFichaBodegas.Ficha_Bodegas

            BodSrv.Url = BD.Instancia.URLBodegas

            NavBodegaOrigen = BodSrv.Read(NoProveedor)

            Dim BeProveedor As New clsBeProveedor
            Dim BeProveedorBodega As New clsBeProveedor_bodega

            If Not NavBodegaOrigen Is Nothing Then

                lblprg.AppendText(String.Format("Procesando Proveedor: {0} ", NavBodegaOrigen.Code, vbNewLine))
                lblprg.AppendText(vbNewLine)
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                BeProveedor.IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1

                BeProveedor.Codigo = NavBodegaOrigen.Code
                BeProveedor.Nombre = NavBodegaOrigen.Name
                BeProveedor.Telefono = IIf(NavBodegaOrigen.Phone_No Is Nothing, "", NavBodegaOrigen.Phone_No)
                BeProveedor.Nit = IIf(NavBodegaOrigen.Code Is Nothing, "", NavBodegaOrigen.Code)
                BeProveedor.Direccion = IIf(NavBodegaOrigen.Address Is Nothing, "", NavBodegaOrigen.Address)
                BeProveedor.Contacto = IIf(NavBodegaOrigen.Contact Is Nothing, "", NavBodegaOrigen.Contact)
                BeProveedor.Activo = True
                BeProveedor.User_agr = BeConfigEnc.IdUsuario
                BeProveedor.Fec_agr = Date.UtcNow
                BeProveedor.User_mod = BeConfigEnc.IdUsuario
                BeProveedor.Fec_mod = Date.UtcNow

                Try

                    clsLnProveedor.Insertar(BeProveedor, lConnection, lTransaction)

                    VContadorBitacoraTomims += 1

                    BeProveedorBodega = New clsBeProveedor_bodega
                    BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1
                    BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                    BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                    BeProveedorBodega.Activo = True
                    BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                    BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                    BeProveedorBodega.Fec_agr = Now
                    BeProveedorBodega.Fec_mod = Now

                    clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, lConnection, lTransaction)

                    Return BeProveedorBodega

                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                               BeProveedor.Codigo,
                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                               BeConfigDet.Idnavconfigdet, lConnectionLog)

                    lblprg.AppendText(String.Format("Error al insertar proveedor: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, ex.Message))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                End Try

            End If

        Catch ex As Exception
            lblprg.AppendText(String.Format("Error al insertar Proveedor a tabla DE TOMWMS: {0}", ex.Message))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Insertar_Proveedores_Desde_Tabla_Intermedia_A_Tabla_TOMIMS(ByRef lblprg As RichTextBox,
                                                                               ByRef prg As ProgressBar,
                                                                               Optional ByVal ForzarEjecucion As Boolean = False,
                                                                               Optional ByVal Pregunta_Si_LLena_Intermedia As Boolean = False) As Boolean

        Insertar_Proveedores_Desde_Tabla_Intermedia_A_Tabla_TOMIMS = False

        Dim CnnInterface As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim CnnLog As New SqlConnection(BD.Instancia.CadenaConexionSQLClient)
        Dim lTransInterface As SqlTransaction = Nothing

        VContadorBitacoraTomims = 0

        Try

            lblprg.AppendText("Force_Ejecución: " & ForzarEjecucion)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not ForzarEjecucion Then

                If Not Ejecutar_Interfaz("Proveedor") Then

                    lblprg.AppendText("La configuración de la interface indica que no se debe ejecutar en este momento. ")
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    Exit Function

                End If

            End If

            CnnLog.Open()

            BeNavEjecucionEnc.IdEjecucionEnc = 0 'clsLnI_nav_ejecucion_enc.MaxID(CnnLog)
            BeNavEjecucionEnc.IdNavConfigEnc = BD.Instancia.IdConfiguracionInterface
            BeNavEjecucionEnc.Fecha = Now

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            'lblprg.AppendText(String.Format("Conectando a BD: {0} Sever: {1}", BD.Instancia.NombreBD, BD.Instancia.Server))
            'lblprg.AppendText(vbNewLine)
            'lblprg.Refresh()

            BeNavEjecucionRes.IdEjecucionRes = 0 'clsLnI_nav_ejecucion_res.Max_IdEjecucionRes(CnnLog) + 1
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            BeNavEjecucionRes.IdNavConfigDet = BeConfigDet.Idnavconfigdet
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes, CnnLog)

            BeNavEjecRes = BeNavEjecucionRes

            CnnInterface.Open()

            lTransInterface = CnnInterface.BeginTransaction()

            'lblprg.AppendText("Iniciando transacción a BD: " & Now)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lblprg.AppendText("Consultando WebService de Proveedor en: " & My.MySettings.Default.DynamicsNavInterface_WSProveedores_Proveedores_Service)
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If Not Pregunta_Si_LLena_Intermedia Then

                If Not Importar_Proveedores_DesdeWSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                    Exit Function
                End If

            Else

                If MessageBox.Show("¿Llenar tabla intermedia desde WS?", "Parametro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    If Not Importar_Proveedores_DesdeWSNav_A_TablaIntermedia(lblprg, prg, CnnLog) Then
                        Exit Function
                    End If

                End If

            End If

            Dim lProveedoresFromNav As New List(Of clsBeI_nav_proveedor)

            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("Consultando proveedores en tabla intermedia ")
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            lProveedoresFromNav = clsLnI_nav_proveedor.GetAll(CnnInterface, lTransInterface)

            lblprg.AppendText(String.Format("Proveedores en tabla intermedia: {0}", lProveedoresFromNav.Count))
            lblprg.AppendText(vbNewLine)
            lblprg.Refresh()
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()

            If lProveedoresFromNav.Count > 0 Then

                Dim BeProveedor As clsBeProveedor = Nothing
                Dim BeProveedorBodega As clsBeProveedor_bodega = Nothing
                Dim BeProveedorExistente As clsBeProveedor = Nothing

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, CnnInterface, lTransInterface)

                If BeConfigEnc Is Nothing Then
                    If BD.Instancia.IdConfiguracionInterface = 0 Then
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique el el conn.ini que se especificó el identificador de configuración para la interface.")
                    Else
                        Throw New Exception("No éstá definida la configuración de interface para el Id: " & BD.Instancia.IdConfiguracionInterface & " verifique en la bd que existe el registro asociado al identificador de inteface: " & BD.Instancia.IdConfiguracionInterface)
                    End If
                End If

                prg.Maximum = lProveedoresFromNav.Count

                Dim vContador As Integer = 0

                prg.Value = 0

                lblprg.AppendText("********** TRASLADANDO DOCUMENTO A TOMWMS ********** ")
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                For Each navProveedor As clsBeI_nav_proveedor In lProveedoresFromNav

                    BeProveedor = New clsBeProveedor
                    BeProveedorExistente = New clsBeProveedor

                    BeProveedorExistente = clsLnProveedor.Existe(navProveedor.No, CnnInterface, lTransInterface)

                    lblprg.AppendText(String.Format("Procesando Proveedor: {0}", navProveedor.No))
                    lblprg.AppendText(vbNewLine)
                    lblprg.Refresh()
                    lblprg.SelectionStart = lblprg.TextLength
                    lblprg.ScrollToCaret()

                    prg.Value = vContador

                    vContador += 1

                    If Not BeProveedorExistente Is Nothing Then

                        BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                        BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                        BeProveedor.IdProveedor = BeProveedorExistente.IdProveedor
                        BeProveedor.Codigo = navProveedor.No
                        BeProveedor.Nombre = navProveedor.Name
                        BeProveedor.Telefono = navProveedor.Phone_No
                        BeProveedor.Nit = navProveedor.VAT_Registratrion_No
                        BeProveedor.Direccion = navProveedor.Adress
                        BeProveedor.Contacto = navProveedor.Contact
                        BeProveedor.Activo = True

                        Try
                            clsLnProveedor.Actualizar(BeProveedor, CnnInterface, lTransInterface)
                        Catch ex As Exception
                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                                               BeProveedor.Codigo,
                                                                                               BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                               BeConfigDet.Idnavconfigdet, CnnLog)

                            lblprg.AppendText(String.Format("Error al actualizar proveedor: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, ex.Message))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()
                        End Try

                        VContadorBitacoraTomims += 1

                    Else

                        BeProveedor.IdEmpresa = BeConfigEnc.Idempresa
                        BeProveedor.IdPropietario = BeConfigEnc.IdPropietario
                        BeProveedor.IdProveedor = clsLnProveedor.MaxID(CnnInterface, lTransInterface) + 1

                        BeProveedor.Codigo = navProveedor.No
                        BeProveedor.Nombre = navProveedor.Name
                        BeProveedor.Telefono = navProveedor.Phone_No
                        BeProveedor.Nit = navProveedor.VAT_Registratrion_No
                        BeProveedor.Direccion = navProveedor.Adress
                        BeProveedor.Contacto = navProveedor.Contact
                        BeProveedor.Activo = True
                        BeProveedor.User_agr = BeConfigEnc.IdUsuario
                        BeProveedor.Fec_agr = Date.UtcNow
                        BeProveedor.User_mod = BeConfigEnc.IdUsuario
                        BeProveedor.Fec_mod = Date.UtcNow

                        Try

                            clsLnProveedor.Insertar(BeProveedor, CnnInterface, lTransInterface)

                            VContadorBitacoraTomims += 1

                            BeProveedorBodega = New clsBeProveedor_bodega
                            BeProveedorBodega.IdAsignacion = clsLnProveedor_bodega.MaxID(CnnInterface, lTransInterface) + 1
                            BeProveedorBodega.IdProveedor = BeProveedor.IdProveedor
                            BeProveedorBodega.IdBodega = BeConfigEnc.Idbodega
                            BeProveedorBodega.Activo = True
                            BeProveedorBodega.User_agr = BeConfigEnc.IdUsuario '1 Esto debería ser parametrizable?
                            BeProveedorBodega.User_mod = BeConfigEnc.IdUsuario  '1 Esto debería ser parametrizable?
                            BeProveedorBodega.Fec_agr = Now
                            BeProveedorBodega.Fec_mod = Now

                            clsLnProveedor_bodega.InsertarFromInterface(BeProveedorBodega, CnnInterface, lTransInterface)

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                       BeProveedor.Codigo,
                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                       BeConfigDet.Idnavconfigdet, CnnLog)

                            lblprg.AppendText(String.Format("Error al insertar proveedor: {0}{1}{2}", BeProveedor.Codigo, vbNewLine, ex.Message))
                            lblprg.AppendText(vbNewLine)
                            lblprg.Refresh()
                            lblprg.SelectionStart = lblprg.TextLength
                            lblprg.ScrollToCaret()

                        End Try

                        Application.DoEvents()

                    End If

                Next

            End If

            lTransInterface.Commit()

            '#EJC20171107_REF04_0254AM: Desplegar cantidad de registros de proveedores procesados
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText("********** FIN DE INSERCIÓN EN TOMWMS ********** ")
            lblprg.AppendText(vbNewLine)
            lblprg.AppendText(String.Format("Proveedores procesados correctamente: {0}", VContadorBitacoraTomims))
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

            '#EJC20171006_0106AM: Se quitó la tnrasacción porque se mandaba la de la interface.
            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes, CnnLog)

        Catch ex As Exception
            If Not lTransInterface Is Nothing Then lTransInterface.Rollback()
            lblprg.AppendText(String.Format("Error al insertar Proveedor a tabla DE TOMWMS: {0}", ex.Message))
            lblprg.SelectionStart = lblprg.TextLength
            lblprg.ScrollToCaret()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If CnnInterface.State = ConnectionState.Open Then CnnInterface.Close()
            If CnnLog.State = ConnectionState.Open Then CnnLog.Close()
            prg.Value = 0
            prg.Visible = False
        End Try

    End Function

End Class

