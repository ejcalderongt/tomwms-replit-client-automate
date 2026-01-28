<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCantidadreemplazo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCantidadreemplazo))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuAplicarReemplazo = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuBuscarProductosReemplazo = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtNombreEstado = New DevExpress.XtraEditors.TextEdit()
        Me.lnkEstadoPorDefecto = New System.Windows.Forms.LinkLabel()
        Me.txtIdEstadoDefectoRecepcion = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.lnkUbicacion = New System.Windows.Forms.LinkLabel()
        Me.txtIdUbicacion = New DevExpress.XtraEditors.TextEdit()
        Me.lblNoStock = New System.Windows.Forms.RichTextBox()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCantidadSolicitada = New System.Windows.Forms.NumericUpDown()
        Me.lblIdStock = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtUMBas = New DevExpress.XtraEditors.TextEdit()
        Me.txtPresentacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtNombreProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdProducto = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblCantidadReemplazo = New DevExpress.XtraEditors.LabelControl()
        Me.txtCantidadReemplazo = New System.Windows.Forms.NumericUpDown()
        Me.dgridPickingUbic = New DevExpress.XtraGrid.GridControl()
        Me.grdvPickingUbic = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel1.SuspendLayout()
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.Panel2.SuspendLayout()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtNombreEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdEstadoDefectoRecepcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.txtCantidadSolicitada, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUMBas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidadReemplazo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridPickingUbic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvPickingUbic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuAplicarReemplazo, Me.mnuBuscarProductosReemplazo})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 3
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1455, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuAplicarReemplazo
        '
        Me.mnuAplicarReemplazo.Caption = "Aplicar"
        Me.mnuAplicarReemplazo.Id = 1
        Me.mnuAplicarReemplazo.ImageOptions.SvgImage = CType(resources.GetObject("mnuAplicarReemplazo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuAplicarReemplazo.Name = "mnuAplicarReemplazo"
        '
        'mnuBuscarProductosReemplazo
        '
        Me.mnuBuscarProductosReemplazo.Caption = "Buscar"
        Me.mnuBuscarProductosReemplazo.Id = 2
        Me.mnuBuscarProductosReemplazo.ImageOptions.SvgImage = CType(resources.GetObject("mnuBuscarProductosReemplazo.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuBuscarProductosReemplazo.Name = "mnuBuscarProductosReemplazo"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuBuscarProductosReemplazo)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuAplicarReemplazo)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 735)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1455, 30)
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 193)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        '
        'SplitContainerControl1.Panel1
        '
        Me.SplitContainerControl1.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GroupControl1)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.lblNoStock)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GroupControl2)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        '
        'SplitContainerControl1.Panel2
        '
        Me.SplitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.dgridPickingUbic)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1455, 542)
        Me.SplitContainerControl1.SplitterPosition = 461
        Me.SplitContainerControl1.TabIndex = 2
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.txtNombreEstado)
        Me.GroupControl1.Controls.Add(Me.lnkEstadoPorDefecto)
        Me.GroupControl1.Controls.Add(Me.txtIdEstadoDefectoRecepcion)
        Me.GroupControl1.Controls.Add(Me.txtNombreUbicacion)
        Me.GroupControl1.Controls.Add(Me.lnkUbicacion)
        Me.GroupControl1.Controls.Add(Me.txtIdUbicacion)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 294)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(457, 160)
        Me.GroupControl1.TabIndex = 14
        Me.GroupControl1.Text = "Estado - Ubicación (Destino)"
        '
        'txtNombreEstado
        '
        Me.txtNombreEstado.Location = New System.Drawing.Point(128, 63)
        Me.txtNombreEstado.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombreEstado.MenuManager = Me.RibbonControl
        Me.txtNombreEstado.Name = "txtNombreEstado"
        Me.txtNombreEstado.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombreEstado.Properties.Appearance.Options.UseFont = True
        Me.txtNombreEstado.Properties.MaskSettings.Set("mask", "n0")
        Me.txtNombreEstado.Properties.ReadOnly = True
        Me.txtNombreEstado.Size = New System.Drawing.Size(304, 30)
        Me.txtNombreEstado.TabIndex = 13
        '
        'lnkEstadoPorDefecto
        '
        Me.lnkEstadoPorDefecto.AutoSize = True
        Me.lnkEstadoPorDefecto.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkEstadoPorDefecto.Location = New System.Drawing.Point(19, 35)
        Me.lnkEstadoPorDefecto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkEstadoPorDefecto.Name = "lnkEstadoPorDefecto"
        Me.lnkEstadoPorDefecto.Size = New System.Drawing.Size(70, 24)
        Me.lnkEstadoPorDefecto.TabIndex = 11
        Me.lnkEstadoPorDefecto.TabStop = True
        Me.lnkEstadoPorDefecto.Text = "Estado"
        '
        'txtIdEstadoDefectoRecepcion
        '
        Me.txtIdEstadoDefectoRecepcion.Location = New System.Drawing.Point(19, 63)
        Me.txtIdEstadoDefectoRecepcion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdEstadoDefectoRecepcion.MenuManager = Me.RibbonControl
        Me.txtIdEstadoDefectoRecepcion.Name = "txtIdEstadoDefectoRecepcion"
        Me.txtIdEstadoDefectoRecepcion.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdEstadoDefectoRecepcion.Properties.Appearance.Options.UseFont = True
        Me.txtIdEstadoDefectoRecepcion.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdEstadoDefectoRecepcion.Size = New System.Drawing.Size(104, 30)
        Me.txtIdEstadoDefectoRecepcion.TabIndex = 12
        '
        'txtNombreUbicacion
        '
        Me.txtNombreUbicacion.Location = New System.Drawing.Point(128, 124)
        Me.txtNombreUbicacion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombreUbicacion.MenuManager = Me.RibbonControl
        Me.txtNombreUbicacion.Name = "txtNombreUbicacion"
        Me.txtNombreUbicacion.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombreUbicacion.Properties.Appearance.Options.UseFont = True
        Me.txtNombreUbicacion.Properties.MaskSettings.Set("mask", "n0")
        Me.txtNombreUbicacion.Properties.ReadOnly = True
        Me.txtNombreUbicacion.Size = New System.Drawing.Size(304, 30)
        Me.txtNombreUbicacion.TabIndex = 10
        '
        'lnkUbicacion
        '
        Me.lnkUbicacion.AutoSize = True
        Me.lnkUbicacion.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkUbicacion.Location = New System.Drawing.Point(19, 96)
        Me.lnkUbicacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkUbicacion.Name = "lnkUbicacion"
        Me.lnkUbicacion.Size = New System.Drawing.Size(95, 24)
        Me.lnkUbicacion.TabIndex = 8
        Me.lnkUbicacion.TabStop = True
        Me.lnkUbicacion.Text = "Ubicación"
        '
        'txtIdUbicacion
        '
        Me.txtIdUbicacion.Location = New System.Drawing.Point(19, 124)
        Me.txtIdUbicacion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUbicacion.MenuManager = Me.RibbonControl
        Me.txtIdUbicacion.Name = "txtIdUbicacion"
        Me.txtIdUbicacion.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdUbicacion.Properties.Appearance.Options.UseFont = True
        Me.txtIdUbicacion.Properties.MaskSettings.Set("mask", "n0")
        Me.txtIdUbicacion.Size = New System.Drawing.Size(104, 30)
        Me.txtIdUbicacion.TabIndex = 9
        '
        'lblNoStock
        '
        Me.lblNoStock.BackColor = System.Drawing.Color.MistyRose
        Me.lblNoStock.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblNoStock.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoStock.Location = New System.Drawing.Point(0, 489)
        Me.lblNoStock.Margin = New System.Windows.Forms.Padding(4)
        Me.lblNoStock.Name = "lblNoStock"
        Me.lblNoStock.Size = New System.Drawing.Size(457, 49)
        Me.lblNoStock.TabIndex = 13
        Me.lblNoStock.Text = "No hay más stock disponible para el artículo"
        Me.lblNoStock.Visible = False
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.LabelControl4)
        Me.GroupControl2.Controls.Add(Me.txtCantidadSolicitada)
        Me.GroupControl2.Controls.Add(Me.lblIdStock)
        Me.GroupControl2.Controls.Add(Me.LabelControl3)
        Me.GroupControl2.Controls.Add(Me.LabelControl2)
        Me.GroupControl2.Controls.Add(Me.txtUMBas)
        Me.GroupControl2.Controls.Add(Me.txtPresentacion)
        Me.GroupControl2.Controls.Add(Me.txtNombreProducto)
        Me.GroupControl2.Controls.Add(Me.txtIdProducto)
        Me.GroupControl2.Controls.Add(Me.LabelControl1)
        Me.GroupControl2.Controls.Add(Me.lblCantidadReemplazo)
        Me.GroupControl2.Controls.Add(Me.txtCantidadReemplazo)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(457, 294)
        Me.GroupControl2.TabIndex = 15
        Me.GroupControl2.Text = "Producto"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(16, 142)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(176, 24)
        Me.LabelControl4.TabIndex = 18
        Me.LabelControl4.Text = "Cantidad solicitada:"
        '
        'txtCantidadSolicitada
        '
        Me.txtCantidadSolicitada.DecimalPlaces = 2
        Me.txtCantidadSolicitada.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadSolicitada.Location = New System.Drawing.Point(216, 142)
        Me.txtCantidadSolicitada.Name = "txtCantidadSolicitada"
        Me.txtCantidadSolicitada.ReadOnly = True
        Me.txtCantidadSolicitada.Size = New System.Drawing.Size(216, 32)
        Me.txtCantidadSolicitada.TabIndex = 19
        '
        'lblIdStock
        '
        Me.lblIdStock.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIdStock.Appearance.Options.UseFont = True
        Me.lblIdStock.Location = New System.Drawing.Point(251, 43)
        Me.lblIdStock.Name = "lblIdStock"
        Me.lblIdStock.Size = New System.Drawing.Size(90, 24)
        Me.lblIdStock.TabIndex = 17
        Me.lblIdStock.Text = "IdStock: 0"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(16, 260)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(114, 24)
        Me.LabelControl3.TabIndex = 16
        Me.LabelControl3.Text = "Presentación"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(16, 220)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(66, 24)
        Me.LabelControl2.TabIndex = 15
        Me.LabelControl2.Text = "UM Bas"
        '
        'txtUMBas
        '
        Me.txtUMBas.Location = New System.Drawing.Point(216, 219)
        Me.txtUMBas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUMBas.MenuManager = Me.RibbonControl
        Me.txtUMBas.Name = "txtUMBas"
        Me.txtUMBas.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUMBas.Properties.Appearance.Options.UseFont = True
        Me.txtUMBas.Properties.ReadOnly = True
        Me.txtUMBas.Size = New System.Drawing.Size(216, 30)
        Me.txtUMBas.TabIndex = 14
        '
        'txtPresentacion
        '
        Me.txtPresentacion.Location = New System.Drawing.Point(216, 257)
        Me.txtPresentacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPresentacion.MenuManager = Me.RibbonControl
        Me.txtPresentacion.Name = "txtPresentacion"
        Me.txtPresentacion.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresentacion.Properties.Appearance.Options.UseFont = True
        Me.txtPresentacion.Properties.ReadOnly = True
        Me.txtPresentacion.Size = New System.Drawing.Size(216, 30)
        Me.txtPresentacion.TabIndex = 13
        '
        'txtNombreProducto
        '
        Me.txtNombreProducto.Location = New System.Drawing.Point(16, 107)
        Me.txtNombreProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreProducto.MenuManager = Me.RibbonControl
        Me.txtNombreProducto.Name = "txtNombreProducto"
        Me.txtNombreProducto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombreProducto.Properties.Appearance.Options.UseFont = True
        Me.txtNombreProducto.Properties.ReadOnly = True
        Me.txtNombreProducto.Size = New System.Drawing.Size(416, 30)
        Me.txtNombreProducto.TabIndex = 11
        '
        'txtIdProducto
        '
        Me.txtIdProducto.Location = New System.Drawing.Point(16, 72)
        Me.txtIdProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdProducto.MenuManager = Me.RibbonControl
        Me.txtIdProducto.Name = "txtIdProducto"
        Me.txtIdProducto.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdProducto.Properties.Appearance.Options.UseFont = True
        Me.txtIdProducto.Properties.ReadOnly = True
        Me.txtIdProducto.Size = New System.Drawing.Size(416, 30)
        Me.txtIdProducto.TabIndex = 12
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(19, 43)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(61, 24)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Código"
        '
        'lblCantidadReemplazo
        '
        Me.lblCantidadReemplazo.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCantidadReemplazo.Appearance.Options.UseFont = True
        Me.lblCantidadReemplazo.Location = New System.Drawing.Point(16, 180)
        Me.lblCantidadReemplazo.Name = "lblCantidadReemplazo"
        Me.lblCantidadReemplazo.Size = New System.Drawing.Size(185, 24)
        Me.lblCantidadReemplazo.TabIndex = 0
        Me.lblCantidadReemplazo.Text = "Cantidad reemplazo:"
        '
        'txtCantidadReemplazo
        '
        Me.txtCantidadReemplazo.DecimalPlaces = 2
        Me.txtCantidadReemplazo.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidadReemplazo.Location = New System.Drawing.Point(216, 180)
        Me.txtCantidadReemplazo.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtCantidadReemplazo.Name = "txtCantidadReemplazo"
        Me.txtCantidadReemplazo.Size = New System.Drawing.Size(216, 32)
        Me.txtCantidadReemplazo.TabIndex = 0
        Me.txtCantidadReemplazo.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'dgridPickingUbic
        '
        Me.dgridPickingUbic.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgridPickingUbic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgridPickingUbic.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.dgridPickingUbic.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.dgridPickingUbic.Location = New System.Drawing.Point(0, 0)
        Me.dgridPickingUbic.MainView = Me.grdvPickingUbic
        Me.dgridPickingUbic.Margin = New System.Windows.Forms.Padding(4)
        Me.dgridPickingUbic.MenuManager = Me.RibbonControl
        Me.dgridPickingUbic.Name = "dgridPickingUbic"
        Me.dgridPickingUbic.Size = New System.Drawing.Size(978, 538)
        Me.dgridPickingUbic.TabIndex = 2
        Me.dgridPickingUbic.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdvPickingUbic})
        '
        'grdvPickingUbic
        '
        Me.grdvPickingUbic.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdvPickingUbic.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdvPickingUbic.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdvPickingUbic.Appearance.Row.Options.UseFont = True
        Me.grdvPickingUbic.DetailHeight = 431
        GridFormatRule1.Name = "Format0"
        GridFormatRule1.Rule = Nothing
        Me.grdvPickingUbic.FormatRules.Add(GridFormatRule1)
        Me.grdvPickingUbic.GridControl = Me.dgridPickingUbic
        Me.grdvPickingUbic.Name = "grdvPickingUbic"
        Me.grdvPickingUbic.OptionsBehavior.Editable = False
        Me.grdvPickingUbic.OptionsView.ColumnAutoWidth = False
        Me.grdvPickingUbic.OptionsView.ShowAutoFilterRow = True
        Me.grdvPickingUbic.OptionsView.ShowFooter = True
        '
        'frmCantidadreemplazo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1455, 765)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmCantidadreemplazo"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Cantidad Reemplazo"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerControl1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtNombreEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdEstadoDefectoRecepcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.txtCantidadSolicitada, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUMBas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidadReemplazo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridPickingUbic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvPickingUbic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuAplicarReemplazo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents lblCantidadReemplazo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCantidadReemplazo As NumericUpDown
    Friend WithEvents dgridPickingUbic As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdvPickingUbic As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents mnuBuscarProductosReemplazo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNombreProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblNoStock As RichTextBox
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNombreEstado As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkEstadoPorDefecto As LinkLabel
    Friend WithEvents txtIdEstadoDefectoRecepcion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNombreUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkUbicacion As LinkLabel
    Friend WithEvents txtIdUbicacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtUMBas As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPresentacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblIdStock As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCantidadSolicitada As NumericUpDown
End Class
