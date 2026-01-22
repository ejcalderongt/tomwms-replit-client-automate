Public Class frmTestCon

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            txtLICENSESERVER_SAP_BO.Text = BD.Instancia.LICENSESERVER_SAP_BO
            txtSERVER_BD_SAP.Text = BD.Instancia.SERVER_BD_SAP
            txtSAP_COMPANY_DB.Text = BD.Instancia.SAP_COMPANY_DB
            txtSAP_USR.Text = BD.Instancia.SAP_USR.Trim()
            txtSAP_DB_PW.Text = BD.Instancia.SAP_USR_PW.Trim()
            txtSAP_DB_USR.Text = BD.Instancia.SAP_DB_USR.Trim()
            txtSAP_DB_PW.Text = BD.Instancia.SAP_DB_PW.Trim()

        Catch ex As Exception

        End Try

    End Sub



End Class