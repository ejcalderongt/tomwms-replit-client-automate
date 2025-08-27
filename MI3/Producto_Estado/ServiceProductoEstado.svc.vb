' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-29-2018
' ***********************************************************************
' <copyright file="ServiceProductoEstado.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection
Imports System.ServiceModel

''' <summary>
''' Class ServiceProductoEstado.
''' </summary>
Public Class ServiceProductoEstado
    Implements IServiceProductoEstado

    ''' <summary>
    ''' Inserts the specified be producto estado.
    ''' </summary>
    ''' <param name="BeProductoEstado">The be producto estado.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    Public Function Insert(ByRef BeProductoEstado As clsBeProducto_estado) As Integer Implements IServiceProductoEstado.Insert

        Try
            Return clsLnProducto_estado.Insertar(BeProductoEstado)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Insert_s the multiple.
    ''' </summary>
    ''' <param name="pListObjPE">The p list object pe.</param>
    ''' <exception cref="FaultException"></exception>
    Public Sub Insert_Multiple(ByVal pListObjPE As List(Of clsBeProducto_estado)) Implements IServiceProductoEstado.Insert_Multiple

        Try

            clsLnProducto_estado.GuardarTransaccion(pListObjPE)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Insert_s the producto_ estado_ with_ ubic.
    ''' </summary>
    ''' <param name="pObjPE">The p object pe.</param>
    ''' <param name="pListDet">The p list det.</param>
    ''' <exception cref="FaultException"></exception>
    Public Sub Insert_Producto_Estado_With_Ubic(ByVal pObjPE As clsBeProducto_estado,
                                     ByVal pListDet As List(Of clsBeProducto_estado_ubic)) Implements IServiceProductoEstado.Insert_Producto_Estado_With_Ubic

        Try

            clsLnProducto_estado.Insert_Producto_Estado_With_Ubic(pObjPE, pListDet)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Updates the specified be producto estado.
    ''' </summary>
    ''' <param name="BeProductoEstado">The be producto estado.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal BeProductoEstado As clsBeProducto_estado) As Integer Implements IServiceProductoEstado.Update

        Try
            Return clsLnProducto_estado.Actualizar(BeProductoEstado)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Disables the specified be producto estado.
    ''' </summary>
    ''' <param name="BeProductoEstado">The be producto estado.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Disable(ByRef BeProductoEstado As clsBeProducto_estado) As Integer Implements IServiceProductoEstado.Disable

        Try
            BeProductoEstado.Activo = False
            Return clsLnProducto_estado.Actualizar(BeProductoEstado)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Delete_s the by_ identifier propietario.
    ''' </summary>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Delete_By_IdPropietario(ByVal pIdPropietario As Integer) Implements IServiceProductoEstado.Delete_By_IdPropietario

        Try
            clsLnProducto_estado.Eliminar_By_IdPropietario(pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Sub

    ''' <summary>
    ''' Deletes the specified be producto estado.
    ''' </summary>
    ''' <param name="BeProductoEstado">The be producto estado.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Delete(ByVal BeProductoEstado As clsBeProducto_estado) Implements IServiceProductoEstado.Delete

        Try
            clsLnProducto_estado.Eliminar(BeProductoEstado)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Sub

    ''' <summary>
    ''' Max_s the identifier producto_ estado.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Max_IdProducto_Estado() As Integer Implements IServiceProductoEstado.Max_IdProducto_Estado

        Try
            Return clsLnProducto_estado.MaxID()
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
    ''' <returns>List(Of clsBeProducto_estado).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeProducto_estado) Implements IServiceProductoEstado.Get_All_Filtro

        Try
            Return clsLnProducto_estado.Get_All_Filtro(pActivo).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ by_ identifier propietario.
    ''' </summary>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>List(Of clsBeProducto_estado).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdPropietario(ByVal pIdPropietario As Integer) As List(Of clsBeProducto_estado) Implements IServiceProductoEstado.Get_All_By_IdPropietario

        Try
            Return clsLnProducto_estado.GetAllByPropietario(pIdPropietario).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ by_ identifier propietario bodega.
    ''' </summary>
    ''' <param name="pIdPropietarioBodega">The p identifier propietario bodega.</param>
    ''' <returns>List(Of clsBeProducto_estado).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdPropietarioBodega(ByVal pIdPropietarioBodega As Integer) As List(Of clsBeProducto_estado) Implements IServiceProductoEstado.Get_All_By_IdPropietarioBodega

        Try

            Return Nothing
            'Return clsLnProducto_estado.GetAllByPropietarioBodega(pIdPropietarioBodega).ToList

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Exist_s the by_ identifier estado.
    ''' </summary>
    ''' <param name="pIdEstado">The p identifier estado.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist_By_IdEstado(ByVal pIdEstado As Integer) As Boolean Implements IServiceProductoEstado.Exist_By_IdEstado

        Try

            Return clsLnProducto_estado.Exists(pIdEstado)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier estado.
    ''' </summary>
    ''' <param name="pIdEstado">The p identifier estado.</param>
    ''' <returns>clsBeProducto_estado.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdEstado(ByVal pIdEstado As Integer) As clsBeProducto_estado Implements IServiceProductoEstado.Get_Single_By_IdEstado

        Try
            Return clsLnProducto_estado.GetSingle(pIdEstado)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ estados_ stock_ by_ identifier producto.
    ''' </summary>
    ''' <param name="pIdProducto">The p identifier producto.</param>
    ''' <returns>List(Of clsBeProducto_estado).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Estados_Stock_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_estado) Implements IServiceProductoEstado.Get_All_Estados_Stock_By_IdProducto

        Try

            Return clsLnProducto_estado.Get_All_Stock_Con_Estado_By_IdProducto(pIdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function


    ''' <summary>
    ''' Get_s the all_ by_ identifier estado_ and_ estatus.
    ''' </summary>
    ''' <param name="pIdEstado">The p identifier estado.</param>
    ''' <param name="Activo">if set to <c>true</c> [activo].</param>
    ''' <returns>List(Of clsBeProducto_estado_ubic).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdEstado_And_Estatus(ByVal pIdEstado As Integer, ByVal Activo As Boolean) As List(Of clsBeProducto_estado_ubic) Implements IServiceProductoEstado.Get_All_By_IdEstado_And_Estatus
        Try
            Return clsLnProducto_estado_ubic.Get_All_By_IdEstado(pIdEstado, Activo).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Delete_s the by_ identifier estado_ ubic.
    ''' </summary>
    ''' <param name="pIdEstadoUbic">The p identifier estado ubic.</param>
    ''' <exception cref="FaultException"></exception>
    Public Sub Delete_By_IdEstado_Ubic(ByVal pIdEstadoUbic As Integer) Implements IServiceProductoEstado.Delete_By_IdEstado_Ubic

        Try
            clsLnProducto_estado.Eliminar_Producto_Estado_By_IdProductoEstadUbic(pIdEstadoUbic)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

End Class