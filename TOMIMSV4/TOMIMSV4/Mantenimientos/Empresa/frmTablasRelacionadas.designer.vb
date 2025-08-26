<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTablasRelacionadas
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTablasRelacionadas))
        Me.listReferencias = New System.Windows.Forms.ListBox()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'listReferencias
        '
        Me.listReferencias.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listReferencias.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listReferencias.FormattingEnabled = True
        Me.listReferencias.ItemHeight = 16
        Me.listReferencias.Location = New System.Drawing.Point(0, 49)
        Me.listReferencias.Name = "listReferencias"
        Me.listReferencias.Size = New System.Drawing.Size(587, 340)
        Me.listReferencias.TabIndex = 0
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Glyph = CType(resources.GetObject("mnuGuardar.Glyph"), System.Drawing.Image)
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.LargeGlyph = CType(resources.GetObject("mnuGuardar.LargeGlyph"), System.Drawing.Image)
        Me.mnuGuardar.Name = "mnuGuardar"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Glyph = CType(resources.GetObject("mnuActualizar.Glyph"), System.Drawing.Image)
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.LargeGlyph = CType(resources.GetObject("mnuActualizar.LargeGlyph"), System.Drawing.Image)
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Glyph = CType(resources.GetObject("mnuEliminar.Glyph"), System.Drawing.Image)
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.LargeGlyph = CType(resources.GetObject("mnuEliminar.LargeGlyph"), System.Drawing.Image)
        Me.mnuEliminar.Name = "mnuEliminar"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 5
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Size = New System.Drawing.Size(587, 49)
        '
        'frmTablasRelacionadas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 389)
        Me.Controls.Add(Me.listReferencias)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmTablasRelacionadas"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "No se puede eliminar, existen referencia a estas tablas"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents listReferencias As ListBox
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
End Class
