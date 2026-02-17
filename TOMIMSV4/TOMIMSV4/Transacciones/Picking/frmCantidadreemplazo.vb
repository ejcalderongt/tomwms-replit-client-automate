Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmCantidadreemplazo

    Public Property Modo_Reemplazo As eModoReemplazo = eModoReemplazo.picking
    Public Property IdCliente As Integer = 0
    Public Property IdBodega As Integer = 0
    Public Property Cantidad_Reemplazo As Double = 0
    Public Property Cantidad_Total As Double = 0

    Private frmSelStock As New frmStock_Especifico_List

    Public pListBeTrans_ubic_hh_det As New List(Of clsBeTrans_ubic_hh_det)

    Public pListBeStockRes As New List(Of clsBeStock_res)
    Public Property BeTipoPedido As New clsBeTrans_pe_tipo
    Public Property Codigo_Producto As String = ""
    Public Property IdPresentacion As Integer = 0
    Private DTStockRes As New DataTable("StockRes")
    Public Property BePickingUbic As clsBeTrans_picking_ubic
    Public Property gTiene_Stock_Disponible As Boolean = False
    Private pBeStockRes As clsBeStock_res = Nothing
    Private IdPresentacionPedido As Integer = 0
    Public Property OpcionesMenu As New clsBeOpcionesMenuRol
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

            If txtCantidadReemplazo.Value = 0 Then
                XtraMessageBox.Show("Ya completó la cantidad a reemplazar", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
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

                Cantidad_Reemplazo = txtCantidadReemplazo.Value

                Dim BeStockSel As clsBeVW_stock_res = frmSelStock.pObjStock

                Dim BeTransUbicHHDet As New clsBeTrans_ubic_hh_det
                Dim vBeStockRes As New clsBeStock_res

                BeTransUbicHHDet.IdStock = BeStockSel.IdStock
                vBeStockRes.IdStock = BeStockSel.IdStock

                Dim cantidadBase As Double = If(BeStockSel.CantidadPresentacion = 0,
                                                BeStockSel.CantidadUmBas,
                                                BeStockSel.CantidadPresentacion)

                Dim cantidadSel As Double = Math.Min(cantidadBase, Cantidad_Reemplazo)

                BeTransUbicHHDet.Cantidad = Math.Min(cantidadBase, Cantidad_Reemplazo)
                vBeStockRes.Cantidad = cantidadSel

                Cantidad_Reemplazo -= BeTransUbicHHDet.Cantidad

                txtCantidadReemplazo.Maximum = Cantidad_Reemplazo
                txtCantidadReemplazo.Value = Cantidad_Reemplazo

                BeTransUbicHHDet.Activo = True
                pListBeTrans_ubic_hh_det.Add(BeTransUbicHHDet)

                vBeStockRes.IdProductoBodega = BeStockSel.IdProductoBodega
                vBeStockRes.IdUbicacion = BeStockSel.IdUbicacion
                vBeStockRes.Fecha_vence = BeStockSel.Fecha_Vence
                vBeStockRes.Lote = BeStockSel.Lote
                vBeStockRes.Lic_plate = BeStockSel.Lic_plate
                vBeStockRes.IdPresentacion = BeStockSel.IdPresentacion
                vBeStockRes.IdUnidadMedida = BeStockSel.IdUnidadMedida
                vBeStockRes.IdProductoEstado = BeStockSel.IdProductoEstado
                vBeStockRes.Ultimo_Lote = ""
                vBeStockRes.Indicador = "PED"

                If BeStockSel.CantidadPresentacion = 0 Then
                    vBeStockRes.Cantidad = cantidadSel
                Else
                    vBeStockRes.Cantidad = cantidadSel * BeStockSel.Factor
                End If

                pListBeStockRes.Add(vBeStockRes)

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
        mnuBuscarProductosReemplazo.Visibility = IIf(gTiene_Stock_Disponible, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)
        mnuAplicarReemplazo.Visibility = IIf(gTiene_Stock_Disponible, DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)

        IdPresentacionPedido = clsLnTrans_pe_det.Get_IdPresentacion_By_IdPedidoDet(BePickingUbic.IdPedidoEnc, BePickingUbic.IdPedidoDet)

        If BeTipoPedido IsNot Nothing Then

            Dim idEstado As Integer = If(BeTipoPedido.IdProductoEstado = 0,
                                         BePickingUbic.IdProductoEstado,
                                         BeTipoPedido.IdProductoEstado)

            txtIdEstadoDefectoRecepcion.EditValue = idEstado
            txtNombreEstado.EditValue = clsLnProducto_estado.Get_Nombre_By_IdEstado(idEstado)

            txtUMBas.EditValue = BePickingUbic.ProductoUnidadMedida
            txtPresentacion.EditValue = BePickingUbic.ProductoPresentacion

            Dim BeProductoEstado As clsBeProducto_estado = clsLnProducto_estado.Get_Single_By_IdEstado(idEstado)

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

            Dim clsTrans As New clsTransaccion

            clsTrans.Begin_Transaction()

            For Each StockReemplazo In pListBeStockRes

                Dim vCantidadSolicitada As Double = txtCantidadSolicitada.Value
                Cantidad_Reemplazo = StockReemplazo.Cantidad

                If StockReemplazo.IdPresentacion <> 0 Then
                    Dim vFactor As New Double
                    vFactor = clsLnProducto_presentacion.Get_Factor_By_IdPresentacion(BePickingUbic.IdPresentacion,
                                                                                          clsTrans.lConnection,
                                                                                          clsTrans.lTransaction)

                    Cantidad_Reemplazo = StockReemplazo.Cantidad / vFactor
                End If

                If Modo_Reemplazo = eModoReemplazo.picking Then

                    Reemplazar_IdStock_By_Stock(StockReemplazo,
                                                Cantidad_Reemplazo,
                                                AP.HostName,
                                                BePickingUbic.IdPickingEnc,
                                                BePickingUbic.IdPedidoEnc,
                                                0,
                                                BePickingUbic.IdPedidoDet,
                                                BePickingUbic.IdPickingUbic,
                                                True,
                                                1,
                                                txtIdUbicacion.EditValue,
                                                txtIdEstadoDefectoRecepcion.EditValue,
                                                Cantidad_Reemplazo,
                                                clsTrans.lConnection,
                                                clsTrans.lTransaction)

                Else

                    Reemplazar_ListPickingUbic_Verificacion(BePickingUbic,
                                                            pBeStockRes,
                                                            BePickingUbic.IdPickingEnc,
                                                            BePickingUbic.IdOperadorBodega_Pickeo,
                                                            AP.HostName,
                                                            AP.IdBodega,
                                                            AP.IdEmpresa,
                                                            txtIdUbicacion.Text,
                                                            txtIdEstadoDefectoRecepcion.Text,
                                                            vCantidadSolicitada,
                                                            Cantidad_Reemplazo,
                                                            Cantidad_Reemplazo,
                                                            BePickingUbic.Cantidad_Solicitada,
                                                            True)
                End If

            Next

            clsTrans.Commit_Transaction()

            Me.DialogResult = DialogResult.OK

            Close()

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
                                                                  IIf(EsReemplazo, 1, 0),
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

            Throw New Exception(vMsgError)

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

    Public Function Reemplazar_ListPickingUbic_Verificacion(ByVal plistPickingUbi As clsBeTrans_picking_ubic,
                                                            ByVal pBeStockRes As clsBeStock_res,
                                                            ByVal pIdPickingEnc As Integer,
                                                            ByVal pIdOperador As Integer,
                                                            ByVal pHost As String,
                                                            ByVal pIdBodega As Integer,
                                                            ByVal pIdEmpresa As Integer,
                                                            ByVal pIdUbicDestino As Integer,
                                                            ByVal pIdEstadoDestino As Integer,
                                                            ByVal pCantLinea As Double,
                                                            ByRef pCantReemplazar As Double,
                                                            ByVal pCantTotal As Double,
                                                            ByRef CantPend As Double,
                                                            ByVal ConExistencia As Boolean) As Boolean


        Reemplazar_ListPickingUbic_Verificacion = False

        Try

            If ConExistencia Then

                Return clsLnStock_res.Reemplazo_Verificacion_By_ListPickingUbic(plistPickingUbi,
                                                                                pBeStockRes,
                                                                                pIdPickingEnc,
                                                                                pIdOperador,
                                                                                pHost,
                                                                                pIdBodega, pIdEmpresa,
                                                                                pIdUbicDestino,
                                                                                pIdEstadoDestino,
                                                                                pCantLinea,
                                                                                pCantReemplazar,
                                                                                pCantTotal,
                                                                                CantPend)
            Else

                Return clsLnStock_res.Reemplazar_ListaPu_By_Stock_Sin_Exist(pCantReemplazar,
                                                                            pIdOperador,
                                                                            plistPickingUbi,
                                                                            False,
                                                                            1,
                                                                            pCantTotal,
                                                                            CantPend,
                                                                            pIdUbicDestino,
                                                                            pIdEstadoDestino)

            End If


        Catch ex As Exception

            Dim vMsgError As String = String.Format("{0} {1}", MethodBase.GetCurrentMethod.Name(), ex.Message)
            Throw New Exception(vMsgError)

        End Try

    End Function

    Public Shared Function Reemplazar_IdStock_By_Stock(ByVal pBeStock_res As clsBeStock_res,
                                                       ByVal CantSol As Double,
                                                       ByVal MaquinaQueSolicita As String,
                                                       ByVal IdPickingEnc As Integer,
                                                       ByVal IdPedidoEnc As Integer,
                                                       ByVal IdUsuarioHH As Integer,
                                                       ByVal IdPedidoDet As Integer,
                                                       ByVal IdPickingUbic As Integer,
                                                       ByVal EsPicking As Boolean,
                                                       ByVal Tipo As Integer,
                                                       ByVal IdUbicDestino As Integer,
                                                       ByVal IdEstDestino As Integer,
                                                       ByRef CantPend As Double,
                                                       ByRef lConnection As SqlConnection,
                                                       ByRef ltransaction As SqlTransaction) As Boolean

        Reemplazar_IdStock_By_Stock = False

        Try
            Dim clsLnStock As New clsLnStock
            Dim BeTransPickingUbic As New clsBeTrans_picking_ubic
            Dim BeTransPeEnc As New clsBeTrans_pe_enc
            Dim StockResList As New List(Of clsBeStock_res)
            Dim lStock As New List(Of clsBeStock)
            Dim vCantidadReservada As Double
            Dim CantidadCompletada As Boolean = False
            Dim CantidadPendiente, CantSolTotal As Double
            Dim IdPropietarioBodega As Integer = 0

            CantSolTotal = CantSol

            BeTransPickingUbic.IdPickingUbic = IdPickingUbic

            clsLnTrans_picking_ubic.GetSingle(BeTransPickingUbic,
                                              lConnection,
                                              ltransaction)

            lStock = clsLnStock.Get_Stock_Para_Reserva(pBeStock_res,
                                                      lConnection,
                                                      ltransaction,
                                                      False)

            If lStock.Count > 0 Then

                For Each s In lStock

                    vCantidadReservada = clsLnStock_res.Get_Cantidad_ReservadaUMBas_By_IdStock(s.IdStock,
                                                                                               False,
                                                                                               lConnection,
                                                                                               ltransaction)

                    If vCantidadReservada <> 0 Then
                        s.Cantidad -= vCantidadReservada
                    End If

                Next

            End If

            For Each i In lStock.Where(Function(x) x.Cantidad > 0)

                Dim resultRes As Boolean = clsLnStock_res.Reservar_Stock_By_Stock_Reem(i,
                                                                                       CantSol,
                                                                                       MaquinaQueSolicita,
                                                                                       IdPickingEnc,
                                                                                       IdPedidoEnc,
                                                                                       IdUsuarioHH,
                                                                                       IdPedidoDet,
                                                                                       IdPickingUbic,
                                                                                       EsPicking,
                                                                                       pBeStock_res.IdPresentacion,
                                                                                       Tipo,
                                                                                       CantidadPendiente,
                                                                                       lConnection,
                                                                                       ltransaction)

                If resultRes Then
                    CantidadCompletada = (CantidadPendiente = 0)
                    CantPend = CantidadPendiente
                    If CantidadCompletada Then
                        Exit For
                    Else
                        CantSol = CantidadPendiente
                    End If
                End If
            Next

            CantPend = CantidadPendiente

            BeTransPeEnc = clsLnTrans_pe_enc.GetSingle(IdPedidoEnc, lConnection, ltransaction)

            If BeTransPeEnc IsNot Nothing Then
                IdPropietarioBodega = BeTransPeEnc.IdPropietarioBodega
            End If

            StockResList = clsLnStock_res.Get_All_Reemplazo_By_IdPedidoDet(IdPedidoDet,
                                                                          IdPropietarioBodega,
                                                                          IdPickingEnc,
                                                                          IdPedidoEnc,
                                                                          lConnection,
                                                                          ltransaction)

            If StockResList IsNot Nothing Then

                Dim resultReemp As Boolean = False

                resultReemp = clsLnTrans_picking_ubic.Reemplazo_Producto_En_Picking(BeTransPickingUbic.IdStock,
                                                                                    IdPickingEnc,
                                                                                    BeTransPickingUbic.IdPickingDet,
                                                                                    CantSolTotal,
                                                                                    MaquinaQueSolicita,
                                                                                    IdUsuarioHH,
                                                                                    StockResList,
                                                                                    BeTransPeEnc.IdBodega,
                                                                                    BeTransPeEnc.Cliente.IdEmpresa,
                                                                                    IdUbicDestino,
                                                                                    IdEstDestino,
                                                                                    BeTransPickingUbic.IdStockRes,
                                                                                    EsPicking,
                                                                                    lConnection,
                                                                                    ltransaction,
                                                                                    Tipo)
                If Not resultReemp Then
                    Throw New Exception(String.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, "No se logró reemplazar el IdStock"))
                End If

            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub dgridPickingUbic_KeyDown(sender As Object, e As KeyEventArgs) Handles dgridPickingUbic.KeyDown
        Try
            Dim currentView As GridView = CType(dgridPickingUbic.FocusedView, GridView)

            If Not currentView Is Nothing Then

                If dgridPickingUbic.IsFocused AndAlso e.KeyCode = Keys.Delete Then

                    If XtraMessageBox.Show("¿Eliminar stock seleccionado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then

                        Dim CantidadUMBas As Double = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "Cantidad_UMBas")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "Cantidad_UMBas"))
                        Dim CantidadPres As Double = IIf(IsDBNull(currentView.GetRowCellValue(currentView.FocusedRowHandle, "Cantidad_Pres")), 0, currentView.GetRowCellValue(currentView.FocusedRowHandle, "Cantidad_Pres"))
                        Dim valor As Double = txtCantidadReemplazo.Value

                        If CantidadPres > 0 Then
                            valor += CantidadPres
                        Else
                            valor += CantidadUMBas
                        End If

                        txtCantidadReemplazo.Minimum = 1
                        txtCantidadReemplazo.Maximum = valor
                        txtCantidadReemplazo.Value = valor

                        currentView.DeleteRow(currentView.FocusedRowHandle)

                        If grdvPickingUbic.RowCount = 0 Then

                            txtCantidadReemplazo.Maximum = txtCantidadSolicitada.Value
                            txtCantidadReemplazo.Value = txtCantidadSolicitada.Value

                        End If

                    End If

                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lnkEstadoPorDefecto_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkEstadoPorDefecto.LinkClicked

        Try

            Dim BePropietarioBodega As New clsBePropietario_bodega
            BePropietarioBodega.IdPropietarioBodega = BePickingUbic.IdPropietarioBodega

            clsLnPropietario_bodega.GetSingle(BePropietarioBodega)

            Dim Estado As New frmProducto_EstadoList With
                    {.pIdPropietario = BePropietarioBodega.IdPropietario,
                    .Modo = frmProducto_EstadoList.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                Estado.OpcionesMenu = OpcionesMenu
                Estado.mnuNuevo.Enabled = OpcionesMenu.Modificar
                Estado.mnuActualizar.Enabled = OpcionesMenu.Leer
            End If

            Estado.ShowDialog()

            If Estado.pObj IsNot Nothing AndAlso Estado.pObj.IdEstado <> 0 Then

                txtIdEstadoDefectoRecepcion.Text = Estado.pObj.IdEstado
                txtNombreEstado.Text = Estado.pObj.Nombre

                txtIdUbicacion.Text = Estado.pObj.IdUbicacionDefecto

                If Estado.pObj.IdUbicacionDefecto > 0 Then
                    txtNombreUbicacion.Text = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(Estado.pObj.IdUbicacionDefecto, AP.IdBodega)
                End If

            End If

            Estado.Close()
            Estado.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub lnkUbicacion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUbicacion.LinkClicked

        Try

            Dim Ubicacion As New frmBodegaUbicacion_List() With
                {.pUbicacionRecepcion = True,
                .pIdBodega = AP.Bodega.IdBodega,
                .Modo = frmBodegaUbicacion_List.pModo.Seleccion}

            If OpcionesMenu IsNot Nothing Then
                Ubicacion.OpcionesMenu = OpcionesMenu
                Ubicacion.mmuActualizar.Enabled = OpcionesMenu.Leer
            End If

            Ubicacion.ShowDialog()

            If Ubicacion.pObj IsNot Nothing AndAlso Ubicacion.pObj.IdUbicacion <> 0 Then
                txtIdUbicacion.Text = Ubicacion.pObj.IdUbicacion
                txtNombreUbicacion.Text = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(Ubicacion.pObj.IdUbicacion, Ubicacion.pObj.IdBodega)
            End If

            Ubicacion.Close()
            Ubicacion.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub txtIdUbicacion_LostFocus(sender As Object, e As EventArgs) Handles txtIdUbicacion.LostFocus
        Try

            txtNombreUbicacion.Text = ""

            Dim vIdUbicacion As Integer = Val(txtIdUbicacion.Text)

            If vIdUbicacion > 0 Then
                txtNombreUbicacion.Text = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(vIdUbicacion, AP.IdBodega)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtIdUbicacion_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIdUbicacion.KeyDown
        Try

            If e.KeyCode = Keys.Enter Then

                txtNombreUbicacion.Text = ""

                Dim vIdUbicacion As Integer = Val(txtIdUbicacion.Text)

                If vIdUbicacion > 0 Then
                    txtNombreUbicacion.Text = clsLnBodega_ubicacion.Get_Nombre_Completo_By_IdUbicacion(vIdUbicacion, AP.IdBodega)
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class