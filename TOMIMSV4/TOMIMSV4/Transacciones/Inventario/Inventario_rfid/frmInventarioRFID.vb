Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen
Imports TOMWMS.wsTOMHH

Public Class frmInventarioRFID

    Public gBeTransInvEnc As New clsBeTrans_inv_enc
    Dim gBetransInvCiclico As New clsBeTrans_inv_ciclico
    Dim BeTareaHH As clsBeTarea_hh
    Dim pIdPropietario As Integer = 0
    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans
    'Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmInventarioRFID_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try



            If Not AP.Listar_Bodegas_By_Usuario(cmbBodega) Then
                XtraMessageBox.Show("No hay bodegas definidas para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            If Not IMS.Listar_TipoInventario(cmbTipoInventario) Then
                XtraMessageBox.Show("No hay tipos de inventarios definidos para la aplicación", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


            If Not IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue) Then
                XtraMessageBox.Show("No hay propietarios definidos para la bodega", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            cargar_forma()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub cargar_forma()
        Try
            Select Case Modo

                Case TipoTrans.Nuevo

                    Fecha.EditValue = Now
                    dtpHoraInicio.EditValue = Now
                    dtpHoraFin.EditValue = Now
                    lblCod.Text = clsLnTrans_inv_enc.MaxID()
                    Estado.Text = "Nuevo"
                    Fecha.DateTime = Today
                    gBeTransInvEnc = New clsBeTrans_inv_enc With {.IsNew = True}

                Case TipoTrans.Editar

                    chkCapturarNoAsignado.Checked = gBeTransInvEnc.Capturar_No_Asignados
                    cmbTipoInventario.EditValue = gBeTransInvEnc.TipoInv
                    Fecha.EditValue = gBeTransInvEnc.Fecha
                    dtpHoraInicio.EditValue = gBeTransInvEnc.Hora_ini
                    dtpHoraFin.EditValue = gBeTransInvEnc.Hora_fin


            End Select

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmdGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdGuardar.ItemClick
        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormDescription("Guardando inventario...")

            If Validacion() Then
                If Guardar() Then
                    SplashScreenManager.CloseForm(False)

                    Modo = TipoTrans.Editar

                    XtraMessageBox.Show("Se guardó el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Me.Close()

                End If

            Else
                SplashScreenManager.CloseForm(False)
                XtraMessageBox.Show("No se pudo guardar el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function Guardar(Optional ByVal Es_Actualizacion As Boolean = False) As Boolean

        Guardar = False

        Try

            If gBeTransInvEnc.IsNew Then
                SplashScreenManager.Default.SetWaitFormDescription("Creando tarea de inventario...")
                Crea_Tarea_HH()

                gBeTransInvEnc.Idpropietario = pIdPropietario
                gBeTransInvEnc.IdBodega = cmbBodega.EditValue
                gBeTransInvEnc.IdTipoInventario = cmbTipoInventario.EditValue
                gBeTransInvEnc.Tipo_Conteo_Producto = cmbTipoConteo.EditValue
                gBeTransInvEnc.Fecha = Fecha.EditValue
                gBeTransInvEnc.Fecha_Ultimo_Inventario = dtpUltimoInv.EditValue
                gBeTransInvEnc.Estado = Estado.Text

                gBeTransInvEnc.Inicial = False
                gBeTransInvEnc.Doble_verificacion = chkDobleVerifica.Checked
                gBeTransInvEnc.Mostrar_Cantidad_Teorica_hh = False
                gBeTransInvEnc.Cambia_Ubicacion = False

                'gBeTransInvEnc.Capturar_no_existente = chkCaptNtExist.Checked
                gBeTransInvEnc.Activo = chkActivo.Checked
                gBeTransInvEnc.Regularizado = False
                gBeTransInvEnc.Hora_ini = dtpHoraInicio.EditValue
                gBeTransInvEnc.Hora_fin = dtpHoraFin.EditValue
                gBeTransInvEnc.User_agr = AP.UsuarioAp.Nombres
                gBeTransInvEnc.Fec_agr = Now
                gBeTransInvEnc.User_mod = AP.UsuarioAp.Nombres
                gBeTransInvEnc.Fec_mod = Now
                '#GT27042026: multipropietario aplica solo a cealsa
                gBeTransInvEnc.multi_propietario = False
                gBeTransInvEnc.IdCentroCosto = 0
                gBeTransInvEnc.Tipo_Asignacion = 0
                gBeTransInvEnc.Capturar_No_Asignados = False

                SplashScreenManager.Default.SetWaitFormDescription("Guardando transacción...")

                Guardar = clsLnTrans_inv_enc.Guardar(gBeTransInvEnc, BeTareaHH)

            Else



            End If



        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)
        End Try
    End Function

    Private Function Validacion() As Boolean
        Try
            If cmbTipoInventario.ItemIndex = -1 Then
                XtraMessageBox.Show("Seleccione un tipo de inventario.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            Return True

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Function

    Private Sub Crea_Tarea_HH()

        Try

            BeTareaHH = New clsBeTarea_hh

            If gBeTransInvEnc IsNot Nothing AndAlso gBeTransInvEnc.IsNew Then

                BeTareaHH.IdPropietario = pIdPropietario
                BeTareaHH.IdBodega = cmbBodega.EditValue

                BeTareaHH.IdMuelle = 0

                BeTareaHH.IdEstado = 1
                BeTareaHH.IdPrioridad = 1
                BeTareaHH.IdTipoTarea = 6
                BeTareaHH.IdTransaccion = gBeTransInvEnc.Idinventarioenc
                BeTareaHH.Tipo = 0
                BeTareaHH.FechaInicio = Fecha.EditValue
                BeTareaHH.FechaFin = Fecha.EditValue
                BeTareaHH.DiaCompleto = False
                BeTareaHH.CreaTarea = True
                BeTareaHH.IsNew = True
                BeTareaHH.Asunto = "Inventario"

            End If

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Try

            If Guardar(True) Then

                SplashScreenManager.CloseForm(False)

                XtraMessageBox.Show("Se actualizó el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                'If Not InvokeListarInventario Is Nothing Then InvokeListarInventario.Invoke

                Close()
            Else
                XtraMessageBox.Show("No se pudo actualizar el inventario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub cmbPropietario_EditValueChanged(sender As Object, e As EventArgs) Handles cmbPropietario.EditValueChanged
        Try
            'pIdPropietario = cmbPropietario.EditValue
            Dim row As DataRowView = CType(cmbPropietario.GetSelectedDataRow(), DataRowView)
            pIdPropietario = row("IdPropietario")
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
End Class