Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnTrans_inv_ne

    Public Shared Function GetAllByInventario(ByVal IdInventario As Integer) As DataTable

        Try

            Dim vSQL As String = "SELECT trans_inv_ne.idinventarioenc as IdInventario,producto.codigo AS Código,producto.nombre as Nombre,trans_inv_ne.cantidad as Cantidad  from trans_inv_ne inner join 
                    producto on producto.idproducto = trans_inv_ne.idproducto where idinventarioenc=@IdInventario"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(vSQL, lConnection) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdInventario", IdInventario)

            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            If lConnection.State = ConnectionState.Open Then lConnection.Close()
            lConnection.Dispose()
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function GetAllByInventario(ByVal IdInventario As Integer, ByVal lConnection As SqlConnection, ByVal lTransaction As SqlTransaction) As DataTable

        Try

            Dim vSQL As String = "SELECT trans_inv_ne.idinventarioenc as IdInventario,producto.codigo AS Código,producto.nombre as Nombre,trans_inv_ne.cantidad as Cantidad  from trans_inv_ne inner join 
                    producto on producto.idproducto = trans_inv_ne.idproducto where idinventarioenc=@IdInventario"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            cmd.Parameters.AddWithValue("@IdInventario", IdInventario)

            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)
            cmd.Dispose()

            Return dt

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
