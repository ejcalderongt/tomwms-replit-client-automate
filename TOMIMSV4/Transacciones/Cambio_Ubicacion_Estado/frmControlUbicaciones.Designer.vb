<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmControlUbicaciones
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmControlUbicaciones))
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.RibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.LabelComponent1 = New DevExpress.XtraGauges.Win.Base.LabelComponent()
        Me.ArcScaleRangeBarComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent()
        Me.ArcScaleComponent1 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent()
        Me.LabelComponent2 = New DevExpress.XtraGauges.Win.Base.LabelComponent()
        Me.ArcScaleComponent2 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent()
        Me.ArcScaleRangeBarComponent2 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent()
        Me.ImageIndicatorComponent1 = New DevExpress.XtraGauges.Win.Base.ImageIndicatorComponent()
        Me.GaugeControl1 = New DevExpress.XtraGauges.Win.GaugeControl()
        Me.CircularGauge1 = New DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge()
        Me.LabelComponent4 = New DevExpress.XtraGauges.Win.Base.LabelComponent()
        Me.LabelComponent5 = New DevExpress.XtraGauges.Win.Base.LabelComponent()
        Me.LabelComponent6 = New DevExpress.XtraGauges.Win.Base.LabelComponent()
        Me.ArcScaleRangeBarComponent4 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent()
        Me.ArcScaleComponent4 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent()
        Me.LabelComponent3 = New DevExpress.XtraGauges.Win.Base.LabelComponent()
        Me.ArcScaleComponent3 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent()
        Me.ArcScaleRangeBarComponent3 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent()
        Me.ArcScaleRangeBarComponent5 = New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent()
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabelComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleRangeBarComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabelComponent2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleComponent2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleRangeBarComponent2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageIndicatorComponent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CircularGauge1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabelComponent4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabelComponent5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabelComponent6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleRangeBarComponent4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleComponent4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabelComponent3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleComponent3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleRangeBarComponent3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArcScaleRangeBarComponent5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 505)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.RibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(898, 23)
        '
        'RibbonControl
        '
        Me.RibbonControl.ExpandCollapseItem.Id = 0
        Me.RibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl.ExpandCollapseItem, Me.BarButtonItem1, Me.BarButtonItem2})
        Me.RibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl.MaxItemId = 3
        Me.RibbonControl.Name = "RibbonControl"
        Me.RibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1})
        Me.RibbonControl.Size = New System.Drawing.Size(898, 142)
        Me.RibbonControl.StatusBar = Me.RibbonStatusBar
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Salir"
        Me.BarButtonItem1.Glyph = Global.TOMWMS.My.Resources.Resources.exist_b
        Me.BarButtonItem1.Id = 1
        Me.BarButtonItem1.LargeGlyph = Global.TOMWMS.My.Resources.Resources.exist_b
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Actualizar"
        Me.BarButtonItem2.Glyph = CType(resources.GetObject("BarButtonItem2.Glyph"), System.Drawing.Image)
        Me.BarButtonItem2.Id = 2
        Me.BarButtonItem2.LargeGlyph = CType(resources.GetObject("BarButtonItem2.LargeGlyph"), System.Drawing.Image)
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "Opciones de Lista"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem2)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.BarButtonItem1)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        '
        'LabelComponent1
        '
        Me.LabelComponent1.AppearanceText.Font = New System.Drawing.Font("Tahoma", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelComponent1.Name = "circularGauge3_Label1"
        Me.LabelComponent1.Position = New DevExpress.XtraGauges.Core.Base.PointF2D(205.0!, 93.0!)
        Me.LabelComponent1.Size = New System.Drawing.SizeF(60.0!, 40.0!)
        Me.LabelComponent1.Text = "0"
        Me.LabelComponent1.ZOrder = -1001
        '
        'ArcScaleRangeBarComponent1
        '
        Me.ArcScaleRangeBarComponent1.ArcScale = Me.ArcScaleComponent1
        Me.ArcScaleRangeBarComponent1.EndOffset = 0!
        Me.ArcScaleRangeBarComponent1.Name = "circularGauge3_RangeBar2"
        Me.ArcScaleRangeBarComponent1.ShowBackground = True
        Me.ArcScaleRangeBarComponent1.StartOffset = 87.0!
        Me.ArcScaleRangeBarComponent1.ZOrder = -10
        '
        'ArcScaleComponent1
        '
        Me.ArcScaleComponent1.AppearanceMajorTickmark.BorderBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent1.AppearanceMajorTickmark.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent1.AppearanceMinorTickmark.BorderBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent1.AppearanceMinorTickmark.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent1.AppearanceTickmarkText.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.ArcScaleComponent1.AppearanceTickmarkText.TextBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#484E5A")
        Me.ArcScaleComponent1.Center = New DevExpress.XtraGauges.Core.Base.PointF2D(125.0!, 125.0!)
        Me.ArcScaleComponent1.EndAngle = 90.0!
        Me.ArcScaleComponent1.MajorTickCount = 0
        Me.ArcScaleComponent1.MajorTickmark.FormatString = "{0:F0}"
        Me.ArcScaleComponent1.MajorTickmark.ShapeOffset = -14.0!
        Me.ArcScaleComponent1.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_1
        Me.ArcScaleComponent1.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight
        Me.ArcScaleComponent1.MaxValue = 100.0!
        Me.ArcScaleComponent1.MinorTickCount = 0
        Me.ArcScaleComponent1.MinorTickmark.ShapeOffset = -7.0!
        Me.ArcScaleComponent1.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_2
        Me.ArcScaleComponent1.Name = "scale1"
        Me.ArcScaleComponent1.StartAngle = -270.0!
        Me.ArcScaleComponent1.Value = 40.0!
        '
        'LabelComponent2
        '
        Me.LabelComponent2.AppearanceText.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.LabelComponent2.Name = "circularGauge3_Label1"
        Me.LabelComponent2.Position = New DevExpress.XtraGauges.Core.Base.PointF2D(205.0!, 93.0!)
        Me.LabelComponent2.Size = New System.Drawing.SizeF(50.0!, 40.0!)
        Me.LabelComponent2.Text = "95"
        Me.LabelComponent2.ZOrder = -1001
        '
        'ArcScaleComponent2
        '
        Me.ArcScaleComponent2.AppearanceMajorTickmark.BorderBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent2.AppearanceMajorTickmark.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent2.AppearanceMinorTickmark.BorderBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent2.AppearanceMinorTickmark.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent2.AppearanceTickmarkText.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.ArcScaleComponent2.AppearanceTickmarkText.TextBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#484E5A")
        Me.ArcScaleComponent2.Center = New DevExpress.XtraGauges.Core.Base.PointF2D(125.0!, 125.0!)
        Me.ArcScaleComponent2.EndAngle = -45.0!
        Me.ArcScaleComponent2.MajorTickCount = 0
        Me.ArcScaleComponent2.MajorTickmark.FormatString = "{0:F0}"
        Me.ArcScaleComponent2.MajorTickmark.ShapeOffset = -14.0!
        Me.ArcScaleComponent2.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_1
        Me.ArcScaleComponent2.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight
        Me.ArcScaleComponent2.MaxValue = 100.0!
        Me.ArcScaleComponent2.MinorTickCount = 0
        Me.ArcScaleComponent2.MinorTickmark.ShapeOffset = -7.0!
        Me.ArcScaleComponent2.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_2
        Me.ArcScaleComponent2.Name = "scale1"
        Me.ArcScaleComponent2.StartAngle = -270.0!
        Me.ArcScaleComponent2.Value = 40.0!
        '
        'ArcScaleRangeBarComponent2
        '
        Me.ArcScaleRangeBarComponent2.ArcScale = Me.ArcScaleComponent2
        Me.ArcScaleRangeBarComponent2.Name = "circularGauge3_RangeBar2"
        Me.ArcScaleRangeBarComponent2.RoundedCaps = True
        Me.ArcScaleRangeBarComponent2.ShowBackground = True
        Me.ArcScaleRangeBarComponent2.StartOffset = 79.0!
        Me.ArcScaleRangeBarComponent2.ZOrder = -10
        '
        'ImageIndicatorComponent1
        '
        Me.ImageIndicatorComponent1.Image = CType(resources.GetObject("ImageIndicatorComponent1.Image"), System.Drawing.Image)
        Me.ImageIndicatorComponent1.Name = "circularGauge1_ImageIndicator1"
        Me.ImageIndicatorComponent1.Position = New DevExpress.XtraGauges.Core.Base.PointF2D(123.0!, 125.0!)
        Me.ImageIndicatorComponent1.ZOrder = -1001
        '
        'GaugeControl1
        '
        Me.GaugeControl1.BackColor = System.Drawing.Color.Gainsboro
        Me.GaugeControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.GaugeControl1.ColorScheme.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GaugeControl1.ColorScheme.TargetElements = CType(((DevExpress.XtraGauges.Core.Base.TargetElement.RangeBar Or DevExpress.XtraGauges.Core.Base.TargetElement.ImageIndicator) _
            Or DevExpress.XtraGauges.Core.Base.TargetElement.Label), DevExpress.XtraGauges.Core.Base.TargetElement)
        Me.GaugeControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GaugeControl1.Gauges.AddRange(New DevExpress.XtraGauges.Base.IGauge() {Me.CircularGauge1})
        Me.GaugeControl1.Location = New System.Drawing.Point(0, 142)
        Me.GaugeControl1.Name = "GaugeControl1"
        Me.GaugeControl1.Size = New System.Drawing.Size(898, 363)
        Me.GaugeControl1.TabIndex = 5
        '
        'CircularGauge1
        '
        Me.CircularGauge1.AutoSize = DevExpress.Utils.DefaultBoolean.[True]
        Me.CircularGauge1.Bounds = New System.Drawing.Rectangle(6, 6, 886, 351)
        Me.CircularGauge1.Labels.AddRange(New DevExpress.XtraGauges.Win.Base.LabelComponent() {Me.LabelComponent4, Me.LabelComponent5, Me.LabelComponent6})
        Me.CircularGauge1.Name = "CircularGauge1"
        Me.CircularGauge1.RangeBars.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent() {Me.ArcScaleRangeBarComponent4})
        Me.CircularGauge1.Scales.AddRange(New DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent() {Me.ArcScaleComponent4})
        '
        'LabelComponent4
        '
        Me.LabelComponent4.AppearanceText.Font = New System.Drawing.Font("Tahoma", 20.25!)
        Me.LabelComponent4.Name = "circularGauge3_Label1"
        Me.LabelComponent4.Position = New DevExpress.XtraGauges.Core.Base.PointF2D(160.0!, 93.0!)
        Me.LabelComponent4.Size = New System.Drawing.SizeF(70.0!, 40.0!)
        Me.LabelComponent4.Text = "95"
        Me.LabelComponent4.ZOrder = -1001
        '
        'LabelComponent5
        '
        Me.LabelComponent5.AppearanceText.Font = New System.Drawing.Font("Tahoma", 20.25!)
        Me.LabelComponent5.Name = "LabelComponent4Copy0"
        Me.LabelComponent5.Position = New DevExpress.XtraGauges.Core.Base.PointF2D(200.0!, 93.0!)
        Me.LabelComponent5.Size = New System.Drawing.SizeF(15.0!, 40.0!)
        Me.LabelComponent5.Text = "/"
        Me.LabelComponent5.ZOrder = -1001
        '
        'LabelComponent6
        '
        Me.LabelComponent6.AppearanceText.Font = New System.Drawing.Font("Tahoma", 20.25!)
        Me.LabelComponent6.Name = "LabelComponent4Copy1"
        Me.LabelComponent6.Position = New DevExpress.XtraGauges.Core.Base.PointF2D(240.0!, 93.0!)
        Me.LabelComponent6.Size = New System.Drawing.SizeF(70.0!, 40.0!)
        Me.LabelComponent6.Text = "95"
        Me.LabelComponent6.ZOrder = -1001
        '
        'ArcScaleRangeBarComponent4
        '
        Me.ArcScaleRangeBarComponent4.ArcScale = Me.ArcScaleComponent4
        Me.ArcScaleRangeBarComponent4.EndOffset = 0!
        Me.ArcScaleRangeBarComponent4.Name = "circularGauge3_RangeBar2"
        Me.ArcScaleRangeBarComponent4.ShowBackground = True
        Me.ArcScaleRangeBarComponent4.StartOffset = 87.0!
        Me.ArcScaleRangeBarComponent4.ZOrder = -10
        '
        'ArcScaleComponent4
        '
        Me.ArcScaleComponent4.AppearanceMajorTickmark.BorderBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent4.AppearanceMajorTickmark.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent4.AppearanceMinorTickmark.BorderBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent4.AppearanceMinorTickmark.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent4.AppearanceTickmarkText.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.ArcScaleComponent4.AppearanceTickmarkText.TextBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#484E5A")
        Me.ArcScaleComponent4.Center = New DevExpress.XtraGauges.Core.Base.PointF2D(125.0!, 125.0!)
        Me.ArcScaleComponent4.EndAngle = 90.0!
        Me.ArcScaleComponent4.MajorTickCount = 0
        Me.ArcScaleComponent4.MajorTickmark.FormatString = "{0:F0}"
        Me.ArcScaleComponent4.MajorTickmark.ShapeOffset = -14.0!
        Me.ArcScaleComponent4.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_1
        Me.ArcScaleComponent4.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight
        Me.ArcScaleComponent4.MaxValue = 100.0!
        Me.ArcScaleComponent4.MinorTickCount = 0
        Me.ArcScaleComponent4.MinorTickmark.ShapeOffset = -7.0!
        Me.ArcScaleComponent4.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_2
        Me.ArcScaleComponent4.Name = "scale1"
        Me.ArcScaleComponent4.StartAngle = -270.0!
        Me.ArcScaleComponent4.Value = 10.0!
        '
        'LabelComponent3
        '
        Me.LabelComponent3.AppearanceText.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.LabelComponent3.Name = "circularGauge3_Label1"
        Me.LabelComponent3.Position = New DevExpress.XtraGauges.Core.Base.PointF2D(205.0!, 93.0!)
        Me.LabelComponent3.Size = New System.Drawing.SizeF(50.0!, 40.0!)
        Me.LabelComponent3.Text = "95"
        Me.LabelComponent3.ZOrder = -1001
        '
        'ArcScaleComponent3
        '
        Me.ArcScaleComponent3.AppearanceMajorTickmark.BorderBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent3.AppearanceMajorTickmark.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent3.AppearanceMinorTickmark.BorderBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent3.AppearanceMinorTickmark.ContentBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White")
        Me.ArcScaleComponent3.AppearanceTickmarkText.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.ArcScaleComponent3.AppearanceTickmarkText.TextBrush = New DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#484E5A")
        Me.ArcScaleComponent3.Center = New DevExpress.XtraGauges.Core.Base.PointF2D(125.0!, 125.0!)
        Me.ArcScaleComponent3.EndAngle = 90.0!
        Me.ArcScaleComponent3.MajorTickCount = 0
        Me.ArcScaleComponent3.MajorTickmark.FormatString = "{0:F0}"
        Me.ArcScaleComponent3.MajorTickmark.ShapeOffset = -14.0!
        Me.ArcScaleComponent3.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_1
        Me.ArcScaleComponent3.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight
        Me.ArcScaleComponent3.MaxValue = 100.0!
        Me.ArcScaleComponent3.MinorTickCount = 0
        Me.ArcScaleComponent3.MinorTickmark.ShapeOffset = -7.0!
        Me.ArcScaleComponent3.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_2
        Me.ArcScaleComponent3.Name = "scale1"
        Me.ArcScaleComponent3.StartAngle = -270.0!
        Me.ArcScaleComponent3.Value = 40.0!
        '
        'ArcScaleRangeBarComponent3
        '
        Me.ArcScaleRangeBarComponent3.ArcScale = Me.ArcScaleComponent3
        Me.ArcScaleRangeBarComponent3.EndOffset = 0!
        Me.ArcScaleRangeBarComponent3.Name = "circularGauge3_RangeBar2"
        Me.ArcScaleRangeBarComponent3.RoundedCaps = True
        Me.ArcScaleRangeBarComponent3.ShowBackground = True
        Me.ArcScaleRangeBarComponent3.StartOffset = 87.0!
        Me.ArcScaleRangeBarComponent3.ZOrder = -10
        '
        'ArcScaleRangeBarComponent5
        '
        Me.ArcScaleRangeBarComponent5.EndOffset = 0!
        Me.ArcScaleRangeBarComponent5.Name = "ArcScaleRangeBarComponent4Copy0"
        Me.ArcScaleRangeBarComponent5.ShowBackground = True
        Me.ArcScaleRangeBarComponent5.StartOffset = 87.0!
        Me.ArcScaleRangeBarComponent5.ZOrder = -10
        '
        'frmControlUbicaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(898, 528)
        Me.Controls.Add(Me.GaugeControl1)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.RibbonControl)
        Me.Name = "frmControlUbicaciones"
        Me.Ribbon = Me.RibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Text = "Control de Ubicaciones"
        CType(Me.RibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabelComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleRangeBarComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabelComponent2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleComponent2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleRangeBarComponent2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageIndicatorComponent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CircularGauge1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabelComponent4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabelComponent5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabelComponent6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleRangeBarComponent4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleComponent4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabelComponent3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleComponent3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleRangeBarComponent3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArcScaleRangeBarComponent5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Private WithEvents LabelComponent1 As DevExpress.XtraGauges.Win.Base.LabelComponent
    Private WithEvents ArcScaleRangeBarComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent
    Private WithEvents ArcScaleComponent1 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent
    Private WithEvents LabelComponent2 As DevExpress.XtraGauges.Win.Base.LabelComponent
    Private WithEvents ArcScaleComponent2 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent
    Private WithEvents ArcScaleRangeBarComponent2 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent
    Private WithEvents ImageIndicatorComponent1 As DevExpress.XtraGauges.Win.Base.ImageIndicatorComponent
    Friend WithEvents GaugeControl1 As DevExpress.XtraGauges.Win.GaugeControl
    Private WithEvents LabelComponent3 As DevExpress.XtraGauges.Win.Base.LabelComponent
    Private WithEvents ArcScaleRangeBarComponent3 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent
    Private WithEvents ArcScaleComponent3 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent
    Private WithEvents ArcScaleRangeBarComponent5 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent
    Friend WithEvents RibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents CircularGauge1 As DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge
    Private WithEvents LabelComponent4 As DevExpress.XtraGauges.Win.Base.LabelComponent
    Private WithEvents LabelComponent5 As DevExpress.XtraGauges.Win.Base.LabelComponent
    Private WithEvents LabelComponent6 As DevExpress.XtraGauges.Win.Base.LabelComponent
    Private WithEvents ArcScaleRangeBarComponent4 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent
    Private WithEvents ArcScaleComponent4 As DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent
End Class
