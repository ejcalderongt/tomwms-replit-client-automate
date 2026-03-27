Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports DevExpress.XtraEditors

Public Class clsMantenimientoBD

    Dim vCurrenQuery As String = ""
    Private Shared vPathUpdate_BD_WMS As String = CurDir() & "\Update_BD_WMS.sql"
    Public Shared Sub Update_Database_Version(ByRef lblprg As RichTextBox)

        Dim vContadorCorrectas As Integer = 0
        Dim vContadorIncorrectas As Integer = 0

        Try

            lblprg.Visible = True

            If clsPublic.isOnline() Then

                Dim myBotNewVersionURL As String = "https://raw.githubusercontent.com/ejcalderongt/DBA/master/WMS_UPD_20231101" ' & gVersionBD
                Dim myBotNewVersionClient As Net.WebClient = New Net.WebClient()
                Dim stream As IO.Stream = Nothing

                Try
                    stream = myBotNewVersionClient.OpenRead(myBotNewVersionURL)
                Catch ex As Exception
                    If ex.Message.Contains("Error en el servidor remoto: (404) No se encontró.") Then
                        XtraMessageBox.Show("No se encontró el archivo " & myBotNewVersionURL & "en el repositorio remoto",
                        "Update BD",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
                    Else
                        Throw ex
                    End If
                End Try

                If Not stream Is Nothing Then

                    Dim reader As IO.StreamReader = New IO.StreamReader(stream)
                    Dim content As String = reader.ReadToEnd()
                    Dim sb = New StringBuilder(content.Length)

                    For Each i As Char In content

                        If i = vbLf Then
                            sb.Append(Environment.NewLine)
                        ElseIf i <> vbCr AndAlso i <> vbTab Then
                            sb.Append(i)
                        End If

                    Next

                    content = sb.ToString()

                    Actualizar_BD_WMS_By_Scrip(lblprg, content, vContadorCorrectas, vContadorIncorrectas)

                End If

            Else

                If Not IO.File.Exists(vPathUpdate_BD_WMS) Then

                    XtraMessageBox.Show("No existe archivo local de actualización de la base de datos.",
                                      "sin acceso a github",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation)

                Else

                    Dim fileContent As String = IO.File.ReadAllText(vPathUpdate_BD_WMS)
                    Actualizar_BD_WMS_By_Scrip(lblprg, fileContent, vContadorCorrectas, vContadorIncorrectas)

                End If

            End If

            If vContadorCorrectas > 0 Then

                XtraMessageBox.Show("Se ejecutaron correctamente " & vContadorCorrectas & " actualizaciones en la base de datos.",
                                      "Update versión",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information)

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Update BD",
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)

        Finally
            lblprg.Visible = False
        End Try

    End Sub
    Private Shared Sub Actualizar_Progreso(ByRef lblprg As RichTextBox, mensaje As String)
        lblprg.AppendText(mensaje & vbNewLine)
        lblprg.Refresh()
        lblprg.SelectionStart = lblprg.TextLength
        lblprg.ScrollToCaret()
    End Sub
    Private Shared Sub Actualizar_BD_WMS_By_Scrip(ByRef lblprg As RichTextBox, ByVal pScript As String, ByRef pEjecutados As Integer, ByRef pNoEjecutados As Integer)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim vContadorCorrectas As Integer = 0
        Dim vContadorInCorrectas As Integer = 0

        If String.IsNullOrEmpty(pScript) Then
            Throw New InvalidOperationException("No se pudo descargar el script de actualización.")
        End If

        ' Divide el script en bloques individuales basados en el separador "GO"
        Dim scriptBlocks As String() = pScript.Split(New String() {"GO" & Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)

        If scriptBlocks.Count > 0 Then

            Try

                lConnection.Open()

                For Each scriptBlock In scriptBlocks

                    If Not String.IsNullOrWhiteSpace(scriptBlock) AndAlso Not scriptBlock.TrimStart().StartsWith("--") Then

                        Using command As New SqlCommand(scriptBlock, lConnection)

                            Try

                                Actualizar_Progreso(lblprg, scriptBlock)
                                command.ExecuteNonQuery()
                                vContadorCorrectas += 1
                            Catch ex As Exception
                                Actualizar_Progreso(lblprg, "Error al ejecutar: " & scriptBlock)
                                vContadorInCorrectas += 1
                            End Try

                        End Using

                    End If

                Next

                pEjecutados = vContadorCorrectas
                pNoEjecutados = vContadorInCorrectas

            Catch ex As Exception
                Console.WriteLine($"Error al ejecutar el bloque de script SQL: {ex.Message}")
                vContadorInCorrectas += 1
            Finally
                lConnection.Close()
            End Try

        End If

    End Sub
    Public Shared Sub Ejecutar_Mantenimiento_Indices_BD(ByRef lblprg As RichTextBox)

        Dim lConnection As New SqlConnection(Configuration.ConfigurationManager.AppSettings("CST"))
        Dim vContadorCorrectas As Integer = 0
        Dim vContadorInCorrectas As Integer = 0
        Try

            lblprg.Visible = True

            lConnection.Open()

            Dim vSQL As String = "exec sp_index_maintenance_daily"

            Using command As New SqlCommand(vSQL, lConnection)

                Try

                    Actualizar_Progreso(lblprg, vSQL)
                    command.ExecuteNonQuery()
                    vContadorCorrectas += 1
                Catch ex As Exception
                    Actualizar_Progreso(lblprg, "Error al ejecutar: " & ex.Message)
                    vContadorInCorrectas += 1
                End Try

            End Using

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            "Update BD",
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)

        Finally
            lblprg.Visible = False
            lConnection.Close()
        End Try

    End Sub


    Public Shared Function Ejecutar_Backup_BD(nombreBackupPersonalizado As String, rutaDestino As String, ByVal pTimeOut As Integer) As Boolean
        Dim connectionString As String = Configuration.ConfigurationManager.AppSettings("CST")

        Try
            ' Obtener base de datos desde la cadena de conexión
            Dim builder As New SqlConnectionStringBuilder(connectionString)
            Dim databaseName As String = builder.InitialCatalog

            ' Validar y preparar el directorio
            If Not Directory.Exists(rutaDestino) Then
                Directory.CreateDirectory(rutaDestino)
            End If

            If Not TienePermisoEscritura(rutaDestino) Then
                Throw New UnauthorizedAccessException("No se tienen permisos de escritura en la ruta especificada.")
            End If

            ' Nombre y ruta del archivo de backup
            Dim backupFileName As String = $"{nombreBackupPersonalizado}_{DateTime.Now:yyyyMMdd_HHmmss}.bak"
            Dim fullBackupPath As String = Path.Combine(rutaDestino, backupFileName)

            ' Comando SQL para realizar el backup
            Dim backupQuery As String = $"BACKUP DATABASE [{databaseName}] TO DISK = N'{fullBackupPath}'"

            ' Ejecutar el comando
            Using lConnection As New SqlConnection(connectionString)
                Using command As New SqlCommand(backupQuery, lConnection)

                    If pTimeOut > 0 Then
                        command.CommandTimeout = pTimeOut
                    End If

                    lConnection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using

            'Console.WriteLine($"Backup exitoso. Ruta del archivo: {fullBackupPath}")
            Return True

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message),
                            "Backup BD",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
            Return False
        End Try
    End Function


    Private Shared Function TienePermisoEscritura(ruta As String) As Boolean
        Try
            ' Intentar crear un archivo temporal en la ruta especificada
            Dim archivoPrueba As String = Path.Combine(ruta, Path.GetRandomFileName())

            Using fs As FileStream = File.Create(archivoPrueba, 1, FileOptions.DeleteOnClose)
            End Using

            Return True
        Catch ex As UnauthorizedAccessException
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Shared Sub Ejecutar_Backup_BD_DEMO(ByVal pNombreBackup As String, ByVal pRutaBackup As String)

        Dim connectionString As String = Configuration.ConfigurationManager.AppSettings("CST")
        Dim serverName As String = "TuServidor"
        Dim databaseName As String = "TuBaseDeDatos"
        'Dim backupPath As String = "Ruta\Donde\Guardar\Backup"
        Dim backupPath As String = pRutaBackup

        ' Verificar o crear el directorio
        If Not Directory.Exists(backupPath) Then
            Directory.CreateDirectory(backupPath)
        End If

        Dim backupFileName As String = $"{databaseName}_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak"
        Dim backupQuery As String = $"BACKUP DATABASE [{databaseName}] TO DISK = N'{Path.Combine(backupPath, backupFileName)}'"

        Try
            Using lConnection As New SqlConnection(connectionString)
                Using command As New SqlCommand(backupQuery, lConnection)
                    lConnection.Open()
                    command.ExecuteNonQuery()
                    Console.WriteLine($"Backup exitoso. Ruta del archivo: {Path.Combine(backupPath, backupFileName)}")
                End Using
            End Using

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                            "Update BD",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class
