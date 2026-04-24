Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_ubicsug : Implements IDisposable

    ' Lista final de ubicaciones sugeridas
    Public Shared Property lUbicacionesSugeridas As New List(Of clsBeUbicacionSugerida)
    ' Lista de ubicaciones excluidas
    Public Shared Property lUbicacionesExcluidas As New List(Of clsBeUbicacionExcluida)

    ' Opciones
    Private Shared opc1Nivel, opc2Vert, opc3LoteUbic, opc4MasLleno, opc5RotAlta As Boolean

    ' Contiene el listado de ubicaciones a partir de la(s) regla(s) asociadas a la bodega.
    Public Shared Property lUbicacionesLista As New List(Of clsBeUbicaciones_por_regla)
    ' Contiene el listado de ubicaciones vacias a partir de la(s) regla(s) asociadas a la bodega.
    Public Shared Property lUbicacionesVacias As New List(Of clsBeUbicaciones_por_regla)
    ' Contiene el listado de ubicaciones filtrado despues de aplicar los filtros de prioridades por producto.
    Public Shared Property lUbicReglaTemp As New List(Of clsBeUbicaciones_por_regla)
    ' Contiene el listado de ubicaciones con el cálculo de las cantidades de producto que puede almacenar en cada ubicación 
    ' en base al volumen del producto y volumen de la ubicación (considerando si el producto tiene presentación o UMBas)
    Public Shared Property lUbicReglaTramo As New List(Of clsBeUbicaciones_por_regla)
    ' Obtiene el listado final de las ubicaciones sugeridas con el ordenamiento aplicado por las prioridades configuradas (por defecto) o por producto
    Public Shared Property lFinalUbicaciones As New List(Of clsBeUbicaciones_por_regla)
    ' Parámetro que define los valores de producto,se toma en cuenta:
    ' IdProducto, IdProductoBodega, se debe enviar el objeto despues de hacer un getsingle de la clase clsLnProducto.
    Public Shared Property pBeProducto As New clsBeProducto
    ' Parámetro que define la presentación a ubicar del producto, si se utilizará la UMBas se debe inicializar
    ' el IdPresentacion en 0, de lo contrario enviar el objeto despues del GetSingle de la clase clsLnProducto_Presentacion
    ' de este objeto se considera el valor EsPallet, para aplicar filtros de ubicación.
    Public Shared Property pBePresentacion As New clsBeProducto_Presentacion
    ' Parámetro que define el estado del producto que será ubicado, se debe enviar el objeto BeEstado despues del GetSingle
    ' de la clase clsLnProducto_Estado.
    Public Shared Property pBeEstado As New clsBeProducto_estado
    ' Se debe inicializar el IdBodega para determinar con que reglas se trabajará.
    Public Shared Property pIdBodega As Integer = 0
    ' Parámetro de Lote utilizado para la ubicación sugerida si diferente de "" se buscan localidades que contenga en mismo lote.
    Public Shared Property pLote As String = ""
    ' Lista de tramos utilizada para determinar los tramos que contienen el producto que se desea ubicar.
    Public Shared Property lTramos As New List(Of clsBeBodega_tramo)
    ' Lista de todos los tramos disponobles despues de aplicar los filtros.
    Public Shared Property lTramosConUbicVacias As New List(Of clsBeBodega_tramo)
    ' Lista de todos los tramos de bodega.
    Public Shared Property lTramosBodega As New List(Of clsBeBodega_tramo)

    Public Shared Property BeReglaUbicPrioProd As New clsBeRegla_ubic_prio_enc

    Private dvt As New DataTable

    Private Shared _PorcentajeExtra As Double = 1.0
    'Define un porcentaje de incremento en el volúmen del producto
    Public Shared ReadOnly Property PorcentajeExtra As Double
        Get
            Return _PorcentajeExtra
        End Get
    End Property

    Private Shared CantidadAUbicar, CantidadAUbicarExtra, AuxCantAUbicInicial As Double
    Private Shared dCantidadAUbicar, VolumenProducto As Double

    Private Shared ubicStock As Integer = 0
    Private Shared completo As Boolean = False

    Public Sub New()
        MyBase.New()
    End Sub

#Region " Destructor "

    Dim Disposed As Boolean = False

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Disposed Then Return

        If disposing Then

            Try
                lUbicacionesSugeridas.Clear()
                lUbicacionesLista.Clear()
                lUbicReglaTemp.Clear()
                lUbicReglaTramo.Clear()
                lFinalUbicaciones.Clear()
                lUbicacionesVacias.Clear()
                lUbicacionesExcluidas.Clear()

                lTramos.Clear()
                lTramosConUbicVacias.Clear()
                lTramosBodega.Clear()

            Catch ex As Exception
            End Try

        End If

        Disposed = True

    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

#End Region

#Region " Public methods "

    Public Shared Function Get_Ubicaciones_Sugeridas(ByVal Cantidad As Double,
                                                     ByVal UbicacionStock As Integer) As Boolean

        Get_Ubicaciones_Sugeridas = False


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            ubicStock = UbicacionStock

            Try

                lUbicacionesSugeridas.Clear()
                lUbicacionesLista.Clear()
                lUbicReglaTemp.Clear()
                lUbicReglaTramo.Clear()
                lFinalUbicaciones.Clear()
                lUbicacionesVacias.Clear()
                lUbicacionesExcluidas.Clear()

                lTramos.Clear()
                lTramosConUbicVacias.Clear()
                lTramosBodega.Clear()
                completo = False

            Catch ex As Exception
            End Try

            If (pBePresentacion IsNot Nothing) Then
                pBePresentacion.IdPresentacion = pBePresentacion.IdPresentacion
            Else
                pBePresentacion = New clsBeProducto_Presentacion
                pBePresentacion.IdPresentacion = 0
            End If

            CantidadAUbicar = Cantidad
            CantidadAUbicarExtra = CantidadAUbicar * PorcentajeExtra
            AuxCantAUbicInicial = CantidadAUbicar

            If (pBePresentacion IsNot Nothing) AndAlso (pBePresentacion.IdPresentacion <> 0) Then
                VolumenProducto = pBePresentacion.Volumen()
            Else
                VolumenProducto = pBeProducto.Volumen()
            End If

            If Get_Reglas_Prioridad(lConnection, lTransaction) Then
                If Get_Lista_Ubicaciones_Filtradas(lConnection, lTransaction) Then
                    If Aplica_Prioridades(lConnection, lTransaction) Then
                        Get_Resultados_Ubic_Sugerida()
                        Get_Ubicaciones_Sugeridas = True
                    End If
                End If
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lTramos Is Nothing Then lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

#End Region

