Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_ped_traslado_det_lote

    Public Shared Function Exist(ByRef pBeI_nav_ped_traslado_det_lote As clsBeI_nav_ped_traslado_det_lote,
                                 ByVal pConnection As SqlConnection,
                                 ByVal pTransaction As SqlTransaction)

        Exist = False

        Try

            '#EJC20180614: Se agregó validación por número de línea porque el artículo se puede repetir por línea
            Dim vSQL As String = "SELECT No FROM I_nav_ped_traslado_det_lote 
                                  Where(NoEnc = @NoEnc AND No = @No AND Line_No = @Line_No AND Batch_No = @BATCH_NO )"

            Using lDTA As New SqlDataAdapter(vSQL, pConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@NOENC", pBeI_nav_ped_traslado_det_lote.NoEnc))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@NO", pBeI_nav_ped_traslado_det_lote.No))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@LINE_NO", pBeI_nav_ped_traslado_det_lote.Line_No))
                lDTA.SelectCommand.Parameters.Add(New SqlParameter("@BATCH_NO", pBeI_nav_ped_traslado_det_lote.Batch_No))
                lDTA.SelectCommand.Transaction = pTransaction

                Dim lDT As New DataTable
                lDTA.Fill(lDT)

                Exist = lDT.Rows.Count > 0

            End Using

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
