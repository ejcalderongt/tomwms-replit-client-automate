Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTransUbicSugerida2

    Public Const MIN_FECHA_VENCE = -30
    Public Const MAX_FECHA_VENCE = 30

    Public Class clsBeProductoUbicacionSugerida

        Public Property IdProducto As Integer = 0
        Public Property IdBodega As Integer = 0
        Public Property IdProductoBodega As Integer = 0
        Public Property Lote As String = ""
        Public Property Fecha_Vence As Date = New Date(199, 1, 1)
        Public Property IdUbicacionOrigen As Integer = 0
        Public Property IdEstadoProducto As Integer = 0
        Public Property IdUnidadMedidaBas As Integer = 0
        Public Property IdPresentacion As Integer = 0
        Public Property Cantidad_A_Ubicar As Double = 0

    End Class

    Private Class clsBeAtributosStock

        Public Property IdProductoEstado As Integer = 0
        Public Property IdUnidadMedida As Integer = 0
        Public Property IdPresentacion As Integer = 0
        Public Property Fecha_Vence As Date = New Date(1900, 1, 1)
        Public Property Lote As String = ""

    End Class

    Public Class StockFlags

        Public Property ContieneLote As Boolean = False
        Public Property ContieneEstado As Boolean = False
        Public Property ContieneUMBas As Boolean = False
        Public Property ContienePresentacion As Boolean = False
        Public Property ContieneVencimiento As Boolean = False

    End Class

    Public Class clsBeTramosUSStage4

        Public Property IdBodega As Integer = 0
        Public Property IdTramo As Integer = 0
        Public Property lUbicaciones As New List(Of USUbicStrucStage1)
        Public Property AvgRating As Double = 0

        Public Function Clone() As clsBeTramosUSStage4
            Return DirectCast(Me.MemberwiseClone(), clsBeTramosUSStage4)
        End Function

    End Class

    Public Class USUbicSingle

        Public Property IdUbicacion As Integer = 0
        Public Property Orientacion As String = ""
        Public Property Bloqueada As Boolean = False
        Public Property Acepta_Pallet As Boolean = False
        Public Property Ubicacion_Merma As Boolean = False
        Public Property Dañado As Boolean = False

    End Class

    Public Class USUbicStrucStage5 : Inherits USUbicStrucStage1
        Public Property IdTramo As Integer = 0
        Public Property AvgPrediction As Double = 0
    End Class

    Public Class USUbicStrucStage1

        Public Property Columna As Integer
        Public Property Nivel As Integer
        Public Property lUbicacionesVacias As New List(Of USUbicSingle)
        Public Property lUbicacionesOcupadas As New List(Of USUbicSingle)
        Public Property PrediccionUbicacion As Double = 0

    End Class

    Public Class clsBeTramosUSStage3

        Public Property IdBodega As Integer = 0
        Public Property IdTramo As Integer = 0
        Public Property PrediccionLote As Double = 0
        Public Property PrediccionVencimiento As Double = 0
        Public Property PrediccionEstado As Double = 0
        Public Property PrediccionProducto As Double = 0
        Public Property AvgPrediction As Double = 0

    End Class

    Public Class clsBeTramosUSStage2

        Public Property IdBodega As Integer = 0
        Public Property IdTramo As Integer = 0
        Public Property PesoOcupacion As Double = 0
        Public Property PesoCaracteristico As Double = 0

    End Class

    Public Class clsBeTramosUSStage1

        Public Property IdBodega As Integer = 0
        Public Property IdTramo As Integer = 0
        ''' <summary>
        ''' Cantidad de posiciones vacías.
        ''' </summary>
        ''' <returns></returns>
        Public Property Ubicaciones_Vacias As Integer = 0
        Public Property Total_Ubicaciones As Integer = 0
        Public Property PesoOcupacion As Double = 0
        Public Property PesoCaracteristico As Double = 0
        Public Property Lote As New StockFlags
        Public Property Estado As New StockFlags
        Public Property Vencimiento As New StockFlags

        Public Property ContieneUMBas As Boolean = False
        Public Property ContienePresentacion As Boolean = False


    End Class


    Public Shared Function Get_Ubicaciones_Sugeridas(ByVal IdProducto As Integer,
                                                     ByVal IdBodega As Integer,
                                                     ByVal IdProductoBodega As Integer,
                                                     ByVal Lote As String,
                                                     ByVal Fecha_Vence As Date,
                                                     ByVal IdEstadoProducto As Integer,
                                                     ByVal IdUmBas As Integer,
                                                     ByVal IdPresentacion As Integer) As List(Of USUbicStrucStage5)

        Get_Ubicaciones_Sugeridas = Nothing


        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lTramosStage1 As New List(Of clsBeTramosUSStage1)
        Dim lResultUbicaciones As New List(Of USUbicStrucStage5)
        Dim lResultTop5 As New List(Of USUbicStrucStage5)
        Dim lTramosStage2 As New List(Of clsBeTramosUSStage2)
        Dim lTramosStage3 As New List(Of clsBeTramosUSStage3)
        Dim lTramosStage4 As New List(Of clsBeTramosUSStage4)
        Dim pBeProductoUS As New clsBeProductoUbicacionSugerida

        pBeProductoUS.IdProducto = IdProducto
        pBeProductoUS.IdProductoBodega = IdProductoBodega
        pBeProductoUS.IdBodega = IdBodega

        Try

            If Campos_Obligatorios_Definidos(pBeProductoUS) Then

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                'Aquí se buscan los tramos que contien el producto (actualmente almacencado)
                'si la lista, retorna nothing, en adelante, la multiplicación de todo es 0.
                'buscar la última posición donde estuvo guardado el producto.
                lTramosStage1 = Get_All_Tramos_With_The_Product(pBeProductoUS.IdProductoBodega,
                                                                pBeProductoUS.IdBodega,
                                                                lConnection,
                                                                lTransaction)

                Asigna_Peso1_Tramos(lTramosStage1,
                                    lConnection,
                                    lTransaction)

                lTramosStage2 = Asigna_Peso_Caracteristico_Tramos(IdProductoBodega,
                                                                  Lote,
                                                                  Fecha_Vence,
                                                                  IdEstadoProducto,
                                                                  IdUmBas,
                                                                  IdPresentacion,
                                                                  lTramosStage1,
                                                                  lConnection,
                                                                  lTransaction)

                '#EJC20200427: Stable pesos..

                Dim lParametros As New List(Of Double)

                Dim mineuronita As New PerceptronUbicacion.Program()

                If Not lTramosStage1 Is Nothing Then

                    For Each T2 In lTramosStage1.OrderBy(Function(x) x.PesoOcupacion)

                        Dim TramoStage3 As New clsBeTramosUSStage3
                        TramoStage3.IdBodega = T2.IdBodega
                        TramoStage3.IdTramo = T2.IdTramo

                        lParametros.Add(T2.PesoOcupacion)
                        lParametros.Add(T2.Lote.ContieneLote)
                        lParametros.Add(T2.Lote.ContieneEstado)
                        lParametros.Add(T2.Lote.ContieneUMBas)
                        lParametros.Add(T2.Lote.ContienePresentacion)
                        lParametros.Add(T2.Lote.ContieneVencimiento)
                        lParametros.Add(T2.PesoCaracteristico)
                        TramoStage3.PrediccionLote = mineuronita.Get_Prediction(lParametros)

                        lParametros = New List(Of Double)
                        lParametros.Add(T2.PesoOcupacion)
                        lParametros.Add(T2.Vencimiento.ContieneLote)
                        lParametros.Add(T2.Vencimiento.ContieneEstado)
                        lParametros.Add(T2.Vencimiento.ContieneUMBas)
                        lParametros.Add(T2.Vencimiento.ContienePresentacion)
                        lParametros.Add(T2.Vencimiento.ContieneVencimiento)
                        lParametros.Add(T2.PesoCaracteristico)
                        TramoStage3.PrediccionVencimiento = mineuronita.Get_Prediction(lParametros)

                        lParametros = New List(Of Double)
                        lParametros.Add(T2.PesoOcupacion)
                        lParametros.Add(T2.Estado.ContieneLote)
                        lParametros.Add(T2.Estado.ContieneEstado)
                        lParametros.Add(T2.Estado.ContieneUMBas)
                        lParametros.Add(T2.Estado.ContienePresentacion)
                        lParametros.Add(T2.Estado.ContieneVencimiento)
                        lParametros.Add(T2.PesoCaracteristico)
                        TramoStage3.PrediccionEstado = mineuronita.Get_Prediction(lParametros)

                        TramoStage3.AvgPrediction = Math.Round((T2.PesoOcupacion + T2.PesoCaracteristico) / 2, 10)

                        lTramosStage3.Add(TramoStage3)

                    Next

                End If

                If lTramosStage3.Count > 0 Then

                    Dim MaxRatingTramo = lTramosStage3.Max(Function(x) x.AvgPrediction)
                    Dim lTramosByDescendingForeCast = From T In lTramosStage3
                                                      Order By T.AvgPrediction Descending
                                                      Select T
                    Dim lUbicacionesVacias As New List(Of USUbicStrucStage1)
                    Dim lNivelesYColumnasOcupadas As New List(Of USUbicStrucStage1)
                    Dim lNivelesVacios As New List(Of Integer)
                    Dim lNivelesOcupados As New List(Of Integer)
                    Dim lColumnasVacias As New List(Of Integer)
                    Dim lColumnasOcupadas As New List(Of Integer)
                    Dim BeTramoUSStage1 As New clsBeTramosUSStage1
                    Dim lDicProductosTramo As New Dictionary(Of String, String)()

                    For Each TramoForeCast In lTramosByDescendingForeCast

                        Dim BeTramoStage4 As New clsBeTramosUSStage4
                        BeTramoStage4.IdBodega = TramoForeCast.IdBodega
                        BeTramoStage4.IdTramo = TramoForeCast.IdTramo
                        BeTramoStage4.AvgRating = TramoForeCast.AvgPrediction

                        lNivelesYColumnasOcupadas = Get_Niveles_Y_Columnas_Ocupadas_By_IdBodega_And_IdTramo(TramoForeCast.IdBodega,
                                                                                                            TramoForeCast.IdTramo,
                                                                                                            BeTramoUSStage1,
                                                                                                            Lote,
                                                                                                            Fecha_Vence,
                                                                                                            IdUmBas,
                                                                                                            IdPresentacion,
                                                                                                            IdEstadoProducto,
                                                                                                            lConnection,
                                                                                                            lTransaction)

                        If Not lNivelesYColumnasOcupadas Is Nothing Then

                            Dim Niveles = From c In lNivelesYColumnasOcupadas
                                          Select New With {Key c.Nivel} Distinct.ToList()

                            For Each N In Niveles
                                lNivelesOcupados.Add(N.Nivel)
                            Next

                            Dim Columnas = From c In lNivelesYColumnasOcupadas
                                           Select New With {Key c.Columna} Distinct.ToList().Take(5)

                            For Each C In Columnas
                                lColumnasOcupadas.Add(C.Columna)
                            Next

                            Dim lProductosTramo As Integer = 0

                            For Each N In lNivelesOcupados.Take(4)

                                For Each Columna In lColumnasOcupadas.Take(3)

                                    lProductosTramo = clsLnVW_stock_res.Get_Count_Stock_ML_Ubicacion_Sugerida(IdProducto,
                                                                                                                  TramoForeCast.IdBodega,
                                                                                                                  TramoForeCast.IdTramo,
                                                                                                                  Columna,
                                                                                                                  lConnection,
                                                                                                                  lTransaction)

                                    If lProductosTramo > 0 Then
                                        TramoForeCast.PrediccionProducto += lProductosTramo / 100
                                    End If

                                    Dim BeUbic As New USUbicStrucStage1
                                    BeUbic.Columna = Columna
                                    BeUbic.Nivel = N
                                    BeUbic.PrediccionUbicacion = TramoForeCast.AvgPrediction + TramoForeCast.PrediccionLote + TramoForeCast.PrediccionVencimiento + TramoForeCast.PrediccionProducto
                                    BeUbic.lUbicacionesVacias = Get_Ubicaciones_Vacias_By_Columna_And_Nivel(TramoForeCast.IdBodega,
                                                                                                            TramoForeCast.IdTramo,
                                                                                                            Columna,
                                                                                                            N,
                                                                                                            lConnection,
                                                                                                            lTransaction)


                                    BeUbic.lUbicacionesOcupadas = Get_Ubicaciones_Ocupadas_By_Columna_And_Nivel(TramoForeCast.IdBodega,
                                                                                                                TramoForeCast.IdTramo,
                                                                                                                Columna,
                                                                                                                N,
                                                                                                                lConnection,
                                                                                                                lTransaction)

                                    BeTramoStage4.lUbicaciones.Add(BeUbic)


                                Next

                            Next

                            'BeTramoStage4.AvgRating = T.PrediccionEstado + T.PrediccionLote + T.PrediccionVencimiento + T.PrediccionProducto

                            lTramosStage4.Add(BeTramoStage4)

                        End If

                    Next

                    lResultUbicaciones = Aplica_Reglas_Por_Bodega(IdBodega,
                                                                 lTramosStage4,
                                                                 lConnection,
                                                                 lTransaction)


                    '#EJC20240711 Posible solución
                    'Dim Top5a = lResultUbicaciones.OrderByDescending(Function(x) x.IdTramo).OrderBy(Function(y) y.Columna).OrderBy(Function(z) z.Nivel).Take(5)

                    '#EJC20200427: Tomar los primeros 5 y ordenar por columna
                    Dim Top5 = lResultUbicaciones.Take(5).OrderByDescending(Function(x) x.AvgPrediction).ThenBy(Function(y) y.Nivel)

                    For Each Result In Top5
                        lResultTop5.Add(Result)
                    Next

                    Return lResultTop5

                End If

            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Private Shared Function Aplica_Reglas_Por_Bodega(ByVal pIdBodega As Integer,
                                                     ByVal pListaUbic As List(Of clsBeTramosUSStage4),
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of USUbicStrucStage5)

        Aplica_Reglas_Por_Bodega = Nothing

        Try

            '#EJC20200427: Aun no se están aplicando las reglas/prioridades
            'Sin embargo quise buscarlas para dejarlas como precedente.S
            Dim BeReglaUbicPrio As New clsBeRegla_ubic_prio_enc
            BeReglaUbicPrio = clsLnRegla_ubic_prio_enc.Get_Single_By_IdBodega(pIdBodega,
                                                                              lConnection,
                                                                              lTransaction)

            If Not BeReglaUbicPrio Is Nothing Then

                If Not pListaUbic Is Nothing Then

                    If pListaUbic.Count > 0 Then

                        If Not BeReglaUbicPrio.lReglaUbicPrioDet Is Nothing Then

                            Dim vPrimerNivel As Boolean = True
                            Dim vLlenadoHorizontal As Boolean = False
                            Dim vTramoMasLLenoPrimero As Boolean = False

                            Dim BeRegla_ubic_prio_det As clsBeRegla_ubic_prio_det = BeReglaUbicPrio.lReglaUbicPrioDet.Find(Function(x) x.IdReglaUbicPrioParam = 6)

                            If Not BeRegla_ubic_prio_det Is Nothing Then
                                vPrimerNivel = BeRegla_ubic_prio_det.Activo
                            End If

                            BeRegla_ubic_prio_det = BeReglaUbicPrio.lReglaUbicPrioDet.Find(Function(x) x.IdReglaUbicPrioParam = 7)

                            If Not BeRegla_ubic_prio_det Is Nothing Then
                                vLlenadoHorizontal = BeRegla_ubic_prio_det.Activo
                            End If

                            BeRegla_ubic_prio_det = BeReglaUbicPrio.lReglaUbicPrioDet.Find(Function(x) x.IdReglaUbicPrioParam = 9)

                            If Not BeRegla_ubic_prio_det Is Nothing Then
                                vTramoMasLLenoPrimero = BeRegla_ubic_prio_det.Activo
                            End If

                            '#EJC20200427: Aplicar estas reglas al 
                            'comportamiento realizado en este paso
                            'Att. Erik del pasado.
                            GoTo Default_List
                            'Yo sé, es raro pero muy práctico llamar el GoTo...
                            'No me juzgues Carol jaja

                        End If

                    End If

                End If

            Else

