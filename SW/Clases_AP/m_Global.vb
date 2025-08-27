Module m_Global

    Public IpWCFs As String = String.Empty

    Public BD As New BaseDatos

    Public pWCFProductoPresentacion As New WCFProductoPresentacion.ServiceProductoPresentacionClient
    Public pWCFOrdenCompra As New WCFOrdenCompra.ServiceOrdenCompraClient
    Public pWCFRecepcion As New WCFRecepcion.ServiceRecepcionClient
    Public pWCFRecepcionInfraccion As New WCFRecepcionInfraccion.ServiceRecepcionInfraccionClient
    Public pWCFPropietarioR As New WCFPropietarioReglaRecepcion.ServicePropietarioReglaRecepcionClient
    Public pWCFPropietarioD As New WCFPropietarioDestinatario.PropietarioDestinatarioClient


    Public Sub InicializarServiciosWCF()

        pWCFProductoPresentacion.Endpoint.Address = New ServiceModel.EndpointAddress(IpWCFs & "Producto_Presentacion/ServiceProductoPresentacion.svc")
        pWCFProductoPresentacion.Open()

        ' Orden Compra
        pWCFOrdenCompra.Endpoint.Address = New ServiceModel.EndpointAddress(IpWCFs & "Orden_Compra/ServiceOrdenCompra.svc")
        pWCFOrdenCompra.Open()

        ' Racepcion
        pWCFRecepcion.Endpoint.Address = New ServiceModel.EndpointAddress(IpWCFs & "Recepcion/ServiceRecepcion.svc")
        pWCFRecepcion.Open()

        ' Recepción Infracción
        pWCFRecepcionInfraccion.Endpoint.Address = New ServiceModel.EndpointAddress(IpWCFs & "Recepcion_Infraccion/ServiceRecepcionInfraccion.svc")
        pWCFRecepcionInfraccion.Open()

        ' Propietario Regla Recepción
        pWCFPropietarioR.Endpoint.Address = New ServiceModel.EndpointAddress(IpWCFs & "Propietario_Regla_Recepcion/ServicePropietarioReglaRecepcion.svc")
        pWCFPropietarioR.Open()

        ' Propietario Destinatario  
        pWCFPropietarioD.Endpoint.Address = New ServiceModel.EndpointAddress(IpWCFs & "Propietario_Destinatario/PropietarioDestinatario.svc")
        pWCFPropietarioD.Open()

    End Sub

End Module
