Imports DevExpress.XtraEditors
Public Class frmUnidad_Medida_Conversion

    Public UMConversion As New clsBeUnidad_medida_conversion
    Public Delegate Sub ListarUMConversion()
    Public Property InvokeListarUMConversion As ListarUMConversion

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick

        If XtraMessageBox.Show("¿Guardar Unidad Medida Conversion?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            If Guardar() Then
                XtraMessageBox.Show("Se guardó el registro correctamente.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                InvokeListarUMConversion.Invoke
                Close()
            Else
                XtraMessageBox.Show("No se guardó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        End If

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        UMConversion.IdConversion = clsLnUnidad_medida_conversion.MaxID()
        UMConversion.IdUnidadMedidaOrigen = cmbOrigen.EditValue
        UMConversion.IdUnidadMedidaDestino = cmbDestino.EditValue
        UMConversion.Factor = txtFactor.Value
        UMConversion.activo = True

        Guardar = clsLnUnidad_medida_conversion.Insertar(UMConversion)

    End Function

    Private Sub frmUnidad_Medida_Conversion_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            IMS.Listar_Unidad_Medida(cmbDestino)
            IMS.Listar_Unidad_Medida(cmbOrigen)


            Select Case Modo

                Case TipoTrans.Nuevo

                    lblID.Text = clsLnUnidad_medida_conversion.MaxID()

                    If OpcionesMenu IsNot Nothing Then
                        cmdActualizar.Enabled = OpcionesMenu.Modificar
                    End If

                    cmdEliminar.Enabled = False

                Case TipoTrans.Editar

                    clsLnUnidad_medida_conversion.Obtener(UMConversion)

                    lblID.Text = UMConversion.IdConversion
                    cmbOrigen.EditValue = UMConversion.IdUnidadMedidaOrigen
                    cmbDestino.EditValue = UMConversion.IdUnidadMedidaDestino
                    txtFactor.Value = UMConversion.Factor

                    cmdGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        cmdEliminar.Enabled = OpcionesMenu.Eliminar
                        cmdActualizar.Enabled = OpcionesMenu.Modificar
                    End If

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Try


    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Datos actualizados correctamente", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InvokeListarUMConversion.Invoke
            Close()
        Else
            XtraMessageBox.Show("No se pudieron actualizar los datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminar.ItemClick

        If XtraMessageBox.Show("¿Desea eliminar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            UMConversion.activo = False
            If clsLnUnidad_medida_conversion.Actualizar(UMConversion) > 0 Then
                XtraMessageBox.Show("Se ha eliminado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                InvokeListarUMConversion.Invoke
                Close()
            End If

        End If

    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        UMConversion.IdUnidadMedidaOrigen = cmbOrigen.EditValue
        UMConversion.IdUnidadMedidaDestino = cmbDestino.EditValue
        UMConversion.Factor = txtFactor.Value
        UMConversion.activo = True

        Actualizar = clsLnUnidad_medida_conversion.Actualizar(UMConversion)


    End Function

    Private Sub frmUnidad_Medida_Conversion_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
End Class