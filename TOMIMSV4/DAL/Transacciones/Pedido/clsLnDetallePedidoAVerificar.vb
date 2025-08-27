Imports System.Reflection

Public Class clsLnDetallePedidoAVerificar

    Public Shared Sub Cargar(ByRef oBeDetallePedidoAVerificar As clsBeDetallePedidoAVerificar, ByRef dr As DataRow)

        Try

            With oBeDetallePedidoAVerificar

                .IdPedidoEnc = IIf(IsDBNull(dr.Item("IdPedidoEnc")), 0, dr.Item("IdPedidoEnc"))
                .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                .Lote = IIf(IsDBNull(dr.Item("Lote")), "", dr.Item("Lote"))
                .LicPlate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                .Nom_Unid_Med = IIf(IsDBNull(dr.Item("Nom_Unid_Med")), "", dr.Item("Nom_Unid_Med"))
                .Fecha_Vence = IIf(IsDBNull(dr.Item("Fecha_Vence")), Now, dr.Item("Fecha_Vence"))
                .Nom_Presentacion = IIf(IsDBNull(dr.Item("Nom_Presentacion")), "", dr.Item("Nom_Presentacion"))
                .Nombre_Producto = IIf(IsDBNull(dr.Item("Nombre_Producto")), "", dr.Item("Nombre_Producto"))
                .Nom_Estado = IIf(IsDBNull(dr.Item("Nom_Estado")), "", dr.Item("Nom_Estado"))
                .Cantidad_Solicitada = IIf(IsDBNull(dr.Item("Cantidad_Solicitada")), 0, dr.Item("Cantidad_Solicitada"))
                .Cantidad_Recibida = IIf(IsDBNull(dr.Item("Cantidad_Recibida")), 0, dr.Item("Cantidad_Recibida"))
                .Cantidad_Verificada = IIf(IsDBNull(dr.Item("Cantidad_Verificada")), 0, dr.Item("Cantidad_Verificada"))
                .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacionPicking")), 0, dr.Item("IdPresentacionPicking"))
                .IdUnidadMedidaBasica = IIf(IsDBNull(dr.Item("IdUnidadMedidaBasica")), 0, dr.Item("IdUnidadMedidaBasica"))
                .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                .Codigo = IIf(IsDBNull(dr.Item("Codigo")), 0, dr.Item("Codigo"))
                .NDias = IIf(IsDBNull(dr.Item("NDias")), 0, dr.Item("NDias")) '#CKFK 20180502 04:15 PM Agregué este campo porque me hace falta para poder listar el inventario disponible al realizar reemplazos
                .NombreArea = IIf(IsDBNull(dr.Item("NombreArea")), "", dr.Item("NombreArea"))
                .NombreClasificacion = IIf(IsDBNull(dr.Item("NombreClasificacion")), "", dr.Item("NombreClasificacion"))
                .Bono = IIf(IsDBNull(dr.Item("Bono")), "", dr.Item("Bono"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub


End Class
