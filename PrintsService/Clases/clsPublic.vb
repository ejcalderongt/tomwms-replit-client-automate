Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text

Public Class clsPublic

    Private Const Ek64 As String = "rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5"
    Private Const Iv As String = "qualityi"

    Public Shared Function Encriptar(ByVal Input As String) As String

        Try

            Dim IV() As Byte = Encoding.ASCII.GetBytes(clsPublic.Iv) 'La clave debe ser de 8 caracteres
            Dim EncryptionKey() As Byte = Convert.FromBase64String(Ek64) 'No se puede alterar la cantidad de caracteres pero si la clave
            Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
            Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider() With {.Key = EncryptionKey, .IV = IV}
            Dim StrEncripted As String = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))
            Dim StrDesEncripted As String = Desencriptar(StrEncripted)

            If StrDesEncripted <> Input Then
                Throw New Exception("El algoritmo de encripción tipo Erik dice que no coincide el patrón")
            End If

            Return StrEncripted

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Desencriptar(ByVal Input As String) As String

        Desencriptar = ""

        Try

            Dim IV() As Byte = Encoding.ASCII.GetBytes(clsPublic.Iv) 'La clave debe ser de 8 caracteres
            Dim EncryptionKey() As Byte = Convert.FromBase64String(Ek64) 'No se puede alterar la cantidad de caracteres pero si la clave
            Dim buffer() As Byte = Convert.FromBase64String(Input)
            Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider() With {.Key = EncryptionKey, .IV = IV}
            Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

            'Length >= 12
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Sub CopyObject(Of wcf)(ByVal ObjOrigen As Object, ByRef ObjDestino As wcf)

        Try

            If ObjOrigen Is Nothing OrElse ObjDestino Is Nothing Then Return
            Dim TipoFuente As Type = ObjOrigen.[GetType]()
            Dim TipoDestino As Type = ObjDestino.[GetType]()
            For Each p As PropertyInfo In TipoFuente.GetProperties()
                Dim ObjPI As PropertyInfo = TipoDestino.GetProperty(p.Name)
                If ObjPI IsNot Nothing Then
                    ObjPI.SetValue(ObjDestino, p.GetValue(ObjOrigen, Nothing), Nothing)
                End If
            Next

        Catch ex1 As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex1.Message))
        End Try

    End Sub

    Public Shared Function EncodeString(ByVal origText As String) As String

        Try
            Dim stringBytes As Byte() = Encoding.Unicode.GetBytes(origText)
            Return Convert.ToBase64String(stringBytes, 0, stringBytes.Length)
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function DecodeString(ByVal encodedText As String) As String

        Try
            Dim stringBytes As Byte() = Convert.FromBase64String(encodedText)
            Return Encoding.Unicode.GetString(stringBytes)
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As Image
        Dim ms As New IO.MemoryStream(byteArrayIn)
        Return Image.FromStream(ms)
    End Function

    Public Shared Function ImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New IO.MemoryStream()
        imageIn.Save(ms, Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

    Public Shared Function Leer_Archivo_Configuracion_Ini(ByRef IndiceInstanciaDefecto As Integer) As List(Of clsCadenaConexion)

        Leer_Archivo_Configuracion_Ini = Nothing

        Dim oRead As IO.StreamReader = Nothing
        Dim AppPath As String = CurDir() & "\"
        Dim RutaIni As String = AppPath & "Conn.ini"
        Dim ListaInstancias As New List(Of clsCadenaConexion)
        Dim ListaInstanciasOrdenadas As New List(Of clsCadenaConexion)
        Dim BuscarHostComoInstancia As Boolean = False

        IndiceInstanciaDefecto = 0

        Try

            If IO.File.Exists(RutaIni) Then

                oRead = IO.File.OpenText(RutaIni)

                Dim Linea As String = ""
                Dim LineaAux As String = ""
                Dim PosicionIgual As Integer = 0
                Dim InstanciaBD As New clsCadenaConexion

                While oRead.Peek >= 0

                    Linea = oRead.ReadLine()

                    If Not Linea Is String.Empty Then

                        If Linea.StartsWith("#FIN") Then

                            If Not InstanciaBD.Seguridad_Integrada AndAlso (InstanciaBD.Usuario = "" OrElse InstanciaBD.Clave = "") Then
                                Throw New Exception("Error en la configuración del archivo ini, el usuario o la clave no están definidos")
                            Else
                                If ListaInstancias.Count = 0 Then
                                    InstanciaBD.Indice = 1
                                Else
                                    InstanciaBD.Indice = ListaInstancias.Count + 1
                                End If
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
                        ElseIf Linea.StartsWith("IDCONFIGURACION") Then
                            PosicionIgual = Linea.IndexOf("=")
                            LineaAux = Trim(Linea.Substring(PosicionIgual + 1, Linea.Length - PosicionIgual - 1))
                            InstanciaBD.IdConfiguracionInterface = LineaAux
                        ElseIf Linea.StartsWith("DEBUG") Then '#EJC20171031_0115AM:Configuración que permite buscar la cadena de conexión en base a la PC que ejecuta la aplicación.
                            PosicionIgual = Linea.IndexOf("=")
                            LineaAux = Trim(Linea.Substring(PosicionIgual + 1, Linea.Length - PosicionIgual - 1))
                            BuscarHostComoInstancia = IIf(LineaAux = "ON", True, False)
                        End If

                    End If 'Fin Sí, línea vacía.

                End While

                Dim vIndicePreOrdenado As Integer = -1

                If Not BuscarHostComoInstancia Then
                    vIndicePreOrdenado = ListaInstancias(0).Indice - 1
                End If

                ListaInstanciasOrdenadas = ListaInstancias.OrderBy(Function(x) x.NombreInstancia).ToList()

                If BuscarHostComoInstancia Then
                    IndiceInstanciaDefecto = Get_Instancia_Defecto(ListaInstanciasOrdenadas)
                End If

                If vIndicePreOrdenado <> -1 Then
                    IndiceInstanciaDefecto = ListaInstanciasOrdenadas.FindIndex(Function(x) x.Indice = vIndicePreOrdenado + 1)
                End If

                Return ListaInstanciasOrdenadas

            Else
                Throw New Exception("No existe archivo de configuración ini en: " & RutaIni)
            End If 'Fin existe ini

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not oRead Is Nothing Then oRead.Close()
        End Try

    End Function

    Private Shared Function Get_Instancia_Defecto(ByVal ListaInstancias As List(Of clsCadenaConexion)) As Integer

        Get_Instancia_Defecto = -1

        Try

            Dim HostName As String = Net.Dns.GetHostName()
            Dim IndiceInstanciaDefecto As Integer = -1

            IndiceInstanciaDefecto = ListaInstancias.FindIndex(Function(x) x.NombreInstancia.StartsWith(HostName.ToUpper()))

            Return IndiceInstanciaDefecto

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

End Class

Public Class SHA

    Public Shared Function GenerateSHA256String(ByVal inputString) As String

        Dim sha256 As SHA256 = SHA256.Create()
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(inputString)
        Dim hash As Byte() = sha256.ComputeHash(bytes)
        Dim stringBuilder As New StringBuilder()

        For i As Integer = 0 To hash.Length - 1
            stringBuilder.Append(hash(i).ToString("X2"))
        Next

        Return stringBuilder.ToString()

    End Function

    Public Shared Function GenerateSHA512String(ByVal inputString) As String

        Dim sha512 As SHA512 = SHA512.Create()
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(inputString)
        Dim hash As Byte() = sha512.ComputeHash(bytes)
        Dim stringBuilder As New StringBuilder()

        For i As Integer = 0 To hash.Length - 1
            stringBuilder.Append(hash(i).ToString("X2"))
        Next

        Return stringBuilder.ToString()

    End Function

End Class
