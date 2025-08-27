Imports System.Data.SqlClient
Imports System.Reflection

Partial Public Class clsLn_vw_ajustes
    Public Shared Function GetAll_Pendientes_Envio() As List(Of clsBe_vw_ajustes)

        Try

            Dim lReturnList As New List(Of clsBe_vw_ajustes)
            Const sp As String = "SELECT * FROM vw_ajustes 
                                  WHERE Enviado = 0"
            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeT_vw_ajustes As New clsBe_vw_ajustes

            For Each dr As DataRow In dt.Rows
                vBeT_vw_ajustes = New clsBe_vw_ajustes
                Cargar(vBeT_vw_ajustes, dr)
                lReturnList.Add(vBeT_vw_ajustes)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Pendientes_Envio(ByVal IdAjusteEnc As Integer) As List(Of clsBe_vw_ajustes)

        Try

            Dim lReturnList As New List(Of clsBe_vw_ajustes)

            Const sp As String = "SELECT * FROM vw_ajustes 
                                  WHERE IdAjusteEnc = @IdAjusteEnc
                                  AND Enviado = 0"

            Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))

            Dim cmd As New SqlCommand(sp, lConnection) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            dad.SelectCommand.Parameters.AddWithValue("@IdAjusteEnc", IdAjusteEnc)
            dad.Fill(dt)

            Dim vBeT_vw_ajustes As New clsBe_vw_ajustes

            For Each dr As DataRow In dt.Rows
                vBeT_vw_ajustes = New clsBe_vw_ajustes
                Cargar(vBeT_vw_ajustes, dr)
                lReturnList.Add(vBeT_vw_ajustes)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    Public Shared Function Get_All_Pendientes_Envio(ByVal IdAjusteEnc As Integer,
                                                    ByRef lConnection As SqlConnection,
                                                    ByRef lTransaction As SqlTransaction) As List(Of clsBe_vw_ajustes)

        Try

            Dim lReturnList As New List(Of clsBe_vw_ajustes)

            Const sp As String = "SELECT * FROM vw_ajustes 
                                  WHERE IdAjusteEnc = @IdAjusteEnc
                                  AND Enviado = 0"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.AddWithValue("@IdAjusteEnc", IdAjusteEnc)
            dad.Fill(dt)

            Dim vBeT_vw_ajustes As New clsBe_vw_ajustes

            For Each dr As DataRow In dt.Rows
                vBeT_vw_ajustes = New clsBe_vw_ajustes
                Cargar(vBeT_vw_ajustes, dr)
                lReturnList.Add(vBeT_vw_ajustes)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

    '#CKFK20241212 Obtener todos los ajustes agrupados por inventario
    Public Shared Function Get_All_Pendientes_Envio_Agrupados_By_Inventario(ByVal IdAjusteEnc As Integer,
                                                                            ByRef lConnection As SqlConnection,
                                                                            ByRef lTransaction As SqlTransaction) As List(Of clsBe_vw_ajustes)

        Try

            Dim lReturnList As New List(Of clsBe_vw_ajustes)

            Const sp As String = "SELECT MIN(idajusteenc) idajusteenc, MIN(idajustedet) idajustedet,fecha,
                                         referencia, codigo_producto, nombre_producto, IdPresentacion,UMBas,
	                                     IdBodegaERP, Codigo_Bodega, Nombre_Bodega, MAX(cantidad_original) cantidad_original, 
	                                     SUM(cantidad_nueva) cantidad_nueva, SUM(peso_nuevo) peso_nuevo, SUM(peso_original) peso_original, 
	                                     MAX(fecha_vence_nueva)fecha_vence_nueva, MAX(fecha_vence_nueva)fecha_vence_original, 
	                                     lote_original, lote_original lote_nuevo, 
	                                     CASE WHEN MAX(cantidad_nueva) -max(cantidad_original) >0 THEN 3 ELSE 5 END Tipo_Ajuste, 
	                                     1 modifica_cantidad, enviado, Motivo_Ajuste, observacion, codigo_ajuste, IdProductoFamilia, 
                                         Nombre_Presentacion, Factor, Codigo_Centro_Costo, Nombre_Centro_Costo, ajuste_por_inventario
                                  FROM vw_ajustes 
                                  WHERE Enviado = 0 and 
                                        ajuste_por_inventario in (SELECT ajuste_por_inventario 
                                                                  FROM trans_ajuste_enc  
                                                                  WHERE idajusteenc = @IdAjusteEnc )
                                  GROUP BY fecha, referencia, codigo_producto, nombre_producto, IdPresentacion, UMBas,enviado, 
		                                   Motivo_Ajuste, observacion, codigo_ajuste, IdProductoFamilia, Nombre_Presentacion,  
		                                   Factor, Codigo_Centro_Costo, Nombre_Centro_Costo, ajuste_por_inventario,
	                                       IdBodegaERP, Codigo_Bodega, Nombre_Bodega, lote_original"

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.SelectCommand.Parameters.AddWithValue("@IdAjusteEnc", IdAjusteEnc)
            dad.Fill(dt)

            Dim vBeT_vw_ajustes As New clsBe_vw_ajustes

            For Each dr As DataRow In dt.Rows
                vBeT_vw_ajustes = New clsBe_vw_ajustes
                Cargar(vBeT_vw_ajustes, dr)
                lReturnList.Add(vBeT_vw_ajustes)
            Next

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
