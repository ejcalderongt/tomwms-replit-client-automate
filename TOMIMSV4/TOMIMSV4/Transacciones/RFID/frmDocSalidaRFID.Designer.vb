<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDocSalidaRFID
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
        Dim lblFechaIngresoTMS As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.lblRegs = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.txtIdRFIDEnc = New System.Windows.Forms.TextBox()
        Me.txtIdPedido = New System.Windows.Forms.TextBox()
        Me.txtEstado = New System.Windows.Forms.TextBox()
        Me.txtTipo = New System.Windows.Forms.TextBox()
        Me.txtFechaAgr = New System.Windows.Forms.TextBox()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.txtCliente = New System.Windows.Forms.TextBox()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetalle = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        lblFechaIngresoTMS = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.grdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblFechaIngresoTMS
        '
        lblFechaIngresoTMS.AutoSize = True
        lblFechaIngresoTMS.Location = New System.Drawing.Point(30, 42)
        lblFechaIngresoTMS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblFechaIngresoTMS.Name = "lblFechaIngresoTMS"
        lblFechaIngresoTMS.Size = New System.Drawing.Size(51, 16)
        lblFechaIngresoTMS.TabIndex = 8
        lblFechaIngresoTMS.Text = "Código:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(30, 75)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(50, 16)
        Label1.TabIndex = 9
        Label1.Text = "Pedido:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(30, 147)
        Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(50, 16)
        Label2.TabIndex = 10
        Label2.Text = "Estado:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(32, 183)
        Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(37, 16)
        Label3.TabIndex = 11
        Label3.Text = "Tipo:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(32, 227)
        Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(46, 16)
        Label4.TabIndex = 13
        Label4.Text = "Fecha:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(30, 111)
        Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(51, 16)
        Label5.TabIndex = 15
        Label5.Text = "Cliente:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.lblRegs})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 2
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(932, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'lblRegs
        '
        Me.lblRegs.Caption = "Registros: 0"
        Me.lblRegs.Id = 1
        Me.lblRegs.Name = "lblRegs"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Salida RFID"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblRegs)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 500)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(932, 30)
        '
        'txtIdRFIDEnc
        '
        Me.txtIdRFIDEnc.AcceptsReturn = True
        Me.txtIdRFIDEnc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdRFIDEnc.Location = New System.Drawing.Point(116, 37)
        Me.txtIdRFIDEnc.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdRFIDEnc.Name = "txtIdRFIDEnc"
        Me.txtIdRFIDEnc.ReadOnly = True
        Me.txtIdRFIDEnc.Size = New System.Drawing.Size(156, 23)
        Me.txtIdRFIDEnc.TabIndex = 4
        '
        'txtIdPedido
        '
        Me.txtIdPedido.AcceptsReturn = True
        Me.txtIdPedido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdPedido.Location = New System.Drawing.Point(116, 73)
        Me.txtIdPedido.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdPedido.Name = "txtIdPedido"
        Me.txtIdPedido.ReadOnly = True
        Me.txtIdPedido.Size = New System.Drawing.Size(156, 23)
        Me.txtIdPedido.TabIndex = 5
        '
        'txtEstado
        '
        Me.txtEstado.AcceptsReturn = True
        Me.txtEstado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstado.Location = New System.Drawing.Point(116, 145)
        Me.txtEstado.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtEstado.Name = "txtEstado"
        Me.txtEstado.ReadOnly = True
        Me.txtEstado.Size = New System.Drawing.Size(156, 23)
        Me.txtEstado.TabIndex = 6
        '
        'txtTipo
        '
        Me.txtTipo.AcceptsReturn = True
        Me.txtTipo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTipo.Location = New System.Drawing.Point(116, 182)
        Me.txtTipo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTipo.Name = "txtTipo"
        Me.txtTipo.ReadOnly = True
        Me.txtTipo.Size = New System.Drawing.Size(156, 23)
        Me.txtTipo.TabIndex = 7
        '
        'txtFechaAgr
        '
        Me.txtFechaAgr.AcceptsReturn = True
        Me.txtFechaAgr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFechaAgr.Location = New System.Drawing.Point(116, 220)
        Me.txtFechaAgr.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFechaAgr.Name = "txtFechaAgr"
        Me.txtFechaAgr.ReadOnly = True
        Me.txtFechaAgr.Size = New System.Drawing.Size(156, 23)
        Me.txtFechaAgr.TabIndex = 12
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 193)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(932, 307)
        Me.XtraTabControl1.TabIndex = 14
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.txtCliente)
        Me.XtraTabPage1.Controls.Add(Label5)
        Me.XtraTabPage1.Controls.Add(Me.txtIdRFIDEnc)
        Me.XtraTabPage1.Controls.Add(Label4)
        Me.XtraTabPage1.Controls.Add(Me.txtIdPedido)
        Me.XtraTabPage1.Controls.Add(Me.txtFechaAgr)
        Me.XtraTabPage1.Controls.Add(Me.txtEstado)
        Me.XtraTabPage1.Controls.Add(Label3)
        Me.XtraTabPage1.Controls.Add(Me.txtTipo)
        Me.XtraTabPage1.Controls.Add(Label2)
        Me.XtraTabPage1.Controls.Add(lblFechaIngresoTMS)
        Me.XtraTabPage1.Controls.Add(Label1)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(930, 277)
        Me.XtraTabPage1.Text = "Encabezado"
        '
        'txtCliente
        '
        Me.txtCliente.AcceptsReturn = True
        Me.txtCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCliente.Location = New System.Drawing.Point(116, 109)
        Me.txtCliente.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.ReadOnly = True
        Me.txtCliente.Size = New System.Drawing.Size(156, 23)
        Me.txtCliente.TabIndex = 14
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.grdDetalle)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(930, 277)
        Me.XtraTabPage2.Text = "Detalle"
        '
        'grdDetalle
        '
        Me.grdDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetalle.Location = New System.Drawing.Point(0, 0)
        Me.grdDetalle.MainView = Me.GridView1
        Me.grdDetalle.MenuManager = Me.RibbonControl
        Me.grdDetalle.Name = "grdDetalle"
        Me.grdDetalle.Size = New System.Drawing.Size(930, 277)
        Me.grdDetalle.TabIndex = 0
        Me.grdDetalle.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.grdDetalle
        Me.GridView1.Name = "GridView1"
        '
        'frmDocSalidaRFID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(932, 530)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmDocSalidaRFID"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Salida"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage1.PerformLayout()
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.grdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents txtIdRFIDEnc As TextBox
    Friend WithEvents txtIdPedido As TextBox
    Friend WithEvents txtEstado As TextBox
    Friend WithEvents txtTipo As TextBox
    Friend WithEvents txtFechaAgr As TextBox
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdDetalle As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtCliente As TextBox
    Friend WithEvents lblRegs As DevExpress.XtraBars.BarButtonItem
End Class
