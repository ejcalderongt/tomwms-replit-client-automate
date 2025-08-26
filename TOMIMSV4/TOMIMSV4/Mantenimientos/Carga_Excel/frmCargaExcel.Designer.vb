<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCargaExcel
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim lblBorra As System.Windows.Forms.Label
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdSalir = New System.Windows.Forms.Button()
        Me.cmdCargar = New System.Windows.Forms.Button()
        Me.txtArchivo = New DevExpress.XtraEditors.ButtonEdit()
        Me.GrpSeleccion = New DevExpress.XtraEditors.GroupControl()
        Me.Grid = New DevExpress.XtraGrid.GridControl()
        Me.DataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsExcel = New TOMWMS.DsExcel()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccionar = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colHoja = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GrpBorraTabla = New System.Windows.Forms.GroupBox()
        Me.lblTipo = New System.Windows.Forms.Label()
        Me.chkBorraTabla = New DevExpress.XtraEditors.CheckEdit()
        Me.pbar = New System.Windows.Forms.ProgressBar()
        Me.lblOperadores = New System.Windows.Forms.Label()
        Me.cmbOperadores = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkInsertInvt = New DevExpress.XtraEditors.CheckEdit()
        Me.grpInsertaInv = New System.Windows.Forms.GroupBox()
        Me.gbErrores = New System.Windows.Forms.GroupBox()
        Me.lblPrg = New System.Windows.Forms.RichTextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblTLog = New DevExpress.XtraEditors.LabelControl()
        lblBorra = New System.Windows.Forms.Label()
        CType(Me.txtArchivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpSeleccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpSeleccion.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpBorraTabla.SuspendLayout()
        CType(Me.chkBorraTabla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOperadores.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInsertInvt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpInsertaInv.SuspendLayout()
        Me.gbErrores.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblBorra
        '
        lblBorra.AutoSize = True
        lblBorra.Location = New System.Drawing.Point(8, 50)
        lblBorra.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        lblBorra.Name = "lblBorra"
        lblBorra.Size = New System.Drawing.Size(82, 16)
        lblBorra.TabIndex = 0
        lblBorra.Text = "Borra Tabla:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 21)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Archivo"
        '
        'cmdSalir
        '
        Me.cmdSalir.Location = New System.Drawing.Point(588, 450)
        Me.cmdSalir.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(127, 30)
        Me.cmdSalir.TabIndex = 5
        Me.cmdSalir.Text = "Salir"
        Me.cmdSalir.UseVisualStyleBackColor = True
        '
        'cmdCargar
        '
        Me.cmdCargar.Location = New System.Drawing.Point(419, 450)
        Me.cmdCargar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdCargar.Name = "cmdCargar"
        Me.cmdCargar.Size = New System.Drawing.Size(127, 30)
        Me.cmdCargar.TabIndex = 4
        Me.cmdCargar.Text = "Cargar"
        Me.cmdCargar.UseVisualStyleBackColor = True
        '
        'txtArchivo
        '
        Me.txtArchivo.Location = New System.Drawing.Point(131, 17)
        Me.txtArchivo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtArchivo.Name = "txtArchivo"
        Me.txtArchivo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, True, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.txtArchivo.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtArchivo.Size = New System.Drawing.Size(447, 22)
        Me.txtArchivo.TabIndex = 1
        '
        'GrpSeleccion
        '
        Me.GrpSeleccion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GrpSeleccion.Controls.Add(Me.Grid)
        Me.GrpSeleccion.Location = New System.Drawing.Point(17, 272)
        Me.GrpSeleccion.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GrpSeleccion.Name = "GrpSeleccion"
        Me.GrpSeleccion.Size = New System.Drawing.Size(561, 167)
        Me.GrpSeleccion.TabIndex = 3
        Me.GrpSeleccion.Text = "Selección de Hoja"
        '
        'Grid
        '
        Me.Grid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Grid.DataSource = Me.DataBindingSource
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Grid.Location = New System.Drawing.Point(2, 28)
        Me.Grid.MainView = Me.GridView1
        Me.Grid.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemTextEdit1})
        Me.Grid.Size = New System.Drawing.Size(557, 137)
        Me.Grid.TabIndex = 0
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'DataBindingSource
        '
        Me.DataBindingSource.DataMember = "Data"
        Me.DataBindingSource.DataSource = Me.DsExcel
        '
        'DsExcel
        '
        Me.DsExcel.DataSetName = "DsExcel"
        Me.DsExcel.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccionar, Me.colHoja})
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.Grid
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsEditForm.PopupEditFormWidth = 1067
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'colSeleccionar
        '
        Me.colSeleccionar.Caption = "Asignar"
        Me.colSeleccionar.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colSeleccionar.FieldName = "Seleccionar"
        Me.colSeleccionar.MinWidth = 27
        Me.colSeleccionar.Name = "colSeleccionar"
        Me.colSeleccionar.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccionar.OptionsFilter.ImmediateUpdatePopupDateFilterOnCheck = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccionar.OptionsFilter.ImmediateUpdatePopupDateFilterOnDateChange = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccionar.OptionsFilter.ShowBlanksFilterItems = DevExpress.Utils.DefaultBoolean.[True]
        Me.colSeleccionar.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.colSeleccionar.Visible = True
        Me.colSeleccionar.VisibleIndex = 0
        Me.colSeleccionar.Width = 100
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colHoja
        '
        Me.colHoja.FieldName = "Hoja"
        Me.colHoja.MinWidth = 27
        Me.colHoja.Name = "colHoja"
        Me.colHoja.OptionsColumn.ReadOnly = True
        Me.colHoja.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.colHoja.Visible = True
        Me.colHoja.VisibleIndex = 1
        Me.colHoja.Width = 100
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'GrpBorraTabla
        '
        Me.GrpBorraTabla.Controls.Add(Me.lblTipo)
        Me.GrpBorraTabla.Controls.Add(lblBorra)
        Me.GrpBorraTabla.Controls.Add(Me.chkBorraTabla)
        Me.GrpBorraTabla.Location = New System.Drawing.Point(17, 137)
        Me.GrpBorraTabla.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GrpBorraTabla.Name = "GrpBorraTabla"
        Me.GrpBorraTabla.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GrpBorraTabla.Size = New System.Drawing.Size(559, 126)
        Me.GrpBorraTabla.TabIndex = 2
        Me.GrpBorraTabla.TabStop = False
        '
        'lblTipo
        '
        Me.lblTipo.AutoSize = True
        Me.lblTipo.Location = New System.Drawing.Point(8, 82)
        Me.lblTipo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.Size = New System.Drawing.Size(19, 16)
        Me.lblTipo.TabIndex = 2
        Me.lblTipo.Text = "---"
        '
        'chkBorraTabla
        '
        Me.chkBorraTabla.Location = New System.Drawing.Point(116, 46)
        Me.chkBorraTabla.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkBorraTabla.Name = "chkBorraTabla"
        Me.chkBorraTabla.Properties.Caption = ""
        Me.chkBorraTabla.Size = New System.Drawing.Size(100, 24)
        Me.chkBorraTabla.TabIndex = 1
        '
        'pbar
        '
        Me.pbar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pbar.Location = New System.Drawing.Point(0, 531)
        Me.pbar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pbar.Name = "pbar"
        Me.pbar.Size = New System.Drawing.Size(1069, 7)
        Me.pbar.TabIndex = 13
        Me.pbar.Visible = False
        '
        'lblOperadores
        '
        Me.lblOperadores.AutoSize = True
        Me.lblOperadores.Location = New System.Drawing.Point(29, 55)
        Me.lblOperadores.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOperadores.Name = "lblOperadores"
        Me.lblOperadores.Size = New System.Drawing.Size(86, 16)
        Me.lblOperadores.TabIndex = 19
        Me.lblOperadores.Text = "Operadores: "
        Me.lblOperadores.Visible = False
        '
        'cmbOperadores
        '
        Me.cmbOperadores.Location = New System.Drawing.Point(128, 52)
        Me.cmbOperadores.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbOperadores.Name = "cmbOperadores"
        Me.cmbOperadores.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperadores.Properties.NullText = ""
        Me.cmbOperadores.Size = New System.Drawing.Size(228, 22)
        Me.cmbOperadores.TabIndex = 18
        Me.cmbOperadores.Visible = False
        '
        'chkInsertInvt
        '
        Me.chkInsertInvt.Location = New System.Drawing.Point(128, 21)
        Me.chkInsertInvt.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkInsertInvt.Name = "chkInsertInvt"
        Me.chkInsertInvt.Properties.Caption = "Insertar como inventario inicial"
        Me.chkInsertInvt.Size = New System.Drawing.Size(228, 24)
        Me.chkInsertInvt.TabIndex = 17
        '
        'grpInsertaInv
        '
        Me.grpInsertaInv.Controls.Add(Me.chkInsertInvt)
        Me.grpInsertaInv.Controls.Add(Me.lblOperadores)
        Me.grpInsertaInv.Controls.Add(Me.cmbOperadores)
        Me.grpInsertaInv.Location = New System.Drawing.Point(20, 47)
        Me.grpInsertaInv.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grpInsertaInv.Name = "grpInsertaInv"
        Me.grpInsertaInv.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grpInsertaInv.Size = New System.Drawing.Size(557, 87)
        Me.grpInsertaInv.TabIndex = 20
        Me.grpInsertaInv.TabStop = False
        '
        'gbErrores
        '
        Me.gbErrores.Controls.Add(Me.lblPrg)
        Me.gbErrores.Controls.Add(Me.prg)
        Me.gbErrores.Controls.Add(Me.lblTLog)
        Me.gbErrores.Location = New System.Drawing.Point(585, 20)
        Me.gbErrores.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbErrores.Name = "gbErrores"
        Me.gbErrores.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbErrores.Size = New System.Drawing.Size(479, 416)
        Me.gbErrores.TabIndex = 22
        Me.gbErrores.TabStop = False
        Me.gbErrores.Text = "Errores importación Excel"
        '
        'lblPrg
        '
        Me.lblPrg.BackColor = System.Drawing.Color.OldLace
        Me.lblPrg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPrg.Location = New System.Drawing.Point(3, 72)
        Me.lblPrg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblPrg.Name = "lblPrg"
        Me.lblPrg.Size = New System.Drawing.Size(473, 342)
        Me.lblPrg.TabIndex = 5
        Me.lblPrg.Text = ""
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(3, 44)
        Me.prg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(473, 28)
        Me.prg.TabIndex = 4
        Me.prg.Visible = False
        '
        'lblTLog
        '
        Me.lblTLog.Appearance.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTLog.Appearance.Options.UseFont = True
        Me.lblTLog.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblTLog.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.lblTLog.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTLog.Location = New System.Drawing.Point(3, 17)
        Me.lblTLog.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblTLog.Name = "lblTLog"
        Me.lblTLog.Size = New System.Drawing.Size(473, 27)
        Me.lblTLog.TabIndex = 3
        Me.lblTLog.Text = "Log"
        '
        'frmCargaExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1069, 538)
        Me.Controls.Add(Me.gbErrores)
        Me.Controls.Add(Me.grpInsertaInv)
        Me.Controls.Add(Me.pbar)
        Me.Controls.Add(Me.GrpBorraTabla)
        Me.Controls.Add(Me.cmdCargar)
        Me.Controls.Add(Me.GrpSeleccion)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.txtArchivo)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmCargaExcel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.txtArchivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpSeleccion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpSeleccion.ResumeLayout(False)
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpBorraTabla.ResumeLayout(False)
        Me.GrpBorraTabla.PerformLayout()
        CType(Me.chkBorraTabla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOperadores.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInsertInvt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpInsertaInv.ResumeLayout(False)
        Me.grpInsertaInv.PerformLayout()
        Me.gbErrores.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdCargar As System.Windows.Forms.Button
    Friend WithEvents txtArchivo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents GrpSeleccion As DevExpress.XtraEditors.GroupControl
    Private WithEvents Grid As DevExpress.XtraGrid.GridControl
    Private WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSeleccionar As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colHoja As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsExcel As TOMWMS.DsExcel
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GrpBorraTabla As System.Windows.Forms.GroupBox
    Friend WithEvents chkBorraTabla As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblTipo As System.Windows.Forms.Label
    Friend WithEvents pbar As ProgressBar
    Friend WithEvents lblOperadores As Label
    Friend WithEvents cmbOperadores As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents chkInsertInvt As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents grpInsertaInv As GroupBox
    Friend WithEvents gbErrores As GroupBox
    Friend WithEvents lblPrg As RichTextBox
    Friend WithEvents prg As ProgressBar
    Friend WithEvents lblTLog As DevExpress.XtraEditors.LabelControl
End Class
