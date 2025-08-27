' NOTE: You can use the "Rename" command on the context menu to change the class name "ServiceOperadorBodega" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select ServiceOperadorBodega.svc or ServiceOperadorBodega.svc.vb at the Solution Explorer and start debugging.
Imports System.Reflection
Imports System.ServiceModel

Public Class ServiceOperadorBodega
    Implements IServiceOperadorBodega

    Public Function Insert(ByRef BeOperadorBodega As clsBeOperador_bodega) As Integer Implements IServiceOperadorBodega.Insert

        Try
            Return clsLnOperador_bodega.Insertar(BeOperadorBodega)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Update(ByRef BeOperadorBodega As clsBeOperador_bodega) As Integer Implements IServiceOperadorBodega.Update

        Try

            Return clsLnOperador_bodega.Actualizar(BeOperadorBodega)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Delete(ByRef BeOperadorBodega As clsBeOperador_bodega) As Integer Implements IServiceOperadorBodega.Delete

        Delete = 0

        Try

            Return clsLnOperador_bodega.Eliminar(BeOperadorBodega)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_AllOperadorBodega() As List(Of clsBeOperador_bodega) Implements IServiceOperadorBodega.Get_AllOperadorBodega

        Try

            Return clsLnOperador_bodega.GetAll()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Max_IdOperadorBodega() As Integer Implements IServiceOperadorBodega.Max_IdOperadorBodega

        Try

            Return clsLnOperador_bodega.MaxID()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
