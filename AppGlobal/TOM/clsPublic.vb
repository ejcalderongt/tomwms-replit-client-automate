Imports System.IO
Imports System.Reflection
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions

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
            Throw ex
        End Try

    End Function
    Public Shared Function Existe_Ini() As Boolean
        Dim AppPath As String = CurDir() & "\"
        If File.Exists(AppPath & "Conn.ini") Then
            Existe_Ini = True
        Else
            Existe_Ini = False
        End If
    End Function
    Public Shared Function Desencriptar(ByVal Input As String) As String

        Desencriptar = ""

        Try

            Dim IV() As Byte = Encoding.ASCII.GetBytes(clsPublic.Iv)
            Dim EncryptionKey() As Byte = Convert.FromBase64String(Ek64)
            Dim buffer() As Byte = Convert.FromBase64String(Input)
            Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider() With {.Key = EncryptionKey, .IV = IV}

            Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

        Catch ex As Exception
            Throw ex
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
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function
    Public Shared Function DecodeString(ByVal encodedText As String) As String

        Try
            Dim stringBytes As Byte() = Convert.FromBase64String(encodedText)
            Return Encoding.Unicode.GetString(stringBytes)
        Catch ex As Exception
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
    Private Shared Function Get_Instancia_Defecto(ByVal ListaInstancias As List(Of clsCadenaConexion)) As Integer

        Get_Instancia_Defecto = -1

        Try

            Dim HostName As String = Net.Dns.GetHostName()
            Dim IndiceInstanciaDefecto As Integer = -1

            IndiceInstanciaDefecto = ListaInstancias.FindIndex(Function(x) x.NombreInstancia.StartsWith(HostName.ToUpper()))

            Return IndiceInstanciaDefecto

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Shared Function Check_MD5(ByVal file_name As String) As String
        Dim hash = MD5.Create()
        Dim hashValue() As Byte
        Dim fileStream As FileStream
        fileStream = File.Open(file_name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        fileStream.Position = 0
        hashValue = hash.ComputeHash(fileStream)
        Dim hash_hex = PrintByteArray(hashValue)
        fileStream.Close()
        Return hash_hex
    End Function

    Public Shared Function PrintByteArray(ByVal array() As Byte) As String
        Dim hex_value As String = ""
        Dim i As Integer
        For i = 0 To array.Length - 1
            hex_value += array(i).ToString("X2")
        Next i
        Return hex_value.ToLower
    End Function

    ''' <summary>
    ''' #EJC20210928: Expresión regular para limpiar caracteres no válidos (por problemas en XML Android)
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    Public Shared Function Quitar_Caracteres_No_Permitidos_original(ByVal input As String) As String

        '#GT29032022: remuevo caracteres no validos (todos menos los de la producción y dejo espacios vacios)
        Dim vTexto As String = Regex.Replace(input, "[^A-Za-z0-9\.,\-\*\u00F1\u00C1\u00F3\u00ED/ñ/Ñ/&]/gi", " ")

        'remuevo espacios vacios entre palabras
        Dim vResultText As String = Regex.Replace(vTexto, "\s{2,}", " ")

        Return vResultText

    End Function

    ''' <summary>
    ''' #CKFK20221029: Expresión regular para limpiar caracteres no válidos (por problemas en XML Android)
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    Public Shared Function Quitar_Caracteres_No_Permitidos(ByVal input As String) As String

        '#EJC20240710: Evitar que reviente por un objeto nulo.
        If input Is Nothing Then input = ""

        '#GT06072023: se remueven primero los acentos de cualquier tipo.
        Dim pInput = RemoveDiacritics(input)

        Dim regexCaracteresNoValidos As New Regex("(?:[^a-z0-9 .,-/%:]|(?<=['""&<>°])s)",
                                                   RegexOptions.IgnoreCase Or
                                                   RegexOptions.CultureInvariant Or
                                                   RegexOptions.Compiled)
        Dim vTexto As String = regexCaracteresNoValidos.Replace(pInput, String.Empty)

        Dim vResultText As String = Regex.Replace(vTexto, "\s{2,}", " ")

        Return vResultText

    End Function

    Public Shared Function RemoveDiacritics(Text As String) As String

        If Not Text Is Nothing Then
            Return New Regex("\p{Mn}", RegexOptions.Compiled).Replace(Text.Normalize(NormalizationForm.FormD), String.Empty)
        Else
            Return ""
        End If

    End Function

    Public Shared Function Quitar_Caracteres_No_Permitidos_ZPL(ByVal input As String) As String

        Dim pInput = RemoveDiacritics(input)

        '#GT27112023: remover los carros de linea
        Dim tmpInput As String = pInput.Replace("+", "")
        tmpInput = tmpInput.Replace("""", "")
        tmpInput = tmpInput.Replace("n\", "")

        Dim regexCaracteresNoValidos As New Regex("((?<=[^>])\\n)|(\\n(?=[^<]))",
                                                   RegexOptions.IgnoreCase Or
                                                   RegexOptions.CultureInvariant Or
                                                   RegexOptions.Compiled)
        Dim vTexto As String = regexCaracteresNoValidos.Replace(tmpInput, String.Empty)

        Dim vResultText As String = Regex.Replace(vTexto, "\s{2,}", " ")

        Return vResultText

    End Function

    Public Shared Function Conversion_ZPL_Codabar_to_QR(ByVal input As String) As String

        Dim tmpInput As String = input.Replace("^BY3,3,160", "^FO275,110 ^BQN,2,7 ^FH^FDMM,B0036%4$s")
        tmpInput = tmpInput.Replace("^FT670,131^BCI,,Y,N", "")
        tmpInput = tmpInput.Replace("^FD%4$s^FS", "")

        Dim temporal As String = tmpInput

        Return temporal

    End Function

    Public Shared Function Conversion_ZPL_Codabar_to_Codabar(ByVal input As String) As String

        Dim tmpInput As String = input.Replace("^BY3,3,160", "^FO240,145^BY2,2,120")
        tmpInput = tmpInput.Replace("^FT670,131", "^BCI,,Y,N")
        'tmpInput = tmpInput.Replace("^BCI,,Y,N", "")
        tmpInput = tmpInput.Replace("^FD%4$s^FS", "^FD>:100:%4$s^FS")

        Dim temporal As String = tmpInput

        Return temporal

    End Function


    Public Shared Function Leer_Archivo_Configuracion_Skin() As String

        Leer_Archivo_Configuracion_Skin = ""

        Dim oRead As StreamReader = Nothing
        Dim AppPath As String = CurDir() & "\"
        Dim RutaIni As String = AppPath & "Skin.ini"

        Try

            If File.Exists(RutaIni) Then

                oRead = File.OpenText(RutaIni)

                Dim Linea As String = ""
                Dim LineaAux As String = ""
                Dim PosicionIgual As Integer = 0
                Dim InstanciaBD As New clsCadenaConexion

                While oRead.Peek >= 0

                    Linea = oRead.ReadLine()

                    If Linea.StartsWith("SKIN") Then
                        PosicionIgual = Linea.IndexOf("=")
                        LineaAux = Trim(Linea.Substring(PosicionIgual + 1, Linea.Length - PosicionIgual - 1))
                        Leer_Archivo_Configuracion_Skin = LineaAux
                    End If

                End While

            End If 'Fin existe ini

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not oRead Is Nothing Then oRead.Close()
        End Try

    End Function

    Public Shared Function Escribir_Archivo_Configuracion_Skin(ByVal NombreSkin As String) As String

        Escribir_Archivo_Configuracion_Skin = ""

        Dim oRead As StreamReader = Nothing
        Dim AppPath As String = CurDir() & "\"
        Dim RutaIni As String = AppPath & "Skin.ini"

        Try

            Dim file As StreamWriter
            file = My.Computer.FileSystem.OpenTextFileWriter(RutaIni, False)
            file.WriteLine("SKIN =" & NombreSkin)
            file.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not oRead Is Nothing Then oRead.Close()
        End Try

    End Function

    Public Shared Sub Split_Decimal(ByVal Numero As Decimal,
                                    ByRef ParteEntera As Decimal,
                                    ByRef ParteDecimal As Decimal)

        Try

            ParteEntera = Math.Truncate(Numero)
            ParteDecimal = Numero - ParteEntera

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Public Shared Function isOnline() As Boolean

        Dim Url As New Uri("https://raw.githubusercontent.com/ejcalderongt/DBA/master/WMS_UPD_20231101")
        Dim oWebReq As Net.WebRequest
        oWebReq = Net.WebRequest.Create(Url)
        Dim oResp As Net.WebResponse = Nothing

        Try
            oWebReq.Timeout = 4000
            oResp = oWebReq.GetResponse()
            oResp.Close()
            oWebReq = Nothing
            Return True
        Catch ex As Exception
            If Not oResp Is Nothing Then oResp.Close()
            oWebReq = Nothing
            Return False
        End Try

    End Function

    Public Shared Function Abs(ByVal pValor As Double, ByVal pPermitirDecimales As Boolean) As Boolean
        ' Si el valor absoluto de pValor no es igual a su parte entera, entonces es un número decimal
        If Math.Abs(pValor) <> Math.Truncate(Math.Abs(pValor)) Then
            If Not pPermitirDecimales Then
                Throw New Exception("Error_202303101448S: El valor a insertar en stock sería un valor decimal no válido, se prevendrá continuar para evitar inconvenientes en reserva.")
            End If
            Return False
        End If
        Return True
    End Function

    Public Shared Function Leer_Archivo_Configuracion_Ini(ByRef IndiceInstanciaDefecto As Integer,
                                                          Optional ByVal EsInterface As Boolean = False) As List(Of clsCadenaConexion)

        Dim RutaIni As String = Path.Combine(CurDir(), "Conn.ini")
        Dim ListaInstancias As New List(Of clsCadenaConexion)
        Dim InstanciaBD As clsCadenaConexion = Nothing
        Dim ListaInstanciasOrdenadas As New List(Of clsCadenaConexion)
        Dim BuscarHostComoInstancia As Boolean = False

        Try

            If Not File.Exists(RutaIni) Then
                Throw New Exception("No existe archivo de configuración ini en: " & RutaIni)
            End If

            Using oRead As StreamReader = File.OpenText(RutaIni)

                Dim Linea As String

                While oRead.Peek() >= 0

                    Linea = oRead.ReadLine().Trim()

                    If String.IsNullOrEmpty(Linea) OrElse Linea.StartsWith("#FIN") Then
                        If InstanciaBD IsNot Nothing Then
                            ListaInstancias.Add(InstanciaBD)
                            InstanciaBD = Nothing
                        End If
                        If Linea.StartsWith("#FIN") Then
                            Continue While
                        End If
                    End If

                    If Linea.StartsWith("#") Then
                        InstanciaBD = New clsCadenaConexion With {.NombreInstancia = Linea.Substring(1)}
                        InstanciaBD.Modo_Debug = BuscarHostComoInstancia
                        Continue While
                    End If

                    Dim splitIndex As Integer = Linea.IndexOf("=")
                    If splitIndex = -1 Then Continue While

                    Dim key As String = Linea.Substring(0, splitIndex).Trim()
                    Dim value As String = Linea.Substring(splitIndex + 1).Trim()

                    Select Case key
                        Case "SERVIDOR_BD_WMS"
                            InstanciaBD.Server = value
                        Case "NOMBRE_BD_WMS"
                            InstanciaBD.NombreBD = value
                        Case "USUARIO_BD_WMS"
                            InstanciaBD.Usuario = value
                        Case "CLAVE_BD_WMS"
                            InstanciaBD.Clave = value
                        Case "SEGURIDAD_INTEGRADA_WMS"
                            InstanciaBD.Seguridad_Integrada = (String.Compare(value, "SI", StringComparison.OrdinalIgnoreCase) = 0)
                        Case "WSBODEGAS"
                            InstanciaBD.URLBodegas = value
                        Case "WSPRODUCTOS"
                            InstanciaBD.URLProductos = value
                        Case "WSPROVEEDORES"
                            InstanciaBD.URLProveedores = value
                        Case "WSPEDIDOSCOMPRA"
                            InstanciaBD.URLPedidosCompra = value
                        Case "WSPEDIDOSTRANSFERENCIA"
                            InstanciaBD.URLPedidosTransferencia = value
                        Case "WSGRUPOPRODUCTOS"
                            InstanciaBD.URLGruposProducto = value
                        Case "WSCATEGORIASPRODUCTOS"
                            InstanciaBD.URLCategoriasProducto = value
                        Case "WSTABLACONVERSIONES"
                            InstanciaBD.URLTablaConversiones = value
                        Case "WSLOTEPEDIDOCOMPRA"
                            InstanciaBD.URLLotePedidoCompra = value
                        Case "WSCANTPEDIDOCOMPRA"
                            InstanciaBD.URLCantidadPedidoCompra = value
                        Case "WSLOTEPEDIDOTRANSF"
                            InstanciaBD.URLLotePedidoTrans = value
                        Case "WSCANTPEDIDOTRANSF"
                            InstanciaBD.URLCantidadPedidoTrans = value
                        Case "WSREGISRECEPCOMPRA"
                            InstanciaBD.URLRegistroRecepCompra = value
                        Case "WSREGISTRANSFENVIO"
                            InstanciaBD.URLRegistroTransfEnvio = value
                        Case "WSREGISTRANSFRECEP"
                            InstanciaBD.URLRegistroTransfRecep = value
                        Case "WSAJUSTEINV"
                            InstanciaBD.URLAjusteInventario = value
                        Case "WSLOTESTRANSFREC"
                            InstanciaBD.URLLotesTransfRec = value
                        Case "WSSERIES"
                            InstanciaBD.URLSeries = value
                        Case "WSDESPACHOROAD"
                            InstanciaBD.URLDespachoRoad = value
                        Case "WSCLIENTES"
                            InstanciaBD.URLClientes = value
                        Case "WSPEDIDOSVENTA"
                            InstanciaBD.URLPedidosVenta = value
                        Case "WSENVIOSALM"
                            InstanciaBD.URLEnviosAlm = value
                        Case "WSORDENESPRODUCCION"
                            InstanciaBD.URLOrdenesProduccion = value
                        Case "WSCUWMS"
                            InstanciaBD.URLCUWMS = value
                        Case "WSCREAPICKING"
                            InstanciaBD.URLCreaPicking = value
                        Case "WSPICKING"
                            InstanciaBD.WSPicking = value
                        Case "WSRECEALM"
                            InstanciaBD.URLReceAlm = value
                        Case "WSDEVOLVENTA"
                            InstanciaBD.WSDevolucionVenta = value
                        Case "WSDEVOL"
                            InstanciaBD.WSDevolucion = value
                        Case "WSDIARIOALMACEN"
                            InstanciaBD.WSDiarioAlmacen = value
                        Case "WSUBICARALMACEN"
                            InstanciaBD.WSUbicarAlmacen = value
                        Case "WSDIMENSIONES"
                            InstanciaBD.WSDimensiones = value
                        Case "WSRECEPCIONESALM"
                            InstanciaBD.URLReceAlm = value
                        Case "TIMEOUT"
                            If Integer.TryParse(value, InstanciaBD.TimeOutConBD) = False Then
                                InstanciaBD.TimeOutConBD = 30 ' Valor predeterminado
                            End If
                        Case "WSTOMHH"
                            InstanciaBD.WSTOMHH = value
                        Case "IDCONFIGURACION"
                            Dim vIdConfiguracion As Integer
                            If Integer.TryParse(value, vIdConfiguracion) Then
                                '#EJC20260415 Se inicializa la InstanciaBD porque dio una excepcion
                                If InstanciaBD Is Nothing Then InstanciaBD = New clsCadenaConexion()
                                InstanciaBD.IdConfiguracionInterface = vIdConfiguracion
                            End If
                        Case "USUARIOWS"
                            InstanciaBD.UsuarioWS = value
                        Case "CLAVEWS"
                            InstanciaBD.ClaveWS = value
                        Case "FORMATO_DESPACHO"
                            Dim vFORMATO_INGRESO_FISCAL As Integer
                            If Integer.TryParse(value, vFORMATO_INGRESO_FISCAL) Then
                                '#GT21062024: en conn.ini no existe formato_ingreso_fiscal
                                'InstanciaBD.FORMATO_INGRESO_FISCAL = vFORMATO_INGRESO_FISCAL
                                InstanciaBD.Formato_Despacho = vFORMATO_INGRESO_FISCAL
                            End If
                        Case "FORMATO_RECEPCION"
                            Dim vFormatoRecepcion As Integer = 0
                            If Integer.TryParse(value, vFormatoRecepcion) Then
                                InstanciaBD.Formato_Recepcion = vFormatoRecepcion
                            End If
                        Case "DEBUG"
                            BuscarHostComoInstancia = (String.Compare(value, "ON", StringComparison.OrdinalIgnoreCase) = 0)
                            'InstanciaBD.ClaveWS = value
                        Case "LICENSESERVER_SAP_BO"
                            InstanciaBD.LICENSESERVER_SAP_BO = value
                        Case "SERVER_BD_SAP"
                            InstanciaBD.SERVER_BD_SAP = value
                        Case "SAP_COMPANY_DB"
                            InstanciaBD.SAP_COMPANY_DB = value
                        Case "SAP_COMPANY_DB2"
                            InstanciaBD.SAP_COMPANY_DB2 = value
                        Case "SAP_DB_USR"
                            InstanciaBD.SAP_DB_USR = value
                        Case "SAP_USR_PW"
                            InstanciaBD.SAP_USR_PW = value
                        Case "SAP_USR"
                            InstanciaBD.SAP_USR = value
                        Case "SAP_DB_PW"
                            InstanciaBD.SAP_DB_PW = value
                        Case "SAP_DB_PW"
                            InstanciaBD.SAP_DB_PW = value
                        Case "SAP_DB_PW"
                            InstanciaBD.SAP_DB_PW = value
                        Case "SAP_DB_VERSION"
                            InstanciaBD.SAP_DB_VERSION = value
                        Case "NOMBRE_BD_ERP"
                            InstanciaBD.NOMBRE_BD_ERP = value
                        Case "USUARIO_BD_ERP"
                            InstanciaBD.USUARIO_BD_ERP = value
                        Case "CLAVE_BD_ERP"
                            InstanciaBD.CLAVE_BD_ERP = value
                        Case "URL_ENTRADA_AJUSTE_POST"
                            InstanciaBD.URL_ENTRADA_AJUSTE_POST = value
                        Case "URL_SALIDA_AJUSTE_POST"
                            InstanciaBD.URL_SALIDA_AJUSTE_POST = value
                        Case "URL_VALIDA_PRODUCTOS_POST"
                            InstanciaBD.URL_VALIDA_PRODUCTOS_POST = value
                        Case "ID_STANDALONE"
                            '#GT25022025: valida si QuickTag requiere bd o no
                            Dim vId_StandAlone As Integer
                            If Integer.TryParse(value, vId_StandAlone) Then
                                InstanciaBD.ID_STANDALONE = vId_StandAlone
                            End If
                        Case "ID_EMPRESA_QUICKTAG"
                            Dim vId_QuickTag As Integer
                            If Integer.TryParse(value, vId_QuickTag) Then
                                InstanciaBD.ID_EMPRESA_QUICKTAG = vId_QuickTag
                            End If
                        Case "IMPRESORA_DEFAULT"
                            InstanciaBD.IMPRESORA_DEFAULT = value
                        Case "ID_EMPRESA_TMS"
                            Dim vIdTMS As Integer
                            If Integer.TryParse(value, vIdTMS) Then
                                InstanciaBD.ID_EMPRESA_TMS = vIdTMS
                            End If
                        Case "HANA_SL"
                            InstanciaBD.HANA_SL = value
                        Case "HANA_SL_USR"
                            InstanciaBD.HANA_SL_USR = value
                        Case "HANA_SL_PWD"
                            InstanciaBD.HANA_SL_PWD = value
                    End Select

                End While

            End Using

            Dim vIndicePreOrdenado As Integer = -1

            '#EJC202312011401: Validar por cambio de Carol. (Pendiente de prueba)
            If Not EsInterface Then
                ListaInstanciasOrdenadas = ListaInstancias.OrderBy(Function(x) x.NombreInstancia).ToList()

                If Not BuscarHostComoInstancia Then
                    If ListaInstancias.Count > 0 Then
                        vIndicePreOrdenado = ListaInstancias(0).Indice - 1
                    End If
                End If

            Else
                ListaInstanciasOrdenadas = ListaInstancias
            End If

            If BuscarHostComoInstancia Then
                IndiceInstanciaDefecto = Get_Instancia_Defecto(ListaInstanciasOrdenadas)
            End If

            Leer_Archivo_Configuracion_Ini = ListaInstancias

        Catch ex As Exception
            Throw
        End Try

        Return Leer_Archivo_Configuracion_Ini

    End Function

    Private Shared Function ObjectToByteArray(ByVal obj As Object) As Byte()
        If obj Is Nothing Then Return Nothing
        Dim bf As BinaryFormatter = New BinaryFormatter()
        Using ms As MemoryStream = New MemoryStream()
            bf.Serialize(ms, obj)
            Return ms.ToArray()
        End Using
    End Function

    Public Shared Function GetRecordHash(ByVal pObj As Object) As String

        GetRecordHash = ""

        Try

            Dim hashValue As Byte()
            Dim messageBytes As Byte() = ObjectToByteArray(pObj)
            Dim ue As UnicodeEncoding = New UnicodeEncoding()
            Dim shHash As SHA256 = SHA256.Create()
            hashValue = shHash.ComputeHash(messageBytes)

            ' sb to create string from bytes
            Dim sBuilder As New StringBuilder()

            ' convert byte data to hex string
            For n As Integer = 0 To hashValue.Length - 1
                sBuilder.Append(hashValue(n).ToString("X2"))
            Next n

            GetRecordHash = sBuilder.ToString()

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Shared Sub Actualizar_Progreso(ByRef lblPrg As RichTextBox, mensaje As String)
    '    If Not lblPrg Is Nothing Then
    '        If lblPrg.InvokeRequired Then
    '            Dim control = lblPrg
    '            control.Invoke(New MethodInvoker(Sub()
    '                                                 control.AppendText(mensaje & vbNewLine)
    '                                                 control.SelectionStart = control.TextLength
    '                                                 control.ScrollToCaret()
    '                                                 control.Refresh()
    '                                                 Application.DoEvents()
    '                                             End Sub))
    '        Else
    '            lblPrg.AppendText(mensaje & vbNewLine)
    '            lblPrg.SelectionStart = lblPrg.TextLength
    '            lblPrg.ScrollToCaret()
    '            lblPrg.Refresh()
    '            Application.DoEvents()
    '        End If
    '    End If
    'End Sub


    Public Shared Sub Actualizar_Progreso_CR(ByRef lblPrg As RichTextBox)
        lblPrg.AppendText(vbNewLine)
        lblPrg.Refresh()
        lblPrg.SelectionStart = lblPrg.TextLength
        lblPrg.ScrollToCaret()
    End Sub

    'Public Shared Sub Actualizar_Progreso(ByRef lblPrg As RichTextBox, mensaje As String, ByVal CRAntesYDespues As Boolean)
    '    If CRAntesYDespues Then Actualizar_Progreso_CR(lblPrg)
    '    lblPrg.AppendText(mensaje & vbNewLine)
    '    lblPrg.Refresh()
    '    lblPrg.SelectionStart = lblPrg.TextLength
    '    lblPrg.ScrollToCaret()
    '    Actualizar_Progreso_CR(lblPrg)
    'End Sub
    Public Shared Sub Actualizar_Progreso(ByRef lblPrg As RichTextBox, mensaje As String, Optional limpiar As Boolean = False)
        If lblPrg IsNot Nothing Then
            If limpiar Then lblPrg.Clear()

            If lblPrg.InvokeRequired Then
                Dim control = lblPrg
                control.Invoke(New MethodInvoker(Sub()
                                                     control.AppendText(mensaje & vbNewLine)
                                                     control.SelectionStart = control.TextLength
                                                     control.ScrollToCaret()
                                                     control.Refresh()
                                                     'Application.DoEvents()
                                                 End Sub))
            Else
                lblPrg.AppendText(mensaje & vbNewLine)
                lblPrg.SelectionStart = lblPrg.TextLength
                lblPrg.ScrollToCaret()
                lblPrg.Refresh()
                'Application.DoEvents()
            End If
        End If
    End Sub

End Class