#Region " Main "

    Private Shared Function Get_Lista_Ubicaciones_Filtradas(ByRef lConnection As SqlConnection,
                                                            ByRef lTransaction As SqlTransaction) As Boolean


        Get_Lista_Ubicaciones_Filtradas = False

        Try

            Dim pFiltros As New clsBeUbicaciones_por_regla()
            pFiltros.IdBodega = pIdBodega
            pFiltros.IdPropietario = pBeProducto.Propietario.IdPropietario
            pFiltros.IdTipoProducto = pBeProducto.TipoProducto.IdTipoProducto
            pFiltros.IdTipoRotacion = pBeProducto.IdTipoRotacion
            pFiltros.IdIndiceRotacion = pBeProducto.IdIndiceRotacion
            pFiltros.IdPresentacion = pBePresentacion.IdPresentacion
            pFiltros.Acepta_pallet = pBePresentacion.EsPallet

            If Not pBeEstado Is Nothing Then pFiltros.IdEstado = pBeEstado.IdEstado

            If Not opc1Nivel Then pFiltros.Nivel = 1

            Dim lReglas As New DataTable
            lReglas = clsLnRegla_ubic_enc.Get_All_DT()

            '#EJC20240816: Validar que exista al menos una regla para validar el resto del proceso.
            If Not lReglas Is Nothing Then

                If lReglas.Rows.Count > 0 Then

                    If Not clsLnRegla_ubic_det_prop.Aplicar_Regla_Por_Propietario(pIdBodega,
                                                                                  pBeProducto.Propietario.IdPropietario,
                                                                                 lConnection,
                                                                                 lTransaction) Then
                        pFiltros.IdPropietario = 0
                    End If

                    '#EJC202408192206: Filtrar siempre por tipo de producto.
                    'If Not clsLnRegla_ubic_det_tp.Aplicar_Regla_Por_TipoProducto(pIdBodega,
                    '                                                             pFiltros.IdTipoProducto,
                    '                                                             lConnection,
                    '                                                             lTransaction) AndAlso pFiltros.IdTipoProducto <> 0 Then
                    '    pFiltros.IdTipoProducto = 0
                    'End If

                    If Not clsLnRegla_ubic_det_tr.Aplicar_Regla_Por_TipoRotacion(pIdBodega,
                                                                                 pFiltros.IdTipoRotacion,
                                                                                 lConnection,
                                                                                 lTransaction) AndAlso pFiltros.IdTipoRotacion <> 0 Then
                        pFiltros.IdTipoRotacion = 0
                    End If

                    '#EJC20171024_0750PM:Corrección de parámetros que se pasan a las funciones de regla.

                    If Not clsLnRegla_ubic_det_ir.Aplicar_Regla_Por_IndiceRotacion(pIdBodega,
                                                                                   pFiltros.IdIndiceRotacion,
                                                                                   lConnection,
                                                                                   lTransaction) AndAlso pFiltros.IdIndiceRotacion <> 0 Then
                        pFiltros.IdIndiceRotacion = 0
                    End If

                    If Not clsLnRegla_ubic_det_pp.Aplicar_Regla_Por_Presentacion(pIdBodega,
                                                                                 pFiltros.IdPresentacion,
                                                                                 lConnection,
                                                                                 lTransaction) AndAlso pFiltros.IdPresentacion <> 0 Then
                        pFiltros.IdPresentacion = 0
                    End If

                    If Not clsLnRegla_ubic_det_pe.Aplicar_Regla_Por_Estado(pIdBodega,
                                                                           pFiltros.IdEstado,
                                                                           lConnection,
                                                                           lTransaction) AndAlso pFiltros.IdEstado <> 0 Then
                        pFiltros.IdEstado = 0
                    End If

                    lUbicacionesLista = clsLnUbicaciones_por_regla.Get_All_By_Filtros(pFiltros, lConnection, lTransaction)

                    Eliminar_Excluidos()

                    lTramosConUbicVacias.Clear()

                    ' todos los tramos diponibles por regla con ubicaciones vacias
                    lTramosConUbicVacias = clsLnUbicaciones_por_regla.Get_All_Tramos_By_Filtros(pFiltros,
                                                                                                lConnection,
                                                                                                lTransaction)

                    lTramosBodega = clsLnUbicaciones_por_regla.Get_All_Tramos_By_IdBodega(pIdBodega,
                                                                                          lConnection,
                                                                                          lTransaction)

                    lUbicacionesVacias = clsLnUbicaciones_por_regla.Get_All_Vacias_By_Filtros(pFiltros,
                                                                                              lConnection,
                                                                                              lTransaction)

                    Get_Lista_Ubicaciones_Filtradas = True

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reglas_Prioridad(ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction) As Boolean

        Get_Reglas_Prioridad = False

        Try

            opc1Nivel = True
            opc2Vert = True
            opc3LoteUbic = True
            opc4MasLleno = True
            opc5RotAlta = True

            clsLnRegla_ubic_prio_enc.Get_Single_With_Details_By_IdProducto(pBeProducto.IdProducto, BeReglaUbicPrioProd, lConnection, lTransaction)

            If Not BeReglaUbicPrioProd Is Nothing Then

                For Each ReglaProd As clsBeRegla_ubic_prio_det In BeReglaUbicPrioProd.lReglaUbicPrioDet

                    Select Case ReglaProd.IdReglaUbicPrioParam
                        Case 6
                            opc1Nivel = ReglaProd.Activo <> 0
                        Case 7
                            opc2Vert = ReglaProd.Activo <> 0
                        Case 8
                            opc3LoteUbic = ReglaProd.Activo <> 0
                        Case 9
                            opc4MasLleno = ReglaProd.Activo <> 0
                        Case 10
                            opc5RotAlta = ReglaProd.Activo <> 0
                        Case Else
                            Exit Select
                    End Select

                Next

            End If

            If Not opc1Nivel Then opc2Vert = False

            Return True

        Catch ex As Exception
            MessageBox.Show(String.Format("{0} : {1}", MethodBase.GetCurrentMethod().Name, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Private Shared Function Aplica_Prioridades(ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction) As Boolean

        Aplica_Prioridades = False

        Try

            Dim ListaOrdenadaPrioProd As New List(Of clsBeRegla_ubic_prio_det)

            If BeReglaUbicPrioProd IsNot Nothing Then
                ListaOrdenadaPrioProd = BeReglaUbicPrioProd.lReglaUbicPrioDet.OrderBy(Function(Y) Y.Orden).ToList()
            End If

            If Not ListaOrdenadaPrioProd Is Nothing Then

                For Each ReglaProd As clsBeRegla_ubic_prio_det In ListaOrdenadaPrioProd

                    Select Case ReglaProd.IdReglaUbicPrioParam

                        Case 1

                            Aplica_Regla_Por_Lote(ReglaProd.IdReglaUbicPrioParam,
                                                  lConnection,
                                                  lTransaction)

                        Case 2

                            Aplica_Regla_Por_Producto(ReglaProd.IdReglaUbicPrioParam,
                                                      lConnection,
                                                      lTransaction)

                        Case 3

                            If pBePresentacion.EsPallet Then
                                Aplica_Regla_Por_Predeterminado_Pallet(ReglaProd.IdReglaUbicPrioParam,
                                                                       lConnection,
                                                                       lTransaction)
                            Else
                                Aplica_Regla_Por_Predeterminado(ReglaProd.IdReglaUbicPrioParam, lConnection, lTransaction)
                            End If

                        Case 4

                            If pBePresentacion.EsPallet Then
                                Aplica_Regla_Por_Vacio_Pallet(ReglaProd.IdReglaUbicPrioParam)
                            Else
                                Aplica_Regla_Por_Vacio(ReglaProd.IdReglaUbicPrioParam)
                            End If

                        Case Else
                            Exit Select

                    End Select

                    If completo Then Exit For

                Next

            End If

            Aplica_Prioridades = True

        Catch ex As Exception
            MessageBox.Show(String.Format("{0} : {1}", MethodBase.GetCurrentMethod().Name, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Private Shared Sub Aplica_Cantidades(ByVal lList As List(Of clsBeUbicaciones_por_regla))

        Try

            For Each Ubic As clsBeUbicaciones_por_regla In lList

                If AuxCantAUbicInicial < Ubic.UBS_Cantidad_Maxima_Por_Ubicacion Then
                    dCantidadAUbicar = AuxCantAUbicInicial
                Else
                    dCantidadAUbicar = Ubic.UBS_Cantidad_Maxima_Por_Ubicacion
                End If

                AuxCantAUbicInicial = AuxCantAUbicInicial - dCantidadAUbicar : If AuxCantAUbicInicial < 0 Then AuxCantAUbicInicial = 0
                CantidadAUbicarExtra = CantidadAUbicarExtra - dCantidadAUbicar

                Ubic.UBS_Cantidad_A_Ubicar = dCantidadAUbicar
                Ubic.UBS_Cantidad_Balance = AuxCantAUbicInicial

                lFinalUbicaciones.Add(Ubic)

                '#EJC20191231: Parametrizar la cantidad de ubicaciones, att Erik del pasado.
                If (CantidadAUbicarExtra <= 0) Or ((VolumenProducto = 0) And (lFinalUbicaciones.Count >= 10)) Then
                    completo = True : Exit For
                End If

            Next

        Catch ex As Exception
            MessageBox.Show(String.Format("{0} : {1}", MethodBase.GetCurrentMethod().Name, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Shared Sub Distribuir_Cantidades_En_Tramo(ByVal IdTramo As Integer)

        If completo Then Return

        Try

            lUbicReglaTramo.Clear()

            For Each Ubic As clsBeUbicaciones_por_regla In lUbicReglaTemp.FindAll(Function(x) x.IdTramo = IdTramo)
                lUbicReglaTramo.Add(Ubic)
            Next

            Dim vListaUbicOrd As New List(Of clsBeUbicaciones_por_regla)

            If opc4MasLleno Then

                If opc2Vert Then
                    vListaUbicOrd = lUbicReglaTramo.OrderBy(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                Else
                    vListaUbicOrd = lUbicReglaTramo.OrderBy(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x).ToList()
                End If

            Else

                If opc2Vert Then
                    vListaUbicOrd = lUbicReglaTramo.OrderByDescending(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                Else
                    vListaUbicOrd = lUbicReglaTramo.OrderByDescending(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x).ToList()
                End If

            End If

            Aplica_Cantidades(vListaUbicOrd)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Shared Function Get_Resultados_Ubic_Sugerida() As List(Of clsBeUbicacionSugerida)

        Get_Resultados_Ubic_Sugerida = Nothing

        Dim SUbic As clsBeUbicacionSugerida
        Dim idx As Integer

        lUbicacionesSugeridas.Clear()

        Try

            For Each Ubic As clsBeUbicaciones_por_regla In lFinalUbicaciones

                SUbic = New clsBeUbicacionSugerida
                SUbic.IdUbicacion = Ubic.IdUbicacion
                SUbic.Descripcion = Ubic.Descripcion
                SUbic.IdTramo = Ubic.IdTramo
                SUbic.Indice_x = Ubic.Indice_x
                SUbic.Nivel = Ubic.Nivel
                SUbic.Acepta_pallet = Ubic.Acepta_pallet
                SUbic.Cant_Max = Ubic.UBS_Cantidad_Maxima_Por_Ubicacion
                SUbic.Cant_Ubicar = Ubic.UBS_Cantidad_A_Ubicar
                SUbic.Cant_Balance = Ubic.UBS_Cantidad_Balance
                SUbic.IdPrior = Ubic.IdReglaUbicacionEnc


                idx = lTramosBodega.FindIndex(Function(x) x.IdTramo = Ubic.IdTramo)
                If idx <> -1 Then
                    SUbic.Tramo = lTramosBodega.Item(idx).Descripcion
                Else
                    SUbic.Tramo = " --- " & Ubic.IdTramo
                End If

                lUbicacionesSugeridas.Add(SUbic)

            Next

            Return lUbicacionesSugeridas

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Sub Eliminar_Excluidos()

        Try

            Dim ii, idubic As Integer

            '#EJC20170909 Agregado para evitar error de ínidce
            'Al intentar remover item y la lista tiene un count =0
            Dim idx As Integer = -1

            For Each Ubic As clsBeUbicaciones_por_regla In lUbicacionesLista
                idx = lUbicacionesLista.FindIndex(Function(x) x.IdUbicacion = ubicStock)
                If idx <> -1 Then Exit For
            Next

            If idx <> -1 Then lUbicacionesLista.RemoveAt(idx)

            For ii = 0 To lUbicacionesExcluidas.Count - 1
                idubic = lUbicacionesExcluidas.Item(ii).idUbicacion
                idx = lUbicacionesLista.FindIndex(Function(x) x.IdUbicacion = idubic)
                If idx <> -1 Then
                    'MsgBox("Elim " & idubic)
                    lUbicacionesLista.RemoveAt(idx)
                End If
            Next

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Ubicacion_Es_Valida(ByVal pIdProducto As Integer,
                                               ByVal pIdUbicacion As Integer,
                                               ByVal pIdBodega As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Ubicacion_Es_Valida = True

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vIdTipoProducto As Integer = clsLnProducto.Get_IdTipoProducto_By_IdProducto(pIdProducto, lConnection, lTransaction)

            If vIdTipoProducto <> 0 Then
                '#EJC20210225: Existe alguna regla definida para este tipo de producto?
                If clsLnRegla_ubic_det_tp.Aplicar_Regla_Por_TipoProducto(pIdBodega,
                                                                         vIdTipoProducto,
                                                                         lConnection,
                                                                         lTransaction) Then
                    '#EJC20210225: Si existe, la ubicación donde estoy ubicando el producto está contenida dentro de las reglas definidas?
                    If Not clsLnUbicaciones_por_regla.Ubicacion_Valida_By_IdTipoProducto(vIdTipoProducto,
                                                                                         pIdUbicacion,
                                                                                         pIdBodega,
                                                                                         lConnection,
                                                                                         lTransaction) Then
                        Ubicacion_Es_Valida = False
                    End If
                Else
                    '#EJC20210225: No Existe regla definida para este tipo de producto, pero la ubicación donde lo estoy colocando admite este tipo de producto?
                    'Es decir, no debe existir una regla para esa ubicación, para que permita colocarlo allí... (creo)
                    If clsLnUbicaciones_por_regla.Ubicacion_Tiene_Regla_Asociada(pIdUbicacion,
                                                                                 pIdBodega,
                                                                                 lConnection,
                                                                                 lTransaction) Then
                        Ubicacion_Es_Valida = False
                    End If
                End If
            End If


        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

#End Region

#Region " Ubicacion - Lote "

    Private Shared Sub Aplica_Regla_Por_Lote(ByVal IdRegla As Integer,
                                             ByRef lConnection As SqlConnection,
                                             ByRef lTransaction As SqlTransaction)


        If completo Then Return
        If pLote = "" Then Return 'Si no tiene lote, no buscar ubicaciones con el mismo lote.
        If pBePresentacion.EsPallet Then Return 'si es pallet, no cabe en ninguna ubicación que ya contenga productos.

        Try

            Dim lStockPorUbicacion As New List(Of clsBeVW_stock_res)
            Dim lStockConLote As New List(Of clsBeVW_stock_res)
            Dim BeTramo As New clsBeBodega_tramo

            lUbicReglaTemp.Clear()

            lStockConLote = clsLnStock.Get_All_By_IdProductoBodega_And_Lote(pBeProducto.IdProductoBodega,
                                                                            pLote,
                                                                            pBeEstado.IdEstado,
                                                                            lConnection,
                                                                            lTransaction)

            Dim IdxUbicacion As Integer = -1
            Dim IdxTramo As Integer = -1
            Dim vVolumenOcupadoUbicacion As Double = 0
            Dim DiferenciaDisponiblePorUbicacion As Double = 0

            For Each UbicR As clsBeUbicaciones_por_regla In lUbicacionesLista

                IdxUbicacion = lStockConLote.FindIndex(Function(x) x.IdUbicacion = UbicR.IdUbicacion)

                If IdxUbicacion <> -1 Then

                    vVolumenOcupadoUbicacion = 0

                    lStockPorUbicacion = New List(Of clsBeVW_stock_res)
                    lStockPorUbicacion = clsLnStock.Get_All_By_IdUbicacion(UbicR.IdUbicacion,
                                                                           lConnection,
                                                                           lTransaction)

                    If lStockPorUbicacion.Count > 0 Then

                        For Each ProdInStock As clsBeVW_stock_res In lStockPorUbicacion

                            If ProdInStock.IdPresentacion <> 0 Then
                                vVolumenOcupadoUbicacion += ProdInStock.VolumenPresEnUbicacion()
                            Else
                                vVolumenOcupadoUbicacion += ProdInStock.VolumenUmBasEnUbicacion()
                            End If

                        Next

                    End If

                    DiferenciaDisponiblePorUbicacion = UbicR.VolumenUbicacion - vVolumenOcupadoUbicacion

                    If pBePresentacion.IdPresentacion <> 0 AndAlso pBePresentacion.Volumen <> 0 Then
                        UbicR.UBS_Cantidad_Maxima_Por_Ubicacion = Math.Truncate(DiferenciaDisponiblePorUbicacion / pBePresentacion.Volumen)
                    ElseIf pBeProducto.Volumen > 0 Then
                        UbicR.UBS_Cantidad_Maxima_Por_Ubicacion = Math.Truncate(DiferenciaDisponiblePorUbicacion / pBeProducto.Volumen)
                    Else
                        UbicR.UBS_Cantidad_Maxima_Por_Ubicacion = 0 'No se cuantos caben en la ubicación.
                    End If

                    UbicR.VolumenDisponible = DiferenciaDisponiblePorUbicacion
                    UbicR.UBS_IdReglaFiltro = IdRegla

                    lUbicReglaTemp.Add(UbicR)

                    BeTramo.IdTramo = UbicR.IdTramo
                    IdxTramo = lTramos.FindIndex(Function(x) x.IdTramo = UbicR.IdTramo)
                    If IdxTramo <> -1 Then lTramos.Add(BeTramo)

                End If

            Next

            ' Remover de la lista principal
            Dim IdxListaPrincipalUbic As Integer = -1

            For Each UbiAgregadaInList As clsBeUbicaciones_por_regla In lUbicReglaTemp
                IdxListaPrincipalUbic = lUbicacionesLista.FindIndex(Function(x) x.IdUbicacion = UbiAgregadaInList.IdUbicacion)
                If IdxListaPrincipalUbic <> -1 Then
                    lUbicacionesLista.RemoveAt(IdxListaPrincipalUbic)
                End If
            Next

            ' Ordenar la lista
            If (lUbicReglaTemp.Count > 0) Then

                If opc3LoteUbic Then

                    If opc4MasLleno Then

                        If opc2Vert Then
                            lUbicReglaTemp = lUbicReglaTemp.OrderBy(Function(x) x.UBS_Cantidad_Maxima_Por_Ubicacion).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                        Else
                            lUbicReglaTemp = lUbicReglaTemp.OrderBy(Function(x) x.UBS_Cantidad_Maxima_Por_Ubicacion).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x).ToList()
                        End If
                    Else

                        If opc2Vert Then
                            lUbicReglaTemp = lUbicReglaTemp.OrderByDescending(Function(x) x.UBS_Cantidad_Maxima_Por_Ubicacion).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                        Else
                            lUbicReglaTemp = lUbicReglaTemp.OrderByDescending(Function(x) x.UBS_Cantidad_Maxima_Por_Ubicacion).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x)
                        End If

                    End If

                    Aplica_Cantidades(lUbicReglaTemp)

                Else
                    Get_Lista_Tramos_Ordenados_Por_Lote(lConnection, lTransaction)
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Shared Sub Get_Lista_Tramos_Ordenados_Por_Lote(ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction)

        Dim vTramos As New List(Of clsBeBodega_tramo)
        Dim Idx As Integer = -1

        Try

            lTramos.Clear()

            Dim BeTramo As New clsBeBodega_tramo

            If (VolumenProducto > 0) Then

                vTramos = clsLnBodega_tramo.Get_All_Tramos_Con_Volumen_Ocupado(pBeProducto.IdProductoBodega,
                                                                               pLote,
                                                                               pBeEstado.IdEstado,
                                                                               lConnection,
                                                                               lTransaction)

                If Not vTramos Is Nothing Then

                    If opc4MasLleno Then

                        Dim Query =
                        (From c In vTramos
                         Order By c.VolumenUtilizado Ascending
                         Group c By c.IdTramo, c.VolumenUtilizado Into Cts = Group
                         Select IdTramo, VolumenUtilizado = Cts.Sum(Function(x) x.VolumenUtilizado)).ToList

                        For Each A As Object In Query

                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.VolumenUtilizado = A.VolumenUtilizado
                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).VolumenUtilizado += A.VolumenUtilizado
                            End If

                        Next

                    Else

                        Dim Query =
                       (From c In vTramos
                        Order By c.VolumenUtilizado Descending
                        Group c By c.IdTramo, c.VolumenUtilizado Into Cts = Group
                        Select IdTramo, VolumenUtilizado = Cts.Sum(Function(x) x.VolumenUtilizado)).ToList

                        For Each A As Object In Query
                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.VolumenUtilizado = A.VolumenUtilizado
                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).VolumenUtilizado += A.VolumenUtilizado
                            End If
                        Next

                    End If

                End If

            Else

                vTramos = clsLnBodega_tramo.Get_All_Tramos_Con_Cantidades_Ocupadas(pBeProducto.IdProductoBodega, pLote, pBeEstado.IdEstado, lConnection, lTransaction)

                If Not vTramos Is Nothing Then

                    If opc4MasLleno Then

                        Dim Query =
                        (From c In vTramos
                         Order By c.CantidadUtilizada Ascending
                         Group c By c.IdTramo, c.CantidadUtilizada Into Cts = Group
                         Select IdTramo, CantidadUtilizada = Cts.Sum(Function(x) x.CantidadUtilizada)).ToList


                        For Each A As Object In Query

                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.CantidadUtilizada = A.CantidadUtilizada

                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).CantidadUtilizada += A.CantidadUtilizad
                            End If


                        Next

                    Else

                        Dim Query =
                       (From c In vTramos
                        Order By c.CantidadUtilizada Descending
                        Group c By c.IdTramo, c.CantidadUtilizada Into Cts = Group
                        Select IdTramo, CantidadUtilizada = Cts.Sum(Function(x) x.CantidadUtilizada)).ToList

                        For Each A As Object In Query
                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.CantidadUtilizada = A.CantidadUtilizada
                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).CantidadUtilizada += A.CantidadUtilizad
                            End If
                        Next

                    End If

                End If


            End If

            For Each T As clsBeBodega_tramo In lTramos
                Distribuir_Cantidades_En_Tramo(T.IdTramo)
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

#End Region

#Region " Ubicacion - Producto "

    Private Shared Sub Aplica_Regla_Por_Producto(ByVal IdRegla As Integer,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction)

        Try

            Dim lStockPorUbicacion As New List(Of clsBeVW_stock_res)

            If pBePresentacion.EsPallet Then Return 'si es pallet, no cabe en ninguna ubicación que ya contenga productos.

            Dim lStockSinLote As New List(Of clsBeVW_stock_res)
            Dim BeTramo As New clsBeBodega_tramo

            lUbicReglaTemp.Clear()

            lStockSinLote = clsLnStock.Get_All_By_IdProductoBodega_And_Lote(pBeProducto.IdProductoBodega,
                                                                            "",
                                                                            pBeEstado.IdEstado,
                                                                            lConnection,
                                                                            lTransaction)

            Dim IdxUbicacion As Integer = -1
            Dim IdxTramo As Integer = -1
            Dim vVolumenOcupadoUbicacion As Double = 0
            Dim DiferenciaDisponiblePorUbicacion As Double = 0

            For Each UbicR As clsBeUbicaciones_por_regla In lUbicacionesLista

                IdxUbicacion = lStockSinLote.FindIndex(Function(x) x.IdUbicacion = UbicR.IdUbicacion)

                If IdxUbicacion <> -1 Then

                    vVolumenOcupadoUbicacion = 0

                    lStockPorUbicacion = New List(Of clsBeVW_stock_res)

                    lStockPorUbicacion = clsLnStock.Get_All_By_IdUbicacion(UbicR.IdUbicacion,
                                                                           lConnection,
                                                                           lTransaction)

                    If lStockPorUbicacion.Count > 0 Then

                        For Each ProdInStock As clsBeVW_stock_res In lStockPorUbicacion

                            If ProdInStock.IdPresentacion <> 0 Then
                                vVolumenOcupadoUbicacion += ProdInStock.VolumenPresEnUbicacion
                            Else
                                vVolumenOcupadoUbicacion += ProdInStock.VolumenUmBasEnUbicacion
                            End If

                        Next

                    End If

                    DiferenciaDisponiblePorUbicacion = UbicR.VolumenUbicacion - vVolumenOcupadoUbicacion

                    If pBePresentacion.IdPresentacion <> 0 AndAlso pBePresentacion.Volumen <> 0 Then
                        UbicR.UBS_Cantidad_Maxima_Por_Ubicacion = Math.Truncate(DiferenciaDisponiblePorUbicacion / pBePresentacion.Volumen)
                    ElseIf pBeProducto.Volumen > 0 Then
                        UbicR.UBS_Cantidad_Maxima_Por_Ubicacion = Math.Truncate(DiferenciaDisponiblePorUbicacion / pBeProducto.Volumen)
                    Else
                        UbicR.UBS_Cantidad_Maxima_Por_Ubicacion = 0 'No se cuantos caben en la ubicación.
                    End If

                    UbicR.VolumenDisponible = DiferenciaDisponiblePorUbicacion
                    UbicR.UBS_IdReglaFiltro = IdRegla
                    UbicR.IdReglaUbicacionEnc = 2

                    lUbicReglaTemp.Add(UbicR)

                    BeTramo.IdTramo = UbicR.IdTramo
                    IdxTramo = lTramos.FindIndex(Function(x) x.IdTramo = UbicR.IdTramo)
                    If IdxTramo <> -1 Then lTramos.Add(BeTramo)

                End If

            Next

            'Remover de la lista principal
            Dim IdxListaPrincipalUbic As Integer = -1

            For Each UbiAgregadaInList As clsBeUbicaciones_por_regla In lUbicReglaTemp
                IdxListaPrincipalUbic = lUbicacionesLista.FindIndex(Function(x) x.IdUbicacion = UbiAgregadaInList.IdUbicacion)
                If IdxListaPrincipalUbic <> -1 Then
                    lUbicacionesLista.RemoveAt(IdxListaPrincipalUbic)
                End If
            Next

            If lUbicReglaTemp.Count > 0 Then

                If opc3LoteUbic Then

                    If opc4MasLleno Then

                        If opc2Vert Then
                            lUbicReglaTemp = lUbicReglaTemp.OrderBy(Function(x) x.UBS_Cantidad_Maxima_Por_Ubicacion).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                        Else
                            lUbicReglaTemp = lUbicReglaTemp.OrderBy(Function(x) x.UBS_Cantidad_Maxima_Por_Ubicacion).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x).ToList()
                        End If
                    Else

                        If opc2Vert Then
                            lUbicReglaTemp = lUbicReglaTemp.OrderByDescending(Function(x) x.UBS_Cantidad_Maxima_Por_Ubicacion).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                        Else
                            lUbicReglaTemp = lUbicReglaTemp.OrderByDescending(Function(x) x.UBS_Cantidad_Maxima_Por_Ubicacion).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x)
                        End If

                    End If

                    Aplica_Cantidades(lUbicReglaTemp)
                Else
                    Get_Lista_Tramos_Ordenados_Sin_Lote(lConnection, lTransaction)
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Shared Sub Get_Lista_Tramos_Ordenados_Sin_Lote(ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction)

        Dim vTramos As New List(Of clsBeBodega_tramo)
        Dim Idx As Integer = -1

        Try

            lTramos.Clear()

            Dim BeTramo As New clsBeBodega_tramo

            If (VolumenProducto > 0) Then

                vTramos = clsLnBodega_tramo.Get_All_Tramos_Con_Volumen_Ocupado(pBeProducto.IdProductoBodega, "", pBeEstado.IdEstado, lConnection, lTransaction)

                If Not vTramos Is Nothing Then

                    If opc4MasLleno Then

                        Dim Query =
                        (From c In vTramos
                         Order By c.VolumenUtilizado Ascending
                         Group c By c.IdTramo, c.VolumenUtilizado Into Cts = Group
                         Select IdTramo, VolumenUtilizado = Cts.Sum(Function(x) x.VolumenUtilizado)).ToList

                        For Each A As Object In Query
                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.VolumenUtilizado = A.VolumenUtilizado
                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).VolumenUtilizado += A.VolumenUtilizado
                            End If
                        Next

                    Else

                        Dim Query =
                       (From c In vTramos
                        Order By c.VolumenUtilizado Descending
                        Group c By c.IdTramo, c.VolumenUtilizado Into Cts = Group
                        Select IdTramo, VolumenUtilizado = Cts.Sum(Function(x) x.VolumenUtilizado)).ToList

                        For Each A As Object In Query
                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.VolumenUtilizado = A.VolumenUtilizado
                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).VolumenUtilizado += A.VolumenUtilizado
                            End If
                        Next

                    End If

                End If

            Else

                vTramos = clsLnBodega_tramo.Get_All_Tramos_Con_Cantidades_Ocupadas(pBeProducto.IdProductoBodega, pLote, pBeEstado.IdEstado, lConnection, lTransaction)

                If Not vTramos Is Nothing Then

                    If opc4MasLleno Then

                        Dim Query =
                        (From c In vTramos
                         Order By c.CantidadUtilizada Descending
                         Group c By c.IdTramo, c.CantidadUtilizada Into Cts = Group
                         Select IdTramo, CantidadUtilizada = Cts.Sum(Function(x) x.CantidadUtilizada)).ToList


                        For Each A As Object In Query

                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.CantidadUtilizada = A.CantidadUtilizada

                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).CantidadUtilizada += A.CantidadUtilizad
                            End If


                        Next

                    Else

                        Dim Query =
                       (From c In vTramos
                        Order By c.CantidadUtilizada Ascending
                        Group c By c.IdTramo, c.CantidadUtilizada Into Cts = Group
                        Select IdTramo, CantidadUtilizada = Cts.Sum(Function(x) x.CantidadUtilizada)).ToList

                        For Each A As Object In Query
                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.CantidadUtilizada = A.CantidadUtilizada
                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).CantidadUtilizada += A.CantidadUtilizad
                            End If
                        Next

                    End If

                End If

            End If

            For Each T As clsBeBodega_tramo In lTramos
                Distribuir_Cantidades_En_Tramo(T.IdTramo)
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

