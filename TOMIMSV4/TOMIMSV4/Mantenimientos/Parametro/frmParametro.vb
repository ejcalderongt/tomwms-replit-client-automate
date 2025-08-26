Imports DevExpress.XtraEditors

Public Class frmParametro


    Private pTexto As New TextBox
    Private pNumero As New NumericUpDown
    Private pFecha As New DateTimePicker
    Private pLogico As New CheckBox
    Private pListObjT As New List(Of clsTabla)

    Private ObjLnPE As New clsLnP_parametro
    Public ObjBePE As New clsBeP_parametro


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


    Private Sub frmProducto_Estado_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Try

            pListObjT = clsBD.GetLongitudByTabla("p_parametro")

            Select Case Modo
                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnP_parametro.MAXIdParametro
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
                    cmbTipo.Enabled = True

                Case TipoTrans.Editar

                    clsLnP_parametro.Obtener(ObjBePE)
                    lblCodigo.Text = ObjBePE.IdParametro
                    cmbTipo.Text = ObjBePE.Tipo
                    cmbTipo.Enabled = False
                    txtDescripcion.Text = ObjBePE.Descripcion
                    chkActivo.Checked = ObjBePE.Activo

                    Select Case cmbTipo.Text

                        Case "Númerico"
                            pNumero.Text = ObjBePE.Valor_numerico
                        Case "Texto"
                            pTexto.Text = ObjBePE.Valor_texto
                        Case "Fecha"
                            pFecha.Value = ObjBePE.Valor_fecha
                        Case "Lógico"
                            pLogico.Checked = ObjBePE.Valor_logico

                    End Select

                    User_agrTextEdit.Text = ObjBePE.User_agr
                    Fec_agrDateEdit.Text = ObjBePE.Fec_agr
                    User_modTextEdit.Text = ObjBePE.User_mod
                    Fec_modDateEdit.Text = ObjBePE.Fec_mod

                    mnuGuardar.Enabled = False

                    If OpcionesMenu IsNot Nothing Then
                        mnuActualizar.Enabled = OpcionesMenu.Modificar
                        mnuEliminar.Enabled = OpcionesMenu.Eliminar
                        mnuAsignacion.Enabled = OpcionesMenu.Modificar
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

        Me.Focus()
        txtDescripcion.Focus()

    End Sub


    Private Function Guardar() As Boolean
        Guardar = False
        Try

            ObjBePE.IdParametro = clsLnP_parametro.MAXIdParametro()
            ObjBePE.Tipo = cmbTipo.Text
            ObjBePE.Descripcion = txtDescripcion.Text.Trim()
            ObjBePE.Activo = True

            Select Case cmbTipo.Text

                Case "Númerico"
                    ObjBePE.Valor_numerico = CDbl(pNumero.Text)
                    ObjBePE.Valor_texto = String.Empty
                    ObjBePE.Valor_fecha = Nothing
                    ObjBePE.Valor_logico = Nothing
                Case "Texto"

                    ObjBePE.Valor_texto = pTexto.Text
                    If ObjBePE.Valor_texto.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "VALOR_TEXTO").Longitud Then
                        XtraMessageBox.Show(String.Format("El Texto debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "VALOR_TEXTO").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        pTexto.Focus()
                        Return False
                    End If

                    ObjBePE.Valor_numerico = Nothing
                    ObjBePE.Valor_fecha = Nothing
                    ObjBePE.Valor_logico = Nothing
                Case "Fecha"
                    ObjBePE.Valor_fecha = pFecha.Value
                    ObjBePE.Valor_numerico = Nothing
                    ObjBePE.Valor_texto = String.Empty
                    ObjBePE.Valor_logico = Nothing
                Case "Lógico"
                    ObjBePE.Valor_logico = pLogico.Checked
                    ObjBePE.Valor_texto = String.Empty
                    ObjBePE.Valor_numerico = Nothing
                    ObjBePE.Valor_fecha = Nothing

            End Select

            ObjBePE.User_agr = AP.UsuarioAp.IdUsuario
            ObjBePE.Fec_agr = Now
            ObjBePE.User_mod = AP.UsuarioAp.IdUsuario
            ObjBePE.Fec_mod = Now

            Guardar = ObjLnPE.Insertar(ObjBePE) > 0

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

                ObjBePE.Tipo = cmbTipo.Text
                ObjBePE.Descripcion = txtDescripcion.Text.Trim()
                ObjBePE.Activo = chkActivo.Checked

                Select Case cmbTipo.Text

                    Case "Númerico"
                        ObjBePE.Valor_numerico = CDbl(pNumero.Text)
                        ObjBePE.Valor_texto = String.Empty
                        ObjBePE.Valor_fecha = Nothing
                        ObjBePE.Valor_logico = Nothing
                    Case "Texto"
                        ObjBePE.Valor_texto = pTexto.Text
                        ObjBePE.Valor_numerico = Nothing
                        ObjBePE.Valor_fecha = Nothing
                        ObjBePE.Valor_logico = Nothing
                    Case "Fecha"
                        ObjBePE.Valor_fecha = pFecha.Value
                        ObjBePE.Valor_numerico = Nothing
                        ObjBePE.Valor_texto = String.Empty
                        ObjBePE.Valor_logico = Nothing
                    Case "Lógico"
                        ObjBePE.Valor_logico = pLogico.Checked
                        ObjBePE.Valor_texto = String.Empty
                        ObjBePE.Valor_numerico = Nothing
                        ObjBePE.Valor_fecha = Nothing

                End Select

                ObjBePE.User_mod = AP.UsuarioAp.IdUsuario
                ObjBePE.Fec_mod = Now

                Actualizar = ObjLnPE.Actualizar(ObjBePE) > 0

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

            If cmbTipo.SelectedIndex = -1 Then
                XtraMessageBox.Show("Seleccione Tipo.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbTipo.Focus()
            ElseIf String.IsNullOrEmpty(txtDescripcion.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Descripción.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDescripcion.Focus()
            ElseIf txtDescripcion.Text.Count > pListObjT.Find(Function(b) b.NombreCampo.ToUpper = "DESCRIPCION").Longitud Then
                XtraMessageBox.Show(String.Format("La Descripción debe de tener como máximo {0} carácteres.", pListObjT.Find(Function(ByVal b) b.NombreCampo.ToUpper = "DESCRIPCION").Longitud), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDescripcion.Focus()
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

            If MessageBox.Show("¿Guardar el parámetro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If Guardar() Then
                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Close()
                End If

            End If

        End If

    End Sub


    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If

    End Sub


    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        If ObjBePE.Activo = False Then
            XtraMessageBox.Show("El registro ya se encuentra desactivado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else

            If MessageBox.Show("¿Desactivar el parámetro?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                ObjBePE.Activo = False

                If ObjLnPE.Actualizar(ObjBePE) > 0 Then
                    Close()
                    'frmParametro_List.Dgrid.Refresh()
                End If

            End If

        End If

    End Sub


    Private Sub mnuAsignacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAsignacion.ItemClick
        XtraMessageBox.Show("En Mantenimiento", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


    Private Sub cmbTipo_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbTipo.SelectedValueChanged

        Try

            Select Case cmbTipo.Text

                Case "Númerico"
                    CreaObjetoNumero()
                Case "Texto"
                    CreaObjetoTexto()
                Case "Fecha"
                    CreaObjetoFecha()
                Case "Lógico"
                    CreaObjetoLogico()

            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub CreaObjetoNumero()

        pTexto.Dispose()
        pFecha.Dispose()
        pLogico.Dispose()

        Try
            'GT 30082021: al crear un tipo de parametro no se requiere que el usuario asigne valor, eso lo hace en producto.
            pNumero = New NumericUpDown()
            With pNumero
                .Value = 0.0
                .Location = New Point(139, 113)
                .Width = 194
                .Maximum = 99999999999
                .Minimum = -9999999999999
                .DecimalPlaces = 6
                .Visible = True
            End With
            Grp.Controls.Add(pNumero)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub CreaObjetoTexto()

        pNumero.Dispose()
        pFecha.Dispose()
        pLogico.Dispose()

        Try
            'GT 30082021: al crear un tipo de parametro no se requiere que el usuario asigne valor, eso lo hace en producto.
            pTexto = New TextBox()
            With pTexto
                .SelectedText = "-"
                .Location = New Point(139, 113)
                .Width = 194
                .Visible = True
            End With
            Grp.Controls.Add(pTexto)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub CreaObjetoFecha()

        pTexto.Dispose()
        pNumero.Dispose()
        pLogico.Dispose()

        Try
            'GT 30082021: al crear un tipo de parametro no se requiere que el usuario asigne valor, eso lo hace en producto.
            pFecha = New DateTimePicker()
            With pFecha
                '.Location = New Point(160, 105)
                .Value = DateTime.Now
                .Location = New Point(139, 113)
                .Width = 194
                .CustomFormat = "dd/MM/yyyy hh:mm:ss"
                .Format = DateTimePickerFormat.Custom
                .Visible = True
            End With
            Grp.Controls.Add(pFecha)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub CreaObjetoLogico()

        pTexto.Dispose()
        pNumero.Dispose()
        pFecha.Dispose()

        Try

            pLogico = New CheckBox()
            With pLogico
                .Location = New Point(139, 113)
                .Width = 194
                .Visible = True
            End With
            Grp.Controls.Add(pLogico)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

End Class