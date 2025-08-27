Imports DevExpress.XtraEditors
Imports System.Reflection
Imports TOMWMS

Public Class frmLicVencida

    Public Property FechaLicencia As Date
    Public Property FechaServer As Date
    Public Property HostEsNuevoServidorDeLicencias As Boolean = False

    Private Sub PicSolLIcencia_Click(sender As Object, e As EventArgs) Handles PicSolLIcencia.Click
        Try

            If XtraMessageBox.Show("No existe un servidor de licencias activo, ¿Registrar éste ordenador como servidor de licencias?",
              Text,
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim frmSol As New frmLicSolicitud
                frmSol.Modo = frmLicSolicitud.pModo.SRV

                frmSol.mac = clsLnLicencia_item.Get_Mac_Host(AP.HostName)

                If frmSol.ShowDialog() = DialogResult.OK Then
                    DialogResult = DialogResult.OK
                Else
                    DialogResult = DialogResult.Cancel
                End If

            Else
                DialogResult = DialogResult.Cancel
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub btnAplicar_Click(sender As Object, e As EventArgs) Handles btnAplicar.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub frmLicVencida_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblFechaLicencia.Text = FechaLicencia
        lblFechaServer.Text = FechaServer
    End Sub

End Class