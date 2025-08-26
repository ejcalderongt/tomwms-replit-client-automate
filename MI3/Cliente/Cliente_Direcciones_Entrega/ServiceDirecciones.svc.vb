' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 03-29-2018
'
' Last Modified By : ejcalderon
' Last Modified On : 03-29-2018
' ***********************************************************************
' <copyright file="ServiceDirecciones.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection
Imports System.ServiceModel

''' <summary>
''' Class ServiceDirecciones.
''' </summary>
Public Class ServiceDirecciones
    Implements IServiceDirecciones

    ''' <summary>
    ''' Inserts the specified be cliente direccion.
    ''' </summary>
    ''' <param name="BeClienteDireccion">The be cliente direccion.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef BeClienteDireccion As clsBeCliente_direccion) As Integer Implements IServiceDirecciones.Insert

        Try
            Return clsLnCliente_direccion.Insertar(BeClienteDireccion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Updates the specified be cliente direccion.
    ''' </summary>
    ''' <param name="BeClienteDireccion">The be cliente direccion.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByRef BeClienteDireccion As clsBeCliente_direccion) As Integer Implements IServiceDirecciones.Update

        Try
            Return clsLnCliente_direccion.Actualizar(BeClienteDireccion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Deletes the specified be cliente direccion.
    ''' </summary>
    ''' <param name="BeClienteDireccion">The be cliente direccion.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Delete(ByRef BeClienteDireccion As clsBeCliente_direccion) As Integer Implements IServiceDirecciones.Delete

        Try
            Return clsLnCliente_direccion.Eliminar(BeClienteDireccion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier cliente direccion.
    ''' </summary>
    ''' <param name="IdCliente">The identifier cliente.</param>
    ''' <param name="IdDireccion">The identifier direccion.</param>
    ''' <returns>clsBeCliente_direccion.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdClienteDireccion(ByVal IdCliente As Integer, IdDireccion As Integer) As clsBeCliente_direccion Implements IServiceDirecciones.Get_Single_By_IdClienteDireccion

        Try
            Return clsLnCliente_direccion.GetSingle(IdCliente, IdDireccion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s all.
    ''' </summary>
    ''' <param name="IdCliente">The identifier cliente.</param>
    ''' <returns>List(Of clsBeCliente_direccion).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All(ByVal IdCliente As Integer) As List(Of clsBeCliente_direccion) Implements IServiceDirecciones.Get_All_By_IdCliente

        Try

            Return clsLnCliente_direccion.GetAllDireccionesByCliente(IdCliente).ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
