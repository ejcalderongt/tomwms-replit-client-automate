Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class Trans_AcuerdosComerciales_Enc


    Public Shared Function Listar_Propietarios_By_Facturacion_For_Combo() As DataTable

        Listar_Propietarios_By_Facturacion_For_Combo = Nothing

        Try

            Const sp As String = "select enc.IdCliente as Codigo,cl.nombre_cliente Nombre,enc.descrip,codacuerdo 
                                         from trans_acuerdoscomerciales_enc enc
                                         inner join propietarios pr on enc.IdCliente=pr.IdPropietario
                                         where enc.codcliente not in (select codigo_cliente from trans_acuerdoscomerciales_det where Idcliente=enc.IdCliente )
                                         order by nombre_cliente asc "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                            Listar_Propietarios_By_Facturacion_For_Combo = lDataTable

                        End If

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return Listar_Propietarios_By_Facturacion_For_Combo

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
