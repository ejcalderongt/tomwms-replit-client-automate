Imports System.IO
Imports System.Text
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports DevExpress.XtraEditors
Imports DevExpress.XtraBars
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting

Partial Public Class frmImportarAjusteExcel
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    ''' <summary>Lista de ajustes válidos listos para cargar en el grid.</summary>
    Public ReadOnly AjustesParaCargar As New List(Of clsBeTrans_ajuste_det)

    Private _idBodega As Integer
    Private _idPropietarioBodega As Integer
    Private _idTipoAjuste As Integer          ' 3=Positivo, 5=Negativo, 1=Lote
    Private _idMotivoDefault As Integer = 0   ' opcional
    Private _controlTallaColor As Boolean = False    ' bodega.Control_Talla_Color

    Private _rutaArchivo As String = ""
    Private _filasValidadas As New List(Of FilaAjusteExcel)

    ''' <summary>Bitácora de diagnóstico de la última validación. Se vuelca a archivo .log
    ''' al lado del .xlsx y se muestra en MessageBox cuando no hay filas válidas.</summary>
    Private _diagnostico As New StringBuilder()

    ' CLASE INTERNA: representa una fila del Excel ya validada
    Public Class FilaAjusteExcel
        Public Valida As Boolean = True
        Public vError As String = ""
        Public Fila As Integer
        Public Hoja As String

        ' Datos del Excel (ingresados por el usuario)
        Public IdUbicacionInput As Integer    ' col A — IdUbicacion numérico
        Public IdStockInput As Integer        ' col B — IdStock numérico
        Public CodigoProducto As String = "" ' col C — solo referencia visual
        Public Lote As String = ""           ' col E — solo referencia visual
        Public LicPlate As String = ""       ' col F — solo referencia visual
        Public TipoTexto As String = ""      ' "POSITIVO" | "NEGATIVO" | "LOTE"
        Public MotivoTexto As String = ""
        Public CantidadTexto As String = ""
        Public LoteNuevo As String = ""      ' solo para ajuste lote
        Public Observacion As String = ""
        Public Ubicacion As String = ""      ' se rellena desde BD tras validar

        ' Datos resueltos desde BD
        Public IdStock As Integer
        Public IdProductoBodega As Integer
        Public IdUbicacion As Integer
        Public IdMotivoAjuste As Integer
        Public IdTipoAjuste As Integer
        Public IdUnidadMedida As Integer
        Public IdPropietarioBodega As Integer
        Public IdProductoEstado As Integer
        Public IdPresentacion As Integer
        Public NombreProducto As String = ""
        Public UmBas As String = ""
        Public NombreUbicacion As String = ""
        Public NombrePresentacion As String = ""
        Public NombreProveedor As String = ""
        Public Presentacion As clsBeProducto_Presentacion = Nothing
        Public Factor As Decimal = 1
        Public CantidadOriginal As Decimal
        Public CantidadNueva As Decimal
        Public CantReservada As Decimal

        ' Talla/Color (solo aplican cuando la bodega controla talla/color).
        ' Se resuelven automáticamente desde el IdStock en Resolver_BD; NO
        ' se piden columnas en el Excel para evitar errores de tipeo.
        Public IdProductoTallaColor As Integer = 0
        Public Talla As String = ""
        Public Color As String = ""
    End Class

    Public Sub New(idBodega As Integer,
                   idPropietarioBodega As Integer,
                   idTipoAjuste As Integer,
                   Optional controlTallaColor As Boolean = False)

        InitializeComponent()

        _idBodega = idBodega
        _idPropietarioBodega = idPropietarioBodega
        _idTipoAjuste = idTipoAjuste
        _controlTallaColor = controlTallaColor

        Me.Text = "Importar Ajuste desde Excel"
        Me.Size = New Size(1100, 700)
        Me.MinimumSize = New Size(900, 600)
        Me.StartPosition = FormStartPosition.CenterParent

        Actualizar_Titulo()

    End Sub

    Private Sub gvPreview_RowStyle(sender As Object, e As RowStyleEventArgs) Handles gvPreview.RowStyle
        If e.RowHandle < 0 Then Return
        Dim view = CType(sender, GridView)
        Dim estado = TryCast(view.GetRowCellValue(e.RowHandle, "Estado"), String)
        If String.IsNullOrEmpty(estado) Then Return
        If estado.Contains("OK") Then
            e.Appearance.BackColor = Color.FromArgb(235, 250, 240)
            e.Appearance.ForeColor = Color.FromArgb(0, 100, 40)
        ElseIf estado.Contains("Error") Then
            e.Appearance.BackColor = Color.FromArgb(255, 235, 235)
            e.Appearance.ForeColor = Color.FromArgb(160, 30, 30)
        End If
    End Sub

    Private Sub btnSeleccionar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnSeleccionar.ItemClick
        Dim dlg As New OpenFileDialog() With {
            .Title = "Seleccionar archivo Excel de ajuste",
            .Filter = "Excel (*.xlsx)|*.xlsx",
            .Multiselect = False
        }
        If dlg.ShowDialog() <> DialogResult.OK Then Return

        _rutaArchivo = dlg.FileName
        txtRutaArchivo.Text = _rutaArchivo
        btnValidar.Enabled = True
        btnProcesar.Enabled = False
        btnImprimirDatos.Enabled = False
        btnImprimirErrores.Enabled = False
        dtPreview.Rows.Clear()
        dtErrores.Rows.Clear()
        _filasValidadas.Clear()
        Actualizar_Status("Archivo seleccionado. Haga clic en Validar.", Color.FromArgb(0, 90, 160))
    End Sub

    Private Sub btnDescargarPlantilla_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnDescargarPlantilla.ItemClick
        Generar_Plantilla()
    End Sub

    Private Sub btnValidar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnValidar.ItemClick
        If String.IsNullOrEmpty(_rutaArchivo) Then Return
        Validar_Archivo()
    End Sub

    Private Sub btnProcesar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnProcesar.ItemClick
        Dim validas = _filasValidadas.Where(Function(f) f.Valida).ToList()
        Dim conError = _filasValidadas.Where(Function(f) Not f.Valida).Count()

        If validas.Count = 0 Then
            MessageBox.Show("No hay filas válidas para cargar.", "Sin datos válidos",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' BYPASS: si hay errores, pedir confirmación para omitirlos
        If conError > 0 Then
            Dim msg As String =
                "Se detectaron " & conError & " fila(s) con error que serán OMITIDAS." & vbCrLf & vbCrLf &
                "¿Desea continuar y cargar únicamente las " & validas.Count & " fila(s) válida(s)?" & vbCrLf & vbCrLf &
                "(Las filas con error pueden corregirse en el Excel y reimportarse después.)"
            Dim resp = XtraMessageBox.Show(msg, "Bypass de filas con error",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp <> DialogResult.Yes Then Return
        End If

        ' Mapear a clsBeTrans_ajuste_det y llenar la lista pública
        AjustesParaCargar.Clear()
        For Each f As FilaAjusteExcel In validas
            Dim det As New clsBeTrans_ajuste_det()
            det.IdAjusteDet = 0
            det.IdAjusteEnc = 0                    ' frmAjusteStock lo asignará
            det.IdStock = f.IdStock
            det.IdPropietarioBodega = f.IdPropietarioBodega
            det.IdProductoBodega = f.IdProductoBodega
            det.IdProductoEstado = f.IdProductoEstado
            det.IdPresentacion = f.IdPresentacion
            det.IdUnidadMedida = f.IdUnidadMedida
            det.IdUbicacion = f.IdUbicacion
            det.Lote_original = f.Lote
            det.Lote_nuevo = If(f.IdTipoAjuste = 1, f.LoteNuevo, f.Lote)
            det.Fecha_vence_original = New Date(1900, 1, 1)
            det.Fecha_vence_nueva = New Date(1900, 1, 1)
            det.Peso_original = 0
            det.Peso_nuevo = 0
            ' Cantidad_original / Cantidad_nueva van en UM base (ya convertidas
            ' en Resolver_BD multiplicando por Factor cuando aplica).
            det.Cantidad_original = f.CantidadOriginal
            det.Cantidad_nueva = f.CantidadNueva
            det.CantReservada = f.CantReservada
            det.UmBas = f.UmBas
            det.Factor = f.Factor
            det.Nombre_Presentacion = f.NombrePresentacion
            det.Codigo_producto = f.CodigoProducto
            det.Nombre_producto = f.NombreProducto
            det.Idtipoajuste = f.IdTipoAjuste
            det.IdMotivoAjuste = f.IdMotivoAjuste
            det.Observacion = f.Observacion
            det.Codigo_ajuste = 0
            det.Enviado = False
            det.lic_plate = f.LicPlate
            det.idstockres = 0
            det.idstocklink = 0
            det.esnuevolink = 0
            ' Talla/Color: solo se popularon en Resolver_BD si _controlTallaColor=True.
            ' Para Ajuste_Cantidad la combinación no cambia → destino = origen.
            det.IdProductoTallaColor_origen = f.IdProductoTallaColor
            det.Talla_origen = f.Talla
            det.Color_origen = f.Color
            det.IdProductoTallaColor_destino = f.IdProductoTallaColor
            det.Talla_destino = f.Talla
            det.Color_destino = f.Color
            If f.Presentacion IsNot Nothing Then det.Presentacion = f.Presentacion
            AjustesParaCargar.Add(det)
        Next

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancelar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnCancelar.ItemClick
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' Garantiza que dtPreview y dtErrores existan y tengan el esquema de
    ''' columnas correcto. Se llama antes de cualquier Rows.Add para evitar
    ''' el error "La matriz de entrada es más larga que el número de columnas
    ''' en esta tabla" si el Designer no inicializó las DataTables a tiempo.
    ''' </summary>
    Private Sub Asegurar_Esquema_DataTables()

        If dtPreview Is Nothing Then dtPreview = New DataTable("Preview")
        If dtPreview.Columns.Count = 0 Then
            Dim cols As String() = {
                "Estado", "Hoja", "Fila", "IdUbicacion", "Ubicacion",
                "IdStock", "Codigo", "Nombre", "Proveedor", "Lote", "LoteNuevo",
                "Talla", "Color",
                "Tipo", "Cantidad", "Motivo", "Observacion"
            }
            For Each n As String In cols
                dtPreview.Columns.Add(n, GetType(String))
            Next
        End If
        If gridPreview IsNot Nothing AndAlso gridPreview.DataSource Is Nothing Then
            gridPreview.DataSource = dtPreview
        End If

        If dtErrores Is Nothing Then dtErrores = New DataTable("Errores")
        If dtErrores.Columns.Count = 0 Then
            dtErrores.Columns.Add("Hoja", GetType(String))
            dtErrores.Columns.Add("Fila", GetType(Integer))
            dtErrores.Columns.Add("Codigo", GetType(String))
            dtErrores.Columns.Add("Error", GetType(String))
        End If
        If gridErrores IsNot Nothing AndAlso gridErrores.DataSource Is Nothing Then
            gridErrores.DataSource = dtErrores
        End If
    End Sub

    Private Sub Validar_Archivo()

        Asegurar_Esquema_DataTables()

        _filasValidadas.Clear()
        dtPreview.Rows.Clear()
        dtErrores.Rows.Clear()
        btnProcesar.Enabled = False
        progressBar.Visible = True
        Application.DoEvents()

        ' Reset bitácora de diagnóstico de esta corrida.
        _diagnostico.Clear()
        _diagnostico.AppendLine("=== Importar Ajuste desde Excel — Diagnóstico de validación ===")
        _diagnostico.AppendLine("Fecha:       " & Now.ToString("yyyy-MM-dd HH:mm:ss"))
        _diagnostico.AppendLine("Usuario WMS: " & Environment.UserName)
        _diagnostico.AppendLine("Archivo:     " & _rutaArchivo)
        _diagnostico.AppendLine("Padre:       IdBodega=" & _idBodega &
                                "  IdPropietarioBodega=" & _idPropietarioBodega &
                                "  IdTipoAjuste=" & _idTipoAjuste &
                                "  (" & Obtener_Tipo_Texto() & ")")
        _diagnostico.AppendLine(New String("-"c, 70))

        Dim filasOtraHoja As Integer = 0
        Dim nombreOtraHojaInfo As String = ""

        Dim faseActual As String = "init"
        Try
            faseActual = "Set_EPPlus_License"
            Set_EPPlus_License()

            ' ─── Validar que el tipo de ajuste del padre soporta importación ─
            ' Solo se admiten dos modos:
            '   _idTipoAjuste = 3 → "Ajuste x Cantidad (+/-)"  → hoja Ajuste_Cantidad
            '   _idTipoAjuste = 1 → "Ajuste de Lote"            → hoja Ajuste_Lote
            ' Cualquier otro tipo predefinido (Vencimiento, Estado, etc.) NO se importa.
            If _idTipoAjuste <> 3 AndAlso _idTipoAjuste <> 1 Then
                XtraMessageBox.Show(
                    "La importación por Excel solo está habilitada para los tipos:" & vbCrLf &
                    "   • Ajuste x Cantidad (+/-)" & vbCrLf &
                    "   • Ajuste de Lote" & vbCrLf & vbCrLf &
                    "Tipo recibido desde el formulario padre: IdTipoAjuste = " & _idTipoAjuste & "." & vbCrLf &
                    "Para otros tipos predefinidos, registre el ajuste manualmente.",
                    "Tipo de ajuste no soportado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                progressBar.Visible = False
                Return
            End If

            faseActual = "Apertura del archivo Excel"
            Using pkg As New ExcelPackage(New FileInfo(_rutaArchivo))

                Dim wsCant As ExcelWorksheet = pkg.Workbook.Worksheets("Ajuste_Cantidad")
                Dim wsLote As ExcelWorksheet = pkg.Workbook.Worksheets("Ajuste_Lote")

                Dim wsActiva As ExcelWorksheet = Nothing
                Dim nombreHojaEsperada As String = ""
                If _idTipoAjuste = 3 Then
                    wsActiva = wsCant
                    nombreHojaEsperada = "Ajuste_Cantidad"
                Else  ' _idTipoAjuste = 1
                    wsActiva = wsLote
                    nombreHojaEsperada = "Ajuste_Lote"
                End If

                If wsActiva Is Nothing Then
                    XtraMessageBox.Show(
                        "El archivo no contiene la hoja '" & nombreHojaEsperada & "'," & vbCrLf &
                        "que es la requerida para el tipo de ajuste seleccionado" & vbCrLf &
                        "en el formulario padre (IdTipoAjuste = " & _idTipoAjuste & ")." & vbCrLf & vbCrLf &
                        "Use la plantilla oficial (botón 'Descargar Plantilla').",
                        "Hoja faltante", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    progressBar.Visible = False
                    Return
                End If

                ' Cuántas filas con datos hay en la hoja activa y en la otra hoja.
                ' Esto es crítico para detectar el caso "datos en hoja equivocada"
                ' al final de la validación.
                Dim wsOtra As ExcelWorksheet = If(_idTipoAjuste = 3, wsLote, wsCant)
                Dim nombreOtraHoja As String = If(_idTipoAjuste = 3, "Ajuste_Lote", "Ajuste_Cantidad")
                nombreOtraHojaInfo = nombreOtraHoja
                filasOtraHoja = Contar_Filas_Con_Datos(wsOtra)
                Dim filasEstaHoja As Integer = Contar_Filas_Con_Datos(wsActiva)
                _diagnostico.AppendLine("Hoja activa  '" & nombreHojaEsperada & "': " &
                                        filasEstaHoja & " fila(s) con IdUbicacion no vacía.")
                _diagnostico.AppendLine("Hoja inactiva '" & nombreOtraHoja & "':  " &
                                        filasOtraHoja & " fila(s) con IdUbicacion no vacía.")
                _diagnostico.AppendLine(New String("-"c, 70))

                If filasOtraHoja > 0 Then
                    Actualizar_Status(
                        "Aviso: la hoja '" & nombreOtraHoja & "' será IGNORADA " &
                        "porque el ajuste padre es '" & Obtener_Tipo_Texto() & "'.",
                        Color.FromArgb(160, 90, 0))
                End If

                If _idTipoAjuste = 3 Then
                    faseActual = "Leer_Hoja_Cantidad"
                    Leer_Hoja_Cantidad(wsActiva)
                Else
                    faseActual = "Leer_Hoja_Lote"
                    Leer_Hoja_Lote(wsActiva)
                End If

            End Using

            faseActual = "Resolver_BD"
            Resolver_BD()

            faseActual = "Poblar_Grillas"
            Poblar_Grillas()

            Dim nValidas = _filasValidadas.Where(Function(f) f.Valida).Count()
            Dim nErrores = _filasValidadas.Where(Function(f) Not f.Valida).Count()

            tabErrores.Text = "Errores de Validación  (" & nErrores & ")"

            btnImprimirDatos.Enabled = (nValidas + nErrores) > 0
            btnImprimirErrores.Enabled = nErrores > 0

            _diagnostico.AppendLine(New String("="c, 70))
            _diagnostico.AppendLine("RESULTADO: " & nValidas & " válida(s), " &
                                    nErrores & " con error, " &
                                    _filasValidadas.Count & " procesada(s) en total.")

            If nValidas > 0 Then
                btnProcesar.Enabled = True
                Actualizar_Status(nValidas & " fila(s) válidas para cargar. " &
                                  If(nErrores > 0, nErrores & " con error.", ""),
                                  If(nErrores > 0, Color.FromArgb(160, 90, 0), Color.FromArgb(0, 110, 40)))
                If nErrores > 0 Then tabControl.SelectedTabPageIndex = 1
            Else
                Actualizar_Status("No hay filas válidas. Corrija los errores indicados.", Color.FromArgb(180, 30, 30))
                tabControl.SelectedTabPageIndex = 1
            End If

            ' ─── Guardar log SIEMPRE para auditoría ───
            Dim logPath As String = Guardar_Log_Diagnostico()
            If Not String.IsNullOrEmpty(logPath) Then
                _diagnostico.AppendLine("Log guardado en: " & logPath)
            End If

            ' ─── Mensaje modal específico cuando no hay NADA en el preview ───
            If _filasValidadas.Count = 0 Then

                Dim cabezal As String =
                    "La validación no generó filas." & vbCrLf & vbCrLf &
                    "Tipo de ajuste padre: " & Obtener_Tipo_Texto() &
                        " (IdTipoAjuste=" & _idTipoAjuste & ")" & vbCrLf &
                    "Hoja leída:           " &
                        If(_idTipoAjuste = 3, "Ajuste_Cantidad", "Ajuste_Lote") &
                        " (filas con datos a partir de la 6: 0)" & vbCrLf

                If filasOtraHoja > 0 Then
                    ' Caso típico: el usuario puso datos en la hoja equivocada.
                    cabezal &= "Otra hoja:            " & nombreOtraHojaInfo &
                               " (filas con datos a partir de la 6: " & filasOtraHoja & ")" & vbCrLf & vbCrLf &
                               "→ Parece que los datos están en la hoja '" & nombreOtraHojaInfo &
                               "', pero el ajuste padre es de tipo '" & Obtener_Tipo_Texto() &
                               "', que solo lee la hoja '" &
                               If(_idTipoAjuste = 3, "Ajuste_Cantidad", "Ajuste_Lote") & "'." & vbCrLf & vbCrLf &
                               "Cierre este importador, cree un nuevo ajuste con el tipo correspondiente " &
                               "a la hoja donde están los datos, y vuelva a importar."
                Else
                    cabezal &= vbCrLf &
                               "→ Ninguna fila tiene IdUbicacion numérico válido a partir de la fila 6." & vbCrLf &
                               "Verifique que los datos comiencen en la fila 6 (filas 1-5 son cabecera/ejemplo) " &
                               "y que la columna A contenga el IdUbicacion."
                End If

                If Not String.IsNullOrEmpty(logPath) Then
                    cabezal &= vbCrLf & vbCrLf & "Log de diagnóstico: " & logPath
                End If

                XtraMessageBox.Show(cabezal,
                                    "Sin filas para validar",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning)

            ElseIf nValidas = 0 Then
                ' Hubo filas pero todas fallaron en Resolver_BD: la pestaña Errores
                ' ya las muestra, pero el usuario puede no notarlo. Toast modal.
                XtraMessageBox.Show(
                    "Se procesaron " & nErrores & " fila(s) pero TODAS fallaron en validación contra la base de datos." & vbCrLf & vbCrLf &
                    "Revise la pestaña 'Errores de Validación' para ver el detalle de cada fila." & vbCrLf &
                    If(Not String.IsNullOrEmpty(logPath), vbCrLf & "Log: " & logPath, ""),
                    "Validación con errores",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            _diagnostico.AppendLine(vbCrLf & "EXCEPCIÓN en fase '" & faseActual & "':")
            _diagnostico.AppendLine("  " & ex.Message)
            If ex.InnerException IsNot Nothing Then
                _diagnostico.AppendLine("  Inner: " & ex.InnerException.Message)
            End If
            _diagnostico.AppendLine("  Stack: " & ex.StackTrace)
            Guardar_Log_Diagnostico()

            Dim detalle As String =
                "Fase: " & faseActual & vbCrLf &
                "Mensaje: " & ex.Message & vbCrLf & vbCrLf &
                "Origen: " & ex.Source & vbCrLf &
                If(ex.InnerException IsNot Nothing,
                   "Detalle interno: " & ex.InnerException.Message & vbCrLf, "") &
                vbCrLf & "Stack:" & vbCrLf & ex.StackTrace
            XtraMessageBox.Show(detalle, "Error en " & faseActual,
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            clsLnLog_error_wms.Agregar_Error(
                "frmImportarAjusteExcel.Validar_Archivo [" & faseActual & "]: " &
                ex.Message & " | Stack: " & ex.StackTrace)
        Finally
            progressBar.Visible = False
        End Try

    End Sub

    ' Columnas: A=IdUbicacion(*) B=IdStock(*) C=Código(ref) D=Nombre(ref)
    '           E=Lote(ref) F=LicPlate(ref) G=Disponible(ref)
    '           H=TipoAjuste(*) I=Motivo(*) J=Cantidad(*) K=Observación
    ' Fila 4=cabecera, fila 5=ejemplo ignorado, datos desde fila 6
    Private Sub Leer_Hoja_Cantidad(ws As ExcelWorksheet)
        Dim filaFin As Integer = If(ws.Dimension Is Nothing, 0, ws.Dimension.End.Row)
        _diagnostico.AppendLine("[" & Now.ToString("HH:mm:ss") & "] Leer_Hoja_Cantidad: dimensión hasta fila " & filaFin)
        If filaFin < 6 Then
            _diagnostico.AppendLine("    La hoja no tiene filas a partir de la 6. Nada que leer.")
            Return
        End If

        Dim leidas As Integer = 0
        Dim saltadas As Integer = 0
        For r As Integer = 6 To filaFin
            Dim idUbic As Integer = CeldaInt(ws, r, 1)
            If idUbic = 0 Then
                saltadas += 1
                Continue For   ' fila vacía o IdUbicacion no numérico = skip
            End If

            Dim f As New FilaAjusteExcel() With {
                .Hoja = "Ajuste_Cantidad",
                .Fila = r,
                .IdUbicacionInput = idUbic,
                .IdStockInput = CeldaInt(ws, r, 2),
                .CodigoProducto = CeldaStr(ws, r, 3),   ' referencia visual
                .Lote = CeldaStr(ws, r, 5),             ' referencia visual
                .LicPlate = CeldaStr(ws, r, 6),         ' referencia visual
                .TipoTexto = CeldaStr(ws, r, 8).Trim().ToUpper(),
                .MotivoTexto = CeldaStr(ws, r, 9).Trim().ToUpper(),
                .CantidadTexto = CeldaStr(ws, r, 10).Trim(),
                .Observacion = CeldaStr(ws, r, 11).Trim()
            }
            _filasValidadas.Add(f)
            leidas += 1
        Next
        _diagnostico.AppendLine("    Filas leídas: " & leidas & "  |  saltadas (col A vacía o no numérica): " & saltadas)
    End Sub

    ' Columnas: A=IdUbicacion(*) B=IdStock(*) C=Código(ref) D=Nombre(ref)
    '           E=LoteAnterior(ref) F=LoteNuevo(*) G=Motivo(*) H=Observación
    ' Fila 4=cabecera, fila 5=ejemplo ignorado, datos desde fila 6
    Private Sub Leer_Hoja_Lote(ws As ExcelWorksheet)
        Dim filaFin As Integer = If(ws.Dimension Is Nothing, 0, ws.Dimension.End.Row)
        _diagnostico.AppendLine("[" & Now.ToString("HH:mm:ss") & "] Leer_Hoja_Lote: dimensión hasta fila " & filaFin)
        If filaFin < 6 Then
            _diagnostico.AppendLine("    La hoja no tiene filas a partir de la 6. Nada que leer.")
            Return
        End If

        Dim leidas As Integer = 0
        Dim saltadas As Integer = 0
        For r As Integer = 6 To filaFin
            Dim idUbic As Integer = CeldaInt(ws, r, 1)
            If idUbic = 0 Then
                saltadas += 1
                Continue For   ' fila vacía o IdUbicacion no numérico = skip
            End If

            Dim f As New FilaAjusteExcel() With {
                .Hoja = "Ajuste_Lote",
                .Fila = r,
                .IdUbicacionInput = idUbic,
                .IdStockInput = CeldaInt(ws, r, 2),
                .CodigoProducto = CeldaStr(ws, r, 3),   ' referencia visual
                .Lote = CeldaStr(ws, r, 5),             ' lote anterior (referencia)
                .LoteNuevo = CeldaStr(ws, r, 6).Trim(),
                .MotivoTexto = CeldaStr(ws, r, 7).Trim().ToUpper(),
                .Observacion = CeldaStr(ws, r, 8).Trim(),
                .TipoTexto = "LOTE"
            }
            _filasValidadas.Add(f)
            leidas += 1
        Next
        _diagnostico.AppendLine("    Filas leídas: " & leidas & "  |  saltadas (col A vacía o no numérica): " & saltadas)
    End Sub

    Private Sub Resolver_BD()

        For Each f As FilaAjusteExcel In _filasValidadas

            Try
                If f.IdUbicacionInput = 0 Then _
                    Throw New Exception("IdUbicacion inválido o vacío.")
                If f.IdStockInput = 0 Then _
                    Throw New Exception("IdStock inválido o vacío.")
                If String.IsNullOrWhiteSpace(f.MotivoTexto) Then _
                    Throw New Exception("Código de motivo vacío.")

                If f.Hoja = "Ajuste_Cantidad" Then
                    ' Regla: cuando el ajuste padre es 'Ajuste x Cantidad' (IdTipoAjuste=3),
                    ' la columna 'Tipo Ajuste' del Excel SOLO admite POSITIVO o NEGATIVO.
                    ' Los demás tipos predefinidos (Lote, Vencimiento, Estado, etc.)
                    ' no modifican cantidad y no se importan por esta hoja.
                    If f.TipoTexto <> "POSITIVO" AndAlso f.TipoTexto <> "NEGATIVO" Then
                        Throw New Exception(
                            "Tipo Ajuste inválido '" & f.TipoTexto & "'. " &
                            "Para 'Ajuste x Cantidad (+/-)' solo se permite POSITIVO o NEGATIVO. " &
                            "Otros tipos predefinidos (Lote, Vencimiento, Estado, etc.) " &
                            "deben registrarse desde su propio formulario.")
                    End If
                    Dim cant As Decimal
                    If Not Decimal.TryParse(f.CantidadTexto.Replace(",", "."),
                                            Globalization.NumberStyles.Any,
                                            Globalization.CultureInfo.InvariantCulture,
                                            cant) OrElse cant <= 0 Then
                        Throw New Exception("Cantidad inválida '" & f.CantidadTexto & "'. Debe ser un número mayor a 0.")
                    End If
                    f.CantidadTexto = cant.ToString()

                ElseIf f.Hoja = "Ajuste_Lote" Then
                    If String.IsNullOrWhiteSpace(f.LoteNuevo) Then _
                        Throw New Exception("Lote nuevo vacío.")
                    If f.Lote.Equals(f.LoteNuevo, StringComparison.OrdinalIgnoreCase) Then _
                        Throw New Exception("Lote anterior y nuevo son iguales.")
                End If

                ' Usa método ya existente en clsLnBodega_ubicacion_Partial.vb
                Dim beUbic As clsBeBodega_ubicacion =
                    clsLnBodega_ubicacion.Get_Single_By_IdUbicacion_And_IdBodega(
                        f.IdUbicacionInput, _idBodega)
                If beUbic Is Nothing OrElse beUbic.IdUbicacion = 0 Then
                    Throw New Exception("IdUbicacion " & f.IdUbicacionInput &
                                        " no existe en la bodega IdBodega=" & _idBodega & ".")
                End If
                f.NombreUbicacion = beUbic.NombreCompleto

                ' Usa la sobrecarga existente clsLnStock.GetSingle(ByRef beStock)
                ' que rellena el objeto a partir del IdStock seteado.
                Dim beStock As New clsBeStock With {.IdStock = f.IdStockInput}
                clsLnStock.GetSingle(beStock)
                If beStock Is Nothing OrElse beStock.IdStock = 0 Then
                    Throw New Exception("No se encontró stock con IdStock=" & f.IdStockInput & ".")
                End If

                '── Cross-validate: el stock pertenece a la ubicación dada
                If beStock.IdUbicacion <> f.IdUbicacionInput Then
                    Throw New Exception("El stock IdStock=" & f.IdStockInput &
                                        " pertenece a IdUbicacion=" & beStock.IdUbicacion &
                                        ", no a IdUbicacion=" & f.IdUbicacionInput & ".")
                End If

                Dim idMotivo As Integer = clsLnAjuste_motivo.Get_IdMotivo_By_Codigo(f.MotivoTexto)
                If idMotivo = 0 Then Throw New Exception("Motivo no encontrado: " & f.MotivoTexto)

                Dim idProducto As Integer = beStock.IdProductoBodega  ' ya disponible en beStock
                Dim beProducto As clsBeProducto = clsLnProducto.Get_Single_By_IdProducto(
                                                      clsLnProducto_bodega.Get_IdProducto_By_IdProductoBodega(
                                                          beStock.IdProductoBodega))

                Dim factor As Decimal = 1
                Dim nombrePres As String = ""
                Dim bePresObj As clsBeProducto_Presentacion = Nothing

                If beStock.IdPresentacion <> 0 Then
                    bePresObj = clsLnProducto_presentacion.GetSingle(beStock.IdPresentacion)
                    If bePresObj IsNot Nothing Then
                        factor = CDec(bePresObj.Factor)
                        nombrePres = bePresObj.Nombre
                    End If
                End If

                ' clsBeStock.Cantidad = existencia total en UM base
                ' clsBeStock.Cantidad_Reservada = cantidad reservada en UM base
                Dim cantDisp As Decimal = CDec(beStock.Cantidad) - CDec(beStock.Cantidad_Reservada)

                ' Convertir a unidades de presentación si aplica
                Dim cantDispPres As Decimal = If(factor > 1, Math.Round(cantDisp / factor, 6), cantDisp)
                Dim cantResPres As Decimal = If(factor > 1, Math.Round(CDec(beStock.Cantidad_Reservada) / factor, 6), CDec(beStock.Cantidad_Reservada))

                Dim idTipoAj As Integer
                If f.Hoja = "Ajuste_Lote" Then
                    idTipoAj = 1
                ElseIf f.TipoTexto = "POSITIVO" Then
                    idTipoAj = 3
                Else
                    idTipoAj = 5
                End If

                Dim cantNueva As Decimal = cantDispPres

                If f.Hoja = "Ajuste_Cantidad" Then
                    Dim cantAjuste As Decimal = CDec(f.CantidadTexto.Replace(",", "."))

                    If idTipoAj = 5 Then ' Negativo
                        If cantAjuste > cantDispPres Then
                            Throw New Exception("Cantidad a ajustar (" & cantAjuste &
                                                ") supera la disponible (" & cantDispPres & ").")
                        End If
                        cantNueva = cantDispPres - cantAjuste
                    Else                  ' Positivo
                        cantNueva = cantDispPres + cantAjuste
                    End If
                End If
                ' Para ajuste de lote, cantidad_nueva = cantidad_original (no cambia)

                f.IdStock = beStock.IdStock
                f.IdProductoBodega = beStock.IdProductoBodega
                f.IdUbicacion = f.IdUbicacionInput        ' ya validado
                f.IdMotivoAjuste = idMotivo
                f.IdTipoAjuste = idTipoAj
                f.IdUnidadMedida = beStock.IdUnidadMedida
                f.IdPropietarioBodega = beStock.IdPropietarioBodega
                f.IdProductoEstado = beStock.IdProductoEstado
                f.IdPresentacion = beStock.IdPresentacion
                f.NombreProducto = If(beProducto IsNot Nothing, beProducto.Nombre, f.CodigoProducto)
                f.UmBas = clsLnUnidad_medida.Get_Nombre_By_IdUnidadMedida(beStock.IdUnidadMedida)
                f.NombreUbicacion = "Id:" & f.IdUbicacionInput.ToString()
                f.Lote = If(String.IsNullOrWhiteSpace(f.Lote), beStock.Lote, f.Lote)
                f.NombrePresentacion = nombrePres
                f.Factor = factor
                f.Presentacion = bePresObj

                ' ── Resolución talla/color ───────────────────────────────
                ' Solo aplica si la bodega controla talla/color. La combinación
                ' viene determinada por el IdStock (que ya identifica unívocamente
                ' IdProductoBodega + Lote + IdProductoTallaColor). NO se piden
                ' columnas Talla/Color en el Excel para evitar errores de tipeo:
                ' se resuelven aquí y se muestran en el preview como referencia.
                If _controlTallaColor Then
                    If beStock.IdProductoTallaColor > 0 Then
                        f.IdProductoTallaColor = beStock.IdProductoTallaColor
                        Try
                            Dim dtTC As DataTable =
                                clsLnProducto_talla_color.Get_Single_Dt_By_IdProductoTallaColor(
                                    beStock.IdProductoTallaColor)
                            If dtTC IsNot Nothing AndAlso dtTC.Rows.Count > 0 Then
                                f.Talla = If(IsDBNull(dtTC.Rows(0).Item("Talla")), "",
                                             dtTC.Rows(0).Item("Talla").ToString())
                                f.Color = If(IsDBNull(dtTC.Rows(0).Item("Color")), "",
                                             dtTC.Rows(0).Item("Color").ToString())
                            End If
                        Catch
                            ' Si la consulta falla, dejamos talla/color como string
                            ' vacío pero conservamos el IdProductoTallaColor para
                            ' que el frmAjusteStock pueda hacer el lookup por su lado.
                        End Try
                    Else
                        ' Bodega controla pero el stock no tiene combinación asignada:
                        ' registro inconsistente, marcamos la fila como error.
                        Throw New Exception(
                            "La bodega IdBodega=" & _idBodega &
                            " controla talla/color, pero el IdStock=" & f.IdStockInput &
                            " no tiene IdProductoTallaColor asignado.")
                    End If
                End If

                ' Proveedor (cadena: stock → trans_re_det → trans_oc_enc → proveedor_bodega → proveedor)
                ' Devuelve Nothing si el stock no proviene de una recepción contra OC.
                Try
                    Dim beProv As clsBeProveedor = clsLnProveedor.Get_Single_By_IdStock(beStock.IdStock)
                    If beProv IsNot Nothing Then
                        If Not String.IsNullOrWhiteSpace(beProv.Codigo) Then
                            f.NombreProveedor = beProv.Codigo & " - " & beProv.Nombre
                        Else
                            f.NombreProveedor = beProv.Nombre
                        End If
                    End If
                Catch
                    ' No bloquea la fila si la consulta de proveedor falla.
                    f.NombreProveedor = ""
                End Try
                ' IMPORTANTE: Cantidad_original y Cantidad_nueva se almacenan SIEMPRE
                ' en UM base (igual que Cargar_Detalle del frm padre). El consumer
                ' divide por Factor solo para visualización en el grid.
                f.CantidadOriginal = cantDisp
                f.CantidadNueva = If(f.Hoja = "Ajuste_Cantidad",
                                     cantNueva * factor,
                                     cantDisp)
                f.CantReservada = CDec(beStock.Cantidad_Reservada)
                f.Valida = True

            Catch ex As Exception
                f.Valida = False
                f.vError = ex.Message
                _diagnostico.AppendLine("    Fila " & f.Fila & " ERROR: " & ex.Message)
            End Try

        Next

        Dim okCount = _filasValidadas.Where(Function(x) x.Valida).Count()
        Dim errCount = _filasValidadas.Where(Function(x) Not x.Valida).Count()
        _diagnostico.AppendLine("[" & Now.ToString("HH:mm:ss") & "] Resolver_BD: " &
                                okCount & " válidas, " & errCount & " con error.")

    End Sub

    Private Sub Poblar_Grillas()

        Asegurar_Esquema_DataTables()

        dtPreview.BeginLoadData()
        dtErrores.BeginLoadData()
        Try
            dtPreview.Rows.Clear()
            dtErrores.Rows.Clear()

            For Each f As FilaAjusteExcel In _filasValidadas

                ' Texto de cantidad para preview (en unidades de presentación,
                ' tal cual lo ingresó el usuario en el Excel).
                Dim cantTexto As String = ""
                If f.Valida Then
                    If f.IdTipoAjuste = 1 Then
                        cantTexto = "→ " & f.LoteNuevo
                    ElseIf f.IdTipoAjuste = 3 Then
                        cantTexto = "+" & f.CantidadTexto
                    Else
                        cantTexto = "-" & f.CantidadTexto
                    End If
                Else
                    cantTexto = f.CantidadTexto
                End If

                Dim loteNuevoMostrar As String = If(f.IdTipoAjuste = 1, f.LoteNuevo, "")

                dtPreview.Rows.Add(
                    If(f.Valida, "✔ OK", "✘ Error"),
                    f.Hoja,
                    f.Fila.ToString(),
                    f.IdUbicacionInput.ToString(),
                    f.NombreUbicacion,
                    f.IdStockInput.ToString(),
                    f.CodigoProducto,
                    f.NombreProducto,
                    f.NombreProveedor,
                    f.Lote,
                    loteNuevoMostrar,
                    f.Talla,
                    f.Color,
                    f.TipoTexto,
                    cantTexto,
                    f.MotivoTexto,
                    f.Observacion
                )

                If Not f.Valida Then
                    dtErrores.Rows.Add(f.Hoja, f.Fila, f.CodigoProducto, f.vError)
                End If
            Next
        Finally
            dtPreview.EndLoadData()
            dtErrores.EndLoadData()
            gvPreview.RefreshData()
            gvErrores.RefreshData()
        End Try

    End Sub

    Private Sub Generar_Plantilla()

        Dim dlg As New SaveFileDialog() With {
            .Title = "Guardar Plantilla de Importación",
            .Filter = "Excel (*.xlsx)|*.xlsx",
            .FileName = "Plantilla_Ajuste_Stock.xlsx"
        }
        If dlg.ShowDialog() <> DialogResult.OK Then Return

        Try
            Set_EPPlus_License()

            Using pkg As New ExcelPackage()

                Dim ws1 As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Ajuste_Cantidad")
                Crear_Hoja_Cantidad(ws1)

                Dim ws2 As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Ajuste_Lote")
                Crear_Hoja_Lote(ws2)

                Dim ws3 As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Instrucciones")
                Crear_Hoja_Instrucciones(ws3)

                pkg.SaveAs(New FileInfo(dlg.FileName))

            End Using

            MessageBox.Show("Plantilla guardada en:" & Environment.NewLine & dlg.FileName,
                            "Plantilla generada", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error al generar la plantilla: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ''' <summary>
    ''' Asigna una imagen del recurso My.Resources al botón del ribbon (LargeImage + Image).
    ''' Usa reflexión para no fallar en compile-time si el recurso no existe en este branch.
    ''' </summary>

    ''' <summary>
    ''' Configura la licencia de EPPlus de manera compatible con cualquier versión instalada
    ''' (v4 no requiere licencia, v5 usa LicenseContext, v6+ usa License.SetNonCommercialPersonal).
    ''' Usa reflexión para no fallar en compile-time si la API no existe.
    ''' </summary>
    Private Sub Set_EPPlus_License()
        Try
            Dim t As Type = GetType(ExcelPackage)

            ' EPPlus v6+: ExcelPackage.License.SetNonCommercialPersonal("...")
            Dim pLicense As Reflection.PropertyInfo = t.GetProperty("License", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static)
            If pLicense IsNot Nothing Then
                Dim licObj As Object = pLicense.GetValue(Nothing, Nothing)
                If licObj IsNot Nothing Then
                    Dim mSet As Reflection.MethodInfo = licObj.GetType().GetMethod("SetNonCommercialPersonal", New Type() {GetType(String)})
                    If mSet IsNot Nothing Then
                        mSet.Invoke(licObj, New Object() {"WMS"})
                        Return
                    End If
                End If
            End If

            ' EPPlus v5: ExcelPackage.LicenseContext = LicenseContext.NonCommercial
            Dim pCtx As Reflection.PropertyInfo = t.GetProperty("LicenseContext", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static)
            If pCtx IsNot Nothing Then
                Dim ctxType As Type = pCtx.PropertyType
                ' Si es Nullable(Of LicenseContext), obtener el tipo subyacente
                Dim underlying As Type = Nullable.GetUnderlyingType(ctxType)
                If underlying IsNot Nothing Then ctxType = underlying
                Dim val As Object = [Enum].Parse(ctxType, "NonCommercial")
                pCtx.SetValue(Nothing, val, Nothing)
                Return
            End If

            ' EPPlus v4 o anterior: no requiere licencia, no hacer nada.
        Catch
            ' Silencioso: si falla la licencia, EPPlus mostrará su propio mensaje al abrir el paquete.
        End Try
    End Sub

    Private Sub Crear_Hoja_Cantidad(ws As ExcelWorksheet)
        ' ── Columnas:
        ' A=IdUbicacion(*) B=IdStock(*) C=Código(ref) D=Nombre(ref)
        ' E=Lote(ref) F=LicPlate(ref) G=Disponible(ref)
        ' H=TipoAjuste(*) I=Motivo(*) J=Cantidad(*) K=Observación
        ' Fila 4=cabecera, fila 5=ejemplo, datos desde fila 6

        ' Título
        ws.Cells("A1:K1").Merge = True
        ws.Cells("A1").Value = "AJUSTE POR CANTIDAD — POSITIVO / NEGATIVO"
        ws.Cells("A1").Style.Font.Bold = True : ws.Cells("A1").Style.Font.Size = 13
        ws.Cells("A1").Style.Font.Color.SetColor(Color.White)
        ws.Cells("A1").Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells("A1").Style.Fill.BackgroundColor.SetColor(Color.FromArgb(31, 73, 125))
        ws.Cells("A1").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        ws.Row(1).Height = 24

        ' Nota
        ws.Cells("A2:K2").Merge = True
        ws.Cells("A2").Value = "Ingrese solo IdUbicacion e IdStock (*). " &
                               "Las columnas C-G son referencia visual (no se procesan). " &
                               "Fila 5=ejemplo (elimínela antes de importar). Datos desde fila 6."
        ws.Cells("A2").Style.Font.Italic = True
        ws.Cells("A2").Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells("A2").Style.Fill.BackgroundColor.SetColor(Color.FromArgb(220, 230, 241))

        ' Leyenda de colores fila 3
        ws.Cells("A3:K3").Merge = True
        ws.Cells("A3").Value = "▐  Fondo azul = obligatorio  |  Fondo gris = solo referencia (no editar)"
        ws.Cells("A3").Style.Font.Italic = True : ws.Cells("A3").Style.Font.Size = 8
        ws.Cells("A3").Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells("A3").Style.Fill.BackgroundColor.SetColor(Color.FromArgb(235, 241, 252))
        ws.Row(3).Height = 14

        ' Cabeceras fila 4
        ' A, B = obligatorias (azul oscuro)
        ' C, D, E, F, G = referencia (gris oscuro)
        ' H, I, J = obligatorias (azul oscuro)
        ' K = opcional (azul medio)
        Dim hdr() As String = {
            "IdUbicacion (*)", "IdStock (*)",
            "Código", "Nombre Producto", "Lote", "Lic. Plate", "Disponible",
            "Tipo Ajuste (*)", "Motivo (*)", "Cantidad (*)", "Observación"
        }
        Dim anchos() As Integer = {14, 12, 18, 28, 14, 14, 12, 14, 10, 12, 28}
        Dim esObligatorio() As Boolean = {True, True, False, False, False, False, False, True, True, True, False}

        For i As Integer = 0 To 10
            ws.Cells(4, i + 1).Value = hdr(i)
            ws.Cells(4, i + 1).Style.Font.Bold = True
            ws.Cells(4, i + 1).Style.Font.Color.SetColor(Color.White)
            ws.Cells(4, i + 1).Style.Fill.PatternType = ExcelFillStyle.Solid
            If esObligatorio(i) Then
                ws.Cells(4, i + 1).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 94))
            Else
                ws.Cells(4, i + 1).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(100, 100, 110))
            End If
            ws.Cells(4, i + 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Column(i + 1).Width = anchos(i)
        Next
        ws.Row(4).Height = 20

        ' Ejemplo fila 5 (gris, itálica)
        Dim ejV() As Object = {239, 7, "2032790000000", "BUTTERBALL Pavo Entero X Libra", "", "WE000000042", 1167.85, "POSITIVO", "ENT", 50, "Ajuste entrada recepción"}
        For c As Integer = 0 To 10
            ws.Cells(5, c + 1).Value = ejV(c)
            ws.Cells(5, c + 1).Style.Font.Italic = True
            ws.Cells(5, c + 1).Style.Font.Color.SetColor(Color.Gray)
            ws.Cells(5, c + 1).Style.Fill.PatternType = ExcelFillStyle.Solid
            ws.Cells(5, c + 1).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240))
        Next
        ws.Row(5).Height = 18

        ' Área de datos fila 6..205
        For r As Integer = 6 To 205
            For c As Integer = 1 To 11
                ws.Cells(r, c).Style.Border.BorderAround(ExcelBorderStyle.Hair, Color.FromArgb(190, 190, 190))
            Next
            ' Fondo alternado en columnas de referencia C-G
            ws.Cells(r, 3, r, 7).Style.Fill.PatternType = ExcelFillStyle.Solid
            ws.Cells(r, 3, r, 7).Style.Fill.BackgroundColor.SetColor(
                If(r Mod 2 = 0, Color.FromArgb(248, 248, 248), Color.FromArgb(255, 255, 255)))
        Next

        ' (Validaciones de datos del Excel se omiten por compatibilidad con la versión instalada de EPPlus.
        '  La validación real se hace en código al cargar el Excel.)

        ws.View.FreezePanes(5, 1)
    End Sub

    Private Sub Crear_Hoja_Lote(ws As ExcelWorksheet)
        ' ── Columnas:
        ' A=IdUbicacion(*) B=IdStock(*) C=Código(ref) D=Nombre(ref)
        ' E=LoteAnterior(ref) F=LoteNuevo(*) G=Motivo(*) H=Observación
        ' Fila 4=cabecera, fila 5=ejemplo, datos desde fila 6

        ws.Cells("A1:H1").Merge = True
        ws.Cells("A1").Value = "AJUSTE DE LOTE — CAMBIO DE NÚMERO DE LOTE"
        ws.Cells("A1").Style.Font.Bold = True : ws.Cells("A1").Style.Font.Size = 13
        ws.Cells("A1").Style.Font.Color.SetColor(Color.White)
        ws.Cells("A1").Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells("A1").Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 112, 60))
        ws.Cells("A1").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        ws.Row(1).Height = 24

        ws.Cells("A2:H2").Merge = True
        ws.Cells("A2").Value = "Ingrese IdUbicacion, IdStock y Lote Nuevo (*). " &
                               "Las columnas C-E son referencia visual. Datos desde fila 6."
        ws.Cells("A2").Style.Font.Italic = True
        ws.Cells("A2").Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells("A2").Style.Fill.BackgroundColor.SetColor(Color.FromArgb(215, 240, 225))

        ws.Cells("A3:H3").Merge = True
        ws.Cells("A3").Value = "▐  Fondo verde = obligatorio  |  Fondo gris = solo referencia (no editar)"
        ws.Cells("A3").Style.Font.Italic = True : ws.Cells("A3").Style.Font.Size = 8
        ws.Cells("A3").Style.Fill.PatternType = ExcelFillStyle.Solid
        ws.Cells("A3").Style.Fill.BackgroundColor.SetColor(Color.FromArgb(235, 250, 241))
        ws.Row(3).Height = 14

        Dim hdr() As String = {"IdUbicacion (*)", "IdStock (*)", "Código", "Nombre Producto", "Lote Anterior", "Lote Nuevo (*)", "Motivo (*)", "Observación"}
        Dim anchos() As Integer = {14, 12, 18, 28, 18, 18, 10, 28}
        Dim esObl() As Boolean = {True, True, False, False, False, True, True, False}

        For i As Integer = 0 To 7
            ws.Cells(4, i + 1).Value = hdr(i)
            ws.Cells(4, i + 1).Style.Font.Bold = True
            ws.Cells(4, i + 1).Style.Font.Color.SetColor(Color.White)
            ws.Cells(4, i + 1).Style.Fill.PatternType = ExcelFillStyle.Solid
            ws.Cells(4, i + 1).Style.Fill.BackgroundColor.SetColor(
                If(esObl(i), Color.FromArgb(0, 80, 40), Color.FromArgb(80, 100, 90)))
            ws.Cells(4, i + 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            ws.Column(i + 1).Width = anchos(i)
        Next
        ws.Row(4).Height = 20

        ' Ejemplo fila 5
        Dim ejL() As Object = {239, 7, "2032790000000", "BUTTERBALL Pavo Entero X Libra", "WE000000042", "WE000000042-R", "CAM", "Corrección lote por re-etiquetado"}
        For c As Integer = 0 To 7
            ws.Cells(5, c + 1).Value = ejL(c)
            ws.Cells(5, c + 1).Style.Font.Italic = True
            ws.Cells(5, c + 1).Style.Font.Color.SetColor(Color.Gray)
            ws.Cells(5, c + 1).Style.Fill.PatternType = ExcelFillStyle.Solid
            ws.Cells(5, c + 1).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240))
        Next
        ws.Row(5).Height = 18

        ' Área de datos fila 6..205
        For r As Integer = 6 To 205
            For c As Integer = 1 To 8
                ws.Cells(r, c).Style.Border.BorderAround(ExcelBorderStyle.Hair, Color.FromArgb(190, 190, 190))
            Next
            ws.Cells(r, 3, r, 5).Style.Fill.PatternType = ExcelFillStyle.Solid
            ws.Cells(r, 3, r, 5).Style.Fill.BackgroundColor.SetColor(
                If(r Mod 2 = 0, Color.FromArgb(248, 248, 248), Color.FromArgb(255, 255, 255)))
        Next

        ' (Validaciones de datos del Excel se omiten por compatibilidad con la versión instalada de EPPlus.
        '  La validación real se hace en código al cargar el Excel.)

        ws.View.FreezePanes(5, 1)
    End Sub

    Private Sub Crear_Hoja_Instrucciones(ws As ExcelWorksheet)
        ws.Column(1).Width = 22 : ws.Column(2).Width = 55
        ws.Cells("A1:B1").Merge = True
        ws.Cells("A1").Value = "INSTRUCCIONES Y CATÁLOGO DE MOTIVOS"
        ws.Cells("A1").Style.Font.Bold = True : ws.Cells("A1").Style.Font.Size = 13
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
            If lineas(i).StartsWith("REGLAS") OrElse lineas(i).StartsWith("CATÁLOGO") OrElse lineas(i).StartsWith("ERRORES") Then
                ws.Cells(r, 1).Style.Font.Bold = True
                ws.Cells(r, 1).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(r, 1).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(220, 230, 241))
            End If
        Next
    End Sub

    Private Function CeldaStr(ws As ExcelWorksheet, fila As Integer, col As Integer) As String
        Dim v As Object = ws.Cells(fila, col).Value
        Return If(v Is Nothing, "", v.ToString().Trim())
    End Function

    Private Function CeldaInt(ws As ExcelWorksheet, fila As Integer, col As Integer) As Integer
        Dim v As Object = ws.Cells(fila, col).Value
        If v Is Nothing Then Return 0
        Dim result As Integer = 0
        Integer.TryParse(v.ToString().Trim(), result)
        Return result
    End Function

    Private Sub Actualizar_Titulo()
        Dim tipoTexto As String = ""
        Select Case _idTipoAjuste
            Case 1 : tipoTexto = "Lote"
            Case 3 : tipoTexto = "Positivo"
            Case 5 : tipoTexto = "Negativo"
            Case Else : tipoTexto = "Mixto (positivo/negativo/lote)"
        End Select
        lblTitulo.Text = "Importar Ajuste desde Excel  —  Tipo: " & tipoTexto
    End Sub

    Private Sub Actualizar_Status(msg As String, color As Color)
        barStatusInfo.Caption = msg
        barStatusInfo.ItemAppearance.Normal.ForeColor = color
        barStatusInfo.ItemAppearance.Normal.Options.UseForeColor = True

        ' DevExpress no refresca el bar item dentro de un Sub síncrono. Forzamos.
        Try
            If barStatusInfo.Manager IsNot Nothing Then
                barStatusInfo.Manager.LayoutChanged()
            End If
        Catch
        End Try
        Try
            Application.DoEvents()
        Catch
        End Try

        ' Espejo en el diagnóstico para que quede registro auditable.
        _diagnostico.AppendLine("[" & Now.ToString("HH:mm:ss") & "] STATUS: " & msg)
    End Sub

    ''' <summary>Cuenta cuántas filas a partir de la fila 6 tienen IdUbicacion (col A) numérico no cero.
    ''' Útil para detectar el caso "el usuario puso datos en la hoja equivocada".</summary>
    Private Function Contar_Filas_Con_Datos(ws As ExcelWorksheet) As Integer
        If ws Is Nothing OrElse ws.Dimension Is Nothing Then Return 0
        Dim filaFin As Integer = ws.Dimension.End.Row
        If filaFin < 6 Then Return 0
        Dim n As Integer = 0
        For r As Integer = 6 To filaFin
            If CeldaInt(ws, r, 1) <> 0 Then n += 1
        Next
        Return n
    End Function

    ''' <summary>Vuelca el contenido de _diagnostico a un archivo .log al lado del .xlsx.
    ''' No bloquea la operación si el disco no permite escribir.</summary>
    Private Function Guardar_Log_Diagnostico() As String
        Try
            If String.IsNullOrWhiteSpace(_rutaArchivo) Then Return ""
            Dim dir As String = Path.GetDirectoryName(_rutaArchivo)
            Dim baseName As String = Path.GetFileNameWithoutExtension(_rutaArchivo)
            Dim stamp As String = Now.ToString("yyyyMMdd_HHmmss")
            Dim logPath As String = Path.Combine(dir, baseName & "_import_" & stamp & ".log")
            File.WriteAllText(logPath, _diagnostico.ToString(), Encoding.UTF8)
            Return logPath
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>Muestra el diagnóstico completo en un MessageBox modal con monoespaciado y scroll.
    ''' Erik puede copiar/pegar para soporte sin tener que abrir el .log.</summary>
    Private Sub Mostrar_Diagnostico_Modal(titulo As String, icono As MessageBoxIcon)
        Dim cuerpo As String = _diagnostico.ToString()
        ' XtraMessageBox tiene scroll nativo; no recortamos pero limitamos a 8000 chars
        ' para que no explote si una iteración loguea miles de filas.
        If cuerpo.Length > 8000 Then
            cuerpo = cuerpo.Substring(0, 8000) &
                     vbCrLf & vbCrLf &
                     "(... truncado a 8000 caracteres. El log completo está en el archivo .log " &
                     "generado al lado del Excel.)"
        End If
        XtraMessageBox.Show(cuerpo, titulo, MessageBoxButtons.OK, icono)
    End Sub

    ' BOTONES: Imprimir Vista Previa / Imprimir Errores (DevExpress nativo)
    Private Sub btnImprimirDatos_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnImprimirDatos.ItemClick
        Imprimir_Grid(gridPreview, "Vista Previa de Ajustes",
                      "Importación de Ajuste de Stock — Tipo: " & Obtener_Tipo_Texto(),
                      Color.FromArgb(31, 73, 125))
    End Sub

    Private Sub btnImprimirErrores_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnImprimirErrores.ItemClick
        Imprimir_Grid(gridErrores, "Errores de Validación",
                      "Errores de Validación — Tipo: " & Obtener_Tipo_Texto(),
                      Color.FromArgb(150, 40, 30))
    End Sub

    Private Function Obtener_Tipo_Texto() As String
        Select Case _idTipoAjuste
            Case 3 : Return "Positivo"
            Case 5 : Return "Negativo"
            Case 1 : Return "Cambio de Lote"
            Case Else : Return "—"
        End Select
    End Function

    ''' <summary>
    ''' Lanza el Print Preview nativo del GridControl con cabecera y pie corporativos.
    ''' </summary>
    Private Sub Imprimir_Grid(grid As GridControl, titulo As String,
                              encabezado As String, colorTitulo As Color)
        If grid Is Nothing OrElse grid.MainView Is Nothing Then Return

        Try
            Dim ps As New DevExpress.XtraPrinting.PrintingSystem()
            Dim link As New PrintableComponentLink(ps)
            link.Component = grid

            ' Página y orientación
            link.Landscape = True
            link.Margins = New System.Drawing.Printing.Margins(40, 40, 60, 60)
            link.PaperKind = System.Drawing.Printing.PaperKind.Letter

            ' Encabezado (título + tipo + fecha)
            AddHandler link.CreateMarginalHeaderArea,
                Sub(s, ev)
                    Dim pi As New PageInfoBrick() With {
                        .PageInfo = PageInfo.NumberOfTotal,
                        .Format = "Página {0} de {1}",
                        .LineAlignment = BrickAlignment.Far,
                        .Alignment = BrickAlignment.Far}

                    Dim tBrick As New TextBrick() With {
                        .Text = encabezado,
                        .BorderWidth = 0
                    }
                    tBrick.Font = New Font("Segoe UI", 14, FontStyle.Bold)
                    tBrick.ForeColor = colorTitulo
                    tBrick.Rect = New RectangleF(0, 0, ev.Graph.ClientPageSize.Width, 28)

                    Dim sBrick As New TextBrick() With {
                        .Text = "Fecha de impresión: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm") &
                                "    |    Bodega: " & _idBodega,
                        .BorderWidth = 0
                    }
                    sBrick.Font = New Font("Segoe UI", 8.5F, FontStyle.Italic)
                    sBrick.ForeColor = Color.FromArgb(80, 80, 80)
                    sBrick.Rect = New RectangleF(0, 30, ev.Graph.ClientPageSize.Width, 16)

                    ev.Graph.DrawBrick(tBrick)
                    ev.Graph.DrawBrick(sBrick)
                    ev.Graph.DrawBrick(pi)
                End Sub

            ' Pie con totales
            AddHandler link.CreateMarginalFooterArea,
                Sub(s, ev)
                    Dim resumen As String
                    If ReferenceEquals(grid, gridPreview) Then
                        Dim ok = _filasValidadas.Where(Function(f) f.Valida).Count()
                        Dim er = _filasValidadas.Where(Function(f) Not f.Valida).Count()
                        resumen = "Total filas: " & _filasValidadas.Count &
                                  "    |    Válidas: " & ok &
                                  "    |    Con error: " & er
                    Else
                        Dim er = _filasValidadas.Where(Function(f) Not f.Valida).Count()
                        resumen = "Total errores: " & er
                    End If

                    Dim fb As New TextBrick() With {
                        .Text = resumen,
                        .BorderWidth = 0
                    }
                    fb.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    fb.ForeColor = Color.FromArgb(60, 60, 60)
                    fb.Rect = New RectangleF(0, 0, ev.Graph.ClientPageSize.Width, 18)
                    ev.Graph.DrawBrick(fb)
                End Sub

            ' Mostrar Print Preview con Ribbon (consistente con el resto del sistema)
            link.ShowRibbonPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

        Catch ex As Exception
            XtraMessageBox.Show("No se pudo imprimir: " & ex.Message,
                                 "Error de impresión",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' BOTÓN: Insertar motivos estándar que no existan en la BD
    Private Sub btnInsertarMotivos_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnInsertarMotivos.ItemClick
        Try
            Dim motivosEstandar As String() = {
                "ENTRADA", "SALIDA", "INVENTARIO",
                "MERMA", "DEVOLUCION", "CAMBIO", "OTRO"
            }

            Dim insertados As New List(Of String)
            Dim yaExistian As New List(Of String)

            Dim usuarioActual As String = If(AP.UsuarioAp IsNot Nothing AndAlso
                                              Not String.IsNullOrWhiteSpace(AP.UsuarioAp.Nombres),
                                              AP.UsuarioAp.Nombres, "SISTEMA")

            For Each codigo As String In motivosEstandar
                Dim id As Integer = clsLnAjuste_motivo.Get_IdMotivo_By_Codigo(codigo)
                If id > 0 Then
                    yaExistian.Add(codigo)
                Else
                    Dim nuevoId As Integer = clsLnAjuste_motivo.Insertar_Motivo_Si_No_Existe(codigo, usuarioActual)
                    If nuevoId > 0 Then insertados.Add(codigo & " (Id=" & nuevoId & ")")
                End If
            Next

            Dim msg As String = ""
            If insertados.Count > 0 Then
                msg &= "Motivos insertados (" & insertados.Count & "):" & vbCrLf &
                       "  • " & String.Join(vbCrLf & "  • ", insertados) & vbCrLf & vbCrLf
            End If
            If yaExistian.Count > 0 Then
                msg &= "Ya existían (" & yaExistian.Count & "):" & vbCrLf &
                       "  • " & String.Join(", ", yaExistian)
            End If
            If String.IsNullOrWhiteSpace(msg) Then msg = "No se realizaron cambios."

            MessageBox.Show(msg, "Resultado de inserción de motivos",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error al insertar motivos: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class