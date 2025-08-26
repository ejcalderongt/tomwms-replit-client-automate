Imports System.Reflection
Imports System.Net.Mail

Public Class MailService

    Private ListaDI As New List(Of WCFRecepcionInfraccion.clsBeTrans_re_det_infraccion)


    Private Sub Inicializar()

        Try
                
            If BD.Existe_Ini() Then

                If BD.Leer_Ini() Then

                    InicializarServiciosWCF()

                    ' Obtenemos todas las bodegas activas
                    Dim ListB As List(Of clsBeBodega_monitor_parametro) = clsLnBodega_monitor_parametro.GetAllBodegasActivas()

                    ' Recorremos las bodegas activas
                    For Each b As clsBeBodega_monitor_parametro In ListB

                        ' Obtenemos todas las Recepciones segun la Bodega
                        Dim ListaR As List(Of WCFRecepcion.clsBeTrans_re_enc) = pWCFRecepcion.GetAllByBodega(b.IdBodega).ToList

                        If ListaR IsNot Nothing Then

                            ' Encabezado de Recepcion
                            For Each re As WCFRecepcion.clsBeTrans_re_enc In ListaR

                                ' Obtenemos todas las reglas que pertencen al propietario de la Recepcion
                                Dim ListaRegla As List(Of WCFPropietarioReglaRecepcion.clsBePropietario_reglas_enc) = pWCFPropietarioR.GetAllByPropietario(re.Propietario.IdPropietario).ToList

                                ' Validamos si en la Recepcion correspondiente trae Orden de Compra
                                If re.ObjROC IsNot Nothing AndAlso re.ObjROC.IdOrdenCompraEnc > 0 AndAlso re.ObjROC.IsNew = False Then

                                    ' Si trae Orden de Compra entonces obtenemos los datos de la OC directamente
                                    Dim ObjOC As WCFOrdenCompra.clsBeTrans_oc_enc = pWCFOrdenCompra.GetSingle(re.ObjROC.IdOrdenCompraEnc)

                                    ' Validamos que traiga datos 
                                    If ObjOC IsNot Nothing AndAlso ObjOC.IdOrdenCompraEnc > 0 AndAlso ObjOC.IsNew = False Then

                                        ' Recorremos el detalle de la Recepcion
                                        For Each red As WCFRecepcion.clsBeTrans_re_det In re.listaObjD

                                            ' Recorremos el detalle de la Orden de Compra
                                            For Each ocd As WCFOrdenCompra.clsBeTrans_oc_det In ObjOC.listaD

                                                ' La Regla con Id = 3 pertenece a Cantidad Menor Ingresada

                                                Dim lExisteRegla3 As Integer = -1
                                                If ListaRegla IsNot Nothing Then
                                                    lExisteRegla3 = ListaRegla.FindIndex(Function(bo) bo.IdReglaRecepcion = 3)
                                                End If

                                                If lExisteRegla3 > -1 Then

                                                    If red.IdProductoBodega = ocd.IdProductoBodega AndAlso red.cantidad_recibida < ocd.Cantidad Then

                                                        ' si entra acá es porque infrigio la regla 3

                                                        Dim ObjDI3 As New WCFRecepcionInfraccion.clsBeTrans_re_det_infraccion
                                                        ObjDI3.IdReglaPropietarioEnc = ListaRegla(lExisteRegla3).IdReglaPropietarioEnc
                                                        ObjDI3.IdOrdenCompraEnc = ocd.IdOrdenCompraEnc
                                                        ObjDI3.IdRecepcionEnc = red.IdRecepcionEnc
                                                        ObjDI3.IdPresentacion = red.Presentacion.IdPresentacion
                                                        ObjDI3.IdProductoBodega = red.IdProductoBodega
                                                        ObjDI3.User_agr = re.User_agr
                                                        ObjDI3.Fec_agr = Now
                                                        ObjDI3.User_mod = re.User_mod
                                                        ObjDI3.Fec_mod = Now
                                                        ObjDI3.Activo = True

                                                        ObjDI3.IsNew = True

                                                        ObjDI3.Empresa = b.NombreEmpresa
                                                        ObjDI3.Bodega = re.Bodega
                                                        'ObjDI3.Propietario = ListaRegla(lExisteRegla3).Propietario
                                                        ObjDI3.IdPropietario = re.Propietario.IdPropietario
                                                        ObjDI3.Propietario = re.NombrePropietario
                                                        ObjDI3.FechaRecepcion = re.Fecha_recepcion

                                                        ObjDI3.IdProveedor = ObjOC.Proveedor.IdProveedor
                                                        ObjDI3.NombreProveedor = ObjOC.Proveedor.Nombre

                                                        If ObjOC IsNot Nothing AndAlso IsDate(ObjOC.Fecha_Creacion) Then
                                                            ObjDI3.FechaOrdenCompra = ObjOC.Fecha_Creacion
                                                        End If

                                                        ObjDI3.ReglaInfraccionada = ListaRegla(lExisteRegla3).Regla
                                                        ObjDI3.CodigoProductoInfraccionado = red.Producto.Codigo
                                                        ObjDI3.ProductoInfraccionado = red.Producto.Nombre

                                                        ObjDI3.CantidadRecibida = red.cantidad_recibida
                                                        ObjDI3.CantidadSolicitada = ocd.Cantidad

                                                        ObjDI3.CostoRecepcion = red.Costo
                                                        ObjDI3.CostoOrdenCompra = ocd.Costo

                                                        ObjDI3.TipoIngreso = ObjOC.TipoIngreso
                                                        ObjDI3.TipoTransaccion = re.Descripcion

                                                        ObjDI3.NombreUsuario = re.Usuario

                                                        ObjDI3.ListaFactura = re.ListaObjF.Cast(Of WCFRecepcion.clsBeTrans_re_fact)()
                                                        ' CopyListObject(re.ListaObjF, ObjDI3.ListaFactura)

                                                        If ObjDI3.IdReglaPropietarioEnc > 0 AndAlso ObjDI3.IdOrdenCompraEnc > 0 AndAlso ObjDI3.IdRecepcionEnc > 0 _
                                                            AndAlso ObjDI3.IdPresentacion > 0 AndAlso ObjDI3.IdProductoBodega > 0 Then
                                                            ListaDI.Add(ObjDI3)
                                                        End If

                                                    End If

                                                End If

                                                ' La Regla con Id = 4 pertenece a Cantidad Mayor Ingresada

                                                Dim lExisteRegla4 As Integer = -1
                                                If ListaRegla IsNot Nothing Then
                                                    lExisteRegla4 = ListaRegla.FindIndex(Function(bo) bo.IdReglaRecepcion = 4)
                                                End If

                                                If lExisteRegla4 > -1 Then

                                                    If red.IdProductoBodega = ocd.IdProductoBodega AndAlso red.cantidad_recibida > ocd.Cantidad Then

                                                        ' si entra acá es porque infrigio la regla 4

                                                        Dim ObjDI4 As New WCFRecepcionInfraccion.clsBeTrans_re_det_infraccion
                                                        ObjDI4.IdReglaPropietarioEnc = ListaRegla(lExisteRegla4).IdReglaPropietarioEnc
                                                        ObjDI4.IdOrdenCompraEnc = ocd.IdOrdenCompraEnc
                                                        ObjDI4.IdRecepcionEnc = red.IdRecepcionEnc
                                                        ObjDI4.IdPresentacion = red.Presentacion.IdPresentacion
                                                        ObjDI4.IdProductoBodega = red.IdProductoBodega
                                                        ObjDI4.User_agr = re.User_agr
                                                        ObjDI4.Fec_agr = Now
                                                        ObjDI4.User_mod = re.User_mod
                                                        ObjDI4.Fec_mod = Now
                                                        ObjDI4.Activo = True

                                                        ObjDI4.IsNew = True

                                                        ObjDI4.Empresa = b.NombreEmpresa
                                                        ObjDI4.Bodega = re.Bodega
                                                        'ObjDI4.Propietario = ListaRegla(lExisteRegla3).Propietario
                                                        ObjDI4.IdPropietario = re.Propietario.IdPropietario
                                                        ObjDI4.Propietario = re.NombrePropietario
                                                        ObjDI4.FechaRecepcion = re.Fecha_recepcion

                                                        ObjDI4.IdProveedor = ObjOC.Proveedor.IdProveedor
                                                        ObjDI4.NombreProveedor = ObjOC.Proveedor.Nombre

                                                        If ObjOC IsNot Nothing AndAlso IsDate(ObjOC.Fecha_Creacion) Then
                                                            ObjDI4.FechaOrdenCompra = ObjOC.Fecha_Creacion
                                                        End If

                                                        ObjDI4.ReglaInfraccionada = ListaRegla(lExisteRegla3).Regla
                                                        ObjDI4.CodigoProductoInfraccionado = red.Producto.Codigo
                                                        ObjDI4.ProductoInfraccionado = red.Producto.Nombre

                                                        ObjDI4.CantidadRecibida = red.cantidad_recibida
                                                        ObjDI4.CantidadSolicitada = ocd.Cantidad

                                                        ObjDI4.CostoRecepcion = red.Costo
                                                        ObjDI4.CostoOrdenCompra = ocd.Costo

                                                        ObjDI4.TipoIngreso = ObjOC.TipoIngreso
                                                        ObjDI4.TipoTransaccion = re.Descripcion

                                                        ObjDI4.NombreUsuario = re.Usuario

                                                        ObjDI4.ListaFactura = re.ListaObjF.Cast(Of WCFRecepcion.clsBeTrans_re_fact)()

                                                        If ObjDI4.IdReglaPropietarioEnc > 0 AndAlso ObjDI4.IdOrdenCompraEnc > 0 AndAlso ObjDI4.IdRecepcionEnc > 0 _
                                                           AndAlso ObjDI4.IdPresentacion > 0 AndAlso ObjDI4.IdProductoBodega > 0 Then
                                                            ListaDI.Add(ObjDI4)
                                                        End If

                                                    End If

                                                End If

                                                ' La Regla con Id = 5 pertenece a Costo no pactado

                                                Dim lExisteRegla5 As Integer = -1
                                                If ListaRegla IsNot Nothing Then
                                                    lExisteRegla5 = ListaRegla.FindIndex(Function(bo) bo.IdReglaRecepcion = 5)
                                                End If

                                                If lExisteRegla5 > -1 Then

                                                    If red.IdProductoBodega = ocd.IdProductoBodega AndAlso red.Costo <> ocd.Costo Then

                                                        ' si entra acá es porque infrigio la regla 5

                                                        Dim ObjDI5 As New WCFRecepcionInfraccion.clsBeTrans_re_det_infraccion
                                                        ObjDI5.IdReglaPropietarioEnc = ListaRegla(lExisteRegla5).IdReglaPropietarioEnc
                                                        ObjDI5.IdOrdenCompraEnc = ocd.IdOrdenCompraEnc
                                                        ObjDI5.IdRecepcionEnc = red.IdRecepcionEnc
                                                        ObjDI5.IdPresentacion = red.Presentacion.IdPresentacion
                                                        ObjDI5.IdProductoBodega = red.IdProductoBodega
                                                        ObjDI5.User_agr = re.User_agr
                                                        ObjDI5.Fec_agr = Now
                                                        ObjDI5.User_mod = re.User_mod
                                                        ObjDI5.Fec_mod = Now
                                                        ObjDI5.Activo = True

                                                        ObjDI5.IsNew = True

                                                        ObjDI5.Empresa = b.NombreEmpresa
                                                        ObjDI5.Bodega = re.Bodega
                                                        'ObjDI5.Propietario = ListaRegla(lExisteRegla3).Propietario
                                                        ObjDI5.IdPropietario = re.Propietario.IdPropietario
                                                        ObjDI5.Propietario = re.NombrePropietario
                                                        ObjDI5.FechaRecepcion = re.Fecha_recepcion

                                                        ObjDI5.IdProveedor = ObjOC.Proveedor.IdProveedor
                                                        ObjDI5.NombreProveedor = ObjOC.Proveedor.Nombre

                                                        If ObjOC IsNot Nothing AndAlso IsDate(ObjOC.Fecha_Creacion) Then
                                                            ObjDI5.FechaOrdenCompra = ObjOC.Fecha_Creacion
                                                        End If

                                                        ObjDI5.ReglaInfraccionada = ListaRegla(lExisteRegla3).Regla
                                                        ObjDI5.CodigoProductoInfraccionado = red.Producto.Codigo
                                                        ObjDI5.ProductoInfraccionado = red.Producto.Nombre

                                                        ObjDI5.CantidadRecibida = red.cantidad_recibida
                                                        ObjDI5.CantidadSolicitada = ocd.Cantidad

                                                        ObjDI5.CostoRecepcion = red.Costo
                                                        ObjDI5.CostoOrdenCompra = ocd.Costo

                                                        ObjDI5.TipoIngreso = ObjOC.TipoIngreso
                                                        ObjDI5.TipoTransaccion = re.Descripcion

                                                        ObjDI5.NombreUsuario = re.Usuario

                                                        ObjDI5.ListaFactura = re.ListaObjF.Cast(Of WCFRecepcion.clsBeTrans_re_fact)()

                                                        If ObjDI5.IdReglaPropietarioEnc > 0 AndAlso ObjDI5.IdOrdenCompraEnc > 0 AndAlso ObjDI5.IdRecepcionEnc > 0 _
                                                           AndAlso ObjDI5.IdPresentacion > 0 AndAlso ObjDI5.IdProductoBodega > 0 Then
                                                            ListaDI.Add(ObjDI5)
                                                        End If

                                                    End If

                                                End If

                                                Dim lPesoPresentacion As Double = pWCFProductoPresentacion.ExistePeso(red.Presentacion.IdPresentacion)

                                                If red.Control_Peso AndAlso lPesoPresentacion > 0 Then

                                                    ' La Regla con Id = 6 pertenece a Peso Menor Ingresado

                                                    Dim lExisteRegla6 As Integer = -1
                                                    If ListaRegla IsNot Nothing Then
                                                        lExisteRegla6 = ListaRegla.FindIndex(Function(bo) bo.IdReglaRecepcion = 6)
                                                    End If

                                                    If lExisteRegla6 > -1 Then

                                                        If red.IdProductoBodega = ocd.IdProductoBodega AndAlso red.Peso < lPesoPresentacion Then

                                                            Dim ObjDI6 As New WCFRecepcionInfraccion.clsBeTrans_re_det_infraccion
                                                            ObjDI6.IdReglaPropietarioEnc = ListaRegla(lExisteRegla6).IdReglaPropietarioEnc
                                                            ObjDI6.IdOrdenCompraEnc = ocd.IdOrdenCompraEnc
                                                            ObjDI6.IdRecepcionEnc = red.IdRecepcionEnc
                                                            ObjDI6.IdPresentacion = red.Presentacion.IdPresentacion
                                                            ObjDI6.IdProductoBodega = red.IdProductoBodega
                                                            ObjDI6.User_agr = re.User_agr
                                                            ObjDI6.Fec_agr = Now
                                                            ObjDI6.User_mod = re.User_mod
                                                            ObjDI6.Fec_mod = Now
                                                            ObjDI6.Activo = True

                                                            ObjDI6.IsNew = True

                                                            ObjDI6.Empresa = b.NombreEmpresa
                                                            ObjDI6.Bodega = re.Bodega
                                                            'ObjDI6.Propietario = ListaRegla(lExisteRegla3).Propietario
                                                            ObjDI6.IdPropietario = re.Propietario.IdPropietario
                                                            ObjDI6.Propietario = re.NombrePropietario
                                                            ObjDI6.FechaRecepcion = re.Fecha_recepcion

                                                            ObjDI6.IdProveedor = ObjOC.Proveedor.IdProveedor
                                                            ObjDI6.NombreProveedor = ObjOC.Proveedor.Nombre

                                                            If ObjOC IsNot Nothing AndAlso IsDate(ObjOC.Fecha_Creacion) Then
                                                                ObjDI6.FechaOrdenCompra = ObjOC.Fecha_Creacion
                                                            End If

                                                            ObjDI6.ReglaInfraccionada = ListaRegla(lExisteRegla3).Regla
                                                            ObjDI6.CodigoProductoInfraccionado = red.Producto.Codigo
                                                            ObjDI6.ProductoInfraccionado = red.Producto.Nombre

                                                            ObjDI6.CantidadRecibida = red.cantidad_recibida
                                                            ObjDI6.CantidadSolicitada = ocd.Cantidad

                                                            ObjDI6.CostoRecepcion = red.Costo
                                                            ObjDI6.CostoOrdenCompra = ocd.Costo

                                                            ObjDI6.TipoIngreso = ObjOC.TipoIngreso
                                                            ObjDI6.TipoTransaccion = re.Descripcion

                                                            ObjDI6.NombreUsuario = re.Usuario

                                                            ObjDI6.ListaFactura = re.ListaObjF.Cast(Of WCFRecepcion.clsBeTrans_re_fact)()

                                                            If ObjDI6.IdReglaPropietarioEnc > 0 AndAlso ObjDI6.IdOrdenCompraEnc > 0 AndAlso ObjDI6.IdRecepcionEnc > 0 _
                                                               AndAlso ObjDI6.IdPresentacion > 0 AndAlso ObjDI6.IdProductoBodega > 0 Then
                                                                ListaDI.Add(ObjDI6)
                                                            End If

                                                        End If

                                                    End If

                                                End If

                                                ' La Regla con Id = 7 pertenece a Peso Mayor Ingresado

                                                Dim lExisteRegla7 As Integer = -1
                                                If ListaRegla IsNot Nothing Then
                                                    lExisteRegla7 = ListaRegla.FindIndex(Function(bo) bo.IdReglaRecepcion = 7)
                                                End If

                                                If lExisteRegla7 > -1 Then

                                                    If red.Peso > lPesoPresentacion Then

                                                        Dim ObjDI7 As New WCFRecepcionInfraccion.clsBeTrans_re_det_infraccion
                                                        ObjDI7.IdReglaPropietarioEnc = ListaRegla(lExisteRegla7).IdReglaPropietarioEnc
                                                        ObjDI7.IdOrdenCompraEnc = ocd.IdOrdenCompraEnc
                                                        ObjDI7.IdRecepcionEnc = red.IdRecepcionEnc
                                                        ObjDI7.IdPresentacion = red.Presentacion.IdPresentacion
                                                        ObjDI7.IdProductoBodega = red.IdProductoBodega
                                                        ObjDI7.User_agr = re.User_agr
                                                        ObjDI7.Fec_agr = Now
                                                        ObjDI7.User_mod = re.User_mod
                                                        ObjDI7.Fec_mod = Now
                                                        ObjDI7.Activo = True

                                                        ObjDI7.IsNew = True

                                                        ObjDI7.Empresa = b.NombreEmpresa
                                                        ObjDI7.Bodega = re.Bodega
                                                        'ObjDI7.Propietario = ListaRegla(lExisteRegla3).Propietario
                                                        ObjDI7.IdPropietario = re.Propietario.IdPropietario
                                                        ObjDI7.Propietario = re.NombrePropietario
                                                        ObjDI7.FechaRecepcion = re.Fecha_recepcion

                                                        ObjDI7.IdProveedor = ObjOC.Proveedor.IdProveedor
                                                        ObjDI7.NombreProveedor = ObjOC.Proveedor.Nombre

                                                        If ObjOC IsNot Nothing AndAlso IsDate(ObjOC.Fecha_Creacion) Then
                                                            ObjDI7.FechaOrdenCompra = ObjOC.Fecha_Creacion
                                                        End If

                                                        ObjDI7.ReglaInfraccionada = ListaRegla(lExisteRegla3).Regla
                                                        ObjDI7.CodigoProductoInfraccionado = red.Producto.Codigo
                                                        ObjDI7.ProductoInfraccionado = red.Producto.Nombre

                                                        ObjDI7.CantidadRecibida = red.cantidad_recibida
                                                        ObjDI7.CantidadSolicitada = ocd.Cantidad

                                                        ObjDI7.CostoRecepcion = red.Costo
                                                        ObjDI7.CostoOrdenCompra = ocd.Costo

                                                        ObjDI7.TipoIngreso = ObjOC.TipoIngreso
                                                        ObjDI7.TipoTransaccion = re.Descripcion

                                                        ObjDI7.NombreUsuario = re.Usuario

                                                        ObjDI7.ListaFactura = re.ListaObjF.Cast(Of WCFRecepcion.clsBeTrans_re_fact)()

                                                        If ObjDI7.IdReglaPropietarioEnc > 0 AndAlso ObjDI7.IdOrdenCompraEnc > 0 AndAlso ObjDI7.IdRecepcionEnc > 0 _
                                                           AndAlso ObjDI7.IdPresentacion > 0 AndAlso ObjDI7.IdProductoBodega > 0 Then
                                                            ListaDI.Add(ObjDI7)
                                                        End If

                                                    End If

                                                End If

                                            Next

                                        Next

                                    End If

                                    ' ProductoFaltante(re.listaObjD, ObjOC.listaD, ListaRegla, re.IdRecepcionEnc)
                                    ' ProductoSobrante(re.listaObjD, ObjOC.listaD, ListaRegla, ObjOC.IdOrdenCompraEnc, re.IdRecepcionEnc)

                                    ProductoFaltante(re, ObjOC, ListaRegla, b.NombreEmpresa)
                                    ProductoSobrante(re, ObjOC, ListaRegla, b.NombreEmpresa)

                                    'pWCFRecepcionInfraccion.Guardar(ListaDI.ToArray)
                                    'ListaDI = New List(Of WCFRecepcionInfraccion.clsBeTrans_re_det_infraccion)

                                End If



                            Next

                        End If

                    Next

                    pWCFRecepcionInfraccion.Guardar(ListaDI.ToArray)
                    CrearPDFInfraccion()

                End If

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub


    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work

        Inicializar()

        'If clsLnBodega_monitor_parametro.ExisteParametroRevisionInconsistencia() = False Then Throw New Exception("El Parámetro de Revisión de Inconsistencias no se encuentra creado.")

    End Sub


    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
    End Sub


    Private Sub ProductoFaltante(ByVal pObjRecepcion As WCFRecepcion.clsBeTrans_re_enc, _
                                 ByVal pObjOrdenCompra As WCFOrdenCompra.clsBeTrans_oc_enc, _
                                 ByVal pListRegla As List(Of WCFPropietarioReglaRecepcion.clsBePropietario_reglas_enc), _
                                 ByVal pNombreEmpresa As String)

        Try

            For Each oc As WCFOrdenCompra.clsBeTrans_oc_det In pObjOrdenCompra.listaD

                Dim existe As Integer = pObjRecepcion.listaObjD.ToList.FindIndex(Function(b) b.Producto.IdProducto = oc.Producto.IdProducto)

                If existe = -1 Then

                    ' El Producto no se encuentra entre los productos ingresados en la recepcion
                    ' La Regla con Id = 1 pertenece a Ingreso con Productos Faltantes

                    Dim lIndexR1 As Integer = -1
                    If pListRegla IsNot Nothing Then
                        lIndexR1 = pListRegla.FindIndex(Function(b) b.IdReglaRecepcion = 1)
                    End If

                    Dim Obj As New WCFRecepcionInfraccion.clsBeTrans_re_det_infraccion

                    Obj.IdReglaPropietarioEnc = pListRegla(lIndexR1).IdReglaPropietarioEnc
                    Obj.IdOrdenCompraEnc = pObjOrdenCompra.IdOrdenCompraEnc
                    Obj.IdRecepcionEnc = pObjRecepcion.IdRecepcionEnc
                    Obj.IdPresentacion = oc.Presentacion.IdPresentacion
                    Obj.IdProductoBodega = oc.IdProductoBodega

                    Obj.User_agr = oc.User_agr
                    Obj.Fec_agr = Now
                    Obj.User_mod = oc.User_mod
                    Obj.Fec_mod = Now
                    Obj.Activo = True

                    Obj.IsNew = True

                    Obj.Empresa = pNombreEmpresa
                    Obj.Bodega = pObjRecepcion.Bodega
                    'Obj.Propietario = ListaRegla(lExisteRegla3).Propietario
                    Obj.IdPropietario = pObjRecepcion.Propietario.IdPropietario
                    Obj.Propietario = pObjRecepcion.NombrePropietario
                    Obj.FechaRecepcion = pObjRecepcion.Fecha_recepcion

                    Obj.IdProveedor = pObjOrdenCompra.Proveedor.IdProveedor
                    Obj.NombreProveedor = pObjOrdenCompra.Proveedor.Nombre

                    If pObjOrdenCompra IsNot Nothing AndAlso IsDate(pObjOrdenCompra.Fecha_Creacion) Then
                        Obj.FechaOrdenCompra = pObjOrdenCompra.Fecha_Creacion
                    End If

                    Obj.ReglaInfraccionada = pListRegla(lIndexR1).Regla
                    Obj.CodigoProductoInfraccionado = oc.Producto.Codigo
                    Obj.ProductoInfraccionado = oc.Producto.Nombre

                    Obj.CantidadRecibida = 0
                    Obj.CantidadSolicitada = oc.Cantidad

                    Obj.CostoRecepcion = 0
                    Obj.CostoOrdenCompra = oc.Costo

                    Obj.TipoIngreso = pObjOrdenCompra.TipoIngreso
                    Obj.TipoTransaccion = pObjRecepcion.Descripcion

                    Obj.NombreUsuario = pObjRecepcion.Usuario

                    Obj.ListaFactura = pObjRecepcion.ListaObjF.Cast(Of WCFRecepcion.clsBeTrans_re_fact)()

                    If Obj.IdReglaPropietarioEnc > 0 AndAlso Obj.IdOrdenCompraEnc > 0 AndAlso Obj.IdRecepcionEnc > 0 _
                       AndAlso Obj.IdPresentacion > 0 AndAlso Obj.IdProductoBodega > 0 Then
                        ListaDI.Add(Obj)
                    End If

                End If

            Next

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", Reflection.MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try





    End Sub

    ' ESTE METODO SE ESTABA USANDO YA QUE SE PENSO HACER LA VALIDACION DENTRO DEL FOR DE LA RECEPCION Y ORDEN DE COMPRA

    'Private Sub ProductoFaltante(ByVal pObjRedet As List(Of TOMIMSV4.WCFRecepcion.clsBeTrans_re_det), _
    '                             ByVal pObjOcDet As List(Of clsBeTrans_oc_det), _
    '                             ByVal pListRegla As List(Of TOMIMSV4.WCFPropietarioReglaRecepcion.clsBePropietario_reglas_enc))

    '    Try

    '        ' Dim existe As Integer = pListObjRec.FindIndex(Function(b) b.Producto.IdProducto = oc.Producto.IdProducto)

    '        If pObjRedet.Producto.IdProducto = pObjOcDet.Producto.IdProducto Then

    '            ' El Producto no se encuentra entre los productos ingresados en la recepcion
    '            ' La Regla con Id = 1 pertenece a Ingreso con Productos Faltantes

    '            Dim lIndexR1 As Integer = -1
    '            If pListRegla IsNot Nothing Then
    '                lIndexR1 = pListRegla.FindIndex(Function(b) b.IdReglaRecepcion = 1)
    '            End If

    '            Dim Obj As New clsBeTrans_re_det_infraccion

    '            Obj.IdReglaPropietarioEnc = pListRegla(lIndexR1).IdReglaPropietarioEnc
    '            Obj.IdOrdenCompraEnc = pObjOcDet.IdOrdenCompraEnc
    '            Obj.IdRecepcionEnc = pObjRedet.IdRecepcionEnc
    '            Obj.IdPresentacion = pObjOcDet.Presentacion.IdPresentacion
    '            Obj.IdProductoBodega = pObjOcDet.IdProductoBodega

    '            Obj.User_agr = pObjOcDet.User_agr
    '            Obj.Fec_agr = Now
    '            Obj.User_mod = pObjOcDet.User_mod
    '            Obj.Fec_mod = Now
    '            Obj.Activo = True

    '            Obj.IsNew = True
    '            ListaDI.Add(Obj)

    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub


    Private Sub ProductoSobrante(ByVal pObjRecepcion As WCFRecepcion.clsBeTrans_re_enc,
                                 ByVal pObjOrdenCompra As WCFOrdenCompra.clsBeTrans_oc_enc,
                                 ByVal pListRegla As List(Of WCFPropietarioReglaRecepcion.clsBePropietario_reglas_enc),
                                 ByVal pNombreEmpresa As String)

        Try

            For Each re As WCFRecepcion.clsBeTrans_re_det In pObjRecepcion.listaObjD

                Dim Existe As Integer = pObjOrdenCompra.listaD.ToList.FindIndex(Function(b) b.Producto.IdProducto = re.Producto.IdProducto)

                If Existe = -1 Then

                    ' El Producto ingresado no se encuentra dentro de la orden de compra
                    ' La Regla con Id = 2 pertenece a Ingreso con Productos Sobrantes/Excedentes

                    Dim lIndexR2 As Integer = -1
                    If pListRegla IsNot Nothing Then
                        lIndexR2 = pListRegla.FindIndex(Function(b) b.IdReglaRecepcion = 2)
                    End If

                    Dim Obj As New WCFRecepcionInfraccion.clsBeTrans_re_det_infraccion

                    Obj.IdReglaPropietarioEnc = pListRegla(lIndexR2).IdReglaPropietarioEnc
                    Obj.IdOrdenCompraEnc = pObjOrdenCompra.IdOrdenCompraEnc
                    Obj.IdRecepcionEnc = pObjRecepcion.IdRecepcionEnc
                    Obj.IdPresentacion = re.Presentacion.IdPresentacion
                    Obj.IdProductoBodega = re.IdProductoBodega

                    Obj.User_agr = re.User_agr
                    Obj.Fec_agr = Now
                    Obj.User_mod = re.User_agr
                    Obj.Fec_mod = Now
                    Obj.Activo = True

                    Obj.IsNew = True

                    Obj.Empresa = pNombreEmpresa
                    Obj.Bodega = pObjRecepcion.Bodega
                    'Obj.Propietario = ListaRegla(lExisteRegla3).Propietario
                    Obj.IdPropietario = pObjRecepcion.Propietario.IdPropietario
                    Obj.Propietario = pObjRecepcion.NombrePropietario
                    Obj.FechaRecepcion = pObjRecepcion.Fecha_recepcion

                    Obj.IdProveedor = pObjOrdenCompra.Proveedor.IdProveedor
                    Obj.NombreProveedor = pObjOrdenCompra.Proveedor.Nombre

                    If pObjOrdenCompra IsNot Nothing AndAlso IsDate(pObjOrdenCompra.Fecha_Creacion) Then
                        Obj.FechaOrdenCompra = pObjOrdenCompra.Fecha_Creacion
                    End If

                    Obj.ReglaInfraccionada = pListRegla(lIndexR2).Regla
                    Obj.CodigoProductoInfraccionado = re.Producto.Codigo
                    Obj.ProductoInfraccionado = re.Producto.Nombre

                    Obj.CantidadRecibida = re.cantidad_recibida
                    Obj.CantidadSolicitada = 0

                    Obj.CostoRecepcion = re.Costo
                    Obj.CostoOrdenCompra = 0

                    Obj.TipoIngreso = pObjOrdenCompra.TipoIngreso
                    Obj.TipoTransaccion = pObjRecepcion.Descripcion

                    Obj.NombreUsuario = pObjRecepcion.Usuario

                    Obj.ListaFactura = pObjRecepcion.ListaObjF.Cast(Of WCFRecepcion.clsBeTrans_re_fact)()

                    If Obj.IdReglaPropietarioEnc > 0 AndAlso Obj.IdOrdenCompraEnc > 0 AndAlso Obj.IdRecepcionEnc > 0 _
                      AndAlso Obj.IdPresentacion > 0 AndAlso Obj.IdProductoBodega > 0 Then
                        ListaDI.Add(Obj)
                    End If

                End If

            Next

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", Reflection.MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try





    End Sub


    ' ESTE METODO SE ESTABA USANDO YA QUE SE PENSO HACER LA VALIDACION DENTRO DEL FOR DE LA RECEPCION Y ORDEN DE COMPRA

    'Private Sub ProductoSobrante(ByVal pObjRedet As List(Of TOMIMSV4.WCFRecepcion.clsBeTrans_re_det), _
    '                             ByVal pObjOCDet As List(Of clsBeTrans_oc_det), _
    '                             ByVal pListRegla As List(Of TOMIMSV4.WCFPropietarioReglaRecepcion.clsBePropietario_reglas_enc))

    '    Try

    '        'Dim Existe As Integer = pLisstObjOC.FindIndex(Function(b) b.Producto.IdProducto = re.Producto.IdProducto)

    '        If pObjOCDet.Producto.IdProducto = pObjRedet.Producto.IdProducto Then

    '            ' El Producto ingresado no se encuentra dentro de la orden de compra
    '            ' La Regla con Id = 2 pertenece a Ingreso con Productos Sobrantes/Excedentes

    '            Dim lIndexR2 As Integer = -1
    '            If pListRegla IsNot Nothing Then
    '                lIndexR2 = pListRegla.FindIndex(Function(b) b.IdReglaRecepcion = 2)
    '            End If

    '            Dim Obj As New clsBeTrans_re_det_infraccion

    '            Obj.IdReglaPropietarioEnc = pListRegla(lIndexR2).IdReglaPropietarioEnc
    '            Obj.IdOrdenCompraEnc = pObjOCDet.IdOrdenCompraEnc
    '            Obj.IdRecepcionEnc = pObjRedet.IdRecepcionEnc
    '            Obj.IdPresentacion = pObjRedet.Presentacion.IdPresentacion
    '            Obj.IdProductoBodega = pObjRedet.IdProductoBodega

    '            Obj.User_agr = pObjRedet.User_agr
    '            Obj.Fec_agr = Now
    '            Obj.User_mod = pObjRedet.User_agr
    '            Obj.Fec_mod = Now
    '            Obj.Activo = True

    '            Obj.IsNew = True
    '            ListaDI.Add(Obj)

    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub


    Private Sub CrearPDFInfraccion()

        ' validar si la empresa va como parametro o como una propiedad dentro de la lista de infraccion

        Try

            ' Lista de Infracciones a Enviar
            For Each objI As WCFRecepcionInfraccion.clsBeTrans_re_det_infraccion In ListaDI

                ' Instanciamos el nuevo reporte - crystal
                Dim rpt As New rptRecepcionDetalleInfraccion
                ' Instanciamos el datasource
                Dim lDS As New dsRecepcionDetalleInfraccion

                Dim dt As New DataTable("Infraccion")
                dt.Columns.Add("Propietario", GetType(String))
                dt.Columns.Add("Bodega", GetType(String))
                dt.Columns.Add("ReglaInfraccionada", GetType(String))
                dt.Columns.Add("FechaOrdenCompra", GetType(DateTime))
                dt.Columns.Add("FechaRecepcion", GetType(DateTime))
                dt.Columns.Add("CodigoProducto", GetType(String))
                dt.Columns.Add("Producto", GetType(String))
                dt.Columns.Add("IdRecepcionEnc", GetType(Integer))
                dt.Columns.Add("IdOrdenCompra", GetType(Integer))
                dt.Columns.Add("CantidadSolicitada", GetType(Double))
                dt.Columns.Add("CantidadRecibida", GetType(Double))
                dt.Columns.Add("CostoOrdenCompra", GetType(Double))
                dt.Columns.Add("CostoRecepcion", GetType(Double))
                dt.Columns.Add("TipoIngreso", GetType(String))
                dt.Columns.Add("TipoTransaccion", GetType(String))

                Dim lRow As DataRow = dt.NewRow

                lRow("Propietario") = objI.Propietario
                lRow("Bodega") = objI.Bodega
                lRow("ReglaInfraccionada") = objI.ReglaInfraccionada
                lRow("FechaOrdenCompra") = objI.FechaOrdenCompra
                lRow("FechaRecepcion") = objI.FechaRecepcion
                lRow("CodigoProducto") = objI.CodigoProductoInfraccionado
                lRow("Producto") = objI.ProductoInfraccionado
                lRow("IdRecepcionEnc") = objI.IdRecepcionEnc

                lRow("IdOrdenCompra") = objI.IdOrdenCompraEnc

                lRow("CantidadSolicitada") = objI.CantidadSolicitada
                lRow("CantidadRecibida") = objI.CantidadRecibida
                lRow("CostoOrdenCompra") = objI.CostoOrdenCompra
                lRow("CostoRecepcion") = objI.CostoRecepcion

                lRow("TipoIngreso") = objI.TipoIngreso

                lRow("TipoTransaccion") = objI.TipoTransaccion

                dt.Rows.Add(lRow)

                Dim DTI As New DataTable("ImagenEmpresa")
                dt.Columns.Add("Imagen", GetType(Byte()))
                dt.Rows.Add(objI.ImagenEmpresa)

                lDS.Tables("Imagen").Merge(DTI)
                lDS.Tables("Data").Merge(dt)

                Dim lPropietario As String = String.Format("{0} - {1}", objI.IdPropietario, objI.Propietario)
                Dim lProveedor As String = String.Format("{0} - {1}", objI.IdProveedor, objI.NombreProveedor)

                Dim lFactura As String = String.Empty
                If objI.ListaFactura IsNot Nothing Then
                    For Each f As WCFRecepcionInfraccion.clsBeTrans_re_fact In objI.ListaFactura
                        lFactura += f.NoFactura & ","
                    Next
                    lFactura = lFactura.Remove(lFactura.Length - 1, 1)
                End If

                SetFormula(rpt, lFactura, objI.Empresa, objI.Bodega, lPropietario, objI.NombreUsuario, lProveedor, objI.EsDevolucion)

                Dim lSubject As String = String.Empty
                If objI.EsDevolucion Then
                    lSubject = "Infracción en Devolución"
                Else
                    lSubject = "Infracción en Ingreso"
                End If

                rpt.SetDataSource(lDS)
                Dim lPath As String = String.Format("{0}\{1}{2}{3}{4}{5}{6}Infraccion.pdf", CurDir(), Now.Year, Now.Day, Now.Hour, Now.Minute, Now.Second, Now.Millisecond)
                rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, String.Format("{0}", lPath))
                rpt.Close()
                rpt.Dispose()

                Dim lBody As String = "El sistema TOMIMS ha detectado una inconsistencia en el documento adjunto. " _
                                    & vbCrLf & "Este correo ha sido generado y enviado de forma automática, no responda este mensaje. " _
                                    & vbCrLf & "Saludos."

                Dim ListaD As List(Of WCFPropietarioDestinatario.clsBePropietario_destinatario) = pWCFPropietarioD.GetAllMail(ListaDI.Select(Function(b) b.IdReglaPropietarioEnc).Distinct.ToArray).ToList

                If ListaD IsNot Nothing Then
                    For Each ObjD As WCFPropietarioDestinatario.clsBePropietario_destinatario In ListaD
                        SendMail(ObjD.Correo_electronico, lSubject, lBody, lPath)
                    Next
                End If

            Next

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", Reflection.MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try





    End Sub


    Private Sub SetFormula(ByRef pReporte As rptRecepcionDetalleInfraccion, ByVal factura As String,
                           ByVal pNombreEmpresa As String, ByVal pNombreBodega As String, ByVal pPropietario As String,
                           ByVal pNombreUsuario As String,
                           ByVal pProveedor As String, ByVal pEsDevolucion As Boolean)

        Try

            For Each fm In pReporte.DataDefinition.FormulaFields
                Select Case fm.FormulaName
                    Case "{@NombreEmpresa}"
                        fm.Text = "'" & pNombreEmpresa & "'"
                    Case "{@Bodega}"
                        fm.Text = "'" & pNombreBodega & "'"
                        'fm.Text = "'" & cmbBodega.Text & "'"
                    Case "{@Propietario}"
                        fm.Text = "'" & pPropietario & "'"
                    Case "{@Proveedor}"
                        fm.Text = "'" & pProveedor & "'"
                    Case "{@Usuario}"
                        fm.Text = String.Format("'{0}'", pNombreUsuario)
                    Case "{@Infraccion}"
                        If pEsDevolucion Then
                            fm.Text = "'Reporte de Infracciones en Devolución'"
                        Else
                            fm.Text = "'Reporte de Infracciones en Ingreso'"
                        End If
                    Case "{@Factura}"
                        fm.Text = "'" & factura & "'"
                End Select
            Next

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", Reflection.MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try





    End Sub


    Friend Class ObjBase
    End Class


    Friend Class Obj
        Inherits ObjBase
    End Class


    Friend Class ObjManager

    End Class


    Private Sub SendMail(ByVal pDestinyMail As String, ByVal pSubject As String, ByVal pBody As String, Optional ByVal pFile As String = "")

        Dim Mail As New System.Net.Mail.MailMessage()
        'Rellenamos los parametros usuales para el envio de un email
        Mail.To.Add(String.Format("{0}", pDestinyMail))
        Mail.From = New MailAddress("tomimsv4@yahoo.com", "TOMIMS", System.Text.Encoding.UTF8)
        Mail.Subject = pSubject
        Mail.SubjectEncoding = System.Text.Encoding.UTF8
        Mail.Body = pBody
        Mail.BodyEncoding = System.Text.Encoding.UTF8
        Mail.IsBodyHtml = True 'Formato texto plano
        'Definimos nuestras credenciales para el unvio de emails a traves de Gmail

        If String.IsNullOrEmpty(pFile) = False Then
            Mail.Attachments.Add(New Attachment(pFile))
        End If

        Dim SClient As New SmtpClient("smtp.mail.yahoo.com")

        SClient.Credentials = New System.Net.NetworkCredential("tomimsv4@yahoo.com", "p4$w00Rd1234")
        SClient.Host = "smtp.mail.yahoo.com" 'Servidor SMTP de Gmail
        SClient.Port = 587 'Puerto del SMTP de Gmail
        SClient.EnableSsl = True 'Habilita el SSL, necesio en Gmail
        'Capturamos los errores en el envio
        Try
            SClient.Send(Mail)
        Catch ex As System.Net.Mail.SmtpException
            Throw ex
        End Try

    End Sub

End Class
