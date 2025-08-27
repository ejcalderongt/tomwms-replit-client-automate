<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAgregaDestinatario
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Label53 As System.Windows.Forms.Label
        Dim Label As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAgregaDestinatario))
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem7 = New DevExpress.XtraBars.BarButtonItem()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.lblusuarioCreo = New System.Windows.Forms.Label()
        Me.cmdAceptar = New System.Windows.Forms.Button()
        Me.lblFechaCreacion = New System.Windows.Forms.Label()
        Me.cmdCancelar = New System.Windows.Forms.Button()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.cmdNuevo = New DevExpress.XtraEditors.SimpleButton()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbDestinatario = New System.Windows.Forms.ComboBox()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Label53 = New System.Windows.Forms.Label()
        Label = New System.Windows.Forms.Label()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label53
        '
        Label53.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label53.AutoSize = True
        Label53.Location = New System.Drawing.Point(25, 46)
        Label53.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label53.Name = "Label53"
        Label53.Size = New System.Drawing.Size(85, 17)
        Label53.TabIndex = 0
        Label53.Text = "Destinatario:"
        '
        'Label
        '
        Label.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Label.AutoSize = True
        Label.Location = New System.Drawing.Point(25, 79)
        Label.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Label.Name = "Label"
        Label.Size = New System.Drawing.Size(46, 17)
        Label.TabIndex = 3
        Label.Text = "Activo"
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem1, Me.BarButtonItem7})
        Me.BarManager1.MaxItemId = 18
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.barDockControlTop.Size = New System.Drawing.Size(713, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 164)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.barDockControlBottom.Size = New System.Drawing.Size(713, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 164)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(713, 0)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 164)
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Nueva Partida"
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem7
        '
        Me.BarButtonItem7.Caption = "Cerrar"
        Me.BarButtonItem7.Id = 6
        Me.BarButtonItem7.Name = "BarButtonItem7"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.lblusuarioCreo)
        Me.PanelControl2.Controls.Add(Me.cmdAceptar)
        Me.PanelControl2.Controls.Add(Me.lblFechaCreacion)
        Me.PanelControl2.Controls.Add(Me.cmdCancelar)
        Me.PanelControl2.Controls.Add(Me.GroupControl2)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(713, 164)
        Me.PanelControl2.TabIndex = 0
        '
        'lblusuarioCreo
        '
        Me.lblusuarioCreo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblusuarioCreo.AutoSize = True
        Me.lblusuarioCreo.Font = New System.Drawing.Font("Arial", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblusuarioCreo.Location = New System.Drawing.Point(7, 44)
        Me.lblusuarioCreo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblusuarioCreo.Name = "lblusuarioCreo"
        Me.lblusuarioCreo.Size = New System.Drawing.Size(0, 16)
        Me.lblusuarioCreo.TabIndex = 2
        Me.lblusuarioCreo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdAceptar
        '
        Me.cmdAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAceptar.Location = New System.Drawing.Point(465, 128)
        Me.cmdAceptar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdAceptar.Name = "cmdAceptar"
        Me.cmdAceptar.Size = New System.Drawing.Size(120, 27)
        Me.cmdAceptar.TabIndex = 3
        Me.cmdAceptar.Text = "Aceptar"
        Me.cmdAceptar.UseVisualStyleBackColor = True
        '
        'lblFechaCreacion
        '
        Me.lblFechaCreacion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFechaCreacion.AutoSize = True
        Me.lblFechaCreacion.Font = New System.Drawing.Font("Arial", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaCreacion.Location = New System.Drawing.Point(7, 22)
        Me.lblFechaCreacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFechaCreacion.Name = "lblFechaCreacion"
        Me.lblFechaCreacion.Size = New System.Drawing.Size(0, 16)
        Me.lblFechaCreacion.TabIndex = 1
        Me.lblFechaCreacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancelar.Location = New System.Drawing.Point(593, 128)
        Me.cmdCancelar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(112, 27)
        Me.cmdCancelar.TabIndex = 4
        Me.cmdCancelar.Text = "Cancelar"
        Me.cmdCancelar.UseVisualStyleBackColor = True
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.cmdNuevo)
        Me.GroupControl2.Controls.Add(Label)
        Me.GroupControl2.Controls.Add(Me.chkActivo)
        Me.GroupControl2.Controls.Add(Label53)
        Me.GroupControl2.Controls.Add(Me.cmbDestinatario)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(709, 160)
        Me.GroupControl2.TabIndex = 0
        Me.GroupControl2.Text = "Dato"
        '
        'cmdNuevo
        '
        Me.cmdNuevo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdNuevo.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNuevo.Appearance.Options.UseFont = True
        Me.cmdNuevo.Location = New System.Drawing.Point(490, 42)
        Me.cmdNuevo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdNuevo.Name = "cmdNuevo"
        Me.cmdNuevo.Size = New System.Drawing.Size(100, 25)
        Me.cmdNuevo.TabIndex = 2
        Me.cmdNuevo.Text = "Nuevo"
        '
        'chkActivo
        '
        Me.chkActivo.Location = New System.Drawing.Point(137, 75)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(133, 24)
        Me.chkActivo.TabIndex = 4
        '
        'cmbDestinatario
        '
        Me.cmbDestinatario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDestinatario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDestinatario.ForeColor = System.Drawing.Color.Navy
        Me.cmbDestinatario.Location = New System.Drawing.Point(137, 42)
        Me.cmbDestinatario.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbDestinatario.Name = "cmbDestinatario"
        Me.cmbDestinatario.Size = New System.Drawing.Size(333, 25)
        Me.cmbDestinatario.TabIndex = 1
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "1335069004_folder_plus.png")
        Me.ImageList1.Images.SetKeyName(1, "1335069124_zoom.png")
        Me.ImageList1.Images.SetKeyName(2, "1335109692_print.png")
        Me.ImageList1.Images.SetKeyName(3, "1335109718_delete.png")
        '
        'frmAgregaDestinatario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(713, 164)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAgregaDestinatario"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Agregar Destinatario"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem7 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblusuarioCreo As System.Windows.Forms.Label
    Friend WithEvents lblFechaCreacion As System.Windows.Forms.Label
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents cmdAceptar As System.Windows.Forms.Button
    Friend WithEvents cmbDestinatario As ComboBox
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmdNuevo As DevExpress.XtraEditors.SimpleButton
End Class
