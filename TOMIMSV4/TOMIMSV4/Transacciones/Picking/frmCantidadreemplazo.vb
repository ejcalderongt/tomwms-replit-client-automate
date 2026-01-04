Imports System.Reflection
Imports DevExpress.XtraEditors

Public Class frmCantidadreemplazo

    Public Property Modo_Reemplazo As eModoReemplazo = eModoReemplazo.picking
    Public Property IdCliente As Integer = 0
    Public Property IdBodega As Integer = 0
    Public Property Cantidad_Reemplazo As Double = 0
    Private frmSelStock As New frmStock_Especifico_List
    Public pListBeTrans_ubic_hh_det As New List(Of clsBeTrans_ubic_hh_det)
    Public Property BeTipoPedido As New clsBeTrans_pe_tipo
    Public Property Codigo_Producto As String = ""
    Public Property IdPresentacion As Integer = 0
    Private DTStockRes As New DataTable("StockRes")
    Public Property BePickingUbic As clsBeTrans_picking_ubic
    Public Property gTiene_Stock_Disponible As Boolean = False
    Private pBeStockRes As clsBeStock_res = Nothing
    Private IdPresentacionPedido As Integer = 0

    Public Enum eModoReemplazo
        picking = 1
        verificacion = 2
    End Enum

    Private Sub mnuBuscarProductosReemplazo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBuscarProductosReemplazo.ItemClick

        Try

            If frmSelStock Is Nothing Then
                frmSelStock = New frmStock_Especifico_List With {.Modo = frmStock_Especifico_List.pModo.Seleccion,
                .WindowState = FormWindowState.Maximized, .IdCliente = IdCliente, .BuscarPoliza = False, .IdBodega = IdBodega}
            Else
                frmSelStock.IdCliente = IdCliente
                frmSelStock.IdBodega = IdBodega
            End If

            frmSelStock.pIdPropietarioBodega = 0

            frmSelStock.Modo = frmStock_Especifico_List.pModo.Seleccion
            frmSelStock.pListObjDet = pListBeTrans_ubic_hh_det
            frmSelStock.WindowState = FormWindowState.Maximized
            frmSelStock.IdProductoEstadoDefault = BeTipoPedido.IdProductoEstado
            frmSelStock.IdPresentacion = IdPresentacion

            If (Codigo_Producto <> "") Then
                frmSelStock.GridView1.ClearColumnsFilter()
                frmSelStock.txtIdProducto.Text = Codigo_Producto
                frmSelStock.txtIdProducto.ReadOnly = True
                frmSelStock.txtIdProducto_Validated(Nothing, Nothing)
            End If

            frmSelStock.pObjStock = Nothing
            frmSelStock.ShowDialog()

            If frmSelStock.pObjStock IsNot Nothing Then

                Dim BeStockSel As clsBeVW_stock_res = frmSelStock.pObjStock
                Dim BeTransUbicHHDet As New clsBeTrans_ubic_hh_det
                BeTransUbicHHDet.IdStock = BeStockSel.IdStock

                Dim cantidadBase As Double = If(BeStockSel.CantidadPresentacion = 0,
                                                BeStockSel.CantidadUmBas,
                                                BeStockSel.CantidadPresentacion)

                Dim cantidadSel As Double = Math.Min(cantidadBase, Cantidad_Reemplazo)

                BeTransUbicHHDet.Cantidad = Math.Min(cantidadBase, Cantidad_Reemplazo)
                Cantidad_Reemplazo -= BeTransUbicHHDet.Cantidad

                txtCantidadReemplazo.Maximum = Cantidad_Reemplazo
                txtCantidadReemplazo.Value = Cantidad_Reemplazo

                BeTransUbicHHDet.Activo = True
                pListBeTrans_ubic_hh_det.Add(BeTransUbicHHDet)

                Dim row As DataRow = DTStockRes.NewRow()

                row("IdStock") = BeStockSel.IdStock
                row("Código") = BeStockSel.Codigo_Producto
                row("Nombre") = BeStockSel.Nombre_Producto
                row("Estado") = BeStockSel.NomEstado
                row("Lote") = BeStockSel.Lote
                row("Licencia") = BeStockSel.Lic_plate
                row("Vence") = BeStockSel.Fecha_Vence
                row("Cantidad_UMBas") = BeStockSel.CantidadUmBas
                row("UMBas") = BeStockSel.UMBas
                row("Cantidad_Pres") = BeStockSel.CantidadPresentacion
                row("Presentación") = BeStockSel.Nombre_Presentacion
                row("Peso") = BeStockSel.Peso
                row("Ubicacion") = BeStockSel.Nombre_Completo

                If BeStockSel.CantidadPresentacion = 0 Then
                    row("Cantidad_UMBas") = cantidadSel
                    row("Cantidad_Pres") = 0
                Else
                    row("Cantidad_Pres") = cantidadSel
                    row("Cantidad_UMBas") = cantidadSel * BeStockSel.Factor
                End If

                DTStockRes.Rows.Add(row)
                DTStockRes.AcceptChanges()

                grdvPickingUbic.BestFitColumns()
                grdvPickingUbic.RefreshData()

                format_grid()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub Set_Columnas_DT_StockRes()
        DTStockRes.Columns.Add("IdStock", GetType(Integer))
        DTStockRes.Columns.Add("Código", GetType(String))
        DTStockRes.Columns.Add("Nombre", GetType(String))
        DTStockRes.Columns.Add("Estado", GetType(String))
        DTStockRes.Columns.Add("Lote", GetType(String))
        DTStockRes.Columns.Add("Licencia", GetType(String))
        DTStockRes.Columns.Add("Vence", GetType(Date))
        DTStockRes.Columns.Add("Cantidad_UMBas", GetType(Double))
        DTStockRes.Columns.Add("UMBas", GetType(String))
        DTStockRes.Columns.Add("Cantidad_Pres", GetType(Double))
        DTStockRes.Columns.Add("Presentación", GetType(String))
        DTStockRes.Columns.Add("Peso", GetType(Double))
        DTStockRes.Columns.Add("Ubicacion", GetType(String))
    End Sub

    Private Sub frmCantidadreemplazo_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Set_Columnas_DT_StockRes()

        dgridPickingUbic.DataSource = DTStockRes
        grdvPickingUbic.BestFitColumns()

        gTiene_Stock_Disponible = Tiene_Stock_Disponible()

        lblNoStock.Visible = Not gTiene_Stock_Disponible

        IdPresentacionPedido = clsLnTrans_pe_det.Get_IdPresentacion_By_IdPedidoDet(BePickingUbic.IdPedidoEnc, BePickingUbic.IdPedidoDet)

        If BeTipoPedido IsNot Nothing Then

            txtIdEstadoDefectoRecepcion.EditValue = BeTipoPedido.IdProductoEstado
            txtNombreEstado.EditValue = clsLnProducto_estado.Get_Nombre_By_IdEstado(BeTipoPedido.IdProductoEstado)

            Dim BeProductoEstado As clsBeProducto_estado = clsLnProducto_estado.Get_Single_By_IdEstado(BeTipoPedido.IdProductoEstado)
            'Dim BeBodega As clsBeBodega = clsLnBodega.GetSingle_By_Idbodega(BePickingUbic.IdBodega)

            If BeProductoEstado IsNot Nothing Then
                Dim idUbicacionFinal As Integer = BePickingUbic.IdUbicacion

                If BeProductoEstado.IdUbicacionBodegaDefecto <> 0 Then
                    idUbicacionFinal = BeProductoEstado.IdUbicacionBodegaDefecto
                ElseIf BeProductoEstado.IdUbicacionDefecto <> 0 Then
                    idUbicacionFinal = BeProductoEstado.IdUbicacionDefecto
                End If

                txtIdUbicacion.EditValue = idUbicacionFinal
                txtNombreUbicacion.EditValue = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(idUbicacionFinal, BePickingUbic.IdBodega)
            End If

        End If

    End Sub

    Private Sub format_grid()

        grdvPickingUbic.Columns("Cantidad_UMBas").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        grdvPickingUbic.Columns("Cantidad_UMBas").DisplayFormat.FormatString = "{0:n6}"
        grdvPickingUbic.Columns("Cantidad_UMBas").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grdvPickingUbic.Columns("Cantidad_UMBas").DisplayFormat.FormatString = "{0:n6}"

        grdvPickingUbic.Columns("Cantidad_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        grdvPickingUbic.Columns("Cantidad_Pres").DisplayFormat.FormatString = "{0:n6}"
        grdvPickingUbic.Columns("Cantidad_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grdvPickingUbic.Columns("Cantidad_Pres").DisplayFormat.FormatString = "{0:n6}"

        grdvPickingUbic.Columns("Peso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        grdvPickingUbic.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"
        grdvPickingUbic.Columns("Peso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grdvPickingUbic.Columns("Peso").DisplayFormat.FormatString = "{0:n6}"

    End Sub

    Private Sub mnuAplicarReemplazo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAplicarReemplazo.ItemClick

        Try

            For Each StockReemplazo In pListBeTrans_ubic_hh_det
                Reemplazar_ListaPu_By_Stock(pBeStockRes,
                                            StockReemplazo.Cantidad,
                                            AP.HostName,
                                            BePickingUbic.IdPickingEnc,
                                            BePickingUbic.IdPedidoEnc,
                                            0,
                                            BePickingUbic.IdPedidoDet,
                                            BePickingUbic,
                                            True,
                                            IdPresentacionPedido,
                                            True,
                                            txtIdUbicacion.EditValue,
                                            txtIdUbicacion.EditValue,)
            Next

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Function Reemplazar_ListaPu_By_Stock(ByVal pBeStock_res As clsBeStock_res,
                                                ByVal CantSol As Double,
                                                ByVal MaquinaQueSolicita As String,
                                                ByVal IdPickingEnc As Integer,
                                                ByVal IdPedidoEnc As Integer,
                                                ByVal IdUsuarioHH As Integer,
                                                ByVal IdPedidoDet As Integer,
                                                ByVal BePickingUbic As clsBeTrans_picking_ubic,
                                                ByVal EsPicking As Boolean,
                                                ByVal IdPresentacionPedido As Integer,
                                                ByVal EsReemplazo As Boolean,
                                                ByVal IdUbicDestino As Integer,
                                                ByVal IdEstDestino As Integer,
                                                ByVal CantidadTotal As Double,
                                                ByRef CantPend As Double,
                                                ByVal ConExistencia As Boolean) As Boolean

        Reemplazar_ListaPu_By_Stock = False

        Try

            If ConExistencia Then

                Return clsLnStock_res.Reemplazar_ListaPu_By_Stock(pBeStock_res,
                                                                  CantSol,
                                                                  MaquinaQueSolicita,
                                                                  IdPickingEnc,
                                                                  IdPedidoEnc,
                                                                  IdUsuarioHH,
                                                                  IdPedidoDet,
                                                                  BePickingUbic,
                                                                  EsPicking,
                                                                  EsReemplazo,
                                                                  IdUbicDestino,
                                                                  IdEstDestino,
                                                                  CantidadTotal,
                                                                  CantPend)
            Else

                Return clsLnStock_res.Reemplazar_ListaPu_By_Stock_Sin_Exist(CantSol,
                                                                            IdUsuarioHH,
                                                                            BePickingUbic,
                                                                            EsPicking,
                                                                            EsReemplazo,
                                                                            CantidadTotal,
                                                                            CantPend,
                                                                            IdUbicDestino,
                                                                            IdEstDestino)
            End If


        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)

            clsLnLog_error_wms_pick.Agregar_Error(vMsgError,
                                      pStackTrace:=ex.StackTrace,
                                      pIdPickingEnc:=IdPickingEnc,
                                      pIdPedidoEnc:=IdPedidoEnc,
                                      pIdPedidoDet:=IdPedidoDet,
                                      pIdPickingUbic:=BePickingUbic.IdPickingUbic)
        End Try

    End Function


    Private Function Tiene_Stock_Disponible() As Boolean

        Tiene_Stock_Disponible = False

        Try


            pBeStockRes = clsLnStock_res.GetSingle_By_IdStockRes(BePickingUbic.IdBodega, BePickingUbic.IdStockRes)

            Dim Dt As DataTable = clsLnStock.Get_All_Stock_Especifico_By_IdPedidoDet(pBeStockRes,
                                                                                     BePickingUbic.IdPedidoEnc,
                                                                                     BePickingUbic.IdBodega)


            Tiene_Stock_Disponible = (Dt.Rows.Count > 0)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

End Class