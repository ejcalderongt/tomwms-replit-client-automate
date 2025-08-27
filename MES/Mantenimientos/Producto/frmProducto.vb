Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports TOMWMS

Public Class frmProducto

    Public pProducto_Finca As New clsBeQTProducto_Sucursal
    Public Delegate Sub Listar_Producto()
    Public Property InvokeListar As Listar_Producto
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


    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        Try

            cmdGuardar.Enabled = False

            If Validar() Then

                If Guardar() Then

                    XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If InvokeListar IsNot Nothing Then
                        InvokeListar.Invoke()
                    End If

                    DialogResult = DialogResult.OK
                    Close()

                End If

            End If

            cmdGuardar.Enabled = True

        Catch ex As Exception
            cmdGuardar.Enabled = True
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function Validar() As Boolean
        Validar = False

        Try
            If String.IsNullOrEmpty(txtCodigo.EditValue) Then
                txtCodigo.Focus()
                XtraMessageBox.Show("Debe colocar un código.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf String.IsNullOrEmpty(txtDescripcion.EditValue) Then
                txtDescripcion.Focus()
                XtraMessageBox.Show("Debe colocar una descripcipon.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf cmbFinca.ItemIndex < 0 Then
                cmbFinca.Focus()
                XtraMessageBox.Show("Debe seleccionar una finca.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Validar = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try

    End Function

    Private Function Guardar() As Boolean
        Guardar = False

        Try
            pProducto_Finca.IdProducto = clsLnQT_Producto_Sucursal.MaxID() + 1
            pProducto_Finca.IsNew = True
            pProducto_Finca.Codigo = txtCodigo.EditValue.trim
            pProducto_Finca.Descripcion = txtDescripcion.EditValue.trim
            pProducto_Finca.IdFinca = cmbFinca.EditValue
            pProducto_Finca.Activo = chkActivo.Checked
            pProducto_Finca.User_agr = 1
            pProducto_Finca.Fec_agr = Now
            pProducto_Finca.User_mod = 1
            pProducto_Finca.Fec_mod = Now
            pProducto_Finca.Img_Producto = txtImgProducto.Text

            Guardar = clsLnQT_Producto_Sucursal.Insertar(pProducto_Finca) > 0

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub frmProducto_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            Cargar_sucursales()

            Select Case Modo
                Case TipoTrans.Nuevo
                    cmdActualizar.Enabled = False
                    lblCorrelativo.Text = clsLnQT_Producto_Sucursal.MaxID() + 1
                    pProducto_Finca = New clsBeQTProducto_Sucursal
                    pProducto_Finca.IsNew = True
                Case TipoTrans.Editar
                    cmdGuardar.Enabled = False
                    pProducto_Finca.IsNew = False
                    lblCorrelativo.Text = pProducto_Finca.IdProducto
                    txtCodigo.Text = pProducto_Finca.Codigo
                    txtDescripcion.Text = pProducto_Finca.Descripcion
                    cmbFinca.EditValue = pProducto_Finca.IdFinca
                    chkActivo.Checked = pProducto_Finca.Activo
                    txtImgProducto.Text = pProducto_Finca.Img_Producto
            End Select
            SplashScreenManager.CloseForm(False)

            cmbFinca.Focus()

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Try
            cmdActualizar.Enabled = False

            If Validar() Then
                If Actualizar() Then
                    XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListar IsNot Nothing Then
                        InvokeListar.Invoke()
                    End If
                    DialogResult = DialogResult.OK
                    Close()
                End If
            End If

            cmdActualizar.Enabled = True

        Catch ex As Exception
            cmdActualizar.Enabled = True
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Actualizar() As Boolean
        Actualizar = False
        Try

            pProducto_Finca.Codigo = txtCodigo.Text
            pProducto_Finca.Descripcion = txtDescripcion.EditValue.trim
            pProducto_Finca.IdFinca = cmbFinca.EditValue
            pProducto_Finca.Activo = chkActivo.Checked
            pProducto_Finca.User_mod = 1
            pProducto_Finca.Fec_mod = Now
            pProducto_Finca.Img_Producto = txtImgProducto.Text
            Actualizar = clsLnQT_Producto_Sucursal.Actualizar(pProducto_Finca) > 0

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Function

    Private Function Cargar_sucursales() As Boolean
        Cargar_sucursales = False
        Try
            Dim pFinca As DataTable
            pFinca = clsLnQT_Sucursal.Listar()

            With cmbFinca.Properties
                .DataSource = pFinca
                .NullText = "Seleccione una finca..."
                .ShowHeader = False
                .ShowFooter = False
                .PopulateColumns()
                .DisplayMember = "descripcion"
                .ValueMember = "IdFinca"
            End With


        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Function


End Class