Imports DevExpress.XtraEditors

Public Class frmTipoTarima

    Private pListObjT As New List(Of clsTabla)
    Private ReadOnly pObjLNJ As New clsLnTipo_tarima
    Public pObjBEJ As New clsBeTipo_tarima
    Public Delegate Sub Listar_TipoTarima()
    Public Property Listar As Listar_TipoTarima

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

    Private Sub frmTipoTarima_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("tipo_tarima")

            Select Case Modo
                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnTipo_tarima.MaxID
                    User_agrTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_agrDateEdit.Text = Now
                    User_modTextEdit.Text = AP.UsuarioAp.IdUsuario
                    Fec_modDateEdit.Text = Now

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    pObjLNJ.Obtener(pObjBEJ)

                    lblCodigo.Text = pObjBEJ.IdTipoTarima
                    txtNombre.Text = pObjBEJ.Nombre
                    txtAlto.Value = pObjBEJ.Alto
                    txtLargo.Value = pObjBEJ.Largo
                    txtAncho.Value = pObjBEJ.Ancho
                    txtCargaDinamica.Value = pObjBEJ.CargaDinamica
                    txtCargaEstatica.Value = pObjBEJ.CargaEstatica
                    txtCargaEstanteria.Value = pObjBEJ.CargaEstanterias
                    txtEntradasPaleta.Value = pObjBEJ.EntradasTransPaleta
                    txtPesoPromedio.Value = pObjBEJ.PesoPromedio
                    chkActivo.Checked = pObjBEJ.Activo

                    User_agrTextEdit.Text = pObjBEJ.User_agr
                    Fec_agrDateEdit.Text = pObjBEJ.Fec_agr
                    User_modTextEdit.Text = pObjBEJ.User_mod
                    Fec_modDateEdit.Text = pObjBEJ.Fec_mod

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

        Focus()
        txtNombre.Focus()

    End Sub

    Private Function Datos_Correctos()

        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
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

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            pObjBEJ = New clsBeTipo_tarima()
            pObjBEJ.IdTipoTarima = clsLnTipo_tarima.MaxID()
            pObjBEJ.Nombre = txtNombre.Text.Trim()
            pObjBEJ.Alto = txtAlto.Value
            pObjBEJ.Largo = txtLargo.Value
            pObjBEJ.Ancho = txtAncho.Value
            pObjBEJ.CargaDinamica = txtCargaDinamica.Value
            pObjBEJ.CargaEstatica = txtCargaEstatica.Value
            pObjBEJ.CargaEstanterias = txtCargaEstanteria.Value
            pObjBEJ.EntradasTransPaleta = txtEntradasPaleta.Value
            pObjBEJ.PesoPromedio = txtPesoPromedio.Value
            pObjBEJ.User_agr = AP.UsuarioAp.IdUsuario
            pObjBEJ.Fec_agr = Now
            pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
            pObjBEJ.Fec_mod = Now
            pObjBEJ.Activo = True

            Guardar = pObjLNJ.Insertar(pObjBEJ) > 0

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

                pObjBEJ.Nombre = txtNombre.Text.Trim()
                pObjBEJ.Alto = txtAlto.Value
                pObjBEJ.Largo = txtLargo.Value
                pObjBEJ.Ancho = txtAncho.Value
                pObjBEJ.CargaDinamica = txtCargaDinamica.Value
                pObjBEJ.CargaEstatica = txtCargaEstatica.Value
                pObjBEJ.CargaEstanterias = txtCargaEstanteria.Value
                pObjBEJ.EntradasTransPaleta = txtEntradasPaleta.Value
                pObjBEJ.PesoPromedio = txtPesoPromedio.Value
                pObjBEJ.Activo = chkActivo.Checked
                pObjBEJ.User_mod = AP.UsuarioAp.IdUsuario
                pObjBEJ.Fec_mod = Now
                Actualizar = pObjLNJ.Actualizar(pObjBEJ) > 0

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
        Try
            If Datos_Correctos() Then
                If XtraMessageBox.Show("¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Guardar() Then
                        XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If Listar IsNot Nothing Then
                            Listar.Invoke()
                        End If
                        Close()
                    End If
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

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            If Actualizar() Then
                XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Listar IsNot Nothing Then
                    Listar.Invoke()
                End If
                Close()
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

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try

            If XtraMessageBox.Show("¿Desactivar el registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                pObjBEJ.Activo = False
                If pObjLNJ.Actualizar(pObjBEJ) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If
                    Close()
                End If
            End If

        Catch ex As Exception
            If ex.HResult = -2146233088 Then TablasRelacionadas("tipo_tarima", pObjBEJ.IdTipoTarima)
        End Try

    End Sub

    Private Sub frmTipoTarima_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class