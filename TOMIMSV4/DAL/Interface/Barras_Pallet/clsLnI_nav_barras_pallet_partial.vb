Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_barras_pallet

    Public Shared Function Actualiza_Estado_Barras_Pallet(ByRef oBeI_nav_barras_pallet As clsBeI_nav_barras_pallet, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_barras_pallet")
            Upd.Add("recibido", "@recibido", DataType.Parametro)
            Upd.Add("idrecepcion", "@idrecepcion", DataType.Parametro)
            Upd.Add("bodega_destino", "@bodega_destino", DataType.Parametro)
            Upd.Where("codigo_barra = @codigo_barra " &
                      IIf(oBeI_nav_barras_pallet.Bodega_Origen <> "", " AND bodega_origen = @bodega_origen", "") &
                      " AND idrecepcion = 0 ")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@RECIBIDO", oBeI_nav_barras_pallet.Recibido))
            cmd.Parameters.Add(New SqlParameter("@IDRECEPCION", oBeI_nav_barras_pallet.IdRecepcion))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeI_nav_barras_pallet.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@BODEGA_DESTINO", oBeI_nav_barras_pallet.Bodega_Destino))
            If oBeI_nav_barras_pallet.Bodega_Origen <> "" Then cmd.Parameters.Add(New SqlParameter("@BODEGA_ORIGEN", oBeI_nav_barras_pallet.Bodega_Origen))

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

    Public Shared Function Actuliza_Status_by_Codigo_Barra(ByVal pRecEnc As clsBeTrans_re_enc,
                                                      ByVal pListStockRec As List(Of List(Of clsBeStock_rec)),
                                        ByRef lConnection As SqlConnection,
                                        ByRef lTransaction As SqlTransaction) As Boolean

        Actuliza_Status_by_Codigo_Barra = False

        Try

            Dim pBeI_Nav_Barras_Pallet As New clsBeI_nav_barras_pallet

            If pRecEnc.IdTipoTransaccion <> "PICH000" Then

                For Each l As List(Of clsBeStock_rec) In pListStockRec

                    For Each ObjS As clsBeStock_rec In l

                        pBeI_Nav_Barras_Pallet.Recibido = 1
                        pBeI_Nav_Barras_Pallet.IdRecepcion = ObjS.IdRecepcionEnc
                        pBeI_Nav_Barras_Pallet.Codigo_barra = ObjS.Lic_plate
                        pBeI_Nav_Barras_Pallet.Bodega_Destino = clsLnBodega.Get_Codigo_Bodega_By_Nombre_Bodega(pRecEnc.Bodega, lConnection, lTransaction)

                        If Actualiza_Estado_Barras_Pallet(pBeI_Nav_Barras_Pallet, lConnection, lTransaction) <= 0 Then
                            Return False
                        Else
                            Actuliza_Status_by_Codigo_Barra = True
                        End If

                    Next

                Next

                Actuliza_Status_by_Codigo_Barra = True

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_DT(ByVal pFechaDel As Date, ByVal pFechaAl As Date) As DataTable

        Get_All_DT = Nothing

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Dim vSQL As String = "SELECT * FROM i_nav_barras_pallet "

                    vSQL += String.Format(" Where cast(fecha_agregado AS DATE) BETWEEN {0} AND {1}", FormatoFechas.fFecha(pFechaDel), FormatoFechas.fFecha(pFechaAl))

                    vSQL += " ORDER BY fecha_agregado "

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

    '#GT06032026: actualizar si la barra ya fue impresa en rfid.
    Public Shared Function Actualiza_Estado_Barras_Pallet_By_Impresion(ByRef oBeI_nav_barras_pallet As clsBeI_nav_barras_pallet, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("i_nav_barras_pallet")
            Upd.Add("Impreso", "@Impreso", DataType.Parametro)
            Upd.Where(" IdPallet=@IdPallet and codigo_barra = @codigo_barra ")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IDPALLET", oBeI_nav_barras_pallet.IdPallet))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeI_nav_barras_pallet.Codigo_barra))
            cmd.Parameters.Add(New SqlParameter("@IMPRESO", oBeI_nav_barras_pallet.Impreso))
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

    '#GT23032026: confirmar que, el tag registrado en barra_enc_rfid coincide con barras_pallet
    Public Shared Function Actualiza_Recibido_BarraPallet_RFID(ByRef oBeI_nav_barras_pallet As clsBeI_nav_barras_pallet, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Actualiza_Recibido_BarraPallet_RFID = False

        Try

            Upd.Init("i_nav_barras_pallet")
            Upd.Add("recibido", "@recibido", DataType.Parametro)
            Upd.Where("codigo_barra = @codigo_barra ")

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@RECIBIDO", oBeI_nav_barras_pallet.Recibido))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_BARRA", oBeI_nav_barras_pallet.Codigo_barra))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Actualiza_Recibido_BarraPallet_RFID = (rowsAffected > 0)

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Actualizar_Barra_No_Recibida(ByVal pIdOrdencompraEnc As Integer,
                                                        ByVal pIdOrdenCompraDet As Integer,
                                                        ByVal pCodigo_Barra As String,
                                                        ByVal pIdRecepcion As Integer,
                                                        Optional ByVal pConection As SqlConnection = Nothing,
                                                        Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = " UPDATE i_nav_barras_pallet SET recibido = 0, IdRecepcion = 0 
                                   WHERE IdOrdenCompraEnc = @IdOrdencompraEnc AND
                                         IdOrdenCompraDet = @IdOrdencompraDet AND
                                         Codigo_Barra = @Codigo_Barra AND
                                         IdRecepcion  = @IdRecepcion"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", pIdOrdencompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IdOrdenCompraDet", pIdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@Codigo_Barra", pCodigo_Barra))
            cmd.Parameters.Add(New SqlParameter("@IdRecepcion", pIdRecepcion))

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
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try
    End Function

    '#EJC20260605_FIX_IMP_OC_CONTADOR_ETIQUETAS_PRESENTACION:
    'Persistir contador real de etiquetas de presentación impresas por licencia.
    Public Shared Function Actualizar_Cant_Etiquetas_Presentacion_Impresas(ByVal pIdOrdenCompraEnc As Integer,
                                                                            ByVal pIdOrdenCompraDet As Integer,
                                                                            ByVal pCodigoBarra As String,
                                                                            ByVal pCantidadEtiquetas As Integer,
                                                                            Optional ByVal pConection As SqlConnection = Nothing,
                                                                            Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            Const sp As String = "UPDATE i_nav_barras_pallet
                                  SET cant_etiquetas_presentacion_impresas = @cant_etiquetas_presentacion_impresas
                                  WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc
                                    AND IdOrdenCompraDet = @IdOrdenCompraDet
                                    AND Codigo_Barra = @Codigo_Barra"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@cant_etiquetas_presentacion_impresas", pCantidadEtiquetas))
            cmd.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", pIdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IdOrdenCompraDet", pIdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@Codigo_Barra", pCodigoBarra))

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
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    '#EJC20260605_FIX_IMP_OC_VALIDA_LIC_MADRE_PREVIA:
    'Valida que la licencia madre exista e impresa antes de permitir impresión de fardos/presentación.
    Public Shared Function Existe_Licencia_Madre_Impresa(ByVal pIdOrdenCompraEnc As Integer,
                                                          ByVal pIdOrdenCompraDet As Integer,
                                                          ByVal pLicenciaMadre As String,
                                                          Optional ByVal pConection As SqlConnection = Nothing,
                                                          Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try
            Const sp As String = "SELECT TOP 1 1
                                  FROM i_nav_barras_pallet
                                  WHERE IdOrdenCompraEnc = @IdOrdenCompraEnc
                                    AND IdOrdenCompraDet = @IdOrdenCompraDet
                                    AND Codigo_Barra = @Codigo_Barra
                                    AND ISNULL(Impreso, 0) = 1"

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection)
                cmd.Transaction = pTransaction
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@IdOrdenCompraEnc", pIdOrdenCompraEnc))
            cmd.Parameters.Add(New SqlParameter("@IdOrdenCompraDet", pIdOrdenCompraDet))
            cmd.Parameters.Add(New SqlParameter("@Codigo_Barra", pLicenciaMadre))

            Dim existe As Boolean = (cmd.ExecuteScalar() IsNot Nothing)

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return existe

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

End Class
