Imports System.Data.SqlClient
Imports DevExpress.Utils.Drawing.Helpers
Imports DevExpress.XtraReports.Web.ReportDesigner.DataContracts

Partial Public Class clsLnTrans_packing_enc

    Public Shared Function Inserta_Packing(ByVal pTrans_packing_enc As List(Of clsBeTrans_packing_enc), pIdResolucion As Integer) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim cnt As Integer = 0
        Dim IdPicking As Integer = 0
        Dim vMaxIdPackingEnc As Integer = 0
        Dim ListaPacking As List(Of clsBeTrans_packing_enc) = Nothing

        Try

            If Not pTrans_packing_enc Is Nothing Then

                If pTrans_packing_enc.Count > 0 Then

                    lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    vMaxIdPackingEnc = MaxID(lConnection, lTransaction) + 1

                    For Each BeTransPackingEnc As clsBeTrans_packing_enc In pTrans_packing_enc

                        '#EJCRP fix(packing-doble-llamada): Separar ramas INSERT vs UPDATE para evitar falso error
                        '"No existen ubicaciones verificadas" cuando ACEPTAR re-llama Inserta_Packing sobre un
                        'packing_enc recien creado cuyo ubic ya tiene Fecha_packing sellada. #EJC20260528
                        If BeTransPackingEnc.Idpackingenc <> 0 Then

                            cnt = Actualizar_Cantidad_Packing(BeTransPackingEnc, lConnection, lTransaction)

                            '#MECR13112025: Se agrego bitacora de packing
                            Dim vMsgUpd As String = "Se actualizó el empaque: " + BeTransPackingEnc.Idpackingenc.ToString()
                            clsLnLog_error_wms_pack.Agregar_Error(vMsgUpd,
                                                                  pIdOperador:=BeTransPackingEnc.Idoperadorbodega,
                                                                  pIdBodega:=BeTransPackingEnc.Idbodega,
                                                                  pIdPedidoEnc:=BeTransPackingEnc.IdPedidoEnc,
                                                                  pIdPickingEnc:=BeTransPackingEnc.Idpickingenc,
                                                                  pIdDespachoEnc:=BeTransPackingEnc.IdDespachoEnc,
                                                                  pIdProductoBodega:=BeTransPackingEnc.Idproductobodega,
                                                                  pIdProductoEstado:=BeTransPackingEnc.Idproductoestado,
                                                                  pIdPresentacion:=BeTransPackingEnc.Idpresentacion,
                                                                  pIdUnidadMedida:=BeTransPackingEnc.Idunidadmedida,
                                                                  pLic_Plate:=BeTransPackingEnc.Lic_plate,
                                                                  pCantidad_Bultos_Packing:=BeTransPackingEnc.Cantidad_bultos_packing,
                                                                  pUsuario_agr:=BeTransPackingEnc.Usr_mod,
                                                                  pConection:=lConnection,
                                                                  pTransaction:=lTransaction)

                        Else

                            '#EJC20260522_FIX_PACKING_VRS_VERIFICACION: La verificacion asociada se calcula por linea de packing.
                            Dim ListaPikcing As List(Of clsBeTrans_picking_ubic) = clsLnTrans_picking_ubic.Get_All_PickingUbic_For_Packing(BeTransPackingEnc,
                                                                                                                                           lConnection,
                                                                                                                                           lTransaction)

                            If ListaPikcing Is Nothing OrElse ListaPikcing.Count = 0 Then
                                Throw New Exception(String.Format("No existen ubicaciones verificadas para empacar el producto {0}, licencia {1}.",
                                                                  BeTransPackingEnc.Idproductobodega,
                                                                  BeTransPackingEnc.Lic_plate))
                            End If

                            BeTransPackingEnc.Idpackingenc = vMaxIdPackingEnc
                            Insertar(BeTransPackingEnc, lConnection, lTransaction)
                            vMaxIdPackingEnc += 1
                            cnt = cnt + 1

                            '#MECR13112025: Se agrego bitacora de packing
                            Dim vMsg As String = "Se creó el empaque: " + BeTransPackingEnc.Idpackingenc.ToString() + " por el operador: " + BeTransPackingEnc.Idoperadorbodega.ToString() +
                                " licencia: " + BeTransPackingEnc.Lic_plate

                            clsLnLog_error_wms_pack.Agregar_Error(vMsg,
                                                                  pIdOperador:=BeTransPackingEnc.Idoperadorbodega,
                                                                  pIdBodega:=BeTransPackingEnc.Idbodega,
                                                                  pIdPedidoEnc:=BeTransPackingEnc.IdPedidoEnc,
                                                                  pIdPickingEnc:=BeTransPackingEnc.Idpickingenc,
                                                                  pIdDespachoEnc:=BeTransPackingEnc.IdDespachoEnc,
                                                                  pIdProductoBodega:=BeTransPackingEnc.Idproductobodega,
                                                                  pIdProductoEstado:=BeTransPackingEnc.Idproductoestado,
                                                                  pIdPresentacion:=BeTransPackingEnc.Idpresentacion,
                                                                  pIdUnidadMedida:=BeTransPackingEnc.Idunidadmedida,
                                                                  pLic_Plate:=BeTransPackingEnc.Lic_plate,
                                                                  pCantidad_Bultos_Packing:=BeTransPackingEnc.Cantidad_bultos_packing,
                                                                  pUsuario_agr:=BeTransPackingEnc.Usr_mod,
                                                                  pConection:=lConnection,
                                                                  pTransaction:=lTransaction)

                            '#EJC20260529 fix(packing-parcial): Restaurar guard de packing parcial.
                            '#AT20250203 comentó la guarda y siempre sellaba fecha_packing=Date.Now()
                            'aunque fuera packing parcial → ubic desaparecía de PENDIENTE en la HH.
                            'Get_CantidadEmpacada YA filtra por lic_plate (el comentario estaba desactualizado).
                            'Fix: solo estampar Date.Now() si total empacado >= cantidad_verificada del ubic;
                            'para packing parcial, restaurar sentinel 1900-01-01 → ubic sigue en PENDIENTE.
                            Dim CantidadEmpacada As Double = Get_CantidadEmpacada(BeTransPackingEnc, lConnection, lTransaction)
                            For Each Picking As clsBeTrans_picking_ubic In ListaPikcing
                                If CantidadEmpacada >= Picking.Cantidad_Verificada Then
                                    '#EJC20260530 PACK_PARCIAL_GUARD: completamente empacado → sellar
                                    Picking.Fecha_packing = Date.Now()
                                    clsLnTrans_picking_ubic.Actualizar_FechaPacking(Picking, lConnection, lTransaction)
                                Else
                                    '#EJC20260530 FIX_SENTINEL_1900: parcial → resetear al sentinel 1900-01-01 (NO NULL).
                                    'La consulta real que alimenta PENDIENTE en la HH es el overload (Int,Int)
                                    'Get_All_PickingUbic_By_IdPickingEnc (clsLnTrans_picking_ubic_Partial línea 6731,
                                    'vía WS Get_All_PickingUbic_By_PickingEnc) y su WHERE YA acepta
                                    '(fecha_packing IS NULL OR fecha_packing < ''19010101''); 1900 SIEMPRE se vio como
                                    'pendiente, así que la premisa de FIX_VIEW_NULL (NULL) era falsa. Usar NULL además
                                    'reactivaba el poison de Cargar (NULL→Date.Now) y los reset de borrado
                                    '(SET ''19000101'' WHERE fecha_packing > ''19010101'') no normalizaban NULL.
                                    '1900-01-01 es la convención establecida: inmune al poison y compatible con los reset.
                                    Dim sqlSentinel As String = "UPDATE trans_picking_ubic " &
                                        " SET fecha_packing = '19000101' " &
                                        " WHERE IdPickingUbic = @IdPickingUbic"
                                    Using cmdSentinel As New SqlCommand(sqlSentinel, lConnection, lTransaction)
                                        cmdSentinel.Parameters.Add(New SqlParameter("@IdPickingUbic", Picking.IdPickingUbic))
                                        cmdSentinel.ExecuteNonQuery()
                                    End Using
                                End If
                            Next

                        End If

                    Next

                    If pIdResolucion <> 0 Then
                        Dim BeResolLp As New clsBeResolucion_lp_operador()
                        BeResolLp = clsLnResolucion_lp_operador.GetSingle(pIdResolucion, lConnection, lTransaction)

                        If Not BeResolLp Is Nothing Then
                            BeResolLp.Correlativo_Actual += 1
                            clsLnResolucion_lp_operador.Actualizar_Correlativo_Actual(BeResolLp,
                                                                                      lConnection,
                                                                                      lTransaction)
                        End If
                    End If

                    lTransaction.Commit()

                End If

            End If

            Inserta_Packing = cnt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_CantidadEmpacada(ByVal pitem As clsBeTrans_packing_enc,
                                            Optional ByVal pConection As SqlConnection = Nothing,
                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Double

        Try
            Get_CantidadEmpacada = 0

            Dim Es_Remota As Boolean = (pConection IsNot Nothing)
            Dim lConnection As SqlConnection = If(pConection, New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST")))


            If Not Es_Remota Then
                lConnection.Open()
            End If

            Dim vSQL As String = "SELECT sum(Cantidad_bultos_packing) as Cantidad_bultos_packing
                              FROM trans_packing_enc 
                              WHERE IdProductoBodega = @IdProductoBodega AND
                                IdPickingEnc=@IdPickingEnc AND
                                IdUnidadMedida=@IdUnidadMedida AND
                                lic_plate = @lic_plate AND 
                                ISNULL(IdPresentacion,0) = @IdPresentacion AND
                                (Lote = @Lote OR Lote IS NULL)  AND 
                                ISNULL(CONVERT(DATE, fecha_vence),CONVERT(DATE, '19000101')) = CONVERT(DATE, @Fecha_Vence) AND 
                                IdProductoEstado = @IdProductoEstado 
                              GROUP BY IdProductoBodega, IdProductoEstado, IdPresentacion, lote, CONVERT(DATE, fecha_vence), lic_plate"

            Using lCommand As New SqlCommand(vSQL, lConnection)
                If pTransaction IsNot Nothing Then
                    lCommand.Transaction = pTransaction
                End If

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@IdProductoBodega", pitem.Idproductobodega)
                lCommand.Parameters.AddWithValue("@Lote", pitem.Lote)
                lCommand.Parameters.AddWithValue("@Fecha_Vence", pitem.Fecha_vence)
                lCommand.Parameters.AddWithValue("@IdPresentacion", pitem.Idpresentacion)
                lCommand.Parameters.AddWithValue("@IdPickingEnc", pitem.Idpickingenc)
                lCommand.Parameters.AddWithValue("@IdUnidadMedida", pitem.Idunidadmedida)
                lCommand.Parameters.AddWithValue("@lic_plate", pitem.Lic_plate)
                lCommand.Parameters.AddWithValue("@IdProductoEstado", pitem.Idproductoestado)

                Using lReader As SqlDataReader = lCommand.ExecuteReader()
                    If lReader.HasRows AndAlso lReader.Read() Then
                        Get_CantidadEmpacada = Convert.ToDouble(lReader("Cantidad_bultos_packing"))
                    End If
                End Using
            End Using

            If Not Es_Remota Then
                lConnection.Close()
            End If

            Return Get_CantidadEmpacada

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Actualizar_Cantidad_Packing(ByVal Packing As clsBeTrans_packing_enc, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_packing_enc")
            Upd.Add("cantidad_bultos_packing", "@cantidad_bultos_packing", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("usr_mod", "@usr_mod", DataType.Parametro)
            Upd.Where("idpackingenc = @idpackingenc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@cantidad_bultos_packing", Packing.Cantidad_bultos_packing))
            cmd.Parameters.Add(New SqlParameter("@idpackingenc", Packing.Idpackingenc))
            cmd.Parameters.Add(New SqlParameter("@fec_mod", Packing.Fec_mod))
            cmd.Parameters.Add(New SqlParameter("@usr_mod", Packing.Usr_mod))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

        'Catch ex As Exception
        '       If Not lTransaction Is Nothing Then lTransaction.Rollback()
        '       Throw ex
        'Finally
        '       If lConnection.State = ConnectionState.Open Then lConnection.Close()
        '       If Not lConnection Is Nothing Then lConnection.Dispose()
        '       If Not lTransaction Is Nothing Then lTransaction.Dispose()
        'End Try

    End Function

    Public Shared Function Eliminar(ByVal pIdPackingEnc As Integer,
                                    Optional ByVal pConection As SqlConnection = Nothing,
                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from trans_packing_enc" &
             "  Where idpackingenc = @idpackingenc"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@idpackingenc", pIdPackingEnc))

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

    Public Shared Function Get_All_By_IdPicking(ByVal IdPicking As Integer,
                                                ByVal IsForAndr As Boolean,
                                                ByVal IdPedidoEnc As Integer) As List(Of clsBeTrans_packing_enc)

        Dim lReturnList As New List(Of clsBeTrans_packing_enc)

        Try
            ' #EJC20260529 FIX_CUMBRE (PUNTO 3/4) - BOF SIN JOIN A TABLA DE PRODUCTO:
            ' Get_All_By_IdPicking solo hacía SELECT a.* de Trans_packing_enc.
            ' Esa tabla guarda el ID numérico del producto (Idproductobodega) pero NUNCA
            ' el nombre ni el código — no los necesita para operar internamente.
            ' La HH compensaba buscando esos datos en pick.items (ubics pendientes),
            ' pero cuando PUNTO 1 filtró las ubics ya empacadas, si el producto estaba
            ' 100% empacado ya no existía en pick.items → Código/Nombre vacíos en pantalla.
            ' FIX: el BOF navega Trans_packing_enc → trans_picking_det → trans_pe_det para
            ' traer codigo_producto y nombre_producto que el pedido capturó al crear el
            ' picking. trans_pe_det los tiene porque son datos del detalle de pedido,
            ' inmutables una vez creado el picking. El mapeo post-Cargar() abajo los
            ' vuelca al objeto entidad para que el WS los incluya en la trama XML.
            Const sp As String = "SELECT a.*,
                                            c.Codigo Codigo_Talla,
                                            c.Nombre Nombre_Talla,
                                            d.Codigo Codigo_Color,
                                            d.Nombre Nombre_Color,
                                            ISNULL(pdet.codigo_producto,'') AS CodigoProducto,
                                            ISNULL(pdet.nombre_producto,'') AS nom_prod,
                                            ISNULL(pdet.nom_presentacion,'') AS ProductoPresentacion,
                                            ISNULL(pdet.nom_unid_med,'') AS ProductoUnidadMedida,
                                            ISNULL(est.nombre,'') AS ProductoEstado
                                            FROM Trans_packing_enc a
                                            left join producto_talla_color b On b.IdProductoTallaColor = a.IdProductoTallaColor
                                            left join talla c On c.IdTalla = b.IdTalla
                                            left join color d On d.IdColor = b.IdColor
                                                                            -- #EJC20260529 FIX_CUMBRE: navegamos hasta trans_pe_det para obtener nombre/codigo del producto
                                            left join (
                                                SELECT DISTINCT pkd.IdPickingEnc, pdt.IdProductoBodega,
                                                                pdt.codigo_producto, pdt.nombre_producto,
                                                                pdt.nom_presentacion, pdt.nom_unid_med
                                                FROM trans_picking_det pkd
                                                INNER JOIN trans_pe_det pdt ON pdt.IdPedidoDet = pkd.IdPedidoDet
                                            ) pdet ON pdet.IdPickingEnc = a.Idpickingenc AND pdet.IdProductoBodega = a.Idproductobodega
                                            left join dbo.producto_estado est ON est.IdEstado = a.Idproductoestado " &
                                            " Where (a.idpickingenc = @idpickingenc) AND (a.iddespachoenc=0) AND (a.IdPedidoEnc = @IdPedidoEnc) "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@idpickingenc", IdPicking))
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeTrans_packing_enc As New clsBeTrans_packing_enc

                        For Each dr As DataRow In lDataTable.Rows
                            vBeTrans_packing_enc = New clsBeTrans_packing_enc()
                            Cargar(vBeTrans_packing_enc, dr, IsForAndr)
                            ' #EJC20260529 FIX_CUMBRE PUNTO 3 — mapeo post-Cargar: Cargar() no conoce las
                            ' columnas nuevas del JOIN (no está en el Partial), así que las leemos aquí
                            ' directamente del DataRow con Columns.Contains para evitar crash si alguna
                            ' BD de otro cliente no tiene la vista o las tablas de pedido enlazadas.
                            If lDataTable.Columns.Contains("CodigoProducto") AndAlso Not IsDBNull(dr.Item("CodigoProducto")) Then vBeTrans_packing_enc.CodigoProducto = CStr(dr.Item("CodigoProducto"))
                            If lDataTable.Columns.Contains("nom_prod") AndAlso Not IsDBNull(dr.Item("nom_prod")) Then vBeTrans_packing_enc.nom_prod = CStr(dr.Item("nom_prod"))
                            If lDataTable.Columns.Contains("ProductoPresentacion") AndAlso Not IsDBNull(dr.Item("ProductoPresentacion")) Then vBeTrans_packing_enc.ProductoPresentacion = CStr(dr.Item("ProductoPresentacion"))
                            If lDataTable.Columns.Contains("ProductoUnidadMedida") AndAlso Not IsDBNull(dr.Item("ProductoUnidadMedida")) Then vBeTrans_packing_enc.ProductoUnidadMedida = CStr(dr.Item("ProductoUnidadMedida"))
                            If lDataTable.Columns.Contains("ProductoEstado") AndAlso Not IsDBNull(dr.Item("ProductoEstado")) Then vBeTrans_packing_enc.ProductoEstado = CStr(dr.Item("ProductoEstado"))
                            lReturnList.Add(vBeTrans_packing_enc)
                        Next

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

    Public Shared Function Get_All_By_IdPicking_DT(ByVal IdPicking As Integer,
                                                   ByVal IdPedidoEnc As Integer) As DataTable

        Get_All_By_IdPicking_DT = Nothing

        Try

            Const sp As String = "SELECT * FROM VW_Packing  " &
            " Where (idpickingenc = @idpickingenc AND idpedidoenc = @idpedidoenc)  "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@idpickingenc", IdPicking))
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@idpedidoenc", IdPedidoEnc))

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Get_All_By_IdPicking_DT = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(idpackingenc),0) FROM Trans_packing_enc"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If

            End Using

            Return lMax

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_All_By_IdPIckingEnc(ByVal IdPickingEnc As Integer,
                                                        Optional ByVal pConection As SqlConnection = Nothing,
                                                        Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from Trans_packing_enc " &
             "  Where(idpickingenc = @idpickingenc)"

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPICKINGENC", IdPickingEnc))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Tiene_Packing_By_IdPicking(ByVal IdPicking As Integer,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As Boolean

        Tiene_Packing_By_IdPicking = False

        Try

            Const sp As String = "SELECT * FROM Trans_packing_enc " &
            " Where (idpickingenc = @idpickingenc) AND (iddespachoenc=0) "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@idpickingenc", IdPicking))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Tiene_Packing_By_IdPicking = (lDataTable.Rows.Count > 0)

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_IdDespachoEnc_By_IdPicking(ByVal IdPicking As Integer,
                                                                 ByVal IdDespachoEnc As Integer,
                                                                 ByVal IdPedidoEnc As Integer,
                                                                 ByVal lConnection As SqlConnection,
                                                                 ByVal lTransaction As SqlTransaction) As Integer

        Try

            Const sp As String = "UPDATE Trans_packing_enc SET IdDespachoEnc = @IdDespachoEnc 
                                  Where (idpickingenc = @idpickingenc) AND (iddespachoenc=0) AND (IdPedidoEnc = @IdPedidoEnc)"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdDespachoEnc", IdDespachoEnc)
                lCommand.Parameters.AddWithValue("@IdPickingEnc", IdPicking)
                lCommand.Parameters.AddWithValue("@IdPedidoEnc", IdPedidoEnc)

                Dim lReturnValue As Object = lCommand.ExecuteNonQuery()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    Actualizar_IdDespachoEnc_By_IdPicking = CInt(lReturnValue)
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPicking(ByVal IdPicking As Integer,
                                                ByVal IsForAndr As Boolean,
                                                ByVal lConnection As SqlConnection,
                                                ByVal lTransaction As SqlTransaction) As List(Of clsBeTrans_packing_enc)

        Dim lReturnList As New List(Of clsBeTrans_packing_enc)

        Try

            Const sp As String = "SELECT p.*, ISNULL(c.Codigo,'') Codigo_Color, ISNULL(c.Nombre,'') Nombre_Color, 
                                         ISNULL(t.Codigo,'') Codigo_Talla, ISNULL(t.Nombre,'') Nombre_Talla
                                  FROM Trans_packing_enc p LEFT JOIN 
                                       producto_talla_color ptc ON p.IdProductoTallaColor = ptc.IdProductoTallaColor LEFT JOIN
                                           talla t ON ptc.IdTalla = t.IdTalla LEFT JOIN
                                           color c ON ptc.IdColor = c.IdColor
                                  WHERE (idpickingenc = @idpickingenc) AND (iddespachoenc=0) "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@idpickingenc", IdPicking))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeTrans_packing_enc As New clsBeTrans_packing_enc

                For Each dr As DataRow In lDataTable.Rows
                    vBeTrans_packing_enc = New clsBeTrans_packing_enc()
                    Cargar(vBeTrans_packing_enc, dr, IsForAndr)
                    lReturnList.Add(vBeTrans_packing_enc)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPicking_DT(ByVal IdPicking As Integer,
                                                   ByVal IdPedidoEnc As Integer,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_By_IdPicking_DT = Nothing

        Try

            Const sp As String = "SELECT * FROM VW_Packing  " &
            " Where (idpickingenc = @idpickingenc AND IdPedidoEnc =  @IdPedidoEnc)  "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@idpickingenc", IdPicking))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)
                Get_All_By_IdPicking_DT = lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdPicking_And_IdPedido_And_IdDespacho_DT(ByVal IdPicking As Integer,
                                                                               ByVal IdPedidoEnc As Integer,
                                                                               ByVal IdDespachoEnc As Integer,
                                                                               ByVal lConnection As SqlConnection,
                                                                               ByVal lTransaction As SqlTransaction) As DataTable

        Get_All_By_IdPicking_And_IdPedido_And_IdDespacho_DT = Nothing

        Try

            Const sp As String = "SELECT * FROM VW_Packing  " &
            " Where (idpickingenc = @idpickingenc AND IdPedidoEnc =  @IdPedidoEnc  AND IdDespachoEnc =  @IdDespachoEnc)  "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@idpickingenc", IdPicking))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdDespachoEnc", IdDespachoEnc))

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)
                Get_All_By_IdPicking_And_IdPedido_And_IdDespacho_DT = lDataTable

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Pedidos_Sin_Procesar_By_IdPicking(ByVal IdPicking As Integer,
                                                                 ByVal IsForAndr As Boolean,
                                                                 ByVal lConnection As SqlConnection,
                                                                 ByVal lTransaction As SqlTransaction) As Integer

        Dim vCantidad As Integer = 0

        Try

            Const sp As String = " SELECT count(distinct IdPedidoEnc) cant FROM Trans_packing_enc 
                                   Where (idpickingenc = @idpickingenc) AND (iddespachoenc=0) and finalizado = 0 "

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.Add(New SqlParameter("@IDPICKINGENC", IdPicking))

                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    vCantidad = CInt(lReturnValue)
                End If

            End Using

            Return vCantidad

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_Linea_Packing(ByVal pBePacking As clsBeTrans_packing_enc,
                                                  ByVal pIdOperadorBodega As Integer) As Boolean

        Eliminar_Linea_Packing = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If Eliminar(pBePacking.Idpackingenc, lConnection, lTransaction) > 0 Then

                '#EJCRP fix(packing-fecha-reset): Resetear Fecha_packing en ubics al eliminar un packing_enc para
                'que el producto pueda volver a empacar (escenario reemplazo + ACEPTAR delete+insert). #EJC20260528
                Dim cmdResetFp As New SqlCommand(
                    "UPDATE trans_picking_ubic SET Fecha_packing = '19000101' " &
                    "WHERE IdPickingEnc = @IdPickingEnc " &
                    "  AND IdProductoBodega = @IdProductoBodega " &
                    "  AND ISNULL(lic_plate,'') = ISNULL(@Lic_plate,'') " &
                    "  AND Fecha_packing > '19010101'",
                    lConnection, lTransaction)
                cmdResetFp.Parameters.AddWithValue("@IdPickingEnc", pBePacking.Idpickingenc)
                cmdResetFp.Parameters.AddWithValue("@IdProductoBodega", pBePacking.Idproductobodega)
                cmdResetFp.Parameters.AddWithValue("@Lic_plate", If(pBePacking.Lic_plate, ""))
                cmdResetFp.ExecuteNonQuery()
                cmdResetFp.Dispose()

                '#MECR12112025: Se agrego bitacora de packing
                'NoLinea es Licencia Packing
                'clsLnLog_error_wms.Agregar_Error("Se elimino la licencia: " & pBePacking.Lic_plate & " IdPickingEnc " & pBePacking.Idpickingenc &
                '                                 " IdPedido " & pBePacking.IdPedidoEnc & " de la licencia packing " & pBePacking.No_linea &
                '                                 " por el IdOperadorBodega " & pIdOperadorBodega)
                Dim vMsg As String = "Se elimino la licencia: " & pBePacking.Lic_plate & " IdPickingEnc " & pBePacking.Idpickingenc &
                    " IdPedido " & pBePacking.IdPedidoEnc & " de la licencia packing " & pBePacking.No_linea & " por el IdOperadorBodega " & pIdOperadorBodega

                clsLnLog_error_wms_pack.Agregar_Error(vMsg,
                                                      pIdOperador:=pIdOperadorBodega,
                                                      pIdBodega:=pBePacking.Idbodega,
                                                      pIdPedidoEnc:=pBePacking.IdPedidoEnc,
                                                      pIdPickingEnc:=pBePacking.Idpickingenc,
                                                      pIdDespachoEnc:=pBePacking.IdDespachoEnc,
                                                      pIdProductoBodega:=pBePacking.Idproductobodega,
                                                      pIdProductoEstado:=pBePacking.Idproductoestado,
                                                      pIdPresentacion:=pBePacking.Idpresentacion,
                                                      pIdUnidadMedida:=pBePacking.Idunidadmedida,
                                                      pLic_Plate:=pBePacking.Lic_plate,
                                                      pCantidad_Bultos_Packing:=pBePacking.Cantidad_bultos_packing,
                                                      pUsuario_agr:=pBePacking.Usr_mod,
                                                      pConection:=lConnection,
                                                      pTransaction:=lTransaction)

                Eliminar_Linea_Packing = True
            End If

            lTransaction.Commit()

            Return Eliminar_Linea_Packing

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try
    End Function

    Public Shared Function Get_LicenciasPacking_Cerrado(ByVal IdPedidoEnc As Integer) As List(Of clsBeTrans_packing_enc)

        Get_LicenciasPacking_Cerrado = Nothing
        Dim resultado As New List(Of clsBeTrans_packing_enc)

        Try

            Const sp As String = "SELECT DISTINCT k.no_linea, p.bodega_destino referencia
                                  FROM trans_packing_enc k INNER JOIN trans_pe_enc p ON k.IdPedidoEnc = p.IdPedidoEnc
                                  WHERE (k.IdPedidoEnc = @IdPedidoEnc AND k.finalizado = 1 ) "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)
                        Dim BePacking As New clsBeTrans_packing_enc

                        For Each dr As DataRow In lDataTable.Rows
                            BePacking = New clsBeTrans_packing_enc()
                            BePacking.No_linea = IIf(IsDBNull(dr.Item("no_linea")), "", dr.Item("no_linea"))
                            BePacking.Referencia = IIf(IsDBNull(dr.Item("referencia")), "", dr.Item("referencia"))
                            resultado.Add(BePacking)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return resultado

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKFK20250323 Agregué esta función para marcar la línea como no empacada
    Public Shared Sub Marcar_Linea_No_Empacada(ByVal pBePickingUbic As clsBeTrans_picking_ubic,
                                               ByVal Usuario As Integer,
                                               Optional ByVal pConnection As SqlConnection = Nothing,
                                               Optional ByVal pTransaction As SqlTransaction = Nothing)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from trans_packing_enc 
                                   Where IdPedidoEnc = @IdPedidoEnc and 
                                         IdPickingEnc =@IdPickingEnc and 
                                         lic_plate = @Lic_Plate and 
                                         IdProductoBodega = @IdProductoBodega and 
                                         IdDespachoEnc = 0 "

            Dim Es_Transaccion_Remota As Boolean = (pConnection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConnection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdPedidoEnc", pBePickingUbic.IdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@IdPickingEnc", pBePickingUbic.IdPickingEnc))
            cmd.Parameters.Add(New SqlParameter("@Lic_Plate", pBePickingUbic.Lic_plate))
            cmd.Parameters.Add(New SqlParameter("@IdProductoBodega", pBePickingUbic.IdProductoBodega))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            '#EJCRP fix(packing-fecha-reset): Resetear Fecha_packing en ubics al cancelar empaque (reemplazo/des-verificacion)
            'para que el producto pueda volver al flujo de empaque correctamente. #EJC20260528
            Dim resetSql As String = "UPDATE trans_picking_ubic SET Fecha_packing = '19000101' " &
                                     "WHERE IdPickingEnc = @IdPickingEnc " &
                                     "  AND IdProductoBodega = @IdProductoBodega " &
                                     "  AND ISNULL(lic_plate,'') = ISNULL(@Lic_plate,'') " &
                                     "  AND Fecha_packing > '19010101'"
            Dim cmdReset As SqlCommand
            If Es_Transaccion_Remota Then
                cmdReset = New SqlCommand(resetSql, pConnection, pTransaction)
            Else
                cmdReset = New SqlCommand(resetSql, lConnection, lTransaction)
            End If
            cmdReset.Parameters.AddWithValue("@IdPickingEnc", pBePickingUbic.IdPickingEnc)
            cmdReset.Parameters.AddWithValue("@IdProductoBodega", pBePickingUbic.IdProductoBodega)
            cmdReset.Parameters.AddWithValue("@Lic_plate", If(pBePickingUbic.Lic_plate, ""))
            cmdReset.ExecuteNonQuery()
            cmdReset.Dispose()

            Dim sp1 As String = " Update trans_packing_enc set Finalizado = 0
                                  Where IdPedidoEnc = @IdPedidoEnc and 
                                         IdPickingEnc =@IdPickingEnc and
                                         IdDespachoEnc = 0 "

            Dim cmd1 As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd1 = New SqlCommand(sp1, pConnection, pTransaction)
            Else
                cmd1 = New SqlCommand(sp1, lConnection, lTransaction)
            End If

            cmd1.Parameters.Add(New SqlParameter("@IdPedidoEnc", pBePickingUbic.IdPedidoEnc))
            cmd1.Parameters.Add(New SqlParameter("@IdPickingEnc", pBePickingUbic.IdPickingEnc))
            cmd1.ExecuteNonQuery()
            cmd1.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Sub

    Public Shared Function Actualizar_Estado_Packing(ByVal pIdPedidoEnc As Integer,
                                                     ByVal pEstado As Boolean,
                                                     ByVal pUsuario As Integer,
                                                     Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("trans_packing_enc")
            Upd.Add("finalizado", "@finalizado", DataType.Parametro)
            Upd.Add("fec_mod", "@fec_mod", DataType.Parametro)
            Upd.Add("usr_mod", "@usr_mod", DataType.Parametro)
            Upd.Where("idpedidoenc = @idpedidoenc")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@finalizado", pEstado))
            cmd.Parameters.Add(New SqlParameter("@idpedidoenc", pIdPedidoEnc))
            cmd.Parameters.Add(New SqlParameter("@fec_mod", pUsuario))
            cmd.Parameters.Add(New SqlParameter("@usr_mod", Now))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
