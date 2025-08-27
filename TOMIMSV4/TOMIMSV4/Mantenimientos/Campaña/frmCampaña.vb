Imports DevExpress.Xpf.Core.Utils
Imports DevExpress.XtraEditors
Imports DocumentFormat.OpenXml.Presentation
Imports System.Reflection

Public Class frmCampaña

    Private pListTablas As New List(Of clsTabla)
    Public Delegate Sub Listar()
    Public Property InvokeListarCampañas As Listar
    Public pObjCampaña As New clsBeCampaña

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

    Private Sub frmCampaña_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            pListTablas = clsBD.GetLongitudByTabla("campaña")

            Select Case Modo

                Case TipoTrans.Nuevo

                    pObjCampaña = New clsBeCampaña
                    lblIdCampaña.Text = clsLnCampaña.MaxID() + 1
                    If OpcionesMenu IsNot Nothing Then
                        cmdGuardar.Enabled = OpcionesMenu.Modificar
                    End If

                    cmdActualizar.Enabled = False
                    cmdEliminar.Enabled = False

                Case TipoTrans.Editar

                    pObjCampaña.IsNew = False
                    lblIdCampaña.Text = pObjCampaña.IdCampaña
                    txtNombreCampaña.Text = pObjCampaña.Nombre
                    dtpFechaDesde.Value = pObjCampaña.FechaInicio.Date
                    dtpFechaHasta.Value = pObjCampaña.FechaFin.Date
                    chkActivo.Checked = pObjCampaña.Activo
                    txtCodigo.Text = pObjCampaña.Codigo
                    cmdGuardar.Enabled = False
                    cmdActualizar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    cmdEliminar.Enabled = IIf(OpcionesMenu IsNot Nothing, OpcionesMenu.Eliminar, True)

            End Select



        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


    Private Function Datos_Correctos() As Boolean

        Datos_Correctos = False

        Try
            If String.IsNullOrEmpty(txtNombreCampaña.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Nombre.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombreCampaña.Focus()
            ElseIf txtNombreCampaña.Text.Count > pListTablas.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud Then
                DevExpress.XtraEditors.XtraMessageBox.Show("El Nombre debe de tener como máximo " & pListTablas.Find(Function(b) b.NombreCampo.ToUpper = "NOMBRE").Longitud & " carácteres.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNombreCampaña.Focus()
            ElseIf String.IsNullOrEmpty(txtCodigo.Text.Trim()) Then
                XtraMessageBox.Show("Ingrese Código.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCodigo.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        cmdGuardar.Enabled = False
        Guardar_Registro()
        cmdGuardar.Enabled = True
    End Sub

    Private Sub Guardar_Registro()
        Try

            If Datos_Correctos() Then

                If MessageBox.Show("¿Guardar la Campaña?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Guardar() Then XtraMessageBox.Show("Se guardó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If InvokeListarCampañas IsNot Nothing Then
                        InvokeListarCampañas.Invoke()
                    End If
                    Close()
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function Guardar() As Boolean
        Guardar = False

        Try
            pObjCampaña = New clsBeCampaña
            pObjCampaña.IsNew = True
            pObjCampaña.IdCampaña = clsLnCampaña.MaxID() + 1
            pObjCampaña.Nombre = txtNombreCampaña.Text
            pObjCampaña.FechaInicio = dtpFechaDesde.Value.Date
            pObjCampaña.FechaFin = dtpFechaHasta.Value.Date
            pObjCampaña.Fec_agr = Now
            pObjCampaña.User_agr = AP.UsuarioAp.IdUsuario
            pObjCampaña.Fec_mod = Now
            pObjCampaña.User_mod = AP.UsuarioAp.IdUsuario
            pObjCampaña.Activo = True
            pObjCampaña.Codigo = txtCodigo.Text
            Guardar = clsLnCampaña.Insertar(pObjCampaña) > 0

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        cmdActualizar.Enabled = False

        If Actualizar() Then
            XtraMessageBox.Show("Se actualizó el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarCampañas IsNot Nothing Then
                InvokeListarCampañas.Invoke()
            End If
            Close()
        End If

        cmdActualizar.Enabled = True
    End Sub


    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                pObjCampaña.Nombre = txtNombreCampaña.Text
                pObjCampaña.Activo = chkActivo.Checked
                pObjCampaña.Fec_mod = Now
                pObjCampaña.User_mod = AP.UsuarioAp.IdUsuario
                '#GT18022025: user_agr, fec_agr no se deben alterar porque es un update
                Actualizar = clsLnCampaña.Actualizar(pObjCampaña) > 0

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Function


    Private Sub cmdEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdEliminar.ItemClick
        cmdActualizar.Enabled = False

        If Eliminar() Then
            XtraMessageBox.Show("Se elimino el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            If InvokeListarCampañas IsNot Nothing Then
                InvokeListarCampañas.Invoke()
            End If
            Close()
        End If

        cmdActualizar.Enabled = True
    End Sub

    Private Function Eliminar() As Boolean

        Eliminar = False

        Try
            If Datos_Correctos() Then
                '#GT28042025: desactivar registro, no se elimina de momento.
                pObjCampaña.User_agr = AP.UsuarioAp.IdUsuario
                pObjCampaña.Fec_mod = Now
                pObjCampaña.Activo = False
                Eliminar = clsLnCampaña.Desactivar(pObjCampaña) > 0
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Function

End Class