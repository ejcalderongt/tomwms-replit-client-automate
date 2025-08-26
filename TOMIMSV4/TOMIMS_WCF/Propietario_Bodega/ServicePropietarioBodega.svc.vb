' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-30-2018
' ***********************************************************************
' <copyright file="ServicePropietarioBodega.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection

''' <summary>
''' Class ServicePropietarioBodega.
''' </summary>
Public Class ServicePropietarioBodega
    Implements IServicePropietarioBodega

    ''' <summary>
    ''' Insert_s the multiple.
    ''' </summary>
    ''' <param name="pListObj">The p list object.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    Public Function Insert_Multiple(ByVal pListObj As List(Of clsBePropietario_destinatario)) As Boolean Implements IServicePropietarioBodega.Insert_Multiple
        Try

            Return clsLnPropietario_destinatario.GuardarDestinatario(pListObj)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Eliminars the destinatario.
    ''' </summary>
    ''' <param name="pListObjE">The p list object e.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    Public Function EliminarDestinatario(ByVal pListObjE As List(Of clsBePropietario_destinatario)) As Boolean Implements IServicePropietarioBodega.EliminarDestinatario

        Try

            Return clsLnPropietario_destinatario.eliminarDestinatario(pListObjE)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Actualizars the datos.
    ''' </summary>
    ''' <param name="pObjP">The p object p.</param>
    ''' <param name="pListObjP">The p list object p.</param>
    ''' <param name="pListDestinatarios">The p list destinatarios.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    Public Function ActualizarDatos(ByVal pObjP As clsBePropietarios,
                                    ByVal pListObjP As List(Of clsBePropietario_bodega),
                                    ByVal pListDestinatarios As List(Of clsBePropietario_destinatario)) As Boolean Implements IServicePropietarioBodega.ActualizarDatos

        Try

            Return clsLnPropietarios.ActualizarDatos(pObjP, pListObjP, pListDestinatarios)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Gets all by propietario.
    ''' </summary>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>List(Of clsBePropietario_bodega).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function GetAllByPropietario(ByVal pIdPropietario As Integer) As List(Of clsBePropietario_bodega) Implements IServicePropietarioBodega.GetAllByPropietario

        Try
            Dim List As New List(Of clsBePropietario_bodega)
            List = clsLnPropietario_bodega.Get_All_By_IdPropietario(pIdPropietario)
            Return List.ToList()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Gets all by identifier propietario.
    ''' </summary>
    ''' <param name="pIdPropietario">The p identifier propietario.</param>
    ''' <returns>List(Of clsBePropietario_destinatario).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Function GetAllByIdPropietario(ByVal pIdPropietario As Integer) As List(Of clsBePropietario_destinatario) Implements IServicePropietarioBodega.GetAllByIdPropietario

        Try
            'get all by mi huevo
            Return clsLnPropietario_destinatario.GetAllByIdPropietario(pIdPropietario).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Desactivars the destinatario.
    ''' </summary>
    ''' <param name="pObjDestinatario">The p object destinatario.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function DesactivarDestinatario(ByRef pObjDestinatario As clsBePropietario_destinatario) As Integer Implements IServicePropietarioBodega.DesactivarDestinatario

        Try
            Dim ObjLnDestinatario As New clsLnPropietario_destinatario
            pObjDestinatario.Activo = False
            Return ObjLnDestinatario.Actualizar(pObjDestinatario)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Deletes the destinatario.
    ''' </summary>
    ''' <param name="pIdDestinatario">The p identifier destinatario.</param>
    ''' <exception cref="FaultException"></exception>
    Public Sub DeleteDestinatario(ByVal pIdDestinatario As Integer) Implements IServicePropietarioBodega.DeleteDestinatario

        Try
            clsLnPropietario_destinatario.DeleteDestinatario(pIdDestinatario)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

End Class
