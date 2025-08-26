Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnTrans_inv_resumen_grid

    Public Shared Sub Cargar(ByRef oBeTrans_inv_resumen As clsBeTrans_inv_resumen_grid, ByRef dr As DataRow)

        Try
            With oBeTrans_inv_resumen
                .Idinventariores = IIf(IsDBNull(dr.Item("idinventariores")), 0, dr.Item("idinventariores"))
                .Idinventarioenct = IIf(IsDBNull(dr.Item("idinventarioenct")), 0, dr.Item("idinventarioenct"))
                .Idtramo = IIf(IsDBNull(dr.Item("idtramo")), 0, dr.Item("idtramo"))
                .Idproducto = IIf(IsDBNull(dr.Item("idproducto")), 0, dr.Item("idproducto"))
                .UnidadMedida = IIf(IsDBNull(dr.Item("unimed")), "", dr.Item("unimed"))
                .presentacion = IIf(IsDBNull(dr.Item("presentacion")), " ", dr.Item("presentacion"))
                .productoestado = IIf(IsDBNull(dr.Item("estado")), "", dr.Item("estado"))
                .Cantidad = IIf(IsDBNull(dr.Item("cantidad")), 0.0, dr.Item("cantidad"))
                .IdUbicacion = IIf(IsDBNull(dr.Item("idubicacion")), 0, dr.Item("idubicacion"))
                .IdBodega = IIf(IsDBNull(dr.Item("idbodega")), 0, dr.Item("idbodega"))
                .Nom_Ubicacion = IIf(IsDBNull(dr.Item("nom_ubicacion")), "", dr.Item("nom_ubicacion"))
            End With
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try
    End Sub

    Public Shared Function GetAll() As List(Of clsBeTrans_inv_resumen_grid)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            Dim lReturnList As New List(Of clsBeTrans_inv_resumen_grid)

            Const sp As String = "SELECT  trans_inv_resumen.idinventariores, trans_inv_resumen.idinventarioenct, trans_inv_resumen.idtramo, trans_inv_resumen.idproducto, producto_estado.nombre AS estado, 
                trans_inv_resumen.idubicacion, producto_bodega.IdBodega as idbodega, dbo.Nombre_Completo_Ubicacion(trans_inv_resumen.idubicacion, producto_bodega.IdBodega) as nom_ubicacion,
                producto_presentacion.nombre AS presentacion, trans_inv_resumen.cantidad, unidad_medida.Nombre AS unimed
                FROM  trans_inv_resumen INNER JOIN
                producto_estado ON trans_inv_resumen.idproductoestado = producto_estado.IdEstado INNER JOIN
                unidad_medida ON trans_inv_resumen.IdUnidadMedida = unidad_medida.IdUnidadMedida LEFT OUTER JOIN
                producto_presentacion ON trans_inv_resumen.idpresentacion = producto_presentacion.IdPresentacion AND trans_inv_resumen.idproducto = producto_presentacion.IdProducto
				INNER JOIN producto_bodega on trans_inv_resumen.idproducto = producto_bodega.IdProducto"

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeTrans_inv_resumen As New clsBeTrans_inv_resumen_grid

            For Each dr As DataRow In dt.Rows
                vBeTrans_inv_resumen = New clsBeTrans_inv_resumen_grid
                Cargar(vBeTrans_inv_resumen, dr)
                lReturnList.Add(vBeTrans_inv_resumen)
            Next

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_All_Inventario_Inicial_By_IdInventario_Enc_And_Idtramo_And_IdProducto(pIdinventarioenct As Integer, pIdtramo As Integer, pIdProducto As Integer, pIdUbicacion As Integer, pIdBodega As Integer) As List(Of clsBeTrans_inv_resumen_grid)

        Dim lReturnList As New List(Of clsBeTrans_inv_resumen_grid)

        Try

            lReturnList = GetAll().FindAll(Function(x) x.Idinventarioenct = pIdinventarioenct And x.IdBodega = pIdBodega).OrderBy(Function(Y) Y.productoestado).ThenBy(Function(z) z.presentacion).ToList()

            If (pIdtramo <> 0) Then lReturnList = lReturnList.FindAll(Function(x) x.Idtramo = pIdtramo).ToList()
            If (pIdProducto <> 0) Then lReturnList = lReturnList.FindAll(Function(x) x.Idproducto = pIdProducto).ToList()
            If (pIdUbicacion <> 0) Then lReturnList = lReturnList.FindAll(Function(x) x.IdUbicacion = pIdUbicacion).ToList()

            Return lReturnList

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Function

End Class
