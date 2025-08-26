Imports System.Data.SqlClient
Public Class BaseDatos

    Public AppPath As String = CurDir() & "/"

    Private _server As String
    Public Property Server() As String
        Get
            Return _server
        End Get
        Set(ByVal value As String)
            _server = value
        End Set
    End Property


    Private _NomBD As String
    Public Property NomBD() As String
        Get
            Return _NomBD
        End Get
        Set(ByVal value As String)
            _NomBD = value
        End Set
    End Property


    Private _usuarioBD As String
    Public Property UsuarioBD() As String
        Get
            Return _usuarioBD
        End Get
        Set(ByVal value As String)
            _usuarioBD = value
        End Set
    End Property


    Private _claveBD As String
    Public Property ClaveBD() As String
        Get
            Return _claveBD
        End Get
        Set(ByVal value As String)
            _claveBD = value
        End Set
    End Property


    Private _candenaCon As String
    Public Property CadenaConexion()
        Get
            Return _candenaCon
        End Get
        Set(ByVal value)
            _candenaCon = value
        End Set
    End Property


    Public Function Existe_Ini() As Boolean
        Existe_Ini = False
        If IO.File.Exists(BD.AppPath & "Conn.ini") Then Existe_Ini = True
    End Function


    Public Function Leer_Ini() As Boolean

        Leer_Ini = False

        Try

            If IO.File.Exists(AppPath & "Conn.ini") Then

                Dim oRead As System.IO.StreamReader = IO.File.OpenText(AppPath & "Conn.ini")

                Server = oRead.ReadLine()
                NomBD = oRead.ReadLine()
                UsuarioBD = oRead.ReadLine()
                ClaveBD = oRead.ReadLine()
                IpWCFs = oRead.ReadLine

                Leer_Ini = True

                CadenaConexion = "Data Source=" & Server & _
                ";Initial Catalog=" & NomBD & _
                ";Persist Security Info=True;User ID=" & UsuarioBD & _
                ";Password=" & ClaveBD

                oRead.Close()

            Else
                Throw New Exception("No existe archivo de configuración")
            End If

        Catch ex As Exception
            Throw New Exception("Error al leer ini: " & ex.Message)
        End Try

    End Function

End Class
