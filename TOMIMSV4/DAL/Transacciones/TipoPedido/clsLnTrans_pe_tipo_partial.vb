Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_pe_tipo

    Public Shared Function Get_All_BeTransPeTipo() As List(Of clsBeTrans_pe_tipo)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Get_All_BeTransPeTipo = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeTrans_pe_tipo)
            Const sp As String = "SELECT * FROM Trans_pe_tipo WHERE Activo =1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_pe_tipo As New clsBeTrans_pe_tipo

            For Each dr As DataRow In dt.Rows

                vBeTrans_pe_tipo = New clsBeTrans_pe_tipo
                Cargar(vBeTrans_pe_tipo, dr)
                vBeTrans_pe_tipo.Nombre = String.Format("{0} {1}", vBeTrans_pe_tipo.Nombre, vBeTrans_pe_tipo.Descripcion)
                lReturnList.Add(vBeTrans_pe_tipo)

            Next

            lTransaction.Commit()

            Get_All_BeTransPeTipo = lReturnList

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_For_Combo(ByVal EsBodegaFiscal As Boolean) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim sp As String = "SELECT IdTipoPedido,Nombre,Descripcion,control_poliza,Verificar 
                                FROM Trans_pe_tipo WHERE Activo =1 "

            'GT 12082021: carga los tip documentos según la bodega
            If EsBodegaFiscal Then
                sp += " AND control_poliza = 1 "
            Else
                sp += " AND control_poliza = 0 "
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Get_All_For_Combo = dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_pe_tipo As clsBeTrans_pe_tipo,
                                     ByRef lConnection As SqlConnection,
                                     ByRef lTransaction As SqlTransaction) As Boolean

        Obtener = False

        Try

            Const sp As String = "SELECT * FROM Trans_pe_tipo " &
            " Where(IdTipoPedido = @IdTipoPedido)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOPEDIDO", oBeTrans_pe_tipo.IDTIPOPEDIDO))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_pe_tipo, dt.Rows(0))
                Obtener = True
            End If

        Catch ex1 As SQLException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_ForCombo() As DataTable

        Get_All_ForCombo = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim vSQL As String = "SELECT IdTipoPedido,CONCAT(Nombre,' ',Descripcion) as Nombre from trans_pe_tipo WHERE Activo =1 "
            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Get_All_ForCombo = dt

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw New Exception("Get_All_ForCombo: " & ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Control_Poliza(ByVal pIdTipoPedido As Integer) As Boolean

        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = "SELECT COUNT(1) FROM trans_pe_tipo WHERE control_poliza=1 AND IdTipoPedido=@IdTipoPedido "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                        lCommand.CommandType = CommandType.Text
                        lCommand.Parameters.AddWithValue("@IdTipoPedido", pIdTipoPedido)
                        Dim lReturnValue As Object = lCommand.ExecuteScalar()

                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lExists = CInt(lReturnValue) > 0
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lExists

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdTipoPedido(ByVal IdTipoPedido As Integer) As clsBeTrans_pe_tipo


        Get_Single_By_IdTipoPedido = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Trans_pe_tipo " &
            " Where(IdTipoPedido = @IdTipoPedido) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOPEDIDO", IdTipoPedido))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_pe_tipo As New clsBeTrans_pe_tipo()
                Cargar(pBeTrans_pe_tipo, dt.Rows(0))
                Get_Single_By_IdTipoPedido = pBeTrans_pe_tipo
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_IdTipoPedido(ByVal IdTipoPedido As Integer,
                                                      ByVal lConnection As SqlConnection,
                                                      ByVal lTransaction As SqlTransaction) As clsBeTrans_pe_tipo


        Get_Single_By_IdTipoPedido = Nothing

        Try

            Const sp As String = "SELECT * FROM Trans_pe_tipo 
                                  Where(IdTipoPedido = @IdTipoPedido) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOPEDIDO", IdTipoPedido))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_pe_tipo As New clsBeTrans_pe_tipo()
                Cargar(pBeTrans_pe_tipo, dt.Rows(0))
                Get_Single_By_IdTipoPedido = pBeTrans_pe_tipo
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_DT(ByRef Activos As Boolean) As DataTable

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeTrans_pe_tipo)

            Dim sp As String = "SELECT * FROM Trans_pe_tipo WHERE 1 > 0 "

            If Activos Then
                sp += " AND Activo =1"
            Else
                sp += " AND Activo =0"
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Get_All_DT = dt

            lTransaction.Commit()

            cmd.Dispose()

        Catch ex As Exception
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function GetSingle(ByVal IdTipoPedido As Integer) As clsBeTrans_pe_tipo


        GetSingle = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM Trans_pe_tipo " &
            " Where(IdTipoPedido = @IdTipoPedido)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDTIPOPEDIDO", IdTipoPedido))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_pe_tipo As New clsBeTrans_pe_tipo()
                Cargar(pBeTrans_pe_tipo, dt.Rows(0))
                GetSingle = pBeTrans_pe_tipo
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_For_Combo(ByVal EsBodegaFiscal As Boolean, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Dim sp As String = "SELECT IdTipoPedido,Nombre,Descripcion,control_poliza,Verificar, Fotografia_Verificacion, Es_Devolucion
                                FROM Trans_pe_tipo WHERE Activo =1 "

            If EsBodegaFiscal Then
                sp += " AND control_poliza = 1 "
            Else
                sp += " AND control_poliza = 0 "
            End If

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            Get_All_For_Combo = dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Single_By_IdPedidoEnc(ByVal IdPedidoEnc As Integer) As clsBeTrans_pe_tipo

        Get_Single_By_IdPedidoEnc = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * 
                                  FROM Trans_pe_tipo INNER JOIN
                                       trans_pe_enc ON trans_pe_tipo.IdTipoPedido  = trans_pe_enc.IdTipoPedido
                                  WHERE (IdPedidoEnc = @IdPedidoEnc) "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdPedidoEnc", IdPedidoEnc))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeTrans_pe_tipo As New clsBeTrans_pe_tipo()
                Cargar(pBeTrans_pe_tipo, dt.Rows(0))
                Get_Single_By_IdPedidoEnc = pBeTrans_pe_tipo
            End If

            lTransaction.Commit()

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

End Class
