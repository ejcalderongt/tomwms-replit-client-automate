' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-30-2018
' ***********************************************************************
' <copyright file="ServiceProveedor.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection
Imports System.ServiceModel
''' <summary>
''' Class ServiceProveedor.
''' </summary>
Public Class ServiceProveedor
    Implements IServiceProveedor

    ''' <summary>
    ''' Inserts the specified be proveedor.
    ''' </summary>
    ''' <param name="BeProveedor">The be proveedor.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef BeProveedor As clsBeProveedor) As Integer Implements IServiceProveedor.Insert

        Try
            Return clsLnProveedor.Insertar(BeProveedor)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Updates the specified be proveedor.
    ''' </summary>
    ''' <param name="BeProveedor">The be proveedor.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal BeProveedor As clsBeProveedor) As Integer Implements IServiceProveedor.Update

        Try
            Return clsLnProveedor.Actualizar(BeProveedor)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Disables the specified be proveedor.
    ''' </summary>
    ''' <param name="BeProveedor">The be proveedor.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Disable(ByRef BeProveedor As clsBeProveedor) As Integer Implements IServiceProveedor.Disable

        Try
            BeProveedor.Activo = False
            Return clsLnProveedor.Actualizar(BeProveedor)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Maximums the identifier proveedor.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function MaxIdProveedor() As Integer Implements IServiceProveedor.MaxIdProveedor

        Try
            Return clsLnProveedor.MaxID()
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
    ''' <returns>List(Of clsBeProveedor).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal IdBodega As Integer) As List(Of clsBeProveedor) Implements IServiceProveedor.Get_All_Filtro

        Try
            Return clsLnProveedor.Get_All_Filtro(pActivo, IdBodega).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ by_ identifier propietario.
    ''' </summary>
    ''' <param name="pActivo">if set to <c>true</c> [p activo].</param>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>List(Of clsBeProveedor).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdPropietario(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProveedor) Implements IServiceProveedor.Get_All_By_IdPropietario

        Try
            Return clsLnProveedor.GetAllByPropietario(pActivo, pIdPropietario).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ by_ identifier bodega.
    ''' </summary>
    ''' <param name="pIdBodega">The p identifier bodega.</param>
    ''' <returns>List(Of clsBeProveedor_bodega).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeProveedor_bodega) Implements IServiceProveedor.Get_All_By_IdBodega

        Try
            Return clsLnProveedor_bodega.Get_All_By_IdBodega_HH(pIdBodega).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier proveedor.
    ''' </summary>
    ''' <param name="pIdProveedor">The p identifier proveedor.</param>
    ''' <returns>clsBeProveedor.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdProveedor(ByVal pIdProveedor As Integer) As clsBeProveedor Implements IServiceProveedor.Get_Single_By_IdProveedor

        Try
            Return clsLnProveedor.GetSingle(pIdProveedor)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the by_ identifier proveedor_ and_ identifier propietario bodega.
    ''' </summary>
    ''' <param name="pIdProveedor">The p identifier proveedor.</param>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>clsBeProveedor.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_By_IdProveedor_And_IdPropietarioBodega(ByVal pIdProveedor As Integer, ByVal pIdPropietario As Integer) As clsBeProveedor Implements IServiceProveedor.Get_By_IdProveedor_And_IdPropietarioBodega

        Try
            Return clsLnProveedor.Get_Single_By_IdProveedor_And_IdPropietario(pIdProveedor, pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
