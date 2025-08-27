Imports System.ComponentModel
Imports System.Web.Services
Imports TESTWCFHH.ServiceGenClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class wsTest
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()>
    Public Function GetEmpresaByCodigoAndClave(ByVal Codigo As String, ByVal Clave As String) As ServiceGenClient.clsBeEmpresa

        GetEmpresaByCodigoAndClave = Nothing

        Try

            Dim Emp As New TOMHHWS
            Dim vEmp As New ServiceGenClient.clsBeEmpresa

            vEmp = Emp.GetEmpresaByCodigoAndClave(Codigo, Clave)

            Return vEmp

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function GetAllEmpresaForHH() As List(Of clsBeTareasIngresoHH)

        GetAllEmpresaForHH = Nothing

        Try

            Dim Emp As New TOMHHWS

            Return Emp.GetAllRecepcionesForHH().ToList()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function GetPropietariosByBodega(ByVal pIdBodega As Integer) As List(Of clsBePropietario_bodega)

        GetPropietariosByBodega = Nothing

        Try

            Dim Emp As New TOMHHWS
            Return Emp.GetPropietariosByBodegaForHH(pIdBodega).ToList()
            Emp = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function ListaEmpresas() As List(Of ServiceGenClient.clsBeEmpresa)

        ListaEmpresas = Nothing

        Try

            Dim Emp As New TOMHHWS

            Return Emp.GetEmpresaForHH(1).ToList()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function ListaBodegas(ByVal IdBodega As Integer) As List(Of ServiceGenClient.clsBeBodega)

        ListaBodegas = Nothing

        Try

            Dim Bod As New TOMHHWS

            Return Bod.GetBodegasByEmpresaForHH(IdBodega).ToList()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function GetAllParamsByProductoForHH(ByVal pIdProducto As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_parametros)

        GetAllParamsByProductoForHH = Nothing

        Try

            Dim Param As New TOMHHWS

            Return Param.GetAllParamsByProductoForHH(pIdProducto, pActivo).ToList

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function ListaClientes(ByVal IdBodega As Integer) As List(Of clsBeCliente_bodega)

        ListaClientes = Nothing

        Try

            Dim Srv As New TOMHHWS

            Return Srv.GetClientesByBodegaForHH(IdBodega).ToList()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function ListaProductos(ByVal IdBodega As Integer) As List(Of clsBeProducto_bodega)

        ListaProductos = Nothing

        Try

            Dim Srv As New TOMHHWS

            Return Srv.GetProductosByBodegaForHH(IdBodega).ToList()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function ListaProveedor(ByVal IdBodega As Integer) As List(Of clsBeProveedor_bodega)

        ListaProveedor = Nothing

        Try

            Dim Srv As New TOMHHWS

            Return Srv.GetProveedoresByBodegaForHH(IdBodega).ToList()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function MotivosAnulacionBodega(ByVal IdBodega As Integer) As List(Of clsBeMotivo_anulacion_bodega)

        MotivosAnulacionBodega = Nothing

        Try

            Dim Srv As New TOMHHWS

            Return Srv.GetMotivoAnulacionBodegaForHH(IdBodega).ToList()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function MotivosDevolucionBodega(ByVal IdBodega As Integer) As List(Of clsBeMotivo_devolucion_bodega)

        MotivosDevolucionBodega = Nothing

        Try

            Dim Srv As New TOMHHWS

            Return Srv.GetMotivoDevolucionBodegaByBodegaForHH(IdBodega).ToList()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function ListaOperadores(ByVal IdBodega As Integer) As List(Of clsBeOperador_bodega)

        ListaOperadores = Nothing

        Try

            Dim Srv As New TOMHHWS

            Return Srv.GetOperadoresByBodegaForHH(IdBodega).ToList()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function ListaImpresoras(ByVal IdEmpresa As Integer) As List(Of clsBeImpresora)

        ListaImpresoras = Nothing

        Try

            Dim Srv As New TOMHHWS

            Return Srv.GetImpresoras(IdEmpresa).ToList()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Function ListaRecepcioensByBodega(ByVal IdBodega As Integer) As List(Of clsBeTrans_re_enc)

        ListaRecepcioensByBodega = Nothing

        Try

            Dim Srv As New TOMHHWS

            Return Srv.GetRecepcionesByBodega(IdBodega).ToList()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    <WebMethod()>
    Public Sub GuardaRecepcionHH()

        Dim Srv As New TOMHHWS

        Try

            Dim gBeRecepcion As New clsBeTrans_re_enc
            Dim gBeOrdenCompra As New clsBeTrans_oc_enc
            Dim gBeReOC As New clsBeTrans_re_oc
            Dim gBeDetalleOC As New List(Of clsBeTrans_oc_det)

            gBeRecepcion = New clsBeTrans_re_enc
            gBeOrdenCompra = New clsBeTrans_oc_enc
            gBeReOC = New clsBeTrans_re_oc
            gBeDetalleOC = New List(Of clsBeTrans_oc_det)

            gBeRecepcion.IdRecepcionEnc = 2
            gBeOrdenCompra.IdOrdenCompraEnc = 2
            gBeReOC.IdRecepcionEnc = 2
            gBeReOC.IdOrdenCompraEnc = 2

            gBeRecepcion = Srv.GetSingleRec(gBeRecepcion.IdRecepcionEnc)
            gBeOrdenCompra = Srv.GetSingleOC(gBeOrdenCompra.IdOrdenCompraEnc)
            gBeReOC = Srv.GetSingleREOC(gBeReOC.IdRecepcionEnc)
            gBeDetalleOC = gBeOrdenCompra.listaD.ToList()

            gBeOrdenCompra.EstadoOC.IdEstadoOC = gBeOrdenCompra.IdEstadoOC

            Srv.GetSingleEstOC(gBeOrdenCompra.EstadoOC)

            Dim lBeTransRecDet As New List(Of clsBeTrans_re_det)

            Dim gBeProducto As New clsBeProducto

            gBeProducto = Srv.GetIdProductoByIdProductoBodega(2)
            gBeProducto.IdProductoBodega = 2

            Dim BeTransReDet As New clsBeTrans_re_det() With
            {.IdPropietarioBodega = 1,
                .Producto = New clsBeProducto()}

            BeTransReDet.Producto = gBeProducto

            BeTransReDet.Producto.IdProducto = gBeProducto.IdProducto
            BeTransReDet.Producto.Codigo = gBeProducto.Codigo
            BeTransReDet.IdProductoBodega = gBeProducto.IdProductoBodega
            BeTransReDet.Nombre_producto = gBeProducto.Nombre

            BeTransReDet.IdRecepcionEnc = gBeRecepcion.IdRecepcionEnc
            BeTransReDet.IdRecepcionDet = 1

            BeTransReDet.Presentacion = New clsBeProducto_presentacion

            BeTransReDet.Presentacion.IdPresentacion = 2
            BeTransReDet.IdPresentacion = 2

            BeTransReDet.No_Linea = 1

            BeTransReDet.UnidadMedida = gBeProducto.UnidadMedida
            BeTransReDet.IdUnidadMedida = gBeProducto.UnidadMedida.IdUnidadMedida

            BeTransReDet.ProductoEstado = New clsBeProducto_estado
            BeTransReDet.ProductoEstado.IdEstado = 1

            BeTransReDet.IdProductoEstado = 1

            BeTransReDet.IsNew = True

            BeTransReDet.User_agr = 1
            BeTransReDet.Fec_agr = Now

            BeTransReDet.MotivoDevolucion = New clsBeMotivo_devolucion

            BeTransReDet.cantidad_recibida = 180
            BeTransReDet.peso_unitario = 0

            BeTransReDet.Nombre_producto = gBeProducto.Nombre
            BeTransReDet.Nombre_presentacion = ""
            BeTransReDet.Nombre_unidad_medida = ""
            BeTransReDet.Nombre_producto_estado = ""
            BeTransReDet.Lote = "LOTE1"
            BeTransReDet.Fecha_vence = "01/01/2018"
            BeTransReDet.Fecha_ingreso = Now
            BeTransReDet.Peso = 100
            BeTransReDet.Peso_Estadistico = 0
            BeTransReDet.Peso_Minimo = 0
            BeTransReDet.Peso_Maximo = 0
            BeTransReDet.Observacion = "hOLA"
            BeTransReDet.Aniada = 1989

            BeTransReDet.Costo = 10
            BeTransReDet.Costo_Oc = 10
            BeTransReDet.Costo_Estadistico = 0

            lBeTransRecDet.Add(BeTransReDet)

            Dim vMensaje As String = ""

            vMensaje = Srv.GuardarRecepcion(gBeRecepcion, gBeReOC, lBeTransRecDet.ToArray)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    '<WebMethod()>
    'Public Sub Test_WCF_Pedido()

    '    Try

    '        Dim binding As New BasicHttpBinding(BasicHttpSecurityMode.None)
    '        binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None
    '        binding.MaxBufferSize = Integer.MaxValue
    '        binding.MaxReceivedMessageSize = Integer.MaxValue
    '        binding.MaxBufferPoolSize = Integer.MaxValue

    '        Dim m_proxy As New ServiceBodegaClient("http://localhost:42634/Bodega/ServiceBodega.svc")

    '        Return m_proxy.Get_All().ToList()

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub

End Class