Default_List:
                '#EJC20200426: Under construction on my brain...
                'It should give back a default list without defined prioritys
                '#EJC20200427: Stable to etapa II
                Dim lUbicacionesVaciasTramo As New List(Of USUbicStrucStage5)
                Dim lTramosCandidatos As New List(Of clsBeTramosUSStage4)
                Dim vIndiceTramo As Integer = -1

                For Each T In pListaUbic

                    Dim lUbicacionesOrdenadas = T.lUbicaciones.OrderBy(Function(x) x.Columna).ThenBy(Function(x) x.Nivel)

                    Dim CopyOfT As New clsBeTramosUSStage4

                    For Each U In lUbicacionesOrdenadas

                        If Not U.lUbicacionesVacias Is Nothing Then

                            If U.lUbicacionesVacias.Count > 0 Then

                                vIndiceTramo = lTramosCandidatos.FindIndex(Function(x) x.IdTramo = T.IdTramo)

                                If vIndiceTramo = -1 Then

                                    Dim BeUbicStage5 As New USUbicStrucStage5
                                    BeUbicStage5.IdTramo = T.IdTramo
                                    BeUbicStage5.Columna = U.Columna
                                    BeUbicStage5.lUbicacionesOcupadas = U.lUbicacionesOcupadas
                                    BeUbicStage5.lUbicacionesVacias = U.lUbicacionesVacias
                                    BeUbicStage5.Nivel = U.Nivel
                                    BeUbicStage5.AvgPrediction = U.PrediccionUbicacion

                                    If Not U.lUbicacionesVacias Is Nothing Then
                                        lUbicacionesVaciasTramo.Add(BeUbicStage5)
                                    End If

                                End If

                            End If

                        Else
                            Dim vMensaje As String = "Tramo:" & T.IdTramo & " Columa: " & U.Columna & " Nivel: " & U.Nivel & " Excluded By No Empty Locations"
                            Debug.WriteLine(vMensaje)
                            Console.WriteLine(vMensaje)
                        End If

                    Next

                Next

                Return lUbicacionesVaciasTramo

            End If

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

    End Function

    Private Shared Function Asigna_Peso_Caracteristico_Tramos(ByVal IdProductoBodega As Integer,
                                                              ByVal Lote As String,
                                                              ByVal Fecha_Vence As Date,
                                                              ByVal IdProductoEstado As Integer,
                                                              ByVal IdUmBas As Integer,
                                                              ByVal IdPresentacion As Integer,
                                                              ByRef lTramosStage1 As List(Of clsBeTramosUSStage1),
                                                              ByVal lConnection As SqlConnection,
                                                              ByVal lTransaction As SqlTransaction) As List(Of clsBeTramosUSStage2)

        Asigna_Peso_Caracteristico_Tramos = Nothing

        'Determinar en que medida, los tramosm contienen características similares al producto.

        Try

            Dim lProductoEstado As New List(Of Integer)
            Dim vIndiceParametro As Integer = -1
            Dim lAtribStock As New List(Of clsBeAtributosStock)
            Dim lReturnList As New List(Of clsBeTramosUSStage2)

            Dim vPesoLote As Double = 0
            Dim vPesoVence As Double = 0
            Dim vPesoEstado As Double = 0
            Dim vCantidadParametrosDivisor As Integer = 0

            If Not lTramosStage1 Is Nothing Then

                For Each Tramo In lTramosStage1

                    vPesoEstado = 0 : vPesoVence = 0 : vPesoLote = 0
                    Dim BeTramoStage2 As New clsBeTramosUSStage2()
                    BeTramoStage2.IdBodega = Tramo.IdBodega
                    BeTramoStage2.IdTramo = Tramo.IdTramo
                    BeTramoStage2.PesoOcupacion = Tramo.PesoOcupacion

