Imports System.Reflection
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports TOMWMS

Public Class frmPolizaTemporal
    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick

        Dim BePoliza As New clsBeTmp_trans_pe_pol
        Dim BePolizaExistente As New clsBeTmp_trans_pe_pol
        Dim clsTransaccion As New clsTransaccion()

        Try

            clsTransaccion.Begin_Transaction()

            BePoliza.IdOrdenPedidoPol = 1
            BePoliza.User_agr = "dts"
            BePoliza.Fec_agr = Now
            BePoliza.NoPoliza = txtPoliza.Text
            BePoliza.Pais_procede = ""
            BePoliza.Total_valoraduana = 0
            BePoliza.Total_bultos_peso = 0
            BePoliza.Total_flete = 0
            BePoliza.Total_usd = 0
            BePoliza.Dua = txtDUA.Text
            BePoliza.Fecha_poliza = Now
            BePoliza.Tipo_cambio = 0
            BePoliza.Total_lineas = 1
            BePoliza.Total_bultos = 0
            BePoliza.Total_seguro = 0
            BePoliza.User_mod = "dts"
            BePoliza.Fec_mod = Now
            BePoliza.IdRegimen = 17
            BePoliza.Codigo_poliza = ""
            BePoliza.Ticket = 0
            BePoliza.Numero_orden = txtNoOrden.Text
            BePoliza.Fecha_aceptacion = dtpFechaAceptacion.Value
            BePoliza.Fecha_llegada = dtpFechaLlegada.Value
            BePoliza.Total_otros = 0
            BePoliza.Clave_aduana = ""
            BePoliza.Nit_imp_exp = ""
            BePoliza.Clase = "10"
            BePoliza.Mod_transporte = "3"
            BePoliza.Total_liquidar = 0
            BePoliza.Total_general = 0
            BePoliza.IdOrdenPedidoEnc = 0

            BePolizaExistente = clsLnTmp_trans_pe_pol.Get_Single_By_No_Orden(txtNoOrden.Text, clsTransaccion.lConnection, clsTransaccion.lTransaction)

            If BePolizaExistente Is Nothing Then

                clsLnTmp_trans_pe_pol.Insertar(BePoliza, clsTransaccion.lConnection, clsTransaccion.lTransaction)

                XtraMessageBox.Show("Póliza insertada correctamente " & txtNoOrden.Text, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

            txtPoliza.Text = ""
            txtDUA.Text = ""
            txtNoOrden.Text = ""
            dtpFechaAceptacion.Value = Now
            dtpFechaLlegada.Value = Now

            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            XtraMessageBox.Show(vMsgError, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub frmPolizaTemporal_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        dtpFechaLlegada.Value = Now
        dtpFechaAceptacion.Value = Now
    End Sub
End Class