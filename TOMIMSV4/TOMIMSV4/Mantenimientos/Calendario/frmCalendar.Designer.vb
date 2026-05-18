Imports TOMWMS.IMS4MBCalendarTableAdapters

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCalendar
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim TimeRuler1 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim TimeRuler2 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Me.SchedulerControl1 = New DevExpress.XtraScheduler.SchedulerControl()
        Me.BarManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.SchedulerStorage = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
        Me.AppointmentsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.IMS4MBCalendar = New IMS4MBCalendar()
        Me.ResourcesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DateNavigator1 = New DevExpress.XtraScheduler.DateNavigator()
        Me.AppointmentsTableAdapter = New AppointmentsTableAdapter()
        Me.ResourcesTableAdapter = New ResourcesTableAdapter()
        CType(Me.SchedulerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SchedulerStorage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AppointmentsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IMS4MBCalendar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResourcesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SchedulerControl1
        '
        Me.SchedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SchedulerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SchedulerControl1.MenuManager = Me.BarManager
        Me.SchedulerControl1.Name = "SchedulerControl1"
        Me.SchedulerControl1.Size = New System.Drawing.Size(711, 474)
        Me.SchedulerControl1.Start = New Date(2016, 5, 18, 0, 0, 0, 0)
        Me.SchedulerControl1.Storage = Me.SchedulerStorage
        Me.SchedulerControl1.TabIndex = 0
        Me.SchedulerControl1.Text = "SchedulerControl1"
        Me.SchedulerControl1.Views.DayView.TimeRulers.Add(TimeRuler1)
        Me.SchedulerControl1.Views.WorkWeekView.TimeRulers.Add(TimeRuler2)
        '
        'BarManager
        '
        Me.BarManager.DockControls.Add(Me.barDockControlTop)
        Me.BarManager.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager.DockControls.Add(Me.barDockControlRight)
        Me.BarManager.Form = Me
        Me.BarManager.MaxItemId = 0
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(890, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 474)
        Me.barDockControlBottom.Size = New System.Drawing.Size(890, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 474)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(890, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 474)
        '
        'SchedulerStorage
        '
        Me.SchedulerStorage.Appointments.CustomFieldMappings.Add(New DevExpress.XtraScheduler.AppointmentCustomFieldMapping("Ubicacion", "Ubicacion"))
        Me.SchedulerStorage.Appointments.DataSource = Me.AppointmentsBindingSource
        Me.SchedulerStorage.Appointments.Mappings.AllDay = "AllDay"
        Me.SchedulerStorage.Appointments.Mappings.Description = "Description"
        Me.SchedulerStorage.Appointments.Mappings.End = "EndDate"
        Me.SchedulerStorage.Appointments.Mappings.Label = "Label"
        Me.SchedulerStorage.Appointments.Mappings.Location = "Location"
        Me.SchedulerStorage.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo"
        Me.SchedulerStorage.Appointments.Mappings.ReminderInfo = "ReminderInfo"
        Me.SchedulerStorage.Appointments.Mappings.ResourceId = "ResourceID"
        Me.SchedulerStorage.Appointments.Mappings.Start = "StartDate"
        Me.SchedulerStorage.Appointments.Mappings.Status = "Status"
        Me.SchedulerStorage.Appointments.Mappings.Subject = "Subject"
        Me.SchedulerStorage.Appointments.Mappings.Type = "Type"
        Me.SchedulerStorage.Resources.DataSource = Me.ResourcesBindingSource
        Me.SchedulerStorage.Resources.Mappings.Caption = "ResourceName"
        Me.SchedulerStorage.Resources.Mappings.Color = "Color"
        Me.SchedulerStorage.Resources.Mappings.Id = "ResourceID"
        Me.SchedulerStorage.Resources.Mappings.Image = "Image"
        '
        'AppointmentsBindingSource
        '
        Me.AppointmentsBindingSource.DataMember = "Appointments"
        Me.AppointmentsBindingSource.DataSource = Me.IMS4MBCalendar
        '
        'IMS4MBCalendar
        '
        Me.IMS4MBCalendar.DataSetName = "IMS4MBCalendar"
        Me.IMS4MBCalendar.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ResourcesBindingSource
        '
        Me.ResourcesBindingSource.DataMember = "Resources"
        Me.ResourcesBindingSource.DataSource = Me.IMS4MBCalendar
        '
        'DateNavigator1
        '
        Me.DateNavigator1.Dock = System.Windows.Forms.DockStyle.Right
        Me.DateNavigator1.HighlightTodayCell = DevExpress.Utils.DefaultBoolean.[Default]
        Me.DateNavigator1.HotDate = Nothing
        Me.DateNavigator1.Location = New System.Drawing.Point(711, 0)
        Me.DateNavigator1.Name = "DateNavigator1"
        Me.DateNavigator1.SchedulerControl = Me.SchedulerControl1
        Me.DateNavigator1.Size = New System.Drawing.Size(179, 474)
        Me.DateNavigator1.TabIndex = 1
        '
        'AppointmentsTableAdapter
        '
        Me.AppointmentsTableAdapter.ClearBeforeFill = True
        '
        'ResourcesTableAdapter
        '
        Me.ResourcesTableAdapter.ClearBeforeFill = True
        '
        'frmCalendar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 474)
        Me.Controls.Add(Me.SchedulerControl1)
        Me.Controls.Add(Me.DateNavigator1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmCalendar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.SchedulerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SchedulerStorage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AppointmentsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IMS4MBCalendar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResourcesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SchedulerControl1 As DevExpress.XtraScheduler.SchedulerControl
    Friend WithEvents SchedulerStorage As DevExpress.XtraScheduler.SchedulerStorage
    Friend WithEvents DateNavigator1 As DevExpress.XtraScheduler.DateNavigator
    Friend WithEvents IMS4MBCalendar As IMS4MBCalendar
    Friend WithEvents AppointmentsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents AppointmentsTableAdapter As AppointmentsTableAdapter
    Friend WithEvents ResourcesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ResourcesTableAdapter As ResourcesTableAdapter
    Friend WithEvents BarManager As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
End Class
