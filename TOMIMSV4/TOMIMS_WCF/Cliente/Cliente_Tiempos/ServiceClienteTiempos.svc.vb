' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 03-29-2018
'
' Last Modified By : ejcalderon
' Last Modified On : 03-29-2018
' ***********************************************************************
' <copyright file="ServiceClienteTiempos.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection

''' <summary>
''' Class ServiceClienteTiempos.
''' </summary>
Public Class ServiceClienteTiempos
    Implements IServiceClienteTiempos

    ''' <summary>
    ''' Inserts the specified be cliente tiempo.
    ''' </summary>
    ''' <param name="BeClienteTiempo">The be cliente tiempo.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef BeClienteTiempo As clsBeCliente_tiempos) As Integer Implements IServiceClienteTiempos.Insert

        Try
            Return clsLnCliente_tiempos.Insertar(BeClienteTiempo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Updates the specified be cliente tiempo.
    ''' </summary>
    ''' <param name="BeClienteTiempo">The be cliente tiempo.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByRef BeClienteTiempo As clsBeCliente_tiempos) As Integer Implements IServiceClienteTiempos.Update

        Try
            Return clsLnCliente_tiempos.Actualizar(BeClienteTiempo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Deletes the specified be cliente tiempo.
    ''' </summary>
    ''' <param name="BeClienteTiempo">The be cliente tiempo.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Delete(ByRef BeClienteTiempo As clsBeCliente_tiempos) As Integer Implements IServiceClienteTiempos.Delete

        Try
            Return clsLnCliente_tiempos.Eliminar(BeClienteTiempo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier cliente tiempo.
    ''' </summary>
    ''' <param name="IdClienteTiempo">The identifier cliente tiempo.</param>
    ''' <returns>clsBeCliente_tiempos.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdClienteTiempo(ByVal IdClienteTiempo As Integer) As clsBeCliente_tiempos Implements IServiceClienteTiempos.Get_Single_By_IdClienteTiempo

        Try
            Return clsLnCliente_tiempos.GetSingle(IdClienteTiempo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function Get_All() As List(Of clsBeCliente_tiempos) Implements IServiceClienteTiempos.Get_All

        Try

            Return clsLnCliente_tiempos.GetAll().ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
