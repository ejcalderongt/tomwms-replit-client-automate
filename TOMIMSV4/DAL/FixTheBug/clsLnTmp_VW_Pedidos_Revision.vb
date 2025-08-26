Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnvw_pedidos_revision

    Public Shared Sub Cargar(ByRef oBevw_pedidos_revision As clsBevw_pedidos_revision, ByRef dr As DataRow)
        Try
            With oBevw_pedidos_revision
                .Fecha_Pedido = IIf(IsDBNull(dr.Item("Fecha_Pedido")), Date.Now, dr.Item("Fecha_Pedido"))
                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .Propietario = IIf(IsDBNull(dr.Item("Propietario")), "", dr.Item("Propietario"))
                .Cliente = IIf(IsDBNull(dr.Item("Cliente")), "", dr.Item("Cliente"))
                .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                .No_documento = IIf(IsDBNull(dr.Item("no_documento")), 0, dr.Item("no_documento"))
                .Referencia = IIf(IsDBNull(dr.Item("referencia")), "", dr.Item("referencia"))
                .No_documento_externo = IIf(IsDBNull(dr.Item("no_documento_externo")), "", dr.Item("no_documento_externo"))
                .NIT_Cliente = IIf(IsDBNull(dr.Item("NIT_Cliente")), "", dr.Item("NIT_Cliente"))
                .NIT_Propietario = IIf(IsDBNull(dr.Item("NIT_Propietario")), "", dr.Item("NIT_Propietario"))
                .Fecha_aceptacion = IIf(IsDBNull(dr.Item("fecha_aceptacion")), Date.Now, dr.Item("fecha_aceptacion"))
                .Numero_orden = IIf(IsDBNull(dr.Item("numero_orden")), "", dr.Item("numero_orden"))
                .Dua = IIf(IsDBNull(dr.Item("dua")), "", dr.Item("dua"))
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Shared Function Get_All() As List(Of clsBevw_pedidos_revision)

        Dim lReturnList As New List(Of clsBevw_pedidos_revision)

        Try

            Const sp As String = "SELECT * FROM vw_pedidos_revision"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBevw_pedidos_revision As New clsBevw_pedidos_revision

                        For Each dr As DataRow In lDataTable.Rows
                            vBevw_pedidos_revision = New clsBevw_pedidos_revision()
                            Cargar(vBevw_pedidos_revision, dr)
                            lReturnList.Add(vBevw_pedidos_revision)
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

    Public Shared Sub GetSingle(ByRef pBevw_pedidos_revision As clsBevw_pedidos_revision)

        Try

            Const sp As String = "SELECT * FROM vw_pedidos_revision"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBevw_pedidos_revision As New clsBevw_pedidos_revision

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBevw_pedidos_revision, lDataTable.Rows(0))
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function MaxID() As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT * FROM vw_pedidos_revision"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUnCommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            lMax = CInt(lReturnValue)
                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lMax

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_Cantidad_Pedidos_Sin_Poliza() As Integer

        Get_Cantidad_Pedidos_Sin_Poliza = 0

        Try


            Const sp As String = " select count(distinct (IdPedidoEnc)) as pedidos_sin_poliza from VW_Pedidos_Revision
								   where idpedidoenc not in (select IdOrdenPedidoEnc from trans_pe_pol) "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_Cantidad_Pedidos_Sin_Poliza = CInt(lReturnValue)
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

    Public Shared Function Get_Cantidad_Pedidos_Con_Poliza() As Integer

        Get_Cantidad_Pedidos_Con_Poliza = 0

        Try


            Const sp As String = " select count(distinct (IdPedidoEnc)) as pedidos_sin_poliza from VW_Pedidos_Revision
								   where idpedidoenc in (select IdOrdenPedidoEnc from trans_pe_pol) "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_Cantidad_Pedidos_Con_Poliza = CInt(lReturnValue)
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

    Public Shared Function Get_Cantidad_Barras_Escaneadas() As Integer

        Get_Cantidad_Barras_Escaneadas = 0

        Try

            Const sp As String = " select count(idordenpedidopol) as barras_escaneadas from tmp_trans_pe_pol "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}

                        Dim lReturnValue As Object = lCommand.ExecuteScalar()
                        If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                            Get_Cantidad_Barras_Escaneadas = CInt(lReturnValue)
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

End Class
