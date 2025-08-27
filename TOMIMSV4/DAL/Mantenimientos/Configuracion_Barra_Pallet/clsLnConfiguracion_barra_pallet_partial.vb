Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnConfiguracion_barra_pallet

    Public Shared Function GetSingle(ByVal pIdConfiguracionBarraPallet As Integer) As clsBeConfiguracion_barra_pallet

        GetSingle = Nothing

        Try

            Const sp As String = "SELECT * FROM Configuracion_barra_pallet" &
            " Where(IdConfiguracionPallet = @IdConfiguracionPallet)"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.Add(New SqlParameter("@IDCONFIGURACIONPALLET", pIdConfiguracionBarraPallet))

            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Dim pBeConfiguracion_barra_pallet As New clsBeConfiguracion_barra_pallet
                Cargar(pBeConfiguracion_barra_pallet, dt.Rows(0))
                Return pBeConfiguracion_barra_pallet
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function


End Class
