<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBD))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdOpendt = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdXcute = New DevExpress.XtraBars.BarButtonItem()
        Me.lblRegistros = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.dgvTablas = New System.Windows.Forms.DataGridView()
        Me.lblTablas = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtQuery = New System.Windows.Forms.TextBox()
        Me.lblSentencia = New System.Windows.Forms.Label()
        Me.DgridDatos = New System.Windows.Forms.DataGridView()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvTablas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DgridDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ApplicationIcon = CType(resources.GetObject("RibbonControl.ApplicationIcon"), System.Drawing.Bitmap)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdOpendt, Me.cmdXcute, Me.lblRegistros})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1048, 143)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdOpendt
        '
        Me.cmdOpendt.Caption = "OpenDT"
        Me.cmdOpendt.Glyph = CType(resources.GetObject("cmdOpendt.Glyph"), System.Drawing.Image)
        Me.cmdOpendt.Id = 1
        Me.cmdOpendt.LargeGlyph = CType(resources.GetObject("cmdOpendt.LargeGlyph"), System.Drawing.Image)
        Me.cmdOpendt.Name = "cmdOpendt"
        '
        'cmdXcute
        '
        Me.cmdXcute.Caption = "Xcute"
        Me.cmdXcute.Glyph = CType(resources.GetObject("cmdXcute.Glyph"), System.Drawing.Image)
        Me.cmdXcute.Id = 2
        Me.cmdXcute.LargeGlyph = CType(resources.GetObject("cmdXcute.LargeGlyph"), System.Drawing.Image)
        Me.cmdXcute.Name = "cmdXcute"
        '
        'lblRegistros
        '
        Me.lblRegistros.Caption = "Registros: 0"
        Me.lblRegistros.Id = 3
        Me.lblRegistros.Name = "lblRegistros"
        Me.lblRegistros.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdOpendt)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdXcute)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegistros)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 637)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1048, 31)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 143)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvTablas)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTablas)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblSentencia)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DgridDatos)
        Me.SplitContainer1.Size = New System.Drawing.Size(1048, 494)
        Me.SplitContainer1.SplitterDistance = 496
        Me.SplitContainer1.TabIndex = 0
        '
        'dgvTablas
        '
        Me.dgvTablas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTablas.BackgroundColor = System.Drawing.Color.White
        Me.dgvTablas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTablas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTablas.Location = New System.Drawing.Point(0, 24)
        Me.dgvTablas.MultiSelect = False
        Me.dgvTablas.Name = "dgvTablas"
        Me.dgvTablas.ReadOnly = True
        Me.dgvTablas.Size = New System.Drawing.Size(496, 470)
        Me.dgvTablas.TabIndex = 1
        '
        'lblTablas
        '
        Me.lblTablas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTablas.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTablas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblTablas.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTablas.Location = New System.Drawing.Point(0, 0)
        Me.lblTablas.Name = "lblTablas"
        Me.lblTablas.Size = New System.Drawing.Size(496, 24)
        Me.lblTablas.TabIndex = 0
        Me.lblTablas.Text = "Tablas"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtQuery)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(556, 207)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'txtQuery
        '
        Me.txtQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtQuery.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuery.Location = New System.Drawing.Point(3, 17)
        Me.txtQuery.Multiline = True
        Me.txtQuery.Name = "txtQuery"
        Me.txtQuery.Size = New System.Drawing.Size(550, 187)
        Me.txtQuery.TabIndex = 0
        Me.txtQuery.Text = "SELECT * FROM TABLE"
        '
        'lblSentencia
        '
        Me.lblSentencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSentencia.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblSentencia.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSentencia.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSentencia.Location = New System.Drawing.Point(0, 0)
        Me.lblSentencia.Name = "lblSentencia"
        Me.lblSentencia.Size = New System.Drawing.Size(548, 24)
        Me.lblSentencia.TabIndex = 0
        Me.lblSentencia.Text = "Comando"
        '
        'DgridDatos
        '
        Me.DgridDatos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DgridDatos.BackgroundColor = System.Drawing.Color.White
        Me.DgridDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgridDatos.Location = New System.Drawing.Point(8, 253)
        Me.DgridDatos.Name = "DgridDatos"
        Me.DgridDatos.Size = New System.Drawing.Size(548, 221)
        Me.DgridDatos.TabIndex = 2
        '
        'frmBD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1048, 668)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmBD"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Base de datos"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvTablas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DgridDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents dgvTablas As DataGridView
    Friend WithEvents lblTablas As Label
    Friend WithEvents DgridDatos As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtQuery As TextBox
    Friend WithEvents lblSentencia As Label
    Friend WithEvents cmdOpendt As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdXcute As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblRegistros As DevExpress.XtraBars.BarStaticItem
End Class
