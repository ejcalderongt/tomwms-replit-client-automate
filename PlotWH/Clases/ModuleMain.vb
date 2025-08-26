Module ModuleMain

    Private ReadOnly Instances() As Process = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName)

    Public mainView As frmDiseño

    Sub Main(ByVal Args As String())

        If Identical_Instance() = False Then
            'Run application
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)

            If Args.Length = 0 Then
                mainView = New frmDiseño
            Else
                Dim IdBodegaFromRemoteCAll As String = Args(0)
                'mainView = New frmDiseño(IdBodegaFromRemoteCAll)
            End If

            Application.Run(mainView) ' I can reference 'mainView' from anywhere in my app, toggle its Visible  property etc.
        Else
            'Do nothing
            'Focus orignal form somehow
            mainView.Focus()
            mainView.BringToFront()
        End If

    End Sub

    Private Function Identical_Instance() As Boolean

        'Open arrays with length determined by number of instances
        Dim Str1(Instances.Length - 1) As String
        Dim Str2(Instances.Length - 1) As String

        'Final string in message box
        Dim MsgString As String = Nothing

        For i = 0 To Instances.Length - 1
            'For each instance store related info
            Str1(i) = "ID: " & Instances(i).Id
            Str2(i) = "    Handle: " & Instances(i).Handle.ToInt32

            'Join strings and carriage return & append to message string
            MsgString = MsgString & Str1(i) & Str2(i) & Chr(13)
        Next

        'Display complete message string (qty of instances = qty of lines of data)
        '        MsgBox(MsgString)

        If Instances.Length = 1 Then
            Return False
        Else
            'Assuming this will prevent any more than 1 instance from opening,
            'it should not get beyond Instances(0) and Instances(1)
            If Instances(0).Handle = Instances(1).Handle Then
                'If handles are the same
                Return True
            Else
                'If handles are different
                Return False
            End If
        End If

    End Function

End Module