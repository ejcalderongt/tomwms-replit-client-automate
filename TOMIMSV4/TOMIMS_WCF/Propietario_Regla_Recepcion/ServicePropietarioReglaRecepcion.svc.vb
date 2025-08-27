
Imports System.Reflection

' NOTA: puede usar el comando "Cambiar nombre" del menú contextual para cambiar el nombre de clase "ServicePropietarioReglaRecepcion" en el código, en svc y en el archivo de configuración a la vez.
' NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServicePropietarioReglaRecepcion.svc o ServicePropietarioReglaRecepcion.svc.vb en el Explorador de soluciones e inicie la depuración.
Public Class ServicePropietarioReglaRecepcion
    Implements IServicePropietarioReglaRecepcion

    Public Sub Guarda(ByVal pObjRE As clsBePropietario_reglas_enc, ByVal pListObjR As List(Of clsBePropietario_reglas_det)) Implements IServicePropietarioReglaRecepcion.Guarda

       
        Try

            clsLnPropietario_reglas_enc.Guarda(pObjRE, pListObjR)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    Public Function GetSingle(ByVal pIdReglaPropietarioEnc As Integer) As clsBePropietario_reglas_enc Implements IServicePropietarioReglaRecepcion.GetSingle

        Try

            Return clsLnPropietario_reglas_enc.GetSingle(pIdReglaPropietarioEnc)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllByEncabezado(ByVal pIdReglaPropietarioEnc As Integer) As List(Of clsBePropietario_reglas_det) Implements IServicePropietarioReglaRecepcion.GetAllByEncabezado

        Try

            Return clsLnPropietario_reglas_det.Get_All_By_IdReglaPropietarioEnc(pIdReglaPropietarioEnc)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllDet() As List(Of clsBePropietario_reglas_det) Implements IServicePropietarioReglaRecepcion.GetAllDet

        Try

            Return clsLnPropietario_reglas_det.GetAll()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function GetAllByPropietario(ByVal pIdPropietario As Integer) As List(Of clsBePropietario_reglas_enc) Implements IServicePropietarioReglaRecepcion.GetAllByPropietario

        Try

            Return clsLnPropietario_reglas_enc.Get_All_By_IdPropietario(pIdPropietario)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Sub DesactivarRegla(ByVal pIdReglaPropietarioEnc As Integer) Implements IServicePropietarioReglaRecepcion.DesactivarRegla

        Try

            clsLnPropietario_reglas_enc.DesactivarRegla(pIdReglaPropietarioEnc)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    Public Sub Desactivar(ByVal pIdReglaPropietarioDet As Integer) Implements IServicePropietarioReglaRecepcion.Desactivar

        Try

            clsLnPropietario_reglas_det.Desactivar(pIdReglaPropietarioDet)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    Public Function ExisteRegla(ByVal pIdReglaRecepcion As Integer, ByVal pIdPropietario As Integer) As Boolean Implements IServicePropietarioReglaRecepcion.ExisteRegla

        Try

            Return clsLnPropietario_reglas_enc.ExisteRegla(pIdReglaRecepcion, pIdPropietario)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
