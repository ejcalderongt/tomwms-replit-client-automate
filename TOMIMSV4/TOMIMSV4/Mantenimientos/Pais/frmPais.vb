Imports DevExpress.XtraEditors

Public Class frmPais

    Private pListObj As New List(Of clsTabla)
    Private lnPais As New clsLnPaises
    Public BePais As New clsBePaises
    Public Delegate Sub listar_Paises()
    Public Property InvokeListarPais As listar_Paises

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

    Private Sub frmPais_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObj = clsBD.GetLongitudByTabla("paises")

            Select Case Modo
                Case TipoTrans.Nuevo
                    lblCodigo.Text = clsLnPaises.MaxID

                    If OpcionesMenu IsNot Nothing Then
                        mnuGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    clsLnPaises.Obtener(BePais)
                    txtISON.Text = BePais.ISONUM
                    txtISO2.Text = BePais.ISO2
                    txtISO3.Text = BePais.ISO3
                    txtNombre.Text = BePais.NOMBRE
                    chkActivo.Checked = BePais.Activo

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

            Application.DoEvents()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        txtISON.Focus()

    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        If Datos_Correctos() Then

            If MessageBox.Show("¿Guardar País?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                If InvokeListarPais IsNot Nothing Then
                    InvokeListarPais.Invoke()
                End If
                Close()
            End If

        End If

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            BePais.IdPais = clsLnPaises.MaxID()
            BePais.ISONUM = txtISON.Text
            BePais.ISO2 = txtISO2.Text.Trim()
            BePais.ISO3 = txtISO3.Text.Trim()
            BePais.NOMBRE = txtNombre.Text.Trim()
            BePais.Activo = True

            Guardar = clsLnPaises.Insertar(BePais) > 0

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

            If String.IsNullOrEmpty(txtISON.Text) Then
                XtraMessageBox.Show("Ingrese Número ISO", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtISON.Focus()

            ElseIf txtISO2.Text.Count > pListObj.Find(Function(b) b.NombreCampo.ToUpper = "ISO2").Longitud Then
                XtraMessageBox.Show(String.Format("El ISO 2 debe de tener como máximo {0} carácteres.", pListObj.Find(Function(ByVal b) b.NombreCampo.ToUpper = "ISO2").Longitud),
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtISO2.Focus()
            ElseIf txtISO3.Text.Count > pListObj.Find(Function(b) b.NombreCampo.ToUpper = "ISO3").Longitud Then
                XtraMessageBox.Show(String.Format("El ISO 3 debe de tener como máximo {0} carácteres.", pListObj.Find(Function(ByVal b) b.NombreCampo.ToUpper = "ISO3").Longitud),
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtISO2.Focus()
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre de Pais", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListObj.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObj.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud),
                                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If (Datos_Correctos()) Then

                BePais.ISONUM = txtISON.Text
                BePais.ISO2 = txtISO2.Text.Trim()
                BePais.ISO3 = txtISO3.Text
                BePais.NOMBRE = txtNombre.Text.Trim()
                BePais.Activo = chkActivo.Checked

                Actualizar = clsLnPaises.Actualizar(BePais) > 0

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

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarPais IsNot Nothing Then
                InvokeListarPais.Invoke()
            End If
            Close()
        End If

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        If BePais.Activo = False Then
            XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            If MessageBox.Show("¿Desactivar País?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If (Datos_Correctos()) Then
                    BePais.Activo = False
                    If Desactivar() Then
                        If InvokeListarPais IsNot Nothing Then
                            InvokeListarPais.Invoke()
                        End If
                        Close()
                        frmPais_List.Dgrid.Refresh()
                    End If
                End If
            End If
        End If

    End Sub

    Private Function Desactivar() As Boolean
        Desactivar = False

        Try

            If (Datos_Correctos()) Then

                BePais.Activo = False
                Desactivar = clsLnPaises.Actualizar(BePais) > 0
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

    Private Sub frmPais_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class