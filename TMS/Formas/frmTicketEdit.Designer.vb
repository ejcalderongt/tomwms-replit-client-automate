<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTicketEdit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTicketEdit))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.txtNoTicket = New DevExpress.XtraEditors.TextEdit()
        Me.lblTicketNo = New DevExpress.XtraEditors.LabelControl()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblNuevaFechaIngreso = New DevExpress.XtraEditors.LabelControl()
        Me.dtpNuevaFechaIngreso = New DevExpress.XtraEditors.DateTimeOffsetEdit()
        Me.dtpFechaIngreso = New DevExpress.XtraEditors.DateTimeOffsetEdit()
        Me.lblFechaIngresoActual = New DevExpress.XtraEditors.LabelControl()
        Me.mnuReimprimirTicket = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoTicket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dtpNuevaFechaIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.BarButtonItem1, Me.mnuReimprimirTicket})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 3
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(907, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Actualizar"
        Me.BarButtonItem1.Id = 1
        Me.BarButtonItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menú edición ticket"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuReimprimirTicket)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 629)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(907, 30)
        '
        'txtNoTicket
        '
        Me.txtNoTicket.EditValue = "0000000001"
        Me.txtNoTicket.Location = New System.Drawing.Point(303, 42)
        Me.txtNoTicket.MenuManager = Me.RibbonControl
        Me.txtNoTicket.Name = "txtNoTicket"
        Me.txtNoTicket.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoTicket.Properties.Appearance.Options.UseFont = True
        Me.txtNoTicket.Properties.Appearance.Options.UseTextOptions = True
        Me.txtNoTicket.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtNoTicket.Properties.ReadOnly = True
        Me.txtNoTicket.Size = New System.Drawing.Size(300, 40)
        Me.txtNoTicket.TabIndex = 3
        '
        'lblTicketNo
        '
        Me.lblTicketNo.Appearance.Font = New System.Drawing.Font("Tahoma", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTicketNo.Appearance.Options.UseFont = True
        Me.lblTicketNo.Location = New System.Drawing.Point(154, 42)
        Me.lblTicketNo.Name = "lblTicketNo"
        Me.lblTicketNo.Size = New System.Drawing.Size(149, 33)
        Me.lblTicketNo.TabIndex = 2
        Me.lblTicketNo.Text = "Ticket No: #"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 193)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtNoTicket)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTicketNo)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblNuevaFechaIngreso)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dtpNuevaFechaIngreso)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dtpFechaIngreso)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblFechaIngresoActual)
        Me.SplitContainer1.Size = New System.Drawing.Size(907, 436)
        Me.SplitContainer1.SplitterDistance = 160
        Me.SplitContainer1.TabIndex = 4
        '
        'lblNuevaFechaIngreso
        '
        Me.lblNuevaFechaIngreso.Appearance.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNuevaFechaIngreso.Appearance.Options.UseFont = True
        Me.lblNuevaFechaIngreso.Location = New System.Drawing.Point(24, 127)
        Me.lblNuevaFechaIngreso.Name = "lblNuevaFechaIngreso"
        Me.lblNuevaFechaIngreso.Size = New System.Drawing.Size(227, 48)
        Me.lblNuevaFechaIngreso.TabIndex = 4
        Me.lblNuevaFechaIngreso.Text = "Nueva Fecha"
        '
        'dtpNuevaFechaIngreso
        '
        Me.dtpNuevaFechaIngreso.EditValue = New System.DateTimeOffset(2022, 5, 19, 10, 29, 0, 0, System.TimeSpan.Parse("-06:00:00"))
        Me.dtpNuevaFechaIngreso.Location = New System.Drawing.Point(303, 124)
        Me.dtpNuevaFechaIngreso.MenuManager = Me.RibbonControl
        Me.dtpNuevaFechaIngreso.Name = "dtpNuevaFechaIngreso"
        Me.dtpNuevaFechaIngreso.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 24.0!)
        Me.dtpNuevaFechaIngreso.Properties.Appearance.Options.UseFont = True
        Me.dtpNuevaFechaIngreso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpNuevaFechaIngreso.Properties.MaskSettings.Set("mask", "d/MM/yyyy HH:mm:ss")
        Me.dtpNuevaFechaIngreso.Size = New System.Drawing.Size(569, 54)
        Me.dtpNuevaFechaIngreso.TabIndex = 3
        '
        'dtpFechaIngreso
        '
        Me.dtpFechaIngreso.EditValue = Nothing
        Me.dtpFechaIngreso.Location = New System.Drawing.Point(303, 33)
        Me.dtpFechaIngreso.MenuManager = Me.RibbonControl
        Me.dtpFechaIngreso.Name = "dtpFechaIngreso"
        Me.dtpFechaIngreso.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaIngreso.Properties.Appearance.Options.UseFont = True
        Me.dtpFechaIngreso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpFechaIngreso.Properties.MaskSettings.Set("mask", "d/MM/yyyy HH:mm:ss")
        Me.dtpFechaIngreso.Properties.ReadOnly = True
        Me.dtpFechaIngreso.Size = New System.Drawing.Size(569, 54)
        Me.dtpFechaIngreso.TabIndex = 1
        '
        'lblFechaIngresoActual
        '
        Me.lblFechaIngresoActual.Appearance.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaIngresoActual.Appearance.Options.UseFont = True
        Me.lblFechaIngresoActual.Location = New System.Drawing.Point(24, 36)
        Me.lblFechaIngresoActual.Name = "lblFechaIngresoActual"
        Me.lblFechaIngresoActual.Size = New System.Drawing.Size(250, 48)
        Me.lblFechaIngresoActual.TabIndex = 0
        Me.lblFechaIngresoActual.Text = "Fecha Ingreso"
        '
        'mnuReimprimirTicket
        '
        Me.mnuReimprimirTicket.Caption = "Reimprimir"
        Me.mnuReimprimirTicket.Id = 2
        Me.mnuReimprimirTicket.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem2.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuReimprimirTicket.Name = "mnuReimprimirTicket"
        '
        'frmTicketEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(907, 659)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmTicketEdit"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Edición de ticket"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoTicket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dtpNuevaFechaIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtNoTicket As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblTicketNo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents dtpFechaIngreso As DevExpress.XtraEditors.DateTimeOffsetEdit
    Friend WithEvents lblFechaIngresoActual As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNuevaFechaIngreso As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpNuevaFechaIngreso As DevExpress.XtraEditors.DateTimeOffsetEdit
    Friend WithEvents mnuReimprimirTicket As DevExpress.XtraBars.BarButtonItem
End Class
