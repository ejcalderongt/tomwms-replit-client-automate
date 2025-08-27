Imports System.Reflection
Imports System.Threading
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmBackup
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub frmBackup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataNeeded.SelectedIndex = 0
    End Sub

    Private Sub mnuGenerarBackup_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGenerarBackup.ItemClick

        CheckForIllegalCrossThreadCalls = False
        Dim Thread As New Thread(AddressOf CrearBackup)
        Thread.Start()

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub CrearBackup()

        Try

            If (FileNameSpinEdit.Text.Length > 0 AndAlso PathSpinEdit.Text.Length > 0 AndAlso txtTimer.Value > 0) Then

                'Dim pTimeOut = clsBD.Instancia.TimeOutConBD
                Dim pTimeOut = txtTimer.Value

                EnabledComponents(False)

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Ejecutando Backup...")

                Try

                    If clsMantenimientoBD.Ejecutar_Backup_BD(FileNameSpinEdit.Text, PathSpinEdit.Text, pTimeOut) Then
                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show("Backup creado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ResetComponents()
                    Else
                        SplashScreenManager.CloseForm(False)
                        EnabledComponents(True)
                    End If

                Catch ex As Exception
                    EnabledComponents(True)
                    SplashScreenManager.CloseForm(False)
                    XtraMessageBox.Show("Error al crear el backup, vuelve a intentar", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Try

            Else
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("Debes llenar todos los campos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text,
          MessageBoxButtons.OK,
          MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    ''' <summary>
    '''     Funciones que asignan valores a los componentes
    ''' </summary>
    Private Sub Progreso(_progress As Integer)
        pbProgress.EditValue = _progress
        lbProgress.Text = _progress.ToString()
    End Sub
    Private Sub EnabledComponents(_value As Boolean)
        PathSpinEdit.Enabled = _value
        FileNameSpinEdit.Enabled = _value
        DataNeeded.Enabled = _value
        btnSearchFolder.Enabled = _value
        txtTimer.Enabled = _value
        mnuGenerar.Enabled = _value
    End Sub
    Private Sub ResetComponents()
        PathSpinEdit.Enabled = True
        FileNameSpinEdit.Enabled = True
        DataNeeded.Enabled = True
        btnSearchFolder.Enabled = True
        mnuGenerar.Enabled = True

        FileNameSpinEdit.Text = ""
        PathSpinEdit.Text = ""

        txtTimer.Value = 150

        pbProgress.EditValue = 0
        lbProgress.Text = "0"
    End Sub

    Private Sub btnSearchFolder_Click(sender As Object, e As EventArgs) Handles btnSearchFolder.Click
        If (FolderBrowserDialog.ShowDialog() = DialogResult.OK) Then
            PathSpinEdit.Text = FolderBrowserDialog.SelectedPath
        End If
    End Sub
End Class