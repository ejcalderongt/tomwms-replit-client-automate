<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmcorreorpt
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
        Dim lblFrom As System.Windows.Forms.Label
        Dim NombreLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmcorreorpt))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdEnviar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.PanDatosMotivoAnulacion = New DevExpress.XtraEditors.GroupControl()
        Me.TabDatos = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.txtAdjunto = New DevExpress.XtraEditors.TextEdit()
        Me.txtFrom = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.txtBody = New System.Windows.Forms.TextBox()
        Me.lnkRolOperador = New System.Windows.Forms.LinkLabel()
        Me.txtTO = New DevExpress.XtraEditors.TextEdit()
        Me.txtAsunto = New DevExpress.XtraEditors.TextEdit()
        lblFrom = New System.Windows.Forms.Label()
        NombreLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanDatosMotivoAnulacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabDatos.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.txtAdjunto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.txtTO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAsunto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblFrom
        '
        lblFrom.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lblFrom.AutoSize = True
        lblFrom.Location = New System.Drawing.Point(26, 46)
        lblFrom.Name = "lblFrom"
        lblFrom.Size = New System.Drawing.Size(27, 16)
        lblFrom.TabIndex = 2
        lblFrom.Text = "De:"
        lblFrom.Visible = False
        '
        'NombreLabel
        '
        NombreLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(26, 130)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(51, 16)
        NombreLabel.TabIndex = 9
        NombreLabel.Text = "Asunto:"
        '
        'Label1
        '
        Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(26, 179)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(56, 16)
        Label1.TabIndex = 50
        Label1.Text = "Adjunto:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdEnviar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 2
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(876, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdEnviar
        '
        Me.cmdEnviar.Caption = "Enviar"
        Me.cmdEnviar.CloseSubMenuOnClickMode = DevExpress.Utils.DefaultBoolean.[False]
        Me.cmdEnviar.Id = 1
        Me.cmdEnviar.ImageOptions.SvgImage = CType(resources.GetObject("cmdEnviar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdEnviar.Name = "cmdEnviar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menú"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdEnviar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 672)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(876, 30)
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1131, 502)
        '
        'PanDatosMotivoAnulacion
        '
        Me.PanDatosMotivoAnulacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanDatosMotivoAnulacion.Location = New System.Drawing.Point(0, 0)
        Me.PanDatosMotivoAnulacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanDatosMotivoAnulacion.Name = "PanDatosMotivoAnulacion"
        Me.PanDatosMotivoAnulacion.Size = New System.Drawing.Size(1131, 502)
        Me.PanDatosMotivoAnulacion.TabIndex = 0
        Me.PanDatosMotivoAnulacion.Tag = ""
        '
        'TabDatos
        '
        Me.TabDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabDatos.Location = New System.Drawing.Point(0, 193)
        Me.TabDatos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TabDatos.Name = "TabDatos"
        Me.TabDatos.SelectedTabPage = Me.XtraTabPage2
        Me.TabDatos.Size = New System.Drawing.Size(876, 479)
        Me.TabDatos.TabIndex = 5
        Me.TabDatos.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage2})
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.GroupControl1)
        Me.XtraTabPage2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(874, 449)
        Me.XtraTabPage2.Text = "Datos generales"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Label1)
        Me.GroupControl1.Controls.Add(Me.txtAdjunto)
        Me.GroupControl1.Controls.Add(Me.txtFrom)
        Me.GroupControl1.Controls.Add(Me.GroupControl2)
        Me.GroupControl1.Controls.Add(Me.lnkRolOperador)
        Me.GroupControl1.Controls.Add(Me.txtTO)
        Me.GroupControl1.Controls.Add(lblFrom)
        Me.GroupControl1.Controls.Add(NombreLabel)
        Me.GroupControl1.Controls.Add(Me.txtAsunto)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(874, 449)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Tag = ""
        '
        'txtAdjunto
        '
        Me.txtAdjunto.Location = New System.Drawing.Point(119, 173)
        Me.txtAdjunto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtAdjunto.MenuManager = Me.RibbonControl
        Me.txtAdjunto.Name = "txtAdjunto"
        Me.txtAdjunto.Properties.ReadOnly = True
        Me.txtAdjunto.Size = New System.Drawing.Size(273, 22)
        Me.txtAdjunto.TabIndex = 49
        '
        'txtFrom
        '
        Me.txtFrom.Location = New System.Drawing.Point(119, 43)
        Me.txtFrom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtFrom.MenuManager = Me.RibbonControl
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Properties.MaskSettings.Set("mask", "n0")
        Me.txtFrom.Size = New System.Drawing.Size(273, 22)
        Me.txtFrom.TabIndex = 48
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.txtBody)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl2.Location = New System.Drawing.Point(2, 207)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(870, 240)
        Me.GroupControl2.TabIndex = 42
        Me.GroupControl2.Text = "Mensaje"
        '
        'txtBody
        '
        Me.txtBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBody.Location = New System.Drawing.Point(27, 46)
        Me.txtBody.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtBody.Multiline = True
        Me.txtBody.Name = "txtBody"
        Me.txtBody.Size = New System.Drawing.Size(816, 177)
        Me.txtBody.TabIndex = 28
        '
        'lnkRolOperador
        '
        Me.lnkRolOperador.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lnkRolOperador.AutoSize = True
        Me.lnkRolOperador.Location = New System.Drawing.Point(26, 85)
        Me.lnkRolOperador.Name = "lnkRolOperador"
        Me.lnkRolOperador.Size = New System.Drawing.Size(38, 16)
        Me.lnkRolOperador.TabIndex = 6
        Me.lnkRolOperador.TabStop = True
        Me.lnkRolOperador.Text = "Para:"
        '
        'txtTO
        '
        Me.txtTO.Location = New System.Drawing.Point(119, 81)
        Me.txtTO.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTO.MenuManager = Me.RibbonControl
        Me.txtTO.Name = "txtTO"
        Me.txtTO.Properties.MaskSettings.Set("mask", "n0")
        Me.txtTO.Size = New System.Drawing.Size(273, 22)
        Me.txtTO.TabIndex = 7
        '
        'txtAsunto
        '
        Me.txtAsunto.Location = New System.Drawing.Point(119, 127)
        Me.txtAsunto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtAsunto.MenuManager = Me.RibbonControl
        Me.txtAsunto.Name = "txtAsunto"
        Me.txtAsunto.Size = New System.Drawing.Size(273, 22)
        Me.txtAsunto.TabIndex = 10
        '
        'frmcorreorpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(876, 702)
        Me.Controls.Add(Me.TabDatos)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmcorreorpt"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Envio de Correo"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanDatosMotivoAnulacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabDatos.ResumeLayout(False)
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.txtAdjunto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.txtTO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAsunto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanDatosMotivoAnulacion As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TabDatos As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtFrom As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lnkRolOperador As LinkLabel
    Friend WithEvents txtTO As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAsunto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAdjunto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtBody As TextBox
    Friend WithEvents cmdEnviar As DevExpress.XtraBars.BarButtonItem
End Class
