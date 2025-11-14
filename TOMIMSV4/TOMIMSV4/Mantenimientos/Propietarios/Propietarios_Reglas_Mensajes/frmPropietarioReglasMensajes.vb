Imports DevExpress.XtraEditors

Public Class frmPropietarioReglasMensajes

    Public pBePropietario As clsBePropietarios
    Private Registros As New System.ComponentModel.BindingList(Of RegistroSeleccion)()

    Public Class Proceso
        Public Property IdProceso As Integer
        Public Property Nombre As String
        Public Property Tipo As String       ' Ejemplo: "Ingreso" o "Salida"
        Public Property Descripcion As String
        Public Property Activo As Boolean
    End Class

    Public Class MensajeDeProceso
        Public Property IdMensajeProceso As Integer
        Public Property IdProceso As Integer     ' Relación directa con Proceso
        Public Property Nombre As String
        Public Property Activo As Boolean
    End Class

    Public Class RegistroSeleccion
        Public Property IdProceso As Integer
        Public Property ProcesoDescripcion As String

        Public Property IdMensajeProceso As Integer
        Public Property MensajeNombre As String

        Public Property IdDestinatario As Integer
        Public Property DestinatarioDescripcion As String
    End Class



    Dim ListaProcesos As New List(Of Proceso) From {
    New Proceso With {.IdProceso = 1, .Nombre = "trans_oc_enc", .Tipo = "Ingreso", .Descripcion = "Doc. de Ingreso", .Activo = True},
    New Proceso With {.IdProceso = 2, .Nombre = "trans_re_enc", .Tipo = "Ingreso", .Descripcion = "Tarea de Recepción HH", .Activo = True},
    New Proceso With {.IdProceso = 3, .Nombre = "trans_pe_enc", .Tipo = "Pedido", .Descripcion = "Pedido", .Activo = True},
    New Proceso With {.IdProceso = 4, .Nombre = "trans_picking_enc", .Tipo = "Pedido", .Descripcion = "Picking", .Activo = True},
    New Proceso With {.IdProceso = 5, .Nombre = "trans_despacho_enc", .Tipo = "Despacho", .Descripcion = "Despacho", .Activo = True}
}

    Dim ListaMensajes As New List(Of MensajeDeProceso) From {
    New MensajeDeProceso With {.IdMensajeProceso = 1, .IdProceso = 1, .Nombre = "Documento de ingreso iniciado", .Activo = True},
    New MensajeDeProceso With {.IdMensajeProceso = 2, .IdProceso = 1, .Nombre = "Documento de ingreso completo", .Activo = True},
    New MensajeDeProceso With {.IdMensajeProceso = 3, .IdProceso = 2, .Nombre = "Tarea de recepción iniciada", .Activo = True},
    New MensajeDeProceso With {.IdMensajeProceso = 4, .IdProceso = 2, .Nombre = "Tarea de recepción completa", .Activo = True},
    New MensajeDeProceso With {.IdMensajeProceso = 5, .IdProceso = 3, .Nombre = "Pedido iniciado", .Activo = True},
    New MensajeDeProceso With {.IdMensajeProceso = 6, .IdProceso = 3, .Nombre = "Pedido completo", .Activo = True},
    New MensajeDeProceso With {.IdMensajeProceso = 7, .IdProceso = 4, .Nombre = "Picking iniciado", .Activo = True},
    New MensajeDeProceso With {.IdMensajeProceso = 8, .IdProceso = 4, .Nombre = "Picking completo", .Activo = True},
    New MensajeDeProceso With {.IdMensajeProceso = 9, .IdProceso = 5, .Nombre = "Despacho completo", .Activo = True}
}


    Private Sub Cargar_Procesos()
        Dim fuente = ListaProcesos.Where(Function(p) p.Activo).ToList()

        With cmbProceso.Properties
            .DataSource = fuente
            .DisplayMember = "Descripcion"          ' lo visible en el editor
            .ValueMember = "IdProceso"              ' lo que se guarda (EditValue)
            .NullText = "Seleccione o escriba para buscar..."
            .DropDownRows = Math.Min(10, fuente.Count)

            ' Limpiar y definir SOLO las columnas visibles en el desplegable
            .Columns.Clear()
            .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Tipo", "Tipo"))
            .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descripcion", "Descripción"))

            ' (Opcional) agregar columnas ocultas si quisieras mantenerlas para otros usos:
            ' .Columns.Add(New LookUpColumnInfo("IdProceso", "Id") With {.Visible = False})
            ' .Columns.Add(New LookUpColumnInfo("Nombre", "Nombre") With {.Visible = False})
            ' .Columns.Add(New LookUpColumnInfo("Activo", "Activo") With {.Visible = False})

            ' Permitir escribir y filtrar por contiene
            .TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
            .ImmediatePopup = True
            .PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
            .SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete
            .AutoSearchColumnIndex = 1 ' búsqueda incremental por "Descripción" (columna 1)

            ' Autoajuste del tamaño del popup y columnas al contenido
            .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
            .ShowHeader = True
        End With

        ' Ajuste inicial de ancho de columnas del popup en base a los datos actuales
        cmbProceso.Properties.BestFit()
    End Sub

    Private Sub ConfigurarLookupMensajes()
        With cmbMensaje.Properties
            .DataSource = Nothing
            .DisplayMember = "Nombre"              ' visible en el editor
            .ValueMember = "IdMensajeProceso"      ' valor interno
            .NullText = "Seleccione un mensaje..."
            .DropDownRows = 10

            ' Limpiar y definir columnas visibles
            .Columns.Clear()
            .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdMensajeProceso", "ID"))
            .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nombre", "Mensaje"))

            ' (Ocultas pero presentes en el origen)
            .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdProceso", "Proceso") With {.Visible = False})
            .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Activo", "Activo") With {.Visible = False})

            ' Permitir escritura y filtrado por texto
            .TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
            .ImmediatePopup = True
            .PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
            .SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete
            .AutoSearchColumnIndex = 1  ' búsqueda incremental por "Nombre"

            ' Ajuste automático del tamaño al contenido
            .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
            .ShowHeader = True
        End With

        ' Ajuste inicial del ancho de columnas y popup
        cmbMensaje.Properties.BestFit()
    End Sub

    Private Sub Cargar_Destinatarios()
        Try

            Dim DT As List(Of clsBePropietario_destinatario) =
       clsLnPropietario_destinatario.GetAll_Propietarios_Destinatarios(pBePropietario.IdPropietario)

            With cmbDestinatario.Properties
                .Columns.Clear()
                .DataSource = Nothing

                If DT IsNot Nothing AndAlso DT.Count > 0 Then
                    .DisplayMember = "Nombre"
                    .ValueMember = "IdDestinatarioPropietario"
                    .DataSource = DT

                    ' Columnas visibles
                    .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdDestinatarioPropietario", "ID"))
                    .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nombre", "Nombre"))

                    ' UX: escribir para buscar y abrir de inmediato
                    .TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
                    .ImmediatePopup = True
                    .PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
                    .SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete
                    .AutoSearchColumnIndex = 1 ' por "Nombre"
                    .ShowHeader = True
                    .NullText = "Seleccione o escriba para buscar..."
                    .DropDownRows = Math.Min(10, DT.Count)

                    ' Autoajuste al contenido
                    .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
                    cmbDestinatario.Properties.BestFit()   ' ajusta columnas y ancho del popup
                Else
                    .NullText = "Sin destinatarios disponibles"
                End If
            End With


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub frmPropietarioReglasMensajes_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Cargar_Procesos()
            ConfigurarLookupMensajes()
            Cargar_Destinatarios()

            GridMensajes.DataSource = Registros

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbProceso_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProceso.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbProceso.ClosePopup()   ' cierra y confirma selección si hay una fila resaltada
            ' En este punto:
            '   cmbProcesos.Text     -> muestra Descripcion
            '   cmbProcesos.EditValue-> contiene IdProceso (Integer)
        End If
    End Sub

    Private Function IdProcesoSeleccionado() As Integer
        Return If(cmbProceso.EditValue Is Nothing, 0, CInt(cmbProceso.EditValue))
    End Function

    Private Sub cmbProceso_EditValueChanged(sender As Object, e As EventArgs) Handles cmbProceso.EditValueChanged
        RefrescarMensajes()
    End Sub

    Private Sub RefrescarMensajes()
        Dim idSel = IdProcesoSeleccionado()
        Dim fuenteFiltrada As List(Of MensajeDeProceso) =
        If(idSel = 0,
           New List(Of MensajeDeProceso),
           ListaMensajes.Where(Function(m) m.IdProceso = idSel AndAlso m.Activo).ToList())

        With cmbMensaje.Properties
            .DataSource = fuenteFiltrada
            .NullText = If(idSel = 0, "Seleccione primero un proceso...", "Seleccione o escriba para buscar...")
        End With

        cmbMensaje.EditValue = Nothing
        cmbMensaje.Refresh()

        ' Ajusta el ancho automáticamente tras cambiar la fuente
        cmbMensaje.Properties.BestFit()
    End Sub

    Private Sub cmdGuardarMensaje_Click(sender As Object, e As EventArgs) Handles cmdGuardarMensaje.Click

        If cmbProceso.EditValue Is Nothing OrElse cmbMensaje.EditValue Is Nothing OrElse cmbDestinatario.EditValue Is Nothing Then
            XtraMessageBox.Show("Seleccione Proceso, Mensaje y Destinatario.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If


        ' Tomar IDs
        Dim idProc As Integer = CInt(cmbProceso.EditValue)
        Dim idMsg As Integer = CInt(cmbMensaje.EditValue)
        Dim idDest As Integer = CInt(cmbDestinatario.EditValue)


        If Registros.Any(Function(r) r.IdProceso = idProc AndAlso r.IdMensajeProceso = idMsg AndAlso r.IdDestinatario = idDest) Then
            XtraMessageBox.Show("La combinación ya existe en la lista.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Tomar descripciones/nombres desde la fila actual del LookUp (más robusto que solo Text)
        Dim procDesc As String = Convert.ToString(cmbProceso.GetColumnValue("Descripcion"))
        Dim msgNombre As String = Convert.ToString(cmbMensaje.GetColumnValue("Nombre"))
        Dim destDesc As String =
            If(cmbDestinatario.Properties.Columns.Item("Descripcion") IsNot Nothing,
               Convert.ToString(cmbDestinatario.GetColumnValue("Descripcion")),
               Convert.ToString(cmbDestinatario.GetColumnValue("Nombre"))) ' fallback si tu catálogo usa "Nombre"

        ' Agregar al BindingList (se refleja automáticamente en el Grid)
        Registros.Add(New RegistroSeleccion With {
            .IdProceso = idProc,
            .ProcesoDescripcion = procDesc,
            .IdMensajeProceso = idMsg,
            .MensajeNombre = msgNombre,
            .IdDestinatario = idDest,
            .DestinatarioDescripcion = destDesc
        })

        gvMensajes.BestFitColumns()

    End Sub
End Class