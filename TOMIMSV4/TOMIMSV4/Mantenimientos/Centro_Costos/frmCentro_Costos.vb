Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmCentro_Costos


    Public gBeCentroCostos As New clsBeCentro_costo

    Public Delegate Sub ListarCentroCosto()
    Public Property InvokeListarCentroCosto As ListarCentroCosto

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

    Private Sub mnuGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuardar.ItemClick

        mnuGuardar.Enabled = False

        If Datos_Correctos() Then
            If MessageBox.Show("¿Guardar Datos?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If Guardar() Then
                    XtraMessageBox.Show("Se guardaron los datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    InvokeListarCentroCosto.Invoke
                    Close()
                End If
            End If
        End If

        mnuGuardar.Enabled = True

    End Sub

    Private Sub frmCentro_Costos_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            IMS.Listar_Empresas(cmbEmpresa)

            Select Case Modo

                Case TipoTrans.Nuevo

                    lblCodigo.Text = clsLnCentro_costo.MaxID() + 1
                    lblCodigo.Enabled = False
                    mnuGuardar.Enabled = If(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    mnuActualizar.Enabled = False
                    mnuEliminar.Enabled = False

                Case TipoTrans.Editar

                    Cargar_CentroCostos()

                    lblCodigo.Enabled = False
                    mnuGuardar.Enabled = False
                    mnuActualizar.Enabled = If(OpcionesMenu IsNot Nothing, OpcionesMenu.Modificar, True)
                    mnuEliminar.Enabled = If(OpcionesMenu IsNot Nothing, OpcionesMenu.Eliminar, True)

            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Cargar_CentroCostos()

        Try

            'lcmbPropietario.EditValue = gBeCliente.IdPropietario
            lblCodigo.Text = gBeCentroCostos.IdCentroCosto
            CodigoTextEdit.Text = gBeCentroCostos.Codigo
            NombreTextEdit.Text = gBeCentroCostos.Nombre
            chkActivo.Checked = gBeCentroCostos.Activo
            chkControlInv.Checked = gBeCentroCostos.Control_Inventario
            txtReferencia.Text = gBeCentroCostos.Referencia

            If gBeCentroCostos.IdEmpresa <> 0 Then
                cmbEmpresa.EditValue = gBeCentroCostos.IdEmpresa
                cmbEmpresa.Enabled = False
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function Guardar() As Boolean

        Guardar = False

        Try

            Dim objCentroCostos As New clsBeCentro_costo()

            objCentroCostos.IdEmpresa = cmbEmpresa.EditValue
            objCentroCostos.Codigo = CodigoTextEdit.EditValue
            objCentroCostos.Nombre = NombreTextEdit.EditValue
            objCentroCostos.Fec_agr = Now
            objCentroCostos.User_agr = AP.UsuarioAp.IdUsuario
            objCentroCostos.Fec_mod = Now
            objCentroCostos.User_mod = AP.UsuarioAp.IdUsuario
            objCentroCostos.Activo = chkActivo.Checked
            objCentroCostos.Control_Inventario = chkControlInv.Checked
            objCentroCostos.Referencia = txtReferencia.Text

            clsLnCentro_costo.Guardar_Transaccion(objCentroCostos)

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

    Private Function Datos_Correctos() As Boolean
        Datos_Correctos = False

        Try

            If String.IsNullOrEmpty(CodigoTextEdit.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Código", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CodigoTextEdit.Focus()
            ElseIf String.IsNullOrEmpty(NombreTextEdit.Text.Trim) Then
                XtraMessageBox.Show("Ingrese Nombre", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                NombreTextEdit.Focus()
            Else
                Datos_Correctos = True
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Try

            mnuActualizar.Enabled = False

            If Actualizar() Then
                XtraMessageBox.Show("Se actualizaron los datos", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Not InvokeListarCentroCosto Is Nothing Then InvokeListarCentroCosto.Invoke
                Close()
            End If

            mnuActualizar.Enabled = True

        Catch ex As Exception

            mnuActualizar.Enabled = True
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                             Text,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Function Actualizar() As Boolean

        Actualizar = False

        Try

            If Datos_Correctos() Then

                gBeCentroCostos = New clsBeCentro_costo

                gBeCentroCostos.IdCentroCosto = lblCodigo.Text.Trim()
                gBeCentroCostos.IdEmpresa = cmbEmpresa.EditValue
                gBeCentroCostos.Codigo = CodigoTextEdit.Text.Trim()
                gBeCentroCostos.Nombre = NombreTextEdit.Text.Trim()
                gBeCentroCostos.User_agr = AP.UsuarioAp.IdUsuario
                gBeCentroCostos.Fec_agr = Now
                gBeCentroCostos.User_mod = AP.UsuarioAp.IdUsuario
                gBeCentroCostos.Fec_mod = Now
                gBeCentroCostos.Activo = chkActivo.Checked
                gBeCentroCostos.Control_Inventario = chkControlInv.Checked
                gBeCentroCostos.Referencia = txtReferencia.Text

                clsLnCentro_costo.Guardar_Transaccion(gBeCentroCostos)

                Actualizar = True

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Function

    Private Sub mnuEliminar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminar.ItemClick

        Try
            mnuEliminar.Enabled = False

            If MessageBox.Show("¿Desactivar Datos?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                gBeCentroCostos.Activo = False

                clsLnCentro_costo.Actualizar(gBeCentroCostos)

                XtraMessageBox.Show("Se ha desactivado el registro", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Not InvokeListarCentroCosto Is Nothing Then InvokeListarCentroCosto.Invoke()

                Close()

            End If

            mnuEliminar.Enabled = True

        Catch ex As Exception
            mnuEliminar.Enabled = True
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try




    End Sub
End Class