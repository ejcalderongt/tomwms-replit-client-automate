Imports System.Data.SqlClient
Imports Sap.Data.Hana

Public Class clsSyncSAPCampaña

    Private Shared Function ConstruirQueryCampaña(Campaña As Integer) As String

        Return "SELECT  TOP 1 ""DocEntry"", ""Remark"", ""U_FechaInicial"", ""U_FechaFinal"" " &
               "FROM ""@APERIODOTIEMPO"" " &
               "WHERE ""DocEntry"" = " & Campaña & " " &
               " ORDER BY ""LogInst"" DESC"

    End Function

    Public Shared Function Insertar_Campaña_From_Sap_Hana(Campaña As Integer,
                                                         lConnection As SqlConnection,
                                                         lTransaction As SqlTransaction) As Integer

        Insertar_Campaña_From_Sap_Hana = 0

        Dim i As Integer = 0

        Try

            BeConfigEnc = clsLnI_nav_config_enc.GetSingle(BD.Instancia.IdConfiguracionInterface, lConnection, lTransaction)

            Using conn As HanaConnection = HanaHelper.OpenDB()

                Dim query As String = ConstruirQueryCampaña(Campaña)
                Dim dt As DataTable = HanaHelper.OpenDT(query, conn)

                If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                    Throw New Exception("Error_20250105: Campaña no encontrada")
                    Return False
                End If

                Dim BeCampaña = MapearCampaña(dt.Rows(0), BeConfigEnc.IdUsuario, lConnection, lTransaction)
                If Not clsLnCampaña.Existe_By_Codigo(BeCampaña.Codigo, lConnection, lTransaction) Then
                    clsLnCampaña.Insertar(BeCampaña, lConnection, lTransaction)
                    Insertar_Campaña_From_Sap_Hana = BeCampaña.IdCampaña
                End If

            End Using

        Catch ex As Exception
            Throw
        End Try

    End Function

    Private Shared Function MapearCampaña(enc As DataRow, Usuario As String, lConnection As SqlConnection, lTransaction As SqlTransaction) As clsBeCampaña
        Dim be As New clsBeCampaña()
        be = New clsBeCampaña
        be.IdCampaña = Convert.ToInt32(enc("DocEntry"))
        be.Codigo = Convert.ToInt32(enc("DocEntry"))
        be.Nombre = enc("Remark")
        be.FechaInicio = Convert.ToDateTime(enc("U_FechaInicial"))
        be.FechaFin = Convert.ToDateTime(enc("U_FechaFinal"))
        be.Activo = True
        be.Fec_agr = Now
        be.Fec_mod = Now
        be.User_agr = Usuario
        be.User_mod = Usuario
        Return be
    End Function

End Class
