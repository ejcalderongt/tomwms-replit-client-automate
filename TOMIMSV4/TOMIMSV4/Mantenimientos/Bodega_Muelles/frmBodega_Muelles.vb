Imports System.Drawing.Imaging
Imports System.IO
Imports DevExpress.XtraEditors

Public Class frmBodega_Muelles

    Private pListObjT As New List(Of clsTabla)
    Public Muelle As New clsBeBodega_muelles

    Public Delegate Sub Listar_Bodega_Muelles()
    Public Property Listar As Listar_Bodega_Muelles

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

    Private Sub frmBodega_Muelles_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            pListObjT = clsBD.GetLongitudByTabla("bodega_muelles")

            If Not AP.Listar_Bodegas_By_Usuario(cmbBodega) Then
                XtraMessageBox.Show("No hay Bodegas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

            IMS.Listar_Ubicaciones_Muelle_By_IdBodega(cmbUbicacionDefecto, cmbBodega.EditValue)

            Select Case Modo

                Case TipoTrans.Nuevo

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

                    clsLnBodega_muelles.Obtener(Muelle)

                    cmbBodega.EditValue = Muelle.IdBodega
                    Codigo_barraTextEdit.Text = Muelle.Codigo_barra
                    NombreTextEdit.Text = Muelle.Nombre
                    chkActivo.Checked = Muelle.Activo
                    chkEntrada.Checked = Muelle.Entrada
                    chkSalida.Checked = Muelle.Salida

                    User_agrTextEdit.Text = Muelle.User_agr
                    Fec_agrDateEdit.Text = Muelle.Fec_agr
                    User_modTextEdit.Text = Muelle.User_mod
                    Fec_modDateEdit.Text = Muelle.Fec_mod

                    If Muelle.Imagen IsNot Nothing Then
                        picFoto.Image = ByteArrayToImage(Muelle.Imagen)
                    End If

                    mnuGuardar.Enabled = False

                    cmbUbicacionDefecto.EditValue = Muelle.IdUbicacionDefecto

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                    End If

            End Select

            Application.DoEvents()

            Focus()
            NombreTextEdit.Focus()

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

            Muelle.IdMuelle = clsLnBodega_muelles.MaxID()
            Muelle.IdBodega = cmbBodega.EditValue
            Muelle.Codigo_barra = Codigo_barraTextEdit.Text.Trim()
            Muelle.Nombre = NombreTextEdit.Text.Trim()
            Muelle.Activo = chkActivo.Checked
            Muelle.Entrada = chkEntrada.Checked
            Muelle.Salida = chkSalida.Checked
            Muelle.User_agr = AP.UsuarioAp.IdUsuario
            Muelle.Fec_agr = Now
            Muelle.User_mod = AP.UsuarioAp.IdUsuario
            Muelle.Fec_mod = Now
            Muelle.IdUbicacionDefecto = cmbUbicacionDefecto.EditValue

            If picFoto.Image IsNot Nothing Then
                Muelle.Imagen = ImageToByteArray(picFoto.Image)
            End If

            Guardar = clsLnBodega_muelles.Insertar(Muelle) > 0

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

                Muelle.IdBodega = cmbBodega.EditValue
                Muelle.Codigo_barra = Codigo_barraTextEdit.Text.Trim()
                Muelle.Nombre = NombreTextEdit.Text.Trim()
                Muelle.Activo = chkActivo.Checked
                Muelle.Entrada = chkEntrada.Checked
                Muelle.Salida = chkSalida.Checked
                Muelle.User_mod = AP.UsuarioAp.IdUsuario
                Muelle.Fec_mod = Now
                Muelle.IdUbicacionDefecto = cmbUbicacionDefecto.EditValue

                If picFoto.Image IsNot Nothing Then
                    Muelle.Imagen = ImageToByteArray(picFoto.Image)
                End If

                Actualizar = clsLnBodega_muelles.Actualizar(Muelle) > 0

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

            If cmbBodega.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione Bodega.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(NombreTextEdit.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NombreTextEdit.Focus()
            ElseIf NombreTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                XtraMessageBox.Show(String.Format("El Nombre debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "NOMBRE").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NombreTextEdit.Focus()
            ElseIf Codigo_barraTextEdit.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud Then
                XtraMessageBox.Show(String.Format("El código de barra debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "CODIGO_BARRA").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Codigo_barraTextEdit.Focus()
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
        mnuGuardar.Enabled = False
        If Datos_Correctos() Then
            If MessageBox.Show("¿Guardar registro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If
                    Close()
                End If
            End If
        End If
        mnuGuardar.Enabled = True
    End Sub

    Private Sub cmbBodega_SelectedIndexChanged(sender As Object, e As EventArgs)
        If Me.cmbBodega.EditValue <> 0 Then
            AP.IdBodega = cmbBodega.EditValue
        End If
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        mnuActualizar.Enabled = False
        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If Listar IsNot Nothing Then
                Listar.Invoke()
            End If
            Close()
        End If
        mnuActualizar.Enabled = True
    End Sub

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try
            mnuEliminar.Enabled = False

            If MessageBox.Show("¿Desactivar Muelle?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Muelle.Activo = False
                If clsLnBodega_muelles.Actualizar(Muelle) > 0 Then
                    XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Listar IsNot Nothing Then
                        Listar.Invoke()
                    End If
                    Close()
                End If
            End If

            mnuEliminar.Enabled = True
        Catch ex As Exception
            mnuEliminar.Enabled = True
            If ex.HResult = -2146233088 Then TablasRelacionadas("bodega_muelles", Muelle.IdMuelle)
        End Try

    End Sub

    Public Shared Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As Image
        Dim ms As New MemoryStream(byteArrayIn)
        Return Image.FromStream(ms)
    End Function

    Public Shared Function ImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New MemoryStream()
        imageIn.Save(ms, ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

    Private Sub btnExaminar_Click(sender As Object, e As EventArgs) Handles btnExaminar.Click
        Dim gFile As New OpenFileDialog() With {.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files|*.*"}
        gFile.ShowDialog()
        If gFile.FileName.Length <> 0 Then
            picFoto.Image = Image.FromFile(gFile.FileName)
        End If
    End Sub

    Private Sub frmBodega_Muelles_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class