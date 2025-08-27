Imports DevExpress.XtraEditors

Public Class frmProducto_Color

    Private pListColor As New List(Of clsTabla)
    Public Delegate Sub Listar()
    Public Property InvokeListarColores As Listar
    Public objColor As New clsBeColor

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

    Private Sub frmProducto_Color_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            pListColor = clsBD.GetLongitudByTabla("color")

            IMS.Listar_Propietarios_By_IdEmpresa(cmbPropietario, AP.IdEmpresa)

            Select Case Modo

                Case TipoTrans.Nuevo

                    objColor = New clsBeColor
                    lblIdColor.Text = clsLnColor.MaxID() + 1
                    If OpcionesMenu IsNot Nothing Then
                        cmdGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    cmdActualizar.Enabled = False
                    cmdEliminar.Enabled = False
                    cmbPropietario.Enabled = True

                Case TipoTrans.Editar

                    objColor.IsNew = False
                    lblIdColor.Text = objColor.IdColor
                    cmbPropietario.EditValue = objColor.Propietario.IdPropietario
                    cmbPropietario.Enabled = False
                    txtNombre.Text = objColor.Nombre

                    '#GT19022025: al obtener el color hexa, asignarlo a la paleta y luego al campo descripcion.
                    If Not String.IsNullOrEmpty(objColor.CodigoHex) Then
                        PaletColor.Color = ColorTranslator.FromHtml(objColor.CodigoHex)
                        Dim colorSeleccionado As Color = PaletColor.Color
                        txtDescripcion.Text = ColorTranslator.ToHtml(colorSeleccionado)
                    End If

                    chkActivo.Checked = objColor.Activo
                    cmdGuardar.Enabled = False
                    cmdActualizar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    cmdEliminar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Eliminar, True)

            End Select



        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        cmdGuardar.Enabled = False
        Guardar_Registro()
        cmdGuardar.Enabled = True
    End Sub

    Private Sub Guardar_Registro()
        Try

            If Datos_Correctos() Then

                If MessageBox.Show("¿Guardar la Talla?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListarColores IsNot Nothing Then
                        InvokeListarColores.Invoke()
                    End If
                    Close()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try

            If cmbPropietario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Propietario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbPropietario.Focus()
            ElseIf String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            ElseIf txtNombre.Text.Count > pListColor.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                DevExpress.XtraEditors.XtraMessageBox.Show("El Nombre debe de tener como máximo " & pListColor.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombre.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Function Guardar() As Boolean

        Guardar = False

        Try
            objColor = New clsBeColor

            objColor.IsNew = True
            objColor.IdColor = clsLnColor.MaxID() + 1
            objColor.Nombre = txtNombre.Text
            objColor.CodigoHex = txtDescripcion.Text
            objColor.Propietario = New clsBePropietarios
            objColor.Propietario.IdPropietario = cmbPropietario.EditValue
            objColor.IdPropietario = cmbPropietario.EditValue
            objColor.Fec_agr = Now
            objColor.User_agr = AP.UsuarioAp.IdUsuario
            objColor.Fec_mod = Now
            objColor.User_mod = AP.UsuarioAp.IdUsuario
            objColor.Activo = True

            Guardar = clsLnColor.Insertar(objColor) > 0

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Function


    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                objColor.Nombre = txtNombre.Text
                objColor.CodigoHex = txtDescripcion.Text
                objColor.Activo = chkActivo.Checked
                objColor.Fec_mod = Now
                objColor.User_mod = AP.UsuarioAp.IdUsuario
                '#GT18022025: user_agr, fec_agr no se deben alterar porque es un update
                Actualizar = clsLnColor.Actualizar(objColor) > 0

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        cmdActualizar.Enabled = False

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarColores IsNot Nothing Then
                InvokeListarColores.Invoke()
            End If
            Close()
        End If

        cmdActualizar.Enabled = True
    End Sub

    Private Sub PaletColor_EditValueChanged(sender As Object, e As EventArgs) Handles PaletColor.EditValueChanged
        Dim colorSeleccionado As Color = PaletColor.Color
        ' Convertir a hexadecimal
        txtDescripcion.Text = ColorTranslator.ToHtml(colorSeleccionado)
    End Sub

    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminar.ItemClick
        cmdEliminar.Enabled = False
        'Eliminar()
        cmdEliminar.Enabled = True
    End Sub



End Class