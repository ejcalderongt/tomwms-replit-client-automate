Imports System.Reflection
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmPrincipal

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick

        Try

            Dim frmNuevoTicket As New frmTicketN
            frmNuevoTicket.Modo = frmTicketN.ModoTrans.Nuevo
            frmNuevoTicket.ShowDialog()
            frmNuevoTicket.Dispose()

            Listar_Ticekts()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub frmPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            AP.Listar_BodegasLogin(cmbBodega)
            Listar_Propietarios()
            Listar_Ticekts()

        Catch ex As Exception
            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub Listar_Ticekts()

        Try

            Dim DT As New DataTable
            DT = clsLnTms_ticket.Get_All_For_Grid(AP.IdEmpresa, dtpFechaDesde.Value, dtpfechaHasta.Value)

            dgridTickets.DataSource = DT

            If GridView1.Columns.Count > 0 Then

                GridView1.Columns("Fecha_Ingreso").DisplayFormat.FormatType = FormatType.DateTime
                GridView1.Columns("Fecha_Ingreso").DisplayFormat.FormatString = "G"

                GridView1.Columns("Fecha_Salida").DisplayFormat.FormatType = FormatType.DateTime
                GridView1.Columns("Fecha_Salida").DisplayFormat.FormatString = "G"

                GridView1.Columns("IdTicket").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                GridView1.Columns("IdTicket").SummaryItem.DisplayFormat = "{0:n0}"

                GridView1.BestFitColumns()


            End If


        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Listar_Propietarios()

        Try

            Dim DT1 As New DataTable
            DT1 = clsLnPropietario_bodega.Get_All_By_Empresa_For_Combo(AP.IdEmpresa)

            cmbPropietario.Properties.DataSource = DT1
            cmbPropietario.Properties.ValueMember = "IdPropietario"
            cmbPropietario.Properties.DisplayMember = "Nombre"
            cmbPropietario.Properties.PopupWidth = 700
            cmbPropietario.Properties.BestFit()
            cmbPropietario.Properties.PopulateColumns()

            If cmbPropietario.Properties.Columns.Count > 0 Then
                cmbPropietario.Properties.Columns(0).Visible = False
                cmbPropietario.Properties.Columns(1).Visible = False
                cmbPropietario.ItemIndex = 0
            End If

            cmbPropietario.Properties.NullText = ""

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub fchDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDesde.ValueChanged

        Try

            If Me.dtpFechaDesde.Value > Me.dtpfechaHasta.Value Or Me.dtpfechaHasta.Value < Me.dtpFechaDesde.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                Listar_Ticekts()

            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub fchAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpfechaHasta.ValueChanged

        Try

            If Me.dtpFechaDesde.Value > Me.dtpfechaHasta.Value Or Me.dtpfechaHasta.Value < Me.dtpFechaDesde.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                Listar_Ticekts()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub mnuActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuActualizar.ItemClick
        Listar_Ticekts()
    End Sub

    Private Sub dgridTickets_DoubleClick(sender As Object, e As EventArgs) Handles dgridTickets.DoubleClick
        Procesa_Registro()
    End Sub

    Private Sub Procesa_Registro()

        Try

            If (GridView1.RowCount > 0) Then

                Dim Dr As DataRowView = GridView1.GetFocusedRow

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

                Dim IdTicket As Integer = Integer.Parse(Dr.Item("IdTicket"))

                Dim BeTicket As New clsBeTms_ticket
                BeTicket = clsLnTms_ticket.Get_Ticket_By_Id(IdTicket)

                If Not BeTicket Is Nothing Then

                    '#EJC20220519: ANTES.
                    'Dim frmNuevoTicket As New frmTicketN
                    'frmNuevoTicket.Modo = frmTicketN.ModoTrans.Editar
                    'frmNuevoTicket.BeTicket = BeTicket
                    'frmNuevoTicket.ShowDialog()
                    'frmNuevoTicket.Dispose()
                    'Listar_Ticekts()

                    If BeTicket.Estado = "Abierto" Then

                        Dim frmEditTicket As New frmTicketEdit
                        frmEditTicket.Modo = frmTicketN.ModoTrans.Editar
                        frmEditTicket.BeTicket = BeTicket
                        frmEditTicket.ShowDialog()
                        frmEditTicket.Dispose()
                        Listar_Ticekts()

                    Else
                        SplashScreenManager.CloseForm(False)
                        XtraMessageBox.Show("El ticket no se encuentra en estado abierto, no se puede modificar.",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

                    End If

                End If

            End If

        Catch ex As Exception
            SplashScreenManager.CloseForm(False)
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub dgridTickets_Click(sender As Object, e As EventArgs) Handles dgridTickets.Click

    End Sub
End Class