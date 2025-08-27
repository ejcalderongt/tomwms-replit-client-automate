Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmLicencia

    'Private cLic As New clsBeLicencia
    Private cLic As New clsBeLicencia_item
    Private aItems As New List(Of clsBeLicencia_item)
    Private pItems As New List(Of clsBeLicencia_item)
    Private sItems As New List(Of clsBeLicencia_solic)

    Private licActiva As Boolean = False

    Private ForceEnable As Boolean = False

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Limpiar_Campos()
        txtLlave.Text = "" : txtLlave.Focus()
        lblBO.Text = "" : lblHH.Text = "" : lblExpira.Text = ""
    End Sub

    'txtLlave.Text = "MwAsADUALAAxADcALAAxADEALAAzADAA"

    Private Sub frmLicencia_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Limpiar_Campos()

            Mostrar_Licencia()

            Listar_Dispositivos()

            If GridViewAct.RowCount = 0 Then
                mnuActualizar.Enabled = True
            Else
                mnuActualizar.Enabled = AP.LicenciaServidor
            End If

            GroupBox1.Enabled = mnuActualizar.Enabled

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub frmLicencia_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        grdLic.Left = 10
        grdLic.Width = (ClientSize.Width - 120) / 2
        grdPend.Width = grdLic.Width
        grdPend.Left = 10 + grdLic.Width + 100 : lblSolic.Left = grdPend.Left
        btnAdd.Left = 10 + grdLic.Width + 10
        btnRem.Left = btnAdd.Left
    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick

        Try

            '#EJC20171108_REF05_0643PM: Refactoring structure, se agrego función validación de llave dentro de try
            If Llave_Valida(txtLlave.Text) Then

                If XtraMessageBox.Show(String.Format("¿Aplicar licencia: {0}Backoffice: {1}  {0}HandHeld:{2} {0}Vence:{3}?", vbCrLf, cLic.CantBackOffice, cLic.CantHandHeld, cLic.Vence.ToShortDateString), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim BeLicLlaveSol As New clsBeLicencia_llave() With {.IdLlave = clsLnLicencia_llave.MaxID(),
                        .Llave = txtLlave.Text}

                    '#EJC20171108_REF02_0605PM: Refactoring clsBeLicencia_llave                
                    If Not clsLnLicencia_llave.Exist(txtLlave.Text) Then
                        clsLnLicencia_llave.Insertar(BeLicLlaveSol)
                    Else
                        clsLnLicencia_llave.Actualizar(BeLicLlaveSol)
                    End If

                    Mostrar_Licencia()

                    XtraMessageBox.Show("Licencia actualizada correctamente",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

                End If

            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Private Sub Mostrar_Licencia()

        licActiva = False

        Try

            If clsLnLicencia_llave.GetSingle(cLic) Then
                lblBO.Text = cLic.CantBackOffice
                lblHH.Text = cLic.CantHandHeld
                lblExpira.Text = cLic.Vence.ToShortDateString
                licActiva = cLic.Activa
            Else

                'XtraMessageBox.Show("La licencia no es válida",
                'Text,
                'MessageBoxButtons.OK,
                'MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            lblBO.Text = "" : lblHH.Text = "" : lblExpira.Text = "" : licActiva = False
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

        btnAdd.Visible = licActiva
        btnRem.Visible = licActiva

    End Sub

    Private Function Llave_Valida(ByVal Llave As String) As Boolean

        Llave_Valida = False

        Try

            clsLnLicencia_item.Cargar(cLic, Llave)

            Llave_Valida = True

        Catch ex As Exception
            XtraMessageBox.Show("Llave incorrecta", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Private Sub Listar_Dispositivos()

        Try

            aItems = clsLnLicencia_item.Get_All_Activos()
            grdLic.DataSource = aItems

            GridViewAct.Columns(0).Visible = False
            GridViewAct.Columns(1).Visible = False
            GridViewAct.Columns(5).Visible = False
            GridViewAct.Columns(2).Caption = "Último ingreso"

            GridViewAct.BestFitColumns(True)

            'pItems = clsLnLicencia_item.Get_All_Pendientes_Aprobacion()
            sItems = clsLnLicencia_item.Get_All_Pendientes_Aprobacion()

            'grdPend.DataSource = pItems
            grdPend.DataSource = sItems

            GridViewPend.Columns(3).Visible = False
            GridViewPend.Columns(5).Visible = False
            GridViewPend.Columns(0).Visible = False
            GridViewPend.Columns(1).Visible = False
            GridViewPend.Columns(5).Visible = False

            GridViewPend.BestFitColumns(True)

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click, grdPend.DoubleClick

        Dim frmSol As New frmLicSolicitud
        Dim sitem As clsBeLicencia_item

        If GridViewPend.RowCount = 0 Then Return

        Try
            sitem = GridViewPend.GetFocusedRow()
        Catch ex As Exception
            Return
        End Try

        frmSol.Modo = frmLicSolicitud.pModo.LIC
        frmSol.sitem = sitem
        '#EJC20171108_REF11_1254AM: desactivado temporalmente por refactorización
        frmSol.cLic = cLic
        frmSol.Bandera = IIf(aItems.Count = 0, 1, 0)
        frmSol.mac = sitem.Identificacion

        If frmSol.ShowDialog = DialogResult.Yes Then
            Listar_Dispositivos()
        End If

    End Sub

    Private Sub btnRem_Click(sender As Object, e As EventArgs) Handles btnRem.Click

        Dim sitem As clsBeLicencia_item
        Dim rslt As Integer

        If GridViewAct.RowCount = 0 Then Return

        Try
            sitem = GridViewAct.GetFocusedRow()
        Catch ex As Exception
            Return
        End Try

        If sitem.Estado = "Conectado" Then
            XtraMessageBox.Show("No se puede desactivar licencia si cliente está conectado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If

        If XtraMessageBox.Show("¿ Desactivar la licencia ?  ", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Try

            rslt = clsLnLicencia_item.Elimina_Licencia(sitem.IdDisp)

            If rslt = 1 Then
                XtraMessageBox.Show("Licencia eliminada correctamente",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)
                Listar_Dispositivos()
            Else
                XtraMessageBox.Show("No se pudo eliminar la licencia. ", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message))
        End Try

    End Sub

    Private Sub btnServ_Click(sender As Object, e As EventArgs) Handles btnServ.Click

        Dim frmSol As New frmLicSolicitud
        Dim sitem As clsBeLicencia_item

        Try

            sitem = GridViewAct.GetFocusedRow()

            If sitem.Tipo <> clsBeLicencia_item.eTipoHost.BOF Then
                XtraMessageBox.Show("Seleccione una licencia de Back Office.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            frmSol.Modo = frmLicSolicitud.pModo.SRV
            frmSol.mac = sitem.Identificacion

            If frmSol.ShowDialog = DialogResult.Yes Then Listar_Dispositivos()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub btnCon_Click(sender As Object, e As EventArgs) Handles btnCon.Click

        Dim frmSol As New frmLicSolicitud
        Dim sitem As clsBeLicencia_item

        Try
            sitem = GridViewAct.GetFocusedRow()
            frmSol.Modo = frmLicSolicitud.pModo.CON
            frmSol.mac = sitem.Identificacion
            If frmSol.ShowDialog = DialogResult.Yes Then Listar_Dispositivos()
        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Listar_Dispositivos()
    End Sub

    Private Sub frmLicencia_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Try

            If e.Control AndAlso e.KeyCode = Keys.D4 Then
                MsgBox("Control interno habilitado", MsgBoxStyle.Information, "Lic")
                mnuActualizar.Enabled = True
                GroupBox1.Enabled = mnuActualizar.Enabled
                Me.Refresh()
            End If

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

End Class