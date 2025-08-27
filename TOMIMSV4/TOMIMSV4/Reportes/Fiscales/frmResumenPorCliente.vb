Imports System.IO
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraSplashScreen

Public Class frmResumenPorCliente

    Private pBeCliente As clsBeCliente
    Private pClienteTiemposList As New List(Of clsBeProveedor_tiempos)

    Private ListaRetroactivo As New List(Of clsBeI_nav_transacciones_out)
    Private pListaMovimientos As New List(Of clsBeI_nav_transacciones_out)


    Private IsLoading As Boolean = False
    Public Property Modo As pModo
    Public Property ModoDepuracion As Boolean = False
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum



    Private Sub frmResumenPorCliente_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            AP.Listar_Bodegas_By_Usuario(cmbBodega)
            cmbBodega.EditValue = Integer.Parse(AP.IdBodega)

            'IMS.ListarRegimen(cmbRegimen)

            Listar_Propietarios()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Sub Listar_Propietarios()

        Try

            Dim DT1 As New DataTable
            DT1 = clsLnPropietario_bodega.Get_All_By_IdBodega_For_Combo(cmbBodega.EditValue)
            lcmbPropietario.Properties.DataSource = DT1
            lcmbPropietario.Properties.ValueMember = "IdPropietarioBodega"
            lcmbPropietario.Properties.DisplayMember = "Nombre"
            lcmbPropietario.Properties.PopupWidth = 700
            lcmbPropietario.Properties.BestFit()
            lcmbPropietario.Properties.PopulateColumns()
            lcmbPropietario.Properties.Columns(0).Visible = False
            lcmbPropietario.Properties.Columns(1).Visible = False
            lcmbPropietario.Properties.NullText = ""

            If DT1.Rows.Count = 1 Then
                lcmbPropietario.Text = DT1.Rows(0).Item("Nombre").ToString
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Sub fchDel_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDesde.ValueChanged

        Try

            If Me.dtpFechaDesde.Value > Me.dtpfechaHasta.Value Or Me.dtpfechaHasta.Value < Me.dtpFechaDesde.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                'Generar_Reporte()
                GridView1.Focus()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub fchAl_ValueChanged(sender As Object, e As EventArgs) Handles dtpfechaHasta.ValueChanged

        Try

            If Me.dtpFechaDesde.Value > Me.dtpfechaHasta.Value Or Me.dtpfechaHasta.Value < Me.dtpFechaDesde.Value Then
                Throw New Exception("Seleccione un rango de fechas válido.")
            Else
                'Generar_Reporte()
                GridView1.Focus()
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdActualizar_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdActualizar.ItemClick
        cmdActualizar.Enabled = False
        Generar_Reporte()
        cmdActualizar.Enabled = True

    End Sub

    Private Sub Generar_Reporte()

        Dim DT As New DataTable
        Dim DT2 As New DataTable 'para validar proceso de retroactivo
        grdExistenciasConLp.DataSource = Nothing

        Try

            If Not IsLoading Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            End If

            If lcmbPropietario.EditValue <> 0 Then

                DT = clsLnReportesFiscales.Get_Fiscal_historico_clientes(dtpFechaDesde.Value, dtpfechaHasta.Value, cmbBodega.EditValue, lcmbPropietario.EditValue)
            Else
                Throw New Exception("NO existe un propietario seleccionado.")
            End If


            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    grdExistenciasConLp.DataSource = DT

                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                    GridView1.OptionsView.ShowFooter = True


                    GridView1.Columns("CANTIDAD").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CANTIDAD").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("CANTIDAD").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("CANTIDAD").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("CIF").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CIF").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("CIF").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("CIF").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("DAI").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("DAI").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("DAI").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("DAI").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("IVA").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("IVA").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView1.Columns("IVA").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("IVA").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("TOTAL_VALOR").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("TOTAL_VALOR").DisplayFormat.FormatString = "{0:n6}"
                    GridView1.Columns("TOTAL_VALOR").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("TOTAL_VALOR").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.BestFitColumns(True)
                End If

            End If



        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not SplashScreenManager.Default Is Nothing Then
                If SplashScreenManager.Default.IsSplashFormVisible Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
        End Try

        Application.DoEvents()

    End Sub

    Private Sub cmdImpExcel_ItemClick(sender As Object, e As ItemClickEventArgs) Handles cmdImpExcel.ItemClick

        Try

            'dgrid.ExportToXlsx()

            Dim myStream As Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True
            saveFileDialog1.FileName = "VW_Resumen_x_Cliente_" & FormatoFechas.tFecha(dtpFechaDesde.Value) & "_Al_" & FormatoFechas.tFecha(dtpfechaHasta.Value) & ".xlsx"

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                myStream = saveFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    grdExistenciasConLp.ExportToXlsx(myStream)
                    myStream.Close()
                End If
            End If

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged
        Listar_Propietarios()
    End Sub

End Class