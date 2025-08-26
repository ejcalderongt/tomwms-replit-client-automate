' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 03-29-2018
'
' Last Modified By : ejcalderon
' Last Modified On : 03-29-2018
' ***********************************************************************
' <copyright file="ServiceClienteBodega.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection
Imports System.ServiceModel

''' <summary>
''' Class ServiceClienteBodega.
''' </summary>
Public Class ServiceClienteBodega
    Implements IServiceClienteBodega

    ''' <summary>
    ''' Inserts the specified be cliente bodega.
    ''' </summary>
    ''' <param name="BeClienteBodega">The be cliente bodega.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef BeClienteBodega As clsBeCliente_bodega) As Integer Implements IServiceClienteBodega.Insert

        Try

            Return clsLnCliente_bodega.Insertar(BeClienteBodega)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Updates the specified be cliente bodega.
    ''' </summary>
    ''' <param name="BeClienteBodega">The be cliente bodega.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByRef BeClienteBodega As clsBeCliente_bodega) As Integer Implements IServiceClienteBodega.Update

        Try

            Return clsLnCliente_bodega.Insertar(BeClienteBodega)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Deletes the specified be cliente bodega.
    ''' </summary>
    ''' <param name="BeClienteBodega">The be cliente bodega.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Delete(ByRef BeClienteBodega As clsBeCliente_bodega) As Integer Implements IServiceClienteBodega.Delete

        Try

            Return clsLnCliente_bodega.Eliminar(BeClienteBodega)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier cliente bodega.
    ''' </summary>
    ''' <param name="IdClienteBodega">The identifier cliente bodega.</param>
    ''' <returns>clsBeCliente_bodega.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdClienteBodega(ByVal IdClienteBodega As Integer) As clsBeCliente_bodega Implements IServiceClienteBodega.Get_Single_By_IdClienteBodega

        Try

            Return clsLnCliente_bodega.GetSingle(IdClienteBodega)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s all.
    ''' </summary>
    ''' <returns>List(Of clsBeCliente_bodega).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All() As List(Of clsBeCliente_bodega) Implements IServiceClienteBodega.Get_All

        Try

            Return clsLnCliente_bodega.GetAll().ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Function Max_Id() As Integer Implements IServiceClienteBodega.Max_Id

        Try
            Return clsLnCliente_bodega.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
