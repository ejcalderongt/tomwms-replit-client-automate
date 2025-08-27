<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEstructuraBod
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEstructuraBod))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuValidarEstructura = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuVerDiseño = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuGenerarEstructura = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuModificar = New DevExpress.XtraBars.BarSubItem()
        Me.mnuModificaSector = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuModificarTramo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuCopiarEstructura = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImportarEstructura = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup4 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.grpSector = New System.Windows.Forms.GroupBox()
        Me.cmbSector = New System.Windows.Forms.ComboBox()
        Me.lblSSector = New System.Windows.Forms.Label()
        Me.btnTramos = New DevExpress.XtraEditors.SimpleButton()
        Me.grpTramo = New System.Windows.Forms.GroupBox()
        Me.cmbTramo = New System.Windows.Forms.ComboBox()
        Me.lblTramoS = New System.Windows.Forms.Label()
        Me.btnUbic = New DevExpress.XtraEditors.SimpleButton()
        Me.grpCopiar = New System.Windows.Forms.GroupBox()
        Me.cboTramo2 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCopiar = New DevExpress.XtraEditors.SimpleButton()
        Me.grpArea = New System.Windows.Forms.GroupBox()
        Me.cmbArea = New System.Windows.Forms.ComboBox()
        Me.lblArea = New System.Windows.Forms.Label()
        Me.cmdModificarArea = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSector.SuspendLayout()
        Me.grpTramo.SuspendLayout()
        Me.grpCopiar.SuspendLayout()
        Me.grpArea.SuspendLayout()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuValidarEstructura, Me.mnuVerDiseño, Me.mnuGenerarEstructura, Me.mnuModificar, Me.mnuModificaSector, Me.mnuModificarTramo, Me.mnuCopiarEstructura, Me.mnuImportarEstructura})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 10
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1051, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuValidarEstructura
        '
        Me.mnuValidarEstructura.Caption = "Validar Estructura"
        Me.mnuValidarEstructura.Id = 1
        Me.mnuValidarEstructura.ImageOptions.SvgImage = CType(resources.GetObject("mnuValidarEstructura.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuValidarEstructura.Name = "mnuValidarEstructura"
        '
        'mnuVerDiseño
        '
        Me.mnuVerDiseño.Caption = "Ver diseño"
        Me.mnuVerDiseño.Id = 2
        Me.mnuVerDiseño.ImageOptions.SvgImage = CType(resources.GetObject("mnuVerDiseño.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuVerDiseño.Name = "mnuVerDiseño"
        '
        'mnuGenerarEstructura
        '
        Me.mnuGenerarEstructura.Caption = "Generar estructura"
        Me.mnuGenerarEstructura.Id = 3
        Me.mnuGenerarEstructura.ImageOptions.SvgImage = CType(resources.GetObject("mnuGenerarEstructura.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGenerarEstructura.Name = "mnuGenerarEstructura"
        '
        'mnuModificar
        '
        Me.mnuModificar.Caption = "Modificar"
        Me.mnuModificar.Id = 5
        Me.mnuModificar.ImageOptions.SvgImage = CType(resources.GetObject("mnuModificar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuModificar.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuModificaSector), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuModificarTramo)})
        Me.mnuModificar.Name = "mnuModificar"
        '
        'mnuModificaSector
        '
        Me.mnuModificaSector.Caption = "Sector"
        Me.mnuModificaSector.Id = 6
        Me.mnuModificaSector.Name = "mnuModificaSector"
        '
        'mnuModificarTramo
        '
        Me.mnuModificarTramo.Caption = "Tramo"
        Me.mnuModificarTramo.Id = 7
        Me.mnuModificarTramo.Name = "mnuModificarTramo"
        '
        'mnuCopiarEstructura
        '
        Me.mnuCopiarEstructura.Caption = "Copiar estructura"
        Me.mnuCopiarEstructura.Id = 8
        Me.mnuCopiarEstructura.ImageOptions.SvgImage = CType(resources.GetObject("mnuCopiarEstructura.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuCopiarEstructura.Name = "mnuCopiarEstructura"
        '
        'mnuImportarEstructura
        '
        Me.mnuImportarEstructura.Caption = "Importar estructura"
        Me.mnuImportarEstructura.Id = 9
        Me.mnuImportarEstructura.ImageOptions.Image = CType(resources.GetObject("mnuImportarEstructura.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuImportarEstructura.ImageOptions.LargeImage = CType(resources.GetObject("mnuImportarEstructura.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuImportarEstructura.Name = "mnuImportarEstructura"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2, Me.RibbonPageGroup3, Me.RibbonPageGroup4})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Estructura de bodega"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGenerarEstructura)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuValidarEstructura)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuVerDiseño)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuModificar)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuCopiarEstructura)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'RibbonPageGroup4
        '
        Me.RibbonPageGroup4.ItemLinks.Add(Me.mnuImportarEstructura)
        Me.RibbonPageGroup4.Name = "RibbonPageGroup4"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 774)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1051, 30)
        '
        'grpSector
        '
        Me.grpSector.Controls.Add(Me.cmbSector)
        Me.grpSector.Controls.Add(Me.lblSSector)
        Me.grpSector.Controls.Add(Me.btnTramos)
        Me.grpSector.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpSector.Location = New System.Drawing.Point(0, 290)
        Me.grpSector.Name = "grpSector"
        Me.grpSector.Size = New System.Drawing.Size(1051, 134)
        Me.grpSector.TabIndex = 2
        Me.grpSector.TabStop = False
        Me.grpSector.Text = "Creación y configuración de tramos "
        '
        'cmbSector
        '
        Me.cmbSector.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbSector.DropDownHeight = 200
        Me.cmbSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSector.Font = New System.Drawing.Font("Tahoma", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSector.FormattingEnabled = True
        Me.cmbSector.IntegralHeight = False
        Me.cmbSector.ItemHeight = 28
        Me.cmbSector.Location = New System.Drawing.Point(3, 51)
        Me.cmbSector.Name = "cmbSector"
        Me.cmbSector.Size = New System.Drawing.Size(876, 36)
        Me.cmbSector.TabIndex = 1
        '
        'lblSSector
        '
        Me.lblSSector.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblSSector.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSSector.Location = New System.Drawing.Point(3, 19)
        Me.lblSSector.Name = "lblSSector"
        Me.lblSSector.Size = New System.Drawing.Size(876, 32)
        Me.lblSSector.TabIndex = 0
        Me.lblSSector.Text = "Sector "
        '
        'btnTramos
        '
        Me.btnTramos.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnTramos.ImageOptions.Image = CType(resources.GetObject("btnTramos.ImageOptions.Image"), System.Drawing.Image)
        Me.btnTramos.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.btnTramos.Location = New System.Drawing.Point(879, 19)
        Me.btnTramos.Name = "btnTramos"
        Me.btnTramos.Size = New System.Drawing.Size(169, 112)
        Me.btnTramos.TabIndex = 2
        Me.btnTramos.Text = "Modificar"
        Me.btnTramos.Visible = False
        '
        'grpTramo
        '
        Me.grpTramo.Controls.Add(Me.cmbTramo)
        Me.grpTramo.Controls.Add(Me.lblTramoS)
        Me.grpTramo.Controls.Add(Me.btnUbic)
        Me.grpTramo.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpTramo.Location = New System.Drawing.Point(0, 424)
        Me.grpTramo.Name = "grpTramo"
        Me.grpTramo.Size = New System.Drawing.Size(1051, 131)
        Me.grpTramo.TabIndex = 3
        Me.grpTramo.TabStop = False
        Me.grpTramo.Text = "Creación y configuración de Ubicaciones "
        '
        'cmbTramo
        '
        Me.cmbTramo.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbTramo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTramo.Font = New System.Drawing.Font("Tahoma", 13.8!)
        Me.cmbTramo.FormattingEnabled = True
        Me.cmbTramo.Location = New System.Drawing.Point(3, 51)
        Me.cmbTramo.Name = "cmbTramo"
        Me.cmbTramo.Size = New System.Drawing.Size(876, 36)
        Me.cmbTramo.TabIndex = 1
        '
        'lblTramoS
        '
        Me.lblTramoS.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTramoS.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTramoS.Location = New System.Drawing.Point(3, 19)
        Me.lblTramoS.Name = "lblTramoS"
        Me.lblTramoS.Size = New System.Drawing.Size(876, 32)
        Me.lblTramoS.TabIndex = 3
        Me.lblTramoS.Text = "Tramo"
        '
        'btnUbic
        '
        Me.btnUbic.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnUbic.ImageOptions.Image = CType(resources.GetObject("btnUbic.ImageOptions.Image"), System.Drawing.Image)
        Me.btnUbic.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.btnUbic.Location = New System.Drawing.Point(879, 19)
        Me.btnUbic.Name = "btnUbic"
        Me.btnUbic.Size = New System.Drawing.Size(169, 109)
        Me.btnUbic.TabIndex = 2
        Me.btnUbic.Text = "Modificar"
        Me.btnUbic.Visible = False
        '
        'grpCopiar
        '
        Me.grpCopiar.Controls.Add(Me.cboTramo2)
        Me.grpCopiar.Controls.Add(Me.Label1)
        Me.grpCopiar.Controls.Add(Me.btnCopiar)
        Me.grpCopiar.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpCopiar.Location = New System.Drawing.Point(0, 555)
        Me.grpCopiar.Name = "grpCopiar"
        Me.grpCopiar.Size = New System.Drawing.Size(1051, 126)
        Me.grpCopiar.TabIndex = 3
        Me.grpCopiar.TabStop = False
        Me.grpCopiar.Text = "  Copiar estructura "
        '
        'cboTramo2
        '
        Me.cboTramo2.Dock = System.Windows.Forms.DockStyle.Top
        Me.cboTramo2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTramo2.Font = New System.Drawing.Font("Tahoma", 13.8!)
        Me.cboTramo2.FormattingEnabled = True
        Me.cboTramo2.Location = New System.Drawing.Point(3, 51)
        Me.cboTramo2.Name = "cboTramo2"
        Me.cboTramo2.Size = New System.Drawing.Size(876, 36)
        Me.cboTramo2.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(876, 32)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Al Tramo"
        '
        'btnCopiar
        '
        Me.btnCopiar.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCopiar.ImageOptions.Image = CType(resources.GetObject("btnCopiar.ImageOptions.Image"), System.Drawing.Image)
        Me.btnCopiar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.btnCopiar.Location = New System.Drawing.Point(879, 19)
        Me.btnCopiar.Name = "btnCopiar"
        Me.btnCopiar.Size = New System.Drawing.Size(169, 104)
        Me.btnCopiar.TabIndex = 2
        Me.btnCopiar.Text = "Copiar"
        Me.btnCopiar.Visible = False
        '
        'grpArea
        '
        Me.grpArea.Controls.Add(Me.cmbArea)
        Me.grpArea.Controls.Add(Me.lblArea)
        Me.grpArea.Controls.Add(Me.cmdModificarArea)
        Me.grpArea.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpArea.Location = New System.Drawing.Point(0, 193)
        Me.grpArea.Name = "grpArea"
        Me.grpArea.Size = New System.Drawing.Size(1051, 97)
        Me.grpArea.TabIndex = 6
        Me.grpArea.TabStop = False
        Me.grpArea.Text = "Creación y configuración de tramos "
        '
        'cmbArea
        '
        Me.cmbArea.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbArea.DropDownHeight = 200
        Me.cmbArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbArea.Font = New System.Drawing.Font("Tahoma", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbArea.FormattingEnabled = True
        Me.cmbArea.IntegralHeight = False
        Me.cmbArea.ItemHeight = 28
        Me.cmbArea.Location = New System.Drawing.Point(3, 48)
        Me.cmbArea.Name = "cmbArea"
        Me.cmbArea.Size = New System.Drawing.Size(876, 36)
        Me.cmbArea.TabIndex = 1
        '
        'lblArea
        '
        Me.lblArea.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblArea.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArea.Location = New System.Drawing.Point(3, 19)
        Me.lblArea.Name = "lblArea"
        Me.lblArea.Size = New System.Drawing.Size(876, 29)
        Me.lblArea.TabIndex = 0
        Me.lblArea.Text = "Area"
        '
        'cmdModificarArea
        '
        Me.cmdModificarArea.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdModificarArea.ImageOptions.Image = CType(resources.GetObject("cmdModificarArea.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdModificarArea.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.cmdModificarArea.Location = New System.Drawing.Point(879, 19)
        Me.cmdModificarArea.Name = "cmdModificarArea"
        Me.cmdModificarArea.Size = New System.Drawing.Size(169, 75)
        Me.cmdModificarArea.TabIndex = 2
        Me.cmdModificarArea.Text = "Modificar"
        Me.cmdModificarArea.Visible = False
        '
        'frmEstructuraBod
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1051, 804)
        Me.Controls.Add(Me.grpCopiar)
        Me.Controls.Add(Me.grpTramo)
        Me.Controls.Add(Me.grpSector)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.grpArea)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmEstructuraBod"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Configuración de estructura de bodega"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSector.ResumeLayout(False)
        Me.grpTramo.ResumeLayout(False)
        Me.grpCopiar.ResumeLayout(False)
        Me.grpArea.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents grpSector As GroupBox
    Friend WithEvents btnTramos As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbSector As ComboBox
    Friend WithEvents grpTramo As GroupBox
    Friend WithEvents btnUbic As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbTramo As ComboBox
    Friend WithEvents grpCopiar As GroupBox
    Friend WithEvents btnCopiar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cboTramo2 As ComboBox
    Friend WithEvents mnuValidarEstructura As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuVerDiseño As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuGenerarEstructura As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuModificar As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuModificaSector As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuModificarTramo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuCopiarEstructura As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblSSector As Label
    Friend WithEvents lblTramoS As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents grpArea As GroupBox
    Friend WithEvents cmbArea As ComboBox
    Friend WithEvents lblArea As Label
    Friend WithEvents cmdModificarArea As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents mnuImportarEstructura As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup4 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
End Class
