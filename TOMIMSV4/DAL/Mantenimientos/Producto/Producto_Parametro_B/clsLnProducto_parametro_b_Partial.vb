Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnProducto_parametro_b

    Public Shared Function Obtener(ByRef oBeParametroB As clsBeProducto_parametro_b,
                                   ByRef lConnection As SqlConnection,
                                   ByRef lTransaction As SqlTransaction) As Boolean

        Try

            Dim vIdUM As Integer = oBeParametroB.IdProductoParametroB

            Const sp As String = "SELECT * FROM producto_parametro_b 
                                      Where(IdProductoParametroB = @IdParametroB)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdParametroB", oBeParametroB.IdProductoParametroB))
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                oBeParametroB = New clsBeProducto_parametro_b()
                Cargar(oBeParametroB, dt.Rows(0))
            Else
                oBeParametroB = Nothing
            End If

        Catch ex1 As SqlException
            Throw ex1
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


    Public Shared Function GetSingle_By_Name(ByVal pNombreParametroB As String, Optional ByVal pConection As SqlConnection = Nothing,
                                                     Optional ByVal pTransaction As SqlTransaction = Nothing) As clsBeProducto_parametro_b

        GetSingle_By_Name = Nothing
        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Es_Transaccion_Remota As Boolean = Not (pConection Is Nothing AndAlso pTransaction Is Nothing)

        Try

            If Not Es_Transaccion_Remota Then

                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If

            Dim vSQL As String = "SELECT * FROM Producto_parametro_b WHERE (Nombre = @Nombre)"

            Dim lCommand As New SqlCommand(vSQL, IIf(Es_Transaccion_Remota, pConection, lConnection),
                                           IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
            With {.CommandType = CommandType.Text}

            Dim dad As New SqlDataAdapter(lCommand)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@Nombre", pNombreParametroB))

            Dim lDataTable As New DataTable
            dad.Fill(lDataTable)

            Dim vBeProducto_parametro_b As New clsBeProducto_parametro_b

            If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                Cargar(vBeProducto_parametro_b, lDataTable.Rows(0))

                GetSingle_By_Name = vBeProducto_parametro_b
            End If

            If Not Es_Transaccion_Remota Then
                lTransaction.Commit()
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

    Public Shared Function Existe_Parametro_By_Id(ByVal IdProductoParametroB As Integer, Optional pConection As SqlConnection = Nothing,
                                                  Optional pTransaction As SqlTransaction = Nothing) As Boolean

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing
        Dim Es_Transaccion_Remota As Boolean = Not (pConection Is Nothing AndAlso pTransaction Is Nothing)
        Existe_Parametro_By_Id = False

        Try

            If Not Es_Transaccion_Remota Then
                lConnection.Open()
                lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)
            End If


            Dim vSQL As String = "SELECT COUNT(1) FROM producto_parametro_b 
                                  WHERE IdProductoParametroB=@IdProductoParametroB"

            Dim lCommand As New SqlCommand(vSQL, IIf(Es_Transaccion_Remota, pConection, lConnection),
                                           IIf(Es_Transaccion_Remota, pTransaction, lTransaction)) _
            With {.CommandType = CommandType.Text}

            lCommand.Parameters.AddWithValue("@IdProductoParametroB", IdProductoParametroB)

            Dim lReturnValue As Object = lCommand.ExecuteScalar()

            If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                Dim lExists As Boolean = CInt(lReturnValue) > 0
                Existe_Parametro_By_Id = lExists
            End If

            If Not Es_Transaccion_Remota Then lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close() : lConnection.Dispose()
            If lTransaction IsNot Nothing Then lTransaction.Dispose()
        End Try

    End Function

End Class
