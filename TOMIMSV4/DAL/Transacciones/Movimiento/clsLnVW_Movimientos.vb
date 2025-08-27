Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnVW_Movimientos

    Public Shared Sub Cargar(ByRef oBeVW_movimientos As clsBeVW_Movimientos, ByRef dr As DataRow)

        Try

            With oBeVW_movimientos

                If dr.Table.Columns.Contains("Propietario") Then .Propietario = IIf(IsDBNull(dr.Item("Propietario")), 0, dr.Item("Propietario"))
                If dr.Table.Columns.Contains("Poliza") Then .Poliza = IIf(IsDBNull(dr.Item("Poliza")), 0, dr.Item("Poliza"))
                If dr.Table.Columns.Contains("Producto") Then .Producto = IIf(IsDBNull(dr.Item("Producto")), 0, dr.Item("Producto"))
                If dr.Table.Columns.Contains("Presentacion") Then .Presentacion = IIf(IsDBNull(dr.Item("Presentación")), "", dr.Item("Presentación"))
                If dr.Table.Columns.Contains("EstadoOrigen") Then .EstadoOrigen = IIf(IsDBNull(dr.Item("EstadoOrigen")), 0, dr.Item("EstadoOrigen"))
                If dr.Table.Columns.Contains("EstadoDestino") Then .EstadoDestino = IIf(IsDBNull(dr.Item("EstadoDestino")), 0, dr.Item("EstadoDestino"))
                If dr.Table.Columns.Contains("UMBas") Then .UMBas = IIf(IsDBNull(dr.Item("UMBas")), 0, dr.Item("UMBas"))
                If dr.Table.Columns.Contains("Cantidad") Then .Cantidad = IIf(IsDBNull(dr.Item("Cantidad")), 0, dr.Item("Cantidad"))
                If dr.Table.Columns.Contains("Peso") Then .Peso = IIf(IsDBNull(dr.Item("Peso")), 0, dr.Item("Peso"))
                If dr.Table.Columns.Contains("Lote") Then .Lote = IIf(IsDBNull(dr.Item("lote")), 0, dr.Item("lote"))
                If dr.Table.Columns.Contains("Fecha_Vence") Then .Fecha_Vence = IIf(IsDBNull(dr.Item("Fecha_Vence")), New Date(1900, 1, 1), dr.Item("Fecha_Vence"))
                If dr.Table.Columns.Contains("UbicOrigen") Then .UbicOrigen = IIf(IsDBNull(dr.Item("UbicOrigen")), 0, dr.Item("UbicOrigen"))
                If dr.Table.Columns.Contains("UbicDestino") Then .UbicDestino = IIf(IsDBNull(dr.Item("UbicDestino")), 0, dr.Item("UbicDestino"))
                If dr.Table.Columns.Contains("TipoTarea") Then .TipoTarea = IIf(IsDBNull(dr.Item("IdTipoTarea")), 0, dr.Item("IdTipoTarea"))
                If dr.Table.Columns.Contains("IdBodegaOrigen") Then .IdBodegaOrigen = IIf(IsDBNull(dr.Item("IdBodegaOrigen")), 0, dr.Item("IdBodegaOrigen"))
                If dr.Table.Columns.Contains("Fecha") Then .Fecha = IIf(IsDBNull(dr.Item("Fecha")), New Date(1900, 1, 1), dr.Item("Fecha"))
                If dr.Table.Columns.Contains("IdProducto") Then .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                If dr.Table.Columns.Contains("Codigo") Then .Codigo = IIf(IsDBNull(dr.Item("Codigo")), 0, dr.Item("Codigo"))
                If dr.Table.Columns.Contains("CodigoBarra") Then .CodigoBarra = IIf(IsDBNull(dr.Item("CodigoBarra")), 0, dr.Item("CodigoBarra"))
                If dr.Table.Columns.Contains("IdTipoTarea") Then .IdTipoTarea = IIf(IsDBNull(dr.Item("IdTipoTarea")), 0, dr.Item("IdTipoTarea"))
                If dr.Table.Columns.Contains("Barra_Pallet") Then .Lic_Plate = IIf(IsDBNull(dr.Item("Barra_Pallet")), "", dr.Item("Barra_Pallet"))
                If dr.Table.Columns.Contains("Licencia") Then .Lic_Plate = IIf(IsDBNull(dr.Item("Licencia")), "", dr.Item("Licencia"))

                If dr.Table.Columns.Contains("IdUbicacionOrigen") Then .IdUbicacionOrigen = IIf(IsDBNull(dr.Item("IdUbicacionOrigen")), 0, dr.Item("IdUbicacionOrigen"))
                If dr.Table.Columns.Contains("IdUbicacionDestino") Then .IdUbicacionDestino = IIf(IsDBNull(dr.Item("IdUbicacionDestino")), 0, dr.Item("IdUbicacionDestino"))


                If dr.Table.Columns.Contains("IdPresentacion") Then .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                If dr.Table.Columns.Contains("IdUnidadMedida") Then .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                If dr.Table.Columns.Contains("IdEstadoOrigen") Then .IdEstadoOrigen = IIf(IsDBNull(dr.Item("IdEstadoOrigen")), 0, dr.Item("IdEstadoOrigen"))
                If dr.Table.Columns.Contains("IdProductoBodega") Then .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))

                If dr.Table.Columns.Contains("No_Doc_Ingreso") Then .No_Doc_Ingreso = IIf(IsDBNull(dr.Item("No_Doc_Ingreso")), "", dr.Item("No_Doc_Ingreso"))
                If dr.Table.Columns.Contains("No_Ref_Ingreso") Then .No_Ref_Ingreso = IIf(IsDBNull(dr.Item("No_Ref_Ingreso")), "", dr.Item("No_Ref_Ingreso"))
                If dr.Table.Columns.Contains("No_Doc_Salida") Then .No_Doc_Salida = IIf(IsDBNull(dr.Item("No_Doc_Salida")), "", dr.Item("No_Doc_Salida"))
                If dr.Table.Columns.Contains("No_Ref_Salida") Then .No_Ref_Salida = IIf(IsDBNull(dr.Item("No_Ref_Salida")), "", dr.Item("No_Ref_Salida"))

                If dr.Table.Columns.Contains("Ingresos") Then .Ingresos = IIf(IsDBNull(dr.Item("Ingresos")), 0, dr.Item("Ingresos"))
                If dr.Table.Columns.Contains("Salidas") Then .Salidas = IIf(IsDBNull(dr.Item("Salidas")), 0, dr.Item("Salidas"))
                If dr.Table.Columns.Contains("Ajustes_Positivos") Then .Ajustes_Positivos = IIf(IsDBNull(dr.Item("Ajustes_Positivos")), 0, dr.Item("Ajustes_Positivos"))
                If dr.Table.Columns.Contains("Ajustes_Negativos") Then .Ajustes_Negativos = IIf(IsDBNull(dr.Item("Ajustes_Negativos")), 0, dr.Item("Ajustes_Negativos"))
                If dr.Table.Columns.Contains("EnMovimiento") Then .EnMovimiento = IIf(IsDBNull(dr.Item("EnMovimiento")), 0, dr.Item("EnMovimiento"))

                If dr.Table.Columns.Contains("IdPropietarioBodega") Then .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))

                If dr.Table.Columns.Contains("Clasificacion") Then .Clasificacion = IIf(IsDBNull(dr.Item("Clasificacion")), 0, dr.Item("Clasificacion"))
                If dr.Table.Columns.Contains("Area_Origen") Then .Area_Origen = IIf(IsDBNull(dr.Item("Area_Origen")), 0, dr.Item("Area_Origen"))
                If dr.Table.Columns.Contains("barra_pallet") Then .Lic_Plate = IIf(IsDBNull(dr.Item("barra_pallet")), "", dr.Item("barra_pallet"))
                '#GT23032022: campo reportes cealsa
                If dr.Table.Columns.Contains("regimen_ingreso") Then .regimen_ingreso = IIf(IsDBNull(dr.Item("regimen_ingreso")), "", dr.Item("regimen_ingreso"))
                If dr.Table.Columns.Contains("no_ticket_tms") Then .no_ticket_tms = IIf(IsDBNull(dr.Item("no_ticket_tms")), 0, dr.Item("no_ticket_tms"))
                If dr.Table.Columns.Contains("fecha_ingreso") Then .fecha_ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), "", dr.Item("fecha_ingreso"))
                If dr.Table.Columns.Contains("placa_ingreso") Then .placa_ingreso = IIf(IsDBNull(dr.Item("placa_ingreso")), "", dr.Item("placa_ingreso"))
                If dr.Table.Columns.Contains("marca_ingreso") Then .marca_ingreso = IIf(IsDBNull(dr.Item("marca_ingreso")), "", dr.Item("marca_ingreso"))
                If dr.Table.Columns.Contains("tipo_ingreso") Then .tipo_ingreso = IIf(IsDBNull(dr.Item("tipo_ingreso")), "", dr.Item("tipo_ingreso"))
                If dr.Table.Columns.Contains("contenedor_ingreso") Then .contenedor_ingreso = IIf(IsDBNull(dr.Item("contenedor_ingreso")), "", dr.Item("contenedor_ingreso"))

                If dr.Table.Columns.Contains("Poliza_Salida") Then .Poliza_Salida = IIf(IsDBNull(dr.Item("Poliza_Salida")), "", dr.Item("Poliza_Salida"))
                If dr.Table.Columns.Contains("Fecha_Salida") Then .Fecha_Salida = IIf(IsDBNull(dr.Item("Fecha_Salida")), "", dr.Item("Fecha_Salida"))
                If dr.Table.Columns.Contains("placa_salida") Then .placa_salida = IIf(IsDBNull(dr.Item("placa_salida")), "", dr.Item("placa_salida"))
                If dr.Table.Columns.Contains("marca_salida") Then .marca_salida = IIf(IsDBNull(dr.Item("marca_salida")), "", dr.Item("marca_salida"))
                If dr.Table.Columns.Contains("tipo_salida") Then .tipo_salida = IIf(IsDBNull(dr.Item("tipo_salida")), "", dr.Item("tipo_salida"))
                If dr.Table.Columns.Contains("Regimen_Salida") Then .regimen_salida = IIf(IsDBNull(dr.Item("Regimen_Salida")), "", dr.Item("Regimen_Salida"))
                If dr.Table.Columns.Contains("contenedor_salida") Then .contenedor_salida = IIf(IsDBNull(dr.Item("contenedor_salida")), "", dr.Item("contenedor_salida"))
                If dr.Table.Columns.Contains("NombreArea") Then .NombreArea = IIf(IsDBNull(dr.Item("NombreArea")), "", dr.Item("NombreArea"))
                If dr.Table.Columns.Contains("Operador") Then .Operador = IIf(IsDBNull(dr.Item("Operador")), "", dr.Item("Operador"))


            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_All_Ingresos_Y_Salidas_By_Fecha(ByVal pFecha As Date,
                                                               ByVal lConnection As SqlConnection,
                                                               ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_Movimientos)

        Dim lMovimientosMismoDia As New List(Of clsBeVW_Movimientos)

        Get_All_Ingresos_Y_Salidas_By_Fecha = Nothing

        Try

            Dim vsql As String = "SELECT * FROM VW_Movimientos_N WHERE cast(fecha AS DATE) = @Fecha 
                                  AND TipoTarea IN ('RECE', 'DESP')"


            Using lDTA As New SqlDataAdapter(vsql, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", FormatoFechas.tFecha(pFecha))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vBeVWMovimiento As New clsBeVW_Movimientos()

                    For Each r In lDataTable.Rows
                        vBeVWMovimiento = New clsBeVW_Movimientos()
                        Cargar(vBeVWMovimiento, r)
                        lMovimientosMismoDia.Add(vBeVWMovimiento)
                    Next

                    Return lMovimientosMismoDia

                End If

            End Using

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Throw ex

        End Try

    End Function

    Public Shared Function Get_All_By_Rango_Fechas(ByVal pFechaDesde As Date,
                                                   ByVal pFechaHasta As Date,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_Movimientos)

        Dim lMovimientos As New List(Of clsBeVW_Movimientos)

        Get_All_By_Rango_Fechas = Nothing

        Try

            Dim vsql As String = "SELECT * FROM VW_Movimientos_N WHERE cast(fecha AS DATE) >= @FechaDesde AND cast(fecha AS DATE) <= @FechaHasta
                                  AND TipoTarea IN ('RECE', 'DESP', 'TRAS')"


            Using lDTA As New SqlDataAdapter(vsql, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@FechaDesde", FormatoFechas.tFecha(pFechaDesde))
                lDTA.SelectCommand.Parameters.AddWithValue("@FechaHasta", FormatoFechas.tFecha(pFechaHasta))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vBeVWMovimiento As New clsBeVW_Movimientos()

                    For Each r In lDataTable.Rows
                        vBeVWMovimiento = New clsBeVW_Movimientos()
                        Cargar(vBeVWMovimiento, r)
                        lMovimientos.Add(vBeVWMovimiento)
                    Next

                    Return lMovimientos

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Rango_Fechas_And_IdProductoBodega(ByVal pFechaDesde As Date,
                                                                        ByVal pFechaHasta As Date,
                                                                        ByVal pIdProductoBodega As Integer,
                                                                        ByVal lConnection As SqlConnection,
                                                                        ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_Movimientos)

        Dim lMovimientos As New List(Of clsBeVW_Movimientos)

        Get_All_By_Rango_Fechas_And_IdProductoBodega = Nothing

        Try

            Dim vsql As String = "SELECT * FROM VW_Movimientos_N WHERE cast(fecha AS DATE) >= @FechaDesde AND cast(fecha AS DATE) <= @FechaHasta
                                  AND TipoTarea IN ('RECE', 'DESP') AND IdProductoBodega = @IdProductoBodega"


            Using lDTA As New SqlDataAdapter(vsql, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@FechaDesde", FormatoFechas.tFecha(pFechaDesde))
                lDTA.SelectCommand.Parameters.AddWithValue("@FechaHasta", FormatoFechas.tFecha(pFechaHasta))
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vBeVWMovimiento As New clsBeVW_Movimientos()

                    For Each r In lDataTable.Rows
                        vBeVWMovimiento = New clsBeVW_Movimientos()
                        Cargar(vBeVWMovimiento, r)
                        lMovimientos.Add(vBeVWMovimiento)
                    Next

                    Get_All_By_Rango_Fechas_And_IdProductoBodega = lMovimientos

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Top_1_By_IdBodega(ByVal pIdBodega As Integer,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_Movimientos)

        Dim lMovimientos As New List(Of clsBeVW_Movimientos)

        Get_Top_1_By_IdBodega = Nothing

        Try

            Dim vsql As String = "SELECT TOP(1) * FROM VW_Movimientos_N WHERE IdBodega = @IdBodega"


            Using lDTA As New SqlDataAdapter(vsql, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vBeVWMovimiento As New clsBeVW_Movimientos()

                    For Each r In lDataTable.Rows
                        vBeVWMovimiento = New clsBeVW_Movimientos()
                        Cargar(vBeVWMovimiento, r)
                        lMovimientos.Add(vBeVWMovimiento)
                    Next

                    Get_Top_1_By_IdBodega = lMovimientos

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Fecha(ByVal pFecha As Date,
                                            ByVal pIdTicketTMS As Integer,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_Movimientos)

        Dim lMovimientosMismoDia As New List(Of clsBeVW_Movimientos)

        Get_All_By_Fecha = Nothing

        Try

            Dim vsql As String = "SELECT * FROM VW_Movimientos_N WHERE cast(fecha AS DATE) = @Fecha 
                                  AND TipoTarea IN ('RECE', 'DESP') AND IdTicketTMS = @IdTicketTMS"


            Using lDTA As New SqlDataAdapter(vsql, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", FormatoFechas.tFecha(pFecha))
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTicketTMS", pIdTicketTMS)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vBeVWMovimiento As New clsBeVW_Movimientos()

                    For Each r In lDataTable.Rows
                        vBeVWMovimiento = New clsBeVW_Movimientos()
                        Cargar(vBeVWMovimiento, r)
                        lMovimientosMismoDia.Add(vBeVWMovimiento)
                    Next

                    Return lMovimientosMismoDia

                End If

            End Using

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Throw ex

        End Try

    End Function

    Public Shared Function Get_All_Ubic_Y_Cest_By_Fecha(ByVal pFecha As Date,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_Movimientos)

        Dim lMovimientosMismoDia As New List(Of clsBeVW_Movimientos)

        Get_All_Ubic_Y_Cest_By_Fecha = Nothing

        Try

            Dim vsql As String = "SELECT * FROM VW_Movimientos_N WHERE cast(fecha AS DATE) = @Fecha 
                                  AND TipoTarea IN ('CEST', 'UBIC')"


            Using lDTA As New SqlDataAdapter(vsql, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha", FormatoFechas.tFecha(pFecha))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vBeVWMovimiento As New clsBeVW_Movimientos()

                    For Each r In lDataTable.Rows
                        vBeVWMovimiento = New clsBeVW_Movimientos()
                        Cargar(vBeVWMovimiento, r)
                        lMovimientosMismoDia.Add(vBeVWMovimiento)
                    Next

                    Return lMovimientosMismoDia

                End If

            End Using

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Throw ex

        End Try

    End Function

    Public Shared Function Get_All_Ubic_Y_Cest_By_Fecha_And_LicPlate(ByVal pFechaDesde As Date,
                                                                     ByVal pFechaHasta As Date,
                                                                     ByVal pLicencia As String,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_Movimientos)

        Dim lMovimientosMismoDia As New List(Of clsBeVW_Movimientos)

        Get_All_Ubic_Y_Cest_By_Fecha_And_LicPlate = Nothing

        Try

            Dim vsql As String = "SELECT * FROM VW_Movimientos_N 
                                  WHERE cast(fecha AS DATE) BETWEEN @Desde AND @Hasta
                                  AND TipoTarea IN ('CEST', 'UBIC') 
                                  AND barra_pallet = @barra_pallet  "


            Using lDTA As New SqlDataAdapter(vsql, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@Desde", pFechaDesde)
                lDTA.SelectCommand.Parameters.AddWithValue("@Hasta", pFechaHasta)
                lDTA.SelectCommand.Parameters.AddWithValue("@barra_pallet", pLicencia)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vBeVWMovimiento As New clsBeVW_Movimientos()

                    For Each r In lDataTable.Rows
                        vBeVWMovimiento = New clsBeVW_Movimientos()
                        Cargar(vBeVWMovimiento, r)
                        lMovimientosMismoDia.Add(vBeVWMovimiento)
                    Next

                    Return lMovimientosMismoDia

                End If

            End Using

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Throw ex

        End Try

    End Function

End Class