Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Web.Services
Imports System.Web.Services.Protocols

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

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class sync
    Inherits System.Web.Services.WebService

    Public Property mArch As New clsArchHeader

    <WebMethod(), SoapHeader("mArch")>
    Public Function Registrar_Empresa(ByVal pNombreEmpresa As String,
                                      ByVal AWSToken As String,
                                      ByVal VersionBD As String,
                                      ByVal WS_HH_QA As String,
                                      ByVal WS_HH_PRD As String) As Boolean

        Registrar_Empresa = False

        Try

            Dim BD As New AWS_WMS_UPDATEREntities
            Dim BeVersionBD As New tbl_version_bd_wms
            BeVersionBD = BD.tbl_version_bd_wms.ToList().Where(Function(x) x.IdVersionBD = Val(VersionBD)).FirstOrDefault()

            If BeVersionBD Is Nothing Then
                Throw New Exception("La versión solicitada no existe en el servidor remoto.")
            ElseIf AWSToken.Trim = "" Then
                Throw New Exception("El token no puede ser vació")
            ElseIf pNombreEmpresa = "" Then
                Throw New Exception("El nombre de empresa puede ser vació")
            Else

                Dim Empresa As New tbl_empresa_registro
                Empresa.NombreEmpresa = pNombreEmpresa
                Empresa.AWSToken = AWSToken
                Empresa.IdVersionBD = VersionBD
                Empresa.URL_WS_HH_QA = WS_HH_QA
                Empresa.URL_WS_HH_PRD = WS_HH_PRD
                Empresa.Fecha_Actualizacion = Now
                BD.tbl_empresa_registro.Add(Empresa)
                BD.SaveChanges()

                Registrar_Empresa = True

            End If

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.InnerException.Message)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
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
    Public Function Existe_Empresa(ByVal AWSToken As String) As Boolean

        Existe_Empresa = False

        Try

            Dim BD As New AWS_WMS_UPDATEREntities
            Dim BeVersionBD As New tbl_empresa_registro
            BeVersionBD = BD.tbl_empresa_registro.ToList().Where(Function(x) x.AWSToken = AWSToken).FirstOrDefault()

            If Not BeVersionBD Is Nothing Then
                Existe_Empresa = True
            End If

        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.InnerException.Message)

            Dim Mensaje As String = ex.Message
            WriteErrorToEventLog(Mensaje)

            If mArch IsNot Nothing Then

                If mArch.Tipo = "WM" Then
                    Throw New Exception(Mensaje)
                Else
                    Dim currrentContext As HttpContext = HttpContext.Current
                    Dim DT As New DataTable("CustomError")
                    DT.Columns.Add("Error", GetType(String))
                    DT.Rows.Add(Mensaje)
                    Dim sw As New StringWriter()
                    DT.WriteXml(sw)
                    HttpContext.Current.Response.Clear()
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