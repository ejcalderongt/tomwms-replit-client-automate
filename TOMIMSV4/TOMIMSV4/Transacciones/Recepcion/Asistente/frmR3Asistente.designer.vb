<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmR3Asistente
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If pObjTR IsNot Nothing Then
                    pObjTR.Dispose()
                    pObjTR = Nothing
                End If
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmR3Asistente))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblDescripcion = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GrpTipoTR = New DevExpress.XtraEditors.GroupControl()
        Me.rdMotivoDevolucion = New System.Windows.Forms.RadioButton()
        Me.rdIngreso = New System.Windows.Forms.RadioButton()
        Me.PicBack = New System.Windows.Forms.PictureBox()
        Me.cmdCancelar = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdSiguiente = New DevExpress.XtraEditors.SimpleButton()
        Me.GrpHH = New DevExpress.XtraEditors.GroupControl()
        Me.rdSinHH = New System.Windows.Forms.RadioButton()
        Me.rdConHH = New System.Windows.Forms.RadioButton()
        Me.GrpReferencia = New DevExpress.XtraEditors.GroupControl()
        Me.rdSinReferencia = New System.Windows.Forms.RadioButton()
        Me.rdConReferencia = New System.Windows.Forms.RadioButton()
        Me.GrpListado = New DevExpress.XtraEditors.GroupControl()
        Me.Grid = New DevExpress.XtraGrid.GridControl()
        Me.DTBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsAsistente = New TOMWMS.DsAsistente()
        Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colTipo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.GrpTipoTR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTipoTR.SuspendLayout()
        CType(Me.PicBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpHH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpHH.SuspendLayout()
        CType(Me.GrpReferencia, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpReferencia.SuspendLayout()
        CType(Me.GrpListado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpListado.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsAsistente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(366, 41)
        Me.Label1.TabIndex = 0
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.lblTitulo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTitulo.Location = New System.Drawing.Point(39, 13)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(66, 16)
        Me.lblTitulo.TabIndex = 2
        Me.lblTitulo.Text = "Recepción"
        '
        'lblDescripcion
        '
        Me.lblDescripcion.AutoSize = True
        Me.lblDescripcion.BackColor = System.Drawing.Color.White
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescripcion.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblDescripcion.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDescripcion.Location = New System.Drawing.Point(31, 61)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(119, 18)
        Me.lblDescripcion.TabIndex = 3
        Me.lblDescripcion.Text = "Tipo Transacción"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(0, 231)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(366, 49)
        Me.Label3.TabIndex = 8
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'GrpTipoTR
        '
        Me.GrpTipoTR.Controls.Add(Me.rdMotivoDevolucion)
        Me.GrpTipoTR.Controls.Add(Me.rdIngreso)
        Me.GrpTipoTR.Location = New System.Drawing.Point(35, 97)
        Me.GrpTipoTR.Name = "GrpTipoTR"
        Me.GrpTipoTR.Size = New System.Drawing.Size(300, 118)
        Me.GrpTipoTR.TabIndex = 9
        '
        'rdMotivoDevolucion
        '
        Me.rdMotivoDevolucion.AutoSize = True
        Me.rdMotivoDevolucion.Location = New System.Drawing.Point(16, 70)
        Me.rdMotivoDevolucion.Name = "rdMotivoDevolucion"
        Me.rdMotivoDevolucion.Size = New System.Drawing.Size(79, 17)
        Me.rdMotivoDevolucion.TabIndex = 9
        Me.rdMotivoDevolucion.Text = "Devolución"
        Me.rdMotivoDevolucion.UseVisualStyleBackColor = True
        '
        'rdIngreso
        '
        Me.rdIngreso.AutoSize = True
        Me.rdIngreso.Location = New System.Drawing.Point(16, 37)
        Me.rdIngreso.Name = "rdIngreso"
        Me.rdIngreso.Size = New System.Drawing.Size(60, 17)
        Me.rdIngreso.TabIndex = 8
        Me.rdIngreso.Text = "Ingreso"
        Me.rdIngreso.UseVisualStyleBackColor = True
        '
        'PicBack
        '
        Me.PicBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.PicBack.Image = CType(resources.GetObject("PicBack.Image"), System.Drawing.Image)
        Me.PicBack.Location = New System.Drawing.Point(9, 10)
        Me.PicBack.Name = "PicBack"
        Me.PicBack.Size = New System.Drawing.Size(24, 22)
        Me.PicBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicBack.TabIndex = 1
        Me.PicBack.TabStop = False
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancelar.Appearance.Options.UseFont = True
        Me.cmdCancelar.Location = New System.Drawing.Point(260, 247)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancelar.TabIndex = 10
        Me.cmdCancelar.Text = "Cancelar"
        '
        'cmdSiguiente
        '
        Me.cmdSiguiente.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSiguiente.Appearance.Options.UseFont = True
        Me.cmdSiguiente.Location = New System.Drawing.Point(179, 247)
        Me.cmdSiguiente.Name = "cmdSiguiente"
        Me.cmdSiguiente.Size = New System.Drawing.Size(75, 23)
        Me.cmdSiguiente.TabIndex = 11
        Me.cmdSiguiente.Text = "&Siguiente >"
        '
        'GrpHH
        '
        Me.GrpHH.Controls.Add(Me.rdSinHH)
        Me.GrpHH.Controls.Add(Me.rdConHH)
        Me.GrpHH.Location = New System.Drawing.Point(373, 97)
        Me.GrpHH.Name = "GrpHH"
        Me.GrpHH.Size = New System.Drawing.Size(300, 118)
        Me.GrpHH.TabIndex = 12
        Me.GrpHH.Visible = False
        '
        'rdSinHH
        '
        Me.rdSinHH.AutoSize = True
        Me.rdSinHH.Location = New System.Drawing.Point(16, 70)
        Me.rdSinHH.Name = "rdSinHH"
        Me.rdSinHH.Size = New System.Drawing.Size(91, 17)
        Me.rdSinHH.TabIndex = 9
        Me.rdSinHH.Text = "Sin HandHeld"
        Me.rdSinHH.UseVisualStyleBackColor = True
        '
        'rdConHH
        '
        Me.rdConHH.AutoSize = True
        Me.rdConHH.Location = New System.Drawing.Point(16, 37)
        Me.rdConHH.Name = "rdConHH"
        Me.rdConHH.Size = New System.Drawing.Size(95, 17)
        Me.rdConHH.TabIndex = 8
        Me.rdConHH.Text = "Con HandHeld"
        Me.rdConHH.UseVisualStyleBackColor = True
        '
        'GrpReferencia
        '
        Me.GrpReferencia.Controls.Add(Me.rdSinReferencia)
        Me.GrpReferencia.Controls.Add(Me.rdConReferencia)
        Me.GrpReferencia.Location = New System.Drawing.Point(373, 282)
        Me.GrpReferencia.Name = "GrpReferencia"
        Me.GrpReferencia.Size = New System.Drawing.Size(300, 118)
        Me.GrpReferencia.TabIndex = 13
        Me.GrpReferencia.Visible = False
        '
        'rdSinReferencia
        '
        Me.rdSinReferencia.AutoSize = True
        Me.rdSinReferencia.Location = New System.Drawing.Point(16, 70)
        Me.rdSinReferencia.Name = "rdSinReferencia"
        Me.rdSinReferencia.Size = New System.Drawing.Size(95, 17)
        Me.rdSinReferencia.TabIndex = 9
        Me.rdSinReferencia.Text = "Sin Referencia"
        Me.rdSinReferencia.UseVisualStyleBackColor = True
        '
        'rdConReferencia
        '
        Me.rdConReferencia.AutoSize = True
        Me.rdConReferencia.Location = New System.Drawing.Point(16, 37)
        Me.rdConReferencia.Name = "rdConReferencia"
        Me.rdConReferencia.Size = New System.Drawing.Size(99, 17)
        Me.rdConReferencia.TabIndex = 8
        Me.rdConReferencia.Text = "Con Referencia"
        Me.rdConReferencia.UseVisualStyleBackColor = True
        '
        'GrpListado
        '
        Me.GrpListado.Controls.Add(Me.Grid)
        Me.GrpListado.Location = New System.Drawing.Point(35, 282)
        Me.GrpListado.Name = "GrpListado"
        Me.GrpListado.Size = New System.Drawing.Size(300, 118)
        Me.GrpListado.TabIndex = 14
        Me.GrpListado.Visible = False
        '
        'Grid
        '
        Me.Grid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Grid.DataSource = Me.DTBindingSource
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.Location = New System.Drawing.Point(2, 21)
        Me.Grid.MainView = Me.gridView1
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.Grid.Size = New System.Drawing.Size(296, 95)
        Me.Grid.TabIndex = 2
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridView1})
        '
        'DTBindingSource
        '
        Me.DTBindingSource.DataMember = "DT"
        Me.DTBindingSource.DataSource = Me.DsAsistente
        '
        'DsAsistente
        '
        Me.DsAsistente.DataSetName = "DsAsistente"
        Me.DsAsistente.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'gridView1
        '
        Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccion, Me.colTipo})
        Me.gridView1.GridControl = Me.Grid
        Me.gridView1.Name = "gridView1"
        Me.gridView1.OptionsView.ShowGroupPanel = False
        '
        'colSeleccion
        '
        Me.colSeleccion.Caption = "Selección"
        Me.colSeleccion.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colSeleccion.FieldName = "colSeleccion"
        Me.colSeleccion.Name = "colSeleccion"
        Me.colSeleccion.Visible = True
        Me.colSeleccion.VisibleIndex = 0
        Me.colSeleccion.Width = 76
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colTipo
        '
        Me.colTipo.Caption = "Tipo Transacción"
        Me.colTipo.FieldName = "colTipo"
        Me.colTipo.Name = "colTipo"
        Me.colTipo.Visible = True
        Me.colTipo.VisibleIndex = 1
        Me.colTipo.Width = 202
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'frmR3Asistente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(366, 280)
        Me.Controls.Add(Me.GrpListado)
        Me.Controls.Add(Me.GrpReferencia)
        Me.Controls.Add(Me.GrpHH)
        Me.Controls.Add(Me.cmdSiguiente)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.GrpTipoTR)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.PicBack)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmR3Asistente"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Asistente"
        CType(Me.GrpTipoTR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTipoTR.ResumeLayout(False)
        Me.GrpTipoTR.PerformLayout()
        CType(Me.PicBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpHH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpHH.ResumeLayout(False)
        Me.GrpHH.PerformLayout()
        CType(Me.GrpReferencia, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpReferencia.ResumeLayout(False)
        Me.GrpReferencia.PerformLayout()
        CType(Me.GrpListado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpListado.ResumeLayout(False)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsAsistente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PicBack As System.Windows.Forms.PictureBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GrpTipoTR As DevExpress.XtraEditors.GroupControl
    Friend WithEvents rdMotivoDevolucion As System.Windows.Forms.RadioButton
    Friend WithEvents rdIngreso As System.Windows.Forms.RadioButton
    Friend WithEvents cmdCancelar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdSiguiente As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GrpHH As DevExpress.XtraEditors.GroupControl
    Friend WithEvents rdSinHH As System.Windows.Forms.RadioButton
    Friend WithEvents rdConHH As System.Windows.Forms.RadioButton
    Friend WithEvents GrpReferencia As DevExpress.XtraEditors.GroupControl
    Friend WithEvents rdSinReferencia As System.Windows.Forms.RadioButton
    Friend WithEvents rdConReferencia As System.Windows.Forms.RadioButton
    Friend WithEvents GrpListado As DevExpress.XtraEditors.GroupControl
    Private WithEvents Grid As DevExpress.XtraGrid.GridControl
    Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents DTBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsAsistente As TOMWMS.DsAsistente
    Friend WithEvents colSeleccion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTipo As DevExpress.XtraGrid.Columns.GridColumn
End Class
