Imports System.Reflection
Imports System.ServiceModel
Imports System.Windows.Forms

Public Class PedidoCompra
    Implements IPedidoCompra

    Public Function Insert_Multiple(ByVal lPedidosCompra As List(Of clsBeI_nav_ped_compra_enc)) As Boolean Implements IPedidoCompra.Insert_Multiple

        Try

            Return clsLnI_nav_ped_compra_enc.Insert_Multiple_Pedido_Compra_From_ERP(lPedidosCompra)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Insert(ByRef BeINavPedCompraEnc As clsBeI_nav_ped_compra_enc,
                           ByRef Resultado As String) As Integer Implements IPedidoCompra.Insert

        Insert = 0

        Dim lblResult As New RichTextBox

        Try

            Resultado = "Validando datos"

            If clsLnI_nav_ped_compra_enc.Datos_Validos(BeINavPedCompraEnc) Then

                Resultado = "Datos Ok"

                If clsLnI_nav_ped_compra_enc.Insert_Single_Pedido_From_ERP(BeINavPedCompraEnc) > 0 Then

                    Resultado = "Procesar_Pedido_Compra_MI3"

                    Dim BePedidoCompraEnc As New clsBeTrans_oc_enc
                    Dim vResult As String = ""

                    If clsLnI_nav_ped_compra_enc.Procesar_Pedido_Compra_MI3(BeINavPedCompraEnc, BePedidoCompraEnc, vResult) Then
                        Insert = 1
                    End If

                    Resultado = vResult

                Else
                    Resultado = "No se pudo insertar el pedido en la tabla intermedia."
                End If

            Else
                Resultado = "Error de validación de datos."
            End If

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Update(ByRef BeINavPedCompraEnc As clsBeI_nav_ped_compra_enc) As Integer Implements IPedidoCompra.Update

        Update = 0

        Try

            If clsLnI_nav_ped_compra_enc.Datos_Validos(BeINavPedCompraEnc) Then
                Return clsLnI_nav_ped_compra_enc.Update_Pedido_Compra_From_ERP(BeINavPedCompraEnc)
            End If

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Delete(ByVal NoEnc As String) As Boolean Implements IPedidoCompra.Delete

        Delete = False

        Try

            Return clsLnI_nav_ped_compra_enc.Delete_By_NoEnc(NoEnc)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Single_By_NoEnc(ByVal NoEnc As String) As clsBeI_nav_ped_compra_enc Implements IPedidoCompra.Get_Single_By_NoEnc

        Try

            Dim BeINavPedCompraEnc As New clsBeI_nav_ped_compra_enc
            BeINavPedCompraEnc.No = NoEnc

            Return clsLnI_nav_ped_compra_enc.GetSingle(BeINavPedCompraEnc)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function
    Public Function Actualizar_Estado_Enviado_A_ERP_By_Referencia(ByVal pReferencia As String,
                                                                  ByVal pEnviado As Boolean) As Integer Implements IPedidoCompra.Actualizar_Estado_Enviado_A_ERP_By_Referencia

        Actualizar_Estado_Enviado_A_ERP_By_Referencia = 0

        Try

            Return clsLnTrans_oc_enc.Actualizar_Estado_Documento_Ingreso_By_Referencia(pReferencia,
                                                                                       pEnviado)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Registrar_Lote_Documento_Ingreso(ByVal pNoDocumentoIngreso As String,
                                                     ByVal pNoLinea As Integer,
                                                     ByVal pCodigoProducto As String,
                                                     ByVal pCantidad As Double,
                                                     ByVal pFechaVence As Date,
                                                     ByVal pLote As String,
                                                     ByVal pLicencia As String,
                                                     ByVal pUbicacionNAV As String) As Boolean Implements IPedidoCompra.Registrar_Lote_Documento_Ingreso

        Registrar_Lote_Documento_Ingreso = False

        Try

            Return clsLnTrans_oc_det_lote.Registrar_Lote_Documento_Ingreso(pNoDocumentoIngreso,
                                                                           pNoLinea,
                                                                           pCodigoProducto,
                                                                           pCantidad,
                                                                           pFechaVence,
                                                                           pLote,
                                                                           pLicencia,
                                                                           pUbicacionNAV)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' #CKFK20220712: Se utiliza en BYB para actualizar la ubicación o la fecha de vencimiento de una licencia.
    ''' </summary>
    ''' <param name="pNoDocumentoIngreso"></param>
    ''' <returns></returns>
    Public Function Actualizar_Licencia_OP(ByVal pNoDocumentoIngreso As String,
                                           ByVal pNoLinea As Integer,
                                           ByVal pCodigoProducto As String,
                                           ByVal pFechaVence As Date,
                                           ByVal pLote As String,
                                           ByVal pLicencia As String,
                                           ByVal pUbicacionNAV As String) As Boolean Implements IPedidoCompra.Actualizar_Licencia_OP

        Actualizar_Licencia_OP = False

        Try

            Return clsLnTrans_oc_det_lote.Actualizar_Licencia_OP(pNoDocumentoIngreso,
                                                                 pNoLinea,
                                                                 pCodigoProducto,
                                                                 pLicencia,
                                                                 pFechaVence,
                                                                 pLote,
                                                                 pUbicacionNAV)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Actualizar_Estado_Enviado_A_ERP_By_No_Documento_Devolucion(ByVal pNo_Documento_Devolucion As String,
                                                                               ByVal pEnviado As Boolean) As Integer Implements IPedidoCompra.Actualizar_Estado_Enviado_A_ERP_By_No_Documento_Devolucion

        Actualizar_Estado_Enviado_A_ERP_By_No_Documento_Devolucion = 0

        Try

            Return clsLnTrans_oc_enc.Actualizar_Estado_Documento_Ingreso_By_No_Documento_Devolucion(pNo_Documento_Devolucion,
                                                                                                    pEnviado)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' #EJC20220411: Se utiliza en BYB para determinar si la Orden de Producción ya existe antes de registrar un lote de ingreso.
    ''' </summary>
    ''' <param name="pNoDocumentoIngreso"></param>
    ''' <returns></returns>
    Public Function Existe_Documento_Ingreso(ByVal pNoDocumentoIngreso As String) As Boolean Implements IPedidoCompra.Existe_Documento_Ingreso

        Existe_Documento_Ingreso = False

        Try

            Return clsLnTrans_oc_det_lote.Existe_Documento(pNoDocumentoIngreso)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' #EJC20220413: Agregado para BYB para que puedan deshabilitar las licencias no procesadas.
    ''' </summary>
    ''' <param name="pNoDocumentoIngreso"></param>
    ''' <param name="pUbicacion"></param>
    ''' <returns></returns>
    Public Function Desactivar_Ubicacion_Documento_Ingreso(ByVal pNoDocumentoIngreso As String,
                                                           ByVal pUbicacion As String) As Boolean Implements IPedidoCompra.Desactivar_Ubicacion_Documento_Ingreso

        Desactivar_Ubicacion_Documento_Ingreso = False

        Try

            Desactivar_Ubicacion_Documento_Ingreso = clsLnTrans_oc_det_lote.Desactivar_Ubicacion_Documento_Ingreso(pNoDocumentoIngreso,
                                                                                                                 pUbicacion)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class