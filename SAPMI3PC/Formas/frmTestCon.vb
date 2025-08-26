Imports System.Reflection
Imports SAPbobsCOM

Public Class frmTestCon

    Private oCompany As Company

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

    Private Function Connect_To_Sap1() As Boolean

        Connect_To_Sap1 = False

        Try

            oCompany = New Company
            oCompany.SLDServer = BD.Instancia.LICENSESERVER_SAP_BO
            oCompany.LicenseServer = BD.Instancia.SERVER_BD_SAP
            oCompany.Server = BD.Instancia.SERVER_BD_SAP
            oCompany.CompanyDB = BD.Instancia.SAP_COMPANY_DB
            oCompany.UserName = BD.Instancia.SAP_USR.Trim()
            oCompany.Password = BD.Instancia.SAP_USR_PW.Trim()
            oCompany.DbUserName = BD.Instancia.SAP_DB_USR.Trim()
            oCompany.DbPassword = BD.Instancia.SAP_DB_PW.Trim()
            oCompany.language = BoSuppLangs.ln_Spanish_La
            oCompany.DbServerType = IIf(BD.Instancia.SAP_DB_VERSION = "2017", BoDataServerTypes.dst_MSSQL2017, BoDataServerTypes.dst_MSSQL2019)
            oCompany.UseTrusted = False

            Dim lRetCode As Integer = oCompany.Connect()
            Dim errMsg As String = oCompany.GetLastErrorDescription()
            Dim ErrNo As Integer = oCompany.GetLastErrorCode()
            Dim ErrContext As String = oCompany.GetLastErrorContext()

            If ErrNo <> 0 Then
                MsgBox("No: " & errMsg)
            Else
                Connect_To_Sap1 = True
                MsgBox("Conectado a SAP")
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al conectar a SAP")
        End Try

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles cmdConectar.Click

        Try

            If Not Connect_To_Sap1() Then

            Else
                Desconectar_SAP(oCompany)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al conectar a SAP")
        End Try

    End Sub

    Public Function Desconectar_SAP(ByRef oCompany As Company) As Boolean

        Desconectar_SAP = False

        Try

            If Not IsNothing(oCompany) Then
                If oCompany.Connected Then
                    oCompany.Disconnect()
                End If
            End If

        Catch ex As Exception
            Throw New Exception(String.Format(" (M) {0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Function

    Public Function Conectar_A_SAP_2019(ByRef oCompany As Company,
                                       Optional ByVal pThrowException As Boolean = False,
                                       Optional ByRef pCodigoError As Integer = 0,
                                       Optional ByRef pMensajeError As String = "") As Boolean

        Conectar_A_SAP_2019 = False

        pCodigoError = 0

        Try

            If oCompany Is Nothing Then
                oCompany = New Company
            End If

            If (Not oCompany.Connected) Then

                oCompany = New Company
                oCompany.SLDServer = BD.Instancia.LICENSESERVER_SAP_BO
                oCompany.LicenseServer = BD.Instancia.SERVER_BD_SAP
                oCompany.Server = BD.Instancia.SERVER_BD_SAP
                oCompany.CompanyDB = BD.Instancia.SAP_COMPANY_DB
                oCompany.UserName = BD.Instancia.SAP_USR.Trim()
                oCompany.Password = BD.Instancia.SAP_USR_PW.Trim()
                oCompany.DbUserName = BD.Instancia.SAP_DB_USR.Trim()
                oCompany.DbPassword = BD.Instancia.SAP_DB_PW.Trim()
                oCompany.language = BoSuppLangs.ln_Spanish_La
                oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2019
                oCompany.UseTrusted = False

                Dim lRetCode As Integer = oCompany.Connect()
                Dim errMsg As String = oCompany.GetLastErrorDescription()
                Dim ErrNo As Integer = oCompany.GetLastErrorCode()
                Dim ErrContext As String = oCompany.GetLastErrorContext()

                If lRetCode <> 0 Then
                    oCompany.GetLastError(pCodigoError, pMensajeError)
                    If pThrowException Then
                        Throw New Exception(pMensajeError)
                    End If
                Else
                    Conectar_A_SAP_2019 = True
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class