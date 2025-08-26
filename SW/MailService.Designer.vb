Imports System.ServiceProcess

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MailService
    Inherits System.ServiceProcess.ServiceBase

    'UserService overrides dispose to clean up the component list.
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

    ' The main entry point for the process
    '<MTAThread()> _
    '<System.Diagnostics.DebuggerNonUserCode()> _
    'Shared Sub Main()
    '    Dim ServicesToRun() As System.ServiceProcess.ServiceBase

    '    ' More than one NT Service may run within the same process. To add
    '    ' another service to this process, change the following line to
    '    ' create a second service object. For example,
    '    '
    '    '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
    '    '
    '    ServicesToRun = New System.ServiceProcess.ServiceBase() {New MailService}

    '    System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    'End Sub

    ' The main entry point for the process
    <MTAThread()> _
    <System.Diagnostics.DebuggerNonUserCode()> _
    Shared Sub Main()
#If (Not Debug) Then
        Dim ServicesToRun() As ServiceBase
        ServicesToRun = New ServiceBase() {New MailService}
        ServiceBase.Run(ServicesToRun)
#Else
        Dim myServ As MailService = New MailService
        myServ.Inicializar()
        ' here Process is my Service function
        ' that will run when my service onstart is call
        ' you need to call your own method or function name here instead of Process();
#End If
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        Me.ServiceName = "Service1"
    End Sub

End Class
