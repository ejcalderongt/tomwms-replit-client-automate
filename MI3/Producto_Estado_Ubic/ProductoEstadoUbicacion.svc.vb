' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-29-2018
' ***********************************************************************
' <copyright file="ProductoEstadoUbicacion.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection
Imports System.ServiceModel

''' <summary>
''' Class ProductoEstadoUbicacion.
''' </summary>
Public Class ProductoEstadoUbicacion
    Implements IProductoEstadoUbicacion

    ''' <summary>
    ''' Inserts the specified be producto_estado_ubic.
    ''' </summary>
    ''' <param name="BeProducto_estado_ubic">The be producto_estado_ubic.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef BeProducto_estado_ubic As clsBeProducto_estado_ubic) As Integer Implements IProductoEstadoUbicacion.Insert

        Try

            Return clsLnProducto_estado_ubic.Insertar(BeProducto_estado_ubic)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Updates the specified be producto_estado_ubic.
    ''' </summary>
    ''' <param name="BeProducto_estado_ubic">The be producto_estado_ubic.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal BeProducto_estado_ubic As clsBeProducto_estado_ubic) As Integer Implements IProductoEstadoUbicacion.Update

        Try

            Return clsLnProducto_estado_ubic.Actualizar(BeProducto_estado_ubic)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Disables the specified be producto_estado_ubic.
    ''' </summary>
    ''' <param name="BeProducto_estado_ubic">The be producto_estado_ubic.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Disable(ByRef BeProducto_estado_ubic As clsBeProducto_estado_ubic) As Integer Implements IProductoEstadoUbicacion.Disable

        Try
            BeProducto_estado_ubic.Activo = False
            Return clsLnProducto_estado_ubic.Actualizar(BeProducto_estado_ubic)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ by_ identifier estado.
    ''' </summary>
    ''' <param name="pIdEstado">The p identifier estado.</param>
    ''' <returns>List(Of clsBeProducto_estado_ubic).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdEstado(ByVal pIdEstado As Integer) As List(Of clsBeProducto_estado_ubic) Implements IProductoEstadoUbicacion.Get_All_By_IdEstado

        Try

            Return clsLnProducto_estado_ubic.Get_All_By_IdEstado(pIdEstado, True).ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class