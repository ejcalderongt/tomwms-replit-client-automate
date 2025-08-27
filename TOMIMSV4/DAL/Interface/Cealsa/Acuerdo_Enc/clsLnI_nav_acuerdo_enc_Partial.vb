Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_acuerdo_enc

    Public Shared Function Existe_Acuerdo(idCliente As Integer, idAcuerdo As Integer, lConnection As SqlConnection, lTransaction As SqlTransaction) As Boolean

        Existe_Acuerdo = False

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT IdAcuerdo FROM I_nav_acuerdo_enc WHERE IdCliente = @IdCliente AND codigo_acuerdo = @codigo_acuerdo"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommand.Parameters.AddWithValue("@IdCliente", idCliente)
                lCommand.Parameters.AddWithValue("@codigo_acuerdo", idAcuerdo)

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

    Public Shared Function Get_All_By_IdCliente(ByVal pIdCliente As Integer) As List(Of clsBeI_nav_acuerdo_enc)

        Dim lReturnList As New List(Of clsBeI_nav_acuerdo_enc)

        Try

            Const sp As String = "SELECT * FROM I_nav_acuerdo_enc WHERE IdCliente = @IdCliente"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_acuerdo_enc As New clsBeI_nav_acuerdo_enc

                        For Each dr As DataRow In lDataTable.Rows
                            vBeI_nav_acuerdo_enc = New clsBeI_nav_acuerdo_enc()
                            Cargar(vBeI_nav_acuerdo_enc, dr)
                            lReturnList.Add(vBeI_nav_acuerdo_enc)
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

    Public Shared Function Get_All_By_IdCliente_DT(ByVal pIdCliente As Integer) As DataTable

        Get_All_By_IdCliente_DT = Nothing

        Try

            Const sp As String = "SELECT codigo_acuerdo as Codigo, codigo_acuerdo + ' - ' + descripcion + ' / ' + nom_moneda as Descripcion, tipo_cobro
								  FROM I_nav_acuerdo_enc WHERE IdCliente = @IdCliente"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdCliente", pIdCliente)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_By_IdCliente_DT = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdAcuerdoEnc(ByVal pIdAcuerdoEnc As Integer) As DataTable

        Get_Single_By_IdAcuerdoEnc = Nothing

        Try

            Const sp As String = "SELECT codigo_acuerdo as Codigo, codigo_acuerdo + ' - ' + descripcion + ' / ' + nom_moneda as Descripcion, tipo_cobro
								  FROM I_nav_acuerdo_enc WHERE IdAcuerdo = @IdAcuerdo"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdo", pIdAcuerdoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_Single_By_IdAcuerdoEnc = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_For_Grid(ByVal IdAcuerdoEnc As Integer) As DataTable

        Get_All_For_Grid = Nothing

        Dim lReturnList As New List(Of clsBeI_nav_servicio)

        Try

            Const sp As String = "SELECT codigo_producto as IdAcuerdoDet, 
	                              servicio as nombre_servicio,
                                  nemonico,
                                  nombre_unidad, 
		                          corre_detalleacuerdo, corre_catalogoproductos 
                                  FROM i_nav_acuerdo_det
                                  WHERE IdAcuerdo = @IdAcuerdo"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@IdAcuerdo", IdAcuerdoEnc)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Get_All_For_Grid = lDataTable

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Function Get_IdAcuerdo_By_Codigo(ByVal pCodigoAcuerdo As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

    '	Get_IdAcuerdo_By_Codigo = -1

    '	Try

    '		Const sp As String = "SELECT * FROM i_nav_acuerdo_enc " &
    '		" Where(codigo_acuerdo = @codigo_acuerdo)"

    '		Using lDTA As New SqlDataAdapter(sp, lConnection)

    '			lDTA.SelectCommand.CommandType = CommandType.Text
    '			lDTA.SelectCommand.Transaction = lTransaction
    '			lDTA.SelectCommand.Parameters.AddWithValue("@CODIGO_ACUERDO", pCodigoAcuerdo)
    '			Dim lDataTable As New DataTable
    '			lDTA.Fill(lDataTable)

    '			Dim vBeI_nav_acuerdo As New clsBeI_nav_acuerdo_enc()

    '			If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
    '				Cargar(vBeI_nav_acuerdo, lDataTable.Rows(0))
    '				Return vBeI_nav_acuerdo.IdAcuerdo
    '			End If

    '		End Using

    '	Catch ex As Exception
    '		Throw ex
    '	End Try

    'End Function

    Public Shared Function Get_IdAcuerdo_By_Nombre(ByVal pNombre As String,
                                                   ByRef pCodigoBase As String,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As Integer

        Get_IdAcuerdo_By_Nombre = -1 : pCodigoBase = ""

        Try

            Const sp As String = "SELECT * FROM i_nav_acuerdo_enc " &
            " Where(descripcion = @descripcion)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@DESCRIPCION", pNombre)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeI_nav_acuerdo As New clsBeI_nav_acuerdo_enc()

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Cargar(vBeI_nav_acuerdo, lDataTable.Rows(0))
                    pCodigoBase = vBeI_nav_acuerdo.Codigo_acuerdo
                    Return vBeI_nav_acuerdo.IdAcuerdo
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdAcuerdo),0) FROM i_nav_acuerdo_enc "

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

    Public Shared Function Existe_By_Codigo(codigo_acuerdo As String, lConnection As SqlConnection, lTransaction As SqlTransaction) As Boolean

        Existe_By_Codigo = False

        Try

            Const sp As String = "SELECT * FROM I_nav_acuerdo_enc " &
            " Where(codigo_acuerdo = @codigo_acuerdo)"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@codigo_acuerdo", codigo_acuerdo)
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Existe_By_Codigo = True
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    '#GT30042024: cargar los acuerdos comerciales desde i_nav_enc con su detalle.
    Public Shared Function Get_All_With_Detail(lConnection As SqlConnection, lTransaction As SqlTransaction) As List(Of clsBeI_nav_acuerdo_enc)

        Dim lReturnList As New List(Of clsBeI_nav_acuerdo_enc)

        Try

            Const sp As String = "SELECT * FROM i_nav_acuerdo_enc "


            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeCEALSA_acuerdoscomerciales As New clsBeI_nav_acuerdo_enc
                Dim vBeCEALSA_acuerdoscomercialExistente As New clsBeI_nav_acuerdo_enc
                Dim vBeDetExistente As New clsBeI_nav_acuerdo_det

                For Each dr As DataRow In lDataTable.Rows

                    vBeCEALSA_acuerdoscomerciales = New clsBeI_nav_acuerdo_enc()
                    Cargar(vBeCEALSA_acuerdoscomerciales, dr)

                    vBeCEALSA_acuerdoscomercialExistente = lReturnList.Find(Function(x) x.Idcliente = vBeCEALSA_acuerdoscomerciales.Idcliente AndAlso
                                                                                                x.Descripcion = vBeCEALSA_acuerdoscomerciales.Descripcion AndAlso
                                                                                                x.Codigo_acuerdo = vBeCEALSA_acuerdoscomerciales.Codigo_acuerdo)



                    If vBeCEALSA_acuerdoscomercialExistente Is Nothing Then

                        'vBeCEALSA_acuerdoscomerciales.lDetalle = clsLnCEALSA_detacuerdoscomerciales.Get_All_By_Codigo_Acuerdo(vBeCEALSA_acuerdoscomerciales.Codigo_acuerdo,
                        'lConnection,
                        'lTransaction)

                        vBeCEALSA_acuerdoscomerciales.lDetalle = clsLnI_nav_acuerdo_det.Get_All_By_Codigo_Acuerdo(vBeCEALSA_acuerdoscomerciales.Codigo_acuerdo,
                                                                                                                  lConnection,
                                                                                                                  lTransaction)

                        lReturnList.Add(vBeCEALSA_acuerdoscomerciales)
                    Else

                        vBeCEALSA_acuerdoscomerciales.lDetalle = clsLnI_nav_acuerdo_det.Get_All_By_Codigo_Acuerdo(vBeCEALSA_acuerdoscomerciales.Codigo_acuerdo,
                                                                                                                   lConnection,
                                                                                                                   lTransaction)


                        If vBeCEALSA_acuerdoscomerciales.lDetalle IsNot Nothing Then

                            For Each Det In vBeCEALSA_acuerdoscomerciales.lDetalle

                                If vBeCEALSA_acuerdoscomercialExistente.lDetalle IsNot Nothing Then

                                    vBeDetExistente = vBeCEALSA_acuerdoscomercialExistente.lDetalle.Find(Function(x) x.Codigo_producto = Det.Codigo_producto)

                                    If vBeDetExistente Is Nothing Then

                                        'Det.EsAdaptado = True
                                        ''Se encontró un producto/servicio que está en un acuerdo con el mismo nombre, pero no existe en el base.
                                        'vBeCEALSA_acuerdoscomercialExistente.lDetalle.Add(Det)

                                        Debug.WriteLine("*** El producto: " & Det.Codigo_producto & " NO existe para el mismo acuerdo, pero se adoptó.***")

                                    Else
                                        Debug.WriteLine("El producto: " & Det.Codigo_producto & " ya existe para el mismo acuerdo")
                                    End If

                                End If

                            Next

                        Else
                            Debug.WriteLine("El Acuerdo Comercial: " & vBeCEALSA_acuerdoscomerciales.Codigo_acuerdo & " - " & vBeCEALSA_acuerdoscomerciales.Descripcion & " Ya existe, pero esta variación no tiene detalle.")
                        End If

                    End If

                Next

            End Using


            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Procesado_WMS(ByRef oBeI_nav_acuerdo As clsBeI_nav_acuerdo_enc,
                                                    Optional ByVal pConection As SqlConnection = Nothing,
                                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Try
            If oBeI_nav_acuerdo IsNot Nothing Then

                Actualizar_procesado_enc(oBeI_nav_acuerdo, pConection, pTransaction)

                If oBeI_nav_acuerdo.lDetalle IsNot Nothing Then

                    If oBeI_nav_acuerdo.lDetalle.Count > 0 Then

                        For Each detalle As clsBeI_nav_acuerdo_det In oBeI_nav_acuerdo.lDetalle
                            detalle.Procesado_wms = True
                            'clsLnTrans_acuerdoscomerciales_det.Actualizar_Procesado_Det(detalle, pConection, pTransaction)
                            clsLnI_nav_acuerdo_det.Actualizar_Procesado_Det(detalle, pConection, pTransaction)

                        Next
                    End If

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try


    End Function


    Public Shared Function Actualizar_procesado_enc(ByRef oBeI_nav_acuerdo As clsBeI_nav_acuerdo_enc,
                                                    Optional ByVal pConection As SqlConnection = Nothing,
                                                    Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

        Try

            '#GT13052024: actualizar por acuerdo y el cliente, ya que el cliente puede tener mas de un acuerdo.
            Upd.Init("i_nav_acuerdo_enc")
            Upd.Add("procesado_wms", "@procesado_wms", DataType.Parametro)
            Upd.Where("IdCliente = @IdCliente and codigo_acuerdo=@codigo_acuerdo")

            Dim sp As String = Upd.SQL()

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            '#CKFK 20210312 Reemplacé el IdCliente por el Codigo cliente
            cmd.Parameters.Add(New SqlParameter("@IDCLIENTE", oBeI_nav_acuerdo.Idcliente))
            cmd.Parameters.Add(New SqlParameter("@CODIGO_ACUERDO", oBeI_nav_acuerdo.Codigo_acuerdo))
            cmd.Parameters.Add(New SqlParameter("@PROCESADO_WMS", oBeI_nav_acuerdo.Procesado_wms))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class