#End Region

#Region " Ubicacion - Predeterminada "

    Private Shared Sub Aplica_Regla_Por_Predeterminado(ByVal IdRegla As Integer,
                                                ByRef lConnection As SqlConnection,
                                                ByRef lTransaction As SqlTransaction)

        Dim vTramos As New List(Of clsBeBodega_tramo)
        Dim volDispTramo, volDispUbic, volOcupUbic As Double
        Dim IdxListaPrincipalUbic As Integer

        If completo Then Return

        lUbicReglaTemp.Clear()

        Try

            lTramos.Clear()

            vTramos = clsLnBodega_tramo.Get_All_Tramos_By_ProductoPredeterminado(pBeProducto.IdProductoBodega, lConnection, lTransaction)

            For Each vTramo As clsBeBodega_tramo In vTramos

                volDispTramo = 0

                For Each Ubic As clsBeUbicaciones_por_regla In lUbicacionesLista.FindAll(Function(x) x.IdTramo = vTramo.IdTramo)

                    volOcupUbic = clsLnBodega_ubicacion.GetVolumenUbicacionByIdUbicacion(Ubic.IdUbicacion, Ubic.IdBodega)
                    volDispUbic = Ubic.VolumenUbicacion - volOcupUbic
                    Ubic.VolumenDisponible = volDispUbic

                    If pBePresentacion.IdPresentacion <> 0 AndAlso pBePresentacion.Volumen <> 0 Then
                        Ubic.UBS_Cantidad_Maxima_Por_Ubicacion = Math.Truncate(volDispUbic / pBePresentacion.Volumen)
                    ElseIf pBeProducto.Volumen > 0 Then
                        Ubic.UBS_Cantidad_Maxima_Por_Ubicacion = Math.Truncate(volDispUbic / pBeProducto.Volumen)
                    Else
                        Ubic.UBS_Cantidad_Maxima_Por_Ubicacion = 0 'No se cuantos caben en la ubicación.
                    End If

                    'Ubic.IdReglaUbicacionEnc = 3 - #EJC20170830 - 0959
                    Ubic.IdReglaUbicacionEnc = IdRegla


                    lUbicReglaTemp.Add(Ubic)

                    For Each UbiAgregadaInList As clsBeUbicaciones_por_regla In lUbicReglaTemp
                        IdxListaPrincipalUbic = lUbicacionesLista.FindIndex(Function(x) x.IdUbicacion = UbiAgregadaInList.IdUbicacion)
                        If IdxListaPrincipalUbic <> -1 Then lUbicacionesLista.RemoveAt(IdxListaPrincipalUbic)
                    Next

                    volDispTramo += volDispUbic

                Next

                vTramo.VolumenUtilizado = volDispTramo

            Next


            ' Ordenar la lista
            If (lUbicReglaTemp.Count > 0) Then

                If opc3LoteUbic Then

                    If opc4MasLleno Then

                        If opc2Vert Then
                            lUbicReglaTemp = lUbicReglaTemp.OrderBy(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                        Else
                            lUbicReglaTemp = lUbicReglaTemp.OrderBy(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x).ToList()
                        End If

                    Else

                        If opc2Vert Then
                            lUbicReglaTemp = lUbicReglaTemp.OrderByDescending(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                        Else
                            lUbicReglaTemp = lUbicReglaTemp.OrderByDescending(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x).ToList()
                        End If

                    End If

                    Aplica_Cantidades(lUbicReglaTemp)

                Else
                    Get_Lista_Tramos_Ordenados_Por_Predeterminado(lConnection, lTransaction)
                End If

            End If


        Catch ex As Exception
            Throw ex

        End Try

    End Sub

    Private Shared Sub Aplica_Regla_Por_Predeterminado_Pallet(ByVal IdRegla As Integer,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction)

        Dim vTramos As New List(Of clsBeBodega_tramo)
        Dim volDispTramo, volDispUbic, volOcupUbic As Double
        Dim IdxListaPrincipalUbic As Integer

        If completo Then Return

        lUbicReglaTemp.Clear()

        Try

            lTramos.Clear()

            vTramos = clsLnBodega_tramo.Get_All_Tramos_By_ProductoPredeterminado(pBeProducto.IdProductoBodega,
                                                                                 lConnection,
                                                                                 lTransaction)

            For Each vTramo As clsBeBodega_tramo In vTramos

                volDispTramo = 0

                For Each Ubic As clsBeUbicaciones_por_regla In lUbicacionesLista.FindAll(Function(x) x.IdTramo = vTramo.IdTramo)

                    IIf(clsLnStock.Get_Bandera_Usado_By_IdUbicacion(Ubic.IdUbicacion,
                                                                   lConnection,
                                                                   lTransaction),
                                                                   volOcupUbic = 1,
                                                                   volOcupUbic = 0)

                    volDispUbic = 1 - volOcupUbic
                    Ubic.VolumenDisponible = volDispUbic
                    Ubic.UBS_Cantidad_Maxima_Por_Ubicacion = 1

                    'Ubic.IdReglaUbicacionEnc = 3 - #EJC20170830 - 1001
                    Ubic.IdReglaUbicacionEnc = IdRegla


                    If volDispUbic > 0 Then lUbicReglaTemp.Add(Ubic)

                    For Each UbiAgregadaInList As clsBeUbicaciones_por_regla In lUbicReglaTemp
                        IdxListaPrincipalUbic = lUbicacionesLista.FindIndex(Function(x) x.IdUbicacion = UbiAgregadaInList.IdUbicacion)
                        If IdxListaPrincipalUbic <> -1 Then lUbicacionesLista.RemoveAt(IdxListaPrincipalUbic)
                    Next

                    volDispTramo += volDispUbic

                Next

                vTramo.VolumenUtilizado = volDispTramo

            Next

            ' Ordenar la lista
            If (lUbicReglaTemp.Count > 0) Then

                If opc3LoteUbic Then

                    If opc4MasLleno Then

                        If opc2Vert Then
                            lUbicReglaTemp = lUbicReglaTemp.OrderBy(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                        Else
                            lUbicReglaTemp = lUbicReglaTemp.OrderBy(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x).ToList()
                        End If

                    Else

                        If opc2Vert Then
                            lUbicReglaTemp = lUbicReglaTemp.OrderByDescending(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                        Else
                            lUbicReglaTemp = lUbicReglaTemp.OrderByDescending(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x).ToList()
                        End If

                    End If

                    Aplica_Cantidades(lUbicReglaTemp)

                Else
                    Get_Lista_Tramos_Ordenados_Por_Predeterminado(lConnection, lTransaction)
                End If

            End If


        Catch ex As Exception
            Throw ex

        End Try

    End Sub

    Private Shared Sub Get_Lista_Tramos_Ordenados_Por_Predeterminado(ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction)

        Dim vTramos As New List(Of clsBeBodega_tramo)
        Dim Idx As Integer = -1

        Try

            lTramos.Clear()

            Dim BeTramo As New clsBeBodega_tramo

            If (VolumenProducto > 0) Then

                'vTramos = clsLnBodega_tramo.GetAllTramosConVolumenOcupado(pProducto.IdProductoBodega, pLote, pEstado.IdEstado)
                vTramos = clsLnBodega_tramo.Get_All_Tramos_By_ProductoPredeterminado(pBeProducto.IdProductoBodega, lConnection, lTransaction)

                If Not vTramos Is Nothing Then

                    If opc4MasLleno Then

                        Dim Query =
                        (From c In vTramos
                         Order By c.VolumenUtilizado Ascending
                         Group c By c.IdTramo, c.VolumenUtilizado Into Cts = Group
                         Select IdTramo, VolumenUtilizado = Cts.Sum(Function(x) x.VolumenUtilizado)).ToList

                        For Each A As Object In Query
                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.VolumenUtilizado = A.VolumenUtilizado
                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).VolumenUtilizado += A.VolumenUtilizado
                            End If
                        Next

                    Else

                        Dim Query =
                       (From c In vTramos
                        Order By c.VolumenUtilizado Descending
                        Group c By c.IdTramo, c.VolumenUtilizado Into Cts = Group
                        Select IdTramo, VolumenUtilizado = Cts.Sum(Function(x) x.VolumenUtilizado)).ToList

                        For Each A As Object In Query
                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.VolumenUtilizado = A.VolumenUtilizado
                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).VolumenUtilizado += A.VolumenUtilizado
                            End If
                        Next

                    End If

                End If

            Else

                vTramos = clsLnBodega_tramo.Get_All_Tramos_Con_Cantidades_Ocupadas(pBeProducto.IdProductoBodega, pLote, pBeEstado.IdEstado, lConnection, lTransaction)

                If Not vTramos Is Nothing Then

                    If opc4MasLleno Then

                        Dim Query =
                        (From c In vTramos
                         Order By c.CantidadUtilizada Ascending
                         Group c By c.IdTramo, c.CantidadUtilizada Into Cts = Group
                         Select IdTramo, CantidadUtilizada = Cts.Sum(Function(x) x.CantidadUtilizada)).ToList


                        For Each A As Object In Query

                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.CantidadUtilizada = A.CantidadUtilizada

                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).CantidadUtilizada += A.CantidadUtilizad
                            End If


                        Next

                    Else

                        Dim Query =
                       (From c In vTramos
                        Order By c.CantidadUtilizada Descending
                        Group c By c.IdTramo, c.CantidadUtilizada Into Cts = Group
                        Select IdTramo, CantidadUtilizada = Cts.Sum(Function(x) x.CantidadUtilizada)).ToList

                        For Each A As Object In Query

                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.CantidadUtilizada = A.CantidadUtilizada
                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).CantidadUtilizada += A.CantidadUtilizad
                            End If

                        Next

                    End If

                End If


            End If

            For Each T As clsBeBodega_tramo In lTramos
                Distribuir_Cantidades_En_Tramo(T.IdTramo)
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

