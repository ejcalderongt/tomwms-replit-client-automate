Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports TOMWMS

Public Class frmSucursal

    Public pSucursal As New clsBeQT_Sucursal
    Private pListObjT As New List(Of clsTabla)

    Public Delegate Sub Listar_Sucursales()
    Public Property InvokeListar As Listar_Sucursales



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

            pSucursal.IdFinca = clsLnQT_Sucursal.MaxID() + 1
            pSucursal.IsNew = True
            pSucursal.Codigo = txtCodigo.EditValue.trim
            pSucursal.Descripcion = txtDescripcion.EditValue.trim
            pSucursal.Predeterminada = chkFincaDefault.Checked
            pSucursal.Activo = chkActivo.Checked
            pSucursal.User_agr = 1
            pSucursal.Fec_agr = Now
            pSucursal.User_mod = 1
            pSucursal.Fec_mod = Now

            Guardar = clsLnQT_Sucursal.Insertar(pSucursal) > 0

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub frmSucursal_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            Select Case Modo
                Case TipoTrans.Nuevo
                    cmdActualizar.Enabled = False
                    pSucursal = New clsBeQT_Sucursal
                    pSucursal.IsNew = True
                Case TipoTrans.Editar
                    cmdGuardar.Enabled = False
                    pSucursal.IsNew = False
                    lblIdCorrelativo.Text = pSucursal.IdFinca
                    txtCodigo.EditValue = pSucursal.Codigo
                    txtDescripcion.Text = pSucursal.Descripcion
                    chkFincaDefault.Checked = pSucursal.Predeterminada
                    chkActivo.Checked = pSucursal.Activo
            End Select

            SplashScreenManager.CloseForm(False)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try
    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Try

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

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Actualizar() As Boolean
        Actualizar = False
        Try
            pSucursal.Descripcion = txtDescripcion.EditValue.trim
            pSucursal.Predeterminada = chkFincaDefault.Checked
            pSucursal.Activo = chkActivo.Checked
            pSucursal.User_mod = 1
            pSucursal.Fec_mod = Now

            Actualizar = clsLnQT_Sucursal.Actualizar(pSucursal) > 0

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try

    End Function

End Class