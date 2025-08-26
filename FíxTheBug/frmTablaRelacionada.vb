Imports System.Reflection
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports TOMWMS

Public Class frmTablaRelacionada

	Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick

		Dim BeTablaRelacionada As New clsBeTablas_relacionadas
		Dim BeTablaRelacionaExistente As New clsBeTablas_relacionadas
		Dim clsTransaccion As New clsTransaccion()

		Try

			clsTransaccion.Begin_Transaction()

			BeTablaRelacionada.Tabla = "WMS"
			BeTablaRelacionada.Unidad = "BULTOS"
			BeTablaRelacionada.Descripcion = ""
			BeTablaRelacionada.Cantidad = 0
			BeTablaRelacionada.Año = Now.Year
			BeTablaRelacionada.Correlativo = 0
			BeTablaRelacionada.Turno = 0
			BeTablaRelacionada.Fecha_orden_entrega = Now
			BeTablaRelacionada.Agente_aduanal = txtAgenteAduanal.Text
			BeTablaRelacionada.Correlativo1 = 0
			BeTablaRelacionada.NoOrdenSalida = ""
			BeTablaRelacionada.Coaniorec = 0
			BeTablaRelacionada.Covehiculo = ""
			BeTablaRelacionada.Coplacas = ""
			BeTablaRelacionada.Copoliza = txtCopoliza.Text
			BeTablaRelacionada.Observacion = ""
			BeTablaRelacionada.Tmoriginal = 0
			BeTablaRelacionada.Tmsalidas = 0
			BeTablaRelacionada.Crfecha = Now
			BeTablaRelacionada.Consignatario = ""
			BeTablaRelacionada.Utilizada = 0

			BeTablaRelacionaExistente = clsLnTablas_relacionadas.Existe(BeTablaRelacionada, clsTransaccion.lConnection, clsTransaccion.lTransaction)

			If BeTablaRelacionaExistente Is Nothing Then

				clsLnTablas_relacionadas.Insertar(BeTablaRelacionada, clsTransaccion.lConnection, clsTransaccion.lTransaction)

				XtraMessageBox.Show("Tabla relacionada insertada correctamente Número de orden: " & BeTablaRelacionada.Copoliza & " IdPedido: " & BeTablaRelacionada.Agente_aduanal, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

			Else

				If XtraMessageBox.Show("Este número de orden ya existe: " & BeTablaRelacionada.Copoliza & " para el IdPedido: " & BeTablaRelacionada.Agente_aduanal & ", va a modificar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

					clsLnTablas_relacionadas.Actualizar_Parcial_WMS(BeTablaRelacionada, BeTablaRelacionaExistente, clsTransaccion.lConnection, clsTransaccion.lTransaction)

					XtraMessageBox.Show("Tabla relacionada insertada correctamente Número de orden: " & BeTablaRelacionada.Copoliza & " IdPedido: " & BeTablaRelacionada.Agente_aduanal, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

				End If

			End If

			txtAgenteAduanal.Text = ""
			txtCopoliza.Text = ""

			clsTransaccion.Commit_Transaction()

		Catch ex As Exception
			clsTransaccion.RollBack_Transaction()
			Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
			clsLnLog_error_wms.Agregar_Error(vMsgError)
			XtraMessageBox.Show(vMsgError, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		End Try
	End Sub
End Class