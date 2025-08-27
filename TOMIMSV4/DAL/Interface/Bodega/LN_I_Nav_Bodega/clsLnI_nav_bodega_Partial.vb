Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_bodega

    Public Shared Function GetAll(ByRef lConnection As SqlConnection, ByRef lTrans As SqlTransaction) As List(Of clsBeI_nav_bodega)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_bodega)
            Const sp As String = "SELECT * FROM I_nav_bodega "
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text, .Transaction = lTrans}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_bodega As New clsBeI_nav_bodega

            For Each dr As DataRow In dt.Rows

                vBeI_nav_bodega = New clsBeI_nav_bodega
                Cargar(vBeI_nav_bodega, dr)
                lReturnList.Add(vBeI_nav_bodega)

            Next

            cmd.Dispose()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
