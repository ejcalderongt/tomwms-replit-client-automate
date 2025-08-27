' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-29-2018
' ***********************************************************************
' <copyright file="ServiceProductoCodigoBarra.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection
Imports System.ServiceModel

''' <summary>
''' Class ServiceProductoCodigoBarra.
''' </summary>
Public Class ServiceProductoCodigoBarra
    Implements IServiceProductoCodigoBarra

    ''' <summary>
    ''' Inserts the specified be producto codigo barra.
    ''' </summary>
    ''' <param name="BeProductoCodigoBarra">The be producto codigo barra.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef BeProductoCodigoBarra As clsBeProducto_codigos_barra) As Boolean Implements IServiceProductoCodigoBarra.Insert

        Try
            Return clsLnProducto_codigos_barra.Insertar(BeProductoCodigoBarra)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Updates the specified p object pc.
    ''' </summary>
    ''' <param name="pObjPC">The p object pc.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal pObjPC As clsBeProducto_codigos_barra) As Integer Implements IServiceProductoCodigoBarra.Update

        Try
            Return clsLnProducto_codigos_barra.Actualizar(pObjPC)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Disables the specified p identifier producto.
    ''' </summary>
    ''' <param name="pIdProducto">The p identifier producto.</param>
    ''' <param name="pIdProveedor">The p identifier proveedor.</param>
    ''' <param name="pCodigoBarra">The p codigo barra.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Disable(ByVal pIdProducto As Integer,
                       ByVal pIdProveedor As Integer,
                       ByVal pCodigoBarra As String) Implements IServiceProductoCodigoBarra.Disable

        Try
            clsLnProducto_codigos_barra.Desactivar(pIdProducto, pIdProveedor, pCodigoBarra)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Sub

    ''' <summary>
    ''' Update_s the existing.
    ''' </summary>
    ''' <param name="pObjPC">The p object pc.</param>
    ''' <param name="pCodigoBarra">The p codigo barra.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update_Existing(ByVal pObjPC As clsBeProducto_codigos_barra,
                                        ByVal pCodigoBarra As String) As Integer Implements IServiceProductoCodigoBarra.Update_Existing

        Try
            Return clsLnProducto_codigos_barra.Actualizar(pObjPC, pCodigoBarra)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Exist_s the codigo_ barra.
    ''' </summary>
    ''' <param name="pIdProducto">The p identifier producto.</param>
    ''' <param name="pIdProveedor">The p identifier proveedor.</param>
    ''' <param name="pCodigoBarra">The p codigo barra.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist_Codigo_Barra(ByVal pIdProducto As Integer,
                                       ByVal pIdProveedor As Integer,
                                       ByVal pCodigoBarra As String) As Boolean Implements IServiceProductoCodigoBarra.Exist_Codigo_Barra

        Try
            Return clsLnProducto_codigos_barra.ExisteCodigoBarra(pIdProducto, pIdProveedor, pCodigoBarra)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Deletes the specified p object pc.
    ''' </summary>
    ''' <param name="pObjPC">The p object pc.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    Public Function Delete(ByRef pObjPC As clsBeProducto_codigos_barra) As Integer Implements IServiceProductoCodigoBarra.Delete

        Try
            pObjPC.Activo = False
            Return clsLnProducto_codigos_barra.Actualizar(pObjPC)
        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ filtro.
    ''' </summary>
    ''' <param name="pActivo">if set to <c>true</c> [p activo].</param>
    ''' <returns>List(Of clsBeProducto_codigos_barra).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeProducto_codigos_barra) Implements IServiceProductoCodigoBarra.Get_All_Filtro

        Try
            Return clsLnProducto_codigos_barra.Get_All_Filtro(pActivo).ToList
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
    ''' <param name="pActivo">if set to <c>true</c> [p activo].</param>
    ''' <returns>List(Of clsBeProducto_codigos_barra).</returns>
    ''' <exception cref="FaultException"></exception>
    Public Function Get_All_By_IdProducto(ByVal pIdProducto As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_codigos_barra) Implements IServiceProductoCodigoBarra.Get_All_By_IdProducto

        Get_All_By_IdProducto = Nothing

        Try

            Dim lCodigosBarra As New List(Of clsBeProducto_codigos_barra)

            lCodigosBarra = clsLnProducto_codigos_barra.Get_All_By_IdProducto(pIdProducto, pActivo)

            If Not lCodigosBarra Is Nothing Then Get_All_By_IdProducto = lCodigosBarra

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier producto.
    ''' </summary>
    ''' <param name="pIdProducto">The p identifier producto.</param>
    ''' <param name="pIdProveedor">The p identifier proveedor.</param>
    ''' <param name="pCodigoBarra">The p codigo barra.</param>
    ''' <returns>clsBeProducto_codigos_barra.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdProducto(ByVal pIdProducto As Integer,
                              ByVal pIdProveedor As Integer,
                              ByVal pCodigoBarra As String) As clsBeProducto_codigos_barra Implements IServiceProductoCodigoBarra.Get_Single_By_IdProducto

        Try
            Return clsLnProducto_codigos_barra.GetSingle(pIdProducto, pIdProveedor, pCodigoBarra)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Get the Max IdProductoCodigoBarra from table producto_codigos_barra
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function MaxID() As Integer Implements IServiceProductoCodigoBarra.MaxID

        Try
            Return clsLnProducto_codigos_barra.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
