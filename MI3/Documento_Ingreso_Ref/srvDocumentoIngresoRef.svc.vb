' NOTE: You can use the "Rename" command on the context menu to change the class name "srvDocumentoIngresoRef" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select srvDocumentoIngresoRef.svc or srvDocumentoIngresoRef.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection
Imports System.ServiceModel

Public Class srvDocumentoIngresoRef
    Implements IsrvDocumentoIngresoRef

    Public Function Insert(ByVal pBeDocuIngresoRef As clsBeTrans_oc_docu_ref) As Integer Implements IsrvDocumentoIngresoRef.Insert

        Try

            Return clsLnTrans_oc_docu_ref.Insertar(pBeDocuIngresoRef)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Function Update(ByVal pBeDocuIngresoRef As clsBeTrans_oc_docu_ref) As Integer Implements IsrvDocumentoIngresoRef.Update

        Try

            Return clsLnTrans_oc_docu_ref.Actualizar(pBeDocuIngresoRef)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function


    Public Function Delete(ByVal pBeDocuIngresoRef As clsBeTrans_oc_docu_ref) As Integer Implements IsrvDocumentoIngresoRef.Delete

        Try

            Return clsLnTrans_oc_docu_ref.Desactivar(pBeDocuIngresoRef)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Function Get_Single_By_Codigo_Documento(ByVal pCodigoDocumento As String) As clsBeTrans_oc_docu_ref Implements IsrvDocumentoIngresoRef.Get_Single_By_Codigo_Documento

        Try

            Return clsLnTrans_oc_docu_ref.Get_Single_By_Codigo_Documento(pCodigoDocumento)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

End Class
