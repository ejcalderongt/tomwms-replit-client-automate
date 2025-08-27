<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmIniWCF
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIniWCF))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdSalir = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.Grp = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmdAplicarWCF = New DevExpress.XtraEditors.SimpleButton()
        Me.txtBDWCF = New System.Windows.Forms.TextBox()
        Me.txtServerWCF = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GrpTIpoTransaccion = New DevExpress.XtraEditors.GroupControl()
        Me.cmdAplicarAPP = New DevExpress.XtraEditors.SimpleButton()
        Me.txtBDAPP = New System.Windows.Forms.TextBox()
        Me.txtServidorAPP = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.Grp,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Grp.SuspendLayout
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupControl1.SuspendLayout
        CType(Me.GrpTIpoTransaccion,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GrpTIpoTransaccion.SuspendLayout
        Me.SuspendLayout
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuSalir, Me.mnuAsignacion, Me.cmdGuardar, Me.cmdSalir, Me.RibbonControl.SearchEditItem})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 7
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(650, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Description = "Guarde el registro"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.mnuGuardar.Name = "mnuGuardar"
        Me.mnuGuardar.ShortcutKeyDisplayString = "G"
        Me.mnuGuardar.SmallWithoutTextWidth = 10
        Me.mnuGuardar.SmallWithTextWidth = 10
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
        'mnuSalir
        '
        Me.mnuSalir.Caption = "Salir"
        Me.mnuSalir.Id = 3
        Me.mnuSalir.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuSalir.Name = "mnuSalir"
        '
        'mnuAsignacion
        '
        Me.mnuAsignacion.Caption = "Asignacion"
        Me.mnuAsignacion.Id = 4
        Me.mnuAsignacion.ImageOptions.Image = CType(resources.GetObject("mnuAsignacion.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuAsignacion.ImageOptions.LargeImage = CType(resources.GetObject("mnuAsignacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuAsignacion.Name = "mnuAsignacion"
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 5
        Me.cmdGuardar.ImageOptions.SvgImage = CType(resources.GetObject("cmdGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdGuardar.Name = "cmdGuardar"
        '
        'cmdSalir
        '
        Me.cmdSalir.Caption = "Salir"
        Me.cmdSalir.Id = 6
        Me.cmdSalir.ImageOptions.SvgImage = CType(resources.GetObject("cmdSalir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdSalir.Name = "cmdSalir"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menu"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdSalir)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 700)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(650, 30)
        '
        'Grp
        '
        Me.Grp.Controls.Add(Me.GroupControl1)
        Me.Grp.Controls.Add(Me.GrpTIpoTransaccion)
        Me.Grp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grp.Location = New System.Drawing.Point(0, 193)
        Me.Grp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Grp.Name = "Grp"
        Me.Grp.Size = New System.Drawing.Size(650, 507)
        Me.Grp.TabIndex = 0
        Me.Grp.Text = "Datos"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.cmdAplicarWCF)
        Me.GroupControl1.Controls.Add(Me.txtBDWCF)
        Me.GroupControl1.Controls.Add(Me.txtServerWCF)
        Me.GroupControl1.Controls.Add(Me.Label3)
        Me.GroupControl1.Controls.Add(Me.Label4)
        Me.GroupControl1.Location = New System.Drawing.Point(6, 196)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(609, 143)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Configuración WCF"
        '
        'cmdAplicarWCF
        '
        Me.cmdAplicarWCF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdAplicarWCF.Location = New System.Drawing.Point(429, 108)
        Me.cmdAplicarWCF.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdAplicarWCF.Name = "cmdAplicarWCF"
        Me.cmdAplicarWCF.Size = New System.Drawing.Size(163, 28)
        Me.cmdAplicarWCF.TabIndex = 4
        Me.cmdAplicarWCF.Text = "Aplicar Configuración"
        '
        'txtBDWCF
        '
        Me.txtBDWCF.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBDWCF.Location = New System.Drawing.Point(160, 74)
        Me.txtBDWCF.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtBDWCF.Name = "txtBDWCF"
        Me.txtBDWCF.Size = New System.Drawing.Size(432, 26)
        Me.txtBDWCF.TabIndex = 3
        '
        'txtServerWCF
        '
        Me.txtServerWCF.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServerWCF.Location = New System.Drawing.Point(160, 41)
        Me.txtServerWCF.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtServerWCF.Name = "txtServerWCF"
        Me.txtServerWCF.Size = New System.Drawing.Size(432, 26)
        Me.txtServerWCF.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(12, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(159, 21)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Base de Datos (BD)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(12, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(151, 21)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Servidor SQLServer"
        '
        'GrpTIpoTransaccion
        '
        Me.GrpTIpoTransaccion.Controls.Add(Me.cmdAplicarAPP)
        Me.GrpTIpoTransaccion.Controls.Add(Me.txtBDAPP)
        Me.GrpTIpoTransaccion.Controls.Add(Me.txtServidorAPP)
        Me.GrpTIpoTransaccion.Controls.Add(Me.Label2)
        Me.GrpTIpoTransaccion.Controls.Add(Me.Label1)
        Me.GrpTIpoTransaccion.Location = New System.Drawing.Point(6, 28)
        Me.GrpTIpoTransaccion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GrpTIpoTransaccion.Name = "GrpTIpoTransaccion"
        Me.GrpTIpoTransaccion.Size = New System.Drawing.Size(609, 143)
        Me.GrpTIpoTransaccion.TabIndex = 0
        Me.GrpTIpoTransaccion.Text = "Configuración APP"
        '
        'cmdAplicarAPP
        '
        Me.cmdAplicarAPP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdAplicarAPP.Location = New System.Drawing.Point(429, 107)
        Me.cmdAplicarAPP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdAplicarAPP.Name = "cmdAplicarAPP"
        Me.cmdAplicarAPP.Size = New System.Drawing.Size(163, 28)
        Me.cmdAplicarAPP.TabIndex = 4
        Me.cmdAplicarAPP.Text = "Aplicar Configuración"
        '
        'txtBDAPP
        '
        Me.txtBDAPP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBDAPP.Location = New System.Drawing.Point(160, 74)
        Me.txtBDAPP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtBDAPP.Name = "txtBDAPP"
        Me.txtBDAPP.Size = New System.Drawing.Size(432, 26)
        Me.txtBDAPP.TabIndex = 3
        '
        'txtServidorAPP
        '
        Me.txtServidorAPP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServidorAPP.Location = New System.Drawing.Point(160, 41)
        Me.txtServidorAPP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtServidorAPP.Name = "txtServidorAPP"
        Me.txtServidorAPP.Size = New System.Drawing.Size(432, 26)
        Me.txtServidorAPP.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(12, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(159, 21)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Base de Datos (BD)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(12, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(151, 21)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Servidor SQLServer"
        '
        'frmIniWCF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(650, 730)
        Me.Controls.Add(Me.Grp)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = false
        Me.Name = "frmIniWCF"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Parámetros de Conexión"
        CType(Me.RibbonControl,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Grp,System.ComponentModel.ISupportInitialize).EndInit
        Me.Grp.ResumeLayout(false)
        CType(Me.GroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupControl1.ResumeLayout(false)
        Me.GroupControl1.PerformLayout
        CType(Me.GrpTIpoTransaccion,System.ComponentModel.ISupportInitialize).EndInit
        Me.GrpTIpoTransaccion.ResumeLayout(false)
        Me.GrpTIpoTransaccion.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Grp As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdSalir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GrpTIpoTransaccion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdAplicarWCF As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdAplicarAPP As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtBDWCF As TextBox
    Friend WithEvents txtServerWCF As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtBDAPP As TextBox
    Friend WithEvents txtServidorAPP As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
