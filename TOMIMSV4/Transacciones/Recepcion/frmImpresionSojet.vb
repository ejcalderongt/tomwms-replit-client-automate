Imports System.Drawing.Printing
Imports System.IO
Imports System.Net.Sockets
Imports System.Reflection
Imports System.Text
Imports DevExpress.XtraEditors
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class frmImpresionSojet

    Private Impresora As String = ""
    Private response As String = ""
    Private Empresa As String
    Private ParteUno As String
    Private ParteDos As String
    Private pImpresoraProdSeleccionada As String = ""

    Private Puerto As Integer = 0
    Private IdImpresora As Integer = 0
    Private LongitudProducto As Integer = 0
    Private pIdImpresoraSojet As Integer = 0

    Private Velocidad As Double = 0
    Private CantidadInicial As Double = 0
    Private CantidadFinal As Double = 0
    Private CantidadActual As Double = 0

    Private BeProducto As clsBeProducto = Nothing
    Private BeSource As clsBeSource = Nothing
    Private BeObject As clsBeObject = Nothing
    Private client As TcpClient = Nothing
    Private stream As NetworkStream = Nothing

    Private EtiquetaDetalle As List(Of clsBeTipo_etiqueta_detalle)
    Private ListaObjetos As New List(Of Integer)()

    Private Resultado As New clsBeResponse()
    Public pTransReDet As New clsBeTrans_re_det()

    Private Sub frmImpresionSojet_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        txtLicencia.Text = pTransReDet.Lic_plate
        txtCodigo.Text = pTransReDet.Codigo_Producto
        txtLote.Text = pTransReDet.Lote
        txtFechaVence.Text = pTransReDet.Fecha_vence
        txtDescripcion.Text = pTransReDet.Nombre_producto
        txtCantidad.Text = pTransReDet.cantidad_recibida

        Empresa = AP.NomEmpresa

        BeProducto = clsLnProducto.Get_Single_BeProducto_By_IdProductoBodega(pTransReDet.IdProductoBodega)

        CargarImpresoras()
        Cargar_Impresoras_Windows(cmbPrinterLicencia)

        cmbImpresora.EditValue = frmRecepcion.pIdImpresoraSojet
        cmbPrinterLicencia.EditValue = frmRecepcion.pImpresoraLicSeleccionada

    End Sub

    Private Sub Procesar()

        Dim Continuar As Boolean = True

        Try
            If Not ValidaConexion() Then
                MessageBox.Show("Error de conexión: Verifique la dirección Ip o Puerto.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If Not ValidarEstadoImpresion() Then
                Return
            End If

            Dim TmpVel = Double.Parse(txtVelocidad.Text)

            If TmpVel <> Velocidad Then

                Dim BePintSettings = New clsBePrintSettings()
                BePintSettings.encoder_speed = TmpVel

                Dim Datos = JsonConvert.SerializeObject(BePintSettings, Formatting.None) & vbCrLf
                Dim ResVelocidad = Enviar(Datos)

                If ResVelocidad("status").ToString.Equals("ok") Then
                    clsLnImpresora.Actualizar_Velocidad(TmpVel, IdImpresora)
                Else
                    MessageBox.Show("Error al actualizar la velocidad de impresión.", "Actualizar Velocidad", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

            End If

            Dim ResCantidadImp = GetTotalImpresion()

            If ResCantidadImp.ContainsKey("status") Then
                CantidadInicial = ResCantidadImp("status")("printhead")(0)("attribute")("total_prints")
            End If

            txtResultado.Text = ""
            EtiquetaDetalle = clsLnTipo_etiqueta_detalle.Get_Detalle_By_TipoEtiqueta(BeProducto.IdTipoEtiqueta)
            ListaObjetos.Clear()

            If EtiquetaDetalle.Count > 0 Then

                For Each Detalle As clsBeTipo_etiqueta_detalle In EtiquetaDetalle

                    LongitudProducto = pTransReDet.Nombre_producto.Length

                    If LongitudProducto > 30 Then

                        Dim Longitud As Integer = 30
                        Dim Producto As String = pTransReDet.Nombre_producto
                        ParteUno = Producto.Substring(0, Math.Min(Longitud, Producto.Length))
                        ParteDos = If(Producto.Length > Longitud, Producto.Substring(Longitud, Math.Min(Longitud, Producto.Length - Longitud)), "")

                    End If

                    If Not Detalle.Nombre.Equals("NombreProducto2") Then

                        Dim RespuestaSource = SetSource(Detalle)

                        If RespuestaSource("status").ToString.Equals("ok") Then

                            Dim RespuestaObject = SetObject(Detalle, RespuestaSource("id").ToString())

                            If RespuestaObject("status").ToString.Equals("ok") Then
                                ListaObjetos.Add(RespuestaObject("id").ToString())
                            Else
                                Continuar = False
                                Exit For
                            End If

                        Else
                            MessageBox.Show("Error al crear el source: " & Detalle.Nombre, "Enviar Source", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Continuar = False
                            Exit For
                        End If

                    End If

                    If Not String.IsNullOrEmpty(ParteDos) AndAlso LongitudProducto > 30 AndAlso Detalle.Nombre.Equals("NombreProducto2") Then

                        If Detalle IsNot Nothing Then

                            Dim RespuestaSource2 = SetSource(Detalle)

                            If RespuestaSource2("status").ToString.Equals("ok") Then

                                Dim RespuestaObject2 = SetObject(Detalle, RespuestaSource2("id").ToString)

                                If RespuestaObject2("status").ToString.Equals("ok") Then
                                    ListaObjetos.Add(RespuestaObject2("id").ToString)
                                Else
                                    Continuar = False
                                    Exit For
                                End If

                            Else
                                MessageBox.Show("Error al crear el source: " & Detalle.Nombre, "Enviar Source", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Continuar = False
                                Exit For
                            End If

                        End If

                    End If

                Next

            Else
                Continuar = False
            End If

            If Continuar Then

                Dim Exito As Integer = 0
                Dim MensajeExiste = clsLnImpresora_mensaje.GetSingleByNombre(pTransReDet.Lic_plate)

                If MensajeExiste IsNot Nothing Then

                    Dim RespuestaEliminar = Eliminar(MensajeExiste.IdMensaje)

                    If RespuestaEliminar("status").ToString.Equals("ok") Then
                        Exito = clsLnImpresora_mensaje.Eliminar(MensajeExiste)
                    End If

                End If

                If Exito > 0 OrElse MensajeExiste Is Nothing Then

                    Dim RespuestaMensaje = SetMensaje()

                    If RespuestaMensaje("status").ToString.Equals("ok") Then

                        Dim BeImpresoraMensaje = New clsBeImpresora_mensaje()
                        BeImpresoraMensaje.IdImpresoraMensaje = clsLnImpresora_mensaje.MaxID() + 1
                        BeImpresoraMensaje.IdImpresora = IdImpresora
                        BeImpresoraMensaje.Mensaje = pTransReDet.Lic_plate
                        BeImpresoraMensaje.IdMensaje = RespuestaMensaje("id")
                        BeImpresoraMensaje.Host = AP.HostName
                        BeImpresoraMensaje.User_agr = AP.UsuarioAp.IdUsuario

                        clsLnImpresora_mensaje.Insertar(BeImpresoraMensaje)

                        Dim RespuestaImpresion = Imprimir(BeImpresoraMensaje.Mensaje)

                        If RespuestaImpresion("status").ToString.Equals("ok") Then
                            If MessageBox.Show("¿Desea detener la impresión?", "Impresión Sojet", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                DetenerImpresion()
                            End If
                        End If

                    Else
                        MessageBox.Show("Error al crear el mensaje", "Enviar Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Else
                    MessageBox.Show("No se pudo eliminar el mensaje " & MensajeExiste.IdImpresoraMensaje & ". Intente de nuevo.", "Eliminar Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else
                MessageBox.Show("No se puede crear el mensaje. Intente de nuevo.", "Error en Procesar", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error:  " & ex.Message, "Error en Procesar", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function SetSource(ByVal Detalle As clsBeTipo_etiqueta_detalle) As JObject

        Dim Valor As String
        Dim Datos As String

        Try

            If Detalle.Nombre.Equals("Empresa") Then
                Valor = Empresa
            Else

                Dim TmpVal As String = pTransReDet.GetType().GetProperty(Detalle.Campo)?.GetValue(pTransReDet)?.ToString()

                Select Case Detalle.Nombre

                    Case "CodigoProducto"
                        Valor = TmpVal & " - " & pTransReDet.Lic_plate
                    Case "FechaVence"
                        Dim TmpFecha() As String = TmpVal.Split(" "c)
                        Dim Fecha As String = TmpFecha(0)

                        Valor = "Vence: " & Fecha
                    Case "Lote"
                        Valor = "Lote: " & TmpVal
                    Case "Licencia"
                        Valor = "$" & TmpVal
                    Case "NombreProducto"
                        If LongitudProducto > 30 Then
                            Valor = ParteUno
                        Else
                            Valor = TmpVal
                        End If
                    Case "NombreProducto2"
                        Valor = ParteDos
                    Case "Empresa1"
                        Valor = "TOMWMS"
                    Case Else
                        Valor = TmpVal
                End Select

            End If

            Dim BeSource As New clsBeSource With {
                .name = Detalle.Nombre,
                .attribute = New AttributeContent With {.content = Valor}
            }

            Datos = JsonConvert.SerializeObject(BeSource, Formatting.None) & vbCrLf
            Return Enviar(Datos)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error en SetSource", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Private Function SetObject(Detalle As clsBeTipo_etiqueta_detalle,
                               IdSource As Integer) As JObject
        Dim Datos As String

        Try

            BeObject = New clsBeObject()
            BeObject.name = Detalle.Nombre
            BeObject.style = New Style()
            BeObject.style.x = Detalle.Coor_x
            BeObject.style.y = Detalle.Coor_y
            BeObject.style.w = Detalle.Width
            BeObject.style.h = Detalle.Height

            If Detalle.Campo.Equals("Lic_plate") Then

                Dim Lic_Plate = "$" & pTransReDet.Lic_plate
                Dim Caracteres As Integer = Lic_Plate.Length
                Dim Milimetros As Double = 8 '9.66

                ' Calcular para 600 DPI
                Dim xDimension600 As Double = CalcularXDimensionDinamico(Caracteres, Milimetros, 600)

                BeObject.type = "barcode"
                BeObject.style.font_style = "ttf-default-r*nnn*-80-80-UTF-8"
                BeObject.style.miter_limit = 4
                BeObject.style.text_margin = 3.0
                BeObject.style.quiet_zone = 0
                BeObject.style.bearer_bar_thickness = 0
                BeObject.style.x_dimension = xDimension600
                BeObject.style.escape_seq = False
                BeObject.style.gs1_nocheck = False
                BeObject.style.dot = False
                BeObject.style.format = "data_matrix"
                BeObject.style.data_type = "unicode"
                BeObject.style.bearer_bar_type = "none"
                BeObject.style.human_readable = "bottom"
                BeObject.style.scale_x = 1
                BeObject.style.scale_y = 1

            End If

            Dim Obj As SourceItem = New SourceItem()
            Obj.id = IdSource
            BeObject.source_list.Add(Obj)

            Datos = JsonConvert.SerializeObject(BeObject, Formatting.None) & vbCrLf
            Return Enviar(Datos)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error en SetObject", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Private Function SetMensaje() As JObject

        Dim Datos As String

        Try

            Dim Mensaje = New clsBeMessage()
            Mensaje.name = pTransReDet.Lic_plate

            For Each Id In ListaObjetos
                Dim objeto As PrintObject = New PrintObject()
                objeto.id = Id
                Mensaje.object_list.Add(objeto)
            Next

            Dim Margen As PrintPrefs = New PrintPrefs()
            Margen.ff_margin = BeProducto.Margen_Impresion
            Mensaje.attribute.printdata_pref.print_prefs.Add(Margen)

            Datos = JsonConvert.SerializeObject(Mensaje, Formatting.None) & vbCrLf
            Return Enviar(Datos)

        Catch ex As Exception
            MessageBox.Show("Error al crear el mensaje." & ex.Message, "Error en Procesar", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Private Function Imprimir(Mensaje As String) As JObject

        Dim Datos As String

        Try

            Dim PrintReq = New clsBePrintData()
            PrintReq.attribute = New PrintAttributeMsg()
            PrintReq.attribute.print_data_name = Mensaje

            Datos = JsonConvert.SerializeObject(PrintReq, Formatting.None) & vbCrLf
            Return Enviar(Datos)

        Catch ex As Exception
            MessageBox.Show("Error al crear el mensaje." & ex.Message, "Error en Procesar", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Private Function Eliminar(IdMensaje As Integer) As JObject

        Dim Datos As String

        Try

            Dim ObjMensaje As New With {
                .request_type = "delete",
                .path = "/data/data",
                .id = IdMensaje
            }

            Datos = JsonConvert.SerializeObject(ObjMensaje, Formatting.None) & vbCrLf

            Return Enviar(Datos)

        Catch ex As Exception
            MessageBox.Show("Error al crear el mensaje." & ex.Message, "Error en Procesar", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Private Function Enviar(Datos As String) As JObject

        Dim jsonResponse As JObject = New JObject()

        Try

            If Not ValidaConexion() Then
                jsonResponse("status") = "error"
                jsonResponse("message") = "No hay conexión con la impresora"
                Return jsonResponse
            End If

            client = New TcpClient()
            client.Connect(Impresora, Puerto)

            If client.Connected Then

                stream = client.GetStream()

                Dim sendBytes As Byte() = Encoding.UTF8.GetBytes(Datos)
                stream.Write(sendBytes, 0, sendBytes.Length)

                Dim buffer(4096) As Byte
                stream.ReadTimeout = 90000
                Dim response As String = ""

                Try

                    Dim bytesRead As Integer = stream.Read(buffer, 0, buffer.Length)
                    response = Encoding.UTF8.GetString(buffer, 0, bytesRead)

                    txtResultado.AppendText("Petición: " & Datos & vbCrLf)
                    txtResultado.AppendText("Respuesta: " & response & vbCrLf & vbCrLf)

                Catch ex As IOException
                    jsonResponse("status") = "error"
                    jsonResponse("message") = "Tiempo de espera excedido al leer la respuesta"
                    MessageBox.Show(jsonResponse("message").ToString(), "Error de Tiempo de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                If Not String.IsNullOrEmpty(response) Then

                    Try
                        jsonResponse = JObject.Parse(response)
                    Catch ex As Exception
                        jsonResponse("status") = "error"
                        jsonResponse("message") = "Error al analizar JSON: " & ex.Message
                    End Try

                Else
                    jsonResponse("status") = "error"
                    jsonResponse("message") = "Respuesta vacía desde la impresora"
                End If

            Else
                jsonResponse("status") = "error"
                jsonResponse("message") = "No se pudo conectar con la impresora"
            End If

        Catch ex As SocketException
            jsonResponse("status") = "error"
            jsonResponse("message") = "Error de conexión: " & ex.Message
            MessageBox.Show(jsonResponse("message").ToString(), "Error de Socket", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As IOException
            jsonResponse("status") = "error"
            jsonResponse("message") = "Error de entrada/salida: " & ex.Message
            MessageBox.Show(jsonResponse("message").ToString(), "Error de E/S", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            jsonResponse("status") = "error"
            jsonResponse("message") = "Error inesperado: " & ex.Message
            MessageBox.Show(jsonResponse("message").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If stream IsNot Nothing Then stream.Close()
            If client IsNot Nothing Then client.Close()
        End Try

        Return jsonResponse

    End Function

    Private Function ValidaConexion() As Boolean

        Try

            Using client As New TcpClient()

                Dim res As IAsyncResult = client.BeginConnect(Impresora, Puerto, Nothing, Nothing)
                Dim exito As Boolean = res.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3), False)

                If Not exito Then
                    Return False
                End If

                client.EndConnect(res)

                Return True

            End Using

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub btnImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnImprimir.ItemClick
        Procesar()
    End Sub

    Private Sub CargarImpresoras()

        Try

            Dim dt As DataTable = clsLnImpresora.Get_All_Impresora_BOF(AP.IdEmpresa, AP.IdBodega)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cmbImpresora.Properties.DataSource = dt
                cmbImpresora.Properties.DisplayMember = "nombre"
                cmbImpresora.Properties.ValueMember = "IdImpresora"
                cmbImpresora.Properties.PopulateViewColumns()
                cmbImpresora.Properties.View.Columns("IdEmpresa").Visible = False
                cmbImpresora.Properties.View.Columns("user_agr").Visible = False
                cmbImpresora.Properties.View.Columns("fec_agr").Visible = False
                cmbImpresora.Properties.View.Columns("user_mod").Visible = False
                cmbImpresora.Properties.View.Columns("fec_mod").Visible = False
                cmbImpresora.Properties.View.Columns("mac_adress").Visible = False
                cmbImpresora.Properties.View.Columns("IdBodega").Visible = False
                cmbImpresora.Properties.View.Columns("numero_serie").Visible = False
                cmbImpresora.Properties.View.Columns("IdImpresoraMarca").Visible = False
                cmbImpresora.Properties.View.Columns("IdLenguaje").Visible = False
                cmbImpresora.Properties.View.Columns("IdTipoConexion").Visible = False
                cmbImpresora.Properties.View.Columns("es_movil").Visible = False
                cmbImpresora.Properties.View.Columns("activo").Visible = False
            Else
                cmbImpresora.Properties.DataSource = Nothing
            End If

        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try

    End Sub

    Public Function Cargar_Impresoras_Windows(ByRef Cmb As LookUpEdit) As Boolean

        Cargar_Impresoras_Windows = False

        Try

            Dim i As Integer
            Dim iList As New ArrayList
            Dim pkInstalledPrinters As String

            For i = 0 To PrinterSettings.InstalledPrinters.Count - 1
                pkInstalledPrinters = PrinterSettings.InstalledPrinters.Item(i)
                iList.Add(pkInstalledPrinters)
            Next

            Cmb.Properties.DataSource = iList

        Catch ex As Exception
            XtraMessageBox.Show("El servicio de impresión no está disponible o no se pudieron listar las impresoras disponibles.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function
    Private Sub cmbImpresora_EditValueChanged(sender As Object, e As EventArgs) Handles cmbImpresora.EditValueChanged

        Try

            If cmbImpresora.EditValue Is Nothing OrElse cmbImpresora.EditValue.ToString() = "" Then
                txtPuerto.Text = ""
                txtIp.Text = ""
                Impresora = ""
                Puerto = 0
                Return
            End If

            Dim Fila As DataRowView = cmbImpresora.GetSelectedDataRow()

            If Fila IsNot Nothing Then

                IdImpresora = Convert.ToInt32(Fila("IdImpresora"))
                Impresora = Fila("direccion_Ip")
                Velocidad = Fila("velocidad")
                Puerto = Fila("puerto")

                txtPuerto.Text = Puerto
                txtIp.Text = Impresora
                txtVelocidad.Text = Velocidad

                frmRecepcion.pIdImpresoraSojet = IdImpresora

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Function CalcularXDimensionDinamico(ByVal Caracteres As Integer, ByVal TamanoTotalMM As Double, ByVal dpi As Integer) As Double

        Try

            Dim ModulosPorTexto As New List(Of Tuple(Of Integer, Integer)) From {
                                            Tuple.Create(6, 10),  ' Hasta 6 caracteres → 10×10 módulos
                                            Tuple.Create(18, 14), ' Hasta 18 caracteres → 14×14 módulos
                                            Tuple.Create(24, 18), ' Hasta 24 caracteres → 18×18 módulos
                                            Tuple.Create(36, 24), ' Hasta 36 caracteres → 24×24 módulos
                                            Tuple.Create(48, 32), ' Hasta 48 caracteres → 32×32 módulos
                                            Tuple.Create(72, 36), ' Hasta 72 caracteres → 36×36 módulos
                                            Tuple.Create(98, 48)  ' Hasta 98 caracteres → 48×48 módulos
                                        }

            ' Determinar el número de módulos según la cantidad de caracteres
            Dim NumModulos As Integer = 48 ' Valor por defecto
            For Each Limite In ModulosPorTexto
                If Caracteres <= Limite.Item1 Then
                    NumModulos = Limite.Item2
                    Exit For
                End If
            Next

            ' Calcular x_dimension en mm
            Dim xDimensionMM As Double = TamanoTotalMM / NumModulos
            ' Convertir a DPI (puntos)
            Dim xDimensionDPI As Double = xDimensionMM * (dpi / 25.4)

            Return xDimensionDPI

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub ProcesarEliminar()

        Dim Realizado As Integer = 0

        Try

            Dim ListaMensajes = clsLnImpresora_mensaje.Get_All()

            If ListaMensajes.Count > 0 Then

                For Each Obj As clsBeImpresora_mensaje In ListaMensajes

                    Dim RespuestaEliminar = Eliminar(Obj.IdMensaje)

                    If RespuestaEliminar("status").ToString.Equals("ok") Then

                        clsLnImpresora_mensaje.Eliminar(Obj)

                        Realizado += 1

                    End If

                Next

            Else
                MessageBox.Show("No se encontraron etiquetas.", "Etiquetas", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error inesperado: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            MessageBox.Show("Se han elminado " & Realizado & " etiqueta(s).", "Etiquetas", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Function ValidarEstadoImpresion() As Boolean

        Dim Continuar As Boolean = True
        Dim Datos As String = "{""request_type"": ""get"", ""path"": ""/engine/real""}" & vbCrLf
        Dim Respuesta As JObject = Enviar(Datos)

        If Respuesta IsNot Nothing AndAlso Respuesta.ContainsKey("status") AndAlso Respuesta.ContainsKey("state") Then

            Dim status As String = Respuesta("status").ToString()
            Dim state As String = Respuesta("state").ToString()

            If status = "ok" AndAlso state = "started" Then

                Dim resultado As DialogResult = MessageBox.Show("Existe un proceso de impresión. ¿Desea finalizarlo?",
                                                                "Impresión Sojet",
                                                                MessageBoxButtons.YesNo,
                                                                MessageBoxIcon.Question)

                If resultado = DialogResult.Yes Then
                    DetenerImpresion()
                Else
                    Continuar = False
                End If

            End If

        Else
            MessageBox.Show("Error al obtener la respuesta de la impresora.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Continuar = False
        End If

        Return Continuar

    End Function

    Public Function GetTotalImpresion() As JObject
        Dim Datos As String = "{""request_type"": ""get"", ""path"": ""/info/status""}" & vbCrLf
        Return Enviar(Datos)
    End Function
    Private Sub btnEjecutarComando_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnEjecutarComando.ItemClick
        txtResultado.Text = ""
        Dim json As String = "{""request_type"":""get"",""path"":""/engine/real""}" & vbCrLf
        Enviar(json)
    End Sub

    Private Sub btnDetenerImpresion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDetenerImpresion.ItemClick
        DetenerImpresion()
    End Sub

    Private Sub DetenerImpresion()

        txtResultado.Text = ""

        Dim json As String = "{""request_type"":""delete"",""path"":""/engine/printjob"",""id"":0}" & vbCrLf
        Enviar(json)

        Dim ResCantidadImp = GetTotalImpresion()
        If ResCantidadImp.ContainsKey("status") Then
            CantidadFinal = ResCantidadImp("status")("printhead")(0)("attribute")("total_prints")
        End If

        If CantidadInicial > 0 AndAlso CantidadFinal > 0 Then
            CantidadActual = CantidadFinal - CantidadInicial
            txtCantidadImpresa.Text = CantidadActual
        End If

    End Sub

    Private Sub btnEliminarEtiqueta_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnEliminarEtiqueta.ItemClick
        ProcesarEliminar()
    End Sub

    Private Sub cmdImpresionLicencia_Click(sender As Object, e As EventArgs) Handles cmdImpresionLicencia.Click
        Imprimir_Licencia(pTransReDet,
                          cmbPrinterLicencia.EditValue,
                          txtCantidadLicencias.Value)
    End Sub

    Private Sub Imprimir_Licencia(ByVal pReDet As clsBeTrans_re_det,
                                  ByVal PrinterName As String,
                                  ByVal pImpresiones As Integer)

        Try

            Dim ZPLString As String = ""
            Dim vEmpresa As String = AP.Empresa.Nombre
            Dim vCodigoBarra As String = "$" & pReDet.Lic_plate.Substring(0, IIf(pReDet.Lic_plate.Length < 10, pReDet.Lic_plate.Length, 9)) ' "20240123"
            Dim vCodigoProducto As String = pReDet.Codigo_Producto
            Dim vNombreProducto As String = pReDet.Nombre_producto.Substring(0, IIf(pReDet.Nombre_producto.Length < 45, pReDet.Nombre_producto.Length, 44)) '"PRAZOLEN 20MG CAJA X 15 CAPSULAS MUY LARGO PARA"
            Dim vLote As String = pReDet.Lote
            Dim vFechaVence As String = pReDet.Fecha_vence.ToShortDateString
            Dim pTipoEtiqueta As Integer = AP.Bodega.IdTipoEtiquetaLicencia
            Dim pTipoSimbologia As Integer = AP.Bodega.IdSimbologiaLicencia
            Dim pClasificacion As Integer = 2
            Dim Tipo_Etiqueta = clsLnTipo_etiqueta.Get_Single_By_IdTipoEtiqueta(pTipoEtiqueta, pTipoSimbologia, pClasificacion)

            Dim vColaImpresiones = pImpresiones

            If PrinterName <> "" Then

                If Tipo_Etiqueta IsNot Nothing Then

                    Dim tmpZPLString = Tipo_Etiqueta.codigo_zpl

                    If tmpZPLString <> "" Then
                        ZPLString = String.Format(tmpZPLString, AP.Bodega.Codigo + " - " + AP.Bodega.Nombre,
                                                  vEmpresa,
                                                  vCodigoProducto + " - " + vNombreProducto.Trim,
                                                  vCodigoBarra,
                                                  AP.UsuarioAp.Nombres + " " + AP.UsuarioAp.Apellidos + " / " + Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                  vLote,
                                                  vFechaVence)
                    End If

                    If ZPLString <> "" Then

                        If vColaImpresiones > 0 Then

                            If vColaImpresiones = 1 Then
                                RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                            Else

                                For i = 1 To vColaImpresiones
                                    RawPrinterHelper.SendStringToPrinter(PrinterName, ZPLString)
                                Next

                            End If

                        End If

                    Else
                        XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), "No está definido el formato de etiqueta"),
                                            Text,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)
                    End If
                Else
                    Throw New Exception("GT14022024: No se cargaron las propiedades de la etiqueta.")
                End If
            Else
                DxErrorProvider1.SetError(cmbPrinterLicencia, "seleccione impresora")
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbPrinterLicencia_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPrinterLicencia.EditValueChanged
        pImpresoraProdSeleccionada = cmbPrinterLicencia.EditValue
        frmRecepcion.pImpresoraLicSeleccionada = pImpresoraProdSeleccionada
    End Sub

End Class