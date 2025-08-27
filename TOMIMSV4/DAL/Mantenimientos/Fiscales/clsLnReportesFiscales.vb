Imports System.Data.SqlClient
Imports System.Text

Public Class clsLnReportesFiscales

    Public Shared Function Get_Fiscal_historico(ByVal pFechaDel As Date, ByVal pFechaAl As Date,
                                                 Optional ByVal pIdBodega As String = Nothing,
                                                 Optional ByVal pIdPropietario As Integer = Nothing,
                                                Optional ByVal pTimeOut As Integer = Nothing) As DataTable
        Get_Fiscal_historico = Nothing

        Try

            Dim vSQL As String = "select REGIMEN,NOMBRE_BODEGA,AREA,CLIENTE,DUA,NUMERO_ORDEN,REFERENCIA,
                                  CODIGO_POLIZA,PRODUCTO,FECHA,FECHA_INVENTARIO,CLASIFICACION,LICENCIA,
                                  CODIGO_BARRA,UBICACION,CANTIDAD,CIF,DAI,IVA,TOTAL_VALOR,SHIPPER, 
                                  ingreso,es_devolucion
                                  FROM dbo.VW_Fiscal_historico WHERE 1 = 1 "

            If pIdBodega = "Fiscal" Then
                vSQL += "AND es_bodega_fiscal = 1"
            ElseIf pIdBodega = "General" Then
                vSQL += "AND es_bodega_fiscal = 0"
            End If

            If pIdPropietario <> 0 Then
                vSQL += " AND Idpropietario = @pIdPropietario"
            End If

            vSQL += String.Format(" AND FECHA BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            'GT12072022_1430: se deja order by por codigo para visualizar al producto por cada dia en inventario
            'vSQL += " ORDER BY FECHA_INVENTARIO"
            vSQL += "ORDER BY codigo_barra, Fecha ASC"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdPropietario <> 0 Then

                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdPropietario", pIdPropietario)

                        End If

                        '#GT05062023: se agrega el timer para que el reporte no se cuelge por los filtros
                        If pTimeOut > 0 Then
                            lDTA.SelectCommand.CommandTimeout = pTimeOut
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Return lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Fiscal_Merca_vencida(ByVal pFechaAl As Date, ByVal pPropietario As Integer, ByVal pIdBodega As Integer) As DataTable
        Get_Fiscal_Merca_vencida = Nothing

        Try

            'Dim vSQL As String = "SELECT numero_orden,codigo_barra,material,nombre_cliente,Poliza,codigo_regimen,dias_regimen,
            '                      Fecha_Creacion as fecha_llegada, CAST(fecha_vencimiento AS DATE) fecha_vencimiento,
            '                      Disponible as cantidad, peso as net_weigth,unidad_peso as weigth_unit,'' as contenedor,dias_vida
            '                      FROM dbo.VW_Fiscal_Merca_Vencida WHERE codigo_regimen <>'NA' "

            'GT16032021 se agrega campo que indica dias a vencer.
            'GT24022022 consolidador se muestra en clasificacion, donde esta el propietario referenciado
            Dim vSQL As String = "SELECT numero_orden,nombre_cliente,clasificacion,codigo_barra,material,Poliza,codigo_regimen,
                                  Fecha_Creacion as fecha_llegada, CAST(fecha_vencimiento AS DATE) fecha_vencimiento,dias_vida as dias_para_vencer,
                                  Disponible as cantidad, peso as net_weigth,unidad_peso as weigth_unit,'' as contenedor
                                  FROM dbo.VW_Fiscal_Merca_Vencida WHERE codigo_regimen <>'NA' and IdBodega= @IdBodega "

            vSQL += String.Format(" AND fecha_vencimiento <= {0}", FormatoFechas.fFecha(pFechaAl))

            If pPropietario <> 0 Then

                vSQL += " AND IdPropietario = @IdPropietario "

            End If

            vSQL += " ORDER BY IdStock"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        If pPropietario <> 0 Then

                            lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pPropietario)

                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Return lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Fiscal_Det_CtasOrden(ByVal pFechaAl As Date,
                                                    ByVal pTipo_transaccion As String,
                                                   Optional ByVal pIdBodega As String = Nothing) As DataTable

        Get_Fiscal_Det_CtasOrden = Nothing

        Try

            'Dim vSQL As String = "SELECT 
            '                        nombre_comercial cliente,
            '                        codigo_poliza poliza,
            '                        codigo_barra,
            '                        sum(cantidad) cantidad,
            '                        sum(valor_dai) valor_dai,
            '                        sum(valor_iva) valor_iva,
            '                        sum(valor_aduana) valor_aduana
            '                        FROM dbo.VW_Fiscal_CtasOrden Where 1=1 "



            '   Dim vSQL As String = "SELECT 
            '                           Bodega,
            '                           nombre_comercial cliente,
            '                           codigo_poliza poliza,
            '                           codigo_barra,
            '                           sum(cantidad) cantidad,
            '                           sum(valor_dai) valor_dai,
            '                           sum(valor_iva) valor_iva,
            'sum(valor_dai) + sum(valor_iva) as suma_dai_iva,
            '                           sum(valor_aduana) valor_aduana
            '                           FROM dbo.VW_Fiscal_CtasOrden Where 1=1  "

            'GT12072022_1520: se omite col poliza, y se agrega referencia y no_orden
            Dim vSQL As String = "SELECT 
                                    Bodega,
                                    nombre_comercial cliente,
                                    Referencia,
                                    numero_orden,
                                    codigo_barra,
                                    sum(cantidad) cantidad,
                                    sum(valor_dai) valor_dai,
                                    sum(valor_iva) valor_iva,
									sum(valor_dai) + sum(valor_iva) as suma_dai_iva,
                                    sum(valor_aduana) valor_aduana
                                    FROM dbo.VW_Fiscal_CtasOrden Where 1=1  "

            vSQL += String.Format(" AND fecha_recepcion >= {0}", FormatoFechas.fFecha(pFechaAl))

            If pTipo_transaccion <> "TODO" Then

                vSQL += " AND tipo_transaccion = @pTipo_transaccion "

            End If


            If pIdBodega = "Fiscal" Then
                vSQL += " AND es_bodega_fiscal = 1 "
            ElseIf pIdBodega = "General" Then
                vSQL += " AND es_bodega_fiscal = 0 "
            End If


            vSQL += " group by nombre_comercial,codigo_barra,Bodega,referencia,numero_orden "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pTipo_transaccion <> "TODO" Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pTipo_transaccion", pTipo_transaccion)
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Return lDataTable
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Fiscal_Poliza_CtasOrden(ByVal pFechaAl As Date,
                                                    ByVal pTipo_Movimiento As String,
                                                   Optional ByVal pIdTipoRegimen As String = Nothing) As DataTable

        Get_Fiscal_Poliza_CtasOrden = Nothing

        Try

            Dim vSQL As String = ""

            If pTipo_Movimiento = "TODO" Then

                vSQL = "SELECT 
                            'INGRESOS' as MOVIMIENTO,
                            ISNULL(sum(valor_dai),0) DAI,
                            ISNULL(sum(valor_iva),0) IVA,
                            ISNULL(Sum (valor_dai+ valor_iva),0) DAI_IVA,
                            ISNULL(sum(valor_aduana),0) VALOR_ADUANA
                            FROM dbo.VW_Fiscal_CtasOrden WHERE 1=1 "
                vSQL += String.Format(" AND fecha_recepcion >= {0} ", FormatoFechas.fFecha(pFechaAl))

                If pIdTipoRegimen = "Fiscal" Then
                    vSQL += " AND es_bodega_fiscal = 1 "
                ElseIf pIdTipoRegimen = "General" Then
                    vSQL += " AND es_bodega_fiscal = 0 "
                End If

                vSQL += " UNION ALL "

                vSQL +=
                            " SELECT 
                            'SALIDAS' as MOVIMIENTO,
                            ISNULL(sum(valor_dai),0) DAI,
                            ISNULL(sum(valor_iva),0) IVA,
                            ISNULL(Sum(valor_dai+ valor_iva),0) DAI_IVA,
                            ISNULL(sum(valor_aduana),0) VALOR_ADUANA
                            FROM dbo.VW_Fiscal_CtasOrden WHERE 1=1 "

                vSQL += String.Format(" AND fecha_despacho >= {0}", FormatoFechas.fFecha(pFechaAl))


                If pIdTipoRegimen = "Fiscal" Then
                    vSQL += " AND es_bodega_fiscal = 1 "
                ElseIf pIdTipoRegimen = "General" Then
                    vSQL += " AND es_bodega_fiscal = 0 "
                End If


            Else

                vSQL = " SELECT 
                            tipo_transaccion as MOVIMIENTO,
                            ISNULL(sum(valor_dai),0) DAI,
                            ISNULL(sum(valor_iva),0) IVA,
                            ISNULL(Sum (valor_dai+ valor_iva),0) DAI_IVA,
                            ISNULL(sum(valor_aduana),0) VALOR_ADUANA
                            FROM dbo.VW_Fiscal_CtasOrden WHERE 1=1 "

                vSQL += String.Format(" AND tipo_transaccion = @pTipo_transaccion")


                If pTipo_Movimiento = "INGRESO" Then

                    vSQL += String.Format(" AND fecha_recepcion >= {0}", FormatoFechas.fFecha(pFechaAl))

                Else

                    vSQL += String.Format(" AND fecha_despacho >= {0}", FormatoFechas.fFecha(pFechaAl))
                End If

                If pIdTipoRegimen = "Fiscal" Then
                    vSQL += " AND es_bodega_fiscal = 1 "
                ElseIf pIdTipoRegimen = "General" Then
                    vSQL += " AND es_bodega_fiscal = 0 "
                End If

                vSQL += String.Format(" GROUP BY tipo_transaccion")

            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text


                        If pTipo_Movimiento <> "TODO" Then

                            lDTA.SelectCommand.Parameters.AddWithValue("@pTipo_transaccion", pTipo_Movimiento)

                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Return lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Fiscal_historico_clientes(ByVal pFechaDel As Date, ByVal pFechaAl As Date,
                                                 Optional ByVal pIdRegimen As String = Nothing,
                                                 Optional ByVal pIdPropietario As Integer = Nothing) As DataTable


        Get_Fiscal_historico_clientes = Nothing

        Try

            Dim vSQL As String = "select FECHA AS FECHA_INVENTARIO,CLIENTE,CODIGO_BARRA,CLASIFICACION,CANTIDAD,
                                  CIF,DAI,IVA,TOTAL_VALOR,FECHA_RECEPCION AS FECHA_LLEGADA,UBICACION,CODIGO_POLIZA,REGIMEN,NOMBRE_BODEGA 
                                  from VW_Fiscal_historico WHERE 1 = 1 "

            If pIdRegimen <> "Todo" Then
                'vSQL += " AND es_bodega_fiscal = @pIdRegimen"
                vSQL += " AND REGIMEN = @pIdRegimen"
            End If

            If pIdPropietario <> 0 Then
                vSQL += " AND Idpropietario = @pIdPropietario"
            End If

            vSQL += String.Format(" AND FECHA BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))


            'vSQL += " GROUP BY FECHA,CLIENTE,FECHA_RECEPCION,CODIGO_BARRA,SHORT_NAME,Cantidad,CIF,DAI,IVA,TOTAL_VALOR,UBICACION,CODIGO_POLIZA,REGIMEN,NOMBRE_BODEGA "

            'vSQL += " ORDER BY FECHA_INVENTARIO, CLIENTE,SHORT_NAME,FECHA_LLEGADA"
            vSQL += " ORDER BY FECHA_INVENTARIO,FECHA_LLEGADA, CLIENTE "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdRegimen <> "Todo" Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdRegimen", pIdRegimen)
                        End If

                        If pIdPropietario <> 0 Then

                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdPropietario", pIdPropietario)

                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Return lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Stock_res_jornada(ByVal pFechaDel As Date, ByVal pFechaAl As Date,
                                                 Optional ByVal pIdRegimen As String = Nothing) As DataTable


        Get_Stock_res_jornada = Nothing

        Try

            'Dim vSQL As String = "select regimen,grupo as Sector,codigo,idbodega,bodega,Fecha,sum(valor_aduana) AS valor_aduana 
            '                     from VW_Stock_res_jornada  WHERE 1 = 1 "

            '#GT16062022: Se renombra la vista para agruparla en los reportes Fiscales Cealsa
            Dim vSQL As String = "select regimen,grupo as Sector,codigo,idbodega,bodega,Fecha,sum(valor_aduana) AS valor_aduana 
                                 from VW_Fiscal_Valorización WHERE 1 = 1 "

            If pIdRegimen <> "Todo" Then
                vSQL += " AND Regimen = @pIdRegimen"
            End If

            vSQL += String.Format(" AND FECHA BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " group by Fecha,bodega,regimen,codigo,idbodega,grupo
                      order by bodega asc, Fecha asc, regimen asc "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdRegimen <> "Todo" Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdRegimen", pIdRegimen)
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Return lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Stock_res_jornada_merca(ByVal pFechaDel As Date, ByVal pFechaAl As Date,
                                                 Optional ByVal pIdRegimen As String = Nothing) As DataTable


        Get_Stock_res_jornada_merca = Nothing

        Try

            Dim vSQL As String = " select * from VW_Stock_res_jornada_merca where 1=1 "

            If pIdRegimen <> "Todo" Then
                vSQL += " AND Regimen = @pIdRegimen"
            End If

            '#GT18082023: no encontre en git cuando se dejó este query asi de mal
            'vSQL += String.Format(" AND FECHA = {0} OR FECHA IS NULL ", FormatoFechas.fFecha(pFechaDel))

            vSQL += String.Format(" AND FECHA BETWEEN {0} AND {1}  ", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " OR FECHA IS NULL "
            vSQL += " order by codigobodega asc, codigomercaderia"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdRegimen <> "Todo" Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdRegimen", pIdRegimen)
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_Stock_res_jornada_merca = lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Movimientos_Retroactivos_All(ByVal pFechaDel As Date, ByVal pFechaAl As Date,
                                                            Optional ByVal pIdRegimen As String = Nothing) As DataTable


        Get_Movimientos_Retroactivos_All = Nothing

        Try

            Dim vSQL As String = " select * from VW_Movimientos_Retroactivos where 1=1 "

            If pIdRegimen <> "Todo" Then
                vSQL += " AND Regimen = @pIdRegimen"
            End If

            vSQL += String.Format(" AND FECHA_INGRESO BETWEEN {0} AND {1} ", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " order by FECHA_INGRESO "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdRegimen <> "Todo" Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdRegimen", pIdRegimen)
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_Movimientos_Retroactivos_All = lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Movimientos_Sin_Retroactivos(ByVal pFechaDel As Date, ByVal pFechaAl As Date,
                                                            Optional ByVal pIdRegimen As String = Nothing) As DataTable


        Get_Movimientos_Sin_Retroactivos = Nothing

        Try

            Dim vSQL As String = " select * from VW_Movimientos_Retroactivos WHERE Estado = 'Historico incompleto' "

            vSQL += String.Format(" AND FECHA_INGRESO BETWEEN {0} AND {1} ", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " ORDER BY FECHA_INGRESO "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_Movimientos_Sin_Retroactivos = lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Ingreso_Fiscal(ByVal pFechaDel As Date, ByVal pFechaAl As Date,
                                              ByVal IdBodega As Integer, ByVal Activo As Boolean,
                                              Optional IdOrdenCompraEnc As Integer = 0) As DataTable


        Get_Ingreso_Fiscal = Nothing

        Try

            Dim vSQL As String = " SELECT * FROM VW_Ingreso_Fiscal 
                                   WHERE IdBodega=@IdBodega and Activo =@Activo "

            If IdOrdenCompraEnc > 0 Then
                vSQL += " and IdOrdenCompraEnc = @IdOrdenCompraEnc "
            End If

            vSQL += String.Format(" AND Fecha_Ingreso BETWEEN {0} AND {1} ", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += "  ORDER BY IdOrdenCompraEnc,Fecha_Ingreso "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Activo", Activo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        If IdOrdenCompraEnc > 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", IdOrdenCompraEnc)
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_Ingreso_Fiscal = lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Salida_Fiscal(ByVal pFechaDel As Date, ByVal pFechaAl As Date,
                                              ByVal IdBodega As Integer, ByVal Activo As Boolean,
                                              Optional pIdPedidoEnc As Integer = 0) As DataTable

        Get_Salida_Fiscal = Nothing

        Try

            Dim vSQL As String = " SELECT * FROM VW_Salida_Fiscal 
                                   WHERE IdBodega=@IdBodega "

            If pIdPedidoEnc > 0 Then
                vSQL += " and IdPedidoEnc = @pIdPedidoEnc "
            End If

            vSQL += String.Format(" AND cast(Fecha_Despacho AS DATE) BETWEEN {0} AND {1} ", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += "  ORDER BY IdPedidoEnc,Fecha_Despacho "


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Activo", Activo)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        If pIdPedidoEnc > 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdPedidoEnc", pIdPedidoEnc)
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_Salida_Fiscal = lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Existencia_Fiscal_DataTable(ByVal pIdPropietarioBodega As Integer) As DataTable

        Get_Existencia_Fiscal_DataTable = Nothing

        Try

            Dim vSQL As String = "SELECT 
                                  Propietario, 
                                  Proveedor,
                                  Bodega,
                                  Codigo as Codigo_Producto, 
                                  Codigo_barra,                                  
                                  Nombre Nombre_Producto,                                  
                                  UnidadMedida,
                                  Presentacion,
                                  NomEstado as Estado,
                                  Sum(isnull(CantidadSF,0)) as CantidadUMBas,
							      SUM(isnull(Cantidad,0)) as CantidadPresentacion,
							      SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							        CASE WHEN FACTOR > 0
							        THEN 
								        --ROUND(SUM(isnull(CantidadReservada/Factor,0)),6)
                                        ROUND(SUM(ISNULL(CantidadReservada,0)) / Factor,6)
							        ELSE
								        0
							        END AS Cantidad_Reservada_Pres,
							        ROUND(SUM(isnull(CantidadSF,0)) - SUM(isnull(CantidadReservada,0)),6)  as Disponible_UMBas, 
							        CASE WHEN FACTOR > 0
							        THEN 
								        --ROUND(SUM(isnull(Cantidad,0)) - SUM(isnull(CantidadReservada/Factor,0)),6)
                                        ROUND(SUM(ISNULL(Cantidad,0)),6) - ROUND(SUM(ISNULL(CantidadReservada,0)) / Factor,6)
							        ELSE
								        0
							        END AS Disponible_Presentación,
							        SUM(Peso) AS Peso,
                                    Lote,
                                    Fecha_Ingreso, Nombre_Completo AS [Ubicación],
                                    codigo_poliza,
									Numero_Orden,
                                    DUCA,
                                    No_Marchamo,
                                    No_Contenedor,
                                    Carta_Cupo,
                                    Observacion_Recepcion,
                                    Placa_Transporte,
                                    Marca_Transporte,
                                    placa_comercial,
                                    Nombre_Piloto,
                                    Apellido_Piloto,
                                    Fecha_Abandono,
                                    Regimen,
                                    Clasificacion,Producto_Marca,Familia,Tipo,parametro_a,parametro_b
							        FROM VW_Stock_Fiscal WHERE 1 > 0 "


            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega"
            End If

            vSQL += " Group by Bodega,NomEstado,Fecha_Ingreso,Codigo,
						  Nombre,Presentacion,UnidadMedida, 
						  Nombre_Completo, Factor, Propietario, codigo_barra,
                          Numero_Orden,
                          codigo_poliza,
                          DUCA,
                          No_Marchamo,
                          No_Contenedor,
                          Carta_Cupo,
                          Observacion_Recepcion,
                          Placa_Transporte,
                          Marca_Transporte,
                          placa_comercial,
                          Nombre_Piloto,
                          Apellido_Piloto,
                          Proveedor,
                          Lote,
                          Fecha_Abandono,Regimen,
                          Clasificacion,Producto_Marca,Familia,Tipo,parametro_a,parametro_b "

            vSQL += " ORDER BY CODIGO, Nombre_Completo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdPropietarioBodega > 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Existencia_Fiscal_DataTable = lDataTable
                            'Return lDataTable
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT06052024: calculo para reporte prefacturación
    Public Shared Function Get_Fiscal_Facturacion_By_IdBodega(ByVal pFechaDel As Date, ByVal pFechaAl As Date,
                                                              ByVal pEs_fiscal As Boolean,
                                                              ByVal pIdPropietario As Integer,
                                                              ByVal pAgrupaPorProducto As Boolean,
                                                              ByVal pBuscarProductoEspecifico As Boolean,
                                                              ByVal pNumero_Orden As String,
                                                              ByVal pCodigo_Barra As String,
                                                              ByVal pIdOrdenCompraEnc As Integer,
                                                              ByVal pTimeOut As Integer) As DataTable

        Get_Fiscal_Facturacion_By_IdBodega = Nothing

        Try

            Dim vSQL As String = ""

            If pEs_fiscal Then

                '#GT30072024: valido si incluyo producto en la consulta y agrupacion
                If pAgrupaPorProducto Then

                    vSQL = "select numero_orden,Fecha,sum(cantidad) unidades,sum(total_valor) valor_total,codigo_barra 
                               FROM dbo.VW_Fiscal_historico WHERE 1 = 1 "

                Else
                    vSQL = "select numero_orden,Fecha,sum(cantidad) unidades,sum(total_valor) valor_total 
                               FROM dbo.VW_Fiscal_historico WHERE 1 = 1 "
                End If

            Else

                '#GT30072024: valido si incluyo producto en la consulta y agrupacion
                If pAgrupaPorProducto Then

                    vSQL = "select referencia,Fecha,sum(cantidad) unidades,sum(total_valor) valor_total,codigo_barra 
                               FROM dbo.VW_Fiscal_historico WHERE 1 = 1 "

                Else
                    vSQL = "select referencia,Fecha,sum(cantidad) unidades,sum(total_valor) valor_total 
                               FROM dbo.VW_Fiscal_historico WHERE 1 = 1 "
                End If

            End If


            If pEs_fiscal Then

                vSQL += "AND es_bodega_fiscal = 1 "

                If Not String.IsNullOrEmpty(pNumero_Orden) Then
                    vSQL += " AND numero_orden=@pNumeroOrden "
                End If

            Else
                vSQL += "AND es_bodega_fiscal = 0 "

                If Not String.IsNullOrEmpty(pNumero_Orden) Then
                    vSQL += " AND referencia=@pNumeroOrden "
                End If
            End If

            If pIdOrdenCompraEnc > 0 Then
                vSQL += " AND IdOrdenCompraEnc=@pIdOrdenCompraEnc "
            End If


            If pIdPropietario <> 0 Then
                vSQL += "AND Idpropietario = @pIdPropietario "
            End If

            vSQL += String.Format(" AND FECHA BETWEEN {0} AND {1} ", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            If pEs_fiscal Then

                If pAgrupaPorProducto OrElse pBuscarProductoEspecifico Then
                    vSQL += "GROUP BY numero_orden,Fecha,codigo_barra Order BY numero_orden,codigo_barra "
                Else
                    vSQL += "GROUP BY numero_orden,Fecha Order BY numero_orden "
                End If

            Else
                If pAgrupaPorProducto OrElse pBuscarProductoEspecifico Then
                    vSQL += "GROUP BY referencia,Fecha,codigo_barra Order BY referencia,codigo_barra "
                Else
                    vSQL += "GROUP BY referencia,Fecha Order BY referencia "
                End If
            End If



            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If Not String.IsNullOrEmpty(pNumero_Orden) Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pNumeroOrden", pNumero_Orden)
                        End If

                        If Not String.IsNullOrEmpty(pCodigo_Barra) Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pCodigo_Barra", pCodigo_Barra)
                        End If

                        If pIdPropietario <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdPropietario", pIdPropietario)
                        End If

                        If pIdOrdenCompraEnc <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)
                        End If

                        '#GT05062023: se agrega el timer para que el reporte no se cuelge por los filtros
                        If pTimeOut > 0 Then
                            lDTA.SelectCommand.CommandTimeout = pTimeOut
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Return lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Fiscal_Facturacion_By_IdOrdenCompra(ByVal pFechaDel As Date, ByVal pFechaAl As Date,
                                                                   ByVal pEs_fiscal As Boolean,
                                                                   ByVal pIdPropietario As Integer,
                                                                   ByVal pAgrupaPorProducto As Boolean,
                                                                   ByVal pIdOrdenCompraEnc As Integer,
                                                                   ByVal pNumero_orden As String,
                                                                   ByVal pTimeOut As Integer) As DataTable

        Get_Fiscal_Facturacion_By_IdOrdenCompra = Nothing

        Try



            'If pEs_fiscal Then

            '    '#GT30072024: valido si incluyo producto en la consulta y agrupacion
            '    If pAgrupaPorProducto Then

            '        vSQL = "select numero_orden,Fecha,sum(cantidad) unidades,sum(total_valor) valor_total,codigo_barra 
            '                   FROM dbo.VW_Fiscal_historico WHERE 1 = 1 "

            '    Else
            '        vSQL = "select numero_orden,Fecha,sum(cantidad) unidades,sum(total_valor) valor_total 
            '                   FROM dbo.VW_Fiscal_historico WHERE 1 = 1 "
            '    End If

            'Else

            '    '#GT30072024: valido si incluyo producto en la consulta y agrupacion
            '    If pAgrupaPorProducto Then

            '        vSQL = "select referencia,Fecha,sum(cantidad) unidades,sum(total_valor) valor_total,codigo_barra 
            '                   FROM dbo.VW_Fiscal_historico WHERE 1 = 1 "

            '    Else
            '        vSQL = "select referencia,Fecha,sum(cantidad) unidades,sum(total_valor) valor_total 
            '                   FROM dbo.VW_Fiscal_historico WHERE 1 = 1 "
            '    End If

            'End If

            'Dim vSQL As String = ""
            'Dim baseQuery As String = "SELECT {0}, IdOrdenCompraEnc,Fecha, SUM(cantidad) unidades, SUM(total_valor) valor_total{1} FROM dbo.VW_Fiscal_historico WHERE 1 = 1"
            '' Columnas dinámicas según las condiciones
            'Dim referenciaColumna As String = If(pEs_fiscal, "numero_orden", "referencia")
            'Dim productoColumna As String = If(pAgrupaPorProducto, ", codigo_barra", "")
            '' Construir la consulta final
            'vSQL = String.Format(baseQuery, referenciaColumna, productoColumna)


            'If pEs_fiscal Then

            '    vSQL += "AND es_bodega_fiscal = 1 "

            '    If Not String.IsNullOrEmpty(pNumero_orden) Then
            '        vSQL += " AND numero_orden=@pNumeroOrden "
            '    End If

            'Else
            '    vSQL += "AND es_bodega_fiscal = 0 "

            '    If Not String.IsNullOrEmpty(pNumero_orden) Then
            '        vSQL += " AND referencia=@pNumeroOrden "
            '    End If
            'End If

            'If pIdOrdenCompraEnc > 0 Then
            '    vSQL += " AND IdOrdenCompraEnc=@pIdOrdenCompraEnc "
            'End If


            'If pIdPropietario <> 0 Then
            '    vSQL += "AND Idpropietario = @pIdPropietario "
            'End If

            'vSQL += String.Format(" AND FECHA BETWEEN {0} AND {1} ", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            'If pEs_fiscal Then

            '    If pAgrupaPorProducto Then
            '        vSQL += "GROUP BY numero_orden,IdOrdenCompraEnc,Fecha,codigo_barra Order BY numero_orden,codigo_barra "
            '    Else
            '        vSQL += "GROUP BY numero_orden,IdOrdenCompraEnc,Fecha Order BY numero_orden "
            '    End If

            'Else
            '    If pAgrupaPorProducto Then
            '        vSQL += "GROUP BY referencia,IdOrdenCompraEnc,Fecha,codigo_barra Order BY referencia,codigo_barra "
            '    Else
            '        vSQL += "GROUP BY referencia,IdOrdenCompraEnc, Fecha Order BY referencia "
            '    End If
            'End If

            Dim vSQL As New StringBuilder()

            ' Definir partes dinámicas según las condiciones
            Dim referenciaColumna As String = If(pEs_fiscal, "numero_orden", "referencia")
            Dim productoColumna As String = If(pAgrupaPorProducto, ", codigo_barra", "")
            Dim condicionFiscal As String = If(pEs_fiscal, "AND es_bodega_fiscal = 1", "AND es_bodega_fiscal = 0")

            ' Base de la consulta
            vSQL.AppendLine($"SELECT {referenciaColumna}, IdOrdenCompraEnc, Fecha, SUM(cantidad) unidades, SUM(total_valor) valor_total{productoColumna}")
            vSQL.AppendLine("FROM dbo.VW_Fiscal_historico WHERE 1 = 1")
            vSQL.AppendLine(condicionFiscal)

            ' Filtros dinámicos
            If Not String.IsNullOrEmpty(pNumero_orden) Then
                Dim columnaFiltro As String = If(pEs_fiscal, "numero_orden", "referencia")
                vSQL.AppendLine($"AND {columnaFiltro} = @pNumeroOrden")
            End If

            If pIdOrdenCompraEnc > 0 Then
                vSQL.AppendLine("AND IdOrdenCompraEnc = @pIdOrdenCompraEnc")
            End If

            If pIdPropietario <> 0 Then
                vSQL.AppendLine("AND IdPropietario = @pIdPropietario")
            End If

            ' Rango de fechas
            vSQL.AppendLine($"AND FECHA BETWEEN {FormatoFechas.fFecha(pFechaDel)} AND {FormatoFechas.fFecha(pFechaAl)}")

            ' Agrupación y ordenación
            If pAgrupaPorProducto Then
                If pEs_fiscal Then
                    vSQL.AppendLine("GROUP BY numero_orden, IdOrdenCompraEnc, Fecha, codigo_barra")
                    vSQL.AppendLine("ORDER BY numero_orden, codigo_barra, Fecha")
                Else
                    vSQL.AppendLine("GROUP BY referencia, IdOrdenCompraEnc, Fecha, codigo_barra")
                    vSQL.AppendLine("ORDER BY referencia, codigo_barra, Fecha")
                End If
            Else
                If pEs_fiscal Then
                    vSQL.AppendLine("GROUP BY numero_orden, IdOrdenCompraEnc, Fecha")
                    vSQL.AppendLine("ORDER BY numero_orden, Fecha")
                Else
                    vSQL.AppendLine("GROUP BY referencia, IdOrdenCompraEnc, Fecha")
                    vSQL.AppendLine("ORDER BY referencia, Fecha")
                End If
            End If

            ' Convertir el StringBuilder a una cadena final
            Dim consultaSQL As String = vSQL.ToString()



            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(consultaSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If Not String.IsNullOrEmpty(pNumero_orden) Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pNumeroOrden", pNumero_orden)
                        End If

                        If pIdPropietario <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdPropietario", pIdPropietario)
                        End If

                        If pIdOrdenCompraEnc <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@pIdOrdenCompraEnc", pIdOrdenCompraEnc)
                        End If

                        '#GT05062023: se agrega el timer para que el reporte no se cuelge por los filtros
                        If pTimeOut > 0 Then
                            lDTA.SelectCommand.CommandTimeout = pTimeOut
                        End If

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Fiscal_Facturacion_By_IdOrdenCompra = lDataTable
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class
