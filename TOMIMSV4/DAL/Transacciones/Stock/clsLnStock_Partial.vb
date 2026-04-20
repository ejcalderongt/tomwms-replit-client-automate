Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks

Partial Public Class clsLnStock

    Private Shared lpBeProductoOutput As New List(Of clsBeProducto)
    Private Shared lBeBodega As New List(Of clsBeBodega)

    ''' <summary>
    ''' Busca las existencias para un IdPropietarioBodega
    ''' </summary>
    ''' <param name="pIdPropietarioBodega">El IdPropietario asociado a una bodega</param>
    ''' <returns>Devuelve lista de ojbetos de tipo: clsBeVW_stock_res (vista VW_Stock_SP en la BD)</returns>
    ''' <remarks>ejcalderon_20160512</remarks>
    Public Shared Function Get_All_By_IdPropietarioBodega(ByVal pIdPropietarioBodega As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "Select * from VW_Stock_Res where VW_Stock_Res.IdPropietario = @IdPropietario"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietarioBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim BeVW_stock_res As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                BeVW_stock_res = New clsBeVW_stock_res
                                clsLnVW_stock_res.Cargar(BeVW_stock_res,
                                                         lRow,
                                                         lConnection,
                                                         lTransaction)
                                lReturnList.Add(BeVW_stock_res)

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

    ''' <summary>
    ''' Get_All_Stock_By_IdBodega_And_IdPropietarioBodega
    ''' #EJC20221011_1153: WMS - Corrección de transacción en método: Get_All_Stock_By_IdBodega_And_IdPropietarioBodega
    ''' </summary>
    ''' <param name="IdBodega"></param>
    ''' <param name="IdPropietarioBodega"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_Stock_By_IdBodega_And_IdPropietarioBodega(ByVal IdBodega As Integer,
                                                                             ByVal IdPropietarioBodega As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        'GT 27082021: Se agregó otro campo en la relación vw_stock_res y bodega_ubicacion

        Get_All_Stock_By_IdBodega_And_IdPropietarioBodega = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#GT18032022_0904: se añade la relación de idbodega
            Const vSQl As String = "SELECT *
									FROM VW_Stock_Res stock_res inner join
									bodega_ubicacion b_ubicacion ON stock_res.IdUbicacion = b_ubicacion.IdUbicacion 
                                    and stock_res.IdTramo= b_ubicacion.IdTramo
									and stock_res.IdBodega = b_ubicacion.IdBodega
                                    inner join bodega_tramo b_tramo ON b_tramo.IdTramo = b_ubicacion.IdTramo 
                                    and b_tramo.IdBodega= b_ubicacion.IdBodega
                                    Where stock_res.IdBodega=@IdBodega and 
                                    stock_res.IdPropietarioBodega=@IdPropietarioBodega
                                    ORDER BY stock_res.codigo"

            Using lDTA As New SqlDataAdapter(vSQl, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lrow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res
                        clsLnVW_stock_res.Cargar(Obj, lrow, lConnection, lTransaction)
                        Debug.Print("Fetching stock: " & Obj.IdStock)

                        If lrow("Nombre_Completo") IsNot DBNull.Value AndAlso lrow("Nombre_Completo") IsNot Nothing Then
                            Obj.Ubicacion_Nombre = CType(lrow("Nombre_Completo"), String)
                        End If

                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            lTransaction.Commit()

            Get_All_Stock_By_IdBodega_And_IdPropietarioBodega = lReturnList

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Reporte_Stock_DataTable(ByVal pIdBodega As Integer,
                                                       ByVal pIdPropietarioBodega As Integer) As DataTable


        Get_Reporte_Stock_DataTable = Nothing

        Try

            Dim vSQL As String = "SELECT 
                                  Propietario, 
                                  IdProducto,
                                  Codigo, 
                                  Codigo_barra,                                  
                                  Nombre,                                  
                                  UnidadMedida,
                                  IdPresentacion,
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
                                    Fecha_Ingreso, Nombre_Completo AS [Ubicación],
                                    codigo_poliza,
									Numero_poliza numero_orden,
                                    Clasificacion,Marca,Familia,Tipo,parametro_a,parametro_b
							        FROM VW_Stock_Res WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega"
            End If

            vSQL += " Group by IdProducto,NomEstado,Fecha_Ingreso,Codigo,
						  Nombre,Presentacion,IdPresentacion,UnidadMedida, 
						  Nombre_Completo, Factor, Propietario, codigo_barra,
                          codigo_poliza,Numero_poliza, 
                          Clasificacion,Marca,Familia,Tipo,parametro_a,parametro_b "

            vSQL += " ORDER BY CODIGO, Nombre_Completo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

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

    Public Shared Function Get_Reporte_Valoracion_Stock_DataTable(ByVal pIdBodega As Integer,
                                                                  ByVal pIdPropietarioBodega As Integer) As DataTable


        Get_Reporte_Valoracion_Stock_DataTable = Nothing

        Try

            '#GT26092022_1620: Se agregan campos para uso DyD (clasificacion, marca, familia, tipo...)
            Dim vSQL As String = "select 
                                  Propietario,
                                  Codigo,
                                  Nombre,
                                  SUM(CantidadSF) as Cantidad_UMBas,
                                  UnidadMedida as UMBas,
                                  SUM(Cantidad) as Cantidad_Pres,
                                  NomEstado as Estado,
                                  Costo,
                                  SUM(ISNULL(CANTIDADRESERVADA,0)) AS Cantidad_Reservada_UMBas,
                                  CASE WHEN FACTOR > 0
                                    THEN 
	                                    ROUND(SUM(ISNULL(CantidadReservada,0)) / Factor,6)
                                    ELSE
                                    0
                                    END AS Cantidad_Reservada_Pres,
                                  SUM(CantidadSF - ISNULL(CANTIDADRESERVADA,0)) AS Disponible_UMBas,
                                  CASE WHEN FACTOR > 0
                                    THEN 
	                                    ROUND(SUM(ISNULL(Cantidad,0)),6)  -ROUND(SUM(ISNULL(CantidadReservada,0)) / Factor,6)
	                                    --ROUND(SUM(isnull(Cantidad,0)) - SUM(isnull(CantidadReservada/Factor,0)),6)
                                    ELSE
                                    0
                                    END AS Disponible_Presentacion,
                                  Factor,
                                  CASE WHEN IdPresentacion <> 0
	                              THEN	
		                            (ROUND(SUM(ISNULL(Cantidad,0)),6)  -ROUND(SUM(ISNULL(CantidadReservada,0)) / Factor,6)) * COSTO
	                              ELSE
		                            SUM(CantidadSF - ISNULL(CANTIDADRESERVADA,0)) * COSTO
                                  END AS Total,
                                  codigo_poliza,
							      Numero_poliza numero_orden,
                                  Clasificacion, Marca, Familia, Tipo, parametro_a, parametro_b   
                                  FROM VW_Stock_Res  WHERE 1 > 0  "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega "
            End If

            vSQL += " GROUP BY
                    propietario,codigo,nombre,
                    UnidadMedida,NomEstado,Costo,
                    IdPresentacion,Factor, codigo_poliza,
					Numero_poliza,Clasificacion, Marca, Familia, Tipo, parametro_a, parametro_b"

            vSQL += " ORDER BY CODIGO, Nombre "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_Reporte_Valoracion_Stock_DataTable = lDataTable

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

    Public Shared Function Get_Reporte_Valoracion_By_OC_DataTable(ByVal pIdBodega As Integer,
                                                                  ByVal pIdPropietarioBodega As Integer) As DataTable


        Get_Reporte_Valoracion_By_OC_DataTable = Nothing

        Try

            Dim vSQL As String = "select 
                                    Propietario,
                                    Codigo,
                                    Nombre,
                                    SUM(CantidadSF) as Cantidad_UMBas,
                                    UnidadMedida as UMBas,
                                    SUM(Cantidad) as Cantidad_Pres,
                                    NomEstado as Estado,
                                    costo_oc as Costo,
                                    SUM(ISNULL(CANTIDADRESERVADA,0)) AS Cantidad_Reservada_UMBas,
                                    SUM((ISNULL(CANTIDADRESERVADA,0) / factor)) AS Cantidad_Reservada_Pres,
                                    SUM(CantidadSF - ISNULL(CANTIDADRESERVADA,0)) AS Disponible_UMBas,
                                    SUM(Cantidad  - (ISNULL(CANTIDADRESERVADA,0) / factor)) as Disponible_Pres,
                                    Factor,
                                    CASE WHEN IdPresentacion <> 0
	                                    THEN	
		                                    ((SUM(Cantidad  - (ISNULL(CANTIDADRESERVADA,0) / factor)))) * costo_oc
	                                    ELSE
		                                    SUM(CantidadSF - ISNULL(CANTIDADRESERVADA,0)) * costo_oc
                                    END AS Total,
                                    codigo_poliza,
									numero_orden
                                    from vw_valorizacion_oc  WHERE 1 > 0  "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega "
            End If

            vSQL += "GROUP BY
                    propietario,codigo,nombre,
                    UnidadMedida,NomEstado,costo_oc,
                    IdPresentacion,Factor, codigo_poliza,
					numero_orden "


            vSQL += " having 
					  CASE WHEN IdPresentacion <> 0
	                                    THEN	
		                                    ((SUM(Cantidad  - (ISNULL(CANTIDADRESERVADA,0) / factor)))) * costo_oc
	                                    ELSE
		                                    SUM(CantidadSF - ISNULL(CANTIDADRESERVADA,0)) * costo_oc
                                    END
					  > 0 "

            vSQL += "ORDER BY CODIGO, Nombre "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

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



    Public Shared Function Get_Reporte_Stock_All_DataTable(ByVal pIdBodega As Integer,
                                                           ByVal pIdPropietarioBodega As Integer,
                                                           ByRef DTDetalleSeries As DataTable) As DataTable


        Get_Reporte_Stock_All_DataTable = Nothing

        Try

            Dim vSQL As String = "select * from VW_Stock_Res  WHERE 1 > 0  "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega "
            End If

            vSQL += "ORDER BY CODIGO, Nombre "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_Reporte_Stock_All_DataTable = lDataTable

                            DTDetalleSeries = Get_All_Stock_Detalle_Series(pIdBodega, pIdPropietarioBodega, lConnection, lTransaction)

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

    Public Shared Function Get_All_Stock_Consolidado_DT_Report(ByVal pIdBodega As Integer,
                                                               ByVal pIdPropietarioBodega As Integer) As DataTable


        Get_All_Stock_Consolidado_DT_Report = Nothing

        Try

            '#CKFK 20211228 Corrección del disponible en presentación
            '#GT26092022_1740: se agregan campos para uso DyD (Familia,Marca,Tipo...)
            '#GT05122022_1600: se agregan campos del producto (precio,costo) y el costo unitario de la oc
            Dim vSQL As String = "SELECT 
                                    VW_Stock_Res.Propietario, 
                                    VW_Stock_Res.IdProducto, 
                                    VW_Stock_Res.codigo, 
                                    VW_Stock_Res.codigo_barra, 
                                    VW_Stock_Res.nombre, 
                                    VW_Stock_Res.UnidadMedida, 
                                    ISNULL(VW_Stock_Res.IdPresentacion, 0) AS IdPresentacion, 
                                    ISNULL(VW_Stock_Res.Presentacion, '') AS Presentacion, 
                                    VW_Stock_Res.NomEstado AS Estado, 
                                    SUM(ISNULL(VW_Stock_Res.CantidadSF, 0)) AS CantidadUMBas, 
                                    SUM(ISNULL(VW_Stock_Res.Cantidad_Presentacion, 0)) AS CantidadPresentacion, 
                                    SUM(ISNULL(VW_Stock_Res.CantidadReservada, 0)) AS Cantidad_Reservada_UMBas, 
                                    VW_Stock_Res.factor, 
                                    CASE WHEN ISNULL(Factor, 0) > 0 THEN ROUND(SUM(ISNULL(CantidadReservada, 0)) / Factor, 6) ELSE 0 END AS Cantidad_Reservada_Pres, 
                                    SUM(ISNULL(VW_Stock_Res.Disponible_UMBas, 0)) AS Disponible_UMBas, 
                                    SUM(ISNULL(VW_Stock_Res.Disponible_Presentacion, 0)) AS Disponible_Presentación, 
                                    SUM(ISNULL(VW_Stock_Res.peso, 0)) AS Peso, 
                                    VW_Stock_Res.Codigo_Poliza, 
                                    VW_Stock_Res.Numero_poliza AS numero_orden, 
                                    VW_Stock_Res.Clasificacion, 
                                    VW_Stock_Res.Area, 
                                    VW_Stock_Res.Familia, 
                                    VW_Stock_Res.marca, 
                                    VW_Stock_Res.tipo, 
                                    VW_Stock_Res.parametro_a, 
                                    VW_Stock_Res.parametro_b, 
                                    VW_Stock_Res.precio_producto, 
                                    VW_Stock_Res.costo_producto, 
                                    VW_Stock_Res.costo_ingreso 
                                FROM 
                                    VW_Stock_Res 
                                  WHERE 1 > 0   "

            If pIdBodega <> 0 Then
                vSQL += " AND VW_Stock_Res.IdBodega = @IdBodega"
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND VW_Stock_Res.IdPropietarioBodega = @IdPropietarioBodega"
            End If

            vSQL += " Group by IdProducto,NomEstado,Codigo,
						  Nombre,Presentacion,VW_Stock_Res.IdPresentacion,UnidadMedida, 
						  Factor, Propietario, codigo_barra,codigo_poliza,
						  Numero_poliza,clasificacion,Area,
                          Familia,Marca,Tipo,parametro_a,parametro_b,
                          precio_producto,costo_producto,costo_ingreso "

            vSQL += " ORDER BY CODIGO, Nombre "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_Stock_Consolidado_DT_Report = lDataTable

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

    Public Shared Function Get_All_Stock_Consolidado_DT_Report_Clasificacion(ByVal pIdBodega As Integer,
                                                                             ByVal pIdPropietarioBodega As Integer) As DataTable


        Get_All_Stock_Consolidado_DT_Report_Clasificacion = Nothing

        Try

            Dim vSQL As String = "SELECT 
                                  Propietario, 
                                  IdProducto,
                                  Codigo, 
                                  Codigo_barra,                                  
                                  Nombre,                                  
                                  UnidadMedida,
                                  IdPresentacion,
                                  Presentacion,
                                  IdClasificacion,
                                  Clasificacion,
                                  NomEstado as Estado,
                                  Sum(isnull(CantidadSF,0)) as CantidadUMBas,
							      SUM(isnull(Cantidad,0)) as CantidadPresentacion,
							      SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							        CASE WHEN FACTOR > 0
							        THEN 
								        ROUND(SUM(isnull(CantidadReservada/Factor,0)),6)
							        ELSE
								        0
							        END AS Cantidad_Reservada_Pres,
							        ROUND(SUM(isnull(CantidadSF,0)) - SUM(isnull(CantidadReservada,0)),6)  as Disponible_UMBas, 
							        CASE WHEN FACTOR > 0
							        THEN 
								        ROUND(SUM(isnull(Cantidad,0)) - SUM(isnull(CantidadReservada/Factor,0)),6)
							        ELSE
								        0
							        END AS Disponible_Presentación,
							        SUM(isnull(Peso,0)) as Peso
							        FROM VW_Stock_Res_Clasificacion WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega"
            End If

            vSQL += " Group by IdProducto,NomEstado,Codigo,
						  Nombre,Presentacion,IdPresentacion,UnidadMedida, 
						  Factor, Propietario, codigo_barra,
                          IdClasificacion, Clasificacion "

            vSQL += "ORDER BY CODIGO, Nombre "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

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

    Public Delegate Sub ChangeLabelDelegate(ByVal pMensaje As String)

    <ThreadStatic()>
    Public Shared lReturnListStock As New List(Of clsBeVW_stock_res)

    Public Shared Function Get_All_Stock_By_IdBodega_And_IdPropietario(ByVal DelSubUpdLbl As ChangeLabelDelegate,
                                                                       ByVal pIdBodega As Integer,
                                                                       ByVal pIdPropietarioBodega As Integer) As List(Of clsBeVW_stock_res)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        lReturnListStock = New List(Of clsBeVW_stock_res)

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQl As String = "Select * from VW_Stock_Res WHERE IdBodega= @IdBodega "

            If pIdPropietarioBodega <> 0 Then
                vSQl += " AND IdPropietarioBodega =@IdPropietarioBodega"
            End If


            Using lDTA As New SqlDataAdapter(vSQl, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                If pIdPropietarioBodega <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lrow In lDataTable.Rows
                        Obj = New clsBeVW_stock_res
                        clsLnVW_stock_res.Cargar(Obj, lrow, lConnection, lTransaction)
                        Debug.Print("Fetching_stock: " & Obj.IdStock)
                        lReturnListStock.Add(Obj)
                    Next

                End If

            End Using

            Debug.Print("Fin_Fetch.")

            lTransaction.Commit()

            Return lReturnListStock

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
            End If
        End Try

    End Function

    Public Shared Function Get_All_Stock_By_IdBodega_And_IdPropietario(ByVal DelSubUpdLbl As ChangeLabelDelegate,
                                                                       ByVal pIdBodega As Integer,
                                                                       ByVal pIdPropietarioBodega As Integer,
                                                                       ByVal lConnection As SqlConnection,
                                                                       ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Dim lReturnListStock As New List(Of clsBeVW_stock_res)

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            Const vSQL As String = "Select * from VW_Stock_Res where IdBodega=@IdBodega and IdPropietarioBodega=@IdPropietarioBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lrow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res

                        clsLnVW_stock_res.Cargar(Obj, lrow, lConnection, lTransaction)

                        If Obj.IdStock <> 0 Then
                            lReturnListStock.Add(Obj)
                            Debug.Print("Fetching stock: " & Obj.IdStock)
                            DelSubUpdLbl("Fetching stock: " & Obj.IdStock)
                        End If

                    Next

                End If

                If lDataTable.Rows.Count <> lReturnListStock.Count Then
                    Debug.Print("Algo salió mal")
                Else
                    DelSubUpdLbl("")
                End If

            End Using

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

            Return lReturnListStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Stock_By_IdBodega(ByVal IdBodega As Integer,
                                                     ByVal IdRe As Integer,
                                                     ByVal DelSubUpdLbl As ChangeLabelDelegate,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Dim lReturnListStock As New List(Of clsBeVW_stock_res)

        '#EJC20171112_0605PM:Agregué transacción

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            Dim vSQL As String = "SELECT * from VW_Stock_Res where IdBodega=@IdBodega and IdPropietarioBodega=@IdPropietarioBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdRe)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lrow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res

                        clsLnVW_stock_res.Cargar(Obj, lrow, lConnection, lTransaction)

                        If Obj.IdStock <> 0 Then
                            lReturnListStock.Add(Obj)
                            Debug.Print("Fetching stock: " & Obj.IdStock)
                            DelSubUpdLbl("Fetching stock: " & Obj.IdStock)
                        End If

                    Next

                End If

                If lDataTable.Rows.Count <> lReturnListStock.Count Then
                    Debug.Print("Algo salió mal")
                Else
                    DelSubUpdLbl("")
                End If

            End Using

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

            Return lReturnListStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    '#GT16032022_0837: para el inventario en linea cealsa
    Public Shared Function Get_All_Stock_By_IdBodega(ByVal DelSubUpdLbl As ChangeLabelDelegate,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Dim lReturnListStock As New List(Of clsBeVW_stock_res)

        '#EJC20171112_0605PM:Agregué transacción

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            Dim vSQL As String = "SELECT * from VW_Stock_Res "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lrow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res

                        clsLnVW_stock_res.Cargar(Obj, lrow, lConnection, lTransaction)

                        If Obj.IdStock <> 0 Then
                            lReturnListStock.Add(Obj)
                            Debug.Print("Fetching stock: " & Obj.IdStock)
                            DelSubUpdLbl("Fetching stock: " & Obj.IdStock)
                        End If

                    Next

                End If

                If lDataTable.Rows.Count <> lReturnListStock.Count Then
                    Debug.Print("Algo salió mal")
                Else
                    DelSubUpdLbl("")
                End If

            End Using

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

            Return lReturnListStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Stock_DT(ByVal IdBodega As Integer,
                                            ByVal IdPropietarioBodega As Integer) As DataTable

        '#EJC20171112_0605PM:Agregué transacción
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#CKFK20221017 Modifiqué la vista VW_Stock_Resumen por la VW_Stock_Res
            Dim vSQL As String = "SELECT * FROM VW_Stock_Res WHERE IdBodega=@IdBodega and disponible_umbas > 0 AND IdPropietarioBodega = @IdPropietarioBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)
                lDTA.SelectCommand.CommandTimeout = 0
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Get_All_Stock_DT = lDataTable

                lTransaction.Commit()

            End Using

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_Stock_DT(ByVal IdBodega As Integer,
                                            ByVal IdPropietarioBodega As Integer,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction) As DataTable

        '#EJC20171112_0605PM:Agregué transacción

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            '#CKFK20221017 Modifiqué la vista VW_Stock_Resumen por la VW_Stock_Res
            Dim vSQL As String = "SELECT * FROM VW_Stock_Res WHERE IdBodega=@IdBodega and disponible_umbas > 0 AND IdPropietarioBodega = @IdPropietarioBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Stock_Especifico_DT(ByVal IdBodega As Integer,
                                                       ByVal IdCliente As Integer,
                                                       ByRef TieneTiempos As Boolean,
                                                       ByVal NoPoliza As String,
                                                       ByVal IdPropietarioBodega As Integer,
                                                       ByVal IdProductoEstadoDefault As Integer,
                                                       ByVal Mostrar_Talla_Color As Boolean) As DataTable

        '#EJC20171112_0605PM:Agregué transacción
        Dim vSQL As String = ""

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim watch As Stopwatch = Stopwatch.StartNew()

            TieneTiempos = clsLnCliente_tiempos.Cliente_Tiene_Tiempos(IdCliente, lConnection, lTransaction)

            If TieneTiempos Then

                '#GT27082025
                vSQL = "SELECT producto_bodega.IdBodega, 
                        propietarios.IdPropietario, 
                        propietario_bodega.IdPropietarioBodega, 
                        producto.IdProducto, 
                        producto_bodega.IdProductoBodega, dbo.
                        stock.IdStock, 
                        ISNULL(stock.IdUbicacion_anterior, 0) AS IdUbicacion_anterior, 
                        unidad_medida.IdUnidadMedida, 
                        stock.IdProductoEstado, 
                        ISNULL(stock.IdPresentacion, 0) AS IdPresentacion, 
                        ISNULL(stock.IdRecepcionEnc, 0) AS IdRecepcionEnc, 
                        propietarios.nombre_comercial AS Propietario, 
                        producto.codigo, 
                        ISNULL(producto.codigo_barra, '') AS Barra, 
                        producto.nombre, ISNULL(unidad_medida.Nombre, '') AS UnidadMedida, 
                        ISNULL(producto_presentacion.nombre, '') AS Presentacion, 
                        ISNULL(stock.lote, '') AS lote, 
                        ISNULL(stock.fecha_ingreso, '19000101') AS Ingreso, 
                        ISNULL(stock.fecha_vence, '19000101') AS Vence, 
                        stock.cantidad AS Cantidad_UMBas, 
                        SUM(ISNULL(stock_res.cantidad, 0)) AS CantidadReservadaUmBas, 
                        stock.cantidad - ISNULL(SUM(stock_res.cantidad), 0) AS Disponible_UMBas, 
                        ISNULL(stock.peso, 0) AS peso, 
                        dbo.Nombre_Completo_Ubicacion(bodega_ubicacion.IdUbicacion, bodega_ubicacion.IdBodega) AS UbicacionCompleta,
                        producto_estado.dañado, 
                        producto_presentacion.factor, 
                        producto_estado.utilizable AS EstadoUtilizable, 
                        ISNULL(stock.IdUbicacion, 0) AS IdUbicacion,
                        ISNULL(stock.lic_plate, '') AS lic_plate,
                        ISNULL(stock.serial, '') AS serial,
                        ISNULL(stock.añada, 1900) AS añada, 
                        ISNULL(producto.IdIndiceRotacion, 0) AS IdIndiceRotacion, ISNULL(producto_presentacion.alto, 0) AS alto, ISNULL(producto_presentacion.largo, 0) AS largo, ISNULL(producto_presentacion.ancho, 0) AS ancho, 
                        bodega_ubicacion.IdTramo, bodega_ubicacion.ancho AS Ancho_ubicacion, bodega_ubicacion.largo AS Largo_ubicacion, bodega_ubicacion.alto AS Alto_ubicacion, 
                        indice_rotacion.Descripcion AS IndiceRotacion, producto.existencia_min AS Existencia_min_umbas, producto.existencia_max AS Existencia_max_umbas, producto.costo, 
                        producto_presentacion.MinimoExistencia AS Existencia_min_pres, producto_presentacion.MaximoExistencia AS Existencia_max_pres, ISNULL(stock.atributo_variante_1, '') AS atributo_variante_1, 
                        bodega_ubicacion.IdUbicacion AS IdUbicacionActual, bodega_ubicacion.nivel AS Ubicacion_Nivel, bodega_ubicacion.indice_x AS Ubicacion_Indice_X, bodega_ubicacion.descripcion AS Ubicacion_Nombre, 
                        bodega_tramo.descripcion AS Ubicacion_Tramo, ISNULL(motivo_devolucion.Nombre, 'N/A') AS MotivoDevolucion, ISNULL(trans_oc_pol.NoPoliza, 'N/D') AS NoPoliza, CASE WHEN (DATEDIFF(DAY, GETDATE(), 
                        stock.fecha_vence) >= (dbo.fdias_locales_by_IdCliente(@IdCliente, producto.IdFamilia, producto.IdClasificacion))) OR
                        (DATEDIFF(DAY, GETDATE(), stock.fecha_vence) >= (dbo.fdias_Exterior_by_IdCliente(@IdCliente, producto.IdFamilia, producto.IdClasificacion))) THEN 'Si' ELSE 'No' END AS Aplica, 
                        ISNULL(stock.cantidad / producto_presentacion.factor, 0) AS Cantidad_Presentacion, producto_estado.nombre AS NomEstado,producto.IdClasificacion, producto.IdFamilia,
                        ISNULL(dbo.fdias_Exterior_by_IdCliente(@IdCliente, producto.IdFamilia, producto.IdClasificacion),0) as Dias_Exterior,
                        ISNULL(dbo.fdias_locales_by_IdCliente(@IdCliente, producto.IdFamilia, producto.IdClasificacion),0) as Dias_Local,
                         trans_oc_enc.no_documento_recepcion_erp No_Contenedor
                    FROM  producto_bodega INNER JOIN
                    producto ON producto_bodega.IdProducto = producto.IdProducto RIGHT OUTER JOIN
                    unidad_medida INNER JOIN
                    propietarios INNER JOIN
                    stock INNER JOIN
                    propietario_bodega ON stock.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega ON propietarios.IdPropietario = propietario_bodega.IdPropietario ON 
                    unidad_medida.IdUnidadMedida = stock.IdUnidadMedida INNER JOIN
                    bodega_tramo INNER JOIN
                    bodega_ubicacion ON bodega_tramo.IdTramo = bodega_ubicacion.IdTramo AND bodega_tramo.IdArea = bodega_ubicacion.IdArea AND bodega_tramo.IdSector = bodega_ubicacion.IdSector AND 
                    bodega_tramo.IdBodega = bodega_ubicacion.IdBodega ON stock.idbodega = bodega_ubicacion.IdBodega AND stock.IdUbicacion = bodega_ubicacion.IdUbicacion ON 
                    producto_bodega.IdProductoBodega = stock.IdProductoBodega LEFT OUTER JOIN
                    trans_oc_pol RIGHT OUTER JOIN
                    trans_re_oc INNER JOIN
                    trans_oc_enc ON trans_re_oc.IdOrdenCompraEnc = trans_oc_enc.IdOrdenCompraEnc LEFT OUTER JOIN
                    motivo_devolucion ON trans_oc_enc.IdMotivoDevolucion = motivo_devolucion.IdMotivoDevolucion ON trans_oc_pol.IdOrdenCompraEnc = trans_oc_enc.IdOrdenCompraEnc ON 
                    stock.IdRecepcionEnc = trans_re_oc.IdRecepcionEnc LEFT OUTER JOIN
                    indice_rotacion ON producto.IdIndiceRotacion = indice_rotacion.IdIndiceRotacion LEFT OUTER JOIN
                    stock_res ON stock.IdStock = stock_res.IdStock LEFT OUTER JOIN
                    producto_estado ON stock.IdProductoEstado = producto_estado.IdEstado LEFT OUTER JOIN
                    producto_presentacion ON stock.IdPresentacion = producto_presentacion.IdPresentacion  and producto.IdPropietario = propietarios.IdPropietario
                    WHERE  
                    (stock.IdUbicacion NOT IN (SELECT IdUbicacion 
                                               FROM bodega_ubicacion AS bodega_ubicacion_1 
                                               WHERE ubicacion_despacho = 1
                                              )
                    )
                    AND stock.IdBodega=@IdBodega "


                If IdPropietarioBodega <> 0 Then
                    vSQL += " AND propietario_bodega.IdPropietarioBodega = @IdPropietarioBodega  "
                End If



                '#ejc20210923: agregar join con poliza antes de...
                'If NoPoliza <> "" Then
                '    vSQL += " AND (codigo_poliza= @NoPoliza OR numero_poliza = @NoPoliza) "
                'End If

                vSQL += "GROUP BY propietarios.nombre_comercial, propietarios.IdPropietario, stock.IdStock, bodega_ubicacion.IdUbicacion, stock.IdUbicacion_anterior, propietario_bodega.IdPropietarioBodega, 
                    producto_bodega.IdProductoBodega, unidad_medida.IdUnidadMedida, unidad_medida.Nombre, producto_presentacion.nombre, producto.IdProducto, producto.codigo, producto.nombre, 
                    stock.lote, stock.fecha_ingreso, stock.serial, stock.añada, producto_bodega.IdBodega, stock.fecha_vence, stock.IdProductoEstado, producto_estado.nombre, producto_estado.utilizable, 
                    producto_estado.dañado, stock.IdUbicacion, stock.IdPresentacion, stock.IdRecepcionEnc, stock.lic_plate, stock.peso, producto.IdIndiceRotacion, producto_presentacion.alto, 
                    producto_presentacion.largo, producto_presentacion.ancho, producto_presentacion.factor, bodega_ubicacion.IdTramo, bodega_ubicacion.ancho, bodega_ubicacion.largo, bodega_ubicacion.alto, 
                    indice_rotacion.Descripcion, producto.existencia_min, producto.existencia_max, producto.codigo_barra, producto.costo, producto_presentacion.MinimoExistencia, 
                    producto_presentacion.MaximoExistencia, stock.cantidad, stock.cantidad / producto_presentacion.factor, stock.atributo_variante_1, bodega_ubicacion.nivel, bodega_ubicacion.indice_x, 
                    bodega_ubicacion.descripcion, bodega_tramo.descripcion, bodega_ubicacion.orientacion_pos, motivo_devolucion.Nombre, trans_oc_pol.codigo_poliza, stock.idbodega, bodega_tramo.es_rack, 
                    bodega_ubicacion.IdBodega, producto.IdFamilia, producto.IdClasificacion,trans_oc_pol.NoPoliza,
                    trans_oc_enc.no_documento_recepcion_erp
                            HAVING (stock.cantidad - ISNULL(SUM(stock_res.cantidad), 0)) > 0 "

            Else

                'GT24022022: se agrega la clasificación para consolidadores con propietario referenciado en excel
                '#CKFK20221017 Modifiqué la vista VW_Stock_Resumen por la VW_Stock_Res
                vSQL = "SELECT st_resumen.IdBodega,
                               st_resumen.IdPropietario,
                               st_resumen.IdPropietarioBodega,
                               st_resumen.IdProducto,
                               st_resumen.IdProductoBodega,
                               st_resumen.IdStock,
                               st_resumen.IdUbicacion_anterior,
                               st_resumen.IdUnidadMedida,
                               st_resumen.IdProductoEstado,
                               st_resumen.IdPresentacion,
                               st_resumen.IdRecepcionEnc,
                               st_resumen.Propietario,
                               st_resumen.codigo as Codigo,
                               st_resumen.Codigo_barra as Barra,
                               st_resumen.nombre as Nombre,
                               st_resumen.UnidadMedida,
                               st_resumen.Presentacion,
                               st_resumen.lote as Lote,
                               st_resumen.fecha_ingreso as Ingreso,
                               st_resumen.fecha_Vence as Vence,
                               st_resumen.Cantidad_UMBas,
                               st_resumen.CantidadReservadaUmBas,
                               st_resumen.Disponible_UMBas,
                               st_resumen.peso as Peso,
                               st_resumen.Disponible_Presentacion as Cantidad_Presentacion,
                               st_resumen.NomEstado,
                               st_resumen.Area,
                               st_resumen.Nombre_Completo AS UbicacionCompleta,
                               st_resumen.dañado,
                               st_resumen.factor,
                               st_resumen.EstadoUtilizable,
                               st_resumen.IdUbicacion,
                               st_resumen.lic_plate as Licencia,
                               st_resumen.serial,
                               st_resumen.añada,
                               st_resumen.IdIndiceRotacion,
                               st_resumen.alto,
                               st_resumen.largo,
                               st_resumen.ancho,
                               st_resumen.IdTramo,
                               st_resumen.Ancho_ubicacion,
                               st_resumen.Largo_ubicacion,
                               st_resumen.Alto_ubicacion,
                               st_resumen.IndiceRotacion,
                               st_resumen.Existencia_min_umbas,
                               st_resumen.Existencia_max_umbas,
                               st_resumen.costo,
                               st_resumen.Existencia_min_pres,
                               st_resumen.Existencia_max_pres,
                               --st_resumen.atributo_variante_1,
                               st_resumen.parametro_personalizado,
                               st_resumen.parametro_valor,
                               st_resumen.IdUbicacionActual,
                               st_resumen.Ubicacion_Nivel,
                               st_resumen.Ubicacion_Indice_X,
                               st_resumen.Ubicacion_Nombre,
                               st_resumen.Ubicacion_Tramo,
                               st_resumen.MotivoDevolucion,
                               st_resumen.codigo_poliza,
                               st_resumen.numero_poliza,
                               st_resumen.NoTO,
                               st_resumen.ReferenciaOCEnc Referencia, 
                               'No' AS Aplica,
                               isnull(pr_clas.nombre,'ND') as clasificacion,
                               parametro_a,
                               parametro_b,
                               familia,
                               marca,
                               st_resumen.no_linea,
                               st_resumen.No_Contenedor "

                If Mostrar_Talla_Color Then
                    vSQL += ",st_resumen.Codigo_Talla,
							  st_resumen.Nombre_Talla,
							  st_resumen.Codigo_Color,
							  st_resumen.Nombre_Color "
                End If

                vSQL += " FROM VW_Stock_Res st_resumen
						 inner join producto pr on st_resumen.codigo = pr.codigo  and pr.IdPropietario = st_resumen.IdPropietario
						 left join producto_clasificacion pr_clas on pr.IdClasificacion = pr_clas.IdClasificacion
                         WHERE IdBodega=@IdBodega"

                If IdPropietarioBodega <> 0 Then
                    vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega and disponible_umbas > 0 "
                End If

                If NoPoliza <> "" Then
                    vSQL += " AND (codigo_poliza= @NoPoliza OR numero_poliza = @NoPoliza or NoTO = @NoPoliza) "
                End If

                If IdProductoEstadoDefault > 0 Then
                    vSQL += " AND IdProductoEstado=@IdProductoEstadoDefault "
                End If

                '#EJC20190311_0948PM: Excluir lo que esté en ubicaciones de tránsito.
                vSQL += " AND IdUbicacion NOT IN (SELECT IdUbicacion
					      FROM  bodega_ubicacion AS bodega_ubicacion 
						  WHERE (ubicacion_despacho = 1 and IdBodega=@IdBodega))"

                vSQL += " ORDER BY CODIGO, UbicacionCompleta ,Aplica, CantidadReservadaUmBas asc"

            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                If IdPropietarioBodega <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)
                End If

                If NoPoliza <> "" Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@NoPoliza", NoPoliza)
                End If

                If TieneTiempos Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", IdCliente)
                End If

                If IdProductoEstadoDefault > 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstadoDefault", IdProductoEstadoDefault)
                End If

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#CKFK 20181122_0511PM Creé esta función para poder listar el stock específico en la HandHeld
    Public Shared Function Get_All_Stock_Especifico_HH(ByVal IdBodega As Integer,
                                                     ByVal IdPedido As Integer,
                                                     ByVal pStockRes As clsBeStock_res) As DataTable

        Dim IdCliente As Integer = 0
        Dim TieneTiempos As Boolean = False
        Dim cadenaSQL As String = ""

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    IdCliente = clsLnTrans_pe_enc.GetIdCliente(IdPedido, lConnection, lTransaction)

                    TieneTiempos = clsLnCliente_tiempos.Cliente_Tiene_Tiempos(IdCliente, lConnection, lTransaction)

                    If TieneTiempos Then

                        cadenaSQL = "SELECT TOP(10) VW_Stock_Especifico.*,
						bodega_ubicacion.indice_x, 
						bodega_ubicacion.nivel, 
						bodega_ubicacion.orientacion_pos, 
						bodega_tramo.descripcion AS Nombre_Tramo 
						FROM VW_Stock_Especifico INNER JOIN
                         bodega_ubicacion ON VW_Stock_Especifico.IdUbicacion = bodega_ubicacion.IdUbicacion AND 
                         VW_Stock_Especifico.IdUbicacion = bodega_ubicacion.IdUbicacion INNER JOIN
                         bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo INNER JOIN
                         bodega ON VW_Stock_Especifico.IdBodega = bodega.IdBodega AND bodega_ubicacion.IdBodega = bodega.IdBodega AND 
                         bodega_tramo.IdBodega = bodega.IdBodega
						WHERE VW_Stock_Especifico.IdBodega=@IdBodega and IdCliente=@IdCliente and disponible_umbas > 0 AND
						VW_Stock_Especifico.idproductobodega=@idproductobodega                     
						and VW_Stock_Especifico.idunidadmedida =@idunidadmedida 
						and VW_Stock_Especifico.idproductoestado=@idproductoestado "

                        If pStockRes.IdPresentacion <> 0 Then
                            cadenaSQL += "and (VW_Stock_Especifico.idpresentacion is null or VW_Stock_Especifico.idpresentacion =@idpresentacion) "
                        End If

                    Else
                        '#CKFK20221017 Modifiqué la vista VW_Stock_Resumen por la VW_Stock_Res
                        cadenaSQL = "SELECT TOP(10) VW_Stock_Res.IdBodega, VW_Stock_Res.IdPropietario, VW_Stock_Res.IdPropietarioBodega, 
							VW_Stock_Res.IdProducto, VW_Stock_Res.IdProductoBodega, VW_Stock_Res.IdStock, 
						VW_Stock_Res.IdUbicacion_anterior, VW_Stock_Res.IdUnidadMedida, VW_Stock_Res.IdProductoEstado, 
						VW_Stock_Res.IdPresentacion, VW_Stock_Res.IdRecepcionEnc, VW_Stock_Res.Propietario, VW_Stock_Res.codigo, 
						VW_Stock_Res.codigo_barra Barra, VW_Stock_Res.nombre, VW_Stock_Res.UnidadMedida, VW_Stock_Res.Presentacion, VW_Stock_Res.lote, 
						VW_Stock_Res.Ingreso, ISNULl(VW_Stock_Res.Vence,'19000101') AS Vence, VW_Stock_Res.Cantidad_UMBas, 
						VW_Stock_Res.CantidadReservadaUmBas, 
						VW_Stock_Res.Disponible_UMBas, VW_Stock_Res.peso, VW_Stock_Res.Cantidad_Presentacion, VW_Stock_Res.NomEstado, 
						VW_Stock_Res.UbicacionCompleta, VW_Stock_Res.dañado, VW_Stock_Res.factor, VW_Stock_Res.EstadoUtilizable, 
						VW_Stock_Res.IdUbicacion, VW_Stock_Res.lic_plate, VW_Stock_Res.serial, VW_Stock_Res.añada, VW_Stock_Res.IdIndiceRotacion, 
						VW_Stock_Res.alto, VW_Stock_Res.largo, VW_Stock_Res.ancho, VW_Stock_Res.IdTramo, VW_Stock_Res.Ancho_ubicacion, 
						VW_Stock_Res.Largo_ubicacion, VW_Stock_Res.Alto_ubicacion, VW_Stock_Res.IndiceRotacion, 
						VW_Stock_Res.Existencia_min_umbas, VW_Stock_Res.Existencia_max_umbas, VW_Stock_Res.costo, VW_Stock_Res.Existencia_min_pres, 
						VW_Stock_Res.Existencia_max_pres, VW_Stock_Res.atributo_variante_1, VW_Stock_Res.IdUbicacionActual, 
						VW_Stock_Res.Ubicacion_Nivel, VW_Stock_Res.Ubicacion_Indice_X, VW_Stock_Res.Ubicacion_Nombre, VW_Stock_Res.Ubicacion_Tramo, '' AS MotivoDevolucion, 
						'' AS NoPoliza,'No'as Aplica,0 as IdClasificacion,0 as IdFamilia, 0 as Dias_Local,
						0 as Dias_Exterior,0 as IdCliente,
						bodega_ubicacion.indice_x, 
						bodega_ubicacion.nivel, 
						bodega_ubicacion.orientacion_pos, 
						bodega_tramo.descripcion AS Nombre_Tramo  
						FROM VW_Stock_Res INNER JOIN
                        bodega_ubicacion ON VW_Stock_Res.IdUbicacion = bodega_ubicacion.IdUbicacion AND 
                        VW_Stock_Res.IdUbicacion = bodega_ubicacion.IdUbicacion INNER JOIN
                        bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo INNER JOIN
                        bodega ON VW_Stock_Res.IdBodega = bodega.IdBodega AND bodega_ubicacion.IdBodega = bodega.IdBodega AND 
                        bodega_tramo.IdBodega = bodega.IdBodega
						WHERE VW_Stock_Res.IdBodega=@IdBodega and disponible_umbas > 0 AND
						VW_Stock_Res.idproductobodega=@idproductobodega                     
						and VW_Stock_Res.idunidadmedida =@idunidadmedida 
						and VW_Stock_Res.idproductoestado=@idproductoestado "

                        If pStockRes.IdPresentacion <> 0 Then
                            cadenaSQL += "and (VW_Stock_Res.idpresentacion is null or VW_Stock_Res.idpresentacion =@idpresentacion) "
                        End If

                    End If

                    Dim IdTipoRotacion As Integer = clsLnProducto.Get_Tipo_Rotacion_By_IdProductoBodega(pStockRes.IdProductoBodega, lConnection, lTransaction)

                    Select Case IdTipoRotacion

                        Case 1 'FIFO
                            cadenaSQL += " ORDER BY ingreso asc,nombre_tramo,indice_x,nivel,orientacion_pos,Cantidad_UMBas, Aplica desc,CantidadReservadaUmBas asc"
                        Case 2 'LIFO
                            cadenaSQL += " ORDER BY ingreso desc,nombre_tramo,indice_x,nivel,orientacion_pos,Cantidad_UMBas, Aplica desc,CantidadReservadaUmBas asc"
                        Case 3 'FEFO
                            cadenaSQL += " ORDER BY vence,nombre_tramo,indice_x,nivel,orientacion_pos,Cantidad_UMBas, Aplica desc,CantidadReservadaUmBas asc"
                        Case Else 'Default
                            cadenaSQL += " ORDER BY ingreso asc,nombre_tramo,indice_x,nivel,orientacion_pos, Aplica desc,CantidadReservadaUmBas asc"

                    End Select

                    Using lDTA As New SqlDataAdapter(cadenaSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pStockRes.IdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pStockRes.IdUnidadMedida)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pStockRes.IdProductoEstado)

                        If TieneTiempos Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", IdCliente)
                        End If

                        If pStockRes.IdPresentacion <> 0 Then
                            lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pStockRes.IdPresentacion)
                        End If

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable("Stock_Especifico")
                        lDTA.Fill(lDataTable)

                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, cadenaSQL))
        End Try

    End Function

    Public Shared Function Get_All_Stock_Especifico_HH(ByVal IdBodega As Integer,
                                                     ByVal IdPedido As Integer,
                                                     ByVal pStockRes As clsBeStock_res,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As DataTable

        Dim IdCliente As Integer = 0
        Dim TieneTiempos As Boolean = False
        Dim cadenaSQL As String = ""

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            IdCliente = clsLnTrans_pe_enc.GetIdCliente(IdPedido, lConnection, lTransaction)

            TieneTiempos = clsLnCliente_tiempos.Cliente_Tiene_Tiempos(IdCliente, lConnection, lTransaction)

            If TieneTiempos Then

                cadenaSQL = "SELECT TOP(10) VW_Stock_Especifico.*,
						bodega_ubicacion.indice_x, 
						bodega_ubicacion.nivel, 
						bodega_ubicacion.orientacion_pos, 
						bodega_tramo.descripcion AS Nombre_Tramo 
						FROM VW_Stock_Especifico INNER JOIN
                         bodega_ubicacion ON VW_Stock_Especifico.IdUbicacion = bodega_ubicacion.IdUbicacion AND 
                         VW_Stock_Especifico.IdUbicacion = bodega_ubicacion.IdUbicacion INNER JOIN
                         bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo INNER JOIN
                         bodega ON VW_Stock_Especifico.IdBodega = bodega.IdBodega AND bodega_ubicacion.IdBodega = bodega.IdBodega AND 
                         bodega_tramo.IdBodega = bodega.IdBodega
						WHERE VW_Stock_Especifico.IdBodega=@IdBodega and IdCliente=@IdCliente and disponible_umbas > 0 AND
						VW_Stock_Especifico.idproductobodega=@idproductobodega                     
						and VW_Stock_Especifico.idunidadmedida =@idunidadmedida 
						and VW_Stock_Especifico.idproductoestado=@idproductoestado "

                If pStockRes.IdPresentacion <> 0 Then
                    cadenaSQL += "and (VW_Stock_Especifico.idpresentacion is null or VW_Stock_Especifico.idpresentacion =@idpresentacion) "
                End If

            Else
                '#CKFK20221017 Modifiqué la vista VW_Stock_Resumen por la VW_Stock_Res
                cadenaSQL = "SELECT TOP(10) VW_Stock_Res.IdBodega, 
                                            VW_Stock_Res.IdPropietario, 
                                            VW_Stock_Res.IdPropietarioBodega, 
                                            VW_Stock_Res.IdProducto,  
                                            VW_Stock_Res.IdProductoBodega,  
                                            VW_Stock_Res.IdStock, 
                                            ISNULL(VW_Stock_Res.IdUbicacion_anterior,0) AS IdUbicacion_anterior,  
                                            VW_Stock_Res.IdUnidadMedida,  
                                            VW_Stock_Res.IdProductoEstado, 
                                            ISNULL(VW_Stock_Res.IdPresentacion,0) AS IdPresentacion,  
                                            ISNULL(VW_Stock_Res.IdRecepcionEnc,0) IdRecepcionEnc,  
                                            VW_Stock_Res.Propietario,  
                                            VW_Stock_Res.codigo,  
                                            ISNULL(VW_Stock_Res.codigo_barra,'') AS Barra,  
                                            VW_Stock_Res.nombre,  
                                            VW_Stock_Res.UnidadMedida,  
                                            ISNULL(VW_Stock_Res.Presentacion,'') AS Presentacion,  
                                            ISNULL(VW_Stock_Res.lote,'') AS Lote, 
                                            ISNULL(VW_Stock_Res.Ingreso,'19000101') AS Ingreso,  
                                            ISNULl(VW_Stock_Res.Vence,'19000101') AS Vence,  
                                            VW_Stock_Res.Cantidad_UMBas, 
                                            VW_Stock_Res.CantidadReservadaUmBas, 
                                            VW_Stock_Res.Disponible_UMBas,  
                                            VW_Stock_Res.peso,  
                                            ISNULl(VW_Stock_Res.Cantidad_Presentacion,0) AS Cantidad_Presentacion,  
                                            VW_Stock_Res.NomEstado,  
                                            VW_Stock_Res.UbicacionCompleta,  
                                            VW_Stock_Res.dañado,  
                                            VW_Stock_Res.factor,  
                                            VW_Stock_Res.EstadoUtilizable,  
                                            VW_Stock_Res.IdUbicacion,  
                                            ISNULL(VW_Stock_Res.lic_plate,'') lic_plate,  
                                            ISNULL(VW_Stock_Res.serial,'') serial,  
                                            VW_Stock_Res.añada,  
                                            VW_Stock_Res.IdIndiceRotacion,  
                                            VW_Stock_Res.alto,  
                                            VW_Stock_Res.largo,  
                                            VW_Stock_Res.ancho,  
                                            VW_Stock_Res.IdTramo,  
                                            VW_Stock_Res.Ancho_ubicacion,  
                                            VW_Stock_Res.Largo_ubicacion,  
                                            VW_Stock_Res.Alto_ubicacion,  
                                            VW_Stock_Res.IndiceRotacion, 
                                            VW_Stock_Res.Existencia_min_umbas,  
                                            VW_Stock_Res.Existencia_max_umbas,  
                                            VW_Stock_Res.costo,  
                                            VW_Stock_Res.Existencia_min_pres, 
                                            VW_Stock_Res.Existencia_max_pres,  
                                            VW_Stock_Res.atributo_variante_1,  
                                            VW_Stock_Res.IdUbicacionActual, 
                                            VW_Stock_Res.Ubicacion_Nivel,  
                                            VW_Stock_Res.Ubicacion_Indice_X,  
                                            VW_Stock_Res.Ubicacion_Nombre,  
                                            VW_Stock_Res.Ubicacion_Tramo,  
                                            '' AS MotivoDevolucion,  
                                            '' AS NoPoliza, 
                                            'No'as Aplica,0 as IdClasificacion, 
                                            0 as IdFamilia,  
                                            0 as Dias_Local, 
                                            0 as Dias_Exterior, 
                                            0 as IdCliente, 
                                            bodega_ubicacion.indice_x, 
                                            bodega_ubicacion.nivel, 
                                            bodega_ubicacion.orientacion_pos, 
                                            bodega_tramo.descripcion AS Nombre_Tramo  
						FROM VW_Stock_Res INNER JOIN
                        bodega_ubicacion ON VW_Stock_Res.IdUbicacion = bodega_ubicacion.IdUbicacion AND 
                        VW_Stock_Res.IdUbicacion = bodega_ubicacion.IdUbicacion INNER JOIN
                        bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo INNER JOIN
                        bodega ON VW_Stock_Res.IdBodega = bodega.IdBodega AND bodega_ubicacion.IdBodega = bodega.IdBodega AND 
                        bodega_tramo.IdBodega = bodega.IdBodega
						WHERE VW_Stock_Res.IdBodega=@IdBodega and disponible_umbas > 0 AND
						VW_Stock_Res.idproductobodega=@idproductobodega                     
						and VW_Stock_Res.idunidadmedida =@idunidadmedida 
						and VW_Stock_Res.idproductoestado=@idproductoestado "

                If pStockRes.IdPresentacion <> 0 Then
                    cadenaSQL += "and (VW_Stock_Res.idpresentacion is null or VW_Stock_Res.idpresentacion =@idpresentacion) "
                End If

            End If

            Dim IdTipoRotacion As Integer = clsLnProducto.Get_Tipo_Rotacion_By_IdProductoBodega(pStockRes.IdProductoBodega, lConnection, lTransaction)

            Select Case IdTipoRotacion

                Case 1 'FIFO
                    cadenaSQL += " ORDER BY ingreso asc,nombre_tramo,indice_x,nivel,orientacion_pos,Cantidad_UMBas, Aplica desc,CantidadReservadaUmBas asc"
                Case 2 'LIFO
                    cadenaSQL += " ORDER BY ingreso desc,nombre_tramo,indice_x,nivel,orientacion_pos,Cantidad_UMBas, Aplica desc,CantidadReservadaUmBas asc"
                Case 3 'FEFO
                    cadenaSQL += " ORDER BY vence,nombre_tramo,indice_x,nivel,orientacion_pos,Cantidad_UMBas, Aplica desc,CantidadReservadaUmBas asc"
                Case Else 'Default
                    cadenaSQL += " ORDER BY ingreso asc,nombre_tramo,indice_x,nivel,orientacion_pos, Aplica desc,CantidadReservadaUmBas asc"

            End Select

            Using lDTA As New SqlDataAdapter(cadenaSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pStockRes.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pStockRes.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pStockRes.IdProductoEstado)

                If TieneTiempos Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", IdCliente)
                End If

                If pStockRes.IdPresentacion <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pStockRes.IdPresentacion)
                End If

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                Dim lDataTable As New DataTable("Stock_Especifico")
                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, cadenaSQL))
        End Try

    End Function

    Public Shared Function Get_All_Stock_DT_By_IdBodega_And_IdPropietarioBodega(ByVal IdBodega As Integer,
                                                                                ByVal IdPropietarioBodega As Integer,
                                                                                ByVal pFechaDel As Date,
                                                                                ByVal pFechaAl As Date,
                                                                                ByVal ConFechas As Boolean,
                                                                                ByVal ConsolidaFechas As Boolean) As DataTable

        Dim vSQL As String = ""

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    If ConsolidaFechas Then

                        '#MECR27082025: Se agregaron campos de Talla y Color
                        vSQL = "SELECT codigo as Código,nombre as Producto,
							SUM(Disponible_UMBas) AS Disponible_UMBas,
							UnidadMedida,
							Presentacion as Presentación,
							NomEstado as Estado,
							Fecha_Ingreso,Fecha_Vence,IdUbicacion,
							Ubicacion_Tramo as Rack,
							Nombre_Completo as UbicacionCompleta,IdRecepcionEnc,
							MotivoDevolucion,codigo_poliza,numero_poliza,
							Referencia,No_Docto AS No_Docto_Rec,
                            lote as Lote,
                            Codigo_Talla as Talla,
                            Codigo_Color as Color   
							from VW_Stock_Res 
							WHERE IdBodega=@IdBodega and IdPropietarioBodega=@IdPropietarioBodega 
							and Disponible_UMBas > 0 "

                        If Not ConFechas Then
                            vSQL += String.Format(" AND cast(Fecha_Ingreso AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))
                        End If

                        vSQL += " group By Fecha_Vence,codigo,nombre,UnidadMedida,Presentacion,
							NomEstado,Fecha_Ingreso,IdUbicacion,Ubicacion_Tramo,Nombre_Completo,IdRecepcionEnc,
							MotivoDevolucion,codigo_poliza,numero_poliza, Referencia,No_Docto, lote, Codigo_Talla, Codigo_Color "


                    Else
                        '#CKFK20221017 Modifiqué la vista VW_Stock_Resumen por la VW_Stock_Res
                        '                  vSQL = "SELECT 
                        '                  Isnull( cast(movimiento as nvarchar(50)),'Error') IMovimiento,
                        '                  Isnull( cast(IdStock as nvarchar(50)),'Error') IdStock,
                        '                  codigo as Código,
                        'nombre as Producto,
                        'Disponible_UMBas,
                        'UnidadMedida,
                        'Presentacion as Presentación,
                        'NomEstado as Estado,
                        'Fecha_Ingreso,Fecha_Vence,IdUbicacion,
                        'Ubicacion_Tramo as Rack,
                        'Nombre_Completo as UbicacionCompleta,IdRecepcionEnc,
                        'MotivoDevolucion,codigo_poliza,numero_poliza,
                        '                  Referencia,No_Docto AS No_Docto_Rec
                        'from [VW_Stock_CLC] 
                        'WHERE IdBodega=@IdBodega and IdPropietarioBodega=@IdPropietarioBodega "

                        vSQL = "SELECT codigo as Código,
						nombre as Producto,
						Disponible_UMBas,
						UnidadMedida,
						Presentacion as Presentación,
						NomEstado as Estado,
						Fecha_Ingreso,Fecha_Vence,IdUbicacion,
						Ubicacion_Tramo as Rack,
						Nombre_Completo as UbicacionCompleta,IdRecepcionEnc,
						MotivoDevolucion,codigo_poliza,numero_poliza,
                        Referencia,No_Docto AS No_Docto_Rec
						from VW_Stock_Res 
						WHERE IdBodega=@IdBodega and IdPropietarioBodega=@IdPropietarioBodega 
						and disponible_umbas > 0 "

                        If Not ConFechas Then
                            vSQL += String.Format(" AND cast(Fecha_Ingreso AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))
                        End If

                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)

                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Stock_DT_By_IdBodega(ByVal IdBodega As Integer,
                                                        ByVal IdPropietarioBodega As Integer) As DataTable

        '#EJC20171112_0605PM:Agregué transacción

        Get_All_Stock_DT_By_IdBodega = Nothing

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()
            '#CKFK20221017 Modifiqué la vista VW_Stock_Resumen por la VW_Stock_Res
            'GT19042022: de momento, este método solo se usa en el rpt Existencias por Ubicación, por eso, se omite el disponible_umbas >0
            'Dim vSQL As String = "SELECT * FROM VW_Stock_Res WHERE IdBodega=@IdBodega and IdPropietarioBodega=@IdPropietarioBodega and disponible_umbas > 0"
            Dim vSQL As String = "SELECT *
                                  FROM VW_Stock_Res 
                                  WHERE IdBodega=@IdBodega and IdPropietarioBodega=@IdPropietarioBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)
                        lDTA.SelectCommand.CommandTimeout = 0
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_Stock_DT_By_IdBodega = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Licencias_Por_Ubicacion(ByVal IdBodega As Integer) As DataTable

        Get_Licencias_Por_Ubicacion = Nothing

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            Dim vSQL As String = "SELECT *
                                  FROM VW_Licencias_Por_Ubicacion 
                                  WHERE Bodega=@IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.CommandTimeout = 0
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_Licencias_Por_Ubicacion = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            watch.Stop()

            Debug.Print("Tiempo transcurrido Get_Licencias_Por_Ubicacion: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK20231005 Función para obtener licencias inconsistentes
    Public Shared Function Get_Licencias_Inconsistente_Por_Ubicacion(ByVal IdBodega As Integer) As DataTable

        Get_Licencias_Inconsistente_Por_Ubicacion = Nothing

        Try

            Dim vSQL As String = "SELECT *
                                  FROM VW_Detalle_Licencias_Inconsistentes 
                                  WHERE IdBodega=@IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.CommandTimeout = 0
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_Licencias_Inconsistente_Por_Ubicacion = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    '#GT16032022_0837: para el inventario en linea cealsa
    Public Shared Function Get_All_Stock_DT() As DataTable

        '#EJC20171112_0605PM:Agregué transacción
        Get_All_Stock_DT = Nothing

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()
            '#CKFK20221017 Modifiqué la vista VW_Stock_Resumen por la VW_Stock_Res
            '#GT16052024: para cealsa, se remueve el where porque aunque no haya disponible, se debe visualizar la linea reservada.
            'Dim vSQL As String = "SELECT * FROM VW_Stock_Res where disponible_umbas > 0"
            Dim vSQL As String = "SELECT * FROM VW_Stock_Res "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_Stock_DT = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Stock_Detalle() As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Const vSQL As String = " select * from VW_Stock_Res "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lrow In lDataTable.Rows
                                Obj = New clsBeVW_stock_res
                                clsLnVW_stock_res.Cargar(Obj, lrow, lConnection, lTransaction)
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

    Public Shared Function Get_All_Stock_Detalle_Valorizacion() As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "SELECT stock.IdStock, propietarios.nombre_comercial AS Propietario, producto_presentacion.nombre AS Presentacion, producto.codigo, producto.nombre, 
						 stock.fecha_ingreso, SUM(stock.cantidad) AS CantidadSF, stock.fecha_vence, stock_res.cantidad AS CantidadReservada, producto.costo, producto.IdProducto
						 FROM producto_estado RIGHT OUTER JOIN
						 producto_bodega INNER JOIN
						 producto ON producto_bodega.IdProducto = producto.IdProducto LEFT OUTER JOIN
						 indice_rotacion ON producto.IdIndiceRotacion = indice_rotacion.IdIndiceRotacion RIGHT OUTER JOIN
						 unidad_medida INNER JOIN
						 propietarios INNER JOIN
						 stock INNER JOIN
						 propietario_bodega ON stock.IdPropietarioBodega = propietario_bodega.IdPropietarioBodega ON propietarios.IdPropietario = propietario_bodega.IdPropietario ON 
						 unidad_medida.IdUnidadMedida = stock.IdUnidadMedida ON producto_bodega.IdProductoBodega = stock.IdProductoBodega LEFT OUTER JOIN
						 stock_res ON stock.IdStock = stock_res.IdStock AND stock.IdPropietarioBodega = stock_res.IdPropietarioBodega AND 
						 stock.IdProductoBodega = stock_res.IdProductoBodega AND stock.IdUbicacion = stock_res.IdUbicacion ON 
						 producto_estado.IdEstado = stock.IdProductoEstado LEFT OUTER JOIN
						 bodega_ubicacion ON stock.IdUbicacion = bodega_ubicacion.IdUbicacion LEFT OUTER JOIN
						 producto_presentacion ON stock.IdPresentacion = producto_presentacion.IdPresentacion
						 GROUP BY propietarios.nombre_comercial, stock.IdStock, producto_presentacion.nombre, producto.codigo, producto.nombre, stock.fecha_ingreso, stock.fecha_vence, 
						 stock_res.cantidad, producto.costo, producto.IdProducto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                    Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                                End If

                                If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                                    Obj.IdStock = CType(lRow("IdStock"), Integer)
                                End If

                                If lRow("codigo") IsNot DBNull.Value AndAlso lRow("codigo") IsNot Nothing Then
                                    Obj.Codigo_Producto = CType(lRow("codigo"), String)
                                End If

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    Obj.Propietario = CType(lRow("Propietario"), String)
                                End If

                                If lRow("nombre") IsNot DBNull.Value AndAlso lRow("nombre") IsNot Nothing Then
                                    Obj.Nombre_Producto = CType(lRow("nombre"), String)
                                End If

                                If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                    Obj.Nombre_Presentacion = CType(lRow("Presentacion"), String)
                                End If

                                If lRow("CantidadSF") IsNot DBNull.Value AndAlso lRow("CantidadSF") IsNot Nothing Then
                                    Obj.CantidadUmBas = CType(lRow("CantidadSF"), String)
                                End If

                                If lRow("CantidadReservada") IsNot DBNull.Value AndAlso lRow("CantidadReservada") IsNot Nothing Then
                                    Obj.Cantidad_Res = CType(lRow("CantidadReservada"), String)
                                End If

                                If lRow("fecha_ingreso") IsNot DBNull.Value AndAlso lRow("fecha_ingreso") IsNot Nothing Then
                                    Obj.Fecha_ingreso = CType(lRow("fecha_ingreso"), Date)
                                End If

                                If lRow("fecha_vence") IsNot DBNull.Value AndAlso lRow("fecha_vence") IsNot Nothing Then
                                    Obj.Fecha_Vence = CType(lRow("fecha_vence"), Date)
                                End If

                                If lRow("costo") IsNot DBNull.Value AndAlso lRow("costo") IsNot Nothing Then
                                    Obj.Costo = CType(lRow("costo"), String)
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

    Public Shared Function Get_All_Stock_Detalle_Parametros() As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "SELECT sp.IdStock AS StockId,sp.valor_texto AS ValorTexto, sp.valor_numerico AS ValorNumerico, 
                                  sp.valor_fecha AS ValorFecha, sp.valor_logico AS ValorLogico 
						          FROM stock_parametro AS sp INNER JOIN stock AS s ON sp.IdStock = s.IdStock "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                If lRow("StockId") IsNot DBNull.Value AndAlso lRow("StockId") IsNot Nothing Then
                                    Obj.IdStock = CType(lRow("StockId"), Integer)
                                End If

                                If lRow("ValorTexto") IsNot DBNull.Value AndAlso lRow("ValorTexto") IsNot Nothing Then
                                    Obj.ValorTexto = CType(lRow("ValorTexto"), String)
                                End If

                                If lRow("ValorNumerico") IsNot DBNull.Value AndAlso lRow("ValorNumerico") IsNot Nothing Then
                                    Obj.ValorNumerico = CType(lRow("ValorNumerico"), Double)
                                End If

                                If lRow("ValorFecha") IsNot DBNull.Value AndAlso lRow("ValorFecha") IsNot Nothing Then
                                    Obj.ValorFecha = CType(lRow("ValorFecha"), Date)
                                End If

                                If lRow("ValorLogico") IsNot DBNull.Value AndAlso lRow("ValorLogico") IsNot Nothing Then
                                    Obj.ValorLogico = CType(lRow("ValorLogico"), Boolean)
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

    Public Shared Function Get_All_Stock_Detalle_Resumen() As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "SELECT * FROM VW_STOCK_RES "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                    Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                                End If

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                                    Obj.IdStock = CType(lRow("IdStock"), Integer)
                                End If

                                If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                    Obj.Codigo_Producto = CType(lRow("Codigo"), String)
                                End If

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    Obj.Propietario = CType(lRow("Propietario"), String)
                                End If

                                If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                    Obj.Nombre_Producto = CType(lRow("Nombre"), String)
                                End If

                                If lRow("Codigo_Barra") IsNot DBNull.Value AndAlso lRow("Barra") IsNot Nothing Then
                                    Obj.Codigo_Barra = CType(lRow("Codigo_Barra"), String)
                                End If

                                If lRow("NomEstado") IsNot DBNull.Value AndAlso lRow("Estado") IsNot Nothing Then
                                    Obj.NomEstado = CType(lRow("NomEstado"), String)
                                End If

                                If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                    Obj.Nombre_Presentacion = CType(lRow("Presentacion"), String)
                                End If

                                If lRow("UnidadMedida") IsNot DBNull.Value AndAlso lRow("UnidadMedida") IsNot Nothing Then
                                    Obj.UMBas = CType(lRow("UnidadMedida"), String)
                                End If

                                If lRow("serial") IsNot DBNull.Value AndAlso lRow("serial") IsNot Nothing Then
                                    Obj.Serial = CType(lRow("serial"), String)
                                End If

                                If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                                    Obj.CantidadPresentacion = CType(lRow("Cantidad"), String)
                                End If

                                If lRow("CantidadSF") IsNot DBNull.Value AndAlso lRow("CantidadSF") IsNot Nothing Then
                                    Obj.CantidadUmBas = CType(lRow("CantidadSF"), String)
                                End If

                                If lRow("Fecha_Ingreso") IsNot DBNull.Value AndAlso lRow("Fecha_Ingreso") IsNot Nothing Then
                                    Obj.Fecha_ingreso = CType(lRow("Fecha_Ingreso"), Date)
                                End If

                                If lRow("Fecha_Vence") IsNot DBNull.Value AndAlso lRow("Fecha_Vence") IsNot Nothing Then
                                    Obj.Fecha_Vence = CType(lRow("Fecha_Vence"), Date)
                                End If

                                If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("lote"), String)
                                End If

                                If lRow("IdRecepcionEnc") IsNot DBNull.Value AndAlso lRow("IdRecepcionEnc") IsNot Nothing Then
                                    Obj.IdRecepcionEnc = CType(lRow("IdRecepcionEnc"), Integer)
                                End If

                                If lRow("IdUbicacionActual") IsNot DBNull.Value AndAlso lRow("IdUbicacionActual") IsNot Nothing Then
                                    Obj.IdUbicacion = CType(lRow("IdUbicacionActual"), Integer)
                                End If

                                If lRow("Ubicacion_Tramo") IsNot DBNull.Value AndAlso lRow("Ubicacion_Tramo") IsNot Nothing Then
                                    Obj.Ubicacion_Tramo = CType(lRow("Tramo"), String)
                                End If

                                If lRow("Nombre_Completo") IsNot DBNull.Value AndAlso lRow("Nombre_Completo") IsNot Nothing Then
                                    Obj.Ubicacion_Nombre = CType(lRow("Nombre_Completo"), String)
                                End If

                                If lRow("largo") IsNot DBNull.Value AndAlso lRow("largo") IsNot Nothing Then
                                    Obj.LargoUbicacion = CType(lRow("largo"), Integer)
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

    Public Shared Function Get_All_Stock_Detalle_Resumen(ByVal pIdProducto As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "SELECT * FROM VW_STOCK_RES WHERE IdProducto = @pIdProducto "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdProducto", pIdProducto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                    Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                                End If

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                                    Obj.IdStock = CType(lRow("IdStock"), Integer)
                                End If

                                If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                    Obj.Codigo_Producto = CType(lRow("Codigo"), String)
                                End If

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    Obj.Propietario = CType(lRow("Propietario"), String)
                                End If

                                If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                    Obj.Nombre_Producto = CType(lRow("Nombre"), String)
                                End If

                                If lRow("Codigo_Barra") IsNot DBNull.Value AndAlso lRow("Codigo_Barra") IsNot Nothing Then
                                    Obj.Codigo_Barra = CType(lRow("Codigo_Barra"), String)
                                End If

                                If lRow("NomEstado") IsNot DBNull.Value AndAlso lRow("NomEstado") IsNot Nothing Then
                                    Obj.NomEstado = CType(lRow("NomEstado"), String)
                                End If

                                If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                    Obj.Nombre_Presentacion = CType(lRow("Presentacion"), String)
                                End If

                                If lRow("UnidadMedida") IsNot DBNull.Value AndAlso lRow("UnidadMedida") IsNot Nothing Then
                                    Obj.UMBas = CType(lRow("UnidadMedida"), String)
                                End If

                                If lRow("serial") IsNot DBNull.Value AndAlso lRow("serial") IsNot Nothing Then
                                    Obj.Serial = CType(lRow("serial"), String)
                                End If

                                If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                                    Obj.CantidadPresentacion = CType(lRow("Cantidad"), String)
                                End If

                                If lRow("CantidadSF") IsNot DBNull.Value AndAlso lRow("CantidadSF") IsNot Nothing Then
                                    Obj.CantidadUmBas = CType(lRow("CantidadSF"), String)
                                End If

                                If lRow("Fecha_Ingreso") IsNot DBNull.Value AndAlso lRow("Fecha_Ingreso") IsNot Nothing Then
                                    Obj.Fecha_ingreso = CType(lRow("Fecha_Ingreso"), Date)
                                End If

                                If lRow("Fecha_Vence") IsNot DBNull.Value AndAlso lRow("Fecha_Vence") IsNot Nothing Then
                                    Obj.Fecha_Vence = CType(lRow("Fecha_Vence"), Date)
                                End If

                                If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("lote"), String)
                                End If

                                If lRow("IdRecepcionEnc") IsNot DBNull.Value AndAlso lRow("IdRecepcionEnc") IsNot Nothing Then
                                    Obj.IdRecepcionEnc = CType(lRow("IdRecepcionEnc"), Integer)
                                End If

                                If lRow("IdUbicacionActual") IsNot DBNull.Value AndAlso lRow("IdUbicacionActual") IsNot Nothing Then
                                    Obj.IdUbicacion = CType(lRow("IdUbicacionActual"), Integer)
                                End If

                                If lRow("Ubicacion_Tramo") IsNot DBNull.Value AndAlso lRow("Ubicacion_Tramo") IsNot Nothing Then
                                    Obj.Ubicacion_Tramo = CType(lRow("Ubicacion_Tramo"), String)
                                End If

                                If lRow("Nombre_Completo") IsNot DBNull.Value AndAlso lRow("Nombre_Completo") IsNot Nothing Then
                                    Obj.Ubicacion_Nombre = CType(lRow("Nombre_Completo"), String)
                                End If

                                If lRow("largo") IsNot DBNull.Value AndAlso lRow("largo") IsNot Nothing Then
                                    Obj.LargoUbicacion = CType(lRow("largo"), Integer)
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

    Public Shared Function Actualizar_Fecha_Vencimiento(ByRef oBeTrans_inv_stock As clsBeStock, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("stock")
            Upd.Add("fecha_vence", "@fecha_vence", DataType.Parametro)
            Upd.Where("IdStock = @IdStock")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", oBeTrans_inv_stock.IdStock))
            cmd.Parameters.Add(New SqlParameter("@FECHA_VENCE", oBeTrans_inv_stock.Fecha_vence))


            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Stock_Detalle_Series() As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "SELECT se.IdStock AS StockId,se.NoSerie AS No_Serie,
						se.NoSerieInicial AS No_Serie_Inicial,
						se.NoSerieFinal AS No_Serie_Final 
						FROM stock_se AS se"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                If lRow("StockId") IsNot DBNull.Value AndAlso lRow("StockId") IsNot Nothing Then
                                    Obj.IdStock = CType(lRow("StockId"), Integer)
                                End If

                                If lRow("No_Serie") IsNot DBNull.Value AndAlso lRow("No_Serie") IsNot Nothing Then
                                    Obj.No_Serie = CType(lRow("No_Serie"), String)
                                End If

                                If lRow("No_Serie_Inicial") IsNot DBNull.Value AndAlso lRow("No_Serie_Inicial") IsNot Nothing Then
                                    Obj.No_Serie_Inicial = CType(lRow("No_Serie_Inicial"), String)
                                End If

                                If lRow("No_Serie_Final") IsNot DBNull.Value AndAlso lRow("No_Serie_Final") IsNot Nothing Then
                                    Obj.No_Serie_Final = CType(lRow("No_Serie_Final"), String)
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

    Public Shared Function Get_All_Stock_Detalle_Series(ByVal pIdBodega As Integer,
                                                        ByVal pIdPropietarioBodega As Integer,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As DataTable

        Get_All_Stock_Detalle_Series = Nothing

        Try

            Dim vSQL As String = "SELECT se.IdStock AS StockId,se.NoSerie AS No_Serie,
						          se.NoSerieInicial AS No_Serie_Inicial,
						          se.NoSerieFinal AS No_Serie_Final 
						          FROM stock_se AS se inner join
                                  producto_bodega pb on 
                                  se.IdProductoBodega = pb.IdProductoBodega
                                  inner join propietario_bodega ppb on 
                                  ppb.IdBodega = pb.IdBodega
                                  WHERE pb.IdBodega = @IdBodega AND ppb.IdPropietarioBodega = @IdPropietarioBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Get_All_Stock_Detalle_Series = lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Stock_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "Select * from VW_Stock_Res WHERE IdProducto=@IdProducto"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res
                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
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

    ''' <summary>
    ''' Función para consultar las existencias de un producto a partir de la tabla stock
    ''' </summary>
    ''' <param name="pIdProductoBodega">Se deben llenar los valores IdProductoBodega, IdProductoEstado, IdUnidadMedida, IdPresentacion(Opcional)</param>
    ''' <returns>Devuelve una lista de clsBESTock a partir de los parámetros enviados en un objeto de tipo clsBEStock</returns>
    ''' <remarks>ejcalderon_20160512</remarks>
    ''' 
    Public Shared Function Get_Lista_Existencias_By_IdProductoBodega(ByVal pIdProductoBodega As Integer) As List(Of clsBeStock)

        Try

            Dim oStock As clsBeStock

            Dim ListStock As New List(Of clsBeStock)

            Dim vSQL As String = "SELECT stock.*  " &
                       " FROM stock INNER JOIN " &
                       " producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega " &
                       " WHERE stock.IdProductoBodega = @IdProductoBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows
                                oStock = New clsBeStock
                                Cargar(oStock, lRow)
                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return ListStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lista_Existencias_By_IdProductoBodega(ByRef pProducto As clsBeStock,
                                                                     ByVal pIdBodega As Integer,
                                                                     ByVal pConnection As SqlConnection,
                                                                     ByVal pTransaction As SqlTransaction,
                                                                     Optional ByVal ConEstado As Boolean = True,
                                                                     Optional ByVal ConLote As Boolean = False) As List(Of clsBeStock)

        Try

            Dim oStock As clsBeStock

            Dim ListStock As New List(Of clsBeStock)

            Dim SQL As String = "SELECT stock.*  
				 FROM stock INNER JOIN 
				 producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega 
				 WHERE stock.IdProductoBodega = @IdProductoBodega 
				 AND stock.idunidadmedida =@idunidadmedida "

            If ConEstado Then
                SQL += " and stock.idproductoestado=@idproductoestado "
            End If

            '#EJC20180618: Colate para distinguir minúsculas y mayúsuculas en comparación
            If ConLote Then
                'SQL += " and (stock.lote is null or stock.lote =@lote COLLATE Latin1_General_CS_AS) "
                SQL += " and (isnull(stock.lote,'') = @lote COLLATE Latin1_General_CS_AS) "
            End If

            '#EJC20190311_0948PM: Excluir lo que esté en ubicaciones de tránsito.
            SQL += " and stock.idubicacion NOT IN (SELECT IdUbicacion
							   FROM  bodega_ubicacion AS bodega_ubicacion 
								WHERE (ubicacion_despacho = 1 and IdBodega=@IdBodega))"

            Using lDTA As New SqlDataAdapter(SQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pProducto.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                If ConEstado Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pProducto.ProductoEstado.IdEstado)
                End If

                If ConLote Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pProducto.Lote)
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        oStock = New clsBeStock
                        Cargar(oStock, lRow)
                        ListStock.Add(oStock)
                    Next

                End If

            End Using

            Return ListStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lista_Existencias_By_IdProductoBodega_And_DiasVencimiento_Picking(ByRef pProducto As clsBeStock,
                                                                                                 ByVal pIdBodega As Integer,
                                                                                                 ByVal DiasVencimientoCliente As Double,
                                                                                                 ByVal pConnection As SqlConnection,
                                                                                                 ByVal pTransaction As SqlTransaction,
                                                                                                 ByVal pExcluirUbicacionPicking As Boolean,
                                                                                                 Optional ByVal ConEstado As Boolean = True,
                                                                                                 Optional ByVal ConLote As Boolean = False) As List(Of clsBeStock)

        Try

            Dim oStock As clsBeStock

            Dim ListStock As New List(Of clsBeStock)

            Dim SQL As String = "SELECT stock.*  
				 FROM stock INNER JOIN 
				 producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega 
				 WHERE stock.IdProductoBodega = @IdProductoBodega 
				 AND stock.idunidadmedida =@idunidadmedida "

            If ConEstado Then
                SQL += " and stock.idproductoestado=@idproductoestado "
            End If

            If DiasVencimientoCliente > 0 Then
                SQL += " and DATEDIFF (DAY,GETDATE(),stock.fecha_vence) >=@DiasVencimientoCliente "
            End If

            '#EJC20180618: Colate para distinguir minúsculas y mayúsuculas en comparación
            If ConLote Then
                'SQL += " and (stock.lote is null or stock.lote =@lote COLLATE Latin1_General_CS_AS) "
                SQL += " and (isnull(stock.lote,'') = @lote COLLATE Latin1_General_CS_AS) "
            End If

            If pProducto.IdPresentacion <> 0 Then
                SQL += " and stock.idpresentacion=@idpresentacion"
            Else
                SQL += "and (isnull(stock.idpresentacion,0) = 0) "
            End If

            If pProducto.IsReportStockEnFecha Then
                SQL += " AND stock.fecha_vence = @fecha_vence "
            End If

            '#EJC20190311_0948PM: Excluir lo que esté en ubicaciones de tránsito.
            SQL += " and stock.idubicacion NOT IN (SELECT IdUbicacion
							                        FROM  bodega_ubicacion AS bodega_ubicacion 
								                    WHERE (ubicacion_despacho = 1 and IdBodega=@IdBodega))"

            If pExcluirUbicacionPicking Then

                '#EJC20190311_0948PM: Excluir lo que esté en ubicaciones de tránsito.
                SQL += " and stock.idubicacion NOT IN (SELECT IdUbicacion
							                        FROM  bodega_ubicacion AS bodega_ubicacion 
								                    WHERE (ubicacion_picking = 1 and IdBodega=@IdBodega))"

            End If


            Using lDTA As New SqlDataAdapter(SQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pProducto.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                If DiasVencimientoCliente > 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@DiasVencimientoCliente", DiasVencimientoCliente)
                End If

                If ConEstado Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pProducto.ProductoEstado.IdEstado)
                End If

                If ConLote Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pProducto.Lote)
                End If

                If pProducto.IdPresentacion <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@idpresentacion", pProducto.IdPresentacion)
                End If

                If pProducto.IsReportStockEnFecha Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@fecha_vence", pProducto.Fecha_vence)
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        oStock = New clsBeStock
                        Cargar(oStock, lRow)
                        ListStock.Add(oStock)
                    Next

                End If

            End Using

            Return ListStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lista_Existencias_By_IdProductoBodega_And_DiasVencimiento(ByRef pProducto As clsBeStock,
                                                                                         ByVal pIdBodega As Integer,
                                                                                         ByVal DiasVencimientoCliente As Double,
                                                                                         ByVal pConnection As SqlConnection,
                                                                                         ByVal pTransaction As SqlTransaction,
                                                                                         ByVal pExcluirUbicacionPicking As Boolean,
                                                                                         Optional ByVal ConEstado As Boolean = True,
                                                                                         Optional ByVal ConLote As Boolean = False,
                                                                                         Optional ByVal pIdUbicacion As Integer = 0) As List(Of clsBeStock)

        Try

            Dim oStock As clsBeStock
            Dim vManejaTallaColor As Boolean = False
            Dim ListStock As New List(Of clsBeStock)

            Dim SQL As String = "SELECT stock.*  
				                 FROM stock INNER JOIN 
				                 producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega 
				                 WHERE stock.IdProductoBodega = @IdProductoBodega 
				                 AND stock.idunidadmedida =@idunidadmedida "

            If ConEstado Then
                SQL += " and stock.idproductoestado=@idproductoestado "
            End If

            If DiasVencimientoCliente > 0 Then
                SQL += " and DATEDIFF (DAY,GETDATE(),stock.fecha_vence) >=@DiasVencimientoCliente "
            End If

            '#EJC20180618: Colate para distinguir minúsculas y mayúsuculas en comparación
            If ConLote Then
                'SQL += " and (stock.lote is null or stock.lote =@lote COLLATE Latin1_General_CS_AS) "
                SQL += " and (isnull(stock.lote,'') = @lote COLLATE Latin1_General_CS_AS) "
            End If

            If pProducto.IdPresentacion <> 0 Then
                SQL += " and stock.idpresentacion=@idpresentacion"
            Else
                SQL += "and (isnull(stock.idpresentacion,0) = 0) "
            End If

            If pProducto.IsReportStockEnFecha Then
                SQL += " AND stock.fecha_vence = @fecha_vence "
            End If

            If pProducto.IdUbicacion <> 0 Then
                SQL += " AND stock.IdUbicacion = @IdUbicacionAbastecerCon"
            Else

                '#EJC20190311_0948PM: Excluir lo que esté en ubicaciones de tránsito.
                SQL += " and stock.idubicacion NOT IN (SELECT IdUbicacion
							                        FROM  bodega_ubicacion AS bodega_ubicacion 
								                    WHERE (ubicacion_despacho = 1 and IdBodega=@IdBodega))"
            End If

            If pIdUbicacion = 0 Then

                If pExcluirUbicacionPicking Then

                    '#EJC20190311_0948PM: Excluir lo que esté en ubicaciones de picking.
                    SQL += " and stock.idubicacion NOT IN (SELECT IdUbicacion
							                        FROM  bodega_ubicacion AS bodega_ubicacion 
								                    WHERE (ubicacion_picking = 1 and IdBodega=@IdBodega))"

                End If

            End If

            If pIdUbicacion <> 0 Then

                '#EJC20190311_0948PM: Excluir lo que esté en ubicaciones de tránsito.
                SQL += " and stock.idubicacion = @IdUbicacion"

            End If

            vManejaTallaColor = clsLnBodega.Get_Maneja_Talla_Color_By_IdBodega(pIdBodega, pConnection, pTransaction)

            If vManejaTallaColor Then
                SQL += " and stock.IdProductoTallaColor = @IdProductoTallaColor  "
            End If

            Using lDTA As New SqlDataAdapter(SQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pProducto.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                '#EJC20190311_0948PM: Excluir lo que esté en ubicaciones de tránsito.
                If pProducto.IdUbicacion <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacionAbastecerCon", pProducto.IdUbicacion)
                End If

                If DiasVencimientoCliente > 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@DiasVencimientoCliente", DiasVencimientoCliente)
                End If

                If ConEstado Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pProducto.ProductoEstado.IdEstado)
                End If

                If ConLote Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pProducto.Lote)
                End If

                If pProducto.IdPresentacion <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@idpresentacion", pProducto.IdPresentacion)
                End If

                If pProducto.IsReportStockEnFecha Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@fecha_vence", pProducto.Fecha_vence)
                End If

                If pIdUbicacion <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                End If

                If vManejaTallaColor Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoTallaColor", pProducto.IdProductoTallaColor)
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim CheckExistencias As Boolean = True

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    If CheckExistencias Then

                        For Each lRow As DataRow In lDataTable.Rows
                            oStock = New clsBeStock
                            Cargar(oStock, lRow)
                            ListStock.Add(oStock)
                        Next

                    End If

                End If

            End Using

            Return ListStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lista_Existencias_By_IdStock(ByRef pProducto As clsBeStock,
                                                            ByVal pConnection As SqlConnection,
                                                            ByVal pTransaction As SqlTransaction) As List(Of clsBeStock)

        Try

            Dim oStock As clsBeStock

            Dim ListStock As New List(Of clsBeStock)

            Dim SQL As String = "SELECT stock.*  
				 FROM stock INNER JOIN 
				 producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega 
				 WHERE stock.IdProductoBodega = @IdProductoBodega 
				 AND stock.IdUnidadmedida =@idunidadmedida
				 AND stock.IdStock = @IdStock"

            Using lDTA As New SqlDataAdapter(SQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pProducto.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", pProducto.IdStock)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        oStock = New clsBeStock
                        Cargar(oStock, lRow)
                        ListStock.Add(oStock)
                    Next

                End If

            End Using

            Return ListStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lista_Existencias_By_IdStock_And_DiasVencimiento(ByRef pProducto As clsBeStock,
                                                                                ByVal DiasVencimientoCliente As Double,
                                                                                ByVal pConnection As SqlConnection,
                                                                                ByVal pTransaction As SqlTransaction) As List(Of clsBeStock)

        Try

            Dim oStock As clsBeStock
            Dim vManejaTallaColor As Boolean = False
            Dim ListStock As New List(Of clsBeStock)

            Dim vSQL As String = "SELECT stock.*  
				                 FROM stock INNER JOIN 
				                 producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega 
				                 WHERE stock.IdProductoBodega = @IdProductoBodega 
				                 AND stock.IdUnidadmedida =@idunidadmedida
				                 AND stock.IdStock = @IdStock "

            If DiasVencimientoCliente > 0 Then
                vSQL += " and DATEDIFF (DAY,GETDATE(),stock.fecha_vence) >=@DiasVencimientoCliente"
            End If

            If pProducto.IsReportStockEnFecha Then
                vSQL += " AND stock.fecha_vence = @fecha_vence "
            End If

            '#EJC20190311_0948PM: Excluir lo que esté en ubicaciones de tránsito.
            If pProducto.IdUbicacion <> 0 Then
                vSQL += " AND stock.IdUbicacion = @IdUbicacionAbastecerCon "
            End If

            vManejaTallaColor = clsLnBodega.Get_Maneja_Talla_Color_By_IdBodega(pProducto.IdBodega, pConnection, pTransaction)

            If vManejaTallaColor Then
                vSQL += " and stock.IdProductoTallaColor = @IdProductoTallaColor  "
            End If

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pProducto.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", pProducto.IdStock)

                If DiasVencimientoCliente > 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@DiasVencimientoCliente", DiasVencimientoCliente)
                End If

                If pProducto.IsReportStockEnFecha Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@fecha_vence", pProducto.Fecha_vence)
                End If

                '#EJC20190311_0948PM: Excluir lo que esté en ubicaciones de tránsito.
                If pProducto.IdUbicacion <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pProducto.IdUbicacion)
                End If

                If vManejaTallaColor Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoTallaColor", pProducto.IdProductoTallaColor)
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        oStock = New clsBeStock
                        Cargar(oStock, lRow)
                        ListStock.Add(oStock)
                    Next

                End If

            End Using

            Return ListStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Cantidad_Reservada_By_IdPedidoDet(ByRef pProducto As clsBeStock,
                                                                ByVal IdPedidoDet As Integer,
                                                                ByRef pConnection As SqlConnection,
                                                                ByRef pTransaction As SqlTransaction,
                                                                Optional ByVal ConEstado As Boolean = True,
                                                                Optional ByVal ConLote As Boolean = True) As Double

        Get_Cantidad_Reservada_By_IdPedidoDet = 0

        Try

            Dim vSQL As String = " select sum(stock_res.cantidad) as cantidad 
					from stock_res inner join  
					producto_bodega on stock_res.idproductobodega = producto_bodega.idproductobodega  
					where producto_bodega.idproductobodega=@idproducto 
					and (stock_res.idpresentacion is null or stock_res.idpresentacion =@idpresentacion) 
					and stock_res.idunidadmedida =@idunidadmedida 
					and IdPedidoDet = @IdPedidoDet "

            If ConEstado Then
                vSQL += " and stock_res.idproductoestado=@idproductoestado "
            End If

            If ConLote Then
                vSQL += " and (stock_res.lote is null or stock_res.lote =@lote)"
            End If

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdProducto", pProducto.IdProductoBodega)

                If Not pProducto.Presentacion Is Nothing Then

                    If pProducto.Presentacion.IdPresentacion <> 0 Then
                        lCommand.Parameters.AddWithValue("@IdPresentacion", pProducto.Presentacion.IdPresentacion)
                    Else
                        lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                    End If
                Else
                    lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                End If

                lCommand.Parameters.AddWithValue("@IdPedidoDet", IdPedidoDet)
                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)

                If ConEstado Then
                    lCommand.Parameters.AddWithValue("@IdProductoEstado", pProducto.ProductoEstado.IdEstado)
                End If

                If ConLote Then
                    lCommand.Parameters.AddWithValue("@Lote", pProducto.Lote)
                End If

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_Cantidad_Reservada_By_IdPedidoDet = lReturnValue
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Cantidad_Reservada_By_IdPedidoDet(ByRef pProducto As clsBeStock,
                                                                 ByVal IdPedidoDet As Integer,
                                                                 Optional ByVal ConEstado As Boolean = True,
                                                                 Optional ByVal ConLote As Boolean = True) As Double

        Get_Cantidad_Reservada_By_IdPedidoDet = 0

        Try

            Dim vSQL As String = " select sum(stock_res.cantidad) as cantidad 
					from stock_res inner join  
					producto_bodega on stock_res.idproductobodega = producto_bodega.idproductobodega  
					where producto_bodega.idproductobodega=@idproducto 
					and (stock_res.idpresentacion is null or stock_res.idpresentacion =@idpresentacion) 
					and stock_res.idunidadmedida =@idunidadmedida 
					and IdPedidoDet = @IdPedidoDet "

            If ConEstado Then
                vSQL += " and stock_res.idproductoestado=@idproductoestado "
            End If

            If ConLote Then
                vSQL += " and (stock_res.lote is null or stock_res.lote =@lote)"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdProducto", pProducto.IdProductoBodega)

                        If Not pProducto.Presentacion Is Nothing Then

                            If pProducto.Presentacion.IdPresentacion <> 0 Then
                                lCommand.Parameters.AddWithValue("@IdPresentacion", pProducto.Presentacion.IdPresentacion)
                            Else
                                lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                            End If
                        Else
                            lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                        End If

                        lCommand.Parameters.AddWithValue("@IdPedidoDet", IdPedidoDet)
                        lCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)

                        If ConEstado Then
                            lCommand.Parameters.AddWithValue("@IdProductoEstado", pProducto.ProductoEstado.IdEstado)
                        End If

                        If ConLote Then
                            lCommand.Parameters.AddWithValue("@Lote", pProducto.Lote)
                        End If

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_Cantidad_Reservada_By_IdPedidoDet = lReturnValue
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


    '#CKFK 20210203 Cree esta función para obtener la cantidad de un producto en una ubicacion
    Public Shared Function Get_Cant_By_IdUbicacion_By_IdProd(ByVal pIdUbicacion As Integer,
                                                             ByVal pIdProductoBodega As Integer,
                                                             ByVal pIdBodega As Integer) As Double

        Get_Cant_By_IdUbicacion_By_IdProd = 0


        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT ISNULL(SUM(cantidad),0) AS CANT FROM VW_Stock_Res WHERE 1 = 1 "
                    If pIdUbicacion <> 0 Then vSQL &= "AND IdUbicacionActual = @pIdUbicacion "
                    If pIdProductoBodega <> 0 Then vSQL &= "AND IdProductoBodega = @pIdProductoBodega "
                    If pIdBodega <> 0 Then vSQL &= "AND IdBodega = @pIdBodega "

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@pIdUbicacion", pIdUbicacion)
                        lCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)
                        lCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_Cant_By_IdUbicacion_By_IdProd = lReturnValue
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

    Public Shared Function Get_Cant_By_IdUbicacion_By_IdProd(ByVal pIdUbicacion As Integer,
                                                             ByVal pIdProductoBodega As Integer,
                                                             ByVal pIdBodega As Integer,
                                                             ByVal pConnection As SqlConnection,
                                                             ByVal pTransaction As SqlTransaction) As Double

        Get_Cant_By_IdUbicacion_By_IdProd = 0


        Try

            Dim vSQL As String = "SELECT ISNULL(SUM(cantidad),0) AS CANT FROM VW_Stock_Res WHERE 1 = 1 "
            If pIdUbicacion <> 0 Then vSQL &= "AND IdUbicacionActual = @pIdUbicacion "
            If pIdProductoBodega <> 0 Then vSQL &= "AND IdProductoBodega = @pIdProductoBodega "
            If pIdBodega <> 0 Then vSQL &= "AND IdBodega = @pIdBodega "

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@pIdUbicacion", pIdUbicacion)
                lCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)
                lCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_Cant_By_IdUbicacion_By_IdProd = lReturnValue
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Cant_UMBAS_By_IdUbicacion_By_IdProd(ByVal pIdUbicacion As Integer,
                                                                   ByVal pIdProductoBodega As Integer,
                                                                   ByVal pIdBodega As Integer) As Double

        Get_Cant_UMBAS_By_IdUbicacion_By_IdProd = 0


        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT ISNULL(SUM(cantidadSF),0) AS CANT FROM VW_Stock_Res WHERE 1 = 1 "
                    If pIdUbicacion <> 0 Then vSQL &= "AND IdUbicacionActual = @pIdUbicacion "
                    If pIdProductoBodega <> 0 Then vSQL &= "AND IdProductoBodega = @pIdProductoBodega "
                    If pIdBodega <> 0 Then vSQL &= "AND IdBodega = @pIdBodega "

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@pIdUbicacion", pIdUbicacion)
                        lCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)
                        lCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_Cant_UMBAS_By_IdUbicacion_By_IdProd = lReturnValue
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

    Public Shared Function Get_Cant_UMBAS_By_IdUbicacion_By_IdProd(ByVal pIdUbicacion As Integer,
                                                                   ByVal pIdProductoBodega As Integer,
                                                                   ByVal pIdBodega As Integer,
                                                                   ByVal pConnection As SqlConnection,
                                                                   ByVal pTransaction As SqlTransaction) As Double

        Get_Cant_UMBAS_By_IdUbicacion_By_IdProd = 0


        Try

            Dim vSQL As String = "SELECT ISNULL(SUM(cantidadSF),0) AS CANT FROM VW_Stock_Res WHERE 1 = 1 "
            If pIdUbicacion <> 0 Then vSQL &= "AND IdUbicacionActual = @pIdUbicacion "
            If pIdProductoBodega <> 0 Then vSQL &= "AND IdProductoBodega = @pIdProductoBodega "
            If pIdBodega <> 0 Then vSQL &= "AND IdBodega = @pIdBodega "

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@pIdUbicacion", pIdUbicacion)
                lCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)
                lCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_Cant_UMBAS_By_IdUbicacion_By_IdProd = lReturnValue
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Public Shared Function Get_Peso_Reservado(ByRef pProducto As clsBeStock,
                                              ByVal IdPedidoDet As Integer,
                                              ByVal pConnection As SqlConnection,
                                              ByVal pTransaction As SqlTransaction,
                                              Optional ByVal ConEstado As Boolean = True) As Double

        Get_Peso_Reservado = 0

        Try

            Dim vSQL As String = " select sum(stock_res.peso) as peso 
			                       from stock_res inner join  
			                       producto_bodega on stock_res.idproductobodega = producto_bodega.idproductobodega  
			                       where producto_bodega.idproductobodega=@idproducto 
			                       and (stock_res.idpresentacion is null or stock_res.idpresentacion=@idpresentacion) 
			                       and stock_res.idunidadmedida =@idunidadmedida 
			                       and stock_res.IdPedidoDet = @IdPedidoDet  "

            If ConEstado Then
                vSQL += " and stock_res.idproductoestado=@idproductoestado "
            End If

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdProducto", pProducto.IdProductoBodega)

                If Not pProducto.Presentacion Is Nothing Then

                    If pProducto.Presentacion.IdPresentacion <> 0 Then
                        lCommand.Parameters.AddWithValue("@IdPresentacion", pProducto.Presentacion.IdPresentacion)
                    Else
                        lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                    End If
                Else
                    lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                End If

                lCommand.Parameters.AddWithValue("@IdPedidoDet", IdPedidoDet)
                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)

                If ConEstado Then
                    lCommand.Parameters.AddWithValue("@IdProductoEstado", pProducto.ProductoEstado.IdEstado)
                End If

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_Peso_Reservado = lReturnValue
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Peso_Reservado(ByRef pProducto As clsBeStock,
                                              ByVal IdPedidoDet As Integer,
                                              Optional ByVal ConEstado As Boolean = True) As Double

        Get_Peso_Reservado = 0

        Try

            Dim vSQL As String = " select sum(stock_res.peso) as peso 
			                       from stock_res inner join  
			                       producto_bodega on stock_res.idproductobodega = producto_bodega.idproductobodega  
			                       where producto_bodega.idproductobodega=@idproducto 
			                       and (stock_res.idpresentacion is null or stock_res.idpresentacion=@idpresentacion) 
			                       and stock_res.idunidadmedida =@idunidadmedida 
			                       and stock_res.IdPedidoDet = @IdPedidoDet  "

            If ConEstado Then
                vSQL += " and stock_res.idproductoestado=@idproductoestado "
            End If


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@IdProducto", pProducto.IdProductoBodega)

                        If Not pProducto.Presentacion Is Nothing Then

                            If pProducto.Presentacion.IdPresentacion <> 0 Then
                                lCommand.Parameters.AddWithValue("@IdPresentacion", pProducto.Presentacion.IdPresentacion)
                            Else
                                lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                            End If
                        Else
                            lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                        End If

                        lCommand.Parameters.AddWithValue("@IdPedidoDet", IdPedidoDet)
                        lCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)
                        lCommand.Parameters.AddWithValue("@IdProductoEstado", pProducto.ProductoEstado.IdEstado)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_Peso_Reservado = lReturnValue
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

    Public Shared Function Get_Cantidad_Reservada(ByRef pProducto As clsBeStock,
                                                  ByRef pConnection As SqlConnection,
                                                  ByRef pTransaction As SqlTransaction,
                                                  Optional ByVal ConEstado As Boolean = True,
                                                  Optional ByVal ConLote As Boolean = False,
                                                  Optional ByRef pExcluirUbicacionesPicking As Boolean = False,
                                                  Optional ByVal pIdUbicacion As Integer = 0) As Double

        Get_Cantidad_Reservada = 0

        Try

            Dim vManejaTallaColor As Boolean = False

            vManejaTallaColor = clsLnBodega.Get_Maneja_Talla_Color_By_IdBodega(pProducto.IdBodega, pConnection, pTransaction)

            Dim vSQL As String = " select sum(stock_res.cantidad) as cantidad
					               from stock_res inner join
					               producto_bodega on stock_res.idproductobodega = producto_bodega.idproductobodega
					               where producto_bodega.idproductobodega=@idproducto
					               and (stock_res.idpresentacion is null or stock_res.idpresentacion =@idpresentacion)
					               and stock_res.idunidadmedida =@idunidadmedida "

            If ConEstado Then
                vSQL += " and stock_res.idproductoestado=@idproductoestado "
            End If

            If ConLote Then
                vSQL += " and stock_res.lote=@lote "
            End If

            If pProducto.IsReportStockEnFecha Then
                vSQL += " AND stock_res.fecha_vence = @fecha_vence "
            End If

            If pExcluirUbicacionesPicking Then

                If pIdUbicacion = 0 Then

                    '#EJC20190311_0948PM: Excluir lo que esté en ubicaciones de tránsito.
                    vSQL += " and stock_res.idubicacion NOT IN (SELECT IdUbicacion
							                        FROM  bodega_ubicacion AS bodega_ubicacion 
								                    WHERE (ubicacion_picking = 1 and IdBodega=@IdBodega))"

                End If

            End If

            If pIdUbicacion <> 0 Then
                '#EJC20190311_0948PM: Buscar stock por ubicación específica.
                vSQL += " and stock_res.idubicacion =@IdUbicacion"
            End If

            If vManejaTallaColor Then
                vSQL += " and stock_res.IdProductoTallaColor = @IdProductoTallaColor  "
            End If

            '#EJC202309120602: ANALIZAR ESTO CON CAROLINA
            'vSQL += " and stock_res.estado='UNCOMMITED'"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdProducto", pProducto.IdProductoBodega)

                If Not pProducto.Presentacion Is Nothing Then

                    If pProducto.Presentacion.IdPresentacion <> 0 Then
                        lCommand.Parameters.AddWithValue("@IdPresentacion", pProducto.Presentacion.IdPresentacion)
                    Else
                        lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                    End If
                Else
                    lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                End If

                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)

                If ConEstado Then
                    lCommand.Parameters.AddWithValue("@IdProductoEstado", pProducto.ProductoEstado.IdEstado)
                End If

                If ConLote Then
                    lCommand.Parameters.AddWithValue("@Lote", pProducto.Lote)
                End If

                If pProducto.IsReportStockEnFecha Then
                    lCommand.Parameters.AddWithValue("@fecha_vence", pProducto.Fecha_vence)
                End If

                If pIdUbicacion <> 0 Then
                    lCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                End If

                If pProducto.IdBodega <> 0 Then
                    lCommand.Parameters.AddWithValue("@IdBodega", pProducto.IdBodega)
                End If

                If vManejaTallaColor Then
                    lCommand.Parameters.AddWithValue("@IdProductoTallaColor", pProducto.IdProductoTallaColor)
                End If

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_Cantidad_Reservada = lReturnValue
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Peso_Reservado(ByRef pProducto As clsBeStock,
                                              ByVal pConnection As SqlConnection,
                                              ByVal pTransaction As SqlTransaction,
                                              Optional ByVal ConEstado As Boolean = True,
                                              Optional ByVal ConLote As Boolean = False,
                                              Optional ByVal pIdUbicacion As Integer = 0) As Double

        Get_Peso_Reservado = 0

        Try

            Dim vSQL As String = " select sum(stock_res.peso) as peso
			                       from stock_res inner join
			                       producto_bodega on stock_res.idproductobodega = producto_bodega.idproductobodega
			                       where producto_bodega.idproductobodega=@idproducto
			                       and (stock_res.idpresentacion is null or stock_res.idpresentacion=@idpresentacion)
			                       and stock_res.idunidadmedida =@idunidadmedida "

            If ConEstado Then
                vSQL += " and stock_res.idproductoestado=@idproductoestado "
            End If

            If ConLote Then
                vSQL += " and stock_res.lote=@lote "
            End If

            If pIdUbicacion <> 0 Then
                vSQL += " and stock_res.IdUbicacion=@IdUbicacion "
            End If

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdProducto", pProducto.IdProductoBodega)

                If Not pProducto.Presentacion Is Nothing Then

                    If pProducto.Presentacion.IdPresentacion <> 0 Then
                        lCommand.Parameters.AddWithValue("@IdPresentacion", pProducto.Presentacion.IdPresentacion)
                    Else
                        lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                    End If
                Else
                    lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                End If

                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)

                If ConEstado Then
                    lCommand.Parameters.AddWithValue("@IdProductoEstado", pProducto.ProductoEstado.IdEstado)
                End If

                If ConLote Then
                    lCommand.Parameters.AddWithValue("@Lote", pProducto.Lote)
                End If

                If pIdUbicacion <> 0 Then
                    lCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                End If

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_Peso_Reservado = lReturnValue
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared lPresentacionesExistencia As New List(Of clsBeProducto_Presentacion)

    'Public Shared Function Get_Cantidad_Disponible_En_Ubicaciones_Picking(ByRef pProducto As clsBeStock,
    '                                                                      ByVal pIdBodega As Integer,
    '                                                                      ByVal pConnection As SqlConnection,
    '                                                                      ByVal pTransaction As SqlTransaction,
    '                                                                      Optional ByVal DiasVencimiento As Double = 0,
    '                                                                      Optional ByVal ConEstado As Boolean = True,
    '                                                                      Optional ByVal ConLote As Boolean = False,
    '                                                                      Optional ByVal pExcluirUbicacionesPickign As Boolean = True) As Double

    '    Get_Cantidad_Disponible_En_Ubicaciones_Picking = 0

    '    Try

    '        Dim lStock As New List(Of clsBeStock)

    '        '#EJC20180905: Se agregó validación de IdStock =0 normalmente Por interface o <> 0 si es un stock específico
    '        If pProducto.IdStock = 0 Then
    '            lStock = Get_Lista_Existencias_By_IdProductoBodega_And_DiasVencimiento_Picking(pProducto,
    '                                                                                           pIdBodega,
    '                                                                                           DiasVencimiento,
    '                                                                                           pConnection,
    '                                                                                           pTransaction,
    '                                                                                           pExcluirUbicacionesPickign,
    '                                                                                           ConEstado,
    '                                                                                           ConLote)
    '        Else
    '            lStock = Get_Lista_Existencias_By_IdStock_And_DiasVencimiento(pProducto,
    '                                                                          DiasVencimiento,
    '                                                                          pConnection,
    '                                                                          pTransaction)
    '        End If

    '        Dim vCantidadAcumulada As Double = 0
    '        Dim BeConversionEntrePresentaciones As New clsBeProducto_presentaciones_conversiones
    '        Dim BePresentacionStock As New clsBeProducto_Presentacion
    '        Dim vCantAux As Double = 0
    '        Dim vFactorPallet As Double = 0
    '        Dim IdxPresentacionEnMemoria As Integer = -1
    '        Dim vIdPresentacion As Integer = pProducto.IdPresentacion

    '        If Not lStock Is Nothing Then

    '            If pProducto.IdPresentacion <> 0 Then

    '                IdxPresentacionEnMemoria = lPresentacionesExistencia.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)

    '                If IdxPresentacionEnMemoria = -1 Then
    '                    pProducto.Presentacion = New clsBeProducto_Presentacion
    '                    pProducto.Presentacion = clsLnProducto_presentacion.GetSingle(pProducto.IdPresentacion, pConnection, pTransaction)
    '                    lPresentacionesExistencia.Add(pProducto.Presentacion.Clone())
    '                Else
    '                    pProducto.Presentacion = lPresentacionesExistencia(IdxPresentacionEnMemoria).Clone()
    '                End If

    '            End If

    '            For Each S In lStock

    '                If S.IdPresentacion <> 0 Then

    '                    IdxPresentacionEnMemoria = lPresentacionesExistencia.FindIndex(Function(x) x.IdPresentacion = S.IdPresentacion)

    '                    If IdxPresentacionEnMemoria = -1 Then
    '                        BePresentacionStock = New clsBeProducto_Presentacion
    '                        BePresentacionStock = clsLnProducto_presentacion.GetSingle(S.IdPresentacion, pConnection, pTransaction)
    '                        lPresentacionesExistencia.Add(BePresentacionStock.Clone())
    '                    Else
    '                        BePresentacionStock = lPresentacionesExistencia(IdxPresentacionEnMemoria).Clone()
    '                    End If

    '                End If

    '                If S.IdPresentacion = pProducto.IdPresentacion Then

    '                    If S.IdPresentacion = 0 AndAlso pProducto.IdPresentacion = 0 Then
    '                        vCantidadAcumulada += S.Cantidad
    '                    ElseIf BePresentacionStock.EsPallet Then
    '                        vFactorPallet = (BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima * BePresentacionStock.Factor)
    '                        'Obtenerla cantidad de pallets
    '                        vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
    '                        vCantidadAcumulada += vCantAux
    '                    Else
    '                        If ConLote AndAlso Not ConEstado Then
    '                            vCantAux = Math.Round(S.Cantidad, 6)
    '                        Else
    '                            vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
    '                        End If
    '                        'Obtenerla cantidad en ump de presentación
    '                        'vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
    '                        'Multiplicar por factor de umbas
    '                        vCantidadAcumulada += vCantAux
    '                    End If

    '                ElseIf (S.IdPresentacion = 0 AndAlso pProducto.IdPresentacion > 0) Then

    '                    If pProducto.Presentacion.EsPallet Then
    '                        vFactorPallet = (pProducto.Presentacion.CajasPorCama * pProducto.Presentacion.CamasPorTarima * pProducto.Presentacion.Factor)
    '                        'Obtenerla cantidad de pallets
    '                        vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
    '                        'Multiplicar por factor de umbas
    '                        'vCantidadAcumulada += vCantAux * pProducto.Presentacion.Factor
    '                        vCantidadAcumulada += vCantAux
    '                    Else
    '                        'Obtenerla cantidad en ump de presentación
    '                        vCantAux = S.Cantidad / IIf(pProducto.Presentacion.Factor > 0, pProducto.Presentacion.Factor, 1)
    '                        'Multiplicar por factor de umbas
    '                        vCantidadAcumulada += vCantAux
    '                    End If

    '                ElseIf (S.IdPresentacion <> 0 AndAlso pProducto.IdPresentacion = 0) Then

    '                    'GT 260720210945: Si el pedido no esta vacio, pero su idpresentación es 0, no se debe multiplicar x el factor, porque se estan pidiendo umbas
    '                    If Not pProducto.Presentacion Is Nothing AndAlso pProducto.Presentacion.IdPresentacion <> 0 Then
    '                        If S.IdPresentacion <> 0 Then
    '                            vCantidadAcumulada += S.Cantidad * BePresentacionStock.Factor
    '                        Else
    '                            vCantidadAcumulada += S.Cantidad * pProducto.Presentacion.Factor
    '                        End If
    '                    Else
    '                        vCantidadAcumulada += S.Cantidad
    '                    End If

    '                ElseIf (S.IdPresentacion > 0 AndAlso pProducto.IdPresentacion > 0) Then

    '                    BeConversionEntrePresentaciones = clsLnProducto_Presentaciones_conversiones.GetSingle(S.IdPresentacion,
    '                                                                                                            pProducto.IdPresentacion,
    '                                                                                                            pConnection,
    '                                                                                                            pTransaction)

    '                    If Not BeConversionEntrePresentaciones Is Nothing Then

    '                        If BePresentacionStock.EsPallet Then
    '                            vFactorPallet = (BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima * BePresentacionStock.Factor)
    '                            'Obtenerla cantidad de pallets
    '                            vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
    '                            'Multiplicar por el factor para obtener la cantidad de umps
    '                            vCantAux = vCantAux * BeConversionEntrePresentaciones.Factor
    '                            'Multiplicar por factor de umbas
    '                            vCantidadAcumulada += vCantAux
    '                        Else
    '                            'Obtenerla cantidad en ump de presentación
    '                            vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
    '                            'Multiplicar por el factor para obtener la cantidad de umps
    '                            vCantAux = vCantAux * BeConversionEntrePresentaciones.Factor
    '                            'Multiplicar por factor de umbas
    '                            vCantidadAcumulada += vCantAux
    '                        End If

    '                    Else

    '                        BeConversionEntrePresentaciones = clsLnProducto_Presentaciones_conversiones.GetSingle(pProducto.IdPresentacion,
    '                                                                        S.IdPresentacion,
    '                                                                        pConnection,
    '                                                                        pTransaction)

    '                        If Not BeConversionEntrePresentaciones Is Nothing Then

    '                            If BePresentacionStock.EsPallet Then
    '                                vFactorPallet = (BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima * BePresentacionStock.Factor)
    '                                'Obtenerla cantidad de pallets
    '                                vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
    '                                'Multiplicar por el factor para obtener la cantidad de umps
    '                                vCantAux = vCantAux * BeConversionEntrePresentaciones.Factor
    '                                'Multiplicar por factor de umbas
    '                                vCantidadAcumulada += vCantAux / pProducto.Presentacion.Factor
    '                            Else
    '                                'Obtenerla cantidad en ump de presentación
    '                                vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
    '                                'Multiplicar por factor de umbas
    '                                vCantidadAcumulada += vCantAux / BeConversionEntrePresentaciones.Factor
    '                            End If

    '                        Else
    '                            Throw New Exception(String.Format("No está definida la conversión entre presentaciones de {0} a {1}", pProducto.Presentacion.Nombre, BePresentacionStock.Nombre))
    '                        End If

    '                    End If

    '                End If

    '            Next

    '            Get_Cantidad_Disponible_En_Ubicaciones_Picking = vCantidadAcumulada

    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Function Get_Cantidad_Disponible(ByRef pProducto As clsBeStock,
                                                   ByVal pIdBodega As Integer,
                                                   ByVal pConnection As SqlConnection,
                                                   ByVal pTransaction As SqlTransaction,
                                                   Optional ByVal DiasVencimiento As Double = 0,
                                                   Optional ByVal ConEstado As Boolean = True,
                                                   Optional ByVal ConLote As Boolean = False,
                                                   Optional ByVal pExcluirUbicacionesPickign As Boolean = False,
                                                   Optional ByVal pIdUbicacion As Integer = 0) As Double

        Get_Cantidad_Disponible = 0

        Try

            Dim lStock As New List(Of clsBeStock)

            '#EJC20180905: Se agregó validación de IdStock =0 normalmente Por interface o <> 0 si es un stock específico
            If pProducto.IdStock = 0 Then
                lStock = Get_Lista_Existencias_By_IdProductoBodega_And_DiasVencimiento(pProducto,
                                                                                       pIdBodega,
                                                                                       DiasVencimiento,
                                                                                       pConnection,
                                                                                       pTransaction,
                                                                                       pExcluirUbicacionesPickign,
                                                                                       ConEstado,
                                                                                       ConLote,
                                                                                       pIdUbicacion)
            Else
                lStock = Get_Lista_Existencias_By_IdStock_And_DiasVencimiento(pProducto,
                                                                              DiasVencimiento,
                                                                              pConnection,
                                                                              pTransaction)
            End If

            Dim vCantidadAcumulada As Double = 0
            Dim BeConversionEntrePresentaciones As New clsBeProducto_presentaciones_conversiones
            Dim BePresentacionStock As New clsBeProducto_Presentacion
            Dim vCantAux As Double = 0
            Dim vFactorPallet As Double = 0
            Dim IdxPresentacionEnMemoria As Integer = -1
            Dim vIdPresentacion As Integer = pProducto.IdPresentacion

            If Not lStock Is Nothing Then

                If pProducto.IdPresentacion <> 0 Then

                    IdxPresentacionEnMemoria = lPresentacionesExistencia.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)

                    If IdxPresentacionEnMemoria = -1 Then
                        pProducto.Presentacion = New clsBeProducto_Presentacion
                        pProducto.Presentacion = clsLnProducto_presentacion.GetSingle(pProducto.IdPresentacion, pConnection, pTransaction)
                        lPresentacionesExistencia.Add(pProducto.Presentacion.Clone())
                    Else
                        pProducto.Presentacion = lPresentacionesExistencia(IdxPresentacionEnMemoria).Clone()
                    End If

                End If

                For Each S In lStock

                    If S.IdPresentacion <> 0 Then

                        IdxPresentacionEnMemoria = lPresentacionesExistencia.FindIndex(Function(x) x.IdPresentacion = S.IdPresentacion)

                        If IdxPresentacionEnMemoria = -1 Then
                            BePresentacionStock = New clsBeProducto_Presentacion
                            BePresentacionStock = clsLnProducto_presentacion.GetSingle(S.IdPresentacion, pConnection, pTransaction)
                            lPresentacionesExistencia.Add(BePresentacionStock.Clone())
                        Else
                            BePresentacionStock = lPresentacionesExistencia(IdxPresentacionEnMemoria).Clone()
                        End If

                    End If

                    If S.IdPresentacion = pProducto.IdPresentacion Then

                        If S.IdPresentacion = 0 AndAlso pProducto.IdPresentacion = 0 Then
                            vCantidadAcumulada += S.Cantidad
                        ElseIf BePresentacionStock.EsPallet Then
                            vFactorPallet = (BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima * BePresentacionStock.Factor)
                            'Obtenerla cantidad de pallets
                            vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
                            vCantidadAcumulada += vCantAux
                        Else
                            If ConLote AndAlso Not ConEstado Then
                                vCantAux = Math.Round(S.Cantidad, 6)
                            Else
                                vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
                            End If
                            'Obtenerla cantidad en ump de presentación
                            'vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
                            'Multiplicar por factor de umbas
                            vCantidadAcumulada += vCantAux
                        End If

                    ElseIf (S.IdPresentacion = 0 AndAlso pProducto.IdPresentacion > 0) Then

                        If pProducto.Presentacion.EsPallet Then
                            vFactorPallet = (pProducto.Presentacion.CajasPorCama * pProducto.Presentacion.CamasPorTarima * pProducto.Presentacion.Factor)
                            'Obtenerla cantidad de pallets
                            vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
                            'Multiplicar por factor de umbas
                            'vCantidadAcumulada += vCantAux * pProducto.Presentacion.Factor
                            vCantidadAcumulada += vCantAux
                        Else
                            'Obtenerla cantidad en ump de presentación
                            vCantAux = S.Cantidad / IIf(pProducto.Presentacion.Factor > 0, pProducto.Presentacion.Factor, 1)
                            'Multiplicar por factor de umbas
                            vCantidadAcumulada += vCantAux
                        End If

                    ElseIf (S.IdPresentacion <> 0 AndAlso pProducto.IdPresentacion = 0) Then

                        'GT 260720210945: Si el pedido no esta vacio, pero su idpresentación es 0, no se debe multiplicar x el factor, porque se estan pidiendo umbas
                        If Not pProducto.Presentacion Is Nothing AndAlso pProducto.Presentacion.IdPresentacion <> 0 Then
                            If S.IdPresentacion <> 0 Then
                                vCantidadAcumulada += S.Cantidad * BePresentacionStock.Factor
                            Else
                                vCantidadAcumulada += S.Cantidad * pProducto.Presentacion.Factor
                            End If
                        Else
                            vCantidadAcumulada += S.Cantidad
                        End If

                    ElseIf (S.IdPresentacion > 0 AndAlso pProducto.IdPresentacion > 0) Then

                        BeConversionEntrePresentaciones = clsLnProducto_Presentaciones_conversiones.GetSingle(S.IdPresentacion,
                                                                                                                pProducto.IdPresentacion,
                                                                                                                pConnection,
                                                                                                                pTransaction)

                        If Not BeConversionEntrePresentaciones Is Nothing Then

                            If BePresentacionStock.EsPallet Then
                                vFactorPallet = (BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima * BePresentacionStock.Factor)
                                'Obtenerla cantidad de pallets
                                vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
                                'Multiplicar por el factor para obtener la cantidad de umps
                                vCantAux = vCantAux * BeConversionEntrePresentaciones.Factor
                                'Multiplicar por factor de umbas
                                vCantidadAcumulada += vCantAux
                            Else
                                'Obtenerla cantidad en ump de presentación
                                vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
                                'Multiplicar por el factor para obtener la cantidad de umps
                                vCantAux = vCantAux * BeConversionEntrePresentaciones.Factor
                                'Multiplicar por factor de umbas
                                vCantidadAcumulada += vCantAux
                            End If

                        Else

                            BeConversionEntrePresentaciones = clsLnProducto_Presentaciones_conversiones.GetSingle(pProducto.IdPresentacion,
                                                                                                                 S.IdPresentacion,
                                                                                                                 pConnection,
                                                                                                                 pTransaction)

                            If Not BeConversionEntrePresentaciones Is Nothing Then

                                If BePresentacionStock.EsPallet Then
                                    vFactorPallet = (BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima * BePresentacionStock.Factor)
                                    'Obtenerla cantidad de pallets
                                    vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
                                    'Multiplicar por el factor para obtener la cantidad de umps
                                    vCantAux = vCantAux * BeConversionEntrePresentaciones.Factor
                                    'Multiplicar por factor de umbas
                                    vCantidadAcumulada += vCantAux / pProducto.Presentacion.Factor
                                Else
                                    'Obtenerla cantidad en ump de presentación
                                    vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
                                    'Multiplicar por factor de umbas
                                    vCantidadAcumulada += vCantAux / BeConversionEntrePresentaciones.Factor
                                End If

                            Else
                                Throw New Exception(String.Format("No está definida la conversión entre presentaciones de {0} a {1}", pProducto.Presentacion.Nombre, BePresentacionStock.Nombre))
                            End If

                        End If

                    End If

                Next

                Get_Cantidad_Disponible = vCantidadAcumulada

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Cantidad_Disponible_Producto_Kit(ByRef pProducto As clsBeStock,
                                                                ByVal pIdBodega As Integer,
                                                                ByVal pConnection As SqlConnection,
                                                                ByVal pTransaction As SqlTransaction,
                                                                Optional ByVal DiasVencimiento As Double = 0,
                                                                Optional ByVal ConEstado As Boolean = True,
                                                                Optional ByVal ConLote As Boolean = False) As Double

        Get_Cantidad_Disponible_Producto_Kit = 0

        Try

            Dim lStock As New List(Of clsBeStock)

            '#EJC20180905: Se agregó validación de IdStock =0 normalmente Por interface o <> 0 si es un stock específico
            If pProducto.IdStock = 0 Then
                lStock = Get_Lista_Existencias_By_IdProductoBodega_And_DiasVencimiento(pProducto,
                                                                                       pIdBodega,
                                                                                       DiasVencimiento,
                                                                                       pConnection,
                                                                                       pTransaction,
                                                                                       ConEstado,
                                                                                       ConLote)
            Else
                lStock = Get_Lista_Existencias_By_IdStock_And_DiasVencimiento(pProducto,
                                                                              DiasVencimiento,
                                                                              pConnection,
                                                                              pTransaction)
            End If

            Dim vCantidadAcumulada As Double = 0
            Dim BeConversionEntrePresentaciones As New clsBeProducto_presentaciones_conversiones
            Dim BePresentacionStock As New clsBeProducto_Presentacion
            Dim vCantAux As Double = 0
            Dim vFactorPallet As Double = 0
            Dim IdxPresentacionEnMemoria As Integer = -1
            Dim vIdPresentacion As Integer = pProducto.IdPresentacion

            If Not lStock Is Nothing Then

                If pProducto.IdPresentacion <> 0 Then

                    IdxPresentacionEnMemoria = lPresentacionesExistencia.FindIndex(Function(x) x.IdPresentacion = vIdPresentacion)

                    If IdxPresentacionEnMemoria = -1 Then
                        pProducto.Presentacion = New clsBeProducto_Presentacion
                        pProducto.Presentacion = clsLnProducto_presentacion.GetSingle(pProducto.IdPresentacion, pConnection, pTransaction)
                        lPresentacionesExistencia.Add(pProducto.Presentacion.Clone())
                    Else
                        pProducto.Presentacion = lPresentacionesExistencia(IdxPresentacionEnMemoria).Clone()
                    End If

                End If

                For Each S In lStock

                    If S.IdPresentacion <> 0 Then

                        IdxPresentacionEnMemoria = lPresentacionesExistencia.FindIndex(Function(x) x.IdPresentacion = S.IdPresentacion)

                        If IdxPresentacionEnMemoria = -1 Then
                            BePresentacionStock = New clsBeProducto_Presentacion
                            BePresentacionStock = clsLnProducto_presentacion.GetSingle(S.IdPresentacion, pConnection, pTransaction)
                            lPresentacionesExistencia.Add(BePresentacionStock.Clone())
                        Else
                            BePresentacionStock = lPresentacionesExistencia(IdxPresentacionEnMemoria).Clone()
                        End If

                    End If

                    If S.IdPresentacion = pProducto.IdPresentacion Then

                        If S.IdPresentacion = 0 AndAlso pProducto.IdPresentacion = 0 Then
                            vCantidadAcumulada += S.Cantidad
                        ElseIf BePresentacionStock.EsPallet Then
                            vFactorPallet = (BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima * BePresentacionStock.Factor)
                            'Obtenerla cantidad de pallets
                            vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
                            vCantidadAcumulada += vCantAux
                        Else
                            If ConLote AndAlso Not ConEstado Then
                                vCantAux = Math.Round(S.Cantidad, 6)
                            Else
                                vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
                            End If
                            'Obtenerla cantidad en ump de presentación
                            'vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
                            'Multiplicar por factor de umbas
                            vCantidadAcumulada += vCantAux
                        End If

                    ElseIf (S.IdPresentacion = 0 AndAlso pProducto.IdPresentacion > 0) Then

                        If pProducto.Presentacion.EsPallet Then
                            vFactorPallet = (pProducto.Presentacion.CajasPorCama * pProducto.Presentacion.CamasPorTarima * pProducto.Presentacion.Factor)
                            'Obtenerla cantidad de pallets
                            vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
                            'Multiplicar por factor de umbas
                            'vCantidadAcumulada += vCantAux * pProducto.Presentacion.Factor
                            vCantidadAcumulada += vCantAux
                        Else
                            'Obtenerla cantidad en ump de presentación
                            vCantAux = S.Cantidad / IIf(pProducto.Presentacion.Factor > 0, pProducto.Presentacion.Factor, 1)
                            'Multiplicar por factor de umbas
                            vCantidadAcumulada += vCantAux
                        End If

                    ElseIf (S.IdPresentacion <> 0 AndAlso pProducto.IdPresentacion = 0) Then

                        If Not pProducto.Presentacion Is Nothing Then
                            If S.IdPresentacion <> 0 Then
                                vCantidadAcumulada += S.Cantidad * BePresentacionStock.Factor
                            Else
                                vCantidadAcumulada += S.Cantidad * pProducto.Presentacion.Factor
                            End If
                        Else
                            vCantidadAcumulada += S.Cantidad
                        End If

                    ElseIf (S.IdPresentacion > 0 AndAlso pProducto.IdPresentacion > 0) Then

                        BeConversionEntrePresentaciones = clsLnProducto_presentaciones_conversiones.GetSingle(S.IdPresentacion,
                        pProducto.IdPresentacion,
                        pConnection,
                        pTransaction)

                        If Not BeConversionEntrePresentaciones Is Nothing Then

                            If BePresentacionStock.EsPallet Then
                                vFactorPallet = (BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima * BePresentacionStock.Factor)
                                'Obtenerla cantidad de pallets
                                vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
                                'Multiplicar por el factor para obtener la cantidad de umps
                                vCantAux = vCantAux * BeConversionEntrePresentaciones.Factor
                                'Multiplicar por factor de umbas
                                vCantidadAcumulada += vCantAux
                            Else
                                'Obtenerla cantidad en ump de presentación
                                vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
                                'Multiplicar por el factor para obtener la cantidad de umps
                                vCantAux = vCantAux * BeConversionEntrePresentaciones.Factor
                                'Multiplicar por factor de umbas
                                vCantidadAcumulada += vCantAux
                            End If

                        Else

                            BeConversionEntrePresentaciones = clsLnProducto_presentaciones_conversiones.GetSingle(pProducto.IdPresentacion,
                            S.IdPresentacion,
                            pConnection,
                            pTransaction)

                            If Not BeConversionEntrePresentaciones Is Nothing Then

                                If BePresentacionStock.EsPallet Then
                                    vFactorPallet = (BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima * BePresentacionStock.Factor)
                                    'Obtenerla cantidad de pallets
                                    vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
                                    'Multiplicar por el factor para obtener la cantidad de umps
                                    vCantAux = vCantAux * BeConversionEntrePresentaciones.Factor
                                    'Multiplicar por factor de umbas
                                    vCantidadAcumulada += vCantAux / pProducto.Presentacion.Factor
                                Else
                                    'Obtenerla cantidad en ump de presentación
                                    vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
                                    'Multiplicar por factor de umbas
                                    vCantidadAcumulada += vCantAux / BeConversionEntrePresentaciones.Factor
                                End If

                            Else
                                Throw New Exception(String.Format("No está definida la conversión entre presentaciones de {0} a {1}", pProducto.Presentacion.Nombre, BePresentacionStock.Nombre))
                            End If

                        End If

                    End If

                Next

                Get_Cantidad_Disponible_Producto_Kit = vCantidadAcumulada

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Cantidad_Disponible(ByRef pProducto As clsBeStock,
                                                   ByVal pIdBodega As Integer,
                                                   ByVal pDiasVencimientoCliente As Integer,
                                                   Optional ByVal pConnection As SqlConnection = Nothing,
                                                   Optional ByVal pTransaction As SqlTransaction = Nothing,
                                                   Optional ByVal ConEstado As Boolean = False,
                                                   Optional ByVal ConLote As Boolean = False) As Double

        Get_Cantidad_Disponible = 0

        Try

            Dim lStock As New List(Of clsBeStock)

            '#EJC20180905: Se agregó validación de IdStock =0 normalmente Por interface o <> 0 si es un stock específico
            If pProducto.IdStock = 0 Then

                If pDiasVencimientoCliente = 0 Then

                    lStock = Get_Lista_Existencias_By_IdProductoBodega(pProducto,
                                                                       pIdBodega,
                                                                       pConnection,
                                                                       pTransaction,
                                                                       ConEstado,
                                                                       ConLote)
                Else

                    lStock = Get_Lista_Existencias_By_IdProductoBodega_And_DiasVencimiento(pProducto,
                                                                                           pIdBodega,
                                                                                           pDiasVencimientoCliente,
                                                                                           pConnection,
                                                                                           pTransaction,
                                                                                           ConEstado,
                                                                                           ConLote)

                End If

            Else

                If pDiasVencimientoCliente = 0 Then

                    lStock = Get_Lista_Existencias_By_IdStock(pProducto,
                                                      pConnection,
                                                      pTransaction)
                Else

                    lStock = Get_Lista_Existencias_By_IdStock_And_DiasVencimiento(pProducto,
                                                                                  pDiasVencimientoCliente,
                                                                                  pConnection,
                                                                                  pTransaction)

                End If


            End If

            Dim vCantidadAcumulada As Double = 0
            Dim BeConversionEntrePresentaciones As New clsBeProducto_presentaciones_conversiones
            Dim BePresentacionStock As New clsBeProducto_Presentacion
            Dim vCantAux As Double = 0
            Dim vFactorPallet As Double = 0

            If Not lStock Is Nothing Then

                If pProducto.IdPresentacion <> 0 Then
                    pProducto.Presentacion = clsLnProducto_presentacion.GetSingle(pProducto.IdPresentacion, pConnection, pTransaction)
                End If

                For Each S In lStock

                    If S.IdPresentacion <> 0 Then
                        BePresentacionStock = clsLnProducto_presentacion.GetSingle(S.IdPresentacion, pConnection, pTransaction)
                    End If

                    If S.IdPresentacion = pProducto.IdPresentacion Then

                        If S.IdPresentacion = 0 AndAlso pProducto.IdPresentacion = 0 Then
                            vCantidadAcumulada += S.Cantidad
                        ElseIf BePresentacionStock.EsPallet Then
                            vFactorPallet = (BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima * BePresentacionStock.Factor)
                            'Obtenerla cantidad de pallets
                            vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
                            vCantidadAcumulada += vCantAux
                        Else
                            If ConLote AndAlso Not ConEstado Then
                                vCantAux = Math.Round(S.Cantidad, 6)
                            Else
                                vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
                            End If
                            'Obtenerla cantidad en ump de presentación
                            'vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
                            'Multiplicar por factor de umbas
                            vCantidadAcumulada += vCantAux
                            vCantAux = 0
                        End If

                    ElseIf (S.IdPresentacion = 0 AndAlso pProducto.IdPresentacion > 0) Then

                        If pProducto.Presentacion.EsPallet Then
                            vFactorPallet = (pProducto.Presentacion.CajasPorCama * pProducto.Presentacion.CamasPorTarima * pProducto.Presentacion.Factor)
                            'Obtenerla cantidad de pallets
                            vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
                            'Multiplicar por factor de umbas
                            'vCantidadAcumulada += vCantAux * pProducto.Presentacion.Factor
                            vCantidadAcumulada += vCantAux
                        Else
                            '#EJC20190807: Sumar la cantidad en UMBas
                            vCantidadAcumulada += S.Cantidad
                        End If

                    ElseIf (S.IdPresentacion <> 0 AndAlso pProducto.IdPresentacion = 0) Then

                        If Not pProducto.Presentacion Is Nothing Then
                            vCantidadAcumulada += S.Cantidad * pProducto.Presentacion.Factor
                        Else
                            vCantidadAcumulada += S.Cantidad
                        End If

                    ElseIf (S.IdPresentacion > 0 AndAlso pProducto.IdPresentacion > 0) Then

                        BeConversionEntrePresentaciones = clsLnProducto_presentaciones_conversiones.GetSingle(S.IdPresentacion,
                        pProducto.IdPresentacion,
                        pConnection,
                        pTransaction)

                        If Not BeConversionEntrePresentaciones Is Nothing Then

                            If BePresentacionStock.EsPallet Then
                                vFactorPallet = (BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima * BePresentacionStock.Factor)
                                'Obtenerla cantidad de pallets
                                vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
                                'Multiplicar por el factor para obtener la cantidad de umps
                                vCantAux = vCantAux * BeConversionEntrePresentaciones.Factor
                                'Multiplicar por factor de umbas
                                vCantidadAcumulada += vCantAux
                            Else
                                'Obtenerla cantidad en ump de presentación
                                vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
                                'Multiplicar por el factor para obtener la cantidad de umps
                                vCantAux = vCantAux * BeConversionEntrePresentaciones.Factor
                                'Multiplicar por factor de umbas
                                vCantidadAcumulada += vCantAux
                            End If

                        Else

                            BeConversionEntrePresentaciones = clsLnProducto_presentaciones_conversiones.GetSingle(pProducto.IdPresentacion,
                            S.IdPresentacion,
                            pConnection,
                            pTransaction)

                            If Not BeConversionEntrePresentaciones Is Nothing Then

                                If BePresentacionStock.EsPallet Then
                                    vFactorPallet = (BePresentacionStock.CajasPorCama * BePresentacionStock.CamasPorTarima * BePresentacionStock.Factor)
                                    'Obtenerla cantidad de pallets
                                    vCantAux = S.Cantidad / IIf(vFactorPallet > 0, vFactorPallet, 1)
                                    'Multiplicar por el factor para obtener la cantidad de umps
                                    vCantAux = vCantAux * BeConversionEntrePresentaciones.Factor
                                    'Multiplicar por factor de umbas
                                    vCantidadAcumulada += vCantAux / pProducto.Presentacion.Factor
                                Else
                                    'Obtenerla cantidad en ump de presentación
                                    vCantAux = S.Cantidad / IIf(BePresentacionStock.Factor > 0, BePresentacionStock.Factor, 1)
                                    'Multiplicar por factor de umbas
                                    vCantidadAcumulada += vCantAux / BeConversionEntrePresentaciones.Factor
                                End If

                            Else
                                Throw New Exception(String.Format("No está definida la conversión entre presentaciones de {0} a {1}", pProducto.Presentacion.Nombre, BePresentacionStock.Nombre))
                            End If

                        End If

                    End If

                Next

                Get_Cantidad_Disponible = vCantidadAcumulada

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Peso_Disponible(ByRef pProducto As clsBeStock,
                                               ByVal pConnection As SqlConnection,
                                               ByVal pTransaction As SqlTransaction,
                                               Optional ByVal ConEstado As Boolean = True,
                                               Optional ByVal ConLote As Boolean = False,
                                               Optional ByVal pIdUbicacion As Integer = 0) As Double

        Get_Peso_Disponible = 0

        Try

            Dim vManejaTallaColor As Boolean = False

            Dim vSQL As String = " select sum(stock.peso) as peso  
                                   from stock inner join  
                                   producto_bodega on stock.idproductobodega = producto_bodega.idproductobodega  
                                   where producto_bodega.idproductobodega=@idproducto  
                                   and (stock.idpresentacion is null or stock.idpresentacion=@idpresentacion)   
                                   and stock.idunidadmedida =@idunidadmedida "

            If ConEstado Then
                vSQL += " and stock.idproductoestado=@idproductoestado "
            End If

            If ConLote Then
                vSQL += " and stock.lote=@lote "
            End If

            If pIdUbicacion <> 0 Then
                vSQL += " and stock.IdUbicacion=@IdUbicacion "
            End If

            vManejaTallaColor = clsLnBodega.Get_Maneja_Talla_Color_By_IdBodega(pProducto.IdBodega, pConnection, pTransaction)

            If vManejaTallaColor Then
                vSQL += " and stock.IdProductoTallaColor = @IdProductoTallaColor  "
            End If

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdProducto", pProducto.IdProductoBodega)

                If Not pProducto.Presentacion Is Nothing Then

                    If pProducto.Presentacion.IdPresentacion <> 0 Then
                        lCommand.Parameters.AddWithValue("@IdPresentacion", pProducto.Presentacion.IdPresentacion)
                    Else
                        lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                    End If
                Else
                    lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                End If

                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)

                If ConEstado Then
                    lCommand.Parameters.AddWithValue("@IdProductoEstado", pProducto.ProductoEstado.IdEstado)
                End If

                If ConLote Then
                    lCommand.Parameters.AddWithValue("@lote", pProducto.Lote)
                End If

                If pIdUbicacion <> 0 Then
                    lCommand.Parameters.AddWithValue("IdUbicacion", pIdUbicacion)
                End If

                If vManejaTallaColor Then
                    lCommand.Parameters.AddWithValue("@IdProductoTallaColor", pProducto.IdProductoTallaColor)
                End If

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_Peso_Disponible = lReturnValue
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Cantidad_Disp_By_IdProducto(ByVal pProducto As clsBeProducto,
                                                           ByVal pIdEstadoProducto As Integer,
                                                           ByVal pIdBodega As Integer,
                                                           Optional ByVal DiasVencimientoCliente As Double = 0,
                                                           Optional ByVal ConLote As Boolean = False,
                                                           Optional ByVal ConEstado As Boolean = False,
                                                           Optional ByVal lConnection As SqlConnection = Nothing,
                                                           Optional ByVal lTransaction As SqlTransaction = Nothing) As clsBeStock

        Get_Cantidad_Disp_By_IdProducto = Nothing
        Dim pBeStock As New clsBeStock

        Dim cmd As New SqlCommand

        Try

            pBeStock.IdProductoBodega = pProducto.IdProductoBodega
            pBeStock.Presentacion.IdPresentacion = 0
            pBeStock.IdPresentacion = 0
            pBeStock.IdUnidadMedida = pProducto.IdUnidadMedidaBasica

            '#EJC202210140219_PENDIENTE_REFACTORIZACIÓN_STOCK
            If ConEstado AndAlso pIdEstadoProducto <> 0 Then
                pBeStock.ProductoEstado.IdEstado = pIdEstadoProducto
            End If

            Dim vCantRes As Double = Get_Cantidad_Reservada(pBeStock,
                                                            lConnection,
                                                            lTransaction,
                                                            ConEstado)

            Dim vCantDis As Double = Get_Cantidad_Disponible_Producto_Kit(pBeStock,
                                                                          pIdBodega,
                                                                          lConnection,
                                                                          lTransaction,
                                                                          DiasVencimientoCliente,
                                                                          ConEstado,
                                                                          ConLote)

            If pBeStock.IdPresentacion <> 0 Then
                vCantRes = vCantRes / IIf(pProducto.Presentacion.Factor > 0, pProducto.Presentacion.Factor, 1)
            End If

            If pBeStock.IdPresentacion <> 0 Then
                vCantDis = vCantDis / IIf(pProducto.Presentacion.Factor > 0, pProducto.Presentacion.Factor, 1)
            End If

            pBeStock.Cantidad = vCantDis - vCantRes
            'pBeStock.Peso = vPesoDis - vPesoRes

            Get_Cantidad_Disp_By_IdProducto = pBeStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function Get_Existencia_Disp_By_IdProducto(ByRef pProducto As clsBeStock,
    '                                                         Optional ByVal DiasVencimientoCliente As Double = 0,
    '                                                         Optional ByVal ConLote As Boolean = False,
    '                                                         Optional ByVal ConEstado As Boolean = True,
    '                                                         Optional ByRef pConection As SqlConnection = Nothing,
    '                                                         Optional ByRef ptransaction As SqlTransaction = Nothing) As Boolean

    '    Get_Existencia_Disp_By_IdProducto = False

    '    Dim EsTransaccionExterna As Boolean = Not (pConection Is Nothing AndAlso ptransaction Is Nothing)

    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing

    '    Dim cmd As New SqlCommand

    '    Try

    '        If Not EsTransaccionExterna Then
    '            lConnection.Open()
    '            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)
    '        Else
    '            lConnection = pConection
    '            lTransaction = ptransaction
    '        End If

    '        Dim vCantDis As Double = Get_Cantidad_Disponible(pProducto,
    '                                                         lConnection,
    '                                                         lTransaction,
    '                                                         ConEstado,
    '                                                         ConLote)

    '        Dim vPesoDis As Double = Get_Peso_Disponible(pProducto,
    '                                                     lConnection,
    '                                                     lTransaction,
    '                                                     ConEstado,
    '                                                     ConLote)

    '        Dim vCantRes As Double = Get_Cantidad_Reservada(pProducto, lConnection, lTransaction, ConEstado)

    '        If pProducto.IdPresentacion <> 0 Then
    '            vCantRes = vCantRes / IIf(pProducto.Presentacion.Factor > 0, pProducto.Presentacion.Factor, 1)
    '        End If

    '        pProducto.Cantidad = vCantDis - vCantRes
    '        pProducto.Peso = vPesoDis

    '        If Not EsTransaccionExterna Then
    '            lTransaction.Commit()
    '        End If

    '        Get_Existencia_Disp_By_IdProducto = True

    '    Catch ex As Exception
    '        If Not EsTransaccionExterna Then If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex
    '    Finally
    '        If Not EsTransaccionExterna Then
    '            If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '        End If
    '        cmd.Dispose()
    '    End Try

    'End Function

    '#EJC20180616:Cálculo de stock específico por lote, utilizado inicialmente en reporte de stock en una fecha. (3.00 am a 5.34 am)

    Public Shared Function Get_Existencia_Disp_By_IdProducto_Reabasto(ByRef pProducto As clsBeStock,
                                                                      ByVal pIdBodega As Integer,
                                                                      ByVal pConection As SqlConnection,
                                                                      ByVal ptransaction As SqlTransaction) As Boolean

        Get_Existencia_Disp_By_IdProducto_Reabasto = False

        Dim cmd As New SqlCommand

        Try


            Dim vCantDis As Double = Get_Cantidad_Disponible(pProducto,
                                                             pIdBodega,
                                                             pConection,
                                                             ptransaction,
                                                             0,
                                                             True,
                                                             False)

            Dim vPesoDis As Double = Get_Peso_Disponible(pProducto,
                                                         pConection,
                                                         ptransaction,
                                                         True,
                                                         False)

            Dim vCantRes As Double = Get_Cantidad_Reservada(pProducto,
                                                            pConection,
                                                            ptransaction,
                                                            True)


            If pProducto.IdPresentacion <> 0 Then
                vCantRes = vCantRes / IIf(pProducto.Presentacion.Factor > 0, pProducto.Presentacion.Factor, 1)
            End If

            If vCantDis = 0 OrElse (vCantRes > vCantDis) Then
                pProducto.Cantidad = 0
                pProducto.Peso = 0
            Else
                pProducto.Cantidad = vCantDis - vCantRes
                pProducto.Peso = vPesoDis
            End If


            Get_Existencia_Disp_By_IdProducto_Reabasto = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Existencia_Disp_By_IdProducto(ByRef pBeStockConsulta As clsBeStock,
                                                             ByVal pIdBodega As Integer,
                                                             Optional ByVal ConEstado As Boolean = True,
                                                             Optional ByVal ConLote As Boolean = False,
                                                             Optional ByVal pDiasVencimientoCliente As Integer = 0,
                                                             Optional ByRef pExcluirUbicacionesPickign As Boolean = False,
                                                             Optional ByRef pConection As SqlConnection = Nothing,
                                                             Optional ByRef ptransaction As SqlTransaction = Nothing) As Boolean

        Get_Existencia_Disp_By_IdProducto = False

        Dim EsTransaccionExterna As Boolean = Not (pConection Is Nothing AndAlso ptransaction Is Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim cmd As New SqlCommand
        Dim vCantDis As Double = 0
        Dim vPesoDis As Double = 0
        Dim vCantRes As Double = 0
        Dim vPesoRes As Double = 0
        Dim vCantDispoSinUbicacionesPicking As Double = 0

        Try

            If Not EsTransaccionExterna Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            vCantDis = Get_Cantidad_Disponible(pBeStockConsulta,
                                              pIdBodega,
                                              IIf(EsTransaccionExterna, pConection, lConnection),
                                              IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                              pDiasVencimientoCliente,
                                              ConEstado,
                                              ConLote,
                                              pExcluirUbicacionesPickign)

            vPesoDis = Get_Peso_Disponible(pBeStockConsulta,
                                           IIf(EsTransaccionExterna, pConection, lConnection),
                                           IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                           ConEstado,
                                           ConLote)

            '#CKFK 20211228 Faltaba el parametro con lote y por eso no devolvía correctamente lo reservado
            vCantRes = Get_Cantidad_Reservada(pBeStockConsulta,
                                              IIf(EsTransaccionExterna, pConection, lConnection),
                                              IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                              ConEstado,
                                              ConLote,
                                              pExcluirUbicacionesPickign)


            '#CKFK 20211228 Faltaba el parametro con lote y por eso no devolvía correctamente lo reservado
            vPesoRes = Get_Peso_Reservado(pBeStockConsulta,
                                          IIf(EsTransaccionExterna, pConection, lConnection),
                                          IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                          ConEstado,
                                          ConLote)


            If pBeStockConsulta.IdPresentacion <> 0 Then
                If vCantRes <> 0 Then
                    vCantRes = vCantRes / IIf(pBeStockConsulta.Presentacion.Factor > 0, pBeStockConsulta.Presentacion.Factor, 1)
                End If
            End If

            If vCantDis = 0 OrElse (vCantRes > vCantDis) Then
                pBeStockConsulta.Cantidad = 0
                pBeStockConsulta.Peso = 0
            Else
                pBeStockConsulta.Cantidad = vCantDis - vCantRes
                pBeStockConsulta.Peso = vPesoDis - vPesoRes
            End If

            If Not EsTransaccionExterna Then lTransaction.Commit()

            Get_Existencia_Disp_By_IdProducto = True

        Catch ex As Exception
            If Not EsTransaccionExterna Then If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not EsTransaccionExterna Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
            End If
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Get_Existencia_Disp_By_IdProducto_And_IdUbicacion(ByRef pProducto As clsBeStock,
                                                                             ByVal pIdBodega As Integer,
                                                                             ByVal pIdUbicacion As Integer,
                                                                             Optional ByVal ConEstado As Boolean = True,
                                                                             Optional ByVal ConLote As Boolean = False,
                                                                             Optional ByVal pDiasVencimientoCliente As Integer = 0,
                                                                             Optional ByRef pExcluirUbicacionesPickign As Boolean = False,
                                                                             Optional ByRef pConection As SqlConnection = Nothing,
                                                                             Optional ByRef ptransaction As SqlTransaction = Nothing) As Boolean

        Get_Existencia_Disp_By_IdProducto_And_IdUbicacion = False

        Dim EsTransaccionExterna As Boolean = Not (pConection Is Nothing AndAlso ptransaction Is Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim cmd As New SqlCommand
        Dim vCantDis As Double = 0
        Dim vPesoDis As Double = 0
        Dim vCantRes As Double = 0
        Dim vCantDispoSinUbicacionesPicking As Double = 0

        Try

            If Not EsTransaccionExterna Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Else
                lConnection = pConection : lTransaction = ptransaction
            End If

            vCantDis = Get_Cantidad_Disponible(pProducto,
                                              pIdBodega,
                                              lConnection,
                                              lTransaction,
                                              pDiasVencimientoCliente,
                                              ConEstado,
                                              ConLote,
                                              pExcluirUbicacionesPickign,
                                              pIdUbicacion)

            vPesoDis = Get_Peso_Disponible(pProducto,
                                           lConnection,
                                           lTransaction,
                                           ConEstado,
                                           ConLote)

            vCantRes = Get_Cantidad_Reservada(pProducto,
                                              lConnection,
                                              lTransaction,
                                              ConEstado,
                                              False,
                                              pExcluirUbicacionesPickign,
                                              pIdUbicacion)


            If pProducto.IdPresentacion <> 0 Then
                If vCantRes <> 0 Then
                    vCantRes = vCantRes / IIf(pProducto.Presentacion.Factor > 0, pProducto.Presentacion.Factor, 1)
                End If
            End If

            If vCantDis = 0 OrElse (vCantRes > vCantDis) Then
                pProducto.Cantidad = 0
                pProducto.Peso = 0
            Else
                pProducto.Cantidad = vCantDis - vCantRes
                pProducto.Peso = vPesoDis
            End If

            If Not EsTransaccionExterna Then lTransaction.Commit()

            Get_Existencia_Disp_By_IdProducto_And_IdUbicacion = True

        Catch ex As Exception
            If Not EsTransaccionExterna Then If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not EsTransaccionExterna Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
            End If
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Procesar_Ajuste(ByVal BeTransAjusteDet As clsBeTrans_ajuste_det,
                                           ByVal BeUsuario As clsBeUsuario,
                                           Optional ByVal pConection As SqlConnection = Nothing,
                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim rowsAffected, IdStock As Integer
            Dim BeStockOriginal, BeStockNuevo, BeStockAActualizar As New clsBeStock

            IdStock = BeTransAjusteDet.IdStock

            BeStockOriginal.IdStock = BeTransAjusteDet.IdStock
            BeStockOriginal = GetSingle(BeStockOriginal.IdStock, pConection, pTransaction)

            If Not BeStockOriginal Is Nothing Then

                If BeTransAjusteDet.esnuevolink Then

                    If BeStockOriginal.Cantidad > 0 Then

                        If BeTransAjusteDet.Cantidad_nueva + BeTransAjusteDet.CantReservada > 0 Then

                            clsPublic.CopyObject(BeStockOriginal, BeStockNuevo)
                            BeStockNuevo.IdStock = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.
                            BeStockNuevo.Cantidad = BeTransAjusteDet.Cantidad_nueva + BeTransAjusteDet.CantReservada
                            BeStockNuevo.Peso = BeTransAjusteDet.Peso_nuevo
                            BeStockNuevo.Fecha_vence = BeTransAjusteDet.Fecha_vence_nueva
                            BeStockNuevo.Lote = BeTransAjusteDet.Lote_nuevo

                            BeStockNuevo.Presentacion = New clsBeProducto_Presentacion
                            BeStockNuevo.Presentacion.IdPresentacion = BeStockNuevo.IdPresentacion
                            BeStockNuevo.ProductoEstado = New clsBeProducto_estado
                            BeStockNuevo.ProductoEstado.IdEstado = BeStockNuevo.IdProductoEstado

                            BeStockNuevo.Fec_agr = Now
                            If Not BeUsuario Is Nothing Then
                                BeStockNuevo.User_agr = BeUsuario.IdUsuario
                            End If

                            BeStockNuevo.Fec_mod = Now
                            If Not BeUsuario Is Nothing Then
                                BeStockNuevo.User_mod = BeUsuario.IdUsuario
                            End If

                            '#GT18122025: considerar que solo talla o color fue actualizado y no ambos.
                            Dim vTalla As String = IIf(Not String.IsNullOrEmpty(BeTransAjusteDet.Talla_destino), BeTransAjusteDet.Talla_destino, BeTransAjusteDet.Talla_origen)
                            Dim vColor As String = IIf(Not String.IsNullOrEmpty(BeTransAjusteDet.Color_destino), BeTransAjusteDet.Color_destino, BeTransAjusteDet.Color_origen)
                            Dim vIdProductoTallaColor As Integer = IIf(BeTransAjusteDet.IdProductoTallaColor_destino > 0, BeTransAjusteDet.IdProductoTallaColor_destino, BeTransAjusteDet.IdProductoTallaColor_origen)

                            BeStockAActualizar.IdProductoTallaColor = vIdProductoTallaColor
                            BeStockAActualizar.Talla = vTalla
                            BeStockAActualizar.Color = vColor

                            Insertar(BeStockNuevo, pConection, pTransaction)

                            IdStock = BeStockNuevo.IdStock

                        End If

                    End If

                End If

                If BeTransAjusteDet.esnuevolink = 0 Then

                    If BeTransAjusteDet.Cantidad_nueva > 0 Then

                        Try

                            If Not BeStockOriginal Is Nothing Then

                                clsPublic.CopyObject(BeStockOriginal, BeStockAActualizar)

                                If BeTransAjusteDet.idstocklink <> 0 Then
                                    BeStockAActualizar.Cantidad = BeTransAjusteDet.Cantidad_nueva + BeTransAjusteDet.CantReservada
                                Else
                                    If (BeTransAjusteDet.Cantidad_original <> BeTransAjusteDet.Cantidad_nueva) Then
                                        BeStockAActualizar.Cantidad = BeTransAjusteDet.Cantidad_nueva + BeTransAjusteDet.CantReservada
                                    End If
                                End If
                                If (BeTransAjusteDet.Peso_original <> BeTransAjusteDet.Peso_nuevo) Then
                                    BeStockAActualizar.Peso = BeTransAjusteDet.Peso_nuevo
                                End If
                                If (BeTransAjusteDet.Fecha_vence_original <> BeTransAjusteDet.Fecha_vence_nueva) Then
                                    BeStockAActualizar.Fecha_vence = BeTransAjusteDet.Fecha_vence_nueva
                                End If
                                If BeTransAjusteDet.Lote_nuevo Is Nothing Then
                                    BeStockAActualizar.Lote = ""
                                ElseIf (BeTransAjusteDet.Lote_original <> BeTransAjusteDet.Lote_nuevo) Then
                                    BeStockAActualizar.Lote = BeTransAjusteDet.Lote_nuevo
                                End If

                                BeStockAActualizar.Fec_mod = Now

                                If Not BeUsuario Is Nothing Then
                                    BeStockAActualizar.User_mod = BeUsuario.IdUsuario
                                End If


                                '#GT18122025: considerar que solo talla o color fue actualizado y no ambos.
                                Dim vTalla As String = IIf(Not String.IsNullOrEmpty(BeTransAjusteDet.Talla_destino), BeTransAjusteDet.Talla_destino, BeTransAjusteDet.Talla_origen)
                                Dim vColor As String = IIf(Not String.IsNullOrEmpty(BeTransAjusteDet.Color_destino), BeTransAjusteDet.Color_destino, BeTransAjusteDet.Color_origen)
                                Dim vIdProductoTallaColor As Integer = IIf(BeTransAjusteDet.IdProductoTallaColor_destino > 0, BeTransAjusteDet.IdProductoTallaColor_destino, BeTransAjusteDet.IdProductoTallaColor_origen)

                                BeStockAActualizar.IdProductoTallaColor = vIdProductoTallaColor
                                BeStockAActualizar.Talla = vTalla
                                BeStockAActualizar.Color = vColor

                                If Es_Transaccion_Remota Then
                                    rowsAffected = Actualizar_Stock_Por_Ajuste(BeStockAActualizar, pConection, pTransaction)
                                Else
                                    rowsAffected = Actualizar_Stock_Por_Ajuste(BeStockAActualizar, lConnection, lTransaction)
                                End If

                            End If

                            Return rowsAffected

                        Catch ex1 As SqlException
                            Throw ex1
                        Catch ex As Exception
                            Throw ex
                        Finally
                            If lConnection.State = ConnectionState.Open Then lConnection.Close()
                        End Try

                    Else

                        Dim Stock As New clsBeStock
                        Stock.IdStock = IdStock
                        Eliminar(Stock, pConection, pTransaction)

                    End If

                End If

            Else

                Throw New Exception("No fue posible obtener el IdStock " & IdStock)

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#EJC20171021_0958AM: ésta función se utiliza en el proceso de finalizar recepción para registrar la existencia histórica en los movimientos.
    Public Shared Sub Get_Existencia_By_IdProductoBodega(ByRef BeStockRec As clsBeStock_rec,
                                                         ByVal pConnection As SqlConnection,
                                                         ByVal pTransaction As SqlTransaction)

        Try

            Dim Lote As String
            Dim Aniada As String
            Dim Fecha_Vence As Date
            Dim lStockConFiltro As New List(Of clsBeVW_stock_res)

            lStockConFiltro = Get_All_By_IdProductoBodega(BeStockRec.IdProductoBodega,
                                                          pConnection,
                                                          pTransaction)

            If lStockConFiltro Is Nothing Then
                '#EJC20180321 -> Cuando no había stock daba excepcipon
                BeStockRec.CantidadEnStock = 0
                BeStockRec.PesoEnStock = 0
            Else

                Dim BeProducto As clsBeProducto = clsLnProducto.Get_Single_Producto_Bodega(BeStockRec.IdProductoBodega,
                                                                                           pConnection,
                                                                                           pTransaction)

                If BeProducto.Control_lote Then
                    Lote = String.Format("'{0}'", BeStockRec.Lote)
                    Lote = BeStockRec.Lote
                    lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.Lote = Lote)
                Else
                    Lote = "Null"
                End If

                If lStockConFiltro.Count > 0 Then
                    If BeProducto.Capturar_aniada Then
                        Aniada = BeStockRec.Añada
                        lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.Añada = Aniada)
                    Else
                        Aniada = "Null"
                    End If
                End If

                If lStockConFiltro.Count > 0 Then
                    If BeProducto.Control_vencimiento Then
                        Fecha_Vence = BeStockRec.Fecha_vence.Date
                        lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.Fecha_Vence = Fecha_Vence)
                    End If
                End If

                Dim vIdPresentacion As Integer = BeStockRec.Presentacion.IdPresentacion
                lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.IdPresentacion = vIdPresentacion)

                Dim vIdUnidadMedida As Integer = BeStockRec.IdUnidadMedida
                lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.IdUnidadMedida = vIdUnidadMedida)

                Dim vIdEstado As Integer = BeStockRec.ProductoEstado.IdEstado
                lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.IdProductoEstado = vIdEstado)

                BeStockRec.CantidadEnStock = lStockConFiltro.Sum(Function(x) x.CantidadUmBas)
                BeStockRec.PesoEnStock = lStockConFiltro.Sum(Function(x) x.Peso)

            End If

        Catch ex As SqlException
            Throw ex
        End Try

    End Sub

    Public Shared Sub Get_Existencia_By_IdProductoBodega(ByRef pObjS As clsBeStock,
                                                         ByRef Cantidad As Double,
                                                         ByRef Peso As Double,
                                                         ByVal pConnection As SqlConnection,
                                                         ByVal pTransaction As SqlTransaction)

        Try

            Dim Lote As String
            Dim Aniada As String
            Dim Fecha_Vence As Date
            Dim lStockConFiltro As New List(Of clsBeVW_stock_res)

            lStockConFiltro = Get_All_By_IdProductoBodega(pObjS.IdProductoBodega, pConnection, pTransaction)

            If lStockConFiltro Is Nothing Then
                '#EJC20180321 -> Cuando no había stock daba excepcipon
                pObjS.Cantidad = 0
                pObjS.Peso = 0
            Else

                Dim ObjP As clsBeProducto = clsLnProducto.Get_Single_Producto_Bodega(pObjS.IdProductoBodega, pConnection, pTransaction)

                If ObjP.Control_lote Then
                    Lote = String.Format("'{0}'", pObjS.Lote)
                    Lote = pObjS.Lote
                    lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.Lote = Lote)
                Else
                    Lote = "Null"
                End If

                If lStockConFiltro.Count > 0 Then
                    If ObjP.Capturar_aniada Then
                        Aniada = pObjS.Añada
                        lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.Añada = Aniada)
                    Else
                        Aniada = "Null"
                    End If
                End If

                If lStockConFiltro.Count > 0 Then
                    If ObjP.Control_vencimiento Then
                        Fecha_Vence = pObjS.Fecha_vence.Date
                        lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.Fecha_Vence = Fecha_Vence)
                    End If
                End If

                Dim vIdPresentacion As Integer = pObjS.Presentacion.IdPresentacion
                lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.IdPresentacion = vIdPresentacion)

                Dim vIdUnidadMedida As Integer = pObjS.IdUnidadMedida
                lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.IdUnidadMedida = vIdUnidadMedida)

                Dim vIdEstado As Integer = pObjS.ProductoEstado.IdEstado
                lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.IdProductoEstado = vIdEstado)

                Dim vIdUbicacion As Integer = pObjS.IdUbicacion
                lStockConFiltro = lStockConFiltro.FindAll(Function(x) x.IdUbicacion = vIdUbicacion)

                Cantidad = lStockConFiltro.Sum(Function(x) x.CantidadUmBas)
                Peso = lStockConFiltro.Sum(Function(x) x.Peso)

            End If

        Catch ex As SqlException
            Throw ex
        End Try

    End Sub

    Public Shared Sub GetExistenciaByIdProductoBodega(ByRef BeStock As clsBeStock)

        Try

            Dim Lote As String
            Dim Aniada As String
            Dim Fecha_Vence As String
            Dim Fecha_Manufactura As String

            Dim BeProducto As clsBeProducto = clsLnProducto.Get_Single_BeProducto_By_IdProductoBodega(BeStock.IdProductoBodega)

            If BeProducto.Control_lote Then
                Lote = String.Format("'{0}'", BeStock.Lote)
            Else
                Lote = "Null"
            End If

            If BeProducto.Capturar_aniada Then
                Aniada = BeStock.Añada
            Else
                Aniada = "Null"
            End If

            If BeProducto.Control_vencimiento Then
                Fecha_Vence = FormatoFechas.fFecha(BeStock.Fecha_vence)
            Else
                Fecha_Vence = "Null"
            End If

            If BeProducto.Fechamanufactura Then
                Fecha_Manufactura = FormatoFechas.fFecha(BeStock.Fecha_Manufactura)
            Else
                Fecha_Manufactura = "Null"
            End If

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand

            Const sp As String = "GetCantidadPesoByProductoBodega " &
                                 " @IdProductoBodega, " &
                                 " @IdPresentacion, " &
                                 " @IdUnidadMedida, " &
                                 " @IdProductoEstado, " &
                                 " @Lote, " &
                                 " @Aniada, " &
                                 " @fecha_vence, " &
                                 " @fecha_manufactura, " &
                                 " @CantidadEnStock, " &
                                 " @PesoEnStock"

            cmd.CommandType = CommandType.StoredProcedure

            cmd = New SqlCommand(sp, lConnection)
            lConnection.Open()

            cmd.Parameters.AddWithValue("@IdProductoBodega", BeStock.IdProductoBodega)

            If Not BeStock.Presentacion Is Nothing Then
                cmd.Parameters.AddWithValue("@IdPresentacion", BeStock.Presentacion.IdPresentacion)
            Else
                cmd.Parameters.AddWithValue("@IdPresentacion", DBNull.Value)
            End If

            cmd.Parameters.AddWithValue("@IdUnidadMedida", BeStock.IdUnidadMedida)
            cmd.Parameters.AddWithValue("@IdProductoEstado", BeStock.ProductoEstado.IdEstado)

            If Lote = "Null" Then
                cmd.Parameters.AddWithValue("@Lote", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@Lote", Lote)
            End If

            If Aniada = "Null" Then
                cmd.Parameters.AddWithValue("@Aniada", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@Aniada", Aniada)
            End If

            If Fecha_Vence = "Null" Then
                cmd.Parameters.AddWithValue("@fecha_vence", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@fecha_vence", Fecha_Vence)
            End If

            If Fecha_Manufactura = "Null" Then
                cmd.Parameters.AddWithValue("@fecha_manufactura", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@fecha_manufactura", Fecha_Manufactura)
            End If

            cmd.Parameters.AddWithValue("@CantidadEnStock", BeStock.Cantidad)
            cmd.Parameters.AddWithValue("@PesoEnStock", BeStock.Peso)
            cmd.Parameters("@CantidadEnStock").Direction = ParameterDirection.Output
            cmd.Parameters("@PesoEnStock").Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()

            Using r = cmd.ExecuteReader()

                If r.Read() Then
                    ' Get the first and second field frm the reader'
                    BeStock.Cantidad = r.Item("CantidadEnStock")
                    BeStock.Peso = r.Item("PesoEnStock")
                End If

            End Using

        Catch ex As SqlException
            Throw ex
        End Try

    End Sub

    '#CKFK20230519 Creé esta función para obtener el inventario de un producto.
    Public Shared Sub GetStockByIdProductoBodega(ByRef BeStock As clsBeStock)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand


        Try

            Const sp As String = "SELECT SUM(cantidad) Cantidad,
                                         SUM(peso) Peso
                                  FROM stock WHERE IdProductoBodega = @IdProductoBodega "

            cmd.CommandType = CommandType.Text

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            cmd = New SqlCommand(sp, lConnection, lTransaction)
            cmd.Parameters.AddWithValue("@IdProductoBodega", BeStock.IdProductoBodega)
            cmd.ExecuteNonQuery()

            Using r = cmd.ExecuteReader()

                If r.Read() Then
                    If r.HasRows Then
                        BeStock.Cantidad = IIf(IsDBNull(r.Item("Cantidad")), 0, r.Item("Cantidad"))
                        BeStock.Peso = IIf(IsDBNull(r.Item("Peso")), 0, r.Item("Peso"))
                    End If
                End If

            End Using

            lTransaction.Commit()

        Catch ex As SqlException
            If Not lTransaction Is Nothing Then
                lTransaction.Rollback()
            End If
            Throw ex
        End Try

    End Sub

    '#CKFK20230522 Creé esta función para obtener el inventario de un producto.
    Public Shared Sub GetStockByIdProducto(ByRef pObjS As clsBeStock)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand


        Try

            Const sp As String = "SELECT SUM(cantidad) Cantidad,
                                         SUM(peso) Peso
                                  FROM stock INNER JOIN 
                                       producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN 
                                       producto ON producto.IdProducto = producto_bodega.IdProducto
                                  WHERE producto.IdProducto = @IdProducto"

            cmd.CommandType = CommandType.Text

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            cmd = New SqlCommand(sp, lConnection, lTransaction)
            cmd.Parameters.AddWithValue("@IdProducto", pObjS.Producto.IdProducto)
            cmd.ExecuteNonQuery()

            Using r = cmd.ExecuteReader()

                If r.Read() Then
                    If r.HasRows Then
                        pObjS.Cantidad = IIf(IsDBNull(r.Item("Cantidad")), 0, r.Item("Cantidad"))
                        pObjS.Peso = IIf(IsDBNull(r.Item("Peso")), 0, r.Item("Peso"))
                    End If
                End If

            End Using

            lTransaction.Commit()

        Catch ex As SqlException
            If Not lTransaction Is Nothing Then
                lTransaction.Rollback()
            End If
            Throw ex
        End Try

    End Sub

    Public Shared Sub GetExistenciaByIdProductoBodegaReport(ByRef BeStock As clsBeStock)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim BeProducto As clsBeProducto = clsLnProducto.Get_Single_BeProducto_By_IdProductoBodega(BeStock.IdProductoBodega)
            Dim cmd As New SqlCommand

            Const sp As String = "GetCantidadPesoByProductoBodega " &
                                   " @IdProductoBodega, " &
                                   " @IdPresentacion, " &
                                   " @IdUnidadMedida, " &
                                   " @IdProductoEstado "

            cmd.CommandType = CommandType.StoredProcedure

            cmd = New SqlCommand(sp, lConnection)
            lConnection.Open()

            cmd.Parameters.AddWithValue("@IdProductoBodega", BeStock.IdProductoBodega)

            If Not BeStock.Presentacion Is Nothing Then
                cmd.Parameters.AddWithValue("@IdPresentacion", BeStock.Presentacion.IdPresentacion)
            Else
                cmd.Parameters.AddWithValue("@IdPresentacion", DBNull.Value)
            End If

            cmd.Parameters.AddWithValue("@IdUnidadMedida", BeStock.IdUnidadMedida)
            cmd.Parameters.AddWithValue("@IdProductoEstado", BeStock.ProductoEstado.IdEstado)

            cmd.Parameters.AddWithValue("@CantidadEnStock", BeStock.Cantidad)
            cmd.Parameters.AddWithValue("@PesoEnStock", BeStock.Peso)
            cmd.Parameters("@CantidadEnStock").Direction = ParameterDirection.Output
            cmd.Parameters("@PesoEnStock").Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()

            Using r = cmd.ExecuteReader()

                If r.Read() Then
                    ' Get the first and second field frm the reader'
                    BeStock.Cantidad = r.Item("CantidadEnStock")
                    BeStock.Peso = r.Item("PesoEnStock")
                End If

            End Using

            lTransaction.Commit()

        Catch ex As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function lStockSpecifico(ByRef pBeStockRes As clsBeStock_res, ByRef pBeProductoOutput As clsBeProducto, ByVal DiasVencimiento As Integer) As List(Of clsBeStock)

        lStockSpecifico = Nothing

        Try

            Dim vBeStock As New clsBeStock
            Dim lBeStock As New List(Of clsBeStock)

            Dim vBeProducto As clsBeProducto = clsLnProducto.Get_Single_BeProducto_By_IdProductoBodega(pBeStockRes.IdProductoBodega)
            pBeProductoOutput = vBeProducto

            Dim vSQL As String = " SELECT * FROM STOCK INNER JOIN " &
                                 " PRODUCTO_BODEGA ON STOCK.IDPRODUCTOBODEGA = PRODUCTO_BODEGA.IDPRODUCTOBODEGA  " &
                                 " WHERE PRODUCTO_BODEGA.IDPRODUCTOBODEGA=@IdProductoBodega " &
                                 " And STOCK.IdPresentacion=@IdPresentacion " &
                                 " And STOCK.IdUnidadMedida =@IdUnidadMedida " &
                                 " And STOCK.IdProductoEstado=@IdProductoEstado "

            If vBeProducto.Control_lote Then
                vSQL += " And STOCK.lote=@Lote "
            End If

            If vBeProducto.Capturar_aniada AndAlso pBeStockRes.añada <> 0 Then
                vSQL += " And STOCK.añada=@añada"
            End If

            If vBeProducto.Control_vencimiento Then
                vSQL += " And STOCK.fecha_vence=@fecha_vence"
            End If

            If vBeProducto.Fechamanufactura Then
                vSQL += " And STOCK.Fecha_Manufactura=@Fecha_Manufactura"
            End If

            If DiasVencimiento <> 0 Then
                vSQL += " And DATEDIFF (DAY,GETDATE(),STOCK.fecha_vence) >=@DiasVencimientoCliente "
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}

                    lConnection.Open()

                    lCommand.Parameters.AddWithValue("@IdProductoBodega", pBeStockRes.IdProductoBodega)
                    lCommand.Parameters.AddWithValue("@IdPresentacion", pBeStockRes.IdPresentacion)
                    lCommand.Parameters.AddWithValue("@IdUnidadMedida", pBeStockRes.IdUnidadMedida)
                    lCommand.Parameters.AddWithValue("@IdProductoEstado", pBeStockRes.IdProductoEstado)

                    If vBeProducto.Control_lote Then
                        lCommand.Parameters.AddWithValue("@Lote", IIf(pBeStockRes.Lote Is Nothing, DBNull.Value, pBeStockRes.Lote))
                    End If

                    If vBeProducto.Capturar_aniada AndAlso pBeStockRes.añada <> 0 Then
                        lCommand.Parameters.AddWithValue("@añada", IIf(pBeStockRes.añada = 0, DBNull.Value, pBeStockRes.Lote))
                    End If

                    If vBeProducto.Control_vencimiento Then
                        lCommand.Parameters.AddWithValue("@fecha_vence", IIf(IsDBNull(pBeStockRes.Fecha_vence), DBNull.Value, pBeStockRes.Fecha_vence))
                    End If

                    If vBeProducto.Fechamanufactura Then
                        lCommand.Parameters.AddWithValue("@Fecha_Manufactura", IIf(IsDBNull(pBeStockRes.Fecha_manufactura), DBNull.Value, pBeStockRes.Fecha_manufactura))
                    End If

                    If DiasVencimiento <> 0 Then
                        lCommand.Parameters.AddWithValue("@DiasVencimientoCliente", DiasVencimiento)
                    End If

                    Using dr = lCommand.ExecuteReader()

                        While dr.Read()

                            vBeStock = New clsBeStock

                            With vBeStock

                                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                                .ProductoEstado.IdEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                                .Presentacion.IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                                .IdUbicacion_anterior = IIf(IsDBNull(dr.Item("IdUbicacion_anterior")), 0, dr.Item("IdUbicacion_anterior"))
                                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                                .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                                .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), 0, dr.Item("lic_plate"))
                                .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                                .Fecha_Ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), New Date(1900, 1, 1), dr.Item("fecha_vence"))
                                .Uds_lic_plate = IIf(IsDBNull(dr.Item("uds_lic_plate")), 0, dr.Item("uds_lic_plate"))
                                .No_bulto = IIf(IsDBNull(dr.Item("no_bulto")), 0, dr.Item("no_bulto"))
                                .Fecha_Manufactura = IIf(IsDBNull(dr.Item("fecha_manufactura")), Date.Now, dr.Item("fecha_manufactura"))
                                .Añada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                                .Peso = IIf(IsDBNull(dr.Item("Peso")), 0.0, dr.Item("Peso"))
                                .Temperatura = IIf(IsDBNull(dr.Item("temperatura")), 0.0, dr.Item("temperatura"))

                            End With

                            lBeStock.Add(vBeStock)

                        End While

                    End Using

                    If lConnection.State = ConnectionState.Open Then lConnection.Close()

                End Using

            End Using

            Return lBeStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared lTipoRotacion As New List(Of clsBeTipo_rotacion)


    Public Shared Function lStock(ByRef pBeStockRes As clsBeStock_res,
                                  ByRef pBeProductoOutput As clsBeProducto,
                                  ByVal DiasVencimiento As Integer,
                                  ByVal pBeConfigEnc As clsBeI_nav_config_enc,
                                  ByRef lConnection As SqlConnection,
                                  ByRef ltransaction As SqlTransaction,
                                  Optional ByVal pExcluirUbicacionPicking As Boolean = False,
                                  Optional ByVal Conmutar_Umbas_A_Presentacion As Boolean = False,
                                  Optional ByVal pTarea_Reabasto As Boolean = False,
                                  Optional ByVal pEs_Devolucion As Boolean = False) As List(Of clsBeStock)

        lStock = Nothing

        Try

            Dim vBeStock As New clsBeStock()
            Dim lBeStock As New List(Of clsBeStock)
            Dim IdxProductoEnMemoria As Integer = -1
            Dim vIdProductoBodega As Integer = 0

            If pBeStockRes Is Nothing Then
                '#EJC20231220
                Exit Function
            End If

            vIdProductoBodega = pBeStockRes.IdProductoBodega

            pBeProductoOutput = New clsBeProducto()
            pBeProductoOutput = lpBeProductoOutput.Find(Function(x) x.IdProductoBodega = vIdProductoBodega)

            If pBeProductoOutput Is Nothing Then

                pBeProductoOutput = New clsBeProducto()
                pBeProductoOutput = clsLnProducto.Get_Single_By_IdProductoBodega(pBeStockRes.IdProductoBodega,
                                                                                 lConnection,
                                                                                 ltransaction)

                If Not pBeProductoOutput Is Nothing Then
                    pBeProductoOutput.IdProductoBodega = pBeStockRes.IdProductoBodega
                    lpBeProductoOutput.Add(pBeProductoOutput)
                End If

            End If

            Dim BeBodega As New clsBeBodega
            BeBodega = lBeBodega.Find(Function(x) x.IdBodega = pBeConfigEnc.Idbodega)

            If BeBodega Is Nothing Then

                BeBodega = clsLnBodega.GetSingle_By_Idbodega(pBeConfigEnc.Idbodega,
                                                             lConnection,
                                                             ltransaction)

                If Not lBeBodega Is Nothing Then
                    lBeBodega.Add(BeBodega)
                End If

            End If

            '#EJC20180420: Mejora en consulta por ordenamiento lógico para picking.
            Dim vSQL As String = "SELECT stock.*,
					              bodega_ubicacion.indice_x, 
					              bodega_ubicacion.nivel, 
					              bodega_ubicacion.orientacion_pos, 
					              bodega_tramo.descripcion AS Nombre_Tramo,
                                  bodega_ubicacion.ubicacion_picking
					              FROM stock INNER JOIN
					              producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
					              bodega_ubicacion ON stock.IdUbicacion = bodega_ubicacion.IdUbicacion 
					              AND stock.IdUbicacion = bodega_ubicacion.IdUbicacion 
                                  AND stock.IdBodega = bodega_ubicacion.IdBodega
					              INNER JOIN
					              bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo 
                                  AND bodega_ubicacion.IdBodega = bodega_tramo.IdBodega
                                  AND bodega_ubicacion.IdSector = bodega_tramo.IdSector "

            If pBeStockRes.Control_Ultimo_Lote Then
                vSQL += " LEFT OUTER JOIN
						 trans_re_det_lote_num ON stock.IdProductoBodega = trans_re_det_lote_num.IdProductoBodega 
						 AND stock.lote = trans_re_det_lote_num.Lote "
            End If


            vSQL += " WHERE bodega_ubicacion.activo = 1 
                      and bodega_ubicacion.bloqueada = 0
                      and producto_bodega.idproductobodega=@idproductobodega                     
					  and stock.idunidadmedida =@idunidadmedida  "

            '#CKFK20240528 Agregué esta validación para cuando sea devolución a proveedor
            If Not pEs_Devolucion AndAlso pBeStockRes.IdUbicacionAbastecerCon = 0 Then
                vSQL += " and stock.idproductoestado=@idproductoestado "
            End If

            If Not BeBodega.Permitir_Decimales Then

                If pBeStockRes.IdPresentacion <> 0 OrElse Not pBeStockRes.Atributo_Variante_1 Is Nothing Then
                    If Not pBeStockRes.Atributo_Variante_1 Is Nothing Then
                        If pBeStockRes.Atributo_Variante_1 <> "" OrElse pBeStockRes.IdPresentacion <> 0 Then
                            vSQL += "and (stock.idpresentacion =@idpresentacion) "
                        Else
                            If (pBeConfigEnc.Explosion_Automatica OrElse pBeConfigEnc.Explosion_Automatica_Desde_Ubicacion_Picking _
                                OrElse pBeStockRes.IdPresentacion = 0) Then
                                vSQL += "and (stock.idpresentacion is null or stock.idpresentacion = 0) "
                            End If
                            If Not Conmutar_Umbas_A_Presentacion Then
                                vSQL += "and (stock.idpresentacion is null or stock.idpresentacion = 0) "
                            End If
                        End If
                    End If
                Else
                    '#EJC20220315:BYB, reservar UMBAS aunque solo tenga producto en PRES ¬¬'
                    If Not Conmutar_Umbas_A_Presentacion Then
                        '#EJC20200129:Parametrizar....
                        vSQL += "and (stock.idpresentacion is null or stock.idpresentacion =0) "
                    End If
                End If

            End If

            If DiasVencimiento <> 0 Then
                vSQL += " And DATEDIFF (DAY,GETDATE(),stock.fecha_vence) >=@DiasVencimientoCliente "
            End If

            '#EJC20220620:Temporal, averiguar si aquí vienen los días de vencimiento del cliente.
            Dim vMsgError As String = String.Format("DIAS_VENCIMIENTO_CLI: Días: {0} IdProductoBodega: {1}", DiasVencimiento, pBeStockRes.IdProductoBodega)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)

            If pBeStockRes.IdUbicacionAbastecerCon = 0 Then
                If Not BeBodega Is Nothing Then

                    If pBeProductoOutput.Control_vencimiento Then

                        '#CKFK20240528 Agregué esta validación para cuando sea devolución a proveedor  AndAlso Not pEs_Devolucion
                        If Not BeBodega.despachar_producto_vencido AndAlso Not pEs_Devolucion Then
                            vSQL += " And stock.fecha_vence > GETDATE() "
                        End If

                    End If

                End If
            End If

            '#EJC20190311_0948PM: Abastecer el pedido con IdUbicacion configurada en cliente.
            If pBeStockRes.IdUbicacionAbastecerCon = 0 Then
                '"#EJC20190312:Excluir el inventario en tránsito al momento de listar stock para reservar.
                vSQL += " AND stock.IdUbicacion NOT IN (SELECT bu.IdUbicacion
							   FROM bodega_ubicacion bu
							   WHERE (bu.ubicacion_despacho = 1) AND (bu.IdBodega = @IdBodega))"
            End If

            If pExcluirUbicacionPicking Then
                '"#EJC20190312:Excluir el inventario en tránsito al momento de litar stock para reservar.
                vSQL += " AND stock.IdUbicacion NOT IN (SELECT bu.IdUbicacion
							                           FROM bodega_ubicacion bu
							                           WHERE (bu.ubicacion_picking = 1) AND (bu.IdBodega = @IdBodega))"
            End If

            If pBeConfigEnc.Excluir_Recepcion_Picking Then
                '"#CKFK20250610:Excluir el inventario de la ubicacion de recepcion
                vSQL += " AND stock.IdUbicacion NOT IN (SELECT bu.IdUbicacion
							                           FROM bodega_ubicacion bu
							                           WHERE (bu.ubicacion_recepcion = 1) AND (bu.IdBodega = @IdBodega))"
            End If

            '#CKFK20230208 Excluir ubicaciones de reabasto
            If pTarea_Reabasto Then
                If pBeConfigEnc.Excluir_Ubicaciones_Reabasto Then
                    vSQL += " AND stock.IdUbicacion NOT IN (SELECT pr.IdUbicacion
                            FROM  producto_rellenado pr
                            WHERE (pr.IdBodega = @IdBodega) AND (pr.Activo = 1))"
                End If
            End If

            '#EJC20190313: Excluir el último lote despachado u obtener uno mayor
            If pBeStockRes.Control_Ultimo_Lote Then

                If IsNumeric(pBeStockRes.Ultimo_Lote) Then
                    vSQL += " And (trans_re_det_lote_num.lote_numerico >= @UltimoLote)"
                End If

            End If

            '#EJC20190311_0948PM: Abastecer el pedido con IdUbicacion configurada en cliente.
            If pBeStockRes.IdUbicacionAbastecerCon <> 0 Then
                vSQL += " and stock.idubicacion = @IdUbicacionAbastecerCon "
            End If

            '#EJC20220523: Evitar que se explosione producto desde los niveles superiores a nivel (1 PEJ.) o ubicaciones de picking.
            If Conmutar_Umbas_A_Presentacion OrElse pBeStockRes.IdPresentacion = 0 Then

                If Not pExcluirUbicacionPicking Then
                    If pBeConfigEnc.Explosion_Automatica_Desde_Ubicacion_Picking Then
                        vSQL += " and bodega_ubicacion.ubicacion_picking= 1 "
                    End If
                End If

                If pBeConfigEnc.Explosion_Automatica_Nivel_Max > 0 Then
                    vSQL += " and bodega_ubicacion.nivel=  " & pBeConfigEnc.Explosion_Automatica_Nivel_Max
                End If

            End If

            If BeBodega.Control_Talla_Color Then

                If Not pBeStockRes.IdProductoTallaColor = 0 Then
                    vSQL += " and stock.IdProductoTallaColor= @IdProductoTallaColor"
                End If

            End If

            '#EJC20200204: Mejora por nuevo tipo de rotación
            Dim IdTipoRotacion As Integer = 0
            Dim vIdxTipoRotacion As Integer = 0
            vIdxTipoRotacion = lTipoRotacion.FindIndex(Function(x) x.IdProductoBodega = vIdProductoBodega)
            Dim BeTipoRotacion As New clsBeTipo_rotacion()

            If vIdxTipoRotacion = -1 Then
                IdTipoRotacion = clsLnProducto.Get_Tipo_Rotacion_By_IdProductoBodega(vIdProductoBodega, lConnection, ltransaction)
                BeTipoRotacion.IdTipoRotacion = IdTipoRotacion
                BeTipoRotacion.IdProductoBodega = vIdProductoBodega
                BeTipoRotacion.Activo = True
                lTipoRotacion.Add(BeTipoRotacion.Clone())
            Else
                IdTipoRotacion = lTipoRotacion(vIdxTipoRotacion).IdTipoRotacion
            End If

            '#EJC20200204: Modifiqué el ordenamiento (quité nombre_tramo del orden)
            Select Case IdTipoRotacion
                Case 1 'FIFO                    
                    vSQL += " ORDER BY fecha_ingreso asc, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad"
                Case 2 'LIFO
                    vSQL += " ORDER BY fecha_ingreso desc,bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad"
                Case 3 'FEFO
                    vSQL += " ORDER BY fecha_vence, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad "
                    '#EJC202004: Este me lo inventé yo por la cagada del inventario inicial en Idealsa
                    'La idea es que saque el producto por ubicaciones,antes que por reglas de rotación
                    'Este ordenamiento forza a tomar producto de las ubicaciones 
                Case 4 'UPSR (Ubicación prioritaria sobre rotación)
                    vSQL += " ORDER BY indice_x,bodega_tramo.es_rack,bodega_ubicacion.IdTramo,nivel,orientacion_pos,cantidad"
                Case Else 'Default
                    vSQL += " ORDER BY fecha_ingreso asc, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad"

            End Select

            Using lCommand As New SqlCommand(vSQL, lConnection, ltransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdProductoBodega", pBeStockRes.IdProductoBodega)

                If Not pBeStockRes.Atributo_Variante_1 Is Nothing OrElse pBeStockRes.IdPresentacion >= 0 Then
                    lCommand.Parameters.AddWithValue("@IdPresentacion", pBeStockRes.IdPresentacion)
                End If

                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pBeStockRes.IdUnidadMedida)
                lCommand.Parameters.AddWithValue("@IdProductoEstado", pBeStockRes.IdProductoEstado)
                lCommand.Parameters.AddWithValue("@IdBodega", pBeConfigEnc.Idbodega)

                If DiasVencimiento <> 0 Then
                    lCommand.Parameters.AddWithValue("@DiasVencimientoCliente", DiasVencimiento)
                End If

                If pBeStockRes.Control_Ultimo_Lote AndAlso pBeStockRes.Ultimo_Lote <> "" Then
                    lCommand.Parameters.AddWithValue("@UltimoLote", Val(pBeStockRes.Ultimo_Lote))
                End If

                If pBeStockRes.IdUbicacionAbastecerCon <> 0 Then
                    lCommand.Parameters.AddWithValue("@IdUbicacionAbastecerCon", pBeStockRes.IdUbicacionAbastecerCon)
                End If

                If Not pBeStockRes.IdProductoTallaColor = 0 Then
                    lCommand.Parameters.AddWithValue("@IdProductoTallaColor", pBeStockRes.IdProductoTallaColor)
                End If

                Using dr = lCommand.ExecuteReader()

                    While dr.Read()

                        vBeStock = New clsBeStock

                        With vBeStock

                            .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                            .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                            .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                            .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                            .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado")) '#CKFK 20180109 07:17 AM Agregué ésta línea para que se llene el Id del estado del producto
                            .ProductoEstado.IdEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                            .Presentacion.IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                            .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                            .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                            .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                            .IdUbicacion_anterior = IIf(IsDBNull(dr.Item("IdUbicacion_anterior")), 0, dr.Item("IdUbicacion_anterior"))
                            .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                            .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                            .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                            .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                            .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                            .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                            .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), 0, dr.Item("lic_plate"))
                            .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                            .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                            .Fecha_Ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                            .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), New Date(1900, 1, 1), dr.Item("fecha_vence"))
                            .Uds_lic_plate = IIf(IsDBNull(dr.Item("uds_lic_plate")), 0, dr.Item("uds_lic_plate"))
                            .No_bulto = IIf(IsDBNull(dr.Item("no_bulto")), 0, dr.Item("no_bulto")) '#CKFK 20180405 Modifiqué la inicializacion cuando el campo es nulo por 0
                            .Fecha_Manufactura = IIf(IsDBNull(dr.Item("fecha_manufactura")), New Date(1900, 1, 1), dr.Item("fecha_manufactura"))
                            .Añada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                            .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                            .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                            .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                            .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                            .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                            .Peso = IIf(IsDBNull(dr.Item("Peso")), 0.0, dr.Item("Peso"))
                            .Temperatura = IIf(IsDBNull(dr.Item("temperatura")), 0.0, dr.Item("temperatura"))
                            .UbicacionPicking = IIf(IsDBNull(dr.Item("ubicacion_picking")), False, dr.Item("ubicacion_picking"))
                            .UbicacionNivel = IIf(IsDBNull(dr.Item("nivel")), 0, dr.Item("nivel"))
                            .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))

                        End With

                        lBeStock.Add(vBeStock)

                    End While

                End Using

            End Using

            Return lBeStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ' #AT 20211228 Copia de la función lstock para devolverla como un datatable
    Public Shared Function lStock_DT(ByRef pBeStockRes As clsBeStock_res,
                                     ByRef pBeProductoOutput As clsBeProducto,
                                     ByVal DiasVencimiento As Integer,
                                     ByVal pBeConfigEnc As clsBeI_nav_config_enc,
                                     ByRef lConnection As SqlConnection,
                                     ByRef ltransaction As SqlTransaction,
                                     Optional ByVal pExcluirUbicacionPicking As Boolean = False,
                                     Optional ByVal pRestringir As Boolean = False) As DataTable

        lStock_DT = Nothing

        Try

            Dim vBeStock As New clsBeStock
            Dim lBeStock As New List(Of clsBeStock)
            Dim IdxProductoEnMemoria As Integer = -1
            Dim vIdProductoBodega As Integer = 0
            Dim DTStock As New DataTable

            vIdProductoBodega = pBeStockRes.IdProductoBodega

            Dim BeBodega As New clsBeBodega
            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pBeStockRes.IdBodega,
                                                         lConnection,
                                                         ltransaction)

            '#EJC20180420: Mejora en consulta por ordenamiento lógico para picking.
            Dim vSQL As String = "SELECT producto.codigo,
		                            producto.nombre as producto,
		                            IIF(producto_presentacion.nombre = '' or producto_presentacion.nombre is null, '-', producto_presentacion.nombre)  as presentacion,
		                            unidad_medida.nombre as UMBas,
		                            stock.cantidad as Cant,
                                    stock.IdUbicacion,
		                            fecha_vence as FechaVence,
		                            IIF(lic_plate = '' or lic_plate is null, '-', lic_plate) as LicPlate,
		                            IIF(lote = '' or lote is null, '-', lote) as lote,
		                            stock.Peso Peso,
		                            producto_estado.nombre as Estado,
		                            'No' as Despachar,
                                    IdProductoEstado,
                                    IIF(stock.IdPresentacion is null, 0, stock.IdPresentacion) as IdPresentacion,
		                            stock.IdProductoBodega, 
		                            stock.IdBodega,
                                    bodega_ubicacion.indice_x, 
		                            bodega_ubicacion.nivel, 
		                            bodega_ubicacion.orientacion_pos,
		                            stock.IdPropietarioBodega,	                             
		                            stock.IdUnidadMedida,
		                            0 IdUbicacion_anterior, 
		                            0 IdRecepcionEnc, 
		                            0 IdRecepcionDet, 
		                            0 IdPedidoEnc, 
		                            0 IdPickingEnc, 
		                            0 IdDespachoEnc, 
		                            '' serial, 
		                            '19000101' fecha_ingreso, 
		                            0 uds_lic_plate, 
		                            0 no_bulto, 
		                            '19000101' fecha_manufactura, 
		                            0 añada,
		                            stock.activo,
		                            0 temperatura, 
		                            '' atributo_variante_1, 
		                            0 pallet_no_estandar,
		                            bodega_ubicacion.ubicacion_picking,
		                            bodega_tramo.es_rack,
		                            bodega_ubicacion.IdTramo, IdStock,                                  
                                    dbo.Nombre_Completo_Ubicacion(stock.IdUbicacion, stock.IdBodega) as NombUbic,
                                    IIF(ptc.IdProductoTallaColor = '' or ptc.IdProductoTallaColor is null, '-', ptc.IdProductoTallaColor) as IdProductoTallaColor, 
                                    IIF(t.Codigo = '' or t.Codigo is null, '-', t.Codigo)  as Codigo_Talla,
                                    IIF(t.Nombre = '' or t.Nombre is null, '-', t.Nombre) as Nombre_Talla,
                                    IIF(c.Codigo = '' or c.Codigo is null, '-', c.Codigo) as Codigo_Color,
                                    IIF(c.Nombre = '' or c.Nombre is null, '-', c.Nombre) as  Nombre_Color
					FROM stock INNER JOIN
		            producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
		            producto on producto.IdProducto = producto_bodega.IdProducto AND
		            producto_bodega.IdBodega = stock.IdBodega INNER JOIN
		            bodega_ubicacion ON stock.IdUbicacion = bodega_ubicacion.IdUbicacion 
		            AND stock.IdUbicacion = bodega_ubicacion.IdUbicacion 
                    AND stock.IdBodega = bodega_ubicacion.IdBodega INNER JOIN
		            bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo 
                    AND bodega_ubicacion.IdBodega = bodega_tramo.IdBodega
                    AND bodega_ubicacion.IdSector = bodega_tramo.IdSector inner join
		            producto_estado on producto_estado.IdEstado = stock.IdProductoEstado inner join
		            unidad_medida on unidad_medida.IdUnidadMedida = producto.IdUnidadMedidaBasica left outer join
		            producto_presentacion on producto_presentacion.IdProducto = producto.IdProducto and stock.IdPresentacion = producto_presentacion.IdPresentacion
                    LEFT JOIN producto_talla_color AS ptc ON ptc.IdProductoTallaColor = stock.IdProductoTallaColor
                    LEFT JOIN talla AS t on t.IdTalla = ptc.IdTalla
                    LEFT JOIN color AS c on c.IdColor = ptc.IdColor "

            If pBeStockRes.Control_Ultimo_Lote Then
                vSQL += " LEFT OUTER JOIN
						 trans_re_det_lote_num ON stock.IdProductoBodega = trans_re_det_lote_num.IdProductoBodega 
						 AND stock.lote = trans_re_det_lote_num.Lote "
            End If


            vSQL += " WHERE bodega_ubicacion.Activo = 1 
                      and bodega_ubicacion.bloqueada = 0
                      AND producto_bodega.idproductobodega=@idproductobodega                     
					  AND stock.idunidadmedida =@idunidadmedida 
					  AND stock.idproductoestado=@idproductoestado "

            If Not BeBodega Is Nothing Then
                '#EJC202302231735: Parametricé, Permitir_Reemplazo_Picking_Misma_Licencia 
                If Not BeBodega.Permitir_Reemplazo_Picking_Misma_Licencia Then
                    '#AT20230106 Si la licencia no es vacia entonces obtiene todo el stock disponible que tenga diferente licencia
                    If pBeStockRes.Lic_plate <> "" Then
                        vSQL += " AND (stock.lic_plate IS NULL OR stock.lic_plate <> @lic_plate) "
                    End If
                End If
            End If

            '#CKFK20220722 Agregué bandera para restringir el inventario a listar
            If pRestringir Then
                '#EJC202207017A
                If BeBodega.Restringir_Lote_En_Reemplazo Then
                    vSQL += " and (stock.lote = @Lote or lote is null) "
                End If

                '#EJC202207017B
                If BeBodega.Restringir_Vencimiento_En_Reemplazo Then
                    vSQL += " and stock.fecha_vence >= @fechavence "
                End If
            End If

            If pBeProductoOutput.Control_vencimiento Then
                If Not BeBodega.Restringir_Vencimiento_En_Reemplazo Then
                    vSQL += " and fecha_vence BETWEEN GETDATE() AND DATEADD(day,  @DiasMaximoVencReemp, @FechaVence) "
                End If

            End If

            If pBeStockRes.IdPresentacion <> 0 Then
                If Not pBeStockRes.Atributo_Variante_1 Is Nothing Then
                    If pBeStockRes.Atributo_Variante_1 <> "" OrElse pBeStockRes.IdPresentacion <> 0 Then
                        vSQL += "and (stock.idpresentacion =@idpresentacion) "
                    Else
                        vSQL += "and (stock.idpresentacion is null or stock.idpresentacion =@idpresentacion) "
                    End If
                End If
            Else
                '#EJC20200129:Parametrizar....
                vSQL += "and (stock.idpresentacion is null or stock.idpresentacion =0) "
            End If

            If DiasVencimiento <> 0 Then
                vSQL += " And DATEDIFF (DAY,GETDATE(),stock.fecha_vence) >=@DiasVencimientoCliente "
            End If

            '#EJC20220510: despachar_producto_vencido BYB
            If Not BeBodega Is Nothing Then
                '#GT0802023: si producto tiene control vence, vaidamos que se pueda despachar vencido
                If pBeProductoOutput.Control_vencimiento Then
                    If Not BeBodega.despachar_producto_vencido Then
                        vSQL += " And stock.fecha_vence > GETDATE()"
                    End If

                End If

            End If

            '"#EJC20190312:Excluir el inventario en tránsito al momento de litar stock para reservar.
            vSQL += " AND stock.IdUbicacion NOT IN (SELECT IdUbicacion
							   FROM bodega_ubicacion AS bodega_ubicacion_1
							   WHERE (ubicacion_despacho = 1))"

            If pExcluirUbicacionPicking Then
                '"#EJC20190312:Excluir el inventario en tránsito al momento de litar stock para reservar.
                vSQL += " AND stock.IdUbicacion NOT IN (SELECT IdUbicacion
							   FROM bodega_ubicacion AS bodega_ubicacion_1
							   WHERE (ubicacion_picking = 1))"
            End If

            '#EJC20190313: Excluir el último lote despachado u obtener uno mayor
            If pBeStockRes.Control_Ultimo_Lote Then

                If IsNumeric(pBeStockRes.Ultimo_Lote) Then
                    vSQL += " And (trans_re_det_lote_num.lote_numerico >= @UltimoLote)"
                End If

            End If

            '#EJC20190311_0948PM: Abastecer el pedido con IdUbicacion configurada en cliente.
            If pBeStockRes.IdUbicacionAbastecerCon <> 0 Then
                vSQL += " and stock.idubicacion = @IdUbicacionAbastecerCon "
            End If

            '#CKFK20220711 Consultarle a Erik y a Anderly sobre esta condición
            ''#EJC20220523: Evitar que se explosione producto desde los niveles superiores a nivel (1 PEJ.) o ubicaciones de picking.
            'If Conmutar_Umbas_A_Presentacion OrElse pBeStockRes.IdPresentacion = 0 Then

            '    If pBeConfigEnc.Explosion_Automatica_Desde_Ubicacion_Picking Then
            '        vSQL += " and bodega_ubicacion.ubicacion_picking= 1 "
            '    End If

            '    If pBeConfigEnc.Explosion_Automatica_Nivel_Max > 0 Then
            '        vSQL += " and bodega_ubicacion.nivel=  " & pBeConfigEnc.Explosion_Automatica_Nivel_Max
            '    End If

            'End If

            '#EJC20200204: Mejora por nuevo tipo de rotación
            Dim IdTipoRotacion As Integer = 0
            Dim vIdxTipoRotacion As Integer = 0
            vIdxTipoRotacion = lTipoRotacion.FindIndex(Function(x) x.IdProductoBodega = vIdProductoBodega)
            Dim BeTipoRotacion As New clsBeTipo_rotacion

            If vIdxTipoRotacion = -1 Then
                IdTipoRotacion = clsLnProducto.Get_Tipo_Rotacion_By_IdProductoBodega(vIdProductoBodega, lConnection, ltransaction)
                BeTipoRotacion.IdTipoRotacion = IdTipoRotacion
                BeTipoRotacion.IdProductoBodega = vIdProductoBodega
                BeTipoRotacion.Activo = True
                lTipoRotacion.Add(BeTipoRotacion.Clone())
            Else
                IdTipoRotacion = lTipoRotacion(vIdxTipoRotacion).IdTipoRotacion
            End If

            '#EJC20200204: Modifiqué el ordenamiento (quité nombre_tramo del orden)
            Select Case IdTipoRotacion
                Case 1 'FIFO                    
                    vSQL += "ORDER BY fecha_ingreso asc, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad"
                Case 2 'LIFO
                    vSQL += "ORDER BY fecha_ingreso desc,bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad"
                Case 3 'FEFO
                    vSQL += "ORDER BY fecha_vence, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,stock.lote,cantidad"
                    '#EJC202004: Este me lo inventé yo por la cagada del inventario inicial en Idealsa
                    'La idea es que saque el producto por ubicaciones,antes que por reglas de rotación
                    'Este ordenamiento forza a tomar producto de las ubicaciones 
                Case 4 'UPSR (Ubicación prioritaria sobre rotación)
                    vSQL += "ORDER BY indice_x,bodega_tramo.es_rack,bodega_ubicacion.IdTramo,nivel,orientacion_pos,cantidad"
                Case Else 'Default
                    vSQL += "ORDER BY fecha_ingreso asc, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad"

            End Select

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = ltransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pBeStockRes.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@fechavence", pBeStockRes.Fecha_vence)
                lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pBeStockRes.Lote)

                If Not pBeStockRes.Atributo_Variante_1 Is Nothing Then
                    If pBeStockRes.IdPresentacion <> 0 Then
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pBeStockRes.IdPresentacion)
                    Else

                    End If
                End If

                lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pBeStockRes.IdUnidadMedida)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pBeStockRes.IdProductoEstado)

                If DiasVencimiento <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@DiasVencimientoCliente", DiasVencimiento)
                End If

                If pBeStockRes.Control_Ultimo_Lote AndAlso pBeStockRes.Ultimo_Lote <> "" Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@UltimoLote", Val(pBeStockRes.Ultimo_Lote))
                End If

                If pBeProductoOutput.Control_vencimiento Then
                    If Not BeBodega.Restringir_Vencimiento_En_Reemplazo Then
                        lDTA.SelectCommand.Parameters.AddWithValue("@DiasMaximoVencReemp", BeBodega.Dias_Maximo_Vencimiento_Reemplazo)
                    End If
                End If

                If pBeStockRes.Lic_plate <> "" Then
                    '#CKFK202300205 Le quité el Val a la licencia
                    lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pBeStockRes.Lic_plate)
                End If

                Dim lDataTable As New DataTable("Stock_Especifico")
                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK20220712 Cree una coopia de la función lStock_DT para devolverla para el reabastecimiento manual
    Public Shared Function lStock_Reabasto_DT(ByRef pBeStockRes As clsBeStock_res,
                                              ByRef pBeProductoOutput As clsBeProducto,
                                              ByVal DiasVencimiento As Integer,
                                              ByVal pBeConfigEnc As clsBeI_nav_config_enc,
                                              ByRef lConnection As SqlConnection,
                                              ByRef ltransaction As SqlTransaction,
                                              Optional ByVal pExcluirUbicacionPicking As Boolean = False) As DataTable

        lStock_Reabasto_DT = Nothing

        Try

            Dim vBeStock As New clsBeStock
            Dim lBeStock As New List(Of clsBeStock)
            Dim IdxProductoEnMemoria As Integer = -1
            Dim vIdProductoBodega As Integer = 0
            Dim DTStock As New DataTable

            vIdProductoBodega = pBeStockRes.IdProductoBodega

            Dim BeBodega As New clsBeBodega
            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pBeStockRes.IdBodega,
                                                         lConnection,
                                                         ltransaction)
            Dim top_reabasto As Integer = BeBodega.Top_Reabastecimiento_Manual

            '#EJC20180420: Mejora en consulta por ordenamiento lógico para picking.
            Dim vSQL As String = "SELECT TOP (@top_reabasto) producto.codigo,
		                            producto.nombre,
		                            producto_presentacion.nombre as presentacion,
		                            unidad_medida.nombre as UMBas,
		                            stock.cantidad as Cant_UMBas,
                                    stock.IdUbicacion,
		                            fecha_vence as FechaVence,
		                            IIF(lic_plate = '' or lic_plate is null, '-', lic_plate) as Licencia,
		                            IIF(lote = '' or lote is null, '-', lote) as Lote,
		                            stock.Peso Peso,
		                            producto_estado.nombre as EstadoProducto,
		                            'No' as Despachar,
                                    IdProductoEstado,
                                    IIF(stock.IdPresentacion is null, 0, stock.IdPresentacion) as IdPresentacion,
		                            stock.IdProductoBodega, 
		                            stock.IdBodega,
                                    bodega_ubicacion.indice_x, 
		                            bodega_ubicacion.nivel, 
		                            bodega_ubicacion.orientacion_pos,
		                            stock.IdPropietarioBodega,	                             
		                            stock.IdUnidadMedida,
		                            0 IdUbicacion_anterior, 
		                            0 IdRecepcionEnc, 
		                            0 IdRecepcionDet, 
		                            0 IdPedidoEnc, 
		                            0 IdPickingEnc, 
		                            0 IdDespachoEnc, 
		                            '' serial, 
		                            '19000101' fecha_ingreso, 
		                            0 uds_lic_plate, 
		                            0 no_bulto, 
		                            '19000101' fecha_manufactura, 
		                            0 añada,
		                            stock.activo,
		                            0 temperatura, 
		                            '' atributo_variante_1, 
		                            0 pallet_no_estandar,
		                            bodega_ubicacion.ubicacion_picking,
		                            bodega_tramo.es_rack,
		                            bodega_ubicacion.IdTramo, IdStock,                                  
                                    dbo.Nombre_Completo_Ubicacion(stock.IdUbicacion, stock.IdBodega) as UbicacionActual,
                                    ISNULL(producto_presentacion.factor,0) Factor,
                                    ISNULL(stock.IdProductoTallaColor, 0) IdProductoTallaColor,
                                    ISNULL(t.Codigo, '') AS Codigo_Talla,
                                    ISNULL(t.Nombre, '') AS Nombre_Talla,
                                    ISNULL(c.Codigo, '') AS Codigo_Color,
                                    ISNULL(c.Nombre, '') AS Nombre_Color
					FROM stock INNER JOIN
		            producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
		            producto on producto.IdProducto = producto_bodega.IdProducto AND
		            producto_bodega.IdBodega = stock.IdBodega INNER JOIN
		            bodega_ubicacion ON stock.IdUbicacion = bodega_ubicacion.IdUbicacion 
		            AND stock.IdUbicacion = bodega_ubicacion.IdUbicacion 
                    AND stock.IdBodega = bodega_ubicacion.IdBodega INNER JOIN
		            bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo 
                    AND bodega_ubicacion.IdBodega = bodega_tramo.IdBodega
                    AND bodega_ubicacion.IdSector = bodega_tramo.IdSector inner join
		            producto_estado on producto_estado.IdEstado = stock.IdProductoEstado inner join
		            unidad_medida on unidad_medida.IdUnidadMedida = producto.IdUnidadMedidaBasica left outer join
		            producto_presentacion on producto_presentacion.IdProducto = producto.IdProducto and stock.IdPresentacion = producto_presentacion.IdPresentacion
                    left join producto_talla_color ptc on ptc.IdProductoTallaColor = stock.IdProductoTallaColor
                    left join talla t on t.IdTalla = ptc.IdTalla
                    left join color c on c.IdColor = ptc.IdColor "

            If pBeStockRes.Control_Ultimo_Lote Then
                vSQL += " LEFT OUTER JOIN
						 trans_re_det_lote_num ON stock.IdProductoBodega = trans_re_det_lote_num.IdProductoBodega 
						 AND stock.lote = trans_re_det_lote_num.Lote "
            End If


            vSQL += " WHERE bodega_ubicacion.Activo = 1 
                      and bodega_ubicacion.bloqueada = 0
                      AND producto_bodega.idproductobodega=@idproductobodega 
                      AND producto_estado.utilizable = 1 "

            If DiasVencimiento <> 0 Then
                vSQL += " And DATEDIFF (DAY,GETDATE(),stock.fecha_vence) >=@DiasVencimientoCliente "
            End If

            '#EJC20220510: despachar_producto_vencido BYB
            If Not BeBodega Is Nothing Then

                If Not BeBodega.despachar_producto_vencido Then
                    vSQL += " And stock.fecha_vence > GETDATE()"
                End If

            End If

            '"#EJC20190312:Excluir el inventario en tránsito al momento de listar stock para reservar.
            vSQL += " AND stock.IdUbicacion NOT IN (SELECT IdUbicacion
							   FROM bodega_ubicacion AS bodega_ubicacion_1
							   WHERE (ubicacion_despacho = 1))"

            If pExcluirUbicacionPicking Then
                '"#EJC20190312:Excluir el inventario en tránsito al momento de litar stock para reservar.
                vSQL += " AND stock.IdUbicacion NOT IN (SELECT IdUbicacion
							   FROM bodega_ubicacion AS bodega_ubicacion_1
							   WHERE (ubicacion_picking = 1))"
            End If

            '#EJC20190313: Excluir el último lote despachado u obtener uno mayor
            If pBeStockRes.Control_Ultimo_Lote Then

                If IsNumeric(pBeStockRes.Ultimo_Lote) Then
                    vSQL += " And (trans_re_det_lote_num.lote_numerico >= @UltimoLote)"
                End If

            End If

            '#EJC20190311_0948PM: Abastecer el pedido con IdUbicacion configurada en cliente.
            If pBeStockRes.IdUbicacionAbastecerCon <> 0 Then
                vSQL += " and stock.idubicacion = @IdUbicacionAbastecerCon "
            End If

            '#EJC20200204: Mejora por nuevo tipo de rotación
            Dim IdTipoRotacion As Integer = 0
            Dim vIdxTipoRotacion As Integer = 0
            vIdxTipoRotacion = lTipoRotacion.FindIndex(Function(x) x.IdProductoBodega = vIdProductoBodega)
            Dim BeTipoRotacion As New clsBeTipo_rotacion

            If vIdxTipoRotacion = -1 Then
                IdTipoRotacion = clsLnProducto.Get_Tipo_Rotacion_By_IdProductoBodega(vIdProductoBodega, lConnection, ltransaction)
                BeTipoRotacion.IdTipoRotacion = IdTipoRotacion
                BeTipoRotacion.IdProductoBodega = vIdProductoBodega
                BeTipoRotacion.Activo = True
                lTipoRotacion.Add(BeTipoRotacion.Clone())
            Else
                IdTipoRotacion = lTipoRotacion(vIdxTipoRotacion).IdTipoRotacion
            End If

            '#EJC20200204: Modifiqué el ordenamiento (quité nombre_tramo del orden)
            Select Case IdTipoRotacion
                Case 1 'FIFO                    
                    vSQL += "ORDER BY fecha_ingreso asc, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad"
                Case 2 'LIFO
                    vSQL += "ORDER BY fecha_ingreso desc,bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad"
                Case 3 'FEFO
                    vSQL += "ORDER BY fecha_vence, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos, stock.lote, cantidad"
                    '#EJC202004: Este me lo inventé yo por la cagada del inventario inicial en Idealsa
                    'La idea es que saque el producto por ubicaciones,antes que por reglas de rotación
                    'Este ordenamiento forza a tomar producto de las ubicaciones 
                Case 4 'UPSR (Ubicación prioritaria sobre rotación)
                    vSQL += "ORDER BY indice_x,bodega_tramo.es_rack,bodega_ubicacion.IdTramo,nivel,orientacion_pos,cantidad"
                Case Else 'Default
                    vSQL += "ORDER BY fecha_ingreso asc, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad"

            End Select

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = ltransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pBeStockRes.IdProductoBodega)
                'lDTA.SelectCommand.Parameters.AddWithValue("@fechavence", pBeStockRes.Fecha_vence)
                'lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pBeStockRes.Lote)

                'If Not pBeStockRes.Atributo_Variante_1 Is Nothing Then
                '    If pBeStockRes.IdPresentacion <> 0 Then
                '        lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pBeStockRes.IdPresentacion)
                '    End If
                'End If

                'lDTA.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", pBeStockRes.IdUnidadMedida)
                'lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pBeStockRes.IdProductoEstado)

                If DiasVencimiento <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@DiasVencimientoCliente", DiasVencimiento)
                End If

                lDTA.SelectCommand.Parameters.AddWithValue("@top_reabasto", top_reabasto)

                If pBeStockRes.Control_Ultimo_Lote AndAlso pBeStockRes.Ultimo_Lote <> "" Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@UltimoLote", Val(pBeStockRes.Ultimo_Lote))
                End If

                Dim lDataTable As New DataTable("Stock_Especifico")
                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ' #AT 20220103 Devuelve el stock disponible restando el stock reservado para el reemplazo en la HH
    Public Shared Function Get_Stock_Disponible_Rem(ByRef pBeStockRes As clsBeStock_res,
                                                    ByRef pBeProductoOutput As clsBeProducto,
                                                    ByVal DiasVencimiento As Integer,
                                                    ByVal pBeConfigEnc As clsBeI_nav_config_enc,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef ltransaction As SqlTransaction,
                                                    Optional ByVal pExcluirUbicacionPicking As Boolean = False) As DataTable
        Try
            Dim DTDatos As DataTable
            Dim RDatos As DataTable = New DataTable
            Dim vCantidadReservada As Double
            Dim IdStock As Integer

            DTDatos = lStock_DT(pBeStockRes,
                                pBeProductoOutput,
                                DiasVencimiento,
                                pBeConfigEnc,
                                lConnection,
                                ltransaction,
                                False,
                                True)

            '#CKFK20220722 Agregué esto para 
            If DTDatos.Rows.Count = 0 Then

                DTDatos.Dispose()

                DTDatos = lStock_DT(pBeStockRes,
                                    pBeProductoOutput,
                                    DiasVencimiento,
                                    pBeConfigEnc,
                                    lConnection,
                                    ltransaction,
                                    False)

            End If

            RDatos = DTDatos.Clone

            If DTDatos.Rows.Count > 0 Then

                For Each lrow As DataRow In DTDatos.Rows

                    If lrow("IdStock") IsNot DBNull.Value AndAlso lrow("IdStock") IsNot Nothing Then

                        IdStock = CType(lrow("IdStock"), Integer)

                        vCantidadReservada = clsLnStock_res.Get_Cantidad_ReservadaUMBas_By_IdStock(IdStock,
                                                                                                    False,
                                                                                                    lConnection,
                                                                                                    ltransaction)

                        If vCantidadReservada <> 0 Then

                            If lrow("Cant") IsNot DBNull.Value AndAlso lrow("Cant") IsNot Nothing Then
                                lrow("Cant") -= vCantidadReservada
                            End If

                        End If
                    End If
                Next

            End If

            Dim resultado = (From drs In DTDatos
                             Where drs("Cant") > 0
                             Group drs By keys = New With {Key .codigo = drs("codigo"),
                                                          Key .producto = drs("producto"),
                                                          Key .presentacion = drs("presentacion"),
                                                          Key .UMBas = drs("UMBas"),
                                                          Key .IdUbicacion = drs("IdUbicacion"),
                                                          Key .FechaVence = drs("FechaVence"),
                                                          Key .LicPlate = drs("LicPlate"),
                                                          Key .lote = drs("lote"),
                                                          Key .Estado = drs("Estado"),
                                                          Key .IdProductoEstado = drs("IdProductoEstado"),
                                                          Key .IdPresentacion = drs("IdPresentacion"),
                                                          Key .IdProductoBodega = drs("IdProductoBodega"),
                                                          Key .IdBodega = drs("IdBodega"),
                                                          Key .indice_x = drs("indice_x"),
                                                          Key .nivel = drs("nivel"),
                                                          Key .orientacion_pos = drs("orientacion_pos"),
                                                          Key .IdPropietarioBodega = drs("IdPropietarioBodega"),
                                                          Key .IdUnidadMedida = drs("IdUnidadMedida"),
                                                          Key .activo = drs("activo"),
                                                          Key .ubicacion_picking = drs("ubicacion_picking"),
                                                          Key .es_rack = drs("es_rack"),
                                                          Key .IdTramo = drs("IdTramo"),
                                                          Key .NombUbic = drs("NombUbic"),
                                                          Key .IdProductoTallaColor = drs("IdProductoTallaColor"),
                                                          Key .Codigo_Talla = drs("Codigo_Talla"),
                                                          Key .Nombre_Talla = drs("Nombre_Talla"),
                                                          Key .Codigo_Color = drs("Codigo_Color"),
                                                          Key .Nombre_Color = drs("Nombre_Color")}
                            Into Group Select New With {Key .codigo = keys.codigo,
                                                        Key .producto = keys.producto,
                                                        Key .presentacion = keys.presentacion,
                                                        Key .UMBas = keys.UMBas,
                                                        Key .Cant = Group.Sum(Function(x) x.Field(Of Double)("Cant")),
                                                        Key .IdUbicacion = keys.IdUbicacion,
                                                        Key .FechaVence = keys.FechaVence,
                                                        Key .LicPlate = keys.LicPlate,
                                                        Key .lote = keys.lote,
                                                        Key .Peso = Group.Sum(Function(x) x.Field(Of Double)("Peso")),
                                                        Key .Estado = keys.Estado,
                                                        Key .IdProductoEstado = keys.IdProductoEstado,
                                                        Key .IdPresentacion = keys.IdPresentacion,
                                                        Key .IdProductoBodega = keys.IdProductoBodega,
                                                        Key .IdBodega = keys.IdBodega,
                                                        Key .indice_x = keys.indice_x,
                                                        Key .nivel = keys.nivel,
                                                        Key .orientacion_pos = keys.orientacion_pos,
                                                        Key .IdPropietarioBodega = keys.IdPropietarioBodega,
                                                        Key .IdUnidadMedida = keys.IdUnidadMedida,
                                                        Key .activo = keys.activo,
                                                        Key .ubicacion_picking = keys.ubicacion_picking,
                                                        Key .es_rack = keys.es_rack,
                                                        Key .IdTramo = keys.IdTramo,
                                                        Key .NombUbic = keys.NombUbic,
                                                        Key .IdProductoTallaColor = keys.IdProductoTallaColor,
                                                        Key .Codigo_Talla = keys.Codigo_Talla,
                                                        Key .Nombre_Talla = keys.Nombre_Talla,
                                                        Key .Codigo_Color = keys.Codigo_Color,
                                                        Key .Nombre_Color = keys.Nombre_Color})

            If resultado IsNot Nothing Then
                Dim row As DataRow
                Dim Factor As Double

                For Each dr In resultado

                    row = RDatos.NewRow

                    row("Codigo") = dr.codigo
                    row("Producto") = dr.producto
                    row("Presentacion") = IIf(IsDBNull(dr.presentacion), "-", dr.presentacion)
                    row("UMBas") = dr.UMBas

                    If dr.IdPresentacion IsNot DBNull.Value OrElse dr.IdPresentacion IsNot Nothing Then
                        If dr.IdPresentacion <> 0 Then
                            Factor = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(dr.IdProductoBodega,
                                                                                               dr.IdPresentacion,
                                                                                               lConnection,
                                                                                               ltransaction)
                            row("Cant") = dr.Cant / Factor
                        Else
                            row("Cant") = dr.Cant
                        End If
                    Else
                        row("Cant") = dr.Cant
                    End If

                    row("IdUbicacion") = dr.IdUbicacion
                    row("FechaVence") = dr.FechaVence
                    row("LicPlate") = dr.LicPlate
                    row("lote") = dr.lote
                    row("Peso") = dr.Peso
                    row("Estado") = dr.Estado
                    row("Despachar") = "No"
                    row("IdProductoEstado") = dr.IdProductoEstado
                    row("IdPresentacion") = dr.IdPresentacion
                    row("IdProductoBodega") = dr.IdProductoBodega
                    row("IdBodega") = dr.IdBodega
                    row("indice_x") = dr.indice_x
                    row("nivel") = dr.nivel
                    row("orientacion_pos") = dr.orientacion_pos
                    row("IdPropietarioBodega") = dr.IdPropietarioBodega
                    row("IdUnidadMedida") = dr.IdUnidadMedida
                    row("IdUbicacion_anterior") = 0
                    row("IdRecepcionEnc") = 0
                    row("IdRecepcionDet") = 0
                    row("IdPedidoEnc") = 0
                    row("IdPickingEnc") = 0
                    row("IdDespachoEnc") = 0
                    row("serial") = ""
                    row("fecha_ingreso") = "19000101"
                    row("uds_lic_plate") = 0
                    row("no_bulto") = 0
                    row("fecha_manufactura") = "19000101"
                    row("añada") = 0
                    row("activo") = dr.activo
                    row("temperatura") = 0
                    row("atributo_variante_1") = ""
                    row("pallet_no_estandar") = 0
                    row("ubicacion_picking") = dr.ubicacion_picking
                    row("es_rack") = dr.es_rack
                    row("IdTramo") = dr.IdTramo
                    row("NombUbic") = dr.NombUbic
                    row("IdProductoTallaColor") = dr.IdProductoTallaColor
                    row("Codigo_Talla") = dr.Codigo_Talla
                    row("Nombre_Talla") = dr.Nombre_Talla
                    row("Codigo_Color") = dr.Codigo_Color
                    row("Nombre_Color") = dr.Nombre_Color

                    RDatos.Rows.Add(row)

                Next
            End If

            Return RDatos

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' #AT 20220103 Devuelve el stock sin agrupar en dependencia a los parametros enviados desde la hh
    Public Shared Function Get_Stock_Para_Reserva(ByRef pBeStockRes As clsBeStock_res,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef ltransaction As SqlTransaction,
                                                 Optional ByVal pExcluirUbicacionPicking As Boolean = False) As List(Of clsBeStock)
        Get_Stock_Para_Reserva = Nothing

        Try
            Dim vSql As String
            Dim vBeStock As New clsBeStock
            Dim lBeStock As New List(Of clsBeStock)
            Dim IdxProductoEnMemoria As Integer = -1
            Dim vIdProductoBodega As Integer = 0


            vSql = "SELECT stock.*,
					bodega_ubicacion.indice_x, 
					bodega_ubicacion.nivel, 
					bodega_ubicacion.orientacion_pos, 
					bodega_tramo.descripcion AS Nombre_Tramo
					FROM stock INNER JOIN
					producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
					bodega_ubicacion ON stock.IdUbicacion = bodega_ubicacion.IdUbicacion 
					AND stock.IdUbicacion = bodega_ubicacion.IdUbicacion 
                    AND stock.IdBodega = bodega_ubicacion.IdBodega
					INNER JOIN
					bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo 
                    AND bodega_ubicacion.IdBodega = bodega_tramo.IdBodega
                    AND bodega_ubicacion.IdSector = bodega_tramo.IdSector "

            If pBeStockRes.Control_Ultimo_Lote Then
                vSql += " LEFT OUTER JOIN
						 trans_re_det_lote_num ON stock.IdProductoBodega = trans_re_det_lote_num.IdProductoBodega 
						 AND stock.lote = trans_re_det_lote_num.Lote "
            End If


            vSql += " WHERE producto_bodega.idproductobodega=@idproductobodega                     
					and stock.idunidadmedida =@idunidadmedida 
					and stock.idproductoestado=@idproductoestado 
                    and stock.idubicacion=@idubicacion 
                    and cast(stock.fecha_vence as date) = @fechavence "

            If pBeStockRes.Lic_plate.Equals("-") Then
                vSql += " and (stock.lic_plate is null or stock.lic_plate = '')"
            Else
                vSql += " and (stock.lic_plate = @LicPlate) "
            End If

            If pBeStockRes.Lote.Equals("-") Then
                vSql += " and (stock.lote is null or stock.lote = '')"
            Else
                vSql += " and (stock.lote = @Lote) "
            End If

            If pBeStockRes.IdPresentacion <> 0 Then
                If Not pBeStockRes.Atributo_Variante_1 Is Nothing Then
                    If pBeStockRes.Atributo_Variante_1 <> "" OrElse pBeStockRes.IdPresentacion <> 0 Then
                        vSql += "and (stock.idpresentacion =@idpresentacion) "
                    Else
                        vSql += "and (stock.idpresentacion is null or stock.idpresentacion =@idpresentacion) "
                    End If
                End If
            Else
                '#EJC20200129:Parametrizar....
                vSql += "and (stock.idpresentacion is null or stock.idpresentacion =0) "
            End If

            Using lCommand As New SqlCommand(vSql, lConnection, ltransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdProductoBodega", pBeStockRes.IdProductoBodega)
                lCommand.Parameters.AddWithValue("@IdUbicacion", pBeStockRes.IdUbicacion)
                lCommand.Parameters.AddWithValue("@fechavence", pBeStockRes.Fecha_vence.Date)
                lCommand.Parameters.AddWithValue("@Lote", pBeStockRes.Lote)
                lCommand.Parameters.AddWithValue("@LicPlate", pBeStockRes.Lic_plate)

                If Not pBeStockRes.Atributo_Variante_1 Is Nothing Then
                    If pBeStockRes.IdPresentacion <> 0 Then
                        lCommand.Parameters.AddWithValue("@IdPresentacion", pBeStockRes.IdPresentacion)
                    End If
                End If

                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pBeStockRes.IdUnidadMedida)
                lCommand.Parameters.AddWithValue("@IdProductoEstado", pBeStockRes.IdProductoEstado)

                'If DiasVencimiento <> 0 Then
                '    lCommand.Parameters.AddWithValue("@DiasVencimientoCliente", DiasVencimiento)
                'End If

                If pBeStockRes.Control_Ultimo_Lote AndAlso pBeStockRes.Ultimo_Lote <> "" Then
                    lCommand.Parameters.AddWithValue("@UltimoLote", Val(pBeStockRes.Ultimo_Lote))
                End If

                Using dr = lCommand.ExecuteReader()

                    While dr.Read()

                        vBeStock = New clsBeStock

                        With vBeStock

                            .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                            .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                            .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                            .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                            .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado")) '#CKFK 20180109 07:17 AM Agregué ésta línea para que se llene el Id del estado del producto
                            .ProductoEstado.IdEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                            .Presentacion.IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                            .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                            .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                            .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                            .IdUbicacion_anterior = IIf(IsDBNull(dr.Item("IdUbicacion_anterior")), 0, dr.Item("IdUbicacion_anterior"))
                            .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                            .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                            .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                            .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                            .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                            .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                            .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), 0, dr.Item("lic_plate"))
                            .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                            .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                            .Fecha_Ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                            .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), New Date(1900, 1, 1), dr.Item("fecha_vence"))
                            .Uds_lic_plate = IIf(IsDBNull(dr.Item("uds_lic_plate")), 0, dr.Item("uds_lic_plate"))
                            .No_bulto = IIf(IsDBNull(dr.Item("no_bulto")), 0, dr.Item("no_bulto")) '#CKFK 20180405 Modifiqué la inicializacion cuando el campo es nulo por 0
                            .Fecha_Manufactura = IIf(IsDBNull(dr.Item("fecha_manufactura")), New Date(1900, 1, 1), dr.Item("fecha_manufactura"))
                            .Añada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                            .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                            .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                            .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                            .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                            .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                            .Peso = IIf(IsDBNull(dr.Item("Peso")), 0.0, dr.Item("Peso"))
                            .Temperatura = IIf(IsDBNull(dr.Item("temperatura")), 0.0, dr.Item("temperatura"))

                        End With

                        lBeStock.Add(vBeStock)

                    End While

                End Using

            End Using

            Return lBeStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#AT 20240515 Devuelve stock para reemplazo automatico
    Public Shared Function Get_Stock_Reemplazo_Automatico(pStockRes As clsBeStock_res,
                                                          ByRef lConnection As SqlConnection,
                                                          ByRef ltransaction As SqlTransaction) As List(Of clsBeStock)
        Get_Stock_Reemplazo_Automatico = Nothing

        Dim vBeStock As New clsBeStock
        Dim lBeStock As New List(Of clsBeStock)

        Try
            Dim SQL As String = "SELECT * FROM STOCK WHERE IdProductoBodega = @IdProductoBodega AND lote = @Lote AND lic_plate = @Licencia AND cast(fecha_vence as date) = @FechaVence"

            If pStockRes.IdPresentacion > 0 Then
                SQL += " AND IdPresentacion = @IdPresentacion"
            Else
                SQL += " AND IdPresentacion is null OR IdPresentacion = 0"
            End If

            Dim cmd As New SqlCommand(SQL, lConnection, ltransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.CommandType = CommandType.Text
            dad.SelectCommand.Transaction = ltransaction
            dad.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pStockRes.IdProductoBodega)
            dad.SelectCommand.Parameters.AddWithValue("@Lote", pStockRes.Lote)
            dad.SelectCommand.Parameters.AddWithValue("@Licencia", pStockRes.Lic_plate)
            dad.SelectCommand.Parameters.AddWithValue("@FechaVence", pStockRes.Fecha_vence.Date)

            If pStockRes.IdPresentacion > 0 Then
                dad.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pStockRes.IdPresentacion)
            End If

            Dim lDataTable As New DataTable
            dad.Fill(lDataTable)

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                For Each lRow As DataRow In lDataTable.Rows
                    vBeStock = New clsBeStock
                    Cargar(vBeStock, lRow)
                    lBeStock.Add(vBeStock)
                Next

            End If

            Return lBeStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Shared Sub WithParametros(ByRef oBeStock As clsBeStock, ByRef dr As DataRow,
                                      ByRef lConnection As SqlConnection,
                                      ByRef lTransaction As SqlTransaction)

        Try

            With oBeStock

                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .ProductoEstado.IdEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                clsLnProducto_estado.Obtener(.ProductoEstado, lConnection, lTransaction)
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .Presentacion.IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                clsLnProducto_presentacion.Obtener(.Presentacion, lConnection, lTransaction)
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .IdUbicacion_anterior = IIf(IsDBNull(dr.Item("IdUbicacion_anterior")), 0, dr.Item("IdUbicacion_anterior"))
                .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), 0, dr.Item("lic_plate"))
                .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Fecha_Ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), New Date(1900, 1, 1), dr.Item("fecha_vence"))
                .Uds_lic_plate = IIf(IsDBNull(dr.Item("uds_lic_plate")), 0, dr.Item("uds_lic_plate"))
                .No_bulto = IIf(IsDBNull(dr.Item("no_bulto")), 0, dr.Item("no_bulto"))
                .Añada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                .Peso = IIf(IsDBNull(dr.Item("Peso")), 0.0, dr.Item("Peso"))
                .Atributo_Variante_1 = IIf(IsDBNull(dr.Item("atributo_variante_1")), "", dr.Item("atributo_variante_1"))
                .Pallet_No_Estandar = IIf(IsDBNull(dr.Item("Pallet_No_Estandar")), False, dr.Item("Pallet_No_Estandar"))
                .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0.0, dr.Item("IdProductoTallaColor"))

                Try
                    .Parametros = clsLnStock_parametro.Get_All_By_IdStock(.IdStock, lConnection, lTransaction)
                Catch ex As Exception
                    'Throw ex
                End Try

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Reporte de mínimos y máximos
    ''' </summary>
    ''' <param name="pIdProducto"></param>
    ''' <returns></returns>
    ''' <remarks>ejcalderon</remarks>
    Public Shared Function GetRptProductsMinMax(ByVal pIdProducto As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)
        Dim Pos As Integer = 0
        Dim lTransaction As SqlTransaction = Nothing

        Try

            '#EJC20171112_0615PM:Agregué transacción en GetRptProductsMinMax
            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                Dim vSQL As String = String.Empty

                If pIdProducto <> 0 Then
                    vSQL = "SELECT * FROM VW_rptMinimosMaximos_v2 
							WHERE (MinimoExistencia = 0) OR (MaximoExistencia = 0) 
							AND IdProducto= @IdProducto"
                Else
                    vSQL = "SELECT * FROM VW_rptMinimosMaximos_v2 
					WHERE (MinimoExistencia = 0) OR (MaximoExistencia = 0)"
                End If

                Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                    lDTA.SelectCommand.CommandType = CommandType.Text
                    lDTA.SelectCommand.Transaction = lTransaction

                    If pIdProducto <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                    Dim lDataTable As New DataTable
                    lDTA.Fill(lDataTable)

                    Dim Obj As clsBeVW_stock_res
                    Dim Idx As Integer = -1

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        Pos = 0
                        '#EJC20171112_0932PM: Progra avanzada, avanzada:
                        ' La función se llama desde un hilo, el cual a su vez, redistribuye de forma paralela el llenado del ciclo
                        '(En otros subhilos), com amor, ejc.
                        Parallel.ForEach(lDataTable.AsEnumerable, Sub(ByVal lrow)
                                                                      SyncLock lReturnList
                                                                          Obj = New clsBeVW_stock_res()
                                                                          clsLnVW_stock_res.Cargar(Obj, lrow, lConnection, lTransaction)
                                                                          Debug.Print("Procesando_GetRptProductsMinMax: " & Obj.IdProducto)
                                                                          Idx = lReturnList.FindIndex(Function(ByVal x) x.IdProducto = Obj.IdProducto AndAlso x.IdPresentacion = Obj.IdPresentacion)
                                                                          If Idx = -1 Then
                                                                              lReturnList.Add(Obj.Clone())
                                                                              Pos += 1
                                                                          End If
                                                                      End SyncLock
                                                                  End Sub)
                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} Pos: {2}", MethodBase.GetCurrentMethod().Name, ex.Message, Pos))
        End Try

    End Function

    ''' <summary>
    ''' Buscar un registro en la tabla stock a partir de un IdStock pero se usa solo para traer los campos de referencia
    ''' </summary>
    ''' <param name="IdStock">Identificador único de fila para obtener el registro</param>
    ''' <returns>Devuelve un objeto de tipo clsBeVW_stock_res (Entidad de la vista VW_Stock_SP en la BD) </returns>
    ''' <remarks>ejcalderon</remarks>
    Public Shared Function Get_Single_By_IdStock(ByVal IdStock As Integer) As clsBeVW_stock_res

        Try

            Dim Obj As New clsBeVW_stock_res

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "Select * from VW_Stock_Res where VW_Stock_Res.IdStock = @IdStock"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        '#GT15062022_1010: deje el Obj fuera, para retornar nothing sino hay filas que setear.
                        Obj = Nothing

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then


                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeVW_stock_res()
                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Obj

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_Single_By_IdStock(ByVal IdStock As Integer,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction) As clsBeStock

        Get_Single_By_IdStock = Nothing

        Try

            Dim vSQL As String = "Select * from stock where IdStock = @IdStock"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim Obj As New clsBeStock
                    Cargar(Obj, lDataTable.Rows(0))
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Stock_By_LicensePlate(ByVal pLicensePlate As String,
                                                     ByVal IdBodega As Integer) As List(Of clsBeProducto)

        Get_Stock_By_LicensePlate = Nothing

        Try

            Dim lObj As New List(Of clsBeProducto)
            Dim BeProducto As New clsBeProducto
            Dim IdxProducto As Integer = -1

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "Select * from vw_stock_res 
    						              WHERE  lic_plate=@lic_plate 
                                          AND IdBodega = @IdBodega "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pLicensePlate)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim vStock As New clsBeVW_stock_res

                            For Each lRow As DataRow In lDataTable.Rows

                                vStock = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(vStock,
                                                         lRow,
                                                         lConnection,
                                                         lTransaction)

                                BeProducto = New clsBeProducto

                                BeProducto = clsLnProducto.Get_BeProducto_By_CodigoHH(vStock.Codigo_Producto,
                                                                                      vStock.IdBodega,
                                                                                      lConnection,
                                                                                      lTransaction)

                                BeProducto.Stock = vStock
                                lObj.Add(BeProducto)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lObj

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20211122 Modifiqué esta búsqueda para que siempre sea por ubicaion
    Public Shared Function Get_Stock_By_LicensePlate_And_IdUbicacion(ByVal pLicensePlate As String,
                                                                     ByVal IdBodega As Integer,
                                                                     ByVal IdUbicacion As Integer) As List(Of clsBeProducto)

        Get_Stock_By_LicensePlate_And_IdUbicacion = Nothing

        Try

            Dim lObj As New List(Of clsBeProducto)
            Dim Obj As New clsBeProducto
            Dim IdxProducto As Integer = -1

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "Select * from vw_stock_res 
    						              WHERE 1 = 1 "

                    If IdUbicacion <> 0 Then vSQL &= " AND IdUbicacionActual = @IdUbicacionActual "
                    If pLicensePlate <> 0 Then vSQL &= " AND lic_plate=@lic_plate "
                    If IdBodega <> 0 Then vSQL &= " AND IdBodega = @IdBodega "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pLicensePlate <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pLicensePlate)
                        If IdUbicacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacionActual", IdUbicacion)
                        If IdBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim vStock As New clsBeVW_stock_res

                            For Each lRow As DataRow In lDataTable.Rows

                                vStock = New clsBeVW_stock_res
                                clsLnVW_stock_res.Cargar(vStock, lRow, lConnection, lTransaction)
                                Obj = New clsBeProducto
                                Obj = clsLnProducto.Get_BeProducto_By_Codigo(vStock.Codigo_Producto,
                                                                             vStock.IdBodega,
                                                                             lConnection,
                                                                             lTransaction)

                                Obj.Stock = vStock
                                lObj.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lObj

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Stock_By_Licencia_And_Codigo(ByVal pLicencia As String,
                                                            ByVal pCodigo As String,
                                                            ByVal IdBodega As Integer) As List(Of clsBeProducto)

        Get_Stock_By_Licencia_And_Codigo = Nothing

        Try

            Dim lObj As New List(Of clsBeProducto)
            Dim Obj As New clsBeProducto
            Dim IdxProducto As Integer = -1

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM vw_stock_res 
    						              WHERE lic_plate=@lic_plate and codigo=@codigo 
    						              AND IdBodega = @IdBodega "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pLicencia)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigo)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim vStock As New clsBeVW_stock_res

                            For Each lRow As DataRow In lDataTable.Rows

                                vStock = New clsBeVW_stock_res
                                clsLnVW_stock_res.Cargar(vStock, lRow, lConnection, lTransaction)
                                Obj = New clsBeProducto
                                Obj = clsLnProducto.Get_BeProducto_By_Codigo(vStock.Codigo_Producto,
                                                                                    vStock.IdBodega,
                                                                                    lConnection,
                                                                                    lTransaction)

                                Obj.Stock = vStock
                                lObj.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lObj

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Reporte de productos proximos a vencer
    ''' </summary>
    ''' <param name="pIdProducto"></param>
    ''' <returns></returns>
    ''' <remarks>Bcuscul</remarks>
    Public Shared Function getRptProductosProximosVencimiento(ByVal pIdProducto As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = String.Empty

                    If pIdProducto <> 0 Then
                        vSQL = "SELECT * FROM VW_CalculoVencimientos WHERE IdProducto= @IdProducto"
                    Else
                        vSQL = "SELECT * FROM VW_CalculoVencimientos"
                    End If

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        If pIdProducto <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res
                        Dim Objs As clsBeBodega_ubicacion

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res
                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)

                                If lRow("IdUbicacion") IsNot DBNull.Value AndAlso lRow("IdUbicacion") IsNot Nothing Then
                                    Objs = New clsBeBodega_ubicacion
                                    Obj.UbicacionActual.IdUbicacion = CType(lRow("IdUbicacion"), Integer)
                                    clsLnBodega_ubicacion.Obtener(Obj.UbicacionActual, lConnection, lTransaction)
                                    Obj.Ubicacion_Nombre = Obj.UbicacionActual.NombreCompleto
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

    Public Shared Function getRptProductosProximosVencimiento_By_Bodega_And_PropietarioBodega(ByVal pIdProducto As Integer, ByVal pIdBodega As Integer, ByVal pIdPropietarioBodega As Integer, ByVal pMaxRange As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = String.Empty

            If pIdProducto <> 0 Then
                vSQL = "SELECT * FROM VW_CalculoVencimientos WHERE IdBodega=@IdBodega AND IdPropietarioBodega=@IdPropietarioBodega AND IdProducto= @IdProducto "
            Else
                vSQL = "SELECT * FROM VW_CalculoVencimientos WHERE IdBodega=@IdBodega AND IdPropietarioBodega=@IdPropietarioBodega "
            End If

            If pMaxRange > 0 Then
                vSQL += " AND [CalculoVencimiento(Días)] BETWEEN 0 and @MaxRange "
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        If pIdProducto <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        If pMaxRange <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@MaxRange", pMaxRange)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res
                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)

                                If lRow("UbicacionCompleta") IsNot DBNull.Value AndAlso lRow("UbicacionCompleta") IsNot Nothing Then
                                    Obj.Ubicacion_Nombre = CType(lRow("UbicacionCompleta"), String)
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

    '#EJC20191205: Refactorizado el día del convivio por rendimiento
    'Devolver un DataTable
    'Se modificó la vista
    'Se agregó indice.
    Public Shared Function Get_Rpt_Horizonte_Critico_By_IdBodega_And_IdPropietarioBodega(ByVal pIdProducto As Integer,
                                                                                         ByVal pIdBodega As Integer,
                                                                                         ByVal pIdPropietarioBodega As Integer,
                                                                                         ByVal pMaxRange As Integer,
                                                                                         ByVal pIncluirVencidos As Boolean) As DataTable

        Get_Rpt_Horizonte_Critico_By_IdBodega_And_IdPropietarioBodega = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT Bodega,Propietario,
                                            Codigo as 'Código', nombre as Nombre, 
                                            UnidadMedida as UM, Presentacion, Lote, 
                                            NomEstado as Estado, lic_plate, 
                                            SUM(Cantidad) as CantPres,
                                            SUM(CantidadSF) as CantUMBas,
                                            Fecha_Ingreso as Fecha_Ingreso,
                                            fecha_vence as Fecha_Vence,
                                            UbicacionCompleta as Ubic,
                                            Tolerancia_dias
                                            FROM VW_ProximosVencimiento "

                    If pIdProducto <> 0 Then

                        vSQL += " WHERE IdBodega=@IdBodega 
                                AND IdPropietarioBodega=@IdPropietarioBodega 
                                AND IdProducto= @IdProducto "
                    Else
                        vSQL += " WHERE IdBodega=@IdBodega 
                                 AND IdPropietarioBodega=@IdPropietarioBodega "
                    End If

                    If pIncluirVencidos Then
                        If pMaxRange = 0 Then
                            vSQL += " AND (Tolerancia_dias < 0  )"
                        Else
                            vSQL += " AND (Tolerancia_dias BETWEEN 0 and @MaxRange OR Tolerancia_dias < 0  )"
                        End If
                    Else
                        If pMaxRange > 0 Then
                            vSQL += " AND Tolerancia_dias BETWEEN 0 and @MaxRange "
                        ElseIf pMaxRange = 0 Then '#CKFK habilitamos el mínimo en 0 para que si seleccionan 0 me muestre los vencidos
                            vSQL += " AND Tolerancia_dias < 0 "
                        End If
                    End If

                    vSQL += "GROUP BY Bodega,Propietario, Codigo , nombre, UnidadMedida, 
		                     Presentacion, Lote, NomEstado, 
		                     lic_plate , fecha_ingreso, Fecha_Vence,
		                     UbicacionCompleta,Tolerancia_Dias,
		                     propietario"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        If pIdProducto <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        If pMaxRange <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@MaxRange", pMaxRange)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20211205: Creado para reporte de Estacionalidad del Producto
    Public Shared Function Get_Rpt_Estacionalidad_Producto_By_IdBodega_And_IdPropietarioBodega(ByVal pIdProducto As Integer, ByVal pIdBodega As Integer, ByVal pIdPropietarioBodega As Integer, ByVal pMaxRange As Integer) As DataTable

        Get_Rpt_Estacionalidad_Producto_By_IdBodega_And_IdPropietarioBodega = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT Propietario,
                                Codigo as 'Código', nombre as Nombre, 
                                UnidadMedida as UM, Presentacion, Lote, 
                                NomEstado as Estado, lic_plate as PalletId, 
                                SUM(Cantidad) as CantPres,
                                SUM(CantidadSF) as CantUMBas,
                                Fecha_Ingreso as Fecha_Ingreso,
                                fecha_vence as Fecha_Vence,
                                UbicacionCompleta as Ubic,
                                Tolerancia_dias
                                FROM VW_EstacionalidadProducto "

                    If pIdProducto <> 0 Then

                        vSQL += " WHERE IdBodega=@IdBodega 
                                AND IdPropietarioBodega=@IdPropietarioBodega 
                                AND IdProducto= @IdProducto "
                    Else
                        vSQL += " WHERE IdBodega=@IdBodega 
                                 AND IdPropietarioBodega=@IdPropietarioBodega "
                    End If

                    If pMaxRange > 0 Then
                        vSQL += " AND Tolerancia_dias > @MaxRange "
                    End If

                    vSQL += "GROUP BY Propietario, Codigo , nombre, UnidadMedida, 
		                     Presentacion, Lote, NomEstado, 
		                     lic_plate , fecha_ingreso, Fecha_Vence,
		                     UbicacionCompleta,Tolerancia_Dias,
		                     propietario"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        If pIdProducto <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        If pMaxRange <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@MaxRange", pMaxRange)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Productos_Proximos_Vencimiento(ByVal pIdProducto As Integer) As DataTable

        Try

            Dim vSQL As String = String.Empty

            If pIdProducto <> 0 Then
                vSQL = "SELECT * FROM VW_CalculoVencimientos WHERE IdProducto= @IdProducto"
            Else
                vSQL = String.Format("SELECT * FROM VW_CalculoVencimientos", pIdProducto)
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        If pIdProducto <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Return lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetStock(ByVal pTransito As Boolean, ByVal pIdOrdenCompraEnc As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = String.Empty

            If pTransito Then

                If pIdOrdenCompraEnc > 0 Then
                    vSQL = "SELECT * FROM VW_Stock_Transito WHERE [Orden de Compra]=" & pIdOrdenCompraEnc
                Else
                    vSQL = "SELECT * FROM VW_Stock_Transito"
                End If

            Else
                vSQL = "SELECT * FROM VW_RptStock"
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

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

    Public Shared Function Get_BeStock_By_IdProductoBodega(ByVal pIdProductoBodega As Integer, ByVal pIdPresentacion As Integer) As clsBeStock

        Try

            Dim vSQL As String = "SELECT IdPropietarioBodega,IdProductoEstado,IdUnidadMedida,Cantidad FROM stock WHERE IdProductoBodega=@IdProductoBodega AND IdPresentacion=@IdPresentacion"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim ObjStock As New clsBeStock

                            Dim lRow As DataRow = lDataTable.Rows(0)

                            If lRow("IdPropietarioBodega") IsNot DBNull.Value AndAlso lRow("IdPropietarioBodega") IsNot Nothing Then
                                ObjStock.IdPropietarioBodega = CType(lRow("IdPropietarioBodega"), Integer)
                            End If

                            If lRow("IdProductoEstado") IsNot DBNull.Value AndAlso lRow("IdProductoEstado") IsNot Nothing Then
                                ObjStock.IdProductoEstado = CType(lRow("IdProductoEstado"), Integer)
                            End If

                            If lRow("IdUnidadMedida") IsNot DBNull.Value AndAlso lRow("IdUnidadMedida") IsNot Nothing Then
                                ObjStock.IdUnidadMedida = CType(lRow("IdUnidadMedida"), Integer)
                            End If

                            If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                                ObjStock.Cantidad = CType(lRow("Cantidad"), Double)
                            End If

                            Return ObjStock

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetProductsPendientesRequisicion(ByVal pIdProducto As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Dim Pos As Integer = 0

        Try

            Dim vSQL As String = String.Empty

            If pIdProducto <> 0 Then
                vSQL = "SELECT * FROM vw_stock_res WHERE IdProducto= @IdProducto  order by Codigo"
            Else
                vSQL = String.Format("SELECT * FROM vw_stock_res order by Codigo", pIdProducto)
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        If pIdProducto <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res
                        Dim Idx As Integer = -1

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim vAgregarAListaPorExistencia As Boolean = False

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)

                                Idx = lReturnList.FindIndex(Function(x) x.IdProducto = Obj.IdProducto AndAlso x.IdPresentacion = Obj.IdPresentacion)

                                If Idx <> -1 Then
                                    lReturnList(Idx).CantidadPresentacion += Obj.CantidadPresentacion
                                End If

                                If Obj.IdPresentacion <> 0 Then
                                    'Validar mínimos y máximos en base a presentación
                                    vAgregarAListaPorExistencia = (Obj.CantidadPresentacion <= Obj.Existencia_min_pres)

                                    '#CKFK 20180525 07:04 PM Erik indicó que no era necesaria la validación por el máximo
                                    ''Si no infringe la regla por el mínimo, buscar si el máximo genera excepción.
                                    'If Not vAgregarAListaPorExistencia Then
                                    '    vAgregarAListaPorExistencia = Obj.CantidadPresentacion >= Obj.Existencia_max_pres
                                    'End If

                                    If vAgregarAListaPorExistencia Then
                                        Obj.Existencia_max_umbas = Obj.Existencia_max_pres
                                        Obj.Existencia_min_umbas = Obj.Existencia_min_pres
                                    End If

                                Else

                                    'Validar mínimos y máximos en base a U.MBas
                                    vAgregarAListaPorExistencia = (Obj.CantidadUmBas <= Obj.Existencia_min_umbas)

                                    '#CKFK 20180525 07:04 PM Erik indicó que no era necesaria la validación por el máximo
                                    'Si no infringe la regla por el mínimo, buscar si el máximo genera excepción.
                                    'If Not vAgregarAListaPorExistencia Then
                                    '    vAgregarAListaPorExistencia = Obj.CantidadUmBas >= Obj.Existencia_max_umbas
                                    'End If

                                End If

                                If vAgregarAListaPorExistencia AndAlso Idx = -1 Then
                                    lReturnList.Add(Obj)
                                End If

                                Pos += 1

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} Pos{2}", MethodBase.GetCurrentMethod().Name, ex.Message, Pos))
        End Try

    End Function

    ''' <summary>
    ''' Metodo para traer el stock por producto Bodega
    ''' </summary>
    ''' <param name="pIdProductoBodega"></param>
    ''' <returns></returns>

    Public Shared Function Get_All_By_IdProductoBodega(ByVal pIdProductoBodega As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "Select * from VW_Stock_Res where VW_Stock_Res.IdProductoBodega = @IdProductoBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
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

    Public Shared Function Get_All_By_IdProductoBodega(ByVal pIdProductoBodega As Integer,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Get_All_By_IdProductoBodega = Nothing

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "Select * from VW_Stock_Res where VW_Stock_Res.IdProductoBodega = @IdProductoBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res
                        clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                    Return lReturnList

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Stock_ConTramo(ByVal pIdProductoBodega As Integer,
                                                   ByVal pLote As String,
                                                   ByVal pIdEstado As Integer,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Dim lStockList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = ""

            If pLote <> "" Then

                vSQL = "SELECT * from VW_Stock_Res 
							WHERE VW_Stock_Res.IdProductoBodega = @IdProductoBodega 
							And VW_Stock_Res.lote = @lote 
							AND VW_Stock_Res.IdProductoEstado = @IdProductoEstado"
            Else

                vSQL = "SELECT * from VW_Stock_Res 
							WHERE VW_Stock_Res.IdProductoBodega = @IdProductoBodega
							AND VW_Stock_Res.IdProductoEstado = @IdProductoEstado"
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                If pLote <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@lote", pLote)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pIdEstado)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res
                        clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
                        lStockList.Add(Obj)

                    Next

                End If

            End Using

            Return lStockList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdProductoBodega_And_Lote(ByVal pIdProductoBodega As Integer,
                                                                ByVal pLote As String,
                                                                ByVal pIdEstado As Integer,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = ""

            If pLote <> "" Then
                vSQL = "SELECT * from VW_Stock_Res where VW_Stock_Res.IdProductoBodega = @IdProductoBodega " &
                                    " AND VW_Stock_Res.lote = @lote " &
                                    " AND VW_Stock_Res.IdProductoEstado = @IdProductoEstado"
            Else
                vSQL = "SELECT * from VW_Stock_Res " &
                                         " WHERE VW_Stock_Res.IdProductoBodega = @IdProductoBodega " &
                                         " AND VW_Stock_Res.IdProductoEstado = @IdProductoEstado "
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                If pLote <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@lote", pLote)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pIdEstado)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res
                        clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_ProductoBodega_And_Lote(ByVal pIdProductoBodega As Integer,
                                                         ByVal pLote As String,
                                                         ByVal pIdEstado As Integer,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = ""

            If pLote <> "" Then

                vSQL = "SELECT * from VW_Stock_Res where VW_Stock_Res.IdProductoBodega = @IdProductoBodega 
						AND VW_Stock_Res.lote = @lote 
						AND VW_Stock_Res.IdProductoEstado = @IdProductoEstado
						ORDER BY Codigo"
            Else
                vSQL = "SELECT * from VW_Stock_Res " &
                                     " WHERE VW_Stock_Res.IdProductoBodega = @IdProductoBodega " &
                                     " AND VW_Stock_Res.IdProductoEstado = @IdProductoEstado "
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                If pLote <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@lote", pLote)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pIdEstado)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res
                        clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_ProductoBodega_And_Lote(ByVal pIdProductoBodega As Integer,
                                                         ByVal pLote As String,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = ""

            If pLote <> "" Then

                vSQL = "SELECT * from VW_Stock_Res where VW_Stock_Res.IdProductoBodega = @IdProductoBodega 
						AND VW_Stock_Res.lote = @lote                         
						ORDER BY Codigo"
            Else
                vSQL = "SELECT * from VW_Stock_Res " &
                                     " WHERE VW_Stock_Res.IdProductoBodega = @IdProductoBodega "
            End If

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                If pLote <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@lote", pLote)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res
                        clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdUbicacion(ByVal pIdUbicacion As Integer, ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "SELECT * FROM VW_Stock_Res 
						WHERE IdUbicacion = @IdUbicacion AND IdBodega = @IdBodega"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res
                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
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

    Public Shared Function Get_All_By_IdUbicacion(ByVal pIdUbicacion As Integer,
                                                  ByRef lConnection As SqlConnection,
                                                  ByRef lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "SELECT * FROM VW_Stock_Res 
						WHERE IdUbicacion = @IdUbicacion "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res
                        clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20181007 Agregué el IdBodega
    Public Shared Function Get_All_By_IdUbicacion(ByVal pIdUbicacion As Integer,
                                                  ByVal pIdProducto As Integer,
                                                  ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    'JP20171027 - filtro por ubicacion y producto
                    Dim vSQL As String = "Select * from VW_Stock_Res WHERE 1 = 1 "
                    If pIdUbicacion <> 0 Then vSQL &= "AND IdUbicacionActual = @IdUbicacion "
                    If pIdProducto <> 0 Then vSQL &= "AND IdProducto = @pIdProducto "
                    If pIdBodega <> 0 Then vSQL &= "AND IdBodega = @pIdBodega "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        If pIdUbicacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        If pIdProducto <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdProducto", pIdProducto)
                        If pIdBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)

                                Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                Obj.UbicacionActual = clsLnBodega_ubicacion.Get_Single_With_Tramo_And_Sector(Obj.IdUbicacion,
                                                                                                        Obj.IdBodega,
                                                                                                        lConnection,
                                                                                                        lTransaction)

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                Return lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_LP(ByVal pLicPlate As String, ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            'JP20171027 - filtro por ubicacion y producto
            Dim vSQL As String = "Select * from VW_Stock_Res WHERE 1 = 1 "
            If pLicPlate <> "" Then vSQL &= " AND lic_plate = @pLicPlate "
            If pIdBodega <> 0 Then vSQL &= " AND IdBodega = @pIdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        If pLicPlate <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@pLicPlate", pLicPlate)
                        If pIdBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)

                                Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                Obj.UbicacionActual = clsLnBodega_ubicacion.Get_Single_With_Tramo_And_Sector(Obj.IdUbicacion, Obj.IdBodega, lConnection, lTransaction)

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

    '#CKFK 20180928 03:04 PM Creé la función GetProductoEnUbicacion para saber si un producto existe en una ubicación y no está reservado
    Public Shared Function Get_Productos_By_IdUbicacion_Original(ByVal pIdUbicacion As Integer, ByVal pIdProductoBodega As Integer) As List(Of clsBeVW_stock_res)

        Get_Productos_By_IdUbicacion_Original = Nothing

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    'JP20171027 - filtro por ubicacion y producto
                    'AT20220727 Se cambio de vista VW_Stock_Res a VW_Stock_CambioUbic
                    Dim vSQL As String = "Select IdBodega, 
                                        IdPropietario, 
                                        IdPropietarioBodega, 
                                        IdProducto, 
                                        IdProductoBodega, 
                                        IdUnidadMedida, 
                                        IdProductoEstado, 
                                        IdPresentacion, 
                                        IdRecepcionEnc,
                                        IdRecepcionDet,
                                        Propietario, 
                                        codigo, 
                                        nombre, 
                                        UnidadMedida, 
                                        Presentacion, 
                                        lote, 
                                        fecha_vence, 
                                        SUM(CantidadSF) AS CantidadSF, 
                                        SUM(peso) AS Peso, 
                                        SUM(ISNULL(Cantidad,0)) AS Cantidad, 
                                        NomEstado, 
                                        dañado, 
                                        factor, 
                                        EstadoUtilizable, 
                                        IdUbicacion, 
                                        lic_plate, 
                                        serial, 
                                        añada, 
                                        IdIndiceRotacion, 
                                        alto, 
                                        largo, 
                                        ancho, 
                                        CantidadReservada, 
                                        IdTramo, 
                                        ancho_ubicacion, 
                                        largo_ubicacion, 
                                        alto_ubicacion, 
                                        IndiceRotacion, 
                                        existencia_min_umbas, 
                                        existencia_max_umbas, 
                                        codigo_barra, 
                                        costo, 
                                        existencia_min_pres, 
                                        existencia_max_pres, 
                                        atributo_variante_1, 
                                        IdUbicacionActual, 
                                        Ubicacion_Nivel, 
                                        Ubicacion_Indice_X, 
                                        Ubicacion_Nombre, 
                                        Ubicacion_Tramo, 
                                        Nombre_Completo 
                                        FROM VW_Stock_CambioUbic 
                                        WHERE ISNULL(CantidadSF,0) - ISNULL(CantidadReservada,0) > 0 "

                    If pIdUbicacion <> 0 Then vSQL &= "AND IdUbicacionActual = @IdUbicacion "

                    If pIdProductoBodega <> 0 Then vSQL &= "AND IdProductoBodega = @pIdProductoBodega "

                    vSQL &= "      GROUP BY
                                    IdBodega, 
                                    IdPropietario, 
                                    IdPropietarioBodega, 
                                    IdProducto, 
                                    IdProductoBodega, 
                                    IdUnidadMedida, 
                                    IdProductoEstado, 
                                    IdPresentacion, 
                                    IdRecepcionEnc, 
                                    Propietario, 
                                    codigo, 
                                    nombre, 
                                    UnidadMedida, 
                                    Presentacion, 
                                    lote, 
                                    fecha_vence, 
                                    NomEstado, 
                                    dañado, 
                                    factor, 
                                    EstadoUtilizable, 
                                    IdUbicacion, 
                                    lic_plate, 
                                    serial, 
                                    añada, 
                                    IdIndiceRotacion, 
                                    alto, 
                                    largo, 
                                    ancho, 
                                    CantidadReservada, 
                                    IdTramo, 
                                    ancho_ubicacion, 
                                    largo_ubicacion, 
                                    alto_ubicacion, 
                                    IndiceRotacion, 
                                    existencia_min_umbas, 
                                    existencia_max_umbas, 
                                    codigo_barra, 
                                    costo, 
                                    existencia_min_pres, 
                                    existencia_max_pres, 
                                    atributo_variante_1, 
                                    IdUbicacionActual, 
                                    Ubicacion_Nivel, 
                                    Ubicacion_Indice_X, 
                                    Ubicacion_Nombre, 
                                    Ubicacion_Tramo, 
                                    Nombre_Completo "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdUbicacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        If pIdProductoBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res
                        Dim lStockRes As New List(Of clsBeStock_res)
                        Dim lStock As New List(Of clsBeVW_stock_res)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj,
                                                         lRow,
                                                         lConnection,
                                                         lTransaction)

                                '#EJC20200205: Cambio por rendimiento!
                                'Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                'Obj.UbicacionActual = clsLnBodega_ubicacion.GetSingleWithTramoAndSector(Obj.IdUbicacion, Obj.IdBodega, lConnection, lTransaction)
                                Obj.UbicacionActual = New clsBeBodega_ubicacion
                                Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                Obj.UbicacionActual.NombreCompleto = IIf(IsDBNull(lRow.Item("Nombre_Completo")), "", lRow.Item("Nombre_Completo"))

                                If Obj.Lic_plate <> "" Then

                                    '#CKFK20220831 Preguntar si tiene sentido que se busque por código de producto, porque la licencia puede ser 0 o puedes ser 1
                                    'lo otro es que parece que aquí solo se necesita saber la cantidad porque no se hace nada con el lStockRes
                                    'Tal vez sea conveniente hacer una función con el count
                                    lStockRes = clsLnStock_res.Get_All_By_Licencia_And_IdBodega(Obj.Lic_plate,
                                                                                                Obj.IdBodega,
                                                                                                Obj.IdProductoBodega,
                                                                                                lConnection,
                                                                                                lTransaction)

                                    If Not lStockRes Is Nothing Then

                                        If lStockRes.Count > 1 Then

                                            lStock = Get_All_By_LP(Obj.Lic_plate,
                                                                   Obj.IdBodega,
                                                                   lConnection,
                                                                   lTransaction)

                                            If Obj.IdPresentacion <> 0 AndAlso Obj.Factor > 0 Then

                                                If Not lStock Is Nothing Then

                                                    Obj.CantidadPresentacion = lStock.Sum(Function(x) x.CantidadPresentacion)
                                                    Obj.CantidadUmBas = lStock.Sum(Function(x) x.CantidadUmBas)

                                                End If

                                            Else
                                                Obj.CantidadUmBas = lStock.Where(Function(x) x.IdPresentacion = 0).Sum(Function(x) x.CantidadUmBas)
                                            End If


                                        End If

                                    End If

                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                Return lReturnList

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Productos_By_IdUbicacion(ByVal pIdUbicacion As Integer, ByVal pIdProductoBodega As Integer) As List(Of clsBeVW_stock_res)

        Get_Productos_By_IdUbicacion = Nothing

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    'JP20171027 - filtro por ubicacion y producto
                    'AT20220727 Se cambio de vista VW_Stock_Res a VW_Stock_CambioUbic
                    Dim vSQL As String = "Select IdBodega, 
                                        IdPropietario, 
                                        IdPropietarioBodega, 
                                        IdProducto, 
                                        IdProductoBodega, 
                                        IdUnidadMedida, 
                                        IdProductoEstado, 
                                        IdPresentacion, 
                                        IdRecepcionEnc,
                                        IdRecepcionDet,
                                        Propietario, 
                                        codigo, 
                                        nombre, 
                                        UnidadMedida, 
                                        Presentacion, 
                                        lote, 
                                        fecha_vence, 
                                        SUM(CantidadSF) AS CantidadSF, 
                                        SUM(peso) AS Peso, 
                                        SUM(ISNULL(Cantidad,0)) AS Cantidad,
                                        NomEstado, 
                                        dañado, 
                                        factor, 
                                        EstadoUtilizable, 
                                        IdUbicacion, 
                                        lic_plate, 
                                        serial, 
                                        añada, 
                                        IdIndiceRotacion, 
                                        alto, 
                                        largo, 
                                        ancho, 
                                        CantidadReservada, 
                                        IdTramo, 
                                        ancho_ubicacion, 
                                        largo_ubicacion, 
                                        alto_ubicacion, 
                                        IndiceRotacion, 
                                        existencia_min_umbas, 
                                        existencia_max_umbas, 
                                        codigo_barra, 
                                        costo, 
                                        existencia_min_pres, 
                                        existencia_max_pres, 
                                        atributo_variante_1, 
                                        IdUbicacionActual, 
                                        Ubicacion_Nivel, 
                                        Ubicacion_Indice_X, 
                                        Ubicacion_Nombre, 
                                        Ubicacion_Tramo, 
                                        Nombre_Completo 
                                        FROM VW_Stock_CambioUbic 
                                        WHERE ISNULL(CantidadSF,0) - ISNULL(CantidadReservada,0) > 0 "

                    If pIdUbicacion <> 0 Then vSQL &= "AND IdUbicacionActual = @IdUbicacion "

                    If pIdProductoBodega <> 0 Then vSQL &= "AND IdProductoBodega = @pIdProductoBodega "

                    vSQL &= "      GROUP BY
                                    IdBodega, 
                                    IdPropietario, 
                                    IdPropietarioBodega, 
                                    IdProducto, 
                                    IdProductoBodega, 
                                    IdUnidadMedida, 
                                    IdProductoEstado, 
                                    IdPresentacion, 
                                    IdRecepcionEnc,
                                    IdRecepcionDet,
                                    Propietario, 
                                    codigo, 
                                    nombre, 
                                    UnidadMedida, 
                                    Presentacion, 
                                    lote, 
                                    fecha_vence, 
                                    NomEstado, 
                                    dañado, 
                                    factor, 
                                    EstadoUtilizable, 
                                    IdUbicacion, 
                                    lic_plate, 
                                    serial, 
                                    añada, 
                                    IdIndiceRotacion, 
                                    alto, 
                                    largo, 
                                    ancho, 
                                    CantidadReservada, 
                                    IdTramo, 
                                    ancho_ubicacion, 
                                    largo_ubicacion, 
                                    alto_ubicacion, 
                                    IndiceRotacion, 
                                    existencia_min_umbas, 
                                    existencia_max_umbas, 
                                    codigo_barra, 
                                    costo, 
                                    existencia_min_pres, 
                                    existencia_max_pres, 
                                    atributo_variante_1, 
                                    IdUbicacionActual, 
                                    Ubicacion_Nivel, 
                                    Ubicacion_Indice_X, 
                                    Ubicacion_Nombre, 
                                    Ubicacion_Tramo, 
                                    Nombre_Completo "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdUbicacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        If pIdProductoBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res
                        Dim lStockRes As New List(Of clsBeVW_stock_res_CI)
                        Dim lStock As New List(Of clsBeVW_stock_res)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj,
                                                         lRow,
                                                         lConnection,
                                                         lTransaction)

                                '#EJC20200205: Cambio por rendimiento!
                                'Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                'Obj.UbicacionActual = clsLnBodega_ubicacion.GetSingleWithTramoAndSector(Obj.IdUbicacion, Obj.IdBodega, lConnection, lTransaction)
                                Obj.UbicacionActual = New clsBeBodega_ubicacion
                                Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                Obj.UbicacionActual.NombreCompleto = IIf(IsDBNull(lRow.Item("Nombre_Completo")), "", lRow.Item("Nombre_Completo"))

                                If Obj.Lic_plate <> "" AndAlso Obj.Lic_plate <> "0" Then

                                    '#CKFK20220831 Preguntar si tiene sentido que se busque por código de producto, porque la licencia puede ser 0 o puedes ser 1
                                    'lo otro es que parece que aquí solo se necesita saber la cantidad porque no se hace nada con el lStockRes
                                    'Tal vez sea conveniente hacer una función con el count
                                    lStockRes = Get_Cant_By_Licencia_And_IdBodega(Obj.Lic_plate,
                                                                                  Obj.IdBodega,
                                                                                  Obj.IdProductoBodega,
                                                                                  Obj.IdUbicacion,
                                                                                  Obj.IdPresentacion,
                                                                                  lConnection,
                                                                                  lTransaction)

                                    If Not lStockRes Is Nothing Then

                                        If lStockRes.Count > 1 Then

                                            If Obj.IdPresentacion <> 0 AndAlso Obj.Factor > 0 Then

                                                Obj.CantidadPresentacion = lStockRes.Sum(Function(x) x.DispPres)
                                                Obj.CantidadUmBas = lStockRes.Sum(Function(x) x.DisponibleUMBas)

                                            Else
                                                Obj.CantidadUmBas = lStockRes.Where(Function(x) x.IdPresentacion = 0).Sum(Function(x) x.DisponibleUMBas)
                                            End If

                                        End If

                                    End If

                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                Return lReturnList

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#AT24032023: cambio de anderly
    Public Shared Function Get_Productos_By_IdUbicacion_Existencia(ByVal pIdUbicacion As Integer,
                                                                   ByVal pIdProductoBodega As Integer,
                                                                   ByVal pFechaVence As Date,
                                                                   ByVal pLote As String,
                                                                   ByVal pIdPresentacion As Integer,
                                                                   ByVal pLicencia As String) As List(Of clsBeVW_stock_res)

        Get_Productos_By_IdUbicacion_Existencia = Nothing

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    'JP20171027 - filtro por ubicacion y producto
                    'AT20220727 Se cambio de vista VW_Stock_Res a VW_Stock_CambioUbic
                    Dim vSQL As String = "Select IdBodega, 
									IdPropietario, 
									IdPropietarioBodega, 
									IdProducto, 
									IdProductoBodega, 
									IdUnidadMedida, 
									IdProductoEstado, 
									IdPresentacion, 
									IdRecepcionEnc,
									IdRecepcionDet,
									Propietario, 
									codigo, 
									nombre, 
									UnidadMedida, 
									Presentacion, 
									lote, 
									fecha_vence, 
									SUM(CantidadSF) AS CantidadSF, 
									SUM(peso) AS Peso, 
									SUM(ISNULL(Cantidad,0)) AS Cantidad,
									NomEstado, 
									dañado, 
									factor, 
									EstadoUtilizable, 
									IdUbicacion, 
									lic_plate, 
									serial, 
									añada, 
									IdIndiceRotacion, 
									alto, 
									largo, 
									ancho, 
									CantidadReservada, 
									IdTramo, 
									ancho_ubicacion, 
									largo_ubicacion, 
									alto_ubicacion, 
									IndiceRotacion, 
									existencia_min_umbas, 
									existencia_max_umbas, 
									codigo_barra, 
									costo, 
									existencia_min_pres, 
									existencia_max_pres, 
									atributo_variante_1, 
									IdUbicacionActual, 
									Ubicacion_Nivel, 
									Ubicacion_Indice_X, 
									Ubicacion_Nombre, 
									Ubicacion_Tramo, 
									Nombre_Completo,
                                    IdProductoTallaColor, 
                                    Talla, 
                                    Color
									FROM VW_Stock_CambioUbic 
									WHERE ISNULL(CantidadSF,0) - ISNULL(CantidadReservada,0) > 0 
										   AND fecha_vence = @FechaVence "

                    If pIdUbicacion <> 0 Then vSQL &= " AND IdUbicacionActual = @IdUbicacion "

                    If pIdProductoBodega <> 0 Then vSQL &= " AND IdProductoBodega = @pIdProductoBodega "

                    If pLote <> "" Then
                        vSQL &= " AND lote = @Lote  "
                    Else
                        vSQL &= " AND (lote = @Lote OR lote IS NULL)  "
                    End If

                    If pIdPresentacion <> 0 Then
                        vSQL &= " AND IdPresentacion = @IdPresentacion  "
                    Else
                        vSQL &= " AND (IdPresentacion = @IdPresentacion OR IdPresentacion IS NULL) "
                    End If

                    If pLicencia <> "" Then
                        If pIdUbicacion <> 0 Then vSQL &= " AND lic_plate = @Licencia "
                    End If

                    vSQL &= "      GROUP BY
								IdBodega, 
								IdPropietario, 
								IdPropietarioBodega, 
								IdProducto, 
								IdProductoBodega, 
								IdUnidadMedida, 
								IdProductoEstado, 
								IdPresentacion, 
								IdRecepcionDet,
								IdRecepcionEnc, 
								Propietario, 
								codigo, 
								nombre, 
								UnidadMedida, 
								Presentacion, 
								lote, 
								fecha_vence, 
								NomEstado, 
								dañado, 
								factor, 
								EstadoUtilizable, 
								IdUbicacion, 
								lic_plate, 
								serial, 
								añada, 
								IdIndiceRotacion, 
								alto, 
								largo, 
								ancho, 
								CantidadReservada, 
								IdTramo, 
								ancho_ubicacion, 
								largo_ubicacion, 
								alto_ubicacion, 
								IndiceRotacion, 
								existencia_min_umbas, 
								existencia_max_umbas, 
								codigo_barra, 
								costo, 
								existencia_min_pres, 
								existencia_max_pres, 
								atributo_variante_1, 
								IdUbicacionActual, 
								Ubicacion_Nivel, 
								Ubicacion_Indice_X, 
								Ubicacion_Nombre, 
								Ubicacion_Tramo, 
								Nombre_Completo,
                                IdProductoTallaColor, 
                                Talla, 
                                Color "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdUbicacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        If pIdProductoBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@FechaVence", pFechaVence)
                        lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pLote)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)
                        If pLicencia <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@Licencia", pLicencia)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res
                        Dim lStockRes As New List(Of clsBeVW_stock_res_CI)
                        Dim lStock As New List(Of clsBeVW_stock_res)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj,
                                                     lRow,
                                                     lConnection,
                                                     lTransaction)

                                '#EJC20200205: Cambio por rendimiento!
                                'Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                'Obj.UbicacionActual = clsLnBodega_ubicacion.GetSingleWithTramoAndSector(Obj.IdUbicacion, Obj.IdBodega, lConnection, lTransaction)
                                Obj.UbicacionActual = New clsBeBodega_ubicacion
                                Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                Obj.UbicacionActual.NombreCompleto = IIf(IsDBNull(lRow.Item("Nombre_Completo")), "", lRow.Item("Nombre_Completo"))

                                '#CKFK20221207 Con esta búsqueda ya no es necesario entrar a buscar la cantidad
                                'If Obj.Lic_plate <> "" Then

                                '    '#CKFK20220831 Preguntar si tiene sentido que se busque por código de producto, porque la licencia puede ser 0 o puedes ser 1
                                '    'lo otro es que parece que aquí solo se necesita saber la cantidad porque no se hace nada con el lStockRes
                                '    'Tal vez sea conveniente hacer una función con el count
                                '    lStockRes = Get_Cant_By_Licencia_And_IdBodega(Obj.Lic_plate,
                                '                                                  Obj.IdBodega,
                                '                                                  Obj.IdProductoBodega,
                                '                                                  Obj.IdUbicacion,
                                '                                                  lConnection,
                                '                                                  lTransaction)

                                '    If Not lStockRes Is Nothing Then

                                '        If lStockRes.Count > 1 Then

                                '            If Obj.IdPresentacion <> 0 AndAlso Obj.Factor > 0 Then

                                '                Obj.CantidadPresentacion = lStockRes.Sum(Function(x) x.DispPres)
                                '                Obj.CantidadUmBas = lStockRes.Sum(Function(x) x.DisponibleUMBas)

                                '            Else
                                '                Obj.CantidadUmBas = lStockRes.Where(Function(x) x.IdPresentacion = 0).Sum(Function(x) x.DisponibleUMBas)
                                '            End If

                                '        End If

                                '    End If

                                'End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                Return lReturnList

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#AT20221025 Función para obtener productos por stock res ci 
    Public Shared Function Get_Productos_By_StockResCI(BeStockResCI As clsBeVW_stock_res_CI) As List(Of clsBeVW_stock_res)

        Get_Productos_By_StockResCI = Nothing

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                    '#AT20240805 Agregue el campo IdStock
                    Dim vSQL As String = " Select IdBodega, 
                                        IdPropietario, 
                                        IdPropietarioBodega, 
                                        IdProducto, 
                                        IdProductoBodega, 
                                        IdUnidadMedida, 
                                        IdProductoEstado, 
                                        IdPresentacion, 
                                        IdRecepcionEnc,
                                        IdRecepcionDet,
                                        Propietario, 
                                        codigo, 
                                        nombre, 
                                        UnidadMedida, 
                                        Presentacion, 
                                        lote, 
                                        fecha_vence, 
                                        SUM(CantidadSF) AS CantidadSF, 
                                        SUM(peso) AS Peso, 
                                        SUM(ISNULL(Cantidad,0)) AS Cantidad,
                                        NomEstado, 
                                        dañado, 
                                        factor, 
                                        EstadoUtilizable, 
                                        IdUbicacion, 
                                        lic_plate, 
                                        serial, 
                                        añada, 
                                        IdIndiceRotacion, 
                                        alto, 
                                        largo, 
                                        ancho, 
                                        CantidadReservada, 
                                        IdTramo, 
                                        ancho_ubicacion, 
                                        largo_ubicacion, 
                                        alto_ubicacion, 
                                        IndiceRotacion, 
                                        existencia_min_umbas, 
                                        existencia_max_umbas, 
                                        codigo_barra, 
                                        costo, 
                                        existencia_min_pres, 
                                        existencia_max_pres, 
                                        atributo_variante_1, 
                                        IdUbicacionActual, 
                                        Ubicacion_Nivel, 
                                        Ubicacion_Indice_X, 
                                        Ubicacion_Nombre, 
                                        Ubicacion_Tramo, 
                                        Nombre_Completo, 
                                        IdStock
                                        FROM VW_Stock_Res 
                                        WHERE ISNULL(CantidadSF,0) - ISNULL(CantidadReservada,0) > 0 "

                    If BeStockResCI.idUbic <> 0 Then vSQL &= "AND IdUbicacionActual = @IdUbicacion "

                    If BeStockResCI.IdProductoBodega <> 0 Then
                        vSQL &= "AND IdProductoBodega = @pIdProductoBodega "
                    Else
                        Throw New Exception("ERROR_202310121150: El IdProductoBodega es 0")
                    End If

                    If BeStockResCI.IdPresentacion <> 0 Then vSQL &= "AND IdPresentacion = @pIdPresentacion "

                    If BeStockResCI.Lote <> "" Then vSQL &= "AND lote = @pLote "

                    If BeStockResCI.LicPlate <> "" Then vSQL &= "AND lic_plate = @pLicPlate "

                    '#GT12102023: validar que fecha vence tenga sentido (1900 no lo tiene)
                    If BeStockResCI.Fecha_Vence.Year.ToString() > "1900" Then vSQL &= " AND fecha_vence = @pFechaVence "

                    vSQL &= "      GROUP BY 
                                    IdBodega, 
                                    IdPropietario, 
                                    IdPropietarioBodega, 
                                    IdProducto, 
                                    IdProductoBodega, 
                                    IdUnidadMedida, 
                                    IdProductoEstado, 
                                    IdPresentacion, 
                                    IdRecepcionEnc, 
                                    IdRecepcionDet,
                                    Propietario, 
                                    codigo, 
                                    nombre, 
                                    UnidadMedida, 
                                    Presentacion, 
                                    lote, 
                                    fecha_vence, 
                                    NomEstado, 
                                    dañado, 
                                    factor, 
                                    EstadoUtilizable, 
                                    IdUbicacion, 
                                    lic_plate, 
                                    serial, 
                                    añada, 
                                    IdIndiceRotacion, 
                                    alto, 
                                    largo, 
                                    ancho, 
                                    CantidadReservada, 
                                    IdTramo, 
                                    ancho_ubicacion, 
                                    largo_ubicacion, 
                                    alto_ubicacion, 
                                    IndiceRotacion, 
                                    existencia_min_umbas, 
                                    existencia_max_umbas, 
                                    codigo_barra, 
                                    costo, 
                                    existencia_min_pres, 
                                    existencia_max_pres, 
                                    atributo_variante_1, 
                                    IdUbicacionActual, 
                                    Ubicacion_Nivel, 
                                    Ubicacion_Indice_X, 
                                    Ubicacion_Nombre, 
                                    Ubicacion_Tramo, 
                                    Nombre_Completo, CantidadSF, IdStock"

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If BeStockResCI.idUbic <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", BeStockResCI.idUbic)
                        If BeStockResCI.IdProductoBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", BeStockResCI.IdProductoBodega)
                        If BeStockResCI.IdPresentacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdPresentacion", BeStockResCI.IdPresentacion)
                        If BeStockResCI.Lote <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@pLote", BeStockResCI.Lote)
                        If BeStockResCI.LicPlate <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@pLicPlate", BeStockResCI.LicPlate)

                        If BeStockResCI.Fecha_Vence.Year.ToString() > "1900" Then lDTA.SelectCommand.Parameters.AddWithValue("@pFechaVence", BeStockResCI.Fecha_Vence)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res
                        Dim lStockRes As New List(Of clsBeStock_res)
                        Dim lStock As New List(Of clsBeVW_stock_res)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj,
                                                         lRow,
                                                         lConnection,
                                                         lTransaction)

                                '#EJC20200205: Cambio por rendimiento!
                                'Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                'Obj.UbicacionActual = clsLnBodega_ubicacion.GetSingleWithTramoAndSector(Obj.IdUbicacion, Obj.IdBodega, lConnection, lTransaction)
                                Obj.UbicacionActual = New clsBeBodega_ubicacion
                                Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                Obj.UbicacionActual.NombreCompleto = IIf(IsDBNull(lRow.Item("Nombre_Completo")), "", lRow.Item("Nombre_Completo"))

                                If Obj.Lic_plate <> "" Then

                                    '#CKFK20220831 Preguntar si tiene sentido que se busque por código de producto, porque la licencia puede ser 0 o puedes ser 1
                                    'lo otro es que parece que aquí solo se necesita saber la cantidad porque no se hace nada con el lStockRes
                                    'Tal vez sea conveniente hacer una función con el count
                                    lStockRes = clsLnStock_res.Get_All_By_Licencia_And_IdBodega_And_IdUbicacion(Obj.Lic_plate,
                                                                                                Obj.IdBodega,
                                                                                                Obj.IdProductoBodega,
                                                                                                Obj.IdUbicacion,
                                                                                                Obj.IdPresentacion,
                                                                                                lConnection,
                                                                                                lTransaction)

                                    If Not lStockRes Is Nothing Then

                                        If lStockRes.Count > 1 Then

                                            lStock = Get_All_By_LP_And_IdUbicacion(Obj.Lic_plate,
                                                                                   Obj.IdBodega,
                                                                                   Obj.IdUbicacion,
                                                                                   Obj.IdPresentacion,
                                                                                   lConnection,
                                                                                   lTransaction)

                                            If Obj.IdPresentacion <> 0 AndAlso Obj.Factor > 0 Then

                                                If Not lStock Is Nothing Then

                                                    Obj.CantidadPresentacion = lStock.Sum(Function(x) x.CantidadPresentacion)
                                                    Obj.CantidadUmBas = lStock.Sum(Function(x) x.CantidadUmBas)

                                                End If

                                            Else
                                                Obj.CantidadUmBas = lStock.Where(Function(x) x.IdPresentacion = 0).Sum(Function(x) x.CantidadUmBas)
                                            End If


                                        End If

                                    End If

                                End If

                                lReturnList.Add(Obj)

                            Next

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                Return lReturnList

            End Using


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Productos_By_IdUbicacion_And_LicPlate(ByVal pIdUbicacion As Integer,
                                                                     ByVal pIdBodega As Integer,
                                                                     ByVal pIdProductoBodega As Integer,
                                                                     ByVal pLicPlate As String,
                                                                     ByVal pIdPresentacion As Integer) As List(Of clsBeVW_stock_res)

        Get_Productos_By_IdUbicacion_And_LicPlate = Nothing

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try
            '#AT20220727 Se cambio de visita de VW_Stock_Res a VW_Stock_CambioUbic 
            Dim vSQL As String = "Select * from VW_Stock_CambioUbic WHERE ISNULL(CantidadSF,0) - ISNULL(CantidadReservada,0) > 0 "
            If pIdUbicacion <> 0 Then vSQL &= "AND IdUbicacionActual = @pIdUbicacion "
            If pIdProductoBodega <> 0 Then vSQL &= "AND IdProductoBodega = @pIdProductoBodega "
            vSQL &= "AND lic_plate = @pLicPlate"
            If pIdPresentacion <> 0 Then vSQL &= " AND IdPresentacion = @pIdPresentacion "
            If pIdPresentacion = 0 Then vSQL &= " AND IdPresentacion IS NULL "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdUbicacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdUbicacion", pIdUbicacion)
                        If pIdProductoBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@pLicPlate", pLicPlate)
                        If pIdPresentacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdPresentacion", pIdPresentacion)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res
                        Dim lStockRes As New List(Of clsBeStock_res)
                        Dim lStock As New List(Of clsBeVW_stock_res)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)

                                '#EJC20200205: Cambio por rendimiento!
                                Obj.UbicacionActual = New clsBeBodega_ubicacion
                                Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                Obj.UbicacionActual.NombreCompleto = IIf(IsDBNull(lRow.Item("Nombre_Completo")), "", lRow.Item("Nombre_Completo"))

                                If Obj.Lic_plate <> "" Then

                                    lStockRes = clsLnStock_res.Get_All_By_Licencia_And_IdBodega_And_IdUbicacion(Obj.Lic_plate,
                                                                                                                Obj.IdBodega,
                                                                                                                Obj.IdProductoBodega,
                                                                                                                Obj.IdUbicacion,
                                                                                                                Obj.IdPresentacion,
                                                                                                                lConnection,
                                                                                                                lTransaction)

                                    If Not lStockRes Is Nothing Then

                                        If lStockRes.Count > 1 Then

                                            lStock = Get_All_By_LP_And_IdUbicacion(Obj.Lic_plate,
                                                                                   Obj.IdBodega,
                                                                                   Obj.IdUbicacion,
                                                                                   Obj.IdPresentacion,
                                                                                   lConnection,
                                                                                   lTransaction)

                                            If Obj.IdPresentacion <> 0 AndAlso Obj.Factor > 0 Then

                                                If Not lStock Is Nothing Then

                                                    Obj.CantidadPresentacion = lStock.Sum(Function(x) x.CantidadPresentacion)
                                                    Obj.CantidadUmBas = lStock.Sum(Function(x) x.CantidadUmBas)

                                                End If

                                            Else
                                                Obj.CantidadUmBas = lStock.Where(Function(x) x.IdPresentacion = 0).Sum(Function(x) x.CantidadUmBas)
                                            End If


                                        End If

                                    End If

                                End If

                                lReturnList.Add(Obj)

                            Next

                        Else

                            lReturnList = Get_Productos_Con_Reserva_By_IdUbicacion_And_LicPlate(pIdUbicacion,
                                                                                                pIdBodega,
                                                                                                pIdProductoBodega,
                                                                                                pLicPlate)

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                Return lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Productos_Con_Reserva_By_IdUbicacion_And_LicPlate(ByVal pIdUbicacion As Integer,
                                                                                 ByVal pIdBodega As Integer,
                                                                                 ByVal pIdProductoBodega As Integer,
                                                                                 ByVal pLicPlate As String) As List(Of clsBeVW_stock_res)

        Get_Productos_Con_Reserva_By_IdUbicacion_And_LicPlate = Nothing

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "Select * from VW_Stock_Res WHERE 1 > 0 "
            If pIdUbicacion <> 0 Then vSQL &= "AND IdUbicacionActual = @pIdUbicacion "
            If pIdProductoBodega <> 0 Then vSQL &= "AND IdProductoBodega = @pIdProductoBodega "
            vSQL &= "AND lic_plate = @pLicPlate"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        If pIdUbicacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdUbicacion", pIdUbicacion)
                        If pIdProductoBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@pLicPlate", pLicPlate)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim vReservadoCompletamente As Boolean = False

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res()

                                clsLnVW_stock_res.Cargar(Obj,
                                                         lRow,
                                                         lConnection,
                                                         lTransaction)

                                '#EJC20200205: Cambio por rendimiento!
                                Obj.UbicacionActual = New clsBeBodega_ubicacion()
                                Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                Obj.UbicacionActual.NombreCompleto = IIf(IsDBNull(lRow.Item("Nombre_Completo")), "", lRow.Item("Nombre_Completo"))

                                '#EJC20220330:Si la licencia está reservada completamente
                                '(es decir, la cantidad disponible total en la licencia fue reservada)
                                'Entonces ReservadoCompletamente debería ser true.
                                'hacer en HH
                                'vReservadoCompletamente = (Obj.CantidadUmBas - Obj.CantidadReservadaUMBas)
                                lReturnList.Add(Obj)

                            Next

                        Else

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                Return lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20181007 09:22 PM Creé la función GetProductoEnUbicacionAgrupado para saber si un producto existe en una ubicación y no está reservado
    Public Shared Function GetProductoEnUbicacionAgrupado(ByVal pIdUbicacion As Integer, ByVal pIdProductoBodega As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdBodega, IdPropietario, IdPropietarioBodega, IdProducto, IdProductoBodega, 0 as IdStock, 0 as IdUbicacion_anterior, IdUnidadMedida, 
						IdProductoEstado, IdPresentacion, 0 as IdRecepcionEnc, Propietario, codigo, nombre, UnidadMedida, Presentacion, lote, GetDate() as fecha_ingreso, 
						fecha_vence, sum(CantidadSF) as CantidadSF , sum(peso) as Peso, SUM(Cantidad) AS Cantidad, NomEstado, dañado, factor, EstadoUtilizable, IdUbicacion, lic_plate, isnull(serial, '') as serial, isnull(añada, 0) as añada,
						IdIndiceRotacion, alto, largo, ancho, sum(CantidadReservada) as CantidadReservada, IdTramo, ancho_ubicacion, largo_ubicacion, alto_ubicacion, IndiceRotacion, 
						existencia_min_umbas, existencia_max_umbas, codigo_barra, costo, existencia_min_pres, existencia_max_pres, atributo_variante_1, 
						IdUbicacionActual, Ubicacion_Nivel, Ubicacion_Indice_X, Ubicacion_Nombre, Ubicacion_Tramo
						from VW_Stock_Res 
						WHERE 1= 1 "

                    If pIdUbicacion <> 0 Then vSQL &= " AND IdUbicacionActual = @IdUbicacion "
                    If pIdProductoBodega <> 0 Then vSQL &= " AND IdProductoBodega = @pIdProductoBodega "

                    vSQL &= "Group by IdBodega, IdPropietario, IdPropietarioBodega, IdProducto, IdProductoBodega, IdUnidadMedida, 
						IdProductoEstado, IdPresentacion, Propietario, codigo, nombre, UnidadMedida, Presentacion, lote,  
						fecha_vence, NomEstado, dañado, factor, EstadoUtilizable, IdUbicacion, lic_plate, isnull(serial, ''), isnull(añada, 0), 
						IdIndiceRotacion, alto, largo, ancho, IdTramo, ancho_ubicacion, largo_ubicacion, alto_ubicacion, IndiceRotacion, 
						existencia_min_umbas, existencia_max_umbas, codigo_barra, costo, existencia_min_pres, existencia_max_pres, atributo_variante_1, 
						IdUbicacionActual, Ubicacion_Nivel, Ubicacion_Indice_X, Ubicacion_Nombre, Ubicacion_Tramo
						Having ISNULL(sum(CantidadSF),0) - ISNULL(sum(CantidadReservada),0) > 0 "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction

                        If pIdUbicacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        If pIdProductoBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdProductoBodega", pIdProductoBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)

                                Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                Obj.UbicacionActual = clsLnBodega_ubicacion.Get_Single_With_Tramo_And_Sector(Obj.IdUbicacion, Obj.IdBodega, lConnection, lTransaction)

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

    Public Shared Function Get_All_By_IdStock(ByVal pIdStock As Integer) As List(Of clsBeVW_stock_res)

        Get_All_By_IdStock = Nothing

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "Select * from VW_Stock_Res WHERE IdStock = @IdStock "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", pIdStock)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res
                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
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

    Public Shared Function Get_Bandera_Usado_By_IdUbicacion(ByVal pIdUbicacion As Integer,
                                                            ByRef lConnection As SqlConnection,
                                                            ByRef lTransaction As SqlTransaction) As Boolean

        Get_Bandera_Usado_By_IdUbicacion = True

        Try

            Dim vSQL As String = "Select * from VW_Stock_Res WHERE IdUbicacion = @IdUbicacion"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Return lDataTable.Rows.Count > 0

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Creado por Erik Calderón
    ''' </summary>
    ''' <param name="pObj"></param>
    ''' <param name="pConnection"></param>
    ''' <param name="pTransaction"></param>
    Public Shared Sub RestarCantidadVerificada(ByVal pObj As clsBeStock, ByVal pConnection As SqlConnection, ByVal pTransaction As SqlTransaction)

        Try

            Dim vSQL As String = "UPDATE stock SET cantidad=@cantidad WHERE IdProductoBodega=@IdProductoBodega And
					IdProductoEstado=@IdProductoEstado And IdPresentacion=@IdPresentacion And 
					IdUnidadMedida=@IdUnidadMedida And IdPedidoEnc=@IdPedidoEnc"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Transaction = pTransaction
                lCommand.Parameters.AddWithValue("@cantidad", pObj.Cantidad)
                lCommand.Parameters.AddWithValue("@IdProductoBodega", pObj.IdProductoBodega)
                lCommand.Parameters.AddWithValue("@IdProductoEstado", pObj.IdProductoEstado)
                lCommand.Parameters.AddWithValue("@IdPresentacion", pObj.IdPresentacion)
                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pObj.IdUnidadMedida)
                lCommand.Parameters.AddWithValue("@IdPedidoEnc", pObj.IdPedidoEnc)

                lCommand.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_Single_Stock_By_IdStock_And_IdProducto_Bodega(ByVal IdStock As Integer,
                                                                             ByVal IdProductoBodega As Integer,
                                                                             ByRef lConnection As SqlConnection,
                                                                             ByRef lTransaction As SqlTransaction) As clsBeStock

        Get_Single_Stock_By_IdStock_And_IdProducto_Bodega = Nothing

        Try

            Dim sp As String = "SELECT * FROM Stock " &
            " WHERE IdStock = @IdStock AND IdProductoBodega = @IdProductoBodega "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdStock", IdStock))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", IdProductoBodega))
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Dim oBeStock As New clsBeStock

            If dt.Rows.Count = 1 Then
                WithParametros(oBeStock, dt.Rows(0), lConnection, lTransaction)
                Return oBeStock
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_Stock_By_IdStock(ByVal IdStock As Integer,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef lTransaction As SqlTransaction) As clsBeStock

        Get_Single_Stock_By_IdStock = Nothing

        Try

            Dim sp As String = "SELECT * FROM Stock " &
            " WHERE IdStock = @IdStock "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdStock", IdStock))
            Dim dt As New DataTable
            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            Dim oBeStock As New clsBeStock

            If dt.Rows.Count = 1 Then
                WithParametros(oBeStock, dt.Rows(0), lConnection, lTransaction)
                Return oBeStock
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20181108_0215PM Creé esta función para determinar si el Stock original que se insertó en la tabla está integro y no ha sido modificado
    Public Shared Function Existe_Stock(ByRef pBeStockOriginal As clsBeStock,
                                         ByRef lConnection As SqlConnection,
                                         ByRef lTransaction As SqlTransaction) As Boolean

        Existe_Stock = False

        Try

            Dim sp As String = "SELECT * FROM stock 
								WHERE IdProductoBodega = @IdProductoBodega 
								AND cantidad = @cantidad 
								AND IdProductoEstado = @IdProductoEstado 
								AND lote = @lote
								AND IdUnidadMedida = @IdUnidadMedida 
								AND ISNULL(IdPresentacion,0) = @IdPresentacion 
								AND (fecha_vence  IS NULL OR fecha_vence = @fecha_vence)
								AND IdRecepcionEnc = @IdRecepcionEnc
								AND IdRecepcionDet = @IdRecepcionDet "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", pBeStockOriginal.IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@cantidad", pBeStockOriginal.Cantidad))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoEstado", pBeStockOriginal.IdProductoEstado))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@lote", pBeStockOriginal.Lote))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUnidadMedida", pBeStockOriginal.IdUnidadMedida))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPresentacion", pBeStockOriginal.IdPresentacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@fecha_vence", pBeStockOriginal.Fecha_vence))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pBeStockOriginal.IdRecepcionEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdRecepcionDet", pBeStockOriginal.IdRecepcionDet))

            Dim dt As New DataTable

            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count > 0 Then
                pBeStockOriginal.IdStock = dt.Rows(0).Item("IdStock")
                Existe_Stock = True
            End If

            Return dt.Rows.Count > 0

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_StockRes(ByRef pReDet As clsBeTrans_re_det,
                                           ByRef IdPedido As Integer,
                                           ByRef lConnection As SqlConnection,
                                           ByRef lTransaction As SqlTransaction) As Boolean

        Existe_StockRes = True

        Try

            Dim sp As String = "SELECT * FROM stock_res 
								WHERE lic_plate = @licencia 
                                AND @licencia <> '0' 
                                AND IdProductoBodega = @IdProductoBodega
                                AND IdRecepcion = @IdRecepcionEnc "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@licencia", pReDet.Lic_plate))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", pReDet.IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pReDet.IdRecepcionEnc))

            Dim dt As New DataTable

            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count > 0 Then
                IdPedido = dt.Rows(0).Item("IdPedido")
            End If

            Return dt.Rows.Count > 0

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Stock_En_Ubicacion(ByRef pBeStockOriginal As clsBeStock,
                                         ByRef lConnection As SqlConnection,
                                         ByRef lTransaction As SqlTransaction) As Boolean

        Existe_Stock_En_Ubicacion = True

        Try

            Dim sp As String = "SELECT * FROM stock 
                                WHERE IdProductoBodega = @IdProductoBodega 
								AND IdProductoEstado = @IdProductoEstado 
								AND lote = @lote
								AND IdUnidadMedida = @IdUnidadMedida 
								AND ISNULL(IdPresentacion,0) = @IdPresentacion 
								AND fecha_vence = @fecha_vence
                                AND IdUbicacion=@IdUbicacion"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", pBeStockOriginal.IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoEstado", pBeStockOriginal.IdProductoEstado))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@lote", pBeStockOriginal.Lote))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUnidadMedida", pBeStockOriginal.IdUnidadMedida))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPresentacion", pBeStockOriginal.IdPresentacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@fecha_vence", pBeStockOriginal.Fecha_vence))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUbicacion", pBeStockOriginal.IdUbicacion))

            Dim dt As New DataTable

            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count > 0 Then
                pBeStockOriginal.IdStock = dt.Rows(0).Item("IdStock")
            End If

            Return dt.Rows.Count > 0

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK 20181108_0215PM Creé esta función para determinar si el Stock original que se insertó en la tabla está integro y no ha sido modificado
    Public Shared Function Get_Stock(ByVal pBeTransReDet As clsBeTrans_re_det,
                                     ByRef pBeStockOriginal As clsBeStock,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        Get_Stock = False

        Try

            Dim sp As String = "SELECT * FROM stock
                                WHERE IdProductoBodega = @IdProductoBodega
                                  AND cantidad = @cantidad
                                  AND IdProductoEstado = @IdProductoEstado
                                  AND lote = @lote
                                  AND IdUnidadMedida = @IdUnidadMedida
                                  AND ISNULL(IdPresentacion, 0) = @IdPresentacion
                                  AND (
                                      (fecha_vence IS NULL) OR
                                      (CAST(fecha_vence AS DATE) = @fecha_vence)
                                  )
                                  AND IdRecepcionEnc = @IdRecepcionEnc
                                  AND IdRecepcionDet = @IdRecepcionDet"


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", pBeTransReDet.IdProductoBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@cantidad", pBeTransReDet.cantidad_recibida))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoEstado", pBeTransReDet.IdProductoEstado))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@lote", pBeTransReDet.Lote))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUnidadMedida", pBeTransReDet.IdUnidadMedida))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPresentacion", pBeTransReDet.IdPresentacion))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@fecha_vence", pBeTransReDet.Fecha_vence.Date))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdRecepcionEnc", pBeTransReDet.IdRecepcionEnc))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdRecepcionDet", pBeTransReDet.IdRecepcionDet))

            Dim dt As New DataTable

            dad.Fill(dt)

            cmd.Dispose()
            dad.Dispose()

            If dt.Rows.Count > 0 Then
                Cargar(pBeStockOriginal, dt.Rows(0))
            End If

            Return dt.Rows.Count > 0

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Cantidad_Reservada(ByVal pIdStock As Integer) As Double

        Try

            Dim CantidadReservada As Double = 0

            Dim query As String = "select ISNULL(sum(s.cantidad / pp.factor),0) as CantidadReservada " &
                                                    " from stock_res  s Inner join producto_presentacion pp " &
                                                    "on s.IdPresentacion = pp.idPresentacion where s.idStock = @idStock"


            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(query, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommand.Parameters.AddWithValue("@idStock", pIdStock)

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            CantidadReservada = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return CantidadReservada

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Despacha_Pallet(ByRef oBeStock As clsBeStock,
                                           Optional ByVal pConection As SqlConnection = Nothing,
                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

        Try

            Dim vSQL As String = "delete from stock where IdStock = @IdStock"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(vSQL, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(vSQL, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdStock", oBeStock.IdStock))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Despacha_Pallet(ByRef oBeStock As clsBeStock,
                                           ByVal pIdUbicacionVirtualDestino As Integer,
                                           Optional ByVal pConection As SqlConnection = Nothing,
                                           Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

        Try

            Upd.Init("stock")
            Upd.Add("IdUbicacion", "@IdUbicacion", DataType.Parametro)
            Upd.Where("IdStock = @IdStock")

            Dim vSQL As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(vSQL, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(vSQL, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdStock", oBeStock.IdStock))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function ActualizaIdUbicacionPallet(ByRef oBeStock As clsBeStock,
                                                      ByVal NuevoIdUbicacion As Integer,
                               Optional ByVal pConection As SqlConnection = Nothing,
                               Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

        Try

            Upd.Init("stock")
            Upd.Add("IdUbicacion", "@NuevoIdUbicacion", DataType.Parametro)
            Upd.Where("IdStock = @IdStock")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NuevoIdUbicacion", NuevoIdUbicacion))
            cmd.Parameters.Add(New SqlParameter("@IdStock", oBeStock.IdStock))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Existe_Lp_In_Stock(ByVal lic_plate As String) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransactiona As SqlTransaction = Nothing

        Existe_Lp_In_Stock = False

        Try

            lConnection.Open() : lTransactiona = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Stock Where(lic_plate = @lic_plate) "
            Dim cmd As New SqlCommand(sp, lConnection, lTransactiona) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", lic_plate))



            Dim dt As New DataTable
            Dim pBeStock_rec As New clsBeStock_rec
            dad.Fill(dt)

            Existe_Lp_In_Stock = dt.Rows.Count > 0

            lTransactiona.Commit()

        Catch ex As Exception
            If Not lTransactiona Is Nothing Then lTransactiona.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#CKFK20220618 Agregué esta función para buscar una licencia en el stock por IdBodega
    Public Shared Function Existe_Lp_In_Stock_By_IdBodega(ByVal pLic_plate As String,
                                                          ByVal pIdBodega As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransactiona As SqlTransaction = Nothing

        Existe_Lp_In_Stock_By_IdBodega = False

        Try

            lConnection.Open() : lTransactiona = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Stock Where(lic_plate = @lic_plate)  AND (IdBodega = @IdBodega)"
            Dim cmd As New SqlCommand(sp, lConnection, lTransactiona) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", pLic_plate))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            Dim dt As New DataTable
            Dim pBeStock_rec As New clsBeStock_rec
            dad.Fill(dt)

            Existe_Lp_In_Stock_By_IdBodega = dt.Rows.Count > 0

            lTransactiona.Commit()

        Catch ex As Exception
            If Not lTransactiona Is Nothing Then lTransactiona.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#AT20240525 Agregué esta función para validar una licencia por producto y bodega 
    Public Shared Function Existe_Lp_In_Stock_By_IdProductoBodega(ByVal pLic_plate As String,
                                                                  ByVal pIdBodega As Integer,
                                                                  ByVal pIdProductoBodega As Integer) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransactiona As SqlTransaction = Nothing

        Existe_Lp_In_Stock_By_IdProductoBodega = False

        Try

            lConnection.Open() : lTransactiona = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Stock Where(UPPER(lic_plate) = UPPER(@lic_plate))  AND (IdBodega = @IdBodega) AND (IdProductoBodega = @IdProductoBodega)"
            Dim cmd As New SqlCommand(sp, lConnection, lTransactiona) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", pLic_plate))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdProductoBodega", pIdProductoBodega))

            Dim dt As New DataTable
            Dim pBeStock_rec As New clsBeStock_rec
            dad.Fill(dt)

            Existe_Lp_In_Stock_By_IdProductoBodega = dt.Rows.Count > 0

            lTransactiona.Commit()

        Catch ex As Exception
            If Not lTransactiona Is Nothing Then lTransactiona.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Existe_Lp_In_Stock(ByVal lic_plate As String, ByVal pConection As SqlConnection, ByVal pTransaction As SqlTransaction) As Boolean

        Existe_Lp_In_Stock = False

        Try

            Const sp As String = "SELECT * FROM Stock Where(lic_plate = @lic_plate) "

            Using lDTA As New SqlDataAdapter(sp, pConection)

                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@LIC_PLATE", lic_plate)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Existe_Lp_In_Stock = lDataTable.Rows.Count > 0

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdStock As Integer,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As clsBeStock

        GetSingle = Nothing

        Dim Obj As New clsBeStock

        Try

            Dim vSQL As String = "Select * from stock where stock.IdStock =@IdStock "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Obj = New clsBeStock

                    Dim lRow As DataRow = lDataTable.Rows(0)

                    Cargar(Obj, lRow)

                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#EJC20171022_0852AM: Ésta función estaba en la clase de cambio de ubicación, se movió para acá a donde pertenece
    Public Shared Function GetSingle(ByVal IdStock As Integer) As clsBeStock

        GetSingle = Nothing

        Try

            Dim Obj As New clsBeStock

            Dim vSQL As String = "Select * from stock where stock.IdStock = @IdStock "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Obj = New clsBeStock
                            Dim lRow As DataRow = lDataTable.Rows(0)
                            Cargar(Obj, lRow)
                            GetSingle = Obj

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

    Public Shared Function Actualizar_Stock_Por_Productos_Pickeados(ByRef pListObjStock As List(Of clsBeStock),
                                                                    ByRef lConnection As SqlConnection,
                                                                    ByRef lTransaction As SqlTransaction) As Integer

        Try

            Actualizar_Stock_Por_Productos_Pickeados = 0

            Dim lMaxS As Integer = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.            
            Dim objStockOrigen As New clsBeStock()
            Dim vCantidadDisponible As Double = 0
            Dim BePres As New clsBeProducto_Presentacion
            Dim vFilasAfectadas As Double = 0
            Dim IdxPresentacion As Integer = 0
            Dim BeMovimiento As New clsBeTrans_movimientos
            Dim vIdMaxMovimiento As Integer = clsLnTrans_movimientos.MaxID(lConnection, lTransaction)
            Dim IdEmpresa As Integer = 0

            If pListObjStock IsNot Nothing AndAlso pListObjStock.Count > 0 Then

                For Each ObjStockDestino As clsBeStock In pListObjStock

                    If IdEmpresa = 0 Then
                        IdEmpresa = clsLnBodega.Get_IdEmpresa_By_IdBodega(ObjStockDestino.IdBodega, lConnection, lTransaction)
                    End If

                    objStockOrigen = New clsBeStock()
                    objStockOrigen = GetSingle(IIf(ObjStockDestino.IdStockOrigen = 0,
                                                   ObjStockDestino.IdStock,
                                                   ObjStockDestino.IdStockOrigen),
                                                   lConnection,
                                                   lTransaction)


                    '#EJC20200115: El puto némesis, ocurrió por la falta de inicialización de la variable de la presentación!
                    BePres = New clsBeProducto_Presentacion
                    '#EJC20191216: Esta variable no se llenaba y se hacía un cagadal al devolver el stock.
                    BePres.IdPresentacion = ObjStockDestino.IdPresentacion

                    vCantidadDisponible = ObjStockDestino.Cantidad
                    ObjStockDestino.Cantidad = vCantidadDisponible

                    If objStockOrigen Is Nothing Then
                        Debug.Print("Aqui")
                    End If
                    objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)

                    lMaxS += 1

                    ObjStockDestino.IdStock = lMaxS

                    If BePres.IdPresentacion <> 0 Then

                        IdxPresentacion = lPresentacionesExistencia.FindIndex(Function(x) x.IdPresentacion = BePres.IdPresentacion)

                        If IdxPresentacion = -1 Then
                            clsLnProducto_presentacion.GetSingle(BePres, lConnection, lTransaction)
                            lPresentacionesExistencia.Add(BePres.Clone())
                        Else
                            BePres = lPresentacionesExistencia(IdxPresentacion).Clone()
                        End If

                        '#EJC20171018_1035AM: Llenar el objeto estado con el IdProductoEstado porque ese es el objeto que se utiliza para insertar el stock
                        ObjStockDestino.ProductoEstado.IdEstado = ObjStockDestino.IdProductoEstado
                        ObjStockDestino.IdPresentacion = BePres.IdPresentacion
                        ObjStockDestino.Presentacion.IdPresentacion = BePres.IdPresentacion
                        '#EJC20191226: Si la cantidad viene en presentación, se debe multiplicar por el factor para insertar en umbas en stock
                        ObjStockDestino.Cantidad = Math.Round(ObjStockDestino.Cantidad * BePres.Factor, 6)

                        If BePres.EsPallet Then
                            '#EJC20170918
                            vFilasAfectadas += ActualizaIdUbicacionPallet(objStockOrigen,
                                                                          ObjStockDestino.IdUbicacion,
                                                                          lConnection,
                                                                          lTransaction)
                        Else

                            ObjStockDestino.Fec_agr = Now : ObjStockDestino.Fec_mod = Now
                            vFilasAfectadas += Insertar(ObjStockDestino, lConnection, lTransaction)
                            objStockOrigen.Cantidad -= (ObjStockDestino.Cantidad)
                            objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)

                            If objStockOrigen.Cantidad = 0 Then
                                vFilasAfectadas += Eliminar(objStockOrigen,
                                                            lConnection,
                                                            lTransaction)
                            Else
                                vFilasAfectadas += Actualiza_Cantidad_Y_Peso(objStockOrigen,
                                                                             lConnection,
                                                                             lTransaction)
                            End If

                        End If

                    Else

                        If ObjStockDestino.Cantidad > 0 Then

                            '#EJC20171018_1035AM: Llenar el objeto estado con el IdProductoEstado porque ese es el objeto que se utiliza para insertar el stock
                            ObjStockDestino.ProductoEstado.IdEstado = ObjStockDestino.IdProductoEstado

                            ObjStockDestino.Fec_agr = Now : ObjStockDestino.Fec_mod = Now
                            'Insertar nuevo stock (ubicación/estado)
                            vFilasAfectadas += Insertar(ObjStockDestino, lConnection, lTransaction)

                            objStockOrigen.Cantidad -= ObjStockDestino.Cantidad
                            objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)

                            '#EJC20171018_0707PM: Eliminar IdStock con cantidad =0
                            If objStockOrigen.Cantidad = 0 Then
                                vFilasAfectadas += Eliminar(objStockOrigen,
                                                            lConnection,
                                                            lTransaction)
                                'Insertar en stock_histórico?
                            Else
                                '#EJC20171014_1140PM
                                vFilasAfectadas += Actualiza_Cantidad_Y_Peso(objStockOrigen,
                                                                      lConnection,
                                                                      lTransaction)
                            End If

                        End If

                    End If

                    '#EJC20220204: Registrar movimiento de cambio de ubicación hacia ubicación de picking.
                    BeMovimiento = New clsBeTrans_movimientos
                    BeMovimiento.IdMovimiento = vIdMaxMovimiento
                    BeMovimiento.IdEmpresa = IdEmpresa
                    BeMovimiento.IdBodegaOrigen = objStockOrigen.IdBodega
                    BeMovimiento.IdTransaccion = objStockOrigen.IdPedidoEnc
                    BeMovimiento.IdPropietarioBodega = objStockOrigen.IdPropietarioBodega
                    BeMovimiento.IdProductoBodega = objStockOrigen.IdProductoBodega
                    BeMovimiento.IdUbicacionOrigen = objStockOrigen.IdUbicacion
                    BeMovimiento.IdUbicacionDestino = ObjStockDestino.IdUbicacion
                    BeMovimiento.IdPresentacion = objStockOrigen.IdPresentacion
                    BeMovimiento.IdEstadoOrigen = objStockOrigen.IdProductoEstado
                    BeMovimiento.IdEstadoDestino = objStockOrigen.IdProductoEstado
                    BeMovimiento.IdUnidadMedida = objStockOrigen.IdUnidadMedida
                    BeMovimiento.IdTipoTarea = clsDataContractDI.tTipoTarea.UBIC_PICK
                    BeMovimiento.IdBodegaDestino = objStockOrigen.IdBodega
                    BeMovimiento.IdRecepcion = objStockOrigen.IdRecepcionEnc
                    BeMovimiento.IdRecepcionDet = objStockOrigen.IdRecepcionDet
                    BeMovimiento.Cantidad = ObjStockDestino.Cantidad
                    BeMovimiento.Serie = objStockOrigen.Serial
                    BeMovimiento.Peso = objStockOrigen.Peso
                    BeMovimiento.Lote = objStockOrigen.Lote
                    BeMovimiento.Barra_pallet = objStockOrigen.Lic_plate
                    BeMovimiento.Fecha_vence = objStockOrigen.Fecha_vence
                    BeMovimiento.Fecha_agr = Now
                    BeMovimiento.Usuario_agr = ObjStockDestino.User_agr

                    objStockOrigen.Presentacion.IdPresentacion = objStockOrigen.IdPresentacion
                    objStockOrigen.ProductoEstado.IdEstado = objStockOrigen.IdProductoEstado

                    Get_Existencia_By_IdProductoBodega(objStockOrigen,
                                                       BeMovimiento.Cantidad_hist,
                                                       BeMovimiento.Peso_hist,
                                                       lConnection,
                                                       lTransaction)

                    BeMovimiento.IdProductoTallaColor = objStockOrigen.IdProductoTallaColor

                    If objStockOrigen.IdProductoTallaColor <> 0 Then
                        Dim BEProductoTallaColor As New clsBeProducto_talla_color
                        BEProductoTallaColor = clsLnProducto_talla_color.GetSingle(objStockOrigen.IdProductoTallaColor)
                        BeMovimiento.Talla = If(clsLnTalla.GetSingle_By_IdTalla(BEProductoTallaColor.IdTalla)?.Codigo, "")
                        BeMovimiento.Color = If(clsLnColor.GetSingle_By_IdColor(BEProductoTallaColor.IdColor)?.Codigo, "")
                    Else
                        BeMovimiento.Talla = ""
                        BeMovimiento.Color = ""
                    End If

                    '#EJC20220303:Para Erik, si esto ocurre, algo se nos escapó, pero quiero prevenirte problemas así que mejor nos notifican cuando suceda esa
                    'SINGULARIDAD.-
                    If BeMovimiento.Cantidad = 0 Then
                        Throw New Exception("ERROR_20220303: La reversión de la ubicación de picking hacia la bodega, generaría un movimiento no válido.")
                    End If

                    clsLnTrans_movimientos.Insertar(BeMovimiento,
                                                    lConnection,
                                                    lTransaction)

                    vIdMaxMovimiento += 1

                Next

            End If

            Return vFilasAfectadas

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_PalletId_Por_Explosion(ByVal pIdBodega As Integer,
                                                             ByVal pIdUsuario As Integer,
                                                             ByVal pIdStock As Integer,
                                                             ByVal pIdMovimiento As Integer,
                                                             ByVal pLic_Plate_Ant As String,
                                                             ByVal pLic_Plate As String,
                                                             ByVal pIdResolucion As Integer) As Boolean


        Actualizar_PalletId_Por_Explosion = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vRegistrosAfectados As Integer = 0
        Dim clsBeRes_Operador As clsBeResolucion_lp_operador = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            vRegistrosAfectados = Actualizar_PalletId_By_IdStock(pIdStock,
                                                                 pLic_Plate,
                                                                 lConnection,
                                                                 lTransaction)

            If vRegistrosAfectados > 0 Then

                Dim BeMovPallet As New clsBeTrans_movimiento_pallet
                BeMovPallet.Idmovimientopallet = clsLnTrans_movimiento_pallet.MaxID(lConnection, lTransaction) + 1
                BeMovPallet.IdBodega = pIdBodega
                BeMovPallet.Lp_origen = pLic_Plate_Ant
                BeMovPallet.Lp_destino = pLic_Plate
                BeMovPallet.Fecha = Now
                BeMovPallet.Idusuario = pIdUsuario

                vRegistrosAfectados += clsLnTrans_movimiento_pallet.Insertar(BeMovPallet,
                                                                             lConnection,
                                                                             lTransaction)

                clsLnTrans_movimientos.Actualizar_LP_Por_Explosion(pIdMovimiento,
                                                                   pLic_Plate,
                                                                   lConnection,
                                                                   lTransaction)

                Dim BeNuevoStockGenerado As New clsBeStock
                BeNuevoStockGenerado = GetSingle(pIdStock,
                                                 lConnection,
                                                 lTransaction)

                Dim BeProdPallet As New clsBeProducto_pallet
                BeProdPallet.IdPallet = clsLnProducto_pallet.MaxID(lConnection, lTransaction) + 1
                BeProdPallet.IdPropietarioBodega = BeNuevoStockGenerado.IdPropietarioBodega
                BeProdPallet.IdProductoBodega = BeNuevoStockGenerado.IdProductoBodega
                BeProdPallet.IdPresentacion = BeNuevoStockGenerado.IdPresentacion
                BeProdPallet.IdRecepcionEnc = BeNuevoStockGenerado.IdRecepcionEnc
                BeProdPallet.IdRecepcionDet = BeNuevoStockGenerado.IdRecepcionDet
                BeProdPallet.IdOperadorBodega = pIdUsuario
                BeProdPallet.Impreso = 0
                BeProdPallet.IdImpresora = 0
                BeProdPallet.Activo = True
                BeProdPallet.Fecha_ingreso = Now
                BeProdPallet.Codigo_Barra = pLic_Plate
                BeProdPallet.Codigo_Producto = clsLnProducto.Get_Codigo_By_IdProductoBodega(BeProdPallet.IdProductoBodega, lConnection, lTransaction)
                BeProdPallet.Reimpresiones = 0
                BeProdPallet.Cantidad = BeNuevoStockGenerado.Cantidad
                BeProdPallet.Fecha_vence = BeNuevoStockGenerado.Fecha_vence
                BeProdPallet.Fec_agr = Now
                BeProdPallet.Fec_mod = Now
                BeProdPallet.Lote = BeNuevoStockGenerado.Lote
                BeProdPallet.User_agr = pIdUsuario
                BeProdPallet.User_mod = pIdUsuario
                BeProdPallet.IsNew = True
                clsLnProducto_pallet.Insertar(BeProdPallet, lConnection, lTransaction)

                'AT20220829 Actualizar correlativo actual en Resolucion_lp_operador
                clsBeRes_Operador = clsLnResolucion_lp_operador.GetSingle(pIdResolucion, lConnection, lTransaction)

                If clsBeRes_Operador IsNot Nothing Then
                    clsBeRes_Operador.Correlativo_Actual += 1
                    clsLnResolucion_lp_operador.Actualizar_Correlativo_Actual(clsBeRes_Operador, lConnection, lTransaction)
                End If

                '#MECR25112025: Se agrego bitacora de logs para reabastecimiento
                Dim msjInsercion As String = "Se creó reabastecimiento con licencia: " + pLic_Plate + " , Por el usuario: " + pIdUsuario.ToString
                clsLnLog_error_wms_reab.Agregar_Error(msjInsercion,
                                                      pIdBodega:=pIdBodega,
                                                      pIdStock:=pIdStock,
                                                      pIdMovimiento:=pIdMovimiento,
                                                      pLic_Plate_Anterior:=pLic_Plate_Ant,
                                                      pLic_Plate:=pLic_Plate,
                                                      pIdResolucion:=pIdResolucion,
                                                      pIdProductoBodega:=BeProdPallet.IdProductoBodega,
                                                      pCantidad:=BeProdPallet.Cantidad,
                                                      pUser_agr:=pIdUsuario,
                                                      pConection:=lConnection,
                                                      pTransaction:=lTransaction)

                Actualizar_PalletId_Por_Explosion = True

            Else
                Throw New Exception("ERR20191216: No se pudo actualizar el registro de stock: " & pIdStock & " con el nuevo número de pallet.")
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Private Shared Function Actualizar_PalletId_By_IdStock(ByVal pIdStock As Integer,
                                                      ByVal pLic_Plate As String,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As Integer

        Actualizar_PalletId_By_IdStock = 0

        Try

            Upd.Init("stock")
            Upd.Add("lic_plate", "@lic_plate", DataType.Parametro)
            Upd.Where("IdStock = @IdStock")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@IDSTOCK", pIdStock))
            cmd.Parameters.Add(New SqlParameter("@LIC_PLATE", pLic_Plate))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return rowsAffected

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub Actualizar_Stock_Por_Cambio_de_Ubicacion(ByRef pListObjStock As List(Of clsBeStock),
                                                               ByVal pListObjDet As List(Of clsBeTrans_ubic_hh_det),
                                                               ByVal EsReabasto As Boolean,
                                                               ByRef lConnection As SqlConnection,
                                                               ByRef lTransaction As SqlTransaction)

        Try

            Dim lMaxS As Integer
            Dim objStockOrigen As New clsBeStock()
            Dim objStockHist As New clsBeStock_hist()
            Dim vCantidadDisponible As Double = 0
            Dim BePres As New clsBeProducto_Presentacion
            Dim vTransUbicHHDet As New clsBeTrans_ubic_hh_det
            Dim ParametrosAActualizar As New List(Of clsBeStock_parametro)
            Dim ParametroAActualizar As New clsBeStock_parametro
            Dim vCantidadInicial As Double = 0

            If pListObjStock IsNot Nothing AndAlso pListObjStock.Count > 0 Then

                For Each ObjStockDestino As clsBeStock In pListObjStock

                    '#EJC20171018_0829: Validar que aún no se ha realizado el cambio en la HH.
                    vTransUbicHHDet = pListObjDet.Find(Function(x) x.IdStock = ObjStockDestino.IdStock AndAlso x.IdUbicacionDestino = ObjStockDestino.IdUbicacion)

                    If Not vTransUbicHHDet Is Nothing Then

                        If Not (vTransUbicHHDet.Realizado) Then

                            '#EJC20171023_0852AM_REF: Obtener el stock con parámetros para actualizar el IdStock
                            objStockOrigen = Get_Single_Stock_By_IdStock(IIf(ObjStockDestino.IdStockOrigen = 0, ObjStockDestino.IdStock, ObjStockDestino.IdStockOrigen), lConnection, lTransaction)

                            '#EJC20180625: Mantener copia del stock original
                            clsPublic.CopyObject(objStockOrigen, objStockHist)

                            '#EJC20171024_0629PM:Colocar el IdPresentacion en el objeto de presentación porque ese es el que se uitlizar para el insert, si no se tiene idpresentación Null en stock cuando se hace cambio de ubicación/estado.
                            ObjStockDestino.Presentacion.IdPresentacion = ObjStockDestino.IdPresentacion

                            BePres.IdPresentacion = ObjStockDestino.IdPresentacion

                            '#EJC20171024_0531PM: La presentación del stock origen y el destino deberían ser la misma (de momento no se me ocurre otro escenario)                            
                            BePres = objStockOrigen.Presentacion

                            '#EJC20170913
                            If BePres.IdPresentacion <> 0 Then

                                BePres.IdPresentacion = ObjStockDestino.IdPresentacion

                                'Llevar la cantidad de la presentación a UMBas antes de insertar #EJC20170910
                                If BePres.EsPallet Then
                                    vCantidadDisponible = Math.Round((ObjStockDestino.Cantidad * BePres.Factor * BePres.CamasPorTarima * BePres.CajasPorCama), 6)
                                Else
                                    If EsReabasto Then
                                        vCantidadDisponible = ObjStockDestino.Cantidad
                                    Else
                                        '#EJC20240602 Si lo necesitan modificar avisen!
                                        'vCantidadDisponible = Math.Round((ObjStockDestino.Cantidad * BePres.Factor), 6)
                                        vCantidadDisponible = ObjStockDestino.Cantidad
                                    End If

                                End If

                            Else
                                vCantidadDisponible = ObjStockDestino.Cantidad
                            End If

                            vCantidadInicial = ObjStockDestino.Cantidad

                            ObjStockDestino.Cantidad = vCantidadDisponible

                            lMaxS = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.

                            ObjStockDestino.IdStock = lMaxS

                            '#EJC20171023_0217PM: Validar si tiene parámetros el stock
                            If Not objStockOrigen.Parametros Is Nothing Then

                                '#EJC20171023_0158PM: Actualizar el IdStock en parámetros.
                                For Each StockParam In objStockOrigen.Parametros
                                    ParametroAActualizar = New clsBeStock_parametro
                                    ParametroAActualizar = StockParam
                                    ParametrosAActualizar.Add(StockParam)
                                Next

                            End If

                            If BePres.IdPresentacion <> 0 Then

                                'Se cambio porque en cambio de estado desde BOF IdEstado tenía el estado origen, ver -> '#EJC20171018_1035AM_REF
                                ObjStockDestino.ProductoEstado.IdEstado = vTransUbicHHDet.IdEstadoDestino
                                ObjStockDestino.IdProductoEstado = vTransUbicHHDet.IdEstadoDestino

                                If BePres.EsPallet Then
                                    '#EJC20170918                                    
                                    ActualizaIdUbicacionPallet(objStockOrigen, ObjStockDestino.IdUbicacion, lConnection, lTransaction)
                                Else

                                    Insertar(ObjStockDestino, lConnection, lTransaction)

                                    If BePres.IdPresentacion <> 0 Then

                                        'Llevar la cantidad de la presentación a UMBas antes de insertar #EJC20170910
                                        If BePres.EsPallet Then
                                            objStockOrigen.Cantidad -= Math.Round((ObjStockDestino.Cantidad / BePres.Factor / BePres.CamasPorTarima / BePres.CajasPorCama), 6)
                                        Else
                                            If EsReabasto Then
                                                objStockOrigen.Cantidad -= ObjStockDestino.Cantidad
                                            Else
                                                '#EJC20240602 Si lo necesitan modificar avisen!
                                                objStockOrigen.Cantidad -= ObjStockDestino.Cantidad
                                            End If

                                        End If

                                    Else
                                        objStockOrigen.Cantidad -= ObjStockDestino.Cantidad
                                    End If

                                    objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)

                                    '#EJC20171024_0613PM: Si la cantidad es 0 eliminar el id stock cuando tiene presentación
                                    If objStockOrigen.Cantidad = 0 Then

                                        '#EJC20171023_0216PM: Insertar siempre una copia de los parámetros con el nuevo IdStock
                                        clsLnStock_parametro.Eliminar_Todos_By_IdStock(objStockOrigen.IdStock, lConnection, lTransaction)
                                        Eliminar(objStockOrigen, lConnection, lTransaction)

                                        '#EJC20180625:1036AM => Insertar stock historico de despacho antes de eliminarlo                                        
                                        objStockHist.IdStockHist = clsLnStock_hist.MaxID(lConnection, lTransaction) + 1
                                        objStockHist.IdNuevoStock = ObjStockDestino.IdStock
                                        objStockHist.IdPedidoEnc = objStockOrigen.IdPedidoEnc
                                        objStockHist.IdPickingEnc = objStockOrigen.IdPickingEnc
                                        objStockHist.IdUbicacion_anterior = objStockOrigen.IdUbicacion
                                        objStockHist.IdUbicacion = ObjStockDestino.IdUbicacion
                                        objStockHist.IdDespachoEnc = 0
                                        objStockHist.Fec_agr = Now
                                        objStockHist.Fec_mod = Now
                                        objStockHist.IdProductoTallaColor = ObjStockDestino.IdProductoTallaColor
                                        clsLnStock_hist.Insertar(objStockHist, lConnection, lTransaction)
                                        '#EJC20180625:1036AM => Fin_Stock_Hist

                                    Else
                                        '#EJC20171014_1140PM
                                        Actualiza_Cantidad_Y_Peso(objStockOrigen, lConnection, lTransaction)
                                    End If

                                End If

                            Else

                                'Se cambio porque en cambio de estado desde BOF IdEstado tenía el estado origen, ver -> '#EJC20171018_1035AM_REF
                                ObjStockDestino.ProductoEstado.IdEstado = vTransUbicHHDet.IdEstadoDestino
                                ObjStockDestino.IdProductoEstado = vTransUbicHHDet.IdEstadoDestino

                                'Insertar nuevo stock (ubicación/estado)
                                Insertar(ObjStockDestino, lConnection, lTransaction)

                                If ParametrosAActualizar.Count > 0 Then
                                    '#EJC20171023_0216PM: Insertar siempre una copia de los parámetros con el nuevo IdStock
                                    clsLnStock_parametro.Insertar_Stock_Parametro_Cambio_Ubicacion(ParametrosAActualizar, lMaxS, lConnection, lTransaction)
                                End If

                                objStockOrigen.Cantidad -= ObjStockDestino.Cantidad
                                objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)

                                '#EJC20171018_0707PM: Eliminar IdStock con cantidad =0
                                If objStockOrigen.Cantidad = 0 Then

                                    '#EJC20180625:1036AM => Insertar stock historico de despacho antes de eliminarlo                                    
                                    objStockHist.IdStockHist = clsLnStock_hist.MaxID(lConnection, lTransaction) + 1
                                    objStockHist.IdNuevoStock = ObjStockDestino.IdStock
                                    objStockHist.IdPedidoEnc = objStockOrigen.IdPedidoEnc
                                    objStockHist.IdPickingEnc = objStockOrigen.IdPickingEnc
                                    objStockHist.IdUbicacion_anterior = objStockOrigen.IdUbicacion
                                    objStockHist.IdUbicacion = ObjStockDestino.IdUbicacion
                                    objStockHist.IdDespachoEnc = 0
                                    objStockHist.Fec_agr = Now
                                    objStockHist.Fec_mod = Now
                                    objStockHist.IdProductoTallaColor = ObjStockDestino.IdProductoTallaColor
                                    clsLnStock_hist.Insertar(objStockHist, lConnection, lTransaction)
                                    '#EJC20180625:1036AM => Fin_Stock_Hist                                    

                                    clsLnStock_parametro.Eliminar_Todos_By_IdStock(objStockOrigen.IdStock, lConnection, lTransaction)
                                    Eliminar(objStockOrigen, lConnection, lTransaction)

                                Else
                                    '#EJC20171014_1140PM
                                    Actualiza_Cantidad_Y_Peso(objStockOrigen, lConnection, lTransaction)
                                End If

                            End If

                            vTransUbicHHDet.Realizado = True

                            '#EJC20170913
                            clsLnTrans_ubic_hh_det.Actualizar(vTransUbicHHDet, lConnection, lTransaction)

                        Else
                            Throw New Exception("La tarea está realizada, nothing to do")
                        End If

                    Else
                        Throw New Exception("Reporte éste error a desarrollo, no se pudo obtener el IdStock antes de actualizar el stock " & ObjStockDestino.IdStock)
                    End If

                Next

            Else
                Throw New Exception("Lista vacía para actualización de stock, se desconoce si esta es una condición válida, Erik C. 20181211 ")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            'If Not file Is Nothing Then file.Close()
        End Try

    End Sub

    Public Shared Sub Actualizar_Stock_Por_Cambio_de_Ubicacion_Traslado(ByRef pListObjStock As List(Of clsBeStock),
                                                                        ByVal pListObjDet As List(Of clsBeTrans_ubic_hh_det),
                                                                        ByRef pListStockDestino As List(Of clsBeStock),
                                                                        ByRef lConnection As SqlConnection,
                                                                        ByRef lTransaction As SqlTransaction)

        Try

            Dim lMaxS As Integer
            Dim objStockOrigen As New clsBeStock()
            Dim objStockHist As New clsBeStock_hist()
            Dim vCantidadDisponible As Double = 0
            Dim BePres As New clsBeProducto_Presentacion
            Dim vTransUbicHHDet As New clsBeTrans_ubic_hh_det
            Dim ParametrosAActualizar As New List(Of clsBeStock_parametro)
            Dim ParametroAActualizar As New clsBeStock_parametro
            Dim vCantidadInicial As Double = 0

            If pListObjStock IsNot Nothing AndAlso pListObjStock.Count > 0 Then

                For Each ObjStockDestino As clsBeStock In pListObjStock

                    '#EJC20171018_0829: Validar que aún no se ha realizado el cambio en la HH.
                    vTransUbicHHDet = pListObjDet.Find(Function(x) x.IdStock = ObjStockDestino.IdStock AndAlso x.IdUbicacionDestino = ObjStockDestino.IdUbicacion)

                    If Not vTransUbicHHDet Is Nothing Then

                        If Not (vTransUbicHHDet.Realizado) Then

                            '#EJC20171023_0852AM_REF: Obtener el stock con parámetros para actualizar el IdStock
                            objStockOrigen = Get_Single_Stock_By_IdStock(IIf(ObjStockDestino.IdStockOrigen = 0, ObjStockDestino.IdStock, ObjStockDestino.IdStockOrigen), lConnection, lTransaction)

                            '#EJC20180625: Mantener copia del stock original
                            clsPublic.CopyObject(objStockOrigen, objStockHist)

                            '#EJC20171024_0629PM:Colocar el IdPresentacion en el objeto de presentación porque ese es el que se uitlizar para el insert, si no se tiene idpresentación Null en stock cuando se hace cambio de ubicación/estado.
                            ObjStockDestino.Presentacion.IdPresentacion = ObjStockDestino.IdPresentacion

                            BePres.IdPresentacion = ObjStockDestino.IdPresentacion

                            '#EJC20171024_0531PM: La presentación del stock origen y el destino deberían ser la misma (de momento no se me ocurre otro escenario)                            
                            BePres = objStockOrigen.Presentacion

                            '#EJC20170913
                            If BePres.IdPresentacion <> 0 Then

                                BePres.IdPresentacion = ObjStockDestino.IdPresentacion

                                'Llevar la cantidad de la presentación a UMBas antes de insertar #EJC20170910
                                If BePres.EsPallet Then
                                    vCantidadDisponible = Math.Round((ObjStockDestino.Cantidad * BePres.Factor * BePres.CamasPorTarima * BePres.CajasPorCama), 6)
                                Else
                                    'vCantidadDisponible = Math.Round((ObjStockDestino.Cantidad * BePres.Factor), 6)
                                    vCantidadDisponible = ObjStockDestino.Cantidad
                                End If

                            Else
                                vCantidadDisponible = ObjStockDestino.Cantidad
                            End If

                            vCantidadInicial = ObjStockDestino.Cantidad

                            ObjStockDestino.Cantidad = vCantidadDisponible

                            lMaxS = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.

                            ObjStockDestino.IdStock = lMaxS

                            '#EJC20171023_0217PM: Validar si tiene parámetros el stock
                            If Not objStockOrigen.Parametros Is Nothing Then

                                '#EJC20171023_0158PM: Actualizar el IdStock en parámetros.
                                For Each StockParam In objStockOrigen.Parametros
                                    ParametroAActualizar = New clsBeStock_parametro
                                    ParametroAActualizar = StockParam
                                    ParametrosAActualizar.Add(StockParam)
                                Next

                            End If

                            If BePres.IdPresentacion <> 0 Then

                                'Se cambio porque en cambio de estado desde BOF IdEstado tenía el estado origen, ver -> '#EJC20171018_1035AM_REF
                                ObjStockDestino.ProductoEstado.IdEstado = vTransUbicHHDet.IdEstadoDestino
                                ObjStockDestino.IdProductoEstado = vTransUbicHHDet.IdEstadoDestino

                                If BePres.EsPallet Then
                                    '#EJC20170918                                    
                                    ActualizaIdUbicacionPallet(objStockOrigen, ObjStockDestino.IdUbicacion, lConnection, lTransaction)
                                Else

                                    Insertar(ObjStockDestino, lConnection, lTransaction)
                                    pListStockDestino.Add(ObjStockDestino)

                                    '#EJC20171024_0610PM: Se quitó la multiplicación por cantidad porque canto objStockOrigen.Cantidad como ObjStockDestino.Cantidad  están en unidad de medida básica.
                                    objStockOrigen.Cantidad -= ObjStockDestino.Cantidad
                                    objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)

                                    '#EJC20171024_0613PM: Si la cantidad es 0 eliminar el id stock cuando tiene presentación
                                    If objStockOrigen.Cantidad = 0 Then

                                        '#EJC20171023_0216PM: Insertar siempre una copia de los parámetros con el nuevo IdStock
                                        clsLnStock_parametro.Eliminar_Todos_By_IdStock(objStockOrigen.IdStock, lConnection, lTransaction)
                                        Eliminar(objStockOrigen, lConnection, lTransaction)

                                        '#EJC20180625:1036AM => Insertar stock historico de despacho antes de eliminarlo                                        
                                        objStockHist.IdStockHist = clsLnStock_hist.MaxID(lConnection, lTransaction) + 1
                                        objStockHist.IdNuevoStock = ObjStockDestino.IdStock
                                        objStockHist.IdPedidoEnc = objStockOrigen.IdPedidoEnc
                                        objStockHist.IdPickingEnc = objStockOrigen.IdPickingEnc
                                        objStockHist.IdUbicacion_anterior = objStockOrigen.IdUbicacion
                                        objStockHist.IdUbicacion = ObjStockDestino.IdUbicacion
                                        objStockHist.IdDespachoEnc = 0
                                        objStockHist.Fec_agr = Now
                                        objStockHist.Fec_mod = Now
                                        objStockHist.IdProductoTallaColor = ObjStockDestino.IdProductoTallaColor
                                        clsLnStock_hist.Insertar(objStockHist, lConnection, lTransaction)
                                        '#EJC20180625:1036AM => Fin_Stock_Hist

                                    Else
                                        '#EJC20171014_1140PM
                                        Actualiza_Cantidad_Y_Peso(objStockOrigen, lConnection, lTransaction)
                                    End If

                                End If

                            Else

                                '#EJC20171018_1035AM_REF: Llenar el objeto estado con el IdProductoEstado porque ese es el objeto que se utiliza para insertar el stock
                                'ObjStockDestino.Estado.IdEstado = ObjStockDestino.IdProductoEstado

                                'Se cambio porque en cambio de estado desde BOF IdEstado tenía el estado origen, ver -> '#EJC20171018_1035AM_REF
                                ObjStockDestino.ProductoEstado.IdEstado = vTransUbicHHDet.IdEstadoDestino
                                ObjStockDestino.IdProductoEstado = vTransUbicHHDet.IdEstadoDestino

                                'Insertar nuevo stock (ubicación/estado)
                                Insertar(ObjStockDestino, lConnection, lTransaction)
                                pListStockDestino.Add(ObjStockDestino)

                                If ParametrosAActualizar.Count > 0 Then
                                    '#EJC20171023_0216PM: Insertar siempre una copia de los parámetros con el nuevo IdStock
                                    clsLnStock_parametro.Insertar_Stock_Parametro_Cambio_Ubicacion(ParametrosAActualizar, lMaxS, lConnection, lTransaction)
                                End If

                                objStockOrigen.Cantidad -= ObjStockDestino.Cantidad
                                objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)

                                '#EJC20171018_0707PM: Eliminar IdStock con cantidad =0
                                If objStockOrigen.Cantidad = 0 Then

                                    '#EJC20180625:1036AM => Insertar stock historico de despacho antes de eliminarlo                                    
                                    objStockHist.IdStockHist = clsLnStock_hist.MaxID(lConnection, lTransaction) + 1
                                    objStockHist.IdNuevoStock = ObjStockDestino.IdStock
                                    objStockHist.IdPedidoEnc = objStockOrigen.IdPedidoEnc
                                    objStockHist.IdPickingEnc = objStockOrigen.IdPickingEnc
                                    objStockHist.IdUbicacion_anterior = objStockOrigen.IdUbicacion
                                    objStockHist.IdUbicacion = ObjStockDestino.IdUbicacion
                                    objStockHist.IdDespachoEnc = 0
                                    objStockHist.Fec_agr = Now
                                    objStockHist.Fec_mod = Now
                                    objStockHist.IdProductoTallaColor = ObjStockDestino.IdProductoTallaColor
                                    clsLnStock_hist.Insertar(objStockHist, lConnection, lTransaction)
                                    '#EJC20180625:1036AM => Fin_Stock_Hist

                                    'Insertar en stock_histórico?
                                    '#EJC20171023_0216PM: Insertar siempre una copia de los parámetros con el nuevo IdStock

                                    clsLnStock_parametro.Eliminar_Todos_By_IdStock(objStockOrigen.IdStock, lConnection, lTransaction)
                                    Eliminar(objStockOrigen, lConnection, lTransaction)

                                Else
                                    '#EJC20171014_1140PM
                                    Actualiza_Cantidad_Y_Peso(objStockOrigen, lConnection, lTransaction)
                                End If

                            End If

                            vTransUbicHHDet.Realizado = True

                            '#EJC20170913
                            clsLnTrans_ubic_hh_det.Actualizar(vTransUbicHHDet, lConnection, lTransaction)

                        Else
                            Throw New Exception("La tarea está realizada, nothing to do")
                        End If

                    Else
                        Throw New Exception("Reporte éste error a desarrollo, no se pudo obtener el IdStock antes de actualizar el stock " & ObjStockDestino.IdStock)
                    End If

                Next

            Else
                Throw New Exception("Lista vacía para actualización de stock, se desconoce si esta es una condición válida, Erik C. 20181211 ")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            'If Not file Is Nothing Then file.Close()
        End Try

    End Sub

    Public Shared Sub Actualizar_Stock_Por_Despacho(ByVal IdDespachoEnc As Integer,
                                                    ByRef pPickingUbic As clsBeTrans_picking_ubic,
                                                    ByVal AllowNegativeExceptionOnStock As Boolean,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction)

        Dim vMensaje As String = ""
        Dim vCantidadDisponible As Double = 0

        Try

            Dim objStockOrigen As clsBeStock = Get_Single_Stock_By_IdStock_And_IdProducto_Bodega(pPickingUbic.IdStock,
                                                                                            pPickingUbic.IdProductoBodega,
                                                                                            lConnection,
                                                                                            lTransaction)

            Dim objStockDet As clsBeStock_det = clsLnStock_det.GetSingle(pPickingUbic.IdStock, lConnection, lTransaction)

            If objStockOrigen Is Nothing Then
                Dim vMsgError As String = String.Format("{0} Stock origen no encontrado. IdStock:{1} IdProductoBodega:{2}",
                                                    MethodBase.GetCurrentMethod.Name(),
                                                    pPickingUbic.IdStock,
                                                    pPickingUbic.IdProductoBodega)
                clsLnLog_error_wms.Agregar_Error(vMsgError)
                Exit Sub
            End If

            Dim BeTransPeDet As New clsBeTrans_pe_det With {.IdPedidoDet = pPickingUbic.IdPedidoDet}
            clsLnTrans_pe_det.GetSingle(BeTransPeDet, lConnection, lTransaction)

            If (objStockOrigen.Presentacion.IdPresentacion = 0) OrElse (BeTransPeDet.IdPresentacion = 0) Then
                vCantidadDisponible = Math.Round(objStockOrigen.Cantidad, 6)
            Else

                If objStockOrigen.Presentacion.EsPallet Then
                    vCantidadDisponible = Math.Round((objStockOrigen.Cantidad * objStockOrigen.Presentacion.Factor * objStockOrigen.Presentacion.CamasPorTarima * objStockOrigen.Presentacion.CajasPorCama), 6)
                Else

                    '#EJC20251209 Originalmente estaba asi
                    'vCantidadDisponible = Math.Round((objStockOrigen.Cantidad / objStockOrigen.Presentacion.Factor), 6)

                    '#EJC20251209 Agregó validación por error reportado en el despacho
                    If objStockOrigen.Cantidad >= objStockOrigen.Presentacion.Factor Then
                        vCantidadDisponible = Math.Round((objStockOrigen.Cantidad / objStockOrigen.Presentacion.Factor), 6)
                    Else
                        vCantidadDisponible = objStockOrigen.Cantidad
                    End If

                End If

            End If

            Dim vCantidadSolicitadaPedido As Double = BeTransPeDet.Cantidad
            Dim vCantPosiciones As Double = If(objStockDet IsNot Nothing, objStockDet.Posiciones, 1)

            Dim vMensajeSingularidadPalletCealsa As String =
            String.Format("Hipotéticamente: lo que solicita no debería ocurrir:
En teoría se está despachando un pallet, el prorrateo de las posiciones que ocupa no permite un despacho parcial del mismo, 
la cantidad restante de posiciones resulta en un valor no entero no válido para la cantida de posiciones que ocupa al despacharle parcialmente.
El IdStock: {0} reporta una cantidad: {1} de la que se intentarán restar: {2}, 
pero se cuentan con {3} posiciones esto generaría una cantidad de posiciones decimales no válidas para el sistema, 
de forma preventiva se ha restringido el despacho de este documento,
En verdad, lamento interrumpir el proceso, lo mejor que puedo hacer es eliminar las posiciones ocupadas por la mercancía, proceder de esta forma?.",
                          objStockOrigen.IdStock, objStockOrigen.Cantidad, pPickingUbic.Cantidad_Verificada, vCantPosiciones)

            vMensaje = String.Format("No se puede realizar el despacho,
éste es un error poco usual y estamos trabajando para resolverlo.
El IdStock: {0} reporta una cantidad: {1} de la que se intentarán restar: {2},
esto generaría un stock negativo no válido para el sistema, de forma preventiva se ha restringido el despacho de este documento.",
                                 objStockOrigen.IdStock, objStockOrigen.Cantidad, pPickingUbic.Cantidad_Verificada)

            Dim vMensaje1 As String =
            "No se puede realizar el despacho,
éste es un error poco usual y estamos trabajando para resolverlo.
El Picking reporta una cantidad verificada mayor a la solicitada en el pedido,
esto generaría un desfase de stock no válido para el sistema, de forma preventiva se ha restringido el despacho de este documento.
Por favor reportar este problema a DevOps."

            '=========================================================
            ' Validación: no despachar más de lo solicitado en el pedido
            '=========================================================
            If pPickingUbic.IdPresentacion = BeTransPeDet.IdPresentacion Then

                If (pPickingUbic.Cantidad_Verificada > vCantidadSolicitadaPedido) AndAlso Not AllowNegativeExceptionOnStock Then
                    Throw New Exception(vMensaje1)
                End If

            Else

                'Picking con presentación y pedido sin presentación -> convertir a UMBAS
                If (pPickingUbic.IdPresentacion <> 0) AndAlso (BeTransPeDet.IdPresentacion = 0) Then

                    Dim vFactor As Integer = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pPickingUbic.IdProductoBodega,
                                                                                                  pPickingUbic.IdPresentacion,
                                                                                                  lConnection,
                                                                                                  lTransaction)
                    Dim vCantidadVerificada As Double = pPickingUbic.Cantidad_Verificada * vFactor

                    If (vCantidadVerificada > vCantidadSolicitadaPedido) AndAlso Not AllowNegativeExceptionOnStock Then
                        Throw New Exception(vMensaje1)
                    End If

                    'Picking sin presentación y pedido con presentación -> convertir a "presentaciones" o asegurar unidad
                ElseIf (pPickingUbic.IdPresentacion = 0) AndAlso (BeTransPeDet.IdPresentacion <> 0) Then

                    Dim vFactor As Integer = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pPickingUbic.IdProductoBodega,
                                                                                                  BeTransPeDet.IdPresentacion,
                                                                                                  lConnection,
                                                                                                  lTransaction)
                    If vFactor <= 0 Then Throw New Exception("El factor de la presentación del pedido es 0, no se puede validar cantidades.")

                    Dim vCantidadVerificada As Double = pPickingUbic.Cantidad_Verificada / vFactor

                    If (vCantidadVerificada > vCantidadSolicitadaPedido) AndAlso Not AllowNegativeExceptionOnStock Then
                        Throw New Exception(vMensaje1)
                    End If

                Else
                    If (pPickingUbic.Cantidad_Verificada > vCantidadSolicitadaPedido) AndAlso Not AllowNegativeExceptionOnStock Then
                        Throw New Exception(vMensaje1)
                    End If
                End If

            End If

            '=========================================================
            ' Diferencia por despacho parcial
            '=========================================================
            Dim vDiferenciaDespachoParcial As Double = pPickingUbic.Cantidad_Verificada - pPickingUbic.Cantidad_despachada
            If vDiferenciaDespachoParcial < 0.00000000001 Then vDiferenciaDespachoParcial = 0

            If vCantidadDisponible < 0 OrElse ((vDiferenciaDespachoParcial > vCantidadDisponible) AndAlso Not AllowNegativeExceptionOnStock) Then
                Throw New Exception(vMensaje)
            End If

            Dim objStockHist As New clsBeStock_hist()

            '=========================================================
            ' Actualización de stock
            '=========================================================
            If (objStockOrigen.Presentacion.IdPresentacion <> 0) AndAlso (BeTransPeDet.IdPresentacion <> 0) Then

                If objStockOrigen.Presentacion.EsPallet Then

                    Despacha_Pallet(objStockOrigen, lConnection, lTransaction)

                Else

                    Dim Res As Double = Math.Round(vCantidadDisponible - vDiferenciaDespachoParcial, 6)
                    objStockOrigen.Cantidad = Math.Round(Res * objStockOrigen.Presentacion.Factor, 6)

                    'FIX: descontar una sola vez
                    objStockOrigen.Peso = Math.Round(objStockOrigen.Peso - Math.Round(pPickingUbic.Peso_verificado, 6), 6)

                    If objStockOrigen.Cantidad = 0 Then

                        clsPublic.CopyObject(objStockOrigen, objStockHist)
                        objStockHist.IdStockHist = clsLnStock_hist.MaxID(lConnection, lTransaction) + 1
                        objStockHist.IdPedidoEnc = pPickingUbic.IdPedidoEnc
                        objStockHist.IdPickingEnc = pPickingUbic.IdPickingEnc
                        objStockHist.IdDespachoEnc = IdDespachoEnc
                        objStockHist.IdNuevoStock = 0
                        objStockHist.Cantidad += Math.Round(pPickingUbic.Cantidad_Verificada, 6)
                        objStockHist.Peso += Math.Round(pPickingUbic.Peso_verificado, 6)
                        objStockHist.Fec_agr = Now
                        objStockHist.Fec_mod = Now
                        objStockHist.IdProductoTallaColor = pPickingUbic.IdProductoTallaColor
                        clsLnStock_hist.Insertar(objStockHist, lConnection, lTransaction)

                        clsLnStock_parametro.Eliminar_Todos_By_IdStock(objStockOrigen.IdStock, lConnection, lTransaction)
                        Eliminar(objStockOrigen, lConnection, lTransaction)

                        If objStockDet IsNot Nothing Then clsLnStock_det.Eliminar(objStockDet, lConnection, lTransaction)

                    ElseIf objStockOrigen.Cantidad > 0 Then

                        Actualiza_Cantidad_Y_Peso(objStockOrigen, lConnection, lTransaction)

                    ElseIf objStockOrigen.Cantidad < 0 Then

                        If AllowNegativeExceptionOnStock Then
                            clsPublic.CopyObject(objStockOrigen, objStockHist)
                            objStockHist.IdStockHist = clsLnStock_hist.MaxID(lConnection, lTransaction) + 1
                            objStockHist.IdPedidoEnc = pPickingUbic.IdPedidoEnc
                            objStockHist.IdPickingEnc = pPickingUbic.IdPickingEnc
                            objStockHist.IdDespachoEnc = IdDespachoEnc
                            objStockHist.IdNuevoStock = 0
                            objStockHist.Cantidad += Math.Round(pPickingUbic.Cantidad_Verificada, 6)
                            objStockHist.Peso += Math.Round(pPickingUbic.Peso_verificado, 6)
                            objStockHist.Fec_agr = Now
                            objStockHist.Fec_mod = Now
                            objStockHist.IdProductoTallaColor = pPickingUbic.IdProductoTallaColor
                            clsLnStock_hist.Insertar(objStockHist, lConnection, lTransaction)
                        End If

                        If objStockDet IsNot Nothing Then
                            If objStockDet.Posiciones <> Math.Round(pPickingUbic.Cantidad_Verificada, 6) Then
                                If MsgBox(vMensajeSingularidadPalletCealsa, MsgBoxStyle.YesNo, "AllowNegativeExceptionOnStock") = MsgBoxResult.Yes Then
                                    clsLnStock_det.Eliminar(objStockDet, lConnection, lTransaction)
                                End If
                            Else
                                clsLnStock_det.Eliminar(objStockDet, lConnection, lTransaction)
                            End If
                        End If

                    End If

                End If

            Else

                Dim vFactorPres As Integer = 0
                If pPickingUbic.IdPresentacion <> 0 Then
                    vFactorPres = clsLnProducto_presentacion.Get_Factor_By_IdProductoBodega(pPickingUbic.IdProductoBodega,
                                                                                       pPickingUbic.IdPresentacion,
                                                                                       lConnection,
                                                                                       lTransaction)
                    If vFactorPres = 0 Then Throw New Exception("El factor de la presentación es 0 no se puede continuar el proceso de actualización de Stock")
                End If

                If pPickingUbic.IdPresentacion <> 0 Then
                    objStockOrigen.Cantidad -= (pPickingUbic.Cantidad_Verificada - pPickingUbic.Cantidad_despachada) * vFactorPres
                Else
                    objStockOrigen.Cantidad -= (pPickingUbic.Cantidad_Verificada - pPickingUbic.Cantidad_despachada)
                End If

                objStockOrigen.Peso -= (pPickingUbic.Peso_verificado - pPickingUbic.Peso_despachado)
                objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)

                If objStockOrigen.Cantidad = 0 Then

                    clsPublic.CopyObject(objStockOrigen, objStockHist)
                    objStockHist.IdStockHist = clsLnStock_hist.MaxID(lConnection, lTransaction) + 1
                    objStockHist.IdPedidoEnc = pPickingUbic.IdPedidoEnc
                    objStockHist.IdPickingEnc = pPickingUbic.IdPickingEnc
                    objStockHist.IdDespachoEnc = IdDespachoEnc
                    objStockHist.IdNuevoStock = 0

                    If pPickingUbic.IdPresentacion <> 0 Then
                        objStockHist.Cantidad += Math.Round(pPickingUbic.Cantidad_Verificada * vFactorPres, 6)
                    Else
                        objStockHist.Cantidad += Math.Round(pPickingUbic.Cantidad_Verificada, 6)
                    End If

                    objStockHist.Peso += Math.Round(pPickingUbic.Peso_verificado, 6)
                    objStockHist.Fec_agr = Now
                    objStockHist.Fec_mod = Now
                    objStockHist.Posiciones = vCantPosiciones
                    objStockHist.IdProductoTallaColor = pPickingUbic.IdProductoTallaColor
                    clsLnStock_hist.Insertar(objStockHist, lConnection, lTransaction)

                    clsLnStock_parametro.Eliminar_Todos_By_IdStock(objStockOrigen.IdStock, lConnection, lTransaction)
                    Eliminar(objStockOrigen, lConnection, lTransaction)

                    If objStockDet IsNot Nothing Then clsLnStock_det.Eliminar(objStockDet, lConnection, lTransaction)

                ElseIf objStockOrigen.Cantidad > 0 Then

                    Actualiza_Cantidad_Y_Peso(objStockOrigen, lConnection, lTransaction)

                    If objStockDet IsNot Nothing Then
                        If objStockDet.Posiciones <> Math.Round(pPickingUbic.Cantidad_Verificada, 6) Then
                            If MsgBox(vMensajeSingularidadPalletCealsa, MsgBoxStyle.YesNo, "AllowNegativeExceptionOnStock") = MsgBoxResult.Yes Then
                                clsLnStock_det.Eliminar(objStockDet, lConnection, lTransaction)
                            Else
                                Throw New Exception(vMensajeSingularidadPalletCealsa)
                            End If
                        Else
                            clsLnStock_det.Eliminar(objStockDet, lConnection, lTransaction)
                        End If
                    End If

                ElseIf objStockOrigen.Cantidad < 0 Then

                    If MsgBox(vMensaje, MsgBoxStyle.YesNo, "AllowNegativeExceptionOnStock") = MsgBoxResult.Yes Then
                        clsPublic.CopyObject(objStockOrigen, objStockHist)
                        objStockHist.IdStockHist = clsLnStock_hist.MaxID(lConnection, lTransaction) + 1
                        objStockHist.IdPedidoEnc = pPickingUbic.IdPedidoEnc
                        objStockHist.IdPickingEnc = pPickingUbic.IdPickingEnc
                        objStockHist.IdDespachoEnc = IdDespachoEnc
                        objStockHist.IdNuevoStock = 0

                        If pPickingUbic.IdPresentacion <> 0 Then
                            objStockHist.Cantidad += Math.Round(pPickingUbic.Cantidad_Verificada * vFactorPres, 6)
                        Else
                            objStockHist.Cantidad += Math.Round(pPickingUbic.Cantidad_Verificada, 6)
                        End If

                        objStockHist.Peso += Math.Round(pPickingUbic.Peso_verificado, 6)
                        objStockHist.Fec_agr = Now
                        objStockHist.Fec_mod = Now
                        objStockHist.IdProductoTallaColor = pPickingUbic.IdProductoTallaColor
                        clsLnStock_hist.Insertar(objStockHist, lConnection, lTransaction)
                    End If

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw
        End Try

    End Sub


    Public Shared Sub Actualizar_Stock_Por_Traslado(ByRef pBePickingUbic As clsBeTrans_picking_ubic,
                                                    ByVal pIdUbicacionVirtualDestino As Integer,
                                                    ByVal pIdDespachoEnc As Integer,
                                                    ByVal pIdBodegaOrigen As Integer,
                                                    ByVal pIdBodegaDestino As Integer,
                                                    ByVal pIdEmpresa As Integer,
                                                    ByVal pIdOrdenCompraEncBodDest As Integer,
                                                    ByVal pIdRecepcionEncBodDest As Integer,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction)

        Try

            Dim BeStockTransito As New clsBeStock_transito

            Dim objStockOrigen As New clsBeStock()
            Dim objStockDestino As New clsBeStock()

            objStockOrigen = Get_Single_Stock_By_IdStock(pBePickingUbic.IdStock,
                                                         lConnection,
                                                         lTransaction)

            clsPublic.CopyObject(objStockOrigen,
                                 objStockDestino)

            If objStockOrigen IsNot Nothing Then

                If objStockOrigen.Presentacion.IdPresentacion <> 0 Then
                    If pBePickingUbic.IdPresentacion <> 0 Then
                        If objStockOrigen.Presentacion.EsPallet Then
                            Despacha_Pallet(objStockOrigen,
                                            pIdUbicacionVirtualDestino,
                                            lConnection,
                                            lTransaction)
                        Else
                            objStockDestino.Cantidad = Math.Round(pBePickingUbic.Cantidad_Verificada * objStockOrigen.Presentacion.Factor, 6)
                            objStockOrigen.Cantidad -= Math.Round(pBePickingUbic.Cantidad_Verificada * objStockOrigen.Presentacion.Factor, 6)
                            objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)
                        End If
                    Else
                        '#EJC20200214: Se están despachando UMBas desde un stock que tiene presentación.
                        objStockDestino.Cantidad = pBePickingUbic.Cantidad_Verificada
                        objStockOrigen.Cantidad -= pBePickingUbic.Cantidad_Verificada
                        objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)
                        objStockDestino.IdPresentacion = 0
                    End If
                Else
                    objStockDestino.Cantidad = pBePickingUbic.Cantidad_Verificada
                    objStockOrigen.Cantidad -= pBePickingUbic.Cantidad_Verificada
                    objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)
                End If

                objStockDestino.IdStock = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.
                objStockDestino.Peso = pBePickingUbic.Peso_verificado
                '#
                objStockDestino.IdBodega = pIdBodegaDestino
                objStockDestino.IdUbicacion = pIdUbicacionVirtualDestino

                '#CKFK20221209 Agregué esto porque puede pasar que la ubicacion anterior no existe en la bodega destino en
                objStockDestino.IdUbicacion_anterior = pIdUbicacionVirtualDestino

                objStockDestino.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pBePickingUbic.IdProducto,
                                                                                                                        pIdBodegaDestino, lConnection, lTransaction)

                '#EJC20190709: Asociar el stock al pedido que se despacha.
                objStockDestino.IdPedidoEnc = pBePickingUbic.IdPedidoEnc
                objStockDestino.IdPickingEnc = pBePickingUbic.IdPickingEnc
                objStockDestino.IdDespachoEnc = pIdDespachoEnc

                Insertar(objStockDestino, lConnection, lTransaction)

                objStockOrigen.Peso -= pBePickingUbic.Peso_verificado
                objStockOrigen.Peso = Math.Round(objStockOrigen.Peso, 6)

                If objStockOrigen.Cantidad = 0 Then
                    clsLnStock_parametro.Eliminar_Todos_By_IdStock(objStockOrigen.IdStock, lConnection, lTransaction)
                    Eliminar(objStockOrigen, lConnection, lTransaction)
                    Actualiza_Cantidad_Y_Peso(objStockOrigen, lConnection, lTransaction)
                Else
                    Actualiza_Cantidad_Y_Peso(objStockOrigen, lConnection, lTransaction)
                End If

                BeStockTransito.IdStockTransito = clsLnStock_transito.MaxID(lConnection, lTransaction) + 1
                BeStockTransito.IdEmpresa = pIdEmpresa
                BeStockTransito.IdBodegaOrigen = pIdBodegaOrigen
                BeStockTransito.IdBodegaDestino = pIdBodegaDestino
                BeStockTransito.IdStock = objStockDestino.IdStock
                BeStockTransito.IdProductoBodegaDestino = objStockDestino.IdProductoBodega  'clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pBePickingUbic.IdProducto,pIdBodegaDestino)
                BeStockTransito.IdProductoBodegaOrigen = pBePickingUbic.IdProductoBodega
                BeStockTransito.IdProductoEstado = objStockDestino.IdProductoEstado
                BeStockTransito.IdPresentacion = objStockDestino.IdPresentacion
                BeStockTransito.IdUnidadMedida = objStockDestino.IdUnidadMedida
                BeStockTransito.IdUbicacion = objStockDestino.IdUbicacion
                BeStockTransito.IdRecepcionEnc = objStockDestino.IdRecepcionEnc
                BeStockTransito.IdRecepcionDet = objStockDestino.IdRecepcionDet
                BeStockTransito.IdPedidoEnc = objStockDestino.IdPedidoEnc
                BeStockTransito.IdPickingEnc = objStockDestino.IdPickingEnc
                BeStockTransito.IdDespachoEnc = objStockDestino.IdDespachoEnc
                BeStockTransito.IdPickingUbic = pBePickingUbic.IdPickingUbic
                BeStockTransito.IdPedidoDet = pBePickingUbic.IdPedidoDet
                BeStockTransito.Lote = pBePickingUbic.Lote
                BeStockTransito.Lic_Plate = pBePickingUbic.Lic_plate
                BeStockTransito.Cantidad = objStockDestino.Cantidad
                BeStockTransito.Fecha_Ingreso = objStockDestino.Fecha_Ingreso
                BeStockTransito.Fecha_Vence = objStockDestino.Fecha_vence
                BeStockTransito.Fecha_Manufactura = objStockDestino.Fecha_Manufactura
                BeStockTransito.Cantidad_Recibida = 0
                BeStockTransito.Fecha_Agregado = Now
                BeStockTransito.IdOrdenCompraEnc_BodDest = pIdOrdenCompraEncBodDest
                BeStockTransito.IdRecepcionEnc_BodDest = pIdRecepcionEncBodDest
                clsLnStock_transito.Insertar(BeStockTransito, lConnection, lTransaction)

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Actualizar_Stock_Por_Traslado_Con_Recepcion_En_Destino(ByRef pBePickingUbic As clsBeTrans_picking_ubic,
                                                                             ByVal pIdUbicacionVirtualDestino As Integer,
                                                                             ByVal BeDespachoEnc As clsBeTrans_despacho_enc,
                                                                             ByVal pIdBodegaOrigen As Integer,
                                                                             ByVal pIdBodegaDestino As Integer,
                                                                             ByVal pIdEmpresa As Integer,
                                                                             ByVal BeOrdenCompraEncBodDest As clsBeTrans_oc_enc,
                                                                             ByVal pIdRecepcionEncBodDest As Integer,
                                                                             ByVal BeRecepcionDet As clsBeTrans_re_det,
                                                                             ByRef lConnection As SqlConnection,
                                                                             ByRef lTransaction As SqlTransaction)

        Try

            Dim BeStockTransito As New clsBeStock_transito
            Dim objStockOrigen As New clsBeStock()
            Dim objStockDestino As New clsBeStock()
            Dim BeStockRec As New clsBeStock_rec()

            objStockOrigen = Get_Single_Stock_By_IdStock(pBePickingUbic.IdStock,
                                                         lConnection,
                                                         lTransaction)

            clsPublic.CopyObject(objStockOrigen,
                                 objStockDestino)

            If objStockOrigen IsNot Nothing Then

                If objStockOrigen.Presentacion.IdPresentacion <> 0 Then
                    If pBePickingUbic.IdPresentacion <> 0 Then
                        If objStockOrigen.Presentacion.EsPallet Then
                            Despacha_Pallet(objStockOrigen,
                                            pIdUbicacionVirtualDestino,
                                            lConnection,
                                            lTransaction)
                        Else
                            objStockDestino.Cantidad = Math.Round(pBePickingUbic.Cantidad_Verificada * objStockOrigen.Presentacion.Factor, 6)
                            objStockOrigen.Cantidad -= Math.Round(pBePickingUbic.Cantidad_Verificada * objStockOrigen.Presentacion.Factor, 6)
                            objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)
                        End If
                    Else
                        '#EJC20200214: Se están despachando UMBas desde un stock que tiene presentación.
                        objStockDestino.Cantidad = pBePickingUbic.Cantidad_Verificada
                        objStockOrigen.Cantidad -= pBePickingUbic.Cantidad_Verificada
                        objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)
                        objStockDestino.IdPresentacion = 0
                    End If
                Else
                    objStockDestino.Cantidad = pBePickingUbic.Cantidad_Verificada - pBePickingUbic.Cantidad_despachada
                    objStockOrigen.Cantidad -= pBePickingUbic.Cantidad_Verificada - pBePickingUbic.Cantidad_despachada
                    objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)
                End If

                objStockDestino.IdStock = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.
                objStockDestino.Peso = pBePickingUbic.Peso_verificado
                '#
                objStockDestino.IdBodega = pIdBodegaDestino
                objStockDestino.IdUbicacion = pIdUbicacionVirtualDestino
                objStockDestino.IdUbicacion_anterior = pIdUbicacionVirtualDestino

                objStockDestino.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pBePickingUbic.IdProducto,
                                                                                                                        pIdBodegaDestino,
                                                                                                                        lConnection,
                                                                                                                        lTransaction)

                '#EJC20190709: Asociar el stock al pedido que se despacha.
                objStockDestino.IdPedidoEnc = pBePickingUbic.IdPedidoEnc
                objStockDestino.IdPickingEnc = pBePickingUbic.IdPickingEnc
                objStockDestino.IdRecepcionEnc = pIdRecepcionEncBodDest
                objStockDestino.IdRecepcionDet = BeRecepcionDet.IdRecepcionDet
                objStockDestino.IdDespachoEnc = BeDespachoEnc.IdDespachoEnc
                objStockDestino.IdPropietarioBodega = BeOrdenCompraEncBodDest.IdPropietarioBodega
                objStockDestino.Fecha_Ingreso = Now

                Insertar(objStockDestino,
                         lConnection,
                         lTransaction)

                objStockOrigen.Peso -= pBePickingUbic.Peso_verificado
                objStockOrigen.Peso = Math.Round(objStockOrigen.Peso, 6)

                If objStockOrigen.Cantidad = 0 Then
                    clsLnStock_parametro.Eliminar_Todos_By_IdStock(objStockOrigen.IdStock,
                                                                   lConnection,
                                                                   lTransaction)
                    Eliminar(objStockOrigen,
                             lConnection,
                             lTransaction)
                Else
                    Actualiza_Cantidad_Y_Peso(objStockOrigen,
                                              lConnection,
                                              lTransaction)
                End If


                clsPublic.CopyObject(objStockDestino,
                                     BeStockRec)


                BeStockRec.IdStockRec = clsLnStock_rec.MaxID(lConnection, lTransaction) + 1
                BeStockRec.IdRecepcionDet = BeRecepcionDet.IdRecepcionDet
                BeStockRec.No_linea = BeRecepcionDet.No_Linea
                BeStockRec.User_agr = BeDespachoEnc.User_agr
                BeStockRec.Fec_agr = Now
                BeStockRec.User_mod = BeDespachoEnc.User_agr
                BeStockRec.Fec_mod = Now

                clsLnStock_rec.Insertar(BeStockRec,
                                        lConnection,
                                        lTransaction)

                clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                      pIdBodegaDestino,
                                                                      BeDespachoEnc.User_agr,
                                                                      BeStockRec,
                                                                      lConnection,
                                                                      lTransaction)

                clsLnI_nav_transacciones_out.Insertar_Ingreso_Parcial(pIdEmpresa,
                                                                      pIdBodegaDestino,
                                                                      BeOrdenCompraEncBodDest.IdTipoIngresoOC,
                                                                      BeRecepcionDet,
                                                                      BeOrdenCompraEncBodDest.IdOrdenCompraEnc,
                                                                      BeDespachoEnc.User_agr,
                                                                      True,
                                                                      lConnection,
                                                                      lTransaction)

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Insertar_Stock_Recepcion(ByVal pListBeTransReDet As List(Of clsBeTrans_re_det),
                                                    ByRef pIdUsuario As Integer,
                                                    ByVal pIdEmpresa As Integer,
                                                    ByVal pIdBodega As Integer,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As Boolean

        Insertar_Stock_Recepcion = False

        Try

            If pListBeTransReDet IsNot Nothing Then

                Dim lMaxS As Integer = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.
                Dim BeStock As New clsBeStock
                Dim BeStockRec As New clsBeStock_rec
                Dim BeVWStockRec As New clsBeVW_stock_res
                Dim vExisteEnStock As Boolean = False
                Dim vExisteEnReDet As Boolean = False

                For Each BeTransReDet As clsBeTrans_re_det In pListBeTransReDet

                    BeStock = New clsBeStock

                    vExisteEnReDet = clsLnTrans_re_det.Existe_By_IdRecepcionEnc_And_IdRecepcionDet(BeTransReDet, lConnection, lTransaction)

                    If Not vExisteEnReDet Then

                        clsPublic.CopyObject(BeTransReDet, BeStock)

                        lMaxS += 1

                        BeStock.IdBodega = pIdBodega
                        BeStock.IdStock = lMaxS
                        BeStock.IdPropietarioBodega = BeTransReDet.IdPropietarioBodega
                        BeStock.IdProductoBodega = BeTransReDet.IdProductoBodega
                        BeStock.ProductoEstado = New clsBeProducto_estado()
                        BeStock.Presentacion = New clsBeProducto_Presentacion()
                        BeStock.ProductoEstado.IdEstado = BeTransReDet.ProductoEstado.IdEstado
                        BeStock.Presentacion.IdPresentacion = BeTransReDet.Presentacion.IdPresentacion
                        BeStock.IdUnidadMedida = BeTransReDet.UnidadMedida.IdUnidadMedida
                        BeStock.IdUbicacion = BeTransReDet.IdUbicacion
                        BeStock.IdUbicacion_anterior = BeTransReDet.IdUbicacion
                        BeStock.IdRecepcionEnc = BeTransReDet.IdRecepcionEnc
                        BeStock.IdRecepcionDet = BeTransReDet.IdRecepcionDet

                        If BeTransReDet.Presentacion.IdPresentacion <> 0 Then

                            Dim BePres As New clsBeProducto_Presentacion With {.IdPresentacion = BeTransReDet.Presentacion.IdPresentacion}
                            clsLnProducto_presentacion.GetSingle(BePres, lConnection, lTransaction)

                            If Not BePres Is Nothing Then

                                If BePres.EsPallet Then
                                    BeStock.Cantidad = Math.Round(BeTransReDet.cantidad_recibida * BePres.Factor * BePres.CajasPorCama * BePres.CamasPorTarima, 6)
                                Else
                                    BeStock.Cantidad = Math.Round(BeTransReDet.cantidad_recibida * BePres.Factor, 6)
                                End If

                            Else
                                Throw New Exception("20200329_0939: No se pudo obtener la presentación para: " & BePres.IdPresentacion)
                            End If

                        Else
                            BeStock.Cantidad = BeTransReDet.cantidad_recibida
                        End If

                        BeStock.Fecha_Ingreso = BeTransReDet.Fecha_ingreso
                        BeStock.Fecha_vence = BeTransReDet.Fecha_vence
                        BeStock.Fecha_Manufactura = Nothing
                        BeStock.User_agr = pIdUsuario
                        BeStock.Fec_agr = Now
                        BeStock.User_mod = pIdUsuario
                        BeStock.Fec_mod = Now
                        BeStock.Activo = True

                        '#EJC20230222349: Agregué validación para evitar que se vuelva a insertar un registro duplicado en stock.
                        BeVWStockRec = Get_Single_By_BeRecepcionDet(BeTransReDet,
                                                                    pIdBodega,
                                                                    lConnection,
                                                                    lTransaction)

                        If Not BeVWStockRec Is Nothing Then
                            Throw New Exception("ERROR_202302222350: EL sistema detectó una condición no válida para la finalización de la tarea, el stock podría duplicarse, para prevenir esto no se finalizará la recepción. Reporte este error a desarrollo, lamentamos el inconveniente, Gracias.")
                        End If

                        '#EJC20191218:IdBodega2Stock
                        Insertar(BeStock, lConnection, lTransaction)

                        clsPublic.CopyObject(BeStock, BeStockRec)

                        '#EJC20180420:Se debe crear movimiento para recepción ciega.
                        'Se registra el movimiento actualmente como recepción normal  (RECE)
                        clsLnTrans_movimientos.Insertar_Movimientos_Recepcion(pIdEmpresa,
                                                                              pIdBodega,
                                                                              pIdUsuario,
                                                                              BeStockRec,
                                                                              lConnection,
                                                                              lTransaction)

                    End If

                Next

                Insertar_Stock_Recepcion = True

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Count_IdStock_By_IdBodega(ByRef pIdBodega As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim retVal As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '       Dim vSQL As String = "select count(idstock) from stock where idubicacion in 
            '(SELECT stock.IdUbicacion
            'FROM bodega_tramo INNER JOIN
            'bodega_ubicacion ON bodega_tramo.IdTramo = bodega_ubicacion.IdTramo and bodega_tramo.IdBodega = bodega_ubicacion.IdUbicacion INNER JOIN
            '               bodega_sector ON bodega_tramo.IdSector = bodega_sector.IdSector and bodega_tramo.IdBodega = bodega_sector.IdBodega INNER JOIN
            '               bodega_area ON bodega_sector.IdArea = bodega_area.IdArea and bodega_sector.IdBodega = bodega_area.IdBodega INNER JOIN
            'stock ON bodega_ubicacion.IdUbicacion = stock.IdUbicacion
            'WHERE (bodega_area.IdBodega = @IdBodega))"

            Dim vSQL As String = "select count(idstock) from stock where IdBodega = @IdBodega"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                retVal = 0

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    retVal = lDataTable.Rows(0).Item(0)
                End If

            End Using

            lTransaction.Commit()

            Return retVal

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Sub Importar_Inventario(ByVal BeTransInvEnc As clsBeTrans_inv_enc,
                                          ByVal ListBeStock As IList(Of clsBeStock),
                                          ByVal ListBeMovimientos As IList(Of clsBeTrans_movimientos))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim IdStock, IdMovimiento As Integer

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            IdStock = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.
            IdMovimiento = clsLnTrans_movimientos.MaxID(lConnection, lTransaction)

            BeTransInvEnc.Regularizado = True
            BeTransInvEnc.Estado = "Finalizado"
            BeTransInvEnc.Activo = False
            clsLnTrans_inv_enc.Actualizar(BeTransInvEnc, lConnection, lTransaction)

            For Each BeStock As clsBeStock In ListBeStock
                IdStock += 1
                If BeTransInvEnc.IdTipoInventario = 1 AndAlso BeStock.Cantidad > 0 Then
                    '#EJC20191218:IdBodega2Stock
                    BeStock.IdBodega = BeTransInvEnc.IdBodega
                    BeStock.IdStock = IdStock
                    Debug.Write("IdStock:" & IdStock)
                    Insertar(BeStock, lConnection, lTransaction)
                ElseIf BeTransInvEnc.IdTipoInventario = 2 AndAlso BeStock.Cantidad > 0 Then
                    '#EJC20191218:IdBodega2Stock
                    BeStock.IdBodega = BeTransInvEnc.IdBodega
                    BeStock.IdStock = IdStock
                    Debug.Write("IdStock:" & IdStock)
                    Insertar(BeStock, lConnection, lTransaction)

                End If
            Next

            For Each BeMov As clsBeTrans_movimientos In ListBeMovimientos
                IdMovimiento += 1 : BeMov.IdMovimiento = IdMovimiento
                clsLnTrans_movimientos.Insertar(BeMov, lConnection, lTransaction)
            Next

            '#CKFK 20180602 Agregué la funcionalidad de que actualice la tarea de inventario a finalizada cuando se regularice
            'Dim IdTareaHH As Integer = clsLnTarea_hh.GetIdTarea(BeTransInvEnc.Idinventarioenc, 6, lConnection, lTransaction)

            clsLnTarea_hh.Actualiza_Estado_Tarea(BeTransInvEnc.Idinventarioenc, 6, 4, lConnection, lTransaction) 'El IdEstado 4 es Finalizado

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    ''' <summary>
    ''' #EJC202405021906: Modificada para no incluir lo pickeado.
    ''' </summary>
    ''' <param name="pIdBodega"></param>
    ''' <param name="pIdPropietarioBodega"></param>
    ''' <returns></returns>
    Public Shared Function Get_Reporte_Stock(ByVal pIdBodega As Integer,
                                             ByVal pIdPropietarioBodega As Integer,
                                             ByVal ExcluirSinExistencia As Boolean) As DataTable

        Get_Reporte_Stock = Nothing

        Try

            Dim vSQL As String = "select v.IdProducto, v.codigo, v.nombre AS Producto, v.NomEstado AS Estado, v.IdPresentacion, 
                                 SUM(ISNULL(v.CantidadSF, 0)) AS CantidadUMBas, v.UnidadMedida, SUM(ISNULL(v.Cantidad_Presentacion, 0)) AS CantidadPresentacion, v.Presentacion, 
                                  SUM(ISNULL(v.CantidadReservada, 0)) AS Cantidad_Reservada_UMBas, SUM(ISNULL(v.Cantidad_Reservada_Pres, 0)) AS Cantidad_Reservada_Pres, 
                                  SUM(ISNULL(v.Disponible_UMBas, 0)) AS Disponible_UMBas, SUM(ISNULL(v.Disponible_Presentacion, 0)) AS Disponible_Presentación, v.peso AS Peso, v.lote as Lote, 
                                  v.lic_plate AS Licencia, v.fecha_ingreso as Fecha_Ingreso, v.fecha_vence as Fecha_Vence, v.Nombre_Completo AS Ubicación, v.Codigo_Poliza, 
                                  v.Numero_poliza AS numero_orden, v.ubicacion_picking, v.Area, v.factor, v.IdUbicacion, dbo.Nombre_Tramo(v.IdTramo, 
                                  v.IdBodega) AS Tramo,
                                       CASE WHEN v.IdPresentacion IS NULL THEN ISNULL(t.Cant_Pickeada,0)ELSE 0 END Cant_Pickeada_UMBas,
	                                   CASE WHEN v.IdPresentacion IS NULL THEN v.CantidadReservadaUmBas - ISNULL(t.Cant_Pickeada,0)ELSE 0 END Cant_No_Pickeada_UMBas,
	                                   CASE WHEN v.IdPresentacion IS NOT NULL THEN ISNULL(t.Cant_Pickeada,0)ELSE 0 END Cant_Pickeada_Presentacion,
	                                   CASE WHEN v.IdPresentacion IS NOT NULL THEN v.Cantidad_Reservada_Pres - ISNULL(t.Cant_Pickeada,0)ELSE 0 END Cant_No_Pickeada_Presentacion,
                                v.Codigo_Talla as Talla, v.Codigo_Color as Color 
                                FROM VW_Stock_Res v left outer join 
                                     (SELECT r.IdStock, sum(u.cantidad_recibida) Cant_Pickeada
	                                  FROM trans_picking_ubic u inner join
			                                trans_pe_enc e ON e.IdPedidoEnc = u.IdPedidoEnc right outer join 
		                                stock_res r ON u.IdStock = r.IdStock
		                                and r.IdPedido = u.IdPedidoEnc and r.IdPedidoDet = u.IdPedidoDet 
	                                  WHERE r.Indicador = 'PED' and e.estado NOT IN ('Despachado','Anulado')
	                                  GROUP BY r.IdStock) t ON v.IdStock = t.IdStock WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND v.IdBodega = @IdBodega"
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND v.IdPropietarioBodega = @IdPropietarioBodega"
            End If

            'vSQL += " AND (Estado_Reserva NOT IN ('PICKEADO','VERIFICADO') OR ESTADO_RESERVA IS NULL) "

            vSQL += " Group by v.IdProducto,v.NomEstado,v.Fecha_Ingreso,Codigo,
					  v.Nombre,Presentacion,v.IdPresentacion,v.UnidadMedida,v.peso, 
					  v.Lote,v.fecha_vence, v.Nombre_Completo,v.lic_plate,
				      v.Factor,v.codigo_poliza,v.Numero_poliza, v.CantidadReservada,Cantidad,
                      v.CantidadSF,v.ubicacion_picking,v.Area, v.Factor, 
                      v.IdUbicacion, v.IdTramo, v.IdBodega, 
                      v.Codigo_Talla, v.Codigo_Color, 
                      t.Cant_Pickeada,v.CantidadReservadaUmBas,v.Cantidad_Reservada_Pres  "

            If ExcluirSinExistencia Then
                vSQL += "HAVING SUM(ISNULL(v.Disponible_UMBas, 0)) > 0 "
            End If

            vSQL += "ORDER BY CODIGO, Nombre_Completo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        If pIdPropietarioBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Get_Reporte_Stock_QA(ByVal pIdBodega As Integer,
                                                ByVal pIdPropietarioBodega As Integer) As DataTable

        Get_Reporte_Stock_QA = Nothing

        Try

            Dim vSQL As String = "SELECT Codigo,
                                  Fecha_Vence,
                                  SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							      SUM(isnull(Cantidad_Reservada_Pres,0)) AS Cantidad_Reservada_Pres,					      
                                  SUM(isnull(Disponible_UMBas,0))  as Disponible_UMBas, 
							      SUM(isnull(Disponible_Presentacion,0)) AS Disponible_Presentación,
                                  sum(isnull(CantidadSF,0)) as CantidadUMBas,
							      SUM(isnull(Cantidad_Presentacion,0)) as CantidadPresentacion,							      
                                  Nombre_Completo AS [Ubicación],	
							      UnidadMedida,
                                  Presentacion,	
							      nombre as Producto,NomEstado as Estado,
							      IdPresentacion,
							      Peso,Lote,lic_plate as Licencia,Fecha_Ingreso,
                                  codigo_poliza,Numero_poliza numero_orden,ubicacion_picking, Area, Factor, IdUbicacion, 
                                  dbo.Nombre_Tramo(IdTramo, IdBodega) Tramo, IdProducto
							      FROM VW_Stock_Res WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega"
            End If

            'vSQL += " AND (Estado_Reserva NOT IN ('PICKEADO','VERIFICADO') OR ESTADO_RESERVA IS NULL) "

            vSQL += " Group by IdProducto,NomEstado,Fecha_Ingreso,Codigo,
					  Nombre,Presentacion,IdPresentacion,UnidadMedida,peso, 
					  Lote,fecha_vence, Nombre_Completo,lic_plate,
				      Factor,codigo_poliza,Numero_poliza, CantidadReservada,Cantidad,
                      CantidadSF,ubicacion_picking,Area, Factor, IdUbicacion, IdTramo, IdBodega  "

            vSQL += "ORDER BY CODIGO, Nombre_Completo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        If pIdPropietarioBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Get_Inventario_Stock_ByIdRec(ByVal pIdBodega As Integer, ByVal pIdPropietarioBodega As Integer) As DataTable

        Get_Inventario_Stock_ByIdRec = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdRecepcionEnc,IdProducto,
                                            Codigo,nombre as Producto,NomEstado as Estado,
                                            IdPresentacion,sum(isnull(CantidadSF,0)) as CantidadUMBas,
                                            UnidadMedida,SUM(isnull(Cantidad,0)) as CantidadPresentacion,Presentacion,
						                    SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada, 
                                            ROUND(SUM(isnull(CantidadSF,0)) - SUM(isnull(CantidadReservada,0)),6) as Disponible, 
						                    Peso,Lote,lic_plate,Fecha_Ingreso,Fecha_Vence, Nombre_Completo AS [Ubicación],
                                            codigo_poliza,Numero_poliza numero_orden
						                    FROM VW_Stock_Res WHERE 1 > 0"

                    If pIdBodega <> 0 Then
                        vSQL += " AND IdBodega = @IdBodega"
                    End If

                    If pIdPropietarioBodega <> 0 Then
                        vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega"
                    End If

                    vSQL += " GROUP BY IdRecepcionEnc,NomEstado,IdProducto,Codigo,nombre,
                              Presentacion,IdPresentacion,UnidadMedida,peso, lote,
                              fecha_vence, Nombre_Completo,lic_plate, Fecha_Ingreso,codigo_poliza,Numero_poliza "

                    vSQL += "ORDER BY CODIGO, Nombre_Completo "

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        If pIdPropietarioBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Inventario_Stock_ByIdRec = lDataTable
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

    Public Shared Function Get_Inventario_Stock_LP(ByVal pIdBodega As Integer, ByVal pIdPropietarioBodega As Integer) As DataTable

        Get_Inventario_Stock_LP = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    '              Dim vSQL As String = "SELECT IdProducto,Propietario,Codigo,nombre as Producto,Presentacion,IdPresentacion,sum(isnull(Cantidad,0)) as CantidadPresentacion,sum(isnull(CantidadSF,0)) as CantidadUMBas,
                    'SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada, ROUND(SUM(isnull(CantidadSF,0)) - SUM(isnull(CantidadReservada,0)),6)  as Disponible, 
                    'UnidadMedida,Peso,Lote,lic_plate as Lic_Plate,Fecha_Ingreso,Fecha_Vence, Nombre_Completo AS [Ubicación],
                    '                  numero_orden,codigo_poliza,clasificacion
                    'FROM VW_Stock_Res WHERE 1 > 0"

                    '#GT07032022_1115: se agrega el embarcador, la clasificación y el TO de la poliza
                    '#GT26092022_1100: se agregan los parametros para DyD
                    '#MECR27082025: Se agrego columna de talla y color
                    Dim vSQL As String = "SELECT IdProducto,Propietario,Codigo,nombre as Producto,Presentacion,IdPresentacion,
                                                 sum(isnull(Cantidad,0)) as CantidadPresentacion,sum(isnull(CantidadSF,0)) as CantidadUMBas,
                                                 SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada, 
                                                 ROUND(SUM(isnull(CantidadSF,0)) - SUM(isnull(CantidadReservada,0)),6)  as Disponible, 
                                                 UnidadMedida,Peso,Lote,lic_plate as Licencia,Fecha_Ingreso,Fecha_Vence, 
                                                 Nombre_Completo AS [Ubicación],
                                                 Numero_poliza  numero_orden,codigo_poliza,NoTO,clasificacion,Embarcador,Area,
                                                 parametro_a,parametro_b,marca,tipo,familia, Codigo_Talla as Talla, Codigo_Color as Color
                                          FROM VW_Stock_Res WHERE 1 > 0 "

                    If pIdBodega <> 0 Then
                        vSQL += " AND IdBodega = @IdBodega"
                    End If

                    If pIdPropietarioBodega <> 0 Then
                        vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega"
                    End If

                    vSQL += " Group by IdProducto,Codigo,nombre,lic_plate,Presentacion,IdPresentacion,UnidadMedida,peso, 
                              lote,fecha_vence, Nombre_Completo,Fecha_Ingreso,Numero_poliza,codigo_poliza,
                              Propietario,clasificacion,Embarcador,NoTO,Area, 
                              parametro_a,parametro_b,marca,tipo,familia, Codigo_Talla, Codigo_Color "

                    vSQL += "ORDER BY CODIGO, Nombre_Completo "

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        If pIdPropietarioBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Get_Inventario_Stock(ByVal pIdBodega As Integer) As DataTable

        Get_Inventario_Stock = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT IdProducto,
							Codigo,
							nombre as Producto,
							Presentacion,
							IdPresentacion,
							SUM(isnull(CantidadSF,0)) as CantidadUMBas,
							SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada, 
							ROUND(SUM(isnull(CantidadSF,0)) - SUM(isnull(CantidadReservada,0)),6)  as Disponible, 
							UnidadMedida,
							Peso,
							Lote,
							Fecha_Vence, 
							Nombre_Completo,
							IdUbicacion
							FROM VW_Stock_Res WHERE 1 > 0"

                    If pIdBodega <> 0 Then
                        vSQL += " AND IdBodega = @IdBodega"
                    End If

                    vSQL += " Group by IdProducto,Codigo,nombre,Presentacion,
							IdPresentacion,UnidadMedida,peso, lote,fecha_vence, Nombre_Completo,IdUbicacion "

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Get_Inventario_Stock_Por_Tipo() As DataTable

        Get_Inventario_Stock_Por_Tipo = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    'GT26092022_1530: se agregan campos para uso DyD (fam,marca, clasificacion, parametros)
                    '#MECR27092025: Se agrego columna de talla y color
                    Dim vSQL As String = "SELECT Codigo,nombre as Producto,Presentacion,
                                                IdPresentacion, 
                                                sum(isnull(CantidadSF,0)) as CantidadUMBas,
						                        sum(isnull(CantidadReservada,0)) as Cantidad_Reservada, 
                                                round(sum(isnull(CantidadSF,0)) - sum(isnull(CantidadReservada,0)),3) as Disponible,
						                        UnidadMedida,Peso,Lote,fecha_ingreso,
                                                Fecha_Vence, NombreTipoProducto As Tipo, 
                                                IdProducto,
                                                Familia,Marca,Clasificacion,
                                                parametro_a,parametro_b,
                                                Codigo_Talla as Talla,
                                                Codigo_Color as Color
						                  from VW_Stock_Res_Tipo_Producto 
						                  Group by IdProducto,Codigo,nombre,
                                                   Presentacion,IdPresentacion,
                                                   UnidadMedida,peso, lote,fecha_vence, 
                                                   NombreTipoProducto,fecha_ingreso,
                                                   Familia,Marca,Clasificacion,
                                                   parametro_a,parametro_b, Codigo_Talla, Codigo_Color"

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Get_Detalle_Lote_Por_Ubi(ByVal IdBodega As Integer, ByVal IdPropietarioBodega As Integer, ByVal FechaVence As Boolean) As DataTable

        Get_Detalle_Lote_Por_Ubi = Nothing

        Dim vSQL As String = ""

        Try

            If FechaVence Then

                '#MECR27092025: Se agrego columna de talla y color
                vSQL = "SELECT vw.IdProducto,vw.Codigo,nombre as Producto,vw.IdPresentacion, sum(isnull(vw.CantidadSF,0)) as CantidadUMBas,vw.UnidadMedida,sum(isnull(vw.Cantidad,0)) as CantidadPresentacion,vw.Presentacion,
                        sum(isnull(vw.CantidadReservada,0)) as Cantidad_Reservada, sum(isnull(vw.CantidadSF,0)) - sum(isnull(vw.CantidadReservada,0)) as Disponible, 
                        vw.Peso,vw.Lote,vw.lic_plate as Licencia,vw.Fecha_Ingreso,vw.Fecha_Vence,
                        vw.Nombre_Completo, 
                        bodega_ubicacion.indice_x as Columna, bodega_ubicacion.Nivel, 
                        CASE WHEN bT.es_rack = 1 THEN 
                        iif(CHARINDEX('-',bT.descripcion,0)=0, bT.descripcion, SUBSTRING(REPLACE(bT.descripcion,'-',''),1, LEN(bT.descripcion)-2)) 
                        ELSE BT.descripcion
                        END AS Rack,
                        iif(CHARINDEX('-',bT.descripcion,0)=0,'','T' + SUBSTRING(bT.descripcion,iif(CHARINDEX('-',bT.descripcion,0)<0,0,CHARINDEX('-',bT.descripcion,0)+1),1)) Tramo,
                        bt.Es_Rack, bt.Descripcion,vw.IdBodega,vw.IdPropietarioBodega,codigo_poliza,Numero_poliza numero_orden,
                        CASE WHEN FACTOR > 0
							THEN 
                                ROUND(isnull(Cantidad,0) - isnull(CantidadReservada/Factor,0),6)
							ELSE
								0
							END AS Disponible_Presentacion,
                        vw.Codigo_Talla as Talla,
                        vw.Codigo_Color as Color
                        FROM VW_Stock_Res vw INNER JOIN
                        bodega_tramo bt ON vw.idtramo = bt.IdTramo and vw.idbodega=bt.idbodega INNER JOIN
                        bodega_ubicacion ON vw.IdUbicacion = bodega_ubicacion.IdUbicacion and vw.idbodega=bodega_ubicacion.IdBodega
                        where vw.IdBodega=@IdBodega and vw.IdPropietarioBodega=@IdPropietarioBodega
                        Group by vw.lic_plate,vw.IdProducto,vw.Codigo,vw.nombre,vw.Presentacion,vw.IdPresentacion,vw.UnidadMedida,vw.peso, vw.lote,vw.fecha_vence,
                        bt.es_rack, bt.descripcion, bodega_ubicacion.indice_x, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos, 
                        bodega_ubicacion.idubicacion,vw.IdBodega,vw.IdPropietarioBodega,vw.Fecha_Ingreso,vw.codigo_poliza,vw.Numero_poliza,vw.Nombre_Completo,
                        vw.Factor,vw.Cantidad,vw.CantidadReservada, Codigo_Talla, Codigo_Color "

            Else

                vSQL = "SELECT vw.IdProducto,vw.Codigo,nombre as Producto,vw.IdPresentacion, sum(isnull(vw.CantidadSF,0)) as CantidadUMBas,vw.UnidadMedida,sum(isnull(vw.Cantidad,0)) as CantidadPresentacion,vw.Presentacion,
						sum(isnull(vw.CantidadReservada,0)) as Cantidad_Reservada, sum(isnull(vw.CantidadSF,0)) - sum(isnull(vw.CantidadReservada,0)) as Disponible, 
						vw.Peso,vw.Lote, vw.Nombre_Completo
						bodega_ubicacion.indice_x as Columna, bodega_ubicacion.Nivel, 
						CASE WHEN bT.es_rack = 1 THEN 
                        iif(CHARINDEX('-',bT.descripcion,0)=0, bT.descripcion, SUBSTRING(REPLACE(bT.descripcion,'-',''),1, LEN(bT.descripcion)-2)) 
                        ELSE BT.descripcion
                        END AS Rack,
                        iif(CHARINDEX('-',bT.descripcion,0)=0,'','T' + SUBSTRING(bT.descripcion,iif(CHARINDEX('-',bT.descripcion,0)<0,0,CHARINDEX('-',bT.descripcion,0)+1),1)) Tramo,
						bt.Es_Rack, bt.Descripcion,vw.IdBodega,vw.IdPropietarioBodega,codigo_poliza,Numero_poliza numero_orden,
                        CASE WHEN FACTOR > 0
							THEN 
                                ROUND(isnull(Cantidad,0) - isnull(CantidadReservada/Factor,0),6)
							ELSE
								0
							END AS Disponible_Presentacion
						FROM VW_Stock_Res vw INNER JOIN
						bodega_tramo bt ON vw.idtramo = bt.IdTramo INNER JOIN
						bodega_ubicacion ON vw.IdUbicacion = bodega_ubicacion.IdUbicacion
						where vw.IdBodega=@IdBodega and vw.IdPropietarioBodega=@IdPropietarioBodega 
						Group by vw.IdProducto,vw.Codigo,vw.nombre,vw.Presentacion,vw.IdPresentacion,vw.UnidadMedida,vw.peso, vw.lote,
						bt.es_rack, bt.descripcion, bodega_ubicacion.indice_x, bodega_ubicacion.nivel, bodega_ubicacion.orientacion_pos, 
                        bodega_ubicacion.idubicacion,vw.IdBodega,vw.IdPropietarioBodega,vw.codigo_poliza,vw.Numero_poliza,vw.Nombre_Completo,
                        vw.Factor,vw.Cantidad,vw.CantidadReservada "

            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_Detalle_Lote_Por_Ubi = lDataTable

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

    '#CKFK 20181114_0134PM Creé esta función para Eliminar un registro de Stock asociado a una recepción
    Public Shared Function Elimina_Stock_Anterior(ByVal pBeStockAnt As clsBeStock,
                                                  ByRef CadenaResultado As String,
                                                  ByRef lConnection As SqlConnection,
                                                  ByRef lTransaction As SqlTransaction) As Boolean

        Dim lMovimientos As New List(Of clsBeTrans_movimientos)
        Dim FilasAfectadasStockParametro As Integer = 0
        Dim FilasAfectadasSe As Integer = 0
        Dim FilasAfectadasMovimientos As Integer = 0
        Dim FilasAfectadasStock As Integer = 0

        Elimina_Stock_Anterior = False

        Try

            If Existe_Stock(pBeStockAnt, lConnection, lTransaction) Then

                '#GT02072025: se obtiene el movimiento basado en IdrecepcionEnc y Det
                lMovimientos = clsLnTrans_movimientos.Get_Movimiento_By_Stock(pBeStockAnt, lConnection, lTransaction)

                If lMovimientos.Count > 0 Then

                    FilasAfectadasStockParametro = clsLnStock_parametro.Eliminar_Todos_By_IdStock(pBeStockAnt.IdStock, lConnection, lTransaction)
                    If FilasAfectadasStockParametro > 0 Then
                        CadenaResultado += String.Format("Eliminó en parametros {0} ", FilasAfectadasStockParametro.ToString)
                    End If

                    FilasAfectadasSe = clsLnStock_se.Eliminar_By_IdStock(pBeStockAnt.IdStock, lConnection, lTransaction)
                    If FilasAfectadasSe > 0 Then
                        CadenaResultado += String.Format("Eliminó en stock_se  {0} ", FilasAfectadasSe.ToString)
                    End If

                    FilasAfectadasMovimientos = clsLnTrans_movimientos.Eliminar(lMovimientos.Item(0), lConnection, lTransaction)
                    If FilasAfectadasMovimientos > 0 Then
                        CadenaResultado += String.Format("Eliminó en movimientos  {0} ", FilasAfectadasMovimientos.ToString)
                    Else
                        Throw New Exception("ERROR_202209161027: No se pudo eliminar el movimiento asociado.")
                    End If

                    FilasAfectadasStock = Eliminar_By_IdStock(pBeStockAnt.IdStock, lConnection, lTransaction)
                    If FilasAfectadasStock > 0 Then
                        CadenaResultado += String.Format("Eliminó en stock  {0}, el IdStock es {1} ", FilasAfectadasStock.ToString, pBeStockAnt.IdStock)
                    Else
                        Throw New Exception("ERROR_202209161028: No se pudo eliminar el stock asociado.")
                    End If

                    Elimina_Stock_Anterior = True

                End If

            Else
                Throw New Exception("El Stock fue modificado no se puede realizar la modificación")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllStockDTByIdBodega_AndIdProducto(ByVal IdBodega As Integer,
                                                   ByVal IdProducto As Integer,
                                         ByVal lConnection As SqlConnection,
                                         ByVal lTransaction As SqlTransaction) As DataTable

        Try
            '#CKFK20221017 Modifiqué la vista VW_Stock_Resumen por la VW_Stock_Res
            Dim vSQL As String = "SELECT * FROM VW_Stock_Res WHERE IdBodega=@IdBodega and IdProducto=@IdProducto and disponible_umbas > 0"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", IdProducto)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                Dim lDataTable As New DataTable("Stock")
                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Stock_Especifico_By_IdPedidoDet_Original(ByVal pIdPedidoDet As Integer,
                                                                   ByVal IdPedidoEnc As Integer,
                                                                   ByVal gIdBodega As Integer,
                                                                   ByVal CantReemplazar As Double,
                                                                   ByRef vCant As Double) As DataTable

        Get_All_Stock_Especifico_By_IdPedidoDet_Original = Nothing


        Dim BeListStockRes As New List(Of clsBeStock_res)
        Dim DT As New DataTable
        Dim DT_Completo As New DataTable("Stock_Especifico")

        Try

            Dim watch As Stopwatch = Stopwatch.StartNew()

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            lConnection.Open()

            Dim lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            BeListStockRes = clsLnStock_res.Get_All_Stock_Res_By_IdPedidoDet(pIdPedidoDet, lConnection, lTransaction)

            For Each StockRes In BeListStockRes

                DT = Get_All_Stock_Especifico_HH(gIdBodega, IdPedidoEnc, StockRes, lConnection, lTransaction)

                If DT.Rows.Count > 0 Then

                    DT_Completo.Merge(DT)

                    vCant = DT_Completo.AsEnumerable().Sum(Function(Z) Z.Field(Of Double)("Disponible_UMBas"))

                    If vCant >= CantReemplazar And vCant > 0 Then
                        Return DT_Completo
                        Exit For
                    End If

                End If

            Next

            If vCant < CantReemplazar And vCant > 0 Then

                Return DT_Completo

            End If

            Return DT_Completo

            lTransaction.Commit()

            lConnection.Close()

            watch.Stop()

            Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ""))
        End Try

    End Function

    ' #AT 20211228 Función creada para Stock Especifico de reemplazo de producto en picking
    Public Shared Function Get_All_Stock_Especifico_By_IdPedidoDet(ByVal pBeStockRes As clsBeStock_res,
                                                                   ByVal IdPedidoEnc As Integer,
                                                                   ByVal gIdBodega As Integer) As DataTable

        Get_All_Stock_Especifico_By_IdPedidoDet = Nothing

        Dim BeListStock As New List(Of clsBeStock)
        Dim DT As New DataTable
        Dim DT_Completo As New DataTable("Stock_Especifico")
        Dim pBeProductoOutput As clsBeProducto
        Dim vIdCliente As Integer = 0
        Dim vDiasVencimientoCliente As Double
        Dim pBePedido As New clsBeTrans_pe_enc
        Dim pBeConfigEnc As New clsBeI_nav_config_enc
        Dim pBeCliente As New clsBeCliente
        Dim pasos As Integer = -1

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            pasos = 1
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            pasos = 2
            Dim watch As Stopwatch = Stopwatch.StartNew()
            pasos = 3

            pBeProductoOutput = New clsBeProducto
            pBeProductoOutput = clsLnProducto.Get_Single_By_IdProductoBodega(pBeStockRes.IdProductoBodega,
                                                                             lConnection,
                                                                             lTransaction)
            pasos = 5
            If IdPedidoEnc = 0 Then

                IdPedidoEnc = clsLnTrans_pe_enc.Get_IdPedido_By_IdPickingEnc(pBeStockRes.IdPicking,
                                                                                  lConnection,
                                                                                  lTransaction)

                If IdPedidoEnc <> 0 Then
                    pBePedido = clsLnTrans_pe_enc.Get_Single_Header(IdPedidoEnc,
                                                                lConnection,
                                                                lTransaction)
                Else
                    Throw New Exception("El IdPedidoEnc es 0 no se puede continuar el proceso")
                End If
            Else
                pBePedido = clsLnTrans_pe_enc.Get_Single_Header(IdPedidoEnc,
                                                                lConnection,
                                                                lTransaction)

            End If

            If pBePedido IsNot Nothing Then
                pasos = 6
                vIdCliente = pBePedido.IdCliente
                vDiasVencimientoCliente = pBePedido.Dias_cliente

                pBeCliente = pBePedido.Cliente
                pasos = 7
                pBeStockRes.Control_Ultimo_Lote = pBeCliente.Control_Ultimo_Lote

                pasos = 8
                DT_Completo = Get_Stock_Disponible_Rem(pBeStockRes,
                                                       pBeProductoOutput,
                                                       vDiasVencimientoCliente,
                                                       pBeConfigEnc,
                                                       lConnection,
                                                       lTransaction,
                                                       False)
                pasos = 9
                lTransaction.Commit()
                pasos = 10
                watch.Stop()
                pasos = 11
                Debug.Print("Tiempo transcurrido GetAllStock: " & watch.Elapsed.TotalSeconds)
                pasos = 12
                Get_All_Stock_Especifico_By_IdPedidoDet = DT_Completo
                pasos = 13
            Else
                Throw New Exception("No se pudo obtener el pedido no se puede continuar el proceso")
            End If


        Catch ex As Exception
            ' gPasos = pasos
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("Paso {0} {1} {2} {3}", pasos, MethodBase.GetCurrentMethod().Name, ex.Message, ""))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

        ' gPasos = pasos

    End Function


    ''' <summary>
    ''' '#CKFK 20201228 Función creada para determinar si un IdStock es un pallet estandar o no
    ''' #EJC20221011: WMS: Corrección en manejo de transacción en función Es_Pallet_No_Estandar
    ''' </summary>
    ''' <param name="pStock"></param>
    ''' <returns></returns>
    Public Shared Function Es_Pallet_No_Estandar(pStock As clsBeStock) As Boolean

        Dim vResult As Boolean = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#CKFK 20210827 Se agregó el LicPlate en el query para saber si es un pallet no estandar 
            Dim vSQL As String = "SELECT pallet_no_estandar
                                  FROM   stock
                                  WHERE  IdUbicacion      = @IdUbicacion      AND
                                         IdProductoBodega = @IdProductoBodega AND
                                         IdProductoEstado = @IdProductoEstado AND  
                                         (IdPresentacion = @IdPresentacion OR @IdPresentacion IS NULL OR IdPresentacion IS NULL) AND
                                         IdUnidadMedida = @IdUnidadMedida   AND
                                         ISNULL(CONVERT(DATE, Fecha_Vence),CONVERT(DATE, '19000101')) = CONVERT(DATE, @Fecha_Vence) AND
                                         (Lote = @Lote OR @Lote IS NULL) AND
                                         pallet_no_estandar =  1 AND
                                         (lic_plate = @Lic_plate OR @Lic_plate IS NULL)"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdUbicacion", pStock.IdUbicacion)
                lCommand.Parameters.AddWithValue("@IdProductoBodega", pStock.IdProductoBodega)
                lCommand.Parameters.AddWithValue("@IdProductoEstado", pStock.IdProductoEstado)
                lCommand.Parameters.AddWithValue("@IdPresentacion", pStock.IdPresentacion)
                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pStock.IdUnidadMedida)
                lCommand.Parameters.AddWithValue("@Fecha_Vence", pStock.Fecha_vence)
                lCommand.Parameters.AddWithValue("@Lote", pStock.Lote)
                lCommand.Parameters.AddWithValue("@Lic_plate", pStock.Lic_plate)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vResult = Convert.ToBoolean(lReturnValue)
                Else
                    vResult = False ' caso 1: Nothing (sin filas) / caso 2: DBNull (NULL)
                End If
            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ""))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

        Return vResult

    End Function


    ''' <summary>
    ''' '#CKFK 20201228 Función creada para determinar si un IdStock de tipo pallet estandar tiene definidas la cantidad de posiciones o no
    ''' #EJC202210111140: WMS: Corrección en manejo de transacción y cierre de conexión en función Tiene_Posiciones.
    ''' </summary>
    ''' <param name="pStock"></param>
    ''' <returns></returns>
    Public Shared Function Tiene_Posiciones(pStock As clsBeStock) As Integer

        Dim vResult As Integer = 0
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try


            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#CKFK 20210827 Agregué al query el LicPlate para que devuelva un registro único cuando exista
            Dim vSQL As String = "SELECT stock_det.posiciones AS Cant
                                  FROM   stock INNER JOIN  stock_det ON stock.IdStock = stock_det.IdStock
                                  WHERE  stock.IdUbicacion      = @IdUbicacion      AND
                                         stock.IdProductoBodega = @IdProductoBodega AND
                                         stock.IdProductoEstado = @IdProductoEstado AND
                                         (stock.IdPresentacion = @IdPresentacion OR @IdPresentacion IS NULL OR stock.IdPresentacion IS NULL) AND
                                         stock.IdUnidadMedida = @IdUnidadMedida   AND
                                         ISNULL(CONVERT(DATE, stock.Fecha_Vence),CONVERT(DATE, '19000101')) = CONVERT(DATE, @Fecha_Vence) AND
                                         (stock.Lote = @Lote OR @Lote IS NULL) AND
                                         stock.Pallet_No_Estandar = 1 AND
                                         (stock.lic_plate = @Lic_plate OR @Lic_plate IS NULL)"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdUbicacion", pStock.IdUbicacion)
                lCommand.Parameters.AddWithValue("@IdProductoBodega", pStock.IdProductoBodega)
                lCommand.Parameters.AddWithValue("@IdProductoEstado", pStock.IdProductoEstado)
                lCommand.Parameters.AddWithValue("@IdPresentacion", pStock.IdPresentacion)
                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pStock.IdUnidadMedida)
                lCommand.Parameters.AddWithValue("@Fecha_Vence", pStock.Fecha_vence)
                lCommand.Parameters.AddWithValue("@Lote", pStock.Lote)
                lCommand.Parameters.AddWithValue("@Lic_plate", pStock.Lic_plate)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vResult = CInt(lReturnValue)
                End If
            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ""))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

        Return vResult

    End Function

    '#CKFK 20201228 Función creada para determinar si un IdStock de tipo pallet no estandar tiene definidas la cantidad de posiciones o no y cual es el valor
    Public Shared Function Tiene_Posiciones(pStock As clsBeStock,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As Integer

        Dim vResult As Integer = 0

        Try

            Dim vSQL As String = "SELECT stock_det.posiciones AS Cant
                                  FROM   stock INNER JOIN  stock_det ON stock.IdStock = stock_det.IdStock
                                  WHERE  stock.IdUbicacion      = @IdUbicacion      AND
                                         stock.IdProductoBodega = @IdProductoBodega AND
                                         stock.IdProductoEstado = @IdProductoEstado AND
                                         (stock.IdPresentacion = @IdPresentacion OR @IdPresentacion IS NULL OR stock.IdPresentacion IS NULL) AND
                                         stock.IdUnidadMedida = @IdUnidadMedida   AND
                                         ISNULL(CONVERT(DATE, stock.Fecha_Vence),CONVERT(DATE, '19000101')) = CONVERT(DATE, @Fecha_Vence) AND
                                         (stock.Lote = @Lote OR @Lote IS NULL) AND
                                         stock.Pallet_No_Estandar = 1 AND
                                         (stock.lic_plate = @Lic_plate OR @Lic_plate IS NULL)"

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdUbicacion", pStock.IdUbicacion)
                lCommand.Parameters.AddWithValue("@IdProductoBodega", pStock.IdProductoBodega)
                lCommand.Parameters.AddWithValue("@IdProductoEstado", pStock.IdProductoEstado)
                lCommand.Parameters.AddWithValue("@IdPresentacion", pStock.IdPresentacion)
                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pStock.IdUnidadMedida)
                lCommand.Parameters.AddWithValue("@Fecha_Vence", pStock.Fecha_vence)
                lCommand.Parameters.AddWithValue("@Lote", pStock.Lote)
                lCommand.Parameters.AddWithValue("@Lic_plate", pStock.Lic_plate)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vResult = CInt(lReturnValue)
                End If
            End Using

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1} {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ""))
        End Try

        Return vResult

    End Function

    Public Shared Function Get_Reporte_Stock_By_IdEmpresa_DT(ByVal pIdEmpresa As Integer) As DataTable

        Get_Reporte_Stock_By_IdEmpresa_DT = Nothing

        Try

            Dim vSQL As String = "SELECT IdProducto,Codigo,
							        nombre as Producto,NomEstado as Estado,
							        IdPresentacion,sum(isnull(CantidadSF,0)) as CantidadUMBas,
							        UnidadMedida,
							        SUM(isnull(Cantidad,0)) as CantidadPresentacion,Presentacion,
							        SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							        CASE WHEN FACTOR > 0
							        THEN 
								        ROUND(SUM(isnull(CantidadReservada/Factor,0)),6)
							        ELSE
								        0
							        END AS Cantidad_Reservada_Pres,
							        ROUND(SUM(isnull(CantidadSF,0)) - SUM(isnull(CantidadReservada,0)),6)  as Disponible_UMBas, 
							        CASE WHEN FACTOR > 0
							        THEN 
								        ROUND(SUM(isnull(Cantidad,0)) - SUM(isnull(CantidadReservada/Factor,0)),6)
							        ELSE
								        0
							        END AS Disponible_Presentación,
							        Peso,Lote,lic_plate,Fecha_Ingreso,Fecha_Vence, Nombre_Completo AS [Ubicación]
							        FROM VW_Stock_Res WHERE 1 > 0 "

            If pIdEmpresa <> 0 Then
                vSQL += " AND IdEmpresa = @IdEmpresa"
            End If

            vSQL += " Group by IdProducto,NomEstado,Fecha_Ingreso,Codigo,
						  Nombre,Presentacion,IdPresentacion,UnidadMedida,peso, 
						  Lote,fecha_vence, Nombre_Completo,lic_plate,
						  Factor "

            vSQL += "ORDER BY CODIGO, Nombre_Completo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdEmpresa <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdEmpresa", pIdEmpresa)
                        End If

                        'If pIdPropietarioBodega <> 0 Then
                        '    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        'End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Reporte_Stock_By_IdEmpresa_DT = lDataTable
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


    Public Shared Function Get_Stock_By_IdEmpresa_MI3(ByVal pIdEmpresa As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "SELECT * FROM VW_STOCK_RES WHERE IdEmpresa = @pIdEmpresa"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@pIdEmpresa", pIdEmpresa)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res()

                                If lRow("IdProducto") IsNot DBNull.Value AndAlso lRow("IdProducto") IsNot Nothing Then
                                    Obj.IdProducto = CType(lRow("IdProducto"), Integer)
                                End If

                                If lRow("IdProductoBodega") IsNot DBNull.Value AndAlso lRow("IdProductoBodega") IsNot Nothing Then
                                    Obj.IdProductoBodega = CType(lRow("IdProductoBodega"), Integer)
                                End If

                                If lRow("IdStock") IsNot DBNull.Value AndAlso lRow("IdStock") IsNot Nothing Then
                                    Obj.IdStock = CType(lRow("IdStock"), Integer)
                                End If

                                If lRow("Codigo") IsNot DBNull.Value AndAlso lRow("Codigo") IsNot Nothing Then
                                    Obj.Codigo_Producto = CType(lRow("Codigo"), String)
                                End If

                                If lRow("Propietario") IsNot DBNull.Value AndAlso lRow("Propietario") IsNot Nothing Then
                                    Obj.Propietario = CType(lRow("Propietario"), String)
                                End If

                                If lRow("Nombre") IsNot DBNull.Value AndAlso lRow("Nombre") IsNot Nothing Then
                                    Obj.Nombre_Producto = CType(lRow("Nombre"), String)
                                End If

                                If lRow("Codigo_Barra") IsNot DBNull.Value AndAlso lRow("Codigo_Barra") IsNot Nothing Then
                                    Obj.Codigo_Barra = CType(lRow("Codigo_Barra"), String)
                                End If

                                If lRow("NomEstado") IsNot DBNull.Value AndAlso lRow("NomEstado") IsNot Nothing Then
                                    Obj.NomEstado = CType(lRow("NomEstado"), String)
                                End If

                                If lRow("Presentacion") IsNot DBNull.Value AndAlso lRow("Presentacion") IsNot Nothing Then
                                    Obj.Nombre_Presentacion = CType(lRow("Presentacion"), String)
                                End If

                                If lRow("UnidadMedida") IsNot DBNull.Value AndAlso lRow("UnidadMedida") IsNot Nothing Then
                                    Obj.UMBas = CType(lRow("UnidadMedida"), String)
                                End If

                                If lRow("serial") IsNot DBNull.Value AndAlso lRow("serial") IsNot Nothing Then
                                    Obj.Serial = CType(lRow("serial"), String)
                                End If

                                If lRow("Cantidad") IsNot DBNull.Value AndAlso lRow("Cantidad") IsNot Nothing Then
                                    Obj.CantidadPresentacion = CType(lRow("Cantidad"), String)
                                End If

                                If lRow("CantidadSF") IsNot DBNull.Value AndAlso lRow("CantidadSF") IsNot Nothing Then
                                    Obj.CantidadUmBas = CType(lRow("CantidadSF"), String)
                                End If

                                If lRow("Fecha_Ingreso") IsNot DBNull.Value AndAlso lRow("Fecha_Ingreso") IsNot Nothing Then
                                    Obj.Fecha_ingreso = CType(lRow("Fecha_Ingreso"), Date)
                                End If

                                If lRow("Fecha_Vence") IsNot DBNull.Value AndAlso lRow("Fecha_Vence") IsNot Nothing Then
                                    Obj.Fecha_Vence = CType(lRow("Fecha_Vence"), Date)
                                End If

                                If lRow("lote") IsNot DBNull.Value AndAlso lRow("lote") IsNot Nothing Then
                                    Obj.Lote = CType(lRow("lote"), String)
                                End If

                                If lRow("lic_plate") IsNot DBNull.Value AndAlso lRow("lic_plate") IsNot Nothing Then
                                    Obj.Lic_plate = CType(lRow("lic_plate"), String)
                                End If

                                If lRow("IdRecepcionEnc") IsNot DBNull.Value AndAlso lRow("IdRecepcionEnc") IsNot Nothing Then
                                    Obj.IdRecepcionEnc = CType(lRow("IdRecepcionEnc"), Integer)
                                End If

                                If lRow("IdUbicacionActual") IsNot DBNull.Value AndAlso lRow("IdUbicacionActual") IsNot Nothing Then
                                    Obj.IdUbicacion = CType(lRow("IdUbicacionActual"), Integer)
                                End If

                                If lRow("Ubicacion_Tramo") IsNot DBNull.Value AndAlso lRow("Ubicacion_Tramo") IsNot Nothing Then
                                    Obj.Ubicacion_Tramo = CType(lRow("Ubicacion_Tramo"), String)
                                End If

                                If lRow("Nombre_Completo") IsNot DBNull.Value AndAlso lRow("Nombre_Completo") IsNot Nothing Then
                                    Obj.Ubicacion_Nombre = CType(lRow("Nombre_Completo"), String)
                                End If

                                If lRow("largo") IsNot DBNull.Value AndAlso lRow("largo") IsNot Nothing Then
                                    Obj.LargoUbicacion = CType(lRow("largo"), Integer)
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

    Public Shared Function Get_All_Stock_por_Consolidador_Report(ByVal pIdBodega As Integer,
                                                               ByVal pIdPropietarioBodega As Integer) As DataTable


        Get_All_Stock_por_Consolidador_Report = Nothing

        Try

            Dim vSQL As String = "SELECT 
                                  Propietario, 
                                  IdProducto,
                                  Codigo, 
                                  Codigo_barra,                                  
                                  Nombre,                                  
                                  UnidadMedida,
                                  IdPresentacion,
                                  Presentacion,
                                  NomEstado as Estado,
                                  Sum(isnull(CantidadSF,0)) as CantidadUMBas,
							      SUM(isnull(Cantidad,0)) as CantidadPresentacion,
							      SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							        CASE WHEN FACTOR > 0
							        THEN 
								        ROUND(SUM(isnull(CantidadReservada/Factor,0)),6)
							        ELSE
								        0
							        END AS Cantidad_Reservada_Pres,
							        ROUND(SUM(isnull(CantidadSF,0)) - SUM(isnull(CantidadReservada,0)),6)  as Disponible_UMBas, 
							        CASE WHEN FACTOR > 0
							        THEN 
								        ROUND(SUM(isnull(Cantidad,0)) - SUM(isnull(CantidadReservada/Factor,0)),6)
							        ELSE
								        0
							        END AS Disponible_Presentación,
							        SUM(isnull(Peso,0)) as Peso
							        FROM VW_Stock_Res_Consolidador WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND consolidador = @consolidador"
            End If

            vSQL += " Group by IdProducto,NomEstado,Codigo,
						  Nombre,Presentacion,IdPresentacion,UnidadMedida, 
						  Factor, Propietario, codigo_barra "

            vSQL += "ORDER BY CODIGO, Nombre "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@consolidador", pIdPropietarioBodega)

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


    Public Shared Function Get_All_Stock_Consolidado_DT_Por_Propietario(ByVal pIdBodega As Integer) As DataTable


        Get_All_Stock_Consolidado_DT_Por_Propietario = Nothing

        Try

            Dim vSQL As String = "SELECT 
                                    Propietario, 
                                    IdProducto,
                                    Codigo, 
                                    Codigo_barra,                                  
                                    Nombre,                                  
                                    UnidadMedida,
                                    IdPresentacion,
                                    Presentacion,
                                    NomEstado as Estado,
                                    Sum(isnull(CantidadSF,0)) as CantidadUMBas,
							        SUM(isnull(Cantidad,0)) as CantidadPresentacion,
							        SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							        CASE WHEN FACTOR > 0
							            THEN 
								            ROUND(SUM(isnull(CantidadReservada/Factor,0)),6)
							            ELSE
								            0
							        END AS Cantidad_Reservada_Pres,
							        ROUND(SUM(isnull(CantidadSF,0)) - SUM(isnull(CantidadReservada,0)),6)  as Disponible_UMBas, 
							        CASE WHEN FACTOR > 0
							            THEN 
								            ROUND(SUM(isnull(Cantidad,0)) - SUM(isnull(CantidadReservada/Factor,0)),6)
							            ELSE
								            0
							        END AS Disponible_Presentación,
							        SUM(isnull(Peso,0)) as Peso, Lic_plate,Lote,Fecha_Vence,
                                    Codigo_Talla as Talla,
                                    Codigo_Color as Color
							        FROM VW_Stock_Res WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            vSQL += " Group by IdProducto,NomEstado,Codigo,
						  Nombre,Presentacion,IdPresentacion,UnidadMedida, 
						  Factor, Propietario, codigo_barra, lic_plate,lote,fecha_vence, Codigo_Talla, Codigo_Color "

            vSQL += "ORDER BY CODIGO, Nombre "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

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

    Public Shared Function Get_Reporte_Stock_y_Posiciones(ByVal pIdBodega As Integer,
                                                          ByVal pIdPropietarioBodega As Integer) As DataTable

        Get_Reporte_Stock_y_Posiciones = Nothing

        Try
            'GT 230720211523: Campos de poliza para mejorar busqueda cealsa
            '#MECR27082025: Se agregaron campos de talla y color
            Dim vSQL As String = "SELECT IdProducto,Codigo,
							nombre as Producto,NomEstado as Estado,
							IdPresentacion,sum(isnull(CantidadSF,0)) as CantidadUMBas,
							UnidadMedida,
							SUM(isnull(Cantidad,0)) as CantidadPresentacion,Presentacion,
							SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							CASE WHEN FACTOR > 0
							THEN 
								--ROUND(SUM(isnull(CantidadReservada/Factor,0)),6)
                                    ROUND(isnull(CantidadReservada/Factor,0),6)
							ELSE
								0
							END AS Cantidad_Reservada_Pres,
							ROUND(SUM(isnull(CantidadSF,0)) - SUM(isnull(CantidadReservada,0)),6)  as Disponible_UMBas, 
							CASE WHEN FACTOR > 0
							THEN 
								--ROUND(SUM(isnull(Cantidad,0)) - SUM(isnull(CantidadReservada/Factor,0)),6)
                                    ROUND(isnull(Cantidad,0) - isnull(CantidadReservada/Factor,0),6)
							ELSE
								0
							END AS Disponible_Presentación,
							Peso,Lote,lic_plate as Licencia,Fecha_Ingreso,Fecha_Vence, 
                            Nombre_Completo AS [Ubicación],Posiciones,pallet_no_estandar AS Pallet_No_Estandard,IdStock,
                            Numero_poliza as Numero_Orden,codigo_poliza as Codigo_Poliza,Documento_Ingreso,clasificacion as Clasificacion,
                            Codigo_Talla as Talla,
                            Codigo_Color as Color
							FROM VW_Stock_Res WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega"
            End If

            vSQL += " Group by IdProducto,NomEstado,Fecha_Ingreso,Codigo,
						  Nombre,Presentacion,IdPresentacion,UnidadMedida,peso, 
						  Lote,fecha_vence, Nombre_Completo,lic_plate,
						  Factor,Posiciones,pallet_no_estandar,IdStock,Numero_poliza,codigo_poliza,Documento_Ingreso
                          ,CantidadReservada,Cantidad,clasificacion, Codigo_Talla, Codigo_Color "

            vSQL += "ORDER BY CODIGO, Nombre_Completo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        If pIdPropietarioBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Corregir_Lotes(ByRef lLotesACorregir As List(Of clsBeProducto_lote_correccion)) As Integer

        Corregir_Lotes = False

        Dim vRegistros As Integer = 0

        Try


            Dim vSQL As String = " "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    For Each L In lLotesACorregir

                        vSQL = "update stock set fecha_vence = @fecha_vence where IdStock = @idstock
                                update trans_inv_stock set fecha_vence = @fecha_vence where IdStock = @idstock
                                update stock_hist set fecha_vence = @fecha_vence where IdStock = @idstock
                                update trans_inventario_det set fecha_vence =@fecha_vence where IdStock = @idstock
                                update trans_picking_ubic set fecha_vence = @fecha_vence where IdStock = @idstock
                                update stock_res set fecha_vence = @fecha_vence where IdStock = @idstock"


                        Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                            lCommand.Parameters.AddWithValue("@fecha_vence", L.Vence)
                            lCommand.Parameters.AddWithValue("@idstock", L.IdStock)

                            Try

                                Dim lReturnValue As Integer = lCommand.ExecuteNonQuery()

                                If lReturnValue > 0 Then

                                    Dim vInsert As String = "update stock set fecha_vence = " & FormatoFechas.fFecha(L.Vence) & " where IdStock = " & L.IdStock

                                    clsLnLog_error_wms.Agregar_Error(vInsert)

                                    vRegistros += lReturnValue

                                    Debug.WriteLine(vInsert)

                                Else
                                    Debug.WriteLine("OJO.")
                                End If

                            Catch ex As Exception
                                Debug.WriteLine("Error: 0" & ex.Message)
                            End Try

                        End Using

                    Next

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Corregir_Lotes = vRegistros

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' '#CKFK 20220701 Agregué esta función para  listar los productos para reabastecimeinto en la HH
    ''' </summary>
    ''' <param name="pIdProducto"></param>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    Public Shared Function Get_All_Products_For_Reabastecimiento(ByVal pIdProducto As Integer,
                                                                 ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim lBeStock As New List(Of clsBeStock)
                    Dim pBeStockRes As New clsBeStock_res
                    Dim vIdProductoBodega As Integer
                    Dim pExcluirUbicacionPicking As Boolean = True
                    Dim pBeProductoOutput As New clsBeProducto
                    Dim pBeConfigEnc As New clsBeI_nav_config_enc

                    If pIdProducto <> 0 Then
                        pBeProductoOutput.IdProducto = pIdProducto
                        clsLnProducto.GetSingle(pBeProductoOutput)
                        vIdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pIdProducto,
                                                                                                                 pIdBodega,
                                                                                                                 lConnection,
                                                                                                                 lTransaction)
                    End If

                    pBeConfigEnc = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario(pIdBodega,
                                                                                                 pBeProductoOutput.IdPropietario,
                                                                                                 lConnection,
                                                                                                 lTransaction)

                    pBeStockRes.IdProductoBodega = vIdProductoBodega
                    pBeStockRes.Control_Ultimo_Lote = False
                    pBeStockRes.IdPresentacion = 0
                    pBeStockRes.Atributo_Variante_1 = ""
                    pBeStockRes.IdUbicacionAbastecerCon = 0
                    pBeStockRes.IdBodega = pIdBodega

                    Dim lDataTable As New DataTable
                    lDataTable = lStock_Reabasto_DT(pBeStockRes,
                                                    pBeProductoOutput,
                                                    0,
                                                    pBeConfigEnc,
                                                    lConnection,
                                                    lTransaction,
                                                    pExcluirUbicacionPicking)
                    Dim RDatos As New DataTable
                    Dim vCantidadReservada As Double
                    Dim IdStock As Integer

                    RDatos = lDataTable.Clone

                    If lDataTable.Rows.Count > 0 Then

                        For Each lrow As DataRow In lDataTable.Rows
                            If lrow("IdStock") IsNot DBNull.Value AndAlso lrow("IdStock") IsNot Nothing Then

                                IdStock = CType(lrow("IdStock"), Integer)

                                vCantidadReservada = clsLnStock_res.Get_Cantidad_ReservadaUMBas_By_IdStock(IdStock,
                                                                                                    False,
                                                                                                    lConnection,
                                                                                                    lTransaction)

                                If vCantidadReservada <> 0 Then

                                    If lrow("Cant_UMBas") IsNot DBNull.Value AndAlso lrow("Cant_UMBas") IsNot Nothing Then
                                        lrow("Cant_UMBas") -= vCantidadReservada
                                    End If

                                End If
                            End If
                        Next

                    End If

                    Dim Obj As clsBeVW_stock_res

                    If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                        For Each lRow As DataRow In lDataTable.Rows

                            If lRow("Cant_UMBas") > 0 Then

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar_Reabasto(Obj, lRow, lConnection, lTransaction)

                                Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                                Obj.UbicacionActual = clsLnBodega_ubicacion.Get_Single_With_Tramo_And_Sector(Obj.IdUbicacion,
                                                                                                             Obj.IdBodega,
                                                                                                             lConnection,
                                                                                                             lTransaction)

                                lReturnList.Add(Obj)

                            End If

                        Next

                    End If

                    lTransaction.Commit()

                End Using

                lConnection.Close()

                Return lReturnList

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_LP(ByVal pLicPlate As String,
                                         ByVal pIdBodega As Integer,
                                         ByVal lConnection As SqlConnection,
                                         ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Get_All_By_LP = Nothing

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            'JP20171027 - filtro por ubicacion y producto
            Dim vSQL As String = "Select * from VW_Stock_Res WHERE 1 = 1 "
            If pLicPlate <> "" Then vSQL &= " AND lic_plate = @pLicPlate "
            If pIdBodega <> 0 Then vSQL &= " AND IdBodega = @pIdBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                If pLicPlate <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@pLicPlate", pLicPlate)
                If pIdBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res
                        clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
                        Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                        lReturnList.Add(Obj)

                    Next

                    Get_All_By_LP = lReturnList

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_LP_And_IdUbicacion(ByVal pLicPlate As String,
                                                         ByVal pIdBodega As Integer,
                                                         ByVal pIdUbicacion As Integer,
                                                         ByVal pIdPresentacion As Integer,
                                                         ByVal lConnection As SqlConnection,
                                                         ByVal lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Get_All_By_LP_And_IdUbicacion = Nothing

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            'JP20171027 - filtro por ubicacion y producto
            Dim vSQL As String = "Select * from VW_Stock_Res WHERE 1 = 1 "
            If pLicPlate <> "" Then vSQL &= " AND lic_plate = @pLicPlate "
            If pIdBodega <> 0 Then vSQL &= " AND IdBodega = @pIdBodega "
            If pIdUbicacion <> 0 Then vSQL &= " AND IdUbicacion = @pIdUbicacion "

            If pIdPresentacion <> 0 Then
                vSQL += " AND IdPresentacion =  @IdPresentacion "
            Else
                vSQL += " AND IdPresentacion IS NULL "
            End If


            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                If pLicPlate <> "" Then lDTA.SelectCommand.Parameters.AddWithValue("@pLicPlate", pLicPlate)
                If pIdBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdBodega", pIdBodega)
                If pIdUbicacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@pIdUbicacion", pIdUbicacion)

                If pIdPresentacion <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res
                        clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
                        Obj.UbicacionActual.IdUbicacion = Obj.IdUbicacion
                        lReturnList.Add(Obj)

                    Next

                    Get_All_By_LP_And_IdUbicacion = lReturnList

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Lp_In_Stock(ByVal lic_plate As String,
                                              ByVal pIdBodega As Integer,
                                              ByVal pConection As SqlConnection,
                                              ByVal pTransaction As SqlTransaction) As Boolean

        Existe_Lp_In_Stock = False

        Try

            Const sp As String = "SELECT * FROM Stock Where(lic_plate = @lic_plate AND IdBodega = @IdBodega) "

            Using lDTA As New SqlDataAdapter(sp, pConection)

                lDTA.SelectCommand.Transaction = pTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@LIC_PLATE", lic_plate)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Existe_Lp_In_Stock = lDataTable.Rows.Count > 0

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Existe_Lp_In_Stock_By_IdBodega(ByVal pLic_plate As String,
                                                          ByVal pIdBodega As Integer,
                                                          ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As Boolean

        Existe_Lp_In_Stock_By_IdBodega = False

        Try

            Const sp As String = "SELECT * FROM Stock Where(lic_plate = @lic_plate)  AND (IdBodega = @IdBodega)"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@LIC_PLATE", pLic_plate))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", pIdBodega))

            Dim dt As New DataTable
            Dim pBeStock_rec As New clsBeStock_rec
            dad.Fill(dt)

            Existe_Lp_In_Stock_By_IdBodega = dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' '#EJC20221011_1148: WMS - Agregué función Existe_Stock_By_IdBodega_And_IdRecepcionEnc para validar si existe stock antes de anular una recepción.
    ''' </summary>
    ''' <param name="IdBodega"></param>
    ''' <param name="IdRecepcionEnc"></param>
    ''' <returns></returns>
    Public Shared Function Existe_Stock_By_IdBodega_And_IdRecepcionEnc(ByVal IdBodega As Integer,
                                                                       ByVal IdRecepcionEnc As Integer) As Boolean

        Dim lReturnListStock As New List(Of clsBeVW_stock_res)
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Existe_Stock_By_IdBodega_And_IdRecepcionEnc = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT * from VW_Stock_Res 
                                  WHERE IdBodega=@IdBodega 
                                  AND IdRecepcionEnc=@IdRecepcionEnc "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", IdRecepcionEnc)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_Stock_By_IdBodega_And_IdRecepcionEnc = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#AT20221102 Crear funcion para obtener inventario
    Public Shared Function Get_Cant_By_Licencia_And_IdBodega(ByVal pLicencia As String,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pIdProductoBodega As Integer,
                                                            ByVal pIdUbicacion As Integer,
                                                            ByVal pIdPresentacion As Integer,
                                                            ByRef lConnection As SqlConnection,
                                                            ByRef lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res_CI)

        Get_Cant_By_Licencia_And_IdBodega = Nothing

        Try

            Dim lReturnList As New List(Of clsBeVW_stock_res_CI)

            Dim sp As String = "SELECT codigo, nombre, UnidadMedida UM, Cantidad_UMBas ExistUMBAs, IdPresentacion Pres, Cantidad_Presentacion  ExistPres, 
                                         CantidadReservadaUmBas ReservadoUMBAs, Cantidad_Reservada_Pres ResPres, Disponible_UMBas DisponibleUMBas, 
                                         Disponible_Presentacion DispPres, lote, fecha_vence, NomEstado Estado, IdUbicacion idUbic, lic_plate LicPlate, 
                                         IdProductoEstado, IdProductoBodega, factor
                                  FROM vw_stock_res 
                                  WHERE lic_plate = @Licencia AND IdBodega = @IdBodega  AND IdProductoBodega = @IdProductoBodega AND IdUbicacion = @IdUbicacion "

            If pIdPresentacion = 0 Then
                sp += " AND (IdPresentacion = 0 OR IdPresentacion is null)"
            Else
                sp += " AND (IdPresentacion = @IdPresentacion)"
            End If


            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@Licencia", pLicencia)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProductoBodega)
            dad.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

            If pIdPresentacion <> 0 Then
                dad.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pIdPresentacion)
            End If

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeStock As New clsBeVW_stock_res_CI

            If dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows

                    vBeStock = New clsBeVW_stock_res_CI
                    clsLnVW_stock_res.Cargar_CI(vBeStock, dr)
                    lReturnList.Add(vBeStock)

                Next

                Get_Cant_By_Licencia_And_IdBodega = lReturnList

            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Stock_By_Licencia_And_Codigo2(ByVal pLicencia As String,
                                                             ByVal pCodigo As String,
                                                             ByVal IdBodega As Integer) As List(Of clsBeVW_stock_res)

        Get_Stock_By_Licencia_And_Codigo2 = Nothing

        Try

            Dim lBeVWStockRes As New List(Of clsBeVW_stock_res)
            Dim Obj As New clsBeProducto
            Dim IdxProducto As Integer = -1

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM vw_stock_res 
										  WHERE lic_plate=@lic_plate and codigo=@codigo 
										  AND IdBodega = @IdBodega "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pLicencia)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigo)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim vStock As New clsBeVW_stock_res

                            For Each lRow As DataRow In lDataTable.Rows

                                vStock = New clsBeVW_stock_res
                                clsLnVW_stock_res.Cargar(vStock, lRow, lConnection, lTransaction)
                                lBeVWStockRes.Add(vStock)

                            Next

                            Get_Stock_By_Licencia_And_Codigo2 = lBeVWStockRes

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

    Public Shared Function Get_Stock_By_LicensePlate2(ByVal pLicensePlate As String,
                                                      ByVal IdBodega As Integer) As List(Of clsBeVW_stock_res)

        Get_Stock_By_LicensePlate2 = Nothing

        Try

            Dim lBeVWStockRes As New List(Of clsBeVW_stock_res)
            Dim BeProducto As New clsBeProducto
            Dim IdxProducto As Integer = -1

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM vw_stock_res 
										  WHERE  lic_plate=@lic_plate 
                                          AND IdBodega = @IdBodega "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text

                        lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pLicensePlate)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim vStock As New clsBeVW_stock_res

                            For Each lRow As DataRow In lDataTable.Rows

                                vStock = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(vStock,
                                                         lRow,
                                                         lConnection,
                                                         lTransaction)


                                lBeVWStockRes.Add(vStock)

                            Next

                            Get_Stock_By_LicensePlate2 = lBeVWStockRes

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lBeVWStockRes

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Stock_By_LicensePlate_And_IdUbicacion(ByVal pLicensePlate As String,
                                                                     ByVal IdBodega As Integer,
                                                                     ByVal IdUbicacion As Integer,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As clsBeVW_stock_res

        Get_Stock_By_LicensePlate_And_IdUbicacion = Nothing

        Try



            Dim vSQL As String = "Select * from vw_stock_res 
    						              WHERE 1 = 1 "

            If IdUbicacion <> 0 Then vSQL &= " AND IdUbicacionActual = @IdUbicacionActual "
            If pLicensePlate <> "0" AndAlso Not String.IsNullOrEmpty(pLicensePlate) Then vSQL &= " AND lic_plate=@lic_plate "
            If IdBodega <> 0 Then vSQL &= " AND IdBodega = @IdBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                If pLicensePlate <> "0" AndAlso Not String.IsNullOrEmpty(pLicensePlate) Then lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pLicensePlate)
                If IdUbicacion <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacionActual", IdUbicacion)
                If IdBodega <> 0 Then lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim vStock As New clsBeVW_stock_res

                    For Each lRow As DataRow In lDataTable.Rows

                        vStock = New clsBeVW_stock_res
                        clsLnVW_stock_res.Cargar(vStock, lRow, lConnection, lTransaction)
                        Return vStock

                    Next

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' #EJC2023022223246: Función creada (como parche, feo lo sé) para prevenir que se inserte por alguna razón que desconozco
    ''' en el finalizar dos veces el stock cuando se salta alguna condición rara, caso de BYB recepción 4904 finalizada en fecha diferente.
    ''' </summary>
    ''' <param name="pBeRecepcionDet"></param>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    Public Shared Function Get_Single_By_BeRecepcionDet(ByVal pBeRecepcionDet As clsBeTrans_re_det,
                                                        ByVal pIdBodega As Integer) As clsBeVW_stock_res

        Get_Single_By_BeRecepcionDet = Nothing

        Try

            Dim BeVWStockRes As New clsBeVW_stock_res

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim vSQL As String = "SELECT * FROM VW_Stock_Res 
                                          WHERE IdBodega = @IdBodega 
                                          AND IdRecepcionEnc = @IdRecepcionEnc 
                                          AND IdRecepcionDet = @IdRecepcionDet 
                                          AND IdProductoBodega = @IdProductoBodega "

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pBeRecepcionDet.IdRecepcionEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pBeRecepcionDet.IdRecepcionDet)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pBeRecepcionDet.IdProductoBodega)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Dim lRow As DataRow = lDataTable.Rows(0)

                            If Not lRow Is Nothing Then
                                BeVWStockRes = New clsBeVW_stock_res
                                clsLnVW_stock_res.Cargar(BeVWStockRes, lRow, lConnection, lTransaction)
                                Get_Single_By_BeRecepcionDet = BeVWStockRes
                            End If

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return BeVWStockRes

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Get_Single_By_BeRecepcionDet(ByVal pBeRecepcionDet As clsBeTrans_re_det,
                                                        ByVal pIdBodega As Integer,
                                                        ByVal lConnection As SqlConnection,
                                                        ByVal lTransaction As SqlTransaction) As clsBeVW_stock_res

        Get_Single_By_BeRecepcionDet = Nothing

        Try

            Dim BeVWStockRes As New clsBeVW_stock_res

            Dim vSQL As String = "SELECT * FROM VW_Stock_Res 
                                          WHERE IdBodega = @IdBodega 
                                          AND IdRecepcionEnc = @IdRecepcionEnc 
                                          AND IdRecepcionDet = @IdRecepcionDet 
                                          AND IdProductoBodega = @IdProductoBodega "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pBeRecepcionDet.IdRecepcionEnc)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", pBeRecepcionDet.IdRecepcionDet)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pBeRecepcionDet.IdProductoBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim lRow As DataRow = lDataTable.Rows(0)

                    If Not lRow Is Nothing Then
                        BeVWStockRes = New clsBeVW_stock_res
                        clsLnVW_stock_res.Cargar(BeVWStockRes, lRow, lConnection, lTransaction)
                        Get_Single_By_BeRecepcionDet = BeVWStockRes
                    End If

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Existencia_Con_Reserva_By_IdProducto(ByRef pBeStockConsulta As clsBeStock,
                                                                    ByVal pIdBodega As Integer,
                                                                    ByVal pIdUbicacion As Integer,
                                                                    Optional ByVal ConEstado As Boolean = True,
                                                                    Optional ByVal ConLote As Boolean = False,
                                                                    Optional ByVal pDiasVencimientoCliente As Integer = 0,
                                                                    Optional ByRef pExcluirUbicacionesPickign As Boolean = False,
                                                                    Optional ByRef pConection As SqlConnection = Nothing,
                                                                    Optional ByRef ptransaction As SqlTransaction = Nothing) As Boolean

        Get_Existencia_Con_Reserva_By_IdProducto = False

        Dim EsTransaccionExterna As Boolean = Not (pConection Is Nothing AndAlso ptransaction Is Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim cmd As New SqlCommand
        Dim vCantDis As Double = 0
        Dim vPesoDis As Double = 0
        Dim vCantRes As Double = 0
        Dim vCantDispoSinUbicacionesPicking As Double = 0

        Try

            If Not EsTransaccionExterna Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            Else
                lConnection = pConection : lTransaction = ptransaction
            End If

            vCantDis = Get_Cantidad_Disponible(pBeStockConsulta,
                                              pIdBodega,
                                              lConnection,
                                              lTransaction,
                                              pDiasVencimientoCliente,
                                              ConEstado,
                                              ConLote,
                                              pExcluirUbicacionesPickign,
                                              pIdUbicacion)

            vPesoDis = Get_Peso_Disponible(pBeStockConsulta,
                                           lConnection,
                                           lTransaction,
                                           ConEstado,
                                           ConLote)

            vCantRes = Get_Cantidad_Reservada(pBeStockConsulta,
                                              lConnection,
                                              lTransaction,
                                              ConEstado,
                                              False,
                                              pExcluirUbicacionesPickign,
                                              pIdUbicacion)


            If pBeStockConsulta.IdPresentacion <> 0 Then
                If vCantRes <> 0 Then
                    vCantRes = vCantRes / IIf(pBeStockConsulta.Presentacion.Factor > 0, pBeStockConsulta.Presentacion.Factor, 1)
                End If
            End If

            If vCantDis = 0 OrElse (vCantRes > vCantDis) Then
                pBeStockConsulta.Cantidad = 0
                pBeStockConsulta.Peso = 0
            Else
                pBeStockConsulta.Cantidad = vCantDis - vCantRes
                pBeStockConsulta.Peso = vPesoDis
            End If

            pBeStockConsulta.Cantidad_Reservada = vCantRes

            If Not EsTransaccionExterna Then lTransaction.Commit()

            Get_Existencia_Con_Reserva_By_IdProducto = True

        Catch ex As Exception
            If Not EsTransaccionExterna Then If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not EsTransaccionExterna Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
            End If
            cmd.Dispose()
        End Try

    End Function


    Public Shared Function Existe_Stock_By_IdBodega(ByVal IdBodega As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Boolean

        Existe_Stock_By_IdBodega = False

        Try

            Dim vSQL As String = "SELECT * from VW_Stock_Res 
                                  WHERE IdBodega=@IdBodega  "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_Stock_By_IdBodega = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK20231008 Creé esta función para obtener el stock de un IdProductoBodega
    Public Shared Function Get_Stock_By_IdProductoBodega(ByVal pIdProdductoBodega As Integer) As DataTable

        Get_Stock_By_IdProductoBodega = Nothing

        Try

            Dim vSQL As String = "SELECT IdProducto,Codigo,
							      nombre as Producto,NomEstado as Estado,
							      IdPresentacion,sum(isnull(CantidadSF,0)) as CantidadUMBas,
							      UnidadMedida,
							      SUM(isnull(Cantidad_Presentacion,0)) as CantidadPresentacion,
                                  Presentacion,
							      SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							      SUM(isnull(Cantidad_Reservada_Pres,0)) AS Cantidad_Reservada_Pres,							      
                                  SUM(isnull(Disponible_UMBas,0))  as Disponible_UMBas, 
							      SUM(isnull(Disponible_Presentacion,0)) AS Disponible_Presentación,
							      Peso,Lote,lic_plate as Licencia,Fecha_Ingreso,
                                  Fecha_Vence, Nombre_Completo AS [Ubicación],
                                  codigo_poliza,Numero_poliza numero_orden,ubicacion_picking, Area, Factor, IdUbicacion, 
                                  dbo.Nombre_Tramo(IdTramo, IdBodega) Tramo
							      FROM VW_Stock_Res WHERE IdProductoBodega = @IdProductoBodega"


            'vSQL += " AND (Estado_Reserva NOT IN ('PICKEADO','VERIFICADO') OR ESTADO_RESERVA IS NULL) "

            vSQL += " Group by IdProducto,NomEstado,Fecha_Ingreso,Codigo,
					  Nombre,Presentacion,IdPresentacion,UnidadMedida,peso, 
					  Lote,fecha_vence, Nombre_Completo,lic_plate,
				      Factor,codigo_poliza,Numero_poliza, CantidadReservada,Cantidad,
                      CantidadSF,ubicacion_picking,Area, Factor, IdUbicacion, IdTramo, IdBodega  "

            vSQL += "ORDER BY CODIGO, Nombre_Completo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pIdProdductoBodega)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Existe_Lote(ByVal IdBodega As Integer, ByVal Lote As String) As Date

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransactiona As SqlTransaction = Nothing

        Existe_Lote = New Date(1900, 1, 1)

        Try

            lConnection.Open() : lTransactiona = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT fecha_vence FROM Stock Where(lote = @Lote AND IdBodega = @IdBodega)
                                  UNION
                                  SELECT fecha_vence FROM Stock_rec Where(lote = @Lote AND IdBodega = @IdBodega)
                                  UNION
                                  SELECT a.fecha_vence FROM Stock_hist a
                                  INNER JOIN producto_bodega b on b.IdProductoBodega = a.IdProductoBodega
                                  INNER JOIN bodega c on c.IdBodega = b.IdBodega 
                                  Where(a.lote = @Lote AND c.IdBodega = @IdBodega)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransactiona) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdBodega", IdBodega))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Lote", Lote))

            Dim dt As New DataTable
            dad.Fill(dt)

            '#GT22122023: si no hay tabla, retornar fecha default.
            If dt.Rows.Count > 0 Then
                Existe_Lote = IIf(IsDBNull(dt.Rows(0).Item("fecha_vence")), New Date(1900, 1, 1), dt.Rows(0).Item("fecha_vence"))
            Else
                Existe_Lote = New Date(1900, 1, 1)
            End If

            lTransactiona.Commit()

        Catch ex As Exception
            If Not lTransactiona Is Nothing Then lTransactiona.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Reporte_Stock_By_IdBodega_and_IdPropietario(ByVal pIdBodega As Integer,
                                                                           ByVal pIdPropietarioBodega As Integer) As DataTable

        Get_Reporte_Stock_By_IdBodega_and_IdPropietario = Nothing

        Try

            '#MECR04092025: Se agrego columna de Talla y Color
            Dim vSQL As String = "SELECT Bodega,Propietario,IdProducto,Codigo,
							      nombre as Producto,NomEstado as Estado,
							      IdPresentacion,sum(isnull(CantidadSF,0)) as CantidadUMBas,
							      UnidadMedida,
							      SUM(isnull(Cantidad_Presentacion,0)) as CantidadPresentacion,
                                  Presentacion,
							      SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							      SUM(isnull(Cantidad_Reservada_Pres,0)) AS Cantidad_Reservada_Pres,							      
                                  SUM(isnull(Disponible_UMBas,0))  as Disponible_UMBas, 
							      SUM(isnull(Disponible_Presentacion,0)) AS Disponible_Presentación,
							      Peso,Lote,lic_plate as Licencia,
                                  Fecha_Ingreso,
                                  Fecha_Vence, Nombre_Completo AS [Ubicación],
                                  codigo_poliza,Numero_poliza numero_orden,ubicacion_picking, Area, Factor, IdUbicacion, 
                                  dbo.Nombre_Tramo(IdTramo, IdBodega) Tramo,IdStock, Codigo_Talla as Talla, Codigo_Color as Color
							      FROM VW_Stock_Res WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega "
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " and IdPropietarioBodega= @IdPropietarioBodega "
            End If

            vSQL += " Group by Bodega,Propietario,IdProducto,NomEstado,Fecha_Ingreso,Codigo,
					  Nombre,Presentacion,IdPresentacion,UnidadMedida,peso, 
					  Lote,fecha_vence, Nombre_Completo,lic_plate,
				      Factor,codigo_poliza,Numero_poliza, CantidadReservada,Cantidad,
                      CantidadSF,ubicacion_picking,Area, Factor, IdUbicacion, IdTramo, IdBodega, IdStock, Codigo_Talla, Codigo_Color "

            vSQL += "ORDER BY CODIGO, Nombre_Completo, IdStock "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        If pIdPropietarioBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Get_Reporte_Stock(ByVal pIdBodega As Integer) As DataTable

        Get_Reporte_Stock = Nothing

        Try

            Dim vSQL As String = "SELECT IdProducto,Codigo,
							      nombre as Producto,NomEstado as Estado,
							      IdPresentacion,sum(isnull(CantidadSF,0)) as CantidadUMBas,
							      UnidadMedida,
							      SUM(isnull(Cantidad_Presentacion,0)) as CantidadPresentacion,
                                  Presentacion,
							      SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							      SUM(isnull(Cantidad_Reservada_Pres,0)) AS Cantidad_Reservada_Pres,							      
                                  SUM(isnull(Disponible_UMBas,0))  as Disponible_UMBas, 
							      SUM(isnull(Disponible_Presentacion,0)) AS Disponible_Presentación,
							      Peso,Lote,lic_plate as Licencia,Fecha_Ingreso,
                                  Fecha_Vence, Nombre_Completo AS [Ubicación],
                                  codigo_poliza,Numero_poliza numero_orden,ubicacion_picking, Area, Factor, IdUbicacion, 
                                  dbo.Nombre_Tramo(IdTramo, IdBodega) Tramo
							      FROM VW_Stock_Res WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            vSQL += " Group by IdProducto,NomEstado,Fecha_Ingreso,Codigo,
					  Nombre,Presentacion,IdPresentacion,UnidadMedida,peso, 
					  Lote,fecha_vence, Nombre_Completo,lic_plate,
				      Factor,codigo_poliza,Numero_poliza, CantidadReservada,Cantidad,
                      CantidadSF,ubicacion_picking,Area, Factor, IdUbicacion, IdTramo, IdBodega  "

            vSQL += "ORDER BY CODIGO, Nombre_Completo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Get_Reporte_Stock_By_Propietario(ByVal pIdPropietario As Integer) As DataTable

        Get_Reporte_Stock_By_Propietario = Nothing

        Try

            Dim vSQL As String = "SELECT IdProducto,Codigo,
							      nombre as Producto,NomEstado as Estado,
							      IdPresentacion,sum(isnull(CantidadSF,0)) as CantidadUMBas,
							      UnidadMedida,
							      SUM(isnull(Cantidad_Presentacion,0)) as CantidadPresentacion,
                                  Presentacion,
							      SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							      SUM(isnull(Cantidad_Reservada_Pres,0)) AS Cantidad_Reservada_Pres,							      
                                  SUM(isnull(Disponible_UMBas,0))  as Disponible_UMBas, 
							      SUM(isnull(Disponible_Presentacion,0)) AS Disponible_Presentación,
							      Peso,Lote,lic_plate as Licencia,Fecha_Ingreso,
                                  Fecha_Vence, Nombre_Completo AS [Ubicación],
                                  codigo_poliza,Numero_poliza numero_orden,ubicacion_picking, Area, Factor, IdUbicacion, 
                                  dbo.Nombre_Tramo(IdTramo, IdBodega) Tramo
							      FROM VW_Stock_Res WHERE 1 > 0 "

            If pIdPropietario <> 0 Then
                vSQL += " AND IdPropietario = @IdPropietario"
            End If

            vSQL += " Group by IdProducto,NomEstado,Fecha_Ingreso,Codigo,
					  Nombre,Presentacion,IdPresentacion,UnidadMedida,peso, 
					  Lote,fecha_vence, Nombre_Completo,lic_plate,
				      Factor,codigo_poliza,Numero_poliza, CantidadReservada,Cantidad,
                      CantidadSF,ubicacion_picking,Area, Factor, IdUbicacion, IdTramo, IdBodega  "

            vSQL += "ORDER BY CODIGO, Nombre_Completo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdPropietario <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietario", pIdPropietario)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Reporte_Stock_By_Propietario = lDataTable
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

    Public Shared Function lStock_SAP(ByRef pBeStockRes As clsBeStock_res,
                                      ByRef pBeProductoOutput As clsBeProducto,
                                      ByVal DiasVencimiento As Integer,
                                      ByVal pBeConfigEnc As clsBeI_nav_config_enc,
                                      ByVal pIdArea As Integer,
                                      ByRef lConnection As SqlConnection,
                                      ByRef ltransaction As SqlTransaction,
                                      Optional ByVal Conmutar_Umbas_A_Presentacion As Boolean = False) As List(Of clsBeStock)

        lStock_SAP = Nothing

        Try

            Dim vBeStock As New clsBeStock()
            Dim lBeStock As New List(Of clsBeStock)
            Dim IdxProductoEnMemoria As Integer = -1
            Dim vIdProductoBodega As Integer = 0

            If pBeStockRes Is Nothing Then
                '#EJC20231220
                Exit Function
            End If

            vIdProductoBodega = pBeStockRes.IdProductoBodega

            pBeProductoOutput = New clsBeProducto()
            pBeProductoOutput = lpBeProductoOutput.Find(Function(x) x.IdProductoBodega = vIdProductoBodega)

            If pBeProductoOutput Is Nothing Then

                pBeProductoOutput = New clsBeProducto()
                pBeProductoOutput = clsLnProducto.Get_Single_By_IdProductoBodega(pBeStockRes.IdProductoBodega,
                                                                                 lConnection,
                                                                                 ltransaction)

                If Not pBeProductoOutput Is Nothing Then
                    pBeProductoOutput.IdProductoBodega = pBeStockRes.IdProductoBodega
                    lpBeProductoOutput.Add(pBeProductoOutput)
                End If

            End If

            Dim BeBodega As New clsBeBodega
            BeBodega = lBeBodega.Find(Function(x) x.IdBodega = pBeConfigEnc.Idbodega)

            If BeBodega Is Nothing Then

                BeBodega = clsLnBodega.GetSingle_By_Idbodega(pBeConfigEnc.Idbodega,
                                                             lConnection,
                                                             ltransaction)

                If Not lBeBodega Is Nothing Then
                    lBeBodega.Add(BeBodega)
                End If

            End If

            '#EJC20180420: Mejora en consulta por ordenamiento lógico para picking.
            Dim vSQL As String = "SELECT stock.*,
					              bodega_ubicacion.indice_x, 
					              bodega_ubicacion.nivel, 
					              bodega_ubicacion.orientacion_pos, 
					              bodega_tramo.descripcion AS Nombre_Tramo,
                                  bodega_ubicacion.ubicacion_picking
					              FROM stock INNER JOIN
					              producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega INNER JOIN
					              bodega_ubicacion ON stock.IdUbicacion = bodega_ubicacion.IdUbicacion 
					              AND stock.IdUbicacion = bodega_ubicacion.IdUbicacion 
                                  AND stock.IdBodega = bodega_ubicacion.IdBodega
					              INNER JOIN
					              bodega_tramo ON bodega_ubicacion.IdTramo = bodega_tramo.IdTramo 
                                  AND bodega_ubicacion.IdBodega = bodega_tramo.IdBodega
                                  AND bodega_ubicacion.IdSector = bodega_tramo.IdSector "

            If pBeStockRes.Control_Ultimo_Lote Then
                vSQL += " LEFT OUTER JOIN
						 trans_re_det_lote_num ON stock.IdProductoBodega = trans_re_det_lote_num.IdProductoBodega 
						 AND stock.lote = trans_re_det_lote_num.Lote "
            End If


            vSQL += " WHERE bodega_ubicacion.activo = 1 
                      and bodega_ubicacion.bloqueada = 0
                      and producto_bodega.idproductobodega=@idproductobodega                     
					  and stock.idunidadmedida =@idunidadmedida 
					  and stock.idproductoestado=@idproductoestado "

            If Not BeBodega.Permitir_Decimales Then

                If pBeStockRes.IdPresentacion <> 0 OrElse Not pBeStockRes.Atributo_Variante_1 Is Nothing Then
                    If Not pBeStockRes.Atributo_Variante_1 Is Nothing Then
                        If pBeStockRes.Atributo_Variante_1 <> "" OrElse pBeStockRes.IdPresentacion <> 0 Then
                            vSQL += "and (stock.idpresentacion =@idpresentacion) "
                        Else
                            If (pBeConfigEnc.Explosion_Automatica OrElse pBeConfigEnc.Explosion_Automatica_Desde_Ubicacion_Picking _
                                OrElse pBeStockRes.IdPresentacion = 0) Then
                                vSQL += "and (stock.idpresentacion is null or stock.idpresentacion = 0) "
                            End If
                            If Not Conmutar_Umbas_A_Presentacion Then
                                vSQL += "and (stock.idpresentacion is null or stock.idpresentacion = 0) "
                            End If
                        End If
                    End If
                Else
                    '#EJC20220315:BYB, reservar UMBAS aunque solo tenga producto en PRES ¬¬'
                    If Not Conmutar_Umbas_A_Presentacion Then
                        '#EJC20200129:Parametrizar....
                        vSQL += "and (stock.idpresentacion is null or stock.idpresentacion =0) "
                    End If
                End If

            End If

            If DiasVencimiento <> 0 Then
                vSQL += " And DATEDIFF (DAY,GETDATE(),stock.fecha_vence) >=@DiasVencimientoCliente "
            End If

            If pBeStockRes.Lote <> "" Then
                vSQL += " And stock.lote = @Lote "
            End If

            If pBeStockRes.Fecha_vence <> New Date(1900, 1, 1) Then
                vSQL += " And stock.fecha_vence = @FechaVence "
            End If

            '#EJC20220620:Temporal, averiguar si aquí vienen los días de vencimiento del cliente.
            Dim vMsgError As String = String.Format("DIAS_VENCIMIENTO_CLI: Días: {0} IdProductoBodega: {1}", DiasVencimiento, pBeStockRes.IdProductoBodega)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)

            If Not BeBodega Is Nothing Then

                If pBeProductoOutput.Control_vencimiento Then

                    If Not BeBodega.despachar_producto_vencido Then
                        vSQL += " And stock.fecha_vence > GETDATE() "
                    End If

                End If

            End If

            '#EJC20190311_0948PM: Abastecer el pedido con IdUbicacion configurada en cliente.
            If pBeStockRes.IdUbicacionAbastecerCon = 0 Then
                '"#EJC20190312:Excluir el inventario en tránsito al momento de listar stock para reservar.
                vSQL += " AND stock.IdUbicacion NOT IN (SELECT bu.IdUbicacion
							   FROM bodega_ubicacion bu
							   WHERE (bu.ubicacion_despacho = 1) AND (bu.IdBodega = @IdBodega))"
            End If

            '#EJC20190313: Excluir el último lote despachado u obtener uno mayor
            If pBeStockRes.Control_Ultimo_Lote Then

                If IsNumeric(pBeStockRes.Ultimo_Lote) Then
                    vSQL += " And (trans_re_det_lote_num.lote_numerico >= @UltimoLote)"
                End If

            End If

            '#EJC20190311_0948PM: Abastecer el pedido con IdUbicacion configurada en cliente.
            If pBeStockRes.IdUbicacionAbastecerCon <> 0 Then
                vSQL += " and stock.idubicacion = @IdUbicacionAbastecerCon "
            End If

            '#EJC20220523: Evitar que se explosione producto desde los niveles superiores a nivel (1 PEJ.) o ubicaciones de picking.
            If Conmutar_Umbas_A_Presentacion OrElse pBeStockRes.IdPresentacion = 0 Then

                If pBeConfigEnc.Explosion_Automatica_Nivel_Max > 0 Then
                    vSQL += " and bodega_ubicacion.nivel=  " & pBeConfigEnc.Explosion_Automatica_Nivel_Max
                End If

            End If

            '#CKFK20240305 Agregué filtro por área
            If pIdArea <> 0 Then
                vSQL += " and bodega_ubicacion.IdArea = @IdArea "
            End If


            '#EJC20200204: Mejora por nuevo tipo de rotación
            Dim IdTipoRotacion As Integer = 0
            Dim vIdxTipoRotacion As Integer = 0
            vIdxTipoRotacion = lTipoRotacion.FindIndex(Function(x) x.IdProductoBodega = vIdProductoBodega)
            Dim BeTipoRotacion As New clsBeTipo_rotacion()

            If vIdxTipoRotacion = -1 Then
                IdTipoRotacion = clsLnProducto.Get_Tipo_Rotacion_By_IdProductoBodega(vIdProductoBodega, lConnection, ltransaction)
                BeTipoRotacion.IdTipoRotacion = IdTipoRotacion
                BeTipoRotacion.IdProductoBodega = vIdProductoBodega
                BeTipoRotacion.Activo = True
                lTipoRotacion.Add(BeTipoRotacion.Clone())
            Else
                IdTipoRotacion = lTipoRotacion(vIdxTipoRotacion).IdTipoRotacion
            End If

            '#EJC20200204: Modifiqué el ordenamiento (quité nombre_tramo del orden)
            Select Case IdTipoRotacion
                Case 1 'FIFO                    
                    vSQL += " ORDER BY fecha_ingreso asc, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad"
                Case 2 'LIFO
                    vSQL += " ORDER BY fecha_ingreso desc,bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad"
                Case 3 'FEFO
                    vSQL += " ORDER BY fecha_vence, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad "
                    '#EJC202004: Este me lo inventé yo por la cagada del inventario inicial en Idealsa
                    'La idea es que saque el producto por ubicaciones,antes que por reglas de rotación
                    'Este ordenamiento forza a tomar producto de las ubicaciones 
                Case 4 'UPSR (Ubicación prioritaria sobre rotación)
                    vSQL += " ORDER BY indice_x,bodega_tramo.es_rack,bodega_ubicacion.IdTramo,nivel,orientacion_pos,cantidad"
                Case Else 'Default
                    vSQL += " ORDER BY fecha_ingreso asc, bodega_ubicacion.ubicacion_picking desc, bodega_tramo.es_rack,bodega_ubicacion.IdTramo,indice_x,nivel,orientacion_pos,cantidad"

            End Select

            Using lCommand As New SqlCommand(vSQL, lConnection, ltransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdProductoBodega", pBeStockRes.IdProductoBodega)

                If Not pBeStockRes.Atributo_Variante_1 Is Nothing OrElse pBeStockRes.IdPresentacion >= 0 Then
                    lCommand.Parameters.AddWithValue("@IdPresentacion", pBeStockRes.IdPresentacion)
                End If

                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pBeStockRes.IdUnidadMedida)
                lCommand.Parameters.AddWithValue("@IdProductoEstado", pBeStockRes.IdProductoEstado)
                lCommand.Parameters.AddWithValue("@IdBodega", pBeConfigEnc.Idbodega)

                If DiasVencimiento <> 0 Then
                    lCommand.Parameters.AddWithValue("@DiasVencimientoCliente", DiasVencimiento)
                End If

                If pBeStockRes.Control_Ultimo_Lote AndAlso pBeStockRes.Ultimo_Lote <> "" Then
                    lCommand.Parameters.AddWithValue("@UltimoLote", Val(pBeStockRes.Ultimo_Lote))
                End If

                If pBeStockRes.IdUbicacionAbastecerCon <> 0 Then
                    lCommand.Parameters.AddWithValue("@IdUbicacionAbastecerCon", pBeStockRes.IdUbicacionAbastecerCon)
                End If

                If pIdArea <> 0 Then
                    lCommand.Parameters.AddWithValue("@IdArea", pIdArea)
                End If

                If pBeStockRes.Lote <> "" Then
                    lCommand.Parameters.AddWithValue("@Lote", pBeStockRes.Lote)
                End If

                If pBeStockRes.Fecha_vence <> New Date(1900, 1, 1) Then
                    lCommand.Parameters.AddWithValue("@FechaVence", pBeStockRes.Fecha_vence.Date)
                End If

                Using dr = lCommand.ExecuteReader()

                    While dr.Read()

                        vBeStock = New clsBeStock

                        With vBeStock

                            .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                            .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                            .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                            .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                            .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado")) '#CKFK 20180109 07:17 AM Agregué ésta línea para que se llene el Id del estado del producto
                            .ProductoEstado.IdEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                            .Presentacion.IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                            .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                            .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                            .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                            .IdUbicacion_anterior = IIf(IsDBNull(dr.Item("IdUbicacion_anterior")), 0, dr.Item("IdUbicacion_anterior"))
                            .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                            .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                            .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                            .IdPickingEnc = IIf(IsDBNull(dr.Item("IdPickingEnc")), 0, dr.Item("IdPickingEnc"))
                            .IdDespachoEnc = IIf(IsDBNull(dr.Item("IdDespachoEnc")), 0, dr.Item("IdDespachoEnc"))
                            .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                            .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), 0, dr.Item("lic_plate"))
                            .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                            .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                            .Fecha_Ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                            .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), New Date(1900, 1, 1), dr.Item("fecha_vence"))
                            .Uds_lic_plate = IIf(IsDBNull(dr.Item("uds_lic_plate")), 0, dr.Item("uds_lic_plate"))
                            .No_bulto = IIf(IsDBNull(dr.Item("no_bulto")), 0, dr.Item("no_bulto")) '#CKFK 20180405 Modifiqué la inicializacion cuando el campo es nulo por 0
                            .Fecha_Manufactura = IIf(IsDBNull(dr.Item("fecha_manufactura")), New Date(1900, 1, 1), dr.Item("fecha_manufactura"))
                            .Añada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                            .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                            .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                            .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                            .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                            .Activo = IIf(IsDBNull(dr.Item("activo")), False, dr.Item("activo"))
                            .Peso = IIf(IsDBNull(dr.Item("Peso")), 0.0, dr.Item("Peso"))
                            .Temperatura = IIf(IsDBNull(dr.Item("temperatura")), 0.0, dr.Item("temperatura"))
                            .UbicacionPicking = IIf(IsDBNull(dr.Item("ubicacion_picking")), False, dr.Item("ubicacion_picking"))
                            .UbicacionNivel = IIf(IsDBNull(dr.Item("nivel")), 0, dr.Item("nivel"))

                        End With

                        lBeStock.Add(vBeStock)

                    End While

                End Using

            End Using

            Return lBeStock

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    '''  #EJC20240410: Obtener existencias por lote para interface de SAP (Generar comparativo contra TOMWMS)
    ''' </summary>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    Public Shared Function Get_Reporte_Stock_For_SAP(ByVal pIdBodega As Integer) As DataTable

        Get_Reporte_Stock_For_SAP = Nothing

        Try

            Dim vSQL As String = "SELECT IdProducto,Codigo,
							      nombre as Producto,NomEstado as Estado,
							      IdPresentacion,sum(isnull(CantidadSF,0)) as CantidadUMBas,
							      UnidadMedida,
							      SUM(isnull(Cantidad_Presentacion,0)) as CantidadPresentacion,
                                  Presentacion,
							      SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							      SUM(isnull(Cantidad_Reservada_Pres,0)) AS Cantidad_Reservada_Pres,							      
                                  SUM(isnull(Disponible_UMBas,0))  as Disponible_UMBas, 
							      SUM(isnull(Disponible_Presentacion,0)) AS Disponible_Presentación,
							      Peso,Lote,lic_plate as Licencia,Fecha_Ingreso,
                                  Fecha_Vence, Nombre_Completo AS Ubicacion,
                                  codigo_poliza,Numero_poliza numero_orden,ubicacion_picking, Area, Factor, IdUbicacion, 
                                  dbo.Nombre_Tramo(IdTramo, IdBodega) Tramo, IdStock
							      FROM VW_Stock_Res WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            vSQL += " Group by IdProducto,NomEstado,Fecha_Ingreso,Codigo,
					  Nombre,Presentacion,IdPresentacion,UnidadMedida,peso, 
					  Lote,fecha_vence, Nombre_Completo,lic_plate,
				      Factor,codigo_poliza,Numero_poliza, CantidadReservada,Cantidad,
                      CantidadSF,ubicacion_picking,Area, Factor, IdUbicacion, IdTramo, IdBodega, IdStock  "

            vSQL += "ORDER BY CODIGO, Nombre_Completo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    ''' <summary>
    ''' #EJC20240525: Traslado SAP
    ''' </summary>
    ''' <param name="pBePickingUbic"></param>
    ''' <param name="pIdUbicacionVirtualDestino"></param>
    ''' <param name="BeDespachoEnc"></param>    
    ''' <param name="lConnection"></param>
    ''' <param name="lTransaction"></param>
    Public Shared Sub Actualizar_Stock_Por_Traslado_AreaWMS_To_SAP(ByRef pBePickingUbic As clsBeTrans_picking_ubic,
                                                                   ByVal pIdUbicacionVirtualDestino As Integer,
                                                                   ByVal pIdEstadoDestino As Integer,
                                                                   ByVal BeDespachoEnc As clsBeTrans_despacho_enc,
                                                                   ByRef lConnection As SqlConnection,
                                                                   ByRef lTransaction As SqlTransaction)

        Try

            Dim BeStockTransito As New clsBeStock_transito
            Dim objStockOrigen As New clsBeStock()
            Dim objStockDestino As New clsBeStock()
            Dim BeStockRec As New clsBeStock_rec()

            objStockOrigen = Get_Single_Stock_By_IdStock(pBePickingUbic.IdStock,
                                                         lConnection,
                                                         lTransaction)

            clsPublic.CopyObject(objStockOrigen,
                                 objStockDestino)

            If objStockOrigen IsNot Nothing Then

                If objStockOrigen.Presentacion.IdPresentacion <> 0 Then
                    If pBePickingUbic.IdPresentacion <> 0 Then
                        If objStockOrigen.Presentacion.EsPallet Then
                            Despacha_Pallet(objStockOrigen,
                                            pIdUbicacionVirtualDestino,
                                            lConnection,
                                            lTransaction)
                        Else
                            objStockDestino.Cantidad = Math.Round(pBePickingUbic.Cantidad_Verificada * objStockOrigen.Presentacion.Factor, 6)
                            objStockOrigen.Cantidad -= Math.Round(pBePickingUbic.Cantidad_Verificada * objStockOrigen.Presentacion.Factor, 6)
                            objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)
                        End If
                    Else
                        '#EJC20200214: Se están despachando UMBas desde un stock que tiene presentación.
                        objStockDestino.Cantidad = pBePickingUbic.Cantidad_Verificada
                        objStockOrigen.Cantidad -= pBePickingUbic.Cantidad_Verificada
                        objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)
                        objStockDestino.IdPresentacion = 0
                    End If
                Else
                    objStockDestino.Cantidad = pBePickingUbic.Cantidad_Verificada
                    objStockOrigen.Cantidad -= pBePickingUbic.Cantidad_Verificada
                    objStockOrigen.Cantidad = Math.Round(objStockOrigen.Cantidad, 6)
                End If

                objStockDestino.IdStock = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.
                objStockDestino.Peso = pBePickingUbic.Peso_verificado


                objStockDestino.IdBodega = BeDespachoEnc.IdBodega
                objStockDestino.IdUbicacion = pIdUbicacionVirtualDestino
                objStockDestino.IdUbicacion_anterior = pBePickingUbic.IdUbicacion

                objStockDestino.IdProductoEstado = pIdEstadoDestino
                objStockDestino.ProductoEstado.IdEstado = pIdEstadoDestino

                objStockDestino.IdProductoBodega = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pBePickingUbic.IdProducto,
                                                                                                                        BeDespachoEnc.IdBodega,
                                                                                                                        lConnection,
                                                                                                                        lTransaction)

                '#EJC20190709: Asociar el stock al pedido que se despacha.
                objStockDestino.IdPedidoEnc = pBePickingUbic.IdPedidoEnc
                objStockDestino.IdPickingEnc = pBePickingUbic.IdPickingEnc
                objStockDestino.IdRecepcionEnc = objStockOrigen.IdRecepcionEnc
                objStockDestino.IdRecepcionDet = objStockOrigen.IdRecepcionDet
                objStockDestino.IdDespachoEnc = BeDespachoEnc.IdDespachoEnc
                objStockDestino.IdPropietarioBodega = objStockOrigen.IdPropietarioBodega
                objStockDestino.Fecha_Ingreso = Now

                Insertar(objStockDestino,
                         lConnection,
                         lTransaction)

                objStockOrigen.Peso -= pBePickingUbic.Peso_verificado
                objStockOrigen.Peso = Math.Round(objStockOrigen.Peso, 6)

                If objStockOrigen.Cantidad = 0 Then
                    clsLnStock_parametro.Eliminar_Todos_By_IdStock(objStockOrigen.IdStock,
                                                                   lConnection,
                                                                   lTransaction)
                    Eliminar(objStockOrigen,
                             lConnection,
                             lTransaction)
                Else
                    Actualiza_Cantidad_Y_Peso(objStockOrigen,
                                              lConnection,
                                              lTransaction)
                End If

                '#CKFK20240527 Coloqué las cantidades que se están moviendo
                objStockOrigen.Cantidad = objStockDestino.Cantidad
                objStockOrigen.Peso = objStockDestino.Peso

                clsLnTrans_movimientos.Insertar_Movimientos_Recepcion_Area_SAP(BeDespachoEnc.IdEmpresa,
                                                                               objStockOrigen.IdBodega,
                                                                               BeDespachoEnc.User_agr,
                                                                               BeStockRec,
                                                                               objStockOrigen,
                                                                               pIdUbicacionVirtualDestino,
                                                                               pIdEstadoDestino,
                                                                               lConnection,
                                                                               lTransaction)


            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function Get_Cantidad_Pickeada(ByRef pProducto As clsBeStock,
                                                 ByRef pConnection As SqlConnection,
                                                 ByRef pTransaction As SqlTransaction,
                                                 Optional ByVal ConEstado As Boolean = True,
                                                 Optional ByVal ConLote As Boolean = False,
                                                 Optional ByRef pExcluirUbicacionesPicking As Boolean = False,
                                                 Optional ByVal pIdUbicacion As Integer = 0,
                                                 Optional ByVal pIdUbicacionPicking As Integer = 0) As Double

        Get_Cantidad_Pickeada = 0

        Try

            Dim vSQL As String = " SELECT SUM(PICKEADO) AS PICKEADO, IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, IdUbicacion
                                    FROM (
                                    SELECT SUM(dbo.stock_res.cantidad) AS cantidad, SUM(ISNULL(dbo.trans_picking_ubic.cantidad_solicitada,0)) AS solicitado_picking, 
                                    SUM(ISNULL(dbo.trans_picking_ubic.cantidad_recibida,0)) AS Pickeado,
                                    stock_res.IdProductoBodega,
                                    stock_res.IdPresentacion as IdPresentacionStockRes, 
                                    trans_picking_ubic.IdPresentacion as IdPresentacionPickingUbic,
                                    stock_res.IdProductoEstado, stock_res.IdPresentacion ,
                                    stock_res.IdUnidadMedida, stock_res.IdUbicacion
                                    FROM stock_res LEFT OUTER JOIN
                                    trans_picking_ubic ON dbo.stock_res.IdStock = dbo.trans_picking_ubic.IdStock AND dbo.stock_res.IdProductoEstado = dbo.trans_picking_ubic.IdProductoEstado AND 
                                    stock_res.IdProductoBodega = dbo.trans_picking_ubic.IdProductoBodega AND dbo.stock_res.IdUbicacion = dbo.trans_picking_ubic.IdUbicacion AND dbo.stock_res.IdPicking = dbo.trans_picking_ubic.IdPickingEnc AND 
                                    stock_res.IdStockRes = dbo.trans_picking_ubic.IdStockRes LEFT OUTER JOIN
                                    producto_bodega ON dbo.stock_res.IdProductoBodega = dbo.producto_bodega.IdProductoBodega
                                    Group by stock_res.IdProductoBodega,
                                    stock_res.IdPresentacion , 
                                    trans_picking_ubic.IdPresentacion ,
                                    stock_res.IdProductoEstado, stock_res.IdPresentacion ,
                                    stock_res.IdUnidadMedida, stock_res.IdUbicacion) AS T 
                                    WHERE IdProductoBodega=@idproducto
					                and (idpresentacion is null or idpresentacion =@idpresentacion)
					                and idunidadmedida =@idunidadmedida "


            If ConEstado Then
                vSQL += " and idproductoestado=@idproductoestado "
            End If

            If ConLote Then
                vSQL += " and lote=@lote "
            End If

            If pIdUbicacionPicking <> 0 Then
                vSQL += " and IdUbicacion=@IdUbicacionPicking "
            End If

            If pProducto.IsReportStockEnFecha Then
                vSQL += " AND fecha_vence = @fecha_vence "
            End If

            If pExcluirUbicacionesPicking Then

                If pIdUbicacion = 0 Then

                    '#EJC20190311_0948PM: Excluir lo que esté en ubicaciones de tránsito.
                    vSQL += " and idubicacion NOT IN (SELECT IdUbicacion
							                        FROM  bodega_ubicacion AS bodega_ubicacion 
								                    WHERE (ubicacion_picking = 1 and IdBodega=@IdBodega))"

                End If

            End If

            If pIdUbicacion <> 0 Then
                '#EJC20190311_0948PM: Buscar stock por ubicación específica.
                vSQL += " and idubicacion =@IdUbicacion"
            End If

            vSQL += " group by IdProductoBodega, IdProductoEstado, IdPresentacion, IdUnidadMedida, IdUbicacion "

            '#EJC202309120602: ANALIZAR ESTO CON CAROLINA
            'vSQL += " and stock_res.estado='UNCOMMITED'"

            Using lCommand As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdProducto", pProducto.IdProductoBodega)

                If Not pProducto.Presentacion Is Nothing Then

                    If pProducto.Presentacion.IdPresentacion <> 0 Then
                        lCommand.Parameters.AddWithValue("@IdPresentacion", pProducto.Presentacion.IdPresentacion)
                    Else
                        lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                    End If
                Else
                    lCommand.Parameters.AddWithValue("@IdPresentacion", 0)
                End If

                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pProducto.IdUnidadMedida)

                If ConEstado Then
                    lCommand.Parameters.AddWithValue("@IdProductoEstado", pProducto.ProductoEstado.IdEstado)
                End If

                If ConLote Then
                    lCommand.Parameters.AddWithValue("@Lote", pProducto.Lote)
                End If

                If pIdUbicacionPicking <> 0 Then
                    lCommand.Parameters.AddWithValue("@IdUbicacionPicking", pIdUbicacionPicking)
                End If

                If pProducto.IsReportStockEnFecha Then
                    lCommand.Parameters.AddWithValue("@fecha_vence", pProducto.Fecha_vence)
                End If

                If pIdUbicacion <> 0 Then
                    lCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                End If

                If pProducto.IdBodega <> 0 Then
                    lCommand.Parameters.AddWithValue("@IdBodega", pProducto.IdBodega)
                End If

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Get_Cantidad_Pickeada = lReturnValue
                End If

                Dim lDataAdapter As New SqlDataAdapter
                lDataAdapter.SelectCommand = lCommand

                Dim lDataTable As New DataTable()
                lDataAdapter.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Return lDataTable.Rows(0).Item("PICKEADO")
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function Get_Existencia_Disp_Menos_Picking_By_IdProducto(ByRef pBeStockConsulta As clsBeStock,
    '                                                                       ByVal pIdBodega As Integer,
    '                                                                       Optional ByVal ConEstado As Boolean = True,
    '                                                                       Optional ByVal ConLote As Boolean = False,
    '                                                                       Optional ByVal pDiasVencimientoCliente As Integer = 0,
    '                                                                       Optional ByRef pExcluirUbicacionesPickign As Boolean = False,
    '                                                                       Optional ByRef pConection As SqlConnection = Nothing,
    '                                                                       Optional ByRef ptransaction As SqlTransaction = Nothing) As Boolean

    '    Get_Existencia_Disp_Menos_Picking_By_IdProducto = False

    '    Dim EsTransaccionExterna As Boolean = Not (pConection Is Nothing AndAlso ptransaction Is Nothing)

    '    Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
    '    Dim lTransaction As SqlTransaction = Nothing

    '    Dim cmd As New SqlCommand
    '    Dim vCantDis As Double = 0
    '    Dim vPesoDis As Double = 0
    '    Dim vCantRes As Double = 0
    '    Dim vPesoRes As Double = 0
    '    Dim vCantPick As Double = 0
    '    Dim vCantDispoSinUbicacionesPicking As Double = 0

    '    Try

    '        If Not EsTransaccionExterna Then
    '            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
    '        End If

    '        vCantDis = Get_Cantidad_Disponible(pBeStockConsulta,
    '                                          pIdBodega,
    '                                          IIf(EsTransaccionExterna, pConection, lConnection),
    '                                          IIf(EsTransaccionExterna, ptransaction, lTransaction),
    '                                          pDiasVencimientoCliente,
    '                                          ConEstado,
    '                                          ConLote,
    '                                          pExcluirUbicacionesPickign)

    '        vPesoDis = Get_Peso_Disponible(pBeStockConsulta,
    '                                       IIf(EsTransaccionExterna, pConection, lConnection),
    '                                       IIf(EsTransaccionExterna, ptransaction, lTransaction),
    '                                       ConEstado,
    '                                       ConLote)

    '        '#CKFK 20211228 Faltaba el parametro con lote y por eso no devolvía correctamente lo reservado
    '        vCantRes = Get_Cantidad_Reservada(pBeStockConsulta,
    '                                          IIf(EsTransaccionExterna, pConection, lConnection),
    '                                          IIf(EsTransaccionExterna, ptransaction, lTransaction),
    '                                          ConEstado,
    '                                          ConLote,
    '                                          pExcluirUbicacionesPickign)


    '        '#CKFK 20211228 Faltaba el parametro con lote y por eso no devolvía correctamente lo reservado
    '        vPesoRes = Get_Peso_Reservado(pBeStockConsulta,
    '                                      IIf(EsTransaccionExterna, pConection, lConnection),
    '                                      IIf(EsTransaccionExterna, ptransaction, lTransaction),
    '                                      ConEstado,
    '                                      ConLote)

    '        vCantPick = Get_Cantidad_Pickeada(pBeStockConsulta,
    '                                      IIf(EsTransaccionExterna, pConection, lConnection),
    '                                      IIf(EsTransaccionExterna, ptransaction, lTransaction),
    '                                      ConEstado,
    '                                      ConLote)

    '        If vCantPick > 0 Then
    '            Debug.Write("hay picking")
    '        End If
    '        pBeStockConsulta.Añada = vCantPick
    '        If pBeStockConsulta.IdPresentacion <> 0 Then
    '            If vCantRes <> 0 Then
    '                vCantRes = vCantRes / IIf(pBeStockConsulta.Presentacion.Factor > 0, pBeStockConsulta.Presentacion.Factor, 1)
    '            End If
    '        End If

    '        If vCantDis = 0 OrElse (vCantRes > vCantDis) Then
    '            pBeStockConsulta.Cantidad = 0
    '            pBeStockConsulta.Peso = 0
    '        Else
    '            pBeStockConsulta.Cantidad = vCantDis - vCantRes
    '            pBeStockConsulta.Peso = vPesoDis - vPesoRes
    '        End If

    '        If Not EsTransaccionExterna Then lTransaction.Commit()

    '        Get_Existencia_Disp_Menos_Picking_By_IdProducto = True

    '    Catch ex As Exception
    '        If Not EsTransaccionExterna Then If lTransaction IsNot Nothing Then lTransaction.Rollback()
    '        Throw ex
    '    Finally
    '        If Not EsTransaccionExterna Then
    '            If lConnection.State = ConnectionState.Open Then lConnection.Close()
    '        End If
    '        cmd.Dispose()
    '    End Try

    'End Function

    Public Shared Function Get_Existencia_Disp_Menos_Picking_By_IdProducto_By_IdUbicacion(ByRef pBeStockConsulta As clsBeStock,
                                                                                          ByRef pBeStockConsultaUbicacion As clsBeStock,
                                                                                          ByVal pIdBodega As Integer,
                                                                                          ByVal pIdUbicacion As Integer,
                                                                                          Optional ByVal ConEstado As Boolean = True,
                                                                                          Optional ByVal ConLote As Boolean = False,
                                                                                          Optional ByVal pDiasVencimientoCliente As Integer = 0,
                                                                                          Optional ByRef pExcluirUbicacionesPickign As Boolean = False,
                                                                                          Optional ByRef pConection As SqlConnection = Nothing,
                                                                                          Optional ByRef ptransaction As SqlTransaction = Nothing) As Boolean

        Get_Existencia_Disp_Menos_Picking_By_IdProducto_By_IdUbicacion = False

        Dim EsTransaccionExterna As Boolean = Not (pConection Is Nothing AndAlso ptransaction Is Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim cmd As New SqlCommand
        Dim vCantDis As Double = 0
        Dim vPesoDis As Double = 0
        Dim vCantDisUbic As Double = 0
        Dim vPesoDisUbic As Double = 0
        Dim vCantRes As Double = 0
        Dim vPesoRes As Double = 0
        Dim vCantPick As Double = 0
        Dim vCantResUbic As Double = 0
        Dim vPesoResUbic As Double = 0
        Dim vCantPickUbic As Double = 0
        Dim vCantDispoSinUbicacionesPicking As Double = 0

        Try

            If Not EsTransaccionExterna Then
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            vCantDis = Get_Cantidad_Disponible(pBeStockConsulta,
                                               pIdBodega,
                                               IIf(EsTransaccionExterna, pConection, lConnection),
                                               IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                               pDiasVencimientoCliente,
                                               ConEstado,
                                               ConLote,
                                               pExcluirUbicacionesPickign)

            vPesoDis = Get_Peso_Disponible(pBeStockConsulta,
                                           IIf(EsTransaccionExterna, pConection, lConnection),
                                           IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                           ConEstado,
                                           ConLote)

            '#CKFK 20211228 Faltaba el parametro con lote y por eso no devolvía correctamente lo reservado
            vCantRes = Get_Cantidad_Reservada(pBeStockConsulta,
                                              IIf(EsTransaccionExterna, pConection, lConnection),
                                              IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                              ConEstado,
                                              ConLote,
                                              pExcluirUbicacionesPickign)


            '#CKFK 20211228 Faltaba el parametro con lote y por eso no devolvía correctamente lo reservado
            vPesoRes = Get_Peso_Reservado(pBeStockConsulta,
                                          IIf(EsTransaccionExterna, pConection, lConnection),
                                          IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                          ConEstado,
                                          ConLote)

            vCantPick = Get_Cantidad_Pickeada(pBeStockConsulta,
                                              IIf(EsTransaccionExterna, pConection, lConnection),
                                              IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                              ConEstado,
                                              ConLote,
                                              pExcluirUbicacionesPickign)

            If vCantPick > 0 Then
                Debug.Write("hay picking")
            End If

            pBeStockConsulta.Añada = vCantPick

            If pBeStockConsulta.IdPresentacion <> 0 Then
                If vCantRes <> 0 Then
                    vCantRes = vCantRes / IIf(pBeStockConsulta.Presentacion.Factor > 0, pBeStockConsulta.Presentacion.Factor, 1)
                End If
            End If

            pBeStockConsulta.Cantidad_Reservada = vCantRes

            If vCantDis = 0 OrElse (vCantRes > vCantDis) Then
                pBeStockConsulta.Cantidad = 0
                pBeStockConsulta.Peso = 0
            Else
                pBeStockConsulta.Cantidad = vCantDis - vCantRes
                pBeStockConsulta.Peso = vPesoDis - vPesoRes
            End If

            '#CKFK20240624 Funciones nuevas para obtener lo disponible reservado y pickeado de la ubicación
            vCantDisUbic = Get_Cantidad_Disponible(pBeStockConsultaUbicacion,
                                               pIdBodega,
                                               IIf(EsTransaccionExterna, pConection, lConnection),
                                               IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                               pDiasVencimientoCliente,
                                               ConEstado,
                                               ConLote,
                                               pExcluirUbicacionesPickign,
                                               pIdUbicacion)

            vPesoDis = Get_Peso_Disponible(pBeStockConsultaUbicacion,
                                           IIf(EsTransaccionExterna, pConection, lConnection),
                                           IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                           ConEstado,
                                           ConLote,
                                           pIdUbicacion)

            vCantResUbic = Get_Cantidad_Reservada(pBeStockConsultaUbicacion,
                                                  IIf(EsTransaccionExterna, pConection, lConnection),
                                                  IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                                  ConEstado,
                                                  ConLote,
                                                  pExcluirUbicacionesPickign,
                                                  pIdUbicacion)

            vPesoResUbic = Get_Peso_Reservado(pBeStockConsultaUbicacion,
                                              IIf(EsTransaccionExterna, pConection, lConnection),
                                              IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                              ConEstado,
                                              ConLote,
                                              pIdUbicacion)

            vCantPickUbic = Get_Cantidad_Pickeada(pBeStockConsultaUbicacion,
                                                  IIf(EsTransaccionExterna, pConection, lConnection),
                                                  IIf(EsTransaccionExterna, ptransaction, lTransaction),
                                                  ConEstado,
                                                  ConLote,
                                                  0,
                                                  0,
                                                  pIdUbicacion)
            If vCantPickUbic > 0 Then
                Debug.Write("hay picking")
            End If

            pBeStockConsultaUbicacion.Añada = vCantPickUbic

            If pBeStockConsultaUbicacion.IdPresentacion <> 0 Then
                If vCantResUbic <> 0 Then
                    vCantResUbic = vCantResUbic / IIf(pBeStockConsultaUbicacion.Presentacion.Factor > 0, pBeStockConsultaUbicacion.Presentacion.Factor, 1)
                End If
            End If

            pBeStockConsultaUbicacion.Cantidad_Reservada = vCantResUbic

            If vCantDisUbic = 0 OrElse (vCantResUbic > vCantDisUbic) Then
                pBeStockConsultaUbicacion.Cantidad = 0
                pBeStockConsultaUbicacion.Peso = 0
            Else
                pBeStockConsultaUbicacion.Cantidad = vCantDisUbic - vCantResUbic
                pBeStockConsultaUbicacion.Peso = vPesoDisUbic - vPesoResUbic
            End If

            If Not EsTransaccionExterna Then lTransaction.Commit()

            Get_Existencia_Disp_Menos_Picking_By_IdProducto_By_IdUbicacion = True

        Catch ex As Exception
            If Not EsTransaccionExterna Then If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not EsTransaccionExterna Then
                If lConnection.State = ConnectionState.Open Then lConnection.Close()
            End If
            cmd.Dispose()
        End Try

    End Function

    Public Shared Function Get_All_Stock_Consolidado_DT_By_Referencia(ByVal pReferencia As String) As DataTable


        Get_All_Stock_Consolidado_DT_By_Referencia = Nothing

        Try

            Dim vSQL As String = "select Noenc as Pedido, no Código, p.nombre as Nombre, dbo.Nombre_Area(u.IdArea, s.IdBodega) Area, Sum(cantidad) Cantidad, 
                                    size as Talla, color as Color
            From stock s inner Join
                producto_bodega pb on s.IdProductoBodega = pb.IdProductoBodega inner Join
                producto p on pb.IdProducto = p.IdProducto inner Join
                i_nav_ped_traslado_det i on p.codigo = i.No inner Join
                bodega_ubicacion u on s.IdUbicacion = u.IdUbicacion And s.IdBodega = u.IdBodega
                                    Where Process_Result <>'OK' AND NoEnc = @Referencia
                                    Group by Noenc, no, dbo.Nombre_Area(u.IdArea, s.IdBodega), p.nombre, size, color  "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@Referencia", pReferencia)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Get_All_Stock_Consolidado_DT_By_Referencia = lDataTable

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

    ''' <summary>
    ''' #EJC20240820
    ''' </summary>
    ''' <param name="pIdBodega"></param>
    ''' <param name="pIdPropietarioBodega"></param>
    ''' <param name="ExcluirSinExistencia"></param>
    ''' <returns></returns>
    Public Shared Function Get_Reporte_Stock_Grafico(ByVal pIdBodega As Integer,
                                                     ByVal pIdPropietarioBodega As Integer,
                                                     ByVal ExcluirSinExistencia As Boolean) As DataTable

        Get_Reporte_Stock_Grafico = Nothing

        Try

            Dim vSQL As String = "SELECT 
                                        codigo, 
                                        Presentacion,
	                                    Fecha_Vence,
                                        SUM(CASE 
                                                WHEN Presentacion IS NULL THEN disponible_umbas 
                                                ELSE disponible_presentacion 
                                            END) AS Stock 
                                    FROM 
                                        VW_Stock_Res WHERE CONVERT(DATE,Fecha_Vence)<>'19000101' "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega"
            End If

            vSQL += "   GROUP BY 
                            codigo, 
                            Presentacion,
	                        fecha_vence  "

            If ExcluirSinExistencia Then
                vSQL += "HAVING SUM(ISNULL(Disponible_UMBas, 0)) > 0 "
            End If

            vSQL += "ORDER BY CODIGO "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        If pIdPropietarioBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Get_Reporte_Stock_Grafico_Familia_And_Clasificacion(pIdBodega As Integer, pIdPropietarioBodega As Integer, ExcluirSinExistencia As Boolean) As DataTable

        Get_Reporte_Stock_Grafico_Familia_And_Clasificacion = Nothing

        Try

            Dim vSQL As String = "SELECT 
                                    Familia,
	                                Clasificacion,
                                    SUM(CASE 
                                            WHEN Presentacion IS NULL THEN disponible_umbas 
                                            ELSE disponible_presentacion 
                                        END) AS Stock 
                                FROM 
                                    VW_Stock_Res
                                WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " AND IdPropietarioBodega = @IdPropietarioBodega"
            End If

            vSQL += "   GROUP BY 
                        Familia,
	                    clasificacion  "

            If ExcluirSinExistencia Then
                vSQL += "HAVING SUM(ISNULL(Disponible_UMBas, 0)) > 0 "
            End If

            vSQL += "ORDER BY Familia, Clasificacion "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        If pIdPropietarioBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Get_Reporte_Stock_By_IdBodega_And_IdUbicacion(ByVal pIdBodega As Integer,
                                                                         ByVal pIdUbicacion As Integer,
                                                                         ByVal ExcluirSinExistencia As Boolean) As DataTable

        Get_Reporte_Stock_By_IdBodega_And_IdUbicacion = Nothing

        Try

            Dim vSQL As String = "select v.IdProducto, v.codigo, v.nombre AS Producto, v.NomEstado AS Estado, v.IdPresentacion, 
                                 SUM(ISNULL(v.CantidadSF, 0)) AS CantidadUMBas, v.UnidadMedida, SUM(ISNULL(v.Cantidad_Presentacion, 0)) AS CantidadPresentacion, v.Presentacion, 
                                  SUM(ISNULL(v.CantidadReservada, 0)) AS Cantidad_Reservada_UMBas, SUM(ISNULL(v.Cantidad_Reservada_Pres, 0)) AS Cantidad_Reservada_Pres, 
                                  SUM(ISNULL(v.Disponible_UMBas, 0)) AS Disponible_UMBas, SUM(ISNULL(v.Disponible_Presentacion, 0)) AS Disponible_Presentación, v.peso AS Peso, v.lote as Lote, 
                                  v.lic_plate AS Licencia, v.fecha_ingreso as Fecha_Ingreso, v.fecha_vence as Fecha_Vence, v.Nombre_Completo AS Ubicación, v.Codigo_Poliza, 
                                  v.Numero_poliza AS numero_orden, v.ubicacion_picking, v.Area, v.factor, v.IdUbicacion, dbo.Nombre_Tramo(v.IdTramo, 
                                  v.IdBodega) AS Tramo,
                                       CASE WHEN v.IdPresentacion IS NULL THEN ISNULL(t.Cant_Pickeada,0)ELSE 0 END Cant_Pickeada_UMBas,
	                                   CASE WHEN v.IdPresentacion IS NULL THEN v.CantidadReservadaUmBas - ISNULL(t.Cant_Pickeada,0)ELSE 0 END Cant_No_Pickeada_UMBas,
	                                   CASE WHEN v.IdPresentacion IS NOT NULL THEN ISNULL(t.Cant_Pickeada,0)ELSE 0 END Cant_Pickeada_Presentacion,
	                                   CASE WHEN v.IdPresentacion IS NOT NULL THEN v.Cantidad_Reservada_Pres - ISNULL(t.Cant_Pickeada,0)ELSE 0 END Cant_No_Pickeada_Presentacion	   
                                from VW_Stock_Res v left outer join 
                                     (SELECT r.IdStock, sum(u.cantidad_recibida) Cant_Pickeada
	                                  FROM trans_picking_ubic u inner join
			                                trans_pe_enc e ON e.IdPedidoEnc = u.IdPedidoEnc right outer join 
		                                stock_res r ON u.IdStock = r.IdStock
		                                and r.IdPedido = u.IdPedidoEnc and r.IdPedidoDet = u.IdPedidoDet 
	                                  WHERE r.Indicador = 'PED' and e.estado NOT IN ('Despachado','Anulado')
	                                  GROUP BY r.IdStock) t ON v.IdStock = t.IdStock WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND v.IdBodega = @IdBodega"
            End If

            If pIdUbicacion <> 0 Then
                vSQL += " AND v.IdUbicacion = @IdUbicacion"
            End If

            'vSQL += " AND (Estado_Reserva NOT IN ('PICKEADO','VERIFICADO') OR ESTADO_RESERVA IS NULL) "

            vSQL += " Group by v.IdProducto,v.NomEstado,v.Fecha_Ingreso,Codigo,
					  v.Nombre,Presentacion,v.IdPresentacion,v.UnidadMedida,v.peso, 
					  v.Lote,v.fecha_vence, v.Nombre_Completo,v.lic_plate,
				      v.Factor,v.codigo_poliza,v.Numero_poliza, v.CantidadReservada,Cantidad,
                      v.CantidadSF,v.ubicacion_picking,v.Area, v.Factor, 
                      v.IdUbicacion, v.IdTramo, v.IdBodega, t.Cant_Pickeada,v.CantidadReservadaUmBas,v.Cantidad_Reservada_Pres  "

            If ExcluirSinExistencia Then
                vSQL += "HAVING SUM(ISNULL(v.Disponible_UMBas, 0)) > 0 "
            End If

            vSQL += "ORDER BY CODIGO, Nombre_Completo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Get_Reporte_Stock_By_IdBodega_And_IdUbicacion = lDataTable
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

    ''' <summary>
    '''  #EJC20240410: Obtener existencias sin lote para interface de SAP (Generar comparativo contra TOMWMS)
    ''' </summary>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    Public Shared Function Get_Reporte_Stock_For_SAP_Sin_Lote(ByVal pIdBodega As Integer) As DataTable

        Get_Reporte_Stock_For_SAP_Sin_Lote = Nothing

        Try

            Dim vSQL As String = "SELECT IdProducto,Codigo,
							      nombre as Producto,NomEstado as Estado,
							      IdPresentacion,sum(isnull(CantidadSF,0)) as CantidadUMBas,
							      UnidadMedida,
							      SUM(isnull(Cantidad_Presentacion,0)) as CantidadPresentacion,
                                  Presentacion,
							      SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							      SUM(isnull(Cantidad_Reservada_Pres,0)) AS Cantidad_Reservada_Pres,							      
                                  SUM(isnull(Disponible_UMBas,0))  as Disponible_UMBas, 
							      SUM(isnull(Disponible_Presentacion,0)) AS Disponible_Presentación,
							      Peso,Fecha_Ingreso,Nombre_Completo AS Ubicacion,
                                  codigo_poliza,Numero_poliza numero_orden,ubicacion_picking,RIGHT(CONCAT('00',IdBodega),2) Area, Factor, IdUbicacion, 
                                  dbo.Nombre_Tramo(IdTramo, IdBodega) Tramo, IdStock
							      FROM VW_Stock_Res WHERE 1 > 0 "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega"
            End If

            vSQL += " Group by IdProducto,NomEstado,Fecha_Ingreso,Codigo,
					  Nombre,Presentacion,IdPresentacion,UnidadMedida,peso, 
					  Nombre_Completo,Factor,codigo_poliza,Numero_poliza, CantidadReservada,Cantidad,
                      CantidadSF,ubicacion_picking,Area, IdUbicacion, IdTramo, IdBodega, IdStock  "

            vSQL += "ORDER BY CODIGO, Nombre_Completo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Get_Stock_Transito(ByVal IdBodega As Integer) As DataTable

        Dim lTable As New DataTable("Result")

        Try
            Dim vSQL As String = "SELECT * FROM VW_Stock_Transito "
            Dim whereClause As String = ""

            ' Condición para IdBodega
            If IdBodega > 0 Then
                If whereClause <> "" Then whereClause &= " AND "
                whereClause &= " BodegaDestino = " & IdBodega
            End If

            ' Construir el query final
            If whereClause <> "" Then
                vSQL &= " WHERE " & whereClause
            End If

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

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

    Public Shared Function Get_Single_By_Lic_Plate_Ubic(ByVal lic_plate As String,
                                                        ByVal IdUbicacion As Integer,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As clsBeStock

        Get_Single_By_Lic_Plate_Ubic = Nothing

        Try

            Dim vSQL As String = "Select * from stock where lic_plate = @lic_plate AND IdUbicacion = @IdUbicacion "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", lic_plate)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", IdUbicacion)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    Dim Obj As New clsBeStock
                    Cargar(Obj, lDataTable.Rows(0))
                    Return Obj

                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function Guardar_Stock_Ajuste_Positivo(ByVal pObjStock As clsBeStock,
                                                         ByRef lConnection As SqlConnection,
                                                         ByRef lTransaction As SqlTransaction) As Boolean

        Guardar_Stock_Ajuste_Positivo = False

        Try
            pObjStock.IdStock = 0 'EJC20260226: el IdStock se asigna en la función Insertar, por lo que se inicializa en 0 para evitar confusiones.
            Insertar(pObjStock, lConnection, lTransaction)
            Guardar_Stock_Ajuste_Positivo = True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet(ByVal IdRecepcionEnc As Integer, ByVal IdRecepcionDet As Integer) As clsBeStock

        GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet = Nothing

        Try

            Dim Obj As New clsBeStock

            Dim vSQL As String = "Select * from stock where (IdRecepcionEnc = @IdRecepcionEnc and IdRecepcionDet=@IdRecepcionDet) "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", IdRecepcionEnc)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", IdRecepcionDet)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Obj = New clsBeStock
                            Dim lRow As DataRow = lDataTable.Rows(0)
                            Cargar(Obj, lRow)
                            GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet = Obj

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

    Public Shared Function GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet(ByVal IdRecepcionEnc As Integer, ByVal IdRecepcionDet As Integer,
                                                                          Optional ByVal pConnection As SqlConnection = Nothing,
                                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeStock

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lDTA As New SqlDataAdapter
        GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet = Nothing

        Try

            Dim Obj As New clsBeStock

            Dim vSQL As String = "Select * from stock where (IdRecepcionEnc = @IdRecepcionEnc and IdRecepcionDet=@IdRecepcionDet) "

            Dim Es_Transaccion_Remota As Boolean = (Not pConnection Is Nothing AndAlso Not pTransaction Is Nothing)
            If Es_Transaccion_Remota Then
                lDTA = New SqlDataAdapter(vSQL, pConnection)
                lDTA.SelectCommand.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lDTA = New SqlDataAdapter(vSQL, lConnection)
            End If

            lDTA.SelectCommand.CommandType = CommandType.Text
            lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", IdRecepcionEnc)
            lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionDet", IdRecepcionDet)

            Dim lDataTable As New DataTable
            lDTA.Fill(lDataTable)

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                Obj = New clsBeStock
                Dim lRow As DataRow = lDataTable.Rows(0)
                Cargar(Obj, lRow)
                GetSingle_By_IdRecepcionEnc_And_IdRecepcionDet = Obj

            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Lotes_Disponibles_DT_By_IdCliente(ByVal pIdBodega As Integer, ByVal pIdCliente As Integer, Optional ByVal Edicion As Boolean = False) As DataTable

        Get_Lotes_Disponibles_DT_By_IdCliente = Nothing

        Dim lDataTable As New DataTable

        Try
            Dim vSQL As String = "SELECT DISTINCT 
                                    BODEGA AS Bodega, 
                                    codigo AS Codigo, 
                                    nombre AS Nombre, 
                                    LOTE AS Lote,     
                                    NomEstado,
                                    SUM(cantidad) AS Cantidad,
                                    ISNULL(Presentacion,unidadmedida) as UM,
                                    fecha_vence,
                                    IdProductoEstado,
                                    IdProducto
                                    FROM VW_Stock_Res
                                    WHERE 1 > 0 "

            If Not Edicion Then
                vSQL += " AND LOTE NOT IN (
                                    SELECT Lote 
                                    FROM cliente_lotes 
                                    WHERE IdCliente = @IdCliente
                                )"
            End If


            vSQL += " And IdBodega = @IdBodega
                      And lote Is Not null
                      And lote <> '' 
                      GROUP BY BODEGA,
                        codigo, 
                        nombre, 
                        LOTE,     
                        NomEstado,
                        fecha_vence,
                        IdProductoEstado,
                        IdProducto,
                        Presentacion,
                        UnidadMedida
                        ORDER BY Codigo, Lote"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        lDTA.Fill(lDataTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lDataTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Estados_Producto_En_Stock() As DataTable

        Get_Estados_Producto_En_Stock = Nothing

        Dim lDataTable As New DataTable

        Try
            Dim vSQL As String = "SELECT IdEstado, nombre FROM producto_estado
                                  WHERE IdEstado in (select IdEstado from stock)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.Fill(lDataTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()
            End Using

            Return lDataTable

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Reporte_Stock_By_IdBodega_and_IdPropietario_For_Implosion(ByVal pIdBodega As Integer,
                                                                                         ByVal pIdPropietarioBodega As Integer) As DataTable

        Get_Reporte_Stock_By_IdBodega_and_IdPropietario_For_Implosion = Nothing

        Try

            Dim vSQL As String = "SELECT Bodega,Propietario,IdProducto,Codigo,
							      nombre as Producto,NomEstado as Estado,
							      IdPresentacion,sum(isnull(CantidadSF,0)) as CantidadUMBas,
							      UnidadMedida,
							      SUM(isnull(Cantidad_Presentacion,0)) as CantidadPresentacion,
                                  Presentacion,
							      SUM(isnull(CantidadReservada,0)) as Cantidad_Reservada_UMBas, 
							      SUM(isnull(Cantidad_Reservada_Pres,0)) AS Cantidad_Reservada_Pres,							      
                                  SUM(isnull(Disponible_UMBas,0))  as Disponible_UMBas, 
							      SUM(isnull(Disponible_Presentacion,0)) AS Disponible_Presentación,
							      Peso,Lote,lic_plate as Licencia,
                                  Fecha_Ingreso,
                                  Fecha_Vence, Nombre_Completo AS [Ubicación],
                                  codigo_poliza,Numero_poliza numero_orden,ubicacion_picking, Area, Factor, IdUbicacion, 
                                  dbo.Nombre_Tramo(IdTramo, IdBodega) Tramo,IdStock
							      FROM VW_Stock_Res WHERE 1 > 0 AND IdPresentacion IS NULL "

            If pIdBodega <> 0 Then
                vSQL += " AND IdBodega = @IdBodega "
            End If

            If pIdPropietarioBodega <> 0 Then
                vSQL += " and IdPropietarioBodega= @IdPropietarioBodega "
            End If

            vSQL += " Group by Bodega,Propietario,IdProducto,NomEstado,Fecha_Ingreso,Codigo,
					  Nombre,Presentacion,IdPresentacion,UnidadMedida,peso, 
					  Lote,fecha_vence, Nombre_Completo,lic_plate,
				      Factor,codigo_poliza,Numero_poliza, CantidadReservada,Cantidad,
                      CantidadSF,ubicacion_picking,Area, Factor, IdUbicacion, IdTramo, IdBodega, IdStock "

            vSQL += "ORDER BY CODIGO, Nombre_Completo, IdStock "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If pIdBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
                        End If

                        If pIdPropietarioBodega <> 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", pIdPropietarioBodega)
                        End If

                        Dim lDataTable As New DataTable()
                        lDataAdapter.Fill(lDataTable)

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

    Public Shared Function Get_All_Stock_DT_AM(ByVal IdBodega As Integer,
                                               ByVal IdPropietarioBodega As Integer,
                                               ByVal lConnection As SqlConnection,
                                               ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Dim vSQL As String = "SELECT * FROM VW_Stock_Res WHERE IdBodega=@IdBodega and disponible_umbas > 0 AND IdPropietarioBodega = @IdPropietarioBodega AND IdStock IN (Select IdStock FROM trans_am_stock) "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPropietarioBodega", IdPropietarioBodega)

                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandType = CommandType.Text

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Return lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdUbicacion_And_LicPlate(ByVal pIdUbicacion As Integer, pLicencia As String) As List(Of clsBeStock)

        Dim lReturnList As List(Of clsBeStock) = Nothing

        Try
            Dim vSQL As String = "SELECT * FROM Stock WHERE IdUbicacion = @IdUbicacion AND lic_plate = @lic_plate"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
                        lDTA.SelectCommand.Parameters.AddWithValue("@lic_plate", pLicencia)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeStock

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            lReturnList = New List(Of clsBeStock)
                            For Each lRow As DataRow In lDataTable.Rows
                                Obj = New clsBeStock
                                Cargar(Obj, lRow)
                                lReturnList.Add(Obj)
                            Next
                        End If
                    End Using

                    lTransaction.Commit()
                End Using
            End Using

            Return lReturnList

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function Get_All_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeVW_stock_res)

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            Dim vSQL As String = "Select * from VW_Stock_Res where IdRecepcionEnc = @IdRecepcionEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc", pIdRecepcionEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim Obj As clsBeVW_stock_res

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            For Each lRow As DataRow In lDataTable.Rows

                                Obj = New clsBeVW_stock_res

                                clsLnVW_stock_res.Cargar(Obj, lRow, lConnection, lTransaction)
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

End Class