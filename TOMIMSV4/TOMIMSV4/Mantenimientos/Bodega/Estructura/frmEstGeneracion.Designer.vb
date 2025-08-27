<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEstGeneracion
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEstGeneracion))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.pBar2 = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.btnGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.btnAgregar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnBorrar = New DevExpress.XtraBars.BarButtonItem()
        Me.btnArriba = New DevExpress.XtraBars.BarButtonItem()
        Me.btnAbajo = New DevExpress.XtraBars.BarButtonItem()
        Me.btnGenerar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarEditItem1 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.cboEdit = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemComboBox2 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.BarButtonItem5 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem6 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuDisenno = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup5 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.lblCreandoUbicaciones = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pBar = New System.Windows.Forms.ProgressBar()
        Me.lblprg1 = New System.Windows.Forms.Label()
        Me.lblPrg = New System.Windows.Forms.Label()
        Me.tvData = New System.Windows.Forms.TreeView()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.BarButtonItem8 = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.pBar2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "point2.png")
        Me.ImageList1.Images.SetKeyName(1, "point.png")
        Me.ImageList1.Images.SetKeyName(2, "Refresh.png")
        Me.ImageList1.Images.SetKeyName(3, "if_sign-check_299110.png")
        Me.ImageList1.Images.SetKeyName(4, "if_sign-error_299045.png")
        Me.ImageList1.Images.SetKeyName(5, "if_dialog-warning_118940.png")
        Me.ImageList1.Images.SetKeyName(6, "blank16.png")
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.GroupBox2)
        Me.GroupControl1.Controls.Add(Me.GroupBox1)
        Me.GroupControl1.Controls.Add(Me.tvData)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1051, 611)
        Me.GroupControl1.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.pBar2)
        Me.GroupBox2.Controls.Add(Me.lblCreandoUbicaciones)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(579, 168)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(470, 161)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'pBar2
        '
        Me.pBar2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pBar2.EditValue = 0
        Me.pBar2.Location = New System.Drawing.Point(3, 110)
        Me.pBar2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pBar2.MenuManager = Me.RibbonControl
        Me.pBar2.Name = "pBar2"
        Me.pBar2.Size = New System.Drawing.Size(464, 47)
        Me.pBar2.TabIndex = 1
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuActualizar, Me.mnuAsignacion, Me.btnGuardar, Me.BarButtonItem3, Me.btnAgregar, Me.btnBorrar, Me.btnArriba, Me.btnAbajo, Me.btnGenerar, Me.BarEditItem1, Me.BarButtonItem2, Me.BarButtonItem4, Me.cboEdit, Me.BarButtonItem5, Me.BarButtonItem6, Me.mnuDisenno})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 22
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComboBox1, Me.RepositoryItemComboBox2})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1051, 193)
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.Image = CType(resources.GetObject("mnuActualizar.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuActualizar.ImageOptions.LargeImage = CType(resources.GetObject("mnuActualizar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuAsignacion
        '
        Me.mnuAsignacion.Caption = "Asignacion"
        Me.mnuAsignacion.Id = 4
        Me.mnuAsignacion.ImageOptions.Image = CType(resources.GetObject("mnuAsignacion.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuAsignacion.ImageOptions.LargeImage = CType(resources.GetObject("mnuAsignacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuAsignacion.Name = "mnuAsignacion"
        Me.mnuAsignacion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'btnGuardar
        '
        Me.btnGuardar.Caption = "Guardar"
        Me.btnGuardar.Id = 5
        Me.btnGuardar.ImageOptions.Image = CType(resources.GetObject("btnGuardar.ImageOptions.Image"), System.Drawing.Image)
        Me.btnGuardar.ImageOptions.LargeImage = CType(resources.GetObject("btnGuardar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Generar Estructura"
        Me.BarButtonItem3.Id = 6
        Me.BarButtonItem3.ImageOptions.Image = CType(resources.GetObject("BarButtonItem3.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem3.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem3.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'btnAgregar
        '
        Me.btnAgregar.Caption = "Agregar"
        Me.btnAgregar.Id = 7
        Me.btnAgregar.ImageOptions.Image = CType(resources.GetObject("btnAgregar.ImageOptions.Image"), System.Drawing.Image)
        Me.btnAgregar.ImageOptions.LargeImage = CType(resources.GetObject("btnAgregar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large
        '
        'btnBorrar
        '
        Me.btnBorrar.Caption = "Borrar"
        Me.btnBorrar.Id = 8
        Me.btnBorrar.ImageOptions.Image = CType(resources.GetObject("btnBorrar.ImageOptions.Image"), System.Drawing.Image)
        Me.btnBorrar.ImageOptions.LargeImage = CType(resources.GetObject("btnBorrar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.btnBorrar.Name = "btnBorrar"
        '
        'btnArriba
        '
        Me.btnArriba.Caption = "Arriba"
        Me.btnArriba.Id = 9
        Me.btnArriba.ImageOptions.Image = CType(resources.GetObject("btnArriba.ImageOptions.Image"), System.Drawing.Image)
        Me.btnArriba.ImageOptions.LargeImage = CType(resources.GetObject("btnArriba.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.btnArriba.Name = "btnArriba"
        '
        'btnAbajo
        '
        Me.btnAbajo.Caption = "Abajo"
        Me.btnAbajo.Id = 10
        Me.btnAbajo.ImageOptions.Image = CType(resources.GetObject("btnAbajo.ImageOptions.Image"), System.Drawing.Image)
        Me.btnAbajo.ImageOptions.LargeImage = CType(resources.GetObject("btnAbajo.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.btnAbajo.Name = "btnAbajo"
        '
        'btnGenerar
        '
        Me.btnGenerar.Caption = "Generar Estructura"
        Me.btnGenerar.Id = 12
        Me.btnGenerar.ImageOptions.Image = CType(resources.GetObject("btnGenerar.ImageOptions.Image"), System.Drawing.Image)
        Me.btnGenerar.ImageOptions.LargeImage = CType(resources.GetObject("btnGenerar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.btnGenerar.Name = "btnGenerar"
        '
        'BarEditItem1
        '
        Me.BarEditItem1.Caption = "BarEditItem1"
        Me.BarEditItem1.Edit = Me.RepositoryItemComboBox1
        Me.BarEditItem1.Id = 13
        Me.BarEditItem1.Name = "BarEditItem1"
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.ActAsDropDown = True
        Me.BarButtonItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.BarButtonItem2.Caption = "Copiar Estructura"
        Me.BarButtonItem2.Id = 14
        Me.BarButtonItem2.ImageOptions.Image = CType(resources.GetObject("BarButtonItem2.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem2.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem2.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "BarButtonItem4"
        Me.BarButtonItem4.Id = 15
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'cboEdit
        '
        Me.cboEdit.Caption = "Tramo"
        Me.cboEdit.Edit = Me.RepositoryItemComboBox2
        Me.cboEdit.Id = 16
        Me.cboEdit.Name = "cboEdit"
        '
        'RepositoryItemComboBox2
        '
        Me.RepositoryItemComboBox2.AutoHeight = False
        Me.RepositoryItemComboBox2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox2.Name = "RepositoryItemComboBox2"
        '
        'BarButtonItem5
        '
        Me.BarButtonItem5.Caption = "Copiar Estructura"
        Me.BarButtonItem5.Id = 17
        Me.BarButtonItem5.Name = "BarButtonItem5"
        '
        'BarButtonItem6
        '
        Me.BarButtonItem6.Caption = "Borrar estructura actual"
        Me.BarButtonItem6.Id = 20
        Me.BarButtonItem6.ImageOptions.Image = CType(resources.GetObject("BarButtonItem6.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem6.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem6.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem6.Name = "BarButtonItem6"
        Me.BarButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuDisenno
        '
        Me.mnuDisenno.Caption = "Diseño"
        Me.mnuDisenno.Id = 21
        Me.mnuDisenno.ImageOptions.Image = CType(resources.GetObject("mnuDisenno.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuDisenno.ImageOptions.LargeImage = CType(resources.GetObject("mnuDisenno.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuDisenno.Name = "mnuDisenno"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup5, Me.RibbonPageGroup3})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'RibbonPageGroup5
        '
        Me.RibbonPageGroup5.ItemLinks.Add(Me.btnGenerar)
        Me.RibbonPageGroup5.Name = "RibbonPageGroup5"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.BarButtonItem6)
        Me.RibbonPageGroup3.ItemLinks.Add(Me.mnuDisenno)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        '
        'lblCreandoUbicaciones
        '
        Me.lblCreandoUbicaciones.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblCreandoUbicaciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreandoUbicaciones.Location = New System.Drawing.Point(3, 20)
        Me.lblCreandoUbicaciones.Name = "lblCreandoUbicaciones"
        Me.lblCreandoUbicaciones.Size = New System.Drawing.Size(464, 37)
        Me.lblCreandoUbicaciones.TabIndex = 0
        Me.lblCreandoUbicaciones.Text = "  Creando ubicaciones . . ."
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.pBar)
        Me.GroupBox1.Controls.Add(Me.lblprg1)
        Me.GroupBox1.Controls.Add(Me.lblPrg)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(579, 28)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(470, 140)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'pBar
        '
        Me.pBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pBar.Location = New System.Drawing.Point(3, 56)
        Me.pBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pBar.Name = "pBar"
        Me.pBar.Size = New System.Drawing.Size(464, 34)
        Me.pBar.TabIndex = 1
        '
        'lblprg1
        '
        Me.lblprg1.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblprg1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblprg1.Location = New System.Drawing.Point(3, 20)
        Me.lblprg1.Name = "lblprg1"
        Me.lblprg1.Size = New System.Drawing.Size(464, 36)
        Me.lblprg1.TabIndex = 0
        Me.lblprg1.Text = "  Generando estructura . . ."
        '
        'lblPrg
        '
        Me.lblPrg.AutoEllipsis = True
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrg.Location = New System.Drawing.Point(3, 94)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(464, 42)
        Me.lblPrg.TabIndex = 2
        Me.lblPrg.Text = "Label1"
        Me.lblPrg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tvData
        '
        Me.tvData.Dock = System.Windows.Forms.DockStyle.Left
        Me.tvData.ImageIndex = 0
        Me.tvData.ImageList = Me.ImageList1
        Me.tvData.Location = New System.Drawing.Point(2, 28)
        Me.tvData.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tvData.Name = "tvData"
        Me.tvData.SelectedImageIndex = 0
        Me.tvData.Size = New System.Drawing.Size(577, 581)
        Me.tvData.TabIndex = 0
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Actualizar"
        Me.BarButtonItem1.Id = 2
        Me.BarButtonItem1.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem1.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem1.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonPageGroup2.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'BarButtonItem8
        '
        Me.BarButtonItem8.Caption = "BarButtonItem8"
        Me.BarButtonItem8.Id = 11
        Me.BarButtonItem8.ImageOptions.Image = CType(resources.GetObject("BarButtonItem8.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem8.Name = "BarButtonItem8"
        '
        'frmEstGeneracion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1051, 804)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.RibbonControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEstGeneracion"
        Me.Ribbon = Me.RibbonControl
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Estructura Generacion"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.pBar2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub

    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnAgregar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnBorrar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnArriba As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnAbajo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnGenerar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarEditItem1 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cboEdit As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemComboBox2 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents BarButtonItem5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup5 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents BarButtonItem8 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tvData As TreeView
    Friend WithEvents lblPrg As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblCreandoUbicaciones As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblprg1 As Label
    Friend WithEvents pBar As ProgressBar
    Friend WithEvents pBar2 As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents BarButtonItem6 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents mnuDisenno As DevExpress.XtraBars.BarButtonItem
End Class
