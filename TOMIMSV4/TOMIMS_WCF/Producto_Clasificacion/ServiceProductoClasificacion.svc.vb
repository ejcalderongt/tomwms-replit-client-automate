' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-29-2018
' ***********************************************************************
' <copyright file="ServiceProductoClasificacion.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection

''' <summary>
''' Class ServiceProductoClasificacion.
''' </summary>
Public Class ServiceProductoClasificacion
    Implements IServiceProductoClasificacion

    ''' <summary>
    ''' Inserts the specified p be producto clasificacion.
    ''' </summary>
    ''' <param name="pBeProductoClasificacion">The p be producto clasificacion.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef pBeProductoClasificacion As clsBeProducto_clasificacion) As Integer Implements IServiceProductoClasificacion.Insert

        Try            
            Return clsLnProducto_clasificacion.Insertar(pBeProductoClasificacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Guardars the transaccion.
    ''' </summary>
    ''' <param name="pListBeProductoClasificacion">The p list be producto clasificacion.</param>
    ''' <exception cref="FaultException"></exception>
    Public Sub GuardarTransaccion(ByVal pListBeProductoClasificacion As List(Of clsBeProducto_clasificacion)) Implements IServiceProductoClasificacion.GuardarTransaccion

        Try

            clsLnProducto_clasificacion.GuardarTransaccion(pListBeProductoClasificacion)

        Catch ex As Exception
            Throw New FaultException(ex.Message)     
        End Try

    End Sub

    ''' <summary>
    ''' Updates the specified p be producto clasificacion.
    ''' </summary>
    ''' <param name="pBeProductoClasificacion">The p be producto clasificacion.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal pBeProductoClasificacion As clsBeProducto_clasificacion) As Integer Implements IServiceProductoClasificacion.Update

        Try            
            Return clsLnProducto_clasificacion.Actualizar(pBeProductoClasificacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Disables the specified p be producto clasificacion.
    ''' </summary>
    ''' <param name="pBeProductoClasificacion">The p be producto clasificacion.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Disable(ByRef pBeProductoClasificacion As clsBeProducto_clasificacion) As Integer Implements IServiceProductoClasificacion.Disable

        Try            
            pBeProductoClasificacion.Activo = False
            Return clsLnProducto_clasificacion.Actualizar(pBeProductoClasificacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Deletes the specified p identifier clasificacion.
    ''' </summary>
    ''' <param name="pIdClasificacion">The p identifier clasificacion.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Delete(ByVal pIdClasificacion As Integer) Implements IServiceProductoClasificacion.Delete

        Try
            clsLnProducto_clasificacion.Delete(pIdClasificacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    ''' <summary>
    ''' Deletes the by propietario.
    ''' </summary>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub DeleteByPropietario(ByVal pIdPropietario As Integer) Implements IServiceProductoClasificacion.DeleteByPropietario

        Try
            clsLnProducto_clasificacion.DeleteByPropietario(pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    ''' <summary>
    ''' Max_s the identifier producto_ clasificacion.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Max_IdProducto_Clasificacion() As Integer Implements IServiceProductoClasificacion.Max_IdProducto_Clasificacion

        Try
            Return clsLnProducto_clasificacion.Max_IdProducto_Clasificacion()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function
 _
    ''' <summary>
    ''' Get_s the all_ filtro.
    ''' </summary>
    ''' <param name="pActivo">if set to <c>true</c> [p activo].</param>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>List(Of clsBeProducto_clasificacion).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProducto_clasificacion) Implements IServiceProductoClasificacion.Get_All_Filtro

        Try
            Return clsLnProducto_clasificacion.Get_All_Filtro(pActivo, pIdPropietario).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Exists the specified identifier clasificacion.
    ''' </summary>
    ''' <param name="IdClasificacion">The identifier clasificacion.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist(ByVal IdClasificacion As Integer) As Boolean Implements IServiceProductoClasificacion.Exist

        Try
            Return clsLnProducto_clasificacion.Exists(IdClasificacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Exist_s the producto_ asociado.
    ''' </summary>
    ''' <param name="IdClasificacion">The identifier clasificacion.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist_Producto_Asociado(ByVal IdClasificacion As Integer) As Boolean Implements IServiceProductoClasificacion.Exist_Producto_Asociado

        Try
            Return clsLnProducto_clasificacion.ExisteProductoLigado(IdClasificacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier producto clasificacion.
    ''' </summary>
    ''' <param name="IdProductoClasificacion">The identifier producto clasificacion.</param>
    ''' <returns>clsBeProducto_clasificacion.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdProductoClasificacion(ByVal IdProductoClasificacion As Integer) As clsBeProducto_clasificacion Implements IServiceProductoClasificacion.Get_Single_By_IdProductoClasificacion

        Try
            Return clsLnProducto_clasificacion.GetSingle(IdProductoClasificacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier producto clas_ and_ identifier propietario.
    ''' </summary>
    ''' <param name="pIdProductoClasificacion">The p identifier producto clasificacion.</param>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>clsBeProducto_clasificacion.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdProductoClas_And_IdPropietario(ByVal pIdProductoClasificacion As Integer, 
                                                                   ByVal pIdPropietario As Integer) As clsBeProducto_clasificacion Implements IServiceProductoClasificacion.Get_Single_By_IdProductoClas_And_IdPropietario

        Try
            Return clsLnProducto_clasificacion.GetSingle(pIdProductoClasificacion, pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
