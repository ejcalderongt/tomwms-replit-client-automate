Imports DevExpress.Xpf.CodeView

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptRepcionOC
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
        Me.components = New System.ComponentModel.Container()
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary3 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim MsSqlConnectionParameters1 As DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters = New DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters()
        Dim SelectQuery1 As DevExpress.DataAccess.Sql.SelectQuery = New DevExpress.DataAccess.Sql.SelectQuery()
        Dim Column1 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression1 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Table1 As DevExpress.DataAccess.Sql.Table = New DevExpress.DataAccess.Sql.Table()
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
        Dim Column14 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression14 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column15 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression15 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column16 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression16 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column17 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression17 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column18 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression18 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column19 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression19 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column20 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression20 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column21 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression21 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column22 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression22 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column23 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression23 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column24 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression24 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column25 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression25 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column26 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression26 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column27 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression27 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column28 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression28 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column29 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression29 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column30 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression30 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column31 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression31 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column32 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression32 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptRepcionOC))
        Dim MsSqlConnectionParameters2 As DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters = New DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters()
        Dim SelectQuery2 As DevExpress.DataAccess.Sql.SelectQuery = New DevExpress.DataAccess.Sql.SelectQuery()
        Dim Column33 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression33 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Table2 As DevExpress.DataAccess.Sql.Table = New DevExpress.DataAccess.Sql.Table()
        Dim Column34 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression34 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column35 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression35 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column36 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression36 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column37 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression37 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column38 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression38 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column39 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression39 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column40 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression40 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column41 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression41 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column42 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression42 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column43 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression43 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column44 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression44 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column45 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression45 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column46 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression46 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column47 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression47 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column48 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression48 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column49 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression49 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column50 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression50 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column51 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression51 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column52 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression52 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column53 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression53 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column54 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression54 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column55 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression55 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column56 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression56 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column57 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression57 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column58 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression58 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column59 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression59 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column60 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression60 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column61 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression61 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column62 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression62 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column63 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression63 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column64 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression64 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column65 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression65 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column66 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression66 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim SelectQuery3 As DevExpress.DataAccess.Sql.SelectQuery = New DevExpress.DataAccess.Sql.SelectQuery()
        Dim Column67 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression67 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Table3 As DevExpress.DataAccess.Sql.Table = New DevExpress.DataAccess.Sql.Table()
        Dim Column68 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression68 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column69 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression69 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column70 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression70 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column71 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression71 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column72 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression72 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column73 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression73 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column74 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression74 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column75 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression75 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column76 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression76 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column77 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression77 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column78 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression78 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column79 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression79 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column80 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression80 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column81 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression81 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column82 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression82 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column83 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression83 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column84 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression84 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column85 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression85 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column86 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression86 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column87 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression87 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column88 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression88 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column89 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression89 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim SelectQuery4 As DevExpress.DataAccess.Sql.SelectQuery = New DevExpress.DataAccess.Sql.SelectQuery()
        Dim Column90 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression90 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Table4 As DevExpress.DataAccess.Sql.Table = New DevExpress.DataAccess.Sql.Table()
        Dim Column91 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression91 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column92 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression92 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column93 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression93 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column94 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression94 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column95 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression95 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column96 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression96 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column97 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression97 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column98 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression98 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column99 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression99 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column100 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression100 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column101 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression101 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column102 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression102 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column103 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression103 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column104 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression104 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column105 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression105 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column106 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression106 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column107 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression107 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column108 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression108 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column109 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression109 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column110 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression110 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column111 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression111 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column112 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression112 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column113 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression113 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column114 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression114 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column115 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression115 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column116 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression116 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column117 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression117 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column118 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression118 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column119 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression119 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column120 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression120 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column121 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression121 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column122 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression122 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column123 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression123 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column124 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression124 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim Column125 As DevExpress.DataAccess.Sql.Column = New DevExpress.DataAccess.Sql.Column()
        Dim ColumnExpression125 As DevExpress.DataAccess.Sql.ColumnExpression = New DevExpress.DataAccess.Sql.ColumnExpression()
        Dim XrWatermark1 As DevExpress.XtraReports.UI.XRWatermark = New DevExpress.XtraReports.UI.XRWatermark()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow3 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell14 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell15 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell16 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell17 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell18 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell19 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell20 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell21 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell22 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell23 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell25 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell27 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.CantidadesIguales = New DevExpress.XtraReports.UI.FormattingRule()
        Me.Faltante = New DevExpress.XtraReports.UI.FormattingRule()
        Me.Sobrante = New DevExpress.XtraReports.UI.FormattingRule()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.ReportHeaderBand1 = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLogo = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrLabel33 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel32 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel31 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel30 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel29 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel28 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel26 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Empresa = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Bodega = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel27 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Title = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.DetailData3_Odd = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.PageInfo = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel39 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Factura = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel40 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel38 = New DevExpress.XtraReports.UI.XRLabel()
        Me.Tipo = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel37 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel36 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell24 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell26 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell8 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell9 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell10 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell11 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell12 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell13 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Cliente_direccionTableAdapter1 = New cliente_direccion_dsetTableAdapters.cliente_direccionTableAdapter()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrLabel35 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel34 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.SqlDataSource3 = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
        Me.SqlDataSource1 = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.SqlDataSource2 = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
        Me.Diferencia = New DevExpress.XtraReports.UI.CalculatedField()
        Me.Factura = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel39 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel40 = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.BorderColor = System.Drawing.Color.Black
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.Detail.Font = New DevExpress.Drawing.DXFont("Times New Roman", 8.0!)
        Me.Detail.ForeColor = System.Drawing.Color.Black
        Me.Detail.HeightF = 43.4166!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(0!), CInt(0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.Detail.StylePriority.UseBorderColor = False
        Me.Detail.StylePriority.UseFont = False
        Me.Detail.StylePriority.UseForeColor = False
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable1
        '
        Me.XrTable1.LocationFloat = New DevExpress.Utils.PointFloat(10.00027!, 0!)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(96.0!))
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow3})
        Me.XrTable1.SizeF = New System.Drawing.SizeF(726.9996!, 43.4166!)
        '
        'XrTableRow3
        '
        Me.XrTableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell14, Me.XrTableCell15, Me.XrTableCell16, Me.XrTableCell17, Me.XrTableCell18, Me.XrTableCell19, Me.XrTableCell20, Me.XrTableCell21, Me.XrTableCell22, Me.XrTableCell23, Me.XrTableCell25, Me.XrTableCell27})
        Me.XrTableRow3.Name = "XrTableRow3"
        Me.XrTableRow3.Weight = 1.0R
        '
        'XrTableCell14
        '
        Me.XrTableCell14.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.No_Linea")})
        Me.XrTableCell14.Multiline = True
        Me.XrTableCell14.Name = "XrTableCell14"
        Me.XrTableCell14.Weight = 0.46654407501220707R
        '
        'XrTableCell15
        '
        Me.XrTableCell15.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.codigo")})
        Me.XrTableCell15.Multiline = True
        Me.XrTableCell15.Name = "XrTableCell15"
        Me.XrTableCell15.Weight = 0.71062221527099612R
        '
        'XrTableCell16
        '
        Me.XrTableCell16.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.Producto")})
        Me.XrTableCell16.Multiline = True
        Me.XrTableCell16.Name = "XrTableCell16"
        Me.XrTableCell16.Weight = 1.2674728716935855R
        '
        'XrTableCell17
        '
        Me.XrTableCell17.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.lic_plate")})
        Me.XrTableCell17.Multiline = True
        Me.XrTableCell17.Name = "XrTableCell17"
        Me.XrTableCell17.Weight = 0.62580487034400345R
        '
        'XrTableCell18
        '
        Me.XrTableCell18.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.Unidad_Medida")})
        Me.XrTableCell18.Multiline = True
        Me.XrTableCell18.Name = "XrTableCell18"
        Me.XrTableCell18.Weight = 0.46133719743143953R
        '
        'XrTableCell19
        '
        Me.XrTableCell19.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.Presentacion")})
        Me.XrTableCell19.Multiline = True
        Me.XrTableCell19.Name = "XrTableCell19"
        Me.XrTableCell19.Weight = 0.34574806712276845R
        '
        'XrTableCell20
        '
        Me.XrTableCell20.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.fecha_vence")})
        Me.XrTableCell20.Multiline = True
        Me.XrTableCell20.Name = "XrTableCell20"
        Me.XrTableCell20.TextFormatString = "{0:d/MM/yyyy}"
        Me.XrTableCell20.Weight = 0.49836441040039059R
        '
        'XrTableCell21
        '
        Me.XrTableCell21.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.lote")})
        Me.XrTableCell21.Multiline = True
        Me.XrTableCell21.Name = "XrTableCell21"
        Me.XrTableCell21.Weight = 0.49836441040039059R
        '
        'XrTableCell22
        '
        Me.XrTableCell22.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.cantidad")})
        Me.XrTableCell22.Multiline = True
        Me.XrTableCell22.Name = "XrTableCell22"
        Me.XrTableCell22.Weight = 0.60453896984248578R
        '
        'XrTableCell23
        '
        Me.XrTableCell23.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.CantidadRecibida")})
        Me.XrTableCell23.Multiline = True
        Me.XrTableCell23.Name = "XrTableCell23"
        Me.XrTableCell23.Weight = 0.66097360871337929R
        '
        'XrTableCell25
        '
        Me.XrTableCell25.Multiline = True
        Me.XrTableCell25.Name = "XrTableCell25"
        Me.XrTableCell25.Weight = 0.4854974920286223R
        '
        'XrTableCell27
        '
        Me.XrTableCell27.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.EstadoProducto")})
        Me.XrTableCell27.Multiline = True
        Me.XrTableCell27.Name = "XrTableCell27"
        Me.XrTableCell27.Weight = 0.64472911878539363R
        '
        'CantidadesIguales
        '
        Me.CantidadesIguales.Condition = "[Diferencia] = 'Igual'"
        Me.CantidadesIguales.Formatting.Font = New DevExpress.Drawing.DXFont("Microsoft Sans Serif", 8.25!, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.CantidadesIguales.Name = "CantidadesIguales"
        '
        'Faltante
        '
        Me.Faltante.Condition = "[Diferencia]= 'Menor'"
        Me.Faltante.Formatting.BackColor = System.Drawing.Color.Salmon
        Me.Faltante.Formatting.Font = New DevExpress.Drawing.DXFont("Microsoft Sans Serif", 8.25!, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.Faltante.Name = "Faltante"
        '
        'Sobrante
        '
        Me.Sobrante.Condition = "[Diferencia] = 'Mayor'"
        Me.Sobrante.Formatting.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Sobrante.Formatting.Font = New DevExpress.Drawing.DXFont("Microsoft Sans Serif", 8.25!, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.Sobrante.Name = "Sobrante"
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(0!), CInt(0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1, Me.XrPageInfo2})
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(0!), CInt(0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(6.0!, 6.0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(313.0!, 23.0!)
        Me.XrPageInfo1.StyleName = "PageInfo"
        Me.XrPageInfo1.TextFormatString = "{0:dddd, d 'de' MMMM 'de' yyyy HH:mm}"
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(428.0!, 9.999974!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(313.0!, 23.0!)
        Me.XrPageInfo2.StyleName = "PageInfo"
        Me.XrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrPageInfo2.TextFormatString = "Page {0} of {1}"
        '
        'ReportHeaderBand1
        '
        Me.ReportHeaderBand1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLogo})
        Me.ReportHeaderBand1.HeightF = 102.2084!
        Me.ReportHeaderBand1.Name = "ReportHeaderBand1"
        '
        'XrLogo
        '
        Me.XrLogo.LocationFloat = New DevExpress.Utils.PointFloat(5.999994!, 0!)
        Me.XrLogo.Name = "XrLogo"
        Me.XrLogo.SizeF = New System.Drawing.SizeF(197.3107!, 100.0!)
        Me.XrLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'XrLabel33
        '
        Me.XrLabel33.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.placa")})
        Me.XrLabel33.LocationFloat = New DevExpress.Utils.PointFloat(562.0869!, 210.4167!)
        Me.XrLabel33.Name = "XrLabel33"
        Me.XrLabel33.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel33.SizeF = New System.Drawing.SizeF(174.9132!, 22.99998!)
        '
        'XrLabel32
        '
        Me.XrLabel32.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel32.LocationFloat = New DevExpress.Utils.PointFloat(400.9201!, 210.4167!)
        Me.XrLabel32.Name = "XrLabel32"
        Me.XrLabel32.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel32.SizeF = New System.Drawing.SizeF(120.7916!, 22.99998!)
        Me.XrLabel32.StylePriority.UseFont = False
        Me.XrLabel32.Text = "Placas:"
        '
        'XrLabel31
        '
        Me.XrLabel31.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.marca")})
        Me.XrLabel31.LocationFloat = New DevExpress.Utils.PointFloat(562.0869!, 187.4167!)
        Me.XrLabel31.Name = "XrLabel31"
        Me.XrLabel31.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel31.SizeF = New System.Drawing.SizeF(174.9132!, 23.0!)
        '
        'XrLabel30
        '
        Me.XrLabel30.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel30.LocationFloat = New DevExpress.Utils.PointFloat(399.8367!, 187.4167!)
        Me.XrLabel30.Name = "XrLabel30"
        Me.XrLabel30.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel30.SizeF = New System.Drawing.SizeF(121.875!, 23.0!)
        Me.XrLabel30.StylePriority.UseFont = False
        Me.XrLabel30.Text = "Vehículo:"
        '
        'XrLabel29
        '
        Me.XrLabel29.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.NombrePiloto")})
        Me.XrLabel29.LocationFloat = New DevExpress.Utils.PointFloat(121.4585!, 233.4167!)
        Me.XrLabel29.Name = "XrLabel29"
        Me.XrLabel29.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel29.SizeF = New System.Drawing.SizeF(226.4582!, 23.0!)
        '
        'XrLabel28
        '
        Me.XrLabel28.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel28.LocationFloat = New DevExpress.Utils.PointFloat(10.00027!, 233.4167!)
        Me.XrLabel28.Name = "XrLabel28"
        Me.XrLabel28.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel28.SizeF = New System.Drawing.SizeF(111.4583!, 23.0!)
        Me.XrLabel28.StylePriority.UseFont = False
        Me.XrLabel28.Text = "Piloto:"
        '
        'XrLabel26
        '
        Me.XrLabel26.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel26.LocationFloat = New DevExpress.Utils.PointFloat(10.00025!, 187.4167!)
        Me.XrLabel26.Name = "XrLabel26"
        Me.XrLabel26.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel26.SizeF = New System.Drawing.SizeF(111.4583!, 23.0!)
        Me.XrLabel26.StylePriority.UseFont = False
        Me.XrLabel26.Text = "Referencia:"
        '
        'XrLabel25
        '
        Me.XrLabel25.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.hora_fin_pc", "{0:hh:mm:ss tt}")})
        Me.XrLabel25.LocationFloat = New DevExpress.Utils.PointFloat(562.0867!, 164.4166!)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel25.SizeF = New System.Drawing.SizeF(174.9132!, 23.0!)
        '
        'XrLabel24
        '
        Me.XrLabel24.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel24.LocationFloat = New DevExpress.Utils.PointFloat(399.8367!, 164.4168!)
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel24.SizeF = New System.Drawing.SizeF(121.875!, 23.0!)
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.Text = "Hora Fin:"
        '
        'XrLabel23
        '
        Me.XrLabel23.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.hora_ini_pc", "{0:hh:mm:ss tt}")})
        Me.XrLabel23.LocationFloat = New DevExpress.Utils.PointFloat(562.0867!, 141.4166!)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel23.SizeF = New System.Drawing.SizeF(174.9132!, 23.0!)
        '
        'XrLabel22
        '
        Me.XrLabel22.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(399.8367!, 141.4167!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(121.875!, 23.00001!)
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.Text = "Hora Inicio:"
        '
        'XrLabel21
        '
        Me.XrLabel21.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.EstadoRec")})
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(121.4585!, 141.4166!)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(226.4582!, 23.0!)
        '
        'XrLabel20
        '
        Me.XrLabel20.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(10.00025!, 141.4166!)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(111.4583!, 23.00001!)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.Text = "Estado Recepción:"
        '
        'XrLabel15
        '
        Me.XrLabel15.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.No_Documento")})
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(121.4585!, 164.4166!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(226.4582!, 23.00002!)
        '
        'XrLabel14
        '
        Me.XrLabel14.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(10.00025!, 164.4166!)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(111.4583!, 23.00002!)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "No Documento:"
        '
        'XrLabel13
        '
        Me.XrLabel13.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.IdRecepcionEnc")})
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(562.0867!, 95.41664!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(174.9132!, 23.00001!)
        '
        'XrLabel12
        '
        Me.XrLabel12.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.Proveedor")})
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(121.4585!, 118.4166!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(226.4582!, 23.0!)
        '
        'XrLabel11
        '
        Me.XrLabel11.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(10.00025!, 118.4166!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(111.4583!, 23.0!)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.Text = "Proveedor:"
        '
        'XrLabel10
        '
        Me.XrLabel10.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(397.7532!, 95.41664!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(164.3335!, 23.0!)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.Text = "Tarea de recepción WMS#:"
        '
        'XrLabel9
        '
        Me.XrLabel9.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.IdOrdenCompraEnc")})
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(562.0867!, 72.41669!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(174.9132!, 23.0!)
        '
        'XrLabel8
        '
        Me.XrLabel8.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(397.7532!, 72.41669!)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(164.3335!, 23.0!)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.Text = "Pedido de Compra WMS#:"
        '
        'XrLabel7
        '
        Me.XrLabel7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.fecha_recepcion", "{0:dd/MM/yyyy}")})
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(562.0867!, 118.4166!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(174.9132!, 23.0!)
        '
        'XrLabel6
        '
        Me.XrLabel6.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(397.7532!, 118.4166!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(164.3335!, 23.0!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.Text = "Fecha recepción:"
        '
        'XrLabel5
        '
        Me.XrLabel5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.Empresa, "Text", "")})
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(121.4585!, 95.41664!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(226.4582!, 23.0!)
        '
        'Empresa
        '
        Me.Empresa.Description = "Empresa"
        Me.Empresa.Name = "Empresa"
        '
        'XrLabel4
        '
        Me.XrLabel4.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(10.00025!, 95.41664!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(111.4583!, 23.0!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.Text = "Empresa:"
        '
        'XrLabel3
        '
        Me.XrLabel3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.Bodega, "Text", "")})
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(121.4585!, 72.41669!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(226.4582!, 23.0!)
        '
        'Bodega
        '
        Me.Bodega.Description = "Bodega"
        Me.Bodega.Name = "Bodega"
        '
        'XrLabel2
        '
        Me.XrLabel2.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, New DevExpress.Drawing.DXFontAdditionalProperty() {New DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", CType(0, Byte))})
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(10.00025!, 72.41669!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(111.4583!, 23.0!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = "Bodega:"
        '
        'XrLabel1
        '
        Me.XrLabel1.BackColor = System.Drawing.Color.Transparent
        Me.XrLabel1.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel1.Font = New DevExpress.Drawing.DXFont("Times New Roman", 8.5!)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(10.00004!, 8.250034!)
        Me.XrLabel1.Multiline = True
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(730.9999!, 41.16666!)
        Me.XrLabel1.StyleName = "Title"
        Me.XrLabel1.StylePriority.UseBackColor = False
        Me.XrLabel1.StylePriority.UseBorders = False
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "TOM, WMS. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DOCUMENTO: RECEPCIÓN DE PEDIDO DE COMPRA"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel27
        '
        Me.XrLabel27.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.Referencia")})
        Me.XrLabel27.LocationFloat = New DevExpress.Utils.PointFloat(121.4585!, 187.4167!)
        Me.XrLabel27.Name = "XrLabel27"
        Me.XrLabel27.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel27.SizeF = New System.Drawing.SizeF(226.4582!, 23.0!)
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell2, Me.XrTableCell3})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1.0R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Text = "XrTableCell1"
        Me.XrTableCell1.Weight = 1.0R
        '
        'XrTableCell2
        '
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.Text = "XrTableCell2"
        Me.XrTableCell2.Weight = 1.0R
        '
        'XrTableCell3
        '
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Text = "XrTableCell3"
        Me.XrTableCell3.Weight = 1.0R
        '
        'Title
        '
        Me.Title.BackColor = System.Drawing.Color.Transparent
        Me.Title.BorderColor = System.Drawing.Color.Black
        Me.Title.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.Title.BorderWidth = 1.0!
        Me.Title.Font = New DevExpress.Drawing.DXFont("Tahoma", 14.0!)
        Me.Title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.Title.Name = "Title"
        '
        'DetailData3_Odd
        '
        Me.DetailData3_Odd.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(231, Byte), Integer))
        Me.DetailData3_Odd.BorderColor = System.Drawing.Color.Transparent
        Me.DetailData3_Odd.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.DetailData3_Odd.BorderWidth = 1.0!
        Me.DetailData3_Odd.Font = New DevExpress.Drawing.DXFont("Tahoma", 8.0!)
        Me.DetailData3_Odd.ForeColor = System.Drawing.Color.Black
        Me.DetailData3_Odd.Name = "DetailData3_Odd"
        Me.DetailData3_Odd.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(6.0!), CInt(6.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.DetailData3_Odd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'PageInfo
        '
        Me.PageInfo.Font = New DevExpress.Drawing.DXFont("Tahoma", 8.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.PageInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.PageInfo.Name = "PageInfo"
        Me.PageInfo.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel39, Me.XrLabel40, Me.XrLabel38, Me.XrLabel37, Me.XrLabel36, Me.XrLabel33, Me.XrLabel32, Me.XrLabel31, Me.XrLabel30, Me.XrLabel29, Me.XrLabel28, Me.XrLabel26, Me.XrLabel25, Me.XrLabel24, Me.XrLabel23, Me.XrLabel22, Me.XrLabel21, Me.XrLabel20, Me.XrLabel15, Me.XrLabel14, Me.XrLabel13, Me.XrLabel12, Me.XrLabel11, Me.XrLabel10, Me.XrLabel9, Me.XrLabel8, Me.XrLabel7, Me.XrLabel6, Me.XrLabel5, Me.XrLabel4, Me.XrLabel3, Me.XrLabel2, Me.XrLabel27, Me.XrLabel1})
        Me.PageHeader.HeightF = 288.4166!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLabel39
        '
        Me.XrLabel39.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.Factura, "Text", "")})
        Me.XrLabel39.LocationFloat = New DevExpress.Utils.PointFloat(562.0867!, 233.4167!)
        Me.XrLabel39.Multiline = True
        Me.XrLabel39.Name = "XrLabel39"
        Me.XrLabel39.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel39.SizeF = New System.Drawing.SizeF(174.9132!, 44.99992!)
        Me.XrLabel39.Text = "XrLabel39"
        '
        'Factura
        '
        Me.Factura.Description = "Factura"
        Me.Factura.Name = "Factura"
        '
        'XrLabel40
        '
        Me.XrLabel40.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel40.LocationFloat = New DevExpress.Utils.PointFloat(400.9201!, 233.4167!)
        Me.XrLabel40.Name = "XrLabel40"
        Me.XrLabel40.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel40.SizeF = New System.Drawing.SizeF(120.7916!, 22.99998!)
        Me.XrLabel40.StylePriority.UseFont = False
        Me.XrLabel40.Text = "Facturas:"
        '
        'XrLabel38
        '
        Me.XrLabel38.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.Tipo, "Text", "")})
        Me.XrLabel38.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.0!)
        Me.XrLabel38.LocationFloat = New DevExpress.Utils.PointFloat(306.5!, 49.41669!)
        Me.XrLabel38.Name = "XrLabel38"
        Me.XrLabel38.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel38.SizeF = New System.Drawing.SizeF(154.9583!, 23.0!)
        Me.XrLabel38.StylePriority.UseFont = False
        Me.XrLabel38.StylePriority.UseTextAlignment = False
        Me.XrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'Tipo
        '
        Me.Tipo.Description = "Tipo"
        Me.Tipo.Name = "Tipo"
        '
        'XrLabel37
        '
        Me.XrLabel37.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.No_Marchamo")})
        Me.XrLabel37.LocationFloat = New DevExpress.Utils.PointFloat(121.4585!, 210.4167!)
        Me.XrLabel37.Name = "XrLabel37"
        Me.XrLabel37.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel37.SizeF = New System.Drawing.SizeF(226.4582!, 23.0!)
        '
        'XrLabel36
        '
        Me.XrLabel36.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel36.LocationFloat = New DevExpress.Utils.PointFloat(10.00027!, 210.4167!)
        Me.XrLabel36.Name = "XrLabel36"
        Me.XrLabel36.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel36.SizeF = New System.Drawing.SizeF(111.4582!, 23.0!)
        Me.XrLabel36.StylePriority.UseFont = False
        Me.XrLabel36.Text = "No. Marchamo:"
        '
        'XrTable2
        '
        Me.XrTable2.BackColor = System.Drawing.Color.LightGray
        Me.XrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTable2.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrTable2.LocationFloat = New DevExpress.Utils.PointFloat(9.999998!, 0!)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.SizeF = New System.Drawing.SizeF(737.0!, 37.91666!)
        Me.XrTable2.StylePriority.UseBackColor = False
        Me.XrTable2.StylePriority.UseBorders = False
        Me.XrTable2.StylePriority.UseFont = False
        Me.XrTable2.StylePriority.UseTextAlignment = False
        Me.XrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell24, Me.XrTableCell4, Me.XrTableCell5, Me.XrTableCell26, Me.XrTableCell6, Me.XrTableCell7, Me.XrTableCell8, Me.XrTableCell9, Me.XrTableCell10, Me.XrTableCell11, Me.XrTableCell12, Me.XrTableCell13})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'XrTableCell24
        '
        Me.XrTableCell24.Name = "XrTableCell24"
        Me.XrTableCell24.Text = "No. Linea"
        Me.XrTableCell24.Weight = 0.51291653894495592R
        '
        'XrTableCell4
        '
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Text = "Código"
        Me.XrTableCell4.Weight = 0.78125014854558661R
        '
        'XrTableCell5
        '
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.Text = "Producto"
        Me.XrTableCell5.Weight = 1.3934455579659677R
        '
        'XrTableCell26
        '
        Me.XrTableCell26.Multiline = True
        Me.XrTableCell26.Name = "XrTableCell26"
        Me.XrTableCell26.Text = "Licencia"
        Me.XrTableCell26.Weight = 0.68800334807001007R
        '
        'XrTableCell6
        '
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.Text = "UmBas"
        Me.XrTableCell6.Weight = 0.5071889080868186R
        '
        'XrTableCell7
        '
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.Text = "Pres"
        Me.XrTableCell7.Weight = 0.38011261525425472R
        '
        'XrTableCell8
        '
        Me.XrTableCell8.Name = "XrTableCell8"
        Me.XrTableCell8.Text = "Vence"
        Me.XrTableCell8.Weight = 0.531249599669243R
        '
        'XrTableCell9
        '
        Me.XrTableCell9.Name = "XrTableCell9"
        Me.XrTableCell9.Text = "Lote"
        Me.XrTableCell9.Weight = 0.50108329679082331R
        '
        'XrTableCell10
        '
        Me.XrTableCell10.Name = "XrTableCell10"
        Me.XrTableCell10.Text = "CantOC"
        Me.XrTableCell10.Weight = 0.72808332558349831R
        '
        'XrTableCell11
        '
        Me.XrTableCell11.Name = "XrTableCell11"
        Me.XrTableCell11.Text = "CantRec"
        Me.XrTableCell11.Weight = 0.72666642263508618R
        '
        'XrTableCell12
        '
        Me.XrTableCell12.Name = "XrTableCell12"
        Me.XrTableCell12.Text = "Peso"
        Me.XrTableCell12.Weight = 0.53374986987978912R
        '
        'XrTableCell13
        '
        Me.XrTableCell13.Name = "XrTableCell13"
        Me.XrTableCell13.Text = "EstadoPro"
        Me.XrTableCell13.Weight = 0.81874920549891639R
        '
        'Cliente_direccionTableAdapter1
        '
        Me.Cliente_direccionTableAdapter1.ClearBeforeFill = True
        '
        'XrLabel19
        '
        Me.XrLabel19.BackColor = System.Drawing.Color.LightGray
        Me.XrLabel19.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.peso")})
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(597.5641!, 10.00001!)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(52.4361!, 23.0!)
        Me.XrLabel19.StylePriority.UseBackColor = False
        XrSummary1.FormatString = "{0:#.00}"
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel19.Summary = XrSummary1
        '
        'XrLabel18
        '
        Me.XrLabel18.BackColor = System.Drawing.Color.LightGray
        Me.XrLabel18.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.CantidadRecibida")})
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(525.5137!, 10.00001!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(72.05023!, 23.0!)
        Me.XrLabel18.StylePriority.UseBackColor = False
        XrSummary2.FormatString = "{0:#.00}"
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel18.Summary = XrSummary2
        '
        'XrLabel17
        '
        Me.XrLabel17.BackColor = System.Drawing.Color.LightGray
        Me.XrLabel17.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.cantidad")})
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(461.4583!, 10.00001!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(64.05551!, 23.0!)
        Me.XrLabel17.StylePriority.UseBackColor = False
        XrSummary3.FormatString = "{0:#.00}"
        XrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel17.Summary = XrSummary3
        '
        'XrLabel16
        '
        Me.XrLabel16.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(386.4584!, 10.00001!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(74.99988!, 23.0!)
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "Totales:"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPictureBox1, Me.XrLabel35, Me.XrLabel34, Me.XrLine2, Me.XrLine1, Me.XrLabel16, Me.XrLabel17, Me.XrLabel19, Me.XrLabel18})
        Me.GroupFooter1.HeightF = 173.9583!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("ImageSource", Nothing, "VW_REC_CON_OC.firma_piloto")})
        Me.XrPictureBox1.LocationFloat = New DevExpress.Utils.PointFloat(523.7083!, 78.45834!)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.SizeF = New System.Drawing.SizeF(199.2086!, 60.50002!)
        Me.XrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'XrLabel35
        '
        Me.XrLabel35.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.Operador")})
        Me.XrLabel35.LocationFloat = New DevExpress.Utils.PointFloat(523.7083!, 140.9583!)
        Me.XrLabel35.Name = "XrLabel35"
        Me.XrLabel35.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel35.SizeF = New System.Drawing.SizeF(199.2086!, 23.0!)
        Me.XrLabel35.StylePriority.UseTextAlignment = False
        Me.XrLabel35.Text = "XrLabel35"
        Me.XrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel34
        '
        Me.XrLabel34.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "VW_REC_CON_OC.NombrePiloto")})
        Me.XrLabel34.LocationFloat = New DevExpress.Utils.PointFloat(21.45852!, 140.9583!)
        Me.XrLabel34.Name = "XrLabel34"
        Me.XrLabel34.Padding = New DevExpress.XtraPrinting.PaddingInfo(CInt(2.0!), CInt(2.0!), CInt(0!), CInt(0!), CSng(100.0!))
        Me.XrLabel34.SizeF = New System.Drawing.SizeF(198.3332!, 23.0!)
        Me.XrLabel34.StylePriority.UseTextAlignment = False
        Me.XrLabel34.Text = "XrLabel34"
        Me.XrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLine2
        '
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(523.9584!, 138.9584!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(198.9583!, 2.0!)
        '
        'XrLine1
        '
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(20.8334!, 138.9584!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(198.9583!, 2.0!)
        '
        'SqlDataSource3
        '
        Me.SqlDataSource3.ConnectionName = "localhost_IMS4MB_CLC_Connection"
        MsSqlConnectionParameters1.AuthorizationType = DevExpress.DataAccess.ConnectionParameters.MsSqlAuthorizationType.SqlServer
        MsSqlConnectionParameters1.DatabaseName = "IMS4MB_CLC"
        MsSqlConnectionParameters1.ServerName = "GSCLCWMS\SQL2014"
        Me.SqlDataSource3.ConnectionParameters = MsSqlConnectionParameters1
        Me.SqlDataSource3.Name = "SqlDataSource3"
        ColumnExpression1.ColumnName = "IdRecepcionEnc"
        Table1.Name = "VW_REC_CON_OC"
        ColumnExpression1.Table = Table1
        Column1.Expression = ColumnExpression1
        ColumnExpression2.ColumnName = "IdRecepcionDet"
        ColumnExpression2.Table = Table1
        Column2.Expression = ColumnExpression2
        ColumnExpression3.ColumnName = "IdPropietarioBodega"
        ColumnExpression3.Table = Table1
        Column3.Expression = ColumnExpression3
        ColumnExpression4.ColumnName = "Propietario"
        ColumnExpression4.Table = Table1
        Column4.Expression = ColumnExpression4
        ColumnExpression5.ColumnName = "fecha_recepcion"
        ColumnExpression5.Table = Table1
        Column5.Expression = ColumnExpression5
        ColumnExpression6.ColumnName = "hora_ini_pc"
        ColumnExpression6.Table = Table1
        Column6.Expression = ColumnExpression6
        ColumnExpression7.ColumnName = "hora_fin_pc"
        ColumnExpression7.Table = Table1
        Column7.Expression = ColumnExpression7
        ColumnExpression8.ColumnName = "TipoTrans"
        ColumnExpression8.Table = Table1
        Column8.Expression = ColumnExpression8
        ColumnExpression9.ColumnName = "No_Linea"
        ColumnExpression9.Table = Table1
        Column9.Expression = ColumnExpression9
        ColumnExpression10.ColumnName = "codigo"
        ColumnExpression10.Table = Table1
        Column10.Expression = ColumnExpression10
        ColumnExpression11.ColumnName = "codigo_barra"
        ColumnExpression11.Table = Table1
        Column11.Expression = ColumnExpression11
        ColumnExpression12.ColumnName = "Producto"
        ColumnExpression12.Table = Table1
        Column12.Expression = ColumnExpression12
        ColumnExpression13.ColumnName = "CantidadRecibida"
        ColumnExpression13.Table = Table1
        Column13.Expression = ColumnExpression13
        ColumnExpression14.ColumnName = "fecha_ingreso"
        ColumnExpression14.Table = Table1
        Column14.Expression = ColumnExpression14
        ColumnExpression15.ColumnName = "lote"
        ColumnExpression15.Table = Table1
        Column15.Expression = ColumnExpression15
        ColumnExpression16.ColumnName = "fecha_vence"
        ColumnExpression16.Table = Table1
        Column16.Expression = ColumnExpression16
        ColumnExpression17.ColumnName = "EstadoProducto"
        ColumnExpression17.Table = Table1
        Column17.Expression = ColumnExpression17
        ColumnExpression18.ColumnName = "Presentacion"
        ColumnExpression18.Table = Table1
        Column18.Expression = ColumnExpression18
        ColumnExpression19.ColumnName = "EstadoRec"
        ColumnExpression19.Table = Table1
        Column19.Expression = ColumnExpression19
        ColumnExpression20.ColumnName = "Unidad_Medida"
        ColumnExpression20.Table = Table1
        Column20.Expression = ColumnExpression20
        ColumnExpression21.ColumnName = "IdOrdenCompraEnc"
        ColumnExpression21.Table = Table1
        Column21.Expression = ColumnExpression21
        ColumnExpression22.ColumnName = "IdRecepcionOc"
        ColumnExpression22.Table = Table1
        Column22.Expression = ColumnExpression22
        ColumnExpression23.ColumnName = "no_docto"
        ColumnExpression23.Table = Table1
        Column23.Expression = ColumnExpression23
        ColumnExpression24.ColumnName = "Id_Proveedor"
        ColumnExpression24.Table = Table1
        Column24.Expression = ColumnExpression24
        ColumnExpression25.ColumnName = "Proveedor"
        ColumnExpression25.Table = Table1
        Column25.Expression = ColumnExpression25
        ColumnExpression26.ColumnName = "IdProductoBodega"
        ColumnExpression26.Table = Table1
        Column26.Expression = ColumnExpression26
        ColumnExpression27.ColumnName = "IdProveedorBodega"
        ColumnExpression27.Table = Table1
        Column27.Expression = ColumnExpression27
        ColumnExpression28.ColumnName = "cantidad"
        ColumnExpression28.Table = Table1
        Column28.Expression = ColumnExpression28
        ColumnExpression29.ColumnName = "Referencia"
        ColumnExpression29.Table = Table1
        Column29.Expression = ColumnExpression29
        ColumnExpression30.ColumnName = "NombrePiloto"
        ColumnExpression30.Table = Table1
        Column30.Expression = ColumnExpression30
        ColumnExpression31.ColumnName = "placa"
        ColumnExpression31.Table = Table1
        Column31.Expression = ColumnExpression31
        ColumnExpression32.ColumnName = "marca"
        ColumnExpression32.Table = Table1
        Column32.Expression = ColumnExpression32
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
        SelectQuery1.Columns.Add(Column14)
        SelectQuery1.Columns.Add(Column15)
        SelectQuery1.Columns.Add(Column16)
        SelectQuery1.Columns.Add(Column17)
        SelectQuery1.Columns.Add(Column18)
        SelectQuery1.Columns.Add(Column19)
        SelectQuery1.Columns.Add(Column20)
        SelectQuery1.Columns.Add(Column21)
        SelectQuery1.Columns.Add(Column22)
        SelectQuery1.Columns.Add(Column23)
        SelectQuery1.Columns.Add(Column24)
        SelectQuery1.Columns.Add(Column25)
        SelectQuery1.Columns.Add(Column26)
        SelectQuery1.Columns.Add(Column27)
        SelectQuery1.Columns.Add(Column28)
        SelectQuery1.Columns.Add(Column29)
        SelectQuery1.Columns.Add(Column30)
        SelectQuery1.Columns.Add(Column31)
        SelectQuery1.Columns.Add(Column32)
        SelectQuery1.Name = "VW_REC_CON_OC"
        SelectQuery1.Tables.Add(Table1)
        Me.SqlDataSource3.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {SelectQuery1})
        Me.SqlDataSource3.ResultSchemaSerializable = resources.GetString("SqlDataSource3.ResultSchemaSerializable")
        '
        'SqlDataSource1
        '
        Me.SqlDataSource1.ConnectionName = "localhost_Connection"
        MsSqlConnectionParameters2.AuthorizationType = DevExpress.DataAccess.ConnectionParameters.MsSqlAuthorizationType.SqlServer
        MsSqlConnectionParameters2.DatabaseName = "IMS4MB_CLC"
        MsSqlConnectionParameters2.ServerName = "GSCLCWMS\SQL2014"
        Me.SqlDataSource1.ConnectionParameters = MsSqlConnectionParameters2
        Me.SqlDataSource1.Name = "SqlDataSource1"
        ColumnExpression33.ColumnName = "IdRecepcionEnc"
        Table2.Name = "VW_REC_CON_OC"
        ColumnExpression33.Table = Table2
        Column33.Expression = ColumnExpression33
        ColumnExpression34.ColumnName = "IdRecepcionDet"
        ColumnExpression34.Table = Table2
        Column34.Expression = ColumnExpression34
        ColumnExpression35.ColumnName = "IdPropietarioBodega"
        ColumnExpression35.Table = Table2
        Column35.Expression = ColumnExpression35
        ColumnExpression36.ColumnName = "Propietario"
        ColumnExpression36.Table = Table2
        Column36.Expression = ColumnExpression36
        ColumnExpression37.ColumnName = "fecha_recepcion"
        ColumnExpression37.Table = Table2
        Column37.Expression = ColumnExpression37
        ColumnExpression38.ColumnName = "hora_ini_pc"
        ColumnExpression38.Table = Table2
        Column38.Expression = ColumnExpression38
        ColumnExpression39.ColumnName = "hora_fin_pc"
        ColumnExpression39.Table = Table2
        Column39.Expression = ColumnExpression39
        ColumnExpression40.ColumnName = "TipoTrans"
        ColumnExpression40.Table = Table2
        Column40.Expression = ColumnExpression40
        ColumnExpression41.ColumnName = "No_Linea"
        ColumnExpression41.Table = Table2
        Column41.Expression = ColumnExpression41
        ColumnExpression42.ColumnName = "codigo"
        ColumnExpression42.Table = Table2
        Column42.Expression = ColumnExpression42
        ColumnExpression43.ColumnName = "codigo_barra"
        ColumnExpression43.Table = Table2
        Column43.Expression = ColumnExpression43
        ColumnExpression44.ColumnName = "Producto"
        ColumnExpression44.Table = Table2
        Column44.Expression = ColumnExpression44
        ColumnExpression45.ColumnName = "CantidadRecibida"
        ColumnExpression45.Table = Table2
        Column45.Expression = ColumnExpression45
        ColumnExpression46.ColumnName = "fecha_ingreso"
        ColumnExpression46.Table = Table2
        Column46.Expression = ColumnExpression46
        ColumnExpression47.ColumnName = "lote"
        ColumnExpression47.Table = Table2
        Column47.Expression = ColumnExpression47
        ColumnExpression48.ColumnName = "fecha_vence"
        ColumnExpression48.Table = Table2
        Column48.Expression = ColumnExpression48
        ColumnExpression49.ColumnName = "EstadoProducto"
        ColumnExpression49.Table = Table2
        Column49.Expression = ColumnExpression49
        ColumnExpression50.ColumnName = "Presentacion"
        ColumnExpression50.Table = Table2
        Column50.Expression = ColumnExpression50
        ColumnExpression51.ColumnName = "EstadoRec"
        ColumnExpression51.Table = Table2
        Column51.Expression = ColumnExpression51
        ColumnExpression52.ColumnName = "Unidad_Medida"
        ColumnExpression52.Table = Table2
        Column52.Expression = ColumnExpression52
        ColumnExpression53.ColumnName = "IdOrdenCompraEnc"
        ColumnExpression53.Table = Table2
        Column53.Expression = ColumnExpression53
        ColumnExpression54.ColumnName = "IdRecepcionOc"
        ColumnExpression54.Table = Table2
        Column54.Expression = ColumnExpression54
        ColumnExpression55.ColumnName = "no_docto"
        ColumnExpression55.Table = Table2
        Column55.Expression = ColumnExpression55
        ColumnExpression56.ColumnName = "Id_Proveedor"
        ColumnExpression56.Table = Table2
        Column56.Expression = ColumnExpression56
        ColumnExpression57.ColumnName = "Proveedor"
        ColumnExpression57.Table = Table2
        Column57.Expression = ColumnExpression57
        ColumnExpression58.ColumnName = "IdProductoBodega"
        ColumnExpression58.Table = Table2
        Column58.Expression = ColumnExpression58
        ColumnExpression59.ColumnName = "IdProveedorBodega"
        ColumnExpression59.Table = Table2
        Column59.Expression = ColumnExpression59
        ColumnExpression60.ColumnName = "cantidad"
        ColumnExpression60.Table = Table2
        Column60.Expression = ColumnExpression60
        ColumnExpression61.ColumnName = "Referencia"
        ColumnExpression61.Table = Table2
        Column61.Expression = ColumnExpression61
        ColumnExpression62.ColumnName = "NombrePiloto"
        ColumnExpression62.Table = Table2
        Column62.Expression = ColumnExpression62
        ColumnExpression63.ColumnName = "placa"
        ColumnExpression63.Table = Table2
        Column63.Expression = ColumnExpression63
        ColumnExpression64.ColumnName = "marca"
        ColumnExpression64.Table = Table2
        Column64.Expression = ColumnExpression64
        ColumnExpression65.ColumnName = "firma_piloto"
        ColumnExpression65.Table = Table2
        Column65.Expression = ColumnExpression65
        ColumnExpression66.ColumnName = "Operador"
        ColumnExpression66.Table = Table2
        Column66.Expression = ColumnExpression66
        SelectQuery2.Columns.Add(Column33)
        SelectQuery2.Columns.Add(Column34)
        SelectQuery2.Columns.Add(Column35)
        SelectQuery2.Columns.Add(Column36)
        SelectQuery2.Columns.Add(Column37)
        SelectQuery2.Columns.Add(Column38)
        SelectQuery2.Columns.Add(Column39)
        SelectQuery2.Columns.Add(Column40)
        SelectQuery2.Columns.Add(Column41)
        SelectQuery2.Columns.Add(Column42)
        SelectQuery2.Columns.Add(Column43)
        SelectQuery2.Columns.Add(Column44)
        SelectQuery2.Columns.Add(Column45)
        SelectQuery2.Columns.Add(Column46)
        SelectQuery2.Columns.Add(Column47)
        SelectQuery2.Columns.Add(Column48)
        SelectQuery2.Columns.Add(Column49)
        SelectQuery2.Columns.Add(Column50)
        SelectQuery2.Columns.Add(Column51)
        SelectQuery2.Columns.Add(Column52)
        SelectQuery2.Columns.Add(Column53)
        SelectQuery2.Columns.Add(Column54)
        SelectQuery2.Columns.Add(Column55)
        SelectQuery2.Columns.Add(Column56)
        SelectQuery2.Columns.Add(Column57)
        SelectQuery2.Columns.Add(Column58)
        SelectQuery2.Columns.Add(Column59)
        SelectQuery2.Columns.Add(Column60)
        SelectQuery2.Columns.Add(Column61)
        SelectQuery2.Columns.Add(Column62)
        SelectQuery2.Columns.Add(Column63)
        SelectQuery2.Columns.Add(Column64)
        SelectQuery2.Columns.Add(Column65)
        SelectQuery2.Columns.Add(Column66)
        SelectQuery2.Name = "VW_REC_CON_OC"
        SelectQuery2.Tables.Add(Table2)
        Me.SqlDataSource1.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {SelectQuery2})
        Me.SqlDataSource1.ResultSchemaSerializable = resources.GetString("SqlDataSource1.ResultSchemaSerializable")
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2})
        Me.GroupHeader1.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!)
        Me.GroupHeader1.HeightF = 40.625!
        Me.GroupHeader1.Name = "GroupHeader1"
        Me.GroupHeader1.StylePriority.UseFont = False
        '
        'SqlDataSource2
        '
        Me.SqlDataSource2.ConnectionName = "localhost_IMS4MB_CEALSA_QAP_Connection"
        Me.SqlDataSource2.Name = "SqlDataSource2"
        ColumnExpression67.ColumnName = "fecha_recepcion"
        Table3.Name = "VW_RecepcionConOC"
        ColumnExpression67.Table = Table3
        Column67.Expression = ColumnExpression67
        ColumnExpression68.ColumnName = "hora_ini_pc"
        ColumnExpression68.Table = Table3
        Column68.Expression = ColumnExpression68
        ColumnExpression69.ColumnName = "TipoTrans"
        ColumnExpression69.Table = Table3
        Column69.Expression = ColumnExpression69
        ColumnExpression70.ColumnName = "IdOrdenCompraEnc"
        ColumnExpression70.Table = Table3
        Column70.Expression = ColumnExpression70
        ColumnExpression71.ColumnName = "hora_fin_pc"
        ColumnExpression71.Table = Table3
        Column71.Expression = ColumnExpression71
        ColumnExpression72.ColumnName = "No_Documento"
        ColumnExpression72.Table = Table3
        Column72.Expression = ColumnExpression72
        ColumnExpression73.ColumnName = "IdRecepcionEnc"
        ColumnExpression73.Table = Table3
        Column73.Expression = ColumnExpression73
        ColumnExpression74.ColumnName = "No_Linea"
        ColumnExpression74.Table = Table3
        Column74.Expression = ColumnExpression74
        ColumnExpression75.ColumnName = "codigo"
        ColumnExpression75.Table = Table3
        Column75.Expression = ColumnExpression75
        ColumnExpression76.ColumnName = "Producto"
        ColumnExpression76.Table = Table3
        Column76.Expression = ColumnExpression76
        ColumnExpression77.ColumnName = "cantidad"
        ColumnExpression77.Table = Table3
        Column77.Expression = ColumnExpression77
        ColumnExpression78.ColumnName = "CantidadRecibida"
        ColumnExpression78.Table = Table3
        Column78.Expression = ColumnExpression78
        ColumnExpression79.ColumnName = "fecha_ingreso"
        ColumnExpression79.Table = Table3
        Column79.Expression = ColumnExpression79
        ColumnExpression80.ColumnName = "EstadoProducto"
        ColumnExpression80.Table = Table3
        Column80.Expression = ColumnExpression80
        ColumnExpression81.ColumnName = "lote"
        ColumnExpression81.Table = Table3
        Column81.Expression = ColumnExpression81
        ColumnExpression82.ColumnName = "Presentacion"
        ColumnExpression82.Table = Table3
        Column82.Expression = ColumnExpression82
        ColumnExpression83.ColumnName = "Proveedor"
        ColumnExpression83.Table = Table3
        Column83.Expression = ColumnExpression83
        ColumnExpression84.ColumnName = "IdProveedorBodega"
        ColumnExpression84.Table = Table3
        Column84.Expression = ColumnExpression84
        ColumnExpression85.ColumnName = "Id_Proveedor"
        ColumnExpression85.Table = Table3
        Column85.Expression = ColumnExpression85
        ColumnExpression86.ColumnName = "IdEstadoOC"
        ColumnExpression86.Table = Table3
        Column86.Expression = ColumnExpression86
        ColumnExpression87.ColumnName = "EstadoRec"
        ColumnExpression87.Table = Table3
        Column87.Expression = ColumnExpression87
        ColumnExpression88.ColumnName = "Unidad_Medida"
        ColumnExpression88.Table = Table3
        Column88.Expression = ColumnExpression88
        ColumnExpression89.ColumnName = "fecha_vence"
        ColumnExpression89.Table = Table3
        Column89.Expression = ColumnExpression89
        SelectQuery3.Columns.Add(Column67)
        SelectQuery3.Columns.Add(Column68)
        SelectQuery3.Columns.Add(Column69)
        SelectQuery3.Columns.Add(Column70)
        SelectQuery3.Columns.Add(Column71)
        SelectQuery3.Columns.Add(Column72)
        SelectQuery3.Columns.Add(Column73)
        SelectQuery3.Columns.Add(Column74)
        SelectQuery3.Columns.Add(Column75)
        SelectQuery3.Columns.Add(Column76)
        SelectQuery3.Columns.Add(Column77)
        SelectQuery3.Columns.Add(Column78)
        SelectQuery3.Columns.Add(Column79)
        SelectQuery3.Columns.Add(Column80)
        SelectQuery3.Columns.Add(Column81)
        SelectQuery3.Columns.Add(Column82)
        SelectQuery3.Columns.Add(Column83)
        SelectQuery3.Columns.Add(Column84)
        SelectQuery3.Columns.Add(Column85)
        SelectQuery3.Columns.Add(Column86)
        SelectQuery3.Columns.Add(Column87)
        SelectQuery3.Columns.Add(Column88)
        SelectQuery3.Columns.Add(Column89)
        SelectQuery3.Name = "VW_RecepcionConOC"
        SelectQuery3.Tables.Add(Table3)
        ColumnExpression90.ColumnName = "IdRecepcionEnc"
        Table4.MetaSerializable = "<Meta X=""30"" Y=""30"" Width=""125"" Height=""784"" />"
        Table4.Name = "VW_REC_CON_OC"
        ColumnExpression90.Table = Table4
        Column90.Expression = ColumnExpression90
        ColumnExpression91.ColumnName = "IdRecepcionDet"
        ColumnExpression91.Table = Table4
        Column91.Expression = ColumnExpression91
        ColumnExpression92.ColumnName = "IdPropietarioBodega"
        ColumnExpression92.Table = Table4
        Column92.Expression = ColumnExpression92
        ColumnExpression93.ColumnName = "Propietario"
        ColumnExpression93.Table = Table4
        Column93.Expression = ColumnExpression93
        ColumnExpression94.ColumnName = "fecha_recepcion"
        ColumnExpression94.Table = Table4
        Column94.Expression = ColumnExpression94
        ColumnExpression95.ColumnName = "hora_ini_pc"
        ColumnExpression95.Table = Table4
        Column95.Expression = ColumnExpression95
        ColumnExpression96.ColumnName = "hora_fin_pc"
        ColumnExpression96.Table = Table4
        Column96.Expression = ColumnExpression96
        ColumnExpression97.ColumnName = "TipoTrans"
        ColumnExpression97.Table = Table4
        Column97.Expression = ColumnExpression97
        ColumnExpression98.ColumnName = "No_Linea"
        ColumnExpression98.Table = Table4
        Column98.Expression = ColumnExpression98
        ColumnExpression99.ColumnName = "codigo"
        ColumnExpression99.Table = Table4
        Column99.Expression = ColumnExpression99
        ColumnExpression100.ColumnName = "codigo_barra"
        ColumnExpression100.Table = Table4
        Column100.Expression = ColumnExpression100
        ColumnExpression101.ColumnName = "Producto"
        ColumnExpression101.Table = Table4
        Column101.Expression = ColumnExpression101
        ColumnExpression102.ColumnName = "CantidadRecibida"
        ColumnExpression102.Table = Table4
        Column102.Expression = ColumnExpression102
        ColumnExpression103.ColumnName = "fecha_ingreso"
        ColumnExpression103.Table = Table4
        Column103.Expression = ColumnExpression103
        ColumnExpression104.ColumnName = "lote"
        ColumnExpression104.Table = Table4
        Column104.Expression = ColumnExpression104
        ColumnExpression105.ColumnName = "fecha_vence"
        ColumnExpression105.Table = Table4
        Column105.Expression = ColumnExpression105
        ColumnExpression106.ColumnName = "EstadoProducto"
        ColumnExpression106.Table = Table4
        Column106.Expression = ColumnExpression106
        ColumnExpression107.ColumnName = "Presentacion"
        ColumnExpression107.Table = Table4
        Column107.Expression = ColumnExpression107
        ColumnExpression108.ColumnName = "EstadoRec"
        ColumnExpression108.Table = Table4
        Column108.Expression = ColumnExpression108
        ColumnExpression109.ColumnName = "Unidad_Medida"
        ColumnExpression109.Table = Table4
        Column109.Expression = ColumnExpression109
        ColumnExpression110.ColumnName = "IdOrdenCompraEnc"
        ColumnExpression110.Table = Table4
        Column110.Expression = ColumnExpression110
        ColumnExpression111.ColumnName = "IdRecepcionOc"
        ColumnExpression111.Table = Table4
        Column111.Expression = ColumnExpression111
        ColumnExpression112.ColumnName = "no_docto"
        ColumnExpression112.Table = Table4
        Column112.Expression = ColumnExpression112
        ColumnExpression113.ColumnName = "Id_Proveedor"
        ColumnExpression113.Table = Table4
        Column113.Expression = ColumnExpression113
        ColumnExpression114.ColumnName = "Proveedor"
        ColumnExpression114.Table = Table4
        Column114.Expression = ColumnExpression114
        ColumnExpression115.ColumnName = "IdProductoBodega"
        ColumnExpression115.Table = Table4
        Column115.Expression = ColumnExpression115
        ColumnExpression116.ColumnName = "IdProveedorBodega"
        ColumnExpression116.Table = Table4
        Column116.Expression = ColumnExpression116
        ColumnExpression117.ColumnName = "cantidad"
        ColumnExpression117.Table = Table4
        Column117.Expression = ColumnExpression117
        ColumnExpression118.ColumnName = "Referencia"
        ColumnExpression118.Table = Table4
        Column118.Expression = ColumnExpression118
        ColumnExpression119.ColumnName = "NombrePiloto"
        ColumnExpression119.Table = Table4
        Column119.Expression = ColumnExpression119
        ColumnExpression120.ColumnName = "placa"
        ColumnExpression120.Table = Table4
        Column120.Expression = ColumnExpression120
        ColumnExpression121.ColumnName = "marca"
        ColumnExpression121.Table = Table4
        Column121.Expression = ColumnExpression121
        ColumnExpression122.ColumnName = "firma_piloto"
        ColumnExpression122.Table = Table4
        Column122.Expression = ColumnExpression122
        ColumnExpression123.ColumnName = "Operador"
        ColumnExpression123.Table = Table4
        Column123.Expression = ColumnExpression123
        ColumnExpression124.ColumnName = "No_Marchamo"
        ColumnExpression124.Table = Table4
        Column124.Expression = ColumnExpression124
        ColumnExpression125.ColumnName = "lic_plate"
        ColumnExpression125.Table = Table4
        Column125.Expression = ColumnExpression125
        SelectQuery4.Columns.Add(Column90)
        SelectQuery4.Columns.Add(Column91)
        SelectQuery4.Columns.Add(Column92)
        SelectQuery4.Columns.Add(Column93)
        SelectQuery4.Columns.Add(Column94)
        SelectQuery4.Columns.Add(Column95)
        SelectQuery4.Columns.Add(Column96)
        SelectQuery4.Columns.Add(Column97)
        SelectQuery4.Columns.Add(Column98)
        SelectQuery4.Columns.Add(Column99)
        SelectQuery4.Columns.Add(Column100)
        SelectQuery4.Columns.Add(Column101)
        SelectQuery4.Columns.Add(Column102)
        SelectQuery4.Columns.Add(Column103)
        SelectQuery4.Columns.Add(Column104)
        SelectQuery4.Columns.Add(Column105)
        SelectQuery4.Columns.Add(Column106)
        SelectQuery4.Columns.Add(Column107)
        SelectQuery4.Columns.Add(Column108)
        SelectQuery4.Columns.Add(Column109)
        SelectQuery4.Columns.Add(Column110)
        SelectQuery4.Columns.Add(Column111)
        SelectQuery4.Columns.Add(Column112)
        SelectQuery4.Columns.Add(Column113)
        SelectQuery4.Columns.Add(Column114)
        SelectQuery4.Columns.Add(Column115)
        SelectQuery4.Columns.Add(Column116)
        SelectQuery4.Columns.Add(Column117)
        SelectQuery4.Columns.Add(Column118)
        SelectQuery4.Columns.Add(Column119)
        SelectQuery4.Columns.Add(Column120)
        SelectQuery4.Columns.Add(Column121)
        SelectQuery4.Columns.Add(Column122)
        SelectQuery4.Columns.Add(Column123)
        SelectQuery4.Columns.Add(Column124)
        SelectQuery4.Columns.Add(Column125)
        SelectQuery4.Name = "VW_REC_CON_OC"
        SelectQuery4.Tables.Add(Table4)
        Me.SqlDataSource2.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {SelectQuery3, SelectQuery4})
        Me.SqlDataSource2.ResultSchemaSerializable = resources.GetString("SqlDataSource2.ResultSchemaSerializable")
        '
        'Diferencia
        '
        Me.Diferencia.DataMember = "VW_REC_CON_OC"
        Me.Diferencia.Name = "Diferencia"
        '
        'Factura
        '
        Me.Factura.Description = "Factura"
        Me.Factura.Name = "Factura"
        '
        'XrLabel39
        '
        Me.XrLabel39.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.Factura, "Text", "")})
        Me.XrLabel39.LocationFloat = New DevExpress.Utils.PointFloat(562.0867!, 233.4167!)
        Me.XrLabel39.Multiline = True
        Me.XrLabel39.Name = "XrLabel39"
        Me.XrLabel39.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel39.SizeF = New System.Drawing.SizeF(174.9132!, 44.99992!)
        Me.XrLabel39.Text = "XrLabel39"
        '
        'XrLabel40
        '
        Me.XrLabel40.Font = New DevExpress.Drawing.DXFont("Times New Roman", 9.75!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel40.LocationFloat = New DevExpress.Utils.PointFloat(400.9201!, 233.4167!)
        Me.XrLabel40.Name = "XrLabel40"
        Me.XrLabel40.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel40.SizeF = New System.Drawing.SizeF(120.7916!, 22.99998!)
        Me.XrLabel40.StylePriority.UseFont = False
        Me.XrLabel40.Text = "Facturas:"
        '
        'rptRepcionOC
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeaderBand1, Me.PageHeader, Me.GroupFooter1, Me.GroupHeader1})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.Diferencia})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.SqlDataSource3, Me.SqlDataSource1, Me.SqlDataSource2})
        Me.DataAdapter = Me.Cliente_direccionTableAdapter1
        Me.DataMember = "VW_REC_CON_OC"
        Me.DataSource = Me.SqlDataSource2
        Me.FormattingRuleSheet.AddRange(New DevExpress.XtraReports.UI.FormattingRule() {Me.CantidadesIguales, Me.Faltante, Me.Sobrante})
        Me.Margins = New DevExpress.Drawing.DXMargins(51.0!, 52.0!, 0!, 100.0!)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.Empresa, Me.Bodega, Me.Tipo, Me.Factura})
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.Title, Me.DetailData3_Odd, Me.PageInfo})
        Me.Version = "24.1"
        XrWatermark1.Id = "Watermark1"
        Me.Watermarks.AddRange(New DevExpress.XtraPrinting.Drawing.Watermark() {XrWatermark1})
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ReportHeaderBand1 As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Title As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents DetailData3_Odd As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents PageInfo As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Empresa As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents Bodega As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell8 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell9 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell10 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell11 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell12 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell13 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents Cliente_direccionTableAdapter1 As cliente_direccion_dsetTableAdapters.cliente_direccionTableAdapter
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTableCell24 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrLabel26 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel27 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel28 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents SqlDataSource3 As DevExpress.DataAccess.Sql.SqlDataSource
    Friend WithEvents XrLabel33 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel32 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel31 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel30 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel29 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel34 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents SqlDataSource1 As DevExpress.DataAccess.Sql.SqlDataSource
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel35 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrLogo As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel36 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel37 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents SqlDataSource2 As DevExpress.DataAccess.Sql.SqlDataSource
    Friend WithEvents CantidadesIguales As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents Faltante As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents Sobrante As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents Diferencia As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrTableCell26 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow3 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell14 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell15 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell16 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell17 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell18 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell19 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell20 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell21 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell22 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell23 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell25 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell27 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Tipo As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrLabel38 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel39 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Factura As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrLabel40 As DevExpress.XtraReports.UI.XRLabel
End Class
