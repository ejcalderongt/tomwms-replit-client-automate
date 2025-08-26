<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDiseño
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If BeBodega IsNot Nothing Then
                BeBodega.Dispose()
                BeBodega = Nothing
            End If            
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
        Dim lblAncho As System.Windows.Forms.Label
        Dim lblLargo As System.Windows.Forms.Label
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager()
        Me.hideContainerLeft = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel2 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel2_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.DgridPosiciones = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.prgUbicacionesPorTramo = New DevExpress.XtraEditors.ProgressBarControl()
        Me.prgTramos = New DevExpress.XtraEditors.ProgressBarControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtZoom = New System.Windows.Forms.NumericUpDown()
        Me.txtAncho = New System.Windows.Forms.NumericUpDown()
        Me.txtLargo = New System.Windows.Forms.NumericUpDown()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.lblPos = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbEmpresa = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbBodegas = New System.Windows.Forms.ComboBox()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.dockPanel3Document = New DevExpress.XtraBars.Docking2010.Views.Tabbed.Document()
        Me.PanBorde = New DevExpress.XtraEditors.PanelControl()
        Me.PanBodega = New DevExpress.XtraEditors.PanelControl()
        Me.DockPanel3 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel3_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        lblAncho = New System.Windows.Forms.Label()
        lblLargo = New System.Windows.Forms.Label()
        CType(Me.DockManager1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.hideContainerLeft.SuspendLayout
        Me.DockPanel2.SuspendLayout
        Me.DockPanel2_Container.SuspendLayout
        CType(Me.DgridPosiciones,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridView1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.DockPanel1.SuspendLayout
        Me.DockPanel1_Container.SuspendLayout
        CType(Me.prgUbicacionesPorTramo.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.prgTramos.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GroupControl2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupControl2.SuspendLayout
        CType(Me.txtZoom,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtAncho,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtLargo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupControl1.SuspendLayout
        CType(Me.dockPanel3Document,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PanBorde,System.ComponentModel.ISupportInitialize).BeginInit
        Me.PanBorde.SuspendLayout
        CType(Me.PanBodega,System.ComponentModel.ISupportInitialize).BeginInit
        Me.DockPanel3.SuspendLayout
        Me.hideContainerBottom.SuspendLayout
        Me.SuspendLayout
        '
        'lblAncho
        '
        lblAncho.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        lblAncho.AutoSize = true
        lblAncho.Location = New System.Drawing.Point(56, 67)
        lblAncho.Name = "lblAncho"
        lblAncho.Size = New System.Drawing.Size(41, 13)
        lblAncho.TabIndex = 34
        lblAncho.Text = "Ancho:"
        '
        'lblLargo
        '
        lblLargo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        lblLargo.AutoSize = true
        lblLargo.Location = New System.Drawing.Point(56, 40)
        lblLargo.Name = "lblLargo"
        lblLargo.Size = New System.Drawing.Size(38, 13)
        lblLargo.TabIndex = 33
        lblLargo.Text = "Largo:"
        '
        'DockManager1
        '
        Me.DockManager1.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerLeft, Me.hideContainerBottom})
        Me.DockManager1.Form = Me
        Me.DockManager1.HiddenPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.DockPanel1})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerLeft
        '
        Me.hideContainerLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(253,Byte),Integer), CType(CType(253,Byte),Integer), CType(CType(253,Byte),Integer))
        Me.hideContainerLeft.Controls.Add(Me.DockPanel2)
        Me.hideContainerLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.hideContainerLeft.Location = New System.Drawing.Point(0, 0)
        Me.hideContainerLeft.Name = "hideContainerLeft"
        Me.hideContainerLeft.Size = New System.Drawing.Size(23, 393)
        '
        'DockPanel2
        '
        Me.DockPanel2.Controls.Add(Me.DockPanel2_Container)
        Me.DockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.DockPanel2.ID = New System.Guid("a7c84705-c271-4e0a-a6e8-6f4f1f989b7f")
        Me.DockPanel2.Location = New System.Drawing.Point(-306, 0)
        Me.DockPanel2.Name = "DockPanel2"
        Me.DockPanel2.OriginalSize = New System.Drawing.Size(306, 200)
        Me.DockPanel2.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.DockPanel2.SavedIndex = 1
        Me.DockPanel2.Size = New System.Drawing.Size(306, 393)
        Me.DockPanel2.Text = "Filtros"
        Me.DockPanel2.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel2_Container
        '
        Me.DockPanel2_Container.Controls.Add(Me.DgridPosiciones)
        Me.DockPanel2_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel2_Container.Name = "DockPanel2_Container"
        Me.DockPanel2_Container.Size = New System.Drawing.Size(300, 365)
        Me.DockPanel2_Container.TabIndex = 0
        '
        'DgridPosiciones
        '
        Me.DgridPosiciones.Dock = System.Windows.Forms.DockStyle.Top
        Me.DgridPosiciones.Location = New System.Drawing.Point(0, 0)
        Me.DgridPosiciones.MainView = Me.GridView1
        Me.DgridPosiciones.Name = "DgridPosiciones"
        Me.DgridPosiciones.Size = New System.Drawing.Size(300, 200)
        Me.DgridPosiciones.TabIndex = 1
        Me.DgridPosiciones.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.DgridPosiciones
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ColumnAutoWidth = false
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.DockPanel1.ID = New System.Guid("3abe56ca-efbf-4645-b4c3-6d1eaefaef07")
        Me.DockPanel1.Location = New System.Drawing.Point(23, 0)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(279, 200)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(279, 393)
        Me.DockPanel1.Text = "Bodega"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.prgUbicacionesPorTramo)
        Me.DockPanel1_Container.Controls.Add(Me.prgTramos)
        Me.DockPanel1_Container.Controls.Add(Me.GroupControl2)
        Me.DockPanel1_Container.Controls.Add(Me.GroupControl1)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(273, 365)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'prgUbicacionesPorTramo
        '
        Me.prgUbicacionesPorTramo.Dock = System.Windows.Forms.DockStyle.Top
        Me.prgUbicacionesPorTramo.Location = New System.Drawing.Point(0, 262)
        Me.prgUbicacionesPorTramo.Name = "prgUbicacionesPorTramo"
        Me.prgUbicacionesPorTramo.Properties.ShowTitle = true
        Me.prgUbicacionesPorTramo.Properties.Step = 1
        Me.prgUbicacionesPorTramo.Size = New System.Drawing.Size(273, 18)
        Me.prgUbicacionesPorTramo.TabIndex = 4
        Me.prgUbicacionesPorTramo.Visible = false
        '
        'prgTramos
        '
        Me.prgTramos.Dock = System.Windows.Forms.DockStyle.Top
        Me.prgTramos.Location = New System.Drawing.Point(0, 244)
        Me.prgTramos.Name = "prgTramos"
        Me.prgTramos.Properties.ShowTitle = true
        Me.prgTramos.Properties.Step = 1
        Me.prgTramos.Size = New System.Drawing.Size(273, 18)
        Me.prgTramos.TabIndex = 5
        Me.prgTramos.Visible = false
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.Label27)
        Me.GroupControl2.Controls.Add(Me.txtZoom)
        Me.GroupControl2.Controls.Add(Me.txtAncho)
        Me.GroupControl2.Controls.Add(Me.txtLargo)
        Me.GroupControl2.Controls.Add(lblAncho)
        Me.GroupControl2.Controls.Add(lblLargo)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl2.Location = New System.Drawing.Point(0, 116)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(273, 128)
        Me.GroupControl2.TabIndex = 11
        Me.GroupControl2.Text = "Parámetros por bodega"
        '
        'Label27
        '
        Me.Label27.AutoSize = true
        Me.Label27.Location = New System.Drawing.Point(56, 94)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(37, 13)
        Me.Label27.TabIndex = 36
        Me.Label27.Text = "Zoom:"
        '
        'txtZoom
        '
        Me.txtZoom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtZoom.DecimalPlaces = 2
        Me.txtZoom.Location = New System.Drawing.Point(100, 92)
        Me.txtZoom.Name = "txtZoom"
        Me.txtZoom.Size = New System.Drawing.Size(70, 21)
        Me.txtZoom.TabIndex = 35
        Me.txtZoom.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'txtAncho
        '
        Me.txtAncho.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtAncho.DecimalPlaces = 3
        Me.txtAncho.Enabled = false
        Me.txtAncho.Location = New System.Drawing.Point(100, 65)
        Me.txtAncho.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtAncho.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtAncho.Name = "txtAncho"
        Me.txtAncho.Size = New System.Drawing.Size(70, 21)
        Me.txtAncho.TabIndex = 32
        '
        'txtLargo
        '
        Me.txtLargo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtLargo.DecimalPlaces = 3
        Me.txtLargo.Enabled = false
        Me.txtLargo.Location = New System.Drawing.Point(100, 38)
        Me.txtLargo.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtLargo.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtLargo.Name = "txtLargo"
        Me.txtLargo.Size = New System.Drawing.Size(70, 21)
        Me.txtLargo.TabIndex = 31
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.lblPos)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Controls.Add(Me.cmbEmpresa)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Controls.Add(Me.cmbBodegas)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(273, 116)
        Me.GroupControl1.TabIndex = 12
        Me.GroupControl1.Text = "Datos Generales"
        '
        'lblPos
        '
        Me.lblPos.AutoSize = true
        Me.lblPos.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPos.Location = New System.Drawing.Point(213, 103)
        Me.lblPos.Name = "lblPos"
        Me.lblPos.Size = New System.Drawing.Size(35, 7)
        Me.lblPos.TabIndex = 10
        Me.lblPos.Text = "Posición:"
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Enabled = false
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DimGray
        Me.Label1.Location = New System.Drawing.Point(2, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Empresa"
        '
        'cmbEmpresa
        '
        Me.cmbEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEmpresa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbEmpresa.ForeColor = System.Drawing.Color.Navy
        Me.cmbEmpresa.Location = New System.Drawing.Point(56, 33)
        Me.cmbEmpresa.Name = "cmbEmpresa"
        Me.cmbEmpresa.Size = New System.Drawing.Size(171, 21)
        Me.cmbEmpresa.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Enabled = false
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DimGray
        Me.Label2.Location = New System.Drawing.Point(2, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Bodega"
        '
        'cmbBodegas
        '
        Me.cmbBodegas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBodegas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbBodegas.ForeColor = System.Drawing.Color.Navy
        Me.cmbBodegas.Location = New System.Drawing.Point(56, 60)
        Me.cmbBodegas.Name = "cmbBodegas"
        Me.cmbBodegas.Size = New System.Drawing.Size(171, 21)
        Me.cmbBodegas.TabIndex = 9
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(192,Byte),Integer))
        Me.LabelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.LabelControl1.Location = New System.Drawing.Point(918, 0)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(68, 15)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "LabelControl1"
        Me.LabelControl1.Visible = false
        '
        'dockPanel3Document
        '
        Me.dockPanel3Document.Caption = "DockPanel3"
        Me.dockPanel3Document.ControlName = "DockPanel3"
        Me.dockPanel3Document.FloatLocation = New System.Drawing.Point(453, 194)
        Me.dockPanel3Document.FloatSize = New System.Drawing.Size(200, 200)
        Me.dockPanel3Document.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.[True]
        Me.dockPanel3Document.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.[True]
        Me.dockPanel3Document.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.[True]
        '
        'PanBorde
        '
        Me.PanBorde.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.PanBorde.Appearance.Image = My.Resources.Resources.fondo_abstracto_de_muro_de_ladrillos_1100_165
        Me.PanBorde.Appearance.Options.UseBackColor = true
        Me.PanBorde.Appearance.Options.UseImage = true
        Me.PanBorde.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanBorde.Controls.Add(Me.PanBodega)
        Me.PanBorde.Location = New System.Drawing.Point(647, 21)
        Me.PanBorde.Name = "PanBorde"
        Me.PanBorde.Size = New System.Drawing.Size(300, 210)
        Me.PanBorde.TabIndex = 1
        Me.PanBorde.Visible = false
        '
        'PanBodega
        '
        Me.PanBodega.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.PanBodega.Location = New System.Drawing.Point(10, 10)
        Me.PanBodega.Name = "PanBodega"
        Me.PanBodega.Size = New System.Drawing.Size(200, 100)
        Me.PanBodega.TabIndex = 1
        '
        'DockPanel3
        '
        Me.DockPanel3.Controls.Add(Me.DockPanel3_Container)
        Me.DockPanel3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel3.ID = New System.Guid("cfa7df5a-d8b9-4fd9-8d13-af5daac1d110")
        Me.DockPanel3.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel3.Name = "DockPanel3"
        Me.DockPanel3.OriginalSize = New System.Drawing.Size(200, 200)
        Me.DockPanel3.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel3.SavedIndex = 0
        Me.DockPanel3.Size = New System.Drawing.Size(994, 200)
        Me.DockPanel3.Text = "DockPanel3"
        Me.DockPanel3.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel3_Container
        '
        Me.DockPanel3_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel3_Container.Name = "DockPanel3_Container"
        Me.DockPanel3_Container.Size = New System.Drawing.Size(988, 172)
        Me.DockPanel3_Container.TabIndex = 0
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(253,Byte),Integer), CType(CType(253,Byte),Integer), CType(CType(253,Byte),Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel3)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(23, 370)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(994, 23)
        '
        'frmDiseño
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = true
        Me.ClientSize = New System.Drawing.Size(1017, 393)
        Me.Controls.Add(Me.PanBorde)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.hideContainerLeft)
        Me.Name = "frmDiseño"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Diseñador de bodega"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DockManager1,System.ComponentModel.ISupportInitialize).EndInit
        Me.hideContainerLeft.ResumeLayout(false)
        Me.DockPanel2.ResumeLayout(false)
        Me.DockPanel2_Container.ResumeLayout(false)
        CType(Me.DgridPosiciones,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView1,System.ComponentModel.ISupportInitialize).EndInit
        Me.DockPanel1.ResumeLayout(false)
        Me.DockPanel1_Container.ResumeLayout(false)
        CType(Me.prgUbicacionesPorTramo.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.prgTramos.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GroupControl2,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl2.ResumeLayout(false)
        Me.GroupControl2.PerformLayout
        CType(Me.txtZoom,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtAncho,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtLargo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl1.ResumeLayout(false)
        Me.GroupControl1.PerformLayout
        CType(Me.dockPanel3Document,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PanBorde,System.ComponentModel.ISupportInitialize).EndInit
        Me.PanBorde.ResumeLayout(false)
        CType(Me.PanBodega,System.ComponentModel.ISupportInitialize).EndInit
        Me.DockPanel3.ResumeLayout(false)
        Me.hideContainerBottom.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dockPanel3Document As DevExpress.XtraBars.Docking2010.Views.Tabbed.Document
    Friend WithEvents PanBorde As DevExpress.XtraEditors.PanelControl
    Friend WithEvents DockPanel2 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel2_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents PanBodega As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label27 As Label
    Friend WithEvents txtZoom As NumericUpDown
    Friend WithEvents txtAncho As NumericUpDown
    Friend WithEvents txtLargo As NumericUpDown
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblPos As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbEmpresa As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbBodegas As ComboBox
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DgridPosiciones As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents prgUbicacionesPorTramo As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents prgTramos As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents hideContainerLeft As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel3 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel3_Container As DevExpress.XtraBars.Docking.ControlContainer
End Class
