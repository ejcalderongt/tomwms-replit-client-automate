<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTipoAcuerdoComercial
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
        Dim lblIdArea As System.Windows.Forms.Label
        Dim lblCodigoArea As System.Windows.Forms.Label
        Dim lblDescripcionAreaBodega As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTipoAcuerdoComercial))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbTipoCobro = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblTipoCobro = New DevExpress.XtraEditors.LabelControl()
        Me.txtCorrelativoAcuerdo = New DevExpress.XtraEditors.TextEdit()
        Me.txtDescripcion = New DevExpress.XtraEditors.TextEdit()
        Me.txtServicio = New DevExpress.XtraEditors.TextEdit()
        lblIdArea = New System.Windows.Forms.Label()
        lblCodigoArea = New System.Windows.Forms.Label()
        lblDescripcionAreaBodega = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoCobro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorrelativoAcuerdo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescripcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServicio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblIdArea
        '
        lblIdArea.AutoSize = True
        lblIdArea.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblIdArea.Location = New System.Drawing.Point(23, 55)
        lblIdArea.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblIdArea.Name = "lblIdArea"
        lblIdArea.Size = New System.Drawing.Size(74, 16)
        lblIdArea.TabIndex = 24
        lblIdArea.Text = "Correlativo:"
        '
        'lblCodigoArea
        '
        lblCodigoArea.AutoSize = True
        lblCodigoArea.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblCodigoArea.Location = New System.Drawing.Point(23, 125)
        lblCodigoArea.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblCodigoArea.Name = "lblCodigoArea"
        lblCodigoArea.Size = New System.Drawing.Size(77, 16)
        lblCodigoArea.TabIndex = 20
        lblCodigoArea.Text = "Descripción:"
        '
        'lblDescripcionAreaBodega
        '
        lblDescripcionAreaBodega.AutoSize = True
        lblDescripcionAreaBodega.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblDescripcionAreaBodega.Location = New System.Drawing.Point(23, 88)
        lblDescripcionAreaBodega.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblDescripcionAreaBodega.Name = "lblDescripcionAreaBodega"
        lblDescripcionAreaBodega.Size = New System.Drawing.Size(57, 16)
        lblDescripcionAreaBodega.TabIndex = 22
        lblDescripcionAreaBodega.Text = "Servicio:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(23, 200)
        Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(54, 16)
        Label1.TabIndex = 30
        Label1.Text = "Bodega:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 2
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(456, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 1
        Me.cmdGuardar.ImageOptions.SvgImage = CType(resources.GetObject("cmdGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 515)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(456, 30)
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.cmbBodega)
        Me.GroupControl4.Controls.Add(Me.cmbTipoCobro)
        Me.GroupControl4.Controls.Add(Me.lblTipoCobro)
        Me.GroupControl4.Controls.Add(Label1)
        Me.GroupControl4.Controls.Add(lblIdArea)
        Me.GroupControl4.Controls.Add(Me.txtCorrelativoAcuerdo)
        Me.GroupControl4.Controls.Add(lblCodigoArea)
        Me.GroupControl4.Controls.Add(Me.txtDescripcion)
        Me.GroupControl4.Controls.Add(lblDescripcionAreaBodega)
        Me.GroupControl4.Controls.Add(Me.txtServicio)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(456, 322)
        Me.GroupControl4.TabIndex = 3
        Me.GroupControl4.Text = "Opciones del acuerdo comercial"
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(111, 194)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Size = New System.Drawing.Size(252, 22)
        Me.cmbBodega.TabIndex = 85
        '
        'cmbTipoCobro
        '
        Me.cmbTipoCobro.Location = New System.Drawing.Point(111, 241)
        Me.cmbTipoCobro.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTipoCobro.Name = "cmbTipoCobro"
        Me.cmbTipoCobro.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.cmbTipoCobro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoCobro.Size = New System.Drawing.Size(252, 22)
        Me.cmbTipoCobro.TabIndex = 84
        '
        'lblTipoCobro
        '
        Me.lblTipoCobro.Appearance.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.lblTipoCobro.Appearance.Options.UseFont = True
        Me.lblTipoCobro.Location = New System.Drawing.Point(26, 244)
        Me.lblTipoCobro.Margin = New System.Windows.Forms.Padding(4)
        Me.lblTipoCobro.Name = "lblTipoCobro"
        Me.lblTipoCobro.Size = New System.Drawing.Size(68, 16)
        Me.lblTipoCobro.TabIndex = 83
        Me.lblTipoCobro.Text = "Tipo Cobro:"
        '
        'txtCorrelativoAcuerdo
        '
        Me.txtCorrelativoAcuerdo.Location = New System.Drawing.Point(111, 49)
        Me.txtCorrelativoAcuerdo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCorrelativoAcuerdo.Name = "txtCorrelativoAcuerdo"
        Me.txtCorrelativoAcuerdo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCorrelativoAcuerdo.Properties.Appearance.Options.UseFont = True
        Me.txtCorrelativoAcuerdo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtCorrelativoAcuerdo.Properties.MaxLength = 50
        Me.txtCorrelativoAcuerdo.Properties.ReadOnly = True
        Me.txtCorrelativoAcuerdo.Size = New System.Drawing.Size(252, 28)
        Me.txtCorrelativoAcuerdo.TabIndex = 25
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Location = New System.Drawing.Point(111, 121)
        Me.txtDescripcion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcion.Properties.Appearance.Options.UseFont = True
        Me.txtDescripcion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtDescripcion.Properties.MaxLength = 50
        Me.txtDescripcion.Size = New System.Drawing.Size(252, 28)
        Me.txtDescripcion.TabIndex = 21
        '
        'txtServicio
        '
        Me.txtServicio.Location = New System.Drawing.Point(111, 85)
        Me.txtServicio.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtServicio.Name = "txtServicio"
        Me.txtServicio.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServicio.Properties.Appearance.Options.UseFont = True
        Me.txtServicio.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.txtServicio.Properties.MaxLength = 50
        Me.txtServicio.Size = New System.Drawing.Size(252, 28)
        Me.txtServicio.TabIndex = 23
        '
        'frmTipoAcuerdoComercial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 545)
        Me.Controls.Add(Me.GroupControl4)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmTipoAcuerdoComercial"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Tipo de Acuerdo Comercial"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoCobro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorrelativoAcuerdo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescripcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServicio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtCorrelativoAcuerdo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDescripcion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtServicio As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmbTipoCobro As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblTipoCobro As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
End Class
