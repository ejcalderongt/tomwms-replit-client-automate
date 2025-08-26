<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        Button1 = New Button()
        GridControl1 = New DevExpress.XtraGrid.GridControl()
        GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(GridControl1, ComponentModel.ISupportInitialize).BeginInit()
        CType(GridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Dock = DockStyle.Bottom
        Button1.Location = New Point(0, 399)
        Button1.Name = "Button1"
        Button1.Size = New Size(800, 51)
        Button1.TabIndex = 0
        Button1.Text = "Imprimir Sin TAG"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' GridControl1
        ' 
        GridControl1.Dock = DockStyle.Top
        GridControl1.Location = New Point(0, 0)
        GridControl1.MainView = GridView1
        GridControl1.Name = "GridControl1"
        GridControl1.Size = New Size(800, 231)
        GridControl1.TabIndex = 1
        GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {GridView1})
        ' 
        ' GridView1
        ' 
        GridView1.GridControl = GridControl1
        GridView1.Name = "GridView1"
        GridView1.OptionsBehavior.Editable = False
        GridView1.OptionsView.ShowGroupPanel = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(GridControl1)
        Controls.Add(Button1)
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Zebra Print Utility - TOMWMS"
        CType(GridControl1, ComponentModel.ISupportInitialize).EndInit()
        CType(GridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView

End Class
