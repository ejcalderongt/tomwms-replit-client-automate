Imports System.IO
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid

Public Class frmStockRFID

    Private listaIngresoRFID As New List(Of clsBeI_nav_barras_rfid_enc)
    Private BeIngresoRFID As New clsBeI_nav_barras_rfid_enc

    Dim fechaInicial As Date
    Dim fechaFinal As Date

    Private Is_Loading As Boolean = False
    Private vNombreArchivoLayOutGrid As String = "frmStockRFID.xml"

    Private Sub cmdActualizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdActualizar.ItemClick
        Try

            fechaInicial = dtpFechaDel.Value.Date
            fechaFinal = dtpFechaAl.Value.Date

            Cargar_Lista_Ingresos(fechaInicial, fechaFinal)

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cargar_Lista_Ingresos(fechaInicial, fechaFinal)

        Dim listaIngresos As DataTable

        Try
            Is_Loading = True

            listaIngresos = clsLnI_nav_barras_rfid_enc.Get_Stock(fechaInicial, fechaFinal)

            If listaIngresos IsNot Nothing AndAlso listaIngresos.Rows.Count > 0 Then
                Dgrid.DataSource = listaIngresos
                Dgrid.ForceInitialize()

                Restore_LayOut()

                If GridView1.Columns.Count > 0 Then

                    If GridView1.Columns("Fecha") IsNot Nothing Then
                        GridView1.Columns("Fecha").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        GridView1.Columns("Fecha").DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
                    End If


                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.BestFitColumns()
                    lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)
                End If
            Else
                Dgrid.DataSource = Nothing
                lblRegs.Caption = String.Format("Registros: {0}", 0)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Is_Loading = False
        End Try

    End Sub

    Private Sub frmStockRFID_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try


            fechaInicial = dtpFechaDel.Value.Date
            fechaFinal = dtpFechaAl.Value.Date

            Cargar_Lista_Ingresos(fechaInicial, fechaFinal)

            'vNombreArchivoLayOutGrid = "frmStockRFID.xml"

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message,
         Text,
         MessageBoxButtons.OK,
         MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_Layout(sender As Object, e As EventArgs) Handles GridView1.Layout

        If Is_Loading Then Exit Sub

        Guardar_Layout()

    End Sub

    Private Sub Guardar_Layout()

        Try

            Dim Ms As New MemoryStream
            GridView1.SaveLayoutToStream(Ms)
            Ms.Seek(0, SeekOrigin.Begin)
            Dim MsReader As New StreamReader(Ms)
            Dim LayoutToText As String = MsReader.ReadToEnd()

            clsLnConfiguracion_usuario_enc.Guardar_Layout(AP.IdEmpresa,
                                                          AP.UsuarioAp.IdUsuario,
                                                          AP.HostName,
                                                          vNombreArchivoLayOutGrid,
                                                          LayoutToText)

            mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub Restore_LayOut()

        Try

            Dim BeConfiguracionUsuarioDet As New clsBeConfiguracion_usuario_det

            BeConfiguracionUsuarioDet = clsLnConfiguracion_usuario_enc.Get_Layout(AP.IdEmpresa,
                                                                                  AP.UsuarioAp.IdUsuario,
                                                                                  AP.HostName,
                                                                                  vNombreArchivoLayOutGrid)

            If Not BeConfiguracionUsuarioDet Is Nothing Then
                GridView1.RestoreLayoutFromStream(BeConfiguracionUsuarioDet.Stream_Template)
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                mnuEliminarLayoutGrid.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnuEliminarLayoutGrid_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEliminarLayoutGrid.ItemClick
        Try

            clsLnConfiguracion_usuario_enc.Delete_Layout(AP.IdEmpresa,
                                                         AP.UsuarioAp.IdUsuario,
                                                         AP.HostName,
                                                         vNombreArchivoLayOutGrid)


            XtraMessageBox.Show("Diseño de grid eliminado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                              Text,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try
    End Sub

    Private Sub cmdExportar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdExportar.ItemClick
        Exportar_Grid_A_Excel(Dgrid, "WMS_StockRFID.xlsx")
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

End Class