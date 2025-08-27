Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmRegimenFiscal

    Private pListObjT As New List(Of clsTabla)
    Public gBeRegimenFiscal As New clsBeRegimen_fiscal

    Public Delegate Sub Listar_RegimenFiscales()
    Public Property Listar As Listar_RegimenFiscales

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

    Private Sub frmRegimenFiscal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("RegimenFiscal")

            Select Case Modo

                Case TipoTrans.Nuevo

                    lbl.Text = clsLnRegimen_fiscal.MaxID()
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
                    txtCodigoRegimen.Focus()


                Case TipoTrans.Editar

                    lbl.Text = gBeRegimenFiscal.IdRegimen
                    txtNombre.Text = gBeRegimenFiscal.Descripcion.Trim()
                    txtPorcentaje.Value = gBeRegimenFiscal.Dias_vencimiento
                    chkActivo.Checked = gBeRegimenFiscal.Activo
                    '#GT17112022_0800: se agrega el código del regimen
                    txtCodigoRegimen.Text = gBeRegimenFiscal.Codigo_regimen
                    txtCodigoRegimen.Enabled = False

                    User_agrTextEdit.Text = gBeRegimenFiscal.User_agr
                    Fec_agrDateEdit.Text = gBeRegimenFiscal.Fec_agr
                    User_modTextEdit.Text = gBeRegimenFiscal.User_mod
                    Fec_modDateEdit.Text = gBeRegimenFiscal.Fec_mod

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

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            gBeRegimenFiscal = New clsBeRegimen_fiscal() With {.IdRegimen = clsLnRegimen_fiscal.MaxID(),
            .Codigo_regimen = txtCodigoRegimen.Text,
            .Descripcion = txtNombre.Text.Trim(),
            .Dias_vencimiento = txtPorcentaje.Value,
            .Activo = True,
            .User_agr = AP.UsuarioAp.IdUsuario,
            .Fec_agr = Now,
            .User_mod = AP.UsuarioAp.IdUsuario,
            .Fec_mod = Now}

            Guardar = clsLnRegimen_fiscal.Insertar(gBeRegimenFiscal) > 0

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

                gBeRegimenFiscal.Codigo_regimen = txtCodigoRegimen.Text.Trim()
                gBeRegimenFiscal.Descripcion = txtNombre.Text.Trim()
                gBeRegimenFiscal.Dias_vencimiento = txtPorcentaje.Value
                gBeRegimenFiscal.Activo = chkActivo.Checked
                gBeRegimenFiscal.User_mod = AP.UsuarioAp.IdUsuario
                gBeRegimenFiscal.Fec_mod = Now
                Actualizar = clsLnRegimen_fiscal.Actualizar(gBeRegimenFiscal) > 0

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
            '#GT17112022_0800: se agrega el código del régimen
            If String.IsNullOrEmpty(txtCodigoRegimen.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Código del régimen.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigoRegimen.Focus()
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf Not Longitud_Maxima_Nombre_Correcta() Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
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

    Private Function Longitud_Maxima_Nombre_Correcta() As Boolean

        Longitud_Maxima_Nombre_Correcta = True

        If Not pListObjT Is Nothing Then
            If pListObjT.Count > 0 Then
                If Not txtNombre.Text.Length > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                    Longitud_Maxima_Nombre_Correcta = False
                End If
            End If
        End If

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

            If gBeRegimenFiscal.Activo = False Then
                XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If XtraMessageBox.Show("¿Eliminar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If clsLnRegimen_fiscal.Eliminar(gBeRegimenFiscal) > 0 Then
                        XtraMessageBox.Show("Se eliminó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If Listar IsNot Nothing Then
                            Listar.Invoke()
                        End If
                        Close()
                    End If
                End If
            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then TablasRelacionadas("RegimenFiscal", gBeRegimenFiscal.IdRegimen)
        End Try

    End Sub

    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub frmRegimenFiscal_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

End Class