#End Region

#Region " Ubicacion - Vacias "

    Private Shared Sub Aplica_Regla_Por_Vacio(ByVal IdRegla As Integer)

        Dim volDispTramo, volDispUbic As Double
        Dim IdxListaPrincipalUbic As Integer

        If completo Then Return

        lUbicReglaTemp.Clear()

        Try

            For Each vTramo As clsBeBodega_tramo In lTramosConUbicVacias

                volDispTramo = 0

                For Each Ubic As clsBeUbicaciones_por_regla In lUbicacionesVacias.FindAll(Function(x) x.IdTramo = vTramo.IdTramo)

                    volDispUbic = Ubic.VolumenUbicacion
                    Ubic.VolumenDisponible = volDispUbic

                    If pBePresentacion.IdPresentacion <> 0 AndAlso pBePresentacion.Volumen <> 0 Then
                        Ubic.UBS_Cantidad_Maxima_Por_Ubicacion = Math.Truncate(volDispUbic / pBePresentacion.Volumen)
                    ElseIf pBeProducto.Volumen > 0 Then
                        Ubic.UBS_Cantidad_Maxima_Por_Ubicacion = Math.Truncate(volDispUbic / pBeProducto.Volumen)
                    Else
                        Ubic.UBS_Cantidad_Maxima_Por_Ubicacion = 0 'No se cuantos caben en la ubicación.
                    End If

                    Ubic.IdReglaUbicacionEnc = IdRegla '4


                    For Each UbiAgregadaInList As clsBeUbicaciones_por_regla In lUbicacionesVacias

                        IdxListaPrincipalUbic = lUbicacionesLista.FindIndex(Function(x) x.IdUbicacion = UbiAgregadaInList.IdUbicacion)

                        If IdxListaPrincipalUbic <> -1 Then
                            lUbicReglaTemp.Add(UbiAgregadaInList)
                            lUbicacionesLista.RemoveAt(IdxListaPrincipalUbic)
                        End If

                    Next

                    volDispTramo += volDispUbic

                Next

                vTramo.VolumenUtilizado = volDispTramo
            Next


            ' Ordenar la lista
            If (lUbicReglaTemp.Count > 0) Then

                If opc3LoteUbic Then

                    If opc4MasLleno Then

                        If opc2Vert Then
                            lUbicReglaTemp = lUbicReglaTemp.OrderBy(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                        Else
                            lUbicReglaTemp = lUbicReglaTemp.OrderBy(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x).ToList()
                        End If

                    Else

                        If opc2Vert Then
                            lUbicReglaTemp = lUbicReglaTemp.OrderByDescending(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                        Else
                            lUbicReglaTemp = lUbicReglaTemp.OrderByDescending(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x).ToList()
                        End If

                    End If

                    Aplica_Cantidades(lUbicReglaTemp)
                Else
                    Get_Lista_Tramos_Ordenados_Por_Vacio()
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Shared Sub Aplica_Regla_Por_Vacio_Pallet(ByVal IdRegla As Integer)

        Dim volDispTramo, volDispUbic As Double
        Dim IdxListaPrincipalUbic As Integer

        If completo Then Return

        lUbicReglaTemp.Clear()

        Try

            For Each vTramo As clsBeBodega_tramo In lTramosConUbicVacias

                volDispTramo = 0

                For Each Ubic As clsBeUbicaciones_por_regla In lUbicacionesVacias.FindAll(Function(x) x.IdTramo = vTramo.IdTramo)

                    volDispUbic = 1
                    Ubic.VolumenDisponible = volDispUbic
                    Ubic.UBS_Cantidad_Maxima_Por_Ubicacion = 1
                    Ubic.IdReglaUbicacionEnc = IdRegla '4

                    For Each UbiAgregadaInList As clsBeUbicaciones_por_regla In lUbicacionesVacias

                        IdxListaPrincipalUbic = lUbicacionesLista.FindIndex(Function(x) x.IdUbicacion = UbiAgregadaInList.IdUbicacion)

                        If IdxListaPrincipalUbic <> -1 Then
                            lUbicReglaTemp.Add(UbiAgregadaInList)
                            lUbicacionesLista.RemoveAt(IdxListaPrincipalUbic)
                        End If

                    Next

                    volDispTramo += volDispUbic

                Next

                vTramo.VolumenUtilizado = volDispTramo
            Next


            ' Ordenar la lista
            If (lUbicReglaTemp.Count > 0) Then

                If opc3LoteUbic Then

                    If opc4MasLleno Then

                        If opc2Vert Then
                            lUbicReglaTemp = lUbicReglaTemp.OrderBy(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                        Else
                            lUbicReglaTemp = lUbicReglaTemp.OrderBy(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x).ToList()
                        End If

                    Else

                        If opc2Vert Then
                            lUbicReglaTemp = lUbicReglaTemp.OrderByDescending(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Indice_x).ThenBy(Function(x) x.Nivel).ToList()
                        Else
                            lUbicReglaTemp = lUbicReglaTemp.OrderByDescending(Function(x) x.VolumenDisponible).ThenBy(Function(x) x.IdTramo).ThenBy(Function(x) x.Nivel).ThenBy(Function(x) x.Indice_x).ToList()
                        End If

                    End If

                    Aplica_Cantidades(lUbicReglaTemp)
                Else
                    Get_Lista_Tramos_Ordenados_Por_Vacio()
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Shared Sub Get_Lista_Tramos_Ordenados_Por_Vacio()

        Dim vTramos As New List(Of clsBeBodega_tramo)
        Dim Idx As Integer = -1

        Try

            lTramos.Clear()

            Dim BeTramo As New clsBeBodega_tramo

            If (VolumenProducto > 0) Then

                vTramos = lTramosConUbicVacias

                If Not vTramos Is Nothing Then

                    If opc4MasLleno Then

                        Dim Query =
                        (From c In vTramos
                         Order By c.VolumenUtilizado Ascending
                         Group c By c.IdTramo, c.VolumenUtilizado Into Cts = Group
                         Select IdTramo, VolumenUtilizado = Cts.Sum(Function(x) x.VolumenUtilizado)).ToList

                        For Each A As Object In Query
                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.VolumenUtilizado = A.VolumenUtilizado
                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).VolumenUtilizado += A.VolumenUtilizado
                            End If
                        Next

                    Else

                        Dim Query =
                       (From c In vTramos
                        Order By c.VolumenUtilizado Descending
                        Group c By c.IdTramo, c.VolumenUtilizado Into Cts = Group
                        Select IdTramo, VolumenUtilizado = Cts.Sum(Function(x) x.VolumenUtilizado)).ToList

                        For Each A As Object In Query
                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.VolumenUtilizado = A.VolumenUtilizado
                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).VolumenUtilizado += A.VolumenUtilizado
                            End If
                        Next

                    End If

                End If

            Else

                vTramos = lTramosConUbicVacias

                If Not vTramos Is Nothing Then

                    If opc4MasLleno Then

                        Dim Query =
                        (From c In vTramos
                         Order By c.CantidadUtilizada Ascending
                         Group c By c.IdTramo, c.CantidadUtilizada Into Cts = Group
                         Select IdTramo, CantidadUtilizada = Cts.Sum(Function(x) x.CantidadUtilizada)).ToList


                        For Each A As Object In Query

                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.CantidadUtilizada = A.CantidadUtilizada

                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).CantidadUtilizada += A.CantidadUtilizad
                            End If


                        Next

                    Else

                        Dim Query =
                       (From c In vTramos
                        Order By c.CantidadUtilizada Descending
                        Group c By c.IdTramo, c.CantidadUtilizada Into Cts = Group
                        Select IdTramo, CantidadUtilizada = Cts.Sum(Function(x) x.CantidadUtilizada)).ToList

                        For Each A As Object In Query
                            BeTramo = New clsBeBodega_tramo
                            BeTramo.IdTramo = A.IdTramo
                            BeTramo.CantidadUtilizada = A.CantidadUtilizada
                            Idx = lTramos.FindIndex(Function(x) x.IdTramo = A.IdTramo)

                            If Idx = -1 Then
                                lTramos.Add(BeTramo)
                            Else
                                lTramos(Idx).CantidadUtilizada += A.CantidadUtilizad
                            End If
                        Next

                    End If

                End If


            End If

            For Each T As clsBeBodega_tramo In lTramos
                Distribuir_Cantidades_En_Tramo(T.IdTramo)
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

#End Region


End Class