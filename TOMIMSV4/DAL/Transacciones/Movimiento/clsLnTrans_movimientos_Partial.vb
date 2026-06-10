Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_movimientos

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdMovimiento),0) FROM trans_movimientos"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text

                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()
                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lMax = CInt(lReturnValue) + 1
                    End If

                End Using

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT ISNULL(Max(IdMovimiento),0) FROM trans_movimientos "

            Using lCommand As New SqlCommand(vSQL, pConnection)

                lCommand.CommandType = CommandType.Text
                lCommand.Transaction = pTransaction

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue) + 1
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Function Get_IdMovimiento_Verificacion_Existente(ByVal pMovimiento As clsBeTrans_movimientos,
                                                                    ByVal pConnection As SqlConnection,
                                                                    ByVal pTransaction As SqlTransaction) As Integer
        Try
            Dim vSQL As String =
                "SELECT TOP 1 IdMovimiento " &
                "FROM trans_movimientos WITH (UPDLOCK, HOLDLOCK) " &
                "WHERE IdTipoTarea = @IdTipoTarea " &
                "  AND IdTransaccion = @IdTransaccion " &
                "  AND ISNULL(IdPedidoEnc, 0) = @IdPedidoEnc " &
                "  AND ISNULL(IdPedidoDet, 0) = @IdPedidoDet " &
                "  AND ISNULL(IdRecepcion, 0) = @IdRecepcion " &
                "  AND ISNULL(IdRecepcionDet, 0) = @IdRecepcionDet " &
                "  AND ISNULL(IdProductoBodega, 0) = @IdProductoBodega " &
                "  AND ISNULL(IdUbicacionOrigen, 0) = @IdUbicacionOrigen " &
                "  AND ISNULL(IdUbicacionDestino, 0) = @IdUbicacionDestino " &
                "  AND ISNULL(IdPresentacion, 0) = @IdPresentacion " &
                "  AND ISNULL(IdEstadoOrigen, 0) = @IdEstadoOrigen " &
                "  AND ISNULL(IdEstadoDestino, 0) = @IdEstadoDestino " &
                "  AND ISNULL(IdUnidadMedida, 0) = @IdUnidadMedida " &
                "  AND ISNULL(barra_pallet, '') = @BarraPallet " &
                "  AND ISNULL(lote, '') = @Lote " &
                "  AND ISNULL(fecha_vence, CONVERT(DATETIME, '19000101', 112)) = ISNULL(@FechaVence, CONVERT(DATETIME, '19000101', 112)) " &
                "  AND ABS(ISNULL(cantidad, 0) - @Cantidad) < 0.000001 " &
                "ORDER BY IdMovimiento"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction)
                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdTipoTarea", pMovimiento.IdTipoTarea)
                lCommand.Parameters.AddWithValue("@IdTransaccion", pMovimiento.IdTransaccion)
                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pMovimiento.IdPedidoEnc)
                lCommand.Parameters.AddWithValue("@IdPedidoDet", pMovimiento.IdPedidoDet)
                lCommand.Parameters.AddWithValue("@IdRecepcion", pMovimiento.IdRecepcion)
                lCommand.Parameters.AddWithValue("@IdRecepcionDet", pMovimiento.IdRecepcionDet)
                lCommand.Parameters.AddWithValue("@IdProductoBodega", pMovimiento.IdProductoBodega)
                lCommand.Parameters.AddWithValue("@IdUbicacionOrigen", pMovimiento.IdUbicacionOrigen)
                lCommand.Parameters.AddWithValue("@IdUbicacionDestino", pMovimiento.IdUbicacionDestino)
                lCommand.Parameters.AddWithValue("@IdPresentacion", pMovimiento.IdPresentacion)
                lCommand.Parameters.AddWithValue("@IdEstadoOrigen", pMovimiento.IdEstadoOrigen)
                lCommand.Parameters.AddWithValue("@IdEstadoDestino", pMovimiento.IdEstadoDestino)
                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pMovimiento.IdUnidadMedida)
                lCommand.Parameters.AddWithValue("@BarraPallet", If(pMovimiento.Barra_pallet, ""))
                lCommand.Parameters.AddWithValue("@Lote", If(pMovimiento.Lote, ""))
                lCommand.Parameters.Add(New SqlParameter("@FechaVence", SqlDbType.DateTime) With {.Value = If(pMovimiento.Fecha_vence = Nothing, DBNull.Value, CType(pMovimiento.Fecha_vence, Object))})
                lCommand.Parameters.AddWithValue("@Cantidad", pMovimiento.Cantidad)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot Nothing AndAlso lReturnValue IsNot DBNull.Value Then
                    Return CInt(lReturnValue)
                End If
            End Using

            Return 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function Normalizar_Cantidad_Verificacion_UMBAS(ByVal pPickingUbic As clsBeTrans_picking_ubic,
                                                                   ByVal pCantidad As Double,
                                                                   ByVal pCantidadEnUmbas As Boolean,
                                                                   ByVal pConnection As SqlConnection,
                                                                   ByVal pTransaction As SqlTransaction) As Double
        If pCantidadEnUmbas OrElse pPickingUbic Is Nothing OrElse pPickingUbic.IdPresentacion <= 0 Then
            Return Math.Round(pCantidad, 6)
        End If

        Dim vFactor As Double = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pPickingUbic.IdProductoBodega,
                                                                                         pPickingUbic.IdPresentacion,
                                                                                         pConnection,
                                                                                         pTransaction)

        If vFactor <= 0 Then Return Math.Round(pCantidad, 6)

        Return Math.Round(pCantidad * vFactor, 6)
    End Function

    Public Shared Function Get_Movimientos(ByVal pIdBodegaOrigen As Integer, ByVal pFechaDel As Date, ByVal pFechaAl As Date, Optional ByVal pLote As String = Nothing) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = ""

            If pIdBodegaOrigen > 0 Then
                vSQL = "SELECT Propietario,IdBodega,Poliza,Producto,Presentación,
                        [Estado Origen],[Estado Destino],[Unidad de Medida],cantidad,peso,lote,Origen,Destino,
                        [Tipo Tarea],IdBodegaOrigen,fecha,IdProducto,codigo,codigo_barra,barra_pallet,
                        fecha_vence,Cantidad_Presentacion,IdDocIngreso,IdDocSalida, IdTransaccion,Clasificacion,
                        Area_Origen, Operador, Codigo_Talla as Talla, Codigo_Color as Color 
                        FROM VW_Movimientos WHERE IdBodegaOrigen=@IdBodegaOrigen"
            Else

                vSQL = "SELECT Propietario,IdBodega,Poliza,Producto,Presentación,
                        [Estado Origen],[Estado Destino],[Unidad de Medida],cantidad,peso,lote,Origen,Destino,
                        [Tipo Tarea],IdBodegaOrigen,fecha,IdProducto,codigo,codigo_barra,barra_pallet,
                        fecha_vence,Cantidad_Presentacion,IdDocIngreso,IdDocSalida,IdTransaccion,Clasificacion,
                        Area_Origen, Operador, Codigo_Talla as Talla, Codigo_Color as Color  FROM VW_Movimientos WHERE IdBodegaOrigen=0"
            End If

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            If Not pLote Is Nothing Then
                If Not pLote.Trim = "" Then
                    vSQL += " AND LOTE LIKE '%" & pLote & "%'"
                End If
            End If

            vSQL += "order by fecha"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    If pIdBodegaOrigen > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodegaOrigen", pIdBodegaOrigen)
                    lDataAdapter.Fill(lTable)
                End Using
            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Movimientos_Dias_Piso(ByVal pIdBodegaOrigen As Integer, ByVal pFechaDel As Date, ByVal pFechaAl As Date, Optional ByVal pLote As String = Nothing) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = ""

            '#GT14022023: se muestran solo despachos, para ver los dias piso que estuvieron almacenados
            vSQL = "SELECT * FROM VW_Movimientos WHERE IdTipoTarea=5 "

            If pIdBodegaOrigen > 0 Then
                vSQL += " AND IdBodegaOrigen=@IdBodegaOrigen"
            End If

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " order by fecha"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    If pIdBodegaOrigen > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodegaOrigen", pIdBodegaOrigen)
                    lDataAdapter.Fill(lTable)
                End Using
            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Movimientos_Kardex(ByVal pIdBodegaOrigen As Integer, ByVal pFechaDel As Date, ByVal pFechaAl As Date, ByVal pIdProductoBodega As String, ByVal pIdPropietarioBodega As Integer) As List(Of clsBeVW_Movimientos)

        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try

            Dim vSQL As String = ""

            If pIdBodegaOrigen > 0 Then

                vSQL = "SELECT * FROM VW_Movimientos_N VW 
                        WHERE VW.Contabilizar = 1 
                        AND VW.IdBodegaOrigen=@IdBodegaOrigen "

            Else
                vSQL = "SELECT * FROM VW_Movimientos_N VW 
                        WHERE VW.Contabilizar = 1 "
            End If

            If pIdProductoBodega > 0 Then
                vSQL += " AND VW.IdProductoBodega=@IdProductoBodega "
            End If

            If pIdPropietarioBodega > 0 Then

                vSQL += " AND VW.IdPropietarioBodega=@IdPropietarioBodega "

            End If

            vSQL += String.Format(" AND cast(VW.Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " ORDER BY VW.FECHA "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    If pIdBodegaOrigen > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodegaOrigen", pIdBodegaOrigen)
                    If pIdProductoBodega > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                    If pIdPropietarioBodega > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                    Dim lTable As New DataTable
                    lDataAdapter.Fill(lTable)

                    Dim Obj As clsBeVW_Movimientos

                    If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lTable.Rows

                            Obj = New clsBeVW_Movimientos

                            clsLnVW_Movimientos.Cargar(Obj, lRow)

                            If lRow("factor") IsNot DBNull.Value AndAlso lRow("factor") IsNot Nothing Then
                                Obj.Factor = CType(lRow("factor"), Double)
                            End If

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT23032022: para cealsa, sin filtro de fecha o propietario. Se usaran de los encabezados del reporte.
    Public Shared Function Get_Movimientos_Kardex_by_Poliza(ByVal pIdPropietarioBodega As Integer, ByVal pFechaDel As DateTime, ByVal pFechaAl As DateTime) As List(Of clsBeVW_Movimientos_Poliza)

        Dim lReturnList As New List(Of clsBeVW_Movimientos_Poliza)

        Try

            Dim vSQL As String = ""


            vSQL = "SELECT DISTINCT * FROM VW_Movimientos_Poliza VW 
                        WHERE VW.Contabilizar = 1 and IdBodega=2 and IdPropietarioBodega=@pIdPropietarioBodega "


            vSQL += String.Format(" AND cast(VW.Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " ORDER BY VW.FECHA "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@pIdPropietarioBodega", pIdPropietarioBodega)

                    'If pIdBodegaOrigen > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodegaOrigen", pIdBodegaOrigen)
                    'If pIdProductoBodega > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                    Dim lTable As New DataTable
                    lDataAdapter.Fill(lTable)

                    Dim Obj As clsBeVW_Movimientos_Poliza

                    If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lTable.Rows

                            Obj = New clsBeVW_Movimientos_Poliza

                            clsLnVW_Movimientos_Poliza.Cargar(Obj, lRow)

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#GT19072022: para cealsa con filtros y valores de OC
    Public Shared Function Get_Movimientos_by_Doc(ByVal pIdBodega As Integer,
                                                    ByVal pIdPropietarioBodega As Integer,
                                                    ByVal pIdArea As Integer,
                                                    ByVal pFechaDel As Date, ByVal pFechaAl As Date) As List(Of clsBeVW_Movimientos_Poliza)

        Dim lReturnList As New List(Of clsBeVW_Movimientos_Poliza)

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT DISTINCT * FROM VW_Movimientos_Documento VW WHERE VW.Contabilizar = 1 "

            If pIdBodega <> 0 Then
                vSQL += " AND  IdBodega =@IdBodega "
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND  IdPropietarioBodega =@IdPropietarioBodega "
            End If

            If pIdArea <> 0 Then
                vSQL += " AND  IdArea =@IdArea "
            End If

            vSQL += String.Format(" AND cast(VW.Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " ORDER BY VW.FECHA "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    If pIdBodega > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                    If pIdPropietarioBodega > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                    If pIdArea > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)

                    Dim lTable As New DataTable
                    lDataAdapter.Fill(lTable)

                    Dim Obj As clsBeVW_Movimientos_Poliza

                    If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lTable.Rows

                            Obj = New clsBeVW_Movimientos_Poliza

                            clsLnVW_Movimientos_Poliza.Cargar(Obj, lRow)

                            lReturnList.Add(Obj)

                        Next

                    End If

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    '#GT13012023: movimientos por propietario llamado tambien estado de cuenta (de movimientos)
    Public Shared Function Get_Movimientos_by_Propietario(ByVal pIdBodega As Integer,
                                                          ByVal pIdPropietarioBodega As Integer,
                                                          ByVal pIdArea As Integer,
                                                          ByVal pFechaDel As Date, ByVal pFechaAl As Date) As List(Of clsBeVW_Movimientos_Propietario)

        Dim lReturnList As New List(Of clsBeVW_Movimientos_Propietario)

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT IdTransaccion,TipoTarea,idpedidoenc,iddespachoenc,IdDespachoDet,observacion,no_ticket_tms,
                 poliza,numero_orden,regimen_ingreso,referencia,IdRecepcion,valor_aduana,valor_dai,valor_iva,Propietario,Producto,
                 presentacion,UMBas,cantidad,fecha,IdProducto,codigo,codigo_barra,IdTipoTarea,Contabilizar,fecha_vence,IdPresentacion,
                 IdUnidadMedida,IdPropietarioBodega,IdBodega,licencia,Clasificacion,Familia,IdMovimiento,Codigo_Bodega_Origen,Nombre_Bodega_Origen,NombreArea,
                 factor,fecha_ingreso_rec,fecha_ingreso_ticket,numero_orden_salida,codigo_poliza_salida,regimen_salida
                    FROM VW_Movimientos_Propietario VW  WHERE VW.Contabilizar = 1 "


            'vSQL += String.Format(" inner join stock_jornada st on vw.licencia= st.lic_plate COLLATE Modern_Spanish_CI_AS and VW.IdStock = st.IdStock and cast(st.Fecha as date) = {0} ", FormatoFechas.fFecha(pFechaDel))

            If pIdBodega <> 0 Then
                vSQL += " AND  VW.IdBodega =@IdBodega "
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND  VW.IdPropietarioBodega =@IdPropietarioBodega "
            End If

            If pIdArea <> 0 Then
                vSQL += " AND  VW.IdArea =@IdArea "
            End If

            vSQL += String.Format(" AND cast(VW.Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " ORDER BY VW.FECHA "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        If pIdBodega > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        If pIdPropietarioBodega > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        'If pIdArea > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdArea", pIdArea)

                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FechaDesde", pFechaDel)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FechaHasta", pFechaAl)

                        Dim lTable As New DataTable
                        lDataAdapter.Fill(lTable)

                        Dim Obj As clsBeVW_Movimientos_Propietario

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            Dim contador As Integer = 0
                            For Each lRow As DataRow In lTable.Rows

                                Obj = New clsBeVW_Movimientos_Propietario

                                contador += 1
                                Debug.Print(contador)

                                If contador = 21 Then
                                    Debug.Write("aqui")
                                End If

                                clsLnVW_Movimientos_propietario.Cargar(Obj, lRow)

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Movimientos_Kardex_Con_Docs(ByVal pIdBodegaOrigen As Integer,
                                                           ByVal pFechaDel As Date,
                                                           ByVal pFechaAl As Date) As List(Of clsBeVW_Movimientos)

        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try

            Dim vSQL As String = ""

            If pIdBodegaOrigen > 0 Then
                vSQL = "SELECT *,P.factor FROM VW_Movimientos_N1 VW left outer JOIN 
                        producto_presentacion P ON P.IdPresentacion = VW.IdPresentacion 
                        WHERE VW.Contabilizar = 1 AND VW.IdBodegaOrigen=@IdBodegaOrigen"
            Else
                vSQL = "SELECT *,P.factor FROM VW_Movimientos_N1 VW left outer JOIN 
                        producto_presentacion P ON P.IdPresentacion = VW.IdPresentacion 
                        WHERE VW.Contabilizar = 1 AND VW.IdBodegaOrigen=0"
            End If

            vSQL += String.Format(" AND cast(VW.Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " ORDER BY VW.FECHA "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodegaOrigen > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodegaOrigen", pIdBodegaOrigen)

                        Dim lTable As New DataTable
                        lDataAdapter.Fill(lTable)

                        Dim Obj As clsBeVW_Movimientos

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows

                                Obj = New clsBeVW_Movimientos

                                clsLnVW_Movimientos.Cargar(Obj, lRow)

                                If lRow("factor") IsNot DBNull.Value AndAlso lRow("factor") IsNot Nothing Then
                                    Obj.Factor = CType(lRow("factor"), Double)
                                End If

                                If lRow("Poliza") IsNot DBNull.Value AndAlso lRow("Poliza") IsNot Nothing Then
                                    Obj.Poliza = CType(lRow("Poliza"), String)
                                End If

                                If lRow("barra_pallet") IsNot DBNull.Value AndAlso lRow("barra_pallet") IsNot Nothing Then
                                    Obj.Lic_Plate = CType(lRow("barra_pallet"), String)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Movimientos_Kardex_Con_Docs(ByVal pIdBodegaOrigen As Integer,
                                                           ByVal pFechaDel As Date,
                                                           ByVal pFechaAl As Date,
                                                           ByVal Lote As String) As List(Of clsBeVW_Movimientos)

        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try

            Dim vSQL As String = ""

            If pIdBodegaOrigen > 0 Then
                vSQL = "SELECT *,P.factor FROM VW_Movimientos_N1 VW left outer JOIN 
                        producto_presentacion P ON P.IdPresentacion = VW.IdPresentacion 
                        WHERE VW.Contabilizar = 1 AND VW.IdBodegaOrigen=@IdBodegaOrigen"
            Else
                vSQL = "SELECT *,P.factor FROM VW_Movimientos_N1 VW left outer JOIN 
                        producto_presentacion P ON P.IdPresentacion = VW.IdPresentacion 
                        WHERE VW.Contabilizar = 1 AND VW.IdBodegaOrigen=0"
            End If

            vSQL += String.Format(" AND cast(VW.Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            If Not Lote.Trim = "" Then
                vSQL += " AND LOTE LIKE '%" & Lote & "%'"
            End If

            vSQL += " ORDER BY VW.FECHA "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodegaOrigen > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodegaOrigen", pIdBodegaOrigen)

                        Dim lTable As New DataTable
                        lDataAdapter.Fill(lTable)

                        Dim Obj As clsBeVW_Movimientos

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows

                                Obj = New clsBeVW_Movimientos

                                clsLnVW_Movimientos.Cargar(Obj, lRow)

                                If lRow("factor") IsNot DBNull.Value AndAlso lRow("factor") IsNot Nothing Then
                                    Obj.Factor = CType(lRow("factor"), Double)
                                End If

                                If lRow("Poliza") IsNot DBNull.Value AndAlso lRow("Poliza") IsNot Nothing Then
                                    Obj.Poliza = CType(lRow("Poliza"), String)
                                End If

                                If lRow("barra_pallet") IsNot DBNull.Value AndAlso lRow("barra_pallet") IsNot Nothing Then
                                    Obj.Lic_Plate = CType(lRow("barra_pallet"), String)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetMovimientosPorDocumento() As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT * FROM VW_Movimientos_N "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.Fill(lTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllMovimientosCambioUbic() As List(Of clsBeVW_Movimientos)

        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try

            Dim vSQL As String = "SELECT sis_tipo_tarea.Nombre AS TipoTarea,producto.codigo AS Codigo,producto.nombre AS Producto,trans_movimientos.cantidad AS Cantidad, 
                    trans_movimientos.peso AS Peso,trans_movimientos.lote AS Lote,trans_movimientos.fecha_vence AS Vence,producto_estado.nombre AS Estado, 
                    motivo_ubicacion.Nombre AS Motivo,propietarios.nombre_comercial AS Propietario,trans_movimientos.IdUbicacionOrigen,trans_movimientos.IdUbicacionDestino,trans_movimientos.fecha,ubicO.IdBodega AS IdBodegaOrigen,ubicD.IdBodega AS IdBodegaDestino
                    FROM trans_movimientos INNER JOIN
                    sis_tipo_tarea ON trans_movimientos.IdTipoTarea = sis_tipo_tarea.IdTipoTarea INNER JOIN
                    trans_ubic_hh_enc ON trans_movimientos.IdTransaccion = trans_ubic_hh_enc.IdTareaUbicacionEnc INNER JOIN
                    producto_bodega ON trans_movimientos.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
                    producto ON producto_bodega.IdProducto = producto.IdProducto INNER JOIN
                    producto_estado ON trans_movimientos.IdEstadoOrigen = producto_estado.IdEstado AND 
                    trans_movimientos.IdEstadoDestino = producto_estado.IdEstado INNER JOIN
                    motivo_ubicacion ON trans_ubic_hh_enc.IdMotivoUbicacion = motivo_ubicacion.IdMotivoUbicacion INNER JOIN
                    propietarios ON producto_estado.IdPropietario = propietarios.IdPropietario AND 
                    producto_estado.IdPropietario = propietarios.IdPropietario INNER JOIN
                    propietario_bodega ON trans_movimientos.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega AND 
                    propietarios.IdPropietario = propietario_bodega.IdPropietario inner join 
                    bodega_ubicacion ubicO on trans_movimientos.IdUbicacionOrigen = ubicO.IdUbicacion inner join 
                    bodega_ubicacion ubicD on trans_movimientos.IdUbicacionDestino = ubicD.Idubicacion
                    WHERE (sis_tipo_tarea.Nombre = 'UBIC')"

            vSQL += "ORDER BY fecha desc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        Dim lTable As New DataTable
                        lDataAdapter.Fill(lTable)

                        Dim Obj As clsBeVW_Movimientos

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows

                                Obj = New clsBeVW_Movimientos

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    Obj.Propietario = CType(lRow("Propietario"), String)
                                End If

                                If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                    Obj.Producto = CType(lRow("Producto"), String)
                                End If

                                If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                                    Obj.Cantidad = CType(lRow("Cantidad"), Double)
                                End If

                                If lRow("Peso") IsNot DBNull.Value AndAlso lRow("Peso") IsNot Nothing Then
                                    Obj.Peso = CType(lRow("Peso"), Double)
                                End If

                                If lRow("Lote") IsNot DBNull.Value AndAlso lRow("Lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("Lote"), String)
                                End If

                                If lRow("TipoTarea") IsNot DBNull.Value AndAlso lRow("TipoTarea") IsNot Nothing Then
                                    Obj.TTarea = CType(lRow("TipoTarea"), String)
                                End If

                                If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                    Obj.Codigo = CType(lRow("Codigo"), String)
                                End If

                                If lRow("Estado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                                    Obj.EstadoDestino = CType(lRow("Estado"), String)
                                End If

                                If lRow("Motivo") IsNot DBNull.Value AndAlso lRow("Motivo") IsNot Nothing Then
                                    Obj.Motivo = CType(lRow("Motivo"), String)
                                End If

                                If lRow("Vence") IsNot DBNull.Value AndAlso lRow("Vence") IsNot Nothing Then
                                    Obj.Fecha_Vence = CType(lRow("Vence"), Date)
                                End If

                                If lRow("IdUbicacionOrigen") IsNot DBNull.Value AndAlso lRow("IdUbicacionOrigen") IsNot Nothing Then
                                    Dim Ubicacion As New clsBeBodega_ubicacion
                                    Ubicacion = clsLnBodega_ubicacion.GetSingle(lRow("IdUbicacionOrigen"), lRow("IdBodegaOrigen"))
                                    Obj.UbicOrigen = Ubicacion.NombreCompleto
                                End If

                                If lRow("IdUbicacionDestino") IsNot DBNull.Value AndAlso lRow("IdUbicacionDestino") IsNot Nothing Then
                                    Dim Ubicacion As New clsBeBodega_ubicacion
                                    Ubicacion = clsLnBodega_ubicacion.GetSingle(lRow("IdUbicacionDestino"), lRow("IdBodegaDestino"))
                                    Obj.UbicDestino = Ubicacion.NombreCompleto
                                End If

                                If lRow("fecha") IsNot DBNull.Value AndAlso lRow("fecha") IsNot Nothing Then
                                    Obj.Fecha = CType(lRow("fecha"), Date)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CM_20190301: Devuelve cambios de ubicación, se añadió para refactorizar reporte. 
    Public Shared Function Get_All_Movimientos_Cambio_Ubic_DT(ByVal pFechaDel As Date,
                                                     ByVal pFechaAl As Date,
                                                     ByVal pIdProductoBodega As Integer,
                                                     ByVal pIdBodega As Integer,
                                                     ByVal pIdPropietarioBodega As Integer) As DataTable

        Get_All_Movimientos_Cambio_Ubic_DT = Nothing

        Try

            Dim vSQL As String = String.Empty

            vSQL = "SELECT * FROM VW_Cambios_Ubicacion WHERE 1 > 0 "

            vSQL += " AND IdBodegaOrigen=@IdBodega and IdPropietarioBodega=@IdPropietarioBodega "

            If pIdProductoBodega <> 0 Then
                vSQL += " AND  IdProductoBodega =@IdProductoBodega "
            End If

            'EJC20260602_STOCK_FECHA: Filtro sargable para mejorar uso de índices por fecha.
            vSQL += " And Fecha >= @FechaDel And Fecha < @FechaAlExclusiva "

            vSQL += " ORDER BY Fecha DESC "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FechaDel", pFechaDel.Date)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FechaAlExclusiva", pFechaAl.Date.AddDays(1))

                        If pIdProductoBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        End If

                        Dim lTable As New DataTable
                        lDataAdapter.Fill(lTable)


                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            Return lTable

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


    '#GT28102024: devuelve cambios de estado, es un clon del metódo anterior

    Public Shared Function Get_All_Movimientos_Cambio_Estado_DT(ByVal pFechaDel As Date,
                                                                ByVal pFechaAl As Date,
                                                                ByVal pIdProductoBodega As Integer,
                                                                ByVal pIdBodega As Integer,
                                                                ByVal pIdPropietarioBodega As Integer) As DataTable

        Get_All_Movimientos_Cambio_Estado_DT = Nothing

        Try

            Dim vSQL As String = String.Empty

            vSQL = "SELECT * FROM VW_Cambios_Estado WHERE 1 > 0 "

            vSQL += " AND IdBodegaOrigen=@IdBodega and IdPropietarioBodega=@IdPropietarioBodega "

            If pIdProductoBodega <> 0 Then
                vSQL += " AND  IdProductoBodega =@IdProductoBodega "
            End If

            'EJC20260602_STOCK_FECHA: Filtro sargable para mejorar uso de índices por fecha.
            vSQL += " And Fecha >= @FechaDel And Fecha < @FechaAlExclusiva "

            vSQL += " ORDER BY Fecha DESC "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FechaDel", pFechaDel.Date)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FechaAlExclusiva", pFechaAl.Date.AddDays(1))

                        If pIdProductoBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        End If

                        Dim lTable As New DataTable
                        lDataAdapter.Fill(lTable)


                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            Get_All_Movimientos_Cambio_Estado_DT = lTable
                            'Return lTable
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

    Public Shared Function GetAllMovimientosByLote(ByVal lote As String, ByVal IdBodega As Integer) As List(Of clsBeVW_Movimientos)

        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try

            Dim vSQL As String = ""

            If lote <> "" Then
                vSQL = " SELECT * From VW_Movimientos Where IdBodegaOrigen=@IdBodega And lote Like '%' + '" + lote + "' + '%' "
            Else
                vSQL = "SELECT * From VW_Movimientos Where IdBodegaOrigen=@IdBodega "
            End If
            vSQL += "ORDER By fecha"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        Dim Obj As clsBeVW_Movimientos

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows

                                Obj = New clsBeVW_Movimientos

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    Obj.Propietario = CType(lRow("Propietario"), String)
                                End If

                                If lRow("Poliza") IsNot DBNull.Value AndAlso lRow("Poliza") IsNot Nothing Then
                                    Obj.Poliza = CType(lRow("Poliza"), String)
                                End If

                                If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                    Obj.Producto = CType(lRow("Producto"), String)
                                End If

                                If lRow("Presentación") IsNot DBNull.Value AndAlso lRow("Presentación") IsNot Nothing Then
                                    Obj.Presentacion = CType(lRow("Presentación"), String)
                                End If

                                If lRow("Estado Origen") IsNot DBNull.Value AndAlso lRow("Estado Origen") IsNot Nothing Then
                                    Obj.EstadoOrigen = CType(lRow("Estado Origen"), String)
                                End If

                                If lRow("Estado Destino") IsNot DBNull.Value AndAlso lRow("Estado Destino") IsNot Nothing Then
                                    Obj.EstadoDestino = CType(lRow("Estado Destino"), String)
                                End If

                                If lRow("Unidad de Medida") IsNot DBNull.Value AndAlso lRow("Unidad de Medida") IsNot Nothing Then
                                    Obj.UMBas = CType(lRow("Unidad de Medida"), String)
                                End If

                                If lRow("cantidad") IsNot DBNull.Value AndAlso lRow("cantidad") IsNot Nothing Then
                                    Obj.Cantidad = CType(lRow("cantidad"), Double)
                                End If

                                If lRow("peso") IsNot DBNull.Value AndAlso lRow("peso") IsNot Nothing Then
                                    Obj.Peso = CType(lRow("peso"), Double)
                                End If

                                If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("lote"), String)
                                End If

                                If lRow("Origen") IsNot DBNull.Value AndAlso lRow("Origen") IsNot Nothing Then
                                    Obj.UbicOrigen = CType(lRow("Origen"), String)
                                End If

                                If lRow("Destino") IsNot DBNull.Value AndAlso lRow("Destino") IsNot Nothing Then
                                    Obj.UbicDestino = CType(lRow("Destino"), String)
                                End If

                                If lRow("Tipo Tarea") IsNot DBNull.Value AndAlso lRow("Tipo Tarea") IsNot Nothing Then
                                    Obj.TTarea = CType(lRow("Tipo Tarea"), String)
                                End If

                                If lRow("IdBodegaOrigen") IsNot DBNull.Value AndAlso lRow("IdBodegaOrigen") IsNot Nothing Then
                                    Obj.IdBodegaOrigen = CType(lRow("IdBodegaOrigen"), Integer)
                                End If

                                If lRow("fecha") IsNot DBNull.Value AndAlso lRow("fecha") IsNot Nothing Then
                                    Obj.Fecha = CType(lRow("fecha"), Date)
                                End If

                                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                    Obj.Fecha_Vence = CType(lRow("fecha_vence"), Date)
                                End If

                                If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                    Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                                End If

                                If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                                    Obj.Codigo = CType(lRow("codigo"), String)
                                End If

                                If lRow("codigo_barra") IsNot DBNull.Value AndAlso lRow("codigo_barra") IsNot Nothing Then
                                    Obj.CodigoBarra = CType(lRow("codigo_barra"), String)
                                End If

                                If lRow("barra_pallet") IsNot DBNull.Value AndAlso lRow("barra_pallet") IsNot Nothing Then
                                    Obj.Lic_Plate = CType(lRow("barra_pallet"), String)
                                End If

                                If lRow("Cantidad_Presentacion") IsNot DBNull.Value AndAlso lRow("Cantidad_Presentacion") IsNot Nothing Then
                                    Obj.Cantidad_Presentacion = CType(lRow("Cantidad_Presentacion"), Double)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()


            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetMovimientosSalidas(ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = String.Format("SELECT * FROM VW_Movimientos WHERE [Tipo Tarea]='DESP' AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_By_Producto(ByVal pIdBodegaOrigen As Integer, ByVal pFechaDel As Date, ByVal pFechaAl As Date, ByVal pIdProducto As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = ""

            If pIdBodegaOrigen > 0 Then
                vSQL = "SELECT * FROM VW_Movimientos WHERE IdBodegaOrigen=@IdBodegaOrigen AND IdProducto=@IdProducto"
            Else
                vSQL = "SELECT * FROM VW_Movimientos WHERE IdProducto=@IdProducto"
            End If

            vSQL += String.Format(" AND cast(Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        If pIdBodegaOrigen > 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodegaOrigen", pIdBodegaOrigen)
                        End If

                        lDataAdapter.Fill(lTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Cantidad_Movimientos_By_IdProducto(ByVal pIdProducto As Integer) As Integer

        Get_Cantidad_Movimientos_By_IdProducto = 0

        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT count(idmovimiento) as Movimientos
                    FROM VW_Movimientos_N 
                    WHERE IdProducto=@IdProducto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdProducto <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        End If

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            Get_Cantidad_Movimientos_By_IdProducto = IIf(IsDBNull(lTable.Rows(0).Item("Movimientos")), 0, lTable.Rows(0).Item("Movimientos"))

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

    Public Shared Function Get_All_Movimientos_By_IdProducto(ByVal pFechaDel As Date,
                                                             ByVal pFechaAl As Date,
                                                             ByVal pIdProductoBodega As Integer,
                                                             ByVal pIdBodega As Integer,
                                                             ByVal pIdPropietarioBodega As Integer,
                                                             ByVal pLote As String,
                                                             Optional ByVal pSoloProductosConStock As Boolean = False) As List(Of clsBeVW_Movimientos)

        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter("dbo.usp_Reporte_StockEnFecha_Movimientos", lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.StoredProcedure
                        'EJC20260602_STOCK_FECHA_TIMEOUT: el reporte puede procesar volúmenes altos; evitar timeout por default (30s).
                        lDTA.SelectCommand.CommandTimeout = 180
                        lDTA.SelectCommand.Parameters.AddWithValue("@FechaDel", pFechaDel.Date)
                        lDTA.SelectCommand.Parameters.AddWithValue("@FechaAl", pFechaAl.Date)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        If pIdProductoBodega = 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", DBNull.Value)
                        Else
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        End If
                        If pLote = "" Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@Lote", DBNull.Value)
                        Else
                            lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pLote)
                        End If
                        lDTA.SelectCommand.Parameters.AddWithValue("@SoloProductosConStock", pSoloProductosConStock)

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        Dim Obj As clsBeVW_Movimientos

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows
                                Obj = New clsBeVW_Movimientos
                                clsLnVW_Movimientos.Cargar_StockEnFecha(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_By_IdProducto(ByVal pFechaDel As Date,
                                                             ByVal pFechaAl As Date,
                                                             ByVal pIdProductoBodega As Integer,
                                                             ByVal pIdBodega As Integer,
                                                             ByVal pIdPropietarioBodega As Integer,
                                                             ByRef lConnection As SqlConnection,
                                                             ByRef lTransaction As SqlTransaction,
                                                             Optional ByVal pSoloProductosConStock As Boolean = False) As List(Of clsBeVW_Movimientos)

        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try

            Using lDTA As New SqlDataAdapter("dbo.usp_Reporte_StockEnFecha_Movimientos", lConnection)

                lDTA.SelectCommand.CommandType = CommandType.StoredProcedure
                lDTA.SelectCommand.Transaction = lTransaction
                'EJC20260602_STOCK_FECHA_TIMEOUT: el reporte puede procesar volúmenes altos; evitar timeout por default (30s).
                lDTA.SelectCommand.CommandTimeout = 180
                lDTA.SelectCommand.Parameters.AddWithValue("@FechaDel", pFechaDel.Date)
                lDTA.SelectCommand.Parameters.AddWithValue("@FechaAl", pFechaAl.Date)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                If pIdProductoBodega = 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", DBNull.Value)
                Else
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                End If
                lDTA.SelectCommand.Parameters.AddWithValue("@Lote", DBNull.Value)
                lDTA.SelectCommand.Parameters.AddWithValue("@SoloProductosConStock", pSoloProductosConStock)

                Dim lTable As New DataTable
                lDTA.Fill(lTable)

                Dim Obj As clsBeVW_Movimientos

                If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lTable.Rows
                        Obj = New clsBeVW_Movimientos
                        clsLnVW_Movimientos.Cargar_StockEnFecha(Obj, lRow)
                        lReturnList.Add(Obj)
                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_By_Lote(ByVal pFechaDel As Date,
                                                       ByVal pFechaAl As Date,
                                                       ByVal pIdProducto As Integer,
                                                       ByVal lote As String,
                                                       ByVal fecha As Date) As List(Of clsBeVW_Movimientos)

        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT Codigo,Producto, SUM(cantidad) AS Cantidad,
                        EstadoOrigen, 
                        EstadoDestino, 
                        TipoTarea, lote,Fecha_Vence, IdTipoTarea, 
                        IdPresentacion, IdUnidadMedida, IdEstadoOrigen, 
                        IdProductoBodega,Fecha,
                        Umbas, EstadoOrigen, Presentación
                        FROM VW_Movimientos_N 
                        WHERE IdProducto=@IdProducto and lote=@lote and fecha_vence=@fecha 
                        AND TIPOTAREA NOT IN ('AJCANTNI','AJCANTPI')"

            vSQL += String.Format(" And Fecha BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " GROUP BY codigo,producto,EstadoOrigen, 
                    EstadoDestino, IdProductoBodega,
                    TipoTarea, lote,fecha_vence, 
                    IdTipoTarea,Fecha, IdPresentacion, 
                    IdUnidadMedida, IdEstadoOrigen,
                    Umbas, EstadoOrigen, Presentación"

            vSQL += " ORDER BY Codigo, Lote, Fecha"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@lote", lote)
                        lDTA.SelectCommand.Parameters.AddWithValue("@fecha", fecha)
                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        Dim Obj As clsBeVW_Movimientos

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows
                                Obj = New clsBeVW_Movimientos
                                clsLnVW_Movimientos.Cargar(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_By_Rango_Fechas(ByVal pFechaDel As Date,
                                                               ByVal pFechaAl As Date,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction) As List(Of clsBeVW_Movimientos)


        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try
            Dim vSQL As String = "SELECT t.codigo, t.Producto, t.EstadoOrigen,  t.EstadoDestino,
                                         t.TipoTarea, t.lote,t.Fecha_Vence, t.IdPresentacion, t.IdUnidadMedida, t.IdEstadoOrigen,
                                         t.IdProductoBodega,t.Fecha, t.Umbas, t.Presentación,t.IdTipoTarea,
                                         sum(t.Ingresos) as Ingresos,
                                         sum(t.Salidas) as Salidas,
                                         sum(t.Ajustes_Positivos) as Ajustes_Positivos,
                                         sum(t.Ajustes_Negativos) as Ajustes_Negativos,
						                 sum(t.EnMovimiento) as EnMovimiento,
						                 t.IdUbicacionorigen, t.IdUbicaciondestino, t.Licencia, t.Fecha
                                  FROM(
                                        SELECT Codigo,Producto, EstadoOrigen,
                                        EstadoDestino,
                                        TipoTarea, lote,Fecha_Vence, IdTipoTarea,
                                        IdPresentacion, IdUnidadMedida, IdEstadoOrigen,IdEstadoDestino,
                                        IdProductoBodega,Fecha,
                                        Umbas, Presentación,IdUbicacionorigen, 
                                        CASE WHEN idtipotarea <> 5 then IdUbicaciondestino ELSE
						                                                   ISNULL((SELECT TOP 1 k.IdUbicacionPicking
                                                                            FROM trans_picking_enc k inner join trans_pe_enc p on k.IdPickingEnc = p.IdPickingEnc
                                                                            WHERE p.IdPedidoEnc in (SELECT d.IdPedidoEnc 
						                                                                            FROM trans_despacho_det d
												                                                    WHERE d.IdDespachoEnc = IdTransaccion )),IdUbicaciondestino) end IdUbicaciondestino, Licencia,
                                        case when IdTipoTarea = 1  then SUM(cantidad) else 0 end AS Ingresos,
                                        case when IdTipoTarea = 5  then SUM(cantidad) else 0 end AS Salidas,
                                        case when IdTipoTarea = 13 then SUM(cantidad) else 0 end AS Ajustes_Positivos,
                                        case when IdTipoTarea = 17 then SUM(cantidad) else 0 end AS Ajustes_Negativos,
                                        case when (IdTipoTarea = 8) then SUM(cantidad) else 0 end AS EnMovimiento                        
                                        FROM VW_Movimientos_N
                                        WHERE  TIPOTAREA NOT IN ('AJCANTNI','AJCANTPI','VERI','UBIC','CEST','PACK',
                                                                 'REEMP_NE_PICK', 'REEMP_ME_PICK','REEMP_BE_PICK')  "

            vSQL += String.Format("And Fecha BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " GROUP BY codigo,producto,EstadoOrigen, EstadoDestino, IdProductoBodega,
                               TipoTarea, lote,fecha_vence, IdTransaccion,
                               IdTipoTarea,Fecha, IdPresentacion,
                               IdUnidadMedida, IdEstadoOrigen,IdEstadoDestino,
                               Umbas, EstadoOrigen, Presentación, IdUbicacionorigen, IdUbicaciondestino,Licencia, Fecha
                      UNION ALL 
                                SELECT Codigo,Producto, EstadoOrigen,
                                                        EstadoOrigen EstadoDestino,
                                                        TipoTarea, lote,Fecha_Vence, IdTipoTarea,
                                                        IdPresentacion, IdUnidadMedida, IdEstadoOrigen,IdEstadoOrigen IdEstadoDestino,
                                                        IdProductoBodega,Fecha,
                                                        Umbas, Presentación,IdUbicacionorigen, IdUbicacionorigen IdUbicaciondestino, Licencia,
						                                0 AS Ingresos,
                                                        sum(Cantidad) AS Salidas,
                                                        0 AS Ajustes_Positivos,
                                                        0 AS Ajustes_Negativos, 
						                                0 EnMovimiento
                                FROM VW_Movimientos_N
                                WHERE  TIPOTAREA in ('UBIC','CEST','REEMP_NE_PICK', 'REEMP_ME_PICK','REEMP_BE_PICK')  "

            vSQL += String.Format(" And Fecha BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " GROUP BY Codigo,Producto, EstadoOrigen, TipoTarea, lote,Fecha_Vence, IdTipoTarea,
                               IdPresentacion, IdUnidadMedida, IdEstadoOrigen,IdUbicaciondestino,
                               IdProductoBodega,Fecha, Umbas, Presentación,IdUbicacionorigen, Licencia
                      UNION ALL
                      SELECT Codigo,Producto, EstadoDestino EstadoOrigen,EstadoDestino,
                             TipoTarea, lote,Fecha_Vence, IdTipoTarea,
                             IdPresentacion, IdUnidadMedida, IdEstadoDestino IdEstadoOrigen,IdEstadoDestino,
                             IdProductoBodega,Fecha,
                             Umbas, Presentación,IdUbicaciondestino IdUbicacionorigen, IdUbicaciondestino, Licencia,
						     SUM(Cantidad) AS Ingresos,
                             0 AS Salidas,
                             0 AS Ajustes_Positivos,
                             0 AS Ajustes_Negativos, 
						     0 Cantidad
                      FROM VW_Movimientos_N
                      WHERE TIPOTAREA in ('UBIC','CEST','REEMP_NE_PICK', 'REEMP_ME_PICK','REEMP_BE_PICK')   "

            vSQL += String.Format(" And Fecha BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " GROUP BY Codigo,Producto, EstadoDestino, TipoTarea, lote,Fecha_Vence, IdTipoTarea,IdEStadoDestino,
                               IdPresentacion, IdUnidadMedida, IdProductoBodega, Fecha, Umbas, Presentación,IdUbicacionDestino, Licencia
                      UNION ALL
                      SELECT Codigo,Producto, EstadoDestino EstadoOrigen, EstadoDestino,
                             TipoTarea, lote,Fecha_Vence, IdTipoTarea,
                             IdPresentacion, IdUnidadMedida,IdEstadoDestino IdEstadoOrigen, IdEstadoDestino,
                             IdProductoBodega,Fecha,
                             Umbas, Presentación,IdUbicaciondestino IdUbicacionorigen, IdUbicaciondestino, 
							 Licencia,
						     SUM(Cantidad) AS Ingresos,
                             0 AS Salidas,
                             0 AS Ajustes_Positivos,
                             0 AS Ajustes_Negativos, 
						     0 Cantidad
                      FROM VW_Movimientos_N
                      WHERE TIPOTAREA in ('PACK') "

            vSQL += String.Format(" And Fecha BETWEEN {0} And {1} {2} ", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl), vbCrLf)

            vSQL += " GROUP BY Codigo,Producto, EstadoDestino, TipoTarea, lote,Fecha_Vence, IdTipoTarea,
					           IdEstadoDestino, IdPresentacion, IdUnidadMedida, IdProductoBodega, Fecha, Umbas,
							   Presentación,IdUbicaciondestino,  Licencia  
                      UNION ALL
                      SELECT Codigo,Producto, EstadoDestino EstadoOrigen,
                                            EstadoDestino,
                                            'DESP' TipoTarea, lote,Fecha_Vence, 5 IdTipoTarea,
                                            IdPresentacion, IdUnidadMedida, IdEstadoOrigen,
                                            IdEstadoDestino,
                                            IdProductoBodega,Fecha,
                                            Umbas, Presentación,IdUbicacionorigen, 
                                            ISNULL((SELECT TOP 1 k.IdUbicacionPicking
                                                    FROM trans_picking_enc k 
                                                    WHERE k.IdPickingEnc = IdTransaccion),IdUbicaciondestino) IdUbicaciondestino, 
                                            Licencia,
						                    0 AS Ingresos,
                                            sum(-Cantidad) AS Salidas,
                                            0 AS Ajustes_Positivos,
                                            0 AS Ajustes_Negativos, 
						                    0 Cantidad
                      FROM VW_Movimientos_N
                      WHERE TIPOTAREA in ('PIK')   "

            vSQL += String.Format(" And Fecha BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " GROUP BY Codigo,Producto, EstadoDestino,IdEstadoOrigen,
                                            TipoTarea, lote,Fecha_Vence, IdTipoTarea,IdEStadoDestino,
                                            IdPresentacion, IdUnidadMedida,
                                            IdProductoBodega,Fecha,
                                            Umbas, Presentación,IdUbicacionOrigen,IdUbicacionDestino, Licencia, IdTransaccion) AS t
                      GROUP BY t.codigo, t.Producto, t.EstadoOrigen,  t.EstadoDestino, t.TipoTarea, t.lote,t.Fecha_Vence, 
                               t.IdPresentacion, t.IdUnidadMedida, t.IdProductoBodega,t.Fecha, t.Umbas, t.Presentación, 
                               t.IdEstadoOrigen,t.IdTipoTarea, t.IdUbicacionorigen, t.IdUbicaciondestino, t.Licencia, t.Fecha
                      ORDER BY t.Codigo, t.Lote, t.Fecha "

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.CommandType = CommandType.Text

            Dim lTable As New DataTable
            dad.Fill(lTable)

            Dim Obj As clsBeVW_Movimientos

            If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                For Each lRow As DataRow In lTable.Rows
                    Obj = New clsBeVW_Movimientos
                    clsLnVW_Movimientos.Cargar(Obj, lRow)
                    lReturnList.Add(Obj)
                Next

            End If

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_By_Rango_Fechas(ByVal pFechaDel As Date,
                                                               ByVal pFechaAl As Date,
                                                               Optional ByVal pIdPropietarioBodega As Integer = Nothing) As List(Of clsBeVW_Movimientos)

        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try

            Dim vSQL As String = "SELECT t.codigo, t.Producto, t.EstadoOrigen,  t.EstadoDestino,
                        t.TipoTarea, t.lote,t.Fecha_Vence, t.IdPresentacion, t.IdUnidadMedida, t.IdEstadoOrigen,
                        t.IdProductoBodega,t.Fecha, t.Umbas, t.Presentación,t.IdTipoTarea,
                        sum(t.Ingresos) as Ingresos,
                        sum(t.Salidas) as Salidas,
                        sum(t.Ajustes_Positivos) as Ajustes_Positivos,
                        sum(t.Ajustes_Negativos) as Ajustes_Negativos
                    from(SELECT Codigo,Producto, EstadoOrigen,
                        EstadoDestino,
                        TipoTarea, lote,Fecha_Vence, IdTipoTarea,
                        IdPresentacion, IdUnidadMedida, IdEstadoOrigen,
                        IdProductoBodega,Fecha,
                        Umbas, Presentación,
                        case when IdTipoTarea = 6 then SUM(cantidad) else 0 end AS INVE,
                        case when IdTipoTarea = 1 then SUM(cantidad) else 0 end AS Ingresos,
                        case when IdTipoTarea = 5 then SUM(cantidad) else 0 end AS Salidas,
                        case when IdTipoTarea = 13 then SUM(cantidad) else 0 end AS Ajustes_Positivos,
                        case when IdTipoTarea = 17 then SUM(cantidad) else 0 end AS Ajustes_Negativos
                        FROM VW_Movimientos_N
                        WHERE  TIPOTAREA NOT IN ('AJCANTNI','AJCANTPI') "

            vSQL += String.Format("And Fecha BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))


            If pIdPropietarioBodega <> 0 Then

                vSQL += "and IdPropietarioBodega= @pIdPropietarioBodega"

            End If

            vSQL += " GROUP BY codigo,producto,EstadoOrigen,
                        EstadoDestino, IdProductoBodega,
                        TipoTarea, lote,fecha_vence,
                        IdTipoTarea,Fecha, IdPresentacion,
                        IdUnidadMedida, IdEstadoOrigen,
                        Umbas, EstadoOrigen, Presentación) AS t
                    group by t.codigo, t.Producto, t.EstadoOrigen,  t.EstadoDestino,
                                t.TipoTarea, t.lote,t.Fecha_Vence, t.IdPresentacion, t.IdUnidadMedida,
                                t.IdProductoBodega,t.Fecha, t.Umbas, t.Presentación, t.IdEstadoOrigen,t.IdTipoTarea
                    ORDER BY t.Codigo, t.Lote, t.Fecha"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.CommandType = CommandType.Text


                    If pIdPropietarioBodega <> 0 Then

                        dad.SelectCommand.Parameters.AddWithValue("@pIdPropietarioBodega", pIdPropietarioBodega)

                    End If


                    Dim lTable As New DataTable
                    dad.Fill(lTable)

                    Dim Obj As clsBeVW_Movimientos

                    If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lTable.Rows
                            Obj = New clsBeVW_Movimientos
                            clsLnVW_Movimientos.Cargar(Obj, lRow)
                            lReturnList.Add(Obj)
                        Next

                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Movimiento_Despacho_By_Stock(ByVal pBeStockAnt As clsBeStock) As List(Of clsBeTrans_movimientos)

        Dim lReturnList As New List(Of clsBeTrans_movimientos)

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT * FROM trans_Movimientos 
                    WHERE IdProductoBodega = @IdProductoBodega                     
                    AND IdEstadoDestino = @IdEstadoDestino 
                    AND lote = @Lote
                    AND IdUnidadMedida = @IdUnidadMedida 
                    AND ISNULL(IdPresentacion,0) = @IdPresentacion 
                    AND fecha_vence = @FechaVence
                    AND IdTipoTarea = 5"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pBeStockAnt.IdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdEstadoDestino", pBeStockAnt.IdProductoEstado)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pBeStockAnt.Lote)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pBeStockAnt.IdUnidadMedida)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pBeStockAnt.IdPresentacion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@FechaVence", pBeStockAnt.Fecha_vence)

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        Dim Obj As clsBeTrans_movimientos

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows
                                Obj = New clsBeTrans_movimientos
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Movimiento_By_Stock(ByVal pBeStockAnt As clsBeStock,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_movimientos)

        Dim lReturnList As New List(Of clsBeTrans_movimientos)

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT * FROM trans_Movimientos 
                     WHERE IdProductoBodega = @IdProductoBodega
                     AND cantidad = @Cantidad 
                     AND IdEstadoDestino = @IdEstadoDestino 
                     AND lote = @Lote
                     AND IdUnidadMedida = @IdUnidadMedida 
                     AND ISNULL(IdPresentacion,0) = @IdPresentacion 
                     AND (fecha_vence IS NULL OR fecha_vence = @FechaVence)
                     AND IdRecepcion = @IdRecepcionEnc
                     AND IdRecepcionDet=@IdRecepcioDet"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pBeStockAnt.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@Cantidad", pBeStockAnt.Cantidad)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdEstadoDestino", pBeStockAnt.IdProductoEstado)
                lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pBeStockAnt.Lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pBeStockAnt.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pBeStockAnt.IdPresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@FechaVence", pBeStockAnt.Fecha_vence)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pBeStockAnt.IdRecepcionEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcioDet", pBeStockAnt.IdRecepcionDet)

                Dim lTable As New DataTable
                lDTA.Fill(lTable)

                Dim Obj As clsBeTrans_movimientos

                If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lTable.Rows
                        Obj = New clsBeTrans_movimientos
                        Cargar(Obj, lRow)
                        lReturnList.Add(Obj)
                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function TieneMovimiento(ByVal pIdProducto As Integer) As List(Of clsBeTrans_movimientos)

        Dim lReturnList As New List(Of clsBeTrans_movimientos)

        Try

            Dim vSQL As String = "SELECT p.nombre AS Producto, stt.Nombre AS TipoTarea, m.IdBodegaOrigen, m.fecha, p.IdProducto, p.codigo, p.codigo_barra
                                FROM trans_movimientos AS m LEFT OUTER JOIN
                                    propietario_bodega AS prb ON m.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                                    propietarios AS pr ON prb.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                                    producto_bodega AS pb ON m.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                                    producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                                    bodega_ubicacion AS u1 ON m.IdUbicacionOrigen = u1.IdUbicacion LEFT OUTER JOIN
                                    bodega_ubicacion AS u2 ON m.IdUbicacionDestino = u2.IdUbicacion LEFT OUTER JOIN
                                    producto_presentacion AS pp ON m.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto LEFT OUTER JOIN
                                    producto_estado AS pe1 ON m.IdEstadoOrigen = pe1.IdEstado AND pe1.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                                    producto_estado AS pe2 ON m.IdEstadoDestino = pe2.IdEstado AND pe2.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                                    unidad_medida AS u ON m.IdUnidadMedida = u.IdUnidadMedida AND u.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                                    sis_tipo_tarea AS stt ON m.IdTipoTarea = stt.IdTipoTarea
                                WHERE p.IdProducto=@IdProducto
                                GROUP BY p.nombre, stt.Nombre, m.IdBodegaOrigen, m.fecha, p.IdProducto, p.codigo, p.codigo_barra"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDTA As New DataTable

                        lDataAdapter.Fill(lDTA)

                        Dim Obj As clsBeTrans_movimientos

                        If lDTA IsNot Nothing AndAlso lDTA.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDTA.Rows

                                Obj = New clsBeTrans_movimientos

                                If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                    Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                                End If

                                If lRow("IdBodegaOrigen") IsNot DBNull.Value AndAlso lRow("IdBodegaOrigen") IsNot Nothing Then
                                    Obj.IdBodegaOrigen = CType(lRow("IdBodegaOrigen"), Integer)
                                End If

                                If lRow("fecha") IsNot DBNull.Value AndAlso lRow("fecha") IsNot Nothing Then
                                    Obj.Fecha = CType(lRow("fecha"), Date)
                                End If

                                If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                                    Obj.Codigo = CType(lRow("codigo"), String)
                                End If

                                If lRow("codigo_barra") IsNot DBNull.Value AndAlso lRow("codigo_barra") IsNot Nothing Then
                                    Obj.CodigoBarra = CType(lRow("codigo_barra"), String)
                                End If

                                If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                    Obj.Producto = CType(lRow("Producto"), String)
                                End If

                                If lRow("TipoTarea") IsNot DBNull.Value AndAlso lRow("TipoTarea") IsNot Nothing Then
                                    Obj.TipoTarea = CType(lRow("TipoTarea"), String)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Dias_Despues_Ultimo_Despacho(ByVal pIdProducto As Integer) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT DATEDIFF(DAY,(SELECT MAX(FECHA) FROM VW_MOVIMIENTOS 
                    WHERE VW_MOVIMIENTOS.IDPRODUCTO = @IdProducto AND VW_MOVIMIENTOS.[Tipo Tarea]='DESP'),GETDATE())"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        Else
                            lMax = 0
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function UltimoInventario(ByVal pIdProducto As Integer, ByVal fechaDel As Date) As Integer

        Try

            Dim lMax As Integer = 0

            Dim vSQL As String = "SELECT MAX(cantidad) FROM VW_MOVIMIENTOS 
                    WHERE VW_MOVIMIENTOS.IdProducto = @IdProducto AND VW_MOVIMIENTOS.[Tipo Tarea]='INVE'"

            vSQL += " AND cast(Fecha AS DATE) < " & FormatoFechas.fFecha(fechaDel)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        Else
                            lMax = 0
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function UltimoInventarioByCodigo(ByVal codProducto As String, ByVal fechaDel As Date) As Integer

        Try

            Dim lMax As Double = 0
            Dim vSQL As String = "SELECT SUM(cantidad) FROM VW_MOVIMIENTOS 
                    WHERE VW_MOVIMIENTOS.codigo = @codigo AND VW_MOVIMIENTOS.[Tipo Tarea]='INVE'"

            vSQL += " AND Fecha >= " & FormatoFechas.fFecha(fechaDel)

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@codigo", codProducto)

                        lConnection.Open()

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        lConnection.Close()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CDbl(lReturnValue)
                        Else
                            lMax = 0
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_By_IdOrdenCompra(ByVal pIdOC As Integer, ByVal pIdRec As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = String.Empty

            If pIdOC > 0 Then

                vSQL = "SELECT * FROM VW_MovimientosDetalle WHERE IdRecepcionOc=@IdRecepcionOc"

            ElseIf pIdRec > 0 Then

                vSQL = "SELECT * FROM VW_MovimientosDetalle WHERE IdRecepcion=@IdRecepcion"

            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        If pIdOC > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdRecepcionOc", pIdOC)
                        If pIdRec > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdRecepcion", pIdRec)
                        lDataAdapter.Fill(lTable)
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetMovimientosByOC(ByVal pIdOC As Integer, ByVal pIdRec As Integer, ByVal pIdBodega As Integer) As List(Of clsBeVW_Movimientos)

        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try

            Dim vSQL As String = String.Empty

            If pIdOC > 0 Then

                vSQL = "SELECT * FROM VW_MovimientosDetalle WHERE IdOrdenCompraEnc=@IdOrdenCompraEnc And IdBodegaOrigen=@IdBodega "

            ElseIf pIdRec > 0 Then

                vSQL = "SELECT * FROM VW_MovimientosDetalle WHERE IdRecepcion=@IdRecepcion And IdBodegaOrigen=@IdBodega "

            End If

            vSQL += " ORDER BY fecha"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdOC > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc", pIdOC)
                        If pIdRec > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdRecepcion", pIdRec)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lTable As New DataTable
                        lDataAdapter.Fill(lTable)

                        Dim Obj As clsBeVW_Movimientos

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows

                                Obj = New clsBeVW_Movimientos

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    Obj.Propietario = CType(lRow("Propietario"), String)
                                End If

                                If lRow("Poliza") IsNot DBNull.Value AndAlso lRow("Poliza") IsNot Nothing Then
                                    Obj.Poliza = CType(lRow("Poliza"), String)
                                End If

                                If lRow("Producto") IsNot DBNull.Value AndAlso lRow("Producto") IsNot Nothing Then
                                    Obj.Producto = CType(lRow("Producto"), String)
                                End If

                                If lRow("Presentación") IsNot DBNull.Value AndAlso lRow("Presentación") IsNot Nothing Then
                                    Obj.Presentacion = CType(lRow("Presentación"), String)
                                End If

                                If lRow("Estado Origen") IsNot DBNull.Value AndAlso lRow("Estado Origen") IsNot Nothing Then
                                    Obj.EstadoOrigen = CType(lRow("Estado Origen"), String)
                                End If

                                If lRow("Estado Destino") IsNot DBNull.Value AndAlso lRow("Estado Destino") IsNot Nothing Then
                                    Obj.EstadoDestino = CType(lRow("Estado Destino"), String)
                                End If

                                If lRow("Unidad de Medida") IsNot DBNull.Value AndAlso lRow("Unidad de Medida") IsNot Nothing Then
                                    Obj.UMBas = CType(lRow("Unidad de Medida"), String)
                                End If

                                If lRow("cantidad") IsNot DBNull.Value AndAlso lRow("cantidad") IsNot Nothing Then
                                    Obj.Cantidad = CType(lRow("cantidad"), Double)
                                End If

                                If lRow("peso") IsNot DBNull.Value AndAlso lRow("peso") IsNot Nothing Then
                                    Obj.Peso = CType(lRow("peso"), Double)
                                End If

                                If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("lote"), String)
                                End If

                                If lRow("Origen") IsNot DBNull.Value AndAlso lRow("Origen") IsNot Nothing Then
                                    Obj.UbicOrigen = CType(lRow("Origen"), String)
                                End If

                                If lRow("Destino") IsNot DBNull.Value AndAlso lRow("Destino") IsNot Nothing Then
                                    Obj.UbicDestino = CType(lRow("Destino"), String)
                                End If

                                If lRow("Tipo Tarea") IsNot DBNull.Value AndAlso lRow("Tipo Tarea") IsNot Nothing Then
                                    Obj.TTarea = CType(lRow("Tipo Tarea"), String)
                                End If

                                If lRow("IdBodegaOrigen") IsNot DBNull.Value AndAlso lRow("IdBodegaOrigen") IsNot Nothing Then
                                    Obj.IdBodegaOrigen = CType(lRow("IdBodegaOrigen"), Integer)
                                End If

                                If lRow("fecha") IsNot DBNull.Value AndAlso lRow("fecha") IsNot Nothing Then
                                    Obj.Fecha = CType(lRow("fecha"), Date)
                                End If

                                'If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                '    Obj.Fecha_Vence = CType(lRow("fecha_vence"), Date)
                                'End If

                                If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                    Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                                End If

                                If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                                    Obj.Codigo = CType(lRow("codigo"), String)
                                End If

                                If lRow("codigo_barra") IsNot DBNull.Value AndAlso lRow("codigo_barra") IsNot Nothing Then
                                    Obj.CodigoBarra = CType(lRow("codigo_barra"), String)
                                End If

                                If lRow("IdRecepcion") IsNot DBNull.Value AndAlso lRow("IdRecepcion") IsNot Nothing Then
                                    Obj.IdRecepcion = CType(lRow("IdRecepcion"), Integer)
                                End If

                                If lRow("IdOrdenCompraEnc") IsNot DBNull.Value AndAlso lRow("IdOrdenCompraEnc") IsNot Nothing Then
                                    Obj.IdRecepcionOC = CType(lRow("IdOrdenCompraEnc"), Integer)
                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Aplicar(ByVal oBeTrans_movimientos As clsBeTrans_movimientos,
                                   ByVal IdStock As Integer,
                                   ByVal EsCiega As Boolean,
                                   ByVal pConnection As SqlConnection,
                                   ByVal pTransaction As SqlTransaction,
                                   Optional pPosiciones As Integer = 0) As Integer

        Dim BeStockOrigen As New clsBeStock
        Dim BeStockNuevo As New clsBeStock
        Dim BeStockRes As clsBeStock_res
        Dim stock_params As List(Of clsBeStock_parametro)
        Dim Cant_Original, Cantidad As Double
        Dim pp As Integer
        Dim vCantidadReservada As Double = 0
        Dim vCantidadDisponible As Double = 0
        Dim resultado As String = ""
        Dim vMsg As String = ""

        Aplicar = -1

        Try

            pp = 1

            BeStockOrigen.IdStock = IdStock

            Try
                BeStockOrigen = clsLnStock.Get_Single_Stock_By_IdStock(BeStockOrigen.IdStock,
                                                                       pConnection,
                                                                       pTransaction)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            pp = 2

            BeStockNuevo = BeStockOrigen.Clone()
            BeStockNuevo.IdBodega = BeStockOrigen.IdBodega
            BeStockNuevo.Fec_agr = Now
            BeStockNuevo.IdStock = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.
            stock_params = BeStockOrigen.Parametros

            'Original 56, IdStock1: 23 IdStock2: 33
            Cant_Original = BeStockOrigen.Cantidad
            Cantidad = oBeTrans_movimientos.Cantidad

            BeStockOrigen.Cantidad = Cant_Original - Cantidad '0
            BeStockOrigen.Cantidad = Math.Round(BeStockOrigen.Cantidad, 6)
            BeStockOrigen.ProductoEstado.IdEstado = BeStockOrigen.IdProductoEstado

            pp = 3

            Dim vIdTransaccionUbicDirigida As Integer = IIf(EsCiega, 0, oBeTrans_movimientos.IdTransaccion)

            '#EJC20181004_0141PM: Que sueño mas agotador llega en la escala 0 hasta la t, por un momento pensé en rendirme, pero gracias a Dios lo encontré.
            'El stock reservado que se buscaba no era por la transacción de ubicación y restaba lo que no tenía que restar (stock_res de pedidos)
            'Y eliminaba lo que no tenía que eliminar.
            BeStockRes = clsLnStock_res.Get_Single_By_IdStock_Para_Ubicacion(IdStock,
                                                                             vIdTransaccionUbicDirigida,
                                                                             pConnection,
                                                                             pTransaction)

            pp = 5

            If BeStockOrigen.Cantidad > 0 Then

                '#EJC20171027_0553AM:Si se hace esto se pueden perder otros valores, actualizar solo la cantidad.
                clsLnStock.Actualizar(BeStockOrigen,
                                      pConnection,
                                      pTransaction)

                If vIdTransaccionUbicDirigida <> 0 Then

                    BeStockRes.Cantidad -= Cantidad

                    If BeStockRes.Cantidad = 0 Then
                        clsLnStock_res.Eliminar(BeStockRes,
                                                pConnection,
                                                pTransaction)
                    Else
                        clsLnStock_res.Actualizar(BeStockRes,
                                                  pConnection,
                                                  pTransaction)
                    End If

                End If

            Else

                clsLnStock_parametro.Eliminar_Todos_By_IdStock(IdStock,
                                                               pConnection,
                                                               pTransaction)

                clsLnStock.Eliminar(BeStockOrigen,
                                    pConnection,
                                    pTransaction)

                If vIdTransaccionUbicDirigida <> 0 Then
                    clsLnStock_res.Eliminar(BeStockRes,
                                            pConnection,
                                            pTransaction)
                End If

            End If

            pp = 4

            BeStockNuevo.IdProductoEstado = oBeTrans_movimientos.IdEstadoDestino

            If oBeTrans_movimientos.IdTipoTarea = 20 Then
                oBeTrans_movimientos.IdPresentacion = 0
                BeStockOrigen.IdPresentacion = 0
            End If

            BeStockNuevo.Presentacion.IdPresentacion = BeStockOrigen.IdPresentacion
            BeStockNuevo.ProductoEstado.IdEstado = oBeTrans_movimientos.IdEstadoDestino
            BeStockNuevo.Lic_plate = IIf(oBeTrans_movimientos.Barra_pallet = "", BeStockOrigen.Lic_plate, oBeTrans_movimientos.Barra_pallet)
            BeStockNuevo.IdUbicacion = IIf(oBeTrans_movimientos.IdUbicacionDestino = 0, BeStockOrigen.IdUbicacion, oBeTrans_movimientos.IdUbicacionDestino)
            BeStockNuevo.IdUbicacion_anterior = BeStockOrigen.IdUbicacion
            BeStockNuevo.Cantidad = Math.Round(Cantidad, 6)
            BeStockNuevo.Pallet_No_Estandar = BeStockOrigen.Pallet_No_Estandar
            BeStockNuevo.IdProductoTallaColor = BeStockOrigen.IdProductoTallaColor

            pp = 6

            Dim rslt As Integer = clsLnStock.Insertar(BeStockNuevo,
                                                      pConnection,
                                                      pTransaction)

            If BeStockNuevo.Pallet_No_Estandar Then

                Dim vPosStockOrig As Integer = clsLnStock.Tiene_Posiciones(BeStockOrigen, pConnection, pTransaction)
                Dim vPosStockNuevo As Integer = clsLnStock.Tiene_Posiciones(BeStockNuevo, pConnection, pTransaction)

                Dim BeStockDet As New clsBeStock_det

                If vPosStockNuevo = 0 Then

                    BeStockDet.IdStock = BeStockNuevo.IdStock

                    If vPosStockOrig > 0 And pPosiciones = 0 Then
                        BeStockDet.Posiciones = vPosStockOrig
                    Else
                        BeStockDet.Posiciones = pPosiciones
                    End If

                    If clsLnStock_det.Get_Single_By_IdStock(BeStockDet,
                                                            pConnection,
                                                            pTransaction) Then
                        '#EJC20220505: Porqué ya existe?
                        BeStockDet.Posiciones = pPosiciones
                        clsLnStock_det.Actualizar(BeStockDet,
                                                  pConnection,
                                                  pTransaction)
                    Else
                        clsLnStock_det.Insertar(BeStockDet,
                                                pConnection,
                                                pTransaction)
                    End If

                    'clsLnStock_det.Insertar(BeStockDet)

                End If

                '#CKFK 20210601 Se debe eliminar el stock_det origen
                BeStockDet = New clsBeStock_det
                BeStockDet.IdStock = BeStockOrigen.IdStock

                clsLnStock_det.Eliminar(BeStockDet,
                                        pConnection,
                                        pTransaction)

            End If

            pp = 7

            If Not stock_params Is Nothing Then
                Try
                    clsLnStock_parametro.Insertar_Stock_Parametro_Cambio_Ubicacion(stock_params,
                                                                                   BeStockNuevo.IdStock,
                                                                                   pConnection,
                                                                                   pTransaction)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End If

            resultado = String.Format("Cant_Original: {0}, 
                                       CantidadDisponible: {1}, 
                                       vCantidadReservada: {2}, 
                                       Cantidad: {3}, Msg {4}", Cant_Original,
                                                               vCantidadDisponible,
                                                               vCantidadReservada,
                                                               Cantidad,
                                                               vMsg)

            Aplicar = BeStockNuevo.IdStock

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message) & " : pp=" & pp & " " & resultado)
        End Try

    End Function

    Public Shared Function Aplicar_Con_Reabastecimiento(ByVal oBeTrans_movimientos As clsBeTrans_movimientos,
                                                         ByVal IdStock As Integer,
                                                         ByVal EsCiega As Boolean,
                                                         ByVal pConnection As SqlConnection,
                                                         ByVal pTransaction As SqlTransaction,
                                                         ByVal pIdReabastecimientoLog As Integer,
                                                         Optional pPosiciones As Integer = 0) As Integer

        Dim BeStockOrigen, BeStockNuevo As New clsBeStock
        Dim BeStockRes As clsBeStock_res
        Dim stock_params As List(Of clsBeStock_parametro)
        Dim Cant_Original, Cantidad As Double
        Dim pp As Integer
        Dim vCantidadReservada As Double = 0
        Dim vCantidadDisponible As Double = 0
        Dim resultado As String = ""
        Dim vMsg As String = ""
        Dim BeTransReabastecimientoLog As New clsBeTrans_reabastecimiento_log
        Dim BeReglaRellenadoProducto As New clsBeProducto_rellenado

        Aplicar_Con_Reabastecimiento = -1

        Try

            pp = 1

            BeStockOrigen.IdStock = IdStock

            Try
                BeStockOrigen = clsLnStock.Get_Single_Stock_By_IdStock(BeStockOrigen.IdStock, pConnection, pTransaction)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            pp = 2

            BeStockNuevo = BeStockOrigen.Clone()
            BeStockNuevo.IdBodega = BeStockOrigen.IdBodega
            BeStockNuevo.Fec_agr = Now
            BeStockNuevo.IdStock = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.
            stock_params = BeStockOrigen.Parametros

            Cant_Original = BeStockOrigen.Cantidad
            Cantidad = oBeTrans_movimientos.Cantidad

            BeStockOrigen.Cantidad = Cant_Original - Cantidad
            BeStockOrigen.Cantidad = Math.Round(BeStockOrigen.Cantidad, 6)
            BeStockOrigen.ProductoEstado.IdEstado = BeStockOrigen.IdProductoEstado

            pp = 3

            Dim vIdTransaccionUbicDirigida As Integer = IIf(EsCiega, 0, oBeTrans_movimientos.IdTransaccion)

            '#EJC20181004_0141PM: Que sueño mas agotador llega en la escala 0 hasta la t, por un momento pensé en rendirme, pero gracias a Dios lo encontré.
            'El stock reservado que se buscaba no era por la transacción de ubicación y restaba lo que no tenía que restar (stock_res de pedidos)
            'Y eliminaba lo que no tenía que eliminar.
            BeStockRes = clsLnStock_res.Get_Single_By_IdStock_Para_Reabasto(IdStock,
                                                                             vIdTransaccionUbicDirigida,
                                                                             pConnection,
                                                                             pTransaction)

            pp = 5

            If BeStockOrigen.Cantidad > 0 Then

                '#EJC20171027_0553AM:Si se hace esto se pueden perder otros valores, actualizar solo la cantidad.
                clsLnStock.Actualizar(BeStockOrigen, pConnection, pTransaction)

                If vIdTransaccionUbicDirigida <> 0 Then

                    BeStockRes.Cantidad -= Cantidad

                    If BeStockRes.Cantidad = 0 Then
                        clsLnStock_res.Eliminar(BeStockRes, pConnection, pTransaction)
                    Else
                        clsLnStock_res.Actualizar(BeStockRes, pConnection, pTransaction)
                    End If

                End If

            Else

                clsLnStock_parametro.Eliminar_Todos_By_IdStock(IdStock, pConnection, pTransaction)

                clsLnStock.Eliminar(BeStockOrigen, pConnection, pTransaction)

                If vIdTransaccionUbicDirigida <> 0 Then
                    clsLnStock_res.Eliminar(BeStockRes, pConnection, pTransaction)
                End If

            End If

            pp = 4

            BeStockNuevo.IdProductoEstado = oBeTrans_movimientos.IdEstadoDestino
            BeStockNuevo.Presentacion.IdPresentacion = BeStockOrigen.IdPresentacion
            BeStockNuevo.ProductoEstado.IdEstado = oBeTrans_movimientos.IdEstadoDestino
            BeStockNuevo.Lic_plate = IIf(oBeTrans_movimientos.Barra_pallet = "", BeStockOrigen.Lic_plate, oBeTrans_movimientos.Barra_pallet)
            BeStockNuevo.IdUbicacion = IIf(oBeTrans_movimientos.IdUbicacionDestino = 0, BeStockOrigen.IdUbicacion, oBeTrans_movimientos.IdUbicacionDestino)
            BeStockNuevo.IdUbicacion_anterior = BeStockOrigen.IdUbicacion
            BeStockNuevo.Cantidad = Math.Round(Cantidad, 6)
            BeStockNuevo.Pallet_No_Estandar = BeStockOrigen.Pallet_No_Estandar

            pp = 6


            '#CKFK 20211120 Voy a llamar a cargar el objeto para poder obtener el Operador Bodega
            BeTransReabastecimientoLog = clsLnTrans_reabastecimiento_log.GetSingle(pIdReabastecimientoLog, pConnection, pTransaction)
            BeReglaRellenadoProducto = clsLnProducto_rellenado.GetSingle(BeTransReabastecimientoLog.IdRellenado, pConnection, pTransaction)
            If BeReglaRellenadoProducto Is Nothing Then
                Throw New Exception("No se encontró la configuración de reabastecimiento con identificador:" & BeTransReabastecimientoLog.IdRellenado)
            End If

            '#EJC20210304: Actualizar banderas de reabastecimiento.
            If Not (pIdReabastecimientoLog = 0) Then

                Dim BeTransReabasto As New clsBeTrans_reabastecimiento_log()
                BeTransReabasto = clsLnTrans_reabastecimiento_log.GetSingle(pIdReabastecimientoLog, pConnection, pTransaction)

                If Not BeTransReabasto Is Nothing Then

                    BeTransReabasto.Fecha_Procesamiento_HH = Now
                    BeTransReabasto.Procesado_HH = True
                    clsLnTrans_reabastecimiento_log.Actualizar_Procesamiento_HH(BeTransReabasto, pConnection, pTransaction)

                    If Not (BeReglaRellenadoProducto.IdPresentacion = BeStockNuevo.Presentacion.IdPresentacion) Then
                        If (BeReglaRellenadoProducto.IdPresentacion = 0) Then '#EJC2021119: Si la presentación origen, es 0, entonces quieren (en teoría) explosionar el producto.
                            BeStockNuevo.Presentacion.IdPresentacion = 0
                        End If
                    End If

                End If

            End If

            Dim rslt As Integer = clsLnStock.Insertar(BeStockNuevo, pConnection, pTransaction)

            If BeStockNuevo.Pallet_No_Estandar Then

                Dim vPosStockOrig As Integer = clsLnStock.Tiene_Posiciones(BeStockOrigen, pConnection, pTransaction)
                Dim vPosStockNuevo As Integer = clsLnStock.Tiene_Posiciones(BeStockNuevo, pConnection, pTransaction)

                Dim BeStockDet As New clsBeStock_det

                If vPosStockNuevo = 0 Then

                    BeStockDet.IdStock = BeStockNuevo.IdStock

                    If vPosStockOrig > 0 And pPosiciones = 0 Then
                        BeStockDet.Posiciones = vPosStockOrig
                    Else
                        BeStockDet.Posiciones = pPosiciones
                    End If

                    If clsLnStock_det.Get_Single_By_IdStock(BeStockDet, pConnection, pTransaction) Then
                        '#EJC20220505: Porqué ya existe?
                        BeStockDet.Posiciones = pPosiciones
                        clsLnStock_det.Actualizar(BeStockDet, pConnection, pTransaction)
                    Else
                        clsLnStock_det.Insertar(BeStockDet, pConnection, pTransaction)
                    End If

                End If

                '#CKFK 20210601 Se debe eliminar el stock_det origen
                BeStockDet = New clsBeStock_det
                BeStockDet.IdStock = BeStockOrigen.IdStock
                clsLnStock_det.Eliminar(BeStockDet, pConnection, pTransaction)

            End If

            pp = 7

            If Not stock_params Is Nothing Then
                Try
                    clsLnStock_parametro.Insertar_Stock_Parametro_Cambio_Ubicacion(stock_params,
                                                                                   BeStockNuevo.IdStock,
                                                                                   pConnection,
                                                                                   pTransaction)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End If

            pp = 8

            vMsg = "Llegué a pp: " & pp

            resultado = String.Format("Cant_Original: {0}, 
                                       CantidadDisponible: {1}, 
                                       vCantidadReservada: {2}, 
                                       Cantidad: {3}, Msg {4}", Cant_Original,
                                                               vCantidadDisponible,
                                                               vCantidadReservada,
                                                               Cantidad,
                                                               vMsg)

            Aplicar_Con_Reabastecimiento = BeStockNuevo.IdStock

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message) & " : pp=" & pp & " " & resultado)
        End Try

    End Function

    Public Shared Function Aplicar_Packing(ByVal oBeTrans_movimientos As clsBeTrans_movimientos,
                                           ByVal IdStock As Integer,
                                           ByVal pNuevoLP As String,
                                           ByVal pPresentacion As Integer,
                                           ByVal pConection As SqlConnection,
                                           ByVal pTransaction As SqlTransaction) As Integer

        Dim BeStockOrigen, BeStockNuevo As New clsBeStock
        Dim BeStockRes As clsBeStock_res
        Dim stock_params As List(Of clsBeStock_parametro)
        Dim Cant_Original, Cantidad As Double
        Dim pp As Integer
        Dim vCantidadReservada As Double = 0
        Dim vCantidadDisponible As Double = 0
        Dim resultado As String = ""
        Dim vMsg As String = ""
        Dim CantidadStockDestino As Double = 0

        Aplicar_Packing = -1

        Try

            pp = 1

            BeStockOrigen.IdStock = IdStock

            Try
                BeStockOrigen = clsLnStock.Get_Single_Stock_By_IdStock(BeStockOrigen.IdStock,
                                                                       pConection,
                                                                       pTransaction)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            pp = 2

            BeStockNuevo = BeStockOrigen.Clone()
            BeStockNuevo.IdBodega = BeStockOrigen.IdBodega
            BeStockNuevo.Fec_agr = Now
            BeStockNuevo.IdStock = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.
            stock_params = BeStockOrigen.Parametros

            Cant_Original = BeStockOrigen.Cantidad
            Cantidad = oBeTrans_movimientos.Cantidad

            BeStockOrigen.Cantidad = Cant_Original - Cantidad
            BeStockOrigen.Cantidad = Math.Round(BeStockOrigen.Cantidad, 6)
            BeStockOrigen.ProductoEstado.IdEstado = BeStockOrigen.IdProductoEstado

            pp = 3

            '#EJC20181004_0141PM: Que sueño mas agotador llega en la escala 0 hasta la t, por un momento pensé en rendirme, pero gracias a Dios lo encontré.
            'El stock reservado que se buscaba no era por la transacción de ubicación y restaba lo que no tenía que restar (stock_res de pedidos)
            'Y eliminaba lo que no tenía que eliminar.
            BeStockRes = clsLnStock_res.Get_Single_By_IdStock(IdStock,
                                                              pConection,
                                                              pTransaction)

            pp = 5

            If Not BeStockRes Is Nothing Then

                '#EDUEP: Error de usuario en proceso.
                Throw New Exception("#EJC20200127_0758 - EDUEP: El IdStock Origen no puede ser adicionado a la lista de packing porque posee stock reservado, Idstock: " & BeStockRes.IdStock & " IdStockRes:" & BeStockRes.IdStockRes)

            Else

                '#CKFK 20210208 Agregué este sino para que cuando la cantidad sea mayor solo actualice la cantidad del Stock Origen y cuando sea igual entonces si lo borre
                If Cant_Original = Cantidad Then
                    clsLnStock_parametro.Eliminar_Todos_By_IdStock(IdStock, pConection, pTransaction)
                    clsLnStock.Eliminar(BeStockOrigen, pConection, pTransaction)
                Else
                    clsLnStock.Actualiza_Cantidad_Y_Peso(BeStockOrigen, pConection, pTransaction)
                End If

            End If

            pp = 4

            BeStockNuevo.IdProductoEstado = oBeTrans_movimientos.IdEstadoDestino
            BeStockNuevo.Presentacion.IdPresentacion = pPresentacion
            BeStockNuevo.ProductoEstado.IdEstado = oBeTrans_movimientos.IdEstadoDestino
            BeStockNuevo.Lic_plate = pNuevoLP
            BeStockNuevo.IdUbicacion = IIf(oBeTrans_movimientos.IdUbicacionDestino = 0, BeStockOrigen.IdUbicacion, oBeTrans_movimientos.IdUbicacionDestino)
            BeStockNuevo.IdUbicacion_anterior = BeStockOrigen.IdUbicacion
            BeStockNuevo.Cantidad = Math.Round(Cantidad, 6)

            pp = 6

            CantidadStockDestino = BeStockNuevo.Cantidad

            Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(BeStockOrigen.IdBodega, pConection, pTransaction)
            clsPublic.Abs(CantidadStockDestino - Fix(CantidadStockDestino), vPermitirDecimales)



            Dim rslt As Integer = clsLnStock.Insertar(BeStockNuevo, pConection, pTransaction)

            pp = 7

            If Not stock_params Is Nothing Then
                Try
                    clsLnStock_parametro.Insertar_Stock_Parametro_Cambio_Ubicacion(stock_params,
                                                                                   BeStockNuevo.IdStock,
                                                                                   pConection,
                                                                                   pTransaction)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End If

            resultado = String.Format("Cant_Original: {0}, 
                                       CantidadDisponible: {1}, 
                                       vCantidadReservada: {2}, 
                                       Cantidad: {3}, Msg {4}", Cant_Original,
                                                               vCantidadDisponible,
                                                               vCantidadReservada,
                                                               Cantidad,
                                                               vMsg)

            Aplicar_Packing = BeStockNuevo.IdStock

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message) & " : pp=" & pp & " " & resultado)
        End Try

    End Function

    Public Shared Function Aplicar_Cambio_Ubicacion_Automatico_Por_Picking(ByRef oBeTrans_movimientos As clsBeTrans_movimientos,
                                                                           ByVal IdStock As Integer,
                                                                           ByVal EsIdStockIgual As Boolean,
                                                                           ByVal pConection As SqlConnection,
                                                                           ByVal pTransaction As SqlTransaction) As String

        Dim BeStockOrigen, BeStockNuevo As New clsBeStock
        Dim BeProductoEstado As New clsBeProducto_estado
        Dim BeStockRes As clsBeStock_res
        Dim stock_params As List(Of clsBeStock_parametro)
        Dim Cant_Original, Cantidad As Double
        Dim pp As Integer
        Dim vCantidadReservada As Double = 0
        Dim vCantidadDisponible As Double = 0
        Dim resultado As String = ""
        Dim vMsg As String = ""
        Dim Insertar_Nuevo_Stock As Boolean = True
        Dim CantidadStockDestino As Double = 0

        Try

            pp = 1

            BeStockOrigen.IdStock = IdStock

            Try
                BeStockOrigen = clsLnStock.Get_Single_Stock_By_IdStock(BeStockOrigen.IdStock,
                                                                       pConection,
                                                                       pTransaction)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            pp = 2

            BeStockNuevo = BeStockOrigen.Clone()
            BeStockNuevo.IdBodega = BeStockOrigen.IdBodega
            BeStockNuevo.IdStock = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.
            stock_params = BeStockOrigen.Parametros

            Cant_Original = BeStockOrigen.Cantidad
            Cantidad = oBeTrans_movimientos.Cantidad

            '#AT20230103 Hago antes la consulta del estado destino para saber si se debe partir el stock
            'Cambie el parametro de  BeStockNuevo.IdProductoEstado a oBeTrans_movimientos.IdEstadoDestino
            BeProductoEstado = clsLnProducto_estado.GetSingle(oBeTrans_movimientos.IdEstadoDestino,
                                                              pConection,
                                                              pTransaction)

            If Not BeProductoEstado Is Nothing Then
                If BeProductoEstado.Utilizable AndAlso Not BeProductoEstado.Dañado Then
                    If BeStockNuevo.IdProductoBodega = BeStockOrigen.IdProductoBodega AndAlso BeStockNuevo.IdPropietarioBodega = BeStockOrigen.IdPropietarioBodega AndAlso BeStockNuevo.Lote = BeStockOrigen.Lote AndAlso BeStockNuevo.IdPresentacion = BeStockOrigen.IdPresentacion AndAlso BeStockNuevo.IdUnidadMedida = BeStockOrigen.IdUnidadMedida AndAlso BeStockNuevo.Fecha_vence = BeStockOrigen.Fecha_vence Then
                        Insertar_Nuevo_Stock = False
                    End If
                End If
            End If

            '#AT20230102 Si insertar_nuevo_stock es verdadero entonces se calcula la cantidad del stock disponible (Origen)
            If Insertar_Nuevo_Stock Then
                BeStockOrigen.Cantidad = Cant_Original - Cantidad
            Else
                BeStockOrigen.Cantidad = Cant_Original
            End If

            BeStockOrigen.Cantidad = Math.Round(BeStockOrigen.Cantidad, 6) '#CKFK 20181026 0826AM Agregué el redondeo a 6 cifras decimales cuando hace la resta
            BeStockOrigen.ProductoEstado.IdEstado = BeStockOrigen.IdProductoEstado

            pp = 3

            '#EJC20181004_0141PM: Que sueño mas agotador llega en la escala 0 hasta la t, por un momento pensé en rendirme, pero gracias a Dios lo encontré.
            'El stock reservado que se buscaba no era por la transacción de ubicación y restaba lo que no tenía que restar (stock_res de pedidos)
            'Y eliminaba lo que no tenía que eliminar.
            BeStockRes = clsLnStock_res.Get_Single_By_IdStock_Para_Picking(IdStock,
                                                                           pConection,
                                                                           pTransaction)

            pp = 5

            If BeStockOrigen.Cantidad > 0 Then

                '#AT20230102 Se actualiza la cantidad en stock  si Insertar_Nuevo_Stock es verdadero 
                If Insertar_Nuevo_Stock Then

                    CantidadStockDestino = BeStockOrigen.Cantidad

                    Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(BeStockOrigen.IdBodega, pConection, pTransaction)
                    clsPublic.Abs(CantidadStockDestino - Fix(CantidadStockDestino), vPermitirDecimales)

                    '#EJC20171027_0553AM:Si se hace esto se pueden perder otros valores, actualizar solo la cantidad.
                    clsLnStock.Actualizar_Cantidad(BeStockOrigen,
                                                   pConection,
                                                   pTransaction)
                End If

            Else

                'clsLnStock_parametro.Eliminar_Todos_By_IdStock(IdStock, pConection, pTransaction)
                clsLnStock.Eliminar(BeStockOrigen, pConection, pTransaction)
                clsLnStock_res.Eliminar(BeStockRes, pConection, pTransaction)

            End If

            pp = 4

            BeStockNuevo.IdProductoEstado = oBeTrans_movimientos.IdEstadoDestino
            '#EJC20171027_0521AM: Copiar el IdPresentacion del stock origen
            BeStockNuevo.Presentacion.IdPresentacion = BeStockOrigen.IdPresentacion
            BeStockNuevo.ProductoEstado.IdEstado = oBeTrans_movimientos.IdEstadoDestino
            BeStockNuevo.IdUbicacion = oBeTrans_movimientos.IdUbicacionDestino
            '#EJC20171027_0521AM: Copiar el IdUbicacion del stock origen, será la ubicación anterior del nuevo stock.
            'snew.IdUbicacion_anterior = oBeTrans_movimientos.IdUbicacionOrigen
            BeStockNuevo.IdUbicacion_anterior = BeStockOrigen.IdUbicacion
            BeStockNuevo.Cantidad = Math.Round(Cantidad, 6)

            pp = 6

            If Insertar_Nuevo_Stock Then

                CantidadStockDestino = BeStockNuevo.Cantidad

                Dim vPermitirDecimales As Boolean = clsLnBodega.Get_Permitir_Decimales(BeStockOrigen.IdBodega,
                                                                                       pConection,
                                                                                       pTransaction)

                clsPublic.Abs(CantidadStockDestino - Fix(CantidadStockDestino), vPermitirDecimales)



                Dim rslt As Integer = clsLnStock.Insertar(BeStockNuevo,
                                                          pConection,
                                                          pTransaction)

                pp = 7

                '#JP20171024_1245 Copia los parametros al nuevo idstock
                If Not stock_params Is Nothing Then
                    Try
                        clsLnStock_parametro.Insertar_Stock_Parametro_Cambio_Ubicacion(stock_params,
                                                                                       BeStockNuevo.IdStock,
                                                                                       pConection,
                                                                                       pTransaction)
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    End Try
                End If
                '# end

                resultado = String.Format("Cant_Original: {0}, 
                                      CantidadDisponible: {1}, 
                                      vCantidadReservada: {2}, 
                                      Cantidad: {3}, Msg {4}", Cant_Original,
                                                               vCantidadDisponible,
                                                               vCantidadReservada,
                                                               Cantidad,
                                                               vMsg)
            End If

            Return resultado ' rslt

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message) & " : pp=" & pp)
        End Try

    End Function

    Public Shared Sub Guardar_Movimientos(ByVal IdTareaUbicacionEnc As Integer, ByVal pListObjMov As List(Of clsBeTrans_movimientos), ByRef lConnection As SqlConnection, ByRef lTransaction As SqlTransaction)

        Try

            If pListObjMov IsNot Nothing AndAlso pListObjMov.Count > 0 Then

                For Each Obj As clsBeTrans_movimientos In pListObjMov

                    If Obj.IdMovimiento = 0 Then
                        '#MA20260519 
                        If Obj.IdTransaccion = 0 Then
                            Obj.IdTransaccion = IdTareaUbicacionEnc
                        End If
                        Obj.Fecha = Now
                        Insertar(Obj, lConnection, lTransaction)
                    End If
                Next

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar_Movimientos_Recepcion(ByVal pIdEmpresa As Integer,
                                                          ByVal pIdBodega As Integer,
                                                          ByVal pIdUsuario As Integer,
                                                          ByVal BeStockRec As clsBeStock_rec,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef lTransaction As SqlTransaction,
                                                          Optional IdOperadorBodega As Integer = 0) As Integer

        Insertar_Movimientos_Recepcion = 0

        Try

            Dim BeTransMovimiento As New clsBeTrans_movimientos()
            BeTransMovimiento.IdMovimiento = 0
            BeTransMovimiento.IdEmpresa = pIdEmpresa
            BeTransMovimiento.IdBodegaOrigen = pIdBodega
            BeTransMovimiento.IdTransaccion = BeStockRec.IdRecepcionEnc
            BeTransMovimiento.IdPropietarioBodega = BeStockRec.IdPropietarioBodega
            BeTransMovimiento.IdProductoBodega = BeStockRec.IdProductoBodega
            BeTransMovimiento.IdUbicacionOrigen = BeStockRec.IdUbicacion
            BeTransMovimiento.IdUbicacionDestino = BeStockRec.IdUbicacion
            BeTransMovimiento.IdPresentacion = BeStockRec.Presentacion.IdPresentacion
            BeTransMovimiento.IdEstadoOrigen = BeStockRec.ProductoEstado.IdEstado
            BeTransMovimiento.IdEstadoDestino = BeStockRec.ProductoEstado.IdEstado
            BeTransMovimiento.IdUnidadMedida = BeStockRec.IdUnidadMedida
            BeTransMovimiento.IdTipoTarea = clsDataContractDI.tTipoTarea.RECE
            BeTransMovimiento.IdBodegaDestino = pIdBodega
            BeTransMovimiento.IdRecepcion = BeStockRec.IdRecepcionEnc
            BeTransMovimiento.IdRecepcionDet = BeStockRec.IdRecepcionDet
            BeTransMovimiento.Cantidad = BeStockRec.Cantidad
            BeTransMovimiento.Serie = BeStockRec.Serial
            BeTransMovimiento.Peso = BeStockRec.Peso
            BeTransMovimiento.Lote = BeStockRec.Lote
            BeTransMovimiento.Barra_pallet = BeStockRec.Lic_plate
            BeTransMovimiento.Fecha_vence = BeStockRec.Fecha_vence
            BeTransMovimiento.Fecha_agr = Now
            BeTransMovimiento.Usuario_agr = pIdUsuario
            BeTransMovimiento.IdOperadorBodega = IdOperadorBodega
            BeTransMovimiento.IdProductoTallaColor = BeStockRec.IdProductoTallaColor
            BeTransMovimiento.Talla = BeStockRec.Talla
            BeTransMovimiento.Color = BeStockRec.Color

            clsLnStock.Get_Existencia_By_IdProductoBodega(BeStockRec,
                                                          lConnection,
                                                          lTransaction)

            BeTransMovimiento.Cantidad_hist = BeStockRec.CantidadEnStock
            BeTransMovimiento.Peso_hist = BeStockRec.PesoEnStock

            Insertar(BeTransMovimiento,
                     lConnection,
                     lTransaction)

            Insertar_Movimientos_Recepcion = BeTransMovimiento.IdMovimiento

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} ", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_RecepcionesAndDespachos(pIdProductoBodega As Integer, pIdPresentacion As Integer, ByVal pFechaInicio As Date, ByVal pFechaFin As Date) As List(Of clsBeTrans_movimientos)

        Try

            Dim lReturnList As New List(Of clsBeTrans_movimientos)

            Dim vSQL As String = "SELECT * FROM trans_movimientos "

            vSQL += String.Format("WHERE DATEDIFF(MINUTE," + FormatoFechas.fFechaHora(pFechaInicio) + ",trans_movimientos.fecha_agr)>0")

            If pIdPresentacion = 0 Then
                vSQL += " AND trans_movimientos.IdProductoBodega = @IdProductoBodega AND trans_movimientos.IdTipoTarea = 1 OR trans_movimientos.IdTipoTarea = 5"
            Else
                vSQL += " AND trans_movimientos.IdProductoBodega = @IdProductoBodega AND trans_movimientos.IdPresentacion= @IdPresentacion AND trans_movimientos.IdTipoTarea = 1 OR trans_movimientos.IdTipoTarea = 5"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        Dim vBeTrans_inv_detalle As New clsBeTrans_movimientos

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_detalle = New clsBeTrans_movimientos
                            Cargar(vBeTrans_inv_detalle, dr)
                            lReturnList.Add(vBeTrans_inv_detalle)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_RecepcionesAndDespachosConLP(pIdProductoBodega As Integer, lic_plate As String, ByVal pFechaInicio As Date, ByVal pFechaFin As Date) As List(Of clsBeTrans_movimientos)

        Dim vSQL As String = ""

        Try

            Dim lReturnList As New List(Of clsBeTrans_movimientos)

            If lic_plate <> "" And lic_plate <> 0 Then

                vSQL = "SELECT * FROM trans_movimientos "

                vSQL += String.Format("WHERE DATEDIFF(MINUTE," + FormatoFechas.fFechaHora(pFechaInicio) + ",trans_movimientos.fecha_agr)>0")

                vSQL += " AND trans_movimientos.IdProductoBodega = @IdProductoBodega AND trans_movimientos.barra_pallet= @lic_plate AND trans_movimientos.IdTipoTarea = 1 OR trans_movimientos.IdTipoTarea = 5"

            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@lic_plate", lic_plate)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        Dim vBeTrans_inv_detalle As New clsBeTrans_movimientos

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_detalle = New clsBeTrans_movimientos
                            Cargar(vBeTrans_inv_detalle, dr)
                            lReturnList.Add(vBeTrans_inv_detalle)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_RecepcionesAndDespachosConLPSinFecha(pIdProductoBodega As Integer, lic_plate As String) As List(Of clsBeTrans_movimientos)

        Dim vSQL As String = ""

        Try

            Dim lReturnList As New List(Of clsBeTrans_movimientos)

            If lic_plate <> "" And lic_plate <> 0 Then

                vSQL = "SELECT * FROM trans_movimientos "

                vSQL += "WHERE trans_movimientos.IdProductoBodega = @IdProductoBodega AND trans_movimientos.barra_pallet=@lic_plate AND trans_movimientos.IdTipoTarea = 1 OR trans_movimientos.IdTipoTarea = 5"

            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@lic_plate", lic_plate)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        Dim vBeTrans_inv_detalle As New clsBeTrans_movimientos

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_detalle = New clsBeTrans_movimientos
                            Cargar(vBeTrans_inv_detalle, dr)
                            lReturnList.Add(vBeTrans_inv_detalle)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_RecepcionesAndDespachosSinFecha(pIdProductoBodega As Integer, pIdPresentacion As Integer) As List(Of clsBeTrans_movimientos)

        Try

            Dim lReturnList As New List(Of clsBeTrans_movimientos)

            Dim vSQL As String = "SELECT * FROM trans_movimientos "

            If pIdPresentacion = 0 Then
                vSQL += "WHERE trans_movimientos.IdProductoBodega = @IdProductoBodega AND trans_movimientos.IdTipoTarea = 1 OR trans_movimientos.IdTipoTarea = 5"
            Else
                vSQL += "WHERE trans_movimientos.IdProductoBodega = @IdProductoBodega AND trans_movimientos.IdPresentacion= @IdPresentacion AND trans_movimientos.IdTipoTarea = 1 OR trans_movimientos.IdTipoTarea = 5"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        Dim vBeTrans_inv_detalle As New clsBeTrans_movimientos

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_inv_detalle = New clsBeTrans_movimientos
                            Cargar(vBeTrans_inv_detalle, dr)
                            lReturnList.Add(vBeTrans_inv_detalle)

                        Next
                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Movimientos_Kardex_By_IdEmpresa(ByVal pIdEmpresa As Integer, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

        Get_Movimientos_Kardex_By_IdEmpresa = Nothing

        Try

            Dim vSQL As String = ""

            If pIdEmpresa > 0 Then
                vSQL = "SELECT *,P.factor FROM VW_Movimientos_N1 VW left outer JOIN 
                        producto_presentacion P ON P.IdPresentacion = VW.IdPresentacion 
                        WHERE VW.Contabilizar = 1 AND VW.IdEmpresa=@IdEmpresa"
            Else
                vSQL = "SELECT *,P.factor FROM VW_Movimientos_N1 VW left outer JOIN 
                        producto_presentacion P ON P.IdPresentacion = VW.IdPresentacion 
                        WHERE VW.Contabilizar = 1 AND VW.IdBodegaOrigen=0"
            End If

            vSQL += String.Format(" AND cast(VW.Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " ORDER BY VW.FECHA "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdEmpresa > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)

                        Dim lTable As New DataTable
                        lDataAdapter.Fill(lTable)

                        Dim Obj As New clsBeVW_Movimientos
                        Dim lista As New List(Of clsBeVW_Movimientos)

                        For Each r As DataRow In lTable.Rows
                            clsLnVW_Movimientos.Cargar(Obj, r)
                            lista.Add(Obj)
                        Next

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            Get_Movimientos_Kardex_By_IdEmpresa = lTable

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

    Public Shared Function Get_Movimientos_Kardex_By_IdEmpresa_MI3(ByVal pIdEmpresa As Integer, ByVal pFechaDel As Date, ByVal pFechaAl As Date) As List(Of clsBeVW_Movimientos)

        Get_Movimientos_Kardex_By_IdEmpresa_MI3 = Nothing

        Try

            Dim vSQL As String = ""

            If pIdEmpresa > 0 Then
                vSQL = "SELECT *,P.factor FROM VW_Movimientos_N1 VW left outer JOIN 
                        producto_presentacion P ON P.IdPresentacion = VW.IdPresentacion 
                        WHERE VW.Contabilizar = 1 AND VW.IdEmpresa=@IdEmpresa"
            Else
                vSQL = "SELECT *,P.factor FROM VW_Movimientos_N1 VW left outer JOIN 
                        producto_presentacion P ON P.IdPresentacion = VW.IdPresentacion 
                        WHERE VW.Contabilizar = 1 AND VW.IdBodegaOrigen=0"
            End If

            vSQL += String.Format(" AND cast(VW.Fecha AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " ORDER BY VW.FECHA "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdEmpresa > 0 Then lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)

                        Dim lTable As New DataTable
                        lDataAdapter.Fill(lTable)

                        Dim Obj As New clsBeVW_Movimientos
                        Dim lista As New List(Of clsBeVW_Movimientos)

                        For Each r As DataRow In lTable.Rows
                            clsLnVW_Movimientos.Cargar(Obj, r)
                            lista.Add(Obj)
                        Next

                        Get_Movimientos_Kardex_By_IdEmpresa_MI3 = lista

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_Reporte_By_IdProducto(
                                                             ByVal pFechaAl As Date,
                                                             ByVal pIdBodega As Integer,
                                                             ByVal pIdPropietarioBodega As Integer) As List(Of clsBeVW_MovimientosRetroactivo)

        Dim lReturnList As New List(Of clsBeVW_MovimientosRetroactivo)

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT Codigo,Producto, SUM(cantidad) AS Cantidad,
                        EstadoOrigen, 
                        EstadoDestino, 
                        TipoTarea, lote,Fecha_Vence, IdTipoTarea, 
                        IdPresentacion, IdUnidadMedida, IdEstadoOrigen, 
                        IdProductoBodega,Fecha,
                        Umbas, EstadoOrigen, Presentación,IdMovimiento
                        FROM VW_Movimientos_Reporte 
                        WHERE IdBodega=@IdBodega and IdPropietarioBodega=@IdPropietarioBodega "

            vSQL += String.Format(" And cast(Fecha AS DATE) <= {0} ", FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " GROUP BY codigo,producto,EstadoOrigen, 
                    EstadoDestino, IdProductoBodega,
                    TipoTarea, lote,fecha_vence, 
                    IdTipoTarea,Fecha, IdPresentacion, 
                    IdUnidadMedida, IdEstadoOrigen,
                    Umbas, EstadoOrigen, Presentación,IdMovimiento
                    ORDER BY Fecha, Codigo, fecha_vence, Lote"

            'vSQL += " ORDER BY Fecha, Codigo, fecha_vence, Lote"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                        Dim lTable As New DataTable
                        lDTA.Fill(lTable)

                        Dim Obj As clsBeVW_MovimientosRetroactivo

                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lTable.Rows
                                Obj = New clsBeVW_MovimientosRetroactivo
                                clsLnVW_MovimientosRetroactivo.Cargar(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_Reporte_By_Rango_Fechas(ByVal pFechaDel As Date,
                                                                       ByVal pFechaAl As Date,
                                                                       ByVal pCodigo As Integer,
                                                                       Optional ByVal pIdPropietarioBodega As Integer = Nothing) As List(Of clsBeVW_MovimientosRetroactivo)

        Dim lReturnList As New List(Of clsBeVW_MovimientosRetroactivo)

        Try

            Dim vSQL As String = "SELECT t.codigo, t.Producto, t.EstadoOrigen,  t.EstadoDestino,
                        t.TipoTarea, t.lote,t.Fecha_Vence, t.IdPresentacion, t.IdUnidadMedida, t.IdEstadoOrigen,
                        t.IdProductoBodega,t.Fecha, t.Umbas, t.Presentación,t.IdTipoTarea,
                        sum(t.Ingresos) as Ingresos,
                        sum(t.Salidas) as Salidas,
                        sum(t.Ajustes_Positivos) as Ajustes_Positivos,
                        sum(t.Ajustes_Negativos) as Ajustes_Negativos, 
                        IdMovimiento
                    from(SELECT Codigo,Producto, EstadoOrigen,
                        EstadoDestino,
                        TipoTarea, lote,Fecha_Vence, IdTipoTarea,
                        IdPresentacion, IdUnidadMedida, IdEstadoOrigen,
                        IdProductoBodega,Fecha,
                        Umbas, Presentación,
                        case when IdTipoTarea = 1 then SUM(cantidad) else 0 end AS Ingresos,
                        case when IdTipoTarea = 5 then SUM(cantidad) else 0 end AS Salidas,
                        case when IdTipoTarea = 13 then SUM(cantidad) else 0 end AS Ajustes_Positivos,
                        case when IdTipoTarea = 17 then SUM(cantidad) else 0 end AS Ajustes_Negativos,
                        IdMovimiento
                        FROM VW_Movimientos_N
                        WHERE  TIPOTAREA NOT IN ('AJCANTNI','AJCANTPI') AND codigo = @pCodigo "

            vSQL += String.Format("And Fecha BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))


            If pIdPropietarioBodega <> 0 Then

                vSQL += "and IdPropietarioBodega= @pIdPropietarioBodega"

            End If

            vSQL += " GROUP BY codigo,producto,EstadoOrigen,
                        EstadoDestino, IdProductoBodega,
                        TipoTarea, lote,fecha_vence,
                        IdTipoTarea,Fecha, IdPresentacion,
                        IdUnidadMedida, IdEstadoOrigen,
                        Umbas, EstadoOrigen, Presentación, IdMovimiento) AS t
                    group by t.codigo, t.Producto, t.EstadoOrigen,  t.EstadoDestino,
                                t.TipoTarea, t.lote,t.Fecha_Vence, t.IdPresentacion, t.IdUnidadMedida,
                                t.IdProductoBodega,t.Fecha, t.Umbas, t.Presentación, t.IdEstadoOrigen,t.IdTipoTarea,t.IdMovimiento
                    ORDER BY t.Codigo, t.Lote, t.Fecha"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)

                    dad.SelectCommand.CommandType = CommandType.Text

                    dad.SelectCommand.Parameters.AddWithValue("@pCodigo", pCodigo)

                    If pIdPropietarioBodega <> 0 Then

                        dad.SelectCommand.Parameters.AddWithValue("@pIdPropietarioBodega", pIdPropietarioBodega)

                    End If


                    Dim lTable As New DataTable
                    dad.Fill(lTable)

                    Dim Obj As clsBeVW_MovimientosRetroactivo

                    If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lTable.Rows
                            Obj = New clsBeVW_MovimientosRetroactivo
                            clsLnVW_MovimientosRetroactivo.Cargar(Obj, lRow)
                            lReturnList.Add(Obj)
                        Next

                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Insertar_Movimiento_Picking(ByVal oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                                       ByVal vIdUbicacionPicking As Integer,
                                                       ByVal pCantidad As Double,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As Integer


        Insertar_Movimiento_Picking = 0

        Try

            '#EJC20260527: el movimiento PIK debe guardar la ubicación destino enviada por picking/muelle; sin esto trans_movimientos.IdUbicacionDestino queda NULL.
            If vIdUbicacionPicking = 0 Then
                Throw New Exception("ERROR_20260527A: No se puede insertar movimiento de picking sin ubicación destino.")
            End If

            Dim pStock = clsLnStock.Get_Single_Stock_By_IdStock_And_IdProducto_Bodega(oBeTrans_picking_ubic.IdStock,
                                                                                      oBeTrans_picking_ubic.IdProductoBodega,
                                                                                      lConnection,
                                                                                      lTransaction)


            Dim pEmpresa As New clsBeEmpresa
            pEmpresa = clsLnEmpresa.GetSingle_By_IdBodega(oBeTrans_picking_ubic.IdBodega,
                                                          lConnection,
                                                          lTransaction)

            Dim BeTransMovimiento As New clsBeTrans_movimientos()

            If Not pStock Is Nothing Then

                BeTransMovimiento.IdMovimiento = 0
                BeTransMovimiento.IdEmpresa = pEmpresa.IdEmpresa
                BeTransMovimiento.IdBodegaOrigen = oBeTrans_picking_ubic.IdBodega
                BeTransMovimiento.IdBodegaDestino = oBeTrans_picking_ubic.IdBodega
                BeTransMovimiento.IdTransaccion = oBeTrans_picking_ubic.IdPickingEnc
                BeTransMovimiento.IdPropietarioBodega = oBeTrans_picking_ubic.IdPropietarioBodega
                BeTransMovimiento.IdProductoBodega = oBeTrans_picking_ubic.IdProductoBodega
                '#CKFK20250507 Validar si se debe enviar la ubicación o la ubicación anterior
                BeTransMovimiento.IdUbicacionOrigen = pStock.IdUbicacion
                BeTransMovimiento.IdUbicacionDestino = vIdUbicacionPicking
                BeTransMovimiento.IdPresentacion = oBeTrans_picking_ubic.IdPresentacion
                BeTransMovimiento.IdEstadoOrigen = oBeTrans_picking_ubic.IdProductoEstado
                BeTransMovimiento.IdEstadoDestino = oBeTrans_picking_ubic.IdProductoEstado
                BeTransMovimiento.IdUnidadMedida = oBeTrans_picking_ubic.IdUnidadMedida
                BeTransMovimiento.IdTipoTarea = clsDataContractDI.tTipoTarea.PIK
                BeTransMovimiento.IdRecepcion = pStock.IdRecepcionEnc
                BeTransMovimiento.IdRecepcionDet = pStock.IdRecepcionDet
                BeTransMovimiento.IdPedidoEnc = oBeTrans_picking_ubic.IdPedidoEnc
                BeTransMovimiento.IdPedidoDet = oBeTrans_picking_ubic.IdPedidoDet
                BeTransMovimiento.IdDespachoEnc = 0
                BeTransMovimiento.IdDespachoDet = 0
                BeTransMovimiento.Cantidad = pCantidad
                BeTransMovimiento.Serie = oBeTrans_picking_ubic.Serial
                BeTransMovimiento.Peso = oBeTrans_picking_ubic.Peso_recibido
                BeTransMovimiento.Lote = oBeTrans_picking_ubic.Lote
                BeTransMovimiento.Barra_pallet = oBeTrans_picking_ubic.Lic_plate
                BeTransMovimiento.Fecha_vence = oBeTrans_picking_ubic.Fecha_Vence
                BeTransMovimiento.Fecha_agr = Now
                BeTransMovimiento.Usuario_agr = oBeTrans_picking_ubic.User_mod
                BeTransMovimiento.Hora_fin = Now
                BeTransMovimiento.IdOperadorBodega = oBeTrans_picking_ubic.IdOperadorBodega_Pickeo
                BeTransMovimiento.IdProductoTallaColor = oBeTrans_picking_ubic.IdProductoTallaColor

                If oBeTrans_picking_ubic.IdProductoTallaColor <> 0 Then
                    Dim BEProductoTallaColor As New clsBeProducto_talla_color
                    BEProductoTallaColor = clsLnProducto_talla_color.GetSingle(oBeTrans_picking_ubic.IdProductoTallaColor)
                    BeTransMovimiento.Talla = If(clsLnTalla.GetSingle_By_IdTalla(BEProductoTallaColor.IdTalla)?.Codigo, "")
                    BeTransMovimiento.Color = If(clsLnColor.GetSingle_By_IdColor(BEProductoTallaColor.IdColor)?.Codigo, "")
                Else
                    BeTransMovimiento.Talla = ""
                    BeTransMovimiento.Color = ""
                End If

                Insertar(BeTransMovimiento,
                         lConnection,
                         lTransaction)

                Insertar_Movimiento_Picking = BeTransMovimiento.IdMovimiento


            End If

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} ", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    '#GT21042023: agregué este método para guardar movimientos de verificación desde la HH.
    Public Shared Function Insertar_Movimiento_Verificacion(ByVal oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                                            ByVal vIdUbicacionPicking As Integer,
                                                            ByVal pCantidad As Double,
                                                            ByVal pPeso As Double,
                                                            ByRef lConnection As SqlConnection,
                                                            ByRef lTransaction As SqlTransaction,
                                                            Optional ByVal pCantidadEnUmbas As Boolean = False) As Integer

        Insertar_Movimiento_Verificacion = 0

        Try

            Dim pStock = clsLnStock.Get_Single_Stock_By_IdStock_And_IdProducto_Bodega(oBeTrans_picking_ubic.IdStock,
                                                                                      oBeTrans_picking_ubic.IdProductoBodega,
                                                                                      lConnection,
                                                                                      lTransaction)


            Dim pEmpresa As New clsBeEmpresa
            pEmpresa = clsLnEmpresa.GetSingle_By_IdBodega(oBeTrans_picking_ubic.IdBodega,
                                                          lConnection,
                                                          lTransaction)

            If pStock IsNot Nothing Then
                Dim vCantidadMovimiento As Double = Normalizar_Cantidad_Verificacion_UMBAS(oBeTrans_picking_ubic,
                                                                                          pCantidad,
                                                                                          pCantidadEnUmbas,
                                                                                          lConnection,
                                                                                          lTransaction)

                Dim BeTransMovimiento As New clsBeTrans_movimientos()
                BeTransMovimiento.IdMovimiento = 0
                BeTransMovimiento.IdEmpresa = pEmpresa.IdEmpresa
                BeTransMovimiento.IdBodegaOrigen = oBeTrans_picking_ubic.IdBodega
                BeTransMovimiento.IdBodegaDestino = oBeTrans_picking_ubic.IdBodega
                BeTransMovimiento.IdTransaccion = oBeTrans_picking_ubic.IdPickingEnc 'como no se ha despachado, esta sería la tran asociada
                BeTransMovimiento.IdPropietarioBodega = oBeTrans_picking_ubic.IdPropietarioBodega
                BeTransMovimiento.IdProductoBodega = oBeTrans_picking_ubic.IdProductoBodega
                '#CKFK20250507 Validar si se debe enviar la ubicación o la ubicación anterior
                BeTransMovimiento.IdUbicacionOrigen = pStock.IdUbicacion_anterior
                BeTransMovimiento.IdUbicacionDestino = vIdUbicacionPicking
                BeTransMovimiento.IdPresentacion = oBeTrans_picking_ubic.IdPresentacion
                BeTransMovimiento.IdEstadoOrigen = oBeTrans_picking_ubic.IdProductoEstado
                BeTransMovimiento.IdEstadoDestino = oBeTrans_picking_ubic.IdProductoEstado
                BeTransMovimiento.IdUnidadMedida = oBeTrans_picking_ubic.IdUnidadMedida
                BeTransMovimiento.IdTipoTarea = clsDataContractDI.tTipoTarea.VERI
                BeTransMovimiento.IdRecepcion = pStock.IdRecepcionEnc
                BeTransMovimiento.IdRecepcionDet = pStock.IdRecepcionDet
                BeTransMovimiento.IdPedidoEnc = oBeTrans_picking_ubic.IdPedidoEnc
                BeTransMovimiento.IdPedidoDet = oBeTrans_picking_ubic.IdPedidoDet
                BeTransMovimiento.IdDespachoEnc = 0
                BeTransMovimiento.IdDespachoDet = 0
                BeTransMovimiento.Cantidad = vCantidadMovimiento
                BeTransMovimiento.Serie = oBeTrans_picking_ubic.Serial
                BeTransMovimiento.Peso = pPeso
                BeTransMovimiento.Lote = oBeTrans_picking_ubic.Lote
                BeTransMovimiento.Barra_pallet = oBeTrans_picking_ubic.Lic_plate
                BeTransMovimiento.Fecha_vence = oBeTrans_picking_ubic.Fecha_Vence
                BeTransMovimiento.Fecha_agr = Now
                BeTransMovimiento.Usuario_agr = oBeTrans_picking_ubic.User_mod
                BeTransMovimiento.Hora_fin = Now
                BeTransMovimiento.IdOperadorBodega = oBeTrans_picking_ubic.IdOperadorBodega_Verifico
                BeTransMovimiento.IdProductoTallaColor = oBeTrans_picking_ubic.IdProductoTallaColor

                If oBeTrans_picking_ubic.IdProductoTallaColor <> 0 Then
                    Dim BEProductoTallaColor As New clsBeProducto_talla_color
                    BEProductoTallaColor = clsLnProducto_talla_color.GetSingle(oBeTrans_picking_ubic.IdProductoTallaColor)
                    BeTransMovimiento.Talla = If(clsLnTalla.GetSingle_By_IdTalla(BEProductoTallaColor.IdTalla)?.Codigo, "")
                    BeTransMovimiento.Color = If(clsLnColor.GetSingle_By_IdColor(BEProductoTallaColor.IdColor)?.Codigo, "")
                Else
                    BeTransMovimiento.Talla = ""
                    BeTransMovimiento.Color = ""
                End If

                If Get_IdMovimiento_Verificacion_Existente(BeTransMovimiento, lConnection, lTransaction) > 0 Then
                    Return 0
                End If

                Insertar_Movimiento_Verificacion = Insertar(BeTransMovimiento,
                                                            lConnection,
                                                            lTransaction)
            Else
                Throw New Exception("No existe el stock asociado a esta verificación no se puede guarde el movimiento")
            End If


        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} ", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_By_IdPropietario(ByVal pIdPropietario As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try
            ' Consulta base que selecciona registros de la vista VW_Movimientos
            Dim vSQL As String = "SELECT * FROM VW_Movimientos WHERE IdPropietario = @IdPropietario"

            ' Calcula las fechas de inicio y fin para el filtro
            Dim fechaInicio As Date = New Date(Now.Year, Now.Month, 1) ' Primer día del mes actual
            Dim fechaFin As Date = fechaInicio.AddMonths(1).AddDays(-1) ' Último día del mes actual

            ' Agrega la condición de filtrado por fechas a la consulta SQL
            vSQL += " AND cast(Fecha AS DATE) BETWEEN @FechaInicio AND @FechaFin"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        ' Agrega los parámetros a la consulta
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin)

                        ' Llena la tabla con los resultados de la consulta
                        lDataAdapter.Fill(lTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        End Try

    End Function

    Public Shared Function Insertar_Movimientos_Recepcion_Area_SAP(ByVal pIdEmpresa As Integer,
                                                                   ByVal pIdBodega As Integer,
                                                                   ByVal pIdUsuario As Integer,
                                                                   ByVal BeStockRec As clsBeStock_rec,
                                                                   ByVal BeStockNew As clsBeStock,
                                                                   ByVal pIdUbicacionVirtualDestino As Integer,
                                                                   ByVal pIdEstadoDestino As Integer,
                                                                   ByRef lConnection As SqlConnection,
                                                                   ByRef lTransaction As SqlTransaction,
                                                                   Optional IdOperadorBodega As Integer = 0) As Integer

        Insertar_Movimientos_Recepcion_Area_SAP = 0

        Try

            Dim BeTransMovimiento As New clsBeTrans_movimientos()
            BeTransMovimiento.IdMovimiento = 0
            BeTransMovimiento.IdEmpresa = pIdEmpresa
            BeTransMovimiento.IdBodegaOrigen = pIdBodega
            BeTransMovimiento.IdTransaccion = BeStockNew.IdRecepcionEnc
            BeTransMovimiento.IdPropietarioBodega = BeStockNew.IdPropietarioBodega
            BeTransMovimiento.IdProductoBodega = BeStockNew.IdProductoBodega
            BeTransMovimiento.IdUbicacionOrigen = BeStockNew.IdUbicacion
            BeTransMovimiento.IdUbicacionDestino = pIdUbicacionVirtualDestino
            BeTransMovimiento.IdPresentacion = BeStockNew.Presentacion.IdPresentacion
            BeTransMovimiento.IdEstadoOrigen = BeStockNew.IdProductoEstado
            BeTransMovimiento.IdEstadoDestino = pIdEstadoDestino
            BeTransMovimiento.IdUnidadMedida = BeStockNew.IdUnidadMedida
            BeTransMovimiento.IdTipoTarea = clsDataContractDI.tTipoTarea.RECE
            BeTransMovimiento.IdBodegaDestino = pIdBodega
            BeTransMovimiento.IdRecepcion = BeStockNew.IdRecepcionEnc
            BeTransMovimiento.IdRecepcionDet = BeStockNew.IdRecepcionDet
            BeTransMovimiento.Cantidad = BeStockNew.Cantidad
            BeTransMovimiento.Serie = BeStockNew.Serial
            BeTransMovimiento.Peso = BeStockNew.Peso
            BeTransMovimiento.Lote = BeStockNew.Lote
            BeTransMovimiento.Barra_pallet = BeStockNew.Lic_plate
            BeTransMovimiento.Fecha_vence = BeStockNew.Fecha_vence
            BeTransMovimiento.Fecha_agr = Now
            BeTransMovimiento.Usuario_agr = pIdUsuario
            BeTransMovimiento.Serie = "TRASLADO_SAP"
            BeTransMovimiento.IdOperadorBodega = IdOperadorBodega

            '#CKFK20240527 Llené este objeto que venía vacío
            BeStockRec.IdProductoBodega = BeStockNew.IdProductoBodega
            BeStockRec.Lote = BeStockNew.Lote
            BeStockRec.Añada = BeStockNew.Añada
            BeStockRec.Fecha_vence = BeStockNew.Fecha_vence
            BeStockRec.Presentacion.IdPresentacion = BeStockNew.IdPresentacion
            BeStockRec.IdUnidadMedida = BeStockNew.IdUnidadMedida
            BeStockRec.ProductoEstado.IdEstado = BeStockNew.IdProductoEstado

            clsLnStock.Get_Existencia_By_IdProductoBodega(BeStockRec,
                                                          lConnection,
                                                          lTransaction)

            BeTransMovimiento.Cantidad_hist = BeStockRec.CantidadEnStock
            BeTransMovimiento.Peso_hist = BeStockRec.PesoEnStock
            BeTransMovimiento.IdProductoTallaColor = BeStockNew.IdProductoTallaColor

            If BeStockNew.IdProductoTallaColor <> 0 Then
                Dim BEProductoTallaColor As New clsBeProducto_talla_color
                BEProductoTallaColor = clsLnProducto_talla_color.GetSingle(BeStockNew.IdProductoTallaColor)
                BeTransMovimiento.Talla = If(clsLnTalla.GetSingle_By_IdTalla(BEProductoTallaColor.IdTalla)?.Codigo, "")
                BeTransMovimiento.Color = If(clsLnColor.GetSingle_By_IdColor(BEProductoTallaColor.IdColor)?.Codigo, "")
            Else
                BeTransMovimiento.Talla = ""
                BeTransMovimiento.Color = ""
            End If

            Insertar(BeTransMovimiento,
                     lConnection,
                     lTransaction)

            Insertar_Movimientos_Recepcion_Area_SAP = BeTransMovimiento.IdMovimiento

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} ", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Reporte_Cant_Movimientos_By_Operador_And_Producto(ByVal pFechaDel As Date,
                                                                                 ByVal pFechaAl As Date,
                                                                                 ByVal pIdProductoBodega As Integer,
                                                                                 ByVal pIdBodega As Integer,
                                                                                 ByVal pIdPropietarioBodega As Integer) As DataTable

        Get_Reporte_Cant_Movimientos_By_Operador_And_Producto = Nothing

        Try

            Dim vSQL As String = String.Empty

            vSQL = "SELECT 
                    CAST(dbo.trans_movimientos.fecha AS DATE) AS Fecha, 
                    dbo.operador.nombres + ' ' + dbo.operador.apellidos AS Operador,
                    (dbo.operador.nombres + ' ' + dbo.operador.apellidos + ' (' + CONVERT(VARCHAR, dbo.trans_movimientos.fecha, 23) + ')') AS FechaOperador,
                    dbo.producto.codigo AS Código,
                    dbo.producto.nombre AS Producto,
                    SUM(CASE WHEN dbo.sis_tipo_tarea.Nombre = 'UBIC' THEN 1 ELSE 0 END) AS CantidadCambiosUBIC,
                    SUM(CASE WHEN dbo.sis_tipo_tarea.Nombre = 'CEST' THEN 1 ELSE 0 END) AS CantidadCambiosCEST
                FROM 
                    dbo.trans_movimientos 
                    INNER JOIN dbo.sis_tipo_tarea ON dbo.trans_movimientos.IdTipoTarea = dbo.sis_tipo_tarea.IdTipoTarea 
                    INNER JOIN dbo.operador_bodega ON dbo.trans_movimientos.IdOperadorBodega = dbo.operador_bodega.IdOperadorBodega 
                    INNER JOIN dbo.operador ON dbo.operador_bodega.IdOperador = dbo.operador.IdOperador
                    INNER JOIN dbo.producto_bodega ON dbo.trans_movimientos.IdProductoBodega = dbo.producto_bodega.IdProductoBodega
                    INNER JOIN dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto
                WHERE 
                    dbo.sis_tipo_tarea.Nombre IN ('UBIC', 'CEST') "

            vSQL += " AND IdBodegaOrigen=@IdBodega and IdPropietarioBodega=@IdPropietarioBodega "

            If pIdProductoBodega <> 0 Then
                vSQL += " AND  IdProductoBodega =@IdProductoBodega "
            End If

            vSQL += String.Format(" And cast(Fecha AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += "GROUP BY     
                            dbo.operador.nombres, 
                            dbo.operador.apellidos,
                            dbo.producto.codigo,
                            dbo.producto.nombre,
	                        trans_movimientos.fecha
                        ORDER BY 
                            Fecha, 
                            Operador, 
                            Código"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                        If pIdProductoBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        End If

                        Dim lTable As New DataTable
                        lDataAdapter.Fill(lTable)


                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            Get_Reporte_Cant_Movimientos_By_Operador_And_Producto = lTable

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

    '#GT28102024: copia del metodo anterior, pero para cambios de estado, no incluye ubicacion.
    Public Shared Function Get_Reporte_Cant_Movimientos_Estado_By_Operador_And_Producto(ByVal pFechaDel As Date,
                                                                                        ByVal pFechaAl As Date,
                                                                                        ByVal pIdProductoBodega As Integer,
                                                                                        ByVal pIdBodega As Integer,
                                                                                        ByVal pIdPropietarioBodega As Integer) As DataTable

        Get_Reporte_Cant_Movimientos_Estado_By_Operador_And_Producto = Nothing

        Try

            Dim vSQL As String = String.Empty

            vSQL = "SELECT 
                    CAST(dbo.trans_movimientos.fecha AS DATE) AS Fecha, 
                    dbo.operador.nombres + ' ' + dbo.operador.apellidos AS Operador,
                    (dbo.operador.nombres + ' ' + dbo.operador.apellidos + ' (' + CONVERT(VARCHAR, dbo.trans_movimientos.fecha, 23) + ')') AS FechaOperador,
                    dbo.producto.codigo AS Código,
                    dbo.producto.nombre AS Producto,
                    SUM(CASE WHEN dbo.sis_tipo_tarea.Nombre = 'CEST' THEN 1 ELSE 0 END) AS CantidadCambiosCEST
                FROM 
                    dbo.trans_movimientos 
                    INNER JOIN dbo.sis_tipo_tarea ON dbo.trans_movimientos.IdTipoTarea = dbo.sis_tipo_tarea.IdTipoTarea 
                    INNER JOIN dbo.operador_bodega ON dbo.trans_movimientos.IdOperadorBodega = dbo.operador_bodega.IdOperadorBodega 
                    INNER JOIN dbo.operador ON dbo.operador_bodega.IdOperador = dbo.operador.IdOperador
                    INNER JOIN dbo.producto_bodega ON dbo.trans_movimientos.IdProductoBodega = dbo.producto_bodega.IdProductoBodega
                    INNER JOIN dbo.producto ON dbo.producto_bodega.IdProducto = dbo.producto.IdProducto
                WHERE 
                    dbo.sis_tipo_tarea.Nombre = 'CEST' "

            vSQL += " AND IdBodegaOrigen=@IdBodega and IdPropietarioBodega=@IdPropietarioBodega "

            If pIdProductoBodega <> 0 Then
                vSQL += " AND  IdProductoBodega =@IdProductoBodega "
            End If

            vSQL += String.Format(" And cast(Fecha AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += "GROUP BY     
                            dbo.operador.nombres, 
                            dbo.operador.apellidos,
                            dbo.producto.codigo,
                            dbo.producto.nombre,
	                        trans_movimientos.fecha
                        ORDER BY 
                            Fecha, 
                            Operador, 
                            Código"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                        If pIdProductoBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        End If

                        Dim lTable As New DataTable
                        lDataAdapter.Fill(lTable)


                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                            Get_Reporte_Cant_Movimientos_Estado_By_Operador_And_Producto = lTable

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

    Public Shared Function Get_Reporte_Ajustes_Stock_By_Fechas(ByVal pFechaDel As Date,
                                                               ByVal pFechaAl As Date,
                                                               ByVal pIdBodega As Integer) As DataTable

        Get_Reporte_Ajustes_Stock_By_Fechas = Nothing

        Try

            Dim vSQL As String = String.Empty

            vSQL = "SELECT 
                    * FROM VW_Ajustes_Det
                WHERE 
                    IdBodega=@IdBodega "

            vSQL += String.Format(" And cast(Fecha AS DATE) BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += "ORDER BY 
                            Fecha "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lTable As New DataTable
                        lDataAdapter.Fill(lTable)


                        If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then
                            Get_Reporte_Ajustes_Stock_By_Fechas = lTable
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

    Public Shared Function Get_All_Movimientos_By_Lote(ByVal pFechaDel As Date,
                                                       ByVal pFechaAl As Date,
                                                       ByVal pIdProducto As Integer,
                                                       ByVal lote As String,
                                                       ByVal fecha As Date,
                                                       ByVal lConnection As SqlConnection,
                                                       ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_Movimientos)

        Dim lReturnList As New List(Of clsBeVW_Movimientos)

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT Codigo,Producto, SUM(cantidad) AS Cantidad,
                        EstadoOrigen, 
                        EstadoDestino, 
                        TipoTarea, lote,Fecha_Vence, IdTipoTarea, 
                        IdPresentacion, IdUnidadMedida, IdEstadoOrigen, 
                        IdProductoBodega,Fecha,
                        Umbas, EstadoOrigen, Presentación
                        FROM VW_Movimientos_N 
                        WHERE IdProducto=@IdProducto and lote=@lote and fecha_vence=@fecha 
                        AND TIPOTAREA NOT IN ('AJCANTNI','AJCANTPI')"

            vSQL += String.Format(" And Fecha BETWEEN {0} And {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            vSQL += " GROUP BY codigo,producto,EstadoOrigen, 
                    EstadoDestino, IdProductoBodega,
                    TipoTarea, lote,fecha_vence, 
                    IdTipoTarea,Fecha, IdPresentacion, 
                    IdUnidadMedida, IdEstadoOrigen,
                    Umbas, EstadoOrigen, Presentación"

            vSQL += " ORDER BY Codigo, Lote, Fecha"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                lDTA.SelectCommand.Parameters.AddWithValue("@lote", lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@fecha", fecha)
                Dim lTable As New DataTable
                lDTA.Fill(lTable)

                Dim Obj As clsBeVW_Movimientos

                If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lTable.Rows
                        Obj = New clsBeVW_Movimientos
                        clsLnVW_Movimientos.Cargar(Obj, lRow)
                        lReturnList.Add(Obj)
                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    '#CKFK20250323 Agregué esta función para eliminar el movimiento del picking
    Public Shared Sub Eliminar_Movimiento_Picking_By_IdMovimiento(ByVal IdMovimiento As Integer,
                                                                  Optional ByVal pConnection As SqlConnection = Nothing,
                                                                  Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from trans_movimientos 
                                   Where IdMovimiento = @IdMovimiento And IdTipoTarea = @IdTipoTarea"

            Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConnection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdMovimiento", IdMovimiento))
            cmd.Parameters.Add(New SqlParameter("@IdTipoTarea", clsDataContractDI.tTipoTarea.PIK))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Sub

    Public Shared Function Get_Single_By_IdMovimiento(ByVal IdMovimiento As Integer,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As clsBeTrans_movimientos


        Get_Single_By_IdMovimiento = Nothing

        Try

            Dim vSQL As String = ""

            vSQL = "SELECT * FROM trans_Movimientos 
                     WHERE IdMovimiento = @IdMovimiento "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento)

                Dim lTable As New DataTable
                lDTA.Fill(lTable)

                Dim Obj As clsBeTrans_movimientos

                If lTable IsNot Nothing AndAlso lTable.Rows.Count > 0 Then

                    Obj = New clsBeTrans_movimientos
                    Cargar(Obj, lTable.Rows(0))
                    Get_Single_By_IdMovimiento = Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub Eliminar_Movimiento_Verificacion_By_PickingUbic(ByVal BeTransPickingUbic As clsBeTrans_movimientos,
                                                                      Optional ByVal pConnection As SqlConnection = Nothing,
                                                                      Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from trans_movimientos 
                                   Where IdTipoTarea = @IdTipoTarea 
                                    AND IdBodegaOrigen = @IdBodegaOrigen 
                                    AND IdTransaccion = @IdTransaccion 
                                    AND IdProductoBodega = @IdProductoBodega
                                    AND IdPropietarioBodega = @IdPropietarioBodega
                                    AND IdEstadoOrigen= @IdEstadoOrigen
                                    AND (IdPresentacion=  @IdPresentacion OR IdPresentacion IS NULL)
                                    AND IdUnidadMedida= @IdUnidadMedida
                                    AND IdRecepcion= @IdRecepcion
                                    AND cantidad= @cantidad                                    
                                    AND lote= @lote
                                    AND fecha_vence= @fecha_vence
                                    AND barra_pallet= @barra_pallet
                                    AND IdPedidoEnc= @IdPedidoEnc
                                    AND IdPedidoDet= @IdPedidoDet "

            Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConnection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdTipoTarea", clsDataContractDI.tTipoTarea.VERI))
            cmd.Parameters.Add(New SqlParameter("@IdBodegaOrigen", BeTransPickingUbic.IdBodegaOrigen))
            cmd.Parameters.Add(New SqlParameter("@IdTransaccion", BeTransPickingUbic.IdTransaccion))
            cmd.Parameters.Add(New SqlParameter("@IdProductoBodega", BeTransPickingUbic.IdProductoBodega))
            cmd.Parameters.Add(New SqlParameter("@IdPropietarioBodega", BeTransPickingUbic.IdPropietarioBodega))
            cmd.Parameters.Add(New SqlParameter("@IdEstadoOrigen", BeTransPickingUbic.IdEstadoOrigen))
            cmd.Parameters.Add(New SqlParameter("@IdPresentacion", BeTransPickingUbic.IdPresentacion))
            cmd.Parameters.Add(New SqlParameter("@IdUnidadMedida", BeTransPickingUbic.IdUnidadMedida))
            cmd.Parameters.Add(New SqlParameter("@IdRecepcion", BeTransPickingUbic.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@cantidad", BeTransPickingUbic.Cantidad))
            cmd.Parameters.Add(New SqlParameter("@lote", BeTransPickingUbic.Lote))
            cmd.Parameters.Add(New SqlParameter("@fecha_vence", BeTransPickingUbic.Fecha_vence))
            cmd.Parameters.Add(New SqlParameter("@barra_pallet", BeTransPickingUbic.Barra_pallet))
            cmd.Parameters.Add(New SqlParameter("@IdPedidoEnc", BeTransPickingUbic.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IdPedidoDet", BeTransPickingUbic.IdPedidoDet))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Sub

    '#GT15052025: obtener movimiento segun recepcion enc, det y si tuviera lic_plate
    Public Shared Function GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet(ByVal pIdRecepcionEnc As Integer, ByVal pIdRecepcionDet As Integer, ByVal pLic_plate As String,
                                                                          Optional ByVal pConnection As SqlConnection = Nothing,
                                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeTrans_movimientos

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet = Nothing

        Try

            Dim sp As String = "SELECT * FROM Trans_movimientos Where(IdRecepcion = @pIdRecepcionEnc and IdRecepcionDet=@pIdRecepcionDet and IdTransaccion=@IdTransaccion) "

            If Not String.IsNullOrEmpty(pLic_plate) Then
                sp += " and barra_pallet =@pLic_plate "
            End If

            Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(sp, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(sp, lConnection)
            End If

            lDTA.SelectCommand.Parameters.Add(New SqlParameter("@pIdRecepcionEnc", pIdRecepcionEnc))
            lDTA.SelectCommand.Parameters.Add(New SqlParameter("@pIdRecepcionDet", pIdRecepcionDet))
            lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdTransaccion", pIdRecepcionEnc))

            If Not String.IsNullOrEmpty(pLic_plate) Then
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@pLic_plate", pLic_plate))
            End If

            Dim dt As New DataTable
            lDTA.Fill(dt)

            lDTA.Dispose()

            If dt.Rows.Count = 1 Then
                Dim oBeTrans_movimientos As New clsBeTrans_movimientos
                Cargar(oBeTrans_movimientos, dt.Rows(0))
                GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet = oBeTrans_movimientos
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    '#GT06102025: sino existe movimiento segun recepción (datos legacy) validar si existen por lic_plate únicamente
    Public Shared Function GetSingle_By_LicPlate(ByVal pIdRecepcionEnc As Integer, ByVal pLic_plate As String,
                                                                          Optional ByVal pConnection As SqlConnection = Nothing,
                                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeTrans_movimientos

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        GetSingle_By_LicPlate = Nothing

        Try

            Dim sp As String = "SELECT * FROM Trans_movimientos Where( IdTransaccion=@IdTransaccion and IdTipoTarea=1) "

            If Not String.IsNullOrEmpty(pLic_plate) Then
                sp += " and barra_pallet =@pLic_plate "
            End If

            Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(sp, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(sp, lConnection)
            End If

            'lDTA.SelectCommand.Parameters.Add(New SqlParameter("@pIdRecepcionEnc", pIdRecepcionEnc))
            'lDTA.SelectCommand.Parameters.Add(New SqlParameter("@pIdRecepcionDet", pIdRecepcionDet))
            lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdTransaccion", pIdRecepcionEnc))

            If Not String.IsNullOrEmpty(pLic_plate) Then
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@pLic_plate", pLic_plate))
            End If

            Dim dt As New DataTable
            lDTA.Fill(dt)

            lDTA.Dispose()

            If dt.Rows.Count = 1 Then
                Dim oBeTrans_movimientos As New clsBeTrans_movimientos
                Cargar(oBeTrans_movimientos, dt.Rows(0))
                GetSingle_By_LicPlate = oBeTrans_movimientos
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_Reporte_By_Rango_Fechas_For_Inv(ByVal pFechaDel As Date,
                                                                               ByVal pFechaAl As Date,
                                                                               ByVal IdBodega As Integer) As DataTable

        Get_All_Movimientos_Reporte_By_Rango_Fechas_For_Inv = Nothing

        Try
            Dim vSQL As String = "SELECT codigo, producto, tipotarea, cantidad, ubicorigen, ubicdestino, 
                                     estadoorigen, estadodestino, fecha, licencia, clasificacion, 
                                     familia, operador 
                              FROM VW_Movimientos_N 
                              WHERE 1 = 1 "

            ' Filtro por fecha con hora
            vSQL += String.Format(" AND fecha BETWEEN {0} AND {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            ' Filtro por bodega
            vSQL += String.Format(" AND IdBodega = {0} ", IdBodega)

            ' Orden final
            vSQL += " ORDER BY codigo, fecha"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)
                    dad.SelectCommand.CommandType = CommandType.Text
                    Dim lTable As New DataTable
                    dad.Fill(lTable)

                    Get_All_Movimientos_Reporte_By_Rango_Fechas_For_Inv = lTable

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function Get_All_Movimientos_Reporte_By_Rango_Fechas_For_Inv(ByVal pFechaDel As Date,
                                                                               ByVal pFechaAl As Date,
                                                                               ByVal IdBodega As Integer,
                                                                               ByVal IdInventarioEnc As Integer,
                                                                               ByVal lConnection As SqlConnection,
                                                                               ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_Movimientos_Reporte_By_Rango_Fechas_For_Inv = Nothing

        Try

            Dim vSQL As String = "SELECT codigo as Codigo, producto as Producto, tipotarea as Tipo, cantidad as Cantidad, 
                                         ubicorigen as Origen, ubicdestino as Destino, 
                                         estadoorigen as EstadoOri, estadodestino as EstadoDest, fecha as Fecha, licencia as Licencia, clasificacion as Clasificacion, 
                                         familia as Familia 
                                  FROM VW_Movimientos_N 
                                  WHERE IdProducto IN (
                                      SELECT DISTINCT pb.IdProducto 
                                      FROM trans_inv_ciclico invc
                                      JOIN producto_bodega pb ON invc.IdProductoBodega = pb.IdProductoBodega 
                                      WHERE invc.IdInventarioEnc = " & IdInventarioEnc.ToString() & "
                                  )
                                  AND (
                                      IdUbicacionOrigen IN (
                                          SELECT DISTINCT IdUbicacion 
                                          FROM trans_inv_ciclico_ubic 
                                          WHERE IdInventarioEnc = " & IdInventarioEnc & " AND IdBodega = " & IdBodega & "
                                      )
                                      OR 
                                      IdUbicacionDestino IN (
                                          SELECT DISTINCT IdUbicacion 
                                          FROM trans_inv_ciclico_ubic 
                                          WHERE IdInventarioEnc = " & IdInventarioEnc & " AND IdBodega = " & IdBodega & "
                                      )
                                  )"

            ' Filtro por fecha con hora
            vSQL += String.Format(" AND fecha BETWEEN {0} AND {1}", FormatoFechas.fFechaHora(pFechaDel), FormatoFechas.fFechaHora(pFechaAl))

            ' Filtro por bodega
            vSQL += String.Format(" AND IdBodega = {0} ", IdBodega)

            ' Orden final
            vSQL += " ORDER BY codigo, fecha"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.CommandType = CommandType.Text

            Dim lTable As New DataTable
            dad.Fill(lTable)

            Get_All_Movimientos_Reporte_By_Rango_Fechas_For_Inv = lTable

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function


End Class
