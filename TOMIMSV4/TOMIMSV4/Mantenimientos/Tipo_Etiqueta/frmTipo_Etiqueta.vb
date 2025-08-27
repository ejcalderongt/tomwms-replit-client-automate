Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Text
Imports DevExpress.XtraEditors


Public Class frmTipo_Etiqueta
    Public ObjTE As New clsBeTipo_etiqueta
    Public ObjDetalle As clsBeTipo_etiqueta_detalle = Nothing
    Public Delegate Sub ListarEtiquetas()
    Public Property InvokeListarEtiquetas As ListarEtiquetas
    Private DTDetalleEtiqueta As New DataTable("DetalleEtiqueta")
    Private ListaDetalle As List(Of clsBeTipo_etiqueta_detalle)

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()

        DTDetalleEtiqueta.Columns.Add("IdTipoEtiquetaDetalle", GetType(Integer))
        DTDetalleEtiqueta.Columns.Add("Nombre", GetType(String))
        DTDetalleEtiqueta.Columns.Add("Campo", GetType(String))
        DTDetalleEtiqueta.Columns.Add("Coor_x", GetType(Double))
        DTDetalleEtiqueta.Columns.Add("Coor_y", GetType(Double))
        DTDetalleEtiqueta.Columns.Add("Ancho", GetType(Double))
        DTDetalleEtiqueta.Columns.Add("Alto", GetType(Double))
    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        If Guardar() Then
            XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarEtiquetas.Invoke
            Close()
            'LabelControl2.Appearance.TextOptions.HAlignment=DevExpress.Utils.HorzAlignment.Center
        End If
    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            ObjTE = New clsBeTipo_etiqueta() With {
                .IdTipoEtiqueta = clsLnTipo_etiqueta.MaxID(),
                .Nombre = txtNombre.Text.Trim(),
                .Alto = txtAlto.Value,
                .Ancho = txtAncho.Value,
                .MargenIzq = txtMargenIzq.Value,
                .MagenDer = txtMargenDer.Value,
                .MargenSup = txtMargenSup.Value,
                .MargenInf = txtMargenInf.Value,
                .User_agr = AP.UsuarioAp.IdUsuario,
                .Fec_agr = Now,
                .User_mod = AP.UsuarioAp.IdUsuario,
                .Fec_mod = Now,
                .Activo = True,
                .dpi = cmbDPI.EditValue,
                .codigo_zpl = clsPublic.Quitar_Caracteres_No_Permitidos_ZPL(txtZPL.Text),
                .Idclasificacion_etiqueta = cmbClasificacionEtiqueta.EditValue,
                .Es_Inkjet = chkInkjet.Checked
            }

            Guardar = clsLnTipo_etiqueta.Insertar(ObjTE) > 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            ObjTE.Nombre = txtNombre.Text.Trim()
            ObjTE.Alto = txtAlto.Value
            ObjTE.Ancho = txtAncho.Value
            ObjTE.MargenIzq = txtMargenIzq.Value
            ObjTE.MagenDer = txtMargenDer.Value
            ObjTE.MargenSup = txtMargenSup.Value
            ObjTE.MargenInf = txtMargenInf.Value
            ObjTE.User_mod = AP.UsuarioAp.IdUsuario
            ObjTE.Fec_mod = Now
            ObjTE.Activo = True
            ObjTE.dpi = cmbDPI.EditValue
            ObjTE.codigo_zpl = clsPublic.Quitar_Caracteres_No_Permitidos_ZPL(txtZPL.Text)
            ObjTE.Idclasificacion_etiqueta = cmbClasificacionEtiqueta.EditValue
            ObjTE.Es_Inkjet = chkInkjet.Checked

            Actualizar = clsLnTipo_etiqueta.Actualizar(ObjTE) > 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarEtiquetas.Invoke
            Close()
        End If
    End Sub

    Private Sub frmTipo_Etiqueta_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            '#GT18092023: llena las opciones de dpi para generar etiqueta
            Llenar_combo_DPI()
            '#GT18092023: combo para identificar si es etiqueta para ubicacion o producto/LP
            Llenar_combo_clasificacion_etiqueta()
            Get_TipoEtiquetas()

            Select Case Modo
                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnTipo_etiqueta.MaxID
                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False



                Case TipoTrans.Editar

                    clsLnTipo_etiqueta.Obtener(ObjTE)

                    lblCodigo.Text = ObjTE.IdTipoEtiqueta
                    txtNombre.Text = ObjTE.Nombre
                    txtAlto.Value = ObjTE.Alto
                    txtAncho.Value = ObjTE.Ancho
                    txtMargenIzq.Value = ObjTE.MargenIzq
                    txtMargenDer.Value = ObjTE.MagenDer
                    txtMargenInf.Value = ObjTE.MargenInf
                    txtMargenSup.Value = ObjTE.MargenSup
                    chkActivo.Checked = ObjTE.Activo
                    chkInkjet.Checked = ObjTE.Es_Inkjet

                    User_agrTextEdit.Text = ObjTE.User_agr
                    Fec_agrDateEdit.Text = ObjTE.Fec_agr
                    User_modTextEdit.Text = ObjTE.User_mod
                    Fec_modDateEdit.Text = ObjTE.Fec_mod
                    txtZPL.Text = ObjTE.codigo_zpl

                    '#GT30082022: set de la densidad de impresion sobre la etiqueta
                    If ObjTE.dpi > 0 Then
                        cmbDPI.EditValue = ObjTE.dpi
                    Else
                        cmbDPI.EditValue = -1
                    End If

                    If ObjTE.Idclasificacion_etiqueta > 0 Then
                        cmbClasificacionEtiqueta.EditValue = ObjTE.Idclasificacion_etiqueta
                        cmbClasificacionEtiqueta.Enabled = False
                    Else
                        cmbClasificacionEtiqueta.Enabled = True
                    End If

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

            Get_Detalle_Tipo_Etiqueta()
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

        Me.Focus()
        txtNombre.Focus()
    End Sub

    Private Sub Llenar_combo_clasificacion_etiqueta()

        Try

            cmbClasificacionEtiqueta.Properties.DisplayMember = "Descripcion"
            cmbClasificacionEtiqueta.Properties.ValueMember = "Idclasificacion_etiqueta"
            cmbClasificacionEtiqueta.Properties.DataSource = clsLnProducto.Get_Producto_Clasificacion_Etiqueta()
            cmbClasificacionEtiqueta.ItemIndex = 0
            cmbClasificacionEtiqueta.Properties.BestFit()


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Get_Detalle_Tipo_Etiqueta()
        Try
            ListaDetalle = clsLnTipo_etiqueta_detalle.Get_All()

            gcLista.DataSource = Nothing

            If ListaDetalle.Count > 0 Then
                DTDetalleEtiqueta.Rows.Clear()

                For Each Obj As clsBeTipo_etiqueta_detalle In ListaDetalle

                    DTDetalleEtiqueta.Rows.Add(Obj.IdTipoEtiquetaDetalle,
                                               Obj.Nombre,
                                               Obj.Campo,
                                               Obj.Coor_x,
                                               Obj.Coor_y,
                                               Obj.Width,
                                               Obj.Height)
                Next

                gcLista.DataSource = DTDetalleEtiqueta
            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub Get_TipoEtiquetas()
        Try
            cmbTipoEtiqueta.Properties.DisplayMember = "Nombre"
            cmbTipoEtiqueta.Properties.ValueMember = "IdTipoEtiqueta"
            cmbTipoEtiqueta.Properties.DataSource = clsLnTipo_etiqueta.GetAllForCombo()
            cmbTipoEtiqueta.ItemIndex = 0
            cmbTipoEtiqueta.Properties.BestFit()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Categories As New List(Of Densidad_Impresion)()
    Private Sub Llenar_combo_DPI()

        Try

            Categories.Add(New Densidad_Impresion() With {.ID = 6, .CategoryName = "152 dpi"})
            Categories.Add(New Densidad_Impresion() With {.ID = 8, .CategoryName = "203 dpi"})
            Categories.Add(New Densidad_Impresion() With {.ID = 12, .CategoryName = "300 dpi"})
            Categories.Add(New Densidad_Impresion() With {.ID = 24, .CategoryName = "600 dpi"})

            cmbDPI.Properties.DataSource = Categories
            cmbDPI.Properties.ValueMember = "ID"
            cmbDPI.Properties.DisplayMember = "CategoryName"
            cmbDPI.ItemIndex = 0
            cmbDPI.Properties.BestFit()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Public Class Densidad_Impresion
        Public Property ID() As Integer
        Public Property CategoryName() As String

    End Class


    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Desactivar el Tipo Etiqueta", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                ObjTE.Activo = False
                If clsLnTipo_etiqueta.Actualizar(ObjTE) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarEtiquetas.Invoke
                    Close()
                    frmTipoTarima_List.Dgrid.Refresh()
                End If
            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then TablasRelacionadas("tipo_etiqueta", ObjTE.IdTipoEtiqueta)
        End Try

    End Sub

    Private Sub txtAlto_ValueChanged(sender As Object, e As EventArgs) Handles txtAlto.ValueChanged
        lblpAlto.Text = String.Format("Alto: {0} pulg {1} Alto: {2} cm {1} Alto: {3} mm ", txtAlto.Value, vbNewLine, txtAlto.Value * 2.54, txtAlto.Value * 2.54 * 100)
    End Sub

    Private Sub txtAncho_ValueChanged(sender As Object, e As EventArgs) Handles txtAncho.ValueChanged
        lblpAncho.Text = String.Format("Ancho: {0} pulg {1} Ancho: {2} cm {1} Ancho: {3} mm ", txtAncho.Value, vbNewLine, txtAncho.Value * 2.54, txtAncho.Value * 2.54 * 100)
    End Sub

    Private Sub frmTipo_Etiqueta_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub btnGenerarImagen_Click(sender As Object, e As EventArgs) Handles btnGenerarImagen.Click

        Try
            If txtZPL.Text.Length = 0 OrElse txtZPL.Text = "" Then
                XtraMessageBox.Show("No se ha colocado ningún código ZPL para generar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                pbZPLImage.Image = Nothing

                'Dim pZPL As String = clsPublic.Conversion_ZPL_Codabar_to_QR(txtZPL.Text)
                'Dim pZPL As String = clsPublic.Conversion_ZPL_Codabar_to_Codabar(txtZPL.Text)

                Dim zpl() As Byte = Encoding.UTF8.GetBytes(txtZPL.Text)

                '#GT18092023: define las medidas de la etiqueta para la API
                Dim alto = txtAlto.Text.ToString()
                Dim ancho = txtAncho.Text.ToString()
                Dim medida = ancho & "x" & alto
                '#GT18092023: define densidad de ipmresión para la API
                Dim pDPI = cmbDPI.EditValue

                Dim request As HttpWebRequest = WebRequest.Create("http://api.labelary.com/v1/printers/" & pDPI & "dpmm/labels/" & medida & "/0/")
                request.Method = "POST"
                'request.Accept = "application/PNG" ' omit this line to get PNG images back
                request.ContentType = "application/x-www-form-urlencoded"
                request.ContentLength = zpl.Length

                Dim requestStream As Stream = request.GetRequestStream()
                requestStream.Write(zpl, 0, zpl.Length)
                requestStream.Close()

                Try
                    Dim response As HttpWebResponse = request.GetResponse()
                    Dim responseStream As Stream = response.GetResponseStream()
                    Dim fileStream As Stream = File.Create("label.png") ' change file name for PNG images
                    responseStream.CopyTo(fileStream)
                    responseStream.Close()
                    fileStream.Close()

                    Using fs As New System.IO.FileStream("label.png", IO.FileMode.Open)

                        pbZPLImage.SizeMode = PictureBoxSizeMode.Normal
                        pbZPLImage.Image = ResizeImage(New Bitmap(Image.FromStream(fs)), 400, 300)
                        pbZPLImage.Image.RotateFlip(RotateFlipType.Rotate180FlipNone)

                    End Using

                Catch ex As WebException
                    XtraMessageBox.Show("No se pudo establecer conexión a Zebra-api, o uno de los valores es incorrecto.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Try

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Function ResizeImage(ByVal img As Image, ByVal w As Integer, ByVal h As Integer) As Image
        Dim newImage As New Bitmap(w, h)
        Using g As Graphics = Graphics.FromImage(newImage)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(img, New Rectangle(0, 0, w, h))
        End Using
        Return newImage
    End Function



    Private seleccionActual As Control
    Private desplazamiento As Point
    Private permitirArrastre As Boolean = True


    Private Sub btnAgregarTextBox_Click(sender As Object, e As EventArgs) Handles btnAgregarTexto.Click
        Dim nuevoTextBox As New TextEdit With {
            .Size = New Size(150, 30),
            .Text = "Nuevo Texto"
        }

        ' Manejo de eventos para moverlo
        AddHandler nuevoTextBox.MouseDown, AddressOf TextBox_MouseDown
        AddHandler nuevoTextBox.MouseMove, AddressOf TextBox_MouseMove
        AddHandler nuevoTextBox.DoubleClick, AddressOf TextBox_DoubleClick

        pnlEtiqueta.Controls.Add(nuevoTextBox)
    End Sub


    ' Evento MouseDown para iniciar el Drag & Drop
    Private Sub TextBox_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            seleccionActual = CType(sender, Control)
            desplazamiento = New Point(e.X, e.Y)
            permitirArrastre = True ' Activar el arrastre
        End If
    End Sub



    ' Evento MouseMove para activar el Drag & Drop en el TextBox
    Private Sub TextBox_MouseMove(sender As Object, e As MouseEventArgs)
        If permitirArrastre AndAlso e.Button = MouseButtons.Left Then
            DoDragDrop(seleccionActual, DragDropEffects.Move)
            'permitirArrastre = False ' Desactivar arrastre después de iniciar
        End If
    End Sub

    ' Evento Doble Click para permitir edición en el TextBox
    Private Sub TextBox_DoubleClick(sender As Object, e As EventArgs)
        Dim txt As TextEdit = CType(sender, TextEdit)
        txt.Properties.ReadOnly = Not txt.Properties.ReadOnly
    End Sub

    Private Sub pnlEtiqueta_DragDrop(sender As Object, e As DragEventArgs) Handles pnlEtiqueta.DragDrop
        If seleccionActual IsNot Nothing Then
            ' Obtener la posición dentro del panel
            Dim posicion As Point = pnlEtiqueta.PointToClient(New Point(e.X, e.Y))
            posicion.X -= desplazamiento.X
            posicion.Y -= desplazamiento.Y

            ' Evitar que el control salga del panel
            If posicion.X < 0 Then posicion.X = 0
            If posicion.Y < 0 Then posicion.Y = 0
            If posicion.X + seleccionActual.Width > pnlEtiqueta.Width Then posicion.X = pnlEtiqueta.Width - seleccionActual.Width
            If posicion.Y + seleccionActual.Height > pnlEtiqueta.Height Then posicion.Y = pnlEtiqueta.Height - seleccionActual.Height

            ' Alineación a una cuadrícula de 10 píxeles
            posicion.X = (posicion.X \ 10) * 10
            posicion.Y = (posicion.Y \ 10) * 10

            ' 🔹 Aquí movemos el control a la nueva posición
            seleccionActual.Location = posicion
        End If
    End Sub

    Private Sub pnlEtiqueta_DragEnter(sender As Object, e As DragEventArgs) Handles pnlEtiqueta.DragEnter
        If e.Data.GetDataPresent(GetType(TextEdit)) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        GuardarDetalleEtiqueta()
    End Sub

    Private Sub GuardarDetalleEtiqueta()
        Dim Exito As Integer = 0
        Try
            Dim EsNuevo As Boolean = (ObjDetalle Is Nothing)

            If EsNuevo Then
                ObjDetalle = New clsBeTipo_etiqueta_detalle With {
                    .IdTipoEtiquetaDetalle = clsLnTipo_etiqueta_detalle.MaxID() + 1
                }
            End If

            With ObjDetalle
                .Nombre = txtNombreE.EditValue
                .Campo = txtCampo.EditValue
                .Coor_x = txtCoorX.EditValue
                .Coor_y = txtCoorY.EditValue
                .IdTipoEtiqueta = cmbTipoEtiqueta.EditValue
                .Width = txtAnchoW.EditValue
                .Height = txtAltoH.EditValue
            End With

            Exito = If(EsNuevo, clsLnTipo_etiqueta_detalle.Insertar(ObjDetalle), clsLnTipo_etiqueta_detalle.Actualizar(ObjDetalle))

            If Exito > 0 Then Get_Detalle_Tipo_Etiqueta()
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub gcLista_DoubleClick(sender As Object, e As EventArgs) Handles gcLista.DoubleClick
        Try
            If grdDetalleEtiqueta.RowCount > 0 Then
                Dim Dr As DataRowView = grdDetalleEtiqueta.GetFocusedRow
                Dim lSelectionIndex As Integer = grdDetalleEtiqueta.FocusedRowHandle

                ObjDetalle = ListaDetalle.Find(Function(b) b.IdTipoEtiquetaDetalle = Dr.Item("IdTipoEtiquetaDetalle"))

                txtNombreE.Text = ListaDetalle(lSelectionIndex).Nombre
                txtCampo.Text = ListaDetalle(lSelectionIndex).Campo
                txtCoorX.Text = ListaDetalle(lSelectionIndex).Coor_x
                txtCoorY.Text = ListaDetalle(lSelectionIndex).Coor_y
                cmbTipoEtiqueta.EditValue = ListaDetalle(lSelectionIndex).IdTipoEtiqueta
                txtAnchoW.Text = ListaDetalle(lSelectionIndex).Width
                txtAltoH.Text = ListaDetalle(lSelectionIndex).Height
            End If
        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub grdDetalleEtiqueta_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdDetalleEtiqueta.RowStyle
        grdDetalleEtiqueta.OptionsBehavior.Editable = False
        grdDetalleEtiqueta.OptionsSelection.EnableAppearanceFocusedCell = False
        grdDetalleEtiqueta.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        grdDetalleEtiqueta.OptionsSelection.EnableAppearanceFocusedRow = True
        grdDetalleEtiqueta.OptionsSelection.EnableAppearanceHideSelection = True
        grdDetalleEtiqueta.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
        grdDetalleEtiqueta.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
        grdDetalleEtiqueta.Appearance.FocusedRow.ForeColor = Color.White
        grdDetalleEtiqueta.Appearance.SelectedRow.ForeColor = Color.White
        grdDetalleEtiqueta.Appearance.SelectedRow.Options.UseBackColor = True
        grdDetalleEtiqueta.Appearance.SelectedRow.Options.UseForeColor = True
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        ObjDetalle = Nothing

        txtNombreE.Text = ""
        txtCampo.Text = ""
        txtCoorX.Text = ""
        txtCoorY.Text = ""
        cmbTipoEtiqueta.EditValue = Nothing
        txtAnchoW.Text = ""
        txtAltoH.Text = ""
    End Sub
End Class