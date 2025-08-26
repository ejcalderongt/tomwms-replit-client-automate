Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.XtraEditors

'#CKFK enviando cambios
Public Class frmIndicadores

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private fecha_ahora As Date
    Private Title As String

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Private Sub frmIndicadores_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            fecha_ahora = clsServidor.Get_Fecha_Servidor()

            IMS.ListarIndicadoresEnc(cmbOperacion)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub dvIndicadores_ConfigureDataConnection(sender As Object, e As DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs) Handles dvIndicadores.ConfigureDataConnection

        Try

            Dim connectionParameters As CustomStringConnectionParameters = New CustomStringConnectionParameters(clsBD.Instancia.CadenaConexionSQLClient)
            e.ConnectionParameters = connectionParameters
            Dim oParam As New MsSqlConnectionParameters()
            oParam.AuthorizationType = MsSqlAuthorizationType.SqlServer
            oParam.ServerName = clsBD.Instancia.Server
            oParam.DatabaseName = clsBD.Instancia.NombreBD
            oParam.UserName = clsBD.Instancia.Usuario
            oParam.Password = clsBD.Instancia.Clave
            e.ConnectionParameters = oParam
            e.ConnectionParameters = connectionParameters

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub cmbOperacion_EditValueChanged(sender As Object, e As EventArgs) Handles cmbOperacion.EditValueChanged

        Try
            Dim pIndicadorEnc = cmbOperacion.EditValue

            If pIndicadorEnc > 0 Then
                '#GT05062023: lleno combo detalle de indicadores
                'es_recarga = False
                IMS.ListarIndicadoresDet(cmbIndicador, pIndicadorEnc)
            Else
                cmbIndicador.EditValue = 0
            End If

        Catch ex As Exception
            cmbIndicador.Clear()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    'Private es_recarga As Boolean = False
    Private Sub cmbIndicador_EditValueChanged(sender As Object, e As EventArgs) Handles cmbIndicador.EditValueChanged

        Try

            Dim pNombreIndicador = cmbIndicador.Text

            Dim vNombreTemplate As String = CurDir() & "\Tableros_Por_Defecto\"
            vNombreTemplate += pNombreIndicador + ".xml"

            If IO.File.Exists(vNombreTemplate) Then

                Title = ""
                dvIndicadores.Dashboard = Nothing
                dvIndicadores.DashboardSource = vNombreTemplate

            Else
                'es_recarga = False
                dvIndicadores.DashboardSource = Nothing
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub dvIndicadores_CustomParameters(sender As Object, e As CustomParametersEventArgs) Handles dvIndicadores.CustomParameters

        Try

            If Title = "" Then
                Title = dvIndicadores.Dashboard.Title.Text
            End If

            Dim fecha_ini As Date
            Dim fecha_fin As Date
            Dim nTitle = ""

            Dim customParameter = e.Parameters.FirstOrDefault(Function(p) p.Name = "Fecha_Inicial")
            Dim customParameter2 = e.Parameters.FirstOrDefault(Function(p) p.Name = "Fecha_Final")
            Dim customParameter3 = e.Parameters.FirstOrDefault(Function(p) p.Name = "IdBodega")

            If customParameter IsNot Nothing AndAlso customParameter2 IsNot Nothing Then

                customParameter.Value = dtpFechaDel.Value.Date
                customParameter2.Value = dtpFechaAl.Value.Date

                fecha_ini = customParameter.Value
                fecha_fin = customParameter2.Value
                nTitle = Title & " del " & fecha_ini.ToString("d") & " al " & fecha_fin.ToString("d")

            End If

            customParameter3.Value = AP.IdBodega

            dvIndicadores.Dashboard.Title.Text = nTitle

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub dvIndicadores_CustomizeDashboardItemCaption(sender As Object, e As CustomizeDashboardItemCaptionEventArgs)

        Dim viewer As DashboardViewer = DirectCast(sender, DashboardViewer)

        If e.DashboardItemName.Contains("chart") Then
            If viewer.Dashboard.Parameters IsNot Nothing Then
                e.FilterText = String.Format(" {0}", " del " & fecha_ahora.Date.AddDays(-5).ToString("d") & " al " & fecha_ahora.Date.ToString("d"))
            End If
        End If

    End Sub

    Private Sub btnRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRefresh.ItemClick

        Try
            dvIndicadores.ReloadData()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class