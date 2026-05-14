<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRevisionProducto
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.cmdImprimir = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.lblRevision = New DevExpress.XtraEditors.LabelControl()
        Me.cmdPosponerTodo = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdPosponerSeleccionado = New DevExpress.XtraEditors.SimpleButton()
        Me.cmdEnviarTarea = New DevExpress.XtraEditors.SimpleButton()
        Me.Grid = New DevExpress.XtraGrid.GridControl()
        Me.RevisionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsRevisionProducto = New DsRevisionProducto()
        Me.GridView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSeleccionar = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colProducto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPresentacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUbicacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMinimo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMaximo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCantidad = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdPropietarioBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdProductoBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdPresentacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdProductoEstado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdUnidadMedida = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colIdUbicacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEstado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colCantBodega = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNA = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RevisionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsRevisionProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.cmdImprimir, Me.cmdActualizar})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 2
        '
        'Bar2
        '
        Me.Bar2.BarName = "Menú principal"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmdImprimir), New DevExpress.XtraBars.LinkPersistInfo(Me.cmdActualizar)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Menú principal"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.Caption = "Imprimir"
        Me.cmdImprimir.Id = 0
        Me.cmdImprimir.Name = "cmdImprimir"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 1
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(4)
        Me.barDockControlTop.Size = New System.Drawing.Size(1187, 25)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 500)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(4)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1187, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 25)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(4)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 475)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1187, 25)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(4)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 475)
        '
        'lblRevision
        '
        Me.lblRevision.Appearance.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRevision.Appearance.Options.UseFont = True
        Me.lblRevision.Location = New System.Drawing.Point(43, 43)
        Me.lblRevision.Margin = New System.Windows.Forms.Padding(4)
        Me.lblRevision.Name = "lblRevision"
        Me.lblRevision.Size = New System.Drawing.Size(213, 23)
        Me.lblRevision.TabIndex = 0
        Me.lblRevision.Text = "Revisión de Productos"
        '
        'cmdPosponerTodo
        '
        Me.cmdPosponerTodo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdPosponerTodo.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPosponerTodo.Appearance.Options.UseFont = True
        Me.cmdPosponerTodo.Location = New System.Drawing.Point(16, 455)
        Me.cmdPosponerTodo.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdPosponerTodo.Name = "cmdPosponerTodo"
        Me.cmdPosponerTodo.Size = New System.Drawing.Size(189, 30)
        Me.cmdPosponerTodo.TabIndex = 2
        Me.cmdPosponerTodo.Text = "Posponer Todos"
        '
        'cmdPosponerSeleccionado
        '
        Me.cmdPosponerSeleccionado.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPosponerSeleccionado.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPosponerSeleccionado.Appearance.Options.UseFont = True
        Me.cmdPosponerSeleccionado.Location = New System.Drawing.Point(727, 455)
        Me.cmdPosponerSeleccionado.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdPosponerSeleccionado.Name = "cmdPosponerSeleccionado"
        Me.cmdPosponerSeleccionado.Size = New System.Drawing.Size(247, 30)
        Me.cmdPosponerSeleccionado.TabIndex = 3
        Me.cmdPosponerSeleccionado.Text = "Posponer Seleccionados"
        '
        'cmdEnviarTarea
        '
        Me.cmdEnviarTarea.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEnviarTarea.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEnviarTarea.Appearance.Options.UseFont = True
        Me.cmdEnviarTarea.Location = New System.Drawing.Point(981, 455)
        Me.cmdEnviarTarea.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdEnviarTarea.Name = "cmdEnviarTarea"
        Me.cmdEnviarTarea.Size = New System.Drawing.Size(189, 30)
        Me.cmdEnviarTarea.TabIndex = 4
        Me.cmdEnviarTarea.Text = "Enviar Tarea"
        '
        'Grid
        '
        Me.Grid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grid.Cursor = System.Windows.Forms.Cursors.Default
        Me.Grid.DataSource = Me.RevisionBindingSource
        Me.Grid.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.Grid.Location = New System.Drawing.Point(16, 73)
        Me.Grid.MainView = Me.GridView
        Me.Grid.Margin = New System.Windows.Forms.Padding(4)
        Me.Grid.Name = "Grid"
        Me.Grid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.Grid.Size = New System.Drawing.Size(1155, 375)
        Me.Grid.TabIndex = 1
        Me.Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView})
        '
        'RevisionBindingSource
        '
        Me.RevisionBindingSource.DataMember = "Revision"
        Me.RevisionBindingSource.DataSource = Me.DsRevisionProducto
        '
        'DsRevisionProducto
        '
        Me.DsRevisionProducto.DataSetName = "DsRevisionProducto"
        Me.DsRevisionProducto.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView
        '
        Me.GridView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSeleccionar, Me.colProducto, Me.colPresentacion, Me.colUbicacion, Me.colMinimo, Me.colMaximo, Me.colCantidad, Me.colIdPropietarioBodega, Me.colIdProductoBodega, Me.colIdPresentacion, Me.colIdProductoEstado, Me.colIdUnidadMedida, Me.colIdUbicacion, Me.colEstado, Me.colCantBodega, Me.colNA})
        Me.GridView.DetailHeight = 431
        Me.GridView.GridControl = Me.Grid
        Me.GridView.Name = "GridView"
        Me.GridView.OptionsBehavior.Editable = False
        '
        'colSeleccionar
        '
        Me.colSeleccionar.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.colSeleccionar.FieldName = "Seleccionar"
        Me.colSeleccionar.MinWidth = 27
        Me.colSeleccionar.Name = "colSeleccionar"
        Me.colSeleccionar.Visible = True
        Me.colSeleccionar.VisibleIndex = 0
        Me.colSeleccionar.Width = 100
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'colProducto
        '
        Me.colProducto.FieldName = "Producto"
        Me.colProducto.MinWidth = 27
        Me.colProducto.Name = "colProducto"
        Me.colProducto.OptionsColumn.ReadOnly = True
        Me.colProducto.Visible = True
        Me.colProducto.VisibleIndex = 1
        Me.colProducto.Width = 100
        '
        'colPresentacion
        '
        Me.colPresentacion.FieldName = "Presentacion"
        Me.colPresentacion.MinWidth = 27
        Me.colPresentacion.Name = "colPresentacion"
        Me.colPresentacion.OptionsColumn.ReadOnly = True
        Me.colPresentacion.Visible = True
        Me.colPresentacion.VisibleIndex = 2
        Me.colPresentacion.Width = 100
        '
        'colUbicacion
        '
        Me.colUbicacion.FieldName = "Ubicacion"
        Me.colUbicacion.MinWidth = 27
        Me.colUbicacion.Name = "colUbicacion"
        Me.colUbicacion.OptionsColumn.ReadOnly = True
        Me.colUbicacion.Visible = True
        Me.colUbicacion.VisibleIndex = 4
        Me.colUbicacion.Width = 100
        '
        'colMinimo
        '
        Me.colMinimo.FieldName = "Minimo"
        Me.colMinimo.MinWidth = 27
        Me.colMinimo.Name = "colMinimo"
        Me.colMinimo.OptionsColumn.ReadOnly = True
        Me.colMinimo.Visible = True
        Me.colMinimo.VisibleIndex = 5
        Me.colMinimo.Width = 100
        '
        'colMaximo
        '
        Me.colMaximo.FieldName = "Maximo"
        Me.colMaximo.MinWidth = 27
        Me.colMaximo.Name = "colMaximo"
        Me.colMaximo.OptionsColumn.ReadOnly = True
        Me.colMaximo.Visible = True
        Me.colMaximo.VisibleIndex = 6
        Me.colMaximo.Width = 100
        '
        'colCantidad
        '
        Me.colCantidad.Caption = "Disponible"
        Me.colCantidad.FieldName = "Cantidad"
        Me.colCantidad.MinWidth = 27
        Me.colCantidad.Name = "colCantidad"
        Me.colCantidad.OptionsColumn.ReadOnly = True
        Me.colCantidad.Visible = True
        Me.colCantidad.VisibleIndex = 7
        Me.colCantidad.Width = 100
        '
        'colIdPropietarioBodega
        '
        Me.colIdPropietarioBodega.FieldName = "IdPropietarioBodega"
        Me.colIdPropietarioBodega.MinWidth = 27
        Me.colIdPropietarioBodega.Name = "colIdPropietarioBodega"
        Me.colIdPropietarioBodega.OptionsColumn.ReadOnly = True
        Me.colIdPropietarioBodega.Width = 100
        '
        'colIdProductoBodega
        '
        Me.colIdProductoBodega.FieldName = "IdProductoBodega"
        Me.colIdProductoBodega.MinWidth = 27
        Me.colIdProductoBodega.Name = "colIdProductoBodega"
        Me.colIdProductoBodega.OptionsColumn.ReadOnly = True
        Me.colIdProductoBodega.Width = 100
        '
        'colIdPresentacion
        '
        Me.colIdPresentacion.FieldName = "IdPresentacion"
        Me.colIdPresentacion.MinWidth = 27
        Me.colIdPresentacion.Name = "colIdPresentacion"
        Me.colIdPresentacion.OptionsColumn.ReadOnly = True
        Me.colIdPresentacion.Width = 100
        '
        'colIdProductoEstado
        '
        Me.colIdProductoEstado.FieldName = "IdProductoEstado"
        Me.colIdProductoEstado.MinWidth = 27
        Me.colIdProductoEstado.Name = "colIdProductoEstado"
        Me.colIdProductoEstado.OptionsColumn.ReadOnly = True
        Me.colIdProductoEstado.Width = 100
        '
        'colIdUnidadMedida
        '
        Me.colIdUnidadMedida.FieldName = "IdUnidadMedida"
        Me.colIdUnidadMedida.MinWidth = 27
        Me.colIdUnidadMedida.Name = "colIdUnidadMedida"
        Me.colIdUnidadMedida.OptionsColumn.ReadOnly = True
        Me.colIdUnidadMedida.Width = 100
        '
        'colIdUbicacion
        '
        Me.colIdUbicacion.FieldName = "IdUbicacion"
        Me.colIdUbicacion.MinWidth = 27
        Me.colIdUbicacion.Name = "colIdUbicacion"
        Me.colIdUbicacion.OptionsColumn.ReadOnly = True
        Me.colIdUbicacion.Width = 100
        '
        'colEstado
        '
        Me.colEstado.FieldName = "Estado"
        Me.colEstado.MinWidth = 27
        Me.colEstado.Name = "colEstado"
        Me.colEstado.OptionsColumn.ReadOnly = True
        Me.colEstado.Visible = True
        Me.colEstado.VisibleIndex = 3
        Me.colEstado.Width = 100
        '
        'colCantBodega
        '
        Me.colCantBodega.FieldName = "CantBodega"
        Me.colCantBodega.MinWidth = 27
        Me.colCantBodega.Name = "colCantBodega"
        Me.colCantBodega.OptionsColumn.ReadOnly = True
        Me.colCantBodega.Visible = True
        Me.colCantBodega.VisibleIndex = 8
        Me.colCantBodega.Width = 100
        '
        'colNA
        '
        Me.colNA.FieldName = "NA"
        Me.colNA.MinWidth = 27
        Me.colNA.Name = "colNA"
        Me.colNA.OptionsColumn.ReadOnly = True
        Me.colNA.Width = 100
        '
        'frmRevisionProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1187, 500)
        Me.Controls.Add(Me.Grid)
        Me.Controls.Add(Me.cmdEnviarTarea)
        Me.Controls.Add(Me.cmdPosponerSeleccionado)
        Me.Controls.Add(Me.cmdPosponerTodo)
        Me.Controls.Add(Me.lblRevision)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmRevisionProducto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RevisionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsRevisionProducto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents cmdImprimir As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents lblRevision As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmdEnviarTarea As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdPosponerSeleccionado As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmdPosponerTodo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Grid As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RevisionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DsRevisionProducto As DsRevisionProducto
    Friend WithEvents colSeleccionar As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPresentacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUbicacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMinimo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaximo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantidad As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdPropietarioBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdProductoBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdPresentacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdProductoEstado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdUnidadMedida As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIdUbicacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEstado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCantBodega As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
