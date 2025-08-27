' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-29-2018
' ***********************************************************************
' <copyright file="ServiceProductoMarca.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection

''' <summary>
''' Class ServiceProductoMarca.
''' </summary>
Public Class ServiceProductoMarca
    Implements IServiceProductoMarca

    ''' <summary>
    ''' Inserts the specified o be producto_ marca.
    ''' </summary>
    ''' <param name="oBeProducto_Marca">The o be producto_ marca.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef oBeProducto_Marca As clsBeProducto_Marca) As Integer Implements IServiceProductoMarca.Insert

        Try

            Return clsLnProducto_marca.Insertar(oBeProducto_Marca)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Insert_s the multiple.
    ''' </summary>
    ''' <param name="pListBeProducto_Marca">The p list be producto_ marca.</param>
    ''' <exception cref="FaultException"></exception>
    Public Sub Insert_Multiple(ByVal pListBeProducto_Marca As List(Of clsBeProducto_Marca)) Implements IServiceProductoMarca.Insert_Multiple

        Try

            clsLnProducto_marca.GuardarTransaccion(pListBeProducto_Marca)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Updates the specified o be producto_ marca.
    ''' </summary>
    ''' <param name="oBeProducto_Marca">The o be producto_ marca.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal oBeProducto_Marca As clsBeProducto_Marca) As Integer Implements IServiceProductoMarca.Update

        Try

            Return clsLnProducto_marca.Actualizar(oBeProducto_Marca)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Disables the specified o be producto_ marca.
    ''' </summary>
    ''' <param name="oBeProducto_Marca">The o be producto_ marca.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Disable(ByRef oBeProducto_Marca As clsBeProducto_Marca) As Integer Implements IServiceProductoMarca.Disable

        Try

            oBeProducto_Marca.Activo = False

            Return clsLnProducto_marca.Actualizar(oBeProducto_Marca)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Deletes the specified p identifier producto marca.
    ''' </summary>
    ''' <param name="pIdProductoMarca">The p identifier producto marca.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Delete(ByVal pIdProductoMarca As Integer) Implements IServiceProductoMarca.Delete

        Try
            clsLnProducto_marca.Delete(pIdProductoMarca)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    ''' <summary>
    ''' Delete_s the by_ identifier propietario.
    ''' </summary>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Delete_By_IdPropietario(ByVal pIdPropietario As Integer) Implements IServiceProductoMarca.Delete_By_IdPropietario

        Try
            clsLnProducto_marca.DeleteByPropietario(pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    ''' <summary>
    ''' Max_s the identifier marca.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Max_IdMarca() As Integer Implements IServiceProductoMarca.Max_IdMarca

        Try
            Return clsLnProducto_marca.Max_IdMarca()
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
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>List(Of clsBeProducto_Marca).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeProducto_Marca) Implements IServiceProductoMarca.Get_All_Filtro

        Try

            Return clsLnProducto_marca.GetAllFiltro(pActivo, pIdPropietario).ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier marca.
    ''' </summary>
    ''' <param name="pIdMarca">The p identifier marca.</param>
    ''' <returns>clsBeProducto_Marca.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdMarca(ByVal pIdMarca As Integer) As clsBeProducto_Marca Implements IServiceProductoMarca.Get_Single_By_IdMarca

        Try
            Return clsLnProducto_marca.GetSingle(pIdMarca)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier marca_ and_ identifier propietario.
    ''' </summary>
    ''' <param name="pIdMarca">The p identifier marca.</param>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>clsBeProducto_Marca.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdMarca_And_IdPropietario(ByVal pIdMarca As Integer, ByVal pIdPropietario As Integer) As clsBeProducto_Marca Implements IServiceProductoMarca.Get_Single_By_IdMarca_And_IdPropietario

        Try
            Return clsLnProducto_marca.GetSingle(pIdMarca, pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Exists the specified p identifier marca.
    ''' </summary>
    ''' <param name="pIdMarca">The p identifier marca.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist(ByVal pIdMarca As Integer) As Boolean Implements IServiceProductoMarca.Exist

        Try

            Return clsLnProducto_marca.Exists(pIdMarca)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Exist_s the producto_ asociado.
    ''' </summary>
    ''' <param name="pIdMarca">The p identifier marca.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist_Producto_Asociado(ByVal pIdMarca As Integer) As Boolean Implements IServiceProductoMarca.Exist_Producto_Asociado

        Try

            Return clsLnProducto_marca.ExisteProductoLigado(pIdMarca)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
