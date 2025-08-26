' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-30-2018
' ***********************************************************************
' <copyright file="ServicePropietario.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection

''' <summary>
''' Class ServicePropietario.
''' </summary>
Public Class ServicePropietario
    Implements IServicePropietario

    ''' <summary>
    ''' Inserts the specified p object p.
    ''' </summary>
    ''' <param name="pObjP">The p object p.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef pObjP As clsBePropietarios) As Integer Implements IServicePropietario.Insert

        Try
            Return clsLnPropietarios.Insertar(pObjP)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Updates the specified p object p.
    ''' </summary>
    ''' <param name="pObjP">The p object p.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal pObjP As clsBePropietarios) As Integer Implements IServicePropietario.Update

        Try
            Return clsLnPropietarios.Actualizar(pObjP)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Disables the specified p object p.
    ''' </summary>
    ''' <param name="pObjP">The p object p.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Disable(ByRef pObjP As clsBePropietarios) As Integer Implements IServicePropietario.Disable

        Try
            pObjP.Activo = False
            Return clsLnPropietarios.Actualizar(pObjP)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Maximums the identifier propietario.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function MaxIdPropietario() As Integer Implements IServicePropietario.MAXIdPropietario

        Try
            Return clsLnPropietarios.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Get_s the all_ filtro.
    ''' </summary>
    ''' <param name="pActivo">if set to <c>true</c> [p activo].</param>
    ''' <returns>List(Of clsBePropietarios).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBePropietarios) Implements IServicePropietario.Get_All_Filtro

        Try
            Return clsLnPropietarios.Get_All_Filtro(pActivo).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Exists the specified p identifier propietario.
    ''' </summary>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist(ByVal pIdPropietario As Integer) As Boolean Implements IServicePropietario.Exist

        Try

            Return clsLnPropietarios.Exists(pIdPropietario)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier propietario.
    ''' </summary>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>clsBePropietarios.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdPropietario(ByVal pIdPropietario As Integer) As clsBePropietarios Implements IServicePropietario.Get_Single_By_IdPropietario

        Try
            Return clsLnPropietarios.GetSingle(pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
