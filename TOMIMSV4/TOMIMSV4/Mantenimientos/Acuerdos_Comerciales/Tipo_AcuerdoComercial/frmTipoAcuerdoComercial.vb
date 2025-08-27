Imports DevExpress.XtraEditors

Public Class frmTipoAcuerdoComercial

    Public BeAcuerdoComercialDet As New clsBeTrans_acuerdoscomerciales_det
    Public Delegate Sub Listar_AcuerdosComerciales()
    Public Property InvokeListarAcuerdosComerciales As Listar_AcuerdosComerciales
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Private Sub frmTipoAcuerdoComercial_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            AP.Listar_Bodegas_By_Usuario(cmbBodega)
            Listar_Tipo_Cobros()
            cmbBodega.EditValue = -1
            cmbTipoCobro.EditValue = -1

            txtCorrelativoAcuerdo.EditValue = BeAcuerdoComercialDet.Correlativo_detalleacuerdo
            txtServicio.EditValue = BeAcuerdoComercialDet.Servicio
            txtDescripcion.EditValue = BeAcuerdoComercialDet.Descripcion
            txtCorrelativoAcuerdo.Enabled = False
            txtServicio.Enabled = False
            txtDescripcion.Enabled = False

            If BeAcuerdoComercialDet.Idbodega > 0 Then
                cmbBodega.EditValue = BeAcuerdoComercialDet.IdBodega
            Else
                cmbBodega.Focus()
            End If

            If BeAcuerdoComercialDet.IdTipoCobro > 0 Then
                cmbTipoCobro.EditValue = BeAcuerdoComercialDet.IdTipoCobro
            Else
                cmbTipoCobro.Focus()
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    Private Sub Listar_Tipo_Cobros()
        Try
            cmbTipoCobro.Properties.DataSource = CrearTablaTipoCobro()
            cmbTipoCobro.Properties.ValueMember = "Id"
            cmbTipoCobro.Properties.DisplayMember = "Descripcion"
            cmbTipoCobro.Properties.NullText = ""

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Function CrearTablaTipoCobro() As DataTable

        Dim dt As New DataTable
        dt.Columns.Add("Id", GetType(Integer))
        dt.Columns.Add("Descripcion", GetType(String))

        dt.Rows.Add(1, "Por Unidad")
        dt.Rows.Add(2, "Por Valor Mercaderia")

        Return dt

    End Function

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        Try

            Grabar()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub Grabar()
        Try

            If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            If InvokeListarAcuerdosComerciales IsNot Nothing Then
                InvokeListarAcuerdosComerciales.Invoke()
            End If

            Close()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            If cmbBodega.EditValue > 0 OrElse cmbTipoCobro.EditValue > 0 Then

                BeAcuerdoComercialDet.IdBodega = cmbBodega.EditValue
                BeAcuerdoComercialDet.IdTipoCobro = cmbTipoCobro.EditValue
                BeAcuerdoComercialDet.User_mod = AP.UsuarioAp.IdUsuario
                BeAcuerdoComercialDet.Fec_mod = Now

                '#GT27052024: validar antes que el acuerdo no haya sido usado, para no crear inconsistencia
                If clsLnTrans_prefactura_det.Exist_By_IdCorrelativo_And_Codigo_Acuerdo(BeAcuerdoComercialDet) Then
                    Throw New Exception("El acuerdo ya esta asociado a una prefactura, no es posible modificar la configuración actual.")
                Else
                    clsLnTrans_acuerdoscomerciales_det.Actualizar_Bodega_and_TipoCobro(BeAcuerdoComercialDet)
                    Guardar = True
                End If

            Else
                XtraMessageBox.Show("Seleccione una bodega o, un tipo de cobro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Guardar = False
            End If

        Catch ex As Exception
            Guardar = False
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Private Sub cmbTipoCobro_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbTipoCobro.KeyDown
        Try

            If e.KeyCode = Keys.Back Then
                cmbTipoCobro.EditValue = 0
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        End Try
    End Sub
End Class