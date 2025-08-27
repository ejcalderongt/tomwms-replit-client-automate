Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnRegla_ubic_prio_param

    Public Shared Function GetAllForSelection() As List(Of clsBeRegla_ubic_prio_param_SelectionList)

        Try

            Dim lReturnList As New List(Of clsBeRegla_ubic_prio_param_SelectionList)
            Const sp As String = "SELECT * FROM Regla_ubic_prio_param WHERE Activo = 1 "
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeRegla_ubic_prio_param As New clsBeRegla_ubic_prio_param_SelectionList

            For Each dr As DataRow In dt.Rows

                vBeRegla_ubic_prio_param = New clsBeRegla_ubic_prio_param_SelectionList
                Cargar(vBeRegla_ubic_prio_param, dr)
                vBeRegla_ubic_prio_param.Seleccionar = False
                lReturnList.Add(vBeRegla_ubic_prio_param)

            Next

            lConnection.Dispose()
            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
