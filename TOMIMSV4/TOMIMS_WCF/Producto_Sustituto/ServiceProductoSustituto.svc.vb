' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-30-2018
' ***********************************************************************
' <copyright file="ServiceProductoSustituto.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection

''' <summary>
''' Class ServiceProductoSustituto.
''' </summary>
Public Class ServiceProductoSustituto
    Implements IServiceProductoSustituto

    ''' <summary>
    ''' Inserts the specified p object.
    ''' </summary>
    ''' <param name="pObj">The p object.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByVal pObj As clsBeProducto_sustituto) As Boolean Implements IServiceProductoSustituto.Insert

        Try            
            Return clsLnProducto_sustituto.Insertar(pObj)
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
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal pObj As clsBeProducto_sustituto) As Boolean Implements IServiceProductoSustituto.Update

        Try            
            Return clsLnProducto_sustituto.Actualizar(pObj)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Deletes the specified p identifier producto sustituto.
    ''' </summary>
    ''' <param name="pIdProductoSustituto">The p identifier producto sustituto.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Delete(ByVal pIdProductoSustituto As Integer) Implements IServiceProductoSustituto.Delete

        Try
            clsLnProducto_sustituto.Delete(pIdProductoSustituto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Sub

    ''' <summary>
    ''' Insert_s the multiple.
    ''' </summary>
    ''' <param name="pListBeProducto_sustituto">The p list be producto_sustituto.</param>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Insert_Multiple(ByVal pListBeProducto_sustituto As List(Of clsBeProducto_sustituto)) Implements IServiceProductoSustituto.Insert_Multiple

        Try

            clsLnProducto_sustituto.GuardarTransaccion(pListBeProducto_sustituto)

        Catch ex As Exception            
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))        
        End Try

    End Sub


    ''' <summary>
    ''' Exists the specified p identifier producto original.
    ''' </summary>
    ''' <param name="pIdProductoOriginal">The p identifier producto original.</param>
    ''' <param name="pIdProductoPresentacionOriginal">The p identifier producto presentacion original.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist(ByVal pIdProductoOriginal As Integer, ByVal pIdProductoPresentacionOriginal As Integer) As Boolean Implements IServiceProductoSustituto.Exist

        Try
            Return clsLnProducto_sustituto.Exists(pIdProductoOriginal, pIdProductoPresentacionOriginal)
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
    ''' <returns>List(Of clsBeProducto_sustituto).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdProducto(ByVal pIdProducto As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_sustituto) Implements IServiceProductoSustituto.Get_All_By_IdProducto

        Try
            Return clsLnProducto_sustituto.GetAllByProducto(pIdProducto, pActivo).ToList()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier producto sustituto.
    ''' </summary>
    ''' <param name="pIdProductoSustituto">The p identifier producto sustituto.</param>
    ''' <returns>clsBeProducto_sustituto.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdProductoSustituto(ByVal pIdProductoSustituto As Integer) As clsBeProducto_sustituto Implements IServiceProductoSustituto.Get_Single_By_IdProductoSustituto

        Try
            Return clsLnProducto_sustituto.GetSingle(pIdProductoSustituto)
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
    Public Function MaxID() As Integer Implements IServiceProductoSustituto.MaxID

        Try
            Return clsLnProducto_sustituto.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function

End Class
