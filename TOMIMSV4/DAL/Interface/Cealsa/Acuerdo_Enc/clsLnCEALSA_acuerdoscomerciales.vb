Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnCEALSA_acuerdoscomerciales

    Public Shared Sub Cargar(ByRef oBeCEALSA_acuerdoscomerciales As clsBeCEALSA_acuerdoscomerciales, ByRef dr As DataRow)
        Try
            With oBeCEALSA_acuerdoscomerciales
                .Emp = IIf(IsDBNull(dr.Item("emp")), 0, dr.Item("emp"))
                .Nombre_emp = IIf(IsDBNull(dr.Item("nombre_emp")), "", dr.Item("nombre_emp"))
                .Codcliente = IIf(IsDBNull(dr.Item("codcliente")), 0, dr.Item("codcliente"))
                .Nomcliente = IIf(IsDBNull(dr.Item("nomcliente")), "", dr.Item("nomcliente"))
                .Codacuerdo = IIf(IsDBNull(dr.Item("codacuerdo")), 0, dr.Item("codacuerdo"))
                .Descrip = IIf(IsDBNull(dr.Item("descrip")), "", dr.Item("descrip"))
                .Tipocobro = IIf(IsDBNull(dr.Item("tipocobro")), "", dr.Item("tipocobro"))
                .Codmoneda = IIf(IsDBNull(dr.Item("codmoneda")), 0, dr.Item("codmoneda"))
                .Moneda = IIf(IsDBNull(dr.Item("moneda")), "", dr.Item("moneda"))
                .Procesado_wms = IIf(IsDBNull(dr.Item("procesado_wms")), 0, dr.Item("procesado_wms"))
                .Estado = IIf(IsDBNull(dr.Item("estado")), "A", dr.Item("estado"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub



    Public Shared Function Listar() As DataTable

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Const sp As String = "SELECT * FROM acuerdoscomerciales"
            lConnection.Open() : lTransaction = lConnection.BeginTransaction()
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.Fill(dt)

            lTransaction.Commit()

            Return dt

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lConnection IsNot Nothing Then lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function GetAll() As List(Of clsBeCEALSA_acuerdoscomerciales)

        Dim lReturnList As New List(Of clsBeCEALSA_acuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM acuerdoscomerciales"

            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCEALSA_acuerdoscomerciales As New clsBeCEALSA_acuerdoscomerciales

                        For Each dr As DataRow In lDataTable.Rows
                            vBeCEALSA_acuerdoscomerciales = New clsBeCEALSA_acuerdoscomerciales()
                            Cargar(vBeCEALSA_acuerdoscomerciales, dr)
                            lReturnList.Add(vBeCEALSA_acuerdoscomerciales)
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

    Public Shared Sub GetSingle(ByRef pBeCEALSA_acuerdoscomerciales As clsBeCEALSA_acuerdoscomerciales)

        Try

            Const sp As String = "SELECT * FROM acuerdoscomerciales"


            Using lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))

                lConnection.Open()

                Using lTransaction As SqlTransaction = lConnection.BeginTransaction()

                    Using lDTA As New SqlDataAdapter(sp, lConnection)

                        lDTA.SelectCommand.CommandType = CommandType.Text
                        lDTA.SelectCommand.Transaction = lTransaction
                        Dim lDataTable As New DataTable
                        lDTA.Fill(lDataTable)

                        Dim vBeCEALSA_acuerdoscomerciales As New clsBeCEALSA_acuerdoscomerciales

                        If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                            Cargar(vBeCEALSA_acuerdoscomerciales, lDataTable.Rows(0))
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

    End Sub

    Public Shared Function Actualizar(ByRef oBeCEALSA_acuerdoscomerciales As clsBeCEALSA_acuerdoscomerciales, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST_ERP"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Upd.Init("acuerdoscomerciales")
            Upd.Add("emp", "@emp", DataType.Parametro)
            Upd.Add("nombre_emp", "@nombre_emp", DataType.Parametro)
            Upd.Add("codcliente", "@codcliente", DataType.Parametro)
            Upd.Add("nomcliente", "@nomcliente", DataType.Parametro)
            Upd.Add("codacuerdo", "@codacuerdo", DataType.Parametro)
            Upd.Add("descrip", "@descrip", DataType.Parametro)
            Upd.Add("tipocobro", "@tipocobro", DataType.Parametro)
            Upd.Add("codmoneda", "@codmoneda", DataType.Parametro)
            Upd.Add("moneda", "@moneda", DataType.Parametro)
            Upd.Add("moneda", "@moneda", DataType.Parametro)

            Dim sp As String = Upd.SQL()

            Dim Es_Transaccion_Remota As Boolean = (pConection IsNot Nothing AndAlso pTransaction IsNot Nothing)

            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction()
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@EMP", oBeCEALSA_acuerdoscomerciales.Emp))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_EMP", oBeCEALSA_acuerdoscomerciales.Nombre_emp))
            cmd.Parameters.Add(New SqlParameter("@CODCLIENTE", oBeCEALSA_acuerdoscomerciales.Codcliente))
            cmd.Parameters.Add(New SqlParameter("@NOMCLIENTE", oBeCEALSA_acuerdoscomerciales.Nomcliente))
            cmd.Parameters.Add(New SqlParameter("@CODACUERDO", oBeCEALSA_acuerdoscomerciales.Codacuerdo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIP", oBeCEALSA_acuerdoscomerciales.Descrip))
            cmd.Parameters.Add(New SqlParameter("@TIPOCOBRO", oBeCEALSA_acuerdoscomerciales.Tipocobro))
            cmd.Parameters.Add(New SqlParameter("@CODMONEDA", oBeCEALSA_acuerdoscomerciales.Codmoneda))
            cmd.Parameters.Add(New SqlParameter("@MONEDA", oBeCEALSA_acuerdoscomerciales.Moneda))

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

    '#GT09042024: buscar en tablas de WMS, no en tablas i_nav
    Public Shared Function Existe_By_CodAcuerdo_WMS(ByVal pcodigoAcuerdo As String,
                                                Optional ByVal pConnection As SqlConnection = Nothing,
                                                Optional ByVal pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim Es_Transaccion_Remota As Boolean = Not (pConnection Is Nothing AndAlso pTransaction Is Nothing)


        Existe_By_CodAcuerdo_WMS = False

        Try

            If Not Es_Transaccion_Remota Then

                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            End If

            Dim vSQL As String = "SELECT * FROM cealsa_vwacuerdocomercialenc where codacuerdo = @CODACUERDO"


            Dim lCommand As New SqlCommand(vSQL, IIf(Es_Transaccion_Remota, pConnection, lConnection),
                                           IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
            With {.CommandType = CommandType.Text}

            lCommand.Parameters.AddWithValue("@CODACUERDO", pcodigoAcuerdo)


            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Existe_By_CodAcuerdo_WMS = lReturnValue
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()



        Catch ex As Exception

            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    '#GT08042024: insertar el acuerdo en tabla WMS, en lugar de I_NAV_ENC
    Public Shared Function Insertar(ByRef oBeCealsa_vwacuerdocomercialenc As clsBeCEALSA_acuerdoscomerciales, Optional ByVal pConection As SqlConnection = Nothing, Optional ByVal pTransaction As SqlTransaction = Nothing) As Integer

        Dim lConnection As New SqlConnection(connectionString:=Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Ins.Init("cealsa_vwacuerdocomercialenc")
            Ins.Add("emp", "@codcliente", DataType.Parametro)
            Ins.Add("nombre_emp", "@codacuerdo", DataType.Parametro)
            Ins.Add("codcliente", "@codcliente", DataType.Parametro)
            Ins.Add("codacuerdo", "@codacuerdo", DataType.Parametro)
            Ins.Add("descrip", "@descrip", DataType.Parametro)
            Ins.Add("tipocobro", "@tipocobro", DataType.Parametro)
            Ins.Add("codmoneda", "@codmoneda", DataType.Parametro)
            Ins.Add("moneda", "@moneda", DataType.Parametro)

            Dim sp As String = Ins.SQL()
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text}

            Dim Es_Transaccion_Remota As Boolean = (Not pConection Is Nothing AndAlso Not pTransaction Is Nothing)

            If Es_Transaccion_Remota Then
                cmd = New SqlCommand(sp, pConection, pTransaction)
            Else
                lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
                cmd = New SqlCommand(sp, lConnection, lTransaction)
            End If

            cmd.Parameters.Add(New SqlParameter("@EMP", oBeCealsa_vwacuerdocomercialenc.Emp))
            cmd.Parameters.Add(New SqlParameter("@NOMBRE_EMP", oBeCealsa_vwacuerdocomercialenc.Nombre_emp))
            cmd.Parameters.Add(New SqlParameter("@CODCLIENTE", oBeCealsa_vwacuerdocomercialenc.Codcliente))
            cmd.Parameters.Add(New SqlParameter("@CODACUERDO", oBeCealsa_vwacuerdocomercialenc.Codacuerdo))
            cmd.Parameters.Add(New SqlParameter("@DESCRIP", oBeCealsa_vwacuerdocomercialenc.Descrip))
            cmd.Parameters.Add(New SqlParameter("@TIPOCOBRO", oBeCealsa_vwacuerdocomercialenc.Tipocobro))
            cmd.Parameters.Add(New SqlParameter("@CODMONEDA", oBeCealsa_vwacuerdocomercialenc.Codmoneda))
            cmd.Parameters.Add(New SqlParameter("@MONEDA", oBeCealsa_vwacuerdocomercialenc.Moneda))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            cmd.Dispose()

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

            Return rowsAffected

        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If Not lConnection Is Nothing Then lConnection.Dispose()
            If Not lTransaction Is Nothing Then lTransaction.Dispose()
        End Try

    End Function


End Class
