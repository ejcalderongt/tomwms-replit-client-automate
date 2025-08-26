Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Sdk.Sfc
Imports System.IO
Imports System.Text

Public Class clsBackup

    Private connection As ServerConnection
    Private server As Server
    Private db As Database

    Public Sub New(ByVal _conection As clsCadenaConexion)
        connection = New ServerConnection(_conection.Server, _conection.Usuario, _conection.Clave)
        server = New Server(connection)
        db = server.Databases(_conection.NombreBD)
    End Sub

    ''' <summary>
    '''     Genera el script de restauracion de la base de datos, con o sin datos
    ''' </summary>
    ''' <param name="file_path">Dirección del archivo</param>
    ''' <param name="script_data">Requiere o no datos</param>
    ''' <returns></returns>
    Public Iterator Function GenerateScript(ByVal file_path As String, Optional ByVal script_data As Boolean = False) As IEnumerable

        ' Configuracion del scripter
        Dim sb As StringBuilder = New StringBuilder()
        Dim scripter As Scripter = New Scripter(server)
        scripter.Options.IncludeIfNotExists = True
        scripter.Options.ScriptSchema = True
        scripter.Options.ScriptData = script_data

        ' Itera las tablas para obtener esquema y datos
        Dim iter As Integer = 0
        For Each tb As Table In db.Tables

            iter = iter + 1
            If (tb.IsSystemObject = False) Then
                For Each data As String In scripter.EnumScript(New Urn() {tb.Urn})
                    sb.Append(data + "\n\n")
                    sb.Append(Environment.NewLine)
                Next
            End If

            ' Retorna progreso
            Yield (iter * 100) / db.Tables.Count

        Next

        ' Escribe en archivo .sql
        Dim PathIter As Integer = 0
        While (File.Exists(file_path + IIf(PathIter > 0, ("_" + PathIter.ToString()), "") + ".sql"))
            PathIter = PathIter + 1
        End While

        file_path = file_path + IIf(PathIter > 0, ("_" + PathIter.ToString()), "")
        Dim fs As StreamWriter = File.CreateText(file_path + ".sql")
        fs.Write(sb.ToString())
        fs.Close()

    End Function

End Class
