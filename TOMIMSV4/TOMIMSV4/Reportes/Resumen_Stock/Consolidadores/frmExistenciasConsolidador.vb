Imports System.IO
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen

Public Class frmExistenciasConsolidador



    Public listaStock As New List(Of clsBeVW_stock_res)
    Public Property ProductoEspecifico As New clsBeProducto
    Private DTs As New DataTable("Dts")
    Private IsLoading As Boolean = False

    Public Property Modo As pModo
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetDatataTable()

        DTs.Columns.Add("IdProducto", GetType(Integer))
        DTs.Columns.Add("Código", GetType(String))
        DTs.Columns.Add("Código_Barra", GetType(String))
        DTs.Columns.Add("Propietario", GetType(String))
        DTs.Columns.Add("Producto", GetType(String))
        DTs.Columns.Add("UM_Bas", GetType(String))
        DTs.Columns.Add("Presentación", GetType(String))
        DTs.Columns.Add("CantidadUMBas", GetType(Double))
        DTs.Columns.Add("CantidadPresentación", GetType(Double))
        DTs.Columns.Add("Cantidad_Reservada", GetType(Double))
        DTs.Columns.Add("Disponible_UMBas", GetType(Double))
        DTs.Columns.Add("Disponible_Presentación", GetType(Double))

    End Sub

    Private Sub frmExistenciasConsolidador_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            IsLoading = True

            SetDatataTable()

            AP.Listar_Bodegas_By_Usuario(cmbBodega)
            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            IsLoading = False
        End Try

    End Sub


    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Cargar()
    End Sub

    Private Sub Cargar()

        Try

            If Not IsLoading Then
                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Obteniendo registros...")
            End If

            Dim DT As New DataTable("Dt")

            grdDetalleExistencia.DataSource = Nothing

            DT = clsLnStock.Get_All_Stock_por_Consolidador_Report(cmbBodega.EditValue, cmbPropietario.EditValue)

            If Not DT Is Nothing Then

                If ProductoEspecifico IsNot Nothing AndAlso ProductoEspecifico.IdProducto > 0 Then

                    Dim query =
                            From c In DT.AsEnumerable()
                            Where c.Field(Of String)("codigo") = (ProductoEspecifico.Codigo)

                    If query.Count > 0 Then
                        DT = query.CopyToDataTable
                    Else
                        DT.Clear()
                    End If

                End If

                If DT.Rows.Count > 0 Then

                    grdDetalleExistencia.DataSource = DT

                    If GridView1.RowCount > 0 Then

                        lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                        GridView1.BestFitColumns(True)

                        GridView1.OptionsView.ShowFooter = True

                        GridView1.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n6}"
                        GridView1.Columns("CantidadUMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("CantidadPresentacion").DisplayFormat.FormatString = "{0:n6}"
                        GridView1.Columns("CantidadPresentacion").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("CantidadPresentacion").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatString = "{0:n6}"
                        GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Cantidad_Reservada_UMBas").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"
                        GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                        GridView1.Columns("Disponible_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                        GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                        GridView1.Columns("Disponible_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GridView1.Columns("Disponible_Presentación").DisplayFormat.FormatString = "{0:n6}"
                        GridView1.Columns("Disponible_Presentación").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        GridView1.Columns("Disponible_Presentación").SummaryItem.DisplayFormat = "{0:n6}"

                    End If

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

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Close()
    End Sub


    Private Sub cmbBodega_EditValueChanged(sender As Object, e As EventArgs) Handles cmbBodega.EditValueChanged

        Try
            IMS.Listar_Propietarios_By_IdBodega(cmbPropietario, cmbBodega.EditValue)
            Cargar()
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try
    End Sub

    Private Sub linklblProducto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linklblProducto.LinkClicked

        Try

            Dim Rec As New frmProductoList() With {
                   .Modo = frmProductoList.pModo.Seleccion,
                   .StartPosition = FormStartPosition.CenterParent,
                   .WindowState = FormWindowState.Maximized}
            Rec.ShowDialog()

            grdDetalleExistencia.DataSource = Nothing

            If Rec.pObjProducto IsNot Nothing AndAlso Rec.pObjProducto.IdProducto <> 0 Then

                txtIdProducto.Text = Rec.pObjProducto.Codigo
                txtNombreProducto.Text = Rec.pObjProducto.Nombre
            End If

            Cargar()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub txtIdProducto_Validated(sender As Object, e As EventArgs) Handles txtIdProducto.Validated

        Try

            If String.IsNullOrEmpty(txtIdProducto.Text.Trim()) = False AndAlso txtIdProducto.Text > "0" Then

                ProductoEspecifico = clsLnProducto.Get_Single_By_Codigo(txtIdProducto.Text)

                If ProductoEspecifico IsNot Nothing AndAlso ProductoEspecifico.IdProducto > 0 Then
                    txtNombreProducto.Text = Trim(String.Format("{0}", ProductoEspecifico.Nombre))
                    Cargar()
                Else
                    XtraMessageBox.Show(String.Format("No existe producto con código {0}", txtIdProducto.Text.Trim()), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtIdProducto.Focus()
                    txtIdProducto.SelectAll()
                    ProductoEspecifico = Nothing
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub txtIdProducto_TextChanged(sender As Object, e As EventArgs) Handles txtIdProducto.TextChanged
        txtNombreProducto.Text = ""
        ProductoEspecifico = Nothing
        Cargar()
    End Sub

    Private Sub Exportar_Grid_A_Excel(ByRef dGrid As GridControl, ByVal NomArchivo As String)

        Try

            Try

                Dim myStream As Stream
                Dim saveFileDialog1 As New SaveFileDialog()

                saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
                saveFileDialog1.FilterIndex = 1
                saveFileDialog1.RestoreDirectory = True
                saveFileDialog1.FileName = NomArchivo

                If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                    myStream = saveFileDialog1.OpenFile()
                    If (myStream IsNot Nothing) Then
                        ' Code to write the stream goes here.
                        dGrid.ExportToXlsx(myStream)
                        myStream.Close()
                    End If
                End If

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub cmdExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExcel.ItemClick
        Exportar_Grid_A_Excel(grdDetalleExistencia, "WMS_ExistenciasConsolidador.xlsx")
    End Sub


End Class