#Region "Existe_Lote"

                    If Lote <> "" Then

                        lAtribStock = Tramo_Contiene_Lote(Tramo.IdBodega,
                                                          Tramo.IdTramo,
                                                          IdProductoBodega,
                                                          Lote,
                                                          lConnection,
                                                          lTransaction)

                        If Not lAtribStock Is Nothing Then

                            Tramo.Lote.ContieneLote = True
                            vPesoLote = 0.2F

                            vIndiceParametro = lAtribStock.FindIndex(Function(x) x.IdProductoEstado = IdProductoEstado)

                            If vIndiceParametro <> -1 Then
                                Tramo.Lote.ContieneEstado = True
                                vPesoLote += 0.2F
                            End If

                            vIndiceParametro = lAtribStock.FindIndex(Function(x) x.IdUnidadMedida = IdUmBas)

                            If vIndiceParametro <> -1 Then
                                Tramo.Lote.ContieneUMBas = True
                                vPesoLote += 0.2F
                            End If

                            vIndiceParametro = lAtribStock.FindIndex(Function(x) x.IdPresentacion = IdPresentacion)

                            If vIndiceParametro <> -1 Then
                                Tramo.Lote.ContienePresentacion = True
                                vPesoLote += 0.2F
                            End If

                            vIndiceParametro = lAtribStock.Exists(Function(x) x.Fecha_Vence <= Fecha_Vence.AddDays(MIN_FECHA_VENCE) And x.Fecha_Vence >= Fecha_Vence.AddDays(MAX_FECHA_VENCE))

                            If vIndiceParametro <> -1 Then
                                Tramo.Lote.ContieneVencimiento = True
                                vPesoLote += 0.2F
                            End If

                            vCantidadParametrosDivisor += 1

                        End If

                    End If

