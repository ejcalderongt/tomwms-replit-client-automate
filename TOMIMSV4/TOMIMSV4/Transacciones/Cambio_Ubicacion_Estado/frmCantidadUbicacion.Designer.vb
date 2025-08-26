<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCantidadUbicacion
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If pListObjDet IsNot Nothing Then
                pListObjDet.Dispose()
                pListObjDet = Nothing
            End If
            If pListObjDet IsNot Nothing Then
                pListObjDet.Dispose()
                pListObjDet = Nothing
            End If
            If objStockRef IsNot Nothing Then
                objStockRef.Dispose()
                objStockRef = Nothing
            End If
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
        Dim Label2 As System.Windows.Forms.Label
        Dim Label20 As System.Windows.Forms.Label
        Dim Label21 As System.Windows.Forms.Label
        Dim Label22 As System.Windows.Forms.Label
        Dim Label18 As System.Windows.Forms.Label
        Dim Label19 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label17 As System.Windows.Forms.Label
        Dim Label16 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim lblCantidad As System.Windows.Forms.Label
        Dim Label24 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim lblIdStock As System.Windows.Forms.Label
        Dim IdPropietarioLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCantidadUbicacion))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuConfirmarCantidad = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.groupCambioDeEstado = New DevExpress.XtraEditors.GroupControl()
        Me.txtIdEstado = New DevExpress.XtraEditors.TextEdit()
        Me.lnkCambioDeEstado = New System.Windows.Forms.LinkLabel()
        Me.txtNombreEstado = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbOperador = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblVolumen = New System.Windows.Forms.Label()
        Me.lblCantRef = New System.Windows.Forms.Label()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.txtIngreso = New DevExpress.XtraEditors.TextEdit()
        Me.txtPresentacion = New DevExpress.XtraEditors.TextEdit()
        Me.txtAñada = New DevExpress.XtraEditors.TextEdit()
        Me.txtUnidadMedida = New DevExpress.XtraEditors.TextEdit()
        Me.txtEstado = New DevExpress.XtraEditors.TextEdit()
        Me.txtVence = New DevExpress.XtraEditors.TextEdit()
        Me.txtSerie = New DevExpress.XtraEditors.TextEdit()
        Me.txtProducto = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdStock = New DevExpress.XtraEditors.TextEdit()
        Me.txtIdOrigen = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.lblFactor = New System.Windows.Forms.Label()
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown()
        Me.txtIdUbicacionDestino = New DevExpress.XtraEditors.TextEdit()
        Me.chkActivoDet = New DevExpress.XtraEditors.CheckEdit()
        Me.txtUbicacionDestino = New DevExpress.XtraEditors.TextEdit()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.chkRealizadoDet = New DevExpress.XtraEditors.CheckEdit()
        Label2 = New System.Windows.Forms.Label()
        Label20 = New System.Windows.Forms.Label()
        Label21 = New System.Windows.Forms.Label()
        Label22 = New System.Windows.Forms.Label()
        Label18 = New System.Windows.Forms.Label()
        Label19 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        Label16 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        lblCantidad = New System.Windows.Forms.Label()
        Label24 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        lblIdStock = New System.Windows.Forms.Label()
        IdPropietarioLabel = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.groupCambioDeEstado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupCambioDeEstado.SuspendLayout()
        CType(Me.txtIdEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombreEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.cmbOperador.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPresentacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAñada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUnidadMedida.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdStock.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdOrigen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIdUbicacionDestino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivoDet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUbicacionDestino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRealizadoDet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(308, 270)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(62, 16)
        Label2.TabIndex = 24
        Label2.Text = "Cantidad:"
        '
        'Label20
        '
        Label20.AutoSize = True
        Label20.Location = New System.Drawing.Point(308, 202)
        Label20.Name = "Label20"
        Label20.Size = New System.Drawing.Size(36, 16)
        Label20.TabIndex = 16
        Label20.Text = "Lote:"
        '
        'Label21
        '
        Label21.AutoSize = True
        Label21.Location = New System.Drawing.Point(17, 233)
        Label21.Name = "Label21"
        Label21.Size = New System.Drawing.Size(55, 16)
        Label21.TabIndex = 18
        Label21.Text = "Ingreso:"
        '
        'Label22
        '
        Label22.AutoSize = True
        Label22.Location = New System.Drawing.Point(308, 233)
        Label22.Name = "Label22"
        Label22.Size = New System.Drawing.Size(85, 16)
        Label22.TabIndex = 20
        Label22.Text = "Presentación:"
        '
        'Label18
        '
        Label18.AutoSize = True
        Label18.Location = New System.Drawing.Point(17, 202)
        Label18.Name = "Label18"
        Label18.Size = New System.Drawing.Size(48, 16)
        Label18.TabIndex = 14
        Label18.Text = "Añada:"
        '
        'Label19
        '
        Label19.AutoSize = True
        Label19.Location = New System.Drawing.Point(17, 268)
        Label19.Name = "Label19"
        Label19.Size = New System.Drawing.Size(69, 16)
        Label19.TabIndex = 22
        Label19.Text = "U. Medida:"
        '
        'Label13
        '
        Label13.AutoSize = True
        Label13.Location = New System.Drawing.Point(17, 169)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(42, 16)
        Label13.TabIndex = 10
        Label13.Text = "Serie:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(16, 137)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(62, 16)
        Label8.TabIndex = 6
        Label8.Text = "Producto:"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(308, 137)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(47, 16)
        Label10.TabIndex = 8
        Label10.Text = "Vence:"
        '
        'Label17
        '
        Label17.AutoSize = True
        Label17.Location = New System.Drawing.Point(308, 170)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(50, 16)
        Label17.TabIndex = 12
        Label17.Text = "Estado:"
        '
        'Label16
        '
        Label16.AutoSize = True
        Label16.Location = New System.Drawing.Point(308, 101)
        Label16.Name = "Label16"
        Label16.Size = New System.Drawing.Size(78, 16)
        Label16.TabIndex = 4
        Label16.Text = "Ubic Origen:"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(9, 112)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(48, 16)
        Label11.TabIndex = 5
        Label11.Text = "Factor:"
        '
        'lblCantidad
        '
        lblCantidad.AutoSize = True
        lblCantidad.Location = New System.Drawing.Point(9, 68)
        lblCantidad.Name = "lblCantidad"
        lblCantidad.Size = New System.Drawing.Size(62, 16)
        lblCantidad.TabIndex = 3
        lblCantidad.Text = "Cantidad:"
        '
        'Label24
        '
        Label24.AutoSize = True
        Label24.Location = New System.Drawing.Point(127, 142)
        Label24.Name = "Label24"
        Label24.Size = New System.Drawing.Size(46, 16)
        Label24.TabIndex = 9
        Label24.Text = "Activo:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(9, 142)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(67, 16)
        Label9.TabIndex = 7
        Label9.Text = "Realizado:"
        '
        'lblIdStock
        '
        lblIdStock.AutoSize = True
        lblIdStock.Location = New System.Drawing.Point(17, 101)
        lblIdStock.Name = "lblIdStock"
        lblIdStock.Size = New System.Drawing.Size(54, 16)
        lblIdStock.TabIndex = 2
        lblIdStock.Text = "IdStock:"
        '
        'IdPropietarioLabel
        '
        IdPropietarioLabel.AutoSize = True
        IdPropietarioLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        IdPropietarioLabel.Location = New System.Drawing.Point(129, 48)
        IdPropietarioLabel.Name = "IdPropietarioLabel"
        IdPropietarioLabel.Size = New System.Drawing.Size(101, 25)
        IdPropietarioLabel.TabIndex = 0
        IdPropietarioLabel.Text = "Operador:"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.RibbonControl.SearchEditItem, Me.mnuConfirmarCantidad})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 2
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1120, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuConfirmarCantidad
        '
        Me.mnuConfirmarCantidad.Caption = "Confirmar"
        Me.mnuConfirmarCantidad.Id = 1
        Me.mnuConfirmarCantidad.ImageOptions.SvgImage = CType(resources.GetObject("mnuConfirmarCantidad.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuConfirmarCantidad.Name = "mnuConfirmarCantidad"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Menú"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuConfirmarCantidad)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 526)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1120, 30)
        '
        'groupCambioDeEstado
        '
        Me.groupCambioDeEstado.Controls.Add(Me.txtIdEstado)
        Me.groupCambioDeEstado.Controls.Add(Me.lnkCambioDeEstado)
        Me.groupCambioDeEstado.Controls.Add(Me.txtNombreEstado)
        Me.groupCambioDeEstado.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.groupCambioDeEstado.Location = New System.Drawing.Point(651, 384)
        Me.groupCambioDeEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.groupCambioDeEstado.Name = "groupCambioDeEstado"
        Me.groupCambioDeEstado.Size = New System.Drawing.Size(469, 142)
        Me.groupCambioDeEstado.TabIndex = 2
        Me.groupCambioDeEstado.Text = "Cambiar de Estado"
        '
        'txtIdEstado
        '
        Me.txtIdEstado.Location = New System.Drawing.Point(101, 46)
        Me.txtIdEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdEstado.MenuManager = Me.RibbonControl
        Me.txtIdEstado.Name = "txtIdEstado"
        Me.txtIdEstado.Properties.Mask.EditMask = "n0"
        Me.txtIdEstado.Size = New System.Drawing.Size(77, 22)
        Me.txtIdEstado.TabIndex = 1
        '
        'lnkCambioDeEstado
        '
        Me.lnkCambioDeEstado.AutoSize = True
        Me.lnkCambioDeEstado.Location = New System.Drawing.Point(48, 49)
        Me.lnkCambioDeEstado.Name = "lnkCambioDeEstado"
        Me.lnkCambioDeEstado.Size = New System.Drawing.Size(45, 16)
        Me.lnkCambioDeEstado.TabIndex = 0
        Me.lnkCambioDeEstado.TabStop = True
        Me.lnkCambioDeEstado.Text = "Estado"
        '
        'txtNombreEstado
        '
        Me.txtNombreEstado.Location = New System.Drawing.Point(185, 46)
        Me.txtNombreEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreEstado.MenuManager = Me.RibbonControl
        Me.txtNombreEstado.Name = "txtNombreEstado"
        Me.txtNombreEstado.Properties.Mask.EditMask = "n0"
        Me.txtNombreEstado.Properties.ReadOnly = True
        Me.txtNombreEstado.Size = New System.Drawing.Size(190, 22)
        Me.txtNombreEstado.TabIndex = 2
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.cmbOperador)
        Me.GroupControl4.Controls.Add(Me.lblVolumen)
        Me.GroupControl4.Controls.Add(IdPropietarioLabel)
        Me.GroupControl4.Controls.Add(lblIdStock)
        Me.GroupControl4.Controls.Add(Me.lblCantRef)
        Me.GroupControl4.Controls.Add(Label2)
        Me.GroupControl4.Controls.Add(Me.txtLote)
        Me.GroupControl4.Controls.Add(Me.txtIngreso)
        Me.GroupControl4.Controls.Add(Label20)
        Me.GroupControl4.Controls.Add(Label21)
        Me.GroupControl4.Controls.Add(Label22)
        Me.GroupControl4.Controls.Add(Me.txtPresentacion)
        Me.GroupControl4.Controls.Add(Me.txtAñada)
        Me.GroupControl4.Controls.Add(Me.txtUnidadMedida)
        Me.GroupControl4.Controls.Add(Label18)
        Me.GroupControl4.Controls.Add(Label19)
        Me.GroupControl4.Controls.Add(Me.txtEstado)
        Me.GroupControl4.Controls.Add(Label13)
        Me.GroupControl4.Controls.Add(Me.txtVence)
        Me.GroupControl4.Controls.Add(Me.txtSerie)
        Me.GroupControl4.Controls.Add(Me.txtProducto)
        Me.GroupControl4.Controls.Add(Label8)
        Me.GroupControl4.Controls.Add(Label10)
        Me.GroupControl4.Controls.Add(Me.txtIdStock)
        Me.GroupControl4.Controls.Add(Label17)
        Me.GroupControl4.Controls.Add(Me.txtIdOrigen)
        Me.GroupControl4.Controls.Add(Label16)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupControl4.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(651, 333)
        Me.GroupControl4.TabIndex = 0
        Me.GroupControl4.Text = "Origen"
        '
        'cmbOperador
        '
        Me.cmbOperador.Location = New System.Drawing.Point(230, 50)
        Me.cmbOperador.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbOperador.MenuManager = Me.RibbonControl
        Me.cmbOperador.Name = "cmbOperador"
        Me.cmbOperador.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbOperador.Properties.NullText = ""
        Me.cmbOperador.Size = New System.Drawing.Size(292, 22)
        Me.cmbOperador.TabIndex = 27
        '
        'lblVolumen
        '
        Me.lblVolumen.AutoSize = True
        Me.lblVolumen.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVolumen.Location = New System.Drawing.Point(394, 303)
        Me.lblVolumen.Name = "lblVolumen"
        Me.lblVolumen.Size = New System.Drawing.Size(103, 24)
        Me.lblVolumen.TabIndex = 26
        Me.lblVolumen.Text = "0x0x0=X"
        '
        'lblCantRef
        '
        Me.lblCantRef.AutoSize = True
        Me.lblCantRef.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.lblCantRef.Location = New System.Drawing.Point(397, 266)
        Me.lblCantRef.Name = "lblCantRef"
        Me.lblCantRef.Size = New System.Drawing.Size(16, 21)
        Me.lblCantRef.TabIndex = 25
        Me.lblCantRef.Text = "-"
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(399, 197)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLote.MenuManager = Me.RibbonControl
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Properties.Mask.EditMask = "n0"
        Me.txtLote.Properties.ReadOnly = True
        Me.txtLote.Size = New System.Drawing.Size(241, 22)
        Me.txtLote.TabIndex = 17
        '
        'txtIngreso
        '
        Me.txtIngreso.Location = New System.Drawing.Point(86, 229)
        Me.txtIngreso.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIngreso.MenuManager = Me.RibbonControl
        Me.txtIngreso.Name = "txtIngreso"
        Me.txtIngreso.Properties.Mask.EditMask = "n0"
        Me.txtIngreso.Properties.ReadOnly = True
        Me.txtIngreso.Size = New System.Drawing.Size(212, 22)
        Me.txtIngreso.TabIndex = 19
        '
        'txtPresentacion
        '
        Me.txtPresentacion.Location = New System.Drawing.Point(399, 229)
        Me.txtPresentacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPresentacion.MenuManager = Me.RibbonControl
        Me.txtPresentacion.Name = "txtPresentacion"
        Me.txtPresentacion.Properties.Mask.EditMask = "n0"
        Me.txtPresentacion.Properties.ReadOnly = True
        Me.txtPresentacion.Size = New System.Drawing.Size(241, 22)
        Me.txtPresentacion.TabIndex = 21
        '
        'txtAñada
        '
        Me.txtAñada.Location = New System.Drawing.Point(86, 197)
        Me.txtAñada.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtAñada.MenuManager = Me.RibbonControl
        Me.txtAñada.Name = "txtAñada"
        Me.txtAñada.Properties.Mask.EditMask = "n0"
        Me.txtAñada.Properties.ReadOnly = True
        Me.txtAñada.Size = New System.Drawing.Size(212, 22)
        Me.txtAñada.TabIndex = 15
        '
        'txtUnidadMedida
        '
        Me.txtUnidadMedida.Location = New System.Drawing.Point(86, 261)
        Me.txtUnidadMedida.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUnidadMedida.MenuManager = Me.RibbonControl
        Me.txtUnidadMedida.Name = "txtUnidadMedida"
        Me.txtUnidadMedida.Properties.Mask.EditMask = "n0"
        Me.txtUnidadMedida.Properties.ReadOnly = True
        Me.txtUnidadMedida.Size = New System.Drawing.Size(212, 22)
        Me.txtUnidadMedida.TabIndex = 23
        '
        'txtEstado
        '
        Me.txtEstado.Location = New System.Drawing.Point(399, 166)
        Me.txtEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtEstado.MenuManager = Me.RibbonControl
        Me.txtEstado.Name = "txtEstado"
        Me.txtEstado.Properties.Mask.EditMask = "n0"
        Me.txtEstado.Properties.ReadOnly = True
        Me.txtEstado.Size = New System.Drawing.Size(241, 22)
        Me.txtEstado.TabIndex = 13
        '
        'txtVence
        '
        Me.txtVence.Location = New System.Drawing.Point(399, 133)
        Me.txtVence.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtVence.MenuManager = Me.RibbonControl
        Me.txtVence.Name = "txtVence"
        Me.txtVence.Properties.Mask.EditMask = "n0"
        Me.txtVence.Properties.ReadOnly = True
        Me.txtVence.Size = New System.Drawing.Size(241, 22)
        Me.txtVence.TabIndex = 9
        '
        'txtSerie
        '
        Me.txtSerie.Location = New System.Drawing.Point(86, 166)
        Me.txtSerie.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSerie.MenuManager = Me.RibbonControl
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Properties.Mask.EditMask = "n0"
        Me.txtSerie.Properties.ReadOnly = True
        Me.txtSerie.Size = New System.Drawing.Size(212, 22)
        Me.txtSerie.TabIndex = 11
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(86, 133)
        Me.txtProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtProducto.MenuManager = Me.RibbonControl
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Properties.Mask.EditMask = "n0"
        Me.txtProducto.Properties.ReadOnly = True
        Me.txtProducto.Size = New System.Drawing.Size(212, 22)
        Me.txtProducto.TabIndex = 7
        '
        'txtIdStock
        '
        Me.txtIdStock.Location = New System.Drawing.Point(86, 97)
        Me.txtIdStock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdStock.MenuManager = Me.RibbonControl
        Me.txtIdStock.Name = "txtIdStock"
        Me.txtIdStock.Properties.Mask.EditMask = "n0"
        Me.txtIdStock.Properties.ReadOnly = True
        Me.txtIdStock.Size = New System.Drawing.Size(212, 22)
        Me.txtIdStock.TabIndex = 3
        '
        'txtIdOrigen
        '
        Me.txtIdOrigen.Location = New System.Drawing.Point(399, 97)
        Me.txtIdOrigen.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdOrigen.MenuManager = Me.RibbonControl
        Me.txtIdOrigen.Name = "txtIdOrigen"
        Me.txtIdOrigen.Properties.Mask.EditMask = "n0"
        Me.txtIdOrigen.Properties.ReadOnly = True
        Me.txtIdOrigen.Size = New System.Drawing.Size(241, 22)
        Me.txtIdOrigen.TabIndex = 5
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.lblFactor)
        Me.GroupControl5.Controls.Add(Label11)
        Me.GroupControl5.Controls.Add(lblCantidad)
        Me.GroupControl5.Controls.Add(Me.txtCantidad)
        Me.GroupControl5.Controls.Add(Me.txtIdUbicacionDestino)
        Me.GroupControl5.Controls.Add(Label24)
        Me.GroupControl5.Controls.Add(Me.chkActivoDet)
        Me.GroupControl5.Controls.Add(Me.txtUbicacionDestino)
        Me.GroupControl5.Controls.Add(Me.LinkLabel1)
        Me.GroupControl5.Controls.Add(Label9)
        Me.GroupControl5.Controls.Add(Me.chkRealizadoDet)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl5.Location = New System.Drawing.Point(651, 193)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(469, 191)
        Me.GroupControl5.TabIndex = 1
        Me.GroupControl5.Text = "Destino"
        '
        'lblFactor
        '
        Me.lblFactor.AutoSize = True
        Me.lblFactor.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.lblFactor.Location = New System.Drawing.Point(98, 108)
        Me.lblFactor.Name = "lblFactor"
        Me.lblFactor.Size = New System.Drawing.Size(16, 21)
        Me.lblFactor.TabIndex = 6
        Me.lblFactor.Text = "-"
        '
        'txtCantidad
        '
        Me.txtCantidad.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCantidad.DecimalPlaces = 30
        Me.txtCantidad.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Location = New System.Drawing.Point(98, 62)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCantidad.Maximum = New Decimal(New Integer() {1215752191, 23, 0, 0})
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(359, 32)
        Me.txtCantidad.TabIndex = 4
        Me.txtCantidad.Value = New Decimal(New Integer() {-106255702, 31102513, 3614, 1507328})
        '
        'txtIdUbicacionDestino
        '
        Me.txtIdUbicacionDestino.Location = New System.Drawing.Point(98, 30)
        Me.txtIdUbicacionDestino.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtIdUbicacionDestino.MenuManager = Me.RibbonControl
        Me.txtIdUbicacionDestino.Name = "txtIdUbicacionDestino"
        Me.txtIdUbicacionDestino.Properties.Mask.EditMask = "n0"
        Me.txtIdUbicacionDestino.Size = New System.Drawing.Size(77, 22)
        Me.txtIdUbicacionDestino.TabIndex = 1
        '
        'chkActivoDet
        '
        Me.chkActivoDet.EditValue = True
        Me.chkActivoDet.Location = New System.Drawing.Point(182, 139)
        Me.chkActivoDet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActivoDet.MenuManager = Me.RibbonControl
        Me.chkActivoDet.Name = "chkActivoDet"
        Me.chkActivoDet.Properties.Caption = ""
        Me.chkActivoDet.Size = New System.Drawing.Size(41, 24)
        Me.chkActivoDet.TabIndex = 10
        '
        'txtUbicacionDestino
        '
        Me.txtUbicacionDestino.Location = New System.Drawing.Point(182, 30)
        Me.txtUbicacionDestino.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtUbicacionDestino.MenuManager = Me.RibbonControl
        Me.txtUbicacionDestino.Name = "txtUbicacionDestino"
        Me.txtUbicacionDestino.Properties.Mask.EditMask = "n0"
        Me.txtUbicacionDestino.Properties.ReadOnly = True
        Me.txtUbicacionDestino.Size = New System.Drawing.Size(190, 22)
        Me.txtUbicacionDestino.TabIndex = 2
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(9, 33)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(77, 16)
        Me.LinkLabel1.TabIndex = 0
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Ubic Destino"
        '
        'chkRealizadoDet
        '
        Me.chkRealizadoDet.EditValue = True
        Me.chkRealizadoDet.Location = New System.Drawing.Point(98, 138)
        Me.chkRealizadoDet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkRealizadoDet.MenuManager = Me.RibbonControl
        Me.chkRealizadoDet.Name = "chkRealizadoDet"
        Me.chkRealizadoDet.Properties.Caption = ""
        Me.chkRealizadoDet.Properties.ReadOnly = True
        Me.chkRealizadoDet.Size = New System.Drawing.Size(41, 24)
        Me.chkRealizadoDet.TabIndex = 8
        '
        'frmCantidadUbicacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1120, 556)
        Me.Controls.Add(Me.GroupControl5)
        Me.Controls.Add(Me.groupCambioDeEstado)
        Me.Controls.Add(Me.GroupControl4)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCantidadUbicacion"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "frmCantidadUbicacion"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.groupCambioDeEstado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupCambioDeEstado.ResumeLayout(False)
        Me.groupCambioDeEstado.PerformLayout()
        CType(Me.txtIdEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombreEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        Me.GroupControl4.PerformLayout()
        CType(Me.cmbOperador.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPresentacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAñada.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUnidadMedida.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProducto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdStock.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdOrigen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        Me.GroupControl5.PerformLayout()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIdUbicacionDestino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivoDet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUbicacionDestino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRealizadoDet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuConfirmarCantidad As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents groupCambioDeEstado As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtIdEstado As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lnkCambioDeEstado As LinkLabel
    Friend WithEvents txtNombreEstado As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblCantRef As Label
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIngreso As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPresentacion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAñada As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUnidadMedida As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtEstado As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtVence As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSerie As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtProducto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdStock As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtIdOrigen As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblFactor As Label
    Friend WithEvents txtCantidad As NumericUpDown
    Friend WithEvents txtIdUbicacionDestino As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkActivoDet As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtUbicacionDestino As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents chkRealizadoDet As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblVolumen As Label
    Friend WithEvents cmbOperador As DevExpress.XtraEditors.LookUpEdit
End Class
