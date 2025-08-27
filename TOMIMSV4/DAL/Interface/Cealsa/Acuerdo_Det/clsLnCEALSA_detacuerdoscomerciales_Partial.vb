Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnCEALSA_detacuerdoscomerciales

    Public Shared Function Get_All_By_Codigo_Acuerdo(ByVal pCodigoAcuerdo As String) As List(Of clsBeCEALSA_detacuerdoscomerciales)

        Dim lReturnList As New List(Of clsBeCEALSA_detacuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM i_nav_acuerdoscomerciales_det WHERE codigo_cliente=@pCodCliente  codacuerdo = @CODACUERDO"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@CODACUERDO", pCodigoAcuerdo)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCEALSA_detacuerdoscomerciales As New clsBeCEALSA_detacuerdoscomerciales

                        For Each dr As DataRow In lDataTable.Rows
                            vBeCEALSA_detacuerdoscomerciales = New clsBeCEALSA_detacuerdoscomerciales()
                            Cargar(vBeCEALSA_detacuerdoscomerciales, dr)
                            lReturnList.Add(vBeCEALSA_detacuerdoscomerciales)
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
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_Codigo_Acuerdo(ByVal pCodCliente As Integer,
                                                    ByVal pCodigoAcuerdo As String,
                                                     ByVal lConnection As SqlConnection,
                                                     ByVal lTransaction As SqlTransaction) As List(Of clsBeCEALSA_detacuerdoscomerciales)

        Get_All_By_Codigo_Acuerdo = Nothing

        Dim lReturnList As New List(Of clsBeCEALSA_detacuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM i_nav_acuerdoscomerciales_det 
                                  WHERE  codigo_cliente=@pCodCliente and corre_cbmaeacuerdosservicios = @CODACUERDO and procesado_wms=0 "

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@PCODCLIENTE", pCodCliente)
                lDTA.SelectCommand.Parameters.AddWithValue("@CODACUERDO", pCodigoAcuerdo)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then

                    Dim vBeCEALSA_detacuerdoscomerciales As New clsBeCEALSA_detacuerdoscomerciales

                    For Each dr As DataRow In lDataTable.Rows
                        vBeCEALSA_detacuerdoscomerciales = New clsBeCEALSA_detacuerdoscomerciales()
                        Cargar(vBeCEALSA_detacuerdoscomerciales, dr)
                        lReturnList.Add(vBeCEALSA_detacuerdoscomerciales)
                    Next

                    Return lReturnList

                End If

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exists_By_Codigo(ByVal pCodigo As String, ByVal pCorrelativo As Integer) As Boolean
        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = " SELECT COUNT(1) 
                                   FROM i_nav_acuerdoscomerciales_det WHERE corre_cbmaeacuerdosservicios=@pCodigo
                                   AND correlativo=@pCorrelativo "

            Using lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

                Using lCommand As New SqlCommand(vSQL, lConnection)

                    lCommand.CommandType = CommandType.Text
                    lCommand.Parameters.AddWithValue("@Codigo", pCodigo)
                    lCommand.Parameters.AddWithValue("@PCORRELATIVO", pCorrelativo)
                    lConnection.Open()

                    Dim lReturnValue As Object = lCommand.ExecuteScalar()

                    lConnection.Close()

                    If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                        lExists = CInt(lReturnValue) > 0
                    End If

                End Using

            End Using

            Return lExists

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Exists_By_Codigo(ByVal pCodigo As String, ByVal pCorrelativo As Integer,
                                            ByRef lConnection As SqlConnection,
                                            ByRef lTransaction As SqlTransaction) As Boolean
        Try

            Dim lExists As Boolean = False

            Dim vSQL As String = " SELECT COUNT(*) FROM i_nav_acuerdoscomerciales_det WHERE corre_cbmaeacuerdosservicios=@pCodigo
                                   AND correlativo=@pCorrelativo "

            Using lCommand As New SqlCommand(vSQL, lConnection, lTransaction)

                lCommand.CommandType = CommandType.Text
                lCommand.Parameters.AddWithValue("@PCODIGO", pCodigo)
                lCommand.Parameters.AddWithValue("@PCORRELATIVO", pCorrelativo)

                Dim lReturnValue As Object = lCommand.ExecuteScalar()

                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lExists = CInt(lReturnValue) > 0
                End If

            End Using

            Return lExists

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    '#GT09042024: Hacia abajo se utilizan metodos sobre tablas cealsa_acuerdos enc y det, no se toca i_nav

    Public Shared Function Get_All_By_Codigo_Acuerdo_And_Cliente(ByVal pCodigoAcuerdo As String, ByVal pCodigoCliente As String,
                                                                 ByVal lConnection As SqlConnection,
                                                                 ByVal lTransaction As SqlTransaction) As List(Of clsBeCEALSA_detacuerdoscomerciales)

        Get_All_By_Codigo_Acuerdo_And_Cliente = Nothing

        Dim lReturnList As New List(Of clsBeCEALSA_detacuerdoscomerciales)

        Try

            Const sp As String = "SELECT *
                                  FROM i_nav_acuerdoscomerciales_det  
                                  WHERE corre_cbmaeacuerdosservicios = @CODACUERDO and codigo_cliente=@PCODIGOCLIENTE"

            Using lDTA As New SqlDataAdapter(sp, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@CODACUERDO", pCodigoAcuerdo)
                lDTA.SelectCommand.Parameters.AddWithValue("@PCODIGOCLIENTE", pCodigoCliente)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable.Rows.Count > 0 Then

                    For Each dr As DataRow In lDataTable.Rows
                        Dim vBeCEALSA_detacuerdoscomerciales = New clsBeCEALSA_detacuerdoscomerciales()
                        Cargar(vBeCEALSA_detacuerdoscomerciales, dr)
                        lReturnList.Add(vBeCEALSA_detacuerdoscomerciales)

                        Get_All_By_Codigo_Acuerdo_And_Cliente = lReturnList
                    Next

                End If

            End Using

            Return Get_All_By_Codigo_Acuerdo_And_Cliente

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try


    End Function

    '#GT07052024: QUITAR, SE TRASLADA A LA CLASE TRANS_ACUERDOCOMERCIALES_DET
    Public Shared Function Get_All_For_Combo(ByVal pCodigo_cuerdo As Integer) As DataTable

        Get_All_For_Combo = Nothing

        Try

            Const sp As String = "SELECT correlativo_detalleacuerdo,servicio,monto,porcentaje,codigo_producto 
                                  FROM trans_acuerdoscomerciales_det  
                                  WHERE codigo_acuerdo = @CODIGO_ACUERDO and estado=1 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@CODIGO_ACUERDO", pCodigo_cuerdo)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        If lDataTable IsNot DBNull.Value AndAlso lDataTable IsNot Nothing Then
                            Get_All_For_Combo = lDataTable
                        End If

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

    '#GT18042024: buscar rubro del acuerdo con prioridad
    Public Shared Function Get_All_By_Codigo_Acuerdo_By_Prioridad(ByVal pCodigoAcuerdo As String) As clsBeCEALSA_detacuerdoscomerciales

        Get_All_By_Codigo_Acuerdo_By_Prioridad = Nothing

        Try

            Const sp As String = "SELECT * FROM trans_acuerdoscomerciales_det 
                                  WHERE codacuerdo = @CODACUERDO and estado=1 AND prioridad=1 "

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        lDTA.SelectCommand.Parameters.AddWithValue("@CODACUERDO", pCodigoAcuerdo)

                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCEALSA_detacuerdoscomerciales As New clsBeCEALSA_detacuerdoscomerciales

                        vBeCEALSA_detacuerdoscomerciales = New clsBeCEALSA_detacuerdoscomerciales()
                        Cargar(vBeCEALSA_detacuerdoscomerciales, lDataTable(0))

                        Get_All_By_Codigo_Acuerdo_By_Prioridad = vBeCEALSA_detacuerdoscomerciales

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



End Class

