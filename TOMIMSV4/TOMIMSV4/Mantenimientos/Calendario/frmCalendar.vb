Imports DevExpress.XtraScheduler

Public Class frmCalendar


    Private Sub frmCalendar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'IMS4MBCalendar.Resources' table. You can move, or remove it, as needed.
        Me.ResourcesTableAdapter.Fill(Me.IMS4MBCalendar.Resources)
        'TODO: This line of code loads data into the 'IMS4MBCalendar.Appointments' table. You can move, or remove it, as needed.
        Me.AppointmentsTableAdapter.Fill(IMS4MBCalendar.Appointments)

    End Sub

    Private Sub OnAppointmentChangedInsertedDeleted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
        AppointmentsTableAdapter.Update(IMS4MBCalendar)
        IMS4MBCalendar.AcceptChanges()
    End Sub

    Private Sub SchedulerControl1_EditAppointmentFormShowing(sender As Object, e As AppointmentFormEventArgs) Handles SchedulerControl1.EditAppointmentFormShowing
        Dim scheduler As SchedulerControl = CType(sender, SchedulerControl)
        Dim form As frmApunte = New frmApunte(scheduler, e.Appointment, e.OpenRecurrenceForm)
        Try
            e.DialogResult = form.ShowDialog
            e.Handled = True
        Finally
            form.Dispose()
        End Try

    End Sub
End Class