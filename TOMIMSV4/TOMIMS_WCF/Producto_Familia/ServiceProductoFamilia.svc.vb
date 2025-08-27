' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-29-2018
' ***********************************************************************
' <copyright file="ServiceProductoFamilia.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection

''' <summary>
''' Class ServiceProductoFamilia.
''' </summary>
Public Class ServiceProductoFamilia
    Implements IServiceProductoFamilia

    ''' <summary>
    ''' Inserts the specified be producto_familia.
    ''' </summary>
    ''' <param name="BeProducto_familia">The be producto_familia.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef BeProducto_familia As clsBeProducto_familia) As Integer Implements IServiceProductoFamilia.Insert

        Try
            Return clsLnProducto_familia.Insertar(BeProducto_familia)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Insert_s the multiple.
    ''' </summary>
    ''' <param name="ListBeProducto_familia">The list be producto_familia.</param>
    ''' <exception cref="FaultException"></exception>
    Public Sub Insert_Multiple(ByVal ListBeProducto_familia As List(Of clsBeProducto_familia)) Implements IServiceProductoFamilia.Insert_Multiple

        Try

            clsLnProducto_familia.GuardarTransaccion(ListBeProducto_familia)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Updates the specified be producto_familia.
    ''' </summary>
    ''' <param name="BeProducto_familia">The be producto_familia.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal BeProducto_familia As clsBeProducto_familia) As Integer Implements IServiceProductoFamilia.Update

        Try

            Return clsLnProducto_familia.Actualizar(BeProducto_familia)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Disables the specified be producto_familia.
    ''' </summary>
    ''' <param name="BeProducto_familia">The be producto_familia.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Disable(ByRef BeProducto_familia As clsBeProducto_familia) As Integer Implements IServiceProductoFamilia.Disable

        Try
            BeProducto_familia.Activo = False
            Return clsLnProducto_familia.Actualizar(BeProducto_familia)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Deletes the specified p identifier familia.
    ''' </summary>
    ''' <param name="pIdFamilia">The p identifier familia.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Delete(ByVal pIdFamilia As Integer) Implements IServiceProductoFamilia.Delete

        Try
            clsLnProducto_familia.Delete(pIdFamilia)
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
    Public Sub Delete_By_IdPropietario(ByVal pIdPropietario As Integer) Implements IServiceProductoFamilia.Delete_By_IdPropietario

        Try
            clsLnProducto_familia.DeleteByPropietario(pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    ''' <summary>
    ''' Max_s the identifier producto_ familia.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Max_IdProducto_Familia() As Integer Implements IServiceProductoFamilia.Max_IdProducto_Familia

        Try
            Return clsLnProducto_familia.Max_IdProducto_Familia()
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
    ''' <param name="pFiltro">The p filtro.</param>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>List(Of clsBeProducto_familia).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Filtro(ByVal pActivo As Boolean, 
                                 ByVal pFiltro As String, 
                                 ByVal pIdPropietario As Integer) As List(Of clsBeProducto_familia) Implements IServiceProductoFamilia.Get_All_Filtro

        Try

            Return clsLnProducto_familia.Get_All_Filtro(pActivo, pFiltro, pIdPropietario).ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Exists the specified p identifier familia.
    ''' </summary>
    ''' <param name="pIdFamilia">The p identifier familia.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    Public Function Exist(ByVal pIdFamilia As Integer) As Boolean Implements IServiceProductoFamilia.Exist

        Try

            Return clsLnProducto_familia.Exists(pIdFamilia)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Exist_s the producto_ asociado.
    ''' </summary>
    ''' <param name="pIdFamilia">The p identifier familia.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    Public Function Exist_Producto_Asociado(ByVal pIdFamilia As Integer) As Boolean Implements IServiceProductoFamilia.Exist_Producto_Asociado

        Try

            Return clsLnProducto_familia.ExisteProductoLigado(pIdFamilia)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier producto_ familia.
    ''' </summary>
    ''' <param name="IdProductoFamilia">The identifier producto familia.</param>
    ''' <returns>clsBeProducto_familia.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdProducto_Familia(ByVal IdProductoFamilia As Integer) As clsBeProducto_familia Implements IServiceProductoFamilia.Get_Single_By_IdProducto_Familia

        Try

            Return clsLnProducto_familia.GetSingle(IdProductoFamilia)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier producto familia_ and_ identifier propietario.
    ''' </summary>
    ''' <param name="pIdFamilia">The p identifier familia.</param>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>clsBeProducto_familia.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdProductoFamilia_And_IdPropietario(ByVal pIdFamilia As Integer, 
                                                                      ByVal pIdPropietario As Integer) As clsBeProducto_familia Implements IServiceProductoFamilia.Get_Single_By_IdProductoFamilia_And_IdPropietario

        Try
            Return clsLnProducto_familia.GetSingle(pIdFamilia, pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class