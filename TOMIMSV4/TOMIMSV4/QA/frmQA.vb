Imports System.Data.SqlClient
Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Public Class frmQA

    Property Nombre_Caso As String = "Resultado"
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol

    Sub New()

        InitializeComponent()

    End Sub

    Private Sub mnuCaso1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso1.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_1_IDEAL_20231002011101)

            Nombre_Caso = "CASO_1_IDEAL_20231002011101"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_1_IDEAL_20231002011101(AP.IdBodega, AP.UsuarioAp.IdUsuario, vResult)

            Actualizar_Progreso(vResult)

        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try
    End Sub

    Private Sub mnuCaso2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso2.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_2_IDEAL_20231002011120)

            Nombre_Caso = "CASO_2_IDEAL_20231002011120"

            xtcQAReservas.TabPages(1).Text = Nombre_Caso

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_2_IDEAL_20231002011120(AP.IdBodega, AP.UsuarioAp.IdUsuario, vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso3.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_3_IDEAL_20231002011128)

            Nombre_Caso = "CASO_3_IDEAL_20231002011128"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_3_IDEAL_20231002011128(AP.IdBodega,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  vResult)
            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso4.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_4_IDEAL_20231002011132)

            Nombre_Caso = "CASO_4_IDEAL_20231002011132"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_4_IDEAL_20231002011132(AP.IdBodega,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso5.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_5_IDEAL_20231002011134)

            Nombre_Caso = "CASO_5_IDEAL_20231002011134"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_5_IDEAL_20231002011134(AP.IdBodega,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso6.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_6_IDEAL_20231002011136)

            Nombre_Caso = "CASO_6_IDEAL_20231002011136"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_6_IDEAL_20231002011136(AP.IdBodega,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try
    End Sub

    Private Sub mnuCaso7_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso7.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_7_IDEAL_20231002011140)

            Nombre_Caso = "CASO_7_IDEAL_20231002011140"

            Dim vResult As String = ""

            clsLnStock_res.Ejecuta_QA_CASO_7_IDEAL_20231002011140(AP.IdBodega,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso8_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso8.ItemClick

        lblprg.Visible = True

        Try

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_8_IDEAL_20231002011142)

            Nombre_Caso = "CASO_8_IDEAL_20231002011142"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_8_IDEAL_20231002011140(AP.IdBodega,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  vResult)
            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub


    Private Sub mnuCaso9_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso9.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_9_IDEAL_20231002011144)

            Nombre_Caso = "CASO_9_IDEAL_20231002011144"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_9_IDEAL_20231002011144(AP.IdBodega,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso10_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso10.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_10_IDEAL_20231002011146)

            Nombre_Caso = "CASO_10_IDEAL_20231002011146"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_10_IDEAL_20231002011146(AP.IdBodega,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso11_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso11.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_11_IDEAL_20231002011153)

            Nombre_Caso = "CASO_11_IDEAL_20231002011153"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_11_IDEAL_20231002011153(AP.IdBodega,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso12_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso12.ItemClick, mnuCaso12.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_12_IDEAL_20231002011159)

            Nombre_Caso = "CASO_12_IDEAL_20231002011159"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_12_IDEAL_20231002011159(AP.IdBodega,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  vResult)
            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try
    End Sub

    Private Sub mnuCaso13_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso13.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_13_IDEAL_20231002011201)

            Nombre_Caso = "CASO_13_IDEAL_20231002011201"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_13_IDEAL_20231002011201(AP.IdBodega,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso14_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso14.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_14_IDEAL_20231002011201)

            Nombre_Caso = "CASO_14_IDEAL_20231002011201"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_14_IDEAL_20231002011201(AP.IdBodega,
                                                                  AP.UsuarioAp.IdUsuario,
                                                                  vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub Actualiza_Grids()

        Try

            xtcQAReservas.TabPages(1).Show()

            xtcQAReservas.TabPages(1).Text = Nombre_Caso

            Listar_Stock_By_IdBodega()

            Listar_Pedidos()

            Listar_Stock_Reservado()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Listar_Stock_By_IdBodega()

        Try

            Dim DT As New DataTable

            DT.Clear()

            dgridStock.DataSource = Nothing

            Dim vIdBodega = Integer.Parse(AP.IdBodega)
            Dim vIdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(AP.IdBodega, 1)

            '#CKFK20231206 Agregué orden a la columnas creando un nuevo método
            DT = clsLnStock.Get_Reporte_Stock_QA(vIdBodega, vIdPropietarioBodega)

            If Not DT Is Nothing Then


                If DT.Rows.Count > 0 Then

                    dgridStock.DataSource = DT

                    'lblRegs.Caption = String.Format("Registros: {0}", GridView3.RowCount)

                    GridView3.OptionsView.ShowFooter = True

                    GridView3.Columns("Cantidad_Reservada_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("Cantidad_Reservada_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("Peso").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("CantidadUMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("CantidadPresentacion").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("CantidadPresentacion").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("CantidadPresentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("CantidadPresentacion").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("Disponible_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("Disponible_Presentación").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("Disponible_Presentación").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("Disponible_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("Disponible_Presentación").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("Fecha_Ingreso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    GridView3.Columns("Fecha_Ingreso").DisplayFormat.FormatString = "G"

                    Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Reservada_UMBas", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView3.Columns("Cantidad_Reservada")}
                    GridView3.GroupSummary.Add(item)

                    Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_UMBas", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView3.Columns("Cantidad_UMBas")}
                    GridView3.GroupSummary.Add(item1)

                    Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Presentacion", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView3.Columns("Cantidad_Presentacion")}
                    GridView3.GroupSummary.Add(item2)

                    Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Disponible_UMBas", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView3.Columns("Disponible_UMBas")}
                    GridView3.GroupSummary.Add(item3)


                    Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Disponible_Presentación", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView3.Columns("Disponible_Presentación")}
                    GridView3.GroupSummary.Add(item4)

                    GridView3.Columns("IdPresentacion").Visible = False

                    GridView3.ExpandAllGroups()

                    GridView3.BestFitColumns()

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

    Private Sub Listar_Stock_Reservado()

        Dim DT As New DataTable

        Try

            DT.Clear() : dgridStockReservado.DataSource = Nothing

            '#CKFK20231206 Agregué orden a la columnas creando un nuevo método
            DT = clsLnVW_Stock_Res_Pedido.Get_All_By_IdBodega_DT_QA(AP.IdBodega)

            Dim vTotalReservado As Double = 0
            Dim vTotalDisponible As Double = 0

            If DT.Rows.Count > 0 Then

                dgridStockReservado.DataSource = DT

                If GridView1.RowCount > 0 Then

                    GridView1.OptionsView.ShowFooter = True
                    GridView1.OptionsView.ColumnAutoWidth = False

                    GridView1.Columns("CantidadFisica").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("CantidadFisica").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("CantidadFisica").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CantidadFisica").DisplayFormat.FormatString = "{0:n6}"


                    GridView1.Columns("Factor").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Factor").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("cantidad_presentacion_reservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("cantidad_presentacion_reservada").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("cantidad_presentacion_reservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("cantidad_presentacion_reservada").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("cantidad_umbas_reservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("cantidad_umbas_reservada").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("cantidad_umbas_reservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("cantidad_umbas_reservada").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("fec_agr").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    GridView1.Columns("fec_agr").DisplayFormat.FormatString = "g"

                    GridView1.BestFitColumns(True)

                    vTotalReservado = DT.AsEnumerable.Sum(Function(x) x.Item("cantidad_umbas_reservada"))
                    vTotalDisponible = DT.AsEnumerable.Sum(Function(x) x.Item("CantidadFisica"))

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

    Public Sub Listar_Pedidos()

        Try

            dgridPedido.DataSource = Nothing

            dgridPedido.BeginUpdate()

            Dim Dt As New DataTable

            Dt = clsLnTrans_pe_enc.Get_All_Pedido_By_IdBodega_DT(AP.IdBodega)

            dgridPedido.DataSource = Dt

            If GridView2.Columns.Count > 1 Then
                GridView2.Columns("fec_agr").DisplayFormat.FormatType = FormatType.DateTime
                GridView2.Columns("fec_agr").DisplayFormat.FormatString = "G"
                GridView2.Columns("fec_agr").Caption = "Fecha_Agregado"
                GridView2.Columns("anulado").Caption = "Anulado"
                GridView2.Columns("activo").Caption = "Activo"
                GridView2.Columns("no_documento").Caption = "No_Documento"
                GridView2.Columns("referencia").Caption = "Referencia"
                GridView2.Columns("IdBodega").Visible = False
            End If

            Try

                GridView2.OptionsView.ColumnAutoWidth = False

                If GridView2.Columns.Count > 0 Then
                    If GridView2.RowCount > 0 Then
                        GridView2.BestFitColumns()
                    End If
                End If

            Catch ex As Exception
            End Try

            dgridPedido.EndUpdate()

            GridView2.LayoutChanged()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuImportarInventario_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuImportarInventario.ItemClick

        Try

            Dim InvImp As New frmInventarioImport
            InvImp.IdInventario = clsLnTrans_inv_enc.MaxID
            InvImp.DobleVerificacion = False
            InvImp.InvTeorico_Multi_Propietario = False
            InvImp.TipoInventario = 1
            InvImp.IdPropietarioBodega = clsLnPropietario_bodega.Get_IdPropietarioBodega_By_IdBodega(AP.IdBodega)
            Dim gBeTransInvEnc As New clsBeTrans_inv_enc
            Dim pIdPropietario As Integer = clsLnPropietario_bodega.Get_IdPropietario_By_IdBodega_IdPropietarioBodega(AP.IdBodega, InvImp.IdPropietarioBodega)

            gBeTransInvEnc.IsNew = True

#Region "Encabezado inventario"

            If gBeTransInvEnc.IsNew Then

                gBeTransInvEnc.Idpropietario = pIdPropietario
                gBeTransInvEnc.IdBodega = AP.IdBodega
                gBeTransInvEnc.IdTipoInventario = InvImp.TipoInventario
                gBeTransInvEnc.Tipo_Conteo_Producto = 0
                gBeTransInvEnc.Fecha = Now
                gBeTransInvEnc.Fecha_Ultimo_Inventario = Now
                gBeTransInvEnc.Estado = "Nuevo"

                gBeTransInvEnc.Inicial = True
                gBeTransInvEnc.Doble_verificacion = False
                gBeTransInvEnc.EsSistema = False
                gBeTransInvEnc.Mostrar_Cantidad_Teorica_hh = False
                gBeTransInvEnc.Cambia_Ubicacion = False

                gBeTransInvEnc.Capturar_no_existente = False
                gBeTransInvEnc.Activo = True
                gBeTransInvEnc.Regularizado = False
                gBeTransInvEnc.Hora_ini = Now
                gBeTransInvEnc.Hora_fin = Now
                gBeTransInvEnc.User_agr = AP.UsuarioAp.Nombres
                gBeTransInvEnc.Fec_agr = Now
                gBeTransInvEnc.User_mod = AP.UsuarioAp.Nombres
                gBeTransInvEnc.Fec_mod = Now
                gBeTransInvEnc.multi_propietario = False

                clsLnTrans_inv_enc.Guardar(gBeTransInvEnc)

            End If

#End Region

            InvImp.ShowDialog()
            InvImp.Dispose()

            If gBeTransInvEnc IsNot Nothing Then
                clsLnTrans_inv_enc.Regularizar_Inventario(gBeTransInvEnc.Idinventarioenc, AP.IdEmpresa)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Configuracion_qaBindingNavigatorSaveItem_Click_1(sender As Object, e As EventArgs) Handles Configuracion_qaBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.Configuracion_qaBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.Configuracion_QADataSet)

    End Sub

    Private Sub frmQA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Llenar_Grid_Configuracion()
    End Sub

    Private Sub Llenar_Grid_Configuracion()

        Try

            Dim lConnection As New SqlConnection(clsBD.Instancia.CadenaConexionSQLClient)
            Me.Configuracion_qaTableAdapter.Connection = lConnection
            Me.Configuracion_qaTableAdapter.Fill(Me.Configuracion_QADataSet.configuracion_qa)

            InitLookUpBodega()
            InitLookUpEmpresa()
            InitLookUpPropietario()
            InitLookUpCliente()
            InitLookUpProducto()

        Catch ex As Exception

        End Try

    End Sub
    Private Sub ProductoGridLookUpEdit_Leave(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim lista As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
            If lista.EditValue Is Nothing Then Return
            Dim drLineaGrid As DataRow = gvConfiguracion.GetFocusedDataRow()
            If drLineaGrid Is Nothing Then Return

            Dim vObjProducto As Object = lista.Properties.GetRowByKeyValue(lista.EditValue)

            If Not vObjProducto Is Nothing Then

                Dim drArticulo As DataRow = (TryCast(lista.Properties.GetRowByKeyValue(lista.EditValue), DataRowView)).Row
                If drArticulo Is Nothing Then Return

                Dim vBeProducto As New clsBeProducto
                vBeProducto = clsLnProducto.Get_Single_By_CodigoProducto(drArticulo("Codigo"))

                Dim vIdProductoBodega As Integer = drArticulo("IdProductoBodega")
                Dim vIdProducto As Integer = drArticulo("IdProducto")

                dgridConfiguracion.BeginInvoke(New MethodInvoker(Sub()
                                                                     gvConfiguracion.FocusedRowHandle = GridControl.AutoFilterRowHandle
                                                                     gvConfiguracion.FocusedColumn = gvConfiguracion.Columns("Cantidad_Pedido_Presentacion")
                                                                     gvConfiguracion.ShowEditor()
                                                                 End Sub))

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub InitLookUpBodega()

        Try

            Dim DTBodegas As New DataTable
            DTBodegas = clsLnBodega.Get_All_By_IdEmpresa_And_IdUsuario_DT_QA(AP.IdEmpresa, AP.UsuarioAp.IdUsuario)

            Dim BodegaGridLookUpEdit As New RepositoryItemGridLookUpEdit
            BodegaGridLookUpEdit.View.Columns.Clear()
            BodegaGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdBodega", .Caption = "IdBodega", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Código", .Visible = True}
               })

            BodegaGridLookUpEdit.ValueMember = "IdBodega"
            BodegaGridLookUpEdit.DisplayMember = "Codigo"
            BodegaGridLookUpEdit.NullText = "-> Bodega"
            BodegaGridLookUpEdit.PopupFormWidth = 700
            BodegaGridLookUpEdit.DataSource = DTBodegas
            BodegaGridLookUpEdit.View.BestFitColumns()

            gvConfiguracion.GridControl.RepositoryItems.Add(BodegaGridLookUpEdit)
            gvConfiguracion.Columns("IdBodegaOrigen").ColumnEdit = BodegaGridLookUpEdit

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub InitLookUpEmpresa()

        Try

            Dim DT As New DataTable
            DT = clsLnEmpresa.GetAllForComboBox()

            Dim riGridLookUpEdit As New RepositoryItemGridLookUpEdit()
            riGridLookUpEdit.DataSource = DT
            riGridLookUpEdit.ValueMember = "IdEmpresa"
            riGridLookUpEdit.DisplayMember = "Nombre"
            riGridLookUpEdit.PopulateViewColumns()
            riGridLookUpEdit.View.OptionsBehavior.AutoPopulateColumns = False
            riGridLookUpEdit.View.Columns("IdEmpresa").Visible = False
            gvConfiguracion.GridControl.RepositoryItems.Add(riGridLookUpEdit)
            gvConfiguracion.Columns("IdEmpresaOrigen").ColumnEdit = riGridLookUpEdit

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub InitLookUpPropietario()

        Try

            Dim DT As New DataTable
            DT = clsLnPropietarios.Get_All_By_IdEmpresa_For_Combo(AP.IdEmpresa)

            Dim riGridLookUpEdit As New RepositoryItemGridLookUpEdit()
            riGridLookUpEdit.DataSource = DT
            riGridLookUpEdit.ValueMember = "IdPropietario"
            riGridLookUpEdit.DisplayMember = "Nombre"
            riGridLookUpEdit.PopulateViewColumns()
            riGridLookUpEdit.View.OptionsBehavior.AutoPopulateColumns = False
            riGridLookUpEdit.View.Columns("IdPropietario").Visible = False
            gvConfiguracion.GridControl.RepositoryItems.Add(riGridLookUpEdit)
            gvConfiguracion.Columns("IdPropietarioOrigen").ColumnEdit = riGridLookUpEdit

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub InitLookUpProducto()

        Try

#Region "Columna - IdProductoBodega"

            Dim ProductoGridLookUpEdit As New RepositoryItemGridLookUpEdit()
            ProductoGridLookUpEdit.View.Columns.Clear()

            ProductoGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdProductoBodega", .Caption = "IdProductoBodega", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Código", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True},
                New GridColumn With {.FieldName = "UMBas", .Caption = "UMBas", .Visible = True},
                New GridColumn With {.FieldName = "IdUmBas", .Caption = "IdUmBas", .Visible = False},
                New GridColumn With {.FieldName = "ControlPeso", .Caption = "ControlPeso", .Visible = False},
                New GridColumn With {.FieldName = "Familia", .Caption = "Familia", .Visible = True},
                New GridColumn With {.FieldName = "Clasificacion", .Caption = "Clasificacion", .Visible = True},
                New GridColumn With {.FieldName = "TipoProducto", .Caption = "TipoProducto", .Visible = True}
               })

            ProductoGridLookUpEdit.ValueMember = "IdProducto"
            ProductoGridLookUpEdit.DisplayMember = "Codigo"
            ProductoGridLookUpEdit.NullText = "-> Producto"
            ProductoGridLookUpEdit.PopupFormWidth = 700
            ProductoGridLookUpEdit.DataSource = clsLnProducto.Get_Lista_For_Grid_By_IdBodega(AP.IdBodega)
            ProductoGridLookUpEdit.View.BestFitColumns()

            RemoveHandler ProductoGridLookUpEdit.Leave, AddressOf ProductoGridLookUpEdit_Leave
            AddHandler ProductoGridLookUpEdit.Leave, AddressOf ProductoGridLookUpEdit_Leave

            ProductoGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            gvConfiguracion.Columns("IdProducto").ColumnEdit = ProductoGridLookUpEdit

#End Region

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub InitLookUpCliente()

        Try

#Region "Columna - IdProductoBodega"

            Dim ClienteGridLookUpEdit As New RepositoryItemGridLookUpEdit()
            ClienteGridLookUpEdit.View.Columns.Clear()

            ClienteGridLookUpEdit.View.Columns.AddRange(New GridColumn() {
                New GridColumn With {.FieldName = "IdCliente", .Caption = "IdCliente", .Visible = False},
                New GridColumn With {.FieldName = "Codigo", .Caption = "Código", .Visible = True},
                New GridColumn With {.FieldName = "Nombre", .Caption = "Nombre", .Visible = True}
               })

            ClienteGridLookUpEdit.ValueMember = "IdCliente"
            ClienteGridLookUpEdit.DisplayMember = "Nombre"
            ClienteGridLookUpEdit.NullText = "-> Cliente"
            ClienteGridLookUpEdit.PopupFormWidth = 700
            ClienteGridLookUpEdit.DataSource = clsLnCliente.Get_All_By_IdEmpresa_For_Combo(AP.IdEmpresa)
            ClienteGridLookUpEdit.View.BestFitColumns()

            ClienteGridLookUpEdit.View.OptionsView.ShowAutoFilterRow = True

            gvConfiguracion.Columns("IdCliente").ColumnEdit = ClienteGridLookUpEdit

#End Region

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorAddNewItem.Click

        Try

            'If XtraMessageBox.Show("¿Insertar configuración base por defecto?", "QA Automatization.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            '    Llenar_Grid_Configuracion()

            'End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuPresionarBoton_Click(sender As Object, e As EventArgs) Handles mnuReservaInventario.Click

        Dim BeQA As New clsBeConfiguracion_qa
        Dim vResultado As String = ""
        Dim pIdPedidoEnc As Integer = 0
        Dim pIdProductoBodega As Integer = 0

        Try

            'Soñar.
            If gvConfiguracion.RowCount > 0 Then

                If gvConfiguracion.SelectedRowsCount > 0 Then

                    Dim Dr As DataRowView = gvConfiguracion.GetFocusedRow

                    If Not Dr Is Nothing Then

                        BeQA = New clsBeConfiguracion_qa
                        BeQA.IdConfiguracionQA = Dr.Item("IdConfiguracionQA")

                        clsLnConfiguracion_qa.GetSingle(BeQA)

                        Dim lSelectionIndex As Integer = gvConfiguracion.FocusedRowHandle

                        BeQA.Cantidad_Pedido_Presentacion = Dr.Item("Cantidad_Pedido_Presentacion")
                        BeQA.Cantidad_Pedido_UMBas = Dr.Item("Cantidad_Pedido_UMBas")

                        If BeQA.Cantidad_Pedido_Presentacion = 0 And BeQA.Cantidad_Pedido_UMBas = 0 Then
                            MsgBox("Debe ingresar la cantidad a reservar")
                            Exit Sub
                        End If

                        clsLnStock_res.Ejecuta_QA_CASO_Dinamico(BeQA,
                                                                vResultado,
                                                                pIdPedidoEnc,
                                                                pIdProductoBodega)

                        lblprg.Text = vResultado

                    End If

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally

            Listar_Pedido(pIdPedidoEnc)
            Listar_Stock_By_IdProdutoBodega(pIdProductoBodega)
            Listar_Stock_Reservado(pIdPedidoEnc)

            xtcQAReservas.TabPages(1).Show()

        End Try

    End Sub

    Private Sub frmQA_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            If Not clsLnConfiguracion_qa.Existe_Configuracion_Por_Defecto() Then

                Dim BeCliente As New clsBeCliente
                BeCliente = clsLnCliente.Get_Cliente_Defecto_Pruebas(AP.IdBodega, 1, AP.UsuarioAp.IdUsuario)

                Dim BeProducto As New clsBeProducto
                BeProducto = clsLnProducto.Get_Producto_Defecto(AP.IdBodega, 1)

                If BeProducto Is Nothing Then
                    Exit Sub
                End If

                If Not BeCliente Is Nothing Then

                    Dim BeConfiguracionQA As New clsBeConfiguracion_qa
                    BeConfiguracionQA.IdConfiguracionQA = clsLnConfiguracion_qa.MaxID() + 1
                    BeConfiguracionQA.Nombre = "CASO_" & FormatoFechas.tFechaHora(Now)
                    BeConfiguracionQA.FechaEjecucion = Now
                    BeConfiguracionQA.IdEmpresaOrigen = AP.IdEmpresa
                    BeConfiguracionQA.IdBodegaOrigen = AP.IdBodega

                    Dim vIdPropietario As Integer = 0

                    Try

                        vIdPropietario = clsLnPropietarios.Get_IdPropietario_By_IdBodega_By_IdProducto(AP.IdBodega, BeProducto.IdProducto)

                    Catch ex As Exception
                        Throw
                    End Try

                    BeConfiguracionQA.IdPropietarioOrigen = vIdPropietario
                    BeConfiguracionQA.IdCliente = BeCliente.IdCliente
                    BeConfiguracionQA.IdProducto = BeProducto.IdProducto
                    BeConfiguracionQA.Cantidad_Pedido_Presentacion = 0
                    BeConfiguracionQA.Cantidad_Pedido_UMBas = 1
                    BeConfiguracionQA.User_agr = AP.UsuarioAp.IdUsuario
                    BeConfiguracionQA.User_mod = AP.UsuarioAp.IdUsuario
                    BeConfiguracionQA.Fec_agr = Now
                    BeConfiguracionQA.Fec_mod = Now
                    clsLnConfiguracion_qa.Insertar(BeConfiguracionQA)

                    XtraMessageBox.Show("Se insertó una configuración por defecto.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Listar_Stock_By_IdProdutoBodega(ByVal pIdProductoBodega As Integer)

        Try

            Dim DT As New DataTable

            DT.Clear()

            dgridStock.DataSource = Nothing

            Dim vIdBodega = Integer.Parse(AP.IdBodega)
            Dim vIdPropietarioBodega = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(AP.IdBodega, 1)

            DT = clsLnStock.Get_Stock_By_IdProductoBodega(pIdProductoBodega)

            If Not DT Is Nothing Then


                If DT.Rows.Count > 0 Then

                    dgridStock.DataSource = DT

                    'lblRegs.Caption = String.Format("Registros: {0}", GridView3.RowCount)

                    GridView3.OptionsView.ShowFooter = True

                    GridView3.Columns("Cantidad_Reservada_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("Cantidad_Reservada_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("Cantidad_Reservada_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("Peso").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("CantidadUMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("CantidadUMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("CantidadUMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("CantidadUMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("CantidadPresentacion").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("CantidadPresentacion").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("CantidadPresentacion").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("CantidadPresentacion").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("Disponible_UMBas").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("Disponible_UMBas").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("Disponible_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("Disponible_UMBas").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("Disponible_Presentación").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView3.Columns("Disponible_Presentación").SummaryItem.DisplayFormat = "{0:n6}"
                    GridView3.Columns("Disponible_Presentación").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView3.Columns("Disponible_Presentación").DisplayFormat.FormatString = "{0:n6}"

                    GridView3.Columns("Fecha_Ingreso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    GridView3.Columns("Fecha_Ingreso").DisplayFormat.FormatString = "G"

                    Dim item As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Reservada_UMBas", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView3.Columns("Cantidad_Reservada")}
                    GridView3.GroupSummary.Add(item)

                    Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_UMBas", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView3.Columns("Cantidad_UMBas")}
                    GridView3.GroupSummary.Add(item1)

                    Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Cantidad_Presentacion", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView3.Columns("Cantidad_Presentacion")}
                    GridView3.GroupSummary.Add(item2)

                    Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Disponible_UMBas", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView3.Columns("Disponible_UMBas")}
                    GridView3.GroupSummary.Add(item3)


                    Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem() _
                        With {.FieldName = "Disponible_Presentación", .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        .DisplayFormat = "{0:n6}", .ShowInGroupColumnFooter = GridView3.Columns("Disponible_Presentación")}
                    GridView3.GroupSummary.Add(item4)

                    GridView3.Columns("IdPresentacion").Visible = False

                    GridView3.ExpandAllGroups()

                    GridView3.BestFitColumns()

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

    Private Sub Listar_Stock_Reservado(ByVal pIdPedidoEnc As Integer)

        Dim DT As New DataTable

        Try

            DT.Clear() : dgridStockReservado.DataSource = Nothing

            DT = clsLnVW_Stock_Res_Pedido.Get_StockRes_By_IdPedidoEnc_DT(pIdPedidoEnc)

            Dim vTotalReservado As Double = 0
            Dim vTotalDisponible As Double = 0

            If DT.Rows.Count > 0 Then

                dgridStockReservado.DataSource = DT

                If GridView1.RowCount > 0 Then

                    GridView1.OptionsView.ShowFooter = True
                    GridView1.OptionsView.ColumnAutoWidth = False

                    GridView1.Columns("CantidadFisica").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("CantidadFisica").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("CantidadFisica").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("CantidadFisica").DisplayFormat.FormatString = "{0:n6}"


                    GridView1.Columns("Factor").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("Factor").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("cantidad_presentacion_reservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("cantidad_presentacion_reservada").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("cantidad_presentacion_reservada").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    GridView1.Columns("cantidad_presentacion_reservada").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("cantidad_umbas_reservada").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    GridView1.Columns("cantidad_umbas_reservada").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("cantidad_umbas_reservada").DisplayFormat.FormatType = FormatType.Numeric
                    GridView1.Columns("cantidad_umbas_reservada").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("Peso").SummaryItem.SummaryType = SummaryItemType.Sum
                    GridView1.Columns("Peso").SummaryItem.DisplayFormat = "{0:n6}"

                    GridView1.Columns("Peso").DisplayFormat.FormatType = FormatType.Numeric
                    GridView1.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

                    GridView1.Columns("fec_agr").DisplayFormat.FormatType = FormatType.DateTime
                    GridView1.Columns("fec_agr").DisplayFormat.FormatString = "g"

                    GridView1.BestFitColumns(True)

                    vTotalReservado = DT.AsEnumerable.Sum(Function(x) x.Item("cantidad_umbas_reservada"))
                    vTotalDisponible = DT.AsEnumerable.Sum(Function(x) x.Item("CantidadFisica"))

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

    Public Sub Listar_Pedido(ByVal IdPedido As Integer)

        Try

            dgridPedido.DataSource = Nothing

            dgridPedido.BeginUpdate()

            Dim Dt As New DataTable

            Dt = clsLnTrans_pe_enc.Get_Pedido_By_IdPedido_DT(AP.IdBodega)

            dgridPedido.DataSource = Dt

            If GridView2.Columns.Count > 1 Then
                GridView2.Columns("fec_agr").DisplayFormat.FormatType = FormatType.DateTime
                GridView2.Columns("fec_agr").DisplayFormat.FormatString = "G"
                GridView2.Columns("fec_agr").Caption = "Fecha_Agregado"
                GridView2.Columns("anulado").Caption = "Anulado"
                GridView2.Columns("activo").Caption = "Activo"
                GridView2.Columns("no_documento").Caption = "No_Documento"
                GridView2.Columns("referencia").Caption = "Referencia"
                GridView2.Columns("IdBodega").Visible = False
            End If

            Try

                GridView2.OptionsView.ColumnAutoWidth = False

                If GridView2.Columns.Count > 0 Then
                    If GridView2.RowCount > 0 Then
                        GridView2.BestFitColumns()
                    End If
                End If

            Catch ex As Exception
            End Try

            dgridPedido.EndUpdate()

            GridView2.LayoutChanged()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub mnuCaso15_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso15.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_15_IDEAL_20231018130000)

            Nombre_Caso = "CASO_15_IDEAL_20231018130000"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_15_IDEAL_20231018130000(AP.IdBodega,
                                                                   AP.UsuarioAp.IdUsuario,
                                                                   vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso16_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso16.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_16_IDEAL_202310200156)

            Nombre_Caso = "CASO_16_IDEAL_202310200156"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_16_IDEAL_202310200156(AP.IdBodega,
                                                                   AP.UsuarioAp.IdUsuario,
                                                                   vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso17_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso17.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_17_IDEAL_202311040904)

            Nombre_Caso = "CASO_17_IDEAL_202311040904"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_17_IDEAL_202311040904(AP.IdBodega,
                                                                 AP.UsuarioAp.IdUsuario,
                                                                 vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuInsertarInventarioDemo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInsertarInventarioDemo.ItemClick

        Try



        Catch ex As Exception

        End Try

    End Sub

    Public Sub Actualizar_Progreso(mensaje As String)
        lblprg.AppendText(mensaje & vbNewLine)
        lblprg.Refresh()
        lblprg.SelectionStart = lblprg.TextLength
        lblprg.ScrollToCaret()
    End Sub

    Private Sub mnuCaso18_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso18.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_18_BYB_202311141034)

            Nombre_Caso = "CASO_18_BYB_202311141034"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_18_BYB_202311141034(AP.IdBodega,
                                                               AP.UsuarioAp.IdUsuario,
                                                               vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso19_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso19.ItemClick

        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_19_BYB_202311162103)

            Nombre_Caso = "CASO_19_BYB_202311162103"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_19_BYB_202311162103(AP.IdBodega,
                                                               AP.UsuarioAp.IdUsuario,
                                                               vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub

    Private Sub mnuCaso20_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCaso20.ItemClick


        lblprg.Visible = True

        Try

            lblprg.Clear()

            Actualizar_Progreso("Validando escenario: " & clsCasosUsoReserva.CASO_20_BYB_202311171219)

            Nombre_Caso = "CASO_20_BYB_202311171219"

            Dim vResult As String = ""
            clsLnStock_res.Ejecuta_QA_CASO_20_BYB_202311171219(AP.IdBodega,
                                                               AP.UsuarioAp.IdUsuario,
                                                               vResult)

            Actualizar_Progreso(vResult)


        Catch ex As Exception
            Actualizar_Progreso(ex.Message)
        Finally
            Actualiza_Grids()
        End Try

    End Sub
End Class