<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCargaExcel_Inv_Ini_Op_Log
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
        Me.pbar = New System.Windows.Forms.ProgressBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gbErrores = New System.Windows.Forms.GroupBox()
        Me.lblprg = New System.Windows.Forms.RichTextBox()
        Me.prg = New System.Windows.Forms.ProgressBar()
        Me.lblTLog = New DevExpress.XtraEditors.LabelControl()
        CType(Me.txtArchivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpSeleccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpSeleccion.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.gbErrores.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 21)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Archivo"
        '
        'cmdSalir
        '
        Me.cmdSalir.Location = New System.Drawing.Point(712, 22)
        Me.cmdSalir.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(127, 30)
        Me.cmdSalir.TabIndex = 5
        Me.cmdSalir.Text = "Salir"
        Me.cmdSalir.UseVisualStyleBackColor = True
        '
        'cmdCargar
        '
        Me.cmdCargar.Location = New System.Drawing.Point(578, 22)
        Me.cmdCargar.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdCargar.Name = "cmdCargar"
        Me.cmdCargar.Size = New System.Drawing.Size(127, 30)
        Me.cmdCargar.TabIndex = 4
        Me.cmdCargar.Text = "Cargar"
        Me.cmdCargar.UseVisualStyleBackColor = True
        '
        'txtArchivo
        '
        Me.txtArchivo.Location = New System.Drawing.Point(131, 17)
        Me.txtArchivo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtArchivo.Name = "txtArchivo"
        Me.txtArchivo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, True, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.txtArchivo.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtArchivo.Size = New System.Drawing.Size(1193, 22)
        Me.txtArchivo.TabIndex = 1
        '
        'GrpSeleccion
        '
        Me.GrpSeleccion.Controls.Add(Me.Grid)
        Me.GrpSeleccion.Location = New System.Drawing.Point(17, 47)
        Me.GrpSeleccion.Margin = New System.Windows.Forms.Padding(4)
        Me.GrpSeleccion.Name = "GrpSeleccion"
        Me.GrpSeleccion.Size = New System.Drawing.Size(690, 271)
        Me.GrpSeleccion.TabIndex = 3
        Me.GrpSeleccion.Text = "Selección de Hoja"
        '
        'Grid
        '
        Me.Grid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Grid.DataSource = Me.DataBindingSource
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.Grid.Location = New System.Drawing.Point(2, 28)
        Me.Grid.MainView = Me.GridView1
        Me.Grid.Margin = New System.Windows.Forms.Padding(4)
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemTextEdit1})
        Me.Grid.Size = New System.Drawing.Size(686, 241)
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
        'pbar
        '
        Me.pbar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pbar.Location = New System.Drawing.Point(0, 383)
        Me.pbar.Margin = New System.Windows.Forms.Padding(4)
        Me.pbar.Name = "pbar"
        Me.pbar.Size = New System.Drawing.Size(1336, 28)
        Me.pbar.TabIndex = 13
        Me.pbar.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdSalir)
        Me.GroupBox1.Controls.Add(Me.cmdCargar)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 325)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1336, 58)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        '
        'gbErrores
        '
        Me.gbErrores.Controls.Add(Me.lblprg)
        Me.gbErrores.Controls.Add(Me.prg)
        Me.gbErrores.Controls.Add(Me.lblTLog)
        Me.gbErrores.Location = New System.Drawing.Point(714, 47)
        Me.gbErrores.Name = "gbErrores"
        Me.gbErrores.Size = New System.Drawing.Size(610, 271)
        Me.gbErrores.TabIndex = 15
        Me.gbErrores.TabStop = False
        Me.gbErrores.Text = "Errores importación Excel"
        '
        'lblprg
        '
        Me.lblprg.BackColor = System.Drawing.Color.OldLace
        Me.lblprg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblprg.Location = New System.Drawing.Point(3, 73)
        Me.lblprg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblprg.Name = "lblprg"
        Me.lblprg.Size = New System.Drawing.Size(604, 195)
        Me.lblprg.TabIndex = 5
        Me.lblprg.Text = ""
        '
        'prg
        '
        Me.prg.Dock = System.Windows.Forms.DockStyle.Top
        Me.prg.Location = New System.Drawing.Point(3, 45)
        Me.prg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.prg.Name = "prg"
        Me.prg.Size = New System.Drawing.Size(604, 28)
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
        Me.lblTLog.Location = New System.Drawing.Point(3, 18)
        Me.lblTLog.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblTLog.Name = "lblTLog"
        Me.lblTLog.Size = New System.Drawing.Size(604, 27)
        Me.lblTLog.TabIndex = 3
        Me.lblTLog.Text = "Log"
        '
        'frmCargaExcel_Inv_Ini_Op_Log
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1336, 411)
        Me.Controls.Add(Me.gbErrores)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.pbar)
        Me.Controls.Add(Me.GrpSeleccion)
        Me.Controls.Add(Me.txtArchivo)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmCargaExcel_Inv_Ini_Op_Log"
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
        Me.GroupBox1.ResumeLayout(False)
        Me.gbErrores.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents pbar As ProgressBar
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents gbErrores As GroupBox
    Friend WithEvents lblprg As RichTextBox
    Friend WithEvents prg As ProgressBar
    Friend WithEvents lblTLog As DevExpress.XtraEditors.LabelControl
End Class
