Imports System.Data.SqlClient
Imports System.Threading.Tasks
Public Class clsLnUbicacionSugerida
    Public Shared Function Get_Licencia_For_US(ByVal Licencia As String) As clsBePalletUS

        Dim LicenciaMixtaONo As New clsBePalletUS
        LicenciaMixtaONo.Productos = New List(Of clsBeProductoUS)

        Dim query As String = "SELECT TOP(1)
                                      IdBodega,
                                      IdProducto, 
                                      codigo, 
                                      Lote, 
                                      fecha_vence, 
                                      Cantidad, 
                                      IdProductoEstado, 
                                      IndiceRotacion,
                                      IdTramo,
                                      IdTipoProducto,
                                      IdUnidadMedida,
                                      IdPresentacion,
                                      IdUbicacion
                                         FROM VW_Stock_Res
										    WHERE lic_plate = @Licencia ORDER BY Cantidad DESC "

        Try

            Using connection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                connection.Open()

                Using transaction As SqlTransaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using command As New SqlCommand(query, connection, transaction)

                        command.Parameters.AddWithValue("@Licencia", Licencia)

                        Using reader As SqlDataReader = command.ExecuteReader()

                            While reader.Read()

                                Dim producto As New clsBeProductoUS
                                producto.IdProducto = IIf(IsDBNull(reader("IdProducto").ToString()), 0, reader("IdProducto").ToString())
                                producto.CodigoProducto = IIf(IsDBNull(reader("codigo").ToString()), "", reader("codigo").ToString())
                                producto.Lote = IIf(IsDBNull(reader("Lote").ToString()), "", reader("Lote").ToString())
                                producto.Fecha_Vence = IIf(IsDBNull(reader("fecha_vence")), New Date(1900, 1, 1), reader("fecha_vence"))
                                producto.Cantidad = IIf(IsDBNull(reader("Cantidad")), 0, reader("Cantidad"))
                                producto.IdProductoEstado = IIf(IsDBNull(reader("IdProductoEstado")), 0, reader("IdProductoEstado"))
                                producto.IdBodega = IIf(IsDBNull(reader("IdBodega")), 0, reader("IdBodega"))
                                producto.IdTramo = IIf(IsDBNull(reader("IdTramo")), 0, reader("IdTramo"))
                                producto.IdTipoProducto = IIf(IsDBNull(reader("IdTipoProducto")), 0, reader("IdTipoProducto"))
                                producto.IdUnidadMedida = IIf(IsDBNull(reader("IdUnidadMedida")), 0, reader("IdUnidadMedida"))
                                producto.IdPresentacion = IIf(IsDBNull(reader("IdPresentacion")), 0, reader("IdPresentacion"))
                                producto.IdUbicacion = IIf(IsDBNull(reader("IdUbicacion")), 0, reader("IdUbicacion"))
                                LicenciaMixtaONo.Productos.Add(producto)

                            End While

                        End Using

                    End Using

                    transaction.Commit()

                End Using

            End Using

        Catch ex As Exception
            Throw
        End Try

        Return LicenciaMixtaONo

    End Function

    Public Shared Function Get_Ubicaciones(ByVal BePallet As clsBePalletUS) As (ubicacionesOcupadas As List(Of clsBeUbicacionUS), ubicacionesVacias As List(Of clsBeUbicacionUS))

        Dim ubicacionesOcupadas As New List(Of clsBeUbicacionUS)
        Dim ubicacionesVacias As New List(Of clsBeUbicacionUS)

        Dim query As String = "SELECT idbodega, 
                                        idtramo, 
                                        idubicacion, 
                                        Ubicacion_Nivel as Nivel, 
                                        Ubicacion_Indice_X as Columna, 
                                        sum(cantidad) as Cantidad,
                                        dbo.Nombre_Completo_Ubicacion(idubicacion,idbodega) as Ubicacion, 
                                        IdProducto,
                                        codigo as CodigoProducto,
                                        Nombre,
                                        Lote,
                                        fecha_vence as Vence,
                                        IdProductoEstado,
                                        IdTipoProducto,
                                        IdUnidadMedida,
                                        IdPresentacion
                               FROM VW_Stock_Res_US
                               WHERE IdBodega = @IdBodega
                               GROUP BY
                                        idtramo, 
                                        Ubicacion_Indice_X, 
                                        Ubicacion_Nivel, 
                                        idubicacion, 
                                        dbo.Nombre_Completo_Ubicacion(idubicacion,idbodega), 
                                        IdProducto,
                                        codigo,
                                        Nombre,
                                        Lote,
                                        fecha_vence,
                                        IdProductoEstado,
                                        idbodega,
                                        IdTipoProducto,
                                        IdUnidadMedida,
                                        IdPresentacion
                               ORDER BY idtramo, 
                                        Ubicacion_Indice_X, 
                                        Ubicacion_Nivel, 
                                        idubicacion "

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using command As New SqlCommand(query, lConnection, lTransaction)

                        command.Parameters.AddWithValue("@IdBodega", BePallet.Productos.FirstOrDefault.IdBodega)

                        Using reader As SqlDataReader = command.ExecuteReader()

                            Dim ubicacionDict As New Dictionary(Of Integer, clsBeUbicacionUS)

                            While reader.Read()

                                Dim idUbicacion As Integer = Convert.ToInt32(reader("IdUbicacion"))

                                Try

                                    ' Si la ubicación aún no se ha añadido al diccionario, crearla
                                    If Not ubicacionDict.ContainsKey(idUbicacion) Then

                                        Dim ubicacion As New clsBeUbicacionUS
                                        ubicacion.IdBodega = If(IsDBNull(reader("IdBodega")), 0, Convert.ToInt32(reader("IdBodega")))
                                        ubicacion.IdTramo = If(IsDBNull(reader("IdTramo")), 0, Convert.ToInt32(reader("IdTramo")))
                                        ubicacion.IdUbicacion = idUbicacion
                                        ubicacion.Nivel = If(IsDBNull(reader("Nivel")), 0, Convert.ToInt32(reader("Nivel")))
                                        ubicacion.Columna = If(IsDBNull(reader("Columna")), 0, Convert.ToInt32(reader("Columna")))
                                        ubicacion.CapacidadRestante = If(IsDBNull(reader("Cantidad")), 0, Convert.ToInt32(reader("Cantidad")))
                                        ubicacion.Productos = New List(Of clsBeProductoUS)()
                                        ubicacion.IdTipoProducto = If(IsDBNull(reader("IdTipoProducto")), 0, Convert.ToInt32(reader("IdTipoProducto")))
                                        ubicacion.IdUnidadMedida = If(IsDBNull(reader("IdUnidadMedida")), 0, reader("IdUnidadMedida"))
                                        ubicacion.IdPresentacion = If(IsDBNull(reader("IdPresentacion")), 0, reader("IdPresentacion"))
                                        ubicacionDict.Add(idUbicacion, ubicacion)

                                    End If

                                Catch ex As Exception
                                    Throw
                                End Try

                                Try

                                    ' Añadir el producto a la ubicación si IdStock no es nulo
                                    If Not IsDBNull(reader("Cantidad")) AndAlso Convert.ToDouble(reader("Cantidad")) > 0 Then

                                        Dim producto As New clsBeProductoUS
                                        producto.IdProducto = If(IsDBNull(reader("IdProducto")), 0, Convert.ToInt32(reader("IdProducto")))
                                        producto.CodigoProducto = If(IsDBNull(reader("CodigoProducto")), "", reader("CodigoProducto").ToString())
                                        producto.Lote = If(IsDBNull(reader("Lote")), "", reader("Lote").ToString())
                                        producto.Fecha_Vence = If(IsDBNull(reader("Vence")), Date.MinValue, Convert.ToDateTime(reader("Vence")))
                                        producto.Cantidad = If(IsDBNull(reader("Cantidad")), 0, (reader("Cantidad")))
                                        producto.IdProductoEstado = If(IsDBNull(reader("IdProductoEstado")), 0, Convert.ToInt32(reader("IdProductoEstado")))
                                        producto.IndiceRotacion = 0 ' Si se requiere, puede calcularse en otro proceso
                                        producto.IdBodega = If(IsDBNull(reader("IdBodega")), 0, Convert.ToInt32(reader("IdBodega")))
                                        producto.IdTramo = If(IsDBNull(reader("IdTramo")), 0, Convert.ToInt32(reader("IdTramo")))
                                        producto.IdTipoProducto = If(IsDBNull(reader("IdTipoProducto")), 0, Convert.ToInt32(reader("IdTipoProducto")))
                                        producto.IdUnidadMedida = If(IsDBNull(reader("IdUnidadMedida")), 0, reader("IdUnidadMedida"))
                                        producto.IdPresentacion = If(IsDBNull(reader("IdPresentacion")), 0, reader("IdPresentacion"))
                                        ubicacionDict(idUbicacion).Productos.Add(producto)

                                    End If

                                Catch ex As Exception
                                    Throw
                                End Try

                            End While

                            ' Separar ubicaciones ocupadas y vacías
                            For Each ubicacion In ubicacionDict.Values
                                If ubicacion.Productos.Count > 0 Then
                                    ubicacionesOcupadas.Add(ubicacion)
                                Else
                                    ubicacionesVacias.Add(ubicacion)
                                End If
                            Next

                        End Using

                        '#EJC20240816: Buscar en las reglas de ubicación las ubicaciones disponibles para ese producto.
                        'si existen.

                        Dim vIdProducto As Integer = BePallet.Productos.FirstOrDefault.IdProducto
                        Dim vIdBodega As Integer = BePallet.Productos.FirstOrDefault.IdBodega
                        Dim vIdPresentacion As Integer = BePallet.Productos.FirstOrDefault.IdPresentacion
                        Dim vIdProductoEstado As Integer = BePallet.Productos.FirstOrDefault.IdProductoEstado
                        Dim vLote As String = BePallet.Productos.FirstOrDefault.Lote
                        Dim vIdUbicacion As Integer = BePallet.Productos.FirstOrDefault.IdUbicacion

                        clsLnTrans_ubicsug.pIdBodega = vIdBodega
                        clsLnTrans_ubicsug.pBeProducto = clsLnProducto.GetSingle(vIdProducto, vIdBodega, lConnection, lTransaction)
                        clsLnTrans_ubicsug.pBePresentacion = clsLnProducto_presentacion.GetSingle(vIdPresentacion, lConnection, lTransaction)
                        clsLnTrans_ubicsug.pBeEstado = clsLnProducto_estado.GetSingle(vIdProductoEstado, lConnection, lTransaction)
                        clsLnTrans_ubicsug.pLote = vLote

                        If clsLnTrans_ubicsug.Get_Ubicaciones_Sugeridas(1, vIdUbicacion) Then

                            If clsLnTrans_ubicsug.lUbicacionesSugeridas.Count > 0 Then

                                Dim lUbicFinal As New List(Of clsBeUbicacionSugeridaFinal)
                                Dim sUbic As clsBeUbicacionSugeridaFinal
                                Parallel.ForEach(clsLnTrans_ubicsug.lUbicacionesSugeridas, Sub(ByVal Ubic)
                                                                                               SyncLock lUbicFinal
                                                                                                   sUbic = New clsBeUbicacionSugeridaFinal()
                                                                                                   sUbic.IdUbicacion = Ubic.IdUbicacion
                                                                                                   sUbic.Descripcion = Ubic.Descripcion
                                                                                                   sUbic.Tramo = Ubic.Tramo
                                                                                                   sUbic.Nivel = Ubic.Nivel
                                                                                                   sUbic.Cant_Ubicar = Ubic.Cant_Ubicar
                                                                                                   lUbicFinal.Add(sUbic)
                                                                                               End SyncLock
                                                                                           End Sub)

                            End If

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

            End Using

        Catch ex As Exception
            Throw
        End Try

        Return (ubicacionesOcupadas, ubicacionesVacias)

    End Function

    ' Función para calcular la distancia entre un pallet y una ubicación
    Public Shared Function Calcular_Distancia(ByRef ubicacion As clsBeUbicacionUS, ByRef pallet As clsBePalletUS) As Double

        Dim distancia As Double = 0

        Try

            If pallet.Productos.Count > 0 Then

                For Each productoPallet In pallet.Productos

                    Dim distanciaProductoMinima As Double = Double.MaxValue

                    ' Comparar el producto en el pallet con los productos en la ubicación
                    For Each productoUbicacion In ubicacion.Productos

                        Dim distanciaProducto As Double = 0

                        ' Penalización por diferencia en Código de Producto
                        If productoUbicacion.CodigoProducto <> productoPallet.CodigoProducto Then
                            distanciaProducto += 1
                        End If

                        ' Penalización por diferencia en Lote
                        If productoUbicacion.Lote <> productoPallet.Lote Then
                            distanciaProducto += 1
                        End If

                        ' Penalización por diferencia en Fecha de Vencimiento
                        distanciaProducto += Math.Abs((productoUbicacion.Fecha_Vence - productoPallet.Fecha_Vence).Days)

                        ' Penalización por diferencia en Estado del Producto
                        If productoUbicacion.IdProductoEstado <> productoPallet.IdProductoEstado Then
                            distanciaProducto += 1
                        End If

                        ' Penalización por diferencia en Tramo (IdTramo)
                        If ubicacion.IdTramo <> productoUbicacion.IdTramo Then
                            distanciaProducto += 100 ' Aumenta la penalización para probar el efecto
                        End If

                        ' Penalización por diferencia en Tipo Producto (IdTipoProducto)
                        If ubicacion.IdTipoProducto <> productoUbicacion.IdTipoProducto Then
                            distanciaProducto += 100 ' Aumenta la penalización para probar el efecto
                        End If

                        ' Penalización por diferencia en Tramo.
                        If ubicacion.IdTramo <> productoUbicacion.IdTramo Then
                            distanciaProducto += 100 ' Aumenta la penalización para probar el efecto
                        End If

                        ' Penalización por diferencia en Índice de Rotación y proximidad a la salida
                        distanciaProducto += Math.Abs(ubicacion.IdUbicacion - productoPallet.IndiceRotacion)

                        ' Imprimir la distancia calculada para depuración
                        Console.WriteLine($"Distancia calculada para el producto {productoPallet.CodigoProducto}: {distanciaProducto}")

                        ' Buscar la mínima distancia
                        distanciaProductoMinima = Math.Min(distanciaProductoMinima, distanciaProducto)

                        productoUbicacion.Distancia = distanciaProductoMinima
                        ubicacion.Distancia = distanciaProductoMinima

                        If productoUbicacion.IdUbicacion = 21 OrElse productoUbicacion.IdUbicacion = 25 OrElse productoUbicacion.IdUbicacion = 202 Then
                            Debug.Write("espera")
                        End If

                    Next

                    ' Acumular la mínima distancia encontrada
                    distancia += distanciaProductoMinima

                    productoPallet.Distancia = distanciaProductoMinima

                Next

            End If

            ' Considerar si la capacidad restante es suficiente
            Dim cantidadTotalPallet = pallet.Productos.Sum(Function(p) p.Cantidad)
            If ubicacion.CapacidadRestante < cantidadTotalPallet Then
                distancia += 1000 ' Penalización alta si no hay suficiente espacio
            End If

            ' Imprimir la distancia final calculada para la ubicación
            Console.WriteLine($"Distancia total calculada para la ubicación {ubicacion.IdUbicacion}: {distancia}")

        Catch ex As Exception
            Console.WriteLine("Error al calcular distancias: " & ex.Message)
        End Try

        Return distancia

    End Function

    Public Shared Function Sugerir_Ubicaciones(ByVal Licencia As String, k As Integer) As List(Of clsBeUbicacionUS)

        Dim ubicacionesSugeridas As New List(Of clsBeUbicacionUS)
        Dim clsTrans As New clsTransaccion
        Dim lUbicacionesRegla As New List(Of clsBeVW_ubicaciones_por_regla)
        Dim clUbicacionesRegla As New List(Of clsBeVW_ubicaciones_por_regla)
        Dim vTipoProductoDefinidoPorRegla As Boolean = True

        Sugerir_Ubicaciones = Nothing

        Try

            clsTrans.Begin_Transaction()

            ' Obtener el pallet asociado a la licencia
            Dim BePallet As New clsBePalletUS
            BePallet = Get_Licencia_For_US(Licencia, clsTrans.lConnection, clsTrans.lTransaction)

            If Not BePallet Is Nothing Then
                If BePallet.Productos.Count = 0 Then
                    Exit Function
                End If
            End If

            ' Obtener las listas de ubicaciones ocupadas y vacías
            Dim result = Get_Ubicaciones(BePallet, clsTrans.lConnection, clsTrans.lTransaction)
            Dim ubicacionesOcupadas As List(Of clsBeUbicacionUS) = result.ubicacionesOcupadas
            Dim ubicacionesVacias As List(Of clsBeUbicacionUS) = result.ubicacionesVacias

            ' Paso 1: Calcular distancias en las ubicaciones ocupadas y encontrar las más cercanas
            Dim distanciasOcupadas As New List(Of Tuple(Of clsBeUbicacionUS, Double))

            For Each ubicacionOcupada In ubicacionesOcupadas
                Dim distancia = Calcular_Distancia(ubicacionOcupada, BePallet)
                distanciasOcupadas.Add(New Tuple(Of clsBeUbicacionUS, Double)(ubicacionOcupada, distancia))
            Next

            ubicacionesOcupadas = ubicacionesOcupadas.OrderBy(Function(x) x.Distancia).ToList()

            ubicacionesSugeridas = ubicacionesOcupadas.Take(k).ToList()

            ' Paso 2: Encontrar ubicaciones vacías cercanas a las ubicaciones ocupadas seleccionadas
            Dim distanciasVacias As New List(Of Tuple(Of clsBeUbicacionUS, Double))

            ' Filtrar las ubicaciones vacías para considerar solo aquellas con nivel > 0
            Dim ubicacionesVaciasFiltradas = ubicacionesVacias.Where(Function(x) x.Nivel > 0 AndAlso x.Columna > 0).Distinct.ToList()

            lUbicacionesRegla = clsLnVW_ubicaciones_por_regla.Get_All(BePallet.Productos.FirstOrDefault.IdBodega, clsTrans.lConnection, clsTrans.lTransaction)
            'Se obtuvieron ubicaciones definidas para la bodega.                        

            If lUbicacionesRegla.Count > 0 Then
                'Buscar si existen ubicaciones que coincidan con el tipo de producto y el indice de rotación
                lUbicacionesRegla = lUbicacionesRegla.Where(Function(x) x.IdTipoProducto = BePallet.Productos.FirstOrDefault.IdTipoProducto AndAlso
                                                                x.IdIndiceRotacion = BePallet.Productos.FirstOrDefault.IndiceRotacion).ToList()

                If lUbicacionesRegla.Count > 0 Then
                    ' Filtrar las ubicaciones vacías para mantener solo las que están en lUbicacionesRegla
                    Dim ubicacionesReglaIds = lUbicacionesRegla _
                .Where(Function(x) x.IdTipoProducto = BePallet.Productos.FirstOrDefault.IdTipoProducto AndAlso
                x.IdIndiceRotacion = BePallet.Productos.FirstOrDefault.IndiceRotacion) _
                .Select(Function(x) x.IdUbicacion).Distinct().ToList()

                    ' Mantener solo las ubicaciones vacías que están en lUbicacionesRegla
                    ubicacionesVaciasFiltradas = ubicacionesVaciasFiltradas _
                .Where(Function(x) ubicacionesReglaIds.Contains(x.IdUbicacion)).ToList()
                Else
                    lUbicacionesRegla = clsLnVW_ubicaciones_por_regla.Get_All(BePallet.Productos.FirstOrDefault.IdBodega, clsTrans.lConnection, clsTrans.lTransaction)
                    vTipoProductoDefinidoPorRegla = False
                End If

            End If

            '#EJC20240917: No se obtuvieron ubicaciones vacías filtradas en base a una regla, retomar los vecinos mas cercanos y recalcular distancia en base a ellas.
            If ubicacionesVaciasFiltradas.Count = 0 Then
                ' Filtrar las ubicaciones vacías para considerar solo aquellas con nivel > 0
                ubicacionesVaciasFiltradas = ubicacionesVacias.Where(Function(x) x.Nivel > 0 AndAlso x.Columna > 0).Distinct.ToList()
            End If

            '#EJC20241001: No se encontró el tipo de producto definido, retornar una ubicación que no pertenezca a otra regla pero que esté vacía.
            If Not vTipoProductoDefinidoPorRegla Then

                ' Crear una lista con los IdUbicacion de las ubicaciones en lUbicacionesRegla
                Dim ubicacionesReglaIdsExcept As List(Of Integer) = lUbicacionesRegla.Select(Function(r) r.IdUbicacion).ToList()

                ' Excluir las ubicaciones vacías que están en lUbicacionesRegla basadas en el IdUbicacion
                Dim lUbicacionesVaciasExceptReglas As New List(Of clsBeUbicacionUS)
                lUbicacionesVaciasExceptReglas = ubicacionesVaciasFiltradas.Where(Function(x) Not ubicacionesReglaIdsExcept.Contains(x.IdUbicacion)).ToList()

                ubicacionesVaciasFiltradas = lUbicacionesVaciasExceptReglas

            End If

            '#EJC20241001: tomar solo las primeras 100 posiciones vacías
            If ubicacionesVaciasFiltradas.Count > 100 Then
                ubicacionesVaciasFiltradas = ubicacionesVaciasFiltradas.Take(100).ToList()
            End If

            If ubicacionesOcupadas.Count > 0 Then
                ubicacionesOcupadas = ubicacionesOcupadas.Where(Function(x) x.IdTipoProducto = BePallet.Productos.FirstOrDefault.IdTipoProducto).ToList()
            End If

            For Each ubicacionSugerida In ubicacionesOcupadas
                For Each ubicacionVacia In ubicacionesVaciasFiltradas
                    ' Calcular distancia entre la ubicación vacía y la ubicación ocupada sugerida
                    Dim distancia = Calcular_Distancia_Entre_Ubicaciones(ubicacionVacia, ubicacionSugerida)
                    ' Asignar una prioridad mayor a las ubicaciones que están en lUbicacionesRegla
                    Dim prioridad = If(lUbicacionesRegla.Any(Function(r) r.IdUbicacion = ubicacionVacia.IdUbicacion), -10, 0)
                    distanciasVacias.Add(New Tuple(Of clsBeUbicacionUS, Double)(ubicacionVacia, distancia + prioridad))
                Next
            Next

            ' Ordenar las ubicaciones vacías por distancia (ascendente)
            distanciasVacias.Sort(Function(x, y) x.Item2.CompareTo(y.Item2))

            ' Limitar las ubicaciones vacías a las k más cercanas
            Dim ubicacionesVaciasMasCercanas As List(Of clsBeUbicacionUS) = distanciasVacias.Select(Function(x) x.Item1).OrderByDescending(Function(x) x.Distancia).Distinct().ToList()

            ' Reemplazar las ubicaciones ocupadas sugeridas con las ubicaciones vacías más cercanas
            ubicacionesSugeridas = ubicacionesVaciasMasCercanas.OrderBy(Function(x) x.IdTramo).ThenBy(Function(y) y.Columna).ThenBy(Function(z) z.Nivel).ThenBy(Function(m) m.IdUbicacion).Take(k).ToList()

            clsTrans.Commit_Transaction()

            Return ubicacionesSugeridas

        Catch ex As Exception
            clsTrans.RollBack_Transaction()
            Throw
        Finally
            clsTrans.Close_Conection()
        End Try

    End Function


    Public Shared Function Calcular_Distancia_Entre_Ubicaciones(ByRef ubicacion1 As clsBeUbicacionUS, ByRef ubicacion2 As clsBeUbicacionUS) As Double

        Dim distancia As Double = 0

        Try
            ' Penalización por diferencia en Tramo (IdTramo)
            If ubicacion1.IdTramo <> ubicacion2.IdTramo Then
                distancia += 10 ' Penalización alta si están en tramos diferentes
            End If

            ' Penalización por diferencia en Nivel
            distancia += Math.Abs(ubicacion1.Nivel - ubicacion2.Nivel)

            ' Penalización por diferencia en Columna
            distancia += Math.Abs(ubicacion1.Columna - ubicacion2.Columna)

            ubicacion1.Distancia = distancia

            If ubicacion1.IdUbicacion = 21 OrElse ubicacion1.IdUbicacion = 25 OrElse ubicacion1.IdUbicacion = 202 Then
                Debug.Write("espera")
            End If

        Catch ex As Exception
            Throw
        End Try

        Return distancia

    End Function

    Public Class UbicacionesResult
        Public Property UbicacionesOcupadas As List(Of clsBeUbicacionUS)
        Public Property UbicacionesVacias As List(Of clsBeUbicacionUS)

        Public Sub New()
            UbicacionesOcupadas = New List(Of clsBeUbicacionUS)
            UbicacionesVacias = New List(Of clsBeUbicacionUS)
        End Sub

    End Class

    Public Shared Function Get_Licencia_For_US(ByVal Licencia As String, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As clsBePalletUS

        Dim LicenciaMixtaONo As New clsBePalletUS
        LicenciaMixtaONo.Productos = New List(Of clsBeProductoUS)

        Dim query As String = "SELECT TOP(1)
                                      IdBodega,
                                      IdProducto, 
                                      codigo, 
                                      Lote, 
                                      fecha_vence, 
                                      Cantidad, 
                                      IdProductoEstado, 
                                      IndiceRotacion,
                                      IdTramo,
                                      IdTipoProducto,
                                      IdUnidadMedida,
                                      IdPresentacion,
                                      IdUbicacion,
                                      IdIndiceRotacion
                                         FROM VW_Stock_Res
										    WHERE lic_plate = @Licencia ORDER BY Cantidad DESC "

        Try

            Using command As New SqlCommand(query, lConnection, lTransaction)

                command.Parameters.AddWithValue("@Licencia", Licencia)

                Using reader As SqlDataReader = command.ExecuteReader()

                    While reader.Read()

                        Dim producto As New clsBeProductoUS
                        producto.IdProducto = IIf(IsDBNull(reader("IdProducto").ToString()), 0, reader("IdProducto").ToString())
                        producto.CodigoProducto = IIf(IsDBNull(reader("codigo").ToString()), "", reader("codigo").ToString())
                        producto.Lote = IIf(IsDBNull(reader("Lote").ToString()), "", reader("Lote").ToString())
                        producto.Fecha_Vence = IIf(IsDBNull(reader("fecha_vence")), New Date(1900, 1, 1), reader("fecha_vence"))
                        producto.Cantidad = IIf(IsDBNull(reader("Cantidad")), 0, reader("Cantidad"))
                        producto.IdProductoEstado = IIf(IsDBNull(reader("IdProductoEstado")), 0, reader("IdProductoEstado"))
                        producto.IdBodega = IIf(IsDBNull(reader("IdBodega")), 0, reader("IdBodega"))
                        producto.IdTramo = IIf(IsDBNull(reader("IdTramo")), 0, reader("IdTramo"))
                        producto.IdTipoProducto = IIf(IsDBNull(reader("IdTipoProducto")), 0, reader("IdTipoProducto"))
                        producto.IdUnidadMedida = IIf(IsDBNull(reader("IdUnidadMedida")), 0, reader("IdUnidadMedida"))
                        producto.IdPresentacion = IIf(IsDBNull(reader("IdPresentacion")), 0, reader("IdPresentacion"))
                        producto.IdUbicacion = IIf(IsDBNull(reader("IdUbicacion")), 0, reader("IdUbicacion"))
                        producto.IndiceRotacion = IIf(IsDBNull(reader("IdIndiceRotacion")), 0, reader("IdIndiceRotacion"))
                        LicenciaMixtaONo.Productos.Add(producto)

                    End While

                End Using

            End Using

        Catch ex As Exception
            Throw
        End Try

        Return LicenciaMixtaONo

    End Function

    Public Shared Function Get_Ubicaciones(ByVal BePallet As clsBePalletUS,
                                           ByVal lConnection As SqlConnection,
                                           ByVal lTransaction As SqlTransaction) As _
                                           (ubicacionesOcupadas As List(Of clsBeUbicacionUS),
                                           ubicacionesVacias As List(Of clsBeUbicacionUS))

        Dim ubicacionesOcupadas As New List(Of clsBeUbicacionUS)
        Dim ubicacionesVacias As New List(Of clsBeUbicacionUS)

        Dim query As String = "SELECT idbodega, 
                                        idtramo, 
                                        idubicacion, 
                                        Ubicacion_Nivel as Nivel, 
                                        Ubicacion_Indice_X as Columna, 
                                        sum(cantidad) as Cantidad,
                                        dbo.Nombre_Completo_Ubicacion(idubicacion,idbodega) as Ubicacion, 
                                        IdProducto,
                                        codigo as CodigoProducto,
                                        Nombre,
                                        Lote,
                                        fecha_vence as Vence,
                                        IdProductoEstado,
                                        IdTipoProducto,
                                        IdUnidadMedida,
                                        IdPresentacion,
                                        IdIndiceRotacion,
                                        IdTipoRotacion
                               FROM VW_Stock_Res_US
                               WHERE IdBodega = @IdBodega
                               GROUP BY
                                        idtramo, 
                                        Ubicacion_Indice_X, 
                                        Ubicacion_Nivel, 
                                        idubicacion, 
                                        dbo.Nombre_Completo_Ubicacion(idubicacion,idbodega), 
                                        IdProducto,
                                        codigo,
                                        Nombre,
                                        Lote,
                                        fecha_vence,
                                        IdProductoEstado,
                                        idbodega,
                                        IdTipoProducto,
                                        IdUnidadMedida,
                                        IdPresentacion,
                                        IdIndiceRotacion,
                                        IdTipoRotacion
                               ORDER BY idtramo, 
                                        Ubicacion_Indice_X, 
                                        Ubicacion_Nivel, 
                                        idubicacion "

        Try

            Using command As New SqlCommand(query, lConnection, lTransaction)

                If BePallet.Productos IsNot Nothing AndAlso BePallet.Productos.Count > 0 Then

                    command.Parameters.AddWithValue("@IdBodega", BePallet.Productos.FirstOrDefault.IdBodega)

                    Using reader As SqlDataReader = command.ExecuteReader()

                        Dim ubicacionDict As New Dictionary(Of Integer, clsBeUbicacionUS)

                        While reader.Read()

                            Dim idUbicacion As Integer = Convert.ToInt32(reader("IdUbicacion"))

                            Try

                                ' Si la ubicación aún no se ha añadido al diccionario, crearla
                                If Not ubicacionDict.ContainsKey(idUbicacion) Then

                                    Dim ubicacion As New clsBeUbicacionUS
                                    ubicacion.IdBodega = If(IsDBNull(reader("IdBodega")), 0, Convert.ToInt32(reader("IdBodega")))
                                    ubicacion.IdTramo = If(IsDBNull(reader("IdTramo")), 0, Convert.ToInt32(reader("IdTramo")))
                                    ubicacion.IdUbicacion = idUbicacion
                                    ubicacion.Nivel = If(IsDBNull(reader("Nivel")), 0, Convert.ToInt32(reader("Nivel")))
                                    ubicacion.Columna = If(IsDBNull(reader("Columna")), 0, Convert.ToInt32(reader("Columna")))
                                    ubicacion.CapacidadRestante = If(IsDBNull(reader("Cantidad")), 0, Convert.ToDouble(reader("Cantidad")))
                                    ubicacion.Productos = New List(Of clsBeProductoUS)()
                                    ubicacion.IdTipoProducto = If(IsDBNull(reader("IdTipoProducto")), 0, Convert.ToInt32(reader("IdTipoProducto")))
                                    ubicacion.IdUnidadMedida = If(IsDBNull(reader("IdUnidadMedida")), 0, reader("IdUnidadMedida"))
                                    ubicacion.IdPresentacion = If(IsDBNull(reader("IdPresentacion")), 0, reader("IdPresentacion"))
                                    ubicacionDict.Add(idUbicacion, ubicacion)

                                End If

                            Catch ex As Exception
                                Throw
                            End Try

                            Try

                                ' Añadir el producto a la ubicación si IdStock no es nulo
                                If Not IsDBNull(reader("Cantidad")) AndAlso Convert.ToDouble(reader("Cantidad")) > 0 Then

                                    Dim producto As New clsBeProductoUS
                                    producto.IdProducto = If(IsDBNull(reader("IdProducto")), 0, Convert.ToInt32(reader("IdProducto")))
                                    producto.CodigoProducto = If(IsDBNull(reader("CodigoProducto")), "", reader("CodigoProducto").ToString())
                                    producto.Lote = If(IsDBNull(reader("Lote")), "", reader("Lote").ToString())
                                    producto.Fecha_Vence = If(IsDBNull(reader("Vence")), Date.MinValue, Convert.ToDateTime(reader("Vence")))
                                    producto.Cantidad = If(IsDBNull(reader("Cantidad")), 0, (reader("Cantidad")))
                                    producto.IdProductoEstado = If(IsDBNull(reader("IdProductoEstado")), 0, Convert.ToInt32(reader("IdProductoEstado")))
                                    producto.IndiceRotacion = If(IsDBNull(reader("IdIndiceRotacion")), 0, Convert.ToInt32(reader("IdIndiceRotacion")))
                                    producto.IdTipoRotacion = If(IsDBNull(reader("IdTipoRotacion")), 0, Convert.ToInt32(reader("IdTipoRotacion")))
                                    producto.IdBodega = If(IsDBNull(reader("IdBodega")), 0, Convert.ToInt32(reader("IdBodega")))
                                    producto.IdTramo = If(IsDBNull(reader("IdTramo")), 0, Convert.ToInt32(reader("IdTramo")))
                                    producto.IdTipoProducto = If(IsDBNull(reader("IdTipoProducto")), 0, Convert.ToInt32(reader("IdTipoProducto")))
                                    producto.IdUnidadMedida = If(IsDBNull(reader("IdUnidadMedida")), 0, reader("IdUnidadMedida"))
                                    producto.IdPresentacion = If(IsDBNull(reader("IdPresentacion")), 0, reader("IdPresentacion"))
                                    ubicacionDict(idUbicacion).Productos.Add(producto)

                                End If

                            Catch ex As Exception
                                Throw
                            End Try

                        End While

                        ' Separar ubicaciones ocupadas y vacías
                        For Each ubicacion In ubicacionDict.Values
                            If ubicacion.Productos.Count > 0 Then
                                ubicacionesOcupadas.Add(ubicacion)
                            Else
                                ubicacionesVacias.Add(ubicacion)
                            End If
                        Next

                    End Using

                    '#EJC20240816: Buscar en las reglas de ubicación las ubicaciones disponibles para ese producto.
                    'si existen.

                End If

            End Using

        Catch ex As Exception
            Throw
        End Try

        Return (ubicacionesOcupadas, ubicacionesVacias)

    End Function

End Class