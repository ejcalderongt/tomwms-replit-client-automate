<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInventarioRFID
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventarioRFID))
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.cmdGuardar = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.cmdActualizar = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.chkCapturarNoAsignado = New DevExpress.XtraEditors.CheckEdit()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkCaptNtExist = New DevExpress.XtraEditors.CheckEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblMostrarCantidad = New System.Windows.Forms.Label()
        Me.chkMostrarCantidad = New DevExpress.XtraEditors.CheckEdit()
        Me.chkCambiaUbicacion = New DevExpress.XtraEditors.CheckEdit()
        Me.lblCambiaUbicacion = New System.Windows.Forms.Label()
        Me.lblUltInv = New System.Windows.Forms.Label()
        Me.dtpUltimoInv = New DevExpress.XtraEditors.DateEdit()
        Me.dtpHoraFin = New DevExpress.XtraEditors.DateEdit()
        Me.dtpHoraInicio = New DevExpress.XtraEditors.DateEdit()
        Me.chkActivo = New DevExpress.XtraEditors.CheckEdit()
        Me.cmbTipoConteo = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbTipoInventario = New DevExpress.XtraEditors.LookUpEdit()
        Me.lblFin = New System.Windows.Forms.Label()
        Me.lblInicio = New System.Windows.Forms.Label()
        Me.lblActivo = New System.Windows.Forms.Label()
        Me.chkDobleVerifica = New DevExpress.XtraEditors.CheckEdit()
        Me.lblDobleVerif = New System.Windows.Forms.Label()
        Me.lblTipoConteo = New System.Windows.Forms.Label()
        Me.lblTipoInventario = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.Fecha = New DevExpress.XtraEditors.DateEdit()
        Me.cmbPropietario = New DevExpress.XtraEditors.LookUpEdit()
        Me.cmbBodega = New DevExpress.XtraEditors.LookUpEdit()
        Me.Estado = New System.Windows.Forms.Label()
        Me.lblCod = New System.Windows.Forms.Label()
        Me.lblPropietario = New System.Windows.Forms.Label()
        Me.lblBodega = New System.Windows.Forms.Label()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.chkCapturarNoAsignado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCaptNtExist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMostrarCantidad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCambiaUbicacion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpUltimoInv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpUltimoInv.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpHoraFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpHoraFin.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpHoraInicio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpHoraInicio.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoConteo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTipoInventario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDobleVerifica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fecha.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fecha.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.cmdGuardar, Me.BarButtonItem1, Me.cmdActualizar})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 4
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(1392, 193)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'cmdGuardar
        '
        Me.cmdGuardar.Caption = "Guardar"
        Me.cmdGuardar.Id = 1
        Me.cmdGuardar.ImageOptions.SvgImage = CType(resources.GetObject("cmdGuardar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdGuardar.Name = "cmdGuardar"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Eliminar"
        Me.BarButtonItem1.Id = 2
        Me.BarButtonItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'cmdActualizar
        '
        Me.cmdActualizar.Caption = "Actualizar"
        Me.cmdActualizar.Id = 3
        Me.cmdActualizar.ImageOptions.SvgImage = CType(resources.GetObject("cmdActualizar.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.cmdActualizar.Name = "cmdActualizar"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Inventario RFID"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdGuardar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.cmdActualizar)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 575)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1392, 30)
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 193)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(1392, 382)
        Me.XtraTabControl1.TabIndex = 2
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.GroupControl1)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1390, 352)
        Me.XtraTabPage1.Text = "Encabezado"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.chkCapturarNoAsignado)
        Me.GroupControl1.Controls.Add(Me.Label4)
        Me.GroupControl1.Controls.Add(Me.chkCaptNtExist)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Controls.Add(Me.lblMostrarCantidad)
        Me.GroupControl1.Controls.Add(Me.chkMostrarCantidad)
        Me.GroupControl1.Controls.Add(Me.chkCambiaUbicacion)
        Me.GroupControl1.Controls.Add(Me.lblCambiaUbicacion)
        Me.GroupControl1.Controls.Add(Me.lblUltInv)
        Me.GroupControl1.Controls.Add(Me.dtpUltimoInv)
        Me.GroupControl1.Controls.Add(Me.dtpHoraFin)
        Me.GroupControl1.Controls.Add(Me.dtpHoraInicio)
        Me.GroupControl1.Controls.Add(Me.chkActivo)
        Me.GroupControl1.Controls.Add(Me.cmbTipoConteo)
        Me.GroupControl1.Controls.Add(Me.cmbTipoInventario)
        Me.GroupControl1.Controls.Add(Me.lblFin)
        Me.GroupControl1.Controls.Add(Me.lblInicio)
        Me.GroupControl1.Controls.Add(Me.lblActivo)
        Me.GroupControl1.Controls.Add(Me.chkDobleVerifica)
        Me.GroupControl1.Controls.Add(Me.lblDobleVerif)
        Me.GroupControl1.Controls.Add(Me.lblTipoConteo)
        Me.GroupControl1.Controls.Add(Me.lblTipoInventario)
        Me.GroupControl1.Controls.Add(Me.lblFecha)
        Me.GroupControl1.Controls.Add(Me.Fecha)
        Me.GroupControl1.Controls.Add(Me.cmbPropietario)
        Me.GroupControl1.Controls.Add(Me.cmbBodega)
        Me.GroupControl1.Controls.Add(Me.Estado)
        Me.GroupControl1.Controls.Add(Me.lblCod)
        Me.GroupControl1.Controls.Add(Me.lblPropietario)
        Me.GroupControl1.Controls.Add(Me.lblBodega)
        Me.GroupControl1.Controls.Add(Me.lblEstado)
        Me.GroupControl1.Controls.Add(Me.lblCodigo)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1390, 352)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "GroupControl1"
        '
        'chkCapturarNoAsignado
        '
        Me.chkCapturarNoAsignado.Location = New System.Drawing.Point(798, 132)
        Me.chkCapturarNoAsignado.Margin = New System.Windows.Forms.Padding(5)
        Me.chkCapturarNoAsignado.MenuManager = Me.RibbonControl
        Me.chkCapturarNoAsignado.Name = "chkCapturarNoAsignado"
        Me.chkCapturarNoAsignado.Properties.Caption = ""
        Me.chkCapturarNoAsignado.Size = New System.Drawing.Size(24, 24)
        Me.chkCapturarNoAsignado.TabIndex = 85
        Me.chkCapturarNoAsignado.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(598, 132)
        Me.Label4.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(135, 16)
        Me.Label4.TabIndex = 84
        Me.Label4.Text = "Capturar no asignado:"
        Me.Label4.Visible = False
        '
        'chkCaptNtExist
        '
        Me.chkCaptNtExist.Location = New System.Drawing.Point(798, 190)
        Me.chkCaptNtExist.Margin = New System.Windows.Forms.Padding(5)
        Me.chkCaptNtExist.MenuManager = Me.RibbonControl
        Me.chkCaptNtExist.Name = "chkCaptNtExist"
        Me.chkCaptNtExist.Properties.Caption = ""
        Me.chkCaptNtExist.Size = New System.Drawing.Size(24, 24)
        Me.chkCaptNtExist.TabIndex = 79
        Me.chkCaptNtExist.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(598, 190)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 16)
        Me.Label2.TabIndex = 78
        Me.Label2.Text = "Capturar no existente:"
        Me.Label2.Visible = False
        '
        'lblMostrarCantidad
        '
        Me.lblMostrarCantidad.AutoSize = True
        Me.lblMostrarCantidad.Location = New System.Drawing.Point(598, 103)
        Me.lblMostrarCantidad.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblMostrarCantidad.Name = "lblMostrarCantidad"
        Me.lblMostrarCantidad.Size = New System.Drawing.Size(108, 16)
        Me.lblMostrarCantidad.TabIndex = 76
        Me.lblMostrarCantidad.Text = "Mostrar cantidad:"
        Me.lblMostrarCantidad.Visible = False
        '
        'chkMostrarCantidad
        '
        Me.chkMostrarCantidad.Location = New System.Drawing.Point(798, 103)
        Me.chkMostrarCantidad.Margin = New System.Windows.Forms.Padding(5)
        Me.chkMostrarCantidad.MenuManager = Me.RibbonControl
        Me.chkMostrarCantidad.Name = "chkMostrarCantidad"
        Me.chkMostrarCantidad.Properties.Caption = ""
        Me.chkMostrarCantidad.Size = New System.Drawing.Size(24, 24)
        Me.chkMostrarCantidad.TabIndex = 77
        Me.chkMostrarCantidad.Visible = False
        '
        'chkCambiaUbicacion
        '
        Me.chkCambiaUbicacion.Location = New System.Drawing.Point(798, 160)
        Me.chkCambiaUbicacion.Margin = New System.Windows.Forms.Padding(5)
        Me.chkCambiaUbicacion.MenuManager = Me.RibbonControl
        Me.chkCambiaUbicacion.Name = "chkCambiaUbicacion"
        Me.chkCambiaUbicacion.Properties.Caption = ""
        Me.chkCambiaUbicacion.Size = New System.Drawing.Size(24, 24)
        Me.chkCambiaUbicacion.TabIndex = 73
        Me.chkCambiaUbicacion.Visible = False
        '
        'lblCambiaUbicacion
        '
        Me.lblCambiaUbicacion.AutoSize = True
        Me.lblCambiaUbicacion.Location = New System.Drawing.Point(598, 160)
        Me.lblCambiaUbicacion.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblCambiaUbicacion.Name = "lblCambiaUbicacion"
        Me.lblCambiaUbicacion.Size = New System.Drawing.Size(112, 16)
        Me.lblCambiaUbicacion.TabIndex = 72
        Me.lblCambiaUbicacion.Text = "Cambia ubicación:"
        Me.lblCambiaUbicacion.Visible = False
        '
        'lblUltInv
        '
        Me.lblUltInv.AutoSize = True
        Me.lblUltInv.Location = New System.Drawing.Point(12, 280)
        Me.lblUltInv.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblUltInv.Name = "lblUltInv"
        Me.lblUltInv.Size = New System.Drawing.Size(51, 16)
        Me.lblUltInv.TabIndex = 69
        Me.lblUltInv.Text = "Ult. Inv."
        '
        'dtpUltimoInv
        '
        Me.dtpUltimoInv.EditValue = New Date(2017, 12, 4, 13, 30, 51, 367)
        Me.dtpUltimoInv.Location = New System.Drawing.Point(208, 280)
        Me.dtpUltimoInv.Margin = New System.Windows.Forms.Padding(5)
        Me.dtpUltimoInv.MenuManager = Me.RibbonControl
        Me.dtpUltimoInv.Name = "dtpUltimoInv"
        Me.dtpUltimoInv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpUltimoInv.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpUltimoInv.Size = New System.Drawing.Size(314, 22)
        Me.dtpUltimoInv.TabIndex = 68
        '
        'dtpHoraFin
        '
        Me.dtpHoraFin.EditValue = New Date(2017, 12, 4, 13, 30, 51, 367)
        Me.dtpHoraFin.Location = New System.Drawing.Point(208, 252)
        Me.dtpHoraFin.Margin = New System.Windows.Forms.Padding(5)
        Me.dtpHoraFin.MenuManager = Me.RibbonControl
        Me.dtpHoraFin.Name = "dtpHoraFin"
        Me.dtpHoraFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpHoraFin.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpHoraFin.Properties.DisplayFormat.FormatString = "t"
        Me.dtpHoraFin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpHoraFin.Properties.EditFormat.FormatString = "t"
        Me.dtpHoraFin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpHoraFin.Properties.MaskSettings.Set("mask", "t")
        Me.dtpHoraFin.Size = New System.Drawing.Size(314, 22)
        Me.dtpHoraFin.TabIndex = 67
        '
        'dtpHoraInicio
        '
        Me.dtpHoraInicio.EditValue = New Date(2017, 12, 4, 13, 30, 51, 367)
        Me.dtpHoraInicio.Location = New System.Drawing.Point(208, 223)
        Me.dtpHoraInicio.Margin = New System.Windows.Forms.Padding(5)
        Me.dtpHoraInicio.MenuManager = Me.RibbonControl
        Me.dtpHoraInicio.Name = "dtpHoraInicio"
        Me.dtpHoraInicio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpHoraInicio.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpHoraInicio.Properties.DisplayFormat.FormatString = "t"
        Me.dtpHoraInicio.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpHoraInicio.Properties.EditFormat.FormatString = "t"
        Me.dtpHoraInicio.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpHoraInicio.Properties.MaskSettings.Set("mask", "t")
        Me.dtpHoraInicio.Size = New System.Drawing.Size(314, 22)
        Me.dtpHoraInicio.TabIndex = 63
        '
        'chkActivo
        '
        Me.chkActivo.EditValue = True
        Me.chkActivo.Enabled = False
        Me.chkActivo.Location = New System.Drawing.Point(208, 310)
        Me.chkActivo.Margin = New System.Windows.Forms.Padding(5)
        Me.chkActivo.MenuManager = Me.RibbonControl
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Properties.Caption = ""
        Me.chkActivo.Size = New System.Drawing.Size(24, 24)
        Me.chkActivo.TabIndex = 71
        '
        'cmbTipoConteo
        '
        Me.cmbTipoConteo.Location = New System.Drawing.Point(798, 39)
        Me.cmbTipoConteo.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbTipoConteo.MenuManager = Me.RibbonControl
        Me.cmbTipoConteo.Name = "cmbTipoConteo"
        Me.cmbTipoConteo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoConteo.Properties.NullText = ""
        Me.cmbTipoConteo.Size = New System.Drawing.Size(314, 22)
        Me.cmbTipoConteo.TabIndex = 65
        Me.cmbTipoConteo.Visible = False
        '
        'cmbTipoInventario
        '
        Me.cmbTipoInventario.Location = New System.Drawing.Point(208, 161)
        Me.cmbTipoInventario.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbTipoInventario.MenuManager = Me.RibbonControl
        Me.cmbTipoInventario.Name = "cmbTipoInventario"
        Me.cmbTipoInventario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTipoInventario.Properties.NullText = ""
        Me.cmbTipoInventario.Size = New System.Drawing.Size(314, 22)
        Me.cmbTipoInventario.TabIndex = 53
        '
        'lblFin
        '
        Me.lblFin.AutoSize = True
        Me.lblFin.Location = New System.Drawing.Point(12, 252)
        Me.lblFin.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblFin.Name = "lblFin"
        Me.lblFin.Size = New System.Drawing.Size(60, 16)
        Me.lblFin.TabIndex = 66
        Me.lblFin.Text = "Hora Fin:"
        '
        'lblInicio
        '
        Me.lblInicio.AutoSize = True
        Me.lblInicio.Location = New System.Drawing.Point(12, 223)
        Me.lblInicio.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblInicio.Name = "lblInicio"
        Me.lblInicio.Size = New System.Drawing.Size(73, 16)
        Me.lblInicio.TabIndex = 62
        Me.lblInicio.Text = "Hora Inicio:"
        '
        'lblActivo
        '
        Me.lblActivo.AutoSize = True
        Me.lblActivo.Location = New System.Drawing.Point(12, 310)
        Me.lblActivo.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblActivo.Name = "lblActivo"
        Me.lblActivo.Size = New System.Drawing.Size(46, 16)
        Me.lblActivo.TabIndex = 70
        Me.lblActivo.Text = "Activo:"
        '
        'chkDobleVerifica
        '
        Me.chkDobleVerifica.Location = New System.Drawing.Point(798, 71)
        Me.chkDobleVerifica.Margin = New System.Windows.Forms.Padding(5)
        Me.chkDobleVerifica.MenuManager = Me.RibbonControl
        Me.chkDobleVerifica.Name = "chkDobleVerifica"
        Me.chkDobleVerifica.Properties.Caption = ""
        Me.chkDobleVerifica.Size = New System.Drawing.Size(24, 24)
        Me.chkDobleVerifica.TabIndex = 54
        Me.chkDobleVerifica.Visible = False
        '
        'lblDobleVerif
        '
        Me.lblDobleVerif.AutoSize = True
        Me.lblDobleVerif.Location = New System.Drawing.Point(598, 71)
        Me.lblDobleVerif.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblDobleVerif.Name = "lblDobleVerif"
        Me.lblDobleVerif.Size = New System.Drawing.Size(114, 16)
        Me.lblDobleVerif.TabIndex = 55
        Me.lblDobleVerif.Text = "Doble Verificación:"
        Me.lblDobleVerif.Visible = False
        '
        'lblTipoConteo
        '
        Me.lblTipoConteo.AutoSize = True
        Me.lblTipoConteo.Location = New System.Drawing.Point(598, 39)
        Me.lblTipoConteo.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblTipoConteo.Name = "lblTipoConteo"
        Me.lblTipoConteo.Size = New System.Drawing.Size(81, 16)
        Me.lblTipoConteo.TabIndex = 64
        Me.lblTipoConteo.Text = "Tipo Conteo:"
        Me.lblTipoConteo.Visible = False
        '
        'lblTipoInventario
        '
        Me.lblTipoInventario.AutoSize = True
        Me.lblTipoInventario.Location = New System.Drawing.Point(12, 161)
        Me.lblTipoInventario.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblTipoInventario.Name = "lblTipoInventario"
        Me.lblTipoInventario.Size = New System.Drawing.Size(98, 16)
        Me.lblTipoInventario.TabIndex = 52
        Me.lblTipoInventario.Text = "Tipo Inventario:"
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.Location = New System.Drawing.Point(12, 191)
        Me.lblFecha.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(46, 16)
        Me.lblFecha.TabIndex = 58
        Me.lblFecha.Text = "Fecha:"
        '
        'Fecha
        '
        Me.Fecha.EditValue = New Date(2017, 12, 4, 13, 30, 51, 367)
        Me.Fecha.Location = New System.Drawing.Point(208, 191)
        Me.Fecha.Margin = New System.Windows.Forms.Padding(5)
        Me.Fecha.MenuManager = Me.RibbonControl
        Me.Fecha.Name = "Fecha"
        Me.Fecha.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fecha.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Fecha.Size = New System.Drawing.Size(314, 22)
        Me.Fecha.TabIndex = 59
        '
        'cmbPropietario
        '
        Me.cmbPropietario.Location = New System.Drawing.Point(208, 129)
        Me.cmbPropietario.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbPropietario.MenuManager = Me.RibbonControl
        Me.cmbPropietario.Name = "cmbPropietario"
        Me.cmbPropietario.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPropietario.Properties.NullText = ""
        Me.cmbPropietario.Size = New System.Drawing.Size(314, 22)
        Me.cmbPropietario.TabIndex = 50
        '
        'cmbBodega
        '
        Me.cmbBodega.Location = New System.Drawing.Point(208, 97)
        Me.cmbBodega.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbBodega.MenuManager = Me.RibbonControl
        Me.cmbBodega.Name = "cmbBodega"
        Me.cmbBodega.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBodega.Properties.NullText = ""
        Me.cmbBodega.Properties.ReadOnly = True
        Me.cmbBodega.Size = New System.Drawing.Size(314, 22)
        Me.cmbBodega.TabIndex = 48
        '
        'Estado
        '
        Me.Estado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Estado.Location = New System.Drawing.Point(208, 67)
        Me.Estado.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Estado.Name = "Estado"
        Me.Estado.Size = New System.Drawing.Size(317, 22)
        Me.Estado.TabIndex = 47
        Me.Estado.Text = "Estado"
        '
        'lblCod
        '
        Me.lblCod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCod.Location = New System.Drawing.Point(208, 39)
        Me.lblCod.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblCod.Name = "lblCod"
        Me.lblCod.Size = New System.Drawing.Size(318, 24)
        Me.lblCod.TabIndex = 45
        Me.lblCod.Text = "000000001"
        '
        'lblPropietario
        '
        Me.lblPropietario.AutoSize = True
        Me.lblPropietario.Location = New System.Drawing.Point(12, 129)
        Me.lblPropietario.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblPropietario.Name = "lblPropietario"
        Me.lblPropietario.Size = New System.Drawing.Size(74, 16)
        Me.lblPropietario.TabIndex = 51
        Me.lblPropietario.Text = "Propietario:"
        '
        'lblBodega
        '
        Me.lblBodega.AutoSize = True
        Me.lblBodega.Location = New System.Drawing.Point(12, 103)
        Me.lblBodega.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.Size = New System.Drawing.Size(54, 16)
        Me.lblBodega.TabIndex = 49
        Me.lblBodega.Text = "Bodega:"
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Location = New System.Drawing.Point(12, 74)
        Me.lblEstado.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(50, 16)
        Me.lblEstado.TabIndex = 46
        Me.lblEstado.Text = "Estado:"
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Location = New System.Drawing.Point(12, 39)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(51, 16)
        Me.lblCodigo.TabIndex = 44
        Me.lblCodigo.Text = "Código:"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1390, 352)
        Me.XtraTabPage2.Text = "XtraTabPage2"
        '
        'frmInventarioRFID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1392, 605)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmInventarioRFID"
        Me.Ribbon = Me.RibbonControl
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Inventario RFID"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.chkCapturarNoAsignado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCaptNtExist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMostrarCantidad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCambiaUbicacion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpUltimoInv.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpUltimoInv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpHoraFin.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpHoraFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpHoraInicio.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpHoraInicio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActivo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoConteo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTipoInventario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDobleVerifica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fecha.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fecha.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPropietario.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBodega.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents cmdGuardar As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkCapturarNoAsignado As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label4 As Label
    Friend WithEvents chkCaptNtExist As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents lblMostrarCantidad As Label
    Friend WithEvents chkMostrarCantidad As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkCambiaUbicacion As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblCambiaUbicacion As Label
    Friend WithEvents lblUltInv As Label
    Friend WithEvents dtpUltimoInv As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtpHoraFin As DevExpress.XtraEditors.DateEdit
    Friend WithEvents dtpHoraInicio As DevExpress.XtraEditors.DateEdit
    Friend WithEvents chkActivo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmbTipoConteo As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbTipoInventario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lblFin As Label
    Friend WithEvents lblInicio As Label
    Friend WithEvents lblActivo As Label
    Friend WithEvents chkDobleVerifica As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lblDobleVerif As Label
    Friend WithEvents lblTipoConteo As Label
    Friend WithEvents lblTipoInventario As Label
    Friend WithEvents lblFecha As Label
    Friend WithEvents Fecha As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cmbPropietario As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cmbBodega As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Estado As Label
    Friend WithEvents lblCod As Label
    Friend WithEvents lblPropietario As Label
    Friend WithEvents lblBodega As Label
    Friend WithEvents lblEstado As Label
    Friend WithEvents lblCodigo As Label
    Friend WithEvents cmdActualizar As DevExpress.XtraBars.BarButtonItem
End Class
