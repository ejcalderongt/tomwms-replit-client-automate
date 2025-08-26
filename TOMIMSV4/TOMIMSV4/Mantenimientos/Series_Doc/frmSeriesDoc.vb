Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmSeriesDoc

    Private pListObjT As New List(Of clsTabla)

    Public Delegate Sub Listar_Series()
    Public Property Listar As Listar_Series
    Public BeTransSeriesDoc As New clsBeTrans_series_doc

    Private DT As New DataTable("Tipo_doc")
    Private DTT As New DataTable("TipoTrans")

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

    Private Sub frmSeriesDoc_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            AP.Listar_Bodegas_By_Usuario(cmbBodega)
            'AP.Listar_Bodegas_By_Usuario(cmbBodega)

            FillComboTipoDoc()

            Select Case Modo

                Case TipoTrans.Nuevo

                    cmbBodega.EditValue = AP.IdBodega

                    lbl.Text = clsLnTrans_series_doc.MaxID + 1
                    User_agrTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = String.Format("{0} {1}", AP.UsuarioAp.Nombres, AP.UsuarioAp.Apellidos)
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False
                    mnuAsignacion.Enabled = False


                Case TipoTrans.Editar

                    lbl.Text = BeTransSeriesDoc.IdTransSerieDoc
                    cmbBodega.EditValue = BeTransSeriesDoc.IdBodega
                    cmbTipoTransaccion.EditValue = BeTransSeriesDoc.IdTipoTrans
                    cmbTipoDocumento.EditValue = BeTransSeriesDoc.Tipo_Doc
                    txtCorrelativoInicial.Value = BeTransSeriesDoc.Inicial
                    txtCorrelativoFinal.Value = BeTransSeriesDoc.Final
                    txtCorrelativoActual.Value = BeTransSeriesDoc.Actual
                    txtSerie.Text = BeTransSeriesDoc.Serie
                    chkActivo.Checked = BeTransSeriesDoc.Activo

                    User_agrTextEdit.Text = BeTransSeriesDoc.UserAgr
                    Fec_agrDateEdit.Text = BeTransSeriesDoc.FecAgr
                    User_modTextEdit.Text = BeTransSeriesDoc.UserMod
                    Fec_modDateEdit.Text = BeTransSeriesDoc.FecMod

                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = OpcionesMenu.Modificar
                    mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    mnuAsignacion.Enabled = OpcionesMenu.Modificar

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub FillComboTipoDoc()

        DT.Columns.Add("Id", GetType(Integer))
        DT.Columns.Add("Tipo", GetType(String))

        Dim Dr As DataRow

        Dr = DT.NewRow
        Dr("Id") = 1
        Dr("Tipo") = "Ingreso"
        DT.Rows.Add(Dr)

        Dr = DT.NewRow
        Dr("Id") = 2
        Dr("Tipo") = "Salida"
        DT.Rows.Add(Dr)

        Dr = DT.NewRow
        Dr("Id") = 3
        Dr("Tipo") = "Ajuste"
        DT.Rows.Add(Dr)

        cmbTipoDocumento.Properties.DataSource = DT
        cmbTipoDocumento.Properties.DisplayMember = "Tipo"
        cmbTipoDocumento.Properties.ValueMember = "Tipo"

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            BeTransSeriesDoc = New clsBeTrans_series_doc()
            BeTransSeriesDoc.IdTransSerieDoc = clsLnTrans_series_doc.MaxID() + 1
            BeTransSeriesDoc.Serie = txtSerie.EditValue
            BeTransSeriesDoc.Tipo_Doc = cmbTipoDocumento.EditValue
            BeTransSeriesDoc.IdTipoTrans = cmbTipoTransaccion.EditValue
            BeTransSeriesDoc.Inicial = txtCorrelativoInicial.Value
            BeTransSeriesDoc.Final = txtCorrelativoFinal.Value
            BeTransSeriesDoc.Actual = txtCorrelativoActual.Value
            BeTransSeriesDoc.IdBodega = cmbBodega.EditValue
            BeTransSeriesDoc.Activo = chkActivo.Checked
            BeTransSeriesDoc.UserAgr = AP.UsuarioAp.IdUsuario
            BeTransSeriesDoc.FecAgr = Now
            BeTransSeriesDoc.UserMod = AP.UsuarioAp.IdUsuario
            BeTransSeriesDoc.FecMod = Now

            Guardar = clsLnTrans_series_doc.Insertar(BeTransSeriesDoc) > 0

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Function

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                BeTransSeriesDoc.Serie = txtSerie.EditValue
                BeTransSeriesDoc.Tipo_Doc = cmbTipoDocumento.EditValue
                BeTransSeriesDoc.IdTransSerieDoc = cmbTipoTransaccion.EditValue
                BeTransSeriesDoc.Inicial = txtCorrelativoInicial.Value
                BeTransSeriesDoc.Final = txtCorrelativoFinal.Value
                BeTransSeriesDoc.Actual = txtCorrelativoActual.Value
                BeTransSeriesDoc.Activo = chkActivo.Checked
                BeTransSeriesDoc.IdBodega = cmbBodega.EditValue
                BeTransSeriesDoc.UserMod = AP.UsuarioAp.IdUsuario
                BeTransSeriesDoc.FecMod = Now
                Actualizar = clsLnTrans_series_doc.Actualizar(BeTransSeriesDoc) > 0

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

    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If clsLnTrans_series_doc.ExistDocument(BeTransSeriesDoc) Then
                XtraMessageBox.Show("Ya existe un documento con los datos ingresados.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Datos_Correctos = True
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

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick
        If Datos_Correctos() Then
            If XtraMessageBox.Show("¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If
                    Close()
                End If
            End If
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Try
            If Actualizar() Then
                XtraMessageBox.Show("Se actualizó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Listar IsNot Nothing Then
                    Listar.Invoke()
                End If
                Close()
            End If
        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If BeTransSeriesDoc.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If XtraMessageBox.Show("¿Eliminar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If clsLnTrans_series_doc.Eliminar(BeTransSeriesDoc) > 0 Then
                        XtraMessageBox.Show("Se eliminó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If Listar IsNot Nothing Then
                            Listar.Invoke()
                        End If
                        Close()
                    End If
                End If
            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then TablasRelacionadas("trans_series_doc", BeTransSeriesDoc.IdTransSerieDoc)
        End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmSeriesDoc_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub cmbTipoDocumento_EditValueChanged(sender As Object, e As EventArgs) Handles cmbTipoDocumento.EditValueChanged

        Try

            If cmbTipoDocumento.EditValue = "Ingreso" Then

                IMS.Lista_Pedido_Ingreso(cmbTipoTransaccion)

            ElseIf cmbTipoDocumento.EditValue = "Salida" Then

                IMS.Lista_Pedido_Salida(cmbTipoTransaccion)

            ElseIf cmbTipoDocumento.EditValue = "Ajuste" Then

                IMS.Listar_Tipo_Ajuste(cmbTipoTransaccion)

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

End Class