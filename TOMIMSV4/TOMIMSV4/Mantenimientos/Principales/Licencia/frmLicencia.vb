Imports System.Reflection
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class frmLicencia

    'Private cLic As New clsBeLicencia
    Private cLic As New clsBeLicencia_item
    Private aItems As New List(Of clsBeLicencia_item)
    Private pItems As New List(Of clsBeLicencia_solic)
    Private licActiva As Boolean = False
    Private ForceEnable As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
    Private licBofAsignadas As Integer = 0
    Private licHHAsignadas As Integer = 0
    Private licUxAsignadas As Integer = 0

    Private licBofDisponible As Integer = 0
    Private licHHDisponible As Integer = 0
    Private LicUxDisponible As Integer = 0



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

            If AP.LicenciaServidor Then
                mnuActualizar.Enabled = True
                GroupBox1.Enabled = True
            Else
                mnuActualizar.Enabled = False
                GroupBox1.Enabled = False
            End If

            'If GridViewAct.RowCount = 0 Then
            '    mnuActualizar.Enabled = True
            'Else
            '    mnuActualizar.Enabled = AP.LicenciaServidor
            'End If

            'GroupBox1.Enabled = mnuActualizar.Enabled

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
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try

    End Sub

    Private Sub Mostrar_Licencia()

        licActiva = False

        Try

            If clsLnLicencia_llave.GetSingle(cLic) Then

                licBofDisponible = cLic.CantBackOffice
                licHHDisponible = cLic.CantHandHeld
                LicUxDisponible = cLic.CantUx
                'lblBO.Text = cLic.CantBackOffice
                'lblHH.Text = cLic.CantHandHeld
                lblExpira.Text = cLic.Vence.ToShortDateString
                licActiva = cLic.Activa
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

            aItems.Clear()
            aItems = clsLnLicencia_item.Get_All_Activos()
            grdLic.DataSource = aItems

            If aItems IsNot Nothing AndAlso aItems.Count > 0 Then

                licBofAsignadas = aItems.Where(Function(x) x.Tipo = 1).Count
                licHHAsignadas = aItems.Where(Function(x) x.Tipo = 0).Count
                licUxAsignadas = aItems.Where(Function(x) x.Tipo = 2).Count

                '#GT17012025: mostrar licencias asignadas contra las disponibles
                lblBO.Text = String.Format("{0}/{1}", licBofAsignadas, licBofDisponible)
                lblHH.Text = String.Format("{0}/{1}", licHHAsignadas, licHHDisponible)
                txtLicUx.Text = String.Format("{0}/{1}", licUxAsignadas, LicUxDisponible)


            End If

            If GridViewAct.Columns.Count > 0 Then

                GridViewAct.Columns(0).Visible = False
                GridViewAct.Columns(1).Visible = False
                GridViewAct.Columns(5).Visible = False

                GridViewAct.Columns("Vence").Caption = "Último ingreso"
                GridViewAct.Columns("Vence").DisplayFormat.FormatType = FormatType.DateTime
                GridViewAct.Columns("Vence").DisplayFormat.FormatString = "G"

                GridViewAct.Columns("Fecha_Sistema").Caption = "Fecha_Autorización"
                GridViewAct.Columns("Fecha_Sistema").DisplayFormat.FormatType = FormatType.DateTime
                GridViewAct.Columns("Fecha_Sistema").DisplayFormat.FormatString = "G"

                GridViewAct.BestFitColumns(True)

            End If

            pItems = clsLnLicencia_item.Get_All_Pendientes_Aprobacion()
            grdPend.DataSource = pItems

            If GridViewPend.Columns.Count > 0 Then

                GridViewPend.Columns("Tipo").Visible = False
                GridViewPend.Columns("Estado").Visible = False

                GridViewPend.Columns("Fecha_Solicitud").DisplayFormat.FormatType = FormatType.DateTime
                GridViewPend.Columns("Fecha_Solicitud").DisplayFormat.FormatString = "G"

                GridViewPend.BestFitColumns(True)

            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Try

            Dim frmSol As New frmLicSolicitud
            Dim sitem As clsBeLicencia_solic

            If GridViewPend.RowCount > 0 Then

                sitem = GridViewPend.GetFocusedRow()

                frmSol.Modo = frmLicSolicitud.pModo.LIC
                frmSol.sitem = sitem
                '#EJC20171108_REF11_1254AM: desactivado temporalmente por refactorización
                frmSol.cLic = cLic
                frmSol.Bandera = IIf(aItems.Count = 0, 1, 0)
                frmSol.mac = sitem.Identificacion

                If frmSol.ShowDialog = DialogResult.Yes Then
                    Listar_Dispositivos()
                End If

            Else
                XtraMessageBox.Show("No hay solicitudes de licencia para aprobar.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
           Text,
           MessageBoxButtons.OK,
           MessageBoxIcon.Exclamation)
        End Try

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
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
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

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
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
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        BarButtonItem2.Enabled = False
        Listar_Dispositivos()
        BarButtonItem2.Enabled = True
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
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub grdPend_DoubleClick(sender As Object, e As EventArgs) Handles grdPend.DoubleClick

        Try

            If GridViewPend.RowCount > 0 Then

                'Dim Dr As New clsBeLicencia_item
                Dim Dr As New clsBeLicencia_solic
                Dr = GridViewPend.GetFocusedRow()

                Dim lSelectionIndex As Integer = GridViewPend.FocusedRowHandle
                Dim IdDispoSolic As String = Dr.IdDisp

                If XtraMessageBox.Show("Eliminar solicitud de: " & IdDispoSolic & "?",
                                          Text,
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question) = DialogResult.Yes Then

                    IdDispoSolic = clsPublic.EncodeString(IdDispoSolic)

                    Dim vResult As Integer = clsLnLicencia_solic.Eliminar_By_IdDisp(IdDispoSolic)

                    If vResult > 0 Then

                        XtraMessageBox.Show("Solicitud eliminada!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Listar_Dispositivos()

                    End If

                End If

            End If


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

End Class