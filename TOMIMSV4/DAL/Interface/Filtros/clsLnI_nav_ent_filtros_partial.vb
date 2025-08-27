Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLnI_nav_ent_filtros

    Enum pEntidadesSycn

        Producto = 1
        Proveedor = 2
        Bodega = 3
        Pedido_Compra = 4
        Pedido_Traslado = 5
        Cliente = 6
        Ordenes_Produccion = 7
        Pedido_Venta = 8
        Devolucion = 9
        Devolucion_Venta = 10
        Traslado_SAP = 11

    End Enum

    Public Shared Function Get_All_By_IdNavEnt(ByVal IdNavEnt As pEntidadesSycn) As List(Of clsBeI_nav_ent_filtros)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ent_filtros)
            Const sp As String = "SELECT * FROM I_nav_ent_filtros WHERE idnavent = @idnavent"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@idnavent", IdNavEnt)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ent_filtros As New clsBeI_nav_ent_filtros

            For Each dr As DataRow In dt.Rows
                vBeI_nav_ent_filtros = New clsBeI_nav_ent_filtros
                Cargar(vBeI_nav_ent_filtros, dr)
                lReturnList.Add(vBeI_nav_ent_filtros)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_By_IdNavEnt(ByVal IdNavEnt As pEntidadesSycn,
                                               ByRef lConnection As SqlConnection,
                                               ByRef lTransaction As SqlTransaction) As List(Of clsBeI_nav_ent_filtros)

        Try

            Dim lReturnList As New List(Of clsBeI_nav_ent_filtros)
            Const sp As String = "SELECT * FROM I_nav_ent_filtros WHERE idnavent = @idnavent"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@idnavent", IdNavEnt)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeI_nav_ent_filtros As New clsBeI_nav_ent_filtros

            For Each dr As DataRow In dt.Rows
                vBeI_nav_ent_filtros = New clsBeI_nav_ent_filtros
                Cargar(vBeI_nav_ent_filtros, dr)
                lReturnList.Add(vBeI_nav_ent_filtros)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
