Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class BaseDatos
    Implements IDisposable

    Public Trans As SqlTransaction

    Public AppPath As String = CurDir() & "/"
    Public Property Server() As String
    Public Property NomBD() As String
    Public Property UsuarioBD() As String
    Public Property ClaveBD() As String
    Public Property CadenaConexion() As Object
    Public Property TimeOutConSQL As Integer = 30
    Public Property IpWCFs As String = ""

    Public Sub Dispose() Implements IDisposable.Dispose
        If Trans IsNot Nothing Then
            Trans.Dispose()
            Trans = Nothing
        End If
    End Sub
    Public Function Leer_Ini() As Boolean

        Leer_Ini = False

        Try

            If IO.File.Exists(AppPath & "Conn.ini") Then

                Dim oRead As IO.StreamReader = IO.File.OpenText(AppPath & "Conn.ini")

                Server = oRead.ReadLine()
                NomBD = oRead.ReadLine()
                UsuarioBD = oRead.ReadLine()
                ClaveBD = oRead.ReadLine()
                IpWCFs = oRead.ReadLine
                TimeOutConSQL = oRead.ReadLine

                CadenaConexion = "Data Source=" & Server &
                ";Initial Catalog=" & NomBD &
                ";Persist Security Info=True;User ID=" & UsuarioBD &
                ";Password=" & ClaveBD &
                ";Connection Timeout=" & TimeOutConSQL & "; "

                oRead.Close()

                Leer_Ini = True

            Else
                Throw New Exception("No existe archivo de configuración")
            End If

        Catch ex As Exception
            Throw New Exception("Error al leer ini: " & ex.Message)
        End Try

    End Function

    Public Function Leer_Archivo_Configuracion_INI() As Boolean

        Leer_Archivo_Configuracion_INI = False

        Dim oRead As IO.StreamReader = Nothing
        Dim RutaIni As String = AppPath & "Conn.ini"

        Try

            oRead = IO.File.OpenText(RutaIni)

            Dim Linea As String = ""
            Dim LineaAux As String = ""
            Dim PosicionIgual As Integer = 0
            Dim InstanciaBD As New clsCadenaConexion

            While oRead.Peek >= 0

                Linea = oRead.ReadLine()

                If Linea.StartsWith("#FIN") Then

                    If Not InstanciaBD.Seguridad_Integrada AndAlso (InstanciaBD.Usuario = "" OrElse InstanciaBD.Clave = "") Then
                        Throw New Exception("Error en la configuración del archivo ini, el usuario o la clave no están definidos")
                    Else
                        ListaInstancias.Add(InstanciaBD)
                    End If

                ElseIf Linea.StartsWith("#") Then
                    InstanciaBD = New clsCadenaConexion
                    Linea = Linea.Remove(0, 1)
                    InstanciaBD.NombreInstancia = Linea
                ElseIf Linea.StartsWith("SERVIDOR") Then
                    PosicionIgual = Linea.IndexOf("=")
                    LineaAux = Trim(Linea.Substring(PosicionIgual + 1, Linea.Length - PosicionIgual - 1))
                    InstanciaBD.Server = LineaAux
                    If InstanciaBD.Server = "" Then Throw New Exception("No está definido el servidor en el archivo de conexión SERVIDOR = (VACÍO)")
                ElseIf Linea.StartsWith("BD") Then
                    PosicionIgual = Linea.IndexOf("=")
                    LineaAux = Trim(Linea.Substring(PosicionIgual + 1, Linea.Length - PosicionIgual - 1))
                    InstanciaBD.NombreBD = LineaAux
                    If InstanciaBD.NombreBD = "" Then Throw New Exception("No está definida la base de datos en el archivo de conexión BD = (VACÍO)")
                ElseIf Linea.StartsWith("USUARIO") Then
                    PosicionIgual = Linea.IndexOf("=")
                    LineaAux = Trim(Linea.Substring(PosicionIgual + 1, Linea.Length - PosicionIgual - 1))
                    InstanciaBD.Usuario = LineaAux
                ElseIf Linea.StartsWith("CLAVE") Then
                    PosicionIgual = Linea.IndexOf("=")
                    LineaAux = Trim(Linea.Substring(PosicionIgual + 1, Linea.Length - PosicionIgual - 1))
                    InstanciaBD.Clave = LineaAux
                ElseIf Linea.StartsWith("SEGURIDAD_INTEGRADA") Then
                    PosicionIgual = Linea.IndexOf("=")
                    LineaAux = Trim(Linea.Substring(PosicionIgual + 1, Linea.Length - PosicionIgual - 1))
                    InstanciaBD.Seguridad_Integrada = IIf(LineaAux = "NO", 0, 1)
                ElseIf Linea.StartsWith("IPWCF") Then
                    PosicionIgual = Linea.IndexOf("=")
                    LineaAux = Trim(Linea.Substring(PosicionIgual + 1, Linea.Length - PosicionIgual - 1))
                    InstanciaBD.IpWCF = LineaAux
                    'IpWCFs = LineaAux
                    'Validate_url(IpWCFs)
                ElseIf Linea.StartsWith("TIMEOUT") Then
                    PosicionIgual = Linea.IndexOf("=")
                    LineaAux = Trim(Linea.Substring(PosicionIgual + 1, Linea.Length - PosicionIgual - 1))
                    InstanciaBD.TimeOutConBD = LineaAux
                End If

            End While

            If ListaInstancias.Count = 0 Then
                Throw New Exception("No se encontró una configuración válida para generar la cadena de conexión en el archivo: " & RutaIni)
            Else

                'Tomar por
                '
                'la primera instancia.
                BD.Server = ListaInstancias(0).Server
                BD.NomBD = ListaInstancias(0).NombreBD
                BD.UsuarioBD = ListaInstancias(0).Usuario
                BD.ClaveBD = ListaInstancias(0).Clave
                BD.CadenaConexion = ListaInstancias(0).CadenaConexionSQLClient
                'IpWCFs = ListaInstancias(0).IpWCF

                Leer_Archivo_Configuracion_INI = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not oRead Is Nothing Then oRead.Close()
        End Try

    End Function

    '=http://ec2-52-32-154-252.us-west-2.compute.amazonaws.com/TOMWCF/

    ''' <summary>
    ''' Ejecuta una sentencia SQL de tipo DDL, devuelve el número de filas afectadas
    ''' </summary>
    ''' <param name="pSQL"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Xcute(ByVal pSql As String) As Integer

        Dim conn As New SqlConnection

        Try

            conn.ConnectionString = CadenaConexion
            conn.Open()

            Dim cmd As New SqlCommand
            cmd = conn.CreateCommand
            cmd.CommandText = pSql
            If Not Trans Is Nothing Then cmd.Transaction = Trans
            Xcute = cmd.ExecuteNonQuery()
            cmd.Dispose()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Function

    Public Shared Function Xcute(ByVal pSQL$, ByRef lTrans As SqlTransaction, ByRef Conn As SqlConnection) As Double

        Dim cmd As New SqlCommand(pSQL, Conn)

        Try

            cmd.CommandTimeout = 30
            If Not lTrans Is Nothing Then cmd.Transaction = lTrans
            Xcute = cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    ''' <summary>
    ''' Ejecuta sentencias "SELECT"  en la base de datos.
    ''' </summary>
    ''' <param name="DT"></param>
    ''' <param name="vSQL"></param>
    ''' <remarks></remarks>
    Public Sub OpenDT(ByRef dT As DataTable, ByVal vSql As String)

        Dim conn As New SqlConnection

        Try

            conn.ConnectionString = CadenaConexion
            conn.Open()

            Dim dAdapter As New SqlDataAdapter(vSql, conn)
            Dim dSet As New DataSet
            dAdapter.Fill(dSet, "Query")
            dT = dSet.Tables("Query")
            dAdapter.Dispose()
            dSet.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

End Class
