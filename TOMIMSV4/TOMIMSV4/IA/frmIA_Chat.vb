Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks
Imports DevExpress.XtraBars.Ribbon
Imports Newtonsoft.Json

Public Class frmIA_Chat

    ' Ribbon principal
    'Private WithEvents Ribbon As DevExpress.XtraBars.Ribbon.RibbonControl
    Private WithEvents pageIA As DevExpress.XtraBars.Ribbon.RibbonPage
    Private WithEvents grpPlantilla As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Private WithEvents grpParametros As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Private WithEvents grpEjecucion As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Private WithEvents grpAyuda As DevExpress.XtraBars.Ribbon.RibbonPageGroup

    ' Items
    Private barTemplates As DevExpress.XtraBars.BarEditItem
    Private repoTemplates As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit

    Private barRag As DevExpress.XtraBars.BarEditItem
    Private repoRag As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit

    Private barTemp As DevExpress.XtraBars.BarEditItem
    Private repoTemp As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit

    Private barMaxTokens As DevExpress.XtraBars.BarEditItem
    Private repoMaxTokens As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit

    Private barBtnSend As DevExpress.XtraBars.BarButtonItem
    Private barBtnStream As DevExpress.XtraBars.BarButtonItem
    Private barBtnClear As DevExpress.XtraBars.BarButtonItem
    Private barBtnReloadPrompts As DevExpress.XtraBars.BarButtonItem
    Private barBtnAbout As DevExpress.XtraBars.BarButtonItem




    ' Status bar
    Private barStatusModel As DevExpress.XtraBars.BarStaticItem
    Private barStatusLatency As DevExpress.XtraBars.BarStaticItem
    Private barStatusTokens As DevExpress.XtraBars.BarStaticItem


    Private dock As DevExpress.XtraBars.Docking.DockManager
    Private dockVars As DevExpress.XtraBars.Docking.DockPanel
    Private dockPreview As DevExpress.XtraBars.Docking.DockPanel

    Private pgVars As DevExpress.XtraVerticalGrid.PropertyGridControl
    Private memoPreview As DevExpress.XtraEditors.MemoEdit
    Private memoUser As DevExpress.XtraEditors.MemoEdit
    Private memoChat As DevExpress.XtraEditors.MemoEdit

    Private split As DevExpress.XtraEditors.SplitContainerControl


    ' Campos para estado
    Private _templates As List(Of PromptTemplate)
    Private _vars As New Dictionary(Of String, String)

    Private Sub InitDockAndEditors()
        dock = New DevExpress.XtraBars.Docking.DockManager(Me)

        ' Dock left: variables
        dockVars = dock.AddPanel(DevExpress.XtraBars.Docking.DockingStyle.Left)
        dockVars.Text = "Variables de plantilla"
        pgVars = New DevExpress.XtraVerticalGrid.PropertyGridControl() With {.Dock = DockStyle.Fill}

        AddHandler pgVars.CellValueChanged, AddressOf PgVars_CellValueChanged

        dockVars.ControlContainer.Controls.Add(pgVars)
        'dockVars.OriginalSize = New Drawing.Size(260, 200)

        ' Dock right: preview
        dockPreview = dock.AddPanel(DevExpress.XtraBars.Docking.DockingStyle.Right)
        dockPreview.Text = "Vista previa (system prompt)"
        memoPreview = New DevExpress.XtraEditors.MemoEdit() With {.Dock = DockStyle.Fill}
        dockPreview.ControlContainer.Controls.Add(memoPreview)
        'dockPreview.OriginalSize = New Drawing.Size(340, 200)

        ' Centro: user + chat
        memoUser = New DevExpress.XtraEditors.MemoEdit() With {.Dock = DockStyle.Top, .Height = 110}
        memoChat = New DevExpress.XtraEditors.MemoEdit() With {.Dock = DockStyle.Fill}
        Me.Controls.Add(memoChat)
        Me.Controls.Add(memoUser)
    End Sub


    Private Sub InitRibbon()
        Ribbon = New DevExpress.XtraBars.Ribbon.RibbonControl()

        ' Repositorios
        repoTemplates = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit() With {
        .DisplayMember = "name",
        .ValueMember = "id",
        .NullText = "[Seleccionar plantilla...]",
        .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
    }
        repoRag = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        repoTemp = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit() With {
        .IsFloatValue = True, .Increment = 0.1D, .MaxValue = 1D, .MinValue = 0D
    }
        repoMaxTokens = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit() With {
        .IsFloatValue = False, .Increment = 128D, .MaxValue = 8192D, .MinValue = 128D
    }

        Ribbon.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {
        repoTemplates, repoRag, repoTemp, repoMaxTokens
    })

        ' BarEditItems
        barTemplates = New DevExpress.XtraBars.BarEditItem() With {.Caption = "Plantilla", .Edit = repoTemplates, .Width = 220}
        barRag = New DevExpress.XtraBars.BarEditItem() With {.Caption = "RAG", .Edit = repoRag}
        barTemp = New DevExpress.XtraBars.BarEditItem() With {.Caption = "Temp", .Edit = repoTemp, .EditValue = 0.3D, .Width = 80}
        barMaxTokens = New DevExpress.XtraBars.BarEditItem() With {.Caption = "MaxTokens", .Edit = repoMaxTokens, .EditValue = 1024, .Width = 100}

        ' Botones
        barBtnSend = New DevExpress.XtraBars.BarButtonItem() With {.Caption = "Enviar", .Hint = "Enviar (no-stream)"}
        barBtnStream = New DevExpress.XtraBars.BarButtonItem() With {.Caption = "Transmitir", .Hint = "Streaming SSE"}
        barBtnClear = New DevExpress.XtraBars.BarButtonItem() With {.Caption = "Limpiar"}
        barBtnReloadPrompts = New DevExpress.XtraBars.BarButtonItem() With {.Caption = "Recargar"}
        barBtnAbout = New DevExpress.XtraBars.BarButtonItem() With {.Caption = "Acerca de"}

        ' Status
        barStatusModel = New DevExpress.XtraBars.BarStaticItem() With {.Caption = "Modelo: -"}
        barStatusLatency = New DevExpress.XtraBars.BarStaticItem() With {.Caption = "Latencia: -"}
        barStatusTokens = New DevExpress.XtraBars.BarStaticItem() With {.Caption = "Tokens: -"}

        ' Páginas y grupos
        pageIA = New DevExpress.XtraBars.Ribbon.RibbonPage("IA")
        grpPlantilla = New DevExpress.XtraBars.Ribbon.RibbonPageGroup("Plantilla")
        grpParametros = New DevExpress.XtraBars.Ribbon.RibbonPageGroup("Parámetros")
        grpEjecucion = New DevExpress.XtraBars.Ribbon.RibbonPageGroup("Ejecución")
        grpAyuda = New DevExpress.XtraBars.Ribbon.RibbonPageGroup("Ayuda")

        grpPlantilla.ItemLinks.Add(barTemplates)
        grpPlantilla.ItemLinks.Add(barBtnReloadPrompts)

        grpParametros.ItemLinks.Add(barRag)
        grpParametros.ItemLinks.Add(barTemp)
        grpParametros.ItemLinks.Add(barMaxTokens)

        grpEjecucion.ItemLinks.Add(barBtnSend)
        grpEjecucion.ItemLinks.Add(barBtnStream)
        grpEjecucion.ItemLinks.Add(barBtnClear)

        grpAyuda.ItemLinks.Add(barBtnAbout)

        pageIA.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {
        grpPlantilla, grpParametros, grpEjecucion, grpAyuda
    })

        Ribbon.Pages.Add(pageIA)
        Ribbon.StatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        StatusBar.Ribbon = Me.Ribbon
        StatusBar.Dock = DockStyle.Bottom
        Ribbon.StatusBar.ItemLinks.Add(barStatusModel)
        Ribbon.StatusBar.ItemLinks.Add(barStatusLatency)
        Ribbon.StatusBar.ItemLinks.Add(barStatusTokens)

        Me.Ribbon.SelectedPage = pageIA
        Me.Ribbon.BringToFront()

        Me.Ribbon = Ribbon
        Me.Controls.Add(Ribbon)
        Me.Controls.Add(Ribbon.StatusBar)
    End Sub

    Private Sub InitCenterArea()
        split = New DevExpress.XtraEditors.SplitContainerControl() With {
        .Dock = DockStyle.Fill,
        .Horizontal = False,     ' panel1 arriba, panel2 abajo
        .SplitterPosition = 120  ' alto del input
    }

        memoUser = New DevExpress.XtraEditors.MemoEdit() With {
        .Dock = DockStyle.Fill
    }
        memoUser.Properties.NullValuePrompt = "Escribe tu consulta aquí..."
        memoUser.Properties.NullValuePromptShowForEmptyValue = True

        memoChat = New DevExpress.XtraEditors.MemoEdit() With {
        .Dock = DockStyle.Fill
    }
        memoChat.Properties.ReadOnly = True

        split.Panel1.Controls.Add(memoUser)
        split.Panel2.Controls.Add(memoChat)

        ' IMPORTANTE: agrega el centro ANTES que los dock-panels, o haz BringToFront()
        Me.Controls.Add(split)
        split.BringToFront()
    End Sub

    Private Sub WireEvents()
        ' Ribbon buttons
        AddHandler barBtnReloadPrompts.ItemClick, AddressOf BarBtnReloadPrompts_ItemClick
        AddHandler barBtnSend.ItemClick, AddressOf BarBtnSend_ItemClick
        AddHandler barBtnStream.ItemClick, AddressOf BarBtnStream_ItemClick
        AddHandler barBtnClear.ItemClick, AddressOf BarBtnClear_ItemClick
        AddHandler barBtnAbout.ItemClick, Sub() DevExpress.XtraEditors.XtraMessageBox.Show("IA WMS", "Acerca de")
        AddHandler pgVars.CellValueChanged, AddressOf PgVars_CellValueChanged

    End Sub


    Private Sub frmIA_Chat_Load(sender As Object, e As EventArgs) Handles Me.Load
        InitRibbon()
        InitDockAndEditors()
        InitCenterArea()
        WireEvents()
        LoadPromptsIntoUI()

        split.BringToFront()
        Me.Ribbon.BringToFront()

    End Sub

    Private Async Sub BarBtnSend_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim tpl = GetSelectedTemplate()
        If tpl Is Nothing Then Return
        Dim systemText = PromptEngine.Render(tpl, New Dictionary(Of String, String)(_vars))
        Dim userText = memoUser.Text

        Dim messages = New List(Of ChatMsg) From {
        New ChatMsg With {.role = "system", .content = systemText},
        New ChatMsg With {.role = "user", .content = userText}
    }

        Dim baseUrl = "https://localhost:7181/"
        Dim client As New AiClient(baseUrl)

        Dim sw = Diagnostics.Stopwatch.StartNew()
        Try
            memoChat.Text &= $"{Environment.NewLine}[user] {userText}{Environment.NewLine}[assistant] "
            Dim ragEnabled As Boolean = Convert.ToBoolean(barRag.EditValue)
            Dim response = Await client.ChatAsync(messages, ragEnabled)
            sw.Stop()
            memoChat.Text &= response & Environment.NewLine

            barStatusLatency.Caption = $"Latencia: {sw.ElapsedMilliseconds} ms"
            barStatusModel.Caption = "Modelo: local"
            barStatusTokens.Caption = "Tokens: (estimado)"
        Catch ex As Exception
            sw.Stop()
            memoChat.Text &= $"{Environment.NewLine}[error] {ex.Message}"
        End Try
    End Sub


    Private Async Sub BarBtnStream_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim tpl = GetSelectedTemplate()
        If tpl Is Nothing Then Return

        Dim systemText = PromptEngine.Render(tpl, New Dictionary(Of String, String)(_vars))
        Dim userText = memoUser.Text

        memoChat.Text &= vbCrLf & "[user] " & userText & vbCrLf & "[assistant] "

        Dim baseUrl = "http://localhost:7181"

        ' payload anónimo
        Dim payload = New With {
        .messages = New Object() {
            New With {.role = "system", .content = systemText},
            New With {.role = "user", .content = userText}
        },
        .stream = True,
        .temperature = Convert.ToDouble(barTemp.EditValue),
        .maxTokens = Convert.ToInt32(barMaxTokens.EditValue),
        .rag = If(Convert.ToBoolean(barRag.EditValue),
                  New With {.enabled = True, .topK = 4, .collections = New String() {"manuales"}},
                  Nothing)
    }

        Dim progress = New Progress(Of String)(
        Sub(delta) memoChat.Text &= delta
    )

        Dim sw = Diagnostics.Stopwatch.StartNew()
        Try
            ' 👇👇 AQUI: PostSseAsync **devuelve Task** y se puede Await
            Await PostSseAsync(baseUrl & "/api/IA/ollama", payload, progress, Threading.CancellationToken.None)
            sw.Stop()
            barStatusLatency.Caption = $"Latencia: {sw.ElapsedMilliseconds} ms"
            barStatusModel.Caption = "Modelo: local"
        Catch ex As Exception
            sw.Stop()
            memoChat.Text &= vbCrLf & "[error] " & ex.Message
        End Try
    End Sub

    Private Async Function PostSseAsync(url As String,
                                    payload As Object,
                                    progress As IProgress(Of String),
                                    ct As Threading.CancellationToken) As Task
        ' Serializa con Newtonsoft para Framework
        Dim json As String = JsonConvert.SerializeObject(payload)
        Using http As New HttpClient()
            Dim req = New HttpRequestMessage(HttpMethod.Post, url) With {
            .Content = New StringContent(json, Encoding.UTF8, "application/json")
        }

            Using resp = Await http.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct)
                resp.EnsureSuccessStatusCode()

                Using s = Await resp.Content.ReadAsStreamAsync()
                    Using r As New IO.StreamReader(s)
                        While Not r.EndOfStream
                            If ct.IsCancellationRequested Then ct.ThrowIfCancellationRequested()

                            Dim line = Await r.ReadLineAsync()
                            If String.IsNullOrWhiteSpace(line) Then Continue While

                            ' Espera líneas tipo: "data: {""delta"":""...""}"
                            If line.StartsWith("data:") Then
                                Dim jsonLine = line.Substring(5).Trim()
                                Dim delta = ExtractDelta(jsonLine) ' ver función abajo
                                If delta IsNot Nothing Then progress.Report(delta)
                            End If
                        End While
                    End Using
                End Using
            End Using
        End Using
    End Function


    Private Function ExtractDelta(json As String) As String
        Dim key = """delta"":"
        Dim idx = json.IndexOf(key, StringComparison.OrdinalIgnoreCase)
        If idx < 0 Then Return Nothing
        Dim startQ = json.IndexOf(""""c, idx + key.Length)
        Dim endQ = json.IndexOf(""""c, startQ + 1)
        If startQ >= 0 AndAlso endQ > startQ Then
            Return json.Substring(startQ + 1, endQ - startQ - 1)
        End If
        Return Nothing
    End Function

    Private Sub BarBtnClear_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        memoUser.EditValue = ""
        memoChat.EditValue = ""
    End Sub

    Private Sub BarBtnReloadPrompts_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        LoadPromptsIntoUI()
    End Sub


    Private Sub LoadPromptsIntoUI()
        ' Ruta dual: ProgramData -> local
        Dim chosen As String = Nothing
        Dim programDataPath = "C:\ProgramData\IAWMS\prompts.json"
        If IO.File.Exists(programDataPath) Then
            chosen = programDataPath
        Else
            Dim local = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IA", "prompts.json")
            If IO.File.Exists(local) Then chosen = local
        End If

        If String.IsNullOrEmpty(chosen) Then
            DevExpress.XtraEditors.XtraMessageBox.Show("No se encontró prompts.json", "Error")
            Return
        End If

        _templates = PromptEngine.LoadTemplates(chosen)
        repoTemplates.DataSource = _templates

        ' Selecciona la primera
        If _templates IsNot Nothing AndAlso _templates.Any() Then
            barTemplates.EditValue = _templates(0).id
            ApplyTemplate(_templates(0))
        End If
    End Sub

    Private Sub Templates_EditValueChanged(sender As Object, e As EventArgs)
        Dim id = TryCast(barTemplates.EditValue, String)
        Dim tpl = _templates?.FirstOrDefault(Function(t) t.id = id)
        If tpl IsNot Nothing Then ApplyTemplate(tpl)
    End Sub

    Private Sub ApplyTemplate(tpl As PromptTemplate)
        If tpl Is Nothing Then Return

        ' 1) Preparar diccionario _vars sin usar el operador ?. y cuidando nulls
        If tpl.variables IsNot Nothing AndAlso tpl.variables.Count > 0 Then
            _vars = tpl.variables.ToDictionary(
            Function(v) v.name,
            Function(v) If(v.defaultValue, String.Empty)
        )
        Else
            _vars = New Dictionary(Of String, String)()
        End If

        ' 2) Construir las filas del PropertyGridControl dinámicamente
        pgVars.BeginUpdate()
        Try
            pgVars.Rows.Clear()

            If tpl.variables IsNot Nothing Then
                For Each v In tpl.variables
                    Dim row As New DevExpress.XtraVerticalGrid.Rows.EditorRow(v.name)
                    row.Properties.Caption = v.name & If(v.required, " *", "")
                    row.Properties.FieldName = v.name
                    row.Properties.Value = If(_vars.ContainsKey(v.name), _vars(v.name), String.Empty)

                    ' (Opcional) tooltip con la descripción
                    'If Not String.IsNullOrEmpty(v.description) Then
                    '    row.tooltip = v.description
                    'End If

                    ' (Opcional) editores: si quieres forzar texto simple
                    Dim textRepo As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
                    row.Properties.RowEdit = textRepo

                    pgVars.Rows.Add(row)
                Next
            End If
        Finally
            pgVars.EndUpdate()
        End Try

        ' 3) Render del preview
        memoPreview.Text = PromptEngine.Render(tpl, New Dictionary(Of String, String)(_vars))
    End Sub

    Private Sub PgVars_CellValueChanged(sender As Object, e As DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs)
        Dim field As String = e.Row.Properties.FieldName
        Dim value As String = If(e.Value, String.Empty).ToString()

        If _vars.ContainsKey(field) Then
            _vars(field) = value
        Else
            _vars.Add(field, value)
        End If

        Dim tpl = GetSelectedTemplate()
        If tpl IsNot Nothing Then
            memoPreview.Text = PromptEngine.Render(tpl, New Dictionary(Of String, String)(_vars))
        End If
    End Sub


    Private Function GetSelectedTemplate() As PromptTemplate
        Dim id = TryCast(barTemplates.EditValue, String)
        Return _templates?.FirstOrDefault(Function(t) t.id = id)
    End Function

End Class