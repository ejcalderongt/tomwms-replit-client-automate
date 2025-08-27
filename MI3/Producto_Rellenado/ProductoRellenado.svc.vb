' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-30-2018
' ***********************************************************************
' <copyright file="ProductoRellenado.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection
Imports System.ServiceModel

''' <summary>
''' Class ProductoRellenado.
''' </summary>
Public Class ProductoRellenado
    Implements IProductoRellenado

    ''' <summary>
    ''' Inserts the specified p object.
    ''' </summary>
    ''' <param name="pObj">The p object.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByVal pObj As clsBeProducto_rellenado) As Boolean Implements IProductoRellenado.Insert

        Try
            pObj.IdRellenado = clsLnProducto_rellenado.MaxID()
            Return clsLnProducto_rellenado.Insertar(pObj)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Updates the specified p object.
    ''' </summary>
    ''' <param name="pObj">The p object.</param>
    ''' <returns><c>true</c> if Sucessfuly updated, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal pObj As clsBeProducto_rellenado) As Boolean Implements IProductoRellenado.Update

        Try
            Return clsLnProducto_rellenado.Actualizar(pObj)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Insert_s the multiple.
    ''' </summary>
    ''' <param name="pListBeProducto_rellenado">The p list be producto_rellenado.</param>
    ''' <exception cref="FaultException"></exception>
    Public Sub Insert_Multiple(ByVal pListBeProducto_rellenado As List(Of clsBeProducto_rellenado)) Implements IProductoRellenado.Insert_Multiple


        Try

            clsLnProducto_rellenado.Insert_Multiple(pListBeProducto_rellenado)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Get_s the single_ by_ identifier rellenado.
    ''' </summary>
    ''' <param name="pIdRellenado">The p identifier rellenado.</param>
    ''' <returns>clsBeProducto_rellenado.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdRellenado(ByVal pIdRellenado As Integer) As clsBeProducto_rellenado Implements IProductoRellenado.Get_Single_By_IdRellenado

        Try
            Return clsLnProducto_rellenado.GetSingle(pIdRellenado)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Get_s the all_ by_ identifier presentacion.
    ''' </summary>
    ''' <param name="pIdPresentacion">The p identifier presentacion.</param>
    ''' <param name="pActivo">if set to <c>true</c> [p activo].</param>
    ''' <returns>List(Of clsBeProducto_rellenado).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdPresentacion(ByVal pIdPresentacion As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_rellenado) Implements IProductoRellenado.Get_All_By_IdPresentacion

        Try
            Return clsLnProducto_rellenado.GetAllByPresentacion(pIdPresentacion, pActivo).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Maximums the identifier.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function MaxID() As Integer Implements IProductoRellenado.MaxID

        Try
            Return clsLnProducto_rellenado.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Existe_s the configuracion.
    ''' </summary>
    ''' <param name="pIdPresentacion">The p identifier presentacion.</param>
    ''' <param name="pIdUbicacion">The p identifier ubicacion.</param>
    ''' <returns>System.Object.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Existe_Configuracion(ByVal pIdPresentacion As Integer,
                                         ByVal pIdUbicacion As Integer,
                                         ByVal pIdBodega As Integer) Implements IProductoRellenado.Existe_Configuracion

        Try

            Return clsLnProducto_rellenado.ExisteConfiguracion(pIdPresentacion, pIdUbicacion, pIdBodega)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function


    ''' <summary>
    ''' Disables the specified p identifier rellenado.
    ''' </summary>
    ''' <param name="pIdRellenado">The p identifier rellenado.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Disable(ByVal pIdRellenado As Integer) Implements IProductoRellenado.Disable

        Try

            clsLnProducto_rellenado.Desactivar(pIdRellenado)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

End Class
