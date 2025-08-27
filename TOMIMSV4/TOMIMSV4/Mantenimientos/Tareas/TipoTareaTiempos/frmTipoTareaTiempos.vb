Imports DevExpress.XtraEditors

Public Class frmTipoTareaTiempos

    Public Delegate Sub Listar_TiemposTareas()
    Public Property Listar As Listar_TiemposTareas
    Public beTiemposTarea As New clsBeTipo_tarea_tiempos

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            beTiemposTarea.IdEmpresa = cmbEmpresa.EditValue
            beTiemposTarea.IdBodega = cmbBodega.EditValue
            beTiemposTarea.IdTipoTarea = cmbTarea.EditValue
            beTiemposTarea.TiempoMedioMinutos = txtTiempo.Value

            If Modo = TipoTrans.Editar Then
                Guardar = clsLnTipo_tarea_tiempos.Actualizar(beTiemposTarea)
            Else
                Guardar = clsLnTipo_tarea_tiempos.Insertar(beTiemposTarea)
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Sub frmTipoTareaTiempos_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            IMS.Listar_Empresas(cmbEmpresa)

            IMS.Listar_Bodegas_Por_Empresa(cmbBodega, cmbEmpresa.EditValue)

            IMS.Listar_TipoTarea(cmbTarea)

            cmdActualizar.Enabled = False
            cmdEliminar.Enabled = False

            If Modo = TipoTrans.Editar Then

                cmdActualizar.Enabled = True
                cmdEliminar.Enabled = True
                cmdGuardar.Enabled = False

                If Not beTiemposTarea Is Nothing Then

                    cmbEmpresa.EditValue = beTiemposTarea.IdEmpresa
                    cmbBodega.EditValue = beTiemposTarea.IdBodega
                    cmbTarea.EditValue = beTiemposTarea.IdTipoTarea
                    txtTiempo.Value = beTiemposTarea.TiempoMedioMinutos

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

    Private Sub cmbEmpresa_EditValueChanged(sender As Object, e As EventArgs) Handles cmbEmpresa.EditValueChanged
        IMS.Listar_Bodegas_Por_Empresa(cmbBodega, cmbEmpresa.EditValue)
    End Sub

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        If Guardar() Then
            XtraMessageBox.Show("Datos guardaros correctamente ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If Listar IsNot Nothing Then
                Listar.Invoke()
            End If
            Close()
        End If
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        If Guardar() Then
            XtraMessageBox.Show("Datos actualizados correctamente ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If Listar IsNot Nothing Then
                Listar.Invoke()
            End If
            Close()
        End If
    End Sub

    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminar.ItemClick
        If XtraMessageBox.Show("¿Está seguro de eliminar este registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            clsLnTipo_tarea_tiempos.Eliminar(beTiemposTarea)
            If Listar IsNot Nothing Then
                Listar.Invoke()
            End If
            Close()
        End If
    End Sub
End Class