Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports TOMWMS.frmOrdenCompra

Public Class frmDocIngresoRFID

    Public Enum ModoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As ModoTrans
    Public gBeRFIDEnc As New clsBeI_nav_barras_rfid_enc
    Public Delegate Sub Listar_Ingresos_RFID()
    Public Property InvokeListarIngresosRFID As Listar_Ingresos_RFID

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmDocIngresoRFID_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            'IsLoading = True

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Documento de Ingreso RFID...")

            Select Case Modo

                Case ModoTrans.Nuevo

                Case ModoTrans.Editar
                    Cargar_Datos()
            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Cargar_Datos()
        Try

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub

End Class