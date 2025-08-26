#Region "Note"
'
'{**************************************************************************************************************}
'{  This file is automatically created when you open the Scheduler Control smart tag                            }
'{  *and click Create Customizable Appointment Dialog.                                                          }
'{  It contains a a descendant of the default appointment editing form created by visual inheritance.           }
'{  In Visual Studio Designer add an editor that is required to edit your appointment custom field.             }
'{  Modify the LoadFormData method to get data from a custom field and fill your editor with data.              }
'{  Modify the SaveFormData method to retrieve data from the editor and set the appointment custom field value. }
'{  The code that displays this form is automatically inserted                                                  }
'{  *in the EditAppointmentFormShowing event handler of the SchedulerControl.                                   }
'{                                                                                                              }
'{**************************************************************************************************************}
'
#End Region ' Note

Imports DevExpress.XtraScheduler

Partial Public Class frmApunte

    Inherits UI.AppointmentForm
    Private pUbicacion As String

    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment)
        MyBase.New(control, apt)
        InitializeComponent()
    End Sub
    Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment, ByVal openRecurrenceForm As Boolean)
        MyBase.New(control, apt, openRecurrenceForm)
        InitializeComponent()
    End Sub
    ''' <summary>
    ''' Add your code to obtain a custom field value and fill the editor with data.
    ''' </summary>
    Public Overrides Sub LoadFormData(ByVal appointment As Appointment)

        MyBase.LoadFormData(appointment)

        If String.IsNullOrEmpty(appointment.CustomFields("Ubicacion")) Then
            tbUbicacion.Text = String.Empty
        Else
            pUbicacion = appointment.CustomFields("Ubicacion").ToString
            tbUbicacion.Text = pUbicacion
        End If

    End Sub

    Public Overrides Function SaveFormData(ByVal appointment As Appointment) As Boolean

        appointment.CustomFields("Ubicacion") = tbUbicacion.Text.Trim
        'Return Me.SaveFormData(appointment)
        Return True

    End Function

    Public Overrides Function IsAppointmentChanged(ByVal appointment As Appointment) As Boolean

        If pUbicacion = appointment.CustomFields("Ubicacion") Then
            Return False
        Else
            Return True
        End If

    End Function

End Class
