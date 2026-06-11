Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Reflection
Partial Public Class clsLnI_nav_ped_traslado_enc

    Private Shared lProductoBodegaInMemory As New List(Of clsBeProducto_bodega)
    Private Shared lBeConfigInMemory As New List(Of clsBeI_nav_config_enc)

    Private Shared Function Limpiar_Motivo_No_Reserva(ByVal pTexto As String) As String

        If String.IsNullOrWhiteSpace(pTexto) Then Return ""

        Dim vTexto As String = pTexto.Replace(vbCr, " ").Replace(vbLf, " ").Trim()

        If String.Equals(vTexto, "Ok", StringComparison.OrdinalIgnoreCase) Then Return ""
        If String.Equals(vTexto, "No se pudo completar la reserva.", StringComparison.OrdinalIgnoreCase) OrElse
           String.Equals(vTexto, "No se pudo completar la reserva", StringComparison.OrdinalIgnoreCase) Then Return ""
        If vTexto.IndexOf("No se pudo completar la reserva, consulte log_error_wms", StringComparison.OrdinalIgnoreCase) >= 0 Then Return ""

        Return vTexto

    End Function

    Private Shared Function Normalizar_Texto_Tipo_No_Reserva(ByVal pTexto As String) As String

        If String.IsNullOrWhiteSpace(pTexto) Then Return ""

        Return pTexto.Replace(vbCr, " ").
                      Replace(vbLf, " ").
                      Replace(vbTab, " ").
                      Trim().
                      ToUpperInvariant().
                      Replace("Á", "A").
                      Replace("É", "E").
                      Replace("Í", "I").
                      Replace("Ó", "O").
                      Replace("Ú", "U")

    End Function

    Private Shared Function Clasificar_Motivo_No_Reserva(ByVal pMotivo As String) As String

        Dim vTexto As String = Normalizar_Texto_Tipo_No_Reserva(pMotivo)

        If vTexto.Contains("TALLA") OrElse
           vTexto.Contains("COLOR") OrElse
           vTexto.Contains("IDPRODUCTOTALLACOLOR") Then
            Return "TALLA_COLOR_NO_APLICA"
        End If

        If vTexto.Contains("UBICACION OBLIGATORIA") OrElse
           vTexto.Contains("UBICACION ABASTECER") OrElse
           vTexto.Contains("IDUBICACIONABASTECERCON") OrElse
           vTexto.Contains("SIN STOCK APLICABLE EN UBICACION") Then
            Return "UBICACION_CLIENTE_OBLIGATORIA"
        End If

        If vTexto.Contains("EXPLOSION_AUTOMATICA_NIVEL") OrElse
           vTexto.Contains("NIVEL PARA LA EXPLOSION") OrElse
           vTexto.Contains("CONDICION DE NIVEL") Then
            Return "EXPLOSION_NIVEL_NO_APLICA"
        End If

        If vTexto.Contains("NO SE PUEDE EXPLOSIONAR") AndAlso
           (vTexto.Contains("NO PICKING") OrElse
            vTexto.Contains("ALM") OrElse
            vTexto.Contains("ALMACENAMIENTO") OrElse
            vTexto.Contains("RACK")) Then
            Return "SOLO_NO_PICKING_SIN_EXPLOSION"
        End If

        If vTexto.Contains("FEFO") OrElse
           vTexto.Contains("FECHA MINIMA") OrElse
           vTexto.Contains("FECHAMINIMA") OrElse
           vTexto.Contains("VENCE") OrElse
           vTexto.Contains("VENCIMIENTO") Then

            If (vTexto.Contains("ZONA PICKING") OrElse vTexto.Contains("ZONAPICKING")) AndAlso
               (vTexto.Contains("ZONA ALM") OrElse vTexto.Contains("ZONAALM") OrElse vTexto.Contains("ALM")) Then
                Return "FEFO_BLOQUEA_PICKING"
            End If

            Return "SIN_VENCIMIENTO_VALIDO"
        End If

        If vTexto.Contains("PRESENTACION") OrElse
           vTexto.Contains("PRES=") OrElse
           vTexto.Contains("PRES ") Then
            Return "PRESENTACION_NO_APLICA"
        End If

        If vTexto.Contains("RESERVADO") OrElse
           vTexto.Contains("RESERVA VIGENTE") OrElse
           vTexto.Contains("RESERVAS VIGENTES") Then
            Return "RESERVADO_POR_OTROS"
        End If

        If vTexto.Contains("LISTA NO TIENE REGISTROS") OrElse
           vTexto.Contains("NO SE OBTUVO NINGUN REGISTRO") OrElse
           vTexto.Contains("NO HAY EXISTENCIA") OrElse
           vTexto.Contains("EXISTENCIA DISPONIBLE") OrElse
           vTexto.Contains("SIN STOCK") Then
            Return "SIN_STOCK_APLICABLE"
        End If

        Return "RESERVA_NO_COMPLETADA"

    End Function

    Private Shared Function Tipificar_Motivo_No_Reserva(ByVal pMotivo As String) As String

        Dim vMotivo As String = Limpiar_Motivo_No_Reserva(pMotivo)
        If String.IsNullOrWhiteSpace(vMotivo) Then Return ""

        If vMotivo.IndexOf("TIPO_NO_RESERVA=", StringComparison.OrdinalIgnoreCase) >= 0 Then
            Return vMotivo
        End If

        Return "TIPO_NO_RESERVA=" & Clasificar_Motivo_No_Reserva(vMotivo) & " | " & vMotivo

    End Function

    Private Shared Sub Asegurar_Cliente_Bodega_Activo(ByVal pIdCliente As Integer,
                                                      ByVal pIdBodega As Integer,
                                                      ByVal pUser As String,
                                                      ByVal pConnection As SqlConnection,
                                                      ByVal pTransaction As SqlTransaction)

        If pIdCliente <= 0 OrElse pIdBodega <= 0 Then Exit Sub

        Dim vClienteBodega As clsBeCliente_bodega = clsLnCliente_bodega.GetSingle(pIdCliente,
                                                                                   pIdBodega,
                                                                                   pConnection,
                                                                                   pTransaction)

        If vClienteBodega IsNot Nothing Then Exit Sub

        Dim vUser As String = If(String.IsNullOrWhiteSpace(pUser), "1", pUser.Trim())
        Dim vNow As Date = Now

        vClienteBodega = New clsBeCliente_bodega With {
            .IdClienteBodega = clsLnCliente_bodega.MaxID(pConnection, pTransaction) + 1,
            .IdCliente = pIdCliente,
            .IdBodega = pIdBodega,
            .Activo = True,
            .User_agr = vUser,
            .User_mod = vUser,
            .Fec_agr = vNow,
            .Fec_mod = vNow
        }

        clsLnCliente_bodega.Insertar_From_Interface(vClienteBodega, pConnection, pTransaction)

    End Sub

    Private Shared Function Obtener_Motivo_No_Reserva(ByVal pBeTrasladoDet As clsBeI_nav_ped_traslado_det,
                                                      ByVal pMotivoDefecto As String) As String

        Dim vMotivo As String = ""

        If pBeTrasladoDet IsNot Nothing Then
            vMotivo = Limpiar_Motivo_No_Reserva(pBeTrasladoDet.Process_Result)
        End If

        If String.IsNullOrWhiteSpace(vMotivo) Then
            vMotivo = pMotivoDefecto
        End If

        Return Tipificar_Motivo_No_Reserva(vMotivo)

    End Function

    Private Shared Function Formatear_Process_Result_No_Reserva(ByVal pMotivo As String) As String

        Dim vMotivo As String = Limpiar_Motivo_No_Reserva(pMotivo)

        If String.IsNullOrWhiteSpace(vMotivo) Then
            Return "No se pudo completar la reserva."
        End If

        vMotivo = Tipificar_Motivo_No_Reserva(vMotivo)

        If vMotivo.IndexOf("No se pudo completar la reserva", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
           vMotivo.IndexOf("Reserva fallida", StringComparison.OrdinalIgnoreCase) >= 0 Then
            Return vMotivo
        End If

        Return "No se pudo completar la reserva: " & vMotivo

    End Function

    Public Shared Function GetAll(ByRef lConnection As SqlConnection,
                                  ByRef lTrans As SqlTransaction) As List(Of clsBeI_nav_ped_traslado_enc)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ped_traslado_enc)
            Const sp As String = "SELECT * FROM I_nav_ped_traslado_enc WHERE No NOT IN (SELECT referencia FROM trans_pe_enc)"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text, .Transaction = lTrans}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ped_traslado_enc As New clsBeI_nav_ped_traslado_enc

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc
                Cargar(vBeI_nav_ped_traslado_enc, dr)
                vBeI_nav_ped_traslado_enc.Lineas_Detalle = clsLnI_nav_ped_traslado_det.GetAll(lConnection, lTrans, vBeI_nav_ped_traslado_enc.No)
                lReturnList.Add(vBeI_nav_ped_traslado_enc)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll_Envios_Almacen(ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction) As List(Of clsBeI_nav_ped_traslado_enc)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ped_traslado_enc)

            Const sp As String = "SELECT * FROM I_nav_ped_traslado_enc 
                                  WHERE No NOT IN (SELECT referencia FROM trans_pe_enc)
                                        AND No LIKE 'EA-%'"

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text, .Transaction = lTransaction}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ped_traslado_enc As New clsBeI_nav_ped_traslado_enc

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc
                Cargar(vBeI_nav_ped_traslado_enc, dr)
                vBeI_nav_ped_traslado_enc.Lineas_Detalle = clsLnI_nav_ped_traslado_det.GetAll(lConnection, lTransaction, vBeI_nav_ped_traslado_enc.No)
                lReturnList.Add(vBeI_nav_ped_traslado_enc)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll_Pedidos_Transferencia(ByRef lConnection As SqlConnection,
                                                        ByRef lTrans As SqlTransaction) As List(Of clsBeI_nav_ped_traslado_enc)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ped_traslado_enc)
            Const sp As String = "SELECT * FROM I_nav_ped_traslado_enc 
                                  WHERE No NOT IN (SELECT referencia FROM trans_pe_enc)
                                        AND No LIKE 'PT-%'"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text, .Transaction = lTrans}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ped_traslado_enc As New clsBeI_nav_ped_traslado_enc

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc
                Cargar(vBeI_nav_ped_traslado_enc, dr)
                vBeI_nav_ped_traslado_enc.Lineas_Detalle = clsLnI_nav_ped_traslado_det.GetAll(lConnection, lTrans, vBeI_nav_ped_traslado_enc.No)
                lReturnList.Add(vBeI_nav_ped_traslado_enc)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAll_Pedidos_Venta(ByRef lConnection As SqlConnection,
                                                ByRef lTrans As SqlTransaction) As List(Of clsBeI_nav_ped_traslado_enc)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ped_traslado_enc)
            Const sp As String = "SELECT * FROM I_nav_ped_traslado_enc 
                                  WHERE No NOT IN (SELECT referencia FROM trans_pe_enc)
                                        AND No LIKE 'PV2-%'"
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text, .Transaction = lTrans}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ped_traslado_enc As New clsBeI_nav_ped_traslado_enc

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ped_traslado_enc = New clsBeI_nav_ped_traslado_enc
                Cargar(vBeI_nav_ped_traslado_enc, dr)
                vBeI_nav_ped_traslado_enc.Lineas_Detalle = clsLnI_nav_ped_traslado_det.GetAll(lConnection, lTrans, vBeI_nav_ped_traslado_enc.No)
                lReturnList.Add(vBeI_nav_ped_traslado_enc)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Importar_Pedido_Cliente_A_Tabla_Intermedia(ByRef PedidoCliente As clsBeI_nav_ped_traslado_enc,
                                                                      ByRef lblprg As RichTextBox) As Boolean

        Dim lConection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim CnnLog As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim IdNavConfigDet As Integer = 102 'Pedidos de clientes
        Dim BeNavEjecucionEnc As New clsBeI_nav_ejecucion_enc
        Dim IdxProductoBodegaInMemory As Integer = 0

        Try

            lConection.Open() : lTransaction = lConection.BeginTransaction()

            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim BeBodega As New clsBeBodega
            Dim vContador As Integer = 0

            CnnLog.Open()

            BeNavEjecucionEnc.IdNavConfigEnc = 1
            BeNavEjecucionEnc.Fecha = Now
            '#EJCCKFK20260520: Cambio por Identity en tabla.
            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc, CnnLog)

            Try

                If Not Exist(PedidoCliente.No, PedidoCliente.Document_Type, lConection, lTransaction) Then
                    Insertar(PedidoCliente, lConection, lTransaction)
                End If

                vContador += 1

                Application.DoEvents()

                If Not PedidoCliente.Lineas_Detalle Is Nothing Then

                    For Each BeI_Nav_PedidoTrasladoDet As clsBeI_nav_ped_traslado_det In PedidoCliente.Lineas_Detalle

                        Try

                            BeI_Nav_PedidoTrasladoDet.NoEnc = PedidoCliente.No
                            BeI_Nav_PedidoTrasladoDet.No = BeI_Nav_PedidoTrasladoDet.Item_No

                            If Not BeI_Nav_PedidoTrasladoDet.Variant_Code Is Nothing Then
                                Debug.Print("Espera")
                            End If

                            BeI_Nav_PedidoTrasladoDet.Variant_Code = BeI_Nav_PedidoTrasladoDet.Variant_Code

                            '#EJC20171106_0926AM_REF01: En pruebas este valor devolvía nothing para algunos elementos obtenidos por eso Agregué validación 
                            '(Nothing podría ser el fin de la lista, sin embargo entré el ciclo en una de las líneas del pedido, por lo que la línea del pedido
                            'puede tener información en una línea que (tal vez) no sea un producto?, más bien es un servicio. (Asumo)

                            '#EJC20171106_1023AM_REF02: El valor nothing indica el final de la vista.
                            If Not BeI_Nav_PedidoTrasladoDet.Item_No Is Nothing Then

                                '#CKFK20221108 Agregué esto para poder obtener la Bodega
                                BeBodega = New clsBeBodega
                                BeBodega = clsLnBodega.GetSingle_By_Codigo(PedidoCliente.Transfer_from_Code,
                                                                           lConection,
                                                                           lTransaction)

                                If BeBodega Is Nothing Then
                                    Throw New Exception("La bodega: " & PedidoCliente.Transfer_from_Code & " no existe.")
                                End If

                                BeProductoBodega = New clsBeProducto_bodega()
                                BeProductoBodega = clsLnProducto_bodega.Existe(BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                               BeBodega.IdBodega,
                                                                               lConection,
                                                                               lTransaction)

                                'Existe el producto en el maestro?
                                If Not BeProductoBodega Is Nothing Then

                                    If clsLnI_nav_ped_traslado_det.Exist(BeI_Nav_PedidoTrasladoDet, lConection, lTransaction) Then
                                        clsLnI_nav_ped_traslado_det.ActualizarFromIn(BeI_Nav_PedidoTrasladoDet, lConection, lTransaction)
                                    Else
                                        clsLnI_nav_ped_traslado_det.Insertar(BeI_Nav_PedidoTrasladoDet, lConection, lTransaction)
                                    End If

                                Else

                                    Try

                                        Dim vMsjgErr As String = String.Format("(M) {0} {1} ", MethodBase.GetCurrentMethod.Name(), "Producto no existe en maestro")

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(vMsjgErr,
                                                                                   BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                                   BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                   IdNavConfigDet)

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Producto no existe en maestro: {0}{1}", BeI_Nav_PedidoTrasladoDet.Item_No, vbNewLine))

                                    Catch ex As Exception
                                        Throw ex
                                    End Try

                                End If 'FIn Existe el producto en el maestro?

                            Else
                                Debug.Print("_: " & BeI_Nav_PedidoTrasladoDet.Description)
                            End If

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                         "Sin informacion",
                                                                         BeNavEjecucionEnc.IdEjecucionEnc,
                                                                         IdNavConfigDet)

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Linea desde el ws a intermedia en pedido de traslado: {0}{1}{2}", BeI_Nav_PedidoTrasladoDet.No, vbNewLine, ex.Message))

                        End Try

                    Next

                Else
                    Console.WriteLine("Pedido de compra sin lineas de detalle?")
                End If

            Catch ex As Exception

                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                  PedidoCliente.No,
                                                  BeNavEjecucionEnc.IdEjecucionEnc,
                                                  IdNavConfigDet)

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Encabezado PT desde ws a intermedia: {0}{1}{2}", PedidoCliente.No,
                                                vbNewLine,
                                                ex.Message))
            End Try

            lTransaction.Commit()

            clsPublic.Actualizar_Progreso(lblprg, "Fin de inserción en tabla intermedia.")

            Importar_Pedido_Cliente_A_Tabla_Intermedia = True

        Catch ex As Exception

            If lTransaction IsNot Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       IdNavConfigDet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("Error al insertar Ordenes Traslado desde ws a intermedia: {0}{1}", vbNewLine, ex.Message))

            Throw ex

        Finally
            If lConection.State = ConnectionState.Open Then lConection.Close()
        End Try

    End Function

    Public Shared Function Importar_Pedido_Cliente_A_Tabla_Intermedia_Bool(ByRef BePedidoCliente As clsBeI_nav_ped_traslado_enc,
                                                                           ByRef lblprg As RichTextBox,
                                                                           ByRef lConnection As SqlConnection,
                                                                           ByRef lTransaction As SqlTransaction) As Boolean

        Dim IdNavConfigDet As Integer = 102 'Pedidos de clientes
        Dim BeNavEjecucionEnc As New clsBeI_nav_ejecucion_enc
        Dim IdxProductoBodegaInMemory As Integer = 0
        Dim vContadorLineas As Integer = 0
        Dim BeConfingEnc As New clsBeI_nav_config_enc

        Importar_Pedido_Cliente_A_Tabla_Intermedia_Bool = False

        Try

            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim BeBodega As New clsBeBodega
            Dim vContador As Integer = 0

            BeNavEjecucionEnc.IdNavConfigEnc = 1
            BeNavEjecucionEnc.Fecha = Now
            '#EJCCKFK20260520: Cambio por Identity en tabla.
            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc)

            Try

                If Not BePedidoCliente.Company_Code = "" Then

                    If Not Exist_By_No_And_Company(BePedidoCliente.No, BePedidoCliente.Company_Code, BePedidoCliente.Document_Type, lConnection, lTransaction) Then
                        If BePedidoCliente.Company_Code.Length > 1 Then
                            'Si el código de la empresa es mayor a 1, se agrega el prefijo de la empresa al número del pedido.
                            BePedidoCliente.No = BePedidoCliente.Company_Code.Substring(0, 1) & BePedidoCliente.No
                        End If

                        If Not Exist(BePedidoCliente.No) Then
                            Insertar(BePedidoCliente, lConnection, lTransaction)
                        Else
                            Dim BePedidoCliente1 As New clsBeTrans_pe_enc
                            BePedidoCliente1 = clsLnTrans_pe_enc.Get_Single_By_Referencia(BePedidoCliente.No)
                            If Not BePedidoCliente1 Is Nothing Then
                                clsPublic.Actualizar_Progreso(lblprg, "El documento ya existe con IdPedido: " & BePedidoCliente1.IdPedidoEnc)
                            Else
                                If BePedidoCliente.Document_Type = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS Then
                                    Eliminar_By_NoEnc(BePedidoCliente.No)
                                    clsPublic.Actualizar_Progreso(lblprg, "El documento ya existía en la tabla intermedia, se eliminó para que reintente.")
                                Else
                                    clsPublic.Actualizar_Progreso(lblprg, "El documento ya existe en la tabla intermedia.")
                                End If
                            End If
                            Exit Function
                        End If

                    End If

                ElseIf Not Exist(BePedidoCliente.No, BePedidoCliente.Document_Type, lConnection, lTransaction) Then
                    Insertar(BePedidoCliente, lConnection, lTransaction)
                End If

                vContador += 1

                lTransaction.Save("Encabezado")

                Application.DoEvents()

                If Not BePedidoCliente.Lineas_Detalle Is Nothing Then

                    Dim vContador1 As Integer = 0
                    Dim vMostrar As Boolean = (BePedidoCliente.Lineas_Detalle.Count > 100)

                    For Each BeI_Nav_PedidoTrasladoDet As clsBeI_nav_ped_traslado_det In BePedidoCliente.Lineas_Detalle

                        If vMostrar Then clsPublic.Actualizar_Progreso(lblprg, "Procesando producto: " & BeI_Nav_PedidoTrasladoDet.Item_No)
                        clsPublic.Actualizar_Progreso(lblprg, "Procesando producto: " & BeI_Nav_PedidoTrasladoDet.Item_No)

                        Try

                            BeI_Nav_PedidoTrasladoDet.NoEnc = BePedidoCliente.No
                            BeI_Nav_PedidoTrasladoDet.No = BeI_Nav_PedidoTrasladoDet.Item_No
                            BeI_Nav_PedidoTrasladoDet.Variant_Code = BeI_Nav_PedidoTrasladoDet.Variant_Code

                            If Not BeI_Nav_PedidoTrasladoDet.Variant_Code Is Nothing Then
                                BeI_Nav_PedidoTrasladoDet.Variant_Code = BeI_Nav_PedidoTrasladoDet.Variant_Code.Replace(".", "")
                            End If

                            '#EJC20171106_1023AM_REF02: El valor nothing indica el final de la vista.
                            If Not BeI_Nav_PedidoTrasladoDet.Item_No Is Nothing Then

                                Dim BeBodegaArea As New clsBeBodega_area
                                BeBodegaArea = clsLnBodega_area.Get_Single_By_Codigo_Bodega(BePedidoCliente.Transfer_from_Code,
                                                                                            lConnection,
                                                                                            lTransaction)

                                '#CKFK20221108 Agregué esto para poder obtener la Bodega
                                BeBodega = New clsBeBodega
                                BeBodega = clsLnBodega.GetSingle_By_Codigo(BePedidoCliente.Transfer_from_Code,
                                                                           lConnection,
                                                                           lTransaction)

                                If BeBodega Is Nothing Then

                                    If Not BeBodegaArea Is Nothing Then

                                        BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeBodegaArea.IdBodega, lConnection, lTransaction)

                                        If BeBodega Is Nothing Then
                                            Throw New Exception("ERROR_20231031A: La bodega: " & BePedidoCliente.Transfer_from_Code & " no existe.")
                                        End If

                                    Else
                                        Throw New Exception("ERROR_20231031: La bodega: " & BePedidoCliente.Transfer_from_Code & " no existe.")
                                    End If

                                End If

                                BeConfingEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega(BeBodega.IdBodega, lConnection, lTransaction)

                                BeProductoBodega = New clsBeProducto_bodega()
                                BeProductoBodega = clsLnProducto_bodega.Existe(BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                               BeBodega.IdBodega,
                                                                               lConnection,
                                                                               lTransaction)
                                If BeProductoBodega Is Nothing Then
                                    If BeConfingEnc.Equiparar_Productos Then
                                        BeProductoBodega = clsLnProducto_bodega.Existe_Parte_By_IdBodega(BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                                                         BeBodega.IdBodega,
                                                                                                         lConnection,
                                                                                                         lTransaction)
                                        If BeProductoBodega Is Nothing Then
                                            BeProductoBodega = clsLnProducto_bodega.Existe_NoSerie_By_IdBodega(BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                                                               BeBodega.IdBodega,
                                                                                                               lConnection,
                                                                                                               lTransaction)
                                        End If
                                    End If
                                End If

                                If Not BeProductoBodega Is Nothing Then
                                    lProductoBodegaInMemory.Add(BeProductoBodega.Clone())
                                Else
                                    Throw New Exception("El producto: " & BeI_Nav_PedidoTrasladoDet.Item_No & " No está asociado a la bodega: " & BePedidoCliente.Transfer_from_Code & " o no existe en el maestro de materiales.")
                                End If

                                'Existe el producto en el maestro?
                                If Not BeProductoBodega Is Nothing Then

                                    'Si Cantidad Recibida es <> 0 no se importa                                    
                                    If (BeI_Nav_PedidoTrasladoDet.Qty_to_Receive = 0) Then

                                        If clsLnI_nav_ped_traslado_det.Exist(BeI_Nav_PedidoTrasladoDet, lConnection, lTransaction) Then
                                            clsLnI_nav_ped_traslado_det.ActualizarFromIn(BeI_Nav_PedidoTrasladoDet, lConnection, lTransaction)
                                        Else
                                            clsLnI_nav_ped_traslado_det.Insertar(BeI_Nav_PedidoTrasladoDet, lConnection, lTransaction)
                                        End If

                                        vContadorLineas += 1

                                    Else

                                        Try

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("Qty_to_Receive <> 0: No se importará, Qty_to_Receive debe ser 0 para procesar. ",
                                                                                       BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                       IdNavConfigDet,
)

                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Qty_to_Receive <> 0: No se importará, Qty_to_Receive debe ser 0 para procesar. : {0}{1}", BeI_Nav_PedidoTrasladoDet.Item_No, vbNewLine))

                                        Catch ex As Exception
                                            Throw ex
                                        End Try

                                    End If 'Fin Si Qty_Ro_Receive =0

                                End If 'FIn Existe el producto en el maestro?

                            Else
                                Debug.Print("_: " & BeI_Nav_PedidoTrasladoDet.Description)
                            End If

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                     "Sin informacion",
                                                                     BeNavEjecucionEnc.IdEjecucionEnc,
                                                                     IdNavConfigDet)

                            Throw ex

                        End Try

                        Application.DoEvents()

                    Next

                Else
                    Console.WriteLine("Pedido de compra sin lineas de detalle?")
                End If

            Catch ex As Exception


                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                          BePedidoCliente.No,
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          IdNavConfigDet)

                Throw ex

            End Try

            Importar_Pedido_Cliente_A_Tabla_Intermedia_Bool = (vContadorLineas > 0)

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       IdNavConfigDet)
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)

            Throw ex

        End Try

    End Function

    Public Shared Function Exist(ByVal pNo As String) As Boolean

        Exist = False

        Try

            Const sp As String = "SELECT No FROM I_nav_ped_traslado_enc Where(No = @No)"

            Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", pNo))

            Dim dt As New DataTable
            dad.Fill(dt)

            Exist = dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Eliminar_MI3(ByVal NoEnc As String) As Boolean

        Eliminar_MI3 = False

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim RegistrosAfectados As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            If clsLnI_nav_ped_traslado_det.Eliminar_By_NoEnc(NoEnc, lConnection, lTransaction) Then
                If Eliminar_By_NoEnc(NoEnc, lConnection, lTransaction) > 0 Then
                    Eliminar_MI3 = True
                End If
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0}_Enc {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lTransaction.Dispose()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar_By_NoEnc(ByVal NoEnc As String,
                                             Optional ByVal pConection As SqlConnection = Nothing,
                                             Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from i_nav_ped_traslado_enc" &
             "  Where(No = @No)"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@NO", NoEnc))


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

    Public Shared Function Importar_Pedidos_Clientes_A_Tabla_Intermedia(ByRef PedidosClientes As List(Of clsBeI_nav_ped_traslado_enc),
                                                                            ByRef Resultado As String) As Integer

        Importar_Pedidos_Clientes_A_Tabla_Intermedia = 0

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lblprg As New RichTextBox
        Dim vIdBodegaOrigen As Integer = 0
        Dim vIdPropitarioBodegaOrigen As Integer = 0
        Dim vIdPropitario As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            For Each BePedidoCliente In PedidosClientes


                'código de Bodega origen -> la que despacha el poducto.
                vIdBodegaOrigen = clsLnBodega.Get_IdBodega_By_CodigoBodega(BePedidoCliente.Transfer_from_Code,
                                                                           lConnection,
                                                                           lTransaction)

                If vIdBodegaOrigen = 0 Then
                    Throw New Exception(String.Format("El código de la bodega origen: (0) no es válido", BePedidoCliente.Transfer_from_Code))
                End If

                'código de propietario del que se despachará la mercancía.
                vIdPropitario = clsLnPropietarios.Get_IdPropietario_By_Codigo(BePedidoCliente.Product_Owner_Code,
                                                                              lConnection,
                                                                              lTransaction)


                'código del propietario asociado a la bodega ? (es válido ese propietario para esa bodega?)
                vIdPropitarioBodegaOrigen = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(vIdPropitario,
                                                                                                                          vIdBodegaOrigen,
                                                                                                                          lConnection,
                                                                                                                          lTransaction)


                If vIdPropitarioBodegaOrigen = 0 Then
                    Throw New Exception(String.Format("El código de propietario: (0) de la bodega origen: (1) no es válido", BePedidoCliente.Product_Owner_Code, BePedidoCliente.Transfer_from_Code))
                End If

                If Importar_Pedido_Cliente_A_Tabla_Intermedia_Bool(BePedidoCliente,
                                                              lblprg,
                                                              lConnection,
                                                              lTransaction) Then

                    Dim BeConfigEnc As New clsBeI_nav_config_enc With {.Idnavconfigenc = 1}

                    BeConfigEnc = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario(vIdBodegaOrigen,
                                                                                                vIdPropitario,
                                                                                                lConnection,
                                                                                                lTransaction)

                    Dim BePedidoEnc As New clsBeTrans_pe_enc
                    BePedidoEnc = Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS(BePedidoCliente,
                                                                           vIdBodegaOrigen,
                                                                           vIdPropitarioBodegaOrigen,
                                                                           BeConfigEnc,
                                                                           lConnection,
                                                                           lTransaction,
                                                                           lblprg)
                    If Not BePedidoEnc Is Nothing Then
                        clsPublic.Actualizar_Progreso(lblprg, "Pedido: " & BePedidoCliente.No & " Procesado correctamente")
                    End If

                Else
                    Resultado = lblprg.Text
                End If

            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0}_Enc {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS(ByRef BeINavPedTrasladoEnc As clsBeI_nav_ped_traslado_enc,
                                                                   ByVal IdBodegaOrigen As Integer,
                                                                   ByVal IdPropietarioBodegaOrigen As Integer,
                                                                   ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                                   ByRef lConectionInterface As SqlConnection,
                                                                   ByRef lTransInterface As SqlTransaction,
                                                                   ByVal lblprg As RichTextBox) As clsBeTrans_pe_enc

        Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS = Nothing

        Dim BeConfigDet As New clsBeI_nav_config_det
        Dim vCodigoCliente As String = ""
        Dim vIdxClienteInMemory As Integer = -1
        Dim pBePedidoEnc As clsBeTrans_pe_enc = Nothing
        Dim lLogErrorWMS As New List(Of clsBeLog_error_wms)
        Dim PedidoClienteExistente As clsBeTrans_pe_enc = Nothing
        Dim BeCliente As New clsBeCliente
        Dim vContador As Integer = 0
        Dim vContadorLineasDet As Integer = 0
        Dim pClienteTiemposList As New List(Of clsBeCliente_tiempos)
        Dim BeProducto As New clsBeProducto
        Dim BeProductoBodega As New clsBeProducto_bodega
        Dim pBePedidoDet As New clsBeTrans_pe_det
        Dim vClienteTiempo As New clsBeCliente_tiempos
        Dim vDiasVencimientoCliente As Integer = 0
        Dim BeUnidadMedida As New clsBeUnidad_medida
        Dim BePresentacion As New clsBeProducto_Presentacion
        Dim BeProductoTallaColor As New clsBeProducto_talla_color
        Dim vContador_Lineas_Detalle_Pedido_Insertadas As Integer = 0
        Dim vContador_Lineas_Detalle_Pedido_Insertadas_Tabla As Integer = 0
        Dim VContadorBitacoraTOMWMS As Integer = 0
        Dim VContadorBitacoraIntermedia As Integer = 0
        Dim IdxProducto As Integer = 0
        Dim vCodigoProducto As String = ""
        Dim IdxPresentacion As Integer = 0
        Dim BeRoadRuta As New clsBeRoad_ruta()
        Dim BeRoadVendedor As New clsBeRoad_p_vendedor()
        Dim vMsgEx3 As String = ""
        Dim IdNavConfigDet As Integer = 103
        Dim vFechaInicio As Date = Now
        Dim vPedidoExistente As Boolean = False
        Dim PedidoClienteExistenteByCompany As New clsBeTrans_pe_enc
        Dim vCantStockRes As Integer = 0
        Dim BeBodega As New clsBeBodega

        Try

            VContadorBitacoraTOMWMS = 0

            If BeINavPedTrasladoEnc.Status > 0 Then

                If Not BeConfigEnc.Interface_SAP Then
                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando Documento: {0} ", BeINavPedTrasladoEnc.No, vbNewLine))
                End If

                If BeINavPedTrasladoEnc.Lineas_Detalle.Count > 0 Then

                    BeBodega = clsLnBodega.GetSingle_By_Idbodega(IdBodegaOrigen, lConectionInterface, lTransInterface)

                    pBePedidoEnc = New clsBeTrans_pe_enc() With {.Referencia = BeINavPedTrasladoEnc.No,
                                                                 .IdTipoPedido = BeINavPedTrasladoEnc.Document_Type,
                                                                 .Codigo_Empresa_ERP = BeINavPedTrasladoEnc.Company_Code.ToString()}

                    PedidoClienteExistente = clsLnTrans_pe_enc.Get_Single_By_Referencia(pBePedidoEnc,
                                                                                        lConectionInterface,
                                                                                        lTransInterface)

                    PedidoClienteExistenteByCompany = clsLnTrans_pe_enc.Get_Single_By_Referencia_And_Company(pBePedidoEnc,
                                                                                                             lConectionInterface,
                                                                                                             lTransInterface)

                    If PedidoClienteExistente IsNot Nothing AndAlso PedidoClienteExistenteByCompany IsNot Nothing Then
                        clsPublic.Actualizar_Progreso(lblprg, "El documento ya existe para : " & PedidoClienteExistente.Codigo_Empresa_ERP & " IdPedidoWMS: " & PedidoClienteExistente.IdPedidoEnc)
                        '#CKFK20260324 Puse el pedido en nothing porque no se puedo importar
                        Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS = Nothing 'pBePedidoEnc
                        Exit Function
                    Else
                        If Not (PedidoClienteExistente Is Nothing AndAlso PedidoClienteExistenteByCompany Is Nothing) Then
                            '#EJC20240613: El correlativo de pedido que se va importar ya existe pero probablemente para la otra empresa
                            pBePedidoEnc.Referencia = BeINavPedTrasladoEnc.Company_Code.Substring(0, 1) & BeINavPedTrasladoEnc.No
                        End If
                    End If

                    vContadorLineasDet = 0

                    If BeConfigEnc.Interface_SAP AndAlso Not BeINavPedTrasladoEnc.Company_Code = "" Then
                        vCodigoCliente = BeINavPedTrasladoEnc.Company_Code.Substring(0, 1) & BeINavPedTrasladoEnc.Transfer_to_Code
                    Else
                        vCodigoCliente = BeINavPedTrasladoEnc.Transfer_to_Code
                    End If

                    BeCliente = New clsBeCliente
                    BeCliente = clsLnCliente.Get_Single_By_Codigo(vCodigoCliente,
                                                                  lConectionInterface,
                                                                  lTransInterface)


                    If BeCliente Is Nothing Then
                        BeCliente = clsLnCliente.Get_Single_By_Codigo(BeINavPedTrasladoEnc.Transfer_to_Code,
                                                                      lConectionInterface,
                                                                      lTransInterface)

                        If BeCliente Is Nothing Then
                            Throw New Exception(String.Format("{0} No existe el cliente {1} en maestro para pedido de traslado ", MethodBase.GetCurrentMethod.Name(), BeINavPedTrasladoEnc.Transfer_to_Code))
                        End If

                    End If

                    BeRoadRuta = clsLnRoad_ruta.Get_IdRuta_By_Codigo(BeINavPedTrasladoEnc.Transfer_to_CodeField,
                                                                     lConectionInterface,
                                                                     lTransInterface)

                    BeRoadVendedor = clsLnRoad_p_vendedor.Get_Vendedor_By_Codigo(BeINavPedTrasladoEnc.Transfer_from_Contact,
                                                                                 lConectionInterface,
                                                                                 lTransInterface)

                    Dim vPedidoAnulado As Boolean = False

                    If Not PedidoClienteExistente Is Nothing Then
                        If PedidoClienteExistente.Estado = "Anulado" Then
                            vPedidoAnulado = True
                        End If
                    End If

                    If Not PedidoClienteExistente Is Nothing AndAlso Not vPedidoAnulado AndAlso PedidoClienteExistenteByCompany IsNot Nothing Then
                        pBePedidoEnc.Activo = True
                        clsLnLog_error_wms.Agregar_Error("Error_202303011605: PED_EXISTENTE_WMS: " & PedidoClienteExistente.IdPedidoEnc)
                        Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS = pBePedidoEnc
                        clsPublic.Actualizar_Progreso(lblprg, "PED_EXISTENTE_WMS: " & PedidoClienteExistente.IdPedidoEnc & " no se actualizará.")
                        vPedidoExistente = True
                    Else

                        clsLnLog_error_wms.Eliminar_By_Referencia_Documento(BeINavPedTrasladoEnc.No,
                                                                            lConectionInterface,
                                                                            lTransInterface)

                        '#EJC20171107_REF13_0506AM: El MaxId del IdPedidoEnc se genera dentro del insert
                        Dim fechaBase As Date = BeINavPedTrasladoEnc.Posting_Date
                        Dim fechaFinal As Date = New DateTime(fechaBase.Year, fechaBase.Month, fechaBase.Day,
                                      Now.Hour, Now.Minute, Now.Second)

                        pBePedidoEnc.Fecha_Pedido = fechaFinal
                        'pBePedidoEnc.Fecha_Pedido = BeINavPedTrasladoEnc.Posting_Date
                        'Dim currentTime As TimeSpan = Now.TimeOfDay
                        'pBePedidoEnc.Fecha_Pedido = pBePedidoEnc.Fecha_Pedido.Add(currentTime)
                        pBePedidoEnc.Referencia = BeINavPedTrasladoEnc.No
                        pBePedidoEnc.IdBodega = IdBodegaOrigen
                        pBePedidoEnc.Cliente = New clsBeCliente
                        '#CKFK20260324: Se asigna el cliente que se obtuvo por el código de cliente
                        pBePedidoEnc.Cliente = BeCliente
                        pBePedidoEnc.IdCliente = BeCliente.IdCliente
                        pBePedidoEnc.Control_Ultimo_Lote = BeCliente.Control_Ultimo_Lote
                        pBePedidoEnc.IdMuelle = 1
                        pBePedidoEnc.PropietarioBodega = New clsBePropietario_bodega
                        pBePedidoEnc.PropietarioBodega.IdPropietarioBodega = IdPropietarioBodegaOrigen
                        pBePedidoEnc.IdPropietarioBodega = IdPropietarioBodegaOrigen
                        pBePedidoEnc.TipoPedido = New clsBeTrans_pe_tipo()

                        '#EJC20210429 Recibir el tipo de documento desde la interface.
                        If BeINavPedTrasladoEnc.Document_Type = 0 Then
                            pBePedidoEnc.TipoPedido.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
                        Else
                            pBePedidoEnc.TipoPedido.IdTipoPedido = BeINavPedTrasladoEnc.Document_Type
                        End If

                        pBePedidoEnc.Hora_ini = Now
                        pBePedidoEnc.Hora_fin = Now.AddHours(1)
                        pBePedidoEnc.HoraEntregaDesde = Now
                        pBePedidoEnc.HoraEntregaHasta = Now.AddHours(1)
                        pBePedidoEnc.Ubicacion = 1
                        pBePedidoEnc.Estado = "Nuevo"
                        pBePedidoEnc.No_despacho = 0
                        pBePedidoEnc.Activo = True
                        pBePedidoEnc.User_agr = BeConfigEnc.IdUsuario
                        pBePedidoEnc.Fec_agr = Now
                        pBePedidoEnc.User_mod = BeConfigEnc.IdUsuario
                        pBePedidoEnc.Fec_mod = Now
                        pBePedidoEnc.Local = True
                        pBePedidoEnc.Pallet_primero = True
                        pBePedidoEnc.Dias_cliente = 0
                        pBePedidoEnc.Anulado = False
                        pBePedidoEnc.IdPickingEnc = 0
                        pBePedidoEnc.RoadKilometraje = 0
                        pBePedidoEnc.RoadFechaEntr = BeINavPedTrasladoEnc.Shipment_Date
                        pBePedidoEnc.RoadDirEntrega = ""
                        pBePedidoEnc.RoadTotal = 0
                        pBePedidoEnc.RoadDesMonto = 0
                        pBePedidoEnc.RoadImpMonto = 0
                        pBePedidoEnc.RoadPeso = 0
                        pBePedidoEnc.RoadBandera = 0
                        pBePedidoEnc.RoadStatCom = ""
                        pBePedidoEnc.RoadCalcoBJ = 0
                        pBePedidoEnc.RoadImpres = 0
                        pBePedidoEnc.RoadADD1 = ""
                        pBePedidoEnc.RoadADD2 = ""
                        pBePedidoEnc.RoadADD3 = ""
                        pBePedidoEnc.RoadStatProc = 0
                        pBePedidoEnc.RoadRechazado = 0
                        pBePedidoEnc.RoadRazon_Rechazado = 0
                        pBePedidoEnc.RoadInformado = 0
                        pBePedidoEnc.RoadSucursal = ""
                        pBePedidoEnc.RoadIdDespacho = 0
                        pBePedidoEnc.RoadIdFacturacion = 0
                        pBePedidoEnc.Codigo_Empresa_ERP = BeINavPedTrasladoEnc.Company_Code

                        If Not BeRoadRuta Is Nothing Then
                            pBePedidoEnc.RoadIdRuta = BeRoadRuta.IdRuta
                        Else

                            If Not BeINavPedTrasladoEnc.RoadCodigoRuta Is Nothing Then

                                If Not BeINavPedTrasladoEnc.RoadCodigoRuta.Trim = "" Then

                                    BeRoadRuta = clsLnRoad_ruta.Get_IdRuta_By_Codigo(BeINavPedTrasladoEnc.RoadCodigoRuta,
                                                                                    lConectionInterface,
                                                                                    lTransInterface)

                                End If

                                If Not BeRoadRuta Is Nothing Then
                                    pBePedidoEnc.RoadIdRuta = BeRoadRuta.IdRuta
                                Else
                                    pBePedidoEnc.RoadIdRuta = 0
                                End If

                            End If

                        End If

                        If Not BeRoadVendedor Is Nothing Then
                            pBePedidoEnc.RoadIdVendedor = BeRoadVendedor.IdVendedor
                        Else

                            If Not BeINavPedTrasladoEnc.RoadCodigoVendedor Is Nothing Then

                                If Not BeINavPedTrasladoEnc.RoadCodigoVendedor.Trim = "" Then

                                    BeRoadVendedor = clsLnRoad_p_vendedor.Get_Vendedor_By_Codigo(BeINavPedTrasladoEnc.RoadCodigoVendedor,
                                                                                                 lConectionInterface,
                                                                                                 lTransInterface)

                                End If

                                If Not BeRoadVendedor Is Nothing Then
                                    pBePedidoEnc.RoadIdVendedor = BeRoadVendedor.IdVendedor
                                Else
                                    pBePedidoEnc.RoadIdVendedor = 0
                                End If

                            End If

                        End If

                        pBePedidoEnc.RoadIdRutaDespacho = 0
                        pBePedidoEnc.RoadIdVendedorDespacho = 0
                        pBePedidoEnc.Enviado_A_ERP = False
                        '#EJC20190711: Se utilizará para generar el pedido de ingreso ( pedido de compra) en la bodega  a la que se le envía el despacho.
                        pBePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino = BeINavPedTrasladoEnc.Receipt_Document_Reference
                        '#EJC20240328: IdTipoManufactura en pedido.
                        pBePedidoEnc.IdTipoManufactura = Val(BeINavPedTrasladoEnc.Manufacturing_Process)
                        '#EJC20240525: Mapear bodega origen
                        pBePedidoEnc.Bodega_Origen = BeINavPedTrasladoEnc.Transfer_from_Code
                        '#EJC20240605: Mapear bodega destino (tienda colonical la cumbre)
                        pBePedidoEnc.Bodega_Destino = BeINavPedTrasladoEnc.Transfer_to_CodeField
                        pBePedidoEnc.RoadDirEntrega = BeINavPedTrasladoEnc.Address
                        pBePedidoEnc.Observacion = BeINavPedTrasladoEnc.Comments
                        pBePedidoEnc.EsExportacion = BeINavPedTrasladoEnc.IsExport
                        pBePedidoEnc.Guia_Transporte = BeINavPedTrasladoEnc.Transportation_Guide

                        If BeINavPedTrasladoEnc.Transport_Company <> "" Then
                            Dim BeTransporte As New clsBeEmpresa_transporte
                            BeTransporte.Nombre = BeINavPedTrasladoEnc.Transport_Company
                            clsLnEmpresa_transporte.GetSingle_By_Nombre(BeTransporte,
                                                                        lConectionInterface,
                                                                        lTransInterface)
                            pBePedidoEnc.IdEmpresaTransporte = BeTransporte.IdEmpresaTransporte
                        End If

                        Asegurar_Cliente_Bodega_Activo(pBePedidoEnc.IdCliente,
                                                       pBePedidoEnc.IdBodega,
                                                       CStr(BeConfigEnc.IdUsuario),
                                                       lConectionInterface,
                                                       lTransInterface)

                        clsLnTrans_pe_enc.Inserta_Encabezado(pBePedidoEnc,
                                                             lConectionInterface,
                                                             lTransInterface)

                        pClienteTiemposList = clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(pBePedidoEnc.IdCliente,
                                                                                                lConectionInterface,
                                                                                                lTransInterface)


                        Dim BeINAVPedDetAnt As New clsBeI_nav_ped_traslado_det
                        Dim refBePedidoDet As New clsBeTrans_pe_det
                        Dim refBePedidoDetAnt As New clsBeTrans_pe_det
                        Dim vMostrar As Boolean = BeINavPedTrasladoEnc.Lineas_Detalle.Count > 0

                        For Each PDet In BeINavPedTrasladoEnc.Lineas_Detalle

                            vCodigoProducto = PDet.Item_No
                            BeProducto = New clsBeProducto()

                            If vMostrar Then clsPublic.Actualizar_Progreso(lblprg, "Procesando producto: " & PDet.Item_No & IIf(PDet.Color = "", "", " Color: " & PDet.Color) & IIf(PDet.Size = "", "", " Talla:" & PDet.Size))

                            If vCodigoProducto = "WMS66" Then
                                Debug.Print("7411000360002")
                            End If

                            '#CKFK20250116 Modifiqué esto para que a la hora de importar el producto solo busque por el código
                            If BeConfigEnc.Valida_Solo_Codigo Then
                                BeProducto = clsLnProducto.Get_BeProducto_By_Only_Codigo(vCodigoProducto,
                                                                                         IdBodegaOrigen,
                                                                                         lConectionInterface,
                                                                                         lTransInterface)
                            Else
                                BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(vCodigoProducto,
                                                                                    IdBodegaOrigen,
                                                                                    lConectionInterface,
                                                                                    lTransInterface)
                            End If

                            If BeProducto Is Nothing Then
                                Dim vMsgEx1 As String = "El código de producto: " & PDet.Item_No & " no existe o no está asociado con el código de bodega: " & IdBodegaOrigen
                                clsPublic.Actualizar_Progreso(lblprg, vMsgEx1)
                                Throw New Exception(vMsgEx1)
                            End If

                            '#EJC20241014:Actualizar la referencia para que reserve según la empresa.
                            PDet.Item_No = BeProducto.Codigo

                            BeUnidadMedida = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(PDet.Unit_of_Measure_Code,
                                                                                                   BeConfigEnc.IdPropietario,
                                                                                                   lConectionInterface,
                                                                                                   lTransInterface)

                            If BeUnidadMedida Is Nothing Then
                                Dim vMsgEx2 As String = "La U.M básica de producto: " & PDet.Item_No & " no existe o no está definida: " & PDet.Unit_of_Measure_Code
                                clsPublic.Actualizar_Progreso(lblprg, vMsgEx2)
                                Throw New Exception(vMsgEx2)
                            Else
                                BeProducto.UnidadMedida = BeUnidadMedida
                            End If

                            If PDet.Variant_Code IsNot Nothing AndAlso PDet.Variant_Code <> "" Then

                                BePresentacion = New clsBeProducto_Presentacion
                                BePresentacion = clsLnProducto_presentacion.Get_Presentacion_By_IdProductoBodega_And_CodPres(BeProducto.IdProductoBodega,
                                                                                                                             PDet.Variant_Code.Replace(".", ""),
                                                                                                                             lConectionInterface,
                                                                                                                             lTransInterface)

                                If BePresentacion Is Nothing Then
                                    vMsgEx3 = "La Presentacion de producto: " & PDet.Item_No & " no existe o no está definida: " & PDet.Variant_Code & " Código Killios " & BeProducto.Noparte & " Código Garesa: " & BeProducto.Noserie
                                    clsPublic.Actualizar_Progreso(lblprg, vMsgEx3)
                                    Throw New Exception(vMsgEx3)
                                End If

                            Else
                                '#EJC20190530: Estan pidiendo en UMBAS.
                                BePresentacion = Nothing
                            End If

                            If BeBodega.Control_Talla_Color Then

                                Dim BeTalla = clsLnTalla.Get_Single_By_Codigo(PDet.Size, lConectionInterface, lTransInterface)
                                If BeTalla IsNot Nothing Then
                                    BeProductoTallaColor.IdTalla = BeTalla.IdTalla
                                Else
                                    Dim vMsgEx2 As String = "La talla " & PDet.Size & " del producto: " & PDet.Item_No & " no existe, valide por favor: "
                                    clsPublic.Actualizar_Progreso(lblprg, vMsgEx2)
                                    Throw New Exception(vMsgEx2)
                                End If

                                Dim BeColor = clsLnColor.Get_Single_By_Codigo(PDet.Color, lConectionInterface, lTransInterface)
                                If BeColor IsNot Nothing Then
                                    BeProductoTallaColor.IdColor = BeColor.IdColor
                                Else
                                    Dim vMsgEx2 As String = "El color " & PDet.Color & " del producto: " & PDet.Item_No & " no existe, valide por favor: "
                                    clsPublic.Actualizar_Progreso(lblprg, vMsgEx2)
                                    Throw New Exception(vMsgEx2)
                                End If

                                BeProductoTallaColor.IdProductoTallaColor = clsLnProducto_talla_color.Get_IdProductoTallaColor_By_CodTalla_and_CodColor(BeTalla.Codigo, BeColor.Codigo, BeProducto.IdProducto, lConectionInterface, lTransInterface)

                                If BeProductoTallaColor.IdProductoTallaColor = 0 Then
                                    Dim vMsgEx2 As String = "La talla color del producto: " & PDet.Item_No & " no existe: " & PDet.Color & " - " & PDet.Size
                                    clsPublic.Actualizar_Progreso(lblprg, vMsgEx2)
                                    Throw New Exception(vMsgEx2)
                                End If
                            End If

                            vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                                                                      x.IdClasificacion = BeProducto.Clasificacion.IdClasificacion _
                                                                      AndAlso x.IdFamilia = BeProducto.Familia.IdFamilia)

                            vMsgEx3 = " El IdClasificación de producto es: " & BeProducto.Clasificacion.IdClasificacion &
                                                    " El IdFamilia es: " & BeProducto.Familia.IdFamilia

                            If Not vClienteTiempo Is Nothing Then
                                vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                            End If

                            If PedidoClienteExistente Is Nothing Then

                                Try
                                    If PDet.Item_No = "AG00023260" AndAlso PDet.Line_No = 21 Then
                                        Debug.WriteLine("espera")
                                    End If

                                    If Inserta_Linea_Detalle_Pedido(pBePedidoEnc,
                                                                    PDet,
                                                                    BeProducto,
                                                                    vDiasVencimientoCliente,
                                                                    BeUnidadMedida,
                                                                    BePresentacion,
                                                                    BeCliente,
                                                                    BeConfigEnc,
                                                                    IdBodegaOrigen,
                                                                    IdPropietarioBodegaOrigen,
                                                                    lblprg,
                                                                    lConectionInterface,
                                                                    lTransInterface,
                                                                    refBePedidoDet) Then

                                        PDet.Status = 1
                                        PDet.Process_Result = "Ok"
                                        clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet,
                                                                                          lConectionInterface,
                                                                                          lTransInterface)
                                        vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                    Else

                                        '#EJC202409041715: Eliminar líneas bonificadas si no se reservan completamente.
                                        If BeConfigEnc.Inferir_Bonificacion_Pedido_SAP Then

                                            If BeConfigEnc.Rechazar_Bonificacion_Incompleta Then

                                                If Not BeINAVPedDetAnt Is Nothing Then

                                                    If BeINAVPedDetAnt.Item_No = PDet.Item_No Then

                                                        clsPublic.Actualizar_Progreso(lblprg, "MSG202409042136: Se detectó una bonificación incompleta, se anularán las líneas bonificadas.")

                                                        '#EJC202409041718: Eliminar la línea anterior.
                                                        clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet(refBePedidoDetAnt.IdPedidoDet,
                                                                                                               lConectionInterface,
                                                                                                               lTransInterface)

                                                        clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoDet(pBePedidoEnc.IdPedidoEnc,
                                                                                                          refBePedidoDetAnt.IdPedidoDet,
                                                                                                          lConectionInterface,
                                                                                                          lTransInterface)

                                                        '#EJC202409041718: Eliminar la línea actual.
                                                        clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet(refBePedidoDet.IdPedidoDet,
                                                                                                               lConectionInterface,
                                                                                                               lTransInterface)

                                                        clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoDet(pBePedidoEnc.IdPedidoEnc,
                                                                                                          refBePedidoDet.IdPedidoDet,
                                                                                                          lConectionInterface,
                                                                                                          lTransInterface)

                                                    End If

                                                End If

                                            End If

                                        End If

                                        PDet.Status = 0

                                        clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet,
                                                                                          lConectionInterface,
                                                                                          lTransInterface)

                                        Dim vMotivoNoReserva As String = Obtener_Motivo_No_Reserva(PDet,
                                                                                                    "No hay existencia aplicable válida para la solicitud después de evaluar presentación, ubicación, vencimiento y reservas vigentes.")
                                        Dim vMensajeEx As String = lblprg.Text

                                        If BeCliente.IdUbicacionAbastecerCon = 0 Then

                                            vMensajeEx = String.Format(vbNewLine & "ERROR_202310021910: No se pudo completar la reserva para el pedido: {0} línea: {1} Código_Producto: {3} U.M.: {4} V.C.: {5} Descripción del error: {2} Cantidad: {6} ", PDet.NoEnc,
                                                                                PDet.Line_No,
                                                                                vMotivoNoReserva,
                                                                                PDet.Item_No,
                                                                                PDet.Unit_of_Measure_Code,
                                                                                PDet.Variant_Code,
                                                                                PDet.Quantity)
                                        Else

                                            vMensajeEx = String.Format(vbNewLine & "ERROR_202310021910A: No se pudo completar la reserva para el pedido: {0} línea: {1} Código_Producto: {3} U.M.: {4} V.C.: {5} Descripción del error: {2} Cantidad: {6} ", PDet.NoEnc,
                                                                                PDet.Line_No,
                                                                                vMotivoNoReserva & " Ubicación obligatoria del cliente: " & BeCliente.IdUbicacionAbastecerCon,
                                                                                PDet.Item_No,
                                                                                PDet.Unit_of_Measure_Code,
                                                                                PDet.Variant_Code,
                                                                                PDet.Quantity)

                                        End If

                                        clsPublic.Actualizar_Progreso(lblprg, vMensajeEx)

                                        PDet.Process_Result = Formatear_Process_Result_No_Reserva(vMotivoNoReserva)
                                        clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(PDet,
                                                                                              lConectionInterface,
                                                                                              lTransInterface)

                                        Dim BeEmpresa As New clsBeEmpresa
                                        BeEmpresa = clsLnEmpresa.GetSingle_By_IdBodega(IdBodegaOrigen,
                                                                                       lConectionInterface,
                                                                                       lTransInterface)

                                        Dim BeMensajeErrorWMS As New clsBeLog_error_wms
                                        BeMensajeErrorWMS.IdError = clsLnLog_error_wms.MaxID() + 1
                                        BeMensajeErrorWMS.IdEmpresa = BeEmpresa.IdEmpresa
                                        BeMensajeErrorWMS.IdBodega = IdBodegaOrigen
                                        BeMensajeErrorWMS.Fecha = Now
                                        BeMensajeErrorWMS.MensajeError = "Error_202303011638A: " & vMensajeEx
                                        BeMensajeErrorWMS.Line_No = PDet.Line_No
                                        BeMensajeErrorWMS.UmBas = PDet.Unit_of_Measure_Code
                                        BeMensajeErrorWMS.Variant_Code = PDet.Variant_Code
                                        BeMensajeErrorWMS.Cantidad = PDet.Quantity
                                        BeMensajeErrorWMS.Referencia_Documento = pBePedidoEnc.Referencia
                                        BeMensajeErrorWMS.Item_No = PDet.Item_No
                                        clsLnLog_error_wms.Insertar(BeMensajeErrorWMS)

                                        clsPublic.Actualizar_Progreso(lblprg, vMensajeEx)

                                    End If

                                Catch ex As Exception
                                    PDet.Status = 0
                                    PDet.Process_Result = ex.Message
                                    clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet, lConectionInterface, lTransInterface)
                                    clsPublic.Actualizar_Progreso(lblprg, ex.Message)
                                    Throw ex
                                End Try

                            Else 'es un pedido existente.

                                'Si la línea de detalle no existe
                                If Not clsLnTrans_pe_det.Existe(PedidoClienteExistente.IdPedidoEnc, PDet.Line_No,
                                                                pBePedidoDet,
                                                                PDet.No,
                                                                lConectionInterface,
                                                                lTransInterface) Then

                                    Try

                                        If Inserta_Linea_Detalle_Pedido(pBePedidoEnc,
                                                                        PDet,
                                                                        BeProducto,
                                                                        vDiasVencimientoCliente,
                                                                        BeUnidadMedida,
                                                                        BePresentacion,
                                                                        BeCliente,
                                                                        BeConfigEnc,
                                                                        IdBodegaOrigen,
                                                                        IdPropietarioBodegaOrigen,
                                                                        lblprg,
                                                                        lConectionInterface,
                                                                        lTransInterface,
                                                                        refBePedidoDet) Then

                                            PDet.Status = 1
                                            PDet.Process_Result = "Ok"
                                            clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet,
                                                                                              lConectionInterface,
                                                                                              lTransInterface)

                                            vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                            clsLnLog_error_wms.Agregar_Error(String.Format("línea nueva: {0} agregada a pedido existente: {1} ", PDet.Line_No, PDet.NoEnc))

                                            Dim vMensaje As String = String.Format("línea nueva: {0} agregada a pedido existente: {1} ", PDet.Line_No, PDet.NoEnc)
                                            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

                                        Else

                                            '#EJC202409041715: Eliminar líneas bonificadas si no se reservan completamente.
                                            If BeConfigEnc.Inferir_Bonificacion_Pedido_SAP Then

                                                If BeConfigEnc.Rechazar_Bonificacion_Incompleta Then

                                                    If Not BeINAVPedDetAnt Is Nothing Then

                                                        If BeINAVPedDetAnt.Item_No = PDet.Item_No Then

                                                            clsPublic.Actualizar_Progreso(lblprg, "MSG202409042136A: Se infirió una bonificación incompleta, se anularán las líneas bonificadas.")

                                                            '#EJC202409041718: Eliminar la línea anterior.
                                                            clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet(refBePedidoDetAnt.IdPedidoDet,
                                                                                                                   lConectionInterface,
                                                                                                                   lTransInterface)

                                                            clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoDet(pBePedidoEnc.IdPedidoEnc,
                                                                                                              refBePedidoDetAnt.IdPedidoDet,
                                                                                                              lConectionInterface,
                                                                                                              lTransInterface)

                                                            '#EJC202409041718: Eliminar la línea actual.
                                                            clsLnStock_res.Eliminar_Stock_Reservado_By_IdPedidoDet(refBePedidoDet.IdPedidoDet,
                                                                                                                   lConectionInterface,
                                                                                                                   lTransInterface)

                                                            clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoDet(pBePedidoEnc.IdPedidoEnc,
                                                                                                              refBePedidoDet.IdPedidoDet,
                                                                                                              lConectionInterface,
                                                                                                              lTransInterface)

                                                        End If

                                                    End If

                                                End If

                                            End If

                                            '#EJC202303011454: Log de errores de WMS.
                                            Dim vMotivoNoReserva As String = Obtener_Motivo_No_Reserva(PDet,
                                                                                                        "No hay existencia aplicable válida para la solicitud después de evaluar presentación, ubicación, vencimiento y reservas vigentes.")
                                            Dim vMensajeEx As String = String.Format(vbNewLine & "ERROR_202310021911: Al reservar stock para el pedido: {0} Línea: {1} Código_Producto: {3} U.M.: {4} V.C.: {5} Descripción del error: {2} Cantidad: {6}", PDet.NoEnc,
                                                                                    PDet.Line_No,
                                                                                    vMotivoNoReserva,
                                                                                    PDet.Item_No,
                                                                                    PDet.Unit_of_Measure_Code,
                                                                                    PDet.Variant_Code,
                                                                                    PDet.Quantity)

                                            PDet.Process_Result = Formatear_Process_Result_No_Reserva(vMotivoNoReserva)

                                            clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(PDet,
                                                                                                  lConectionInterface,
                                                                                                  lTransInterface)

                                            Dim BeEmpresa As New clsBeEmpresa
                                            BeEmpresa = clsLnEmpresa.GetSingle_By_IdBodega(IdBodegaOrigen)

                                            Dim BeMensajeErrorWMS As New clsBeLog_error_wms
                                            BeMensajeErrorWMS.IdError = clsLnLog_error_wms.MaxID() + 1
                                            BeMensajeErrorWMS.IdEmpresa = BeEmpresa.IdEmpresa
                                            BeMensajeErrorWMS.IdBodega = pBePedidoEnc.IdBodega
                                            BeMensajeErrorWMS.Fecha = Now
                                            BeMensajeErrorWMS.MensajeError = "Error_202303011638: " & vMensajeEx
                                            BeMensajeErrorWMS.Line_No = PDet.Line_No
                                            BeMensajeErrorWMS.UmBas = PDet.Unit_of_Measure_Code
                                            BeMensajeErrorWMS.Variant_Code = PDet.Variant_Code
                                            BeMensajeErrorWMS.Cantidad = PDet.Quantity
                                            BeMensajeErrorWMS.Referencia_Documento = pBePedidoEnc.Referencia
                                            BeMensajeErrorWMS.Item_No = PDet.Item_No
                                            clsLnLog_error_wms.Insertar(BeMensajeErrorWMS)

                                        End If

                                    Catch ex As Exception
                                        PDet.Status = 0
                                        PDet.Process_Result = ex.Message
                                        clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet, lConectionInterface, lTransInterface)
                                        clsPublic.Actualizar_Progreso(lblprg, ex.Message)
                                        Throw New Exception(String.Format("#ERROR_202112271234: {0} ", ex.Message))
                                    End Try

                                Else

                                    If pBePedidoDet.Cantidad <> PDet.Quantity Then

                                        clsLnLog_error_wms.Agregar_Error(String.Format("INF_20250519: El pedido: {0} existe,
                                                                                    la línea de detalle: {1} existe, 
                                                                                    cantidad_origen <> cantidad_destino
                                                                                    no se puede actualizar (aún)", PDet.NoEnc, PDet.Line_No))

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido: {0} exite,
                                                                        la línea de detalle: {1} existe, 
                                                                        cantidad_origen <> cantidad_destino
                                                                        no se puede actualizar (aún)", PDet.NoEnc, PDet.Line_No))

                                    Else

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido: {0} exite,
                                                                        la línea de detalle: {1} existe, 
                                                                        cantidad_origen = cantidad_destino
                                                                        no se actualizará", PDet.NoEnc, PDet.Line_No))
                                    End If

                                End If

                            End If 'fin TrasladoExistente

                            '#EJC202409041652: Punteros para revertir las líneas.
                            BeINAVPedDetAnt = PDet
                            refBePedidoDetAnt = refBePedidoDet

                        Next

                        Try

                            vCantStockRes = clsLnStock_res.Get_Count_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                                   lConectionInterface,
                                                                                                   lTransInterface)

                            '#EJC20180712: No se insertó ninguna línea de detalle del pedido
                            'Eliminar el encabezado.
                            If vContador_Lineas_Detalle_Pedido_Insertadas = 0 AndAlso Not vPedidoExistente Then

                                Try

                                    If Not clsLnTrans_pe_enc.Tiene_Detalle(pBePedidoEnc.IdPedidoEnc,
                                                                           lConectionInterface,
                                                                           lTransInterface) Then

                                        clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                                     lConectionInterface,
                                                                                     lTransInterface)

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido {0} de traslado no tiene líneas de detalle válidas para el WMS y se eliminará la cabecera: {1}", BeINavPedTrasladoEnc.No, vbNewLine))

                                    Else

                                        vContador_Lineas_Detalle_Pedido_Insertadas = clsLnTrans_pe_det.Get_Count_Lines_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                                                                            lConectionInterface,
                                                                                                                                            lTransInterface)

                                        '#CKFK20240808 Agregué otra validación para el caso en que el pedido si tenga líneas creadas

                                        If vContador_Lineas_Detalle_Pedido_Insertadas = 0 OrElse vCantStockRes = 0 Then
                                            '#EJC202310191429
                                            clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                              lConectionInterface,
                                                                                              lTransInterface)


                                            clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                                         lConectionInterface,
                                                                                         lTransInterface)


                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido {0} de traslado no tiene líneas de detalle válidas para el WMS y se eliminará la cabecera: {1}", BeINavPedTrasladoEnc.No, vbNewLine))
                                        ElseIf vCantStockRes > 0 Then
                                            Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS = pBePedidoEnc
                                        End If

                                    End If

                                Catch ex As Exception
                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("#202112271242: Error al eliminar cabecera de pedido de transferencia sin detalle : {0} {1}", ex.Message, vbNewLine))
                                End Try


                            ElseIf vContador_Lineas_Detalle_Pedido_Insertadas = BeINavPedTrasladoEnc.Lineas_Detalle.Count OrElse vCantStockRes > 0 Then
                                Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS = pBePedidoEnc
                            Else
                                If BeConfigEnc.Rechazar_pedido_incompleto Then
                                    Dim vMensajeError20230301 As String = String.Format("vContador_Lineas_Detalle_Pedido_Insertadas: " & vContador_Lineas_Detalle_Pedido_Insertadas & " BeINavPedTrasladoEnc.Lineas_Detalle.Count: " & BeINavPedTrasladoEnc.Lineas_Detalle.Count)
                                    clsLnLog_error_wms.Agregar_Error(vMensajeError20230301)
                                    '#EJC202303011403: aquí se debería de rechazar el pedido, hay que cambiar esta validación de líneas o corregirla.
                                    Throw New Exception(vMensajeError20230301)
                                Else
                                    Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS = pBePedidoEnc
                                End If
                            End If

                        Catch ex As Exception
                            Throw ex
                        End Try

                    End If

                    If Not vPedidoExistente Then

                        '#EJC202409041939:Eliminar la cabecera del pedido si no tiene detalle, esto puede ocurrir por las bonificaciones de becofarma.
                        If Not clsLnTrans_pe_enc.Tiene_Detalle(pBePedidoEnc.IdPedidoEnc,
                                                               lConectionInterface,
                                                               lTransInterface) Then

                            clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                         lConectionInterface,
                                                                         lTransInterface)

                        End If

                    End If

                End If

            Else
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Documento Inactivo {0} ", BeINavPedTrasladoEnc.No, vbNewLine))
            End If

            vContador += 1

            VContadorBitacoraTOMWMS = vContador

            If Not pBePedidoEnc Is Nothing Then
                If pBePedidoEnc.IdPedidoEnc > 0 Then

                    If vCantStockRes > 0 Then
                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Pedido procesado correctamente - IdPedido: {0}. {1} líneas procesadas correctamente de {2}",
                                                                        pBePedidoEnc.IdPedidoEnc,
                                                                        vContador_Lineas_Detalle_Pedido_Insertadas,
                                                                        BeINavPedTrasladoEnc.Lineas_Detalle.Count))

                    End If

                    If vContador_Lineas_Detalle_Pedido_Insertadas = BeINavPedTrasladoEnc.Lineas_Detalle.Count Then
                        Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS = pBePedidoEnc
                    End If

                End If
            End If

            Dim difSegundos As Double = DateDiff(DateInterval.Second, vFechaInicio, Now)

            If Not BeConfigEnc.Interface_SAP Then
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))
            End If

        Catch ex As Exception

            Dim vListaMensajeError As String = ""

            If Not pBePedidoEnc Is Nothing Then
                lLogErrorWMS = clsLnLog_error_wms.Get_All_By_Referencia_Documento(pBePedidoEnc.Referencia)
            Else
                lLogErrorWMS = clsLnLog_error_wms.Get_All_By_Referencia_Documento(BeINavPedTrasladoEnc.No)
            End If

            If Not lLogErrorWMS Is Nothing Then
                If lLogErrorWMS.Count > 0 Then
                    For Each Lwms In lLogErrorWMS
                        vListaMensajeError += vbNewLine & Lwms.MensajeError
                    Next
                End If
            End If

            Dim vMensajeError As String = ex.Message

            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
            clsLnLog_error_wms.Agregar_Error(vMensajeError)

            clsPublic.Actualizar_Progreso(lblprg, vMensajeError)

            If vListaMensajeError = "" Then
                Throw ex
            Else

                Throw New Exception(vMensajeError & vbNewLine & vListaMensajeError)
            End If

        End Try

    End Function

    Private Shared Function Inserta_Linea_Detalle_Pedido(ByVal BePedidoEnc As clsBeTrans_pe_enc,
                                                         ByRef pBeTrasladoDet As clsBeI_nav_ped_traslado_det,
                                                         ByVal pBePoducto As clsBeProducto,
                                                         ByVal pDiasVencimientoCliente As Integer,
                                                         ByVal pBeUnidadMedida As clsBeUnidad_medida,
                                                         ByVal pBePresentacion As clsBeProducto_Presentacion,
                                                         ByVal pBeCliente As clsBeCliente,
                                                         ByVal pBeConfigEnc As clsBeI_nav_config_enc,
                                                         ByVal pIdBodegaOrigen As Integer,
                                                         ByVal pIdPropietarioBodega As Integer,
                                                         ByVal pIdejecucionenc As Integer,
                                                         ByRef plblprg As RichTextBox,
                                                         ByRef lConectionInterface As SqlConnection,
                                                         ByRef lTransactionInterface As SqlTransaction) As Boolean

        Inserta_Linea_Detalle_Pedido = False

        Dim pBePedidoDet As New clsBeTrans_pe_det
        Dim pBeStockRes As New clsBeStock_res
        Dim IdNavConfigDet As Integer = 102 'Pedidos de clientes
        Dim IdxPresentacion As Integer = -1

        Try

            pBePedidoDet = New clsBeTrans_pe_det
            pBePedidoDet.IdPedidoDet = 0 'EJC20260226: En recepción automática en destino, el detalle de recepción se va creando a medida que se van procesando las líneas de despacho, por lo tanto no se tiene un IdRecepcionDet definido al momento de crear el objeto.
            pBePedidoDet.No_linea = pBeTrasladoDet.Line_No
            pBePedidoDet.Atributo_Variante_1 = pBeTrasladoDet.Variant_Code
            pBePedidoDet.IdPedidoEnc = BePedidoEnc.IdPedidoEnc
            pBePedidoDet.Producto = New clsBeProducto
            pBePedidoDet.Producto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(pBePoducto.IdProductoBodega,
                                                                                                 lConectionInterface,
                                                                                                 lTransactionInterface)
            pBePedidoDet.Producto.IdProductoBodega = pBePoducto.IdProductoBodega
            pBePedidoDet.IdProductoBodega = pBePoducto.IdProductoBodega
            pBePedidoDet.Codigo_Producto = pBeTrasladoDet.Item_No
            pBePedidoDet.Producto.Codigo = pBeTrasladoDet.Item_No
            '#EJC20220622:Quitar caracteres no permitidos.
            pBePedidoDet.Producto.Nombre = clsPublic.Quitar_Caracteres_No_Permitidos(pBeTrasladoDet.Description)
            pBePedidoDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(pBeTrasladoDet.Description)
            pBePedidoDet.IdUnidadMedidaBasica = pBeUnidadMedida.IdUnidadMedida
            pBePedidoDet.Cantidad = pBeTrasladoDet.Quantity
            pBePedidoDet.Peso = 0
            pBePedidoDet.Precio = pBeTrasladoDet.Price
            pBePedidoDet.No_recepcion = 0
            pBePedidoDet.Cant_despachada = 0
            pBePedidoDet.IdEstado = pBeConfigEnc.IdProductoEstado
            pBePedidoDet.Ndias = pDiasVencimientoCliente
            pBePedidoDet.Nom_estado = "Buen Estado"
            pBePedidoDet.IsNew = True
            pBePedidoDet.Fec_agr = Now
            pBePedidoDet.User_agr = pBeConfigEnc.IdUsuario
            pBePedidoDet.RoadDes = 0
            pBePedidoDet.RoadDesMon = 0
            pBePedidoDet.RoadPrecioDoc = pBeTrasladoDet.Price
            pBePedidoDet.RoadTotal = Math.Round(pBeTrasladoDet.Price * pBeTrasladoDet.Quantity, 6)
            pBePedidoDet.RoadVAL1 = 0
            pBePedidoDet.RoadVAL2 = 0

            If Not pBePresentacion Is Nothing Then
                If pBePresentacion.IdPresentacion <> 0 Then
                    pBePedidoDet.Nom_presentacion = pBePresentacion.Nombre
                    pBePedidoDet.IdPresentacion = pBePresentacion.IdPresentacion
                Else
                    pBePedidoDet.Nom_presentacion = ""
                End If
            Else
                pBePedidoDet.Nom_presentacion = ""
            End If

            pBePedidoDet.Nom_unid_med = pBeTrasladoDet.Unit_of_Measure_Code
            pBePedidoDet.Nom_estado = "Buen Estado"
            pBeStockRes.IdStockRes = 0
            pBeStockRes.IdTransaccion = BePedidoEnc.IdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.Indicador = "PED"
            pBeStockRes.añada = 0
            pBeStockRes.Cantidad = pBeTrasladoDet.Quantity
            pBeStockRes.Estado = "PPC"
            pBePedidoDet.Ndias = pDiasVencimientoCliente
            pBeStockRes.User_agr = pBeConfigEnc.IdUsuario
            pBeStockRes.Fec_agr = Now
            pBeStockRes.User_mod = pBeConfigEnc.IdUsuario
            pBeStockRes.Fec_mod = Now
            pBeStockRes.Host = "Interface"

            Dim BeProductoEstadoList As New List(Of clsBeProducto_estado)

            Dim vIdPropietario As Integer = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(pIdBodegaOrigen,
                                                                                                                      pIdPropietarioBodega,
                                                                                                                      lConectionInterface,
                                                                                                                      lTransactionInterface)
            Dim BeBodega As New clsBeBodega
            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pBeConfigEnc.Idbodega,
                                                         lConectionInterface,
                                                         lTransactionInterface)
            Try
                '#CKFK 20240723 Agregué esta funcionalidad para Clarispharma porque en el caso de ellos si van a poder sacar produdcto de cualquier área
                If Not BeBodega Is Nothing Then
                    If BeBodega.Interface_SAP AndAlso BeBodega.Restringir_Areas_SAP Then
                        pBeStockRes.IdProductoEstado = clsLnProducto_estado.Get_IdEstado_By_Codigo_Area(BePedidoEnc.Bodega_Origen,
                                                                                                        lConectionInterface,
                                                                                                        lTransactionInterface)
                    Else
                        '#EJC202220620:Buscar el estado de producto de la interface.
                        Dim vIdEstadoProductoInterface As Integer = pBeConfigEnc.IdProductoEstado

                        BeProductoEstadoList = clsLnProducto_estado.Existe_IdEstado_By_IdPropietario(vIdPropietario,
                                                                                                     vIdEstadoProductoInterface,
                                                                                                     lConectionInterface,
                                                                                                     lTransactionInterface)

                        If Not BeProductoEstadoList Is Nothing Then

                            If Not BeProductoEstadoList.FirstOrDefault() Is Nothing Then
                                pBeStockRes.IdProductoEstado = BeProductoEstadoList.FirstOrDefault.IdEstado()
                            Else
                                Throw New Exception("ERR_202205121200A: Error al obtener el estado de producto por defecto para los parámetros IdPropietario: " & pIdPropietarioBodega & " and IdBodega: " & pIdBodegaOrigen)
                            End If

                        Else
                            Throw New Exception("ERR_202205121200B: Error al obtener el estado de producto por defecto para los parámetros IdPropietario: " & pIdPropietarioBodega & " and IdBodega: " & pIdBodegaOrigen)
                        End If

                    End If
                End If

            Catch ex As Exception
                Throw New Exception("ERR_20250917: " & ex.Message)
            End Try

            pBeStockRes.IdPedido = BePedidoEnc.IdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.IdProductoBodega = pBePoducto.IdProductoBodega

            '#EJC20220512: 'clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(pIdBodega,pIdPropietarioBodega,lConectionInterface,lTransactionInterface)
            pBeStockRes.IdPropietarioBodega = pIdPropietarioBodega
            pBeStockRes.IdUnidadMedida = clsLnProducto.Get_Id_Unidad_Medida_By_Codigo(pBePedidoDet.Producto.Codigo,
                                                                                      lConectionInterface,
                                                                                      lTransactionInterface)
            pBeStockRes.Atributo_Variante_1 = pBePedidoDet.Atributo_Variante_1

            '#EJC20190314: Asignar control ultimo lote a objeto de reserva.
            pBeStockRes.Control_Ultimo_Lote = pBeCliente.Control_Ultimo_Lote

            Dim BePresentacion As New clsBeProducto_Presentacion

            If pBePedidoDet.IdPresentacion <> 0 Then

                If Not pBePedidoDet.Atributo_Variante_1 Is Nothing Then

                    '#CKFK20240724 Cambié la función Existe_By_IdProducto_And_Codigo 
                    BePresentacion = New clsBeProducto_Presentacion
                    BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(pBePedidoDet.Producto.IdProducto,
                                                                                              pBePedidoDet.Atributo_Variante_1,
                                                                                              lConectionInterface,
                                                                                              lTransactionInterface)

                    If Not BePresentacion Is Nothing Then
                        pBeStockRes.IdPresentacion = BePresentacion.IdPresentacion
                    Else
                        pBeStockRes.IdPresentacion = 0 'No se encontró la presentación solicitada
                    End If

                Else
                    pBeStockRes.IdPresentacion = 0 'No se encontró la presentación solicitada
                End If

            End If

            If pBeStockRes.Control_Ultimo_Lote Then
                '#EJC20190314: Capturar último lote despachado para evitar enviar el mismo.
                pBeStockRes.Ultimo_Lote = clsLnVW_Despacho_Rep.Get_Ultimo_Lote_By_IdCliente(pBeCliente.IdCliente,
                                                                                            pBePedidoDet.Producto.IdProducto)
            End If

            '#EJC20220712_0853:Asignar la ubicación con la que se va a abastecer el pedido de un determinado cliente.
            'MI3: (Quedaría pendiente validar si la ubicación que trae es válida, pero eso que lo haga otro... que está viendo mi pantalla.
            If Val(pBeCliente.IdUbicacionAbastecerCon) <> 0 Then
                pBeStockRes.IdUbicacionAbastecerCon = pBeCliente.IdUbicacionAbastecerCon
            Else
                pBeStockRes.IdUbicacionAbastecerCon = 0
            End If

            Try

                If clsLnTrans_pe_det.Reservar_Stock_Por_Linea_Interface(pDiasVencimientoCliente,
                                                                        pBeTrasladoDet,
                                                                        pBePedidoDet,
                                                                        pBeStockRes,
                                                                        "Interface",
                                                                        pBeConfigEnc,
                                                                        pIdPropietarioBodega,
                                                                        lConectionInterface,
                                                                        lTransactionInterface) Then
                    Inserta_Linea_Detalle_Pedido = True

                    pBeTrasladoDet.Process_Result = "Ok"
                    clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                          lConectionInterface,
                                                                          lTransactionInterface)

                Else

                    '#EJC202303011454: Log de errores de WMS.
                    Dim vMotivoNoReserva As String = Obtener_Motivo_No_Reserva(pBeTrasladoDet,
                                                                                "No hay existencia aplicable válida para la solicitud después de evaluar presentación, ubicación, vencimiento y reservas vigentes.")
                    Dim vMensajeEx As String = String.Format(vbNewLine & "ERROR_202310021909: Al reservar stock para el pedido: {0} Línea: {1} Código_Producto: {3} U.M.: {4} V.C.: {5} Descripción del error: {2} Cantidad: {6}", pBeTrasladoDet.NoEnc,
                                                            pBeTrasladoDet.Line_No,
                                                            vMotivoNoReserva,
                                                            pBeTrasladoDet.Item_No,
                                                            pBeTrasladoDet.Unit_of_Measure_Code,
                                                            pBeTrasladoDet.Variant_Code,
                                                            pBeTrasladoDet.Quantity)

                    pBeTrasladoDet.Process_Result = Formatear_Process_Result_No_Reserva(vMotivoNoReserva)

                    clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                          lConectionInterface,
                                                                          lTransactionInterface)

                End If

            Catch ex As Exception

                Dim vMensajeEx As String = String.Format(vbNewLine & "{0}{1}{2}{2}{2}{2} Documento:{7} línea:{3} U.M: {5} V.C: {6}",
                                                         ex.Message,
                                                         vbNewLine,
                                                         vbTab,
                                                         pBeTrasladoDet.Line_No,
                                                         pBeTrasladoDet.Item_No,
                                                         pBeTrasladoDet.Unit_of_Measure_Code,
                                                         pBeTrasladoDet.Variant_Code,
                                                         BePedidoEnc.Referencia)

                pBeTrasladoDet.Process_Result = vMensajeEx

                clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                      lConectionInterface,
                                                                      lTransactionInterface)

                clsPublic.Actualizar_Progreso(plblprg, vMensajeEx)

                If pBeConfigEnc.Rechazar_pedido_incompleto Then
                    Throw New Exception(vMensajeEx)
                End If

            End Try

        Catch ex As Exception
            Dim st As New StackTrace(True)
            st = New StackTrace(ex, True)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exist(ByVal pNo As String,
                                 ByVal pTipoDocumento As clsDataContractDI.tTipoDocumentoSalida,
                                 ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction) As Boolean

        Exist = False

        Try

            Const sp As String = "SELECT No FROM i_nav_ped_traslado_enc Where(No = @No AND Document_Type = @Document_Type)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", pNo))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Document_Type", pTipoDocumento))

            Dim dt As New DataTable
            dad.Fill(dt)

            Exist = dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Pedidos_Interface(ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

        Dim lTable As New DataTable("Result")

        Try


            Dim vSQL As String = String.Format("SELECT enc.no as NoEnc,enc.posting_date as Fecha,det.Description as Producto,det.Item_No as código, SUM(det.Quantity) as Cantidad
                                FROM i_nav_ped_traslado_enc enc inner join 
                                i_nav_ped_traslado_det det on det.NoEnc = enc.No WHERE ")

            vSQL += String.Format(" cast(enc.posting_date AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

            vSQL += " GROUP BY enc.no,enc.posting_date,det.Description,det.Item_No"

            Using lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)
                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.SelectCommand.Transaction = lTransaction
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

    Public Shared Function Eliminar_Pedido_By_NoEnc(ByVal NoEnc As String) As Integer

        Eliminar_Pedido_By_NoEnc = 0

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim rowsAffected As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const spDet As String = " Delete from i_nav_ped_traslado_det " &
             "  Where(NoEnc = @No)"

            Dim cmddet As New SqlCommand(spDet, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmddet.Parameters.Add(New SqlParameter("@NO", NoEnc))
            rowsAffected += cmddet.ExecuteNonQuery()
            cmddet.Dispose()

            Const sp As String = " Delete from i_nav_ped_traslado_enc " &
             "  Where(No = @No)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@NO", NoEnc))
            rowsAffected += cmd.ExecuteNonQuery()
            cmd.Dispose()

            lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Eliminar_Pedido_Tabla_Intermedia_By_NoEnc(ByVal NoEnc As String,
                                                                     ByVal lConnection As SqlConnection,
                                                                     ByVal lTransaction As SqlTransaction) As Boolean

        Eliminar_Pedido_Tabla_Intermedia_By_NoEnc = False

        Dim rowsAffected As Integer = 0

        Try

            Const spDet As String = " Delete from i_nav_ped_traslado_det " &
             "  Where(NoEnc = @No)"

            Dim cmddet As New SqlCommand(spDet, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmddet.Parameters.Add(New SqlParameter("@NO", NoEnc))
            rowsAffected += cmddet.ExecuteNonQuery()
            cmddet.Dispose()

            Const sp As String = " Delete from i_nav_ped_traslado_enc " &
             "  Where(No = @No)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.Add(New SqlParameter("@NO", NoEnc))
            rowsAffected += cmd.ExecuteNonQuery()
            cmd.Dispose()

            Eliminar_Pedido_Tabla_Intermedia_By_NoEnc = (rowsAffected > 0)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Importar_Transferencia(ByRef PedidoCliente As clsBeI_nav_ped_traslado_enc,
                                                  ByRef lblprg As RichTextBox,
                                                  ByRef lConection As SqlConnection,
                                                  ByRef lTransaction As SqlTransaction) As Boolean

        'Dim lConnectionLog As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim IdNavConfigDet As Integer = 102 'Pedidos de clientes
        Dim BeNavEjecucionEnc As New clsBeI_nav_ejecucion_enc
        Dim IdxProductoBodegaInMemory As Integer = 0
        Dim vContadorLineas As Integer = 0

        Importar_Transferencia = False

        Try

            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim BeBodega As New clsBeBodega
            Dim vContador As Integer = 0

            BeNavEjecucionEnc.IdNavConfigEnc = 1
            BeNavEjecucionEnc.Fecha = Now
            '#EJCCKFK20260520: Cambio por Identity en tabla.
            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc)

            Try

                If Not Exist(PedidoCliente.No, PedidoCliente.Document_Type, lConection, lTransaction) Then
                    Insertar(PedidoCliente, lConection, lTransaction)
                End If

                vContador += 1

                lTransaction.Save("Encabezado")

                Application.DoEvents()

                If Not PedidoCliente.Lineas_Detalle Is Nothing Then

                    For Each BeI_Nav_PedidoTrasladoDet As clsBeI_nav_ped_traslado_det In PedidoCliente.Lineas_Detalle

                        Try

                            BeI_Nav_PedidoTrasladoDet.NoEnc = PedidoCliente.No
                            BeI_Nav_PedidoTrasladoDet.No = BeI_Nav_PedidoTrasladoDet.Item_No
                            BeI_Nav_PedidoTrasladoDet.Variant_Code = BeI_Nav_PedidoTrasladoDet.Variant_Code

                            '#EJC20171106_1023AM_REF02: El valor nothing indica el final de la vista.
                            If Not BeI_Nav_PedidoTrasladoDet.Item_No Is Nothing Then

                                '#CKFK20221108 Agregué esto para poder obtener la Bodega
                                BeBodega = New clsBeBodega
                                BeBodega = clsLnBodega.GetSingle_By_Codigo(PedidoCliente.Transfer_from_Code,
                                                                           lConection,
                                                                           lTransaction)

                                If BeBodega Is Nothing Then
                                    Throw New Exception("ERROR_20231031: La bodega: " & PedidoCliente.Transfer_from_Code & " no existe.")
                                End If

                                BeProductoBodega = New clsBeProducto_bodega()
                                BeProductoBodega = clsLnProducto_bodega.Existe(BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                               BeBodega.IdBodega,
                                                                               lConection,
                                                                               lTransaction)
                                If Not BeProductoBodega Is Nothing Then
                                    lProductoBodegaInMemory.Add(BeProductoBodega.Clone())
                                Else
                                    Throw New Exception("El producto: " & BeI_Nav_PedidoTrasladoDet.Item_No & " No está asociado a la bodega: " & PedidoCliente.Transfer_from_Code & " o no existe en el maestro de materiales.")
                                End If

                                'Existe el producto en el maestro?
                                If Not BeProductoBodega Is Nothing Then

                                    'Si Cantidad Recibida es <> 0 no se importa                                    
                                    If (BeI_Nav_PedidoTrasladoDet.Qty_to_Receive = 0) Then

                                        If clsLnI_nav_ped_traslado_det.Exist(BeI_Nav_PedidoTrasladoDet) Then
                                            clsLnI_nav_ped_traslado_det.ActualizarFromIn(BeI_Nav_PedidoTrasladoDet, lConection, lTransaction)
                                        Else
                                            clsLnI_nav_ped_traslado_det.Insertar(BeI_Nav_PedidoTrasladoDet, lConection, lTransaction)
                                        End If

                                        vContadorLineas += 1

                                    Else

                                        Try

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("Qty_to_Receive <> 0: No se importará, Qty_to_Receive debe ser 0 para procesar. ",
                                                                                       BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                       IdNavConfigDet,
)
                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Qty_to_Receive <> 0: No se importará, Qty_to_Receive debe ser 0 para procesar. : {0}{1}", BeI_Nav_PedidoTrasladoDet.Item_No, vbNewLine))

                                        Catch ex As Exception
                                            Throw ex
                                        End Try

                                    End If 'Fin Si Qty_Ro_Receive =0

                                End If 'FIn Existe el producto en el maestro?

                            Else
                                Debug.Print("_: " & BeI_Nav_PedidoTrasladoDet.Description)
                            End If

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                     "Sin informacion",
                                                                     BeNavEjecucionEnc.IdEjecucionEnc,
                                                                     IdNavConfigDet)

                            Throw ex

                        End Try

                    Next

                Else
                    clsPublic.Actualizar_Progreso(lblprg, "Pedido de compra sin lineas de detalle?")
                End If

            Catch ex As Exception


                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                          PedidoCliente.No,
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          IdNavConfigDet)

                Throw ex

            End Try

            Importar_Transferencia = (vContadorLineas > 0)

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       IdNavConfigDet)

            clsPublic.Actualizar_Progreso(lblprg, String.Format("{1}{0}{1}", vbNewLine, ex.Message))

            Throw ex

        End Try

    End Function

    '#CKFK20240227 creó esta función para importar los traslados hacia SAP
    Public Shared Function Importar_Traslado_A_Tabla_Intermedia(ByRef BeNavPedTrasladoEnc As clsBeI_nav_ped_traslado_enc,
                                                                 ByRef Resultado As String) As Integer

        Importar_Traslado_A_Tabla_Intermedia = 0

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim lblprg As New RichTextBox
        Dim vIdBodegaOrigenSAP As Integer = 0
        Dim vIdBodegaWMS As Integer = 0
        Dim vIdPropietario As Integer = 0
        Dim vIdPropietarioBodegaOrigen As Integer = 0
        Dim vIdxConfig As Integer = -1
        Dim vIndicadorDeExcepcion As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            'código de Bodega origen -> la que despacha el producto.
            vIdBodegaWMS = clsLnBodega.Get_IdBodega_By_CodigoArea(BeNavPedTrasladoEnc.Transfer_from_Code,
                                                                  lConnection,
                                                                  lTransaction)

            vIdBodegaOrigenSAP = clsLnBodega_area.Get_IdArea_By_Codigo_Area(BeNavPedTrasladoEnc.Transfer_from_Code,
                                                                            lConnection,
                                                                            lTransaction)

            vIndicadorDeExcepcion = 1

            If vIdBodegaOrigenSAP = 0 Then
                Throw New Exception(String.Format("El código de la bodega origen: {0} no es válido", BeNavPedTrasladoEnc.Transfer_from_Code))
            End If

            'código de propietario del que se despachará la mercancía.
            vIdPropietario = clsLnPropietarios.Get_IdPropietario_By_Codigo(BeNavPedTrasladoEnc.Product_Owner_Code,
                                                                          lConnection,
                                                                          lTransaction)

            If vIdPropietario = 0 Then
                Throw New Exception(String.Format("El código de propietario: ({0}) no es válido", BeNavPedTrasladoEnc.Product_Owner_Code, BeNavPedTrasladoEnc.Transfer_from_Code))
            End If

            vIndicadorDeExcepcion = 2

            'código del propietario asociado a la bodega ? (es válido ese propietario para esa bodega?)
            vIdPropietarioBodegaOrigen = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdArea(vIdPropietario,
                                                                                                                     vIdBodegaOrigenSAP,
                                                                                                                     lConnection,
                                                                                                                     lTransaction)

            If vIdPropietarioBodegaOrigen = 0 Then
                Throw New Exception(String.Format("El código de propietario: ({0}) de la bodega origen: ({1}) no es válido", BeNavPedTrasladoEnc.Product_Owner_Code, BeNavPedTrasladoEnc.Transfer_from_Code))
            End If

            vIndicadorDeExcepcion = 3

            If Importar_Cambio_Ubicacion_A_Tabla_Intermedia(BeNavPedTrasladoEnc,
                                                            lblprg,
                                                            lConnection,
                                                            lTransaction) Then

                vIndicadorDeExcepcion = 4

                vIdxConfig = lBeConfigInMemory.FindIndex(Function(x) x.Idbodega = vIdBodegaOrigenSAP AndAlso x.IdPropietario = vIdPropietario)

                Dim BeConfigEnc As New clsBeI_nav_config_enc With {.Idnavconfigenc = 1, .IdPropietario = 1}

                BeConfigEnc = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario(vIdBodegaWMS,
                                                                                            vIdPropietario,
                                                                                            lConnection,
                                                                                            lTransaction)

                If BeConfigEnc Is Nothing Then
                    Dim vMsgEx As String = "ERROR_202310311436: No existe la configuración asociada a la bodega: " & vIdBodegaOrigenSAP & " en la tabla i_nav_config_enc configure los parámetros por defecto para la interfaz"
                    clsPublic.Actualizar_Progreso(lblprg, vMsgEx)
                    Resultado = lblprg.Text
                    Throw New Exception(vMsgEx)
                Else

                    vIndicadorDeExcepcion = 5

                    clsLnTrans_ubic_hh_enc.Guardar_Traslado_SAP(BeNavPedTrasladoEnc,
                                                                BeConfigEnc,
                                                                vIdPropietarioBodegaOrigen,
                                                                "MI3",
                                                                lblprg,
                                                                lConnection,
                                                                lTransaction)

                    Resultado = lblprg.Text

                End If

            Else
                Resultado = lblprg.Text
            End If

            lTransaction.Commit()

            '#CKFK20240424 Resultado de la importación
            Importar_Traslado_A_Tabla_Intermedia = 1

        Catch ex As Exception
            If Not lTransaction Is Nothing Then
                lTransaction.Rollback()
            Else
                Throw ex
            End If
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    '#CKFK20240227 creó esta función para crear la tarea de cambio de ubicación dirigida al importar los traslados hacia SAP
    Public Shared Function Importar_Cambio_Ubicacion_A_Tabla_Intermedia(ByRef BeNavPedTrasladoEnc As clsBeI_nav_ped_traslado_enc,
                                                                        ByRef lblprg As RichTextBox,
                                                                        ByRef lConection As SqlConnection,
                                                                        ByRef lTransaction As SqlTransaction) As Boolean

        Dim IdNavConfigDet As Integer = 102 'Pedidos de clientes
        Dim BeNavEjecucionEnc As New clsBeI_nav_ejecucion_enc
        Dim IdxProductoBodegaInMemory As Integer = 0
        Dim vContadorLineas As Integer = 0

        Importar_Cambio_Ubicacion_A_Tabla_Intermedia = False

        Try

            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim BeTransUbicHHEnc As New clsBeTrans_ubic_hh_enc
            Dim BeBodega As New clsBeBodega
            Dim vContador As Integer = 0

            BeNavEjecucionEnc.IdNavConfigEnc = 1
            BeNavEjecucionEnc.Fecha = Now
            '#EJCCKFK20260520: Cambio por Identity en tabla.
            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc)

            Try

                If Not Exist(BeNavPedTrasladoEnc.No, BeNavPedTrasladoEnc.Document_Type, lConection, lTransaction) Then
                    Insertar(BeNavPedTrasladoEnc, lConection, lTransaction)
                End If

                vContador += 1

                lTransaction.Save("Encabezado")

                Application.DoEvents()

                If Not BeNavPedTrasladoEnc.Lineas_Detalle Is Nothing Then

                    For Each BeI_Nav_PedidoTrasladoDet As clsBeI_nav_ped_traslado_det In BeNavPedTrasladoEnc.Lineas_Detalle

                        Try

                            BeI_Nav_PedidoTrasladoDet.NoEnc = BeNavPedTrasladoEnc.No
                            BeI_Nav_PedidoTrasladoDet.No = BeI_Nav_PedidoTrasladoDet.Item_No
                            BeI_Nav_PedidoTrasladoDet.Variant_Code = BeI_Nav_PedidoTrasladoDet.Variant_Code

                            '#EJC20171106_1023AM_REF02: El valor nothing indica el final de la vista.
                            If Not BeI_Nav_PedidoTrasladoDet.Item_No Is Nothing Then

                                '#CKFK20221108 Agregué esto para poder obtener la Bodega
                                BeBodega = New clsBeBodega
                                BeBodega = clsLnBodega.GetSingle_By_CodigoArea(BeNavPedTrasladoEnc.Transfer_from_Code,
                                                                               lConection,
                                                                               lTransaction)

                                If BeBodega Is Nothing Then
                                    Throw New Exception("ERROR_20231031: La bodega: " & BeNavPedTrasladoEnc.Transfer_from_Code & " no existe.")
                                End If

                                BeProductoBodega = New clsBeProducto_bodega()
                                BeProductoBodega = clsLnProducto_bodega.Existe(BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                               BeBodega.IdBodega,
                                                                               lConection,
                                                                               lTransaction)
                                If Not BeProductoBodega Is Nothing Then
                                    lProductoBodegaInMemory.Add(BeProductoBodega.Clone())
                                Else
                                    Throw New Exception("El producto: " & BeI_Nav_PedidoTrasladoDet.Item_No & " No está asociado a la bodega: " & BeNavPedTrasladoEnc.Transfer_from_Code & " o no existe en el maestro de materiales.")
                                End If

                                'Existe el producto en el maestro?
                                If Not BeProductoBodega Is Nothing Then

                                    'Si Cantidad Recibida es <> 0 no se importa                                    
                                    If (BeI_Nav_PedidoTrasladoDet.Qty_to_Receive = 0) Then

                                        If clsLnI_nav_ped_traslado_det.Exist(BeI_Nav_PedidoTrasladoDet, lConection, lTransaction) Then
                                            clsLnI_nav_ped_traslado_det.ActualizarFromIn(BeI_Nav_PedidoTrasladoDet, lConection, lTransaction)
                                        Else
                                            clsLnI_nav_ped_traslado_det.Insertar(BeI_Nav_PedidoTrasladoDet, lConection, lTransaction)

                                            For Each BeI_Nav_PedidoTrasladoDetLote As clsBeI_nav_ped_traslado_det_lote In BeI_Nav_PedidoTrasladoDet.Lotes_Detalle

                                                If Not clsLnI_nav_ped_traslado_det_lote.Exist(BeI_Nav_PedidoTrasladoDetLote, lConection, lTransaction) Then
                                                    clsLnI_nav_ped_traslado_det_lote.Insertar(BeI_Nav_PedidoTrasladoDetLote, lConection, lTransaction)
                                                End If

                                            Next

                                        End If

                                        vContadorLineas += 1

                                    Else

                                        Try

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("Qty_to_Receive <> 0: No se importará, Qty_to_Receive debe ser 0 para procesar. ",
                                                                                       BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                       IdNavConfigDet,
)

                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Qty_to_Receive <> 0: No se importará, Qty_to_Receive debe ser 0 para procesar. : {0}{1}", BeI_Nav_PedidoTrasladoDet.Item_No, vbNewLine))

                                        Catch ex As Exception
                                            Throw ex
                                        End Try

                                    End If 'Fin Si Qty_Ro_Receive =0

                                End If 'FIn Existe el producto en el maestro?

                            Else
                                Debug.Print("_: " & BeI_Nav_PedidoTrasladoDet.Description)
                            End If

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                     "Sin informacion",
                                                                     BeNavEjecucionEnc.IdEjecucionEnc,
                                                                     IdNavConfigDet)

                            Throw ex

                        End Try

                    Next

                Else
                    Console.WriteLine("Traslado sin lineas de detalle?")
                End If

            Catch ex As Exception

                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                          BeNavPedTrasladoEnc.No,
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          IdNavConfigDet)

                Throw ex

            End Try

            Importar_Cambio_Ubicacion_A_Tabla_Intermedia = (vContadorLineas > 0)

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       IdNavConfigDet)
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)

            Throw ex

        End Try

    End Function

    '#CKFK20240228 creó esta función para importar el traslado de SAP, cuando es un cambio de estado
    Public Shared Function Importar_Traslado_Tabla_Intermedia_A_WMS(ByRef BeINavPedTrasladoEnc As clsBeI_nav_ped_traslado_enc,
                                                                   ByVal IdBodegaOrigen As Integer,
                                                                   ByVal IdPropietarioBodegaOrigen As Integer,
                                                                   ByVal BeConfigEnc As clsBeI_nav_config_enc,
                                                                   ByRef lConectionInterface As SqlConnection,
                                                                   ByRef lTransInterface As SqlTransaction,
                                                                   ByRef lblprg As RichTextBox) As Boolean

        Importar_Traslado_Tabla_Intermedia_A_WMS = False

        Dim BeNavEjecucionEnc As New clsBeI_nav_ejecucion_enc
        Dim BeConfigDet As New clsBeI_nav_config_det
        Dim vCodigoCliente As String = ""
        Dim vIdxClienteInMemory As Integer = -1
        Dim pBePedidoEnc As clsBeTrans_pe_enc = Nothing
        Dim lLogErrorWMS As New List(Of clsBeLog_error_wms)

        Try

            BeNavEjecucionEnc.IdNavConfigEnc = 102 'Pedido de cliente
            BeNavEjecucionEnc.Fecha = Now
            BeNavEjecucionEnc.IdBodega = IdBodegaOrigen
            BeNavEjecucionEnc.IdPropietario = IdPropietarioBodegaOrigen

            clsLnI_nav_ejecucion_enc.Insertar_From_Interface(BeNavEjecucionEnc)

            Dim BeNavEjecucionRes As New clsBeI_nav_ejecucion_res
            BeNavEjecucionRes.IdEjecucionEnc = BeNavEjecucionEnc.IdEjecucionEnc
            '#EJCCKFK20260520: Cambio por Identity en tabla.
            BeNavEjecucionRes.IdNavConfigDet = 102 'Pedido de cliente
            BeNavEjecucionRes.Registros_ws = 0
            BeNavEjecucionRes.Registros_ti = 0
            BeNavEjecucionRes.Registros_WMS = 0
            BeNavEjecucionRes.Exitosa = False

            clsLnI_nav_ejecucion_res.Insertar(BeNavEjecucionRes)

            Dim TrasladoExistente As clsBeTrans_pe_enc = Nothing
            Dim BeCliente As New clsBeCliente
            Dim vContador As Integer = 0
            Dim vContadorLineasDet As Integer = 0
            Dim pClienteTiemposList As New List(Of clsBeCliente_tiempos)
            Dim BeProducto As New clsBeProducto
            Dim pBePedidoDet As New clsBeTrans_pe_det
            Dim vClienteTiempo As New clsBeCliente_tiempos
            Dim vDiasVencimientoCliente As Integer = 0
            Dim BeUnidadMedida As New clsBeUnidad_medida
            Dim BePresentacion As New clsBeProducto_Presentacion
            Dim vContador_Lineas_Detalle_Pedido_Insertadas As Integer = 0
            Dim VContadorBitacoraTOMWMS As Integer = 0
            Dim VContadorBitacoraIntermedia As Integer = 0
            Dim IdxProducto As Integer = 0
            Dim vCodigoProducto As String = ""
            Dim IdxPresentacion As Integer = 0
            Dim BeRoadRuta As New clsBeRoad_ruta()
            Dim BeRoadVendedor As New clsBeRoad_p_vendedor()
            Dim vMsgEx3 As String = ""

            VContadorBitacoraTOMWMS = 0

            If BeINavPedTrasladoEnc.Status > 0 Then

                clsPublic.Actualizar_Progreso(lblprg, String.Format("Procesando P.T.: {0} ", BeINavPedTrasladoEnc.No, vbNewLine))

                If BeINavPedTrasladoEnc.Lineas_Detalle.Count > 0 Then

                    pBePedidoEnc = New clsBeTrans_pe_enc() With {.Referencia = BeINavPedTrasladoEnc.No,
                                                                 .IdTipoPedido = BeINavPedTrasladoEnc.Document_Type}

                    TrasladoExistente = clsLnTrans_pe_enc.Get_Single_By_Referencia(pBePedidoEnc,
                                                                                   lConectionInterface,
                                                                                   lTransInterface)

                    vContadorLineasDet = 0

                    vCodigoCliente = BeINavPedTrasladoEnc.Transfer_to_Code

                    BeCliente = New clsBeCliente
                    BeCliente = clsLnCliente.Get_Single_By_Codigo(BeINavPedTrasladoEnc.Transfer_to_Code,
                                                                  lConectionInterface,
                                                                  lTransInterface)


                    If BeCliente Is Nothing Then
                        Throw New Exception(String.Format("{0} No existe el cliente {1} en maestro para pedido de tralsado ", MethodBase.GetCurrentMethod.Name(), BeINavPedTrasladoEnc.Transfer_to_Code))
                    End If

                    BeRoadRuta = clsLnRoad_ruta.Get_IdRuta_By_Codigo(BeINavPedTrasladoEnc.Transfer_to_CodeField,
                                                                     lConectionInterface,
                                                                     lTransInterface)

                    BeRoadVendedor = clsLnRoad_p_vendedor.Get_Vendedor_By_Codigo(BeINavPedTrasladoEnc.Transfer_from_Contact,
                                                                                 lConectionInterface,
                                                                                 lTransInterface)

                    Dim vPedidoAnulado As Boolean = False

                    If Not TrasladoExistente Is Nothing Then
                        If TrasladoExistente.Estado = "Anulado" Then
                            vPedidoAnulado = True
                        End If
                    End If

                    If Not TrasladoExistente Is Nothing AndAlso Not vPedidoAnulado Then
                        pBePedidoEnc.Activo = True
                        clsLnLog_error_wms.Agregar_Error("APED_EXISTENTE_WMS: " & TrasladoExistente.IdPedidoEnc)
                    Else

                        '#EJC202303011648: Eliminar_By_Referencia_Documento antes de procesar el pedido.
                        clsLnLog_error_wms.Eliminar_By_Referencia_Documento(BeINavPedTrasladoEnc.No,
                                                                            lConectionInterface,
                                                                            lTransInterface)

                        '#EJC20171107_REF13_0506AM: El MaxId del IdPedidoEnc se genera dentro del insert                            
                        pBePedidoEnc.Fecha_Pedido = BeINavPedTrasladoEnc.Posting_Date
                        pBePedidoEnc.Referencia = BeINavPedTrasladoEnc.No
                        pBePedidoEnc.IdBodega = IdBodegaOrigen
                        pBePedidoEnc.Cliente = New clsBeCliente
                        pBePedidoEnc.Cliente.IdCliente = BeCliente.IdCliente
                        pBePedidoEnc.IdCliente = BeCliente.IdCliente
                        pBePedidoEnc.Control_Ultimo_Lote = BeCliente.Control_Ultimo_Lote
                        pBePedidoEnc.IdMuelle = 0
                        pBePedidoEnc.PropietarioBodega = New clsBePropietario_bodega
                        pBePedidoEnc.PropietarioBodega.IdPropietarioBodega = IdPropietarioBodegaOrigen
                        pBePedidoEnc.IdPropietarioBodega = IdPropietarioBodegaOrigen
                        pBePedidoEnc.TipoPedido = New clsBeTrans_pe_tipo()

                        '#EJC20210429 Recibir el tipo de documento desde la interface.
                        If BeINavPedTrasladoEnc.Document_Type = 0 Then
                            pBePedidoEnc.TipoPedido.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
                        Else
                            pBePedidoEnc.TipoPedido.IdTipoPedido = BeINavPedTrasladoEnc.Document_Type
                        End If

                        pBePedidoEnc.Fecha_Pedido = BeINavPedTrasladoEnc.Posting_Date
                        pBePedidoEnc.Hora_ini = Now
                        pBePedidoEnc.Hora_fin = Now.AddHours(1)
                        pBePedidoEnc.HoraEntregaDesde = Now
                        pBePedidoEnc.HoraEntregaHasta = Now.AddHours(1)
                        pBePedidoEnc.Ubicacion = 1
                        pBePedidoEnc.Estado = "Nuevo"
                        pBePedidoEnc.No_despacho = 0
                        pBePedidoEnc.Activo = True
                        pBePedidoEnc.User_agr = BeConfigEnc.IdUsuario
                        pBePedidoEnc.Fec_agr = Now
                        pBePedidoEnc.User_mod = BeConfigEnc.IdUsuario
                        pBePedidoEnc.Fec_mod = Now
                        '#EJC20171107_REF14_0507AM: Se sobreescribe No_documento en InsertaEncabezado por consecutivo de sistema
                        'pBePedidoEnc.No_documento = BeINavPedTrasladoEnc.No
                        pBePedidoEnc.Local = True
                        pBePedidoEnc.Pallet_primero = True
                        pBePedidoEnc.Dias_cliente = 0
                        pBePedidoEnc.Anulado = False
                        pBePedidoEnc.IdPickingEnc = 0
                        pBePedidoEnc.RoadKilometraje = 0
                        pBePedidoEnc.RoadFechaEntr = BeINavPedTrasladoEnc.Shipment_Date
                        pBePedidoEnc.RoadDirEntrega = ""
                        pBePedidoEnc.RoadTotal = 0
                        pBePedidoEnc.RoadDesMonto = 0
                        pBePedidoEnc.RoadImpMonto = 0
                        pBePedidoEnc.RoadPeso = 0
                        pBePedidoEnc.RoadBandera = 0
                        pBePedidoEnc.RoadStatCom = ""
                        pBePedidoEnc.RoadCalcoBJ = 0
                        pBePedidoEnc.RoadImpres = 0
                        pBePedidoEnc.RoadADD1 = ""
                        pBePedidoEnc.RoadADD2 = ""
                        pBePedidoEnc.RoadADD3 = ""
                        pBePedidoEnc.RoadStatProc = 0
                        pBePedidoEnc.RoadRechazado = 0
                        pBePedidoEnc.RoadRazon_Rechazado = 0
                        pBePedidoEnc.RoadInformado = 0
                        pBePedidoEnc.RoadSucursal = ""
                        pBePedidoEnc.RoadIdDespacho = 0
                        pBePedidoEnc.RoadIdFacturacion = 0

                        If Not BeRoadRuta Is Nothing Then
                            pBePedidoEnc.RoadIdRuta = BeRoadRuta.IdRuta
                        Else

                            If Not BeINavPedTrasladoEnc.RoadCodigoRuta Is Nothing Then

                                If Not BeINavPedTrasladoEnc.RoadCodigoRuta.Trim = "" Then

                                    BeRoadRuta = clsLnRoad_ruta.Get_IdRuta_By_Codigo(BeINavPedTrasladoEnc.RoadCodigoRuta,
                                                                                    lConectionInterface,
                                                                                    lTransInterface)

                                End If

                                If Not BeRoadRuta Is Nothing Then
                                    pBePedidoEnc.RoadIdRuta = BeRoadRuta.IdRuta
                                Else
                                    pBePedidoEnc.RoadIdRuta = 0
                                End If

                            End If

                        End If

                        If Not BeRoadVendedor Is Nothing Then
                            pBePedidoEnc.RoadIdVendedor = BeRoadVendedor.IdVendedor
                        Else

                            If Not BeINavPedTrasladoEnc.RoadCodigoVendedor Is Nothing Then

                                If Not BeINavPedTrasladoEnc.RoadCodigoVendedor.Trim = "" Then

                                    BeRoadVendedor = clsLnRoad_p_vendedor.Get_Vendedor_By_Codigo(BeINavPedTrasladoEnc.RoadCodigoVendedor,
                                                                                                 lConectionInterface,
                                                                                                 lTransInterface)

                                End If

                                If Not BeRoadVendedor Is Nothing Then
                                    pBePedidoEnc.RoadIdVendedor = BeRoadVendedor.IdVendedor
                                Else
                                    pBePedidoEnc.RoadIdVendedor = 0
                                End If

                            End If

                        End If

                        pBePedidoEnc.RoadIdRutaDespacho = 0
                        pBePedidoEnc.RoadIdVendedorDespacho = 0
                        pBePedidoEnc.Enviado_A_ERP = False
                        '#EJC20190711: Se utilizará para generar el pedido de ingreso ( pedido de compra) en la bodega  a la que se le envía el despacho.
                        pBePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino = BeINavPedTrasladoEnc.Receipt_Document_Reference

                        Asegurar_Cliente_Bodega_Activo(pBePedidoEnc.IdCliente,
                                                       pBePedidoEnc.IdBodega,
                                                       CStr(BeConfigEnc.IdUsuario),
                                                       lConectionInterface,
                                                       lTransInterface)

                        clsLnTrans_pe_enc.Inserta_Encabezado(pBePedidoEnc,
                                                             lConectionInterface,
                                                             lTransInterface)

                        pClienteTiemposList = clsLnCliente_tiempos.Get_All_Tiempos_By_IdCliente(pBePedidoEnc.IdCliente,
                                                                                                lConectionInterface,
                                                                                                lTransInterface)


                        For Each PDet In BeINavPedTrasladoEnc.Lineas_Detalle

                            vCodigoProducto = PDet.Item_No

                            BeProducto = New clsBeProducto()
                            BeProducto.Codigo = PDet.Item_No
                            BeProducto = clsLnProducto.Get_BeProducto_By_Codigo(BeProducto.Codigo,
                                                                               IdBodegaOrigen,
                                                                               lConectionInterface,
                                                                               lTransInterface)


                            If BeProducto Is Nothing Then
                                Dim vMsgEx1 As String = "El código de producto: " & PDet.Item_No & " no existe o no está asociado con el código de bodega: " & IdBodegaOrigen
                                clsPublic.Actualizar_Progreso(lblprg, vMsgEx1)
                                Throw New Exception(vMsgEx1)
                            End If

                            BeUnidadMedida = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(PDet.Unit_of_Measure_Code,
                                                                                                   BeConfigEnc.IdPropietario,
                                                                                                   lConectionInterface,
                                                                                                   lTransInterface)

                            If BeUnidadMedida Is Nothing Then
                                Dim vMsgEx2 As String = "La U.M básica de producto: " & PDet.Item_No & " no existe o no está definida: " & PDet.Unit_of_Measure_Code
                                clsPublic.Actualizar_Progreso(lblprg, vMsgEx2)
                                Throw New Exception(vMsgEx2)
                            Else
                                BeProducto.UnidadMedida = BeUnidadMedida
                            End If

                            If PDet.Variant_Code IsNot Nothing Then

                                BePresentacion = New clsBeProducto_Presentacion
                                BePresentacion = clsLnProducto_presentacion.Get_Presentacion_By_IdProductoBodega_And_CodPres(BeProducto.IdProductoBodega,
                                                                                                                             PDet.Variant_Code.Replace(".", ""),
                                                                                                                             lConectionInterface,
                                                                                                                             lTransInterface)

                                If BePresentacion Is Nothing Then
                                    vMsgEx3 = "La Presentacion de producto: " & PDet.Item_No & " no existe o no está definida: " & PDet.Unit_of_Measure_Code
                                    clsPublic.Actualizar_Progreso(lblprg, vMsgEx3)
                                    Throw New Exception(vMsgEx3)
                                End If

                            Else
                                '#EJC20190530: Estan pidiendo en UMBAS.
                                BePresentacion = Nothing
                            End If

                            vClienteTiempo = pClienteTiemposList.Find(Function(x) _
                                                                      x.IdClasificacion = BeProducto.Clasificacion.IdClasificacion _
                                                                      AndAlso x.IdFamilia = BeProducto.Familia.IdFamilia)

                            vMsgEx3 = " El IdClasificación de producto es: " & BeProducto.Clasificacion.IdClasificacion &
                                      " El IdFamilia es: " & BeProducto.Familia.IdFamilia

                            If Not vClienteTiempo Is Nothing Then
                                vDiasVencimientoCliente = vClienteTiempo.Dias_Local
                            End If

                            If TrasladoExistente Is Nothing Then

                                Try

                                    clsPublic.Actualizar_Progreso(lblprg, "Reservando existencias para producto: " & PDet.Item_No)

                                    If Inserta_Linea_Detalle_Pedido(pBePedidoEnc,
                                                                    PDet,
                                                                    BeProducto,
                                                                    vDiasVencimientoCliente,
                                                                    BeUnidadMedida,
                                                                    BePresentacion,
                                                                    BeCliente,
                                                                    BeConfigEnc,
                                                                    IdBodegaOrigen,
                                                                    IdPropietarioBodegaOrigen,
                                                                    BeNavEjecucionEnc.IdEjecucionEnc,
                                                                    lblprg,
                                                                    lConectionInterface,
                                                                    lTransInterface) Then

                                        PDet.Status = 1
                                        PDet.Process_Result = "Ok"
                                        clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet,
                                                                                          lConectionInterface,
                                                                                          lTransInterface)
                                        vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                    Else

                                        PDet.Status = 0

                                        clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet,
                                                                                          lConectionInterface,
                                                                                          lTransInterface)

                                        '#EJC202303011454: Log de errores de WMS.
                                        Dim vMotivoNoReserva As String = Obtener_Motivo_No_Reserva(PDet,
                                                                                                    "No hay existencia aplicable válida para la solicitud después de evaluar presentación, ubicación, vencimiento y reservas vigentes.")
                                        Dim vMensajeEx As String = String.Format(vbNewLine & "ERROR_202310021910: No se pudo completar la reserva para el pedido: {0} Línea: {1} Código_Producto: {3} U.M.: {4} V.C.: {5} Descripción del error: {2} Cantidad: {6} ", PDet.NoEnc,
                                                                                PDet.Line_No,
                                                                                vMotivoNoReserva,
                                                                                PDet.Item_No,
                                                                                PDet.Unit_of_Measure_Code,
                                                                                PDet.Variant_Code,
                                                                                PDet.Quantity)

                                        PDet.Process_Result = Formatear_Process_Result_No_Reserva(vMotivoNoReserva)
                                        clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(PDet,
                                                                                              lConectionInterface,
                                                                                              lTransInterface)

                                        Dim BeEmpresa As New clsBeEmpresa
                                        BeEmpresa = clsLnEmpresa.GetSingle_By_IdBodega(IdBodegaOrigen,
                                                                                       lConectionInterface,
                                                                                       lTransInterface)

                                        Dim BeMensajeErrorWMS As New clsBeLog_error_wms
                                        BeMensajeErrorWMS.IdError = clsLnLog_error_wms.MaxID() + 1
                                        BeMensajeErrorWMS.IdEmpresa = BeEmpresa.IdEmpresa
                                        BeMensajeErrorWMS.IdBodega = IdBodegaOrigen
                                        BeMensajeErrorWMS.Fecha = Now
                                        BeMensajeErrorWMS.MensajeError = "Error_202303011638A: " & vMensajeEx
                                        BeMensajeErrorWMS.Line_No = PDet.Line_No
                                        BeMensajeErrorWMS.UmBas = PDet.Unit_of_Measure_Code
                                        BeMensajeErrorWMS.Variant_Code = PDet.Variant_Code
                                        BeMensajeErrorWMS.Cantidad = PDet.Quantity
                                        BeMensajeErrorWMS.Referencia_Documento = pBePedidoEnc.Referencia
                                        BeMensajeErrorWMS.Item_No = PDet.Item_No
                                        clsLnLog_error_wms.Insertar(BeMensajeErrorWMS)

                                        clsPublic.Actualizar_Progreso(lblprg, vMensajeEx)

                                    End If

                                Catch ex As Exception
                                    PDet.Status = 0
                                    PDet.Process_Result = ex.Message
                                    clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet, lConectionInterface, lTransInterface)
                                    Throw ex
                                End Try

                            Else 'es un pedido existente.

                                'Si la línea de detalle no existe
                                If Not clsLnTrans_pe_det.Existe(TrasladoExistente.IdPedidoEnc, PDet.Line_No,
                                                                pBePedidoDet,
                                                                PDet.No,
                                                                lConectionInterface,
                                                                lTransInterface) Then

                                    Try

                                        If Inserta_Linea_Detalle_Pedido(pBePedidoEnc,
                                                                        PDet,
                                                                        BeProducto,
                                                                        vDiasVencimientoCliente,
                                                                        BeUnidadMedida,
                                                                        BePresentacion,
                                                                        BeCliente,
                                                                        BeConfigEnc,
                                                                        IdBodegaOrigen,
                                                                        IdPropietarioBodegaOrigen,
                                                                        BeNavEjecucionEnc.IdEjecucionEnc,
                                                                        lblprg,
                                                                        lConectionInterface,
                                                                        lTransInterface) Then

                                            PDet.Status = 1
                                            PDet.Process_Result = "Ok"
                                            clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet,
                                                                                              lConectionInterface,
                                                                                              lTransInterface)

                                            vContador_Lineas_Detalle_Pedido_Insertadas += 1

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("línea nueva: {0} agregada a pedido existente: {1} ", PDet.Line_No, PDet.NoEnc),
                                                                                       PDet.No,
                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                       BeNavEjecucionRes.IdNavConfigDet)

                                            Dim vMensaje As String = String.Format("línea nueva: {0} agregada a pedido existente: {1} ", PDet.Line_No, PDet.NoEnc)
                                            clsPublic.Actualizar_Progreso(lblprg, vMensaje)

                                        Else

                                            '#EJC202303011454: Log de errores de WMS.
                                            Dim vMotivoNoReserva As String = Obtener_Motivo_No_Reserva(PDet,
                                                                                                        "No hay existencia aplicable válida para la solicitud después de evaluar presentación, ubicación, vencimiento y reservas vigentes.")
                                            Dim vMensajeEx As String = String.Format(vbNewLine & "ERROR_202310021911: Al reservar stock para el pedido: {0} Línea: {1} Código_Producto: {3} U.M.: {4} V.C.: {5} Descripción del error: {2} Cantidad: {6}", PDet.NoEnc,
                                                                                    PDet.Line_No,
                                                                                    vMotivoNoReserva,
                                                                                    PDet.Item_No,
                                                                                    PDet.Unit_of_Measure_Code,
                                                                                    PDet.Variant_Code,
                                                                                    PDet.Quantity)

                                            PDet.Process_Result = Formatear_Process_Result_No_Reserva(vMotivoNoReserva)

                                            clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(PDet,
                                                                                                  lConectionInterface,
                                                                                                  lTransInterface)

                                            Dim BeEmpresa As New clsBeEmpresa
                                            BeEmpresa = clsLnEmpresa.GetSingle_By_IdBodega(IdBodegaOrigen)

                                            Dim BeMensajeErrorWMS As New clsBeLog_error_wms
                                            BeMensajeErrorWMS.IdError = clsLnLog_error_wms.MaxID() + 1
                                            BeMensajeErrorWMS.IdEmpresa = BeEmpresa.IdEmpresa
                                            BeMensajeErrorWMS.IdBodega = pBePedidoEnc.IdBodega
                                            BeMensajeErrorWMS.Fecha = Now
                                            BeMensajeErrorWMS.MensajeError = "Error_202303011638: " & vMensajeEx
                                            BeMensajeErrorWMS.Line_No = PDet.Line_No
                                            BeMensajeErrorWMS.UmBas = PDet.Unit_of_Measure_Code
                                            BeMensajeErrorWMS.Variant_Code = PDet.Variant_Code
                                            BeMensajeErrorWMS.Cantidad = PDet.Quantity
                                            BeMensajeErrorWMS.Referencia_Documento = pBePedidoEnc.Referencia
                                            BeMensajeErrorWMS.Item_No = PDet.Item_No
                                            clsLnLog_error_wms.Insertar(BeMensajeErrorWMS)

                                        End If

                                    Catch ex As Exception
                                        PDet.Status = 0
                                        PDet.Process_Result = ex.Message
                                        clsLnI_nav_ped_traslado_det.Actualizar_Status_Det(PDet, lConectionInterface, lTransInterface)
                                        Throw New Exception(String.Format("#ERROR_202112271234: {0} ", ex.Message))
                                    End Try

                                Else

                                    If pBePedidoDet.Cantidad <> PDet.Quantity Then

                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(
                                                    String.Format("El pedido: {0} exite,
                                                                la línea de detalle: {1} existe, 
                                                                cantidad_origen <> cantidad_destino
                                                                no se puede actualizar (aún)", PDet.NoEnc, PDet.Line_No),
                                                                PDet.No,
                                                                BeNavEjecucionEnc.IdEjecucionEnc,
                                                                BeNavEjecucionRes.IdNavConfigDet)

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido: {0} exite,
                                                                        la línea de detalle: {1} existe, 
                                                                        cantidad_origen <> cantidad_destino
                                                                        no se puede actualizar (aún)", PDet.NoEnc, PDet.Line_No))

                                    Else

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido: {0} exite,
                                                                        la línea de detalle: {1} existe, 
                                                                        cantidad_origen = cantidad_destino
                                                                        no se actualizará", PDet.NoEnc, PDet.Line_No))
                                    End If

                                End If

                            End If 'fin TrasladoExistente

                        Next

                        Try

                            '#EJC20180712: No se insertó ninguna línea de detalle del pedido
                            'Eliminar el encabezado.
                            If vContador_Lineas_Detalle_Pedido_Insertadas = 0 Then

                                Try

                                    If Not clsLnTrans_pe_enc.Tiene_Detalle(pBePedidoEnc.IdPedidoEnc,
                                                                           lConectionInterface,
                                                                           lTransInterface) Then

                                        clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                                     lConectionInterface,
                                                                                     lTransInterface)

                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido {0} de traslado no tiene líneas de detalle válidas para el WMS y se eliminará la cabecera: {1}", BeINavPedTrasladoEnc.No, vbNewLine))

                                    Else
                                        '#EJC202310191429
                                        clsLnTrans_pe_det.Eliminar_Detalle_By_IdPedidoEnc(pBePedidoEnc.IdPedidoEnc,
                                                                                          lConectionInterface,
                                                                                          lTransInterface)


                                        clsLnTrans_pe_enc.Eliminar_Encabezado_Pedido(pBePedidoEnc.IdPedidoEnc,
                                                                                     lConectionInterface,
                                                                                     lTransInterface)


                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("El pedido {0} de traslado no tiene líneas de detalle válidas para el WMS y se eliminará la cabecera: {1}", BeINavPedTrasladoEnc.No, vbNewLine))

                                    End If

                                Catch ex As Exception
                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("#202112271242: Error al eliminar cabecera de pedido de transferencia sin detalle : {0} {1}", ex.Message, vbNewLine))
                                End Try


                            ElseIf vContador_Lineas_Detalle_Pedido_Insertadas = BeINavPedTrasladoEnc.Lineas_Detalle.Count Then
                                Importar_Traslado_Tabla_Intermedia_A_WMS = True
                            Else
                                If BeConfigEnc.Rechazar_pedido_incompleto Then
                                    Dim vMensajeError20230301 As String = String.Format("vContador_Lineas_Detalle_Pedido_Insertadas: " & vContador_Lineas_Detalle_Pedido_Insertadas & " BeINavPedTrasladoEnc.Lineas_Detalle.Count: " & BeINavPedTrasladoEnc.Lineas_Detalle.Count)
                                    clsLnLog_error_wms.Agregar_Error(vMensajeError20230301)
                                    '#EJC202303011403: aquí se debería de rechazar el pedido, hay que cambiar esta validación de líneas o corregirla.
                                    Throw New Exception(vMensajeError20230301)
                                End If
                            End If

                        Catch ex As Exception
                            Throw ex
                        End Try

                    End If

                End If

            Else
                clsPublic.Actualizar_Progreso(lblprg, String.Format("PT Inactivo {0} ", BeINavPedTrasladoEnc.No, vbNewLine))
            End If

            vContador += 1

            VContadorBitacoraTOMWMS = vContador

            If Importar_Traslado_Tabla_Intermedia_A_WMS Then
                clsPublic.Actualizar_Progreso(lblprg, String.Format("Pedidos de traslado procesados correctamente: {0}", VContadorBitacoraTOMWMS))
            End If

            Dim difSegundos As Double = DateDiff(DateInterval.Second, BeNavEjecucionEnc.Fecha, Now)
            clsPublic.Actualizar_Progreso(lblprg, String.Format("Tiempo transcurrido: {0} segundo(s)", difSegundos))

            BeNavEjecucionRes.Registros_ti = VContadorBitacoraIntermedia
            BeNavEjecucionRes.Registros_WMS = VContadorBitacoraTOMWMS

            If VContadorBitacoraIntermedia = VContadorBitacoraTOMWMS Then
                BeNavEjecucionRes.Exitosa = True
            Else
                BeNavEjecucionRes.Exitosa = False
            End If

            clsLnI_nav_ejecucion_res.Actualizar(BeNavEjecucionRes)

        Catch ex As Exception

            Dim vListaMensajeError As String = ""

            If Not pBePedidoEnc Is Nothing Then
                lLogErrorWMS = clsLnLog_error_wms.Get_All_By_Referencia_Documento(pBePedidoEnc.Referencia)
            Else
                lLogErrorWMS = clsLnLog_error_wms.Get_All_By_Referencia_Documento(BeINavPedTrasladoEnc.No)
            End If

            If Not lLogErrorWMS Is Nothing Then
                If lLogErrorWMS.Count > 0 Then
                    For Each Lwms In lLogErrorWMS
                        vListaMensajeError += vbNewLine & Lwms.MensajeError
                    Next
                End If
            End If

            Dim vMensajeError As String = ex.Message

            '#EJC20171105_1259AM_REF01: Agregar excepción a log.
            clsLnI_nav_ejecucion_det_error.Inserta_Log(vMensajeError.Trim(),
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       BeConfigDet.Idnavconfigdet)

            clsPublic.Actualizar_Progreso(lblprg, vMensajeError)

            If vListaMensajeError = "" Then
                Throw ex
            Else
                Throw New Exception(vListaMensajeError)
            End If

        End Try

    End Function

    Private Shared Function Inserta_Linea_Detalle_Pedido(ByVal BePedidoEnc As clsBeTrans_pe_enc,
                                                         ByRef pBeTrasladoDet As clsBeI_nav_ped_traslado_det,
                                                         ByVal pBePoducto As clsBeProducto,
                                                         ByVal pDiasVencimientoCliente As Integer,
                                                         ByVal pBeUnidadMedida As clsBeUnidad_medida,
                                                         ByVal pBePresentacion As clsBeProducto_Presentacion,
                                                         ByVal pBeCliente As clsBeCliente,
                                                         ByVal pBeConfigEnc As clsBeI_nav_config_enc,
                                                         ByVal pIdBodegaOrigen As Integer,
                                                         ByVal pIdPropietarioBodega As Integer,
                                                         ByRef plblprg As RichTextBox,
                                                         ByRef lConectionInterface As SqlConnection,
                                                         ByRef lTransactionInterface As SqlTransaction,
                                                         ByRef BePedidoDet As clsBeTrans_pe_det) As Boolean

        Inserta_Linea_Detalle_Pedido = False

        Dim pBePedidoDet As New clsBeTrans_pe_det
        Dim pBeStockRes As New clsBeStock_res
        Dim IdNavConfigDet As Integer = 102 'Pedidos de clientes
        Dim IdxPresentacion As Integer = -1

        BePedidoDet = Nothing

        Try

            Dim BeBodega As New clsBeBodega
            BeBodega = clsLnBodega.GetSingle_By_Idbodega(pBeConfigEnc.Idbodega,
                                                         lConectionInterface,
                                                         lTransactionInterface)

            pBePedidoDet = New clsBeTrans_pe_det
            pBePedidoDet.IdPedidoDet = 0 'EJC20260226: En recepción automática en destino, el detalle de recepción se va creando a medida que se van procesando las líneas de despacho, por lo tanto no se tiene un IdRecepcionDet definido al momento de crear el objeto.
            pBePedidoDet.No_linea = pBeTrasladoDet.Line_No
            pBePedidoDet.Atributo_Variante_1 = pBeTrasladoDet.Variant_Code
            pBePedidoDet.IdPedidoEnc = BePedidoEnc.IdPedidoEnc
            pBePedidoDet.Producto = New clsBeProducto
            pBePedidoDet.Producto.IdProducto = clsLnProducto.Get_Id_Producto_By_IdProductoBodega(pBePoducto.IdProductoBodega,
                                                                                                 lConectionInterface,
                                                                                                 lTransactionInterface)
            pBePedidoDet.Producto.IdProductoBodega = pBePoducto.IdProductoBodega
            pBePedidoDet.IdProductoBodega = pBePoducto.IdProductoBodega
            pBePedidoDet.Codigo_Producto = pBeTrasladoDet.Item_No
            pBePedidoDet.Producto.Codigo = pBeTrasladoDet.Item_No
            '#EJC20220622:Quitar caracteres no permitidos.
            pBePedidoDet.Producto.Nombre = clsPublic.Quitar_Caracteres_No_Permitidos(pBeTrasladoDet.Description)
            pBePedidoDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(pBeTrasladoDet.Description)
            pBePedidoDet.IdUnidadMedidaBasica = pBeUnidadMedida.IdUnidadMedida
            pBePedidoDet.Cantidad = pBeTrasladoDet.Quantity
            pBePedidoDet.Peso = 0
            pBePedidoDet.Precio = pBeTrasladoDet.Price
            pBePedidoDet.No_recepcion = 0
            pBePedidoDet.Cant_despachada = 0
            pBePedidoDet.IdEstado = pBeConfigEnc.IdProductoEstado
            pBePedidoDet.Ndias = pDiasVencimientoCliente
            pBePedidoDet.Nom_estado = "Buen Estado"
            pBePedidoDet.IsNew = True
            pBePedidoDet.Fec_agr = Now
            pBePedidoDet.User_agr = pBeConfigEnc.IdUsuario
            pBePedidoDet.RoadDes = 0
            pBePedidoDet.RoadDesMon = 0
            pBePedidoDet.RoadPrecioDoc = pBeTrasladoDet.Price
            pBePedidoDet.RoadTotal = Math.Round(pBeTrasladoDet.Price * pBeTrasladoDet.Quantity, 6)
            pBePedidoDet.RoadVAL1 = 0
            pBePedidoDet.RoadVAL2 = 0
            pBePedidoDet.Talla = pBeTrasladoDet.Size
            pBePedidoDet.Color = pBeTrasladoDet.Color

            Dim BeProductoTallaColor = Nothing

            If BeBodega.Control_Talla_Color Then

                BeProductoTallaColor = clsLnProducto_talla_color.Get_Single_By_IdProducto(pBePedidoDet.Producto.IdProducto,
                                                                                          pBeTrasladoDet.Size,
                                                                                          pBeTrasladoDet.Color,
                                                                                          lConectionInterface,
                                                                                          lTransactionInterface)

                If Not BeProductoTallaColor Is Nothing Then
                    pBePedidoDet.IdProductoTallaColor = BeProductoTallaColor.IdProductoTallaColor
                End If

            End If

            If Not pBePresentacion Is Nothing Then
                If pBePresentacion.IdPresentacion <> 0 Then
                    pBePedidoDet.Nom_presentacion = pBePresentacion.Nombre
                    pBePedidoDet.IdPresentacion = pBePresentacion.IdPresentacion
                Else
                    pBePedidoDet.Nom_presentacion = ""
                End If
            Else
                pBePedidoDet.Nom_presentacion = ""
            End If

            pBePedidoDet.Nom_unid_med = pBeTrasladoDet.Unit_of_Measure_Code
            pBePedidoDet.Nom_estado = "Buen Estado"
            pBeStockRes.IdStockRes = 0
            pBeStockRes.IdTransaccion = BePedidoEnc.IdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.Indicador = "PED"
            pBeStockRes.añada = 0
            pBeStockRes.Cantidad = pBeTrasladoDet.Quantity
            pBeStockRes.Estado = "PPC"
            pBePedidoDet.Ndias = pDiasVencimientoCliente
            pBeStockRes.User_agr = pBeConfigEnc.IdUsuario
            pBeStockRes.Fec_agr = Now
            pBeStockRes.User_mod = pBeConfigEnc.IdUsuario
            pBeStockRes.Fec_mod = Now
            pBeStockRes.Host = "Interface"

            Dim BePtc = clsLnProducto_talla_color.Get_Single_By_IdProductoBodega(pBePoducto.IdProductoBodega, pBeStockRes.Talla, pBeStockRes.Color)
            If Not BePtc Is Nothing Then
                If BePtc.IdProductoTallaColor <> pBePedidoDet.IdProductoTallaColor Then
                    Throw New Exception("Discrepancia entre la asignación de talla y color")
                End If
            End If
            If Not BeProductoTallaColor Is Nothing Then pBeStockRes.IdProductoTallaColor = pBePedidoDet.IdProductoTallaColor

            Dim vCantidadEnteraPres As Double = 0
            Dim vCantidadDecimalUMBas As Double = 0
            Dim vCantidadSolicitadaPedido As Double = 0

