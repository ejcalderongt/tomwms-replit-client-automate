Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnVW_Stock_Res_Pedido

    Public Shared Sub Cargar(ByRef oBeVW_Stock_Res_Pedido As clsBeVW_Stock_Res_Pedido, ByRef dr As DataRow)
        Try
            With oBeVW_Stock_Res_Pedido
                .Codigo = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                .Nombre = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                .Presentacion = IIf(IsDBNull(dr.Item("presentacion")), "", dr.Item("presentacion"))
                .NomEstado = IIf(IsDBNull(dr.Item("NomEstado")), "", dr.Item("NomEstado"))
                .Unidadmedida = IIf(IsDBNull(dr.Item("unidadmedida")), "", dr.Item("unidadmedida"))
                .Propietario = IIf(IsDBNull(dr.Item("propietario")), "", dr.Item("propietario"))
                .Bodegaubicacion = IIf(IsDBNull(dr.Item("bodegaubicacion")), "", dr.Item("bodegaubicacion"))
                .Cantidadfisica = IIf(IsDBNull(dr.Item("cantidadfisica")), 0.0, dr.Item("cantidadfisica"))
                .Factor = IIf(IsDBNull(dr.Item("factor")), 0.0, dr.Item("factor"))
                .IdStockRes = IIf(IsDBNull(dr.Item("IdStockRes")), 0, dr.Item("IdStockRes"))
                .IdTransaccion = IIf(IsDBNull(dr.Item("IdTransaccion")), 0, dr.Item("IdTransaccion"))
                .Indicador = IIf(IsDBNull(dr.Item("Indicador")), "", dr.Item("Indicador"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                .Fecha_ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                .Fecha_vence = IIf(IsDBNull(dr.Item("fecha_vence")), Date.Now, dr.Item("fecha_vence"))
                .Uds_lic_plate = IIf(IsDBNull(dr.Item("uds_lic_plate")), 0.0, dr.Item("uds_lic_plate"))
                .Ubicacion_ant = IIf(IsDBNull(dr.Item("ubicacion_ant")), "", dr.Item("ubicacion_ant"))
                .No_bulto = IIf(IsDBNull(dr.Item("no_bulto")), 0, dr.Item("no_bulto"))
                .IdRecepcion = IIf(IsDBNull(dr.Item("IdRecepcion")), 0, dr.Item("IdRecepcion"))
                .IdPicking = IIf(IsDBNull(dr.Item("IdPicking")), 0, dr.Item("IdPicking"))
                .IdPedido = IIf(IsDBNull(dr.Item("IdPedido")), 0, dr.Item("IdPedido"))
                .IdDespacho = IIf(IsDBNull(dr.Item("IdDespacho")), 0, dr.Item("IdDespacho"))
                .User_agr = IIf(IsDBNull(dr.Item("user_agr")), "", dr.Item("user_agr"))
                .Fec_agr = IIf(IsDBNull(dr.Item("fec_agr")), Date.Now, dr.Item("fec_agr"))
                .User_mod = IIf(IsDBNull(dr.Item("user_mod")), "", dr.Item("user_mod"))
                .Fec_mod = IIf(IsDBNull(dr.Item("fec_mod")), Date.Now, dr.Item("fec_mod"))
                .Host = IIf(IsDBNull(dr.Item("host")), "", dr.Item("host"))
                .Añada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                .Fecha_manufactura = IIf(IsDBNull(dr.Item("fecha_manufactura")), Date.Now, dr.Item("fecha_manufactura"))
                .Referencia = IIf(IsDBNull(dr.Item("referencia")), "", dr.Item("referencia"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function GetAll() As List(Of clsBeVW_Stock_Res_Pedido)

        Dim lTable As New DataTable("Result")

        Try

            Dim lReturnList As New List(Of clsBeVW_Stock_Res_Pedido)

            Dim vSQL As String = "SELECT * FROM VW_Stock_Res_Pedido"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.Fill(lTable)

                    Dim vBeTemp_VW_Stock_Res_Pedido As New clsBeVW_Stock_Res_Pedido

                    For Each dr As DataRow In lTable.Rows
                        vBeTemp_VW_Stock_Res_Pedido = New clsBeVW_Stock_Res_Pedido
                        Cargar(vBeTemp_VW_Stock_Res_Pedido, dr)
                        lReturnList.Add(vBeTemp_VW_Stock_Res_Pedido)
                    Next

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByBodega(ByVal IdBodega As Integer) As List(Of clsBeVW_Stock_Res_Pedido)

        Dim lTable As New DataTable("Result")

        Try

            Dim lReturnList As New List(Of clsBeVW_Stock_Res_Pedido)

            Dim vSQL As String = "SELECT * FROM VW_Stock_Res_Pedido WHERE IdBodega = @IdBodega "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                    lDataAdapter.SelectCommand.CommandType = CommandType.Text
                    lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                    lDataAdapter.Fill(lTable)

                    Dim vBeTemp_VW_Stock_Res_Pedido As New clsBeVW_Stock_Res_Pedido

                    For Each dr As DataRow In lTable.Rows
                        vBeTemp_VW_Stock_Res_Pedido = New clsBeVW_Stock_Res_Pedido
                        Cargar(vBeTemp_VW_Stock_Res_Pedido, dr)
                        lReturnList.Add(vBeTemp_VW_Stock_Res_Pedido)
                    Next

                End Using

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_DT(ByVal IdBodega As Integer, Optional ByVal IdTran As Integer = 0) As DataTable

        Get_All_By_IdBodega_DT = Nothing

        Try

            Dim lTable As New DataTable("Result")

            '#EJC20250724: Agregué bodega destino (SAP) y Cliente
            '#EJC20211221: Ordenar por fecha descendente para ver las reservas mas recientes.
            'GT06012022: Se agrega opcional IdTran para mostrar stock reservado en tareas de ubicacion dirigida
            'GT25012022: Se agrega dbo.Nombre_Completo_Ubicacion en lugar de la descripcion
            Dim vSQL As String = "
                                SELECT 
                                    sr.IdStockRes, 
                                    sr.IdStock, 
                                    sr.IdTransaccion, 
                                    sr.codigo AS Código,
                                    sr.nombre AS Producto,
                                    sr.unidadmedida AS UMBas, 
                                    sr.presentacion AS Presentación,
                                    sr.NomEstado AS EstadoProducto,
                                    sr.Lote,
                                    sr.fecha_vence AS FechaVence,
                                    CASE 
                                        WHEN sr.factor > 0 THEN sr.cantidad / sr.factor 
                                        ELSE 0 
                                    END AS cantidad_presentacion_reservada,
                                    sr.factor AS Factor,
                                    sr.referencia AS Ref_Pedido,
                                    sr.IdPedido,
                                    sr.cantidad AS cantidad_umbas_reservada,
                                    sr.cantidadfisica AS CantidadFisica,
                                    sr.peso AS Peso,
                                    sr.propietario AS Propietario,
                                    dbo.Nombre_Area(sr.IdArea, sr.IdBodega) AS Area,
                                    dbo.Nombre_Completo_Ubicacion(sr.IdUbicacion, sr.IdBodega) AS UbicaciónActual,
                                    dbo.Nombre_Completo_Ubicacion(sr.ubicacion_ant, sr.IdBodega) AS UbicaciónAnterior,
                                    sr.Lic_Plate AS Licencia,
                                    sr.Serial,
                                    sr.Indicador,
                                    sr.fecha_ingreso AS FechaIngreso,
                                    sr.Uds_lic_plate,
                                    sr.fecha_manufactura AS FechaManofactura,
                                    sr.estado AS EstadoRes,
                                    sr.Host,
                                    sr.IdPicking, 
                                    sr.IdProductoBodega,
                                    sr.fec_agr,
                                    sr.Columna,
                                    sr.Nivel,
                                    sr.Tramo,
                                    sr.Estructura,
                                    sr.Color,
                                    sr.Talla,
                                    pe.bodega_destino AS Bodega_Destino,
                                    cli.nombre_comercial AS Cliente
                                FROM VW_Stock_Res_Pedido sr                                
                                LEFT JOIN trans_pe_enc pe 
                                    ON sr.Indicador = 'PED' AND sr.IdTransaccion = pe.IdPedidoEnc
                                LEFT JOIN cliente cli 
                                    ON pe.IdCliente = cli.IdCliente
                                WHERE sr.IdBodega = @IdBodega
                                "



            If IdTran > 0 Then
                vSQL += " AND IdTransaccion=@IdTran"
            End If

            vSQL += " ORDER BY fec_agr DESC"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()


                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If IdTran > 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdTran", IdTran)
                        End If

                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDataAdapter.Fill(lTable)

                        Return lTable

                    End Using


                    lTransaction.Commit()

                End Using

                lConnection.Close()


            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdBodega_DT_QA(ByVal IdBodega As Integer, Optional ByVal IdTran As Integer = 0) As DataTable

        Get_All_By_IdBodega_DT_QA = Nothing

        Try

            Dim lTable As New DataTable("Result")

            Dim vSQL As String = "SELECT codigo as Código,fecha_vence as FechaVence,
                                  case when factor > 0 then
                                    cantidad / factor
                                  else
                                    0           
                                  end
					              as  cantidad_presentacion_reservada,
					              cantidad as cantidad_umbas_reservada,
                                  dbo.Nombre_Completo_Ubicacion(IdUbicacion,IdBodega) as UbicaciónActual,
                                  unidadmedida as UMBas, 
                                  presentacion as Presentación, 
                                  nombre as Producto,
                                  Lote,
                                  factor as Factor,
                                  referencia as Ref_Pedido,
                                  IdPedido,
                                  NomEstado as EstadoProducto,
                                  cantidadfisica as CantidadFisica,
                                  peso as Peso,propietario as Propietario,
                                  dbo.Nombre_Area(IdArea, IdBodega) as Area,
                                  dbo.Nombre_Completo_Ubicacion(ubicacion_ant, IdBodega) as UbicaciónAnterior,
                                  Lic_Plate as Licencia,Serial,Indicador,fecha_ingreso as FechaIngreso,Uds_lic_plate,fecha_manufactura FechaManofactura,
                                  estado as EstadoRes,Host,IdPicking, IdProductoBodega,fec_agr,
                                  Columna,Nivel,Tramo, Estructura,
                                  IdStockRes, IdStock, IdTransaccion
		                          From VW_Stock_Res_Pedido  WHERE IdBodega = @IdBodega  "


            If IdTran > 0 Then
                vSQL += " AND IdTransaccion=@IdTran"
            End If

            vSQL += " ORDER BY IdStockRes ASC"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()


                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        If IdTran > 0 Then
                            lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdTran", IdTran)
                        End If

                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                        lDataAdapter.Fill(lTable)

                        Return lTable

                    End Using


                    lTransaction.Commit()

                End Using

                lConnection.Close()


            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeTemp_VW_Stock_Res_Pedido As clsBeVW_Stock_Res_Pedido)

        Try

            Const sp As String = "SELECT * FROM VW_Stock_Res_Pedido WHERE IdStockRes=@IDSTOCKRES"

            Dim lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKRES", pBeTemp_VW_Stock_Res_Pedido.IdStockRes))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTemp_VW_Stock_Res_Pedido, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdStock_DT(ByVal IdBodega As Integer, ByVal IdStock As Integer,
                                                                        ByVal lConnection As SqlConnection,
                                                                        ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Dim lTable As New DataTable("Result")

            'GT06012022: parametro IdStock para obtener el IdstockRes necesario para el proceso de liberación desde CambioUbicación
            Const sp As String = "SELECT IdStockRes, IdStock, IdTransaccion, 
                                codigo as Código,nombre as Producto,unidadmedida as UMBas, presentacion as Presentación,
                                NomEstado as EstadoProducto,Lote,fecha_vence as FechaVence,
                                case when factor > 0 then
                                    cantidad / factor
                                else
                                    0           
                                end
					                as  cantidad_presentacion_reservada,
                                factor as Factor,
                                referencia as Ref_Pedido,
                                IdPedido,
					            cantidad as cantidad_umbas_reservada,
                                cantidadfisica as CantidadFisica,
                                peso as Peso,propietario as Propietario,IdUbicacion as UbicaciónActual,ubicacion_ant as UbicaciónAnterior,
                                Lic_Plate,Serial,Indicador,fecha_ingreso as FechaIngreso,Uds_lic_plate,fecha_manufactura FechaManofactura,
                                estado as EstadoRes,Host,IdPicking, IdProductoBodega,fec_agr 
		                        From VW_Stock_Res_Pedido  WHERE IdBodega = @IdBodega AND IdStock =@IdStock 
                                ORDER BY fec_agr DESC"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdStock", IdStock)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
            dad.Fill(lTable)

            Return lTable

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#CKFK20231008 Creé esta función para obtener el stock resevado de un pedido
    Public Shared Function Get_StockRes_By_IdPedidoEnc_DT(ByVal pIdPedidoEnc As Integer) As DataTable

        Get_StockRes_By_IdPedidoEnc_DT = Nothing

        Try

            Dim lTable As New DataTable("Result")

            Dim vSQL As String = "SELECT IdStockRes, IdStock, IdTransaccion, 
                                  codigo as Código,nombre as Producto,unidadmedida as UMBas, 
                                  presentacion as Presentación,
                                  NomEstado as EstadoProducto,Lote,fecha_vence as FechaVence,
                                  case when factor > 0 then
                                    cantidad / factor
                                  else
                                    0           
                                  end
					              as  cantidad_presentacion_reservada,
                                  factor as Factor,
                                  referencia as Ref_Pedido,
                                  IdPedido,
					              cantidad as cantidad_umbas_reservada,
                                  cantidadfisica as CantidadFisica,
                                  peso as Peso,propietario as Propietario,
                                  dbo.Nombre_Area(IdArea, IdBodega) as Area,
                                  dbo.Nombre_Completo_Ubicacion(IdUbicacion,IdBodega) as UbicaciónActual,
                                  dbo.Nombre_Completo_Ubicacion(ubicacion_ant, IdBodega) as UbicaciónAnterior,
                                  Lic_Plate as Licencia,Serial,Indicador,fecha_ingreso as FechaIngreso,Uds_lic_plate,fecha_manufactura FechaManofactura,
                                  estado as EstadoRes,Host,IdPicking, IdProductoBodega,fec_agr,
                                  Columna,Nivel,Tramo, Estructura
		                          From VW_Stock_Res_Pedido  WHERE IdTransaccion=@IdTran "

            vSQL += " ORDER BY fec_agr DESC"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()


                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text

                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@IdTran", pIdPedidoEnc)
                        lDataAdapter.Fill(lTable)

                        Return lTable

                    End Using


                    lTransaction.Commit()

                End Using

                lConnection.Close()


            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


End Class
