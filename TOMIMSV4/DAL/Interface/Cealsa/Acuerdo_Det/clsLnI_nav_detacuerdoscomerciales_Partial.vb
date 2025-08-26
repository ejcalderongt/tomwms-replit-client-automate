Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_detacuerdoscomerciales

    Public Shared Function Get_All_By_CodCliente(ByVal pCodCliente As Integer) As List(Of clsBeI_nav_detacuerdoscomerciales)

        Dim lReturnList As New List(Of clsBeI_nav_detacuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM I_nav_detacuerdoscomerciales WHERE codcliente = @codcliente "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@codcliente", pCodCliente)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeI_nav_detacuerdoscomerciales As New clsBeI_nav_detacuerdoscomerciales

                        For Each dr As DataRow In lDataTable.Rows
                            vBeI_nav_detacuerdoscomerciales = New clsBeI_nav_detacuerdoscomerciales()
                            Cargar(vBeI_nav_detacuerdoscomerciales, dr)
                            lReturnList.Add(vBeI_nav_detacuerdoscomerciales)
                        Next

                    End Using

                    lTransaction.Commit()

                End Using

                lConnection.Close()

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_By_CodCliente(ByVal pCodCliente As Integer,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_detacuerdoscomerciales)

        Dim lReturnList As New List(Of clsBeI_nav_detacuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM I_nav_detacuerdoscomerciales WHERE codcliente = @codcliente AND procesado_wms =0"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@codcliente", pCodCliente)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeI_nav_detacuerdoscomerciales As New clsBeI_nav_detacuerdoscomerciales

                For Each dr As DataRow In lDataTable.Rows
                    vBeI_nav_detacuerdoscomerciales = New clsBeI_nav_detacuerdoscomerciales()
                    Cargar(vBeI_nav_detacuerdoscomerciales, dr)
                    lReturnList.Add(vBeI_nav_detacuerdoscomerciales)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_Encabezados_Acuerdos_Comerciales_By_CodCliente(ByVal pCodCliente As Integer,
                                                                                  ByVal lConnection As SqlConnection,
                                                                                  ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_acuerdo_enc)

        Dim lReturnList As New List(Of clsBeI_nav_acuerdo_enc)

        Try

            Const sp As String = "SELECT codcliente as IdCliente, codacuerdo as IdAcuerdo, 
                                  codacuerdo as codigo_acuerdo, descrip as descripcion, 
                                  tipocobro as tipo_cobro,codmoneda as cod_moneda, moneda as nom_moneda,
                                  procesado_wms
                                  FROM i_nav_detacuerdoscomerciales
                                  WHERE codcliente = @codcliente 
                                  AND procesado_wms =0
                                  GROUP BY codcliente, codacuerdo,descrip, tipocobro,codmoneda, moneda,procesado_wms "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@codcliente", pCodCliente)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeI_nav_detacuerdoscomerciales As New clsBeI_nav_acuerdo_enc

                For Each dr As DataRow In lDataTable.Rows
                    vBeI_nav_detacuerdoscomerciales = New clsBeI_nav_acuerdo_enc()
                    clsLnI_nav_acuerdo_enc.Cargar(vBeI_nav_detacuerdoscomerciales, dr)
                    lReturnList.Add(vBeI_nav_detacuerdoscomerciales)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_Encabezados_Acuerdos_Comerciales_No_Procesados(ByVal lConnection As SqlConnection,
                                                                                  ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_acuerdo_enc)

        Dim lReturnList As New List(Of clsBeI_nav_acuerdo_enc)

        Try

            Const sp As String = "SELECT codcliente as IdCliente, codacuerdo as IdAcuerdo, 
                                  codacuerdo as codigo_acuerdo, descrip as descripcion, 
                                  tipocobro as tipo_cobro,codmoneda as cod_moneda, moneda as nom_moneda,
                                  procesado_wms
                                  FROM i_nav_detacuerdoscomerciales
                                  WHERE procesado_wms =0
                                  GROUP BY codcliente, codacuerdo,descrip, tipocobro,codmoneda, moneda,procesado_wms "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeI_nav_detacuerdoscomerciales As New clsBeI_nav_acuerdo_enc

                For Each dr As DataRow In lDataTable.Rows
                    vBeI_nav_detacuerdoscomerciales = New clsBeI_nav_acuerdo_enc()
                    clsLnI_nav_acuerdo_enc.Cargar(vBeI_nav_detacuerdoscomerciales, dr)
                    lReturnList.Add(vBeI_nav_detacuerdoscomerciales)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Get_All_Detalles_No_Procesados(ByVal lConnection As SqlConnection,
                                                          ByVal lTransaction As SqlTransaction) As List(Of clsBeI_nav_detacuerdoscomerciales)

        Dim lReturnList As New List(Of clsBeI_nav_detacuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM I_nav_detacuerdoscomerciales WHERE procesado_wms =0"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim vBeI_nav_detacuerdoscomerciales As New clsBeI_nav_detacuerdoscomerciales

                For Each dr As DataRow In lDataTable.Rows
                    vBeI_nav_detacuerdoscomerciales = New clsBeI_nav_detacuerdoscomerciales()
                    Cargar(vBeI_nav_detacuerdoscomerciales, dr)
                    lReturnList.Add(vBeI_nav_detacuerdoscomerciales)
                Next

            End Using

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class
