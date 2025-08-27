<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProceso
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProceso))
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        Me.grpScanPoliza = New DevExpress.XtraEditors.GroupControl()
        Me.txtScanPoliza = New System.Windows.Forms.TextBox()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.Dgrid = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RibbonControl1 = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuProceso2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProcesarPolizarImportadas = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProcesarPorSimilitud = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsociarPolizasExcelCEALSA = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEquipararNombres = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProcesarPolizasExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizarPorCoPoliza = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuInsertarPolizasNoExistentes = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdInsertaTablaRelacionada = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdInsertaPolizaTemporal = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdImportar_Polizas_Ilegibles = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar1 = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grpScanPoliza, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpScanPoliza.SuspendLayout()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prg.Location = New System.Drawing.Point(3, 391)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(691, 27)
        Me.prg.TabIndex = 2
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.95362!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.04638!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Dgrid, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 193)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 427.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1466, 427)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.prg)
        Me.GroupBox1.Controls.Add(Me.grpScanPoliza)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(697, 421)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblprg)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 122)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(691, 269)
        Me.GroupBox2.TabIndex = 27
        Me.GroupBox2.TabStop = False
        '
        'lblprg
        '
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblprg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.lblprg.Location = New System.Drawing.Point(3, 19)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.Size = New System.Drawing.Size(685, 247)
        Me.lblprg.TabIndex = 1
        Me.lblprg.Text = ""
        '
        'grpScanPoliza
        '
        Me.grpScanPoliza.CaptionImageOptions.Image = CType(resources.GetObject("grpScanPoliza.CaptionImageOptions.Image"), System.Drawing.Image)
        Me.grpScanPoliza.Controls.Add(Me.txtScanPoliza)
        Me.grpScanPoliza.Controls.Add(Me.LabelControl4)
        Me.grpScanPoliza.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpScanPoliza.Location = New System.Drawing.Point(3, 19)
        Me.grpScanPoliza.Margin = New System.Windows.Forms.Padding(5, 8, 5, 8)
        Me.grpScanPoliza.Name = "grpScanPoliza"
        Me.grpScanPoliza.Size = New System.Drawing.Size(691, 103)
        Me.grpScanPoliza.TabIndex = 26
        Me.grpScanPoliza.Text = "Escanéo de Poliza"
        '
        'txtScanPoliza
        '
        Me.txtScanPoliza.BackColor = System.Drawing.Color.LightGray
        Me.txtScanPoliza.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtScanPoliza.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScanPoliza.Location = New System.Drawing.Point(2, 67)
        Me.txtScanPoliza.Name = "txtScanPoliza"
        Me.txtScanPoliza.Size = New System.Drawing.Size(687, 28)
        Me.txtScanPoliza.TabIndex = 1
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl4.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelControl4.Location = New System.Drawing.Point(2, 33)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(5, 8, 5, 8)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(687, 34)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Escanée Poliza:"
        '
        'Dgrid
        '
        Me.Dgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgrid.Location = New System.Drawing.Point(706, 3)
        Me.Dgrid.MainView = Me.GridView1
        Me.Dgrid.Name = "Dgrid"
        Me.Dgrid.Size = New System.Drawing.Size(757, 421)
        Me.Dgrid.TabIndex = 4
        Me.Dgrid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.Dgrid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 700
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFooter = True
        '
        'RibbonControl1
        '
        Me.RibbonControl1.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(26, 30, 26, 30)
        Me.RibbonControl1.ExpandCollapseItem.Id = 0
        Me.RibbonControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl1.ExpandCollapseItem, Me.mnuProceso2, Me.mnuProcesarPolizarImportadas, Me.mnuProcesarPorSimilitud, Me.mnuAsociarPolizasExcelCEALSA, Me.mnuEquipararNombres, Me.mnuProcesarPolizasExcel, Me.mnuActualizarPorCoPoliza, Me.mnuInsertarPolizasNoExistentes, Me.cmdInsertaTablaRelacionada, Me.cmdInsertaPolizaTemporal, Me.cmdImportar_Polizas_Ilegibles})
        Me.RibbonControl1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl1.MaxItemId = 14
        Me.RibbonControl1.Name = "RibbonControl1"
        Me.RibbonControl1.OptionsMenuMinWidth = 289
        Me.RibbonControl1.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl1.Size = New System.Drawing.Size(1466, 193)
        Me.RibbonControl1.StatusBar = Me.RibbonStatusBar1
        '
        'mnuProceso2
        '
        Me.mnuProceso2.Caption = "Asociar polizas por copoliza (3)"
        Me.mnuProceso2.Id = 2
        Me.mnuProceso2.ImageOptions.SvgImage = CType(resources.GetObject("mnuProceso2.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuProceso2.Name = "mnuProceso2"
        '
        'mnuProcesarPolizarImportadas
        '
        Me.mnuProcesarPolizarImportadas.Caption = "Procesar polizas importadas"
        Me.mnuProcesarPolizarImportadas.Id = 3
        Me.mnuProcesarPolizarImportadas.Name = "mnuProcesarPolizarImportadas"
        '
        'mnuProcesarPorSimilitud
        '
        Me.mnuProcesarPorSimilitud.Caption = "Procesar por similitud de nombres (Beta)"
        Me.mnuProcesarPorSimilitud.Id = 4
        Me.mnuProcesarPorSimilitud.ImageOptions.SvgImage = CType(resources.GetObject("mnuProcesarPorSimilitud.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuProcesarPorSimilitud.Name = "mnuProcesarPorSimilitud"
        '
        'mnuAsociarPolizasExcelCEALSA
        '
        Me.mnuAsociarPolizasExcelCEALSA.Caption = "Asociar polizas excel CEALSA (2)"
        Me.mnuAsociarPolizasExcelCEALSA.Id = 5
        Me.mnuAsociarPolizasExcelCEALSA.ImageOptions.SvgImage = CType(resources.GetObject("mnuAsociarPolizasExcelCEALSA.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuAsociarPolizasExcelCEALSA.Name = "mnuAsociarPolizasExcelCEALSA"
        '
        'mnuEquipararNombres
        '
        Me.mnuEquipararNombres.Caption = "Equiparar nombres"
        Me.mnuEquipararNombres.Id = 6
        Me.mnuEquipararNombres.Name = "mnuEquipararNombres"
        '
        'mnuProcesarPolizasExcel
        '
        Me.mnuProcesarPolizasExcel.Caption = "Procesar polizas excel escaneadas (0)"
        Me.mnuProcesarPolizasExcel.Id = 7
        Me.mnuProcesarPolizasExcel.ImageOptions.SvgImage = CType(resources.GetObject("mnuProcesarPolizasExcel.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuProcesarPolizasExcel.Name = "mnuProcesarPolizasExcel"
        '
        'mnuActualizarPorCoPoliza
        '
        Me.mnuActualizarPorCoPoliza.Caption = "Actualizar por CoPoliza (4)"
        Me.mnuActualizarPorCoPoliza.Id = 9
        Me.mnuActualizarPorCoPoliza.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizarPorCoPoliza.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizarPorCoPoliza.Name = "mnuActualizarPorCoPoliza"
        '
        'mnuInsertarPolizasNoExistentes
        '
        Me.mnuInsertarPolizasNoExistentes.Caption = "Insertar polizas no existentes"
        Me.mnuInsertarPolizasNoExistentes.Id = 10
        Me.mnuInsertarPolizasNoExistentes.ImageOptions.SvgImage = CType(resources.GetObject("mnuInsertarPolizasNoExistentes.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuInsertarPolizasNoExistentes.Name = "mnuInsertarPolizasNoExistentes"
        '
        'cmdInsertaTablaRelacionada
        '
        Me.cmdInsertaTablaRelacionada.Caption = "Inserta tabla relacionada"
        Me.cmdInsertaTablaRelacionada.Id = 11
        Me.cmdInsertaTablaRelacionada.ImageOptions.Image = CType(resources.GetObject("cmdInsertaTablaRelacionada.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdInsertaTablaRelacionada.ImageOptions.LargeImage = CType(resources.GetObject("cmdInsertaTablaRelacionada.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdInsertaTablaRelacionada.Name = "cmdInsertaTablaRelacionada"
        '
        'cmdInsertaPolizaTemporal
        '
        Me.cmdInsertaPolizaTemporal.Caption = "Inserta poliza temporal"
        Me.cmdInsertaPolizaTemporal.Id = 12
        Me.cmdInsertaPolizaTemporal.ImageOptions.Image = CType(resources.GetObject("cmdInsertaPolizaTemporal.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdInsertaPolizaTemporal.ImageOptions.LargeImage = CType(resources.GetObject("cmdInsertaPolizaTemporal.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdInsertaPolizaTemporal.Name = "cmdInsertaPolizaTemporal"
        '
        'cmdImportar_Polizas_Ilegibles
        '
        Me.cmdImportar_Polizas_Ilegibles.Caption = "Importar polizas iIlegibles(1)"
        Me.cmdImportar_Polizas_Ilegibles.Id = 13
        Me.cmdImportar_Polizas_Ilegibles.ImageOptions.Image = CType(resources.GetObject("cmdImportar_Polizas_Ilegibles.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdImportar_Polizas_Ilegibles.ImageOptions.LargeImage = CType(resources.GetObject("cmdImportar_Polizas_Ilegibles.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdImportar_Polizas_Ilegibles.Name = "cmdImportar_Polizas_Ilegibles"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menú Fix the process"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuProcesarPolizasExcel)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdImportar_Polizas_Ilegibles)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuAsociarPolizasExcelCEALSA)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuProceso2)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizarPorCoPoliza)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuProcesarPorSimilitud)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuInsertarPolizasNoExistentes)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdInsertaTablaRelacionada)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdInsertaPolizaTemporal)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar1
        '
        Me.RibbonStatusBar1.Location = New System.Drawing.Point(0, 620)
        Me.RibbonStatusBar1.Name = "RibbonStatusBar1"
        Me.RibbonStatusBar1.Ribbon = Me.RibbonControl1
        Me.RibbonStatusBar1.Size = New System.Drawing.Size(1466, 30)
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "RibbonPage2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "BarButtonItem1"
        Me.BarButtonItem3.Id = 11
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'frmProceso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1466, 650)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.RibbonStatusBar1)
        Me.Controls.Add(Me.RibbonControl1)
        Me.Name = "frmProceso"
        Me.Ribbon = Me.RibbonControl1
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar1
        Me.Text = "Proceso de asociación de pólizas desfasadas"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grpScanPoliza, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpScanPoliza.ResumeLayout(False)
        Me.grpScanPoliza.PerformLayout()
        CType(Me.Dgrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents prg As Windows.Forms.ProgressBar
    Friend WithEvents TableLayoutPanel1 As Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents Dgrid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grpScanPoliza As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtScanPoliza As Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents lblprg As Windows.Forms.RichTextBox
    Friend WithEvents RibbonControl1 As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar1 As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents cmdProceso2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProceso2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProcesarPolizarImportadas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProcesarPorSimilitud As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAsociarPolizasExcelCEALSA As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEquipararNombres As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProcesarPolizasExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizarPorCoPoliza As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuInsertarPolizasNoExistentes As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdInsertaTablaRelacionada As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdInsertaPolizaTemporal As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdImportar_Polizas_Ilegibles As DevExpress.XtraBars.BarButtonItem
End Class
