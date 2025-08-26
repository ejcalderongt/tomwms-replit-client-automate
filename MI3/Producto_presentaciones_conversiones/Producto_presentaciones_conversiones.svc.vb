Imports System.Reflection
Imports System.ServiceModel

Public Class Producto_presentaciones_conversiones
    Implements IProducto_presentaciones_conversiones
    Public Function Max_Id() As Integer Implements IProducto_presentaciones_conversiones.Max_Id

        Try
            Return clsLnProducto_presentaciones_conversiones.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_All() As List(Of clsBeProducto_presentaciones_conversiones) Implements IProducto_presentaciones_conversiones.Get_All

        Try

            Return clsLnProducto_presentaciones_conversiones.GetAll().ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Single_By_IdConversion(ByVal pIdConversion As Integer) As clsBeProducto_presentaciones_conversiones Implements IProducto_presentaciones_conversiones.Get_Single_By_IdConversion

        Try

            Return clsLnProducto_presentaciones_conversiones.GetSingle(pIdConversion)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Get_Single_By_IdPresentacionOrigen_And_IdPresentacionDestino(ByVal pIdPresentacionOrigen As Integer,
                                               ByVal pIdPresentacionDestino As Integer) As clsBeProducto_presentaciones_conversiones Implements IProducto_presentaciones_conversiones.Get_Single_By_IdPresentacionOrigen_And_IdPresentacionDestino

        Try

            Return clsLnProducto_presentaciones_conversiones.GetSingle(pIdPresentacionOrigen,
                                                                       pIdPresentacionDestino)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Insert(ByRef oBeProducto_presentaciones_conversiones As clsBeProducto_presentaciones_conversiones) As Integer Implements IProducto_presentaciones_conversiones.Insert

        Try

            Return clsLnProducto_presentaciones_conversiones.Insertar(oBeProducto_presentaciones_conversiones)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Update(ByRef oBeProducto_presentaciones_conversiones As clsBeProducto_presentaciones_conversiones) As Integer Implements IProducto_presentaciones_conversiones.Update

        Try

            Return clsLnProducto_presentaciones_conversiones.Actualizar(oBeProducto_presentaciones_conversiones)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Delete(ByRef oBeProducto_presentaciones_conversiones As clsBeProducto_presentaciones_conversiones) As Integer Implements IProducto_presentaciones_conversiones.Delete

        Try

            Return clsLnProducto_presentaciones_conversiones.Eliminar(oBeProducto_presentaciones_conversiones)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
