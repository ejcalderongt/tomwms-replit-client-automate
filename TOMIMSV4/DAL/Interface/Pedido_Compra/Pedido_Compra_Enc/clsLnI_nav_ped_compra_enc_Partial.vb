Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_ped_compra_enc

    Public Shared Function Exist(ByVal pNo As String)

        Exist = False

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT No FROM I_nav_ped_compra_enc 
			                      Where(No = @No)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", pNo))

            Dim dt As New DataTable
            dad.Fill(dt)

            Exist = dt.Rows.Count > 0

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Exist(ByVal pNo As String, ByVal lConnection As SqlConnection, ByVal lTrans As SqlTransaction)

        Exist = False

        Try

            Const sp As String = "SELECT No FROM I_nav_ped_compra_enc 
			 Where(No = @No)"

            Dim cmd As New SqlCommand(sp, lConnection, lTrans) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", pNo))

            Dim dt As New DataTable
            dad.Fill(dt)

            Exist = dt.Rows.Count > 0

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function GetAll(ByRef lConnection As SqlConnection,
                                  ByRef lTrans As SqlTransaction,
                                  ByRef lblprg As RichTextBox,
                                  ByRef prg As System.Windows.Forms.ProgressBar,
                                  Optional ByVal TransferenciasInternas As Boolean = False) As List(Of clsBeI_nav_ped_compra_enc)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ped_compra_enc)
            Dim sp As String = "SELECT * FROM I_nav_ped_compra_enc "

            If TransferenciasInternas Then
                sp += " WHERE Is_Internal_Transfer = 1"
            End If

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text, .Transaction = lTrans}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ped_compra_enc As New clsBeI_nav_ped_compra_enc

            For Each dr As DataRow In dt.Rows

                vBeI_nav_ped_compra_enc = New clsBeI_nav_ped_compra_enc
                Cargar(vBeI_nav_ped_compra_enc, dr)
                vBeI_nav_ped_compra_enc.Lineas_Detalle = clsLnI_nav_ped_compra_det.Get_All_By_NoEnc(lConnection, lTrans, vBeI_nav_ped_compra_enc.No)
                vBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes = clsLnI_nav_ped_compra_det_lote.GetAll(lConnection, lTrans, vBeI_nav_ped_compra_enc.No)
                lReturnList.Add(vBeI_nav_ped_compra_enc)

                lblprg.AppendText(vbNewLine)
                lblprg.AppendText(vbTab & String.Format("Llenando detalle Doc. Num: {0}", vBeI_nav_ped_compra_enc.No))
                lblprg.AppendText(vbNewLine)
                lblprg.Refresh()
                lblprg.SelectionStart = lblprg.TextLength
                lblprg.ScrollToCaret()

                Application.DoEvents()

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Insert_Multiple_Pedido_Compra_From_ERP(ByVal lPedidosCompra As List(Of clsBeI_nav_ped_compra_enc)) As Boolean

        Insert_Multiple_Pedido_Compra_From_ERP = False

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim BeTransOcEnc As New clsBeTrans_oc_enc
        Dim Result As New RichTextBox
        Dim vContador As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction()

            For Each BeI_nav_PedidoCompra In lPedidosCompra

                Try

                    If Datos_Validos(BeI_nav_PedidoCompra) Then

                        If Insert_Single_Pedido_From_ERP(BeI_nav_PedidoCompra, lConnection, lTransaction) Then

                            BeTransOcEnc = New clsBeTrans_oc_enc

                            Dim vResult As String = ""

                            If Procesar_Pedido_Compra_MI3(BeI_nav_PedidoCompra, BeTransOcEnc, Result.Text, Nothing, lConnection, lTransaction) Then
                                vContador += 1
                            End If

                        End If

                    End If


                Catch ex As Exception

                    clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                      BeI_nav_PedidoCompra.No,
                                                      0,
                                                      0)

                    Throw New Exception(String.Format("{0}_Enc {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

                End Try

            Next

            lTransaction.Commit()

            If lPedidosCompra.Count = vContador Then
                Insert_Multiple_Pedido_Compra_From_ERP = True
            End If

        Catch ex As Exception

            If lTransaction IsNot Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       0,
                                                       0)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Update_Pedido_Compra_From_ERP(ByRef oBeI_nav_ped_compra_enc As clsBeI_nav_ped_compra_enc) As Integer

        Update_Pedido_Compra_From_ERP = 0

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim RegistrosAfectados As Integer = 0

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            RegistrosAfectados = Actualizar(oBeI_nav_ped_compra_enc, lConnection, lTransaction)

            For Each Det In oBeI_nav_ped_compra_enc.Lineas_Detalle
                RegistrosAfectados += clsLnI_nav_ped_compra_det.Actualizar(Det, lConnection, lTransaction)
            Next

            For Each L In oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes
                RegistrosAfectados += clsLnI_nav_ped_compra_det_lote.Actualizar(L, lConnection, lTransaction)
            Next

            Update_Pedido_Compra_From_ERP = RegistrosAfectados

        Catch ex As Exception
            Throw New Exception(String.Format("{0}_Enc {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Insert_Single_Pedido_From_ERP(ByVal oBeI_nav_ped_compra_enc As clsBeI_nav_ped_compra_enc) As Integer

        Insert_Single_Pedido_From_ERP = 0

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim vContador As Integer = 0
            Dim RegistrosAfectados As Integer = 0
            Dim Bodega_Es_Valida_Para_Recepcion As Boolean = False
            Dim vIdBodega As Integer = 0

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Try

                'Insertar encabezado
                If Not Exist(oBeI_nav_ped_compra_enc.No, lConnection, lTransaction) Then
                    RegistrosAfectados += Insertar(oBeI_nav_ped_compra_enc, lConnection, lTransaction)
                Else
                    RegistrosAfectados += Actualizar(oBeI_nav_ped_compra_enc, lConnection, lTransaction)
                End If

                vContador += 1

                lTransaction.Save("Encabezado")

                'Insertar detalle
                If oBeI_nav_ped_compra_enc.Lineas_Detalle IsNot Nothing Then

                    For Each Det As clsBeI_nav_ped_compra_det In oBeI_nav_ped_compra_enc.Lineas_Detalle

                        Try

                            If Det.Type.ToString = "2" Then 'Es Producto                                                                

                                If Det.Location_Code IsNot Nothing Then

                                    '#EJC20190130: Validaciones agregadas para transferencias interbodega
                                    If Not oBeI_nav_ped_compra_enc.Is_Internal_Transfer Then
                                        'El código de cliente es válido?
                                        Bodega_Es_Valida_Para_Recepcion = clsLnCliente.Bodega_Es_Valida_Para_Recepcion(Det.Location_Code, lConnection, lTransaction)

                                        If Not Bodega_Es_Valida_Para_Recepcion Then
                                            Throw New Exception(String.Format("La bodega: {0} para el producto: {1} no se encuentra en la lista de bodegas válidas para recepción. 
                                                                               Mantenimientos->Cliene: Verifique que exista un cliente con el código: {0}", Det.Location_Code, Det.No))
                                        End If

                                    Else
                                        'El código de bodega es válido?
                                        Bodega_Es_Valida_Para_Recepcion = clsLnBodega.Exists_By_Codigo(Det.Location_Code, lConnection, lTransaction)

                                        If Not Bodega_Es_Valida_Para_Recepcion Then
                                            Throw New Exception(String.Format("La bodega: {0} para el producto: {1} no se encuentra en la lista de bodegas válidas para recepción. 
                                                                               Mantenimientos->Bodega: Verifique que exista una bodega con el código: {0}", Det.Location_Code, Det.No))
                                        End If

                                    End If

                                    If Bodega_Es_Valida_Para_Recepcion Then

                                        vIdBodega = clsLnBodega.Get_IdBodega_By_Codigo(Det.Location_Code, lConnection, lTransaction)

                                        If vIdBodega = 0 Then
                                            Throw New Exception("No se pudo obtener el identificador para la bodega: " & Det.Location_Code)
                                        End If

                                        BeProductoBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(Det.No,
                                                                                                          vIdBodega,
                                                                                                          lConnection,
                                                                                                          lTransaction)

                                        'Existe el producto en el maestro?
                                        If BeProductoBodega IsNot Nothing Then

                                            If Det.Quantity <> Det.Quantity_Received Then
                                                If clsLnI_nav_ped_compra_det.Exist(Det, lConnection, lTransaction) Then
                                                    RegistrosAfectados += clsLnI_nav_ped_compra_det.Actualizar(Det, lConnection, lTransaction)
                                                Else
                                                    RegistrosAfectados += clsLnI_nav_ped_compra_det.Insertar(Det, lConnection, lTransaction)
                                                End If
                                            End If

                                            If oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes IsNot Nothing Then
                                                If oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes.Count > 0 Then
                                                    For Each Lote In oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes
                                                        If Not clsLnI_nav_ped_compra_det_lote.Exist(Lote, lConnection, lTransaction) Then
                                                            RegistrosAfectados += clsLnI_nav_ped_compra_det_lote.Insertar(Lote, lConnection, lTransaction)
                                                        Else
                                                            RegistrosAfectados += clsLnI_nav_ped_compra_det_lote.Actualizar(Lote, lConnection, lTransaction)
                                                        End If
                                                    Next
                                                Else
                                                    If oBeI_nav_ped_compra_enc.Is_Internal_Transfer Then
                                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("El documento de transferencia interna No: {0}. No tiene definidos los lotes y fechas de vencimiento", Det.No), oBeI_nav_ped_compra_enc.No,
                                                                                        0,
                                                                                        0)
                                                        Throw New Exception("Error_202301191027A: El documento de transferencia interna no tiene definidos los lotes y fechas de vencimiento count(0). Is_Internal_Transfer = " & oBeI_nav_ped_compra_enc.Is_Internal_Transfer)
                                                    End If
                                                End If
                                            Else
                                                If oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes Is Nothing AndAlso oBeI_nav_ped_compra_enc.Is_Internal_Transfer Then
                                                    Throw New Exception(String.Format("El documento de transferencia interna No: {0}. No tiene definidos los lotes y fechas de vencimiento", Det.No))
                                                End If

                                            End If

                                        Else
                                            Throw New Exception(String.Format("El código de producto: {0} no existe en maestro o no está asociado a la bodega: {1}", Det.No, Det.Location_Code))
                                        End If 'Fin 'Existe el producto en el maestro?

                                    Else

                                        If lTransaction IsNot Nothing Then lTransaction.Rollback("Encabezado")

                                        Throw New Exception(String.Format("La bodega: {0} para el producto: {1} no se encuentra en la lista de bodegas válidas para recepción. 
                                                            Si es una transferencia interna: la bodega debe existir en el maestro de bodegas.
                                                            Si no es T.I. el código de bodega debe existir en maestro de clientes", Det.Location_Code, Det.No))

                                    End If 'Fin Bodega_Es_Valida_Para_Recepcion

                                Else

                                    If Det.No IsNot Nothing Then
                                        Throw New Exception(String.Format("No está definida bodega para producto: {0}, no se importará", Det.No))
                                    End If

                                End If 'Fin location code is nothing                                        

                            End If

                        Catch ex As Exception
                            Throw New Exception(String.Format(" (M) {0} -> {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                        End Try

                    Next

                    lTransaction.Commit()

                    Insert_Single_Pedido_From_ERP = RegistrosAfectados

                Else

                    Console.WriteLine("Pedido de compra sin lineas de detalle?")
                    Throw New Exception(String.Format("Pedido de compra No: {0} sin lineas de detalle", oBeI_nav_ped_compra_enc.No))

                End If

            Catch ex As Exception
                Throw New Exception(String.Format(" (M) {0}  -> {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
            End Try


        Catch ex As Exception

            If lTransaction IsNot Nothing Then lTransaction.Rollback()

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       oBeI_nav_ped_compra_enc.No,
                                                       0,
                                                       0)

            Throw New Exception(String.Format(" (M) {0} --> {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Insert_Single_Pedido_From_ERP(ByVal oBeI_nav_ped_compra_enc As clsBeI_nav_ped_compra_enc,
                                                         ByVal lConnection As SqlConnection,
                                                         ByVal lTransaction As SqlTransaction) As Boolean

        Insert_Single_Pedido_From_ERP = False

        Try

            Dim BeI_nav_PedidoCompraDet As clsBeI_nav_ped_compra_det
            Dim BeProductoBodega As New clsBeProducto_bodega
            Dim BeBodega As New clsBeBodega
            Dim vContador As Integer = 0
            Dim RegistrosAfectados As Integer = 0

            Try

                'Insertar encabezado
                If Not Exist(oBeI_nav_ped_compra_enc.No) Then
                    Insertar(oBeI_nav_ped_compra_enc, lConnection, lTransaction)
                Else
                    Actualizar(oBeI_nav_ped_compra_enc, lConnection, lTransaction)
                End If

                vContador += 1

                'Insertar detalle
                If oBeI_nav_ped_compra_enc.Lineas_Detalle IsNot Nothing Then

                    If oBeI_nav_ped_compra_enc.Lineas_Detalle.Count = 0 Then

                        Console.WriteLine("Pedido de compra sin lineas de detalle?")

                        clsLnI_nav_ejecucion_det_error.Inserta_Log("Pedido de compra sin lineas de detalle",
                                                                oBeI_nav_ped_compra_enc.No,
                                                                0,
                                                                0)

                        Throw New Exception(String.Format("Pedido de compra No: {0} sin lineas de detalle", oBeI_nav_ped_compra_enc.No))

                    End If

                    For Each Det In oBeI_nav_ped_compra_enc.Lineas_Detalle

                        BeI_nav_PedidoCompraDet = New clsBeI_nav_ped_compra_det

                        Try

                            BeI_nav_PedidoCompraDet.NoEnc = oBeI_nav_ped_compra_enc.No

                            If (Det.Type = "Item") OrElse (Det.Type.ToString = "2") Then 'Es Producto

                                If Det.Location_Code IsNot Nothing Then

                                    If clsLnCliente.Bodega_Es_Valida_Para_Recepcion(Det.Location_Code, lConnection, lTransaction) Then

                                        '#CKFK20221108 Agregué esto para poder obtener la Bodega
                                        BeBodega = New clsBeBodega
                                        BeBodega = clsLnBodega.GetSingle_By_Codigo(Det.Location_Code,
                                                                                   lConnection,
                                                                                   lTransaction)

                                        If BeBodega Is Nothing Then
                                            Throw New Exception("La bodega: " & Det.Location_Code & " no existe.")
                                        End If

                                        BeProductoBodega = clsLnProducto_bodega.Existe(Det.No,
                                                                                       BeBodega.IdBodega,
                                                                                       lConnection,
                                                                                       lTransaction)

                                        'Existe el producto en el maestro?
                                        If BeProductoBodega IsNot Nothing Then

                                            If Det.Quantity <> Det.Quantity_Received Then
                                                If clsLnI_nav_ped_compra_det.Exist(BeI_nav_PedidoCompraDet, lConnection, lTransaction) Then
                                                    clsLnI_nav_ped_compra_det.Actualizar(BeI_nav_PedidoCompraDet, lConnection, lTransaction)
                                                Else
                                                    clsLnI_nav_ped_compra_det.Insertar(BeI_nav_PedidoCompraDet, lConnection, lTransaction)
                                                End If
                                            End If

                                            If oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes IsNot Nothing Then
                                                If oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes.Count > 0 Then
                                                    For Each Lote In oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes
                                                        RegistrosAfectados += clsLnI_nav_ped_compra_det_lote.Insertar(Lote, lConnection, lTransaction)
                                                    Next
                                                Else
                                                    If oBeI_nav_ped_compra_enc.Is_Internal_Transfer Then
                                                        clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("El documento de transferencia interna No: {0}. No tiene definidos los lotes y fechas de vencimiento",
                                                                                                   Det.No),
                                                                                                   oBeI_nav_ped_compra_enc.No,
                                                                                                   0,
                                                                                                   0)
                                                        Throw New Exception("Error_202301191027: El documento de transferencia interna no tiene definidos los lotes y fechas de vencimiento count(0). Is_Internal_Transfer = " & oBeI_nav_ped_compra_enc.Is_Internal_Transfer)
                                                    End If
                                                End If
                                            Else
                                                If oBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes Is Nothing AndAlso oBeI_nav_ped_compra_enc.Is_Internal_Transfer Then
                                                    clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("El documento de transferencia interna No: {0}. No tiene definidos los lotes y fechas de vencimiento", Det.No), oBeI_nav_ped_compra_enc.No,
                                                                                        0,
                                                                                        0)
                                                    Throw New Exception(String.Format("El documento de transferencia interna No: {0}. No tiene definidos los lotes y fechas de vencimiento", Det.No))
                                                End If
                                            End If

                                        Else
                                            Try
                                                clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Producto: {0} no existe en maestro", Det.No), oBeI_nav_ped_compra_enc.No, 0, 0)
                                                Throw New Exception(String.Format("Producto: {0} no existe en maestro", Det.No))
                                            Catch ex As Exception
                                                Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                            End Try
                                        End If 'Fin 'Existe el producto en el maestro?

                                    Else

                                        Try

                                            '#EJC20180614: información no útil en log
                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("Producto: {0} no pertenece a lista de bodegas válidas para recepción", Det.No), oBeI_nav_ped_compra_enc.No,
                                            0,
                                            0)

                                            Throw New Exception(String.Format("Producto: {0} no pertenece a lista de bodegas válidas para recepción", Det.No))

                                        Catch ex As Exception
                                            clsLnI_nav_ejecucion_det_error.Inserta_Log("Errorx: " & ex.Message, oBeI_nav_ped_compra_enc.No,
                                            0,
                                            0)
                                            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                        End Try

                                    End If 'Fin Bodega_Es_Valida_Para_Recepcion

                                Else

                                    If Det.No IsNot Nothing Then

                                        Try

                                            clsLnI_nav_ejecucion_det_error.Inserta_Log(String.Format("No está definida bodega para producto: {0}, no se importará", Det.No), oBeI_nav_ped_compra_enc.No,
                                            0,
                                            0)

                                            Throw New Exception(String.Format("No está definida bodega para producto: {0}, no se importará", Det.No))

                                        Catch ex As Exception
                                            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
                                        End Try

                                    End If

                                End If 'Fin location code is nothing                                        

                            End If

                        Catch ex As Exception

                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                                oBeI_nav_ped_compra_enc.No,
                                                                0,
                                                                0)

                        End Try

                    Next

                Else

                    Console.WriteLine("Pedido de compra sin lineas de detalle?")

                    clsLnI_nav_ejecucion_det_error.Inserta_Log("Pedido de compra sin lineas de detalle",
                                                                oBeI_nav_ped_compra_enc.No,
                                                                0,
                                                                0)
                    Throw New Exception(String.Format("Pedido de compra No: {0} sin lineas de detalle", oBeI_nav_ped_compra_enc.No))

                End If

            Catch ex As Exception

                clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                  oBeI_nav_ped_compra_enc.No,
                                                  0,
                                                  0)

            End Try

            Insert_Single_Pedido_From_ERP = True

        Catch ex As Exception

            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message,
                                                       "",
                                                       0,
                                                       0)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))

        End Try

    End Function

    Public Enum pEmpresa
        Killios = 1
        Garesa = 2
    End Enum

    Public Shared Function Asigna_Unidad_De_Medida(BePedidoCompraDet As clsBeTrans_oc_det,
                                                   navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                                   BeUnidadMedidaPedCompra As clsBeUnidad_medida,
                                                   BeProductoBodega As clsBeProducto_bodega,
                                                   lConnection As SqlConnection,
                                                   lTransaction As SqlTransaction) As Boolean
        Try
            If Not clsLnProducto.Existe(navPedidoCompraDet.No,
                                     BeUnidadMedidaPedCompra.IdUnidadMedida,
                                     lConnection,
                                     lTransaction) Then

                Dim BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(BeProductoBodega.IdProducto,
                                                                                          navPedidoCompraDet.Variant_Code,
                                                                                          lConnection,
                                                                                          lTransaction)

                If BePresentacion IsNot Nothing Then
                    BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                    BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
                    BePedidoCompraDet.IdUnidadMedidaBasica = BeProductoBodega.Producto.IdUnidadMedidaBasica
                    BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeProductoBodega.Producto.IdUnidadMedidaBasica
                    BePedidoCompraDet.Nombre_unidad_medida_basica = BeProductoBodega.Producto.UnidadMedida.Nombre

                Else
                    Dim vFactorConv = clsLnUnidad_medida_conversion.Get_Factor(BeUnidadMedidaPedCompra.IdUnidadMedida,
                                                                            BeProductoBodega.Producto.UnidadMedida.IdUnidadMedida,
                                                                            lConnection,
                                                                            lTransaction)

                    If vFactorConv > 0 Then
                        ' Se desactiva la creación automática de presentaciones. 
                        Throw New Exception("ERROR_20220727_1228C: No se encontró la presentación asociada al código: " &
                                        navPedidoCompraDet.No & " Con código variante: " & navPedidoCompraDet.Variant_Code)
                    Else
                        Throw New Exception(String.Format("Error: No existe factor en unidad_medida_conversion para Producto: {0} UnidMedBas {1} <> UnidMed Ped. Compra {2} ",
                                                      navPedidoCompraDet.No,
                                                      BeProductoBodega.Producto.UnidadMedida.Nombre,
                                                      navPedidoCompraDet.Unit_of_Measure_Code))
                    End If
                End If

            Else
                BePedidoCompraDet.IdUnidadMedidaBasica = BeUnidadMedidaPedCompra.IdUnidadMedida
                BePedidoCompraDet.UnidadMedida.IdUnidadMedida = BeUnidadMedidaPedCompra.IdUnidadMedida
                BePedidoCompraDet.Nombre_unidad_medida_basica = navPedidoCompraDet.Unit_of_Measure_Code
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(vMsgError)
        End Try
    End Function

    Public Shared Function Datos_Validos(ByRef BeINavPedCompraEnc As clsBeI_nav_ped_compra_enc,
                                         ByVal lConnection As SqlConnection,
                                         ByVal lTrans As SqlTransaction) As Boolean

        Datos_Validos = False

        Try

            '#EJC20180810_0145AM: Validar que tenga detalle el documento.
            If BeINavPedCompraEnc.Lineas_Detalle Is Nothing Then
                Throw New Exception("No se proporcionó el detalle del documento")
            ElseIf BeINavPedCompraEnc.Lineas_Detalle.Count = 0 Then
                Throw New Exception("No se proporcionó el detalle del documento")
            ElseIf BeINavPedCompraEnc.No = "" Then
                Throw New Exception("El número de documento no puede ser vacío ")
            ElseIf Exist(BeINavPedCompraEnc.No, lConnection, lTrans) Then
                Throw New Exception("El número de documento: " & BeINavPedCompraEnc.No & " ya existe.")
            ElseIf BeINavPedCompraEnc.Product_Owner_Code = "" Then
                Throw New Exception("El campo Producto_Owner_Code no puede ser vacío, este valor corresponde al codigo de propietario tabla -> propietarios ")
            Else
                Datos_Validos = True
            End If

        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Shared Function Datos_Validos(ByRef BeINavPedCompraEnc As clsBeI_nav_ped_compra_enc) As Boolean

        Datos_Validos = False

        Try

            '#EJC20180810_0145AM: Validar que tenga detalle el documento.
            If BeINavPedCompraEnc.Lineas_Detalle Is Nothing Then
                Throw New Exception("No se proporcionó el detalle del documento")
            ElseIf BeINavPedCompraEnc.Lineas_Detalle.Count = 0 Then
                Throw New Exception("No se proporcionó el detalle del documento")
            ElseIf BeINavPedCompraEnc.No = "" Then
                Throw New Exception("El número de documento no puede ser vacío ")
            ElseIf BeINavPedCompraEnc.Product_Owner_Code = "" Then
                Throw New Exception("El campo Producto_Owner_Code no puede ser vacío, éste valor corresponde al codigo de propietario tabla -> propietarios ")
            Else
                Datos_Validos = True
            End If

        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    Public Shared Function Delete_By_NoEnc(ByVal NoEnc As String) As Boolean

        Delete_By_NoEnc = False

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            If clsLnI_nav_ped_compra_det.Eliminar_By_NoEnc(NoEnc, lConnection, lTransaction) Then
                If clsLnI_nav_ped_compra_det_lote.Eliminar_By_NoEnc(NoEnc, lConnection, lTransaction) Then
                    If Delete_By_NoEnc(NoEnc, lConnection, lTransaction) > 0 Then
                        Delete_By_NoEnc = True
                    End If
                End If
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0}_Enc {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Delete_By_NoEnc(ByVal NoEnc As String,
                                             ByVal pConection As SqlConnection,
                                             ByVal pTransaction As SqlTransaction) As Integer

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " Delete from I_nav_ped_compra_enc 
			                       Where(No = @No) "

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Single(ByRef pBeI_nav_ped_compra_enc As clsBeI_nav_ped_compra_enc) As Boolean

        Dim lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_Single = False

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM I_nav_ped_compra_enc 
             Where(No = @No)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@NO", pBeI_nav_ped_compra_enc.No))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeI_nav_ped_compra_enc, dt.Rows(0))
                pBeI_nav_ped_compra_enc.Lineas_Detalle = clsLnI_nav_ped_compra_det.Get_All_By_NoEnc(lConnection, lTransaction, pBeI_nav_ped_compra_enc.No)
                pBeI_nav_ped_compra_enc.Lineas_Detalle_Lotes = clsLnI_nav_ped_compra_det_lote.GetAll(lConnection, lTransaction, pBeI_nav_ped_compra_enc.No)
            End If

            lTransaction.Commit()

            Return True

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Get_Detalle_Pedido_Traslado_By_Referencia(ByVal Ref As String) As DataTable

        Dim lTable As New DataTable("Result")

        Try

            Dim vSQL As String = "SELECT enc.No,det.no as código,det.Description as Producto,det.Quantity as Cantidad
                    FROM i_nav_ped_compra_det det inner join 
                    i_nav_ped_compra_enc enc on enc.No = det.NoEnc
                    WHERE enc.no = @Referencia 
                    Order By det.no"

            Using lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDataAdapter As New SqlDataAdapter(vSQL, lConnection)

                        lDataAdapter.SelectCommand.Transaction = lTransaction
                        lDataAdapter.SelectCommand.Parameters.AddWithValue("@Referencia", Ref)

                        lDataAdapter.SelectCommand.CommandType = CommandType.Text
                        lDataAdapter.Fill(lTable)

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lTable

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function
    Public Shared Function Procesar_Pedido_Compra_MI3(ByRef navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                                      ByRef OutBeOrdenCompra As clsBeTrans_oc_enc,
                                                      ByRef lblprg As String,
                                                      Optional ByVal BePedidoEnc As clsBeTrans_pe_enc = Nothing,
                                                      Optional ByRef lConnection As SqlConnection = Nothing,
                                                      Optional ByRef lTransInterface As SqlTransaction = Nothing) As Boolean

        Procesar_Pedido_Compra_MI3 = False

        Dim BeConfigEnc As New clsBeI_nav_config_enc
        Dim IdNavConfigDet As Integer = 101
        Dim VContadorBitacoraTOMWMS As Integer = 0
        Dim gBeOrdenCompraEnc As clsBeTrans_oc_enc = Nothing
        Dim PedidoCompraExistente As clsBeTrans_oc_enc = Nothing
        Dim vContadorLineasDetInsertadas As Integer = 0
        Dim InsertoEncabezado As Boolean = False
        Dim BeOcDetLote As New clsBeTrans_oc_det_lote()
        Dim BeTipoDocumento As New clsBeTrans_oc_ti()
        Dim LotesExistentes As New List(Of clsBeTrans_oc_det_lote)
        Dim vCantidadDecimalUMBas As Double = 0
        Dim vCantidadEnteraPres As Double = 0
        Dim vProductoNoExiste As Integer = 0
        Dim Es_Transaccion_Remota As Boolean = (lConnection IsNot Nothing AndAlso lTransInterface IsNot Nothing)

        Try

            If Not Es_Transaccion_Remota Then
                lConnection = New SqlConnection(ConfigurationManager.AppSettings("CST"))
                lConnection.Open() : lTransInterface = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            If navPedidoCompraEnc.Status <> 0 Then

                lblprg += vbNewLine & $"Procesando Documento: {navPedidoCompraEnc.No}" & vbNewLine

                gBeOrdenCompraEnc = New clsBeTrans_oc_enc() With {
                .Referencia = navPedidoCompraEnc.No,
                .IdTipoIngresoOC = navPedidoCompraEnc.Document_Type
            }

                PedidoCompraExistente = clsLnTrans_oc_enc.Get_Single_By_Referencia(gBeOrdenCompraEnc, lConnection, lTransInterface)

                Dim IdBodegaDestino = ObtenerIdBodegaDestino(navPedidoCompraEnc, BeConfigEnc, gBeOrdenCompraEnc, lConnection, lTransInterface)
                Dim vIdEmpresa = clsLnBodega.Get_IdEmpresa_By_IdBodega(gBeOrdenCompraEnc.IdBodega, lConnection, lTransInterface)

                BeConfigEnc = ObtenerConfiguracionDeBodega(navPedidoCompraEnc, BeConfigEnc, gBeOrdenCompraEnc, IdBodegaDestino, vIdEmpresa, lConnection, lTransInterface)

                If PedidoCompraExistente IsNot Nothing Then

                    lblprg += "El documento ya existe, se actualizará." & vbNewLine

                    gBeOrdenCompraEnc.Activo = True
                    gBeOrdenCompraEnc.IdCampaña = navPedidoCompraEnc.Campaign_No

                    '#CKFK20250815 Se actualizan los siguientes campos en la orden de compra
                    gBeOrdenCompraEnc.Serie = navPedidoCompraEnc.Series
                    gBeOrdenCompraEnc.Usr_Documento = navPedidoCompraEnc.User_Document
                    gBeOrdenCompraEnc.Comentarios = navPedidoCompraEnc.Comments

                    Dim BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navPedidoCompraEnc.Buy_From_Vendor_No, BeConfigEnc.Idbodega, lConnection, lTransInterface)
                    BeTipoDocumento = AsignarTipoDocumentoIngreso(navPedidoCompraEnc, gBeOrdenCompraEnc, BeTipoDocumento, lConnection, lTransInterface)
                    ConfigurarEncabezadoOrdenCompra(navPedidoCompraEnc,
                                                    BeConfigEnc,
                                                    BeProveedorBodega,
                                                    gBeOrdenCompraEnc,
                                                    lConnection,
                                                    lTransInterface)
                    lTransInterface.Save("oc_enc")

                    lblprg += $"Procesando# : {navPedidoCompraEnc.No}" & vbNewLine
                    VContadorBitacoraTOMWMS += 1

                    For Each navPedidoCompraDet In navPedidoCompraEnc.Lineas_Detalle
                        Try
                            Dim BePresentacion As New clsBeProducto_Presentacion()
                            Dim BeUnidadMedidaPedCompra As clsBeUnidad_medida = Nothing
                            Dim BeProductoBodega = BuscarProductoBodega(navPedidoCompraDet, IdBodegaDestino, BeConfigEnc, lConnection, lTransInterface)

                            If BeProductoBodega IsNot Nothing Then
                                Dim BePedidoCompraDet = clsLnTrans_oc_det.Exist(PedidoCompraExistente.IdOrdenCompraEnc, navPedidoCompraDet.Line_No, lConnection, lTransInterface)
                                BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(navPedidoCompraDet.Unit_of_Measure_Code, BeConfigEnc.IdPropietario, lConnection, lTransInterface)
                                ValidarPresentaciones(navPedidoCompraDet, BeProductoBodega, BePresentacion, lConnection, lTransInterface)
                                If BePedidoCompraDet Is Nothing Then
                                    CrearYGuardarDetalleOC(navPedidoCompraEnc, navPedidoCompraDet, BePedidoCompraDet, BeProductoBodega, BeUnidadMedidaPedCompra, BePresentacion, PedidoCompraExistente, VContadorBitacoraTOMWMS, IdNavConfigDet, BeConfigEnc, lblprg, LotesExistentes, BeOcDetLote, lConnection, lTransInterface)
                                Else
                                    ActualizarDetalleOrdenCompra(navPedidoCompraEnc, navPedidoCompraDet, BePedidoCompraDet, BeOcDetLote, LotesExistentes, BeProductoBodega, VContadorBitacoraTOMWMS, vContadorLineasDetInsertadas, lblprg, BeConfigEnc, IdNavConfigDet, PedidoCompraExistente, lConnection, lTransInterface)
                                End If
                            ElseIf BeConfigEnc.Interface_SAP Then
                                lblprg += $"Producto no existe en WMS: {navPedidoCompraDet.No}" & vbNewLine
                                vProductoNoExiste += 1
                            End If
                        Catch ex As Exception
                            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "Pedido Sin Detalle", 0, IdNavConfigDet)
                            lblprg += $"Pedido sin detalle: {ex.Message}" & vbNewLine
                        End Try
                    Next

                ElseIf navPedidoCompraEnc.Lineas_Detalle.Count > 0 Then
                    Try
                        Dim creada = InicializarEncabezadoNuevaOC(navPedidoCompraEnc,
                                                                  BeConfigEnc,
                                                                  BeTipoDocumento,
                                                                  IdNavConfigDet,
                                                                  lblprg,
                                                                  gBeOrdenCompraEnc,
                                                                  IdBodegaDestino,
                                                                  lConnection,
                                                                  lTransInterface)
                        lTransInterface.Save("oc_enc")
                        InsertoEncabezado = True
                        VContadorBitacoraTOMWMS += 1

                        For Each navPedidoCompraDet In navPedidoCompraEnc.Lineas_Detalle
                            Dim BeProductoBodega = BuscarProductoBodega(navPedidoCompraDet, IdBodegaDestino, BeConfigEnc, lConnection, lTransInterface)
                            If BeProductoBodega IsNot Nothing Then
                                Dim BeUnidadMedidaPedCompra As New clsBeUnidad_medida
                                Dim BePresentacion As New clsBeProducto_Presentacion
                                Dim vCantidadSolicitadaPedido As Double = 0
                                ValidarYCalcularUMBas(navPedidoCompraDet, BeUnidadMedidaPedCompra, BePresentacion, BeProductoBodega, BeConfigEnc, vCantidadSolicitadaPedido, vCantidadEnteraPres, vCantidadDecimalUMBas, lblprg, lConnection, lTransInterface)
                                InsertarDetalleOrdenCompra(navPedidoCompraEnc, navPedidoCompraDet, BeProductoBodega, BePresentacion, BeConfigEnc, BeUnidadMedidaPedCompra, vCantidadEnteraPres, vCantidadDecimalUMBas, vContadorLineasDetInsertadas, lblprg, BeOcDetLote, LotesExistentes, gBeOrdenCompraEnc, lConnection, lTransInterface)
                            Else
                                lblprg += $"El código de producto:{navPedidoCompraDet.No} no existe o no está asociado a la bodega:{navPedidoCompraDet.Location_Code}" & vbNewLine
                                vProductoNoExiste += 1
                            End If
                        Next

                    Catch ex As Exception
                        Dim vMsgEx4 = vbNewLine & $"Error al insertar el documento de ingreso con Referencia: {navPedidoCompraEnc.No}{vbNewLine}{ex.Message}"
                        clsLnI_nav_ejecucion_det_error.Inserta_Log(vMsgEx4, navPedidoCompraEnc.No, 0, IdNavConfigDet)
                        lblprg += vMsgEx4 & vbNewLine
                        Throw New Exception(vMsgEx4)
                    End Try
                Else
                    lblprg += $"Pedido #:{navPedidoCompraEnc.No} Sin Detalle" & vbNewLine
                End If
            Else
                lblprg += vbNewLine & $"OC Inactiva {navPedidoCompraEnc.No}" & vbNewLine
            End If

            If InsertoEncabezado Then
                If vContadorLineasDetInsertadas > 0 AndAlso vProductoNoExiste = 0 Then
                    If OutBeOrdenCompra IsNot Nothing Then
                        Generar_Tarea_Recepcion(gBeOrdenCompraEnc, BeConfigEnc, BeTipoDocumento, navPedidoCompraEnc, lblprg, BePedidoEnc, lConnection, lTransInterface)
                    End If

                    Procesar_Pedido_Compra_MI3 = True
                    If Not Es_Transaccion_Remota Then lTransInterface.Commit()
                    OutBeOrdenCompra = gBeOrdenCompraEnc.Clone()
                    lblprg += vbNewLine & $"Documento de ingreso procesados correctamente - IdWMS: {OutBeOrdenCompra.IdOrdenCompraEnc}" & vbNewLine
                Else
                    lTransInterface.Rollback("oc_enc")
                    'lblprg += vbNewLine & If(vProductoNoExiste > 0, $"Se anuló la transacción, hay {vProductoNoExiste} producto(s) que no existen en WMS.", "Se anuló la transacción, no se modificó el detalle del documento.")
                End If
            Else
                'lblprg += vbNewLine & "No se insertó el encabezado, se anuló la transacción, no se modificó el detalle del documento."
            End If

        Catch ex As Exception
            If Not Es_Transaccion_Remota Then If lTransInterface IsNot Nothing Then lTransInterface.Rollback()
            clsLnI_nav_ejecucion_det_error.Inserta_Log(ex.Message, "", 0, IdNavConfigDet)
            lblprg += vbNewLine & $"Error al insertar pedido de compra a tabla de TOMWMS : {ex.Message}" & vbNewLine
            Throw
        Finally
            If Not Es_Transaccion_Remota Then
                If lConnection IsNot Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
            End If
        End Try

    End Function
    Public Shared Function ObtenerIdBodegaDestino(navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                                  ByRef BeConfigEnc As clsBeI_nav_config_enc,
                                                  ByRef gBeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                  lConnection As SqlConnection,
                                                  lTransInterface As SqlTransaction) As Integer

        Try
            Dim IdBodegaDestino As Integer = 0

            If Not navPedidoCompraEnc.Is_Internal_Transfer Then
                IdBodegaDestino = clsLnCliente.Get_IdUbicacionVirtual_By_Codigo(navPedidoCompraEnc.Location_Code, lConnection, lTransInterface)

                If IdBodegaDestino = 0 Then
                    IdBodegaDestino = clsLnBodega.Get_IdBodega_By_Codigo(navPedidoCompraEnc.Location_Code, lConnection, lTransInterface)

                    If IdBodegaDestino = 0 Then
                        Dim beBodegaArea = clsLnBodega_area.Get_Single_By_Codigo_Bodega(navPedidoCompraEnc.Location_Code, lConnection, lTransInterface)
                        If beBodegaArea IsNot Nothing Then
                            IdBodegaDestino = beBodegaArea.IdBodega
                        ElseIf IsNumeric(navPedidoCompraEnc.Location_Code) AndAlso clsLnBodega.Exists_By_IdBodega(navPedidoCompraEnc.Location_Code, lConnection, lTransInterface) Then
                            IdBodegaDestino = CInt(navPedidoCompraEnc.Location_Code)
                        End If
                    End If
                End If
            Else
                IdBodegaDestino = clsLnBodega.Get_IdBodega_By_Codigo(navPedidoCompraEnc.Location_Code, lConnection, lTransInterface)

                If IdBodegaDestino = 0 AndAlso IsNumeric(navPedidoCompraEnc.Location_Code) AndAlso clsLnBodega.Exists_By_IdBodega(navPedidoCompraEnc.Location_Code, lConnection, lTransInterface) Then
                    IdBodegaDestino = CInt(navPedidoCompraEnc.Location_Code)
                End If
            End If

            If IdBodegaDestino = 0 Then
                Dim mensaje As String = If(navPedidoCompraEnc.Is_Internal_Transfer,
                    $"No se ha configurado la ubicación virtual para el cliente/bodega: {navPedidoCompraEnc.Location_Code} IsInternalTransfer: {navPedidoCompraEnc.Is_Internal_Transfer}",
                    $"#ERROR_202309131539: No se ha configurado la ubicación virtual para la bodega destino: {navPedidoCompraEnc.Location_Code} IsInternalTransfer: {navPedidoCompraEnc.Is_Internal_Transfer}, revise que el material está asociado a la bodega.")
                Throw New Exception(mensaje)
            End If

            BeConfigEnc.Idbodega = IdBodegaDestino
            gBeOrdenCompraEnc.IdBodega = IdBodegaDestino

            Return IdBodegaDestino

        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Shared Function ObtenerConfiguracionDeBodega(navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                                        ByRef BeConfigEnc As clsBeI_nav_config_enc,
                                                        ByRef gBeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                        IdBodegaDestino As Integer,
                                                        vIdEmpresa As Integer,
                                                        lConnection As SqlConnection,
                                                        lTransInterface As SqlTransaction) As clsBeI_nav_config_enc

        Try
            BeConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(gBeOrdenCompraEnc.IdBodega,
                                                                                    vIdEmpresa,
                                                                                    lConnection,
                                                                                    lTransInterface)
            If BeConfigEnc Is Nothing Then
                Throw New Exception("La configuración de interface para la bodega no fue definida")
            End If

            BeConfigEnc.IdPropietario = clsLnPropietarios.Get_IdPropietario_By_Codigo(navPedidoCompraEnc.Product_Owner_Code,
                                                                                      lConnection,
                                                                                      lTransInterface)

            If BeConfigEnc.IdPropietario = 0 Then
                Throw New Exception("No se pudo obtener el Identificador de propietario para: " & navPedidoCompraEnc.Product_Owner_Code)
            End If

            gBeOrdenCompraEnc.PropietarioBodega = New clsBePropietario_bodega With {
                .IdPropietario = BeConfigEnc.IdPropietario,
                .IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(IdBodegaDestino,
                                                                                                               BeConfigEnc.IdPropietario,
                                                                                                               lConnection,
                                                                                                               lTransInterface)
            }

            If gBeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega = 0 Then
                Throw New Exception("No se pudo obtener el IdPropietarioBodega")
            End If

            gBeOrdenCompraEnc.IdPropietarioBodega = gBeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega

            Return BeConfigEnc

        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Shared Function AsignarTipoDocumentoIngreso(navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                                       ByRef gBeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                       ByRef BeTipoDocumento As clsBeTrans_oc_ti,
                                                       lConnection As SqlConnection,
                                                       lTransInterface As SqlTransaction) As clsBeTrans_oc_ti

        Try
            If navPedidoCompraEnc.Document_Type = 0 Then
                If navPedidoCompraEnc.Is_Internal_Transfer Then
                    gBeOrdenCompraEnc.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Transferencia
                    BeTipoDocumento.Genera_Tarea_Ingreso = True
                Else
                    gBeOrdenCompraEnc.IdTipoIngresoOC = clsDataContractDI.tTipoDocumentoIngreso.Ingreso
                End If
            Else
                gBeOrdenCompraEnc.IdTipoIngresoOC = navPedidoCompraEnc.Document_Type
                BeTipoDocumento = clsLnTrans_oc_ti.GetSingle(gBeOrdenCompraEnc.IdTipoIngresoOC, lConnection, lTransInterface)
                If BeTipoDocumento Is Nothing Then
                    BeTipoDocumento = New clsBeTrans_oc_ti()
                    BeTipoDocumento.Genera_Tarea_Ingreso = False
                End If
            End If

            Return BeTipoDocumento

        Catch ex As Exception
            Throw
        End Try

    End Function
    Public Shared Sub ConfigurarEncabezadoOrdenCompra(navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                                      BeConfigEnc As clsBeI_nav_config_enc,
                                                      BeProveedorBodega As clsBeProveedor_bodega,
                                                      ByRef gBeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                      lConnection As SqlConnection,
                                                      lTransInterface As SqlTransaction)
        Try
            gBeOrdenCompraEnc.IdProveedorBodega = BeProveedorBodega.IdAsignacion
            gBeOrdenCompraEnc.No_Documento = navPedidoCompraEnc.Vendor_Invoice_No

            If BeConfigEnc.Interface_SAP Then
                gBeOrdenCompraEnc.No_Documento_Recepcion_ERP = navPedidoCompraEnc.Ship_To_Contact
            End If

            gBeOrdenCompraEnc.User_Mod = BeConfigEnc.IdUsuario
            gBeOrdenCompraEnc.Fec_Mod = Now

            If BeConfigEnc.Interface_SAP Then
                gBeOrdenCompraEnc.Procedencia = navPedidoCompraEnc.Buy_From_Vendor_No & " " & navPedidoCompraEnc.Buy_From_Vendor_Name
            Else
                gBeOrdenCompraEnc.Procedencia = navPedidoCompraEnc.Ship_To_Contact
            End If

            gBeOrdenCompraEnc.No_Marchamo = ""
            gBeOrdenCompraEnc.Referencia = navPedidoCompraEnc.No
            gBeOrdenCompraEnc.Observacion = navPedidoCompraEnc.Posting_Description
            gBeOrdenCompraEnc.Control_Poliza = False
            gBeOrdenCompraEnc.Codigo_Empresa_ERP = navPedidoCompraEnc.Company_Code

            If gBeOrdenCompraEnc.IsNew Then
                gBeOrdenCompraEnc.ObjPoliza = Nothing
            End If

            clsLnTrans_oc_enc.Actualizar(gBeOrdenCompraEnc, lConnection, lTransInterface)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Shared Function BuscarProductoBodega(navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                                IdBodegaDestino As Integer,
                                                BeConfigEnc As clsBeI_nav_config_enc,
                                                lConnection As SqlConnection,
                                                lTransInterface As SqlTransaction) As clsBeProducto_bodega
        Try
            Dim productoBodega As clsBeProducto_bodega = Nothing

            If BeConfigEnc.Equiparar_Productos Then
                productoBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(navPedidoCompraDet.No,
                                                                            IdBodegaDestino,
                                                                            lConnection,
                                                                            lTransInterface)

                If productoBodega Is Nothing Then
                    productoBodega = clsLnProducto_bodega.Existe_Parte_By_IdBodega(navPedidoCompraDet.No,
                                                                               IdBodegaDestino,
                                                                               lConnection,
                                                                               lTransInterface)

                    If productoBodega Is Nothing Then
                        productoBodega = clsLnProducto_bodega.Existe_NoSerie_By_IdBodega(navPedidoCompraDet.No,
                                                                                      IdBodegaDestino,
                                                                                      lConnection,
                                                                                      lTransInterface)
                    End If
                End If
            Else
                productoBodega = clsLnProducto_bodega.Existe_Codigo_By_IdBodega(navPedidoCompraDet.No,
                                                                            IdBodegaDestino,
                                                                            lConnection,
                                                                            lTransInterface)
            End If

            Return productoBodega

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Sub ValidarPresentaciones(navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                            BeProductoBodega As clsBeProducto_bodega,
                                            ByRef BePresentacion As clsBeProducto_Presentacion,
                                            lConnection As SqlConnection,
                                            lTransInterface As SqlTransaction)
        Try
            ' Validación por código de unidad de medida (nombre)
            BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Nombre(BeProductoBodega.IdProducto,
                                                                                       navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                       lConnection,
                                                                                       lTransInterface)

            ' Validación por variant_code si aplica
            If Not String.IsNullOrWhiteSpace(navPedidoCompraDet.Variant_Code) Then
                BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(BeProductoBodega.IdProducto,
                                                                                           navPedidoCompraDet.Variant_Code,
                                                                                           lConnection,
                                                                                           lTransInterface)

                If BePresentacion Is Nothing Then
                    Throw New Exception("ERROR_20220727_1228E: No se encontró la presentación asociada al código: " &
                                    navPedidoCompraDet.No & " Con código variante: " & navPedidoCompraDet.Variant_Code)
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Shared Function CrearYGuardarDetalleOC(navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                                  navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                                  ByRef BePedidoCompraDet As clsBeTrans_oc_det,
                                                  BeProductoBodega As clsBeProducto_bodega,
                                                  BeUnidadMedidaPedCompra As clsBeUnidad_medida,
                                                  BePresentacion As clsBeProducto_Presentacion,
                                                  PedidoCompraExistente As clsBeTrans_oc_enc,
                                                  ByRef VContadorBitacoraTOMWMS As Integer,
                                                  IdNavConfigDet As Integer,
                                                  BeConfigEnc As clsBeI_nav_config_enc,
                                                  ByRef lblprg As String,
                                                  LotesExistentes As List(Of clsBeTrans_oc_det_lote),
                                                  ByRef BeOcDetLote As clsBeTrans_oc_det_lote,
                                                  lConnection As SqlConnection,
                                                  lTransInterface As SqlTransaction) As Boolean
        Try

            BePedidoCompraDet = New clsBeTrans_oc_det
            BePedidoCompraDet.IdOrdenCompraEnc = PedidoCompraExistente.IdOrdenCompraEnc
            BePedidoCompraDet.IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(PedidoCompraExistente.IdOrdenCompraEnc, lConnection, lTransInterface) + 1
            BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega

            If BePresentacion IsNot Nothing Then
                BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
                BePedidoCompraDet.Presentacion.IdPresentacion = BePresentacion.IdPresentacion
            Else
                BePedidoCompraDet.IdPresentacion = 0
            End If

            BePedidoCompraDet.Nombre_producto = navPedidoCompraDet.Description

            BePedidoCompraDet.Nombre_unidad_medida_basica = If(String.IsNullOrEmpty(navPedidoCompraDet.Variant_Code),
                                                               navPedidoCompraDet.Unit_of_Measure_Code,
                                                               BeProductoBodega.Producto.UnidadMedida.Nombre)

            BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity
            BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
            BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost
            BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount
            BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No
            BePedidoCompraDet.Activo = True
            BePedidoCompraDet.Porcentaje_arancel = 0
            BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
            BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
            BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code

            If Asigna_Unidad_De_Medida(BePedidoCompraDet,
                                        navPedidoCompraDet,
                                        BeUnidadMedidaPedCompra,
                                        BeProductoBodega,
                                        lConnection,
                                        lTransInterface) Then

                clsLnTrans_oc_det.Insertar(BePedidoCompraDet, lConnection, lTransInterface)

                ProcesarLotes(navPedidoCompraEnc,
                              navPedidoCompraDet,
                              BeOcDetLote,
                              BePedidoCompraDet,
                              LotesExistentes,
                              lConnection,
                              lTransInterface)


                VContadorBitacoraTOMWMS += 1

                Return True

            End If

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error(ex.Message)
            lblprg += String.Format("Error al insertar Detalle en : {0}{1}", ex.Message, vbNewLine)
            lblprg += vbNewLine
        End Try

        Return False
    End Function
    Public Shared Function ProcesarLotes(navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                        navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                        ByRef BeOcDetLote As clsBeTrans_oc_det_lote,
                                        BePedidoCompraDet As clsBeTrans_oc_det,
                                        LotesExistentes As List(Of clsBeTrans_oc_det_lote),
                                        lConnection As SqlConnection,
                                        lTransInterface As SqlTransaction) As Boolean
        Try

            If navPedidoCompraEnc.Lineas_Detalle_Lotes Is Nothing OrElse navPedidoCompraEnc.Lineas_Detalle_Lotes.Count = 0 Then
                Return False
            End If

            For Each Lote In navPedidoCompraEnc.Lineas_Detalle_Lotes.Where(Function(x) x.NoEnc = navPedidoCompraDet.NoEnc AndAlso
                                                                                 x.Item_No = navPedidoCompraDet.No AndAlso
                                                                                 x.Source_Prod_Order_Line = navPedidoCompraDet.Line_No)
                Dim LoteExistente = LotesExistentes.Find(Function(x) x.No_linea = Lote.Source_Prod_Order_Line AndAlso x.Lote = Lote.Lot_No)

                BeOcDetLote = New clsBeTrans_oc_det_lote With {
                    .IdOrdenCompraEnc = BePedidoCompraDet.IdOrdenCompraEnc,
                    .IdOrdenCompraDet = BePedidoCompraDet.IdOrdenCompraDet,
                    .Cantidad = Lote.Quantity_Base,
                    .No_linea = Lote.Source_Prod_Order_Line,
                    .IdProductoBodega = BePedidoCompraDet.IdProductoBodega,
                    .Lote = Lote.Lot_No,
                    .Cantidad_recibida = 0,
                    .Codigo_producto = Lote.Item_No
                }

                If LoteExistente Is Nothing Then
                    BeOcDetLote.IdOrdenCompraDetLote = clsLnTrans_oc_det_lote.MaxID(lConnection, lTransInterface) + 1
                    clsLnTrans_oc_det_lote.Insertar(BeOcDetLote, lConnection, lTransInterface)
                Else
                    clsLnTrans_oc_det_lote.Actualizar(BeOcDetLote, lConnection, lTransInterface)
                End If
            Next

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function ActualizarDetalleOrdenCompra(navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                                        navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                                        ByRef BePedidoCompraDet As clsBeTrans_oc_det,
                                                        ByRef BeOcDetLote As clsBeTrans_oc_det_lote,
                                                        ByRef LotesExistentes As List(Of clsBeTrans_oc_det_lote),
                                                        BeProductoBodega As clsBeProducto_bodega,
                                                        ByRef VContadorBitacoraTOMWMS As Integer,
                                                        ByRef vContadorLineasDetInsertadas As Integer,
                                                        ByRef lblprg As String,
                                                        BeConfigEnc As clsBeI_nav_config_enc,
                                                        IdNavConfigDet As Integer,
                                                        PedidoCompraExistente As clsBeTrans_oc_enc,
                                                        lConnection As SqlConnection,
                                                        lTransInterface As SqlTransaction) As Boolean
        Try
            BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
            BePedidoCompraDet.Codigo_Producto = BeProductoBodega.Producto.Codigo
            BePedidoCompraDet.Nombre_producto = clsPublic.Quitar_Caracteres_No_Permitidos(navPedidoCompraDet.Description)
            BePedidoCompraDet.Nombre_unidad_medida_basica = If(String.IsNullOrEmpty(navPedidoCompraDet.Variant_Code),
                                                               navPedidoCompraDet.Unit_of_Measure_Code,
                                                               BeProductoBodega.Producto.UnidadMedida.Nombre)

            Dim DifCant As Double = navPedidoCompraDet.Quantity - BePedidoCompraDet.Cantidad

            If BePedidoCompraDet.Cantidad <> 0 Then
                lblprg += vbNewLine
                lblprg += String.Format(If(DifCant = 0,
                                           "La cantidad no se modificó para pedido {0} producto {1} ",
                                           If(DifCant > 0,
                                              "La cantidad incrementó respecto a TOM para pedido {0} producto {1} ",
                                              "La cantidad disminuyó respecto al original en WMS  para pedido {0} producto {1} ")),
                                        navPedidoCompraEnc.No,
                                        navPedidoCompraDet.No)
            End If

            BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity
            BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
            BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost
            BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount
            BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No
            BePedidoCompraDet.Activo = True
            BePedidoCompraDet.Porcentaje_arancel = 0
            BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
            BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
            BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code

            clsLnTrans_oc_det.Actualizar_Desde_Interface(BePedidoCompraDet, lConnection, lTransInterface)

            LotesExistentes = clsLnTrans_oc_det_lote.Get_By_IdOrdenCompraEnc(PedidoCompraExistente.IdOrdenCompraEnc,
                                                                             lConnection,
                                                                             lTransInterface)

            ProcesarLotes(navPedidoCompraEnc,
                          navPedidoCompraDet,
                          BeOcDetLote,
                          BePedidoCompraDet,
                          LotesExistentes,
                          lConnection,
                          lTransInterface)

            VContadorBitacoraTOMWMS += 1
            vContadorLineasDetInsertadas += 1

            Return True
        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error(ex.Message)
            lblprg += String.Format("Pedido Sin Detalle: {0}{1}", ex.Message, vbNewLine)
            Return False
        End Try
    End Function
    Public Shared Function InicializarEncabezadoNuevaOC(navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                                        BeConfigEnc As clsBeI_nav_config_enc,
                                                        ByRef BeTipoDocumento As clsBeTrans_oc_ti,
                                                        IdNavConfigDet As Integer,
                                                        ByRef lblprg As String,
                                                        ByRef gBeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                        IdBodegaDestino As Integer,
                                                        lConnection As SqlConnection,
                                                        lTransInterface As SqlTransaction) As Boolean
        Try

            gBeOrdenCompraEnc = New clsBeTrans_oc_enc()
            gBeOrdenCompraEnc.IdOrdenCompraEnc = clsLnTrans_oc_enc.MaxID(lConnection, lTransInterface) + 1
            gBeOrdenCompraEnc.IdBodega = IdBodegaDestino
            gBeOrdenCompraEnc.PropietarioBodega = New clsBePropietario_bodega With {
            .IdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(IdBodegaDestino,
                                                                                                           BeConfigEnc.IdPropietario,
                                                                                                           lConnection,
                                                                                                           lTransInterface)}

            If gBeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega = 0 Then
                Throw New Exception("No se pudo obtener el IdPropietarioBodega")
            End If

            gBeOrdenCompraEnc.IdPropietarioBodega = gBeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega
            gBeOrdenCompraEnc.IdEstadoOC = clsDataContractDI.tEstadoOC.NUEVA
            gBeOrdenCompraEnc.Hora_Creacion = Now
            gBeOrdenCompraEnc.User_Agr = BeConfigEnc.IdUsuario
            gBeOrdenCompraEnc.Fec_Agr = Now
            gBeOrdenCompraEnc.Fecha_Creacion = Now
            gBeOrdenCompraEnc.Activo = True
            gBeOrdenCompraEnc.IdCampaña = Val(navPedidoCompraEnc.Campaign_No)

            '#CKFK20250815 Se actualizan los siguientes campos en la orden de compra
            gBeOrdenCompraEnc.usr_documento = navPedidoCompraEnc.User_Document
            gBeOrdenCompraEnc.Serie = navPedidoCompraEnc.Series
            gBeOrdenCompraEnc.Comentarios = navPedidoCompraEnc.Comments

            Dim BeProveedorBodega As New clsBeProveedor_bodega()
            BeProveedorBodega = clsLnProveedor.Get_ProveedorBodega_By_Codigo_Proveedor(navPedidoCompraEnc.Buy_From_Vendor_No,
                                                                                       IdBodegaDestino,
                                                                                       BeConfigEnc,
                                                                                       lConnection,
                                                                                       lTransInterface)

            If BeProveedorBodega Is Nothing Then
                If Not InsertaProveedor(navPedidoCompraEnc, gBeOrdenCompraEnc, BeConfigEnc, BeProveedorBodega, lConnection, lTransInterface) Then
                    clsLnI_nav_ejecucion_det_error.Inserta_Log($"El proveedor: {navPedidoCompraEnc.Buy_From_Vendor_No} no existe, no se puede importar el pedido de compra: {navPedidoCompraEnc.No}",
                                                            navPedidoCompraEnc.Buy_From_Vendor_No,
                                                            0,
                                                            IdNavConfigDet)
                End If
            End If

            gBeOrdenCompraEnc.IdProveedorBodega = BeProveedorBodega.IdAsignacion

            BeTipoDocumento = AsignarTipoDocumentoIngreso(navPedidoCompraEnc,
                                                          gBeOrdenCompraEnc,
                                                          BeTipoDocumento,
                                                          lConnection,
                                                          lTransInterface)

            gBeOrdenCompraEnc.No_Documento = navPedidoCompraEnc.Vendor_Invoice_No

            If String.IsNullOrWhiteSpace(gBeOrdenCompraEnc.No_Documento) Then
                Throw New Exception("No se definió el número de documento: Vendor_Invoice_No para la cabecera del documento.")
            End If

            gBeOrdenCompraEnc.User_Mod = BeConfigEnc.IdUsuario
            gBeOrdenCompraEnc.Fec_Mod = Now
            gBeOrdenCompraEnc.Procedencia = If(BeConfigEnc.Interface_SAP,
                                           navPedidoCompraEnc.Buy_From_Vendor_No & " " & navPedidoCompraEnc.Buy_From_Vendor_Name,
                                           navPedidoCompraEnc.Ship_To_Contact)
            gBeOrdenCompraEnc.No_Marchamo = ""
            gBeOrdenCompraEnc.Referencia = navPedidoCompraEnc.No
            gBeOrdenCompraEnc.No_Documento_Recepcion_ERP = If(BeConfigEnc.Interface_SAP, If(Not String.IsNullOrWhiteSpace(navPedidoCompraEnc.Internal_Transfer_Document_No), navPedidoCompraEnc.Internal_Transfer_Document_No, navPedidoCompraEnc.Ship_To_Contact), "")
            gBeOrdenCompraEnc.Observacion = navPedidoCompraEnc.Posting_Description
            gBeOrdenCompraEnc.Control_Poliza = False
            If gBeOrdenCompraEnc.IsNew Then gBeOrdenCompraEnc.ObjPoliza = Nothing
            gBeOrdenCompraEnc.Enviado_A_ERP = False
            gBeOrdenCompraEnc.Codigo_Empresa_ERP = navPedidoCompraEnc.Company_Code
            clsLnTrans_oc_enc.Insertar(gBeOrdenCompraEnc, lConnection, lTransInterface)

            Return True

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function ValidarYCalcularUMBas(navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                                 ByRef BeUnidadMedidaPedCompra As clsBeUnidad_medida,
                                                 ByRef BePresentacion As clsBeProducto_Presentacion,
                                                 BeProductoBodega As clsBeProducto_bodega,
                                                 BeConfigEnc As clsBeI_nav_config_enc,
                                                 ByRef vCantidadSolicitadaPedido As Double,
                                                 ByRef vCantidadEnteraPres As Double,
                                                 ByRef vCantidadDecimalUMBas As Double,
                                                 ByRef lblprg As String,
                                                 lConnection As SqlConnection,
                                                 lTransInterface As SqlTransaction) As Boolean

        Try
            ' Buscar UM básica por código y propietario
            BeUnidadMedidaPedCompra = clsLnUnidad_medida.Existe_By_Codigo_And_IdPropietario(navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                            BeConfigEnc.IdPropietario,
                                                                                            lConnection,
                                                                                            lTransInterface)

            If BeUnidadMedidaPedCompra IsNot Nothing Then
                ' Confirmar que el producto existe con la unidad medida
                If Not clsLnProducto.Existe(navPedidoCompraDet.No, BeUnidadMedidaPedCompra.IdUnidadMedida, lConnection, lTransInterface) Then
                    ' Buscar presentación por código variante si existe
                    If navPedidoCompraDet.Variant_Code <> "" Then
                        BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Codigo(BeProductoBodega.IdProducto,
                                                                                                  navPedidoCompraDet.Variant_Code,
                                                                                                  lConnection,
                                                                                                  lTransInterface)
                        If BePresentacion IsNot Nothing Then
                            BeUnidadMedidaPedCompra = BeProductoBodega.Producto.UnidadMedida
                        Else
                            Throw New Exception("ERROR_20220727_1228A: No se encontró la presentación asociada al código: " & navPedidoCompraDet.No &
                                            " Con código de variante: " & navPedidoCompraDet.Variant_Code & " para el IdProducto: " &
                                            BeProductoBodega.IdProducto & " en la línea " & navPedidoCompraDet.Line_No)
                        End If
                    End If
                End If
            Else
                If BeProductoBodega.Producto.UnidadMedida Is Nothing Then
                    Throw New Exception($"Producto: {navPedidoCompraDet.No} UnidMedBas No definida")
                End If

                ' Buscar presentación por nombre
                BePresentacion = clsLnProducto_presentacion.Existe_Presentacion_By_Nombre(BeProductoBodega.IdProducto,
                                                                                       navPedidoCompraDet.Unit_of_Measure_Code,
                                                                                       lConnection,
                                                                                       lTransInterface)

                If BePresentacion IsNot Nothing Then
                    BeUnidadMedidaPedCompra = BeProductoBodega.Producto.UnidadMedida
                Else
                    Throw New Exception($"La unidad de medida: {navPedidoCompraDet.Unit_of_Measure_Code} no está definida para el código de producto:{navPedidoCompraDet.No} en la tabla unidad_medida.")
                End If
            End If

            ' Evaluar si se requiere conversión a UM básica
            If BeConfigEnc.Convertir_decimales_a_umbas = 1 AndAlso BeConfigEnc.Interface_SAP Then
                BePresentacion = clsLnProducto_presentacion.Get_Presentacion_Defecto_By_IdProducto(BeProductoBodega.IdProducto,
                                                                                               lConnection,
                                                                                               lTransInterface)

                If BePresentacion IsNot Nothing Then
                    If BePresentacion.Factor <= 0 Then
                        Throw New Exception("ERROR_202210251745: El factor es 0 para la presentación NO se puede inferir la conversión.")
                    End If

                    clsPublic.Split_Decimal(navPedidoCompraDet.Quantity / BePresentacion.Factor,
                                        vCantidadEnteraPres,
                                        vCantidadDecimalUMBas)

                    vCantidadDecimalUMBas = Math.Round(vCantidadDecimalUMBas * BePresentacion.Factor)
                    vCantidadEnteraPres = vCantidadEnteraPres * BePresentacion.Factor

                    vCantidadSolicitadaPedido = If(vCantidadEnteraPres > 0, vCantidadEnteraPres, vCantidadDecimalUMBas)
                Else
                    vCantidadSolicitadaPedido = navPedidoCompraDet.Quantity
                End If
            Else
                vCantidadSolicitadaPedido = navPedidoCompraDet.Quantity
            End If

            Return True
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Function InsertarDetalleOrdenCompra(navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                                      navPedidoCompraDet As clsBeI_nav_ped_compra_det,
                                                      BeProductoBodega As clsBeProducto_bodega,
                                                      BePresentacion As clsBeProducto_Presentacion,
                                                      BeConfigEnc As clsBeI_nav_config_enc,
                                                      BeUnidadMedidaPedCompra As clsBeUnidad_medida,
                                                      vCantidadEnteraPres As Double,
                                                      vCantidadDecimalUMBas As Double,
                                                      ByRef vContadorLineasDetInsertadas As Integer,
                                                      ByRef lblprg As String,
                                                      ByRef BeOcDetLote As clsBeTrans_oc_det_lote,
                                                      ByRef LotesExistentes As List(Of clsBeTrans_oc_det_lote),
                                                      gBeOrdenCompraEnc As clsBeTrans_oc_enc,
                                                      lConnection As SqlConnection,
                                                      lTransInterface As SqlTransaction) As Boolean
        Try

            Dim BePedidoCompraDet As New clsBeTrans_oc_det() With {
                .IdOrdenCompraEnc = gBeOrdenCompraEnc.IdOrdenCompraEnc,
                .IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(gBeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransInterface) + 1
            }

            BePedidoCompraDet.IdProductoBodega = BeProductoBodega.IdProductoBodega
            BePedidoCompraDet.Codigo_Producto = IIf(navPedidoCompraDet.Barcode = "", BeProductoBodega.Producto.Codigo, navPedidoCompraDet.Barcode)
            BePedidoCompraDet.Nombre_producto = navPedidoCompraDet.Description

            If Not (BeConfigEnc.Convertir_decimales_a_umbas = 1 Or BeConfigEnc.Interface_SAP) AndAlso vCantidadEnteraPres > 0 Then
                BePedidoCompraDet.Cantidad = Math.Round(vCantidadEnteraPres / BePresentacion.Factor, 6)
                BePedidoCompraDet.IdPresentacion = BePresentacion.IdPresentacion
            Else
                BePedidoCompraDet.Cantidad = navPedidoCompraDet.Quantity
            End If

            BePedidoCompraDet.Cantidad_recibida = navPedidoCompraDet.Quantity_Received
            BePedidoCompraDet.Costo = navPedidoCompraDet.Direct_Unit_Cost
            BePedidoCompraDet.Total_linea = navPedidoCompraDet.Line_Amount
            BePedidoCompraDet.No_Linea = navPedidoCompraDet.Line_No
            BePedidoCompraDet.Activo = True
            BePedidoCompraDet.Porcentaje_arancel = 0
            BePedidoCompraDet.User_agr = BeConfigEnc.IdUsuario
            BePedidoCompraDet.User_mod = BeConfigEnc.IdUsuario
            BePedidoCompraDet.Atributo_variante_1 = navPedidoCompraDet.Variant_Code
            BePedidoCompraDet.IdPresentacion = If(BePresentacion IsNot Nothing, BePresentacion.IdPresentacion, 0)
            BePedidoCompraDet.Presentacion.IdPresentacion = BePedidoCompraDet.IdPresentacion

            If navPedidoCompraDet.Barcode <> "" Then
                Dim BeProductoTallaColor As New clsBeProducto_talla_color
                BeProductoTallaColor = clsLnProducto_talla_color.Get_Single_By_Params(BeProductoBodega.IdProducto, navPedidoCompraDet.Size, navPedidoCompraDet.Color, lConnection, lTransInterface)
                BePedidoCompraDet.IdProductoTallaColor = BeProductoTallaColor.IdProductoTallaColor
            End If

            If Asigna_Unidad_De_Medida(BePedidoCompraDet, navPedidoCompraDet, BeUnidadMedidaPedCompra, BeProductoBodega, lConnection, lTransInterface) Then

                If Not (BeConfigEnc.Convertir_decimales_a_umbas = 1 Or BeConfigEnc.Interface_SAP) AndAlso vCantidadDecimalUMBas > 0 Then
                    Dim BePedidoCompraDetUmBas As New clsBeTrans_oc_det()
                    clsPublic.CopyObject(BePedidoCompraDet, BePedidoCompraDetUmBas)
                    BePedidoCompraDetUmBas.IdOrdenCompraEnc = gBeOrdenCompraEnc.IdOrdenCompraEnc
                    BePedidoCompraDetUmBas.IdOrdenCompraDet = clsLnTrans_oc_det.MaxID(gBeOrdenCompraEnc.IdOrdenCompraEnc, lConnection, lTransInterface) + 1
                    BePedidoCompraDetUmBas.Cantidad = vCantidadDecimalUMBas
                    BePedidoCompraDetUmBas.IdPresentacion = 0
                    BePedidoCompraDetUmBas.Presentacion.IdPresentacion = 0
                    clsLnTrans_oc_det.Insertar(BePedidoCompraDetUmBas, lConnection, lTransInterface)
                Else
                    clsLnTrans_oc_det.Insertar(BePedidoCompraDet, lConnection, lTransInterface)
                End If

                vContadorLineasDetInsertadas += 1

                Return True

            End If

            Return False
        Catch ex As Exception
            Dim vMsgEx3 As String = String.Format("Error al insertar desde ws a intermedia: {0}{1}{2}", ex.Message, ex.Source, vbNewLine)
            clsLnI_nav_ejecucion_det_error.Inserta_Log(vMsgEx3, navPedidoCompraDet.Description, 0, 0)
            lblprg += vMsgEx3 & vbNewLine
            Throw New Exception(vMsgEx3)
        End Try

    End Function
    Public Shared Function InsertaProveedor(navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                           gBeOrdenCompraEnc As clsBeTrans_oc_enc,
                                           BeConfigEnc As clsBeI_nav_config_enc,
                                           ByRef BeProveedorBodega As clsBeProveedor_bodega,
                                           lConnection As SqlConnection,
                                           lTransaction As SqlTransaction) As Boolean
        InsertaProveedor = False
        Try
            If Not clsLnProveedor.Existe_Proveedor(navPedidoCompraEnc.Buy_From_Vendor_No, lConnection, lTransaction) Then
                Dim BeProveedorIngresoDefecto As New clsBeProveedor With {
                .IdProveedor = clsLnProveedor.MaxID(lConnection, lTransaction) + 1,
                .IdEmpresa = clsLnBodega.Get_IdEmpresa_By_IdBodega(gBeOrdenCompraEnc.IdBodega, lConnection, lTransaction),
                .IdPropietario = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(gBeOrdenCompraEnc.IdBodega, gBeOrdenCompraEnc.PropietarioBodega.IdPropietarioBodega, lConnection, lTransaction),
                .Codigo = navPedidoCompraEnc.Buy_From_Vendor_No,
                .Nombre = navPedidoCompraEnc.Buy_From_Vendor_Name,
                .Activo = True,
                .User_agr = BeConfigEnc.User_agr,
                .User_mod = BeConfigEnc.User_mod,
                .Fec_agr = Now,
                .Fec_mod = Now
            }
                clsLnProveedor.Insertar(BeProveedorIngresoDefecto, lConnection, lTransaction)

                BeProveedorBodega = New clsBeProveedor_bodega With {
                .IdAsignacion = clsLnProveedor_bodega.MaxID(lConnection, lTransaction) + 1,
                .IdProveedor = BeProveedorIngresoDefecto.IdProveedor,
                .IdBodega = gBeOrdenCompraEnc.IdBodega,
                .Activo = True,
                .User_agr = BeConfigEnc.IdUsuario,
                .User_mod = BeConfigEnc.IdUsuario,
                .Fec_agr = Now,
                .Fec_mod = Now
            }
                clsLnProveedor_bodega.Insertar(BeProveedorBodega, lConnection, lTransaction)
                Return True
            End If

        Catch ex As Exception
            Throw New Exception("Error en InsertarProveedorDefecto: " & ex.Message)
        End Try
    End Function
    Public Shared Sub Generar_Tarea_Recepcion(gBeOrdenCompraEnc As clsBeTrans_oc_enc,
                                              BeConfigEnc As clsBeI_nav_config_enc,
                                              BeTipoDocumento As clsBeTrans_oc_ti,
                                              navPedidoCompraEnc As clsBeI_nav_ped_compra_enc,
                                              ByRef lblprg As String,
                                              Optional BePedidoEnc As clsBeTrans_pe_enc = Nothing,
                                              Optional lConnection As SqlConnection = Nothing,
                                              Optional lTransInterface As SqlTransaction = Nothing)
        Try
            Dim OutBeRecepcionEnc As New clsBeTrans_re_enc

            If BeConfigEnc.Crear_Recepcion_De_Compra_NAV OrElse BeTipoDocumento.Genera_Tarea_Ingreso Then
                If BeConfigEnc.Interface_SAP AndAlso BePedidoEnc IsNot Nothing Then
                    clsLnTrans_re_enc.Generar_Tarea_Recepcion_By_OrdenCompraEnc_And_Pedido(gBeOrdenCompraEnc,
                                                                                           lblprg,
                                                                                           True,
                                                                                           BeConfigEnc,
                                                                                           OutBeRecepcionEnc,
                                                                                           BePedidoEnc,
                                                                                           lConnection,
                                                                                           lTransInterface)
                Else
                    clsLnTrans_re_enc.Generar_Tarea_Recepcion_By_OrdenCompraEnc(gBeOrdenCompraEnc,
                                                                                lblprg,
                                                                                True,
                                                                                BeConfigEnc,
                                                                                OutBeRecepcionEnc,
                                                                                lConnection,
                                                                                lTransInterface)
                End If

                lblprg += vbNewLine & vbTab & String.Format("Se creó la tarea de recepción: {0} para el documento de ingreso: {1} {2}",
                                                        OutBeRecepcionEnc.IdRecepcionEnc,
                                                        navPedidoCompraEnc.No,
                                                        vbNewLine)
            End If
        Catch ex As Exception
            Throw New Exception("Error en Generar_Tarea_Recepcion: " & ex.Message)
        End Try
    End Sub

End Class