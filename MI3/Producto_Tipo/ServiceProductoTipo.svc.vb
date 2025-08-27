' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-30-2018
' ***********************************************************************
' <copyright file="ServiceProductoTipo.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection
Imports System.ServiceModel

''' <summary>
''' Class ServiceProductoTipo.
''' </summary>
Public Class ServiceProductoTipo
    Implements IServiceProductoTipo

    ''' <summary>
    ''' Inserts the specified p object pf.
    ''' </summary>
    ''' <param name="pObjPF">The p object pf.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef pObjPF As clsBeProducto_tipo) As Integer Implements IServiceProductoTipo.Insert

        Try
            Return clsLnProducto_tipo.Insertar(pObjPF) > 0
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Updates the specified p object pf.
    ''' </summary>
    ''' <param name="pObjPF">The p object pf.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal pObjPF As clsBeProducto_tipo) As Integer Implements IServiceProductoTipo.Update

        Try
            Return clsLnProducto_tipo.Actualizar(pObjPF)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Disables the specified p object pf.
    ''' </summary>
    ''' <param name="pObjPF">The p object pf.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Disable(ByRef pObjPF As clsBeProducto_tipo) As Integer Implements IServiceProductoTipo.Disable

        Try
            pObjPF.Activo = False
            Return clsLnProducto_tipo.Actualizar(pObjPF)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Deletes the specified p identifier tipo producto.
    ''' </summary>
    ''' <param name="pIdTipoProducto">The p identifier tipo producto.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Delete(ByVal pIdTipoProducto As Integer) Implements IServiceProductoTipo.Delete

        Try
            clsLnProducto_tipo.Delete(pIdTipoProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    ''' <summary>
    ''' Maximums the identifier producto tipo.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function MaxIdProductoTipo() As Integer Implements IServiceProductoTipo.MAXIdProductoTipo

        Try
            Return clsLnProducto_tipo.MAXIdTipoProducto()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Get_s the all_ filtro_ by_ identifier propietario.
    ''' </summary>
    ''' <param name="pActivo">if set to <c>true</c> [p activo].</param>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>List(Of clsBeProducto_tipo).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Filtro_By_IdPropietario(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProducto_tipo) Implements IServiceProductoTipo.Get_All_Filtro_By_IdPropietario

        Try
            Return clsLnProducto_tipo.GetAllFiltro(pActivo, pIdPropietario).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier tipo producto.
    ''' </summary>
    ''' <param name="pIdTipoProducto">The p identifier tipo producto.</param>
    ''' <returns>clsBeProducto_tipo.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdTipoProducto(ByVal pIdTipoProducto As Integer) As clsBeProducto_tipo Implements IServiceProductoTipo.Get_Single_By_IdTipoProducto

        Try
            Return clsLnProducto_tipo.GetSingle(pIdTipoProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier tipo producto_ and_ identifier propietario.
    ''' </summary>
    ''' <param name="pIdTipoProducto">The p identifier tipo producto.</param>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>clsBeProducto_tipo.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdTipoProducto_And_IdPropietario(ByVal pIdTipoProducto As Integer, ByVal pIdPropietario As Integer) As clsBeProducto_tipo Implements IServiceProductoTipo.Get_Single_By_IdTipoProducto_And_IdPropietario

        Try
            Return clsLnProducto_tipo.GetSingle(pIdTipoProducto, pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Exists the specified p identifier tipo producto.
    ''' </summary>
    ''' <param name="pIdTipoProducto">The p identifier tipo producto.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist(ByVal pIdTipoProducto As Integer) As Boolean Implements IServiceProductoTipo.Exist

        Try

            Return clsLnProducto_tipo.Exists(pIdTipoProducto)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    Public Function Exist_By_Codigo(ByVal pCodigo As String) As Boolean Implements IServiceProductoTipo.Exist_By_Codigo

        Try

            Return clsLnProducto_tipo.Exists_By_Codigo(pCodigo)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Existe_s the producto_ asocidado.
    ''' </summary>
    ''' <param name="pIdTipoProducto">The p identifier tipo producto.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Existe_Producto_Asocidado(ByVal pIdTipoProducto As Integer) As Boolean Implements IServiceProductoTipo.Existe_Producto_Asocidado

        Try

            Return clsLnProducto_tipo.ExisteProductoLigado(pIdTipoProducto)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
