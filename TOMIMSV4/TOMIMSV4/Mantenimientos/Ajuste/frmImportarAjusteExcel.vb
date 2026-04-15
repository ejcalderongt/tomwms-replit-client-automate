Imports System.IO
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports DevExpress.XtraEditors
Imports OfficeOpenXml.DataValidation

Public Class frmImportarAjusteExcel
    Inherits Form

    '─── Datos devueltos a frmAjusteStock ────────────────────────────────────
    ''' <summary>Lista de ajustes válidos listos para cargar en el grid.</summary>
    Public ReadOnly AjustesParaCargar As New List(Of clsBeTrans_ajuste_det)

    '─── Configuración que llega desde frmAjusteStock ────────────────────────
    Private _idBodega As Integer
    Private _idPropietarioBodega As Integer
    Private _idTipoAjuste As Integer          ' 3=Positivo, 5=Negativo, 1=Lote
    Private _idMotivoDefault As Integer = 0   ' opcional

    '─── Controles de la forma ───────────────────────────────────────────────
    Private WithEvents pnlTop As Panel
    Private WithEvents pnlBottom As Panel
    Private WithEvents pnlErrores As Panel

    Private lblTitulo As Label
    Private lblArchivo As Label
    Private WithEvents txtRutaArchivo As TextBox
    Private WithEvents btnSeleccionar As Button
    Private WithEvents btnDescargarPlantilla As Button
    Private WithEvents btnValidar As Button
    Private WithEvents btnCargar As Button
    Private WithEvents btnCerrar As Button

    Private WithEvents tabControl As TabControl
    Private tabDatos As TabPage
    Private tabErrores As TabPage

    Private WithEvents dgvPreview As DataGridView
    Private WithEvents dgvErrores As DataGridView

    Private lblStatus As Label
    Private lblConteo As Label

    Private progressBar As DevExpress.XtraEditors.ProgressBarControl

    '─── Estado interno ──────────────────────────────────────────────────────
    Private _rutaArchivo As String = ""
    Private _filasValidadas As New List(Of FilaAjusteExcel)

    '=========================================================================
    ' CLASE INTERNA: representa una fila del Excel ya validada
    '=========================================================================
    Public Class FilaAjusteExcel
        Public Property Valida As Boolean = True
        Public Property MensajeError As String = ""
        Public Property Fila As Integer
        Public Property Hoja As String

        ' Datos del Excel
        Public Property CodigoProducto As String = ""
        Public Property Ubicacion As String = ""
        Public Property Lote As String = ""
        Public Property TipoTexto As String = ""       ' "POSITIVO" | "NEGATIVO" | "LOTE"
        Public Property MotivoTexto As String = ""
        Public Property CantidadTexto As String = ""
        Public Property LoteNuevo As String = ""       ' solo para ajuste lote
        Public Property Observacion As String = ""
        Public Property LicPlate As String = ""

        ' Datos resueltos desde BD
        Public Property IdStock As Integer
        Public Property IdProductoBodega As Integer
        Public Property IdUbicacion As Integer
        Public Property IdMotivoAjuste As Integer
        Public Property IdTipoAjuste As Integer
        Public Property IdUnidadMedida As Integer
        Public Property IdPropietarioBodega As Integer
        Public Property IdProductoEstado As Integer
        Public Property IdPresentacion As Integer
        Public Property NombreProducto As String = ""
        Public Property UmBas As String = ""
        Public Property NombreUbicacion As String = ""
        Public Property NombrePresentacion As String = ""
        Public Property Presentacion As clsBeProducto_Presentacion = Nothing
        Public Property Factor As Decimal = 1
        Public Property CantidadOriginal As Decimal
        Public Property CantidadNueva As Decimal
        Public Property CantReservada As Decimal
        Public Property CantidadAjuste As Decimal
    End Class

    '=========================================================================
    ' CONSTRUCTOR
    '=========================================================================
    Public Sub New(idBodega As Integer,
                   idPropietarioBodega As Integer,
                   idTipoAjuste As Integer)

        InitializeComponent_Custom()

        _idBodega = idBodega
        _idPropietarioBodega = idPropietarioBodega
        _idTipoAjuste = idTipoAjuste

        Me.Text = "Importar Ajuste desde Excel"
        Me.Size = New Size(1100, 700)
        Me.MinimumSize = New Size(900, 600)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Icon = SystemIcons.Application

        Actualizar_Titulo()
    End Sub

    '=========================================================================
    ' CONSTRUCCIÓN DINÁMICA DE LA INTERFAZ
    '=========================================================================
    Private Sub InitializeComponent_Custom()
        ' Configurar licencia de EPPlus
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial

        ' ── Panel superior (selección de archivo) ──────────────────────────
        pnlTop = New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 110,
            .Padding = New Padding(10),
            .BackColor = Color.FromArgb(240, 244, 250)
        }

        lblTitulo = New Label() With {
            .Text = "Importación de Ajuste de Stock desde Excel",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .ForeColor = Color.FromArgb(31, 73, 125),
            .Location = New Point(10, 8),
            .AutoSize = True
        }

        Dim lblArchivoLbl As New Label() With {
            .Text = "Archivo Excel:",
            .Location = New Point(10, 42),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 9)
        }

        txtRutaArchivo = New TextBox() With {
            .Location = New Point(10, 62),
            .Width = 520,
            .ReadOnly = True,
            .Font = New Font("Segoe UI", 9),
            .BackColor = Color.White
        }

        btnSeleccionar = New Button() With {
            .Text = "📂  Seleccionar...",
            .Location = New Point(538, 60),
            .Size = New Size(140, 28),
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .BackColor = Color.FromArgb(0, 114, 198),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand,
            .UseVisualStyleBackColor = False
        }
        btnSeleccionar.FlatAppearance.BorderSize = 0

        btnDescargarPlantilla = New Button() With {
            .Text = "⬇  Descargar Plantilla",
            .Location = New Point(686, 60),
            .Size = New Size(170, 28),
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .BackColor = Color.FromArgb(0, 150, 80),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand,
            .UseVisualStyleBackColor = False
        }
        btnDescargarPlantilla.FlatAppearance.BorderSize = 0

        btnValidar = New Button() With {
            .Text = "✔  Validar",
            .Location = New Point(864, 60),
            .Size = New Size(110, 28),
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .BackColor = Color.FromArgb(180, 120, 0),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Enabled = False,
            .Cursor = Cursors.Hand,
            .UseVisualStyleBackColor = False
        }
        btnValidar.FlatAppearance.BorderSize = 0

        progressBar = New DevExpress.XtraEditors.ProgressBarControl()
        progressBar.Location = New Point(10, 94)
        progressBar.Width = 964
        progressBar.Height = 8
        progressBar.Visible = False

        pnlTop.Controls.AddRange({lblTitulo, lblArchivoLbl, txtRutaArchivo,
                                   btnSeleccionar, btnDescargarPlantilla, btnValidar, progressBar})

        ' ── Panel inferior (botones de acción) ────────────────────────────
        pnlBottom = New Panel() With {
            .Dock = DockStyle.Bottom,
            .Height = 52,
            .Padding = New Padding(10, 8, 10, 8),
            .BackColor = Color.FromArgb(240, 244, 250)
        }

        lblStatus = New Label() With {
            .Location = New Point(10, 16),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 9, FontStyle.Italic),
            .ForeColor = Color.FromArgb(80, 80, 80)
        }

        lblConteo = New Label() With {
            .Location = New Point(10, 16),
            .AutoSize = True,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .ForeColor = Color.FromArgb(0, 110, 50),
            .Visible = False
        }

        btnCargar = New Button() With {
            .Text = "✔  Cargar al Ajuste",
            .Size = New Size(160, 34),
            .Dock = DockStyle.Right,
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .BackColor = Color.FromArgb(0, 130, 60),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Enabled = False,
            .Cursor = Cursors.Hand,
            .UseVisualStyleBackColor = False
        }
        btnCargar.FlatAppearance.BorderSize = 0

        btnCerrar = New Button() With {
            .Text = "Cancelar",
            .Size = New Size(100, 34),
            .Dock = DockStyle.Right,
            .Font = New Font("Segoe UI", 10),
            .BackColor = Color.FromArgb(180, 50, 40),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Cursor = Cursors.Hand,
            .UseVisualStyleBackColor = False
        }
        btnCerrar.FlatAppearance.BorderSize = 0

        pnlBottom.Controls.AddRange({lblStatus, btnCerrar, btnCargar})

        ' ── TabControl central ────────────────────────────────────────────
        tabControl = New TabControl() With {
            .Dock = DockStyle.Fill,
            .Font = New Font("Segoe UI", 9)
        }

        tabDatos = New TabPage("Vista Previa de Datos")
        tabErrores = New TabPage("Errores de Validación (0)")

        ' Grid de previsualización
        dgvPreview = Crear_Grid_Preview()
        dgvPreview.Dock = DockStyle.Fill
        tabDatos.Controls.Add(dgvPreview)

        ' Grid de errores
        dgvErrores = Crear_Grid_Errores()
        dgvErrores.Dock = DockStyle.Fill
        tabErrores.Controls.Add(dgvErrores)

        tabControl.TabPages.Add(tabDatos)
        tabControl.TabPages.Add(tabErrores)

        ' ── Ensamblar controles en la forma ───────────────────────────────
        Me.Controls.Add(tabControl)
        Me.Controls.Add(pnlBottom)
        Me.Controls.Add(pnlTop)
    End Sub

    Private Function Crear_Grid_Preview() As DataGridView
        Dim dg As New DataGridView()
        dg.AllowUserToAddRows = False
        dg.AllowUserToDeleteRows = False
        dg.ReadOnly = True
        dg.RowHeadersVisible = False
        dg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        dg.RowTemplate.Height = 22
        dg.Font = New Font("Segoe UI", 8.5F)
        dg.BorderStyle = BorderStyle.None
        dg.GridColor = Color.FromArgb(210, 220, 235)
        dg.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 255)
        dg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 73, 125)
        dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dg.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
        dg.EnableHeadersVisualStyles = False
        dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        Dim cols As (Name As String, Header As String, Width As Integer)() = {
            ("Estado", "Estado", 80),
            ("Hoja", "Hoja", 90),
            ("Fila", "Fila", 45),
            ("Codigo", "Código Producto", 120),
            ("Nombre", "Nombre Producto", 180),
            ("Ubicacion", "Ubicación", 140),
            ("Lote", "Lote / Lote Ant.", 110),
            ("LoteNuevo", "Lote Nuevo", 110),
            ("Tipo", "Tipo Ajuste", 90),
            ("Cantidad", "Cantidad", 100),
            ("Motivo", "Motivo", 65),
            ("Observacion", "Observación", 140)
        }

        For Each col In cols
            Dim c As New DataGridViewTextBoxColumn() With {
                .Name = col.Name,
                .HeaderText = col.Header,
                .Width = col.Width,
                .SortMode = DataGridViewColumnSortMode.NotSortable
            }
            dg.Columns.Add(c)
        Next

        Return dg
    End Function

    Private Function Crear_Grid_Errores() As DataGridView
        Dim dg As New DataGridView()
        dg.AllowUserToAddRows = False
        dg.AllowUserToDeleteRows = False
        dg.ReadOnly = True
        dg.RowHeadersVisible = False
        dg.RowTemplate.Height = 22
        dg.Font = New Font("Segoe UI", 8.5F)
        dg.BorderStyle = BorderStyle.None
        dg.EnableHeadersVisualStyles = False
        dg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(150, 40, 30)
        dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dg.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
        dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        dg.Columns.Add(New DataGridViewTextBoxColumn() With {
            .Name = "Hoja",
            .HeaderText = "Hoja",
            .Width = 110,
            .SortMode = DataGridViewColumnSortMode.NotSortable
        })
        dg.Columns.Add(New DataGridViewTextBoxColumn() With {
            .Name = "Fila",
            .HeaderText = "Fila",
            .Width = 50,
            .SortMode = DataGridViewColumnSortMode.NotSortable
        })
        dg.Columns.Add(New DataGridViewTextBoxColumn() With {
            .Name = "Codigo",
            .HeaderText = "Código",
            .Width = 120,
            .SortMode = DataGridViewColumnSortMode.NotSortable
        })
        dg.Columns.Add(New DataGridViewTextBoxColumn() With {
            .Name = "Error",
            .HeaderText = "Descripción del Error",
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            .SortMode = DataGridViewColumnSortMode.NotSortable
        })

        Return dg
    End Function

    '=========================================================================
    ' HANDLERS DE BOTONES
    '=========================================================================
    Private Sub btnSeleccionar_Click(sender As Object, e As EventArgs) Handles btnSeleccionar.Click
        Using dlg As New OpenFileDialog()
            dlg.Title = "Seleccionar archivo Excel de ajuste"
            dlg.Filter = "Excel Files (*.xlsx)|*.xlsx"
            dlg.Multiselect = False
            dlg.CheckFileExists = True
            dlg.RestoreDirectory = True

            If dlg.ShowDialog() <> DialogResult.OK Then Return

            _rutaArchivo = dlg.FileName
            txtRutaArchivo.Text = _rutaArchivo
            btnValidar.Enabled = True
            btnCargar.Enabled = False
            dgvPreview.Rows.Clear()
            dgvErrores.Rows.Clear()
            _filasValidadas.Clear()
            tabErrores.Text = "Errores de Validación (0)"
            Actualizar_Status("Archivo seleccionado. Haga clic en Validar.", Color.FromArgb(0, 90, 160))
        End Using
    End Sub

    Private Sub btnDescargarPlantilla_Click(sender As Object, e As EventArgs) Handles btnDescargarPlantilla.Click
        Generar_Plantilla()
    End Sub

    Private Sub btnValidar_Click(sender As Object, e As EventArgs) Handles btnValidar.Click
        If String.IsNullOrEmpty(_rutaArchivo) Then
            MessageBox.Show("Primero seleccione un archivo Excel.", "Sin archivo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Validar_Archivo()
    End Sub

    Private Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        Dim validas = _filasValidadas.Where(Function(f) f.Valida).ToList()
        If validas.Count = 0 Then
            MessageBox.Show("No hay filas válidas para cargar.", "Sin datos válidos",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Mapear a clsBeTrans_ajuste_det y llenar la lista pública
        AjustesParaCargar.Clear()

        For Each f As FilaAjusteExcel In validas
            Dim det As New clsBeTrans_ajuste_det()

            ' Propiedades básicas
            det.IdAjusteDet = 0
            det.IdAjusteEnc = 0                    ' frmAjusteStock lo asignará
            det.IdStock = f.IdStock
            det.IdPropietarioBodega = f.IdPropietarioBodega
            det.IdProductoBodega = f.IdProductoBodega
            det.IdProductoEstado = f.IdProductoEstado
            det.IdPresentacion = f.IdPresentacion
            det.IdUnidadMedida = f.IdUnidadMedida
            det.IdUbicacion = f.IdUbicacion

            ' Lotes
            det.Lote_original = f.Lote
            det.Lote_nuevo = If(f.IdTipoAjuste = 1, f.LoteNuevo, f.Lote)

            ' Fechas (valores por defecto)
            det.Fecha_vence_original = New Date(1900, 1, 1)
            det.Fecha_vence_nueva = New Date(1900, 1, 1)

            ' Pesos
            det.Peso_original = 0
            det.Peso_nuevo = 0

            ' Cantidades
            det.Cantidad_original = f.CantidadOriginal
            det.Cantidad_nueva = f.CantidadNueva
            det.CantReservada = f.CantReservada

            ' Unidad de medida y producto
            det.UmBas = f.UmBas
            det.Codigo_producto = f.CodigoProducto
            det.Nombre_producto = f.NombreProducto

            ' Tipo de ajuste y motivo
            det.Idtipoajuste = f.IdTipoAjuste
            det.IdMotivoAjuste = f.IdMotivoAjuste
            det.Observacion = f.Observacion

            ' Otros campos
            det.Codigo_ajuste = 0
            det.Enviado = False
            det.lic_plate = f.LicPlate
            det.idstockres = 0
            det.idstocklink = 0
            det.esnuevolink = 0

            ' Presentación
            If f.Presentacion IsNot Nothing Then
                det.Presentacion = f.Presentacion
            End If

            AjustesParaCargar.Add(det)
        Next

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    '=========================================================================
    ' VALIDACIÓN PRINCIPAL
    '=========================================================================
    Private Sub Validar_Archivo()
        _filasValidadas.Clear()
        dgvPreview.Rows.Clear()
        dgvErrores.Rows.Clear()
        btnCargar.Enabled = False
        progressBar.Visible = True
        Application.DoEvents()

        Try
            Using pkg As New ExcelPackage(New FileInfo(_rutaArchivo))
                Dim wsCant As ExcelWorksheet = pkg.Workbook.Worksheets("Ajuste_Cantidad")
                Dim wsLote As ExcelWorksheet = pkg.Workbook.Worksheets("Ajuste_Lote")

                If wsCant Is Nothing AndAlso wsLote Is Nothing Then
                    MessageBox.Show("El archivo no contiene las hojas 'Ajuste_Cantidad' ni 'Ajuste_Lote'." &
                                    Environment.NewLine & "Use la plantilla oficial.",
                                    "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Leer hojas
                If wsCant IsNot Nothing Then
                    Leer_Hoja_Cantidad(wsCant)
                End If

                If wsLote IsNot Nothing Then
                    Leer_Hoja_Lote(wsLote)
                End If
            End Using

            ' Resolver datos en BD
            If _filasValidadas.Count > 0 Then
                Resolver_BD()
                Poblar_Grillas()
                Actualizar_UI_PostValidacion()
            Else
                Actualizar_Status("No se encontraron datos para validar.", Color.FromArgb(180, 30, 30))
                MessageBox.Show("No se encontraron filas con datos en el archivo Excel.",
                                "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error al leer el archivo: " & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            clsLnLog_error_wms.Agregar_Error("frmImportarAjusteExcel.Validar_Archivo: " & ex.Message)
            Actualizar_Status("Error: " & ex.Message, Color.FromArgb(180, 30, 30))
        Finally
            progressBar.Visible = False
        End Try
    End Sub

    '─── Lectura de hoja Ajuste_Cantidad ─────────────────────────────────────
    ' Fila 4 = cabecera, filas 5-7 = ejemplos ignorados, datos desde fila 8
    Private Sub Leer_Hoja_Cantidad(ws As ExcelWorksheet)
        If ws.Dimension Is Nothing Then Return

        Dim filaFin As Integer = ws.Dimension.End.Row
        If filaFin < 8 Then Return

        For r As Integer = 8 To filaFin
            Dim codigo As String = CeldaStr(ws, r, 1)
            If String.IsNullOrWhiteSpace(codigo) Then Continue For

            Dim f As New FilaAjusteExcel() With {
                .Hoja = "Ajuste_Cantidad",
                .Fila = r,
                .CodigoProducto = codigo.Trim(),
                .Ubicacion = CeldaStr(ws, r, 2).Trim(),
                .Lote = CeldaStr(ws, r, 3).Trim(),
                .TipoTexto = CeldaStr(ws, r, 4).Trim().ToUpperInvariant(),
                .MotivoTexto = CeldaStr(ws, r, 5).Trim().ToUpperInvariant(),
                .CantidadTexto = CeldaStr(ws, r, 6).Trim(),
                .Observacion = CeldaStr(ws, r, 7).Trim(),
                .LicPlate = CeldaStr(ws, r, 8).Trim()
            }
            _filasValidadas.Add(f)
        Next
    End Sub

    '─── Lectura de hoja Ajuste_Lote ─────────────────────────────────────────
    ' Fila 4 = cabecera, fila 5 = ejemplo ignorado, datos desde fila 6
    Private Sub Leer_Hoja_Lote(ws As ExcelWorksheet)
        If ws.Dimension Is Nothing Then Return

        Dim filaFin As Integer = ws.Dimension.End.Row
        If filaFin < 6 Then Return

        For r As Integer = 6 To filaFin
            Dim codigo As String = CeldaStr(ws, r, 1)
            If String.IsNullOrWhiteSpace(codigo) Then Continue For

            Dim f As New FilaAjusteExcel() With {
                .Hoja = "Ajuste_Lote",
                .Fila = r,
                .CodigoProducto = codigo.Trim(),
                .Ubicacion = CeldaStr(ws, r, 2).Trim(),
                .Lote = CeldaStr(ws, r, 3).Trim(),
                .LoteNuevo = CeldaStr(ws, r, 4).Trim(),
                .MotivoTexto = CeldaStr(ws, r, 5).Trim().ToUpperInvariant(),
                .Observacion = CeldaStr(ws, r, 6).Trim(),
                .TipoTexto = "LOTE"
            }
            _filasValidadas.Add(f)
        Next
    End Sub

    '─── Resolver datos en BD para cada fila ─────────────────────────────────
    Private Sub Resolver_BD()
        For Each f As FilaAjusteExcel In _filasValidadas
            Try
                Validar_Campos_Obligatorios(f)
                Validar_Tipo_Ajuste(f)
                Dim cantAjuste As Decimal = Validar_Y_Obtener_Cantidad(f)

                ' Resolver producto
                Dim idProducto As Integer = clsLnProducto.Get_IdProducto_By_Codigo(f.CodigoProducto)
                If idProducto = 0 Then Throw New Exception($"Producto no encontrado: {f.CodigoProducto}")

                Dim idProdBod As Integer = clsLnProducto_bodega.Get_IdProductoBodega_By_IdProducto_And_IdBodega(idProducto, _idBodega)
                If idProdBod = 0 Then Throw New Exception("El producto no está configurado en esta bodega.")

                ' Resolver ubicación
                Dim BeUbicacion As clsBeBodega_ubicacion = clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(f.Ubicacion, _idBodega)
                If BeUbicacion Is Nothing Then Throw New Exception($"Ubicación no encontrada: {f.Ubicacion}")

                ' Resolver stock
                Dim beStock As clsBeStock = clsLnStock.Get_Single_By_ProductoBodega_Ubicacion_Lote(idProdBod, BeUbicacion.IdUbicacion, f.Lote, _idBodega)
                If beStock Is Nothing Then
                    Throw New Exception($"Sin stock para: Producto={f.CodigoProducto} | Ubic={f.Ubicacion} | Lote={f.Lote}")
                End If

                ' Resolver motivo
                Dim idMotivo As Integer = clsLnAjuste_motivo.Get_IdMotivo_By_Codigo(f.MotivoTexto)
                If idMotivo = 0 Then Throw New Exception($"Motivo no encontrado: {f.MotivoTexto}")

                ' Obtener producto y presentación
                Dim beProducto As clsBeProducto = clsLnProducto.Get_Single_By_IdProducto(idProducto)
                Dim bePresObj As clsBeProducto_Presentacion = Nothing
                Dim factor As Decimal = 1
                Dim nombrePres As String = ""

                If beStock.IdPresentacion <> 0 Then
                    bePresObj = clsLnProducto_presentacion.GetSingle(beStock.IdPresentacion)
                    If bePresObj IsNot Nothing Then
                        factor = CDec(bePresObj.Factor)
                        nombrePres = bePresObj.Nombre
                    End If
                End If

                ' Calcular cantidades
                Dim cantDisp As Decimal = CDec(beStock.Cantidad) - CDec(beStock.Cantidad_Reservada)
                Dim cantDispPres As Decimal = If(factor > 1, Math.Round(cantDisp / factor, 6), cantDisp)
                Dim cantResPres As Decimal = If(factor > 1, Math.Round(CDec(beStock.Cantidad_Reservada) / factor, 6), CDec(beStock.Cantidad_Reservada))

                ' Determinar tipo de ajuste
                Dim idTipoAj As Integer = Obtener_Id_Tipo_Ajuste(f)

                ' Calcular cantidad nueva
                Dim cantNueva As Decimal = Calcular_Cantidad_Nueva(f, cantDispPres, cantAjuste, idTipoAj)

                ' Asignar valores a la fila
                Asignar_Valores_Validados(f, beStock, idProdBod, BeUbicacion.IdUbicacion, idMotivo, idTipoAj,
                                          beProducto, bePresObj, factor, nombrePres,
                                          cantDispPres, cantNueva, cantResPres)

            Catch ex As Exception
                f.Valida = False
                f.MensajeError = ex.Message
            End Try
        Next
    End Sub

    Private Sub Validar_Campos_Obligatorios(f As FilaAjusteExcel)
        If String.IsNullOrWhiteSpace(f.CodigoProducto) Then
            Throw New Exception("Código de producto vacío.")
        End If

        If String.IsNullOrWhiteSpace(f.Ubicacion) Then
            Throw New Exception("Ubicación vacía.")
        End If

        If String.IsNullOrWhiteSpace(f.Lote) Then
            Throw New Exception("Lote vacío.")
        End If

        If String.IsNullOrWhiteSpace(f.MotivoTexto) Then
            Throw New Exception("Código de motivo vacío.")
        End If

        If f.Hoja = "Ajuste_Lote" AndAlso String.IsNullOrWhiteSpace(f.LoteNuevo) Then
            Throw New Exception("Lote nuevo vacío.")
        End If

        If f.Hoja = "Ajuste_Lote" AndAlso f.Lote.Equals(f.LoteNuevo, StringComparison.OrdinalIgnoreCase) Then
            Throw New Exception("Lote anterior y nuevo son iguales.")
        End If
    End Sub

    Private Sub Validar_Tipo_Ajuste(f As FilaAjusteExcel)
        If f.Hoja = "Ajuste_Cantidad" Then
            If f.TipoTexto <> "POSITIVO" AndAlso f.TipoTexto <> "NEGATIVO" Then
                Throw New Exception($"Tipo Ajuste inválido '{f.TipoTexto}'. Use POSITIVO o NEGATIVO.")
            End If
        End If
    End Sub

    Private Function Validar_Y_Obtener_Cantidad(f As FilaAjusteExcel) As Decimal
        If f.Hoja = "Ajuste_Cantidad" Then
            Dim cant As Decimal
            Dim cantidadTexto = f.CantidadTexto.Replace(",", ".")

            If Not Decimal.TryParse(cantidadTexto, Globalization.NumberStyles.Any,
                                    Globalization.CultureInfo.InvariantCulture, cant) Then
                Throw New Exception($"Cantidad inválida '{f.CantidadTexto}'. Debe ser un número mayor a 0.")
            End If

            If cant <= 0 Then
                Throw New Exception($"Cantidad inválida '{f.CantidadTexto}'. Debe ser mayor a 0.")
            End If

            f.CantidadAjuste = cant
            Return cant
        End If

        Return 0
    End Function

    Private Function Obtener_Id_Tipo_Ajuste(f As FilaAjusteExcel) As Integer
        If f.Hoja = "Ajuste_Lote" Then
            Return 1
        ElseIf f.TipoTexto = "POSITIVO" Then
            Return 3
        Else
            Return 5
        End If
    End Function

    Private Function Calcular_Cantidad_Nueva(f As FilaAjusteExcel, cantDispPres As Decimal,
                                              cantAjuste As Decimal, idTipoAj As Integer) As Decimal
        If f.Hoja = "Ajuste_Cantidad" Then
            If idTipoAj = 5 Then ' Negativo
                If cantAjuste > cantDispPres Then
                    Throw New Exception($"Cantidad a ajustar ({cantAjuste}) supera la disponible ({cantDispPres}).")
                End If
                Return cantDispPres - cantAjuste
            Else ' Positivo
                Return cantDispPres + cantAjuste
            End If
        End If

        ' Para ajuste de lote, cantidad_nueva = cantidad_original (no cambia)
        Return cantDispPres
    End Function

    Private Sub Asignar_Valores_Validados(f As FilaAjusteExcel, beStock As clsBeStock,
                                          idProdBod As Integer, idUbic As Integer,
                                          idMotivo As Integer, idTipoAj As Integer,
                                          beProducto As clsBeProducto,
                                          bePresObj As clsBeProducto_Presentacion,
                                          factor As Decimal, nombrePres As String,
                                          cantDispPres As Decimal, cantNueva As Decimal,
                                          cantResPres As Decimal)

        f.IdStock = beStock.IdStock
        f.IdProductoBodega = idProdBod
        f.IdUbicacion = idUbic
        f.IdMotivoAjuste = idMotivo
        f.IdTipoAjuste = idTipoAj
        f.IdUnidadMedida = beStock.IdUnidadMedida
        f.IdPropietarioBodega = beStock.IdPropietarioBodega
        f.IdProductoEstado = beStock.IdProductoEstado
        f.IdPresentacion = beStock.IdPresentacion
        f.NombreProducto = If(beProducto IsNot Nothing, beProducto.Nombre, f.CodigoProducto)
        f.UmBas = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(beStock.IdUnidadMedida)
        f.NombreUbicacion = f.Ubicacion
        f.NombrePresentacion = nombrePres
        f.Factor = factor
        f.Presentacion = bePresObj
        f.CantidadOriginal = cantDispPres
        f.CantidadNueva = cantNueva
        f.CantReservada = cantResPres
        f.Valida = True
    End Sub

    '─── Poblar grillas de previsualización y errores ────────────────────────
    Private Sub Poblar_Grillas()
        dgvPreview.SuspendLayout()
        dgvErrores.SuspendLayout()

        Try
            dgvPreview.Rows.Clear()
            dgvErrores.Rows.Clear()

            For Each f As FilaAjusteExcel In _filasValidadas
                Dim cantTexto As String = Obtener_Texto_Cantidad(f)
                Dim loteNuevoMostrar As String = If(f.IdTipoAjuste = 1, f.LoteNuevo, "")
                Dim nombreProducto As String = If(f.Valida, f.NombreProducto, "")

                Dim rowIndex As Integer = dgvPreview.Rows.Add(
                    If(f.Valida, "✔ OK", "✘ Error"),
                    f.Hoja,
                    f.Fila,
                    f.CodigoProducto,
                    nombreProducto,
                    f.Ubicacion,
                    f.Lote,
                    loteNuevoMostrar,
                    f.TipoTexto,
                    cantTexto,
                    f.MotivoTexto,
                    f.Observacion
                )

                ' Aplicar estilo según estado
                If f.Valida Then
                    dgvPreview.Rows(rowIndex).DefaultCellStyle.BackColor = Color.FromArgb(235, 250, 240)
                    dgvPreview.Rows(rowIndex).DefaultCellStyle.ForeColor = Color.FromArgb(0, 100, 40)
                Else
                    dgvPreview.Rows(rowIndex).DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235)
                    dgvPreview.Rows(rowIndex).DefaultCellStyle.ForeColor = Color.FromArgb(160, 30, 30)

                    dgvErrores.Rows.Add(f.Hoja, f.Fila, f.CodigoProducto, f.MensajeError)
                    Dim errorIndex As Integer = dgvErrores.Rows.Count - 1
                    dgvErrores.Rows(errorIndex).DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235)
                End If
            Next

        Finally
            dgvPreview.ResumeLayout()
            dgvErrores.ResumeLayout()
        End Try
    End Sub

    Private Function Obtener_Texto_Cantidad(f As FilaAjusteExcel) As String
        If Not f.Valida Then
            Return f.CantidadTexto
        End If

        Select Case f.IdTipoAjuste
            Case 1 ' Lote
                Return "→ " & f.LoteNuevo
            Case 3 ' Positivo
                Dim delta As Decimal = f.CantidadNueva - f.CantidadOriginal
                Return $"+{delta:N4}"
            Case 5 ' Negativo
                Dim delta As Decimal = f.CantidadOriginal - f.CantidadNueva
                Return $"-{delta:N4}"
            Case Else
                Return f.CantidadTexto
        End Select
    End Function

    Private Sub Actualizar_UI_PostValidacion()
        Dim nValidas = _filasValidadas.Where(Function(f) f.Valida).Count()
        Dim nErrores = _filasValidadas.Where(Function(f) Not f.Valida).Count()

        tabErrores.Text = $"Errores de Validación ({nErrores})"

        If nValidas > 0 Then
            btnCargar.Enabled = True

            Dim mensaje As String = $"{nValidas} fila(s) válidas para cargar."
            If nErrores > 0 Then
                mensaje &= $" {nErrores} con error."
                Actualizar_Status(mensaje, Color.FromArgb(160, 90, 0))
                tabControl.SelectedIndex = 1
            Else
                Actualizar_Status(mensaje, Color.FromArgb(0, 110, 40))
                tabControl.SelectedIndex = 0
            End If
        Else
            Actualizar_Status("No hay filas válidas. Corrija los errores indicados.", Color.FromArgb(180, 30, 30))
            tabControl.SelectedIndex = 1
        End If
    End Sub

    '=========================================================================
    ' GENERACIÓN DE PLANTILLA EXCEL
    '=========================================================================
    Private Sub Generar_Plantilla()
        Using dlg As New SaveFileDialog()
            dlg.Title = "Guardar Plantilla de Importación"
            dlg.Filter = "Excel Files (*.xlsx)|*.xlsx"
            dlg.FileName = "Plantilla_Ajuste_Stock.xlsx"
            dlg.RestoreDirectory = True

            If dlg.ShowDialog() <> DialogResult.OK Then Return

            Try
                Using pkg As New ExcelPackage()
                    ' Hoja Ajuste_Cantidad
                    Dim ws1 As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Ajuste_Cantidad")
                    Crear_Hoja_Cantidad(ws1)

                    ' Hoja Ajuste_Lote
                    Dim ws2 As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Ajuste_Lote")
                    Crear_Hoja_Lote(ws2)

                    ' Hoja Instrucciones
                    Dim ws3 As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Instrucciones")
                    Crear_Hoja_Instrucciones(ws3)

                    pkg.SaveAs(New FileInfo(dlg.FileName))
                End Using

                MessageBox.Show($"Plantilla guardada en:{Environment.NewLine}{dlg.FileName}",
                                "Plantilla generada", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show($"Error al generar la plantilla: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                clsLnLog_error_wms.Agregar_Error($"frmImportarAjusteExcel.Generar_Plantilla: {ex.Message}")
            End Try
        End Using
    End Sub

    Private Sub Crear_Hoja_Cantidad(ws As ExcelWorksheet)
        ' Título
        ws.Cells("A1:H1").Merge = True
        ws.Cells("A1").Value = "AJUSTE POR CANTIDAD — POSITIVO / NEGATIVO"
        ws.Cells("A1").Style.Font.Bold = True
        ws.Cells("A1").Style.Font.Size = 13
        ws.Cells("A1").Style.Font.Color.SetColor(Color.White)
        ws.Cells("A1").Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells("A1").Style.Fill.BackgroundColor.SetColor(Color.FromArgb(31, 73, 125))
        ws.Cells("A1").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        ws.Row(1).Height = 24

        ' Nota
        ws.Cells("A2:H2").Merge = True
        ws.Cells("A2").Value = "Columnas con (*) son obligatorias. Filas 5-7 son ejemplos (elimínelas antes de importar). Datos desde fila 8."
        ws.Cells("A2").Style.Font.Italic = True
        ws.Cells("A2").Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells("A2").Style.Fill.BackgroundColor.SetColor(Color.FromArgb(220, 230, 241))

        ' Cabeceras fila 4
        Dim hdr() As String = {"Código Producto (*)", "Ubicación (*)", "Lote (*)", "Tipo Ajuste (*)", "Motivo (*)", "Cantidad (*)", "Observación", "Lic. Plate"}
        Dim anchos() As Integer = {20, 32, 18, 16, 12, 12, 32, 16}

        For i As Integer = 0 To 7
            ws.Cells(4, i + 1).Value = hdr(i)
            ws.Cells(4, i + 1).Style.Font.Bold = True
            ws.Cells(4, i + 1).Style.Font.Color.SetColor(Color.White)
            ws.Cells(4, i + 1).Style.Fill.PatternType = ExcelFillStyle.Solid
            ws.Cells(4, i + 1).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 94))
            ws.Cells(4, i + 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Column(i + 1).Width = anchos(i)
        Next
        ws.Row(4).Height = 20

        ' Ejemplos (filas 5-7) en gris
        Dim ej(,) As String = {
            {"PROD001", "ZONA-A / RACK-01 / NIVEL-02", "LOTE-2024-001", "POSITIVO", "ENT", "50", "Ingreso bodega", ""},
            {"PROD002", "ZONA-B / RACK-05 / NIVEL-01", "LOTE-2024-002", "NEGATIVO", "MER", "10", "Merma", "LP-0045"},
            {"PROD003", "ZONA-A / RACK-02 / NIVEL-03", "LOTE-2024-003", "POSITIVO", "INV", "5", "", ""}
        }

        For r As Integer = 0 To 2
            For c As Integer = 0 To 7
                ws.Cells(r + 5, c + 1).Value = ej(r, c)
                ws.Cells(r + 5, c + 1).Style.Font.Italic = True
                ws.Cells(r + 5, c + 1).Style.Font.Color.SetColor(Color.Gray)
                ws.Cells(r + 5, c + 1).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(r + 5, c + 1).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240))
            Next
        Next

        ' Área de datos (fila 8 en adelante)
        For r As Integer = 8 To 107
            For c As Integer = 1 To 8
                ws.Cells(r, c).Style.Border.BorderAround(ExcelBorderStyle.Hair, Color.FromArgb(190, 190, 190))
            Next

            If r Mod 2 = 0 Then
                ws.Cells(r, 1, r, 8).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(r, 1, r, 8).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(248, 251, 255))
            End If
        Next

        ' Validación lista en Tipo Ajuste
        Dim vTipo As ExcelDataValidationList = ws.DataValidations.AddListValidation("D8:D107")
        vTipo.ShowErrorMessage = True
        vTipo.ErrorTitle = "Valor inválido"
        vTipo.Error = "Use POSITIVO o NEGATIVO"
        vTipo.Formula.Values.Add("POSITIVO")
        vTipo.Formula.Values.Add("NEGATIVO")

        ' Validación numérica en Cantidad
        Dim vCant As ExcelDataValidationDecimal = ws.DataValidations.AddDecimalValidation("F8:F107")
        vCant.ShowErrorMessage = True
        vCant.ErrorTitle = "Cantidad inválida"
        vCant.Error = "Ingrese un número mayor a 0"
        vCant.Operator = ExcelDataValidationOperator.greaterThan
        vCant.Formula.Value = 0

        ws.View.FreezePanes(5, 1)
    End Sub

    Private Sub Crear_Hoja_Lote(ws As ExcelWorksheet)
        ws.Cells("A1:F1").Merge = True
        ws.Cells("A1").Value = "AJUSTE DE LOTE — CAMBIO DE NÚMERO DE LOTE"
        ws.Cells("A1").Style.Font.Bold = True
        ws.Cells("A1").Style.Font.Size = 13
        ws.Cells("A1").Style.Font.Color.SetColor(Color.White)
        ws.Cells("A1").Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells("A1").Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 112, 60))
        ws.Cells("A1").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        ws.Row(1).Height = 24

        ws.Cells("A2:F2").Merge = True
        ws.Cells("A2").Value = "Indique el lote actual y el nuevo número de lote a asignar. Fila 5 es ejemplo, datos desde fila 6."
        ws.Cells("A2").Style.Font.Italic = True
        ws.Cells("A2").Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells("A2").Style.Fill.BackgroundColor.SetColor(Color.FromArgb(215, 240, 225))

        Dim hdr() As String = {"Código Producto (*)", "Ubicación (*)", "Lote Anterior (*)", "Lote Nuevo (*)", "Motivo (*)", "Observación"}
        Dim anchos() As Integer = {20, 32, 20, 20, 12, 32}

        For i As Integer = 0 To 5
            ws.Cells(4, i + 1).Value = hdr(i)
            ws.Cells(4, i + 1).Style.Font.Bold = True
            ws.Cells(4, i + 1).Style.Font.Color.SetColor(Color.White)
            ws.Cells(4, i + 1).Style.Fill.PatternType = ExcelFillStyle.Solid
            ws.Cells(4, i + 1).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 80, 40))
            ws.Cells(4, i + 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Column(i + 1).Width = anchos(i)
        Next
        ws.Row(4).Height = 20

        ' Ejemplo fila 5
        ws.Cells(5, 1).Value = "PROD001"
        ws.Cells(5, 2).Value = "ZONA-A / RACK-01 / NIVEL-02"
        ws.Cells(5, 3).Value = "LOTE-ANT"
        ws.Cells(5, 4).Value = "LOTE-NUEVO"
        ws.Cells(5, 5).Value = "CAM"
        ws.Cells(5, 6).Value = "Corrección de lote"

        For c As Integer = 1 To 6
            ws.Cells(5, c).Style.Font.Italic = True
            ws.Cells(5, c).Style.Font.Color.SetColor(Color.Gray)
            ws.Cells(5, c).Style.Fill.PatternType = ExcelFillStyle.Solid
            ws.Cells(5, c).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240))
        Next

        ' Área de datos
        For r As Integer = 6 To 105
            For c As Integer = 1 To 6
                ws.Cells(r, c).Style.Border.BorderAround(ExcelBorderStyle.Hair, Color.FromArgb(190, 190, 190))
            Next

            If r Mod 2 = 0 Then
                ws.Cells(r, 1, r, 6).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(r, 1, r, 6).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(245, 252, 248))
            End If
        Next

        ws.View.FreezePanes(5, 1)
    End Sub

    Private Sub Crear_Hoja_Instrucciones(ws As ExcelWorksheet)
        ws.Column(1).Width = 22
        ws.Column(2).Width = 55

        ws.Cells("A1:B1").Merge = True
        ws.Cells("A1").Value = "INSTRUCCIONES Y CATÁLOGO DE MOTIVOS"
        ws.Cells("A1").Style.Font.Bold = True
        ws.Cells("A1").Style.Font.Size = 13
        ws.Cells("A1").Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells("A1").Style.Fill.BackgroundColor.SetColor(Color.FromArgb(68, 84, 106))
        ws.Cells("A1").Style.Font.Color.SetColor(Color.White)
        ws.Cells("A1").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

        Dim lineas() As String = {
            "REGLAS GENERALES",
            "1. No modifique los encabezados de columna.",
            "2. Campos con (*) son OBLIGATORIOS.",
            "3. No deje filas vacías entre registros.",
            "4. El código de producto debe existir en el sistema.",
            "5. La ubicación debe coincidir exactamente (copiar del sistema).",
            "6. El lote debe existir en el stock.",
            "7. Hoja Ajuste_Cantidad: Tipo Ajuste solo acepta POSITIVO o NEGATIVO.",
            "8. Hoja Ajuste_Lote: solo cambia el número de lote, no la cantidad.",
            "9. Las filas 5-7 (o 5) de cada hoja son ejemplos — elimínelas.",
            "",
            "CATÁLOGO DE MOTIVOS (verificar con su administrador)",
            "ENT — Entrada / recepción de mercancía",
            "SAL — Salida / despacho",
            "INV — Ajuste por inventario físico",
            "MER — Merma o deterioro",
            "DEV — Devolución",
            "CAM — Cambio o corrección de lote/dato",
            "OTR — Otro motivo",
            "",
            "ERRORES FRECUENTES",
            "• Producto no encontrado → verificar el código en catálogo.",
            "• Ubicación no encontrada → copiar nombre exacto del sistema.",
            "• Lote no encontrado → el lote debe existir actualmente en stock.",
            "• Cantidad 0 o negativa → siempre ingresar valor positivo.",
            "• Negativo supera disponible → la cantidad a restar no puede superar la existencia.",
            "• Lote anterior = Lote nuevo → deben ser diferentes en ajuste de lote."
        }

        For i As Integer = 0 To lineas.Length - 1
            Dim r As Integer = i + 3
            ws.Cells(r, 1, r, 2).Merge = True
            ws.Cells(r, 1).Value = lineas(i)
            ws.Row(r).Height = 16

            If lineas(i).StartsWith("REGLAS") OrElse
               lineas(i).StartsWith("CATÁLOGO") OrElse
               lineas(i).StartsWith("ERRORES") Then
                ws.Cells(r, 1).Style.Font.Bold = True
                ws.Cells(r, 1).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(r, 1).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(220, 230, 241))
            End If
        Next
    End Sub

    '=========================================================================
    ' UTILIDADES
    '=========================================================================
    Private Function CeldaStr(ws As ExcelWorksheet, fila As Integer, col As Integer) As String
        Dim v As Object = ws.Cells(fila, col).Value
        Return If(v Is Nothing, "", v.ToString().Trim())
    End Function

    Private Sub Actualizar_Titulo()
        Dim tipoTexto As String = ""

        Select Case _idTipoAjuste
            Case 1
                tipoTexto = "Lote"
            Case 3
                tipoTexto = "Positivo"
            Case 5
                tipoTexto = "Negativo"
            Case Else
                tipoTexto = "Mixto (positivo/negativo/lote)"
        End Select

        lblTitulo.Text = $"Importar Ajuste desde Excel — Tipo: {tipoTexto}"
    End Sub

    Private Sub Actualizar_Status(msg As String, color As Color)
        If lblStatus IsNot Nothing Then
            lblStatus.Text = msg
            lblStatus.ForeColor = color
        End If
    End Sub

End Class