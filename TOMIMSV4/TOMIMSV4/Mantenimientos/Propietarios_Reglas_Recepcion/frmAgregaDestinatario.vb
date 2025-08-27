Imports DevExpress.XtraEditors

Public Class frmAgregaDestinatario

    Public pIndex As Integer
    Public pIdReglaPropietarioEnc As Integer
    Public pIdReglaPropietarioDet As Integer
    Public pIdPropietario As Integer
    Public pNombrePropietario As String
    Public pListObjPR As List(Of clsBePropietario_reglas_det)
    Public Delegate Sub Operar()
    Public Cargar As Operar

    Private Sub Valida()

        If cmbDestinatario.SelectedIndex = -1 Then Throw New Exception("Seleccione Destinatario.")

    End Sub

    Private Sub Limpiar()

        cmbDestinatario.SelectedIndex = -1
        cmbDestinatario.Focus()

    End Sub

    Private Sub CargarDatos()

        Try

            chkActivo.Enabled = False

            If pIndex > -1 AndAlso pListObjPR IsNot Nothing AndAlso pListObjPR.Count > 0 Then

                cmbDestinatario.SelectedValue = pListObjPR(pIndex).IdDestinatarioPropietario
                chkActivo.Checked = pListObjPR(pIndex).Activo
                If pListObjPR(pIndex).Activo = False Then
                    chkActivo.Enabled = True
                End If

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

    Private Sub frmAgregaDestinatario_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Limpiar()
        IMS.Listar_DestinatarioByPropietario(cmbDestinatario, pIdPropietario)
        CargarDatos()

    End Sub

    Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click

        Try
            cmdAceptar.Enabled = False

            Call Me.Valida()
            Me.Cursor = Cursors.WaitCursor

            If pIndex > -1 Then

                Dim find As Integer = -1
                If pListObjPR IsNot Nothing Then

                    pListObjPR(pIndex).Activo = chkActivo.Checked
                    pListObjPR(pIndex).User_mod = AP.UsuarioAp.IdUsuario
                    pListObjPR(pIndex).Fec_mod = Now

                    If pIdReglaPropietarioEnc > 0 Then
                        find = pListObjPR.FindIndex(Function(b) b.IdReglaPropietarioEnc = pIdReglaPropietarioEnc And b.IdReglaPropietarioDet = pIdReglaPropietarioDet And b.IdDestinatarioPropietario = cmbDestinatario.SelectedValue)
                    Else
                        find = pListObjPR.FindIndex(Function(b) b.IdReglaPropietarioDet = pIdReglaPropietarioDet And b.IdDestinatarioPropietario = cmbDestinatario.SelectedValue)
                    End If

                    If find > -1 Then
                        Throw New Exception(String.Format("El Destinatario {0} ya fue ingresado.", cmbDestinatario.Text))
                    End If

                    pListObjPR(pIndex).IdDestinatarioPropietario = cmbDestinatario.SelectedValue
                    pListObjPR(pIndex).NombreDestinatario = cmbDestinatario.Text

                End If

            Else

                If pListObjPR IsNot Nothing AndAlso pListObjPR.Count > 0 Then

                    Dim find As Integer = -1
                    find = pListObjPR.FindIndex(Function(b) b.IdDestinatarioPropietario = cmbDestinatario.SelectedValue)
                    If find > -1 Then
                        Throw New Exception(String.Format("El Destinatario {0} ya fue ingresado.", cmbDestinatario.Text.Trim))
                    End If

                End If

                Dim ObjF As New clsBePropietario_reglas_det
                ObjF.IdDestinatarioPropietario = cmbDestinatario.SelectedValue
                ObjF.NombreDestinatario = cmbDestinatario.Text
                ObjF.User_agr = AP.UsuarioAp.IdUsuario
                ObjF.Fec_agr = Now
                ObjF.User_mod = AP.UsuarioAp.IdUsuario
                ObjF.Fec_mod = Now
                ObjF.Activo = True
                ObjF.IsNew = True

                pListObjPR.Add(ObjF)

            End If

            Limpiar()
            cmdAceptar.Enabled = True

        Catch ex As Exception
            cmdAceptar.Enabled = True
            Me.Cursor = Cursors.Default
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
            Cargar.Invoke()
        End Try

    End Sub

    Private Sub cmdNuevo_Click(sender As Object, e As EventArgs) Handles cmdNuevo.Click

        Try

            Dim Dest As New frmPropietarioDestinatario(frmPropietarioDestinatario.TipoTrans.Nuevo)
            Dest.pIdPropietario = pIdPropietario
            Dest.pNombrePropietario = pNombrePropietario
            Dest.ShowDialog()

            IMS.Listar_DestinatarioByPropietario(cmbDestinatario, pIdPropietario)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub
End Class