Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen

Public Class frmIngresos_List

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Modo As pModo

    Enum pModo
        Lista = 1
        Seleccion = 2
    End Enum

    Public Property pBodega As clsBeBodega
    Public Property pIdPropietario As Integer
    Public Property Es_Seleccion_Multiple As Boolean
    Public Property ListaSeleccionMultiple As New List(Of clsBeTrans_oc_enc)
    '#GT28112024: guardar selección multiple, previo a aplicar filtros
    Private selectedKeys As New HashSet(Of Object)()



    Private Sub frmIngresos_List_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Dim DT As New DataTable

        Try

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Cargando ingresos...")

            selectedKeys.Clear()

            GridView1.OptionsSelection.MultiSelect = True
            GridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
            'selectedRows.Clear()

            If pBodega.Es_Bodega_Fiscal Then
                DT = clsLnTrans_oc_pol.Get_All_By_IdPropietarioBodega_And_IdBodega(pIdPropietario, pBodega.IdBodega)
            Else
                DT = clsLnTrans_oc_enc.Get_All_By_IdPropietarioBodega_And_IdBodega(pIdPropietario, pBodega.IdBodega)
            End If

            If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
                grdIngresos.DataSource = DT
            End If


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            SplashScreenManager.CloseForm(False)

        End Try

    End Sub

    Private Sub mnuTomarSeleccionados_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTomarSeleccionados.ItemClick
        Try

            Dim selectedRowHandles As Integer() = GridView1.GetSelectedRows()

            If selectedRowHandles.Length = 0 Then
                XtraMessageBox.Show("Seleccione al menos un registro",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Else

                Dim IdOrdenCompraEnc As Integer = 0
                Dim numero_orden As String = ""
                Dim Aplica As String = False

                SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Llenando lista...")

                ListaSeleccionMultiple = New List(Of clsBeTrans_oc_enc)

                For i As Integer = 0 To selectedRowHandles.Length - 1

                    Dim BeOCEnc = New clsBeTrans_oc_enc

                    IdOrdenCompraEnc = IIf(IsDBNull(GridView1.GetRowCellValue(selectedRowHandles(i), "IdOrdenCompraEnc")), 0, GridView1.GetRowCellValue(selectedRowHandles(i), "IdOrdenCompraEnc"))
                    numero_orden = IIf(IsDBNull(GridView1.GetRowCellValue(selectedRowHandles(i), "numero_orden")), 0, GridView1.GetRowCellValue(selectedRowHandles(i), "numero_orden"))


                    BeOCEnc = clsLnTrans_oc_enc.Get_Single_By_IdOrdenCompraEnc_And_IdBodega(IdOrdenCompraEnc, pBodega.IdBodega)

                    If BeOCEnc IsNot Nothing Then
                        ListaSeleccionMultiple.Add(BeOCEnc)
                    End If

                    SplashScreenManager.Default.SetWaitFormCaption("Ingreso: " & IdOrdenCompraEnc)
                    Application.DoEvents()

                Next

                If ListaSeleccionMultiple.Count > 1 Then
                    Es_Seleccion_Multiple = True
                Else
                    Es_Seleccion_Multiple = False
                End If

                DialogResult = DialogResult.OK

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub SaveSelectedRows()
        'selectedKeys.Clear()
        Dim selectedRowHandles As Integer() = GridView1.GetSelectedRows()

        For Each handle As Integer In selectedRowHandles
            If handle >= 0 Then
                Dim key As Object = GridView1.GetRowCellValue(handle, "IdOrdenCompraEnc") ' Cambia "ID" a tu columna clave única
                If key IsNot Nothing Then
                    selectedKeys.Add(key)
                End If
            End If
        Next
    End Sub

    Private Sub RestoreSelectedRows()
        GridView1.ClearSelection()

        For i As Integer = 0 To GridView1.RowCount - 1
            Dim key As Object = GridView1.GetRowCellValue(i, "IdOrdenCompraEnc") ' Cambia "ID" a tu columna clave única
            If key IsNot Nothing AndAlso selectedKeys.Contains(key) Then
                GridView1.SelectRow(i)
            End If
        Next
    End Sub

    Private Sub GridView1_ColumnFilterChanged(sender As Object, e As EventArgs) Handles GridView1.ColumnFilterChanged
        SaveSelectedRows()
        RestoreSelectedRows()
    End Sub

End Class