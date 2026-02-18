Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmCapturaParametroPicking

    Private ListObjP As New List(Of clsBeProducto_parametros)

    Public pIndex As Integer
    Public pIdPickingDet As Integer
    Public pObjP As clsBeProducto
    Public pListObjDP As List(Of clsBeTrans_picking_det_parametros)

    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub CargaParametros()

        Try

            ListObjP = clsLnProducto_parametros.Get_All_By_IdProducto(pObjP.IdProducto, True)

            Grid.BeginUpdate()

            If pIndex >= 0 Then

                If ListObjP.Count > 0 Then

                    For Each Obj As clsBeTrans_picking_det_parametros In pListObjDP.FindAll(Function(b) b.IdPickingDet = pIdPickingDet)

                        Dim ObjPR As clsBeProducto_parametros = ListObjP.Find(Function(b) b.IdProductoParametro = Obj.IdProductoParametro)

                        If ObjPR IsNot Nothing Then

                            Dim lRow As DataRow = Nothing
                            lRow = DSPR.DT.NewRow

                            If Not String.IsNullOrEmpty(Obj.Valor_texto) Then
                                lRow.Item("colTexto") = Obj.Valor_texto
                            ElseIf Obj.Valor_numerico.HasValue Then
                                lRow.Item("colNumerico") = Obj.Valor_numerico
                            ElseIf Obj.Valor_fecha.HasValue Then
                                lRow.Item("colFecha") = Obj.Valor_fecha
                            ElseIf Obj.Valor_logico.HasValue Then
                                lRow.Item("colLogico") = Obj.Valor_logico
                            End If

                            If ObjPR.IdParametro > 0 Then
                                lRow.Item("IdParametro") = ObjPR.IdParametro
                            End If

                            If ObjPR.TipoParametro IsNot Nothing Then
                                lRow.Item("colDescripcion") = ObjPR.TipoParametro.Descripcion
                            End If

                            lRow.Item("IdParametroDet") = Obj.IdParametroPicking
                            DSPR.DT.AddDTRow(lRow)

                        End If

                    Next

                End If

            Else

                If ListObjP.Count > 0 Then

                    For Each Obj As clsBeProducto_parametros In ListObjP

                        Obj.IsNew = True
                        Dim lRow As DataRow = Nothing
                        lRow = DSPR.DT.NewRow

                        If Not String.IsNullOrEmpty(Obj.Valor_texto) Then
                            lRow.Item("colTexto") = Obj.Valor_texto
                        ElseIf Not Obj.Valor_numerico = 0 Then
                            lRow.Item("colNumerico") = Obj.Valor_numerico
                        ElseIf Not Obj.Valor_fecha = New Date(1900, 1, 1) Then
                            lRow.Item("colFecha") = Obj.Valor_fecha
                        End If

                        lRow.Item("colLogico") = Obj.Valor_logico
                        lRow.Item("IdParametro") = Obj.IdParametro
                        lRow.Item("colDescripcion") = Obj.TipoParametro.Descripcion

                        DSPR.DT.AddDTRow(lRow)

                    Next

                End If

            End If


            Grid.EndUpdate()
            Grid.ForceInitialize()
            Dim ritem As RepositoryItemCheckEdit = TryCast(GrdP.Columns("colLogico").RealColumnEdit, RepositoryItemCheckEdit)
            AddHandler ritem.CheckedChanged, AddressOf ritem_CheckedChanged


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ritem_CheckedChanged(sender As Object, e As EventArgs)

        Try

            Dim ritem As DevExpress.XtraEditors.CheckEdit = TryCast(sender, DevExpress.XtraEditors.CheckEdit)
            If ritem IsNot Nothing Then
                Dim Dr As DataRowView = GrdP.GetFocusedRow
                Dim lIndex As Integer = -1
                lIndex = ListObjP.FindIndex(Function(b) b.IdParametro = CInt(Dr.Item("IdParametro")))
                If lIndex > -1 Then
                    If ritem.Checked Then
                        ListObjP(lIndex).Valor_logico = True
                    Else
                        ListObjP(lIndex).Valor_logico = False
                    End If
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


    Private Sub frmCapturaParametroRecepcion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            CargaParametros()
            Me.CenterToScreen()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub



    Private Sub GrdP_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GrdP.CellValueChanged

        Try

            If GrdP.RowCount > 0 Then

                Dim Dr As DataRowView = GrdP.GetFocusedRow
                Dim lIndex As Integer = -1

                If pIndex >= 0 Then

                    If pListObjDP IsNot Nothing AndAlso pListObjDP.Count > 0 Then

                        Dim val As Object = Nothing
                        If e.Column.Caption = "Texto" Then
                            val = Dr.Item("colTexto")
                        ElseIf e.Column.Caption = "Númerico" Then
                            val = Dr.Item("colNumerico")
                        ElseIf e.Column.Caption = "Fecha" Then
                            val = Dr.Item("colFecha")
                        ElseIf e.Column.Caption = "Lógico" Then
                            val = Dr.Item("colLogico")
                        End If

                        lIndex = pListObjDP.FindIndex(Function(p) p.IdParametroPicking = CInt(Dr.Item("IdParametroDet")))

                        If lIndex > -1 Then

                            If String.IsNullOrEmpty(pListObjDP(lIndex).Valor_texto) = False Then
                                pListObjDP(lIndex).Valor_texto = val.ToString
                            ElseIf pListObjDP(lIndex).Valor_numerico.HasValue Then
                                pListObjDP(lIndex).Valor_numerico = CDbl(val)
                            ElseIf pListObjDP(lIndex).Valor_fecha.HasValue Then
                                pListObjDP(lIndex).Valor_fecha = CDate(val)
                            ElseIf pListObjDP(lIndex).Valor_logico.HasValue Then
                                pListObjDP(lIndex).Valor_logico = CBool(val)
                            End If

                        End If

                    End If

                Else

                    If ListObjP IsNot Nothing AndAlso ListObjP.Count > 0 Then

                        Dim val As Object = Nothing
                        If e.Column.Caption = "Texto" Then
                            val = Dr.Item("colTexto")
                        ElseIf e.Column.Caption = "Númerico" Then
                            val = Dr.Item("colNumerico")
                        ElseIf e.Column.Caption = "Fecha" Then
                            val = Dr.Item("colFecha")
                        ElseIf e.Column.Caption = "Lógico" Then
                            val = Dr.Item("colLogico")
                        End If

                        lIndex = ListObjP.FindIndex(Function(p) p.IdParametro = CInt(Dr.Item("IdParametro")))

                        If lIndex > -1 Then

                            If String.IsNullOrEmpty(ListObjP(lIndex).Valor_texto) = False Then
                                ListObjP(lIndex).Valor_texto = val.ToString
                            ElseIf ListObjP(lIndex).Valor_numerico <> 0 Then
                                ListObjP(lIndex).Valor_numerico = CDbl(val)
                            ElseIf Not ListObjP(lIndex).Valor_fecha = New Date(1900, 1, 1) Then
                                ListObjP(lIndex).Valor_fecha = CDate(val)
                            End If

                            ListObjP(lIndex).Valor_logico = CBool(val)

                        End If

                    End If

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


    Private Function celdaTextoNula(ByVal row As Integer) As Boolean

        Try
            Dim col As GridColumn = GrdP.Columns("colTexto")
            Dim val = Convert.ToString(GrdP.GetRowCellValue(row, col))

            If val = String.Empty Then
                Return True
            End If
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try





    End Function


    Private Function celdaNumericaNula(ByVal row As Integer) As Boolean

        Try
            Dim col As GridColumn = GrdP.Columns("colNumerico")
            Dim val = Convert.ToString(GrdP.GetRowCellValue(row, col))

            If val = String.Empty Then
                Return True
            End If
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try





    End Function


    Private Function celdaFechaNula(ByVal row As Integer) As Boolean

        Try
            Dim col As GridColumn = GrdP.Columns("colFecha")
            Dim val = Convert.ToString(GrdP.GetRowCellValue(row, col))

            If val = String.Empty Then
                Return True
            End If
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try





    End Function


    Private Function celdaLogicaNula(ByVal row As Integer) As Boolean

        Try
            Dim col As GridColumn = GrdP.Columns("colLogico")
            Dim val = Convert.ToString(GrdP.GetRowCellValue(row, col))

            If val = String.Empty Then
                Return True
            End If
        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
            Throw ex
        End Try





    End Function


    Private Sub GrdP_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GrdP.ShowingEditor

        Try

            If GrdP.FocusedColumn.FieldName = "colTexto" AndAlso celdaTextoNula(GrdP.FocusedRowHandle) Then
                e.Cancel = True
            ElseIf GrdP.FocusedColumn.FieldName = "colNumerico" AndAlso celdaNumericaNula(GrdP.FocusedRowHandle) Then
                e.Cancel = True
            ElseIf GrdP.FocusedColumn.FieldName = "colFecha" AndAlso celdaFechaNula(GrdP.FocusedRowHandle) Then
                e.Cancel = True
            ElseIf GrdP.FocusedColumn.FieldName = "colLogico" AndAlso celdaLogicaNula(GrdP.FocusedRowHandle) Then
                e.Cancel = True
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


    Private Sub cmdAcept_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdAcept.ItemClick

        Try

            For Each Obj As clsBeProducto_parametros In ListObjP

                Dim ObjDP As New clsBeTrans_picking_det_parametros()

                ObjDP.IdPickingDet = pIdPickingDet
                ObjDP.IdProductoParametro = Obj.IdProductoParametro
                ObjDP.Valor_texto = Obj.Valor_texto.Trim()
                ObjDP.Valor_logico = Obj.Valor_logico
                ObjDP.Valor_fecha = Obj.Valor_fecha
                ObjDP.Valor_numerico = Obj.Valor_numerico
                ObjDP.User_agr = AP.UsuarioAp.IdUsuario
                ObjDP.Fec_agr = Now
                ObjDP.IsNew = Obj.IsNew

                If Obj.IsNew Then
                    pListObjDP.Add(ObjDP)
                End If

            Next

            Close()

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub


    Private Sub cmdCancel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdCancel.ItemClick
        If XtraMessageBox.Show("¿Desea Cancelar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Close()
        End If
    End Sub


    Private Sub GrdP_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GrdP.RowStyle

        Try

            GrdP.OptionsBehavior.Editable = True
            GrdP.OptionsSelection.EnableAppearanceFocusedCell = True

            GrdP.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

            GrdP.OptionsSelection.EnableAppearanceFocusedRow = True
            GrdP.OptionsSelection.EnableAppearanceHideSelection = True
            GrdP.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")
            GrdP.Appearance.SelectedRow.BackColor = ColorTranslator.FromHtml("#CD5E00")

            GrdP.Appearance.FocusedRow.ForeColor = Color.White
            GrdP.Appearance.SelectedRow.ForeColor = Color.White

            GrdP.Appearance.SelectedRow.Options.UseBackColor = True
            GrdP.Appearance.SelectedRow.Options.UseForeColor = True

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

End Class