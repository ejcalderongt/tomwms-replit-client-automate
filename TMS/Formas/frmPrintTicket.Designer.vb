<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintTicket
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintTicket))
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.lblServerAPP = New DevExpress.XtraBars.BarButtonItem()
        Me.lblBD = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuListaTickets = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtNoTicket = New DevExpress.XtraEditors.TextEdit()
        Me.lblTicketNo = New DevExpress.XtraEditors.LabelControl()
        Me.btnImprimir = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtNoTicket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblServerAPP)
        Me.RibbonStatusBar.ItemLinks.Add(Me.lblBD)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 520)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(970, 32)
        '
        'lblServerAPP
        '
        Me.lblServerAPP.Caption = "Server:"
        Me.lblServerAPP.Id = 2
        Me.lblServerAPP.ImageOptions.Image = CType(resources.GetObject("lblServerAPP.ImageOptions.Image"), System.Drawing.Image)
        Me.lblServerAPP.ImageOptions.LargeImage = CType(resources.GetObject("lblServerAPP.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblServerAPP.Name = "lblServerAPP"
        '
        'lblBD
        '
        Me.lblBD.Caption = "BD"
        Me.lblBD.Id = 3
        Me.lblBD.ImageOptions.Image = CType(resources.GetObject("lblBD.ImageOptions.Image"), System.Drawing.Image)
        Me.lblBD.ImageOptions.LargeImage = CType(resources.GetObject("lblBD.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.lblBD.ImageOptions.SvgImage = CType(resources.GetObject("lblBD.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.lblBD.Name = "lblBD"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuListaTickets, Me.lblServerAPP, Me.lblBD})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(970, 198)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuListaTickets
        '
        Me.mnuListaTickets.Caption = "Lista de tickets"
        Me.mnuListaTickets.Id = 1
        Me.mnuListaTickets.ImageOptions.SvgImage = CType(resources.GetObject("mnuListaTickets.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuListaTickets.Name = "mnuListaTickets"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Imprimir Ticket"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuListaTickets)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.SplitContainer1)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 198)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(970, 322)
        Me.GroupControl4.TabIndex = 2
        Me.GroupControl4.Text = "Datos del Ticket"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 33)
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnImprimir)
        Me.SplitContainer1.Size = New System.Drawing.Size(966, 287)
        Me.SplitContainer1.SplitterDistance = 142
        Me.SplitContainer1.TabIndex = 4
        '
        'txtNoTicket
        '
        Me.txtNoTicket.EditValue = "0000000001"
        Me.txtNoTicket.Location = New System.Drawing.Point(363, 37)
        Me.txtNoTicket.MenuManager = Me.RibbonControl
        Me.txtNoTicket.Name = "txtNoTicket"
        Me.txtNoTicket.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 25.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoTicket.Properties.Appearance.Options.UseFont = True
        Me.txtNoTicket.Properties.Appearance.Options.UseTextOptions = True
        Me.txtNoTicket.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtNoTicket.Properties.ReadOnly = True
        Me.txtNoTicket.Size = New System.Drawing.Size(300, 58)
        Me.txtNoTicket.TabIndex = 1
        '
        'lblTicketNo
        '
        Me.lblTicketNo.Appearance.Font = New System.Drawing.Font("Tahoma", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTicketNo.Appearance.Options.UseFont = True
        Me.lblTicketNo.Appearance.Options.UseTextOptions = True
        Me.lblTicketNo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblTicketNo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblTicketNo.Location = New System.Drawing.Point(259, 40)
        Me.lblTicketNo.Name = "lblTicketNo"
        Me.lblTicketNo.Size = New System.Drawing.Size(98, 55)
        Me.lblTicketNo.TabIndex = 0
        Me.lblTicketNo.Text = "Ticket #"
        '
        'btnImprimir
        '
        Me.btnImprimir.Appearance.Font = New System.Drawing.Font("Tahoma", 25.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Appearance.Options.UseFont = True
        Me.btnImprimir.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnImprimir.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftTop
        Me.btnImprimir.ImageOptions.SvgImage = CType(resources.GetObject("btnImprimir.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnImprimir.Location = New System.Drawing.Point(0, 0)
        Me.btnImprimir.LookAndFeel.SkinName = "DevExpress Style"
        Me.btnImprimir.LookAndFeel.UseDefaultLookAndFeel = False
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(966, 141)
        Me.btnImprimir.TabIndex = 3
        Me.btnImprimir.Text = "Imprimir"
        '
        'frmPrintTicket
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(970, 552)
        Me.Controls.Add(Me.GroupControl4)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Name = "frmPrintTicket"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "WMS PRINT TICKET"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtNoTicket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblTicketNo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNoTicket As DevExpress.XtraEditors.TextEdit
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents btnImprimir As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents mnuListaTickets As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents lblServerAPP As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lblBD As DevExpress.XtraBars.BarButtonItem
End Class
