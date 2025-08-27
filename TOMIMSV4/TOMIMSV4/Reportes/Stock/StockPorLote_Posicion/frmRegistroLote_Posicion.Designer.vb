<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegistroLote_Posicion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRegistroLote_Posicion))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCancelar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.lblIngreso = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblRecepcion = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPresentacion = New DevExpress.XtraEditors.TextEdit()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtEstado = New DevExpress.XtraEditors.TextEdit()
        Me.dtRece = New DevExpress.XtraEditors.DateEdit()
        Me.dtVence = New DevExpress.XtraEditors.DateEdit()
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.chkPalletNoStandar = New DevExpress.XtraEditors.CheckEdit()
        Me.txtUm = New DevExpress.XtraEditors.TextEdit()
        Me.txtProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtRece.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtRece.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtVence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPalletNoStandar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.cmdActualizar, Me.cmdCancelar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 3
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(891, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 1
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Caption = "Cancelar"
        Me.cmdCancelar.Id = 2
        Me.cmdCancelar.ImageOptions.SvgImage = CType(resources.GetObject("cmdCancelar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdCancelar.Name = "cmdCancelar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Posiciones ocupadas y Pallet no Standar"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdCancelar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 608)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(891, 30)
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.lblIngreso)
        Me.PanelControl1.Controls.Add(Me.Label5)
        Me.PanelControl1.Controls.Add(Me.lblRecepcion)
        Me.PanelControl1.Controls.Add(Me.Label3)
        Me.PanelControl1.Controls.Add(Me.Label2)
        Me.PanelControl1.Controls.Add(Me.txtPresentacion)
        Me.PanelControl1.Controls.Add(Me.lblCodigo)
        Me.PanelControl1.Controls.Add(Me.txtEstado)
        Me.PanelControl1.Controls.Add(Me.dtRece)
        Me.PanelControl1.Controls.Add(Me.dtVence)
        Me.PanelControl1.Controls.Add(Me.txtCantidad)
        Me.PanelControl1.Controls.Add(Me.chkPalletNoStandar)
        Me.PanelControl1.Controls.Add(Me.txtUm)
        Me.PanelControl1.Controls.Add(Me.txtProducto)
        Me.PanelControl1.Controls.Add(Me.txtCodigo)
        Me.PanelControl1.Controls.Add(Me.Label19)
        Me.PanelControl1.Controls.Add(Me.Label18)
        Me.PanelControl1.Controls.Add(Me.Label17)
        Me.PanelControl1.Controls.Add(Me.Label14)
        Me.PanelControl1.Controls.Add(Me.Label13)
        Me.PanelControl1.Controls.Add(Me.Label12)
        Me.PanelControl1.Controls.Add(Me.Label10)
        Me.PanelControl1.Controls.Add(Me.Label9)
        Me.PanelControl1.Controls.Add(Me.Label1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 193)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(891, 415)
        Me.PanelControl1.TabIndex = 3
        '
        'lblIngreso
        '
        Me.lblIngreso.AutoSize = True
        Me.lblIngreso.Location = New System.Drawing.Point(344, 61)
        Me.lblIngreso.Name = "lblIngreso"
        Me.lblIngreso.Size = New System.Drawing.Size(23, 17)
        Me.lblIngreso.TabIndex = 44
        Me.lblIngreso.Text = "---"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(255, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 17)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Doc Ingreso:"
        '
        'lblRecepcion
        '
        Me.lblRecepcion.AutoSize = True
        Me.lblRecepcion.Location = New System.Drawing.Point(607, 62)
        Me.lblRecepcion.Name = "lblRecepcion"
        Me.lblRecepcion.Size = New System.Drawing.Size(23, 17)
        Me.lblRecepcion.TabIndex = 42
        Me.lblRecepcion.Text = "---"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(530, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 17)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Recepción:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 258)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 17)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Presentación:"
        '
        'txtPresentacion
        '
        Me.txtPresentacion.Location = New System.Drawing.Point(178, 254)
        Me.txtPresentacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPresentacion.MenuManager = Me.RibbonControl
        Me.txtPresentacion.Name = "txtPresentacion"
        Me.txtPresentacion.Properties.ReadOnly = True
        Me.txtPresentacion.Size = New System.Drawing.Size(194, 22)
        Me.txtPresentacion.TabIndex = 39
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Location = New System.Drawing.Point(175, 62)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(23, 17)
        Me.lblCodigo.TabIndex = 33
        Me.lblCodigo.Text = "---"
        '
        'txtEstado
        '
        Me.txtEstado.Location = New System.Drawing.Point(602, 133)
        Me.txtEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtEstado.MenuManager = Me.RibbonControl
        Me.txtEstado.Name = "txtEstado"
        Me.txtEstado.Properties.ReadOnly = True
        Me.txtEstado.Size = New System.Drawing.Size(194, 22)
        Me.txtEstado.TabIndex = 32
        '
        'dtRece
        '
        Me.dtRece.EditValue = Nothing
        Me.dtRece.Location = New System.Drawing.Point(602, 173)
        Me.dtRece.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtRece.MenuManager = Me.RibbonControl
        Me.dtRece.Name = "dtRece"
        Me.dtRece.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtRece.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtRece.Properties.ReadOnly = True
        Me.dtRece.Size = New System.Drawing.Size(194, 22)
        Me.dtRece.TabIndex = 31
        '
        'dtVence
        '
        Me.dtVence.EditValue = Nothing
        Me.dtVence.Location = New System.Drawing.Point(178, 292)
        Me.dtVence.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtVence.MenuManager = Me.RibbonControl
        Me.dtVence.Name = "dtVence"
        Me.dtVence.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtVence.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtVence.Properties.ReadOnly = True
        Me.dtVence.Size = New System.Drawing.Size(194, 22)
        Me.dtVence.TabIndex = 30
        '
        'txtCantidad
        '
        Me.txtCantidad.DecimalPlaces = 6
        Me.txtCantidad.Location = New System.Drawing.Point(178, 334)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {1410065408, 2, 0, 0})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(194, 23)
        Me.txtCantidad.TabIndex = 28
        '
        'chkPalletNoStandar
        '
        Me.chkPalletNoStandar.Location = New System.Drawing.Point(182, 103)
        Me.chkPalletNoStandar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkPalletNoStandar.MenuManager = Me.RibbonControl
        Me.chkPalletNoStandar.Name = "chkPalletNoStandar"
        Me.chkPalletNoStandar.Properties.Caption = ""
        Me.chkPalletNoStandar.Size = New System.Drawing.Size(24, 24)
        Me.chkPalletNoStandar.TabIndex = 24
        '
        'txtUm
        '
        Me.txtUm.Location = New System.Drawing.Point(178, 214)
        Me.txtUm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUm.MenuManager = Me.RibbonControl
        Me.txtUm.Name = "txtUm"
        Me.txtUm.Properties.ReadOnly = True
        Me.txtUm.Size = New System.Drawing.Size(94, 22)
        Me.txtUm.TabIndex = 22
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(178, 177)
        Me.txtProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtProducto.MenuManager = Me.RibbonControl
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Properties.ReadOnly = True
        Me.txtProducto.Size = New System.Drawing.Size(194, 22)
        Me.txtProducto.TabIndex = 21
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(178, 140)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigo.MenuManager = Me.RibbonControl
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Properties.ReadOnly = True
        Me.txtCodigo.Size = New System.Drawing.Size(194, 22)
        Me.txtCodigo.TabIndex = 20
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(36, 104)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(116, 17)
        Me.Label19.TabIndex = 18
        Me.Label19.Text = "Pallet no Standar:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(497, 176)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(99, 17)
        Me.Label18.TabIndex = 17
        Me.Label18.Text = "Fecha Ingreso:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(36, 295)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(90, 17)
        Me.Label17.TabIndex = 16
        Me.Label17.Text = "Fecha Vence:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(36, 337)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(75, 17)
        Me.Label14.TabIndex = 13
        Me.Label14.Text = "Posiciones:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(541, 136)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(55, 17)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "Estado:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(36, 219)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(101, 17)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Unidad Medida:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(36, 181)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 17)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Producto:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(36, 143)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 17)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Código:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Código Transacción:"
        '
        'frmRegistroLote_Posicion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(891, 638)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmRegistroLote_Posicion"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Actualización de Posiciones y Pallet no standar"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.txtPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtRece.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtRece.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtVence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPalletNoStandar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdCancelar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPresentacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCodigo As Label
    Friend WithEvents txtEstado As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dtRece As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtVence As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents chkPalletNoStandar As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtUm As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label19 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblRecepcion As Label
    Friend WithEvents lblIngreso As Label
    Friend WithEvents Label5 As Label
End Class
