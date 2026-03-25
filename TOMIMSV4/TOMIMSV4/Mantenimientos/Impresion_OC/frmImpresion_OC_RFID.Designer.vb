<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImpresion_OC_RFID
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImpresion_OC_RFID))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.chkSeleccionMultiple = New DevExpress.XtraBars.BarCheckItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.grdListaBarraPallets = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtCantidadImpresiones = New System.Windows.Forms.NumericUpDown()
        Me.lblImpresiones = New DevExpress.XtraEditors.LabelControl()
        Me.cmbImpresora = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmdImpresion = New DevExpress.XtraEditors.SimpleButton()
        Me.chkEstadoImpreso = New DevExpress.XtraBars.BarCheckItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.grdListaBarraPallets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtCantidadImpresiones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbImpresora.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(71)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.chkSeleccionMultiple, Me.chkEstadoImpreso, Me.cmdActualizar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(8)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 805
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1279, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'chkSeleccionMultiple
        '
        Me.chkSeleccionMultiple.Caption = "Selección multiple"
        Me.chkSeleccionMultiple.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkSeleccionMultiple.Hint = "Habilita que registros se imprimen y cuales no."
        Me.chkSeleccionMultiple.Id = 1
        Me.chkSeleccionMultiple.Name = "chkSeleccionMultiple"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Impresion RFID"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.chkSeleccionMultiple)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.chkEstadoImpreso)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 609)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(8)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1279, 30)
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.GroupControl2)
        Me.GroupControl4.Controls.Add(Me.GroupControl1)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(10, 12, 10, 12)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(1279, 416)
        Me.GroupControl4.TabIndex = 3
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.grdListaBarraPallets)
        Me.GroupControl2.Location = New System.Drawing.Point(12, 108)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(983, 286)
        Me.GroupControl2.TabIndex = 43
        Me.GroupControl2.Text = "Lista de impresión"
        '
        'grdListaBarraPallets
        '
        Me.grdListaBarraPallets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdListaBarraPallets.Location = New System.Drawing.Point(2, 28)
        Me.grdListaBarraPallets.MainView = Me.GridView1
        Me.grdListaBarraPallets.MenuManager = Me.RibbonControl
        Me.grdListaBarraPallets.Name = "grdListaBarraPallets"
        Me.grdListaBarraPallets.Size = New System.Drawing.Size(979, 256)
        Me.grdListaBarraPallets.TabIndex = 0
        Me.grdListaBarraPallets.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.grdListaBarraPallets
        Me.GridView1.Name = "GridView1"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtCantidadImpresiones)
        Me.GroupControl1.Controls.Add(Me.lblImpresiones)
        Me.GroupControl1.Controls.Add(Me.cmbImpresora)
        Me.GroupControl1.Controls.Add(Me.cmdImpresion)
        Me.GroupControl1.Location = New System.Drawing.Point(1012, 108)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(8)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(257, 284)
        Me.GroupControl1.TabIndex = 42
        Me.GroupControl1.Text = "Impresión"
        '
        'txtCantidadImpresiones
        '
        Me.txtCantidadImpresiones.Location = New System.Drawing.Point(137, 97)
        Me.txtCantidadImpresiones.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtCantidadImpresiones.Name = "txtCantidadImpresiones"
        Me.txtCantidadImpresiones.Size = New System.Drawing.Size(98, 23)
        Me.txtCantidadImpresiones.TabIndex = 49
        Me.txtCantidadImpresiones.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'lblImpresiones
        '
        Me.lblImpresiones.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblImpresiones.Appearance.Options.UseFont = True
        Me.lblImpresiones.Location = New System.Drawing.Point(23, 99)
        Me.lblImpresiones.Margin = New System.Windows.Forms.Padding(4)
        Me.lblImpresiones.Name = "lblImpresiones"
        Me.lblImpresiones.Size = New System.Drawing.Size(72, 16)
        Me.lblImpresiones.TabIndex = 48
        Me.lblImpresiones.Text = "# de copias:"
        '
        'cmbImpresora
        '
        Me.cmbImpresora.Location = New System.Drawing.Point(23, 45)
        Me.cmbImpresora.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbImpresora.Name = "cmbImpresora"
        Me.cmbImpresora.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.cmbImpresora.Properties.Appearance.Options.UseFont = True
        Me.cmbImpresora.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbImpresora.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbImpresora.Properties.NullText = ""
        Me.cmbImpresora.Size = New System.Drawing.Size(212, 28)
        Me.cmbImpresora.TabIndex = 47
        '
        'cmdImpresion
        '
        Me.cmdImpresion.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdImpresion.Appearance.Options.UseFont = True
        Me.cmdImpresion.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.cmdImpresion.ImageOptions.SvgImage = CType(resources.GetObject("cmdImpresion.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdImpresion.Location = New System.Drawing.Point(23, 192)
        Me.cmdImpresion.Margin = New System.Windows.Forms.Padding(10)
        Me.cmdImpresion.Name = "cmdImpresion"
        Me.cmdImpresion.Size = New System.Drawing.Size(212, 80)
        Me.cmdImpresion.TabIndex = 46
        Me.cmdImpresion.Text = "Impresión TAG"
        '
        'chkEstadoImpreso
        '
        Me.chkEstadoImpreso.Caption = "ReImpresión"
        Me.chkEstadoImpreso.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.chkEstadoImpreso.Hint = "Mostrar registros previamente impresos"
        Me.chkEstadoImpreso.Id = 2
        Me.chkEstadoImpreso.Name = "chkEstadoImpreso"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 3
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'frmImpresion_OC_RFID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1279, 639)
        Me.Controls.Add(Me.GroupControl4)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmImpresion_OC_RFID"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Impresión Barras Pallets RFID"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.grdListaBarraPallets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtCantidadImpresiones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbImpresora.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdImpresion As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdListaBarraPallets As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cmbImpresora As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblImpresiones As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCantidadImpresiones As NumericUpDown
    Friend WithEvents chkSeleccionMultiple As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents chkEstadoImpreso As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
End Class
