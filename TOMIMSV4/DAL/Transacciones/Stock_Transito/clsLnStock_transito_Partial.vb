Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnStock_transito

    Public Shared Function MaxID(ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As Integer

        Try

            Dim lMax As Integer = 0

            Const sp As String = "SELECT ISNULL(Max(IdStockTransito),0) FROM Stock_transito"

            Using lCommand As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
                Dim lReturnValue As Object = lCommand.ExecuteScalar()
                If lReturnValue IsNot DBNull.Value AndAlso lReturnValue IsNot Nothing Then
                    lMax = CInt(lReturnValue)
                End If
            End Using

            Return lMax

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetSingle(ByRef pBeStock_transito As clsBeStock_transito,
                                     ByVal lConnection As SqlConnection,
                                     ByVal lTransaction As SqlTransaction)

        Try

            Const sp As String = "SELECT * FROM Stock_transito" &
            " Where(IdStockTransito = @IdStockTransito)"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDSTOCKTRANSITO", pBeStock_transito.IdStockTransito))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeStock_transito, dt.Rows(0))
            End If

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdOrdenCompraEnc_And_IdRecepcionEnc(ByVal IdOrdenCompraEnc As Integer,
                                                                          ByVal IdRecepcionEnc As Integer,
                                                                          ByVal lConnection As SqlConnection,
                                                                          ByVal lTransaction As SqlTransaction) As List(Of clsBeStock_transito)

        Get_All_By_IdOrdenCompraEnc_And_IdRecepcionEnc = Nothing

        Try

            Dim lReturnList As New List(Of clsBeStock_transito)

            Const sp As String = "SELECT * FROM Stock_transito  
                                  WHERE IdOrdenCompraEnc_BodDest = @IdOrdenCompraEnc_BodDest 
                                  AND IdRecepcionEnc_BodDest = @IdRecepcionEnc_BodDest "

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdOrdenCompraEnc_BodDest", IdOrdenCompraEnc)
            dad.SelectCommand.Parameters.AddWithValue("@IdRecepcionEnc_BodDest", IdRecepcionEnc)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeStock_transito As New clsBeStock_transito

            If dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows
                    vBeStock_transito = New clsBeStock_transito
                    Cargar(vBeStock_transito, dr)
                    lReturnList.Add(vBeStock_transito)
                Next

                Get_All_By_IdOrdenCompraEnc_And_IdRecepcionEnc = lReturnList

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
