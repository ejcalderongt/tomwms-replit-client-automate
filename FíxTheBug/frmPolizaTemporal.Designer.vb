<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPolizaTemporal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPolizaTemporal))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblPoliza = New DevExpress.XtraEditors.LabelControl()
        Me.txtPoliza = New System.Windows.Forms.TextBox()
        Me.txtDUA = New System.Windows.Forms.TextBox()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNoOrden = New System.Windows.Forms.TextBox()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.dtpFechaAceptacion = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaLlegada = New System.Windows.Forms.DateTimePicker()
        Me.lblFechaAceptacion = New DevExpress.XtraEditors.LabelControl()
        Me.lblFechaLlegada = New DevExpress.XtraEditors.LabelControl()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 2
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(435, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 1
        Me.cmdGuardar.ImageOptions.Image = CType(resources.GetObject("cmdGuardar.ImageOptions.Image"), System.Drawing.Image)
        Me.cmdGuardar.ImageOptions.LargeImage = CType(resources.GetObject("cmdGuardar.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.cmdGuardar.Name = "cmdGuardar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Opciones"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 494)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(435, 30)
        '
        'lblPoliza
        '
        Me.lblPoliza.Location = New System.Drawing.Point(34, 228)
        Me.lblPoliza.Name = "lblPoliza"
        Me.lblPoliza.Size = New System.Drawing.Size(38, 16)
        Me.lblPoliza.TabIndex = 2
        Me.lblPoliza.Text = "Póliza:"
        '
        'txtPoliza
        '
        Me.txtPoliza.Location = New System.Drawing.Point(149, 226)
        Me.txtPoliza.Name = "txtPoliza"
        Me.txtPoliza.Size = New System.Drawing.Size(192, 23)
        Me.txtPoliza.TabIndex = 3
        '
        'txtDUA
        '
        Me.txtDUA.Location = New System.Drawing.Point(149, 264)
        Me.txtDUA.Name = "txtDUA"
        Me.txtDUA.Size = New System.Drawing.Size(192, 23)
        Me.txtDUA.TabIndex = 5
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(34, 266)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(29, 16)
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "DUA:"
        '
        'txtNoOrden
        '
        Me.txtNoOrden.Location = New System.Drawing.Point(149, 305)
        Me.txtNoOrden.Name = "txtNoOrden"
        Me.txtNoOrden.Size = New System.Drawing.Size(192, 23)
        Me.txtNoOrden.TabIndex = 7
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(34, 307)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(63, 16)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "No. Orden:"
        '
        'dtpFechaAceptacion
        '
        Me.dtpFechaAceptacion.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFechaAceptacion.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFechaAceptacion.Location = New System.Drawing.Point(149, 348)
        Me.dtpFechaAceptacion.Name = "dtpFechaAceptacion"
        Me.dtpFechaAceptacion.Size = New System.Drawing.Size(192, 23)
        Me.dtpFechaAceptacion.TabIndex = 8
        '
        'dtpFechaLlegada
        '
        Me.dtpFechaLlegada.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFechaLlegada.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFechaLlegada.Location = New System.Drawing.Point(149, 389)
        Me.dtpFechaLlegada.Name = "dtpFechaLlegada"
        Me.dtpFechaLlegada.Size = New System.Drawing.Size(192, 23)
        Me.dtpFechaLlegada.TabIndex = 9
        '
        'lblFechaAceptacion
        '
        Me.lblFechaAceptacion.Location = New System.Drawing.Point(34, 353)
        Me.lblFechaAceptacion.Name = "lblFechaAceptacion"
        Me.lblFechaAceptacion.Size = New System.Drawing.Size(104, 16)
        Me.lblFechaAceptacion.TabIndex = 10
        Me.lblFechaAceptacion.Text = "Fecha aceptación:"
        '
        'lblFechaLlegada
        '
        Me.lblFechaLlegada.Location = New System.Drawing.Point(34, 391)
        Me.lblFechaLlegada.Name = "lblFechaLlegada"
        Me.lblFechaLlegada.Size = New System.Drawing.Size(84, 16)
        Me.lblFechaLlegada.TabIndex = 11
        Me.lblFechaLlegada.Text = "Fecha llegada:"
        '
        'frmPolizaTemporal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 524)
        Me.Controls.Add(Me.lblFechaLlegada)
        Me.Controls.Add(Me.lblFechaAceptacion)
        Me.Controls.Add(Me.dtpFechaLlegada)
        Me.Controls.Add(Me.dtpFechaAceptacion)
        Me.Controls.Add(Me.txtNoOrden)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.txtDUA)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.txtPoliza)
        Me.Controls.Add(Me.lblPoliza)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmPolizaTemporal"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Póliza  Temporal"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblPoliza As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPoliza As Windows.Forms.TextBox
    Friend WithEvents txtDUA As Windows.Forms.TextBox
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNoOrden As Windows.Forms.TextBox
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpFechaAceptacion As Windows.Forms.DateTimePicker
    Friend WithEvents dtpFechaLlegada As Windows.Forms.DateTimePicker
    Friend WithEvents lblFechaAceptacion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblFechaLlegada As DevExpress.XtraEditors.LabelControl
End Class
