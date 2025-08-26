Imports DevExpress.XtraScheduler

Public Class frmCalendar01

    Private Sub frmCalendar01_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'IMS4MBCalendar.Resources' table. You can move, or remove it, as needed.
        Me.ResourcesTableAdapter.Fill(Me.IMS4MBCalendar.Resources)
        'TODO: This line of code loads data into the 'IMS4MBCalendar.Appointments' table. You can move, or remove it, as needed.
        Me.AppointmentsTableAdapter.Fill(Me.IMS4MBCalendar.Appointments)

    End Sub

    Private Sub OnAppointmentChangedInsertedDeleted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles SchedulerStorage.AppointmentsInserted, SchedulerStorage.AppointmentsDeleted, SchedulerStorage.AppointmentsChanged
        AppointmentsTableAdapter.Update(IMS4MBCalendar)
        IMS4MBCalendar.AcceptChanges()
    End Sub

End Class