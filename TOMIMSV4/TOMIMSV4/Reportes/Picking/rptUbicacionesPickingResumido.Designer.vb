<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptUbicacionesPickingResumido
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim SelectQuery1 As DevExpress.DataAccess.Sql.SelectQuery = New DevExpress.DataAccess.Sql.SelectQuery()
        Dim Column1 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression1 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Table3 As DevExpress.DataAccess.Sql.Table = New DevExpress.DataAccess.Sql.Table()
        Dim Column2 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression2 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column3 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression3 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column4 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression4 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column5 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression5 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column6 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression6 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column7 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression7 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column8 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression8 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column9 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression9 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column10 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression10 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column11 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression11 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column12 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression12 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column13 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression13 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptUbicacionesPickingResumido))
        Me.SqlDataSource1 = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.pageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.pageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.label1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.table1 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell4 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell5 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell6 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell7 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell8 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell9 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell10 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell11 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.table2 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell12 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell13 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell14 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell15 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell16 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell17 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell18 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell19 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell20 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell21 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell22 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Title = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailCaption1 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailData1 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailData3_Odd = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.PageInfo = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.table1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.table2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'SqlDataSource1
        '
        Me.SqlDataSource1.ConnectionName = "localhost_Connection"
        Me.SqlDataSource1.Name = "SqlDataSource1"
        ColumnExpression1.ColumnName = "IdPedido"
        Table3.Name = "VW_Ubicaciones_Picking_Resumido"
        ColumnExpression1.Table = Table3
        Column1.Expression = ColumnExpression1
        ColumnExpression2.ColumnName = "IdPicking"
        ColumnExpression2.Table = Table3
        Column2.Expression = ColumnExpression2
        ColumnExpression3.ColumnName = "codigo"
        ColumnExpression3.Table = Table3
        Column3.Expression = ColumnExpression3
        ColumnExpression4.ColumnName = "nombre"
        ColumnExpression4.Table = Table3
        Column4.Expression = ColumnExpression4
        ColumnExpression5.ColumnName = "Cant_UMBas"
        ColumnExpression5.Table = Table3
        Column5.Expression = ColumnExpression5
        ColumnExpression6.ColumnName = "UMBas"
        ColumnExpression6.Table = Table3
        Column6.Expression = ColumnExpression6
        ColumnExpression7.ColumnName = "Cant_Pres"
        ColumnExpression7.Table = Table3
        Column7.Expression = ColumnExpression7
        ColumnExpression8.ColumnName = "Presentacion"
        ColumnExpression8.Table = Table3
        Column8.Expression = ColumnExpression8
        ColumnExpression9.ColumnName = "peso"
        ColumnExpression9.Table = Table3
        Column9.Expression = ColumnExpression9
        ColumnExpression10.ColumnName = "lote"
        ColumnExpression10.Table = Table3
        Column10.Expression = ColumnExpression10
        ColumnExpression11.ColumnName = "Licencia"
        ColumnExpression11.Table = Table3
        Column11.Expression = ColumnExpression11
        ColumnExpression12.ColumnName = "Fecha_Vence"
        ColumnExpression12.Table = Table3
        Column12.Expression = ColumnExpression12
        ColumnExpression13.ColumnName = "Ubicacion"
        ColumnExpression13.Table = Table3
        Column13.Expression = ColumnExpression13
        SelectQuery1.Columns.Add(Column1)
        SelectQuery1.Columns.Add(Column2)
        SelectQuery1.Columns.Add(Column3)
        SelectQuery1.Columns.Add(Column4)
        SelectQuery1.Columns.Add(Column5)
        SelectQuery1.Columns.Add(Column6)
        SelectQuery1.Columns.Add(Column7)
        SelectQuery1.Columns.Add(Column8)
        SelectQuery1.Columns.Add(Column9)
        SelectQuery1.Columns.Add(Column10)
        SelectQuery1.Columns.Add(Column11)
        SelectQuery1.Columns.Add(Column12)
        SelectQuery1.Columns.Add(Column13)
        SelectQuery1.Name = "VW_Ubicaciones_Picking_Resumido"
        SelectQuery1.Tables.Add(Table3)
        Me.SqlDataSource1.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {SelectQuery1})
        Me.SqlDataSource1.ResultSchemaSerializable = resources.GetString("SqlDataSource1.ResultSchemaSerializable")
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.Name = "TopMargin"
        '
        'BottomMargin
        '
        Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.pageInfo1, Me.pageInfo2})
        Me.BottomMargin.Dpi = 254.0!
        Me.BottomMargin.Name = "BottomMargin"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel2, Me.XrLabel1, Me.label1})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.HeightF = 152.4!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.table1})
        Me.GroupHeader1.Dpi = 254.0!
        Me.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail
        Me.GroupHeader1.HeightF = 71.12!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.table2})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 63.42!
        Me.Detail.HierarchyPrintOptions.Indent = 50.8!
        Me.Detail.Name = "Detail"
        '
        'pageInfo1
        '
        Me.pageInfo1.Dpi = 254.0!
        Me.pageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.pageInfo1.Name = "pageInfo1"
        Me.pageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.pageInfo1.SizeF = New System.Drawing.SizeF(1297.0!, 58.0!)
        Me.pageInfo1.StyleName = "PageInfo"
        '
        'pageInfo2
        '
        Me.pageInfo2.Dpi = 254.0!
        Me.pageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(1297.0!, 0!)
        Me.pageInfo2.Name = "pageInfo2"
        Me.pageInfo2.SizeF = New System.Drawing.SizeF(1297.0!, 58.0!)
        Me.pageInfo2.StyleName = "PageInfo"
        Me.pageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.pageInfo2.TextFormatString = "Page {0} of {1}"
        '
        'label1
        '
        Me.label1.Dpi = 254.0!
        Me.label1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.label1.Name = "label1"
        Me.label1.SizeF = New System.Drawing.SizeF(2594.0!, 61.45361!)
        Me.label1.StyleName = "Title"
        Me.label1.StylePriority.UseTextAlignment = False
        Me.label1.Text = "Ubicaciones picking resumido"
        Me.label1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'table1
        '
        Me.table1.Dpi = 254.0!
        Me.table1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.table1.Name = "table1"
        Me.table1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow1})
        Me.table1.SizeF = New System.Drawing.SizeF(2594.0!, 71.12!)
        '
        'tableRow1
        '
        Me.tableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell1, Me.tableCell2, Me.tableCell3, Me.tableCell4, Me.tableCell5, Me.tableCell6, Me.tableCell7, Me.tableCell8, Me.tableCell9, Me.tableCell10, Me.tableCell11})
        Me.tableRow1.Dpi = 254.0!
        Me.tableRow1.Name = "tableRow1"
        Me.tableRow1.Weight = 1.0R
        '
        'tableCell1
        '
        Me.tableCell1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.tableCell1.Dpi = 254.0!
        Me.tableCell1.Name = "tableCell1"
        Me.tableCell1.StyleName = "DetailCaption1"
        Me.tableCell1.StylePriority.UseBorders = False
        Me.tableCell1.Text = "Código"
        Me.tableCell1.Weight = 0.075438670773826016R
        '
        'tableCell2
        '
        Me.tableCell2.Dpi = 254.0!
        Me.tableCell2.Name = "tableCell2"
        Me.tableCell2.StyleName = "DetailCaption1"
        Me.tableCell2.Text = "Nombre"
        Me.tableCell2.Weight = 0.17500784128189253R
        '
        'tableCell3
        '
        Me.tableCell3.Dpi = 254.0!
        Me.tableCell3.Name = "tableCell3"
        Me.tableCell3.StyleName = "DetailCaption1"
        Me.tableCell3.StylePriority.UseTextAlignment = False
        Me.tableCell3.Text = "Cant UMBas"
        Me.tableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell3.Weight = 0.056498929370022634R
        '
        'tableCell4
        '
        Me.tableCell4.Dpi = 254.0!
        Me.tableCell4.Name = "tableCell4"
        Me.tableCell4.StyleName = "DetailCaption1"
        Me.tableCell4.Text = "UM  Bas"
        Me.tableCell4.Weight = 0.073245569917301148R
        '
        'tableCell5
        '
        Me.tableCell5.Dpi = 254.0!
        Me.tableCell5.Name = "tableCell5"
        Me.tableCell5.StyleName = "DetailCaption1"
        Me.tableCell5.StylePriority.UseTextAlignment = False
        Me.tableCell5.Text = "Cant Pres"
        Me.tableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell5.Weight = 0.0678043885655109R
        '
        'tableCell6
        '
        Me.tableCell6.Dpi = 254.0!
        Me.tableCell6.Multiline = True
        Me.tableCell6.Name = "tableCell6"
        Me.tableCell6.StyleName = "DetailCaption1"
        Me.tableCell6.Text = "Pres" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.tableCell6.Weight = 0.071221957734008176R
        '
        'tableCell7
        '
        Me.tableCell7.Dpi = 254.0!
        Me.tableCell7.Multiline = True
        Me.tableCell7.Name = "tableCell7"
        Me.tableCell7.StyleName = "DetailCaption1"
        Me.tableCell7.StylePriority.UseTextAlignment = False
        Me.tableCell7.Text = "Peso"
        Me.tableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell7.Weight = 0.045716838154997129R
        '
        'tableCell8
        '
        Me.tableCell8.Dpi = 254.0!
        Me.tableCell8.Name = "tableCell8"
        Me.tableCell8.StyleName = "DetailCaption1"
        Me.tableCell8.Text = "Lote"
        Me.tableCell8.Weight = 0.055140204773374814R
        '
        'tableCell9
        '
        Me.tableCell9.Dpi = 254.0!
        Me.tableCell9.Name = "tableCell9"
        Me.tableCell9.StyleName = "DetailCaption1"
        Me.tableCell9.Text = "Licencia"
        Me.tableCell9.Weight = 0.087613352096898661R
        '
        'tableCell10
        '
        Me.tableCell10.Dpi = 254.0!
        Me.tableCell10.Name = "tableCell10"
        Me.tableCell10.StyleName = "DetailCaption1"
        Me.tableCell10.Text = "Fecha Vence"
        Me.tableCell10.Weight = 0.074144630443170656R
        '
        'tableCell11
        '
        Me.tableCell11.Dpi = 254.0!
        Me.tableCell11.Name = "tableCell11"
        Me.tableCell11.StyleName = "DetailCaption1"
        Me.tableCell11.Text = "Ubicacion"
        Me.tableCell11.Weight = 0.21816759924197843R
        '
        'table2
        '
        Me.table2.Dpi = 254.0!
        Me.table2.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.table2.Name = "table2"
        Me.table2.OddStyleName = "DetailData3_Odd"
        Me.table2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow2})
        Me.table2.SizeF = New System.Drawing.SizeF(2594.0!, 63.42!)
        '
        'tableRow2
        '
        Me.tableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell12, Me.tableCell13, Me.tableCell14, Me.tableCell15, Me.tableCell16, Me.tableCell17, Me.tableCell18, Me.tableCell19, Me.tableCell20, Me.tableCell21, Me.tableCell22})
        Me.tableRow2.Dpi = 254.0!
        Me.tableRow2.Name = "tableRow2"
        Me.tableRow2.Weight = 11.683999633789062R
        '
        'tableCell12
        '
        Me.tableCell12.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.tableCell12.Dpi = 254.0!
        Me.tableCell12.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[codigo]")})
        Me.tableCell12.Name = "tableCell12"
        Me.tableCell12.StyleName = "DetailData1"
        Me.tableCell12.StylePriority.UseBorders = False
        Me.tableCell12.Weight = 0.075438670773826016R
        '
        'tableCell13
        '
        Me.tableCell13.Dpi = 254.0!
        Me.tableCell13.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[nombre]")})
        Me.tableCell13.Name = "tableCell13"
        Me.tableCell13.StyleName = "DetailData1"
        Me.tableCell13.Weight = 0.17500784128189253R
        '
        'tableCell14
        '
        Me.tableCell14.Dpi = 254.0!
        Me.tableCell14.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Cant_UMBas]")})
        Me.tableCell14.Name = "tableCell14"
        Me.tableCell14.StyleName = "DetailData1"
        Me.tableCell14.StylePriority.UseTextAlignment = False
        Me.tableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell14.Weight = 0.056498929370022634R
        '
        'tableCell15
        '
        Me.tableCell15.Dpi = 254.0!
        Me.tableCell15.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[UMBas]")})
        Me.tableCell15.Name = "tableCell15"
        Me.tableCell15.StyleName = "DetailData1"
        Me.tableCell15.Weight = 0.073245569917301148R
        '
        'tableCell16
        '
        Me.tableCell16.Dpi = 254.0!
        Me.tableCell16.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Cant_Pres]")})
        Me.tableCell16.Name = "tableCell16"
        Me.tableCell16.StyleName = "DetailData1"
        Me.tableCell16.StylePriority.UseTextAlignment = False
        Me.tableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell16.TextFormatString = "{0:#.00}"
        Me.tableCell16.Weight = 0.0678043885655109R
        '
        'tableCell17
        '
        Me.tableCell17.Dpi = 254.0!
        Me.tableCell17.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Presentacion]")})
        Me.tableCell17.Name = "tableCell17"
        Me.tableCell17.StyleName = "DetailData1"
        Me.tableCell17.Weight = 0.071221957734008176R
        '
        'tableCell18
        '
        Me.tableCell18.Dpi = 254.0!
        Me.tableCell18.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[peso]")})
        Me.tableCell18.Name = "tableCell18"
        Me.tableCell18.StyleName = "DetailData1"
        Me.tableCell18.StylePriority.UseTextAlignment = False
        Me.tableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell18.TextFormatString = "{0:#.00}"
        Me.tableCell18.Weight = 0.045716838154997129R
        '
        'tableCell19
        '
        Me.tableCell19.Dpi = 254.0!
        Me.tableCell19.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[lote]")})
        Me.tableCell19.Name = "tableCell19"
        Me.tableCell19.StyleName = "DetailData1"
        Me.tableCell19.Weight = 0.055140204773374821R
        '
        'tableCell20
        '
        Me.tableCell20.Dpi = 254.0!
        Me.tableCell20.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Licencia]")})
        Me.tableCell20.Name = "tableCell20"
        Me.tableCell20.StyleName = "DetailData1"
        Me.tableCell20.Weight = 0.087613352096898647R
        '
        'tableCell21
        '
        Me.tableCell21.Dpi = 254.0!
        Me.tableCell21.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Fecha_Vence]")})
        Me.tableCell21.Name = "tableCell21"
        Me.tableCell21.StyleName = "DetailData1"
        Me.tableCell21.TextFormatString = "{0:dd/MM/yyyy}"
        Me.tableCell21.Weight = 0.074144536325737836R
        '
        'tableCell22
        '
        Me.tableCell22.Dpi = 254.0!
        Me.tableCell22.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Ubicacion]")})
        Me.tableCell22.Name = "tableCell22"
        Me.tableCell22.StyleName = "DetailData1"
        Me.tableCell22.Weight = 0.21816769335941122R
        '
        'Title
        '
        Me.Title.BackColor = System.Drawing.Color.Transparent
        Me.Title.BorderColor = System.Drawing.Color.Black
        Me.Title.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.Title.BorderWidth = 1.0!
        Me.Title.Font = New DevExpress.Drawing.DXFont("Arial", 14.25!)
        Me.Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Title.Name = "Title"
        Me.Title.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(15), CInt(15), CInt(0), CInt(0), CSng(254.0!))
        '
        'DetailCaption1
        '
        Me.DetailCaption1.BackColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.DetailCaption1.BorderColor = System.Drawing.Color.White
        Me.DetailCaption1.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.DetailCaption1.BorderWidth = 2.0!
        Me.DetailCaption1.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.DetailCaption1.ForeColor = System.Drawing.Color.White
        Me.DetailCaption1.Name = "DetailCaption1"
        Me.DetailCaption1.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(15), CInt(15), CInt(0), CInt(0), CSng(254.0!))
        Me.DetailCaption1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailData1
        '
        Me.DetailData1.BorderColor = System.Drawing.Color.Transparent
        Me.DetailData1.Borders = DevExpress.XtraPrinting.BorderSide.Left
        Me.DetailData1.BorderWidth = 2.0!
        Me.DetailData1.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.DetailData1.ForeColor = System.Drawing.Color.Black
        Me.DetailData1.Name = "DetailData1"
        Me.DetailData1.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(15), CInt(15), CInt(0), CInt(0), CSng(254.0!))
        Me.DetailData1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'DetailData3_Odd
        '
        Me.DetailData3_Odd.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(231, Byte), Integer))
        Me.DetailData3_Odd.BorderColor = System.Drawing.Color.Transparent
        Me.DetailData3_Odd.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.DetailData3_Odd.BorderWidth = 1.0!
        Me.DetailData3_Odd.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!)
        Me.DetailData3_Odd.ForeColor = System.Drawing.Color.Black
        Me.DetailData3_Odd.Name = "DetailData3_Odd"
        Me.DetailData3_Odd.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(15), CInt(15), CInt(0), CInt(0), CSng(254.0!))
        Me.DetailData3_Odd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'PageInfo
        '
        Me.PageInfo.Font = New DevExpress.Drawing.DXFont("Arial", 8.25!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.PageInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.PageInfo.Name = "PageInfo"
        Me.PageInfo.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(15), CInt(15), CInt(0), CInt(0), CSng(254.0!))
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[IdPicking]")})
        Me.XrLabel1.Font = New DevExpress.Drawing.DXFont("Arial", 14.25!)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(1317.25!, 61.45361!)
        Me.XrLabel1.Multiline = True
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2), CInt(2), CInt(0), CInt(0), CSng(96.0!))
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "XrLabel1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel2
        '
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(1017.717!, 61.45358!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(299.5337!, 61.45361!)
        Me.XrLabel2.StyleName = "Title"
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "No. picking:"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'rptUbicacionesPickingResumido
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.GroupHeader1, Me.Detail})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.SqlDataSource1})
        Me.DataMember = "VW_Ubicaciones_Picking_Resumido"
        Me.DataSource = Me.SqlDataSource1
        Me.Dpi = 254.0!
        Me.Font = New DevExpress.Drawing.DXFont("Arial", 9.75!)
        Me.Landscape = True
        Me.Margins = New DevExpress.Drawing.DXMargins(100, 100, 100, 100)
        Me.PageHeight = 2159
        Me.PageWidth = 2794
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.SnapGridSize = 25.0!
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.Title, Me.DetailCaption1, Me.DetailData1, Me.DetailData3_Odd, Me.PageInfo})
        Me.Version = "21.2"
        CType(Me.table1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.table2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents SqlDataSource1 As DevExpress.DataAccess.Sql.SqlDataSource
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents pageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents pageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents label1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents table1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell8 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell9 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell10 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell11 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents table2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell12 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell13 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell14 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell15 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell16 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell17 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell18 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell19 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell20 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell21 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell22 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Title As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailCaption1 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailData1 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailData3_Odd As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents PageInfo As DevExpress.XtraReports.UI.XRControlStyle
End Class
