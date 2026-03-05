Imports System.Reflection
Imports System.Text
Imports Newtonsoft.Json


Public Class clsHelper
    Public Enum TipoMensaje
        Info
        Exito
        Advertencia
        Error_
    End Enum

    ''' <summary>
    ''' Escribe un mensaje en el RichTextBox con color y hora.
    ''' </summary>
    ''' <param name="logBox">El RichTextBox donde escribir el log.</param>
    ''' <param name="mensaje">Mensaje a mostrar.</param>
    ''' <param name="tipo">Tipo de mensaje (Info, Error, etc.).</param>
    ''' <param name="incluirHora">Si se desea mostrar la hora actual.</param>
    Public Shared Sub LogMensaje(logBox As RichTextBox, mensaje As String, Optional tipo As TipoMensaje = TipoMensaje.Info, Optional incluirHora As Boolean = True)
        If incluirHora Then
            mensaje = $"{Now:HH:mm:ss} - {mensaje}"
        End If

        ' Determina el color del texto según el tipo de mensaje
        Dim color As Color
        Select Case tipo
            Case TipoMensaje.Info
                color = color.Black
            Case TipoMensaje.Exito
                color = color.Green
            Case TipoMensaje.Advertencia
                color = color.Blue
            Case TipoMensaje.Error_
                color = color.Red
            Case Else
                color = color.Black
        End Select

        ' Acción que escribe el texto en el RichTextBox
        Dim escribir As Action = Sub()
                                     logBox.SelectionStart = logBox.TextLength
                                     logBox.SelectionColor = color
                                     logBox.AppendText(Environment.NewLine & mensaje & Environment.NewLine)
                                     logBox.SelectionColor = logBox.ForeColor ' Restaurar color por defecto
                                     logBox.Refresh()
                                     logBox.SelectionStart = logBox.TextLength
                                     logBox.ScrollToCaret()
                                 End Sub

        ' Ejecutar en el hilo de la UI si es necesario
        If logBox.InvokeRequired Then
            logBox.Invoke(escribir)
        Else
            escribir()
        End If
    End Sub

    Public Shared Function FragmentarPorTamanoEnBytes(Of T)(lista As List(Of T), tamañoMaximoBytes As Integer) As List(Of String)
        Dim fragmentos As New List(Of String)
        Dim fragmentoActual As New List(Of T)
        Dim tamañoAcumulado As Integer = 0
        Dim encoding_ = Encoding.UTF8

        For Each item In lista
            ' Serializa el ítem individual para medir su tamaño
            Dim jsonItem As String = JsonConvert.SerializeObject(item)
            Dim tamañoItem As Integer = encoding_.GetByteCount(jsonItem)

            ' Verifica si al agregar este ítem se excede el límite
            If tamañoAcumulado + tamañoItem > tamañoMaximoBytes Then
                ' Serializa y agrega el fragmento actual a la lista final
                Dim jsonFragmento As String = JsonConvert.SerializeObject(fragmentoActual)
                fragmentos.Add(jsonFragmento)

                ' Reinicia el acumulador y el fragmento actual
                fragmentoActual = New List(Of T)
                tamañoAcumulado = 0
            End If

            fragmentoActual.Add(item)
            tamañoAcumulado += tamañoItem
        Next

        ' Agrega el último fragmento si hay elementos restantes
        If fragmentoActual.Count > 0 Then
            Dim jsonFragmento As String = JsonConvert.SerializeObject(fragmentoActual)
            fragmentos.Add(jsonFragmento)

            ' Reinicia el acumulador y el fragmento actual
            fragmentoActual = New List(Of T)
            tamañoAcumulado = 0
        End If
        Return fragmentos
    End Function

    ' Diccionario estático compartido
    Private Shared ReadOnly MapeoTablas As New Dictionary(Of String, String) From {
            {"ExportarSalidas", "trans_pe_enc"},
            {"ExportarIngresos", "trans_oc_enc"},
            {"ExportarProductos", "productos"}
}

    ' Método para obtener el nombre de la tabla desde un nombre de proceso
    Public Shared Function ObtenerNombreTabla(proceso As String) As String
        If MapeoTablas.ContainsKey(proceso) Then
            Return MapeoTablas(proceso)
        Else
            Return "Desconocido" ' o Nothing si prefieres
        End If
    End Function

    Public Shared Function GetTablasSincronizables() As List(Of String)
        Return MapeoTablas.Values.Distinct().ToList()
    End Function

    Public Shared Sub Registrar_Log_Nube(ByVal IdPropietario As Integer, ByVal pRegistrosEnviados As Integer, ByVal pRespuesta As String, ByVal pTablaSincronizada As String, Optional ByVal pTiempo As Integer = 0)
        Dim BeLogSincronizacion As New clsBeDMS_Log_sincronizacion_nube()
        Try
            BeLogSincronizacion = New clsBeDMS_Log_sincronizacion_nube()
            BeLogSincronizacion.IdLog = clsLnDMS_Log_sincronizacion_nube.MaxID() + 1
            BeLogSincronizacion.Fecha_sincronizacion = Now
            BeLogSincronizacion.User_agr = AP.UsuarioAp.IdUsuario
            BeLogSincronizacion.Fec_agr = Now
            BeLogSincronizacion.Estado = "Ok"
            BeLogSincronizacion.Entidad = pTablaSincronizada

            If pTiempo > 0 Then
                BeLogSincronizacion.Tiempo_de_envio = pTiempo
            End If

            If pRespuesta.Contains("Parcial") Then
                BeLogSincronizacion.Estado = "Parcial"
                BeLogSincronizacion.Mensaje_error = pRespuesta
            ElseIf pRespuesta.Contains("Error") Then
                BeLogSincronizacion.Estado = "Error"
                BeLogSincronizacion.Mensaje_error = pRespuesta
            End If

            clsLnDMS_Log_sincronizacion_nube.Insertar(BeLogSincronizacion)

        Catch ex As Exception
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try
    End Sub

End Class



