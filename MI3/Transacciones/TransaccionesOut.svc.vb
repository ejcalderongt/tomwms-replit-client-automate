Imports System.Reflection
Imports System.ServiceModel

Public Class TransaccionesOut
    Implements ITransaccionesOut

    Public Function Get_Ingresos_Pendientes_De_Procesar_By_IdTipoDocumento(ByVal pIdTipoDocumento As Integer) As List(Of clsBeI_nav_transacciones_out) Implements ITransaccionesOut.Get_Ingresos_Pendientes_De_Procesar_By_IdTipoDocumento

        Get_Ingresos_Pendientes_De_Procesar_By_IdTipoDocumento = Nothing

        Try

            Return clsLnI_nav_transacciones_out.Get_All_Ingresos_Pendientes_De_Envio_By_IdTipoDocumento(pIdTipoDocumento)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Ingresos_Pendientes_De_Procesar_By_No_Pedido_And_IdTipoDocumento(ByVal pIdTipoDocumento As Integer,
                                                                                         ByVal pNoPedido As String) As List(Of clsBeI_nav_transacciones_out) Implements ITransaccionesOut.Get_Ingresos_Pendientes_De_Procesar_By_No_Pedido_And_IdTipoDocumento

        Get_Ingresos_Pendientes_De_Procesar_By_No_Pedido_And_IdTipoDocumento = Nothing

        Try

            Return clsLnI_nav_transacciones_out.Get_All_Ingresos_Pendientes_De_Envio_By_IdTipoDocumento(pIdTipoDocumento, pNoPedido)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Ingresos_Pendientes_De_Procesar() As List(Of clsBeI_nav_transacciones_out) Implements ITransaccionesOut.Get_Ingresos_Pendientes_De_Procesar

        Get_Ingresos_Pendientes_De_Procesar = Nothing

        Try

            Return clsLnI_nav_transacciones_out.Get_All_Ingresos_Pendientes_De_Envio()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Ingresos_Pendientes_De_Procesar_By_No_Pedido(ByVal pNoPedido As String) As List(Of clsBeI_nav_transacciones_out) Implements ITransaccionesOut.Get_Ingresos_Pendientes_De_Procesar_By_No_Pedido

        Get_Ingresos_Pendientes_De_Procesar_By_No_Pedido = Nothing

        Try

            Return clsLnI_nav_transacciones_out.Get_All_Ingresos_Pendientes_De_Envio(pNoPedido)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Despachos_Pendientes_De_Procesar_By_No_Pedido(ByVal pNoPedido As String) As List(Of clsBeI_nav_transacciones_out) Implements ITransaccionesOut.Get_Despachos_Pendientes_De_Procesar_By_No_Pedido

        Get_Despachos_Pendientes_De_Procesar_By_No_Pedido = Nothing

        Try

            Return clsLnI_nav_transacciones_out.Get_All_Despachos_Pendientes_De_Envio(pNoPedido)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Despachos_Pendientes_De_Procesar_By_No_Pedido_And_IdTipoDocumento(ByVal pNoPedido As String,
                                                                                          ByVal pIdTipoDocumento As clsDataContractDI.tTipoDocumentoSalida) As List(Of clsBeI_nav_transacciones_out) Implements ITransaccionesOut.Get_Despachos_Pendientes_De_Procesar_By_No_Pedido_And_IdTipoDocumento

        Get_Despachos_Pendientes_De_Procesar_By_No_Pedido_And_IdTipoDocumento = Nothing

        Try

            Return clsLnI_nav_transacciones_out.Get_All_Despachos_Pendientes_De_Envio(pNoPedido, pIdTipoDocumento)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Despachos_Pendientes_De_Procesar() As List(Of clsBeI_nav_transacciones_out) Implements ITransaccionesOut.Get_Despachos_Pendientes_De_Procesar

        Get_Despachos_Pendientes_De_Procesar = Nothing

        Try

            Return clsLnI_nav_transacciones_out.Get_All_Despachos_Pendientes_De_Envio()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar_Registro_Procesado(ByRef BeINavTransaccionesOut As clsBeI_nav_transacciones_out) As Integer Implements ITransaccionesOut.Actualizar_Registro_Procesado

        Actualizar_Registro_Procesado = 0

        Try

            Return clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado(BeINavTransaccionesOut)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar_Registro_Procesado_By_No_Pedido(ByVal pNoPedido As String) As Integer Implements ITransaccionesOut.Actualizar_Registro_Procesado_By_No_Pedido

        Actualizar_Registro_Procesado_By_No_Pedido = 0

        Try

            Return clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado_By_No_Pedido(pNoPedido, True)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar_Registro_Procesado_By_No_Pedido_And_IdTipoDocumento(ByVal pNoPedido As String,
                                                                                   ByVal pIdTipoDocumento As Integer) As Integer Implements ITransaccionesOut.Actualizar_Registro_Procesado_By_No_Pedido_And_IdTipoDocumento

        Actualizar_Registro_Procesado_By_No_Pedido_And_IdTipoDocumento = 0

        Try

            Return clsLnI_nav_transacciones_out.Actualizar_Bandera_Enviado_By_No_Pedido_And_IdTipoDocumento(pNoPedido, pIdTipoDocumento, True)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar_Ajuste_Procesado(ByVal pIdAjusteEnc As Integer,
                                                ByVal pEnviado_A_ERP As Boolean,
                                                ByVal pReferencia As String) As Integer Implements ITransaccionesOut.Actualizar_Ajuste_Procesado

        Actualizar_Ajuste_Procesado = 0

        Try

            Return clsLnTrans_ajuste_enc.Actualizar_Estado_Enviado_A_ERP(pIdAjusteEnc,
                                                                         pEnviado_A_ERP,
                                                                         pReferencia)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Ajustes_Pendientes_Envio(ByRef Resultado As String) As List(Of clsBeAjustesMI3) Implements ITransaccionesOut.Get_Ajustes_Pendientes_Envio

        Get_Ajustes_Pendientes_Envio = Nothing

        Try

            Return clsLnI_nav_transacciones_out.Get_Ajustes_Pendientes_Envio_MI3(Resultado)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Insertar_Mensaje_Error_ERP(ByVal pMensajeInterface As clsBeI_nav_transacciones_out_error) As Integer Implements ITransaccionesOut.Insertar_Mensaje_Error_ERP

        Try

            pMensajeInterface.IdMensaje = clsLnI_nav_transacciones_out_error.MaxID() + 1

            Return clsLnI_nav_transacciones_out_error.Insertar(pMensajeInterface)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Mensajes_Error_ERP(ByVal IdDirectorio As Integer,
                                           ByVal FechaDesde As Date,
                                           ByVal FechaHasta As Date) As List(Of clsBeI_nav_transacciones_out_error) Implements ITransaccionesOut.Get_Mensajes_Error_ERP

        Try

            Return clsLnI_nav_transacciones_out_error.Get_All_By_Filtros(IdDirectorio, FechaDesde, FechaHasta)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Stock_Jornada_By_IdPropietario_And_Rango_Fechas(ByVal pIdPropietario As Integer,
                                                                        ByVal FechaDesde As Date,
                                                                        ByVal FechaHasta As Date) As List(Of clsBeStock_jornada) Implements ITransaccionesOut.Get_Stock_Jornada_By_IdPropietario_And_Rango_Fechas

        Try

            Return clsLnStock_jornada.Get_All_By_IdPropietario_And_Rango_Fechas(pIdPropietario, FechaDesde, FechaHasta)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Almacenaje_Historico_By_No_Orden_And_Rango_Fechas(ByVal pNoOrden As String,
                                                                          ByVal FechaDesde As Date,
                                                                          ByVal FechaHasta As Date) As List(Of clsBeStock_jornada_logistico) Implements ITransaccionesOut.Get_Almacenaje_Historico_By_No_Orden_And_Rango_Fechas

        Try

            Return clsLnStock_jornada.Get_Almacenaje_Historico_By_No_Orden_And_Rango_Fechas(pNoOrden,
                                                                                           FechaDesde,
                                                                                           FechaHasta)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    '#CKFK 20211102 Función modificada por cambios solicitados por CEALSA
    Public Function Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas(ByVal pIdCliente As Integer,
                                                                               ByVal pIdConsolidador As Integer,
                                                                               ByVal pNoOrden As String,
                                                                               ByVal FechaDesde As Date,
                                                                               ByVal FechaHasta As Date,
                                                                               ByVal Almacen As clsDataContractDI.tTipoAlmacen,
                                                                               ByVal Clasificacion As String,
                                                                               ByVal pTipoRubro As clsDataContractDI.tRubroERP,
                                                                               ByVal pProducto As String) As List(Of clsBeStock_jornada_logistico) Implements ITransaccionesOut.Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas

        Try

            Return clsLnStock_jornada.Get_Almacenaje_Historico_By_IdPropietario_And_Rango_Fechas(pIdCliente,
                                                                                                 pIdConsolidador,
                                                                                                 pNoOrden,
                                                                                                 FechaDesde,
                                                                                                 FechaHasta,
                                                                                                 Almacen,
                                                                                                 Clasificacion,
                                                                                                 pTipoRubro,
                                                                                                 pProducto)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Servicios_By_IdPropietario_And_Rango_Fechas(ByVal pIdCliente As Integer,
                                                                    ByVal pIdConsolidador As Integer,
                                                                    ByVal pNoOrden As String,
                                                                    ByVal FechaDesde As Date,
                                                                    ByVal FechaHasta As Date,
                                                                    ByVal Almacen As clsDataContractDI.tTipoAlmacen,
                                                                    ByVal pCodigoProducto As String) As List(Of clsBe_Servicio_Logistico) Implements ITransaccionesOut.Get_Servicios_By_IdPropietario_And_Rango_Fechas

        Try

            Return clsLnTrans_servicio_det.Get_Servicios_By_Rango_Fechas(pIdCliente,
                                                                         pIdConsolidador,
                                                                         pNoOrden,
                                                                         FechaDesde,
                                                                         FechaHasta,
                                                                         Almacen,
                                                                         pCodigoProducto)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Servicios_By_No_Orden_And_Rango_Fechas(ByVal pNoOrden As String,
                                                               ByVal FechaDesde As Date,
                                                               ByVal FechaHasta As Date) As List(Of clsBeStock_jornada_logistico) Implements ITransaccionesOut.Get_Servicios_By_No_Orden_And_Rango_Fechas

        Try

            Return clsLnTrans_servicio_enc.Get_Servicios_By_NoOrden_And_Rango_Fechas(pNoOrden,
                                                                                     FechaDesde,
                                                                                     FechaHasta)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Devolucion_De_Ruta_By_Filtros(ByVal pIdTipoDocumento As clsDataContractDI.tTipoDocumentoIngreso,
                                                      ByVal pCodigoBodega As String,
                                                      ByVal pNoDocumentoDevolucionRef As String) As List(Of clsBeI_nav_transacciones_out) Implements ITransaccionesOut.Get_Devolucion_De_Ruta_By_Filtros

        Get_Devolucion_De_Ruta_By_Filtros = Nothing

        Try

            Return clsLnI_nav_transacciones_out.Get_Devolucion_De_Ruta_By_Filtros(pIdTipoDocumento,
                                                                                  pCodigoBodega,
                                                                                  pNoDocumentoDevolucionRef)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Insertar_Referencia_Documento_Salida(ByVal pDocumentoRef As clsBeTrans_pe_docu_ref) As Integer Implements ITransaccionesOut.Insertar_Referencia_Documento_Salida

        Insertar_Referencia_Documento_Salida = 0

        Try

            Dim BeDocumentoRef As New clsBeTrans_pe_docu_ref()

            BeDocumentoRef = clsLnTrans_pe_docu_ref.Get_Single_By_Filtros(pDocumentoRef.Codigo,
                                                                          pDocumentoRef.Nombre,
                                                                          pDocumentoRef.Empresa,
                                                                          pDocumentoRef.Bodega)
            '#EJC20210922: Insertar solo si no existe.
            If BeDocumentoRef Is Nothing Then

                pDocumentoRef.IdDocumentoRef = clsLnTrans_pe_docu_ref.MaxID() + 1
                Return clsLnTrans_pe_docu_ref.Insertar(pDocumentoRef)

            End If

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Desactivar_Referencia_Documento_Salida(ByVal pReferencia As String) As Integer Implements ITransaccionesOut.Desactivar_Referencia_Documento_Salida

        Try


            Return clsLnTrans_pe_docu_ref.Desactivar_Documento(pReferencia)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Set_Flag_Activo_Documento_Referencia_Salida(ByVal IdDocumentoRef As Integer,
                                                                ByVal Activo As Boolean) As Integer Implements ITransaccionesOut.Set_Flag_Activo_Documento_Referencia_Salida

        Try

            Return clsLnTrans_pe_docu_ref.Set_Flag_Activo_Documento(IdDocumentoRef, Activo)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Documento_Referencia_By_Filtros(ByVal Serie As String,
                                                        ByVal NoFactura As String,
                                                        ByVal Empresa As String,
                                                        ByVal Bodega As String) As clsBeTrans_pe_docu_ref Implements ITransaccionesOut.Get_Documento_Referencia_By_Filtros

        Get_Documento_Referencia_By_Filtros = Nothing

        Try

            Return clsLnTrans_pe_docu_ref.Get_Single_By_Filtros(Serie,
                                                                NoFactura,
                                                                Empresa,
                                                                Bodega)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar_Registro_Procesado_By_IdDespacho_And_Codigo_Producto(ByVal pNoPedido As String,
                                                                                    ByVal IdDespachoWMS As Integer,
                                                                                    ByVal pCodigoProducto As String) As Integer Implements ITransaccionesOut.Actualizar_Registro_Procesado_By_IdDespacho_And_Codigo_Producto

        Actualizar_Registro_Procesado_By_IdDespacho_And_Codigo_Producto = 0

        Try

            Return clsLnI_nav_transacciones_out.Actualizar_Registro_Procesado_By_IdDespacho_And_Codigo_Producto(pNoPedido, IdDespachoWMS, pCodigoProducto, True)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar_Registro_Procesado_By_IdRecepcionEnc_And_Codigo_Producto(ByVal pNoPedido As String,
                                                                                        ByVal IdDespachoWMS As Integer,
                                                                                        ByVal pCodigoProducto As String) As Integer Implements ITransaccionesOut.Actualizar_Registro_Procesado_By_IdRecepcionEnc_And_Codigo_Producto

        Actualizar_Registro_Procesado_By_IdRecepcionEnc_And_Codigo_Producto = 0

        Try

            Return clsLnI_nav_transacciones_out.Actualizar_Registro_Procesado_By_IdRecepcionEnc_And_Codigo_Producto(pNoPedido, IdDespachoWMS, pCodigoProducto, True)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar_Registro_Procesado_By_No_Referencia_ERP_Almacenaje(ByVal No_Documento_Procesado_ERP As String,
                                                                                  ByVal pEnviado As Boolean) As Integer Implements ITransaccionesOut.Actualizar_Registro_Procesado_By_No_Referencia_ERP_Almacenaje

        Actualizar_Registro_Procesado_By_No_Referencia_ERP_Almacenaje = 0

        Try

            Return clsLnI_nav_transacciones_out.Actualizar_Registro_Procesado_By_No_Referencia_ERP_Almacenaje(No_Documento_Procesado_ERP, pEnviado)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar_Registro_Procesado_By_No_Referencia_ERP_Servicios(ByVal No_Documento_Procesado_ERP As String,
                                                                                  ByVal pEnviado As Boolean) As Integer Implements ITransaccionesOut.Actualizar_Registro_Procesado_By_No_Referencia_ERP_Servicios

        Actualizar_Registro_Procesado_By_No_Referencia_ERP_Servicios = 0

        Try

            Return clsLnI_nav_transacciones_out.Actualizar_Registro_Procesado_By_No_Referencia_ERP_Almacenaje(No_Documento_Procesado_ERP, pEnviado)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Single_By_IdTransaccion(ByVal IdTransaccion As Integer) As clsBeI_nav_transacciones_out Implements ITransaccionesOut.Get_Single_By_IdTransaccion

        Get_Single_By_IdTransaccion = Nothing

        Try

            Return clsLnI_nav_transacciones_out.GetSingle(IdTransaccion)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Function Insertar_Referencia_Documento_Salida_Con_Detalle(ByVal pDocumentoRef As clsBeTrans_pe_docu_ref,
                                                                     ByVal lDetalle As List(Of clsBeTrans_pe_docu_ref_det)) As Integer Implements ITransaccionesOut.Insertar_Referencia_Documento_Salida_Con_Detalle

        Try

            pDocumentoRef.IdDocumentoRef = clsLnTrans_pe_docu_ref.MaxID() + 1

            Return clsLnTrans_pe_docu_ref.Insertar_Con_Detalle(pDocumentoRef, lDetalle)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar_Registro_Procesado_By_NoPedido_And_Codigo_Producto(ByVal pNoPedido As String,
                                                                                  ByVal pCodigoProducto As String) As Integer Implements ITransaccionesOut.Actualizar_Registro_Procesado_By_NoPedido_And_Codigo_Producto

        Actualizar_Registro_Procesado_By_NoPedido_And_Codigo_Producto = 0

        Try

            Return clsLnI_nav_transacciones_out.Actualizar_Registro_Procesado_By_NoPedido_And_Codigo_Producto(pNoPedido, pCodigoProducto, True)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class