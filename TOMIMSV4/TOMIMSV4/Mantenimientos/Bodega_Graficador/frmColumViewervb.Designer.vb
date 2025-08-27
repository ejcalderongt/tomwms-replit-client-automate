<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmColumViewervb
    Inherits System.Windows.Forms.Form

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
        Dim Label20 As System.Windows.Forms.Label
        Dim lblAlto As System.Windows.Forms.Label
        Dim Label45 As System.Windows.Forms.Label
        Dim lblCodigoSectorReferencia As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim Label26 As System.Windows.Forms.Label
        Dim lblNiveles As System.Windows.Forms.Label
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.picRack = New System.Windows.Forms.PictureBox()
        Me.cmdDrawText = New System.Windows.Forms.Button()
        Me.txtX = New System.Windows.Forms.NumericUpDown()
        Me.txtY = New System.Windows.Forms.NumericUpDown()
        Me.lblx = New System.Windows.Forms.Label()
        Me.lblY = New System.Windows.Forms.Label()
        Me.txtTexto = New System.Windows.Forms.TextBox()
        Me.lblCordinates = New System.Windows.Forms.Label()
        Me.listPOints = New System.Windows.Forms.ListBox()
        Me.cmdDrawPallet = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupControl11 = New DevExpress.XtraEditors.GroupControl()
        Me.txtNiveles = New System.Windows.Forms.NumericUpDown()
        Me.txtAncho = New System.Windows.Forms.NumericUpDown()
        Me.txtAlto = New System.Windows.Forms.NumericUpDown()
        Me.GroupControl12 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.chkEsRack = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbAreasR = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtDescripcionTramo = New DevExpress.XtraEditors.TextEdit()
        Me.cmbSector = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkSistemaTramo = New DevExpress.XtraEditors.CheckEdit()
        Me.txtCodigoTramo = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivoTramo = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.DgridStock = New DevExpress.XtraGrid.GridControl()
        Me.gridview1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Label20 = New System.Windows.Forms.Label()
        lblAlto = New System.Windows.Forms.Label()
        Label45 = New System.Windows.Forms.Label()
        lblCodigoSectorReferencia = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        Label26 = New System.Windows.Forms.Label()
        lblNiveles = New System.Windows.Forms.Label()
        CType(Me.picRack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl11.SuspendLayout()
        CType(Me.txtNiveles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAncho, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAlto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl12.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.chkEsRack.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAreasR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcionTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSector.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSistemaTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigoTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoTramo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.DgridStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridview1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label20
        '
        Label20.AutoSize = True
        Label20.Location = New System.Drawing.Point(33, 128)
        Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label20.Name = "Label20"
        Label20.Size = New System.Drawing.Size(47, 16)
        Label20.TabIndex = 4
        Label20.Text = "Ancho:"
        '
        'lblAlto
        '
        lblAlto.AutoSize = True
        lblAlto.Location = New System.Drawing.Point(33, 94)
        lblAlto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblAlto.Name = "lblAlto"
        lblAlto.Size = New System.Drawing.Size(34, 16)
        lblAlto.TabIndex = 2
        lblAlto.Text = "Alto:"
        '
        'Label45
        '
        Label45.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label45.AutoSize = True
        Label45.Location = New System.Drawing.Point(7, 47)
        Label45.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label45.Name = "Label45"
        Label45.Size = New System.Drawing.Size(39, 16)
        Label45.TabIndex = 0
        Label45.Text = "Area:"
        '
        'lblCodigoSectorReferencia
        '
        lblCodigoSectorReferencia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblCodigoSectorReferencia.AutoSize = True
        lblCodigoSectorReferencia.Location = New System.Drawing.Point(7, 113)
        lblCodigoSectorReferencia.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigoSectorReferencia.Name = "lblCodigoSectorReferencia"
        lblCodigoSectorReferencia.Size = New System.Drawing.Size(51, 16)
        lblCodigoSectorReferencia.TabIndex = 4
        lblCodigoSectorReferencia.Text = "Código:"
        '
        'Label15
        '
        Label15.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label15.AutoSize = True
        Label15.Location = New System.Drawing.Point(7, 79)
        Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(49, 16)
        Label15.TabIndex = 2
        Label15.Text = "Sector:"
        '
        'Label26
        '
        Label26.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label26.AutoSize = True
        Label26.Location = New System.Drawing.Point(7, 145)
        Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label26.Name = "Label26"
        Label26.Size = New System.Drawing.Size(77, 16)
        Label26.TabIndex = 6
        Label26.Text = "Descripción:"
        '
        'lblNiveles
        '
        lblNiveles.AutoSize = True
        lblNiveles.Location = New System.Drawing.Point(33, 59)
        lblNiveles.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblNiveles.Name = "lblNiveles"
        lblNiveles.Size = New System.Drawing.Size(52, 16)
        lblNiveles.TabIndex = 0
        lblNiveles.Text = "Niveles:"
        '
        'picRack
        '
        Me.picRack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picRack.Dock = System.Windows.Forms.DockStyle.Left
        Me.picRack.Image = Global.TOMWMS.My.Resources.Resources.SingleColumn_4Levels_Empty_Fit
        Me.picRack.Location = New System.Drawing.Point(0, 0)
        Me.picRack.Margin = New System.Windows.Forms.Padding(4)
        Me.picRack.Name = "picRack"
        Me.picRack.Size = New System.Drawing.Size(534, 757)
        Me.picRack.TabIndex = 0
        Me.picRack.TabStop = False
        '
        'cmdDrawText
        '
        Me.cmdDrawText.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDrawText.Location = New System.Drawing.Point(186, 195)
        Me.cmdDrawText.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdDrawText.Name = "cmdDrawText"
        Me.cmdDrawText.Size = New System.Drawing.Size(160, 28)
        Me.cmdDrawText.TabIndex = 7
        Me.cmdDrawText.Text = "Pinta Texto"
        Me.cmdDrawText.UseVisualStyleBackColor = True
        '
        'txtX
        '
        Me.txtX.Location = New System.Drawing.Point(186, 131)
        Me.txtX.Margin = New System.Windows.Forms.Padding(4)
        Me.txtX.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.txtX.Name = "txtX"
        Me.txtX.Size = New System.Drawing.Size(160, 23)
        Me.txtX.TabIndex = 3
        Me.txtX.Value = New Decimal(New Integer() {170, 0, 0, 0})
        '
        'txtY
        '
        Me.txtY.Location = New System.Drawing.Point(186, 163)
        Me.txtY.Margin = New System.Windows.Forms.Padding(4)
        Me.txtY.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.txtY.Name = "txtY"
        Me.txtY.Size = New System.Drawing.Size(160, 23)
        Me.txtY.TabIndex = 5
        Me.txtY.Value = New Decimal(New Integer() {330, 0, 0, 0})
        '
        'lblx
        '
        Me.lblx.AutoSize = True
        Me.lblx.Location = New System.Drawing.Point(354, 131)
        Me.lblx.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblx.Name = "lblx"
        Me.lblx.Size = New System.Drawing.Size(15, 16)
        Me.lblx.TabIndex = 4
        Me.lblx.Text = "X"
        '
        'lblY
        '
        Me.lblY.AutoSize = True
        Me.lblY.Location = New System.Drawing.Point(354, 170)
        Me.lblY.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblY.Name = "lblY"
        Me.lblY.Size = New System.Drawing.Size(14, 16)
        Me.lblY.TabIndex = 6
        Me.lblY.Text = "Y"
        '
        'txtTexto
        '
        Me.txtTexto.Location = New System.Drawing.Point(186, 99)
        Me.txtTexto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTexto.Name = "txtTexto"
        Me.txtTexto.Size = New System.Drawing.Size(159, 23)
        Me.txtTexto.TabIndex = 2
        Me.txtTexto.Text = "Pallet "
        '
        'lblCordinates
        '
        Me.lblCordinates.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblCordinates.Location = New System.Drawing.Point(534, 732)
        Me.lblCordinates.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCordinates.Name = "lblCordinates"
        Me.lblCordinates.Size = New System.Drawing.Size(1153, 25)
        Me.lblCordinates.TabIndex = 2
        Me.lblCordinates.Text = "-."
        '
        'listPOints
        '
        Me.listPOints.FormattingEnabled = True
        Me.listPOints.ItemHeight = 16
        Me.listPOints.Location = New System.Drawing.Point(18, 45)
        Me.listPOints.Margin = New System.Windows.Forms.Padding(4)
        Me.listPOints.Name = "listPOints"
        Me.listPOints.Size = New System.Drawing.Size(159, 228)
        Me.listPOints.TabIndex = 0
        '
        'cmdDrawPallet
        '
        Me.cmdDrawPallet.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDrawPallet.Location = New System.Drawing.Point(18, 281)
        Me.cmdDrawPallet.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdDrawPallet.Name = "cmdDrawPallet"
        Me.cmdDrawPallet.Size = New System.Drawing.Size(160, 28)
        Me.cmdDrawPallet.TabIndex = 8
        Me.cmdDrawPallet.Text = "Pinta pallet"
        Me.cmdDrawPallet.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(186, 45)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(160, 28)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Reset"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GroupControl11
        '
        Me.GroupControl11.Controls.Add(Me.txtNiveles)
        Me.GroupControl11.Controls.Add(lblNiveles)
        Me.GroupControl11.Controls.Add(Me.txtAncho)
        Me.GroupControl11.Controls.Add(Me.txtAlto)
        Me.GroupControl11.Controls.Add(Label20)
        Me.GroupControl11.Controls.Add(lblAlto)
        Me.GroupControl11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl11.Location = New System.Drawing.Point(378, 28)
        Me.GroupControl11.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl11.Name = "GroupControl11"
        Me.GroupControl11.Size = New System.Drawing.Size(329, 241)
        Me.GroupControl11.TabIndex = 1
        Me.GroupControl11.Text = "Dimensiones"
        '
        'txtNiveles
        '
        Me.txtNiveles.DecimalPlaces = 6
        Me.txtNiveles.Location = New System.Drawing.Point(103, 57)
        Me.txtNiveles.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNiveles.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtNiveles.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtNiveles.Name = "txtNiveles"
        Me.txtNiveles.Size = New System.Drawing.Size(156, 23)
        Me.txtNiveles.TabIndex = 1
        '
        'txtAncho
        '
        Me.txtAncho.DecimalPlaces = 6
        Me.txtAncho.Location = New System.Drawing.Point(103, 124)
        Me.txtAncho.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAncho.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtAncho.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtAncho.Name = "txtAncho"
        Me.txtAncho.Size = New System.Drawing.Size(156, 23)
        Me.txtAncho.TabIndex = 5
        '
        'txtAlto
        '
        Me.txtAlto.DecimalPlaces = 6
        Me.txtAlto.Location = New System.Drawing.Point(103, 89)
        Me.txtAlto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAlto.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtAlto.Minimum = New Decimal(New Integer() {1215752191, 23, 0, -2147483648})
        Me.txtAlto.Name = "txtAlto"
        Me.txtAlto.Size = New System.Drawing.Size(156, 23)
        Me.txtAlto.TabIndex = 3
        '
        'GroupControl12
        '
        Me.GroupControl12.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl12.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl12.Controls.Add(Me.GroupControl11)
        Me.GroupControl12.Controls.Add(Me.GroupControl2)
        Me.GroupControl12.Location = New System.Drawing.Point(543, 0)
        Me.GroupControl12.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl12.Name = "GroupControl12"
        Me.GroupControl12.Size = New System.Drawing.Size(709, 271)
        Me.GroupControl12.TabIndex = 0
        Me.GroupControl12.Text = "Datos de Tramo"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.chkEsRack)
        Me.GroupControl2.Controls.Add(Me.cmbAreasR)
        Me.GroupControl2.Controls.Add(Me.txtDescripcionTramo)
        Me.GroupControl2.Controls.Add(Me.cmbSector)
        Me.GroupControl2.Controls.Add(Label26)
        Me.GroupControl2.Controls.Add(Me.chkSistemaTramo)
        Me.GroupControl2.Controls.Add(Label45)
        Me.GroupControl2.Controls.Add(Me.txtCodigoTramo)
        Me.GroupControl2.Controls.Add(Me.chkActivoTramo)
        Me.GroupControl2.Controls.Add(Label15)
        Me.GroupControl2.Controls.Add(lblCodigoSectorReferencia)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl2.Location = New System.Drawing.Point(2, 28)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(376, 241)
        Me.GroupControl2.TabIndex = 0
        Me.GroupControl2.Text = "Datos Generales"
        '
        'chkEsRack
        '
        Me.chkEsRack.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkEsRack.Location = New System.Drawing.Point(184, 186)
        Me.chkEsRack.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEsRack.Name = "chkEsRack"
        Me.chkEsRack.Properties.Caption = "Es Rack"
        Me.chkEsRack.Size = New System.Drawing.Size(81, 24)
        Me.chkEsRack.TabIndex = 9
        '
        'cmbAreasR
        '
        Me.cmbAreasR.Location = New System.Drawing.Point(100, 44)
        Me.cmbAreasR.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbAreasR.Name = "cmbAreasR"
        Me.cmbAreasR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbAreasR.Properties.NullText = ""
        Me.cmbAreasR.Properties.ReadOnly = True
        Me.cmbAreasR.Size = New System.Drawing.Size(257, 22)
        Me.cmbAreasR.TabIndex = 1
        '
        'txtDescripcionTramo
        '
        Me.txtDescripcionTramo.Location = New System.Drawing.Point(100, 142)
        Me.txtDescripcionTramo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDescripcionTramo.Name = "txtDescripcionTramo"
        Me.txtDescripcionTramo.Properties.MaxLength = 50
        Me.txtDescripcionTramo.Properties.ReadOnly = True
        Me.txtDescripcionTramo.Size = New System.Drawing.Size(257, 22)
        Me.txtDescripcionTramo.TabIndex = 7
        '
        'cmbSector
        '
        Me.cmbSector.Location = New System.Drawing.Point(100, 76)
        Me.cmbSector.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbSector.Name = "cmbSector"
        Me.cmbSector.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbSector.Properties.NullText = ""
        Me.cmbSector.Properties.ReadOnly = True
        Me.cmbSector.Size = New System.Drawing.Size(257, 22)
        Me.cmbSector.TabIndex = 3
        '
        'chkSistemaTramo
        '
        Me.chkSistemaTramo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkSistemaTramo.Location = New System.Drawing.Point(275, 186)
        Me.chkSistemaTramo.Margin = New System.Windows.Forms.Padding(4)
        Me.chkSistemaTramo.Name = "chkSistemaTramo"
        Me.chkSistemaTramo.Properties.Caption = "Sistema"
        Me.chkSistemaTramo.Size = New System.Drawing.Size(81, 24)
        Me.chkSistemaTramo.TabIndex = 10
        '
        'txtCodigoTramo
        '
        Me.txtCodigoTramo.Location = New System.Drawing.Point(100, 110)
        Me.txtCodigoTramo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigoTramo.Name = "txtCodigoTramo"
        Me.txtCodigoTramo.Properties.MaxLength = 50
        Me.txtCodigoTramo.Properties.ReadOnly = True
        Me.txtCodigoTramo.Size = New System.Drawing.Size(257, 22)
        Me.txtCodigoTramo.TabIndex = 5
        '
        'chkActivoTramo
        '
        Me.chkActivoTramo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkActivoTramo.EditValue = True
        Me.chkActivoTramo.Location = New System.Drawing.Point(100, 186)
        Me.chkActivoTramo.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActivoTramo.Name = "chkActivoTramo"
        Me.chkActivoTramo.Properties.Caption = "Activo"
        Me.chkActivoTramo.Size = New System.Drawing.Size(81, 24)
        Me.chkActivoTramo.TabIndex = 8
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.listPOints)
        Me.GroupControl1.Controls.Add(Me.cmdDrawText)
        Me.GroupControl1.Controls.Add(Me.txtX)
        Me.GroupControl1.Controls.Add(Me.txtY)
        Me.GroupControl1.Controls.Add(Me.Button2)
        Me.GroupControl1.Controls.Add(Me.lblx)
        Me.GroupControl1.Controls.Add(Me.cmdDrawPallet)
        Me.GroupControl1.Controls.Add(Me.lblY)
        Me.GroupControl1.Controls.Add(Me.txtTexto)
        Me.GroupControl1.Location = New System.Drawing.Point(543, 277)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(378, 336)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Pallets en ubicación"
        '
        'DgridStock
        '
        Me.DgridStock.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgridStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgridStock.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.DgridStock.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.DgridStock.Location = New System.Drawing.Point(534, 0)
        Me.DgridStock.MainView = Me.gridview1
        Me.DgridStock.Margin = New System.Windows.Forms.Padding(4)
        Me.DgridStock.Name = "DgridStock"
        Me.DgridStock.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPictureEdit1})
        Me.DgridStock.Size = New System.Drawing.Size(1153, 732)
        Me.DgridStock.TabIndex = 3
        Me.DgridStock.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridview1})
        '
        'gridview1
        '
        Me.gridview1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridview1.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridview1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridview1.Appearance.Row.Options.UseFont = True
        Me.gridview1.DetailHeight = 431
        Me.gridview1.GridControl = Me.DgridStock
        Me.gridview1.Name = "gridview1"
        Me.gridview1.OptionsBehavior.Editable = False
        Me.gridview1.OptionsView.ColumnAutoWidth = False
        Me.gridview1.OptionsView.ShowAutoFilterRow = True
        Me.gridview1.OptionsView.ShowFooter = True
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        '
        'frmColumViewervb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1687, 757)
        Me.Controls.Add(Me.DgridStock)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.GroupControl12)
        Me.Controls.Add(Me.lblCordinates)
        Me.Controls.Add(Me.picRack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmColumViewervb"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Visor de columna"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.picRack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl11.ResumeLayout(False)
        Me.GroupControl11.PerformLayout()
        CType(Me.txtNiveles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAncho, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAlto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl12.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.chkEsRack.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAreasR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcionTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSector.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSistemaTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigoTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoTramo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.DgridStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridview1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents picRack As PictureBox
    Friend WithEvents cmdDrawText As Button
    Friend WithEvents txtX As NumericUpDown
    Friend WithEvents txtY As NumericUpDown
    Friend WithEvents lblx As Label
    Friend WithEvents lblY As Label
    Friend WithEvents txtTexto As TextBox
    Friend WithEvents lblCordinates As Label
    Friend WithEvents listPOints As ListBox
    Friend WithEvents cmdDrawPallet As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents GroupControl11 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtAncho As NumericUpDown
    Friend WithEvents txtAlto As NumericUpDown
    Friend WithEvents GroupControl12 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkEsRack As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmbSector As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbAreasR As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkActivoTramo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtCodigoTramo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkSistemaTramo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtDescripcionTramo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNiveles As NumericUpDown
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents DgridStock As DevExpress.XtraGrid.GridControl
    Friend WithEvents gridview1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
End Class
