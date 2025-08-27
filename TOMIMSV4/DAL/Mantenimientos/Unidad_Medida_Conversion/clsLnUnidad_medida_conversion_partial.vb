Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnUnidad_medida_conversion

    Public Shared Function Existe_Factor(ByVal IdUnidadMedidaOrigen As Integer, ByVal IdUnidadMedidaDestino As Integer, _
                                                    ByRef lConection As SqlConnection, _
                                                    ByRef lTransaction As SqlTransaction) As Boolean

        Existe_Factor = False

        Try

            Const sp As String = "SELECT * FROM unidad_medida_conversion " &
            " Where(IdUnidadMedidaOrigen = @IdUnidadMedidaOrigen AND IdUnidadMedidaDestino = @IdUnidadMedidaDestino)"

            Dim cmd As New SqlCommand(sp, lConection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUnidadMedidaOrigen", IdUnidadMedidaOrigen))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUnidadMedidaDestino", IdUnidadMedidaDestino))

            Dim dt As New DataTable
            dad.Fill(dt)

            Existe_Factor = (dt.Rows.Count > 0)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Factor(ByVal IdUnidadMedidaOrigen As Integer,
                                      ByVal IdUnidadMedidaDestino As Integer,
                                      ByRef lConection As SqlConnection,
                                      ByRef lTransaction As SqlTransaction) As Double

        Get_Factor = 0

        Try

            Const sp As String = "SELECT Factor FROM unidad_medida_conversion 
                                  Where(IdUnidadMedidaOrigen = @IdUnidadMedidaOrigen AND IdUnidadMedidaDestino = @IdUnidadMedidaDestino)"

            Dim cmd As New SqlCommand(sp, lConection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUnidadMedidaOrigen", IdUnidadMedidaOrigen))
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IdUnidadMedidaDestino", IdUnidadMedidaDestino))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count > 0 Then
                Get_Factor = IIf(IsDBNull(dt.Rows(0).Item("Factor")), 0, dt.Rows(0).Item("Factor"))
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
