' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-29-2018
' ***********************************************************************
' <copyright file="ServiceProductoBodega.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection

''' <summary>
''' Class ServiceProductoBodega.
''' </summary>
Public Class ServiceProductoBodega
    Implements IServiceProductoBodega

    ''' <summary>
    ''' Inserts the specified o be producto_bodega.
    ''' </summary>
    ''' <param name="oBeProducto_bodega">The o be producto_bodega.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    Public Function Insert(ByRef oBeProducto_bodega As clsBeProducto_bodega) As Integer Implements IServiceProductoBodega.Insert

        Try
            
           Return clsLnProducto_bodega.Insertar(oBeProducto_bodega)
       
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    ''' <summary>
    ''' Updates the specified o be producto_bodega.
    ''' </summary>
    ''' <param name="oBeProducto_bodega">The o be producto_bodega.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    Public Function Update(ByRef oBeProducto_bodega As clsBeProducto_bodega) As Integer Implements IServiceProductoBodega.Update

        Try
            
           Return clsLnProducto_bodega.Actualizar(oBeProducto_bodega)
       
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    ''' <summary>
    ''' Deletes the specified o be producto_bodega.
    ''' </summary>
    ''' <param name="oBeProducto_bodega">The o be producto_bodega.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    Public Function Delete(ByRef oBeProducto_bodega As clsBeProducto_bodega) As Integer Implements IServiceProductoBodega.Delete

        Try
            
           Return clsLnProducto_bodega.Eliminar(oBeProducto_bodega)
       
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier producto bodega.
    ''' </summary>
    ''' <param name="IdProductoBodega">The identifier producto bodega.</param>
    ''' <returns>clsBeProducto_bodega.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdProductoBodega(ByVal IdProductoBodega As Integer) As clsBeProducto_bodega Implements IServiceProductoBodega.Get_Single_By_IdProductoBodega

        Try
            Return clsLnProducto_bodega.GetSingle(IdProductoBodega)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ by_ identifier producto.
    ''' </summary>
    ''' <param name="pIdProducto">The p identifier producto.</param>
    ''' <returns>List(Of clsBeProducto_bodega).</returns>
    ''' <exception cref="FaultException"></exception>
    Public Function Get_All_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_bodega) Implements IServiceProductoBodega.Get_All_By_IdProducto

        Try

            Dim List As New List(Of clsBeProducto_bodega)
            List = clsLnProducto_bodega.Get_All_By_IdProducto(pIdProducto)

            Return List

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))    
        End Try

    End Function

End Class