#End Region

                    If Fecha_Vence <> New Date(1900, 1, 1) Then

                        lAtribStock = Tramo_Contiene_Vencimiento(Tramo.IdBodega,
                                                                 Tramo.IdTramo,
                                                                 IdProductoBodega,
                                                                 Fecha_Vence,
                                                                 lConnection,
                                                                 lTransaction)

                        If Not lAtribStock Is Nothing Then

                            Tramo.Vencimiento.ContieneVencimiento = True
                            vPesoVence = 0.25F
                            '#EJC20200421
                            'Inferir que como primero se evaluó el lote,
                            'Aquí no es necesario volver a evaluarlo, ya debe tener un valor
                            'Sea este falo o verdadero

                            vIndiceParametro = lAtribStock.FindIndex(Function(x) x.IdUnidadMedida = IdUmBas)

                            If vIndiceParametro <> -1 Then
                                Tramo.Vencimiento.ContieneUMBas = True
                                vPesoVence += 0.25F
                            End If

                            vIndiceParametro = lAtribStock.FindIndex(Function(x) x.IdPresentacion = IdPresentacion)

                            If vIndiceParametro <> -1 Then
                                Tramo.Vencimiento.ContienePresentacion = True
                                vPesoVence += 0.25F
                            End If

                            vIndiceParametro = lAtribStock.FindIndex(Function(x) x.IdProductoEstado = IdProductoEstado)

                            If vIndiceParametro <> -1 Then
                                Tramo.Vencimiento.ContieneEstado = True
                                vPesoVence += 0.25F
                            End If

                            vCantidadParametrosDivisor += 1

                        End If

                    End If

                    lAtribStock = Tramo_Contiene_Estado(Tramo.IdBodega,
                                                        Tramo.IdTramo,
                                                        IdProductoBodega,
                                                        IdProductoEstado,
                                                        lConnection,
                                                        lTransaction)

                    If Not lAtribStock Is Nothing Then

                        Tramo.Estado.ContieneEstado = True
                        vPesoEstado = 0.33F

                        '#EJC20200421
                        'Inferir que como primero se evaluó el lote,
                        'Aquí no es necesario volver a evaluarlo, ya debe tener un valor
                        'Sea este falo o verdadero

                        vIndiceParametro = lAtribStock.FindIndex(Function(x) x.IdUnidadMedida = IdUmBas)

                        If vIndiceParametro <> -1 Then
                            Tramo.Estado.ContieneUMBas = True
                            vPesoEstado += 0.33F
                        End If

                        vIndiceParametro = lAtribStock.FindIndex(Function(x) x.IdPresentacion = IdPresentacion)

                        If vIndiceParametro <> -1 Then
                            Tramo.Estado.ContienePresentacion = True
                            vPesoEstado += 0.33F
                        End If

                        vCantidadParametrosDivisor += 1

                    End If

                    If vCantidadParametrosDivisor > 0 Then
                        BeTramoStage2.PesoCaracteristico = Math.Round((vPesoEstado + vPesoLote + vPesoVence) / vCantidadParametrosDivisor, 2)
                    Else
                        BeTramoStage2.PesoCaracteristico = 0
                    End If

                    Tramo.PesoCaracteristico = BeTramoStage2.PesoCaracteristico

                    lReturnList.Add(BeTramoStage2)

                Next

            End If

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Campos_Obligatorios_Definidos(ByVal pBeProductoUS As clsBeProductoUbicacionSugerida) As Boolean

        Campos_Obligatorios_Definidos = False

        If pBeProductoUS.IdBodega = 0 Then
            Throw New Exception("No se definió la bodega para el producto en la solicitud de ubicación sugerida.")
        ElseIf pBeProductoUS.IdProducto = 0 Then
            Throw New Exception("No se definió el IdProducto en la solicitud de ubicación sugerida.")
        ElseIf pBeProductoUS.IdProductoBodega = 0 Then
            Throw New Exception("No se definió el IdProductoBodega en la solicitud de ubicación sugerida.")
        Else
            Campos_Obligatorios_Definidos = True
        End If

    End Function

    ''' <summary>
    ''' Devuelve el listado de tramos que contienen stock del producto solicitado.
    ''' </summary>
    ''' <param name="pIdProductoBodega"></param>
    ''' <param name="pIdBodega"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Private Shared Function Get_All_Tramos_With_The_Product(ByVal pIdProductoBodega As Integer,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal lConnection As SqlConnection,
                                                            ByVal lTransaction As SqlTransaction) As List(Of clsBeTramosUSStage1)

        Get_All_Tramos_With_The_Product = Nothing

        Try

            '--#EJC202211022145: Agregué campos, bodega_ubicacion.activo, bloqueada, ubicacion_merma
            Dim vSQL As String = "SELECT DISTINCT IdTramo FROM vw_stock_res 
                                  WHERE IdProductoBodega = @IdProductoBodega
                                  AND IdBodega = @IdBodega 
                                  AND activo = 1 
                                  AND bloqueada =0 
                                  AND ubicacion_merma = 0 "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim BeTramoUSStage1 As clsBeTramosUSStage1

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lTramosList As New List(Of clsBeTramosUSStage1)

                    For Each lRow As DataRow In lDataTable.Rows

                        BeTramoUSStage1 = New clsBeTramosUSStage1
                        BeTramoUSStage1.IdBodega = pIdBodega
                        BeTramoUSStage1.IdTramo = IIf(IsDBNull(lRow.Item("IdTramo")), 0, lRow.Item("IdTramo"))
                        BeTramoUSStage1.Ubicaciones_Vacias = 0
                        lTramosList.Add(BeTramoUSStage1)

                    Next

                    Return lTramosList

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Asigna_Peso1_Tramos(ByRef pListTramosStage1 As List(Of clsBeTramosUSStage1),
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As Boolean

        Try

            If Not pListTramosStage1 Is Nothing Then

                For Each Tramo In pListTramosStage1

                    Tramo.Ubicaciones_Vacias = Get_Ubicaciones_Vacias(Tramo.IdBodega,
                                                                      Tramo.IdTramo,
                                                                      lConnection,
                                                                      lTransaction)

                    Tramo.Total_Ubicaciones = Get_Total_Ubicaciones_Tramo(Tramo.IdBodega,
                                                                          Tramo.IdTramo,
                                                                          lConnection,
                                                                          lTransaction)

                    If Tramo.Total_Ubicaciones <> 0 Then
                        Tramo.PesoOcupacion = Math.Round(Tramo.Ubicaciones_Vacias / Tramo.Total_Ubicaciones, 2)
                    Else
                        Tramo.PesoOcupacion = 0
                    End If

                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Get_Total_Ubicaciones_Tramo(ByVal IdBodega As Integer,
                                                        ByVal IdTramo As Integer,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As Integer

        Get_Total_Ubicaciones_Tramo = 0

        Try

            Return clsLnBodega_tramo.Get_Ubicaciones_Vacias_By_IdBodega_And_IdTramo(IdBodega,
                                                                             IdTramo,
                                                                             lConnection,
                                                                             lTransaction)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Get_Ubicaciones_Vacias(ByVal pIdBodega As Integer,
                                                   ByVal pIdTramo As Integer,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As Integer

        Get_Ubicaciones_Vacias = 0

        Try

            Dim vSQL As String = "SELECT Count(IdUbicacion) AS Ubicaciones_Vacias FROM VW_OcupacionBodegaTramo
                                  WHERE IdTramo = @IdTramo
                                  AND IdBodega = @IdBodega 
                                  AND IdStock =0"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vCantUbicacionesVacias As Integer = IIf(IsDBNull(lDataTable.Rows(0).Item("Ubicaciones_Vacias")), 0, lDataTable.Rows(0).Item("Ubicaciones_Vacias"))

                    Return vCantUbicacionesVacias

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Tramo_Contiene_Lote(ByVal pIdBodega As Integer,
                                                ByVal pIdTramo As Integer,
                                                ByVal pIdProductoBodega As Integer,
                                                ByVal pLote As String,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As List(Of clsBeAtributosStock)

        Tramo_Contiene_Lote = Nothing

        Try


            Dim vSQL As String = "SELECT DISTINCT IdProductoEstado, 
                                                  IdUnidadMedida, 
                                                  IdPresentacion,
                                                  Lote,
                                                  Fecha_Vence
                                  FROM VW_STOCK_RES
                                  WHERE IdTramo = @IdTramo
                                  AND IdBodega = @IdBodega 
                                  AND IdProductoBodega = @IdProductoBodega 
                                  AND Lote LIKE CONCAT('%',@Lote,'%') "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pLote)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vIdProductoEstado As Integer = 0
                    Dim pLista As New List(Of clsBeAtributosStock)

                    For Each lRow As DataRow In lDataTable.Rows

                        Dim pAtStock As New clsBeAtributosStock
                        pAtStock.IdUnidadMedida = IIf(IsDBNull(lRow.Item("IdUnidadMedida")), 0, lRow.Item("IdUnidadMedida"))
                        pAtStock.IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                        pAtStock.IdProductoEstado = IIf(IsDBNull(lRow.Item("IdProductoEstado")), 0, lRow.Item("IdProductoEstado"))
                        pAtStock.Lote = IIf(IsDBNull(lRow.Item("IdProductoEstado")), 0, lRow.Item("IdProductoEstado"))
                        pAtStock.Fecha_Vence = IIf(IsDBNull(lRow.Item("Fecha_Vence")), New Date(1900, 1, 1), lRow.Item("Fecha_Vence"))
                        pLista.Add(pAtStock)

                    Next

                    Return pLista

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Tramo_Contiene_Vencimiento(ByVal pIdBodega As Integer,
                                                       ByVal pIdTramo As Integer,
                                                       ByVal pIdProductoBodega As Integer,
                                                       ByVal pVencimiento As Date,
                                                       ByVal lConnection As SqlConnection,
                                                       ByVal lTransaction As SqlTransaction) As List(Of clsBeAtributosStock)

        Tramo_Contiene_Vencimiento = Nothing

        Try


            Dim vSQL As String = "SELECT DISTINCT IdProductoEstado, 
                                                  IdUnidadMedida, 
                                                  IdPresentacion,
                                                  Lote,
                                                  Fecha_Vence
                                  FROM VW_STOCK_RES
                                  WHERE IdTramo = @IdTramo
                                  AND IdBodega = @IdBodega 
                                  AND IdProductoBodega = @IdProductoBodega 
                                  AND fecha_vence BETWEEN " & FormatoFechas.fFecha(pVencimiento.AddDays(MIN_FECHA_VENCE)) &
                                  " AND " & FormatoFechas.fFecha(pVencimiento.AddDays(MAX_FECHA_VENCE))



            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vIdProductoEstado As Integer = 0
                    Dim pLista As New List(Of clsBeAtributosStock)

                    For Each lRow As DataRow In lDataTable.Rows

                        Dim pAtStock As New clsBeAtributosStock
                        pAtStock.IdUnidadMedida = IIf(IsDBNull(lRow.Item("IdUnidadMedida")), 0, lRow.Item("IdUnidadMedida"))
                        pAtStock.IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                        pAtStock.IdProductoEstado = IIf(IsDBNull(lRow.Item("IdProductoEstado")), 0, lRow.Item("IdProductoEstado"))
                        pAtStock.Lote = IIf(IsDBNull(lRow.Item("Lote")), "", lRow.Item("Lote"))
                        pAtStock.Fecha_Vence = IIf(IsDBNull(lRow.Item("Fecha_Vence")), New Date(1900, 1, 1), lRow.Item("Fecha_Vence"))
                        pLista.Add(pAtStock)

                    Next

                    Return pLista

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Tramo_Contiene_Estado(ByVal pIdBodega As Integer,
                                                  ByVal pIdTramo As Integer,
                                                  ByVal pIdProductoBodega As Integer,
                                                  ByVal pIdProductoEstado As Integer,
                                                  ByVal lConnection As SqlConnection,
                                                  ByVal lTransaction As SqlTransaction) As List(Of clsBeAtributosStock)

        Tramo_Contiene_Estado = Nothing

        Try


            Dim vSQL As String = "SELECT DISTINCT IdProductoEstado, 
                                                  IdUnidadMedida, 
                                                  IdPresentacion,
                                                  Lote,
                                                  Fecha_Vence
                                  FROM VW_STOCK_RES
                                  WHERE IdTramo = @IdTramo
                                  AND IdBodega = @IdBodega 
                                  AND IdProductoBodega = @IdProductoBodega 
                                  AND IdProductoEstado = @IdProductoEstado "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pIdProductoEstado)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vIdProductoEstado As Integer = 0
                    Dim pLista As New List(Of clsBeAtributosStock)

                    For Each lRow As DataRow In lDataTable.Rows

                        Dim pAtStock As New clsBeAtributosStock
                        pAtStock.IdUnidadMedida = IIf(IsDBNull(lRow.Item("IdUnidadMedida")), 0, lRow.Item("IdUnidadMedida"))
                        pAtStock.IdPresentacion = IIf(IsDBNull(lRow.Item("IdPresentacion")), 0, lRow.Item("IdPresentacion"))
                        pAtStock.IdProductoEstado = IIf(IsDBNull(lRow.Item("IdProductoEstado")), 0, lRow.Item("IdProductoEstado"))
                        pAtStock.Lote = IIf(IsDBNull(lRow.Item("IdProductoEstado")), 0, lRow.Item("IdProductoEstado"))
                        pAtStock.Fecha_Vence = IIf(IsDBNull(lRow.Item("Fecha_Vence")), New Date(1900, 1, 1), lRow.Item("Fecha_Vence"))
                        pLista.Add(pAtStock)

                    Next

                    Return pLista

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Get_Ubicaciones_Ocupadas_By_Columna_And_Nivel(ByVal pIdBodega As Integer,
                                                                        ByVal pIdTramo As Integer,
                                                                        ByVal pColumna As Integer,
                                                                        ByVal pNivel As Integer,
                                                                        ByVal lConnection As SqlConnection,
                                                                        ByVal lTransaction As SqlTransaction) As List(Of USUbicSingle)

        Get_Ubicaciones_Ocupadas_By_Columna_And_Nivel = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_Ubicaciones_Tramo_Disponibles
                                  WHERE IdTramo = @IdTramo
                                  AND IdBodega = @IdBodega 
                                  AND Nivel = @Nivel
                                  AND Indice_X = @Columna
                                  AND IdStock <> 0 "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@Columna", pColumna)
                lDTA.SelectCommand.Parameters.AddWithValue("@Nivel", pNivel)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lReturnList As New List(Of USUbicSingle)
                    Dim BeUbicSinge As USUbicSingle

                    For Each lRow As DataRow In lDataTable.Rows

                        BeUbicSinge = New USUbicSingle
                        BeUbicSinge.IdUbicacion = lRow.Item("IdUbicacion")

                        BeUbicSinge.Orientacion = IIf(IsDBNull(lRow.Item("orientacion_pos")), "0", lRow.Item("orientacion_pos"))
                        BeUbicSinge.Bloqueada = IIf(IsDBNull(lRow.Item("bloqueada")), False, lRow.Item("bloqueada"))
                        BeUbicSinge.Acepta_Pallet = IIf(IsDBNull(lRow.Item("acepta_pallet")), False, lRow.Item("acepta_pallet"))
                        BeUbicSinge.Ubicacion_Merma = IIf(IsDBNull(lRow.Item("ubicacion_merma")), False, lRow.Item("ubicacion_merma"))
                        BeUbicSinge.Dañado = IIf(IsDBNull(lRow.Item("dañado")), False, lRow.Item("dañado"))
                        lReturnList.Add(BeUbicSinge)

                    Next

                    Return lReturnList

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Get_Ubicaciones_Vacias_By_Columna_And_Nivel(ByVal pIdBodega As Integer,
                                                                        ByVal pIdTramo As Integer,
                                                                        ByVal pColumna As Integer,
                                                                        ByVal pNivel As Integer,
                                                                        ByVal lConnection As SqlConnection,
                                                                        ByVal lTransaction As SqlTransaction) As List(Of USUbicSingle)

        Get_Ubicaciones_Vacias_By_Columna_And_Nivel = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_Ubicaciones_Tramo_Disponibles
                                  WHERE IdTramo = @IdTramo
                                  AND IdBodega = @IdBodega 
                                  AND Nivel = @Nivel
                                  AND Indice_X = @Columna
                                  AND IdStock =0 "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@Columna", pColumna)
                lDTA.SelectCommand.Parameters.AddWithValue("@Nivel", pNivel)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim Obj As New USUbicSingle
                    Dim lReturnList As New List(Of USUbicSingle)
                    Dim vIdUbicacion As Integer = 0

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New USUbicSingle
                        Obj.IdUbicacion = lRow.Item("IdUbicacion")
                        Obj.Orientacion = lRow.Item("orientacion_pos")
                        Obj.Bloqueada = IIf(IsDBNull(lRow.Item("bloqueada")), False, lRow.Item("bloqueada"))
                        Obj.Acepta_Pallet = IIf(IsDBNull(lRow.Item("acepta_pallet")), False, lRow.Item("acepta_pallet"))
                        Obj.Ubicacion_Merma = IIf(IsDBNull(lRow.Item("ubicacion_merma")), False, lRow.Item("ubicacion_merma"))
                        Obj.Dañado = IIf(IsDBNull(lRow.Item("dañado")), False, lRow.Item("dañado"))
                        lReturnList.Add(Obj)

                    Next

                    Return lReturnList

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function Get_Ubicaciones_Vacias_By_IdBodega_And_IdTramo(ByVal pIdBodega As Integer,
                                                                           ByVal pIdTramo As Integer,
                                                                           ByVal lConnection As SqlConnection,
                                                                           ByVal lTransaction As SqlTransaction) As List(Of clsBeBodega_ubicacion)

        Get_Ubicaciones_Vacias_By_IdBodega_And_IdTramo = Nothing

        Try

            Dim vSQL As String = "SELECT * FROM VW_Ubicaciones_Tramo_Disponibles
                                  WHERE IdTramo = @IdTramo
                                  AND IdBodega = @IdBodega 
                                  AND IdStock =0"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim Obj As New clsBeBodega_ubicacion
                    Dim lReturnList As New List(Of clsBeBodega_ubicacion)

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeBodega_ubicacion
                        clsLnBodega_ubicacion.Cargar(Obj, lRow, lTransaction, lConnection)
                        lReturnList.Add(Obj)

                    Next

                    Return lReturnList

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC20200422: Busco obtener las columnas y niveles ocupadasd por el producto
    ''' Tomando en cuenta que deben de cumplir algunas de las características 
    ''' que hicieron coincidir el tramo, pej. Lote, Vence, UmBas, Pres, Estado
    ''' El objetivo es identificar las posiciones vacías mas cercanas al "mismo" producto.
    ''' </summary>
    ''' <param name="pIdBodega"></param>
    ''' <param name="pIdTramo"></param>
    ''' <param name="pFitParams"></param>
    ''' <param name="Lote"></param>
    ''' <param name="FechaVence"></param>
    ''' <param name="IdUmBas"></param>
    ''' <param name="IdPres"></param>
    ''' <param name="IdProductoEstado"></param>
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    ''' <returns></returns>
    Private Shared Function Get_Niveles_Y_Columnas_Ocupadas_By_IdBodega_And_IdTramo(ByVal pIdBodega As Integer,
                                                                                    ByVal pIdTramo As Integer,
                                                                                    ByVal pFitParams As clsBeTramosUSStage1,
                                                                                    ByVal Lote As String,
                                                                                    ByVal FechaVence As Date,
                                                                                    ByVal IdUmBas As Integer,
                                                                                    ByVal IdPres As Integer,
                                                                                    ByVal IdProductoEstado As Integer,
                                                                                    ByVal lConnection As SqlConnection,
                                                                                    ByVal lTransaction As SqlTransaction) As List(Of USUbicStrucStage1)

        Get_Niveles_Y_Columnas_Ocupadas_By_IdBodega_And_IdTramo = Nothing

        Try

            If Lote Is Nothing Then Lote = ""

            Dim vSQL As String = "SELECT DISTINCT 
                                  Ubicacion_Nivel, Ubicacion_Indice_X 
                                  FROM VW_STOCK_RES
                                  WHERE IdTramo = @IdTramo
                                  AND IdBodega = @IdBodega 
                                  AND IdStock <> 0 "

            If pFitParams.Lote.ContieneLote Then

                vSQL += "AND Lote = @Lote "

                If pFitParams.Lote.ContieneEstado AndAlso (Lote <> "") Then
                    vSQL += "AND IdProductoEstado = @IdProductoEstado "
                End If

                If pFitParams.Lote.ContienePresentacion AndAlso (IdPres <> 0) Then
                    vSQL += "AND IdPresentacion = @IdPresentacion "
                End If

                If pFitParams.Lote.ContieneUMBas Then
                    vSQL += "AND IdUnidadMedida = @IdUnidadMedida "
                End If

                If pFitParams.Lote.ContieneVencimiento Then
                    vSQL += "AND Fecha_Vence BETWEEN @D1 AND @D2 "
                End If

            End If

            If pFitParams.Vencimiento.ContieneVencimiento Then

                If pFitParams.Vencimiento.ContieneEstado AndAlso (Lote <> "") Then
                    vSQL += "AND IdProductoEstado = @IdProductoEstado "
                End If

                If pFitParams.Vencimiento.ContienePresentacion AndAlso (IdPres <> 0) Then
                    vSQL += "AND IdPresentacion = @IdPresentacion "
                End If

                If pFitParams.Vencimiento.ContieneUMBas Then
                    vSQL += "AND IdUnidadMedida = @IdUnidadMedida "
                End If

                If pFitParams.Vencimiento.ContieneVencimiento Then
                    vSQL += "AND Fecha_Vence BETWEEN @D1 AND @D2 "
                End If

            End If

            If pFitParams.Estado.ContieneEstado Then

                If pFitParams.Estado.ContieneEstado Then
                    vSQL += "AND IdProductoEstado = @IdProductoEstado "
                End If

                If pFitParams.Estado.ContienePresentacion AndAlso (IdPres <> 0) Then
                    vSQL += "AND IdPresentacion = @IdPresentacion "
                End If

                If pFitParams.Estado.ContieneUMBas Then
                    vSQL += "AND IdUnidadMedida = @IdUnidadMedida "
                End If

                If pFitParams.Estado.ContieneVencimiento Then
                    vSQL += "AND Fecha_Vence BETWEEN @D1 AND @D2 "
                End If

            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", pIdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                If (Lote <> "") Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@Lote", Lote)
                End If

                lDTA.SelectCommand.Parameters.AddWithValue("@D1", FechaVence.AddDays(MIN_FECHA_VENCE))
                lDTA.SelectCommand.Parameters.AddWithValue("@D2", FechaVence.AddDays(MAX_FECHA_VENCE))

                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", IdUmBas)

                If (IdPres <> 0) Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", IdPres)
                End If

                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", IdProductoEstado)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim Obj As New USUbicStrucStage1
                    Dim lReturnList As New List(Of USUbicStrucStage1)

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New USUbicStrucStage1
                        Obj.Nivel = lRow.Item("Ubicacion_Nivel")
                        Obj.Columna = lRow.Item("Ubicacion_Indice_X")
                        lReturnList.Add(Obj)

                    Next

                    Return lReturnList

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Private Shared Function GenerateCompositeKey(keys As List(Of String)) As String
        Return String.Join("-", keys)
    End Function

End Class
