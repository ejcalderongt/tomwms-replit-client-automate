Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraSplashScreen
Public Class frmRegularizarInventario

    Public SinMovs As Integer = 0
    Public ConMovs As Integer = 0
    Public NoRecal As Integer = 0
    Private ListCiclico As New List(Of clsBeTrans_inv_ciclico)
    Private ListMovimientos As New List(Of clsBeTrans_movimientos)
    Private ListStock As New List(Of clsBeStock)
    Public gBeInventario As New clsBeTrans_inv_enc
    Public pBeTransAjustEnc As New clsBeTrans_ajuste_enc
    Private lBeTransAjusteDet As New List(Of clsBeTrans_ajuste_det)
    Private ListMovs As New List(Of clsBeTrans_movimientos)
    Private pBeMovs As New clsBeTrans_movimientos
    Private ListStockNuevo As New List(Of clsBeStock)
    Private pBeAjusteDet As New clsBeTrans_ajuste_det
    Private pBeStock As New clsBeStock
    Private vCantidad As Double = 0.0
    Private vPeso As Double = 0.0
    Private Diferencia_Stock_Vrs_Conteo As Double = 0
    Private IdTipoTarea As Integer = 0
    Private vLote As String = ""
    Private Producto As New clsBeProducto
    Private IdAjusteDet As Integer = 0
    Private IdAjusteEnc As Integer = 0
    Private lOperaciones As New List(Of KeyValuePair(Of Integer, Integer))
    Private ReadOnly mProductoByProductoBodegaCache As New Dictionary(Of Integer, clsBeProducto)
    Private ReadOnly mTallaColorCache As New Dictionary(Of Integer, clsBeProducto_talla_color)
    Private ReadOnly mTallaCodigoCache As New Dictionary(Of Integer, String)
    Private ReadOnly mColorCodigoCache As New Dictionary(Of Integer, String)
    Private ReadOnly mBodegaERPCache As New Dictionary(Of String, Integer)
    Private mIdMotivoAjusteCache As Integer = -1
    Private mRegularizacionUltimoTick As Integer = 0
    Private mRegularizacionTotalAjustes As Integer = 0
    Private mRegularizacionProcesados As Integer = 0

    Public Enum TipoTrans
        Nuevo = 1
        Editar = 2
    End Enum

    Public Property Modo As TipoTrans

    Public Sub New(ByVal pModo As TipoTrans)
        InitializeComponent()
        Modo = pModo
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Limpiar_Caches_Regularizacion()

        mProductoByProductoBodegaCache.Clear()
        mTallaColorCache.Clear()
        mTallaCodigoCache.Clear()
        mColorCodigoCache.Clear()
        mBodegaERPCache.Clear()
        mIdMotivoAjusteCache = -1
        mRegularizacionUltimoTick = 0

    End Sub

    Private Function Get_IdMotivoAjuste_Regularizacion(ByVal lConnection As SqlConnection,
                                                       ByVal lTransaction As SqlTransaction) As Integer

        If mIdMotivoAjusteCache = -1 Then
            Dim DT As DataTable = clsLnAjuste_motivo.Listar()
            mIdMotivoAjusteCache = 0

            If DT IsNot Nothing Then
                If DT.Rows.Count > 0 Then
                    mIdMotivoAjusteCache = DT.Rows(0).Item("IdMotivoAjuste")
                End If
                DT.Dispose()
            End If
        End If

        Return mIdMotivoAjusteCache

    End Function

    Private Function Get_IdBodegaERP_Regularizacion(ByVal pCodigoBodegaERP As String,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As Integer

        Dim vKey As String = If(pCodigoBodegaERP, "")

        If Not mBodegaERPCache.ContainsKey(vKey) Then
            mBodegaERPCache(vKey) = clsLnCliente.Get_IdBodega_By_Codigo(vKey, lConnection, lTransaction)
        End If

        Return mBodegaERPCache(vKey)

    End Function

    Private Function Get_Producto_Regularizacion(ByVal pIdProductoBodega As Integer,
                                                 ByVal lConnection As SqlConnection,
                                                 ByVal lTransaction As SqlTransaction) As clsBeProducto

        If Not mProductoByProductoBodegaCache.ContainsKey(pIdProductoBodega) Then
            mProductoByProductoBodegaCache(pIdProductoBodega) = clsLnProducto.Get_Single_By_IdProductoBodega(pIdProductoBodega,
                                                                                                            lConnection,
                                                                                                            lTransaction)
        End If

        Return mProductoByProductoBodegaCache(pIdProductoBodega)

    End Function

    Private Function Get_TallaColor_Regularizacion(ByVal pIdProductoTallaColor As Integer,
                                                   ByVal lConnection As SqlConnection,
                                                   ByVal lTransaction As SqlTransaction) As clsBeProducto_talla_color

        If Not mTallaColorCache.ContainsKey(pIdProductoTallaColor) Then
            mTallaColorCache(pIdProductoTallaColor) = clsLnProducto_talla_color.GetSingle(pIdProductoTallaColor,
                                                                                         lConnection,
                                                                                         lTransaction)
        End If

        Return mTallaColorCache(pIdProductoTallaColor)

    End Function

    Private Function Get_TallaCodigo_Regularizacion(ByVal pIdTalla As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As String

        If Not mTallaCodigoCache.ContainsKey(pIdTalla) Then
            Dim vTalla = clsLnTalla.GetSingle(pIdTalla, lConnection, lTransaction)
            mTallaCodigoCache(pIdTalla) = If(vTalla Is Nothing, "", vTalla.Codigo)
        End If

        Return mTallaCodigoCache(pIdTalla)

    End Function

    Private Function Get_ColorCodigo_Regularizacion(ByVal pIdColor As Integer,
                                                    ByVal lConnection As SqlConnection,
                                                    ByVal lTransaction As SqlTransaction) As String

        If Not mColorCodigoCache.ContainsKey(pIdColor) Then
            Dim vColor = clsLnColor.GetSingle(pIdColor, lConnection, lTransaction)
            mColorCodigoCache(pIdColor) = If(vColor Is Nothing, "", vColor.Codigo)
        End If

        Return mColorCodigoCache(pIdColor)

    End Function

    Private Sub Actualizar_Progreso_Regularizacion(ByVal pMensaje As String,
                                                   ByVal pActual As Integer,
                                                   ByVal pTotal As Integer,
                                                   Optional ByVal pForzar As Boolean = False)

        Dim vAhora As Integer = Environment.TickCount

        If Not pForzar AndAlso pActual < pTotal AndAlso Math.Abs(vAhora - mRegularizacionUltimoTick) < 250 Then
            Return
        End If

        mRegularizacionUltimoTick = vAhora

        Try
            Dim vTotal As Integer = Math.Max(pTotal, 1)
            Dim vActual As Integer = Math.Min(Math.Max(pActual, 0), vTotal)
            Dim vTexto As String = String.Format("{0} ({1}/{2})", pMensaje, vActual, vTotal)

            If SplashScreenManager.Default IsNot Nothing Then
                SplashScreenManager.Default.SetWaitFormDescription(vTexto)
            End If

            prg.Visible = True
            prg.Minimum = 0
            prg.Maximum = vTotal
            prg.Value = vActual
            lblPrg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            lblPrg.Caption = vTexto
            lblPrg.Refresh()

            Application.DoEvents()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub Cargar_Datos()

        Dim clsTransaccion As New clsTransaccion
        Dim vRegularizarUpdateSuspendido As Boolean = False
        Dim vReservaUpdateSuspendido As Boolean = False
        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Cargando datos...")

        Try

            Dim DT As New DataTable

            clsTransaccion.Begin_Transaction()

            clsLnTrans_inv_ciclico.Get_All_Inventario_Regularizacion(gBeInventario.Idinventarioenc,
                                                                     prg,
                                                                     rdStockInventarioMovs.Checked,
                                                                     gBeInventario.Fec_agr,
                                                                     clsTransaccion.lConnection,
                                                                     clsTransaccion.lTransaction)

            lblPrg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            lblPrg.Caption = "Llenando grid..."
            lblPrg.Refresh()

            GridView1.BeginDataUpdate()
            vRegularizarUpdateSuspendido = True
            GridView1.GroupSummary.Clear()

            grdRegularizar.DataSource = clsLnTrans_inv_ciclico.Get_All_By_Comparacion_Inventario_A_Regularizar(gBeInventario.Idinventarioenc,
                                                                                                               clsTransaccion.lConnection,
                                                                                                               clsTransaccion.lTransaction)

            If GridView1.RowCount > 0 Then

                GridView1.OptionsView.ShowFooter = True
                GridView1.BestFitColumns(True)

                'GridView1.Columns("Código").Group()

                Dim item1 As New GridGroupSummaryItem() _
                With {.FieldName = "PesoConteo",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("PesoConteo")}
                GridView1.GroupSummary.Add(item1)

                Dim item3 As New GridGroupSummaryItem() _
                With {.FieldName = "PesoStock",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("PesoStock")}
                GridView1.GroupSummary.Add(item3)

                Dim item5 As New GridGroupSummaryItem() _
                With {.FieldName = "NuevoStock",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("NuevoStock")}
                GridView1.GroupSummary.Add(item5)

                Dim item6 As New GridGroupSummaryItem() _
                With {.FieldName = "DiferenciaCantidad",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("DiferenciaCantidad")}
                GridView1.GroupSummary.Add(item6)

                Dim item7 As New GridGroupSummaryItem() _
                With {.FieldName = "DiferenciaPeso",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("DiferenciaPeso")}
                GridView1.GroupSummary.Add(item7)

                Dim item10 As New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad_Reservada_UmBas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView1.Columns("Cantidad_Reservada_UmBas")}
                GridView1.GroupSummary.Add(item10)

                lblRegs.Caption = String.Format("Registros: {0}", GridView1.RowCount)

                GridView1.Columns("CantidadConteo_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadConteo_Pres").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("CantidadConteo_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadConteo_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("PesoConteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("PesoConteo").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("PesoConteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("PesoConteo").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("CantidadStock_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("CantidadStock_Pres").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("CantidadStock_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("CantidadStock_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("PesoStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("PesoStock").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("PesoStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("PesoStock").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Entradas_Salidas_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Entradas_Salidas_Pres").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Entradas_Salidas_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Entradas_Salidas_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("NuevoStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("NuevoStock").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("NuevoStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("NuevoStock").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("DiferenciaCantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("DiferenciaCantidad").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("DiferenciaCantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("DiferenciaCantidad").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("DiferenciaPeso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("DiferenciaPeso").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("DiferenciaPeso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("DiferenciaPeso").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Entradas_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Entradas_Pres").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Entradas_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Entradas_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Salidas_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Salidas_Pres").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Salidas_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Salidas_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView1.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"

                GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView1.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                GridView1.Columns("PesoConteo").Visible = False
                GridView1.Columns("PesoStock").Visible = False
                GridView1.Columns("DiferenciaPeso").Visible = False
                GridView1.ExpandAllGroups()

            End If

            Dim DTMov As DataTable = clsLnTrans_movimientos.Get_All_Movimientos_Reporte_By_Rango_Fechas_For_Inv(gBeInventario.Fec_agr,
                                                                                                                Now,
                                                                                                                gBeInventario.IdBodega,
                                                                                                                gBeInventario.Idinventarioenc,
                                                                                                                clsTransaccion.lConnection,
                                                                                                                clsTransaccion.lTransaction)

            dgridMovimientos.DataSource = DTMov

            If GridView2.RowCount > 0 Then

                GridView2.OptionsView.ShowFooter = True
                GridView2.BestFitColumns(True)

                GridView2.Columns("Codigo").Group()

                Dim item As New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = GridView2.Columns("Cantidad")}
                GridView2.GroupSummary.Add(item)

                GridView2.Columns("Cantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                GridView2.Columns("Cantidad").DisplayFormat.FormatString = "{0:n6}"
                GridView2.Columns("Cantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                GridView2.Columns("Cantidad").SummaryItem.DisplayFormat = "{0:n6}"

                GridView2.Columns("Fecha").DisplayFormat.FormatString = "G"

                GridView2.ExpandAllGroups()

            End If

            Dim DTNoRegularizar As DataTable = clsLnTrans_inv_ciclico.Get_All_By_Comparacion_Inventario_No_Regularizar(gBeInventario.Idinventarioenc,
                                                                                                                       clsTransaccion.lConnection,
                                                                                                                       clsTransaccion.lTransaction)

            grdvInventarioConReserva.BeginDataUpdate()
            vReservaUpdateSuspendido = True
            grdvInventarioConReserva.GroupSummary.Clear()

            grdInventarioConReserva.DataSource = DTNoRegularizar

            If (grdvInventarioConReserva.RowCount > 0) Then

                grdvInventarioConReserva.OptionsView.ShowFooter = True
                grdvInventarioConReserva.BestFitColumns(True)

                Dim item1 As New GridGroupSummaryItem() _
                With {.FieldName = "PesoConteo",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("PesoConteo")}
                grdvInventarioConReserva.GroupSummary.Add(item1)

                Dim item3 As New GridGroupSummaryItem() _
                With {.FieldName = "PesoStock",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("PesoStock")}

                Dim item5 As New GridGroupSummaryItem() _
                With {.FieldName = "NuevoStock",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("NuevoStock")}
                grdvInventarioConReserva.GroupSummary.Add(item5)

                Dim item6 As New GridGroupSummaryItem() _
                With {.FieldName = "DiferenciaCantidad",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("DiferenciaCantidad")}
                grdvInventarioConReserva.GroupSummary.Add(item6)

                Dim item7 As New GridGroupSummaryItem() _
                With {.FieldName = "DiferenciaPeso",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("DiferenciaPeso")}
                grdvInventarioConReserva.GroupSummary.Add(item7)

                Dim item10 As New GridGroupSummaryItem() _
                With {.FieldName = "Cantidad_Reservada_UmBas",
                .SummaryType = DevExpress.Data.SummaryItemType.Sum,
                .DisplayFormat = "{0:n6}",
                .ShowInGroupColumnFooter = grdvInventarioConReserva.Columns("Cantidad_Reservada_UmBas")}
                grdvInventarioConReserva.GroupSummary.Add(item10)

                lblRegs.Caption = String.Format("Registros: {0}", grdvInventarioConReserva.RowCount)

                grdvInventarioConReserva.Columns("CantidadConteo_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("CantidadConteo_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("CantidadConteo_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("CantidadConteo_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("PesoConteo").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("PesoConteo").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("PesoConteo").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("PesoConteo").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("CantidadStock_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("CantidadStock_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("CantidadStock_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("CantidadStock_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("PesoStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("PesoStock").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("PesoStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("PesoStock").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("Entradas_Salidas_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("Entradas_Salidas_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("Entradas_Salidas_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("Entradas_Salidas_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("NuevoStock").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("NuevoStock").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("NuevoStock").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("NuevoStock").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("DiferenciaCantidad").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("DiferenciaCantidad").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("DiferenciaCantidad").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("DiferenciaCantidad").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("DiferenciaPeso").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("DiferenciaPeso").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("DiferenciaPeso").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("DiferenciaPeso").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("Entradas_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("Entradas_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("Entradas_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("Entradas_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("Salidas_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("Salidas_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("Salidas_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("Salidas_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grdvInventarioConReserva.Columns("Cantidad_Reservada_Pres").DisplayFormat.FormatString = "{0:n6}"

                grdvInventarioConReserva.Columns("Cantidad_Reservada_Pres").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grdvInventarioConReserva.Columns("Cantidad_Reservada_Pres").SummaryItem.DisplayFormat = "{0:n6}"

                grdvInventarioConReserva.Columns("PesoConteo").Visible = False
                grdvInventarioConReserva.Columns("PesoStock").Visible = False
                grdvInventarioConReserva.Columns("DiferenciaPeso").Visible = False
                grdvInventarioConReserva.ExpandAllGroups()

            End If

            clsLnTempComparacionInventario.Insertar_Comparacion_Inventario(gBeInventario.Idinventarioenc,
                                                                           clsTransaccion.lConnection,
                                                                           clsTransaccion.lTransaction)

            clsLnTempComparacionInventario.Eliminar(gBeInventario.Idinventarioenc,
                                                    clsTransaccion.lConnection,
                                                    clsTransaccion.lTransaction)

            Carga_Regularizado_Consolidado(clsTransaccion.lConnection,
                                                    clsTransaccion.lTransaction)
            clsTransaccion.Commit_Transaction()

        Catch ex As Exception
            clsTransaccion.RollBack_Transaction()
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If vRegularizarUpdateSuspendido Then
                GridView1.EndDataUpdate()
            End If

            If vReservaUpdateSuspendido Then
                grdvInventarioConReserva.EndDataUpdate()
            End If

            SplashScreenManager.CloseForm(False)
            prg.Visible = False
            lblPrg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            clsTransaccion.Close_Conection()
        End Try

    End Sub

    Private Sub cmdRegularizar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdRegularizar.ItemClick

        Try

            If XtraMessageBox.Show("¿Iniciar proceso de regularizacion?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return
            If XtraMessageBox.Show("¡Este proceso no se puede revertir !" & vbCrLf & "¿Está seguro de continuar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

            If clsLnEmpresa.Get_Id_Motivo_Ajuste(AP.IdEmpresa) = 0 Then
                XtraMessageBox.Show("No tiene definido el IdMotivoAjuste por defecto.",
                   Text,
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation)
                Return
            End If

            If Aplica_Inventario() Then
                XtraMessageBox.Show("El inventario ha sido aplicado correctamente.",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)
                Close()
            Else
                XtraMessageBox.Show("No se pudo aplicar la regularización",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub Inserta_Detalle_Ajuste_Fecha(ByVal BeTransInvStockProd As clsBeTrans_inv_stock_prod,
                                            ByVal lConnection As SqlConnection,
                                            ByVal lTransaction As SqlTransaction,
                                            ByVal pIdAjusteEnc As Integer,
                                            ByVal IdPropietarioBodega As Integer)

        Try

            Dim Nuevo As New List(Of clsBeTrans_inv_stock_prod)
            Dim Congelado As New List(Of clsBeVW_stock_res)
            Dim pBeStock As New clsBeStock
            Dim pBeTransInvStock As New clsBeTrans_inv_stock
            Dim ListStockNuevo As New List(Of clsBeTrans_inv_stock)
            Dim AplicaPos As Boolean = False
            Dim AplicaNeg As Boolean = False
            Dim MaxId As Integer

            Dim Producto As New clsBeProducto

            Congelado = clsLnStock.Get_All_By_ProductoBodega_And_Lote(BeTransInvStockProd.IdProducto,
                                                                      BeTransInvStockProd.Lote,
                                                                      lConnection,
                                                                      lTransaction)

            SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)

            If Congelado.Count > 0 Then

                For Each BeVWStockRes In Congelado

                    Producto.IdProducto = BeTransInvStockProd.IdProducto

                    Producto = clsLnProducto.GetSingle(BeTransInvStockProd.IdProducto,
                                                       lConnection,
                                                       lTransaction)

                    If Producto.Codigo = "01007121" OrElse Producto.Codigo = "01007011" OrElse BeVWStockRes.IdStock = 4427 Then
                        'Debug.Print("Espera")
                    End If

                    If BeTransInvStockProd.Fecha_vence <> BeVWStockRes.Fecha_Vence Then

                        IdAjusteDet = clsLnTrans_ajuste_det.MaxID(lConnection,
                                                                  lTransaction) + 1

                        pBeAjusteDet = New clsBeTrans_ajuste_det

                        pBeAjusteDet.IdAjusteDet = IdAjusteDet
                        pBeAjusteDet.IdAjusteEnc = pIdAjusteEnc
                        pBeAjusteDet.IdPropietarioBodega = IdPropietarioBodega
                        pBeAjusteDet.IdProductoBodega = BeVWStockRes.IdProductoBodega
                        pBeAjusteDet.IdProductoEstado = BeVWStockRes.IdProductoEstado
                        pBeAjusteDet.IdPresentacion = BeVWStockRes.IdPresentacion
                        pBeAjusteDet.IdUnidadMedida = BeVWStockRes.IdUnidadMedida
                        pBeAjusteDet.IdUbicacion = BeVWStockRes.IdUbicacion
                        pBeAjusteDet.IdStock = BeVWStockRes.IdStock
                        pBeAjusteDet.Lote_original = BeVWStockRes.Lote
                        pBeAjusteDet.Lote_nuevo = vLote
                        pBeAjusteDet.Fecha_vence_original = BeVWStockRes.Fecha_Vence
                        pBeAjusteDet.Fecha_vence_nueva = BeTransInvStockProd.Fecha_vence
                        pBeAjusteDet.Peso_original = BeVWStockRes.Peso
                        pBeAjusteDet.Peso_nuevo = BeVWStockRes.Peso
                        pBeAjusteDet.Cantidad_original = BeVWStockRes.CantidadUmBas
                        pBeAjusteDet.Cantidad_nueva = BeVWStockRes.CantidadUmBas
                        pBeAjusteDet.Codigo_producto = Producto.Codigo
                        pBeAjusteDet.Nombre_producto = Producto.Nombre
                        '#EJC20180822:Id de motivo de ajuste por fecha de vencimiento (de momento fijo, migrar a parámetro)
                        pBeAjusteDet.IdMotivoAjuste = 1
                        pBeAjusteDet.Observacion = "Ajuste por fecha distinta con NAV"
                        '#EJC20180822:Id de tipo ajuste por fecha de vencimiento (de momento fijo, migrar a parámetro)
                        pBeAjusteDet.Idtipoajuste = 2

                        pBeAjusteDet.Codigo_ajuste = 15 '#EJC20180822:Id de codigo de ajuste por fecha de vencimiento (de momento fijo, migrar a parámetro)
                        pBeAjusteDet.Enviado = 1

                        clsLnTrans_ajuste_det.Insertar(pBeAjusteDet,
                                                       lConnection,
                                                       lTransaction)

                        pBeStock = New clsBeStock

                        With pBeStock
                            .IdStock = BeVWStockRes.IdStock
                            .Fecha_vence = BeTransInvStockProd.Fecha_vence
                        End With

                        clsLnStock.Actualizar_Fecha_Vencimiento(pBeStock,
                                                                lConnection,
                                                                lTransaction)

                        With pBeTransInvStock
                            .Idinventario = BeTransInvStockProd.Idinventario
                            .IdStock = BeVWStockRes.IdStock
                            .Fecha_vence = BeTransInvStockProd.Fecha_vence
                        End With

                        clsLnTrans_inv_stock.Actualizar_Fecha_Vencimiento(pBeTransInvStock,
                                                                          lConnection,
                                                                          lTransaction)

                        With pBeTransInvStock
                            .Idinventario = BeTransInvStockProd.Idinventario
                            .IdStock = BeVWStockRes.IdStock
                            .Fecha_vence = BeTransInvStockProd.Fecha_vence
                        End With

                        clsLnTrans_inv_ciclico.Actualizar_Fecha_Vencimiento(pBeTransInvStock,
                                                                            lConnection,
                                                                            lTransaction)

                        Debug.Print("Registro actualizado: " & pBeTransInvStock.IdStock & vbNewLine)
                        Debug.Print("Producto: " & Producto.Codigo)

                        AplicaNeg = True
                        AplicaPos = True

                        If AplicaPos Then

                            pBeMovs = New clsBeTrans_movimientos

                            MaxId = clsLnTrans_movimientos.MaxID(lConnection,
                                                                 lTransaction) + 1

                            pBeMovs.IdMovimiento = MaxId

                            pBeMovs.IdEmpresa = AP.IdEmpresa
                            pBeMovs.IdBodegaOrigen = AP.IdBodega
                            pBeMovs.IdTransaccion = gBeInventario.Idinventarioenc
                            pBeMovs.IdPropietarioBodega = IdPropietarioBodega
                            pBeMovs.IdProductoBodega = BeVWStockRes.IdProductoBodega
                            pBeMovs.IdUbicacionOrigen = BeVWStockRes.IdUbicacion
                            pBeMovs.IdUbicacionDestino = BeVWStockRes.IdUbicacion
                            pBeMovs.IdPresentacion = BeTransInvStockProd.IdPresentacion
                            pBeMovs.IdEstadoOrigen = BeVWStockRes.IdProductoEstado
                            pBeMovs.IdEstadoDestino = BeVWStockRes.IdProductoEstado
                            pBeMovs.IdUnidadMedida = Producto.IdUnidadMedidaBasica
                            pBeMovs.IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTNI '18 Ajuste Positivo por inventario 
                            pBeMovs.IdBodegaDestino = AP.IdBodega
                            pBeMovs.IdRecepcion = BeVWStockRes.IdRecepcionEnc
                            pBeMovs.IdRecepcionDet = BeVWStockRes.IdRecepcionDet
                            pBeMovs.Serie = ""
                            pBeMovs.Lote = vLote
                            pBeMovs.Fecha_vence = BeTransInvStockProd.Fecha_vence
                            pBeMovs.Fecha = Now
                            pBeMovs.Barra_pallet = BeVWStockRes.Lic_plate
                            pBeMovs.Hora_ini = Now
                            pBeMovs.Hora_fin = Now
                            pBeMovs.Fecha_agr = Now
                            pBeMovs.Usuario_agr = AP.UsuarioAp.IdUsuario
                            pBeMovs.Cantidad = BeVWStockRes.CantidadUmBas
                            pBeMovs.Cantidad_hist = BeVWStockRes.CantidadUmBas
                            pBeMovs.Peso = vPeso
                            pBeMovs.Peso_hist = BeTransInvStockProd.Peso

                            clsLnTrans_movimientos.Insertar(pBeMovs,
                                                            lConnection,
                                                            lTransaction)

                        End If

                        If AplicaNeg Then

                            pBeMovs = New clsBeTrans_movimientos

                            MaxId = clsLnTrans_movimientos.MaxID(lConnection,
                                                                 lTransaction) + 1

                            pBeMovs.IdMovimiento = MaxId
                            pBeMovs.IdEmpresa = AP.IdEmpresa
                            pBeMovs.IdBodegaOrigen = AP.IdBodega
                            pBeMovs.IdTransaccion = gBeInventario.Idinventarioenc
                            pBeMovs.IdPropietarioBodega = IdPropietarioBodega
                            pBeMovs.IdProductoBodega = BeVWStockRes.IdProductoBodega
                            pBeMovs.IdUbicacionOrigen = BeVWStockRes.IdUbicacion
                            pBeMovs.IdUbicacionDestino = BeVWStockRes.IdUbicacion
                            pBeMovs.IdPresentacion = BeTransInvStockProd.IdPresentacion
                            pBeMovs.IdEstadoOrigen = BeVWStockRes.IdProductoEstado
                            pBeMovs.IdEstadoDestino = BeVWStockRes.IdProductoEstado
                            pBeMovs.IdUnidadMedida = Producto.IdUnidadMedidaBasica
                            pBeMovs.IdTipoTarea = 19 'Ajuste Negativo por inventario 
                            pBeMovs.IdBodegaDestino = AP.IdBodega
                            pBeMovs.IdRecepcion = BeVWStockRes.IdRecepcionEnc
                            pBeMovs.IdRecepcionDet = BeVWStockRes.IdRecepcionDet
                            pBeMovs.Serie = ""
                            pBeMovs.Lote = vLote
                            pBeMovs.Fecha_vence = BeTransInvStockProd.Fecha_vence
                            pBeMovs.Fecha = Now
                            pBeMovs.Barra_pallet = BeVWStockRes.Lic_plate
                            pBeMovs.Hora_ini = Now
                            pBeMovs.Hora_fin = Now
                            pBeMovs.Fecha_agr = Now
                            pBeMovs.Usuario_agr = AP.UsuarioAp.IdUsuario
                            pBeMovs.Cantidad = BeVWStockRes.CantidadUmBas
                            pBeMovs.Cantidad_hist = BeVWStockRes.CantidadUmBas
                            pBeMovs.Peso = vPeso
                            pBeMovs.Peso_hist = BeTransInvStockProd.Peso

                            clsLnTrans_movimientos.Insertar(pBeMovs,
                                                            lConnection,
                                                            lTransaction)

                        End If

                        SplashScreenManager.Default.SetWaitFormDescription("Cargando datos..." & BeTransInvStockProd.IdProducto)
                        Application.DoEvents()

                    Else
                        SplashScreenManager.Default.SetWaitFormDescription("Omitiendo x fechas iguales..." & BeTransInvStockProd.IdProducto)
                        Application.DoEvents()
                        Debug.Print("Omitiendo x fechas iguales..." & BeTransInvStockProd.IdProducto)
                    End If

                Next

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub cmdImprimir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdImprimir.ItemClick
        Imprimir_Vista()
    End Sub

    Private Sub Imprimir_Vista()

        Try

            Dim printingSystem1 As New DevExpress.XtraPrinting.PrintingSystem()
            Dim printLink As New DevExpress.XtraPrinting.PrintableComponentLink()

            AddHandler printLink.CreateReportHeaderArea, AddressOf PrintableComponentLink_CreateReportHeaderArea

            Const leftColumnFoot As String = "Páginas: [Page # of Pages #] "
            Dim leftColumnHead As String = "Usuario: [User Name] - " & AP.UsuarioAp.Nombres

            Const rightColumn As String = "Fecha: [Date Printed] [Time Printed] "

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
            TryCast(printLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)

            phf.Header.Content.Clear()

            phf.Footer.Content.AddRange(New String() _
            {leftColumnFoot})
            phf.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Near

            phf.Header.Content.AddRange(New String() {leftColumnHead, "", rightColumn})
            phf.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Far

            printingSystem1.PageSettings.Landscape = True
            printLink.Component = grdRegularizar
            printLink.Landscape = True
            printLink.CreateDocument(printingSystem1)
            printingSystem1.PreviewFormEx.ShowDialog()
            printingSystem1.Dispose()

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub PrintableComponentLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)

        '#EJC20260602_PRINT_HELPER:
        'Cabecera base homologada para reportes de vista.
        clsUiPrintHelper.DrawStandardHeader(e,
                                            "Reporte de Inventario Ciclico",
                                            String.Format("Inventario: {0}", gBeInventario.Idinventarioenc),
                                            AP.NomBodega)

    End Sub

    Private Sub rdStockInventario_CheckedChanged(sender As Object, e As EventArgs) Handles rdStockInventario.CheckedChanged
        Cargar_Datos()
    End Sub

    Private Sub rdStockInventarioMovs_CheckedChanged(sender As Object, e As EventArgs) Handles rdStockInventarioMovs.CheckedChanged
        Cargar_Datos()
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle

        ' #EJC20260603_ROWSTYLE_PRINT_GUARD: evitar costo de formato por celda durante impresión.
        If clsUiPrintHelper.IsPrintingPreviewInProgress Then Exit Sub
        Try

            Dim View As GridView = sender
            Dim CantidadCont As Object = View.GetRowCellValue(e.RowHandle, View.Columns("CantidadConteo"))
            Dim CantDif As Object = View.GetRowCellValue(e.RowHandle, View.Columns("DiferenciaCantidad"))

            If e.Column.FieldName = "DiferenciaCantidad" Then

                If CantDif <> 0 Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                ElseIf CantDif = 0 Then
                    e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.White
                End If

            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Exportar_Excel(ByRef dGrid As GridControl, ByVal NomArchivo As String)

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
                    dGrid.ExportToXlsx(myStream)
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

    Private Sub mnuExportar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuExportar.ItemClick
        Exportar_Excel(grdRegularizar, "WMS_Regularización_Inventario_Ciclio.xlsx")
    End Sub

    Private Sub frmRegularizarInventario_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Try

            dtFecha.EditValue = gBeInventario.Fec_agr
            dtHora.EditValue = gBeInventario.Fec_agr
            IMS.Listar_BodegasPorPropietario(cmbBodega, gBeInventario.Idpropietario)

            cmbBodega.EditValue = gBeInventario.IdBodega

            Select Case Modo

                Case TipoTrans.Nuevo

                    clsLnTrans_inv_ciclico.Actualizar_Regularizar_By_IdInventarioEnc(gBeInventario.Idinventarioenc, True)

                    Cargar_Datos()

                Case TipoTrans.Editar

            End Select

            '#EJC20260602_GRID_COPY_HELPER:
            'Habilita menu contextual para copiar valores en grids de consulta.
            clsUiGridCopyHelper.Attach(GridView1, "Copiar")
            clsUiGridCopyHelper.Attach(GridView2, "Copiar")
            clsUiGridCopyHelper.Attach(GridViewRegularizado, "Copiar")

        Catch ex As Exception

            XtraMessageBox.Show(ex.Message,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

            Dim vMsgError As String = ex.Message
            clsLnLog_error_wms.Agregar_Error(vMsgError)

        End Try

    End Sub

    Private Sub cmdSalir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles cmdSalir.ItemClick
        Me.Close()
    End Sub

    Private Function Aplica_Inventario() As Boolean

        SplashScreenManager.ShowForm(Me, GetType(WaitForm), True, True, False)
        SplashScreenManager.Default.SetWaitFormDescription("Procesando ajustes de inventario...")

        Aplica_Inventario = False

        Dim clsTrans As New clsTransaccion()

        Try
            ' Inicializa conexión y transacción
            clsTrans.Open_Connection()
            clsTrans.Begin_Transaction()

            Limpiar_Caches_Regularizacion()
            lOperaciones.Clear()
            lBeTransAjusteDet.Clear()
            ListMovs.Clear()
            ListStockNuevo.Clear()

            Actualizar_Progreso_Regularizacion("Cargando inventario ciclico", 0, 1, True)

            ' Carga datos del inventario cíclico
            ListCiclico = clsLnTrans_inv_ciclico.Get_All_By_IdInventarioEnc(gBeInventario.Idinventarioenc,
                                                                            clsTrans.lConnection,
                                                                            clsTrans.lTransaction)

            '#AG27052026: Bloquea regularización si existe conteo menor a la cantidad reservada.
            Dim dtReservadoMenorConteo As DataTable =
            clsLnTrans_inv_ciclico.Get_Conteos_Menores_A_Reservado(gBeInventario.Idinventarioenc,
                                                           gBeInventario.IdBodega,
                                                           clsTrans.lConnection,
                                                           clsTrans.lTransaction)

            If dtReservadoMenorConteo IsNot Nothing AndAlso dtReservadoMenorConteo.Rows.Count > 0 Then

                Dim dr As DataRow = dtReservadoMenorConteo.Rows(0)

                Throw New Exception("No se puede regularizar. Existen productos con cantidad contada menor a la cantidad reservada." &
                        vbCrLf & vbCrLf &
                        String.Format("IdStock: {0} | Ubicación: {1} | Reservado: {2:n6} | Contado: {3:n6}",
                                      dr("IdStock"),
                                      dr("Ubicacion"),
                                      Convert.ToDouble(dr("CantidadReservada")),
                                      Convert.ToDouble(dr("CantidadContada"))))

            End If

            Actualizar_Progreso_Regularizacion("Clasificando ajustes", 0, Math.Max(ListCiclico.Count, 1), True)

            Dim vIdPropietarioBodega As Integer = clsLnPropietarios.Get_IdPropietarioBodega_By_IdBodega_And_IdPropietario(gBeInventario.IdBodega,
                                                                                                                          gBeInventario.Idpropietario,
                                                                                                                          clsTrans.lConnection,
                                                                                                                          clsTrans.lTransaction)
            Dim vIdProducto As Integer = 0

            '#AT20260212 Regularizacion Inv Talla Color
            Dim ajustesCantidad = ListCiclico.Where(Function(x) x.Cant_stock <> x.Cantidad And
                                                                x.Fecha_vence = x.Fecha_vence_stock AndAlso
                                                                (x.Lote = x.Lote_stock) AndAlso
                                                                (x.IdProductoEstado = x.IdProductoEst_nuevo OrElse x.IdProductoEst_nuevo = 0) AndAlso
                                                                (x.IdProductoTallaColor = x.IdProductoTallaColor_nuevo OrElse x.IdProductoTallaColor_nuevo = 0) AndAlso
                                                                x.Regularizar = True).ToList()

            Dim ListaExcluyente = ListCiclico.ToList

            Dim ajustesVencimiento = ListaExcluyente.Where(Function(x) x.Fecha_vence <> x.Fecha_vence_stock).ToList()
            ListaExcluyente = ListaExcluyente.Except(ajustesVencimiento).ToList

            Dim ajustesLote = ListaExcluyente.Where(Function(x) x.Lote <> x.Lote_stock).ToList()
            ListaExcluyente = ListaExcluyente.Except(ajustesLote).ToList

            Dim ajustesEstado = ListaExcluyente.Where(Function(x) x.IdProductoEstado <> x.IdProductoEst_nuevo AndAlso
                                                                  x.IdProductoEst_nuevo <> 0).ToList()

            Dim ajusteTallaColor = ListaExcluyente.Where(Function(x) x.IdProductoTallaColor <> x.IdProductoTallaColor_nuevo).ToList()

            Dim ajustesPositivos = ajustesCantidad.Where(Function(x) x.Cantidad > x.Cant_stock OrElse x.IdUbicacion_nuevo <> 0 AndAlso x.Regularizar = True).ToList()
            Dim ajustesNegativos = ajustesCantidad.Where(Function(x) x.Cantidad < x.Cant_stock AndAlso x.Regularizar = True).ToList().Except(ajustesPositivos).ToList()

            mRegularizacionTotalAjustes = ajustesVencimiento.Count +
                                         ajustesLote.Count +
                                         ajustesEstado.Count +
                                         ajusteTallaColor.Count +
                                         ajustesPositivos.Count +
                                         ajustesNegativos.Count
            mRegularizacionProcesados = 0
            Actualizar_Progreso_Regularizacion("Preparando ajustes", 0, mRegularizacionTotalAjustes, True)

            ' Procesar ajustes por tipo
            If ajustesVencimiento.Any() Then
                ProcesarAjustePorTipo(ajustesVencimiento, clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento, clsTrans, vIdPropietarioBodega, "Vencimiento")
            End If

            If ajustesLote.Any() Then
                ProcesarAjustePorTipo(ajustesLote, clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote, clsTrans, vIdPropietarioBodega, "Lote")
            End If

            If ajustesEstado.Any() Then
                ProcesarAjustePorTipo(ajustesEstado, clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado, clsTrans, vIdPropietarioBodega, "Estado")
            End If

            If ajusteTallaColor.Any() Then
                ProcesarAjustePorTipo(ajusteTallaColor, clsDataContractDI.tTipoAjusteWMS.Ajuste_TallaColor, clsTrans, vIdPropietarioBodega, "TallaColor")
            End If

            If ajustesCantidad.Any() Then

                If ajustesPositivos.Any() Then
                    ProcesarAjustePorTipo(ajustesPositivos, clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo, clsTrans, vIdPropietarioBodega, "Positivo")
                End If

                If ajustesNegativos.Any() Then
                    ProcesarAjustePorTipo(ajustesNegativos, clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo, vIdPropietarioBodega, "Negativo", clsTrans)
                End If

            End If

            Actualizar_Progreso_Regularizacion("Aplicando inventario", mRegularizacionProcesados, Math.Max(mRegularizacionTotalAjustes, 1), True)

            ' Regularizar inventario
            clsLnTrans_inv_ciclico.Regularizar_Inventario(gBeInventario,
                                                          ListStockNuevo,
                                                          ListMovs,
                                                          AP.UsuarioAp,
                                                          lBeTransAjusteDet,
                                                          lOperaciones,
                                                          AP.Bodega.codigo_bodega_erp,
                                                          AP.Bodega.Control_Talla_Color,
                                                          clsTrans.lConnection,
                                                          clsTrans.lTransaction)

            ' Confirmar transacción
            clsTrans.Commit_Transaction()

            Actualizar_Progreso_Regularizacion("Regularizacion finalizada", Math.Max(mRegularizacionTotalAjustes, 1), Math.Max(mRegularizacionTotalAjustes, 1), True)

            Aplica_Inventario = True

        Catch ex As Exception
            ' Revertir transacción en caso de error
            clsTrans.RollBack_Transaction()
            Throw New Exception($"Error en Aplica_Inventario: {ex.Message}")
        Finally
            ' Cerrar conexión y pantalla de carga
            clsTrans.Close_Conection()
            SplashScreenManager.CloseForm(False)
        End Try

        Return Aplica_Inventario

    End Function

    Private Sub ProcesarAjustePorTipo(ByVal ajustes As List(Of clsBeTrans_inv_ciclico),
                                      ByVal tipoAjuste As Integer,
                                      ByVal IdPropietarioBodega As Integer,
                                      ByVal pReferencia As String,
                                      ByVal clsTrans As clsTransaccion)


        Try

            ' Crear encabezado de ajuste
            Dim idAjusteEnc As Integer = 0

            idAjusteEnc = clsLnTrans_ajuste_enc.Inserta_Encabezado_Ajuste(gBeInventario.Idinventarioenc,
                                                                          AP.UsuarioAp,
                                                                          gBeInventario.IdBodega,
                                                                          IdPropietarioBodega,
                                                                          gBeInventario.Idinventarioenc,
                                                                          gBeInventario.IdCentroCosto,
                                                                          pReferencia,
                                                                          clsTrans.lConnection,
                                                                          clsTrans.lTransaction)

            ' Procesar cada ajuste
            For Each item In ajustes
                mRegularizacionProcesados += 1
                Actualizar_Progreso_Regularizacion("Generando ajuste " & pReferencia,
                                                   mRegularizacionProcesados,
                                                   Math.Max(mRegularizacionTotalAjustes, 1))

                LLena_Objetos_Detalle_Ajuste(item,
                                             IdPropietarioBodega,
                                             clsTrans.lConnection,
                                             clsTrans.lTransaction,
                                             lBeTransAjusteDet,
                                             ListMovs,
                                             idAjusteEnc,
                                             tipoAjuste)
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub LLena_Objetos_Detalle_Ajuste(ByVal BeTransInvCiclico As clsBeTrans_inv_ciclico,
                                             ByVal IdPropietarioBodega As Integer,
                                             ByVal lConnection As SqlConnection,
                                             ByVal lTransaction As SqlTransaction,
                                             ByRef lBeTransAjusteDet As List(Of clsBeTrans_ajuste_det),
                                             ByRef ListMovs As List(Of clsBeTrans_movimientos),
                                             ByVal IdAjusteEnc As Integer,
                                             ByVal TipoAjuste As Integer)

        Try
            ' Inicializar IDs y objetos necesarios
            Dim IdAjusteDet As Integer = clsLnTrans_ajuste_det.MaxID(lConnection, lTransaction) + 1
            Dim pBeAjusteDet As New clsBeTrans_ajuste_det
            Dim pBeMovs As New clsBeTrans_movimientos
            Dim vIdMotivoAjuste As Integer = Get_IdMotivoAjuste_Regularizacion(lConnection, lTransaction)

            ' Configuración de tareas según el tipo de ajuste
            Dim IdTipoTarea As Integer
            Select Case TipoAjuste
                Case clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote
                    IdTipoTarea = clsDataContractDI.tTipoTarea.AJLOTE
                Case clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento
                    IdTipoTarea = clsDataContractDI.tTipoTarea.AJVENC
                Case clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo
                    IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTPI
                Case clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo
                    IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTNI
                Case clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado
                    IdTipoTarea = clsDataContractDI.tTipoTarea.CESTI
                Case Else
                    Throw New Exception("Tipo de ajuste desconocido.")
            End Select

            ' Llenar los detalles del ajuste
            pBeAjusteDet.IdAjusteDet = IdAjusteDet
            pBeAjusteDet.IdAjusteEnc = IdAjusteEnc
            pBeAjusteDet.IdStock = BeTransInvCiclico.IdStock
            pBeAjusteDet.IdPropietarioBodega = IdPropietarioBodega
            pBeAjusteDet.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
            pBeAjusteDet.IdProductoEstado = BeTransInvCiclico.IdProductoEst_nuevo
            pBeAjusteDet.IdPresentacion = BeTransInvCiclico.IdPresentacion
            pBeAjusteDet.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
            pBeAjusteDet.IdUbicacion = BeTransInvCiclico.IdUbicacion
            pBeAjusteDet.Lote_original = BeTransInvCiclico.Lote_stock
            pBeAjusteDet.Lote_nuevo = BeTransInvCiclico.Lote
            pBeAjusteDet.Fecha_vence_original = BeTransInvCiclico.Fecha_vence_stock
            pBeAjusteDet.Fecha_vence_nueva = BeTransInvCiclico.Fecha_vence
            pBeAjusteDet.Peso_original = BeTransInvCiclico.Peso_stock
            pBeAjusteDet.Peso_nuevo = BeTransInvCiclico.Peso
            pBeAjusteDet.Cantidad_original = BeTransInvCiclico.Cant_stock
            pBeAjusteDet.Cantidad_nueva = BeTransInvCiclico.Cantidad
            pBeAjusteDet.Idtipoajuste = TipoAjuste
            pBeAjusteDet.Codigo_ajuste = IdTipoTarea
            pBeAjusteDet.Enviado = True

            Dim BodegaERP As Integer = Get_IdBodegaERP_Regularizacion(AP.Bodega.codigo_bodega_erp,
                                                                      lConnection,
                                                                      lTransaction)

            pBeAjusteDet.IdBodegaERP = Val(BodegaERP)

            pBeAjusteDet.lic_plate = BeTransInvCiclico.lic_plate

            Dim vProducto As clsBeProducto = Get_Producto_Regularizacion(BeTransInvCiclico.IdProductoBodega,
                                                                         lConnection,
                                                                         lTransaction)

            pBeAjusteDet.Codigo_producto = vProducto.IdProducto
            pBeAjusteDet.Codigo_producto = vProducto.Codigo
            pBeAjusteDet.Nombre_producto = vProducto.Nombre

            '#AT20260121 Primera vez viendo regularizacion de inventario ciclico, procedo a llenar talla y color
            If AP.Bodega.Control_Talla_Color Then
                If BeTransInvCiclico.IdProductoTallaColor <> 0 Then
                    Dim BeTallaColor = Get_TallaColor_Regularizacion(BeTransInvCiclico.IdProductoTallaColor, lConnection, lTransaction)

                    If BeTallaColor IsNot Nothing Then
                        pBeAjusteDet.IdProductoTallaColor_origen = BeTransInvCiclico.IdProductoTallaColor
                        pBeAjusteDet.Talla_origen = Get_TallaCodigo_Regularizacion(BeTallaColor.IdTalla, lConnection, lTransaction)
                        pBeAjusteDet.Color_origen = Get_ColorCodigo_Regularizacion(BeTallaColor.IdColor, lConnection, lTransaction)
                    End If
                End If

                If BeTransInvCiclico.IdProductoTallaColor_nuevo <> 0 Then
                    Dim BeTallaColor = Get_TallaColor_Regularizacion(BeTransInvCiclico.IdProductoTallaColor_nuevo, lConnection, lTransaction)

                    If BeTallaColor IsNot Nothing Then
                        pBeAjusteDet.IdProductoTallaColor_destino = BeTransInvCiclico.IdProductoTallaColor
                        pBeAjusteDet.Talla_destino = Get_TallaCodigo_Regularizacion(BeTallaColor.IdTalla, lConnection, lTransaction)
                        pBeAjusteDet.Color_destino = Get_ColorCodigo_Regularizacion(BeTallaColor.IdColor, lConnection, lTransaction)
                    End If
                End If
            End If

            '#CKFK20250130 Cambié que esnuevolink solo sea pora los ajustes que no sean por cantidad
            If TipoAjuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo OrElse TipoAjuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo Then
                pBeAjusteDet.esnuevolink = 0
            Else
                pBeAjusteDet.esnuevolink = 1
            End If

            pBeAjusteDet.IdMotivoAjuste = vIdMotivoAjuste

            lBeTransAjusteDet.Add(pBeAjusteDet)
            clsLnTrans_ajuste_det.Insertar(pBeAjusteDet, lConnection, lTransaction)

            ' Llenar los movimientos asociados
            pBeMovs.IdMovimiento = clsLnTrans_movimientos.MaxID(lConnection, lTransaction)
            pBeMovs.IdEmpresa = AP.IdEmpresa
            pBeMovs.IdBodegaOrigen = AP.IdBodega
            pBeMovs.IdBodegaDestino = AP.IdBodega
            pBeMovs.IdTransaccion = gBeInventario.Idinventarioenc
            pBeMovs.IdPropietarioBodega = IdPropietarioBodega
            pBeMovs.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
            pBeMovs.IdUbicacionOrigen = BeTransInvCiclico.IdUbicacion
            If BeTransInvCiclico.IdUbicacion_nuevo <> 0 Then
                pBeMovs.IdUbicacionDestino = BeTransInvCiclico.IdUbicacion_nuevo
            Else
                pBeMovs.IdUbicacionDestino = BeTransInvCiclico.IdUbicacion
            End If
            pBeMovs.IdPresentacion = BeTransInvCiclico.IdPresentacion
            pBeMovs.IdEstadoOrigen = BeTransInvCiclico.IdProductoEstado
            pBeMovs.IdEstadoDestino = BeTransInvCiclico.IdProductoEst_nuevo
            pBeMovs.Lote = BeTransInvCiclico.Lote
            pBeMovs.Fecha_vence = BeTransInvCiclico.Fecha_vence
            pBeMovs.Cantidad = BeTransInvCiclico.Cantidad
            pBeMovs.Cantidad_hist = BeTransInvCiclico.Cant_stock
            pBeMovs.Peso = BeTransInvCiclico.Peso
            pBeMovs.Peso_hist = BeTransInvCiclico.Peso_stock
            pBeMovs.IdTipoTarea = IdTipoTarea
            pBeMovs.Fecha = DateTime.Now
            pBeMovs.Hora_ini = DateTime.Now
            pBeMovs.Hora_fin = DateTime.Now
            pBeMovs.Fecha_agr = DateTime.Now
            pBeMovs.Usuario_agr = AP.UsuarioAp.IdUsuario
            pBeMovs.Barra_pallet = BeTransInvCiclico.lic_plate
            pBeMovs.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
            pBeMovs.IdProductoTallaColor = BeTransInvCiclico.IdProductoTallaColor

            If BeTransInvCiclico.IdProductoTallaColor <> 0 Then
                Dim BEProductoTallaColor As clsBeProducto_talla_color = Get_TallaColor_Regularizacion(BeTransInvCiclico.IdProductoTallaColor, lConnection, lTransaction)
                pBeMovs.Talla = If(BEProductoTallaColor Is Nothing, "", Get_TallaCodigo_Regularizacion(BEProductoTallaColor.IdTalla, lConnection, lTransaction))
                pBeMovs.Color = If(BEProductoTallaColor Is Nothing, "", Get_ColorCodigo_Regularizacion(BEProductoTallaColor.IdColor, lConnection, lTransaction))
            Else
                pBeMovs.Talla = ""
                pBeMovs.Color = ""
            End If

            ListMovs.Add(pBeMovs)
            clsLnTrans_movimientos.Insertar(pBeMovs, lConnection, lTransaction)

        Catch ex As Exception
            Throw New Exception($"Error en LLena_Objetos_Detalle_Ajuste: {ex.Message}")
        End Try

    End Sub

    Private Sub ProcesarAjustePorTipo(ajustes As List(Of clsBeTrans_inv_ciclico),
                                      tipoAjuste As clsDataContractDI.tTipoAjusteWMS,
                                      clsTrans As clsTransaccion,
                                      vIdPropietarioBodega As Integer,
                                      pReferencia As String)

        Dim vIdStock As Integer = 0
        Dim vIdStockLista As Integer = 0
        Dim vIdMotivoAjuste As Integer = 0

        Try

            ' Crear encabezado para el ajuste
            pBeTransAjustEnc = New clsBeTrans_ajuste_enc()
            pBeTransAjustEnc.IdAjusteenc = clsLnTrans_ajuste_enc.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
            pBeTransAjustEnc.IdBodega = gBeInventario.IdBodega
            pBeTransAjustEnc.Idusuario = AP.UsuarioAp.IdUsuario
            pBeTransAjustEnc.Fecha = Date.Now
            pBeTransAjustEnc.IdCentroCosto = gBeInventario.IdCentroCosto
            pBeTransAjustEnc.IdPropietarioBodega = vIdPropietarioBodega
            pBeTransAjustEnc.User_agr = AP.UsuarioAp.Nombres
            pBeTransAjustEnc.User_mod = AP.UsuarioAp.Nombres
            pBeTransAjustEnc.Referencia = $"Ajuste {pReferencia} generado por inventario No. {gBeInventario.Idinventarioenc}"
            pBeTransAjustEnc.Ajuste_Por_Inventario = gBeInventario.Idinventarioenc
            pBeTransAjustEnc.Enviado_A_ERP = True
            pBeTransAjustEnc.Centro_Costo_Erp = AP.Bodega.Centro_Costo_Erp
            pBeTransAjustEnc.Centro_Costo_Dir_Erp = AP.Bodega.Centro_Costo_Dir_Erp
            pBeTransAjustEnc.Centro_Costo_Dep_Erp = AP.Bodega.Centro_Costo_Dep_Erp

            pBeAjusteDet.IdMotivoAjuste = 0
            vIdMotivoAjuste = Get_IdMotivoAjuste_Regularizacion(clsTrans.lConnection, clsTrans.lTransaction)

            ' Insertar encabezado
            clsLnTrans_ajuste_enc.Insertar(pBeTransAjustEnc, clsTrans.lConnection, clsTrans.lTransaction)

            ' Procesar los detalles de los ajustes
            For Each BeTransInvCiclico In ajustes
                mRegularizacionProcesados += 1
                Actualizar_Progreso_Regularizacion("Generando ajuste " & pReferencia,
                                                   mRegularizacionProcesados,
                                                   Math.Max(mRegularizacionTotalAjustes, 1))

                ' Crear detalle del ajuste
                Dim pBeAjusteDet As New clsBeTrans_ajuste_det()
                pBeAjusteDet.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                pBeAjusteDet.IdAjusteEnc = pBeTransAjustEnc.IdAjusteenc
                pBeAjusteDet.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
                pBeAjusteDet.IdProductoEstado = BeTransInvCiclico.IdProductoEst_nuevo
                pBeAjusteDet.IdPresentacion = BeTransInvCiclico.IdPresentacion
                pBeAjusteDet.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
                pBeAjusteDet.IdUbicacion = BeTransInvCiclico.IdUbicacion
                pBeAjusteDet.Cantidad_original = BeTransInvCiclico.Cant_stock
                pBeAjusteDet.Cantidad_nueva = BeTransInvCiclico.Cantidad
                pBeAjusteDet.Lote_original = BeTransInvCiclico.Lote_stock
                pBeAjusteDet.Lote_nuevo = BeTransInvCiclico.Lote
                pBeAjusteDet.Fecha_vence_original = BeTransInvCiclico.Fecha_vence_stock
                pBeAjusteDet.Fecha_vence_nueva = BeTransInvCiclico.Fecha_vence
                pBeAjusteDet.Idtipoajuste = tipoAjuste
                pBeAjusteDet.lic_plate = BeTransInvCiclico.lic_plate
                pBeAjusteDet.Enviado = True

                If clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado = tipoAjuste Then
                    pBeAjusteDet.Codigo_ajuste = clsDataContractDI.tTipoTarea.CESTI
                ElseIf clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote = tipoAjuste Then
                    pBeAjusteDet.Codigo_ajuste = clsDataContractDI.tTipoTarea.AJLOTEPI
                ElseIf clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento = tipoAjuste Then
                    pBeAjusteDet.Codigo_ajuste = clsDataContractDI.tTipoTarea.AJVENCEPI
                ElseIf clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo = tipoAjuste Then
                    pBeAjusteDet.Codigo_ajuste = clsDataContractDI.tTipoTarea.AJCANTPI
                ElseIf clsDataContractDI.tTipoAjusteWMS.Ajuste_TallaColor = tipoAjuste Then
                    pBeAjusteDet.Codigo_ajuste = clsDataContractDI.tTipoTarea.AJCANTPI
                End If

                pBeAjusteDet.Peso_original = BeTransInvCiclico.Peso
                pBeAjusteDet.Peso_nuevo = BeTransInvCiclico.Peso
                pBeAjusteDet.lic_plate = BeTransInvCiclico.lic_plate
                pBeAjusteDet.IdStock = BeTransInvCiclico.IdStock

                Dim vProducto As clsBeProducto = Get_Producto_Regularizacion(BeTransInvCiclico.IdProductoBodega,
                                                                             clsTrans.lConnection,
                                                                             clsTrans.lTransaction)

                pBeAjusteDet.Codigo_producto = vProducto.IdProducto
                pBeAjusteDet.Codigo_producto = vProducto.Codigo
                pBeAjusteDet.Nombre_producto = vProducto.Nombre
                pBeAjusteDet.IdMotivoAjuste = vIdMotivoAjuste

                Dim BodegaERP As Integer = Get_IdBodegaERP_Regularizacion(AP.Bodega.codigo_bodega_erp,
                                                                          clsTrans.lConnection,
                                                                          clsTrans.lTransaction)

                '#AT20260121 Primera vez viendo regularizacion de inventario ciclico, procedo a llenar talla y color
                If AP.Bodega.Control_Talla_Color Then
                    If BeTransInvCiclico.IdProductoTallaColor <> 0 Then
                        Dim BeTallaColor = Get_TallaColor_Regularizacion(BeTransInvCiclico.IdProductoTallaColor, clsTrans.lConnection, clsTrans.lTransaction)

                        If BeTallaColor IsNot Nothing Then
                            pBeAjusteDet.IdProductoTallaColor_origen = BeTransInvCiclico.IdProductoTallaColor
                            pBeAjusteDet.Talla_origen = Get_TallaCodigo_Regularizacion(BeTallaColor.IdTalla, clsTrans.lConnection, clsTrans.lTransaction)
                            pBeAjusteDet.Color_origen = Get_ColorCodigo_Regularizacion(BeTallaColor.IdColor, clsTrans.lConnection, clsTrans.lTransaction)
                        End If
                    End If

                    If BeTransInvCiclico.IdProductoTallaColor_nuevo <> 0 Then
                        Dim BeTallaColor = Get_TallaColor_Regularizacion(BeTransInvCiclico.IdProductoTallaColor_nuevo, clsTrans.lConnection, clsTrans.lTransaction)

                        If BeTallaColor IsNot Nothing Then
                            pBeAjusteDet.IdProductoTallaColor_destino = BeTransInvCiclico.IdProductoTallaColor
                            pBeAjusteDet.Talla_destino = Get_TallaCodigo_Regularizacion(BeTallaColor.IdTalla, clsTrans.lConnection, clsTrans.lTransaction)
                            pBeAjusteDet.Color_destino = Get_ColorCodigo_Regularizacion(BeTallaColor.IdColor, clsTrans.lConnection, clsTrans.lTransaction)
                        End If
                    End If
                End If

                pBeAjusteDet.IdBodegaERP = Val(BodegaERP)

                ' Insertar detalle
                If BeTransInvCiclico.Fecha_vence <> BeTransInvCiclico.Fecha_vence_stock Then
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento
                    clsLnTrans_ajuste_det.Insertar(pBeAjusteDet, clsTrans.lConnection, clsTrans.lTransaction)
                End If

                If BeTransInvCiclico.Lote_stock <> BeTransInvCiclico.Lote Then

                    Dim pBeAjusteDetLote As New clsBeTrans_ajuste_det

                    clsPublic.CopyObject(pBeAjusteDet, pBeAjusteDetLote)

                    pBeAjusteDetLote.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeAjusteDetLote.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote

                    clsLnTrans_ajuste_det.Insertar(pBeAjusteDetLote, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If BeTransInvCiclico.IdProductoEstado <> BeTransInvCiclico.IdProductoEst_nuevo Then

                    Dim pBeAjusteDetEstado As New clsBeTrans_ajuste_det

                    clsPublic.CopyObject(pBeAjusteDet, pBeAjusteDetEstado)

                    pBeAjusteDetEstado.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeAjusteDetEstado.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado

                    clsLnTrans_ajuste_det.Insertar(pBeAjusteDetEstado, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                '#AT20260213 Ajuste talla y color deber ir positivo
                If BeTransInvCiclico.IdProductoTallaColor <> BeTransInvCiclico.IdProductoTallaColor_nuevo Then

                    Dim pBeAjusteDetEstado As New clsBeTrans_ajuste_det

                    clsPublic.CopyObject(pBeAjusteDet, pBeAjusteDetEstado)

                    pBeAjusteDetEstado.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeAjusteDetEstado.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo

                    clsLnTrans_ajuste_det.Insertar(pBeAjusteDetEstado, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If tipoAjuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo Then

                    Dim pBeAjusteDetNegativo As New clsBeTrans_ajuste_det

                    clsPublic.CopyObject(pBeAjusteDet, pBeAjusteDetNegativo)

                    pBeAjusteDetNegativo.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeAjusteDetNegativo.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo

                    clsLnTrans_ajuste_det.Insertar(pBeAjusteDetNegativo, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If tipoAjuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo Then

                    Dim pBeAjusteDetPositivo As New clsBeTrans_ajuste_det

                    clsPublic.CopyObject(pBeAjusteDet, pBeAjusteDetPositivo)

                    pBeAjusteDetPositivo.IdAjusteDet = clsLnTrans_ajuste_det.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeAjusteDetPositivo.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo

                    clsLnTrans_ajuste_det.Insertar(pBeAjusteDetPositivo, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                ' Insertar movimiento directamente
                Dim pBeMovs As New clsBeTrans_movimientos()
                pBeMovs = New clsBeTrans_movimientos()
                pBeMovs.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                pBeMovs.IdEmpresa = AP.IdEmpresa
                pBeMovs.IdBodegaOrigen = AP.IdBodega
                pBeMovs.IdTransaccion = gBeInventario.Idinventarioenc
                pBeMovs.IdPropietarioBodega = vIdPropietarioBodega
                pBeMovs.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
                pBeMovs.IdUbicacionOrigen = BeTransInvCiclico.IdUbicacion
                pBeMovs.IdUbicacionDestino = BeTransInvCiclico.IdUbicacion
                pBeMovs.IdPresentacion = BeTransInvCiclico.IdPresentacion
                pBeMovs.IdEstadoOrigen = BeTransInvCiclico.IdProductoEstado
                pBeMovs.IdEstadoDestino = BeTransInvCiclico.IdProductoEst_nuevo
                pBeMovs.IdUnidadMedida = Producto.IdUnidadMedidaBasica
                pBeMovs.IdTipoTarea = IdTipoTarea
                pBeMovs.IdBodegaDestino = AP.IdBodega
                pBeMovs.IdRecepcion = 0
                pBeMovs.IdRecepcionDet = 0
                pBeMovs.Serie = ""
                pBeMovs.Lote = BeTransInvCiclico.Lote_stock
                pBeMovs.Fecha_vence = BeTransInvCiclico.Fecha_vence_stock
                pBeMovs.Fecha = Now
                pBeMovs.Barra_pallet = BeTransInvCiclico.lic_plate
                pBeMovs.Hora_ini = Now
                pBeMovs.Hora_fin = Now
                pBeMovs.Fecha_agr = Now
                pBeMovs.Usuario_agr = AP.UsuarioAp.IdUsuario

                '#CM_20180821_426PM: Cantidad correcta para movimientos de ajustes. 
                If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo Then
                    If BeTransInvCiclico.Cantidad < BeTransInvCiclico.Cant_stock Then
                        pBeMovs.Cantidad = BeTransInvCiclico.Cantidad
                    Else
                        pBeMovs.Cantidad = Math.Round((BeTransInvCiclico.Cantidad - BeTransInvCiclico.Cant_stock), 6)
                    End If
                ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo Then
                    pBeMovs.Cantidad = Math.Round((BeTransInvCiclico.Cant_stock - BeTransInvCiclico.Cantidad), 6)
                ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote OrElse
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento OrElse
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado OrElse
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_TallaColor Then
                    pBeMovs.Cantidad = BeTransInvCiclico.Cantidad

                    If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote Then
                        pBeMovs.IdTipoTarea = clsDataContractDI.tTipoTarea.AJLOTEPI
                    ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento Then
                        pBeMovs.IdTipoTarea = clsDataContractDI.tTipoTarea.AJVENCEPI
                    ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado Then
                        pBeMovs.IdTipoTarea = clsDataContractDI.tTipoTarea.CESTI
                    End If

                End If

                pBeMovs.Cantidad_hist = BeTransInvCiclico.Cant_stock
                pBeMovs.Peso = vPeso
                pBeMovs.Peso_hist = BeTransInvCiclico.Peso_stock
                pBeMovs.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
                pBeMovs.IdPresentacion = BeTransInvCiclico.IdPresentacion

                If BeTransInvCiclico.Fecha_vence <> BeTransInvCiclico.Fecha_vence_stock Then
                    pBeMovs.IdTipoTarea = clsDataContractDI.tTipoTarea.AJVENCEPI
                    clsLnTrans_movimientos.Insertar(pBeMovs, clsTrans.lConnection, clsTrans.lTransaction)
                End If

                '#CKFK20241202
                '****************************
                If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote OrElse
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento OrElse
                    pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado Then

                    Dim pBeMovsInverso As New clsBeTrans_movimientos()
                    pBeMovsInverso = New clsBeTrans_movimientos()
                    pBeMovsInverso.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction)
                    pBeMovsInverso.IdEmpresa = AP.IdEmpresa
                    pBeMovsInverso.IdBodegaOrigen = AP.IdBodega
                    pBeMovsInverso.IdTransaccion = gBeInventario.Idinventarioenc
                    pBeMovsInverso.IdPropietarioBodega = vIdPropietarioBodega
                    pBeMovsInverso.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
                    pBeMovsInverso.IdUbicacionOrigen = BeTransInvCiclico.IdUbicacion
                    pBeMovsInverso.IdUbicacionDestino = BeTransInvCiclico.IdUbicacion
                    pBeMovsInverso.IdPresentacion = BeTransInvCiclico.IdPresentacion
                    pBeMovsInverso.IdEstadoOrigen = BeTransInvCiclico.IdProductoEstado
                    pBeMovsInverso.IdEstadoDestino = BeTransInvCiclico.IdProductoEst_nuevo
                    pBeMovsInverso.IdUnidadMedida = Producto.IdUnidadMedidaBasica
                    pBeMovsInverso.IdTipoTarea = IdTipoTarea
                    pBeMovsInverso.IdBodegaDestino = AP.IdBodega
                    pBeMovsInverso.IdRecepcion = 0
                    pBeMovsInverso.IdRecepcionDet = 0
                    pBeMovsInverso.Serie = ""
                    pBeMovsInverso.Lote = BeTransInvCiclico.Lote_stock
                    pBeMovsInverso.Fecha_vence = BeTransInvCiclico.Fecha_vence_stock
                    pBeMovsInverso.Fecha = Now
                    pBeMovsInverso.Barra_pallet = BeTransInvCiclico.lic_plate
                    pBeMovsInverso.Hora_ini = Now
                    pBeMovsInverso.Hora_fin = Now
                    pBeMovsInverso.Fecha_agr = Now
                    pBeMovsInverso.Usuario_agr = AP.UsuarioAp.IdUsuario

                    pBeMovsInverso.Cantidad = BeTransInvCiclico.Cantidad

                    If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Lote Then
                        pBeMovsInverso.IdTipoTarea = clsDataContractDI.tTipoTarea.AJLOTENI
                    ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Vencimiento Then
                        pBeMovsInverso.IdTipoTarea = clsDataContractDI.tTipoTarea.AJVENCENI
                    ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado Then
                        pBeMovsInverso.IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTNI
                    ElseIf pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Estado Then
                        pBeMovsInverso.IdTipoTarea = clsDataContractDI.tTipoTarea.TALLACOLOR
                    End If

                    pBeMovsInverso.Cantidad_hist = BeTransInvCiclico.Cant_stock
                    pBeMovsInverso.Peso = vPeso
                    pBeMovsInverso.Peso_hist = BeTransInvCiclico.Peso_stock
                    pBeMovsInverso.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
                    pBeMovsInverso.IdPresentacion = BeTransInvCiclico.IdPresentacion

                    clsLnTrans_movimientos.Insertar(pBeMovsInverso, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                '****************************

                If BeTransInvCiclico.Lote_stock <> BeTransInvCiclico.Lote AndAlso BeTransInvCiclico.Lote <> "" Then

                    Dim pBeMovsLote As New clsBeTrans_movimientos

                    clsPublic.CopyObject(pBeMovs, pBeMovsLote)

                    pBeMovsLote.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeMovsLote.IdTipoTarea = clsDataContractDI.tTipoTarea.AJLOTEPI

                    clsLnTrans_movimientos.Insertar(pBeMovsLote, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If BeTransInvCiclico.IdProductoEstado <> BeTransInvCiclico.IdProductoEst_nuevo AndAlso BeTransInvCiclico.IdProductoEst_nuevo <> 0 Then

                    Dim pBeMovsEstado As New clsBeTrans_movimientos

                    clsPublic.CopyObject(pBeMovs, pBeMovsEstado)

                    pBeMovsEstado.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeMovsEstado.IdTipoTarea = clsDataContractDI.tTipoTarea.CESTI

                    clsLnTrans_movimientos.Insertar(pBeMovsEstado, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                '#AT20260213 Talla Color
                If BeTransInvCiclico.IdProductoTallaColor <> BeTransInvCiclico.IdProductoTallaColor_nuevo Then

                    Dim pBeMovsEstado As New clsBeTrans_movimientos

                    clsPublic.CopyObject(pBeMovs, pBeMovsEstado)

                    pBeMovsEstado.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeMovsEstado.IdTipoTarea = clsDataContractDI.tTipoTarea.TALLACOLOR

                    clsLnTrans_movimientos.Insertar(pBeMovsEstado, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If BeTransInvCiclico.IdUbicacion <> BeTransInvCiclico.IdUbicacion_nuevo AndAlso
                    BeTransInvCiclico.IdUbicacion_nuevo <> 0 Then

                    Dim pBeMovsUbicacion As New clsBeTrans_movimientos

                    clsPublic.CopyObject(pBeMovs, pBeMovsUbicacion)

                    pBeMovsUbicacion.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeMovsUbicacion.IdTipoTarea = clsDataContractDI.tTipoTarea.CUBII

                    pBeMovsUbicacion.IdUbicacionDestino = BeTransInvCiclico.IdUbicacion_nuevo

                    clsLnTrans_movimientos.Insertar(pBeMovsUbicacion, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Positivo Then

                    Dim pBeMovsPositivos As New clsBeTrans_movimientos

                    clsPublic.CopyObject(pBeMovs, pBeMovsPositivos)

                    pBeMovsPositivos.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeMovsPositivos.IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTPI

                    clsLnTrans_movimientos.Insertar(pBeMovsPositivos, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                If pBeAjusteDet.Idtipoajuste = clsDataContractDI.tTipoAjusteWMS.Ajuste_Negativo Then

                    Dim pBeMovsNegativos As New clsBeTrans_movimientos

                    clsPublic.CopyObject(pBeMovs, pBeMovsNegativos)

                    pBeMovsNegativos.IdMovimiento = clsLnTrans_movimientos.MaxID(clsTrans.lConnection, clsTrans.lTransaction) + 1
                    pBeMovsNegativos.IdTipoTarea = clsDataContractDI.tTipoTarea.AJCANTNI

                    clsLnTrans_movimientos.Insertar(pBeMovsNegativos, clsTrans.lConnection, clsTrans.lTransaction)

                End If

                ' Llenar lista de stock
                Dim pBeStock As New clsBeStock()
                pBeStock = clsLnStock.GetSingle(BeTransInvCiclico.IdStock, clsTrans.lConnection, clsTrans.lTransaction)

                If pBeStock IsNot Nothing Then

                    pBeStock.IdStock = BeTransInvCiclico.IdStock
                    pBeStock.IdProductoBodega = BeTransInvCiclico.IdProductoBodega

                    pBeStock.Cantidad = BeTransInvCiclico.Cantidad

                    If BeTransInvCiclico.IdProductoEst_nuevo <> 0 Then
                        pBeStock.ProductoEstado.IdEstado = BeTransInvCiclico.IdProductoEst_nuevo
                        pBeStock.IdProductoEstado = BeTransInvCiclico.IdProductoEst_nuevo
                    Else
                        pBeStock.ProductoEstado.IdEstado = BeTransInvCiclico.IdProductoEstado
                        pBeStock.IdProductoEstado = BeTransInvCiclico.IdProductoEstado
                    End If

                    If BeTransInvCiclico.IdUbicacion_nuevo <> 0 Then
                        pBeStock.IdUbicacion = BeTransInvCiclico.IdUbicacion_nuevo
                    Else
                        pBeStock.IdUbicacion = BeTransInvCiclico.IdUbicacion
                    End If

                    pBeStock.Fecha_vence = BeTransInvCiclico.Fecha_vence

                    If BeTransInvCiclico.Lote <> BeTransInvCiclico.Lote_stock AndAlso BeTransInvCiclico.Lote <> "" Then
                        pBeStock.Lote = BeTransInvCiclico.Lote
                    End If

                    '#AT20260128 Actualizar talla color en stock
                    If BeTransInvCiclico.IdProductoTallaColor <> BeTransInvCiclico.IdProductoTallaColor_nuevo Then
                        pBeStock.IdProductoTallaColor = BeTransInvCiclico.IdProductoTallaColor_nuevo
                    Else
                        pBeStock.IdProductoTallaColor = BeTransInvCiclico.IdProductoTallaColor
                    End If

                    lOperaciones.Add(New KeyValuePair(Of Integer, Integer)(pBeStock.IdStock, tipoAjuste))

                Else

                    pBeStock = New clsBeStock()
                    pBeStock.IdBodega = BeTransInvCiclico.IdBodega
                    pBeStock.IdStock = 0
                    pBeStock.IdProductoBodega = BeTransInvCiclico.IdProductoBodega
                    pBeStock.Cantidad = BeTransInvCiclico.Cantidad
                    pBeStock.IdUbicacion = BeTransInvCiclico.IdUbicacion
                    pBeStock.Fecha_vence = BeTransInvCiclico.Fecha_vence
                    pBeStock.ProductoEstado.IdEstado = BeTransInvCiclico.IdProductoEst_nuevo
                    pBeStock.IdProductoEstado = BeTransInvCiclico.IdProductoEst_nuevo
                    pBeStock.Lote = BeTransInvCiclico.Lote
                    pBeStock.IdUbicacion = BeTransInvCiclico.IdUbicacion

                    pBeStock.IdPropietarioBodega = vIdPropietarioBodega
                    pBeStock.Presentacion.IdPresentacion = BeTransInvCiclico.IdPresentacion
                    pBeStock.IdUnidadMedida = BeTransInvCiclico.IdUnidadMedida
                    pBeStock.IdUbicacion = BeTransInvCiclico.IdUbicacion
                    pBeStock.IdUbicacion_anterior = BeTransInvCiclico.IdUbicacion
                    pBeStock.Fecha_Ingreso = Now
                    pBeStock.Activo = True
                    pBeStock.Peso = 0
                    pBeStock.Temperatura = 0
                    pBeStock.Fec_agr = Now
                    pBeStock.Fec_mod = Now
                    pBeStock.User_agr = BeTransInvCiclico.User_agr
                    pBeStock.User_mod = BeTransInvCiclico.User_agr
                    pBeStock.Pallet_No_Estandar = False
                    pBeStock.Lic_plate = BeTransInvCiclico.lic_plate
                    pBeStock.IdProductoTallaColor = BeTransInvCiclico.IdProductoTallaColor_nuevo

                    clsLnStock.Insertar(pBeStock, clsTrans.lConnection, clsTrans.lTransaction)

                    lOperaciones.Add(New KeyValuePair(Of Integer, Integer)(pBeStock.IdStock, tipoAjuste))

                End If

                ListStockNuevo.Add(pBeStock)

            Next

        Catch ex As Exception
            Throw New Exception($"Error en ProcesarAjustePorTipo: {ex.Message}")
        End Try

    End Sub

    Public Sub Carga_Regularizado_Consolidado(ByVal lConnection As SqlConnection,
                                              ByVal lTransaction As SqlTransaction)

        Try
            dgridRegularizado.DataSource = clsLnTrans_inv_ciclico.Get_All_By_Regularizacion_Inventario_Consolidado(gBeInventario.Idinventarioenc,
                                                                                                                   lConnection,
                                                                                                                   lTransaction)

            If GridViewRegularizado.RowCount > 0 Then
                GridViewRegularizado.OptionsView.ShowFooter = True
                GridViewRegularizado.BestFitColumns(True)

                FormatearColumnaNumerica(GridViewRegularizado, "CantidadContada")
                FormatearColumnaNumerica(GridViewRegularizado, "CantidadEsperada")
                FormatearColumnaNumerica(GridViewRegularizado, "SalidasEntradas")
                FormatearColumnaNumerica(GridViewRegularizado, "Diferencia")
            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub FormatearColumnaNumerica(ByVal view As GridView,
                                         ByVal columna As String)

        view.Columns(columna).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        view.Columns(columna).DisplayFormat.FormatString = "{0:n6}"
        view.Columns(columna).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        view.Columns(columna).SummaryItem.DisplayFormat = "{0:n6}"

    End Sub

End Class
