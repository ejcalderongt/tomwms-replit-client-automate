Imports System.Reflection
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid

Public Class frmColumViewervb

    Public Property IdBodega As Integer

    Public BeBodegaUbicacion As New clsBeBodega_ubicacion()

    Private Sub picRack_MouseWheel(sender As Object, e As MouseEventArgs) Handles picRack.MouseWheel

        Try

            If e.Delta <> 0 Then
                If e.Delta <= 0 Then
                    If picRack.Width < 500 Then Exit Sub 'minimum 500?
                Else
                    If picRack.Width > 2000 Then Exit Sub 'maximum 2000?
                End If

                picRack.Width += CInt(picRack.Width * e.Delta / 1000)
                picRack.Height += CInt(picRack.Height * e.Delta / 1000)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub cmdDrawText_Click(sender As Object, e As EventArgs) Handles cmdDrawText.Click

        Try

            Dim gr As Graphics = Graphics.FromImage(picRack.Image)
            gr.DrawString(txtTexto.Text.Trim,
                          New Font("Tahoma", 14),
                          New SolidBrush(Color.Green),
                          txtX.Value, txtY.Value)
            gr.Dispose()

            picRack.Refresh()

            ' MsgBox("Done", MsgBoxStyle.Information, Text)

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Private Sub frmColumViewervb_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            'Get the X and Y ratio each time an image is loaded (If the images are different sizes)
            'Otherwise if the images are all the same size then you only need to calculate them once.
            RatioX = picRack.Image.Width / picRack.ClientSize.Width
            RatioY = picRack.Image.Height / picRack.ClientSize.Height

            Load_Info_Sector()

        Catch ex As Exception
            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)
        End Try

    End Sub

    Dim RatioX As Double = 0
    Dim RatioY As Double = 0

    Private Sub PicRack_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picRack.MouseMove
        Dim mp As New Point((e.X * RatioX), (e.Y * RatioY))
        lblCordinates.Text = mp.ToString
    End Sub

    Private Sub picRack_Click(sender As Object, e As EventArgs) Handles picRack.Click

        Try

            listPOints.Items.Add(lblCordinates.Text)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub cmdDrawPallet_Click(sender As Object, e As EventArgs) Handles cmdDrawPallet.Click

        Try

            Dim gr As Graphics = Graphics.FromImage(picRack.Image)
            'Dim P As Pen = New Pen(Color.Red)
            'gr.DrawLine(P, 138, 180, 477, 180)
            'gr.Dispose()

            'picRack.Refresh()

            'MsgBox("Done", MsgBoxStyle.Information, Text)

            Dim img As New Bitmap(My.Resources.Single_Pallet_Box_Fit)
            Dim myImage As Image = DirectCast(img.Clone(), Image)

            gr.DrawImage(myImage, txtX.Value, txtY.Value)
            gr.Dispose()

            picRack.Refresh()

            'MsgBox("Done", MsgBoxStyle.Information, Text)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        picRack.Image = Nothing
        picRack.Image = My.Resources.SingleColumn_4Levels_Empty_Fit
        picRack.Refresh()

    End Sub

    Private Sub cmdZoomIn_Click(sender As Object, e As EventArgs)
        picRack.Image = ZoomImage(picRack.Image, Val(txtTexto.Text))
    End Sub

    'To resize the image with a new width and height
    Public Function ZoomImage(ByVal Image As Image, ByVal NewSize As Size) As Image
        Return New Bitmap(Image, NewSize.Width, NewSize.Height)
    End Function

    'Or, if you want to zoom it by a factor
    Public Function ZoomImage(ByVal Image As Image, ByVal ZoomFactor As Double) As Image
        Return New Bitmap(Image, Int(Image.Width * ZoomFactor), Int(Image.Height * ZoomFactor))
    End Function

    Private Sub Load_Info_Sector()

        Try

            If IMS.Listar_Areas_By_Bodega(cmbAreasR, IdBodega) Then
                If Not IMS.Listar_Sectores_By_Area(cmbSector, cmbAreasR.EditValue, IdBodega) Then
                    cmbSector.Properties.DataSource = Nothing
                End If
            End If

            Dim Obj As New clsBeBodega_tramo
            Obj = BeBodegaUbicacion.Tramo
            BeBodegaUbicacion.Tramo.IdBodega = IdBodega

            BeBodegaUbicacion.Tramo = clsLnBodega_tramo.Get_Single_By_IdTramo_And_IdBodega(Obj.IdTramo, IdBodega)

            cmbAreasR.EditValue = clsLnBodega_area.Get_IdArea_By_IdTramo_And_IdBodega(Obj.IdTramo, IdBodega)

            Dim lInfoColumna As New List(Of clsBeBodega_tramo)

            lInfoColumna = clsLnBodega_ubicacion.Get_Column_Info_By_IdTramo_And_IdBodega(Obj.IdTramo, BeBodegaUbicacion.Indice_x, IdBodega)

            If Not lInfoColumna Is Nothing Then

                txtNiveles.Value = lInfoColumna.Count() 'Los niveles es el conteo de todos los niveles.
                txtAlto.Value = (From p In lInfoColumna).Sum(Function(x) x.Alto) 'El alto es la suma de todos los niveles.
                txtAncho.Value = lInfoColumna(0).Ancho 'El ancho debería ser el mismo para toda la columan, no he visto racks tetricos.

            End If

            txtCodigoTramo.Text = BeBodegaUbicacion.Tramo.Codigo
            txtDescripcionTramo.Text = BeBodegaUbicacion.Tramo.Descripcion
            chkSistemaTramo.Checked = Obj.Sistema
            chkEsRack.Checked = Obj.Es_Rack
            chkActivoTramo.Checked = Obj.Activo
            txtTexto.Text = "Pallet Pos " & BeBodegaUbicacion.Descripcion
            cmdDrawPallet_Click(cmdDrawPallet, Nothing)
            cmdDrawText_Click(cmdDrawText, Nothing)

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub frmColumViewervb_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            Cargar_Datos()

        Catch ex As Exception
            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
          Text,
          MessageBoxButtons.OK,
          MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Cargar_Datos()


        Try

            Dim DT As New DataTable

            DT.Clear()

            DgridStock.DataSource = Nothing

            DT = clsLnStock.Get_Reporte_Stock_By_IdBodega_And_IdUbicacion(IdBodega, BeBodegaUbicacion.IdUbicacion, True)

            If Not DT Is Nothing Then

                If DT.Rows.Count > 0 Then

                    DgridStock.DataSource = DT

                    gridview1.OptionsView.ShowFooter = True

                    gridview1.Columns("Cantidad_Reservada_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    gridview1.Columns("Cantidad_Reservada_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    gridview1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gridview1.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gridview1.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                    gridview1.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                    gridview1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gridview1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"

                    gridview1.Columns("Peso").SummaryItem.SummaryType = SummaryItemType.Sum
                    gridview1.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"
                    gridview1.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gridview1.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                    gridview1.Columns("CantidadUMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    gridview1.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    gridview1.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gridview1.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n6}"

                    gridview1.Columns("CantidadPresentacion").SummaryItem.SummaryType = SummaryItemType.Sum
                    gridview1.Columns("CantidadPresentacion").SummaryItem.DisplayFormat = "{0:n6}"
                    gridview1.Columns("CantidadPresentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gridview1.Columns("CantidadPresentacion").DisplayFormat.FormatString = "{0:n6}"

                    gridview1.Columns("Disponible_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    gridview1.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    gridview1.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gridview1.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gridview1.Columns("Disponible_Presentación").SummaryItem.SummaryType = SummaryItemType.Sum
                    gridview1.Columns("Disponible_Presentación").SummaryItem.DisplayFormat = "{0:n6}"
                    gridview1.Columns("Disponible_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gridview1.Columns("Disponible_Presentación").DisplayFormat.FormatString = "{0:n6}"

                    gridview1.Columns("Cant_Pickeada_Presentacion").SummaryItem.SummaryType = SummaryItemType.Sum
                    gridview1.Columns("Cant_Pickeada_Presentacion").SummaryItem.DisplayFormat = "{0:n6}"
                    gridview1.Columns("Cant_Pickeada_Presentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gridview1.Columns("Cant_Pickeada_Presentacion").DisplayFormat.FormatString = "{0:n6}"

                    gridview1.Columns("Cant_No_Pickeada_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    gridview1.Columns("Cant_No_Pickeada_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    gridview1.Columns("Cant_No_Pickeada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    gridview1.Columns("Cant_No_Pickeada_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    gridview1.Columns("Fecha_Ingreso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    gridview1.Columns("Fecha_Ingreso").DisplayFormat.FormatString = "G"

                    Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Reservada_UMBas", .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = gridview1.Columns("Cantidad_Reservada")}
                    gridview1.GroupSummary.Add(item)

                    Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_UMBas", .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = gridview1.Columns("Cantidad_UMBas")}
                    gridview1.GroupSummary.Add(item1)

                    Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Presentacion", .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = gridview1.Columns("Cantidad_Presentacion")}
                    gridview1.GroupSummary.Add(item2)

                    Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Disponible_UMBas", .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = gridview1.Columns("Disponible_UMBas")}
                    gridview1.GroupSummary.Add(item3)


                    Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Disponible_Presentación", .SummaryType = SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = gridview1.Columns("Disponible_Presentación")}
                    gridview1.GroupSummary.Add(item4)

                    gridview1.Columns("IdPresentacion").Visible = False

                    gridview1.ExpandAllGroups()

                    gridview1.BestFitColumns()

                End If

            End If

            'Set_LayOut_Grid()

        Catch ex As Exception

            XtraMessageBox.Show(String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message),
                                Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        Finally
            'IsLoading = False
        End Try

    End Sub

End Class