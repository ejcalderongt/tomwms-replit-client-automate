Imports System.Reflection
Imports System.ServiceModel
Imports System.Windows.Forms

Public Class PedidoCliente
    Implements IPedidoCliente

    Public Function Insert_Multiple(ByVal lPedidosCompra As List(Of clsBeI_nav_ped_traslado_enc), ByRef Resultado As String) As Boolean Implements IPedidoCliente.Insert_Multiple

        Try

            Return clsLnI_nav_ped_traslado_enc.Importar_Pedidos_Clientes_A_Tabla_Intermedia(lPedidosCompra, Resultado)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Insert(ByRef BeINavPedCompraEnc As clsBeI_nav_ped_traslado_enc, ByRef Resultado As String) As Integer Implements IPedidoCliente.Insert

        Insert = 0

        Try

            If Datos_Validos(BeINavPedCompraEnc) Then

                Dim BePedidoEnc As New clsBeTrans_pe_enc
                Dim cantLineas As Integer = 0

                BePedidoEnc = clsLnI_nav_ped_traslado_enc.Importar_Pedido_Cliente_A_Tabla_Intermedia_If(BeINavPedCompraEnc, Resultado)
                cantLineas = clsLnTrans_pe_det.Get_Count_Lines_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc)

                Insert = cantLineas

            End If

        Catch ex As FaultException
            Throw New FaultException(ex.Message)
        Catch ex1 As Exception
            Throw New Exception(ex1.Message)
        End Try

    End Function

    Public Function Update(ByRef BeINavPedCompraEnc As clsBeI_nav_ped_traslado_enc) As Integer Implements IPedidoCliente.Update

        Update = 0

        Try

            If Datos_Validos(BeINavPedCompraEnc) Then
                Return clsLnI_nav_ped_traslado_enc.Actualizar(BeINavPedCompraEnc)
            End If

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Update_Receipt_Document_Reference(ByRef BeINavPedCompraEnc As clsBeI_nav_ped_traslado_enc) As Integer Implements IPedidoCliente.Update_Receipt_Document_Reference

        Update_Receipt_Document_Reference = 0

        Try

            If Datos_Validos(BeINavPedCompraEnc) Then
                Return clsLnI_nav_ped_traslado_enc.Actualizar_Documento_De_Ingreso_En_Bodega_Destino(BeINavPedCompraEnc)
            End If

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    'Actualizar_Documento_De_Ingreso_En_Bodega_Destino
    Public Function Delete(ByVal NoEnc As String) As Boolean Implements IPedidoCliente.Delete

        Delete = False

        Try

            Return clsLnI_nav_ped_traslado_enc.Eliminar_MI3(NoEnc)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Single_By_NoEnc(ByVal NoEnc As String) As clsBeI_nav_ped_traslado_enc Implements IPedidoCliente.Get_Single_By_NoEnc

        Try

            Dim BeINavPedCompraEnc As New clsBeI_nav_ped_traslado_enc
            BeINavPedCompraEnc.No = NoEnc

            Return clsLnI_nav_ped_traslado_enc.GetSingle(BeINavPedCompraEnc)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Private Function Datos_Validos(ByRef BeINavPedClienteEnc As clsBeI_nav_ped_traslado_enc) As Boolean

        Datos_Validos = False

        Try

            '#EJC20180810_0145AM: Validar que tenga detalle el documento.
            If BeINavPedClienteEnc.Lineas_Detalle Is Nothing Then
                Throw New Exception("No se proporcionó el detalle del documento")
            ElseIf BeINavPedClienteEnc.Lineas_Detalle.Count = 0 Then
                Throw New Exception("No se proporcionó el detalle del documento")
            ElseIf BeINavPedClienteEnc.No = "" Then
                Throw New Exception("El número de documento no puede ser vacío ")
            ElseIf clsLnI_nav_ped_traslado_enc.Exist(BeINavPedClienteEnc.No) Then
                Throw New Exception("El número de documento: " & BeINavPedClienteEnc.No & " ya existe.")
            ElseIf BeINavPedClienteEnc.Product_Owner_Code = "" Then
                Throw New Exception("El campo Producto_Owner_Code no puede ser vacío, este valor corresponde al codigo de propietario tabla -> propietarios ")
            Else
                Datos_Validos = True
            End If

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar_Estado_Enviado_A_ERP_By_Referencia(ByVal pReferencia As String,
                                                                  ByVal pEnviado As Boolean) As Integer Implements IPedidoCliente.Actualizar_Estado_Enviado_A_ERP_By_Referencia

        Actualizar_Estado_Enviado_A_ERP_By_Referencia = 0

        Try

            Return clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP_By_Referencia(pReferencia,
                                                                                   pEnviado)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer,
                                                                   ByVal pEnviado As Boolean) As Integer Implements IPedidoCliente.Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc

        Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc = 0

        Try

            Return clsLnTrans_pe_enc.Actualizar_Estado_Enviado_A_ERP_By_IdPedidoEnc(pIdPedidoEnc,
                                                                                    pEnviado,
                                                                                    0)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    ''' <summary>
    ''' Actualiza el número de documento del ERP en un pedido despachado desde WMS 
    ''' Se utiliza cuando el pedido fue creado en WMS y procesado posteriormente en el ERP.
    ''' Actualiza también los registros en la tabla I_NAV_TRANSACCIONES_OUT
    ''' </summary>
    ''' <param name="pReferencia"></param>
    ''' <param name="pIdPedidoEnc"></param>
    ''' <returns>Devuelve la cantidad de registros afectados por el Update</returns>
    Public Function Actualizar_Referencia_By_IdPedidoEnc(ByVal pReferencia As String,
                                                         ByVal pIdPedidoEnc As Integer) As Integer Implements IPedidoCliente.Actualizar_Referencia_By_IdPedidoEnc

        Actualizar_Referencia_By_IdPedidoEnc = False

        Try

            Return clsLnTrans_pe_enc.Actualizar_Referencia_By_IdPedidoEnc(pReferencia,
                                                                          pIdPedidoEnc)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Generar_Ingreso_Por_Anulacion_NC_SAP(ByVal IdPedidoEnc As Integer,
                                                         ByVal DocEntrySolicitudDevolucion As Integer) As Boolean Implements IPedidoCliente.Generar_Ingreso_Por_Anulacion_NC_SAP

        Generar_Ingreso_Por_Anulacion_NC_SAP = False

        Dim BePedidoEnc As New clsBeTrans_pe_enc
        Dim BePedidoCompraEnc As New clsBeTrans_oc_enc
        Dim BePedidoDetWMS As New clsBeI_nav_ped_compra_det()
        Dim vResult As String = ""

        Try

            BePedidoEnc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc)

            If Not BePedidoEnc Is Nothing Then

                Dim BeINavPedCompra As New clsBeI_nav_ped_compra_enc
                BeINavPedCompra = New clsBeI_nav_ped_compra_enc()
                BeINavPedCompra.No = clsLnTrans_oc_enc.MaxID() + 1
                BeINavPedCompra.Posting_Date = Now
                BeINavPedCompra.Order_Date = Now
                BeINavPedCompra.Document_Date = Now
                BeINavPedCompra.Expected_Receipt_Date = Now
                BeINavPedCompra.Status = 1
                BeINavPedCompra.Buy_From_Vendor_No = BePedidoEnc.Cliente.Codigo
                BeINavPedCompra.Buy_From_Vendor_Name = BePedidoEnc.Cliente.Nombre_comercial
                BeINavPedCompra.Is_Internal_Transfer = False
                BeINavPedCompra.Location_Code = BePedidoEnc.IdBodega
                BeINavPedCompra.Vendor_Invoice_No = DocEntrySolicitudDevolucion
                BeINavPedCompra.Posting_Description = BePedidoEnc.Observacion
                BeINavPedCompra.Product_Owner_Code = BePedidoEnc.PropietarioBodega.IdPropietario
                BeINavPedCompra.Vendor_Invoice_No = BePedidoEnc.IdPedidoEnc
                BeINavPedCompra.Document_Type = clsDataContractDI.tTipoDocumentoIngreso.Ingreso_Por_NC_Anulada
                BeINavPedCompra.IsImport = False

                Dim BeProducto As New clsBeProducto

                For Each Det In BePedidoEnc.Detalle

                    BeProducto = clsLnProducto.Get_Single_By_IdProductoBodega(Det.IdProductoBodega)

                    If Not BeProducto Is Nothing Then

                        BePedidoDetWMS = New clsBeI_nav_ped_compra_det()
                        BePedidoDetWMS.NoEnc = BeINavPedCompra.No
                        BePedidoDetWMS.No = BeProducto.Codigo
                        BePedidoDetWMS.Line_No = Det.No_linea
                        BePedidoDetWMS.Planed_Receipt_Date = Date.Now
                        BePedidoDetWMS.Quantity = Det.Cantidad
                        BePedidoDetWMS.Quantity_Received = 0
                        BePedidoDetWMS.Description = BeProducto.Nombre
                        BePedidoDetWMS.Unit_of_Measure_Code = Det.Nom_unid_med
                        BePedidoDetWMS.Type = 2
                        BePedidoDetWMS.Variant_Code = Nothing
                        BePedidoDetWMS.Location_Code = BePedidoCompraEnc.IdBodega
                        BeINavPedCompra.Lineas_Detalle.Add(BePedidoDetWMS)

                    Else
                        Throw New Exception("no se pudo obtener el producto")
                    End If

                Next

                If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavPedCompra,
                                                                        BePedidoCompraEnc,
                                                                        vResult) Then
                    Generar_Ingreso_Por_Anulacion_NC_SAP = True
                Else
                    Throw New Exception(vResult)
                End If


            Else
                Throw New Exception("No se obtuvo el objeto de pedido para el IdPedidodEnc: " & IdPedidoEnc)
            End If

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
