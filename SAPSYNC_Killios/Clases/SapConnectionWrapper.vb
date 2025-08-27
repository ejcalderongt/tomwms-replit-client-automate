Imports SAPbobsCOM
Public Class SapConnectionWrapper
    Public Property Company As Company
    Public Property InUse As Boolean = False
    Public Property LastUsed As DateTime
    Public Property Empresa As pEmpresa

    Public Sub New(pEmpresa As pEmpresa)
        Company = New Company()
        InUse = False
        LastUsed = DateTime.Now
        Empresa = pEmpresa
    End Sub

    Public Function Connect() As Boolean

        Try

            Dim instancia = BD.Instancia

            Company.SLDServer = instancia.LICENSESERVER_SAP_BO
            Company.Server = instancia.SERVER_BD_SAP

            If Empresa = pEmpresa.Killios Then
                Company.CompanyDB = instancia.SAP_COMPANY_DB
            Else
                Company.CompanyDB = instancia.SAP_COMPANY_DB2
            End If

            Company.UserName = instancia.SAP_USR.Trim()
            Company.Password = instancia.SAP_USR_PW.Trim()
            Company.DbUserName = instancia.SAP_DB_USR.Trim()
            Company.DbPassword = instancia.SAP_DB_PW.Trim()
            Company.language = BoSuppLangs.ln_Spanish_La
            Company.UseTrusted = False

            If instancia.SAP_DB_VERSION = 2017 Then
                Company.DbServerType = BoDataServerTypes.dst_MSSQL2017
            ElseIf instancia.SAP_DB_VERSION = 2019 Then
                Company.LicenseServer = instancia.SERVER_BD_SAP
                Company.DbServerType = BoDataServerTypes.dst_MSSQL2019
            End If

            Dim result = Company.Connect()
            If result <> 0 Then
                Dim code As Integer, msg As String = ""
                Company.GetLastError(code, msg)
                Throw New Exception($"Error conectando a SAP: {msg}")
            End If

            InUse = False
            Return True

        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Sub Disconnect()
        If Company.Connected Then
            Company.Disconnect()
        End If
        InUse = False
    End Sub
End Class