' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-29-2018
' ***********************************************************************
' <copyright file="ServiceProductoPresentacion.svc.vb" company="DTSolutions S.A.">
'     Copyright © TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection

''' <summary>
''' Class ServiceProductoPresentacion.
''' </summary>
Public Class ServiceProductoPresentacion
    Implements IServiceProductoPresentacion


    ''' <summary>
    ''' Inserts the specified p be producto_ presentacion.
    ''' </summary>
    ''' <param name="pBeProducto_Presentacion">The p be producto_ presentacion.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Insert(ByRef pBeProducto_Presentacion As clsBeProducto_Presentacion) As Boolean Implements IServiceProductoPresentacion.Insert

        Try

            Return clsLnProducto_presentacion.Insertar(pBeProducto_Presentacion)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    ''' <summary>
    ''' Insert_s the multiple.
    ''' </summary>
    ''' <param name="pListObjPP">The p list object pp.</param>
    ''' <exception cref="FaultException"></exception>
    Public Sub Insert_Multiple(ByVal pListObjPP As List(Of clsBeProducto_Presentacion)) Implements IServiceProductoPresentacion.Insert_Multiple

        Try

            clsLnProducto_presentacion.Insert_Multiple(pListObjPP)

        Catch ex As Exception
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Sub


    ''' <summary>
    ''' Updates the specified be producto presentacion.
    ''' </summary>
    ''' <param name="BeProductoPresentacion">The be producto presentacion.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal BeProductoPresentacion As clsBeProducto_Presentacion) As Integer Implements IServiceProductoPresentacion.Update

        Try

            Return clsLnProducto_presentacion.Actualizar(BeProductoPresentacion)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function


    ''' <summary>
    ''' Disables the specified p object pp.
    ''' </summary>
    ''' <param name="pObjPP">The p object pp.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Disable(ByRef pObjPP As clsBeProducto_Presentacion) As Integer Implements IServiceProductoPresentacion.Disable

        Try

            pObjPP.Activo = False

            Return clsLnProducto_presentacion.Actualizar(pObjPP)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    ''' <summary>
    ''' Deletes the specified p identifier producto.
    ''' </summary>
    ''' <param name="pIdProducto">The p identifier producto.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Delete(ByVal pIdProducto As Integer) Implements IServiceProductoPresentacion.Delete

        Try
            clsLnProducto_presentacion.Delete(pIdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub


    ''' <summary>
    ''' Desactivars the specified p identifier presentacion.
    ''' </summary>
    ''' <param name="pIdPresentacion">The p identifier presentacion.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Desactivar(ByVal pIdPresentacion As Integer) Implements IServiceProductoPresentacion.Desactivar

        Try
            clsLnProducto_presentacion.Desactivar(pIdPresentacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub


    ''' <summary>
    ''' Exists the specified p identifier presentacion.
    ''' </summary>
    ''' <param name="pIdPresentacion">The p identifier presentacion.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist(ByVal pIdPresentacion As Integer) As Boolean Implements IServiceProductoPresentacion.Exist

        Try
            Return clsLnProducto_presentacion.Exists(pIdPresentacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    ''' <summary>
    ''' Update_s the minimo_ and_ maximo.
    ''' </summary>
    ''' <param name="pBeProducto_Presentacion">The p be producto_ presentacion.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Update_Minimo_And_Maximo(ByVal pBeProducto_Presentacion As clsBeProducto_Presentacion) Implements IServiceProductoPresentacion.Update_Minimo_And_Maximo

        Try
            clsLnProducto_presentacion.UpdateMM(pBeProducto_Presentacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub


    ''' <summary>
    ''' Exist_s the stock_ by_ identifier producto_ and_ identifier presentacion.
    ''' </summary>
    ''' <param name="pIdProducto">The p identifier producto.</param>
    ''' <param name="pIdPresentacion">The p identifier presentacion.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist_Stock_By_IdProducto_And_IdPresentacion(ByVal pIdProducto As Integer, ByVal pIdPresentacion As Integer) As Boolean Implements IServiceProductoPresentacion.Exist_Stock_By_IdProducto_And_IdPresentacion

        Try
            Return clsLnProducto_presentacion.Existe_Stock(pIdProducto, pIdPresentacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    ''' <summary>
    ''' Existe_s the presentacion_ by_ codigo_ barra.
    ''' </summary>
    ''' <param name="pCodigoBarra">The p codigo barra.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Existe_Presentacion_By_Codigo_Barra(ByVal pCodigoBarra As String) As Boolean Implements IServiceProductoPresentacion.Existe_Presentacion_By_Codigo_Barra

        Try
            Return clsLnProducto_presentacion.Existe_Presentacion_By_Codigo_Barra(pCodigoBarra)
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
    ''' <returns>List(Of clsBeProducto_Presentacion).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Filtro(ByVal pActivo As Boolean) As List(Of clsBeProducto_Presentacion) Implements IServiceProductoPresentacion.Get_All_Filtro

        Try
            Return clsLnProducto_presentacion.Get_All_Filtro(pActivo).ToList
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
    ''' <returns>List(Of clsBeProducto_Presentacion).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdProducto(ByVal pIdProducto As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_Presentacion) Implements IServiceProductoPresentacion.Get_All_By_IdProducto

        Try

            Return clsLnProducto_presentacion.Get_All_Presentaciones_By_IdProducto(pIdProducto, pActivo)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function


    ''' <summary>
    ''' Get_s the single_ by_ identifier presentacion.
    ''' </summary>
    ''' <param name="pIdPresentacion">The p identifier presentacion.</param>
    ''' <returns>clsBeProducto_Presentacion.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdPresentacion(ByVal pIdPresentacion As Integer) As clsBeProducto_Presentacion Implements IServiceProductoPresentacion.Get_Single_By_IdPresentacion

        Try
            Return clsLnProducto_presentacion.GetSingle(pIdPresentacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    ''' <summary>
    ''' Get_s the factor_ by_ identifier presentacion.
    ''' </summary>
    ''' <param name="pIdPresentacion">The p identifier presentacion.</param>
    ''' <returns>clsBeProducto_Presentacion.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Factor_By_IdPresentacion(ByVal pIdPresentacion As Integer) As clsBeProducto_Presentacion Implements IServiceProductoPresentacion.Get_Factor_By_IdPresentacion

        Try
            Return clsLnProducto_presentacion.Get_BeProductoPresentacion_By_IdPresentacion(pIdPresentacion)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try


    End Function


    ''' <summary>
    ''' Save_s the presentacion.
    ''' </summary>
    ''' <param name="pInsert">if set to <c>true</c> [p insert].</param>
    ''' <param name="pObjPP">The p object pp.</param>
    ''' <param name="pIdProducto">The p identifier producto.</param>
    ''' <param name="pIdProveedor">The p identifier proveedor.</param>
    ''' <param name="pCodigoBarraAnterior">The p codigo barra anterior.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Save_Presentacion(ByVal pInsert As Boolean,
                                       ByVal pObjPP As clsBeProducto_Presentacion,
                                       ByVal pIdProducto As Integer,
                                       ByVal pIdProveedor As Integer,
                                       ByVal pCodigoBarraAnterior As String) As Boolean Implements IServiceProductoPresentacion.Save_Presentacion

        Try
            Return clsLnProducto_presentacion.GuardaPresentacion(pInsert, pObjPP, pCodigoBarraAnterior)
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
    ''' <returns>List(Of clsBeProducto_Presentacion).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_Presentacion) Implements IServiceProductoPresentacion.Get_All_By_IdProducto

        Try
            Return clsLnProducto_presentacion.Get_All_By_IdProducto(pIdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    ''' <summary>
    ''' Get_s the peso_ by_ identifier presentacion.
    ''' </summary>
    ''' <param name="pIdPresentacion">The p identifier presentacion.</param>
    ''' <returns>System.Double.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Peso_By_IdPresentacion(ByVal pIdPresentacion As Integer) As Double Implements IServiceProductoPresentacion.Get_Peso_By_IdPresentacion

        Try
            Return clsLnProducto_presentacion.Get_Peso_By_IdPresentacion(pIdPresentacion)
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
    Public Function MaxID() As Integer Implements IServiceProductoPresentacion.MaxID

        Try
            Return clsLnProducto_presentacion.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    ''' <summary>
    ''' Get_s the all_ stock_ con_ presentacion_ by_ identifier producto.
    ''' </summary>
    ''' <param name="pIdProducto">The p identifier producto.</param>
    ''' <returns>List(Of clsBeProducto_Presentacion).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Stock_Con_Presentacion_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_Presentacion) Implements IServiceProductoPresentacion.Get_All_Stock_Con_Presentacion_By_IdProducto

        Try

            Return clsLnProducto_presentacion.Get_All_Stock_Con_Presentacion_By_IdProducto(pIdProducto).ToList()

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    ''' <summary>
    ''' Tiene_s the peso_ by_ identifier presentacion.
    ''' </summary>
    ''' <param name="pIdPresentacion">The p identifier presentacion.</param>
    ''' <returns>System.Double.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Tiene_Peso_By_IdPresentacion(ByVal pIdPresentacion As Integer) As Double Implements IServiceProductoPresentacion.Tiene_Peso_By_IdPresentacion

        Try

            Return clsLnProducto_presentacion.ExistePeso(pIdPresentacion)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function


    ''' <summary>
    ''' Get_s the factor_ by_ identifier producto_ and_ identifier presentacion.
    ''' </summary>
    ''' <param name="pIdProducto">The p identifier producto.</param>
    ''' <param name="pIdPresentacion">The p identifier presentacion.</param>
    ''' <returns>System.Double.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Factor_By_IdProducto_And_IdPresentacion(ByVal pIdProducto As Integer, 
                                                                ByVal pIdPresentacion As Integer) As Double Implements IServiceProductoPresentacion.Get_Factor_By_IdProducto_And_IdPresentacion

        Try

            Return clsLnProducto_presentacion.Get_Factor_By_IdProducto_And_IdPresentacion(pIdProducto, pIdPresentacion)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class