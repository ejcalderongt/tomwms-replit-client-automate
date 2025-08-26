Imports DevExpress.XtraEditors

Public Class frmFactura

    Public pIndex As Integer

    Public pListObjF As List(Of clsBeTrans_re_fact)
    Public Delegate Sub Operar()
    Public Cargar As Operar

    Private Sub Valida()

        If String.IsNullOrEmpty(txtFactura.Text.Trim) Then Throw New Exception("Ingrese Factura.")

    End Sub

    Private Sub Limpiar()

        txtFactura.Text = String.Empty
        txtObservacion.Text = String.Empty
        txtFactura.Focus()

    End Sub

    Private Sub CargarDatos()

        Try

            If pIndex > -1 AndAlso pListObjF IsNot Nothing AndAlso pListObjF.Count > 0 Then

                txtFactura.Text = pListObjF(pIndex).NoFactura
                txtObservacion.Text = pListObjF(pIndex).Observacion
                chkCompleta.Checked = pListObjF(pIndex).Completa

            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click

        Close()

    End Sub

    Private Sub frmFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Limpiar()
        CargarDatos()

    End Sub

    Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click

        Try

            Call Me.Valida()
            Me.Cursor = Cursors.WaitCursor

            If pIndex > -1 Then

                pListObjF(pIndex).Observacion = txtObservacion.Text.Trim
                pListObjF(pIndex).Completa = chkCompleta.Checked

                pListObjF(pIndex).User_mod = AP.UsuarioAp.IdUsuario
                pListObjF(pIndex).Fec_mod = Now

                Dim find As Integer = -1
                find = pListObjF.FindIndex(Function(b) b.NoFactura = txtFactura.Text.Trim)
                If find > -1 Then
                    Throw New Exception(String.Format("El número de factura {0} ya fue ingresada.", txtFactura.Text.Trim))
                End If

                pListObjF(pIndex).NoFactura = txtFactura.Text.Trim

            Else

                Dim ObjF As New clsBeTrans_re_fact

                If pListObjF IsNot Nothing AndAlso pListObjF.Count > 0 Then

                    Dim find As Integer = -1
                    find = pListObjF.FindIndex(Function(b) b.NoFactura = txtFactura.Text.Trim)
                    If find > -1 Then
                        Throw New Exception(String.Format("El número de factura {0} ya fue ingresada.", txtFactura.Text.Trim))
                    End If

                    ObjF.Orden = pListObjF.Max(Function(b) b.Orden) + 1
                Else
                    ObjF.Orden = 1
                End If

                ObjF.NoFactura = txtFactura.Text.Trim
                ObjF.Observacion = txtObservacion.Text.Trim
                ObjF.Completa = chkCompleta.Checked

                ObjF.User_agr = AP.UsuarioAp.IdUsuario
                ObjF.Fec_agr = Now
                ObjF.User_mod = AP.UsuarioAp.IdUsuario
                ObjF.Fec_mod = Now

                ObjF.Completa = chkCompleta.Checked
                ObjF.IsNew = True

                pListObjF.Add(ObjF)

            End If

            Limpiar()

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
            Cargar.Invoke()
        End Try

    End Sub

End Class