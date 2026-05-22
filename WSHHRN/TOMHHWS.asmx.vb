Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Web.Script.Services
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports TOMWMS.wsBYBNavCUWMS
Imports TOMWMS.wsBYBNavMovProd
Imports TOMWMS.wsBYBNavUbicarAlmacen
Imports TOMWMS.wsBYBNavUInternas
Imports TOMWMS.WSDevolucionVentaNAV
Imports TOMWMS.WSLotePedidoCompra
Imports TOMWMS.WSPaginaLotes
Imports TOMWMS.WSPedidosCompraNAV
Imports TOMWMS.WSRecepAlm
Imports TOMWMS.wsTransferenciaIngresoNAV

Public Class clsArchHeader : Inherits SoapHeader

    Public Property Tipo As String = "WM"

    '#EJC20200427:   Constructor...
    Sub New()
        'Valor x defecto será WM
        Tipo = "WM"
    End Sub

    Public Sub ValidationSoapHeader(ByVal pArch As String)
        Tipo = pArch
    End Sub

End Class

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class TOMHHWS
    Inherits System.Web.Services.WebService

    '#EJCCKF20260519_Notificar_SAP_Hana_MAMAPA: Estados SAP HANA SL para el flujo operativo MAMAPA.
    ' 1=Nueva / disponible para reasignar picking; 2=Asignado al crear picking; 3=Pickeando al guardar el primer pickeo.
    ' 4=Pickeado al finalizar picking; 5=Verificando al guardar la primera verificación; 6=Verificado al finalizar verificación.
    ' 8=Cerrada/entregada; 11=Anulada; 12=Back order. Las llamadas se hacen después del commit WMS para no romper la operación local.
    Private Const TAG_NOTIFICAR_SAP_HANA_MAMPA As String = "#EJCCKF20260519_Notificar_SAP_Hana_MAMPA"

    Private Function Tiene_Avance_Picking(ByVal pIdPickingEnc As Integer) As Boolean

        Try
            Const vSQL As String = "SELECT COUNT(1) FROM trans_picking_ubic WHERE IdPickingEnc=@IdPickingEnc AND ISNULL(Cantidad_Recibida,0) > 0"

            Using lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lCommand As New SqlCommand(vSQL, lConnection)
                    lCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)
                    Return Convert.ToInt32(lCommand.ExecuteScalar()) > 0
                End Using
            End Using

        Catch ex As Exception
            clsLnLog_error_wms_pick.Agregar_Error(TAG_NOTIFICAR_SAP_HANA_MAMPA & ": No se pudo validar avance de picking. " & ex.Message,
                                                  pIdPickingEnc:=pIdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
            Return True
        End Try

    End Function

    Private Function Tiene_Avance_Verificacion(ByVal pIdPickingEnc As Integer) As Boolean

        Try
            Const vSQL As String = "SELECT COUNT(1) FROM trans_picking_ubic WHERE IdPickingEnc=@IdPickingEnc AND ISNULL(Cantidad_Verificada,0) > 0"

            Using lConnection As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lCommand As New SqlCommand(vSQL, lConnection)
                    lCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)
                    Return Convert.ToInt32(lCommand.ExecuteScalar()) > 0
                End Using
            End Using

        Catch ex As Exception
            clsLnLog_error_wms_pick.Agregar_Error(TAG_NOTIFICAR_SAP_HANA_MAMPA & ": No se pudo validar avance de verificación. " & ex.Message,
                                                  pIdPickingEnc:=pIdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
            Return True
        End Try

    End Function

    Private Function Get_Verificacion_Automatica(ByVal pIdPickingEnc As Integer) As Boolean

        Try
            Const vSQL As String = "SELECT ISNULL(verifica_auto,0) FROM trans_picking_enc WHERE IdPickingEnc=@IdPickingEnc"

            Using lConnection As New SqlConnection(ConfigurationManager.AppSettings("CST"))
                lConnection.Open()

                Using lCommand As New SqlCommand(vSQL, lConnection)
                    lCommand.Parameters.AddWithValue("@IdPickingEnc", pIdPickingEnc)
                    Return Convert.ToBoolean(lCommand.ExecuteScalar())
                End Using
            End Using

        Catch ex As Exception
            clsLnLog_error_wms_pick.Agregar_Error(TAG_NOTIFICAR_SAP_HANA_MAMPA & ": No se pudo validar verificación automática. " & ex.Message,
                                                  pIdPickingEnc:=pIdPickingEnc,
                                                  pStackTrace:=ex.StackTrace)
            Return False
        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Empresa_By_Codigo_And_Clave(ByVal Codigo As String, ByVal Clave As String) As clsBeEmpresa

        Get_Empresa_By_Codigo_And_Clave = Nothing

        Try

            Dim lEmpresas As New clsBeEmpresa
            lEmpresas = clsLnEmpresa.Get_Empresa_By_Codigo_And_Clave(Codigo, Clave)
            Return lEmpresas

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_List_Empresas_For_HH(ByVal IdEmpresa As Integer) As List(Of clsBeEmpresa)

        Get_List_Empresas_For_HH = Nothing

        Try

            Return clsLnEmpresa.Get_List_Empresas_For_HH(IdEmpresa)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Bodegas_By_IdEmpresa_For_HH(ByVal IdEmpresa As Integer) As List(Of clsBeBodega)

        Get_Bodegas_By_IdEmpresa_For_HH = Nothing

        Try

            Return clsLnBodega.Get_All_By_IdEmpresa(IdEmpresa)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Android_Get_Bodegas_By_IdEmpresa(ByVal IdEmpresa As Integer) As List(Of clsBeBodega)

        Android_Get_Bodegas_By_IdEmpresa = Nothing

        Try

            Return clsLnBodega.Android_Get_All_By_IdEmpresa(IdEmpresa)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Productos_By_IdBodega_For_HH(ByVal IdBodega As Integer) As List(Of clsBeProducto_bodega)

        Get_Productos_By_IdBodega_For_HH = Nothing

        Try

            Return clsLnProducto_bodega.Get_All_By_IdBodega_HH(IdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Propietarios_By_IdBodega_For_HH(ByVal IdBodega As Integer) As List(Of clsBePropietario_bodega)

        Get_Propietarios_By_IdBodega_For_HH = Nothing

        Try

            Return clsLnPropietario_bodega.Get_All_By_IdBodega_HH(IdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Reglas_Recepcion_For_HH() As List(Of clsBeReglas_recepcion)

        Get_All_Reglas_Recepcion_For_HH = Nothing

        Try

            Return clsLnReglas_recepcion.Get_Reglas_Recepcion()

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Clientes_By_IdBodega_For_HH(ByVal IdBodega As Integer) As List(Of clsBeCliente_bodega)

        Get_Clientes_By_IdBodega_For_HH = Nothing

        Try

            Return clsLnCliente_bodega.Get_All_By_IdBodega_HH(IdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Proveedores_By_Bodega_For_HH(ByVal IdBodega As Integer, ByVal IdProveedorBodega As Integer) As List(Of clsBeProveedor_bodega)

        Get_Proveedores_By_Bodega_For_HH = Nothing

        Try

            Return clsLnProveedor_bodega.Get_All_By_IdBodega_HH(IdBodega, IdProveedorBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_By_IdBodega_HH_Filtro(ByVal IdBodega As Integer, ByVal Filtro As String) As List(Of clsBeProveedor_bodega)

        Get_All_By_IdBodega_HH_Filtro = Nothing

        Try

            Return clsLnProveedor_bodega.Get_All_By_IdBodega_HH_Filtro(IdBodega, Filtro)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Motivo_Anulacion_By_IdBodega_For_HH(ByVal IdBodega As Integer) As List(Of clsBeMotivo_anulacion)

        Get_Motivo_Anulacion_By_IdBodega_For_HH = Nothing

        Try

            Return clsLnMotivo_anulacion.Get_All_By_IdBodega(IdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Motivos_Anulacion_By_IdBodega_For_HH(ByVal IdBodega As Integer) As List(Of clsBeMotivo_anulacion_bodega)

        Get_Motivos_Anulacion_By_IdBodega_For_HH = Nothing

        Try

            Return clsLnMotivo_anulacion_bodega.Get_All_By_Bodega(IdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Motivos_Devolucion_By_IdBodega_For_HH(ByVal IdBodega As Integer) As List(Of clsBeMotivo_devolucion)

        Get_Motivos_Devolucion_By_IdBodega_For_HH = Nothing

        Try

            Return clsLnMotivo_devolucion.Get_All_By_IdBodega(IdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Motivos_Devolucion_Bodega_By_IdBodega_For_HH(ByVal IdBodega As Integer) As List(Of clsBeMotivo_devolucion_bodega)

        Get_Motivos_Devolucion_Bodega_By_IdBodega_For_HH = Nothing

        Try

            Return clsLnMotivo_devolucion_bodega.Get_Motivos_Devolucion_Bodega_By_IdBodega(IdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Motivos_Ubicacion_For_HH() As List(Of clsBeMotivo_ubicacion)

        Get_Motivos_Ubicacion_For_HH = Nothing

        Try

            Return clsLnMotivo_ubicacion.Get_Motivos_Ubicacion()

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function ServiceIsAlive() As Boolean

        Return True

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function GetTimeStampServer() As Date

        Return Now

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Operadores_By_IdBodega_For_HH(ByVal IdBodega As Integer) As List(Of clsBeOperador_bodega)

        Get_Operadores_By_IdBodega_For_HH = Nothing

        Try

            Dim lReturnlIst As New List(Of clsBeOperador_bodega)
            lReturnlIst = clsLnOperador_bodega.Get_All_By_IdBodega_HH(IdBodega)

            If Not lReturnlIst Is Nothing Then

                Return lReturnlIst.OrderBy(Function(x) x.Operador.Nombres).ToList()

            End If

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Menu_Rol_Op_For_HH() As List(Of clsBeMenu_rol_op)

        Get_Menu_Rol_Op_For_HH = Nothing

        Try

            Return clsLnMenu_rol_op.GetAll()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Operador_Valido_HH(ByRef oBeOperador As clsBeOperador_bodega, ByRef pCodigoBodega As String, ByRef pIdProductoEstadoNE As Integer) As Boolean

        Operador_Valido_HH = False

        Try

            Return clsLnOperador_bodega.Operador_Valido(oBeOperador, pCodigoBodega, pIdProductoEstadoNE)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function GetSis_estado_tarea_hh() As List(Of clsBeSis_estado_tarea_hh)

        GetSis_estado_tarea_hh = Nothing

        Try

            Dim lSis_estado_tarea_hh As New List(Of clsBeSis_estado_tarea_hh)
            lSis_estado_tarea_hh = clsLnSis_estado_tarea_hh.GetAll()
            Return lSis_estado_tarea_hh

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function GetTarea_HH(ByVal IdBodega As Integer) As List(Of clsBeTarea_hh)

        GetTarea_HH = Nothing

        Try

            Dim lTarea_HH As New List(Of clsBeTarea_hh)
            lTarea_HH = clsLnTarea_hh.Get_All_By_IdBodega(IdBodega)
            Return lTarea_HH

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Tipo_Recepcion() As List(Of clsBeTrans_re_tr)

        Get_Tipo_Recepcion = Nothing

        Try

            Dim lTipoR As New List(Of clsBeTrans_re_tr)
            lTipoR = clsLnTrans_re_tr_Partial.GetAll()
            Return lTipoR

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Recepciones_By_IdBodega(ByVal IdBodega As Integer) As List(Of clsBeTrans_re_enc)

        Get_Recepciones_By_IdBodega = Nothing

        Try

            Return clsLnTarea_hh.Get_Recepciones_By_IdBodega(IdBodega)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, IdBodega, 0, ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Nombre_Rol_Operador_For_HH_By_IdRolOperador(ByVal IdRolOperador As Integer) As String

        Get_Nombre_Rol_Operador_For_HH_By_IdRolOperador = ""

        Try

            Dim RolOpe As New clsBeRol_operador() With {.IdRolOperador = IdRolOperador}

            If clsLnRol_operador.Obtener(RolOpe) Then
                Get_Nombre_Rol_Operador_For_HH_By_IdRolOperador = RolOpe.Nombre
            End If

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Menu_By_IdRolOperador_For_HH(ByVal IdRolOperador As Integer) As List(Of clsBeMenu_rol_op)

        Get_Menu_By_IdRolOperador_For_HH = Nothing

        Try

            Return clsLnMenu_rol_op.Get_List_Menu_By_IdRolOperador(IdRolOperador)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Recepciones_For_HH_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeTareasIngresoHH)

        Get_All_Recepciones_For_HH_By_IdBodega = Nothing

        Try

            Return clsLnTarea_hh.Get_All_Recepciones_For_HH_By_IdBodega(pIdBodega)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, pIdBodega, 0, ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Recepciones_For_HH_By_IdBodega_By_Operador(ByVal pIdBodega As Integer, ByVal pIdOperadorBodega As Integer) As List(Of clsBeTareasIngresoHH)

        Get_All_Recepciones_For_HH_By_IdBodega_By_Operador = Nothing

        Try

            Return clsLnTarea_hh.Get_All_Recepciones_For_HH_By_IdBodega_By_Operador(pIdBodega, pIdOperadorBodega)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, pIdBodega, 0, ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Rec_Ciegas_For_HH_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeTareasIngresoHH)

        Get_All_Rec_Ciegas_For_HH_By_IdBodega = Nothing

        Try

            Return clsLnTarea_hh.Get_All_Rec_Ciegas_For_HH_By_IdBodega(pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Trans_Re_Tr_For_HH() As List(Of clsBeTrans_re_tr)

        Get_All_Trans_Re_Tr_For_HH = Nothing

        Try

            Dim LnTrPartial As New clsLnTrans_re_tr_Partial

            Return LnTrPartial.Get_All_For_HH()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Empresas_For_HH() As List(Of clsBeEmpresa)

        Get_All_Empresas_For_HH = Nothing

        Try

            Dim lEmpresas As New List(Of clsBeEmpresa)
            lEmpresas = clsLnEmpresa.GetAll(True)
            Return lEmpresas

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Filtro_TransOCTipo_For_HH(ByVal pActivo As Boolean) As List(Of clsBeTrans_oc_ti)

        Get_All_Filtro_TransOCTipo_For_HH = Nothing

        Try

            Return clsLnTrans_oc_ti.Get_All_Filtro(pActivo)

        Catch ex As Exception

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function GetSingleRec(ByVal pIdRecepcionEnc As Integer) As clsBeTrans_re_enc

        GetSingleRec = Nothing

        Try

            Return clsLnTrans_re_enc.GetSingleHH(pIdRecepcionEnc)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_BeTransOcEnc_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer) As clsBeTrans_oc_enc

        Get_BeTransOcEnc_By_IdOrdenCompraEnc = Nothing

        Try
            Return clsLnTrans_oc_enc.Get_BeTransOcEnc_By_IdOrdenCompraEnc(pIdOrdenCompraEnc)
        Catch ex As Exception

            '#MECR03102025: Se agrego nueva bitacora de logs para OC
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Single_BeTrans_OC_Estado(ByRef pBeTrans_oc_estado As clsBeTrans_oc_estado) As Boolean

        Get_Single_BeTrans_OC_Estado = False

        Try

            Return clsLnTrans_oc_estado.GetSingle(pBeTrans_oc_estado)

        Catch ex As Exception

            '#MECR07102025: Se agrego nueva bitacora de logs para OC
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_BeRolOPerador_By_IdRolOperador(ByVal IdRolOperador As Integer) As clsBeRol_operador

        Get_BeRolOPerador_By_IdRolOperador = Nothing

        Try

            Return clsLnRol_operador.Obtener(IdRolOperador)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Impresora_By_IdEmpresa_And_IdBodega(ByVal IdEmpresa As Integer, ByVal IdBodega As Integer) As List(Of clsBeImpresora)

        Get_All_Impresora_By_IdEmpresa_And_IdBodega = Nothing

        Try

            Return clsLnImpresora.Get_All_Impresora_By_IdEmpresa_And_IdBodega(IdEmpresa, IdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Impresora_By_IdEmpresa_And_IdBodega_Dt(ByVal IdEmpresa As Integer, ByVal IdBodega As Integer) As DataTable

        Get_All_Impresora_By_IdEmpresa_And_IdBodega_Dt = Nothing

        Try

            Return clsLnImpresora.Get_All_Impresora_By_IdEmpresa_And_IdBodega_DT(IdEmpresa, IdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_BeTransReEnc_By_IdREcepcionEnc_For_HH(ByVal pIdRecepcionEnc As Integer) As clsBeTrans_re_enc

        Get_BeTransReEnc_By_IdREcepcionEnc_For_HH = Nothing

        Try

            Return clsLnTrans_re_enc.Get_Single_By_IdREcepcionEnc(pIdRecepcionEnc)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Producto_By_IdProductoBodega(ByVal IdProductoBodega As Integer) As clsBeProducto

        Get_Producto_By_IdProductoBodega = Nothing

        Try

            Return clsLnProducto_bodega.Get_Producto_By_IdProductoBodega_For_HH(IdProductoBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Sub Get_Control_Lote_And_Vencimiento_By_IdProductoBodega(ByVal IdProductoBodega As Integer,
                                                                        ByRef IdProducto As Integer,
                                                                        ByRef Control_Lote As Boolean,
                                                                        ByRef Control_Vencimiento As Boolean)

        Try

            clsLnProducto.Tiene_Control_Por_Lote_Y_Vencimiento(IdProductoBodega,
                                                                   IdProducto,
                                                                   Control_Lote,
                                                                   Control_Vencimiento)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Estados_By_IdPropietario_And_IdBodegaHH(ByVal pIdPropietario As Integer,
                                                                ByVal pIdBodega As Integer) As List(Of clsBeProducto_estado)

        Get_Estados_By_IdPropietario_And_IdBodegaHH = Nothing

        Try

            Return clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodegaHH(pIdPropietario, pIdBodega)

        Catch ex As Exception

            '#MECR19112025: Se agrego bitacora de logs para implosion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pEsImplosion:=True, pIdBodega:=pIdBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Estados_By_IdPropietario_And_IdBodega(ByVal pIdPropietario As Integer, ByVal pIdBodega As Integer) As List(Of clsBeProducto_estado)

        Get_Estados_By_IdPropietario_And_IdBodega = Nothing

        Try

            Return clsLnProducto_estado.Get_Estados_By_IdPropietario_And_IdBodega(pIdPropietario, pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Estados_By_IdPropietario_JSON(ByVal pIdPropietario As Integer)

        Dim responseObj As New Dictionary(Of String, Object)
        Dim curContext As HttpContext = HttpContext.Current

        Try
            Dim resultado As List(Of clsBeProducto_estado) = clsLnProducto_estado.Get_Estados_By_IdPropietario_For_HH(pIdPropietario)

            ConvertirListasVaciasANothing(resultado)

            responseObj("data") = resultado

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonResult As String = serializer.Serialize(responseObj)

            With curContext.Response
                .Clear()
                .StatusCode = 200
                .ContentType = "application/json"
                .Write(jsonResult)
            End With

            curContext.ApplicationInstance.CompleteRequest()

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing AndAlso mArch.Tipo = "WM" Then
                Throw New Exception(Mensaje)
            End If

            Dim errorObj As New Dictionary(Of String, Object)
            errorObj("error") = True
            errorObj("mensaje") = ex.Message

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim errorJson As String = serializer.Serialize(errorObj)

            With curContext.Response
                .Clear()
                .StatusCode = 500
                .ContentType = "application/json"
                .Write(errorJson)
            End With

            curContext.ApplicationInstance.CompleteRequest()

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Estados_By_IdPropietario(ByVal pIdPropietario As Integer) As List(Of clsBeProducto_estado)

        Get_Estados_By_IdPropietario = Nothing

        Try

            Return clsLnProducto_estado.Get_Estados_By_IdPropietario_For_HH(pIdPropietario)

        Catch ex As Exception

            '#MECR18112025: Se agrego bitacora de logs para implosion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pEsImplosion:=True)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function


    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Estados_Producto_For_HH() As List(Of clsBeProducto_estado)

        Get_Estados_Producto_For_HH = New List(Of clsBeProducto_estado)

        Try

            Return clsLnProducto_estado.Get_Estados_Producto()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Single_By_IdEstado(ByVal pIdEstado As Integer) As clsBeProducto_estado

        Get_Single_By_IdEstado = Nothing

        Try

            Return clsLnProducto_estado.Get_Single_By_IdEstado(pIdEstado)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Presentaciones_By_IdProducto(ByVal pIdProducto As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_Presentacion)

        Get_All_Presentaciones_By_IdProducto = New List(Of clsBeProducto_Presentacion)

        Try
            Return clsLnProducto_presentacion.Get_All_Presentaciones_By_IdProducto(pIdProducto, pActivo)

        Catch ex As Exception


            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Presentaciones_By_IdProductoBodega(ByVal pIdProductoBodega As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_Presentacion)

        Get_All_Presentaciones_By_IdProductoBodega = New List(Of clsBeProducto_Presentacion)

        Try

            Return clsLnProducto_presentacion.Get_All_Presentaciones_By_IdProductoBodega(pIdProductoBodega, pActivo)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_BeTrasReDet_By_IdOrdenCompraEnc(ByVal pIdOrdenCompraEnc As Integer) As List(Of clsBeTrans_re_det)

        Get_All_BeTrasReDet_By_IdOrdenCompraEnc = New List(Of clsBeTrans_re_det)

        Try

            Return clsLnTrans_re_det.Get_All_By_IdOrdenCompraEnc(pIdOrdenCompraEnc)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Detalle_Rec_By_IdRecepcionEnc(ByVal pIdRecEnc As Integer) As List(Of clsBeTrans_re_det)

        Get_All_Detalle_Rec_By_IdRecepcionEnc = New List(Of clsBeTrans_re_det)

        Try

            Return clsLnTrans_re_det.Get_All_By_IdRecepcionEnc_Sin_OC(pIdRecEnc)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function MaxIDStockRec() As Integer

        MaxIDStockRec = 0

        Try

            Return clsLnStock_rec.MaxID()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try
    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function MaxIDStockSeRec() As Integer

        MaxIDStockSeRec = 0

        Try

            Return clsLnStock_se_rec.MaxID()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try
    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function MaxIDInventarioCiclico() As Integer

        MaxIDInventarioCiclico = 0

        Try

            Return clsLnTrans_inv_ciclico.MaxID()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_ProductoParametros_By_IdProducto_HH(ByVal pIdProducto As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_parametros)

        Get_All_ProductoParametros_By_IdProducto_HH = New List(Of clsBeProducto_parametros)


        Try

            Return clsLnProducto_parametros.Get_All_ProductoParametros_By_IdProducto_HH(pIdProducto, pActivo)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Params_By_IdRecepcionEnc_And_IdRecepcion_Det_For_HH(ByVal pIdRecepcionEnc As Integer, ByVal pIdRecepcionDet As Integer) As List(Of clsBeTrans_re_det_parametros) 'Implements IServiceGen.GetAllParamsByRecepcionDetalleForHH

        Get_All_Params_By_IdRecepcionEnc_And_IdRecepcion_Det_For_HH = New List(Of clsBeTrans_re_det_parametros)

        Try

            Return clsLnTrans_re_det_parametros.Get_All_Params_By_IdRecepcionEnc_And_IdRecepcion_Det_For_HH(pIdRecepcionEnc, pIdRecepcionDet)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#MA20250210 migracion de xml a Json
    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_BeProducto_By_Codigo_For_HH_JSON(ByVal pCodigo As String, ByVal IdBodega As Integer)

        ' Get_BeProducto_By_Codigo_For_HH = Nothing

        Try
            Dim curContext As HttpContext = HttpContext.Current

            Dim producto As clsBeProducto = clsLnProducto.Get_BeProducto_By_Codigo(pCodigo, IdBodega)

            ConvertirListasVaciasANothing(producto)

            ' Serializamos el producto a JSON incluyendo nulls
            Dim json As String = JsonConvert.SerializeObject(producto, New JsonSerializerSettings With {
             .NullValueHandling = NullValueHandling.Include
            })

            curContext.Response.Clear()
            curContext.Response.ContentType = "application/json"
            curContext.Response.StatusCode = 200
            curContext.Response.Write(json)
            curContext.Response.End()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            '#MA20260505 Manejo de error
            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try

    End Sub


    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_List_Product_By_CodigoBarra_By_Picking(ByVal pCodigo As String,
                                                                      ByVal IdBodega As Integer,
                                                                      ByVal IdPickingEnc As Integer) As List(Of clsBeProducto)


        Get_List_Product_By_CodigoBarra_By_Picking = Nothing

        Try

            Return clsLnProducto.Get_List_Product_By_CodigoBarra_By_Picking(pCodigo, IdBodega, IdPickingEnc)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_BeProducto_By_Codigo_Or_Licencia(ByVal pCodigo As String, ByVal IdBodega As Integer) As clsBeProducto


        Get_BeProducto_By_Codigo_Or_Licencia = Nothing

        Try

            Return clsLnProducto.Get_BeProducto_By_Codigo_Or_Licencia(pCodigo, IdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_List_Product_By_CodigoBarra_By_OrdenCompraEnc(ByVal pCodigo As String,
                                                                      ByVal IdBodega As Integer,
                                                                      ByVal IdOrdenCompraEnc As Integer) As List(Of clsBeProducto)


        Get_List_Product_By_CodigoBarra_By_OrdenCompraEnc = Nothing

        Try

            Return clsLnProducto.Get_List_Product_By_CodigoBarra_By_OrdenCompraEnc(pCodigo, IdBodega, IdOrdenCompraEnc)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function


    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_BeProducto_By_LP_For_HH(ByVal pLic_Plate As String,
                                                ByVal pIdBodega As Integer,
                                                ByRef pBeStockRec As clsBeStock_rec) As clsBeProducto

        Get_BeProducto_By_LP_For_HH = Nothing

        '#EJC20180321: Usada en la HH para validar LP

        Try

            Dim BeProducto As New clsBeProducto

            pBeStockRec = clsLnStock_rec.GetSingleLP(pLic_Plate,
                                                     pIdBodega,
                                                     BeProducto)

            '#EJC20220830: Mejorado/optmizado, se busca el objeto beproducto dentro para aprovechar una sola transacción.
            'Si no se encontró el LP no hace sentido devolver el objeto de producto
            If Not pBeStockRec Is Nothing OrElse Not BeProducto Is Nothing Then
                Get_BeProducto_By_LP_For_HH = BeProducto
            End If

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Iniciar_Recepcion_OC(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdRecepcionEnc As Integer) As Integer

        Iniciar_Recepcion_OC = 0

        Try

            Return clsLnTrans_oc_enc.Iniciar_Recepcion_OC(pIdOrdenCompraEnc, pIdRecepcionEnc)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Guardar_Recepcion(ByVal pRecEnc As clsBeTrans_re_enc,
                                      ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                      ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                      ByVal pListStockRec As List(Of clsBeStock_rec),
                                      ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                      ByVal pLotesRec As clsBeTrans_oc_det_lote,
                                      ByVal pIdEmpresa As Integer,
                                      ByVal pIdBodega As Integer,
                                      ByVal pIdUsuario As Integer,
                                      ByVal pIdResolucionLp As Integer,
                                      ByVal pIdOperadorBodega As Integer,
                                      ByVal pRecepcionCajaMaster As Boolean) As String

        Guardar_Recepcion = ""

        Try

            '#GT05102022_1600: deje el Operador bodega como opcional, porque se instancia GuardarHH en varios lados,
            'no dimensiono si siempre sera necesario enviarlo o no.

            Dim vResult As String = ""
            vResult = clsLnTrans_re_enc.GuardarHH(pRecEnc,
                                                  pRecOrdenCompra,
                                                  pRecEnc.Detalle,
                                                  pRecEnc.DetalleParametros,
                                                  pListStockRecSer,
                                                  pListStockRec,
                                                  pListProductoPallet,
                                                  pLotesRec,
                                                  pIdEmpresa,
                                                  pIdBodega,
                                                  pIdUsuario,
                                                  pIdResolucionLp,
                                                  pIdOperadorBodega,
                                                  pRecepcionCajaMaster)


            Return String.Format("Res:{0}", vResult)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdEmpresa, pIdBodega, pIdUsuario, ex.StackTrace, pRecEnc.IdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function GuardarRecepcionModif(ByVal pRecEnc As clsBeTrans_re_enc,
                                               ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                               ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                               ByVal pListStockRec As List(Of clsBeStock_rec),
                                               ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                               ByVal pbeStockAnt As clsBeStock,
                                               ByVal pIdEmpresa As Integer,
                                               ByVal pIdBodega As Integer,
                                               ByVal pIdUsuario As Integer) As String

        GuardarRecepcionModif = ""

        Try

            Dim vResult As String = ""

            vResult = clsLnTrans_re_enc.GuardarHH(pRecEnc,
                                                      pRecOrdenCompra,
                                                      pRecEnc.Detalle,
                                                      pRecEnc.DetalleParametros,
                                                      pListStockRecSer,
                                                      pListStockRec,
                                                      pListProductoPallet,
                                                      pbeStockAnt,
                                                      pIdEmpresa,
                                                      pIdBodega,
                                                      pIdUsuario)

            Return String.Format("Res:{0}", vResult)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdEmpresa, pIdBodega, pIdUsuario, ex.StackTrace, pRecEnc.IdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKFK20220524 Función para guardar la recepción cuando se recibe sin presentacion
    <WebMethod(), SoapHeader("mArch")>
    Public Function Guardar_Recepcion_Sin_Presentacion(ByVal pRecEnc As clsBeTrans_re_enc,
                                                       ByVal pRecOrdenCompra As clsBeTrans_re_oc,
                                                       ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                                       ByVal pListStockRec As List(Of clsBeStock_rec),
                                                       ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                                       ByVal pLotesRec As clsBeTrans_oc_det_lote,
                                                       ByVal pIdEmpresa As Integer,
                                                       ByVal pIdBodega As Integer,
                                                       ByVal pIdUsuario As Integer,
                                                       ByVal pIdResolucionLp As Integer,
                                                       ByVal pBeTransOcDet As clsBeTrans_oc_det) As String

        Guardar_Recepcion_Sin_Presentacion = ""

        Try

            Dim vResult As String = ""
            vResult = clsLnTrans_re_enc.GuardarHHSP(pRecEnc,
                                                    pRecOrdenCompra,
                                                    pRecEnc.Detalle,
                                                    pRecEnc.DetalleParametros,
                                                    pListStockRecSer,
                                                    pListStockRec,
                                                    pListProductoPallet,
                                                    pLotesRec,
                                                    pIdEmpresa,
                                                    pIdBodega,
                                                    pIdUsuario,
                                                    pIdResolucionLp,
                                                    pBeTransOcDet)

            Return String.Format("Res:{0}", vResult)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdEmpresa, pIdBodega, pIdUsuario, ex.StackTrace, pRecEnc.IdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Detalle_By_IdRecepcionDet_HH(ByVal pIdRecepcionEnc As Integer,
                                                     ByVal pIdProductoBodega As Integer,
                                                     ByVal pNoLinea As Integer,
                                                     ByVal pIdOrdenCompraDet As Integer) As List(Of clsBeTrans_re_det)

        Get_Detalle_By_IdRecepcionDet_HH = Nothing

        Try

            Return clsLnTrans_re_det.Get_Detalle_By_IdRecepcionDet_HH(pIdRecepcionEnc, pIdProductoBodega, pNoLinea, pIdOrdenCompraDet)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Picking_For_HH_By_IdBodega_And_IdOperadorBodega(ByVal pIdBodega As Integer,
                                                                                ByVal pIdOperadorBodega As Integer) As List(Of clsBeTrans_picking_enc)

        Get_All_Picking_For_HH_By_IdBodega_And_IdOperadorBodega = Nothing

        Try

            Return clsLnTarea_hh.Get_All_Picking_For_HH_By_IdBodega_And_IdOperadorBodega(pIdBodega, pIdOperadorBodega)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#EJC20220112 Corrgido por Erik 
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_By_IDPickingEnc(ByVal pIdPickingEnc As Integer, ByVal pIdBodega As Integer) As List(Of clsBeTrans_picking_det)

        Get_All_By_IDPickingEnc = Nothing

        Try

            Return clsLnTrans_picking_det.Get_All_By_IdPickingEnc(pIdPickingEnc, pIdBodega)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '<WebMethod(), SoapHeader("mArch")>
    'Public Function Get_All_By_IDPickingEnc(ByVal pIdPickingEnc As Integer) As List(Of clsBeTrans_picking_det)

    '    Get_All_By_IDPickingEnc = Nothing

    '    Try

    '        Return clsLnTrans_picking_det.Get_All_By_IdPickingEnc(pIdPickingEnc)

    '    Catch ex As Exception

    '        'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
    '        Dim Mensaje As String = ex.Message
    '        WriteErrorToEventLog(Mensaje)

    '        If mArch IsNot Nothing Then

    '            If mArch.Tipo = "WM" Then
    '                Throw New Exception(Mensaje)
    '            Else
    '                Dim currrentContext As HttpContext = HttpContext.Current
    '                Dim DT As New DataTable("CustomError")
    '                DT.Columns.Add("Error", GetType(String))
    '                DT.Rows.Add(Mensaje)
    '                Dim sw As New StringWriter()
    '                DT.WriteXml(sw)
    '                HttpContext.Current.Response.Clear()
    '                HttpContext.Current.Response.StatusCode = 299
    '                HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
    '                HttpContext.Current.Response.Output.Write(sw.ToString())
    '                HttpContext.Current.Response.ContentType = "text/xml"
    '                HttpContext.Current.Response.End()
    '            End If

    '        End If

    '    End Try

    'End Function

    ''' <summary>
    ''' Lista de productos a pickear.
    ''' </summary>
    ''' <param name="pIdPickingEnc"></param>
    ''' <param name="pDetalleOperador"></param>
    ''' <param name="pIdOperadorBodega"></param>
    ''' <returns></returns>
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_PickingUbic_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                        ByVal pDetalleOperador As Boolean,
                                                        ByVal pIdOperadorBodega As Integer) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_By_IdPickingEnc = Nothing

        Try

            Return clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingEnc(pIdPickingEnc,
                                                                               pDetalleOperador,
                                                                               pIdOperadorBodega)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function


    ''' <summary>
    ''' Lista de productos a verificar.
    ''' </summary>
    ''' <param name="pIdPickingEnc"></param>
    ''' <param name="pDetalleOperador"></param>
    ''' <param name="pIdOperadorBodega"></param>
    ''' <returns></returns>
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_PickingUbic_By_IdPickingEnc_For_Verificacion(ByVal pIdPickingEnc As Integer,
                                                                         ByVal pDetalleOperador As Boolean,
                                                                         ByVal pIdOperadorBodega As Integer) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_By_IdPickingEnc_For_Verificacion = Nothing

        Try

            Return clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingEnc_For_Verificacion(pIdPickingEnc,
                                                                                                pDetalleOperador,
                                                                                                pIdOperadorBodega)

        Catch ex As Exception

            '#MECR04122025: Se agrego bitacora para logs de verificacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    ''' <summary>
    ''' Lista de productos a pickear.
    ''' </summary>
    ''' <param name="pIdPickingEnc"></param>
    ''' <param name="pDetalleOperador"></param>
    ''' <param name="pIdOperadorBodega"></param>
    ''' <returns></returns>
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_PickingUbic_By_IdPickingEnc_Tipo(ByVal pIdPickingEnc As Integer,
                                                             ByVal pDetalleOperador As Boolean,
                                                             ByVal pIdOperadorBodega As Integer,
                                                             ByVal Tipo As Integer) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_By_IdPickingEnc_Tipo = Nothing

        Try

            Return clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingEnc_Tipo(pIdPickingEnc,
                                                                                    pDetalleOperador,
                                                                                    pIdOperadorBodega,
                                                                                    Tipo)
        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_PickingUbic_By_IdPickingEnc_And_IdPedidoEnc(ByVal pIdPickingEnc As Integer,
                                                                            ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_By_IdPickingEnc_And_IdPedidoEnc = Nothing

        Try

            Return clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingEnc_And_IdPedidoEnc(pIdPickingEnc, pIdPedidoEnc)

        Catch ex As Exception

            '#MECR04122025: Se agrego bitacora para logs de verificacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Stock_By_IdRecepcionEnc_And_IdRecpecionDet(ByVal pIdRecepcionEnc As Integer, ByVal pIdRecepcionDet As Integer) As List(Of clsBeStock_rec)

        Get_Stock_By_IdRecepcionEnc_And_IdRecpecionDet = Nothing

        Try

            Return clsLnStock_rec.Get_Stock_By_IdRecepcionEnc_And_IdRecpecionDet(pIdRecepcionEnc, pIdRecepcionDet)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Stock_Rec_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeStock_rec)

        Get_Stock_Rec_By_IdRecepcionEnc = Nothing

        Try

            Return clsLnStock_rec.Get_All_By_IdRecepcionEnc(pIdRecepcionEnc)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_By_Barra_Proveedor_Cliente(ByVal Proveedor As String,
                                                                  ByVal Barra As String,
                                                                  ByVal IdOrdenCompraEnc As Integer) As clsBeStock_rec

        Get_All_By_Barra_Proveedor_Cliente = Nothing

        Try

            Return clsLnStock_rec.Get_All_By_Barra_Proveedor_Cliente(Proveedor, Barra, IdOrdenCompraEnc)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Detalle_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer, ByVal pIdBodega As Integer) As List(Of clsBeTrans_re_det)

        Get_Detalle_By_IdRecepcionEnc = Nothing

        Try

            Return clsLnTrans_re_det.Get_Detalle_By_IdRecepcionEnc(pIdRecepcionEnc, pIdBodega)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Delete_Det_By_IdRecepcionEnc_And_IdRecpecionDet(ByVal pIdOrdenCompraEnc As Integer,
                                                                    ByVal pIdRecepcionEnc As Integer,
                                                                    ByVal pIdRecepcionDet As Integer,
                                                                    ByVal pIdHost As String) As String

        Delete_Det_By_IdRecepcionEnc_And_IdRecpecionDet = ""

        Try

            Dim resultado As String = ""

            resultado = clsLnTrans_re_det.Delete_Det_By_IdRecepcionEnc_And_IdRecpecionDet_hh(pIdOrdenCompraEnc,
                                                                                             pIdRecepcionEnc,
                                                                                             pIdRecepcionDet,
                                                                                             pIdHost)


            'Return resultado
            Return String.Format("Res:{0}", resultado)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function MaxIdRecEnc() As Integer

        MaxIdRecEnc = 0

        Try

            Return clsLnTrans_re_enc.MaxID()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Sub Get_Banderas_Recepcion(ByVal pIdRecepcionEnc As Integer, ByRef pFinalizada As Boolean, ByRef pAnulada As Boolean)

        Try

            clsLnTrans_re_enc.Get_Banderas_Recepcion(pIdRecepcionEnc, pFinalizada, pAnulada)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Codigos_Barra_By_IdProducto(ByVal pIdProducto As Integer) As List(Of clsBeProducto_codigos_barra)

        Get_All_Codigos_Barra_By_IdProducto = Nothing

        Try

            Return clsLnProducto.Get_All_Codigos_Barra_By_IdProducto(pIdProducto)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Sub Get_Single_BodegaMuelle(ByRef pBeBodega_muelles As clsBeBodega_muelles)

        Try

            clsLnBodega_muelles.GetSingle(pBeBodega_muelles)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Max_IdRecepcion_Det_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As Integer

        Max_IdRecepcion_Det_By_IdRecepcionEnc = 0

        Try
            Return clsLnTrans_re_det.MaxID(pIdRecepcionEnc)
        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.            
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Detalle_OC_By_IdOrdeCompraDet(ByRef oBeTrans_oc_det As clsBeTrans_oc_det) As Boolean

        Get_Detalle_OC_By_IdOrdeCompraDet = False

        Try

            Return clsLnTrans_oc_det.Obtener(oBeTrans_oc_det)

        Catch ex As Exception

            '#MECR07102025: Se agrego nueva bitacora de logs para OC
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, oBeTrans_oc_det.IdOrdenCompraEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKFK20220524 Agregué esta funcion para obtener el detalle de la OC                        
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Detalle_OC_By_IdOrdenCompraEnc_HH(ByVal pIdOrdenCompraEnc As Integer) As List(Of clsBeTrans_oc_det)

        Get_Detalle_OC_By_IdOrdenCompraEnc_HH = Nothing

        Try

            Return clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc_HH(pIdOrdenCompraEnc)

        Catch ex As Exception

            '#MECR07102025: Se agrego nueva bitacora de logs para OC
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Es_ValidoMuelle_By_CodigoBarra(ByVal IdOBarra As String) As String

        Es_ValidoMuelle_By_CodigoBarra = ""

        Try

            Return clsLnBodega_muelles.Es_ValidoMuelle_By_CodigoBarra(IdOBarra)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Es_ValidaUbicacion_By_IdUbicacion(ByVal IdUbicacion As Integer) As String

        Es_ValidaUbicacion_By_IdUbicacion = ""

        Try

            Return clsLnBodega_ubicacion.Es_ValidaUbicacion_By_IdUbicacion(IdUbicacion)

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Ubicacion_Valida_By_IdUbicacion_And_IdEstado(ByVal IdUbicacion As Integer,
                                                ByVal IdEstado As Integer,
                                                ByVal IdBodega As Integer,
                                                ByRef pNombreUbicacion As String) As Boolean

        Ubicacion_Valida_By_IdUbicacion_And_IdEstado = False
        Try

            Return clsLnBodega_ubicacion.Ubicacion_Valida_By_IdUbicacion_And_IdEstado(IdUbicacion,
                                                                                          IdEstado,
                                                                                          IdBodega,
                                                                                          pNombreUbicacion)

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=IdBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Reemplazo_Producto_En_Picking(ByVal IdStockCambioEst As Integer,
                                                    ByVal IdPickingEnc As Integer,
                                                    ByVal IdPickingDet As Integer,
                                                    ByVal CantSol As Double,
                                                    ByVal MaquinaQueSolicita As String,
                                                    ByVal UsuarioHH As Integer,
                                                    ByVal lBeStockRes As List(Of clsBeStock_res),
                                                    ByVal IdBodega As Integer,
                                                    ByVal IdEmpresa As Integer,
                                                    ByVal IdUbicDestino As Integer,
                                                    ByVal IdEstDestino As Integer,
                                                    ByVal IdStockResCambioEst As Integer,
                                                    ByVal EsPicking As Boolean) As Boolean


        Reemplazo_Producto_En_Picking = False

        Try

            'Return clsLnTrans_picking_ubic.Reemplazo_Producto_En_Picking(IdStockCambioEst,
            '                                                                 IdPickingEnc,
            '                                                                 IdPickingDet,
            '                                                                 CantSol,
            '                                                                 MaquinaQueSolicita,
            '                                                                 UsuarioHH,
            '                                                                 lBeStockRes,
            '                                                                 IdBodega,
            '                                                                 IdEmpresa,
            '                                                                 IdUbicDestino,
            '                                                                 IdEstDestino,
            '                                                                 IdStockResCambioEst,
            '                                                                 EsPicking)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdEmpresa:=IdEmpresa,
                                                  pIdBodega:=IdBodega,
                                                  pUserAgr:=UsuarioHH,
                                                  pIdPickingEnc:=IdPickingEnc,
                                                  pIdPickingDet:=IdPickingDet)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKFK 20180422 11:20 PM Creé la función SustituirProductoNEPicking
    <WebMethod(), SoapHeader("mArch")>
    Public Function Sustituir_Producto_NE_Picking(ByVal IdPickingEnc As Integer,
                                                   ByVal IdPickingDet As Integer,
                                                   ByVal CantSol As Double,
                                                   ByVal MaquinaQueSolicita As String,
                                                   ByVal UsuarioHH As Integer,
                                                   ByVal lBeStockRes As List(Of clsBeStock_res),
                                                   ByVal IdBodega As Integer,
                                                   ByVal IdEmpresa As Integer,
                                                   ByVal IdStockProductoNE As Integer,
                                                   ByVal IdStockResProductoNE As Integer,
                                                   ByRef Resultado As String) As Boolean

        Sustituir_Producto_NE_Picking = False

        Try

            Return clsLnTrans_picking_ubic.Sustituir_Producto_NE_Picking(IdPickingEnc,
                                                                             IdPickingDet,
                                                                             CantSol,
                                                                             MaquinaQueSolicita,
                                                                             UsuarioHH,
                                                                             lBeStockRes,
                                                                             IdBodega,
                                                                             IdEmpresa,
                                                                             IdStockProductoNE,
                                                                             IdStockResProductoNE,
                                                                             Resultado)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdEmpresa:=IdEmpresa,
                                                  pIdBodega:=IdBodega,
                                                  pUserAgr:=UsuarioHH,
                                                  pIdPickingEnc:=IdPickingEnc,
                                                  pIdPickingDet:=IdPickingDet)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKFK 20180423 11:20 PM Creé la función SustituirProductoNEPicking_Test
    <WebMethod(), SoapHeader("mArch")>
    Public Function SustituirProductoNEPicking_Test(ByVal IdPickingEnc As Integer,
                                                   ByVal IdPickingDet As Integer,
                                                   ByVal CantSol As Double,
                                                   ByVal MaquinaQueSolicita As String,
                                                   ByVal UsuarioHH As Integer,
                                                   ByVal IdBodega As Integer,
                                                   ByVal IdEmpresa As Integer,
                                                   ByVal IdStockProductoNE As Integer,
                                                   ByVal IdStockResProductoNE As Integer,
                                                   ByVal IdPedidoDet As Integer,
                                                   ByVal IdPropietarioBodega As Integer,
                                                   ByVal IdPedidoEnc As Integer) As Boolean

        SustituirProductoNEPicking_Test = False

        Try

            Return clsLnTrans_picking_ubic.SustituirProductoNEPicking_Test(IdPickingEnc,
                                                                           IdPickingDet,
                                                                           CantSol,
                                                                           MaquinaQueSolicita,
                                                                           UsuarioHH,
                                                                           IdBodega,
                                                                           IdEmpresa,
                                                                           IdStockProductoNE,
                                                                           IdStockResProductoNE,
                                                                           IdPedidoDet,
                                                                           IdPropietarioBodega,
                                                                           IdPedidoEnc)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdEmpresa:=IdEmpresa,
                                                  pIdBodega:=IdBodega,
                                                  pUserAgr:=UsuarioHH,
                                                  pIdPickingEnc:=IdPickingEnc,
                                                  pIdPickingDet:=IdPickingDet)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '<WebMethod(), SoapHeader("mArch")>
    'Public Function ReemplazoProductoPickingTest(ByVal IdStockCambioEst As Integer,
    '                                             ByVal IdPickingEnc As Integer,
    '                                             ByVal IdPickingDet As Integer,
    '                                             ByVal CantSol As Double,
    '                                             ByVal MaquinaQueSolicita As String,
    '                                             ByVal UsuarioHH As Integer,
    '                                             ByVal IdBodega As Integer,
    '                                             ByVal IdEmpresa As Integer,
    '                                             ByVal IdUbicDestino As Integer,
    '                                             ByVal IdEstDestino As Integer,
    '                                             ByVal IdStockResCambioEst As Integer,
    '                                             ByVal IdPedidoDet As Integer,
    '                                             ByVal IdPropietarioBodega As Integer,
    '                                             ByVal IdPedidoEnc As Integer) As Boolean

    '    ReemplazoProductoPickingTest = False

    '    Try

    '        Return clsLnTrans_picking_ubic.ReemplazoProductoPickingTest(IdStockCambioEst,
    '                                                                    IdPickingEnc,
    '                                                                    IdPickingDet,
    '                                                                    CantSol,
    '                                                                    MaquinaQueSolicita,
    '                                                                    UsuarioHH,
    '                                                                    IdBodega,
    '                                                                    IdEmpresa,
    '                                                                    IdUbicDestino,
    '                                                                    IdEstDestino,
    '                                                                    IdStockResCambioEst,
    '                                                                    IdPedidoDet,
    '                                                                    IdPropietarioBodega,
    '                                                                    IdPedidoEnc)

    '    Catch ex As Exception

    '        'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
    '        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)

    '        Dim Mensaje As String = ex.Message
    '        WriteErrorToEventLog(Mensaje)

    '        If mArch IsNot Nothing Then

    '            If mArch.Tipo = "WM" Then
    '                Throw New Exception(Mensaje)
    '            Else
    '                Dim currrentContext As HttpContext = HttpContext.Current
    '                Dim DT As New DataTable("CustomError")
    '                DT.Columns.Add("Error", GetType(String))
    '                DT.Rows.Add(Mensaje)
    '                Dim sw As New StringWriter()
    '                DT.WriteXml(sw)
    '                HttpContext.Current.Response.Clear()
    '                HttpContext.Current.Response.StatusCode = 299
    '                HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
    '                HttpContext.Current.Response.Output.Write(sw.ToString())
    '                HttpContext.Current.Response.ContentType = "text/xml"
    '                HttpContext.Current.Response.End()
    '            End If

    '        End If

    '    End Try

    'End Function

    '#CKFK20220208 Recuperada para WM
    <WebMethod(), SoapHeader("mArch")>
    Public Sub Genera_Tarea_Cambio_Estado_Por_Producto_Dañado(ByVal IdBodega As Integer,
                                                              ByVal IdEmpresa As Integer,
                                                              ByVal IdStock As Integer,
                                                              ByVal IdStockRes As Integer,
                                                              ByVal UsuarioHH As Integer,
                                                              ByVal CantDañada As Double,
                                                              ByVal IdUbicDest As Integer,
                                                              ByVal IdEstadoDest As Integer,
                                                              ByVal IdPropietarioBodega As Integer,
                                                              ByVal IdPickingUbic As Integer,
                                                              ByVal EsPicking As Boolean)

        Try

            clsLnTrans_ubic_hh_enc.Genera_Tarea_Cambio_Estado_Por_Producto_Dañado(IdBodega,
                                                                                  IdEmpresa,
                                                                                  IdStock,
                                                                                  IdStockRes,
                                                                                  UsuarioHH,
                                                                                  CantDañada,
                                                                                  IdUbicDest,
                                                                                  IdEstadoDest,
                                                                                  IdPropietarioBodega,
                                                                                  IdPickingUbic,
                                                                                  EsPicking)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdEmpresa:=IdEmpresa,
                                                  pIdBodega:=IdBodega,
                                                  pUserAgr:=UsuarioHH,
                                                  pIdPickingUbic:=IdPickingUbic)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    '#CKFK 20180422 04:24 PM Funciones y procedimientos creados por producto no encontrado en picking
    <WebMethod(), SoapHeader("mArch")>
    Public Function Marcar_No_Encontrado(ByVal IdBodega As Integer,
                                                  ByVal IdEmpresa As Integer,
                                                  ByVal IdStock As Integer,
                                                  ByVal IdStockRes As Integer,
                                                  ByVal UsuarioHH As Integer,
                                                  ByVal IdPickingEnc As Integer,
                                                  ByVal CantNoEncontrada As Double,
                                                  ByVal IdPropietarioBodega As Integer,
                                                  ByVal IdPickingUbic As Integer) As String

        Dim vResult As String = ""

        Try

            vResult = clsLnTrans_picking_ubic.Marcar_No_Encontrado(IdBodega,
                                                                 IdEmpresa,
                                                                 IdStock,
                                                                 IdStockRes,
                                                                 IdPickingEnc,
                                                                 UsuarioHH,
                                                                 CantNoEncontrada,
                                                                 IdPropietarioBodega,
                                                                 IdPickingUbic)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdEmpresa:=IdEmpresa,
                                                  pIdBodega:=IdBodega,
                                                  pUserAgr:=UsuarioHH,
                                                  pIdPickingUbic:=IdPickingUbic)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

        Return vResult

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Reservar_Stock_By_IdStock(ByVal IdStock As Integer,
                                              ByVal CantSol As Double,
                                              ByVal MaquinaQueSolicita As String,
                                              ByVal IdPickingEnc As Integer,
                                              ByVal IdPedidoEnc As Integer,
                                              ByVal IdUsuarioHH As Integer,
                                              ByVal IdPedidoDet As Integer,
                                              ByVal IdPickingUbic As Integer,
                                              ByVal EsPicking As Boolean,
                                              ByVal IdPresentacionPedido As Integer,
                                              ByVal Tipo As Integer) As Boolean

        Reservar_Stock_By_IdStock = False

        Try

            Return clsLnStock_res.Reservar_Stock_By_IdStock(IdStock,
                                                            CantSol,
                                                            MaquinaQueSolicita,
                                                            IdPickingEnc,
                                                            IdPedidoEnc,
                                                            IdUsuarioHH,
                                                            IdPedidoDet,
                                                            IdPickingUbic,
                                                            EsPicking,
                                                            IdPresentacionPedido,
                                                            Tipo)


        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdPickingEnc:=IdPickingEnc,
                                                  pIdPedidoEnc:=IdPedidoEnc,
                                                  pIdPedidoDet:=IdPedidoDet,
                                                  pIdPickingUbic:=IdPickingUbic)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKFK20220208 Creada para Android porque se usa en WM (Reservar_Stock_By_IdStock)
    <WebMethod(), SoapHeader("mArch")>
    Public Function Reservar_Stock_By_Stock(ByVal pBeStock_res As clsBeStock_res,
                                              ByVal CantSol As Double,
                                              ByVal MaquinaQueSolicita As String,
                                              ByVal IdPickingEnc As Integer,
                                              ByVal IdPedidoEnc As Integer,
                                              ByVal IdUsuarioHH As Integer,
                                              ByVal IdPedidoDet As Integer,
                                              ByVal IdPickingUbic As Integer,
                                              ByVal EsPicking As Boolean,
                                              ByVal IdPresentacionPedido As Integer,
                                              ByVal Tipo As Integer,
                                              ByVal IdUbicDestino As Integer,
                                              ByVal IdEstDestino As Integer,
                                              ByRef CantPend As Double) As Boolean

        Reservar_Stock_By_Stock = False

        Try

            Return clsLnStock_res.Reemplazar_IdStock_By_Stock(pBeStock_res,
                                                            CantSol,
                                                            MaquinaQueSolicita,
                                                            IdPickingEnc,
                                                            IdPedidoEnc,
                                                            IdUsuarioHH,
                                                            IdPedidoDet,
                                                            IdPickingUbic,
                                                            EsPicking,
                                                            Tipo,
                                                            IdUbicDestino,
                                                            IdEstDestino,
                                                            CantPend)


        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdPickingEnc:=IdPickingEnc,
                                                  pIdPedidoEnc:=IdPedidoEnc,
                                                  pIdPedidoDet:=IdPedidoDet,
                                                  pIdPickingUbic:=IdPickingUbic)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Reemplazar_ListaPu_By_Stock(ByVal pBeStock_res As clsBeStock_res,
                                              ByVal CantSol As Double,
                                              ByVal MaquinaQueSolicita As String,
                                              ByVal IdPickingEnc As Integer,
                                              ByVal IdPedidoEnc As Integer,
                                              ByVal IdUsuarioHH As Integer,
                                              ByVal IdPedidoDet As Integer,
                                              ByVal BePickingUbic As clsBeTrans_picking_ubic,
                                              ByVal EsPicking As Boolean,
                                              ByVal IdPresentacionPedido As Integer,
                                              ByVal Tipo As Integer,
                                              ByVal IdUbicDestino As Integer,
                                              ByVal IdEstDestino As Integer,
                                              ByVal CantidadTotal As Double,
                                              ByRef CantPend As Double,
                                              ByVal ConExistencia As Boolean) As Boolean

        Reemplazar_ListaPu_By_Stock = False

        Try

            If ConExistencia Then

                Return clsLnStock_res.Reemplazar_ListaPu_By_Stock(pBeStock_res,
                                                                  CantSol,
                                                                  MaquinaQueSolicita,
                                                                  IdPickingEnc,
                                                                  IdPedidoEnc,
                                                                  IdUsuarioHH,
                                                                  IdPedidoDet,
                                                                  BePickingUbic,
                                                                  EsPicking,
                                                                  Tipo,
                                                                  IdUbicDestino,
                                                                  IdEstDestino,
                                                                  CantidadTotal,
                                                                  CantPend)
            Else

                Return clsLnStock_res.Reemplazar_ListaPu_By_Stock_Sin_Exist(CantSol,
                                                                            IdUsuarioHH,
                                                                            BePickingUbic,
                                                                            EsPicking,
                                                                            Tipo,
                                                                            CantidadTotal,
                                                                            CantPend,
                                                                            IdUbicDestino,
                                                                            IdEstDestino)
            End If


        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                      pStackTrace:=ex.StackTrace,
                                      pIdPickingEnc:=IdPickingEnc,
                                      pIdPedidoEnc:=IdPedidoEnc,
                                      pIdPedidoDet:=IdPedidoDet,
                                      pIdPickingUbic:=BePickingUbic.IdPickingUbic)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#AT 20220127 Cree estan función para el reemplazo en la verificación
    <WebMethod(), SoapHeader("mArch")>
    Public Function Reemplazar_ListPickingUbic_Verificacion(ByVal plistPickingUbi As clsBeTrans_picking_ubic,
                                                            ByVal pBeStockRes As clsBeStock_res,
                                                            ByVal pIdPickingEnc As Integer,
                                                            ByVal pIdOperador As Integer,
                                                            ByVal pHost As String,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pIdEmpresa As Integer,
                                                            ByVal pIdUbicDestino As Integer,
                                                            ByVal pIdEstadoDestino As Integer,
                                                            ByVal pCantLinea As Double,
                                                            ByRef pCantReemplazar As Double,
                                                            ByVal pCantTotal As Double,
                                                            ByRef CantPend As Double,
                                                            ByVal ConExistencia As Boolean) As Boolean

        Reemplazar_ListPickingUbic_Verificacion = False

        Try

            If ConExistencia Then

                Return clsLnStock_res.Reemplazo_Verificacion_By_ListPickingUbic(plistPickingUbi,
                                                                            pBeStockRes,
                                                                            pIdPickingEnc,
                                                                            pIdOperador,
                                                                            pHost,
                                                                            pIdBodega,
                                                                            pIdEmpresa,
                                                                            pIdUbicDestino,
                                                                            pIdEstadoDestino,
                                                                            pCantLinea,
                                                                            pCantReemplazar,
                                                                            pCantTotal,
                                                                            CantPend)
            Else

                Return clsLnStock_res.Reemplazar_ListaPu_By_Stock_Sin_Exist(pCantReemplazar,
                                                                            pIdOperador,
                                                                            plistPickingUbi,
                                                                            False,
                                                                            1,
                                                                            pCantTotal,
                                                                            CantPend,
                                                                            pIdUbicDestino,
                                                                            pIdEstadoDestino)

            End If


        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdEmpresa:=pIdEmpresa,
                                                  pIdBodega:=pIdBodega,
                                                  pUserAgr:=pIdOperador,
                                                  pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#AT 20240515 Funcion para reemplazo automatico
    <WebMethod(), SoapHeader("mArch")>
    Public Function Reemplazo_Automatico(pStockRes As clsBeStock_res,
                                         CantSol As Double,
                                         pPickingUbic As clsBeTrans_picking_ubic,
                                         IdUsuarioHH As Integer,
                                         Tipo As Integer,
                                         ByRef CantPend As Double) As List(Of clsBeTrans_picking_ubic)
        Reemplazo_Automatico = Nothing

        Try

            If Tipo = 1 Then
                Reemplazo_Automatico = clsLnStock_res.Reemplazo_Automatico_Conso(pStockRes,
                                                                                CantSol,
                                                                                "1",
                                                                                pPickingUbic.IdPickingEnc,
                                                                                pPickingUbic.IdPedidoEnc,
                                                                                IdUsuarioHH,
                                                                                pPickingUbic.IdPedidoDet,
                                                                                pPickingUbic,
                                                                                True,
                                                                                Tipo,
                                                                                pPickingUbic.IdUbicacion,
                                                                                pPickingUbic.IdProductoEstado,
                                                                                CantSol,
                                                                                CantPend)
            Else
                Reemplazo_Automatico = clsLnStock_res.Reemplazo_Automatico(pStockRes,
                                                                            CantSol,
                                                                            "1",
                                                                            pPickingUbic.IdPickingEnc,
                                                                            pPickingUbic.IdPedidoEnc,
                                                                            IdUsuarioHH,
                                                                            pPickingUbic.IdPedidoDet,
                                                                            pPickingUbic.IdPickingUbic,
                                                                            True,
                                                                            1,
                                                                            pPickingUbic.IdUbicacion,
                                                                            pPickingUbic.IdProductoEstado,
                                                                            pPickingUbic,
                                                                            CantPend)
            End If

            Return Reemplazo_Automatico
        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pUserAgr:=IdUsuarioHH,
                                                  pIdPickingEnc:=pPickingUbic.IdPickingEnc,
                                                  pIdPickingDet:=pPickingUbic.IdPickingDet,
                                                  pIdPickingUbic:=pPickingUbic.IdPickingUbic,
                                                  pIdPedidoEnc:=pPickingUbic.IdPedidoEnc,
                                                  pIdPedidoDet:=pPickingUbic.IdPedidoDet)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try
    End Function

    '#AT20240525 Función para validar licencia por producto y bodega 
    <WebMethod(), SoapHeader("mArch")>
    Public Function Valida_Licencia_By_ProductoBodega(pLicencia As String,
                                                      pIdBodega As Integer, pIdProductoBodega As Integer) As Boolean
        Valida_Licencia_By_ProductoBodega = False

        Try

            Return clsLnStock.Existe_Lp_In_Stock_By_IdProductoBodega(pLicencia,
                                                                     pIdBodega,
                                                                     pIdProductoBodega)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try
    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Reservar_Y_Reemplazar_Stock_By_IdStock(ByVal IdStockReservarDesde As Integer,
                                                            ByVal IdStockOriginal As Integer,
                                                            ByVal CantSol As Double,
                                                            ByVal MaquinaQueSolicita As String,
                                                            ByVal IdPickingEnc As Integer,
                                                            ByVal IdPedidoEnc As Integer,
                                                            ByVal IdUsuarioHH As Integer,
                                                            ByVal IdPedidoDet As Integer,
                                                            ByVal IdPickingUbic As Integer,
                                                            ByVal EsPicking As Boolean,
                                                            ByVal IdPresentacionPedido As Integer,
                                                            ByVal IdPickingDet As Integer,
                                                            ByVal IdBodega As Integer,
                                                            ByVal IdEmpresa As Integer,
                                                            ByVal IdUbicDestino As Integer,
                                                            ByVal IdProductoEstadoDestino As Integer,
                                                            ByVal IdStockResOrigen As Integer,
                                                            ByVal MarcarComoNE As Boolean) As Boolean



        Reservar_Y_Reemplazar_Stock_By_IdStock = False

        Try



            If clsLnStock_res.Reservar_Y_Reemplazar_Stock_By_IdStock(IdStockReservarDesde,
                                                            IdStockOriginal,
                                                            CantSol,
                                                            MaquinaQueSolicita,
                                                            IdPickingEnc,
                                                            IdPickingDet,
                                                            IdPedidoEnc,
                                                            IdUsuarioHH,
                                                            IdPedidoDet,
                                                            IdPickingUbic,
                                                            EsPicking,
                                                            IdPresentacionPedido,
                                                            IdBodega,
                                                            IdEmpresa,
                                                            IdUbicDestino,
                                                            IdProductoEstadoDestino,
                                                            IdStockResOrigen,
                                                            MarcarComoNE) Then

                Reservar_Y_Reemplazar_Stock_By_IdStock = True

            End If



        Catch ex As Exception


            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdEmpresa:=IdEmpresa,
                                                  pIdBodega:=IdBodega,
                                                  pUserAgr:=IdUsuarioHH,
                                                  pIdPickingEnc:=IdPickingEnc,
                                                  pIdPickingDet:=IdPickingDet,
                                                  pIdPickingUbic:=IdPickingUbic)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Sub Actualizar_Estado_Recepcion(ByVal pIdRecepcionEnc As Integer, ByRef Estado As String)

        Try

            clsLnTrans_re_enc.Actualizar_Estado_Recepcion(pIdRecepcionEnc, Estado)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Sub Guarda_Firma_Recepcion(ByVal pIdRecepcionEnc As Integer, ByVal Firma_piloto As Byte())

        Try

            clsLnTrans_re_enc.Guarda_Firma_Recepcion(pIdRecepcionEnc, Firma_piloto)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Sub Guardar_Fotos_Recepcion(ByVal pIdRecepcionEnc As Integer, ByVal Foto As Byte())

        Try

            'CM_20201201:Guarda fotos de recepcion
            clsLnTrans_re_img.Guardar_Imagen_Recepcion(pIdRecepcionEnc, Foto)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Guardar_Producto_Imagen(ByVal pBeProductoImagen As clsBeProducto_imagen) As Boolean

        Guardar_Producto_Imagen = False

        Try

            Return clsLnProducto_imagen.Guardar_Imagen(pBeProductoImagen)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try
    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Sub Guardar_Fotos_Verificacion(ByVal pIdPedidoDet As Integer, ByVal Foto As Byte())

        Try

            '#CKFK20231029:Guarda fotos de verificacion
            clsLnTrans_Picking_Img.Guardar_Imagen_Verificacion(pIdPedidoDet, Foto)

        Catch ex As Exception

            '#MECR04122025: Se agrego bitacora de logs para verificacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_verificacion_bof.Agregar_Error(vMsgError,
            'pStackTrace:=ex.StackTrace,
            'pIdPickingDet:=pIdPedidoDet)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Producto_Imagen(ByVal pIdProducto As Integer) As List(Of clsBeProducto_imagen)

        Get_All_Producto_Imagen = Nothing

        Try

            Return clsLnProducto_imagen.Get_All_By_IdProducto(pIdProducto)

        Catch ex As Exception
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If
        End Try
    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Imagen_Recepcion(ByVal pIdRecepcion As Integer) As List(Of clsBeTrans_re_img)

        Get_All_Imagen_Recepcion = Nothing

        Try

            Return clsLnTrans_re_img.Get_All_Imagen_Recepcion_By_IdRecepcion(pIdRecepcion)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcion)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If
        End Try
    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Imagen_Verificacion(ByVal pIdPedidoDet As Integer) As List(Of clsBeTrans_picking_img)

        Get_All_Imagen_Verificacion = Nothing

        Try

            Return clsLnTrans_Picking_Img.Get_All_Imagen_By_IdPedidoDet(pIdPedidoDet)

        Catch ex As Exception
            '#MECR04122025: Se agrego bitacora de logs para verificacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_verificacion_bof.Agregar_Error(vMsgError,
            'pStackTrace:=ex.StackTrace,
            'pIdPedidoDet:=pIdPedidoDet)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If
        End Try
    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Reemplazo_By_IdPedidoDet(ByVal pIdPedidoDet As Integer,
                                          ByVal pIdPropietarioBodega As Integer,
                                          ByVal pIdPickingEnc As Integer,
                                          ByVal pIdPedidoEnc As Integer) As List(Of clsBeStock_res)

        Get_All_Reemplazo_By_IdPedidoDet = Nothing

        Try

            Return clsLnStock_res.Get_All_Reemplazo_By_IdPedidoDet(pIdPedidoDet,
                                                                        pIdPropietarioBodega,
                                                                        pIdPickingEnc,
                                                                        pIdPedidoEnc)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdPickingEnc:=pIdPickingEnc,
                                                  pIdPedidoEnc:=pIdPedidoEnc,
                                                  pIdPedidoDet:=pIdPedidoDet)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet(ByVal pIdRecepcionEnc As Integer,
                                                                                          ByVal pIdRecepcionDet As Integer) As Double

        Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet = False

        Try

            Return clsLnTrans_re_enc.Get_Cantidad_Recibida_Actual_By_IdRecepcionEnc_And_IdRecepcionDet(pIdRecepcionEnc, pIdRecepcionDet)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_IdUbicMerma_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Get_IdUbicMerma_By_IdBodega = 0

        Try

            Return clsLnBodega.Get_IdUbicMerma_By_IdBodega(pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_IdUbicacion_Picking_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Get_IdUbicacion_Picking_By_IdBodega = 0

        Try

            Return clsLnBodega.Get_IdUbicacion_Picking_By_IdBodega(pIdBodega)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    ' Cambio de Ubicacion / estado
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Cambio_Ubic_By_IdBodega_And_IdOperador(pIdBodega As Integer, ByVal pIdOperador As Integer, pIdTarea As Integer, cambio_estado As Boolean) As List(Of clsBeTrans_ubic_hh_enc)

        Get_All_Cambio_Ubic_By_IdBodega_And_IdOperador = Nothing

        Try

            Return clsLnTrans_ubic_hh_enc.Get_All_Pendientes_By_IdBodega_And_IdOperador(pIdBodega,
                                                                                        pIdOperador,
                                                                                        pIdTarea,
                                                                                        cambio_estado)

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pIdOperador:=pIdOperador)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_By_IdTransUbicEnc_And_IdOperador(ByVal pIdTransUbicHhEnc As Integer, ByVal pIdOperador As Integer) As List(Of clsBeTrans_ubic_hh_det)

        Get_All_By_IdTransUbicEnc_And_IdOperador = Nothing

        Try

            Return clsLnTrans_ubic_hh_det.Get_All_By_IdTransUbicEnc_And_IdOperador(pIdTransUbicHhEnc, pIdOperador, True)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#MA20250210 migracion de xml a Json
    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Get_Ubicacion_By_Codigo_Barra_And_IdBodega_JSON(ByVal pBarra As String,
                                                                    ByVal pIdBodega As Integer) As clsBeBodega_ubicacion



        Try
            'No se encontró ninguna ubicación con el código de barra especificado.
            ' Obtener la ubicación
            Dim curContext As HttpContext = HttpContext.Current

            Dim ubicacion As clsBeBodega_ubicacion = clsLnBodega_ubicacion.Get_Ubicacion_By_Codigo_Barra_And_IdBodega(pBarra, pIdBodega)

            Dim BeUbicacion As String = JsonConvert.SerializeObject(
                New With {
                    .ubicacion = ubicacion
                },
                New JsonSerializerSettings With {
                    .NullValueHandling = NullValueHandling.Include,
                    .ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    .Formatting = Formatting.None
                })

            curContext.Response.Clear()
            curContext.Response.ContentType = "application/json"
            curContext.Response.StatusCode = 200
            curContext.Response.Write(BeUbicacion)


            Return Nothing 'retorno en éxito

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

            Return Nothing

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_Trans_Ubic_HH_Det(ByVal oBeTrans_ubic_hh_det As clsBeTrans_ubic_hh_det,
                                                 ByVal pMovimiento As clsBeTrans_movimientos,
                                                 ByVal pPosiciones As Integer,
                                                 ByVal pIdReabastecimientoLog As Integer) As Boolean

        Actualizar_Trans_Ubic_HH_Det = False

        Try

            If pMovimiento Is Nothing Then
                If clsLnTrans_ubic_hh_det.Procesar_Cambio_Ubicacion_Dirigido(oBeTrans_ubic_hh_det, pIdReabastecimientoLog) > 0 Then
                    Actualizar_Trans_Ubic_HH_Det = True
                End If
            Else
                Actualizar_Trans_Ubic_HH_Det = clsLnTrans_ubic_hh_det.Guardar_Detalle(oBeTrans_ubic_hh_det,
                                                                                      pMovimiento,
                                                                                      pIdReabastecimientoLog,
                                                                                      pPosiciones)
            End If

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdTareaUbicacionEnc:=oBeTrans_ubic_hh_det.IdTareaUbicacionEnc,
                                                  pIdTareaUbicacionDet:=oBeTrans_ubic_hh_det.IdTareaUbicacionDet,
                                                  pIdStock:=oBeTrans_ubic_hh_det.IdStock,
                                                  pIdUMBAs:=oBeTrans_ubic_hh_det.UnidadMedida.IdUnidadMedida,
                                                  pIdPresentacion:=oBeTrans_ubic_hh_det.ProductoPresentacion.IdPresentacion,
                                                  pIdUbicacionOrigen:=oBeTrans_ubic_hh_det.IdUbicacionOrigen,
                                                  pIdUbicacionDestino:=oBeTrans_ubic_hh_det.IdUbicacionDestino,
                                                  pIdEstadoOrigen:=oBeTrans_ubic_hh_det.IdEstadoOrigen,
                                                  pIdEstadoDestino:=oBeTrans_ubic_hh_det.IdEstadoDestino,
                                                  pCantidad:=oBeTrans_ubic_hh_det.Cantidad,
                                                  pIdOperador:=oBeTrans_ubic_hh_det.IdOperadorBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Aplicar_Cambio_Estado_HH(ByVal pMovimiento As clsBeTrans_movimientos, idstock As Integer) As String

        Aplicar_Cambio_Estado_HH = ""

        Try

            Return clsLnTrans_ubic_hh_det.Aplicar_Movimiento(pMovimiento, idstock)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Aplica_Cambio_Estado_Ubic_HH(ByVal pMovimiento As clsBeTrans_movimientos,
                                                 ByVal pStockRes As clsBeVW_stock_res,
                                                 ByRef pIdStockNuevo As Integer,
                                                 ByRef pIdMovimientoNuevo As Integer,
                                                 ByVal pPosiciones As Integer) As Boolean

        Aplica_Cambio_Estado_Ubic_HH = False


        Try
            '#MECR25112025: Sea grego bitacora de logs para reabastecimiento
            '#GT28072023: control de llamadas al ws
            'clsLnLog_error_wms.Agregar_Error(pMovimiento.IdEmpresa, pMovimiento.IdBodegaOrigen, "Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: " & pMovimiento.IdOperadorBodega & " y TipoTarea " & pMovimiento.IdTipoTarea)
            Dim msjControl As String = "Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: " & pMovimiento.IdOperadorBodega & " y TipoTarea " & pMovimiento.IdTipoTarea
            clsLnLog_error_wms_reab.Agregar_Error(msjControl,
                                                  pIdStock:=pIdStockNuevo,
                                                  pIdMovimiento:=pIdMovimientoNuevo,
                                                  pLic_Plate:=pMovimiento.Lic_plate,
                                                  pIdProductoBodega:=pMovimiento.IdProductoBodega,
                                                  pCantidad:=pMovimiento.Cantidad)

            Return clsLnTrans_ubic_hh_det.Aplica_Cambio_Estado_Ubic(pMovimiento,
                                                                    pStockRes,
                                                                    pIdStockNuevo,
                                                                    pIdMovimientoNuevo,
                                                                    pPosiciones)

        Catch ex As Exception

            '#MECR25112025: Sea grego bitacora de logs para reabastecimiento
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_reab.Agregar_Error(vMsgError,
                                                  pIdStock:=pIdStockNuevo,
                                                  pIdMovimiento:=pIdMovimientoNuevo,
                                                  pLic_Plate:=pMovimiento.Lic_plate,
                                                  pIdProductoBodega:=pMovimiento.IdProductoBodega,
                                                  pCantidad:=pMovimiento.Cantidad)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Aplica_Cambio_Estado_Ubic_HH_LicCompleta(ByVal pStockResList As List(Of clsBeVW_stock_res)) As Boolean

        Aplica_Cambio_Estado_Ubic_HH_LicCompleta = False


        Try

            For Each BeVWStockRes As clsBeVW_stock_res In pStockResList
                clsLnLog_error_wms.Agregar_Error(BeVWStockRes.Movimiento.IdEmpresa, BeVWStockRes.Movimiento.IdBodegaOrigen, "Aplica_Cambio_Estado_Ubic_HH: llamada de WS con usuario: " & BeVWStockRes.Movimiento.IdOperadorBodega & " y TipoTarea " & BeVWStockRes.Movimiento.IdTipoTarea)
                Aplica_Cambio_Estado_Ubic_HH_LicCompleta = clsLnTrans_ubic_hh_det.Aplica_Cambio_Estado_Ubic(BeVWStockRes.Movimiento, BeVWStockRes, 0, 0)
            Next

            Return Aplica_Cambio_Estado_Ubic_HH_LicCompleta

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Set_Nuevo_Pallet_Id(ByVal pIdBodega As Integer,
                                            ByVal pIdUsuario As Integer,
                                            ByVal pLicPlateAnt As String,
                                            ByVal pLicPlateNuevo As String,
                                            ByVal pIdStockNuevo As Integer,
                                            ByVal pIdMovimientoNuevo As Integer,
                                            ByVal pIdResolucion As Integer) As Boolean

        Set_Nuevo_Pallet_Id = False

        Try

            '#MECR25112025: Se agrego bitacora de logs para Reabastecimiento
            '#GT28072023: control de llamadas al ws
            'clsLnLog_error_wms.Agregar_Error(0, pIdBodega, "Set_Nuevo_Pallet_Id: llamada de WS con usuario: " & pIdUsuario)
            Dim msjControl As String = "Set_Nuevo_Pallet_Id: llamada de WS con usuario: " & pIdUsuario
            clsLnLog_error_wms_reab.Agregar_Error(msjControl,
                                                  pIdBodega:=pIdBodega,
                                                  pIdStock:=pIdStockNuevo,
                                                  pIdMovimiento:=pIdMovimientoNuevo,
                                                  pLic_Plate_Anterior:=pLicPlateAnt,
                                                  pLic_Plate:=pLicPlateNuevo,
                                                  pIdResolucion:=pIdResolucion,
                                                  pUser_agr:=pIdUsuario)

            Return clsLnStock.Actualizar_PalletId_Por_Explosion(pIdBodega,
                                                                    pIdUsuario,
                                                                    pIdStockNuevo,
                                                                    pIdMovimientoNuevo,
                                                                    pLicPlateAnt,
                                                                    pLicPlateNuevo,
                                                                    pIdResolucion)

        Catch ex As Exception

            '#MECR25112025: Se agrego bitacora de logs para Reabastecimiento
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_reab.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdBodega:=pIdBodega,
                                                  pIdStock:=pIdStockNuevo,
                                                  pIdMovimiento:=pIdMovimientoNuevo,
                                                  pLic_Plate_Anterior:=pLicPlateAnt,
                                                  pLic_Plate:=pLicPlateNuevo,
                                                  pIdResolucion:=pIdResolucion,
                                                  pUser_agr:=pIdUsuario)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Set_LP_Stock(ByVal pMovimiento As clsBeTrans_movimientos, pStockRes As clsBeVW_stock_res, pIdResolucionLp As Integer) As String

        Set_LP_Stock = ""

        Try

            Return clsLnTrans_ubic_hh_det.Aplica_LP_Stock(pMovimiento, pStockRes, pIdResolucionLp, True)

        Catch ex As Exception

            '#MECR19112025: Se agrego bitacora de logs para implosion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdEmpresa:=pMovimiento.IdEmpresa,
                                                  pIdBodega:=pMovimiento.IdBodegaDestino,
                                                  pIdPedidoEnc:=pMovimiento.IdPedidoEnc,
                                                  pIdDespachoEnc:=pMovimiento.IdDespachoEnc,
                                                  pIdProductoBodega:=pMovimiento.IdProductoBodega,
                                                  pIdPresentacion:=pMovimiento.IdPresentacion,
                                                  pIdUnidadMedida:=pMovimiento.IdUnidadMedida,
                                                  pLic_Plate:=pMovimiento.Lic_plate,
                                                  pIdOperador:=pMovimiento.IdOperadorBodega,
                                                  pUsuario_agr:=pMovimiento.Usuario_agr,
                                                  pEsImplosion:=True)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#AT20240630 Proceso de implosion lp mixto
    <WebMethod(), SoapHeader("mArch")>
    Public Function Set_LP_Stock_Mixto(ByVal pStockResList As List(Of clsBeVW_stock_res), pIdResolucionLp As Integer) As Boolean

        Set_LP_Stock_Mixto = False

        Try
            Return Set_LP_Stock_Mixto = clsLnTrans_ubic_hh_det.Aplica_LP_Stock_Mixto(pStockResList, pIdResolucionLp)
        Catch ex As Exception

            '#MECR19112025: Se agrego bitacora de logs para implosion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_Estado_Cambio(ByVal pIdTransUbicHHEnc As clsBeTrans_ubic_hh_enc, ByVal finalizar As Boolean) As Integer

        Actualizar_Estado_Cambio = 0

        Try

            If Not finalizar Then
                Return clsLnTrans_ubic_hh_enc.Actualizar(pIdTransUbicHHEnc)
            Else
                Return clsLnTrans_ubic_hh_enc.Actualiza_Cambio_Estado(pIdTransUbicHHEnc)
            End If

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_IdUbic_Ciega_Recepcion_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Get_IdUbic_Ciega_Recepcion_By_IdBodega = 0

        Try

            Return clsLnStock.Get_Primera_Ubic_Recepcion_By_IdBodega(pIdBodega)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, pIdBodega, 0, ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#MA20251410 migracion de xml a json
    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_IdUbicacion_Recepcion_By_IdBodega_Json(ByVal pIdBodega As Integer)

        ' Get_IdUbicacion_Recepcion_By_IdBodega = 0

        Try
            Dim idUbicacion As Integer = clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(pIdBodega)
            Dim result = New With {.ubicacion = idUbicacion}
            Dim strserialize As String = JsonConvert.SerializeObject(result)

            Dim currrentContext As HttpContext = HttpContext.Current
            currrentContext.Response.ContentType = "application/json"
            currrentContext.Response.StatusCode = 200
            currrentContext.Response.Write(strserialize)
            currrentContext.Response.Flush()

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pEsImplosion:=True)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try
    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_IdUbicacion_Recepcion_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Get_IdUbicacion_Recepcion_By_IdBodega = 0

        Try

            Return clsLnBodega.Get_IdUbicacion_Recepcion_By_IdBodega(pIdBodega)

        Catch ex As Exception

            '#MECR01811025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pEsImplosion:=True)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#MA20251010 migracion de xml a json
    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Get_Productos_By_IdUbicacion(ByVal pIdUbicacion As Integer,
                                                 ByVal pIdProductoBodega As Integer) As Object
        Try
            Dim productos As List(Of clsBeVW_stock_res) = clsLnStock.Get_Productos_By_IdUbicacion(pIdUbicacion, pIdProductoBodega)

            ' Pasar a JArray para poder “tocar” propiedades
            Dim arr As JArray = JArray.FromObject(productos)

            For Each producto As JObject In arr.OfType(Of JObject)()
                SerializarJson(producto, "Stock.BePresentacionProductoEnStock.MedidasPorTarima")
                SerializarJson(producto, "Stock.BePresentacionProductoEnStock.RellenadoPorUbicacionDePicking")
            Next

            ' Devuelve JSON “normal” consistente
            Return New With {
                .Error = False,
                .Resultado = arr
            }

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdProductoBodega:=pIdProductoBodega, pEsImplosion:=True)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If
            Return Nothing
        End Try
    End Function

    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Productos_By_IdUbicacion_Existencias_JSON(ByVal pIdUbicacion As Integer,
                                                             ByVal pIdProductoBodega As Integer,
                                                             ByVal pFechaVence As Date,
                                                             ByVal pLote As String,
                                                             ByVal pIdPresentacion As Integer,
                                                             ByVal pLicencia As String)

        ' Get_Productos_By_IdUbicacion_Existencias = Nothing

        Try
            Dim productos As List(Of clsBeVW_stock_res) = clsLnStock.Get_Productos_By_IdUbicacion_Existencia(pIdUbicacion,
                                                                                                             pIdProductoBodega,
                                                                                                             pFechaVence,
                                                                                                             pLote,
                                                                                                             pIdPresentacion,
                                                                                                             pLicencia)

            For Each prod In productos
                ConvertirListasVaciasANothing(prod)
            Next

            Dim json As String = JsonConvert.SerializeObject(productos, New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Include})
            Dim curContext As HttpContext = HttpContext.Current

            curContext.Response.Clear()
            curContext.Response.ContentType = "application/json"
            curContext.Response.Write(json)
            curContext.ApplicationInstance.CompleteRequest()

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try

    End Sub

    '#AT24032023 cambio de anderly
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Productos_By_IdUbicacion_Existencias(ByVal pIdUbicacion As Integer,
                                                         ByVal pIdProductoBodega As Integer,
                                                         ByVal pFechaVence As Date,
                                                         ByVal pLote As String,
                                                         ByVal pIdPresentacion As Integer,
                                                         ByVal pLicencia As String) As List(Of clsBeVW_stock_res)

        Get_Productos_By_IdUbicacion_Existencias = Nothing

        Try

            Return clsLnStock.Get_Productos_By_IdUbicacion_Existencia(pIdUbicacion, pIdProductoBodega, pFechaVence, pLote,
                                                                      pIdPresentacion, pLicencia)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    '#AT20221025 Obtener producto por stockres_ci
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Productos_By_StockResCI(BeStockResCI As clsBeVW_stock_res_CI) As List(Of clsBeVW_stock_res)

        Get_Productos_By_StockResCI = Nothing

        Try

            Return clsLnStock.Get_Productos_By_StockResCI(BeStockResCI)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#MA20251410 migracion de xml a json'
    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Productos_By_IdUbicacion_And_LicPlate_JSON(ByVal pIdUbicacion As Integer,
                                                              ByVal pIdBodega As Integer,
                                                              ByVal pIdProductoBodega As Integer,
                                                              ByVal pLicPlate As String,
                                                              ByVal pIdPresentacion As Integer)



        'Get_Productos_By_IdUbicacion_And_LicPlate = Nothing

        Try
            Dim curContext As HttpContext = HttpContext.Current

            Dim productos As List(Of clsBeVW_stock_res) = clsLnStock.Get_Productos_By_IdUbicacion_And_LicPlate(pIdUbicacion,
                                                                                                               pIdBodega,
                                                                                                               pIdProductoBodega,
                                                                                                               pLicPlate,
                                                                                                               pIdPresentacion)

            For Each prod In productos
                If prod.BePresentacionProductoEnStock IsNot Nothing AndAlso prod.BePresentacionProductoEnStock IsNot Nothing Then
                    If prod.BePresentacionProductoEnStock.MedidasPorTarima IsNot Nothing AndAlso prod.BePresentacionProductoEnStock.MedidasPorTarima.Count = 0 Then
                        prod.BePresentacionProductoEnStock.MedidasPorTarima = Nothing
                    End If
                End If

                If prod.BePresentacionProductoEnStock IsNot Nothing AndAlso prod.BePresentacionProductoEnStock.RellenadoPorUbicacionDePicking IsNot Nothing Then
                    prod.BePresentacionProductoEnStock.RellenadoPorUbicacionDePicking = Nothing
                End If
            Next

            Dim json As String = JsonConvert.SerializeObject(productos, New JsonSerializerSettings With {
                 .NullValueHandling = NullValueHandling.Include
            })

            curContext.Response.Clear()
            curContext.Response.ContentType = "application/json"
            curContext.Response.StatusCode = 200
            curContext.Response.Write(json)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try
    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Productos_By_IdUbicacion_And_LicPlate(ByVal pIdUbicacion As Integer,
                                                              ByVal pIdBodega As Integer,
                                                              ByVal pIdProductoBodega As Integer,
                                                              ByVal pLicPlate As String,
                                                              ByVal pIdPresentacion As Integer
                                                              ) As List(Of clsBeVW_stock_res)

        Get_Productos_By_IdUbicacion_And_LicPlate = Nothing

        Try

            Return clsLnStock.Get_Productos_By_IdUbicacion_And_LicPlate(pIdUbicacion,
                                                                        pIdBodega,
                                                                        pIdProductoBodega,
                                                                        pLicPlate,
                                                                        pIdPresentacion)

        Catch ex As Exception

            '#MECR18112025: Se agrego bitacora de logs para implosion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdBodega:=pIdBodega,
                                                  pIdProductoBodega:=pIdProductoBodega,
                                                  pLic_Plate:=pLicPlate,
                                                  pIdPresentacion:=pIdPresentacion,
                                                  pEsImplosion:=True)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Stock_Por_Producto_Ubicacion(ByVal pidProducto As Integer,
                                                     ByVal pIdUbicacion As Integer,
                                                     ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res)

        Get_Stock_Por_Producto_Ubicacion = Nothing

        Try

            Return clsLnStock.Get_All_By_IdUbicacion(pIdUbicacion,
                                                     pidProducto,
                                                     pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Stock_Por_Producto_Ubicacion_CI(ByVal pidProducto As String,
                                                        ByVal pIdUbicacion As Integer,
                                                        ByVal pIdBodega As Integer,
                                                        ByVal pNombre As String,
                                                        ByVal pDetallado As Boolean) As List(Of clsBeVW_stock_res_CI)

        Get_Stock_Por_Producto_Ubicacion_CI = Nothing

        Try

            Get_Stock_Por_Producto_Ubicacion_CI = clsLnStock_CI.Get_All_By_IdUbicacion(pIdUbicacion,
                                                                                       pidProducto,
                                                                                       pIdBodega,
                                                                                       pNombre,
                                                                                       pDetallado)

            Return Get_Stock_Por_Producto_Ubicacion_CI

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Stock_Por_Pallet(ByVal pLicPlate As String,
                                         ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res)

        Get_Stock_Por_Pallet = Nothing

        Try

            Return clsLnStock.Get_All_By_LP(pLicPlate, pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Stock_Por_Pallet_CI(ByVal pLicPlate As String,
                                             ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res_CI)

        Get_Stock_Por_Pallet_CI = Nothing

        Try

            Return clsLnStock_CI.Get_All_By_LP(pLicPlate, pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Stock_Por_Pallet_By_IdUbicacion_CI(ByVal pLicPlate As String,
                                                           ByVal pIdBodega As Integer,
                                                           ByVal pIdUbicacion As String,
                                                           ByVal pNombre As String,
                                                           ByVal pDetallado As Boolean) As List(Of clsBeVW_stock_res_CI)

        Get_Stock_Por_Pallet_By_IdUbicacion_CI = Nothing

        Try

            Return clsLnStock_CI.Get_All_By_LP_And_IdUbicacion(pIdUbicacion,
                                                               pLicPlate,
                                                               pIdBodega,
                                                               pNombre,
                                                               pDetallado)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Stock_Especifico_HH(ByVal IdBodega As Integer,
                                                  ByVal IdPedidoEnc As Integer,
                                                  ByVal pStockRes As clsBeStock_res) As DataTable

        Get_All_Stock_Especifico_HH = Nothing

        Try

            Return clsLnStock.Get_All_Stock_Especifico_HH(IdBodega, IdPedidoEnc, pStockRes)

        Catch ex As Exception

            '#MECR21102025: Se agrego bitacora de logs para pedidos
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdBodega:=IdBodega, pIdPedidoEnc:=IdPedidoEnc, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Stock_Especifico_By_IdPedidoDet(ByVal pBeStockRes As clsBeStock_res,
                                                            ByVal IdPedidoEnc As Integer,
                                                            ByVal gIdBodega As Integer) As DataTable

        Get_All_Stock_Especifico_By_IdPedidoDet = Nothing
        Dim vPasos As Integer

        Try

            Return clsLnStock.Get_All_Stock_Especifico_By_IdPedidoDet(pBeStockRes,
                                                                      IdPedidoEnc,
                                                                      gIdBodega)

        Catch ex As Exception

            '#MECR21102025: Se agrego bitacora de logs para pedidos
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("Pasos{0} {1} {2}", vPasos, MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdBodega:=gIdBodega, pIdPedidoEnc:=IdPedidoEnc, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = vMsgError
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function


    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Stock_Res_By_IdPedidoDet(ByVal pIdPedidoDet As Integer, ByVal IdPedidoEnc As Integer) As List(Of clsBeStock_res)

        Get_All_Stock_Res_By_IdPedidoDet = Nothing

        Try

            Return clsLnStock_res.Get_All_Stock_Res_By_IdPedidoDet(pIdPedidoDet, IdPedidoEnc)

        Catch ex As Exception

            '#MECR21102025: Se agrego bitacora de logs para pedidos
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdPedidoEnc:=IdPedidoEnc, pIdPedidoDet:=pIdPedidoDet, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function loginHandHeld(ByVal idHandHeld As String, nombreHanHeld As String) As Integer

        loginHandHeld = 0

        Try

            Return clsLnLicencia_item.Valida_HandHeld(idHandHeld, nombreHanHeld)

        Catch ex As Exception
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            Dim currrentContext As HttpContext = HttpContext.Current
            Dim DT As New DataTable("CustomError")
            DT.Columns.Add("Error", GetType(String))
            DT.Rows.Add(Mensaje)
            Dim sw As New StringWriter()
            DT.WriteXml(sw)
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.StatusCode = 299
            HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
            HttpContext.Current.Response.Output.Write(sw.ToString())
            HttpContext.Current.Response.ContentType = "text/xml"
            HttpContext.Current.Response.End()
        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Sub cancelaConexion(mac As String)
        Try
            clsLnLicencia_item.Registra_Conexion(mac)
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

    ' Inventario Inicial
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Inventario_By_IdBodega_And_IdOperador(ByVal pIdBodega As Integer, ByVal pIdOperador As Integer, pIdTarea As Integer) As List(Of clsBeTrans_inv_enc)

        Get_All_Inventario_By_IdBodega_And_IdOperador = Nothing

        Try

            Get_All_Inventario_By_IdBodega_And_IdOperador = clsLnTrans_inv_enc.Get_All_Pendientes_By_IdBodega_And_IdOperador(pIdBodega, pIdOperador, pIdTarea)

            Return Get_All_Inventario_By_IdBodega_And_IdOperador
        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Inventario_Tramos_By_IdTarea(pIdTarea As Integer) As List(Of clsBeTrans_inv_tramo)

        Get_All_Inventario_Tramos_By_IdTarea = Nothing

        Try

            Return clsLnTrans_inv_tramo.Get_All_Tramos_By_IdTarea(pIdTarea)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Sub InventarioInicialVerAgrega(ByVal pItem As clsBeTrans_inv_resumen)

        Try

            '#eEJC20220507: Prevenir que se inserten ubicaciones o tramos en 0
            If pItem.IdUbicacion = 0 Then
                Throw New Exception("#ERR_20220507_1216: La ubicación no se definió en la captura")
            End If

            If pItem.Idtramo = 0 Then
                Throw New Exception("#ERR_20220507_1216A: El tramo no se definió en la captura")
            End If

            pItem.Fecha_captura = Date.Now
            clsLnTrans_inv_resumen.Insertar(pItem)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try
    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Inventario_Inicial_By_IdInventario_Enc_And_Idtramo_And_IdProducto(ByVal pIdinventarioenct As Integer, pIdtramo As Integer, pIdProducto As Integer, pIdUbicacion As Integer, pIdBodega As Integer) As List(Of clsBeTrans_inv_resumen_grid)

        Get_All_Inventario_Inicial_By_IdInventario_Enc_And_Idtramo_And_IdProducto = Nothing

        Try
            Return clsLnTrans_inv_resumen_grid.Get_All_Inventario_Inicial_By_IdInventario_Enc_And_Idtramo_And_IdProducto(pIdinventarioenct, pIdtramo, pIdProducto, pIdUbicacion, pIdBodega)
        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Lista_Conteo_Inventario_Inicial_By_IdInventarioEnc(ByVal pIdInventarioEnc As Integer,
                                                    ByVal pIdtramo As Integer,
                                                    ByVal pIdProducto As Integer,
                                                    ByVal pIdUbic As Integer) As List(Of clsBeTrans_inv_detalle_grid)

        Get_Lista_Conteo_Inventario_Inicial_By_IdInventarioEnc = Nothing

        Try

            Return clsLnTrans_inv_detalle_grid.Get_Lista_Conteo_Inventario_Inicial_By_IdInventarioEnc(pIdInventarioEnc,
                                                                                                          pIdtramo,
                                                                                                          pIdProducto,
                                                                                                          pIdUbic)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function InventarioInicialVerGet(ByVal pIdinventariores As Integer) As clsBeTrans_inv_resumen

        InventarioInicialVerGet = Nothing

        Dim item As New clsBeTrans_inv_resumen

        Try
            item.Idinventariores = pIdinventariores
            clsLnTrans_inv_resumen.GetSingle(item)
            Return item
        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function InventarioInicialDetGet(pIdinventariodet As Integer) As clsBeTrans_inv_detalle

        InventarioInicialDetGet = Nothing
        Dim item As New clsBeTrans_inv_detalle

        Try
            item.Idinventariodet = pIdinventariodet
            clsLnTrans_inv_detalle.GetSingle(item)
            Return item
        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Sub InventarioInicialVerActualizar(pItem As clsBeTrans_inv_resumen)

        Try
            clsLnTrans_inv_resumen.Actualizar(pItem)
        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Sub InventarioInicialDetActualizar(pItem As clsBeTrans_inv_detalle)

        Try

            If pItem.IdBodega = 0 OrElse pItem.Nom_producto = "" OrElse pItem.IdPropietarioBodega = 0 Then

                Throw New Exception("ERR_20220507: El formato del conteo no es válido.")

            Else

                clsLnTrans_inv_detalle.Actualizar(pItem)
                clsLnTrans_inv_detalle.Eliminar_Registros_Cantidad_0()

            End If


        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Sub InventarioInicialVerEliminar(pIdinventariores As Integer)

        Dim item As New clsBeTrans_inv_resumen

        Try
            item.Idinventariores = pIdinventariores
            clsLnTrans_inv_resumen.Eliminar(item)
        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Sub InventarioInicialDetEliminar(pIdinventariodet As Integer)

        Dim item As New clsBeTrans_inv_detalle

        Try
            item.Idinventariodet = pIdinventariodet
            clsLnTrans_inv_detalle.Eliminar(item)
        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_BeProducto_By_IdProducto(ByVal pIdProducto As Integer) As clsBeProducto

        Get_BeProducto_By_IdProducto = Nothing

        Dim item As New clsBeProducto

        Try

            item.IdProducto = pIdProducto
            clsLnProducto.GetSingle(item)
            Return item

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Sub Actualizar_Inventario_Inicial_By_BeTransInvTramo(ByVal pTramo As clsBeTrans_inv_tramo)

        Dim item As New clsBeTrans_inv_resumen
        Dim inv As New clsBeTrans_inv_enc
        Dim fecha As DateTime

        Try

            clsLnTrans_inv_tramo.Actualizar_Tramo(pTramo)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

        Try

            fecha = clsLnTrans_inv_tramo.Get_Fecha_Inicio_By_IdInventarioEnc(pTramo.Idinventario)
            inv.Idinventarioenc = pTramo.Idinventario

            clsLnTrans_inv_enc.GetSingle(inv)
            inv.Hora_ini = fecha

            clsLnTrans_inv_enc.Actualizar(inv)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Inventario_Inicial_By_IdInventarioEnc_And_IdTramo(ByVal pidinventario As Integer, ByVal pidtramo As Integer) As clsBeTrans_inv_tramo

        Dim pTramo As New clsBeTrans_inv_tramo

        Get_Inventario_Inicial_By_IdInventarioEnc_And_IdTramo = Nothing

        Try

            pTramo.Idinventario = pidinventario
            pTramo.Idtramo = pidtramo
            clsLnTrans_inv_tramo.Get_Single_By_BeTramo(pTramo)
            Return pTramo

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function UbicacionVecina(idubicacion As Integer, letra As String) As Integer

        Dim oBeBodega_ubicacion As New clsBeBodega_ubicacion

        UbicacionVecina = Nothing

        Try

            oBeBodega_ubicacion.IdUbicacion = idubicacion
            clsLnBodega_ubicacion.GetSingle(oBeBodega_ubicacion)

            Return clsLnBodega_ubicacion.UbicacionVecina(oBeBodega_ubicacion, letra)

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    'Agregar_Inventario_Inicial
    <WebMethod(), SoapHeader("mArch")>
    Public Sub Agregar_Inventario_Inicial(ByVal pItem As clsBeTrans_inv_detalle)

        Try

            If pItem.IdBodega = 0 OrElse pItem.Nom_producto = "" OrElse pItem.IdPropietarioBodega = 0 Then
                Throw New Exception("ERR_20220507: El formato del conteo no es válido.")
            Else
                clsLnTrans_inv_detalle.InsertarSinID(pItem)
            End If


        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Sub InventarioInicialAgregaMulti(pItem As clsBeTrans_inv_detalle, ubic_a As Integer, ubic_b As Integer, ubic_c As Integer, ubic_d As Integer)

        Dim pI1, pI2, pI3, pI4 As New clsBeTrans_inv_detalle

        Try

            pI1 = pItem.Clone : pI1.IdUbicacion = ubic_a
            pI2 = pItem.Clone : pI2.IdUbicacion = ubic_b
            pI3 = pItem.Clone : pI3.IdUbicacion = ubic_c
            pI4 = pItem.Clone : pI4.IdUbicacion = ubic_d

            clsLnTrans_inv_detalle.InsertaMulti(pI1, pI2, pI3, pI4)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try
    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Inventario_Inicial_Acepta_Mal_Estado_By_IdUbicacion(ByVal pIdUbicacion As Integer, pEstMalo As Integer) As Boolean

        Inventario_Inicial_Acepta_Mal_Estado_By_IdUbicacion = False

        Try

            Return clsLnProducto_estado_ubic.AceptaEstadoMalo(pIdUbicacion, pEstMalo)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try


    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function testWS(valor As Integer) As Integer

        'Dim srv As New wsPedidoCliente.PedidoCliente
        'srv.Generar_Ingreso_Por_Anulacion_NC_SAP(88, True, "29919", True, True, True)

        Return valor

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function nombreServidorLicencias() As String

        nombreServidorLicencias = ""

        Try


            Dim BeLicItem As New clsBeLicencia_item

            Dim vNombreServerLicencias As String = "."

            If clsLnLicencia_item.Existe_Servidor_De_Licencias(BeLicItem) Then
                vNombreServerLicencias = " : " & BeLicItem.Identificacion
            End If

            Return vNombreServerLicencias

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function validaUbicacionPalet(ByVal pidinvenc As Integer,
                                         ByVal pidubicacion As Integer,
                                         ByVal pidpresentacion As Integer) As Integer

        validaUbicacionPalet = 0

        Dim ubic As New clsBeBodega_ubicacion
        Dim pres As New clsBeProducto_Presentacion
        Dim items As New List(Of clsBeTrans_inv_detalle)

        Try

            ubic.IdUbicacion = pidubicacion
            clsLnBodega_ubicacion.GetSingle(ubic)

            If IsNothing(ubic) Then Return 0

            Dim uvol As Double = ubic.Alto * ubic.Ancho * ubic.Largo
            If uvol <= 0 Then Return -1

            pres.IdPresentacion = pidpresentacion
            clsLnProducto_presentacion.GetSingle(pres)
            If Not pres.EsPallet Then Return -1
            'Dim pvol As Double = pres.Alto * pres.Ancho * pres.Largo
            'If pvol <= 0 Then Return -1

            items = clsLnTrans_inv_detalle.Get_All_By_InvEncUbic(pidinvenc, pidubicacion)

            If IsNothing(items) Then Return 0

            For Each item As clsBeTrans_inv_detalle In items
                pidpresentacion = item.IdPresentacion
                If pidpresentacion > 0 Then
                    pres.IdPresentacion = pidpresentacion
                    clsLnProducto_presentacion.GetSingle(pres)
                    If pres.EsPallet Then Return 1
                End If
            Next

            Return 0
        Catch ex As Exception
            '#MECR04112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function validaUbicacionPaletOld(cant As Integer, pidubicacion As String, pidpresentacion As Integer) As Integer

        validaUbicacionPaletOld = 0

        Dim ubic As New clsBeBodega_ubicacion
        Dim pres As New clsBeProducto_Presentacion
        Dim items As New List(Of clsBeTrans_inv_detalle)

        Try
            ubic.IdUbicacion = pidubicacion
            clsLnBodega_ubicacion.GetSingle(ubic)
            Dim uvol As Double = ubic.Alto * ubic.Ancho * ubic.Largo
            If uvol <= 0 Then Return -1

            pres.IdPresentacion = pidpresentacion
            clsLnProducto_presentacion.GetSingle(pres)
            If Not pres.EsPallet Then Return -1

            If cant > 1 Then Return 1

            Return 0

        Catch ex As Exception
            '#MECR04112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdUbicacionOrigen:=pidubicacion, pIdPresentacion:=pidpresentacion)

            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    ' Inventario Ciclico
    <WebMethod(), SoapHeader("mArch")>
    Public Function Inventario_Ciclico_ReConteos(ByVal pIdenc As Integer) As List(Of clsBeTrans_inv_enc_reconteo)

        Inventario_Ciclico_ReConteos = Nothing

        Try
            Return clsLnTrans_inv_enc_reconteo.Lista_Reconteos_Activos(pIdenc)
        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function InventarioCiclicoListaConteo(pidinventarioenc As Integer, pidoperador As Integer, pPendientes As Boolean) As List(Of clsBeTrans_inv_ciclico_vw)

        InventarioCiclicoListaConteo = Nothing

        Try
            Return clsLnTrans_inv_ciclico_vw.GetAllByOperador(pidinventarioenc, pidoperador, pPendientes)
        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Inventario_Ciclico_Listar_Conteo(ByVal pIdInventarioEnc As Integer, pIdOperador As Integer, pPendientes As Boolean) As DataTable

        Inventario_Ciclico_Listar_Conteo = Nothing

        Try

            Return clsLnTrans_inv_ciclico_vw.Get_All_By_IdInventarioEnc_And_IdOperador(pIdInventarioEnc, pIdOperador, pPendientes)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    'AT20240827 Get cantidad contada
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Conteo_Inv_Ciclico(ByVal pInvCiclico As clsBeTrans_inv_ciclico) As Double

        Get_Conteo_Inv_Ciclico = 0

        Try
            Return clsLnTrans_inv_ciclico.Get_Total_InvCiclico(pInvCiclico)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#AT20241212 Obtener inventario congelado - inv ciclico
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Stock_Congelado(ByVal pInvCiclico As clsBeTrans_inv_ciclico) As clsBeTrans_inv_ciclico

        Get_Stock_Congelado = Nothing

        Try
            Return clsLnTrans_inv_stock.Get_Stock_Congelado_InvCiclico(pInvCiclico)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function InventarioCiclicoPaletConteo(pidinventarioenc As Integer, pidoperador As Integer, pPendientes As Boolean, pLicPlate As String) As List(Of clsBeTrans_inv_ciclico_vw)

        InventarioCiclicoPaletConteo = Nothing

        Try

            Return clsLnTrans_inv_ciclico_vw.GetPaletByOperador(pidinventarioenc, pidoperador, pPendientes, pLicPlate)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_IdProductoBodega_By_IdProducto_And_IdBodega(ByVal pIdProducto As Integer, pIdBodega As Integer) As Integer

        Get_IdProductoBodega_By_IdProducto_And_IdBodega = 0

        Try
            Return clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(pIdProducto, pIdBodega)
        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Inventario_Ciclico_Actualiza_Conteo(ByVal pitem As clsBeTrans_inv_ciclico_vw,
                                                            ByRef Resultado As String) As Integer

        Inventario_Ciclico_Actualiza_Conteo = 0

        Try


            Dim Res As String = ""

            Dim thread As New Thread(
                  Sub()
                      clsLnTrans_inv_ciclico.Actualiza_Conteo_Ciclico_HH(pitem, Res)
                  End Sub
                )

            thread.Start()

            Return 1

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#AT20241205 Nueva Funcion para manejar el conteo del inventario ciclico
    <WebMethod(), SoapHeader("mArch")>
    Public Function Inventario_Ciclico_Act_Conteo_Andr(ByVal pitem As clsBeTrans_inv_ciclico,
                                                       ByVal pReconteo As Integer,
                                                       ByVal esOriginal As Boolean,
                                                       ByRef Resultado As String,
                                                       ByVal CrearTallaColor As Boolean,
                                                       ByVal ProductoTallaColor As clsBeProducto_talla_color) As Integer

        Inventario_Ciclico_Act_Conteo_Andr = 0

        Try
            Dim Res As String = ""
            Return clsLnTrans_inv_ciclico.Actualiza_Conteo_Ciclico_HH(pitem,
                                                                      pReconteo,
                                                                      esOriginal,
                                                                      Res,
                                                                      CrearTallaColor,
                                                                      ProductoTallaColor)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    <WebMethod(), SoapHeader("mArch")>
    Public Function Inventario_Ciclico_Actualiza_Reconteo(ByVal idinvreconteo As Integer, pCantidad_Reconteo As Decimal) As Integer

        Inventario_Ciclico_Actualiza_Reconteo = 0
        'texto

        Try

            Return clsLnTrans_inv_ciclico.Actualiza_Inventario_Ciclico_Reconteo(idinvreconteo, pCantidad_Reconteo)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Inventario_Agregar_Conteo(ByVal pBeTransInvCiclico As clsBeTrans_inv_ciclico, pIdResolucion As Integer) As Integer

        Inventario_Agregar_Conteo = 0

        Try

            Return clsLnTrans_inv_ciclico.Agregar_Conteo(pBeTransInvCiclico, pIdResolucion)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Nuevo_Correlativo_LicensePlate(ByVal pIdEmpresa As Integer,
                                                       ByVal pIdBodega As Integer,
                                                       ByVal pIdPropietario As Integer,
                                                       ByVal pIdProducto As Integer) As String

        Get_Nuevo_Correlativo_LicensePlate = ""
        Try

            Return clsLnStock_rec.Get_Nuevo_Correlativo_LicensePlate(pIdEmpresa,
                                                                     pIdBodega,
                                                                     pIdPropietario,
                                                                     pIdProducto)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Nuevo_Correlativo_LicensePlate_S(ByVal pIdEmpresa As Integer,
                                                         ByVal pIdBodega As Integer,
                                                         ByVal pIdPropietario As Integer,
                                                         ByVal pIdProducto As Integer,
                                                         ByVal UltimoPalletGenerado As String) As String

        Get_Nuevo_Correlativo_LicensePlate_S = ""

        Try

            Return clsLnStock_rec.Get_Nuevo_Correlativo_LicensePlate(pIdEmpresa,
                                                                     pIdBodega,
                                                                     pIdPropietario,
                                                                     pIdProducto,
                                                                     UltimoPalletGenerado)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function GetSingleProductoTarima(ByVal pIdPresentacion As Integer) As clsBeProducto_presentacion_tarima

        GetSingleProductoTarima = Nothing

        Try

            Return clsLnProducto_presentacion_tarima.GetSingle(pIdPresentacion)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Licenses_Plates_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeLicensePlates)

        Get_Licenses_Plates_By_IdRecepcionEnc = Nothing

        Try

            Return clsLnLicensePlates.Get_Licenses_Plates_By_IdRecepcionEnc(pIdRecepcionEnc)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Existe_LP_By_IdRecepcionEnc_And_IdRecepcionDet(ByVal IdRecepcionEnc As Integer,
                                                                   ByVal LicPlate As String,
                                                                   ByVal IdRecepcionDet As Integer) As Boolean

        Existe_LP_By_IdRecepcionEnc_And_IdRecepcionDet = False

        Try

            Return clsLnTrans_re_det.ExisteLP(IdRecepcionEnc,
                                              LicPlate,
                                              IdRecepcionDet)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, IdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Finalizar_Recepcion(ByVal pRecEnc As clsBeTrans_re_enc,
                                        ByVal backOrder As Boolean,
                                        ByVal pIdOrdenCompraEnc As Integer,
                                        ByVal pIdRecepcionEnc As Integer,
                                        ByVal pIdEmpresa As Integer,
                                        ByVal pIdBodega As Integer,
                                        ByVal pIdUsuario As String,
                                        ByVal pListObjDetR As List(Of clsBeTrans_re_det),
                                        ByVal pHabilitarStock As Boolean) As String

        Finalizar_Recepcion = ""

        Try

            clsLnTrans_re_enc.Finalizar_Recepcion(pRecEnc,
                                                  backOrder,
                                                  pIdOrdenCompraEnc,
                                                  pIdRecepcionEnc,
                                                  pIdEmpresa,
                                                  pIdBodega,
                                                  pIdUsuario,
                                                  pListObjDetR,
                                                  pHabilitarStock)

            '#EJC20220224_0326AM: Identificar si se debe registrar en NAV el documento para BYB.
            Dim BeTransOCEnc As New clsBeTrans_oc_enc
            BeTransOCEnc = clsLnTrans_oc_enc.Get_Single_By_IdOrdenCompraEnc_And_IdBodega(pIdOrdenCompraEnc,
                                                                                         pIdBodega)

            If Not BeTransOCEnc Is Nothing Then

                If BeTransOCEnc.Push_To_NAV Then

                    If (Not BeTransOCEnc.No_Documento_Recepcion_ERP = "" AndAlso Not BeTransOCEnc.Referencia = "") Then

                        Dim vResultPutAway As Boolean = Registrar_Pedido_Compra_To_NAV_For_BYB(BeTransOCEnc.IdOrdenCompraEnc,
                                                                                               BeTransOCEnc.Referencia,
                                                                                               BeTransOCEnc.No_Documento_Recepcion_ERP)

                        If Not vResultPutAway Then

                            Throw New Exception("Error NAV - No se pudo realizar el registro.")

                        End If

                    End If

                End If

            End If

            Finalizar_Recepcion = "Ok"

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdEmpresa, pIdBodega, pIdUsuario, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Sub Finalizar_Recepcion_Parcial(ByVal pRecEnc As clsBeTrans_re_enc,
                                           ByVal pIdOrdenCompraEnc As Integer,
                                           ByVal pIdRecepcionEnc As Integer,
                                           ByVal pIdEmpresa As Integer,
                                           ByVal pIdBodega As Integer,
                                           ByVal pIdUsuario As String,
                                           ByVal pBeStockRec As clsBeStock_rec)

        Try

            clsLnTrans_re_enc.Finalizar_Recepcion_Parcial(pRecEnc,
                                                          pIdOrdenCompraEnc,
                                                          pIdRecepcionEnc,
                                                          pIdEmpresa,
                                                          pIdBodega,
                                                          pIdUsuario,
                                                          pBeStockRec)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdEmpresa, pIdBodega, pIdUsuario, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Sub Finalizar_Recepcion_Parcial_Pallet_Proveedor(ByVal pRecEnc As clsBeTrans_re_enc,
                                                            ByVal pIdOrdenCompraEnc As Integer,
                                                            ByVal pIdRecepcionEnc As Integer,
                                                            ByVal pIdEmpresa As Integer,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pIdUsuario As String,
                                                            ByVal pBeStockRec As clsBeStock_rec,
                                                            ByVal pBeRecDet As clsBeTrans_re_det,
                                                            ByVal pBeBarraPallet As clsBeI_nav_barras_pallet,
                                                            ByVal pEsTransferencia As Boolean)

        Try

            clsLnTrans_re_enc.Finalizar_Recepcion_Parcial_Pallet_Proveedor(pRecEnc,
                                                                           pIdOrdenCompraEnc,
                                                                           pIdRecepcionEnc,
                                                                           pIdEmpresa,
                                                                           pIdBodega,
                                                                           pIdUsuario,
                                                                           pBeStockRec,
                                                                           pBeRecDet,
                                                                           pBeBarraPallet,
                                                                           pEsTransferencia)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdEmpresa, pIdBodega, pIdUsuario, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Picking_By_IdPickingEnc(ByVal pIdPickingEnc As Integer,
                                                ByVal pIdOperadorBodega As Integer) As clsBeTrans_picking_enc

        Get_Picking_By_IdPickingEnc = Nothing

        Try

            Return clsLnTrans_picking_enc.Get_Single_By_IdPickingEnc_For_HH_Reducido(pIdPickingEnc, pIdOperadorBodega)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function


    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Tipo_Etiqueta_By_IdTipoEtiqueta(ByRef pBeTipo_etiqueta As clsBeTipo_etiqueta,
                                                        ByVal IdSimbologia As Integer)

        Get_Tipo_Etiqueta_By_IdTipoEtiqueta = Nothing

        Try

            '#GT27112023: cambie el GetSingle por Getsingle_By_Simbologia para adecuar zpl con barra lineal, QR...
            clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(pBeTipo_etiqueta, IdSimbologia)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Single_StockRes(ByRef pBeStock_res As clsBeStock_res) As Boolean

        Get_Single_StockRes = False

        Try

            If clsLnStock_res.GetSingle(pBeStock_res) Then
                Get_Single_StockRes = True
            Else
                pBeStock_res = Nothing
            End If

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Single_Trans_Picking_Ubic(ByVal pIdPickingUbic As Integer) As clsBeTrans_picking_ubic

        Get_Single_Trans_Picking_Ubic = Nothing

        Try

            Dim pBeTransPickingUbic As New clsBeTrans_picking_ubic()
            pBeTransPickingUbic.IdPickingUbic = pIdPickingUbic

            If clsLnTrans_picking_ubic.GetSingle(pBeTransPickingUbic) Then
                Return pBeTransPickingUbic
            Else
                Get_Single_Trans_Picking_Ubic = Nothing
            End If

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingUbic:=pIdPickingUbic)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKFK20230218 beTransPickingDet se lo quité de los parámetros y le agregué la cantidad
    '#AT20241014 Agregue Host a los parametros para el id del dispositivo desde la hh
    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_Picking(ByVal oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                       ByVal BeStockRes As clsBeStock_res,
                                       ByVal IdBodega As Integer,
                                       ByVal pCantidad As Double,
                                       ByVal host As String) As String

        Actualizar_Picking = False

        Try

            Dim vMsg1 As String = "ID_Picking: " & oBeTrans_picking_ubic.IdPickingUbic.ToString()
            vMsg1 += ", ID_StockRes: " & BeStockRes.IdStockRes.ToString

            '#EJC20200125: Se sobreescribe el valor que envía la HH por si la HH tiene fecha diferente....
            oBeTrans_picking_ubic.Fecha_picking = Now

            Dim vResult As String = clsLnTrans_picking_ubic.Actualizar_Picking(oBeTrans_picking_ubic,
                                                                               BeStockRes,
                                                                               IdBodega,
                                                                               pCantidad,
                                                                               host)

            Return String.Format("{0} Res:{1}", vMsg1, vResult)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Guardar_Picking_Cm(ByVal pListaPicking As List(Of clsBeTrans_picking_ubic),
                                       ByVal IdBodega As Integer,
                                       ByVal pIdOperador As Integer,
                                       ByVal host As String, TipoLista As Integer) As String

        Guardar_Picking_Cm = False

        Dim vResult As String = "Inicio picking"
        Dim oBeTrans_picking_ubic As clsBeTrans_picking_ubic = Nothing
        Dim vIdPickingEnc As Integer = If(pListaPicking IsNot Nothing AndAlso pListaPicking.Count > 0, pListaPicking(0).IdPickingEnc, 0)
        Dim vEsPrimerPickeo As Boolean = (vIdPickingEnc > 0 AndAlso Not Tiene_Avance_Picking(vIdPickingEnc))

        Try

            If TipoLista = 2 Then

                For Each ObjUbic As clsBeTrans_picking_ubic In pListaPicking

                    oBeTrans_picking_ubic = ObjUbic

                    Dim BeStockRes = clsLnStock_res.GetSingle_By_IdStockRes(IdBodega, ObjUbic.IdStockRes)
                    ObjUbic.Cantidad_Recibida = ObjUbic.Cantidad_Solicitada
                    ObjUbic.IdOperadorBodega_Pickeo = pIdOperador
                    ObjUbic.Acepto = True
                    ObjUbic.Encontrado = True
                    ObjUbic.Fecha_picking = Now
                    ObjUbic.Fec_mod = Now

                    clsLnTrans_picking_ubic.Actualizar_Picking(ObjUbic,
                                                               BeStockRes,
                                                               IdBodega,
                                                               ObjUbic.Cantidad_Solicitada,
                                                               host)
                Next

                vResult = " terminó la actualizacion"
            Else

                oBeTrans_picking_ubic = pListaPicking(0)

                clsLnTrans_picking_ubic.Actualiza_Picking_Consolidado_Cm(pListaPicking(0),
                                                                        pIdOperador,
                                                                        host)

                vResult = " terminó la actualizacion"
            End If

            If vEsPrimerPickeo Then
                '#EJCCKF20260519_Notificar_SAP_Hana_MAMPA: Estado 3 = Pickeando al registrar el primer pickeo real desde HH.
                SapServiceLayerClient.Notificar_Estado_SAP_Hana_MAMPA_By_Picking(vIdPickingEnc, 3, 3, 1,
                                                                                 pIdOperador.ToString(), IdBodega)
            End If

            Return String.Format("Res:{0}", vResult)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdBodega:=IdBodega,
                                                  pIdPickingEnc:=oBeTrans_picking_ubic.IdPickingEnc,
                                                  pIdPickingDet:=oBeTrans_picking_ubic.IdPickingDet,
                                                  pIdPedidoEnc:=oBeTrans_picking_ubic.IdPedidoEnc,
                                                  pIdPedidoDet:=oBeTrans_picking_ubic.IdPedidoDet,
                                                  pIdPickingUbic:=oBeTrans_picking_ubic.IdPickingUbic)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function


    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_Picking_Con_Reemplazo_De_Pallet(ByVal oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                                              ByVal BeStockRes As clsBeStock_res,
                                                              ByVal oBeTrans_picking_det As clsBeTrans_picking_det,
                                                              ByVal IdBodega As Integer,
                                                              ByVal pBeStockPalletReemplazo As clsBeProducto) As String


        Actualizar_Picking_Con_Reemplazo_De_Pallet = ""

        Try

            Dim vEsPrimerPickeo As Boolean = (oBeTrans_picking_ubic.IdPickingEnc > 0 AndAlso Not Tiene_Avance_Picking(oBeTrans_picking_ubic.IdPickingEnc))
            Dim vMsg1 As String = "ID_Picking: " & oBeTrans_picking_ubic.IdPickingUbic.ToString
            vMsg1 += ", ID_StockRes: " & BeStockRes.IdStockRes.ToString

            '#EJC20200125: Se sobreescribe el valor que envía la HH por si la HH tiene fecha diferente....
            oBeTrans_picking_ubic.Fecha_picking = Now

            Dim vResult As String = clsLnTrans_picking_ubic.Actualizar_Picking(oBeTrans_picking_ubic,
                                                                               BeStockRes,
                                                                               oBeTrans_picking_det,
                                                                               IdBodega,
                                                                               pBeStockPalletReemplazo)

            If vEsPrimerPickeo Then
                '#EJCCKF20260519_Notificar_SAP_Hana_MAMPA: Estado 3 = Pickeando también aplica cuando el primer avance entra por reemplazo de pallet.
                SapServiceLayerClient.Notificar_Estado_SAP_Hana_MAMPA_By_Picking(oBeTrans_picking_ubic.IdPickingEnc, 3, 3, 1, oBeTrans_picking_ubic.IdOperadorBodega_Pickeo.ToString(), IdBodega)
            End If

            Return String.Format("{0} Res:{1}", vMsg1, vResult)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdBodega:=IdBodega,
                                                  pIdPickingEnc:=oBeTrans_picking_ubic.IdPickingEnc,
                                                  pIdPickingDet:=oBeTrans_picking_ubic.IdPickingDet,
                                                  pIdPedidoEnc:=oBeTrans_picking_ubic.IdPedidoEnc,
                                                  pIdPedidoDet:=oBeTrans_picking_ubic.IdPedidoDet,
                                                  pIdPickingUbic:=oBeTrans_picking_ubic.IdPickingUbic)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_PickingUbic_Por_Verificacion(ByVal oBeTrans_picking_ubic As clsBeTrans_picking_ubic,
                                                            ByVal BeStockRes As clsBeStock_res) As String

        Actualizar_PickingUbic_Por_Verificacion = ""

        Try

            Dim vEsPrimeraVerificacion As Boolean = (oBeTrans_picking_ubic.IdPickingEnc > 0 AndAlso Not Tiene_Avance_Verificacion(oBeTrans_picking_ubic.IdPickingEnc))
            Dim vMsg1 As String = "ID_Picking: " & oBeTrans_picking_ubic.IdPickingUbic.ToString
            vMsg1 += ", ID_StockRes: " & BeStockRes.IdStockRes.ToString

            '#EJC20200125: Se sobreescribe el valor que envía la HH por si la HH tiene fecha diferente....
            oBeTrans_picking_ubic.Fecha_verificado = Now

            Dim vResult As String = clsLnTrans_picking_ubic.Actualizar_PickingUbic_Por_Verificacion(oBeTrans_picking_ubic, BeStockRes)

            If vEsPrimeraVerificacion Then
                '#EJCCKF20260519_Notificar_SAP_Hana_MAMPA: Estado 5 = Verificando al registrar la primera verificación real desde HH.
                SapServiceLayerClient.Notificar_Estado_SAP_Hana_MAMPA_By_Picking(oBeTrans_picking_ubic.IdPickingEnc, 5, 5, 1, oBeTrans_picking_ubic.IdOperadorBodega_Verifico.ToString(), BeStockRes.IdBodega)
            End If

            Return String.Format("{0} Res:{1}", vMsg1, vResult)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdPickingEnc:=oBeTrans_picking_ubic.IdPickingEnc,
                                                  pIdPickingDet:=oBeTrans_picking_ubic.IdPickingDet,
                                                  pIdPedidoEnc:=oBeTrans_picking_ubic.IdPedidoEnc,
                                                  pIdPedidoDet:=oBeTrans_picking_ubic.IdPedidoDet,
                                                  pIdPickingUbic:=oBeTrans_picking_ubic.IdPickingUbic)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function ObtenerPickingDet(ByRef oBeTrans_picking_det As clsBeTrans_picking_det) As Boolean

        ObtenerPickingDet = False

        Try

            Return clsLnTrans_picking_det.Obtener(oBeTrans_picking_det)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdPickingEnc:=oBeTrans_picking_det.IdPickingEnc,
                                                  pIdPickingDet:=oBeTrans_picking_det.IdPickingDet,
                                                  pIdPedidoEnc:=oBeTrans_picking_det.IdPedidoEnc,
                                                  pIdPedidoDet:=oBeTrans_picking_det.IdPedidoDet)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_PickingEnc_Procesado(ByVal oBeTrans_picking_enc As clsBeTrans_picking_enc) As Integer

        Actualizar_PickingEnc_Procesado = 0

        Try

            Return clsLnTrans_picking_enc.Actualizar_PickingEnc_Procesado(oBeTrans_picking_enc)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdPickingEnc:=oBeTrans_picking_enc.IdPickingEnc,
                                                  pIdPedidoEnc:=oBeTrans_picking_enc.IdPedidoEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_PickingEnc_Procesado_Andr(ByVal pIdPickingEnc As Integer,
                                                         ByVal pIdOperadorBodegaCerro As Integer,
                                                         ByVal pHostCerro As String) As Integer

        Actualizar_PickingEnc_Procesado_Andr = 0

        Try

            Dim vResult As Integer = clsLnTrans_picking_enc.Actualizar_PickingEnc_Procesado_Andr(pIdPickingEnc,
                                                                                                  pIdOperadorBodegaCerro,
                                                                                                  pHostCerro)

            If vResult > 0 Then
                '#EJCCKF20260519_Notificar_SAP_Hana_MAMPA: Estado 4 = Pickeado. Si la verificación es automática, se avanza también a estado 6 = Verificado.
                SapServiceLayerClient.Notificar_Estado_SAP_Hana_MAMPA_By_Picking(pIdPickingEnc, 4, 4, 1, pIdOperadorBodegaCerro.ToString())

                If Get_Verificacion_Automatica(pIdPickingEnc) Then
                    SapServiceLayerClient.Notificar_Estado_SAP_Hana_MAMPA_By_Picking(pIdPickingEnc, 6, 6, 1, pIdOperadorBodegaCerro.ToString())
                End If
            End If

            Return vResult

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_PickingEnc_Verificado(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc) As Integer

        Actualizar_PickingEnc_Verificado = 0

        Try

            Dim vResult As Integer = clsLnTrans_picking_enc.Actualizar_PickingEnc_Verificado(oBeTrans_picking_enc)

            If vResult > 0 Then
                '#EJCCKF20260519_Notificar_SAP_Hana_MAMPA: Estado 6 = Verificado al finalizar el proceso de verificación.
                SapServiceLayerClient.Notificar_Estado_SAP_Hana_MAMPA_By_Picking(oBeTrans_picking_enc.IdPickingEnc, 6, 6, 1, oBeTrans_picking_enc.User_mod, oBeTrans_picking_enc.IdBodega)
            End If

            Return vResult

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_verificacion_bof.Agregar_Error(vMsgError,
            'pStackTrace:=ex.StackTrace,
            'pIdPickingEnc:=oBeTrans_picking_enc.IdPickingEnc,
            'pIdPedidoEnc:=oBeTrans_picking_enc.IdPedidoEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Tiene_Pedidos_Sin_Verificar_By_IdPickingEnc(ByVal pIdPickingEnc As Integer) As Boolean

        Tiene_Pedidos_Sin_Verificar_By_IdPickingEnc = False

        Try

            Return clsLnTrans_picking_enc.Tiene_Pedidos_Sin_Verificar_By_IdPickingEnc(pIdPickingEnc)

        Catch ex As Exception

            '#MECR04122025: Se agrego bitacora para logs de verficiacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_verificacion_bof.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function GetSingleTareaHH(ByRef pBeTarea_hh As clsBeTarea_hh) As Boolean

        GetSingleTareaHH = False

        Try

            Return clsLnTarea_hh.GetSingle(pBeTarea_hh)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Pedidos_A_Verificar_By_IdBodega(ByVal pIdBodega As Integer, ByVal pIdOperadorBodega As Integer) As List(Of clsBeTrans_pe_enc)

        Get_All_Pedidos_A_Verificar_By_IdBodega = Nothing

        Try

            Return clsLnTrans_pe_enc.Get_All_Pedidos_A_Verificar_By_IdBodega(pIdBodega, pIdOperadorBodega)

        Catch ex As Exception

            '#MECR04122025: Se agrego bitacora de logs para verificacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_verificacion_bof.Agregar_Error(vMsgError, pIdBodega:=pIdBodega, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Pedido_A_Verificar_By_LP(ByVal pLP As String) As List(Of clsBeTrans_pe_enc)

        Get_Pedido_A_Verificar_By_LP = Nothing

        Try

            Return clsLnTrans_pe_enc.Get_Pedido_A_Verificar_By_LP(pLP)

        Catch ex As Exception

            '#MECR21102025: Se agrego bitacora de logs para pedidos
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Detalle_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer, ByVal pIdBodega As Integer) As List(Of clsBeDetallePedidoAVerificar)

        Get_Detalle_By_IdPedidoEnc = Nothing

        Try

            Return clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc(pIdPedidoEnc, pIdBodega)

        Catch ex As Exception

            '#MECR04122025: Se agrego bitacora de logs para verficiacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_verificacion_bof.Agregar_Error(vMsgError, pIdBodega:=pIdBodega, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Detalle_Verificacion_Consolidada(ByVal pIdPedidoEnc As Integer) As List(Of clsBeDetallePedidoAVerificar)

        Get_Detalle_Verificacion_Consolidada = Nothing

        Try

            Return clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc_Consolidado(pIdPedidoEnc)

        Catch ex As Exception

            '#MECR04122025: Se agrego bitacora de logs para verificacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_verificacion_bof.Agregar_Error(vMsgError, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Detalle_Verificacion_Consolidada_LFV(pIdPedidoEnc As Integer,
                                                             pIdProductoBodega As Integer,
                                                             pIdPresentacion As Integer) As List(Of clsBeDetallePedidoAVerificar)


        Get_Detalle_Verificacion_Consolidada_LFV = Nothing

        Try

            Return clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc_Consolidado_LFV(pIdPedidoEnc,
                                                                                pIdProductoBodega,
                                                                                pIdPresentacion)

        Catch ex As Exception

            '#MECR11122025: Se agrego bitacora de logs para verificacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_verificacion_bof.Agregar_Error(vMsgError, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace, pIdProductoBodega:=pIdProductoBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function


    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Reemplazo_Producto_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer) As List(Of clsBeDetallePedidoAVerificar)

        Get_Reemplazo_Producto_By_IdPedidoEnc = Nothing

        Try

            Return clsLnTrans_pe_det.Get_Reemplazo_Producto_By_IdPedidoEnc(pIdPedidoEnc)

        Catch ex As Exception

            '#MECR04122025: Se agrego bitacora de logs para verificacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_verificacion_bof.Agregar_Error(vMsgError, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Single_By_IdPedidoEnc(ByVal pIdPedidoEnc As Integer) As clsBeTrans_pe_enc

        Get_Single_By_IdPedidoEnc = Nothing

        Try

            Return clsLnTrans_pe_enc.Get_Single_By_IdPedidoEnc(pIdPedidoEnc)

        Catch ex As Exception

            '#MECR21102025: Se agrego bitacora de logs para pedidos
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_Estado_Pedido(ByRef oBeTrans_pe_enc As clsBeTrans_pe_enc) As Integer

        Actualizar_Estado_Pedido = 0

        Try

            Return clsLnTrans_pe_enc.Actualizar_Estado(oBeTrans_pe_enc)

        Catch ex As Exception

            '#MECR21102025: Se agrego bitacora de logs para pedidos
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdPedidoEnc:=oBeTrans_pe_enc.IdPedidoEnc, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Set_Estado_Pedido_Verificado(ByRef oBeTrans_pe_enc As clsBeTrans_pe_enc) As Integer

        Set_Estado_Pedido_Verificado = 0


        Try

            Set_Estado_Pedido_Verificado = clsLnTrans_pe_enc.Actualizar_Estado_Verificado(oBeTrans_pe_enc)

            'MECR04122025: Se agrego bitacora de logs para verificacion.
            Dim msgControl As String = "Se actualizo estado verificado el pedido: " & oBeTrans_pe_enc.IdPedidoEnc
            'clsLnLog_verificacion_bof.Agregar_Error(msgControl,
            'pIdPedidoEnc:=oBeTrans_pe_enc.IdPedidoEnc,
            'pIdBodega:=oBeTrans_pe_enc.IdBodega,
            'pIdPickingEnc:=oBeTrans_pe_enc.IdPickingEnc,
            'pUser_agr:=oBeTrans_pe_enc.User_mod)

            Return Set_Estado_Pedido_Verificado

        Catch ex As Exception

            '#MECR04122025: Se agrego bitacora de logs para verificacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_verificacion_bof.Agregar_Error(vMsgError,
            'pIdPedidoEnc:=oBeTrans_pe_enc.IdPedidoEnc,
            ' pStackTrace:=ex.StackTrace,
            'pIdBodega:=oBeTrans_pe_enc.IdBodega,
            '                                      pIdPickingEnc:=oBeTrans_pe_enc.IdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_Estado_Picking(ByRef oBeTrans_picking_enc As clsBeTrans_picking_enc) As Integer

        Actualizar_Estado_Picking = 0

        Try

            Return clsLnTrans_picking_enc.Actualizar_Estado(oBeTrans_picking_enc)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdPickingEnc:=oBeTrans_picking_enc.IdPickingEnc,
                                                  pIdPedidoEnc:=oBeTrans_picking_enc.IdPedidoEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Barras_Pallet_By_IdRecepcionEnc(ByVal pIdRecepcionEnc As Integer) As List(Of clsBeProducto_pallet)

        Get_All_Barras_Pallet_By_IdRecepcionEnc = Nothing

        Try

            Return clsLnProducto_pallet.Get_All_By_IdRecepcionEnc(pIdRecepcionEnc)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Inventario_Teorico_By_Codigo(ByVal IdInventarioEnc As Integer,
                                                     ByVal IdProducto As Integer) As List(Of clsBeTrans_inv_stock_prod)

        Get_Inventario_Teorico_By_Codigo = Nothing

        Try

            Return clsLnTrans_inv_stock_prod.GetAll(IdInventarioEnc, IdProducto)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Existe_Barra_Pallet_I_Nav(ByVal pCodigoBarraPallet As String) As Boolean

        Existe_Barra_Pallet_I_Nav = False

        Try

            Return clsLnI_nav_barras_pallet.Existe(pCodigoBarraPallet)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Pallet_Ingreso_By_Barra(ByVal pCodigoBarraPallet As String,
                                                    ByVal pIdBodega As Integer,
                                                    ByRef BeProducto As clsBeProducto) As clsBeI_nav_barras_pallet

        Get_Pallet_Ingreso_By_Barra = Nothing

        Try

            Return clsLnI_nav_barras_pallet.Get_Single_Pallet_Ingreso_By_Codigo_Barra_Pallet(pCodigoBarraPallet,
                                                                                                 pIdBodega,
                                                                                                 BeProducto)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Pallet_Ingreso_By_Barra(ByVal pCodigoBarraPallet As String,
                                                    ByVal pIdBodega As Integer,
                                                    ByRef BeProducto As clsBeProducto) As List(Of clsBeI_nav_barras_pallet)

        Get_All_Pallet_Ingreso_By_Barra = Nothing

        Try

            Return clsLnI_nav_barras_pallet.Get_All_Pallet_Ingreso_By_Codigo_Barra_Pallet(pCodigoBarraPallet,
                                                                                          pIdBodega,
                                                                                          BeProducto)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Configuracion_Barra_Pallet_By_IdConfiguracion(ByVal pIdConfiguracionBarraPallet As Integer) As clsBeConfiguracion_barra_pallet

        Get_Configuracion_Barra_Pallet_By_IdConfiguracion = Nothing

        Try

            Return clsLnConfiguracion_barra_pallet.GetSingle(pIdConfiguracionBarraPallet)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Existe_Producto(ByVal pCodigo As String) As Boolean

        Existe_Producto = False

        Try

            Return clsLnProducto.Existe_Codigo(pCodigo)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#AT20241209 Agregue el parametro pIdResolucion
    <WebMethod(), SoapHeader("mArch")>
    Public Function Guardar_Producto_Nuevo_Inventario(ByVal pBeProducto As clsBeProducto,
                                                          ByVal IdBodega As Integer,
                                                          ByVal IdInventario As Integer,
                                                          ByVal EsCiclico As Boolean,
                                                          ByVal BeInvCiclico As clsBeTrans_inv_ciclico,
                                                          ByVal BeInvInicial As clsBeTrans_inv_detalle,
                                                          ByVal pIdResolucion As Integer) As Boolean

        Guardar_Producto_Nuevo_Inventario = False

        Try

            Return clsLnTrans_inv_enc.Guarda_ProductoNuevo_AgregaInventario(pBeProducto, IdBodega, IdInventario, EsCiclico, BeInvCiclico, BeInvInicial, pIdResolucion)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Datos_Maestros_Inv(ByVal pIdPropietario As Integer,
                                               ByRef pListFamilia As DataTable,
                                               ByRef pListClasificacion As DataTable,
                                               ByRef pListMarca As DataTable,
                                               ByRef pListTipo As DataTable,
                                               ByRef pListUMBas As DataTable) As Boolean

        Get_Datos_Maestros_Inv = False
        Try

            Return clsLnProducto.Get_Datos_Maestros_Inv(pIdPropietario, pListFamilia, pListClasificacion, pListMarca, pListTipo, pListUMBas)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Datos_Maestros_Inv_Fam(ByVal pIdPropietario As Integer,
                                               ByRef pListFamilia As DataTable) As Boolean

        Get_Datos_Maestros_Inv_Fam = False
        Try

            Return clsLnProducto.Get_Datos_Maestros_Inv_Fam(pIdPropietario, pListFamilia)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Datos_Maestros_Inv_Clas(ByVal pIdPropietario As Integer,
                                               ByRef pListClasificacion As DataTable) As Boolean

        Get_Datos_Maestros_Inv_Clas = False
        Try

            Return clsLnProducto.Get_Datos_Maestros_Inv_Clas(pIdPropietario, pListClasificacion)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Datos_Maestros_Inv_Marc(ByVal pIdPropietario As Integer,
                                               ByRef pListMarca As DataTable) As Boolean

        Get_Datos_Maestros_Inv_Marc = False
        Try

            Return clsLnProducto.Get_Datos_Maestros_Inv_Marc(pIdPropietario, pListMarca)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Datos_Maestros_Inv_Tip(ByVal pIdPropietario As Integer,
                                             ByRef pListTipo As DataTable) As Boolean

        Get_Datos_Maestros_Inv_Tip = False
        Try

            Return clsLnProducto.Get_Datos_Maestros_Inv_Tip(pIdPropietario, pListTipo)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Datos_Maestros_Inv_UmBas(ByVal pIdPropietario As Integer,
                                               ByRef pListUMBas As DataTable) As Boolean

        Get_Datos_Maestros_Inv_UmBas = False
        Try

            Return clsLnProducto.Get_Datos_Maestros_Inv_UmBas(pIdPropietario, pListUMBas)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#MA20251410
    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Stock_By_Lic_Plate_JSON(ByVal pLicensePlate As String,
                                           ByVal pIdBodega As Integer)

        'Get_Stock_By_Lic_Plate = Nothing
        Try

            'Return clsLnStock.Get_Stock_By_LicensePlate(pLicensePlate,
            'pIdBodega)

            Dim productos As List(Of clsBeProducto) = clsLnStock.Get_Stock_By_LicensePlate(pLicensePlate, pIdBodega)

            ConvertirListasVaciasANothing(productos)

            Dim stockprodu As String = JsonConvert.SerializeObject(productos)
            Dim ObjProducto As JArray = JArray.Parse(stockprodu)

            For Each producto As JObject In ObjProducto

                SerializarJson(producto, "Presentacion.MedidasPorTarima")
                SerializarJson(producto, "Presentacion.RellenadoPorUbicacionDePicking")
                SerializarJson(producto, "Presentaciones")
                SerializarJson(producto, "Codigos_Barra")
                SerializarJson(producto, "Parametros")
                SerializarJson(producto, "Stock.BePresentacionProductoEnStock.MedidasPorTarima")
                SerializarJson(producto, "Stock.BePresentacionProductoEnStock.RellenadoPorUbicacionDePicking")

                Dim items = producto.SelectToken("Presentaciones.items")
                If items IsNot Nothing AndAlso items.Type = JTokenType.Array Then
                    For Each item As JObject In items
                        If item("RellenadoPorUbicacionDePicking") IsNot Nothing AndAlso item("RellenadoPorUbicacionDePicking").Type = JTokenType.Array Then
                            item("RellenadoPorUbicacionDePicking") = Nothing
                        End If
                    Next
                End If

            Next

            'Dim responseObj As New With {.items = productos}
            Dim json As String = JsonConvert.SerializeObject(ObjProducto, New JsonSerializerSettings With {
            .NullValueHandling = NullValueHandling.Include,
            .ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            .Formatting = Formatting.None
            })

            Dim jsonModificado As String = json.Replace("[]", "null")
            Dim curContext As HttpContext = HttpContext.Current

            curContext.Response.Clear()
            curContext.Response.StatusCode = 200
            curContext.Response.ContentType = "application/json"
            curContext.Response.Write(jsonModificado)
            'curContext.ApplicationInstance.CompleteRequest()

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pEsImplosion:=True, pIdBodega:=pIdBodega, pLic_Plate:=pLicensePlate)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try
    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Stock_By_Lic_Plate(ByVal pLicensePlate As String,
                                           ByVal pIdBodega As Integer) As List(Of clsBeProducto)

        Get_Stock_By_Lic_Plate = Nothing

        Try

            Return clsLnStock.Get_Stock_By_LicensePlate(pLicensePlate,
                                                        pIdBodega)

        Catch ex As Exception

            '#MECR18112025: Se agrego log para implosion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pEsImplosion:=True, pIdBodega:=pIdBodega, pLic_Plate:=pLicensePlate)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    '<WebMethod(), SoapHeader("mArch")>
    'Public Function Get_Stock_By_Lic_Plate_And_IdUbicacion(ByVal pLicensePlate As String,
    '                                                       ByVal pIdBodega As Integer,
    '                                                       ByVal pIdUbicacion As Integer) As List(Of clsBeProducto)

    '    Get_Stock_By_Lic_Plate_And_IdUbicacion = Nothing

    '    Try

    '        Return clsLnStock.Get_Stock_By_LicensePlate_And_IdUbicacion(pLicensePlate,
    '                                                    pIdBodega,
    '                                                    pIdUbicacion)

    '    Catch ex As Exception

    '        'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
    '        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)

    '        Dim Mensaje As String = ex.Message
    '        WriteErrorToEventLog(Mensaje)

    '        If mArch IsNot Nothing Then

    '            If mArch.Tipo = "WM" Then
    '                Throw New Exception(Mensaje)
    '            Else
    '                Dim currrentContext As HttpContext = HttpContext.Current
    '                Dim DT As New DataTable("CustomError")
    '                DT.Columns.Add("Error", GetType(String))
    '                DT.Rows.Add(Mensaje)
    '                Dim sw As New StringWriter()
    '                DT.WriteXml(sw)
    '                HttpContext.Current.Response.Clear()
    '                HttpContext.Current.Response.StatusCode = 299
    '                HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
    '                HttpContext.Current.Response.Output.Write(sw.ToString())
    '                HttpContext.Current.Response.ContentType = "text/xml"
    '                HttpContext.Current.Response.End()
    '            End If

    '        End If

    '    End Try

    'End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Stock_By_Lic_Plate_And_Codigo(ByVal pLicensePlate As String,
                                                      ByVal pCodigo As String,
                                                      ByVal pIdBodega As Integer) As List(Of clsBeProducto)

        Get_Stock_By_Lic_Plate_And_Codigo = Nothing

        Try

            Return clsLnStock.Get_Stock_By_Licencia_And_Codigo(pLicensePlate,
                                                                pCodigo,
                                                                pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Productos_By_ImpresionBarras(ByVal IdBodega As Integer,
                                                             ByVal IdEmpresa As Integer,
                                                             ByVal pIdPropietario As Integer,
                                                             ByVal pCodigo As String,
                                                             ByVal pNombre As String,
                                                             ByVal pIdClasificacion As Integer) As DataTable

        Get_All_Productos_By_ImpresionBarras = Nothing

        Try

            Return clsLnProducto.Get_All_For_Print_BarCodes(IdBodega,
                                                                IdEmpresa,
                                                                pIdPropietario,
                                                                pCodigo,
                                                                pNombre,
                                                                pIdClasificacion)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Propietarios_By_IdEmpresa(ByVal IdEmpresa As Integer) As DataTable

        Get_All_Propietarios_By_IdEmpresa = Nothing

        Try

            Return clsLnPropietarios.Get_All_By_IdEmpresa(IdEmpresa)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Clasificacion_By_IdPropietario(ByVal IdPropietario As Integer) As DataTable

        Get_All_Clasificacion_By_IdPropietario = Nothing

        Try

            Return clsLnProducto_clasificacion.Get_All_By_Propietario(IdPropietario)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_UMB_Pres_Stock_ByIdProducto_And_IdUMBas_AndIdBodega(ByVal IdBodega As Integer,
                                                                                ByVal IdUMBas As Integer,
                                                                                ByVal IdProducto As Integer,
                                                                                ByRef UMBas As DataTable,
                                                                                ByRef Presentaciones As DataTable,
                                                                                ByRef Stock As DataTable) As Boolean

        Get_UMB_Pres_Stock_ByIdProducto_And_IdUMBas_AndIdBodega = 0

        Try

            Return clsLnProducto.Get_UMB_Pres_ByIdProducto_And_IdBodega(IdBodega,
                                                                            IdUMBas,
                                                                            IdProducto,
                                                                            UMBas,
                                                                            Presentaciones,
                                                                            Stock)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Insertar_ImpresionProductosBarra(ByRef BeImpresion As clsBeImpresion_productos_barras) As Integer

        Insertar_ImpresionProductosBarra = 0

        Try

            Return clsLnImpresion_productos_barras.Insertar_Barra(BeImpresion)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_IdProductoBuenEstado_Por_Defecto_By_IdBodega_And_IdEmpresa(ByVal pIdBodega As Integer, ByVal pIdEmpresa As Integer) As Integer

        Get_IdProductoBuenEstado_Por_Defecto_By_IdBodega_And_IdEmpresa = 0

        Try

            Dim BeNavConfigEnc As New clsBeI_nav_config_enc
            BeNavConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(pIdBodega, pIdEmpresa)

            If BeNavConfigEnc IsNot Nothing Then
                Get_IdProductoBuenEstado_Por_Defecto_By_IdBodega_And_IdEmpresa = BeNavConfigEnc.IdProductoEstado
            Else
                Throw New Exception("No está definido el estado por defecto en la tabla: I_nav_config_enc para Empresa: " & pIdEmpresa & " Bodega: " & pIdBodega)
            End If

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Recepcion_Genera_Historico(ByVal pIdBodega As Integer, ByVal pIdEmpresa As Integer) As Boolean

        Get_Recepcion_Genera_Historico = False

        Try

            Dim BeNavConfigEnc As New clsBeI_nav_config_enc
            BeNavConfigEnc = clsLnI_nav_config_enc.Get_Single_By_IdBodega_And_IdEmpresa(pIdBodega, pIdEmpresa)

            If BeNavConfigEnc IsNot Nothing Then
                'Get_Recepcion_Genera_Historico = BeNavConfigEnc.Recepcion_Genera_Historico                
            Else
                Throw New Exception("No está definido generar histórico por defecto en la tabla: I_nav_config_enc para Empresa: " & pIdEmpresa & " Bodega: " & pIdBodega)
            End If

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdEmpresa, pIdBodega, 0, ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Codigo_Bodega_By_IdBodega(ByVal pIdBodega As Integer) As String

        Get_Codigo_Bodega_By_IdBodega = ""

        Try

            Return clsLnBodega.Get_Codigo_By_IdBodega(pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_IdEstadoProductoNE_By_IdBodega(ByVal pIdBodega As Integer) As Integer

        Get_IdEstadoProductoNE_By_IdBodega = 0

        Try

            Return clsLnBodega.Get_IdProductoEstadoNE_By_IdBodega(pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_BeImpresora_By_IdImpresora(ByVal pIdBodega As Integer) As clsBeImpresora

        Get_BeImpresora_By_IdImpresora = Nothing

        Try

            Return clsLnImpresora.Get_Single_By_IdEmpresora(pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Permite_Imprimir(ByVal pIdImpresora As Integer) As Boolean

        Get_Permite_Imprimir = False

        Try

            Return clsLnImpresora.Get_PermiteImprimir(pIdImpresora)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Paises(ByRef BePais As clsBePaises) As Boolean

        Get_Paises = False

        Try

            Return clsLnPaises.GetSingle(BePais)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    Public Property mArch As New clsArchHeader

    <WebMethod(), SoapHeader("mArch")>
    Public Function Android_Get_All_Empresas() As List(Of clsBeEmpresaBase)

        Android_Get_All_Empresas = Nothing

        Try
            '#EJC20210223: Agregue imagen empresa
            Dim lEmpresas As New List(Of clsBeEmpresaBase)
            lEmpresas = clsLnEmpresa.Android_Get_All()
            Return lEmpresas

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    Public Function WriteErrorToEventLog(ByVal entry As String) As Boolean

        Dim objEventLog As New EventLog

        Try

            Dim appName As String = "WMS"
            Dim logName As String = "WebServiceHH"
            'Register the Application as an Event Source
            If Not EventLog.SourceExists(appName) Then
                EventLog.CreateEventSource(appName, logName)
            End If

            'log the entry
            objEventLog.Source = appName
            objEventLog.WriteEntry(entry, EventLogEntryType.Error)

            Return True

        Catch Ex As Exception
            Return False
        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_cantidad_decimales_calculo(ByVal pIdEmpresa As Integer) As Integer

        Get_cantidad_decimales_calculo = 6

        Try

            Return clsLnEmpresa.Get_cantidad_decimales_calculo(pIdEmpresa)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Cantidad_decimales_despliegue(ByVal pIdEmpresa As Integer) As Integer

        Get_Cantidad_decimales_despliegue = 2

        Try

            Return clsLnEmpresa.Get_cantidad_decimales_despliegue(pIdEmpresa)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Sub Agregar_Marcaje(ByVal pIdEmpresa As Integer,
                               ByVal pIdBodega As Integer,
                               ByVal pIdOperador As Integer,
                               ByVal pIdDispositivo As String,
                               ByVal pEsSalida As Boolean)

        Try

            clsLnMarcaje.Agregar_Marcaje(pIdEmpresa, pIdBodega, pIdOperador, pIdDispositivo, pEsSalida)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    '#MA20250210 migracion de xml a Json
    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub ml_get_ubicacion_sugerida_JSON(ByVal pIdProducto As Integer,
                                          ByVal pIdBodega As Integer,
                                          ByVal pIdProductoBodega As Integer,
                                          ByVal pLote As String,
                                          ByVal pFechaVence As Date,
                                          ByVal pIdProductoEstado As Integer,
                                          ByVal pIdUmBas As Integer,
                                          ByVal pIdPresentacion As Integer)

        ' ml_get_ubicacion_sugerida = Nothing

        Try

            Dim listaUbicaciones As List(Of clsLnTransUbicSugerida2.USUbicStrucStage5) =
            clsLnTransUbicSugerida2.Get_Ubicaciones_Sugeridas(pIdProducto,
                                                              pIdBodega,
                                                              pIdProductoBodega,
                                                              pLote,
                                                              pFechaVence,
                                                              pIdProductoEstado,
                                                              pIdUmBas,
                                                              pIdPresentacion)

            If listaUbicaciones Is Nothing Then
                listaUbicaciones = New List(Of clsLnTransUbicSugerida2.USUbicStrucStage5)()
            End If

            Dim json As String = JsonConvert.SerializeObject(New With {
                                                                .items = listaUbicaciones
                                                            }, New JsonSerializerSettings With {
                                                                .NullValueHandling = NullValueHandling.Include,
                                                                .ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                                .Formatting = Formatting.None
                                                            })

            Dim curContext As HttpContext = HttpContext.Current

            curContext.Response.Clear()
            curContext.Response.StatusCode = 200
            curContext.Response.ContentType = "application/json"
            curContext.Response.Write(json)
            'curContext.ApplicationInstance.CompleteRequest()

        Catch ex As Exception
            '#MECR04112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdBodega:=pIdBodega,
                                                  pIdUMBAs:=pIdUmBas,
                                                  pIdPresentacion:=pIdPresentacion)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then
                '#MA20260505 Manejo de error
                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try
    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function ml_get_ubicacion_sugerida(ByVal pIdProducto As Integer,
                                              ByVal pIdBodega As Integer,
                                              ByVal pIdProductoBodega As Integer,
                                              ByVal pLote As String,
                                              ByVal pFechaVence As Date,
                                              ByVal pIdProductoEstado As Integer,
                                              ByVal pIdUmBas As Integer,
                                              ByVal pIdPresentacion As Integer) As List(Of clsLnTransUbicSugerida2.USUbicStrucStage5)

        ml_get_ubicacion_sugerida = Nothing

        Try

            Return clsLnTransUbicSugerida2.Get_Ubicaciones_Sugeridas(pIdProducto,
                                                                     pIdBodega,
                                                                     pIdProductoBodega,
                                                                     pLote,
                                                                     pFechaVence,
                                                                     pIdProductoEstado,
                                                                     pIdUmBas,
                                                                     pIdPresentacion)

        Catch ex As Exception
            '#MECR04112025: Se agrego bitacora de ubicacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdBodega:=pIdBodega,
                                                  pIdUMBAs:=pIdUmBas,
                                                  pIdPresentacion:=pIdPresentacion)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKFK 20200505 Creé función para actualizar la cantidad en las verificaciones de la aplicación de Android
    '#AT20241206 Agregue el parametro pIdPedidoEnc
    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualiza_Cant_Peso_Verificacion(ByVal pBePickingUbicList As List(Of clsBeTrans_picking_ubic),
                                                     ByVal pIdOperador As Integer,
                                                     ByRef pCantidad As Double,
                                                     ByRef pPeso As Double,
                                                     ByVal pTipo As Integer,
                                                     ByVal pIdPedidoEnc As Integer,
                                                     ByRef pEtiqueta As clsBeTrans_verificacion_etiqueta) As Boolean

        Actualiza_Cant_Peso_Verificacion = False

        Try

            If pBePickingUbicList IsNot Nothing Then

                Return clsLnTrans_picking_ubic.Actualiza_Cant_Peso_Verificacion(pBePickingUbicList,
                                                                                pIdOperador,
                                                                                pCantidad,
                                                                                pPeso,
                                                                                pTipo,
                                                                                pIdPedidoEnc,
                                                                                pEtiqueta)


            Else
                Throw New Exception("ERROR_20220410_0907: La lista de verificación está vacía.")
            End If

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKFK 20200511 Creé esta función para reemplazo de inventario en las verificaciones
    <WebMethod(), SoapHeader("mArch")>
    Public Function Reemplaza_Producto_Dannado_Menor(ByVal plistPickingUbi As List(Of clsBeTrans_picking_ubic),
                                                     ByVal pIdStock As Integer,
                                                     ByVal pIdPickingEnc As Integer,
                                                     ByVal pIdOperador As Integer,
                                                     ByVal pHost As String,
                                                     ByVal pIdBodega As Integer,
                                                     ByVal pIdEmpresa As Integer,
                                                     ByVal pIdUbicDestino As Integer,
                                                     ByVal pIdEstadoDestino As Integer,
                                                     ByVal pCantLinea As Double,
                                                     ByRef pCantReemplazar As Double) As Boolean

        Dim BeStockRes As New clsBeStock_res
        Dim resultado As String = ""
        Dim BePickingUbic As New clsBeTrans_picking_ubic

        Reemplaza_Producto_Dannado_Menor = False

        Try
            Return clsLnTrans_picking_ubic.Reemplaza_Producto_Dannado_Menor(plistPickingUbi,
                                                                                  pIdStock,
                                                                                  pIdPickingEnc,
                                                                                  pIdOperador,
                                                                                  pHost,
                                                                                  pIdBodega,
                                                                                  pIdEmpresa,
                                                                                  pIdUbicDestino,
                                                                                  pIdEstadoDestino,
                                                                                  pCantLinea,
                                                                                  pCantReemplazar)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdEmpresa:=pIdEmpresa,
                                                  pIdBodega:=pIdBodega,
                                                  pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKFK 20200511 Creé esta función para reemplazo de inventario en las verificaciones
    <WebMethod(), SoapHeader("mArch")>
    Public Function Reemplaza_Producto_Dannado_Mayor_Igual(ByVal plistPickingUbi As List(Of clsBeTrans_picking_ubic),
                                                           ByVal pIdStock As Integer,
                                                           ByVal pIdPickingEnc As Integer,
                                                           ByVal pIdOperador As Integer,
                                                           ByVal pHost As String,
                                                           ByVal pIdBodega As Integer,
                                                           ByVal pIdEmpresa As Integer,
                                                           ByVal pIdUbicDestino As Integer,
                                                           ByVal pIdEstadoDestino As Integer,
                                                           ByVal pCantLinea As Double,
                                                           ByRef pCantReemplazar As Double) As Boolean

        Reemplaza_Producto_Dannado_Mayor_Igual = False

        Try

            Return clsLnTrans_picking_ubic.Reemplaza_Producto_Dannado_Mayor_Igual(plistPickingUbi,
                                                                                  pIdStock,
                                                                                  pIdPickingEnc,
                                                                                  pIdOperador,
                                                                                  pHost,
                                                                                  pIdBodega,
                                                                                  pIdEmpresa,
                                                                                  pIdUbicDestino,
                                                                                  pIdEstadoDestino,
                                                                                  pCantLinea,
                                                                                  pCantReemplazar)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdEmpresa:=pIdEmpresa,
                                                  pIdBodega:=pIdBodega,
                                                  pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKFK 20200514 Creé esta función para marcar dañados todos los picking ubic del pedido det cuando no haya existencias
    <WebMethod(), SoapHeader("mArch")>
    Public Function Marcar_Danado(ByVal plistPickingUbi As List(Of clsBeTrans_picking_ubic),
                                  ByVal pCantidad As Double,
                                  ByVal pIdBodega As Integer,
                                  ByVal pIdEmpresa As Integer,
                                  ByVal pIdUbicDestino As Integer,
                                  ByVal pIdEstadoDestino As Integer,
                                  ByVal pIdOperador As Integer,
                                  ByVal pHostSolicita As String) As Boolean

        Marcar_Danado = False

        Try

            Return clsLnTrans_picking_ubic.Marcar_Danado(plistPickingUbi,
                                                         pCantidad,
                                                         pIdBodega,
                                                         pIdEmpresa,
                                                         pIdUbicDestino,
                                                         pIdEstadoDestino,
                                                         pIdOperador,
                                                         pHostSolicita)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdEmpresa:=pIdEmpresa,
                                                  pIdBodega:=pIdBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    'CM: funciones para mostrar valores para agregar producto nuevo
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Familia_Inv(ByVal pIdPropietario As Integer) As DataTable

        Get_Familia_Inv = Nothing
        Try

            Return clsLnProducto.Get_Familia_Inv(pIdPropietario)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Clasificacion_Inv(ByVal pIdPropietario As Integer) As DataTable

        Get_Clasificacion_Inv = Nothing
        Try

            Return clsLnProducto.Get_Clasificacion_Inv(pIdPropietario)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Marca_Inv(ByVal pIdPropietario As Integer) As DataTable

        Get_Marca_Inv = Nothing

        Try

            Return clsLnProducto.Get_Clasificacion_Inv(pIdPropietario)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Tipo_Inv(ByVal pIdPropietario As Integer) As DataTable

        Get_Tipo_Inv = Nothing

        Try

            Return clsLnProducto.Get_Tipo_Inv(pIdPropietario)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_UMbas_Inv(ByVal pIdPropietario As Integer) As DataTable

        Get_UMbas_Inv = Nothing

        Try

            Return clsLnProducto.Get_UMbas_Inv(pIdPropietario)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Existe_Lp(ByVal pLic_Plate As String,
                              ByVal pIdBodega As Integer,
                              ByVal pIdStock As Integer) As Boolean

        Existe_Lp = False

        Try

            Existe_Lp = clsLnStock_rec.Existe_Lp(pLic_Plate, pIdBodega, pIdStock)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Existe_Lp_In_Stock(ByVal pLic_Plate As String) As Boolean

        Existe_Lp_In_Stock = False

        Try

            'Existe_Lp_In_Stock = clsLnStock_rec.Existe_Lp_In_Stock(pLic_Plate)
            Existe_Lp_In_Stock = clsLnStock.Existe_Lp_In_Stock(pLic_Plate)


        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    '#MA20251410 migracion de xml a json
    <WebMethod(), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True)>
    Public Sub Existe_Lp_By_Licencia_And_IdBodega_JSON(ByVal pLic_Plate As String,
                                                       ByVal pIdBodega As Integer)

        ' Existe_Lp_By_Licencia_And_IdBodega = False

        Try

            'Existe_Lp_By_Licencia_And_IdBodega = clsLnStock_rec.Existe_Lp_In_Stock(pLic_Plate)
            '#CKFK20220618 Modifiqué la funcion que se llama para que si se haga esta consulta por licencia
            Dim Existe As Boolean = clsLnStock.Existe_Lp_In_Stock_By_IdBodega(pLic_Plate, pIdBodega)
            Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Existe = Existe})
            Dim curContext As HttpContext = HttpContext.Current
            curContext.Response.Clear()
            curContext.Response.StatusCode = 200
            curContext.Response.ContentType = "application/json"
            curContext.Response.Write(json)
            'curContext.ApplicationInstance.CompleteRequest()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = String.Format("{{""Error"":true,""Mensaje"":""{0}""}}", ex.Message.Replace("""", "'"))
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Existe_Lp_By_Licencia_And_IdBodega(ByVal pLic_Plate As String, ByVal pIdBodega As Integer) As Boolean

        Existe_Lp_By_Licencia_And_IdBodega = False

        Try

            'Existe_Lp_By_Licencia_And_IdBodega = clsLnStock_rec.Existe_Lp_In_Stock(pLic_Plate)
            '#CKFK20220618 Modifiqué la funcion que se llama para que si se haga esta consulta por licencia
            Existe_Lp_By_Licencia_And_IdBodega = clsLnStock.Existe_Lp_In_Stock_By_IdBodega(pLic_Plate, pIdBodega)

        Catch ex As Exception

            '#MECR19112025: Se agrego bitacora de logs para implosion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pLic_Plate:=pLic_Plate, pEsImplosion:=True)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    '#AT20250129 Agregué el parámetro pIdUbicStock en Get_Ubicacion_LP
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Ubicacion_LP(ByVal pLic_Plate As String,
                                     ByVal pIdBodega As Integer,
                                     ByVal pIdUbicStock As Integer,
                                     ByRef nombre_ubicacion As String) As Integer

        Get_Ubicacion_LP = 0

        Try

            Get_Ubicacion_LP = clsLnStock_rec.Get_Ubicacion_LP(pLic_Plate, pIdBodega, pIdUbicStock, nombre_ubicacion)

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pLicencia:=pLic_Plate)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Existe_Serie(ByVal pSerie As String,
                                 ByVal pIdBodega As Integer) As Boolean

        Existe_Serie = False

        Try

            Existe_Serie = clsLnStock_rec.Existe_Serie(pSerie, pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#MA20251410 migracion de xml a json
    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=False, XmlSerializeString:=False)>
    Public Function Es_Pallet_No_Estandar_JSON(ByVal pStock As clsBeStock) As Object

        Try
            Dim esNoEstandar As Boolean = clsLnStock.Es_Pallet_No_Estandar(pStock)

            Return New With {
            .Error = False,
            .Resultado = esNoEstandar
        }

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            WriteErrorToEventLog(ex.Message)

            ' Si necesitas mantener el comportamiento legacy por SoapHeader:
            If mArch IsNot Nothing AndAlso mArch.Tipo = "WM" Then
                Throw New Exception(ex.Message, ex)
            End If

            ' Devuelve JSON de error consistente
            Return New With {
            .Error = True,
            .Mensaje = ex.Message
        }
        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Es_Pallet_No_Estandar(ByVal pStock As clsBeStock) As Boolean

        Es_Pallet_No_Estandar = False

        Try

            Es_Pallet_No_Estandar = clsLnStock.Es_Pallet_No_Estandar(pStock)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    <WebMethod(), SoapHeader("mArch")>
    Public Function Tiene_Posiciones(ByVal pStock As clsBeStock) As Integer

        Tiene_Posiciones = 0

        Try

            Tiene_Posiciones = clsLnStock.Tiene_Posiciones(pStock)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Count_Recepciones_For_HH_By_IdBodega(ByVal pIdBodega As Integer,
                                                             ByVal pIdOperadorBodega As Integer,
                                                             ByRef lrec As List(Of Integer)) As Integer

        Get_Count_Recepciones_For_HH_By_IdBodega = 0

        Try
            Get_Count_Recepciones_For_HH_By_IdBodega = clsLnTarea_hh.Get_Count_Recepciones_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega)
            lrec = clsLnTarea_hh.Get_IDs_Recepciones_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega)
        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, pIdBodega, 0, ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If Not mArch Is Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Count_Picking_For_HH_By_IdBodega(ByVal pIdBodega As Integer,
                                                         ByVal pIdOperadorBodega As Integer, ByRef lrec As List(Of Integer)) As Integer
        Get_Count_Picking_For_HH_By_IdBodega = 0
        Try
            Get_Count_Picking_For_HH_By_IdBodega = clsLnTarea_hh.Get_Count_Picking_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega)
            lrec = clsLnTarea_hh.Get_IDs_Picking_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega)
        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If Not mArch Is Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Count_Verificaciones_For_HH_By_IdBodega(ByVal pIdBodega As Integer,
                                                                ByVal pIdOperadorBodega As Integer,
                                                                ByRef lrec As List(Of Integer)) As Integer

        Get_Count_Verificaciones_For_HH_By_IdBodega = 0

        Try
            Get_Count_Verificaciones_For_HH_By_IdBodega = clsLnTarea_hh.Get_Count_Verificaciones_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega)
            lrec = clsLnTarea_hh.Get_IDs_Verificaciones_For_HH_By_IdBodega(pIdBodega, pIdOperadorBodega)
        Catch ex As Exception

            ''Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If Not mArch Is Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Count_Cambio_Est_Ubic_For_HH(ByVal pCambioEstado As Integer,
                                                     ByVal pIdBodega As Integer,
                                                     ByVal pIdOperadorBodega As Integer,
                                                     ByRef lrec As List(Of Integer)) As Integer

        Get_Count_Cambio_Est_Ubic_For_HH = 0
        Try
            Get_Count_Cambio_Est_Ubic_For_HH = clsLnTarea_hh.Get_Count_Cambio_Est_Ubic_For_HH(pCambioEstado, pIdBodega, pIdOperadorBodega)
            lrec = clsLnTarea_hh.Get_IDs_Cambio_Est_Ubic_For_HH(pCambioEstado, pIdBodega, pIdOperadorBodega)
        Catch ex As Exception

            ''Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If Not mArch Is Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Count_Tareas_Servicio(ByVal pIdBodega As Integer, ByVal pIdOperadorBodega As Integer) As String

        Dim Ss As String

        Try

            '#GT18042023: la codificación anterior se maneja dentro del nuevo método, bajo una misma transacción.
            Ss = ""
            Ss = clsLnTarea_hh.Get_Count_All_Services(pIdBodega, pIdOperadorBodega)

            Return Ss

        Catch ex As Exception
            Return "#" & ex.Message
        End Try

    End Function


    '#MA20250210 migracion de xml a Json
    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Ubicacion_Es_Valida_JSON(ByVal pIdProducto As Integer,
                                        ByVal pIdUbicacion As Integer,
                                        ByVal pIdBodega As Integer)

        ' Ubicacion_Es_Valida = True

        Try

            Dim esvalida As Boolean = clsLnTrans_ubicsug.Ubicacion_Es_Valida(pIdProducto, pIdUbicacion, pIdBodega)

            Dim json As String = JsonConvert.SerializeObject(New With {
            .UbicacionValida = esvalida
                        },
            New JsonSerializerSettings With {
                .NullValueHandling = NullValueHandling.Include
            })

            Dim curContext As HttpContext = HttpContext.Current
            curContext.Response.Clear()
            curContext.Response.StatusCode = 200
            curContext.Response.ContentType = "application/json"
            curContext.Response.Write(json)
            'curContext.ApplicationInstance.CompleteRequest()

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pIdUbicacionDestino:=pIdUbicacion)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try

    End Sub
    <WebMethod(), SoapHeader("mArch")>
    Public Function Ubicacion_Es_Valida(ByVal pIdProducto As Integer,
                                        ByVal pIdUbicacion As Integer,
                                        ByVal pIdBodega As Integer) As Boolean

        Ubicacion_Es_Valida = True

        Try

            Return clsLnTrans_ubicsug.Ubicacion_Es_Valida(pIdProducto, pIdUbicacion, pIdBodega)

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pIdUbicacionDestino:=pIdUbicacion)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If Not mArch Is Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    Public Shared Property CredencialesConexion As New NetworkCredential With
                {.Domain = "byb",
                .UserName = "wmsbodega1",
                .Password = "**ProductosbyB.2022++"}

    <WebMethod(), SoapHeader("mArch")>
    Public Function Push_Recepcion_To_NAV_For_BYB(ByVal Location_Code As String,
                                                  ByVal Zone_Code As String,
                                                  ByVal Bin_Code As String,
                                                  ByVal Assigne_User_Id As String,
                                                  ByVal Item_No As String,
                                                  ByVal No_Orden_Prod As String,
                                                  ByVal IdRecepcionEnc As Integer,
                                                  ByVal IdRecepcionDet As Integer,
                                                  ByVal pIdUsuario As Integer,
                                                  ByRef pRespuesta As String) As Ubicar_Almacen

        Dim ActivityNo As String = ""
        Dim vResultadoPutAwayCreate As String = ""

        Dim vDocumentoUbicacion As String = ""
        Dim vRecepcionAlmacen As String = ""
        Dim vTipoPush As String = "Push_Recepcion_To_NAV_For_BYB"

        Push_Recepcion_To_NAV_For_BYB = Nothing

        Try
            clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(vDocumentoUbicacion, vRecepcionAlmacen, vTipoPush, "", IdRecepcionEnc, IdRecepcionDet, pIdUsuario)
        Catch ex As Exception
            Throw New Exception("No se pudo guardar la transaccion de push " + ex.Message)
        End Try

        Dim ws1 As New U_Internas_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = "http://186.151.196.178:50605/DynamicsNAV110/WS/PRALCASA/Page/U_Internas"
        }

        Dim ws2 As New CUWMS() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = "http://186.151.196.178:50605/DynamicsNAV110/WS/PRALCASA/Codeunit/CUWMS"
        }

        Dim ws3 As New Ubicar_Almacen_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = "http://186.151.196.178:50605/DynamicsNAV110/WS/PRALCASA/Page/Ubicar_Almacen"
        }

        '#EJC20210311: No de pedido.
        'Tipo de orden
        Dim ws4 As New Mov_Productos_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = "http://168.194.73.242:1050/DynamicsNAV110/WS/PRALCASA/Page/Mov_Productos"
        }

        Try

            '#EJC20210309: Crear un documento de ubicación (cabecera)
            Dim BeUInterna As New U_Internas()
            BeUInterna.Location_Code = Location_Code 'BA0002
            BeUInterna.From_Zone_Code = Zone_Code 'PROD
            BeUInterna.From_Bin_Code = Bin_Code 'PROD
            BeUInterna.Assigned_User_ID = Assigne_User_Id '"byb\wmsbodega1"
            BeUInterna.Sorting_Method = wsBYBNavUbicarAlmacen.Sorting_Method.Item
            ws1.Create(BeUInterna)

            Dim vDocumentoUbicacionAsignado As String = BeUInterna.No
            Dim vResultadoGetBinContent As String = ""

            '#EJC20210309: Crear línea para documento.
            ws2.GetBinContent(vDocumentoUbicacionAsignado,
                              Location_Code,
                              Zone_Code,
                              Bin_Code,
                              Item_No,
                              vResultadoGetBinContent,
                              "")

            If vResultadoGetBinContent = "Successfully Created" Then

                '#EJC20210309:Obtener el registro de produccion de un producto en particular (item_no)
                Dim BeUInternaRegistradaEnProd As New U_Internas()
                BeUInternaRegistradaEnProd = ws1.Read(vDocumentoUbicacionAsignado)

                Try

                    '#EJC20210309:Crea la ubicación en el almacén.
                    ws2.PutAwayCreate(vDocumentoUbicacionAsignado, ActivityNo, vResultadoPutAwayCreate)
                    'ws2.RegisterPutAway()
                Catch ex As Exception

                    If ex.Message.Contains("There is nothing to release for Whse.") Then
                        'No tiene líneas de detalle.
                        '#EJC20210311: Eliminar el documento.
                        ws1.Delete(BeUInterna.Key)
                    End If

                End Try

                If vResultadoPutAwayCreate = "Successfully Created" Then

                    Dim UbicarAlmacen As New Ubicar_Almacen()
                    UbicarAlmacen = ws3.Read(ActivityNo)

                    Dim BeINavTransaccionesPush As New clsBeI_nav_transacciones_push

                    BeINavTransaccionesPush.IdRecepcionEnc = IdRecepcionEnc
                    BeINavTransaccionesPush.IdRecepcionDet = IdRecepcionDet
                    clsLnI_nav_transacciones_push.GetSingle_By_RecepcionDet(BeINavTransaccionesPush)

                    If Not BeINavTransaccionesPush Is Nothing Then
                        BeINavTransaccionesPush.IdTransaccionPush = BeINavTransaccionesPush.IdTransaccionPush
                        BeINavTransaccionesPush.Enviado_A_ERP = 1
                        BeINavTransaccionesPush.Fec_mod = Now.Date

                        clsLnI_nav_transacciones_push.Actualizar_Bandera_Enviado(BeINavTransaccionesPush)
                    End If

                    Push_Recepcion_To_NAV_For_BYB = UbicarAlmacen

                End If

            End If

        Catch ex As Exception

            Dim Mensaje As String = ex.Message
            Dim cantReg As Integer
            pRespuesta = Mensaje

            WriteErrorToEventLog(Mensaje)

            '#CKFK20220121 Aquí vamos a insertar el registro que no se comunicó en la tabla i_nav_transacciones_push
            cantReg = clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(vDocumentoUbicacion,
                                                                                  vRecepcionAlmacen,
                                                                                  vTipoPush,
                                                                                  Mensaje,
                                                                                  IdRecepcionEnc,
                                                                                  IdRecepcionDet,
                                                                                  pIdUsuario)

            If cantReg = 0 Then

                If Not mArch Is Nothing Then

                    If mArch.Tipo = "WM" Then
                        Throw New Exception(Mensaje)
                    Else
                        Dim currrentContext As HttpContext = HttpContext.Current
                        Dim DT As New DataTable("CustomError")
                        DT.Columns.Add("Error", GetType(String))
                        DT.Rows.Add(Mensaje)
                        Dim sw As New StringWriter()
                        DT.WriteXml(sw)
                        HttpContext.Current.Response.Clear()
                        HttpContext.Current.Response.StatusCode = 299
                        HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                        HttpContext.Current.Response.Output.Write(sw.ToString())
                        HttpContext.Current.Response.ContentType = "text/xml"
                        HttpContext.Current.Response.End()
                    End If

                End If

            Else

                Return Nothing

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Push_Recepcion_Produccion_To_NAV_For_BYB(ByVal DocumentoUbicacion As String,
                                                             ByVal CodigoProducto As String,
                                                             ByVal Cantidad As Double,
                                                             ByVal IdRecepcionEnc As Integer,
                                                             ByVal IdRecepcionDet As Integer,
                                                             ByVal pIdUsuario As Integer,
                                                             ByRef pRespuesta As String) As Boolean

        Dim vResultadoPutAwayCreate As String = ""

        Dim vDocumentoUbicacion As String = DocumentoUbicacion
        Dim vRecepcionAlmacen As String = ""
        Dim vTipoPush As String = "Push_Recepcion_Produccion_To_NAV_For_BYB"

        Push_Recepcion_Produccion_To_NAV_For_BYB = False

        Try
            clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(vDocumentoUbicacion, vRecepcionAlmacen, vTipoPush, "", IdRecepcionEnc, IdRecepcionDet, pIdUsuario)
        Catch ex As Exception
            Throw New Exception("No se pudo guardar la transaccion de push " + ex.Message)
        End Try

        Dim vUrl As String = My.Settings.WSHHRN_wsBYBNavCUWMS_CUWMS

        Dim ws2 As New CUWMS() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrl
        }

        vUrl = My.Settings.WSHHRN_wsBYBNavUbicarAlmacen_Ubicar_Almacen_Service

        Dim ws3 As New Ubicar_Almacen_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrl
        }

        Try

            Dim UbicarAlmacen As New Ubicar_Almacen()
            UbicarAlmacen = ws3.Read(DocumentoUbicacion)

            '#EJC20220405: Algunas veces NAV no devuelve la ubicación..
            If Not UbicarAlmacen Is Nothing Then

                '#EJC20210324: Modificar cantidad a tomar/colocar si es diferente.
                For Each Lu In UbicarAlmacen.WhseActivityLines
                    If Lu.Item_No = CodigoProducto Then
                        If Not (Lu.Qty_Handled = Cantidad) Then
                            Lu.Qty_to_Handle = Cantidad
                        End If
                    End If
                Next

                '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
                ws3.Update(UbicarAlmacen)

                ws2.RegisterPutAway(DocumentoUbicacion,
                                vResultadoPutAwayCreate)

                If vResultadoPutAwayCreate = "Successfully Created" Then
                    pRespuesta = vResultadoPutAwayCreate

                    Dim BeINavTransaccionesPush As New clsBeI_nav_transacciones_push

                    BeINavTransaccionesPush.IdRecepcionEnc = IdRecepcionEnc
                    BeINavTransaccionesPush.IdRecepcionDet = IdRecepcionDet
                    clsLnI_nav_transacciones_push.GetSingle_By_RecepcionDet(BeINavTransaccionesPush)

                    If Not BeINavTransaccionesPush Is Nothing Then
                        BeINavTransaccionesPush.IdTransaccionPush = BeINavTransaccionesPush.IdTransaccionPush
                        BeINavTransaccionesPush.Enviado_A_ERP = 1
                        BeINavTransaccionesPush.Fec_mod = Now.Date

                        clsLnI_nav_transacciones_push.Actualizar_Bandera_Enviado(BeINavTransaccionesPush)
                    End If

                    Push_Recepcion_Produccion_To_NAV_For_BYB = True
                Else
                    Throw New Exception(vResultadoPutAwayCreate & " Ubicación " & DocumentoUbicacion)
                End If
            Else

                Throw New Exception("No se pudo obtener la ubicacion en NAV " & DocumentoUbicacion)

            End If

        Catch ex As Exception

            'Throw ex
            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, pIdUsuario, ex.StackTrace, IdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            Dim cantReg As Integer
            pRespuesta = Mensaje

            WriteErrorToEventLog(Mensaje)

            '#CKFK20220121 Aquí vamos a insertar el registro que no se comunicó en la tabla i_nav_transacciones_push
            cantReg = clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(vDocumentoUbicacion,
                                                                                  vRecepcionAlmacen,
                                                                                  vTipoPush,
                                                                                  Mensaje,
                                                                                  IdRecepcionEnc,
                                                                                  IdRecepcionDet,
                                                                                  pIdUsuario)

            If cantReg = 0 Then

                If Not mArch Is Nothing Then

                    If mArch.Tipo = "WM" Then
                        Throw New Exception(Mensaje)
                    Else
                        Dim currrentContext As HttpContext = HttpContext.Current
                        Dim DT As New DataTable("CustomError")
                        DT.Columns.Add("Error", GetType(String))
                        DT.Rows.Add(Mensaje)
                        Dim sw As New StringWriter()
                        DT.WriteXml(sw)
                        HttpContext.Current.Response.Clear()
                        HttpContext.Current.Response.StatusCode = 299
                        HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                        HttpContext.Current.Response.Output.Write(sw.ToString())
                        HttpContext.Current.Response.ContentType = "text/xml"
                        HttpContext.Current.Response.End()
                    End If

                End If

            Else

                Return False

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Guardar_Transaccion_Error(ByVal pDocumentoUbicacion As String,
                                              ByVal pTipoPush As String,
                                              ByVal pRecepcionAlmacen As String,
                                              ByVal pIdRecepcionEnc As Integer,
                                              ByVal pIdRecepcionDet As Integer,
                                              ByVal pIdUsuario As Integer,
                                              ByVal pMensaje As String) As Boolean

        Guardar_Transaccion_Error = False

        Try

            Dim cantReg As Integer

            '#CKFK20220121 Aquí vamos a insertar el registro que no se comunicó en la tabla i_nav_transacciones_push
            cantReg = clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(pDocumentoUbicacion,
                                                                                  pRecepcionAlmacen,
                                                                                  pTipoPush,
                                                                                  pMensaje,
                                                                                  pIdRecepcionEnc,
                                                                                  pIdRecepcionDet,
                                                                                  pIdUsuario)

            Return cantReg > 0

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            If Not mArch Is Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(pMensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(pMensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Push_Recepcion_Pedido_Compra_To_NAV_For_BYB(ByVal DocumentoIngreso As String,
                                                                ByVal DocumentoRecepcion As String,
                                                                ByVal NoLinea As Integer,
                                                                ByVal CodigoProducto As String,
                                                                ByVal Cantidad As Double,
                                                                ByVal NoLote As String,
                                                                ByVal FechaVence As Date,
                                                                ByVal NomUnidadMedida As String,
                                                                ByVal IdRecepcionEnc As Integer,
                                                                ByVal IdRecepcionDet As Integer,
                                                                ByVal pIdUsuario As Integer,
                                                                ByRef pRespuesta As String) As Boolean

        '#EJC20210428: Debería devolver el número de ubicación.
        Dim vResultadoPostWhseReceipt As String = ""
        Dim vResultadoPutAwayCreate As String = ""

        Dim vDocumentoUbicacion As String = ""
        Dim vRecepcionAlmacen As String = ""
        Dim vTipoPush As String = "Push_Recepcion_Pedido_Compra_To_NAV_For_BYB"

        Dim Actualizo_Cantidad_En_Documento = False

        Push_Recepcion_Pedido_Compra_To_NAV_For_BYB = False

        Try
            clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(vDocumentoUbicacion, vRecepcionAlmacen, vTipoPush, "", IdRecepcionEnc, IdRecepcionDet, pIdUsuario)
        Catch ex As Exception
            Throw New Exception("No se pudo guardar la transaccion de push " + ex.Message)
        End Try

        Dim vUrlCodeUnit As String = My.Settings.WSHHRN_wsBYBNavCUWMS_CUWMS

        Dim ws2 As New CUWMS() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlCodeUnit
        }

        Dim vUrlRecepcionAlmacen As String = My.Settings.WSHHRN_WSRecepAlm_Recep_Almacen_Service

        Dim ws3 As New Recep_Almacen_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlRecepcionAlmacen
        }

        Dim vUrlPaginaLotes As String = My.Settings.WSHHRN_WSPaginaLotes_Pagina_lotes_Service

        Dim ws4 As New Pagina_lotes_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlPaginaLotes
        }

        Dim vUrlLotePedidoCompra As String = My.Settings.WSHHRN_WSLotePedidoCompra_Lote_PedidoCompra

        Dim wsLotePedidoCompra As New Lote_PedidoCompra With
        {
        .UseDefaultCredentials = False,
        .Credentials = CredencialesConexion,
        .Url = vUrlLotePedidoCompra
        }

        Dim vUrlPedidoCompra As String = My.Settings.WSHHRN_WSPedidosCompraNAV_Pedidos_Compra_Service

        Dim wsPedidoCompraService As New Pedidos_Compra_Service() With
            {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlPedidoCompra
            }

        Dim NavPedCompra As Pedidos_Compra = wsPedidoCompraService.Read(DocumentoIngreso)
        Dim vCantidadBasePedidoCompra As Double = 0

        If Not NavPedCompra Is Nothing Then
            If Not NavPedCompra.PurchLines Is Nothing Then
                vCantidadBasePedidoCompra = NavPedCompra.PurchLines.Where(Function(x) x.Line_No = NoLinea).FirstOrDefault.Quantity
            End If
        End If

        Try

            Dim RecepcionAlmacen As New Recep_Almacen()
            RecepcionAlmacen = ws3.Read(DocumentoRecepcion)

            If Not RecepcionAlmacen Is Nothing Then

                '#EJC20210324: Modificar cantidad a tomar/colocar si es diferente.
                For Each Lu As Whse_Receipt_Line In RecepcionAlmacen.WhseReceiptLines.Where(Function(x) x.Item_No = CodigoProducto _
                                                                                            AndAlso x.Unit_of_Measure_Code = NomUnidadMedida _
                                                                                            AndAlso x.Line_No = NoLinea)
                    If Lu.Qty_to_Receive = 0 Then
                        Lu.Qty_to_Receive = Cantidad
                        Actualizo_Cantidad_En_Documento = True
                    Else
                        If Not (Lu.Qty_to_Receive = Cantidad) Then
                            If vCantidadBasePedidoCompra = Lu.Qty_Base Then
                                If Cantidad <= Lu.Qty_Base Then
                                    Lu.Qty_to_Receive += Cantidad
                                    Actualizo_Cantidad_En_Documento = True
                                Else
                                    Dim vMensaje As String = String.Format("No puede recibir más producto del indicado en la línea del documento de ingreso. Cantidad_DI:{0} Cantidad_RE:{1} ", vCantidadBasePedidoCompra, Cantidad)
                                    Throw New Exception(vMensaje)
                                End If
                            ElseIf Lu.Qty_Received = 0 Then
                                Lu.Qty_to_Receive += Cantidad
                                Actualizo_Cantidad_En_Documento = True
                            End If
                        Else
                            '#EJC20220228:Ya tiene recibido.
                            If Cantidad <= Lu.Qty_Base Then
                                Lu.Qty_to_Receive += Cantidad
                                Actualizo_Cantidad_En_Documento = True
                            Else
                                Dim vMensaje As String = String.Format("No puede recibir más producto del indicado en la línea del documento de ingreso. Cantidad_DI:{0} Cantidad_RE:{1} ", vCantidadBasePedidoCompra, Cantidad)
                                Throw New Exception(vMensaje)
                            End If
                        End If
                    End If

                Next

            Else
                Throw New Exception("No se pudo leer la recepción desde el servicio de NAV #EJC202111111353.")
            End If

            If Actualizo_Cantidad_En_Documento Then

                '#EJC30330407:No insertar lote si vacío.
                If Not NoLote = "" Then

                    '#EJC20180503: Enviar siempre UMBAS en Enviar_Lotes_Ingreso.
                    wsLotePedidoCompra.LoteLineaPedidoCompra(DocumentoIngreso,
                                                             NoLinea,
                                                             CodigoProducto,
                                                             "",
                                                             NomUnidadMedida,
                                                             Cantidad,
                                                             NoLote,
                                                             FechaVence)

                End If


                '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
                ws3.Update(RecepcionAlmacen)

                Dim BeINavTransaccionesPush As New clsBeI_nav_transacciones_push

                BeINavTransaccionesPush.IdRecepcionEnc = IdRecepcionEnc
                BeINavTransaccionesPush.IdRecepcionDet = IdRecepcionDet
                clsLnI_nav_transacciones_push.GetSingle_By_RecepcionDet(BeINavTransaccionesPush)

                If Not BeINavTransaccionesPush Is Nothing Then
                    BeINavTransaccionesPush.IdTransaccionPush = BeINavTransaccionesPush.IdTransaccionPush
                    BeINavTransaccionesPush.Enviado_A_ERP = 1
                    BeINavTransaccionesPush.Fec_mod = Now.Date

                    clsLnI_nav_transacciones_push.Actualizar_Bandera_Enviado(BeINavTransaccionesPush)
                End If

                Push_Recepcion_Pedido_Compra_To_NAV_For_BYB = True

            Else
                Throw New Exception("ERROR_20220308_1657: No se pudo registrar el lote en NAV.")
            End If

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, pIdUsuario, ex.StackTrace, IdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            Dim cantReg As Integer
            pRespuesta = Mensaje

            WriteErrorToEventLog(Mensaje)

            '#CKFK20220121 Aquí vamos a insertar el registro que no se comunicó en la tabla i_nav_transacciones_push
            cantReg = clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(vDocumentoUbicacion,
                                                                                  vRecepcionAlmacen,
                                                                                  vTipoPush,
                                                                                  Mensaje,
                                                                                  IdRecepcionEnc,
                                                                                  IdRecepcionDet,
                                                                                  pIdUsuario)

            If cantReg = 0 Then

                If Not mArch Is Nothing Then

                    If mArch.Tipo = "WM" Then
                        Throw New Exception(Mensaje)
                    Else
                        Dim currrentContext As HttpContext = HttpContext.Current
                        Dim DT As New DataTable("CustomError")
                        DT.Columns.Add("Error", GetType(String))
                        DT.Rows.Add(Mensaje)
                        Dim sw As New StringWriter()
                        DT.WriteXml(sw)
                        HttpContext.Current.Response.Clear()
                        HttpContext.Current.Response.StatusCode = 299
                        HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                        HttpContext.Current.Response.Output.Write(sw.ToString())
                        HttpContext.Current.Response.ContentType = "text/xml"
                        HttpContext.Current.Response.End()
                    End If

                End If

            Else

                Return False

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB(ByVal DocumentoIngreso As String,
                                                                   ByVal DocumentoRecepcion As String,
                                                                   ByVal NoLinea As Integer,
                                                                   ByVal CodigoProducto As String,
                                                                   ByVal Cantidad As Double,
                                                                   ByVal NoLote As String,
                                                                   ByVal FechaVence As Date,
                                                                   ByVal NomUnidadMedida As String,
                                                                   ByVal IdRecepcionEnc As Integer,
                                                                   ByVal IdRecepcionDet As Integer,
                                                                   ByVal pIdUsuario As Integer,
                                                                   ByRef pRespuesta As String) As Boolean

        '#EJC20210428: Debería devolver el número de ubicación.
        Dim vResultadoPostWhseReceipt As String = ""
        Dim vResultadoPutAwayCreate As String = ""

        Dim vDocumentoUbicacion As String = ""
        Dim vRecepcionAlmacen As String = ""
        Dim vTipoPush As String = "Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB"

        Push_Recepcion_Devolucion_Venta_To_NAV_For_BYB = False

        Try
            clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(vDocumentoUbicacion, vRecepcionAlmacen, vTipoPush, "", IdRecepcionEnc, IdRecepcionDet, pIdUsuario)
        Catch ex As Exception
            Throw New Exception("No se pudo guardar la transaccion de push " + ex.Message)
        End Try

        Dim vUrlCodeUnit As String = My.Settings.WSHHRN_wsBYBNavCUWMS_CUWMS

        Dim ws2 As New CUWMS() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlCodeUnit
        }

        Dim vUrlRecepcionAlmacen As String = My.Settings.WSHHRN_WSRecepAlm_Recep_Almacen_Service

        Dim ws3 As New Recep_Almacen_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlRecepcionAlmacen
        }

        Dim vUrlDevolucionVenta As String = My.Settings.WSHHRN_WSDevolucionVentaNAV_Devolucion_venta_Service

        Dim wsDevolucionVentaService As New Devolucion_venta_Service() With
            {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlDevolucionVenta
            }

        Try

            Dim NavDevolucionVenta As New Devolucion_venta
            NavDevolucionVenta = wsDevolucionVentaService.Read(DocumentoIngreso)
            Dim vCantidadBasePedidoDevolucionVenta As Double = 0
            Dim Actualizo_Cantidad_En_Documento As Boolean = False
            Dim vNoLineaDevolucionVenta As Integer = 0

            If Not NavDevolucionVenta Is Nothing Then

                If Not NavDevolucionVenta.SalesLines Is Nothing Then

                    vCantidadBasePedidoDevolucionVenta = NavDevolucionVenta.SalesLines.Where(Function(x) x.Line_No = NoLinea).FirstOrDefault.Quantity
                    vNoLineaDevolucionVenta = NavDevolucionVenta.SalesLines.Where(Function(x) x.Line_No = NoLinea).FirstOrDefault.Line_No

                    Dim RecepcionAlmacen As New Recep_Almacen()
                    RecepcionAlmacen = ws3.Read(DocumentoRecepcion)

                    If Not RecepcionAlmacen Is Nothing Then
                        '#EJC20210324: Modificar cantidad a tomar/colocar si es diferente.
                        For Each Lu As Whse_Receipt_Line In RecepcionAlmacen.WhseReceiptLines.Where(Function(x) x.Item_No = CodigoProducto AndAlso
                                                                                                        x.Source_Line_No = vNoLineaDevolucionVenta)

                            If Lu.Qty_to_Receive = 0 Then
                                Lu.Qty_to_Receive = Cantidad
                                Actualizo_Cantidad_En_Documento = True
                            Else
                                If Not (Lu.Qty_to_Receive = Cantidad) Then
                                    If vCantidadBasePedidoDevolucionVenta = Lu.Qty_Base Then
                                        If Cantidad <= Lu.Qty_Base Then
                                            Lu.Qty_to_Receive += Cantidad
                                            Actualizo_Cantidad_En_Documento = True
                                        Else
                                            Dim vMensaje As String = String.Format("No puede recibir más producto del indicado en la línea del documento de ingreso. Cantidad_DI:{0} Cantidad_RE:{1} ",
                                                                                   vCantidadBasePedidoDevolucionVenta,
                                                                                   Cantidad)
                                            Throw New Exception(vMensaje)
                                        End If
                                    ElseIf Lu.Qty_Received = 0 Then
                                        Lu.Qty_to_Receive += Cantidad
                                        Actualizo_Cantidad_En_Documento = True
                                    End If
                                Else
                                    '#EJC20220228:Ya tiene recibido.
                                    If Cantidad <= Lu.Qty_Base Then
                                        Lu.Qty_to_Receive += Cantidad
                                        Actualizo_Cantidad_En_Documento = True
                                    Else
                                        Dim vMensaje As String = String.Format("No puede recibir más producto del indicado en la línea del documento de ingreso. Cantidad_DI:{0} Cantidad_RE:{1} ", vCantidadBasePedidoDevolucionVenta, Cantidad)
                                        Throw New Exception(vMensaje)
                                    End If
                                End If
                            End If

                        Next

                        '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
                        ws3.Update(RecepcionAlmacen)

                        Dim BeINavTransaccionesPush As New clsBeI_nav_transacciones_push

                        BeINavTransaccionesPush.IdRecepcionEnc = IdRecepcionEnc
                        BeINavTransaccionesPush.IdRecepcionDet = IdRecepcionDet
                        clsLnI_nav_transacciones_push.GetSingle_By_RecepcionDet(BeINavTransaccionesPush)

                        If Not BeINavTransaccionesPush Is Nothing Then
                            BeINavTransaccionesPush.IdTransaccionPush = BeINavTransaccionesPush.IdTransaccionPush
                            BeINavTransaccionesPush.Enviado_A_ERP = 1
                            BeINavTransaccionesPush.Fec_mod = Now.Date

                            clsLnI_nav_transacciones_push.Actualizar_Bandera_Enviado(BeINavTransaccionesPush)
                        End If

                    Else
                        Throw New Exception("Error NAV - No se pudo leer la recepción desde el servicio de NAV #EJC202111111351.")
                    End If

                Else
                    Throw New Exception("Error NAV - No se pudo realizar el registro no hay líneas para registrar")
                End If

            Else
                Throw New Exception("Error NAV - No se pudo realizar el registro no hay devolución para registrar")
            End If


        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, pIdUsuario, ex.StackTrace, IdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            Dim cantReg As Integer
            pRespuesta = Mensaje

            WriteErrorToEventLog(Mensaje)

            '#CKFK20220121 Aquí vamos a insertar el registro que no se comunicó en la tabla i_nav_transacciones_push
            cantReg = clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(vDocumentoUbicacion,
                                                                                  vRecepcionAlmacen,
                                                                                  vTipoPush,
                                                                                  Mensaje,
                                                                                  IdRecepcionEnc,
                                                                                  IdRecepcionDet,
                                                                                  pIdUsuario)

            If cantReg = 0 Then

                If Not mArch Is Nothing Then

                    If mArch.Tipo = "WM" Then
                        Throw New Exception(Mensaje)
                    Else
                        Dim currrentContext As HttpContext = HttpContext.Current
                        Dim DT As New DataTable("CustomError")
                        DT.Columns.Add("Error", GetType(String))
                        DT.Rows.Add(Mensaje)
                        Dim sw As New StringWriter()
                        DT.WriteXml(sw)
                        HttpContext.Current.Response.Clear()
                        HttpContext.Current.Response.StatusCode = 299
                        HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                        HttpContext.Current.Response.Output.Write(sw.ToString())
                        HttpContext.Current.Response.ContentType = "text/xml"
                        HttpContext.Current.Response.End()
                    End If

                End If

            Else

                Return False

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Push_Recepcion_Transferencias_Ingreso_To_NAV_For_BYB(ByVal DocumentoIngreso As String,
                                                                         ByVal DocumentoRecepcion As String,
                                                                         ByVal NoLinea As Integer,
                                                                         ByVal CodigoProducto As String,
                                                                         ByVal Cantidad As Double,
                                                                         ByVal NoLote As String,
                                                                         ByVal FechaVence As Date,
                                                                         ByVal NomUnidadMedida As String,
                                                                         ByVal IdRecepcionEnc As Integer,
                                                                         ByVal IdRecepcionDet As Integer,
                                                                         ByVal pIdUsuario As Integer,
                                                                         ByRef pRespuesta As String) As Boolean

        '#EJC20210428: Debería devolver el número de ubicación.
        Dim vResultadoPostWhseReceipt As String = ""
        Dim vResultadoPutAwayCreate As String = ""
        Dim Actualizo_Cantidad_En_Documento As Boolean = False
        Dim vDocumentoUbicacion As String = ""
        Dim vRecepcionAlmacen As String = ""
        Dim vTipoPush As String = "Push_Recepcion_Transferencias_Ingreso_To_NAV_For_BYB"

        Push_Recepcion_Transferencias_Ingreso_To_NAV_For_BYB = False


        Try
            clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(vDocumentoUbicacion, vRecepcionAlmacen, vTipoPush, "", IdRecepcionEnc, IdRecepcionDet, pIdUsuario)
        Catch ex As Exception
            Throw New Exception("No se pudo guardar la transaccion de push " + ex.Message)
        End Try

        Dim vUrlCodeUnit As String = My.Settings.WSHHRN_wsBYBNavCUWMS_CUWMS

        Dim ws2 As New CUWMS() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlCodeUnit
        }

        Dim vUrlRecepcionAlmacen As String = My.Settings.WSHHRN_WSRecepAlm_Recep_Almacen_Service

        Dim ws3 As New Recep_Almacen_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlRecepcionAlmacen
        }

        Dim vUrlPaginaLotes As String = My.Settings.WSHHRN_WSPaginaLotes_Pagina_lotes_Service

        Dim ws4 As New Pagina_lotes_Service() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlPaginaLotes
        }

        Dim vUrlLotePedidoCompra As String = My.Settings.WSHHRN_WSLotePedidoCompra_Lote_PedidoCompra

        Dim wsLotePedidoCompra As New Lote_PedidoCompra With
        {
        .UseDefaultCredentials = False,
        .Credentials = CredencialesConexion,
        .Url = vUrlLotePedidoCompra
        }

        Dim vUrlTransferenciaIngreso As String = My.Settings.WSHHRN_wsTransferenciaIngreso_Pedidos_Transferencia_Service

        Dim wsTransferenciaIngresoService As New Pedidos_Transferencia_Service() With
            {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrlTransferenciaIngreso
            }

        Dim NavDevolucionVenta As New Pedidos_Transferencia
        Dim NavPedTransferencia = wsTransferenciaIngresoService.Read(DocumentoIngreso)
        Dim vCantidadTransferenciaIngreso As Double = 0

        If Not NavPedTransferencia Is Nothing Then
            If Not NavPedTransferencia.TransferLines Is Nothing Then
                vCantidadTransferenciaIngreso = NavPedTransferencia.TransferLines.Where(Function(x) x.Line_No = NoLinea).FirstOrDefault.Quantity
            End If
        End If

        Try

            Dim RecepcionAlmacen As New Recep_Almacen()
            RecepcionAlmacen = ws3.Read(DocumentoRecepcion)

            If Not RecepcionAlmacen Is Nothing Then

                '#CKFK 20211116 Actualizar la fecha del día para el registro
                RecepcionAlmacen.Posting_Date = Now.Date

                '#EJC20210324: Modificar cantidad a tomar/colocar si es diferente.
                For Each Lu As Whse_Receipt_Line In RecepcionAlmacen.WhseReceiptLines.Where(Function(x) x.Item_No = CodigoProducto And
                                                                                                        x.Line_No = NoLinea)

                    If Lu.Qty_to_Receive = 0 Then
                        Lu.Qty_to_Receive = Cantidad
                        Actualizo_Cantidad_En_Documento = True
                    Else
                        If Not (Lu.Qty_to_Receive = Cantidad) Then
                            If vCantidadTransferenciaIngreso = Lu.Qty_Base Then
                                If Cantidad <= Lu.Qty_Base Then
                                    Lu.Qty_to_Receive += Cantidad
                                    Actualizo_Cantidad_En_Documento = True
                                Else
                                    Dim vMensaje As String = String.Format("No puede recibir más producto del indicado en la línea del documento de ingreso. Cantidad_DI:{0} Cantidad_RE:{1} ", vCantidadTransferenciaIngreso, Cantidad)
                                    Throw New Exception(vMensaje)
                                End If
                            ElseIf Lu.Qty_Received = 0 Then
                                Lu.Qty_to_Receive += Cantidad
                                Actualizo_Cantidad_En_Documento = True
                            End If
                        Else
                            '#EJC20220228:Ya tiene recibido.
                            If Cantidad <= Lu.Qty_Base Then
                                Lu.Qty_to_Receive += Cantidad
                                Actualizo_Cantidad_En_Documento = True
                            Else
                                Dim vMensaje As String = String.Format("No puede recibir más producto del indicado en la línea del documento de ingreso. Cantidad_DI:{0} Cantidad_RE:{1} ", vCantidadTransferenciaIngreso, Cantidad)
                                Throw New Exception(vMensaje)
                            End If
                        End If
                    End If

                    '#EJC20220405:En revisión con Ricardo, se debe sumar a la recepción la cantidad.
                    'If Not (Lu.Qty_Received = Cantidad) Then
                    '    If vCantidadTransferenciaIngreso = Lu.Quantity Then
                    '        If Cantidad <= Lu.Quantity Then
                    '            Lu.Qty_to_Receive += Cantidad
                    '        Else
                    '            Dim vMensaje As String = String.Format("No puede recibir más producto del indicado en la línea del documento de ingreso. Cantidad_DI:{0} Cantidad_RE:{1} ", vCantidadTransferenciaIngreso, Cantidad)
                    '            Throw New Exception(vMensaje)
                    '        End If
                    '    ElseIf Lu.Qty_Received = 0 Then
                    '        Lu.Qty_to_Receive += Cantidad
                    '    End If
                    'End If
                Next
            Else
                Throw New Exception("No se pudo leer la recepción desde el servicio de NAV #EJC202111111353.")
            End If


            'Aquí  no vamos a enviar los lotes
            ''#EJC20180503: Enviar siempre UMBAS en Enviar_Lotes_Ingreso.
            'wsLotePedidoCompra.LoteLineaPedidoCompra(DocumentoIngreso,
            '                                         NoLinea,
            '                                         CodigoProducto,
            '                                         "",
            '                                         NomUnidadMedida,
            '                                         Cantidad,
            '                                         NoLote,
            '                                         FechaVence)

            If Actualizo_Cantidad_En_Documento Then

                '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
                ws3.Update(RecepcionAlmacen)

                '#EJC20220224_0332AM: No realizar hasta finalizar la recepción en HH.
                'Registrar_Pedido_Compra_To_NAV_For_BYB(DocumentoIngreso, DocumentoRecepcion, RecepcionAlmacen)

                Dim BeINavTransaccionesPush As New clsBeI_nav_transacciones_push

                BeINavTransaccionesPush.IdRecepcionEnc = IdRecepcionEnc
                BeINavTransaccionesPush.IdRecepcionDet = IdRecepcionDet
                clsLnI_nav_transacciones_push.GetSingle_By_RecepcionDet(BeINavTransaccionesPush)

                If Not BeINavTransaccionesPush Is Nothing Then
                    BeINavTransaccionesPush.IdTransaccionPush = BeINavTransaccionesPush.IdTransaccionPush
                    BeINavTransaccionesPush.Enviado_A_ERP = 1
                    BeINavTransaccionesPush.Fec_mod = Now.Date

                    clsLnI_nav_transacciones_push.Actualizar_Bandera_Enviado(BeINavTransaccionesPush)
                End If

                Push_Recepcion_Transferencias_Ingreso_To_NAV_For_BYB = True

            End If

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, IdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            Dim cantReg As Integer
            pRespuesta = Mensaje

            WriteErrorToEventLog(Mensaje)

            '#CKFK20220121 Aquí vamos a insertar el registro que no se comunicó en la tabla i_nav_transacciones_push
            cantReg = clsLnI_nav_transacciones_push.Guardar_Transaccion_Existente(vDocumentoUbicacion,
                                                                                  vRecepcionAlmacen,
                                                                                  vTipoPush,
                                                                                  Mensaje,
                                                                                  IdRecepcionEnc,
                                                                                  IdRecepcionDet,
                                                                                  pIdUsuario)

            If cantReg = 0 Then

                If Not mArch Is Nothing Then

                    If mArch.Tipo = "WM" Then
                        Throw New Exception(Mensaje)
                    Else
                        Dim currrentContext As HttpContext = HttpContext.Current
                        Dim DT As New DataTable("CustomError")
                        DT.Columns.Add("Error", GetType(String))
                        DT.Rows.Add(Mensaje)
                        Dim sw As New StringWriter()
                        DT.WriteXml(sw)
                        HttpContext.Current.Response.Clear()
                        HttpContext.Current.Response.StatusCode = 299
                        HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                        HttpContext.Current.Response.Output.Write(sw.ToString())
                        HttpContext.Current.Response.ContentType = "text/xml"
                        HttpContext.Current.Response.End()
                    End If

                End If

            Else

                Return False

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Registrar_Pedido_Compra_To_NAV_For_BYB(ByVal IdOrdenCompraEnc As Integer,
                                                           ByVal Referencia As String,
                                                           ByVal No_Documento_Recepcion_ERP As String) As Boolean

        Registrar_Pedido_Compra_To_NAV_For_BYB = False

        Try


            Dim vResultadoPostWhseReceipt As String = ""
            Dim vResultadoPutAwayCreate As String = ""
            Dim vUrlRecepcionAlmacen As String = My.Settings.WSHHRN_WSRecepAlm_Recep_Almacen_Service
            Dim vUrlCodeUnit As String = My.Settings.WSHHRN_wsBYBNavCUWMS_CUWMS

            Dim DocumentoIngreso As String = Referencia
            Dim DocumentoRecepcion As String = No_Documento_Recepcion_ERP

            Dim ws2 As New CUWMS() With
            {
                .UseDefaultCredentials = False,
                .Credentials = CredencialesConexion,
                .Url = vUrlCodeUnit
            }

            Dim ws3 As New Recep_Almacen_Service() With
            {
                .UseDefaultCredentials = False,
                .Credentials = CredencialesConexion,
                .Url = vUrlRecepcionAlmacen
            }

            Dim RecepcionAlmacen As New Recep_Almacen()
            RecepcionAlmacen = ws3.Read(DocumentoRecepcion)

            If Not RecepcionAlmacen Is Nothing Then

                '#EJC20220407:Ricardo indicó enviar esto para evitar, esto: Exception: Posting Date is not within your range of allowed posting dates 
                RecepcionAlmacen.Posting_Date = Now
                ws3.Update(RecepcionAlmacen)

                '#EJC20210527: Para la recepción de compra, devuelve el número de documento de ubicación.
                ws2.PostWhseReceipt(RecepcionAlmacen.No,
                                    vResultadoPostWhseReceipt,
                                    DocumentoIngreso)

                If vResultadoPostWhseReceipt.StartsWith("UA") Then

                    clsLnTrans_oc_enc.Actualizar_No_Documento_Ubicacion_ERP(vResultadoPostWhseReceipt,
                                                                            IdOrdenCompraEnc)

                    ws2.RegisterPutAway(vResultadoPostWhseReceipt,
                                        vResultadoPutAwayCreate)

                    If vResultadoPutAwayCreate = "Successfully Created" Then

                        Registrar_Pedido_Compra_To_NAV_For_BYB = True

                        clsLnTrans_oc_enc.Actualizar_PutAway_Registrado(IdOrdenCompraEnc)

                    Else
                        Throw New Exception(vResultadoPutAwayCreate)
                    End If

                Else
                    Throw New Exception(vResultadoPostWhseReceipt)
                End If


                '#EJC202212151638: Volver a colocar QTY_To_Recieve en 0 después de registrar el documento.
                RecepcionAlmacen = New Recep_Almacen()
                RecepcionAlmacen = ws3.Read(DocumentoRecepcion)

                If Not RecepcionAlmacen Is Nothing Then

                    If Not RecepcionAlmacen.WhseReceiptLines Is Nothing Then

                        '#EJC20210324: Modificar cantidad a tomar/colocar 0, para que se pueda recibir parcial en HH.
                        For Each Lu As Whse_Receipt_Line In RecepcionAlmacen.WhseReceiptLines
                            Lu.Qty_to_Receive = 0
                        Next

                        '#EJC20210412: Actualizar la cantidad registrada en la HH en NAV.
                        ws3.Update(RecepcionAlmacen)

                    End If

                End If

            Else
                Throw New Exception("Error_20220228: No se pudo leer la recepción de NAV:" & DocumentoRecepcion)
            End If


        Catch ex As Exception

            'Throw ex            

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            Try
                '#MECR21102025: Se agrego bitacora de logs para ordenes de compra
                'clsLnLog_error_wms.Agregar_Error(vMsgError)
                clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, pStackTrace:=ex.StackTrace, pIdOCEnc:=IdOrdenCompraEnc)
            Catch ex1 As Exception
                Debug.Print(ex1.Message)
            End Try

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If Not mArch Is Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Borrar_Picking(ByVal Envio_Almacen As String) As Boolean

        Dim vResultadoBorrar_Picking As String = ""

        Dim vEnvio_Almacen As String = Envio_Almacen

        Borrar_Picking = False

        Dim vUrl As String = My.Settings.WSHHRN_wsBYBNavCUWMS_CUWMS

        Dim ws2 As New CUWMS() With
        {
            .UseDefaultCredentials = False,
            .Credentials = CredencialesConexion,
            .Url = vUrl
        }

        Try

            ws2.BorraPicking(vEnvio_Almacen)

            'If vResultadoPutAwayCreate = "Successfully Created" Then
            '    pRespuesta = vResultadoPutAwayCreate
            '    Push_Recepcion_Produccion_To_NAV_For_BYB = True
            'Else
            '    Throw New Exception(vResultadoPutAwayCreate)
            'End If

            Borrar_Picking = True

        Catch ex As Exception

            Dim Mensaje As String = ex.Message

            WriteErrorToEventLog(Mensaje)

            If Not mArch Is Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Android_Get_All_Versiones() As List(Of clsBeVersion_wms_hh_and)

        Android_Get_All_Versiones = Nothing

        Try

            Dim lEmpresas As New List(Of clsBeVersion_wms_hh_and)
            lEmpresas = clsLnVersion_wms_hh.Android_Get_All()
            Return lEmpresas

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Resoluciones_Lp_By_IdOperador_And_IdBodega(ByVal pIdOperador As Integer,
                                                                   ByVal pIdBodega As Integer) As clsBeResolucion_lp_operador

        Get_Resoluciones_Lp_By_IdOperador_And_IdBodega = Nothing

        Try

            Dim lResolucionesLP As New clsBeResolucion_lp_operador()
            lResolucionesLP = clsLnResolucion_lp_operador.Get_Resolucion_By_IdOperador_And_IdBodega(pIdOperador,
                                                                                                    pIdBodega)
            Return lResolucionesLP

        Catch ex As Exception

            '#MECR12112025: Se agrego bitacora de packing
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdOperador:=pIdOperador, pIdBodega:=pIdBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    ' Packing
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Picking_Para_Empaque_By_IdBodega(ByVal pIdBodega As Integer) As List(Of clsBeTrans_picking_enc)

        Get_All_Picking_Para_Empaque_By_IdBodega = Nothing

        Try

            Return clsLnTarea_hh.Get_All_Picking_Para_Empaque_By_IdBodega(pIdBodega)

        Catch ex As Exception

            '#MECR12112025: Se agrego bitacora para logs de packing
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            ' clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_PickingUbic_By_PickingEnc(ByVal pIdPickingEnc As Integer, ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_By_PickingEnc = Nothing

        Try

            Return clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingEnc(pIdPickingEnc, pIdPedidoEnc)

        Catch ex As Exception

            '#MECR12112025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc, pIdPedidoEnc:=pIdPedidoEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_PickingUbic_By_PickingEnc_Group(ByVal pIdPickingEnc As Integer) As List(Of clsBeTrans_picking_ubic)

        Get_All_PickingUbic_By_PickingEnc_Group = Nothing

        Try

            Return clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingEnc_Group(pIdPickingEnc)

        Catch ex As Exception

            '#MECR12112025: Se agrego bitacora de packing
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Inserta_Packing(ByVal pTrans_packing_enc As List(Of clsBeTrans_packing_enc), pIdResolucion As Integer) As Integer

        Inserta_Packing = 0

        Try

            Return clsLnTrans_packing_enc.Inserta_Packing(pTrans_packing_enc, pIdResolucion)

        Catch ex As Exception

            '#MECR12112025: Se agrego bitacora de packing
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdBodega:=pTrans_packing_enc.FirstOrDefault().Idbodega,
                                                  pIdOperador:=pTrans_packing_enc.FirstOrDefault.Idoperadorbodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_Estado_Packing(ByVal pIdPicking As Integer,
                                              ByVal pIdPedidoEnc As Integer) As Integer

        Actualizar_Estado_Packing = 0

        Try

            Return clsLnTrans_picking_enc.Set_Estado_Finalizado_Packing(pIdPicking, pIdPedidoEnc)

        Catch ex As Exception

            '#MECR12112025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPicking, pIdPedidoEnc:=pIdPedidoEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Eliminar_Linea_Packing(ByVal pBePackingEnc As clsBeTrans_packing_enc,
                                           ByVal pIdOperadorBodega As Integer) As Boolean

        Eliminar_Linea_Packing = False

        Try

            Return clsLnTrans_packing_enc.Eliminar_Linea_Packing(pBePackingEnc, pIdOperadorBodega)

        Catch ex As Exception

            '#MECR12112025: Se agrego bitacora de packing
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError,
                                                  pStackTrace:=ex.StackTrace,
                                                  pIdOperador:=pIdOperadorBodega,
                                                  pIdBodega:=pBePackingEnc.Idbodega,
                                                  pIdPedidoEnc:=pBePackingEnc.IdPedidoEnc,
                                                  pIdPickingEnc:=pBePackingEnc.Idpickingenc,
                                                  pIdDespachoEnc:=pBePackingEnc.IdDespachoEnc,
                                                  pIdProductoBodega:=pBePackingEnc.Idproductobodega,
                                                  pIdProductoEstado:=pBePackingEnc.Idproductoestado,
                                                  pIdPresentacion:=pBePackingEnc.Idpresentacion,
                                                  pIdUnidadMedida:=pBePackingEnc.Idunidadmedida,
                                                  pLic_Plate:=pBePackingEnc.Lic_plate,
                                                  pCantidad_Bultos_Packing:=pBePackingEnc.Cantidad_bultos_packing,
                                                  pUsuario_agr:=pBePackingEnc.Usr_mod)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Packing_By_IdPicking(ByVal IdPicking As Integer,
                                                 ByVal IdPedidoEnc As Integer) As List(Of clsBeTrans_packing_enc)

        Get_All_Packing_By_IdPicking = Nothing

        Try

            Return clsLnTrans_packing_enc.Get_All_By_IdPicking(IdPicking, True, IdPedidoEnc)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=IdPicking, pIdPedidoEnc:=IdPedidoEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Set_Estado_Pendiente_Packing(ByVal pIdPickingEnc As Integer) As Integer

        Set_Estado_Pendiente_Packing = 0


        Try

            Return clsLnTrans_picking_enc.Set_Estado_Pendiente_Packing(pIdPickingEnc)

        Catch ex As Exception

            '#MECR12112025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Disponibilidad_Ubicacion_Destino(ByVal pIdBodega As Integer, ByVal pIdUbicacion As Integer) As Double

        Get_Disponibilidad_Ubicacion_Destino = 0

        Try


            Get_Disponibilidad_Ubicacion_Destino = clsLnVW_stock_res.Get_Disponibilidad_Ubicacion_By_IdBodega_And_IdUbicacion(pIdBodega, pIdUbicacion)


        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    ''' <summary>
    ''' #EJC20220330: Validar si la ubicación de recepción escaneada es válida (CEALSA)-
    ''' </summary>
    ''' <param name="pBarra"></param>
    ''' <param name="pIdBodega"></param>
    ''' <returns></returns>
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Ubicacion_Recepcion_By_Codigo_Barra_And_IdBodega(ByVal pBarra As String, ByVal pIdBodega As Integer) As clsBeBodega_ubicacion

        Get_Ubicacion_Recepcion_By_Codigo_Barra_And_IdBodega = Nothing

        Try

            Return clsLnBodega_ubicacion.Get_Ubicacion_Recepcion_By_Codigo_Barra_And_IdBodega(pBarra, pIdBodega)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, pIdBodega, 0, ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    ''' <summary>
    ''' #EJC20220404:BYB Filtrar por licencia de ingreso de producción.
    ''' </summary>
    ''' <param name="pLicenciaIngreso"></param>
    ''' <returns></returns>
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_IdOrdenCompraEnc_By_Licencia(ByVal pLicenciaIngreso As String) As Integer

        Get_IdOrdenCompraEnc_By_Licencia = -1

        Try

            Dim BeTransOcDetLote As New clsBeTrans_oc_det_lote()
            BeTransOcDetLote = clsLnTrans_oc_det_lote.Get_Single_By_Licencia(pLicenciaIngreso)

            If Not BeTransOcDetLote Is Nothing Then
                Get_IdOrdenCompraEnc_By_Licencia = BeTransOcDetLote.IdOrdenCompraEnc
            End If

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    ''' <summary>
    ''' #CKFK20240603:Cumbre Filtrar por codigo de producto las ordenes de compra que lo contienen.
    ''' </summary>
    ''' <param name="pCodigo"></param>
    ''' <param name="pIdOperadorBodega"></param>
    ''' <returns></returns>
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_ListOrdenCompraEnc_By_Codigo_Producto(ByVal pCodigo As String,
                                                              ByVal pIdOperadorBodega As String) As DataTable

        Get_ListOrdenCompraEnc_By_Codigo_Producto = Nothing

        Try

            Dim Lista As New DataTable
            Lista.TableName = "ListaOC"
            Lista = clsLnTrans_oc_det.Get_ListOrdenCompraEnc_By_Codigo_Producto(pCodigo, pIdOperadorBodega)

            If Not Lista Is Nothing Then
                Get_ListOrdenCompraEnc_By_Codigo_Producto = Lista
            End If

        Catch ex As Exception

            '#MECR07102025: Se agrego nueva bitacora de logs para OC
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Existencia_NAV(ByVal pCodigoProducto As String, ByVal pLote As String) As Decimal

        Get_Existencia_NAV = 0

        Try

            Dim vUrl As String = My.Settings.WSHHRN_wsBYBNavCUWMS_CUWMS

            Dim ws2 As New CUWMS() With
            {
                .UseDefaultCredentials = False,
                .Credentials = CredencialesConexion,
                .Url = vUrl
            }

            Get_Existencia_NAV = ws2.InvPorLote(pCodigoProducto, pLote)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Inventario_Teorico_By_Codigo_O_Licencia(ByVal pIdInventario As Integer,
                                                                ByVal pCodigo As String,
                                                                ByVal pIdBodega As Integer) As List(Of clsBeTrans_inv_stock_prod)


        Get_Inventario_Teorico_By_Codigo_O_Licencia = Nothing

        Try

            Return clsLnTrans_inv_stock_prod.Get_All_By_Codigo_O_Licencia(pIdInventario, pCodigo, pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    'AT20220506 Agregué esta función para obtener unicamente la presentación relacionada a  una bodega y producto
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Presentaciones_By_IdProducto_IdBodega(ByVal pIdProducto As Integer, ByVal pIdBodega As Integer, ByVal pActivo As Boolean) As List(Of clsBeProducto_Presentacion)

        Get_All_Presentaciones_By_IdProducto_IdBodega = New List(Of clsBeProducto_Presentacion)

        Try

            Return clsLnProducto_presentacion.Get_All_By_IdProducto_And_IdBodega(pIdProducto, pIdBodega, pActivo)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Codigos_By_IdInventario_And_IdBodega(ByVal pIdInventario As Integer,
                                                                ByVal pIdBodega As Integer) As List(Of clsBeTrans_inv_stock_prod_sug)


        Get_All_Codigos_By_IdInventario_And_IdBodega = Nothing

        Try

            Return clsLnTrans_inv_stock_prod.Get_All_Codigos_By_IdInventario_And_IdBodega(pIdInventario, pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    'AT20220517 Obtiene el total de la cantidad de productos contados
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_CantidadInvConteo_By_Producto(ByVal pIdUbicacion As Integer,
                                                    ByVal pIdProducto As Integer,
                                                    ByVal pIdBodega As Integer,
                                                    ByVal pIdPresentacion As Integer) As clsBeTrans_inv_detalle


        Get_CantidadInvConteo_By_Producto = Nothing

        Try

            Return clsLnTrans_inv_detalle.Get_CantidadInvConteo_By_Producto(pIdUbicacion, pIdProducto, pIdBodega, pIdPresentacion)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    'AT20220516 Obtiene el total de la cantidad de productos verificados en inventario 
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_CantidadInvVer_By_Producto(ByVal pIdUbicacion As Integer,
                                                   ByVal pIdProducto As Integer,
                                                   ByVal pIdBodega As Integer,
                                                   ByVal pIdPresentacion As Integer,
                                                   ByVal pIdInventarioEnc As Integer) As clsBeTrans_inv_resumen


        Get_CantidadInvVer_By_Producto = Nothing

        Try
            Return clsLnTrans_inv_resumen.Get_CantidadInvVer_By_Producto(pIdUbicacion, pIdProducto, pIdBodega, pIdPresentacion, pIdInventarioEnc)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    'AT20240527 Obtiene inventario por licencia de Trans_inv_stock_prod
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_InventarioItem_By_Licencia(ByVal pLicencia As String,
                                                   pIdBodega As Integer,
                                                   pUbicacion As Integer) As clsBeTrans_inv_stock_prod


        Get_InventarioItem_By_Licencia = Nothing

        Try
            Return clsLnTrans_inv_stock_prod.Get_Inventario_By_Licencia(pLicencia, pIdBodega, pUbicacion)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Reservas_By_IdStock(ByVal pIdStock As Integer,
                                                ByVal pIdBodega As Integer,
                                                ByVal pIdPedido As Integer) As Boolean

        Dim BeListReservas As New List(Of clsBeStock_res)

        Get_All_Reservas_By_IdStock = False

        Try

            BeListReservas = clsLnStock_res.Get_All_Reservas_By_IdStock(pIdStock, pIdBodega, pIdPedido)

            Get_All_Reservas_By_IdStock = (BeListReservas.Count > 0)

        Catch ex As Exception

            '#MECR21102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdBodega:=pIdBodega, pIdPedidoEnc:=pIdPedido, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_Ubicaciones_Reservadas_By_IdStock(ByVal pIdStock As Integer,
                                                                 ByVal pIdBodega As Integer,
                                                                 ByVal pIdUbicacion As Integer,
                                                                 ByVal pIdOperador As Integer) As Integer

        Actualizar_Ubicaciones_Reservadas_By_IdStock = Nothing

        Try

            Return clsLnStock_res.Actualizar_Ubicacion_Stock_Reservado_By_IdStock(pIdStock,
                                                                                  pIdBodega,
                                                                                  pIdUbicacion,
                                                                                  pIdOperador)

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pIdStock:=pIdStock, pIdOperador:=pIdOperador)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function


    'AT20241009 Actualizar ubicaciones reservadas pallet mixto
    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_Ubicaciones_Reservadas_Pallet_Mixto(ByVal pLista As List(Of clsBeVW_stock_res),
                                                                 ByVal pIdBodega As Integer,
                                                                 ByVal pIdUbicacion As Integer,
                                                                 ByVal pIdOperador As Integer) As Integer

        Actualizar_Ubicaciones_Reservadas_Pallet_Mixto = 0

        Try


            For Each BeVWStockRes As clsBeVW_stock_res In pLista
                clsLnStock_res.Actualizar_Ubicacion_Stock_Reservado_By_IdStock(BeVWStockRes.IdStock,
                                                                               pIdBodega,
                                                                               pIdUbicacion,
                                                                               pIdOperador)

                Actualizar_Ubicaciones_Reservadas_Pallet_Mixto += 1
            Next

            Return Actualizar_Ubicaciones_Reservadas_Pallet_Mixto

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pIdOperador:=pIdOperador)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#AT20220917 Cuando IdStock = 0 en el cambio de ubicación de stock completamente reservado.
    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_Ubicaciones_Reservadas_By_StockRes(ByVal pBeStockRes As clsBeVW_stock_res,
                                                                  ByVal pIdBodega As Integer,
                                                                  ByVal pIdUbicacion As Integer,
                                                                  ByVal pIdOperador As Integer) As Integer

        Actualizar_Ubicaciones_Reservadas_By_StockRes = Nothing

        Try

            Return clsLnStock_res.Actualizar_Ubicacion_Stock_Reservado_By_StockRes(pBeStockRes,
                                                                                  pIdBodega,
                                                                                  pIdUbicacion,
                                                                                  pIdOperador)

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pIdOperador:=pIdOperador)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Existe_Conteo(ByVal pIdUbicacion As Integer, ByVal pIdBodega As Integer, ByVal pIdProducto As Integer) As List(Of clsBeTrans_inv_detalle)


        Existe_Conteo = Nothing

        Try

            Return clsLnTrans_inv_detalle.Get_Conteo_By_IdProducto_And_IdUbicacion(pIdUbicacion, pIdBodega, pIdProducto)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Existe_Verificacion(ByVal pIdUbicacion As Integer,
                                        ByVal pIdBodega As Integer,
                                        ByVal pIdProducto As Integer) As List(Of clsBeTrans_inv_resumen)


        Existe_Verificacion = Nothing

        Try

            Return clsLnTrans_inv_resumen.Get_Verificacion_By_IdUbicacion(pIdUbicacion, pIdBodega, pIdProducto)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Valida_Unico_Producto(ByVal pIdBodega As Integer,
                                          ByVal pIdUbicacion As Integer,
                                          ByVal pIdProducto As Integer) As List(Of clsBeTrans_inv_detalle)


        Valida_Unico_Producto = Nothing

        Try

            Return clsLnTrans_inv_detalle.Valida_Producto_ConteoInventario(pIdBodega, pIdUbicacion, pIdProducto)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKFK20220524 Función para crear una nueva línea de unidades para la trans_oc_det
    <WebMethod(), SoapHeader("mArch")>
    Public Function Crear_Linea_Unidades(ByVal BeTransOcDet As clsBeTrans_oc_det,
                                         ByVal pCantidadUnidades As Integer,
                                         ByVal pCantidadRecibida As Integer) As clsBeTrans_oc_det


        Crear_Linea_Unidades = Nothing

        Try

            'Return clsLnTrans_oc_det.Crear_Linea_Unidades(BeTransOcDet,
            'pCantidadUnidades,
            'pCantidadRecibida)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualizar_Estado_Picking_Andr(ByVal pEstadoPicking As String, ByVal pIdPickingEnc As Integer) As Integer

        Actualizar_Estado_Picking_Andr = 0

        Try

            Return clsLnTrans_picking_enc.Actualizar_Estado_Andr(pEstadoPicking, pIdPickingEnc)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Codigo_By_CodigoBarra(ByVal pCodigoBarra As String, ByVal pIdBodega As Integer) As clsBeProducto

        Get_Codigo_By_CodigoBarra = Nothing

        Try
            Get_Codigo_By_CodigoBarra = clsLnProducto.Get_Codigo_By_CodigoBarra(pCodigoBarra, pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Codigo_By_CodigoBarra_By_Picking(ByVal pCodigoBarra As String,
                                                         ByVal pIdBodega As Integer,
                                                         ByVal pIdPickingEnc As Integer) As clsBeProducto

        Get_Codigo_By_CodigoBarra_By_Picking = Nothing

        Try
            Get_Codigo_By_CodigoBarra_By_Picking = clsLnProducto.Get_Product_By_CodigoBarra_By_PickingEnc(pCodigoBarra, pIdBodega, pIdPickingEnc)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Lista_Codigos_By_CodigoBarra_By_Picking(ByVal pCodigoBarra As String,
                                                                ByVal pIdBodega As Integer,
                                                                ByVal pIdPickingEnc As Integer) As List(Of clsBeProducto)

        Get_Lista_Codigos_By_CodigoBarra_By_Picking = Nothing

        Try
            Get_Lista_Codigos_By_CodigoBarra_By_Picking = clsLnProducto.Get_List_Product_By_CodigoBarra_By_PickingEnc(pCodigoBarra, pIdBodega, pIdPickingEnc)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function


    <WebMethod(), SoapHeader("mArch")>
    Public Function Exist_Codigo_By_CodigoBarra(ByVal pCodigoBarra As String,
                                                ByVal pIdProductoBodega As Integer) As Boolean

        'GT1872022: valida si el codigo_barras esta asociado con el idproducto, no es necesario devolver objeto porque no se itera o parecido
        Exist_Codigo_By_CodigoBarra = False

        Try

            Exist_Codigo_By_CodigoBarra = clsLnProducto.Exist_Codigo_By_CodigoBarra(pCodigoBarra, pIdProductoBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKKF20220627 Creé esta función para cargar la información que vamos a mostra como stock reservado en la HH
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Stock_Res_By_Codigo_And_IdUbicacion(ByVal pIdUbicacion As Integer,
                                                            ByVal pCodigo As String,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pReferencia As String) As List(Of clsBeVW_stock_res)

        Get_Stock_Res_By_Codigo_And_IdUbicacion = Nothing

        Try

            Return clsLnVW_stock_res.Get_Stock_Res_By_Codigo_And_IdUbicacion2(pIdUbicacion,
                                                                              pCodigo,
                                                                              pIdBodega,
                                                                              pReferencia)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKKF20220627 Creé esta función para cargar la información que vamos a mostra como stock reservado en la HH
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Products_For_Reabastecimiento(ByVal pIdUbicacion As Integer,
                                                          ByVal pIdProducto As Integer,
                                                          ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res)

        Get_All_Products_For_Reabastecimiento = Nothing

        Try

            Return clsLnStock.Get_All_Products_For_Reabastecimiento(pIdProducto,
                                                                    pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Guardar_Recepcion_S(ByVal pIdRecpecionEnc As Integer,
                                        ByVal pIdTipoDocumentoDI As Integer,
                                        ByVal pIdOrdenCompraEnc As Integer,
                                        ByVal BeRecDet As clsBeTrans_re_det,
                                        ByVal pListRecDetParam As List(Of clsBeTrans_re_det_parametros),
                                        ByVal pListStockRecSer As List(Of clsBeStock_se_rec),
                                        ByVal pListStockRec As List(Of clsBeStock_rec),
                                        ByVal pListProductoPallet As List(Of clsBeProducto_pallet),
                                        ByVal pLotesRec As clsBeTrans_oc_det_lote,
                                        ByVal pIdEmpresa As Integer,
                                        ByVal pIdBodega As Integer,
                                        ByVal pIdUsuario As Integer,
                                        ByVal pIdResolucionLp As Integer,
                                        ByVal pIdOperadorBodega As Integer,
                                        ByVal EsCajaMaster As Boolean) As String

        Guardar_Recepcion_S = ""

        Try

            '#GT05102022_1600: deje el Operador bodega como opcional, porque se instancia GuardarHH en varios lados,
            'no dimensiono si siempre sera necesario enviarlo o no.

            Dim vResult As String = ""
            vResult = clsLnTrans_re_enc.GuardarHH_S(pIdRecpecionEnc,
                                                    pIdOrdenCompraEnc,
                                                    BeRecDet,
                                                    pListRecDetParam,
                                                    pListStockRecSer,
                                                    pListStockRec,
                                                    pListProductoPallet,
                                                    pLotesRec,
                                                    pIdEmpresa,
                                                    pIdBodega,
                                                    pIdUsuario,
                                                    pIdResolucionLp,
                                                    pIdOperadorBodega,
                                                    EsCajaMaster)


            Return String.Format("Res:{0}", vResult)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdEmpresa, pIdBodega, pIdUsuario, ex.StackTrace, pIdRecpecionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Detalle_OC_By_IdOrdenCompraEnc_HH2(ByVal pIdOrdenCompraEnc As Integer, ByVal pIdBodega As Integer) As List(Of clsBeTrans_oc_det)

        Get_Detalle_OC_By_IdOrdenCompraEnc_HH2 = Nothing

        Try

            Return clsLnTrans_oc_det.Get_Detalle_OC_By_IdOrdenCompraEnc_HH2(pIdOrdenCompraEnc, pIdBodega)

        Catch ex As Exception

            '#MECR07102025: Se agrego nueva bitacora de logs para OC
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, pIdBodega, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function


    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Detalle_Lotes_OC_By_IdOrdenCompraEnc_HH(ByVal pIdOrdenCompraEnc As Integer) As List(Of clsBeTrans_oc_det_lote)

        Get_Detalle_Lotes_OC_By_IdOrdenCompraEnc_HH = Nothing

        Try

            Return clsLnTrans_oc_det_lote.Get_Detalle_Lotes_OC_By_IdOrdenCompraEnc(pIdOrdenCompraEnc)

        Catch ex As Exception

            '#MECR07102025: Se agrego nueva bitacora de logs para OC
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_oc.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdOrdenCompraEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Stock_By_Lic_Plate_And_Codigo2(ByVal pLicensePlate As String,
                                                      ByVal pCodigo As String,
                                                      ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res)

        Get_Stock_By_Lic_Plate_And_Codigo2 = Nothing

        Try

            Return clsLnStock.Get_Stock_By_Licencia_And_Codigo2(pLicensePlate,
                                                               pCodigo,
                                                               pIdBodega)

        Catch ex As Exception

            '#MECR19112025: Se agrego bitacora de logs para implosion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pLic_Plate:=pLicensePlate, pEsImplosion:=True)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    Public Function Get_Stock_By_Lic_Plate2(ByVal pLicensePlate As String,
                                            ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res)

        Get_Stock_By_Lic_Plate2 = Nothing

        Try

            Return clsLnStock.Get_Stock_By_LicensePlate2(pLicensePlate,
                                                         pIdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Finalizar_Recepcion_S(ByVal pIdRecepcionEnc As Integer,
                                          ByVal backOrder As Boolean,
                                          ByVal pIdOrdenCompraEnc As Integer,
                                          ByVal pIdEmpresa As Integer,
                                          ByVal pIdBodega As Integer,
                                          ByVal pIdUsuario As String,
                                          ByVal pHabilitarStock As Boolean) As String

        Finalizar_Recepcion_S = ""

        Try

            clsLnTrans_re_enc.Finalizar_Recepcion_By_IdRecepcionEnc(pIdOrdenCompraEnc,
                                                                    backOrder,
                                                                    pIdRecepcionEnc,
                                                                    pIdEmpresa,
                                                                    pIdBodega,
                                                                    pIdUsuario,
                                                                    pHabilitarStock)

            '#EJC20220224_0326AM: Identificar si se debe registrar en NAV el documento para BYB.
            Dim BeTransOCEnc As New clsBeTrans_oc_enc
            BeTransOCEnc = clsLnTrans_oc_enc.Get_Single_By_IdOrdenCompraEnc_And_IdBodega(pIdOrdenCompraEnc,
                                                                                         pIdBodega)

            If Not BeTransOCEnc Is Nothing Then

                If BeTransOCEnc.Push_To_NAV Then

                    If (Not BeTransOCEnc.No_Documento_Recepcion_ERP = "" AndAlso Not BeTransOCEnc.Referencia = "") Then

                        Dim vResultPutAway As Boolean = Registrar_Pedido_Compra_To_NAV_For_BYB(BeTransOCEnc.IdOrdenCompraEnc,
                                                                                               BeTransOCEnc.Referencia,
                                                                                               BeTransOCEnc.No_Documento_Recepcion_ERP)

                        If Not vResultPutAway Then

                            Throw New Exception("Error NAV - No se pudo realizar el registro.")

                        End If

                    End If

                Else

                    Dim BeConfigEncBodega As New clsBeI_nav_config_enc
                    BeConfigEncBodega = clsLnI_nav_config_enc.Get_Single_By_IdBodega(BeTransOCEnc.IdBodega)

                    If BeConfigEncBodega.Interface_SAP Then

                        'Using sPT As New clsSyncSAPSSolicitudTraslado()
                        '    sPT.Enviar_Solicitud_Traslado_Proveedor(clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor)
                        'End Using
                        'Ejecutar_Interface("5-" & BeConfigEncBodega.Idnavconfigenc & "-" & 0 & "-" & "WS" & "-0-0" & "-" & "WS_HH", BeConfigEncBodega.Idnavconfigenc)

                    End If

                End If

            End If

            Finalizar_Recepcion_S = "Ok"

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdEmpresa, pIdBodega, pIdUsuario, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Android_Get_All_Empresas_Json()

        Try
            '#EJC20210223: Agregue imagen empresa
            Dim lEmpresas As New List(Of clsBeEmpresaBase)
            lEmpresas = clsLnEmpresa.Android_Get_All()

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET")
            Dim strserialize As String = JsonConvert.SerializeObject(lEmpresas)
            Dim currrentContext As HttpContext = HttpContext.Current
            currrentContext.Response.ContentType = "application/json"
            currrentContext.Response.Write(strserialize)
            currrentContext.Response.Flush()

        Catch ex As Exception

            ' Si la excepcion es diferente a SQL
            If Not TypeOf ex Is SqlException Then
                Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
                clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace)
            End If

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Stock_Por_Producto_Ubicacion_CI_Json(ByVal pidProducto As String,
                                                        ByVal pIdUbicacion As Integer,
                                                        ByVal pIdBodega As Integer,
                                                        ByVal pNombre As String,
                                                        ByVal pDetallado As Boolean)

        Try

            Dim lStock As New List(Of clsBeVW_stock_res_CI)

            lStock = clsLnStock_CI.Get_All_By_IdUbicacion(pIdUbicacion,
                                                          pidProducto,
                                                          pIdBodega,
                                                          pNombre,
                                                          pDetallado)


            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE")
            Dim strserialize As String = JsonConvert.SerializeObject(lStock)
            Dim currrentContext As HttpContext = HttpContext.Current
            currrentContext.Response.StatusCode = 200
            currrentContext.Response.ContentType = "application/json"
            currrentContext.Response.Write(strserialize)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            WriteErrorToEventLog(ex.Message)

            Dim errorJson As String = JsonConvert.SerializeObject(New With {
                .Error = True,
                .Mensaje = ex.Message
            })

            Dim curContext As HttpContext = HttpContext.Current
            curContext.Response.Clear()
            curContext.Response.StatusCode = 200
            curContext.Response.ContentType = "application/json"
            curContext.Response.Write(errorJson)
            curContext.ApplicationInstance.CompleteRequest()

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Sub Finalizar_Recepcion_Parcial_Pallet_Proveedor_S(ByVal pIdOrdenCompraEnc As Integer,
                                                              ByVal pIdRecepcionEnc As Integer,
                                                              ByVal pIdEmpresa As Integer,
                                                              ByVal pIdBodega As Integer,
                                                              ByVal pIdUsuario As String,
                                                              ByVal pBeStockRec As clsBeStock_rec,
                                                              ByVal pBeRecDet As clsBeTrans_re_det,
                                                              ByVal pBeBarraPallet As clsBeI_nav_barras_pallet,
                                                              ByVal pEsTransferencia As Boolean)

        Try

            'clsLnTrans_re_enc.Finalizar_Recepcion_Parcial_Pallet_Proveedor(pIdOrdenCompraEnc,
            '                                                               pIdRecepcionEnc,
            '                                                               pIdEmpresa,
            '                                                               pIdBodega,
            '                                                               pIdUsuario,
            '                                                               pBeStockRec,
            '                                                               pBeRecDet,
            '                                                               pBeBarraPallet,
            '                                                               pEsTransferencia)

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, pIdEmpresa, pIdBodega, pIdUsuario, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    ''' <summary>
    ''' #EJC202211022209: Simplificar como la HH manda los datos para mejorar la velocidad en la HH al procesar los argumentos.
    ''' </summary>
    ''' <param name="pIdPickingEnc"></param>
    ''' <param name="pIdPickingDet"></param>
    ''' <returns></returns>
    <WebMethod(), SoapHeader("mArch")>
    Public Function Obtener_Picking_Det_By_IdPickingEnc_And_IdPickingDet(ByVal pIdPickingEnc As Integer,
                                                                         ByVal pIdPickingDet As Integer) As clsBeTrans_picking_det

        Obtener_Picking_Det_By_IdPickingEnc_And_IdPickingDet = Nothing

        Try

            Return clsLnTrans_picking_det.Get_Single_By_IdPickingEnc_And_IdPickingDet(pIdPickingEnc, pIdPickingDet)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc, pIdPickingDet:=pIdPickingDet)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Single_StockRes_By_IdBodega_And_IdStockRes(ByVal pIdBodega As Integer, ByVal pIdStockRes As Integer) As clsBeStock_res

        Get_Single_StockRes_By_IdBodega_And_IdStockRes = Nothing

        Try

            Return clsLnStock_res.GetSingle_By_IdStockRes(pIdBodega, pIdStockRes)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Get_Picking_By_IdPickingEncJson(ByVal pIdPickingEnc As Integer,
                                                    ByVal pIdOperadorBodega As Integer) As clsBeTrans_picking_enc

        Get_Picking_By_IdPickingEncJson = Nothing

        Try

            Dim pBePicking As New clsBeTrans_picking_enc
            pBePicking = clsLnTrans_picking_enc.Get_Single_By_IdPickingEnc_For_HH(pIdPickingEnc, pIdOperadorBodega)

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE")

            If Not pBePicking Is Nothing Then

                Dim BePicking As String = JsonConvert.SerializeObject(pBePicking)
                Dim parsejsonBePicking As JObject = JsonConvert.SerializeObject(BePicking)
                Dim ListaPickingDetJson As String = JsonConvert.SerializeObject(pBePicking.ListaPickingDet)
                Dim ListaPickingUbicJson As String = JsonConvert.SerializeObject(pBePicking.ListaPickingUbic)
                Dim Detalle_operadorJson As String = JsonConvert.SerializeObject(pBePicking.Detalle_operador)
                Dim UbicacionPickingJson As String = JsonConvert.SerializeObject(pBePicking.UbicacionPicking)

                Dim vPath = "ListaPickingDet"
                parsejsonBePicking.SelectToken(vPath).Replace(ListaPickingDetJson)

                Dim vPath1 = "UbicacionPicking"
                parsejsonBePicking.SelectToken(vPath1).Replace(UbicacionPickingJson)

                Dim vPath2 = "ListaPickingUbic"
                parsejsonBePicking.SelectToken(vPath2).Replace(ListaPickingUbicJson)

                Dim vPath3 = "Detalle_operador"
                parsejsonBePicking.SelectToken(vPath3).Replace(Detalle_operadorJson)

                Dim currrentContext As HttpContext = HttpContext.Current
                currrentContext.Response.ContentType = "application/json"

                Dim parsejsonBePicking1 As JObject = JObject.Parse(parsejsonBePicking)

                currrentContext.Response.Write(parsejsonBePicking1)
                currrentContext.Response.Flush()

            End If

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod, ScriptMethod(ResponseFormat:=ResponseFormat.Json, XmlSerializeString:=False)>
    Public Sub Get_Reporte_Proximos_A_Vencer_By_OpenIA(prompt As String)

        Dim result As New Dictionary(Of String, String)()
        Dim vparsedresult As String = ""

        Try

            Dim chatService As New ChatGPTService()
            Dim response As String = Task.Run(Function() chatService.Get_Reporte_Proximos_A_Vencer_By_OpenIA(prompt, "gpt-4")).Result
            Dim vresponse As String = response

            ' Intentar analizar la respuesta como JSON.
            Try
                Dim jsonResponse As JObject = JObject.Parse(response)
                Dim jsonMessage = jsonResponse("choices")(0)("message")("content").ToString()
                ' Extraer y retornar el objeto JSON después del token "respuesta_wms".
                Dim startIndex As Integer = jsonMessage.IndexOf("{")
                Dim endIndex As Integer = jsonMessage.LastIndexOf("}")
                If startIndex > -1 And endIndex > -1 Then
                    Dim jsonSubstring = jsonMessage.Substring(startIndex, endIndex - startIndex + 1)

                    ' Validar y corregir el JSON extraído si es necesario.
                    jsonSubstring = Regex.Replace(jsonSubstring, """\s*", """") ' Elimina los espacios después de las comillas dobles.
                    jsonSubstring = Regex.Replace(jsonSubstring, "\s*""", """") ' Elimina los espacios antes de las comillas dobles.

                    ' Verificar si el JSON es válido. Si no, devolver un error.
                    Dim validJson As JObject = JObject.Parse(jsonSubstring)
                    vparsedresult = jsonSubstring

                End If

            Catch ex As Exception
                Console.WriteLine("No se pudo parsear la respuesta")
            End Try

            Dim currrentContext As HttpContext = HttpContext.Current
            currrentContext.Response.ContentType = "application/json"

            If vparsedresult = "" Then
                currrentContext.Response.Write(vresponse)
            Else
                currrentContext.Response.Write(vparsedresult)
            End If

            currrentContext.Response.Flush()

        Catch ae As AggregateException
            result("status") = "error"
            result("message") = "No se pudo obtener una respuesta de ChatGPT. Detalles: " & ae.InnerExceptions(0).Message
        Catch ex As Exception
            result("status") = "error"
            result("message") = "Algo salió mal. Detalles: " & ex.Message
        End Try

    End Sub

    <WebMethod, ScriptMethod(ResponseFormat:=ResponseFormat.Json, XmlSerializeString:=False)>
    Public Sub Ask_ChatGPT_JsonResult(prompt As String)

        Dim result As New Dictionary(Of String, String)()
        Dim vparsedresult As String = ""

        Try

            Dim chatService As New ChatGPTService()
            Dim response As String = Task.Run(Function() chatService.Get_Generyc_Response_Async(prompt, "gpt-4")).Result
            Dim vresponse As String = response

            Try

                Dim jsonResponse As JObject = JObject.Parse(response)
                Dim jsonMessage = jsonResponse("choices")(0)("message")("content").ToString()
                Dim startIndex As Integer = jsonMessage.IndexOf("{")
                Dim endIndex As Integer = jsonMessage.LastIndexOf("}")

                If startIndex > -1 And endIndex > -1 Then

                    Dim jsonSubstring = jsonMessage.Substring(startIndex, endIndex - startIndex + 1)
                    Dim validJson As JObject = JObject.Parse(jsonSubstring)
                    vparsedresult = jsonSubstring

                End If

            Catch ex As Exception
                Console.WriteLine("No se pudo parsear la respuesta")
            End Try

            Dim currrentContext As HttpContext = HttpContext.Current
            currrentContext.Response.ContentType = "application/json"

            If vparsedresult = "" Then
                currrentContext.Response.Write(vresponse)
            Else
                currrentContext.Response.Write(vparsedresult)
            End If

            currrentContext.Response.Flush()

        Catch ae As AggregateException
            result("status") = "error"
            result("message") = "No se pudo obtener una respuesta de ChatGPT. Detalles: " & ae.InnerExceptions(0).Message
        Catch ex As Exception
            result("status") = "error"
            result("message") = "Algo salió mal. Detalles: " & ex.Message
        End Try

    End Sub

    <WebMethod, ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Existe_Lote_By_IdBodega_And_NoLote(ByVal IdBodega As Integer, ByVal NoLote As String) As Date

        Existe_Lote_By_IdBodega_And_NoLote = New Date(1900, 1, 1)

        Try

            Existe_Lote_By_IdBodega_And_NoLote = clsLnStock.Existe_Lote(IdBodega, NoLote)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '<WebMethod, ScriptMethod(ResponseFormat:=ResponseFormat.Json, XmlSerializeString:=False)>
    'Public Sub Get_Info_Imagen_By_IA()

    '    Dim result As New Dictionary(Of String, String)()
    '    Dim vparsedresult As String = ""
    '    Dim jsonMessage As String = ""

    '    Try

    '        Dim chatService As New ChatGPTService()
    '        Dim response As String = Task.Run(Function() chatService.Get_Info_Imagen_By_IA("gpt-4")).Result
    '        Dim vresponse As String = response
    '        Dim jsonResponse As JObject = Nothing

    '        Try

    '            If Not response.Equals("") Then

    '                jsonResponse = JObject.Parse(response)
    '                jsonMessage = jsonResponse("choices")(0)("message")("content").ToString()
    '                Dim startIndex As Integer = jsonMessage.IndexOf("{")
    '                Dim endIndex As Integer = jsonMessage.LastIndexOf("}")

    '                If startIndex > -1 And endIndex > -1 Then

    '                    Dim jsonSubstring = jsonMessage.Substring(startIndex, endIndex - startIndex + 1)
    '                    Dim validJson As JObject = JObject.Parse(jsonSubstring)
    '                    vparsedresult = jsonSubstring
    '                Else
    '                    vparsedresult = jsonMessage
    '                End If

    '            Else
    '                Dim validJson As JObject = JObject.Parse(vresponse)
    '            End If

    '        Catch ex As Exception
    '            Console.WriteLine("No se pudo parsear la respuesta")
    '        End Try

    '        Dim currrentContext As HttpContext = HttpContext.Current
    '        currrentContext.Response.ContentType = "application/json"

    '        If vparsedresult = "" Then
    '            currrentContext.Response.Write(JObject.Parse(vresponse))
    '        ElseIf Not IsNothing(jsonResponse) Then
    '            currrentContext.Response.Write(JObject.Parse(jsonResponse))
    '        Else
    '            currrentContext.Response.Write(vresponse)
    '        End If

    '        currrentContext.Response.Flush()

    '    Catch ae As AggregateException
    '        result("status") = "error"
    '        result("message") = "No se pudo obtener una respuesta de ChatGPT. Detalles: " & ae.InnerExceptions(0).Message
    '    Catch ex As Exception
    '        result("status") = "error"
    '        result("message") = "Algo salió mal. Detalles: " & ex.Message
    '    End Try

    'End Sub


    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Vencimiento_By_IdBodega_And_Lote(ByVal pIdBodega As Integer, ByVal pNoLote As String) As String

        Get_Vencimiento_By_IdBodega_And_Lote = ""

        Try

            Return clsLnStock.Existe_Lote(pIdBodega, pNoLote).ToString("yyyy-MM-dd'T'HH:mm:ss")

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Get_Tipo_Etiqueta_By_IdTipoEtiqueta_Json(ByVal IdTipoEtiqueta As Integer,
                                                             ByVal IdSimbologia As Integer)

        Get_Tipo_Etiqueta_By_IdTipoEtiqueta_Json = Nothing

        Try

            Dim BeTipo_etiqueta As New clsBeTipo_etiqueta
            BeTipo_etiqueta = clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(IdTipoEtiqueta, IdSimbologia, 1)

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE")
            Dim strserialize As String = JsonConvert.SerializeObject(BeTipo_etiqueta)

            ' Serializar a JSON
            Dim jsonResult As String = JsonConvert.SerializeObject(BeTipo_etiqueta)

            ' Establecer los encabezados de respuesta adecuados
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.StatusCode = 200
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8"
            HttpContext.Current.Response.Write(jsonResult)
            'HttpContext.Current.Response.End()

            Return jsonResult

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try

    End Function

    <WebMethod>
    Public Function Ejecutar_Interface(ByVal ParametroEjecucion As String, ByVal IdConfiguracionInterface As Integer) As Boolean

        Dim vRutaInterface As String = ""
        Dim vNombre_Ejecutable As String

        Ejecutar_Interface = False

        Try
            ' Obtén el nombre del ejecutable desde la base de datos o configuración
            vNombre_Ejecutable = clsLnI_nav_config_enc.Get_Nombre_Ejecutable(IdConfiguracionInterface)

            If Not String.IsNullOrEmpty(vNombre_Ejecutable) Then
                ' Define la ruta completa del ejecutable
                vRutaInterface = Server.MapPath("~/" & vNombre_Ejecutable) ' Asegúrate de que la ruta es correcta

                If File.Exists(vRutaInterface) Then
                    Dim startInfo As New ProcessStartInfo()
                    startInfo.FileName = vRutaInterface
                    startInfo.Arguments = ParametroEjecucion
                    startInfo.WorkingDirectory = Path.GetDirectoryName(vRutaInterface) ' Establece el directorio de trabajo

                    ' Asegúrate de que la salida de la aplicación esté redirigida para evitar bloqueos
                    startInfo.RedirectStandardOutput = True
                    startInfo.RedirectStandardError = True
                    startInfo.UseShellExecute = False
                    startInfo.CreateNoWindow = True

                    ' Ejecuta el proceso y espera a que termine
                    Using process As Process = Process.Start(startInfo)
                        process.WaitForExit() ' Espera a que el proceso termine
                        Dim output As String = process.StandardOutput.ReadToEnd()
                        Dim [error] As String = process.StandardError.ReadToEnd()

                        If process.ExitCode = 0 Then
                            Ejecutar_Interface = True
                        Else
                            ' Loggea el error
                            ' Por ejemplo: LogError("Error al ejecutar el proceso: " & [error])
                        End If
                    End Using
                End If
            End If

        Catch ex As Exception
            ' Captura y maneja cualquier excepción
            ' Por ejemplo: LogError("Excepción: " & ex.Message)
            Throw
        End Try

        Return Ejecutar_Interface

    End Function


    Private Sub ShellandWait(ByVal ProcessPath As String,
                             ByVal Args As String)

        Dim objProcess As Process

        Try

            objProcess = New Process()

            Dim startInfo As New ProcessStartInfo()
            startInfo.UseShellExecute = False
            startInfo.RedirectStandardOutput = True
            startInfo.RedirectStandardError = True
            startInfo.CreateNoWindow = True
            startInfo.WindowStyle = ProcessWindowStyle.Hidden
            startInfo.Arguments = Args
            startInfo.FileName = ProcessPath
            objProcess.StartInfo = startInfo
            objProcess.Start()

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub SerializarJson(ByVal raiz As JObject, ByVal ruta As String)
        Try
            If raiz Is Nothing OrElse String.IsNullOrWhiteSpace(ruta) Then Exit Sub

            Dim partes As String() = ruta.Split("."c)
            Dim objetoPadre As JObject = raiz

            For i As Integer = 0 To partes.Length - 2
                Dim siguiente As JToken = objetoPadre(partes(i))
                If siguiente Is Nothing OrElse siguiente.Type <> JTokenType.Object Then
                    Exit Sub
                End If
                objetoPadre = CType(siguiente, JObject)
            Next

            Dim claveFinal As String = partes(partes.Length - 1)
            Dim valorActual As JToken = objetoPadre(claveFinal)
            If valorActual Is Nothing Then Exit Sub

            If valorActual.Type = JTokenType.Object Then
                Dim obj = CType(valorActual, JObject)
                If obj("items") IsNot Nothing Then Exit Sub
            End If

            Dim nuevaEstructura As New JObject()
            nuevaEstructura("items") = valorActual
            objetoPadre(claveFinal) = nuevaEstructura

        Catch ex As Exception
        End Try
    End Sub

    '#MA 20251014 Migracion de xml a json
    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Get_Ubicacion_Sugerida_JSON(ByVal Licencia As String) As String

        Dim responseObj As New Dictionary(Of String, Object)
        Try

            ' Sugerir ubicaciones para el pallet
            Dim k = 3 ' Número de ubicaciones sugeridas
            Dim ubicacionesSugeridas = clsLnUbicacionSugerida.Sugerir_Ubicaciones(Licencia, k)

            Dim vIdUbicacion As Integer = 0

            If ubicacionesSugeridas Is Nothing Then
                clsLnLog_error_wms.Agregar_Error("Sugerir_Ubicaciones devolvió Nothing")
            Else
                clsLnLog_error_wms.Agregar_Error("Cantidad de ubicaciones encontradas: " & ubicacionesSugeridas.Count)
            End If

            responseObj("data") = ubicacionesSugeridas

            If Not ubicacionesSugeridas Is Nothing Then
                If ubicacionesSugeridas.Count > 0 Then
                    vIdUbicacion = ubicacionesSugeridas.Take(1).FirstOrDefault.IdUbicacion
                End If
            End If

            ' Serializar a JSON y establecer encabezados de respuesta
            ' Dim jsonResult As String = JsonConvert.SerializeObject(vIdUbicacion)

            ' HttpContext.Current.Response.Clear()
            ' HttpContext.Current.Response.ContentType = "application/json; charset=utf-8"
            'HttpContext.Current.Response.Write(jsonResult)
            'HttpContext.Current.Response.End()

            ' Return jsonResult

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            ' Manejo de errores
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pLicencia:=Licencia)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    If Not Mensaje.Contains("Subproceso anulado") Then
                        Throw New Exception(Mensaje)
                    End If
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonResult As String = serializer.Serialize(responseObj)
        Dim statusCode As Integer = If(responseObj.ContainsKey("error"), 500, 200)

        With HttpContext.Current.Response
            .Clear()
            .StatusCode = statusCode
            .ContentType = "application/json"
            .Output.Write(jsonResult)
            .End()
        End With

        Return jsonResult
    End Function
    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Get_Ubicacion_Sugerida(ByVal Licencia As String) As String

        Get_Ubicacion_Sugerida = Nothing

        Try

            ' Sugerir ubicaciones para el pallet
            Dim k = 3 ' Número de ubicaciones sugeridas
            Dim ubicacionesSugeridas = clsLnUbicacionSugerida.Sugerir_Ubicaciones(Licencia, k)

            Dim vIdUbicacion As Integer = 0

            If Not ubicacionesSugeridas Is Nothing Then
                If ubicacionesSugeridas.Count > 0 Then
                    vIdUbicacion = ubicacionesSugeridas.Take(1).FirstOrDefault.IdUbicacion
                End If
            End If

            ' Serializar a JSON y establecer encabezados de respuesta
            Dim jsonResult As String = JsonConvert.SerializeObject(vIdUbicacion)

            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8"
            HttpContext.Current.Response.Write(jsonResult)
            'HttpContext.Current.Response.End()

            Return jsonResult

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            ' Manejo de errores
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pLicencia:=Licencia)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    If Not Mensaje.Contains("Subproceso anulado") Then
                        Throw New Exception(Mensaje)
                    End If
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function
    ''' <summary>
    ''' #EJC202409040600: Despacho automático desde HH.
    ''' </summary>
    ''' <param name="IdPedidoEnc"></param>
    ''' <param name="IdBodega"></param>
    ''' <param name="IdOperadorVerifico"></param>
    ''' <returns></returns>
    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Despachar_Pedido(ByVal IdPedidoEnc As Integer,
                                     ByVal IdBodega As Integer,
                                     ByVal IdOperadorVerifico As Integer) As Integer

        Despachar_Pedido = 0

        Dim BeDespachoEnc As New clsBeTrans_despacho_enc
        Dim cTrans As New clsTransaccion
        Dim BePedidoEnc As New clsBeTrans_pe_enc

        Try

            cTrans.Open_Connection() : cTrans.Begin_Transaction()

            Dim BeINavConfig As New clsBeI_nav_config_enc
            BeINavConfig = clsLnI_nav_config_enc.Get_Single_By_IdBodega(IdBodega)

            If Not BeINavConfig Is Nothing Then

                BePedidoEnc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc,
                                                          cTrans.lConnection,
                                                          cTrans.lTransaction)

                If Not BePedidoEnc Is Nothing Then

                    If Not clsLnTrans_pe_enc.Pedido_Tienen_Verificacion_Parcial(BePedidoEnc,
                                                                                cTrans.lConnection,
                                                                                cTrans.lTransaction) Then

                        '#EJC20220627:Pendiente validar si funciona que no permita despachos múltiples para un mismo pedido.
                        If clsLnTrans_pe_enc.Pedidos_Permiten_Despacho_Multiple(BePedidoEnc, cTrans.lConnection, cTrans.lTransaction) Then

                            BeDespachoEnc.IdDespachoEnc = clsLnTrans_despacho_enc.MaxID(cTrans.lConnection,
                                                                                        cTrans.lTransaction)
                            BeDespachoEnc.IdBodega = BePedidoEnc.IdBodega
                            BeDespachoEnc.IdEmpresa = clsLnBodega.Get_IdEmpresa_By_IdBodega(BeDespachoEnc.IdBodega,
                                                                                            cTrans.lConnection,
                                                                                            cTrans.lTransaction)
                            BeDespachoEnc.IdPropietarioBodega = BePedidoEnc.IdPropietarioBodega
                            BeDespachoEnc.IdPiloto = 0
                            BeDespachoEnc.IdVehiculo = 0
                            BeDespachoEnc.IdRuta = 0
                            BeDespachoEnc.Fecha = clsServidor.Get_Fecha_Servidor()
                            BeDespachoEnc.No_pase = 0
                            BeDespachoEnc.Observacion = "Despacho automático generado desde HH"
                            BeDespachoEnc.Hora_ini = BePedidoEnc.Fecha_Pedido
                            BeDespachoEnc.Hora_fin = Now
                            BeDespachoEnc.Estado = "Finalizado"
                            BeDespachoEnc.Numero = 0
                            BeDespachoEnc.Marchamo = ""
                            BeDespachoEnc.Cant_bultos = 0
                            BeDespachoEnc.IsNew = True
                            BeDespachoEnc.User_agr = IdOperadorVerifico
                            BeDespachoEnc.Fec_agr = Now
                            BeDespachoEnc.User_mod = IdOperadorVerifico
                            BeDespachoEnc.Fec_mod = Now
                            BeDespachoEnc.Activo = True

                            BeDespachoEnc.ListaPedidos.Add(BePedidoEnc)

                            For Each Detalle In BePedidoEnc.Detalle
                                If Not Detalle.ListaPickingUbic Is Nothing Then
                                    For Each BePickingUbic In Detalle.ListaPickingUbic
                                        Adicionar_Producto_A_Detalle_Despacho(BeDespachoEnc,
                                                                              BePickingUbic,
                                                                              BePedidoEnc.Fecha_Pedido,
                                                                              IdOperadorVerifico)
                                    Next
                                End If
                            Next


                            If clsLnTrans_despacho_enc.Guardar(BeDespachoEnc,
                                                               False,
                                                               cTrans.lConnection,
                                                               cTrans.lTransaction) Then

                                If BeINavConfig.Idnavconfigenc <> -1 Then

                                    If BeINavConfig.Interface_SAP Then

                                        Dim vArgumentosAEnviarAInterface As String = ""

                                        If Not BeINavConfig Is Nothing Then

                                            If BeINavConfig.Ejecutar_En_Despacho_Automaticamente Then

                                                Dim tipoDocumento As New clsDataContractDI.tTipoDocumentoSalida

                                                tipoDocumento = BePedidoEnc.IdTipoPedido

                                                Select Case tipoDocumento
                                                    Case clsDataContractDI.tTipoDocumentoSalida.Devolucion_Proveedor
                                            'vArgumentosAEnviarAInterface = "10-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & BeDespachoEnc.ListaPedidos(0).Referencia & "-0" & "-" & clsBD.Instancia.NombreInstancia
                                                    Case clsDataContractDI.tTipoDocumentoSalida.Pedido_De_Cliente
                                            'vArgumentosAEnviarAInterface = "9-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & BeDespachoEnc.ListaPedidos(0).Referencia & "-0" & "-" & clsBD.Instancia.NombreInstancia
                                                    Case clsDataContractDI.tTipoDocumentoSalida.Requisicion
                                            'vArgumentosAEnviarAInterface = "11-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & BeDespachoEnc.ListaPedidos(0).Referencia & "-0" & "-" & clsBD.Instancia.NombreInstancia
                                                    Case clsDataContractDI.tTipoDocumentoSalida.Transferencia_Directa
                                            'vArgumentosAEnviarAInterface = "6-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & BeDespachoEnc.ListaPedidos(0).Referencia & "-0" & "-" & clsBD.Instancia.NombreInstancia
                                                    Case clsDataContractDI.tTipoDocumentoSalida.Traslado_Por_Estados_SAP
                                                        'vArgumentosAEnviarAInterface = "11-" & AP.IdConfiguracionInterface & "-" & gIndiceInstancia & "-" & AP.UsuarioAp.IdUsuario & "-" & BeDespachoEnc.ListaPedidos(0).Referencia & "-0" & "-" & clsBD.Instancia.NombreInstancia
                                                End Select

                                                'Ejecutar_Interface(vArgumentosAEnviarAInterface, Me)

                                            End If

                                        End If
                                    Else
                                        Throw New Exception("ERROR_20240904A: No se puede despachar automaticamente cuando la bandera interface SAP está inactiva.")
                                    End If


                                End If

                            Else
                                Throw New Exception("ERROR202409040734: La configuración de interface no permite despachos múltiples para un mismo pedido.")
                            End If

                        End If

                    End If

                Else
                    Throw New Exception("ERROR_202409040725: No se pudo obtener el pedido.")
                End If

            Else
                Throw New Exception("ERROR_20240904: No está definida la configuración de interface.")
            End If

            cTrans.Commit_Transaction()

            ' Serializar a JSON y establecer encabezados de respuesta
            Dim jsonResult As String = JsonConvert.SerializeObject(BeDespachoEnc.IdDespachoEnc)

            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8"
            HttpContext.Current.Response.Write(jsonResult)
            'HttpContext.Current.Response.End()

            Return jsonResult

        Catch ex As Exception
            cTrans.RollBack_Transaction()

            '#MECR04122025: Se agrego bitacora de logs para verificacion
            ' Manejo de errores
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_verificacion_bof.Agregar_Error(vMsgError, pIdBodega:=IdBodega, pIdPedidoEnc:=IdPedidoEnc, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    If Not Mensaje.Contains("Subproceso anulado") Then
                        Throw New Exception(Mensaje)
                    End If
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If
        Finally
            cTrans.Close_Conection()
        End Try

    End Function

    Private Sub Adicionar_Producto_A_Detalle_Despacho(ByRef BeDespachoEnc As clsBeTrans_despacho_enc,
                                                      ByVal BeTransPickingUbic As clsBeTrans_picking_ubic,
                                                      ByVal FechaPedido As Date,
                                                      ByVal IdUsuario As Integer)

        Try

            Dim BeDespachoDet As New clsBeTrans_despacho_det

            If BeDespachoEnc.ListaDetalle IsNot Nothing AndAlso BeDespachoEnc.ListaDetalle.Count > 0 Then
                BeDespachoDet.IdDespachoDet = BeDespachoEnc.ListaDetalle.Max(Function(b) b.IdDespachoDet) + 1
            Else
                BeDespachoDet.IdDespachoDet = 1
            End If

            BeDespachoDet.Codigo = BeTransPickingUbic.CodigoProducto
            BeDespachoDet.NombreProducto = BeTransPickingUbic.NombreProducto
            BeDespachoDet.NombreEstado = BeTransPickingUbic.ProductoEstado
            BeDespachoDet.IdPickingUbic = BeTransPickingUbic.IdPickingUbic
            BeDespachoDet.IdProductoBodega = BeTransPickingUbic.IdProductoBodega
            BeDespachoDet.IdProductoEstado = BeTransPickingUbic.IdProductoEstado
            BeDespachoDet.IdPresentacion = BeTransPickingUbic.IdPresentacion
            BeDespachoDet.IdUnidadMedidaBasica = BeTransPickingUbic.IdUnidadMedida
            BeDespachoDet.IdPedidoEnc = BeTransPickingUbic.IdPedidoEnc
            BeDespachoDet.IdPedidoDet = BeTransPickingUbic.IdPedidoDet
            BeDespachoDet.IdPickingUbic = BeTransPickingUbic.IdPickingUbic
            BeDespachoDet.CantidadDespachada = BeTransPickingUbic.Cantidad_Verificada
            BeDespachoDet.PesoDespachado = BeTransPickingUbic.Peso_verificado
            BeDespachoDet.User_agr = IdUsuario
            BeDespachoDet.Fec_agr = Now
            BeDespachoDet.User_mod = IdUsuario
            BeDespachoDet.NombreUbicacion = BeTransPickingUbic.NombreUbicacion
            BeDespachoDet.Fec_mod = Now
            BeDespachoDet.Activo = True
            BeDespachoDet.IsNew = True
            BeDespachoDet.FechaPedido = FechaPedido

            BeDespachoEnc.ListaDetalle.Add(BeDespachoDet)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Value_From_Script(ByVal Clave As String) As String

        Get_Value_From_Script = Nothing

        Try


            Dim ValorNoEncriptado As String = clsPublic.Desencriptar(Clave)

            Return ValorNoEncriptado

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_LicenciasPacking_Cerrado(ByVal pIdPedidoEnc As Integer) As List(Of clsBeTrans_packing_enc)

        Get_LicenciasPacking_Cerrado = Nothing

        Try

            Return clsLnTrans_packing_enc.Get_LicenciasPacking_Cerrado(pIdPedidoEnc)

        Catch ex As Exception

            '#MECR12112025: Se agrego bitacora de logs para pedidos
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pack.Agregar_Error(vMsgError, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try
    End Function

    '#AT20250219 Funcion para obtener el detalle por codigo y lista de pedidos
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Verificacion_By_Producto_And_Pedido(ByVal pProducto As String,
                                                            ByVal pListaPedidos As String) As clsBeDetallePedidoAVerificar

        Get_Verificacion_By_Producto_And_Pedido = Nothing

        Try

            Return clsLnTrans_pe_det.Get_Detalle_By_IdPedidoEnc_And_Producto_Consolidado(pProducto, pListaPedidos)

        Catch ex As Exception

            '#MECR21102025: Se agrego bitacora de logs para pedidos
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try
    End Function

    ''' <summary>
    ''' Lista de productos a pickear como json
    ''' </summary>
    ''' <param name="pIdPickingEnc"></param>
    ''' <param name="pDetalleOperador"></param>
    ''' <param name="pIdOperadorBodega"></param>
    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_All_PickingUbic_By_IdPickingEnc_Tipo_Json(ByVal pIdPickingEnc As Integer,
                                                             ByVal pDetalleOperador As Boolean,
                                                             ByVal pIdOperadorBodega As Integer,
                                                             ByVal Tipo As Integer)

        Dim vLista As List(Of clsBeTrans_picking_ubic) = Nothing

        Try

            vLista = clsLnTrans_picking_ubic.Get_All_PickingUbic_By_IdPickingEnc_Tipo(pIdPickingEnc,
                                                                                      pDetalleOperador,
                                                                                      pIdOperadorBodega,
                                                                                      Tipo)

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE")

            If Not vLista Is Nothing Then

                Dim BePicking As String = JsonConvert.SerializeObject(vLista)
                Dim currrentContext As HttpContext = HttpContext.Current
                currrentContext.Response.StatusCode = 200
                currrentContext.Response.ContentType = "application/json"
                currrentContext.Response.Write(BePicking)
                currrentContext.Response.Flush()

            End If

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try

    End Sub

    '#CKFK20250226 Agregué este metodo
    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Single_By_IdPedidoEnc_Json(ByVal pIdPedidoEnc As Integer,
                                              ByVal pCodigoProducto As String)

        Try

            Dim vPedido As clsBeTrans_pe_enc = Nothing

            vPedido = clsLnTrans_pe_enc.Get_Single_By_IdPedidoEnc_And_Codigo_Producto(pIdPedidoEnc, pCodigoProducto)

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE")

            If Not vPedido Is Nothing Then

                ' Serializar el objeto original
                Dim BePicking As String = JsonConvert.SerializeObject(vPedido)

                ' Deserializar en un objeto JObject para modificarlo
                Dim objPedido As JObject = JObject.Parse(BePicking)

                ' Verificar que ListaPickingUbic existe antes de modificarlo
                If objPedido("Picking")("ListaPickingUbic") IsNot Nothing Then
                    ' Crear un nuevo JObject con "items"
                    Dim nuevaEstructura As New JObject()
                    nuevaEstructura("items") = objPedido("Picking")("ListaPickingUbic")

                    ' Asignar la nueva estructura
                    objPedido("Picking")("ListaPickingUbic") = nuevaEstructura
                End If

                ' Volver a serializar el objeto modificado
                Dim BePedido As String = JsonConvert.SerializeObject(objPedido, Formatting.Indented)

                Dim currrentContext As HttpContext = HttpContext.Current
                currrentContext.Response.StatusCode = 200
                currrentContext.Response.ContentType = "application/json"
                currrentContext.Response.Write(BePedido)
                currrentContext.Response.Flush()

            End If

        Catch ex As Exception

            '#MECR21102025: Se agrego bitacora de logs para pedidos
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_pe.Agregar_Error(vMsgError, pIdPedidoEnc:=pIdPedidoEnc, pStackTrace:=ex.StackTrace)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try

    End Sub


    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Lista_Codigos_By_CodigoBarra_By_Picking_And_Pedido(ByVal pCodigoBarra As String,
                                                                           ByVal pIdBodega As Integer,
                                                                           ByVal pIdPickingEnc As Integer,
                                                                           ByVal pIdPedidoEnc As Integer) As List(Of clsBeProducto)

        Get_Lista_Codigos_By_CodigoBarra_By_Picking_And_Pedido = Nothing

        Try
            Get_Lista_Codigos_By_CodigoBarra_By_Picking_And_Pedido = clsLnProducto.Get_List_Product_By_CodigoBarra_By_PickingEnc_And_Pedido(pCodigoBarra,
                                                                                                                                            pIdBodega,
                                                                                                                                            pIdPickingEnc,
                                                                                                                                            pIdPedidoEnc)

        Catch ex As Exception

            '#MECR04122025: Se agrego bitacora de logs para verificacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_verificacion_bof.Agregar_Error(vMsgError,
            'pIdPedidoEnc:=pIdPedidoEnc,
            'pStackTrace:=ex.StackTrace,
            'pIdBodega:=pIdBodega,
            'pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function


    '#GT21042025: retorna el tipo documento del pedido, para identificar si apica lectura muelle en picking
    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Tipo_Pedido_Json(ByVal pIdPickingEnc As Integer)

        Dim pTipoPedido As New clsBeTrans_pe_tipo
        pTipoPedido = Nothing

        Try

            pTipoPedido = clsLnTrans_pe_enc.Get_TipoPedido_By_IdPickingEnc(pIdPickingEnc)

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE")

            If Not pTipoPedido Is Nothing Then

                Dim TipoPedido As String = JsonConvert.SerializeObject(pTipoPedido)
                Dim currrentContext As HttpContext = HttpContext.Current
                currrentContext.Response.StatusCode = 200
                currrentContext.Response.ContentType = "application/json"
                currrentContext.Response.Write(TipoPedido)
                currrentContext.Response.Flush()

            End If

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=pIdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                Dim errorJson As String = JsonConvert.SerializeObject(New With {
                    .Error = True,
                    .Mensaje = ex.Message
                })

                Dim curContext As HttpContext = HttpContext.Current
                curContext.Response.Clear()
                curContext.Response.StatusCode = 200
                curContext.Response.ContentType = "application/json"
                curContext.Response.Write(errorJson)
                curContext.ApplicationInstance.CompleteRequest()

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_CantidadPedidos_Picking(ByVal IdPickingEnc As Integer) As Integer

        Get_CantidadPedidos_Picking = 0

        Try
            Return clsLnTrans_picking_enc.Get_CantidadPedidos_By_IdPickingEnc(IdPickingEnc)

        Catch ex As Exception

            '#MECR28102025: Se agrego bitacora para logs de picking
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            clsLnLog_error_wms_pick.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdPickingEnc:=IdPickingEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Presentacion_Defecto_By_Producto(ByVal pIdProducto As Integer) As clsBeProducto_Presentacion

        Get_Presentacion_Defecto_By_Producto = Nothing

        Try

            Return clsLnProducto_presentacion.Get_Presentacion_Defecto_By_IdProducto(pIdProducto)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_By_IdUbicacion_And_LicPlate(ByVal pIdUbicacion As Integer, pLicencia As String) As List(Of clsBeStock)

        Get_All_By_IdUbicacion_And_LicPlate = Nothing

        Try

            Return clsLnStock.Get_All_By_IdUbicacion_And_LicPlate(pIdUbicacion, pLicencia)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function GetSingleRecJson(ByVal pIdRecepcionEnc As Integer, ByVal CodigoProducto As String) As String

        Try
            ' Obtener el objeto original
            Dim beRecepcion As clsBeTrans_re_enc = clsLnTrans_re_enc.GetSingleHH_By_Codigo(pIdRecepcionEnc, CodigoProducto)

            ' Serializar a JSON
            Dim serializer As New JavaScriptSerializer()
            serializer.MaxJsonLength = Integer.MaxValue
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET")
            Dim strserialize As String = JsonConvert.SerializeObject(beRecepcion)
            GetSingleRecJson = strserialize
            Dim currrentContext As HttpContext = HttpContext.Current
            currrentContext.Response.ContentType = "application/json"
            currrentContext.Response.Write(strserialize)
            currrentContext.Response.Flush()

        Catch ex As Exception

            '#MECR01102025: Se agrego bitacora de logs para recepciones.
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_rec.Agregar_Error(vMsgError, 0, 0, 0, ex.StackTrace, pIdRecepcionEnc)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then
                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)

                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)

                    currrentContext.Response.Clear()
                    currrentContext.Response.StatusCode = 299
                    currrentContext.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    currrentContext.Response.Output.Write(sw.ToString())
                    currrentContext.Response.ContentType = "application/json"
                    currrentContext.Response.End()
                End If
            End If

            Return "{""error"":""" & Mensaje.Replace("""", "\""") & """}"
        End Try

    End Function

    '#MA20251015 Migracion de xml a json
    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Producto_Talla_Color_JSON()

        Try
            Dim ltallacolor As List(Of clsBeProducto_talla_color)
            ltallacolor = clsLnProducto_talla_color.Get_All()

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET")
            Dim strserialize As String = JsonConvert.SerializeObject(ltallacolor)
            Dim currrentContext As HttpContext = HttpContext.Current
            currrentContext.Response.StatusCode = 200
            currrentContext.Response.ContentType = "application/json"
            currrentContext.Response.Write(strserialize)
            currrentContext.Response.Flush()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try

    End Sub

    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Colores()

        Try
            Dim lColores As New List(Of clsBeColor)
            lColores = clsLnColor.Get_All()

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET")
            Dim strserialize As String = JsonConvert.SerializeObject(lColores)
            Dim currrentContext As HttpContext = HttpContext.Current
            currrentContext.Response.ContentType = "application/json"
            currrentContext.Response.Write(strserialize)
            currrentContext.Response.Flush()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Tallas()

        Try
            Dim lTallas As New List(Of clsBeTalla)
            lTallas = clsLnTalla.Get_All()

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET")
            Dim strserialize As String = JsonConvert.SerializeObject(lTallas)
            Dim currrentContext As HttpContext = HttpContext.Current
            currrentContext.Response.ContentType = "application/json"
            currrentContext.Response.Write(strserialize)
            currrentContext.Response.Flush()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    '#MA20201510 Migracion de xml a json
    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Detalle_Rec_By_IdCompra_Licencia_JSON(ByVal pIdOrdenCompra As Integer,
                                                         ByVal pLicencia As String)

        'Get_Detalle_Rec_By_IdCompra_Licencia_ = Nothing

        Try
            Dim listaDetalles As List(Of clsBeTrans_re_det)
            listaDetalles = clsLnTrans_re_det.Get_Detalle_Rec_By_IdCompra_Licencia(pIdOrdenCompra, pLicencia)

            Dim jsonResponse As String = JsonConvert.SerializeObject(listaDetalles,
            New JsonSerializerSettings With {
                .NullValueHandling = NullValueHandling.Include,
                .ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                .Formatting = Formatting.None
            })

            Dim jsonModificado As String = jsonResponse.Replace("[]", "null")
            Dim curContext As HttpContext = HttpContext.Current
            curContext.Response.Clear()
            curContext.Response.ContentType = "application/json"
            curContext.Response.StatusCode = 200
            curContext.Response.Write(jsonModificado)
            '  curContext.ApplicationInstance.CompleteRequest()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current
                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try

    End Sub

    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Producto_Talla_Color_By_Id(pIdProductoTallaColor As Integer)

        Try
            Dim TallaColor As New clsBeProducto_talla_color()
            TallaColor = clsLnProducto_talla_color.GetSingle(pIdProductoTallaColor)

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET")
            Dim strserialize As String = JsonConvert.SerializeObject(TallaColor)
            Dim currrentContext As HttpContext = HttpContext.Current
            currrentContext.Response.ContentType = "application/json"
            currrentContext.Response.Write(strserialize)
            currrentContext.Response.Flush()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Guardar_Recepcion_Caja_Master(ByVal pIdRecpecionEnc As Integer,
                                                  ByVal pIdOrdenCompra As Integer,
                                                  ByVal pIdEmpresa As Integer,
                                                  ByVal pIdBodega As Integer,
                                                  ByVal pIdUsuario As Integer,
                                                  ByVal pIdOperadorBodega As Integer,
                                                  ByVal pListaStockRec As List(Of clsBeStock_rec),
                                                  ByVal pListaDetLote As List(Of clsBeTrans_oc_det_lote),
                                                  ByVal pListaRecDet As List(Of clsBeTrans_re_det)) As String


        Guardar_Recepcion_Caja_Master = ""

        Try
            Dim vResult As String = ""

            vResult = clsLnTrans_re_enc.GuardarHH_CM(pIdRecpecionEnc,
                                                     pIdOrdenCompra,
                                                     pIdEmpresa,
                                                     pIdBodega,
                                                     pIdUsuario,
                                                     pIdOperadorBodega,
                                                     pListaStockRec,
                                                     pListaDetLote,
                                                     pListaRecDet)

            Return String.Format("Res:{0}", vResult)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#AT20251120 Packing consolidado MAMPA
    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Sub Get_Picking_Para_Emapaque_Consolidado()

        Try
            Dim lista = clsLnTarea_hh.Get_All_Picking_Para_Empaque_Consolidado()
            Dim jArray As New JArray()

            For Each item In lista
                Dim jObj As JObject = JObject.FromObject(item)

                SerializarJson(jObj, "ListaPickingDet")
                SerializarJson(jObj, "ListaPickingUbic")

                jArray.Add(jObj)
            Next

            Dim json As String = JsonConvert.SerializeObject(jArray,
                New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Include}
            )

            Dim curContext As HttpContext = HttpContext.Current
            curContext.Response.Clear()
            curContext.Response.ContentType = "application/json"
            curContext.Response.Write(json)
            curContext.ApplicationInstance.CompleteRequest()

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

        End Try

    End Sub


    '<WebMethod(), SoapHeader("mArch")>
    'Public Function Get_Ubicacion_By_Codigo_Barra_And_IdBodega(ByVal pBarra As String, ByVal pIdBodega As Integer) As clsBeBodega_ubicacion

    '    Get_Ubicacion_By_Codigo_Barra_And_IdBodega = Nothing

    '    Try

    '        Return clsLnBodega_ubicacion.Get_Ubicacion_By_Codigo_Barra_And_IdBodega(pBarra, pIdBodega)

    '    Catch ex As Exception

    '        '#MECR04112025: Se agrego bitacora de ubicacion
    '        'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
    '        Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
    '        clsLnLog_error_wms.Agregar_Error(vMsgError)

    '        Dim Mensaje As String = ex.Message
    '        WriteErrorToEventLog(Mensaje)

    '        If mArch IsNot Nothing Then

    '            If mArch.Tipo = "WM" Then
    '                Throw New Exception(Mensaje)
    '            Else
    '                Dim currrentContext As HttpContext = HttpContext.Current
    '                Dim DT As New DataTable("CustomError")
    '                DT.Columns.Add("Error", GetType(String))
    '                DT.Rows.Add(Mensaje)
    '                Dim sw As New StringWriter()
    '                DT.WriteXml(sw)
    '                HttpContext.Current.Response.Clear()
    '                HttpContext.Current.Response.StatusCode = 299
    '                HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
    '                HttpContext.Current.Response.Output.Write(sw.ToString())
    '                HttpContext.Current.Response.ContentType = "text/xml"
    '                HttpContext.Current.Response.End()
    '            End If

    '        End If

    '    End Try

    'End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_Ubicacion_By_Codigo_Barra_And_IdBodega(ByVal pBarra As String, ByVal pIdBodega As Integer) As clsBeBodega_ubicacion

        Get_Ubicacion_By_Codigo_Barra_And_IdBodega = Nothing

        Try

            Return clsLnBodega_ubicacion.Get_Ubicacion_By_Codigo_Barra_And_IdBodega(pBarra, pIdBodega)

        Catch ex As Exception

            '#MECR04112025: Se agrego bitacora de ubicacion
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            'clsLnLog_error_wms.Agregar_Error(vMsgError)
            'clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_BeProducto_By_Codigo_For_HH(ByVal pCodigo As String, ByVal IdBodega As Integer) As clsBeProducto


        Get_BeProducto_By_Codigo_For_HH = Nothing

        Try

            Return clsLnProducto.Get_BeProducto_By_Codigo(pCodigo, IdBodega)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_TipoEtiqueta_By_Id(ByVal pIdTipoEtiqueta As Integer) As clsBeTipo_etiqueta
        Try
            Return clsLnTipo_etiqueta.GetSingle_By_IdTipoEtiqueta(pIdTipoEtiqueta)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then
                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje, ex)
                Else
                    ' Error XML
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)

                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)

                    currrentContext.Response.Clear()
                    currrentContext.Response.StatusCode = 299
                    currrentContext.Response.SubStatusCode = CInt(HttpStatusCode.InternalServerError)
                    currrentContext.Response.Output.Write(sw.ToString())
                    currrentContext.Response.ContentType = "text/xml"
                    currrentContext.Response.End()

                    Return Nothing ' ✅ por consistencia (aunque End() corta la ejecución)
                End If
            End If

            Return Nothing ' ✅ si no hay mArch, igual retornar algo
        End Try
    End Function

    '#MA20251204'
    <WebMethod(), SoapHeader("mArch")>
    Public Function Operador_Tiene_Permiso(ByVal pOperador As clsBeOperador, ByVal pOpcion As String) As Boolean

        Operador_Tiene_Permiso = False

        Try
            'Si existe en el WS, RETORNA'
            Dim BeOperador = clsLnOperador.Existe(pOperador)

            If BeOperador IsNot Nothing Then
                Operador_Tiene_Permiso = clsLnOperador_bodega.Operador_Tiene_Permiso(BeOperador.IdOperador, pOpcion)
            Else
                Operador_Tiene_Permiso = False
            End If

        Catch ex As Exception
            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    ' #MA20251222 Reimprimir
    <WebMethod(), SoapHeader("mArch")>
    Public Function Obtener_Etiqueta_Verificacion_WS(ByVal pIdPedidoEnc As Integer,
                                                     ByVal pIdProductoBodega As Integer,
                                                     ByVal pLote As String,
                                                     ByVal pFechaVence As Date,
                                                     ByVal pLicPlate As String) As clsBeTrans_verificacion_etiqueta
        Try

            Return clsLnTrans_verificacion_etiqueta.Obtener_Etiqueta_Verificacion(pIdPedidoEnc,
                                                                                  pIdProductoBodega,
                                                                                  pLote,
                                                                                  pFechaVence,
                                                                                  pLicPlate)

        Catch ex As Exception
            clsLnLog_error_wms.Agregar_Error("Obtener_Etiqueta_Verificacion_WS " & ex.Message)
            Throw New Exception(ex.Message)
        End Try

    End Function

    '#CM Función creada para actualizar el Picking consolidado
    '#AT20241014 Agregue el parametro host (dispositivo id hh)
    <WebMethod(), SoapHeader("mArch")>
    Public Function Actualiza_Picking_Consolidado(ByVal pBePickingUbic As clsBeTrans_picking_ubic,
                                                  ByVal pIdOperador As Integer,
                                                  ByVal ReemplazoLP As Boolean,
                                                  ByRef pCantidad As Double,
                                                  ByRef pPeso As Double,
                                                  ByVal BeStockPallet As clsBeProducto,
                                                  ByVal host As String) As Boolean


        Actualiza_Picking_Consolidado = False

        Try

            Return clsLnTrans_picking_ubic.Actualiza_Picking_Consolidado(pBePickingUbic,
                                                                         pIdOperador,
                                                                         ReemplazoLP,
                                                                         pCantidad,
                                                                         pPeso,
                                                                         BeStockPallet,
                                                                         host)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#MA20260116
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_BeProducto_By_Codigo_Or_Barra_For_HH(ByVal pCodigo As String, ByVal IdBodega As Integer) As clsBeProducto
        Get_BeProducto_By_Codigo_Or_Barra_For_HH = Nothing
        Try
            Return clsLnProducto.Get_BeProducto_By_Codigo_Or_Barra(pCodigo, IdBodega)
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            If mArch IsNot Nothing Then
                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If
            End If
        End Try
    End Function

    '#GT18032026: validar existencia de tag
    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Obtener_Barras_Pallet_I_Nav_Lote(ByVal pListaCodigoBarraPallet As List(Of String)) As String

        Try

            Dim vLista As New List(Of clsBeI_nav_barras_pallet)
            '#GT20042026: la lista valida que exista el tag pero que no tenga un ingreso previo
            'vLista = clsLnI_nav_barras_pallet.Get_Single_By_Barra_RFID(pListaCodigoBarraPallet)
            vLista = clsLnI_nav_barras_pallet.Lista_Tags_SinIgreso_Valida(pListaCodigoBarraPallet)

            Return JsonConvert.SerializeObject(vLista)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim js As New JavaScriptSerializer()
                    Dim vError = New With {.Error = Mensaje}

                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 500
                    HttpContext.Current.Response.ContentType = "application/json; charset=utf-8"
                    HttpContext.Current.Response.Write(js.Serialize(vError))
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                    Return Nothing
                End If

            End If

            Throw New Exception(Mensaje)

        End Try

    End Function

    '#GT19032026: generar encabezado para tags en HH
    <WebMethod(), SoapHeader("mArch")>
    Public Function Guardar_Encabezado_RFID(ByVal pEncabezado As clsBeI_nav_barras_rfid_enc) As clsBeI_nav_barras_rfid_enc
        Guardar_Encabezado_RFID = Nothing

        Try

            Guardar_Encabezado_RFID = clsLnI_nav_barras_rfid_enc.Guardar_Encabezado_Con_Primer_Detalle(pEncabezado)

            Return Guardar_Encabezado_RFID

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#GT26032026: guardar detalle de tags 
    <WebMethod(), SoapHeader("mArch")>
    Public Function Agregar_Detalle_A_Encabezado_RFID(ByVal pEncabezado As clsBeI_nav_barras_rfid_enc) As Boolean
        Agregar_Detalle_A_Encabezado_RFID = False
        Try

            Agregar_Detalle_A_Encabezado_RFID = clsLnI_nav_barras_rfid_enc.Agregar_Detalle_A_Encabezado_RFID(pEncabezado)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If
        End Try
    End Function

    '#GT07042026: cargar stock para la HH
    <WebMethod(), SoapHeader("mArch")>
    Public Function Cargar_Stock_RFID_Paginado(pPagina As Integer, pTamanoPagina As Integer, pBusqueda As String, pCriterioBusqueda As String) As List(Of clsBeI_nav_barras_rfid_enc)


        Cargar_Stock_RFID_Paginado = New List(Of clsBeI_nav_barras_rfid_enc)

        Try

            Cargar_Stock_RFID_Paginado = clsLnI_nav_barras_rfid_enc.Get_Stock_WS_Paginado(pPagina, pTamanoPagina, pBusqueda, pCriterioBusqueda)


        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)

                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)

                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 500
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#GT18032026: validar tag con existencia
    <WebMethod, SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Obtener_EPC_Con_Existencia_Para_Salida(ByVal pListaCodigoBarraPallet As List(Of String)) As String
        Try

            Dim vLista As New List(Of clsBeI_nav_barras_pallet)
            vLista = clsLnI_nav_barras_pallet.Obtener_EPC_Con_Existencia_Para_Salida(pListaCodigoBarraPallet)

            Return JsonConvert.SerializeObject(vLista)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim js As New JavaScriptSerializer()
                    Dim vError = New With {.Error = Mensaje}

                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 500
                    HttpContext.Current.Response.ContentType = "application/json; charset=utf-8"
                    HttpContext.Current.Response.Write(js.Serialize(vError))
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                    Return Nothing
                End If

            End If

            Throw New Exception(Mensaje)
        End Try
    End Function

    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Get_Productos_By_IdUbicacion_JSON(ByVal pIdUbicacion As Integer,
                                                      ByVal pIdBodega As Integer) As List(Of clsBeVW_stock_res)

        Try
            Dim lStock As List(Of clsBeVW_stock_res) =
            clsLnStock.Get_All_By_IdUbicacion(pIdUbicacion, pIdBodega)

            If lStock Is Nothing Then
                lStock = New List(Of clsBeVW_stock_res)
            End If

            Dim jsonResult As String =
            JsonConvert.SerializeObject(
                lStock,
                New JsonSerializerSettings With {
                    .NullValueHandling = NullValueHandling.Include,
                    .ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    .Formatting = Formatting.None
                })

            Dim curContext As HttpContext = HttpContext.Current
            curContext.Response.Clear()
            curContext.Response.StatusCode = 200
            curContext.Response.ContentType = "application/json"
            curContext.Response.Write(jsonResult)
            ' curContext.ApplicationInstance.CompleteRequest()

            Return lStock

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)

            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega)
            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then
                '#MA20260505 Manejo de error
                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

            Return Nothing
        End Try

    End Function

    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Validar_Mismo_Producto_Posicion_JSON(ByVal pIdBodega As Integer,
                                                         ByVal pIdTramo As Integer,
                                                         ByVal pIndice_x As Integer,
                                                         ByVal pNivel As Integer,
                                                         ByVal pIdUbicacion As Integer,
                                                         ByVal pIdProductoBodega As Integer) As Object

        Try
            Dim posicionValida As Boolean = True
            Dim mensaje As String = ""
            Dim aplicaDobleProfundidad As Boolean = False

            If clsLnTrans_ubic_hh_det.Validar_Mismo_Producto_Posicion_JSON(pIdBodega,
                                                                           pIdTramo,
                                                                           pIndice_x,
                                                                           pNivel,
                                                                           pIdUbicacion,
                                                                           pIdProductoBodega,
                                                                           posicionValida,
                                                                           mensaje,
                                                                           aplicaDobleProfundidad) Then
                Dim jsonResult As String =
                                        JsonConvert.SerializeObject(
                                            New With {
                                                .PosicionValida = posicionValida,
                                                .AplicaDobleProfundidad = aplicaDobleProfundidad,
                                                .Mensaje = mensaje
                                            },
                                            New JsonSerializerSettings With {
                                                .NullValueHandling = NullValueHandling.Include,
                                                .ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                .Formatting = Formatting.None
                                            })

                Dim curContext As HttpContext = HttpContext.Current
                curContext.Response.Clear()
                curContext.Response.StatusCode = 200
                curContext.Response.ContentType = "application/json"
                curContext.Response.Write(jsonResult)
                'curContext.ApplicationInstance.CompleteRequest()

            End If

            Return Nothing

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)

            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)
            '#MA20260505 Manejo de error
            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

            Return Nothing
        End Try

    End Function

    <WebMethod(), SoapHeader("mArch"), ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=True, XmlSerializeString:=False)>
    Public Function Validar_Regla_Ubicacion_JSON(ByVal pIdProducto As Integer,
                                                 ByVal pIdUbicacion As Integer,
                                                 ByVal pIdBodega As Integer,
                                                 ByVal pIdEmpresa As Integer,
                                                 ByVal pIdEstado As Integer) As Object

        Try
            Dim ubicacionValida As Boolean = True
            Dim mensaje As String = ""

            If clsLnTrans_ubic_hh_det.Validar_Regla_Ubicacion_JSON(pIdProducto,
                                                                pIdUbicacion,
                                                                pIdBodega,
                                                                pIdEmpresa,
                                                                pIdEstado,
                                                                ubicacionValida,
                                                                mensaje) Then

                Dim jsonResult As String =
                                JsonConvert.SerializeObject(
                                    New With {
                                        .UbicacionValida = ubicacionValida,
                                        .Mensaje = mensaje
                                    },
                                    New JsonSerializerSettings With {
                                        .NullValueHandling = NullValueHandling.Include,
                                        .ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                        .Formatting = Formatting.None
                                    })

                Dim curContext As HttpContext = HttpContext.Current
                curContext.Response.Clear()
                curContext.Response.StatusCode = 200
                curContext.Response.ContentType = "application/json"
                curContext.Response.Write(jsonResult)
                'curContext.ApplicationInstance.CompleteRequest()

            End If

            Return Nothing

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            clsLnLog_error_wms_ubic.Agregar_Error(vMsgError, pStackTrace:=ex.StackTrace, pIdBodega:=pIdBodega, pIdUbicacionDestino:=pIdUbicacion)
            '#MA20260505 Manejo de error
            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim errorJson As String = JsonConvert.SerializeObject(New With {.Error = True, .Mensaje = ex.Message})
                    Dim curContext As HttpContext = HttpContext.Current

                    curContext.Response.Clear()
                    curContext.Response.StatusCode = 500
                    curContext.Response.ContentType = "application/json"
                    curContext.Response.Write(errorJson)
                    curContext.ApplicationInstance.CompleteRequest()
                End If

            End If

            Return Nothing
        End Try
    End Function

    ' #MA20260326 Validación Rack + Implosion automática
    <WebMethod(), SoapHeader("mArch")>
    Public Function Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack(ByVal pMovimiento As clsBeTrans_movimientos,
                                                                   ByVal pStockRes As clsBeVW_stock_res,
                                                                   ByRef pIdStockNuevo As Integer,
                                                                   ByRef pIdMovimientoNuevo As Integer,
                                                                   ByVal pPosiciones As Integer) As Boolean
        Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack = False


        Try
            Dim msjControl As String = "Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack: llamada de WS con usuario: " & pMovimiento.IdOperadorBodega & " y TipoTarea " & pMovimiento.IdTipoTarea
            clsLnLog_error_wms_reab.Agregar_Error(pMensajeExcepcion:=msjControl,
                                                  pIdStock:=pIdStockNuevo,
                                                  pIdMovimiento:=pIdMovimientoNuevo,
                                                  pLic_Plate:=pMovimiento.Lic_plate,
                                                  pIdProductoBodega:=pMovimiento.IdProductoBodega,
                                                  pCantidad:=pMovimiento.Cantidad)

            Return clsLnTrans_ubic_hh_det.Aplica_Cambio_Estado_Ubic_HH_ConValidacionRack(pMovimiento,
                                                                                         pStockRes,
                                                                                         pIdStockNuevo,
                                                                                         pIdMovimientoNuevo,
                                                                                         pPosiciones)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms_reab.Agregar_Error(pMensajeExcepcion:=vMsgError,
                                                  pIdStock:=pIdStockNuevo,
                                                  pIdMovimiento:=pIdMovimientoNuevo,
                                                  pLic_Plate:=pMovimiento.Lic_plate,
                                                  pIdProductoBodega:=pMovimiento.IdProductoBodega,
                                                  pCantidad:=pMovimiento.Cantidad)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#CKFK20260518 Modifique esta función
    Public Sub ConvertirListasVaciasANothing(obj As Object)
        If obj Is Nothing Then Return

        Dim tipo As System.Type = obj.GetType()

        If tipo.IsPrimitive OrElse
       tipo Is GetType(String) OrElse
       tipo Is GetType(DateTime) OrElse
       tipo Is GetType(Decimal) OrElse
       tipo.IsEnum Then
            Return
        End If

        For Each prop As Reflection.PropertyInfo In tipo.GetProperties(
        Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)

            If Not prop.CanRead OrElse Not prop.CanWrite Then Continue For
            If prop.GetIndexParameters().Length > 0 Then Continue For

            Dim valor As Object = prop.GetValue(obj, Nothing)
            Dim tipoProp As System.Type = prop.PropertyType

            If GetType(System.Collections.IList).IsAssignableFrom(tipoProp) Then

                If valor IsNot Nothing Then
                    Dim lista = DirectCast(valor, System.Collections.IList)

                    If lista.Count = 0 Then
                        prop.SetValue(obj, Nothing, Nothing)
                    Else
                        For Each item In lista
                            ConvertirListasVaciasANothing(item)
                        Next
                    End If
                End If

            ElseIf Not tipoProp.IsPrimitive AndAlso
               tipoProp IsNot GetType(String) AndAlso
               tipoProp IsNot GetType(DateTime) AndAlso
               tipoProp IsNot GetType(Decimal) AndAlso
               Not tipoProp.IsEnum Then

                If valor IsNot Nothing Then
                    ConvertirListasVaciasANothing(valor)
                End If

            End If

        Next
    End Sub

    <WebMethod(), SoapHeader("mArch")>
    Public Function Aplica_Cambio_Estado_Ubic_HH_LicCompleta_ConValidacionRack(ByVal pStockResList As List(Of clsBeVW_stock_res),
                                                                               ByVal pEsCambioEstado As Boolean) As Boolean

        Aplica_Cambio_Estado_Ubic_HH_LicCompleta_ConValidacionRack = False

        Try

            If pStockResList Is Nothing OrElse pStockResList.Count = 0 Then
                Throw New Exception("La lista enviada no contiene datos.")
            End If

            '#EJC20260416:
            'Para licencia completa se toma la primera línea solo como encabezado de contexto.
            'La composición real de la licencia se reconstruye en BD.
            Dim primeraLinea As clsBeVW_stock_res = pStockResList(0)

            If primeraLinea.Movimiento Is Nothing Then
                Throw New Exception("La línea no contiene información de movimiento.")
            End If

            If String.IsNullOrWhiteSpace(primeraLinea.Lic_plate) Then
                Throw New Exception("No se recibió la licencia.")
            End If

            Dim idStock As Integer = 0
            Dim idMov As Integer = 0

            clsLnLog_error_wms.Agregar_Error(primeraLinea.Movimiento.IdEmpresa,
                                             primeraLinea.Movimiento.IdBodegaOrigen,
                                             "Aplica_Cambio_Estado_Ubic_HH_LicCompleta_ConValidacionRack: llamada WS usuario: " &
                                             primeraLinea.Movimiento.IdOperadorBodega &
                                             " licencia: " & primeraLinea.Lic_plate)


            Dim exito As Boolean =
                clsLnTrans_ubic_hh_det.Aplica_Cambio_Estado_Ubic_HH_LicenciaCompleta_ConValidacionRack(primeraLinea.Movimiento,
                                                                                                       primeraLinea.Lic_plate,
                                                                                                       primeraLinea.IdUbicacion,
                                                                                                       primeraLinea.Movimiento.IdUbicacionDestino,
                                                                                                       idStock,
                                                                                                       idMov,
                                                                                                       0,
                                                                                                       pEsCambioEstado)

            If Not exito Then Return False

            Return True

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then
                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If
            End If
        End Try
    End Function

    '#GT30042026: inventarios exclusivos para RFID.
    ' Inventario Inicial
    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Inventario_RFID_By_IdBodega_And_IdOperador(ByVal pIdBodega As Integer, ByVal pIdOperador As Integer, pIdTarea As Integer) As List(Of clsBeTrans_inv_enc)

        Get_All_Inventario_RFID_By_IdBodega_And_IdOperador = Nothing

        Try

            Get_All_Inventario_RFID_By_IdBodega_And_IdOperador = clsLnTrans_inv_enc.Get_All_Pendientes_RFID_By_IdBodega_And_IdOperador(pIdBodega, pIdOperador, pIdTarea)

            Return Get_All_Inventario_RFID_By_IdBodega_And_IdOperador

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    <WebMethod(), SoapHeader("mArch")>
    Public Function Get_All_Pallet_Ingreso_By_IdOrdenCompraEnc_Det(ByVal pIdOrdenCompraEnc As Integer,
                                                                   ByVal pIdOrdenCompraDet As Integer) As List(Of clsBeI_nav_barras_pallet)

        Get_All_Pallet_Ingreso_By_IdOrdenCompraEnc_Det = Nothing

        Try

            Return clsLnI_nav_barras_pallet.Get_All_By_IdOrdenCompraEnc_Det(pIdOrdenCompraEnc,
                                                                            pIdOrdenCompraDet)

        Catch ex As Exception

            'Dim Mensaje As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message)
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

    '#GT11052026: guardar lecturas toma inventario ciclico RFID
    <WebMethod(), SoapHeader("mArch")>
    Public Function Guardar_Lectura_InventarioCiclico_RFID(ByVal pListaLecturas As List(Of clsBeTrans_inv_ciclico_rfid)) As Boolean
        Guardar_Lectura_InventarioCiclico_RFID = False
        Try

            Guardar_Lectura_InventarioCiclico_RFID = clsLnTrans_inv_ciclico_rfid.Actualizar_DetalleCiclico_Con_Lectura_RFID(pListaLecturas)

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If
        End Try
    End Function

    '#GT14052026: retornar cuantas pallets deben leerse en tags.
    <WebMethod(), SoapHeader("mArch")>
    Public Function GetProductosAInventariarCiclico_RFID(ByVal pIdInventarioEnc As Integer, ByVal pIdBodega As Integer) As Integer

        GetProductosAInventariarCiclico_RFID = -1

        Try

            GetProductosAInventariarCiclico_RFID = clsLnTrans_inv_ciclico_rfid.Get_Conteo_Productos(pIdInventarioEnc, pIdBodega)

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
                    HttpContext.Current.Response.StatusCode = 299
                    HttpContext.Current.Response.SubStatusCode = HttpStatusCode.InternalServerError
                    HttpContext.Current.Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.ContentType = "text/xml"
                    HttpContext.Current.Response.End()
                End If

            End If

        End Try

    End Function

End Class