#Region "Procesar cantidades en fracción"

            If (pBeConfigEnc.Convertir_decimales_a_umbas = 1 AndAlso pBeConfigEnc.Interface_SAP) Then

                If pBePresentacion IsNot Nothing Then

                    If pBePresentacion.Factor > 0 Then

                        Dim cantidadDecimal As Decimal
                        If Decimal.TryParse(pBeTrasladoDet.Quantity, cantidadDecimal) Then

                            clsPublic.Split_Decimal(pBeTrasladoDet.Quantity, vCantidadEnteraPres, vCantidadDecimalUMBas)

                            '#EJC20190602_0137AM: Multiplicar la parte decimal por el factor, para obtener la cantidad de unidades de medida básica.
                            vCantidadDecimalUMBas = Math.Round(vCantidadDecimalUMBas * pBePresentacion.Factor)
                            vCantidadEnteraPres = vCantidadEnteraPres * pBePresentacion.Factor

                            If vCantidadEnteraPres > 0 Then
                                vCantidadSolicitadaPedido = vCantidadEnteraPres
                            Else
                                vCantidadSolicitadaPedido = vCantidadDecimalUMBas
                                pBeStockRes.Atributo_Variante_1 = Nothing
                                pBeStockRes.IdPresentacion = 0
                            End If

                        Else
                            vCantidadSolicitadaPedido = pBeTrasladoDet.Quantity
                        End If

                    Else
                        Throw New Exception("ERROR_202210251745: El factor es 0 para la presentación NO se puede inferir la conversión.")
                    End If

                Else
                    vCantidadSolicitadaPedido = pBeTrasladoDet.Quantity
                End If

            Else
                vCantidadSolicitadaPedido = pBeTrasladoDet.Quantity
            End If
