
Imports System.ComponentModel
Namespace My

    'This class allows you to handle specific events on the settings class:
    ' The SettingChanging event is raised before a setting's value is changed.
    ' The PropertyChanged event is raised after a setting's value is changed.
    ' The SettingsLoaded event is raised after the setting values are loaded.
    ' The SettingsSaving event is raised before the setting values are saved.
    Partial Friend NotInheritable Class MySettings


        Public Sub New()

        End Sub
        Public Sub New(ByVal owner As IComponent)
            MyBase.New(owner)

        End Sub
        Public Sub New(ByVal settingsKey As String)
            MyBase.New(settingsKey)

        End Sub
        Public Sub New(ByVal owner As IComponent, ByVal settingsKey As String)
            MyBase.New(owner, settingsKey)

        End Sub

        Private Sub MySettings_SettingsLoaded(ByVal sender As Object, ByVal e As System.Configuration.SettingsLoadedEventArgs) Handles Me.SettingsLoaded
            Me.Item("IMS4MB_QAConnectionStringPrograN") = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Me.Item("IMS4MB_ConnectionStringConfigurable")
        End Sub

    End Class
End Namespace
