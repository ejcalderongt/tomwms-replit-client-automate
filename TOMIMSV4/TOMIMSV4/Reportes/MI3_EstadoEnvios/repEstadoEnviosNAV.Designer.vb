<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class repEstadoEnviosNAV
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim Series1 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim PieSeriesLabel1 As DevExpress.XtraCharts.PieSeriesLabel = New DevExpress.XtraCharts.PieSeriesLabel()
        Dim PieSeriesView1 As DevExpress.XtraCharts.PieSeriesView = New DevExpress.XtraCharts.PieSeriesView()
        Dim ChartTitle1 As DevExpress.XtraCharts.ChartTitle = New DevExpress.XtraCharts.ChartTitle()
        Dim Series2 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim PieSeriesLabel2 As DevExpress.XtraCharts.PieSeriesLabel = New DevExpress.XtraCharts.PieSeriesLabel()
        Dim PieSeriesView2 As DevExpress.XtraCharts.PieSeriesView = New DevExpress.XtraCharts.PieSeriesView()
        Dim ChartTitle2 As DevExpress.XtraCharts.ChartTitle = New DevExpress.XtraCharts.ChartTitle()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Usuario = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.DsEnviosNav1 = New TOMWMS.dsEnviosNav1()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.FechaFin = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.FechaIni = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.SubBand1 = New DevExpress.XtraReports.UI.SubBand()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.TotalPT = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.TotalPC = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrChart1 = New DevExpress.XtraReports.UI.XRChart()
        Me.DtEstadoEnviosPTTableAdapter = New TOMWMS.dsEnviosNav1TableAdapters.dtEstadoEnviosPTTableAdapter()
        Me.XrChart2 = New DevExpress.XtraReports.UI.XRChart()
        Me.DtEstadoEnvioPCTableAdapter1 = New TOMWMS.dsEnviosNav1TableAdapters.dtEstadoEnvioPCTableAdapter()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        CType(Me.DsEnviosNav1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrChart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(PieSeriesLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(PieSeriesView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrChart2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(PieSeriesLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(PieSeriesView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Expanded = False
        Me.Detail.HeightF = 25.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(0), CInt(0), CInt(0), CInt(0), CSng(100.0!))
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        Me.Detail.Visible = False
        '
        'TopMargin
        '
        Me.TopMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel18, Me.XrPageInfo1})
        Me.TopMargin.HeightF = 52.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(0), CInt(0), CInt(0), CInt(0), CSng(100.0!))
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel18
        '
        Me.XrLabel18.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.Usuario, "Text", "Usuario: {0}")})
        Me.XrLabel18.Font = New DevExpress.Drawing.DXFont("Verdana", 9.0!, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(0!, 29.0!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2), CInt(2), CInt(0), CInt(0), CSng(100.0!))
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(301.8749!, 23.0!)
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.Text = "XrLabel18"
        '
        'Usuario
        '
        Me.Usuario.Description = "Usuario"
        Me.Usuario.Name = "Usuario"
        Me.Usuario.Visible = False
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New DevExpress.Drawing.DXFont("Verdana", 9.0!, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrPageInfo1.Format = "Fecha impresión: {0}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(350.0!, 29.0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2), CInt(2), CInt(0), CInt(0), CSng(100.0!))
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(305.0!, 23.0!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        '
        'DsEnviosNav1
        '
        Me.DsEnviosNav1.DataSetName = "dsEnviosNav"
        Me.DsEnviosNav1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 54.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(0), CInt(0), CInt(0), CInt(0), CSng(100.0!))
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel20, Me.XrLabel17, Me.XrLabel16, Me.XrLabel15, Me.XrLabel19, Me.XrLabel1})
        Me.PageHeader.HeightF = 123.9583!
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.SubBands.AddRange(New DevExpress.XtraReports.UI.SubBand() {Me.SubBand1})
        '
        'XrLabel20
        '
        Me.XrLabel20.Font = New DevExpress.Drawing.DXFont("Verdana", 9.0!)
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(336.4584!, 90.95834!)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2), CInt(2), CInt(0), CInt(0), CSng(100.0!))
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(37.5!, 23.0!)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "al"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel17
        '
        Me.XrLabel17.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.FechaFin, "Text", "{0:dd/MM/yyyy}")})
        Me.XrLabel17.Font = New DevExpress.Drawing.DXFont("Verdana", 9.0!)
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(373.9583!, 90.95834!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2), CInt(2), CInt(0), CInt(0), CSng(100.0!))
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(88.54166!, 23.0!)
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'FechaFin
        '
        Me.FechaFin.Description = "Fecha final"
        Me.FechaFin.Name = "FechaFin"
        Me.FechaFin.Type = GetType(Date)
        Me.FechaFin.ValueInfo = "1900-01-01"
        Me.FechaFin.Visible = False
        '
        'XrLabel16
        '
        Me.XrLabel16.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.FechaIni, "Text", "{0:dd/MM/yyyy}")})
        Me.XrLabel16.Font = New DevExpress.Drawing.DXFont("Verdana", 9.0!)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(239.375!, 90.95834!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2), CInt(2), CInt(0), CInt(0), CSng(100.0!))
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(95.83331!, 23.0!)
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'FechaIni
        '
        Me.FechaIni.Description = "Fecha inicial"
        Me.FechaIni.Name = "FechaIni"
        Me.FechaIni.Type = GetType(Date)
        Me.FechaIni.ValueInfo = "1900-01-01"
        Me.FechaIni.Visible = False
        '
        'XrLabel15
        '
        Me.XrLabel15.Font = New DevExpress.Drawing.DXFont("Verdana", 14.25!, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 43.33334!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2), CInt(2), CInt(0), CInt(0), CSng(100.0!))
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(640.0!, 28.62499!)
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "Estado de los envíos a NAV"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel19
        '
        Me.XrLabel19.Font = New DevExpress.Drawing.DXFont("Verdana", 9.0!)
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(199.7915!, 90.95834!)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2), CInt(2), CInt(0), CInt(0), CSng(100.0!))
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(38.5417!, 23.0!)
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.Text = "Del"
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New DevExpress.Drawing.DXFont("Verdana", 10.0!)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 20.33334!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2), CInt(2), CInt(0), CInt(0), CSng(100.0!))
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(640.0!, 23.0!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "TOMIMS,WMS. Reporte de:"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'SubBand1
        '
        Me.SubBand1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel3, Me.XrLabel2, Me.XrChart1, Me.XrChart2})
        Me.SubBand1.HeightF = 344.1669!
        Me.SubBand1.KeepTogether = True
        Me.SubBand1.Name = "SubBand1"
        '
        'XrLabel3
        '
        Me.XrLabel3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.TotalPT, "Text", "Total despachos: {0}")})
        Me.XrLabel3.Font = New DevExpress.Drawing.DXFont("Verdana", 9.75!, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(350.0!, 311.1669!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2), CInt(2), CInt(0), CInt(0), CSng(100.0!))
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(295.0!, 23.0!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'TotalPT
        '
        Me.TotalPT.Description = "Total pedidos de transferencia"
        Me.TotalPT.Name = "TotalPT"
        Me.TotalPT.Type = GetType(Integer)
        Me.TotalPT.ValueInfo = "0"
        Me.TotalPT.Visible = False
        '
        'XrLabel2
        '
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.TotalPC, "Text", "Total recepciones: {0}")})
        Me.XrLabel2.Font = New DevExpress.Drawing.DXFont("Verdana", 9.75!, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(0!, 311.1669!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2), CInt(2), CInt(0), CInt(0), CSng(100.0!))
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(301.8749!, 23.0!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'TotalPC
        '
        Me.TotalPC.Description = "Total Pedidos de compra"
        Me.TotalPC.Name = "TotalPC"
        Me.TotalPC.Type = GetType(Integer)
        Me.TotalPC.ValueInfo = "0"
        Me.TotalPC.Visible = False
        '
        'XrChart1
        '
        Me.XrChart1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.XrChart1.BorderColor = System.Drawing.Color.Black
        Me.XrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrChart1.DataAdapter = Me.DtEstadoEnviosPTTableAdapter
        Me.XrChart1.DataSource = Me.DsEnviosNav1
        Me.XrChart1.Legend.Name = "Default Legend"
        Me.XrChart1.LocationFloat = New DevExpress.Utils.PointFloat(348.1251!, 10.00001!)
        Me.XrChart1.Name = "XrChart1"
        Series1.ArgumentDataMember = "dtEstadoEnviosPT.ESTADO"
        PieSeriesLabel1.TextPattern = "{V}"
        Series1.Label = PieSeriesLabel1
        Series1.LegendTextPattern = "{A}"
        Series1.Name = "Series 1"
        Series1.ToolTipHintDataMember = "dtEstadoEnviosPT.ESTADO"
        Series1.ToolTipPointPattern = "{A}{V}"
        Series1.ValueDataMembersSerializable = "dtEstadoEnviosPT.Cantidad"
        Series1.View = PieSeriesView1
        Me.XrChart1.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series1}
        Me.XrChart1.SeriesTemplate.ArgumentDataMember = "dtEstadoEnviosPT.ESTADO"
        Me.XrChart1.SeriesTemplate.ToolTipHintDataMember = "dtEstadoEnviosPT.Estado"
        Me.XrChart1.SeriesTemplate.ValueDataMembersSerializable = "dtEstadoEnviosPT.Cantidad"
        Me.XrChart1.SizeF = New System.Drawing.SizeF(301.8749!, 288.3334!)
        Me.XrChart1.StylePriority.UseBackColor = False
        ChartTitle1.Text = "Despachos"
        Me.XrChart1.Titles.AddRange(New DevExpress.XtraCharts.ChartTitle() {ChartTitle1})
        '
        'DtEstadoEnviosPTTableAdapter
        '
        Me.DtEstadoEnviosPTTableAdapter.ClearBeforeFill = True
        '
        'XrChart2
        '
        Me.XrChart2.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.XrChart2.BorderColor = System.Drawing.Color.Black
        Me.XrChart2.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrChart2.DataAdapter = Me.DtEstadoEnvioPCTableAdapter1
        Me.XrChart2.DataSource = Me.DsEnviosNav1
        Me.XrChart2.Legend.Name = "Default Legend"
        Me.XrChart2.LocationFloat = New DevExpress.Utils.PointFloat(1.874892!, 10.00001!)
        Me.XrChart2.Name = "XrChart2"
        Series2.ArgumentDataMember = "dtEstadoEnvioPC.ESTADO"
        PieSeriesLabel2.TextPattern = "{V}"
        Series2.Label = PieSeriesLabel2
        Series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[True]
        Series2.LegendName = "Default Legend"
        Series2.LegendTextPattern = "{A}"
        Series2.Name = "Estado"
        Series2.ToolTipHintDataMember = "dtEstadoEnvioPC.ESTADO"
        Series2.ToolTipPointPattern = "{A}{S}"
        Series2.ValueDataMembersSerializable = "dtEstadoEnvioPC.Cantidad"
        Series2.View = PieSeriesView2
        Me.XrChart2.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series2}
        Me.XrChart2.SeriesTemplate.ArgumentDataMember = "dtEstadoEnvioPC.ESTADO"
        Me.XrChart2.SeriesTemplate.ToolTipHintDataMember = "dtEstadoEnvioPC.Estado"
        Me.XrChart2.SeriesTemplate.ValueDataMembersSerializable = "dtEstadoEnvioPC.Cantidad"
        Me.XrChart2.SizeF = New System.Drawing.SizeF(300.0!, 288.3334!)
        Me.XrChart2.StylePriority.UseBackColor = False
        ChartTitle2.Text = "Recepciones"
        Me.XrChart2.Titles.AddRange(New DevExpress.XtraCharts.ChartTitle() {ChartTitle2})
        '
        'DtEstadoEnvioPCTableAdapter1
        '
        Me.DtEstadoEnvioPCTableAdapter1.ClearBeforeFill = True
        '
        'ReportHeader
        '
        Me.ReportHeader.HeightF = 0!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'repEstadoEnviosNAV
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.ReportHeader})
        Me.DataMember = "dtEstadoEnvios"
        Me.DataSource = Me.DsEnviosNav1
        Me.Margins = New DevExpress.Drawing.DXMargins(100, 95, 52, 54)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.Usuario, Me.FechaIni, Me.FechaFin, Me.TotalPC, Me.TotalPT})
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "17.1"
        CType(Me.DsEnviosNav1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(PieSeriesLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(PieSeriesView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrChart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(PieSeriesLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(PieSeriesView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrChart2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents DsEnviosNav1 As dsEnviosNav1
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents SubBand1 As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents XrChart1 As DevExpress.XtraReports.UI.XRChart
    Friend WithEvents DtEstadoEnviosPTTableAdapter As dsEnviosNav1TableAdapters.dtEstadoEnviosPTTableAdapter
    Friend WithEvents XrChart2 As DevExpress.XtraReports.UI.XRChart
    Friend WithEvents DtEstadoEnvioPCTableAdapter1 As dsEnviosNav1TableAdapters.dtEstadoEnvioPCTableAdapter
    Friend WithEvents Usuario As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents FechaIni As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents FechaFin As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents TotalPC As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents TotalPT As DevExpress.XtraReports.Parameters.Parameter
End Class
