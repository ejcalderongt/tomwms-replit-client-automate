' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-28-2018
' ***********************************************************************
' <copyright file="ServiceClienteTipo.svc.vb" company="DTSolutions S.A.">
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection

''' <summary>
''' Class ServiceClienteTipo.
''' </summary>
Public Class ServiceClienteTipo
    Implements IServiceClienteTipo

    ''' <summary>
    ''' Insertars the specified p object um.
    ''' </summary>
    ''' <param name="BeClienteTipo">The p object um.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef BeClienteTipo As clsBeCliente_tipo) As Integer Implements IServiceClienteTipo.Insert

        Try
            Return clsLnCliente_tipo.Insertar(BeClienteTipo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Actualizars the specified p object um.
    ''' </summary>
    ''' <param name="BeClienteTipo">The p object um.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal BeClienteTipo As clsBeCliente_tipo) As Integer Implements IServiceClienteTipo.Update

        Try
            Return clsLnCliente_tipo.Actualizar(BeClienteTipo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Eliminars the specified p object um.
    ''' </summary>
    ''' <param name="BeClienteTipo">The p object um.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Disable(ByRef BeClienteTipo As clsBeCliente_tipo) As Integer Implements IServiceClienteTipo.Disable

        Try
            BeClienteTipo.Activo = False
            Return clsLnCliente_tipo.Actualizar(BeClienteTipo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Maximums the identifier cliente tipo.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Max_IdClienteTipo() As Integer Implements IServiceClienteTipo.Max_IdClienteTipo

        Try
            Return clsLnCliente_tipo.MaxIDClienteTipo()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Gets all filtro.
    ''' </summary>
    ''' <param name="pActivo">if set to <c>true</c> [p activo].</param>
    ''' <returns>List(Of clsBeCliente_tipo).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeCliente_tipo) Implements IServiceClienteTipo.Get_All_Filtro

        Try
            Return clsLnCliente_tipo.GetAllFiltro(pActivo).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Gets the single ClienteTipo.
    ''' </summary>
    ''' <param name="IdClienteTipo">The p identifier cliente tipo.</param>
    ''' <returns>clsBeCliente_tipo.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdClienteTipo(ByVal IdClienteTipo As Integer) As clsBeCliente_tipo Implements IServiceClienteTipo.Get_Single_By_IdClienteTipo

        Try
            Return clsLnCliente_tipo.GetSingle(IdClienteTipo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    ''' <summary>
    ''' Exist the ClienteTipo by identifier propietario bodega.
    ''' </summary>
    ''' <param name="IdPropietarioBodega">The p identifier propietario bodega.</param>
    ''' <returns><c>true</c> if the TipoCliente exist, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist_By_IdPropietarioBodega(ByVal IdPropietarioBodega As Integer) As Boolean Implements IServiceClienteTipo.Exist_By_IdPropietarioBodega

        Try
            Return clsLnCliente_tipo.Existe_By_IdPropietarioBodega(IdPropietarioBodega)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
