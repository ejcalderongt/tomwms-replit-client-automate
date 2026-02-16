Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_oc_estado

    Public Shared Function GetSingle(ByVal pIdEstadoOc As Integer) As clsBeTrans_oc_estado

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        GetSingle = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Const sp As String = "SELECT * FROM Trans_oc_estado 
             Where(IdEstadoOC = @IdEstadoOC)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDESTADOOC", pIdEstadoOc))

            Dim pBeTrans_oc_estado As New clsBeTrans_oc_estado
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeTrans_oc_estado, dt.Rows(0))
                GetSingle = pBeTrans_oc_estado
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
            If lConnection IsNot Nothing Then lConnection.Dispose()
        End Try

    End Function

    Public Shared Function Obtener(ByRef oBeTrans_oc_estado As clsBeTrans_oc_estado,
                                             ByRef lTransaction As SqlTransaction,
                                             ByRef lConnection As SqlConnection) As Boolean

        Obtener = False

        Try

            Const sp As String = "SELECT * FROM Trans_oc_estado" &
            " Where(IdEstadoOC = @IdEstadoOC)"

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text, .Transaction = lTransaction}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDESTADOOC", oBeTrans_oc_estado.IdEstadoOC))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(oBeTrans_oc_estado, dt.Rows(0))
                Return True
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
