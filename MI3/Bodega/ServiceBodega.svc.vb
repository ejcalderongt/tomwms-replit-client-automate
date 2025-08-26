' NOTE: You can use the "Rename" command on the context menu to change the class name "ServiceBodega" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select ServiceBodega.svc or ServiceBodega.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection
Imports System.ServiceModel

Public Class ServiceBodega
    Implements IServiceBodega

    ''' <summary>
    ''' Gets the single instance of physic warehouse by identifier Idbodega.
    ''' </summary>
    ''' <param name="pIdBodega">The p identifier bodega.</param>
    ''' <returns></returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="Exception"></exception>
    Public Function Get_Single_By_IdBodega(ByVal pIdBodega As Integer) As clsBeBodega Implements IServiceBodega.Get_Single_By_IdClienteBodega

        Get_Single_By_IdBodega = Nothing

        Try

            Dim BeBodega As New clsBeBodega
            BeBodega.IdBodega = pIdBodega
            clsLnBodega.GetSingle(BeBodega)

            Return BeBodega

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_IdBodegaWMS_By_Codigo_Bodega_ERP(ByVal pCodBodegaERP As String) As Integer Implements IServiceBodega.Get_IdBodegaWMS_By_Codigo_Bodega_ERP

        Get_IdBodegaWMS_By_Codigo_Bodega_ERP = 0

        Try

            Get_IdBodegaWMS_By_Codigo_Bodega_ERP = clsLnBodega.Get_IdBodegaWMS_By_Codigo_ERP(pCodBodegaERP)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_IdBodegaWMS_By_Codigo(ByVal pCodigoBodega As String) As Integer Implements IServiceBodega.Get_IdBodegaWMS_By_Codigo

        Get_IdBodegaWMS_By_Codigo = 0

        Try

            Get_IdBodegaWMS_By_Codigo = clsLnBodega.Get_IdBodegaWMS_By_Codigo(pCodigoBodega)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_All() As List(Of clsBeBodega) Implements IServiceBodega.Get_All

        Try

            Return clsLnBodega.GetAll()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Inserta_I_NAV_Bodega(ByVal pBeBodegaERP As clsBeI_nav_bodega) As Integer Implements IServiceBodega.Inserta_I_NAV_Bodega

        Try

            Return clsLnI_nav_bodega.Insertar(pBeBodegaERP)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_All_I_Nav_Bodega() As List(Of clsBeI_nav_bodega) Implements IServiceBodega.Get_All_I_Nav_Bodega

        Try

            Return clsLnI_nav_bodega.GetAll()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Eliminar_I_Nav_Bodega(ByVal pCodigoBodega As String) As Integer Implements IServiceBodega.Eliminar_I_Nav_Bodega

        Try

            Return clsLnI_nav_bodega.Eliminar_By_Bodega_Code(pCodigoBodega)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_AllGet_All_By_IdEmpresa_And_IdUsuario(ByVal pIdEmpresa As Integer, ByVal pIdUsuario As Integer) As List(Of clsBodegasUsuarioRes) Implements IServiceBodega.Get_All_By_IdEmpresa_And_IdUsuario
        Try

            Return clsLnBodega.Get_All_By_IdEmpresa_And_IdUsuario_MI3(pIdEmpresa, pIdUsuario)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

End Class
