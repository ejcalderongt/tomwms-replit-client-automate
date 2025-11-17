Imports DevExpress.XtraEditors

Public Class frmPropietarioReglasMensajes

    Public pBePropietario As clsBePropietarios
    Private Registros As New System.ComponentModel.BindingList(Of RegistroSeleccion)()
    Private pObjEnc As New clsBePropietario_reglas_enc
    Private pListObjR As New List(Of clsBePropietario_reglas_det)

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
        Public Property IdProceso As Integer = 0
        Public Property ProcesoDescripcion As String = ""

        Public Property IdMensajeProceso As Integer = 0
        Public Property MensajeNombre As String = ""

        Public Property IdDestinatario As Integer = 0
        Public Property DestinatarioDescripcion As String = ""

        Public Property IsNew As Boolean = False

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

        Dim fuente = clsLnReglas_recepcion.GetAll_By_Proceso(True)

        With cmbProceso.Properties
            .DataSource = fuente
            .DisplayMember = "Descripcion"          ' lo visible en el editor
            .ValueMember = "IdReglaRecepcion"              ' lo que se guarda (EditValue)
            .NullText = "Seleccione o escriba para buscar..."
            .DropDownRows = Math.Min(10, fuente.Count)

            ' Limpiar y definir SOLO las columnas visibles en el desplegable
            .Columns.Clear()
            .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("TipoRegla", "TipoRegla"))
            .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descripcion", "Descripción"))

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
            .ValueMember = "IdMensajeRegla"      ' valor interno
            .NullText = "Seleccione un mensaje..."
            .DropDownRows = 10

            ' Limpiar y definir columnas visibles
            .Columns.Clear()
            .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdMensajeRegla", "ID"))
            .Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nombre", "Mensaje"))

            ' (Ocultas pero presentes en el origen)
            '.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdProceso", "Proceso") With {.Visible = False})
            '.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Activo", "Activo") With {.Visible = False})

            ' Permitir escritura y filtrado por texto
            .TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
            .ImmediatePopup = True
            .PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
            .SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete
            .AutoSearchColumnIndex = 1  ' búsqueda incremental por "Nombre"
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


        'Dim fuenteFiltrada As List(Of MensajeDeProceso) =
        'If(idSel = 0,
        '   New List(Of MensajeDeProceso),
        '   ListaMensajes.Where(Function(m) m.IdProceso = idSel AndAlso m.Activo).ToList())

        Dim Mensaje_por_Proceeso = clsLnMensaje_regla.GetAll_By_IdProceso(idSel)

        With cmbMensaje.Properties
            .DataSource = Mensaje_por_Proceeso
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
            .DestinatarioDescripcion = destDesc,
            .IsNew = True
        })

        gvMensajes.BestFitColumns()

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        mnuGuardar.Enabled = False
        If Datos_Correctos() Then
            If XtraMessageBox.Show("¿Guardar Regla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                End If
            End If
        End If
        mnuGuardar.Enabled = True
    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If cmbProceso.EditValue <= 0 Then
                XtraMessageBox.Show("Seleccione Proceso", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf cmbMensaje.EditValue <= 0 Then
                XtraMessageBox.Show("Seleccione Mensaje", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf Registros Is Nothing OrElse Registros.Count = 0 Then
                XtraMessageBox.Show("Ingrese reglas con un destinatario asociado", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function


    Private Function Guardar() As Boolean

        Guardar = False

        Try

            '#GT14112025: se puede manejar mas de una regla en el mismo grid
            Dim pListPropietario_Reglas_Enc = New List(Of clsBePropietario_reglas_enc)()
            Dim listaFinal As List(Of RegistroSeleccion) = Registros.ToList()

            If listaFinal.Count > 0 Then

                For Each r As RegistroSeleccion In listaFinal

                    pObjEnc = New clsBePropietario_reglas_enc()
                    Dim pObjDet = New clsBePropietario_reglas_det()

                    pObjEnc.IdPropietario = pBePropietario.IdPropietario
                    pObjEnc.IdReglaRecepcion = r.IdProceso
                    pObjEnc.IdMensajeRegla = r.IdMensajeProceso
                    pObjEnc.User_agr = AP.UsuarioAp.IdUsuario
                    pObjEnc.Fec_agr = Now
                    pObjEnc.User_mod = AP.UsuarioAp.IdUsuario
                    pObjEnc.Fec_mod = Now
                    pObjEnc.Activo = True
                    pObjEnc.IsNew = r.IsNew

                    pObjDet.IdDestinatarioPropietario = r.IdDestinatario
                    pObjDet.User_agr = AP.UsuarioAp.IdUsuario
                    pObjDet.Fec_agr = Now
                    pObjDet.User_mod = AP.UsuarioAp.IdUsuario
                    pObjDet.Fec_mod = Now
                    pObjDet.Activo = True
                    pObjDet.IsNew = r.IsNew

                    pObjEnc.ReglasDet.Add(pObjDet)
                    pListPropietario_Reglas_Enc.Add(pObjEnc)

                Next

            End If

            clsLnPropietario_reglas_enc.Guarda_Procesos(pListPropietario_Reglas_Enc)

            Return True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

End Class