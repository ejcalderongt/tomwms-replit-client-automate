Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Threading.Tasks
Imports DevExpress.XtraEditors

Partial Public Class clsLnTrans_ajuste_enc
    Public Shared Function GetAll(ByVal pFechaDel As Date,
                                  ByVal pFechaAl As Date,
                                  ByVal pIdBodega As Integer) As List(Of clsBeTrans_ajuste_enc)

        Try

            Dim lReturnList As New List(Of clsBeTrans_ajuste_enc)
            Dim sp As String = "SELECT * FROM Trans_ajuste_enc"
            sp += " WHERE IdBodega = @IdBodega AND cast(Trans_ajuste_enc.fec_agr AS DATE) BETWEEN " & FormatoFechas.fFecha(pFechaDel) &
                   " AND " & FormatoFechas.fFecha(pFechaAl)

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_ajuste_enc As New clsBeTrans_ajuste_enc

            For Each dr As DataRow In dt.Rows
                vBeTrans_ajuste_enc = New clsBeTrans_ajuste_enc
                Cargar(vBeTrans_ajuste_enc, dr)
                lReturnList.Add(vBeTrans_ajuste_enc)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Sub Aplicar_Ajuste(ByVal BeAjusteEnc As clsBeTrans_ajuste_enc,
                                     ByVal lBeTransAjusteDet As List(Of clsBeTrans_ajuste_det),
                                     ByVal lBeTransMovimientos As List(Of clsBeTrans_movimientos))

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim vIdAjusteDet As Integer

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            vIdAjusteDet = clsLnTrans_ajuste_det.MaxID(lConnection, lTransaction)

            Actualizar(BeAjusteEnc, lConnection, lTransaction)

            For Each BeAjusteDet As clsBeTrans_ajuste_det In lBeTransAjusteDet

                If BeAjusteDet.IdPresentacion <> 0 Then
                    BeAjusteDet.Cantidad_original = Math.Round(BeAjusteDet.Cantidad_original * BeAjusteDet.Presentacion.Factor, 6)
                    BeAjusteDet.Cantidad_nueva = Math.Round(BeAjusteDet.Cantidad_nueva * BeAjusteDet.Presentacion.Factor, 6)
                    BeAjusteDet.CantReservada = Math.Round(BeAjusteDet.CantReservada * BeAjusteDet.Presentacion.Factor, 6)
                End If

                Dim BeUsuario As New clsBeUsuario
                BeUsuario.IdUsuario = BeAjusteEnc.Idusuario

                clsLnUsuario.GetSingle(BeUsuario, lConnection, lTransaction)

                clsLnStock.Procesar_Ajuste(BeAjusteDet,
                                           BeUsuario,
                                           lConnection,
                                           lTransaction)

                vIdAjusteDet += 1 : BeAjusteDet.IdAjusteDet = vIdAjusteDet : BeAjusteDet.IdAjusteEnc = BeAjusteEnc.Idajusteenc

                clsLnTrans_ajuste_det.Insertar(BeAjusteDet,
                                               lConnection,
                                               lTransaction)

            Next

            For Each BeTransMov As clsBeTrans_movimientos In lBeTransMovimientos
                clsLnTrans_movimientos.Insertar(BeTransMov,
                                                lConnection,
                                                lTransaction)
            Next

            clsLnStock_res.Eliminar_All_Stock_Ajuste(BeAjusteEnc.Idajusteenc,
                                                     lConnection,
                                                     lTransaction)

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    '#GT02122024: se crea una copia del metodo, y se agrega transacción porque se trata de un ajuste positivo sin stock
    Public Shared Sub Aplicar_Ajuste(ByVal BeAjusteEnc As clsBeTrans_ajuste_enc,
                                     ByVal lBeTransAjusteDet As List(Of clsBeTrans_ajuste_det),
                                     ByVal lBeTransMovimientos As List(Of clsBeTrans_movimientos),
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction)


        Dim vIdAjusteDet As Integer

        Try

            vIdAjusteDet = clsLnTrans_ajuste_det.MaxID(lConnection, lTransaction)

            Actualizar(BeAjusteEnc, lConnection, lTransaction)

            For Each BeAjusteDet As clsBeTrans_ajuste_det In lBeTransAjusteDet

                If BeAjusteDet.IdPresentacion <> 0 Then
                    BeAjusteDet.Cantidad_original = Math.Round(BeAjusteDet.Cantidad_original * BeAjusteDet.Presentacion.Factor, 6)
                    BeAjusteDet.Cantidad_nueva = Math.Round(BeAjusteDet.Cantidad_nueva * BeAjusteDet.Presentacion.Factor, 6)
                    BeAjusteDet.CantReservada = Math.Round(BeAjusteDet.CantReservada * BeAjusteDet.Presentacion.Factor, 6)
                End If

                Dim BeUsuario As New clsBeUsuario
                BeUsuario.IdUsuario = BeAjusteEnc.Idusuario

                clsLnUsuario.GetSingle(BeUsuario, lConnection, lTransaction)

                clsLnStock.Procesar_Ajuste(BeAjusteDet,
                                           BeUsuario,
                                           lConnection,
                                           lTransaction)

                vIdAjusteDet += 1 : BeAjusteDet.IdAjusteDet = vIdAjusteDet : BeAjusteDet.IdAjusteEnc = BeAjusteEnc.Idajusteenc

                clsLnTrans_ajuste_det.Insertar(BeAjusteDet,
                                               lConnection,
                                               lTransaction)

            Next

            For Each BeTransMov As clsBeTrans_movimientos In lBeTransMovimientos
                clsLnTrans_movimientos.Insertar(BeTransMov,
                                                lConnection,
                                                lTransaction)
            Next

            clsLnStock_res.Eliminar_All_Stock_Ajuste(BeAjusteEnc.Idajusteenc,
                                                     lConnection,
                                                     lTransaction)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Shared Sub Actualizar_Ajuste(ByVal lDet As List(Of clsBeTrans_ajuste_det),
                                        ByVal lMovs As List(Of clsBeTrans_movimientos),
                                        ByVal pIdEmpresa As Integer,
                                        ByVal pIdBodega As Integer,
                                        ByVal pIdUsuario As String)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()

            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            For Each item As clsBeTrans_ajuste_det In lDet
                If item.IdPresentacion <> 0 Then
                    item.Cantidad_original = Math.Round(item.Cantidad_original * item.Presentacion.Factor, 6)
                    item.Cantidad_nueva = Math.Round(item.Cantidad_nueva * item.Presentacion.Factor, 6)
                    item.CantReservada = Math.Round(item.CantReservada * item.Presentacion.Factor, 6)
                End If
                clsLnTrans_ajuste_det.Actualizar(item, lConnection, lTransaction)
            Next

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Sub RollBackStockRes(enc As clsBeTrans_ajuste_enc)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open()

            lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Eliminar(enc, lConnection, lTransaction)

            clsLnStock_res.Eliminar_All_Stock_Ajuste(enc.Idajusteenc, lConnection, lTransaction)

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function GetAll_Pendientes_Envio() As List(Of clsBeTrans_ajuste_enc)

        GetAll_Pendientes_Envio = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_ajuste_enc)

            Const sp As String = "SELECT distinct trans_ajuste_enc.*
                                  FROM trans_ajuste_det INNER JOIN
                                  trans_ajuste_enc 
                                  ON trans_ajuste_det.idajusteenc = trans_ajuste_enc.idajusteenc 
                                  WHERE trans_ajuste_det.enviado = 0"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_ajuste_enc As New clsBeTrans_ajuste_enc

            For Each dr As DataRow In dt.Rows
                vBeTrans_ajuste_enc = New clsBeTrans_ajuste_enc
                Cargar(vBeTrans_ajuste_enc, dr)
                lReturnList.Add(vBeTrans_ajuste_enc)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Pendientes_Envio(ByRef lConnection As SqlConnection,
                                                   ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_ajuste_enc)

        Get_All_Pendientes_Envio = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_ajuste_enc)

            Const sp As String = "SELECT distinct trans_ajuste_enc.*
                                  FROM trans_ajuste_det INNER JOIN
                                  trans_ajuste_enc 
                                  ON trans_ajuste_det.idajusteenc = trans_ajuste_enc.idajusteenc 
                                  WHERE trans_ajuste_det.enviado = 0 and trans_ajuste_enc.auditado = 1 "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_ajuste_enc As New clsBeTrans_ajuste_enc

            For Each dr As DataRow In dt.Rows
                vBeTrans_ajuste_enc = New clsBeTrans_ajuste_enc
                Cargar(vBeTrans_ajuste_enc, dr)
                lReturnList.Add(vBeTrans_ajuste_enc)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Estado_Enviado_A_ERP(ByVal pIdAjusteEnc As Integer,
                                                           ByVal pEnviado_A_ERP As Boolean) As Integer

        Actualizar_Estado_Enviado_A_ERP = 0

        Try

            Dim vSQL As String = "UPDATE trans_ajuste_enc 
                                  SET Enviado_A_ERP=@Enviado_A_ERP 
                                  WHERE IdAjusteEnc=@IdAjusteEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommandEnc As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommandEnc.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                        lCommandEnc.Parameters.AddWithValue("@IdAjusteEnc", pIdAjusteEnc)
                        Actualizar_Estado_Enviado_A_ERP += lCommandEnc.ExecuteNonQuery()

                        vSQL = "UPDATE trans_ajuste_det
                                SET enviado =@Enviado_A_ERP 
                                WHERE IdAjusteEnc=@IdAjusteEnc"

                        Using lCommandDet As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                            lCommandDet.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                            lCommandDet.Parameters.AddWithValue("@IdAjusteEnc", pIdAjusteEnc)
                            Actualizar_Estado_Enviado_A_ERP += lCommandDet.ExecuteNonQuery()
                        End Using

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

    '#GT02122024: copia del metodo, pero con transaccion para guardar ajuste a producto sin stock
    Public Shared Function Actualizar_Estado_Enviado_A_ERP(ByVal pIdAjusteEnc As Integer,
                                                           ByVal pEnviado_A_ERP As Boolean,
                                                           ByRef lConnection As SqlConnection,
                                                           ByRef lTransaction As SqlTransaction) As Integer

        Actualizar_Estado_Enviado_A_ERP = 0

        Try

            Dim vSQL As String = "UPDATE trans_ajuste_enc 
                                  SET Enviado_A_ERP=@Enviado_A_ERP 
                                  WHERE IdAjusteEnc=@IdAjusteEnc"


            Using lCommandEnc As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                lCommandEnc.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                lCommandEnc.Parameters.AddWithValue("@IdAjusteEnc", pIdAjusteEnc)
                Actualizar_Estado_Enviado_A_ERP += lCommandEnc.ExecuteNonQuery()

                vSQL = "UPDATE trans_ajuste_det
                        SET enviado =@Enviado_A_ERP 
                        WHERE IdAjusteEnc=@IdAjusteEnc"

                Using lCommandDet As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    lCommandDet.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                    lCommandDet.Parameters.AddWithValue("@IdAjusteEnc", pIdAjusteEnc)
                    Actualizar_Estado_Enviado_A_ERP += lCommandDet.ExecuteNonQuery()
                End Using

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Estado_Enviado_A_ERP(ByVal pIdAjusteEnc As Integer,
                                                           ByVal pEnviado_A_ERP As Boolean,
                                                           ByVal pReferencia As String) As Integer

        Actualizar_Estado_Enviado_A_ERP = 0

        Try

            Dim vSQL As String = "UPDATE trans_ajuste_enc 
                                  SET Enviado_A_ERP=@Enviado_A_ERP,
                                  Referencia=@Referencia
                                  WHERE IdAjusteEnc=@IdAjusteEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommandEnc As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommandEnc.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                        lCommandEnc.Parameters.AddWithValue("@Referencia", pReferencia)
                        lCommandEnc.Parameters.AddWithValue("@IdAjusteEnc", pIdAjusteEnc)

                        Actualizar_Estado_Enviado_A_ERP += lCommandEnc.ExecuteNonQuery()

                        vSQL = "UPDATE trans_ajuste_det
                                SET Enviado=@Enviado_A_ERP 
                                WHERE IdAjusteEnc=@IdAjusteEnc"

                        Using lCommandDet As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                            lCommandDet.Parameters.AddWithValue("@Enviado_A_ERP", pEnviado_A_ERP)
                            lCommandDet.Parameters.AddWithValue("@IdAjusteEnc", pIdAjusteEnc)
                            Actualizar_Estado_Enviado_A_ERP = lCommandDet.ExecuteNonQuery()
                        End Using

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

    Public Shared Function Actualizar_Referencia(ByVal pIdAjusteEnc As Integer,
                                                ByVal pReferencia As String) As Integer

        Actualizar_Referencia = 0

        Try

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Dim vSQL As String = "UPDATE trans_ajuste_enc SET referencia=@referencia
                    WHERE IdAjusteEnc=@IdAjusteEnc "

                lConnection.Open()

                Using lCommand As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
                    lCommand.Parameters.AddWithValue("@referencia", pReferencia)
                    lCommand.Parameters.AddWithValue("@IdAjusteEnc", pIdAjusteEnc)
                    Actualizar_Referencia = lCommand.ExecuteNonQuery()
                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Actualizar_Estado_Auditado(ByVal pIdAjusteEnc As Integer,
                                                      ByVal pAuditado As Boolean) As Integer

        Actualizar_Estado_Auditado = 0

        Try

            Dim vSQL As String = "UPDATE trans_ajuste_enc 
                                  SET Auditado=@Auditado                                   
                                  WHERE IdAjusteEnc=@IdAjusteEnc"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommandEnc As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        lCommandEnc.Parameters.AddWithValue("@Auditado", pAuditado)
                        lCommandEnc.Parameters.AddWithValue("@IdAjusteEnc", pIdAjusteEnc)
                        Actualizar_Estado_Auditado += lCommandEnc.ExecuteNonQuery()

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

    Public Shared Function Get_All_Auditados_Pendientes_Envio(ByRef lConnection As SqlConnection,
                                                              ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_ajuste_enc)

        Get_All_Auditados_Pendientes_Envio = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_ajuste_enc)

            Const sp As String = "SELECT distinct trans_ajuste_enc.*
                                  FROM trans_ajuste_det INNER JOIN
                                  trans_ajuste_enc 
                                  ON trans_ajuste_det.idajusteenc = trans_ajuste_enc.idajusteenc 
                                  WHERE trans_ajuste_det.enviado = 0 and trans_ajuste_enc.auditado = 1 "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_ajuste_enc As New clsBeTrans_ajuste_enc

            For Each dr As DataRow In dt.Rows
                vBeTrans_ajuste_enc = New clsBeTrans_ajuste_enc
                Cargar(vBeTrans_ajuste_enc, dr)
                lReturnList.Add(vBeTrans_ajuste_enc)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Auditados_Pendientes_Envio_By_IdInventarioEnc(ByRef lConnection As SqlConnection,
                                                                                 ByRef lTransaction As SqlTransaction) As List(Of clsBeTrans_ajuste_enc)

        Get_All_Auditados_Pendientes_Envio_By_IdInventarioEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_ajuste_enc)

            Const sp As String = "SELECT trans_ajuste_enc.*
                                  FROM trans_ajuste_det INNER JOIN
                                  trans_ajuste_enc 
                                  ON trans_ajuste_det.idajusteenc = trans_ajuste_enc.idajusteenc
                                  WHERE trans_ajuste_det.enviado = 0 and trans_ajuste_enc.auditado = 1 AND ajuste_por_inventario >0"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_ajuste_enc As New clsBeTrans_ajuste_enc

            For Each dr As DataRow In dt.Rows
                vBeTrans_ajuste_enc = New clsBeTrans_ajuste_enc
                Cargar(vBeTrans_ajuste_enc, dr)
                lReturnList.Add(vBeTrans_ajuste_enc)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdInventarioEnc(ByVal pIdInventarioEnc As Integer) As List(Of Integer)

        Get_All_By_IdInventarioEnc = Nothing

        Try

            Dim lReturnList As New List(Of Integer)

            Const sp As String = "SELECT IdAjusteEnc 
                                  FROM trans_ajuste_enc 
                                  WHERE IdAjusteEnc IN (SELECT IdAjusteEnc 
                                                            FROM trans_ajuste_enc  
                                                            WHERE ajuste_por_inventario = @ajuste_por_inventario)"

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                    Dim dad As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable

                    cmd.Parameters.AddWithValue("@ajuste_por_inventario", pIdInventarioEnc)

                    dad.Fill(dt)

                    Dim vIdAjusteEnc As Integer = 0

                    For Each dr As DataRow In dt.Rows
                        vIdAjusteEnc = dr.Item(0)
                        lReturnList.Add(vIdAjusteEnc)
                    Next

                    Return lReturnList

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

    '#CKFK20250306 Agregué esta funcionalidad para poder actualizar el campo Enviado_A_ERP de todos los ajustes
    'cuyos detalles ya están sincronizados
    Public Shared Function Actualizar_Estado_Enviado_A_ERP_All(ByVal lAjustes As List(Of Integer),
                                                               ByVal pConnection As SqlConnection,
                                                               ByVal pTransaction As SqlTransaction) As Integer

        Actualizar_Estado_Enviado_A_ERP_All = 0

        Try

            Dim vSQL As String = "UPDATE trans_ajuste_enc SET Enviado_A_ERP = 1
                                  WHERE Enviado_A_ERP = 0 AND 
                                        idajusteenc IN (SELECT DISTINCT idajusteenc FROM trans_ajuste_det WHERE enviado = 1) AND
                                        idajusteenc NOT IN (SELECT DISTINCT idajusteenc FROM trans_ajuste_det WHERE enviado = 0)AND
                                        idajusteenc IN (@lAjustes) "

            Using lCommandEnc As New SqlCommand(vSQL, pConnection, pTransaction) With {.CommandType = CommandType.Text}

                Dim ajustesComoString As String = String.Join(",", lAjustes)

                lCommandEnc.Parameters.AddWithValue("@lAjustes", ajustesComoString)
                Actualizar_Estado_Enviado_A_ERP_All += lCommandEnc.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Inserta_Stock_Y_Movimiento(ByVal pAjusteEnc As clsBeTrans_ajuste_enc,
                                                      ByVal pIdEmpresa As Integer,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As Boolean
        Try
            Inserta_Stock_Y_Movimiento = False

            Dim BeStock As clsBeStock
            Dim BeMov As clsBeTrans_movimientos
            Dim IdMovimiento As Integer = clsLnTrans_movimientos.MaxID(lConnection, lTransaction) + 1
            Dim IdStock As Integer = clsLnStock.MaxID(lConnection, lTransaction) + 1

            For Each item As clsBeTrans_ajuste_det In pAjusteEnc.Lineas_Detalle

                'Stock del ajuste positivo
                BeStock = New clsBeStock With {
                .IsNew = True,
                .IdStock = IdStock,
                .IdPropietarioBodega = item.IdPropietarioBodega,
                .IdProductoBodega = item.IdProductoBodega,
                .IdUnidadMedida = item.IdUnidadMedida,
                .Fecha_Ingreso = Now,
                .Fecha_vence = item.Fecha_vence_nueva,
                .IdPresentacion = item.IdPresentacion,
                .IdProductoEstado = item.IdProductoEstado,
                .ProductoEstado = clsLnProducto_estado.GetSingleByIdEstado(item.IdProductoEstado, lConnection, lTransaction),
                .IdUbicacion = item.IdUbicacion,
                .IdUbicacion_anterior = item.IdUbicacion,
                .Cantidad = item.Cantidad_nueva,
                .Lic_plate = item.lic_plate,
                .Lote = item.Lote_nuevo,
                .Peso = item.Peso_nuevo,
                .User_agr = pAjusteEnc.Idusuario,
                .Fec_agr = Now,
                .User_mod = pAjusteEnc.Idusuario,
                .Fec_mod = Now,
                .IdBodega = pAjusteEnc.IdBodega,
                .Activo = 1,
                .IdProductoTallaColor = item.IdProductoTallaColor_destino
            }

                Dim RowsStockInsertado As Integer = clsLnStock.Insertar(BeStock, lConnection, lTransaction)

                If RowsStockInsertado = 0 Then
                    Throw New Exception("Error al insertar stock para ajuste positivo.")
                End If

                clsLnTrans_ajuste_det.Actualizar_IdStock(item, BeStock.IdStock, lConnection, lTransaction)

                'Movimiento de ajuste positivo
                BeMov = New clsBeTrans_movimientos With {
                .IdMovimiento = IdMovimiento,
                .IdEmpresa = pIdEmpresa,
                .IdBodegaOrigen = pAjusteEnc.IdBodega,
                .IdTransaccion = item.IdAjusteEnc,
                .IdPropietarioBodega = BeStock.IdPropietarioBodega,
                .IdProductoBodega = BeStock.IdProductoBodega,
                .IdUbicacionOrigen = BeStock.IdUbicacion,
                .IdUbicacionDestino = BeStock.IdUbicacion,
                .IdPresentacion = BeStock.IdPresentacion,
                .IdEstadoOrigen = BeStock.IdProductoEstado,
                .IdEstadoDestino = BeStock.IdProductoEstado,
                .IdUnidadMedida = BeStock.IdUnidadMedida,
                .IdTipoTarea = 13,
                .IdBodegaDestino = pAjusteEnc.IdBodega,
                .IdRecepcion = BeStock.IdRecepcionEnc,
                .IdRecepcionDet = BeStock.IdRecepcionDet,
                .Serie = BeStock.Serial,
                .Lote = item.Lote_nuevo,
                .Fecha_vence = item.Fecha_vence_nueva,
                .Fecha = Now,
                .Hora_ini = Now,
                .Hora_fin = Now,
                .Fecha_agr = Now,
                .Usuario_agr = pAjusteEnc.Idusuario,
                .Barra_pallet = BeStock.Lic_plate,
                .Cantidad = item.Cantidad_nueva,
                .Cantidad_hist = 0,
                .Peso = item.Peso_nuevo,
                .Peso_hist = 0
            }

                'Si el ajuste es en presentación, multiplicar por el factor (umbas)
                If BeMov.IdPresentacion <> 0 Then
                    Dim BePresentacion As clsBeProducto_Presentacion =
                    clsLnProducto_presentacion.GetSingle(BeMov.IdPresentacion, lConnection, lTransaction)

                    If BePresentacion IsNot Nothing Then
                        BeMov.Cantidad = Math.Round(BeMov.Cantidad * BePresentacion.Factor, 6)
                    End If
                End If

                Dim RowsMovsInsertado As Integer = clsLnTrans_movimientos.Insertar(BeMov, lConnection, lTransaction)

                If RowsMovsInsertado = 0 Then
                    Throw New Exception("Error al insertar movimientos para ajuste positivo.")
                End If

                IdMovimiento += 1
                IdStock += 1
            Next

            Return True

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error(ex.Message)
            Throw
        End Try
    End Function

End Class
