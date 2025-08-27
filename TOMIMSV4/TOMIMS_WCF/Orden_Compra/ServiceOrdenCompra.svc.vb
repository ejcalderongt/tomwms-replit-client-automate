Imports System.Reflection

Public Class ServiceOrdenCompra
    Implements IServiceOrdenCompra

    Function MaxID() As Integer Implements IServiceOrdenCompra.MaxID

        Try
            Return clsLnTrans_oc_enc.MaxID()
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function ActualizarDatos(ByVal pObjE As clsBeTrans_oc_enc,
                                    ByVal pListObjTD As List(Of clsBeTrans_oc_det),
                                    ByVal pObjP As clsBeTrans_oc_pol,
                                    ByVal pListObjI As List(Of clsBeTrans_oc_imagen),
                                    ByVal pListObjP As List(Of clsBeProducto)) As Integer Implements IServiceOrdenCompra.ActualizaDatos

        Try

            Return clsLnTrans_oc_enc.Actualizar_Datos(pObjE, pListObjTD, pObjP, pListObjI, pListObjP)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        Finally

        End Try

    End Function

    Public Function GetSingle(ByVal pIdOrdenCompra As Integer) As clsBeTrans_oc_enc Implements IServiceOrdenCompra.GetSingle

        Try
            Return clsLnTrans_oc_enc.GetSingle(pIdOrdenCompra)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetByImpresion(ByVal pIdOrdenCompra As Integer) As DataTable Implements IServiceOrdenCompra.GetByImpresion

        Try
            Return clsLnTrans_oc_enc.GetImpresionByOC(pIdOrdenCompra)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetOrdenCompraByPropietario(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdPropietarioBodega As Integer) As clsBeTrans_oc_enc Implements IServiceOrdenCompra.GetOrdenCompraByPropietario

        GetOrdenCompraByPropietario = Nothing

        Try
            'Return clsLnTrans_oc_enc.GetOrdenCompraByPropietario(pIdOrdenCompraEnc, pIdPropietarioBodega)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetOrdenCompra(ByVal pIdOrdenCompraEnc As Integer) As clsBeTrans_oc_enc Implements IServiceOrdenCompra.GetOrdenCompra

        GetOrdenCompra = Nothing

        Try
            'Return clsLnTrans_oc_enc.GetOrdenCompra(pIdOrdenCompraEnc)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAll(ByVal pActivo As Boolean, ByVal pFechaDel As Date, ByVal pFechaAl As Date, Optional ByVal pIdBodega As Integer = 0, Optional ByVal pIdPropietario As Integer = 0) As DataTable Implements IServiceOrdenCompra.GetAll

        Try
            Return clsLnTrans_oc_enc.GetAll(pActivo, pFechaDel, pFechaAl, pIdBodega, pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetDetalle(ByVal pIdOrdenCompra As Integer) As List(Of clsBeTrans_oc_det) Implements IServiceOrdenCompra.GetDetalle

        Try
            Return clsLnTrans_oc_det.Get_All_TransOcDet_By_IdOrdenCompraEnc(pIdOrdenCompra).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetImpresionByOC(ByVal pIdOrdenCompra As Integer) As DataTable Implements IServiceOrdenCompra.GetImpresionByOC

        Try
            Return clsLnTrans_oc_enc.GetImpresionByOC(pIdOrdenCompra)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetCodigos(ByVal pIdBobdega As Integer, ByVal pIdPropietario As Int16) As DataTable Implements IServiceOrdenCompra.GetCodigos

        Try
            Return clsLnProducto.GetCodigos(pIdBobdega, pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetPoliza(ByVal pIdOrdenCompra As Integer) As clsBeTrans_oc_pol Implements IServiceOrdenCompra.GetPoliza

        Try
            Return clsLnTrans_oc_pol.GetSingle(pIdOrdenCompra)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetImagenes(ByVal pIdOrdenCompra As Integer) As List(Of clsBeTrans_oc_imagen) Implements IServiceOrdenCompra.GetImagenes

        Try
            Return clsLnTrans_oc_imagen.GetByOrdenCompra(pIdOrdenCompra).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Sub DeleteImageByIndex(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdOrdenCompraImg As Integer) Implements IServiceOrdenCompra.DeleteImageByIndex

        Try
            clsLnTrans_oc_imagen.Delete(pIdOrdenCompraEnc, pIdOrdenCompraImg)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    Public Sub Delete(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdOrdenCompraDet As Integer) Implements IServiceOrdenCompra.Delete

        Try
            clsLnTrans_oc_det.Delete(pIdOrdenCompraEnc, pIdOrdenCompraDet)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Sub

    Public Function Anula(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdMotivoAnulacionBodega As Integer) As Boolean Implements IServiceOrdenCompra.Anula

        Anula = False

        Dim lListRecepciones As New List(Of Integer)

        Try
            lListRecepciones = clsLnTrans_re_oc.Get_IdRecepcionEnc_By_IdOrdenCompraEnc(pIdOrdenCompraEnc).ToList
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

        Dim lConnection As New SqlClient.SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlClient.SqlTransaction = Nothing

        Try

            lConnection.Open()

            lTransaction = lConnection.BeginTransaction()

            ' Anulamos todas las recepciones ligadas a la orden de compra
            For Each recepcion As Integer In lListRecepciones
                clsLnTrans_re_enc.Anular(recepcion, lConnection, lTransaction)
            Next

            ' Anulamos la orden de compra correspondiente
            clsLnTrans_oc_enc.Set_Estado_Anulado_OC(pIdOrdenCompraEnc, pIdMotivoAnulacionBodega, lConnection, lTransaction)

            lTransaction.Commit()
            lConnection.Close()

            Anula = True

        Catch ex As Exception
            lTransaction.Rollback()
            lConnection.Close()
            Throw New FaultException(ex.Message)
        End Try

    End Function

    Public Function GetAllByOrdenCompra(ByVal pIdOrdenCompra As Integer) As List(Of clsBeTrans_oc_det) Implements IServiceOrdenCompra.GetAllByOrdenCompra

        Try

            Return clsLnTrans_oc_det.Get_All_By_IdOrdenCompraEnc(pIdOrdenCompra).ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' suma las cantidades de todas la ordenes de compra que aun no han sido recibidas
    ''' </summary>
    ''' <param name="pIdProducto"></param>
    ''' <returns></returns>
    Public Function CantidadTransito(ByVal pIdProducto As Integer, ByVal pIdPresentacion As Integer) As Double Implements IServiceOrdenCompra.CantidadTransito

        Try

            Return clsLnTrans_oc_enc.CantidadTransito(pIdProducto, pIdPresentacion)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Tiene_Recepciones_Activas(ByVal pIdOrdenCompraEnc As Integer) As Boolean Implements IServiceOrdenCompra.Tiene_Recepciones_Activas

        Tiene_Recepciones_Activas = False

        Try

            Return clsLnTrans_re_enc.OC_Tiene_Recepciones_Activas(pIdOrdenCompraEnc)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
