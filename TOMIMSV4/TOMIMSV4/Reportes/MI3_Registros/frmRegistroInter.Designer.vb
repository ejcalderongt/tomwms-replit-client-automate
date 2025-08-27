<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegistroInter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRegistroInter))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdCancelar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPresentacion = New DevExpress.XtraEditors.TextEdit()
        Me.lblDes = New System.Windows.Forms.Label()
        Me.lblPed = New System.Windows.Forms.Label()
        Me.lblRec = New System.Windows.Forms.Label()
        Me.lblOC = New System.Windows.Forms.Label()
        Me.lblTipoTrans = New System.Windows.Forms.Label()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtNoPedido = New DevExpress.XtraEditors.TextEdit()
        Me.dtRece = New DevExpress.XtraEditors.DateEdit()
        Me.dtVence = New DevExpress.XtraEditors.DateEdit()
        Me.txtPeso = New System.Windows.Forms.NumericUpDown()
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.chkEnviado = New DevExpress.XtraEditors.CheckEdit()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.txtUm = New DevExpress.XtraEditors.TextEdit()
        Me.txtProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtCodigo = New DevExpress.XtraEditors.TextEdit()
        Me.txtLinea = New DevExpress.XtraEditors.TextEdit()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoPedido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtRece.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtRece.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtVence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPeso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEnviado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLinea.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdActualizar, Me.cmdCancelar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 3
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(922, 193)
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
        Me.RibbonPage1.Text = "Registro Interface"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdCancelar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 807)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(922, 30)
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.Label2)
        Me.PanelControl1.Controls.Add(Me.txtPresentacion)
        Me.PanelControl1.Controls.Add(Me.lblDes)
        Me.PanelControl1.Controls.Add(Me.lblPed)
        Me.PanelControl1.Controls.Add(Me.lblRec)
        Me.PanelControl1.Controls.Add(Me.lblOC)
        Me.PanelControl1.Controls.Add(Me.lblTipoTrans)
        Me.PanelControl1.Controls.Add(Me.lblCodigo)
        Me.PanelControl1.Controls.Add(Me.txtNoPedido)
        Me.PanelControl1.Controls.Add(Me.dtRece)
        Me.PanelControl1.Controls.Add(Me.dtVence)
        Me.PanelControl1.Controls.Add(Me.txtPeso)
        Me.PanelControl1.Controls.Add(Me.txtCantidad)
        Me.PanelControl1.Controls.Add(Me.chkEnviado)
        Me.PanelControl1.Controls.Add(Me.txtLote)
        Me.PanelControl1.Controls.Add(Me.txtUm)
        Me.PanelControl1.Controls.Add(Me.txtProducto)
        Me.PanelControl1.Controls.Add(Me.txtCodigo)
        Me.PanelControl1.Controls.Add(Me.txtLinea)
        Me.PanelControl1.Controls.Add(Me.Label20)
        Me.PanelControl1.Controls.Add(Me.Label19)
        Me.PanelControl1.Controls.Add(Me.Label18)
        Me.PanelControl1.Controls.Add(Me.Label17)
        Me.PanelControl1.Controls.Add(Me.Label16)
        Me.PanelControl1.Controls.Add(Me.Label15)
        Me.PanelControl1.Controls.Add(Me.Label14)
        Me.PanelControl1.Controls.Add(Me.Label13)
        Me.PanelControl1.Controls.Add(Me.Label12)
        Me.PanelControl1.Controls.Add(Me.Label11)
        Me.PanelControl1.Controls.Add(Me.Label10)
        Me.PanelControl1.Controls.Add(Me.Label9)
        Me.PanelControl1.Controls.Add(Me.Label8)
        Me.PanelControl1.Controls.Add(Me.Label7)
        Me.PanelControl1.Controls.Add(Me.Label6)
        Me.PanelControl1.Controls.Add(Me.Label5)
        Me.PanelControl1.Controls.Add(Me.Label1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 193)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(922, 614)
        Me.PanelControl1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(79, 294)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 16)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Presentación:"
        '
        'txtPresentacion
        '
        Me.txtPresentacion.Location = New System.Drawing.Point(178, 295)
        Me.txtPresentacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPresentacion.MenuManager = Me.RibbonControl
        Me.txtPresentacion.Name = "txtPresentacion"
        Me.txtPresentacion.Properties.ReadOnly = True
        Me.txtPresentacion.Size = New System.Drawing.Size(194, 22)
        Me.txtPresentacion.TabIndex = 39
        '
        'lblDes
        '
        Me.lblDes.AutoSize = True
        Me.lblDes.Location = New System.Drawing.Point(610, 224)
        Me.lblDes.Name = "lblDes"
        Me.lblDes.Size = New System.Drawing.Size(17, 16)
        Me.lblDes.TabIndex = 38
        Me.lblDes.Text = "--"
        '
        'lblPed
        '
        Me.lblPed.AutoSize = True
        Me.lblPed.Location = New System.Drawing.Point(610, 192)
        Me.lblPed.Name = "lblPed"
        Me.lblPed.Size = New System.Drawing.Size(17, 16)
        Me.lblPed.TabIndex = 37
        Me.lblPed.Text = "--"
        '
        'lblRec
        '
        Me.lblRec.AutoSize = True
        Me.lblRec.Location = New System.Drawing.Point(610, 153)
        Me.lblRec.Name = "lblRec"
        Me.lblRec.Size = New System.Drawing.Size(17, 16)
        Me.lblRec.TabIndex = 36
        Me.lblRec.Text = "--"
        '
        'lblOC
        '
        Me.lblOC.AutoSize = True
        Me.lblOC.Location = New System.Drawing.Point(610, 105)
        Me.lblOC.Name = "lblOC"
        Me.lblOC.Size = New System.Drawing.Size(17, 16)
        Me.lblOC.TabIndex = 35
        Me.lblOC.Text = "--"
        '
        'lblTipoTrans
        '
        Me.lblTipoTrans.AutoSize = True
        Me.lblTipoTrans.Location = New System.Drawing.Point(610, 62)
        Me.lblTipoTrans.Name = "lblTipoTrans"
        Me.lblTipoTrans.Size = New System.Drawing.Size(17, 16)
        Me.lblTipoTrans.TabIndex = 34
        Me.lblTipoTrans.Text = "--"
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Location = New System.Drawing.Point(175, 62)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(22, 16)
        Me.lblCodigo.TabIndex = 33
        Me.lblCodigo.Text = "---"
        '
        'txtNoPedido
        '
        Me.txtNoPedido.Location = New System.Drawing.Point(610, 252)
        Me.txtNoPedido.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNoPedido.MenuManager = Me.RibbonControl
        Me.txtNoPedido.Name = "txtNoPedido"
        Me.txtNoPedido.Properties.ReadOnly = True
        Me.txtNoPedido.Size = New System.Drawing.Size(184, 22)
        Me.txtNoPedido.TabIndex = 32
        '
        'dtRece
        '
        Me.dtRece.EditValue = Nothing
        Me.dtRece.Location = New System.Drawing.Point(610, 332)
        Me.dtRece.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtRece.MenuManager = Me.RibbonControl
        Me.dtRece.Name = "dtRece"
        Me.dtRece.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtRece.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtRece.Properties.ReadOnly = True
        Me.dtRece.Size = New System.Drawing.Size(184, 22)
        Me.dtRece.TabIndex = 31
        '
        'dtVence
        '
        Me.dtVence.EditValue = Nothing
        Me.dtVence.Location = New System.Drawing.Point(178, 407)
        Me.dtVence.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtVence.MenuManager = Me.RibbonControl
        Me.dtVence.Name = "dtVence"
        Me.dtVence.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtVence.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtVence.Properties.ReadOnly = True
        Me.dtVence.Size = New System.Drawing.Size(194, 22)
        Me.dtVence.TabIndex = 30
        '
        'txtPeso
        '
        Me.txtPeso.DecimalPlaces = 6
        Me.txtPeso.Location = New System.Drawing.Point(178, 374)
        Me.txtPeso.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPeso.Maximum = New Decimal(New Integer() {1410065408, 2, 0, 0})
        Me.txtPeso.Name = "txtPeso"
        Me.txtPeso.Size = New System.Drawing.Size(194, 23)
        Me.txtPeso.TabIndex = 29
        '
        'txtCantidad
        '
        Me.txtCantidad.DecimalPlaces = 6
        Me.txtCantidad.Location = New System.Drawing.Point(178, 338)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {1410065408, 2, 0, 0})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(194, 23)
        Me.txtCantidad.TabIndex = 28
        '
        'chkEnviado
        '
        Me.chkEnviado.Location = New System.Drawing.Point(610, 364)
        Me.chkEnviado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkEnviado.MenuManager = Me.RibbonControl
        Me.chkEnviado.Name = "chkEnviado"
        Me.chkEnviado.Properties.Caption = ""
        Me.chkEnviado.Size = New System.Drawing.Size(184, 24)
        Me.chkEnviado.TabIndex = 24
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(610, 295)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLote.MenuManager = Me.RibbonControl
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Size = New System.Drawing.Size(184, 22)
        Me.txtLote.TabIndex = 23
        '
        'txtUm
        '
        Me.txtUm.Location = New System.Drawing.Point(178, 251)
        Me.txtUm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUm.MenuManager = Me.RibbonControl
        Me.txtUm.Name = "txtUm"
        Me.txtUm.Properties.ReadOnly = True
        Me.txtUm.Size = New System.Drawing.Size(194, 22)
        Me.txtUm.TabIndex = 22
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(178, 218)
        Me.txtProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtProducto.MenuManager = Me.RibbonControl
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Properties.ReadOnly = True
        Me.txtProducto.Size = New System.Drawing.Size(194, 22)
        Me.txtProducto.TabIndex = 21
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(178, 181)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCodigo.MenuManager = Me.RibbonControl
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Properties.ReadOnly = True
        Me.txtCodigo.Size = New System.Drawing.Size(194, 22)
        Me.txtCodigo.TabIndex = 20
        '
        'txtLinea
        '
        Me.txtLinea.Location = New System.Drawing.Point(178, 144)
        Me.txtLinea.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLinea.MenuManager = Me.RibbonControl
        Me.txtLinea.Name = "txtLinea"
        Me.txtLinea.Size = New System.Drawing.Size(194, 22)
        Me.txtLinea.TabIndex = 3
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(492, 62)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(106, 16)
        Me.Label20.TabIndex = 19
        Me.Label20.Text = "Tipo transacción:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(492, 369)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(56, 16)
        Me.Label19.TabIndex = 18
        Me.Label19.Text = "Enviado:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(492, 338)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(105, 16)
        Me.Label18.TabIndex = 17
        Me.Label18.Text = "Fecha recepción:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(79, 405)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(85, 16)
        Me.Label17.TabIndex = 16
        Me.Label17.Text = "Fecha Vence:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(492, 299)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(36, 16)
        Me.Label16.TabIndex = 15
        Me.Label16.Text = "Lote:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(79, 369)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(39, 16)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "Peso:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(79, 336)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(62, 16)
        Me.Label14.TabIndex = 13
        Me.Label14.Text = "Cantidad:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(492, 261)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(69, 16)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "No Pedido:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(79, 255)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(96, 16)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Unidad Medida:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(79, 143)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(42, 16)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Línea:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(79, 217)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(62, 16)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Producto:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(79, 180)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 16)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Código:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(492, 224)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 16)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Despacho:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(492, 187)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 16)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Pedido:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(492, 148)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 16)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Recepción:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(492, 105)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(114, 16)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Orden de Compra:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(50, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Código Transacción:"
        '
        'frmRegistroInter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(922, 837)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmRegistroInter"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Registro Interface"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.txtPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoPedido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtRece.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtRece.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtVence.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtVence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPeso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEnviado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLinea.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents chkEnviado As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUm As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtCodigo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtLinea As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPeso As NumericUpDown
    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents txtNoPedido As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dtRece As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtVence As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblPed As Label
    Friend WithEvents lblRec As Label
    Friend WithEvents lblOC As Label
    Friend WithEvents lblTipoTrans As Label
    Friend WithEvents lblCodigo As Label
    Friend WithEvents lblDes As Label
    Friend WithEvents txtPresentacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As Label
End Class