#End Region

            Dim BeProductoEstadoList As New List(Of clsBeProducto_estado)
            Dim vIdPropietario As Integer = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(pIdBodegaOrigen,
                                                                                                                      pIdPropietarioBodega,
                                                                                                                      lConectionInterface,
                                                                                                                      lTransactionInterface)
            Try
                '#CKFK 20240723 Agregué esta funcionalidad para Clarispharma porque en el caso de ellos si van a poder sacar produdcto de cualquier área
                If Not BeBodega Is Nothing Then
                    If BeBodega.Interface_SAP AndAlso BeBodega.Restringir_Areas_SAP Then
                        pBeStockRes.IdProductoEstado = clsLnProducto_estado.Get_IdEstado_By_Codigo_Area(BePedidoEnc.Bodega_Origen,
                                                                                                        lConectionInterface,
                                                                                                        lTransactionInterface)
                    Else
                        '#EJC202220620:Buscar el estado de producto de la interface.
                        Dim vIdEstadoProductoInterface As Integer = pBeConfigEnc.IdProductoEstado

                        BeProductoEstadoList = clsLnProducto_estado.Existe_IdEstado_By_IdPropietario(vIdPropietario,
                                                                                                     vIdEstadoProductoInterface,
                                                                                                     lConectionInterface,
                                                                                                     lTransactionInterface)

                        If Not BeProductoEstadoList Is Nothing Then

                            If Not BeProductoEstadoList.FirstOrDefault() Is Nothing Then
                                pBeStockRes.IdProductoEstado = BeProductoEstadoList.FirstOrDefault.IdEstado()
                            Else
                                Throw New Exception("ERR_202205121200A: Error al obtener el estado de producto por defecto para los parámetros IdPropietario: " & pIdPropietarioBodega & " and IdBodega: " & pIdBodegaOrigen)
                            End If

                        Else
                            Throw New Exception("ERR_202205121200B: Error al obtener el estado de producto por defecto para los parámetros IdPropietario: " & pIdPropietarioBodega & " and IdBodega: " & pIdBodegaOrigen)
                        End If

                    End If
                End If

            Catch ex As Exception
                Throw New Exception("ERES_TU: " & ex.Message)
            End Try

            pBeStockRes.IdPedido = BePedidoEnc.IdPedidoEnc
            pBeStockRes.IdPedidoDet = pBePedidoDet.IdPedidoDet
            pBeStockRes.IdProductoBodega = pBePoducto.IdProductoBodega

            '#EJC20220512: 'clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(pIdBodega,pIdPropietarioBodega,lConectionInterface,lTransactionInterface)
            pBeStockRes.IdPropietarioBodega = pIdPropietarioBodega
            pBeStockRes.IdUnidadMedida = clsLnProducto.Get_Id_Unidad_Medida_By_Codigo(pBePedidoDet.Producto.Codigo,
                                                                                      lConectionInterface,
                                                                                      lTransactionInterface)
            '#CKFK20260322 Agregué esta validación porque queda mal guardado el stock res
            If pBeStockRes.IdUnidadMedida <> pBePedidoDet.IdUnidadMedidaBasica Then
                pBeStockRes.IdUnidadMedida = pBePedidoDet.IdUnidadMedidaBasica
            End If

            pBeStockRes.Atributo_Variante_1 = pBePedidoDet.Atributo_Variante_1

            '#EJC20190314: Asignar control ultimo lote a objeto de reserva.
            pBeStockRes.Control_Ultimo_Lote = pBeCliente.Control_Ultimo_Lote

            Dim BePresentacion As New clsBeProducto_Presentacion

            If pBePedidoDet.IdPresentacion <> 0 Then

                If Not pBePedidoDet.Atributo_Variante_1 Is Nothing Then

                    '#CKFK20240724 Cambié la función Existe_By_IdProducto_And_Codigo 
                    BePresentacion = New clsBeProducto_Presentacion
                    BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(pBePedidoDet.Producto.IdProducto,
                                                                                              pBePedidoDet.Atributo_Variante_1,
                                                                                              lConectionInterface,
                                                                                              lTransactionInterface)

                    If Not BePresentacion Is Nothing Then
                        pBeStockRes.IdPresentacion = BePresentacion.IdPresentacion
                    Else
                        pBeStockRes.IdPresentacion = 0 'No se encontró la presentación solicitada
                    End If

                Else
                    pBeStockRes.IdPresentacion = 0 'No se encontró la presentación solicitada
                End If

            End If

            pBeStockRes.IdProductoTallaColor = pBePedidoDet.IdProductoTallaColor
            pBeStockRes.Talla = pBePedidoDet.Talla
            pBeStockRes.Color = pBePedidoDet.Color

            If vCantidadDecimalUMBas > 0 Then
                pBeStockRes.Cantidad = vCantidadSolicitadaPedido
                pBeStockRes.IdPresentacion = 0
            End If

            If pBeStockRes.Control_Ultimo_Lote Then
                '#EJC20190314: Capturar último lote despachado para evitar enviar el mismo.
                pBeStockRes.Ultimo_Lote = clsLnVW_Despacho_Rep.Get_Ultimo_Lote_By_IdCliente(pBeCliente.IdCliente,
                                                                                            pBePedidoDet.Producto.IdProducto)
            End If

            '#EJC20220712_0853:Asignar la ubicación con la que se va a abastecer el pedido de un determinado cliente.
            'MI3: (Quedaría pendiente validar si la ubicación que trae es válida, pero eso que lo haga otro... que está viendo mi pantalla.
            If Val(pBeCliente.IdUbicacionAbastecerCon) <> 0 Then
                pBeStockRes.IdUbicacionAbastecerCon = pBeCliente.IdUbicacionAbastecerCon
            Else
                pBeStockRes.IdUbicacionAbastecerCon = 0
            End If

            Try

                If clsLnTrans_pe_det.Reservar_Stock_Por_Linea_Interface(pDiasVencimientoCliente,
                                                                        pBeTrasladoDet,
                                                                        pBePedidoDet,
                                                                        pBeStockRes,
                                                                        "Interface",
                                                                        pBeConfigEnc,
                                                                        pIdPropietarioBodega,
                                                                        lConectionInterface,
                                                                        lTransactionInterface) Then
                    Inserta_Linea_Detalle_Pedido = True

                    pBeTrasladoDet.Process_Result = "Ok"
                    clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                          lConectionInterface,
                                                                          lTransactionInterface)

                Else

                    Dim vMensajeEx As String = ""
                    Dim vMotivoNoReserva As String = Obtener_Motivo_No_Reserva(pBeTrasladoDet,
                                                                                "No hay existencia aplicable válida para la solicitud después de evaluar presentación, ubicación, vencimiento y reservas vigentes.")

                    Dim tieneTallaOColor As Boolean = Not String.IsNullOrWhiteSpace(pBeTrasladoDet.Size) OrElse
                                                     Not String.IsNullOrWhiteSpace(pBeTrasladoDet.Color)

                    If pBeStockRes.IdUbicacionAbastecerCon = 0 Then
                        If tieneTallaOColor Then
                            vMensajeEx = String.Format(vbNewLine &
                                                    "Reserva fallida. Pedido {0}, línea {1}: {2} (T: {3}, C: {4} IdTc:{5}). Motivo: {6}. Cant: {7}",
                                                    pBeTrasladoDet.NoEnc,
                                                    pBeTrasladoDet.Line_No,
                                                    pBeTrasladoDet.Item_No,
                                                    pBeTrasladoDet.Size,
                                                    pBeTrasladoDet.Color,
                                                    pBeStockRes.IdProductoTallaColor,
                                                    vMotivoNoReserva,
                                                    pBeTrasladoDet.Quantity)
                        Else
                            vMensajeEx = String.Format(vbNewLine &
                                                        "Reserva fallida. Pedido {0}, línea {1}: {2}. Motivo: {3}. Cant: {4}",
                                                        pBeTrasladoDet.NoEnc,
                                                        pBeTrasladoDet.Line_No,
                                                        pBeTrasladoDet.Item_No,
                                                        vMotivoNoReserva,
                                                        pBeTrasladoDet.Quantity)
                        End If
                    Else
                        If tieneTallaOColor Then
                            vMensajeEx = String.Format(vbNewLine &
                                                        "Reserva fallida. Pedido {0}, línea {1}: {2} (T: {3}, C: {4}) sin stock aplicable en ubicación {5}. Motivo: {6}. Cant: {7}",
                                                        pBeTrasladoDet.NoEnc,
                                                        pBeTrasladoDet.Line_No,
                                                        pBeTrasladoDet.Item_No,
                                                        pBeTrasladoDet.Size,
                                                        pBeTrasladoDet.Color,
                                                        pBeStockRes.IdUbicacionAbastecerCon,
                                                        vMotivoNoReserva,
                                                        pBeTrasladoDet.Quantity)
                        Else
                            vMensajeEx = String.Format(vbNewLine &
                                                        "Reserva fallida. Pedido {0}, línea {1}: {2} sin stock aplicable en ubicación {3}. Motivo: {4}. Cant: {5}",
                                                        pBeTrasladoDet.NoEnc,
                                                        pBeTrasladoDet.Line_No,
                                                        pBeTrasladoDet.Item_No,
                                                        pBeStockRes.IdUbicacionAbastecerCon,
                                                        vMotivoNoReserva,
                                                        pBeTrasladoDet.Quantity)
                        End If
                    End If

                    pBeTrasladoDet.Process_Result = vMensajeEx

                    clsPublic.Actualizar_Progreso(plblprg, vMensajeEx)

                    clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                          lConectionInterface,
                                                                          lTransactionInterface)

                End If

            Catch ex As Exception

                Dim vMensajeEx As String = String.Format(vbNewLine & "{0}{1}{2}{2}{2}{2} Documento:{7} línea:{3} U.M: {5} V.C: {6}",
                                                         ex.Message,
                                                         vbNewLine,
                                                         vbTab,
                                                         pBeTrasladoDet.Line_No,
                                                         pBeTrasladoDet.Item_No,
                                                         pBeTrasladoDet.Unit_of_Measure_Code,
                                                         pBeTrasladoDet.Variant_Code,
                                                         BePedidoEnc.Referencia)

                pBeTrasladoDet.Process_Result = vMensajeEx

                clsLnI_nav_ped_traslado_det.Actualizar_Process_Result(pBeTrasladoDet,
                                                                      lConectionInterface,
                                                                      lTransactionInterface)

                clsPublic.Actualizar_Progreso(plblprg, vMensajeEx)

                If pBeConfigEnc.Rechazar_pedido_incompleto Then
                    Throw New Exception(vMensajeEx)
                End If

            End Try

            '#EJC202409041648: Referencia en memoria del pedidodet para poderlo revertir después.
            BePedidoDet = pBePedidoDet

        Catch ex As Exception
            Dim st As New StackTrace(True)
            st = New StackTrace(ex, True)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function
    Public Shared Function Importar_Pedido_Cliente_A_Tabla_Intermedia(ByRef BePedidoCliente As clsBeI_nav_ped_traslado_enc,
                                                                      ByVal lblprg As RichTextBox,
                                                                      Optional ByRef lConnection As SqlConnection = Nothing,
                                                                      Optional ByRef lTransaction As SqlTransaction = Nothing) As clsBeTrans_pe_enc

        Importar_Pedido_Cliente_A_Tabla_Intermedia = Nothing

        Dim Es_Transaccion_Remota As Boolean = Not (lConnection Is Nothing AndAlso lTransaction Is Nothing)

        Dim LocalConnection As SqlConnection = Nothing
        Dim LocalTransaction As SqlTransaction = Nothing
        Dim vIdBodegaOrigen As Integer = 0
        Dim vIdPropietario As Integer = 0
        Dim vIdPropitarioBodegaOrigen As Integer = 0
        Dim vIdxConfig As Integer = -1
        Dim vIndicadorDeExcepcion As Integer = 0
        Dim logString As String = ""

        Try
            If Not Es_Transaccion_Remota Then
                LocalConnection = New SqlConnection(ConfigurationManager.AppSettings("CST"))
                LocalConnection.Open()
                LocalTransaction = LocalConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lConnection = LocalConnection
                lTransaction = LocalTransaction
            End If

            Dim BeBodegaArea As clsBeBodega_area = clsLnBodega_area.Get_Single_By_Codigo_Bodega(BePedidoCliente.Transfer_from_Code, lConnection, lTransaction)

            vIdBodegaOrigen = clsLnBodega.Get_IdBodega_By_Codigo(BePedidoCliente.Transfer_from_Code, lConnection, lTransaction)
            vIndicadorDeExcepcion = 1

            If vIdBodegaOrigen = 0 Then
                If BeBodegaArea IsNot Nothing Then
                    vIdBodegaOrigen = BeBodegaArea.IdBodega
                Else
                    Throw New Exception(String.Format("El código de la bodega origen: {0} no es válido para la solicitud de traslado, 
                                                       es posible que sea una transferencia de ingreso.", BePedidoCliente.Transfer_from_Code))
                End If
            End If

            vIdPropietario = clsLnPropietarios.Get_IdPropietario_By_Codigo(BePedidoCliente.Product_Owner_Code, lConnection, lTransaction)

            If vIdPropietario = 0 Then
                Throw New Exception(String.Format("El código de propietario: ({0}) no es válido", BePedidoCliente.Product_Owner_Code))
            End If

            vIndicadorDeExcepcion = 2

            vIdPropitarioBodegaOrigen = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(vIdPropietario, vIdBodegaOrigen, lConnection, lTransaction)

            If vIdPropitarioBodegaOrigen = 0 Then
                Throw New Exception(String.Format("El código de propietario: ({0}) de la bodega origen: ({1}) no es válido", BePedidoCliente.Product_Owner_Code, BePedidoCliente.Transfer_from_Code))
            End If

            vIndicadorDeExcepcion = 3

            If Importar_Pedido_Cliente_A_Tabla_Intermedia_Bool(BePedidoCliente, lblprg, lConnection, lTransaction) Then

                vIndicadorDeExcepcion = 4

                vIdxConfig = lBeConfigInMemory.FindIndex(Function(x) x.Idbodega = vIdBodegaOrigen AndAlso x.IdPropietario = vIdPropietario)

                Dim BeConfigEnc As clsBeI_nav_config_enc = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario(vIdBodegaOrigen, vIdPropietario, lConnection, lTransaction)

                If BeConfigEnc Is Nothing Then
                    Dim vMsgEx As String = "ERROR_202310311436: No existe la configuración asociada a la bodega: " & vIdBodegaOrigen & " en la tabla i_nav_config_enc configure los parámetros por defecto para la interfaz"
                    clsPublic.Actualizar_Progreso(lblprg, vMsgEx)
                    Throw New Exception(vMsgEx)
                Else
                    vIndicadorDeExcepcion = 5

                    Dim BePedidoEnc As clsBeTrans_pe_enc = Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS(BePedidoCliente,
                                                                                                   vIdBodegaOrigen,
                                                                                                   vIdPropitarioBodegaOrigen,
                                                                                                   BeConfigEnc,
                                                                                                   lConnection,
                                                                                                   lTransaction,
                                                                                                   lblprg)

                    If BePedidoEnc IsNot Nothing Then
                        Importar_Pedido_Cliente_A_Tabla_Intermedia = BePedidoEnc
                    End If

                End If

            End If

            If Not Es_Transaccion_Remota Then
                LocalTransaction.Commit()
            End If

        Catch ex As Exception

            clsPublic.Actualizar_Progreso(lblprg, ex.Message, False)

            If Not Es_Transaccion_Remota AndAlso LocalTransaction IsNot Nothing Then
                LocalTransaction.Rollback()
            ElseIf Es_Transaccion_Remota Then
                Throw ex
            End If
            Throw ex

        Finally
            If Not Es_Transaccion_Remota AndAlso LocalConnection IsNot Nothing AndAlso LocalConnection.State = ConnectionState.Open Then
                LocalConnection.Close()
            End If
        End Try

    End Function

    Public Shared Function Exist_By_No_And_Company(ByVal pNo As String,
                                                   ByVal pCompany As String,
                                                   ByVal pTipoDocumento As clsDataContractDI.tTipoDocumentoSalida,
                                                   ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As Boolean

        Exist_By_No_And_Company = False

        Try

            Const sp As String = "SELECT No FROM i_nav_ped_traslado_enc
                                 Where(No = @No AND Document_Type = @Document_Type AND Company_Code = @Company_Code)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", pNo))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Document_Type", pTipoDocumento))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Company_Code", pCompany))

            Dim dt As New DataTable
            dad.Fill(dt)

            Exist_By_No_And_Company = dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Nuevo_Picking(pBePedidoEnc As clsBeTrans_pe_enc,
                                         pEstadoPicking As String,
                                         lConnection As SqlConnection,
                                         lTransaction As SqlTransaction) As Boolean

        Nuevo_Picking = False

        Dim lPayload As New List(Of clsBePickingOnProcess)

        Try
            Dim pBeCliente = clsLnCliente.GetSingle(pBePedidoEnc.Cliente.IdCliente, lConnection, lTransaction)
            If pBeCliente Is Nothing Then
                Throw New Exception($"Cliente no encontrado: {pBePedidoEnc.Cliente.IdCliente}")
            End If

            Dim pBePropietarioBodega = clsLnPropietario_bodega.Get_Single_With_Propietario_By_IdPropietarioBodega(
            pBePedidoEnc.PropietarioBodega.IdPropietarioBodega)

            ' Procesar cada detalle del pedido
            For Each BePedidoDet As clsBeTrans_pe_det In pBePedidoEnc.Detalle
                BePedidoDet.ListaStockRes = clsLnTrans_pe_det.Get_All_Stock_Res_By_IdPedidoDet(
                BePedidoDet.IdPedidoDet, BePedidoDet.IdPedidoEnc, lConnection, lTransaction)

                Dim singlePayload = SetProductoPicking(BePedidoDet, pBePedidoEnc, lConnection, lTransaction)

                If singlePayload IsNot Nothing Then
                    lPayload.Add(singlePayload)
                End If

                Application.DoEvents()
            Next

            ' Separar las listas usando LINQ
            Dim listaPickingDet = lPayload.Where(Function(p) p.PickingDet IsNot Nothing) _
                                     .Select(Function(p) p.PickingDet) _
                                     .ToList()

            Dim listaPickingUbic = lPayload.Where(Function(p) p.PickingUbic IsNot Nothing) _
                                      .SelectMany(Function(p) p.PickingUbic) _
                                      .ToList()

            ' Llamar a Guardar_Picking
            If Guardar_Picking(pBePedidoEnc, listaPickingDet, listaPickingUbic, pEstadoPicking, lConnection, lTransaction) Then
                Nuevo_Picking = True
            End If

        Catch ex As Exception
            Dim errorMsg = $"Error en Nuevo_Picking para pedido {pBePedidoEnc?.IdPedidoEnc}: {ex.Message}"
            clsLnLog_error_wms.Agregar_Error(errorMsg)
            Throw New Exception(errorMsg, ex)
        End Try

        Return Nuevo_Picking
    End Function

    Public Class clsBePickingOnProcess
        Public Property PickingDet As clsBeTrans_picking_det
        Public Property PickingUbic As List(Of clsBeTrans_picking_ubic)

        Public Sub New()
            PickingUbic = New List(Of clsBeTrans_picking_ubic)()
        End Sub

        Public Sub New(pickingDet As clsBeTrans_picking_det, ubicaciones As List(Of clsBeTrans_picking_ubic))
            Me.PickingDet = pickingDet
            Me.PickingUbic = If(ubicaciones, New List(Of clsBeTrans_picking_ubic)())
        End Sub
    End Class

    Public Shared Function SetProductoPicking(ByVal pBeTransPeDet As clsBeTrans_pe_det,
                                              ByVal PedidoEnc As clsBeTrans_pe_enc,
                                              ByVal pConnection As SqlConnection,
                                              ByVal pTransaction As SqlTransaction) As clsBePickingOnProcess

        If PedidoEnc Is Nothing Then
            Throw New ArgumentNullException(NameOf(PedidoEnc), "El encabezado del pedido no puede ser nulo")
        End If

        If pBeTransPeDet Is Nothing Then
            Throw New ArgumentNullException(NameOf(pBeTransPeDet), "El detalle del pedido no puede ser nulo")
        End If

        Dim resultados As New clsBePickingOnProcess
        Dim bePickingDet As clsBeTrans_picking_det = Nothing
        Dim pListPickingUbic As New List(Of clsBeTrans_picking_ubic)()

        Try
            ' 1. Inicializar objeto picking detalle
            bePickingDet = InicializarPickingDetalle(pBeTransPeDet, PedidoEnc)

            ' 2. Obtener y validar producto
            Dim producto As clsBeProducto = ObtenerProductoValidado(pBeTransPeDet, pConnection, pTransaction)

            ' 3. Configurar información del producto en el picking
            ConfigurarProductoEnPicking(bePickingDet, pBeTransPeDet, producto, pConnection, pTransaction)

            ' 4. Procesar stock reservado
            If pBeTransPeDet.ListaStockRes?.Count > 0 Then
                pListPickingUbic = ProcesarStockReservado(pBeTransPeDet, bePickingDet, PedidoEnc, pConnection, pTransaction)
            Else
                Throw New Exception($"No hay stock reservado para la línea {pBeTransPeDet.No_linea} - Producto: {pBeTransPeDet.Codigo_Producto}")
            End If

            ' 5. Crear resultado
            resultados.PickingDet = bePickingDet
            resultados.PickingUbic = pListPickingUbic

            Return resultados

        Catch ex As Exception
            Dim metodoActual = MethodBase.GetCurrentMethod().Name
            Dim mensajeError = $"{metodoActual}: {ex.Message}"
            clsLnLog_error_wms.Agregar_Error(mensajeError)
            Throw New Exception(mensajeError, ex)
        End Try
    End Function

    ' Métodos auxiliares para modularizar la funcionalidad
    Private Shared Function InicializarPickingDetalle(pBeTransPeDet As clsBeTrans_pe_det, PedidoEnc As clsBeTrans_pe_enc) As clsBeTrans_picking_det
        Return New clsBeTrans_picking_det With {
        .IdPedidoEnc = pBeTransPeDet.IdPedidoEnc,
        .IdPedidoDet = pBeTransPeDet.IdPedidoDet,
        .Cantidad = pBeTransPeDet.Cantidad,
        .Cantidad_recibida = 0,
        .User_agr = PedidoEnc.User_agr,
        .Fec_agr = DateTime.Now,
        .User_mod = PedidoEnc.User_agr,
        .Fec_mod = DateTime.Now,
        .Activo = True,
        .IsNew = True,
        .Cliente_dias = 0,
        .Presentacion = New clsBeProducto_Presentacion(),
        .UnidadMedida = New clsBeUnidad_medida(),
        .ProductoEstado = New clsBeProducto_estado(),
        .Producto = New clsBeProducto()
    }
    End Function

    Private Shared Function ObtenerProductoValidado(pBeTransPeDet As clsBeTrans_pe_det,
                                                    ByVal pConnection As SqlConnection,
                                                    ByVal pTransaction As SqlTransaction) As clsBeProducto
        Dim producto As clsBeProducto = pBeTransPeDet.Producto

        ' Si el producto es nulo o no tiene ID válido, intentar obtenerlo
        If producto Is Nothing OrElse producto.IdProducto = 0 Then
            If pBeTransPeDet.IdProductoBodega <> 0 Then
                producto = clsLnProducto.Get_Single_By_IdProductoBodega(pBeTransPeDet.IdProductoBodega, pConnection, pTransaction)
            End If

            If producto Is Nothing OrElse producto.IdProducto = 0 Then
                Throw New Exception($"No se encontró el producto para la línea {pBeTransPeDet.No_linea} - Código: {pBeTransPeDet.Codigo_Producto}")
            End If

            ' Actualizar referencia en el detalle
            pBeTransPeDet.Producto = producto
        End If

        Return producto
    End Function

    Private Shared Sub ConfigurarProductoEnPicking(bePickingDet As clsBeTrans_picking_det,
                                                   pBeTransPeDet As clsBeTrans_pe_det,
                                                   producto As clsBeProducto,
                                                   ByVal pConnection As SqlConnection,
                                                   ByVal pTransaction As SqlTransaction)

        ' Obtener datos completos del producto
        Dim productoCompleto = clsLnProducto.Get_Single_By_IdProducto(producto.IdProducto, pConnection, pTransaction)
        If productoCompleto Is Nothing Then
            Throw New Exception($"No se pudo obtener información completa del producto ID: {producto.IdProducto}")
        End If

        ' Configurar propiedades del picking
        bePickingDet.Producto.Codigo = productoCompleto.Codigo
        bePickingDet.Producto.Nombre = productoCompleto.Nombre
        bePickingDet.Codigo = productoCompleto.Codigo
        bePickingDet.NombreProducto = productoCompleto.Nombre
        bePickingDet.Presentacion.IdPresentacion = pBeTransPeDet.IdPresentacion
        bePickingDet.Presentacion.Nombre = pBeTransPeDet.Nom_presentacion
        bePickingDet.UnidadMedida.IdUnidadMedida = pBeTransPeDet.IdUnidadMedidaBasica
        bePickingDet.UnidadMedida.Nombre = pBeTransPeDet.Nom_unid_med
        bePickingDet.ProductoEstado.IdEstado = pBeTransPeDet.IdEstado
        bePickingDet.ProductoEstado.Nombre = pBeTransPeDet.Nom_estado
        bePickingDet.IdPedidoEnc = pBeTransPeDet.IdPedidoEnc
        bePickingDet.IdPickingEnc = bePickingDet.IdPickingEnc
    End Sub

    Private Shared Function ProcesarStockReservado(pBeTransPeDet As clsBeTrans_pe_det,
                                                  bePickingDet As clsBeTrans_picking_det,
                                                  PedidoEnc As clsBeTrans_pe_enc,
                                                   ByVal pConnection As SqlConnection,
                                                   ByVal pTransaction As SqlTransaction) As List(Of clsBeTrans_picking_ubic)

        Dim ubicaciones As New List(Of clsBeTrans_picking_ubic)()
        Dim presentacionCache As New Dictionary(Of Integer, clsBeProducto_Presentacion)()

        For Each stockRes In pBeTransPeDet.ListaStockRes
            ValidarStockReservado(stockRes)

            Dim ubicacion = CrearPickingUbicacion(stockRes, bePickingDet, PedidoEnc, presentacionCache, pConnection, pTransaction)
            ubicaciones.Add(ubicacion)
        Next

        Return ubicaciones
    End Function

    Private Shared Sub ValidarStockReservado(stockRes As clsBeStock_res)
        If stockRes.Cantidad = 0 Then
            Throw New Exception(
            $"Stock inconsistente - IdStock: {stockRes.IdStock} reporta cantidad: {stockRes.Cantidad}. " &
            "Se ha restringido el picking de este documento por seguridad.")
        End If
    End Sub

    Private Shared Function CrearPickingUbicacion(stockRes As clsBeStock_res,
                                                 bePickingDet As clsBeTrans_picking_det,
                                                 PedidoEnc As clsBeTrans_pe_enc,
                                                 presentacionCache As Dictionary(Of Integer, clsBeProducto_Presentacion),
                                                  ByVal pConnection As SqlConnection,
                                                  ByVal pTransaction As SqlTransaction) As clsBeTrans_picking_ubic

        Dim ubicacion = New clsBeTrans_picking_ubic With {
        .IdPedidoEnc = stockRes.IdPedido,
        .IdPedidoDet = stockRes.IdPedidoDet,
        .IdStockRes = stockRes.IdStockRes,
        .IdPickingDet = bePickingDet.IdPickingDet,
        .IdStock = stockRes.IdStock,
        .IdPropietarioBodega = stockRes.IdPropietarioBodega,
        .IdProductoBodega = stockRes.IdProductoBodega,
        .IdProductoEstado = stockRes.IdProductoEstado,
        .IdPresentacion = stockRes.IdPresentacion,
        .IdUnidadMedida = stockRes.IdUnidadMedida,
        .IdUbicacionAnterior = If(stockRes.Ubicacion_ant IsNot Nothing, Convert.ToInt32(stockRes.Ubicacion_ant), 0),
        .IdRecepcion = stockRes.IdRecepcion,
        .IdUbicacion = stockRes.IdUbicacion,
        .Lote = stockRes.Lote,
        .Fecha_Vence = stockRes.Fecha_vence,
        .Serial = stockRes.Serial,
        .Lic_plate = stockRes.Lic_plate,
        .Peso_solicitado = stockRes.Peso,
        .IdBodega = PedidoEnc.IdBodega,
        .Cantidad_Recibida = 0.0,
        .Fecha_real_vence = stockRes.Fecha_vence,
        .User_agr = PedidoEnc.User_agr,
        .Fec_agr = DateTime.Now,
        .User_mod = PedidoEnc.User_agr,
        .Fec_mod = DateTime.Now,
        .Activo = True,
        .IsNew = True,
        .IdProductoTallaColor = stockRes.IdProductoTallaColor
    }

        ' Calcular cantidad solicitada considerando presentación
        ubicacion.Cantidad_Solicitada = CalcularCantidadConPresentacion(stockRes, presentacionCache, pConnection, pTransaction)

        Return ubicacion
    End Function

    Private Shared Function CalcularCantidadConPresentacion(stockRes As clsBeStock_res,
                                                            presentacionCache As Dictionary(Of Integer, clsBeProducto_Presentacion),
                                                            ByVal pConnection As SqlConnection,
                                                            ByVal pTransaction As SqlTransaction) As Decimal

        If stockRes.IdPresentacion = 0 Then
            Return stockRes.Cantidad
        End If

        ' Usar cache para evitar consultas repetidas
        If Not presentacionCache.ContainsKey(stockRes.IdPresentacion) Then
            Dim presentacion = clsLnProducto_presentacion.GetSingle(stockRes.IdPresentacion, pConnection, pTransaction)
            If presentacion Is Nothing Then
                Throw New Exception($"No se encontró la presentación ID: {stockRes.IdPresentacion}")
            End If
            presentacionCache(stockRes.IdPresentacion) = presentacion
        End If

        Dim factor = presentacionCache(stockRes.IdPresentacion).Factor
        If factor = 0 Then factor = 1

        Return stockRes.Cantidad / factor
    End Function

    Private Shared Function Guardar_Picking(ByVal BePedidoEnc As clsBeTrans_pe_enc,
                                            ByVal BeListPickingDet As List(Of clsBeTrans_picking_det),
                                            ByVal pListBePickingUbic As List(Of clsBeTrans_picking_ubic),
                                            ByVal pEstadoPicking As String,
                                            lConnection As SqlConnection,
                                            lTransaction As SqlTransaction) As Boolean

        Dim BePickingEnc As New clsBeTrans_picking_enc

        Guardar_Picking = False

        Try

            Dim BeTipoPedido As New clsBeTrans_pe_tipo
            BeTipoPedido = clsLnTrans_pe_tipo.Get_Single_By_IdTipoPedido(BePedidoEnc.IdTipoPedido)

            If (BePedidoEnc.IdTipoPedido = clsDataContractDI.tTipoDocumentoSalida.Transferencia_Interna_WMS AndAlso
                               BePedidoEnc.Cliente.Codigo = BePedidoEnc.Bodega_Destino) Then
                BeTipoPedido.Verificar = True
            End If

            BePedidoEnc.TipoPedido = BeTipoPedido

            BePickingEnc.IdPickingEnc = 0
            BePickingEnc.IdBodega = BePedidoEnc.IdBodega
            BePickingEnc.IdPropietarioBodega = BePedidoEnc.IdPropietarioBodega
            BePickingEnc.IdUbicacionPicking = clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(BePedidoEnc.IdBodega, lConnection, lTransaction)
            BePickingEnc.Fecha_picking = Now
            BePickingEnc.Hora_ini = BePedidoEnc.Hora_ini
            BePickingEnc.Hora_fin = BePedidoEnc.Hora_fin
            BePickingEnc.Estado = pEstadoPicking
            BePickingEnc.User_agr = BePedidoEnc.User_agr
            BePickingEnc.User_mod = BePedidoEnc.User_agr
            BePickingEnc.Fec_mod = Now
            BePickingEnc.Detalle_operador = False
            BePickingEnc.Activo = True
            BePickingEnc.verifica_auto = BePedidoEnc.TipoPedido.Verificar
            BePickingEnc.procesado_bof = True
            BePickingEnc.Requiere_Preparacion = False
            BePickingEnc.Fotografia_Verificacion = False
            BePickingEnc.Estado_Preparacion = "N/A"
            BePickingEnc.Fecha_Inicio_Preparacion = New Date(1900, 1, 1)
            BePickingEnc.Fecha_Fin_Preparacion = New Date(1900, 1, 1)
            BePickingEnc.Tipo_Preparacion = ""
            BePickingEnc.Referencia = BePedidoEnc.Referencia
            BePickingEnc.IdBodegaMuelle = clsLnBodega_muelles.Get_IdMuelle_By_IdBodega(BePedidoEnc.IdBodega, lConnection, lTransaction)
            BePickingEnc.IdPrioridadPicking = 0
            BePickingEnc.IsNew = True
            BePickingEnc.IdPedidoEnc = BePedidoEnc.IdPedidoEnc

            If Not pListBePickingUbic.Count = 0 Then

                Dim BeListOp As New List(Of clsBeTrans_picking_op)
                Dim BeOp As New clsBeTrans_picking_op
                Dim vAsignarTodosOperadores As Boolean = BeTipoPedido.Asignar_Todos_Operadores

                If vAsignarTodosOperadores Then
                    Dim BeOperadoresList = clsLnOperador_bodega.Get_All_By_IdBodega_For_Tarea(BePickingEnc.IdBodega,
                                                                                              clsDataContractDI.tTipoTarea.PICK,
                                                                                              lConnection,
                                                                                              lTransaction)
                    For Each BeOperador As clsBeOperador_bodega In BeOperadoresList
                        BeOp = New clsBeTrans_picking_op
                        BeOp.IdOperadorPicking = clsLnTrans_picking_op.MaxID(lConnection, lTransaction) + 1
                        BeOp.IdPickingEnc = BePickingEnc.IdPickingEnc
                        BeOp.IdOperadorBodega = BeOperador.IdOperadorBodega
                        BeOp.User_agr = "MI3"
                        BeOp.Fec_agr = Now
                        BeOp.User_mod = "MI3"
                        BeOp.Fec_mod = Now
                        BeOp.IsNew = True
                        BeListOp.Add(BeOp)
                    Next
                Else
                    BeOp = clsLnTrans_picking_op.Get_BeOperador_Defecto_By_IdPickingEnc(BePickingEnc.IdBodega,
                                                                                    BePickingEnc.IdPickingEnc,
                                                                                    lConnection,
                                                                                    lTransaction)

                    BeListOp.Add(BeOp)
                End If

                If BeListOp.Count = 0 Then
                    If BePickingEnc IsNot Nothing AndAlso BePickingEnc.IdPickingEnc > 0 Then
                        BeListOp = clsLnTrans_picking_op.Get_All_By_IdPickingEnc(BePickingEnc.IdPickingEnc).ToList
                    Else
                        BeListOp = New List(Of clsBeTrans_picking_op)
                    End If
                End If

                Guardar_Picking = clsLnTrans_picking_enc.Guardar(BePickingEnc,
                                                                 Nothing,
                                                                 BeListPickingDet,
                                                                 Nothing,
                                                                 BeListOp,
                                                                 pListBePickingUbic,
                                                                 lConnection,
                                                                 lTransaction)
            Else
                Throw New Exception("Al parecer el picking no tiene líneas, no se podrá guardar la transacción.")
            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw
        End Try

    End Function

    Public Shared Function Importar_Pedido_Cliente_A_Tabla_Intermedia_Transac_WMS(ByRef BePedidoCliente As clsBeI_nav_ped_traslado_enc,
                                                                                  ByRef lblprg As Object,
                                                                                  Optional ByRef lConnection As SqlConnection = Nothing,
                                                                                  Optional ByRef lTransaction As SqlTransaction = Nothing) As clsBeTrans_pe_enc

        Importar_Pedido_Cliente_A_Tabla_Intermedia_Transac_WMS = Nothing

        Dim Es_Transaccion_Remota As Boolean = Not (lConnection Is Nothing AndAlso lTransaction Is Nothing)

        Dim LocalConnection As SqlConnection = Nothing
        Dim LocalTransaction As SqlTransaction = Nothing
        Dim vIdBodegaOrigen As Integer = 0
        Dim vIdPropietario As Integer = 0
        Dim vIdPropitarioBodegaOrigen As Integer = 0
        Dim vIdxConfig As Integer = -1
        Dim vIndicadorDeExcepcion As Integer = 0
        Dim logString As String = ""

        Try
            If Not Es_Transaccion_Remota Then
                LocalConnection = New SqlConnection(ConfigurationManager.AppSettings("CST"))
                LocalConnection.Open()
                LocalTransaction = LocalConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                lConnection = LocalConnection
                lTransaction = LocalTransaction
            End If

            Dim BeBodegaArea As clsBeBodega_area = clsLnBodega_area.Get_Single_By_Codigo_Bodega(BePedidoCliente.Transfer_from_Code, lConnection, lTransaction)

            vIdBodegaOrigen = clsLnBodega.Get_IdBodega_By_Codigo(BePedidoCliente.Transfer_from_Code, lConnection, lTransaction)
            vIndicadorDeExcepcion = 1

            If vIdBodegaOrigen = 0 Then
                If BeBodegaArea IsNot Nothing Then
                    vIdBodegaOrigen = BeBodegaArea.IdBodega
                Else
                    Throw New Exception(String.Format("El código de la bodega origen: {0} no es válido", BePedidoCliente.Transfer_from_Code))
                End If
            End If

            vIdPropietario = clsLnPropietarios.Get_IdPropietario_By_Codigo(BePedidoCliente.Product_Owner_Code, lConnection, lTransaction)

            If vIdPropietario = 0 Then
                Throw New Exception(String.Format("El código de propietario: ({0}) no es válido", BePedidoCliente.Product_Owner_Code))
            End If

            vIndicadorDeExcepcion = 2

            vIdPropitarioBodegaOrigen = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdPropietario_And_IdBodega(vIdPropietario, vIdBodegaOrigen, lConnection, lTransaction)

            If vIdPropitarioBodegaOrigen = 0 Then
                Throw New Exception(String.Format("El código de propietario: ({0}) de la bodega origen: ({1}) no es válido", BePedidoCliente.Product_Owner_Code, BePedidoCliente.Transfer_from_Code))
            End If

            vIndicadorDeExcepcion = 3

            If Importar_Traslado_A_Tabla_Intermedia(BePedidoCliente, lblprg, lConnection, lTransaction) Then

                vIndicadorDeExcepcion = 4

                vIdxConfig = lBeConfigInMemory.FindIndex(Function(x) x.Idbodega = vIdBodegaOrigen AndAlso x.IdPropietario = vIdPropietario)

                Dim BeConfigEnc As clsBeI_nav_config_enc = clsLnI_nav_config_enc.GetSingle_By_IdBodega_And_IdPropietario(vIdBodegaOrigen, vIdPropietario, lConnection, lTransaction)

                If BeConfigEnc Is Nothing Then
                    Dim vMsgEx As String = "ERROR_202310311436: No existe la configuración asociada a la bodega: " & vIdBodegaOrigen & " en la tabla i_nav_config_enc configure los parámetros por defecto para la interfaz"
                    clsPublic.Actualizar_Progreso(lblprg, vMsgEx)
                    Throw New Exception(vMsgEx)
                Else
                    vIndicadorDeExcepcion = 5

                    Dim BePedidoEnc As clsBeTrans_pe_enc = Imp_Ped_Trans_Env_Desde_Tab_Inter_A_WMS(BePedidoCliente,
                                                                                                   vIdBodegaOrigen,
                                                                                                   vIdPropitarioBodegaOrigen,
                                                                                                   BeConfigEnc,
                                                                                                   lConnection,
                                                                                                   lTransaction,
                                                                                                   lblprg)

                    If BePedidoEnc IsNot Nothing Then

                        Dim TieneStockRes As Boolean = clsLnStock_res.Tiene_StockRes_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc,
                                                                                                    BePedidoEnc.IdBodega,
                                                                                                    lConnection,
                                                                                                    lTransaction)
                        If TieneStockRes Then

                            BePedidoEnc.Detalle = clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc, lConnection, lTransaction)
                            '#EJC20251911: Terminar de afinar el método.

                            If Nuevo_Picking(BePedidoEnc, "Cerrado", lConnection, lTransaction) Then

                                clsPublic.Actualizar_Progreso(lblprg, String.Format("Picking creado para el documento: {0}/{1}{2}",
                                                                                     BePedidoEnc.Referencia, BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino, vbNewLine))

                                Dim pListBePickingUbic As List(Of clsBeTrans_picking_ubic) =
                                        clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc,
                                                                                                   BePedidoEnc.IdBodega,
                                                                                                   lConnection,
                                                                                                   lTransaction)

                                BePedidoEnc.IdPickingEnc = pListBePickingUbic.Item(0).IdPickingEnc

                                Dim BeListPickingDet As List(Of clsBeTrans_picking_det) =
                                    clsLnTrans_picking_det.Get_All_Picking_Det_By_IdPickingEnc(BePedidoEnc.IdPickingEnc,
                                                                                               lConnection,
                                                                                               lTransaction)

                                Dim BePickingEnc As clsBeTrans_picking_enc = Nothing
                                BePickingEnc = clsLnTrans_picking_enc.GetSingle(BePedidoEnc.IdPickingEnc,
                                                                            lConnection,
                                                                            lTransaction)
                                Dim pListBeStockRes As List(Of clsBeStock_res) =
                                    clsLnStock_res.Get_All_StockRes_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc,
                                                                                   lConnection,
                                                                                   lTransaction)

                                clsLnTrans_picking_ubic.Procesar_Picking_Desde_BOF(pListBePickingUbic,
                                                                                   BePedidoEnc.User_agr,
                                                                                   BeListPickingDet,
                                                                                   BePickingEnc,
                                                                                   pListBeStockRes,
                                                                                   lConnection,
                                                                                   lTransaction)

                                BePedidoEnc.Detalle = clsLnTrans_pe_det.Get_All_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc,
                                                                                           lConnection,
                                                                                           lTransaction)

                                For Each BePedidoDet As clsBeTrans_pe_det In BePedidoEnc.Detalle
                                    BePedidoDet.ListaPickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoDet(BePedidoDet.IdPedidoDet,
                                                                                                                          BePedidoEnc.IdPedidoEnc,
                                                                                                                          lConnection,
                                                                                                                          lTransaction)
                                Next

                                pListBePickingUbic = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPedidoEnc(BePedidoEnc.IdPedidoEnc,
                                                                                                                         BePedidoEnc.IdBodega,
                                                                                                                         lConnection,
                                                                                                                         lTransaction)

                                Dim despachado As Boolean = clsLnTrans_despacho_enc.Guardar_Despacho(pListBePickingUbic,
                                                                                                         BePedidoEnc,
                                                                                                         lConnection,
                                                                                                         lTransaction)

                                If Not despachado Then
                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Pedido: {0}/{1} no pudo ser despachado {2}",
                                                                                           BePedidoEnc.Referencia, BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino, vbNewLine))

                                    BePedidoEnc = Nothing
                                Else

                                    clsPublic.Actualizar_Progreso(lblprg, String.Format("Pedido: {0}/{1} despachado {2}",
                                                                                           BePedidoEnc.Referencia, BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino, vbNewLine))

                                    Importar_Pedido_Cliente_A_Tabla_Intermedia_Transac_WMS = BePedidoEnc

                                End If

                            End If

                        Else

                            clsPublic.Actualizar_Progreso(lblprg, String.Format("No se reservó inventario para el pedido: {0}/{1}{2}",
                                                                                     BePedidoEnc.Referencia, BePedidoEnc.Referencia_Documento_Ingreso_Bodega_Destino, vbNewLine))

                            Importar_Pedido_Cliente_A_Tabla_Intermedia_Transac_WMS = Nothing

                        End If

                    End If

                End If

            End If

            If Not Es_Transaccion_Remota Then
                LocalTransaction.Commit()
            End If

        Catch ex As Exception

            clsPublic.Actualizar_Progreso(lblprg, ex.Message, False)

            If Not Es_Transaccion_Remota AndAlso LocalTransaction IsNot Nothing Then
                LocalTransaction.Rollback()
            ElseIf Es_Transaccion_Remota Then
                Throw ex
            End If
            Throw ex

        Finally
            If Not Es_Transaccion_Remota AndAlso LocalConnection IsNot Nothing AndAlso LocalConnection.State = ConnectionState.Open Then
                LocalConnection.Close()
            End If
        End Try

    End Function
    Public Shared Function Importar_Traslado_A_Tabla_Intermedia(ByRef BePedidoCliente As clsBeI_nav_ped_traslado_enc,
                                                                ByRef lblprg As RichTextBox,
                                                                ByRef lConnection As SqlConnection,
                                                                ByRef lTransaction As SqlTransaction) As Boolean

        Dim IdNavConfigDet As Integer = 102 'Pedidos de clientes
        Dim BeNavEjecucionEnc As New clsBeI_nav_ejecucion_enc
        Dim IdxProductoBodegaInMemory As Integer = 0
        Dim vContadorLineas As Integer = 0
        Dim BeConfingEnc As New clsBeI_nav_config_enc

        Importar_Traslado_A_Tabla_Intermedia = False

        Try

            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim BeBodega As New clsBeBodega
            Dim vContador As Integer = 0

            BeNavEjecucionEnc.IdNavConfigEnc = 1
            BeNavEjecucionEnc.Fecha = Now
            '#EJCCKFK20260520: Cambio por Identity en tabla.
            clsLnI_nav_ejecucion_enc.Insertar(BeNavEjecucionEnc, lConnection, lTransaction)

            Try

                If Not BePedidoCliente.Company_Code = "" Then
                    If Not Exist_By_No_And_Company(BePedidoCliente.No, BePedidoCliente.Company_Code, BePedidoCliente.Document_Type, lConnection, lTransaction) Then
                        If BePedidoCliente.Company_Code.Length > 1 Then
                            'Si el código de la empresa es mayor a 1, se agrega el prefijo de la empresa al número del pedido.
                            BePedidoCliente.No = BePedidoCliente.Company_Code.Substring(0, 1) & BePedidoCliente.No
                        End If
                        Insertar(BePedidoCliente, lConnection, lTransaction)
                    End If
                ElseIf Not Exist(BePedidoCliente.No, BePedidoCliente.Document_Type, lConnection, lTransaction) Then
                    Insertar(BePedidoCliente, lConnection, lTransaction)
                End If

                clsPublic.Actualizar_Progreso(lblprg, "Encabezado de documento " & BePedidoCliente.External_Document_No & " guardado correctamente...")

                vContador += 1

                lTransaction.Save("Encabezado")

                Application.DoEvents()

                If Not BePedidoCliente.Lineas_Detalle Is Nothing Then

                    clsPublic.Actualizar_Progreso(lblprg, "Recorriendo detalle del documento " & BePedidoCliente.External_Document_No & "...")

                    For Each BeI_Nav_PedidoTrasladoDet As clsBeI_nav_ped_traslado_det In BePedidoCliente.Lineas_Detalle

                        Try

                            BeI_Nav_PedidoTrasladoDet.NoEnc = BePedidoCliente.No
                            BeI_Nav_PedidoTrasladoDet.No = BeI_Nav_PedidoTrasladoDet.Item_No
                            BeI_Nav_PedidoTrasladoDet.Variant_Code = BeI_Nav_PedidoTrasladoDet.Variant_Code

                            '#EJC20171106_1023AM_REF02: El valor nothing indica el final de la vista.
                            If Not BeI_Nav_PedidoTrasladoDet.Item_No Is Nothing Then

                                Dim BeBodegaArea As New clsBeBodega_area
                                BeBodegaArea = clsLnBodega_area.Get_Single_By_Codigo_Bodega(BePedidoCliente.Transfer_from_Code,
                                                                                            lConnection,
                                                                                            lTransaction)

                                '#CKFK20221108 Agregué esto para poder obtener la Bodega
                                BeBodega = New clsBeBodega
                                BeBodega = clsLnBodega.GetSingle_By_Codigo(BePedidoCliente.Transfer_from_Code,
                                                                           lConnection,
                                                                           lTransaction)

                                If BeBodega Is Nothing Then

                                    If Not BeBodegaArea Is Nothing Then

                                        BeBodega = clsLnBodega.GetSingle_By_Idbodega(BeBodegaArea.IdBodega, lConnection, lTransaction)

                                        If BeBodega Is Nothing Then
                                            clsPublic.Actualizar_Progreso(lblprg, "ERROR_20231031A: La bodega: " & BePedidoCliente.Transfer_from_Code & " no existe.")
                                            Throw New Exception("ERROR_20231031A: La bodega: " & BePedidoCliente.Transfer_from_Code & " no existe.")
                                        End If

                                    Else
                                        clsPublic.Actualizar_Progreso(lblprg, "ERROR_20231031B: La bodega: " & BePedidoCliente.Transfer_from_Code & " no existe.")
                                        Throw New Exception("ERROR_20231031B: La bodega: " & BePedidoCliente.Transfer_from_Code & " no existe.")
                                    End If

                                End If

                                BeConfingEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega(BeBodega.IdBodega, lConnection, lTransaction)

                                BeProductoBodega = New clsBeProducto_bodega()
                                BeProductoBodega = clsLnProducto_bodega.Existe(BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                               BeBodega.IdBodega,
                                                                               lConnection,
                                                                               lTransaction)
                                If BeProductoBodega Is Nothing Then
                                    If BeConfingEnc.Equiparar_Productos Then
                                        BeProductoBodega = clsLnProducto_bodega.Existe_Parte_By_IdBodega(BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                                                         BeBodega.IdBodega,
                                                                                                         lConnection,
                                                                                                         lTransaction)
                                        If BeProductoBodega Is Nothing Then
                                            BeProductoBodega = clsLnProducto_bodega.Existe_NoSerie_By_IdBodega(BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                                                               BeBodega.IdBodega,
                                                                                                               lConnection,
                                                                                                               lTransaction)
                                        End If

                                    Else

                                        Dim BeProducto As New clsBeProducto
                                        BeProducto = clsLnProducto.Existe(BeI_Nav_PedidoTrasladoDet.Item_No, lConnection, lTransaction)

                                        If BeProducto IsNot Nothing Then
                                            BeProductoBodega = New clsBeProducto_bodega With {
                                                .IdProductoBodega = clsLnProducto_bodega.MaxID(lConnection, lTransaction) + 1,
                                                .IdProducto = BeProducto.IdProducto,
                                                .IdBodega = BeBodega.IdBodega,
                                                .Activo = True,
                                                .User_agr = "MI3",
                                                .User_mod = "MI3",
                                                .Fec_agr = Now,
                                                .Fec_mod = Now
                                            }

                                            clsLnProducto_bodega.InsertarFromInterface(BeProductoBodega, lConnection, lTransaction)

                                        End If

                                    End If

                                End If

                                If Not BeProductoBodega Is Nothing Then
                                    lProductoBodegaInMemory.Add(BeProductoBodega.Clone())
                                Else
                                    If BeBodega.Control_Talla_Color Then
                                        clsPublic.Actualizar_Progreso(lblprg, "El producto: " & BeI_Nav_PedidoTrasladoDet.Item_No & " Talla: " & BeI_Nav_PedidoTrasladoDet.Size & " Color: " & BeI_Nav_PedidoTrasladoDet.Color & " No está asociado a la bodega: " & BePedidoCliente.Transfer_from_Code & " o no existe en el maestro de materiales.")
                                        Throw New Exception("El producto: " & BeI_Nav_PedidoTrasladoDet.Item_No & " Talla: " & BeI_Nav_PedidoTrasladoDet.Size & " Color: " & BeI_Nav_PedidoTrasladoDet.Color & " No está asociado a la bodega: " & BePedidoCliente.Transfer_from_Code & " o no existe en el maestro de materiales.")
                                    Else
                                        clsPublic.Actualizar_Progreso(lblprg, "El producto: " & BeI_Nav_PedidoTrasladoDet.Item_No & " No está asociado a la bodega: " & BePedidoCliente.Transfer_from_Code & " o no existe en el maestro de materiales.")
                                        Throw New Exception("El producto: " & BeI_Nav_PedidoTrasladoDet.Item_No & " No está asociado a la bodega: " & BePedidoCliente.Transfer_from_Code & " o no existe en el maestro de materiales.")
                                    End If
                                End If

                                'Existe el producto en el maestro?
                                If Not BeProductoBodega Is Nothing Then

                                    'Si Cantidad Recibida es <> 0 no se importa                                    
                                    If (BeI_Nav_PedidoTrasladoDet.Qty_to_Receive = 0) Then

                                        If clsLnI_nav_ped_traslado_det.Exist(BeI_Nav_PedidoTrasladoDet, lConnection, lTransaction) Then
                                            clsLnI_nav_ped_traslado_det.ActualizarFromIn(BeI_Nav_PedidoTrasladoDet, lConnection, lTransaction)
                                        Else
                                            clsLnI_nav_ped_traslado_det.Insertar(BeI_Nav_PedidoTrasladoDet, lConnection, lTransaction)
                                        End If

                                        vContadorLineas += 1
                                        clsPublic.Actualizar_Progreso(lblprg, String.Format("Se importó producto: {0}{1}", BeI_Nav_PedidoTrasladoDet.Item_No, vbNewLine))

                                    Else

                                        Try

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("Qty_to_Receive <> 0: No se importará, Qty_to_Receive debe ser 0 para procesar. ",
                                                                                       BeI_Nav_PedidoTrasladoDet.Item_No,
                                                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                                                       IdNavConfigDet, lConnection)
                                            clsPublic.Actualizar_Progreso(lblprg, String.Format("Qty_to_Receive <> 0: No se importará, Qty_to_Receive debe ser 0 para procesar. : {0}{1}", BeI_Nav_PedidoTrasladoDet.Item_No, vbNewLine))

                                        Catch ex As Exception
                                            Throw ex
                                        End Try

                                    End If 'Fin Si Qty_Ro_Receive =0

                                End If 'FIn Existe el producto en el maestro?

                            Else
                                Debug.Print("_: " & BeI_Nav_PedidoTrasladoDet.Description)
                            End If

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                     "Sin informacion",
                                                                     BeNavEjecucionEnc.IdEjecucionEnc,
                                                                     IdNavConfigDet)

                            Throw ex

                        End Try

                    Next

                Else
                    Console.WriteLine("Pedido de compra sin lineas de detalle?")
                    clsPublic.Actualizar_Progreso(lblprg, "Pedido de compra sin lineas de detalle")
                End If

            Catch ex As Exception

                clsPublic.Actualizar_Progreso(lblprg, ex.Message)
                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                          BePedidoCliente.No,
                                                          BeNavEjecucionEnc.IdEjecucionEnc,
                                                          IdNavConfigDet)

                Throw ex

            End Try

            Importar_Traslado_A_Tabla_Intermedia = (vContadorLineas > 0)

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       BeNavEjecucionEnc.IdEjecucionEnc,
                                                       IdNavConfigDet)
            clsPublic.Actualizar_Progreso(lblprg, ex.Message)

            Throw ex

        End Try

    End Function

End Class
