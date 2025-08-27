Imports System.Reflection
Imports System.Web.Configuration

Public Class ServiceConfiguracion
    Implements IServiceConfiguracion

    Public Function GetCST() As List(Of String) Implements IServiceConfiguracion.GetCST

        Dim lLista As New List(Of String)

        Try

            Dim myConfiguration As Configuration = WebConfigurationManager.OpenWebConfiguration("~")

            If myConfiguration.AppSettings.Settings.Count > 0 Then
                Dim lCST As String() = Split(myConfiguration.AppSettings.Settings.Item("CST").Value.ToString, ";")
                If lCST.Count > 0 Then
                    For Each objc As String In lCST
                        Dim litem As String() = Split(objc, "=")
                        Dim obj As String = clsPublic.Encriptar(litem(1).ToString)
                        lLista.Add(obj)
                    Next
                End If
            End If

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

        Return lLista

    End Function

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <param name="pObjC"></param>
    ''' <returns></returns>
    Public Function ModificarCST(ByVal pObjC As Configuracion) As Boolean Implements IServiceConfiguracion.ModificarCST

        ModificarCST = False

        Try

            Dim servidor As String = clsPublic.Desencriptar(pObjC.Servidor).ToString
            Dim basedatos As String = clsPublic.Desencriptar(pObjC.BaseDatos).ToString
            Dim usuario As String = clsPublic.Desencriptar(pObjC.Usuario).ToString
            Dim clave As String = clsPublic.Desencriptar(pObjC.Clave).ToString

            Dim lCST As String = String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password ={3};Persist Security Info=True", servidor, basedatos, usuario, clave)

            Dim myConfiguration As Configuration = WebConfigurationManager.OpenWebConfiguration("~")

            myConfiguration.AppSettings.Settings.Item("CST").Value = lCST
            myConfiguration.Save()

            ModificarCST = True

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Modificar Parcialmente la cadena de conexión osea el servidor y base de datos
    ''' </summary>
    ''' <param name="pObjC"></param>
    ''' <returns></returns>
    Public Function ModificarCSTParcial(ByVal pObjC As Configuracion) As Boolean Implements IServiceConfiguracion.ModificarCSTParcial

        ModificarCSTParcial = False

        Try

            Dim servidor As String = clsPublic.Desencriptar(pObjC.Servidor).ToString
            Dim basedatos As String = clsPublic.Desencriptar(pObjC.BaseDatos).ToString

            Dim lLista As List(Of String) = GetCST()

            If lLista IsNot Nothing AndAlso lLista.Count > 0 Then
                pObjC.Usuario = clsPublic.Desencriptar(lLista(2).ToString)
                pObjC.Clave = clsPublic.Desencriptar(lLista(3).ToString)
            End If

            Dim lCST As String = String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password ={3};Persist Security Info=True", servidor, basedatos, pObjC.Usuario, pObjC.Clave)

            Dim myConfiguration As Configuration = WebConfigurationManager.OpenWebConfiguration("~")

            myConfiguration.AppSettings.Settings.Item("CST").Value = lCST
            myConfiguration.Save()

            ModificarCSTParcial = True

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

    ''' <summary>
    ''' Creado por Ricardo García
    ''' </summary>
    ''' <returns></returns>
    Public Function AbreConexion() As Boolean Implements IServiceConfiguracion.AbreConexion

        AbreConexion = False

        Dim cnn As SqlClient.SqlConnection

        Try

            cnn = New SqlClient.SqlConnection(ConfigurationManager.AppSettings("CST"))
            cnn.Open()
            cnn.Close()
            AbreConexion = True

        Catch ex1 As Exception

            Try
                Throw New Exception(String.Format("{0} {1}", "Conexión a BD", ex1.Message))
            Catch ex As Exception
                Throw New Exception(String.Format("{0} {1}", "Conexión a BD", ex1.Message))
            End Try

        End Try

    End Function

    ''' <summary>
    ''' Creada por Ricardo Garcìa
    ''' </summary>
    ''' <param name="pNombreBaseDatos"></param>
    ''' <returns></returns>
    Public Function ExisteBaseDatos(ByVal pNombreBaseDatos As String) As Boolean Implements IServiceConfiguracion.ExisteBaseDatos

        Try

            Return clsServidor.ExisteBaseDatos(pNombreBaseDatos)

        Catch ex As FaultException
            Throw New FaultException(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message))
        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Function

End Class
