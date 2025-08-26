<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTipoDocumentoIngreso
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim NombreLabel As System.Windows.Forms.Label
        Dim User_agrLabel As System.Windows.Forms.Label
        Dim Fec_agrLabel As System.Windows.Forms.Label
        Dim User_modLabel As System.Windows.Forms.Label
        Dim Fec_modLabel As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTipoDocumentoIngreso))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnuGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuEliminar = New DevExpress.XtraBars.BarButtonItem()
        Me.mnuAsignacion = New DevExpress.XtraBars.BarButtonItem()
        Me.chkActivo = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario = New DevExpress.XtraEditors.GroupControl()
        Me.lblPropietario = New System.Windows.Forms.Label()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbEstado = New DevExpress.XtraEditors.LookUpEdit()
        Me.chkPermiteVencidoIngreso = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkPreguntarEnBackOrder = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkMarcarRegistrosEnviadosMI3 = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkExigirCampoReferencia = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkRequerirUbicRecIngreso = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkRequerirDocumentoRefWMS = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkRequerirProveedorEsBodegaWMS = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkGeneraTareaIngreso = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkPolizaEsConsolidada = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkRequerirDocumentoRef = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkControlPoliza = New DevExpress.XtraEditors.ToggleSwitch()
        Me.chkEsDevolucion = New DevExpress.XtraEditors.ToggleSwitch()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_agrDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_agrTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.Fec_modDateEdit = New DevExpress.XtraEditors.DateEdit()
        Me.User_modTextEdit = New DevExpress.XtraEditors.TextEdit()
        Me.dkProducto_Tipo = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        NombreLabel = New System.Windows.Forms.Label()
        User_agrLabel = New System.Windows.Forms.Label()
        Fec_agrLabel = New System.Windows.Forms.Label()
        User_modLabel = New System.Windows.Forms.Label()
        Fec_modLabel = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.SuspendLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEstado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPermiteVencidoIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPreguntarEnBackOrder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMarcarRegistrosEnviadosMI3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExigirCampoReferencia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequerirUbicRecIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequerirDocumentoRefWMS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequerirProveedorEsBodegaWMS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGeneraTareaIngreso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPolizaEsConsolidada.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRequerirDocumentoRef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlPoliza.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEsDevolucion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dkProducto_Tipo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.SuspendLayout()
        '
        'NombreLabel
        '
        NombreLabel.AutoSize = True
        NombreLabel.Location = New System.Drawing.Point(26, 77)
        NombreLabel.Name = "NombreLabel"
        NombreLabel.Size = New System.Drawing.Size(57, 16)
        NombreLabel.TabIndex = 4
        NombreLabel.Text = "Nombre:"
        '
        'User_agrLabel
        '
        User_agrLabel.AutoSize = True
        User_agrLabel.Location = New System.Drawing.Point(40, 20)
        User_agrLabel.Name = "User_agrLabel"
        User_agrLabel.Size = New System.Drawing.Size(100, 16)
        User_agrLabel.TabIndex = 0
        User_agrLabel.Text = "Usuario Agregó:"
        '
        'Fec_agrLabel
        '
        Fec_agrLabel.AutoSize = True
        Fec_agrLabel.Location = New System.Drawing.Point(40, 52)
        Fec_agrLabel.Name = "Fec_agrLabel"
        Fec_agrLabel.Size = New System.Drawing.Size(91, 16)
        Fec_agrLabel.TabIndex = 4
        Fec_agrLabel.Text = "Fecha Agregó:"
        '
        'User_modLabel
        '
        User_modLabel.AutoSize = True
        User_modLabel.Location = New System.Drawing.Point(524, 20)
        User_modLabel.Name = "User_modLabel"
        User_modLabel.Size = New System.Drawing.Size(106, 16)
        User_modLabel.TabIndex = 3
        User_modLabel.Text = "Usuario Modificó:"
        '
        'Fec_modLabel
        '
        Fec_modLabel.AutoSize = True
        Fec_modLabel.Location = New System.Drawing.Point(524, 52)
        Fec_modLabel.Name = "Fec_modLabel"
        Fec_modLabel.Size = New System.Drawing.Size(97, 16)
        Fec_modLabel.TabIndex = 6
        Fec_modLabel.Text = "Fecha Modificó:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(26, 44)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(46, 16)
        Label12.TabIndex = 0
        Label12.Text = "Código"
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.mnuGuardar, Me.mnuActualizar, Me.mnuEliminar, Me.mnuAsignacion, Me.chkActivo})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonControl.MaxItemId = 6
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.RibbonControl.Size = New System.Drawing.Size(1291, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Caption = "Guardar"
        Me.mnuGuardar.Id = 1
        Me.mnuGuardar.ImageOptions.SvgImage = CType(resources.GetObject("mnuGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuGuardar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G))
        Me.mnuGuardar.Name = "mnuGuardar"
        Me.mnuGuardar.ShortcutKeyDisplayString = "G"
        '
        'mnuActualizar
        '
        Me.mnuActualizar.Caption = "Actualizar"
        Me.mnuActualizar.Id = 2
        Me.mnuActualizar.ImageOptions.SvgImage = CType(resources.GetObject("mnuActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuActualizar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.mnuActualizar.Name = "mnuActualizar"
        '
        'mnuEliminar
        '
        Me.mnuEliminar.Caption = "Eliminar"
        Me.mnuEliminar.Id = 3
        Me.mnuEliminar.ImageOptions.SvgImage = CType(resources.GetObject("mnuEliminar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.mnuEliminar.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.mnuEliminar.Name = "mnuEliminar"
        Me.mnuEliminar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'mnuAsignacion
        '
        Me.mnuAsignacion.Caption = "Asignacion"
        Me.mnuAsignacion.Id = 4
        Me.mnuAsignacion.ImageOptions.Image = CType(resources.GetObject("mnuAsignacion.ImageOptions.Image"), System.Drawing.Image)
        Me.mnuAsignacion.ImageOptions.LargeImage = CType(resources.GetObject("mnuAsignacion.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.mnuAsignacion.Name = "mnuAsignacion"
        '
        'chkActivo
        '
        Me.chkActivo.Caption = "Activo"
        Me.chkActivo.Id = 5
        Me.chkActivo.Name = "chkActivo"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Tipo de documento ingreso"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.mnuEliminar)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.chkActivo)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 770)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1291, 30)
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario)
        Me.GroupControl1.Controls.Add(Me.chkPermiteVencidoIngreso)
        Me.GroupControl1.Controls.Add(Me.chkPreguntarEnBackOrder)
        Me.GroupControl1.Controls.Add(Me.chkMarcarRegistrosEnviadosMI3)
        Me.GroupControl1.Controls.Add(Me.chkExigirCampoReferencia)
        Me.GroupControl1.Controls.Add(Me.chkRequerirUbicRecIngreso)
        Me.GroupControl1.Controls.Add(Me.chkRequerirDocumentoRefWMS)
        Me.GroupControl1.Controls.Add(Me.chkRequerirProveedorEsBodegaWMS)
        Me.GroupControl1.Controls.Add(Me.chkGeneraTareaIngreso)
        Me.GroupControl1.Controls.Add(Me.chkPolizaEsConsolidada)
        Me.GroupControl1.Controls.Add(Me.chkRequerirDocumentoRef)
        Me.GroupControl1.Controls.Add(Me.chkControlPoliza)
        Me.GroupControl1.Controls.Add(Me.chkEsDevolucion)
        Me.GroupControl1.Controls.Add(Me.lblCodigo)
        Me.GroupControl1.Controls.Add(Label12)
        Me.GroupControl1.Controls.Add(NombreLabel)
        Me.GroupControl1.Controls.Add(Me.txtNombre)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 193)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1291, 551)
        Me.GroupControl1.TabIndex = 0
        '
        'GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario
        '
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Controls.Add(Me.lblPropietario)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Controls.Add(Me.lblEstado)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Controls.Add(Me.cmbPropietario)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Controls.Add(Me.cmbEstado)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Location = New System.Drawing.Point(935, 28)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Name = "GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario"
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Size = New System.Drawing.Size(354, 521)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.TabIndex = 31
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.Text = "Estado por defecto para una recepcion"
        '
        'lblPropietario
        '
        Me.lblPropietario.AutoSize = True
        Me.lblPropietario.Location = New System.Drawing.Point(18, 66)
        Me.lblPropietario.Name = "lblPropietario"
        Me.lblPropietario.Size = New System.Drawing.Size(74, 16)
        Me.lblPropietario.TabIndex = 29
        Me.lblPropietario.Text = "Propietario:"
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Location = New System.Drawing.Point(18, 103)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(50, 16)
        Me.lblEstado.TabIndex = 30
        Me.lblEstado.Text = "Estado:"
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbPropietario.Location = New System.Drawing.Point(110, 54)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.cmbPropietario.Properties.Appearance.Options.UseFont = True
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(226, 28)
        Me.cmbPropietario.TabIndex = 27
        '
        'cmbEstado
        '
        Me.cmbEstado.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbEstado.Location = New System.Drawing.Point(110, 91)
        Me.cmbEstado.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmbEstado.MenuManager = Me.RibbonControl
        Me.cmbEstado.Name = "cmbEstado"
        Me.cmbEstado.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.cmbEstado.Properties.Appearance.Options.UseFont = True
        Me.cmbEstado.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbEstado.Properties.NullText = ""
        Me.cmbEstado.Size = New System.Drawing.Size(226, 28)
        Me.cmbEstado.TabIndex = 28
        '
        'chkPermiteVencidoIngreso
        '
        Me.chkPermiteVencidoIngreso.EditValue = True
        Me.chkPermiteVencidoIngreso.Location = New System.Drawing.Point(618, 148)
        Me.chkPermiteVencidoIngreso.MenuManager = Me.RibbonControl
        Me.chkPermiteVencidoIngreso.Name = "chkPermiteVencidoIngreso"
        Me.chkPermiteVencidoIngreso.Properties.OffText = "Permitir Vencidos en Ingreso"
        Me.chkPermiteVencidoIngreso.Properties.OnText = "Permitir Vencidos en Ingreso"
        Me.chkPermiteVencidoIngreso.Size = New System.Drawing.Size(293, 24)
        Me.chkPermiteVencidoIngreso.TabIndex = 26
        Me.chkPermiteVencidoIngreso.ToolTip = "#EJC20220504: Si true, permite recibir producto vencido"
        '
        'chkPreguntarEnBackOrder
        '
        Me.chkPreguntarEnBackOrder.Location = New System.Drawing.Point(159, 263)
        Me.chkPreguntarEnBackOrder.MenuManager = Me.RibbonControl
        Me.chkPreguntarEnBackOrder.Name = "chkPreguntarEnBackOrder"
        Me.chkPreguntarEnBackOrder.Properties.OffText = "Preguntar en BackOrder"
        Me.chkPreguntarEnBackOrder.Properties.OnText = "Preguntar en BackOrder"
        Me.chkPreguntarEnBackOrder.Size = New System.Drawing.Size(439, 24)
        Me.chkPreguntarEnBackOrder.TabIndex = 25
        Me.chkPreguntarEnBackOrder.ToolTip = "#EJC20220719: Si true, en la HH pregunta si dejar o no en Backorder el documento " &
    "de ingreso"
        '
        'chkMarcarRegistrosEnviadosMI3
        '
        Me.chkMarcarRegistrosEnviadosMI3.Location = New System.Drawing.Point(159, 382)
        Me.chkMarcarRegistrosEnviadosMI3.MenuManager = Me.RibbonControl
        Me.chkMarcarRegistrosEnviadosMI3.Name = "chkMarcarRegistrosEnviadosMI3"
        Me.chkMarcarRegistrosEnviadosMI3.Properties.OffText = "Marcar registros como enviados en MI3 al finalizar ingresos"
        Me.chkMarcarRegistrosEnviadosMI3.Properties.OnText = "Marcar registros como enviados en MI3 al finalizar ingresos"
        Me.chkMarcarRegistrosEnviadosMI3.Size = New System.Drawing.Size(439, 24)
        Me.chkMarcarRegistrosEnviadosMI3.TabIndex = 24
        Me.chkMarcarRegistrosEnviadosMI3.ToolTip = "#EJC20220504: Si true, en tabla I_NAV_TRANSACCIONES_OUT los registros de interfac" &
    "e se insertan como enviados"
        '
        'chkExigirCampoReferencia
        '
        Me.chkExigirCampoReferencia.Location = New System.Drawing.Point(159, 352)
        Me.chkExigirCampoReferencia.MenuManager = Me.RibbonControl
        Me.chkExigirCampoReferencia.Name = "chkExigirCampoReferencia"
        Me.chkExigirCampoReferencia.Properties.OffText = "Exigir campo referencia"
        Me.chkExigirCampoReferencia.Properties.OnText = "Exigir campo referencia"
        Me.chkExigirCampoReferencia.Size = New System.Drawing.Size(439, 24)
        Me.chkExigirCampoReferencia.TabIndex = 23
        Me.chkExigirCampoReferencia.ToolTip = "#EJC20220504: Si true, en el documento de ingreso, se exige se ingrese valor en e" &
    "l campo referencia"
        '
        'chkRequerirUbicRecIngreso
        '
        Me.chkRequerirUbicRecIngreso.Location = New System.Drawing.Point(159, 322)
        Me.chkRequerirUbicRecIngreso.MenuManager = Me.RibbonControl
        Me.chkRequerirUbicRecIngreso.Name = "chkRequerirUbicRecIngreso"
        Me.chkRequerirUbicRecIngreso.Properties.OffText = "Requerir escanear ubicación de recepción en tareas de ingreso."
        Me.chkRequerirUbicRecIngreso.Properties.OnText = "Requerir ubicación de recepción en tareas de ingreso."
        Me.chkRequerirUbicRecIngreso.Size = New System.Drawing.Size(439, 24)
        Me.chkRequerirUbicRecIngreso.TabIndex = 22
        Me.chkRequerirUbicRecIngreso.ToolTip = "#EJC20220330_AT: Si true, se traslada el parámetro a la tarea de recepción en el " &
    "BOF - trans_oc_ti / requerir_ubic_rec_ingreso bit default 0"
        '
        'chkRequerirDocumentoRefWMS
        '
        Me.chkRequerirDocumentoRefWMS.Location = New System.Drawing.Point(159, 292)
        Me.chkRequerirDocumentoRefWMS.MenuManager = Me.RibbonControl
        Me.chkRequerirDocumentoRefWMS.Name = "chkRequerirDocumentoRefWMS"
        Me.chkRequerirDocumentoRefWMS.Properties.OffText = "Requerir documento ref WMS (Devolución)"
        Me.chkRequerirDocumentoRefWMS.Properties.OnText = "Requerir documento ref WMS (Devolución)"
        Me.chkRequerirDocumentoRefWMS.Size = New System.Drawing.Size(353, 24)
        Me.chkRequerirDocumentoRefWMS.TabIndex = 21
        '
        'chkRequerirProveedorEsBodegaWMS
        '
        Me.chkRequerirProveedorEsBodegaWMS.Location = New System.Drawing.Point(159, 233)
        Me.chkRequerirProveedorEsBodegaWMS.MenuManager = Me.RibbonControl
        Me.chkRequerirProveedorEsBodegaWMS.Name = "chkRequerirProveedorEsBodegaWMS"
        Me.chkRequerirProveedorEsBodegaWMS.Properties.OffText = "Requerir proveedor como bodega de WMS"
        Me.chkRequerirProveedorEsBodegaWMS.Properties.OnText = "Requerir proveedor como bodega de WMS"
        Me.chkRequerirProveedorEsBodegaWMS.Size = New System.Drawing.Size(353, 24)
        Me.chkRequerirProveedorEsBodegaWMS.TabIndex = 20
        '
        'chkGeneraTareaIngreso
        '
        Me.chkGeneraTareaIngreso.Location = New System.Drawing.Point(159, 203)
        Me.chkGeneraTareaIngreso.MenuManager = Me.RibbonControl
        Me.chkGeneraTareaIngreso.Name = "chkGeneraTareaIngreso"
        Me.chkGeneraTareaIngreso.Properties.OffText = "No genera tarea de ingreso en bodega destino"
        Me.chkGeneraTareaIngreso.Properties.OnText = "Genera tarea de ingreso en bodega destino"
        Me.chkGeneraTareaIngreso.Size = New System.Drawing.Size(353, 24)
        Me.chkGeneraTareaIngreso.TabIndex = 19
        '
        'chkPolizaEsConsolidada
        '
        Me.chkPolizaEsConsolidada.Location = New System.Drawing.Point(618, 118)
        Me.chkPolizaEsConsolidada.MenuManager = Me.RibbonControl
        Me.chkPolizaEsConsolidada.Name = "chkPolizaEsConsolidada"
        Me.chkPolizaEsConsolidada.Properties.OffText = "Ingreso consolidado (Multipropietario)"
        Me.chkPolizaEsConsolidada.Properties.OnText = "Ingreso no consolidado (Propietario Único)"
        Me.chkPolizaEsConsolidada.Size = New System.Drawing.Size(293, 24)
        Me.chkPolizaEsConsolidada.TabIndex = 18
        '
        'chkRequerirDocumentoRef
        '
        Me.chkRequerirDocumentoRef.Location = New System.Drawing.Point(159, 173)
        Me.chkRequerirDocumentoRef.MenuManager = Me.RibbonControl
        Me.chkRequerirDocumentoRef.Name = "chkRequerirDocumentoRef"
        Me.chkRequerirDocumentoRef.Properties.OffText = "No requiere documento de referencia"
        Me.chkRequerirDocumentoRef.Properties.OnText = "Requiere documento de refencia"
        Me.chkRequerirDocumentoRef.Size = New System.Drawing.Size(293, 24)
        Me.chkRequerirDocumentoRef.TabIndex = 17
        '
        'chkControlPoliza
        '
        Me.chkControlPoliza.Location = New System.Drawing.Point(159, 143)
        Me.chkControlPoliza.MenuManager = Me.RibbonControl
        Me.chkControlPoliza.Name = "chkControlPoliza"
        Me.chkControlPoliza.Properties.OffText = "Sin control de poliza"
        Me.chkControlPoliza.Properties.OnText = "Control de poliza"
        Me.chkControlPoliza.Size = New System.Drawing.Size(190, 24)
        Me.chkControlPoliza.TabIndex = 16
        '
        'chkEsDevolucion
        '
        Me.chkEsDevolucion.Location = New System.Drawing.Point(159, 113)
        Me.chkEsDevolucion.MenuManager = Me.RibbonControl
        Me.chkEsDevolucion.Name = "chkEsDevolucion"
        Me.chkEsDevolucion.Properties.OffText = "No es Devolución"
        Me.chkEsDevolucion.Properties.OnText = "Es Devolución"
        Me.chkEsDevolucion.Size = New System.Drawing.Size(169, 24)
        Me.chkEsDevolucion.TabIndex = 14
        '
        'lblCodigo
        '
        Me.lblCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(159, 42)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(229, 25)
        Me.lblCodigo.TabIndex = 1
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(159, 74)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombre.MenuManager = Me.RibbonControl
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(229, 22)
        Me.txtNombre.TabIndex = 5
        '
        'Fec_agrDateEdit
        '
        Me.Fec_agrDateEdit.EditValue = Nothing
        Me.Fec_agrDateEdit.Enabled = False
        Me.Fec_agrDateEdit.Location = New System.Drawing.Point(142, 48)
        Me.Fec_agrDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_agrDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_agrDateEdit.Name = "Fec_agrDateEdit"
        Me.Fec_agrDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_agrDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_agrDateEdit.TabIndex = 5
        '
        'User_agrTextEdit
        '
        Me.User_agrTextEdit.Enabled = False
        Me.User_agrTextEdit.Location = New System.Drawing.Point(142, 16)
        Me.User_agrTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_agrTextEdit.MenuManager = Me.RibbonControl
        Me.User_agrTextEdit.Name = "User_agrTextEdit"
        Me.User_agrTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_agrTextEdit.TabIndex = 1
        '
        'Fec_modDateEdit
        '
        Me.Fec_modDateEdit.EditValue = Nothing
        Me.Fec_modDateEdit.Enabled = False
        Me.Fec_modDateEdit.Location = New System.Drawing.Point(626, 48)
        Me.Fec_modDateEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Fec_modDateEdit.MenuManager = Me.RibbonControl
        Me.Fec_modDateEdit.Name = "Fec_modDateEdit"
        Me.Fec_modDateEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fec_modDateEdit.Size = New System.Drawing.Size(261, 22)
        Me.Fec_modDateEdit.TabIndex = 7
        '
        'User_modTextEdit
        '
        Me.User_modTextEdit.Enabled = False
        Me.User_modTextEdit.Location = New System.Drawing.Point(626, 16)
        Me.User_modTextEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.User_modTextEdit.MenuManager = Me.RibbonControl
        Me.User_modTextEdit.Name = "User_modTextEdit"
        Me.User_modTextEdit.Size = New System.Drawing.Size(261, 22)
        Me.User_modTextEdit.TabIndex = 2
        '
        'dkProducto_Tipo
        '
        Me.dkProducto_Tipo.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.dkProducto_Tipo.Form = Me
        Me.dkProducto_Tipo.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 744)
        Me.hideContainerBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(1291, 26)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("b43ffebe-a0fb-42a7-b56f-f32caab34d5e")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 101)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(953, 124)
        Me.DockPanel1.Text = "Bitácora"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.Fec_agrDateEdit)
        Me.DockPanel1_Container.Controls.Add(Me.User_agrTextEdit)
        Me.DockPanel1_Container.Controls.Add(Fec_agrLabel)
        Me.DockPanel1_Container.Controls.Add(User_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.User_modTextEdit)
        Me.DockPanel1_Container.Controls.Add(User_agrLabel)
        Me.DockPanel1_Container.Controls.Add(Fec_modLabel)
        Me.DockPanel1_Container.Controls.Add(Me.Fec_modDateEdit)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 31)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(946, 90)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'frmTipoDocumentoIngreso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1291, 800)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmTipoDocumentoIngreso"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Tipo documento de ingreso"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.ResumeLayout(False)
        Me.GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario.PerformLayout()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEstado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPermiteVencidoIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPreguntarEnBackOrder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMarcarRegistrosEnviadosMI3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExigirCampoReferencia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequerirUbicRecIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequerirDocumentoRefWMS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequerirProveedorEsBodegaWMS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGeneraTareaIngreso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPolizaEsConsolidada.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRequerirDocumentoRef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlPoliza.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEsDevolucion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_agrDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_agrTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fec_modDateEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.User_modTextEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dkProducto_Tipo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents mnuGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuActualizar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuEliminar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuAsignacion As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_agrDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_agrTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Fec_modDateEdit As DevExpress.XtraEditors.DateEdit
    Friend WithEvents User_modTextEdit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents dkProducto_Tipo As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents chkGeneraTareaIngreso As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkPolizaEsConsolidada As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkRequerirDocumentoRef As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkControlPoliza As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkEsDevolucion As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkActivo As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents chkRequerirProveedorEsBodegaWMS As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkRequerirDocumentoRefWMS As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkRequerirUbicRecIngreso As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkExigirCampoReferencia As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkMarcarRegistrosEnviadosMI3 As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkPreguntarEnBackOrder As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents chkPermiteVencidoIngreso As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents cmbEstado As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblEstado As Label
    Friend WithEvents lblPropietario As Label
    Friend WithEvents GroupControlParametrosDeSoftwareNoPersonalizadosPorUsuario As DevExpress.XtraEditors.GroupControl
End Class
