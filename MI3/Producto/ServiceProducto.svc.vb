' ***********************************************************************
' Assembly         : TOMIMS_WCF
' Author           : ejcalderon
' Created          : 08-14-2017
'
' Last Modified By : ejcalderon
' Last Modified On : 03-29-2018
' ***********************************************************************
' <copyright file="ServiceProducto.svc.vb" company="DTSolutions S.A.">
'     Copyright TEAM OS 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection
Imports System.ServiceModel

''' <summary>
''' Class ServiceProducto.
''' </summary>
Public Class ServiceProducto
    Implements IServiceProducto

    ''' <summary>
    ''' Inserts the specified be producto.
    ''' </summary>
    ''' <param name="BeProducto">The be producto.</param>
    ''' <param name="ListBeProductoParametro">The list be producto parametro.</param>
    ''' <param name="ListBeProductoCodigosBarra">The list be producto codigos barra.</param>
    ''' <param name="ListBeProductoPresentacion">The list be producto presentacion.</param>
    ''' <param name="ListBeProductosSustitutos">The list be productos sustitutos.</param>
    ''' <param name="ListBeProductoRellenado">The list be producto rellenado.</param>
    ''' <param name="ListBePresentacionTarima">The list be presentacion tarima.</param>
    ''' <param name="ListBeProductoBodega">The list be producto bodega.</param>
    ''' <param name="ListBeConversionesPresentacion">The list be conversiones presentacion.</param>
    ''' <exception cref="FaultException"></exception>
    Public Sub Insert(ByVal BeProducto As clsBeProducto,
                       ByVal ListBeProductoParametro As List(Of clsBeProducto_parametros),
                       ByVal ListBeProductoCodigosBarra As List(Of clsBeProducto_codigos_barra),
                       ByVal ListBeProductoPresentacion As List(Of clsBeProducto_Presentacion),
                       ByVal ListBeProductosSustitutos As List(Of clsBeProducto_sustituto),
                       ByVal ListBeProductoRellenado As List(Of clsBeProducto_rellenado),
                       ByVal ListBePresentacionTarima As List(Of clsBeProducto_presentacion_tarima),
                       ByVal ListBeProductoBodega As List(Of clsBeProducto_bodega),
                       ByVal ListBeConversionesPresentacion As List(Of clsBeProducto_presentaciones_conversiones)) Implements IServiceProducto.Insert

        Try

            clsLnProducto.Guardar(BeProducto,
                                  ListBeProductoParametro,
                                  ListBeProductoCodigosBarra,
                                  ListBeProductoPresentacion,
                                  ListBeProductosSustitutos,
                                  ListBeProductoRellenado,
                                  ListBePresentacionTarima,
                                  ListBeProductoBodega,
                                  ListBeConversionesPresentacion,
                                  Nothing,
                                  Nothing)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    Public Sub Insert_Single(ByVal BeProducto As clsBeProducto) Implements IServiceProducto.Insert_Single

        Try

            clsLnProducto.InsertarFromInterface(BeProducto)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Disables the specified be producto.
    ''' </summary>
    ''' <param name="BeProducto">The be producto.</param>
    ''' <exception cref="FaultException"></exception>
    Public Sub Disable(ByVal BeProducto As clsBeProducto) Implements IServiceProducto.Disable

        Try

            clsLnProducto.Desactivar(BeProducto)

        Catch ex As Exception
            Throw New FaultException(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Updates the specified be producto.
    ''' </summary>
    ''' <param name="BeProducto">The be producto.</param>
    ''' <returns><c>true</c> if sucessfuly update the product, <c>false</c> error may be ocurred.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Update(ByVal BeProducto As clsBeProducto) As Boolean Implements IServiceProducto.Update

        Try
            Return clsLnProducto.Actualizar(BeProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Deletes the specified identifier producto.
    ''' </summary>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Sub Delete(ByVal IdProducto As Integer) Implements IServiceProducto.Delete

        Try
            clsLnProducto.Delete(IdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    ''' <summary>
    ''' Maximums the identifier.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function MaxID() As Integer Implements IServiceProducto.MaxID

        Try
            Return clsLnProducto.MaxID()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Gets the all products.
    ''' </summary>
    ''' <param name="Activo">if set to <c>true</c> [activo] get only active products.</param>
    ''' <returns>List(Of clsBeProducto).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Producto(ByVal Activo As Boolean) As List(Of clsBeProducto) Implements IServiceProducto.Get_All_Producto

        Try
            Return clsLnProducto.Get_All_Producto(Activo).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Get_s the all_ by_ identifier propietario.
    ''' </summary>
    ''' <param name="ActivoDefault">if set to <c>true</c> [activo default].</param>
    ''' <param name="Activo">if set to <c>true</c> [activo].</param>
    ''' <param name="IdBodega">The identifier bodega.</param>
    ''' <param name="IdPropietario">The identifier propietario.</param>
    ''' <returns>List(Of clsBeProducto).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdPropietario(ByVal ActivoDefault As Boolean,
                                             ByVal Activo As Boolean,
                                             ByVal IdBodega As Integer,
                                             ByVal IdPropietario As Integer) As List(Of clsBeProducto) Implements IServiceProducto.Get_All_By_IdPropietario

        Try
            Return clsLnProducto.Get_All_By_Propietario(ActivoDefault, Activo, IdBodega, IdPropietario).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ by_ identifier propietario bodega.
    ''' </summary>
    ''' <param name="ActivoDefault">if set to <c>true</c> [activo default].</param>
    ''' <param name="Activo">if set to <c>true</c> [activo].</param>
    ''' <param name="IdBodega">The identifier bodega.</param>
    ''' <param name="IdPropietarioBodega">The identifier propietario bodega.</param>
    ''' <returns>List(Of clsBeProducto).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_By_IdPropietarioBodega(ByVal ActivoDefault As Boolean,
                                                   ByVal Activo As Boolean,
                                                   ByVal IdBodega As Integer,
                                                   ByVal IdPropietarioBodega As Integer) As List(Of clsBeProducto) Implements IServiceProducto.Get_All_By_IdPropietarioBodega

        Try
            Return clsLnProducto.Get_All_By_PropietarioBodega(ActivoDefault, Activo, IdBodega, IdPropietarioBodega).ToList
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Gets the single instance of product.
    ''' </summary>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <returns>clsBeProducto.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdProducto(ByVal IdProducto As Integer) As clsBeProducto Implements IServiceProducto.Get_Single_By_IdProducto

        Try
            Return clsLnProducto.Get_Single_By_IdProducto(IdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ identifier producto bodega.
    ''' </summary>
    ''' <param name="IdProductoBodega">The identifier producto bodega.</param>
    ''' <returns>clsBeProducto.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_IdProductoBodega(ByVal IdProductoBodega As Integer) As clsBeProducto Implements IServiceProducto.Get_Single_By_IdProductoBodega

        Try
            Return clsLnProducto.Get_Single_BeProducto_By_IdProductoBodega(IdProductoBodega)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Exist_s the by_ codigo_ barra.
    ''' </summary>
    ''' <param name="CodigoBarra">The codigo barra.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist_By_Codigo_Barra(ByVal CodigoBarra As String) As Boolean Implements IServiceProducto.Exist_By_Codigo_Barra

        Try
            Return clsLnProducto.Existe_Codigo(CodigoBarra)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Exist_s the producto_ by_ bodega.
    ''' </summary>
    ''' <param name="IdBodega">The identifier bodega.</param>
    ''' <param name="IdPropietarioBodega">The identifier propietario bodega.</param>
    ''' <param name="CodigoProducto">The codigo producto.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist_Producto_By_Bodega(ByVal IdBodega As Integer,
                                             ByVal IdPropietarioBodega As Integer,
                                             ByVal CodigoProducto As String) As Boolean Implements IServiceProducto.Exist_Producto_By_Bodega

        Try
            Return clsLnProducto.Existe_ProductoBodega_By_IdBodega_And_IdPropietarioBodega(IdBodega, IdPropietarioBodega, CodigoProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Exist_s the by_ identifier producto.
    ''' </summary>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist_By_IdProducto(ByVal IdProducto As Integer) As Boolean Implements IServiceProducto.Exist_By_IdProducto

        Try
            Return clsLnProducto.Exists(IdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Exist_s the stock_ by_ identifier producto.
    ''' </summary>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Exist_Stock_By_IdProducto(ByVal IdProducto As Integer) As Boolean Implements IServiceProducto.Exist_Stock_By_IdProducto

        Try
            Return clsLnProducto.Existe_Stock_By_IdProducto(IdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Obtiene el IdProductoBodega asociado a un producto que pertenece a una bodega, a través del código(s)
    ''' </summary>
    ''' <param name="Codigo">código de Producto, código de Barra.</param>
    ''' <returns>IdProductoBodega de la tabla producto_bodega</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    ''' <remarks>ejcalderon_20160516</remarks>
    Public Function Get_IdProductoBodega_By_Codigo(ByVal Codigo As String) As Integer Implements IServiceProducto.Get_IdProductoBodega_By_Codigo

        Try
            Return clsLnProducto.Get_IdProductoBodega_By_Codigo(Codigo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the identifier producto_ by_ codigo.
    ''' </summary>
    ''' <param name="Codigo">The codigo.</param>
    ''' <returns>System.Int32.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_IdProducto_By_Codigo(ByVal Codigo As String) As Integer Implements IServiceProducto.Get_IdProducto_By_Codigo

        Try
            Return clsLnProducto.Get_IdProducto_By_Codigo(Codigo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the single_ by_ codigo.
    ''' </summary>
    ''' <param name="Codigo">The codigo.</param>
    ''' <returns>clsBeProducto.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Single_By_Codigo(ByVal Codigo As String) As clsBeProducto Implements IServiceProducto.Get_Single_By_Codigo

        Try
            Return clsLnProducto.Get_BeProducto_By_Codigo(Codigo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Busca un producto por su código, pero ademas el IdProductoBodega por bodega.
    ''' </summary>
    ''' <param name="Codigo">código a traves del cuál se busca el producto</param>
    ''' <param name="IdBodega">IdBodega por el cual se busca el producto</param>
    ''' <returns>devuelve un objeto del tipo clsBeProducto</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    ''' <remarks>ejcalderon_20160519</remarks>
    Public Function Get_By_Codigo_And_Bodega(ByVal Codigo As String,
                                             ByVal IdBodega As Integer) As clsBeProducto Implements IServiceProducto.GetByCodigoAndBodega

        Try
            Return clsLnProducto.Get_BeProducto_By_Codigo(Codigo, IdBodega)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Funcion para reporte de minimos y maximos devuelve un Datatable
    ''' </summary>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <returns>DataTable.</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Rep_Minimos_Y_Maximos_By_IdProducto(ByVal IdProducto As Integer) As DataTable Implements IServiceProducto.Get_Rep_Minimos_Y_Maximos_By_IdProducto
        Try
            Return clsLnProducto.Get_Minimos_y_Maximos_By_IdProducto(IdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try
    End Function

    ''' <summary>
    ''' Obtiene todos los productos que estan por debajo del minimo
    ''' </summary>
    ''' <returns>List(Of clsBeStock.Revision).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_Rep_Stock_Minimo() As List(Of clsBeReabasto) Implements IServiceProducto.Get_Rep_Stock_Minimo

        Try
            Return clsLnProducto.Get_Reabastecimientos_Productos()
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ parametros_ by_ identifier producto.
    ''' </summary>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <returns>List(Of clsBeProducto_parametros).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Parametros_By_IdProducto(ByVal IdProducto As Integer) As List(Of clsBeProducto_parametros) Implements IServiceProducto.Get_All_Parametros_By_IdProducto

        Try
            Return clsLnProducto.Get_All_Parametro_By_IdProducto(IdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ codigos_ barra_ by_ identifier producto.
    ''' </summary>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <returns>List(Of clsBeProducto_codigos_barra).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Codigos_Barra_By_IdProducto(ByVal IdProducto As Integer) As List(Of clsBeProducto_codigos_barra) Implements IServiceProducto.Get_All_Codigos_Barra_By_IdProducto

        Try
            Return clsLnProducto.Get_All_Codigos_Barra_By_IdProducto(IdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ presentaciones_ by_ identifier producto.
    ''' </summary>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <returns>List(Of clsBeProducto_presentacion).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Presentaciones_By_IdProducto(ByVal IdProducto As Integer, ByVal pIdBodega As Integer) As List(Of clsBeProducto_Presentacion) Implements IServiceProducto.Get_All_Presentaciones_By_IdProducto

        Try
            Return clsLnProducto.Get_All_Presentacion_By_IdProducto(IdProducto, pIdBodega)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ productos_ sustitutos_ by_ identifier producto.
    ''' </summary>
    ''' <param name="IdProductoOriginal">The identifier producto original.</param>
    ''' <returns>List(Of clsBeProducto_sustituto).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Productos_Sustitutos_By_IdProducto(ByVal IdProductoOriginal As Integer) As List(Of clsBeProducto_sustituto) Implements IServiceProducto.Get_All_Productos_Sustitutos_By_IdProducto

        Try
            Return clsLnProducto.Get_All_Producto_Sustituto_By_IdProductoOriginal(IdProductoOriginal)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ rellenado_ producto_ by_ identifier producto.
    ''' </summary>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <returns>List(Of clsBeProducto_rellenado).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Rellenado_Producto_By_IdProducto(ByVal IdProducto As Integer) As List(Of clsBeProducto_rellenado) Implements IServiceProducto.Get_All_Rellenado_Producto_By_IdProducto

        Try
            Return clsLnProducto_rellenado.Get_All_By_IdProducto(IdProducto, True)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ presentaciones_ tarima_ by_ identifier producto.
    ''' </summary>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <returns>List(Of clsBeProducto_presentacion_tarima).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Presentaciones_Tarima_By_IdProducto(ByVal IdProducto As Integer) As List(Of clsBeProducto_presentacion_tarima) Implements IServiceProducto.Get_All_Presentaciones_Tarima_By_IdProducto

        Try

            Return clsLnProducto_presentacion_tarima.Get_All_By_IdProducto(IdProducto)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Get_s the all_ bodegas_ by_ identifier producto.
    ''' </summary>
    ''' <param name="IdProducto">The identifier producto.</param>
    ''' <returns>List(Of clsBeProducto_bodega).</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Get_All_Bodegas_By_IdProducto(ByVal IdProducto As Integer) As List(Of clsBeProducto_bodega) Implements IServiceProducto.Get_All_Bodegas_By_IdProducto

        Try
            Return clsLnProducto.Get_All_Bodegas_By_IdProducto(IdProducto)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Existe  by_ code producto.
    ''' </summary>
    ''' <param name="Codigo">The code producto.</param>
    ''' <returns>clsBeProducto</returns>
    ''' <exception cref="FaultException"></exception>
    ''' <exception cref="System.Exception"></exception>
    Public Function Existe(ByVal Codigo As String) As clsBeProducto Implements IServiceProducto.Existe

        Try
            Return clsLnProducto.Existe(Codigo)
        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
