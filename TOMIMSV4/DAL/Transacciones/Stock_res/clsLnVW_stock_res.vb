Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsLnVW_stock_res

    Private Shared Property lProductosInMemory As New List(Of clsBeProducto)

    Public Shared Sub Cargar(ByRef oBeT_vw_stock_res As clsBeVW_stock_res,
                             ByRef dr As DataRow,
                             ByRef lConnection As SqlConnection,
                             ByRef lTransaction As SqlTransaction)

        Try

            With oBeT_vw_stock_res

                If dr.Table.Columns.Contains("IdBodega") Then .IdBodega = IIf(IsDBNull(dr.Item("IdBodega")), 0, dr.Item("IdBodega"))
                If dr.Table.Columns.Contains("IdPropietario") Then .IdPropietario = IIf(IsDBNull(dr.Item("IdPropietario")), 0, dr.Item("IdPropietario"))
                If dr.Table.Columns.Contains("IdPropietarioBodega") Then .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))
                If dr.Table.Columns.Contains("IdProducto") Then .IdProducto = IIf(IsDBNull(dr.Item("IdProducto")), 0, dr.Item("IdProducto"))
                If dr.Table.Columns.Contains("IdProductoBodega") Then .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                If dr.Table.Columns.Contains("IdStock") Then .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                If dr.Table.Columns.Contains("IdUbicacionActual") Then .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacionActual")), 0, dr.Item("IdUbicacionActual"))
                If dr.Table.Columns.Contains("IdUbicacion_anterior") Then .IdUbicacion_Anterior = Val(IIf(IsDBNull(dr.Item("IdUbicacion_anterior")), 0, dr.Item("IdUbicacion_anterior")))
                If dr.Table.Columns.Contains("IdUnidadMedida") Then .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                If dr.Table.Columns.Contains("IdProductoEstado") Then .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                If dr.Table.Columns.Contains("IdPresentacion") Then .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                If dr.Table.Columns.Contains("IdRecepcionEnc") Then .IdRecepcionEnc = IIf(IsDBNull(dr.Item("IdRecepcionEnc")), 0, dr.Item("IdRecepcionEnc"))
                If dr.Table.Columns.Contains("IdRecepcionDet") Then .IdRecepcionDet = IIf(IsDBNull(dr.Item("IdRecepcionDet")), 0, dr.Item("IdRecepcionDet"))
                If dr.Table.Columns.Contains("Propietario") Then .Propietario = IIf(IsDBNull(dr.Item("Propietario")), "", dr.Item("Propietario"))
                If dr.Table.Columns.Contains("UnidadMedida") Then .UMBas = IIf(IsDBNull(dr.Item("UnidadMedida")), "", dr.Item("UnidadMedida"))
                If dr.Table.Columns.Contains("Presentacion") Then .Nombre_Presentacion = IIf(IsDBNull(dr.Item("Presentacion")), "", dr.Item("Presentacion"))
                If dr.Table.Columns.Contains("Codigo") Then .Codigo_Producto = IIf(IsDBNull(dr.Item("codigo")), "", dr.Item("codigo"))
                If dr.Table.Columns.Contains("Nombre") Then .Nombre_Producto = IIf(IsDBNull(dr.Item("nombre")), "", dr.Item("nombre"))
                If dr.Table.Columns.Contains("Lote") Then .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                If dr.Table.Columns.Contains("Fecha_ingreso") Then .Fecha_ingreso = IIf(IsDBNull(dr.Item("fecha_ingreso")), Date.Now, dr.Item("fecha_ingreso"))
                If dr.Table.Columns.Contains("Serial") Then .Serial = IIf(IsDBNull(dr.Item("serial")), "", dr.Item("serial"))
                If dr.Table.Columns.Contains("Añada") Then .Añada = IIf(IsDBNull(dr.Item("añada")), 0, dr.Item("añada"))
                If dr.Table.Columns.Contains("CantidadSF") Then .CantidadUmBas = IIf(IsDBNull(dr.Item("CantidadSF")), 0.0, dr.Item("CantidadSF"))
                If dr.Table.Columns.Contains("Factor") Then .Factor = IIf(IsDBNull(dr.Item("factor")), 0.0, dr.Item("factor"))
                If dr.Table.Columns.Contains("Cantidad") Then .CantidadPresentacion = IIf(IsDBNull(dr.Item("Cantidad")), 0.0, dr.Item("Cantidad"))
                If dr.Table.Columns.Contains("Fecha_vence") Then .Fecha_Vence = IIf(IsDBNull(dr.Item("fecha_vence")), "01/01/1990", dr.Item("fecha_vence"))
                If dr.Table.Columns.Contains("NomEstado") Then .NomEstado = IIf(IsDBNull(dr.Item("NomEstado")), "", dr.Item("NomEstado"))
                If dr.Table.Columns.Contains("EstadoUtilizable") Then .EstadoUtilizable = IIf(IsDBNull(dr.Item("EstadoUtilizable")), False, dr.Item("EstadoUtilizable"))
                If dr.Table.Columns.Contains("Dañado") Then .Dañado = IIf(IsDBNull(dr.Item("dañado")), False, dr.Item("dañado"))
                If dr.Table.Columns.Contains("IdUbicacion") Then .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                If dr.Table.Columns.Contains("Lic_plate") Then .Lic_plate = IIf(IsDBNull(dr.Item("lic_plate")), "", dr.Item("lic_plate"))
                If dr.Table.Columns.Contains("Peso") Then .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                If dr.Table.Columns.Contains("IdIndiceRotacion") Then .IdIndiceRotacion = IIf(IsDBNull(dr.Item("IdIndiceRotacion")), 0, dr.Item("IdIndiceRotacion"))
                If dr.Table.Columns.Contains("Alto") Then .AltoUbicacion = IIf(IsDBNull(dr.Item("alto")), 0.0, dr.Item("alto"))
                If dr.Table.Columns.Contains("Largo") Then .LargoUbicacion = IIf(IsDBNull(dr.Item("largo")), 0.0, dr.Item("largo"))
                If dr.Table.Columns.Contains("Ancho") Then .AnchoUbicacion = IIf(IsDBNull(dr.Item("ancho")), 0.0, dr.Item("ancho"))
                If dr.Table.Columns.Contains("CantidadReservada") Then .CantidadReservadaUMBas = IIf(IsDBNull(dr.Item("CantidadReservada")), 0.0, dr.Item("CantidadReservada"))
                If dr.Table.Columns.Contains("IdTramo") Then .IdTramo = IIf(IsDBNull(dr.Item("IdTramo")), 0, dr.Item("IdTramo"))
                If dr.Table.Columns.Contains("Ancho_ubicacion") Then .Ancho_ubicacion = IIf(IsDBNull(dr.Item("ancho_ubicacion")), 0.0, dr.Item("ancho_ubicacion"))
                If dr.Table.Columns.Contains("Largo_ubicacion") Then .Largo_ubicacion = IIf(IsDBNull(dr.Item("largo_ubicacion")), 0.0, dr.Item("largo_ubicacion"))
                If dr.Table.Columns.Contains("Alto_ubicacion") Then .Alto_ubicacion = IIf(IsDBNull(dr.Item("alto_ubicacion")), 0.0, dr.Item("alto_ubicacion"))
                If dr.Table.Columns.Contains("IndiceRotacion") Then .IndiceRotacion = IIf(IsDBNull(dr.Item("IndiceRotacion")), "", dr.Item("IndiceRotacion"))
                If dr.Table.Columns.Contains("Existencia_min_umbas") Then .Existencia_min_umbas = IIf(IsDBNull(dr.Item("existencia_min_umbas")), 0.0, dr.Item("existencia_min_umbas"))
                If dr.Table.Columns.Contains("Existencia_max_umbas") Then .Existencia_max_umbas = IIf(IsDBNull(dr.Item("existencia_max_umbas")), 0.0, dr.Item("existencia_max_umbas"))
                If dr.Table.Columns.Contains("Existencia_min_pres") Then .Existencia_min_pres = IIf(IsDBNull(dr.Item("Existencia_min_pres")), 0.0, dr.Item("Existencia_min_pres"))
                If dr.Table.Columns.Contains("Existencia_max_pres") Then .Existencia_max_pres = IIf(IsDBNull(dr.Item("Existencia_max_pres")), 0.0, dr.Item("Existencia_max_pres"))
                If dr.Table.Columns.Contains("Codigo_barra") Then .Codigo_Barra = IIf(IsDBNull(dr.Item("codigo_barra")), "", dr.Item("codigo_barra"))
                If dr.Table.Columns.Contains("Costo") Then .Costo = IIf(IsDBNull(dr.Item("costo")), 0.0, dr.Item("costo"))
                If dr.Table.Columns.Contains("IdPedido") Then .IdPedido = IIf(IsDBNull(dr.Item("IdPedido")), 0, dr.Item("IdPedido"))
                If dr.Table.Columns.Contains("IdPedidoDet") Then .IdPedidoDet = IIf(IsDBNull(dr.Item("IdPedidoDet")), 0, dr.Item("IdPedidoDet"))
                If dr.Table.Columns.Contains("IdPicking") Then .IdPicking = IIf(IsDBNull(dr.Item("IdPicking")), 0, dr.Item("IdPicking"))
                If dr.Table.Columns.Contains("Tolerancia") Then .Tolerancia = IIf(IsDBNull(dr.Item("Tolerancia")), 0, dr.Item("Tolerancia"))
                oBeT_vw_stock_res.IdProducto = .IdProducto
                '#EJC20180125: Se debe mejorar esto porque hace muy lenta la carga del stock.
                Get_Parametros_Dimesion_UmBas(oBeT_vw_stock_res, lConnection, lTransaction)
                Get_Cantidad_Presentacion(oBeT_vw_stock_res, lConnection, lTransaction)
                If dr.Table.Columns.Contains("atributo_variante_1") Then .Atributo_variante_1 = IIf(IsDBNull(dr.Item("atributo_variante_1")), "", dr.Item("atributo_variante_1"))
                If dr.Table.Columns.Contains("ubicacion_nivel") Then .Ubicacion_Nivel = IIf(IsDBNull(dr.Item("ubicacion_nivel")), "0", dr.Item("ubicacion_nivel"))
                If dr.Table.Columns.Contains("ubicacion_nombre") Then .Ubicacion_Nombre = IIf(IsDBNull(dr.Item("ubicacion_nombre")), "", dr.Item("Ubicacion_Nombre"))
                If dr.Table.Columns.Contains("ubicacion_indice_x") Then .Ubicacion_Indice_x = IIf(IsDBNull(dr.Item("ubicacion_indice_x")), "0", dr.Item("ubicacion_indice_x"))
                If dr.Table.Columns.Contains("ubicacion_tramo") Then .Ubicacion_Tramo = IIf(IsDBNull(dr.Item("ubicacion_tramo")), "", dr.Item("ubicacion_tramo"))
                If dr.Table.Columns.Contains("IdTipoProducto") Then .IdTipoProducto = IIf(IsDBNull(dr.Item("IdTipoProducto")), 0, dr.Item("IdTipoProducto"))
                If dr.Table.Columns.Contains("NombreTipoProducto") Then .NombreTipoProducto = IIf(IsDBNull(dr.Item("NombreTipoProducto")), "", dr.Item("NombreTipoProducto"))
                If dr.Table.Columns.Contains("Pallet_No_Estandar") Then .Pallet_No_Estandar = IIf(IsDBNull(dr.Item("Pallet_No_Estandar")), False, dr.Item("Pallet_No_Estandar"))
                If dr.Table.Columns.Contains("Posiciones") Then .Posiciones = IIf(IsDBNull(dr.Item("Posiciones")), 0, dr.Item("Posiciones"))
                'GT 040820211152:campos de poliza para cealsa
                If dr.Table.Columns.Contains("codigo_poliza") Then .codigo_poliza = IIf(IsDBNull(dr.Item("codigo_poliza")), 0, dr.Item("codigo_poliza"))
                If dr.Table.Columns.Contains("Numero_poliza") Then .Numero_poliza = IIf(IsDBNull(dr.Item("Numero_poliza")), 0, dr.Item("Numero_poliza"))
                '#EJC20211216_1305:
                If dr.Table.Columns.Contains("ubicacion_picking") Then .ubicacion_picking = IIf(IsDBNull(dr.Item("ubicacion_picking")), 0, dr.Item("ubicacion_picking"))

                '#EJC20220129: 
                If dr.Table.Columns.Contains("CajasPorCama") Then .CajasPorCama = IIf(IsDBNull(dr.Item("CajasPorCama")), 0, dr.Item("CajasPorCama"))
                If dr.Table.Columns.Contains("CamasPorTarima") Then .CamasPorTarima = IIf(IsDBNull(dr.Item("CamasPorTarima")), 0, dr.Item("CamasPorTarima"))
                If dr.Table.Columns.Contains("es_rack") Then .es_rack = IIf(IsDBNull(dr.Item("es_rack")), False, dr.Item("es_rack"))

                '#EJC20220303
                If dr.Table.Columns.Contains("IdStockRes") Then .IdStockRes = IIf(IsDBNull(dr.Item("IdStockRes")), 0, dr.Item("IdStockRes"))

                '#EJC20220309
                If dr.Table.Columns.Contains("Area") Then .Area = IIf(IsDBNull(dr.Item("Area")), "", dr.Item("Area"))
                If dr.Table.Columns.Contains("Clasificacion") Then .Nombre_Clasificacion = IIf(IsDBNull(dr.Item("Clasificacion")), "", dr.Item("Clasificacion"))

                '#AT 20220314 Ubicación completa
                If dr.Table.Columns.Contains("Nombre_Completo") Then .Nombre_Completo = IIf(IsDBNull(dr.Item("Nombre_Completo")), "", dr.Item("Nombre_Completo"))

                If dr.Table.Columns.Contains("Fecha_Preparacion") Then .Fecha_Preparacion = IIf(IsDBNull(dr.Item("Fecha_Preparacion")), New Date(1900, 1, 1), dr.Item("Fecha_Preparacion"))
                If dr.Table.Columns.Contains("Fecha_Pedido") Then .Fecha_Pedido = IIf(IsDBNull(dr.Item("Fecha_Pedido")), New Date(1900, 1, 1), dr.Item("Fecha_Pedido"))

                If dr.Table.Columns.Contains("Codigo_Talla") Then .Codigo_Talla = IIf(IsDBNull(dr.Item("Codigo_Talla")), "", dr.Item("Codigo_Talla"))
                If dr.Table.Columns.Contains("Nombre_Talla") Then .Nombre_Talla = IIf(IsDBNull(dr.Item("Nombre_Talla")), "", dr.Item("Nombre_Talla"))
                If dr.Table.Columns.Contains("Codigo_Color") Then .Codigo_Color = IIf(IsDBNull(dr.Item("Codigo_Color")), "", dr.Item("Codigo_Color"))
                If dr.Table.Columns.Contains("Nombre_Color") Then .Nombre_Color = IIf(IsDBNull(dr.Item("Nombre_Color")), "", dr.Item("Nombre_Color"))
                If dr.Table.Columns.Contains("IdProductoTallaColor") Then .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
                If dr.Table.Columns.Contains("Talla") Then .Codigo_Talla = IIf(IsDBNull(dr.Item("Talla")), 0, dr.Item("Talla"))
                If dr.Table.Columns.Contains("Color") Then .Codigo_Color = IIf(IsDBNull(dr.Item("Color")), 0, dr.Item("Color"))

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Sub Cargar_CI(ByRef oBeT_vw_stock_res As clsBeVW_stock_res_CI,
                                ByRef dr As DataRow)

        Try

            With oBeT_vw_stock_res

                If dr.Table.Columns.Contains("Codigo") Then .Codigo = IIf(IsDBNull(dr.Item("Codigo")), 0, dr.Item("Codigo"))
                If dr.Table.Columns.Contains("Nombre") Then .Nombre = IIf(IsDBNull(dr.Item("Nombre")), 0, dr.Item("Nombre"))
                If dr.Table.Columns.Contains("UM") Then .UM = IIf(IsDBNull(dr.Item("UM")), 0, dr.Item("UM"))
                If dr.Table.Columns.Contains("ExistUMBAs") Then .ExistUMBAs = IIf(IsDBNull(dr.Item("ExistUMBAs")), 0, dr.Item("ExistUMBAs"))
                If dr.Table.Columns.Contains("Pres") Then .Pres = IIf(IsDBNull(dr.Item("Pres")), 0, dr.Item("Pres"))
                If dr.Table.Columns.Contains("ExistPres") Then .ExistPres = IIf(IsDBNull(dr.Item("ExistPres")), 0, dr.Item("ExistPres"))
                If dr.Table.Columns.Contains("ReservadoUMBAs") Then .ReservadoUMBAs = IIf(IsDBNull(dr.Item("ReservadoUMBAs")), 0, dr.Item("ReservadoUMBAs"))
                If dr.Table.Columns.Contains("DisponibleUMBas") Then .DisponibleUMBas = IIf(IsDBNull(dr.Item("DisponibleUMBas")), 0, dr.Item("DisponibleUMBas"))
                If dr.Table.Columns.Contains("Lote") Then .Lote = IIf(IsDBNull(dr.Item("Lote")), 0, dr.Item("Lote"))
                If dr.Table.Columns.Contains("Vence") Then .Fecha_Vence = IIf(IsDBNull(dr.Item("Vence")), New Date(1900, 1, 1), dr.Item("Vence"))
                If dr.Table.Columns.Contains("Fecha_Vence") Then .Fecha_Vence = IIf(IsDBNull(dr.Item("Fecha_Vence")), New Date(1900, 1, 1), dr.Item("Fecha_Vence"))
                If dr.Table.Columns.Contains("Estado") Then .Estado = IIf(IsDBNull(dr.Item("Estado")), 0, dr.Item("Estado"))
                If dr.Table.Columns.Contains("Ubic") Then .Ubic = IIf(IsDBNull(dr.Item("Ubic")), 0, dr.Item("Ubic"))
                If dr.Table.Columns.Contains("idUbic") Then .idUbic = IIf(IsDBNull(dr.Item("idUbic")), 0, dr.Item("idUbic"))
                If dr.Table.Columns.Contains("Pedido") Then .Pedido = IIf(IsDBNull(dr.Item("Pedido")), 0, dr.Item("Pedido"))
                If dr.Table.Columns.Contains("Pick") Then .Pick = IIf(IsDBNull(dr.Item("Pick")), 0, dr.Item("Pick"))
                If dr.Table.Columns.Contains("LicPlate") Then .LicPlate = IIf(IsDBNull(dr.Item("LicPlate")), "", dr.Item("LicPlate"))
                If dr.Table.Columns.Contains("IdProductoEstado") Then .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                If dr.Table.Columns.Contains("IdProductoBodega") Then .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                If dr.Table.Columns.Contains("factor") Then .factor = IIf(IsDBNull(dr.Item("factor")), 0, dr.Item("factor"))
                If dr.Table.Columns.Contains("ingreso") Then .ingreso = IIf(IsDBNull(dr.Item("ingreso")), New Date(1900, 1, 1), dr.Item("ingreso"))
                If dr.Table.Columns.Contains("IdTipoEtiqueta") Then .IdTipoEtiqueta = IIf(IsDBNull(dr.Item("IdTipoEtiqueta")), 0, dr.Item("IdTipoEtiqueta"))
                If dr.Table.Columns.Contains("DispPres") Then .DispPres = IIf(IsDBNull(dr.Item("DispPres")), 0, dr.Item("DispPres"))
                If dr.Table.Columns.Contains("ResPres") Then .ResPres = IIf(IsDBNull(dr.Item("ResPres")), 0, dr.Item("ResPres"))
                If dr.Table.Columns.Contains("NombreArea") Then .NombreArea = IIf(IsDBNull(dr.Item("NombreArea")), "", dr.Item("NombreArea"))
                If dr.Table.Columns.Contains("IdArea") Then .IdArea = IIf(IsDBNull(dr.Item("IdArea")), 0, dr.Item("IdArea"))
                If dr.Table.Columns.Contains("Clasificacion") Then .Clasificacion = IIf(IsDBNull(dr.Item("Clasificacion")), "", dr.Item("Clasificacion"))
                If dr.Table.Columns.Contains("IdPresentacion") Then .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                '#AT20240805 Agregue el campo IdStock para la consulta detallada de existencia
                If dr.Table.Columns.Contains("IdStock") Then .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                If dr.Table.Columns.Contains("Codigo_Talla") Then .Codigo_Talla = IIf(IsDBNull(dr.Item("Codigo_Talla")), "", dr.Item("Codigo_Talla"))
                If dr.Table.Columns.Contains("Nombre_Talla") Then .Nombre_Talla = IIf(IsDBNull(dr.Item("Nombre_Talla")), "", dr.Item("Nombre_Talla"))
                If dr.Table.Columns.Contains("Codigo_Color") Then .Codigo_Color = IIf(IsDBNull(dr.Item("Codigo_Color")), "", dr.Item("Codigo_Color"))
                If dr.Table.Columns.Contains("Nombre_Color") Then .Nombre_Color = IIf(IsDBNull(dr.Item("Nombre_Color")), "", dr.Item("Nombre_Color"))
                If dr.Table.Columns.Contains("CodigoSKU") Then .CodigoSKU = IIf(IsDBNull(dr.Item("CodigoSKU")), "", dr.Item("CodigoSKU"))

                If dr.Table.Columns.Contains("IdUbicacion_anterior") Then .IdStock = IIf(IsDBNull(dr.Item("IdUbicacion_anterior")), 0, dr.Item("IdUbicacion_anterior"))
                If dr.Table.Columns.Contains("Codigo_Barra") Then .Codigo_Barra = IIf(IsDBNull(dr.Item("Codigo_Barra")), "", dr.Item("Codigo_Barra"))


            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    '#CKKF20220627 Creé esta función para cargar la información que vamos a mostrar como stock reservado en la HH
    Public Shared Sub Cargar_Res(ByRef oBeT_vw_stock_res As clsBeVW_stock_res,
                                 ByRef dr As DataRow,
                                 ByRef lConnection As SqlConnection,
                                 ByRef lTransaction As SqlTransaction)

        Try

            With oBeT_vw_stock_res

                If dr.Table.Columns.Contains("IdPedido") Then .IdPedido = IIf(IsDBNull(dr.Item("IdPedido")), 0, dr.Item("IdPedido"))
                If dr.Table.Columns.Contains("IdPicking") Then .IdPicking = IIf(IsDBNull(dr.Item("IdPicking")), 0, dr.Item("IdPicking"))
                If dr.Table.Columns.Contains("Codigo") Then .Codigo_Producto = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                If dr.Table.Columns.Contains("Nombre") Then .Nombre_Producto = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                If dr.Table.Columns.Contains("Cant_UMBas") Then .CantidadReservadaUMBas = IIf(IsDBNull(dr.Item("Cant_UMBas")), 0.0, dr.Item("Cant_UMBas"))
                If dr.Table.Columns.Contains("Cant_Presentacion") Then .CantidadPresentacion = IIf(IsDBNull(dr.Item("Cant_Presentacion")), 0.0, dr.Item("Cant_Presentacion"))
                If dr.Table.Columns.Contains("UMBas") Then .UMBas = IIf(IsDBNull(dr.Item("UMBas")), "", dr.Item("UMBas"))
                If dr.Table.Columns.Contains("Presentacion") Then .Nombre_Presentacion = IIf(IsDBNull(dr.Item("Presentacion")), "", dr.Item("Presentacion"))
                If dr.Table.Columns.Contains("EstadoProducto") Then .NomEstado = IIf(IsDBNull(dr.Item("EstadoProducto")), "", dr.Item("EstadoProducto"))
                If dr.Table.Columns.Contains("Licencia") Then .Lic_plate = IIf(IsDBNull(dr.Item("Licencia")), "", dr.Item("Licencia"))
                If dr.Table.Columns.Contains("Lote") Then .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                If dr.Table.Columns.Contains("FechaVence") Then .Fecha_Vence = IIf(IsDBNull(dr.Item("FechaVence")), "01/01/1990", dr.Item("FechaVence"))
                If dr.Table.Columns.Contains("UbicacionActual") Then .Ubicacion_Nombre = IIf(IsDBNull(dr.Item("UbicacionActual")), "", dr.Item("UbicacionActual"))
                If dr.Table.Columns.Contains("Nivel") Then .Ubicacion_Nivel = IIf(IsDBNull(dr.Item("Nivel")), 0, dr.Item("Nivel"))
                If dr.Table.Columns.Contains("IdProductoBodega") Then .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                If dr.Table.Columns.Contains("IdStockRes") Then .IdStockRes = IIf(IsDBNull(dr.Item("IdStockRes")), 0, dr.Item("IdStockRes"))
                If dr.Table.Columns.Contains("IdStock") Then .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                If dr.Table.Columns.Contains("Factor") Then .Factor = IIf(IsDBNull(dr.Item("factor")), 0.0, dr.Item("factor"))
                If dr.Table.Columns.Contains("Peso") Then .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                If dr.Table.Columns.Contains("Propietario") Then .Propietario = IIf(IsDBNull(dr.Item("Propietario")), "", dr.Item("Propietario"))
                If dr.Table.Columns.Contains("IdUbicacion") Then .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                If dr.Table.Columns.Contains("IdUnidadMedida") Then .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                If dr.Table.Columns.Contains("IdPresentacion") Then .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                If dr.Table.Columns.Contains("IdProductoEstado") Then .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                If dr.Table.Columns.Contains("Fecha_Preparacion") Then .Fecha_Preparacion = IIf(IsDBNull(dr.Item("Fecha_Preparacion")), New Date(1900, 1, 1), dr.Item("Fecha_Preparacion"))
                If dr.Table.Columns.Contains("Fecha_Pedido") Then .Fecha_Pedido = IIf(IsDBNull(dr.Item("Fecha_Pedido")), New Date(1900, 1, 1), dr.Item("Fecha_Pedido"))

                If dr.Table.Columns.Contains("Codigo_Talla") Then
                    .Codigo_Talla = IIf(IsDBNull(dr.Item("Codigo_Talla")), "", dr.Item("Codigo_Talla"))
                End If

                If dr.Table.Columns.Contains("Nombre_Talla") Then
                    .Nombre_Talla = IIf(IsDBNull(dr.Item("Nombre_Talla")), "", dr.Item("Nombre_Talla"))
                End If

                If dr.Table.Columns.Contains("Codigo_Color") Then
                    .Codigo_Color = IIf(IsDBNull(dr.Item("Codigo_Color")), "", dr.Item("Codigo_Color"))
                End If

                If dr.Table.Columns.Contains("Nombre_Color") Then
                    .Nombre_Color = IIf(IsDBNull(dr.Item("Nombre_Color")), "", dr.Item("Nombre_Color"))
                End If

                If dr.Table.Columns.Contains("IdProductoTallaColor") Then
                    .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
                End If

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    '#CKKF20220627 Creé esta función para cargar la información que vamos a mostrar para el reabastecimiento en la HH
    Public Shared Sub Cargar_Reabasto(ByRef oBeT_vw_stock_res As clsBeVW_stock_res,
                                      ByRef dr As DataRow,
                                      ByRef lConnection As SqlConnection,
                                      ByRef lTransaction As SqlTransaction)

        Try

            With oBeT_vw_stock_res

                If dr.Table.Columns.Contains("IdPedido") Then .IdPedido = IIf(IsDBNull(dr.Item("IdPedido")), 0, dr.Item("IdPedido"))
                If dr.Table.Columns.Contains("IdPicking") Then .IdPicking = IIf(IsDBNull(dr.Item("IdPicking")), 0, dr.Item("IdPicking"))
                If dr.Table.Columns.Contains("Codigo") Then .Codigo_Producto = IIf(IsDBNull(dr.Item("Codigo")), "", dr.Item("Codigo"))
                If dr.Table.Columns.Contains("Nombre") Then .Nombre_Producto = IIf(IsDBNull(dr.Item("Nombre")), "", dr.Item("Nombre"))
                If dr.Table.Columns.Contains("Cant_UMBas") Then .CantidadReservadaUMBas = IIf(IsDBNull(dr.Item("Cant_UMBas")), 0.0, dr.Item("Cant_UMBas"))
                If dr.Table.Columns.Contains("Cant_Presentacion") Then .CantidadPresentacion = IIf(IsDBNull(dr.Item("Cant_Presentacion")), 0.0, dr.Item("Cant_Presentacion"))
                If dr.Table.Columns.Contains("UMBas") Then .UMBas = IIf(IsDBNull(dr.Item("UMBas")), "", dr.Item("UMBas"))
                If dr.Table.Columns.Contains("Presentacion") Then .Nombre_Presentacion = IIf(IsDBNull(dr.Item("Presentacion")), "", dr.Item("Presentacion"))
                If dr.Table.Columns.Contains("EstadoProducto") Then .NomEstado = IIf(IsDBNull(dr.Item("EstadoProducto")), "", dr.Item("EstadoProducto"))
                If dr.Table.Columns.Contains("Licencia") Then .Lic_plate = IIf(IsDBNull(dr.Item("Licencia")), "", dr.Item("Licencia"))
                If dr.Table.Columns.Contains("Lote") Then .Lote = IIf(IsDBNull(dr.Item("lote")), "", dr.Item("lote"))
                If dr.Table.Columns.Contains("FechaVence") Then .Fecha_Vence = IIf(IsDBNull(dr.Item("FechaVence")), "01/01/1990", dr.Item("FechaVence"))
                If dr.Table.Columns.Contains("UbicacionActual") Then .Ubicacion_Nombre = IIf(IsDBNull(dr.Item("UbicacionActual")), "", dr.Item("UbicacionActual"))
                If dr.Table.Columns.Contains("Nivel") Then .Ubicacion_Nivel = IIf(IsDBNull(dr.Item("Nivel")), 0, dr.Item("Nivel"))
                If dr.Table.Columns.Contains("IdProductoBodega") Then .IdProductoBodega = IIf(IsDBNull(dr.Item("IdProductoBodega")), 0, dr.Item("IdProductoBodega"))
                If dr.Table.Columns.Contains("IdStockRes") Then .IdStockRes = IIf(IsDBNull(dr.Item("IdStockRes")), 0, dr.Item("IdStockRes"))
                If dr.Table.Columns.Contains("IdStock") Then .IdStock = IIf(IsDBNull(dr.Item("IdStock")), 0, dr.Item("IdStock"))
                If dr.Table.Columns.Contains("Factor") Then .Factor = IIf(IsDBNull(dr.Item("factor")), 0.0, dr.Item("factor"))
                If dr.Table.Columns.Contains("Peso") Then .Peso = IIf(IsDBNull(dr.Item("peso")), 0.0, dr.Item("peso"))
                If dr.Table.Columns.Contains("Propietario") Then .Propietario = IIf(IsDBNull(dr.Item("Propietario")), "", dr.Item("Propietario"))
                If dr.Table.Columns.Contains("IdUbicacion") Then .IdUbicacion = IIf(IsDBNull(dr.Item("IdUbicacion")), 0, dr.Item("IdUbicacion"))
                If dr.Table.Columns.Contains("IdUnidadMedida") Then .IdUnidadMedida = IIf(IsDBNull(dr.Item("IdUnidadMedida")), 0, dr.Item("IdUnidadMedida"))
                If dr.Table.Columns.Contains("IdPresentacion") Then .IdPresentacion = IIf(IsDBNull(dr.Item("IdPresentacion")), 0, dr.Item("IdPresentacion"))
                If dr.Table.Columns.Contains("IdProductoEstado") Then .IdProductoEstado = IIf(IsDBNull(dr.Item("IdProductoEstado")), 0, dr.Item("IdProductoEstado"))
                If dr.Table.Columns.Contains("Fecha_Preparacion") Then .Fecha_Preparacion = IIf(IsDBNull(dr.Item("Fecha_Preparacion")), New Date(1900, 1, 1), dr.Item("Fecha_Preparacion"))
                If dr.Table.Columns.Contains("Fecha_Pedido") Then .Fecha_Pedido = IIf(IsDBNull(dr.Item("Fecha_Pedido")), New Date(1900, 1, 1), dr.Item("Fecha_Pedido"))
                If dr.Table.Columns.Contains("IdPropietarioBodega") Then .IdPropietarioBodega = IIf(IsDBNull(dr.Item("IdPropietarioBodega")), 0, dr.Item("IdPropietarioBodega"))

                If dr.Table.Columns.Contains("Codigo_Talla") Then
                    .Codigo_Talla = IIf(IsDBNull(dr.Item("Codigo_Talla")), "", dr.Item("Codigo_Talla"))
                End If

                If dr.Table.Columns.Contains("Nombre_Talla") Then
                    .Nombre_Talla = IIf(IsDBNull(dr.Item("Nombre_Talla")), "", dr.Item("Nombre_Talla"))
                End If

                If dr.Table.Columns.Contains("Codigo_Color") Then
                    .Codigo_Color = IIf(IsDBNull(dr.Item("Codigo_Color")), "", dr.Item("Codigo_Color"))
                End If

                If dr.Table.Columns.Contains("Nombre_Color") Then
                    .Nombre_Color = IIf(IsDBNull(dr.Item("Nombre_Color")), "", dr.Item("Nombre_Color"))
                End If

                If dr.Table.Columns.Contains("IdProductoTallaColor") Then
                    .IdProductoTallaColor = IIf(IsDBNull(dr.Item("IdProductoTallaColor")), 0, dr.Item("IdProductoTallaColor"))
                End If

            End With

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Shared Sub Get_Cantidad_Presentacion(ByRef oBeT_vw_stock_res As clsBeVW_stock_res)

        Try

            If oBeT_vw_stock_res.IdPresentacion <> 0 Then

                oBeT_vw_stock_res.BePresentacionProductoEnStock = New clsBeProducto_Presentacion

                oBeT_vw_stock_res.BePresentacionProductoEnStock.IdPresentacion = oBeT_vw_stock_res.IdPresentacion
                oBeT_vw_stock_res.BePresentacionProductoEnStock = clsLnProducto_presentacion.GetSingle(oBeT_vw_stock_res.BePresentacionProductoEnStock.IdPresentacion)

                If oBeT_vw_stock_res.BePresentacionProductoEnStock.EsPallet Then
                    oBeT_vw_stock_res.CantidadPresentacion = (oBeT_vw_stock_res.CantidadUmBas / (oBeT_vw_stock_res.BePresentacionProductoEnStock.Factor * oBeT_vw_stock_res.BePresentacionProductoEnStock.CajasPorCama * oBeT_vw_stock_res.BePresentacionProductoEnStock.CamasPorTarima))
                Else
                    oBeT_vw_stock_res.CantidadPresentacion = (oBeT_vw_stock_res.CantidadUmBas / oBeT_vw_stock_res.BePresentacionProductoEnStock.Factor)
                End If

            Else
                oBeT_vw_stock_res.CantidadPresentacion = 0 ' oBeT_vw_stock_res.CantidadUmBas
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Shared Sub Get_Cantidad_Presentacion(ByRef oBeT_vw_stock_res As clsBeVW_stock_res,
                                                 ByRef lConnection As SqlConnection,
                                                 ByRef lTransaction As SqlTransaction)

        Try

            Dim IdxPresentacion As Integer = -1

            If oBeT_vw_stock_res.IdPresentacion <> 0 Then

                oBeT_vw_stock_res.BePresentacionProductoEnStock = New clsBeProducto_Presentacion
                oBeT_vw_stock_res.BePresentacionProductoEnStock.IdPresentacion = oBeT_vw_stock_res.IdPresentacion
                oBeT_vw_stock_res.BePresentacionProductoEnStock = clsLnProducto_presentacion.GetSingle(oBeT_vw_stock_res.BePresentacionProductoEnStock.IdPresentacion, lConnection, lTransaction)

                Dim Denominador As Double = 0

                If oBeT_vw_stock_res.BePresentacionProductoEnStock.EsPallet Then
                    Denominador = (oBeT_vw_stock_res.BePresentacionProductoEnStock.Factor * oBeT_vw_stock_res.BePresentacionProductoEnStock.CajasPorCama * oBeT_vw_stock_res.BePresentacionProductoEnStock.CamasPorTarima)
                    oBeT_vw_stock_res.CantidadPresentacion = (oBeT_vw_stock_res.CantidadUmBas / IIf(Denominador = 0, 1, Denominador))
                Else
                    Denominador = oBeT_vw_stock_res.BePresentacionProductoEnStock.Factor
                    oBeT_vw_stock_res.CantidadPresentacion = (oBeT_vw_stock_res.CantidadUmBas / IIf(Denominador = 0, 1, Denominador))
                End If

            Else
                oBeT_vw_stock_res.CantidadPresentacion = 0
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Shared Sub Get_Parametros_Dimesion_UmBas(ByRef oBeT_vw_stock_res As clsBeVW_stock_res,
                                                     ByRef lConnection As SqlConnection,
                                                     ByRef lTransaction As SqlTransaction)

        Try

            Dim IdxProducto As Integer = 0
            Dim BeProducto As New clsBeProducto

            If oBeT_vw_stock_res.IdProducto <> 0 Then

                Dim vIdProducto As Integer = oBeT_vw_stock_res.IdProducto

                IdxProducto = lProductosInMemory.FindIndex(Function(x) x.IdProducto = vIdProducto)

                If IdxProducto = -1 Then

                    BeProducto = New clsBeProducto()
                    Dim pCampos(4) As clsBeProducto.ProdPropiedades
                    pCampos(0) = clsBeProducto.ProdPropiedades.Alto
                    pCampos(1) = clsBeProducto.ProdPropiedades.Largo
                    pCampos(2) = clsBeProducto.ProdPropiedades.Ancho
                    pCampos(3) = clsBeProducto.ProdPropiedades.Codigo
                    BeProducto.IdProducto = oBeT_vw_stock_res.IdProducto

                    BeProducto = clsLnProducto.GetSingle(BeProducto.IdProducto, pCampos, lConnection, lTransaction)
                    lProductosInMemory.Add(BeProducto)

                Else
                    BeProducto = lProductosInMemory(IdxProducto)
                End If

                If Not BeProducto Is Nothing Then
                    oBeT_vw_stock_res.AltoUMBas = BeProducto.Alto
                    oBeT_vw_stock_res.LargoUmBas = BeProducto.Largo
                    oBeT_vw_stock_res.AnchoUmBas = BeProducto.Ancho
                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Public Shared Function GetSingle(ByRef pBeT_vw_stock_res As clsBeVW_stock_res) As Boolean


        GetSingle = False

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM vw_stock_res WHERE IdStock = @IdStock"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdStock", pBeT_vw_stock_res.IdStock)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt.Rows.Count = 1 Then
                Cargar(pBeT_vw_stock_res, dt.Rows(0), lConnection, lTransaction)
                Return True
            End If

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Single_By_IdProducto(ByVal pIdProducto As Integer, ByVal pIdBodega As Integer)

        Dim pBeT_vw_stock_res As New clsBeVW_stock_res

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM vw_stock_res WHERE IdProducto = @IdProducto AND IdBodega = @IdBodega"
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            Dim dt As New DataTable
            dad.Fill(dt)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each lRow As DataRow In dt.Rows

                    pBeT_vw_stock_res = New clsBeVW_stock_res
                    Cargar(pBeT_vw_stock_res, lRow, lConnection, lTransaction)

                Next

            End If

            Return pBeT_vw_stock_res

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Lista_Stock(ByVal pStockRes As clsBeVW_stock_res,
                                           ByRef lConnection As SqlConnection,
                                           ByRef lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Get_Lista_Stock = Nothing

        Try

            Dim lReturnList As New List(Of clsBeVW_stock_res)

            Dim vSQL As String = "SELECT  *
                                  FROM  vw_stock_res 
                                  WHERE IdProductoBodega = @IdProductoBodega AND IdUbicacion  = @IdUbicacion 
                                  AND (Lote = @Lote OR @Lote IS NULL)  AND ISNULL(CONVERT(DATE, Fecha_Vence),CONVERT(DATE, '19000101')) = CONVERT(DATE, @Fecha_Vence)
                                  AND ISNULL(IdPresentacion,0) = @IdPresentacion AND IdProductoEstado = @IdProductoEstado"

            If pStockRes.IdStock <> 0 Then vSQL &= " AND IdStock = @IdStock "

            vSQL &= " Order By CantidadSF "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pStockRes.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pStockRes.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pStockRes.Lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence", pStockRes.Fecha_Vence)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pStockRes.IdPresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pStockRes.IdProductoEstado)

                If pStockRes.IdStock <> 0 Then
                    lDTA.SelectCommand.Parameters.AddWithValue("@IdStock", pStockRes.IdStock)
                End If

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        Obj = New clsBeVW_stock_res()
                        Cargar(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)
                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lista_Stock_By_IdStock(ByVal pStockRes As clsBeVW_stock_res,
                                                      ByRef lConnection As SqlConnection,
                                                      ByRef lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Get_Lista_Stock_By_IdStock = Nothing

        Try

            Dim lReturnList As New List(Of clsBeVW_stock_res)

            Dim vSQL As String = "SELECT  *
                    FROM  vw_stock_res 
                    WHERE IdProductoBodega = @IdProductoBodega AND IdUbicacion  = @IdUbicacion 
                    AND (Lote = @Lote OR @Lote IS NULL)  AND ISNULL(CONVERT(DATE, Fecha_Vence),CONVERT(DATE, '19000101')) = CONVERT(DATE, @Fecha_Vence)
                    AND ISNULL(IdPresentacion,0) = @IdPresentacion AND IdProductoEstado = @IdProductoEstado 
                    AND IdStock = @IdStock
                    Order By CantidadSF "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pStockRes.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pStockRes.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pStockRes.Lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence", pStockRes.Fecha_Vence)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pStockRes.IdPresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pStockRes.IdProductoEstado)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pStockRes.IdStock)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows
                        Obj = New clsBeVW_stock_res()
                        Cargar(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)
                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lista_Stock_By_Lic_Plate(ByVal pStockRes As clsBeVW_stock_res,
                                                        ByRef lConnection As SqlConnection,
                                                        ByRef lTransaction As SqlTransaction) As List(Of clsBeVW_stock_res)

        Get_Lista_Stock_By_Lic_Plate = Nothing

        Try

            Dim lReturnList As New List(Of clsBeVW_stock_res)

            Dim vSQL As String = "SELECT  *
                                  FROM  vw_stock_res 
                                  WHERE IdProductoBodega = @IdProductoBodega AND IdUbicacion  = @IdUbicacion 
                                  AND (Lote = @Lote OR @Lote IS NULL)  AND ISNULL(CONVERT(DATE, Fecha_Vence),CONVERT(DATE, '19000101')) = CONVERT(DATE, @Fecha_Vence)
                                  AND ISNULL(IdPresentacion,0) = @IdPresentacion 
                                  AND IdProductoEstado = @IdProductoEstado 
                                  AND Lic_Plate = @Lic_Plate 
                                  ORDER BY CantidadSF "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.CommandTimeout = 60
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoBodega", pStockRes.IdProductoBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pStockRes.IdUbicacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pStockRes.Lote)
                lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence", pStockRes.Fecha_Vence)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdPresentacion", pStockRes.IdPresentacion)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProductoEstado", pStockRes.IdProductoEstado)
                lDTA.SelectCommand.Parameters.AddWithValue("@Lic_Plate", pStockRes.Lic_plate)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then

                    For Each lRow As DataRow In lDataTable.Rows

                        Obj = New clsBeVW_stock_res()
                        Cargar(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)

                    Next

                End If

            End Using

            Return lReturnList

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function Get_Lista_Stock(ByVal pCodigo As String,
                                           ByVal pLote As String,
                                           ByVal pFechaVence As Date,
                                           ByVal IdBodega As Integer) As List(Of clsBeVW_stock_res)

        Get_Lista_Stock = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeVW_stock_res)

            Dim vSQL As String = "SELECT  *
                                  FROM  vw_stock_res
                                  WHERE codigo = @codigo "

            vSQL += " and IdBodega=@IdBodega "

            vSQL += " and (isnull(Lote,'') = @Lote COLLATE Latin1_General_CS_AS)  "

            If pFechaVence <> "01/01/1900" Then
                vSQL += " AND CONVERT(DATE, Fecha_Vence) = CONVERT(DATE, @Fecha_Vence) "
            End If

            vSQL += " Order By NombreUbicacion "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@codigo", pCodigo)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@Lote", pLote)
                If pFechaVence <> "01/01/1900" Then lDTA.SelectCommand.Parameters.AddWithValue("@Fecha_Vence", pFechaVence)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                Dim Obj As clsBeVW_stock_res

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    For Each lRow As DataRow In lDataTable.Rows
                        Obj = New clsBeVW_stock_res()
                        Cargar(Obj, lRow, lConnection, lTransaction)
                        lReturnList.Add(Obj)
                    Next
                End If

            End Using

            lTransaction.Commit()

            Return lReturnList

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Sub Actualizar_Pallet_No_Standar(ByRef pBeT_vw_stock_res As clsBeVW_stock_res)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim pBeStock_det As New clsBeStock_det
            Dim vBeStock_det As New clsBeStock_det

            vBeStock_det.IdStock = pBeT_vw_stock_res.IdStock
            vBeStock_det.Posiciones = pBeT_vw_stock_res.Posiciones


            'GT 11062021 se actualiza el stock, y el pallet, no importa si inserta o elimina en detalle
            clsLnStock.Actualizar_Pallet_No_Standar(pBeT_vw_stock_res.IdStock, pBeT_vw_stock_res.Pallet_No_Estandar, lConnection, lTransaction)

            'GT 14062021 Valida si existe un detalle, para aumentar/disminuir o eliminar las posiciones
            pBeStock_det = clsLnStock_det.GetSingle(pBeT_vw_stock_res.IdStock, lConnection, lTransaction)

            If pBeT_vw_stock_res.Pallet_No_Estandar Then

                If pBeStock_det IsNot Nothing Then
                    clsLnStock_det.Actualizar(vBeStock_det, lConnection, lTransaction)
                Else
                    clsLnStock_det.Insertar(vBeStock_det, lConnection, lTransaction)
                End If
            Else
                If pBeStock_det IsNot Nothing Then
                    clsLnStock_det.Eliminar(vBeStock_det, lConnection, lTransaction)
                End If
            End If

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw New Exception(ex.Message)
        Finally
            If Not lConnection Is Nothing AndAlso lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Sub

    Public Shared Function Get_Disponibilidad_Ubicacion_By_IdBodega_And_IdUbicacion(ByVal pIdBodega As Integer, ByVal pIdUbicacion As Integer) As Double

        Dim pBeT_vw_stock_res As New clsBeVW_stock_res

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Const sp As String = "SELECT * FROM vw_stock_res WHERE IdBodega = @IdBodega AND IdUbicacion = @IdUbicacion and es_rack = 1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim vCantidadTarima As Double = 0
            Dim vEspacioOcupado As Double = 0

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each lRow As DataRow In dt.Rows

                    pBeT_vw_stock_res = New clsBeVW_stock_res
                    Cargar(pBeT_vw_stock_res, lRow, lConnection, lTransaction)

                    If pBeT_vw_stock_res.IdPresentacion <> 0 Then

                        If pBeT_vw_stock_res.CajasPorCama <> 0 OrElse pBeT_vw_stock_res.CamasPorTarima <> 0 Then

                            vCantidadTarima = pBeT_vw_stock_res.CajasPorCama * pBeT_vw_stock_res.CamasPorTarima
                            '#EJC20220129: El resultado debería ser un valor entre 0 y 1, si excede de 1 quiere decir que
                            'esa posición tiene más producto de lo que la ubicación en teoría soporta...
                            vEspacioOcupado += pBeT_vw_stock_res.CantidadPresentacion / vCantidadTarima

                        End If

                    End If

                Next

            End If

            Return vEspacioOcupado

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Disponibilidad_Ubicacion_By_IdBodega_And_IdUbicacion(ByVal pIdBodega As Integer,
                                                                                    ByVal pIdUbicacion As Integer,
                                                                                    ByVal lConnection As SqlConnection,
                                                                                    ByVal lTransaction As SqlTransaction) As Double

        Dim pBeT_vw_stock_res As New clsBeVW_stock_res

        Try

            Const sp As String = "SELECT * FROM vw_stock_res 
                                  WHERE IdBodega = @IdBodega AND IdUbicacion = @IdUbicacion and es_rack = 1 "
            Dim cmd As New SqlCommand(sp, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)
            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)
            dad.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)

            Dim dt As New DataTable
            dad.Fill(dt)

            Dim vCantidadTarima As Double = 0
            Dim vEspacioOcupado As Double = 0

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each lRow As DataRow In dt.Rows

                    pBeT_vw_stock_res = New clsBeVW_stock_res

                    Cargar(pBeT_vw_stock_res, lRow, lConnection, lTransaction)

                    If pBeT_vw_stock_res.IdPresentacion <> 0 Then

                        If pBeT_vw_stock_res.CajasPorCama <> 0 OrElse pBeT_vw_stock_res.CamasPorTarima <> 0 Then

                            vCantidadTarima = pBeT_vw_stock_res.CajasPorCama * pBeT_vw_stock_res.CamasPorTarima
                            '#EJC20220129: El resultado debería ser un valor entre 0 y 1, si excede de 1 quiere decir que
                            'esa posición tiene más producto de lo que la ubicación en teoría soporta...
                            If (vCantidadTarima > 0) Then
                                vEspacioOcupado += pBeT_vw_stock_res.CantidadPresentacion / vCantidadTarima
                            Else
                                vEspacioOcupado = 0
                            End If

                        ElseIf pBeT_vw_stock_res.CantidadPresentacion > 0 Then '#CKFK20221128 Agregué esta condición para cuando no haya entarimado
                            vEspacioOcupado = 1
                        End If

                    ElseIf pBeT_vw_stock_res.CantidadUmBas > 0 Then '#CKFK20221128 Agregué esta condición para cuando la cantidad sea en UMBas
                        vEspacioOcupado = 1
                    End If

                Next

            End If

            Return vEspacioOcupado

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    '#CKKF20220627 Creé esta función para cargar la información que vamos a mostrar como stock reservado en la HH
    Public Shared Function Get_Stock_Res_By_Codigo_And_IdUbicacion(ByVal pIdUbicacion As Integer,
                                                                   ByVal pCodigo As String,
                                                                   ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim vSQL As String = "SELECT IdPedido,
	                                     IdPicking,
		                                 Codigo,
		                                 Nombre,
		                                 cantidad as Cant_UMBas,
		                                 unidadmedida as UMBas,
		                                 case when factor > 0 then
			                                 cantidad / factor
		                                 else
			                                 0           
		                                 end as  Cant_Presentacion,
		                                 presentacion as Presentacion,
		                                 NomEstado as EstadoProducto,
		                                 Lic_Plate as Licencia,
		                                 Lote,
		                                 fecha_vence as FechaVence,
		                                 Nombre_Completo as UbicacionActual,
		                                 Nivel,
		                                 IdProductoBodega,
		                                 IdStockRes, 
		                                 IdStock,	   
                                         factor as Factor, 
		                                 peso as Peso,
		                                 propietario as Propietario,
		                                 IdUbicacion,
		                                 IdUnidadMedida,
		                                 IdPresentacion, 
                                         Fecha_Pedido,
                                         Fecha_Preparacion
                                 From VW_Stock_Res_Pedido  
                                 WHERE IdBodega = @IdBodega AND 
                                       Indicador = 'PED' AND  
                                       Nivel >1 AND  
                                       estado = 'UNCOMMITED' "

            If pCodigo <> "" Then
                vSQL += " AND  Codigo = @Codigo "
            End If

            If pIdUbicacion <> 0 Then
                vSQL += "  AND  IdUbicacion = @IdUbicacion "
            End If

            vSQL += " ORDER BY fec_agr DESC"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            If pCodigo <> "" Then
                dad.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
            End If

            If pIdUbicacion <> 0 Then
                dad.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
            End If

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeStockVW_res As New clsBeVW_stock_res

            For Each dr As DataRow In dt.Rows

                vBeStockVW_res = New clsBeVW_stock_res
                Cargar_Res(vBeStockVW_res, dr, lConnection, lTransaction)
                lReturnList.Add(vBeStockVW_res)

            Next

            lTransaction.Commit()

            Get_Stock_Res_By_Codigo_And_IdUbicacion = lReturnList

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try


    End Function

    '#CKKF20220718 Creé esta función para cargar la información que vamos a mostrar como stock reservado en la HH, pero por IdStock,
    ' no por pedido
    Public Shared Function Get_Stock_Res_By_Codigo_And_IdUbicacion2(ByVal pIdUbicacion As Integer,
                                                                    ByVal pCodigo As String,
                                                                    ByVal pIdBodega As Integer,
                                                                    ByVal pReferencia As String) As List(Of clsBeVW_stock_res)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Dim lReturnList As New List(Of clsBeVW_stock_res)

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            '#AT20220810 Se agregó el top 50 este listado
            Dim vSQL As String = "SELECT TOP(50) 0 IdPedido,
	                                     0 IdPicking,
		                                 producto.codigo,
		                                 producto.Nombre,
		                                 stock.cantidad as Cant_UMBas,
		                                 unidad_medida.nombre as UMBas,
		                                 case when ISNULL(producto_presentacion.factor,0) > 0 then
			                                 cantidad / producto_presentacion.factor
		                                 else
			                                 0           
		                                 end as  Cant_Presentacion,
		                                 ISNULL(producto_presentacion.nombre,'') as Presentacion,
		                                 producto_estado.nombre as EstadoProducto,
		                                 Lic_Plate as Licencia,
		                                 stock.Lote,
		                                 stock.fecha_vence as FechaVence,
		                                 dbo.Nombre_Completo_Ubicacion(stock.IdUbicacion,stock.IdBodega) as UbicacionActual,
		                                 bodega_ubicacion.Nivel,
		                                 stock.IdProductoBodega,
		                                 0 AS IdStockRes, 
		                                 stock.IdStock,	   
                                         ISNULL(producto_presentacion.factor,0) as Factor, 
		                                 stock.peso as Peso,
		                                 propietarios.nombre_comercial as Propietario,
		                                 stock.IdUbicacion,
		                                 stock.IdUnidadMedida,
		                                 stock.IdPresentacion, 
                                         '1900-01-01T00:00:01' as Fecha_Pedido,
                                         '1900-01-01T00:00:01' as Fecha_Preparacion,
                                         stock.fec_agr,
                                         ISNULL(stock.IdProductoTallaColor, 0) IdProductoTallaColor,
                                         ISNULL(t.Codigo, '') AS Codigo_Talla,
                                         ISNULL(t.Nombre, '') AS Nombre_Talla,
                                         ISNULL(c.Codigo, '') AS Codigo_Color,
                                         ISNULL(c.Nombre, '') AS Nombre_Color
                                  FROM   stock INNER JOIN 
                                         producto_bodega ON stock.IdProductoBodega = producto_bodega.IdProductoBodega AND 
                                                            stock.IdBodega = producto_bodega.IdBodega INNER JOIN
                                         producto ON producto.IdProducto = producto_bodega.IdProducto INNER JOIN
	                                     propietario_bodega ON producto.IdPropietario = propietario_bodega.IdPropietario AND 
	                                                             stock.IdBodega = propietario_bodega.IdBodega INNER JOIN
                                         propietarios ON propietarios.IdPropietario = propietario_bodega.IdPropietario INNER JOIN
	                                     producto_estado ON propietarios.IdPropietario = producto_estado.IdPropietario AND
	                                                         stock.IdProductoEstado = producto_estado.IdEstado INNER JOIN
                                         bodega_ubicacion ON bodega_ubicacion.IdUbicacion = stock.IdUbicacion AND
	                                                         bodega_ubicacion.IdBodega = stock.IdBodega INNER JOIN
	                                     unidad_medida ON unidad_medida.IdPropietario = propietarios.IdPropietario AND
	                                                         unidad_medida.IdUnidadMedida = stock.IdUnidadMedida LEFT OUTER JOIN
	                                     producto_presentacion ON producto.IdProducto = producto_presentacion.IdProducto AND
	                                                                producto_presentacion.IdPresentacion = stock.IdPresentacion
                                         left join producto_talla_color ptc on ptc.IdProductoTallaColor = stock.IdProductoTallaColor
                                         left join talla t on t.IdTalla = ptc.IdTalla
                                         left join color c on c.IdColor = ptc.IdColor
                                  WHERE IdStock IN ( SELECT  IdStock
                                                    FROM    VW_Stock_Res_Pedido  
                                                    WHERE   IdBodega = @IdBodega AND 
                                                            Indicador = 'PED' AND  
                                                            ubicacion_picking = 0 AND  
                                                            estado = 'UNCOMMITED' "
            If pReferencia <> "" Then
                vSQL += " AND  (referencia = @Referencia OR CONVERT(nvarchar(50),IdPedido) = @Referencia
                                OR CONVERT(nvarchar(50),IdPicking) = @Referencia)) AND stock.IdBodega = @IdBodega "
            Else
                vSQL += ") AND stock.IdBodega = @IdBodega "
            End If

            If pCodigo <> "" Then
                vSQL += " AND  producto.Codigo = @Codigo "
            End If

            If pIdUbicacion <> 0 Then
                vSQL += "  AND  stock.IdUbicacion = @IdUbicacion "
            End If

            vSQL += " ORDER BY fec_agr DESC"

            Dim cmd As New SqlCommand(vSQL, lConnection, lTransaction) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            dad.SelectCommand.Parameters.AddWithValue("@IdBodega", pIdBodega)

            If pCodigo <> "" Then
                dad.SelectCommand.Parameters.AddWithValue("@Codigo", pCodigo)
            End If

            If pIdUbicacion <> 0 Then
                dad.SelectCommand.Parameters.AddWithValue("@IdUbicacion", pIdUbicacion)
            End If

            If pReferencia <> "" Then
                dad.SelectCommand.Parameters.AddWithValue("@Referencia", pReferencia)
            End If

            Dim dt As New DataTable

            dad.Fill(dt)

            Dim vBeStockVW_res As New clsBeVW_stock_res

            For Each dr As DataRow In dt.Rows

                vBeStockVW_res = New clsBeVW_stock_res
                Cargar_Res(vBeStockVW_res, dr, lConnection, lTransaction)
                lReturnList.Add(vBeStockVW_res)

            Next

            lTransaction.Commit()

            Get_Stock_Res_By_Codigo_And_IdUbicacion2 = lReturnList

        Catch ex1 As SqlException
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Throw ex1
        Catch ex As Exception
            If Not lTransaction Is Nothing Then lTransaction.Rollback()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try


    End Function

    Public Shared Function Get_Count_Stock_ML_Ubicacion_Sugerida(ByVal pIdProducto As String,
                                                                 ByVal IdBodega As Integer,
                                                                 ByVal IdTramo As Integer,
                                                                 ByVal Columna As Integer) As Integer

        Get_Count_Stock_ML_Ubicacion_Sugerida = Nothing

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim lTransaction As SqlTransaction = Nothing

        Try

            lConnection.Open() : lTransaction = lConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            Dim lReturnList As New List(Of clsBeVW_stock_res)

            Dim vSQL As String = "SELECT  count(idstock) as registros
                                  FROM  vw_stock_res
                                  WHERE IdProducto = @IdProducto "

            vSQL += " and IdBodega=@IdBodega AND IdTramo = @IdTramo And Ubicacion_Indice_X = @Columna"

            vSQL += " Order By Ubicacion_Nivel "

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction

                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", IdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@Columna", Columna)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_Count_Stock_ML_Ubicacion_Sugerida = IIf(IsDBNull(lDataTable.Rows(0).Item("registros")), 0, lDataTable.Rows(0).Item("registros"))
                End If

            End Using

            lTransaction.Commit()

        Catch ex As Exception
            If lTransaction IsNot Nothing Then lTransaction.Rollback()
            Throw ex
        Finally
            If lConnection.State = ConnectionState.Open Then lConnection.Close()
        End Try

    End Function

    Public Shared Function Get_Count_Stock_ML_Ubicacion_Sugerida(ByVal pIdProducto As String,
                                                                 ByVal IdBodega As Integer,
                                                                 ByVal IdTramo As Integer,
                                                                 ByVal Columna As Integer,
                                                                 ByVal lConnection As SqlConnection,
                                                                 ByVal lTransaction As SqlTransaction) As Integer

        Get_Count_Stock_ML_Ubicacion_Sugerida = Nothing

        Try

            Dim lReturnList As New List(Of clsBeVW_stock_res)

            Dim vSQL As String = "SELECT  count(idstock) as registros
                                  FROM  vw_stock_res
                                  WHERE IdProducto = @IdProducto 
                                  AND IdBodega=@IdBodega 
                                  AND IdTramo = @IdTramo 
                                  AND Ubicacion_Indice_X = @Columna"

            Using lDTA As New SqlDataAdapter(vSQL, lConnection)

                lDTA.SelectCommand.CommandType = CommandType.Text
                lDTA.SelectCommand.Transaction = lTransaction
                lDTA.SelectCommand.Parameters.AddWithValue("@IdProducto", pIdProducto)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdBodega", IdBodega)
                lDTA.SelectCommand.Parameters.AddWithValue("@IdTramo", IdTramo)
                lDTA.SelectCommand.Parameters.AddWithValue("@Columna", Columna)

                Dim lDataTable As New DataTable
                lDTA.Fill(lDataTable)

                If lDataTable IsNot Nothing AndAlso lDataTable.Rows.Count > 0 Then
                    Get_Count_Stock_ML_Ubicacion_Sugerida = IIf(IsDBNull(lDataTable.Rows(0).Item("registros")), 0, lDataTable.Rows(0).Item("registros"))
                End If

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
