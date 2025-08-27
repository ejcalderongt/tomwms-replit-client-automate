Imports DevExpress.XtraSplashScreen

Public Class WaitFormHelper

    Private Shared splash As SplashScreenManager

    Public Shared Sub Show(Optional caption As String = "Cargando...", Optional description As String = "")
        Try
            If splash Is Nothing OrElse Not splash.IsSplashFormVisible Then
                splash = New SplashScreenManager(Nothing, GetType(WaitForm), True, True)
                splash.ShowWaitForm()
            End If

            If splash IsNot Nothing AndAlso splash.IsSplashFormVisible Then
                splash.SetWaitFormCaption(caption)
                splash.SetWaitFormDescription(description)
            End If
        Catch ex As Exception
            ' Puedes loguear aquí si quieres
        End Try
    End Sub

    Public Shared Sub Close()
        Try
            If splash IsNot Nothing AndAlso splash.IsSplashFormVisible Then
                splash.CloseWaitForm()
                splash = Nothing
            End If
        Catch ex As Exception
            ' Puedes loguear aquí si quieres
        End Try
    End Sub

End Class
