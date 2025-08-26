Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class frmFont_Tramo

    Public Delegate Sub Listar()
    Public Property InvokeListarFont As Listar

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

    Public pObjBeFE As New clsBeFont_Enc

    Private Sub cmdSeleccionarFont_Click(sender As Object, e As EventArgs) Handles cmdSeleccionarFont.Click
        MostrarFont()
    End Sub

    Private Sub MostrarFont()

        'FontDialog1.Color = Color.FromArgb(txtColor.Text)

        If FontDialog1.ShowDialog <> DialogResult.Cancel Then
            txtFont.Text = FontDialog1.Font.Name.ToString
            txtSize.Text = FontDialog1.Font.Size.ToString
            txtColor.Text = FontDialog1.Color.ToArgb.ToString
            txtColor.BackColor = Color.FromArgb(txtColor.Text)
            chkNegrita.Checked = FontDialog1.Font.Bold
            chkCursiva.Checked = FontDialog1.Font.Italic
            chkSubrayado.Checked = FontDialog1.Font.Underline
        End If
    End Sub

    Public Sub AplicarFont()

        Dim vLetra As String
        Dim vTamaño As Integer
        Dim vNegrita As Boolean
        Dim vCursiva As Boolean
        Dim vSubrayado As Boolean
        Dim vColorFont As String
        Dim vColorFondo As String
        Dim vFontStyle As FontStyle
        Dim vFont As Font
        'Dim Color As Color

        Try
            Const sp As String = "Select * from Font_det"
            Dim cnn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("CST"))
            Dim cmd As New SqlCommand(sp, cnn) With {.CommandType = CommandType.Text}
            Dim dad As New SqlDataAdapter(cmd)

            Dim dt As New DataTable

            dad.Fill(dt)

            For vI = 0 To dt.Rows.Count - 1

                vLetra = dt.Rows(vI).Item("Letra")
                vTamaño = dt.Rows(vI).Item("Tamaño")
                vNegrita = dt.Rows(vI).Item("Negrita")
                vCursiva = dt.Rows(vI).Item("Cursiva")
                vSubrayado = dt.Rows(vI).Item("Subrayado")
                vColorFont = dt.Rows(vI).Item("ColorFont")
                vColorFondo = dt.Rows(vI).Item("ColorFondo")

                If vNegrita Then vFontStyle = FontStyle.Bold Else vFontStyle = FontStyle.Regular
                If vCursiva Then vFontStyle = vFontStyle Or FontStyle.Italic
                If vSubrayado Then vFontStyle = vFontStyle Or FontStyle.Underline

                vFont = New Font(vLetra, vTamaño, vNegrita, vCursiva, vSubrayado, GraphicsUnit.Point)

            Next


        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub SeleccionaColor()

        'ColorDialog1.Color = Color.FromArgb(txtFondo.Text)

        If ColorDialog1.ShowDialog <> DialogResult.Cancel Then
            txtFondo.Text = ColorDialog1.Color.ToArgb.ToString
            txtFondo.BackColor = Color.FromArgb(txtFondo.Text)
        End If

    End Sub

    Private Sub cmdSeleccionarColor_Click(sender As Object, e As EventArgs) Handles cmdSeleccionarColor.Click
        SeleccionaColor()
    End Sub

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        If Guardar(True) Then
            XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarFont IsNot Nothing Then
                InvokeListarFont.Invoke()
            End If
            Close()
        End If

    End Sub

    Private Function Guardar(ByVal IsNew As Boolean) As Boolean

        Guardar = False

        Try

            If IsNew Then
                pObjBeFE = New clsBeFont_enc()
                pObjBeFE.lDet = New List(Of clsBeFont_det)
                Dim Obj As New clsBeFont_det
                pObjBeFE.lDet.Add(Obj)
            End If

            pObjBeFE.IsNew = IsNew
            pObjBeFE.Nombre = txtDescripcion.Text.Trim()
            pObjBeFE.lDet(0).IdFontEnc = pObjBeFE.IdFontEnc
            pObjBeFE.lDet(0).Letra = txtFont.Text.Trim()
            pObjBeFE.lDet(0).Tamaño = txtSize.Text.Trim()
            pObjBeFE.lDet(0).Negrita = chkNegrita.Checked
            pObjBeFE.lDet(0).Cursiva = chkCursiva.Checked
            pObjBeFE.lDet(0).Subrayado = chkSubrayado.Checked
            pObjBeFE.lDet(0).ColorFont = txtColor.Text.Trim()
            pObjBeFE.lDet(0).ColorFondo = txtFondo.Text.Trim()
            pObjBeFE.lDet(0).IsNew = IsNew

            Guardar = clsLnFont_enc.Guardar_Font(pObjBeFE)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    'Private Function Actualizar() As Boolean

    '    Actualizar = False

    '    Try

    '        Dim ObjE As New clsBeFont_enc
    '        Dim Obj As New clsBeFont_det

    '        pObjBeFE.Nombre = txtDescripcion.Text.Trim()
    '        Actualizar = clsLnFont_enc.Actualizar(pObjBeFE) > 0

    '        pObjBeFE.lDet(0).Letra = txtFont.Text.Trim()
    '        pObjBeFE.lDet(0).Tamaño = txtSize.Text.Trim()
    '        pObjBeFE.lDet(0).Negrita = chkNegrita.Checked
    '        pObjBeFE.lDet(0).Cursiva = chkCursiva.Checked
    '        pObjBeFE.lDet(0).Subrayado = chkSubrayado.Checked
    '        pObjBeFE.lDet(0).ColorFont = txtColor.Text.Trim()
    '        pObjBeFE.lDet(0).ColorFondo = txtFondo.Text.Trim()
    '        Actualizar = clsLnFont_det.Actualizar(pObjBeFE.lDet(0)) > 0

    '    Catch ex As Exception
    '        XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try

    'End Function

    Private Sub frmFont_Tramo_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Top = 50
        'Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2

        Try

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCod.Text = clsLnFont_enc.MaxID()
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

                    'clsLnFont_enc.Obtener(pObjBeFE)
                    'pObjFD.Obtener(pObjBeFD)
                    Buscar_Registro()

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Buscar_Registro()

        Try

            lblCod.Text = pObjBeFE.IdFontEnc
            txtDescripcion.Text = pObjBeFE.Nombre

            txtFont.Text = pObjBeFE.lDet(0).Letra
            txtSize.Text = pObjBeFE.lDet(0).Tamaño
            chkNegrita.Checked = pObjBeFE.lDet(0).Negrita
            chkCursiva.Checked = pObjBeFE.lDet(0).Cursiva
            chkSubrayado.Checked = pObjBeFE.lDet(0).Subrayado
            txtColor.Text = pObjBeFE.lDet(0).ColorFont
            txtColor.BackColor = Color.FromArgb(txtColor.Text)
            txtFondo.Text = pObjBeFE.lDet(0).ColorFondo
            txtFondo.BackColor = Color.FromArgb(txtFondo.Text)

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

        If Guardar(False) Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarFont IsNot Nothing Then
                InvokeListarFont.Invoke()
            End If
            Close()
        End If

    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

    End Sub

    Private Sub frmFont_Tramo_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class