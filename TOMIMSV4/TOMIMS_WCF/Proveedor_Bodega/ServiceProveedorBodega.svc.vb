' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-30-2018
' ***********************************************************************
' <copyright file="ServiceProveedorBodega.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection

''' <summary>
''' Class ServiceProveedorBodega.
''' </summary>
Public Class ServiceProveedorBodega
    Implements IServiceProveedorBodega

    ''' <summary>
    ''' Update_s the multiple.
    ''' </summary>
    ''' <param name="pObjMD">The p object md.</param>
    ''' <param name="pListObjMDB">The p list object MDB.</param>
    ''' <returns><c>true</c> if Sucessfuly updated, <c>false</c> otherwise.</returns>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update_Multiple(ByVal pObjMD As clsBeProveedor, 
                                    ByVal pListObjMDB As List(Of clsBeProveedor_bodega)) As Boolean Implements IServiceProveedorBodega.ActualizaDatos
   
        Update_Multiple = False

        Try

            Return clsLnProveedor_bodega.ActualizarDatos(pObjMD,pListObjMDB)
          
        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ by_ identifier proveedor.
    ''' </summary>
    ''' <param name="pIdProveedor">The p identifier proveedor.</param>
    ''' <returns>List(Of clsBeProveedor_bodega).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdProveedor(ByVal pIdProveedor As Integer) As List(Of clsBeProveedor_bodega) Implements IServiceProveedorBodega.Get_All_By_IdProveedor

        Try
            Dim List As New List(Of clsBeProveedor_bodega)
            List = clsLnProveedor_bodega.Get_All_By_IdProveedor(pIdProveedor)
            Return List.ToList()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier proveedor bodega.
    ''' </summary>
    ''' <param name="pIdProveedorBodega">The p identifier proveedor bodega.</param>
    ''' <returns>clsBeProveedor_bodega.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdProveedorBodega(ByVal pIdProveedorBodega As Integer) As clsBeProveedor_bodega Implements IServiceProveedorBodega.Get_Single_By_IdProveedorBodega

        Try

            Dim pBeProvBod As New clsBeProveedor_bodega() With {.IdAsignacion = pIdProveedorBodega}
            clsLnProveedor_bodega.GetSingle(pBeProvBod)
            Return pBeProvBod

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

End Class
