<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLicGen
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
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGenerarLic = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.TabLic = New DevExpress.XtraBars.Navigation.TabPane()
        Me.TabServer = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.numUx = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtLlave = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.numBO = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.numHH = New System.Windows.Forms.NumericUpDown()
        Me.TabCliente = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtLic = New System.Windows.Forms.TextBox()
        Me.txtSolicitudCliente = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TabAntidoto = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCon = New System.Windows.Forms.TextBox()
        Me.txtSolicitudAntidoto = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TabActivarServer = New DevExpress.XtraBars.Navigation.TabNavigationPage()
        Me.txtSolicitudActivacionServer = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtServ = New System.Windows.Forms.TextBox()
        Me.chkConUx = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabLic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabLic.SuspendLayout()
        Me.TabServer.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.numUx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numBO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numHH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabCliente.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabAntidoto.SuspendLayout()
        Me.TabActivarServer.SuspendLayout()
        CType(Me.chkConUx.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.EmptyAreaImageOptions.ImagePadding = New System.Windows.Forms.Padding(35, 37, 35, 37)
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGenerarLic})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonControl.MaxItemId = 2
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.OptionsMenuMinWidth = 385
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1116, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGenerarLic
        '
        Me.mnuGenerarLic.Caption = "Generar"
        Me.mnuGenerarLic.Id = 1
        Me.mnuGenerarLic.Name = "mnuGenerarLic"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menú"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGenerarLic)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 590)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1116, 30)
        '
        'TabLic
        '
        Me.TabLic.Controls.Add(Me.TabServer)
        Me.TabLic.Controls.Add(Me.TabCliente)
        Me.TabLic.Controls.Add(Me.TabAntidoto)
        Me.TabLic.Controls.Add(Me.TabActivarServer)
        Me.TabLic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabLic.Location = New System.Drawing.Point(0, 193)
        Me.TabLic.Margin = New System.Windows.Forms.Padding(4)
        Me.TabLic.Name = "TabLic"
        Me.TabLic.Pages.AddRange(New DevExpress.XtraBars.Navigation.NavigationPageBase() {Me.TabServer, Me.TabCliente, Me.TabAntidoto, Me.TabActivarServer})
        Me.TabLic.RegularSize = New System.Drawing.Size(1116, 397)
        Me.TabLic.SelectedPage = Me.TabServer
        Me.TabLic.Size = New System.Drawing.Size(1116, 397)
        Me.TabLic.TabIndex = 2
        Me.TabLic.Text = "TabPane1"
        '
        'TabServer
        '
        Me.TabServer.Caption = "Servidor"
        Me.TabServer.Controls.Add(Me.GroupBox1)
        Me.TabServer.Margin = New System.Windows.Forms.Padding(4)
        Me.TabServer.Name = "TabServer"
        Me.TabServer.Size = New System.Drawing.Size(1116, 356)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkConUx)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.numUx)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtLlave)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtpFecha)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.numBO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.numHH)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1116, 356)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(424, 23)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(123, 20)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Ux (portal web)"
        '
        'numUx
        '
        Me.numUx.Location = New System.Drawing.Point(553, 20)
        Me.numUx.Margin = New System.Windows.Forms.Padding(4)
        Me.numUx.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numUx.Name = "numUx"
        Me.numUx.Size = New System.Drawing.Size(139, 26)
        Me.numUx.TabIndex = 7
        Me.numUx.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label5.Location = New System.Drawing.Point(4, 179)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1108, 25)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Clave de activación"
        '
        'txtLlave
        '
        Me.txtLlave.BackColor = System.Drawing.Color.White
        Me.txtLlave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLlave.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtLlave.Location = New System.Drawing.Point(4, 204)
        Me.txtLlave.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLlave.Multiline = True
        Me.txtLlave.Name = "txtLlave"
        Me.txtLlave.ReadOnly = True
        Me.txtLlave.Size = New System.Drawing.Size(1108, 148)
        Me.txtLlave.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(424, 65)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Back Office"
        '
        'dtpFecha
        '
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(552, 131)
        Me.dtpFecha.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(139, 26)
        Me.dtpFecha.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(424, 134)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 20)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Vence"
        '
        'numBO
        '
        Me.numBO.Location = New System.Drawing.Point(553, 62)
        Me.numBO.Margin = New System.Windows.Forms.Padding(4)
        Me.numBO.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numBO.Name = "numBO"
        Me.numBO.Size = New System.Drawing.Size(139, 26)
        Me.numBO.TabIndex = 1
        Me.numBO.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(424, 99)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Hand held"
        '
        'numHH
        '
        Me.numHH.Location = New System.Drawing.Point(552, 97)
        Me.numHH.Margin = New System.Windows.Forms.Padding(4)
        Me.numHH.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numHH.Name = "numHH"
        Me.numHH.Size = New System.Drawing.Size(140, 26)
        Me.numHH.TabIndex = 2
        Me.numHH.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'TabCliente
        '
        Me.TabCliente.Caption = "Cliente"
        Me.TabCliente.Controls.Add(Me.GroupBox2)
        Me.TabCliente.Margin = New System.Windows.Forms.Padding(4)
        Me.TabCliente.Name = "TabCliente"
        Me.TabCliente.Size = New System.Drawing.Size(1116, 356)
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtLic)
        Me.GroupBox2.Controls.Add(Me.txtSolicitudCliente)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(1116, 356)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Location = New System.Drawing.Point(4, 179)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1108, 25)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Clave de activación"
        '
        'txtLic
        '
        Me.txtLic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLic.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtLic.Location = New System.Drawing.Point(4, 204)
        Me.txtLic.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLic.Multiline = True
        Me.txtLic.Name = "txtLic"
        Me.txtLic.Size = New System.Drawing.Size(1108, 148)
        Me.txtLic.TabIndex = 5
        '
        'txtSolicitudCliente
        '
        Me.txtSolicitudCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSolicitudCliente.Location = New System.Drawing.Point(94, 76)
        Me.txtSolicitudCliente.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSolicitudCliente.Name = "txtSolicitudCliente"
        Me.txtSolicitudCliente.Size = New System.Drawing.Size(913, 26)
        Me.txtSolicitudCliente.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(94, 52)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(131, 20)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Llave de solicitud"
        '
        'TabAntidoto
        '
        Me.TabAntidoto.Caption = "Antídoto"
        Me.TabAntidoto.Controls.Add(Me.Label8)
        Me.TabAntidoto.Controls.Add(Me.txtCon)
        Me.TabAntidoto.Controls.Add(Me.txtSolicitudAntidoto)
        Me.TabAntidoto.Controls.Add(Me.Label6)
        Me.TabAntidoto.Margin = New System.Windows.Forms.Padding(4)
        Me.TabAntidoto.Name = "TabAntidoto"
        Me.TabAntidoto.Size = New System.Drawing.Size(1116, 356)
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label8.Location = New System.Drawing.Point(0, 183)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1116, 25)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Clave de activación"
        '
        'txtCon
        '
        Me.txtCon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCon.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtCon.Location = New System.Drawing.Point(0, 208)
        Me.txtCon.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCon.Multiline = True
        Me.txtCon.Name = "txtCon"
        Me.txtCon.Size = New System.Drawing.Size(1116, 148)
        Me.txtCon.TabIndex = 8
        '
        'txtSolicitudAntidoto
        '
        Me.txtSolicitudAntidoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSolicitudAntidoto.Location = New System.Drawing.Point(100, 70)
        Me.txtSolicitudAntidoto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSolicitudAntidoto.Name = "txtSolicitudAntidoto"
        Me.txtSolicitudAntidoto.Size = New System.Drawing.Size(913, 23)
        Me.txtSolicitudAntidoto.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(100, 46)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(131, 20)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Llave de solicitud"
        '
        'TabActivarServer
        '
        Me.TabActivarServer.Caption = "Activación de servidor"
        Me.TabActivarServer.Controls.Add(Me.txtSolicitudActivacionServer)
        Me.TabActivarServer.Controls.Add(Me.Label10)
        Me.TabActivarServer.Controls.Add(Me.Label9)
        Me.TabActivarServer.Controls.Add(Me.txtServ)
        Me.TabActivarServer.Margin = New System.Windows.Forms.Padding(4)
        Me.TabActivarServer.Name = "TabActivarServer"
        Me.TabActivarServer.Size = New System.Drawing.Size(1116, 356)
        '
        'txtSolicitudActivacionServer
        '
        Me.txtSolicitudActivacionServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSolicitudActivacionServer.Location = New System.Drawing.Point(91, 71)
        Me.txtSolicitudActivacionServer.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSolicitudActivacionServer.Name = "txtSolicitudActivacionServer"
        Me.txtSolicitudActivacionServer.Size = New System.Drawing.Size(913, 23)
        Me.txtSolicitudActivacionServer.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(91, 47)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(131, 20)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "Llave de solicitud"
        '
        'Label9
        '
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Location = New System.Drawing.Point(0, 183)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1116, 25)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Activar bandera servidor"
        '
        'txtServ
        '
        Me.txtServ.BackColor = System.Drawing.Color.Linen
        Me.txtServ.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtServ.Location = New System.Drawing.Point(0, 208)
        Me.txtServ.Margin = New System.Windows.Forms.Padding(4)
        Me.txtServ.Multiline = True
        Me.txtServ.Name = "txtServ"
        Me.txtServ.ReadOnly = True
        Me.txtServ.Size = New System.Drawing.Size(1116, 148)
        Me.txtServ.TabIndex = 12
        '
        'chkConUx
        '
        Me.chkConUx.Location = New System.Drawing.Point(724, 21)
        Me.chkConUx.MenuManager = Me.RibbonControl
        Me.chkConUx.Name = "chkConUx"
        Me.chkConUx.Properties.Caption = "Con UX"
        Me.chkConUx.Size = New System.Drawing.Size(94, 24)
        Me.chkConUx.TabIndex = 9
        '
        'frmLicGen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1116, 620)
        Me.Controls.Add(Me.TabLic)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmLicGen"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Generador de licencia"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabLic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabLic.ResumeLayout(False)
        Me.TabServer.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numUx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numBO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numHH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabCliente.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabAntidoto.ResumeLayout(False)
        Me.TabAntidoto.PerformLayout()
        Me.TabActivarServer.ResumeLayout(False)
        Me.TabActivarServer.PerformLayout()
        CType(Me.chkConUx.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGenerarLic As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents TabLic As DevExpress.XtraBars.Navigation.TabPane
    Friend WithEvents TabServer As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents TabCliente As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents TabAntidoto As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtLlave As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpFecha As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents numBO As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents numHH As NumericUpDown
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtLic As TextBox
    Friend WithEvents txtSolicitudCliente As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txtCon As TextBox
    Friend WithEvents txtSolicitudAntidoto As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TabActivarServer As DevExpress.XtraBars.Navigation.TabNavigationPage
    Friend WithEvents Label9 As Label
    Friend WithEvents txtServ As TextBox
    Friend WithEvents txtSolicitudActivacionServer As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents numUx As NumericUpDown
    Friend WithEvents chkConUx As DevExpress.XtraEditors.CheckEdit
End Class
