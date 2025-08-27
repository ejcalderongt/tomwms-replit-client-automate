' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-30-2018
' ***********************************************************************
' <copyright file="ServiceUnidadMedida.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection

''' <summary>
''' Class ServiceUnidadMedida.
''' </summary>
Public Class ServiceUnidadMedida
    Implements IServiceUnidadMedida

    ''' <summary>
    ''' Inserts the specified p object um.
    ''' </summary>
    ''' <param name="pObjUM">The p object um.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef pObjUM As clsBeUnidad_medida) As Integer Implements IServiceUnidadMedida.Insert

        Try
            Return clsLnUnidad_medida.Insertar(pObjUM)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Insert_s the multiple.
    ''' </summary>
    ''' <param name="pListObjUM">The p list object um.</param>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Insert_Multiple(ByVal pListObjUM As List(Of clsBeUnidad_medida)) Implements IServiceUnidadMedida.Insert_Multiple
        

        Try

           clsLnUnidad_medida.Insert_Multiple(pListObjUM)

        Catch ex As Exception            
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))        
        End Try

    End Sub

    ''' <summary>
    ''' Updates the specified p object um.
    ''' </summary>
    ''' <param name="pObjUM">The p object um.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal pObjUM As clsBeUnidad_medida) As Integer Implements IServiceUnidadMedida.Update

        Try
            Return clsLnUnidad_medida.Actualizar(pObjUM)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Disables the specified p object um.
    ''' </summary>
    ''' <param name="pObjUM">The p object um.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Disable(ByRef pObjUM As clsBeUnidad_medida) As Integer Implements IServiceUnidadMedida.Disable

        Try
            pObjUM.Activo = False
            Return clsLnUnidad_medida.Actualizar(pObjUM)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Deletes the specified p identifier unidad medida.
    ''' </summary>
    ''' <param name="pIdUnidadMedida">The p identifier unidad medida.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Delete(ByVal pIdUnidadMedida As Integer) Implements IServiceUnidadMedida.Delete

        Try
            clsLnUnidad_medida.Delete(pIdUnidadMedida)
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
    Public Sub Delete_By_IdPropietario(ByVal pIdPropietario As Integer) Implements IServiceUnidadMedida.Delete_By_IdPropietario

        Try
            clsLnUnidad_medida.DeleteByPropietario(pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    ''' <summary>
    ''' Maximums the identifier unidad medida.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function MaxIdUnidadMedida() As Integer Implements IServiceUnidadMedida.MAXIdUnidadMedida

        Try
            Return clsLnUnidad_medida.MaxID()
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
    ''' <returns>List(Of clsBeUnidad_medida).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Filtro(ByVal pActivo As Boolean, ByVal pIdPropietario As Integer) As List(Of clsBeUnidad_medida) Implements IServiceUnidadMedida.Get_All_Filtro

        Try
            Return clsLnUnidad_medida.Get_All_Filtro(pActivo, pIdPropietario).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Exists the specified p identifier unidad medida.
    ''' </summary>
    ''' <param name="pIdUnidadMedida">The p identifier unidad medida.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist(ByVal pIdUnidadMedida As Integer) As Boolean Implements IServiceUnidadMedida.Exist

        Try
            Return clsLnUnidad_medida.Exists(pIdUnidadMedida)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Existe_s the producto_ asociado.
    ''' </summary>
    ''' <param name="pIdUnidadMedida">The p identifier unidad medida.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Existe_Producto_Asociado(ByVal pIdUnidadMedida As Integer) As Boolean Implements IServiceUnidadMedida.Existe_Producto_Asociado

        Try
            Return clsLnUnidad_medida.ExisteProductoLigado(pIdUnidadMedida)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier unidad medida.
    ''' </summary>
    ''' <param name="pIdUnidadMedida">The p identifier unidad medida.</param>
    ''' <returns>clsBeUnidad_medida.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdUnidadMedida(ByVal pIdUnidadMedida As Integer) As clsBeUnidad_medida Implements IServiceUnidadMedida.Get_Single_By_IdUnidadMedida

        Try
            Return clsLnUnidad_medida.GetSingle(pIdUnidadMedida)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier unidad medida_ and_ identifier propietario.
    ''' </summary>
    ''' <param name="pIdUnidadMedida">The p identifier unidad medida.</param>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>clsBeUnidad_medida.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdUnidadMedida_And_IdPropietario(ByVal pIdUnidadMedida As Integer, ByVal pIdPropietario As Integer) As clsBeUnidad_medida Implements IServiceUnidadMedida.Get_Single_By_IdUnidadMedida_And_IdPropietario

        Try
            Return clsLnUnidad_medida.GetSingle(pIdUnidadMedida, pIdPropietario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

End Class
