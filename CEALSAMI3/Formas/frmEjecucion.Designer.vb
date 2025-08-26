<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEjecucion
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEjecucion))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuBodegas = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizarProveedores = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAcuerdos = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimir = New DevExpress.XtraBars.BarSubItem()
        Me.mnuImprimirGrid1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuImprimirgrid2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuReporteEjecuciones = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarDatos = New DevExpress.XtraBars.BarSubItem()
        Me.mnuEnviarPedidosCompra = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEnviarPedidosTransferencia = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdRptTransac = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuProductosI = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.rgpInterfaceEjecucion = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblTLog = New DevExpress.XtraEditors.LabelControl()
        Me.timerProducto = New System.Windows.Forms.Timer(Me.components)
        Me.BwProducto = New System.ComponentModel.BackgroundWorker()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuBodegas, Me.mnuActualizarProveedores, Me.mnuAcuerdos, Me.BarButtonItem2, Me.mnuImprimir, Me.mnuImprimirGrid1, Me.mnuImprimirgrid2, Me.mnuReporteEjecuciones, Me.mnuEnviarDatos, Me.mnuEnviarPedidosCompra, Me.mnuEnviarPedidosTransferencia, Me.cmdRptTransac, Me.mnuProductosI, Me.BarButtonItem1})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 41
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1444, 193)
        '
        'mnuBodegas
        '
        Me.mnuBodegas.Caption = "Bodegas"
        Me.mnuBodegas.Id = 1
        Me.mnuBodegas.ImageOptions.SvgImage = CType(resources.GetObject("mnuBodegas.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuBodegas.Name = "mnuBodegas"
        Me.mnuBodegas.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuActualizarProveedores
        '
        Me.mnuActualizarProveedores.Caption = "Propietarios/Clientes"
        Me.mnuActualizarProveedores.Id = 2
        Me.mnuActualizarProveedores.ImageOptions.Image = CType(resources.GetObject("mnuActualizarProveedores.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuActualizarProveedores.ImageOptions.LargeImage = CType(resources.GetObject("mnuActualizarProveedores.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuActualizarProveedores.Name = "mnuActualizarProveedores"
        '
        'mnuAcuerdos
        '
        Me.mnuAcuerdos.Caption = "Acuerdos Comerciales"
        Me.mnuAcuerdos.Id = 5
        Me.mnuAcuerdos.ImageOptions.SvgImage = CType(resources.GetObject("mnuAcuerdos.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuAcuerdos.Name = "mnuAcuerdos"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.ActAsDropDown = True
        Me.BarButtonItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.BarButtonItem2.Caption = "BarButtonItem2"
        Me.BarButtonItem2.Id = 10
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'mnuImprimir
        '
        Me.mnuImprimir.Caption = "Imprimir"
        Me.mnuImprimir.Id = 18
        Me.mnuImprimir.ImageOptions.SvgImage = CType(resources.GetObject("mnuImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuImprimir.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImprimirGrid1), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuImprimirgrid2)})
        Me.mnuImprimir.Name = "mnuImprimir"
        '
        'mnuImprimirGrid1
        '
        Me.mnuImprimirGrid1.Caption = "Imprimir_Grid_1"
        Me.mnuImprimirGrid1.Id = 19
        Me.mnuImprimirGrid1.Name = "mnuImprimirGrid1"
        '
        'mnuImprimirgrid2
        '
        Me.mnuImprimirgrid2.Caption = "Imprimir_Grid_2"
        Me.mnuImprimirgrid2.Id = 20
        Me.mnuImprimirgrid2.Name = "mnuImprimirgrid2"
        '
        'mnuReporteEjecuciones
        '
        Me.mnuReporteEjecuciones.Caption = "Reporte de Ejecuciones"
        Me.mnuReporteEjecuciones.Id = 21
        Me.mnuReporteEjecuciones.ImageOptions.Image = CType(resources.GetObject("mnuReporteEjecuciones.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuReporteEjecuciones.ImageOptions.LargeImage = CType(resources.GetObject("mnuReporteEjecuciones.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuReporteEjecuciones.Name = "mnuReporteEjecuciones"
        '
        'mnuEnviarDatos
        '
        Me.mnuEnviarDatos.Caption = "Enviar datos a ERP"
        Me.mnuEnviarDatos.Id = 24
        Me.mnuEnviarDatos.ImageOptions.SvgImage = CType(resources.GetObject("mnuEnviarDatos.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEnviarDatos.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEnviarPedidosCompra), New DevExpress.XtraBars.LinkPersistInfo(Me.mnuEnviarPedidosTransferencia), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem1)})
        Me.mnuEnviarDatos.Name = "mnuEnviarDatos"
        Me.mnuEnviarDatos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuEnviarPedidosCompra
        '
        Me.mnuEnviarPedidosCompra.Caption = "Pedidos de compra"
        Me.mnuEnviarPedidosCompra.Id = 25
        Me.mnuEnviarPedidosCompra.Name = "mnuEnviarPedidosCompra"
        '
        'mnuEnviarPedidosTransferencia
        '
        Me.mnuEnviarPedidosTransferencia.Caption = "Pedidos de transferencia"
        Me.mnuEnviarPedidosTransferencia.Id = 26
        Me.mnuEnviarPedidosTransferencia.Name = "mnuEnviarPedidosTransferencia"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Ajustes"
        Me.BarButtonItem1.Id = 34
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'cmdRptTransac
        '
        Me.cmdRptTransac.Caption = "Datos MI3"
        Me.cmdRptTransac.Id = 32
        Me.cmdRptTransac.ImageOptions.Image = CType(resources.GetObject("cmdRptTransac.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdRptTransac.ImageOptions.LargeImage = CType(resources.GetObject("cmdRptTransac.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdRptTransac.Name = "cmdRptTransac"
        Me.cmdRptTransac.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuProductosI
        '
        Me.mnuProductosI.Caption = "Productos ERP"
        Me.mnuProductosI.Id = 33
        Me.mnuProductosI.ImageOptions.Image = CType(resources.GetObject("mnuProductosI.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuProductosI.ImageOptions.LargeImage = CType(resources.GetObject("mnuProductosI.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuProductosI.Name = "mnuProductosI"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rgpInterfaceEjecucion, Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Interface"
        '
        'rgpInterfaceEjecucion
        '
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.mnuBodegas)
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.mnuActualizarProveedores)
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.mnuProductosI)
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.mnuAcuerdos)
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.mnuImprimir)
        Me.rgpInterfaceEjecucion.ItemLinks.Add(Me.mnuEnviarDatos)
        Me.rgpInterfaceEjecucion.Name = "rgpInterfaceEjecucion"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuReporteEjecuciones)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdRptTransac)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'lblprg
        '
        Me.lblprg.BackColor = System.Drawing.Color.OldLace
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblprg.Location = New System.Drawing.Point(0, 252)
        Me.lblprg.Margin = New System.Windows.Forms.Padding(4)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.Size = New System.Drawing.Size(1444, 502)
        Me.lblprg.TabIndex = 2
        Me.lblprg.Text = ""
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(0, 223)
        Me.prg.Margin = New System.Windows.Forms.Padding(4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(1444, 29)
        Me.prg.TabIndex = 1
        Me.prg.Visible = False
        '
        'lblTLog
        '
        Me.lblTLog.Appearance.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTLog.Appearance.Options.UseFont = True
        Me.lblTLog.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblTLog.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblTLog.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTLog.Location = New System.Drawing.Point(0, 193)
        Me.lblTLog.Margin = New System.Windows.Forms.Padding(4)
        Me.lblTLog.Name = "lblTLog"
        Me.lblTLog.Size = New System.Drawing.Size(1444, 30)
        Me.lblTLog.TabIndex = 0
        Me.lblTLog.Text = "Log"
        '
        'timerProducto
        '
        Me.timerProducto.Interval = 1000
        '
        'BwProducto
        '
        Me.BwProducto.WorkerReportsProgress = True
        Me.BwProducto.WorkerSupportsCancellation = True
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Entrada Mercancía"
        Me.BarButtonItem3.Id = 35
        Me.BarButtonItem3.ImageOptions.Image = CType(resources.GetObject("BarButtonItem3.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem3.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem3.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'frmEjecucion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1444, 754)
        Me.Controls.Add(Me.lblprg)
        Me.Controls.Add(Me.prg)
        Me.Controls.Add(Me.lblTLog)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmEjecucion"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MI3_SYNC ->TOM, WMS (vrs 20200122)"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents mnuBodegas As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizarProveedores As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAcuerdos As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents rgpInterfaceEjecucion As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblTLog As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblprg As RichTextBox
    Friend WithEvents mnuImprimir As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuImprimirGrid1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuImprimirgrid2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents prg As ProgressBar
    Friend WithEvents BwProducto As System.ComponentModel.BackgroundWorker
    Friend WithEvents timerProducto As Timer
    Friend WithEvents mnuReporteEjecuciones As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarDatos As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mnuEnviarPedidosCompra As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEnviarPedidosTransferencia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents cmdRptTransac As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuProductosI As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
End Class
